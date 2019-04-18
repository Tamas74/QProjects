using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmPaymentTransferInfo : Form
    {


        #region " Variable Declarations "
        
        public bool oDialogResult = false;
        
        #endregion


        #region "Propoerty Procedures "
        
        public string PaymentTransferCloseDate { get; set; }

        public string AccountIDs { get; set; }

        public string AdjustmentCode { get; set; }

        public string AdjustmentDescription { get; set; }

        public Int64 PaymentTrayID { get; set; }

        public string PaymentTrayName { get; set; }

        public bool RemoveBadDebtFollowup { get; set; }

        public bool RemoveBadDebtStatus { get; set; }

        public bool IsForAutoDistribution { get; set; }

        public String CleargagePosting { get; set; }

        
        #endregion


        #region "Constructor "
        public  frmPaymentTransferInfo()
        {
            InitializeComponent();
        }
        
        #endregion
       

        #region " Form Events "

        private void frmPaymentTransferInfo_Load(object sender, EventArgs e)
        {
            FillPaymentTray();
            FillAdjustmentCode();
            SetCloseDate();
            if (IsForAutoDistribution)
            {
                lblAdjustmentCode.Visible = false;
                cmbAdjustmentCode.Visible = false;
                chkBadDebtFollowup.Visible = false;
                chkBadDebtStatus.Visible = false;
                lblOnSuccess.Visible = false;
                this.Text = "Auto Reserves Distribution";
                this.Height = 160;
               // this.Height = this.Height - 100;
            }
            if (Convert.ToString(CleargagePosting) != "" && Convert.ToString(CleargagePosting)!=null)
            {
                lblAdjustmentCode.Visible = false;
                cmbAdjustmentCode.Visible = false;
                chkBadDebtFollowup.Visible = false;
                chkBadDebtStatus.Visible = false;
                lblOnSuccess.Visible = false;
                this.Height = 160;
                this.Text = "Payment Tray Information";
            }

        }
        private void SetCloseDate()
        {
            gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                //mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (mskDate != null)
                {
                    if (strDate.Length <= 0)
                    {
                        #region " Set last selected close date "

                        string _lastCloseDate = gloBilling.GetUserWiseCloseDay(gloGlobal.gloPMGlobal.UserID, CloseDayType.Payment);
                        mskCloseDate.Text = _lastCloseDate;
                        if (mskDate.Text == "")
                        {
                            mskCloseDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        }
                        else
                        { mskCloseDate.Text = mskCloseDate.Text; }
                        #endregion " Set last selected close date "
                    }
                }
                mskDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
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
            DataTable dtPaymentTray = new DataTable();
            try
            {
                
                #region " .... Get the last selected Payment tray ... "

                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                Object _retSettingValue = null;
                oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { _lastselectedTrayId = Convert.ToInt64(_retSettingValue); }

                #endregion " .... Get the last selected Payment tray ... "

                #region " ... Get the default Payment Tray .... "

                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray " +
               " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
               " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + gloGlobal.gloPMGlobal.UserID + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "";
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _defaultTrayId = Convert.ToInt64(_retVal); }

                #endregion " ... Get the default Payment Tray .... "

                //...Load the last selected tray if present or else load the default tray
                //SLR: Free previously allocated memory before making new memory
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

                string _sqlRetrieveQuery = String.Empty;
                
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
               
                    _sqlRetrieveQuery = "Select nCloseDayTrayID AS nID,sCode,sDescription,nNumberOfDays,Case when bIsDefault=0 " +
                        " Then '' else 'Default' end ,CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nStartDate), 101) AS nStartDate, " +
                        " CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nEndDate), 101) AS nEndDate,Case when isnull(bIsClosed,0)=0 " +
                        " Then 'Active' else 'Closed' end from BL_CloseDayTray WITH(NOLOCK) " +
                        " where nUserID='" + gloGlobal.gloPMGlobal.UserID + "' AND ISNULL(bIsClosed,0) = 0 AND  ISNULL(bIsActive,0)=1 AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "";


                oDB.Connect(false);
                oDB.Retrive_Query(_sqlRetrieveQuery, out dtPaymentTray);
                oDB.Disconnect();

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

                long nSelectedTryID = 0;
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oDB.Connect(false);
                _retVal = new object();
                //if (_defaultTrayId == 0)
                //{
                //    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WHERE nCloseDayTrayID = " + _lastselectedTrayId + " and ISNULL(bIsActive,0) = 1 AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
                //    if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                //    {
                //        nSelectedTryID = _lastselectedTrayId;
                //    }
                //    else
                //    {
                //        nSelectedTryID = 0;
                //    }
                //}
                //else
                //{
                //    nSelectedTryID = _defaultTrayId;
                //}
                if (_lastselectedTrayId > 0)
                {
                    if (IsPaymentTrayActive(_lastselectedTrayId))
                    {
                        _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WHERE nCloseDayTrayID = " + _lastselectedTrayId + " and ISNULL(bIsActive,0) = 1 AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + "");
                        if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                        {
                            nSelectedTryID = _lastselectedTrayId;
                        }
                        else
                        {
                            nSelectedTryID = 0;
                        }
                    }
                    else
                    {
                        nSelectedTryID = _defaultTrayId;
                    }
                }
                else
                {
                    nSelectedTryID = _defaultTrayId;
                }
                oDB.Disconnect();


                cmbPaymentTray.DataSource = dtPaymentTray;
                cmbPaymentTray.DisplayMember = "sDescription";
                cmbPaymentTray.ValueMember = "nID";
                cmbPaymentTray.SelectedValue = nSelectedTryID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
                //SLR: Free ogloValidateUser
                if (ogloValidateUser != null)
                {
                    ogloValidateUser.Dispose();
                }
            }
            
        }

        private void FillAdjustmentCode()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtAdjustmentTypes = null;
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT nAdjustmentTypeId,ISNULL(sAdjustmentTypeCode,'') AS sAdjustmentTypeCode, " +
                            " ISNULL(sAdjustmentTypeDesc,'') AS sAdjustmentTypeDesc, " +
                            " CASE ISNULL(bIsBlocked,'false') WHEN 'false' THEN 'Active' WHEN 'true' THEN 'Inactive' END AS Status, " +
                            " ISNULL(nClinicID,0) AS nClinicID " +
                            " FROM BL_AdjustmentType_MST WITH (NOLOCK) " +
                            " WHERE  nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + " AND bIsBlocked =0";

                oDB.Retrive_Query(_sqlQuery, out dtAdjustmentTypes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            cmbAdjustmentCode.DataSource = dtAdjustmentTypes;
            cmbAdjustmentCode.DisplayMember = "sAdjustmentTypeDesc";
            cmbAdjustmentCode.ValueMember = "sAdjustmentTypeCode";

        }
        
        private void tls_CloseMod_Click(object sender, EventArgs e)
        {
            mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            DialogResult result;

            if (mskCloseDate.Text != "" && mskCloseDate.Text != null)
            {
                result = MessageBox.Show("Do you want to save the changes?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    tls_SaveAndCloseMod_Click(null, null);

                }
                else if (result == DialogResult.No)
                {
                    oDialogResult = false;
                    this.Close();
                }
            }
            else
            {
                oDialogResult = false;
                this.Close();
            }
            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskCloseDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void mskCloseDate_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void tls_SaveAndCloseMod_Click(object sender, EventArgs e)
        {
             gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            if (ValidateDate())
            {
                oDialogResult = true;
                AdjustmentCode = Convert.ToString(cmbAdjustmentCode.SelectedValue);
                AdjustmentDescription = Convert.ToString(cmbAdjustmentCode.Text);
                PaymentTrayID = Convert.ToInt64(cmbPaymentTray.SelectedValue);
                PaymentTrayName = Convert.ToString(cmbPaymentTray.Text);
                RemoveBadDebtFollowup = chkBadDebtFollowup.Checked;
                RemoveBadDebtStatus = chkBadDebtStatus.Checked;
                if (Convert.ToInt64(cmbPaymentTray.SelectedValue) > 0)
                {
                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(mskCloseDate.Text).ToString("MM/dd/yyyy"), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", Convert.ToString(cmbPaymentTray.SelectedValue), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.Dispose();
                }
                ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, gloGlobal.gloPMGlobal.ClinicID);
                ogloBilling.Dispose();
                ogloBilling = null;
                this.Close(); 
            }
        }

        #endregion


        #region " Private Methods "

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

        private bool ValidateDate()
        {
            bool bReturn = false;
            try
            {
                mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskCloseDate.Text;
                mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                Int64 nNextActionCloseDate = 0;
                Int64 nPaymentCloseDate = 0;
                int _addDays = 0;
                _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
                if (mskCloseDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskCloseDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCloseDate.Focus();
                            bReturn = false;
                        }
                        else if (IsLessThenPreTransDate_Multiple(AccountIDs, mskCloseDate.Text.Trim(), ref nNextActionCloseDate))
                        {
                            MessageBox.Show("Payment Close Date must be on or after [" + gloDateMaster.gloDate.DateAsDateString(nNextActionCloseDate) + "]", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCloseDate.Focus();
                            bReturn = false;
                        }
                        else
                        {
                            gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                            if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                            {
                                MessageBox.Show("Selected date is already closed. Please select a different close date.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskCloseDate.Select(); mskCloseDate.Focus();
                                ogloBilling.Dispose();
                                ogloBilling = null;
                                return false;
                            }
                            ogloBilling.Dispose();
                            ogloBilling = null;
                            if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                            {
                                MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                mskCloseDate.Focus();
                                mskCloseDate.Select();
                                return false;
                            }
                          
                            
                            if (ChkCloseDateWithPaymentDate(AccountIDs, Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text)), ref nPaymentCloseDate))
                            {
                                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                                {
                                    DialogResult _dlgCloseDate = DialogResult.None;
                                    _dlgCloseDate = MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                                    if (_dlgCloseDate == DialogResult.Cancel)
                                    {
                                        mskCloseDate.Focus();
                                        mskCloseDate.Select();
                                        bReturn = false;
                                    }
                                    else
                                    {
                                        PaymentTransferCloseDate = mskCloseDate.Text;
                                        bReturn = true;
                                    }
                                }
                                else
                                {

                                    PaymentTransferCloseDate = mskCloseDate.Text;
                                    bReturn = true;

                                }
                            }
                            else
                            {
                                if (nPaymentCloseDate > 0)
                                {
                                    MessageBox.Show("Payment Close Date cannot be less than [" + gloDateMaster.gloDate.DateAsDateString(nPaymentCloseDate) + "]", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    mskCloseDate.Focus();
                                    bReturn = false;
                                }
                                else
                                {
                                   
                                        PaymentTransferCloseDate = mskCloseDate.Text;
                                        bReturn = true;
                                   
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter Close Date.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskCloseDate.Focus();
                    mskCloseDate.Select();
                    bReturn = false;
                }
                if (cmbPaymentTray.Text == "")
                {
                    MessageBox.Show("Please select payment tray. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPaymentTray.Focus();
                    bReturn = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                
            }
            return bReturn;
        }

        private Boolean IsLessThenPreTransDate_Multiple(string nTransactionID, string strDate, ref Int64 nNextActionCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            DataTable dtCloseDate = null;
            Boolean bReturn = false;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT ISNULL(MAX(nclosedate),0) as nCloseDate FROM BL_EOB_NextAction_HST AS bl_ INNER JOIN dbo.BL_Transaction_MST AS bl_2 ON bl_.nBillingTransactionID=bl_2.nTransactionID WHERE bl_2.nPAccountID IN (" + nTransactionID + ")";
                oDB.Retrive_Query(strQuery, out dtCloseDate);

                if (dtCloseDate.Rows.Count > 0 && dtCloseDate != null)
                {
                    if (Convert.ToInt32(dtCloseDate.Rows[0][0]) > 0)
                    {
                        if (gloDateMaster.gloDate.DateAsNumber(strDate) < Convert.ToInt32(dtCloseDate.Rows[0][0]))
                        {
                            nNextActionCloseDate = Convert.ToInt64(dtCloseDate.Rows[0][0]);
                            bReturn = true;

                        }
                        else
                        {
                            bReturn = false;
                        }
                    }
                    else
                    {
                        bReturn = false;
                    }
                }


            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return bReturn;
        }

        private Boolean ChkCloseDateWithPaymentDate(string nMasterTransID, Int32 strDate, ref Int64 nPaymentCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            DataTable dtCloseDate = null;
            Boolean bReturn = false;
            Int32 _paymentDate = 0;
            try
            {
                oDB.Connect(false);
                //strQuery = "SELECT ISNULL(MAX(nclosedate),0) as nCloseDate FROM BL_EOBPayment_dtl WHERE nBillingTransactionID = " + nMasterTransID + " and ISNULL(bIsPaymentVoid,0) = 0 ";

              //  strQuery = "SELECT MAX(dtCloseDate) as nCloseDate FROM Debits WHERE nPAccountID IN (" + nMasterTransID + ") and ISNULL(bIsPaymentVoid,0) = 0 ";
                strQuery = " SELECT MAX(Temp.dtCloseDate) FROM " +
                         " ( " +
                         " SELECT MAX(dtCloseDate) AS dtCloseDate  FROM Debits WITH (NOLOCK) WHERE nPAccountID IN (" + nMasterTransID + ") and ISNULL(bIsPaymentVoid,0) = 0 " +
                         " UNION " +
                         " SELECT MAX(r.dtCloseDate) AS dtCloseDate FROM dbo.Reserves r WITH (NOLOCK) WHERE r.nReserveType NOT IN (7,4,1) AND ISNULL(r.bIsPaymentVoid,0)=0  AND nPAccountID IN (" + nMasterTransID + ") " +
                         " ) Temp ";
                oDB.Retrive_Query(strQuery, out dtCloseDate);

                if (dtCloseDate.Rows.Count > 0 && dtCloseDate != null)
                {
                    if (dtCloseDate.Rows[0][0] != DBNull.Value && Convert.ToDateTime(dtCloseDate.Rows[0][0]).ToShortDateString() != "")
                    {
                        
                        _paymentDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dtCloseDate.Rows[0][0]).ToString("MM/dd/yyyy"));

                        if (strDate < _paymentDate)
                        {
                            nPaymentCloseDate = Convert.ToInt64(_paymentDate);
                            bReturn = false;
                        }
                        else
                        {
                            bReturn = true;
                        }
                    }
                    else
                    {
                        nPaymentCloseDate = Convert.ToInt64(_paymentDate);
                        bReturn = false;
                    }
                }


            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return bReturn;
        }
        private static bool IsPaymentTrayActive(Int64 TrayId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            object _retVal = null;
            bool _isActiveTray = false;

            try
            {
                _sqlQuery = " SELECT ISNULL(bIsActive,1) AS IsActive FROM BL_CloseDayTray " +
                            " WHERE nCloseDayTrayID = " + TrayId + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID;

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _isActiveTray = true; }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _isActiveTray;
        }
        #endregion


    }
}
