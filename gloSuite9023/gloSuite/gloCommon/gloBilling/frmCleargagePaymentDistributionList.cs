using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public enum CGTransactionType
    { 
        COPAY=1,
        OTHER=2,
        DECLINE=3,
        REJECTFEE=4,
        LATEFEE=5

    }
    public partial class frmCleargagePaymentDistributionList : gloGlobal.Common.TriarqFormWithFocusListner
    {
        #region " Declarations "

        private string _messageBoxCaption = String.Empty;
        ClsCleargagePaymentPosting oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        //   private bool _IsPatientAccountFeature = false;
        Font Font_CellStyle = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //   Font Font_Template = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));



        DataTable dtPaymentList = null;
        DataTable dtPAccountID = null;
        Int64 nPAccountID = 0;
        public string sAction = string.Empty;
        public string sPaymentMethod = string.Empty;
        #endregion " Declarations "

        #region "Column Declaration For c1Cleargage"

        const int COL_PACCOUNTID = 0;
        const int COL_GUARANTORID = 1;
        const int COL_ACCOUNT_PATIENT_ID = 2;
        const int COL_CLEARGAGEFILEID = 3;
        const int COL_BILLING_TRANSACTON_ID = 4;
        const int COL_BILLING_TRANSACTON_DETAILID = 5;
        const int COL_BILLING_TRANSACTON_LINENO = 6;
        const int COL_PATIENTID = 7;

        const int COL_ACCOUNTNO = 8;
        const int COL_PATIENTNAME = 9;
        const int COL_PAYMENTMETHOD = 10;
        const int COL_ACTION = 11;
        const int COL_REFERENCENUMBER = 12;
        const int COL_ENCOUNTERID = 13;
        const int COL_CHECKAMOUNT = 14;
        const int COL_TOTALAMOUNT =15;
        const int COL_AVAILABLEAMOUNT = 16;
        const int COL_FROMDOS = 17;
        const int COL_CLAIM_NUM = 18;
        const int COL_DOS_FROM = 19;
        const int COL_CPT_CODE = 20;
        const int COL_MODIFIER = 21;
        const int COL_TOTAL_CHARGE = 22;
        const int COL_TOT_BAL_AMT = 23;
        const int COL_PREV_PAID = 24;
        const int COL_PAT_DUE =25;
        const int COL_DISTRIBUTE_PAYMENT = 26;

        const int COL_RESERVE_CLOSE_DATE = 27;
        const int COL_DOS_TO = 28;
        const int COL_CLAIM_DATE = 29; //ClaimFromDate
        const int COL_CLAIM_CLOSE_DATE = 30;
        const int COL_FACILITY_TYPE = 31;
        const int COL_CLAIM_NO = 32;
        const int COL_CPT_DESCRIPTON = 33;
        const int COL_CHARGE = 34;
        const int COL_UNIT = 35;
        const int COL_ALLOWED = 36;
        const int COL_PROVIDERID = 37;
        const int COL_TRANSACTION_LINESTATUS = 38;
        const int COL_SENDTOFLAG = 39;
        
        const int COL_PREV_ADJ = 40;
        const int COL_PREV_PAT_ADJ = 41;
        const int COL_INS_DUE = 42;
        const int COL_BadDebt_DUE = 43;
        const int COL_PREV_PAT_PAID_AMT = 44;
        const int COL_SPLIT_CLAIM_NO = 45;
        const int COL_TRACK_TRN_ID = 46;
        const int COL_TRACK_TRN_DTL_ID = 47;
        const int COL_TRACK_SUB_CLAIM_NO = 48;
        const int COL_TRACK_IS_HOLD = 49;
        const int COL_TRACK_HOLD_INFO = 50;
        const int COL_TRACK_RES_PARTY = 51;
        const int COL_NON_SERVICECODE = 52;
        
        const int COL_ADJUSTMENT_CODE = 53;
        const int COL_ADJUSTMENT_AMT = 54;
        const int COL_ADJUSTMENT_DESC = 55;
        //const int COL_ENCOUNTERID = 57;
        
        const int COL_DISTINCTACCOUNTNO = 56;
        
        const int COL_PAYMENTPLANID=57;
        const int COL_CG_TRANSACTIONID=58;
        const int COL_CG_ORIGINALTRANSACTIONID=59;
        const int COL_TIMESTAMP = 60;
        const int COL_SERVICEDATE = 61;
        const int COL_BRANCHID = 62;
        const int COL_CLIENTID = 63;
        const int COL_ORIGIN = 64;
        const int COL_USER = 65;
        const int COL_TxnTYPE = 66;
        //const int COL_REFERENCENUMBER = 68;
        const int COL_APPROVALCODE = 67;
        const int COL_NATIONALPROVIDERID=68;
        const int COL_ENTRYMETHOD = 69;
        const int COL_ACCOUNTTYPE = 70;
        const int COL_CG_ACCOUNTNUMBER = 71;
        const int COL_ACCOUNTNAME = 72;
        const int COL_LINESTATUS = 73;
        const int COL_PATIENTCODE = 74;
        const int COL_R_ENCOUNTERID = 75;  //Reserve Encounter ID
        const int COL_COLCOUNT = 76;
        int CleargagelistRowCount = 0;
        #endregion
        public bool IsCallingFromByAcc = false;
        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        private Int64 _nCleargageFileID = 0;

        public frmCleargagePaymentDistributionList()
        {
            try
            {

                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _ClinicID = 0; }
                }
                else
                { _ClinicID = 0; }
                InitializeComponent();

                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _messageBoxCaption = "gloPM";
                    }
                }
                else
                { _messageBoxCaption = "gloPM"; }

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #region "Constructor"

        public frmCleargagePaymentDistributionList(Int64 nCleargageFileID)
        {
            _nCleargageFileID = nCleargageFileID;
            try
            {

                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _ClinicID = 0; }
                }
                else
                { _ClinicID = 0; }

                InitializeComponent();
                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _messageBoxCaption = "gloPM";
                    }
                }
                else
                { _messageBoxCaption = "gloPM"; }

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }


        #endregion

        private void frmCleargagePaymentDistributionList_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCleargagePaymentList();
                if (sAction == Convert.ToString(Actions.ADJUSTMENT))
                {
                    label4.Text = "Cleargage Adjustment Distribution Work List";
                    this.Text = "Cleargage Adjustment Distribution List";
                    tsb_AutoDistributeCleargagePayment.Text = "&Auto Distribute Adjustment";
                    tsb_AutoDistributeCleargagePayment.ToolTipText = "Auto Distribute Cleargage Adjustment";
                    tsb_AutoDistributeCleargagePayment.Image = Properties.Resources.AutoDistributeAdjustment;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Open, "Cleargage Discount Posting list open", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);
                }
                else if (sAction == Convert.ToString(Actions.DISCOUNT))
                {
                    label4.Text = "Cleargage Discount Distribution Work List";
                    this.Text = "Cleargage Discount Distribution List";
                    tsb_AutoDistributeCleargagePayment.Text = "&Auto Distribute Discount";
                    tsb_AutoDistributeCleargagePayment.ToolTipText = "Auto Distribute Cleargage Discount";
                    tsb_AutoDistributeCleargagePayment.Image = Properties.Resources.AutoDistributeDiscount;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Open, "Cleargage Discount Posting list open", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);
                }
                else if (sAction == Convert.ToString(Actions.PAYMENT))
                {
                    tsb_AutoDistributeCleargagePayment.Image = Properties.Resources.AutoDistributePayment;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Open, "Cleargage Payment Posting list open", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Open, "Exception occured while Cleargage Payment Posting list open: "+ ex.ToString(), 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Failure,SoftwareComponent.gloPM,true);
            }
            finally
            {
            }
        }

        private void tsb_close_Click(object sender, EventArgs e)
        {
            try
            {
                C1Cleargage.Select();
                this.Close();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Close, "Cleargage Payment posting list close", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Close, "Exception occured while Cleargage Payment posting list close : "+ex.ToString(), 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Failure,SoftwareComponent.gloPM,true);
            }
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            //C1Cleargage.Select();
            C1Cleargage.Select(0, COL_ACCOUNTNO);
            if (rb_ShowAll.Checked == true)
            {
                LoadCleargagePaymentList();
            }
            else
            {
                LoadCleargagePaymentOneByOne();
            }

        }

        private void frmCleargagePaymentDistributionList_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                RemoveGotFocusListener(this);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Close, "Cleargage Payment posting list close", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Close, "Exception occured while Cleargage Payment posting list close : "+ex.ToString(), 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Failure,SoftwareComponent.gloPM,true);
            }
        }

        #region "Auto Distribution Payment Amount"



        private void C1Cleargage_CellChanged(object sender, RowColEventArgs e)
        {
            try
            {
                if (C1Cleargage.Rows.Count > 1)
                {
                    if (sAction == Convert.ToString(Actions.ADJUSTMENT))// && sPaymentMethod == "")
                    {
                        #region Validate Adjustment Amount

                        if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT)).Trim() != "")
                        {
                            decimal _CurrentAdjusmentAmt = 0;
                            decimal _TotalCharge = 0;
                            decimal _PrevAdjustment = 0;
                            decimal _CurrentPayment = 0;
                            decimal _PrevPayment = 0;
                            decimal _PrevTotalAdjustment = 0;

                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_TOTAL_CHARGE) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_TOTAL_CHARGE)).Trim() != "")
                            {
                                _TotalCharge = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_TOTAL_CHARGE));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAT_ADJ) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAT_ADJ)).Trim() != "")
                            {
                                _PrevAdjustment = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAT_ADJ));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_DISTRIBUTE_PAYMENT) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_DISTRIBUTE_PAYMENT)).Trim() != "")
                            {
                                _CurrentPayment = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_DISTRIBUTE_PAYMENT));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAID) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAID)).Trim() != "")
                            {
                                _PrevPayment = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAID));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT)).Trim() != "")
                            {
                                _CurrentAdjusmentAmt = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_ADJ) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_ADJ)).Trim() != "")
                            {
                                _PrevTotalAdjustment = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_ADJ));
                            }

                            if (_CurrentAdjusmentAmt > (_TotalCharge - _PrevTotalAdjustment - _CurrentPayment - _PrevPayment))
                            {
                                MessageBox.Show("Adjustment amount cannot be greater than net charge.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                try
                                {
                                    this.C1Cleargage.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                    C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT, null);
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                    ex = null;
                                }
                                finally
                                {
                                    this.C1Cleargage.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                }
                            }
                        }
                        #endregion

                        #region Validate Adjustment Code
                        if (e.Col == COL_ADJUSTMENT_CODE)
                        {
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE)).Trim() != "")
                            {
                                string[] AdjCodeDesc = null;
                                string _adjstr = "";
                                _adjstr = Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
                                bool _isValidCOde = false;
                                CellStyle cs = C1Cleargage.GetCellStyle(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE);

                                if (_adjstr != "")
                                {
                                    _adjstr = _adjstr = Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE)).Trim();
                                    _isValidCOde = cs.ComboList.Contains(_adjstr);
                                }

                                try
                                {
                                    this.C1Cleargage.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);

                                    if (_isValidCOde == true)
                                    {
                                        AdjCodeDesc = _adjstr.Split('-');
                                        C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE, AdjCodeDesc[0]);
                                        C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_DESC, AdjCodeDesc[1]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please select a valid code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE, null);
                                        C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_DESC, null);
                                        C1Cleargage.Focus();
                                        C1Cleargage.Select(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE, true);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                                }
                                finally
                                {
                                    this.C1Cleargage.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                }


                            }
                        }

                        #endregion


                        for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                        {
                            if (e.Col == COL_ADJUSTMENT_AMT && Convert.ToInt64(C1Cleargage.GetData(i, COL_PACCOUNTID)) == Convert.ToInt64(C1Cleargage.GetData(e.Row, COL_PACCOUNTID)))
                            {
                                if ((Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) == "" && Convert.ToString(C1Cleargage.GetData(e.Row, COL_DOS_FROM)) == "") || (Convert.ToString(C1Cleargage.GetData(e.Row, COL_DOS_FROM)) == Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS))))
                                {
                                    decimal dDistributedAjustment = 0;
                                    decimal dLineamt = 0;
                                    decimal dDistributeAdj = Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_ADJUSTMENT_AMT));

                                    if (Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_PAT_DUE)) > 0)
                                    {
                                        dLineamt = Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_PAT_DUE));
                                    }


                                    #region "Validating Manually Distributed  Amount"

                                    if (dDistributeAdj > dLineamt)
                                    {
                                        MessageBox.Show("Distributed payment amount exceeds due.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        C1Cleargage.SetData(e.Row, COL_ADJUSTMENT_AMT, Convert.ToDecimal(0));

                                        #region "Calculate Available Adustment amount"


                                        dDistributedAjustment = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAjustment > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAjustment);
                                        }
                                        else if (dDistributedAjustment == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                        }


                                        #endregion

                                        return;
                                    }
                                    else if (dDistributeAdj == 0)
                                    {
                                        #region "Calculate Available Amount"

                                        dDistributedAjustment = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAjustment > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAjustment);
                                        }
                                        else if (dDistributedAjustment == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                        }

                                        #endregion

                                        return;
                                    }
                                    else
                                    {
                                        dDistributedAjustment = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAjustment > Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)))
                                        {
                                            MessageBox.Show("Distributed adjustment amount exceeds available amount,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            C1Cleargage.SetData(e.Row, COL_ADJUSTMENT_AMT, Convert.ToDecimal(0));

                                            #region "Calculate Available Payment Amount"

                                            dDistributedAjustment = CalculateAvailableAmount(sender, e, i);
                                            if (dDistributedAjustment > 0)
                                            {
                                                C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAjustment);
                                            }
                                            else if (dDistributedAjustment == 0)
                                            {
                                                C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                            }

                                            #endregion

                                            return;
                                        }


                                    }

                                    #endregion

                                    if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) != Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)))
                                    {
                                        #region "Calculate Available amount"

                                        dDistributedAjustment = CalculateAvailableAmount(sender, e, i);

                                        if (dDistributedAjustment == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                            return;
                                        }

                                        #endregion



                                        if (dDistributedAjustment <= Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) && dDistributedAjustment > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAjustment);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        dDistributedAjustment = Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT));
                                        dDistributedAjustment = dDistributedAjustment - Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_ADJUSTMENT_AMT));
                                        C1Cleargage.SetData(i, COL_TOTALAMOUNT, dDistributedAjustment);
                                        return;
                                    }
                                }
                            }


                        }
                    }
                    else if (sAction == Convert.ToString(Actions.DISCOUNT))// && sPaymentMethod == "")
                    {
                        #region Validate Adjustment Amount

                        if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT)).Trim() != "")
                        {
                            decimal _CurrentAdjusmentAmt = 0;
                            decimal _TotalCharge = 0;
                            decimal _PrevAdjustment = 0;
                            decimal _CurrentPayment = 0;
                            decimal _PrevPayment = 0;
                            decimal _PrevTotalAdjustment = 0;

                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_TOTAL_CHARGE) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_TOTAL_CHARGE)).Trim() != "")
                            {
                                _TotalCharge = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_TOTAL_CHARGE));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAT_ADJ) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAT_ADJ)).Trim() != "")
                            {
                                _PrevAdjustment = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAT_ADJ));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_DISTRIBUTE_PAYMENT) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_DISTRIBUTE_PAYMENT)).Trim() != "")
                            {
                                _CurrentPayment = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_DISTRIBUTE_PAYMENT));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAID) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAID)).Trim() != "")
                            {
                                _PrevPayment = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_PAID));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT)).Trim() != "")
                            {
                                _CurrentAdjusmentAmt = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT));
                            }
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_ADJ) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_ADJ)).Trim() != "")
                            {
                                _PrevTotalAdjustment = Convert.ToDecimal(C1Cleargage.GetData(C1Cleargage.RowSel, COL_PREV_ADJ));
                            }

                            if (_CurrentAdjusmentAmt > (_TotalCharge - _PrevTotalAdjustment - _CurrentPayment - _PrevPayment))
                            {
                                MessageBox.Show("Adjustment amount cannot be greater than net charge.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                try
                                {
                                    this.C1Cleargage.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                    C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_AMT, null);
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                    ex = null;
                                }
                                finally
                                {
                                    this.C1Cleargage.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                }
                            }
                        }
                        #endregion

                        #region Validate Adjustment Code
                        if (e.Col == COL_ADJUSTMENT_CODE)
                        {
                            if (C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE) != null && Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE)).Trim() != "")
                            {
                                string[] AdjCodeDesc = null;
                                string _adjstr = "";
                                _adjstr = Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
                                bool _isValidCOde = false;
                                CellStyle cs = C1Cleargage.GetCellStyle(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE);

                                if (_adjstr != "")
                                {
                                    _adjstr = _adjstr = Convert.ToString(C1Cleargage.GetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE)).Trim();
                                    _isValidCOde = cs.ComboList.Contains(_adjstr);
                                }

                                try
                                {
                                    this.C1Cleargage.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);

                                    if (_isValidCOde == true)
                                    {
                                        AdjCodeDesc = _adjstr.Split('-');
                                        C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE, AdjCodeDesc[0]);
                                        C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_DESC, AdjCodeDesc[1]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please select a valid code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE, null);
                                        C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_DESC, null);
                                        C1Cleargage.Focus();
                                        C1Cleargage.Select(C1Cleargage.RowSel, COL_ADJUSTMENT_CODE, true);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                                }
                                finally
                                {
                                    this.C1Cleargage.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                }


                            }
                        }

                        #endregion


                        for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                        {
                            if (e.Col == COL_ADJUSTMENT_AMT && Convert.ToInt64(C1Cleargage.GetData(i, COL_PACCOUNTID)) == Convert.ToInt64(C1Cleargage.GetData(e.Row, COL_PACCOUNTID)) && Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(e.Row, COL_DISTINCTACCOUNTNO)))
                            {
                                if ((Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) == "" && Convert.ToString(C1Cleargage.GetData(e.Row, COL_DOS_FROM)) == "") || (Convert.ToString(C1Cleargage.GetData(e.Row, COL_DOS_FROM)) == Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS))))
                                {
                                    decimal dDistributedAjustment = 0;
                                    decimal dLineamt = 0;
                                    decimal dDistributeAdj = Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_ADJUSTMENT_AMT));

                                    if (Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_PAT_DUE)) > 0)
                                    {
                                        dLineamt = Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_PAT_DUE));
                                    }


                                    #region "Validating Manually Distributed  Amount"

                                    if (dDistributeAdj > dLineamt)
                                    {
                                        MessageBox.Show("Distributed payment amount exceeds due.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        C1Cleargage.SetData(e.Row, COL_ADJUSTMENT_AMT, Convert.ToDecimal(0));

                                        #region "Calculate Available Adustment amount"


                                        dDistributedAjustment = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAjustment > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAjustment);
                                        }
                                        else if (dDistributedAjustment == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                        }


                                        #endregion

                                        return;
                                    }
                                    else if (dDistributeAdj == 0)
                                    {
                                        #region "Calculate Available Amount"

                                        dDistributedAjustment = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAjustment > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAjustment);
                                        }
                                        else if (dDistributedAjustment == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                        }

                                        #endregion

                                        return;
                                    }
                                    else
                                    {
                                        dDistributedAjustment = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAjustment > Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)))
                                        {
                                            MessageBox.Show("Distributed adjustment amount exceeds available amount,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            C1Cleargage.SetData(e.Row, COL_ADJUSTMENT_AMT, Convert.ToDecimal(0));

                                            #region "Calculate Available Payment Amount"

                                            dDistributedAjustment = CalculateAvailableAmount(sender, e, i);
                                            if (dDistributedAjustment > 0)
                                            {
                                                C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAjustment);
                                            }
                                            else if (dDistributedAjustment == 0)
                                            {
                                                C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                            }

                                            #endregion

                                            return;
                                        }


                                    }

                                    #endregion

                                    if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) != Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) && (Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(e.Row, COL_DISTINCTACCOUNTNO))))
                                    {
                                        #region "Calculate Available amount"

                                        dDistributedAjustment = CalculateAvailableAmount(sender, e, i);

                                        if (dDistributedAjustment == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                            return;
                                        }

                                        #endregion



                                        if (dDistributedAjustment <= Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) && dDistributedAjustment > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAjustment);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        dDistributedAjustment = Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT));
                                        if (Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(e.Row, COL_DISTINCTACCOUNTNO)))
                                        {
                                            dDistributedAjustment = dDistributedAjustment - Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_ADJUSTMENT_AMT));
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, dDistributedAjustment);
                                            return;
                                        }
                                    }
                                }
                            }


                        }
                    }
                    if (sAction == Convert.ToString(Actions.PAYMENT)) //&& sPaymentMethod == Convert.ToString(PaymentMethod.CREDIT))
                    {
                        for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                        {
                            if (e.Col == COL_DISTRIBUTE_PAYMENT && Convert.ToInt64(C1Cleargage.GetData(i, COL_PACCOUNTID)) == Convert.ToInt64(C1Cleargage.GetData(e.Row, COL_PACCOUNTID)) && Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(e.Row, COL_DISTINCTACCOUNTNO)))
                            {
                                if ((Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) == "" && Convert.ToString(C1Cleargage.GetData(e.Row, COL_DOS_FROM)) == "") || (Convert.ToString(C1Cleargage.GetData(e.Row, COL_DOS_FROM)) == Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS))))
                                {
                                    decimal dDistributedAmount = 0;
                                    decimal dLineamt = 0;
                                    decimal dDistributeAmt = Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_DISTRIBUTE_PAYMENT));

                                    if (Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_PAT_DUE)) > 0)
                                    {
                                        dLineamt = Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_PAT_DUE));
                                    }

                                    for (int j = i; j < C1Cleargage.Rows.Count; j++)
                                    {
                                        if (Convert.ToInt64(C1Cleargage.GetData(j, COL_PACCOUNTID)) == Convert.ToInt64(C1Cleargage.GetData(e.Row, COL_PACCOUNTID)) && Convert.ToString(C1Cleargage.GetData(j, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(e.Row, COL_DISTINCTACCOUNTNO)))
                                        {
                                            if (Convert.ToDecimal(C1Cleargage.GetData(j, COL_DISTRIBUTE_PAYMENT)) == 0 && Convert.ToString(C1Cleargage.GetData(j, COL_CPT_CODE)) ==Convert.ToString(CPTCode.CGFEE))
                                            {
                                                for (int p = j; p < C1Cleargage.Rows.Count; p++)
                                                {
                                                    if (Convert.ToInt64(C1Cleargage.GetData(p, COL_PACCOUNTID)) == Convert.ToInt64(C1Cleargage.GetData(e.Row, COL_PACCOUNTID)) && Convert.ToString(C1Cleargage.GetData(p, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(e.Row, COL_DISTINCTACCOUNTNO)))
                                                    {
                                                        if (Convert.ToDecimal(C1Cleargage.GetData(p, COL_DISTRIBUTE_PAYMENT)) != 0 && Convert.ToString(C1Cleargage.GetData(p, COL_CPT_CODE)) != Convert.ToString(CPTCode.CGFEE))
                                                        {
                                                            string sMessage = string.Format("Account contains {0} charge. Allocate payment to {0} charge first.", Convert.ToString(CPTCode.CGFEE));
                                                            MessageBox.Show(sMessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            for (int k = j; k < C1Cleargage.Rows.Count; k++)
                                                            {
                                                                if (Convert.ToInt64(C1Cleargage.GetData(k, COL_PACCOUNTID)) == Convert.ToInt64(C1Cleargage.GetData(e.Row, COL_PACCOUNTID)) && Convert.ToString(C1Cleargage.GetData(k, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(e.Row, COL_DISTINCTACCOUNTNO)) && Convert.ToString(C1Cleargage.GetData(k, COL_FROMDOS)) == "")
                                                                {
                                                                    this.C1Cleargage.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                                                    C1Cleargage.SetData(k, COL_DISTRIBUTE_PAYMENT, Convert.ToDecimal(0));
                                                                    dDistributeAmt = Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_DISTRIBUTE_PAYMENT));
                                                                    this.C1Cleargage.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }

                                                }
                                            }
                                            else if (Convert.ToDecimal(C1Cleargage.GetData(j, COL_DISTRIBUTE_PAYMENT)) != 0 && Convert.ToString(C1Cleargage.GetData(j, COL_CPT_CODE)) ==Convert.ToString(CPTCode.CGFEE))
                                            {
                                                if (Convert.ToDecimal(C1Cleargage.GetData(j, COL_PAT_DUE)) > Convert.ToDecimal(C1Cleargage.GetData(j, COL_DISTRIBUTE_PAYMENT)) && Convert.ToDecimal(C1Cleargage.GetData(j, COL_DISTRIBUTE_PAYMENT)) != 0)
                                                {
                                                    string sMessage = string.Format("Can not apply partial amount for {0} charge.", Convert.ToString(CPTCode.CGFEE));
                                                    MessageBox.Show(sMessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    for (int k = e.Row; k < C1Cleargage.Rows.Count; k++)
                                                    {
                                                        if (Convert.ToInt64(C1Cleargage.GetData(k, COL_PACCOUNTID)) == Convert.ToInt64(C1Cleargage.GetData(e.Row, COL_PACCOUNTID)) && Convert.ToString(C1Cleargage.GetData(k, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(e.Row, COL_DISTINCTACCOUNTNO)) && Convert.ToString(C1Cleargage.GetData(k, COL_FROMDOS)) == "")
                                                        {
                                                            this.C1Cleargage.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                                            C1Cleargage.SetData(k, COL_DISTRIBUTE_PAYMENT, Convert.ToDecimal(0));
                                                            dDistributeAmt = Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_DISTRIBUTE_PAYMENT));
                                                            this.C1Cleargage.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                        
                                    }
                                   

                                    #region "Validating Manually Distributed  Amount"

                                    if (dDistributeAmt > dLineamt)
                                    {
                                        MessageBox.Show("Distributed payment amount exceeds due.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        C1Cleargage.SetData(e.Row, COL_DISTRIBUTE_PAYMENT, Convert.ToDecimal(0));

                                        #region "Calculate Available Payment amount"


                                        dDistributedAmount = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAmount > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAmount);
                                        }
                                        else if (dDistributedAmount == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                        }


                                        #endregion

                                        return;
                                    }
                                    else if (dDistributeAmt < 0)
                                    {
                                        MessageBox.Show("Distributed payment amount cannot be negative,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        C1Cleargage.SetData(e.Row, COL_DISTRIBUTE_PAYMENT, Convert.ToDecimal(0));

                                        #region "Calculate Available Payment amount"

                                        dDistributedAmount = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAmount > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAmount);
                                        }
                                        else if (dDistributedAmount == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                        }

                                        #endregion

                                        return;
                                    }
                                    else if (dDistributeAmt == 0)
                                    {
                                        #region "Calculate Available Amount"

                                        dDistributedAmount = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAmount > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAmount);
                                        }
                                        else if (dDistributedAmount == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                        }

                                        #endregion

                                        return;
                                    }
                                    else
                                    {
                                        dDistributedAmount = CalculateAvailableAmount(sender, e, i);
                                        if (dDistributedAmount > Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)))
                                        {
                                            MessageBox.Show("Distributed amount exceeds available amount,please correct the allocation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            C1Cleargage.SetData(e.Row, COL_DISTRIBUTE_PAYMENT, Convert.ToDecimal(0));

                                            #region "Calculate Available Payment Amount"

                                            dDistributedAmount = CalculateAvailableAmount(sender, e, i);
                                            if (dDistributedAmount > 0)
                                            {
                                                C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAmount);
                                            }
                                            else if (dDistributedAmount == 0)
                                            {
                                                C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                            }

                                            #endregion

                                            return;
                                        }


                                    }

                                    #endregion

                                    if ((Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) != Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT))) && (Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(e.Row, COL_DISTINCTACCOUNTNO))))
                                    {
                                        #region "Calculate Available amount"

                                        dDistributedAmount = CalculateAvailableAmount(sender, e, i);

                                        if (dDistributedAmount == 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)));
                                            return;
                                        }

                                        #endregion



                                        if (dDistributedAmount <= Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) && dDistributedAmount > 0)
                                        {
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - dDistributedAmount);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        dDistributedAmount = Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT));
                                        if (Convert.ToString(C1Cleargage.GetData(i,COL_DISTINCTACCOUNTNO))==Convert.ToString(C1Cleargage.GetData(e.Row,COL_DISTINCTACCOUNTNO)))
                                        {
                                            dDistributedAmount = dDistributedAmount - Convert.ToDecimal(C1Cleargage.GetData(e.Row, COL_DISTRIBUTE_PAYMENT));
                                            C1Cleargage.SetData(i, COL_TOTALAMOUNT, dDistributedAmount);
                                            return;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {

            }
        }

        private void C1Cleargage_BeforeEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Col == COL_DISTRIBUTE_PAYMENT && C1Cleargage.GetData(e.Row, COL_TOTALAMOUNT) != null)
                {
                    e.Cancel = true;
                }
                if (e.Col == COL_ADJUSTMENT_CODE && C1Cleargage.GetData(e.Row, COL_TOTALAMOUNT) != null)
                {
                    e.Cancel = true;
                }
                if (e.Col == COL_ADJUSTMENT_AMT && C1Cleargage.GetData(e.Row, COL_TOTALAMOUNT) != null)
                {
                    e.Cancel = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void C1Cleargage_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (C1Cleargage.ColSel == COL_DISTRIBUTE_PAYMENT)
                    {
                        if (C1Cleargage.GetData(C1Cleargage.RowSel, C1Cleargage.ColSel) != null)
                        {
                            C1Cleargage.SetData(C1Cleargage.RowSel, C1Cleargage.ColSel, 0);
                        }
                    }
                    if (C1Cleargage.ColSel == COL_ADJUSTMENT_AMT)
                    {
                        if (C1Cleargage.GetData(C1Cleargage.RowSel, C1Cleargage.ColSel) != null)
                        {
                            C1Cleargage.SetData(C1Cleargage.RowSel, C1Cleargage.ColSel, 0);
                        }
                    }
                    if (C1Cleargage.ColSel == COL_ADJUSTMENT_CODE)
                    {
                        if (C1Cleargage.GetData(C1Cleargage.RowSel, C1Cleargage.ColSel) != null)
                        {
                            C1Cleargage.SetData(C1Cleargage.RowSel, C1Cleargage.ColSel, null);
                            C1Cleargage.SetData(C1Cleargage.RowSel, COL_ADJUSTMENT_DESC, null);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private decimal CalculateAvailableAmount(object sender, RowColEventArgs e, int i)
        {
            decimal dDistributedAmt = 0;
            try
            {
                if (sAction == Convert.ToString(Actions.PAYMENT))// && sPaymentMethod ==  Convert.ToString(PaymentMethod.CREDIT))
                {
                    for (int j = i; j < C1Cleargage.Rows.Count; j++)
                    {
                        if (Convert.ToInt64(C1Cleargage.GetData(j, COL_PACCOUNTID)) == Convert.ToInt64(C1Cleargage.GetData(e.Row, COL_PACCOUNTID)) && Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(j, COL_DISTINCTACCOUNTNO)))
                        {
                            if (Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) == "" && Convert.ToString(C1Cleargage.GetData(j, COL_DOS_FROM)) == "")
                            {
                                dDistributedAmt = dDistributedAmt + Convert.ToDecimal(C1Cleargage.GetData(j, COL_DISTRIBUTE_PAYMENT));
                            }
                            else if (Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) == Convert.ToString(C1Cleargage.GetData(j, COL_DOS_FROM)) )
                            {
                                dDistributedAmt = dDistributedAmt + Convert.ToDecimal(C1Cleargage.GetData(j, COL_DISTRIBUTE_PAYMENT));
                            }
                        }
                    }
                }
                else
                {
                    for (int j = i; j < C1Cleargage.Rows.Count; j++)
                    {
                        if (Convert.ToInt64(C1Cleargage.GetData(j, COL_PACCOUNTID)) == Convert.ToInt64(C1Cleargage.GetData(e.Row, COL_PACCOUNTID)) && Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO)) == Convert.ToString(C1Cleargage.GetData(j, COL_DISTINCTACCOUNTNO)))
                        {
                            if (Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) == "" && Convert.ToString(C1Cleargage.GetData(j, COL_DOS_FROM)) == "")
                            {
                                dDistributedAmt = dDistributedAmt + Convert.ToDecimal(C1Cleargage.GetData(j, COL_ADJUSTMENT_AMT));
                            }
                            else if (Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) == Convert.ToString(C1Cleargage.GetData(j, COL_DOS_FROM)))
                            {
                                dDistributedAmt = dDistributedAmt + Convert.ToDecimal(C1Cleargage.GetData(j, COL_ADJUSTMENT_AMT));
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return dDistributedAmt;
        }

        private void rb_ShowOneByOne_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rb_ShowOneByOne.Checked)
                {
                    CleargagelistRowCount = 0;
                    tsb_Next.Visible = true;
                    tsb_Save.Text = "Save && Next";
                    tsb_Save.ToolTipText = "Save & Next";

                    rb_ShowOneByOne.Font = new Font(rb_ShowOneByOne.Font, FontStyle.Bold);
                    LoadCleargagePaymentOneByOne();
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Select, "Cleargage Payment Select Show All ", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);

                }
                else
                {
                    rb_ShowOneByOne.Font = new Font(rb_ShowOneByOne.Font, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Select, "Exception occured while Cleargage Payment Select Show All : "+ex.ToString(), 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Failure,SoftwareComponent.gloPM,true);
            }
        }

        #endregion

        private void C1Cleargage_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar("-"))
            {
                e.Handled = true;
            }
        }



        private void LoadCleargagePaymentList()
        {

            pnl_CleargagePaymentDistributionList.Visible = true;

            try
            {
                Int32 nActionEnumCode=0;
                switch (sAction.ToUpper())
                {
                    case "PAYMENT":nActionEnumCode=Actions.PAYMENT.GetHashCode();
                        break;
                    case "FEE": nActionEnumCode = Actions.FEE.GetHashCode();
                        break;
                    case "CREDIT": nActionEnumCode = Actions.CREDIT.GetHashCode();
                        break;
                    case "FEECREDIT": nActionEnumCode = Actions.FEECREDIT.GetHashCode();
                        break;
                    case "DISCOUNT": nActionEnumCode = Actions.DISCOUNT.GetHashCode();
                        break;
                    case "ADJUSTMENT": nActionEnumCode = Actions.ADJUSTMENT.GetHashCode();
                        break;
                    case "PREFUNDDISCOUNT": nActionEnumCode = Actions.PREFUNDDISCOUNT.GetHashCode();
                        break;
                    case "PREFUNDBUYOUT": nActionEnumCode = Actions.PREFUNDBUYOUT.GetHashCode();
                        break;
                   
                }
                dtPaymentList = ClsCleargagePaymentPosting.GetCleargagePaymentList(_nCleargageFileID, nPAccountID, sAction.ToUpper(), sPaymentMethod, nActionEnumCode);
                #region "Get AccountID from List

                dtPAccountID = new DataTable();
                dtPAccountID.Columns.Add("nPAccountID");
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = dtPAccountID.Columns["nPAccountID"];
                dtPAccountID.PrimaryKey = keyColumns;
                for (int i = 0; i < dtPaymentList.Rows.Count; i++)
                {
                    if (dtPAccountID.Rows.Count == 0)
                    {
                        dtPAccountID.Rows.Add(Convert.ToInt64(dtPaymentList.Rows[i]["nPAccountID"]));
                    }
                    else
                    {
                        if (!dtPAccountID.Rows.Contains(Convert.ToInt64(dtPaymentList.Rows[i]["nPAccountID"])))
                        {
                            dtPAccountID.Rows.Add(Convert.ToInt64(dtPaymentList.Rows[i]["nPAccountID"]));
                        }
                    }
                }

                #endregion
                C1Cleargage.DataSource = null;
                DesignClearGagePayment();

                #region "Bind Data"

                var Accounts = new List<KeyValuePair<string, string>>();

                for (int i = 0; i < dtPaymentList.Rows.Count; i++)
                {

                    if (!Accounts.Contains(new KeyValuePair<string, string>(Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nAmount"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]), Convert.ToString(dtPaymentList.Rows[i]["nFromDate"]))))                   
                    {
                        #region "MasterData"

                        C1Cleargage.Rows.Add();
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTNO, Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTNAME, Convert.ToString(dtPaymentList.Rows[i]["PatientName"]));

                        if (Convert.ToString(dtPaymentList.Rows[i]["nFromDate"]) == "")
                        {
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_FROMDOS, "");
                        }
                        else
                        {
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_FROMDOS, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtPaymentList.Rows[i]["nFromDate"])).ToString("MM/dd/yyyy"));
                        }
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TOTALAMOUNT, Convert.ToString(dtPaymentList.Rows[i]["nAmount"]));

                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PACCOUNTID, Convert.ToString(dtPaymentList.Rows[i]["nPAccountID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_GUARANTORID, Convert.ToString(dtPaymentList.Rows[i]["nGuarantorID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNT_PATIENT_ID, Convert.ToString(dtPaymentList.Rows[i]["nAccountPatientID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLEARGAGEFILEID, Convert.ToInt64(dtPaymentList.Rows[i]["nCleargageFileID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTID, Convert.ToString(dtPaymentList.Rows[i]["nPatientID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, Convert.ToString(dtPaymentList.Rows[i]["nTransactionMasterID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_DETAILID, Convert.ToString(dtPaymentList.Rows[i]["nTransactionMasterDetailID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_LINENO, Convert.ToString(dtPaymentList.Rows[i]["nTransactionLineNo"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ENCOUNTERID, Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CHECKAMOUNT, Convert.ToString(dtPaymentList.Rows[i]["nAmount"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAYMENTMETHOD, Convert.ToString(dtPaymentList.Rows[i]["PaymentMethod"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAYMENTPLANID, Convert.ToString(dtPaymentList.Rows[i]["PaymentPlanID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_TRANSACTIONID, Convert.ToString(dtPaymentList.Rows[i]["TransactionID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_ORIGINALTRANSACTIONID, Convert.ToString(dtPaymentList.Rows[i]["OriginalTransactionID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TIMESTAMP, Convert.ToDateTime(dtPaymentList.Rows[i]["Timestamp"]).ToString("MM/dd/yyyy"));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_SERVICEDATE, Convert.ToString(dtPaymentList.Rows[i]["ServiceDate"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BRANCHID, Convert.ToString(dtPaymentList.Rows[i]["BranchID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLIENTID, Convert.ToString(dtPaymentList.Rows[i]["ClientID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ORIGIN, Convert.ToString(dtPaymentList.Rows[i]["Origin"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_USER, Convert.ToString(dtPaymentList.Rows[i]["User"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE, Convert.ToString(dtPaymentList.Rows[i]["TxnType"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_REFERENCENUMBER, Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_APPROVALCODE, Convert.ToString(dtPaymentList.Rows[i]["ApprovalCode"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_NATIONALPROVIDERID, Convert.ToString(dtPaymentList.Rows[i]["NationalProviderID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ENTRYMETHOD, Convert.ToString(dtPaymentList.Rows[i]["EntryMethod"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTTYPE, Convert.ToString(dtPaymentList.Rows[i]["AccountType"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_ACCOUNTNUMBER, Convert.ToString(dtPaymentList.Rows[i]["AccountNumber"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTNAME, Convert.ToString(dtPaymentList.Rows[i]["AccountName"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_LINESTATUS, Convert.ToString(dtPaymentList.Rows[i]["nLineStatus"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTCODE, Convert.ToString(dtPaymentList.Rows[i]["PatientCode"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACTION, Convert.ToString(dtPaymentList.Rows[i]["Action"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_R_ENCOUNTERID, Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]));
                        C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_DISTINCTACCOUNTNO, Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nAmount"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]));

                        Accounts.Add(new KeyValuePair<string, string>(Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nAmount"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]), Convert.ToString(dtPaymentList.Rows[i]["nFromDate"])));

                        #endregion

                        #region "Style For Master Data"

                        C1Cleargage.Rows[C1Cleargage.Rows.Count - 1].Style = C1Cleargage.Styles["cs_ClaimRowStyle"];
                       

                        #endregion
                    }
                    if ((Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY) || Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.OTHER) || Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE)))
                    {
                        continue;
                    }
                    #region "Sub Data"

                    C1Cleargage.Rows.Add();
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PACCOUNTID, Convert.ToString(dtPaymentList.Rows[i]["nPAccountID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_GUARANTORID, Convert.ToString(dtPaymentList.Rows[i]["nGuarantorID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNT_PATIENT_ID, Convert.ToString(dtPaymentList.Rows[i]["nAccountPatientID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTID, Convert.ToString(dtPaymentList.Rows[i]["nPatientID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, Convert.ToString(dtPaymentList.Rows[i]["nTransactionMasterID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_DETAILID, Convert.ToString(dtPaymentList.Rows[i]["nTransactionMasterDetailID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_LINENO, Convert.ToString(dtPaymentList.Rows[i]["nTransactionLineNo"]));

                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLAIM_NUM, Convert.ToString(dtPaymentList.Rows[i]["SplitClaimNo"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtPaymentList.Rows[i]["nFromDate"])).ToString("MM/dd/yyyy"));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CPT_CODE, Convert.ToString(dtPaymentList.Rows[i]["sCPTCode"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_MODIFIER, Convert.ToString(dtPaymentList.Rows[i]["Modifier"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TOTAL_CHARGE, Convert.ToString(dtPaymentList.Rows[i]["dTotal"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TOT_BAL_AMT, Convert.ToString(dtPaymentList.Rows[i]["TotalBalanceAmount"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAT_DUE, Convert.ToString(dtPaymentList.Rows[i]["PatientDue"]));


                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_DOS_TO, Convert.ToString(dtPaymentList.Rows[i]["nToDate"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLAIM_DATE, Convert.ToString(dtPaymentList.Rows[i]["ClaimDate"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLAIM_CLOSE_DATE, Convert.ToString(dtPaymentList.Rows[i]["nCloseDate"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_FACILITY_TYPE, Convert.ToString(dtPaymentList.Rows[i]["nFacilityType"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLAIM_NO, Convert.ToString(dtPaymentList.Rows[i]["nClaimNo"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CPT_DESCRIPTON, Convert.ToString(dtPaymentList.Rows[i]["sCPTDescription"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CHARGE, Convert.ToString(dtPaymentList.Rows[i]["dCharges"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_UNIT, Convert.ToString(dtPaymentList.Rows[i]["dUnit"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ALLOWED, Convert.ToString(dtPaymentList.Rows[i]["dAllowed"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PROVIDERID, Convert.ToString(dtPaymentList.Rows[i]["nProvider"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRANSACTION_LINESTATUS, Convert.ToString(dtPaymentList.Rows[i]["nTransactionLineStatus"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_SENDTOFLAG, Convert.ToString(dtPaymentList.Rows[i]["nSendToFlag"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PREV_PAID, Convert.ToString(dtPaymentList.Rows[i]["PreviousPaid"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PREV_ADJ, Convert.ToString(dtPaymentList.Rows[i]["PreviousAdjuestment"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PREV_PAT_ADJ, Convert.ToString(dtPaymentList.Rows[i]["PrevPatAdj"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BadDebt_DUE, Convert.ToString(dtPaymentList.Rows[i]["BadDebtDue"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PREV_PAT_PAID_AMT, Convert.ToString(dtPaymentList.Rows[i]["PreviousPatientPaidAmount"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_SPLIT_CLAIM_NO, Convert.ToString(dtPaymentList.Rows[i]["SplitClaimNo"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_TRN_ID, Convert.ToString(dtPaymentList.Rows[i]["TrackTransactionID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_TRN_DTL_ID, Convert.ToString(dtPaymentList.Rows[i]["TrackTransactionDetailID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_SUB_CLAIM_NO, Convert.ToString(dtPaymentList.Rows[i]["TrackSubClaimNo"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_IS_HOLD, Convert.ToString(dtPaymentList.Rows[i]["TrackIsHold"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_HOLD_INFO, Convert.ToString(dtPaymentList.Rows[i]["TrackHoldInfo"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_RES_PARTY, Convert.ToString(dtPaymentList.Rows[i]["RespParty"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_NON_SERVICECODE, Convert.ToString(dtPaymentList.Rows[i]["bNonServiceCode"]));
                    //C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACTION, Convert.ToString(dtPaymentList.Rows[i]["Action"]));
                    //C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ENCOUNTERID, Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]));

                    //C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAYMENTMETHOD, Convert.ToString(dtPaymentList.Rows[i]["PaymentMethod"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAYMENTPLANID, Convert.ToString(dtPaymentList.Rows[i]["PaymentPlanID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_TRANSACTIONID, Convert.ToString(dtPaymentList.Rows[i]["TransactionID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_ORIGINALTRANSACTIONID, Convert.ToString(dtPaymentList.Rows[i]["OriginalTransactionID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TIMESTAMP, Convert.ToDateTime(dtPaymentList.Rows[i]["Timestamp"]).ToString("MM/dd/yyyy"));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_SERVICEDATE, Convert.ToString(dtPaymentList.Rows[i]["ServiceDate"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BRANCHID, Convert.ToString(dtPaymentList.Rows[i]["BranchID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLIENTID, Convert.ToString(dtPaymentList.Rows[i]["ClientID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ORIGIN, Convert.ToString(dtPaymentList.Rows[i]["Origin"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_USER, Convert.ToString(dtPaymentList.Rows[i]["User"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE, Convert.ToString(dtPaymentList.Rows[i]["TxnType"]));
                    //C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_REFERENCENUMBER, Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_APPROVALCODE, Convert.ToString(dtPaymentList.Rows[i]["ApprovalCode"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_NATIONALPROVIDERID, Convert.ToString(dtPaymentList.Rows[i]["NationalProviderID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ENTRYMETHOD, Convert.ToString(dtPaymentList.Rows[i]["EntryMethod"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTTYPE, Convert.ToString(dtPaymentList.Rows[i]["AccountType"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_ACCOUNTNUMBER, Convert.ToString(dtPaymentList.Rows[i]["AccountNumber"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTNAME, Convert.ToString(dtPaymentList.Rows[i]["AccountName"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_LINESTATUS, Convert.ToString(dtPaymentList.Rows[i]["nLineStatus"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTCODE, Convert.ToString(dtPaymentList.Rows[i]["PatientCode"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_R_ENCOUNTERID, Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]));
                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_DISTINCTACCOUNTNO, Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nAmount"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]));

                    #endregion

                    #region "Style For Sub Data"

                    C1Cleargage.Rows[C1Cleargage.Rows.Count - 1].Style = C1Cleargage.Styles["cs_ClaimServiceRowStyle"];
                    C1Cleargage.SetCellStyle(C1Cleargage.Rows.Count - 1, COL_DISTRIBUTE_PAYMENT, "cs_EditableCurrencyStyle");
                    C1Cleargage.SetCellStyle(C1Cleargage.Rows.Count - 1, COL_ADJUSTMENT_CODE, "cs_EditableAdjustment");
                    C1Cleargage.SetCellStyle(C1Cleargage.Rows.Count - 1, COL_ADJUSTMENT_AMT, "cs_EditableCurrencyStyle");
                    #endregion
                }


                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);               
                ex = null;
            }
            finally
            {
                if (dtPaymentList != null)
                {
                    dtPaymentList = null;
                }
            }
        }
        private void DesignClearGagePayment()
        {
            try
            {

                gloC1FlexStyle.Style(C1Cleargage, true);
                C1Cleargage.AllowSorting = AllowSortingEnum.None;
                C1Cleargage.SelectionMode = SelectionModeEnum.Cell;

                C1Cleargage.Cols.Count = COL_COLCOUNT;
                C1Cleargage.Rows.Count = 1;
                C1Cleargage.Rows.Fixed = 1;

                #region "Design"

                #region "Headers"


                C1Cleargage.SetData(0, COL_PACCOUNTID, "Pat. AccountID");
                C1Cleargage.SetData(0, COL_GUARANTORID, "Pat. GuarantorID");
                C1Cleargage.SetData(0, COL_ACCOUNT_PATIENT_ID, "Pat. AccPatID");
                C1Cleargage.SetData(0, COL_CLEARGAGEFILEID, "Cleargage File ID");
                C1Cleargage.SetData(0, COL_BILLING_TRANSACTON_ID, "Transacton ID");
                C1Cleargage.SetData(0, COL_BILLING_TRANSACTON_DETAILID, "Transacton Detail ID");
                C1Cleargage.SetData(0, COL_BILLING_TRANSACTON_LINENO, "Transacton Line No");
                C1Cleargage.SetData(0, COL_PATIENTID, "Patient ID");

                C1Cleargage.SetData(0, COL_ACCOUNTNO, "Acc. #");
                C1Cleargage.SetData(0, COL_PATIENTNAME, "Patient");
                C1Cleargage.SetData(0, COL_CHECKAMOUNT, "Amount");
                C1Cleargage.SetData(0, COL_TOTALAMOUNT, "Available");
                C1Cleargage.SetData(0, COL_AVAILABLEAMOUNT, "AvailableReserve");
                C1Cleargage.SetData(0, COL_FROMDOS, "From DOS");

                C1Cleargage.SetData(0, COL_CLAIM_NUM, "Claim #");
                C1Cleargage.SetData(0, COL_DOS_FROM, "DOS");
                C1Cleargage.SetData(0, COL_CPT_CODE, "CPT");
                C1Cleargage.SetData(0, COL_MODIFIER, "Mod");
                C1Cleargage.SetData(0, COL_TOTAL_CHARGE, "Charge");
                C1Cleargage.SetData(0, COL_TOT_BAL_AMT, "Balance");
                C1Cleargage.SetData(0, COL_PAT_DUE, "Pat. Due");
                C1Cleargage.SetData(0, COL_DISTRIBUTE_PAYMENT, "Payment");


                C1Cleargage.SetData(0, COL_RESERVE_CLOSE_DATE, "Res Close Date");
                C1Cleargage.SetData(0, COL_DOS_TO, "DOS To");
                C1Cleargage.SetData(0, COL_CLAIM_DATE, "Claim Date");
                C1Cleargage.SetData(0, COL_CLAIM_CLOSE_DATE, "Claim Close Date");
                C1Cleargage.SetData(0, COL_CPT_DESCRIPTON, "CPT Description");
                C1Cleargage.SetData(0, COL_FACILITY_TYPE, "Facility Type");
                C1Cleargage.SetData(0, COL_CLAIM_NO, "Claim No");
                C1Cleargage.SetData(0, COL_UNIT, "Unit");
                C1Cleargage.SetData(0, COL_CHARGE, "dCharge");
                C1Cleargage.SetData(0, COL_ALLOWED, "Allowed");
                C1Cleargage.SetData(0, COL_PROVIDERID, "Provider ID");
                C1Cleargage.SetData(0, COL_TRANSACTION_LINESTATUS, "Tran Line Status");
                C1Cleargage.SetData(0, COL_SENDTOFLAG, "Send To Flag");
                C1Cleargage.SetData(0, COL_PREV_PAID, "Prev Paid");
                C1Cleargage.SetData(0, COL_PREV_ADJ, "Prev Adj");
                C1Cleargage.SetData(0, COL_PREV_PAT_ADJ, "Prev Pat. Adj");
                C1Cleargage.SetData(0, COL_INS_DUE, "Ins. Due");
                C1Cleargage.SetData(0, COL_BadDebt_DUE, "Bad Debt Due");
                C1Cleargage.SetData(0, COL_PREV_PAT_PAID_AMT, "Prev. PatPaidAmt");
                C1Cleargage.SetData(0, COL_SPLIT_CLAIM_NO, "Split Claim No.");
                C1Cleargage.SetData(0, COL_TRACK_TRN_ID, "TrackTrnID");
                C1Cleargage.SetData(0, COL_TRACK_TRN_DTL_ID, "TrackTrnDtlID");
                C1Cleargage.SetData(0, COL_TRACK_SUB_CLAIM_NO, "Claim #");
                C1Cleargage.SetData(0, COL_TRACK_IS_HOLD, "Hold");
                C1Cleargage.SetData(0, COL_TRACK_HOLD_INFO, "Hold Info");
                C1Cleargage.SetData(0, COL_TRACK_RES_PARTY, "Party");
                C1Cleargage.SetData(0, COL_NON_SERVICECODE, "Non Service Code");
              
                C1Cleargage.SetData(0, COL_ACTION, "Action");
                C1Cleargage.SetData(0, COL_ADJUSTMENT_CODE, "Adj. Code");
                C1Cleargage.SetData(0, COL_ADJUSTMENT_AMT, "Adj. Amount");
                C1Cleargage.SetData(0, COL_ADJUSTMENT_DESC, "Adjustment Desc");
                C1Cleargage.SetData(0, COL_ENCOUNTERID, "EncounterID");
                C1Cleargage.SetData(0, COL_DISTINCTACCOUNTNO, "Distinct Account #");
                C1Cleargage.SetData(0, COL_PAYMENTPLANID, "Payment Plan ID");
                C1Cleargage.SetData(0, COL_CG_TRANSACTIONID, "CG Transaction ID");
                C1Cleargage.SetData(0, COL_CG_ORIGINALTRANSACTIONID, "Original Transaction ID");
                C1Cleargage.SetData(0, COL_TIMESTAMP, "Time Stamp");
                C1Cleargage.SetData(0, COL_SERVICEDATE, "Service Date");
                C1Cleargage.SetData(0, COL_BRANCHID, "Branch ID");
                C1Cleargage.SetData(0, COL_CLIENTID, "Client ID");
                C1Cleargage.SetData(0, COL_ORIGIN, "Origin");
                C1Cleargage.SetData(0, COL_USER, "User");
                C1Cleargage.SetData(0, COL_TxnTYPE, "Txn Type");
                C1Cleargage.SetData(0, COL_REFERENCENUMBER, "Reference #");
                C1Cleargage.SetData(0, COL_APPROVALCODE, "Approval Code");
                C1Cleargage.SetData(0, COL_NATIONALPROVIDERID, "National Provider ID");
                C1Cleargage.SetData(0, COL_ENTRYMETHOD, "Entry Method");
                C1Cleargage.SetData(0, COL_ACCOUNTTYPE, "Account Type");
                C1Cleargage.SetData(0, COL_CG_ACCOUNTNUMBER, "Account #");
                C1Cleargage.SetData(0, COL_ACCOUNTNAME, "Account Name");
                C1Cleargage.SetData(0, COL_LINESTATUS, "Line Status");
                C1Cleargage.SetData(0, COL_PAYMENTMETHOD, "Pay. Method");
                C1Cleargage.SetData(0, COL_PATIENTCODE, "Patient Code");
                C1Cleargage.SetData(0, COL_R_ENCOUNTERID, "Reserve EncounterID");
                #endregion

                #region "Visible"


                C1Cleargage.Cols[COL_PACCOUNTID].Visible = false;
                C1Cleargage.Cols[COL_GUARANTORID].Visible = false;
                C1Cleargage.Cols[COL_ACCOUNT_PATIENT_ID].Visible = false;
                C1Cleargage.Cols[COL_CLEARGAGEFILEID].Visible = false;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_ID].Visible = false;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_DETAILID].Visible = false;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_LINENO].Visible = false;
                C1Cleargage.Cols[COL_PATIENTID].Visible = false;

                C1Cleargage.Cols[COL_ACCOUNTNO].Visible = true;
                C1Cleargage.Cols[COL_PATIENTNAME].Visible = true;
                C1Cleargage.Cols[COL_TOTALAMOUNT].Visible = true;
                C1Cleargage.Cols[COL_AVAILABLEAMOUNT].Visible = false;
                C1Cleargage.Cols[COL_FROMDOS].Visible = false;

                C1Cleargage.Cols[COL_CLAIM_NUM].Visible = true;
                C1Cleargage.Cols[COL_DOS_FROM].Visible = true;
                C1Cleargage.Cols[COL_CPT_CODE].Visible = true;
                C1Cleargage.Cols[COL_MODIFIER].Visible = true;
                C1Cleargage.Cols[COL_TOTAL_CHARGE].Visible = true;
                C1Cleargage.Cols[COL_TOT_BAL_AMT].Visible = false;
                C1Cleargage.Cols[COL_PAT_DUE].Visible = true;
                C1Cleargage.Cols[COL_DISTRIBUTE_PAYMENT].Visible = false;


                C1Cleargage.Cols[COL_RESERVE_CLOSE_DATE].Visible = false;
                C1Cleargage.Cols[COL_DOS_TO].Visible = false;
                C1Cleargage.Cols[COL_CLAIM_DATE].Visible = false;
                C1Cleargage.Cols[COL_CLAIM_CLOSE_DATE].Visible = false;
                C1Cleargage.Cols[COL_CPT_DESCRIPTON].Visible = false;
                C1Cleargage.Cols[COL_FACILITY_TYPE].Visible = false;
                C1Cleargage.Cols[COL_CLAIM_NO].Visible = false;
                C1Cleargage.Cols[COL_UNIT].Visible = false;
                C1Cleargage.Cols[COL_CHARGE].Visible = false;
                C1Cleargage.Cols[COL_ALLOWED].Visible = false;
                C1Cleargage.Cols[COL_PROVIDERID].Visible = false;
                C1Cleargage.Cols[COL_TRANSACTION_LINESTATUS].Visible = false;
                C1Cleargage.Cols[COL_SENDTOFLAG].Visible = false;
                C1Cleargage.Cols[COL_PREV_PAID].Visible = true;
                C1Cleargage.Cols[COL_PREV_ADJ].Visible = false;
                C1Cleargage.Cols[COL_PREV_PAT_ADJ].Visible = false;
                C1Cleargage.Cols[COL_INS_DUE].Visible = false;
                C1Cleargage.Cols[COL_BadDebt_DUE].Visible = false;
                C1Cleargage.Cols[COL_PREV_PAT_PAID_AMT].Visible = false;
                C1Cleargage.Cols[COL_SPLIT_CLAIM_NO].Visible = false;
                C1Cleargage.Cols[COL_TRACK_TRN_ID].Visible = false;
                C1Cleargage.Cols[COL_TRACK_TRN_DTL_ID].Visible = false;
                C1Cleargage.Cols[COL_TRACK_SUB_CLAIM_NO].Visible = false;
                C1Cleargage.Cols[COL_TRACK_IS_HOLD].Visible = false;
                C1Cleargage.Cols[COL_TRACK_HOLD_INFO].Visible = false;
                C1Cleargage.Cols[COL_TRACK_RES_PARTY].Visible = false;
                C1Cleargage.Cols[COL_NON_SERVICECODE].Visible = false;
               
                C1Cleargage.Cols[COL_ACTION].Visible = false;
                C1Cleargage.Cols[COL_ADJUSTMENT_CODE].Visible = true;
                C1Cleargage.Cols[COL_ADJUSTMENT_AMT].Visible = true;
                C1Cleargage.Cols[COL_ADJUSTMENT_DESC].Visible = false;
                C1Cleargage.Cols[COL_ENCOUNTERID].Visible = true;
                C1Cleargage.Cols[COL_CHECKAMOUNT].Visible = true;
                C1Cleargage.Cols[COL_DISTINCTACCOUNTNO].Visible = false;
                C1Cleargage.Cols[COL_PAYMENTPLANID].Visible = false;
                C1Cleargage.Cols[COL_CG_TRANSACTIONID].Visible = false;
                C1Cleargage.Cols[COL_CG_ORIGINALTRANSACTIONID].Visible = false;
                C1Cleargage.Cols[COL_TIMESTAMP].Visible = false;
                C1Cleargage.Cols[COL_SERVICEDATE].Visible = false;
                C1Cleargage.Cols[COL_BRANCHID].Visible = false;
                C1Cleargage.Cols[COL_CLIENTID].Visible = false;
                C1Cleargage.Cols[COL_ORIGIN].Visible = false;
                C1Cleargage.Cols[COL_USER].Visible = false;
                C1Cleargage.Cols[COL_TxnTYPE].Visible = false;
                C1Cleargage.Cols[COL_REFERENCENUMBER].Visible = false;
                C1Cleargage.Cols[COL_APPROVALCODE].Visible = false;
                C1Cleargage.Cols[COL_NATIONALPROVIDERID].Visible = false;
                C1Cleargage.Cols[COL_ENTRYMETHOD].Visible = false;
                C1Cleargage.Cols[COL_ACCOUNTTYPE].Visible = false;
                C1Cleargage.Cols[COL_CG_ACCOUNTNUMBER].Visible = false;
                C1Cleargage.Cols[COL_ACCOUNTNAME].Visible = false;
                C1Cleargage.Cols[COL_LINESTATUS].Visible = false;
                C1Cleargage.Cols[COL_PAYMENTMETHOD].Visible = false;
                C1Cleargage.Cols[COL_PATIENTCODE].Visible = false;
                C1Cleargage.Cols[COL_R_ENCOUNTERID].Visible = false;
                if (sAction == Convert.ToString(Actions.PAYMENT)) //&& sPaymentMethod ==  Convert.ToString(PaymentMethod.CREDIT))
                {
                    C1Cleargage.Cols[COL_PAYMENTMETHOD].Visible = true;
                    C1Cleargage.Cols[COL_ADJUSTMENT_CODE].Visible = false;
                    C1Cleargage.Cols[COL_ADJUSTMENT_AMT].Visible = false;
                    C1Cleargage.Cols[COL_ADJUSTMENT_DESC].Visible = false;
                    C1Cleargage.Cols[COL_DISTRIBUTE_PAYMENT].Visible = true;
                    C1Cleargage.Cols[COL_ACTION].Visible = true;
                    C1Cleargage.Cols[COL_REFERENCENUMBER].Visible = true;
                }

                #endregion

                #region "Width"


                C1Cleargage.Cols[COL_PACCOUNTID].Width = 0;
                C1Cleargage.Cols[COL_GUARANTORID].Width = 0;
                C1Cleargage.Cols[COL_ACCOUNT_PATIENT_ID].Width = 0;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_ID].Width = 0;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_DETAILID].Width = 0;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_LINENO].Width = 0;
                C1Cleargage.Cols[COL_PATIENTID].Width = 0;

                C1Cleargage.Cols[COL_ACCOUNTNO].Width = 62;
                C1Cleargage.Cols[COL_PATIENTNAME].Width =130;
                C1Cleargage.Cols[COL_PAYMENTMETHOD].Width = 88;
                C1Cleargage.Cols[COL_TOTALAMOUNT].Width =70;
                C1Cleargage.Cols[COL_AVAILABLEAMOUNT].Width = 75;
                C1Cleargage.Cols[COL_FROMDOS].Width = 0;

                C1Cleargage.Cols[COL_CLAIM_NUM].Width = 60;
                C1Cleargage.Cols[COL_DOS_FROM].Width =80;
                C1Cleargage.Cols[COL_CPT_CODE].Width = 40;
                C1Cleargage.Cols[COL_MODIFIER].Width = 75;
                C1Cleargage.Cols[COL_TOTAL_CHARGE].Width =65;
                C1Cleargage.Cols[COL_TOT_BAL_AMT].Width = 65;
                C1Cleargage.Cols[COL_PAT_DUE].Width = 65;
                C1Cleargage.Cols[COL_DISTRIBUTE_PAYMENT].Width = 75;


                C1Cleargage.Cols[COL_RESERVE_CLOSE_DATE].Width = 0;
                C1Cleargage.Cols[COL_DOS_TO].Width = 0;
                C1Cleargage.Cols[COL_CLAIM_DATE].Width = 0;
                C1Cleargage.Cols[COL_CLAIM_CLOSE_DATE].Width = 0;
                C1Cleargage.Cols[COL_CPT_DESCRIPTON].Width = 0;
                C1Cleargage.Cols[COL_FACILITY_TYPE].Width = 0;
                C1Cleargage.Cols[COL_CLAIM_NO].Width = 0;
                C1Cleargage.Cols[COL_CHARGE].Width = 0;
                C1Cleargage.Cols[COL_UNIT].Width = 0;
                C1Cleargage.Cols[COL_ALLOWED].Width = 0;
                C1Cleargage.Cols[COL_PROVIDERID].Width = 0;
                C1Cleargage.Cols[COL_TRANSACTION_LINESTATUS].Width = 0;
                C1Cleargage.Cols[COL_SENDTOFLAG].Width = 0;
                C1Cleargage.Cols[COL_PREV_PAID].Width = 75;
                C1Cleargage.Cols[COL_PREV_ADJ].Width = 0;
                C1Cleargage.Cols[COL_PREV_PAT_ADJ].Width = 0;
                C1Cleargage.Cols[COL_INS_DUE].Width = 0;
                C1Cleargage.Cols[COL_BadDebt_DUE].Width = 0;
                C1Cleargage.Cols[COL_PREV_PAT_PAID_AMT].Width = 0;
                C1Cleargage.Cols[COL_SPLIT_CLAIM_NO].Width = 0;
                C1Cleargage.Cols[COL_TRACK_TRN_ID].Width = 0;
                C1Cleargage.Cols[COL_TRACK_TRN_DTL_ID].Width = 0;
                C1Cleargage.Cols[COL_TRACK_SUB_CLAIM_NO].Width = 0;
                C1Cleargage.Cols[COL_TRACK_IS_HOLD].Width = 0;
                C1Cleargage.Cols[COL_TRACK_HOLD_INFO].Width = 0;
                C1Cleargage.Cols[COL_TRACK_RES_PARTY].Width = 0;
                C1Cleargage.Cols[COL_NON_SERVICECODE].Width = 0;
              
                C1Cleargage.Cols[COL_ACTION].Width = 75;
                C1Cleargage.Cols[COL_ADJUSTMENT_CODE].Width = 80;
                C1Cleargage.Cols[COL_ADJUSTMENT_AMT].Width = 80;
                C1Cleargage.Cols[COL_ADJUSTMENT_DESC].Width = 0;
                C1Cleargage.Cols[COL_ENCOUNTERID].Width = 160;
                C1Cleargage.Cols[COL_DISTINCTACCOUNTNO].Width = 0;
                C1Cleargage.Cols[COL_PAYMENTPLANID].Width = 0;
                C1Cleargage.Cols[COL_CG_TRANSACTIONID].Width = 0;
                C1Cleargage.Cols[COL_CG_ORIGINALTRANSACTIONID].Width = 0;
                C1Cleargage.Cols[COL_TIMESTAMP].Width = 0;
                C1Cleargage.Cols[COL_SERVICEDATE].Width = 0;
                C1Cleargage.Cols[COL_BRANCHID].Width = 0;
                C1Cleargage.Cols[COL_CLIENTID].Width = 0;
                C1Cleargage.Cols[COL_ORIGIN].Width = 0;
                C1Cleargage.Cols[COL_USER].Width = 0;
                C1Cleargage.Cols[COL_TxnTYPE].Width = 0;
                C1Cleargage.Cols[COL_REFERENCENUMBER].Width = 90;
                C1Cleargage.Cols[COL_APPROVALCODE].Width = 0;
                C1Cleargage.Cols[COL_NATIONALPROVIDERID].Width = 0;
                C1Cleargage.Cols[COL_ENTRYMETHOD].Width = 0;
                C1Cleargage.Cols[COL_ACCOUNTTYPE].Width = 0;
                C1Cleargage.Cols[COL_CG_ACCOUNTNUMBER].Width = 0;
                C1Cleargage.Cols[COL_ACCOUNTNAME].Width = 0;
                C1Cleargage.Cols[COL_LINESTATUS].Width = 0;
                C1Cleargage.Cols[COL_PATIENTCODE].Width = 0;
                C1Cleargage.Cols[COL_CHECKAMOUNT].Width =60;
                C1Cleargage.Cols[COL_R_ENCOUNTERID].Width = 100;
              
                #endregion

                #region "Alignment"


                C1Cleargage.Cols[COL_PACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_GUARANTORID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_ACCOUNT_PATIENT_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_DETAILID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_LINENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                C1Cleargage.Cols[COL_ACCOUNTNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_TOTALAMOUNT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1Cleargage.Cols[COL_AVAILABLEAMOUNT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1Cleargage.Cols[COL_FROMDOS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                C1Cleargage.Cols[COL_CLAIM_NUM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_DOS_FROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_CPT_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_MODIFIER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_TOTAL_CHARGE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1Cleargage.Cols[COL_TOT_BAL_AMT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1Cleargage.Cols[COL_PAT_DUE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1Cleargage.Cols[COL_DISTRIBUTE_PAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                C1Cleargage.Cols[COL_RESERVE_CLOSE_DATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_DOS_TO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_CLAIM_DATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_FACILITY_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_CLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_CHARGE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_UNIT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_PROVIDERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_TRANSACTION_LINESTATUS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_SENDTOFLAG].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_PREV_PAID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1Cleargage.Cols[COL_PREV_ADJ].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_PREV_PAT_ADJ].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_INS_DUE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_BadDebt_DUE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_PREV_PAT_PAID_AMT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_SPLIT_CLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_TRACK_TRN_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_TRACK_TRN_DTL_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_TRACK_SUB_CLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_TRACK_IS_HOLD].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_TRACK_HOLD_INFO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_TRACK_RES_PARTY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_NON_SERVICECODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                
                C1Cleargage.Cols[COL_ACTION].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_ADJUSTMENT_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Cleargage.Cols[COL_ADJUSTMENT_AMT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1Cleargage.Cols[COL_ADJUSTMENT_DESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                #endregion

                #region " Set Styles "

                #region "Currency Style"

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;
                try
                {
                    if (C1Cleargage.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = C1Cleargage.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = C1Cleargage.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = Font_CellStyle;
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }
                }
                catch
                {
                    csCurrencyStyle = C1Cleargage.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = Font_CellStyle;
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                }

                C1Cleargage.Cols[COL_TOT_BAL_AMT].Style = csCurrencyStyle;
                C1Cleargage.Cols[COL_PAT_DUE].Style = csCurrencyStyle;
                C1Cleargage.Cols[COL_TOTALAMOUNT].Style = csCurrencyStyle;
                C1Cleargage.Cols[COL_AVAILABLEAMOUNT].Style = csCurrencyStyle;
                C1Cleargage.Cols[COL_TOTAL_CHARGE].Style = csCurrencyStyle;
                C1Cleargage.Cols[COL_CHECKAMOUNT].Style = csCurrencyStyle;
                C1Cleargage.Cols[COL_PREV_PAID].Style = csCurrencyStyle;

                #endregion

                #region "Editable Currency Style"

                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;
                try
                {
                    if (C1Cleargage.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = C1Cleargage.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = C1Cleargage.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";
                        csEditableCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = C1Cleargage.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;

                }



                #endregion

                #region "Claim Rows"

                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;
                try
                {
                    if (C1Cleargage.Styles.Contains("cs_ClaimRowStyle"))
                    {
                        csClaimRowStyle = C1Cleargage.Styles["cs_ClaimRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = C1Cleargage.Styles.Add("cs_ClaimRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);

                        csClaimRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }
                }
                catch
                {
                    csClaimRowStyle = C1Cleargage.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                }

                #endregion

                #region "Claim Service Line Rows"

                C1.Win.C1FlexGrid.CellStyle csClaimServiceLineRowStyle;
                try
                {
                    if (C1Cleargage.Styles.Contains("cs_ClaimServiceRowStyle"))
                    {
                        csClaimServiceLineRowStyle = C1Cleargage.Styles["cs_ClaimServiceRowStyle"];
                    }
                    else
                    {
                        csClaimServiceLineRowStyle = C1Cleargage.Styles.Add("cs_ClaimServiceRowStyle");
                        csClaimServiceLineRowStyle.DataType = typeof(System.String);

                        csClaimServiceLineRowStyle.Font = gloGlobal.clsgloFont.gFont;
                        csClaimServiceLineRowStyle.BackColor = Color.FromArgb(252, 253, 255);
                    }

                }
                catch
                {
                    csClaimServiceLineRowStyle = C1Cleargage.Styles.Add("cs_ClaimServiceRowStyle");
                    csClaimServiceLineRowStyle.DataType = typeof(System.String);
                    csClaimServiceLineRowStyle.Font = gloGlobal.clsgloFont.gFont;
                    csClaimServiceLineRowStyle.BackColor = Color.FromArgb(252, 253, 255);
                }

                #endregion

                #region "Cell Button Style"

                string _comboList = "";
                gloAccountsV2.gloPatientPaymentV2 ogloEOBPayPat = new gloAccountsV2.gloPatientPaymentV2();

                C1.Win.C1FlexGrid.CellStyle csEditableAdjustment;
                try
                {
                    if (C1Cleargage.Styles.Contains("cs_EditableAdjustment"))
                    {
                        csEditableAdjustment = C1Cleargage.Styles["cs_EditableAdjustment"];
                    }
                    else
                    {
                        csEditableAdjustment = C1Cleargage.Styles.Add("cs_EditableAdjustment");
                        csEditableAdjustment.DataType = typeof(System.String);
                        csEditableAdjustment.Font = Font_CellStyle;
                        csEditableAdjustment.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableAdjustment = C1Cleargage.Styles.Add("cs_EditableAdjustment");
                    csEditableAdjustment.DataType = typeof(System.String);

                    csEditableAdjustment.Font = Font_CellStyle;
                    csEditableAdjustment.BackColor = Color.White;
                }

                _comboList = ogloEOBPayPat.GetAdjustmentCodes();
                csEditableAdjustment.ComboList = _comboList;
                
                //SLR: Free ogloEOBPayPat
                if (ogloEOBPayPat != null)
                {
                    ogloEOBPayPat.Dispose();
                }

                #endregion

                #region "Claim Service Line Rows for Reserve"

                C1.Win.C1FlexGrid.CellStyle csServiceLineRowStyleForReserve;
                try
                {
                    if (C1Cleargage.Styles.Contains("cs_ServiceRowStyleForReserve"))
                    {
                        csServiceLineRowStyleForReserve = C1Cleargage.Styles["cs_ServiceRowStyleForReserve"];
                    }
                    else
                    {
                        csServiceLineRowStyleForReserve = C1Cleargage.Styles.Add("cs_ServiceRowStyleForReserve");
                        csServiceLineRowStyleForReserve.DataType = typeof(System.String);

                        csServiceLineRowStyleForReserve.Font = gloGlobal.clsgloFont.gFont;
                        csServiceLineRowStyleForReserve.BackColor = Color.FromArgb(255, 228, 181);
                    }

                }
                catch
                {
                    csServiceLineRowStyleForReserve = C1Cleargage.Styles.Add("cs_ServiceRowStyleForReserve");
                    csServiceLineRowStyleForReserve.DataType = typeof(System.String);
                    csServiceLineRowStyleForReserve.Font = gloGlobal.clsgloFont.gFont;
                    csServiceLineRowStyleForReserve.BackColor = Color.FromArgb(255, 228, 181);
                }

                #endregion


                #region "Allow Editing"

                C1Cleargage.AllowEditing = true;

                C1Cleargage.Cols[COL_PACCOUNTID].AllowEditing = false;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_ID].AllowEditing = false;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_DETAILID].AllowEditing = false;
                C1Cleargage.Cols[COL_BILLING_TRANSACTON_LINENO].AllowEditing = false;
                C1Cleargage.Cols[COL_PATIENTID].AllowEditing = false;

                C1Cleargage.Cols[COL_ACCOUNTNO].AllowEditing = false;
                C1Cleargage.Cols[COL_PATIENTNAME].AllowEditing = false;
                C1Cleargage.Cols[COL_TOTALAMOUNT].AllowEditing = false;
                C1Cleargage.Cols[COL_AVAILABLEAMOUNT].AllowEditing = false;
                C1Cleargage.Cols[COL_FROMDOS].AllowEditing = false;

                C1Cleargage.Cols[COL_CLAIM_NUM].AllowEditing = false;
                C1Cleargage.Cols[COL_DOS_FROM].AllowEditing = false;
                C1Cleargage.Cols[COL_CPT_CODE].AllowEditing = false;
                C1Cleargage.Cols[COL_MODIFIER].AllowEditing = false;
                C1Cleargage.Cols[COL_TOTAL_CHARGE].AllowEditing = false;
                C1Cleargage.Cols[COL_TOT_BAL_AMT].AllowEditing = false;
                C1Cleargage.Cols[COL_PAT_DUE].AllowEditing = false;
                C1Cleargage.Cols[COL_DISTRIBUTE_PAYMENT].AllowEditing = true;


                C1Cleargage.Cols[COL_RESERVE_CLOSE_DATE].AllowEditing = false;
                C1Cleargage.Cols[COL_DOS_TO].AllowEditing = false;
                C1Cleargage.Cols[COL_CLAIM_DATE].AllowEditing = false;
                C1Cleargage.Cols[COL_CLAIM_CLOSE_DATE].AllowEditing = false;
                C1Cleargage.Cols[COL_CPT_DESCRIPTON].AllowEditing = false;
                C1Cleargage.Cols[COL_FACILITY_TYPE].AllowEditing = false;
                C1Cleargage.Cols[COL_CLAIM_NO].AllowEditing = false;
                C1Cleargage.Cols[COL_UNIT].AllowEditing = false;
                C1Cleargage.Cols[COL_CHARGE].AllowEditing = false;
                C1Cleargage.Cols[COL_ALLOWED].AllowEditing = false;
                C1Cleargage.Cols[COL_PROVIDERID].AllowEditing = false;
                C1Cleargage.Cols[COL_TRANSACTION_LINESTATUS].AllowEditing = false;
                C1Cleargage.Cols[COL_SENDTOFLAG].AllowEditing = false;
                C1Cleargage.Cols[COL_PREV_PAID].AllowEditing = false;
                C1Cleargage.Cols[COL_PREV_ADJ].AllowEditing = false;
                C1Cleargage.Cols[COL_PREV_PAT_ADJ].AllowEditing = false;
                C1Cleargage.Cols[COL_INS_DUE].AllowEditing = false;
                C1Cleargage.Cols[COL_BadDebt_DUE].AllowEditing = false;
                C1Cleargage.Cols[COL_PREV_PAT_PAID_AMT].AllowEditing = false;
                C1Cleargage.Cols[COL_SPLIT_CLAIM_NO].AllowEditing = false;
                C1Cleargage.Cols[COL_TRACK_TRN_ID].AllowEditing = false;
                C1Cleargage.Cols[COL_TRACK_TRN_DTL_ID].AllowEditing = false;
                C1Cleargage.Cols[COL_TRACK_SUB_CLAIM_NO].AllowEditing = false;
                C1Cleargage.Cols[COL_TRACK_IS_HOLD].AllowEditing = false;
                C1Cleargage.Cols[COL_TRACK_HOLD_INFO].AllowEditing = false;
                C1Cleargage.Cols[COL_TRACK_RES_PARTY].AllowEditing = false;
                C1Cleargage.Cols[COL_NON_SERVICECODE].AllowEditing = false;
              
                C1Cleargage.Cols[COL_ADJUSTMENT_CODE].AllowEditing = true;
                C1Cleargage.Cols[COL_CHECKAMOUNT].AllowEditing = false;
                C1Cleargage.Cols[COL_PAYMENTMETHOD].AllowEditing = false;
                C1Cleargage.Cols[COL_REFERENCENUMBER].AllowEditing = false;
                C1Cleargage.Cols[COL_ENCOUNTERID].AllowEditing = false;
                C1Cleargage.Cols[COL_ACTION].AllowEditing = false;
                #endregion

                C1Cleargage.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                C1Cleargage.ExtendLastCol = true;

                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }
        private void LoadCleargagePaymentOneByOne()
        {

            Int64 nPAccountID = 0;
            try
            {
                C1Cleargage.Select();
                #region "Bind Data"

                var Accounts = new List<KeyValuePair<string, string>>();
                if (rb_ShowAll.Checked == false)
                {
                    if (dtPAccountID != null && dtPAccountID.Rows.Count > 0)
                    {
                        if (dtPAccountID.Rows.Count > 0 && dtPAccountID.Rows.Count > CleargagelistRowCount)
                        {
                            nPAccountID = Convert.ToInt64(dtPAccountID.Rows[CleargagelistRowCount]["nPAccountID"]);
                        }
                        else
                        {
                            tsb_Next.Enabled = false;
                            if (dtPAccountID.Rows.Count > 0 && tsb_Next.Enabled == false)
                            {
                                MessageBox.Show("You have gone through all the accounts. List will be refreshed for all the accounts.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadCleargagePaymentList();
                                rb_ShowAll.Checked = true;
                                rb_ShowOneByOne.Checked = false;
                            }
                            return;
                        }
                    }

                    dtPaymentList = ClsCleargagePaymentPosting.GetCleargagePaymentList(_nCleargageFileID, nPAccountID, sAction.ToUpper(), sPaymentMethod, (Actions.PAYMENT).GetHashCode());
                    DesignClearGagePayment();

                    for (int i = 0; i < dtPaymentList.Rows.Count; i++)
                    {
                        if (nPAccountID == Convert.ToInt64(dtPaymentList.Rows[i]["nPAccountID"]))
                        {
                            if (!Accounts.Contains(new KeyValuePair<string, string>(Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nAmount"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]), Convert.ToString(dtPaymentList.Rows[i]["nFromDate"]))))
                            {
                                #region "MasterData"

                                C1Cleargage.Rows.Add();
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTNO, Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTNAME, Convert.ToString(dtPaymentList.Rows[i]["PatientName"]));

                                if (Convert.ToString(dtPaymentList.Rows[i]["nFromDate"]) == "")
                                {
                                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_FROMDOS, "");
                                }
                                else
                                {
                                    C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_FROMDOS, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtPaymentList.Rows[i]["nFromDate"])).ToString("MM/dd/yyyy"));
                                }
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TOTALAMOUNT, Convert.ToString(dtPaymentList.Rows[i]["nAmount"]));

                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PACCOUNTID, Convert.ToString(dtPaymentList.Rows[i]["nPAccountID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_GUARANTORID, Convert.ToString(dtPaymentList.Rows[i]["nGuarantorID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNT_PATIENT_ID, Convert.ToString(dtPaymentList.Rows[i]["nAccountPatientID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTID, Convert.ToString(dtPaymentList.Rows[i]["nPatientID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, Convert.ToString(dtPaymentList.Rows[i]["nTransactionMasterID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_DETAILID, Convert.ToString(dtPaymentList.Rows[i]["nTransactionMasterDetailID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_LINENO, Convert.ToString(dtPaymentList.Rows[i]["nTransactionLineNo"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ENCOUNTERID, Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CHECKAMOUNT, Convert.ToString(dtPaymentList.Rows[i]["nAmount"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAYMENTMETHOD, Convert.ToString(dtPaymentList.Rows[i]["PaymentMethod"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAYMENTPLANID, Convert.ToString(dtPaymentList.Rows[i]["PaymentPlanID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_TRANSACTIONID, Convert.ToString(dtPaymentList.Rows[i]["TransactionID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_ORIGINALTRANSACTIONID, Convert.ToString(dtPaymentList.Rows[i]["OriginalTransactionID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TIMESTAMP, Convert.ToDateTime(dtPaymentList.Rows[i]["Timestamp"]).ToString("MM/dd/yyyy"));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_SERVICEDATE, Convert.ToString(dtPaymentList.Rows[i]["ServiceDate"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BRANCHID, Convert.ToString(dtPaymentList.Rows[i]["BranchID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLIENTID, Convert.ToString(dtPaymentList.Rows[i]["ClientID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ORIGIN, Convert.ToString(dtPaymentList.Rows[i]["Origin"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_USER, Convert.ToString(dtPaymentList.Rows[i]["User"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE, Convert.ToString(dtPaymentList.Rows[i]["TxnType"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_REFERENCENUMBER, Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_APPROVALCODE, Convert.ToString(dtPaymentList.Rows[i]["ApprovalCode"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_NATIONALPROVIDERID, Convert.ToString(dtPaymentList.Rows[i]["NationalProviderID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ENTRYMETHOD, Convert.ToString(dtPaymentList.Rows[i]["EntryMethod"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTTYPE, Convert.ToString(dtPaymentList.Rows[i]["AccountType"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_ACCOUNTNUMBER, Convert.ToString(dtPaymentList.Rows[i]["AccountNumber"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTNAME, Convert.ToString(dtPaymentList.Rows[i]["AccountName"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_LINESTATUS, Convert.ToString(dtPaymentList.Rows[i]["nLineStatus"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTCODE, Convert.ToString(dtPaymentList.Rows[i]["PatientCode"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACTION, Convert.ToString(dtPaymentList.Rows[i]["Action"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_R_ENCOUNTERID, Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]));
                                C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_DISTINCTACCOUNTNO, Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nAmount"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]));

                                Accounts.Add(new KeyValuePair<string, string>(Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nAmount"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]), Convert.ToString(dtPaymentList.Rows[i]["nFromDate"])));

                                #endregion

                                #region "Style For Master Data"

                                C1Cleargage.Rows[C1Cleargage.Rows.Count - 1].Style = C1Cleargage.Styles["cs_ClaimRowStyle"];
                               

                                #endregion
                            }
                            if ((Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY) || Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.OTHER) || Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || Convert.ToString(C1Cleargage.GetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE)))
                            {
                                continue;
                            }
                            #region "Sub Data"

                            C1Cleargage.Rows.Add();
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PACCOUNTID, Convert.ToString(dtPaymentList.Rows[i]["nPAccountID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_GUARANTORID, Convert.ToString(dtPaymentList.Rows[i]["nGuarantorID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNT_PATIENT_ID, Convert.ToString(dtPaymentList.Rows[i]["nAccountPatientID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTID, Convert.ToString(dtPaymentList.Rows[i]["nPatientID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, Convert.ToString(dtPaymentList.Rows[i]["nTransactionMasterID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_DETAILID, Convert.ToString(dtPaymentList.Rows[i]["nTransactionMasterDetailID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BILLING_TRANSACTON_LINENO, Convert.ToString(dtPaymentList.Rows[i]["nTransactionLineNo"]));

                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLAIM_NUM, Convert.ToString(dtPaymentList.Rows[i]["SplitClaimNo"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtPaymentList.Rows[i]["nFromDate"])).ToString("MM/dd/yyyy"));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CPT_CODE, Convert.ToString(dtPaymentList.Rows[i]["sCPTCode"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_MODIFIER, Convert.ToString(dtPaymentList.Rows[i]["Modifier"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TOTAL_CHARGE, Convert.ToString(dtPaymentList.Rows[i]["dTotal"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TOT_BAL_AMT, Convert.ToString(dtPaymentList.Rows[i]["TotalBalanceAmount"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAT_DUE, Convert.ToString(dtPaymentList.Rows[i]["PatientDue"]));


                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_DOS_TO, Convert.ToString(dtPaymentList.Rows[i]["nToDate"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLAIM_DATE, Convert.ToString(dtPaymentList.Rows[i]["ClaimDate"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLAIM_CLOSE_DATE, Convert.ToString(dtPaymentList.Rows[i]["nCloseDate"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_FACILITY_TYPE, Convert.ToString(dtPaymentList.Rows[i]["nFacilityType"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLAIM_NO, Convert.ToString(dtPaymentList.Rows[i]["nClaimNo"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CPT_DESCRIPTON, Convert.ToString(dtPaymentList.Rows[i]["sCPTDescription"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CHARGE, Convert.ToString(dtPaymentList.Rows[i]["dCharges"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_UNIT, Convert.ToString(dtPaymentList.Rows[i]["dUnit"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ALLOWED, Convert.ToString(dtPaymentList.Rows[i]["dAllowed"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PROVIDERID, Convert.ToString(dtPaymentList.Rows[i]["nProvider"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRANSACTION_LINESTATUS, Convert.ToString(dtPaymentList.Rows[i]["nTransactionLineStatus"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_SENDTOFLAG, Convert.ToString(dtPaymentList.Rows[i]["nSendToFlag"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PREV_PAID, Convert.ToString(dtPaymentList.Rows[i]["PreviousPaid"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PREV_ADJ, Convert.ToString(dtPaymentList.Rows[i]["PreviousAdjuestment"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PREV_PAT_ADJ, Convert.ToString(dtPaymentList.Rows[i]["PrevPatAdj"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BadDebt_DUE, Convert.ToString(dtPaymentList.Rows[i]["BadDebtDue"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PREV_PAT_PAID_AMT, Convert.ToString(dtPaymentList.Rows[i]["PreviousPatientPaidAmount"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_SPLIT_CLAIM_NO, Convert.ToString(dtPaymentList.Rows[i]["SplitClaimNo"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_TRN_ID, Convert.ToString(dtPaymentList.Rows[i]["TrackTransactionID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_TRN_DTL_ID, Convert.ToString(dtPaymentList.Rows[i]["TrackTransactionDetailID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_SUB_CLAIM_NO, Convert.ToString(dtPaymentList.Rows[i]["TrackSubClaimNo"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_IS_HOLD, Convert.ToString(dtPaymentList.Rows[i]["TrackIsHold"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_HOLD_INFO, Convert.ToString(dtPaymentList.Rows[i]["TrackHoldInfo"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TRACK_RES_PARTY, Convert.ToString(dtPaymentList.Rows[i]["RespParty"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_NON_SERVICECODE, Convert.ToString(dtPaymentList.Rows[i]["bNonServiceCode"]));
                            //C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACTION, Convert.ToString(dtPaymentList.Rows[i]["Action"]));
                            //C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ENCOUNTERID, Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_DISTINCTACCOUNTNO, Convert.ToString(dtPaymentList.Rows[i]["sAccountNo"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nAmount"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]) + "_" + Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]));

                         //   C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAYMENTMETHOD, Convert.ToString(dtPaymentList.Rows[i]["PaymentMethod"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PAYMENTPLANID, Convert.ToString(dtPaymentList.Rows[i]["PaymentPlanID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_TRANSACTIONID, Convert.ToString(dtPaymentList.Rows[i]["TransactionID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_ORIGINALTRANSACTIONID, Convert.ToString(dtPaymentList.Rows[i]["OriginalTransactionID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TIMESTAMP, Convert.ToDateTime(dtPaymentList.Rows[i]["Timestamp"]).ToString("MM/dd/yyyy"));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_SERVICEDATE, Convert.ToDateTime(dtPaymentList.Rows[i]["ServiceDate"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_BRANCHID, Convert.ToString(dtPaymentList.Rows[i]["BranchID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CLIENTID, Convert.ToString(dtPaymentList.Rows[i]["ClientID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ORIGIN, Convert.ToString(dtPaymentList.Rows[i]["Origin"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_USER, Convert.ToString(dtPaymentList.Rows[i]["User"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_TxnTYPE, Convert.ToString(dtPaymentList.Rows[i]["TxnType"]));
                            //C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_REFERENCENUMBER, Convert.ToString(dtPaymentList.Rows[i]["ReferenceNumber"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_APPROVALCODE, Convert.ToString(dtPaymentList.Rows[i]["ApprovalCode"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_NATIONALPROVIDERID, Convert.ToString(dtPaymentList.Rows[i]["NationalProviderID"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ENTRYMETHOD, Convert.ToString(dtPaymentList.Rows[i]["EntryMethod"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTTYPE, Convert.ToString(dtPaymentList.Rows[i]["AccountType"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_CG_ACCOUNTNUMBER, Convert.ToString(dtPaymentList.Rows[i]["AccountNumber"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_ACCOUNTNAME, Convert.ToString(dtPaymentList.Rows[i]["AccountName"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_LINESTATUS, Convert.ToString(dtPaymentList.Rows[i]["nLineStatus"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_PATIENTCODE, Convert.ToString(dtPaymentList.Rows[i]["PatientCode"]));
                            C1Cleargage.SetData(C1Cleargage.Rows.Count - 1, COL_R_ENCOUNTERID, Convert.ToString(dtPaymentList.Rows[i]["nEncounterID"]));
                            


                            #endregion

                            #region "Style For Sub Data"

                            C1Cleargage.Rows[C1Cleargage.Rows.Count - 1].Style = C1Cleargage.Styles["cs_ClaimServiceRowStyle"];
                            C1Cleargage.SetCellStyle(C1Cleargage.Rows.Count - 1, COL_DISTRIBUTE_PAYMENT, "cs_EditableCurrencyStyle");
                            C1Cleargage.SetCellStyle(C1Cleargage.Rows.Count - 1, COL_ADJUSTMENT_CODE, "cs_EditableAdjustment");
                            C1Cleargage.SetCellStyle(C1Cleargage.Rows.Count - 1, COL_ADJUSTMENT_AMT, "cs_EditableCurrencyStyle");
                            #endregion
                        }
                    }
                }



                #endregion

                if (dtPAccountID != null)
                {
                    if (dtPAccountID.Rows.Count == CleargagelistRowCount + 1)
                    {
                        tsb_Next.Enabled = false;
                        tsb_Save.Text = "&Save";
                        tsb_Save.ToolTipText = "Save";
                    }
                    else
                    {
                        tsb_Next.Enabled = true;
                        tsb_Save.Text = "&Save && Next";
                        tsb_Save.ToolTipText = "Save & Next";
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
                if (dtPaymentList != null)
                {
                    dtPaymentList = null;
                }
            }
        }

        private void tsb_Next_Click(object sender, EventArgs e)
        {
            try
            {
                C1Cleargage.Select();
                CleargagelistRowCount++;
                LoadCleargagePaymentOneByOne();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }

        }

        private void rb_ShowAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rb_ShowAll.Checked)
                {
                    tsb_Next.Visible = false;
                    tsb_Save.Text = "&Save";
                    tsb_Save.ToolTipText = "Save";

                    rb_ShowAll.Font = new Font(rb_ShowAll.Font, FontStyle.Bold);
                    LoadCleargagePaymentList();
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Select, "Cleargage Payment Select Show one by one ", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);
                }
                else
                {
                    rb_ShowAll.Font = new Font(rb_ShowAll.Font, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Select, "Exception occured while Cleargage Payment Select Show one by one : "+ex.ToString(), 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                ex = null;
            }
            
        }

        private void tsb_AutoDistributeCleargagePayment_Click(object sender, EventArgs e)
        {
            try
            {
                C1Cleargage.Select();
                if (C1Cleargage.Rows.Count > 0)
                {
                    this.C1Cleargage.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
                    if (sAction == Convert.ToString(Actions.PAYMENT))
                    {
                        for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                        {
                            Int64 _PAccountID = 0;
                            decimal dLineamt = 0;
                            decimal dAvailablePayment = 0;
                            string dtFromDOS = string.Empty;
                            string sDistinctAccountNo = string.Empty;

                            #region "Auto Distribution"

                            if (Convert.ToString(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) != null && Convert.ToString(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) != "")
                            {
                                _PAccountID = Convert.ToInt64(C1Cleargage.GetData(i, COL_PACCOUNTID));
                                dAvailablePayment = Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT));
                                sDistinctAccountNo = Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO));
                                if (Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) != "")
                                {
                                    dtFromDOS = Convert.ToDateTime(C1Cleargage.GetData(i, COL_FROMDOS)).ToString("MM/dd/yyyy");
                                }
                                else
                                {
                                    dtFromDOS = "";
                                }

                                for (int j = 1; j < C1Cleargage.Rows.Count; j++)
                                {
                                    if (_PAccountID == Convert.ToInt64(C1Cleargage.GetData(j, COL_PACCOUNTID)) && dtFromDOS == Convert.ToString(C1Cleargage.GetData(j, COL_DOS_FROM)) && Convert.ToDecimal(C1Cleargage.GetData(j, COL_DISTRIBUTE_PAYMENT)) == 0 && Convert.ToDecimal(C1Cleargage.GetData(j, COL_TOTALAMOUNT)) == 0 && sDistinctAccountNo==Convert.ToString(C1Cleargage.GetData(j,COL_DISTINCTACCOUNTNO)))
                                    {
                                        #region "Distribute amount "

                                        if (dAvailablePayment == 0)
                                        {
                                            C1Cleargage.SetData(j, COL_DISTRIBUTE_PAYMENT, dAvailablePayment);
                                        }
                                        if (dAvailablePayment > 0)
                                        {
                                            if (Convert.ToDecimal(C1Cleargage.GetData(j, COL_PAT_DUE)) > 0)
                                            {
                                                dLineamt = Convert.ToDecimal(C1Cleargage.GetData(j, COL_PAT_DUE));
                                            }
                                            else
                                            {
                                                dLineamt = 0;
                                            }

                                            if (dAvailablePayment > dLineamt)
                                            {
                                                dAvailablePayment = dAvailablePayment - dLineamt;
                                                C1Cleargage.SetData(j, COL_DISTRIBUTE_PAYMENT, dLineamt);
                                            }
                                            else
                                            {
                                                C1Cleargage.SetData(j, COL_DISTRIBUTE_PAYMENT, dAvailablePayment);
                                                dAvailablePayment = 0;
                                            }
                                        }

                                        #endregion
                                    }
                                }

                                #region "Set Available Payment after Distribution"

                                if (_PAccountID == Convert.ToInt64(C1Cleargage.GetData(i, COL_PACCOUNTID)))
                                {
                                    C1Cleargage.SetData(i, COL_TOTALAMOUNT, dAvailablePayment);
                                }

                                #endregion
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                        {
                            Int64 _PAccountID = 0;
                            decimal dLineamt = 0;
                            decimal dAvailableAdjustment = 0;
                            string dtFromDOS = string.Empty;
                            string sDistinctAccountNo = string.Empty;

                            #region "Auto Distribution"

                            if (Convert.ToString(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) != null && Convert.ToString(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) != "")
                            {
                                _PAccountID = Convert.ToInt64(C1Cleargage.GetData(i, COL_PACCOUNTID));
                                dAvailableAdjustment = Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT));
                                sDistinctAccountNo = Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO));
                                if (Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) != "")
                                {
                                    dtFromDOS = Convert.ToDateTime(C1Cleargage.GetData(i, COL_FROMDOS)).ToString("MM/dd/yyyy");
                                }
                                else
                                {
                                    dtFromDOS = "";
                                }

                                for (int j = 1; j < C1Cleargage.Rows.Count; j++)
                                {
                                    if (_PAccountID == Convert.ToInt64(C1Cleargage.GetData(j, COL_PACCOUNTID)) && dtFromDOS == Convert.ToString(C1Cleargage.GetData(j, COL_DOS_FROM)) && Convert.ToDecimal(C1Cleargage.GetData(j, COL_ADJUSTMENT_AMT)) == 0 && Convert.ToDecimal(C1Cleargage.GetData(j, COL_TOTALAMOUNT)) == 0 && sDistinctAccountNo==Convert.ToString(C1Cleargage.GetData(j,COL_DISTINCTACCOUNTNO)))
                                    {
                                        #region "Distribute amount "

                                        if (dAvailableAdjustment == 0)
                                        {
                                            C1Cleargage.SetData(j, COL_ADJUSTMENT_AMT, dAvailableAdjustment);
                                        }
                                        if (dAvailableAdjustment > 0)
                                        {
                                            if (Convert.ToDecimal(C1Cleargage.GetData(j, COL_PAT_DUE)) > 0)
                                            {
                                                dLineamt = Convert.ToDecimal(C1Cleargage.GetData(j, COL_PAT_DUE));
                                            }
                                            else
                                            {
                                                dLineamt = 0;
                                            }

                                            if (dAvailableAdjustment > dLineamt)
                                            {
                                                dAvailableAdjustment = dAvailableAdjustment - dLineamt;
                                                C1Cleargage.SetData(j, COL_ADJUSTMENT_AMT, dLineamt);
                                            }
                                            else
                                            {
                                                C1Cleargage.SetData(j, COL_ADJUSTMENT_AMT, dAvailableAdjustment);
                                                dAvailableAdjustment = 0;
                                            }
                                        }

                                        #endregion
                                    }
                                }

                                #region "Set Available adjustment after Distribution"

                                if (_PAccountID == Convert.ToInt64(C1Cleargage.GetData(i, COL_PACCOUNTID)))
                                {
                                    C1Cleargage.SetData(i, COL_TOTALAMOUNT, dAvailableAdjustment);
                                }

                                #endregion
                            }

                            #endregion
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                this.C1Cleargage.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (C1Cleargage.Rows.Count > 1)
                {
                    if (sAction == Convert.ToString(Actions.PAYMENT))// && sPaymentMethod == Convert.ToString(PaymentMethod.CREDIT))
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Cleargage Payment Save Start ", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);
                        SaveCleargagePayment();
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Cleargage Payment Save End ", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Save, "Cleargage Discount Save Start ", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                        SaveCleargageAdjustment();
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Save, "Cleargage Discount Save End ", 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Save, "Exception occured while Cleargage Payment Save : "+ex.ToString(), 0, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
            }


        }

        private void SaveCleargagePayment()
        {
            Int64 _PAccountID = 0;
            Int64 SelectedPaymentTrayID = 0;
            string sSelectedPaymentTray = string.Empty;
            frmPaymentTransferInfo ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
            ClsCleargagePaymentPosting oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            string sAccountID = string.Empty;
            List<string> Acc = new List<string>();
            DateTime dtCloseDate = DateTime.MinValue;
            gloAccountPayment.BulkPaymentOperation bulkWriteOff = null;
            decimal TotalCheckAmount = 0;
            List<string> EncounterID = new List<string>();
            string sEncounterID = string.Empty;
            Int64 returnPaymentID = 0;
            Int64 PatientID = 0;
            string sActions= string.Empty;
            string TransactionID = string.Empty;
            string OriginalTransactionID = string.Empty;
            string PatientName=string.Empty;
            DateTime TimeStamp = DateTime.MaxValue;
            string AccountType =string.Empty;
            string AccountNo = string.Empty;
            string ReferenceNo = string.Empty;
            string PlanID = string.Empty;
            string PaymentMethod = string.Empty;
            string PatientCode = string.Empty;
            string sAction = string.Empty;
            try
            {
                C1Cleargage.Select();
                #region Check Amount is Distributed or Not

                if (C1Cleargage.Rows.Count > 1)
                {
                    bool IsAmountDistribute = false;
                    for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_DISTRIBUTE_PAYMENT)) != 0 || (Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.OTHER) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE)))
                        {
                            IsAmountDistribute = true;
                            break;
                        }
                        else
                        {
                            IsAmountDistribute = false;
                        }

                    }
                    if (IsAmountDistribute == false)
                    {
                        MessageBox.Show("No payment has been made to save.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                #endregion

                    #region "Accounts"

                    for (int j = 1; j < C1Cleargage.Rows.Count; j++)
                    {
                        if (Convert.ToInt64(C1Cleargage.GetData(j, COL_PACCOUNTID)) != 0 && Convert.ToDecimal(C1Cleargage.GetData(j, COL_TOTALAMOUNT)) == 0 && (Convert.ToDecimal(C1Cleargage.GetData(j, COL_DISTRIBUTE_PAYMENT)) != 0) || (Convert.ToString(C1Cleargage.GetData(j, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY) || Convert.ToString(C1Cleargage.GetData(j, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.OTHER) || Convert.ToString(C1Cleargage.GetData(j, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || Convert.ToString(C1Cleargage.GetData(j, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || Convert.ToString(C1Cleargage.GetData(j, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE)))
                        {
                            
                            if (sAccountID != string.Empty && !Acc.Contains(Convert.ToString(C1Cleargage.GetData(j, COL_DISTINCTACCOUNTNO))))
                            {
                                sAccountID = sAccountID + "," + Convert.ToString(C1Cleargage.GetData(j, COL_PACCOUNTID));
                                Acc.Add(Convert.ToString(C1Cleargage.GetData(j, COL_DISTINCTACCOUNTNO)));
                            }
                            else if (sAccountID == string.Empty)
                            {
                                sAccountID = Convert.ToString(C1Cleargage.GetData(j, COL_PACCOUNTID));
                                Acc.Add(Convert.ToString(C1Cleargage.GetData(j, COL_DISTINCTACCOUNTNO)));
                            }
                        }
                    }

                    #endregion                                        

                    #region "Payment Tray"

                    if (SelectedPaymentTrayID == 0)
                    {
                        ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
                        ofrmPaymentTransferInfo.CleargagePosting = "PostPayment";
                        ofrmPaymentTransferInfo.AccountIDs = sAccountID;
                        ofrmPaymentTransferInfo.ShowDialog();
                        if (ofrmPaymentTransferInfo.PaymentTrayID > 0)
                        {
                            sSelectedPaymentTray = ofrmPaymentTransferInfo.PaymentTrayName;
                            SelectedPaymentTrayID = ofrmPaymentTransferInfo.PaymentTrayID;
                        }
                        else
                        {
                            sSelectedPaymentTray = string.Empty;
                            SelectedPaymentTrayID = 0;
                        }
                    }
                    if (ofrmPaymentTransferInfo.PaymentTransferCloseDate == string.Empty || SelectedPaymentTrayID == 0)
                    {
                        return;
                    }

                    #endregion


                    #region DataTable
                    foreach (object o in Acc)
                    {
                        gloAccountPayment.PaymentInfoParameter paymentParameter = null;
                        gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
                        DataTable dtChargeLineDetails = new DataTable();

                        #region "Columns"

                        dtChargeLineDetails.Columns.Add("nTransactionDetailID");
                        dtChargeLineDetails.Columns.Add("nTransactionLineNo");
                        dtChargeLineDetails.Columns.Add("ClaimNumber");
                        dtChargeLineDetails.Columns.Add("nFromDate");
                        dtChargeLineDetails.Columns.Add("nToDate");
                        dtChargeLineDetails.Columns.Add("sCPTCode");
                        dtChargeLineDetails.Columns.Add("ClaimDate");
                        dtChargeLineDetails.Columns.Add("nTransactionID");
                        dtChargeLineDetails.Columns.Add("nTransactionDate");
                        dtChargeLineDetails.Columns.Add("nFacilityType");
                        dtChargeLineDetails.Columns.Add("nClaimNo");
                        dtChargeLineDetails.Columns.Add("nPatientName");
                        dtChargeLineDetails.Columns.Add("nPatientID");
                        dtChargeLineDetails.Columns.Add("Modifier");
                        dtChargeLineDetails.Columns.Add("sCPTDescription");
                        dtChargeLineDetails.Columns.Add("dCharges");
                        dtChargeLineDetails.Columns.Add("dUnit");
                        dtChargeLineDetails.Columns.Add("dTotal");
                        dtChargeLineDetails.Columns.Add("dAllowed");
                        dtChargeLineDetails.Columns.Add("nProviderID");
                        dtChargeLineDetails.Columns.Add("nTransactionLineStatus");
                        dtChargeLineDetails.Columns.Add("nSendToFlag");
                        dtChargeLineDetails.Columns.Add("TotalBalanceAmount");
                        dtChargeLineDetails.Columns.Add("PreviousPaid");
                        dtChargeLineDetails.Columns.Add("PreviousAdjuestment");
                        dtChargeLineDetails.Columns.Add("PrevPatAdj");
                        dtChargeLineDetails.Columns.Add("PatientDue");
                        dtChargeLineDetails.Columns.Add("BadDebtDue");
                        dtChargeLineDetails.Columns.Add("PreviousPatientPaidAmount");
                        dtChargeLineDetails.Columns.Add("SplitClaimNo");
                        dtChargeLineDetails.Columns.Add("TrackTransactionID");
                        dtChargeLineDetails.Columns.Add("TrackTransactionDetailID");
                        dtChargeLineDetails.Columns.Add("TrackSubClaimNo");
                        dtChargeLineDetails.Columns.Add("TrackIsHold");
                        dtChargeLineDetails.Columns.Add("TrackHoldInfo");
                        dtChargeLineDetails.Columns.Add("RespParty");
                        dtChargeLineDetails.Columns.Add("bNonServiceCode");
                        dtChargeLineDetails.Columns.Add("Action");
                        dtChargeLineDetails.Columns.Add("dPayment");
                        dtChargeLineDetails.Columns.Add("AdjustmentCode");
                        dtChargeLineDetails.Columns.Add("AdjustmentDesc");
                        dtChargeLineDetails.Columns.Add("AdjustmentAmount");
                        dtChargeLineDetails.Columns.Add("ReferenceNo");
                        dtChargeLineDetails.Columns.Add("AccountNo");
                        dtChargeLineDetails.Columns.Add("AccountType");
                        dtChargeLineDetails.Columns.Add("PaymentMethod");
                        dtChargeLineDetails.Columns.Add("CGTransactionID");
                        dtChargeLineDetails.Columns.Add("CGOriginalTransactionID");
                        dtChargeLineDetails.Columns.Add("TimeStamp");
                        dtChargeLineDetails.Columns.Add("PaymentPlanID");
                        dtChargeLineDetails.Columns.Add("TxnType");
                        #endregion

                        string _sDistPAccountID = Convert.ToString(o);
                        string sReserveDetails = string.Empty;
                        for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                        {
                            if (Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO)) == _sDistPAccountID)
                            {
                                if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) > 0 && Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) != "" || (Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.OTHER) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE)))
                                {
                                    if ((Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) != 0) || (Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.OTHER) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE)))
                                    {
                                        string sProviderName = "";
                                        string sProviderID = "0";
                                        DataTable _dtDefaultPatientProvider = new DataTable();
                                        _dtDefaultPatientProvider = gloAccountsV2.gloBillingCommonV2.GetDefaultPatientProvider(Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)));
                                        if (_dtDefaultPatientProvider != null && _dtDefaultPatientProvider.Rows.Count > 0)
                                        {
                                            sProviderName = Convert.ToString(_dtDefaultPatientProvider.Rows[0]["ProviderName"]);
                                            sProviderID = Convert.ToString(_dtDefaultPatientProvider.Rows[0]["nProviderID"]);
                                        }
                                        if (Convert.ToInt64(sProviderID) == 0)
                                        {
                                            MessageBox.Show("Reserve can not created for account #: " + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)) + " and reference #: " + Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER)) + " as patient provider is not assign/inactive.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Reserve not created for account# : " + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)), Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)), _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);
                                            break;
                                        }
                                        else if ((Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.OTHER) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE)))
                                        {
                                            if (Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY))
                                            {
                                                if (MessageBox.Show("Copay reserve will be created for account #:" + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)) + " with reference #:" + Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER)) + ". \nDo you want to put this amount into reserve ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                                {
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "NO is selected for \"Putting payment amount into copay reserve \" for account# : " + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)), Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)), _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                                    break;
                                                }

                                            }
                                            else
                                            {
                                                if (MessageBox.Show("Other reserve will be created  for account #:" + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)) + " with reference #:" + Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER)) + ". \nDo you want to put this amount into reserve ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                                {
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "NO is selected for \"Putting payment amount into other reserve \" for account# : " + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)), Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)), _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                                    break;
                                                }
                                            }
                                        }
                                        else if (MessageBox.Show("Reserve remaining available amount for account #:" + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)) + " with reference #:" + Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER)) + ". \nDo you want to put remaining amount into reserve ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "NO is selected for \"Putting remaining amount into reserve \" for account# : " + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)), Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)), _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                            break;
                                        }
                                        #region "if Reserve Created"
                                        if (Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY))
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Yes is selected for \"Putting remaining amount into copay reserve \" for account# : " + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)), Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)), _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                        }
                                        else if (Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.OTHER) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE))
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Yes is selected for \"Putting remaining amount into other reserve \" for account# : " + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)), Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)), _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                        }
                                        else
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Yes is selected for \"Putting remaining amount into reserve \" for account# : " + Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO)), Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)), _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                        }
                                        #endregion
                                        decimal ReserveAmount = Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT));
                                        string sReserveNote = "";
                                        if (Convert.ToString(C1Cleargage.GetData(i, COL_ENCOUNTERID)) != "")
                                        {
                                            sReserveNote = "Remaining amount from cleargage ref# :" + Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER)) + " date :" + Convert.ToString(C1Cleargage.GetData(i, COL_TIMESTAMP)) + " for encounter Id :" + Convert.ToString(C1Cleargage.GetData(i, COL_ENCOUNTERID)) + " has been reserved.";
                                        }
                                        else
                                        {
                                            sReserveNote = "Payment amount from cleargage ref# :" + Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER)) + " date :" + Convert.ToString(C1Cleargage.GetData(i, COL_TIMESTAMP)) + " has been reserved.";
                                        }
                                        string sReserveSubType = "";
                                        if (Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY))
                                        { sReserveSubType = Convert.ToString(gloAccountsV2.NoteSubTypeV2.Copay.GetHashCode()); }//"2";}
                                        else
                                        { sReserveSubType = Convert.ToString(gloAccountsV2.NoteSubTypeV2.Other.GetHashCode()); }//"2";}

                                        string sReserveNoteOnPrint = "";

                                        string AdvCPT = "";
                                        string AdvICD9 = "";
                                        string dtReserveForDOS = Convert.ToString(DateTime.MinValue);

                                        sReserveDetails = ReserveAmount + "~" + sReserveNote + "~" + sReserveSubType + "~" + sReserveNoteOnPrint + "~" + AdvCPT + "~" + AdvICD9 + "~" + sProviderName + "~" + sProviderID + "~" + dtReserveForDOS;
                                    }

                                }
                                if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) != 0 && Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) != "")
                                {
                                    TotalCheckAmount = Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT));//- Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT));
                                    PatientName = Convert.ToString(C1Cleargage.GetData(i, COL_PATIENTNAME));
                                    TransactionID = Convert.ToString(C1Cleargage.GetData(i, COL_CG_TRANSACTIONID));
                                    OriginalTransactionID = Convert.ToString(C1Cleargage.GetData(i, COL_CG_ORIGINALTRANSACTIONID));
                                    PlanID = Convert.ToString(C1Cleargage.GetData(i, COL_PAYMENTPLANID));
                                    TimeStamp = Convert.ToDateTime(C1Cleargage.GetData(i, COL_TIMESTAMP));
                                    AccountType = Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTTYPE));
                                    AccountNo = Convert.ToString(C1Cleargage.GetData(i, COL_CG_ACCOUNTNUMBER));
                                    ReferenceNo = Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER));
                                    PaymentMethod = Convert.ToString(C1Cleargage.GetData(i, COL_PAYMENTMETHOD));
                                    PatientCode = Convert.ToString(C1Cleargage.GetData(i, COL_PATIENTCODE));
                                    sEncounterID = Convert.ToString(C1Cleargage.GetData(i, COL_ENCOUNTERID));
                                    sAction = Convert.ToString(C1Cleargage.GetData(i, COL_ACTION));
                                }



                                dtCloseDate = Convert.ToDateTime(ofrmPaymentTransferInfo.PaymentTransferCloseDate);

                                if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_DISTRIBUTE_PAYMENT)) != 0 || (Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.COPAY) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.OTHER) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)).ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE)))
                                {
                                    paymentParameter = new gloAccountPayment.PaymentInfoParameter(
                                        Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)),
                                        Convert.ToInt64(C1Cleargage.GetData(i, COL_PACCOUNTID)),
                                        Convert.ToInt64(C1Cleargage.GetData(i, COL_GUARANTORID)),
                                        Convert.ToInt64(C1Cleargage.GetData(i, COL_ACCOUNT_PATIENT_ID)),
                                        dtCloseDate,
                                        SelectedPaymentTrayID,
                                        sSelectedPaymentTray,
                                        "0.00",
                                        "0.00",
                                        TotalCheckAmount == 0 ? "0.00" : Convert.ToString(TotalCheckAmount)
                                        );


                                    Decimal dDistributeamt = Convert.ToDecimal(C1Cleargage.GetData(i, COL_DISTRIBUTE_PAYMENT));
                                    PatientID = paymentParameter.PatientID;
                                    //sEncounterID = Convert.ToString(C1Cleargage.GetData(i, COL_ENCOUNTERID));

                                    #region "DataTable"

                                    dtChargeLineDetails.Rows.Add(Convert.ToString(C1Cleargage.GetData(i, COL_BILLING_TRANSACTON_DETAILID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_BILLING_TRANSACTON_LINENO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CLAIM_NUM)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_DOS_FROM)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_DOS_TO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CPT_CODE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CLAIM_DATE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_BILLING_TRANSACTON_ID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CLAIM_CLOSE_DATE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_FACILITY_TYPE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CLAIM_NO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PATIENTNAME)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PATIENTID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_MODIFIER)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CPT_DESCRIPTON)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CHARGE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_UNIT)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TOTAL_CHARGE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ALLOWED)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PROVIDERID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRANSACTION_LINESTATUS)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_SENDTOFLAG)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TOT_BAL_AMT)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PREV_PAID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PREV_ADJ)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PREV_PAT_ADJ)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PAT_DUE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_BadDebt_DUE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PREV_PAT_PAID_AMT)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_SPLIT_CLAIM_NO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_TRN_ID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_TRN_DTL_ID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_SUB_CLAIM_NO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_IS_HOLD)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_HOLD_INFO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_RES_PARTY)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_NON_SERVICECODE)),
                                        //Convert.ToString(C1Cleargage.GetData(i, COL_ACTION)),
                                        sAction,
                                        Convert.ToString(C1Cleargage.GetData(i, COL_DISTRIBUTE_PAYMENT)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_CODE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_DESC)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_AMT)),
                                        //Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER)),
                                        ReferenceNo,
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CG_ACCOUNTNUMBER)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTTYPE)),
                                        //Convert.ToString(C1Cleargage.GetData(i,COL_PAYMENTMETHOD)),
                                        PaymentMethod,
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CG_TRANSACTIONID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CG_ORIGINALTRANSACTIONID)),
                                        Convert.ToDateTime(C1Cleargage.GetData(i, COL_TIMESTAMP)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PAYMENTPLANID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)));
                                    #endregion
                                }

                            }
                        }
                        if (paymentParameter != null && dtChargeLineDetails.Rows.Count > 0 && dtChargeLineDetails != null)
                        {
                            #region "Save in TVP"

                            bulkWriteOff = new gloAccountPayment.BulkPaymentOperation();
                            returnPaymentID = bulkWriteOff.AutoCleargagePaymentDistribution(paymentParameter, dtChargeLineDetails, paymentParameter.PatientID, sReserveDetails);
                            #endregion

                            if (returnPaymentID != 0)
                            {
                                string _message = "Auto Cleargage Payment for encounter ID : " + sEncounterID + " and System credit ID : " + returnPaymentID;
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, _message, paymentParameter.PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);

                                //Update Stauts as Posted Of Perticular Claims
                                bool Result = false;
                                Result = oclsCleargagePaymentPosting.UpdateStatusAsPosted(sEncounterID, _nCleargageFileID, TransactionID, OriginalTransactionID, ReferenceNo, sAction);
                                if (Result == true)
                                {
                                    string _Statusmessage = "Auto Cleargage payment file status updated for encounter ID : " + sEncounterID + " and System credit ID : " + returnPaymentID;
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Modify, _Statusmessage, paymentParameter.PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                }

                                Int64 returnCGTransactionID = 0;
                                returnCGTransactionID = oclsCleargagePaymentPosting.SavePaymentPostingDetails(returnCGTransactionID, PatientCode, PatientName, PlanID, TransactionID, OriginalTransactionID, TotalCheckAmount, PaymentMethod, sAction, TimeStamp, "", "", AccountType, AccountNo, ReferenceNo, returnPaymentID, sEncounterID, _nCleargageFileID);
                                if (returnCGTransactionID > 0)
                                {
                                    string _Detailsmessage = "Auto Cleargage payment save credit details for encounter ID : " + sEncounterID + " and System credit ID : " + returnPaymentID;
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, _Detailsmessage, paymentParameter.PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                }
                            }
                        }
                    }
                    //Update file status as Posted if All Cleargage payment posted
                    bool Result1=oclsCleargagePaymentPosting.UpdateMasterDetailsStatus(_nCleargageFileID);
                    if (Result1 == true)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Modify, "Auto Cleargage payment file status updated for CleargageFileID : " + _nCleargageFileID, PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                    }
                    #endregion

                    if (rb_ShowAll.Checked == true)
                    {
                        LoadCleargagePaymentList();
                    }
                    else
                    {
                        CleargagelistRowCount++;
                        LoadCleargagePaymentOneByOne();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Exception occured while Cleargage Payment for encounter IDs : " + sEncounterID + " and System credit ID : " + returnPaymentID+":"+ex.ToString(), PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
                if (ofrmPaymentTransferInfo != null)
                {
                    ofrmPaymentTransferInfo.Dispose();
                    ofrmPaymentTransferInfo = null;
                }
            }
        }
        private void SaveCleargageAdjustment()
        {
            //Int64 _PAccountID = 0;
            Int64 SelectedPaymentTrayID = 0;
            string sSelectedPaymentTray = string.Empty;
            frmPaymentTransferInfo ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
            ClsCleargagePaymentPosting oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            string sAccountID = string.Empty;
            List<string> Acc = new List<string>();
            DateTime dtCloseDate = DateTime.MinValue;
            gloAccountPayment.BulkPaymentOperation bulkWriteOff = null;
            decimal TotalCheckAmount = 0;
            List<string> EncounterID = new List<string>();
            string sEncounterID = string.Empty;
            Int64 returnPaymentID = 0;
            Int64 PatientID = 0;
            string PartialAmountDistributeAcc = string.Empty;
            string TransactionID = string.Empty;
            string OriginalTransactionID = string.Empty;
            string PatientName = string.Empty;
            DateTime TimeStamp = DateTime.MaxValue;
            string AccountType = string.Empty;
            string AccountNo = string.Empty;
            string ReferenceNo = string.Empty;
            string PlanID = string.Empty;
            string PaymentMethod = string.Empty;
            string PatientCode = string.Empty;
            string sAction = string.Empty;
            try
            {
                C1Cleargage.Select();
                #region Check Amount is Distributed or Not

                if (C1Cleargage.Rows.Count > 1)
                {
                    bool IsAmountDistribute = false;
                    for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_ADJUSTMENT_AMT)) != 0)
                        {
                            IsAmountDistribute = true;
                            break;
                        }
                        else
                        {
                            IsAmountDistribute = false;
                        }

                    }
                    if (IsAmountDistribute == false)
                    {
                        MessageBox.Show("No payment has been made to save.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                #endregion
                    #region Validate Adjustment Code present or not
                    for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_ADJUSTMENT_AMT)) != 0 && (Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_CODE)) == "" || Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_CODE)) == null))
                        {
                            string _Msg = " Select adjustment code for charge ('" + Convert.ToString(C1Cleargage.GetData(i, COL_CPT_CODE)) + "') ";
                            MessageBox.Show(_Msg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                    #endregion

                    #region "Accounts"

                    for (int j = 1; j < C1Cleargage.Rows.Count; j++)
                    {
                        if (Convert.ToInt64(C1Cleargage.GetData(j, COL_PACCOUNTID)) != 0 && Convert.ToDecimal(C1Cleargage.GetData(j, COL_TOTALAMOUNT)) == 0 && (Convert.ToDecimal(C1Cleargage.GetData(j, COL_ADJUSTMENT_AMT)) != 0))
                        {
                            
                            if (sAccountID != string.Empty && !Acc.Contains(Convert.ToString(C1Cleargage.GetData(j, COL_DISTINCTACCOUNTNO))))
                            {
                                sAccountID = sAccountID + "," + Convert.ToString(C1Cleargage.GetData(j, COL_PACCOUNTID));
                                Acc.Add(Convert.ToString(C1Cleargage.GetData(j, COL_DISTINCTACCOUNTNO)));
                            }
                            else if (sAccountID == string.Empty)
                            {
                                sAccountID = Convert.ToString(C1Cleargage.GetData(j, COL_PACCOUNTID));
                                Acc.Add(Convert.ToString(C1Cleargage.GetData(j, COL_DISTINCTACCOUNTNO)));
                            }
                        }
                    }

                    #endregion

                   

                    #region "Payment Tray"

                    if (SelectedPaymentTrayID == 0)
                    {
                        ofrmPaymentTransferInfo = new frmPaymentTransferInfo();
                        ofrmPaymentTransferInfo.CleargagePosting = "PostAdjustment";
                        ofrmPaymentTransferInfo.AccountIDs = sAccountID;
                        ofrmPaymentTransferInfo.ShowDialog();
                        if (ofrmPaymentTransferInfo.PaymentTrayID > 0)
                        {
                            sSelectedPaymentTray = ofrmPaymentTransferInfo.PaymentTrayName;
                            SelectedPaymentTrayID = ofrmPaymentTransferInfo.PaymentTrayID;
                        }
                        else
                        {
                            sSelectedPaymentTray = string.Empty;
                            SelectedPaymentTrayID = 0;
                        }
                    }
                    if (ofrmPaymentTransferInfo.PaymentTransferCloseDate == string.Empty || SelectedPaymentTrayID == 0)
                    {
                        return;
                    }

                    #endregion

                    #region DataTable
                    foreach (object o in Acc)
                    {
                        gloAccountPayment.PaymentInfoParameter paymentParameter = null;
                        gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
                        DataTable dtChargeLineDetails = new DataTable();

                        #region "Columns"

                        dtChargeLineDetails.Columns.Add("nTransactionDetailID");
                        dtChargeLineDetails.Columns.Add("nTransactionLineNo");
                        dtChargeLineDetails.Columns.Add("ClaimNumber");
                        dtChargeLineDetails.Columns.Add("nFromDate");
                        dtChargeLineDetails.Columns.Add("nToDate");
                        dtChargeLineDetails.Columns.Add("sCPTCode");
                        dtChargeLineDetails.Columns.Add("ClaimDate");
                        dtChargeLineDetails.Columns.Add("nTransactionID");
                        dtChargeLineDetails.Columns.Add("nTransactionDate");
                        dtChargeLineDetails.Columns.Add("nFacilityType");
                        dtChargeLineDetails.Columns.Add("nClaimNo");
                        dtChargeLineDetails.Columns.Add("nPatientName");
                        dtChargeLineDetails.Columns.Add("nPatientID");
                        dtChargeLineDetails.Columns.Add("Modifier");
                        dtChargeLineDetails.Columns.Add("sCPTDescription");
                        dtChargeLineDetails.Columns.Add("dCharges");
                        dtChargeLineDetails.Columns.Add("dUnit");
                        dtChargeLineDetails.Columns.Add("dTotal");
                        dtChargeLineDetails.Columns.Add("dAllowed");
                        dtChargeLineDetails.Columns.Add("nProviderID");
                        dtChargeLineDetails.Columns.Add("nTransactionLineStatus");
                        dtChargeLineDetails.Columns.Add("nSendToFlag");
                        dtChargeLineDetails.Columns.Add("TotalBalanceAmount");
                        dtChargeLineDetails.Columns.Add("PreviousPaid");
                        dtChargeLineDetails.Columns.Add("PreviousAdjuestment");
                        dtChargeLineDetails.Columns.Add("PrevPatAdj");
                        dtChargeLineDetails.Columns.Add("PatientDue");
                        dtChargeLineDetails.Columns.Add("BadDebtDue");
                        dtChargeLineDetails.Columns.Add("PreviousPatientPaidAmount");
                        dtChargeLineDetails.Columns.Add("SplitClaimNo");
                        dtChargeLineDetails.Columns.Add("TrackTransactionID");
                        dtChargeLineDetails.Columns.Add("TrackTransactionDetailID");
                        dtChargeLineDetails.Columns.Add("TrackSubClaimNo");
                        dtChargeLineDetails.Columns.Add("TrackIsHold");
                        dtChargeLineDetails.Columns.Add("TrackHoldInfo");
                        dtChargeLineDetails.Columns.Add("RespParty");
                        dtChargeLineDetails.Columns.Add("bNonServiceCode");
                        dtChargeLineDetails.Columns.Add("Action");
                        dtChargeLineDetails.Columns.Add("dPayment");
                        dtChargeLineDetails.Columns.Add("AdjustmentCode");
                        dtChargeLineDetails.Columns.Add("AdjustmentDesc");
                        dtChargeLineDetails.Columns.Add("AdjustmentAmount");
                        dtChargeLineDetails.Columns.Add("ReferenceNo");
                        dtChargeLineDetails.Columns.Add("AccountNo");
                        dtChargeLineDetails.Columns.Add("AccountType");
                        dtChargeLineDetails.Columns.Add("PaymentMethod");
                        dtChargeLineDetails.Columns.Add("CGTransactionID");
                        dtChargeLineDetails.Columns.Add("CGOriginalTransactionID");
                        dtChargeLineDetails.Columns.Add("TimeStamp");
                        dtChargeLineDetails.Columns.Add("PaymentPlanID");
                        dtChargeLineDetails.Columns.Add("TxnType");

                        #endregion

                        string _sDistPAccountID = Convert.ToString(o);

                        for (int i = 1; i < C1Cleargage.Rows.Count; i++)
                        {
                            if (Convert.ToString(C1Cleargage.GetData(i, COL_DISTINCTACCOUNTNO)) == _sDistPAccountID)
                            {
                                if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT)) > 0 && Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) != "")
                                {
                                    PartialAmountDistributeAcc =PartialAmountDistributeAcc+','+ Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTNO));
                                    break;
                                }
                                if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) != 0 && Convert.ToString(C1Cleargage.GetData(i, COL_FROMDOS)) != "")
                                {
                                    TotalCheckAmount = Convert.ToDecimal(C1Cleargage.GetData(i, COL_CHECKAMOUNT)) - Convert.ToDecimal(C1Cleargage.GetData(i, COL_TOTALAMOUNT));
                                    PatientName = Convert.ToString(C1Cleargage.GetData(i, COL_PATIENTNAME));
                                    TransactionID = Convert.ToString(C1Cleargage.GetData(i, COL_CG_TRANSACTIONID));
                                    OriginalTransactionID = Convert.ToString(C1Cleargage.GetData(i, COL_CG_ORIGINALTRANSACTIONID));
                                    PlanID = Convert.ToString(C1Cleargage.GetData(i, COL_PAYMENTPLANID));
                                    TimeStamp = Convert.ToDateTime(C1Cleargage.GetData(i, COL_TIMESTAMP));
                                    AccountType = Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTTYPE));
                                    AccountNo = Convert.ToString(C1Cleargage.GetData(i,COL_CG_ACCOUNTNUMBER));
                                    ReferenceNo = Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER));
                                    PaymentMethod = Convert.ToString(C1Cleargage.GetData(i, COL_PAYMENTMETHOD));
                                    PatientCode = Convert.ToString(C1Cleargage.GetData(i, COL_PATIENTCODE));
                                    sEncounterID = Convert.ToString(C1Cleargage.GetData(i, COL_ENCOUNTERID));
                                    sAction = Convert.ToString(C1Cleargage.GetData(i, COL_ACTION));
                                }

                                dtCloseDate = Convert.ToDateTime(ofrmPaymentTransferInfo.PaymentTransferCloseDate);
                               
                                if (Convert.ToDecimal(C1Cleargage.GetData(i, COL_ADJUSTMENT_AMT)) != 0)
                                {
                                    paymentParameter = new gloAccountPayment.PaymentInfoParameter(
                                        Convert.ToInt64(C1Cleargage.GetData(i, COL_PATIENTID)),
                                        Convert.ToInt64(C1Cleargage.GetData(i, COL_PACCOUNTID)),
                                        Convert.ToInt64(C1Cleargage.GetData(i, COL_GUARANTORID)),
                                        Convert.ToInt64(C1Cleargage.GetData(i, COL_ACCOUNT_PATIENT_ID)),
                                        dtCloseDate,
                                        SelectedPaymentTrayID,
                                        sSelectedPaymentTray,
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_CODE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_DESC)),
                                        TotalCheckAmount == 0 ? "0.00" : Convert.ToString(TotalCheckAmount)
                                        );


                                    Decimal dDistributeamt = Convert.ToDecimal(C1Cleargage.GetData(i, COL_DISTRIBUTE_PAYMENT));
                                    PatientID = paymentParameter.PatientID;
                                   // sEncounterID = Convert.ToString(C1Cleargage.GetData(i, COL_ENCOUNTERID));


                                    #region "DataTable"

                                    dtChargeLineDetails.Rows.Add(Convert.ToString(C1Cleargage.GetData(i, COL_BILLING_TRANSACTON_DETAILID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_BILLING_TRANSACTON_LINENO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CLAIM_NUM)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_DOS_FROM)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_DOS_TO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CPT_CODE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CLAIM_DATE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_BILLING_TRANSACTON_ID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CLAIM_CLOSE_DATE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_FACILITY_TYPE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CLAIM_NO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PATIENTNAME)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PATIENTID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_MODIFIER)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CPT_DESCRIPTON)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CHARGE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_UNIT)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TOTAL_CHARGE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ALLOWED)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PROVIDERID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRANSACTION_LINESTATUS)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_SENDTOFLAG)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TOT_BAL_AMT)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PREV_PAID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PREV_ADJ)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PREV_PAT_ADJ)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PAT_DUE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_BadDebt_DUE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PREV_PAT_PAID_AMT)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_SPLIT_CLAIM_NO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_TRN_ID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_TRN_DTL_ID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_SUB_CLAIM_NO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_IS_HOLD)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_HOLD_INFO)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TRACK_RES_PARTY)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_NON_SERVICECODE)),
                                       // Convert.ToString(C1Cleargage.GetData(i, COL_ACTION)),
                                       sAction,
                                        Convert.ToString(C1Cleargage.GetData(i, COL_DISTRIBUTE_PAYMENT)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_CODE)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_DESC)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ADJUSTMENT_AMT)),
                                        //Convert.ToString(C1Cleargage.GetData(i, COL_REFERENCENUMBER)),
                                        ReferenceNo,
                                        Convert.ToString(C1Cleargage.GetData(i, COL_CG_ACCOUNTNUMBER)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_ACCOUNTTYPE)),
                                        //Convert.ToString(C1Cleargage.GetData(i,COL_PAYMENTMETHOD)),
                                        PaymentMethod,
                                        Convert.ToString(C1Cleargage.GetData(i,COL_CG_TRANSACTIONID)),
                                        Convert.ToString(C1Cleargage.GetData(i,COL_CG_ORIGINALTRANSACTIONID)),
                                        Convert.ToDateTime(C1Cleargage.GetData(i,COL_TIMESTAMP)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_PAYMENTPLANID)),
                                        Convert.ToString(C1Cleargage.GetData(i, COL_TxnTYPE)));
                                    #endregion
                                }

                            }
                        }
                        if (paymentParameter != null && dtChargeLineDetails.Rows.Count > 0 && dtChargeLineDetails != null)
                        {
                            #region "Save in TVP"
                            bulkWriteOff = new gloAccountPayment.BulkPaymentOperation();
                            returnPaymentID = bulkWriteOff.AutoCleargagePaymentDistribution(paymentParameter, dtChargeLineDetails, paymentParameter.PatientID, "");//reserve details pass "" as reserve not created for adjustment
                            #endregion
                            if (returnPaymentID != 0)
                            {
                                string _message = "Auto Cleargage Discount adjusted for encounter ID : " + sEncounterID + " and System credit ID : " + returnPaymentID;
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Save, _message, paymentParameter.PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success,SoftwareComponent.gloPM,true);

                                //Update Stauts as Posted Of Perticular Claims
                                bool Result = false;
                                Result = oclsCleargagePaymentPosting.UpdateStatusAsPosted(sEncounterID, _nCleargageFileID, TransactionID, OriginalTransactionID, ReferenceNo, sAction);
                                if (Result == true)
                                {
                                    string _Statusmessage = "Auto Cleargage Discount file status updated for encounter ID : " + sEncounterID + " and System credit ID : " + returnPaymentID;
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Modify, _Statusmessage, paymentParameter.PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                }

                                Int64 returnCGTransactionID = 0;
                                returnCGTransactionID = oclsCleargagePaymentPosting.SavePaymentPostingDetails(returnCGTransactionID, PatientCode, PatientName, PlanID, TransactionID, OriginalTransactionID, TotalCheckAmount, PaymentMethod, sAction, TimeStamp, "", "", AccountType, AccountNo, ReferenceNo, returnPaymentID, sEncounterID, _nCleargageFileID);
                                if (returnCGTransactionID > 0)
                                {
                                    string _Creditmessage = "Auto Cleargage Discount credits details save for encounter ID : " + sEncounterID + " and System credit ID : " + returnPaymentID;
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Save, _Creditmessage, paymentParameter.PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                                }
                            }
                        }
                    }
                    //Update file status as Posted if All Cleargage payment posted
                    bool Result1=oclsCleargagePaymentPosting.UpdateMasterDetailsStatus(_nCleargageFileID);
                    if (Result1 == true)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Modify, "Auto Cleargage Discount file status updated for CleargageFileID : " + _nCleargageFileID, PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                    }
                    #endregion
                    if (PartialAmountDistributeAcc != "")
                    {
                        PartialAmountDistributeAcc = PartialAmountDistributeAcc.Substring(1);
                        MessageBox.Show("Please apply all discount amount for account# : " + PartialAmountDistributeAcc, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Save, "Auto Cleargage Discount not posted for Account# : " + PartialAmountDistributeAcc, PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                    }

                    if (rb_ShowAll.Checked == true)
                    {
                        LoadCleargagePaymentList();
                    }
                    else
                    {
                        CleargagelistRowCount++;
                        LoadCleargagePaymentOneByOne();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargageDiscount, gloAuditTrail.ActivityType.Save, "Exception occured while Cleargage Adjustment for encounter IDs : " + sEncounterID + " and System credit ID : " + returnPaymentID + ":" + ex.ToString(), PatientID, _nCleargageFileID, 0, gloAuditTrail.ActivityOutCome.Failure, SoftwareComponent.gloPM, true);

            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
                if (ofrmPaymentTransferInfo != null)
                {
                    ofrmPaymentTransferInfo.Dispose();
                    ofrmPaymentTransferInfo = null;
                }
            }
        }

        private void C1Cleargage_MouseMove(object sender, MouseEventArgs e)
        {
            SetToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private static void SetToolTip(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
        {
            try
            {
                System.Drawing.Font myfont = oGrid.Font;
                System.Drawing.SizeF stringsize;

                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0;

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    nRow = oGrid.MouseRow;
                    nCol = oGrid.MouseCol;

                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    if (oGrid.Cols[nCol].DataType != typeof(System.Boolean))
                    {
                        if (nRow > 0)
                        {
                            if (oGrid.GetData(nRow, nCol) != null)
                            {
                                if (oGrid.GetData(nRow, COL_TxnTYPE).ToString().ToUpper() == Convert.ToString(CGTransactionType.COPAY) || oGrid.GetData(nRow, COL_TxnTYPE).ToString().ToUpper() == Convert.ToString(CGTransactionType.OTHER) || oGrid.GetData(nRow, COL_TxnTYPE).ToString().ToUpper() == Convert.ToString(CGTransactionType.DECLINE) || oGrid.GetData(nRow, COL_TxnTYPE).ToString().ToUpper() == Convert.ToString(CGTransactionType.LATEFEE) || oGrid.GetData(nRow, COL_TxnTYPE).ToString().ToUpper() == Convert.ToString(CGTransactionType.REJECTFEE))
                               {
                                    string dText = Convert.ToString("Reserve will be created after save click");
                                    //Added Format for Tooltip in 6031
                                    sText = dText.ToString();
                                }
                                else
                                {
                                    sText = oGrid.GetData(nRow, nCol).ToString();
                                }

                                //sText = oGrid.GetData(nRow, nCol).ToString();
                            }

                            colsize = oGrid.Cols[nCol].WidthDisplay;
                        }
                        System.Drawing.Graphics oGrp = oGrid.CreateGraphics();
                        stringsize = oGrp.MeasureString(sText, myfont);
                        //Code Review Changes: Dispose Graphics object
                        oGrp.Dispose();
                        oC1ToolTip.SetToolTip(oGrid, sText);

                        if (sText.Contains("\r\n"))
                        {
                            sText = sText.ToString().Replace("\r\n", " ");
                            oC1ToolTip.SetToolTip(oGrid, sText);

                        }
                        else if (stringsize.Width > colsize)
                        {

                            oC1ToolTip.SetToolTip(oGrid, sText);
                        }
                        else
                        {
                            oC1ToolTip.SetToolTip(oGrid, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }


    }
}
