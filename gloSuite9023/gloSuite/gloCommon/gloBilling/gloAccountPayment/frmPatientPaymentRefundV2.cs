using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloBilling;
using gloPatient;
using gloBilling.Payment;
using gloGlobal;
using gloBilling.Collections;

namespace gloAccountsV2
{
    public partial class frmPatientPaymentRefundV2 : Form
    {

        #region " Private Variables "

       
        private bool _IsFormLoading = false;
        private Int64 _patientId = 0;
        private Int64 _ncloseDate = 0;         
        private bool _isValidResAmount = true;
        private bool _isValidate = true;
        private bool _isFormClosing = false;
       // private string _paymentPrefix = "GPM#";
      //  private gloGeneralItem.gloItems _oSeletedReserveItems = null;

        //Code added by SaiKrishna date 03-02-2011 for Patient Account Feature.
        private Int64 _nPAccountId;
        private Int64 _nSelectedPatientId;
        private Int64 _nGuarantorId;
        private Int64 _nAccountPatientId;


        gloBilling.gloAccountPayment.dsPaymentTVP_V2 dsPayment_TVP = null;
        PaymentModeV2 _EOBPaymentMode = PaymentModeV2.None;

        #endregion " Private Variables "

        #region  " Grid Constants "
        
        const int COL_EOBPAYMENTID = 0;

        const int COL_PATIENTID = 1;
        const int COL_SOURCE =2; //Patient or Insurance Name
        const int COL_ORIGINALPAYMENT = 3;//Check Number,Date,Amount
        const int COL_TORESERVES = 4;//Amount for reserve
        const int COL_TYPE = 5;
        const int COL_NOTE = 6;//Note
        const int COL_AVAILABLE = 7;//Available amount
        const int COL_USERESERVE = 8;//Used Reserve
        const int COL_REFUND = 9;//Current amount to use from avaiable amount
        const int COL_PAYMODE = 10;
        const int COL_REFEOBPAYID = 11;
        const int COL_PACCOUNTID = 12;
        const int COL_RES_EOBPAYID = 13;

        const int COL_COUNT = 14;

        #endregion 

        #region " Property Procedures "

        //Code added by SaiKrishna date 03-02-2011 for Patient Account Feature.
        public Int64 PAccountId
        {
            get { return _nPAccountId; }
            set { _nPAccountId = value; }
        }

        public Int64 SelectedPatientId
        {
            get { return _nSelectedPatientId; }
            set { _nSelectedPatientId = value; }
        }

        public Int64 GuarantorId
        {
            get { return _nGuarantorId; }
            set { _nGuarantorId = value; }
        }

        public Int64 AccountPatientId
        {
            get { return _nAccountPatientId; }
            set { _nAccountPatientId = value; }
        }

        #endregion " Property Procedures "

        #region " Constructors "

        public frmPatientPaymentRefundV2(string DatabaseConnectionString, Int64 Patientid)
        {
            InitializeComponent();
            _patientId = Patientid;
            gloAccount objAccount = new gloAccount( gloPMGlobal.DatabaseConnectionString);
        }


        #endregion " Constructors "

        #region "Form Event"

        private void frmPaymentUseReserve_Load(object sender, EventArgs e)
        {
            SetCloseDate();
            mskrefunddate.Text = mskCloseDate.Text;
            FillPaymentTray();
            FillReserves();
            SetRefundTo();
            FillPaymentMode();
            FillCreditCards();

           
            //Set focus on Refund Entry Grid
            if (c1Reserve.Rows.Count > 1) { c1Reserve.Focus(); c1Reserve.Select(1, COL_REFUND); }
        }

        private void frmPaymentUseReserve_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isFormClosing = true;
            c1Reserve.FinishEditing();
            if (this.DialogResult != DialogResult.OK) { this.DialogResult = DialogResult.Cancel; }
        }

        #endregion

        #region "Form Control Event"

            private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    if (cmbPayMode.SelectedIndex >= 0)
                    {

                        cmbCardType.SelectedIndex = -1;
                        txtCardAuthorizationNo.Text = "";
                        mskCreditExpiryDate.Text = "";
                        mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                       

                        if (cmbPayMode.Text.Trim() == PaymentModeV2.Cash.ToString())
                        {
                            _EOBPaymentMode = PaymentModeV2.Cash;

                            txtCheckNumber.Text = "";
                            lblCheckDate.Text = "Date :";
                            lblCheckNo.Text = "Ref.# :";
                            lblCheckNo.Enabled = true;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.Enabled = true;
                        }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.Check.ToString())
                        {
                            _EOBPaymentMode = PaymentModeV2.Check;
                            lblCheckDate.Text = "Check Date :";
                            lblCheckNo.Text = "Check# :";
                            //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.MaxLength = 15;

                        }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.MoneyOrder.ToString())
                        {
                            _EOBPaymentMode = PaymentModeV2.MoneyOrder;
                            lblCheckDate.Text = "MO Date :";
                            lblCheckNo.Text = "MO# :";
                            //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.MaxLength = 15;
                        }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.CreditCard.ToString())
                        {
                            _EOBPaymentMode = PaymentModeV2.CreditCard;
                            lblCheckDate.Text = "Date :";
                            lblCheckNo.Text = "Card# :";
                            //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                            pnlCredit.Enabled = true;
                            txtCheckNumber.MaxLength = 4;

                        }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.EFT.ToString())
                        {
                            _EOBPaymentMode = PaymentModeV2.EFT;
                            lblCheckDate.Text = "EFT Date :";
                            lblCheckNo.Text = "EFT# :";
                            //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.MaxLength = 15;
                        }
                        else if (cmbPayMode.Text.Trim() == PaymentModeV2.Voucher.ToString())
                        {
                            _EOBPaymentMode = PaymentModeV2.Voucher;
                            lblCheckDate.Text = "Voucher Date :";
                            lblCheckNo.Text = "Voucher# :";
                            //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.MaxLength = 15;
                        }
                    }

                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                }


            }

            private void btnSelectPaymentTray_Click(object sender, EventArgs e)
            {
                try
                {
                    gloBilling.frmBillingTraySelection ofrmBillingTraySelection = new gloBilling.frmBillingTraySelection( gloPMGlobal.DatabaseConnectionString);
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
                    MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        #region "Close date validation"
            private void mskrefunddate_Validating(object sender, CancelEventArgs e)
            {
                try
                {
                    _isValidate = true;
                    MaskedTextBox mskDate = (MaskedTextBox)sender;
                    mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    string strDate = mskDate.Text;
                    mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if (mskDate != null)
                    {
                        if (strDate.Length > 0)
                        {
                            if (IsValidDate(mskDate.Text.Trim()) == false)
                            {
                                MessageBox.Show("Enter valid refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isValidate = false;
                                e.Cancel = true;
                            }

                        }
                        else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        {
                            MessageBox.Show("Enter refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidate = false;
                            e.Cancel = true;
                        }
                    }
                }
                catch (Exception)// ex)
                {
                    MessageBox.Show("Enter valid refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isValidate = false;
                    e.Cancel = true;
                    //ex.ToString();
                    //ex = null;
                }
            }

            private void mskCheckDate_Validating(object sender, CancelEventArgs e)
            {
                string msg = "";
                try
                {
                    _isValidate = true;
                    MaskedTextBox mskDate = (MaskedTextBox)sender;
                    mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    string strDate = mskDate.Text;
                    mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                    if (mskDate != null)
                    {
                        if (strDate.Length > 0)
                        {
                            if (IsValidDate(mskDate.Text.Trim()) == false)
                            {
                                msg = "Enter valid " + lblCheckDate.Text.Replace(":", "").Trim().ToLower() + ".";
                                MessageBox.Show(msg, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isValidate = false;
                                e.Cancel = true;
                            }

                        }
                        else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        {
                            msg = "Enter " + lblCheckDate.Text.Replace(":", "").Trim().ToLower() + ".";
                            MessageBox.Show(msg, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidate = false;
                            e.Cancel = true;
                        }
                    }
                }
                catch (Exception)// ex)
                {
                    msg = "Enter valid " + lblCheckDate.Text.Replace(":", "").Trim().ToLower() + ".";
                    MessageBox.Show(msg, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isValidate = false;
                    e.Cancel = true;
                    //ex.ToString();
                    //ex = null;
                }
            }

            private void masktext_click(object sender, MouseEventArgs e)
            {

                ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (((MaskedTextBox)sender).Text.Trim() == "")
                {
                    ((MaskedTextBox)sender).SelectionStart = 0;
                    ((MaskedTextBox)sender).SelectionLength = 0;
                }
            }

            private void mskCreditExpiryDate_MouseClick(object sender, MouseEventArgs e)
            {
                ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (((MaskedTextBox)sender).Text.Trim() == "")
                {
                    ((MaskedTextBox)sender).SelectionStart = 0;
                    ((MaskedTextBox)sender).SelectionLength = 0;
                }
            }

        private void mskCloseDate_Validating_1(object sender, CancelEventArgs e)
        {
            try
            {
                _isValidate = true;
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Enter valid close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidate = false;
                            e.Cancel = true;
                           
                        }
                        else if (mskCloseDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        {
                            if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                            {
                                MessageBox.Show("Close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isValidate = false;
                                e.Cancel = true;
                            }
                        }
                    }
                    else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                    {
                        MessageBox.Show("Enter close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isValidate = false;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Enter valid close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isValidate = false;
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion "Close date validation"

        #endregion

        #region "Tool Strip button events "

        private void tsb_OK_Click(object sender, EventArgs e)
        {

            c1Reserve.FinishEditing();
            tsb_OK.Select();
            label5.Focus();
            if (_isValidate == true && _isValidResAmount == true)            
            {
                if (SavePaymentValidation())
                {
                    FillEOBData();
                    //if (SavePatientRefund() > 0)
                    //{
                    //    this.Close();
                    //}

                    SavePatientRefundV2();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                   
                   
                }
            }
           
        }
  
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
 
        #endregion

        #region " Design Grid "

        private void DesignPaymentGrid(C1FlexGrid c1Payment)
        {
            try
            {
                _IsFormLoading = true;
                c1Payment.Redraw = false;
                c1Payment.AllowSorting = AllowSortingEnum.None;

                c1Payment.Clear();
                c1Payment.Cols.Count = COL_COUNT;
                c1Payment.Rows.Count = 1;
                c1Payment.Rows.Fixed = 1;
                c1Payment.Cols.Fixed = 0;

                #region " Set Headers "

                c1Payment.SetData(0, COL_EOBPAYMENTID,"EOBPaymentID");
                c1Payment.SetData(0, COL_PATIENTID,"PatientID");
                c1Payment.SetData(0, COL_SOURCE,"Source");//Patient or Insurance Name

                c1Payment.SetData(0, COL_ORIGINALPAYMENT,"Original Payment");//Check Number,Date,Amount
                c1Payment.SetData(0, COL_TORESERVES,"To Reserves");//Amount for reserve
                c1Payment.SetData(0, COL_TYPE,"Type");
                c1Payment.SetData(0, COL_NOTE,"Note");//Note

                c1Payment.SetData(0, COL_AVAILABLE,"Available");//Available amount
                c1Payment.SetData(0, COL_USERESERVE, "Used");//Used Reserve
                c1Payment.SetData(0, COL_REFUND,"Refund");//Current amount to use from avaiable amount
                c1Payment.SetData(0, COL_PACCOUNTID, "PAccountID");
                c1Payment.SetData(0, COL_PAYMODE,"Payment Mode");
                c1Payment.SetData(0, COL_REFEOBPAYID,"Ref.EOBID");
              
                c1Payment.SetData(0, COL_RES_EOBPAYID,"ReserveRefPayID");
            


                #endregion

                #region " Show/Hide "

                c1Payment.Cols[COL_SOURCE].Visible = true;
                c1Payment.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1Payment.Cols[COL_TYPE].Visible = true;
                c1Payment.Cols[COL_TORESERVES].Visible = true;
                c1Payment.Cols[COL_NOTE].Visible = true;
                c1Payment.Cols[COL_AVAILABLE].Visible = true;
                c1Payment.Cols[COL_REFUND].Visible = true;

                c1Payment.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
                c1Payment.Cols[COL_PATIENTID].Visible = false;// 0;
                c1Payment.Cols[COL_PAYMODE].Visible = false;// 100;
                c1Payment.Cols[COL_REFEOBPAYID].Visible = false;// 0;              
                c1Payment.Cols[COL_USERESERVE].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYID].Visible = false;
                c1Payment.Cols[COL_PACCOUNTID].Visible = false;
                

                #endregion

                #region " Width "
                bool _designWidth = false;
       

                if (_designWidth == false)
                {

                    c1Payment.Cols[COL_EOBPAYMENTID].Width = 0;
                   
                    c1Payment.Cols[COL_PATIENTID].Width = 0;
                    c1Payment.Cols[COL_SOURCE].Width = 100;
                    c1Payment.Cols[COL_ORIGINALPAYMENT].Width = 250;
                    c1Payment.Cols[COL_TORESERVES].Width = 80;
                    c1Payment.Cols[COL_TYPE].Width = 100;
                    c1Payment.Cols[COL_NOTE].Width = 230;
                    c1Payment.Cols[COL_AVAILABLE].Width = 75;
                    c1Payment.Cols[COL_REFUND].Width = 75;
                    c1Payment.Cols[COL_PAYMODE].Width = 100;
                    c1Payment.Cols[COL_REFEOBPAYID].Width = 0; 
                    c1Payment.Cols[COL_USERESERVE].Width = 0;
                    c1Payment.Cols[COL_RES_EOBPAYID].Width = 0;
                    c1Payment.Cols[COL_PACCOUNTID].Width = 0;
                   
                }

                #endregion

                #region " Data Type "

                c1Payment.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_SOURCE].DataType = typeof(System.String);
                c1Payment.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
                c1Payment.Cols[COL_TYPE].DataType = typeof(System.String);
                c1Payment.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_NOTE].DataType = typeof(System.String);
                c1Payment.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_REFUND].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_REFEOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PACCOUNTID].DataType = typeof(System.Int64);

                #endregion

                #region " Alignment "

                c1Payment.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_REFUND].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAYMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_REFEOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_PACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

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
                        // csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        //csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    // csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    //csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

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
                       //csEditableCurrencyStyle.BackColor = Color.White;
                   }

               }
               catch
               {
                   csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                   csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                   csEditableCurrencyStyle.Format = "c";
                   csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                   //csEditableCurrencyStyle.BackColor = Color.White;

               }
         

                //C1.Win.C1FlexGrid.CellStyle csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                //csEditableStringStyle.DataType = typeof(System.String);
                //csEditableStringStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //csEditableStringStyle.BackColor = Color.White;

                //C1.Win.C1FlexGrid.CellStyle csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                //csEditableDateStyle.DataType = typeof(System.DateTime);
                //csEditableDateStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //csEditableDateStyle.BackColor = Color.White;

                //C1.Win.C1FlexGrid.CellStyle csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                //csClaimRowStyle.DataType = typeof(System.String);
                //csClaimRowStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);

                //C1.Win.C1FlexGrid.CellStyle csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                //csPatientRowStyle.DataType = typeof(System.String);
                //csPatientRowStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);

                c1Payment.Cols[COL_TORESERVES].Style = csCurrencyStyle;
                c1Payment.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_REFUND].Style = csCurrencyStyle;
                c1Payment.Cols[COL_USERESERVE].Style = csCurrencyStyle;
                
                #endregion

                c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Payment.AllowEditing = true;

                c1Payment.Cols[COL_EOBPAYMENTID].AllowEditing = false;
                c1Payment.Cols[COL_PATIENTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_SOURCE].AllowEditing = false;//100;
                c1Payment.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
                c1Payment.Cols[COL_TYPE].AllowEditing = false;//100;
                c1Payment.Cols[COL_TORESERVES].AllowEditing = false;//100;
                c1Payment.Cols[COL_NOTE].AllowEditing = false;//100;
                c1Payment.Cols[COL_AVAILABLE].AllowEditing = false;//100;
                c1Payment.Cols[COL_REFUND].AllowEditing = true;//100;
                c1Payment.Cols[COL_USERESERVE].AllowEditing = false;//100;
                c1Payment.Cols[COL_PAYMODE].AllowEditing = false;//100;
                c1Payment.Cols[COL_REFEOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
                #endregion

                //c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                //c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                //c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                //c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.ShowCellLabels =false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
            finally
            { _IsFormLoading = false; c1Payment.Redraw = true; }
        }

        #endregion " Design Grid "

        #region " Grid Events "
     
        private void c1Reserve_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (c1Reserve.ColSel == COL_SOURCE)
                {
                    c1Reserve.Select(c1Reserve.RowSel, COL_REFUND);
                }
            }
        }

        private void c1Reserve_CellChanged(object sender, RowColEventArgs e)
        {
            try
            {               
                if (_IsFormLoading == false && _isFormClosing == false)
                {
                    _isValidResAmount = true;
                    if (e.Row > 0 && e.Col == COL_REFUND)
                    {
                        if (c1Reserve.GetData(e.Row, e.Col) != null && Convert.ToString(c1Reserve.GetData(e.Row, e.Col)).Trim() != ""
                         && c1Reserve.GetData(e.Row, COL_AVAILABLE) != null && Convert.ToString(c1Reserve.GetData(e.Row, COL_AVAILABLE)).Trim() != "")
                        {
                            if (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_REFUND)) < 0)
                            {
                                try
                                {
                                    this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
                                    MessageBox.Show("Refund amount cannot be negative. Enter valid refund amount.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                                 
                                    _isValidResAmount = false;
                                    c1Reserve.SetData(e.Row, e.Col, "0.00");
                                    CalculateAmount();                                
                                    c1Reserve.Focus();
                                    c1Reserve.Select(e.Row, COL_REFUND);
                                    e.Cancel=true;
                                 }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    ex = null; 
                                }
                                finally
                                { this.c1Reserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged); 
                                    
                                }
                            }
                            else if (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_REFUND)) > Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_AVAILABLE)))
                            {
                                try
                                {
                                    this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
                                    MessageBox.Show("Refund amount cannot be more than available amount.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                                    _isValidResAmount = false;
                                    c1Reserve.SetData(e.Row, e.Col, "0.00");
                                    CalculateAmount();
                                    c1Reserve.Focus();
                                    c1Reserve.Select(e.Row, COL_REFUND);
                                    e.Cancel = true;
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    ex = null;
                                }
                                finally
                                { 
                                    this.c1Reserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged); 
                                   
                                }

                            }
                            else
                            {
                                CalculateAmount();
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
        }
        
        private void c1Reserve_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1Reserve_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            #region "Numeric Validation"
            if (c1Reserve.ColSel == COL_REFUND)
            {
                decimal _result = Convert.ToDecimal(c1Reserve.GetData(c1Reserve.RowSel, c1Reserve.ColSel));
                if (e.KeyChar == Convert.ToChar("-"))
                {
                    e.Handled = true;
                }

            }
            #endregion
        }

        #endregion

        #region " Private & Public Methods "

        private void FillPaymentMode()
        {
            cmbPayMode.Items.Clear();
            cmbPayMode.Items.Add(PaymentMode.Cash.ToString());
            cmbPayMode.Items.Add(PaymentMode.Check.ToString());
            cmbPayMode.Items.Add(PaymentMode.CreditCard.ToString());
            cmbPayMode.Items.Add(PaymentMode.MoneyOrder.ToString());
            cmbPayMode.Items.Add(PaymentMode.EFT.ToString());
            cmbPayMode.Items.Add(PaymentMode.Voucher.ToString());

            for (int i = 0; i <= cmbPayMode.Items.Count - 1; i++)
            {
                if (cmbPayMode.Items[i].ToString() == PaymentMode.Check.ToString())
                {
                    cmbPayMode.SelectedIndex = i;
                    break;
                }
            }

        }

        //For Payment Tray
        private bool IsAdmin(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
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
            }
            oDataTable.Dispose();
            oDB.Dispose();
            return result;


        }

        private void FillReserves()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtReserves = new DataTable();

            try
            {
                
                DesignPaymentGrid(c1Reserve);

                _IsFormLoading = true;

                //Code added by SaiKrishna for Patient Account Feature. 
                oParameters.Add("@nPAccountID", _nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", _nSelectedPatientId, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                
                oDB.Connect(false);
               // oDB.Retrive("PA_BL_SELECT_PaymentTransaction_UseReserve_V2", oParameters, out _dtReserves);
                oDB.Retrive("PA_Patient_Financial_View_Reserve_V2", oParameters, out _dtReserves);
                oDB.Disconnect();

                if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                {
                    int _rowIndex = 0;

                    for (int i = 0; i < _dtReserves.Rows.Count; i++)
                    {

                        #region " Set Data "

                        _rowIndex = c1Reserve.Rows.Add().Index;

                        c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_PATIENTID,Convert.ToString(_dtReserves.Rows[i]["nPatientID"]));
                        c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(_dtReserves.Rows[i]["PatientName"]));//Patient or Insurance Name
                        //string _originalPayment = "";
                        //_originalPayment = ((PaymentModeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentMode"])).ToString() + "# " + Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " +String.Format("{0:MM/dd/yyyy}", _dtReserves.Rows[i]["nCheckDate"]) + " $ " + Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
                        c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, Convert.ToString(_dtReserves.Rows[i]["OriginalAmount"]));//Check Number,Date,Amount
                        c1Reserve.SetData(_rowIndex, COL_TYPE, Convert.ToString(_dtReserves.Rows[i]["nPaymentNoteSubType"]));//Copay,Advance,Other
                        c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));                       
                        c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note
                        c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(_dtReserves.Rows[i]["UsedReserve"]));//Used amount
                        c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
                        c1Reserve.SetData(_rowIndex, COL_REFUND,0);//Current amount to use from avaiable amount
                        c1Reserve.SetData(_rowIndex, COL_PAYMODE, ((PaymentModeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentMode"])));
                        c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID,Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_PACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nPAccountID"]));
                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentID"]));
                       
                        #region " Set Styles "

                        c1Reserve.SetCellStyle(_rowIndex, COL_REFUND, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                        #endregion " Set Styles "


                        #endregion
                    }
                }


            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtReserves != null) { _dtReserves.Dispose(); }
                _IsFormLoading = false;
            }
        }

        private void FillCreditCards()
        {
            CreditCards oCreditCards = new CreditCards( gloPMGlobal.DatabaseConnectionString);
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
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oCreditCards != null) { oCreditCards.Dispose(); }
                //if (_dtCards != null) { _dtCards.Dispose(); }
            }
        }

        private bool SavePaymentValidation()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling( gloPMGlobal.DatabaseConnectionString, "");
            int _addDays = 0;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();

            MaskedTextBox mskDate = mskCloseDate;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskDate.Text;
            mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            try
            {
                if (strDate.Trim() == "")
                {
                    MessageBox.Show("Enter close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    return false;
                }

                if (IsValidDate(mskDate.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter valid close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    return false;

                }              
                else if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                {
                    MessageBox.Show("Close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();                   
                    return false;
                }


                else if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();                   
                    return false;
                }



                else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                {
                    MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskCloseDate.Focus();
                    mskCloseDate.Select();
                    return false;
                }
     

                if (lblPaymentTray.Tag == null || lblPaymentTray.Tag.ToString().Trim() == "" || Convert.ToInt64(lblPaymentTray.Tag) <= 0)
                {
                    MessageBox.Show("Select payment tray.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSelectPaymentTray.Select();
                    btnSelectPaymentTray.Focus();
                    return false;
                }

                if (txtTo.Text.Trim() == "")
                {
                    MessageBox.Show("Enter refund to name.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTo.Focus();
                    txtTo.Select();
                    return false;
                }


                mskDate = mskrefunddate;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (strDate.Trim() == "")
                {
                    MessageBox.Show("Enter refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskrefunddate.Select();
                    mskrefunddate.Focus();
                    return false;
                }
                if (IsValidDate(mskDate.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter valid refund date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskrefunddate.Select();
                    mskrefunddate.Focus();
                    return false;
                }      

                if (txtRefundAmount.Text.Trim().Length == 0 || Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim()) <= 0)
                {
                    MessageBox.Show("Enter refund amount.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (c1Reserve.Rows.Count > 1)
                    {
                        c1Reserve.Focus();
                        c1Reserve.Select(1, COL_REFUND);
                    }
                    return false;
                }

                PaymentModeV2 _EOBPaymentMode = PaymentModeV2.None;
                if (cmbPayMode.Text.Trim() == PaymentModeV2.Cash.ToString())
                { _EOBPaymentMode = PaymentModeV2.Cash; }
                if (cmbPayMode.Text.Trim() == PaymentModeV2.Check.ToString())
                { _EOBPaymentMode = PaymentModeV2.Check; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.MoneyOrder.ToString())
                { _EOBPaymentMode = PaymentModeV2.MoneyOrder; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.CreditCard.ToString())
                { _EOBPaymentMode = PaymentModeV2.CreditCard; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.EFT.ToString())
                { _EOBPaymentMode = PaymentModeV2.EFT; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.Voucher.ToString())
                { _EOBPaymentMode = PaymentModeV2.Voucher; }

                if (_EOBPaymentMode == PaymentModeV2.None)
                {
                    MessageBox.Show("Select refund mode.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPayMode.Select();
                    cmbPayMode.Focus();
                    return false;
                }
                else if (_EOBPaymentMode == PaymentModeV2.CreditCard)
                {

                    mskDate = mskCheckDate;
                    mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    strDate = mskDate.Text;
                    mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                    if (strDate.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                    if (IsValidDate(mskDate.Text.Trim()) == false)
                    {
                        MessageBox.Show("Enter valid " + _EOBPaymentMode.ToString().ToLower() + " date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                    if (cmbCardType == null || cmbCardType.Items.Count <= 0 || cmbCardType.Text.Trim() == "")
                    {
                        MessageBox.Show("Select card type.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbCardType.Select();
                        cmbCardType.Focus();
                        return false;
                    }

                    mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskCreditExpiryDate.Text != "")
                    {
                        if (mskCreditExpiryDate.MaskFull == false)
                        {
                            MessageBox.Show("Enter valid " + _EOBPaymentMode.ToString().ToLower() + " expiration date (MM/yy).", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCreditExpiryDate.Select();
                            mskCreditExpiryDate.Focus();
                            return false;
                        }
                    }

                }
                else if (_EOBPaymentMode == PaymentModeV2.Check)
                {

                    if (txtCheckNumber.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " number.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckNumber.Select();
                        txtCheckNumber.Focus();
                        return false;
                    }

                    mskDate = mskCheckDate;
                    mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    strDate = mskDate.Text;
                    mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                    if (strDate.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                    if (IsValidDate(mskDate.Text.Trim()) == false)
                    {
                        MessageBox.Show("Enter valid " + _EOBPaymentMode.ToString().ToLower() + " date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                }
                else if (_EOBPaymentMode == PaymentModeV2.EFT)
                {
                    if (txtCheckNumber.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToUpper() + " number.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckNumber.Select();
                        txtCheckNumber.Focus();
                        return false;
                    }
                }

                else if (_EOBPaymentMode == PaymentModeV2.Voucher)
                {
                    if (txtCheckNumber.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToUpper() + " number.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckNumber.Select();
                        txtCheckNumber.Focus();
                        return false;
                    }

                    mskDate = mskCheckDate;
                    mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    strDate = mskDate.Text;
                    mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                    if (strDate.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                    if (IsValidDate(mskDate.Text.Trim()) == false)
                    {
                        MessageBox.Show("Enter valid " + _EOBPaymentMode.ToString().ToLower() + " date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }
                }
               
                if (txtNotes.Text.Trim() == "")
                {
                    MessageBox.Show("Enter refund note.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNotes.Select();
                    txtNotes.Focus();
                    return false;
                }


                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                {
                    DialogResult _dlgCloseDate = DialogResult.None;
                    _dlgCloseDate = MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (_dlgCloseDate == DialogResult.Cancel)
                    {
                        mskCloseDate.Focus();
                        mskCloseDate.Select();
                        return false;
                    }
                    else
                    {
                       
                    }
                }

             
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }

            return true;
        }
       
        //Calculate Total Refund Amount
        private void CalculateAmount()
        {
            try
            {
                decimal _refundtotal = Convert.ToDecimal("0.00");
                for (int i = 1; c1Reserve.Rows.Count > i; i++)
                {
                    if (Convert.ToDecimal(c1Reserve.GetData(i, COL_REFUND)) > 0)
                    {
                        _refundtotal += Convert.ToDecimal(c1Reserve.GetData(i, COL_REFUND));
                    }
                }
                txtRefundAmount.Text = "$" + _refundtotal.ToString();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }

        private void SetRefundTo()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            object _ResultGuarantor = null;
            try
            {
                string _strQuery = "";

                //_strQuery = "SELECT ISNULL(Patient_OtherContacts.sFirstName,'') + SPACE(1) + ISNULL(Patient_OtherContacts.sMiddleName,'') + (CASE WHEN ISNULL(Patient.sMiddleName,'')<> '' Then SPACE(1) Else '' End)+ ISNULL(Patient_OtherContacts.sLastName,'') AS Guarantor "
                //          + " FROM Patient WITH (NOLOCK) LEFT JOIN Patient_OtherContacts WITH (NOLOCK) ON Patient.nPatientID = Patient_OtherContacts.nPatientID "
                //          + " WHERE Patient.nPatientID = " + _patientId + " AND Patient.nClinicID =" + gloPMGlobal.ClinicID + " AND (Patient_OtherContacts.nPatientContactTypeFlag = 1 OR Patient_OtherContacts.nPatientContactTypeFlag  IS NULL )  ";               


                _strQuery = "Select ISNULL(sFirstName,'')+SPACE(1)+ISNULL(sMiddleName,'')+(Case When ISNULL(sMiddleName,'')<> '' Then SPACE(1) Else '' End)+ISNULL(sLastName,'') As Guarantor "
                             + " From PA_Accounts WITH (NOLOCK) Where nPAccountID =" + _nPAccountId;
                oDB.Connect(false);
                _ResultGuarantor = oDB.ExecuteScalar_Query(_strQuery);

                if (Convert.ToString(_ResultGuarantor).Trim() == "")
                {
                    _strQuery = "SELECT dbo.GET_NAME(sFirstName,sMiddleName,sLastName) FROM Patient WITH (NOLOCK) WHERE nPatientID ='" + _patientId + "'";
                    _ResultGuarantor = oDB.ExecuteScalar_Query(_strQuery);
                }

                txtTo.Text = Convert.ToString(_ResultGuarantor);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_ResultGuarantor != null) { _ResultGuarantor = null; }

            }


        }

        private void FillEOBData()
        {
            gloGeneralItem.gloItems _oSeletedReserveItems = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem ogloItem = null;
            decimal _selectedAmount = 0;
            Int64 _selEOBPayId = 0;
            Int64 _selEOBPayDtlId = 0;
            Int64 _selRefEOBPayId = 0;
            Int64 _selRefEOBPayDtlId = 0;
            Int32 _selEOBPayPayMode = 0;
            //Code added by SaiKrishna date 04-02-2011 for Patient Account Feature.
            Int64 _selPatientID = 0;
            try
            {
                if (c1Reserve != null && c1Reserve.Rows.Count > 1)
                {
                    c1Reserve.FinishEditing();

                    for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
                    {
                        if (c1Reserve.GetData(i, COL_REFUND) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFUND)).ToString().Trim() != "")
                        {
                            if (Convert.ToDecimal(c1Reserve.GetData(i, COL_REFUND)) > 0)
                            {
                                _selectedAmount += Convert.ToDecimal(c1Reserve.GetData(i, COL_REFUND));

                                if (c1Reserve.GetData(i, COL_EOBPAYMENTID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTID)).ToString().Trim() != "")
                                { _selEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTID)); }
                                //if (c1Reserve.GetData(i, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
                                //{ _selEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)); }

                                if (c1Reserve.GetData(i, COL_REFEOBPAYID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYID)).ToString().Trim() != "")
                                { _selRefEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYID)); }

                                //if (c1Reserve.GetData(i, COL_REFEOBPAYDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)).ToString().Trim() != "")
                                //{ _selRefEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)); }

                                if (c1Reserve.GetData(i, COL_PAYMODE) != null && Convert.ToString(c1Reserve.GetData(i, COL_PAYMODE)).ToString().Trim() != "")
                                {
                                    _selEOBPayPayMode = ((PaymentModeV2)Convert.ToInt32(c1Reserve.GetData(i, COL_PAYMODE))).GetHashCode();
                                }

                                //Code added by SaiKrishna date 04-2-2011 for Patient Account Feature.
                                if (c1Reserve.GetData(i, COL_PATIENTID) != null && Convert.ToString(c1Reserve.GetData(i, COL_PATIENTID)).Trim() != "")
                                {
                                    _selPatientID = Convert.ToInt64(c1Reserve.GetData(i, COL_PATIENTID));
                                }
                                //Code added by SaiKrishna date 04-02-2011 for Patient Account Feature.
                                ogloItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(c1Reserve.GetData(i, COL_REFUND)).Trim(), Convert.ToString(c1Reserve.GetData(i, COL_PATIENTID)));
                                ogloItem.SubItems.Add(_selRefEOBPayId, _selEOBPayPayMode.ToString(), _selRefEOBPayDtlId.ToString());
                                _oSeletedReserveItems.Add(ogloItem);
                                // ogloItem.Dispose(); //SLR: subitems will be cleared.

                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayPayMode = 0;
                                //Code added by SaiKrishna date 04-02-2011 for Patient Account Feature.
                                _selPatientID = 0;
                            }
                        }
                    }


                }
                //if (_selectedAmount > 0)
                //{
                //    oSeletedReserveItems = _oSeletedReserveItems;
                //}
                else
                {
                    MessageBox.Show("Enter refund amount.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (c1Reserve != null && c1Reserve.Rows.Count > 1) { c1Reserve.Focus(); c1Reserve.Select(1, COL_REFUND, true); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_oSeletedReserveItems != null)
                {
                    _oSeletedReserveItems.Clear();
                    _oSeletedReserveItems.Dispose();
                    _oSeletedReserveItems = null;
                }
            }
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
                Success = false; // If this line is reached, an exception was thrown
                e.ToString();
                e = null;
            }
            return Success;
        }

        private gloAccountsV2.PaymentCollection.Credit GetCreditDetails()
        {
            gloAccountsV2.PaymentCollection.Credit oCredit = new PaymentCollection.Credit();
            oCredit.ReceiptNo = txtCheckNumber.Text.Trim();
            oCredit.PaymentID = 0;
            oCredit.PayerID = _nSelectedPatientId;
            oCredit.PayerName = "";
            oCredit.PayerType = PayerTypeV2.Self;
            oCredit.Entrytype = PaymentEntryTypeV2.PatientRefund;
            oCredit.PaymentMode = PaymentModeV2.Check;
            oCredit.ReceiptNo = txtCheckNumber.Text.Trim(); ;
            if (txtRefundAmount.Text.Remove(0, 1).Trim() != "")
            {
                oCredit.ReceiptAmount = Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim());
            }

            if (mskCheckDate.MaskCompleted)
            {
                mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                oCredit.ReceiptDate = Convert.ToDateTime(mskCheckDate.Text);
            }


            oCredit.CreditCardType = cmbCardType.Text.Trim();
            oCredit.AuthorizationNo = txtCardAuthorizationNo.Text.Trim();
            if (mskCreditExpiryDate.MaskCompleted)
            {
                mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            }

            oCredit.AccountID = _nAccountPatientId;
            oCredit.CreatedDateTime = DateTime.Now;
            oCredit.ModifiedDateTime = DateTime.Now;

            oCredit.PaymentTrayID = Convert.ToInt64(lblPaymentTray.Tag);
            oCredit.PaymentTrayDesc = lblPaymentTray.Text.Trim().Replace("'", "''");

            if (mskCloseDate.MaskCompleted == true)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                oCredit.CloseDate = Convert.ToDateTime(mskCloseDate.Text);
            }

            oCredit.UserID = gloPMGlobal.UserID;
            oCredit.UserName = gloPMGlobal.UserName;

            return oCredit;
        }

        private gloAccountsV2.PaymentCollection.CreditsEXT GetCreditExt()
        {
            gloAccountsV2.PaymentCollection.CreditsEXT oPaymentInsuranceEXT = new gloAccountsV2.PaymentCollection.CreditsEXT();
            oPaymentInsuranceEXT.Credits_ID = 0;
            oPaymentInsuranceEXT.Credits_EXTID = 0;

            oPaymentInsuranceEXT.CreditCardType = cmbCardType.Text.Trim();
            oPaymentInsuranceEXT.AuthorizationNo = txtCardAuthorizationNo.Text.Trim();
            if (mskCreditExpiryDate.MaskCompleted)
            {
                mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            }

            oPaymentInsuranceEXT.ClinicID = gloPMGlobal.ClinicID;
            oPaymentInsuranceEXT.PaymentVoidDateTime = DateTime.Now;
            oPaymentInsuranceEXT.CreatedDateTime = DateTime.Now;
            oPaymentInsuranceEXT.ModifiedDateTime = DateTime.Now;

            oPaymentInsuranceEXT.Version = Environment.Version.ToString();
            oPaymentInsuranceEXT.MachineName = Environment.MachineName;
            oPaymentInsuranceEXT.SiteID = "";
            oPaymentInsuranceEXT.IsFinished = false;
            return oPaymentInsuranceEXT;
        }

        private Int64 SavePatientRefundV2()
        {
            Int64 _retPayId = 0;
            DataTable _dtUniqueIDs = new DataTable();
            DataTable _dtUniqueCreditID = new DataTable();
            Int64 _nCreditID = 0;

            try
            {
                Int64 _CloseDayTrayID = 0;
                string _CloseDayTrayName = "";


                #region "Payment Tray"

                _CloseDayTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());
                //_CloseDayTrayCode = "";
                _CloseDayTrayName = lblPaymentTray.Text;

                #endregion

                #region " Master Data "
                dsPayment_TVP = new gloBilling.gloAccountPayment.dsPaymentTVP_V2();
                _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                { _nCreditID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString()); }
                SetCreditsDetails(dsPayment_TVP, _nCreditID);
                #endregion

                #region "Refund Data "
                dsPayment_TVP.Tables["Refunds"].Rows.Add();
                dsPayment_TVP.Tables["Refunds"].Rows[0]["nRefundID"] = 0;
                dsPayment_TVP.Tables["Refunds"].Rows[0]["sRefundTo"] = txtTo.Text;
                dsPayment_TVP.Tables["Refunds"].Rows[0]["sRefundNotes"] = txtNotes.Text;
                if (mskrefunddate.MaskCompleted == true)
                {
                    mskrefunddate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    dsPayment_TVP.Tables["Refunds"].Rows[0]["dtRefundDate"] = Convert.ToDateTime(mskrefunddate.Text);
                }
                dsPayment_TVP.Tables["Refunds"].Rows[0]["nRefundAmount"] = Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim());
                dsPayment_TVP.Tables["Refunds"].Rows[0]["nCreditID"] = Convert.ToInt64(dsPayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                if (_nSelectedPatientId == 0 && _nPAccountId > 0)
                {
                    dsPayment_TVP.Tables["Refunds"].Rows[0]["nPayerID"] = _patientId;
                }
                else
                {
                    dsPayment_TVP.Tables["Refunds"].Rows[0]["nPayerID"] = _nSelectedPatientId;
                }
                dsPayment_TVP.Tables["Refunds"].Rows[0]["sVoidNote"] = 0;
                dsPayment_TVP.Tables["Refunds"].Rows[0]["nTransactionID"] = 0;
                dsPayment_TVP.Tables["Refunds"].Rows[0]["nTransactionMasterID"] = 0;
                dsPayment_TVP.Tables["Refunds"].Rows[0]["sClaimNo"] = 0;
                dsPayment_TVP.Tables["Refunds"].Rows[0]["nPAccountID"] = _nPAccountId;
                dsPayment_TVP.Tables["Refunds"].Rows[0]["nPatientID"] = _patientId;
                dsPayment_TVP.Tables["Refunds"].Rows[0]["nAccountPatientID"] = _nAccountPatientId;
                dsPayment_TVP.Tables["Refunds"].AcceptChanges();

                #endregion

                #region "Reserve Debit Entry "
                if (c1Reserve != null && c1Reserve.Rows.Count > 1)
                {
                    c1Reserve.FinishEditing();
                    int row_num = 0;
                    for (int i = 0; i < c1Reserve.Rows.Count - 1; i++)
                    {
                        if (c1Reserve.GetData(i + 1, COL_REFUND) != null && Convert.ToString(c1Reserve.GetData(i + 1, COL_REFUND)).ToString().Trim() != "")
                        {

                            if (Convert.ToDecimal(c1Reserve.GetData(i + 1, COL_REFUND)) > 0)
                            {
                                //#region "Reserve"
                                //dsPayment_TVP.Tables["Reserves"].Rows.Add();
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nReserveID"] = 0;
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nCreditID"] = Convert.ToInt64(c1Reserve.GetData(i + 1, COL_EOBPAYMENTID));
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nCredits_RefID"] = Convert.ToInt64(dsPayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["dReserveAmount"] = Convert.ToInt64(c1Reserve.GetData(i + 1, COL_REFUND)) * (-1);
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nReserveType"] = ReserveEntryTypeV2.PatientRefund.GetHashCode();
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nInsCompanyID"] = 0;
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nPatientID"] = _patientId;
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nUserID"] = gloPMGlobal.UserID;
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["sUserName"] = gloPMGlobal.UserName;
                                //if (mskCloseDate.MaskCompleted == true)
                                //{
                                //    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                //    dsPayment_TVP.Tables["Reserves"].Rows[row_num]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                //}
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["bIsPaymentVoid"] = false;
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nVoidType"] = 0;
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                //if (_IsPatientAccountFeature)
                                //{
                                //    dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nPAccountID"] = _nPAccountId;
                                //    dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nGuarantorID"] = 0;
                                //    dsPayment_TVP.Tables["Reserves"].Rows[row_num]["nAccountPatientID"] = _nAccountPatientId;
                                //}
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["dtPaymentVoidDateTime"] = DBNull.Value;
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["dtCreatedDateTime"] = DateTime.Now;
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["dtModifiedDateTime"] = DateTime.Now;
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["sReserveNote"] = "Reserved";
                                //dsPayment_TVP.Tables["Reserves"].Rows[row_num]["sMachineName"] = Environment.MachineName;
                                //dsPayment_TVP.Tables["Reserves"].AcceptChanges();
                                //#endregion

                                #region "Credit Details"

                                dsPayment_TVP.Tables["CreditsDTL"].Rows.Add();
                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["nCreditsDTL_ID"] = Convert.ToInt64(dsPayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["nCreditID"] = Convert.ToInt64(dsPayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["nCreditsRef_ID"] = Convert.ToInt64(c1Reserve.GetData(i + 1, COL_EOBPAYMENTID));
                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["nReserveRef_ID"] = Convert.ToInt64(c1Reserve.GetData(i + 1, COL_RES_EOBPAYID));
                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["dAmount"] = Convert.ToDecimal(c1Reserve.GetData(i + 1, COL_REFUND));
                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["nEntryType"] = PaymentEntryTypeV2.PatientRefund.GetHashCode();
                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["sEntryDesc"] = "PR";
                                if (mskCloseDate.MaskCompleted == true)
                                {
                                    dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                }
                                else
                                {
                                    dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["dtCloseDate"] = DateTime.Now.Date;
                                }

                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["dtCreatedDateTime"] = DateTime.Now;
                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["dtModifiedDateTime"] = DateTime.Now;
                                dsPayment_TVP.Tables["CreditsDTL"].Rows[row_num]["bIsPaymentVoid"] = false;
                                dsPayment_TVP.Tables["CreditsDTL"].AcceptChanges();
                                row_num = row_num + 1;
                                #endregion


                            }
                        }

                    }
                }
                #endregion

                gloPatientPaymentV2 objPatPaymentV2 = new gloPatientPaymentV2();
                Int64 retPayId = 0;
                if (dsPayment_TVP != null)
                {
                    retPayId = objPatPaymentV2.SavePatientRefund(dsPayment_TVP);
                    if (retPayId > 0)
                    { CL_FollowUpCode.SetAutoAccountFollowUp(_nPAccountId, _patientId, Convert.ToDateTime(mskCloseDate.Text.Trim())); }
                }

                //GLO2011-0013738 : Patient Payment screen does not prompt to pick a tray
                // whenver payment tray load it will show last payment tray selected which is used for transaction.

                if (_retPayId > 0)
                {
                    gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal .gloPMGlobal.DatabaseConnectionString , "");
                    ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, gloGlobal.gloPMGlobal.ClinicID);
                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);

                    oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(mskCloseDate.Text).ToString("MM/dd/yyyy"), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", _CloseDayTrayID.ToString(), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.Dispose();
                    ogloBilling.Dispose();
                }
                //end

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
            return _retPayId;
        }

        private void SetCreditsDetails(gloBilling.gloAccountPayment.dsPaymentTVP_V2 dsPayment_TVP, Int64 _nCreditID)
        {
            dsPayment_TVP.Tables["Credits"].Rows.Add();
            if (_nCreditID != 0)
            {
                dsPayment_TVP.Tables["Credits"].Rows[0]["nCreditID"] = _nCreditID;
            }
            else
            {
                dsPayment_TVP.Tables["Credits"].Rows[0]["nCreditID"] = 0;
            }
            dsPayment_TVP.Tables["Credits"].Rows[0]["sReceiptNo"] = txtCheckNumber.Text.Trim();
            if (txtRefundAmount.Text.Remove(0, 1).Trim() != "")
            {
                dsPayment_TVP.Tables["Credits"].Rows[0]["dReceiptAmount"] = Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim());
            }
            if (mskCheckDate.MaskCompleted)
            {
                mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                dsPayment_TVP.Tables["Credits"].Rows[0]["dtReceiptDate"] = Convert.ToDateTime(mskCheckDate.Text);
            }
            dsPayment_TVP.Tables["Credits"].Rows[0]["nPayerType"] = PayerTypeV2.Self.GetHashCode();
            if (_nSelectedPatientId == 0 && _nPAccountId > 0)
            {
                dsPayment_TVP.Tables["Credits"].Rows[0]["nPayerID"] = _patientId;
            }
            else
            {
                dsPayment_TVP.Tables["Credits"].Rows[0]["nPayerID"] = _nSelectedPatientId;
            }
            dsPayment_TVP.Tables["Credits"].Rows[0]["sPayerName"] = "";


            string sPaymentNo = "";
            DataTable _dtUniquePaymentPrfixNumber = null;
            _dtUniquePaymentPrfixNumber = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
            if (_dtUniquePaymentPrfixNumber != null && _dtUniquePaymentPrfixNumber.Rows.Count > 0)
            {
                sPaymentNo = Convert.ToString(_dtUniquePaymentPrfixNumber.Rows[0]["ID"].ToString());
            }
            if (_dtUniquePaymentPrfixNumber != null) { _dtUniquePaymentPrfixNumber.Dispose(); }

            dsPayment_TVP.Tables["Credits"].Rows[0]["sPaymentNo"] = sPaymentNo;
            //None = 0,
            //Cash = 1,
            //Check = 2,
            //MoneyOrder = 3,
            //CreditCard = 4,
            //EFT = 5
            if (_EOBPaymentMode == PaymentModeV2.Check)
            { dsPayment_TVP.Tables["Credits"].Rows[0]["nPaymentMode"] = 2; }
            else if (_EOBPaymentMode == PaymentModeV2.Cash)
            { dsPayment_TVP.Tables["Credits"].Rows[0]["nPaymentMode"] = 1; }
            else if (_EOBPaymentMode == PaymentModeV2.EFT)
            { dsPayment_TVP.Tables["Credits"].Rows[0]["nPaymentMode"] = 5; }
            else if (_EOBPaymentMode == PaymentModeV2.Voucher)
            { dsPayment_TVP.Tables["Credits"].Rows[0]["nPaymentMode"] = 6; }
            else if (_EOBPaymentMode == PaymentModeV2.MoneyOrder)
            { dsPayment_TVP.Tables["Credits"].Rows[0]["nPaymentMode"] = 3; }
            else if (_EOBPaymentMode == PaymentModeV2.CreditCard)
            { dsPayment_TVP.Tables["Credits"].Rows[0]["nPaymentMode"] = 4; }
            else
            { dsPayment_TVP.Tables["Credits"].Rows[0]["nPaymentMode"] = 0; }

            if (mskCloseDate.MaskCompleted == true)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                dsPayment_TVP.Tables["Credits"].Rows[0]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
            }
            dsPayment_TVP.Tables["Credits"].Rows[0]["nPaymentTrayID"] = Convert.ToInt64(lblPaymentTray.Tag);
            dsPayment_TVP.Tables["Credits"].Rows[0]["sPaymentTrayDesc"] = lblPaymentTray.Text.Trim().Replace("'", "''");
            dsPayment_TVP.Tables["Credits"].Rows[0]["nUserID"] = gloPMGlobal.UserID;
            dsPayment_TVP.Tables["Credits"].Rows[0]["sUserName"] = gloPMGlobal.UserName;

            dsPayment_TVP.Tables["Credits"].Rows[0]["bIsPaymentVoid"] = 0;
            dsPayment_TVP.Tables["Credits"].Rows[0]["nVoidType"] = 0;
            dsPayment_TVP.Tables["Credits"].Rows[0]["dtPaymentVoidCloseDate"] = DBNull.Value;

            dsPayment_TVP.Tables["Credits"].Rows[0]["nPAccountID"] = _nPAccountId;
            dsPayment_TVP.Tables["Credits"].Rows[0]["nAccountPatientID"] = _nAccountPatientId;
            dsPayment_TVP.Tables["Credits"].Rows[0]["nGuarantorID"] = _nGuarantorId;

            dsPayment_TVP.Tables["Credits"].Rows[0]["sPaymentNote"] = "Payment Note";
            dsPayment_TVP.Tables["Credits"].Rows[0]["nPaymentVoidTrayID"] = DBNull.Value;
            dsPayment_TVP.Tables["Credits"].Rows[0]["sPaymentVoidTrayDesc"] = "Blank Tray";

            dsPayment_TVP.Tables["Credits"].Rows[0]["nEntryType"] = PaymentEntryTypeV2.PatientRefund.GetHashCode();

            dsPayment_TVP.Tables["Credits"].Rows[0]["Credits_EXTID"] = 0;
            if (_EOBPaymentMode == PaymentModeV2.CreditCard)
            {
                dsPayment_TVP.Tables["Credits"].Rows[0]["CreditCardType"] = cmbCardType.Text.Trim();
                dsPayment_TVP.Tables["Credits"].Rows[0]["AuthorizationNo"] = txtCardAuthorizationNo.Text.Trim();
                if (mskCreditExpiryDate.MaskCompleted)
                {
                    mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                }
            }
            dsPayment_TVP.Tables["Credits"].Rows[0]["ClinicID"] = gloPMGlobal.ClinicID;
            dsPayment_TVP.Tables["Credits"].Rows[0]["PaymentVoidDateTime"] = DBNull.Value;
            dsPayment_TVP.Tables["Credits"].Rows[0]["CreatedDateTime"] = DateTime.Now;
            dsPayment_TVP.Tables["Credits"].Rows[0]["ModifiedDateTime"] = DateTime.Now;
            dsPayment_TVP.Tables["Credits"].Rows[0]["sVersion"] = Environment.Version.ToString();
            dsPayment_TVP.Tables["Credits"].Rows[0]["sMachineName"] = Environment.MachineName;
            dsPayment_TVP.Tables["Credits"].Rows[0]["SiteID"] = "";
            dsPayment_TVP.Tables["Credits"].Rows[0]["IsFinished"] = false;
            dsPayment_TVP.Tables["Credits"].Rows[0]["IsERAPost"] = false;
            dsPayment_TVP.Tables["Credits"].Rows[0]["BPRID"] = DBNull.Value;

            dsPayment_TVP.Tables["Credits"].AcceptChanges();


        }
        
        private void SetCloseDate()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
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

                        //gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings( gloPMGlobal.DatabaseConnectionString);
                        //Object _retValue = null;
                        //string _clsDate = "";
                        //oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out _retValue);
                        //oSettings.Dispose();

                        //if (_retValue != null && Convert.ToString(_retValue).Trim() != "")
                        //{
                        //    try
                        //    { _clsDate = Convert.ToDateTime(Convert.ToString(_retValue).Trim()).ToString("MM/dd/yyyy");
                        //    _ncloseDate = gloDateMaster.gloDate.DateAsNumber(_clsDate);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        //        ex = null;
                        //        _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
                        //        _ncloseDate = gloDateMaster.gloDate.DateAsNumber(_clsDate);
                        //    }
                        //}
                        //else
                        //{ _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
                        //_ncloseDate = gloDateMaster.gloDate.DateAsNumber(_clsDate);
                        //}

                        //if (_clsDate.Trim() != "")
                        //{

                        //    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_clsDate.Trim())) == true)
                        //    { _clsDate = "";
                        //    _ncloseDate = 0;
                        //    }
                        //    ogloBilling.Dispose();
                        //}

                        //mskCloseDate.Text = _clsDate;

                        string _lastCloseDate = gloBilling.gloBilling.GetUserWiseCloseDay(gloPMGlobal.UserID, gloBilling.CloseDayType.Payment);
                        mskCloseDate.Text = _lastCloseDate;

                        #endregion " Set last selected close date "
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        private void FillPaymentTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Int64 _lastselectedTrayId = 0;
            Object _retVal = null;

            try
            {
                //if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
                //{
                #region " .... Get the last selected Payment tray ... "

                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                Object _retSettingValue = null;
                oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { _lastselectedTrayId = Convert.ToInt64(_retSettingValue); }

                #endregion " .... Get the last selected Payment tray ... "

                #region " ... Get the default Payment Tray .... "

                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK)" +
               " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
               " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloPMGlobal.UserID + " AND nClinicID = " + gloPMGlobal.ClinicID + "";
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _defaultTrayId = Convert.ToInt64(_retVal); }

                #endregion " ... Get the default Payment Tray .... "
                if (oDB != null) { oDB.Dispose(); }
                //...Load the last selected tray if present or else load the default tray
                oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                oDB.Connect(false);
                _retVal = new object();
                if (_lastselectedTrayId > 0)
                {
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _lastselectedTrayId + " and ISNULL(bIsActive,0) = 1 AND nClinicID = " + gloPMGlobal.ClinicID + "");
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
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + gloPMGlobal.ClinicID + "");
                    if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                    {
                        lblPaymentTray.Text = _retVal.ToString(); ;
                        lblPaymentTray.Tag = _defaultTrayId;
                    }
                }

                oDB.Disconnect();

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

       
        #endregion " Private & Public Methods "

    

    }
}