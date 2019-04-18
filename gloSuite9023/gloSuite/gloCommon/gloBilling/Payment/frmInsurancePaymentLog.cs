using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

using gloBilling.Payment;
using gloSettings;
using gloDateMaster;

namespace gloBilling
{
    public partial class frmInsurancePaymentLog : Form
    {
        #region " gloListControl Variables "

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        string _ControlType = "";
        private ComboBox combo;
        #endregion

        #region  " Grid Constants "

        const int COL_COUNT = 14;

        const int COL_CLOSEDATE = 0;
        const int COL_TRAY = 1;
        const int COL_COMPANY = 2;
        const int COL_CHECK_NO = 3;
        const int COL_PAYMENT_DATE = 4;
        const int COL_PAYMENT_AMOUNT = 5;
        const int COL_REMAINING_AMOUNT = 6;
        const int COL_NOTE = 7;
        const int COL_STATUS = 8;

        const int COL_EOBPAYMENT_ID = 9;
        const int COL_COMPANY_ID = 10;
        const int COL_PAYMENT_TRAY_ID = 11;
        const int COL_USER_ID = 12;
        const int COL_DEBIT_AMOUNT = 13;

        #endregion

        #region " Property Procedures "

        Int64 _insuranceCompanyID = 0;
        private Int64 SelectedInsuranceCompanyID
        {
            get { return _insuranceCompanyID; }
            set
            {
                _insuranceCompanyID = value;
                lblInsCompany.Tag = _insuranceCompanyID;

                if (_insuranceCompanyID == 0)
                { SelectedInsuranceCompany = string.Empty; }
            }
        }

        string _insuranceCompanyName = string.Empty;
        private string SelectedInsuranceCompany
        {
            get { return _insuranceCompanyName; }
            set
            {
                _insuranceCompanyName = value;
                lblInsCompany.Text = _insuranceCompanyName;
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

        public string _userName = string.Empty;
        public string UserName
        {
            get
            {
                if (cmbUsers.SelectedIndex > 0)
                { _userName = Convert.ToString(cmbUsers.SelectedText); }
                else
                { _userName = string.Empty; }

                return _userName;
            }
            set
            {
                _userName = value;
                cmbUsers.Text = _userName;
            }
        }

        public string CheckNumber
        {
            get
            {
                string _checkNumber = string.Empty;
                if (txtCheckNo.Text != "")
                { _checkNumber = txtCheckNo.Text.Trim(); }
                return _checkNumber;
            }
        }

        public Int64 _paymentDate = 0;
        public Int64 PaymentDate
        {
            get
            {
                if (mskPaymentDate.MaskCompleted)
                { _paymentDate = gloDate.DateAsNumber(mskPaymentDate.Text); }
                else
                { _paymentDate = 0; }

                return _paymentDate;
            }
            set { _paymentDate = value; }
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


        #endregion

        #region " Constructors "

        public frmInsurancePaymentLog()
        {
            InitializeComponent();
        }

        #endregion " Constructors "

        #region " Form Load "

        private void frmInsurancePaymentLog_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1InsuranceLog, false);
            LoadFormData();

        }

        private void LoadFormData()
        {
            try
            {
                //Cls_TabIndexSettings tabSettings = new Cls_TabIndexSettings(this);
                //tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);

                CloseDate = InsurancePayment.GetLastUnclosedDate();
                FillUserList();
                LoadPaymentLog();

                btnSearchCompany.Focus();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        #endregion

        #region " ToolStrip Button Click Events "

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_ViewPayment_Click(object sender, EventArgs e)
        {
            ViewPaymentDetails();
        }

        private void tsb_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPaymentLog();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region " Form Controls Events "

        private void btnSearchCompany_Click(object sender, EventArgs e)
        {
            SelectInsuranceCompany();
        }

        private void btnTraySelection_Click(object sender, EventArgs e)
        {
            SelectPaymentTray();
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
            SelectedInsuranceCompanyID = 0;
        }

        private void btnClearTray_Click(object sender, EventArgs e)
        {
            SelectedPaymentTrayID = 0;
        }

        private void c1InsuranceLog_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1InsuranceLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ViewPaymentDetails();
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

        private void ValidateDate(object sender, CancelEventArgs e)
        {
            MaskedTextBox mskDate = (MaskedTextBox)sender;

            if (!IsValidDate(mskDate, true))
            {
                MessageBox.Show("Please enter a valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskDate.Clear();
                mskDate.Focus();
                e.Cancel = true;
            }
        }

        #endregion

        #region " Methods & Procedures "

        private void FillUserList()
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

        private void SelectInsuranceCompany()
        {
            try
            {
                using (frmSearchInsuranceCompany ofrmSearchInsuranceCompany = new frmSearchInsuranceCompany())
                {
                    ofrmSearchInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                    ofrmSearchInsuranceCompany.ShowDialog(this);

                    if (ofrmSearchInsuranceCompany.FrmDlgRst == DialogResult.OK)
                    {
                        this.SelectedInsuranceCompanyID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                        this.SelectedInsuranceCompany = ofrmSearchInsuranceCompany.InsuranceCompanyName;

                        if (SelectedInsuranceCompanyID != 0 && SelectedInsuranceCompany != "")
                        { toolTip1.SetToolTip(lblInsCompany, SelectedInsuranceCompany); }
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void FillPaymentTray()
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch { }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(AppSettings.ConnectionStringPM, gloListControl.gloListControlType.PaymentTray, true, this.Width);
            oListControl.FilterPaymentTrayByUsers = false;

            oListControl.ClinicID = AppSettings.ClinicID;
            oListControl.ControlHeader = " Payment Tray";

            oListControl.FilterPaymentTrayByUsers = false;

            _CurrentControlType = gloListControl.gloListControlType.PaymentTray;

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            _ControlType = cmbPayTray.Name.ToString();
            this.Controls.Add(oListControl);

            if (cmbPayTray.DataSource != null)
            {
                for (int i = 0; i < cmbPayTray.Items.Count; i++)
                {
                    cmbPayTray.SelectedIndex = i;
                    cmbPayTray.Refresh();
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbPayTray.SelectedValue), cmbPayTray.Text);
                }
            }
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
        }

        private string SelectedPaymentTrays()
        {
            string _selectedTrays = string.Empty;

            StringBuilder sbPayTray = new StringBuilder();
            sbPayTray.Remove(0, sbPayTray.Length);
            for (int i = 0; i < cmbPayTray.Items.Count; i++)
            {
                if (i == cmbPayTray.Items.Count - 1)
                { sbPayTray.Append(Convert.ToString(((DataRowView)cmbPayTray.Items[i])["ID"])); }
                else
                { sbPayTray.Append(Convert.ToString(((DataRowView)cmbPayTray.Items[i])["ID"]) + ","); }
            }
            _selectedTrays = Convert.ToString(sbPayTray);

            return _selectedTrays;
        }

        private void ViewPaymentDetails()
        {
            try
            {
                if (c1InsuranceLog != null && c1InsuranceLog.Rows.Count > 1)
                {
                    if (c1InsuranceLog.RowSel > 0)
                    {
                        Int64 _nEOBPaymentID = Convert.ToInt64(c1InsuranceLog.GetData(c1InsuranceLog.RowSel, COL_EOBPAYMENT_ID));
                        using (frmViewInsurancePayment ofrmViewInsurancePayment = new frmViewInsurancePayment(_nEOBPaymentID))
                        {
                            ofrmViewInsurancePayment.ShowInTaskbar = false;
                            ofrmViewInsurancePayment.StartPosition = FormStartPosition.CenterScreen;
                            ofrmViewInsurancePayment.ShowDialog(this);

                            bool _isPaymentVoid = InsurancePayment.IsVoidedInsurancePayment(_nEOBPaymentID);
                            if (_isPaymentVoid)
                            {
                                LoadPaymentLog();
                            }
                        }
                    }
                    else
                    { MessageBox.Show("Select a payment to view.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else
                { MessageBox.Show("No exisiting payment found to view.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void LoadPaymentLog()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable _dtPaymentLog = new DataTable();
                if (ValidateParameters())
                {
                    DesignPaymentGrid();

                    //_dtPaymentLog = InsurancePayment.GetInsurancePaymentLog(SelectedInsuranceCompanyID, SelectedPaymentTrayID, SelectedUserID, CheckNumber, PaymentDate, CloseDate);
                    _dtPaymentLog = InsurancePayment.GetInsurancePaymentLog(SelectedInsuranceCompanyID, SelectedPaymentTrays(), SelectedUserID, CheckNumber, PaymentDate, CloseDate);
                    foreach (DataRow row in _dtPaymentLog.Rows)
                    {
                        c1InsuranceLog.Rows.Add();
                        int i = c1InsuranceLog.Rows.Count - 1;
                        c1InsuranceLog.SetData(i, COL_CLOSEDATE, row["CloseDate"]);
                        c1InsuranceLog.SetData(i, COL_TRAY, row["Tray"]);
                        c1InsuranceLog.SetData(i, COL_COMPANY, row["Company"]);
                        c1InsuranceLog.SetData(i, COL_CHECK_NO, row["CheckNumber"]);
                        c1InsuranceLog.SetData(i, COL_PAYMENT_DATE, row["PaymentDate"]);
                        c1InsuranceLog.SetData(i, COL_PAYMENT_AMOUNT, row["Amount"]);
                        c1InsuranceLog.SetData(i, COL_NOTE, row["sNoteDescription"]);
                        c1InsuranceLog.SetData(i, COL_REMAINING_AMOUNT, row["Remaining"]);
                        c1InsuranceLog.SetData(i, COL_EOBPAYMENT_ID, row["nEOBPaymentID"]);
                        c1InsuranceLog.SetData(i, COL_STATUS, row["Status"]);
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private bool ValidateParameters()
        {
            if (!IsValidDate(mskCloseDate, true))
            {
                MessageBox.Show("Please enter a valid close date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskCloseDate.ResetText();
                mskCloseDate.Focus();
                return false;
            }
            else if (!IsValidDate(mskPaymentDate, true))
            {
                MessageBox.Show("Please enter a valid payment date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskPaymentDate.ResetText();
                mskPaymentDate.Focus();
                return false;
            }
            return true;
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

        private void DesignPaymentGrid()
        {
            try
            {
                #region " Grid Settings "

                c1InsuranceLog.Redraw = false;
                c1InsuranceLog.Clear();

                c1InsuranceLog.Cols.Count = COL_COUNT;
                c1InsuranceLog.Rows.Count = 1;
                c1InsuranceLog.Rows.Fixed = 1;
                c1InsuranceLog.Cols.Fixed = 0;

                c1InsuranceLog.AllowEditing = false;
                c1InsuranceLog.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1InsuranceLog.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #endregion

                #region " Set Headers "

                c1InsuranceLog.SetData(0, COL_CLOSEDATE, "Close Date");
                c1InsuranceLog.SetData(0, COL_TRAY, "Tray");
                c1InsuranceLog.SetData(0, COL_COMPANY, "Insurance Company");
                c1InsuranceLog.SetData(0, COL_CHECK_NO, "Check# / Ref.#");
                c1InsuranceLog.SetData(0, COL_PAYMENT_DATE, "Payment Date");
                c1InsuranceLog.SetData(0, COL_PAYMENT_AMOUNT, "Amount");
                c1InsuranceLog.SetData(0, COL_REMAINING_AMOUNT, "Remaining");
                c1InsuranceLog.SetData(0, COL_NOTE, "Note");
                c1InsuranceLog.SetData(0, COL_STATUS, "Status");

                #endregion

                #region " Show/Hide "

                c1InsuranceLog.Cols[COL_EOBPAYMENT_ID].Visible = false;
                c1InsuranceLog.Cols[COL_COMPANY_ID].Visible = false;
                c1InsuranceLog.Cols[COL_PAYMENT_TRAY_ID].Visible = false;
                c1InsuranceLog.Cols[COL_USER_ID].Visible = false;
                c1InsuranceLog.Cols[COL_DEBIT_AMOUNT].Visible = false;

                #endregion

                #region " Width "

                c1InsuranceLog.Cols[COL_CLOSEDATE].Width = 80;
                c1InsuranceLog.Cols[COL_TRAY].Width = 90;
                c1InsuranceLog.Cols[COL_COMPANY].Width = 150;
                c1InsuranceLog.Cols[COL_COMPANY_ID].Width = 100;
                c1InsuranceLog.Cols[COL_CHECK_NO].Width = 100;
                c1InsuranceLog.Cols[COL_PAYMENT_DATE].Width = 100;
                c1InsuranceLog.Cols[COL_PAYMENT_AMOUNT].Width = 90;
                c1InsuranceLog.Cols[COL_REMAINING_AMOUNT].Width = 90;
                c1InsuranceLog.Cols[COL_NOTE].Width = 160;
                c1InsuranceLog.Cols[COL_STATUS].Width = 70;

                #endregion

                #region " Data Type "

                c1InsuranceLog.Cols[COL_CLOSEDATE].DataType = typeof(System.String);
                c1InsuranceLog.Cols[COL_TRAY].DataType = typeof(System.String);
                c1InsuranceLog.Cols[COL_COMPANY].DataType = typeof(System.String);
                c1InsuranceLog.Cols[COL_CHECK_NO].DataType = typeof(System.String);

                c1InsuranceLog.Cols[COL_PAYMENT_DATE].DataType = typeof(System.String);
                c1InsuranceLog.Cols[COL_NOTE].DataType = typeof(System.String);
                c1InsuranceLog.Cols[COL_STATUS].DataType = typeof(System.String);

                #endregion

                #region " Alignment "

                c1InsuranceLog.Cols[COL_COMPANY].TextAlign = TextAlignEnum.LeftCenter;
                c1InsuranceLog.Cols[COL_CHECK_NO].TextAlign = TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1InsuranceLog.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1InsuranceLog.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1InsuranceLog.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1InsuranceLog.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1InsuranceLog.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
       

                c1InsuranceLog.Cols[COL_PAYMENT_AMOUNT].Style = csCurrencyStyle;
                c1InsuranceLog.Cols[COL_REMAINING_AMOUNT].Style = csCurrencyStyle;

                //c1InsuranceLog.KeyActionEnter = KeyActionEnum.MoveAcross;
                //c1InsuranceLog.VisualStyle = VisualStyle.Custom;
                //c1InsuranceLog.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                //c1InsuranceLog.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                //c1InsuranceLog.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                #endregion
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { c1InsuranceLog.Redraw = true; }
        }

        #endregion

        #region " gloListControl control events"

        private void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;
            ComboBox oComboBox = null;

            if (_ControlType == "cmbPayTray")
                oComboBox = cmbPayTray;

            switch (_CurrentControlType)
            {
                case gloListControl.gloListControlType.PaymentTray:
                    {
                        
                        oComboBox.DataSource = null;
                        oComboBox.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            oComboBox.DataSource = oBindTable;
                            oComboBox.DisplayMember = "DispName";
                            oComboBox.ValueMember = "ID";
                        }

                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch { }
                 
            }
        }

        private void btnBrowsePayTray_Click(object sender, EventArgs e)
        {
            FillPaymentTray();
        }

        private void btnClearPayTray_Click(object sender, EventArgs e)
        {
            ClearCombo(cmbPayTray);
        }

        private void ClearCombo(ComboBox oComboBox)
        {
           // oComboBox.Items.Clear();
            oComboBox.DataSource = null;
            oComboBox.Items.Clear();
            oComboBox.Refresh();
        } 

        #endregion

        #region " To be deleted "

        public Int64 _selectedTrayID = 0;
        private Int64 SelectedPaymentTrayID
        {
            get { return _selectedTrayID; }
            set
            {
                _selectedTrayID = value;
                lblPaymentTray.Tag = _selectedTrayID;

                if (_selectedTrayID == 0)
                { SelectedPaymentTray = string.Empty; }
            }
        }

        string _selectedTrayName = string.Empty;
        private string SelectedPaymentTray
        {
            get { return _selectedTrayName; }
            set
            {
                _selectedTrayName = value;
                lblPaymentTray.Text = _selectedTrayName;
            }
        }

        string _selectedTrayCode = string.Empty;
        private string SelectedPaymentTrayCode
        {
            get { return _selectedTrayCode; }
            set { _selectedTrayCode = value; }
        }

        private void SelectPaymentTray()
        {
            try
            {
                using (frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(AppSettings.ConnectionStringPM))
                {
                    ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                    ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                    ofrmBillingTraySelection.IsChargeTray = false;
                    ofrmBillingTraySelection.ShowDialog(this);

                    toolTip1.SetToolTip(lblPaymentTray, null);

                    if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                    {
                        if (ofrmBillingTraySelection.SelectedTrayID > 0)
                        {
                            this.SelectedPaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                            this.SelectedPaymentTrayID = ofrmBillingTraySelection.SelectedTrayID;

                            toolTip1.SetToolTip(lblPaymentTray, ofrmBillingTraySelection.SelectedTrayName);
                        }
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }


        }

        #endregion

        #region "Payment Tray Tooltip Set"
        private void cmbPayTray_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                combo = (ComboBox)sender;
                toolTip1.RemoveAll(); 
                if (cmbPayTray.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPayTray.Items[cmbPayTray.SelectedIndex])["DispName"]), cmbPayTray) >= cmbPayTray.DropDownWidth - 20)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        toolTip1.SetToolTip(cmbPayTray, Convert.ToString(((DataRowView)cmbPayTray.Items[cmbPayTray.SelectedIndex])["DispName"]));
                    }
                    else
                    {
                        this.toolTip1.Hide(cmbPayTray);
                    }
                }
                else
                {
                    this.toolTip1.Hide(cmbPayTray);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }

        private void cmbPayTray_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbPayTray);
        }
        #endregion
    }
}