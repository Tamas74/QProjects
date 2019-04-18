using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmBillingEOBPatientRefund : Form
    {

        #region " Private Variables "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        Int64 _UserId = 0;
        string _UserName = "";
        private string _MessageBoxCaption = "";
       // private bool _IsFormLoading = false;
        private Int64 _patientId = 0;
        private gloGeneralItem.gloItems _oSeletedReserveItems = null;
        private decimal _refundAmt = 0;
        private DateTime _closeDate = DateTime.Now;
        private string _closeDayTray = "";
        private string _paymentPrefix = "GPM#";

        #endregion " Private Variables "

        #region " Property Procedures "

        public Int64 ClinicID
        { get { return _ClinicID; } set { _ClinicID = value; } }
        public Int64 UserID
        { get { return _UserId; } set { _UserId = value; } }
        public string UserName
        { get { return _UserName; } set { _UserName = value; } }
        public Int64 PatientID
        { get { return _patientId; } set { _patientId = value; } }
        public gloGeneralItem.gloItems oSeletedReserveItems
        {
            get { return _oSeletedReserveItems; }
            set { _oSeletedReserveItems = value; }
        }
        public decimal RefundAmt
        {
            get { return _refundAmt; }
            set { _refundAmt = value; }
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

        #endregion " Property Procedures "

        #region "  Constructor  "
        
        public frmBillingEOBPatientRefund(string DatabaseConnectionString, Int64 Patientid)
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

        #endregion

        #region " Form Events "

        private void frmBillingEOBPatientRefund_Load(object sender, EventArgs e)
        {


            try
            {
                FillPaymentTray();
                FillPaymentMode();

                txtRefundAmount.Text = "#0.00";
                mskCloseDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                #region " Patient Details "

                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                lblPatientNm.Text = ogloPatient.GetPatientName(_patientId);
                gloPatient.PatientOtherContacts oPatientOtherContacts = new gloPatient.PatientOtherContacts();
                oPatientOtherContacts = ogloPatient.GetGaruntorDetails(_patientId);
                if (oPatientOtherContacts != null)
                {
                    for (int i = 0; i <= oPatientOtherContacts.Count - 1; i++)
                    {
                        if (oPatientOtherContacts[i].nGuarantorTypeFlag == 1)
                        {
                            txtTo.Text = Convert.ToString(oPatientOtherContacts[i].FirstName) + " " + Convert.ToString(oPatientOtherContacts[i].MiddleName) + " " + Convert.ToString(oPatientOtherContacts[i].LastName);
                            txtAddress.Text = Convert.ToString(oPatientOtherContacts[i].AddressLine1) + " " + Convert.ToString(oPatientOtherContacts[i].AddressLine2);
                        }
                    }
                }
                else
                {
                    txtTo.Text = ogloPatient.GetPatientName(_patientId);
                }
                ogloPatient.Dispose();
                ogloPatient = null;
                if (_refundAmt > 0)
                { txtRefundAmount.Text = _refundAmt.ToString("#0.00"); }

                #endregion

                lblDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                lblUser.Text = Convert.ToString(_UserName);
                mskCloseDate.Text = CloseDate.ToString("MM/dd/yyyy");
                cmbPaymentTray.Text = CloseDayTray;

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;                
            }
           
        }

        #endregion

        #region " Tool Strip Events "

        private void tls_btnSaveNClose_Click(object sender, EventArgs e)
        {
            //gloGeneralItem.gloItems _oSeletedReserveItems = new gloGeneralItem.gloItems();
           // gloGeneralItem.gloItem ogloItem = null;

            if (SavePaymentValidation())
            {
                if (SavePatientRefund() > 0)
                {
                    this.Close();
                }

            }
        }

        private void tls_btnSave_Click(object sender, EventArgs e)
        {

        }

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
        #endregion

        #region " Form Control Events "

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
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void btnModifyJournal_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _TrayID = 0;
                if (cmbPaymentTray != null && cmbPaymentTray.DataSource != null && cmbPaymentTray.Items.Count > 0)
                {
                    if (cmbPaymentTray.SelectedValue != null && cmbPaymentTray.SelectedValue.ToString() != "")
                    {
                        _TrayID = Convert.ToInt64(cmbPaymentTray.SelectedValue.ToString());

                        frmSetupCloseDayJournals ofrmSetupCloseDayJournals = new frmSetupCloseDayJournals(_TrayID, _databaseconnectionstring);
                        ofrmSetupCloseDayJournals.StartPosition = FormStartPosition.CenterScreen;
                        ofrmSetupCloseDayJournals.ShowDialog(this);
                        ofrmSetupCloseDayJournals.Dispose();
                        FillPaymentTray();
                        cmbPaymentTray.SelectedValue = _TrayID;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

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

                 //   EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;

                    if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                    {
                        //_EOBPaymentMode = EOBPaymentMode.Cash;

                        txtCheckNumber.Text = "";
                        lblCheckNo.Text = "Ref.# :";
                        lblCheckNo.Enabled = true;
                        txtCheckNumber.Enabled = true;
                    }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                    {
                        //_EOBPaymentMode = EOBPaymentMode.Check;
                        lblCheckNo.Text = "Check# :";
                        lblCheckNo.TextAlign = ContentAlignment.MiddleCenter;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;

                    }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                    {
                        //_EOBPaymentMode = EOBPaymentMode.MoneyOrder;
                        lblCheckNo.Text = "MO# :";
                        lblCheckNo.TextAlign = ContentAlignment.MiddleCenter;
                        pnlCredit.Enabled = false;
                        txtCheckNumber.MaxLength = 15;
                    }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                    {
                      //  _EOBPaymentMode = EOBPaymentMode.CreditCard;
                        lblCheckNo.Text = "Card# :";
                        lblCheckNo.TextAlign = ContentAlignment.MiddleCenter;
                        pnlCredit.Enabled = true;
                        txtCheckNumber.MaxLength = 4;

                    }
                    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                    {
                        //_EOBPaymentMode = EOBPaymentMode.EFT;
                        lblCheckNo.Text = "EFT# :";
                        lblCheckNo.TextAlign = ContentAlignment.MiddleCenter;
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

        #endregion " Form Control Events "

        #region " Private & Public Methods "

        private void FillPaymentTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(_databaseconnectionstring);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {
                if (IsAdmin(_UserId) == true)
                {
                    _sqlQuery = "SELECT nCloseDayTrayID,sCode, " +
                        " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                        " FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                        "AND sDescription <> '' AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _ClinicID + "";
                }
                else
                {
                    _sqlQuery = "SELECT nCloseDayTrayID,sCode, " +
                    " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                    " FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                    "AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0  AND nUserID = " + _UserId + " AND nClinicID = " + _ClinicID + "";
                }

                DataTable dtCloseDayTray = new DataTable();
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtCloseDayTray);
                oDB.Disconnect();


                cmbPaymentTray.DataSource = dtCloseDayTray;
                cmbPaymentTray.ValueMember = "nCloseDayTrayID";
                cmbPaymentTray.DisplayMember = "sDescription";

                if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
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
                        cmbPaymentTray.SelectedValue = _defaultTrayId;
                    }
                    else
                    {
                        cmbPaymentTray.SelectedIndex = 0;
                    }
                }
               

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

        private bool SavePaymentValidation()
        {
            try
            {

                    if (txtTo.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the Receiver's name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTo.Focus();
                        txtTo.Select();
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
                        MessageBox.Show("Please select the payment mode", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbPayMode.Select();
                        cmbPayMode.Focus();
                        return false;
                    }
                    else if (_EOBPaymentMode == EOBPaymentMode.CreditCard)
                    {

                        if (mskCheckDate.MaskCompleted == false)
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString().ToLower() + " date", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCheckDate.Select();
                            mskCheckDate.Focus();
                            return false;
                        }

                        if (cmbCardType == null || cmbCardType.Items.Count <= 0 || cmbCardType.Text.Trim() == "")
                        {
                            MessageBox.Show("Please select the card type", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbCardType.Select();
                            cmbCardType.Focus();
                            return false;
                        }

                        mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mskCreditExpiryDate.Text != "")
                        {
                            if (mskCreditExpiryDate.MaskFull == false)
                            {
                                MessageBox.Show("Please enter valid " + _EOBPaymentMode.ToString().ToLower() + " expiration date (MM/yy).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString().ToLower() + " number", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCheckNumber.Select();
                            txtCheckNumber.Focus();
                            return false;
                        }

                        if (mskCheckDate.MaskCompleted == false)
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString().ToLower() + " date", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCheckDate.Select();
                            mskCheckDate.Focus();
                            return false;
                        }
                    }
                    else if (_EOBPaymentMode == EOBPaymentMode.EFT)
                    {
                        if (txtCheckNumber.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString().ToLower() + " number", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCheckNumber.Select();
                            txtCheckNumber.Focus();
                            return false;
                        }
                    }
                    
             
                    if (txtRefundAmount.Text.Trim() == "" || Convert.ToDecimal(txtRefundAmount.Text.Trim()) <= 0)
                    {
                        MessageBox.Show("Please enter the refund amount", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRefundAmount.Select();
                        txtRefundAmount.Focus();
                        return false;
                    }

                    

             
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            { }

            return true;
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

            try
            {
                Int64 _CloseDayTrayID = 0;
                string _CloseDayTrayCode = "";
                string _CloseDayTrayName = "";

                    #region "Validation"

                #endregion

                if (txtRefundAmount.Text.Trim().Length > 0 && Convert.ToDecimal(txtRefundAmount.Text) > 0)
                {

                    #region "Payment Tray"

                    if (cmbPaymentTray.SelectedIndex >= 0)
                    {
                        DataRowView dvr = (DataRowView)cmbPaymentTray.SelectedItem;
                        if (dvr != null)
                        {
                            _CloseDayTrayID = Convert.ToInt64(cmbPaymentTray.SelectedValue.ToString());
                            _CloseDayTrayCode = dvr.Row["sCode"].ToString();
                            _CloseDayTrayName = cmbPaymentTray.Text;
                        }
                        if (dvr != null) { dvr = null; }
                    }

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

                    //...Changes done on 20091027 by Sagar,to remove ref. no field
                    //oPaymentPatient.EOBRefNO = txtEOBRefNumber.Text.Trim();
                    if (_EOBPaymentMode == EOBPaymentMode.Cash)
                    { oPaymentPatient.EOBRefNO = txtCheckNumber.Text.Trim(); }
                    else
                    { oPaymentPatient.EOBRefNO = ""; }

                    oPaymentPatient.PayerName = "";

                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                    oPaymentPatient.PayerID = _patientId;
                    oPaymentPatient.PayerType = EOBPaymentAccountType.Patient ;
                    oPaymentPatient.PaymentMode = _EOBPaymentMode;
                    oPaymentPatient.CheckNumber = txtCheckNumber.Text.Trim(); ;
                    if (txtRefundAmount.Text.Trim() != "") { oPaymentPatient.CheckAmount = Convert.ToDecimal(txtRefundAmount.Text); }

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
                        oPaymentPatientLineNote.Amount = Convert.ToDecimal(txtRefundAmount.Text.Trim());
                        oPaymentPatientLineNote.IncludeOnPrint = false;
                        oPaymentPatientLineNote.ClinicID = _ClinicID;
                        oPaymentPatientLineNote.PaymentNoteType = EOBPaymentType.PatientPayment;
                        oPaymentPatientLineNote.PaymentNoteSubType = EOBPaymentSubType.Other;
                        oPaymentPatientLineNote.HasData = chkPayMstIncludeNotes.Checked;

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
                    oEOBPatientPaymentCreditDetail.Amount = Convert.ToDecimal(txtRefundAmount.Text);
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

                    oPaymentPatient.EOBPatientPaymentLineDetails.Add(oEOBPatientPaymentCreditDetail);

                    #endregion

                    #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"
                    //if (btnReserveRemaining.Tag != null && btnReserveRemaining.Tag.ToString().Trim().Length > 0)
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
                            if(_oSeletedReserveItems[i] != null && _oSeletedReserveItems[i].Description.Trim() != ""
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

                                oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientRefund ;
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
                                oEOBPatientPaymentDetail.PatientID = _patientId;
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
                                

                                oPaymentPatient.EOBPatientPaymentReserveLineDetail.Add(oEOBPatientPaymentDetail);
                                oEOBPatientPaymentDetail.Dispose();
                            }
                        }
                    }
                    #endregion

                  //  _retPayId = ogloEOBPaymentPatient.SavePatientRefund(oPaymentPatient,false);
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

        #endregion

    }
}