using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloDateMaster;
using gloBilling.Payment;
using gloSettings;

namespace gloBilling
{
    public partial class frmBillingCheckDiff : Form
    {
        #region  " Variable Declarations "

        public DialogResult _frmDlgRst = DialogResult.None;
        private bool _showForm = true;
      //  private bool _isFormLoading = false;

        #endregion " Variable Declarations "

        #region " Property Procedures "
        
        private string _checkNo = "";
        public string CheckNo
        {
            get { return _checkNo; }
            set { _checkNo = value; }
        }

        private Int64 _checkDate = 0;
        public Int64 CheckDate
        {
            get { return _checkDate; }
            set { _checkDate = value; }
        }

        private decimal _checkAmount = 0;
        public decimal CheckAmount
        {
            get { return _checkAmount; }
            set { _checkAmount = value; }
        }

        private decimal _pendingAmount = 0;
        public decimal PendingAmount
        {
            get { return _pendingAmount; }
            set { _pendingAmount = value; }
        }

        private Int64 _multiTranId = 0;
        public Int64 MultipleTransactionID
        {
            get { return _multiTranId; }
            set { _multiTranId = value; }
        }

        private Int64 _eobPaymentId = 0;
        public Int64 EOBPaymentID
        {
            get { return _eobPaymentId; }
            set { _eobPaymentId = value; }
        }

        private Int64 _InsuranceCompanyId = 0;
        public Int64 InsuranceCompanyID
        {
            get { return _InsuranceCompanyId; }
            set
            {
                _InsuranceCompanyId = value;
                InsuranceCompanyName = string.Empty;
            }
        }

        private string _InsuranceCompanyName = "";
        public string InsuranceCompanyName
        {
            get { return _InsuranceCompanyName; }
            set
            {
                _InsuranceCompanyName = value;
                lblInsCompany.Text = _InsuranceCompanyName;
            }
        }

        public Int64 _selectedUserID = 0;
        public Int64 SelectedUserID
        {
            get
            {
                if (cmbUsers.SelectedIndex > 0)
                { _selectedUserID = Convert.ToInt64(cmbUsers.SelectedValue); }
                else
                { _selectedUserID = 0; }

                return _selectedUserID;
            }
            set { _selectedUserID = value; }
        }

        public Int64 _closeDate = 0;
        public Int64 CloseDate
        {
            get
            {
                if (mskCloseDate.MaskCompleted)
                { _closeDate = gloDate.DateAsNumber(mskCloseDate.Text); }
                else
                { _closeDate = 0; }

                return _closeDate;
            }
            set
            {
                _closeDate = value;

                if (_closeDate != 0)
                { mskCloseDate.Text = gloDate.DateAsDate(_closeDate).Date.ToString("MM/dd/yyyy"); }
                else
                { mskCloseDate.ResetText(); }
            }
        }

        public bool ShowForm
        {
            get { return _showForm; }
            set { _showForm = value; }
        }

        // Added by Pankaj 
        // Will return whether to show Completed Checks in to the list or not
        public bool IsShowCompleted
        {
            get 
            {
                if (tsb_ShowHideZeroCheck.Tag.ToString() == "Show") 
                { return true; }
                else
                { return false; }
            }
        }

        #endregion

        #region " Constructor "

        public frmBillingCheckDiff()
        {
            InitializeComponent();
        }

        #endregion " Constructor "

        #region " Form Load "

        private void frmBillingCheckDiff_Load(object sender, EventArgs e)
        {
            //_isFormLoading = true;
            try
            {
                gloC1FlexStyle.Style(c1PendingCheck, true);
                tsb_ShowHideZeroCheck.Tag = "Show";
                tsb_ShowHideZeroCheck.Text = "&Show Completed";
                tsb_ShowHideZeroCheck.ToolTipText = "Show Completed Check";
                tsb_ShowHideZeroCheck.Image = global::gloBilling.Properties.Resources.Show_Completed;

                // Fill User list & set current user as default one
                FillUserList();
                // Load pending check list as per default filters set
                FillPendingCheck();
            }
            catch (Exception EX)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), true);
            }
            finally
            {
               // _isFormLoading = false;
            }
        }

        #endregion " Form Load "

        #region " Private & Public Methods "

        void FillUserList()
        {
            DataTable _dtUsers = new DataTable();
            _dtUsers = InsurancePayment.GetUserList();
            cmbUsers.DataSource = _dtUsers;
            cmbUsers.DisplayMember = "sLoginName";
            cmbUsers.ValueMember = "nUserID";

            if (_dtUsers.Rows.Count != 0)
            {
                DataRow dr = _dtUsers.NewRow();
                dr["sLoginName"] = "";
                _dtUsers.Rows.InsertAt(dr, 0);
                _dtUsers.AcceptChanges();

                if (AppSettings.UserID != 0)
                { cmbUsers.SelectedValue = AppSettings.UserID; }
                else
                { cmbUsers.SelectedValue = 1; }
            }
        }

        private void FillPendingCheck()
        {
            if (!ValidateParameters())
            { return; }

            EOBPayment.gloEOBPaymentInsurance ogloEOBPayIns = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            DataTable _dtPendingCheck = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                //_dtPendingCheck = ogloEOBPayIns.SplitGetPendingChecks(_selectedUserId,_selectedCloseDate);
                _dtPendingCheck = InsurancePayment.GetInsurancePendingChecks(InsuranceCompanyID, CloseDate, SelectedUserID, IsShowCompleted);

                if (_dtPendingCheck != null && _dtPendingCheck.Rows.Count > 0)
                {
                    for (int i = _dtPendingCheck.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Convert.ToDecimal(_dtPendingCheck.Rows[i]["Remaining"]) == 0
                            && Convert.ToBoolean(_dtPendingCheck.Rows[i]["bIsDayClosed"]) == true)
                        {
                            _dtPendingCheck.Rows.RemoveAt(i);
                        }
                    }
                }
                if (_dtPendingCheck != null) //&& _dtPendingCheck.Rows.Count > 0)
                {
                    c1PendingCheck.DataSource = _dtPendingCheck;
                    CellStyle csCurrency;// = c1PendingCheck.Styles.Add("csCurrencyCell");
                    try
                    {
                        if (c1PendingCheck.Styles.Contains("csCurrencyCell"))
                        {
                            csCurrency = c1PendingCheck.Styles["csCurrencyCell"];
                        }
                        else
                        {
                            csCurrency = c1PendingCheck.Styles.Add("csCurrencyCell");
                            csCurrency.DataType = typeof(System.Decimal);
                            csCurrency.Format = "c";
                            csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
 
                        }

                    }
                    catch
                    {
                        csCurrency = c1PendingCheck.Styles.Add("csCurrencyCell");
                        csCurrency.DataType = typeof(System.Decimal);
                        csCurrency.Format = "c";
                        csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
 
                    }

                    c1PendingCheck.Cols["Check Amount"].Style = csCurrency;
                    c1PendingCheck.Cols["DebitAmount"].Style = csCurrency;
                    c1PendingCheck.Cols["Remaining"].Style = csCurrency;
                    c1PendingCheck.Cols["Remaining"].Caption = "Remaining Amount";

                    c1PendingCheck.Cols["Check Date"].Width = 80;
                    c1PendingCheck.Cols["CloseDate"].Width = 80;
                    c1PendingCheck.Cols["Ins. Company"].Width = 180;
                    c1PendingCheck.Cols["Check Amount"].Width = 130;
                    c1PendingCheck.Cols["Remaining"].Width = 125;
                    c1PendingCheck.Cols["Check #"].Width = 125;

                    c1PendingCheck.Cols["Check Date"].AllowResizing = false;
                    c1PendingCheck.Cols["CloseDate"].AllowResizing = false;
                    c1PendingCheck.Cols["Ins. Company"].AllowResizing = false;
                    c1PendingCheck.Cols["Check Amount"].AllowResizing = false;
                    c1PendingCheck.Cols["Remaining"].AllowResizing = false;
                    c1PendingCheck.Cols["Check #"].AllowResizing = false;


                    c1PendingCheck.Cols["nEOBPaymentID"].Visible = false;
                    c1PendingCheck.Cols["DebitAmount"].Visible = false;
                    c1PendingCheck.Cols["nPayerID"].Visible = false;
                    c1PendingCheck.Cols["nUserID"].Visible = false;
                    c1PendingCheck.Cols["bIsDayClosed"].Visible = false;
                    c1PendingCheck.Cols["nClinicID"].Visible = false;

                    c1PendingCheck.Cols["CloseDate"].Caption = "Close Date";

                    //c1PendingCheck.VisualStyle = VisualStyle.Office2007Blue;
                    //c1PendingCheck.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                    //c1PendingCheck.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                    //c1PendingCheck.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                    //if (c1PendingCheck.Rows.Count > 1)
                    //{ c1PendingCheck.Select(1, c1PendingCheck.Cols["Check #"].Index, true); }

                }
                else
                { _showForm = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void SelectCheck(int rowIndex)
        {
            try
            {
                if (c1PendingCheck != null && c1PendingCheck.Rows.Count > 1)
                {
                    if (rowIndex > 0)
                    {
                        if (c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check #"].Index) != null
                        && Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check #"].Index)).Trim() != "")
                        {
                            _checkNo = Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check #"].Index)).Trim();

                            string _strChkDate = "";
                            _strChkDate = Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Date"].Index)).Trim();
                            if (_strChkDate != "")
                            { _checkDate = gloDateMaster.gloDate.DateAsNumber(_strChkDate); }

                            if (c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Amount"].Index) != null
                                && Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Amount"].Index)).Trim() != ""
                                && Convert.ToDecimal(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Amount"].Index)) > 0)
                            {
                                _checkAmount = Convert.ToDecimal(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Check Amount"].Index));
                            }

                            if (c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Remaining"].Index) != null
                                && Convert.ToString(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Remaining"].Index)).Trim() != ""
                                && Convert.ToDecimal(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Remaining"].Index)) > 0)
                            {
                                _pendingAmount = Convert.ToDecimal(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["Remaining"].Index));
                            }

                            if (c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["nEOBPaymentID"].Index) != null)
                            {
                                _eobPaymentId = Convert.ToInt64(c1PendingCheck.GetData(rowIndex, c1PendingCheck.Cols["nEOBPaymentID"].Index));
                            }

                            _frmDlgRst = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
            }
        }

        private bool IsValidDate(MaskedTextBox mskDate, bool AllowBlank)
        {
            try
            {
                bool _isValid = false;

                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    { _isValid = gloDate.IsValid(mskDate.Text); }
                    else
                    {
                        if (AllowBlank)
                        { _isValid = true; }
                        else
                        { _isValid = false; }
                    }
                }
                return _isValid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private bool ValidateParameters()
        {
            if (!IsValidDate(mskCloseDate, true))
            {
                MessageBox.Show("Enter a valid close date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskCloseDate.Clear();
                mskCloseDate.Focus();
                return false;
            }
            return true;
        }
        #endregion " Private & Public Methods "

        #region " ToolStrip Button Events "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            if (!IsValidDate(mskCloseDate, true))
            {
                MessageBox.Show("Enter a valid close date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskCloseDate.Clear();
                mskCloseDate.Focus();
            }
            else
            {
                SelectCheck(c1PendingCheck.RowSel);
            }

        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            _frmDlgRst = DialogResult.Cancel;
            this.Close();
        }

        private void tsb_ShowHideZeroCheck_Click(object sender, EventArgs e)
        {
            if (tsb_ShowHideZeroCheck.Tag.ToString() == "Show")
            {
                tsb_ShowHideZeroCheck.Tag = "Hide";
                tsb_ShowHideZeroCheck.Text = "&Hide Completed";
                tsb_ShowHideZeroCheck.ToolTipText = "Hide Completed Check";
                tsb_ShowHideZeroCheck.Image = global::gloBilling.Properties.Resources.Hide_Completed;
                FillPendingCheck(); //FillPendingCheck(true)
            }
            else
            {
                tsb_ShowHideZeroCheck.Tag = "Show";
                tsb_ShowHideZeroCheck.Text = "&Show Completed";
                tsb_ShowHideZeroCheck.ToolTipText = "Show Completed Check";
                tsb_ShowHideZeroCheck.Image = global::gloBilling.Properties.Resources.Show_Completed;
                FillPendingCheck(); //FillPendingCheck(false);
            }
        }

        #endregion " ToolStrip Button Events "

        #region " Form Control Events "

        private void btnSearchInsuranceCompany_Click(object sender, EventArgs e)
        {
            try
            {
                frmSearchInsuranceCompany ofrmSearchInsuranceCompany = new frmSearchInsuranceCompany();
                ofrmSearchInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                ofrmSearchInsuranceCompany.ShowDialog(this);

                if (ofrmSearchInsuranceCompany.FrmDlgRst == DialogResult.OK)
                {
                    InsuranceCompanyID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                    InsuranceCompanyName = ofrmSearchInsuranceCompany.InsuranceCompanyName;

                    if (InsuranceCompanyID != 0 && InsuranceCompanyName != "")
                    {
                        toolTip1.SetToolTip(lblInsCompany, InsuranceCompanyName);
                    }
                }
                ofrmSearchInsuranceCompany.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
            InsuranceCompanyID = 0;
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mskCloseDate_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateParameters())
            {
                e.Cancel = true;
            }
        }

        private void DateMouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        #endregion

        #region " C1 Grid Events "

        private void c1PendingCheck_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            C1.Win.C1FlexGrid.HitTestInfo hitInfo;

            try
            {
                if (c1PendingCheck != null && c1PendingCheck.Rows.Count > 1)
                {
                    hitInfo = c1PendingCheck.HitTest(e.X, e.Y);
                    if (hitInfo.Row > 0)
                    {
                        SelectCheck(hitInfo.Row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
      
        #endregion " C1 Grid Events "

        #region " To be deleted "

        private void FillPendingCheck_20100325()
        {
            EOBPayment.gloEOBPaymentInsurance ogloEOBPayIns = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            DataTable _dtPendingCheck = null;

            try
            {
                //_dtPendingCheck = ogloEOBPayIns.SplitGetPendingChecks(IsShowCompleted);
                DataView dvMain = _dtPendingCheck.DefaultView;
                string _Filter = string.Empty;

                if (_InsuranceCompanyId != 0)
                { _Filter = "nPayerID='" + Convert.ToString(_InsuranceCompanyId) + "'"; }

                if (cmbUsers.SelectedIndex != 0)
                {
                    if (_Filter != "")
                    { _Filter += " AND nUserID = " + cmbUsers.SelectedValue + ""; }
                    else
                    { _Filter += " nUserID = " + cmbUsers.SelectedValue + ""; }
                }

                //if (_filterClosedate > 0)
                //{
                //    if (_Filter != "")
                //    { _Filter += " AND CloseDate = '"+ dtpCloseDate.Value.ToString("MM/dd/yyyy") +"'"; }
                //    else
                //    { _Filter += " CloseDate = '" + dtpCloseDate.Value.ToString("MM/dd/yyyy") + "'"; }
                //}

                if (_Filter != "")
                {
                    dvMain.Sort = "nPayerID, nUserID ";
                    dvMain.RowFilter = _Filter;
                }

                if (_dtPendingCheck != null) //&& _dtPendingCheck.Rows.Count > 0)
                {
                    c1PendingCheck.DataSource = dvMain;
                    CellStyle csCurrency;// = c1PendingCheck.Styles.Add("csCurrencyCell");
                    try
                    {
                        if (c1PendingCheck.Styles.Contains("csCurrencyCell"))
                        {
                            csCurrency = c1PendingCheck.Styles["csCurrencyCell"];
                        }
                        else
                        {
                            csCurrency = c1PendingCheck.Styles.Add("csCurrencyCell");
                            csCurrency.DataType = typeof(System.Decimal);
                            csCurrency.Format = "c";
                            csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        }

                    }
                    catch
                    {
                        csCurrency = c1PendingCheck.Styles.Add("csCurrencyCell");
                        csCurrency.DataType = typeof(System.Decimal);
                        csCurrency.Format = "c";
                        csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }
                       
                    c1PendingCheck.Cols["Check Amount"].Style = csCurrency;
                    c1PendingCheck.Cols["DebitAmount"].Style = csCurrency;
                    c1PendingCheck.Cols["Remaining"].Style = csCurrency;
                    c1PendingCheck.Cols["Remaining"].Caption = "Remaining Amount";

                    //c1PendingCheck.Cols["Check Amount"].Width = 100;
                    //c1PendingCheck.Cols["Remaining"].Width = 100;
                    //c1PendingCheck.Cols["Check #"].Width = 100;
                    c1PendingCheck.Cols["Check Amount"].Width = 130;
                    c1PendingCheck.Cols["Remaining"].Width = 130;
                    c1PendingCheck.Cols["Check #"].Width = 126;

                    c1PendingCheck.Cols["nEOBPaymentID"].Visible = false;
                    c1PendingCheck.Cols["DebitAmount"].Visible = false;
                    c1PendingCheck.Cols["nPayerID"].Visible = false;
                    c1PendingCheck.Cols["nUserID"].Visible = false;
                    c1PendingCheck.Cols["bIsDayClosed"].Visible = false;

                    c1PendingCheck.VisualStyle = VisualStyle.Office2007Blue;
                    c1PendingCheck.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                    c1PendingCheck.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                    c1PendingCheck.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                    if (c1PendingCheck.Rows.Count > 1)
                    { c1PendingCheck.Select(1, c1PendingCheck.Cols["Check #"].Index, true); }

                }
                else
                { _showForm = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            { }
        }

        private void FillPendingCheck_Old()
        {
            gloPayment ogloPayment = new gloPayment(AppSettings.ConnectionStringPM);
            DataTable _dtPendingCheck = null;

            try
            {
                _dtPendingCheck = ogloPayment.GetCheckAmountDiff(AppSettings.UserID, AppSettings.ClinicID);

                if (_dtPendingCheck != null && _dtPendingCheck.Rows.Count > 0)
                {
                    c1PendingCheck.DataSource = _dtPendingCheck;
                    CellStyle csCurrency;// = c1PendingCheck.Styles.Add("csCurrencyCell");
                    try
                    {
                        if (c1PendingCheck.Styles.Contains("csCurrencyCell"))
                        {
                            csCurrency = c1PendingCheck.Styles["csCurrencyCell"];
                        }
                        else
                        {
                            csCurrency = c1PendingCheck.Styles.Add("csCurrencyCell");
                            csCurrency.DataType = typeof(System.Decimal);
                            csCurrency.Format = "c";
                            csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        }

                    }
                    catch
                    {
                        csCurrency = c1PendingCheck.Styles.Add("csCurrencyCell");
                        csCurrency.DataType = typeof(System.Decimal);
                        csCurrency.Format = "c";
                        csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }
                    c1PendingCheck.Cols["CheckAmount"].Style = csCurrency;
                    c1PendingCheck.Cols["AppliedAmount"].Style = csCurrency;
                    c1PendingCheck.Cols["PendingAmount"].Style = csCurrency;
                    c1PendingCheck.Cols["nMultiPaymentTransactionID"].Visible = false;
                }
                else
                { _showForm = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            { }
        }

        private void lblCloseDate_Click(object sender, EventArgs e)
        {

        }

        //private void c1PendingCheck_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    C1.Win.C1FlexGrid.HitTestInfo hitInfo;

        //    try
        //    {
        //        if (c1PendingCheck != null && c1PendingCheck.Rows.Count > 1)
        //        {
        //            hitInfo = c1PendingCheck.HitTest(e.X, e.Y);
        //            if (hitInfo.Row > 0)
        //            {
        //                if (c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["CheckNo"].Index) != null
        //                    && Convert.ToString(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["CheckNo"].Index)).Trim() != "")
        //                {
        //                    _checkNo = Convert.ToString(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["CheckNo"].Index)).Trim();

        //                    string _strChkDate = "";
        //                    _strChkDate = Convert.ToString(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["Check Date"].Index)).Trim();
        //                    if (_strChkDate != "")
        //                    { _checkDate = gloDateMaster.gloDate.DateAsNumber(_strChkDate); }

        //                    if (c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["CheckAmount"].Index) != null
        //                        && Convert.ToString(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["CheckAmount"].Index)).Trim() != ""
        //                        && Convert.ToDecimal(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["CheckAmount"].Index)) > 0)
        //                    {
        //                        _checkAmount = Convert.ToDecimal(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["CheckAmount"].Index));
        //                    }

        //                    if (c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["PendingAmount"].Index) != null
        //                        && Convert.ToString(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["PendingAmount"].Index)).Trim() != ""
        //                        && Convert.ToDecimal(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["PendingAmount"].Index)) > 0)
        //                    {
        //                        _pendingAmount = Convert.ToDecimal(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["PendingAmount"].Index));
        //                    }

        //                    if (c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["nMultiPaymentTransactionID"].Index) != null)
        //                    {
        //                        _multiTranId = Convert.ToInt64(c1PendingCheck.GetData(hitInfo.Row, c1PendingCheck.Cols["nMultiPaymentTransactionID"].Index));
        //                    }

        //                    _frmDlgRst = DialogResult.OK;
        //                    this.Close();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information);
        //    }
        //}

        private void dtpCloseDate_ValueChanged(object sender, EventArgs e)
        {

            //try
            //{
            //    //_filterClosedate = gloDateMaster.gloDate.DateTimeAsNumber(dtpCloseDate.Value.ToString("MM/dd/yyyy"));
            //    if (_isFormLoading == false)
            //    {
            //        if (_filterClosedate > 0)
            //        {
            //            FillPendingCheck(); 
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
            //finally
            //{ 

            //}
        }

        private void cmbUsers_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void lblInsCompany_TextChanged(object sender, EventArgs e)
        {
            //if (!lblInsCompany.Text.Equals(""))
            //{
            //    FillPendingCheck(); //FillPendingCheck(true);
            //}
        }

        private bool ValidateCloseDate()
        {
            mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskCloseDate.Text;
            mskCloseDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            bool Success = false;
            {

                if (strDate.Trim() == "")
                {
                    MessageBox.Show("Enter the close date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;

                }
                else if (IsValidDate(mskCloseDate, false) == false)
                {
                    MessageBox.Show("Enter a valid close date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select();
                    mskCloseDate.Focus();
                    Success = false;
                    return Success;
                }
                else
                {
                    Success = true;
                }
            }
            return Success;
        } 

        #endregion

        private void tsb_Generate_Click(object sender, EventArgs e)
        {
            FillPendingCheck();
        }
    }
}