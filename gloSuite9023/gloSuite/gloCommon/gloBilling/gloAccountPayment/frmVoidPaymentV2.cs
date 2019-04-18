using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmVoidPaymentV2 : Form
    {
        
        #region "Variable Declarations"
        private Int64 _EOBPaymentID = 0;
        public  string _UserName = "";
        private Int64 _ncloseDate = 0;
        public string _VoidType = string.Empty;
        private string _messageBoxcaption = "";
        public bool oDialogResult = false;
        private Int64 _nVoidedTrayID = 0;
        private Int64 _nUserID = 0;
        private string _sUserName = "";
        private DateTime _nVoidCloseDate = DateTime.Now;
        private string _sVoidedTrayName = "";
        private string _sVoidType = "";
        private string _sVoidTrayCode = "";
        private string _sVoidNotes = "";
        private ToolTip _tlTip = new ToolTip();
        Label label;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
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

        public Int64 UserID
        {
            get { return _nUserID; }
            set
            {
                _nUserID = value;
            }
        }

        public string UserName
        {
            get { return _sUserName; }
            set
            {
                _sUserName = value;
            }
        }


        public DateTime VoidCloseDate
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

        public string VoidType
        {
            get { return _sVoidType; }
            set
            {
                _sVoidType = value;
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

        public frmVoidPaymentV2(Int64 EOBPaymentID)
        {
            InitializeComponent();
            _EOBPaymentID = EOBPaymentID;
            _VoidType = VoidType;
            
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

        }

        public frmVoidPaymentV2(Int64 EOBPaymentID,bool IsForCleargage=false)
        {
            InitializeComponent();
            _EOBPaymentID = EOBPaymentID;
            _VoidType = VoidType;
            if (IsForCleargage)
            {
                try
                {
                    #region "Get Close Date"
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    DataTable _dt = null;
                    // DataTable _dtSplit;
                    string strQuery = "";
                    oDB.Connect(false);

                    //Bug #52673: 00000487 : Reporting
                    //Description: Financial summary and monthly close report was not match.
                    //Commented the query return close date of reserve created and write new return last payment date for the created reserve.

                    //Check For Payment Close Date ,If No Payment is done then Get the Charges Close Date
                    //strQuery = " SELECT dbo.CONVERT_DateAsNumber(Credits.dtCloseDate) as CloseDate FROM Credits WITH (NOLOCK) LEFT OUTER JOIN " +
                    //              " Debits WITH (NOLOCK) ON Credits.nCreditID = Debits.nCreditID " +
                    //              " where Credits.nCreditID = " + _EOBPaymentID;

                    //Bug #58368: V7022 - gloPM - Exception on Insurance Payment Void which is not allocated - Found during Support Activity.
                    //Description: if create check with wrong information and void the check without using or allocating to any claim throws exception.
                    //union condition is added on credit table for only check created but not used anywhere and void the check.

                    strQuery = "SELECT MAX(CloseDate) AS CloseDate FROM " +
                               "( SELECT MAX(dbo.CONVERT_DateAsNumber(Reserves.dtCloseDate)) AS CloseDate FROM Reserves WITH (NOLOCK) WHERE nCreditID= " + _EOBPaymentID +
                               "  UNION SELECT MAX(dbo.CONVERT_DateAsNumber(Credits_DTL.dtCloseDate)) AS CloseDate FROM dbo.Credits_DTL WITH (NOLOCK)  WHERE nCreditsRef_ID= " + _EOBPaymentID +
                               "  UNION SELECT MAX(dbo.CONVERT_DateAsNumber(Debits.dtCloseDate)) AS CloseDate FROM dbo.Debits WITH (NOLOCK)  WHERE nCredit_RefID= " + _EOBPaymentID +
                               "  UNION SELECT MAX(dbo.CONVERT_DateAsNumber(Credits.dtCloseDate)) AS CloseDate FROM dbo.Credits WITH (NOLOCK)  WHERE nCreditID= " + _EOBPaymentID +
                               " )Temp";

                    oDB.Retrive_Query(strQuery, out _dt);

                    if (_dt == null || _dt.Rows.Count == 0)
                    {
                        strQuery = "select distinct nTransactionDate as CloseDate from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionMasterID= " + _EOBPaymentID + "";
                        //_ncloseDate = gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString());
                        //mskCloseDate.Text = gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToString("MM/dd/yyyy"); 
                        oDB.Retrive_Query(strQuery, out _dt);
                    }

                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        mskCloseDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dt.Rows[0]["CloseDate"])).ToString("MM/dd/yyyy");
                        _ncloseDate = Convert.ToInt64(_dt.Rows[0]["CloseDate"]);
                    }

                    oDB.Disconnect();
                #endregion "Get Close Date"
                    FillPaymentTray();
                }
                catch (Exception EX)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), true);
                }
            }

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

        }
        
        #endregion

        #region "Form Load "

        private void frmVoidPayment_Load(object sender, EventArgs e)
        {
            txtNotes.Text = "";
            txtNotes.Select();
            #region "Get Close Date"
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dt = null;
            // DataTable _dtSplit;
            string strQuery = "";
            oDB.Connect(false);

            //Bug #52673: 00000487 : Reporting
            //Description: Financial summary and monthly close report was not match.
            //Commented the query return close date of reserve created and write new return last payment date for the created reserve.

            //Check For Payment Close Date ,If No Payment is done then Get the Charges Close Date
            //strQuery = " SELECT dbo.CONVERT_DateAsNumber(Credits.dtCloseDate) as CloseDate FROM Credits WITH (NOLOCK) LEFT OUTER JOIN " +
            //              " Debits WITH (NOLOCK) ON Credits.nCreditID = Debits.nCreditID " +
            //              " where Credits.nCreditID = " + _EOBPaymentID;
            
            //Bug #58368: V7022 - gloPM - Exception on Insurance Payment Void which is not allocated - Found during Support Activity.
            //Description: if create check with wrong information and void the check without using or allocating to any claim throws exception.
            //union condition is added on credit table for only check created but not used anywhere and void the check.

            strQuery = "SELECT MAX(CloseDate) AS CloseDate FROM " +
                       "( SELECT MAX(dbo.CONVERT_DateAsNumber(Reserves.dtCloseDate)) AS CloseDate FROM Reserves WITH (NOLOCK) WHERE nCreditID= " + _EOBPaymentID +
                       "  UNION SELECT MAX(dbo.CONVERT_DateAsNumber(Credits_DTL.dtCloseDate)) AS CloseDate FROM dbo.Credits_DTL WITH (NOLOCK)  WHERE nCreditsRef_ID= " + _EOBPaymentID +
                       "  UNION SELECT MAX(dbo.CONVERT_DateAsNumber(Debits.dtCloseDate)) AS CloseDate FROM dbo.Debits WITH (NOLOCK)  WHERE nCredit_RefID= " + _EOBPaymentID +
                       "  UNION SELECT MAX(dbo.CONVERT_DateAsNumber(Credits.dtCloseDate)) AS CloseDate FROM dbo.Credits WITH (NOLOCK)  WHERE nCreditID= " + _EOBPaymentID +
                       " )Temp";

            oDB.Retrive_Query(strQuery, out _dt);

            if (_dt == null || _dt.Rows.Count == 0)
            {
                strQuery = "select distinct nTransactionDate as CloseDate from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionMasterID= " + _EOBPaymentID + "";
                //_ncloseDate = gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString());
                //mskCloseDate.Text = gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToString("MM/dd/yyyy"); 
                oDB.Retrive_Query(strQuery, out _dt);
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                mskCloseDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dt.Rows[0]["CloseDate"])).ToString("MM/dd/yyyy");
                _ncloseDate = Convert.ToInt64(_dt.Rows[0]["CloseDate"]);
            }

            oDB.Disconnect();
            #endregion "Get Close Date"
            FillPaymentTray();
            
            }
            catch (Exception EX)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(),true);
            }
            //SetPaymentTrayPopup();
        }

        private void frmVoidPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        #endregion

        #region " Tool Strip Events "

        private void btnSelectChargeTry_Click(object sender, EventArgs e)
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

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            try
            {
                //This Function is used to Validate the Void close date,Void Tray and Notes
                isValid = ValidateVoidClaim(); //Returns true When no Pblms found
                if (isValid)
                {
                    _sVoidType = _VoidType;
                    _sVoidNotes = txtNotes.Text.Trim();
                    VoidCloseDate = Convert.ToDateTime(mskCloseDate.Text);
                    VoidTrayID = Convert.ToInt64(lblPaymentTray.Tag);
                    VoidTrayName = Convert.ToString(lblPaymentTray.Text);
                    UserID = gloGlobal.gloPMGlobal.UserID;
                    UserName = _UserName;
                    oDialogResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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
                            MessageBox.Show("Please enter the valid close date. ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Please enter the close date.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Please enter the valid close date.  ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
        }
        #endregion "Close date validation"

        private void SetPaymentTrayPopup()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {
                if (lblPaymentTray.Tag != null && lblPaymentTray.Tag.ToString().Trim().Length > 0)
                {
                    _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK) " +
                   " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                   " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloGlobal.gloPMGlobal.UserID + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                    {
                        _defaultTrayId = Convert.ToInt64(_retVal);

                        #region " Set last selected tray "

                        //...Check if the last selected tray is same as the default tray if yes set the 
                        //...last selected tray or else show pop to select the tray

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        Object _retSettingValue = null;
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, out _retSettingValue);
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
                                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                oDB.Connect(false);
                                _retVal = new object();
                                _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray  WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
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
                        }
                        else
                        {
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                            oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                            oDB.Connect(false);
                            _retVal = new object();
                            _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
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

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        Object _retSettingValue = null;
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, out _retSettingValue);
                        oSettings.Dispose();

                        if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                        {
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                            oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                            oDB.Connect(false);
                            _retVal = new object();
                            _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + Convert.ToInt64(_retSettingValue) + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
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
        private void FillPaymentTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(gloGlobal.gloPMGlobal.DatabaseConnectionString);
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

                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                Object _retSettingValue = null;
                oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { _lastselectedTrayId = Convert.ToInt64(_retSettingValue); }

                #endregion " .... Get the last selected Payment tray ... "

                #region " ... Get the default Payment Tray .... "

                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK) " +
               " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
               " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloGlobal.gloPMGlobal.UserID + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "";
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _defaultTrayId = Convert.ToInt64(_retVal); }

                #endregion " ... Get the default Payment Tray .... "

                //...Load the last selected tray if present or else load the default tray
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oDB.Connect(false);
                _retVal = new object();
                if (_lastselectedTrayId > 0)
                {
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray  WITH (NOLOCK) WHERE nCloseDayTrayID = " + _lastselectedTrayId + " and ISNULL(bIsActive,0) = 1 AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
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
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray  WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
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
            int _addDays = 0;
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
            try
            {

                if (strDate.Trim() == "")
                {
                    MessageBox.Show("Please enter the close date.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;

                }
                if (IsValidDate(mskCloseDate.Text.Trim()) == false)
                {
                    MessageBox.Show("Please enter the valid close date.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }

                if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select(); mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }

                if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                {
                    MessageBox.Show("Void close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }
                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                {
                    MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskCloseDate.Focus();
                    mskCloseDate.Select();
                    Success = false;
                    return Success;
                }
                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
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
                    }
                }
                if (lblPaymentTray.Tag == null || lblPaymentTray.Tag.ToString().Trim() == "" || Convert.ToInt64(lblPaymentTray.Tag) <= 0)
                {
                    MessageBox.Show("Please select the payment tray.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSelectPaymentTray.Select();
                    btnSelectPaymentTray.Focus();
                    Success = false;
                    return Success;
                }
                if (txtNotes.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the Notes.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNotes.Focus();
                    txtNotes.Select();
                    Success = false;
                    return Success;
                }
                else
                {
                    Success = true;
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
            return Success;
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
        private void lblPaymentTray_MouseMove(object sender, MouseEventArgs e)
        {
            //try
            //{

            //    label = (Label)sender;

            //    if (lblPaymentTray.Text != null && lblPaymentTray.Text != "")
            //    {
            //        //if (getWidthofListItems(Convert.ToString(lblPaymentTray.Text), lblPaymentTray) >= lblPaymentTray.Width - 20)
            //        if (lblPaymentTray.Text.Length >= 17)
            //        {
            //            //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
            //            _tlTip.SetToolTip(lblPaymentTray, lblPaymentTray.Text);
            //        }
            //        else
            //        {
            //            this._tlTip.Hide(lblPaymentTray);
            //        }
            //    }
            //    else
            //    {
            //        this._tlTip.Hide(lblPaymentTray);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            //    Ex = null;
            //}
        }

        private void lblPaymentTray_MouseLeave(object sender, EventArgs e)
        {
            this._tlTip.Hide(lblPaymentTray);
        }
        //Added By Mitesh To Resolved bug 4732 (Flickering ToolTip)
        private void lblPaymentTray_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                label = (Label)sender;
                _tlTip.RemoveAll();
                if (lblPaymentTray.Text != null && lblPaymentTray.Text != "")
                {
                    //if (getWidthofListItems(Convert.ToString(lblPaymentTray.Text), lblPaymentTray) >= lblPaymentTray.Width - 20)
                    if (lblPaymentTray.Text.Length >= 17)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        _tlTip.SetToolTip(lblPaymentTray, lblPaymentTray.Text);
                    }
                    else
                    {
                        this._tlTip.Hide(lblPaymentTray);
                    }
                }
                else
                {
                    this._tlTip.Hide(lblPaymentTray);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

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