using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloSettings;
using gloPatientStripControl;
using gloDateMaster;
using gloBilling.EOBPayment.Common;
using System.Data.SqlClient;
using gloBilling.Payment;

namespace gloBilling.gloERA
{
    public partial class frmCloseDate : Form
    {


        # region  "variables declaration"
        private Int64 _SelectedTrayID = 0;
        private string _SelectedTrayName = "";
        private string _PaymentTray = "";
        private DateTime _CloseDate;
        // private bool _IsSave = false;

        private ToolTip oToolTip;

        # endregion

        public frmCloseDate()
        {
            InitializeComponent();
        }

        # region " form load events"

        private void frmCloseDate_Load(object sender, EventArgs e)
        {
            oToolTip = new ToolTip();
            oToolTip.SetToolTip(btnTraySelection, "Select Payment Tray");

            LoadFormData();
        }
        #endregion


        # region  "Property declaration"

        public Int64 SelectedPaymentTrayID
        {
            get { return _SelectedTrayID; }
            set
            {
                _SelectedTrayID = value;
                lbBillingtray.Tag = _SelectedTrayID;

            }
        }
        public string SelectedPaymentTray
        {
            get { return _SelectedTrayName; }
            set
            {
                _SelectedTrayName = value;
                lbBillingtray.Text = _SelectedTrayName;
            }
        }
        public string PaymentTray
        {
            get { return _PaymentTray; }
            set { _PaymentTray = value; }
        }
        public DateTime CloseDate
        {
            get { return _CloseDate; }
            set { _CloseDate = value; }

        }

        # endregion


        #region "Private Methods"
        private void LoadFormData()
        {
            if (!IsCloseDaySet)
            { SetCloseDate(); }

            if (SelectedPaymentTrayID == 0)
            { FillPaymentTray(); }
        }
        private bool IsCloseDaySet
        {
            get { return mskCloseDate.MaskCompleted; }
        }
        private void SetCloseDate()
        {
            gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);

            try
            {
                //...Load last selected close date
                string _lastCloseDate = BillingSettings.LastSelectedCloseDate;

                //...If the last selected close date is being closed then make the close date empty.
                if (!_lastCloseDate.Equals(string.Empty))
                {
                    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_lastCloseDate)) == true)
                    {
                        _lastCloseDate = string.Empty;
                    }
                }
                mskCloseDate.Text = _lastCloseDate;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloBilling.Dispose();
            }
        }
        private void FillPaymentTray()
        {
            Int64 _lastSelectedTrayID = BillingSettings.LastSelectedPaymentTrayID;
            Int64 _defaultTrayID = InsurancePayment.GetDefaultPaymentTrayID();

            // Set default payment tray
            SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(_defaultTrayID);
            SelectedPaymentTrayID = _defaultTrayID;

            if (_lastSelectedTrayID > 0)
            {
                if (InsurancePayment.IsPaymentTrayActive(_lastSelectedTrayID))
                {
                    if (!_lastSelectedTrayID.Equals(_defaultTrayID))
                    {
                        SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(_lastSelectedTrayID);
                        SelectedPaymentTrayID = _lastSelectedTrayID;
                        SelectPaymentTray();
                    }
                }
                else
                {
                    SelectPaymentTray();
                }
            }
        }
        private void SelectPaymentTray()
        {
            frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(AppSettings.ConnectionStringPM);

            try
            {
                //...Show pop-up to select the Tray
                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingTraySelection.IsChargeTray = false;
                ofrmBillingTraySelection.ShowDialog(this);

                // toolTip1.SetToolTip(lbBillingtray, null);

                if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        this.SelectedPaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                        this.SelectedPaymentTrayID = ofrmBillingTraySelection.SelectedTrayID;

                        //  toolTip1.SetToolTip(lbBillingtray, ofrmBillingTraySelection.SelectedTrayName);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ofrmBillingTraySelection.Dispose();
            }
        }
        # endregion


        #region "Button click events"


        #endregion

        private void btnTraySelection_Click(object sender, EventArgs e)
        {
            SelectPaymentTray();
        }



        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (IsValidCloseDate())
            {
                DateTime.TryParse(mskCloseDate.Text, out _CloseDate);
                _PaymentTray = SelectedPaymentTray;
                this.Close();
            }


        }
        private void tsb_Close_Click(object sender, EventArgs e)
        {

            this.Close();

        }


        private void ValidateDate(object sender, CancelEventArgs e)
        {
            MaskedTextBox mskDate = (MaskedTextBox)sender;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskDate.Text;
            mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

            bool _isValid = false;

            if (mskDate != null)
            {
                if (strDate.Length > 0)
                {
                    _isValid = gloDate.IsValid(mskDate.Text);

                    if (!_isValid)
                    {
                        MessageBox.Show("Please enter a valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskDate.Clear();
                        mskDate.Focus();
                        e.Cancel = true;
                    }
                }
            }
        }
        private bool IsValidCloseDate()
        {
            int _addDays = 0;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
            if (mskCloseDate.MaskCompleted == false)
            {
                MessageBox.Show("Please enter the close date", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskCloseDate.Focus();
                mskCloseDate.Select();
                return false;
            }

            if (gloDate.IsValid(mskCloseDate.Text) == false)
            {
                CancelEventArgs e = new CancelEventArgs();
                ValidateDate(mskCloseDate, e);
                if (e.Cancel)
                { return false; }
            }
            else
            {
                #region " check for day closed "

                gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);
                if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select(); mskCloseDate.Focus();
                    if (ogloBilling != null)
                    {
                        ogloBilling.Dispose();
                        ogloBilling = null;
                    }
                    return false;
                }
                ogloBilling.Dispose();

                #endregion " check for day closed  "

                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                {
                    MessageBox.Show("Close date " + mskCloseDate.Text.Trim() + " is too far in the future. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); mskCloseDate.Focus();
                    mskCloseDate.Select();
                    return false;
                }
                else
                {
                    if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                    {
                        DialogResult _dlgCloseDate = DialogResult.None;
                        _dlgCloseDate = MessageBox.Show("Close date " + mskCloseDate.Text.Trim() + " is in future.Are you sure you want to continue with save ?", AppSettings.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (_dlgCloseDate == DialogResult.Cancel)
                        {
                            mskCloseDate.Focus();
                            mskCloseDate.Select();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void frmCloseDate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (oToolTip != null)
            {
                oToolTip.RemoveAll();
                oToolTip.Dispose();
                oToolTip = null;
            }
        }



    }
}
