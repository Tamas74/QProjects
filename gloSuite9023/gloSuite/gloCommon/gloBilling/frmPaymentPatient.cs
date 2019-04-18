using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloBilling.Common;
//Added by Subashish_b on 16/Feb /2011 (integration made on date-10/May/2011) for  working with patient related values
using gloPatient;
//End
namespace gloBilling
{
    public partial class frmPaymentPatient: Form
    {

        #region " Variable Declarations "
        Label label;
        //gloPatientStripControl.gloPatientStripControl oPatientControl = null;
        //Added by Subashish_b on 9/Feb /2011 (integration made on date-10/May/2011) for  comenting old controlname and creating object for new patient banner control
        gloStripControl.gloPatientStrip_FA oPatientControl = null;
        //End
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        Int64 _PatientID = 0;
        Int64 _ClinicID = 1;
        Int64 _UserId = 0;
        string _UserName = "";
        Int64 _EOBPaymentID = 0; // it is used to hold master payement id for multiple claim payment
        EOBPayment.Common.EOBPatientPaymentDetails EOBPatientPaymentMasterLines = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetails();

        string _databaseconnectionstring = "";
        string _messageboxcaption = string.Empty;
        bool _IsFormLoading = false;
        bool _IsOpenForModify = false; 
        bool _IsPaymentGridLoading = false;
        bool _IsUseReserveEntry = false;

        bool _IsLoadingFromExistingPayment = false;
        Int64 _IsLoadingFromExistingPaymentID = 0;

        C1FlexGrid c1ModifyPaymentTempGrid = null;
        EOBPaymentSubType _nEOBNewPaymentType = EOBPaymentSubType.None;

        Int64 _mPaymentClaimNo = 0;// For Modify
        Int64 _mEOBPaymentID = 0;// For Modify
        Int64 _mEOBID = 0;// For Modify
        Int64 _mEOBPaymentDetailID = 0;// For Modify

        private bool _showZeroBalanceClaims = true;

        private bool _IsPaymentCorrectionMode = false;
        private string _paymentPrefix = "GPM#";

        private bool _IsAdjustmentMode = false;
        private bool _IsAllDatesValid = true;
        private bool _IsEditable = false;
        private bool _IsClaimDateMessageShown = true;
     //   private bool _IsFormLoaded = false;
      //  private bool _IsClaimDateMessageHide = false;
     //   private bool _IsC1SinglePaymentClicked = false;
        private Int64 _afterCloseDateChanged = 0;
        decimal _previousamt = 0;
        public int width;

        //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for declaring variables to store PAF values for patient
        private Int64 nPAccountID = 0;
        private Int64 nAccountPatientID = 0;
        private Int64 nGuarantorID = 0;
     //   Int64 _PAccountID = 0;
        private bool _IsPatientAccountFeature = false;
        //End

        private bool isclosecheck = false;

        #endregion " Variable Declarations "

        #region " Property Procedures "

        public bool ShowZeroBalanceClaims
        {
            get { return _showZeroBalanceClaims; }
            set { _showZeroBalanceClaims = value; }
        }

        #endregion

        #region "Grid Constant"
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
        //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  setting current Responsible Party in the grid
        const int COL_CRESP_PARTY = 14;
        //Modified by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  setting current Responsible Party in the grid
        const int COL_DOS_FROM = 15;
        const int COL_DOS_TO = 16;
        const int COL_CPT_CODE = 17;
        const int COL_MODIFIER = 18;
        const int COL_CPT_DESCRIPTON = 19;

        const int COL_CHARGE = 20;
        const int COL_UNIT = 21;
        const int COL_TOTALCHARGE = 22;
        const int COL_PREV_PAID = 23;
        const int COL_PREV_ADJ = 24;

        const int COL_CLM_BALANCE = 25;
        const int COL_PAT_DUE = 26;

        const int COL_CUR_PAYMENT = 27;

        const int COL_CUR_ADJ_TYPECODE = 28;
        const int COL_CUR_ADJ_TYPEDESCRIPTION = 29;
        const int COL_CUR_ADJ_AMOUNT = 30;

        const int COL_SERVICELINE_TYPE = 31;
        const int COL_ISOPENFORMODIFY = 32;
        const int COL_PAY_CLINICID = 33;
        const int COL_PAY_LINESTATUS = 34;

        const int COL_CELLRANGE_R1 = 35;
        const int COL_CELLRANGE_R2 = 36;

        const int COL_TRACK_TRN_ID = 37; //TrackTrnID
        const int COL_TRACK_TRN_DTL_ID = 38; //TrackTrnDtlID
        const int COL_SUB_CLAIM_NO = 39; //sSubClaimNo
        const int COL_PREV_PATIENTPAYMENT_AMT = 40;
        const int COL_CLAIMSUBCLAIM_NO = 41;
        const int COL_HOLD = 42;
        const int COL_TRANSACTION_DATE = 43;
        const int COL_COUNT = 44;
        //End

        //const int COL_DOS_FROM = 14;
        //const int COL_DOS_TO = 15;
        //const int COL_CPT_CODE = 16;
        //const int COL_MODIFIER = 17;
        //const int COL_CPT_DESCRIPTON = 18;

        //const int COL_CHARGE = 19;
        //const int COL_UNIT = 20;
        //const int COL_TOTALCHARGE = 21;
        //const int COL_PREV_PAID = 22;
        //const int COL_PREV_ADJ = 23;

        //const int COL_CLM_BALANCE = 24; 
        //const int COL_PAT_DUE = 25;

        //const int COL_CUR_PAYMENT = 26;

        //const int COL_CUR_ADJ_TYPECODE = 27;
        //const int COL_CUR_ADJ_TYPEDESCRIPTION = 28;
        //const int COL_CUR_ADJ_AMOUNT = 29;

        //const int COL_SERVICELINE_TYPE = 30;
        //const int COL_ISOPENFORMODIFY = 31;
        //const int COL_PAY_CLINICID = 32;
        //const int COL_PAY_LINESTATUS = 33;

        //const int COL_CELLRANGE_R1 = 34;
        //const int COL_CELLRANGE_R2 = 35;

        //const int COL_TRACK_TRN_ID = 36; //TrackTrnID
        //const int COL_TRACK_TRN_DTL_ID = 37; //TrackTrnDtlID
        //const int COL_SUB_CLAIM_NO = 38; //sSubClaimNo
        //const int COL_PREV_PATIENTPAYMENT_AMT = 39;
        //const int COL_CLAIMSUBCLAIM_NO = 40;
        //const int COL_HOLD = 41;
        //const int COL_TRANSACTION_DATE = 42;
        //const int COL_COUNT = 43;
      
        #endregion

        #region " Enumeratioin "

        private enum ColServiceLineType
        {
            None = 0, Claim = 1, ServiceLine = 2, Patient = 3
        }

        #endregion " Enumeratioin "

        #region " Constructor "

        public frmPaymentPatient(Int64 PatientID, bool IsOpenForModify, Int64 mPaymentClaimNo, Int64 mEOBPaymentID, Int64 mEOBID, Int64 mEOBPaymentDetailID,EOBPaymentSubType nEOBNewPaymentType)
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

            _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);
            _PatientID = PatientID;

            _IsOpenForModify = IsOpenForModify;
            _mPaymentClaimNo = mPaymentClaimNo;
            _mPaymentClaimNo = mPaymentClaimNo;
            _mEOBPaymentID = mEOBPaymentID;
            _mEOBID = mEOBID;
            _mEOBPaymentDetailID = mEOBPaymentDetailID;
            _nEOBNewPaymentType = nEOBNewPaymentType;

            _PatientID = PatientID;

            //Added by Mahesh S on 10/Mar /2011 (integration made on date-10/May/2011) for  getting the PAF status from appSetting
            gloAccount objAccount = new gloAccount(_databaseconnectionstring);
            _IsPatientAccountFeature = objAccount.GetPatientAccountFeatureSetting();
            //End
        }

        #endregion " Constructor "

        #region " Form Load Event "

        private void frmPaymentInsurace_Load(object sender, EventArgs e)
        {
            LoadPatientStrip(_PatientID, 0, false);
            if (oPatientControl != null)
            {
                txtPatientSearch.Text = oPatientControl.PatientCode;
                //oPatientControl.ClearControl();
                oPatientControl.PatientID = _PatientID;
                //oPatientControl.SearchOnPatientCode = true;
                ////oPatientControl.SearchOnPatientCode = false;
                //oPatientControl.SearchPatient(txtPatientSearch.Text.Trim());
            }
            //_IsFormLoaded = true;
            SetShowZeroBalance();
            ClearFormData();
            
           

            SetPaymentTrayPopup();

            mskCloseDate.Focus();

            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);
            AllowEditValidation();
        }

        #endregion " Form Load Event "

        #region " C1 Grid Design Methods "

        private void RefreshClaimList()
        {
            //if (_PatientID > 0)
            //{ FillBillingTransaction(_PatientID, 0, false); }

            //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  refresh the claim list of the patients for the account
            if (_IsPatientAccountFeature)
            {

                if (oPatientControl.CmbSelectedPatientID > 0)
                {
                    FillBillingTransaction(_PatientID, 0, false);
                }
                else
                {
                    Int64 nTempAccountPatientID = 0;
                    //  Get Active Patients list available in selected Account.
                    DataTable oTempTable = GetAccountPatients(this.nPAccountID);
                    //  Remove record for current selected patient
                    DataRow[] oTempRow = oTempTable.Select(string.Format("nPatientID = '{0}'", _PatientID));

                    Int64 nTemp1 = 0;
                    Int64 nTemp2 = 0;
                    Int64 nTemp3 = 0;
                    string sTemp4 = "";
                    string sTemp5 = "";

                    foreach (DataRow oRow in oTempRow)
                    {
                        nTemp1 = Convert.ToInt64(oRow[0].ToString());
                        nTemp2 = Convert.ToInt64(oRow[1].ToString());
                        nTemp3 = Convert.ToInt64(oRow[2].ToString());
                        sTemp4 = oRow[3].ToString();
                        sTemp5 = oRow[4].ToString();

                        oTempTable.Rows.Remove(oRow);
                    }

                    foreach (DataRow oRow in oTempRow)
                    {
                        DataRow oNewRow = oTempTable.NewRow();
                        oNewRow[0] = nTemp1;
                        oNewRow[1] = nTemp2;
                        oNewRow[2] = nTemp3;
                        oNewRow[3] = sTemp4;
                        oNewRow[4] = sTemp5;

                        oTempTable.Rows.Add(oNewRow);
                        oTempTable.AcceptChanges();

                    }

                    int i = 0;
                    Boolean bProcessOnce;
                    foreach (DataRow oRow in oTempTable.Rows)
                    {
                        bProcessOnce = false;
                        if (i == 0) { bProcessOnce = true; }

                        nTempAccountPatientID = Convert.ToInt64(oRow["nPatientID"].ToString());
                        if (nTempAccountPatientID > 0)
                            FillBillingTransaction_PAF(nTempAccountPatientID, 0, false, bProcessOnce);

                        i++;
                    }

                }
                //End
            }
            else
            {


                if (_PatientID > 0)
                { FillBillingTransaction(_PatientID, 0, false); }
            }
        }

        private void RefreshFormData()
        {
            //ClearFormData();

            
            RefreshClaimList();

            CalculateRemainingAmount();

            //SetShowZeroBalance();
            //LoadPatientStrip(_PatientID, 0, false);
            if (oPatientControl != null)
            {
                //txtPatientSearch.Text =  oPatientControl.PatientCode;
                //oPatientControl.ClearControl();
                //oPatientControl.PatientID = _PatientID;
                //oPatientControl.SearchOnPatientCode = true;
                //oPatientControl.SearchPatient(txtPatientSearch.Text.Trim());

                // oPatientControl.SetInsuracePaymentType(gloPatientStripControl.InsuracePaymentType.Patient);
                //oPatientControl.FillDetails(_PatientID, gloPatientStripControl.FormName.None, 0, false);

                //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  changing the patient banner control
                //oPatientControl.SetInsuracePaymentType(gloPatientStripControl.InsuracePaymentType.Patient);
                //oPatientControl.IsRevisedPayment = false;
                oPatientControl.FillDetails(_PatientID, gloStripControl.FormName.None, 0, false);
            }
                       

            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);
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
               
                if (c1Payment.Name == c1SinglePaymentTotal.Name || c1Payment.Name == c1MultiplePaymentTotal.Name)
                {
                    c1Payment.Rows.Fixed = 0;
                    c1Payment.ScrollBars = ScrollBars.Horizontal;
                    c1Payment.ForeColor = Color.Maroon;
                }
                else
                #region " Set Headers "
                {
                    c1Payment.SetData(0, COL_GENERAL, "General");
                    c1Payment.SetData(0, COL_PATIENTID , "Patient ID");
                    c1Payment.SetData(0, COL_PATIENTNAME , "Patient");
                    c1Payment.SetData(0, COL_CLAIMNO , "Claim #");
                    //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  displaying Responsible party name and no
                    c1Payment.SetData(0, COL_CRESP_PARTY, "Party");

                    c1Payment.SetData(0, COL_BILLING_TRANSACTON_ID, "Transacton ID");
                    c1Payment.SetData(0, COL_BILLING_TRANSACTON_DETAILID, "Transacton Detail ID");
                    c1Payment.SetData(0, COL_BILLING_TRANSACTON_LINENO, "Transacton Line No");

                    c1Payment.SetData(0, COL_PAYMENT_NO , "Payment No");
                    c1Payment.SetData(0, COL_PAY_DATE , "Payment Date");

                    c1Payment.SetData(0, COL_PAY_EOBPAYMENTID , "EOBPaymentID");
                    c1Payment.SetData(0, COL_PAY_EOBID, "EOBID");
                    c1Payment.SetData(0, COL_PAY_EOBDTLID, "EOBDetailID");
                    c1Payment.SetData(0, COL_PAY_EOBPAYMENTDTLID, "EOBPaymentDetailID");

                    c1Payment.SetData(0, COL_CLAIMDISPNO , "Claim #");
                    c1Payment.SetData(0, COL_DOS_FROM , "DOS");
                    c1Payment.SetData(0, COL_DOS_TO , "DOS To");
                    c1Payment.SetData(0, COL_CPT_CODE , "CPT");
                    c1Payment.SetData(0, COL_CPT_DESCRIPTON , "CPT Description");

                    c1Payment.SetData(0, COL_CHARGE , "Charge");
                    c1Payment.SetData(0, COL_UNIT , "Unit");
                    c1Payment.SetData(0, COL_TOTALCHARGE , "Charge");
                    c1Payment.SetData(0, COL_PREV_PAID , "Prev Paid");
                    c1Payment.SetData(0, COL_PREV_ADJ , "Prev Adj");
                    c1Payment.SetData(0, COL_PAT_DUE , "Pat. Due");
                    c1Payment.SetData(0, COL_CLM_BALANCE , "Balance");
                    

                    c1Payment.SetData(0, COL_CUR_PAYMENT , "Payment");

                    c1Payment.SetData(0, COL_CUR_ADJ_TYPECODE , "Adj Code");
                    c1Payment.SetData(0, COL_CUR_ADJ_TYPEDESCRIPTION , "Adj Description");
                    c1Payment.SetData(0, COL_CUR_ADJ_AMOUNT , "Adj Amount");

                    c1Payment.SetData(0, COL_SERVICELINE_TYPE , "Line Type");
                    c1Payment.SetData(0, COL_ISOPENFORMODIFY , "Open For Modify");
                    c1Payment.SetData(0, COL_PAY_CLINICID , "Clinic ID");
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

                if (rbPayType_Payment.Checked == true )
                {
                    c1Payment.Cols[COL_GENERAL].Visible = false;
                    c1Payment.Cols[COL_PATIENTID].Visible = false;
                    //c1Payment.Cols[COL_PATIENTNAME].Visible = false;
                    //Added by Subashish_b on 24/Jan /2011 (integration made on date-10/May/2011) for  displaying patient name if PAF is true
                    if (_IsPatientAccountFeature)
                    { c1Payment.Cols[COL_PATIENTNAME].Visible = true; }
                    else
                    {
                        c1Payment.Cols[COL_PATIENTNAME].Visible = false;//previous code
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

                    c1Payment.Cols[COL_SERVICELINE_TYPE].Visible = false;
                    c1Payment.Cols[COL_ISOPENFORMODIFY].Visible = false;
                    c1Payment.Cols[COL_PAY_CLINICID].Visible = false;
                    c1Payment.Cols[COL_PAY_LINESTATUS].Visible = false;

                    c1Payment.Cols[COL_CELLRANGE_R1].Visible = false;
                    c1Payment.Cols[COL_CELLRANGE_R2].Visible = false;
                }
                else
                {
                    c1Payment.Cols[COL_GENERAL].Visible = false;
                    c1Payment.Cols[COL_PATIENTID].Visible = false;
                    //c1Payment.Cols[COL_PATIENTNAME].Visible = false;
                    //Added by \glo_b on 24/Jan /2011 (integration made on date-10/May/2011) for  displaying patient name if PAF is true
                    if (_IsPatientAccountFeature)
                    { c1Payment.Cols[COL_PATIENTNAME].Visible = true; }
                    else
                    {
                        c1Payment.Cols[COL_PATIENTNAME].Visible = false;//previous code
                    }
                    //End
                    c1Payment.Cols[COL_CLAIMNO].Visible = false;
                    //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  displaying Responsible party name and no
                    c1Payment.Cols[COL_CRESP_PARTY].Visible = false;
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
                    c1Payment.Cols[COL_MODIFIER].Visible = true;
                    c1Payment.Cols[COL_CPT_DESCRIPTON].Visible = false;

                    c1Payment.Cols[COL_CHARGE].Visible = false;
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

                    c1Payment.Cols[COL_SERVICELINE_TYPE].Visible = false;
                    c1Payment.Cols[COL_ISOPENFORMODIFY].Visible = false;
                    c1Payment.Cols[COL_PAY_CLINICID].Visible = false;
                    c1Payment.Cols[COL_PAY_LINESTATUS].Visible = false;

                    c1Payment.Cols[COL_CELLRANGE_R1].Visible = false;
                    c1Payment.Cols[COL_CELLRANGE_R2].Visible = false;
                    
                }

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
                //try
                //{
                //    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
                //    if (c1Payment.Name == c1SinglePayment.Name)
                //    {
                //        _designWidth = oSetting.LoadGridColumnWidth(c1Payment, gloSettings.ModuleOfGridColumn.PaymentSinglePaymentGrid, _userId);
                //    }
                //    else if (c1Payment.Name == c1MultiplePayment.Name)
                //    {
                //        _designWidth = oSetting.LoadGridColumnWidth(c1Payment, gloSettings.ModuleOfGridColumn.PaymentMultiplePaymentGrid, _userId);
                //    }
                //    oSetting.Dispose();
                //}
                //catch (Exception ex)
                //{

                //}

                if (_designWidth == false)
                {
                    c1Payment.Cols[COL_GENERAL].Width = 0;
                    c1Payment.Cols[COL_PATIENTID].Width = 0;
                    //c1Payment.Cols[COL_PATIENTNAME].Width = 0;
                    //Added by Subashish_b on 24/Jan /2011 (integration made on date-10/May/2011) for  setting width of patient name if PAF is true
                    if (_IsPatientAccountFeature)
                    { c1Payment.Cols[COL_PATIENTNAME].Width = 120; }
                    else
                    {
                        c1Payment.Cols[COL_PATIENTNAME].Width = 0;
                    }
                    //End
                    c1Payment.Cols[COL_CLAIMNO].Width = 0;
                    //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  displaying Responsible party name and no
                    c1Payment.Cols[COL_CRESP_PARTY].Width = 100;
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
                    c1Payment.Cols[COL_DOS_FROM].Width = 90;
                    c1Payment.Cols[COL_DOS_TO].Width = 0;
                    c1Payment.Cols[COL_CPT_CODE].Width = 50;
                    c1Payment.Cols[COL_CPT_DESCRIPTON].Width = 0;
                    c1Payment.Cols[COL_MODIFIER].Width = 75;
                    c1Payment.Cols[COL_CHARGE].Width = 0;
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
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
    

                c1Payment.Cols[COL_CHARGE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_TOTALCHARGE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_PREV_PAID].Style = csCurrencyStyle;
                c1Payment.Cols[COL_PREV_ADJ].Style = csCurrencyStyle;
                c1Payment.Cols[COL_PAT_DUE].Style = csCurrencyStyle;
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
                        csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;

                }
      
 

                //NumericUpDown _numUpDown = new NumericUpDown();
                //_numUpDown.Visible = false;
                //_numUpDown.BorderStyle = BorderStyle.None;
                //_numUpDown.ThousandsSeparator = true;
                //_numUpDown.Minimum = -100;
                //_numUpDown.Maximum = 100;
                //_numUpDown.DecimalPlaces = 2;
                //csEditableCurrencyStyle.Editor = numericUpDown1;
                                
                
                
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
                        csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableStringStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                    csEditableStringStyle.DataType = typeof(System.String);
                    csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableDateStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                    csEditableDateStyle.DataType = typeof(System.DateTime);
                    csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);
                    }

                }
                catch
                {
                    csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                    csPatientRowStyle.DataType = typeof(System.String);
                    csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);

                }
    
  


                #region " Set Payment Action Status "

                if (c1Payment.Name == c1SinglePayment.Name || c1Payment.Name == c1MultiplePayment.Name)
                {
                    string _comboList = "";
                    EOBPayment.gloEOBPaymentPatient ogloEOBPayPat = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);

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
                            csEditableAdjustment.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            csEditableAdjustment.BackColor = Color.White;
                        }

                    }
                    catch
                    {
                        csEditableAdjustment = c1Payment.Styles.Add("cs_EditableAdjustment");
                        csEditableAdjustment.DataType = typeof(System.String);
                        csEditableAdjustment.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableAdjustment.BackColor = Color.White;

                    }
      
                    _comboList = ogloEOBPayPat.GetAdjustmentCodes();
                    csEditableAdjustment.ComboList = _comboList;

                    //C1.Win.C1FlexGrid.CellStyle csEditableParty = c1Payment.Styles.Add("cs_EditableParty");
                    //csEditableParty.DataType = typeof(System.String);
                    //csEditableParty.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    //csEditableParty.BackColor = Color.White;
                    //_comboList = "";
                    //_comboList = ogloEOBPayIns.GetPaymentParty(_PatientID);
                    //csEditableParty.ComboList = _comboList;

                    //C1.Win.C1FlexGrid.CellStyle csEditableReason = c1Payment.Styles.Add("cs_EditableReason");
                    //csEditableReason.DataType = typeof(System.String);
                    //csEditableReason.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    //csEditableReason.BackColor = Color.White;
                    //csEditableReason.ComboList = "...";

                    //ogloEOBPayIns.Dispose();
                    
                }
                

                #endregion " Set Payment Action Status "

                #endregion

                //c1Payment.AllowEditing = false;
                c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Payment.AllowEditing = true;

                if (c1Payment.Name == c1SinglePayment.Name)
                {
                    if (rbPayType_Payment.Checked == true)
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
                    }
                    else
                    {
                        c1Payment.Cols[COL_GENERAL].AllowEditing = false;
                        c1Payment.Cols[COL_PATIENTID].AllowEditing = false;
                        c1Payment.Cols[COL_PATIENTNAME].AllowEditing = false;
                        c1Payment.Cols[COL_CLAIMNO].AllowEditing = false;
                        //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  allow editing status
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
                    }
                    // Newly added columns by pankaj
                    c1Payment.Cols[COL_TRACK_TRN_ID].AllowEditing = false;
                    c1Payment.Cols[COL_TRACK_TRN_DTL_ID].AllowEditing = false;
                    c1Payment.Cols[COL_SUB_CLAIM_NO].AllowEditing = false;
                    c1Payment.Cols[COL_PREV_PATIENTPAYMENT_AMT].AllowEditing = false;
                    c1Payment.Cols[COL_CLAIMSUBCLAIM_NO].AllowEditing = false;
                    c1Payment.Cols[COL_HOLD].AllowEditing = false;
                }
                else
                {
                    for (int i = 0; i <= c1Payment.Cols.Count - 1; i++)
                    {
                        c1Payment.Cols[i].AllowEditing = false;
                    }
                }
                #endregion

                //c1Payment.KeyActionEnter = KeyActionEnum.MoveAcross;

                c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
               
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

        void oPatientControl_OnPatientSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (oPatientControl.PatientID > 0)
            {
                //_PatientID = Convert.ToInt64(oPatientControl.PatientID);
                //Modified by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  getting the PAF values from the new Patient  Banner
                _PatientID = Convert.ToInt64(oPatientControl.CmbSelectedPatientID);
                nPAccountID = oPatientControl.PAccountID;
                //End
                txtPatientSearch.Text = oPatientControl.PatientCode;
                if (sender == null && e == null)
                {
                    txtPatientSearch.Select(); txtPatientSearch.Focus();
                }                                
            }
            else
            {
                //_PatientID = Convert.ToInt64(oPatientControl.PatientID);
                //oPatientControl.ClearControl();
                //Modified by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  getting the PAF values from the new Patient  Banner
                _PatientID = Convert.ToInt64(oPatientControl.CmbSelectedPatientID);
                //End
            }


            if (_IsOpenForModify == false)
            {
                if (rbPayType_Refund.Checked == true || rbPayType_ExitingPayment.Checked == true)
                {
                    #region "Save"

                    

                    #endregion
                }
                else
                {
                    #region "Save"
                    _IsFormLoading = true;
                //    bool _ClaimExists = false;
                //    Int64 _fillEOBPaymentID = 0;
                //    Int64 _fillEOBID = 0;
                //    Int64 _fillEOBPaymentDetailID = 0;

                    
                    #region "Load Claim for New Payment"
                   
                    //Modified by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  code commented
                    //if (oPatientControl.SearchOnClaimNumber == true)
                    //{
                    //    FillBillingTransaction(_PatientID, oPatientControl.ClaimNumber,true);
                    //}
                    //else
                    //{
                    FillBillingTransaction(_PatientID, 0, false);
                    //}
                    //End
                    //Previous code-----
                    //if (oPatientControl.SearchOnClaimNumber == true)
                    //{
                    //    FillBillingTransaction(_PatientID, oPatientControl.ClaimNumber,true);
                    //}
                    //else
                    //{
                    //    FillBillingTransaction(_PatientID, 0,false);
                    //}
                    //End
                    if (oPatientControl != null)
                    {
                        string _fillInsuranceName = "";
                        Int64 _fillInsuranceID = 0;
                        Int32 _fillInsSelfMode = 0;
                        Int64 _fillInsContactID = 0;
                        //subashish comment
                        // oPatientControl.GetSelectedInsurance(out _fillInsuranceID, out _fillInsuranceName, out _fillInsSelfMode, out _fillInsContactID);
                        lblPayer.Text = _fillInsuranceName;
                        lblPayer.Tag = _fillInsuranceID + "~" + _fillInsuranceName + "~" + _fillInsSelfMode + "~" + _fillInsContactID;
                        if (_fillInsSelfMode == 1 && _fillInsuranceName.ToUpper() == "SELF")
                        {
                            lblPayerDisplay.Text = "Patient";
                        }
                        else
                        {
                            lblPayerDisplay.Text = _fillInsuranceName;
                        }

                    }
                    #endregion
                    

                    _IsFormLoading = false;
                    #endregion
                }
            }
            else
            {
                if (rbPayType_Refund.Checked == true || rbPayType_ExitingPayment.Checked == true)
                {
                    #region "Modify"

                    #endregion
                }
                else
                {
                }
            }


            //if (oPatientControl.PatientID > 0)
            //{
            //    cmbPayMode.Select(); cmbPayMode.Focus();
            //}
        }

        void oPatientControl_ControlSize_Changed(object sender, EventArgs e)
        {

        }

        //void oPatientControl_PatientModified(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (oPatientControl.PatientID > 0)
        //        {
        //            _PatientID = oPatientControl.PatientID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //}
        void oPatientControl_PatientChanged(object sender, EventArgs e)
        {
            try
            {
                //Added-Modified existing code by Subashish_b on 22/Feb /2011 (integration made on date-10/May/2011) for  validating Patient ID from the new Patient  Banner
                if (oPatientControl.CmbSelectedPatientID == 0)
                {
                    FillBillingTransaction(oPatientControl.CmbSelectedPatientID, 0, false);
                    #region "Code comented"
                    ////Start Code Added by Subashish -----21-02-2011----------------Start 
                    ////To fill The All Patient data of the account selected

                    //Int64 nTempAccountPatientID = 0;
                    ////  Get Active Patients list available in selected Account.
                    //DataTable oTempTable = GetAccountPatients(this.nPAccountID);
                    ////  Remove record for current selected patient
                    //DataRow[] oTempRow = oTempTable.Select(string.Format("nPatientID = '{0}'", _PatientID));

                    //Int64 nTemp1 = 0;
                    //Int64 nTemp2 = 0;
                    //Int64 nTemp3 = 0;
                    //string sTemp4 = "";
                    //string sTemp5 = "";

                    //foreach (DataRow oRow in oTempRow)
                    //{
                    //    nTemp1 = Convert.ToInt64(oRow[0].ToString());
                    //    nTemp2 = Convert.ToInt64(oRow[1].ToString());
                    //    nTemp3 = Convert.ToInt64(oRow[2].ToString());
                    //    sTemp4 = oRow[3].ToString();
                    //    sTemp5 = oRow[4].ToString();

                    //    oTempTable.Rows.Remove(oRow);
                    //}

                    //foreach (DataRow oRow in oTempRow)
                    //{
                    //    DataRow oNewRow = oTempTable.NewRow();
                    //    oNewRow[0] = nTemp1;
                    //    oNewRow[1] = nTemp2;
                    //    oNewRow[2] = nTemp3;
                    //    oNewRow[3] = sTemp4;
                    //    oNewRow[4] = sTemp5;

                    //    oTempTable.Rows.Add(oNewRow);
                    //    oTempTable.AcceptChanges();

                    //}

                    //int i = 0;
                    //Boolean bProcessOnce;
                    //foreach (DataRow oRow in oTempTable.Rows)
                    //{
                    //    bProcessOnce = false;
                    //    if (i == 0) { bProcessOnce = true; }

                    //    nTempAccountPatientID = Convert.ToInt64(oRow["nPatientID"].ToString());
                    //    if (nTempAccountPatientID > 0)
                    //        FillBillingTransaction_PAF(nTempAccountPatientID, 0, false, bProcessOnce);

                    //    i++;
                    //}


                    //End Code added by subashish  ----------------End 
                    #endregion
                }
                else
                {
                    if (oPatientControl.CmbSelectedPatientID > 0)
                    {                        
                        oPatientControl.FillDetails(oPatientControl.CmbSelectedPatientID, gloStripControl.FormName.None, 0, false);
                        FillBillingTransaction(oPatientControl.CmbSelectedPatientID, 0, false);

                        _PatientID = oPatientControl.CmbSelectedPatientID;
                        nPAccountID = oPatientControl.PAccountID;


                        this.nPAccountID = oPatientControl.PAccountID;
                        this.nGuarantorID = oPatientControl.GuarantorID;
                        this.nAccountPatientID = oPatientControl.AccountPatientID;

                        txtPatientSearch.Text = oPatientControl.PatientCode.ToString();

                    }
                }
                //End
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  working with  new patientbanner delegate
        void oPatientControl_OnAccountChanged(object sender, EventArgs e)
        {
            _PatientID = oPatientControl.CmbSelectedPatientID;
            nPAccountID = oPatientControl.PAccountID;
            nAccountPatientID = oPatientControl.AccountPatientID;
            nGuarantorID = oPatientControl.GuarantorID;

            FillBillingTransaction(oPatientControl.CmbSelectedPatientID, 0, false);
            txtPatientSearch.Text = oPatientControl.PatientCode.ToString();
        }
        //End
        //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  working with  new patientbanner delegate events
        //void oPatientControl_OnAccountPatientChanged(object sender, EventArgs e)
        //{
        //    _PatientID = oPatientControl.CmbSelectedPatientID;
        //    nPAccountID = oPatientControl.PAccountID;
        //    nAccountPatientID = oPatientControl.AccountPatientID;
        //    nGuarantorID = oPatientControl.GuarantorID;

        //    FillBillingTransaction(oPatientControl.CmbSelectedPatientID, 0, false);
        //    txtPatientSearch.Text = oPatientControl.PatientCode.ToString();
        //}
        ////End

        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            try
            {

                if (oPatientControl != null)
                {
                    for (int i = 0; i < pnlPatientStrip.Controls.Count; i++)
                    {
                        if (oPatientControl.Name == pnlPatientStrip.Controls[i].Name)
                        {
                            pnlPatientStrip.Controls.RemoveAt(i);
                            break;
                        }
                    }
                    try
                    {
                        oPatientControl.OnPatientChanged -= new gloStripControl.gloPatientStrip_FA.PatientChanged(oPatientControl_PatientChanged);
                        oPatientControl.OnAccountChanged -= new gloStripControl.gloPatientStrip_FA.AccountChangedHandler(oPatientControl_OnAccountChanged);

                    }
                    catch { }
                    oPatientControl.Dispose();
                    oPatientControl = null;
                }

                //Added by Subashish_b on 22/Feb /2011 (integration made on date-10/May/2011) for  setting the control delagates to the form events
                oPatientControl = new gloStripControl.gloPatientStrip_FA(gloStripControl.FormName.PatientPayment);
                oPatientControl.OnPatientChanged += new gloStripControl.gloPatientStrip_FA.PatientChanged(oPatientControl_PatientChanged);
                oPatientControl.OnAccountChanged += new gloStripControl.gloPatientStrip_FA.AccountChangedHandler(oPatientControl_OnAccountChanged);
                //oPatientControl.OnAccountPatientChanged += new gloStripControl.gloPatientStrip_FA.AccountPatientChangedHandler(oPatientControl_OnAccountPatientChanged);
                oPatientControl.FillDetails(PatientId, gloStripControl.FormName.NewCharges, PatientProviderId, false);
                //End
                this.Controls.Add(oPatientControl);

                oPatientControl.Dock = DockStyle.Top;
                oPatientControl.TabStop = true;
                oPatientControl.SendToBack();
                oPatientControl.Padding = new Padding(3, 3, 3, 0);
                panel2.SendToBack();
                panel1.SendToBack();
                pnlToolStrip.SendToBack();

                //pnlSinglePayment.BringToFront();

                _PatientID = oPatientControl.PatientID;
                //Added by Subashish_b on 22/Feb /2011 (integration made on date-10/May/2011) for  getting the PAF values from the patient banner control
                this.nPAccountID = oPatientControl.PAccountID;
                this.nGuarantorID = oPatientControl.GuarantorID;
                this.nAccountPatientID = oPatientControl.AccountPatientID;
                //End

                #region "OLDPatientStrip Code"
                //oPatientControl = new gloPatientStripControl.gloPatientStripControl(_databaseconnectionstring, SearchEnable,false,false,false);
                //oPatientControl.ControlSize_Changed += new gloPatientStripControl.gloPatientStripControl.ControlSizeChanged(oPatientControl_ControlSize_Changed);
                //oPatientControl.OnPatientSearchKeyPress += new gloPatientStripControl.gloPatientStripControl.PatientSearchKeyPressHandler(oPatientControl_OnPatientSearchKeyPress);
                //oPatientControl.PatientModified += new gloPatientStripControl.gloPatientStripControl.Patient_Modified(oPatientControl_PatientModified);
                //oPatientControl.InsuranceSelected += new gloPatientStripControl.gloPatientStripControl.Insurance_Selected(oPatientControl_InsuranceSelected);
                //oPatientControl.btnDownEnable = true;
                //oPatientControl.btnUpEnable = true;
                //oPatientControl.ShowUpDown();
                //oPatientControl.DTP.Visible = false;
                //oPatientControl.ShowInsurances = true;
                //oPatientControl.ShowTotalBalance = true;
                //oPatientControl.ShowAlerts = true;
                //oPatientControl.ShowSearch = false;
                //oPatientControl.SetInsuracePaymentType(gloPatientStripControl.InsuracePaymentType.Patient);
                //oPatientControl.FillDetails(PatientId, gloPatientStripControl.FormName.None, PatientProviderId, false);
                //pnlPatientStrip.Controls.Add(oPatientControl);
                //oPatientControl.Dock = DockStyle.Top;
                //oPatientControl.Padding = new Padding(0, 0, 3, 0);
                //oPatientControl.TabStop = false;
                //pnlSinglePayment.BringToFront();
                //End
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        void oPatientControl_InsuranceSelected(long InsuranceID, string InsuranceName, int InsuraceSelfMode, long ContactID)
        {
            try
            {
                lblPayer.Text = InsuranceName;
                lblPayer.Tag = InsuranceID + "~" + InsuranceName + "~" + InsuraceSelfMode + "~" + ContactID;
                if (InsuraceSelfMode == 1 && InsuranceName.ToUpper() == "SELF")
                {
                    lblPayerDisplay.Text = "Patient";
                }
                else
                {
                    lblPayerDisplay.Text = InsuranceName;
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
        }

        #endregion " Patient Strip Control Events "

        #region "Methods and Procedures"

        //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  working with fillbilling transaction same copy modified(FillBillingTransaction)
        private void FillBillingTransaction_PAF(Int64 PatientId, Int64 ClaimNumber, bool IsClaimSearch, bool bProcessOnce)
        {
            EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
            EOBPayment.Common.PaymentPatientClaim oPaymentPatientClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();
            EOBPayment.Common.PaymentPatientClaims oPaymentPatientClaims = new global::gloBilling.EOBPayment.Common.PaymentPatientClaims();
            int _FocusRowIndex = 0;
            int _ClaimStartRowIndex = 0;


            try
            {
                if (bProcessOnce) // Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  working with fillbilling
                {
                    _IsPaymentGridLoading = true;
                    DesignPaymentGrid(c1SinglePayment);

                    c1SinglePayment.Redraw = false;
                    c1SinglePayment.ScrollBars = ScrollBars.None;
                }


                #region "Get Billied Transaction"
                if (_IsLoadingFromExistingPayment == true && _IsLoadingFromExistingPaymentID > 0)
                {
                    if (_IsOpenForModify == false)
                    {
                        if (rbPayType_Payment.Checked == true)
                        { oPaymentPatientClaims = ogloEOBPaymentPatient.GetBillingExistingTransaction(PatientId, _IsLoadingFromExistingPaymentID); }
                        else if (rbPayType_Refund.Checked == true)
                        { }
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (_IsOpenForModify == false)
                    {
                        if (rbPayType_Payment.Checked == true)
                        {
                            oPaymentPatientClaims = ogloEOBPaymentPatient.GetBillingTransaction_PAF(ClaimNumber, nPAccountID, PatientId, IsClaimSearch, ShowZeroBalanceClaims);
                        }
                        else if (rbPayType_Refund.Checked == true)
                        {
                            oPaymentPatientClaims = ogloEOBPaymentPatient.GetBillingTransaction(ClaimNumber, PatientId, IsClaimSearch, ShowZeroBalanceClaims);
                        }
                    }
                    else
                    {
                        oPaymentPatientClaims = ogloEOBPaymentPatient.GetBillingTransaction(ClaimNumber, PatientId, IsClaimSearch, ShowZeroBalanceClaims);
                    }
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
                                //  c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CRESP_PARTY, oPaymentPatientClaim.RespParty);

                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMDISPNO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMNO, oPaymentPatientClaim.ClaimNo);

                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMSUBCLAIM_NO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, oPaymentPatientClaim.BillingTransactionID);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_SERVICELINE_TYPE, ColServiceLineType.Claim);

                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_ISOPENFORMODIFY, _IsOpenForModify);
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
                                    //if (j > 0) 
                                    //{
                                    c1SinglePayment.Rows.Add();
                                    //}
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
                                    c1SinglePayment.SetData(_RowIndex, COL_UNIT, oPaymentPatientClaim.CliamLines[j].Unit);
                                    c1SinglePayment.SetData(_RowIndex, COL_TOTALCHARGE, oPaymentPatientClaim.CliamLines[j].TotalCharges);
                                    c1SinglePayment.SetData(_RowIndex, COL_CUR_PAYMENT, "");

                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PAID, oPaymentPatientClaim.CliamLines[j].LinePreviousPaid);
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_ADJ, oPaymentPatientClaim.CliamLines[j].LinePreviousAdjuestment);


                                    c1SinglePayment.SetData(_RowIndex, COL_PAT_DUE, oPaymentPatientClaim.CliamLines[j].LinePatientDue);
                                    c1SinglePayment.SetData(_RowIndex, COL_CLM_BALANCE, oPaymentPatientClaim.CliamLines[j].LineBalance);


                                    c1SinglePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);
                                    c1SinglePayment.SetData(_RowIndex, COL_ISOPENFORMODIFY, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_CLINICID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PATIENTPAYMENT_AMT, oPaymentPatientClaim.CliamLines[j].LinePreviousPatientPaid);
                                    c1SinglePayment.SetData(_RowIndex, COL_TRANSACTION_DATE, oPaymentPatientClaim.BillingTransactionDate);



                                    #endregion

                                    SetTrackingInformation(_RowIndex, oPaymentPatientClaim.CliamLines[j], j);

                                    // add the code here for sending transaction master & detail id with claim number to newly created function
                                    // to get the trackid & trackdtlid & subclaim number.
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

                                c1SinglePaymentTotal.SetData(0, COL_PREV_PAID, CalculateSinglePaymentTotal(COL_PREV_PAID));
                                c1SinglePaymentTotal.SetData(0, COL_PREV_ADJ, CalculateSinglePaymentTotal(COL_PREV_ADJ));
                                c1SinglePaymentTotal.SetData(0, COL_PAT_DUE, CalculateSinglePaymentTotal(COL_PAT_DUE));
                                c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, CalculateSinglePaymentTotal(COL_CLM_BALANCE));
                                #endregion
                            }
                        }
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
                    c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, 0);
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
                        csTotalHeader.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csTotalHeader.ForeColor = Color.Maroon;
                        csTotalHeader.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csTotalHeader = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                    csTotalHeader.DataType = typeof(System.String);
                    csTotalHeader.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csTotalHeader.ForeColor = Color.Maroon;
                    csTotalHeader.TextAlign = TextAlignEnum.RightCenter;

                }
    
                c1SinglePaymentTotal.Styles[CellStyleEnum.Fixed].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.Styles[CellStyleEnum.Alternate].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.SetData(0, COL_CPT_CODE, "Total : ");
                c1SinglePaymentTotal.SetCellStyle(0, COL_CPT_CODE, csTotalHeader);

                #endregion " Set the style for the total grid "

                //AllowEditValidation();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
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
                if (ogloEOBPaymentPatient != null) { ogloEOBPaymentPatient.Dispose(); }
                if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); }
                if (oPaymentPatientClaims != null) { oPaymentPatientClaims.Dispose(); }
            }
        }
        //End

        private void FillBillingTransaction(Int64 PatientId, Int64 ClaimNumber,bool IsClaimSearch)
        {
            EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
            EOBPayment.Common.PaymentPatientClaim oPaymentPatientClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();
            EOBPayment.Common.PaymentPatientClaims oPaymentPatientClaims = new global::gloBilling.EOBPayment.Common.PaymentPatientClaims();
            int _FocusRowIndex = 0;
            int _ClaimStartRowIndex = 0;
           // Int64 TransactionDate = 0;

            try
            {
                _IsPaymentGridLoading = true;
                DesignPaymentGrid(c1SinglePayment);

                c1SinglePayment.Redraw = false;
                c1SinglePayment.ScrollBars = ScrollBars.None;

                #region "Get Billied Transaction"
                if (_IsLoadingFromExistingPayment == true && _IsLoadingFromExistingPaymentID > 0)
                {
                    if (_IsOpenForModify == false)
                    {
                        if (rbPayType_Payment.Checked == true)
                        { oPaymentPatientClaims = ogloEOBPaymentPatient.GetBillingExistingTransaction(PatientId, _IsLoadingFromExistingPaymentID); }
                        else if (rbPayType_Refund.Checked == true)
                        { }
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    if (_IsOpenForModify == false)
                    {
                        if (rbPayType_Payment.Checked == true)
                        {
                            //Added by Subashish_b on 16/March/2011 for calling GetBillingTransaction_PAF
                            oPaymentPatientClaims = ogloEOBPaymentPatient.GetBillingTransaction_PAF(ClaimNumber, nPAccountID, PatientId, IsClaimSearch, ShowZeroBalanceClaims);
                            //End
                            //oPaymentPatientClaims = ogloEOBPaymentPatient.GetBillingTransaction(ClaimNumber, PatientId, IsClaimSearch, ShowZeroBalanceClaims);
                        }
                        else if (rbPayType_Refund.Checked == true)
                        { 
                            oPaymentPatientClaims = ogloEOBPaymentPatient.GetBillingTransaction(ClaimNumber, PatientId, IsClaimSearch, ShowZeroBalanceClaims);
                        }
                    }
                    else
                    {
                        oPaymentPatientClaims = ogloEOBPaymentPatient.GetBillingTransaction(ClaimNumber, PatientId, IsClaimSearch, ShowZeroBalanceClaims);
                    }
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

                                //Added by Subashish_b on 16/March/2011 for  Displaying Patient Name and Responsible party 
                                if (_IsPatientAccountFeature)
                                {
                                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PATIENTNAME, oPaymentPatientClaim.PatientName);
                                }
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CRESP_PARTY, oPaymentPatientClaim.RespParty);
                                //End
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMDISPNO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMNO, oPaymentPatientClaim.ClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMSUBCLAIM_NO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, oPaymentPatientClaim.BillingTransactionID);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_SERVICELINE_TYPE, ColServiceLineType.Claim);

                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_ISOPENFORMODIFY, _IsOpenForModify);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PAY_EOBID, 0);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_TRANSACTION_DATE, oPaymentPatientClaim.BillingTransactionDate);
                                c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].Style = c1SinglePayment.Styles["cs_ClaimRowStyle"];

                                _ClaimStartRowIndex = c1SinglePayment.Rows.Count - 1;
                                c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R1, c1SinglePayment.Rows.Count - 1);
                                
                                #endregion

                                for (int j = 0; j <= oPaymentPatientClaim.CliamLines.Count - 1; j++)
                                {
                                    //if (j > 0) 
                                    //{
                                    c1SinglePayment.Rows.Add();
                                    //}
                                    int _RowIndex = c1SinglePayment.Rows.Count - 1;
                                    if (_FocusRowIndex == 0) { _FocusRowIndex = _RowIndex; }

                                    #region "Service Lines"

                                    c1SinglePayment.SetData(_RowIndex, COL_GENERAL, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PATIENTID, oPaymentPatientClaim.CliamLines[j].PatientID);
                                    c1SinglePayment.SetData(_RowIndex, COL_PATIENTNAME, "");

                                    c1SinglePayment.SetData(_RowIndex, COL_CLAIMNO, oPaymentPatientClaim.CliamLines[j].ClaimNumber);
                                    //Added by Subashish_b on 18/March/2011 for setting value ""
                                    c1SinglePayment.SetData(_RowIndex, COL_CRESP_PARTY, "");
                                    //End
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
                                    c1SinglePayment.SetData(_RowIndex, COL_UNIT, oPaymentPatientClaim.CliamLines[j].Unit);
                                    c1SinglePayment.SetData(_RowIndex, COL_TOTALCHARGE, oPaymentPatientClaim.CliamLines[j].TotalCharges);
                                    c1SinglePayment.SetData(_RowIndex, COL_CUR_PAYMENT, "");

                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PAID, oPaymentPatientClaim.CliamLines[j].LinePreviousPaid);
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_ADJ, oPaymentPatientClaim.CliamLines[j].LinePreviousAdjuestment);


                                    c1SinglePayment.SetData(_RowIndex, COL_PAT_DUE, oPaymentPatientClaim.CliamLines[j].LinePatientDue);
                                    c1SinglePayment.SetData(_RowIndex, COL_CLM_BALANCE, oPaymentPatientClaim.CliamLines[j].LineBalance);


                                    c1SinglePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);
                                    c1SinglePayment.SetData(_RowIndex, COL_ISOPENFORMODIFY, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_CLINICID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, "");
                                    //c1SinglePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, oPaymentPatientClaim.CliamLines[j].BLInsuranceName);
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PATIENTPAYMENT_AMT, oPaymentPatientClaim.CliamLines[j].LinePreviousPatientPaid);
                                    c1SinglePayment.SetData(_RowIndex, COL_TRANSACTION_DATE, oPaymentPatientClaim.BillingTransactionDate);
                                    #endregion

                                    SetTrackingInformation(_RowIndex, oPaymentPatientClaim.CliamLines[j], j);

                                    // add the code here for sending transaction master & detail id with claim number to newly created function
                                    // to get the trackid & trackdtlid & subclaim number.
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

                                c1SinglePaymentTotal.SetData(0, COL_PREV_PAID, CalculateSinglePaymentTotal(COL_PREV_PAID));
                                c1SinglePaymentTotal.SetData(0, COL_PREV_ADJ, CalculateSinglePaymentTotal(COL_PREV_ADJ));
                                c1SinglePaymentTotal.SetData(0, COL_PAT_DUE, CalculateSinglePaymentTotal(COL_PAT_DUE));
                                c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, CalculateSinglePaymentTotal(COL_CLM_BALANCE));
                                #endregion
                            }
                        }
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
                    c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, 0);
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
                        csTotalHeader.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csTotalHeader.ForeColor = Color.Maroon;
                        csTotalHeader.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csTotalHeader = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                    csTotalHeader.DataType = typeof(System.String);
                    csTotalHeader.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csTotalHeader.ForeColor = Color.Maroon;
                    csTotalHeader.TextAlign = TextAlignEnum.RightCenter;

                }
  
                c1SinglePaymentTotal.Styles[CellStyleEnum.Fixed].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.Styles[CellStyleEnum.Alternate].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.SetData(0, COL_CPT_CODE, "Total : ");
                c1SinglePaymentTotal.SetCellStyle(0, COL_CPT_CODE, csTotalHeader);

                #endregion " Set the style for the total grid "

                //AllowEditValidation();
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
                if (ogloEOBPaymentPatient != null) { ogloEOBPaymentPatient.Dispose(); }
                if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); }
                if (oPaymentPatientClaims != null) { oPaymentPatientClaims.Dispose(); }
            }
        }

        private void AllowClaimsEdit()
        {
             gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            Object _retValue = null;
            string _clsDate = "";
            Int64 _closeDate = 0;
            
            StringBuilder _ClaimNumbers = new StringBuilder();
            String FormattedClaimNo = "";

            String _ClaimValidationMesssage = "";
            #region "GetCloseDate"
            MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mskDate.Text == "")
            {
                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", _UserId, _ClinicID, out _retValue);
                oSettings.Dispose();
                oSettings = null;
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
                    ogloBilling.Dispose();
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
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }

        }

        void SetTrackingInformation(int RowIndex, EOBPayment.Common.PaymentPatientLine oCliamLine,int index)
        {
            try
            {
                EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);

                Int64 TrackTrnID = 0;
                Int64 TrackTrnDtlID = 0;
                string sSubClaimNo = string.Empty;
                string sHold = String.Empty;
                #region " Collect Tracking Information "

                DataTable dtTrackInfo = ogloEOBPaymentPatient.GetTrackingInformation(oCliamLine.ClaimNumber, oCliamLine.BLTransactionID, oCliamLine.BLTransactionDetailID);

                if (dtTrackInfo != null)
                {
                    if (dtTrackInfo.Rows.Count > 0)
                    {
                        DataRow drTrackInfo = dtTrackInfo.Rows[0];
                        TrackTrnID = Convert.ToInt64(drTrackInfo["nTransactionID"]);
                        TrackTrnDtlID = Convert.ToInt64(drTrackInfo["nTransactionDetailID"]);
                        sSubClaimNo = Convert.ToString(drTrackInfo["nSubClaimNo"]);
                        if (index == 0 )
                        {
                            sHold = Convert.ToString(drTrackInfo["HoldInfo"]);
                        }
                    }
                }

                #endregion

                #region " Setting Tracking Information "

                c1SinglePayment.SetData(RowIndex, COL_TRACK_TRN_ID, TrackTrnID);
                c1SinglePayment.SetData(RowIndex, COL_TRACK_TRN_DTL_ID, TrackTrnDtlID);
                c1SinglePayment.SetData(RowIndex, COL_SUB_CLAIM_NO, sSubClaimNo);
                if (sHold != string.Empty && index == 0)
                { c1SinglePayment.SetData(RowIndex - 1, COL_HOLD, sHold); }

                #endregion
            }
            catch (Exception)
            { }
        }
        private void FillPaymentTransaction(Int64 PatientId, Int64 ClaimNumber, Int64 fillEOBPaymentID, Int64 fillEOBID, Int64 fillEOBPaymentDetailID)
        {
            EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
            EOBPayment.Common.PaymentPatientClaim oPaymentPatientClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();

            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(_databaseconnectionstring);
            EOBPayment.Common.PaymentInsuranceClaim oPaymentInsuranceClaim = new EOBPayment.Common.PaymentInsuranceClaim();

         //   int _FocusRowIndex = 0;
        //    int _ClaimStartRowIndex = 0;
         //   Int64 TransactionId = 0;

            try
            {
                _IsPaymentGridLoading = true;
                DesignPaymentGrid(c1SinglePayment);

                c1SinglePayment.Redraw = false;
                c1SinglePayment.ScrollBars = ScrollBars.None;

                //#region " Get Cliam Billing Transaction ID "

                //TransactionId = ogloEOBPaymentInsurance.GetBillingTransactionID(ClaimNumber);

                //#endregion " Get Cliam Billing Transaction ID "

                //#region "Get Billied Transaction"
                //if (_IsOpenForModify == true)
                //{
                //    if (rbPayType_Payment.Checked == true)
                //    { oPaymentInsuranceClaim = ogloEOBPaymentInsurance.GetPaymentTransaction(TransactionId, fillEOBPaymentID, fillEOBID, PatientId); }
                //    else if (rbPayType_Refund.Checked == true)
                //    { oPaymentInsuranceClaim = ogloEOBPaymentInsurance.GetPaymentTransaction(TransactionId, fillEOBPaymentID, fillEOBID, PatientId); }
                //}

                //#endregion

                //#region "Fill Billied Transaction"
                //if (oPaymentInsuranceClaim != null)
                //{
                //    if (oPaymentInsuranceClaim.CliamLines.Count > 0)
                //    {
                //        #region "Master Data"
                //        c1SinglePayment.Rows.Add();
                //        c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].AllowEditing = false;

                //        c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMDISPNO, oPaymentInsuranceClaim.DisplayClaimNo);
                //        c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMNO, oPaymentInsuranceClaim.ClaimNo);

                //        c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, oPaymentInsuranceClaim.BillingTransactionID);
                //        c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_SERVICELINE_TYPE, ColServiceLineType.Claim);

                //        c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_ISOPENFORMODIFY, _IsOpenForModify);
                //        c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PAY_EOBID, 0);

                //        c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].Style = c1SinglePayment.Styles["cs_ClaimRowStyle"];

                //        _ClaimStartRowIndex = c1SinglePayment.Rows.Count - 1;
                //        c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R1, c1SinglePayment.Rows.Count - 1);

                //        #endregion

                //        for (int j = 0; j <= oPaymentInsuranceClaim.CliamLines.Count - 1; j++)
                //        {
                //            //if (j > 0) 
                //            //{
                //            c1SinglePayment.Rows.Add();
                //            //}
                //            int _RowIndex = c1SinglePayment.Rows.Count - 1;
                //            if (_FocusRowIndex == 0) { _FocusRowIndex = _RowIndex; }

                //            #region "Service Lines"

                //            c1SinglePayment.SetData(_RowIndex, COL_GENERAL, "");
                //            c1SinglePayment.SetData(_RowIndex, COL_PATIENTID, oPaymentInsuranceClaim.CliamLines[j].PatientID);
                //            c1SinglePayment.SetData(_RowIndex, COL_PATIENTNAME, "");

                //            c1SinglePayment.SetData(_RowIndex, COL_CLAIMNO, oPaymentInsuranceClaim.CliamLines[j].ClaimNumber);

                //            c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_ID, oPaymentInsuranceClaim.CliamLines[j].BLTransactionID);
                //            c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_DETAILID, oPaymentInsuranceClaim.CliamLines[j].BLTransactionDetailID);
                //            c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_LINENO, oPaymentInsuranceClaim.CliamLines[j].BLTransactionLineNo);

                //            c1SinglePayment.SetData(_RowIndex, COL_PAYMENT_NO, j + 1);
                //            c1SinglePayment.SetData(_RowIndex, COL_PAY_DATE, "");
                //            c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, ""); //TO DO
                //            c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBID, 0);
                //            c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBDTLID, 0);
                //            c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTDTLID, 0);

                //            c1SinglePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(oPaymentInsuranceClaim.CliamLines[j].DOSFrom).ToShortDateString());
                //            c1SinglePayment.SetData(_RowIndex, COL_DOS_TO, gloDateMaster.gloDate.DateAsDate(oPaymentInsuranceClaim.CliamLines[j].DOSTo).ToShortDateString());
                //            c1SinglePayment.SetData(_RowIndex, COL_CPT_CODE, oPaymentInsuranceClaim.CliamLines[j].CPTCode);
                //            c1SinglePayment.SetData(_RowIndex, COL_CPT_DESCRIPTON, oPaymentInsuranceClaim.CliamLines[j].CPTDescription);

                //            c1SinglePayment.SetData(_RowIndex, COL_CHARGE, oPaymentInsuranceClaim.CliamLines[j].Charges);
                //            c1SinglePayment.SetData(_RowIndex, COL_UNIT, oPaymentInsuranceClaim.CliamLines[j].Unit);
                //            c1SinglePayment.SetData(_RowIndex, COL_TOTALCHARGE, oPaymentInsuranceClaim.CliamLines[j].TotalCharges);
                //            c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Allowed);

                //            c1SinglePayment.SetData(_RowIndex, COL_PAYMENT, oPaymentInsuranceClaim.CliamLines[j].InsuranceAmount);
                //            c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].WriteOff);
                //            c1SinglePayment.SetData(_RowIndex, COL_COPAY, oPaymentInsuranceClaim.CliamLines[j].Copay);
                //            c1SinglePayment.SetData(_RowIndex, COL_DEDUCTIBLE, oPaymentInsuranceClaim.CliamLines[j].Deductible);
                //            c1SinglePayment.SetData(_RowIndex, COL_COINSURANCE, oPaymentInsuranceClaim.CliamLines[j].CoInsurance);
                //            c1SinglePayment.SetData(_RowIndex, COL_WITHHOLD, oPaymentInsuranceClaim.CliamLines[j].Withhold);

                //            c1SinglePayment.SetData(_RowIndex, COL_PREVPAID, "");
                //            c1SinglePayment.SetData(_RowIndex, COL_BALANCE, "");

                //            c1SinglePayment.SetData(_RowIndex, COL_NEXT, "");
                //            c1SinglePayment.SetData(_RowIndex, COL_PARTY, "");
                //            c1SinglePayment.SetData(_RowIndex, COL_REASON, "");

                //            c1SinglePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);
                //            c1SinglePayment.SetData(_RowIndex, COL_ISOPENFORMODIFY, "");
                //            c1SinglePayment.SetData(_RowIndex, COL_PAY_CLINICID, 0);
                //            c1SinglePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, "");

                //            #endregion

                //            #region "Allow Editing"
                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_ALLOWED, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_PAYMENT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_WRITEOFF, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_COPAY, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_DEDUCTIBLE, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_COINSURANCE, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_WITHHOLD, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);

                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_NEXT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_PARTY, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                //            c1SinglePayment.SetCellStyle(_RowIndex, COL_REASON, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                //            #endregion

                //        }
                //        c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R2, c1SinglePayment.Rows.Count - 1);


                //        #region "Calculate Total of Clims"
                //        c1SinglePaymentTotal.SetData(0, COL_CHARGE, CalculateSinglePaymentTotal(COL_CHARGE));
                //        c1SinglePaymentTotal.SetData(0, COL_ALLOWED, CalculateSinglePaymentTotal(COL_ALLOWED));
                //        c1SinglePaymentTotal.SetData(0, COL_PAYMENT, CalculateSinglePaymentTotal(COL_PAYMENT));

                //        c1SinglePaymentTotal.SetData(0, COL_WRITEOFF, CalculateSinglePaymentTotal(COL_WRITEOFF));

                //        c1SinglePaymentTotal.SetData(0, COL_COPAY, CalculateSinglePaymentTotal(COL_COPAY));
                //        c1SinglePaymentTotal.SetData(0, COL_DEDUCTIBLE, CalculateSinglePaymentTotal(COL_DEDUCTIBLE));
                //        c1SinglePaymentTotal.SetData(0, COL_COINSURANCE, CalculateSinglePaymentTotal(COL_COINSURANCE));
                //        c1SinglePaymentTotal.SetData(0, COL_WITHHOLD, CalculateSinglePaymentTotal(COL_WITHHOLD));

                //        c1SinglePaymentTotal.SetData(0, COL_PREVPAID, CalculateSinglePaymentTotal(COL_PREVPAID));
                //        c1SinglePaymentTotal.SetData(0, COL_BALANCE, CalculateSinglePaymentTotal(COL_BALANCE));
                //        c1SinglePaymentTotal.SetData(0, COL_ADJ_AMOUNT, CalculateSinglePaymentTotal(COL_ADJ_AMOUNT));
                //        #endregion
                //    }
                //}
                //#endregion

                //#region "Set Index"
                //if (_FocusRowIndex > 0)
                //{
                //    for (int t = COL_CHARGE; t <= COL_BALANCE; t++)
                //    {
                //        decimal _TotAmount = 0;

                //        for (int r = 1; r <= c1SinglePayment.Rows.Count - 1; r++)
                //        {
                //            if (c1SinglePayment.GetData(r, t) != null && c1SinglePayment.GetData(r, t).ToString() != "")
                //            {
                //                _TotAmount = _TotAmount + Convert.ToDecimal(c1SinglePayment.GetData(r, t).ToString());
                //            }
                //        }
                //        c1SinglePaymentTotal.SetData(0, t, _TotAmount);
                //    }

                //    c1SinglePayment.Select(_FocusRowIndex, COL_ALLOWED);
                //    c1SinglePayment.Focus();

                //    //SetDetailGridLineData(); //TO DO
                //}
                //#endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _IsPaymentGridLoading = false;
                c1SinglePayment.Redraw = true;
                c1SinglePayment.ScrollBars = ScrollBars.Vertical;
                if (ogloEOBPaymentPatient != null) { ogloEOBPaymentPatient.Dispose(); }
                if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); }
            }
        }

        private void FillPaymentTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(_databaseconnectionstring);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Int64 _lastselectedTrayId = 0;
            Object _retVal = null;

            try
            {
                //if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
                //{
                    #region " .... Get the last selected Payment tray ... "

                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                    Object _retSettingValue = null;
                    oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserId, _ClinicID, out _retSettingValue);
                    oSettings.Dispose();

                    if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                    { _lastselectedTrayId = Convert.ToInt64(_retSettingValue); }

                    #endregion " .... Get the last selected Payment tray ... "

                    #region " ... Get the default Payment Tray .... "

                    _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK) " +
                   " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                   " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + _UserId + " AND nClinicID = " + _ClinicID + "";
                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                    { _defaultTrayId = Convert.ToInt64(_retVal); }

                    #endregion " ... Get the default Payment Tray .... "

                    //...Load the last selected tray if present or else load the default tray
                    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    _retVal = new object();
                    if (_lastselectedTrayId > 0)
                    {
                        _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _lastselectedTrayId + " and ISNULL(bIsActive,0) = 1 AND nClinicID = " + _ClinicID + "");
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
                        _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + _ClinicID + "");
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
                if (ogloValidateUser != null)
                {
                    ogloValidateUser.Dispose();
                    ogloValidateUser = null;
                }
            }
        }

        private void SetPaymentTrayPopup()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(_databaseconnectionstring);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {
                if (lblPaymentTray.Tag != null && lblPaymentTray.Tag.ToString().Trim().Length > 0)
                {
                    _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK) " +
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

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                        Object _retSettingValue = null;
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserId, _ClinicID, out _retSettingValue);
                        oSettings.Dispose();

                        if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                        {
                            if (Convert.ToString(_retSettingValue).Trim() == _defaultTrayId.ToString())
                            {
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                    oDB.Dispose();
                                    oDB = null;
                                }
                                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                oDB.Connect(false);
                                _retVal = new object();
                                _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + _ClinicID + "");
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
                                frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(_databaseconnectionstring);
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
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                            oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            oDB.Connect(false);
                            _retVal = new object();
                            _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + _ClinicID + "");
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

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                        Object _retSettingValue = null;
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserId, _ClinicID, out _retSettingValue);
                        oSettings.Dispose();

                        if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                        {
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                            oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            oDB.Connect(false);
                            _retVal = new object();
                            _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + Convert.ToInt64(_retSettingValue) + " AND nClinicID = " + _ClinicID + "");
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
                    frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(_databaseconnectionstring);
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
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (ogloValidateUser != null)
                {
                    
                    ogloValidateUser.Dispose();
                    ogloValidateUser = null;
                }
            }
        }

        private void FillPaymentMode()
        {
            cmbPayMode.Items.Clear();
            cmbPayMode.Items.Add(PaymentMode.Cash.ToString());
            cmbPayMode.Items.Add(PaymentMode.Check.ToString());
            cmbPayMode.Items.Add(PaymentMode.CreditCard.ToString());
            cmbPayMode.Items.Add(PaymentMode.MoneyOrder.ToString());
            cmbPayMode.Items.Add(PaymentMode.EFT.ToString());

            for (int i = 0; i <= cmbPayMode.Items.Count - 1; i++)
            {
                if (cmbPayMode.Items[i].ToString() == PaymentMode.Check.ToString())
                {
                    cmbPayMode.SelectedIndex = i;
                    mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    break;
                }
            }
        }

        private bool IsAdmin(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oDataTable = new DataTable();
            bool result = false;
            oDB.Connect(false);
            oDB.Retrive_Query("Select nAdministrator from User_MST WITH (NOLOCK) where nUserID='" + UserId + "' and nAdministrator = 1", out oDataTable);
            if (oDataTable != null)
            {
                if (oDataTable.Rows.Count > 0)
                {
                    result = true;
                }
                oDataTable.Dispose();
                oDataTable = null;
            }
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
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

        private void FillEOBPayments(Int64 EOBPaymentID)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            //gloDatabaseLayer.DBParameter oParameter = new gloDatabaseLayer.DBParameter();
            //DataTable _dt = new DataTable();

            //try
            //{
            //    oDB.Connect(false);
            //    oParameter = new gloDatabaseLayer.DBParameter("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oParameters.Add(oParameter);
            //    oDB.Retrive("BL_SELECT_EOBPAYMENT", oParameters, out _dt);
            //    oDB.Disconnect();
            //    oParameter.Dispose();
            //    oDB.Dispose();

            //    DesignPaymentGrid(c1MultiplePayment);
            //    c1MultiplePayment.MergedRanges.Clear();

            //    if (_dt != null && _dt.Rows.Count > 0)
            //    {
            //        int _RowIndex = 0;

            //        c1MultiplePayment.SetData(0, COL_DOS_FROM, "Patient");
            //        c1MultiplePayment.SetData(0, COL_CPT_CODE, "");

            //        for (int i = 0; i < _dt.Rows.Count; i++)
            //        {
            //            c1MultiplePayment.Rows.Add();
            //            _RowIndex = c1MultiplePayment.Rows.Count - 1;
                        
            //            c1MultiplePayment.SetData(_RowIndex, COL_GENERAL, "");
            //            c1MultiplePayment.SetData(_RowIndex, COL_PATIENTID, Convert.ToInt64(_dt.Rows[i]["nPatientID"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_PATIENTNAME, Convert.ToString(_dt.Rows[i]["PatientName"]));
                        
            //            c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, Convert.ToString(_dt.Rows[i]["PatientName"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_DOS_TO, Convert.ToString(_dt.Rows[i]["PatientName"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_CPT_CODE, Convert.ToString(_dt.Rows[i]["PatientName"]));

            //            #region "Merge Patient Cells"
            //            c1MultiplePayment.AllowMerging = AllowMergingEnum.Custom;
            //            CellRange cs = c1MultiplePayment.GetCellRange(_RowIndex, COL_DOS_FROM, _RowIndex, COL_CPT_CODE);
            //            c1MultiplePayment.MergedRanges.Add(cs, false);
            //            #endregion

            //            EOBPayment.gloEOBPaymentInsurance ogloEOBIns = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(_databaseconnectionstring);
            //            c1MultiplePayment.SetData(_RowIndex, COL_CLAIMNO, Convert.ToString(_dt.Rows[i]["nClaimNo"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_CLAIMDISPNO, ogloEOBIns.GetFormattedClaimPaymentNumber(Convert.ToString(_dt.Rows[i]["nClaimNo"])));
            //            ogloEOBIns.Dispose();
                        
            //            //c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT_NO, j + 1);
            //            //c1MultiplePayment.SetData(_RowIndex, COL_PAY_DATE, "");
            //            //c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID,Convert.ToInt64(_dt.Rows[i]["nEOBID"]));
            //            //c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBID, 0);
            //            //c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBDTLID, 0);

            //            c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, Convert.ToInt64(_dt.Rows[i]["nEOBPaymentID"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBID, Convert.ToInt64(_dt.Rows[i]["nEOBID"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBDTLID, 0);
            //            c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTDTLID, 0);

            //            //c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(oPaymentInsuranceClaim.CliamLines[j].DOSFrom).ToShortDateString());
            //            //c1MultiplePayment.SetData(_RowIndex, COL_DOS_TO, gloDateMaster.gloDate.DateAsDate(oPaymentInsuranceClaim.CliamLines[j].DOSTo).ToShortDateString());
            //            //c1MultiplePayment.SetData(_RowIndex, COL_CPT_CODE, oPaymentInsuranceClaim.CliamLines[j].CPTCode);
            //            //c1MultiplePayment.SetData(_RowIndex, COL_CPT_DESCRIPTON, oPaymentInsuranceClaim.CliamLines[j].CPTDescription);

            //            c1MultiplePayment.SetData(_RowIndex, COL_CHARGE, Convert.ToDecimal(_dt.Rows[i]["dTotalCharges"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, Convert.ToDecimal(_dt.Rows[i]["dTotalCharges"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_ALLOWED, Convert.ToDecimal(_dt.Rows[i]["dTotalCharges"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT, Convert.ToDecimal(_dt.Rows[i]["dPayment"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_WRITEOFF, Convert.ToDecimal(_dt.Rows[i]["dWriteOff"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_COPAY, Convert.ToDecimal(_dt.Rows[i]["dCopay"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_DEDUCTIBLE, Convert.ToDecimal(_dt.Rows[i]["dDeductible"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_COINSURANCE, Convert.ToDecimal(_dt.Rows[i]["dCoInsurance"]));
            //            c1MultiplePayment.SetData(_RowIndex, COL_WITHHOLD, Convert.ToDecimal(_dt.Rows[i]["dWithhold"]));

            //            //c1MultiplePayment.SetData(_RowIndex, COL_NEXT, "");
            //            //c1MultiplePayment.SetData(_RowIndex, COL_PARTY, "");
            //            //c1MultiplePayment.SetData(_RowIndex, COL_REASON, "");

            //            c1MultiplePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);
            //            //c1MultiplePayment.SetData(_RowIndex, COL_ISOPENFORMODIFY, "");
            //            //c1MultiplePayment.SetData(_RowIndex, COL_PAY_CLINICID, 0);
            //            //c1MultiplePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, "");
            //        }
            //    }

            //    #region "Calculate Total of Clims"
            //    c1MultiplePaymentTotal.SetData(0, COL_CHARGE, CalculateMultiplePaymentTotal(COL_CHARGE));
            //    c1MultiplePaymentTotal.SetData(0, COL_ALLOWED, CalculateMultiplePaymentTotal(COL_ALLOWED));
            //    c1MultiplePaymentTotal.SetData(0, COL_PAYMENT, CalculateMultiplePaymentTotal(COL_PAYMENT));

            //    c1MultiplePaymentTotal.SetData(0, COL_WRITEOFF, CalculateMultiplePaymentTotal(COL_WRITEOFF));

            //    c1MultiplePaymentTotal.SetData(0, COL_COPAY, CalculateMultiplePaymentTotal(COL_COPAY));
            //    c1MultiplePaymentTotal.SetData(0, COL_DEDUCTIBLE, CalculateMultiplePaymentTotal(COL_DEDUCTIBLE));
            //    c1MultiplePaymentTotal.SetData(0, COL_COINSURANCE, CalculateMultiplePaymentTotal(COL_COINSURANCE));
            //    c1MultiplePaymentTotal.SetData(0, COL_WITHHOLD, CalculateMultiplePaymentTotal(COL_WITHHOLD));

            //    c1MultiplePaymentTotal.SetData(0, COL_PREVPAID, CalculateMultiplePaymentTotal(COL_PREVPAID));
            //    c1MultiplePaymentTotal.SetData(0, COL_BALANCE, CalculateMultiplePaymentTotal(COL_BALANCE));
            //    #endregion

            //}
            //catch (gloDatabaseLayer.DBException dbEx)
            //{ dbEx.ERROR_Log(dbEx.ToString()); }
            //catch (Exception ex)
            //{ }
            //finally
            //{ }
        }

        private void FillPendingCheck()
        {
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
                throw;
            }
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
                        if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
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
                        if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                        { cmbCardType.Select(); cmbCardType.Focus(); cmbCardType_MouseEnter(null,null); }
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
                        //if (c1SinglePayment.Rows.Count > 1)
                        //{
                        //    for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                        //    {
                        //        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        //        {

                        //            c1SinglePayment.Select(i, COL_CUR_PAYMENT, true);
                        //            c1SinglePayment.Focus();
                        //            break;
                        //        }
                        //    }
                        //}
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
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
           // DataTable dtCategories = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemplates = null;
          //  String CategoryName = "";

            tls_btnReceipt.DropDownItems.Clear();

            try
            {
                oDB.Connect(false);
                tls_btnDefaultReceipt.Visible = false;
                tls_btnReceipt.Visible = false;
                tls_btnDefaultReceipt.Tag = null;

                dtTemplates = ogloTemplate.GetAssociation(gloOffice.AssociationCategories.PatientReceipt);

                if (dtTemplates != null && dtTemplates.Rows.Count > 0)
                {
                    for (int j = 0; j < dtTemplates.Rows.Count; j++)
                    {
                        ToolStripMenuItem oTemplateItem = new ToolStripMenuItem();
                        oTemplateItem.Text = dtTemplates.Rows[j]["sTemplateName"].ToString();
                        oTemplateItem.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                        oTemplateItem.ForeColor = Color.FromArgb(31, 73, 125);
                        oTemplateItem.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        oTemplateItem.Image = global::gloBilling.Properties.Resources.Bullet06;
                        oTemplateItem.ImageAlign = ContentAlignment.MiddleLeft;
                        oTemplateItem.ImageScaling = ToolStripItemImageScaling.None;
                        tls_btnReceipt.DropDownItems.Add(oTemplateItem);
                        oTemplateItem.Click += new EventHandler(oTemplateItem_Click);

                        if (dtTemplates.Rows[j]["bIsDefault"] != null && Convert.ToBoolean(dtTemplates.Rows[j]["bIsDefault"]) == true)
                        {
                            tls_btnDefaultReceipt.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                            tls_btnDefaultReceipt.Visible = true;
                        }
                    }
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
                if (ogloTemplate != null)
                {
                    ogloTemplate.Dispose();
                    ogloTemplate = null;
                }
            }
        }

        #endregion "Methods and Procedures"

        void oTemplateItem_Click(object sender, EventArgs e)
        {
            if (_IsOpenForModify == false)
            {
                if (_IsAllDatesValid == true && SavePaymentValidation())
                {
                    Int64 _payId = 0;
                    _payId = SavePayment();

                    ToolStripMenuItem oTemplateItem = null;
                    oTemplateItem = (ToolStripMenuItem)sender;
                    if (oTemplateItem != null && oTemplateItem.Tag != null && oTemplateItem.Tag.ToString().Trim().Length > 0)
                    {
                        PrintReceipt(_payId, Convert.ToInt64(Convert.ToString(oTemplateItem.Tag)));
                    }
                    oTemplateItem = null;
                }
            }
        }

        private void PrintReceipt(Int64 PaymentId, Int64 TemplateID)
        {
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
       //     DataTable dtAssociation = null;
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
                        gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(_databaseconnectionstring, ogloTemplate);
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
            { }
        }

        private void FillCreditCards()
        {
            CreditCards oCreditCards = new CreditCards(_databaseconnectionstring);
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

        private void SetHideZeroBalance()
        {
            tsb_ShowHideZeroBalance.Tag = "Hide";
            tsb_ShowHideZeroBalance.Text = "Hide Zero &Balance";
            ShowZeroBalanceClaims = true;
            tsb_ShowHideZeroBalance.Image = global ::gloBilling.Properties.Resources.Hide_Zero_Balance;
        }

        private void SetCloseDate()
        {
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
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

                                //gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
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
                                //    //gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                                //    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_clsDate.Trim())) == true)
                                //    { _clsDate = ""; }
                                //    ogloBilling.Dispose();
                                //}

                                //mskCloseDate.Text = _clsDate;
                        string _lastCloseDate = gloBilling.GetUserWiseCloseDay(_UserId, CloseDayType.Payment);
                        mskCloseDate.Text = _lastCloseDate;

                                #endregion " Set last selected close date "
                    }
                }
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
                    c1SinglePayment.ScrollPosition = c1SinglePaymentTotal.ScrollPosition;
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
                                if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)) < 0)
                                {
                                    MessageBox.Show("Adjustment amount cannot be less than 0 (zero).", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                else
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

                                    if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE)).Trim() != "")
                                    { _TotalCharge = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE)); }

                                    if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)).Trim() != "")
                                    { _PrevAdjustment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)); }

                                    if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != "")
                                    { _CurrentPayment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)); }

                                    if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID)).Trim() != "")
                                    { _PrevPayment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID)); }

                                    if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                    { _CurrentAdjusmentAmt = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)); }

                                    //..
                                    if (_CurrentAdjusmentAmt > (_TotalCharge - _PrevAdjustment - _CurrentPayment - _PrevPayment))
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
                                if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PAT_DUE))>0 && Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) > Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PAT_DUE)))
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
                            //....Code changes done by Sagar Ghodke on 20100504
                            //....Code changes done coz we use to show only patient payments in the prev. paid column
                            //....but according to new logic we show insurance + patient paid amount in the prev. paid column
                            //....so added new column which contains on patient paid amount & will check the condition on that 
                            //....below commented code is the existing condition

                            //if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != ""
                            // && c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID)).Trim() != "")

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != ""
                             && c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTPAYMENT_AMT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTPAYMENT_AMT)).Trim() != "")
                            {
                                //....Code changes done by Sagar Ghodke on 20100504
                                //....Code changes done coz we use to show only patient payments in the prev. paid column
                                //....but according to new logic we show insurance + patient paid amount in the prev. paid column
                                //....so added new column which contains on patient paid amount & will check the condition on that 
                                //....below commented code is the existing condition
                                
                                //if ((
                                //    Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) -
                                //    (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) * 2)
                                //    )
                                //    > (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID))))

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

                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != "")
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

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)).Trim() != "")
                            { _PrevAdjustment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)); }

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != "")
                            { _CurrentPayment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)); }

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID)).Trim() != "")
                            { _PrevPayment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID)); }

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                            { _CurrentAdjusmentAmt = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)); }

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                            {
                                if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)) > 0)
                                {
                                    MessageBox.Show("Correction adjustment amount cannot be positive.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            
                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                             && c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)).Trim() != "")
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
                                    MessageBox.Show("Correction adjustment amount cannot be greater than previous adjustment.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    if (_IsOpenForModify == false)
                    {
                        if (txtCheckRemaining.Text != null && txtCheckRemaining.Text.Trim().Length > 0)
                        {
                            //if (c1SinglePayment.GetData(e.Row, e.Col) != null && c1SinglePayment.GetData(e.Row, e.Col).ToString().Trim().Length > 0)
                            //{
                            //    if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(e.Row, e.Col))) > Convert.ToDecimal(txtCheckRemaining.Text))
                            //    {
                            //        c1SinglePayment.SetData(e.Row, e.Col, 0);
                            //    }
                            //}
                            try
                            {
                                //this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                                //c1SinglePaymentTotal.SetData(0, e.Col, CalculateSinglePaymentTotal(e.Col));
                                //CalculateRemainingAmount();
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
                c1MultiplePaymentTotal.Cols[e.Col].Width = c1SinglePayment.Cols[e.Col].Width;
                c1MultiplePayment.Cols[e.Col].Width = c1SinglePayment.Cols[e.Col].Width;

                //gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseConnectionString);
                //oSetting.SaveGridColumnWidth(c1SinglePayment, gloSettings.ModuleOfGridColumn.PaymentSinglePaymentGrid, _userId);
                //oSetting.SaveGridColumnWidth(c1MultiplePayment, gloSettings.ModuleOfGridColumn.PaymentMultiplePaymentGrid, _userId);
                //oSetting.SaveGridColumnWidth(c1Total, gloSettings.ModuleOfGridColumn.PaymentTotalSinglePaymentGrid, _userId);
                //oSetting.SaveGridColumnWidth(c1TotalMultiplePatient, gloSettings.ModuleOfGridColumn.PaymentTotalMultiplePaymentGrid, _userId);
                //oSetting.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }

        private void c1SinglePayment_CellButtonClick(object sender, RowColEventArgs e)
        {
            try
            {

               

                //frmSetupPaymentNotes ofrmSetupPaymentNotes = new frmSetupPaymentNotes(_databaseconnectionstring, _ClinicID);
                //ofrmSetupPaymentNotes.ShowDialog();
                //ofrmSetupPaymentNotes.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }

        private void c1SinglePayment_BeforeEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Col == COL_CUR_PAYMENT || e.Col == COL_CUR_ADJ_AMOUNT)
                {
                    if (txtCheckRemaining.Text.Trim() == "" || Convert.ToDecimal(txtCheckRemaining.Text.Trim()) <= 0)
                    {
                        //MessageBox.Show("There is no remaining amount to allocate", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //e.Cancel = true;
                    }
                }
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
                    for (int i = c1SinglePayment.RowSel ; i <= c1SinglePayment.Rows.Count - 1; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            c1SinglePayment.Select(i, COL_CUR_PAYMENT,true);
                            break;
                        }
                    }
                }
                else if (c1SinglePayment.ColSel == COL_CUR_PAYMENT)
                {
                    //if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) == 0)
                    //{
                    //    try
                    //    {
                    //        this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                    //        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT, null);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //    }
                    //    finally
                    //    { this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                        
                    //}

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
                        { c1SinglePayment.SetData(c1SinglePayment.RowSel, c1SinglePayment.ColSel,null); }
                        else if(c1SinglePayment.ColSel == COL_CUR_ADJ_TYPECODE)
                        { 
                            c1SinglePayment.SetData(c1SinglePayment.RowSel, c1SinglePayment.ColSel,null);
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
                        if (c1SinglePayment.GetData(e.NewRange.r1, COL_SERVICELINE_TYPE) != null
                         && (((ColServiceLineType)c1SinglePayment.GetData(e.NewRange.r1, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                         || ((ColServiceLineType)c1SinglePayment.GetData(e.NewRange.r1, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine))
                            )
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

                                //Added by Subashish_b on 16/Feb /2011 (integration made on date-10/May/2011) for  work with PAF Values  old code modified temporarily.
                                if (_IsPatientAccountFeature)
                                {
                                    Int64 nSelectedClaimPatient = 0;
                                    if (c1SinglePayment.Rows[c1SinglePayment.RowSel][COL_PATIENTID] != null)
                                        nSelectedClaimPatient = Convert.ToInt64(c1SinglePayment.Rows[c1SinglePayment.RowSel][COL_PATIENTID].ToString());

                                    //_PatientID = Convert.ToInt64(c1SinglePayment.Rows[c1SinglePayment.RowSel][COL_PATIENTID].ToString());


                                    if (nSelectedClaimPatient > 0)
                                    {
                                        //if (_subClaimNo.Trim() != "")
                                        //{ oPatientControl.SubClaimNumber = Convert.ToInt64(_subClaimNo); }
                                        //else
                                        //{ oPatientControl.SubClaimNumber = 0; }

                                        //oPatientControl.ClaimNumber = _clmNo;
                                        //Modified  by Subashish_b on 16/Feb/2011 Temp Comments
                                        //oPatientControl.FillClaimInsurances(_clmNo, nSelectedClaimPatient);

                                        // Once code is confirmed commented code can be removed.
                                        // oPatientControl.FillDetails(nSelectedClaimPatient, gloPatientStripControl.FormName.None, 0, false);
                                        // oPatientControl.FillDetails(_PatientID, gloPatientStripControl.FormName.None, 0, false);
                                        // txtPatientSearch.Text = oPatientControl.PatientCode.ToString();
                                        // _PatientID = nSelectedClaimPatient;
                                        // oPatientControl.FillDetails(nSelectedClaimPatient,);
                                        //End
                                    }
                                }
                                else
                                {
                                    // original code

                                    if (oPatientControl != null && oPatientControl.PatientID > 0)
                                    {
                                        //if (_subClaimNo.Trim() != "")
                                        //{ oPatientControl.SubClaimNumber = Convert.ToInt64(_subClaimNo); }
                                        //else
                                        //{ oPatientControl.SubClaimNumber = 0; }
                                        //oPatientControl.ClaimNumber = _clmNo;
                                        //subashish comment
                                        //oPatientControl.FillClaimInsurances(_clmNo, oPatientControl.PatientID);
                                    }
                                }
                                //End
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
            { }
        }

        #endregion

        #region "Multiple"
            private void c1MultiplePaymentTotal_AfterScroll(object sender, RangeEventArgs e)
            {
                try
                {
                    c1MultiplePayment.ScrollPosition = c1MultiplePaymentTotal.ScrollPosition;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null; 
                }
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

        private decimal CalculateMultiplePaymentTotal(int ColNumber)
        {
            decimal _result = 0;
            try
            {
                if (c1MultiplePayment.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1MultiplePayment.Rows.Count - 1; i++)
                    {
                        if (c1MultiplePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1MultiplePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            if (c1MultiplePayment.GetData(i, ColNumber) != null && c1MultiplePayment.GetData(i, ColNumber).ToString() != null && c1MultiplePayment.GetData(i, ColNumber).ToString().Trim().Length > 0)
                            {
                                _result = _result + Convert.ToDecimal(Convert.ToString(c1MultiplePayment.GetData(i, ColNumber)));
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

                if (oList != null && oList.Length == 6)
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
                c1SinglePayment.FinishEditing();

                if (_IsOpenForModify == false)
                {
                    if (_IsAllDatesValid == true && SavePaymentValidation())
                    { 

                        //...If only adjustment is made against charges and there is remaining amount
                        //...present then promp to put remaining in reserves or allocate the amount
                        //...and then allow save the payment
                        //solving sales force case - GLO2011-0011127   
                        if (_IsAdjustmentMode == true && Convert.ToDecimal(txtCheckRemaining.Text) > 0)
                        {
                            MessageBox.Show("Please use the remaining amount or put it into reserve to continue with next payment.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnReserveRemaining.Focus();
                            btnReserveRemaining.Select();
                        } //end
                        else
                        {
                            SavePayment();
                            ClearFormData();

                            #region "After save load the all claims against that patient"

                            if (_IsOpenForModify == false)
                            {
                                if (rbPayType_Refund.Checked == true || rbPayType_ExitingPayment.Checked == true)
                                {
                                    #region "Save"



                                    #endregion
                                }
                                else
                                {
                                    #region "Save"
                                    _IsFormLoading = true;
                                  //  bool _ClaimExists = false;
                                    //Int64 _fillEOBPaymentID = 0;
                                    //Int64 _fillEOBID = 0;
                                    //Int64 _fillEOBPaymentDetailID = 0;


                                    #region "Load Claim for New Payment"
                                    //Old Code
                                    //if (oPatientControl.SearchOnClaimNumber == true)
                                    //{
                                    //    FillBillingTransaction(_PatientID, oPatientControl.ClaimNumber, true);
                                    //}
                                    //else
                                    //{
                                    //    FillBillingTransaction(_PatientID, 0, false);
                                    //}
                                    //End

                                    //Added by Subashish_b on 18/Feb/2011-Temp Comments
                                    //if (oPatientControl.SearchOnClaimNumber == true)
                                    //{
                                    //    FillBillingTransaction(_PatientID, oPatientControl.ClaimNumber, true);
                                    //}
                                    //else
                                    //{
                                    FillBillingTransaction(_PatientID, 0, false);
                                    // }
                                    //End



                                    if (oPatientControl != null)
                                    {
                                        string _fillInsuranceName = "";
                                        Int64 _fillInsuranceID = 0;
                                        Int32 _fillInsSelfMode = 0;
                                        Int64 _fillInsContactID = 0;

                                        //Added by Subashish_b on 18/Feb/2011-Temp Comments
                                        //oPatientControl.GetSelectedInsurance(out _fillInsuranceID, out _fillInsuranceName, out _fillInsSelfMode, out _fillInsContactID);
                                        //End
                                        //subashish comment
                                        //oPatientControl.GetSelectedInsurance(out _fillInsuranceID, out _fillInsuranceName, out _fillInsSelfMode, out _fillInsContactID);
                                        lblPayer.Text = _fillInsuranceName;
                                        lblPayer.Tag = _fillInsuranceID + "~" + _fillInsuranceName + "~" + _fillInsSelfMode + "~" + _fillInsContactID;
                                        if (_fillInsSelfMode == 1 && _fillInsuranceName.ToUpper() == "SELF")
                                        {
                                            lblPayerDisplay.Text = "Patient";
                                        }
                                        else
                                        {
                                            lblPayerDisplay.Text = _fillInsuranceName;
                                        }

                                    }
                                    #endregion


                                    _IsFormLoading = false;
                                    #endregion
                                    //AllowClaimsEdit();
                                }
                            }
                            else
                            {
                                if (rbPayType_Refund.Checked == true || rbPayType_ExitingPayment.Checked == true)
                                {
                                    #region "Modify"

                                    #endregion
                                }
                                else
                                {
                                }
                            }

                            #endregion

                            AllowEditValidation();
                        }
                    }
                }
                else
                { 
                  UpdatePayment();
                  AllowEditValidation();
                }
                //Added by Mahesh_s on 18/Feb/2011-Temp Comments
                //oPatientControl.FillAccountsAndPatients(oPatientControl.CmbSelectedPatientID, nPAccountID);
                oPatientControl.RefreshBalances();
                //End
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }

        #region "Save Methods"

        private Int64 SavePayment()
        {
            Int64 _retPayId = 0;

            if (rbPayType_Payment.Checked == true)
            {
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
            }
            else if (rbPayType_Refund.Checked == true)
            {
            }
            else if (rbPayType_ExitingPayment.Checked == true)
            {
            }

            return _retPayId;
        }
        
        private Int64 SavePatientPayment()
        {
            Int64 _retPayId = 0;

            EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
            EOBPayment.Common.PaymentPatient oPaymentPatient = new global::gloBilling.EOBPayment.Common.PaymentPatient();
            EOBPayment.Common.PaymentPatientClaim oPaymentPatientClaim = null;
            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentDetail = null;
            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentCreditDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail(); 

            try
            {
                Int64 _CloseDayTrayID = 0;
                string _CloseDayTrayCode = "";
                string _CloseDayTrayName = "";
                EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
                

                #region "Validation"

                #endregion

                if ((txtCheckAmount.Text.Trim().Length  > 0 && Convert.ToDecimal(txtCheckAmount.Text) > 0) || _IsAdjustmentMode == true)
                {

                    #region "Payment Tray"
                        _CloseDayTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());
                        _CloseDayTrayCode = "";
                        _CloseDayTrayName = lblPaymentTray.Text;
                    #endregion

                    #region "Payment Mode"
                    if (cmbPayMode.Text != "")
                    {
                        if (cmbPayMode.Text.Trim() == EOBPaymentMode.None.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.None; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.Cash; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.Check; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.EFT; }
                    }
                    #endregion

                    #region " Master Data "

                    oPaymentPatient.PaymentNumber = lblPaymetNo.Text.Trim().Split('#')[1];
                    oPaymentPatient.PaymentNumberPefix = _paymentPrefix;
                    oPaymentPatient.EOBPaymentID = _EOBPaymentID;
                    
                    //...Changes done on 20091027 by Sagar,to remove ref. no field
                    //oPaymentPatient.EOBRefNO = txtEOBRefNumber.Text.Trim();
                    if (_EOBPaymentMode == EOBPaymentMode.Cash)
                    { oPaymentPatient.EOBRefNO = txtCheckNumber.Text.Trim(); }
                    else
                    { oPaymentPatient.EOBRefNO = ""; }

                    oPaymentPatient.PayerName = lblPayer.Text;
                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    oPaymentPatient.PayerID = _PatientID;
                    oPaymentPatient.PayerType = EOBPaymentAccountType.Patient;
                    oPaymentPatient.PaymentMode = _EOBPaymentMode;
                    oPaymentPatient.CheckNumber = txtCheckNumber.Text.Trim(); ;
                    if (txtCheckAmount.Text.Trim() != "") { oPaymentPatient.CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }

                    if (mskCheckDate.MaskCompleted)
                    {
                        mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oPaymentPatient.CheckDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
                    }

                    if (_EOBPaymentMode == EOBPaymentMode.CreditCard)
                    {
                        oPaymentPatient.CardType = cmbCardType.Text.Trim();
                        oPaymentPatient.AuthorizationNo = txtCardAuthorizationNo.Text.Trim();
                        if (mskCreditExpiryDate.MaskCompleted)
                        {
                            mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                            oPaymentPatient.CardExpiryDate = Convert.ToInt64(mskCreditExpiryDate.Text);
                        }
                        oPaymentPatient.CardID = Convert.ToInt64(cmbCardType.SelectedValue);
                    }

                    oPaymentPatient.MSTAccountID = _PatientID;
                    oPaymentPatient.MSTAccountType = EOBPaymentAccountType.Patient;
                    oPaymentPatient.ClinicID = _ClinicID;
                    oPaymentPatient.CreatedDateTime = DateTime.Now;
                    oPaymentPatient.ModifiedDateTime = DateTime.Now;

                    oPaymentPatient.PaymentTrayID = _CloseDayTrayID;
                    oPaymentPatient.PaymentTrayCode = _CloseDayTrayCode;
                    oPaymentPatient.PaymentTrayDesc = _CloseDayTrayName;

                    //Added by Subashish_b on 18/Feb /2011 (integration made on date-10/May/2011) for  setting Parameters value in oPaymentPatient of PAF
                    oPaymentPatient.PAccountID = this.nPAccountID;
                    oPaymentPatient.AccountPatientID = this.nAccountPatientID;
                    oPaymentPatient.GuarantorID = this.nGuarantorID;
                    //End

                    if (mskCloseDate.MaskCompleted == true)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oPaymentPatient.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }

                    oPaymentPatient.UserID = _UserId;
                    oPaymentPatient.UserName = _UserName;

                    #region "Payment Master Note"
                    //Notes if any to main payment to all claim OR main payment to reserve account
                    if (txtPayMstNotes.Text.Trim().Length > 0 )
                    {
                        EOBPayment.Common.PaymentPatientLineNote oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

                        oPaymentPatientLineNote.ClaimNo = 0;
                        oPaymentPatientLineNote.EOBPaymentID = 0;
                        oPaymentPatientLineNote.EOBID = 0;
                        oPaymentPatientLineNote.EOBPaymentDetailID = 0;
                        oPaymentPatientLineNote.BillingTransactionID = 0;
                        oPaymentPatientLineNote.BillingTransactionDetailID = 0;
                        oPaymentPatientLineNote.Code = "";
                        oPaymentPatientLineNote.Description = txtPayMstNotes.Text.Trim();
                        if (txtCheckAmount.Text.Trim() != "")
                        { oPaymentPatientLineNote.Amount = Convert.ToDecimal(txtCheckAmount.Text.Trim()); }
                        oPaymentPatientLineNote.IncludeOnPrint = chkPayMstIncludeNotes.Checked;
                        oPaymentPatientLineNote.ClinicID = _ClinicID;
                        oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientPayment;
                        oPaymentPatientLineNote.PaymentNoteSubType = EOBPaymentSubType.Other;
                        oPaymentPatientLineNote.HasData = true;

                        oPaymentPatient.Notes.Add(oPaymentPatientLineNote);
                        oPaymentPatientLineNote.Dispose();
                    }
                    #endregion

                    #endregion

                    #region "Credit Service Line Entry applicable to all claims, so it goes to master level not line level"
                    //now its one time entry, but for partial payement implement like insurace payment

                   
                    oEOBPatientPaymentCreditDetail.EOBPaymentID = _EOBPaymentID;
                    oEOBPatientPaymentCreditDetail.EOBID = 0;
                    oEOBPatientPaymentCreditDetail.EOBDtlID = 0;
                    oEOBPatientPaymentCreditDetail.EOBPaymentDetailID = 0;
                    oEOBPatientPaymentCreditDetail.RefEOBPaymentID = 0;
                    oEOBPatientPaymentCreditDetail.RefEOBPaymentDetailID = 0;
                    oEOBPatientPaymentCreditDetail.ReserveEOBPaymentID = 0;
                    oEOBPatientPaymentCreditDetail.ReserveEOBPaymentDetailID = 0;
               

                    oEOBPatientPaymentCreditDetail.BillingTransactionID = 0;
                    oEOBPatientPaymentCreditDetail.BillingTransactionDetailID = 0;
                    oEOBPatientPaymentCreditDetail.BillingTransactionLineNo = 0;
                    if (mskCloseDate.MaskCompleted == true)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oEOBPatientPaymentCreditDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                        oEOBPatientPaymentCreditDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }
                    oEOBPatientPaymentCreditDetail.CPTCode = "";
                    oEOBPatientPaymentCreditDetail.CPTDescription = "";

                    if (txtCheckAmount.Text.Trim() != "")
                    { oEOBPatientPaymentCreditDetail.Amount = Convert.ToDecimal(txtCheckAmount.Text); }
                    
                    if (_IsAdjustmentMode == true)
                    { oEOBPatientPaymentCreditDetail.IsNullAmount = true; }
                    else
                    { oEOBPatientPaymentCreditDetail.IsNullAmount = false; }

                    oEOBPatientPaymentCreditDetail.PaymentType = EOBPaymentType.PatientPayment;
                    oEOBPatientPaymentCreditDetail.PaymentSubType = EOBPaymentSubType.Patient;
                    oEOBPatientPaymentCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                    oEOBPatientPaymentCreditDetail.PayMode = _EOBPaymentMode;
                    
                    //Added by Subashish_b on 18/Feb /2011 (integration made on date-10/May/2011) for  set values of PAF
                    oEOBPatientPaymentCreditDetail.PAccountID = this.nPAccountID;
                    oEOBPatientPaymentCreditDetail.AccountPatientID = this.nAccountPatientID;
                    oEOBPatientPaymentCreditDetail.GuarantorID = this.nGuarantorID;
                    //End
                    
                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    oEOBPatientPaymentCreditDetail.AccountID = _PatientID;
                    oEOBPatientPaymentCreditDetail.AccountType = EOBPaymentAccountType.Patient;
                    oEOBPatientPaymentCreditDetail.MSTAccountID = _PatientID;
                    oEOBPatientPaymentCreditDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                    oEOBPatientPaymentCreditDetail.ContactInsID = 0;

                    oEOBPatientPaymentCreditDetail.PatientID = _PatientID;
                    oEOBPatientPaymentCreditDetail.PaymentTrayID = _CloseDayTrayID;
                    oEOBPatientPaymentCreditDetail.PaymentTrayCode = _CloseDayTrayCode;
                    oEOBPatientPaymentCreditDetail.PaymentTrayDescription = _CloseDayTrayName;
                    oEOBPatientPaymentCreditDetail.UserID = _UserId;
                    oEOBPatientPaymentCreditDetail.UserName = _UserName;
                    oEOBPatientPaymentCreditDetail.ClinicID = _ClinicID;

                    oEOBPatientPaymentCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);                    

                    oEOBPatientPaymentCreditDetail.FinanceLieNo = 1;
                    oEOBPatientPaymentCreditDetail.MainCreditLineID = EOBPaymentCreditLineType.MainCreditLine.GetHashCode() ;
                    oEOBPatientPaymentCreditDetail.IsMainCreditLine = true;
                    oEOBPatientPaymentCreditDetail.IsReserveCreditLine = false;
                    oEOBPatientPaymentCreditDetail.IsCorrectionCreditLine = false;
                    oEOBPatientPaymentCreditDetail.RefFinanceLieNo = 0;
                    oEOBPatientPaymentCreditDetail.UseRefFinanceLieNo = false;


                    oPaymentPatient.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentCreditDetail);
                    
                    #endregion

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
                                for (int index = _claimStartIndex; index <= _claimEndIndex ; index++)
                                {
                                    if (
                                        (c1SinglePayment.GetData(index, COL_CUR_PAYMENT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)) > 0) ||
                                        (c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)) > 0)
                                       )
                                    {
                                        _hasDataToSave = true;
                                        break;
                                    }

                                }

                                if (_hasDataToSave == false) { continue; }

                                #endregion

                                oPaymentPatientClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();

                                oPaymentPatientClaim.ClaimNo =  Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_CLAIMNO)));
                                oPaymentPatientClaim.ClaimNoPrefix = "";
                                oPaymentPatientClaim.ClinicID = _ClinicID;
                                oPaymentPatientClaim.DisplayClaimNo = Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_CLAIMDISPNO));
                                oPaymentPatientClaim.PatientID = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_PATIENTID)));
                                oPaymentPatientClaim.PatientName = Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_PATIENTNAME));
                                oPaymentPatientClaim.BillingTransactionID = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_BILLING_TRANSACTON_ID)));

                                #region "Claim wise EOB and Finance Line"
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
                                        && Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)) > 0)
                                       )
                                        {
                                            #region "EOB Service Lines"



                                            EOBPayment.Common.PaymentPatientLine oPaymentPatientLine = new global::gloBilling.EOBPayment.Common.PaymentPatientLine();

                                            #region "EOB Line"
                                            oPaymentPatientLine.PatientID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                            oPaymentPatientLine.PatInsuranceID = 0;// Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                            oPaymentPatientLine.InsContactID = 0;// Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                                            oPaymentPatientLine.BLTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            oPaymentPatientLine.BLTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            oPaymentPatientLine.BLTransactionLineNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                            oPaymentPatientLine.ClaimNumber = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));

                                            oPaymentPatientLine.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                            oPaymentPatientLine.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                            oPaymentPatientLine.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));

                                            oPaymentPatientLine.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                            oPaymentPatientLine.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));

                                            oPaymentPatientLine.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                            oPaymentPatientLine.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                            oPaymentPatientLine.BLInsuranceID = 0;
                                            oPaymentPatientLine.BLInsuranceName = "";
                                            oPaymentPatientLine.BLInsuranceFlag = InsuranceTypeFlag.None;

                                            if (c1SinglePayment.GetData(i, COL_CHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CHARGE)).Trim() != "")
                                            { oPaymentPatientLine.Charges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CHARGE)); oPaymentPatientLine.IsNullCharges = false; }

                                            if (c1SinglePayment.GetData(i, COL_UNIT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_UNIT)).Trim() != "")
                                            { oPaymentPatientLine.Unit = Convert.ToInt64(c1SinglePayment.GetData(i, COL_UNIT)); oPaymentPatientLine.IsNullUnit = false; }

                                            if (c1SinglePayment.GetData(i, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_TOTALCHARGE)).Trim() != "")
                                            { oPaymentPatientLine.TotalCharges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_TOTALCHARGE)); oPaymentPatientLine.IsNullTotalCharges = false; }

                                            
                                            oPaymentPatientLine.Allowed = 0;
                                            oPaymentPatientLine.IsNullAllowed = true;

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            { oPaymentPatientLine.WriteOff = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)); oPaymentPatientLine.IsNullWriteOff = false; }

                                            oPaymentPatientLine.NonCovered = 0;

                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                            { oPaymentPatientLine.InsuranceAmount = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)); oPaymentPatientLine.IsNullInsurance = false; }

                                            oPaymentPatientLine.Copay = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COPAY)); }
                                            oPaymentPatientLine.Deductible = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)); }
                                            oPaymentPatientLine.CoInsurance = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COINSURANCE)); }
                                            oPaymentPatientLine.Withhold = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WITHHOLD)); }

                                            oPaymentPatientLine.PaymentTrayID = _CloseDayTrayID;
                                            oPaymentPatientLine.PaymentTrayCode = _CloseDayTrayCode;
                                            oPaymentPatientLine.PaymentTrayDesc = _CloseDayTrayName;


                                            oPaymentPatientLine.UserID = _UserId;
                                            oPaymentPatientLine.UserName = _UserName;
                                            oPaymentPatientLine.ClinicID = _ClinicID;

                                            oPaymentPatientLine.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                            oPaymentPatientLine.EOBType = EOBPaymentType.PatientPayment;

                                            //Adjestment Information
                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            {
                                                if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) > 0)
                                                {
                                                    EOBPayment.Common.PaymentPatientLineAdjustmentCode oPaymentPatientLineAdjustmentCode = new global::gloBilling.EOBPayment.Common.PaymentPatientLineAdjustmentCode();

                                                    oPaymentPatientLineAdjustmentCode.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                                    oPaymentPatientLineAdjustmentCode.EOBPaymentID = 0;
                                                    oPaymentPatientLineAdjustmentCode.EOBID = 0;
                                                    oPaymentPatientLineAdjustmentCode.EOBPaymentDetailID = 0;

                                                    oPaymentPatientLineAdjustmentCode.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    oPaymentPatientLineAdjustmentCode.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));


                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                                    { oPaymentPatientLineAdjustmentCode.Code = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim(); }
                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim() != "")
                                                    { oPaymentPatientLineAdjustmentCode.Description = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim(); }

                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    { oPaymentPatientLineAdjustmentCode.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); oPaymentPatientLineAdjustmentCode.IsNullAmount = false; }

                                                    oPaymentPatientLineAdjustmentCode.ClinicID = _ClinicID;

                                                    oPaymentPatientLine.LineAdjestmentCodes.Add(oPaymentPatientLineAdjustmentCode);
                                                    oPaymentPatientLineAdjustmentCode.Dispose();
                                                }
                                            }
                                            #endregion

                                            #region "Debit Service Line - Patient"

                                            #region "Debit Service Line - patient "
                                            oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                            oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                            oEOBPatientPaymentDetail.EOBID = 0;
                                            oEOBPatientPaymentDetail.EOBDtlID = 0;
                                            oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                            oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                            oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                            oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                            oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                            oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                            decimal _fillPayAmt = 0; decimal _fillAdjAmt = 0;
                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                            { _fillPayAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT))); oEOBPatientPaymentDetail.IsNullAmount = false; }

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                            //oEOBPatientPaymentDetail.Amount = _fillPayAmt -_fillAdjAmt;
                                            //Initial thought was amount - adjestment is actual payment, but user is going to enter actual payment
                                            //and actual adjestment
                                            //suppose check is 20 $ and in payment he will enter 20 $ and he cas enter adj 10 $,
                                            //which is not releated to that check.
                                            oEOBPatientPaymentDetail.Amount = _fillPayAmt;
                                            

                                            oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                            oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Patient;
                                            oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                            oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;


                                            oEOBPatientPaymentDetail.RefEOBPaymentID = oEOBPatientPaymentCreditDetail.RefEOBPaymentID;
                                            oEOBPatientPaymentDetail.RefEOBPaymentDetailID = oEOBPatientPaymentCreditDetail.RefEOBPaymentDetailID;
                                            oEOBPatientPaymentDetail.ReserveEOBPaymentID = oEOBPatientPaymentCreditDetail.ReserveEOBPaymentID;
                                            oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = oEOBPatientPaymentCreditDetail.ReserveEOBPaymentDetailID;

                                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                            oEOBPatientPaymentDetail.AccountID = _PatientID;
                                            oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                            oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
                                            oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                            oEOBPatientPaymentDetail.PatientID = _PatientID;//Previous code
                                            
                                            //Added by Subashish_b on 18/Feb /2011 (integration made on date-10/May/2011) for  set value of PAF
                                            if (_IsPatientAccountFeature)
                                            {
                                                oEOBPatientPaymentDetail.PatientID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                            }

                                            oEOBPatientPaymentDetail.PAccountID = this.nPAccountID;
                                            oEOBPatientPaymentDetail.GuarantorID = this.nGuarantorID;
                                            oEOBPatientPaymentDetail.AccountPatientID = this.nAccountPatientID;
                                            //End


                                            oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                            oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                            oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                            oEOBPatientPaymentDetail.UserID = _UserId;
                                            oEOBPatientPaymentDetail.UserName = _UserName;
                                            oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                            oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                            oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                            oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                            oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                            oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                            oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                            oEOBPatientPaymentDetail.RefFinanceLieNo = 1;
                                            oEOBPatientPaymentDetail.UseRefFinanceLieNo = false;

                                            // newly added columns by pankaj
                                            oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                            oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                            oEOBPatientPaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));

                                            oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
                                            oEOBPatientPaymentDetail.Dispose();
                                            #endregion

                                            #region "Debit Service Line - patient adjuestment if any"
                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            {
                                                if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) > 0)
                                                {
                                                    oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                                    oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                                    oEOBPatientPaymentDetail.EOBID = 0;
                                                    oEOBPatientPaymentDetail.EOBDtlID = 0;
                                                    oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                                    oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                    oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                                    oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                                    oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                                    oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                    oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    { oEOBPatientPaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); oEOBPatientPaymentDetail.IsNullAmount = false; }

                                                    oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                                    oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Adjuestment;
                                                    oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                                    oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                                    oEOBPatientPaymentDetail.RefEOBPaymentID = 0;
                                                    oEOBPatientPaymentDetail.RefEOBPaymentDetailID = 0;
                                                    oEOBPatientPaymentDetail.ReserveEOBPaymentID = 0;
                                                    oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = 0;

                                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                    oEOBPatientPaymentDetail.AccountID = _PatientID;
                                                    oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                                    oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
                                                    oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                                    oEOBPatientPaymentDetail.PatientID = _PatientID;
                                                    oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                                    oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                                    oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                                    oEOBPatientPaymentDetail.UserID = _UserId;
                                                    oEOBPatientPaymentDetail.UserName = _UserName;
                                                    oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                                    oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);


                                                    oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                                    oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                                    oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                                    oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                                    oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                                    oEOBPatientPaymentDetail.RefFinanceLieNo = 1;
                                                    oEOBPatientPaymentDetail.UseRefFinanceLieNo = false;

                                                    // Newly added columns by pankaj
                                                    oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                    oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                    oEOBPatientPaymentDetail.SubClaimNo  = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));

                                                    //Added by Subashish_b on 18/Feb /2011 (integration made on date-10/May/2011) for  set value PAF
                                                    oEOBPatientPaymentDetail.PAccountID = this.nPAccountID;
                                                    oEOBPatientPaymentDetail.AccountPatientID = this.nAccountPatientID;
                                                    oEOBPatientPaymentDetail.GuarantorID = this.nGuarantorID;
                                                    //End

                                                    oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
                                                    oEOBPatientPaymentDetail.Dispose();
                                                }
                                            }
                                            #endregion

                                            #endregion

                                            oPaymentPatientClaim.CliamLines.Add(oPaymentPatientLine);

                                            oPaymentPatientLine.Dispose();

                                            if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); }

                                            #endregion
                                        }
                                    }
                                }

                                #endregion

                                oPaymentPatient.PatientClaims.Add(oPaymentPatientClaim);
                                oPaymentPatientClaim.Dispose();
                            }
                        }
                    }

                    #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"
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

                        

                        if (oList != null && oList.Length == 6)
                        {
                            //...Condition added on 20100507 by sagar ghodke to avoid zero reserve entry in detail table
                            if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "" && Convert.ToDecimal(oList[0]) > 0)
                            {
                                oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                oEOBPatientPaymentDetail.EOBID = 0;
                                oEOBPatientPaymentDetail.EOBDtlID = 0;
                                oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                oEOBPatientPaymentDetail.BillingTransactionID = 0;
                                oEOBPatientPaymentDetail.BillingTransactionDetailID = 0;
                                oEOBPatientPaymentDetail.BillingTransactionLineNo = 0;
                                if (mskCloseDate.MaskCompleted == true)
                                {
                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                    oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                }
                                oEOBPatientPaymentDetail.CPTCode = "";
                                oEOBPatientPaymentDetail.CPTDescription = "";

                                if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                                { oEOBPatientPaymentDetail.Amount = Convert.ToDecimal(oList[0]); oEOBPatientPaymentDetail.IsNullAmount = false; }

                                //Added by Subashish_b on 18/Feb /2011 (integration made on date-10/May/2011) for  set value of PAF
                                oEOBPatientPaymentDetail.PAccountID = this.nPAccountID;
                                oEOBPatientPaymentDetail.AccountPatientID = this.nAccountPatientID;
                                oEOBPatientPaymentDetail.GuarantorID = this.nGuarantorID;
                                //End

                                oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientReserved;
                                oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Reserved;
                                oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                oEOBPatientPaymentDetail.RefEOBPaymentID = oEOBPatientPaymentCreditDetail.RefEOBPaymentID;
                                oEOBPatientPaymentDetail.RefEOBPaymentDetailID = oEOBPatientPaymentCreditDetail.RefEOBPaymentDetailID;
                                oEOBPatientPaymentDetail.ReserveEOBPaymentID = oEOBPatientPaymentCreditDetail.ReserveEOBPaymentID;
                                oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = oEOBPatientPaymentCreditDetail.ReserveEOBPaymentDetailID;

                                //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                oEOBPatientPaymentDetail.AccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode();
                                oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Reserved;
                                oEOBPatientPaymentDetail.MSTAccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode();
                                oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Reserved;
                                oEOBPatientPaymentDetail.PatientID = _PatientID;
                                oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                oEOBPatientPaymentDetail.UserID = _UserId;
                                oEOBPatientPaymentDetail.UserName = _UserName;
                                oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);


                                oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                oEOBPatientPaymentDetail.RefFinanceLieNo = 1;
                                oEOBPatientPaymentDetail.UseRefFinanceLieNo = true;

                                //0 ReserveAmount 
                                //1 ReserveNote 
                                //2 ReserveSubType 
                                //3 ReserveNoteOnPrint 

                                if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "" && Convert.ToDecimal(oList[0]) > 0)
                                {
                                    #region "General Note"
                                    EOBPayment.Common.PaymentPatientLineNote oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

                                    oPaymentPatientLineNote.ClaimNo = 0;
                                    oPaymentPatientLineNote.EOBPaymentID = 0;
                                    oPaymentPatientLineNote.EOBID = 0;
                                    oPaymentPatientLineNote.EOBPaymentDetailID = 0;
                                    oPaymentPatientLineNote.BillingTransactionID = 0;
                                    oPaymentPatientLineNote.BillingTransactionDetailID = 0;

                                    oPaymentPatientLineNote.Code = "";
                                    oPaymentPatientLineNote.Description = Convert.ToString(oList[1]).Trim();

                                    oPaymentPatientLineNote.Amount = Convert.ToDecimal(oList[0]);

                                    if (oList[3] != null && oList[3].ToString().Trim() != "")
                                    {
                                        oPaymentPatientLineNote.IncludeOnPrint = Convert.ToBoolean(oList[3]);
                                    }
                                    oPaymentPatientLineNote.ClinicID = _ClinicID;
                                    oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientReserved;
                                    if (oList[2] != null && oList[2].ToString().Trim() != "")
                                    {
                                        oPaymentPatientLineNote.PaymentNoteSubType = (EOBPaymentSubType)Convert.ToInt32(oList[2]);
                                    }
                                    oPaymentPatientLineNote.HasData = true;

                                    oEOBPatientPaymentDetail.LineNotes.Add(oPaymentPatientLineNote);
                                    oPaymentPatientLineNote.Dispose();
                                    #endregion

                                    if ((EOBPaymentSubType)Convert.ToInt32(oList[2]) == EOBPaymentSubType.Advance)
                                    {
                                        #region "CPT Note"

                                        if (oList[4] != null && oList[4].ToString().TrimEnd() != "")
                                        {
                                            oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

                                            oPaymentPatientLineNote.ClaimNo = 0;
                                            oPaymentPatientLineNote.EOBPaymentID = 0;
                                            oPaymentPatientLineNote.EOBID = 0;
                                            oPaymentPatientLineNote.EOBPaymentDetailID = 0;
                                            oPaymentPatientLineNote.BillingTransactionID = 0;
                                            oPaymentPatientLineNote.BillingTransactionDetailID = 0;

                                            oPaymentPatientLineNote.Code = "CPT";

                                            if (oList[4] != null && oList[4].ToString().TrimEnd() != "")
                                            {
                                                oPaymentPatientLineNote.Description = Convert.ToString(oList[4]).Replace('^', '~');
                                            }

                                            oPaymentPatientLineNote.Amount = Convert.ToDecimal(oList[0]);
                                            if (oList[3] != null && oList[3].ToString().Trim() != "")
                                            {
                                                oPaymentPatientLineNote.IncludeOnPrint = Convert.ToBoolean(oList[3]);
                                            }
                                            oPaymentPatientLineNote.ClinicID = _ClinicID;
                                            oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientReserved;
                                            oPaymentPatientLineNote.PaymentNoteSubType = EOBPaymentSubType.None;
                                            oPaymentPatientLineNote.HasData = true;

                                            oEOBPatientPaymentDetail.LineNotes.Add(oPaymentPatientLineNote);
                                            oPaymentPatientLineNote.Dispose();
                                        }
                                        #endregion

                                        #region "ICD9 Note"

                                        if (oList[5] != null && oList[5].Trim() != "")
                                        {
                                            oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

                                            oPaymentPatientLineNote.ClaimNo = 0;
                                            oPaymentPatientLineNote.EOBPaymentID = 0;
                                            oPaymentPatientLineNote.EOBID = 0;
                                            oPaymentPatientLineNote.EOBPaymentDetailID = 0;
                                            oPaymentPatientLineNote.BillingTransactionID = 0;
                                            oPaymentPatientLineNote.BillingTransactionDetailID = 0;

                                            oPaymentPatientLineNote.Code = "ICD9";
                                            if (oList[5] != null && oList[5].Trim() != "")
                                            { oPaymentPatientLineNote.Description = Convert.ToString(oList[5]).Replace('^', '~'); }

                                            oPaymentPatientLineNote.Amount = Convert.ToDecimal(oList[0]);
                                            if (oList[3] != null && oList[3].ToString().Trim() != "")
                                            {
                                                oPaymentPatientLineNote.IncludeOnPrint = Convert.ToBoolean(oList[3]);
                                            }
                                            oPaymentPatientLineNote.ClinicID = _ClinicID;
                                            oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientReserved;
                                            oPaymentPatientLineNote.PaymentNoteSubType = EOBPaymentSubType.None;
                                            oPaymentPatientLineNote.HasData = true;

                                            oEOBPatientPaymentDetail.LineNotes.Add(oPaymentPatientLineNote);
                                            oPaymentPatientLineNote.Dispose();
                                        }
                                        #endregion
                                    }
                                }

                                oPaymentPatient.EOBPatientPaymentReserveLineDetail.Add(oEOBPatientPaymentDetail);
                                oEOBPatientPaymentDetail.Dispose();
                            }
                        }
                    }
                    #endregion

                    _EOBPaymentID = ogloEOBPaymentPatient.SavePatientPayment(oPaymentPatient,false, out EOBPatientPaymentMasterLines);

                    //FillEOBPayments(_EOBPaymentID);

                    _retPayId = _EOBPaymentID;
                    _EOBPaymentID = 0;
                    _IsAdjustmentMode = false;
                    EOBPatientPaymentMasterLines.Clear();
                    btnReserveRemaining.Tag = null;
                    
                    #region "Desing Grids"
                    _IsFormLoading = true;
                    rbPayType_Payment_CheckedChanged(null, null);
                    _IsFormLoading = false;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloEOBPaymentPatient != null) { ogloEOBPaymentPatient.Dispose(); };
                if (oPaymentPatient != null) { oPaymentPatient.Dispose(); };
                if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); };
                if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); };
                if (oEOBPatientPaymentCreditDetail != null) { oEOBPatientPaymentCreditDetail.Dispose(); };
            }
            return _retPayId;
        }
        
        private Int64 SavePatientUseReservePayment()
        {
            Int64 _retPayId = 0;
            EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
            EOBPayment.Common.PaymentPatient oPaymentPatient = new global::gloBilling.EOBPayment.Common.PaymentPatient();
            EOBPayment.Common.PaymentPatientClaim oPaymentPatientClaim = null;
            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentDetail = null;
            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentCreditDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail(); 

            try
            {
                

                Int64 _CloseDayTrayID = 0;
                string _CloseDayTrayCode = "";
                string _CloseDayTrayName = "";
                EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;


                #region "Validation"

                #endregion

                if (txtCheckAmount.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckAmount.Text) > 0)
                {

                    #region "Payment Tray"
                        _CloseDayTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());
                        _CloseDayTrayCode = "";
                        _CloseDayTrayName = lblPaymentTray.Text.Trim();
                    #endregion

                    #region "Payment Mode"
                    if (cmbPayMode.Text != "")
                    {
                        if (cmbPayMode.Text.Trim() == EOBPaymentMode.None.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.None; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.Cash; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.Check; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.EFT; }
                    }
                    #endregion

                    #region " Master Data "

                    oPaymentPatient.PaymentNumber = lblPaymetNo.Text.Trim().Split('#')[1];
                    oPaymentPatient.PaymentNumberPefix = _paymentPrefix;
                    oPaymentPatient.EOBPaymentID = _EOBPaymentID;

                    //oPaymentPatient.EOBRefNO = txtEOBRefNumber.Text.Trim();
                    if (_EOBPaymentMode == EOBPaymentMode.Cash)
                    { oPaymentPatient.EOBRefNO = txtCheckNumber.Text.Trim(); }
                    else
                    { oPaymentPatient.EOBRefNO = ""; }

                    oPaymentPatient.PayerName = lblPayer.Text;
                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    oPaymentPatient.PayerID = _PatientID;
                    oPaymentPatient.PayerType = EOBPaymentAccountType.Patient;
                    oPaymentPatient.PaymentMode = _EOBPaymentMode;
                    oPaymentPatient.CheckNumber = txtCheckNumber.Text.Trim(); ;
                    if (txtCheckAmount.Text.Trim() != "") { oPaymentPatient.CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }

                    if (mskCheckDate.MaskCompleted)
                    {
                        mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oPaymentPatient.CheckDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
                    }

                    oPaymentPatient.MSTAccountID = _PatientID;
                    oPaymentPatient.MSTAccountType = EOBPaymentAccountType.Patient;
                    oPaymentPatient.ClinicID = _ClinicID;
                    oPaymentPatient.CreatedDateTime = DateTime.Now;
                    oPaymentPatient.ModifiedDateTime = DateTime.Now;

                    oPaymentPatient.PaymentTrayID = _CloseDayTrayID;
                    oPaymentPatient.PaymentTrayCode = _CloseDayTrayCode;
                    oPaymentPatient.PaymentTrayDesc = _CloseDayTrayName;

                    if (mskCloseDate.MaskCompleted == true)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oPaymentPatient.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }
                    oPaymentPatient.CardType = "";
                    oPaymentPatient.CardSecurityNo = "";
                    oPaymentPatient.CardID = 0;

                    oPaymentPatient.UserID = _UserId;
                    oPaymentPatient.UserName = _UserName;

                    //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  set value of PAF
                    oPaymentPatient.PAccountID = this.nPAccountID;
                    oPaymentPatient.AccountPatientID = this.nAccountPatientID;
                    oPaymentPatient.GuarantorID = this.nGuarantorID;
                    //End

                    #region "Payment Master Note"
                    //Notes if any to main payment to all claim OR main payment to reserve account
                    if (txtPayMstNotes.Text.Trim().Length > 0)
                    {
                        EOBPayment.Common.PaymentPatientLineNote oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

                        oPaymentPatientLineNote.ClaimNo = 0;
                        oPaymentPatientLineNote.EOBPaymentID = 0;
                        oPaymentPatientLineNote.EOBID = 0;
                        oPaymentPatientLineNote.EOBPaymentDetailID = 0;
                        oPaymentPatientLineNote.BillingTransactionID = 0;
                        oPaymentPatientLineNote.BillingTransactionDetailID = 0;
                        oPaymentPatientLineNote.Code = "";
                        oPaymentPatientLineNote.Description = txtPayMstNotes.Text.Trim();
                        if (txtCheckAmount.Text.Trim() != "") { oPaymentPatientLineNote.Amount = Convert.ToDecimal(txtCheckAmount.Text.Trim()); }
                        oPaymentPatientLineNote.IncludeOnPrint = chkPayMstIncludeNotes.Checked;
                        oPaymentPatientLineNote.ClinicID = _ClinicID;
                        oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientPayment;
                        oPaymentPatientLineNote.PaymentNoteSubType = EOBPaymentSubType.Other;
                        oPaymentPatientLineNote.HasData = true;

                        oPaymentPatient.Notes.Add(oPaymentPatientLineNote);
                        oPaymentPatientLineNote.Dispose();
                    }
                    #endregion

                    #endregion


                    #region "Credit Service Line Entry applicable to all claims, so it goes to master level not line level"

                    #region "Main Credit Line entry will be zero for reserve payment, when check and reserve in same entry then we will use this one, till that point its 0 payment entry"
                    //now its one time entry, but for partial payement implement like insurace payment
                    oEOBPatientPaymentCreditDetail.EOBPaymentID = _EOBPaymentID;
                    oEOBPatientPaymentCreditDetail.EOBID = 0;
                    oEOBPatientPaymentCreditDetail.EOBDtlID = 0;
                    oEOBPatientPaymentCreditDetail.EOBPaymentDetailID = 0;
                    oEOBPatientPaymentCreditDetail.RefEOBPaymentID = 0;
                    oEOBPatientPaymentCreditDetail.RefEOBPaymentDetailID = 0;
                    oEOBPatientPaymentCreditDetail.ReserveEOBPaymentID = 0;
                    oEOBPatientPaymentCreditDetail.ReserveEOBPaymentDetailID = 0;


                    oEOBPatientPaymentCreditDetail.BillingTransactionID = 0;
                    oEOBPatientPaymentCreditDetail.BillingTransactionDetailID = 0;
                    oEOBPatientPaymentCreditDetail.BillingTransactionLineNo = 0;
                    if (mskCloseDate.MaskCompleted == true)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oEOBPatientPaymentCreditDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                        oEOBPatientPaymentCreditDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }
                    oEOBPatientPaymentCreditDetail.CPTCode = "";
                    oEOBPatientPaymentCreditDetail.CPTDescription = "";

                    oEOBPatientPaymentCreditDetail.Amount = 0;
                    oEOBPatientPaymentCreditDetail.IsNullAmount = false;

                    oEOBPatientPaymentCreditDetail.PaymentType = EOBPaymentType.PatientPayment;
                    oEOBPatientPaymentCreditDetail.PaymentSubType = EOBPaymentSubType.Patient;
                    oEOBPatientPaymentCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                    oEOBPatientPaymentCreditDetail.PayMode = _EOBPaymentMode;

                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    oEOBPatientPaymentCreditDetail.AccountID = _PatientID;
                    oEOBPatientPaymentCreditDetail.AccountType = EOBPaymentAccountType.Patient;
                    oEOBPatientPaymentCreditDetail.MSTAccountID = _PatientID;
                    oEOBPatientPaymentCreditDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                    oEOBPatientPaymentCreditDetail.ContactInsID = 0;

                    oEOBPatientPaymentCreditDetail.PatientID = _PatientID;
                    //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  set value of PAF
                    oEOBPatientPaymentCreditDetail.PAccountID = this.nPAccountID;
                    oEOBPatientPaymentCreditDetail.AccountPatientID = this.nAccountPatientID;
                    oEOBPatientPaymentCreditDetail.GuarantorID = this.nGuarantorID;
                    //End
                    oEOBPatientPaymentCreditDetail.PaymentTrayID = _CloseDayTrayID;
                    oEOBPatientPaymentCreditDetail.PaymentTrayCode = _CloseDayTrayCode;
                    oEOBPatientPaymentCreditDetail.PaymentTrayDescription = _CloseDayTrayName;
                    oEOBPatientPaymentCreditDetail.UserID = _UserId;
                    oEOBPatientPaymentCreditDetail.UserName = _UserName;
                    oEOBPatientPaymentCreditDetail.ClinicID = _ClinicID;

                    oEOBPatientPaymentCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                    oEOBPatientPaymentCreditDetail.FinanceLieNo = 1;
                    oEOBPatientPaymentCreditDetail.MainCreditLineID = EOBPaymentCreditLineType.ReserveAsCreditLine.GetHashCode();
                    oEOBPatientPaymentCreditDetail.IsMainCreditLine = true;
                    oEOBPatientPaymentCreditDetail.IsReserveCreditLine = false;
                    oEOBPatientPaymentCreditDetail.IsCorrectionCreditLine = false;
                    oEOBPatientPaymentCreditDetail.RefFinanceLieNo = 0;
                    oEOBPatientPaymentCreditDetail.UseRefFinanceLieNo = false;

                    oPaymentPatient.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentCreditDetail);
                    
                    #endregion

                    #region "Reserve Credit Entries"

                    decimal _crPayAmt = 0; 
                    Int64 _crResPayID = 0; Int64 _crResPayDtlID = 0;
                    Int64 _crRefPayID = 0; Int64 _crRefPayDtlID = 0;
                    int _crResPayMode = 0;

                    gloGeneralItem.gloItems ocrItems = null;
                    if (btnUseReserve.Tag != null)
                    {
                        ocrItems = (gloGeneralItem.gloItems)btnUseReserve.Tag;
                    }
                    if (ocrItems == null)
                    {
                        ocrItems = new gloGeneralItem.gloItems();
                    }
                    for (int crPay = 0; crPay <= ocrItems.Count - 1; crPay++)
                    {
                        _crPayAmt = 0;_crResPayID = 0; _crResPayDtlID = 0;_crRefPayID = 0; _crRefPayDtlID = 0;
                        EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();

                        if (Convert.ToDecimal(ocrItems[crPay].Description) > 0)
                        {
                            _crPayAmt = Convert.ToDecimal(ocrItems[crPay].Description);
                            _crResPayID = Convert.ToInt64(ocrItems[crPay].ID);
                            _crResPayDtlID = Convert.ToInt64(ocrItems[crPay].Code);

                            if (ocrItems[crPay].SubItems != null && ocrItems[crPay].SubItems.Count > 0)
                            {
                                _crRefPayID = Convert.ToInt64(ocrItems[crPay].SubItems[0].ID);
                                _crRefPayDtlID = Convert.ToInt64(ocrItems[crPay].SubItems[0].Description);
                                _crResPayMode = Convert.ToInt32(ocrItems[crPay].SubItems[0].Code);
                            }

                            #region "Set Object"

                            oEOBPatientPaymentResAsCreditDetail.EOBPaymentID = _EOBPaymentID;
                            oEOBPatientPaymentResAsCreditDetail.EOBID = 0;
                            oEOBPatientPaymentResAsCreditDetail.EOBDtlID = 0;
                            oEOBPatientPaymentResAsCreditDetail.EOBPaymentDetailID = 0;
                            oEOBPatientPaymentResAsCreditDetail.RefEOBPaymentID = _crRefPayID;
                            oEOBPatientPaymentResAsCreditDetail.RefEOBPaymentDetailID = _crRefPayDtlID;
                            oEOBPatientPaymentResAsCreditDetail.ReserveEOBPaymentID = 0;
                            oEOBPatientPaymentResAsCreditDetail.ReserveEOBPaymentDetailID = 0;


                            oEOBPatientPaymentResAsCreditDetail.BillingTransactionID = 0;
                            oEOBPatientPaymentResAsCreditDetail.BillingTransactionDetailID = 0;
                            oEOBPatientPaymentResAsCreditDetail.BillingTransactionLineNo = 0;
                            if (mskCloseDate.MaskCompleted == true)
                            {
                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                oEOBPatientPaymentResAsCreditDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                oEOBPatientPaymentResAsCreditDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            }
                            oEOBPatientPaymentResAsCreditDetail.CPTCode = "";
                            oEOBPatientPaymentResAsCreditDetail.CPTDescription = "";
                            
                            oEOBPatientPaymentResAsCreditDetail.Amount = _crPayAmt;
                            oEOBPatientPaymentResAsCreditDetail.IsNullAmount = false;

                            oEOBPatientPaymentResAsCreditDetail.PaymentType = EOBPaymentType.PatientReserved;
                            oEOBPatientPaymentResAsCreditDetail.PaymentSubType = EOBPaymentSubType.Reserved;
                            oEOBPatientPaymentResAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                            oEOBPatientPaymentResAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                            oEOBPatientPaymentResAsCreditDetail.AccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode(); 
                            oEOBPatientPaymentResAsCreditDetail.AccountType = EOBPaymentAccountType.Reserved;
                            oEOBPatientPaymentResAsCreditDetail.MSTAccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode();
                            oEOBPatientPaymentResAsCreditDetail.MSTAccountType = EOBPaymentAccountType.Reserved;
                            oEOBPatientPaymentResAsCreditDetail.PatientID = _PatientID;

                            //Added by Subashish_b on 21/Feb/2011 for set value of PAF
                            oEOBPatientPaymentResAsCreditDetail.PAccountID = this.nPAccountID;
                            oEOBPatientPaymentResAsCreditDetail.AccountPatientID = this.nAccountPatientID;
                            oEOBPatientPaymentResAsCreditDetail.GuarantorID = this.nGuarantorID;
                            //End
                            oEOBPatientPaymentResAsCreditDetail.PaymentTrayID = _CloseDayTrayID;
                            oEOBPatientPaymentResAsCreditDetail.PaymentTrayCode = _CloseDayTrayCode;
                            oEOBPatientPaymentResAsCreditDetail.PaymentTrayDescription = _CloseDayTrayName;
                            oEOBPatientPaymentResAsCreditDetail.UserID = _UserId;
                            oEOBPatientPaymentResAsCreditDetail.UserName = _UserName;
                            oEOBPatientPaymentResAsCreditDetail.ClinicID = _ClinicID;

                            oEOBPatientPaymentResAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                            oEOBPatientPaymentResAsCreditDetail.FinanceLieNo = oPaymentPatient.EOBPatientPaymentLineDetails.Count + 1;
                            oEOBPatientPaymentResAsCreditDetail.MainCreditLineID = 0;
                            oEOBPatientPaymentResAsCreditDetail.IsMainCreditLine = false;
                            oEOBPatientPaymentResAsCreditDetail.IsReserveCreditLine = true;
                            oEOBPatientPaymentResAsCreditDetail.IsCorrectionCreditLine = false;
                            oEOBPatientPaymentResAsCreditDetail.RefFinanceLieNo = 0;
                            oEOBPatientPaymentResAsCreditDetail.UseRefFinanceLieNo = false;

                            oPaymentPatient.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentResAsCreditDetail);

                            #endregion

                        }
                        oEOBPatientPaymentResAsCreditDetail.Dispose();
                    }
                    ocrItems.Dispose();
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
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)) > 0)
                                       )
                                    {
                                        _hasDataToSave = true;
                                        break;
                                    }

                                }

                                if (_hasDataToSave == false) { continue; }

                                #endregion

                                oPaymentPatientClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();

                                oPaymentPatientClaim.ClaimNo = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_CLAIMNO)));
                                oPaymentPatientClaim.ClaimNoPrefix = "";
                                oPaymentPatientClaim.ClinicID = _ClinicID;
                                oPaymentPatientClaim.DisplayClaimNo = Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_CLAIMDISPNO));
                                oPaymentPatientClaim.PatientID = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_PATIENTID)));
                                oPaymentPatientClaim.PatientName = Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_PATIENTNAME));
                                oPaymentPatientClaim.BillingTransactionID = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_BILLING_TRANSACTON_ID)));


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
                                        && Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)) > 0)
                                       )
                                        {
                                            #region "EOB Service Lines"

                                            EOBPayment.Common.PaymentPatientLine oPaymentPatientLine = new global::gloBilling.EOBPayment.Common.PaymentPatientLine();

                                            #region "EOB Line"

                                            oPaymentPatientLine.PatientID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                            oPaymentPatientLine.PatInsuranceID = 0;// Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                            oPaymentPatientLine.InsContactID = 0;// Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                                            oPaymentPatientLine.BLTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            oPaymentPatientLine.BLTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            oPaymentPatientLine.BLTransactionLineNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                            oPaymentPatientLine.ClaimNumber = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));

                                            oPaymentPatientLine.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                            oPaymentPatientLine.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                            oPaymentPatientLine.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));

                                            oPaymentPatientLine.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                            oPaymentPatientLine.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));

                                            oPaymentPatientLine.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                            oPaymentPatientLine.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                            oPaymentPatientLine.BLInsuranceID = 0;
                                            oPaymentPatientLine.BLInsuranceName = "";
                                            oPaymentPatientLine.BLInsuranceFlag = InsuranceTypeFlag.None;

                                            if (c1SinglePayment.GetData(i, COL_CHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CHARGE)).Trim() != "")
                                            { oPaymentPatientLine.Charges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CHARGE)); oPaymentPatientLine.IsNullCharges = false; }

                                            if (c1SinglePayment.GetData(i, COL_UNIT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_UNIT)).Trim() != "")
                                            { oPaymentPatientLine.Unit = Convert.ToInt64(c1SinglePayment.GetData(i, COL_UNIT)); oPaymentPatientLine.IsNullUnit = false; }

                                            if (c1SinglePayment.GetData(i, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_TOTALCHARGE)).Trim() != "")
                                            { oPaymentPatientLine.TotalCharges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_TOTALCHARGE)); oPaymentPatientLine.IsNullTotalCharges = false; }

                                            oPaymentPatientLine.Allowed = 0;

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            { oPaymentPatientLine.WriteOff = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)); oPaymentPatientLine.IsNullWriteOff = false; }

                                            oPaymentPatientLine.NonCovered = 0;

                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                            { oPaymentPatientLine.InsuranceAmount = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)); oPaymentPatientLine.IsNullInsurance = false; }

                                            oPaymentPatientLine.Copay = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COPAY)); }
                                            oPaymentPatientLine.Deductible = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)); }
                                            oPaymentPatientLine.CoInsurance = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COINSURANCE)); }
                                            oPaymentPatientLine.Withhold = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WITHHOLD)); }

                                            oPaymentPatientLine.PaymentTrayID = _CloseDayTrayID;
                                            oPaymentPatientLine.PaymentTrayCode = _CloseDayTrayCode;
                                            oPaymentPatientLine.PaymentTrayDesc = _CloseDayTrayName;

                                            //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  set value of PAF
                                            oPaymentPatientLine.PAccountID = this.nPAccountID;
                                            oPaymentPatientLine.AccountPatientID = this.nAccountPatientID;
                                            oPaymentPatientLine.GuarantorID = this.nGuarantorID;
                                            //End

                                            oPaymentPatientLine.UserID = _UserId;
                                            oPaymentPatientLine.UserName = _UserName;
                                            oPaymentPatientLine.ClinicID = _ClinicID;

                                            oPaymentPatientLine.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                            oPaymentPatientLine.EOBType = EOBPaymentType.PatientPayment;

                                            //Adjestment Information
                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            {
                                                if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) > 0)
                                                {
                                                    EOBPayment.Common.PaymentPatientLineAdjustmentCode oPaymentPatientLineAdjustmentCode = new global::gloBilling.EOBPayment.Common.PaymentPatientLineAdjustmentCode();

                                                    oPaymentPatientLineAdjustmentCode.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                                    oPaymentPatientLineAdjustmentCode.EOBPaymentID = 0;
                                                    oPaymentPatientLineAdjustmentCode.EOBID = 0;
                                                    oPaymentPatientLineAdjustmentCode.EOBPaymentDetailID = 0;

                                                    oPaymentPatientLineAdjustmentCode.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    oPaymentPatientLineAdjustmentCode.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));


                                                    //if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                                    //{
                                                    //    string[] AdjCodeDesc = null;
                                                    //    string _adjstr = "";
                                                    //    _adjstr = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim();
                                                    //    AdjCodeDesc = _adjstr.Split('-');

                                                    //    oPaymentPatientLineAdjustmentCode.Code = AdjCodeDesc[0];
                                                    //    oPaymentPatientLineAdjustmentCode.Description = AdjCodeDesc[1];
                                                    //}

                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                                    { oPaymentPatientLineAdjustmentCode.Code = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim(); }
                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim() != "")
                                                    { oPaymentPatientLineAdjustmentCode.Description = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim(); }

                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    {
                                                        oPaymentPatientLineAdjustmentCode.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)));
                                                        oPaymentPatientLineAdjustmentCode.IsNullAmount = false; 
                                                    }
                                                    oPaymentPatientLineAdjustmentCode.ClinicID = _ClinicID;

                                                    oPaymentPatientLine.LineAdjestmentCodes.Add(oPaymentPatientLineAdjustmentCode);
                                                    oPaymentPatientLineAdjustmentCode.Dispose();
                                                }
                                            }
                                            #endregion

                                            #region "Debit Service Line - Patient"

                                            #region "Debit Service Line - patient "

                                            gloGeneralItem.gloItems oItems = null;
                                            if (btnUseReserve.Tag != null)
                                            {
                                                oItems = (gloGeneralItem.gloItems)btnUseReserve.Tag;
                                            }
                                            if (oItems == null)
                                            {
                                                oItems  = new gloGeneralItem.gloItems();
                                            }
                                            decimal _fillPayAmt = 0; decimal _fillAdjAmt = 0; decimal _fillResAmt = 0;
                                            Int64 _fillResPayID = 0; Int64 _fillResPayDtlID = 0;
                                            Int64 _fillRefPayID = 0; Int64 _fillRefPayDtlID = 0;
                                            int _fillrPayIndx = -1;
                                            int _fillRefFinanceLieNo = 0;
                                            bool _fillUseRefFinanceLieNo = false;
                                            bool _isNullfillPayAmt = true;
                                          //  bool _isNullfillAdjAmt = true;

                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                            { _fillPayAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT))); _isNullfillPayAmt = false; }

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)));
                                            //    _isNullfillAdjAmt = false; 
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
                                                        _fillUseRefFinanceLieNo = true;
                                                        _fillRefFinanceLieNo = rPay + 2;
                                                    }

                                                    _fillrPayIndx = rPay;
                                                    break;
                                                }
                                            }

                                            if (_fillPayAmt <= _fillResAmt)
                                            {
                                                #region "Set Less Amount Single object"

                                                oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                                oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                                oEOBPatientPaymentDetail.EOBID = 0;
                                                oEOBPatientPaymentDetail.EOBDtlID = 0;
                                                oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                                oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                                oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                                oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                                oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                                oEOBPatientPaymentDetail.Amount = _fillPayAmt;
                                                oEOBPatientPaymentDetail.IsNullAmount = _isNullfillPayAmt;

                                                oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                                oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Patient;
                                                oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                                oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                                oEOBPatientPaymentDetail.RefEOBPaymentID = _fillRefPayID;
                                                oEOBPatientPaymentDetail.RefEOBPaymentDetailID = _fillRefPayDtlID;
                                                oEOBPatientPaymentDetail.ReserveEOBPaymentID = _fillResPayID;
                                                oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = _fillResPayDtlID;

                                                //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                oEOBPatientPaymentDetail.AccountID = _PatientID;
                                                oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                                oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
                                                oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                                oEOBPatientPaymentDetail.PatientID = _PatientID;
                                                oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                                oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                                oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                                oEOBPatientPaymentDetail.UserID = _UserId;
                                                oEOBPatientPaymentDetail.UserName = _UserName;
                                                oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                                oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);


                                                oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                                oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                                oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                                oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                                oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                                oEOBPatientPaymentDetail.RefFinanceLieNo = _fillRefFinanceLieNo;
                                                oEOBPatientPaymentDetail.UseRefFinanceLieNo = _fillUseRefFinanceLieNo;

                                                // Newly added columns by pankaj
                                                oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                oEOBPatientPaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));

                                                oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
                                                oEOBPatientPaymentDetail.Dispose();

                                                if (_fillrPayIndx != -1)
                                                {
                                                    oItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
                                                    btnUseReserve.Tag = oItems;
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                #region "Set More Amount Multiple object"

                                                decimal _fillPayMulAmt = _fillPayAmt;
                                                do
                                                {
                                                    if (Convert.ToDecimal(oItems[_fillrPayIndx].Description) > 0)
                                                    {
                                                        _fillResAmt = Convert.ToDecimal(oItems[_fillrPayIndx].Description);
                                                        _fillResPayID = Convert.ToInt64(oItems[_fillrPayIndx].ID);
                                                        _fillResPayDtlID = Convert.ToInt64(oItems[_fillrPayIndx].Code);
                                                        _fillRefFinanceLieNo = 0;
                                                        _fillUseRefFinanceLieNo = false;

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
                                                            _fillUseRefFinanceLieNo = true;
                                                            _fillRefFinanceLieNo = _fillrPayIndx + 2;
                                                        }
                                                    }

                                                    #region "Set object"

                                                    oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                                    oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                                    oEOBPatientPaymentDetail.EOBID = 0;
                                                    oEOBPatientPaymentDetail.EOBDtlID = 0;
                                                    oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                                    oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                    oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                                    oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                                    oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                                    oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                    oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                                    oEOBPatientPaymentDetail.Amount = _fillPayAmt;
                                                    oEOBPatientPaymentDetail.IsNullAmount = _isNullfillPayAmt;

                                                    oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                                    oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Patient;
                                                    oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                                    oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                                    oEOBPatientPaymentDetail.RefEOBPaymentID = _fillRefPayID;
                                                    oEOBPatientPaymentDetail.RefEOBPaymentDetailID = _fillRefPayDtlID;
                                                    oEOBPatientPaymentDetail.ReserveEOBPaymentID = _fillResPayID;
                                                    oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = _fillResPayDtlID;

                                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                    oEOBPatientPaymentDetail.AccountID = _PatientID;
                                                    oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                                    oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
                                                    oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                                    oEOBPatientPaymentDetail.PatientID = _PatientID;
                                                    oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                                    oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                                    oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                                    oEOBPatientPaymentDetail.UserID = _UserId;
                                                    oEOBPatientPaymentDetail.UserName = _UserName;
                                                    oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                                    oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);


                                                    oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                                    oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                                    oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                                    oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                                    oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                                    oEOBPatientPaymentDetail.RefFinanceLieNo = _fillRefFinanceLieNo;
                                                    oEOBPatientPaymentDetail.UseRefFinanceLieNo = _fillUseRefFinanceLieNo;

                                                    // Newly added columns by pankaj
                                                    oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                    oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                    oEOBPatientPaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));

                                                    oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
                                                    oEOBPatientPaymentDetail.Dispose();

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

                                            #region "Debit Service Line - patient adjuestment if any"

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            {
                                                if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) > 0)
                                                {
                                                    oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                                    oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                                    oEOBPatientPaymentDetail.EOBID = 0;
                                                    oEOBPatientPaymentDetail.EOBDtlID = 0;
                                                    oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                                    oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                    oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                                    oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                                    oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                                    oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                    oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));


                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    { oEOBPatientPaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); oEOBPatientPaymentDetail.IsNullAmount = false; }


                                                    oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                                    oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Adjuestment;
                                                    oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                                    oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                                    oEOBPatientPaymentDetail.RefEOBPaymentID = 0;
                                                    oEOBPatientPaymentDetail.RefEOBPaymentDetailID = 0;
                                                    oEOBPatientPaymentDetail.ReserveEOBPaymentID = 0;
                                                    oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = 0;

                                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                    oEOBPatientPaymentDetail.AccountID = _PatientID;
                                                    oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                                    oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
                                                    oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                                    oEOBPatientPaymentDetail.PatientID = _PatientID;
                                                    oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                                    oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                                    oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                                    oEOBPatientPaymentDetail.UserID = _UserId;
                                                    oEOBPatientPaymentDetail.UserName = _UserName;
                                                    oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                                    oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);


                                                    oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                                    oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                                    oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                                    oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                                    oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                                    oEOBPatientPaymentDetail.RefFinanceLieNo = 0;
                                                    oEOBPatientPaymentDetail.UseRefFinanceLieNo = false;

                                                    // Newly added columns by pankaj
                                                    oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                    oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                    oEOBPatientPaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));


                                                    oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
                                                    oEOBPatientPaymentDetail.Dispose();
                                                }
                                            }
                                            #endregion

                                            #endregion

                                            oPaymentPatientClaim.CliamLines.Add(oPaymentPatientLine);

                                            oPaymentPatientLine.Dispose();

                                            if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); }

                                            #endregion
                                        }
                                    }
                                }

                                oPaymentPatient.PatientClaims.Add(oPaymentPatientClaim);
                                oPaymentPatientClaim.Dispose();
                            }
                        }
                    }

                    #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"
                    //if (btnReserveRemaining.Tag != null && btnReserveRemaining.Tag.ToString().Trim().Length > 0)
                    //{

                    //    //0 ReserveAmount 
                    //    //1 ReserveNote 
                    //    //2 ReserveSubType 
                    //    //3 ReserveNoteOnPrint 

                    //    string[] oList = null;
                    //    if (btnReserveRemaining.Tag != null)
                    //    {
                    //        oList = btnReserveRemaining.Tag.ToString().Split('~');
                    //    }



                    //    if (oList != null && oList.Length == 4)
                    //    {
                    //        oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                    //        oEOBPatientPaymentDetail.EOBPaymentID = 0;
                    //        oEOBPatientPaymentDetail.EOBID = 0;
                    //        oEOBPatientPaymentDetail.EOBDtlID = 0;
                    //        oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                    //        oEOBPatientPaymentDetail.BillingTransactionID = 0;
                    //        oEOBPatientPaymentDetail.BillingTransactionDetailID = 0;
                    //        oEOBPatientPaymentDetail.BillingTransactionLineNo = 0;
                    //        if (mskCloseDate.MaskCompleted == true)
                    //        {
                    //            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    //            oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    //            oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    //        }
                    //        oEOBPatientPaymentDetail.CPTCode = "";
                    //        oEOBPatientPaymentDetail.CPTDescription = "";

                    //        if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                    //        { oEOBPatientPaymentDetail.Amount = Convert.ToDecimal(oList[0]); }


                    //        oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientReserved;
                    //        oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Reserved;
                    //        oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                    //        oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                    //        oEOBPatientPaymentDetail.RefEOBPaymentID = oEOBPatientPaymentCreditDetail.RefEOBPaymentID;
                    //        oEOBPatientPaymentDetail.RefEOBPaymentDetailID = oEOBPatientPaymentCreditDetail.RefEOBPaymentDetailID;
                    //        oEOBPatientPaymentDetail.ReserveEOBPaymentID = oEOBPatientPaymentCreditDetail.ReserveEOBPaymentID;
                    //        oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = oEOBPatientPaymentCreditDetail.ReserveEOBPaymentDetailID;

                    //        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    //        oEOBPatientPaymentDetail.AccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode();
                    //        oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Reserved;
                    //        oEOBPatientPaymentDetail.MSTAccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode();
                    //        oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Reserved;
                    //        oEOBPatientPaymentDetail.PatientID = _PatientID;
                    //        oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                    //        oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                    //        oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                    //        oEOBPatientPaymentDetail.UserID = _UserId;
                    //        oEOBPatientPaymentDetail.UserName = _UserName;
                    //        oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                    //        //0 ReserveAmount 
                    //        //1 ReserveNote 
                    //        //2 ReserveSubType 
                    //        //3 ReserveNoteOnPrint 

                    //        if (oList[1] != null && Convert.ToString(oList[1]).Trim() != "")
                    //        {
                    //            EOBPayment.Common.PaymentPatientLineNote oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

                    //            oPaymentPatientLineNote.ClaimNo = 0;
                    //            oPaymentPatientLineNote.EOBPaymentID = 0;
                    //            oPaymentPatientLineNote.EOBID = 0;
                    //            oPaymentPatientLineNote.EOBPaymentDetailID = 0;
                    //            oPaymentPatientLineNote.BillingTransactionID = 0;
                    //            oPaymentPatientLineNote.BillingTransactionDetailID = 0;
                    //            oPaymentPatientLineNote.Code = "";
                    //            oPaymentPatientLineNote.Description = Convert.ToString(oList[1]).Trim();
                    //            oPaymentPatientLineNote.Amount = Convert.ToDecimal(oList[0]);
                    //            if (oList[3] != null && oList[3].ToString().Trim() != "")
                    //            {
                    //                oPaymentPatientLineNote.IncludeOnPrint = Convert.ToBoolean(oList[3]);
                    //            }
                    //            oPaymentPatientLineNote.ClinicID = _ClinicID;
                    //            oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientReserved;
                    //            if (oList[2] != null && oList[2].ToString().Trim() != "")
                    //            {
                    //                oPaymentPatientLineNote.PaymentNoteSubType = (EOBPaymentSubType)Convert.ToInt32(oList[2]);
                    //            }
                    //            oPaymentPatientLineNote.HasData = true;

                    //            oEOBPatientPaymentDetail.LineNotes.Add(oPaymentPatientLineNote);
                    //            oPaymentPatientLineNote.Dispose();
                    //        }

                    //        oPaymentPatient.EOBPatientPaymentReserveLineDetail.Add(oEOBPatientPaymentDetail);
                    //        oEOBPatientPaymentDetail.Dispose();
                    //    }
                    //}
                    #endregion
                    

                    _EOBPaymentID = ogloEOBPaymentPatient.SavePatientPayment(oPaymentPatient, false, out EOBPatientPaymentMasterLines);

                    FillEOBPayments(_EOBPaymentID);

                    _retPayId = _EOBPaymentID;
                    _EOBPaymentID = 0;
                    EOBPatientPaymentMasterLines.Clear();
                    btnReserveRemaining.Tag = null;

                    #region "Desing Grids"
                    _IsFormLoading = true;
                    rbPayType_Payment_CheckedChanged(null, null);
                    _IsFormLoading = false;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloEOBPaymentPatient != null) { ogloEOBPaymentPatient.Dispose(); };
                if (oPaymentPatient != null) { oPaymentPatient.Dispose(); };
                if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); };
                if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); };
                if (oEOBPatientPaymentCreditDetail != null) { oEOBPatientPaymentCreditDetail.Dispose(); };
            }
            return _retPayId;
        }

        private Int64 SavePatientCorrectionPayment()
        {
            Int64 _retPayId = 0;
            EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
            EOBPayment.Common.PaymentPatient oPaymentPatient = new global::gloBilling.EOBPayment.Common.PaymentPatient();
            EOBPayment.Common.PaymentPatientClaim oPaymentPatientClaim = null;
            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentDetail = null;
            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentCreditDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
            gloGeneralItem.gloItems oCrItems = new gloGeneralItem.gloItems();

            try
            {


                Int64 _CloseDayTrayID = 0;
                string _CloseDayTrayCode = "";
                string _CloseDayTrayName = "";
                EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;


                #region "Validation"

                #endregion

                #region "Payment Tray"
                    _CloseDayTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());
                    _CloseDayTrayCode = "";
                    _CloseDayTrayName = lblPaymentTray.Text;
                #endregion

                #region "Payment Mode"
                if (cmbPayMode.Text != "")
                {
                    if (cmbPayMode.Text.Trim() == EOBPaymentMode.None.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.None; }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.Cash; }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.Check; }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.EFT; }
                }
                #endregion

                #region " Master Data "

                oPaymentPatient.PaymentNumber = lblPaymetNo.Text.Trim().Split('#')[1];
                oPaymentPatient.PaymentNumberPefix = _paymentPrefix;
                oPaymentPatient.EOBPaymentID = _EOBPaymentID;

                //oPaymentPatient.EOBRefNO = txtEOBRefNumber.Text.Trim();
                if (_EOBPaymentMode == EOBPaymentMode.Cash)
                { oPaymentPatient.EOBRefNO = txtCheckNumber.Text.Trim(); }
                else
                { oPaymentPatient.EOBRefNO = ""; }

                oPaymentPatient.PayerName = lblPayer.Text;
                //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                oPaymentPatient.PayerID = _PatientID;
                oPaymentPatient.PayerType = EOBPaymentAccountType.Patient;
                oPaymentPatient.PaymentMode = _EOBPaymentMode;
                oPaymentPatient.CheckNumber = txtCheckNumber.Text.Trim(); ;
                if (txtCheckAmount.Text.Trim() != "") { oPaymentPatient.CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }

                if (mskCheckDate.MaskCompleted)
                {
                    mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    oPaymentPatient.CheckDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
                }

                oPaymentPatient.MSTAccountID = _PatientID;
                oPaymentPatient.MSTAccountType = EOBPaymentAccountType.Patient;
                oPaymentPatient.ClinicID = _ClinicID;
                oPaymentPatient.CreatedDateTime = DateTime.Now;
                oPaymentPatient.ModifiedDateTime = DateTime.Now;

                oPaymentPatient.PaymentTrayID = _CloseDayTrayID;
                oPaymentPatient.PaymentTrayCode = _CloseDayTrayCode;
                oPaymentPatient.PaymentTrayDesc = _CloseDayTrayName;

                if (mskCloseDate.MaskCompleted == true)
                {
                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    oPaymentPatient.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                }
                oPaymentPatient.CardType = "";
                oPaymentPatient.CardSecurityNo = "";
                oPaymentPatient.CardID = 0;

                oPaymentPatient.UserID = _UserId;
                oPaymentPatient.UserName = _UserName;

                //Added by Subashish_b on 21/Feb/2011 for set value of PAF
                oPaymentPatient.PAccountID = this.nPAccountID;
                oPaymentPatient.AccountPatientID = this.nAccountPatientID;
                oPaymentPatient.GuarantorID = this.nGuarantorID;
                //End

                #region "Payment Master Note"
                //Notes if any to main payment to all claim OR main payment to reserve account
                if (txtPayMstNotes.Text.Trim().Length > 0)
                {
                    EOBPayment.Common.PaymentPatientLineNote oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

                    oPaymentPatientLineNote.ClaimNo = 0;
                    oPaymentPatientLineNote.EOBPaymentID = 0;
                    oPaymentPatientLineNote.EOBID = 0;
                    oPaymentPatientLineNote.EOBPaymentDetailID = 0;
                    oPaymentPatientLineNote.BillingTransactionID = 0;
                    oPaymentPatientLineNote.BillingTransactionDetailID = 0;
                    oPaymentPatientLineNote.Code = "";
                    oPaymentPatientLineNote.Description = txtPayMstNotes.Text.Trim();
                    if (txtCheckAmount.Text.Trim() != "") { oPaymentPatientLineNote.Amount = Convert.ToDecimal(txtCheckAmount.Text.Trim()); }
                    oPaymentPatientLineNote.IncludeOnPrint = chkPayMstIncludeNotes.Checked;
                    oPaymentPatientLineNote.ClinicID = _ClinicID;
                    oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientPayment;
                    oPaymentPatientLineNote.PaymentNoteSubType = EOBPaymentSubType.Other;
                    oPaymentPatientLineNote.HasData = true;

                    oPaymentPatient.Notes.Add(oPaymentPatientLineNote);
                    oPaymentPatientLineNote.Dispose();
                }
                #endregion

                #endregion

                #region "Credit Service Line Entry applicable to all claims, so it goes to master level not line level"

                #region "Main Credit Line entry will be zero for reserve payment, when check and reserve in same entry then we will use this one, till that point its 0 payment entry"

                //now its one time entry, but for partial payement implement like insurace payment
                oEOBPatientPaymentCreditDetail.EOBPaymentID = _EOBPaymentID;
                oEOBPatientPaymentCreditDetail.EOBID = 0;
                oEOBPatientPaymentCreditDetail.EOBDtlID = 0;
                oEOBPatientPaymentCreditDetail.EOBPaymentDetailID = 0;
                oEOBPatientPaymentCreditDetail.RefEOBPaymentID = 0;
                oEOBPatientPaymentCreditDetail.RefEOBPaymentDetailID = 0;
                oEOBPatientPaymentCreditDetail.ReserveEOBPaymentID = 0;
                oEOBPatientPaymentCreditDetail.ReserveEOBPaymentDetailID = 0;


                oEOBPatientPaymentCreditDetail.BillingTransactionID = 0;
                oEOBPatientPaymentCreditDetail.BillingTransactionDetailID = 0;
                oEOBPatientPaymentCreditDetail.BillingTransactionLineNo = 0;
                if (mskCloseDate.MaskCompleted == true)
                {
                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    oEOBPatientPaymentCreditDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    oEOBPatientPaymentCreditDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                }
                oEOBPatientPaymentCreditDetail.CPTCode = "";
                oEOBPatientPaymentCreditDetail.CPTDescription = "";

                oEOBPatientPaymentCreditDetail.Amount = 0;
                oEOBPatientPaymentCreditDetail.IsNullAmount = false;

                oEOBPatientPaymentCreditDetail.PaymentType = EOBPaymentType.PatientPayment;
                oEOBPatientPaymentCreditDetail.PaymentSubType = EOBPaymentSubType.Patient;
                oEOBPatientPaymentCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                oEOBPatientPaymentCreditDetail.PayMode = _EOBPaymentMode;

                //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                oEOBPatientPaymentCreditDetail.AccountID = _PatientID;
                //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  set value of PAF
                oEOBPatientPaymentCreditDetail.PAccountID = this.nPAccountID;
                oEOBPatientPaymentCreditDetail.AccountPatientID = this.nAccountPatientID;
                oEOBPatientPaymentCreditDetail.GuarantorID = this.nGuarantorID;
                //End
                oEOBPatientPaymentCreditDetail.AccountType = EOBPaymentAccountType.Patient;
                oEOBPatientPaymentCreditDetail.MSTAccountID = _PatientID;
                oEOBPatientPaymentCreditDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                oEOBPatientPaymentCreditDetail.ContactInsID = 0;

                oEOBPatientPaymentCreditDetail.PatientID = _PatientID;
                oEOBPatientPaymentCreditDetail.PaymentTrayID = _CloseDayTrayID;
                oEOBPatientPaymentCreditDetail.PaymentTrayCode = _CloseDayTrayCode;
                oEOBPatientPaymentCreditDetail.PaymentTrayDescription = _CloseDayTrayName;
                oEOBPatientPaymentCreditDetail.UserID = _UserId;
                oEOBPatientPaymentCreditDetail.UserName = _UserName;
                oEOBPatientPaymentCreditDetail.ClinicID = _ClinicID;

                oEOBPatientPaymentCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                oEOBPatientPaymentCreditDetail.FinanceLieNo = 1;
                oEOBPatientPaymentCreditDetail.MainCreditLineID = EOBPaymentCreditLineType.CorrectionAsCreditLine.GetHashCode(); 
                oEOBPatientPaymentCreditDetail.IsMainCreditLine = true;
                oEOBPatientPaymentCreditDetail.IsReserveCreditLine = false;
                oEOBPatientPaymentCreditDetail.IsCorrectionCreditLine = false;
                oEOBPatientPaymentCreditDetail.RefFinanceLieNo = 0;
                oEOBPatientPaymentCreditDetail.UseRefFinanceLieNo = false;

                oPaymentPatient.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentCreditDetail);

                #endregion

                #region "Negative Amount Credit Entries - Fetch from database and set to object"

                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
                {
                    for (int nCrIndex = 1; nCrIndex <= c1SinglePayment.Rows.Count - 1; nCrIndex++)
                    {
                        if (c1SinglePayment.GetData(nCrIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(nCrIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            if (c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT) != null && c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT).ToString().Trim() != "")
                            {
                                if (Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT)) < 0)
                                {
                                    decimal _crPayAmt = Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT)) - (Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT)) * 2);
                                    int _crResPayMode = 0;

                                    Int64 _crBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                    Int64 _crBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
                                    Int64 _crBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));
                                    Int64 _crPatientId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_PATIENTID));
                                    string _crCptCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE)).Trim();

                                    //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  assigning Patient Account value
                                    Int64 _crPAccountID = this.nPAccountID;
                                    //End

                                    gloDatabaseLayer.DBLayer _nCrDBLayer = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                    gloDatabaseLayer.DBParameters _nCrDBParameters = new gloDatabaseLayer.DBParameters();
                                    DataTable _nCrDataTable = new DataTable();
                                    decimal _checkSumCorrection = 0;

                                    _nCrDBParameters.Add("@CorrectionAmount", _crPayAmt, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                    _nCrDBParameters.Add("@nPatientID", _crPatientId, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
                                    _nCrDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
                                    _nCrDBParameters.Add("@nBillingTransactionID", _crBillTrnId, ParameterDirection.Input, SqlDbType.BigInt);//   numeric(18,0),
                                    _nCrDBParameters.Add("@nBillingTransactionDetailID", _crBillTrnDtlId, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0)
                                    _nCrDBLayer.Connect(false);
                                    _nCrDBLayer.Retrive("BL_SELECT_EOBCorrectionAmountList", _nCrDBParameters, out _nCrDataTable);
                                    _nCrDBLayer.Disconnect();
                                    _nCrDBLayer.Dispose();

                                    if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
                                    {
                                        _checkSumCorrection = 0;

                                        for (int nCrDTIndex = 0; nCrDTIndex <= _nCrDataTable.Rows.Count - 1; nCrDTIndex++)
                                        {
                                            _checkSumCorrection += Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);

                                            //Store Cr Amt list for apply to debit lines
                                            gloGeneralItem.gloItem ogloItem = new gloGeneralItem.gloItem(Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]), Convert.ToString(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]), Convert.ToString(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]));
                                            ogloItem.SubItems.Add(Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]), Convert.ToString(_nCrDataTable.Rows[nCrDTIndex]["nPayMode"]), Convert.ToString(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]));
                                            oCrItems.Add(ogloItem);
                                           // ogloItem.Dispose(); //SLR: it should not be since subitems will be disposed.

                                            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                            
                                            #region "Set Object"

                                            oEOBPatientPaymentResAsCreditDetail.EOBPaymentID = _EOBPaymentID;
                                            oEOBPatientPaymentResAsCreditDetail.EOBID = 0;
                                            oEOBPatientPaymentResAsCreditDetail.EOBDtlID = 0;
                                            oEOBPatientPaymentResAsCreditDetail.EOBPaymentDetailID = 0;

                                            oEOBPatientPaymentResAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                            oEOBPatientPaymentResAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                            oEOBPatientPaymentResAsCreditDetail.ReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                            oEOBPatientPaymentResAsCreditDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                            oEOBPatientPaymentResAsCreditDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                            oEOBPatientPaymentResAsCreditDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
                                            oEOBPatientPaymentResAsCreditDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));

                                            oEOBPatientPaymentResAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                            oEOBPatientPaymentResAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                            oEOBPatientPaymentResAsCreditDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE));
                                            oEOBPatientPaymentResAsCreditDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_DESCRIPTON));

                                            oEOBPatientPaymentResAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) - (Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) * 2);
                                            oEOBPatientPaymentResAsCreditDetail.IsNullAmount = false;

                                            oEOBPatientPaymentResAsCreditDetail.PaymentType = EOBPaymentType.PatientPayment;
                                            oEOBPatientPaymentResAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                            oEOBPatientPaymentResAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                            oEOBPatientPaymentResAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                            oEOBPatientPaymentResAsCreditDetail.AccountID = _PatientID;
                                            oEOBPatientPaymentResAsCreditDetail.AccountType = EOBPaymentAccountType.Patient;
                                            oEOBPatientPaymentResAsCreditDetail.MSTAccountID = _PatientID;
                                            oEOBPatientPaymentResAsCreditDetail.MSTAccountType = EOBPaymentAccountType.Patient;

                                            //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  payment correction and value assigning
                                            if (_IsPatientAccountFeature)
                                            {
                                                oEOBPatientPaymentResAsCreditDetail.PatientID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_PATIENTID));

                                                //oEOBPatientPaymentResAsCreditDetail.PatientID = _PatientID;
                                            }
                                            else
                                            {
                                                oEOBPatientPaymentResAsCreditDetail.PatientID = _PatientID;
                                            }
                                            oEOBPatientPaymentResAsCreditDetail.PAccountID = this.nPAccountID;
                                            oEOBPatientPaymentResAsCreditDetail.AccountPatientID = this.nAccountPatientID;
                                            oEOBPatientPaymentResAsCreditDetail.GuarantorID = this.nGuarantorID;

                                            //End

                                            //oEOBPatientPaymentResAsCreditDetail.PatientID = _PatientID;
                                            oEOBPatientPaymentResAsCreditDetail.PaymentTrayID = _CloseDayTrayID;
                                            oEOBPatientPaymentResAsCreditDetail.PaymentTrayCode = _CloseDayTrayCode;
                                            oEOBPatientPaymentResAsCreditDetail.PaymentTrayDescription = _CloseDayTrayName;
                                            oEOBPatientPaymentResAsCreditDetail.UserID = _UserId;
                                            oEOBPatientPaymentResAsCreditDetail.UserName = _UserName;
                                            oEOBPatientPaymentResAsCreditDetail.ClinicID = _ClinicID;

                                            oEOBPatientPaymentResAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                            oEOBPatientPaymentResAsCreditDetail.FinanceLieNo = oPaymentPatient.EOBPatientPaymentLineDetails.Count+1;
                                            oEOBPatientPaymentResAsCreditDetail.MainCreditLineID = 0;
                                            oEOBPatientPaymentResAsCreditDetail.IsMainCreditLine = false;
                                            oEOBPatientPaymentResAsCreditDetail.IsReserveCreditLine = false;
                                            oEOBPatientPaymentResAsCreditDetail.IsCorrectionCreditLine = true;
                                            oEOBPatientPaymentResAsCreditDetail.RefFinanceLieNo = 0;
                                            oEOBPatientPaymentResAsCreditDetail.UseRefFinanceLieNo = false;

                                            // newly added column by pankaj
                                            oEOBPatientPaymentResAsCreditDetail.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_TRN_ID));
                                            oEOBPatientPaymentResAsCreditDetail.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_TRN_DTL_ID));
                                            oEOBPatientPaymentResAsCreditDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_SUB_CLAIM_NO));

                                            oPaymentPatient.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentResAsCreditDetail);

                                            #endregion

                                            oEOBPatientPaymentResAsCreditDetail.Dispose();
                                        }

                                        //..Code added on 20100701 by Sagar Ghodke
                                        //..Code added to check the correction list sum amount is equal to the actual correction amount sent
                                        //..If not equal abort save else continue.
                                        if (_crPayAmt != _checkSumCorrection)
                                        {
                                            string _message = "Invalid Patient Payment correction list retrival for PatientID : "+_crPatientId+", Amount : "+ _crPayAmt +",BillingTrnID : "+_crBillTrnId+",BillingTrnDtlID : "+_crBillTrnDtlId+" ";
                                            MessageBox.Show("ERROR : Invalid correction amount.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Payment, gloAuditTrail.ActivityType.Add, _message, _crPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure);
                                            return 0;
                                        }
                                        //..End code add on 20100701 Sagar Ghodke
                                    }
                                    _nCrDBLayer.Dispose();
                                }
                            }
                        }
                    }
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
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)) < 0)
                                       )
                                    {
                                        _hasDataToSave = true;
                                        break;
                                    }

                                }

                                if (_hasDataToSave == false) { continue; }

                                #endregion

                                oPaymentPatientClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();

                                oPaymentPatientClaim.ClaimNo = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_CLAIMNO)));
                                oPaymentPatientClaim.ClaimNoPrefix = "";
                                oPaymentPatientClaim.ClinicID = _ClinicID;
                                oPaymentPatientClaim.DisplayClaimNo = Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_CLAIMDISPNO));
                                oPaymentPatientClaim.PatientID = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_PATIENTID)));
                                oPaymentPatientClaim.PatientName = Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_PATIENTNAME));
                                oPaymentPatientClaim.BillingTransactionID = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_BILLING_TRANSACTON_ID)));


                                for (int i = _claimStartIndex; i <= _claimEndIndex; i++)
                                {
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

                                            EOBPayment.Common.PaymentPatientLine oPaymentPatientLine = new global::gloBilling.EOBPayment.Common.PaymentPatientLine();

                                            #region "EOB Line"

                                            oPaymentPatientLine.PatientID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                            oPaymentPatientLine.PatInsuranceID = 0;// Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                            oPaymentPatientLine.InsContactID = 0;// Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                                            oPaymentPatientLine.BLTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            oPaymentPatientLine.BLTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            oPaymentPatientLine.BLTransactionLineNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                            oPaymentPatientLine.ClaimNumber = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));

                                            oPaymentPatientLine.TrackTrnID  = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                            oPaymentPatientLine.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                            oPaymentPatientLine.SubClaimNo  = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));

                                            oPaymentPatientLine.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                            oPaymentPatientLine.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));

                                            oPaymentPatientLine.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                            oPaymentPatientLine.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                            oPaymentPatientLine.BLInsuranceID = 0;
                                            oPaymentPatientLine.BLInsuranceName = "";
                                            oPaymentPatientLine.BLInsuranceFlag = InsuranceTypeFlag.None;

                                            if (c1SinglePayment.GetData(i, COL_CHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CHARGE)).Trim() != "")
                                            { oPaymentPatientLine.Charges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CHARGE)); oPaymentPatientLine.IsNullCharges = false; }

                                            if (c1SinglePayment.GetData(i, COL_UNIT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_UNIT)).Trim() != "")
                                            { oPaymentPatientLine.Unit = Convert.ToInt64(c1SinglePayment.GetData(i, COL_UNIT)); oPaymentPatientLine.IsNullUnit = false; }

                                            if (c1SinglePayment.GetData(i, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_TOTALCHARGE)).Trim() != "")
                                            { oPaymentPatientLine.TotalCharges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_TOTALCHARGE)); oPaymentPatientLine.IsNullTotalCharges = false; }

                                            oPaymentPatientLine.Allowed = 0;

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            { oPaymentPatientLine.WriteOff = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)); oPaymentPatientLine.IsNullWriteOff = false; }

                                            oPaymentPatientLine.NonCovered = 0;
                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                            { oPaymentPatientLine.InsuranceAmount = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)); oPaymentPatientLine.IsNullInsurance = false; }

                                            oPaymentPatientLine.Copay = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COPAY)); }
                                            oPaymentPatientLine.Deductible = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)); }
                                            oPaymentPatientLine.CoInsurance = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COINSURANCE)); }
                                            oPaymentPatientLine.Withhold = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WITHHOLD)); }

                                            oPaymentPatientLine.PaymentTrayID = _CloseDayTrayID;
                                            oPaymentPatientLine.PaymentTrayCode = _CloseDayTrayCode;
                                            oPaymentPatientLine.PaymentTrayDesc = _CloseDayTrayName;


                                            oPaymentPatientLine.UserID = _UserId;
                                            oPaymentPatientLine.UserName = _UserName;
                                            oPaymentPatientLine.ClinicID = _ClinicID;

                                            oPaymentPatientLine.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                            oPaymentPatientLine.EOBType = EOBPaymentType.PatientPayment;

                                            //Adjestment Information
                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            {
                                                if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) < 0)
                                                {
                                                    EOBPayment.Common.PaymentPatientLineAdjustmentCode oPaymentPatientLineAdjustmentCode = new global::gloBilling.EOBPayment.Common.PaymentPatientLineAdjustmentCode();

                                                    oPaymentPatientLineAdjustmentCode.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                                    oPaymentPatientLineAdjustmentCode.EOBPaymentID = 0;
                                                    oPaymentPatientLineAdjustmentCode.EOBID = 0;
                                                    oPaymentPatientLineAdjustmentCode.EOBPaymentDetailID = 0;

                                                    oPaymentPatientLineAdjustmentCode.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    oPaymentPatientLineAdjustmentCode.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));


                                                    //if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                                    //{
                                                    //    string[] AdjCodeDesc = null;
                                                    //    string _adjstr = "";
                                                    //    _adjstr = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim();
                                                    //    AdjCodeDesc = _adjstr.Split('-');

                                                    //    oPaymentPatientLineAdjustmentCode.Code = AdjCodeDesc[0];
                                                    //    oPaymentPatientLineAdjustmentCode.Description = AdjCodeDesc[1];
                                                    //}

                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                                    { oPaymentPatientLineAdjustmentCode.Code = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim(); }
                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim() != "")
                                                    { oPaymentPatientLineAdjustmentCode.Description = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim(); }


                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    {
                                                        oPaymentPatientLineAdjustmentCode.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)));
                                                        oPaymentPatientLineAdjustmentCode.IsNullAmount = false;
                                                    }
                                                    oPaymentPatientLineAdjustmentCode.ClinicID = _ClinicID;

                                                    oPaymentPatientLine.LineAdjestmentCodes.Add(oPaymentPatientLineAdjustmentCode);
                                                    oPaymentPatientLineAdjustmentCode.Dispose();
                                                }
                                            }

                                            #endregion

                                            #region "Debit Service Line - Patient"
                                            if (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)) > 0)
                                            {
                                                #region "Debit Service Line - patient "

                                                decimal _fillPayAmt = 0; decimal _fillAdjAmt = 0; decimal _fillResAmt = 0;
                                                Int64 _fillResPayID = 0; Int64 _fillResPayDtlID = 0;
                                                Int64 _fillRefPayID = 0; Int64 _fillRefPayDtlID = 0;
                                                int _fillrPayIndx = -1;
                                                int _fillRefFinanceLieNo = 0;
                                                bool _fillUseRefFinanceLieNo = false;
                                                bool _isNullfillPayAmt = false;
                                            //    bool _isNullfillAdjAmt = false;

                                                if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                                { _fillPayAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT))); _isNullfillPayAmt = false; }

                                                //Commented on 20100729 by Sagar Ghodke
                                                //
                                                //_fillPayAmt = _fillPayAmt - (_fillPayAmt * 2);

                                                if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); 
                                                //    _isNullfillAdjAmt = false; 
                                                }

                                                _fillAdjAmt = _fillAdjAmt - (_fillAdjAmt * 2);

                                                for (int rPay = 0; rPay <= oCrItems.Count - 1; rPay++)
                                                {
                                                    if (Convert.ToDecimal(oCrItems[rPay].Description) > 0)
                                                    {
                                                        _fillResAmt = Convert.ToDecimal(oCrItems[rPay].Description);
                                                        _fillResPayID = Convert.ToInt64(oCrItems[rPay].ID);
                                                        _fillResPayDtlID = Convert.ToInt64(oCrItems[rPay].Code);

                                                        if (oCrItems[rPay].SubItems != null && oCrItems[rPay].SubItems.Count > 0)
                                                        {
                                                            _fillRefPayID = Convert.ToInt64(oCrItems[rPay].SubItems[0].ID);
                                                            _fillRefPayDtlID = Convert.ToInt64(oCrItems[rPay].SubItems[0].Description);
                                                        }

                                                        //This logic is temporary depend upon the gloItems
                                                        //when we implement partial payment it should be depend upon the "EOBInsurancePaymentMasterAllocationLines" this object
                                                        if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
                                                        {
                                                            _fillUseRefFinanceLieNo = true;
                                                            _fillRefFinanceLieNo = rPay + 2;
                                                        }
                                                        
                                                        _fillrPayIndx = rPay;
                                                        break;
                                                    }
                                                }
                                               
                                                if (_fillPayAmt <= _fillResAmt)
                                                {
                                                    #region "Set Less Amount Single object"
                                                    oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                                    oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                                    oEOBPatientPaymentDetail.EOBID = 0;
                                                    oEOBPatientPaymentDetail.EOBDtlID = 0;
                                                    oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                                    oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                    oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                                    oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                                    oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                                    oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                    oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                                    if (_fillPayAmt < 0)
                                                    { oEOBPatientPaymentDetail.Amount = _fillPayAmt - (_fillPayAmt * 2); }
                                                    else { oEOBPatientPaymentDetail.Amount = _fillPayAmt; }

                                                    oEOBPatientPaymentDetail.IsNullAmount = _isNullfillPayAmt;

                                                    oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                                    oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Patient;
                                                    oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                                    oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                                    oEOBPatientPaymentDetail.RefEOBPaymentID = _fillRefPayID;
                                                    oEOBPatientPaymentDetail.RefEOBPaymentDetailID = _fillRefPayDtlID;
                                                    oEOBPatientPaymentDetail.ReserveEOBPaymentID = _fillResPayID;
                                                    oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = _fillResPayDtlID;

                                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                    oEOBPatientPaymentDetail.AccountID = _PatientID;
                                                    oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                                    oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
                                                    oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                                    oEOBPatientPaymentDetail.PatientID = _PatientID;
                                                    oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                                    oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                                    oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                                    oEOBPatientPaymentDetail.UserID = _UserId;
                                                    oEOBPatientPaymentDetail.UserName = _UserName;
                                                    oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                                    oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                                    oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                                    oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                                    oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                                    oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                                    oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                                    oEOBPatientPaymentDetail.RefFinanceLieNo = _fillRefFinanceLieNo;
                                                    oEOBPatientPaymentDetail.UseRefFinanceLieNo = _fillUseRefFinanceLieNo;

                                                    // Newly added columns by Pankaj
                                                    oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                    oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                    oEOBPatientPaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));

                                                    //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  set value of PAF
                                                    oEOBPatientPaymentDetail.PAccountID = this.nPAccountID;
                                                    oEOBPatientPaymentDetail.AccountPatientID = this.nAccountPatientID;
                                                    oEOBPatientPaymentDetail.GuarantorID = this.nGuarantorID;
                                                    //End


                                                    oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
                                                    oEOBPatientPaymentDetail.Dispose();

                                                    if (_fillrPayIndx != -1)
                                                    {
                                                        //...Code added on 20100507 by Sagar Ghodke
                                                        if (_fillPayAmt < 0) { _fillPayAmt = _fillPayAmt - (_fillPayAmt * 2); }
                                                        //...End code added on 20100507 by Sagar Ghodke

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
                                                            _fillResPayID = Convert.ToInt64(oCrItems[_fillrPayIndx].ID);
                                                            _fillResPayDtlID = Convert.ToInt64(oCrItems[_fillrPayIndx].Code);
                                                            _fillRefFinanceLieNo = 0;
                                                            _fillUseRefFinanceLieNo = false;
                                                            _isNullfillPayAmt = false;

                                                            if (oCrItems[_fillrPayIndx].SubItems != null && oCrItems[_fillrPayIndx].SubItems.Count > 0)
                                                            {
                                                                _fillRefPayID = Convert.ToInt64(oCrItems[_fillrPayIndx].SubItems[0].ID);
                                                                _fillRefPayDtlID = Convert.ToInt64(oCrItems[_fillrPayIndx].SubItems[0].Description);
                                                            }

                                                            if (_fillPayMulAmt >= _fillResAmt)
                                                            { _fillPayAmt = _fillResAmt; _fillPayMulAmt = _fillPayMulAmt - _fillPayAmt; }
                                                            else
                                                            { _fillPayAmt = _fillPayMulAmt; _fillPayMulAmt = _fillPayMulAmt - _fillPayMulAmt; }

                                                            //This logic is temporary depend upon the gloItems
                                                            //when we implement partial payment it should be depend upon the "EOBInsurancePaymentMasterAllocationLines" this object
                                                            if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
                                                            {
                                                                _fillUseRefFinanceLieNo = true;
                                                                _fillRefFinanceLieNo = _fillrPayIndx + 2;
                                                            }
                                                        }

                                                        #region "Set object"

                                                        oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                                        oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                                        oEOBPatientPaymentDetail.EOBID = 0;
                                                        oEOBPatientPaymentDetail.EOBDtlID = 0;
                                                        oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                                        oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                        oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                        oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                                        oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                                        oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                                        oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                        oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                                        if (_fillPayAmt < 0)
                                                        { oEOBPatientPaymentDetail.Amount = _fillPayAmt - (_fillPayAmt * 2); }
                                                        else
                                                        { oEOBPatientPaymentDetail.Amount = _fillPayAmt; }
                                                        oEOBPatientPaymentDetail.IsNullAmount = _isNullfillPayAmt;

                                                        oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                                        oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Patient;
                                                        oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                                        oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                                        oEOBPatientPaymentDetail.RefEOBPaymentID = _fillRefPayID;
                                                        oEOBPatientPaymentDetail.RefEOBPaymentDetailID = _fillRefPayDtlID;
                                                        oEOBPatientPaymentDetail.ReserveEOBPaymentID = _fillResPayID;
                                                        oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = _fillResPayDtlID;

                                                        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                        oEOBPatientPaymentDetail.AccountID = _PatientID;
                                                        oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                                        oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
                                                        oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                                        oEOBPatientPaymentDetail.PatientID = _PatientID;
                                                        oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                                        oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                                        oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                                        oEOBPatientPaymentDetail.UserID = _UserId;
                                                        oEOBPatientPaymentDetail.UserName = _UserName;
                                                        oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                                        oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);


                                                        oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                                        oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                                        oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                                        oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                                        oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                                        oEOBPatientPaymentDetail.RefFinanceLieNo = _fillRefFinanceLieNo;
                                                        oEOBPatientPaymentDetail.UseRefFinanceLieNo = _fillUseRefFinanceLieNo;

                                                        // Newly added columns by Pankaj
                                                        oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                        oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                        oEOBPatientPaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));

                                                        //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  assigning value of (PAF)patient account
                                                        oEOBPatientPaymentDetail.PAccountID = this.nPAccountID;
                                                        oEOBPatientPaymentDetail.AccountPatientID = this.nAccountPatientID;
                                                        oEOBPatientPaymentDetail.GuarantorID = this.nGuarantorID;
                                                        //End
                                                        
                                                        oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
                                                        oEOBPatientPaymentDetail.Dispose();

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

                                            if (_IsAdjustmentMode == true)
                                            {
                                                #region "Debit Service Line - patient adjuestment if any"

                                                if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                {
                                                    if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) < 0)
                                                    {
                                                        oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                                        oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                                        oEOBPatientPaymentDetail.EOBID = 0;
                                                        oEOBPatientPaymentDetail.EOBDtlID = 0;
                                                        oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                                        oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                        oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                        oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
                                                        oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                                        oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                                        oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                        oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                                        if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                        { oEOBPatientPaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); oEOBPatientPaymentDetail.IsNullAmount = false; }


                                                        oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                                        oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Adjuestment;
                                                        oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                                        oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                                        oEOBPatientPaymentDetail.RefEOBPaymentID = 0;
                                                        oEOBPatientPaymentDetail.RefEOBPaymentDetailID = 0;
                                                        oEOBPatientPaymentDetail.ReserveEOBPaymentID = 0;
                                                        oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = 0;

                                                        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                        oEOBPatientPaymentDetail.AccountID = _PatientID;
                                                        oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                                        oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
                                                        oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                                        oEOBPatientPaymentDetail.PatientID = _PatientID;
                                                        oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                                        oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                                        oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                                        oEOBPatientPaymentDetail.UserID = _UserId;
                                                        oEOBPatientPaymentDetail.UserName = _UserName;
                                                        oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                                        oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text); 

                                                        oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                                        oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                                        oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                                        oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                                        oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                                        oEOBPatientPaymentDetail.RefFinanceLieNo = 0;
                                                        oEOBPatientPaymentDetail.UseRefFinanceLieNo = false;

                                                        // Newly added columns by Pankaj
                                                        oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                        oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                        oEOBPatientPaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));
                                                        
                                                        //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  assigning value of (PAF)patient account
                                                        oEOBPatientPaymentDetail.PAccountID = this.nPAccountID;
                                                        oEOBPatientPaymentDetail.AccountPatientID = this.nAccountPatientID;
                                                        oEOBPatientPaymentDetail.GuarantorID = this.nGuarantorID;
                                                        //End


                                                        oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
                                                        oEOBPatientPaymentDetail.Dispose();
                                                    }
                                                }
                                                #endregion
                                            }
                                            //}
                                            #endregion

                                            oPaymentPatientClaim.CliamLines.Add(oPaymentPatientLine);

                                            oPaymentPatientLine.Dispose();

                                            if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); }

                                            #endregion
                                        }
                                    }
                                }

                                oPaymentPatient.PatientClaims.Add(oPaymentPatientClaim);
                                oPaymentPatientClaim.Dispose();
                            }
                        }
                    }

                    #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"
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
                        if (oList != null && oList.Length == 6)
                        {
                            if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                            {
                                if (Convert.ToDecimal(Convert.ToString(oList[0]).Trim()) > 0)
                                {
                                    #region "Put Amount into reserve, but according to remaining amount which is collected from negative payment"
                                //    decimal _fillCorrPayAmt = 0; decimal _fillCorrAdjAmt = 0; 
                                    decimal _fillCorrResAmt = 0;
                                    Int64 _fillCorrResPayID = 0; Int64 _fillCorrResPayDtlID = 0;
                                    Int64 _fillCorrRefPayID = 0; Int64 _fillCorrRefPayDtlID = 0;
                                 //   int _fillCorrPayIndx = -1;

                                    for (int rPay = 0; rPay <= oCrItems.Count - 1; rPay++)
                                    {
                                        if (Convert.ToDecimal(oCrItems[rPay].Description) > 0)
                                        {
                                            #region "First Sum of same check or reserve entries into single"
                                            _fillCorrResAmt = Convert.ToDecimal(oCrItems[rPay].Description);
                                            _fillCorrResPayID = Convert.ToInt64(oCrItems[rPay].ID);
                                            _fillCorrResPayDtlID = Convert.ToInt64(oCrItems[rPay].Code);

                                            if (oCrItems[rPay].SubItems != null && oCrItems[rPay].SubItems.Count > 0)
                                            {
                                                _fillCorrRefPayID = Convert.ToInt64(oCrItems[rPay].SubItems[0].ID);
                                                _fillCorrRefPayDtlID = Convert.ToInt64(oCrItems[rPay].SubItems[0].Description);
                                            }

                                            for (int rSrchPay = rPay + 1; rSrchPay <= oCrItems.Count - 1; rSrchPay++)
                                            {
                                                decimal _fillSrchResAmt = Convert.ToDecimal(oCrItems[rSrchPay].Description);
                                                Int64 _fillSrchResPayID = Convert.ToInt64(oCrItems[rSrchPay].ID);
                                                Int64 _fillSrchResPayDtlID = Convert.ToInt64(oCrItems[rSrchPay].Code);
                                                Int64 _fillSrchRefPayID = 0;
                                                Int64 _fillSrchRefPayDtlID = 0;
                                                if (oCrItems[rSrchPay].SubItems != null && oCrItems[rSrchPay].SubItems.Count > 0)
                                                {
                                                    _fillSrchRefPayID = Convert.ToInt64(oCrItems[rSrchPay].SubItems[0].ID);
                                                    _fillSrchRefPayDtlID = Convert.ToInt64(oCrItems[rSrchPay].SubItems[0].Description);
                                                }

                                                if (_fillCorrResPayID == _fillSrchResPayID && _fillCorrResPayDtlID == _fillSrchResPayDtlID &&
                                                    _fillCorrRefPayID == _fillSrchRefPayID && _fillCorrRefPayDtlID == _fillSrchRefPayDtlID)
                                                {
                                                    _fillCorrResAmt = _fillCorrResAmt + _fillSrchResAmt;
                                                    oCrItems[rSrchPay].Description = "0";
                                                }
                                            }
                                            #endregion

                                            #region "Set Amount object"
                                            oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                                            oEOBPatientPaymentDetail.EOBPaymentID = 0;
                                            oEOBPatientPaymentDetail.EOBID = 0;
                                            oEOBPatientPaymentDetail.EOBDtlID = 0;
                                            oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

                                            oEOBPatientPaymentDetail.BillingTransactionID = 0;
                                            oEOBPatientPaymentDetail.BillingTransactionDetailID = 0;
                                            oEOBPatientPaymentDetail.BillingTransactionLineNo = 0;
                                            if (mskCloseDate.MaskCompleted == true)
                                            {
                                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                            }
                                            oEOBPatientPaymentDetail.CPTCode = "";
                                            oEOBPatientPaymentDetail.CPTDescription = "";

                                            oEOBPatientPaymentDetail.Amount = _fillCorrResAmt;
                                            oEOBPatientPaymentDetail.IsNullAmount = false;

                                            oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientReserved;
                                            oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Reserved;
                                            oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                            oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                            oEOBPatientPaymentDetail.RefEOBPaymentID = _fillCorrRefPayID;
                                            oEOBPatientPaymentDetail.RefEOBPaymentDetailID = _fillCorrRefPayDtlID;

                                            oEOBPatientPaymentDetail.ReserveEOBPaymentID = 0;
                                            oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = 0;

                                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                            oEOBPatientPaymentDetail.AccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode();
                                            oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Reserved;
                                            oEOBPatientPaymentDetail.MSTAccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode();
                                            oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Reserved;
                                            oEOBPatientPaymentDetail.PatientID = _PatientID;
                                            oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                            oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                            oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                            oEOBPatientPaymentDetail.UserID = _UserId;
                                            oEOBPatientPaymentDetail.UserName = _UserName;
                                            oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                            oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                            //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  assigning value of (PAF)patient account
                                            oEOBPatientPaymentDetail.PAccountID = this.nPAccountID;
                                            oEOBPatientPaymentDetail.AccountPatientID = this.nAccountPatientID;
                                            oEOBPatientPaymentDetail.GuarantorID = this.nGuarantorID;
                                            //End

                                            //if (oList[1] != null && Convert.ToString(oList[1]).Trim() != "")
                                            {
                                                EOBPayment.Common.PaymentPatientLineNote oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

                                                oPaymentPatientLineNote.ClaimNo = 0;
                                                oPaymentPatientLineNote.EOBPaymentID = 0;
                                                oPaymentPatientLineNote.EOBID = 0;
                                                oPaymentPatientLineNote.EOBPaymentDetailID = 0;
                                                oPaymentPatientLineNote.BillingTransactionID = 0;
                                                oPaymentPatientLineNote.BillingTransactionDetailID = 0;
                                                oPaymentPatientLineNote.Code = "";
                                                oPaymentPatientLineNote.Description = Convert.ToString(oList[1]).Trim();
                                                oPaymentPatientLineNote.Amount = Convert.ToDecimal(oList[0]);
                                                if (oList[3] != null && oList[3].ToString().Trim() != "")
                                                {
                                                    oPaymentPatientLineNote.IncludeOnPrint = Convert.ToBoolean(oList[3]);
                                                }
                                                oPaymentPatientLineNote.ClinicID = _ClinicID;
                                                oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientReserved;
                                                if (oList[2] != null && oList[2].ToString().Trim() != "")
                                                {
                                                    oPaymentPatientLineNote.PaymentNoteSubType = (EOBPaymentSubType)Convert.ToInt32(oList[2]);
                                                }
                                                oPaymentPatientLineNote.HasData = true;

                                                oEOBPatientPaymentDetail.LineNotes.Add(oPaymentPatientLineNote);
                                                oPaymentPatientLineNote.Dispose();
                                            }

                                            oPaymentPatient.EOBPatientPaymentReserveLineDetail.Add(oEOBPatientPaymentDetail);
                                            oEOBPatientPaymentDetail.Dispose();
                                            #endregion

                                            oCrItems[rPay].Description = "0";
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion

                    _EOBPaymentID = ogloEOBPaymentPatient.SavePatientCorrectionPayment(oPaymentPatient, false, out EOBPatientPaymentMasterLines);

                    //FillEOBPayments(_EOBPaymentID);

                    _IsAdjustmentMode = false;
                    _retPayId = _EOBPaymentID;
                    _EOBPaymentID = 0;
                    EOBPatientPaymentMasterLines.Clear();
                    btnReserveRemaining.Tag = null;

                    #region "Desing Grids"
                    _IsFormLoading = true;
                    rbPayType_Payment_CheckedChanged(null, null);
                    _IsFormLoading = false;
                    #endregion
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloEOBPaymentPatient != null) { ogloEOBPaymentPatient.Dispose(); };
                if (oPaymentPatient != null) { oPaymentPatient.Dispose(); };
                if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); };
                if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); };
                if (oEOBPatientPaymentCreditDetail != null) { oEOBPatientPaymentCreditDetail.Dispose(); };
                if (oCrItems != null)
                {
                    oCrItems.Clear();
                    oCrItems.Dispose();
                    oCrItems = null;
                }
            }
            return _retPayId;
        }

        private void UpdateNextActionAmount(decimal Amount,Int64 TranId,Int64 TranDtlId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";


            try
            {
                oDB.Connect(false);

                _sqlQuery = " UPDATE BL_EOB_NextAction WITH (READPAST) " +
                " SET dNextActionAmount = " + Amount + " " +
                " WHERE nBillingTransactionID = " + TranId + " AND nBillingTransactionDetailID = " + TranDtlId + " AND nClinicID = " + _ClinicID + " " +
                " AND nNextPartyType = 1 ";

                int _result = oDB.Execute_Query(_sqlQuery);

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }
        }

        #region " BackUp Patient CorrectionPayment "

        //private Int64 SavePatientCorrectionPayment()
        //{
        //    Int64 _retPayId = 0;
        //    EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
        //    EOBPayment.Common.PaymentPatient oPaymentPatient = new global::gloBilling.EOBPayment.Common.PaymentPatient();
        //    EOBPayment.Common.PaymentPatientClaim oPaymentPatientClaim = null;
        //    EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentDetail = null;
        //    EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentCreditDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
        //    gloGeneralItem.gloItems oCrItems = new gloGeneralItem.gloItems();

        //    try
        //    {


        //        Int64 _CloseDayTrayID = 0;
        //        string _CloseDayTrayCode = "";
        //        string _CloseDayTrayName = "";
        //        EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;


        //        #region "Validation"

        //        #endregion

        //        #region "Payment Tray"
        //        if (cmbPaymentTray.SelectedIndex >= 0)
        //        {
        //            DataRowView dvr = (DataRowView)cmbPaymentTray.SelectedItem;
        //            if (dvr != null)
        //            {
        //                _CloseDayTrayID = Convert.ToInt64(cmbPaymentTray.SelectedValue.ToString());
        //                _CloseDayTrayCode = dvr.Row["sCode"].ToString();
        //                _CloseDayTrayName = cmbPaymentTray.Text;
        //            }
        //            if (dvr != null) { dvr = null; }
        //        }
        //        #endregion

        //        #region "Payment Mode"
        //        if (cmbPayMode.Text != "")
        //        {
        //            if (cmbPayMode.Text.Trim() == EOBPaymentMode.None.ToString())
        //            { _EOBPaymentMode = EOBPaymentMode.None; }
        //            else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
        //            { _EOBPaymentMode = EOBPaymentMode.Cash; }
        //            else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
        //            { _EOBPaymentMode = EOBPaymentMode.Check; }
        //            else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
        //            { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
        //            else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
        //            { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
        //            else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
        //            { _EOBPaymentMode = EOBPaymentMode.EFT; }
        //        }
        //        #endregion

        //        #region " Master Data "

        //        oPaymentPatient.PaymentNumber = lblPaymetNo.Text.Trim().Split('#')[1];
        //        oPaymentPatient.PaymentNumberPefix = _paymentPrefix;
        //        oPaymentPatient.EOBPaymentID = _EOBPaymentID;

        //        //oPaymentPatient.EOBRefNO = txtEOBRefNumber.Text.Trim();
        //        if (_EOBPaymentMode == EOBPaymentMode.Cash)
        //        { oPaymentPatient.EOBRefNO = txtCheckNumber.Text.Trim(); }
        //        else
        //        { oPaymentPatient.EOBRefNO = ""; }

        //        oPaymentPatient.PayerName = lblPayer.Text;
        //        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //        oPaymentPatient.PayerID = _PatientID;
        //        oPaymentPatient.PayerType = EOBPaymentAccountType.Patient;
        //        oPaymentPatient.PaymentMode = _EOBPaymentMode;
        //        oPaymentPatient.CheckNumber = txtCheckNumber.Text.Trim(); ;
        //        if (txtCheckAmount.Text.Trim() != "") { oPaymentPatient.CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }

        //        if (mskCheckDate.MaskCompleted)
        //        {
        //            mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //            oPaymentPatient.CheckDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
        //        }

        //        oPaymentPatient.MSTAccountID = _PatientID;
        //        oPaymentPatient.MSTAccountType = EOBPaymentAccountType.Patient;
        //        oPaymentPatient.ClinicID = _ClinicID;
        //        oPaymentPatient.CreatedDateTime = DateTime.Now;
        //        oPaymentPatient.ModifiedDateTime = DateTime.Now;

        //        oPaymentPatient.PaymentTrayID = _CloseDayTrayID;
        //        oPaymentPatient.PaymentTrayCode = _CloseDayTrayCode;
        //        oPaymentPatient.PaymentTrayDesc = _CloseDayTrayName;

        //        if (mskCloseDate.MaskCompleted == true)
        //        {
        //            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //            oPaymentPatient.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //        }
        //        oPaymentPatient.CardType = "";
        //        oPaymentPatient.CardSecurityNo = "";
        //        oPaymentPatient.CardID = 0;

        //        oPaymentPatient.UserID = _UserId;
        //        oPaymentPatient.UserName = _UserName;

        //        #region "Payment Master Note"
        //        //Notes if any to main payment to all claim OR main payment to reserve account
        //        if (txtPayMstNotes.Text.Trim().Length > 0)
        //        {
        //            EOBPayment.Common.PaymentPatientLineNote oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

        //            oPaymentPatientLineNote.ClaimNo = 0;
        //            oPaymentPatientLineNote.EOBPaymentID = 0;
        //            oPaymentPatientLineNote.EOBID = 0;
        //            oPaymentPatientLineNote.EOBPaymentDetailID = 0;
        //            oPaymentPatientLineNote.BillingTransactionID = 0;
        //            oPaymentPatientLineNote.BillingTransactionDetailID = 0;
        //            oPaymentPatientLineNote.Code = "";
        //            oPaymentPatientLineNote.Description = txtPayMstNotes.Text.Trim();
        //            if (txtCheckAmount.Text.Trim() != "") { oPaymentPatientLineNote.Amount = Convert.ToDecimal(txtCheckAmount.Text.Trim()); }
        //            oPaymentPatientLineNote.IncludeOnPrint = chkPayMstIncludeNotes.Checked;
        //            oPaymentPatientLineNote.ClinicID = _ClinicID;
        //            oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientPayment;
        //            oPaymentPatientLineNote.PaymentNoteSubType = EOBPaymentSubType.Other;
        //            oPaymentPatientLineNote.HasData = true;

        //            oPaymentPatient.Notes.Add(oPaymentPatientLineNote);
        //            oPaymentPatientLineNote.Dispose();
        //        }
        //        #endregion

        //        #endregion

        //        #region "Credit Service Line Entry applicable to all claims, so it goes to master level not line level"

        //        #region "Main Credit Line entry will be zero for reserve payment, when check and reserve in same entry then we will use this one, till that point its 0 payment entry"

        //        //now its one time entry, but for partial payement implement like insurace payment
        //        oEOBPatientPaymentCreditDetail.EOBPaymentID = _EOBPaymentID;
        //        oEOBPatientPaymentCreditDetail.EOBID = 0;
        //        oEOBPatientPaymentCreditDetail.EOBDtlID = 0;
        //        oEOBPatientPaymentCreditDetail.EOBPaymentDetailID = 0;
        //        oEOBPatientPaymentCreditDetail.RefEOBPaymentID = 0;
        //        oEOBPatientPaymentCreditDetail.RefEOBPaymentDetailID = 0;
        //        oEOBPatientPaymentCreditDetail.ReserveEOBPaymentID = 0;
        //        oEOBPatientPaymentCreditDetail.ReserveEOBPaymentDetailID = 0;


        //        oEOBPatientPaymentCreditDetail.BillingTransactionID = 0;
        //        oEOBPatientPaymentCreditDetail.BillingTransactionDetailID = 0;
        //        oEOBPatientPaymentCreditDetail.BillingTransactionLineNo = 0;
        //        if (mskCloseDate.MaskCompleted == true)
        //        {
        //            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //            oEOBPatientPaymentCreditDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //            oEOBPatientPaymentCreditDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //        }
        //        oEOBPatientPaymentCreditDetail.CPTCode = "";
        //        oEOBPatientPaymentCreditDetail.CPTDescription = "";

        //        oEOBPatientPaymentCreditDetail.Amount = 0;
        //        oEOBPatientPaymentCreditDetail.IsNullAmount = false;

        //        oEOBPatientPaymentCreditDetail.PaymentType = EOBPaymentType.PatientPayment;
        //        oEOBPatientPaymentCreditDetail.PaymentSubType = EOBPaymentSubType.Patient;
        //        oEOBPatientPaymentCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
        //        oEOBPatientPaymentCreditDetail.PayMode = _EOBPaymentMode;

        //        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //        oEOBPatientPaymentCreditDetail.AccountID = _PatientID;
        //        oEOBPatientPaymentCreditDetail.AccountType = EOBPaymentAccountType.Patient;
        //        oEOBPatientPaymentCreditDetail.MSTAccountID = _PatientID;
        //        oEOBPatientPaymentCreditDetail.MSTAccountType = EOBPaymentAccountType.Patient;
        //        oEOBPatientPaymentCreditDetail.ContactInsID = 0;

        //        oEOBPatientPaymentCreditDetail.PatientID = _PatientID;
        //        oEOBPatientPaymentCreditDetail.PaymentTrayID = _CloseDayTrayID;
        //        oEOBPatientPaymentCreditDetail.PaymentTrayCode = _CloseDayTrayCode;
        //        oEOBPatientPaymentCreditDetail.PaymentTrayDescription = _CloseDayTrayName;
        //        oEOBPatientPaymentCreditDetail.UserID = _UserId;
        //        oEOBPatientPaymentCreditDetail.UserName = _UserName;
        //        oEOBPatientPaymentCreditDetail.ClinicID = _ClinicID;

        //        oEOBPatientPaymentCreditDetail.FinanceLieNo = 1;
        //        oEOBPatientPaymentCreditDetail.MainCreditLineID = 0;
        //        oEOBPatientPaymentCreditDetail.IsMainCreditLine = true;
        //        oEOBPatientPaymentCreditDetail.IsReserveCreditLine = false;
        //        oEOBPatientPaymentCreditDetail.IsCorrectionCreditLine = false;
        //        oEOBPatientPaymentCreditDetail.RefFinanceLieNo = 0;
        //        oEOBPatientPaymentCreditDetail.UseRefFinanceLieNo = false;

        //        oPaymentPatient.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentCreditDetail);

        //        #endregion

        //        #region "Negative Amount Credit Entries - Fetch from database and set to object"

        //        if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
        //        {
        //            for (int nCrIndex = 1; nCrIndex <= c1SinglePayment.Rows.Count - 1; nCrIndex++)
        //            {
        //                if (c1SinglePayment.GetData(nCrIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(nCrIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
        //                {
        //                    if (c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT) != null && c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT).ToString().Trim() != "")
        //                    {
        //                        if (Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT)) < 0)
        //                        {
        //                            decimal _crPayAmt = Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT)) - (Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT)) * 2);
        //                            int _crResPayMode = 0;

        //                            Int64 _crBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
        //                            Int64 _crBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
        //                            Int64 _crBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));
        //                            Int64 _crPatientId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_PATIENTID));
        //                            string _crCptCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE)).Trim();

        //                            gloDatabaseLayer.DBLayer _nCrDBLayer = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //                            gloDatabaseLayer.DBParameters _nCrDBParameters = new gloDatabaseLayer.DBParameters();
        //                            DataTable _nCrDataTable = new DataTable();

        //                            _nCrDBParameters.Add("@CorrectionAmount", _crPayAmt, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,2),
        //                            _nCrDBParameters.Add("@nPatientID", _crPatientId, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
        //                            _nCrDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
        //                            _nCrDBParameters.Add("@nBillingTransactionID", _crBillTrnId, ParameterDirection.Input, SqlDbType.BigInt);//   numeric(18,0),
        //                            _nCrDBParameters.Add("@nBillingTransactionDetailID", _crBillTrnDtlId, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0)
        //                            _nCrDBLayer.Connect(false);
        //                            _nCrDBLayer.Retrive("BL_SELECT_EOBCorrectionAmountList", _nCrDBParameters, out _nCrDataTable);
        //                            _nCrDBLayer.Disconnect();
        //                            _nCrDBLayer.Dispose();

        //                            if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
        //                            {
        //                                for (int nCrDTIndex = 0; nCrDTIndex <= _nCrDataTable.Rows.Count - 1; nCrDTIndex++)
        //                                {
        //                                    //Store Cr Amt list for apply to debit lines
        //                                    gloGeneralItem.gloItem ogloItem = new gloGeneralItem.gloItem(Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]), Convert.ToString(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]), Convert.ToString(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]));
        //                                    ogloItem.SubItems.Add(Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]), Convert.ToString(_nCrDataTable.Rows[nCrDTIndex]["nPayMode"]), Convert.ToString(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]));
        //                                    oCrItems.Add(ogloItem);
        //                                    ogloItem.Dispose();

        //                                    EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();

        //                                    #region "Set Object"

        //                                    oEOBPatientPaymentResAsCreditDetail.EOBPaymentID = _EOBPaymentID;
        //                                    oEOBPatientPaymentResAsCreditDetail.EOBID = 0;
        //                                    oEOBPatientPaymentResAsCreditDetail.EOBDtlID = 0;
        //                                    oEOBPatientPaymentResAsCreditDetail.EOBPaymentDetailID = 0;

        //                                    oEOBPatientPaymentResAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
        //                                    oEOBPatientPaymentResAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
        //                                    oEOBPatientPaymentResAsCreditDetail.ReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
        //                                    oEOBPatientPaymentResAsCreditDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

        //                                    oEOBPatientPaymentResAsCreditDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
        //                                    oEOBPatientPaymentResAsCreditDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
        //                                    oEOBPatientPaymentResAsCreditDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));

        //                                    oEOBPatientPaymentResAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
        //                                    oEOBPatientPaymentResAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

        //                                    oEOBPatientPaymentResAsCreditDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE));
        //                                    oEOBPatientPaymentResAsCreditDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_DESCRIPTON));

        //                                    oEOBPatientPaymentResAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) - (Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) * 2);
        //                                    oEOBPatientPaymentResAsCreditDetail.IsNullAmount = false;

        //                                    oEOBPatientPaymentResAsCreditDetail.PaymentType = EOBPaymentType.PatientPayment;
        //                                    oEOBPatientPaymentResAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
        //                                    oEOBPatientPaymentResAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
        //                                    oEOBPatientPaymentResAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

        //                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                                    oEOBPatientPaymentResAsCreditDetail.AccountID = _PatientID;
        //                                    oEOBPatientPaymentResAsCreditDetail.AccountType = EOBPaymentAccountType.Patient;
        //                                    oEOBPatientPaymentResAsCreditDetail.MSTAccountID = _PatientID;
        //                                    oEOBPatientPaymentResAsCreditDetail.MSTAccountType = EOBPaymentAccountType.Patient;
        //                                    oEOBPatientPaymentResAsCreditDetail.PatientID = _PatientID;
        //                                    oEOBPatientPaymentResAsCreditDetail.PaymentTrayID = _CloseDayTrayID;
        //                                    oEOBPatientPaymentResAsCreditDetail.PaymentTrayCode = _CloseDayTrayCode;
        //                                    oEOBPatientPaymentResAsCreditDetail.PaymentTrayDescription = _CloseDayTrayName;
        //                                    oEOBPatientPaymentResAsCreditDetail.UserID = _UserId;
        //                                    oEOBPatientPaymentResAsCreditDetail.UserName = _UserName;
        //                                    oEOBPatientPaymentResAsCreditDetail.ClinicID = _ClinicID;

        //                                    oEOBPatientPaymentResAsCreditDetail.FinanceLieNo = oPaymentPatient.EOBPatientPaymentLineDetails.Count + 1;
        //                                    oEOBPatientPaymentResAsCreditDetail.MainCreditLineID = 0;
        //                                    oEOBPatientPaymentResAsCreditDetail.IsMainCreditLine = false;
        //                                    oEOBPatientPaymentResAsCreditDetail.IsReserveCreditLine = false;
        //                                    oEOBPatientPaymentResAsCreditDetail.IsCorrectionCreditLine = true;
        //                                    oEOBPatientPaymentResAsCreditDetail.RefFinanceLieNo = 0;
        //                                    oEOBPatientPaymentResAsCreditDetail.UseRefFinanceLieNo = false;

        //                                    oPaymentPatient.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentResAsCreditDetail);

        //                                    #endregion

        //                                    oEOBPatientPaymentResAsCreditDetail.Dispose();
        //                                }
        //                            }
        //                            _nCrDBLayer.Dispose();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        #endregion

        //        #endregion

        //        if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
        //        {
        //            for (int rowIndex = 1; rowIndex <= c1SinglePayment.Rows.Count - 1; rowIndex++)
        //            {
        //                if (c1SinglePayment.GetData(rowIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rowIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
        //                {
        //                    int _claimStartIndex = Convert.ToInt32(c1SinglePayment.GetData(rowIndex, COL_CELLRANGE_R1)) + 1;
        //                    int _claimEndIndex = Convert.ToInt32(c1SinglePayment.GetData(rowIndex, COL_CELLRANGE_R2));

        //                    #region  "..Need to check if payment or adjustment is made against any of the claim line then skip claim "

        //                    bool _hasDataToSave = false;
        //                    for (int index = _claimStartIndex; index <= _claimEndIndex; index++)
        //                    {
        //                        if (
        //                            (c1SinglePayment.GetData(index, COL_CUR_PAYMENT) != null
        //                            && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)).Trim() != ""
        //                            && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)) > 0) ||
        //                            (c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT) != null
        //                            && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)).Trim() != ""
        //                            && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)) > 0)
        //                           )
        //                        {
        //                            _hasDataToSave = true;
        //                            break;
        //                        }

        //                    }

        //                    if (_hasDataToSave == false) { continue; }

        //                    #endregion

        //                    oPaymentPatientClaim = new global::gloBilling.EOBPayment.Common.PaymentPatientClaim();

        //                    oPaymentPatientClaim.ClaimNo = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_CLAIMNO)));
        //                    oPaymentPatientClaim.ClaimNoPrefix = "";
        //                    oPaymentPatientClaim.ClinicID = _ClinicID;
        //                    oPaymentPatientClaim.DisplayClaimNo = Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_CLAIMDISPNO));
        //                    oPaymentPatientClaim.PatientID = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_PATIENTID)));
        //                    oPaymentPatientClaim.PatientName = Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_PATIENTNAME));
        //                    oPaymentPatientClaim.BillingTransactionID = Convert.ToInt64(Convert.ToString(c1SinglePayment.GetData(_claimStartIndex, COL_BILLING_TRANSACTON_ID)));


        //                    for (int i = _claimStartIndex; i <= _claimEndIndex; i++)
        //                    {
        //                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
        //                        {
        //                            if (
        //                            (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null
        //                            && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
        //                            || (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null
        //                            && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
        //                           )
        //                            {
        //                                #region "EOB Service Lines"

        //                                EOBPayment.Common.PaymentPatientLine oPaymentPatientLine = new global::gloBilling.EOBPayment.Common.PaymentPatientLine();

        //                                #region "EOB Line"

        //                                oPaymentPatientLine.PatientID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
        //                                oPaymentPatientLine.PatInsuranceID = 0;// Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
        //                                oPaymentPatientLine.InsContactID = 0;// Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
        //                                oPaymentPatientLine.BLTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                oPaymentPatientLine.BLTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                                oPaymentPatientLine.BLTransactionLineNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
        //                                oPaymentPatientLine.ClaimNumber = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));

        //                                oPaymentPatientLine.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
        //                                oPaymentPatientLine.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));

        //                                oPaymentPatientLine.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
        //                                oPaymentPatientLine.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

        //                                oPaymentPatientLine.BLInsuranceID = 0;
        //                                oPaymentPatientLine.BLInsuranceName = "";
        //                                oPaymentPatientLine.BLInsuranceFlag = InsuranceTypeFlag.None;

        //                                if (c1SinglePayment.GetData(i, COL_CHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CHARGE)).Trim() != "")
        //                                { oPaymentPatientLine.Charges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CHARGE)); oPaymentPatientLine.IsNullCharges = false; }

        //                                if (c1SinglePayment.GetData(i, COL_UNIT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_UNIT)).Trim() != "")
        //                                { oPaymentPatientLine.Unit = Convert.ToInt64(c1SinglePayment.GetData(i, COL_UNIT)); oPaymentPatientLine.IsNullUnit = false; }

        //                                if (c1SinglePayment.GetData(i, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_TOTALCHARGE)).Trim() != "")
        //                                { oPaymentPatientLine.TotalCharges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_TOTALCHARGE)); oPaymentPatientLine.IsNullTotalCharges = false; }

        //                                oPaymentPatientLine.Allowed = 0;

        //                                if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
        //                                { oPaymentPatientLine.WriteOff = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)); oPaymentPatientLine.IsNullWriteOff = false; }

        //                                oPaymentPatientLine.NonCovered = 0;
        //                                if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
        //                                { oPaymentPatientLine.InsuranceAmount = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)); oPaymentPatientLine.IsNullInsurance = false; }

        //                                oPaymentPatientLine.Copay = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COPAY)); }
        //                                oPaymentPatientLine.Deductible = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)); }
        //                                oPaymentPatientLine.CoInsurance = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COINSURANCE)); }
        //                                oPaymentPatientLine.Withhold = 0;//Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WITHHOLD)); }

        //                                oPaymentPatientLine.PaymentTrayID = _CloseDayTrayID;
        //                                oPaymentPatientLine.PaymentTrayCode = _CloseDayTrayCode;
        //                                oPaymentPatientLine.PaymentTrayDesc = _CloseDayTrayName;


        //                                oPaymentPatientLine.UserID = _UserId;
        //                                oPaymentPatientLine.UserName = _UserName;
        //                                oPaymentPatientLine.ClinicID = _ClinicID;
        //                                oPaymentPatientLine.EOBType = EOBPaymentType.PatientPayment;

        //                                //Adjestment Information
        //                                if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
        //                                {
        //                                    if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) > 0)
        //                                    {
        //                                        EOBPayment.Common.PaymentPatientLineAdjustmentCode oPaymentPatientLineAdjustmentCode = new global::gloBilling.EOBPayment.Common.PaymentPatientLineAdjustmentCode();

        //                                        oPaymentPatientLineAdjustmentCode.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
        //                                        oPaymentPatientLineAdjustmentCode.EOBPaymentID = 0;
        //                                        oPaymentPatientLineAdjustmentCode.EOBID = 0;
        //                                        oPaymentPatientLineAdjustmentCode.EOBPaymentDetailID = 0;

        //                                        oPaymentPatientLineAdjustmentCode.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                        oPaymentPatientLineAdjustmentCode.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));


        //                                        //if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
        //                                        //{
        //                                        //    string[] AdjCodeDesc = null;
        //                                        //    string _adjstr = "";
        //                                        //    _adjstr = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim();
        //                                        //    AdjCodeDesc = _adjstr.Split('-');

        //                                        //    oPaymentPatientLineAdjustmentCode.Code = AdjCodeDesc[0];
        //                                        //    oPaymentPatientLineAdjustmentCode.Description = AdjCodeDesc[1];
        //                                        //}

        //                                        if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
        //                                        { oPaymentPatientLineAdjustmentCode.Code = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim(); }
        //                                        if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim() != "")
        //                                        { oPaymentPatientLineAdjustmentCode.Description = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim(); }


        //                                        if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
        //                                        {
        //                                            oPaymentPatientLineAdjustmentCode.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)));
        //                                            oPaymentPatientLineAdjustmentCode.IsNullAmount = false;
        //                                        }
        //                                        oPaymentPatientLineAdjustmentCode.ClinicID = _ClinicID;

        //                                        oPaymentPatientLine.LineAdjestmentCodes.Add(oPaymentPatientLineAdjustmentCode);
        //                                        oPaymentPatientLineAdjustmentCode.Dispose();
        //                                    }
        //                                }

        //                                #endregion

        //                                #region "Debit Service Line - Patient"
        //                                if (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)) > 0)
        //                                {
        //                                    #region "Debit Service Line - patient "

        //                                    decimal _fillPayAmt = 0; decimal _fillAdjAmt = 0; decimal _fillResAmt = 0;
        //                                    Int64 _fillResPayID = 0; Int64 _fillResPayDtlID = 0;
        //                                    Int64 _fillRefPayID = 0; Int64 _fillRefPayDtlID = 0;
        //                                    int _fillrPayIndx = -1;
        //                                    int _fillRefFinanceLieNo = 0;
        //                                    bool _fillUseRefFinanceLieNo = false;
        //                                    bool _isNullfillPayAmt = false;
        //                                    bool _isNullfillAdjAmt = false;

        //                                    if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
        //                                    { _fillPayAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT))); _isNullfillPayAmt = false; }

        //                                    _fillPayAmt = _fillPayAmt - (_fillPayAmt * 2);

        //                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
        //                                    { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); _isNullfillAdjAmt = false; }

        //                                    _fillAdjAmt = _fillAdjAmt - (_fillAdjAmt * 2);

        //                                    for (int rPay = 0; rPay <= oCrItems.Count - 1; rPay++)
        //                                    {
        //                                        if (Convert.ToDecimal(oCrItems[rPay].Description) > 0)
        //                                        {
        //                                            _fillResAmt = Convert.ToDecimal(oCrItems[rPay].Description);
        //                                            _fillResPayID = Convert.ToInt64(oCrItems[rPay].ID);
        //                                            _fillResPayDtlID = Convert.ToInt64(oCrItems[rPay].Code);

        //                                            if (oCrItems[rPay].SubItems != null && oCrItems[rPay].SubItems.Count > 0)
        //                                            {
        //                                                _fillRefPayID = Convert.ToInt64(oCrItems[rPay].SubItems[0].ID);
        //                                                _fillRefPayDtlID = Convert.ToInt64(oCrItems[rPay].SubItems[0].Description);
        //                                            }

        //                                            //This logic is temporary depend upon the gloItems
        //                                            //when we implement partial payment it should be depend upon the "EOBInsurancePaymentMasterAllocationLines" this object
        //                                            if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
        //                                            {
        //                                                _fillUseRefFinanceLieNo = true;
        //                                                _fillRefFinanceLieNo = rPay + 2;
        //                                            }

        //                                            _fillrPayIndx = rPay;
        //                                            break;
        //                                        }
        //                                    }

        //                                    if (_fillPayAmt <= _fillResAmt)
        //                                    {
        //                                        #region "Set Less Amount Single object"
        //                                        oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
        //                                        oEOBPatientPaymentDetail.EOBPaymentID = 0;
        //                                        oEOBPatientPaymentDetail.EOBID = 0;
        //                                        oEOBPatientPaymentDetail.EOBDtlID = 0;
        //                                        oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

        //                                        oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                        oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                                        oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
        //                                        oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
        //                                        oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
        //                                        oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
        //                                        oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

        //                                        oEOBPatientPaymentDetail.Amount = _fillPayAmt - (_fillPayAmt * 2);
        //                                        oEOBPatientPaymentDetail.IsNullAmount = _isNullfillPayAmt;

        //                                        oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
        //                                        oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Patient;
        //                                        oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
        //                                        oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

        //                                        oEOBPatientPaymentDetail.RefEOBPaymentID = _fillRefPayID;
        //                                        oEOBPatientPaymentDetail.RefEOBPaymentDetailID = _fillRefPayDtlID;
        //                                        oEOBPatientPaymentDetail.ReserveEOBPaymentID = _fillResPayID;
        //                                        oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = _fillResPayDtlID;

        //                                        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                                        oEOBPatientPaymentDetail.AccountID = _PatientID;
        //                                        oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
        //                                        oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
        //                                        oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
        //                                        oEOBPatientPaymentDetail.PatientID = _PatientID;
        //                                        oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
        //                                        oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
        //                                        oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
        //                                        oEOBPatientPaymentDetail.UserID = _UserId;
        //                                        oEOBPatientPaymentDetail.UserName = _UserName;
        //                                        oEOBPatientPaymentDetail.ClinicID = _ClinicID;

        //                                        oEOBPatientPaymentDetail.FinanceLieNo = 0;
        //                                        oEOBPatientPaymentDetail.MainCreditLineID = 0;
        //                                        oEOBPatientPaymentDetail.IsMainCreditLine = false;
        //                                        oEOBPatientPaymentDetail.IsReserveCreditLine = false;
        //                                        oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
        //                                        oEOBPatientPaymentDetail.RefFinanceLieNo = _fillRefFinanceLieNo;
        //                                        oEOBPatientPaymentDetail.UseRefFinanceLieNo = _fillUseRefFinanceLieNo;

        //                                        oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
        //                                        oEOBPatientPaymentDetail.Dispose();

        //                                        if (_fillrPayIndx != -1)
        //                                        {
        //                                            oCrItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
        //                                        }
        //                                        #endregion
        //                                    }
        //                                    else
        //                                    {
        //                                        #region "Set More Amount Multiple object"

        //                                        decimal _fillPayMulAmt = _fillPayAmt;

        //                                        do
        //                                        {
        //                                            if (Convert.ToDecimal(oCrItems[_fillrPayIndx].Description) > 0)
        //                                            {
        //                                                _fillResAmt = Convert.ToDecimal(oCrItems[_fillrPayIndx].Description);
        //                                                _fillResPayID = Convert.ToInt64(oCrItems[_fillrPayIndx].ID);
        //                                                _fillResPayDtlID = Convert.ToInt64(oCrItems[_fillrPayIndx].Code);
        //                                                _fillRefFinanceLieNo = 0;
        //                                                _fillUseRefFinanceLieNo = false;
        //                                                _isNullfillPayAmt = false;

        //                                                if (oCrItems[_fillrPayIndx].SubItems != null && oCrItems[_fillrPayIndx].SubItems.Count > 0)
        //                                                {
        //                                                    _fillRefPayID = Convert.ToInt64(oCrItems[_fillrPayIndx].SubItems[0].ID);
        //                                                    _fillRefPayDtlID = Convert.ToInt64(oCrItems[_fillrPayIndx].SubItems[0].Description);
        //                                                }

        //                                                if (_fillPayMulAmt >= _fillResAmt)
        //                                                { _fillPayAmt = _fillResAmt; _fillPayMulAmt = _fillPayMulAmt - _fillPayAmt; }
        //                                                else
        //                                                { _fillPayAmt = _fillPayMulAmt; _fillPayMulAmt = _fillPayMulAmt - _fillPayMulAmt; }

        //                                                //This logic is temporary depend upon the gloItems
        //                                                //when we implement partial payment it should be depend upon the "EOBInsurancePaymentMasterAllocationLines" this object
        //                                                if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
        //                                                {
        //                                                    _fillUseRefFinanceLieNo = true;
        //                                                    _fillRefFinanceLieNo = _fillrPayIndx + 2;
        //                                                }
        //                                            }

        //                                            #region "Set object"

        //                                            oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
        //                                            oEOBPatientPaymentDetail.EOBPaymentID = 0;
        //                                            oEOBPatientPaymentDetail.EOBID = 0;
        //                                            oEOBPatientPaymentDetail.EOBDtlID = 0;
        //                                            oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

        //                                            oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                            oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                                            oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
        //                                            oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
        //                                            oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
        //                                            oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
        //                                            oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

        //                                            oEOBPatientPaymentDetail.Amount = _fillPayAmt - (_fillPayAmt * 2);
        //                                            oEOBPatientPaymentDetail.IsNullAmount = _isNullfillPayAmt;

        //                                            oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
        //                                            oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Patient;
        //                                            oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
        //                                            oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

        //                                            oEOBPatientPaymentDetail.RefEOBPaymentID = _fillRefPayID;
        //                                            oEOBPatientPaymentDetail.RefEOBPaymentDetailID = _fillRefPayDtlID;
        //                                            oEOBPatientPaymentDetail.ReserveEOBPaymentID = _fillResPayID;
        //                                            oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = _fillResPayDtlID;

        //                                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                                            oEOBPatientPaymentDetail.AccountID = _PatientID;
        //                                            oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
        //                                            oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
        //                                            oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
        //                                            oEOBPatientPaymentDetail.PatientID = _PatientID;
        //                                            oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
        //                                            oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
        //                                            oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
        //                                            oEOBPatientPaymentDetail.UserID = _UserId;
        //                                            oEOBPatientPaymentDetail.UserName = _UserName;
        //                                            oEOBPatientPaymentDetail.ClinicID = _ClinicID;

        //                                            oEOBPatientPaymentDetail.FinanceLieNo = 0;
        //                                            oEOBPatientPaymentDetail.MainCreditLineID = 0;
        //                                            oEOBPatientPaymentDetail.IsMainCreditLine = false;
        //                                            oEOBPatientPaymentDetail.IsReserveCreditLine = false;
        //                                            oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
        //                                            oEOBPatientPaymentDetail.RefFinanceLieNo = _fillRefFinanceLieNo;
        //                                            oEOBPatientPaymentDetail.UseRefFinanceLieNo = _fillUseRefFinanceLieNo;

        //                                            oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
        //                                            oEOBPatientPaymentDetail.Dispose();

        //                                            #endregion

        //                                            if (_fillrPayIndx != -1)
        //                                            {
        //                                                oCrItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
        //                                                _fillrPayIndx = _fillrPayIndx + 1;
        //                                                if (_fillrPayIndx >= oCrItems.Count) { break; }
        //                                            }

        //                                        }
        //                                        while (_fillPayMulAmt > 0);

        //                                        #endregion
        //                                    }



        //                                    #endregion

        //                                    #region "Debit Service Line - patient adjuestment if any"

        //                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
        //                                    {
        //                                        if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) > 0)
        //                                        {
        //                                            oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
        //                                            oEOBPatientPaymentDetail.EOBPaymentID = 0;
        //                                            oEOBPatientPaymentDetail.EOBID = 0;
        //                                            oEOBPatientPaymentDetail.EOBDtlID = 0;
        //                                            oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

        //                                            oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                            oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                                            oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));
        //                                            oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
        //                                            oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
        //                                            oEOBPatientPaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
        //                                            oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

        //                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
        //                                            { oEOBPatientPaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); oEOBPatientPaymentDetail.IsNullAmount = false; }


        //                                            oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
        //                                            oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Adjuestment;
        //                                            oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
        //                                            oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

        //                                            oEOBPatientPaymentDetail.RefEOBPaymentID = 0;
        //                                            oEOBPatientPaymentDetail.RefEOBPaymentDetailID = 0;
        //                                            oEOBPatientPaymentDetail.ReserveEOBPaymentID = 0;
        //                                            oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = 0;

        //                                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                                            oEOBPatientPaymentDetail.AccountID = _PatientID;
        //                                            oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
        //                                            oEOBPatientPaymentDetail.MSTAccountID = _PatientID;
        //                                            oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
        //                                            oEOBPatientPaymentDetail.PatientID = _PatientID;
        //                                            oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
        //                                            oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
        //                                            oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
        //                                            oEOBPatientPaymentDetail.UserID = _UserId;
        //                                            oEOBPatientPaymentDetail.UserName = _UserName;
        //                                            oEOBPatientPaymentDetail.ClinicID = _ClinicID;

        //                                            oEOBPatientPaymentDetail.FinanceLieNo = 0;
        //                                            oEOBPatientPaymentDetail.MainCreditLineID = 0;
        //                                            oEOBPatientPaymentDetail.IsMainCreditLine = false;
        //                                            oEOBPatientPaymentDetail.IsReserveCreditLine = false;
        //                                            oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
        //                                            oEOBPatientPaymentDetail.RefFinanceLieNo = 0;
        //                                            oEOBPatientPaymentDetail.UseRefFinanceLieNo = false;

        //                                            oPaymentPatientLine.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentDetail);
        //                                            oEOBPatientPaymentDetail.Dispose();
        //                                        }
        //                                    }
        //                                    #endregion
        //                                }
        //                                #endregion

        //                                oPaymentPatientClaim.CliamLines.Add(oPaymentPatientLine);

        //                                oPaymentPatientLine.Dispose();

        //                                if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); }

        //                                #endregion
        //                            }
        //                        }
        //                    }

        //                    oPaymentPatient.PatientClaims.Add(oPaymentPatientClaim);
        //                    oPaymentPatientClaim.Dispose();
        //                }
        //            }
        //        }

        //        #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"
        //        if (btnReserveRemaining.Tag != null && btnReserveRemaining.Tag.ToString().Trim().Length > 0)
        //        {

        //            //0 ReserveAmount 
        //            //1 ReserveNote 
        //            //2 ReserveSubType 
        //            //3 ReserveNoteOnPrint 
        //            string[] oList = null;
        //            if (btnReserveRemaining.Tag != null)
        //            {
        //                oList = btnReserveRemaining.Tag.ToString().Split('~');
        //            }
        //            if (oList != null && oList.Length == 6)
        //            {
        //                if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
        //                {
        //                    if (Convert.ToDecimal(Convert.ToString(oList[0]).Trim()) > 0)
        //                    {
        //                        #region "Put Amount into reserve, but according to remaining amount which is collected from negative payment"
        //                        decimal _fillCorrPayAmt = 0; decimal _fillCorrAdjAmt = 0; decimal _fillCorrResAmt = 0;
        //                        Int64 _fillCorrResPayID = 0; Int64 _fillCorrResPayDtlID = 0;
        //                        Int64 _fillCorrRefPayID = 0; Int64 _fillCorrRefPayDtlID = 0;
        //                        int _fillCorrPayIndx = -1;

        //                        for (int rPay = 0; rPay <= oCrItems.Count - 1; rPay++)
        //                        {
        //                            if (Convert.ToDecimal(oCrItems[rPay].Description) > 0)
        //                            {
        //                                #region "First Sum of same check or reserve entries into single"
        //                                _fillCorrResAmt = Convert.ToDecimal(oCrItems[rPay].Description);
        //                                _fillCorrResPayID = Convert.ToInt64(oCrItems[rPay].ID);
        //                                _fillCorrResPayDtlID = Convert.ToInt64(oCrItems[rPay].Code);

        //                                if (oCrItems[rPay].SubItems != null && oCrItems[rPay].SubItems.Count > 0)
        //                                {
        //                                    _fillCorrRefPayID = Convert.ToInt64(oCrItems[rPay].SubItems[0].ID);
        //                                    _fillCorrRefPayDtlID = Convert.ToInt64(oCrItems[rPay].SubItems[0].Description);
        //                                }

        //                                for (int rSrchPay = rPay + 1; rSrchPay <= oCrItems.Count - 1; rSrchPay++)
        //                                {
        //                                    decimal _fillSrchResAmt = Convert.ToDecimal(oCrItems[rSrchPay].Description);
        //                                    Int64 _fillSrchResPayID = Convert.ToInt64(oCrItems[rSrchPay].ID);
        //                                    Int64 _fillSrchResPayDtlID = Convert.ToInt64(oCrItems[rSrchPay].Code);
        //                                    Int64 _fillSrchRefPayID = 0;
        //                                    Int64 _fillSrchRefPayDtlID = 0;
        //                                    if (oCrItems[rSrchPay].SubItems != null && oCrItems[rSrchPay].SubItems.Count > 0)
        //                                    {
        //                                        _fillSrchRefPayID = Convert.ToInt64(oCrItems[rSrchPay].SubItems[0].ID);
        //                                        _fillSrchRefPayDtlID = Convert.ToInt64(oCrItems[rSrchPay].SubItems[0].Description);
        //                                    }

        //                                    if (_fillCorrResPayID == _fillSrchResPayID && _fillCorrResPayDtlID == _fillSrchResPayDtlID &&
        //                                        _fillCorrRefPayID == _fillSrchRefPayID && _fillCorrRefPayDtlID == _fillSrchRefPayDtlID)
        //                                    {
        //                                        _fillCorrResAmt = _fillCorrResAmt + _fillSrchResAmt;
        //                                        oCrItems[rSrchPay].Description = "0";
        //                                    }
        //                                }
        //                                #endregion

        //                                #region "Set Amount object"
        //                                oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
        //                                oEOBPatientPaymentDetail.EOBPaymentID = 0;
        //                                oEOBPatientPaymentDetail.EOBID = 0;
        //                                oEOBPatientPaymentDetail.EOBDtlID = 0;
        //                                oEOBPatientPaymentDetail.EOBPaymentDetailID = 0;

        //                                oEOBPatientPaymentDetail.BillingTransactionID = 0;
        //                                oEOBPatientPaymentDetail.BillingTransactionDetailID = 0;
        //                                oEOBPatientPaymentDetail.BillingTransactionLineNo = 0;
        //                                if (mskCloseDate.MaskCompleted == true)
        //                                {
        //                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                                    oEOBPatientPaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                                    oEOBPatientPaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                                }
        //                                oEOBPatientPaymentDetail.CPTCode = "";
        //                                oEOBPatientPaymentDetail.CPTDescription = "";

        //                                oEOBPatientPaymentDetail.Amount = _fillCorrResAmt;
        //                                oEOBPatientPaymentDetail.IsNullAmount = false;

        //                                oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientReserved;
        //                                oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Reserved;
        //                                oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
        //                                oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

        //                                oEOBPatientPaymentDetail.RefEOBPaymentID = _fillCorrRefPayID;
        //                                oEOBPatientPaymentDetail.RefEOBPaymentDetailID = _fillCorrRefPayDtlID;

        //                                oEOBPatientPaymentDetail.ReserveEOBPaymentID = 0;
        //                                oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = 0;

        //                                //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                                oEOBPatientPaymentDetail.AccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode();
        //                                oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Reserved;
        //                                oEOBPatientPaymentDetail.MSTAccountID = EOBPaymentTypeAccountNo.PatientReserved.GetHashCode();
        //                                oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Reserved;
        //                                oEOBPatientPaymentDetail.PatientID = _PatientID;
        //                                oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
        //                                oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
        //                                oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
        //                                oEOBPatientPaymentDetail.UserID = _UserId;
        //                                oEOBPatientPaymentDetail.UserName = _UserName;
        //                                oEOBPatientPaymentDetail.ClinicID = _ClinicID;

        //                                if (oList[1] != null && Convert.ToString(oList[1]).Trim() != "")
        //                                {
        //                                    EOBPayment.Common.PaymentPatientLineNote oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

        //                                    oPaymentPatientLineNote.ClaimNo = 0;
        //                                    oPaymentPatientLineNote.EOBPaymentID = 0;
        //                                    oPaymentPatientLineNote.EOBID = 0;
        //                                    oPaymentPatientLineNote.EOBPaymentDetailID = 0;
        //                                    oPaymentPatientLineNote.BillingTransactionID = 0;
        //                                    oPaymentPatientLineNote.BillingTransactionDetailID = 0;
        //                                    oPaymentPatientLineNote.Code = "";
        //                                    oPaymentPatientLineNote.Description = Convert.ToString(oList[1]).Trim();
        //                                    oPaymentPatientLineNote.Amount = Convert.ToDecimal(oList[0]);
        //                                    if (oList[3] != null && oList[3].ToString().Trim() != "")
        //                                    {
        //                                        oPaymentPatientLineNote.IncludeOnPrint = Convert.ToBoolean(oList[3]);
        //                                    }
        //                                    oPaymentPatientLineNote.ClinicID = _ClinicID;
        //                                    oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientReserved;
        //                                    if (oList[2] != null && oList[2].ToString().Trim() != "")
        //                                    {
        //                                        oPaymentPatientLineNote.PaymentNoteSubType = (EOBPaymentSubType)Convert.ToInt32(oList[2]);
        //                                    }
        //                                    oPaymentPatientLineNote.HasData = true;

        //                                    oEOBPatientPaymentDetail.LineNotes.Add(oPaymentPatientLineNote);
        //                                    oPaymentPatientLineNote.Dispose();
        //                                }

        //                                oPaymentPatient.EOBPatientPaymentReserveLineDetail.Add(oEOBPatientPaymentDetail);
        //                                oEOBPatientPaymentDetail.Dispose();
        //                                #endregion

        //                                oCrItems[rPay].Description = "0";
        //                            }
        //                        }
        //                        #endregion
        //                    }
        //                }
        //            }
        //        }
        //        #endregion

        //        _EOBPaymentID = ogloEOBPaymentPatient.SavePatientCorrectionPayment(oPaymentPatient, out EOBPatientPaymentMasterLines);

        //        FillEOBPayments(_EOBPaymentID);

        //        _retPayId = _EOBPaymentID;
        //        _EOBPaymentID = 0;
        //        EOBPatientPaymentMasterLines.Clear();
        //        btnReserveRemaining.Tag = null;

        //        #region "Desing Grids"
        //        _IsFormLoading = true;
        //        rbPayType_Payment_CheckedChanged(null, null);
        //        _IsFormLoading = false;
        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (ogloEOBPaymentPatient != null) { ogloEOBPaymentPatient.Dispose(); };
        //        if (oPaymentPatient != null) { oPaymentPatient.Dispose(); };
        //        if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); };
        //        if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); };
        //        if (oEOBPatientPaymentCreditDetail != null) { oEOBPatientPaymentCreditDetail.Dispose(); };
        //    }
        //    return _retPayId;
        //}

        #endregion

        private bool SavePaymentValidation()
        {
            try
            {
                //If only special character is present the set to zero
                isclosecheck = false;
                try
                { Convert.ToDecimal(txtCheckAmount.Text.Trim()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null; 
                    decimal _amt = 0;
                    txtCheckAmount.Text = _amt.ToString("#0.00");
                }
                //
                //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  validating selected patient account
                if (oPatientControl.CmbSelectedPatientID == 0)
                {
                    MessageBox.Show("Please select the patient. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return false;
                }
                //End

                //if (oPatientControl == null || oPatientControl.PatientID <= 0)
                //{
                //    MessageBox.Show("Please the select the patient.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtPatientSearch.Select(); txtPatientSearch.Focus();
                //    return false;
                //}

                if (mskCloseDate.MaskCompleted == false)
                {
                    MessageBox.Show("Please enter the close date", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Focus();
                    mskCloseDate.Select();
                    return false;
                }

                gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select(); mskCloseDate.Focus();
                    if (ogloBilling != null)
                    {
                        ogloBilling.Dispose();
                        ogloBilling = null;
                    }

                    return false;
                }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }


                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(7))
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

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    object _retVal = null;
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + Convert.ToInt64(lblPaymentTray.Tag) + " and ISNULL(bIsActive,0) = 1 AND nClinicID = " + _ClinicID + "");
                    oDB.Disconnect();
                    oDB.Dispose();

                    if (_retVal == null || _retVal.ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Selected payment tray is inactive. Please select the another payment tray.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
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
                EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;

                if (_IsUseReserveEntry == false && _IsPaymentCorrectionMode == false)
                {
                    if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.Cash; }
                    if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.Check; }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                    { _EOBPaymentMode = EOBPaymentMode.EFT; }

                    if (_EOBPaymentMode == EOBPaymentMode.None)
                    {
                        MessageBox.Show("Select the payment mode", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbPayMode.Select();
                        cmbPayMode.Focus();
                        return false;
                    }
                    else if (_EOBPaymentMode == EOBPaymentMode.CreditCard)
                    {

                        if (mskCheckDate.MaskCompleted == false)
                        {
                            MessageBox.Show("Enter the " + _EOBPaymentMode.ToString().ToLower() + " date", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCheckDate.Select();
                            mskCheckDate.Focus();
                            return false;
                        }

                        if (cmbCardType == null || cmbCardType.Items.Count <= 0 || cmbCardType.Text.Trim() == "")
                        {
                            MessageBox.Show("Select the card type", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbCardType.Select();
                            cmbCardType.Focus();
                            return false;
                        }

                        mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mskCreditExpiryDate.Text != "")
                        {
                            if (mskCreditExpiryDate.MaskFull == false)
                            {
                                MessageBox.Show("Enter valid " + _EOBPaymentMode.ToString().ToLower() + " expiration date (MM/yy).", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskCreditExpiryDate.Select();
                                mskCreditExpiryDate.Focus();
                                return false;
                            }
                        }

                    }
                    else if (_EOBPaymentMode == EOBPaymentMode.Check)
                    {

                        if (txtCheckNumber.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString().ToLower() + " number", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCheckNumber.Text = "";
                            txtCheckNumber.Select();
                            txtCheckNumber.Focus();
                            return false;
                        }
                        //else
                        //{
                        //    EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
                        //    if (ogloEOBPaymentPatient.IsExistCheck(txtCheckNumber.Text.Trim()) == true)
                        //    {
                        //        MessageBox.Show("Payment for the check number already exists.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //        //txtCheckNumber.SelectAll();
                        //        //txtCheckNumber.Focus();
                        //        //return false;
                        //    }
                        //    ogloEOBPaymentPatient.Dispose();
                        //}

                        if (mskCheckDate.MaskCompleted == false)
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString().ToLower() + " date", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCheckDate.Select();
                            mskCheckDate.Focus();
                            return false;
                        }
                    }
                    else if (_EOBPaymentMode == EOBPaymentMode.EFT)
                    {
                        if (txtCheckNumber.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString() + " number", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCheckNumber.Select();
                            txtCheckNumber.Focus();
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

                decimal _ValidateAmt = 0;
                _ValidateAmt = Convert.ToDecimal(Convert.ToString(txtCheckRemaining.Text.Trim()));
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

                #region " Warning Message if the entered check number already exists "

                //if (_EOBPaymentMode == EOBPaymentMode.Check)
                //{
                //    EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
                //    if (ogloEOBPaymentPatient.IsExistCheck(txtCheckNumber.Text.Trim()) == true)
                //    {
                //        DialogResult _dlgCheck = DialogResult.None;
                //        _dlgCheck = MessageBox.Show("Payment for the check number already exists. Do you want to continue ?", _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                //        if (_dlgCheck == DialogResult.Cancel)
                //        { return false; }

                //    }
                //    ogloEOBPaymentPatient.Dispose();
                //}

                if (_EOBPaymentMode == EOBPaymentMode.Check)
                {
                    string _checkNo = "";
                    Int64 _checkDate = 0;
                    decimal _checkAmount = 0;
                    string _showCheckDate = "";

                    if (txtCheckNumber.Text.Trim() != "")
                    { _checkNo = txtCheckNumber.Text.Trim(); }

                    if (mskCheckDate.MaskCompleted == true)
                    {
                        _checkDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
                        _showCheckDate = mskCheckDate.Text;
                    }

                    if (txtCheckAmount.Text.Trim() != "")
                    { _checkAmount = Convert.ToDecimal(txtCheckAmount.Text.Trim()); }

                    if (_EOBPaymentID <= 0)
                    {
                        EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
                        if (ogloEOBPaymentPatient.IsExistCheck(_checkNo, _checkDate, _checkAmount) == true)
                        {
                            DialogResult _checkDlg = DialogResult.None;
                            string _message = "";
                            _message = " Same Check with Check#: " + _checkNo + ", Check Date: " + _showCheckDate + Environment.NewLine +  " and Check Amount: $" + _checkAmount.ToString("#0.00") + " " + Environment.NewLine + " already exists in the system." + Environment.NewLine + " Do you want to continue with save? ";
                            _checkDlg = MessageBox.Show(_message, _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                            if (_checkDlg == DialogResult.Cancel)
                            {
                                txtCheckNumber.SelectAll(); txtCheckNumber.Focus();
                                return false;
                            }
                        }
                        if (ogloEOBPaymentPatient != null) { ogloEOBPaymentPatient.Dispose(); }
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

                        if (oList != null && oList.Length == 6)
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

                //...Code added on 20100508 by Sagar Ghodke
                //...Code added to check the adjustment code is set if adjustment amount is entered
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
                //...End code add on 20100508 by Sagar Ghodke

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

        private void UpdatePayment()
        {
        }

        #endregion

        private void tls_btnSaveNClose_Click(object sender, EventArgs e)
        {
            //_IsClaimDateMessageHide = true;
            try
            {
                c1SinglePayment.FinishEditing();

                //tls_btnSave_Click(null, null);
                //this.Close();
                if (_IsOpenForModify == false)
                {
                    if (_IsAllDatesValid == true && SavePaymentValidation())
                    {
                        //...If only adjustment is made against charges and there is remaining amount
                        //...present then promp to put remaining in reserves or allocate the amount
                        //...and then allow save the payment
                        //solving sales force case - GLO2011-0011127   
                        if (_IsAdjustmentMode == true && Convert.ToDecimal(txtCheckRemaining.Text) > 0)
                        {
                            MessageBox.Show("Please use the remaining amount or put it into reserve to continue with next payment.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnReserveRemaining.Focus();
                            btnReserveRemaining.Select();
                        } //end
                        else
                        {
                            SavePayment();
                            this.Close();
                        }

                        //SavePayment(); 
                        //this.Close(); 
                    }
                }
                else
                { UpdatePayment(); }
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

        private DialogResult SetNewPayment()
        {
            DialogResult _dlgRst = DialogResult.None;

            if (_IsOpenForModify == false)
            {
                if (IsPaymentMade() == true)
                {
                    
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

                            #region " Previous Code "

                            // lblPayType.Enabled = true;
                            // cmbPayMode.Enabled = true;
                            // lblCheckNo.Enabled = true;
                            // txtCheckNumber.Enabled = true;
                            // // btnLoadCheck.Enabled = true;
                            //// btnRemoveCheck.Enabled = true;
                            // lblCheckDate.Enabled = true;
                            // mskCheckDate.Enabled = true;
                            // lblCardType.Enabled = true;
                            // cmbCardType.Enabled = true;
                            // lblCardAuthorizationNo.Enabled = true;
                            // txtCardAuthorizationNo.Enabled = true;
                            // lblExpiryDate.Enabled = true;
                            // mskCreditExpiryDate.Enabled = true;
                            // tls_btnReceipt.Visible = true;
                            // if (tls_btnDefaultReceipt.Tag != null && Convert.ToString(tls_btnDefaultReceipt.Tag).Trim().Length > 0)
                            // { tls_btnDefaultReceipt.Visible = true; }
                            // //dont put this clear method above

                            // ClearFormData();
                            // lblCheckAmount.Visible = true;
                            // txtCheckAmount.Visible = true;
                            // btnDistubuteAmount.Visible = true;
                            // btnUseReserve.Visible = true;



                            // _IsPaymentCorrectionMode = false;
                            // _IsUseReserveEntry = false;
                            // _IsLoadingFromExistingPayment = false;
                            // _IsLoadingFromExistingPaymentID = 0;
                            // txtPatientSearch.Enabled = false;

                            #endregion " Previous Code "

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
                            tls_btnReceipt.Visible = true;
                            if (tls_btnDefaultReceipt.Tag != null && Convert.ToString(tls_btnDefaultReceipt.Tag).Trim().Length > 0)
                            { tls_btnDefaultReceipt.Visible = true; }
                            //dont put this clear method above
                            ClearFormData();

                            lblCheckAmount.Visible = true;
                            txtCheckAmount.Visible = true;
                            btnDistubuteAmount.Visible = true;
                            btnUseReserve.Visible = true;
                            _IsPaymentCorrectionMode = false;
                            _IsUseReserveEntry = false;
                            _IsLoadingFromExistingPayment = false;
                            _IsLoadingFromExistingPaymentID = 0;
                            txtPatientSearch.Enabled = true;

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
                        tls_btnReceipt.Visible = true;
                        if (tls_btnDefaultReceipt.Tag != null && Convert.ToString(tls_btnDefaultReceipt.Tag).Trim().Length > 0)
                        { tls_btnDefaultReceipt.Visible = true; }
                        //dont put this clear method above
                        ClearFormData();

                        lblCheckAmount.Visible = true;
                        txtCheckAmount.Visible = true;
                        btnDistubuteAmount.Visible = true;
                        btnUseReserve.Visible = true;
                        _IsPaymentCorrectionMode = false;
                        _IsUseReserveEntry = false;
                        _IsLoadingFromExistingPayment = false;
                        _IsLoadingFromExistingPaymentID = 0;
                        txtPatientSearch.Enabled = true;

                        //FillBillingTransaction(_PatientID, 0, false);
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
                    // btnLoadCheck.Enabled = true;
                    // btnRemoveCheck.Enabled = true;
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

                    ////dont put this clear method above

                    ClearFormData();

                    lblCheckAmount.Visible = true;
                    txtCheckAmount.Visible = true;
                    btnDistubuteAmount.Visible = true;
                    btnUseReserve.Visible = true;
                    _IsPaymentCorrectionMode = false;
                    _IsUseReserveEntry = false;
                    _IsLoadingFromExistingPayment = false;
                    _IsLoadingFromExistingPaymentID = 0;
                    txtPatientSearch.Enabled = true;

                    //if (_PatientID > 0)
                    //{ FillBillingTransaction(_PatientID, 0, false); }
                }
            }
            return _dlgRst;
        }

        #region " Clear Form for new Payment "

        private bool IsPaymentMade()
        {
            try
            {
                _IsAdjustmentMode = false;

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


                                if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                         && (Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) > 0
                                         || Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) < 0)
                                       )
                                {
                                    if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                    {
                                        _IsAdjustmentMode = true;
                                        return true;
                                    }
                                    //else
                                    //{
                                    //    string _Msg = "";
                                    //    string _cptCode = "";
                                    //    if (c1SinglePayment.GetData(rIndex, COL_CPT_CODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)).Trim() != "")
                                    //    { _cptCode = Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)).Trim(); }
                                    //    _Msg = " Select adjustment code for Charge ('" + _cptCode + "') ";
                                    //    MessageBox.Show(_Msg, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    return false;
                                    //}
                                }
                            }
                        }
                    }

                    if ((txtCheckAmount.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckAmount.Text.Trim()) > 0)
                        || txtCheckRemaining.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckRemaining.Text.Trim()) > 0)
                    {
                        return true;
                    }
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

                                return true;
                            }

                            //if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                            //{
                            //    return true;
                            //}

                            //if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                            //   && (Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) > 0
                            //   || Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) < 0))
                            //{
                            //    return true;
                            //}

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
            try
            {
                rbPaySource_Personal.Checked = true;
                rbPaySource_Personal.Visible = false;
                rbPaySource_Insurance.Visible = false;

                //code moved to up by mahesh s on 18/may/2011.

                DesignPaymentGrid(c1SinglePayment);
                DesignPaymentGrid(c1SinglePaymentTotal);
                DesignPaymentGrid(c1MultiplePayment);
                DesignPaymentGrid(c1MultiplePaymentTotal);

                ////Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  Code Comented by subashish  filldetails
                if (_IsPatientAccountFeature)
                {
                    if (oPatientControl == null)
                    {                       
                        oPatientControl.FillDetails(_PatientID, gloStripControl.FormName.None, 0, false);
                        //Temp Comments
                        // oPatientControl.SetClaimNoSearch(false);
                    }
                }
                else
                {
                    if (oPatientControl != null)
                    {
                        // oPatientControl.FillDetails(_PatientID, gloPatientStripControl.FormName.None, 0, false); oPatientControl.SetClaimNoSearch(false);
                        oPatientControl.FillDetails(_PatientID, gloStripControl.FormName.None, 0, false);
                        //oPatientControl.SetClaimNoSearch(false);
                    }
                }

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

                FillPaymentTray();
                FillPaymentMode();
                FillPrintReceipt();
                FillCreditCards();

                //mskCloseDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                SetCloseDate();

                btnReserveRemaining.Tag = null;

                rbPayType_Payment_CheckedChanged(null, null);
                txtPatientSearch.Select(); txtPatientSearch.Focus();
                txtPayMstNotes.Text = "";
                chkPayMstIncludeNotes.Checked = false;

                txtPatientSearch.Enabled = true;

                lblPaymetNo.Text = "";
                EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
                lblPaymetNo.Text = ogloEOBPaymentPatient.GetPaymentPrefixNumber(_paymentPrefix).Trim();
                ogloEOBPaymentPatient.Dispose();


                if (_IsOpenForModify == true)
                {
                    FillPaymentTransaction(_PatientID, _mPaymentClaimNo, _mEOBPaymentID, _mEOBID, _mEOBPaymentDetailID);
                }
                else
                {
                    //If open for new payment then select payment type, bcz we are hiding payment type from this form, but its selected from dashboard like copay, advance 
                    //and if its just patient payment then other as payment type
                    if (_nEOBNewPaymentType == EOBPaymentSubType.Copay)
                    {
                        rbRecType_Copay.Checked = true;
                    }
                    else if (_nEOBNewPaymentType == EOBPaymentSubType.Advance)
                    {
                        rbRecType_Advance.Checked = true;
                    }
                    else
                    {
                        rbRecType_Other.Checked = true;
                    }
                }

                _IsLoadingFromExistingPayment = false;
                _IsLoadingFromExistingPaymentID = 0;

                if (_PatientID > 0)
                {
                    //FillBillingTransaction(_PatientID, 0, false); 


                    //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  displaying all patient claims of selected account

                    if (_IsPatientAccountFeature)
                    {

                        if (oPatientControl.CmbSelectedPatientID > 0)
                        {
                            FillBillingTransaction(_PatientID, 0, false);
                        }
                        else
                        {

                            Int64 nTempAccountPatientID = 0;
                            //  Get Active Patients list available in selected Account.
                            DataTable oTempTable = GetAccountPatients(this.nPAccountID);
                            //  Remove record for current selected patient
                            DataRow[] oTempRow = oTempTable.Select(string.Format("nPatientID = '{0}'", _PatientID));

                            Int64 nTemp1 = 0;
                            Int64 nTemp2 = 0;
                            Int64 nTemp3 = 0;
                            string sTemp4 = "";
                            string sTemp5 = "";

                            foreach (DataRow oRow in oTempRow)
                            {
                                nTemp1 = Convert.ToInt64(oRow[0].ToString());
                                nTemp2 = Convert.ToInt64(oRow[1].ToString());
                                nTemp3 = Convert.ToInt64(oRow[2].ToString());
                                sTemp4 = oRow[3].ToString();
                                sTemp5 = oRow[4].ToString();

                                oTempTable.Rows.Remove(oRow);
                            }

                            foreach (DataRow oRow in oTempRow)
                            {
                                DataRow oNewRow = oTempTable.NewRow();
                                oNewRow[0] = nTemp1;
                                oNewRow[1] = nTemp2;
                                oNewRow[2] = nTemp3;
                                oNewRow[3] = sTemp4;
                                oNewRow[4] = sTemp5;

                                oTempTable.Rows.Add(oNewRow);
                                oTempTable.AcceptChanges();

                            }

                            int i = 0;
                            Boolean bProcessOnce;
                            foreach (DataRow oRow in oTempTable.Rows)
                            {
                                bProcessOnce = false;
                                if (i == 0) { bProcessOnce = true; }

                                nTempAccountPatientID = Convert.ToInt64(oRow["nPatientID"].ToString());
                                if (nTempAccountPatientID > 0)
                                    FillBillingTransaction_PAF(nTempAccountPatientID, 0, false, bProcessOnce);

                                i++;
                            }

                        }
                        //End
                    }
                    else
                    {
                        FillBillingTransaction(_PatientID, 0, false);
                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        #endregion " Clear Form for new Payment "

        //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  Getting patients for a account
        private DataTable GetAccountPatients(Int64 nPAccountID)
        {
            DataTable oDataTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloSettings.AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _sqlQuery = "";
            try
            {

                oParameters.Clear();
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                _sqlQuery = "PA_Select_AccountPatients";
                oDB.Connect(false);
                oDB.Retrive(_sqlQuery, oParameters, out oDataTable);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Clear();
                    oParameters.Dispose();
                    oParameters = null;
                }
            }


            return oDataTable;
        }
        //End

        private void tls_btnNewCorrection_Click(object sender, EventArgs e)
        {
            c1SinglePayment.FinishEditing();

            if (_IsAllDatesValid == false) { return; }

            if (_IsOpenForModify == false)
            {
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
                            btnDistubuteAmount.Visible = false;
                            btnUseReserve.Visible = false;

                            tls_btnReceipt.Visible = false;
                            tls_btnDefaultReceipt.Visible = false;

                            _IsPaymentCorrectionMode = true;
                            _IsUseReserveEntry = false;
                            _IsLoadingFromExistingPayment = false;
                            _IsLoadingFromExistingPaymentID = 0;
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
                        btnDistubuteAmount.Visible = false;
                        btnUseReserve.Visible = false;
                        tls_btnReceipt.Visible = false;
                        tls_btnDefaultReceipt.Visible = false;

                        _IsPaymentCorrectionMode = true;
                        _IsUseReserveEntry = false;
                        _IsLoadingFromExistingPayment = false;
                        _IsLoadingFromExistingPaymentID = 0;
                        _IsAdjustmentMode = false;

                    }
                    else if (_dlgRst == DialogResult.Cancel)
                    { }
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
                    btnDistubuteAmount.Visible = false;
                    btnUseReserve.Visible = false;
                    tls_btnReceipt.Visible = false;
                    tls_btnDefaultReceipt.Visible = false;

                    _IsPaymentCorrectionMode = true;
                    _IsUseReserveEntry = false;
                    _IsLoadingFromExistingPayment = false;
                    _IsLoadingFromExistingPaymentID = 0;
                    _IsAdjustmentMode = false;
                }
            }
            AllowEditValidation();
        }

        private void tls_btnDefaultReceipt_Click(object sender, EventArgs e)
        {
            c1SinglePayment.FinishEditing();

            if (_IsOpenForModify == false)
            {
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
            }
        }

        private void tsb_ShowHideZeroBalance_Click(object sender, EventArgs e)
        {
            if (_IsAllDatesValid == false) { return; }
            //_IsClaimDateMessageHide = true;
            if (oPatientControl != null && oPatientControl.PatientID > 0)
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

                        // ---------------------------------------------
                        // Code commented to resolve an issue #4196, 
                        // Also added a function RefreshClaimList() to refresh the grid only
                        // ---------------------------------------------
                        //txtPatientSearch.Text = oPatientControl.PatientCode;
                        //oPatientControl.SearchPatient(txtPatientSearch.Text.Trim());
                        RefreshClaimList();
                    }
                }
                else
                {
                    if (tsb_ShowHideZeroBalance.Tag.ToString().Trim() == "Hide")
                    { SetShowZeroBalance(); }
                    else if (tsb_ShowHideZeroBalance.Tag.ToString().Trim() == "Show")
                    { SetHideZeroBalance(); }

                    // ---------------------------------------------
                    // Code commented to resolve an issue #4196, 
                    // Also added a function RefreshClaimList() to refresh the grid only
                    // ---------------------------------------------
                    //txtPatientSearch.Text = oPatientControl.PatientCode;
                    //oPatientControl.SearchPatient(txtPatientSearch.Text.Trim());
                    RefreshClaimList();
                }
            }
            AllowEditValidation();
        }

        #endregion

        #region "Form Control Events"
        
        private void txtCheckAmount_KeyDown(object sender, KeyEventArgs e)
        {
            //if (txtCheckAmount.Text.Trim() == "0.00")
            //{
            //    txtCheckAmount.Text = "";
            //}

            
        }

        private void txtCheckAmount_Leave(object sender, EventArgs e)
        {
            bool _isValidAmt = true;
            decimal _CheckAmount = 0;

            try
            {                
                try
                {
                    Convert.ToDecimal(txtCheckAmount.Text.Trim());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null; 
                    _isValidAmt = false;
                }

                if (_isValidAmt == true)
                {
                    if (txtCheckAmount.Text.Trim() != "")
                    { _CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }
                }
                
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

            //if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
            //{
            //    if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
            //    {
            //        e.Handled = true;
            //    }
            //    else
            //    {
            //        // e.Handled = true;
            //        //int _CurPos = txtCheckAmount.SelectionStart;
            //        //int _DecPos = txtCheckAmount.Text.IndexOf(".");
            //        //int _TotLen = txtCheckAmount.Text.Length;
            //        //int _DecLen = txtCheckAmount.Text.Substring(_DecPos + 1).Length; 
            //    }
            //}
            //else
            //    if (txtCheckAmount.Text.Contains(".") && e.KeyChar == Convert.ToChar(46))
            //    {

            //        e.Handled = true;
            //    }

            if (e.KeyChar == Convert.ToChar(13))
            {
                CalculateRemainingAmount();
                //txtCheckAmount.Select();
                //txtCheckAmount.Focus();
            }
        }

        private void txtPatientSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                if (oPatientControl != null)
                {
                    //oPatientControl.c1PatientDetails
                    ////oPatientControl.ClearControl();
                    ////oPatientControl.SearchPatient(txtPatientSearch.Text.Trim());

                    //Added by Subashish_b on 16/Mar /2011 (integration made on date-10/May/2011) for   Temp Comments

                    oPatientControl.SearchPatient(txtPatientSearch.Text.Trim());
                    txtPatientSearch.Text = oPatientControl.PatientCode.ToString();
                    //End
                }

                //if (oPatientControl.PatientID > 0) { MoveCursor(sender, null); }
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

        private void ValidateDate(object sender, CancelEventArgs e)
        {
            
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
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
                //btnLoadCheck.Visible = false;
                //btnRemoveCheck.Visible = false;
                txtCheckNumber.Text = "";
                mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                cmbCardType.SelectedIndex = -1;
                txtCardAuthorizationNo.Text = "";
                mskCreditExpiryDate.Text = "";
               // panel16.TabStop = false;
                pnlCredit.TabStop = false;

                EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;

                if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                {
                    _EOBPaymentMode = EOBPaymentMode.Cash;
                    lblCheckDate.Text = "         Date :";
                    //btnLoadCheck.Visible = false;
                   // btnRemoveCheck.Visible = false;
                    txtCheckNumber.Text = "";
                    lblCheckNo.Text = "   Ref.# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    lblCheckNo.Enabled = true;
                    txtCheckNumber.Enabled = true;

                    //panel16.Enabled = false;
                    pnlCredit.Enabled = false;
                }
                else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                {
                    _EOBPaymentMode = EOBPaymentMode.Check;

                    lblCheckNo.Text = "Check# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    //lblCheckNo.Location = new Point(25, 63);

                    lblCheckDate.Text = "Check Date :";
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    //lblCheckDate.Location = new Point(8, 88);

                    //panel16.Enabled = false;
                    pnlCredit.Enabled = false;
                    txtCheckNumber.MaxLength = 15;
                    //btnLoadCheck.Visible = true;
                   // btnRemoveCheck.Visible = true;
                }
                else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                {
                    _EOBPaymentMode = EOBPaymentMode.MoneyOrder;

                    lblCheckNo.Text = "    MO# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    //lblCheckNo.Location = new Point(25, 63);

                    lblCheckDate.Text = "    MO Date :";
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    //lblCheckDate.Location = new Point(8, 88);

                    //panel16.Enabled = false;
                    pnlCredit.Enabled = false;
                    txtCheckNumber.MaxLength = 15;
                }
                else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                {
                    _EOBPaymentMode = EOBPaymentMode.CreditCard;

                    lblCheckNo.Text = "  Card# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    //lblCheckNo.Location = new Point(25, 63);

                    lblCheckDate.Text = "         Date :";
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    //lblCheckDate.Location = new Point(8, 88);

                    //panel16.Enabled = true;
                    pnlCredit.Enabled = true;
                    txtCheckNumber.MaxLength = 4;
                    //panel16.TabStop = true;
                    pnlCredit.TabStop = true;
                }
                else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                {
                    _EOBPaymentMode = EOBPaymentMode.EFT;
                    lblCheckNo.Text = "   EFT# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                   // lblCheckNo.Location = new Point(25, 63);

                    lblCheckDate.Text = "   EFT Date :";
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    //lblCheckDate.Location = new Point(8, 88);

                   // panel16.Enabled = false;
                    pnlCredit.Enabled = false;
                    txtCheckNumber.MaxLength = 15;
                }

                if (_EOBPaymentMode == EOBPaymentMode.Check)
                {
                    mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }
            }
        }

        #endregion

        #region " Form Button Click "

        private void btnLoadCheck_Click(object sender, EventArgs e)
        {
            btnRemoveCheck_Click(null, null);

            frmBillingCheckDiff ofrmBillingCheckDiff = new frmBillingCheckDiff();
            ofrmBillingCheckDiff.ShowDialog(this);
            if (ofrmBillingCheckDiff._frmDlgRst == DialogResult.OK)
            {
                FillEOBPayments(ofrmBillingCheckDiff.EOBPaymentID);
                txtCheckNumber.Text = ofrmBillingCheckDiff.CheckNo;
                txtCheckAmount.Text = ofrmBillingCheckDiff.CheckAmount.ToString();
                mskCheckDate.Text = gloDateMaster.gloDate.DateAsDate(ofrmBillingCheckDiff.CheckDate).ToString("MM/dd/yyyy");
                txtCheckRemaining.Text = ofrmBillingCheckDiff.PendingAmount.ToString();
                lblShowRemaining.Text = ofrmBillingCheckDiff.PendingAmount.ToString();
                _EOBPaymentID = ofrmBillingCheckDiff.EOBPaymentID;


                EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(_databaseconnectionstring);
                DataTable _dtPayDtls = new DataTable();
                _dtPayDtls = ogloEOBPaymentInsurance.GetEOBPaymentDetails(_EOBPaymentID, EOBPaymentSign.Payment_Credit);

                if (_dtPayDtls != null && _dtPayDtls.Rows.Count > 0)
                {
                    EOBPatientPaymentMasterLines = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetails();
                    EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentDetail = null;

                    for (int i = 0; i < _dtPayDtls.Rows.Count; i++)
                    {
                        oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
                        oEOBPatientPaymentDetail.EOBPaymentID = Convert.ToInt64(_dtPayDtls.Rows[i]["nEOBPaymentID"]);
                        oEOBPatientPaymentDetail.EOBID = 0;// Convert.ToInt64(_dtPayDtls.Rows[i]["nEOBID"]);
                        oEOBPatientPaymentDetail.EOBDtlID = 0;// Convert.ToInt64(_dtPayDtls.Rows[i]["nEOBDtlID"]);
                        oEOBPatientPaymentDetail.EOBPaymentDetailID = Convert.ToInt64(_dtPayDtls.Rows[i]["nEOBPaymentDetailID"]);
                        oEOBPatientPaymentDetail.Amount = Convert.ToDecimal(ofrmBillingCheckDiff.CheckAmount);
                        oEOBPatientPaymentDetail.PaymentType = ((EOBPaymentType)Convert.ToInt32(_dtPayDtls.Rows[i]["nPaymentType"]));
                        oEOBPatientPaymentDetail.PaymentSubType = ((EOBPaymentSubType)Convert.ToInt32(_dtPayDtls.Rows[i]["nPaymentSubType"]));

                        EOBPatientPaymentMasterLines.Add(oEOBPatientPaymentDetail);
                    }
                    oEOBPatientPaymentDetail.Dispose();
                }

                ogloEOBPaymentInsurance.Dispose();
                _dtPayDtls.Dispose();
            }
            ofrmBillingCheckDiff.Dispose();
        }

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
                lblPayer.Text = "";
                lblPayer.Tag = null;
                lblPayerDisplay.Text = "";
                rbPaySource_Insurance.Checked = true;

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
                DesignPaymentGrid(c1MultiplePayment);
                DesignPaymentGrid(c1MultiplePaymentTotal);

                if (oPatientControl != null)
                {
                    //Modified by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  Temp Comments
                    // oPatientControl.ClearControl(true);
                    //oPatientControl.Select(); oPatientControl.Focus(); oPatientControl.SelectSearchBox();
                    //End
                }

                _PatientID = 0;
                _mPaymentClaimNo = 0;
                _mPaymentClaimNo = 0;
                _mEOBPaymentID = 0;
                _mEOBID = 0;
                _mEOBPaymentDetailID = 0; 
                _EOBPaymentID = 0; // it is used to hold master payement id for multiple claim payment
                if (EOBPatientPaymentMasterLines != null) { EOBPatientPaymentMasterLines.Clear(); }
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
                        for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                        {
                            _PatLineAmount = 0;

                            if (_PatBalAmount > 0)
                            {
                                if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                {
                                    if (c1SinglePayment.Rows[i].AllowEditing == true)
                                    {
                                        if (_SelectFirstIndex <= 0) { _SelectFirstIndex = i; }

                                        if (c1SinglePayment.GetData(i, COL_PAT_DUE) != null && c1SinglePayment.GetData(i, COL_PAT_DUE).ToString().Trim() != "")
                                        {
                                            _PatLineBalAmount = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAT_DUE).ToString());
                                        }
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

        private void btnSetupJournal_Click(object sender, EventArgs e)
        {
            try
            {
                frmSetupCloseDayJournals ofrmSetupCloseDayJournals = new frmSetupCloseDayJournals(0, _databaseconnectionstring);
                ofrmSetupCloseDayJournals.StartPosition = FormStartPosition.CenterScreen;
                ofrmSetupCloseDayJournals.ShowDialog(this);
                ofrmSetupCloseDayJournals.Dispose();
                FillPaymentTray();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void btnModifyJournal_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null; // new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object _retVal = null;

            try
            {
                Int64 _TrayID = 0;
                if (lblPaymentTray.Tag != null && lblPaymentTray.Tag.ToString().Trim().Length > 0)
                {
                    if (lblPaymentTray.Tag != null && lblPaymentTray.Text.ToString().Trim() != "")
                    {
                        _TrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());

                        frmSetupCloseDayJournals ofrmSetupCloseDayJournals = new frmSetupCloseDayJournals(_TrayID, _databaseconnectionstring);
                        ofrmSetupCloseDayJournals.StartPosition = FormStartPosition.CenterScreen;
                        ofrmSetupCloseDayJournals.ShowDialog(this);
                        ofrmSetupCloseDayJournals.Dispose();
                        FillPaymentTray();

                        oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        oDB.Connect(false);
                        _retVal = new object();
                        _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _TrayID + " AND nClinicID = " + _ClinicID + "");
                        if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                        {
                            lblPaymentTray.Text = _retVal.ToString(); ;
                            lblPaymentTray.Tag = _TrayID;
                        }
                        oDB.Disconnect();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }

            }
        }

        private void btnTraySelection_Click(object sender, EventArgs e)
        {
            try
            {
                frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(_databaseconnectionstring);
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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Added by Pankaj on 11022010
        /// Validation for Reserve Remaining Button
        /// </summary>
        /// <returns></returns>
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
            // Validation added for ReserveRemaining Button
            //if (!IsValidForReserveRemaining())
            //{
            //    return;
            //}

            if (_IsOpenForModify == false)
            {
                //0 ReserveAmount 
                //1 ReserveNote 
                //2 ReserveSubType 
                //3 ReserveNoteOnPrint 
                //4 CPT
                //5 ICD9

                string _AdvCPT = "";
                string _AdvICD9 = "";
                gloGeneralItem.gloItems _AdvList = null; // new gloGeneralItem.gloItems();

                if (txtCheckRemaining.Text != null && txtCheckRemaining.Text.Trim().Length > 0)
                {
                    //if (Convert.ToDecimal(Convert.ToString(txtCheckRemaining.Text)) >= 0)
                    {
                        string[] oList = null;
                        if (btnReserveRemaining.Tag != null)
                        {
                            oList = btnReserveRemaining.Tag.ToString().Split('~');
                        }

                        frmPaymentReserveRemaning ofrmPaymentReserveRemaning = new frmPaymentReserveRemaning(_databaseconnectionstring);
                        if (oList != null && oList.Length == 6)
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

                                    //...Code added on 20100507 by  Sagar Ghodke 

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

                                    //...End code added on 20100507 by  Sagar Ghodke 

                                }
                            }
                            else
                            {
                                if (oList != null && oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                                {
                                    if (Convert.ToDecimal(Convert.ToString(oList[0])) > 0)
                                    {
                                        ofrmPaymentReserveRemaning.ReserveAmount = Convert.ToDecimal(Convert.ToString(oList[0]));

                                        //...Code added on 20100507 by  Sagar Ghodke 

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

                                        //...End code added on 20100507 by  Sagar Ghodke 


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
                            ofrmPaymentReserveRemaning.ReserveSubType = (EOBPaymentSubType)Convert.ToInt32(oList[2]);
                        }
                        else
                        {
                            if (rbRecType_Copay.Checked == true)
                            {
                                ofrmPaymentReserveRemaning.ReserveSubType = EOBPaymentSubType.Copay;
                            }
                            else if (rbRecType_Advance.Checked == true)
                            {
                                ofrmPaymentReserveRemaning.ReserveSubType = EOBPaymentSubType.Advance;
                            }
                            else
                            {
                                ofrmPaymentReserveRemaning.ReserveSubType = EOBPaymentSubType.Other;
                            }
                        }
                        if (oList != null && oList.Length == 6)
                        {
                            if (oList != null && oList[4] != null && Convert.ToString(oList[4]).Trim() != "")
                            {
                                //if (_AdvList != null)
                                //{
                                //    _AdvList.Clear();
                                //    _AdvList.Dispose();
                                //    _AdvList = null;
                                //}
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
                                //_AdvList.Dispose();
                            }
                        }
                        if (oList != null && oList.Length == 6)
                        {
                            if (oList != null && oList[5] != null && Convert.ToString(oList[5]).Trim() != "")
                            {
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


                        
                        //if (ofrmPaymentReserveRemaning.ReserveAmount > 0)
                        //{
                            ofrmPaymentReserveRemaning.ShowInTaskbar = false;
                            ofrmPaymentReserveRemaning.StartPosition = FormStartPosition.CenterScreen;
                            ofrmPaymentReserveRemaning.ShowDialog(this);
                            if (ofrmPaymentReserveRemaning.DialogResult == DialogResult.OK)
                            {
                                _AdvCPT = "";
                                _AdvICD9 = "";
                                if (ofrmPaymentReserveRemaning.oCPTList != null && ofrmPaymentReserveRemaning.oCPTList.Count > 0)
                                {
                                  //  _AdvList = new gloGeneralItem.gloItems();
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
                                    //_AdvList = new gloGeneralItem.gloItems();
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
                                EOBPaymentSubType reserveType = ofrmPaymentReserveRemaning.ReserveSubType;
                                btnReserveRemaining.Tag = ofrmPaymentReserveRemaning.ReserveAmount + "~" + ofrmPaymentReserveRemaning.ReserveNote + "~" + reserveType.GetHashCode() + "~" + ofrmPaymentReserveRemaning.ReserveNoteOnPrint + "~" + _AdvCPT + "~" + _AdvICD9;
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
            }

        }

        private void btnUseReserve_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (_PatientID > 0)
                {
                    DialogResult _dlgRst = DialogResult.None;
                    _dlgRst = SetNewPayment();

                    if (_dlgRst != DialogResult.Cancel)
                    {
                        //frmPaymentUseReserve ofrmPaymentUseReserve = new frmPaymentUseReserve(_databaseconnectionstring, _PatientID);
                        frmPaymentUseReserve ofrmPaymentUseReserve;

                        //Added by Subashish_b on 21/Feb /2011 (integration made on date-10/May/2011) for  calling method depending on the status of the PAF status

                        ofrmPaymentUseReserve = new frmPaymentUseReserve(_databaseconnectionstring, _PatientID, this.nPAccountID);
                        //if (_IsPatientAccountFeature)
                        //{
                        //    //ofrmPaymentUseReserve = new frmPaymentUseReserve(_databaseconnectionstring, _PatientID, oPatientControl.CmbSelectedAccountID);
                        //    ofrmPaymentUseReserve = new frmPaymentUseReserve(_databaseconnectionstring, _PatientID, this.nPAccountID);
                        //}
                        //else
                        //{
                        //    ofrmPaymentUseReserve = new frmPaymentUseReserve(_databaseconnectionstring, _PatientID);
                        //}
                        //End
                        if (mskCloseDate.MaskCompleted == true)
                        {
                            ofrmPaymentUseReserve.CloseDate = Convert.ToDateTime(mskCloseDate.Text);
                        }

                        ofrmPaymentUseReserve.CloseDayTray = lblPaymentTray.Text.Trim();
                        ofrmPaymentUseReserve.ShowDialog(this);
                        if (ofrmPaymentUseReserve.DialogResult == DialogResult.OK)
                        {
                            btnUseReserve.Visible = false;
                            btnClearReserve.Visible = true;
                            btnReserveRemaining.Visible = false;
                            txtCheckAmount.ReadOnly = true;
                            txtCheckAmount.BackColor = Color.White;
                            decimal selectedUserReserveAmount = ofrmPaymentUseReserve.SelectedUseReserveAmount;
                            txtCheckAmount.Text = selectedUserReserveAmount.ToString();
                            btnUseReserve.Tag = ofrmPaymentUseReserve.oSeletedReserveItems;

                            tls_btnReceipt.Enabled = false;
                            tls_btnDefaultReceipt.Enabled = false;

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
                            //_IsClaimDateMessageHide = true;
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
                 
            }
        }

        private void btnClearReserve_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear reserve amount?",_messageboxcaption ,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
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
                            if ((ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
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

        //private void mnuPayment_ReasonCode_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
        //        {
        //            if (c1SinglePayment.RowSel > 0)
        //            {
        //                if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
        //                {
        //                    C1.Win.C1FlexGrid.RowColEventArgs evt = new RowColEventArgs(c1SinglePayment.RowSel, c1SinglePayment.ColSel);
        //                    c1SinglePayment_CellButtonClick(null, evt);
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Please select serviceline to add reason code", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        #endregion " Short Cut Menu Click Events "

        #region "Payment Collect and Apply Changed"
       private void rbPayType_Payment_CheckedChanged(object sender, EventArgs e)
        {
            lblCheckAmount.Visible = true;
            txtCheckAmount.Visible = true;

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
            DesignPaymentGrid(c1MultiplePayment);
            DesignPaymentGrid(c1MultiplePaymentTotal);
            pnlMultiplePayment.Visible = false;
            pnlSinglePayment.Visible = true;
            mskCheckDate.Text = "";
            if (cmbPayMode.SelectedIndex >= 0)
            {
                if (cmbPayMode.Text  == PaymentMode.Check.ToString())
                {
                    mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }
            }
                
            txtCheckNumber.Text = "";
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



            //Temp Comments subashish old patient banner function
            // if (oPatientControl != null) { oPatientControl.SetClaimNoSearch(false); }
     
        }

        private void rbPayType_Refund_CheckedChanged(object sender, EventArgs e)
        {
            lblCheckAmount.Visible = true;
            txtCheckAmount.Visible = true;

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
            DesignPaymentGrid(c1MultiplePayment);
            DesignPaymentGrid(c1MultiplePaymentTotal);
            pnlMultiplePayment.Visible = false;
            pnlSinglePayment.Visible = true;

            txtCheckNumber.Text = "";
            txtCheckAmount.Text = "";
            txtCheckRemaining.Text = "";
            lblShowRemaining.Text = "";
            mskCheckDate.Text = "";
            if (cmbPayMode.SelectedIndex >= 0)
            {
                if (cmbPayMode.Text == PaymentMode.Check.ToString())
                {
                    mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }
            }

            //Code commented by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  Temp Comments
            //if (oPatientControl != null) { oPatientControl.SetClaimNoSearch(false); }
            //End
        }

        private void rbPayType_ExitingPayment_CheckedChanged(object sender, EventArgs e)
        {
            lblCheckAmount.Visible = true;
            txtCheckAmount.Visible = true;
            lblCheckRemaining.Visible = true;
            lblShowRemaining.Visible = true; //txtCheckRemaining.Visible = true;

            btnDistubuteAmount.Visible = true;
            btnReserveRemaining.Visible = true;
            btnUseReserve.Visible = true;
            btnClearReserve.Visible = false;
            txtCheckAmount.ReadOnly = false;

            DesignPaymentGrid(c1SinglePayment);
            DesignPaymentGrid(c1SinglePaymentTotal);
            DesignPaymentGrid(c1MultiplePayment);
            DesignPaymentGrid(c1MultiplePaymentTotal);
            pnlMultiplePayment.Visible = false;
            pnlSinglePayment.Visible = true;

            txtCheckNumber.Text = "";
            txtCheckAmount.Text = "";
            lblShowRemaining.Text = ""; //txtCheckRemaining.Text = "";
            mskCheckDate.Text = "";
            if (cmbPayMode.SelectedIndex >= 0)
            {
                if (cmbPayMode.Text == PaymentMode.Check.ToString())
                {
                    mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }
            }

            //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  Temp Comments
            // if (oPatientControl != null) { oPatientControl.SetClaimNoSearch(false); }
            //End
            
        }
        #endregion

        private void mskCloseDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void tls_btnCharge_Click(object sender, EventArgs e)
        {
            if (_IsAllDatesValid == true)
            {
                //_IsClaimDateMessageHide = true;
                OpenModifyCharges();
                AllowEditValidation();
            }
        }

        private void c1SinglePayment_DoubleClick(object sender, EventArgs e)
        {
            OpenModifyCharges();
            AllowEditValidation();
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
                        }
                    }
                    else
                    {
                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TRACK_TRN_ID) != null && c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TRACK_TRN_ID).ToString().Trim() != "")
                        {
                            _transactionId = Convert.ToInt64(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TRACK_TRN_ID));
                        }
                    }

                }

                #endregion


                if (_transactionId > 0)
                {
                    #region " Modify Charges Claim Code "

                    //bool _isTransactionOpen = false;
                    //string _recordMachineId = "";
                    //Int64 _recordUserId = 0;

                    //gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                    //_isTransactionOpen = ogloBilling.IsRecordOpen(_transactionId, out _recordMachineId, out _recordUserId);
                    //ogloBilling.ShowModifyCharges(oPatientControl.PatientID, _transactionId);

                    //ogloBilling.Dispose();



                    //frmBillingModifyCharges ofrmBillingModifyCharges = frmBillingModifyCharges.GetInstance(oPatientControl.PatientID, _transactionId, false, _databaseconnectionstring, string.Empty);
                    //ofrmBillingModifyCharges.WindowState = FormWindowState.Maximized;
                    //ofrmBillingModifyCharges.ShowDialog();


                    //// If changes has been modified then only refresh the claim else skip
                    //if (ofrmBillingModifyCharges.IsModified)
                    //{
                    //    //frmPaymentInsurace_Load(null, null);
                    //    RefreshFormData();
                    //}

                    //------------------------------------------------------
                    Boolean _IsModified = false;

                    gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                    _IsModified = ogloBilling.ShowModifyCharges(oPatientControl.PatientID, _transactionId, this);
                    ogloBilling.Dispose();

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

        private void lblPaymentTray_MouseMove(object sender, MouseEventArgs e)
        {
            //try
            //{

            //    label = (Label)sender;

            //    if (lblPaymentTray.Text != null && lblPaymentTray.Text != "")
            //    {
            //        if (getWidthofListItems(Convert.ToString(lblPaymentTray.Text), lblPaymentTray) >= lblPaymentTray.Width - 20)
            //        {
            //            //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
            //            tooltip_Billing.SetToolTip(lblPaymentTray, lblPaymentTray.Text);
            //        }
            //        else
            //        {
            //            this.tooltip_Billing.Hide(lblPaymentTray);
            //        }
            //    }
            //    else
            //    {
            //        this.tooltip_Billing.Hide(lblPaymentTray);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            //    Ex = null;
            //}
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

        private void lblPaymentTray_MouseLeave(object sender, EventArgs e)
        {
            //this.tooltip_Billing.Hide(lblPaymentTray);
        }

        private void c1SinglePayment_Click(object sender, EventArgs e)
        {
            //_IsC1SinglePaymentClicked = true;
            //_IsClaimDateMessageHide = true;
        }

        private void mskCloseDate_Validated(object sender, EventArgs e)
        {
            if(isclosecheck == false)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                
                if (mskCloseDate.Text.Length > 0)
                {
                    //if(_IsFormLoaded == true)
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
                    //if(_IsFormLoaded == true)
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
                _IsClaimDateMessageShown = false ;
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

     
    }
}
