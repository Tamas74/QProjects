using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloSettings;
using gloCommon;
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmInsurancePayRefundViewV2 : Form
    {

        #region " Private Variables "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        string _UserName = "";
        private string _MessageBoxCaption = "";
        private string _closeDayTray = "";

        Label label;

        private Int64 _nInsuranceID = 0;
        private Int64 _nRefEOBPaymentID = 0;
        private Int64 _ncloseDate = 0;
        private Int64 _nRefundcloseDate = 0;
        private Int64 _nrefundid = 0;

        public decimal SelectedUseReserveAmount = 0;

        private DateTime _closeDate = DateTime.Now;

        public bool voidstatus=false;
        private bool _isValidate = true;
        private bool _isDayClose = false;
        private bool _isInsRefVoided = false;

        private gloGeneralItem.gloItems _oSeletedReserveItems = null;

      //  gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControl oRefListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        
        #endregion " Private Variables "

        #region  " Grid Constants "
        
        const int COL_CREDITID = 0;
        const int COL_PAYMODE = 1;
        const int COL_PAYMENTNOTESUBTYPE = 2;
        const int COL_INSURANCECOMPANYID = 3;
        const int COL_INSURANCECOMPANYNAME = 4;
        const int COL_ORIGINALPAYMENT = 5;//Check Number,Date,Amount
        const int COL_NOTE = 6;//Note
        const int COL_AMOUNT = 7;//Amount
        const int COL_RESERVEID = 8;
        
        const int COL_COUNT = 9;
        

        #endregion 

        #region " Property Procedures "

        public Int64 ClinicID
        { get { return gloGlobal.gloPMGlobal.ClinicID; } set { gloGlobal.gloPMGlobal.ClinicID = value; } }
        public Int64  UserID
        { get { return gloGlobal.gloPMGlobal.UserID; } set { gloGlobal.gloPMGlobal.UserID = value; } }
        public string UserName
        { get { return _UserName; } set { _UserName = value; } }
        private Int64 InsuranceID
        { get { return _nInsuranceID; } set { _nInsuranceID = value; } }

        private Int64 RefEOBPaymentID
        { get { return _nRefEOBPaymentID; } set { _nRefEOBPaymentID = value; } }
        public DateTime CloseDate
        {
            get { return _closeDate; }
            set { _closeDate = value; }
        }
        public string CloseDayTray
        {
            get { return _closeDayTray; }
            set { _closeDayTray = value; }
        }
        public gloGeneralItem.gloItems oSeletedReserveItems
        {
            get { return _oSeletedReserveItems; }
            set { _oSeletedReserveItems = value; }
        }

        Int64 _PatientID = 0;
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        string _PatientName = "";
        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }

        string _ClaimNo = "";
        public string ClaimNo
        {
            get { return _ClaimNo; }
            set { _ClaimNo = value; }
        }

        Int64 _nMSTTransactionID = 0;
        public Int64 MSTTransactionID
        {
            get { return _nMSTTransactionID; }
            set { _nMSTTransactionID = value; }
        }

        Int64 _nTransactionID = 0;
        public Int64 TransactionID
        {
            get { return _nTransactionID; }
            set { _nTransactionID = value; }
        }


        #endregion " Property Procedures "

        #region " Constructors "

        public frmInsurancePayRefundViewV2(string DatabaseConnectionString, Int64 InsuranceID, Int64 RefundId, Int64 RefEOBPaymentID)
        {
            InitializeComponent();
            gloGlobal.gloPMGlobal.DatabaseConnectionString = DatabaseConnectionString;
            _nInsuranceID = InsuranceID;
            _nrefundid = RefundId;
            _nRefEOBPaymentID = RefEOBPaymentID;

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

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        #endregion " Constructors "

        #region "Form Event"
     
        private void frmPaymentUseReserve_Load(object sender, EventArgs e)
        {

            gloBilling.Cls_TabIndexSettings.TabScheme scheme = gloBilling.Cls_TabIndexSettings.TabScheme.AcrossFirst;
            gloBilling.Cls_TabIndexSettings tom = new gloBilling.Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);


            gloC1FlexStyle.Style(c1Refund, false);
            lblvoid.Visible = false;           
            //FillCreditCards();
            FillPaymentMode();
            FillPatientRefund();
            SetNoteType();
            gloBilling.gloBilling globill = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");         

            if (IsValidDate(mskCloseDate.Text.Trim()))
          {
                if (globill.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)))
                {
                    _isDayClose = true;
                    DisableControl();
                }
            }
            SetVoidData();
            timer1.Start();
            txtTo.Select();
            txtTo.Focus();
            globill.Dispose();

           
        }

        private void frmPaymentUseReserve_FormClosed(object sender, FormClosedEventArgs e)
        {      
            c1Refund.FinishEditing();
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

                 //   PaymentModeV2 _EOBPaymentMode = PaymentModeV2.None;

                    if (cmbPayMode.Text.Trim() == PaymentModeV2.Cash.ToString())
                    {
                   //     _EOBPaymentMode = PaymentModeV2.Cash;

                        txtCheckNumber.Text = "";
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Ref.# :";
                        lblCheckNo.Enabled = true;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.Enabled = true;
                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.Check.ToString())
                    {
                     //   _EOBPaymentMode = PaymentModeV2.Check;
                        lblCheckDate.Text = "Refund Check Date :";
                        lblCheckNo.Text = "Refund Check# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;

                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.MoneyOrder.ToString())
                    {
                       // _EOBPaymentMode = PaymentModeV2.MoneyOrder;
                        lblCheckDate.Text = "MO Date :";
                        lblCheckNo.Text = "MO# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.CreditCard.ToString())
                    {
                      //  _EOBPaymentMode = PaymentModeV2.CreditCard;
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Card# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = true;
                        txtCheckNumber.MaxLength = 4;

                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.EFT.ToString())
                    {
                      //  _EOBPaymentMode = PaymentModeV2.EFT;
                        lblCheckDate.Text = "EFT Date :";
                        lblCheckNo.Text = "EFT# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }

                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.Voucher.ToString())
                    {
                        //  _EOBPaymentMode = PaymentModeV2.EFT;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (voidstatus == true)
            {
                if (lblvoid.Visible == true)
                {
                    lblvoid.Visible = false;
                }
                else
                {
                    lblvoid.Visible = true;
                }
            }

        }

        private void btnSelectPaymentTray_Click(object sender, EventArgs e)
        {
            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

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
                            MessageBox.Show("Enter valid refund date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidate = false;
                            e.Cancel = true;
                        }

                    }
                    else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                    {
                        MessageBox.Show("Enter refund date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isValidate = false;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Enter valid refund date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show(msg, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidate = false;
                            e.Cancel = true;
                        }

                    }
                    else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                    {
                        msg = "Enter " + lblCheckDate.Text.Replace(":", "").Trim().ToLower() + ".";
                        MessageBox.Show(msg, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isValidate = false;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                msg = "Enter valid " + lblCheckDate.Text.Replace(":", "").Trim().ToLower() + ".";
                MessageBox.Show(msg, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        toolTip1.SetToolTip(lblPaymentTray, lblPaymentTray.Text);
                    }
                    else
                    {
                        toolTip1.SetToolTip(lblPaymentTray, "");
                    }
                }
                else
                {
                  toolTip1.SetToolTip(lblPaymentTray, "");
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
                Ex = null;
            }
        }

        private void lblusername_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                label = (Label)sender;
                if (lblusername.Text != null && lblusername.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblusername.Text), lblusername) >= lblusername.Width - 20)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        toolTip1.SetToolTip(lblusername, lblusername.Text);
                    }
                    else
                    {
                        toolTip1.SetToolTip(lblusername, "");
                    }
                }
                else
                {
                    toolTip1.SetToolTip(lblusername, "");
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
                Ex = null;
            }
        }

        private void lblSourceTxt_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                label = (Label)sender;
                if (lblSourceTxt.Text != null && lblSourceTxt.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblSourceTxt.Text), lblSourceTxt) >= lblSourceTxt.Width - 20)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        toolTip1.SetToolTip(lblSourceTxt, lblSourceTxt.Text);
                    }
                    else
                    {
                        toolTip1.SetToolTip(lblSourceTxt, "");
                    }
                }
                else
                {
                    toolTip1.SetToolTip(lblSourceTxt, "");
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
                Ex = null;
            }

        }

        #region "Tool Strip button events "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            tsb_OK.Select();
            label1.Focus();
            if (_isValidate == true)
            {
                if (SavePaymentValidation())
                {
                    UpdateInsuranceRefund();
                    this.Close();
                }
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tsbVoidRefund_Click(object sender, EventArgs e)
        {
            try
            {
             mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
             if (mskCloseDate.Text.Trim() == string.Empty)
             {
                 MessageBox.Show("Enter close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                 mskCloseDate.Focus();
                 mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                 return;
             }
                if (mskCloseDate.Text.Trim().Length > 0 && !mskCloseDate.MaskCompleted)
                {
                    MessageBox.Show("Enter valid close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Focus();
                    mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    return;
                }
                if (mskCloseDate.MaskCompleted)
                {
                    mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if (IsValidDate(Convert.ToString(mskCloseDate.Text.Trim())) == false)
                    {
                        MessageBox.Show("Enter valid close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Focus();
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                        return;

                    }
                }

                if (mskCloseDate.MaskCompleted == true)
                {
                    if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _nRefundcloseDate)
                    {
                        MessageBox.Show("Close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_nRefundcloseDate).ToShortDateString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Focus();
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                        return;

                    }
                }
                            
                mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
               
               

                if (DialogResult.Yes == MessageBox.Show("Do you want to void refund? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    int _date = 0;
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        _date = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.Trim());

                        frmVoidInsuranceRefundV2 ofrm = new frmVoidInsuranceRefundV2(_date, _nrefundid, _nInsuranceID);
                        ofrm.ShowDialog(this);
                    SetVoidData();
                    ofrm.Dispose();
                }
            }
            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        #endregion

        #region " Grid Events "

        private void c1Refund_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        #endregion

        #region "Close date validation"

        private void mskCloseDate_Validating_1(object sender, CancelEventArgs e)
        {
            try
            {
                if (_isInsRefVoided == false)
                {
                    _isValidate = true;
                    MaskedTextBox mskDate = (MaskedTextBox)sender;
                    mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    string strDate = mskDate.Text;
                    mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if (mskDate != null)
                    {
                        if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) != _ncloseDate)
                        {
                            if (strDate.Length > 0)
                            {
                                if (IsValidDate(mskDate.Text.Trim()) == false)
                                {
                                    MessageBox.Show("Enter valid close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    _isValidate = false;
                                    e.Cancel = true;

                                }
                                else if (mskCloseDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskCloseDate.Name)
                                {
                                    if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                                    {
                                        MessageBox.Show("Close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _isValidate = false;
                                        e.Cancel = true;
                                    }
                                }
                            }
                            else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                            {
                                MessageBox.Show("Enter close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isValidate = false;
                                e.Cancel = true;
                            }
                        }
                    }
                }
                else
                {
                    _isInsRefVoided = false;
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Enter valid close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isValidate = false;
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion "Close date validation"

        #endregion

        #region " Design Grid "

        private void DesignReserveRefundGrid()
        {
            try
            {
                #region " Set Header "

                c1Refund.Cols[COL_CREDITID].Caption = "CreditID";
                c1Refund.Cols[COL_PAYMODE].Caption = "PaymentMode";
                c1Refund.Cols[COL_PAYMENTNOTESUBTYPE].Caption = "Type";
                c1Refund.Cols[COL_INSURANCECOMPANYID].Caption = "InsCompanyID";
                c1Refund.Cols[COL_INSURANCECOMPANYNAME].Caption = "Source";
                c1Refund.Cols[COL_ORIGINALPAYMENT].Caption = "Original Payment";
                c1Refund.Cols[COL_NOTE].Caption = "Note";
                c1Refund.Cols[COL_AMOUNT].Caption = "Refund";
                 c1Refund.Cols[COL_RESERVEID].Caption = "ReserveID";
  
                #endregion

                int _nWidth = 0;

                c1Refund.Cols[COL_CREDITID].Width = 0;
                c1Refund.Cols[COL_PAYMODE].Width = 0;
                c1Refund.Cols[COL_PAYMENTNOTESUBTYPE].Width = 0;
                c1Refund.Cols[COL_INSURANCECOMPANYID].Width = 0;
                c1Refund.Cols[COL_INSURANCECOMPANYNAME].Width = 0;
                c1Refund.Cols[COL_RESERVEID].Width = 0;

                c1Refund.Cols[COL_CREDITID].Visible = false;
                c1Refund.Cols[COL_PAYMODE].Visible = false;
                c1Refund.Cols[COL_PAYMENTNOTESUBTYPE].Visible = false;
                c1Refund.Cols[COL_INSURANCECOMPANYID].Visible = false;
                c1Refund.Cols[COL_INSURANCECOMPANYNAME].Visible = false;
                c1Refund.Cols[COL_RESERVEID].Visible = false;

                _nWidth = 930;//Convert.ToInt32( c1QueuedClaims.Width);

                c1Refund.Cols[COL_ORIGINALPAYMENT].Width = Convert.ToInt32(_nWidth * 0.25) + Convert.ToInt32(_nWidth * 0.15);
                c1Refund.Cols[COL_NOTE].Width = Convert.ToInt32(_nWidth * 0.33) + Convert.ToInt32(_nWidth * 0.10);
                c1Refund.Cols[COL_AMOUNT].Width = Convert.ToInt32(_nWidth * 0.15);

                c1Refund.Cols[COL_AMOUNT].Format = "c";

                c1Refund.ShowCellLabels = false;

             }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null; 
            }
            finally
            {  c1Refund.Redraw = true; }
        }

        #endregion " Design Grid "

        #region " Private & Public Methods "

        private void FillPatientRefund()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet _dsReservesRefund = new DataSet();

            try
            {                           
                //oParameters.Add("@nRefundID", _nrefundid , ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),                
                //oDB.Connect(false);
                //oDB.Retrive("BL_SELECT_Insurance_Refund", oParameters, out _dsReservesRefund);
                //oDB.Disconnect();
                _dsReservesRefund = gloInsurancePaymentV2.FillInsuranceRefund(_nrefundid);
                c1Refund.DataSource = _dsReservesRefund.Tables[0];
                DesignReserveRefundGrid();
                if (_dsReservesRefund.Tables[0].Rows.Count > 0 && _dsReservesRefund.Tables[1].Rows.Count > 0)
                {
                    txtTo.Text = _dsReservesRefund.Tables[1].Rows[0]["sRefundTo"].ToString();
                    txtNotes.Text = _dsReservesRefund.Tables[1].Rows[0]["sRefundNotes"].ToString();
                    txtRefundAmount.Text = "$" + _dsReservesRefund.Tables[1].Rows[0]["nRefundAmount"].ToString();
                    mskrefunddate.Text = String.Format("{0:MM/dd/yyyy}",_dsReservesRefund.Tables[1].Rows[0]["nRefundDate"]);
                    mskCheckDate.Text = String.Format("{0:MM/dd/yyyy}",_dsReservesRefund.Tables[1].Rows[0]["nCheckDate"]);
                    txtCheckNumber.Text = _dsReservesRefund.Tables[1].Rows[0]["sCheckNumber"].ToString();
                    mskCloseDate.Text = String.Format("{0:MM/dd/yyyy}", _dsReservesRefund.Tables[1].Rows[0]["nCloseDate"]); 
                    lblPaymentTray.Text = _dsReservesRefund.Tables[1].Rows[0]["sPaymentTrayDescription"].ToString();
                    lblPaymentTray.Tag = _dsReservesRefund.Tables[1].Rows[0]["nPaymentTrayID"].ToString();
                    lblusername.Text = _dsReservesRefund.Tables[1].Rows[0]["sUserName"].ToString();
                    if (Convert.ToString(_dsReservesRefund.Tables[1].Rows[0]["dtModifiedDateTime"])!= "")
                    {
                        lbldatetime.Text = Convert.ToDateTime(_dsReservesRefund.Tables[1].Rows[0]["dtModifiedDateTime"]).ToString("MM/dd/yyyy hh:mm:ss tt");
                    }
                    //_ncloseDate = gloDateMaster.gloDate.DateAsNumber(_dsReservesRefund.Tables[1].Rows[0]["nCloseDate"].ToString());
                    _nRefundcloseDate=gloDateMaster.gloDate.DateAsNumber(_dsReservesRefund.Tables[1].Rows[0]["nCloseDate"].ToString()); ;
                    lblSourceTxt.Text = _dsReservesRefund.Tables[0].Rows[0]["InsCompanyName"].ToString();

                    //Added By Pramod Nair To Display the Associated Claim and Patient 
                    //lblDisplayClaimNo.Text = _dsReservesRefund.Tables[1].Rows[0]["Claim"].ToString();
                    //lblDisplayPat.Text = _dsReservesRefund.Tables[1].Rows[0]["Patient"].ToString();

                    TxtRefundPatient.Text = _dsReservesRefund.Tables[1].Rows[0]["sRefundAssoPatient"].ToString();
                    TxtRefundPatient.Tag = _dsReservesRefund.Tables[1].Rows[0]["nRefundAssoPatientID"].ToString();



                    getPatientClaimNos(Convert.ToInt64(TxtRefundPatient.Tag), "refund");

                    ////if (CmbRefundClaim.Items.Count > 0)
                    ////{
                    if (CmbRefundClaim.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        CmbRefundClaim.Text = Convert.ToString(_dsReservesRefund.Tables[1].Rows[0]["sRefundAssoClaim"].ToString());
                        CmbRefundClaim.Tag = Convert.ToString(_dsReservesRefund.Tables[1].Rows[0]["nRefundAssoTransactionMstID"]) + '-' + Convert.ToString(_dsReservesRefund.Tables[1].Rows[0]["nRefundAssoTransactionID"]);

                    }
                    else
                    {

                        CmbRefundClaim.Text = _dsReservesRefund.Tables[1].Rows[0]["sRefundAssoClaim"].ToString();
                        CmbRefundClaim.Tag = Convert.ToString(_dsReservesRefund.Tables[1].Rows[0]["nRefundAssoTransactionMstID"]) + '-' + Convert.ToString(_dsReservesRefund.Tables[1].Rows[0]["nRefundAssoTransactionID"]);
                    }
                    //}

                    //CmbRefundClaim.Text = _dsReservesRefund.Tables[1].Rows[0]["sRefundAssoClaim"].ToString();
                    //CmbRefundClaim.ValueMember = Convert.ToString(_dsReservesRefund.Tables[1].Rows[0]["nRefundAssoTransactionMstID"]) + '-' + Convert.ToString(_dsReservesRefund.Tables[1].Rows[0]["nRefundAssoTransactionID"]); //Convert.ToString(c1Reserve.GetData(i, COL_CLAIM));
                    


                    //MaheshB
                    this.cmbPayMode.SelectedIndexChanged -= new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);
                    if (PaymentModeV2.Cash.GetHashCode() == Convert.ToInt16(_dsReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.Cash.ToString();
                        //txtCheckNumber.Text = "";
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Ref.# :";
                        lblCheckNo.Enabled = true;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.Enabled = true;
                    }
                    else if (PaymentModeV2.Check.GetHashCode() == Convert.ToInt16(_dsReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.Check.ToString();
                        lblCheckDate.Text = "Refund Check Date :";
                        lblCheckNo.Text = "Refund Check# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (PaymentModeV2.CreditCard.GetHashCode() == Convert.ToInt16(_dsReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.CreditCard.ToString();
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Card# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = true;
                        txtCheckNumber.MaxLength = 4;
                        cmbCardType.SelectedValue = _dsReservesRefund.Tables[1].Rows[0]["nCardID"].ToString().Trim();
                        txtCardAuthorizationNo.Text = _dsReservesRefund.Tables[1].Rows[0]["sAuthorizationNo"].ToString();
                        if (_dsReservesRefund.Tables[1].Rows[0]["nCardExpDate"].ToString().Length == 3)
                        {
                            mskCreditExpiryDate.Text = "0" + _dsReservesRefund.Tables[1].Rows[0]["nCardExpDate"].ToString();
                        }
                        else
                        {
                            if (Convert.ToString(_dsReservesRefund.Tables[1].Rows[0]["nCardExpDate"]) != "0")
                            {
                                mskCreditExpiryDate.Text = _dsReservesRefund.Tables[1].Rows[0]["nCardExpDate"].ToString();
                            }
                        }

                    }
                    else if (PaymentModeV2.EFT.GetHashCode() == Convert.ToInt16(_dsReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.EFT.ToString();
                        lblCheckDate.Text = "EFT Date :";
                        lblCheckNo.Text = "EFT# :";
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (PaymentModeV2.Voucher.GetHashCode() == Convert.ToInt16(_dsReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.Voucher.ToString();
                        lblCheckDate.Text = "Voucher Date :";
                        lblCheckNo.Text = "Voucher# :";
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (PaymentModeV2.MoneyOrder.GetHashCode() == Convert.ToInt16(_dsReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = PaymentModeV2.MoneyOrder.ToString();
                        lblCheckDate.Text = "MO Date :";
                        lblCheckNo.Text = "MO# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    this.cmbPayMode.SelectedIndexChanged += new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);

                }
                else
                {
                    lblSourceTxt.Text = "";
                }
                DataTable _dtRefCloseDate = new DataTable();
                //oDB.Connect(false);
                //string strQuery = "SELECT isnull(BL_EOBPayment_DTL.nCloseDate,0) as nCloseDate FROM BL_EOBPayment_DTL WITH (NOLOCK) LEFT OUTER JOIN " +
                //" dbo.BL_EOBPayment_MST WITH (NOLOCK) ON dbo.BL_EOBPayment_DTL.nEOBPaymentID = dbo.BL_EOBPayment_MST.nEOBPaymentID WHERE (BL_EOBPayment_DTL.nPaymentType = 1) " +
                //" AND (BL_EOBPayment_DTL.nPaymentSubType = 9) AND(dbo.BL_EOBPayment_DTL.nPaySign = 2) AND (ISNULL(BL_EOBPayment_MST.nVoidType,0) NOT IN (3, 5, 9, 8)) " +
                //" AND BL_EOBPayment_DTL.nEOBPaymentID = (SELECT TOP 1 BL_EOBPayment_DTL.nRefEOBPaymentID from BL_EOBPayment_DTL WITH (NOLOCK) where nEOBPaymentID=" + _nRefEOBPaymentID + " AND nPaySign = 2 ORDER BY nCloseDate)";
                ////oDB.Retrive_Query("Select ISNULL(nCloseDate,0) as nclosedate from view_SelectPaymentCloseDate where nEOBPaymentID = (SELECT BL_EOBPayment_DTL.nRefEOBPaymentID from BL_EOBPayment_DTL where nEOBPaymentID=" + _nRefEOBPaymentID + " AND nPaySign = 2 )", out _dtReserves);
                //oDB.Retrive_Query(strQuery, out _dtRefCloseDate);
                _dtRefCloseDate = gloInsurancePaymentV2.getRefundCloseDate(_nRefEOBPaymentID);
                if (_dtRefCloseDate != null && _dtRefCloseDate.Rows.Count > 0)
                {
                    _ncloseDate = gloDateMaster.gloDate.DateAsNumber(_dtRefCloseDate.Rows[0]["nclosedate"].ToString());
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                if (oDB != null)
                    oDB.Dispose();

            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
        }
    
        private void FillPaymentMode()
        {
            cmbPayMode.Items.Clear();
            //cmbPayMode.Items.Add(PaymentMode.Cash.ToString());
            cmbPayMode.Items.Add(PaymentMode.Check.ToString());
            //cmbPayMode.Items.Add(PaymentMode.CreditCard.ToString());
            //cmbPayMode.Items.Add(PaymentMode.MoneyOrder.ToString());
            //cmbPayMode.Items.Add(PaymentMode.EFT.ToString());

            for (int i = 0; i <= cmbPayMode.Items.Count - 1; i++)
            {
                if (cmbPayMode.Items[i].ToString() == PaymentMode.Check.ToString())
                {
                    cmbPayMode.SelectedIndex = i;
                    break;
                }
            }

        }

        //private void FillCreditCards()
        //{
        //    CreditCards oCreditCards = new CreditCards(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    DataTable _dtCards = null;

        //    try
        //    {
        //        cmbCardType.DataSource = null;
        //        cmbCardType.Items.Clear();
        //        _dtCards = oCreditCards.GetList();

        //        if (_dtCards != null && _dtCards.Rows.Count > 0)
        //        {
        //            DataRow _dr = _dtCards.NewRow();
        //            _dr["nCreditCardID"] = 0;
        //            _dr["sCreditCardDesc"] = "";
        //            _dtCards.Rows.InsertAt(_dr, 0);
        //            _dtCards.AcceptChanges();

        //            cmbCardType.DataSource = _dtCards.Copy();
        //            cmbCardType.ValueMember = _dtCards.Columns[0].ColumnName;
        //            cmbCardType.DisplayMember = _dtCards.Columns[1].ColumnName;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    {
        //        if (oCreditCards != null) { oCreditCards.Dispose(); } 
        //        //if (_dtCards != null) { _dtCards.Dispose(); }
        //    }
        //}

        private bool SavePaymentValidation()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            int _addDays = 0;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();

            MaskedTextBox mskDate = mskCloseDate;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskDate.Text;
            mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            try
            {
                if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) != _ncloseDate)
                {
                    if (strDate.Trim() == "")
                    {
                        MessageBox.Show("Enter close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select();
                        mskCloseDate.Focus();
                        return false;
                    }

                    if (IsValidDate(mskDate.Text.Trim()) == false)
                    {
                        MessageBox.Show("Enter valid close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select();
                        mskCloseDate.Focus();
                        return false;

                    }

                    else if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                    {
                        MessageBox.Show("Close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select();
                        mskCloseDate.Focus();
                        return false;
                    }


                    else if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                    {
                        //Commneted by Pramod Nair To Allow the user to add or remove the patient or claim number from Insurance Company Reserves and Refunds.   The close date does not matter.  
                        //MessageBox.Show("Selected closed date is being closed. Select open date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //mskCloseDate.Select();
                        //mskCloseDate.Focus();
                        //return false;
                    }


                    else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                    {
                        MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mskCloseDate.Focus();
                        mskCloseDate.Select();
                        return false;
                    }
                    else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                    {
                        DialogResult _dlgCloseDate = DialogResult.None;
                        _dlgCloseDate = MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (_dlgCloseDate == DialogResult.Cancel)
                        {
                            mskCloseDate.Focus();
                            mskCloseDate.Select();
                            return false;
                        }
                        else
                        {
                            goto outer;
                        }
                    }
                }

                outer:
                
                if (lblPaymentTray.Tag == null || lblPaymentTray.Tag.ToString().Trim() == "" || Convert.ToInt64(lblPaymentTray.Tag) <= 0)
                {
                    MessageBox.Show("Select payment tray.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSelectPaymentTray.Select();
                    btnSelectPaymentTray.Focus();
                    return false;
                }

                if (txtTo.Text.Trim() == "")
                {
                    MessageBox.Show("Enter name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Enter refund date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskrefunddate.Select();
                    mskrefunddate.Focus();
                    return false;
                }
                if (IsValidDate(mskDate.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter valid refund date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskrefunddate.Select();
                    mskrefunddate.Focus();
                    return false;
                }      

                if (txtRefundAmount.Text.Trim().Length == 0 || Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim()) <= 0)
                {
                    MessageBox.Show("Enter refund amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (c1Refund.Rows.Count > 1)
                    {
                        c1Refund.Focus();
                        c1Refund.Select(1, COL_AMOUNT);
                    }
                    return false;
                }

                PaymentModeV2 _EOBPaymentMode = PaymentModeV2.Check;
                //if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                //{ _EOBPaymentMode = EOBPaymentMode.Cash; }
                //if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                //{ _EOBPaymentMode = EOBPaymentMode.Check; }
                //else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                //{ _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                //else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                //{ _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                //else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                //{ _EOBPaymentMode = EOBPaymentMode.EFT; }

                if (_EOBPaymentMode == PaymentModeV2.None)
                {
                    MessageBox.Show("Select refund mode.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPayMode.Select();
                    cmbPayMode.Focus();
                    return false;
                }
                else if (_EOBPaymentMode == PaymentModeV2.CreditCard)
                {

                    if (mskCheckDate.MaskCompleted == false)
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                    if (cmbCardType == null || cmbCardType.Items.Count <= 0 || cmbCardType.Text.Trim() == "")
                    {
                        MessageBox.Show("Select card type.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbCardType.Select();
                        cmbCardType.Focus();
                        return false;
                    }

                    mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskCreditExpiryDate.Text != "")
                    {
                        if (mskCreditExpiryDate.MaskFull == false)
                        {
                            MessageBox.Show("Enter valid " + _EOBPaymentMode.ToString().ToLower() + " expiration date (MM/yy).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " number.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToLower() + " date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                    if (IsValidDate(mskDate.Text.Trim()) == false)
                    {
                        MessageBox.Show("Enter valid " + _EOBPaymentMode.ToString().ToLower() + " date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Select();
                        mskCheckDate.Focus();
                        return false;
                    }

                }
                else if (_EOBPaymentMode == PaymentModeV2.EFT)
                {
                    if (txtCheckNumber.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToUpper() + " number.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckNumber.Select();
                        txtCheckNumber.Focus();
                        return false;
                    }
                }
                else if (_EOBPaymentMode == PaymentModeV2.Voucher)
                {
                    if (txtCheckNumber.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter " + _EOBPaymentMode.ToString().ToUpper() + " number.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckNumber.Select();
                        txtCheckNumber.Focus();
                        return false;
                    }
                }

               
                if (txtNotes.Text.Trim() == "")
                {
                    MessageBox.Show("Enter refund note.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNotes.Select();
                    txtNotes.Focus();
                    return false;
                }

            }
            catch (Exception) // ex)
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

        private void SetVoidData()
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            //string _strQuery = "";
            DataTable dtVoidData = new DataTable();
            try
            {
            //    _strQuery = "SELECT bisvoid,dtVoidDateTime,sVoidUserName,nVoidTrayDescription,nVoidCloseDate FROM BL_EOBInsurance_Refund WITH (NOLOCK) WHERE isnull(bIsvoid ,0) = 1 and nRefundID=" + _nrefundid;
            //    oDB.Connect(false);
            //    oDB.Retrive_Query(_strQuery, out dtVoidData);
                dtVoidData=gloInsurancePaymentV2.getVoidData(_nrefundid);
                if (dtVoidData != null & dtVoidData.Rows.Count > 0)
                {
                    _isInsRefVoided = true;
                    voidstatus = true;
                    lblvoid.Visible = true;
                    DisableControl();
                    lblvoid.Text = "Voided [" + dtVoidData.Rows[0]["sVoidUserName"].ToString() + "] on " + string.Format("{0:MM/dd/yyyy}",dtVoidData.Rows[0]["nVoidCloseDate"]);
                    //lblusername.Text = dtVoidData.Rows[0]["sVoidUserName"].ToString();
                    //lblvoidtray.Text = dtVoidData.Rows[0]["nVoidTrayDescription"].ToString();
                    //lbldatetime.Text = dtVoidData.Rows[0]["dtVoidDateTime"].ToString();
                    tsb_void_refund.Enabled = false;
                    tsb_OK.Enabled = false;
                    txtNotes.Enabled = false;

                    //Added By Pramod Nair to Diable the Patient Association After Refund Void
                    TxtRefundPatient.Enabled = false;
                    btnSearchRefundPatient.Enabled = false;
                    btnClearRefundPatient.Enabled = false;
                    CmbRefundClaim.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DisableControl()
        {
            mskCheckDate.Enabled = false;
            mskCloseDate.Enabled = false;
            mskrefunddate.Enabled = false;
            txtCardAuthorizationNo.Enabled = false;
            txtCheckNumber.Enabled = false;
            txtNotes.Enabled = false;
            btnSelectPaymentTray.Enabled = false;
            txtTo.Enabled = false;
            tsb_OK.Enabled = false;
            tsb_void_refund.Enabled = false;
            cmbPayMode.Enabled = false;
            cmbCardType.Enabled = false;
            mskCreditExpiryDate.Enabled = false;
            if (_isDayClose == true)
            {
                tsb_void_refund.Enabled = true;
                tsb_OK.Enabled = true;
                txtNotes.Enabled = true;
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

        private void UpdateInsuranceRefund()
        {

            DateTime closedate = DateTime.Now;
            DateTime checkdate = DateTime.Now;
            string checkno = "";
         //   string cardtype = "";
         //   long cardid = 0;
         //   string AuthorizationNo = "";
         //   long Cardexp = 0;

            PaymentModeV2 _EOBPaymentMode = PaymentModeV2.Check;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            //EOBPayment.Common.PatientPaymentReturn oPatientPaymentReturn = new global::gloBilling.EOBPayment.Common.PatientPaymentReturn();
            gloAccountsV2.PaymentCollection.InsurancePaymentRefundV2 oInsurancePaymentRefundV2 = new gloAccountsV2.PaymentCollection.InsurancePaymentRefundV2();

            getValidClaimDetails();
            try
            {
                if (_isDayClose == false)
                {

                    #region "Payment Mode"
                    //if (cmbPayMode.Text != "")
                    //{
                    //    if (cmbPayMode.Text.Trim() == EOBPaymentMode.None.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.None; }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.Cash; }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                    //    {
                    _EOBPaymentMode = PaymentModeV2.Check;
                    //    }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                    //    {
                    //        _EOBPaymentMode = EOBPaymentMode.CreditCard;
                    //        cardtype = cmbCardType.Text;
                    //        cardid = Convert.ToInt64(cmbCardType.SelectedValue);
                    //        AuthorizationNo = txtCardAuthorizationNo.Text;
                    //        if (mskCreditExpiryDate.MaskCompleted)
                    //        {
                    //            mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    //            Cardexp = Convert.ToInt64(mskCreditExpiryDate.Text);
                    //        }
                    //    }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.EFT; }
                    //}
                    #endregion


                    if (mskrefunddate.MaskCompleted)
                    {
                        mskrefunddate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oInsurancePaymentRefundV2.Refunddate = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", mskrefunddate.Text)).Date;
                    }

                    oInsurancePaymentRefundV2.RefundNotes = txtNotes.Text;
                    oInsurancePaymentRefundV2.RefundTo = txtTo.Text;

                    if (mskCloseDate.MaskCompleted)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        closedate = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", mskCloseDate.Text)).Date;
                    }
                    if (mskCheckDate.MaskCompleted)
                    {
                        mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        checkdate = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", mskCheckDate.Text)).Date;
                    }
                    checkno = txtCheckNumber.Text.Trim();

                    oParameters.Add("@Flag", _isDayClose, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@nRefundID", _nrefundid, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),       
                    oParameters.Add("@nClosedate", closedate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@nPaymentTrayId", lblPaymentTray.Tag, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sPaymentTrayDesc", lblPaymentTray.Text, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sRefundto", oInsurancePaymentRefundV2.RefundTo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nRefundDate", oInsurancePaymentRefundV2.Refunddate, ParameterDirection.Input, SqlDbType.Date);
                    oParameters.Add("@sRefundNote", oInsurancePaymentRefundV2.RefundNotes, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nPaymentmode", _EOBPaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oParameters.Add("@sCheckno", checkno, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nCheckdate", checkdate, ParameterDirection.Input, SqlDbType.Date);
                    //oParameters.Add("@sCardType", cardtype, ParameterDirection.Input, SqlDbType.VarChar);
                    //oParameters.Add("@nCardID", cardid, ParameterDirection.Input, SqlDbType.BigInt);
                    //oParameters.Add("@sAuthorizationNo", AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oParameters.Add("@nCardExpDate", Cardexp, ParameterDirection.Input, SqlDbType.BigInt);
                    //oParameters.Add("@dtModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);

                }
                else
                {
                    oParameters.Add("@Flag", _isDayClose, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@nRefundID", _nrefundid, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),   
                    oParameters.Add("@sRefundNote", txtNotes.Text, ParameterDirection.Input, SqlDbType.VarChar);
                    //oParameters.Add("@dtModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);

                }

                //if (CmbRefundClaim.Text != null)
                //{
                //    Int64 nRefundAssoTransactionMstID = 0;
                //    Int64 nRefundAssoTransactionID = 0;
                //    string sClaimNo = "";

                //    string[] IDs = CmbRefundClaim.SelectedValue.ToString().Split('-');
                //    if (IDs.Length.Equals(2))
                //    {
                //        nRefundAssoTransactionMstID = Convert.ToInt64(IDs[0]);
                //        nRefundAssoTransactionID = Convert.ToInt64(IDs[1]);
                //        sClaimNo = Convert.ToString(CmbRefundClaim.Text);

                //    }
                //    oParameters.Add("@nRefundAssoTransactionMstID", nRefundAssoTransactionMstID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),  
                //    oParameters.Add("@nRefundAssoTransactionID", nRefundAssoTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),  
                //    oParameters.Add("@sRefundAssoClaim", sClaimNo, ParameterDirection.Input, SqlDbType.VarChar);

                //}

                oParameters.Add("@nRefundAssoTransactionMstID", MSTTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),  
                oParameters.Add("@nRefundAssoTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),  
                oParameters.Add("@sRefundAssoClaim", ClaimNo, ParameterDirection.Input, SqlDbType.VarChar);

                if (Convert.ToString(TxtRefundPatient.Tag) != "")
                {
                    Int64 nRefundAssoPatientID = Convert.ToInt64(TxtRefundPatient.Tag);
                    oParameters.Add("@nRefundAssoPatientID", nRefundAssoPatientID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),  

                }


                oDB.Connect(false);
                oDB.Execute("BL_UPDATE_INSURANCE_REFUND_V2", oParameters);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oInsurancePaymentRefundV2 != null) { oInsurancePaymentRefundV2.Dispose(); }

            }

        }

        private void SetNoteType()
        {
            if (c1Refund.DataSource != null && c1Refund.Rows.Count > 0)
            {

                for (int _rowIndex = 1; _rowIndex < c1Refund.Rows.Count; _rowIndex++)
                {
                    string _tempData = "";
                    _tempData = ((PaymentModeV2)Convert.ToInt32(c1Refund.Rows[_rowIndex][COL_PAYMODE])).ToString() +
                     c1Refund.Rows[_rowIndex][COL_ORIGINALPAYMENT];
                    c1Refund.SetData(_rowIndex, COL_ORIGINALPAYMENT, _tempData);//Check Number,Date,Amount                   
                    c1Refund.SetData(_rowIndex, COL_PAYMENTNOTESUBTYPE, ((NoteSubTypeV2)Convert.ToInt32(c1Refund.Rows[_rowIndex][COL_PAYMENTNOTESUBTYPE])).ToString());//Copay,Advance,Other
                }
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

        #endregion " Private & Public Methods "

        private void tsb_view_reserve_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1Refund.DataSource != null && c1Refund.Rows.Count > 1)
                {
                    if (c1Refund.RowSel > 0)
                    {
                        Int64 _nEOBPaymentID = Convert.ToInt64(c1Refund.GetData(c1Refund.RowSel, COL_CREDITID));
                        Int64 _nInsCompanyID = Convert.ToInt64(c1Refund.GetData(c1Refund.RowSel, COL_INSURANCECOMPANYID));
                        Int64 _nResEOBPaymentDetailID = Convert.ToInt64(c1Refund.GetData(c1Refund.RowSel, COL_RESERVEID));
                        frmViewInsuranceReserveV2 ofrmViewInsuranceReserve = new frmViewInsuranceReserveV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, _nEOBPaymentID, _nInsCompanyID, _nResEOBPaymentDetailID);
                        ofrmViewInsuranceReserve.ShowInTaskbar = false;
                        ofrmViewInsuranceReserve.StartPosition = FormStartPosition.CenterScreen;
                        ofrmViewInsuranceReserve.ShowDialog(this);
                        ofrmViewInsuranceReserve.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchRefundPatient_Click(object sender, EventArgs e)
        {
            try
            {
                if (oRefListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oRefListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                }
                oRefListControl = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.Patient, false, this.Width);
                oRefListControl.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                oRefListControl.ControlHeader = " Patient";

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oRefListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oRefListControl_ItemSelectedClick);
                oRefListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oRefListControl_ItemClosedClick);

                this.Controls.Add(oRefListControl);

                oRefListControl.OpenControl();
                oRefListControl.Dock = DockStyle.Fill;
                oRefListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearRefundPatient_Click(object sender, EventArgs e)
        {
            TxtRefundPatient.Text = "";
            TxtRefundPatient.Tag = "";
        //    CmbRefundClaim.Items.Clear();
            CmbRefundClaim.DataSource = null;
            CmbRefundClaim.Items.Clear();
            CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
            CmbRefundClaim.Text = "";
          
        }

        void oRefListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;
            switch (_CurrentControlType)
            {

                case gloListControl.gloListControlType.Patient:
                    {
                        TxtRefundPatient.Text = "";
                        if (oRefListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oRefListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oRefListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oRefListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            //TxtRefundPatient.Text  = oBindTable;
                            TxtRefundPatient.Text = Convert.ToString(oBindTable.Rows[0]["DispName"]);
                            TxtRefundPatient.Tag = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                           
                        }

                        DataTable dtClaim = new DataTable();
                      
                        CmbRefundClaim.DataSource = null;
                        CmbRefundClaim.Items.Clear();
                        CmbRefundClaim.Text = "";
                        getPatientClaimNos(Convert.ToInt64(TxtRefundPatient.Tag), "refund");

                    }

                    break;

            }
            this.Width = 970;
            this.Height = 470;
        }

        void oRefListControl_ItemClosedClick(object sender, EventArgs e)
        {
            if (oRefListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oRefListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }


            }
            this.Width = 970;
            this.Height = 470;
        }

        private void getPatientClaimNos(Int64 nPatientID, string ClaimReff)
        {
            DataTable _dtClaimNo = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            try
            {
                //string _strSql = "";
                //_strSql = "SELECT CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionMasterID )+ '-' + CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionID) AS ID, "
                //         + " dbo.GetSubClaimNumber(BL_Transaction_Claim_MST.nClaimNo,BL_Transaction_Claim_MST.nSubClaimNo ,BL_Transaction_Claim_MST.sMainClaimNo,5) as Claim  "
                //         + " FROM BL_Transaction_Claim_MST  WITH (NOLOCK) WHERE (LEFT(nSubClaimNo,1)<> '-') AND nPatientID = " + nPatientID 
                //         + " ORDER BY dtCreateDate DESC";

                //oDB.Connect(false);
                //oDB.Retrive_Query(_strSql, out _dtClaimNo);
                //oDB.Disconnect();
                //if (oDB != null) { oDB.Dispose(); }

                _dtClaimNo = gloInsurancePaymentV2.getPatientClaimNos(nPatientID);
                
                    if (_dtClaimNo != null && _dtClaimNo.Rows.Count > 0)
                    {
                        if (_dtClaimNo.Rows.Count > 1)
                        {
                            DataRow dr = _dtClaimNo.NewRow();
                            dr["Claim"] = "";
                            dr["ID"] = 0;
                            _dtClaimNo.Rows.InsertAt(dr, 0);

                            CmbRefundClaim.DropDownStyle = ComboBoxStyle.DropDownList;
                            CmbRefundClaim.DataSource = _dtClaimNo;
                            CmbRefundClaim.DisplayMember = "Claim";
                            CmbRefundClaim.ValueMember = "ID";

                        }
                        else
                        {

                            CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
                            CmbRefundClaim.DataSource = _dtClaimNo;
                            CmbRefundClaim.DisplayMember = "Claim";

                        }
                    }
                    else
                    {
                        CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
                    }

                


            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

        }

        private void CmbRefundClaim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                if (e.KeyChar == Convert.ToChar(45) && CmbRefundClaim.Text.Contains("-") == true)
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == Convert.ToChar(45) && CmbRefundClaim.Text.Contains("-") == false)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }

            if (e.KeyChar == 13)
            {
                getValidClaimDetails();
            }
        }



        private bool getValidClaimDetails()
        {


            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

            try
            {

                if (CmbRefundClaim.Text.StartsWith("-"))
                {
                    MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtRefundPatient.Text = "";
                    CmbRefundClaim.Text = "";
                    return false;
                }
                else
                {
                    this.MSTTransactionID = 0;
                    this.TransactionID = 0;
                    this.PatientID = 0;
                    this.PatientName = ""; ;
                    ClaimNo = "";

                    ogloBilling.ClaimNumber = CmbRefundClaim.Text;
                    ogloBilling.SetClaimNumbers();

                    if (ogloBilling.MainClaimNumber != 0 || ogloBilling.SubClaimNumber != "")
                    {
                        SplitClaimDetails ClaimDetails = new SplitClaimDetails(ogloBilling.MainClaimNumber, ogloBilling.SubClaimNumber);
                        if (!ClaimDetails.IsClaimExist)
                        {
                            MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TxtRefundPatient.Text = "";
                            return false;
                        }
                        else
                        {
                            DataTable dtTransactionID = new DataTable();
                            dtTransactionID = gloInsurancePaymentV2.getValidClaimDetails(ogloBilling.MainClaimNumber, ogloBilling.SubClaimNumber);
                            //string _strSql = "";

                            //if (ogloBilling.SubClaimNumber == "")
                            //{
                            //    _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                            //        + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                            //        + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                            //        + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                            //        + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + "";
                            //}
                            //else
                            //{
                            //    _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                            //            + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                            //            + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                            //            + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                            //            + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + " AND nSubClaimNo = " + ogloBilling.SubClaimNumber;
                            //}
                            //oDB.Connect(false);
                            //oDB.Retrive_Query(_strSql, out dtTransactionID);
                            //oDB.Disconnect();
                            //if (oDB != null) { oDB.Dispose(); }


                            if (ClaimDetails.TransactionMasterID == 0)
                            {
                                this.MSTTransactionID = Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionMasterID"]);
                                this.TransactionID = Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionID"]);
                                ClaimNo = CmbRefundClaim.Text;
                                CmbRefundClaim.Tag = Convert.ToString(MSTTransactionID) + '-' + Convert.ToString(TransactionID);
                            }
                            else
                            {
                                this.MSTTransactionID = ClaimDetails.TransactionMasterID;
                                this.TransactionID = ClaimDetails.TransactionID;
                                ClaimNo = Convert.ToString(ClaimDetails.ClaimNo);
                                CmbRefundClaim.Tag = Convert.ToString(MSTTransactionID) + '-' + Convert.ToString(TransactionID);
                            }

                            TxtRefundPatient.Text = "";
                            this.PatientID = Convert.ToInt64(dtTransactionID.Rows[0]["nPatientID"]);
                            this.PatientName = Convert.ToString(dtTransactionID.Rows[0]["Patient"]);
                            TxtRefundPatient.Text = PatientName;
                            TxtRefundPatient.Tag = Convert.ToInt64(PatientID);



                        }
                    }
                    else
                    {
                        this.PatientID = Convert.ToInt64(TxtRefundPatient.Tag);
                        this.PatientName = TxtRefundPatient.Text;
                    }

                } return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return false;

            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        private void CmbRefundClaim_Leave(object sender, EventArgs e)
        {
            getValidClaimDetails();
        }
    


    }
}
