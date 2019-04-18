using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using C1.Win.C1FlexGrid;
using System.Collections;
using gloSettings;
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmInsuranceOffset : Form
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private bool _isOpenForModify = false;
        private bool _isDayClosed = false;
        private bool _isProviderMandatory = false;
        string _ReserveNote = string.Empty;
        string _InsuranceCompany = string.Empty;
        public bool _isOpenForView = false;
        decimal _ReserveAllowed = 0;
        decimal _AmountToReserve = 0;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        bool IsProviderEnable = false;
        bool IsBusinessCenterEnable = false;
       // gloAccountsV2.PaymentCollection.Credit oPaymentInsurace = null;
       // gloAccountsV2.PaymentCollection.Debits oEOBInsurancePaymentReserveDetail = null;
        private gloAccountsV2.PaymentCollection.Reserves _ReserveDetails = null;
        private SplitClaimDetails _ClaimDetails = new SplitClaimDetails();
        bool _viewPatientDefault = false;
        Int64 _EOBPaymentDetailID = 0;
        Int64 _EOBPaymentID = 0;
        private ComboBox combo;
        ToolTip toolTip = new ToolTip();
        gloBilling.gloAccountPayment.dsPaymentTVP_V2 dsInsurancePayment_TVP = null;
        private Int64 _ncloseDate = 0;
        public Int64 _UserID = 0;
        #endregion " Declarations "

        #region " Property Procedures "

        public string ReserveNote
        {
            get { return _ReserveNote; }
            set { _ReserveNote = value; }
        }

        public string InsuranceCompany
        {
            get { return _InsuranceCompany; }
            set { _InsuranceCompany = value; lblInsCompany.Text = _InsuranceCompany; }
        }

        public decimal AmountToReserve
        {
            get { return _AmountToReserve; }
            set { _AmountToReserve = value; }
        }

        public Int64 EOBPaymentID
        {
            get { return _EOBPaymentID; }
            set { _EOBPaymentID = value; }
        }

        public Int64 EOBPaymentDetailID
        {
            get { return _EOBPaymentDetailID; }
            set { _EOBPaymentDetailID = value; }
        }

        public bool IsOpenForModify
        {
            get { return _isOpenForModify; }
            set { _isOpenForModify = value; }
        }

        public bool IsDayClosed
        {
            get { return _isDayClosed; }
            set { _isDayClosed = value; }
        }

        public bool IsProviderMandatory
        {
            get { return _isProviderMandatory; }
            set { _isProviderMandatory = value; }
        }

        private SplitClaimDetails ClaimDetails
        {
            get { return _ClaimDetails; }
            set
            {
                _ClaimDetails = value;

                cmbClaimNo.Text = string.Empty;
                if (_ClaimDetails.IsClaimExist)
                { cmbClaimNo.Text = _ClaimDetails.ClaimDisplayNo; }
            }
        }

        public gloAccountsV2.PaymentCollection.Reserves ReserveDetails
        {

            get { return _ReserveDetails; }
            set { _ReserveDetails = value; }

        }

        public bool viewPatientDefault
        {
            get { return _viewPatientDefault; }
            set { _viewPatientDefault = value; }
        }

        #endregion

        #region " Constructor "

        public frmInsuranceOffset(string DatabaseConnectionString, Int64 nEOBPaymentID, Int64 nEOBPaymentDetailID)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;
            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            #region " Retrieve ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

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

            _EOBPaymentID = nEOBPaymentID;
            _EOBPaymentDetailID = nEOBPaymentDetailID;
            _isOpenForModify = true;
        }

        public frmInsuranceOffset(string DatabaseConnectionString, decimal AllowedReserveAmt, decimal ReserveAmount, string Note, bool OpenModify)
        {
            InitializeComponent();

            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            _databaseconnectionstring = DatabaseConnectionString;

            #region " Retrieve ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

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

            _ReserveAllowed = AllowedReserveAmt;
            _AmountToReserve = ReserveAmount;
            _ReserveNote = Note;
            _isOpenForModify = OpenModify;

        }

        public frmInsuranceOffset(string DatabaseConnectionString)
        {
            InitializeComponent();

            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            _databaseconnectionstring = DatabaseConnectionString;

            #region " Retrieve ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

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
        }

        #endregion

        #region " Form Load Event "

        private void frmInsuranceOffset_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            tom.SetTabOrder(scheme);
            try
            {
                getData();
                IsProviderEnable = gloAccountsV2.gloBillingCommonV2.IsInsuranceReserve_ProviderEnable();
                IsBusinessCenterEnable = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_PatientAccount");

                if (IsBusinessCenterEnable)
                {
                    pnlBusinessCenter.Visible = true;
                    this.Height = 400;
                }

                if (_isOpenForView)
                {
                   
                    btnSelectPaymentTray.Enabled = false;
                    mskCloseDate.Enabled = false; 
                    ShowInsuranceReserved();
                }
                else
                {
                    FillCloseDateandPaymentTray();
                    btnSelectPaymentTray.Enabled = true;
                }
                             
                if (txtProvider.Tag != null && Convert.ToInt64(txtProvider.Tag) > 0)
                {
                    if (IsProviderMandatory) { btnClearProvider.Enabled = false; }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion " Form Load Event "

        #region " Control Events "

        private void tlsbtnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveValidation() == true && IsInsuranceCompanySelected())
                {
                    if (IsProviderEnable)
                    {
                        if (Convert.ToString(txtProvider.Text.ToString()) == "" && btnSearchProvider.Enabled == true && !_isOpenForModify)
                        {
                            MessageBox.Show("Please select provider.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSearchProvider.Focus();
                            return;
                        }
                    }
                    if (IsBusinessCenterEnable)
                    {
                        if (ReserveDetails.ClaimsAccountID > 0)
                        {
                            if (gloInsurancePaymentV2.IsBusinessCenterAssociated(ReserveDetails.ClaimsAccountID))
                            {
                                if (Convert.ToString(cmbBusinessCenter.SelectedValue) == "" || Convert.ToInt64(cmbBusinessCenter.SelectedValue) == 0)
                                {
                                    MessageBox.Show("Please select business center.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbBusinessCenter.Focus();
                                    return;
                                }
                            }
                            else if (Convert.ToString(cmbBusinessCenter.SelectedValue) == "" || Convert.ToInt64(cmbBusinessCenter.SelectedValue) == 0)
                            {
                                MessageBox.Show("Please select business center.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmbBusinessCenter.Focus();
                                return;
                            }
                        }
                        else if (Convert.ToString(cmbBusinessCenter.SelectedValue) == "" || Convert.ToInt64(cmbBusinessCenter.SelectedValue) == 0)
                        {
                            MessageBox.Show("Please select business center.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbBusinessCenter.Focus();
                            return;
                        }
                    }

                    bool _IsValidClaim = false;
                    _IsValidClaim = getValidClaimDetails();
                    if (_IsValidClaim)
                    {
                        setData();
                        if (_isOpenForView)
                        {
                            UpdateReserveNote();
                        }
                        else
                        {
                            SavePaymentOffeset();
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); 
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
        }

        private void tlsbtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
        }

        private void btnSearchPatient_Click(object sender, EventArgs e)
        {
            try
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Patient";

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); 
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
        }

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            txtPatient.Text = "";
            txtPatient.Tag = 0;
            //cmbClaimNo.Items.Clear();
            cmbClaimNo.DataSource = null;
            cmbClaimNo.Items.Clear();
            cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
            cmbClaimNo.Text = "";
            cmbClaimNo.Tag = 0;
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                int _Counter = 0;
                switch (_CurrentControlType)
                {
                    case gloListControl.gloListControlType.InsuranceCompany:
                        {
                            txtInsCompany.Text = "";
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
                                txtInsCompany.Tag = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                                txtInsCompany.Text = Convert.ToString(oBindTable.Rows[0]["DispName"]);
                            }
                        }
                        break;
                    case gloListControl.gloListControlType.Patient:
                        {
                            txtPatient.Text = "";
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

                                txtPatient.Text = Convert.ToString(oBindTable.Rows[0]["DispName"]);
                                txtPatient.Tag = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                            }
                            DataTable dtClaim = new DataTable();
                          
                            cmbClaimNo.DataSource = null;
                            cmbClaimNo.Items.Clear();
                            cmbClaimNo.Text = "";
                            getPatientClaimNos(Convert.ToInt64(txtPatient.Tag));
                        }
                        break;
                    case gloListControl.gloListControlType.Providers:
                        {
                            txtProvider.Text = "";
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

                                txtProvider.Text = Convert.ToString(oBindTable.Rows[0]["DispName"]);
                                txtProvider.Tag = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                            }

                        }

                        break;
                }
                this.Width = 588;
                if (IsBusinessCenterEnable)
                {
                    pnlBusinessCenter.Visible = true;
                    this.Height = 400;
                }
                else { this.Height = 355; }
            }
            catch (Exception)
            {


            }
            finally
            {


            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
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
            this.Width = 588;
            if (IsBusinessCenterEnable)
            {
                pnlBusinessCenter.Visible = true;
                this.Height = 400;
            }
            else { this.Height = 355; }
        }

        private void cmbClaimNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                if (e.KeyChar == Convert.ToChar(45) && cmbClaimNo.Text.Contains("-") == true)
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == Convert.ToChar(45) && cmbClaimNo.Text.Contains("-") == false)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 13)
            {
                getValidClaimDetails();
            }
        }

        private void btnSearchInsuranceCompany_Click(object sender, EventArgs e)
        {

            try
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.InsuranceCompany, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Insurance Company";
                _CurrentControlType = gloListControl.gloListControlType.InsuranceCompany;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }

        }

        private void btnClearInsCompany_Click(object sender, EventArgs e)
        {
            txtInsCompany.Text = "";
            txtInsCompany.Tag = 0;
        }

        private void cmbClaimNo_Leave(object sender, EventArgs e)
        {
            getValidClaimDetails();
        }

        private void btnSearchProvider_Click(object sender, EventArgs e)
        {
            try
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Provider";

                _CurrentControlType = gloListControl.gloListControlType.Providers;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
            txtProvider.Text = "";
            txtProvider.Tag = 0;
        }

        private void cmbBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                combo = cmbBusinessCenter;
                if (Convert.ToString(cmbBusinessCenter.SelectedValue) != "" && Convert.ToString(cmbBusinessCenter.SelectedValue) != "0")
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBusinessCenter_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbBusinessCenter;
                if (Convert.ToString(cmbBusinessCenter.SelectedValue) != "" && Convert.ToString(cmbBusinessCenter.SelectedValue) != "0")
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearBusinessCenter_Click(object sender, EventArgs e)
        {
            cmbBusinessCenter.SelectedValue = 0;
        }

        #endregion

        #region " Private and Public Methods "

        private bool SaveValidation()
        {
            bool _retValue = true;
            decimal _ReserveAmount = 0;
            try
            {
                _ReserveAmount = Convert.ToDecimal(txtAmount.Text);
            }
            catch
            {
                MessageBox.Show("Please enter a valid amount.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog("Please enter a valid amount", false);
                _retValue = false;
            }
            return _retValue;
        }

        /// <summary>
        /// To Update the Reserve Notes if Open for Modify
        /// </summary>
        public void UpdateReserveNote()
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                #region "Add Reserve Association Entry"

                oParameters.Clear();
                oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBPaymentDetailID", EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", ReserveDetails.MSTTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTrackTrnID", ReserveDetails.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", ReserveDetails.PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.ExecuteScalar("BL_INUP_Reserve_Association", oParameters);
                oDB.Disconnect();

                #endregion

                #region "Update Note"


                string _strSQL = "";

                _strSQL = " UPDATE BL_EOB_Notes SET sNoteDescription = '" + ReserveDetails.ReserveNote.Replace("'", "''") + "' WHERE nEOBPaymentID= " + EOBPaymentID +
                    " AND nPaymentNoteType = 3 AND nPaymentNoteSubType = 1 AND nEOBPaymentDetailID = " + _EOBPaymentDetailID;

                oDB.Connect(false);
                oDB.ExecuteScalar_Query(_strSQL);
                oDB.Disconnect();

                #endregion

                #region "Update Provider"

                if (txtProvider.Tag != null)
                {
                    oDB.Connect(false);
                    _strSQL = " UPDATE BL_Reserve_Association SET nProviderID = " + Convert.ToInt64(txtProvider.Tag.ToString()) +
                       " WHERE nEOBPaymentID = " + EOBPaymentID + " AND nEOBPaymentDetailID = " + _EOBPaymentDetailID;
                    oDB.Execute_Query(_strSQL);
                    oDB.Disconnect();
                }
                #endregion "Update Provider"

                #region "Update Business Center"

                if (Convert.ToString(cmbBusinessCenter.SelectedValue) != "" && Convert.ToString(cmbBusinessCenter.SelectedValue) != "0")
                {
                    oDB.Connect(false);
                    _strSQL = " UPDATE BL_Reserve_Association SET nBusinessCenterID = " + Convert.ToInt64(cmbBusinessCenter.SelectedValue) +
                       " WHERE nEOBPaymentID = " + EOBPaymentID + " AND nEOBPaymentDetailID = " + _EOBPaymentDetailID;
                    oDB.Execute_Query(_strSQL);
                    oDB.Disconnect();
                }
                
                #endregion "Update Business Center"

                if (oDB != null) 
                {
                    oDB.Disconnect();
                    oDB.Dispose(); 
                }
                if (oParameters != null)
                {
                    oParameters.Clear();
                    oParameters = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }

        }

        void ShowInsuranceReserved()
        {
            txtAmount.Enabled = false;

            //Added By Pramod Nair To Edit the Notes 
            txtNotes.Enabled = true;
            tlsbtnSaveClose.Visible = true;
            btnClearInsCompany.Enabled = false;
            btnSearchInsuranceCompany.Enabled = false;
        }

        private void getPatientClaimNos(Int64 nPatientID)
        {
            DataTable _dtClaimNo = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                string _strSql = "";
                _strSql = "SELECT CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionMasterID )+ '-' + CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionID) AS ID, "
                         + " dbo.GetSubClaimNumber(BL_Transaction_Claim_MST.nClaimNo,BL_Transaction_Claim_MST.nSubClaimNo ,BL_Transaction_Claim_MST.sMainClaimNo,5) as Claim  "
                         + " FROM BL_Transaction_Claim_MST  WITH (NOLOCK) WHERE (LEFT(nSubClaimNo,1)<> '-')  AND nPatientID = " + nPatientID + ""
                         + " ORDER BY dtCreateDate DESC";

                oDB.Connect(false);
                oDB.Retrive_Query(_strSql, out _dtClaimNo);
                oDB.Disconnect();

                if (_dtClaimNo != null && _dtClaimNo.Rows.Count > 0)
                {
                    //if (_dtClaimNo.Rows.Count > 1)
                    //{
                    DataRow dr = _dtClaimNo.NewRow();
                    dr["Claim"] = "";
                    dr["ID"] = 0;
                    _dtClaimNo.Rows.InsertAt(dr, 0);


                    cmbClaimNo.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmbClaimNo.DataSource = _dtClaimNo;
                    cmbClaimNo.DisplayMember = "Claim";
                    cmbClaimNo.ValueMember = "ID";

                    //}
                    //else
                    //{
                    //    cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
                    //    cmbClaimNo.DataSource = _dtClaimNo;
                    //    cmbClaimNo.DisplayMember = "Claim";
                    //    cmbClaimNo.ValueMember = "ID";
                    //}
                }
                else
                {
                    cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private DataTable getInsCompany(Int64 nInsuranceCompanyID)
        {
            DataTable dtInsCompany = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                if (nInsuranceCompanyID != 0)
                {
                    string _strSql = "";
                    _strSql = "SELECT ISNULL(sDescription,'') AS sInsName FROM Contacts_InsuranceCompany_MST  WITH (NOLOCK) " +
                              " WHERE nID = " + nInsuranceCompanyID + "";

                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSql, out dtInsCompany);
                    oDB.Disconnect();
                }
                return dtInsCompany;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
               

            }
        }

        private bool getValidClaimDetails()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, "");
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                if (cmbClaimNo.Text.StartsWith("-"))
                {
                    MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPatient.Text = "";
                    cmbClaimNo.Text = "";
                    return false;
                }
                else
                {
                    ogloBilling.ClaimNumber = cmbClaimNo.Text;
                    ogloBilling.SetClaimNumbers();

                    if (ogloBilling.MainClaimNumber != 0 || ogloBilling.SubClaimNumber != "")
                    {
                        ClaimDetails = new SplitClaimDetails(ogloBilling.MainClaimNumber, ogloBilling.SubClaimNumber);

                        if (!ClaimDetails.IsClaimExist)
                        {
                            MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPatient.Text = "";
                            return false;
                        }
                        else
                        {

                            DataTable dtTransactionID = new DataTable();
                            string _strSql = "";

                            if (ogloBilling.SubClaimNumber == "")
                            {
                                _strSql = " SELECT top(1) BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                                    + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                                    + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                                    + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                                    + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + " order by dtcreateDate asc";
                            }
                            else
                            {
                                _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                                        + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                                        + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                                        + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                                        + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + " AND nSubClaimNo = " + ogloBilling.SubClaimNumber;
                            }
                            oDB.Connect(false);
                            oDB.Retrive_Query(_strSql, out dtTransactionID);
                            oDB.Disconnect();
                            if (oDB != null) { oDB.Dispose(); oDB = null; }

                            cmbClaimNo.Tag = Convert.ToString(Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionMasterID"])) + '-' + Convert.ToString(Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionID"]));
                            txtPatient.Text = "";
                            txtPatient.Text = Convert.ToString(dtTransactionID.Rows[0]["Patient"]);
                            txtPatient.Tag = Convert.ToInt64(dtTransactionID.Rows[0]["nPatientID"]);

                        }
                    }


                } return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }

        }

        private bool IsInsuranceCompanySelected()
        {
            if (txtInsCompany.Tag == null || Convert.ToInt64(txtInsCompany.Tag).Equals(0))
            {
                MessageBox.Show("Please select Insurance Company.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        public void setData()
        {
            try
            {
                ReserveDetails = new gloAccountsV2.PaymentCollection.Reserves();

                ReserveDetails.InsCompanyName = txtInsCompany.Text;
                ReserveDetails.InsCompanyID = Convert.ToInt64(txtInsCompany.Tag);
                ReserveDetails.PatientID = Convert.ToInt64(txtPatient.Tag);
                ReserveDetails.ProviderID = Convert.ToInt64(txtProvider.Tag);
                ReserveDetails.ProviderName = txtProvider.Text;
                ReserveDetails.PatientName = txtPatient.Text;
                ReserveDetails.ClaimNo = cmbClaimNo.Text;
                ReserveDetails.ReserveAmount = Convert.ToDecimal(txtAmount.Text);
                ReserveDetails.ReserveNote = txtNotes.Text;
                if (Convert.ToString(cmbBusinessCenter.SelectedValue) != "" && Convert.ToString(cmbBusinessCenter.SelectedValue) != "0")
                {
                    ReserveDetails.BusinessCenterID = Convert.ToInt64(cmbBusinessCenter.SelectedValue);
                }
                if (cmbClaimNo.SelectedValue != null && cmbClaimNo.SelectedValue.ToString() != "0")
                {
                    string[] IDs = cmbClaimNo.SelectedValue.ToString().Split('-');
                    if (IDs.Length.Equals(2))
                    {
                        ReserveDetails.MSTTransactionID = Convert.ToInt64(IDs[0]);
                        ReserveDetails.TransactionID = Convert.ToInt64(IDs[1]);
                    }
                }
                else if (cmbClaimNo.Text != null && Convert.ToString(cmbClaimNo.Text) != "")
                {
                    string[] IDs = cmbClaimNo.Tag.ToString().Split('-');
                    if (IDs.Length.Equals(2))
                    {
                        ReserveDetails.MSTTransactionID = Convert.ToInt64(IDs[0]);
                        ReserveDetails.TransactionID = Convert.ToInt64(IDs[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
        }

        public void getData()
        {
            try
            {
                if (ReserveDetails != null)
                {
                    txtInsCompany.Text = ReserveDetails.InsCompanyName;
                    txtInsCompany.Tag = Convert.ToString(ReserveDetails.InsCompanyID);
                    txtPatient.Text = ReserveDetails.PatientName;
                    txtPatient.Tag = Convert.ToString(ReserveDetails.PatientID);
                    txtProvider.Text = ReserveDetails.ProviderName;
                    txtProvider.Tag = ReserveDetails.ProviderID;
                    getPatientClaimNos(ReserveDetails.PatientID);
                    cmbClaimNo.Text = ReserveDetails.ClaimNo;
                    cmbClaimNo.Tag = Convert.ToString(ReserveDetails.MSTTransactionID) + '-' + Convert.ToString(ReserveDetails.TransactionID);
                    txtAmount.Text = ReserveDetails.ReserveAmount.ToString("#0.00");
                    txtNotes.Text = ReserveDetails.ReserveNote.Trim();
                    mskCloseDate.Text  = ReserveDetails.CloseDateOffset.ToString();
                    lblPaymentTray.Text = ReserveDetails.PaymentTryDesc.ToString();
                    lblPaymentTray.Tag = ReserveDetails.PaymentTryID.ToString();    
 
                    FillBusinessCenter();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
        }

        private void FillBusinessCenter()
        {
            #region "Fill Business Center Combo box"
            try
            {
                DataTable dtBusinessCenter = new DataTable();
                dtBusinessCenter = gloGlobal.gloPMMasters.GetBusinessCenter();

                if (dtBusinessCenter != null)
                {
                    DataRow dr = dtBusinessCenter.NewRow();
                    dr["BusinessCenter"] = "";
                    dr["nBusinessCenterID"] = 0;

                    dtBusinessCenter.Rows.InsertAt(dr, 0);

                    cmbBusinessCenter.ValueMember = "nBusinessCenterID";
                    cmbBusinessCenter.DisplayMember = "BusinessCenter";
                    cmbBusinessCenter.DataSource = dtBusinessCenter.Copy();

                    Int64 _DefaultBusinessCenter = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);

                    if (ReserveDetails.ClaimsAccountID > 0 && !gloInsurancePaymentV2.IsBusinessCenterAssociated(ReserveDetails.ClaimsAccountID)
                        && ReserveDetails.BusinessCenterID == 0)
                    {
                        cmbBusinessCenter.SelectedValue = 0;
                    }
                    else
                    {
                        if (_DefaultBusinessCenter > 0 && ReserveDetails.BusinessCenterID == 0)
                        { cmbBusinessCenter.SelectedValue = _DefaultBusinessCenter; }
                        else
                        { cmbBusinessCenter.SelectedValue = ReserveDetails.BusinessCenterID; }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            #endregion "Fill Business Center Combo box"
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g != null)
            {
                SizeF s = g.MeasureString(_text, cmbBusinessCenter.Font);
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
                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth)
                            this.toolTip.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                    }
                    else
                    {
                        toolTip.Hide(combo);
                    }
                }
                else
                {
                    toolTip.Hide(combo);
                }
                e.DrawFocusRectangle();
            }
        }

        private bool SavePaymentOffeset()
        {
            bool _IsDataSaved = true;
            gloAccountsV2.gloInsurancePaymentV2 ObjaccPayment = new gloAccountsV2.gloInsurancePaymentV2();
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(AppSettings.ConnectionStringPM, "");
            DataTable _dtUniqueIDs = new DataTable();
            DataTable _dtUniqueIDsCorrection = new DataTable();
            DataTable _dtUniqueCreditID = new DataTable();
            Int32 row_num = 0;
          //  Int64 _nEOBID = 0;
            Int64 _nCreditID = 0;
       //     Boolean _bIsUsedReserveVal = false;
            try
            {
                dsInsurancePayment_TVP = new gloBilling.gloAccountPayment.dsPaymentTVP_V2();
                if (true)
                {
                    #region "Master Data"
                    _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                    SetCreditsDetails(dsInsurancePayment_TVP, _nCreditID);
                    //Adding Unique KEY for Credit ID
                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                    { _nCreditID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString()); }
                    dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"] = _nCreditID;


                    #endregion "Master Data"


                    if (true)
                    {
                        decimal _reserveAmt = 0;
                        _reserveAmt = ReserveDetails.ReserveAmount;

                        //0 ReserveAmount 
                        //1 ReserveNote 
                        //3 ReserveNoteOnPrint 

                        if (ReserveDetails.ReserveAmount > 0)
                        {
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows.Add();
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveID"] = 0;
                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                            {
                                dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCredits_RefID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                            }
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dReserveAmount"] = _reserveAmt;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveType"] = ReserveEntryTypeV2.InsuraceReserve.GetHashCode();
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nInsCompanyID"] = ReserveDetails.InsCompanyID;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPatientID"] = ReserveDetails.PatientID;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;

                            //Need to decide 
                            //if (mskCloseDate.MaskCompleted == true)
                            //{
                            //    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCloseDate"] = DateTime.Today; //Convert.ToDateTime(mskCloseDate.Text);
                            //}
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nVoidType"] = 0;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPAccountID"] = 0;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nGuarantorID"] = 0;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nAccountPatientID"] = 0;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sReserveNote"] = "PaymentOffset";
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                            dsInsurancePayment_TVP.Tables["Reserves"].AcceptChanges();

                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Add();
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTransactionID"] = ReserveDetails.MSTTransactionID;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTrackTrnID"] = ReserveDetails.TransactionID;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nPatientID"] = ReserveDetails.PatientID;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nProviderID"] = ReserveDetails.ProviderID;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nBusinessCenterID"] = ReserveDetails.BusinessCenterID;

                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].AcceptChanges();
                            #region "General Note"

                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                            {
                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                            }
                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                            {
                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                            }

                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = ReserveDetails.ReserveNote.Trim();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = ReserveDetails.ReserveAmount;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_InsuranceReserved.GetHashCode();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.MasterPayment.GetHashCode();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = false;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = DBNull.Value;// gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();

                            #endregion
                            EOBPaymentID = ObjaccPayment.SavePayment(dsInsurancePayment_TVP);

                        }
                    }

                }




            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _IsDataSaved = false;
            }
            finally
            {
                if (ObjaccPayment != null) { ObjaccPayment.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (oSplitClaimDetails != null) { oSplitClaimDetails.Dispose(); }
                if (_dtUniqueIDs != null) { _dtUniqueIDs = null; }
            }

            return _IsDataSaved;
        }

        private void SetCreditsDetails(gloBilling.gloAccountPayment.dsPaymentTVP_V2 dsInsurancePayment_TVP, Int64 _nCreditID)
        {

            dsInsurancePayment_TVP.Tables["Credits"].Rows.Add();
            if (_nCreditID != 0)
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
            }
            else
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nCreditID"] = 0;
            }
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sReceiptNo"] = "Payment Offset";
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dReceiptAmount"] = ReserveDetails.ReserveAmount;

            //need to check
            //if (mskCheckDate.MaskCompleted)
            //{
            //    mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtReceiptDate"] = DateTime.Today;// Convert.ToDateTime(mskCheckDate.Text);
            //}

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerType"] = PayerTypeV2.Insurance.GetHashCode();
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerID"] = ReserveDetails.InsCompanyID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPayerName"] = ReserveDetails.InsCompanyName;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNo"] = 000;// lblPaymetNo.Text.Trim().Split('#')[1];
            //None = 0,
            //Cash = 1,
            //Check = 2,
            //MoneyOrder = 3,
            //CreditCard = 4,
            //EFT = 5

            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 2; }

            //if (mskCloseDate.MaskCompleted == true)
            //{
            //    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtCloseDate"] = DateTime.Today;// Convert.ToDateTime(mskCloseDate.Text);
            //}
            if (lblPaymentTray.Tag.ToString()  != "")
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentTrayID"] = Convert.ToInt64(lblPaymentTray.Tag);
            }
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentTrayDesc"] = lblPaymentTray.Text;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nVoidType"] = 0;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPAccountID"] = 0;// this.nPAccountID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nAccountPatientID"] = 0;// this.nAccountPatientID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nGuarantorID"] = 0;// this.nGuarantorID;


            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNote"] = "Payment Note";
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = "Blank Tray";

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsurancetOffset.GetHashCode();

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["Credits_EXTID"] = 0;
            //if (SelectedPaymentMode == PaymentModeV2.CreditCard)
            //{
            //    dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["CreditCardType"] = "";// cmbCardType.Text.Trim();
            //    dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["AuthorizationNo"] = "";// txtCardAuthorizationNo.Text.Trim();
            //    if (mskCreditExpiryDate.MaskCompleted)
            //    {
            //        mskCreditExpiryDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            //        dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sCreditCardExpiryDate"] = "";// mskCreditExpiryDate.Text;
            //    }
            //    if (mskCreditExpiryDate.MaskCompleted)
            //    {
            //        mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //    }
            //}
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["ClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["PaymentVoidDateTime"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["CreatedDateTime"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["ModifiedDateTime"] = DBNull.Value;

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["SiteID"] = "";
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["IsFinished"] = false;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["IsERAPost"] = false;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["BPRID"] = DBNull.Value;

            dsInsurancePayment_TVP.Tables["Credits"].AcceptChanges();


        }
        #endregion

        private void btnSelectPaymentTray_Click(object sender, EventArgs e)
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
        private void FillCloseDateandPaymentTray()
        {
        try
            {
                SetCloseDate();
              FillPaymentTray();
            
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetCloseDate()
        {
            try
            {
                mskCloseDate.Text = gloBilling.gloBilling.GetUserWiseCloseDay(gloGlobal.gloPMGlobal.UserID, CloseDayType.Payment);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _defaultTrayId = Convert.ToInt64(_retVal); }

                #endregion " ... Get the default Payment Tray .... "
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                

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

                //if (lblPaymentTray.Tag != null && lblPaymentTray.Tag.ToString() != "")
                //{ VoidTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString()); }
                //VoidTrayCode = "";
                //VoidTrayName = lblPaymentTray.Text.Trim();
                oDB.Disconnect();
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); MessageBox.Show(ex.Message, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (_retVal != null) { _retVal = null; }
                if (ogloValidateUser != null)
                {
                    ogloValidateUser.Dispose();
                    ogloValidateUser = null;
                }
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
                            MessageBox.Show("Please enter the valid close date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                        else if (mskCloseDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        {
                            if (gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString()) < _ncloseDate)
                            {
                                MessageBox.Show("Void close date must be on or after " + gloDateMaster.gloDate.DateAsDate(_ncloseDate).ToShortDateString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                    }
                    else if (((MaskedTextBox)sender).Name == mskCloseDate.Name)
                    {
                        MessageBox.Show("Please enter the close date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Please enter the valid close date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                //ex.ToString();
                //ex = null;
            }
        }


    }

}
