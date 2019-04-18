using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloSettings;
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmVoidInsuranceRefundV2 : Form
    {
        
        #region "Variable Declarations"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _nVoidedTrayID = 0;
        private Int64 _ncloseDate = 0;
        Int64 _PatientID = 0;
        Int64 _refundID = 0;
        private int _nVoidCloseDate = 0;

        private string _messageBoxcaption = "";
        private string _sVoidedTrayName = "";
        private string _sVoidTrayCode = "";
        private string _sVoidNotes = "";
        public string _UserName = "";

        public bool oDialogResult = false;

        private Label label;
        
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

        public frmVoidInsuranceRefundV2(Int64 CloseDate,Int64 refundID,Int64 PatientID)
        {
            InitializeComponent();
            _ncloseDate = CloseDate;
            _PatientID = PatientID;
            _refundID=refundID;

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

            gloGlobal.gloPMGlobal.DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
        }
        
        #endregion

        #region "Form Load "

        private void frmVoidInsurancePayment_Load(object sender, EventArgs e)
        {
            txtNotes.Text = "";
            txtNotes.Select();
            try
            {
                #region "Set Close Date"

                //SetCloseDate();
                mskCloseDate.Text = Convert.ToString(gloDateMaster.gloDate.DateAsDateString(_ncloseDate));

                #endregion

                //FillPaymentTray();

                string sPaymentTray = gloInsurancePaymentV2.FillPaymentTray(gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID);
                if (sPaymentTray.Trim() != "")
                {
                    lblPaymentTray.Text = Convert.ToString(sPaymentTray.Substring(0, sPaymentTray.IndexOf("~")));
                    lblPaymentTray.Tag = Convert.ToInt64(sPaymentTray.Substring(sPaymentTray.IndexOf("~") + 1));
                }
                else
                {
                    lblPaymentTray.Text = "";
                    lblPaymentTray.Tag = 0;
                }

            }
            catch (Exception EX)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), true);
            }
        }

        #endregion

        #region Form Control Events

        #region " Tool Strip Events "

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            gloAccountsV2.gloInsurancePaymentV2 InsurancePaymentV2 = null;
            try
            {
                //This Function is used to Validate the Void close date,Void Tray and Notes
                isValid = ValidateVoidClaim();
                if (isValid)
                {
                    _sVoidNotes = txtNotes.Text.Trim();
                    DataTable _dt = new DataTable();
                    _dt = gloInsurancePaymentV2.GetEOBPaymentID(_refundID);
                    Int64 _EOBPaymentID = 0;
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        //_EOBPaymentID = Convert.ToInt64(_dt.Rows[0]["nEOBPaymentID"]);
                        _EOBPaymentID = Convert.ToInt64(_dt.Rows[0]["nCreditID"]);
                    }

                    if (_EOBPaymentID > 0)
                    {
                        if (mskCloseDate.MaskCompleted == true)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        }
                        InsurancePaymentV2 = new gloInsurancePaymentV2();
                        InsurancePaymentV2.VoidInsuranceRefund(_EOBPaymentID, _PatientID, "", mskCloseDate.Text.ToString(), txtNotes.Text.Trim(), Convert.ToDateTime(mskCloseDate.Text.ToString()), Convert.ToInt64(lblPaymentTray.Tag), "", lblPaymentTray.Text.Trim().Replace("'", "''"), _refundID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.UserID);
                    }

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }

        }

        #region "Close date validation"

        private void mskCloseDate_Validating(object sender, CancelEventArgs e)
        {
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
                        MessageBox.Show("Enter close date.", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Enter valid close date.  ", _messageBoxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
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

        #endregion "Close date validation"

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

        #endregion

        #region " Private and Public function"

        private void SetPaymentTrayPopup()
        {

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
           // gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(gloGlobal.gloPMGlobal.DatabaseConnectionString);
         //   string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {
                if (lblPaymentTray.Tag != null && lblPaymentTray.Tag.ToString().Trim().Length > 0)
                {
                   // _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK) " +
                   //" WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                   //" AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloGlobal.gloPMGlobal.UserID + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "";

                   // oDB.Connect(false);
                   // _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    //oDB.Disconnect();
                    _retVal = gloInsurancePaymentV2.GetDefaultPaymentTrayID();

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
                                //oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                //oDB.Connect(false);
                                //_retVal = new object();
                                //_retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
                                _retVal = gloInsurancePaymentV2.GetPaymentTrayDescription(_defaultTrayId);
                                if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                                {
                                    lblPaymentTray.Text = _retVal.ToString(); ;
                                    lblPaymentTray.Tag = _defaultTrayId;
                                }
                                //oDB.Disconnect();

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
                            //oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                            //oDB.Connect(false);
                            _retVal = new object();
                            //_retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
                            _retVal = gloInsurancePaymentV2.GetPaymentTrayDescription(_defaultTrayId);
                            if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                            {
                                lblPaymentTray.Text = _retVal.ToString(); ;
                                lblPaymentTray.Tag = _defaultTrayId;
                            }
                            //oDB.Disconnect();
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
                            //oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                            //oDB.Connect(false);
                            _retVal = new object();
                            //_retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + Convert.ToInt64(_retSettingValue) + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
                            _retVal = gloInsurancePaymentV2.GetPaymentTrayDescription(Convert.ToInt64(_retSettingValue));
                            if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                            {
                                lblPaymentTray.Text = _retVal.ToString(); ;
                                lblPaymentTray.Tag = Convert.ToInt64(_retSettingValue);
                            }
                            //oDB.Disconnect();
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
              //  if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void SetCloseDate()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
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

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        Object _retValue = null;
                        string _clsDate = "";
                        oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, out _retValue);
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
                            //gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                            if (ogloBilling.IsDayClosed(Convert.ToDateTime(_clsDate.Trim())) == true)
                            { _clsDate = ""; }
                            ogloBilling.Dispose();
                            ogloBilling = null;
                        }

                        mskCloseDate.Text = _clsDate;
                        _ncloseDate = gloDateMaster.gloDate.DateAsNumber(_clsDate);
                        #endregion " Set last selected close date "
                    }
                }
                //_ncloseDate = _clsDate;
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
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            try
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

                else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
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


        //public EOBPayment.Common.PaymentInsurance GetMasterDetailsForPatientPaymentVoid(Int64 EOBPaymentID, Int64 ClinicID, Int64 VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    DataTable dtEOBPaymentMST = new DataTable();
        //    EOBPayment.Common.PaymentInsurance oInsurancePayment = null;
        //    int _PaymentMode = 0;
        //    EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
        //    try
        //    {
        //        if (EOBPaymentID > 0)
        //        {
        //            #region "Retrieve Patient Payment Master Details For Void"

        //            oParameters.Clear();
        //            oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
        //            oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //            oDB.Connect(false);
        //            oDB.Retrive("BL_SELECT_PatientPaymentMasterDetailsForVoid", oParameters, out dtEOBPaymentMST);
        //            oDB.Disconnect();
        //            oParameters.Clear();

        //            #endregion "Retrieve Patient Payment Master Details For Void"

        //            if (dtEOBPaymentMST != null && dtEOBPaymentMST.Rows.Count > 0)
        //            {
        //                oInsurancePayment = new global::gloBilling.EOBPayment.Common.PaymentInsurance();
        //                _PaymentMode = Convert.ToInt32(dtEOBPaymentMST.Rows[0]["nPaymentMode"].ToString());
        //                if (_PaymentMode == 0)
        //                { _EOBPaymentMode = EOBPaymentMode.None; }
        //                else if (_PaymentMode == 1)
        //                { _EOBPaymentMode = EOBPaymentMode.Cash; }
        //                else if (_PaymentMode == 2)
        //                { _EOBPaymentMode = EOBPaymentMode.Check; }
        //                else if (_PaymentMode == 3)
        //                { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
        //                else if (_PaymentMode == 4)
        //                { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
        //                else if (_PaymentMode == 5)
        //                { _EOBPaymentMode = EOBPaymentMode.EFT; }

        //                oInsurancePayment.PaymentNumber = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentNo"].ToString());
        //                oInsurancePayment.PaymentNumberPefix = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentNoPrefix"].ToString());
        //                oInsurancePayment.EOBPaymentID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nEOBPaymentID"].ToString());
        //                oInsurancePayment.EOBRefNO = Convert.ToString(dtEOBPaymentMST.Rows[0]["nEOBRefNO"].ToString());
        //                oInsurancePayment.PayerName = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPayerName"].ToString());
        //                oInsurancePayment.PayerID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nPayerID"].ToString());
        //                oInsurancePayment.PayerType = EOBPaymentAccountType.InsuranceCompany;
        //                oInsurancePayment.PaymentMode = _EOBPaymentMode;
        //                oInsurancePayment.CheckNumber = Convert.ToString(dtEOBPaymentMST.Rows[0]["sCheckNumber"].ToString());
        //                oInsurancePayment.CheckAmount = Convert.ToDecimal(dtEOBPaymentMST.Rows[0]["nCheckAmount"].ToString()) * -1;
        //                oInsurancePayment.CheckDate = Convert.ToInt32(dtEOBPaymentMST.Rows[0]["nCheckDate"].ToString());
        //                oInsurancePayment.CardType = Convert.ToString(dtEOBPaymentMST.Rows[0]["sCardType"].ToString());
        //                oInsurancePayment.AuthorizationNo = Convert.ToString(dtEOBPaymentMST.Rows[0]["sAuthorizationNo"].ToString());
        //                oInsurancePayment.CardExpiryDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCardExpDate"].ToString());
        //                oInsurancePayment.CardID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCardID"].ToString());
        //                oInsurancePayment.MSTAccountID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nMSTAccountID"].ToString());
        //                oInsurancePayment.MSTAccountType = EOBPaymentAccountType.InsuranceCompany;
        //                oInsurancePayment.ClinicID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nClinicID"].ToString());
        //                oInsurancePayment.CreatedDateTime = DateTime.Now;
        //                oInsurancePayment.ModifiedDateTime = DateTime.Now;
        //                oInsurancePayment.PaymentTrayID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nPaymentTrayID"].ToString()); ;
        //                oInsurancePayment.PaymentTrayCode = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentTrayCode"].ToString()); ;
        //                oInsurancePayment.PaymentTrayDesc = Convert.ToString(dtEOBPaymentMST.Rows[0]["sPaymentTrayDescription"].ToString()); ;
        //                oInsurancePayment.CloseDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nCloseDate"].ToString());
        //                oInsurancePayment.UserID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nUserID"].ToString());
        //                oInsurancePayment.UserName = Convert.ToString(dtEOBPaymentMST.Rows[0]["sUserName"].ToString());

        //                oInsurancePayment.IsVoid = Convert.ToBoolean(dtEOBPaymentMST.Rows[0]["bIsVoid"].ToString());
        //                oInsurancePayment.VoidCloseDate = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nVoidCloseDate"].ToString()); ;
        //                oInsurancePayment.VoidRefPaymentID = EOBPaymentID;
        //                oInsurancePayment.VoidTrayID = Convert.ToInt64(dtEOBPaymentMST.Rows[0]["nVoidTrayID"].ToString()); ;
        //                oInsurancePayment.VoidType = VoidType.InsurancePaymentRefundVoidEntry;


        //            }
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    { ex.ERROR_Log(ex.ToString()); }
        //    catch (Exception ex)
        //    { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        //    finally
        //    {
        //        if (oParameters != null) { oParameters.Dispose(); }
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (dtEOBPaymentMST != null) { dtEOBPaymentMST.Dispose(); }
        //    }

        //    return oInsurancePayment;

        //}

        //public EOBPayment.Common.EOBInsurancePaymentDetails GetPaymentForVoid(Int64 EOBPaymentID, Int64 PatientId, int VoidCloseDate, Int64 VoidTrayID, string VoidTrayCode, string VoidTrayName)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    DataTable dtEOBInsurancePayment = new DataTable();
        //    EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentDetail = null;
        //    EOBPayment.Common.EOBInsurancePaymentDetails oEOBInsurancePaymentDetails = null;
        //    EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;
        //    int _PaymentMode = 0;
        //    EOBPaymentSign _EOBPaymentSign = EOBPaymentSign.None;
        //    int _PaymentSign = 0;
        //    EOBPaymentType _EOBPaymentType = EOBPaymentType.None;
        //    int _PaymentType = 0;
        //    EOBPaymentSubType _EOBPaymentSubType = EOBPaymentSubType.None;
        //    int _PaymentSubType = 0;
        //    Int64 _voidTrayID = 0;
        //    int _voidCloseDate = 0;
        //    try
        //    {
        //        if (EOBPaymentID > 0)
        //        {
        //            _voidTrayID = VoidTrayID;
        //            _voidCloseDate = VoidCloseDate;
        //            #region "Retrieve Patient Payment Details For Void"

        //            oParameters.Clear();
        //            oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
        //            oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //            oDB.Connect(false);
        //            oDB.Retrive("BL_SELECT_PatientPaymentDetailsForVoid", oParameters, out dtEOBInsurancePayment);
        //            oDB.Disconnect();
        //            oParameters.Clear();

        //            #endregion "Retrieve Patient Payment Details For Void"

        //            #region " Set Payment Detail Data "

        //            if (dtEOBInsurancePayment != null && dtEOBInsurancePayment.Rows.Count > 0)
        //            {
        //                oEOBInsurancePaymentDetails = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetails();
        //                for (int _nPayDtlCounter = 0; _nPayDtlCounter < dtEOBInsurancePayment.Rows.Count; _nPayDtlCounter++)
        //                {
        //                    oEOBInsurancePaymentDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();

        //                    #region "Get the debit allocation entry for voiding payment"
        //                    _PaymentSign = Convert.ToInt32(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nPaySign"].ToString());
        //                    _PaymentType = Convert.ToInt32(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nPaymentType"].ToString());
        //                    _PaymentSubType = Convert.ToInt32(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nPaymentSubType"].ToString());
        //                    if (_PaymentSign == 1) { _EOBPaymentSign = EOBPaymentSign.Payment_Credit; }
        //                    else if (_PaymentSign == 2) { _EOBPaymentSign = EOBPaymentSign.Receipt_Debit; }
        //                    if (_PaymentType == 1) { _EOBPaymentType = EOBPaymentType.InsuraceReserverd; }
        //                    else if (_PaymentType == 4) { _EOBPaymentType = EOBPaymentType.InsuracePayment; }
        //                    if (_PaymentSubType == 13) { _EOBPaymentSubType = EOBPaymentSubType.Correction; }
        //                    else if (_PaymentSubType == 1) { _EOBPaymentSubType = EOBPaymentSubType.Insurace; }

        //                    if (_EOBPaymentSign == EOBPaymentSign.Receipt_Debit || _EOBPaymentSubType == EOBPaymentSubType.Correction)
        //                    {
        //                        _PaymentMode = Convert.ToInt32(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nPayMode"].ToString());
        //                        if (_PaymentMode == 0)
        //                        { _EOBPaymentMode = EOBPaymentMode.None; }
        //                        else if (_PaymentMode == 1)
        //                        { _EOBPaymentMode = EOBPaymentMode.Cash; }
        //                        else if (_PaymentMode == 2)
        //                        { _EOBPaymentMode = EOBPaymentMode.Check; }
        //                        else if (_PaymentMode == 3)
        //                        { _EOBPaymentMode = EOBPaymentMode.MoneyOrder; }
        //                        else if (_PaymentMode == 4)
        //                        { _EOBPaymentMode = EOBPaymentMode.CreditCard; }
        //                        else if (_PaymentMode == 5)
        //                        { _EOBPaymentMode = EOBPaymentMode.EFT; }


        //                        oEOBInsurancePaymentDetail.EOBPaymentID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nEOBPaymentID"].ToString());
        //                        oEOBInsurancePaymentDetail.EOBID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nEOBID"].ToString());
        //                        oEOBInsurancePaymentDetail.EOBDtlID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nEOBDtlID"].ToString());
        //                        oEOBInsurancePaymentDetail.EOBPaymentDetailID = GetPrefixTransactionID();

        //                        oEOBInsurancePaymentDetail.BillingTransactionID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nBillingTransactionID"].ToString());
        //                        oEOBInsurancePaymentDetail.BillingTransactionDetailID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nBillingTransactionDetailID"].ToString());
        //                        oEOBInsurancePaymentDetail.BillingTransactionLineNo = Convert.ToInt32(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nBillingTransactionLineNo"].ToString());
        //                        oEOBInsurancePaymentDetail.DOSFrom = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nDOSFrom"].ToString());
        //                        oEOBInsurancePaymentDetail.DOSTo = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nDOSTo"].ToString());
        //                        oEOBInsurancePaymentDetail.CPTCode = Convert.ToString(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["sCPTCode"].ToString());
        //                        oEOBInsurancePaymentDetail.CPTDescription = Convert.ToString(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["sCPTDescription"].ToString());

        //                        oEOBInsurancePaymentDetail.IsNullAmount = false;
        //                        decimal _fillPayAmt = Convert.ToDecimal(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nAmount"].ToString());
        //                        if (_EOBPaymentType == EOBPaymentType.InsuracePayment || (_EOBPaymentType == EOBPaymentType.InsuracePayment && _EOBPaymentSubType == EOBPaymentSubType.Correction) && _EOBPaymentSign == EOBPaymentSign.Receipt_Debit)
        //                        {
        //                            oEOBInsurancePaymentDetail.Amount = (_fillPayAmt * -1);
        //                            oEOBInsurancePaymentDetail.PayMode = _EOBPaymentMode;
        //                        }
        //                        else
        //                        {
        //                            oEOBInsurancePaymentDetail.Amount = _fillPayAmt;
        //                            oEOBInsurancePaymentDetail.PayMode = EOBPaymentMode.PaymentVoidReserved;
        //                        }
        //                        oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuraceReserverd;
        //                        oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.Reserved;
        //                        oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;

        //                        oEOBInsurancePaymentDetail.RefEOBPaymentID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nRefEOBPaymentID"].ToString());
        //                        oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nRefEOBPaymentDetailID"].ToString());
        //                        if (_EOBPaymentType != EOBPaymentType.InsuraceReserverd)
        //                        {
        //                            oEOBInsurancePaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nResEOBPaymentID"].ToString());
        //                            oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nResEOBPaymentDetailID"].ToString());
        //                        }
        //                        else
        //                        {
        //                            oEOBInsurancePaymentDetail.ReserveEOBPaymentID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nEOBPaymentID"].ToString());
        //                            oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nEOBPaymentDetailID"].ToString());
        //                        }
        //                        oEOBInsurancePaymentDetail.AccountID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nAccountID"].ToString());
        //                        oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
        //                        oEOBInsurancePaymentDetail.MSTAccountID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nMSTAccountID"].ToString());
        //                        oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.InsuranceCompany;
        //                        oEOBInsurancePaymentDetail.PatientID = PatientId;
        //                        oEOBInsurancePaymentDetail.PaymentTrayID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nPaymentTrayID"].ToString());
        //                        oEOBInsurancePaymentDetail.PaymentTrayCode = Convert.ToString(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["sPaymentTrayCode"].ToString());
        //                        oEOBInsurancePaymentDetail.PaymentTrayDescription = Convert.ToString(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["sPaymentTrayDescription"].ToString());
        //                        oEOBInsurancePaymentDetail.UserID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nUserID"].ToString());
        //                        oEOBInsurancePaymentDetail.UserName = Convert.ToString(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["sUserName"].ToString());
        //                        oEOBInsurancePaymentDetail.ClinicID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nClinicID"].ToString());
        //                        oEOBInsurancePaymentDetail.CloseDate = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nCloseDate"].ToString());
        //                        oEOBInsurancePaymentDetail.FinanceLieNo = 0;
        //                        oEOBInsurancePaymentDetail.MainCreditLineID = 0;
        //                        oEOBInsurancePaymentDetail.IsMainCreditLine = false;
        //                        oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
        //                        oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
        //                        oEOBInsurancePaymentDetail.RefFinanceLieNo = 1;
        //                        oEOBInsurancePaymentDetail.UseRefFinanceLieNo = false;
        //                        oEOBInsurancePaymentDetail.TrackBillingTransactionID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nTrackTrnID"].ToString());
        //                        oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nTrackTrnDtlID"].ToString());
        //                        oEOBInsurancePaymentDetail.SubClaimNo = "";


        //                        oEOBInsurancePaymentDetail.VoidCloseDate = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nVoidCloseDate"].ToString());
        //                        oEOBInsurancePaymentDetail.VoidTrayID = Convert.ToInt64(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["nVoidTrayID"].ToString());
        //                        oEOBInsurancePaymentDetail.IsVoid = Convert.ToBoolean(dtEOBInsurancePayment.Rows[_nPayDtlCounter]["bIsVoid"].ToString());

        //                        oEOBInsurancePaymentDetail.VoidType = VoidType.InsurancePaymentRefundVoidEntry;
        //                        oEOBInsurancePaymentDetails.Add(oEOBInsurancePaymentDetail);
        //                    }
        //                    #endregion "Get the debit allocation entry for voiding payment"
        //                }
        //            }
        //            #endregion " Set Payment Detail Data "
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    { ex.ERROR_Log(ex.ToString()); }
        //    catch (Exception ex)
        //    { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        //    finally
        //    {
        //        if (oParameters != null) { oParameters.Dispose(); }
        //        if (oEOBInsurancePaymentDetail != null) { oEOBInsurancePaymentDetail.Dispose(); }
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (dtEOBInsurancePayment != null) { dtEOBInsurancePayment.Dispose(); }
        //    }

        //    return oEOBInsurancePaymentDetails;
        //}

      
        private Int64 GetPrefixTransactionID()
        {
            Int64 _Result = 0;
           
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = ""; 
            string _result = "";

            TimeSpan oTS;

            try
            {
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
            return _Result;
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

        #endregion

    }
}