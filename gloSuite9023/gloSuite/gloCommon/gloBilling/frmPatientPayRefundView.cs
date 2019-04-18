using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public partial class frmPatientPayRefundView : Form
    {

        #region " Private Variables "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        Int64 _UserId = 0;
        string _UserName = "";
        private string _MessageBoxCaption = "";

        private Int64 _patientId = 0;
        private Int64 _ncloseDate = 0;
        private Int64 _nrefundid = 0;
        public decimal SelectedUseReserveAmount = 0;
        public bool voidstatus=false;
        private bool _isValidate = true;
        private DateTime _closeDate = DateTime.Now;
        private string _closeDayTray = "";
        private bool _isDayClose = false;

        private gloGeneralItem.gloItems _oSeletedReserveItems = null;

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
        const int COL_SOURCE = 10; //Patient or Insurance Name

        const int COL_ORIGINALPAYMENT = 11;//Check Number,Date,Amount
        const int COL_TORESERVES = 12;//Amount for reserve
        const int COL_TYPE = 13;//Copay,Advance,Other
        const int COL_NOTE = 14;//Note

        const int COL_AVAILABLE = 15;//Available amount
        const int COL_USERESERVE = 16;//Used Reserve
        const int COL_REFUND = 17;//Current amount to use from avaiable amount

        const int COL_PAYMODE = 18;
        const int COL_REFEOBPAYID = 19;
        const int COL_REFEOBPAYDTLID = 20;
        const int COL_ACCOUNTID = 21;
        const int COL_ACCOUNTTYPE = 22;
        const int COL_MSTACCOUNTID = 23;
        const int COL_MSTACCOUNTTYPE = 24;
        const int COL_RES_EOBPAYID = 25;
        const int COL_RES_EOBPAYDTLID = 26;
        

        const int COL_COUNT = 27;

        #endregion 

        #region " Property Procedures "

        public Int64 ClinicID
        { get { return _ClinicID; } set { _ClinicID = value; } }
        public Int64  UserID
        { get { return _UserId; } set { _UserId = value; } }
        public string UserName
        { get { return _UserName; } set { _UserName = value; } }
        private Int64 PatientID
        { get { return _patientId; } set { _patientId = value; } }
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
        #endregion " Property Procedures "

        #region " Constructors "

        public frmPatientPayRefundView(string DatabaseConnectionString, Int64 Patientid,Int64 RefundId)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _patientId = Patientid;
            _nrefundid = RefundId;
           

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
            lblvoid.Visible = false;           
            FillCreditCards();
            FillPaymentMode();
            FillPatientRefund();
            SetNoteType();
            gloBilling globill = new gloBilling(_databaseconnectionstring,"");         

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

            if (globill != null)
                {
                    globill.Dispose();
                    globill = null;
                }
            
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

                  //  EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;

                    if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                    {
                        //_EOBPaymentMode = EOBPaymentMode.Cash;

                        txtCheckNumber.Text = "";
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Ref.# :";
                        lblCheckNo.Enabled = true;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.Enabled = true;
                    }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                    {
                       // _EOBPaymentMode = EOBPaymentMode.Check;
                        lblCheckDate.Text = "Check Date :";
                        lblCheckNo.Text = "Check# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;

                    }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                    {
                      //  _EOBPaymentMode = EOBPaymentMode.MoneyOrder;
                        lblCheckDate.Text = "MO Date :";
                        lblCheckNo.Text = "MO# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                    {
                      //  _EOBPaymentMode = EOBPaymentMode.CreditCard;
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Card# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = true;
                        txtCheckNumber.MaxLength = 4;

                    }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                    {
                    //    _EOBPaymentMode = EOBPaymentMode.EFT;
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

        #region "Tool Strip button events "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            tsb_OK.Select();
            label1.Focus();
             if(_isValidate == true )
            {
                if (SavePaymentValidation())
                {
                    UpdatePatientRefund();
                    this.Close();                                 
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1Refund.RowSel > 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to void refund ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        int _date = 0;

                        if (mskCloseDate.MaskCompleted)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            _date = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.Trim());
                        }
                        frmVoidPatientRefund ofrm = new frmVoidPatientRefund(_date, _nrefundid, _patientId);
                        ofrm.ShowDialog(this);
                        ofrm.Dispose();
                        ofrm = null;
                        SetVoidData();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
                 
        #endregion

        #region " Design Grid "

        private void DesignReserveRefundGrid()
        {
            try
            {
              
                #region " Set Header "
                c1Refund.Cols["PatientName"].Caption = "Source";
                c1Refund.Cols["OriginalPay"].Caption = "Original Payment";
                c1Refund.Cols["nPaymentNoteSubType"].Caption = "Type";
                c1Refund.Cols["sNoteDescription"].Caption = "Note";
                c1Refund.Cols["nAmount"].Caption = "Refund";
                   
                #endregion

                int _nWidth = 0;
        
                  c1Refund.Cols[5].Width = 0;
                  c1Refund.Cols[6].Width = 0;
                  c1Refund.Cols[7].Width = 0;
       
                  c1Refund.Cols[5].Visible = false;
                  c1Refund.Cols[6].Visible = false;
                  c1Refund.Cols[7].Visible = false;
           

                _nWidth = 930;//Convert.ToInt32( c1QueuedClaims.Width);
                c1Refund.Cols["PatientName"].Width = Convert.ToInt32(_nWidth * 0.15); ;            
                c1Refund.Cols["OriginalPay"].Width = Convert.ToInt32(_nWidth * 0.25);
                c1Refund.Cols["nPaymentNoteSubType"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1Refund.Cols["sNoteDescription"].Width = Convert.ToInt32(_nWidth * 0.33);
                c1Refund.Cols["nAmount"].Width = Convert.ToInt32(_nWidth * 0.15);
                c1Refund.Cols["nAmount"].Format = "c";
                c1Refund.ShowCellLabels = false;
                
             }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
            finally
            {  c1Refund.Redraw = true; }
        }

        #endregion " Design Grid "

        #region " Grid Events "

        private void c1Reserve_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            #region "Numeric Validation"
            if (c1Refund.ColSel == COL_REFUND)
            {
                decimal _result = Convert.ToDecimal(c1Refund.GetData(c1Refund.RowSel, c1Refund.ColSel));
                if (e.KeyChar == Convert.ToChar("-"))
                {
                    e.Handled = true;
                }

            }
            #endregion
        }

        private void c1Refund_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }
                     
        #endregion

        #region " Private & Public Methods "

        public void FillPatientRefund()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet _dtReservesRefund = new DataSet();

            try
            {                           
                oParameters.Add("@nRefundID", _nrefundid , ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),                
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Payment_Refund", oParameters, out _dtReservesRefund);
                oDB.Disconnect();
                c1Refund.DataSource = _dtReservesRefund.Tables[0];
                DesignReserveRefundGrid();
                if (_dtReservesRefund.Tables[0].Rows.Count > 0 && _dtReservesRefund.Tables[1].Rows.Count > 0)
                {
                    txtTo.Text = _dtReservesRefund.Tables[1].Rows[0]["sRefundTo"].ToString();
                    txtNotes.Text = _dtReservesRefund.Tables[1].Rows[0]["sRefundNotes"].ToString();
                    txtRefundAmount.Text = "$" + _dtReservesRefund.Tables[1].Rows[0]["nRefundAmount"].ToString();
                    mskrefunddate.Text = _dtReservesRefund.Tables[1].Rows[0]["nRefundDate"].ToString();
                    mskCheckDate.Text = _dtReservesRefund.Tables[1].Rows[0]["nCheckDate"].ToString();
                    txtCheckNumber.Text = _dtReservesRefund.Tables[1].Rows[0]["sCheckNumber"].ToString();
                    mskCloseDate.Text = _dtReservesRefund.Tables[1].Rows[0]["nCloseDate"].ToString();
                    lblPaymentTray.Text = _dtReservesRefund.Tables[1].Rows[0]["sPaymentTrayDescription"].ToString();
                    lblPaymentTray.Tag = _dtReservesRefund.Tables[1].Rows[0]["nPaymentTrayID"].ToString();
                    lblusername.Text = _dtReservesRefund.Tables[1].Rows[0]["sUserName"].ToString();
                    lbldatetime.Text = Convert.ToDateTime(_dtReservesRefund.Tables[1].Rows[0]["dtModifiedDateTime"]).ToString("MM/dd/yyy hh:mm tt");
                   _ncloseDate = gloDateMaster.gloDate.DateAsNumber(_dtReservesRefund.Tables[1].Rows[0]["nCloseDate"].ToString());

                    //MaheshB
                   this.cmbPayMode.SelectedIndexChanged -= new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);
                    if (EOBPaymentMode.Cash.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = EOBPaymentMode.Cash.ToString();
                        //txtCheckNumber.Text = "";
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Ref.# :";
                        lblCheckNo.Enabled = true;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.Enabled = true;
                    }
                    else if (EOBPaymentMode.Check.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = EOBPaymentMode.Check.ToString();
                        lblCheckDate.Text = "Check Date :";
                        lblCheckNo.Text = "Check# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (EOBPaymentMode.CreditCard.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = EOBPaymentMode.CreditCard.ToString();
                        lblCheckDate.Text = "Date :";
                        lblCheckNo.Text = "Card# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = true;
                        txtCheckNumber.MaxLength = 4;
                        cmbCardType.SelectedValue = _dtReservesRefund.Tables[1].Rows[0]["nCardID"].ToString().Trim();
                        txtCardAuthorizationNo.Text = _dtReservesRefund.Tables[1].Rows[0]["sAuthorizationNo"].ToString();
                        if (_dtReservesRefund.Tables[1].Rows[0]["nCardExpDate"].ToString().Length == 3)
                        {
                            mskCreditExpiryDate.Text = "0"+_dtReservesRefund.Tables[1].Rows[0]["nCardExpDate"].ToString();
                        }
                        else
                        {
                            if (Convert.ToString(_dtReservesRefund.Tables[1].Rows[0]["nCardExpDate"])!= "0")
                            {
                                mskCreditExpiryDate.Text = _dtReservesRefund.Tables[1].Rows[0]["nCardExpDate"].ToString();
                            }
                        }

                    }
                    else if (EOBPaymentMode.EFT.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = EOBPaymentMode.EFT.ToString();
                        lblCheckDate.Text = "EFT Date :";
                        lblCheckNo.Text = "EFT# :";
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (EOBPaymentMode.MoneyOrder.GetHashCode() == Convert.ToInt16(_dtReservesRefund.Tables[1].Rows[0]["nPaymentMode"]))
                    {
                        cmbPayMode.SelectedItem = EOBPaymentMode.MoneyOrder.ToString();
                        lblCheckDate.Text = "MO Date :";
                        lblCheckNo.Text = "MO# :";
                        //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    this.cmbPayMode.SelectedIndexChanged += new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);

                }

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
            }
            oDataTable.Dispose();
            oDB.Dispose();
            return result;


        }
       
        public void FillCreditCards()
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
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oCreditCards != null) { oCreditCards.Dispose(); }
                //if (_dtCards != null) { _dtCards.Dispose(); }
            }
        }

        private bool SavePaymentValidation()
        {
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");

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
                        c1Refund.Select(1, COL_REFUND);
                    }
                    return false;
                }

                EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
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
                    MessageBox.Show("Select refund mode.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPayMode.Select();
                    cmbPayMode.Focus();
                    return false;
                }
                else if (_EOBPaymentMode == EOBPaymentMode.CreditCard)
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
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strQuery = "";
            DataTable dtVoidData = new DataTable();
            try
            {
                _strQuery = "SELECT bisvoid,dtVoidDateTime,sVoidUserName,nVoidTrayDescription,nVoidCloseDate FROM BL_EOBPatient_Refund WITH (NOLOCK) WHERE isnull(bIsvoid ,0) = 1 and nRefundID=" + _nrefundid;
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtVoidData);
                if (dtVoidData != null & dtVoidData.Rows.Count > 0)
                {
                    voidstatus = true;
                    lblvoid.Visible = true;
                    DisableControl();
                    lblvoid.Text = "Voided [" + dtVoidData.Rows[0]["sVoidUserName"].ToString() + "] on " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtVoidData.Rows[0]["nVoidCloseDate"])).ToString("MM/dd/yyyy");
                    //lblusername.Text = dtVoidData.Rows[0]["sVoidUserName"].ToString();
                    //lblvoidtray.Text = dtVoidData.Rows[0]["nVoidTrayDescription"].ToString();
                    //lbldatetime.Text = dtVoidData.Rows[0]["dtVoidDateTime"].ToString();
                    tsb_void_refund.Enabled = false;
                    tsb_OK.Enabled = false;
                    txtNotes.Enabled = false;
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

        private void UpdatePatientRefund()
        {

            long closedate = 0;
            long checkdate = 0;
            string checkno = "";
            string cardtype = "";
            long cardid = 0;
            string AuthorizationNo = "";
            long Cardexp = 0;

            EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            EOBPayment.Common.PatientPaymentReturn oPatientPaymentReturn = new global::gloBilling.EOBPayment.Common.PatientPaymentReturn();

            try
            {
                if (_isDayClose == false)
                {

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
                        {
                            _EOBPaymentMode = EOBPaymentMode.CreditCard;
                            cardtype = cmbCardType.Text;
                            cardid = Convert.ToInt64(cmbCardType.SelectedValue);
                            AuthorizationNo = txtCardAuthorizationNo.Text;
                            if (mskCreditExpiryDate.MaskCompleted)
                            {
                                mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                                Cardexp = Convert.ToInt64(mskCreditExpiryDate.Text);
                            }
                        }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                        { _EOBPaymentMode = EOBPaymentMode.EFT; }
                    }
                    #endregion


                    if (mskrefunddate.MaskCompleted)
                    {
                        mskrefunddate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oPatientPaymentReturn.Refunddate = gloDateMaster.gloDate.DateAsNumber(mskrefunddate.Text);
                    }

                    oPatientPaymentReturn.RefundNotes = txtNotes.Text;
                    oPatientPaymentReturn.RefundTo = txtTo.Text;

                    if (mskCloseDate.MaskCompleted)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        closedate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }
                    if (mskCheckDate.MaskCompleted)
                    {
                        mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        checkdate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
                    }
                    checkno = txtCheckNumber.Text.Trim();

                    oParameters.Add("@Flag", _isDayClose, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@nRefundID", _nrefundid, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),       
                    oParameters.Add("@nClosedate", closedate, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPaymentTrayId", lblPaymentTray.Tag, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sPaymentTrayDesc", lblPaymentTray.Text, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sRefundto", oPatientPaymentReturn.RefundTo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nRefundDate", oPatientPaymentReturn.Refunddate, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sRefundNote", oPatientPaymentReturn.RefundNotes, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nPaymentmode", _EOBPaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sCheckno", checkno, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nCheckdate", checkdate, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sCardType", cardtype, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nCardID", cardid, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sAuthorizationNo", AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nCardExpDate", Cardexp, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dtModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);
                   
                }
                else
                {
                    oParameters.Add("@Flag", _isDayClose, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@nRefundID", _nrefundid, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),   
                    oParameters.Add("@sRefundNote", txtNotes.Text, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@dtModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);
                 
                
                }

                oDB.Connect(false);
                oDB.Execute("BL_UPDATE_REFUND_DATA", oParameters);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oPatientPaymentReturn != null) { oPatientPaymentReturn.Dispose(); }

            }

        }

        //private void GetCloseDate()
        //{
        //    gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
        //    try
        //    {
        //        MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
        //        mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        //        string strDate = mskDate.Text;
        //        mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
        //        if (mskDate != null)
        //        {
        //            if (strDate.Length <= 0)
        //            {

        //                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
        //                Object _retValue = null;
        //                string _clsDate = "";
        //                oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", _UserId, _ClinicID, out _retValue);
        //                oSettings.Dispose();

        //                if (_retValue != null && Convert.ToString(_retValue).Trim() != "")
        //                {
        //                    try
        //                    {
        //                        _clsDate = Convert.ToDateTime(Convert.ToString(_retValue).Trim()).ToString("MM/dd/yyyy");
        //                        _ncloseDate = gloDateMaster.gloDate.DateAsNumber(_clsDate);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //                        ex = null;
        //                        _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
        //                        _ncloseDate = gloDateMaster.gloDate.DateAsNumber(_clsDate);
        //                    }
        //                }
        //                else
        //                {
        //                    _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
        //                    _ncloseDate = gloDateMaster.gloDate.DateAsNumber(_clsDate);
        //                }

        //                if (_clsDate.Trim() != "")
        //                {

        //                    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_clsDate.Trim())) == true)
        //                    {
        //                        _clsDate = "";
        //                        _ncloseDate = 0;
        //                    }
        //                    ogloBilling.Dispose();
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    {
        //        if (ogloBilling != null) { ogloBilling.Dispose(); }
        //    }

        //}

        private void SetNoteType()
        {
            if (c1Refund.DataSource != null && c1Refund.Rows.Count > 0)
            {

                for (int _rowIndex = 1; _rowIndex < c1Refund.Rows.Count; _rowIndex++)
                {
                    string _tempData = "";
                    _tempData = ((EOBPaymentMode)Convert.ToInt32(c1Refund.Rows[_rowIndex]["EOBPaymode"])).ToString() +
                     c1Refund.Rows[_rowIndex]["OriginalPay"];
                    c1Refund.SetData(_rowIndex, "OriginalPay", _tempData);//Check Number,Date,Amount                   
                    c1Refund.SetData(_rowIndex, "nPaymentNoteSubType", ((EOBPaymentSubType)Convert.ToInt32(c1Refund.Rows[_rowIndex]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                }
            }
        }

        #endregion " Private & Public Methods "

           
    }
}