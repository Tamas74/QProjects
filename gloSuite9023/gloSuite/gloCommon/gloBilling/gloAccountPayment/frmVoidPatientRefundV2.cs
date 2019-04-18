using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloGlobal;
using gloBilling.Collections;

namespace gloAccountsV2
{
    public partial class frmVoidPatientRefundV2 : Form
    {
        
        #region "Variable Declarations"

        
        private Int64 _ncloseDate = 0;
        Int64 _PatientID = 0;      
     //   gloPatientStripControl.gloPatientStripControl oPatientControl = null;       
        public bool oDialogResult = false;
        private Int64 _nVoidedTrayID = 0;
       // private int _nVoidCloseDate = 0;
        private string _sVoidedTrayName = "";
        private string _sVoidTrayCode = "";
        private string _sVoidNotes = "";
        private ToolTip _tlTip = new ToolTip();
        Int64 _refundID=0;
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

        //public int VoidCloseDate
        //{
        //    get { return _nVoidCloseDate; }
        //    set
        //    {
        //        _nVoidCloseDate = value;
        //    }
        //}

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

        //public string VoidNotes
        //{
        //    get { return _sVoidNotes; }
        //    set { _sVoidNotes = value;}
        //}
        #endregion "Properties"

        #region "Constructor"

        public frmVoidPatientRefundV2(Int64 CloseDate,Int64 refundID,Int64 PatientID)
        {
            InitializeComponent();
            _ncloseDate = CloseDate;
            _PatientID = PatientID;
            _refundID=refundID;
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

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            Int64 _RetPayAccountID = 0;
            //gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            gloAccountsV2.gloPatientFinancialViewV2 PatientFinancialViewV2 = null;

            try
            {
                //This Function is used to Validate the Void close date,Void Tray and Notes
                isValid = ValidateVoidClaim(); 
                if (isValid)
                {
                    _sVoidNotes = txtNotes.Text.Trim();

                    //string _sqlQuery = " select nEOBPaymentID from BL_EOBPatient_Refund WITH (NOLOCK) where nrefundID=" + _refundID + " ";
                    //ODB.Connect(false);
                    DataTable _dt =  gloPatientPaymentV2.GetEOBPaymentID(_refundID,_PatientID);
                    Int64 _EOBPaymentID = 0;
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            _EOBPaymentID = Convert.ToInt64(_dt.Rows[i]["nEOBPaymentID"]);
                            if (_EOBPaymentID > 0)
                            {
                                if (mskCloseDate.MaskCompleted == true)
                                {
                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                }
                                PatientFinancialViewV2 = new gloPatientFinancialViewV2(_PatientID);
                                PatientFinancialViewV2.VoidPatientRefund(_EOBPaymentID, _PatientID, "", mskCloseDate.Text.ToString(), txtNotes.Text.Trim(), Convert.ToDateTime(mskCloseDate.Text.ToString()), Convert.ToInt64(lblPaymentTray.Tag), "", lblPaymentTray.Text.Trim(), _refundID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.UserID);
                                _RetPayAccountID = gloInsurancePaymentV2.GetPatientAccountsForPatRefundVoid(_EOBPaymentID);
                                if (_RetPayAccountID > 0)
                                {
                                    CL_FollowUpCode.SetAutoAccountFollowUp(_RetPayAccountID, _PatientID, Convert.ToDateTime(mskCloseDate.Text.ToString()));
                                }
                            }
                        }
                    }
                    if (_dt != null)
                    {
                        _dt.Dispose();
                        _dt = null;
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
                            MessageBox.Show("Enter valid close date. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                        else if (mskCloseDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        {
                            if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                            {
                                MessageBox.Show("Void close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                    }
                    else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                    {
                        MessageBox.Show("Enter close date. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Enter valid close date. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion "Close date validation"

        #region " Private and Public function"

        private void SetPaymentTrayPopup()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser( gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {
                if (lblPaymentTray.Tag != null && lblPaymentTray.Tag.ToString().Trim().Length > 0)
                {
                    _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK) " +
                   " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                   " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloPMGlobal.UserID + " AND nClinicID = " + gloPMGlobal.ClinicID + "";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                    {
                        _defaultTrayId = Convert.ToInt64(_retVal);

                        #region " Set last selected tray "

                        //...Check if the last selected tray is same as the default tray if yes set the 
                        //...last selected tray or else show pop to select the tray

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                        Object _retSettingValue = null;
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out _retSettingValue);
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
                                oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                                oDB.Connect(false);
                                _retVal = new object();
                                _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + gloPMGlobal.ClinicID + "");
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
                                gloBilling.frmBillingTraySelection ofrmBillingTraySelection = new gloBilling.frmBillingTraySelection(gloPMGlobal.DatabaseConnectionString);
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
                            oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                            oDB.Connect(false);
                            _retVal = new object();
                            _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + gloPMGlobal.ClinicID + "");
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

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                        Object _retSettingValue = null;
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out _retSettingValue);
                        oSettings.Dispose();

                        if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                        {
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                            oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                            oDB.Connect(false);
                            _retVal = new object();
                            _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE nCloseDayTrayID = " + Convert.ToInt64(_retSettingValue) + " AND nClinicID = " + gloPMGlobal.ClinicID + "");
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
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void SetCloseDate()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling( gloPMGlobal.DatabaseConnectionString, "");
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

                        ////....Load last selected close date
                        ////...If the last selected close date is being closed then make the close date empty.

                        //gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings( gloPMGlobal.DatabaseConnectionString);
                        //Object _retValue = null;
                        //string _clsDate = "";
                        //oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out _retValue);
                        //oSettings.Dispose();

                        //if (_retValue != null && Convert.ToString(_retValue).Trim() != "")
                        //{
                        //    try
                        //    { _clsDate = Convert.ToDateTime(Convert.ToString(_retValue).Trim()).ToString("MM/dd/yyyy"); }
                        //    catch (Exception ex)
                        //    {
                        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        //        ex = null;
                        //        _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
                        //    }
                        //}
                        //else
                        //{ _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy"); }

                        //if (_clsDate.Trim() != "")
                        //{
                        //    //gloBilling ogloBilling = new gloBilling( gloPMGlobal.DatabaseConnectionString, "");
                        //    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_clsDate.Trim())) == true)
                        //    { _clsDate = ""; }
                        //    ogloBilling.Dispose();
                        //}

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
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser( gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Int64 _lastselectedTrayId = 0;
            Object _retVal = null;
           // Int64 _nVoidTrayID = 0;
            try
            {
                //if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
                //{
                #region " .... Get the last selected Payment tray ... "

                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings( gloPMGlobal.DatabaseConnectionString);
                Object _retSettingValue = null;
                oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { _lastselectedTrayId = Convert.ToInt64(_retSettingValue); }

                #endregion " .... Get the last selected Payment tray ... "

                #region " ... Get the default Payment Tray .... "

                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray WITH (NOLOCK) " +
               " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
               " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloPMGlobal.UserID + " AND nClinicID = " + gloPMGlobal.ClinicID + "";
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _defaultTrayId = Convert.ToInt64(_retVal); }

                #endregion " ... Get the default Payment Tray .... "
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                //...Load the last selected tray if present or else load the default tray
                oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
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
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling( gloPMGlobal.DatabaseConnectionString, "");
            int _addDays = 0;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
            try
            {

                if (strDate.Trim() == "")
                {
                    MessageBox.Show("Enter close date. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;

                }
                else if (IsValidDate(mskCloseDate.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter valid close date. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }

                else if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select(); mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }

                else if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                {
                    MessageBox.Show("Void close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }

                else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                {
                    MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskCloseDate.Focus();
                    mskCloseDate.Select();
                    Success = false;
                    return Success;
                }



                else if (lblPaymentTray.Tag == null || lblPaymentTray.Tag.ToString().Trim() == "" || Convert.ToInt64(lblPaymentTray.Tag) <= 0)
                {
                    MessageBox.Show("Select payment tray. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSelectPaymentTray.Select();
                    btnSelectPaymentTray.Focus();
                    Success = false;
                    return Success;
                }


                else if (txtNotes.Text.Trim() == "")
                {
                    MessageBox.Show("Enter note. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNotes.Focus();
                    txtNotes.Select();
                    Success = false;
                    return Success;
                }

                else if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                {
                    DialogResult _dlgCloseDate = DialogResult.None;
                    _dlgCloseDate = MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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