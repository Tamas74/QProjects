using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmInsTransCloseDate : Form
    {


        #region " Variable Declarations "
        
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public bool oDialogResult = false;
        private string _databaseConnection = "";
        private Int64 _TransactionID = 0;
        private Int64 _TransactionMasterID = 0;
        private string _TransactionCloseDate = "";
        public ClaimHold _oClaimHold = null;
        private string _messageBoxCaption = "";
        public long CollectionAgency { get; set; }

        private String _sTransactionIDs = "";
        private String _sTransactionMasterIDs = "";
        #endregion


        #region "Propoerty Procedures "
        
        public string InsTransferCloseDate { get; set; }
        
        #endregion


        #region "Constructor "

        public frmInsTransCloseDate(string DatabaseConnectionString, Int64 TransactionID, Int64 TransactionMstID, string sCloseDate)
        {
            InitializeComponent();

            this._databaseConnection = DatabaseConnectionString;
            this._TransactionID = TransactionID;
            this._TransactionMasterID = TransactionMstID;
            this._TransactionCloseDate = sCloseDate;

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        }

        public frmInsTransCloseDate(string DatabaseConnectionString, string sCloseDate)
        {
            InitializeComponent();

            this._databaseConnection = DatabaseConnectionString;
            this._TransactionCloseDate = sCloseDate;
           
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        }

        public frmInsTransCloseDate(string DatabaseConnectionString, string nTransactionIDs, string nTransactionMstIDs, string sCloseDate)
        {
            InitializeComponent();

            this._databaseConnection = DatabaseConnectionString;
            this._sTransactionIDs = nTransactionIDs;
            this._sTransactionMasterIDs = nTransactionMstIDs;
            this._TransactionCloseDate = sCloseDate;

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        }
        #endregion
       

        #region " Form Events "

        private void frmInsTransCloseDate_Load(object sender, EventArgs e)
        {
            Int64 nPaymentCloseDate = 0;
            gloBilling ogloBilling = new gloBilling(_databaseConnection, "");

            if (!ChkCloseDateWithPaymentDate(_TransactionMasterID, Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(_TransactionCloseDate)), ref nPaymentCloseDate))
            {
                if (nPaymentCloseDate > 0)
                {
                    if (!ogloBilling.IsDayClosed(Convert.ToDateTime(gloDateMaster.gloDate.DateAsDateString(nPaymentCloseDate))))
                    {
                        mskClaimDate.Text = gloDateMaster.gloDate.DateAsDateString(nPaymentCloseDate);
                    }
                    else
                    {
                        ApplyNextDate(nPaymentCloseDate);
                    }
                }
                else
                {
                    if (!ogloBilling.IsDayClosed(Convert.ToDateTime(_TransactionCloseDate)))
                    {
                        mskClaimDate.Text = gloDateMaster.gloDate.DateAsDateString(gloDateMaster.gloDate.DateAsNumber(_TransactionCloseDate));
                    }
                    else
                    {
                        ApplyNextDate(gloDateMaster.gloDate.DateAsNumber(_TransactionCloseDate));
                    }
                }
            }
            else
            {
                if (!ogloBilling.IsDayClosed(Convert.ToDateTime(_TransactionCloseDate)))
                {
                    mskClaimDate.Text = gloDateMaster.gloDate.DateAsDateString(gloDateMaster.gloDate.DateAsNumber(_TransactionCloseDate));
                }
                else
                {
                    ApplyNextDate(gloDateMaster.gloDate.DateAsNumber(_TransactionCloseDate));
                }
            }
            if (ogloBilling != null)
            {
                ogloBilling.Dispose();
                ogloBilling = null;
            }

        }
        
        private void tls_CloseMod_Click(object sender, EventArgs e)
        {
            mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            DialogResult result;

            if (mskClaimDate.Text != "" && mskClaimDate.Text != null)
            {
                result = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
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
            mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskClaimDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void mskClaimDate_Validating(object sender, CancelEventArgs e)
        {
            gloBilling ogloBilling = new gloBilling(_databaseConnection, "");

            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                Int64 nPaymentCloseDate = 0;

                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                        else if (mskClaimDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskClaimDate.Name)
                        {
                            if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskDate.Text)))
                            {
                                MessageBox.Show("Day is already closed. You cannot perform any transaction for this close date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                            else
                            {
                                if (ChkCloseDateWithPaymentDate(_TransactionMasterID, Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(mskDate.Text)), ref nPaymentCloseDate))
                                {
                                    InsTransferCloseDate = mskClaimDate.Text;
                                }
                                else
                                {
                                    if (nPaymentCloseDate > 0)
                                    {
                                        MessageBox.Show("Transfer Close Date cannot be less than Payment Close Date [" + gloDateMaster.gloDate.DateAsDateString(nPaymentCloseDate) + "]", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        e.Cancel = true;
                                        return;
                                    }
                                    else
                                    {
                                        if (Convert.ToDateTime(mskClaimDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(7))
                                        {
                                            if (MessageBox.Show("Transfer Close Date [" + mskClaimDate.Text.Trim() + "] is too far in the future" + Environment.NewLine+ "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                            {
                                                mskClaimDate.Focus();
                                                mskClaimDate.Select();
                                                return;
                                            }
                                            else
                                            {
                                                InsTransferCloseDate = mskClaimDate.Text;
                                            }
                                        }
                                        else
                                        {
                                            InsTransferCloseDate = mskClaimDate.Text;
                                            //return;
                                        }
                                    }
                                }

                            }

                        }
                    }

                }

            }
            catch //(Exception ex)
            {
                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                ogloBilling.Dispose();
            }
        }

        private void tls_SaveAndCloseMod_Click(object sender, EventArgs e)
        {
            if (ValidateCloseDate())
            {
                oDialogResult = true;
                this.Close();
            }
        }

       

        #endregion


        #region " Private Methods "

        private void ApplyNextDate(Int64 nPaymentCloseDate)
        {
            DateTime dtClosedate =DateTime.Now;
            gloBilling ogloBilling = new gloBilling(_databaseConnection, "");
            while (ogloBilling.IsDayClosed(Convert.ToDateTime(gloDateMaster.gloDate.DateAsDateString(nPaymentCloseDate))))
            {
                dtClosedate = Convert.ToDateTime(gloDateMaster.gloDate.DateAsDateString(nPaymentCloseDate)).AddDays(1);

                nPaymentCloseDate = gloDateMaster.gloDate.DateAsNumber(dtClosedate.ToShortDateString());
            }
            if (ogloBilling != null)
            {
                ogloBilling.Dispose();
                ogloBilling = null;
            }

            mskClaimDate.Text = gloDateMaster.gloDate.DateAsDateString(nPaymentCloseDate);
        }

        private bool ValidateCloseDate()
        {
            gloBilling ogloBilling = new gloBilling(_databaseConnection, "");
            bool bReturn = false;
            try
            {

                mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskClaimDate.Text;
                mskClaimDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                Int64 nPaymentCloseDate = 0;
                Int64 nNextActionCloseDate = 0;
                int _addDays = 0;
                _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
                if (mskClaimDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskClaimDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskClaimDate.Focus();
                            bReturn = false;
                        }
                        else if (mskClaimDate.MaskCompleted == true)
                        {
                            if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskClaimDate.Text)))
                            {
                                MessageBox.Show("Day is already closed. You cannot perform any transaction for this close date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskClaimDate.Focus();
                                bReturn = false;
                            }
                            else if (_sTransactionIDs.Trim() != "" ? IsLessThenPreTransDate_Multiple(_sTransactionIDs, mskClaimDate.Text.Trim(), ref nNextActionCloseDate) : IsLessThenPreTransDate(_TransactionID, mskClaimDate.Text.Trim(), ref nNextActionCloseDate))
                            {
                                MessageBox.Show("Transfer Close Date must be on or after [" + gloDateMaster.gloDate.DateAsDateString(nNextActionCloseDate) + "]", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskClaimDate.Focus();
                                bReturn = false;
                            }
                          
                            else
                            {
                                if (ChkCloseDateWithPaymentDate(_TransactionMasterID, Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text)), ref nPaymentCloseDate))
                                {
                                    InsTransferCloseDate = mskClaimDate.Text;
                                    bReturn = true;
                                }
                                else
                                {
                                    if (nPaymentCloseDate > 0)
                                    {
                                        MessageBox.Show("Transfer Close Date cannot be less than Payment Close Date [" + gloDateMaster.gloDate.DateAsDateString(nPaymentCloseDate) + "]", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        mskClaimDate.Focus();
                                        bReturn = false;
                                    }
                                    else
                                    {
                                        if (Convert.ToDateTime(mskClaimDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                                        {
                                            MessageBox.Show("Transfer Close Date [" + mskClaimDate.Text.Trim() + "] is too far in the future.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            mskClaimDate.Focus();
                                            mskClaimDate.Select();
                                            bReturn = false;
                                        }
                                        else if (Convert.ToDateTime(mskClaimDate.Text.Trim()).Date > DateTime.Now.Date)
                                        {
                                            DialogResult _dlgCloseDate = DialogResult.None;
                                            _dlgCloseDate = MessageBox.Show("Close Date " + mskClaimDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                            if (_dlgCloseDate == DialogResult.Cancel)
                                            {
                                                mskClaimDate.Focus();
                                                mskClaimDate.Select();
                                                bReturn = false;
                                            }
                                            else
                                            {
                                                InsTransferCloseDate = mskClaimDate.Text;
                                                bReturn = true;
                                            }
                                        }
                                        else
                                        {
                                            InsTransferCloseDate = mskClaimDate.Text;
                                            bReturn = true;
                                        }
                                     
                                    }
                                }

                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter Transfer Close Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskClaimDate.Focus();
                        bReturn = false;
                    }

                }


            }
            catch //(Exception ex)
            {
                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskClaimDate.Focus();
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                ogloBilling.Dispose();
            }
            return bReturn;
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


        private Boolean ChkCloseDateWithPaymentDate(Int64 nMasterTransID, Int32 strDate, ref Int64 nPaymentCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            string strQuery = "";
            DataTable dtCloseDate = null;
            Boolean bReturn = false;
            Int32 _paymentDate = 0;
            try
            {
                oDB.Connect(false);
                //strQuery = "SELECT ISNULL(MAX(nclosedate),0) as nCloseDate FROM BL_EOBPayment_dtl WHERE nBillingTransactionID = " + nMasterTransID + " and ISNULL(bIsPaymentVoid,0) = 0 ";

                strQuery = "SELECT MAX(dtCloseDate) as nCloseDate FROM Debits WHERE nBillingTransactionID = " + nMasterTransID + " and ISNULL(bIsPaymentVoid,0) = 0 ";
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

        private Boolean IsLessThenPreTransDate(Int64 nTransactionID, string strDate, ref Int64 nNextActionCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            string strQuery = "";
            DataTable dtCloseDate = null;
            Boolean bReturn = false;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT ISNULL(MAX(nclosedate),0) as nCloseDate FROM BL_EOB_NextAction_HST WHERE nTrackMstTrnID = " + nTransactionID;
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

        private Boolean IsLessThenPreTransDate_Multiple(string nTransactionID, string strDate, ref Int64 nNextActionCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            string strQuery = "";
            DataTable dtCloseDate = null;
            Boolean bReturn = false;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT ISNULL(MAX(nclosedate),0) as nCloseDate FROM BL_EOB_NextAction_HST WHERE nTrackMstTrnID IN (" + nTransactionID+")";
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


        #endregion

    }
}
