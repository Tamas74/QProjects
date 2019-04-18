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
    public partial class frmPatientRefund : Form
    {

        #region " Private Variables "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        Int64 _UserId = 0;
        string _UserName = "";
        private string _MessageBoxCaption = "";
        private bool _IsFormLoading = false;
        private Int64 _patientId = 0;
        private Int64 _ncloseDate = 0;
        public decimal SelectedUseReserveAmount = 0;
      
        
        private DateTime _closeDate = DateTime.Now;
        private string _closeDayTray = "";
        private bool _isValidResAmount = true;
        private bool _isValidate = true;
        private bool _isFormClosing = false;
        private string _paymentPrefix = "GPM#";
        private gloGeneralItem.gloItems _oSeletedReserveItems = null;

        //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd).
        private Int64 _nPAccountId;
        private Int64 _nSelectedPatientId;
        private Int64 _nGuarantorId;
        private Int64 _nAccountPatientId;

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

        //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd).
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

        public frmPatientRefund(string DatabaseConnectionString, Int64 Patientid)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _patientId = Patientid;

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

                  //      EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;

                        if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                        {
                    //        _EOBPaymentMode = EOBPaymentMode.Cash;

                            txtCheckNumber.Text = "";
                            lblCheckDate.Text = "Date :";
                            lblCheckNo.Text = "Ref.# :";
                            lblCheckNo.Enabled = true;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.Enabled = true;
                        }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                        {
                      //      _EOBPaymentMode = EOBPaymentMode.Check;
                            lblCheckDate.Text = "Check Date :";
                            lblCheckNo.Text = "Check# :";
                            //lblCheckNo.TextAlign = ContentAlignment.MiddleLeft;
                            pnlCredit.Enabled = false;
                            txtCheckNumber.MaxLength = 15;

                        }
                        else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                        {
                        //    _EOBPaymentMode = EOBPaymentMode.MoneyOrder;
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

            private void refunddate_Leave(object sender, EventArgs e)
            {
                //try
                //{
                //    if (Convert.ToDateTime(mskCloseDate.Text ) <= Convert.ToDateTime(mskrefunddate.Text ))
                //    {
                //        MessageBox.Show(" ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
                //catch(Exception ex)
                //{
                //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //    ex = null;
                //}


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

            c1Reserve.FinishEditing();
            tsb_OK.Select();
            label5.Focus();
            if (_isValidate == true && _isValidResAmount == true)            
            {
                if (SavePaymentValidation())
                {
                    FillEOBData();
                    if (SavePatientRefund() > 0)
                    {
                        this.Close();
                    }
                   
                }
            }
        }

        private void SetRefundTo()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
             object _ResultGuarantor = null;
            try
            {
                string _strQuery = "";
                //_strQuery = "SELECT ISNULL(Patient_OtherContacts.sFirstName,'') + SPACE(1) + ISNULL(Patient_OtherContacts.sMiddleName,'') + SPACE(1)+ ISNULL(Patient_OtherContacts.sLastName,'') AS Guarantor "
                //          + " FROM Patient WITH (NOLOCK) LEFT JOIN Patient_OtherContacts WITH (NOLOCK) ON Patient.nPatientID = Patient_OtherContacts.nPatientID "
                //          + " WHERE Patient.nPatientID = " + _patientId + " AND Patient.nClinicID =" + _ClinicID + " AND (Patient_OtherContacts.nPatientContactTypeFlag = 1 OR Patient_OtherContacts.nPatientContactTypeFlag  IS NULL )  ";

                //Account Guarantor.
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

            //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd).
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

                                //Code added by SaiKrishna date 04-2-2011 for Patient Account Feature.
                                if (c1Reserve.GetData(i, COL_PATIENTID) != null && Convert.ToString(c1Reserve.GetData(i, COL_PATIENTID)).Trim() != "")
                                {
                                    _selPatientID = Convert.ToInt64(c1Reserve.GetData(i, COL_PATIENTID));
                                }
                                //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd).
                                ogloItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(c1Reserve.GetData(i, COL_REFUND)).Trim(), Convert.ToString(c1Reserve.GetData(i, COL_PATIENTID)));
                                ogloItem.SubItems.Add(_selRefEOBPayId, _selEOBPayPayMode.ToString(), _selRefEOBPayDtlId.ToString());
                                _oSeletedReserveItems.Add(ogloItem);
                                //ogloItem.Dispose(); //SLR: it should not be because it will dispose subitems..

                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayPayMode = 0;
                                //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd).
                                _selPatientID = 0;
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
                    MessageBox.Show("Enter refund amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (c1Reserve != null && c1Reserve.Rows.Count > 1) { c1Reserve.Focus(); c1Reserve.Select(1, COL_REFUND, true); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

        private Int64 SavePatientRefund()
        {
            Int64 _retPayId = 0;

            EOBPayment.gloEOBPaymentPatient ogloEOBPaymentPatient = new global::gloBilling.EOBPayment.gloEOBPaymentPatient(_databaseconnectionstring);
            EOBPayment.Common.PaymentPatient oPaymentPatient = new global::gloBilling.EOBPayment.Common.PaymentPatient();
            EOBPayment.Common.PaymentPatientClaim oPaymentPatientClaim = null;
            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentDetail = null;            
            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentCreditDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();
            EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
            EOBPayment.Common.PatientPaymentReturn oPatientPaymentReturn = new global::gloBilling.EOBPayment.Common.PatientPaymentReturn();

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


                    oPaymentPatient.PaymentNumber = ogloEOBPaymentPatient.GetPaymentPrefixNumber(_paymentPrefix).Split('#')[1];
                    oPaymentPatient.PaymentNumberPefix = _paymentPrefix;

                    oPaymentPatient.EOBPaymentID = 0;

            
                    //oPaymentPatient.EOBRefNO = txtEOBRefNumber.Text.Trim();
                    if (_EOBPaymentMode == EOBPaymentMode.Cash)
                    { oPaymentPatient.EOBRefNO = txtCheckNumber.Text.Trim(); }
                    else
                    { oPaymentPatient.EOBRefNO = ""; }

                    oPaymentPatient.PayerName = "";

                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    oPaymentPatient.PayerID = _patientId;
                    oPaymentPatient.PayerType = EOBPaymentAccountType.Patient;
                    oPaymentPatient.PaymentMode = _EOBPaymentMode;
                    oPaymentPatient.CheckNumber = txtCheckNumber.Text.Trim(); ;
                    if (txtRefundAmount.Text.Remove(0, 1).Trim() != "") { oPaymentPatient.CheckAmount = Convert.ToDecimal(txtRefundAmount.Text.Remove(0,1).Trim()); }

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

                    oPaymentPatient.MSTAccountID = _patientId;
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

                    oPaymentPatient.UserID = _UserId;
                    oPaymentPatient.UserName = _UserName;
                  

                    #region "Payment Master Note"
                    //Notes if any to main payment to all claim OR main payment to reserve account
                    if (txtNotes.Text.Trim().Length > 0)
                    {
                        EOBPayment.Common.PaymentPatientLineNote oPaymentPatientLineNote = new global::gloBilling.EOBPayment.Common.PaymentPatientLineNote();

                        oPaymentPatientLineNote.ClaimNo = 0;
                        oPaymentPatientLineNote.EOBPaymentID = 0;
                        oPaymentPatientLineNote.EOBID = 0;
                        oPaymentPatientLineNote.EOBPaymentDetailID = 0;
                        oPaymentPatientLineNote.BillingTransactionID = 0;
                        oPaymentPatientLineNote.BillingTransactionDetailID = 0;
                        oPaymentPatientLineNote.Code = "";
                        oPaymentPatientLineNote.Description = txtNotes.Text.Trim();
                        oPaymentPatientLineNote.Amount = Convert.ToDecimal(txtRefundAmount.Text.Remove(0,1).Trim());
                        oPaymentPatientLineNote.IncludeOnPrint = false;
                        oPaymentPatientLineNote.ClinicID = _ClinicID;
                        oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientPayment;
                        oPaymentPatientLineNote.PaymentNoteSubType = EOBPaymentSubType.Other;
                   

                        oPaymentPatient.Notes.Add(oPaymentPatientLineNote);
                        oPaymentPatientLineNote.Dispose();
                    }
                    #endregion

                    #endregion

                    #region "Credit Service Line Entry applicable to all claims, so it goes to master level not line level"

                    oEOBPatientPaymentCreditDetail.EOBPaymentID = 0;
                    oEOBPatientPaymentCreditDetail.EOBID = 0;
                    oEOBPatientPaymentCreditDetail.EOBDtlID = 0;
                    oEOBPatientPaymentCreditDetail.EOBPaymentDetailID = 0;

                    oEOBPatientPaymentCreditDetail.RefEOBPaymentID = 0;
                    oEOBPatientPaymentCreditDetail.RefEOBPaymentDetailID = 0;
                    oEOBPatientPaymentCreditDetail.ReserveEOBPaymentID = 0;
                    oEOBPatientPaymentCreditDetail.ReserveEOBPaymentDetailID = 0;
                    //}

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
                    oEOBPatientPaymentCreditDetail.Amount = Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim());
                    oEOBPatientPaymentCreditDetail.IsNullAmount = false;
                    oEOBPatientPaymentCreditDetail.PaymentType = EOBPaymentType.PatientRefund;
                    oEOBPatientPaymentCreditDetail.PaymentSubType = EOBPaymentSubType.Refund;
                    oEOBPatientPaymentCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                    oEOBPatientPaymentCreditDetail.PayMode = _EOBPaymentMode;

                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    oEOBPatientPaymentCreditDetail.AccountID = _patientId;
                    oEOBPatientPaymentCreditDetail.AccountType = EOBPaymentAccountType.Patient;
                    oEOBPatientPaymentCreditDetail.MSTAccountID = _patientId;
                    oEOBPatientPaymentCreditDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                    oEOBPatientPaymentCreditDetail.PatientID = _patientId;
                    oEOBPatientPaymentCreditDetail.PaymentTrayID = _CloseDayTrayID;
                    oEOBPatientPaymentCreditDetail.PaymentTrayCode = _CloseDayTrayCode;
                    oEOBPatientPaymentCreditDetail.PaymentTrayDescription = _CloseDayTrayName;
                    oEOBPatientPaymentCreditDetail.UserID = _UserId;
                    oEOBPatientPaymentCreditDetail.UserName = _UserName;
                    oEOBPatientPaymentCreditDetail.ClinicID = _ClinicID;

                    oEOBPatientPaymentCreditDetail.FinanceLieNo = 1;
                    oEOBPatientPaymentCreditDetail.MainCreditLineID = 1;
                    oEOBPatientPaymentCreditDetail.IsMainCreditLine = true;
                    oEOBPatientPaymentCreditDetail.IsReserveCreditLine = false;
                    oEOBPatientPaymentCreditDetail.IsCorrectionCreditLine = false;
                    oEOBPatientPaymentCreditDetail.RefFinanceLieNo = 0;
                    oEOBPatientPaymentCreditDetail.UseRefFinanceLieNo = false;

                    if (mskCloseDate.MaskCompleted == true)
                    {
                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oEOBPatientPaymentCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }

                    oPatientPaymentReturn.RefundAmount = Convert.ToDecimal(txtRefundAmount.Text.Remove(0, 1).Trim());

                    if (mskrefunddate.MaskCompleted == true)
                    {
                        mskrefunddate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        oPatientPaymentReturn.Refunddate = gloDateMaster.gloDate.DateAsNumber(mskrefunddate.Text);
                    }
                    oPatientPaymentReturn.RefundNotes = txtNotes.Text;
                    oPatientPaymentReturn.RefundTo = txtTo.Text;

                    //Code added by saikrishna date 03-02-2011 for assinging account related  parameters.
                    oEOBPatientPaymentCreditDetail.PAccountID = _nPAccountId;
                    oEOBPatientPaymentCreditDetail.AccountPatientID = _nAccountPatientId;
                    oEOBPatientPaymentCreditDetail.GuarantorID = _nGuarantorId;

                    oPaymentPatient.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentCreditDetail);

                    #endregion

                    #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"
            
                    if (_oSeletedReserveItems != null && _oSeletedReserveItems.Count > 0)
                    {
                        for (int i = 0; i < _oSeletedReserveItems.Count; i++)
                        {


                            //0 ReserveAmount 
                            //1 ReserveNote 
                            //2 ReserveSubType 
                            //3 ReserveNoteOnPrint 

                            //string[] oList = null;
                            //if (btnReserveRemaining.Tag != null)
                            //{
                            //    oList = btnReserveRemaining.Tag.ToString().Split('~');
                            //}

                            //if (oList != null && oList.Length >= 4)
                            if (_oSeletedReserveItems[i] != null && _oSeletedReserveItems[i].Description.Trim() != ""
                                && Convert.ToDecimal(_oSeletedReserveItems[i].Description.Trim()) > 0)
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

                                //if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                                decimal _amt = 0;
                                _amt = Convert.ToDecimal(_oSeletedReserveItems[i].Description.Trim());
                                oEOBPatientPaymentDetail.Amount = _amt;
                                oEOBPatientPaymentDetail.IsNullAmount = false;

                                oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientRefund;
                                oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Refund;
                                oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;

                                oEOBPatientPaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(_oSeletedReserveItems[i].ID); //
                                oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(_oSeletedReserveItems[i].Code);
                                oEOBPatientPaymentDetail.RefEOBPaymentID = Convert.ToInt64(_oSeletedReserveItems[i].SubItems[0].ID);
                                oEOBPatientPaymentDetail.RefEOBPaymentDetailID = Convert.ToInt64(_oSeletedReserveItems[i].SubItems[0].Description);

                                //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                oEOBPatientPaymentDetail.AccountID = EOBPaymentTypeAccountNo.None.GetHashCode();
                                oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                oEOBPatientPaymentDetail.MSTAccountID = EOBPaymentTypeAccountNo.None.GetHashCode();
                                oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd).
                                //oEOBPatientPaymentDetail.PatientID = _patientId;
                                oEOBPatientPaymentDetail.PatientID = Convert.ToInt64(oSeletedReserveItems[i].SSN);//Indicates PatientID
                                oEOBPatientPaymentDetail.PaymentTrayID = _CloseDayTrayID;
                                oEOBPatientPaymentDetail.PaymentTrayCode = _CloseDayTrayCode;
                                oEOBPatientPaymentDetail.PaymentTrayDescription = _CloseDayTrayName;
                                oEOBPatientPaymentDetail.UserID = _UserId;
                                oEOBPatientPaymentDetail.UserName = _UserName;
                                oEOBPatientPaymentDetail.ClinicID = _ClinicID;

                                oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                oEOBPatientPaymentDetail.RefFinanceLieNo = 1;
                                oEOBPatientPaymentDetail.UseRefFinanceLieNo = true;

                                if (mskCloseDate.MaskCompleted == true)
                                {
                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                    oEOBPatientPaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                }

                                oPaymentPatient.EOBPatientPaymentReserveLineDetail.Add(oEOBPatientPaymentDetail);
                                oEOBPatientPaymentDetail.Dispose();
                            }
                        }
                    }
                    #endregion
                    ////Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd) for assinging account related parameters.
                    oPaymentPatient.PAccountID = _nPAccountId;
                    oPaymentPatient.AccountPatientID = _nAccountPatientId;
                    oPaymentPatient.GuarantorID = _nGuarantorId;

                    _retPayId = ogloEOBPaymentPatient.SavePatientRefund(oPaymentPatient,oPatientPaymentReturn, false);
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
                if (oPatientPaymentReturn != null) { oPatientPaymentReturn.Dispose(); };
            }
            return _retPayId;
        }


        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

                        string _lastCloseDate = gloBilling.GetUserWiseCloseDay(_UserId, CloseDayType.Payment);
                        mskCloseDate.Text = _lastCloseDate;

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

        public void FillPaymentTray()
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

                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK)" +
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
                c1Payment.Cols[COL_TYPE].Visible = true;// 100;
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
                    c1Payment.Cols[COL_SOURCE].Width = 100;
                    c1Payment.Cols[COL_ORIGINALPAYMENT].Width = 250;
                    c1Payment.Cols[COL_TORESERVES].Width = 80;
                    c1Payment.Cols[COL_TYPE].Width = 100;
                    c1Payment.Cols[COL_NOTE].Width = 230;
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
                       csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                       //csEditableCurrencyStyle.BackColor = Color.White;

                   }

               }
               catch
               {
                   csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                   csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                   csEditableCurrencyStyle.Format = "c";
                   csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                                    MessageBox.Show("Refund amount cannot be negative. Enter valid refund amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                                 
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
                                    MessageBox.Show("Refund amount cannot be more than available amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                                
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

        public void FillReserves()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtReserves = new DataTable();

            try
            {
                
                DesignPaymentGrid(c1Reserve);

                _IsFormLoading = true;

                //Code added by SaiKrishna for Patient Account Feature. 
                oParameters.Add("@nPAccountID", _nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", _nSelectedPatientId, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
                oParameters.Add("@nEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0)

                oDB.Connect(false);
                oDB.Retrive("PA_BL_SELECT_PaymentTransaction_UseReserve", oParameters, out _dtReserves);
                oDB.Disconnect();

                if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                {
                    int _rowIndex = 0;

                    for (int i = 0; i < _dtReserves.Rows.Count; i++)
                    {

                        #region " Set Data "

                        _rowIndex = c1Reserve.Rows.Add().Index;

                        c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_EOBID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBID"]));
                        c1Reserve.SetData(_rowIndex, COL_EOBDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBDtlID"]));
                        c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_BLTRANSACTIONID,Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionID"]));
                        c1Reserve.SetData(_rowIndex, COL_BLTRANDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_BLTRANLINEID,Convert.ToString(_dtReserves.Rows[i]["nBillingTransactionLineNo"]));
                        c1Reserve.SetData(_rowIndex, COL_DOSFROM,Convert.ToInt64(_dtReserves.Rows[i]["nDOSFrom"]));
                        c1Reserve.SetData(_rowIndex, COL_DOSTO, Convert.ToString(_dtReserves.Rows[i]["nDOSTo"]));
                        c1Reserve.SetData(_rowIndex, COL_PATIENTID,Convert.ToString(_dtReserves.Rows[i]["nPatientID"]));
                        c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(_dtReserves.Rows[i]["PatientName"]));//Patient or Insurance Name

                        string _originalPayment = "";
                        _originalPayment = ((EOBPaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])).ToString() +"# "+ Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dtReserves.Rows[i]["nCheckDate"])).ToString("MM/dd/yyyy") +" $ "+ Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
                        c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT,_originalPayment);//Check Number,Date,Amount

                        c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));
                        c1Reserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                        c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note
                        c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(_dtReserves.Rows[i]["UsedReserve"]));//Used amount
                        c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
                        c1Reserve.SetData(_rowIndex, COL_REFUND,0);//Current amount to use from avaiable amount

                        c1Reserve.SetData(_rowIndex, COL_PAYMODE, ((EOBPaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])));
                        c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID,Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_REFEOBPAYDTLID,Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID,Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nAccountID"]));
                        c1Reserve.SetData(_rowIndex, COL_ACCOUNTTYPE,Convert.ToInt32(_dtReserves.Rows[i]["nAccountType"]));
                        c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTID,Convert.ToInt64(_dtReserves.Rows[i]["nMSTAccountID"]));
                        c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTTYPE,Convert.ToInt32(_dtReserves.Rows[i]["nMSTAccountType"]));
                                                                                                            
                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentDetailID"]));
                        //Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"])
                        //
                        //

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
            CreditCards oCreditCards = new CreditCards(_databaseconnectionstring);
            DataTable _dtCards = null;

            try
            {
              //  cmbCardType.Items.Clear();
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

           
        #endregion " Private & Public Methods "

    

    }
}