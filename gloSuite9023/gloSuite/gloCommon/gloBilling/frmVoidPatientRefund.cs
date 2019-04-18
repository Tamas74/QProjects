using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmVoidPatientRefund : Form
    {
        
        #region "Variable Declarations"

        private string _databaseconnectionstring = "";
        Int64 _ClinicID = 1;
        public Int64 _UserID = 0;
     //   private Int64 _EOBPaymentID = 0;
        public  string _UserName = "";
        private Int64 _ncloseDate = 0;
        Int64 _PatientID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
   //     gloPatientStripControl.gloPatientStripControl oPatientControl = null;
        private string _messageBoxcaption = "";
        public bool oDialogResult = false;
        private Int64 _nVoidedTrayID = 0;
        private int _nVoidCloseDate = 0;
        private string _sVoidedTrayName = "";
        private string _sVoidTrayCode = "";
        private string _sVoidNotes = "";
        private ToolTip _tlTip = new ToolTip();
        Int64 _refundID=0;

        #endregion

        #region "Properties"
        public Int64 VoidTrayID
        {
            get { return _nVoidedTrayID; }
            set
            {
                _nVoidedTrayID = value;
            }
        }

        public int VoidCloseDate
        {
            get { return _nVoidCloseDate; }
            set
            {
                _nVoidCloseDate = value;
            }
        }

        public string VoidTrayName
        {
            get { return _sVoidedTrayName; }
            set
            {
                _sVoidedTrayName = value;
            }
        }

        public string VoidTrayCode
        {
            get { return _sVoidTrayCode; }
            set
            {
                _sVoidTrayCode = value;
            }
        }

        public string VoidNotes
        {
            get { return _sVoidNotes; }
            set { _sVoidNotes = value;}
        }
        #endregion "Properties"

        #region "Constructor"

        public frmVoidPatientRefund(Int64 CloseDate,Int64 refundID,Int64 PatientID)
        {
            InitializeComponent();
            _ncloseDate = CloseDate;
            _PatientID = PatientID;
            _refundID=refundID;
            
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
                    _messageBoxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxcaption = "gloPM"; ;
                }
            }
            else
            { _messageBoxcaption = "gloPM"; ; }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
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


           
           
        }
        
        #endregion

        #region "Form Load "

        private void frmVoidPatientPayment_Load(object sender, EventArgs e)
        {
            txtNotes.Text = "";
            txtNotes.Select();
           
            try
            {
                 #region "Set Close Date"

                SetCloseDate();

                #endregion 

                 FillPaymentTray();
            
            }
            catch (Exception EX)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(),true);
            }
            //SetPaymentTrayPopup();
        }

        private void frmVoidPatientPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        #endregion

        #region " Tool Strip Events "

        private void btnSelectChargeTry_Click(object sender, EventArgs e)
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

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //This Function is used to Validate the Void close date,Void Tray and Notes
                isValid = ValidateVoidClaim();
                if (isValid)
                {
                    _sVoidNotes = txtNotes.Text.Trim();

                    string _sqlQuery = " select nEOBPaymentID from BL_EOBPatient_Refund WITH (NOLOCK) where nrefundID=" + _refundID + " ";
                    ODB.Connect(false);
                    DataTable _dt = new DataTable();
                    ODB.Retrive_Query(_sqlQuery, out _dt);
                    Int64 _EOBPaymentID = 0;
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        _EOBPaymentID = Convert.ToInt64(_dt.Rows[0]["nEOBPaymentID"]);
                    }

                    if (_EOBPaymentID > 0)
                    {
                        if (mskCloseDate.MaskCompleted == true)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        }
                        VoidPatientPayment(_EOBPaymentID, _PatientID, "", mskCloseDate.Text.ToString(), txtNotes.Text.Trim(), gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()), Convert.ToInt64(lblPaymentTray.Tag), "", lblPaymentTray.Text.Trim(), _refundID);
                    }

                    oDialogResult = true;
                    if (_dt != null)
                    {
                        _dt.Dispose();
                        _dt = null;
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                    ODB = null;
                }
            }
        }


        #endregion

        #region "Close date validation"

        private void mskCloseDate_Validating(object sender, CancelEventArgs e) {
            try
            {
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
                            MessageBox.Show("Enter valid close date. ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                        else if (mskCloseDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        {
                            if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                            {
                                MessageBox.Show("Void close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                    }
                    else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                    {
                        MessageBox.Show("Enter close date. ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Enter valid close date. ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion "Close date validation"

        #region " Private and Public function"

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
                   " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + _UserID + " AND nClinicID = " + _ClinicID + "";

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
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserID, _ClinicID, out _retSettingValue);
                        oSettings.Dispose();

                        if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                        {
                            if (Convert.ToString(_retSettingValue).Trim() == _defaultTrayId.ToString())
                            {
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                    oDB.Dispose();
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
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserID, _ClinicID, out _retSettingValue);
                        oSettings.Dispose();

                        if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                        {
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                
                if (ogloValidateUser != null)
                {
                    ogloValidateUser.Dispose();
                    ogloValidateUser = null;
                }
            }
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

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                        Object _retValue = null;
                        string _clsDate = "";
                        oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", _UserID, _ClinicID, out _retValue);
                        oSettings.Dispose();

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
                            //gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                            if (ogloBilling.IsDayClosed(Convert.ToDateTime(_clsDate.Trim())) == true)
                            { _clsDate = ""; }
                            ogloBilling.Dispose();
                        }

                        mskCloseDate.Text = _clsDate;

                        #endregion " Set last selected close date "
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        //    Int64 _nVoidTrayID = 0;
            try
            {
                //if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
                //{
                #region " .... Get the last selected Payment tray ... "

                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                Object _retSettingValue = null;
                oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserID, _ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { _lastselectedTrayId = Convert.ToInt64(_retSettingValue); }

                #endregion " .... Get the last selected Payment tray ... "

                #region " ... Get the default Payment Tray .... "

                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK) " +
               " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
               " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + _UserID + " AND nClinicID = " + _ClinicID + "";
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
                //if (mskCloseDate.Text.ToString() != "  /  /") { _ncloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()); }
                //else { _ncloseDate = gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()); }//gloDateMaster.gloDate.DateAsNumber(); }
                //_nVoidTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString());
               
                if (lblPaymentTray.Tag != null && lblPaymentTray.Tag.ToString() != "")
                { VoidTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString()); }
                VoidTrayCode = "";
                VoidTrayName = lblPaymentTray.Text.Trim();
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

        private bool ValidateVoidClaim()
        {
            mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskCloseDate.Text;
            mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            bool Success = false; 
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            //if (strDate.Length > 0)
            {

                if (strDate.Trim() == "")
                {
                    MessageBox.Show("Enter close date. ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;
 
                }
                else if (IsValidDate(mskCloseDate.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter valid close date. ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }

                else if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select(); mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }

                else if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                {
                    MessageBox.Show("Void close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }

                else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(7))
                {
                    MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskCloseDate.Focus();
                    mskCloseDate.Select();
                    Success = false;
                    return Success;
                }
           


                else if (lblPaymentTray.Tag == null || lblPaymentTray.Tag.ToString().Trim() == "" || Convert.ToInt64(lblPaymentTray.Tag) <= 0)
                {
                    MessageBox.Show("Select payment tray. ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSelectPaymentTray.Select();
                    btnSelectPaymentTray.Focus();
                    Success = false;
                    return Success;
                }
                
                
                else if (txtNotes.Text.Trim() == "")
                {
                    MessageBox.Show("Enter note. ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNotes.Focus();
                    txtNotes.Select();
                    Success = false;
                    return Success;
                }

                else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                {
                    DialogResult _dlgCloseDate = DialogResult.None;
                    _dlgCloseDate = MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", _messageBoxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (_dlgCloseDate == DialogResult.Cancel)
                    {
                        mskCloseDate.Focus();
                        mskCloseDate.Select();
                        Success = false;
                        return Success;
                    }
                    else
                    {
                        Success = true;
                        return Success;
                    }
                }
                else
                {
                    Success = true;
                }
            }
            ogloBilling.Dispose();
            return Success;
        }

        private bool VoidRefund(Int64 refundID)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 VoidCloseDate = 0;

            if (mskCloseDate.MaskCompleted == true)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                VoidCloseDate =gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString());
            }

            try
            {
                oDB.Connect(false);
                oParameters.Add("@refundID", _refundID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsUpdated", 0, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@bIsVoid", 1, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nVoidType", VoidType.PatientPaymentRefundVoid.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nVoidEntry", VoidType.PatientPaymentRefundVoidEntry.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@bIsPaymentVoid", 1, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPaymentVoidTrayID", Convert.ToInt64(lblPaymentTray.Tag), ParameterDirection.Input, SqlDbType.BigInt);


                oParameters.Add("@nVoidUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sVoidUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);
                //oParameters.Add("@sVoidUserName", gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sVoidNote", txtNotes.Text.Trim(), ParameterDirection.Input, SqlDbType.NVarChar,255);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                


                oDB.Execute("BL_INUP_VOID_REFUND", oParameters);

                
            }
            catch //(Exception ex)
            {
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
            return _result;
        }

        public Int64 VoidPatientPayment(Int64 EOBPaymentID, Int64 PatientId, string PatientName, string CloseDate, string VoidNote, int VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName, Int64 refundID)
        {
            System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseconnectionstring);
            System.Data.SqlClient.SqlCommand _sqlCommand = null;
            System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
            EOBPayment.Common.EOBPatientPaymentDetails EOBPatientPaymentDtls = new EOBPayment.Common.EOBPatientPaymentDetails();
            EOBPayment.Common.EOBPatientPaymentDetail EOBPatPayDtl = null;
            EOBPayment.Common.PaymentPatientLines PaymentPatientEOBLines = new global::gloBilling.EOBPayment.Common.PaymentPatientLines();
        //    EOBPayment.Common.PaymentPatientLine PaymentPatientEOBLine = null;
            EOBPayment.Common.PaymentPatient PaymentPatientMST = new global::gloBilling.EOBPayment.Common.PaymentPatient();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            object _retVal = null;
         //   object _valRet = null;
            Int64 _EOBPayId = 0;
            Int64 _EOBPayDtlId = 0;
            string _sqlQuery = "";
        //    bool _UseExtEOBID = false;
            try
            {
                if (EOBPatientPaymentDtls != null)
                {
                    _sqlConnection.Open();
                    _sqlTransaction = _sqlConnection.BeginTransaction();

                    


                    PaymentPatientMST = GetMasterDetailsForPatientPaymentVoid(EOBPaymentID, _ClinicID, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName);
                    #region " Master Data Save "
                    if (PaymentPatientMST != null)
                    {
                        //nEOBPaymentID,nEOBRefNO,sPayerName,nPayerID,nPayerType,nPaymentMode,sCheckNumber,nCheckAmount,nCheckDate
                        //nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayName,nCloseDate,sCardType,sCardSecurityNo
                        //nCardID,nUserID,sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID

                        oParameters.Clear();

                        oParameters.Add("@sPaymentNo", PaymentPatientMST.PaymentNumber, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sPaymentNoPrefix", PaymentPatientMST.PaymentNumberPefix, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nEOBRefNO", PaymentPatientMST.EOBRefNO, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sPayerName", PaymentPatientMST.PayerName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Unchecked
                        oParameters.Add("@nPayerID", PaymentPatientMST.PayerID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nPayerType", PaymentPatientMST.PayerType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentMode", PaymentPatientMST.PaymentMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@sCheckNumber", PaymentPatientMST.CheckNumber, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Unchecked
                        oParameters.Add("@nCheckAmount", PaymentPatientMST.CheckAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nCheckDate", PaymentPatientMST.CheckDate, ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nMSTAccountID", PaymentPatientMST.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@nMSTAccountType", PaymentPatientMST.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int	Unchecked
                        oParameters.Add("@nPaymentTrayID", PaymentPatientMST.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sPaymentTrayCode", PaymentPatientMST.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                        oParameters.Add("@sPaymentTrayDescription", PaymentPatientMST.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255
                        oParameters.Add("@nCloseDate", PaymentPatientMST.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sCardType", PaymentPatientMST.CardType, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@sCardSecurityNo", PaymentPatientMST.CardSecurityNo, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100)	Checked
                        oParameters.Add("@nCardID", PaymentPatientMST.CardID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sAuthorizationNo", PaymentPatientMST.AuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                        oParameters.Add("@nCardExpDate", PaymentPatientMST.CardExpiryDate, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                        oParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                        oParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtCreatedDateTime", PaymentPatientMST.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@dtModifiedDateTime", PaymentPatientMST.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	numeric(18, 0)	Checked
                        oParameters.Add("@nClinicID", PaymentPatientMST.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Unchecked
                        oParameters.Add("@bIsVoid", PaymentPatientMST.IsVoid, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nVoidCloseDate", PaymentPatientMST.VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nVoidTrayID", PaymentPatientMST.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);

                        oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);

                        oParameters.Add("@nVoidType", PaymentPatientMST.VoidType, ParameterDirection.Input, SqlDbType.Int);
                        oParameters.Add("@nVoidRefPaymentID", PaymentPatientMST.VoidRefPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "BL_INUP_EOBPayment_MST_PatPayment";

                        int _result = 0;
                        _result = _sqlCommand.ExecuteNonQuery();

                        if (_sqlCommand.Parameters["@nEOBPaymentID"].Value != null)
                        { _retVal = _sqlCommand.Parameters["@nEOBPaymentID"].Value; }
                        else
                        { _retVal = 0; }

                        if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                        { _EOBPayId = Convert.ToInt64(_retVal); }

                    }
                    #endregion "Master Data Save"

                    #region "Master Void Payment Note"

                    //if (VoidNote != null)
                    //{
                    //    Object _RcValue = null;
                    //    _RcValue = null;
                    //    oParameters.Clear();

                    //    oParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                    //    oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    //    oParameters.Add("@nEOBVoidPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    //    oParameters.Add("@dVoidAmount", PaymentPatientMST.CheckAmount * -1, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 0),
                    //    oParameters.Add("@sNoteDescription", VoidNote.ToString(), ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0),
                    //    oParameters.Add("@nVoidNoteType", PaymentPatientMST.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),	
                    //    oParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);//	numeric(18, 0),
                    //    oParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);//	varchar(5),
                    //    oParameters.Add("@nClinicID", PaymentPatientMST.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	decimal(18, 2),
                    //    oParameters.Add("@nVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    //    oParameters.Add("@nVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    //    oParameters.Add("@sVoidTrayDescription", VoidTrayName.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    //    oParameters.Add("@sVoidTrayCode", VoidTrayCode.ToString(), ParameterDirection.Input, SqlDbType.VarChar);

                    //    _sqlCommand = new System.Data.SqlClient.SqlCommand();
                    //    _sqlCommand = GetCmdParameters(oParameters);
                    //    _sqlCommand.Connection = _sqlConnection;
                    //    _sqlCommand.Transaction = _sqlTransaction;
                    //    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    //    _sqlCommand.CommandText = "BL_INUP_EOBPaymentVoid_Notes";

                    //    int _result = 0;
                    //    _result = _sqlCommand.ExecuteNonQuery();

                    //    if (_sqlCommand.Parameters["@nID"].Value != null)
                    //    { _RcValue = _sqlCommand.Parameters["@nID"].Value; }
                    //    else
                    //    { _RcValue = 0; }

                    //}


                    #endregion "Master Void Payment Note"

                    //Commmented
                    #region "EOB Data save for voiding patient payment "

                    //PaymentPatientEOBLines = GetEOBLinesDetailForPatientPaymentVoid(EOBPaymentID, PatientId, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName);

                    //if (EOBPaymentID > 0 && PaymentPatientEOBLines != null && PaymentPatientEOBLines.Count > 0)
                    //{
                    //    for (int _payVoidEOBLineIndex = 0; _payVoidEOBLineIndex < PaymentPatientEOBLines.Count; _payVoidEOBLineIndex++)
                    //    {
                    //        if (PaymentPatientEOBLines[_payVoidEOBLineIndex] != null)
                    //        {
                    //            PaymentPatientEOBLine = PaymentPatientEOBLines[_payVoidEOBLineIndex];
                    //            oParameters.Clear();

                    //            oParameters.Add("@nEOBID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                    //            oParameters.Add("@nEOBDtlID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//
                    //            oParameters.Add("@nEOBPaymentID", PaymentPatientEOBLine.mEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0)
                    //            oParameters.Add("@nClaimNo", PaymentPatientEOBLine.ClaimNumber, ParameterDirection.Input, SqlDbType.Int);//	int
                    //            oParameters.Add("@nDOSFrom", PaymentPatientEOBLine.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                    //            oParameters.Add("@nDOSTo", PaymentPatientEOBLine.DOSTo, ParameterDirection.Input, SqlDbType.BigInt);//	int
                    //            oParameters.Add("@sCPTCode", PaymentPatientEOBLine.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                    //            oParameters.Add("@sCPTDescription", PaymentPatientEOBLine.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                    //            if (PaymentPatientEOBLine.IsNullCharges == false)
                    //            {
                    //                oParameters.Add("@dCharges", PaymentPatientEOBLine.Charges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullUnit == false)
                    //            {
                    //                oParameters.Add("@dUnit", PaymentPatientEOBLine.Unit, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dUnit", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 2)	numeric(18, 0)
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullTotalCharges == false)
                    //            {
                    //                oParameters.Add("@dTotalCharges", PaymentPatientEOBLine.TotalCharges, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dTotalCharges", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullAllowed == false)
                    //            {
                    //                oParameters.Add("@dAllowed", PaymentPatientEOBLine.Allowed, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dAllowed", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullWriteOff == false)
                    //            {
                    //                oParameters.Add("@dWriteOff", PaymentPatientEOBLine.WriteOff, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dWriteOff", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullNonCovered == false)
                    //            {
                    //                oParameters.Add("@dNotCovered", PaymentPatientEOBLine.NonCovered, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dNotCovered", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullInsurance == false)
                    //            {
                    //                oParameters.Add("@dPayment", PaymentPatientEOBLine.InsuranceAmount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dPayment", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullCopay == false)
                    //            {
                    //                oParameters.Add("@dCopay", PaymentPatientEOBLine.Copay, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dCopay", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullDeductible == false)
                    //            {
                    //                oParameters.Add("@dDeductible", PaymentPatientEOBLine.Deductible, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dDeductible", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullCoInsurance == false)
                    //            {
                    //                oParameters.Add("@dCoInsurance", PaymentPatientEOBLine.CoInsurance, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dCoInsurance", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)	
                    //            }

                    //            if (PaymentPatientEOBLine.IsNullWithhold == false)
                    //            {
                    //                oParameters.Add("@dWithhold", PaymentPatientEOBLine.Withhold, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }
                    //            else
                    //            {
                    //                oParameters.Add("@dWithhold", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                    //            }

                    //            oParameters.Add("@nPaymentTrayID", PaymentPatientEOBLine.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                    //            oParameters.Add("@sPaymentTrayCode", PaymentPatientEOBLine.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)	Checked
                    //            oParameters.Add("@sPaymentTrayDescription", PaymentPatientEOBLine.PaymentTrayDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                    //            oParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)	Checked
                    //            oParameters.Add("@sUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)	Checked
                    //            oParameters.Add("@dtCreatedDateTime", PaymentPatientEOBLine.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                    //            oParameters.Add("@dtModifiedDateTime", PaymentPatientEOBLine.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime	Checked
                    //            oParameters.Add("@nPatientID", PaymentPatientEOBLine.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                    //            oParameters.Add("@nInsuraceID", PaymentPatientEOBLine.PatInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                    //            oParameters.Add("@nContactID", PaymentPatientEOBLine.InsContactID, ParameterDirection.Input, SqlDbType.BigInt);//	datetime	Checked
                    //            oParameters.Add("@nClinicID", PaymentPatientEOBLine.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                    //            oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                    //            oParameters.Add("@nEOBType", PaymentPatientEOBLine.EOBType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);// int,
                    //            oParameters.Add("@nBillingTransactionID", PaymentPatientEOBLine.BLTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    //            oParameters.Add("@nBillingTransactionDetailID", PaymentPatientEOBLine.BLTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    //            oParameters.Add("@nBillingTransactionLineNo", PaymentPatientEOBLine.BLTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                    //            oParameters.Add("@bUseExtEOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                    //            oParameters.Add("@nCloseDate", PaymentPatientEOBLine.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                    //            oParameters.Add("@bIsVoid", PaymentPatientEOBLine.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                    //            oParameters.Add("@nVoidCloseDate", PaymentPatientEOBLine.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                    //            oParameters.Add("@nVoidTrayID", PaymentPatientEOBLine.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),

                    //            oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                    //            oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                    //            oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),

                    //            oParameters.Add("@nVoidType", PaymentPatientEOBLine.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    //            _retVal = null;
                    //            _valRet = null;

                    //            _sqlCommand = new System.Data.SqlClient.SqlCommand();
                    //            _sqlCommand = GetCmdParameters(oParameters);
                    //            _sqlCommand.Connection = _sqlConnection;
                    //            _sqlCommand.Transaction = _sqlTransaction;
                    //            _sqlCommand.CommandType = CommandType.StoredProcedure;
                    //            _sqlCommand.CommandText = "BL_INSERT_EOBPayment_EOB_PatPayment";

                    //            int _result = 0;
                    //            _result = _sqlCommand.ExecuteNonQuery();

                    //            if (_sqlCommand.Parameters["@nEOBID"].Value != null)
                    //            { _retVal = _sqlCommand.Parameters["@nEOBID"].Value; }
                    //            else
                    //            { _retVal = 0; }

                    //            if (_sqlCommand.Parameters["@nEOBDtlID"].Value != null)
                    //            { _valRet = _sqlCommand.Parameters["@nEOBDtlID"].Value; }
                    //            else
                    //            { _valRet = 0; }
                    //        }
                    //    }
                    //}

                    #endregion " EOB Data save for voiding patient payment


                    EOBPatientPaymentDtls = GetPaymentForVoid(EOBPaymentID, PatientId, VoidCloseDate, VoidTrayID, VoidTrayCode, VoidTrayName);
                    #region " Payment Line Details save for voiding patient payment "

                    if (EOBPaymentID > 0 && EOBPatientPaymentDtls != null && EOBPatientPaymentDtls.Count > 0)
                    {
                        for (int _payVoidLineIndex = 0; _payVoidLineIndex < EOBPatientPaymentDtls.Count; _payVoidLineIndex++)
                        {
                            if (EOBPatientPaymentDtls[_payVoidLineIndex] != null)
                            {
                                EOBPatPayDtl = EOBPatientPaymentDtls[_payVoidLineIndex];
                                oParameters.Clear();
                                oParameters.Add("@nEOBPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBDtlID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionID", EOBPatPayDtl.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionDetailID", EOBPatPayDtl.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nBillingTransactionLineNo", EOBPatPayDtl.BillingTransactionLineNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nPatientID", EOBPatPayDtl.PatientID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nDOSFrom", EOBPatPayDtl.DOSFrom, ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@nDOSTo", EOBPatPayDtl.DOSTo, ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@sCPTCode", EOBPatPayDtl.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                oParameters.Add("@sCPTDescription", EOBPatPayDtl.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)

                                if (EOBPatPayDtl.IsNullAmount == false)
                                {
                                    oParameters.Add("@nAmount", EOBPatPayDtl.Amount, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }
                                else
                                {
                                    oParameters.Add("@nAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	numeric(18, 2)
                                }

                                oParameters.Add("@nPaymentType", EOBPatPayDtl.PaymentType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@nPaymentSubType", EOBPatPayDtl.PaymentSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@nPaySign", EOBPatPayDtl.PaySign.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@nPayMode", EOBPatPayDtl.PayMode.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@nAccountID", EOBPatPayDtl.AccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nAccountType", EOBPatPayDtl.AccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@nMSTAccountID", EOBPatPayDtl.MSTAccountID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nMSTAccountType", EOBPatPayDtl.MSTAccountType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int
                                oParameters.Add("@nPaymentTrayID", EOBPatPayDtl.PaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@sPaymentTrayCode", EOBPatPayDtl.PaymentTrayCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50)
                                oParameters.Add("@sPaymentTrayDescription", EOBPatPayDtl.PaymentTrayDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                oParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255)
                                oParameters.Add("@dtCreatedDateTime", EOBPatPayDtl.CreatedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                oParameters.Add("@dtModifiedDateTime", EOBPatPayDtl.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime);//	datetime
                                oParameters.Add("@nClinicID", EOBPatPayDtl.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                                if (EOBPatPayDtl.RefEOBPaymentID == 0) { EOBPatPayDtl.RefEOBPaymentID = 0; }
                                if (EOBPatPayDtl.RefEOBPaymentDetailID == 0) { EOBPatPayDtl.RefEOBPaymentDetailID = 0; }

                                oParameters.Add("@nRefEOBPaymentID", EOBPatPayDtl.RefEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nRefEOBPaymentDetailID", EOBPatPayDtl.RefEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                                oParameters.Add("@nResEOBPaymentID", EOBPatPayDtl.ReserveEOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nResEOBPaymentDetailID", EOBPatPayDtl.ReserveEOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nContactInsID", EOBPatPayDtl.ContactInsID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nCreditLineID", EOBPatPayDtl.MainCreditLineID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nEOBVoidPaymentID", _EOBPayId, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nCloseDate", EOBPatPayDtl.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnID", EOBPatPayDtl.TrackTrnID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@nTrackTrnDtlID", EOBPatPayDtl.TrackTrnDtlID, ParameterDirection.Input, SqlDbType.BigInt);  // numeric(18,0),
                                oParameters.Add("@sSubClaimNo", EOBPatPayDtl.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);  // varchar(50),

                                oParameters.Add("@bIsVoid", EOBPatPayDtl.IsVoid, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                oParameters.Add("@nVoidCloseDate", EOBPatPayDtl.VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                oParameters.Add("@nVoidTrayID", EOBPatPayDtl.VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),
                                oParameters.Add("@nVoidType", EOBPatPayDtl.VoidType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                                oParameters.Add("@bIsPaymentVoid", true, ParameterDirection.Input, SqlDbType.Bit);  // varchar(50),
                                oParameters.Add("@nPaymentVoidCloseDate", VoidCloseDate, ParameterDirection.Input, SqlDbType.Int);  // varchar(50),
                                oParameters.Add("@nPaymentVoidTrayID", VoidTrayID, ParameterDirection.Input, SqlDbType.BigInt);  // varchar(50),

                                oParameters.Add("@nOldResEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nOldResEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_INUP_EOBPayment_DTL_PatPayment";

                                int _result = _sqlCommand.ExecuteNonQuery();

                                if (_sqlCommand.Parameters["@nEOBPaymentDetailID"].Value != null)
                                { _retVal = _sqlCommand.Parameters["@nEOBPaymentDetailID"].Value; }
                                else
                                { _retVal = 0; }

                                if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                                { _EOBPayDtlId = Convert.ToInt64(_retVal); }

                                EOBPatPayDtl = null;
                            }
                        }
                    }
                    #endregion " Payment Line Details save for voiding patient payment "

                    if (_EOBPayId > 0)
                    {

                        _sqlQuery = " UPDATE BL_EOBPayment_MST WITH (READPAST) SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.PatientPaymentRefundVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " ) AND nVoidType <> " + VoidType.PatientPaymentRefundVoidEntry.GetHashCode() + " ";// OR nResEOBPaymentID = " + EOBPaymentID + " OR nRefEOBPaymentID =  "+ EOBPaymentID + " ";
                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _sqlQuery;
                        _sqlCommand.ExecuteNonQuery();

                        //_sqlQuery = "";
                        //_sqlQuery = " UPDATE BL_EOBPayment_EOB SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.PatientPaymentVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " ) AND nVoidType <> " + VoidType.PatientPaymentVoidEntry.GetHashCode() + " ";//" OR nResEOBPaymentID = " + EOBPaymentID + " OR nRefEOBPaymentID =  " + EOBPaymentID + " ";
                        //_sqlCommand = new System.Data.SqlClient.SqlCommand();
                        //_sqlCommand.Connection = _sqlConnection;
                        //_sqlCommand.Transaction = _sqlTransaction;
                        //_sqlCommand.CommandType = CommandType.Text;
                        //_sqlCommand.CommandText = _sqlQuery;
                        //_sqlCommand.ExecuteNonQuery();

                        _sqlQuery = "";
                        _sqlQuery = " UPDATE BL_EOBPayment_DTL WITH (READPAST) SET bIsPaymentVoid = 'true', nPaymentVoidCloseDate = " + VoidCloseDate + ", nPaymentVoidTrayID = " + VoidTrayID + ", nVoidType = " + VoidType.PatientPaymentRefundVoid.GetHashCode() + " WHERE ( nEOBPaymentID = " + EOBPaymentID + " OR nResEOBPaymentID = " + EOBPaymentID + " OR nRefEOBPaymentID =  " + EOBPaymentID + " ) AND nVoidType <> " + VoidType.PatientPaymentVoidEntry.GetHashCode() + " AND (nPaymentType <> " + EOBPaymentType.PatientPayment.GetHashCode() + " OR nPaymentSubType <> " + EOBPaymentSubType.Adjuestment.GetHashCode() + ") ";
                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _sqlQuery;
                        _sqlCommand.ExecuteNonQuery();

                        _sqlQuery = "";
                        _sqlQuery = "Update BL_EOBPatient_Refund WITH (READPAST) set bIsVoid=1,nVoidCloseDate=" + VoidCloseDate + ",nVoidTrayID=" + VoidTrayID + ",nVoidUserID=" + _UserID + ",sVoidUserName='" + _UserName + "',dtVoidDateTime='" + System.DateTime.Now + "',sVoidNote='" + VoidNote.Replace("'", "''").Trim() + "' where nrefundID=" + refundID + " and nClinicID=" + _ClinicID + "";
                        _sqlCommand = new System.Data.SqlClient.SqlCommand();
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _sqlQuery;
                        _sqlCommand.ExecuteNonQuery();
                    }
                    _sqlTransaction.Commit();
                    _sqlConnection.Close();

                }
            }
            catch (gloDatabaseLayer.DBException ex)
            { _sqlTransaction.Rollback(); ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { _sqlTransaction.Rollback(); gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_retVal != null) { _retVal = null; }
                if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                if (_sqlCommand != null) { _sqlCommand.Dispose(); }
                if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
                if (EOBPatPayDtl != null) { EOBPatPayDtl.Dispose(); };
                if (EOBPatientPaymentDtls != null) { EOBPatientPaymentDtls.Dispose(); }
                if (PaymentPatientMST != null) { PaymentPatientMST.Dispose(); }
            }
            return 0;
        }

        public EOBPayment.Common.PaymentPatient GetMasterDetailsForPatientPaymentVoid(Int64 EOBPaymentID, Int64 ClinicID, Int64 VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtEOBPaymentMST = new DataTable();
            EOBPayment.Common.PaymentPatient oPatientPayment = null;
            int _PaymentMode = 0;
            EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
            try
            {
                if (EOBPaymentID > 0)
                {
                    #region "Retrieve Patient Payment Master Details For Void"

                    oParameters.Clear();
                    oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Connect(false);
                    oDB.Retrive("BL_SELECT_PatientPaymentMasterDetailsForVoid", oParameters, out dtEOBPaymentMST);
                    oDB.Disconnect();
                    oParameters.Clear();

                    #endregion "Retrieve Patient Payment Master Details For Void"

                    if (dtEOBPaymentMST != null && dtEOBPaymentMST.Rows.Count > 0)
                    {
                        oPatientPayment = new global::gloBilling.EOBPayment.Common.PaymentPatient();
                        _PaymentMode = Convert.ToInt32(dtEOBPaymentMST.Rows[0]["nPaymentMode"].ToString());
                        if (_PaymentMode == 0)
                        { _EOBPaymentMode = EOBPaymentMode.None; }
                        else if (_PaymentMode == 1)
                        { _EOBPaymentMode = EOBPaymentMode.Cash; }
                        else if (_PaymentMode == 2)
                        { _EOBPaymentMode = EOBPaymentMode.Check; }
                        else if (_PaymentMode == 3)
                        { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                        else if (_PaymentMode == 4)
                        { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                        else if (_PaymentMode == 5)
                        { _EOBPaymentMode = EOBPaymentMode.EFT; }

                        oPatientPayment.PaymentNumber = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentNo"].ToString());
                        oPatientPayment.PaymentNumberPefix = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentNoPrefix"].ToString());
                        oPatientPayment.EOBPaymentID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nEOBPaymentID"].ToString());
                        oPatientPayment.EOBRefNO = Convert.ToString(dtEOBPaymentMST.Rows[0]["nEOBRefNO"].ToString());
                        oPatientPayment.PayerName = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPayerName"].ToString());
                        oPatientPayment.PayerID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nPayerID"].ToString());
                        oPatientPayment.PayerType = EOBPaymentAccountType.Patient;
                        oPatientPayment.PaymentMode = _EOBPaymentMode;
                        oPatientPayment.CheckNumber = Convert.ToString(dtEOBPaymentMST.Rows[0]["sCheckNumber"].ToString());
                        oPatientPayment.CheckAmount = Convert.ToDecimal(dtEOBPaymentMST.Rows[0]["nCheckAmount"].ToString()) * -1;
                        oPatientPayment.CheckDate = Convert.ToInt32(dtEOBPaymentMST.Rows[0]["nCheckDate"].ToString());
                        oPatientPayment.CardType = Convert.ToString(dtEOBPaymentMST.Rows[0]["sCardType"].ToString());
                        oPatientPayment.AuthorizationNo = Convert.ToString(dtEOBPaymentMST.Rows[0]["sAuthorizationNo"].ToString());
                        oPatientPayment.CardExpiryDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCardExpDate"].ToString());
                        oPatientPayment.CardID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCardID"].ToString());
                        oPatientPayment.MSTAccountID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nMSTAccountID"].ToString());
                        oPatientPayment.MSTAccountType = EOBPaymentAccountType.Patient;
                        oPatientPayment.ClinicID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nClinicID"].ToString());
                        oPatientPayment.CreatedDateTime = DateTime.Now;
                        oPatientPayment.ModifiedDateTime = DateTime.Now;
                        oPatientPayment.PaymentTrayID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nPaymentTrayID"].ToString()); ;
                        oPatientPayment.PaymentTrayCode = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentTrayCode"].ToString()); ;
                        oPatientPayment.PaymentTrayDesc = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentTrayDescription"].ToString()); ;
                        oPatientPayment.CloseDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCloseDate"].ToString());
                        oPatientPayment.UserID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nUserID"].ToString());
                        oPatientPayment.UserName = Convert.ToString(dtEOBPaymentMST.Rows[0]["sUserName"].ToString());

                        oPatientPayment.IsVoid = Convert.ToBoolean(dtEOBPaymentMST.Rows[0]["bIsVoid"].ToString());
                        oPatientPayment.VoidCloseDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nVoidCloseDate"].ToString()); ;
                        oPatientPayment.VoidRefPaymentID = EOBPaymentID;
                        oPatientPayment.VoidTrayID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nVoidTrayID"].ToString()); ;
                        oPatientPayment.VoidType = VoidType.PatientPaymentRefundVoidEntry;


                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtEOBPaymentMST != null) { dtEOBPaymentMST.Dispose(); }
            }

            return oPatientPayment;

        }

     

        public EOBPayment.Common.EOBPatientPaymentDetails GetPaymentForVoid(Int64 EOBPaymentID, Int64 PatientId, int VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtEOBPatientPayment = new DataTable();
            EOBPayment.Common.EOBPatientPaymentDetail oEOBPatientPaymentDetail = null;
            EOBPayment.Common.EOBPatientPaymentDetails oEOBPatientPaymentDetails = null;
            EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
            int _PaymentMode = 0;
            EOBPaymentSign _EOBPaymentSign = EOBPaymentSign.None;
            int _PaymentSign = 0;
            EOBPaymentType _EOBPaymentType = EOBPaymentType.None;
            int _PaymentType = 0;
            EOBPaymentSubType _EOBPaymentSubType = EOBPaymentSubType.None;
            int _PaymentSubType = 0;
            Int64 _voidTrayID = 0;
            int _voidCloseDate = 0;
            try
            {
                if (EOBPaymentID > 0)
                {
                    _voidTrayID = VoidTrayID;
                    _voidCloseDate = VoidCloseDate;
                    #region "Retrieve Patient Payment Details For Void"

                    oParameters.Clear();
                    oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Connect(false);
                    oDB.Retrive("BL_SELECT_PatientPaymentDetailsForVoid", oParameters, out dtEOBPatientPayment);
                    oDB.Disconnect();
                    oParameters.Clear();

                    #endregion "Retrieve Patient Payment Details For Void"

                    #region " Set Payment Detail Data "

                    //nEOBPaymentID,nEOBID,nEOBDtlID,nEOBPaymentDetailID,nBillingTransactionID,nBillingTransactionDetailID,nBillingTransactionLineNo,
                    //nPatientID,nDOSFrom,nDOSTo,sCPTCode,sCPTDescription,nAmount,nPaymentType,nPaymentSubType,nPaySign,nPayMode,nRefEOBPaymentID,
                    //nRefEOBPaymentDetailID,nAccountID,nAccountType,nMSTAccountID,nMSTAccountType,nPaymentTrayID,sPaymentTrayCode,sPaymentTrayDescription,nUserID,
                    //sUserName,dtCreatedDateTime,dtModifiedDateTime,nClinicID,nResEOBPaymentID,nResEOBPaymentDetailID,nCreditLineID,nContactInsID,
                    //dtDayClosedOn,nDayCloseUserID,sDayCloseUserName,bIsUpdated,bIsDayClosed,bIsVoid,nEOBVoidPaymentID,nCloseDate,nVoidCloseDate,
                    //nVoidTrayID,nTrackTrnID,nTrackTrnDtlID,nTrackTrnLineNo,sSubClaimNo,nOldRefEOBPaymentID,nOldRefEOBPaymentDetailID,nOldResEOBPaymentID,
                    //nOldResEOBPaymentDetailID,sVersion,nVoidType

                    if (dtEOBPatientPayment != null && dtEOBPatientPayment.Rows.Count > 0)
                    {
                        oEOBPatientPaymentDetails = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetails();
                        for (int _nPayDtlCounter = 0; _nPayDtlCounter < dtEOBPatientPayment.Rows.Count; _nPayDtlCounter++)
                        {
                            oEOBPatientPaymentDetail = new global::gloBilling.EOBPayment.Common.EOBPatientPaymentDetail();

                            #region "Get the debit allocation entry for voiding payment"
                            _PaymentSign = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPaySign"].ToString());
                            _PaymentType = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPaymentType"].ToString());
                            _PaymentSubType = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPaymentSubType"].ToString());
                            if (_PaymentSign == 1) { _EOBPaymentSign = EOBPaymentSign.Payment_Credit; }
                            else if (_PaymentSign == 2) { _EOBPaymentSign = EOBPaymentSign.Receipt_Debit; }
                            if (_PaymentType == 2) { _EOBPaymentType = EOBPaymentType.PatientReserved; }
                            else if (_PaymentType == 6) { _EOBPaymentType = EOBPaymentType.PatientPayment; }
                            if (_PaymentSubType == 13) { _EOBPaymentSubType = EOBPaymentSubType.Correction; }
                            else if (_PaymentSubType == 8) { _EOBPaymentSubType = EOBPaymentSubType.Patient; }

                            if (_EOBPaymentSign == EOBPaymentSign.Receipt_Debit || _EOBPaymentSubType == EOBPaymentSubType.Correction)
                            {
                                _PaymentMode = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPayMode"].ToString());
                                if (_PaymentMode == 0)
                                { _EOBPaymentMode = EOBPaymentMode.None; }
                                else if (_PaymentMode == 1)
                                { _EOBPaymentMode = EOBPaymentMode.Cash; }
                                else if (_PaymentMode == 2)
                                { _EOBPaymentMode = EOBPaymentMode.Check; }
                                else if (_PaymentMode == 3)
                                { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                                else if (_PaymentMode == 4)
                                { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
                                else if (_PaymentMode == 5)
                                { _EOBPaymentMode = EOBPaymentMode.EFT; }


                                oEOBPatientPaymentDetail.EOBPaymentID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBPaymentID"].ToString());
                                oEOBPatientPaymentDetail.EOBID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBID"].ToString());
                                oEOBPatientPaymentDetail.EOBDtlID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBDtlID"].ToString());
                                oEOBPatientPaymentDetail.EOBPaymentDetailID = GetPrefixTransactionID();

                                oEOBPatientPaymentDetail.BillingTransactionID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nBillingTransactionID"].ToString());
                                oEOBPatientPaymentDetail.BillingTransactionDetailID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nBillingTransactionDetailID"].ToString());
                                oEOBPatientPaymentDetail.BillingTransactionLineNo = Convert.ToInt32(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nBillingTransactionLineNo"].ToString());
                                oEOBPatientPaymentDetail.DOSFrom = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nDOSFrom"].ToString());
                                oEOBPatientPaymentDetail.DOSTo = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nDOSTo"].ToString());
                                oEOBPatientPaymentDetail.CPTCode = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sCPTCode"].ToString());
                                oEOBPatientPaymentDetail.CPTDescription = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sCPTDescription"].ToString());

                                oEOBPatientPaymentDetail.IsNullAmount = false;
                                decimal _fillPayAmt = Convert.ToDecimal(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nAmount"].ToString());
                                if (_EOBPaymentType == EOBPaymentType.PatientPayment || (_EOBPaymentType == EOBPaymentType.PatientPayment && _EOBPaymentSubType == EOBPaymentSubType.Correction) && _EOBPaymentSign == EOBPaymentSign.Receipt_Debit)
                                {
                                    oEOBPatientPaymentDetail.Amount = (_fillPayAmt * -1);
                                    oEOBPatientPaymentDetail.PayMode = _EOBPaymentMode;
                                }
                                else
                                {
                                    oEOBPatientPaymentDetail.Amount = _fillPayAmt;
                                    oEOBPatientPaymentDetail.PayMode = EOBPaymentMode.PaymentVoidReserved;
                                }
                                oEOBPatientPaymentDetail.PaymentType = EOBPaymentType.PatientPayment;
                                oEOBPatientPaymentDetail.PaymentSubType = EOBPaymentSubType.Patient;
                                oEOBPatientPaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;

                                oEOBPatientPaymentDetail.RefEOBPaymentID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nRefEOBPaymentID"].ToString());
                                oEOBPatientPaymentDetail.RefEOBPaymentDetailID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nRefEOBPaymentDetailID"].ToString());
                                if (_EOBPaymentType != EOBPaymentType.PatientReserved)
                                {
                                    oEOBPatientPaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nResEOBPaymentID"].ToString());
                                    oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nResEOBPaymentDetailID"].ToString());
                                }
                                else
                                {
                                    oEOBPatientPaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBPaymentID"].ToString());
                                    oEOBPatientPaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nEOBPaymentDetailID"].ToString());
                                }
                                oEOBPatientPaymentDetail.AccountID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nAccountID"].ToString());
                                oEOBPatientPaymentDetail.AccountType = EOBPaymentAccountType.Patient;
                                oEOBPatientPaymentDetail.MSTAccountID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nMSTAccountID"].ToString());
                                oEOBPatientPaymentDetail.MSTAccountType = EOBPaymentAccountType.Patient;
                                oEOBPatientPaymentDetail.PatientID = PatientId;
                                oEOBPatientPaymentDetail.PaymentTrayID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nPaymentTrayID"].ToString());
                                oEOBPatientPaymentDetail.PaymentTrayCode = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sPaymentTrayCode"].ToString());
                                oEOBPatientPaymentDetail.PaymentTrayDescription = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sPaymentTrayDescription"].ToString());
                                oEOBPatientPaymentDetail.UserID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nUserID"].ToString());
                                oEOBPatientPaymentDetail.UserName = Convert.ToString(dtEOBPatientPayment.Rows[_nPayDtlCounter]["sUserName"].ToString());
                                oEOBPatientPaymentDetail.ClinicID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nClinicID"].ToString());
                                oEOBPatientPaymentDetail.CloseDate = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nCloseDate"].ToString());
                                oEOBPatientPaymentDetail.FinanceLieNo = 0;
                                oEOBPatientPaymentDetail.MainCreditLineID = 0;
                                oEOBPatientPaymentDetail.IsMainCreditLine = false;
                                oEOBPatientPaymentDetail.IsReserveCreditLine = false;
                                oEOBPatientPaymentDetail.IsCorrectionCreditLine = false;
                                oEOBPatientPaymentDetail.RefFinanceLieNo = 1;
                                oEOBPatientPaymentDetail.UseRefFinanceLieNo = false;
                                oEOBPatientPaymentDetail.TrackTrnID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nTrackTrnID"].ToString());
                                oEOBPatientPaymentDetail.TrackTrnDtlID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nTrackTrnDtlID"].ToString());
                                oEOBPatientPaymentDetail.SubClaimNo = "";


                                oEOBPatientPaymentDetail.VoidCloseDate = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nVoidCloseDate"].ToString());
                                oEOBPatientPaymentDetail.VoidTrayID = Convert.ToInt64(dtEOBPatientPayment.Rows[_nPayDtlCounter]["nVoidTrayID"].ToString());
                                oEOBPatientPaymentDetail.IsVoid = Convert.ToBoolean(dtEOBPatientPayment.Rows[_nPayDtlCounter]["bIsVoid"].ToString());

                                oEOBPatientPaymentDetail.VoidType = VoidType.PatientPaymentRefundVoidEntry;
                                oEOBPatientPaymentDetails.Add(oEOBPatientPaymentDetail);
                            }
                            #endregion "Get the debit allocation entry for voiding payment"
                        }
                    }
                    #endregion " Set Payment Detail Data "
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oEOBPatientPaymentDetail != null) { oEOBPatientPaymentDetail.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                if (dtEOBPatientPayment != null) { dtEOBPatientPayment.Dispose(); }
            }

            return oEOBPatientPaymentDetails;
        }

        private Int64 GetPrefixTransactionID()
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            try
            {
                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            finally
            {

            }
            return _Result;
        }

        private void btnSelectPaymentTray_MouseHover(object sender, EventArgs e)
        {
            try
            {
                _tlTip.SetToolTip(btnSelectPaymentTray, "Select Payment Tray");
            }
            catch //(Exception ex)
            {
            }
        }

        #endregion

        private void mskCloseDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

    }
}