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
using gloBilling;
using System.Collections;

namespace gloAccountsV2
{
    public partial class frmInsurancePaymentRefundLogV2 : Form
    {
        #region " gloListControl Variables "

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        string _ControlType = "";
        private ComboBox combo;
        private Label label;
        public gloAccountsV2.PaymentCollection.Credit EOBInsurancePaymentMasterLines = null;

        #endregion

        #region  " Grid Constants "

        const int COL_COUNT = 16;

        const int COL_CLOSEDATE = 0;
        const int COL_TRAY = 1;
        const int COL_COMPANY = 2;
        const int COL_CHECK_NO = 3;
        const int COL_REFUND_DATE = 4;
        const int COL_REFUND_AMOUNT = 5;
        const int COL_USER = 6;
        const int COL_NOTE = 7;
        const int COL_STATUS = 8;
        const int COL_EOBPAYMENT_ID = 9;
        const int COL_COMPANY_ID = 10;
        const int COL_PAYMENT_TRAY_ID = 11;
        const int COL_USER_ID = 12;
        const int COL_DEBIT_AMOUNT = 13;
        const int COL_DATETIME = 14;
        const int COL_REFUNDID = 15;

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

        private ArrayList _arrLstUsedReservesFromInsuranceForm = null;
        #endregion

        #region " Constructors "

        public frmInsurancePaymentRefundLogV2()
        {
            InitializeComponent();
            cmbPayTray.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPayTray.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }
        public frmInsurancePaymentRefundLogV2(ArrayList arrLstUsedUnsavedReservesOnForm)
        {
            InitializeComponent();

            this._arrLstUsedReservesFromInsuranceForm = new ArrayList();
            this._arrLstUsedReservesFromInsuranceForm = arrLstUsedUnsavedReservesOnForm;

            cmbPayTray.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPayTray.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }

        #endregion " Constructors "

        #region " Form Load "

        private void frmInsurancePaymentLog_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1InsuranceRefundLog, false);
            LoadFormData();

        }

        private void LoadFormData()
        {
            try
            {
                CloseDate = gloInsurancePaymentV2.GetLastUnclosedDate();
                FillUserList();
                LoadInsuranceRefundLog();
                btnSearchCompany.Focus();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        #endregion

       

        #region " Form Controls Events "

        #region " ToolStrip Button Click Events "

        private void tsb_NewRefund_Click(object sender, EventArgs e)
        {
            InsPaymentRefundDetails();
        }

        private void tsb_ViewRefund_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1InsuranceRefundLog != null && c1InsuranceRefundLog.Rows.Count > 1)
                {
                    if (c1InsuranceRefundLog.RowSel > 0)
                    {
                        Int64 _nInsuranceCompanyID = Convert.ToInt64(c1InsuranceRefundLog.GetData(c1InsuranceRefundLog.RowSel, COL_COMPANY_ID));
                        Int64 _nRefundID = Convert.ToInt64(c1InsuranceRefundLog.GetData(c1InsuranceRefundLog.RowSel, COL_REFUNDID));
                        Int64 _nRefEOBPaymentID = Convert.ToInt64(c1InsuranceRefundLog.GetData(c1InsuranceRefundLog.RowSel, COL_EOBPAYMENT_ID));
                        frmInsurancePayRefundViewV2 ofrmInsurancePayRefundView = new frmInsurancePayRefundViewV2(AppSettings.ConnectionStringPM, _nInsuranceCompanyID, _nRefundID, _nRefEOBPaymentID);
                        ofrmInsurancePayRefundView.ShowInTaskbar = false;
                        ofrmInsurancePayRefundView.StartPosition = FormStartPosition.CenterScreen;
                        ofrmInsurancePayRefundView.ShowDialog(this);
                        LoadInsuranceRefundLog();
                        ofrmInsurancePayRefundView.Dispose();

                    }
                }
                else
                {
                    MessageBox.Show("No existing refund found to view.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsb_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInsuranceRefundLog();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void btnSearchCompany_Click(object sender, EventArgs e)
        {
            SelectInsuranceCompany();
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
            SelectedInsuranceCompanyID = 0;
        }

        private void btnTraySelection_Click(object sender, EventArgs e)
        {
            SelectPaymentTray();
        }

        private void btnClearTray_Click(object sender, EventArgs e)
        {
            SelectedPaymentTrayID = 0;
        }

        private void cmbPayTray_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPayTray.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPayTray.Items[cmbPayTray.SelectedIndex])["DispName"]), cmbPayTray) >= cmbPayTray.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbPayTray, Convert.ToString(((DataRowView)cmbPayTray.Items[cmbPayTray.SelectedIndex])["DispName"]));//, cmbClearingHouse, 0, cmbClearingHouse.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbPayTray, "");
            }
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbUsers.Items[cmbUsers.SelectedIndex])["sLoginName"]), cmbUsers) >= cmbUsers.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbUsers, Convert.ToString(((DataRowView)cmbUsers.Items[cmbUsers.SelectedIndex])["sLoginName"]));//, cmbClearingHouse, 0, cmbClearingHouse.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbUsers, "");
            }
        }

        private void cmbUsers_MouseEnter(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbUsers.Items[cmbUsers.SelectedIndex])["sLoginName"]), cmbUsers) >= cmbUsers.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbUsers, Convert.ToString(((DataRowView)cmbUsers.Items[cmbUsers.SelectedIndex])["sLoginName"]));//, cmbClearingHouse, 0, cmbClearingHouse.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbUsers, "");
            }
        }

        private void cmbPayTray_MouseEnter(object sender, EventArgs e)
        {
            if (cmbPayTray.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPayTray.Items[cmbPayTray.SelectedIndex])["DispName"]), cmbPayTray) >= cmbPayTray.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbPayTray, Convert.ToString(((DataRowView)cmbPayTray.Items[cmbPayTray.SelectedIndex])["DispName"]));//, cmbClearingHouse, 0, cmbClearingHouse.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbPayTray, "");
            }
        }

        private void lblInsCompany_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                label = (Label)sender;
                if (lblInsCompany.Text != null && lblInsCompany.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblInsCompany.Text), lblInsCompany) >= lblInsCompany.Width - 20)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        toolTip1.SetToolTip(lblInsCompany, lblInsCompany.Text);
                    }
                    else
                    {
                        toolTip1.SetToolTip(lblInsCompany, "");
                    }
                    //toolTip1.SetToolTip(lblInsCompany, SelectedInsuranceCompany);
                }
                else
                {
                    toolTip1.SetToolTip(lblInsCompany, "");
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
                Ex = null;
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

        private void c1InsuranceLog_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1InsuranceLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1InsuranceRefundLog != null && c1InsuranceRefundLog.Rows.Count > 1)
                {
                    if (c1InsuranceRefundLog.RowSel > 0)
                    {
                        HitTestInfo hitInfo = this.c1InsuranceRefundLog.HitTest(e.X, e.Y);
                        if (hitInfo.Row > 0)
                        {
                            Int64 _nInsuranceCompanyID = Convert.ToInt64(c1InsuranceRefundLog.GetData(c1InsuranceRefundLog.RowSel, COL_COMPANY_ID));
                            Int64 _nRefundID = Convert.ToInt64(c1InsuranceRefundLog.GetData(c1InsuranceRefundLog.RowSel, COL_REFUNDID));
                            Int64 _nRefEOBPaymentID = Convert.ToInt64(c1InsuranceRefundLog.GetData(c1InsuranceRefundLog.RowSel, COL_EOBPAYMENT_ID));
                            frmInsurancePayRefundViewV2 ofrmInsurancePayRefundView = new frmInsurancePayRefundViewV2(AppSettings.ConnectionStringPM, _nInsuranceCompanyID, _nRefundID, _nRefEOBPaymentID);
                            ofrmInsurancePayRefundView.ShowInTaskbar = false;
                            ofrmInsurancePayRefundView.StartPosition = FormStartPosition.CenterScreen;
                            ofrmInsurancePayRefundView.ShowDialog(this);
                            LoadInsuranceRefundLog();
                            ofrmInsurancePayRefundView.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnMouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloBilling.Properties.Resources.Img_LongYellow;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloBilling.Properties.Resources.Img_LongButton;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

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
            }
        }

        #endregion

        #endregion

        #region " Methods & Procedures "

        private void FillUserList()
        {
            DataTable _dtUsers = new DataTable();
            _dtUsers = gloGlobal.gloPMMasters.GetUsers();
            if (_dtUsers.Rows.Count != 0)
            {
                DataRow dr = _dtUsers.NewRow();
                dr["sLoginName"] = "";
                _dtUsers.Rows.InsertAt(dr, 0);
                _dtUsers.AcceptChanges();

                cmbUsers.DataSource = _dtUsers.Copy();
                cmbUsers.DisplayMember = "sLoginName";
                cmbUsers.ValueMember = "nUserID";

                if (AppSettings.UserID != 0)
                { cmbUsers.SelectedValue = AppSettings.UserID; }
                else
                { cmbUsers.SelectedValue = 1; }
            }
            else
            {
                DataRow dr = _dtUsers.NewRow();
                dr["sLoginName"] = "";
                _dtUsers.Rows.InsertAt(dr, 0);
                _dtUsers.AcceptChanges();
            }
        }

        private void SelectInsuranceCompany()
        {
            using (frmSearchInsuranceCompany ofrmSearchInsuranceCompany = new frmSearchInsuranceCompany())
            {
                try
                {
                    ofrmSearchInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                    ofrmSearchInsuranceCompany.ShowDialog(this);

                    if (ofrmSearchInsuranceCompany.FrmDlgRst == DialogResult.OK)
                    {
                        this.SelectedInsuranceCompanyID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                        this.SelectedInsuranceCompany = ofrmSearchInsuranceCompany.InsuranceCompanyName;

                    //    if (SelectedInsuranceCompanyID != 0 && SelectedInsuranceCompany != "")
                    //    { toolTip1.SetToolTip(lblInsCompany, SelectedInsuranceCompany); }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ofrmSearchInsuranceCompany.Dispose();
                }
            }
           
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

        private void InsPaymentRefundDetails()
        {
            try
            {
                using (frmInsurancePaymentRefundV2 ofrmInsurancePaymentRefund = new frmInsurancePaymentRefundV2(AppSettings.ConnectionStringPM, _insuranceCompanyID, this._arrLstUsedReservesFromInsuranceForm))
                {
                    ofrmInsurancePaymentRefund.EOBInsurancePaymentMasterLines = this.EOBInsurancePaymentMasterLines;





                    ofrmInsurancePaymentRefund.ShowInTaskbar = false;
                    ofrmInsurancePaymentRefund.StartPosition = FormStartPosition.CenterScreen;
                    ofrmInsurancePaymentRefund.ShowDialog(this);
                    LoadInsuranceRefundLog();
                    ofrmInsurancePaymentRefund.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean FillReserves(Int64 _nInsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtReserves = new DataTable();
            Boolean bResult = false;
            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("Select InsuarnceCompanyName,nEOBPaymentID,nEOBID,nEOBDtlID,nEOBPaymentDetailID, "
                                + " nBillingTransactionID,nBillingTransactionDetailID,nBillingTransactionLineNo,nPatientID, "
                                + " nDOSFrom,nDOSTo,nAmount,nPayMode,nRefEOBPaymentID,nRefEOBPaymentDetailID,nResEOBPaymentID,"
                                + " nResEOBPaymentDetailID,nAccountID,nAccountType,nMSTAccountID,nMSTAccountType,nPaymentMode, "
                                + " CheckNumber,nCheckAmount,nCheckDate,nPayerID,PatientName,sNoteDescription,sNoteCode, "
                                + " nPaymentNoteSubType,UsedReserve,AvailableReserve,nCloseDate,AssociationPatientName, "
                                + " AssociationPatientID,AssociationClaim,AssociationMSTTransactionID,AssociationnTransactionID	"
                                + " from view_InsuranceCompanyReserves where nPayerID = " + _nInsuranceID, out _dtReserves);
                oDB.Disconnect();

                if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                {
                    bResult = true;
                }
                return bResult;
            }
            catch //(Exception ex)
            {
                return bResult;
            }
            finally
            {
                _dtReserves.Dispose();
                oDB.Dispose();
            }


        }
        
        private void LoadInsuranceRefundLog()
        { 
            DataTable _dtRefundLog = new DataTable();
            try
            {
                this.Cursor = Cursors.WaitCursor;
               
                if (ValidateParameters())
                {
                    DesignPaymentGrid();
                    _dtRefundLog = gloInsurancePaymentV2.GetInsurancePaymentRefundLog(SelectedInsuranceCompanyID, SelectedPaymentTrays(), SelectedUserID, CheckNumber, PaymentDate, CloseDate);
                    foreach (DataRow row in _dtRefundLog.Rows)
                    {
                        c1InsuranceRefundLog.Rows.Add();
                        int i = c1InsuranceRefundLog.Rows.Count - 1;
                        c1InsuranceRefundLog.SetData(i, COL_CLOSEDATE, row["CloseDate"]);
                        c1InsuranceRefundLog.SetData(i, COL_TRAY, row["Tray"]);
                        c1InsuranceRefundLog.SetData(i, COL_COMPANY, row["Company"]);
                        c1InsuranceRefundLog.SetData(i, COL_CHECK_NO, row["CheckNumber"]);
                        c1InsuranceRefundLog.SetData(i, COL_REFUND_DATE, row["PaymentDate"]);
                        c1InsuranceRefundLog.SetData(i, COL_REFUND_AMOUNT, row["Amount"]);
                        c1InsuranceRefundLog.SetData(i, COL_NOTE, row["sNoteDescription"]);
                        c1InsuranceRefundLog.SetData(i, COL_USER, row["User Name"]);
                        c1InsuranceRefundLog.SetData(i, COL_EOBPAYMENT_ID, row["nEOBPaymentID"]);
                        c1InsuranceRefundLog.SetData(i, COL_STATUS, row["Status"]);
                        c1InsuranceRefundLog.SetData(i, COL_DATETIME, row["RefundDateTime"]);
                        c1InsuranceRefundLog.SetData(i, COL_REFUNDID, row["nRefundId"]);
                        c1InsuranceRefundLog.SetData(i, COL_COMPANY_ID, row["nPayerID"]);
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            { 
            this.Cursor = Cursors.Default;
            _dtRefundLog.Dispose();
            }
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
            if (!IsValidDate(mskPaymentDate, true))
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

                c1InsuranceRefundLog.Redraw = false;
                c1InsuranceRefundLog.Clear();

                c1InsuranceRefundLog.Cols.Count = COL_COUNT;
                c1InsuranceRefundLog.Rows.Count = 1;
                c1InsuranceRefundLog.Rows.Fixed = 1;
                c1InsuranceRefundLog.Cols.Fixed = 0;

                c1InsuranceRefundLog.AllowEditing = false;
                c1InsuranceRefundLog.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1InsuranceRefundLog.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #endregion

                #region " Set Headers "

                c1InsuranceRefundLog.SetData(0, COL_CLOSEDATE, "Close Date");
                c1InsuranceRefundLog.SetData(0, COL_TRAY, "Tray");
                c1InsuranceRefundLog.SetData(0, COL_COMPANY, "Refund To");
                c1InsuranceRefundLog.SetData(0, COL_CHECK_NO, "Refund Check#");
                c1InsuranceRefundLog.SetData(0, COL_REFUND_DATE, "Refund Date");
                c1InsuranceRefundLog.SetData(0, COL_REFUND_AMOUNT, "Amount");
                c1InsuranceRefundLog.SetData(0, COL_USER, "User");
                c1InsuranceRefundLog.SetData(0, COL_NOTE, "Note");
                c1InsuranceRefundLog.SetData(0, COL_DATETIME, "Date / Time");
                c1InsuranceRefundLog.SetData(0, COL_STATUS, "Status");
                c1InsuranceRefundLog.SetData(0, COL_REFUNDID, "nRefundID");

                #endregion

                #region " Show/Hide "

                c1InsuranceRefundLog.Cols[COL_EOBPAYMENT_ID].Visible = false;
                c1InsuranceRefundLog.Cols[COL_COMPANY_ID].Visible = false;
                c1InsuranceRefundLog.Cols[COL_PAYMENT_TRAY_ID].Visible = false;
                c1InsuranceRefundLog.Cols[COL_USER_ID].Visible = false;
                c1InsuranceRefundLog.Cols[COL_DEBIT_AMOUNT].Visible = false;
                c1InsuranceRefundLog.Cols[COL_REFUNDID].Visible = false;

                #endregion

                #region " Width "

                c1InsuranceRefundLog.Cols[COL_CLOSEDATE].Width = 75;
                c1InsuranceRefundLog.Cols[COL_TRAY].Width = 90;
                c1InsuranceRefundLog.Cols[COL_COMPANY].Width = 150;
                c1InsuranceRefundLog.Cols[COL_COMPANY_ID].Width = 0;
                c1InsuranceRefundLog.Cols[COL_CHECK_NO].Width = 100;
                c1InsuranceRefundLog.Cols[COL_REFUND_DATE].Width = 90;
                c1InsuranceRefundLog.Cols[COL_REFUND_AMOUNT].Width = 90;
                c1InsuranceRefundLog.Cols[COL_USER].Width = 85;
                c1InsuranceRefundLog.Cols[COL_NOTE].Width = 210;
                c1InsuranceRefundLog.Cols[COL_STATUS].Width = 50;
                c1InsuranceRefundLog.Cols[COL_DATETIME].Width = 140;

                #endregion

                #region " Data Type "

                c1InsuranceRefundLog.Cols[COL_CLOSEDATE].DataType = typeof(System.DateTime);
                c1InsuranceRefundLog.Cols[COL_TRAY].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_COMPANY].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_CHECK_NO].DataType = typeof(System.String);

                c1InsuranceRefundLog.Cols[COL_REFUND_DATE].DataType = typeof(System.DateTime);
                c1InsuranceRefundLog.Cols[COL_NOTE].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_STATUS].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_DATETIME].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_REFUNDID].DataType = typeof(System.Object);

                #endregion

                #region " Alignment "

                c1InsuranceRefundLog.Cols[COL_COMPANY].TextAlign = TextAlignEnum.LeftCenter;
                c1InsuranceRefundLog.Cols[COL_CHECK_NO].TextAlign = TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1InsuranceRefundLog.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1InsuranceRefundLog.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1InsuranceRefundLog.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1InsuranceRefundLog.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont ;//new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1InsuranceRefundLog.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont ;//new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                }
  

                c1InsuranceRefundLog.Cols[COL_REFUND_AMOUNT].Style = csCurrencyStyle;
                //c1InsuranceRefundLog.Cols[COL_USER].Style = csCurrencyStyle;

                //c1InsuranceLog.KeyActionEnter = KeyActionEnum.MoveAcross;
                //c1InsuranceLog.VisualStyle = VisualStyle.Custom;
                //c1InsuranceLog.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                //c1InsuranceLog.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                //c1InsuranceLog.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                #endregion
                c1InsuranceRefundLog.Cols[COL_CLOSEDATE].Format = "MM/dd/yyy";
                c1InsuranceRefundLog.Cols[COL_REFUND_DATE].Format = "MM/dd/yyy";

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { c1InsuranceRefundLog.Redraw = true; }
        }

        private void ClearCombo(ComboBox oComboBox)
        {
           // oComboBox.Items.Clear();
            oComboBox.DataSource = null;
            oComboBox.Items.Clear();
            oComboBox.Refresh();
        } 

        #region "Tooltip Methods"

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

        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {

            combo = (ComboBox)sender;
            if (combo.Items.Count > 0 && e.Index >= 0)
            {

                e.DrawBackground();
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(combo.GetItemText(combo.Items[e.Index]).ToString(), e.Font, br, e.Bounds);
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    if (combo.DroppedDown)
                    {
                        string txt = combo.GetItemText(combo.Items[e.Index]).ToString();


                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 20)
                        {
                            if (toolTip1.GetToolTip(combo) != txt)
                            {
                                this.toolTip1.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                            }
                        }
                        else
                        {
                            this.toolTip1.SetToolTip(combo, "");
                        }
                    }
                    else
                    {
                        this.toolTip1.Hide(combo);
                    }
                }
                else
                {
                    //this.tooltip_Billing.SetToolTip(combo,"");
                }
                e.DrawFocusRectangle();
            }
        }

        #endregion

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
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); }


        }

        #endregion

    }
}