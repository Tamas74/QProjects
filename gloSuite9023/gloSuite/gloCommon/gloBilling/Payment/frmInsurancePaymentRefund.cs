using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloSettings;

namespace gloBilling
{
    public partial class frmInsurancePaymentRefund : Form
    {

        #region " Variables Declaration"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = "";
        private string _closeDayTray = "";
        private string _paymentPrefix = "GPM#";
        string _UserName = "";

        private Int64 _ClinicID = 0;
        private Int64 _nInsuranceID = 0;
        private Int64 _ncloseDate = 0;
        Int64 _UserId = 0; 
        
        private bool _IsFormLoading = false;
        private bool _isValidResAmount = true;
        private bool _isValidate = true;
        private bool _isFormClosing = false;
        
        public decimal SelectedUseReserveAmount = 0;
       
        private Label label;
        
        private DateTime _closeDate = DateTime.Now;
        
        private gloGeneralItem.gloItems _oSeletedReserveItems = null;
             

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControl oRefListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        public EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsurancePaymentMasterLines = null;

        #endregion " Private Variables "

        #region  " Grid Constants "
        
        const int COL_EOBPAYMENTID = 0;
        const int COL_EOBID = 1;
        const int COL_EOBDTLID = 2;
        const int COL_EOBPAYMENTDTLID = 3;
        const int COL_BLTRANSACTIONID = 4;
        const int COL_BLTRANDTLID = 5;
        const int COL_BLTRANLINEID = 6;
        const int COL_DOSFROM = 7;
        const int COL_DOSTO = 8;
        const int COL_PATIENTID = 9;
        const int COL_INSURANCECOMPANY = 10;
        const int COL_ORIGINALPAYMENT = 11;//Check Number,Date,Amount
        const int COL_ASSO_PATIENT = 12;
        const int COL_ASSO_CLAIM = 13;
        const int COL_PAYMENTCLOSEDATE = 14;
        const int COL_TORESERVES = 15;//Amount for reserve
        const int COL_TYPE = 16;//Copay,Advance,Other
        const int COL_NOTE = 17;//Note
        const int COL_AVAILABLE = 18;//Available amount
        const int COL_USERESERVE = 19;//Used Reserve
        const int COL_REFUND = 20;//Current amount to use from avaiable amount
        const int COL_PAYMODE = 21;
        const int COL_REFEOBPAYID = 22;
        const int COL_REFEOBPAYDTLID = 23;
        const int COL_ACCOUNTID = 24;
        const int COL_ACCOUNTTYPE = 25;
        const int COL_MSTACCOUNTID = 26;
        const int COL_MSTACCOUNTTYPE = 27;
        const int COL_RES_EOBPAYID = 28;
        const int COL_RES_EOBPAYDTLID = 29;
        const int COL_SOURCE = 30; //Patient or Insurance Name
        const int COL_PAYMENTMODE = 31;
        const int COL_PAYMENTMODENO = 32;

        const int COL_ASSO_MSTTRANSACTIONID = 33;
        const int COL_ASSO_TRANSACTIONID = 34;
        const int COL_ASSO_PATIENTID = 35;

        const int COL_COUNT = 36;
        

       
        #endregion 

        #region " Property Procedures "

        public Int64 ClinicID
        { 
            get { return _ClinicID; } 
            set { _ClinicID = value; } 
        }
        public Int64  UserID
        { 
            get { return _UserId; } 
            set { _UserId = value; } 
        }
        public string UserName
        { 
            get { return _UserName; } 
            set { _UserName = value; }
        }
        private Int64 InsuranceID
        { 
            get { return _nInsuranceID; } 
            set { _nInsuranceID = value; }
        }
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

        Int64 _insuranceCompanyID = 0;
        private Int64 SelectedInsuranceCompanyID
        {
            get { return _insuranceCompanyID; }
            set
            {
                _insuranceCompanyID = value;
                lblInsCompany.Tag = _insuranceCompanyID;

                if (_insuranceCompanyID == 0)
                { SelectedInsuranceCompany = string.Empty; }
            }
        }

        string _insuranceCompanyName = string.Empty;
        private string SelectedInsuranceCompany
        {
            get { return _insuranceCompanyName; }
            set
            {
                _insuranceCompanyName = value;
                lblInsCompany.Text = _insuranceCompanyName;
            }
        }

        //---
       
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

        public Int64 UsedInsuranceReserveID { get; set; }

        private SplitClaimDetails _ClaimDetails = new SplitClaimDetails();

        private SplitClaimDetails ClaimDetails
        {
            get { return _ClaimDetails; }
            set
            {
                _ClaimDetails = value;

                cmbClaimNo.Text = string.Empty;
                if (_ClaimDetails.IsClaimExist)
                { cmbClaimNo.Text = _ClaimDetails.ClaimDisplayNo; }
            }
        }

        private SplitClaimDetails RefundClaimDetails
        {
            get { return _ClaimDetails; }
            set
            {
                _ClaimDetails = value;

                CmbRefundClaim.Text = string.Empty;
                if (_ClaimDetails.IsClaimExist)
                { CmbRefundClaim.Text = _ClaimDetails.ClaimDisplayNo; }
            }
        }

        //--x--

        #endregion " Property Procedures "

        #region " Constructors "

        public frmInsurancePaymentRefund(string DatabaseConnectionString, Int64 InsuranceId)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _nInsuranceID = InsuranceId;
            SelectedInsuranceCompanyID = InsuranceId;
            //SelectedInsuranceCompanyID = InsuranceId;
            #region " Retrive Clinic ID "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion " Retrive Clinic ID "

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
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);

            DesignPaymentGrid(c1Reserve);
            gloC1FlexStyle.Style(c1Reserve, false);
            if (_nInsuranceID == 0)
            {
                this.Show();
                SelectInsuranceCompany();
            }
            SetCloseDate();
            mskrefunddate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now.Date);
            FillPaymentTray();
           
            SetRefundTo();
            FillPaymentMode();
            FillInsuranceCompany();

            FillReserves();
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

                   //     EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.Check;

                        if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                        {
                     //       _EOBPaymentMode = EOBPaymentMode.Cash;

                            txtCheckNumber.Text = "";
                            lblCheckDate.Text = "Date :";
                            lblCheckNo.Text = "Ref.# :";
                            lblCheckNo.Enabled = true;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.Enabled = true;
                        }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                        {
                       //     _EOBPaymentMode = EOBPaymentMode.Check;
                            lblCheckDate.Text = "Refund Check Date :";
                            lblCheckNo.Text = "Refund Check# :";
                            //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.MaxLength = 15;

                        }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                        {
                         //   _EOBPaymentMode = EOBPaymentMode.MoneyOrder;
                            lblCheckDate.Text = "MO Date :";
                            lblCheckNo.Text = "MO# :";
                            //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.MaxLength = 15;
                        }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                        {
                           // _EOBPaymentMode = EOBPaymentMode.CreditCard;
                            lblCheckDate.Text = "Date :";
                            lblCheckNo.Text = "Card# :";
                            //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                            pnlCredit.Enabled = true;
                            txtCheckNumber.MaxLength = 4;

                        }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                        {
                           // _EOBPaymentMode = EOBPaymentMode.EFT;
                            lblCheckDate.Text = "EFT Date :";
                            lblCheckNo.Text = "EFT# :";
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

            private void btnSearchCompany_Click(object sender, EventArgs e)
            {
                SelectInsuranceCompany();
               
            }

            private void lblInsCompany_MouseEnter(object sender, EventArgs e)
            {
                try
                {
                    label = (Label)sender;
                    if (lblInsCompany.Text != null && lblInsCompany.Text != "")
                    {
                        if (getWidthofListItems(Convert.ToString(lblInsCompany.Text), lblInsCompany) >= lblInsCompany.Width - 20)
                        {
                            //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                            toolTip1.SetToolTip(lblInsCompany, lblInsCompany.Text);
                        }
                        else
                        {
                            toolTip1.SetToolTip(lblInsCompany, "");
                        }
                        //toolTip1.SetToolTip(lblInsCompany, SelectedInsuranceCompany);
                    }
                    else
                    {
                        toolTip1.SetToolTip(lblInsCompany, "");
                    }
                }
                catch (Exception Ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                    Ex = null;
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
                        //toolTip1.SetToolTip(lblPaymentTray, SelectedInsuranceCompany);
                    }
                    else
                    {
                        toolTip1.SetToolTip(lblPaymentTray, "");
                    }
                }
                catch (Exception Ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                    Ex = null;
                }
            }

            private void btnClearInsurance_Click(object sender, EventArgs e)
            {
                SelectedInsuranceCompanyID = 0;
                SelectedInsuranceCompany = "";
                _nInsuranceID = 0;
            }

            private void btnSearchPatient_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Patient";

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
              //  if (oListControl != null) { oListControl.Dispose(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null; 
            }
        }

            private void btnClearPatient_Click(object sender, EventArgs e)
            {

                TxtPatient.Text = "";
                TxtPatient.Tag = "";
              //  cmbClaimNo.Items.Clear();
                cmbClaimNo.DataSource = null;
                cmbClaimNo.Items.Clear();
                cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
                cmbClaimNo.Text = "";
                PatientID = 0;
                PatientName = "";
                ClaimNo = "";
            }
        
            private void btnClearRefundPatient_Click(object sender, EventArgs e)
            {
                TxtRefundPatient.Text = "";
                TxtRefundPatient.Tag = "";
             //   CmbRefundClaim.Items.Clear();
                CmbRefundClaim.DataSource = null;
                CmbRefundClaim.Items.Clear();
                CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
                CmbRefundClaim.Text = "";
                PatientID = 0;
                PatientName = "";
                ClaimNo = "";
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
                        try
                        {
                            oRefListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oRefListControl_ItemSelectedClick);
                            oRefListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oRefListControl_ItemClosedClick);

                        }
                        catch { }
                        oRefListControl.Dispose();
                        oRefListControl = null;
                    }
                    oRefListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);
                    oRefListControl.ClinicID = _ClinicID;
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
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
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
                    getValidRefundClaimDetails();
                }
            }

            private void panel7_Paint(object sender, PaintEventArgs e)
            {

            }

            private void panel4_Paint(object sender, PaintEventArgs e)
            {

            }
        
            private void cmbClaimNo_KeyPress(object sender, KeyPressEventArgs e)
            {

                if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
                {
                    if (e.KeyChar == Convert.ToChar(45) && cmbClaimNo.Text.Contains("-") == true)
                    {
                        e.Handled = true;
                    }
                    else if (e.KeyChar == Convert.ToChar(45) && cmbClaimNo.Text.Contains("-") == false)
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

        #region "Tool Strip button events "

            private void tsb_OK_Click(object sender, EventArgs e)
            {
                _isValidResAmount = true;
                c1Reserve.FinishEditing();
                tsb_OK.Select();
                label5.Focus();
                bool _isValidClaim = false;
                _isValidClaim = getValidRefundClaimDetails();
                try
                {
                    if (_isValidate == true && _isValidResAmount == true && _isValidClaim ==true)
                    {
                        if (SavePaymentValidation())
                        {
                            if (FillEOBData())
                            {
                                if (SaveInsuranceRefund() > 0)
                                {
                                    this.Close();
                                }
                            }
                            else
                            {
                                if (c1Reserve != null && c1Reserve.Rows.Count > 1)
                                {
                                    c1Reserve.Focus();
                                    c1Reserve.Select(1, COL_REFUND, true);
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

            private void tsb_Generate_Click(object sender, EventArgs e)
            {
                try
                {

                    if (txtRefundAmount.Text != "" && txtRefundAmount.Text != null && txtRefundAmount.Text != "$0.00")
                    {
                        
                        DialogResult _dlgCloseDate = DialogResult.None;
                        _dlgCloseDate = MessageBox.Show("Do you want to save previous Refund details?", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (_dlgCloseDate == DialogResult.Yes)
                        {
                            tsb_OK_Click(null, null);
                        }
                        else if (_dlgCloseDate == DialogResult.No)
                        {
                            UsedInsuranceReserveID = 0;
                            if (lblInsCompany.Text == "")
                            {
                                //MessageBox.Show("Select Insurance Company. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //return;
                            }
                            txtRefundAmount.Text = "";
                            TxtRefundPatient.Text = "";
                            TxtRefundPatient.Tag = 0;
                         //   CmbRefundClaim.Items.Clear();
                            CmbRefundClaim.DataSource = null;
                            CmbRefundClaim.Items.Clear();
                            CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
                            CmbRefundClaim.Text = "";
                            ClearFormFields();
                            txtTo.Text = "";
                            txtTo.Tag = 0;

                            FillReserves();
                        }
                    }
                    else
                    {
                        UsedInsuranceReserveID = 0;
                        if (lblInsCompany.Text == "")
                        {
                            //MessageBox.Show("Select Insurance Company. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //return;
                        }
                        txtRefundAmount.Text = "";
                        TxtRefundPatient.Text = "";
                        TxtRefundPatient.Tag = 0;
                      //  CmbRefundClaim.Items.Clear();
                        CmbRefundClaim.DataSource = null;
                        CmbRefundClaim.Items.Clear();
                        CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
                        CmbRefundClaim.Text = "";
                        ClearFormFields();
                        //txtTo.Text = "";
                        //txtTo.Tag = 0;

                        FillReserves();                    
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex = null;
                }
            }
          
            private void tsb_Cancel_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            
        #endregion

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

                                if (Convert.ToInt64(c1Reserve.GetData(e.Row, COL_ACCOUNTID)) != UsedInsuranceReserveID && UsedInsuranceReserveID !=0)
                                {

                                    try
                                     {
                                        this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
                                        MessageBox.Show(" Only one company’s reserves may be refunded at a time.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _isValidResAmount = false;
                                        c1Reserve.SetData(e.Row, e.Col, "0.00");
                                        CalculateAmount();
                                        c1Reserve.Focus();
                                        c1Reserve.Select(e.Row, COL_REFUND);
                                        e.Cancel = true;
                                        c1Reserve.KeyActionEnter = KeyActionEnum.None;
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

                                else if (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_REFUND)) < 0)
                                {
                                    try
                                    {
                                        this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
                                        MessageBox.Show("Refund amount cannot be negative. Enter valid refund amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _isValidResAmount = false;
                                        c1Reserve.SetData(e.Row, e.Col, "0.00");
                                        CalculateAmount();
                                        c1Reserve.Focus();
                                        c1Reserve.Select(e.Row, COL_REFUND);
                                        e.Cancel = true;
                                        c1Reserve.KeyActionEnter = KeyActionEnum.None;
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
                                else if (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_REFUND)) > Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_AVAILABLE)))
                                {
                                    try
                                    {
                                        this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
                                        MessageBox.Show("Refund amount cannot be more than available amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _isValidResAmount = false;
                                        c1Reserve.SetData(e.Row, e.Col, "0.00");
                                        CalculateAmount();
                                        c1Reserve.Focus();
                                        c1Reserve.Select(e.Row,COL_REFUND);
                                        e.Cancel = true;
                                        c1Reserve.KeyActionEnter = KeyActionEnum.None;
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
                                    this.txtTo.Text = Convert.ToString(c1Reserve.GetData(e.Row, COL_INSURANCECOMPANY));
                                    this.txtTo.Tag = Convert.ToInt64(c1Reserve.GetData(e.Row, COL_ACCOUNTID));
                                    this.lblInsCompany.Text = Convert.ToString(c1Reserve.GetData(e.Row, COL_INSURANCECOMPANY));
                                    this.lblInsCompany.Tag = Convert.ToInt64(c1Reserve.GetData(e.Row, COL_ACCOUNTID));
                                    this.InsuranceID = Convert.ToInt64(c1Reserve.GetData(e.Row, COL_ACCOUNTID));
                                    
                                    CalculateAmount();
                                    AddRefundPatient();
                                    c1Reserve.KeyActionEnter = KeyActionEnum.MoveDown;
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

            private void c1Reserve_Click(object sender, EventArgs e)
            {

            }
        #endregion

        #region "Close date validation"

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
                                MessageBox.Show("Enter valid close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isValidate = false;
                                e.Cancel = true;

                            }
                            //else if (mskCloseDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskCloseDate.Name)
                            //{
                            //    if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                            //    {
                            //        MessageBox.Show("Close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        _isValidate = false;
                            //        e.Cancel = true;
                            //    }
                            //}
                        }
                        //else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        //{
                        //    MessageBox.Show("Enter close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    _isValidate = false;
                        //    e.Cancel = true;
                        //}
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
                c1Payment.SetData(0, COL_EOBID,"EOBID");
                c1Payment.SetData(0, COL_EOBDTLID,"EOBDetailID");
                c1Payment.SetData(0, COL_EOBPAYMENTDTLID, "EOBPaymentDetailID");
                c1Payment.SetData(0, COL_BLTRANSACTIONID,"BillingTransactioID");
                c1Payment.SetData(0, COL_BLTRANDTLID, "BillingTransactioDetailID");
                c1Payment.SetData(0, COL_BLTRANLINEID, "BillingTransactioLineID");
                c1Payment.SetData(0, COL_DOSFROM, "DOSFrom");
                c1Payment.SetData(0, COL_DOSTO,"DOSTo");
                c1Payment.SetData(0, COL_PATIENTID,"PatientID");
                c1Payment.SetData(0, COL_SOURCE,"Source");//Patient or Insurance Name

                c1Payment.SetData(0, COL_ORIGINALPAYMENT,"Original Payment");//Check Number,Date,Amount
                c1Payment.SetData(0, COL_TORESERVES,"To Reserves");//Amount for reserve
                c1Payment.SetData(0, COL_TYPE,"Type");//Copay,Advance,Other
                c1Payment.SetData(0, COL_NOTE,"Note");//Note

                c1Payment.SetData(0, COL_AVAILABLE,"Available");//Available amount
                c1Payment.SetData(0, COL_USERESERVE, "Used");//Used Reserve
                c1Payment.SetData(0, COL_REFUND,"Refund");//Current amount to use from avaiable amount

                c1Payment.SetData(0, COL_PAYMODE,"Payment Mode");
                c1Payment.SetData(0, COL_REFEOBPAYID,"Ref.EOBID");
                c1Payment.SetData(0, COL_REFEOBPAYDTLID,"Ref.EOBDetailID");
                c1Payment.SetData(0, COL_ACCOUNTID,"AccountID");
                c1Payment.SetData(0, COL_ACCOUNTTYPE,"Account Type");
                c1Payment.SetData(0, COL_MSTACCOUNTID,"Mst.AccountID");
                c1Payment.SetData(0, COL_MSTACCOUNTTYPE, "Mst.AccountType");
                c1Payment.SetData(0, COL_RES_EOBPAYID,"ReserveRefPayID");
                c1Payment.SetData(0, COL_RES_EOBPAYDTLID,"ReserveRefPayDtlID");
                c1Payment.SetData(0, COL_PAYMENTCLOSEDATE, "Close Date");
                c1Payment.SetData(0, COL_PAYMENTMODE, "sPaymentMode");
                c1Payment.SetData(0, COL_PAYMENTMODENO, "sPaymentNo");

                c1Payment.SetData(0, COL_ASSO_PATIENT, "Patient");
                c1Payment.SetData(0, COL_ASSO_CLAIM, "Claim #");
                c1Payment.SetData(0, COL_INSURANCECOMPANY, "Ins. Company");

                c1Payment.SetData(0, COL_ASSO_MSTTRANSACTIONID, "MSTTransactionID");
                c1Payment.SetData(0, COL_ASSO_TRANSACTIONID, "TransactionID");
                c1Payment.SetData(0, COL_ASSO_PATIENTID, "RefundPatientID");

                #endregion

                #region " Show/Hide "

                c1Payment.Cols[COL_SOURCE].Visible = true;
                c1Payment.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1Payment.Cols[COL_TORESERVES].Visible = true;
                c1Payment.Cols[COL_TYPE].Visible = true;
                c1Payment.Cols[COL_NOTE].Visible = true;
                c1Payment.Cols[COL_AVAILABLE].Visible = true;
                c1Payment.Cols[COL_REFUND].Visible = true;

                c1Payment.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANSACTIONID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANLINEID].Visible = false;// 0;
                c1Payment.Cols[COL_DOSFROM].Visible = false;// 50;
                c1Payment.Cols[COL_DOSTO].Visible = false;// 0;
                c1Payment.Cols[COL_PATIENTID].Visible = false;// 0;
                c1Payment.Cols[COL_SOURCE].Visible = true;// 100;
                c1Payment.Cols[COL_ORIGINALPAYMENT].Visible = true;// 100;
                c1Payment.Cols[COL_TORESERVES].Visible = true;// 100;
                c1Payment.Cols[COL_TYPE].Visible = false;// 100;
                c1Payment.Cols[COL_NOTE].Visible = true;// 100;
                c1Payment.Cols[COL_AVAILABLE].Visible = true;// 100;
                c1Payment.Cols[COL_REFUND].Visible = true;// 100;
                c1Payment.Cols[COL_PAYMODE].Visible = false;// 100;
                c1Payment.Cols[COL_REFEOBPAYID].Visible = false;// 0;
                c1Payment.Cols[COL_REFEOBPAYDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_ACCOUNTID].Visible = false;// 0;
                c1Payment.Cols[COL_ACCOUNTTYPE].Visible = false;// 0;
                c1Payment.Cols[COL_MSTACCOUNTID].Visible = false;// 0;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].Visible = false;// 0;
                c1Payment.Cols[COL_USERESERVE].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYID].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].Visible = false;
                c1Payment.Cols[COL_PAYMENTCLOSEDATE].Visible = true;
                c1Payment.Cols[COL_PAYMENTMODE].Visible = false;
                c1Payment.Cols[COL_PAYMENTMODENO].Visible = false;

                c1Payment.Cols[COL_ASSO_PATIENT].Visible = true;
                c1Payment.Cols[COL_ASSO_CLAIM].Visible = true;
                c1Payment.Cols[COL_INSURANCECOMPANY].Visible = true;

                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].Visible = false;
                c1Payment.Cols[COL_ASSO_TRANSACTIONID].Visible = false;
                c1Payment.Cols[COL_ASSO_PATIENTID].Visible = false;

                #endregion

                #region " Width "

                bool _designWidth = false;
                
                if (_designWidth == false)
                {

                    c1Payment.Cols[COL_EOBPAYMENTID].Width = 0;
                    c1Payment.Cols[COL_EOBID].Width = 0;
                    c1Payment.Cols[COL_EOBDTLID].Width = 0;
                    c1Payment.Cols[COL_EOBPAYMENTDTLID].Width = 0;
                    c1Payment.Cols[COL_BLTRANSACTIONID].Width = 0;
                    c1Payment.Cols[COL_BLTRANDTLID].Width = 0;
                    c1Payment.Cols[COL_BLTRANLINEID].Width = 0;
                    c1Payment.Cols[COL_DOSFROM].Width = 50;
                    c1Payment.Cols[COL_DOSTO].Width = 0;
                    c1Payment.Cols[COL_PATIENTID].Width = 0;
                    c1Payment.Cols[COL_SOURCE].Width = 0;
                    c1Payment.Cols[COL_ORIGINALPAYMENT].Width = 265;
                    c1Payment.Cols[COL_TORESERVES].Width = 80;
                    c1Payment.Cols[COL_TYPE].Width = 0;
                    c1Payment.Cols[COL_NOTE].Width = 220;
                    c1Payment.Cols[COL_AVAILABLE].Width = 75;
                    c1Payment.Cols[COL_REFUND].Width = 75;
                    c1Payment.Cols[COL_PAYMODE].Width = 100;
                    c1Payment.Cols[COL_REFEOBPAYID].Width = 0;
                    c1Payment.Cols[COL_REFEOBPAYDTLID].Width = 0;
                    c1Payment.Cols[COL_ACCOUNTID].Width = 0;
                    c1Payment.Cols[COL_ACCOUNTTYPE].Width = 0;
                    c1Payment.Cols[COL_MSTACCOUNTID].Width = 0;
                    c1Payment.Cols[COL_MSTACCOUNTTYPE].Width = 0;
                    c1Payment.Cols[COL_USERESERVE].Width = 0;
                    c1Payment.Cols[COL_RES_EOBPAYID].Width = 0;
                    c1Payment.Cols[COL_RES_EOBPAYDTLID].Width = 0;
                    c1Payment.Cols[COL_PAYMENTCLOSEDATE].Width = 80;
                    c1Payment.Cols[COL_PAYMENTMODE].Width = 0;
                    c1Payment.Cols[COL_PAYMENTMODENO].Width = 0;

                    c1Payment.Cols[COL_ASSO_PATIENT].Width = 160;
                    c1Payment.Cols[COL_ASSO_CLAIM].Width = 60;
                    c1Payment.Cols[COL_INSURANCECOMPANY].Width = 130;

                    c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].Width = 0;
                    c1Payment.Cols[COL_ASSO_TRANSACTIONID].Width = 0;
                    c1Payment.Cols[COL_ASSO_PATIENTID].Width = 0;

                }

                #endregion

                #region " Data Type "

                c1Payment.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANLINEID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_DOSFROM].DataType = typeof(System.String);
                c1Payment.Cols[COL_DOSTO].DataType = typeof(System.String);
                c1Payment.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_SOURCE].DataType = typeof(System.String);
                c1Payment.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
                c1Payment.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_TYPE].DataType = typeof(System.String);
                c1Payment.Cols[COL_NOTE].DataType = typeof(System.String);
                c1Payment.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_REFUND].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_REFEOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_REFEOBPAYDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ACCOUNTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_MSTACCOUNTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_MSTACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_RES_EOBPAYDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAYMENTCLOSEDATE].DataType = typeof(System.String);
                c1Payment.Cols[COL_PAYMENTMODE].DataType = typeof(System.String);
                c1Payment.Cols[COL_PAYMENTMODENO].DataType = typeof(System.String);

                c1Payment.Cols[COL_ASSO_PATIENT].DataType = typeof(System.String);
                c1Payment.Cols[COL_ASSO_CLAIM].DataType = typeof(System.String);
                c1Payment.Cols[COL_INSURANCECOMPANY].DataType = typeof(System.String);

                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ASSO_TRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ASSO_PATIENTID].DataType = typeof(System.Int64);
                #endregion

                #region " Alignment "

                c1Payment.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_BLTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BLTRANDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BLTRANLINEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_DOSFROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_DOSTO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
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
                c1Payment.Cols[COL_REFEOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_MSTACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_PAYMENTCLOSEDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAYMENTMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_PAYMENTMODENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                c1Payment.Cols[COL_ASSO_PATIENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_CLAIM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_INSURANCECOMPANY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

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
                c1Payment.Cols[COL_EOBID].AllowEditing = false;//0;
                c1Payment.Cols[COL_EOBDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANSACTIONID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANLINEID].AllowEditing = false;//0;
                c1Payment.Cols[COL_DOSFROM].AllowEditing = false;//50;
                c1Payment.Cols[COL_DOSTO].AllowEditing = false;//0;
                c1Payment.Cols[COL_PATIENTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_SOURCE].AllowEditing = false;//100;
                c1Payment.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
                c1Payment.Cols[COL_TORESERVES].AllowEditing = false;//100;
                c1Payment.Cols[COL_TYPE].AllowEditing = false;//100;
                c1Payment.Cols[COL_NOTE].AllowEditing = false;//100;
                c1Payment.Cols[COL_AVAILABLE].AllowEditing = false;//100;
                c1Payment.Cols[COL_REFUND].AllowEditing = true;//100;
                c1Payment.Cols[COL_USERESERVE].AllowEditing = false;//100;
                c1Payment.Cols[COL_PAYMODE].AllowEditing = false;//100;
                c1Payment.Cols[COL_REFEOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_REFEOBPAYDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ACCOUNTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ACCOUNTTYPE].AllowEditing = false;//0;
                c1Payment.Cols[COL_MSTACCOUNTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].AllowEditing = false;//0;
                c1Payment.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_PAYMENTCLOSEDATE].AllowEditing = false;//0;
                c1Payment.Cols[COL_PAYMENTMODE].AllowEditing = false;//0;
                c1Payment.Cols[COL_PAYMENTMODENO].AllowEditing = false;//0;

                c1Payment.Cols[COL_ASSO_PATIENT].AllowEditing = false;
                c1Payment.Cols[COL_ASSO_CLAIM].AllowEditing = false;
                c1Payment.Cols[COL_INSURANCECOMPANY].AllowEditing = false;

                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ASSO_TRANSACTIONID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ASSO_PATIENTID].AllowEditing = false;//0;
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

        #region " Private & Public Methods "

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
                    break;
                }
            }

        }

        //For Payment Tray
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
            }
            oDB.Disconnect();
            oDB.Dispose();
            oDB = null;
            return result;
        }

        private void FillReserves()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtReserves = new DataTable();
            try
            {
                DesignPaymentGrid(c1Reserve);
                _IsFormLoading = true;
                oDB.Connect(false);
                Int64 _patientId = 0;
                if (Convert.ToString(TxtPatient.Tag) != "") { _patientId = Convert.ToInt64(TxtPatient.Tag); }

                if (_IsFormLoading == true && SelectedInsuranceCompanyID != 0 || _patientId != 0 || cmbClaimNo.Text != "")
                {

                    string _strQuery = " Select * from view_InsuranceCompanyReserves where nEOBPaymentID <> 0 ";

                    if (SelectedInsuranceCompanyID != 0)
                    {
                        _strQuery = _strQuery + "and  nPayerID = " + SelectedInsuranceCompanyID + "";
                    }
                    if (_patientId != 0)
                    {
                        _strQuery = _strQuery + " and  AssociationPatientID = " + _patientId + "";
                    }
                    if (cmbClaimNo.Text != "")
                    {
                        _strQuery = _strQuery + " and  AssociationClaim ='" + cmbClaimNo.Text + "'";
                    }
                    oDB.Retrive_Query(_strQuery, out _dtReserves);

                    oDB.Disconnect();

                    if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                    {
                        int _rowIndex = 0;
                        //SLR: Logic to be checked on 4/2/2014
                        for (int i = 0; i <= _dtReserves.Rows.Count-1; i++)
                        {
                             if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                            {
                                for (int cntr = 0; cntr <= EOBInsurancePaymentMasterLines.Count - 1; cntr++)
                                {
                                    //if (Convert.ToInt64(EOBInsurancePaymentMasterLines[cntr].EOBPaymentID) == 0
                                    //    && Convert.ToInt64(EOBInsurancePaymentMasterLines[cntr].EOBPaymentDetailID) == 0)
                                    {
                                        if (
                                            //EOBInsurancePaymentMasterLines[cntr].InsuranceCompanyID == Convert.ToInt64(_dtReserves.Rows[i]["nAccountID"])
                                           //&& 
                                            EOBInsurancePaymentMasterLines[cntr].ReserveEOBPaymentID == Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"])
                                           && EOBInsurancePaymentMasterLines[cntr].ReserveEOBPaymentDetailID == Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"])
                                           && EOBInsurancePaymentMasterLines[cntr].PaymentType == EOBPaymentType.InsuraceReserverd
                                           && EOBInsurancePaymentMasterLines[cntr].PaymentSubType == EOBPaymentSubType.Reserved
                                           && EOBInsurancePaymentMasterLines[cntr].PaySign == EOBPaymentSign.Payment_Credit)
                                        {

                                            _dtReserves.Rows[i]["AvailableReserve"] = Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]) - (Convert.ToDecimal(EOBInsurancePaymentMasterLines[cntr].Amount )- Convert.ToDecimal(EOBInsurancePaymentMasterLines[cntr].DBReserveAmount));//Available amount
                                        }
                                    }

                                }
                            }
                             if (_dtReserves.Rows[i]["AvailableReserve"] != null && Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]) != 0 && Convert.ToString(_dtReserves.Rows[i]["AvailableReserve"]).Trim() != "")
                             {
                             }
                             else
                             {
                                 _dtReserves.Rows.RemoveAt(i);
                                 i--;
                             }
                        }
                          for (int i = 0; i < _dtReserves.Rows.Count; i++)
                          {
                            #region " Set Data "

                            _rowIndex = c1Reserve.Rows.Add().Index;

                            c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
                            c1Reserve.SetData(_rowIndex, COL_EOBID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBID"]));
                            c1Reserve.SetData(_rowIndex, COL_EOBDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBDtlID"]));
                            c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
                            c1Reserve.SetData(_rowIndex, COL_BLTRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionID"]));
                            c1Reserve.SetData(_rowIndex, COL_BLTRANDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionDetailID"]));
                            c1Reserve.SetData(_rowIndex, COL_BLTRANLINEID, Convert.ToString(_dtReserves.Rows[i]["nBillingTransactionLineNo"]));
                            c1Reserve.SetData(_rowIndex, COL_DOSFROM, Convert.ToInt64(_dtReserves.Rows[i]["nDOSFrom"]));
                            c1Reserve.SetData(_rowIndex, COL_DOSTO, Convert.ToString(_dtReserves.Rows[i]["nDOSTo"]));
                            c1Reserve.SetData(_rowIndex, COL_PATIENTID, Convert.ToString(_dtReserves.Rows[i]["nPatientID"]));
                            c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(_dtReserves.Rows[i]["PatientName"]));//Patient or Insurance Name

                            string _originalPayment = "";
                            _originalPayment = ((EOBPaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])).ToString() + "# " + Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dtReserves.Rows[i]["nCheckDate"])).ToString("MM/dd/yyyy") + " $ " + Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
                            c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                            c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));
                            c1Reserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                            c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note
                            c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(_dtReserves.Rows[i]["UsedReserve"]));//Used amount
                            c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount


                            //if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                            //{
                            //    for (int cntr = 0; cntr <= EOBInsurancePaymentMasterLines.Count - 1; cntr++)
                            //    {
                            //        if (EOBInsurancePaymentMasterLines[cntr].InsuranceCompanyID == Convert.ToInt64(_dtReserves.Rows[i]["nAccountID"])
                            //          && EOBInsurancePaymentMasterLines[cntr].ReserveEOBPaymentID == Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"])
                            //          && EOBInsurancePaymentMasterLines[cntr].ReserveEOBPaymentDetailID == Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]))
                            //          //   && EOBInsurancePaymentMasterLines[cntr].ReserveEOBPaymentID == Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentID"])
                            //          //&& EOBInsurancePaymentMasterLines[cntr].ReserveEOBPaymentDetailID == Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentDetailID"]))
                            //        {
                            //            c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]) - Convert.ToDecimal(EOBInsurancePaymentMasterLines[cntr].Amount));//Available amount
                            //        }
                            //        //else
                            //        //{
                            //        //    c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
                            //        //}
                            //    }
                            //}
                            //else
                            //{
                            //    c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
                            //}
                            c1Reserve.SetData(_rowIndex, COL_REFUND, 0);//Current amount to use from avaiable amount

                            c1Reserve.SetData(_rowIndex, COL_PAYMODE, ((EOBPaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])));
                            c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentID"]));
                            c1Reserve.SetData(_rowIndex, COL_REFEOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentDetailID"]));
                            c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
                            c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
                            c1Reserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nAccountID"]));
                            c1Reserve.SetData(_rowIndex, COL_ACCOUNTTYPE, Convert.ToInt32(_dtReserves.Rows[i]["nAccountType"]));
                            c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nMSTAccountID"]));
                            c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTTYPE, Convert.ToInt32(_dtReserves.Rows[i]["nMSTAccountType"]));

                            c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentID"]));
                            c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentDetailID"]));
                            c1Reserve.SetData(_rowIndex, COL_PAYMENTCLOSEDATE, gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_dtReserves.Rows[i]["nCloseDate"])));
                            c1Reserve.SetData(_rowIndex, COL_PAYMENTMODE, Convert.ToInt64(_dtReserves.Rows[i]["nPayMode"]));
                            c1Reserve.SetData(_rowIndex, COL_PAYMENTMODENO, Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]));

                            c1Reserve.SetData(_rowIndex, COL_INSURANCECOMPANY, Convert.ToString(_dtReserves.Rows[i]["InsuarnceCompanyName"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENT, Convert.ToString(_dtReserves.Rows[i]["AssociationPatientName"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_CLAIM, Convert.ToString(_dtReserves.Rows[i]["AssociationClaim"]));

                            c1Reserve.SetData(_rowIndex, COL_ASSO_TRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["AssociationnTransactionID"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["AssociationMSTTransactionID"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(_dtReserves.Rows[i]["AssociationPatientID"]));

                            #region " Set Styles "

                            c1Reserve.SetCellStyle(_rowIndex, COL_REFUND, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                            #endregion " Set Styles "


                            #endregion
                        }
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

        private bool  SavePaymentValidation()
        {
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            MaskedTextBox mskDate = mskCloseDate;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskDate.Text;
            mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            try
            {
                if (lblInsCompany.Text.Trim() == "")
                {
                    MessageBox.Show("Select Insurance Company.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSearchCompany.Focus();
                    return false;
                }

                if (c1Reserve.Rows.Count <= 1)
                {
                    MessageBox.Show("Cannot create an Insurance Refund for " + lblInsCompany.Text.Trim() + " because there are no available reserves for " + lblInsCompany.Text.Trim() + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSearchCompany.Focus();
                    return false;
                }
               
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
                //else if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                //{
                //    MessageBox.Show("Close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    mskCloseDate.Select();
                //    mskCloseDate.Focus();                   
                //    return false;
                //}


                else if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();                   
                    return false;
                }



                else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(7))
                {
                    MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskCloseDate.Focus();
                    mskCloseDate.Select();
                    return false;
                }
     

                if (lblPaymentTray.Tag == null || lblPaymentTray.Tag.ToString().Trim() == "" || Convert.ToInt64(lblPaymentTray.Tag) <= 0)
                {
                    MessageBox.Show("Select payment tray.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSelectPaymentTray.Select();
                    btnSelectPaymentTray.Focus();
                    return false;
                }

                if (txtTo.Text.Trim() == "")
                {
                    MessageBox.Show("Enter refund to name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (c1Reserve.Rows.Count > 1)
                    {
                        c1Reserve.Focus();
                        c1Reserve.Select(1, COL_REFUND);
                    }
                    return false;
                }

                EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.Check;
                if (_EOBPaymentMode == EOBPaymentMode.None)
                {
                    MessageBox.Show("Select refund mode.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPayMode.Select();
                    cmbPayMode.Focus();
                    return false;
                }
                else if (_EOBPaymentMode == EOBPaymentMode.CreditCard)
                {

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
                else if (_EOBPaymentMode == EOBPaymentMode.Check)
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
                else if (_EOBPaymentMode == EOBPaymentMode.EFT)
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


                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
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
                       
                    }
                }
                if (c1Reserve.Rows.Count > 1)
                {
                    StringBuilder strBldrChkNos = new StringBuilder("");
                    for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
                    {
                        if (c1Reserve.GetData(i, COL_REFUND) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFUND)).ToString().Trim() != "")
                        {
                            if (Convert.ToDecimal(c1Reserve.GetData(i, COL_REFUND)) > 0)
                            {
                                if (Convert.ToDateTime(c1Reserve.GetData(i, COL_PAYMENTCLOSEDATE)).Date > Convert.ToDateTime(mskCloseDate.Text.Trim()).Date)
                                {
                                    if (strBldrChkNos.Length == 0)
                                    {
                                        strBldrChkNos.Append((EOBPaymentMode)Convert.ToInt64(c1Reserve.GetData(i, COL_PAYMENTMODE)) + "# " + Convert.ToString(c1Reserve.GetData(i, COL_PAYMENTMODENO)));
                                    }
                                    else
                                    {
                                        char chr=',';
                                        string[] strCheckNos;
                                        strCheckNos = strBldrChkNos.ToString().Split(chr);
                                        if (strCheckNos.Length <= 9)
                                        {
                                            strBldrChkNos.Append(", " + (EOBPaymentMode)Convert.ToInt64(c1Reserve.GetData(i, COL_PAYMENTMODE)) + "# " + Convert.ToString(c1Reserve.GetData(i, COL_PAYMENTMODENO)));
                                        }
                                        else if (strCheckNos.Length > 9)
                                        {
                                            strBldrChkNos.Append("...");
                                            break;
                                        }
                                    }
                                    //MessageBox.Show("Refund Close Date for ["+ (EOBPaymentMode)Convert.ToInt64(c1Reserve.GetData(i, COL_PAYMENTMODE)) + "# " + Convert.ToString(c1Reserve.GetData(i, COL_PAYMENTMODENO)) + "] cannot be less than Payment Close Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //return false;
                                }
                            }
                        }
                    }
                    if (strBldrChkNos.Length > 0)
                    {
                        MessageBox.Show("Refund Close Date for [" + strBldrChkNos + "] cannot be less than Payment Close Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
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

        //Add patient & Claim number
        private void AddRefundPatient()
         {
            try
            {
                Int64 _nLastSelectedPatId = 0;
                string _nLastSelectedClaim = "";
                TxtRefundPatient.Text = string.Empty;
                TxtRefundPatient.Tag = string.Empty;
              
                CmbRefundClaim.DataSource = null;
                CmbRefundClaim.Items.Clear();
                CmbRefundClaim.Text = "";
                CmbRefundClaim.Tag = "";
                if (Convert.ToInt64(c1Reserve.GetData(c1Reserve.RowSel, COL_REFUND)) == 0)
                {
                    UsedInsuranceReserveID = 0;
                    CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;

                }
                for (int i = 1; c1Reserve.Rows.Count > i; i++)
                {
                    
                    if (Convert.ToInt64(c1Reserve.GetData(i, COL_REFUND)) > 0)
                    {

                        if (Convert.ToInt64(c1Reserve.GetData(i, COL_ASSO_PATIENTID)) == _nLastSelectedPatId || _nLastSelectedPatId == 0)
                        {
                            TxtRefundPatient.Text = Convert.ToString(c1Reserve.GetData(i, COL_ASSO_PATIENT));
                            TxtRefundPatient.Tag = Convert.ToString(c1Reserve.GetData(i, COL_ASSO_PATIENTID));
                            getPatientClaimNos(Convert.ToInt64(TxtRefundPatient.Tag), "refund");

                        }
                        else
                        {
                            TxtRefundPatient.Text ="";
                            TxtRefundPatient.Tag = "";
                        }

                        if (Convert.ToString(c1Reserve.GetData(i, COL_ASSO_CLAIM)) == _nLastSelectedClaim || _nLastSelectedPatId == 0)
                        {
                            CmbRefundClaim.Text = Convert.ToString(c1Reserve.GetData(i, COL_ASSO_CLAIM));
                            CmbRefundClaim.Tag = CmbRefundClaim.SelectedValue;
                            //CmbRefundClaim.Text = Convert.ToString(c1Reserve.GetData(i, COL_ASSO_CLAIM));
                            //CmbRefundClaim.ValueMember = Convert.ToString(c1Reserve.GetData(i, COL_ASSO_MSTTRANSACTIONID)) + '-' + Convert.ToString(c1Reserve.GetData(i, COL_ASSO_TRANSACTIONID)); //Convert.ToString(c1Reserve.GetData(i, COL_CLAIM));
                        }
                        else
                        {
                            if (Convert.ToInt64(c1Reserve.GetData(i, COL_ASSO_PATIENTID)) == _nLastSelectedPatId)
                            {
                                CmbRefundClaim.Text = "";
                                CmbRefundClaim.Tag = 0;
                            }
                            else
                            {
                                
                                CmbRefundClaim.DataSource = null;
                                CmbRefundClaim.Items.Clear();
                                CmbRefundClaim.Text = "";
                                CmbRefundClaim.Tag = "";
                                CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
                            }
                        }

                        if (_nLastSelectedPatId == 0 && Convert.ToInt64(c1Reserve.GetData(i, COL_ASSO_PATIENTID)) != 0 )//|| _nLastSelectedPatId != Convert.ToInt64(c1Reserve.GetData(i, COL_ASSO_PATIENTID)))
                        {
                            _nLastSelectedPatId = Convert.ToInt64(c1Reserve.GetData(i, COL_ASSO_PATIENTID));
                        }

                        if (_nLastSelectedClaim == "" && Convert.ToString(c1Reserve.GetData(i, COL_ASSO_CLAIM)) != "")
                        {
                            _nLastSelectedClaim = Convert.ToString(c1Reserve.GetData(i, COL_ASSO_CLAIM));
                        }

                        UsedInsuranceReserveID = Convert.ToInt64(c1Reserve.GetData(c1Reserve.RowSel, COL_ACCOUNTID));
                    }


                }

               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }

        private void SelectInsuranceCompany()
        {
            try
            {
                if (txtRefundAmount.Text != "" && txtRefundAmount.Text !=null)
                {
                    DialogResult _dlgCloseDate = DialogResult.None;
                    _dlgCloseDate = MessageBox.Show("Do you want to save previous Refund details?", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (_dlgCloseDate == DialogResult.Yes)
                    {
                        tsb_OK_Click(null, null);
                    }
                    else if (_dlgCloseDate == DialogResult.No)
                    {

                        using (frmSearchInsuranceCompany ofrmSearchInsuranceCompany = new frmSearchInsuranceCompany())
                        {
                            ofrmSearchInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                            ofrmSearchInsuranceCompany.ShowDialog(this);

                            if (ofrmSearchInsuranceCompany.FrmDlgRst == DialogResult.OK)
                            {
                                this.SelectedInsuranceCompanyID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                                this.SelectedInsuranceCompany = ofrmSearchInsuranceCompany.InsuranceCompanyName;
                                this.txtTo.Text = ofrmSearchInsuranceCompany.InsuranceCompanyName;
                                _nInsuranceID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                                this.TxtRefundPatient.Text = "";
                                this.TxtRefundPatient.Tag = 0;
                                this.CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
                              
                                this.CmbRefundClaim.DataSource = null;
                                this.CmbRefundClaim.Items.Clear();
                                this.CmbRefundClaim.Text = "";
                                this.cmbClaimNo.ValueMember = "";
                                ClearFormFields();
                               // c1Reserve.Clear();
                                c1Reserve.DataSource = null;
                                DesignPaymentGrid(c1Reserve);
                                // FillReserves();
                            }
                        }
                    }
                }
                else
                {
                    using (frmSearchInsuranceCompany ofrmSearchInsuranceCompany = new frmSearchInsuranceCompany())
                    {
                        ofrmSearchInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                        ofrmSearchInsuranceCompany.ShowDialog(this);

                        if (ofrmSearchInsuranceCompany.FrmDlgRst == DialogResult.OK)
                        {
                            this.SelectedInsuranceCompanyID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                            this.SelectedInsuranceCompany = ofrmSearchInsuranceCompany.InsuranceCompanyName;
                            this.txtTo.Text = ofrmSearchInsuranceCompany.InsuranceCompanyName;
                            _nInsuranceID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                            this.TxtRefundPatient.Text = "";
                            this.TxtRefundPatient.Tag = 0;
                            this.CmbRefundClaim.Text = "";
                            this.cmbClaimNo.ValueMember = "";
                            ClearFormFields();
                          //  c1Reserve.Clear();
                            c1Reserve.DataSource = null;
                            DesignPaymentGrid(c1Reserve);
                            // FillReserves();
                        }
                    }

                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void ClearFormFields()
        {
            //txtTo.Text = string.Empty;
            //mskrefunddate.Text = string.Empty;
            txtRefundAmount.Text = string.Empty;
            txtCheckNumber.Text = string.Empty;
            //mskCheckDate.Text = string.Empty;
            txtNotes.Text = string.Empty;
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

        private void getPatientClaimNos(Int64 nPatientID, string ClaimReff)
        {
            DataTable _dtClaimNo = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                string _strSql = "";
                _strSql = "SELECT CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionMasterID )+ '-' + CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionID) AS ID, "
                         + " dbo.GetSubClaimNumber(BL_Transaction_Claim_MST.nClaimNo,BL_Transaction_Claim_MST.nSubClaimNo ,BL_Transaction_Claim_MST.sMainClaimNo,5) as Claim  "
                         + " FROM BL_Transaction_Claim_MST  WITH (NOLOCK) WHERE (LEFT(nSubClaimNo,1)<> '-')  AND nPatientID = " + nPatientID + " ORDER BY dtCreateDate DESC";

                oDB.Connect(false);
                oDB.Retrive_Query(_strSql, out _dtClaimNo);
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }

                if (ClaimReff != "refund")
                {
                    if (_dtClaimNo != null && _dtClaimNo.Rows.Count > 0)
                    {
                        //if (_dtClaimNo.Rows.Count > 1)
                        //{
                            DataRow dr = _dtClaimNo.NewRow();
                            dr["Claim"] = "";
                            dr["ID"] = 0;
                            _dtClaimNo.Rows.InsertAt(dr, 0);


                            cmbClaimNo.DropDownStyle = ComboBoxStyle.DropDownList;
                            cmbClaimNo.DataSource = _dtClaimNo;
                            cmbClaimNo.DisplayMember = "Claim";
                            cmbClaimNo.ValueMember = "ID";

                        //}
                        //else
                        //{

                        //    cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
                        //    cmbClaimNo.DataSource = _dtClaimNo;
                        //    cmbClaimNo.DisplayMember = "Claim";
                        //    cmbClaimNo.ValueMember = "ID";
                        //}
                    }
                    else
                    {
                        cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
                    }
                }
                else  //For Refund ClaimNo Dropdown
                {
                    if (_dtClaimNo != null && _dtClaimNo.Rows.Count > 0)
                    {
                        //if (_dtClaimNo.Rows.Count > 1)
                        //{
                            DataRow dr = _dtClaimNo.NewRow();
                            dr["Claim"] = "";
                            dr["ID"] = 0;
                            _dtClaimNo.Rows.InsertAt(dr, 0);

                            CmbRefundClaim.DropDownStyle = ComboBoxStyle.DropDownList;
                            CmbRefundClaim.DataSource = _dtClaimNo;
                            CmbRefundClaim.DisplayMember = "Claim";
                            CmbRefundClaim.ValueMember = "ID";

                        //}
                        //else
                        //{

                        //    CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
                        //    CmbRefundClaim.DataSource = _dtClaimNo;
                        //    CmbRefundClaim.DisplayMember = "Claim";
                        //}
                    }
                    else
                    {
                        CmbRefundClaim.DropDownStyle = ComboBoxStyle.Simple;
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
            }
        }

        private DataTable getInsCompany(Int64 nInsuranceCompanyID)
        {
            DataTable dtInsCompany = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                if (nInsuranceCompanyID != 0)
                {

                    string _strSql = "";
                    _strSql = "SELECT ISNULL(sDescription,'') AS sInsName FROM Contacts_InsuranceCompany_MST  WITH (NOLOCK) " +
                              " WHERE nID = " + nInsuranceCompanyID + "";

                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSql, out dtInsCompany);
                    oDB.Disconnect();
                    if (oDB != null) { oDB.Dispose(); }
                }
                return dtInsCompany;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        private bool getValidClaimDetails()
        {
          gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");

            try
            {
                if (cmbClaimNo.Text.StartsWith("-"))
                {
                    MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtPatient.Text = "";
                    cmbClaimNo.Text = "";
                    return false;
                }
                else
                {
                    this.MSTTransactionID = 0;
                    this.TransactionID = 0;
                    this.PatientID = 0;
                    this.PatientName = ""; ;
                    ClaimNo = "";

                    ogloBilling.ClaimNumber = cmbClaimNo.Text;
                    ogloBilling.SetClaimNumbers();

                    if (ogloBilling.MainClaimNumber != 0 || ogloBilling.SubClaimNumber != "")
                    {
                        ClaimDetails = new SplitClaimDetails(ogloBilling.MainClaimNumber, ogloBilling.SubClaimNumber);

                        if (!ClaimDetails.IsClaimExist)
                        {
                            MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TxtPatient.Text = "";
                            return false;
                        }
                        else
                        {
                            //ogloBilling.ClaimNumber = cmbClaimNo.Text;
                            //ogloBilling.SetClaimNumbers();
                            ////ogloBilling.is
                            //ClaimDetails = new SplitClaimDetails(ogloBilling.MainClaimNumber, ogloBilling.SubClaimNumber);

                            DataTable dtTransactionID = new DataTable();
                            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            string _strSql = "";

                            if (ogloBilling.SubClaimNumber == "")
                            {
                                _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                                    + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                                    + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                                    + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                                    + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + "";
                            }
                            else
                            {
                                _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                                        + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                                        + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                                        + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                                        + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + " AND nSubClaimNo = " + ogloBilling.SubClaimNumber;
                            }
                            oDB.Connect(false);
                            oDB.Retrive_Query(_strSql, out dtTransactionID);
                            oDB.Disconnect();
                            if (oDB != null) { oDB.Dispose(); }

                            if (ClaimDetails.TransactionMasterID == 0)
                            {
                                this.MSTTransactionID = Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionMasterID"]);
                                this.TransactionID = Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionID"]);
                                ClaimNo = cmbClaimNo.Text;
                                cmbClaimNo.Tag = Convert.ToString(MSTTransactionID) + '-' + Convert.ToString(TransactionID);
                            }
                            else
                            {
                                this.MSTTransactionID = ClaimDetails.TransactionMasterID;
                                this.TransactionID = ClaimDetails.TransactionID;
                                ClaimNo = Convert.ToString(ClaimDetails.ClaimNo);
                                cmbClaimNo.Tag = Convert.ToString(MSTTransactionID) + '-' + Convert.ToString(TransactionID);
                            }

                            TxtPatient.Text = "";
                            this.PatientID = Convert.ToInt64(dtTransactionID.Rows[0]["nPatientID"]);
                            this.PatientName = Convert.ToString(dtTransactionID.Rows[0]["Patient"]);
                            TxtPatient.Text = PatientName;
                            TxtPatient.Tag = Convert.ToInt64(PatientID);
                         
                        }
                    }
                    else
                    {
                        this.PatientID = Convert.ToInt64(TxtPatient.Tag);
                        this.PatientName = Convert.ToString(TxtPatient.Text);
                    }

                }
                
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;

            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        private bool getValidRefundClaimDetails()
        {

            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");

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
                       
                        RefundClaimDetails = new SplitClaimDetails(ogloBilling.MainClaimNumber, ogloBilling.SubClaimNumber);

                        if (!RefundClaimDetails.IsClaimExist)
                        {
                            MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //TxtRefundPatient.Text = "";
                            return false;
                        }
                        else
                        {


                            DataTable dtTransactionID = new DataTable();
                            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            string _strSql = "";

                            if (ogloBilling.SubClaimNumber == "")
                            {
                                _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                                    + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                                    + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                                    + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                                    + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + "";
                            }
                            else
                            {
                                _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                                        + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                                        + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                                        + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                                        + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + " AND nSubClaimNo = " + ogloBilling.SubClaimNumber;
                            }
                            oDB.Connect(false);
                            oDB.Retrive_Query(_strSql, out dtTransactionID);
                            oDB.Disconnect();
                            if (oDB != null) { oDB.Dispose(); }
                            //  CmbRefundClaim.DataSource = null;
                            if (RefundClaimDetails.TransactionMasterID == 0)
                            {
                                this.MSTTransactionID = Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionMasterID"]);
                                this.TransactionID = Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionID"]);
                                ClaimNo = CmbRefundClaim.Text;
                                CmbRefundClaim.Tag = Convert.ToString(MSTTransactionID) + '-' + Convert.ToString(TransactionID);
                            }
                            else
                            {
                                this.MSTTransactionID = RefundClaimDetails.TransactionMasterID;
                                this.TransactionID = RefundClaimDetails.TransactionID;
                                ClaimNo = Convert.ToString(RefundClaimDetails.ClaimNo);
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
                        if (Convert.ToString(TxtPatient.Tag) != "")
                        {
                            this.PatientID = Convert.ToInt64(TxtPatient.Tag);
                            this.PatientName = Convert.ToString(TxtPatient.Text);
                        }
                        else
                        {
                            this.PatientID = 0;
                            this.PatientName ="";
                        }
                        
                    }
                } 
                return true;
            } 
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;

            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        private void SetRefundTo()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _ResultGuarantor = null;
            try
            {
                string _strQuery = "";
                _strQuery = "SELECT ISNULL(Patient_OtherContacts.sFirstName,'') + SPACE(1) + ISNULL(Patient_OtherContacts.sMiddleName,'') + SPACE(1)+ ISNULL(Patient_OtherContacts.sLastName,'') AS Guarantor "
                          + " FROM Patient WITH (NOLOCK) LEFT JOIN Patient_OtherContacts WITH (NOLOCK) ON Patient.nPatientID = Patient_OtherContacts.nPatientID "
                          + " WHERE Patient.nPatientID = " + _nInsuranceID + " AND Patient.nClinicID =" + _ClinicID + " AND (Patient_OtherContacts.nPatientContactTypeFlag = 1 OR Patient_OtherContacts.nPatientContactTypeFlag  IS NULL )  ";


                oDB.Connect(false);
                _ResultGuarantor = oDB.ExecuteScalar_Query(_strQuery);

                if (Convert.ToString(_ResultGuarantor).Trim() == "")
                {
                    _strQuery = "SELECT dbo.GET_NAME(sFirstName,sMiddleName,sLastName) FROM Patient WITH (NOLOCK) WHERE nPatientID ='" + _nInsuranceID + "'";
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

        private Boolean FillEOBData()
        {
            Boolean bIsRefundAmount = true;
            gloGeneralItem.gloItems _oSeletedReserveItems = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem ogloItem = null;
            decimal _selectedAmount = 0;
            Int64 _selEOBPayId = 0;
            Int64 _selEOBPayDtlId = 0;
            Int64 _selRefEOBPayId = 0;
            Int64 _selRefEOBPayDtlId = 0;
            Int32 _selEOBPayPayMode = 0;
            //Int64 _selEOBAccountId = 0;
            //Int64 _selEOBMSTAccountId = 0;

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
                                if (c1Reserve.GetData(i, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
                                { _selEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)); }

                                if (c1Reserve.GetData(i, COL_REFEOBPAYID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYID)).ToString().Trim() != "")
                                { _selRefEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYID)); }

                                if (c1Reserve.GetData(i, COL_REFEOBPAYDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)).ToString().Trim() != "")
                                { _selRefEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)); }

                                if (c1Reserve.GetData(i, COL_PAYMODE) != null && Convert.ToString(c1Reserve.GetData(i, COL_PAYMODE)).ToString().Trim() != "")
                                {
                                    _selEOBPayPayMode = ((EOBPaymentMode)Convert.ToInt32(c1Reserve.GetData(i, COL_PAYMODE))).GetHashCode();
                                }

                                //if (c1Reserve.GetData(i, COL_ACCOUNTID) != null && Convert.ToString(c1Reserve.GetData(i, COL_ACCOUNTID)).ToString().Trim() != "")
                                //{
                                //    _selEOBAccountId = Convert.ToInt64(c1Reserve.GetData(i, COL_ACCOUNTID));
                                //}

                                //if (c1Reserve.GetData(i, COL_MSTACCOUNTID) != null && Convert.ToString(c1Reserve.GetData(i, COL_PAYMODE)).ToString().Trim() != "")
                                //{
                                //    _selEOBMSTAccountId = Convert.ToInt64(c1Reserve.GetData(i, COL_MSTACCOUNTID));
                                //}

                                ogloItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(c1Reserve.GetData(i, COL_REFUND)).Trim());
                                ogloItem.SubItems.Add(_selRefEOBPayId, _selEOBPayPayMode.ToString(), _selRefEOBPayDtlId.ToString());
                                _oSeletedReserveItems.Add(ogloItem);
                               // ogloItem.Dispose(); //SLR: Should not be sunbitems.

                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayPayMode = 0;
                            }
                        }
                    }


                }
                if (_selectedAmount > 0)
                {
                    oSeletedReserveItems = _oSeletedReserveItems;
                }
                else
                {
                    bIsRefundAmount = false;
                    //MessageBox.Show("Enter refund amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //if (c1Reserve != null && c1Reserve.Rows.Count > 1) { c1Reserve.Focus(); c1Reserve.Select(1, COL_REFUND, true); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bIsRefundAmount = false;
                return bIsRefundAmount;
            }
            return bIsRefundAmount;

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

        private Int64 SaveInsuranceRefund()
        {
            Int64 _retPayId = 0;

            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(_databaseconnectionstring);
            EOBPayment.Common.PaymentInsurance oPaymentInsurance = new global::gloBilling.EOBPayment.Common.PaymentInsurance();
            EOBPayment.Common.PaymentInsuranceClaim oPaymentInsuranceClaim = null;
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentDetail = null;
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
            EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.Check;
            EOBPayment.Common.InsurancePaymentRefund oInsurancePaymentRefund = new global::gloBilling.EOBPayment.Common.InsurancePaymentRefund();

            try
            {
                Int64 _CloseDayTrayID = 0;
                string _CloseDayTrayCode = "";
                string _CloseDayTrayName = "";

                if (txtRefundAmount.Text.Remove(0, 1).Trim().Length > 0 && Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim()) > 0)
                {

                    #region "Payment Tray"

                    _CloseDayTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());
                    _CloseDayTrayCode = "";
                    _CloseDayTrayName = lblPaymentTray.Text;

                    #endregion

                    #region "Payment Mode"
                    //if (cmbPayMode.Text != "")
                    //{
                    //    if (cmbPayMode.Text.Trim() == EOBPaymentMode.None.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.None; }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.Cash; }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.Check; }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                    //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                    //    { _EOBPaymentMode = EOBPaymentMode.EFT; }
                    //}
                    _EOBPaymentMode = EOBPaymentMode.Check;
                    #endregion

                    #region " Master Data "


                    oPaymentInsurance.PaymentNumber = ogloEOBPaymentInsurance.GetPaymentPrefixNumber(_paymentPrefix).Split('#')[1];
                    oPaymentInsurance.PaymentNumberPefix = _paymentPrefix;

                    oPaymentInsurance.EOBPaymentID = 0;

                    //oPaymentInsurance.EOBRefNO = txtEOBRefNumber.Text.Trim();
                    if (_EOBPaymentMode == EOBPaymentMode.Cash)
                    { oPaymentInsurance.EOBRefNO = txtCheckNumber.Text.Trim(); }
                    else
                    { oPaymentInsurance.EOBRefNO = ""; }

                    oPaymentInsurance.PayerName = "";

                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    oPaymentInsurance.PayerID = _nInsuranceID;
                    oPaymentInsurance.PayerType = EOBPaymentAccountType.InsuranceCompany;
                    oPaymentInsurance.PaymentMode = _EOBPaymentMode;
                    oPaymentInsurance.CheckNumber = txtCheckNumber.Text.Trim(); ;
                    if (txtRefundAmount.Text.Remove(0, 1).Trim() != "")
                    {
                        oPaymentInsurance.CheckAmount = Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim());
                    }

                    if (mskCheckDate.MaskCompleted)
                    {
                        mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oPaymentInsurance.CheckDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
                    }

                    if (_EOBPaymentMode == EOBPaymentMode.CreditCard)
                    {
                        oPaymentInsurance.CardType = cmbCardType.Text.Trim();
                        oPaymentInsurance.AuthorizationNo = txtCardAuthorizationNo.Text.Trim();
                        if (mskCreditExpiryDate.MaskCompleted)
                        {
                            mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                            oPaymentInsurance.CardExpiryDate = Convert.ToInt64(mskCreditExpiryDate.Text);
                        }
                        oPaymentInsurance.CardID = Convert.ToInt64(cmbCardType.SelectedValue);
                    }

                    oPaymentInsurance.MSTAccountID = _nInsuranceID;
                    oPaymentInsurance.MSTAccountType = EOBPaymentAccountType.InsuranceCompany;
                    oPaymentInsurance.ClinicID = _ClinicID;
                    oPaymentInsurance.CreatedDateTime = DateTime.Now;
                    oPaymentInsurance.ModifiedDateTime = DateTime.Now;

                    oPaymentInsurance.PaymentTrayID = _CloseDayTrayID;
                    oPaymentInsurance.PaymentTrayCode = _CloseDayTrayCode;
                    oPaymentInsurance.PaymentTrayDesc = _CloseDayTrayName;

                    if (mskCloseDate.MaskCompleted == true)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oPaymentInsurance.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }

                    oPaymentInsurance.UserID = _UserId;
                    oPaymentInsurance.UserName = _UserName;


                    #region "Payment Master Note"
                    //Notes if any to main payment to all claim OR main payment to reserve account
                    if (txtNotes.Text.Trim().Length > 0)
                    {
                        EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                        oPaymentInsuranceLineNote.ClaimNo = 0;
                        oPaymentInsuranceLineNote.EOBPaymentID = 0;
                        oPaymentInsuranceLineNote.EOBID = 0;
                        oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;
                        oPaymentInsuranceLineNote.BillingTransactionID = 0;
                        oPaymentInsuranceLineNote.BillingTransactionDetailID = 0;
                        oPaymentInsuranceLineNote.Code = "";
                        oPaymentInsuranceLineNote.Description = txtNotes.Text.Trim();
                        oPaymentInsuranceLineNote.Amount = Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim());
                        oPaymentInsuranceLineNote.IncludeOnPrint = false;
                        oPaymentInsuranceLineNote.ClinicID = _ClinicID;
                        oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
                        oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.Other;


                        oPaymentInsurance.Notes.Add(oPaymentInsuranceLineNote);
                        oPaymentInsuranceLineNote.Dispose();
                    }
                    #endregion



                    #endregion

                    #region "Credit Service Line Entry applicable to all claims, so it goes to master level not line level"

                    oEOBInsurancePaymentCreditDetail.EOBPaymentID = 0;
                    oEOBInsurancePaymentCreditDetail.EOBID = 0;
                    oEOBInsurancePaymentCreditDetail.EOBDtlID = 0;
                    oEOBInsurancePaymentCreditDetail.EOBPaymentDetailID = 0;

                    oEOBInsurancePaymentCreditDetail.RefEOBPaymentID = 0;
                    oEOBInsurancePaymentCreditDetail.RefEOBPaymentDetailID = 0;
                    oEOBInsurancePaymentCreditDetail.ReserveEOBPaymentID = 0;
                    oEOBInsurancePaymentCreditDetail.ReserveEOBPaymentDetailID = 0;
                    //}

                    oEOBInsurancePaymentCreditDetail.BillingTransactionID = 0;
                    oEOBInsurancePaymentCreditDetail.BillingTransactionDetailID = 0;
                    oEOBInsurancePaymentCreditDetail.BillingTransactionLineNo = 0;
                    if (mskCloseDate.MaskCompleted == true)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oEOBInsurancePaymentCreditDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                        oEOBInsurancePaymentCreditDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }
                    oEOBInsurancePaymentCreditDetail.CPTCode = "";
                    oEOBInsurancePaymentCreditDetail.CPTDescription = "";
                    oEOBInsurancePaymentCreditDetail.Amount = Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim());
                    oEOBInsurancePaymentCreditDetail.IsNullAmount = false;
                    oEOBInsurancePaymentCreditDetail.PaymentType = EOBPaymentType.InsuraceRefund;
                    oEOBInsurancePaymentCreditDetail.PaymentSubType = EOBPaymentSubType.Refund;
                    oEOBInsurancePaymentCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                    oEOBInsurancePaymentCreditDetail.PayMode = _EOBPaymentMode;

                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    oEOBInsurancePaymentCreditDetail.AccountID = _nInsuranceID;
                    oEOBInsurancePaymentCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                    oEOBInsurancePaymentCreditDetail.MSTAccountID = _nInsuranceID;
                    oEOBInsurancePaymentCreditDetail.MSTAccountType = EOBPaymentAccountType.InsuranceCompany;
                    oEOBInsurancePaymentCreditDetail.PatientID = _PatientID;
                    oEOBInsurancePaymentCreditDetail.PaymentTrayID = _CloseDayTrayID;
                    oEOBInsurancePaymentCreditDetail.PaymentTrayCode = _CloseDayTrayCode;
                    oEOBInsurancePaymentCreditDetail.PaymentTrayDescription = _CloseDayTrayName;
                    oEOBInsurancePaymentCreditDetail.UserID = _UserId;
                    oEOBInsurancePaymentCreditDetail.UserName = _UserName;
                    oEOBInsurancePaymentCreditDetail.ClinicID = _ClinicID;

                    oEOBInsurancePaymentCreditDetail.FinanceLieNo = 1;
                    oEOBInsurancePaymentCreditDetail.MainCreditLineID = 1;
                    oEOBInsurancePaymentCreditDetail.IsMainCreditLine = true;
                    oEOBInsurancePaymentCreditDetail.IsReserveCreditLine = false;
                    oEOBInsurancePaymentCreditDetail.IsCorrectionCreditLine = false;
                    oEOBInsurancePaymentCreditDetail.RefFinanceLieNo = 0;
                    oEOBInsurancePaymentCreditDetail.UseRefFinanceLieNo = false;

                    if (mskCloseDate.MaskCompleted == true)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oEOBInsurancePaymentCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }

                    oInsurancePaymentRefund.RefundAmount = Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim());

                    if (mskrefunddate.MaskCompleted == true)
                    {
                        mskrefunddate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oInsurancePaymentRefund.Refunddate = gloDateMaster.gloDate.DateAsNumber(mskrefunddate.Text);
                    }
                    oInsurancePaymentRefund.RefundNotes = txtNotes.Text;
                    oInsurancePaymentRefund.RefundTo = txtTo.Text;

                    #region "Add Refund parameter"

                    //if (CmbRefundClaim.DropDownStyle == ComboBoxStyle.DropDownList)
                    //{
                    //if (CmbRefundClaim.ValueMember != null)
                    //{
                    //    string[] IDs = CmbRefundClaim.ValueMember.ToString().Split('-');
                    //    if (IDs.Length.Equals(2))
                    //    {
                    //        oInsurancePaymentRefund.MasterTransactionID = Convert.ToInt64(IDs[0]);
                    //        oInsurancePaymentRefund.TransactionID = Convert.ToInt64(IDs[1]);
                    //        oInsurancePaymentRefund.ClaimNo = Convert.ToString(CmbRefundClaim.Text);

                    //    }
                    //}
                    // }
                    oInsurancePaymentRefund.MasterTransactionID = RefundClaimDetails.TransactionMasterID;
                    oInsurancePaymentRefund.TransactionID = RefundClaimDetails.TransactionID;
                    oInsurancePaymentRefund.ClaimNo = Convert.ToString(RefundClaimDetails.ClaimNo);
                    if (Convert.ToString(TxtRefundPatient.Tag) != "")
                    {

                        oInsurancePaymentRefund.PatientID = Convert.ToInt64(TxtRefundPatient.Tag);
                    }

                    #endregion""

                    oPaymentInsurance.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentCreditDetail);

                    #endregion

                    #region "Reserve Debit Entry if any and it will goes directly to payment object with credit line"

                    if (c1Reserve != null && c1Reserve.Rows.Count > 1)
                    {
                        c1Reserve.FinishEditing();

                        for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
                        {
                            if (c1Reserve.GetData(i, COL_REFUND) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFUND)).ToString().Trim() != "")
                            {
                                if (Convert.ToDecimal(c1Reserve.GetData(i, COL_REFUND)) > 0)
                                {

                                    oEOBInsurancePaymentDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                    oEOBInsurancePaymentDetail.EOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.EOBID = 0;
                                    oEOBInsurancePaymentDetail.EOBDtlID = 0;
                                    oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

                                    oEOBInsurancePaymentDetail.BillingTransactionID = 0;
                                    oEOBInsurancePaymentDetail.BillingTransactionDetailID = 0;
                                    oEOBInsurancePaymentDetail.BillingTransactionLineNo = 0;
                                    if (mskCloseDate.MaskCompleted == true)
                                    {
                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                        oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                        oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    }
                                    oEOBInsurancePaymentDetail.CPTCode = "";
                                    oEOBInsurancePaymentDetail.CPTDescription = "";

                                    //if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                                    decimal _amt = 0;
                                    // _amt = Convert.ToDecimal(_oSeletedReserveItems[i].Description.Trim());
                                    _amt = Convert.ToDecimal(c1Reserve.GetData(i, COL_REFUND));
                                    oEOBInsurancePaymentDetail.Amount = _amt;
                                    oEOBInsurancePaymentDetail.IsNullAmount = false;

                                    oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuraceRefund;
                                    oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.Refund;
                                    oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                    oEOBInsurancePaymentDetail.PayMode = _EOBPaymentMode;

                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTID)); ; //
                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID));
                                    oEOBInsurancePaymentDetail.RefEOBPaymentID = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYID));
                                    oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYDTLID));

                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                    oEOBInsurancePaymentDetail.AccountID = Convert.ToInt64(c1Reserve.GetData(i, COL_ACCOUNTID));
                                    oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                    oEOBInsurancePaymentDetail.MSTAccountID = Convert.ToInt64(c1Reserve.GetData(i, COL_MSTACCOUNTID));
                                    oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.InsuranceCompany;
                                    oEOBInsurancePaymentDetail.PatientID = _nInsuranceID;
                                    oEOBInsurancePaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                    oEOBInsurancePaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                    oEOBInsurancePaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                    oEOBInsurancePaymentDetail.UserID = _UserId;
                                    oEOBInsurancePaymentDetail.UserName = _UserName;
                                    oEOBInsurancePaymentDetail.ClinicID = _ClinicID;

                                    oEOBInsurancePaymentDetail.FinanceLieNo = 0;
                                    oEOBInsurancePaymentDetail.MainCreditLineID = 0;
                                    oEOBInsurancePaymentDetail.IsMainCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
                                    oEOBInsurancePaymentDetail.RefFinanceLieNo = 1;
                                    oEOBInsurancePaymentDetail.UseRefFinanceLieNo = true;

                                    if (mskCloseDate.MaskCompleted == true)
                                    {
                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                        oEOBInsurancePaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    }

                                    oPaymentInsurance.EOBInsurancePaymentReserveLineDetail.Add(oEOBInsurancePaymentDetail);
                                    oEOBInsurancePaymentDetail.Dispose();



                    #endregion
                                }
                            }
                        }
                    }
                    _retPayId = ogloEOBPaymentInsurance.SaveInsuranceRefund(oPaymentInsurance, oInsurancePaymentRefund, false);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloEOBPaymentInsurance != null) { ogloEOBPaymentInsurance.Dispose(); };
                if (oPaymentInsurance != null) { oPaymentInsurance.Dispose(); };
                if (oPaymentInsuranceClaim != null) { oPaymentInsuranceClaim.Dispose(); };
                if (oEOBInsurancePaymentDetail != null) { oEOBInsurancePaymentDetail.Dispose(); };
                if (oEOBInsurancePaymentCreditDetail != null) { oEOBInsurancePaymentCreditDetail.Dispose(); };
                if (oInsurancePaymentRefund != null) { oInsurancePaymentRefund.Dispose(); };
            }
            return _retPayId;
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

                        string _lastCloseDate = gloBilling.GetUserWiseCloseDay(_UserId, CloseDayType.Payment);
                        mskCloseDate.Text = _lastCloseDate;
                        _ncloseDate = (_lastCloseDate.Trim() == string.Empty ? 0 : gloDateMaster.gloDate.DateAsNumber(_lastCloseDate));

                        #endregion " Set last selected close date "
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
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

        private void FillInsuranceCompany()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtInsCompanies = null;
            string _sqlQuery = "";
            try
            {
                _sqlQuery = "SELECT ISNULL(nID,0) AS nID,ISNULL(sCode,'') AS sCode, " +
                 " ISNULL(sDescription,'') AS sDescription from Contacts_InsuranceCompany_MST WITH (NOLOCK) " +
                 " WHERE nClinicID = " + _ClinicID + " AND nID = " + _nInsuranceID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtInsCompanies);
                oDB.Disconnect();
                if (_dtInsCompanies != null && _dtInsCompanies.Rows.Count >= 0)
                {
                    lblInsCompany.Text = Convert.ToString(_dtInsCompanies.Rows[0]["sDescription"].ToString().Trim());
                    txtTo.Text = Convert.ToString(_dtInsCompanies.Rows[0]["sDescription"].ToString().Trim());
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }
        }

        #endregion " Private & Public Methods "

        #region "User Control events"

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;
            switch (_CurrentControlType)
            {

                case gloListControl.gloListControlType.Patient:
                    {
                        TxtPatient.Text = "";
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            //txtPatient.Text  = oBindTable;
                            TxtPatient.Text = Convert.ToString(oBindTable.Rows[0]["DispName"]);
                            TxtPatient.Tag = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                            PatientID = Convert.ToInt64(TxtPatient.Tag);
                            PatientName = TxtPatient.Text;

                        }

                        DataTable dtClaim = new DataTable();
                       
                        cmbClaimNo.DataSource = null;
                        cmbClaimNo.Items.Clear();
                        cmbClaimNo.Text = "";
                        getPatientClaimNos(Convert.ToInt64(TxtPatient.Tag),"Reserve");

                    }

                    break;

            }
          
            //this.Width = 1183;
            //this.Height = 570;
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch { }

            }
            //this.Width = 1183;
            //this.Height = 570;
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
                            PatientID = Convert.ToInt64(TxtRefundPatient.Tag);
                            PatientName = TxtRefundPatient.Text;

                        }

                        DataTable dtClaim = new DataTable();
                      
                        CmbRefundClaim.DataSource = null;
                        CmbRefundClaim.Items.Clear();
                        CmbRefundClaim.Text = "";
                        getPatientClaimNos(Convert.ToInt64(TxtRefundPatient.Tag), "refund");

                    }

                    break;

            }
            //this.Width = 1183;
            //this.Height = 570;
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
                try
                {
                    oRefListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oRefListControl_ItemSelectedClick);
                    oRefListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oRefListControl_ItemClosedClick);

                }
                catch { }
                

            }
            //this.Width = 1183;
            //this.Height = 570;
        }

        #endregion 

        private void cmbClaimNo_Leave(object sender, EventArgs e)
        {
            getValidClaimDetails();
        }

        private void CmbRefundClaim_Leave(object sender, EventArgs e)
        {
            getValidClaimDetails();
        }
    
        //private bool SaveRefundAssociation(Int64 nEOBPaymentID, Int64 nEOBPaymentDetailID, Int64 nTransactionID, Int64 nTrackTrnID, Int64 nPatientID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {
        //        oDB.Connect(false);

        //        oDBParameters.Add("@nEOBPaymentID", nEOBPaymentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@nEOBPaymentDetailID", nEOBPaymentDetailID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@nTransactionID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@nTrackTrnID", nTrackTrnID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);              

        //        oDB.Execute("BL_INUP_Refund_Association", oDBParameters);


        //        return true;
        //    }
        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //        MessageBox.Show(DBErr.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDBParameters.Dispose();
        //        oDB.Dispose();
        //    }
        //}

       
       
       
        

       
    }
}