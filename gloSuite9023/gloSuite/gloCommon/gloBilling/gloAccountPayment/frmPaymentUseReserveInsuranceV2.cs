using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Data.SqlClient;
using gloSettings;
using gloBilling;
using gloGlobal;

namespace gloAccountsV2
{
    public partial class frmPaymentUseReserveInsuranceV2: Form
    {

        #region " Private Variables "

        private Int64 _InsuranceCompanyID = 0;
        private decimal _AmountTakenFromReserve = 0;
    //    private decimal _ResAmtFrmSameCheck = 0;
        private DateTime _closeDate = DateTime.Now;
        private bool _IsFormLoading = false;
      //  bool _IsGenerateReserve = false;
        public gloAccountsV2.PaymentCollection.CreditsDTL ReservesLines = new PaymentCollection.CreditsDTL();
        public gloAccountsV2.PaymentCollection.Credit Reserves = new PaymentCollection.Credit();
        bool IsProviderEnable = false;
        bool IsBusinessCenterEnable = false;
        Int64 _EOBPaymentDetailID = 0;
        Int64 _EOBPaymentID = 0;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        public bool _IsUseNowValidate =false;
        public bool _IsReserveOpenForModify = false;
        ComboBox combo;
        ToolTip toolTip = new ToolTip();
        private gloAccountsV2.PaymentCollection.Reserves _ReserveDetails = new PaymentCollection.Reserves();
        #endregion " Private Variables "

        #region  " Grid Constants "
        
        const int COL_CREDITID = 0;
        const int COL_EOBPAYMENTDTLID = 1;
        const int COL_INSURANCE_COMPANY_ID = 2;
        const int COL_INSURANCE_COMPANY_NAME = 3; // Insurance Name will come
        const int COL_ORIGINALPAYMENT = 4;//Check Number,Date,Amount
        
        const int COL_PROVIDERID = 5;
        const int COL_PROVIDERNAME = 6;

        const int COL_ASSO_PATIENTID = 7;
        const int COL_ASSO_PATIENTNAME = 8;
        const int COL_BUSINESSCENTER_ID = 9;
        const int COL_BUSINESSCENTER_CODE = 10;
        const int COL_ASSO_CLAIMNO = 11;
        const int COL_ASSO_TRACKTRANSACTIONID = 12;
        const int COL_ASSO_MSTTRANSACTIONID = 13;
        const int COL_TORESERVES = 14;//Amount for reserve
        const int COL_CREATED_DATE_USER = 15;
        const int COL_NOTE = 16;//Note
        const int COL_AVAILABLE = 17;//Available amount
        const int COL_USERESERVE = 18;//Used Reserve
        const int COL_USENOW = 19;//Current amount to use from avaiable amount
        const int COL_RES_EOBPAYID = 20;
        const int COL_ASSO_SELECT = 21;
        const int COL_EntryType = 22;
        const int COL_CloseDate=23;
        const int COL_PaymentTryID = 24;
        const int COL_PaymentTryDesc = 25;
        

        const int COL_COUNT = 26;
        #endregion 

        #region " Property Procedures "

        public Int64 InsuranceCompanyID
        {
            get { return _InsuranceCompanyID; }
            set { _InsuranceCompanyID = value; }
        }

        public decimal AmountTakenFromReserve
        {
            get { return _AmountTakenFromReserve; }
            set { _AmountTakenFromReserve = value; }
        }

        Int64 _PatientID = 0;
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        string _PatientName = "";
        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }

        Int64 _ProviderID = 0;
        public Int64 ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }

        string _ProviderName = "";
        public string ProviderName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }

        Int64 _nLoadedClaimPatientID = 0;
        public Int64 LoadedClaimPatientID
        {
            get { return _nLoadedClaimPatientID; }
            set { _nLoadedClaimPatientID = value; }
        }

        string _sLoadedClaimPatientName = "";
        public string LoadedClaimPatientName
        {
            get { return _sLoadedClaimPatientName; }
            set { _sLoadedClaimPatientName = value; }
        }

        public Int64 LoadedCheckEOBPaymentID
        {
            get { return _EOBPaymentID; }
            set { _EOBPaymentID = value; }
        }

        public Int64 LoadedCheckEOBPaymentDetailID
        {
            get { return _EOBPaymentDetailID; }
            set { _EOBPaymentDetailID = value; }
        }

        string _ClaimNo = "";
        public string ClaimNo
        {
            get { return _ClaimNo; }
            set { _ClaimNo = value; }
        }

        Int64 _nMSTTransactionID = 0;
        public Int64 MSTTransactionID
        {
            get { return _nMSTTransactionID; }
            set { _nMSTTransactionID = value; }
        }

        Int64 _nTransactionID = 0;
        public Int64 TransactionID
        {
            get { return _nTransactionID; }
            set { _nTransactionID = value; }
        }

        public Int64 BusinessCenterID { get; set; }

        private SplitClaimDetails _ClaimDetails = new SplitClaimDetails();
        private SplitClaimDetails ClaimDetails
        {
            get { return _ClaimDetails; }
            set
            {
                _ClaimDetails = value;

                cmbClaimNo.Text = string.Empty;
                if (_ClaimDetails.IsClaimExist)
                { 
                    cmbClaimNo.Text = _ClaimDetails.ClaimDisplayNo; }
            }
        }
        public gloAccountsV2.PaymentCollection.Reserves ReserveDetails
        {

            get { return _ReserveDetails; }
            set { _ReserveDetails = value; }

        }    
        #endregion " Property Procedures "

        #region " Constructors "

        public frmPaymentUseReserveInsuranceV2(string DatabaseConnectionString,Int64 InsuranceCompanyID)
        {
            InitializeComponent();
            _InsuranceCompanyID =InsuranceCompanyID;
            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }

        #endregion " Constructors "

        #region " Form Events "

        private void frmPaymentUseReserve_Load(object sender, EventArgs e)
        {
            try
            {
                gloC1FlexStyle.Style(c1Reserve, false);
                gloC1FlexStyle.Style(c1SelectReserve, false);
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);

                IsProviderEnable = gloAccountsV2.gloBillingCommonV2.IsInsuranceReserve_ProviderEnable();
                IsBusinessCenterEnable = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_PatientAccount");
                if (IsBusinessCenterEnable) { FillBusinessCenter(); pnlBusinessCenter.Visible = true; }
                FillSelectReserves();               
                FillReserves();
                
                if (c1Reserve.Rows.Count > 1) { c1Reserve.Select(1, COL_USENOW); }

                DataTable dtInsCompany = new DataTable();
                dtInsCompany = getInsCompany(_InsuranceCompanyID);
                if (dtInsCompany.Rows.Count > 0)
                {
                    txtInsCompany.Text = dtInsCompany.Rows[0]["sInsName"].ToString();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void frmPaymentUseReserve_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (this.DialogResult != DialogResult.OK) { this.DialogResult = DialogResult.Cancel; }
        }
           
        #endregion 

        #region "Form control events"

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
                }
                oListControl = new gloListControl.gloListControl( gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.InsuranceCompany, false, this.Width);
                oListControl.ClinicID = gloPMGlobal.ClinicID;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void btnClearInsCompany_Click(object sender, EventArgs e)
        {
            txtInsCompany.Text = "";
            InsuranceCompanyID = 0;
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
                }
                oListControl = new gloListControl.gloListControl( gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.Patient, false, this.Width);
                oListControl.ClinicID = gloPMGlobal.ClinicID;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            try
            {
                txtPatient.Text = "";
                txtPatient.Tag = 0;
               // cmbClaimNo.Items.Clear();
                cmbClaimNo.DataSource = null;
                cmbClaimNo.Items.Clear();
                cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
                cmbClaimNo.Text = "";
                PatientID = 0;
                PatientName = "";
                ClaimNo = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }       

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
                }
                oListControl = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.Providers, false, this.Width);
                oListControl.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
            txtProvider.Text = "";
            txtProvider.Tag = 0;
            ProviderID = 0;
            ProviderName = "";
        }

        private void cmbClaimNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Claim# validation
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

        #endregion

        #region "User control events"

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;
            try
            {
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
                                InsuranceCompanyID = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
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
                                PatientID = Convert.ToInt64(txtPatient.Tag);
                                PatientName = txtPatient.Text;
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
                                ProviderID = Convert.ToInt64(txtProvider.Tag);
                                ProviderName = txtProvider.Text;
                            }

                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
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
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        #endregion

        #region "Toolstrip button events "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            c1Reserve.FinishEditing();
            ValidatePaymentReserve();

            if (AmountTakenFromReserve >= 0)
            { this.DialogResult = DialogResult.OK; }
            else
            { this.DialogResult = DialogResult.Cancel; }

            this.Close();
        }

        private void tsb_GenerateReserve_Click(object sender, EventArgs e)
        {

            bool _isUseNowAmountExist = false;

            try
            {
               
                    if (c1Reserve.Rows.Count > 0)
                    {
                        for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
                        {

                            if (c1Reserve.GetData(i, COL_USENOW) != null && Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).ToString().Trim() != "")
                            {
                                if (Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW)) > 0 && InsuranceCompanyID != 0 && InsuranceCompanyID != Convert.ToDecimal(c1Reserve.GetData(i, COL_INSURANCE_COMPANY_ID)))
                                {
                                    _isUseNowAmountExist = true;
                                }
                            }
                        }
                    }

                    if (_isUseNowAmountExist == true)
                    {
                        DialogResult _dialogResult;
                        _dialogResult = MessageBox.Show("Selected insurance reserve’s company does not match insurance payment’s company. Please verify.", "gloPM", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (_dialogResult == DialogResult.OK)
                        {
                            //_IsGenerateReserve = true;

                            bool _IsValidClaim = false;
                            _IsValidClaim = getValidClaimDetails();

                            if (_IsValidClaim)
                            {
                                FillSelectReserves();
                            }
                        }
                        else
                        {
                            c1SelectReserve.Clear();
                            c1SelectReserve.DataSource = null;
                            c1SelectReserve.Rows.Count = 1;
                        }
                    }
                
                else
                {
                    //_IsGenerateReserve = true;

                    bool _IsValidClaim = false;
                    _IsValidClaim = getValidClaimDetails();

                    if (_IsValidClaim)
                    {
                        FillSelectReserves();
                    }
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;

            }
            finally
            {
                //_IsGenerateReserve = false;
            }

            #region "Commented Code"

            //    DataTable _dtReserves = GetReserves();
            //    DesignPaymentGrid(c1Reserve);
            //    decimal _objAmount = 0;
            //    decimal _db_objAmount = 0;
            //    decimal _avaible = 0;
            //    decimal _dbReserves = 0;

            //    try
            //    {

            //        int _rowIndex = 0;

            //        this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);

            //        foreach (DataRow row in _dtReserves.Rows)
            //        {
            //            _rowIndex = c1Reserve.Rows.Add().Index;
            //            _objAmount = 0;
            //            _db_objAmount = 0;
            //            _avaible = 0;
            //            _dbReserves = 0;

            //            #region " Set Data "
            //            c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(row["nEOBPaymentID"]));
            //            c1Reserve.SetData(_rowIndex, COL_EOBID, Convert.ToInt64(row["nEOBID"]));
            //            c1Reserve.SetData(_rowIndex, COL_EOBDTLID, Convert.ToInt64(row["nEOBDtlID"]));
            //            c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(row["nEOBPaymentDetailID"]));
            //            c1Reserve.SetData(_rowIndex, COL_BLTRANSACTIONID, Convert.ToInt64(row["nBillingTransactionID"]));
            //            c1Reserve.SetData(_rowIndex, COL_BLTRANDTLID, Convert.ToInt64(row["nBillingTransactionDetailID"]));
            //            c1Reserve.SetData(_rowIndex, COL_BLTRANLINEID, Convert.ToString(row["nBillingTransactionLineNo"]));
            //            c1Reserve.SetData(_rowIndex, COL_DOSFROM, Convert.ToInt64(row["nDOSFrom"]));
            //            c1Reserve.SetData(_rowIndex, COL_DOSTO, Convert.ToString(row["nDOSTo"]));
            //            c1Reserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_ID, Convert.ToString(row["nAccountID"]));
            //            c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(row["InsuarnceCompanyName"]));// Insurance Name

            //            string _originalPayment = "";
            //            _originalPayment = ((EOBPaymentMode)Convert.ToInt32(row["nPayMode"])).ToString() + "# " + Convert.ToString(row["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nCheckDate"])).ToString("MM/dd/yyyy") + " $ " + Convert.ToDecimal(row["nCheckAmount"]);
            //            c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

            //            c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(row["nAmount"]));
            //            //c1Reserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(row["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
            //            c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(row["sNoteDescription"]));//Note

            //            //if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
            //            //{
            //            //    for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
            //            //    {
            //            //        if (
            //            //            EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID  == Convert.ToInt64(row["nEOBPaymentID"]) 
            //            //            && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == Convert.ToInt64(row["nEOBPaymentDetailID"])
            //            //            && EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true)
            //            //        {
            //            //            if (EOBInsurancePaymentMasterLines[index].EOBPaymentID > 0 && EOBInsurancePaymentMasterLines[index].EOBPaymentDetailID > 0)
            //            //            { _objAmount += EOBInsurancePaymentMasterLines[index].Amount; }
            //            //            else
            //            //            { _db_objAmount += EOBInsurancePaymentMasterLines[index].Amount; }
            //            //        }
            //            //    }
            //            //}

            //            if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
            //            {
            //                for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
            //                {
            //                    if (
            //                        EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == Convert.ToInt64(row["nEOBPaymentID"])
            //                        && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == Convert.ToInt64(row["nEOBPaymentDetailID"])
            //                        && EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true)
            //                    {
            //                        if (EOBInsurancePaymentMasterLines[index].EOBPaymentID > 0 && EOBInsurancePaymentMasterLines[index].EOBPaymentDetailID > 0)
            //                        {
            //                            _objAmount += EOBInsurancePaymentMasterLines[index].Amount;
            //                        }
            //                        else
            //                        { _db_objAmount += EOBInsurancePaymentMasterLines[index].Amount; }

            //                        _dbReserves += EOBInsurancePaymentMasterLines[index].DBReserveAmount;
            //                    }
            //                }
            //            }

            //            if (Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves <= Convert.ToDecimal(row["nAmount"]))
            //            { _avaible = Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves; }
            //            else
            //            { _avaible = Convert.ToDecimal(row["AvailableReserve"]); }

            //            //_avaible = Convert.ToDecimal(row["AvailableReserve"]) + _objAmount; 


            //            if (_db_objAmount <= 0) { _db_objAmount = _objAmount; }

            //            //c1Reserve.SetData(_rowIndex, COL_DB_AMOUNT, Convert.ToDecimal(row["AvailableReserve"]));

            //            c1Reserve.SetData(_rowIndex, COL_DB_AMOUNT, Convert.ToDecimal(row["AvailableReserve"]));

            //            //c1Reserve.SetData(_rowIndex, COL_OBJ_AMOUNT, _db_objAmount);
            //            c1Reserve.SetData(_rowIndex, COL_OBJ_AMOUNT, _dbReserves);


            //            c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(row["UsedReserve"]));//Used amount
            //            c1Reserve.SetData(_rowIndex, COL_AVAILABLE, _avaible);//Available amount
            //            c1Reserve.SetData(_rowIndex, COL_USENOW, _db_objAmount);//Current amount to use from avaiable amount

            //            c1Reserve.SetData(_rowIndex, COL_PAYMODE, ((EOBPaymentMode)Convert.ToInt32(row["nPayMode"])));
            //            c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID, Convert.ToInt64(row["nRefEOBPaymentID"]));
            //            c1Reserve.SetData(_rowIndex, COL_REFEOBPAYDTLID, Convert.ToInt64(row["nRefEOBPaymentDetailID"]));
            //            //c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nEOBPaymentID"]));
            //            //c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(row["nEOBPaymentDetailID"]));
            //            c1Reserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(row["nAccountID"]));
            //            c1Reserve.SetData(_rowIndex, COL_ACCOUNTTYPE, Convert.ToInt32(row["nAccountType"]));
            //            c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTID, Convert.ToInt64(row["nMSTAccountID"]));
            //            c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTTYPE, Convert.ToInt32(row["nMSTAccountType"]));

            //            c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nResEOBPaymentID"]));
            //            c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(row["nResEOBPaymentDetailID"]));

            //            c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(row["AssociationPatientID"]));
            //            c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTNAME, Convert.ToString(row["AssociationPatient"]));
            //            c1Reserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(row["AssociationMSTTransactionID"]));
            //            c1Reserve.SetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(row["AssociationnTransactionID"]));
            //            c1Reserve.SetData(_rowIndex, COL_ASSO_CLAIMNO, Convert.ToString(row["AssociationClaim"]));
            //            c1Reserve.SetData(_rowIndex, COL_ASSO_SELECT, false); 
            //            c1Reserve.SetData(_rowIndex, COL_CREATED_DATE_USER, Convert.ToString(row["CreatedDateUser"]));
            //            #endregion

            //            #region " Set Styles "

            //            c1Reserve.SetCellStyle(_rowIndex, COL_USENOW, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

            //            #endregion " Set Styles "

            //}
            //}
            //catch (Exception ex)
            //{ gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            //finally
            //{
            //    this.c1Reserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
            //    _IsFormLoading = false;
            //}
            //} 

            #endregion


        }
     
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
          //  RemovePaymentReserve();
            //if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.PaymentCredit.Count > 0)
            //{
            //    EOBInsurancePaymentMasterLines.Dispose();
            //}

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tsb_ShowDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1Reserve.Focused)
                {
                    if (c1Reserve != null && c1Reserve.Rows.Count > 1)
                    {

                        OpenReserveForModify(c1Reserve.RowSel, c1Reserve);
                    }
                }
                else if (c1SelectReserve.Focused)
                {
                    if (c1SelectReserve != null && c1SelectReserve.Rows.Count >1)
                    {

                        OpenReserveForModify(c1SelectReserve.RowSel, c1SelectReserve);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void tsb_ShowInsRefund_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region " Design Grid "

        private void DesignPaymentGrid(C1FlexGrid c1Payment)
        {
            try
            {
               
                _IsFormLoading = true;
                c1Payment.Redraw = false;
                c1Payment.AllowSorting = AllowSortingEnum.SingleColumn;

                c1Payment.Clear();
                c1Payment.Cols.Count = COL_COUNT;
                c1Payment.Rows.Count = 1;
                c1Payment.Rows.Fixed = 1;
                c1Payment.Cols.Fixed = 0;

                #region " Set Headers "

                c1Payment.SetData(0, COL_CREDITID,"EOBPaymentID");
                c1Payment.SetData(0, COL_INSURANCE_COMPANY_ID, "InsuranceCompanyID"); // InsuranceCompanyID will come
                c1Payment.SetData(0, COL_INSURANCE_COMPANY_NAME,"Insurance Company");// Insurance Name

                c1Payment.SetData(0, COL_ORIGINALPAYMENT,"Original Payment");//Check Number,Date,Amount
                c1Payment.SetData(0, COL_TORESERVES,"To Reserves");//Amount for reserve
                c1Payment.SetData(0, COL_NOTE,"Note");//Note
                c1Payment.SetData(0, COL_AVAILABLE,"Available");//Available amount
                c1Payment.SetData(0, COL_USERESERVE, "Used");//Used Reserve
                c1Payment.SetData(0, COL_USENOW,"Use Now");//Current amount to use from avaiable amount
                c1Payment.SetData(0, COL_RES_EOBPAYID,"ReserveID");
               
                c1Payment.SetData(0, COL_ASSO_PATIENTID, "PatientID");
                c1Payment.SetData(0, COL_ASSO_PATIENTNAME, "Patient");
                c1Payment.SetData(0, COL_BUSINESSCENTER_CODE, "BUS");
                c1Payment.SetData(0, COL_ASSO_MSTTRANSACTIONID, "nTransactionID");
                c1Payment.SetData(0, COL_ASSO_TRACKTRANSACTIONID, "nTrackTrnID");
                c1Payment.SetData(0, COL_ASSO_CLAIMNO, "Claim #");
                c1Payment.SetData(0, COL_ASSO_SELECT, "Select");
                c1Payment.SetData(0, COL_CREATED_DATE_USER, "Created");
                c1Payment.SetData(0, COL_PROVIDERID, "ProviderID");
                c1Payment.SetData(0, COL_PROVIDERNAME, "Provider");
                c1Payment.SetData(0, COL_EntryType, "EntryType");

                c1Payment.SetData(0, COL_CloseDate, "CloseDate");
                c1Payment.SetData(0, COL_PaymentTryID, "PaymentTry");
                c1Payment.SetData(0, COL_PaymentTryDesc, "PaymentTryDesc");
                
                #endregion

                #region " Show/Hide "

                c1Payment.Cols[COL_INSURANCE_COMPANY_NAME].Visible = true;
                c1Payment.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1Payment.Cols[COL_TORESERVES].Visible = true;
                c1Payment.Cols[COL_NOTE].Visible = true;
                c1Payment.Cols[COL_AVAILABLE].Visible = true;
                c1Payment.Cols[COL_USENOW].Visible = true;

                c1Payment.Cols[COL_CREDITID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_INSURANCE_COMPANY_ID].Visible = false;// 0;
                c1Payment.Cols[COL_INSURANCE_COMPANY_NAME].Visible = true;// 100;
                
                c1Payment.Cols[COL_USERESERVE].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYID].Visible = false;
                c1Payment.Cols[COL_ASSO_PATIENTID].Visible = false;
                c1Payment.Cols[COL_ASSO_PATIENTNAME].Visible = true;
                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].Visible = false;
                c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].Visible = false;
                c1Payment.Cols[COL_ASSO_CLAIMNO].Visible = true;
                c1Payment.Cols[COL_PROVIDERID].Visible = false;
                c1Payment.Cols[COL_PROVIDERNAME].Visible = true;
                c1Payment.Cols[COL_EntryType].Visible = false;

                c1Payment.Cols[COL_CloseDate].Visible = false;
                c1Payment.Cols[COL_PaymentTryID].Visible = false;
                c1Payment.Cols[COL_PaymentTryDesc].Visible = false;

                if (c1Payment.Name == "c1SelectReserve")
                {
                    c1Payment.Cols[COL_CREATED_DATE_USER].Visible = false;
                    c1Payment.Cols[COL_ASSO_SELECT].Visible = true;
                    c1Payment.Cols[COL_USENOW].Visible = false;// 100;
                }
                else
                {
                    c1Payment.Cols[COL_CREATED_DATE_USER].Visible = true;
                    c1Payment.Cols[COL_ASSO_SELECT].Visible = false;
                    c1Payment.Cols[COL_USENOW].Visible = true;// 100;
                }
                c1Payment.Cols[COL_BUSINESSCENTER_ID].Visible = false;
                c1Payment.Cols[COL_BUSINESSCENTER_CODE].Visible = false;
                if (IsBusinessCenterEnable)
                { c1Payment.Cols[COL_BUSINESSCENTER_CODE].Visible = true; }

                #endregion

                #region " Width "
                bool _designWidth = false;

                if (_designWidth == false)
                {

                    c1Payment.Cols[COL_CREDITID].Width = 0;
                    c1Payment.Cols[COL_EOBPAYMENTDTLID].Width = 0;
                    c1Payment.Cols[COL_INSURANCE_COMPANY_ID].Width = 0;
                    c1Payment.Cols[COL_TORESERVES].Width = 80;
                    if (c1Payment.Name == "c1SelectReserve")
                    {
                        c1Payment.Cols[COL_NOTE].Width = 250;
                    }
                    else
                    {
                        c1Payment.Cols[COL_NOTE].Width = 115;
                    }

                    c1Payment.Cols[COL_PROVIDERNAME].Width = 120;
                    c1Payment.Cols[COL_ASSO_PATIENTNAME].Width = 120;
                    c1Payment.Cols[COL_ASSO_CLAIMNO].Width = 70;
                    c1Payment.Cols[COL_INSURANCE_COMPANY_NAME].Width = 130;
                    c1Payment.Cols[COL_AVAILABLE].Width = 75;
                    c1Payment.Cols[COL_USENOW].Width = 75;
                    c1Payment.Cols[COL_ORIGINALPAYMENT].Width = 245;
                    c1Payment.Cols[COL_USERESERVE].Width = 0;
                    c1Payment.Cols[COL_RES_EOBPAYID].Width = 0;
                    c1Payment.Cols[COL_ASSO_PATIENTID].Width = 0;
                    
                    c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].Width =0;
                    c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].Width = 0;
                    
                    c1Payment.Cols[COL_ASSO_SELECT].Width = 45;
                    c1Payment.Cols[COL_CREATED_DATE_USER].Width = 108;
                    c1Payment.Cols[COL_PROVIDERID].Width = 0;
                    c1Payment.Cols[COL_BUSINESSCENTER_CODE].Width = 60;
                    
                    
                }

                #endregion

                #region " Data Type "

                c1Payment.Cols[COL_CREDITID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_INSURANCE_COMPANY_ID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_INSURANCE_COMPANY_NAME].DataType = typeof(System.String);
                c1Payment.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
                c1Payment.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_NOTE].DataType = typeof(System.String);
                c1Payment.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_USENOW].DataType = typeof(System.Decimal);

                c1Payment.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ASSO_PATIENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ASSO_PATIENTNAME].DataType = typeof(System.String);
                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ASSO_CLAIMNO].DataType = typeof(System.String);
                c1Payment.Cols[COL_ASSO_SELECT].DataType = typeof(System.Boolean);
                c1Payment.Cols[COL_CREATED_DATE_USER].DataType = typeof(System.String);
                c1Payment.Cols[COL_PROVIDERID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PROVIDERNAME].DataType = typeof(System.String);
                c1Payment.Cols[COL_BUSINESSCENTER_ID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BUSINESSCENTER_CODE].DataType = typeof(System.String);
                #endregion

                #region " Alignment "

                c1Payment.Cols[COL_CREDITID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_INSURANCE_COMPANY_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_USENOW].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_ASSO_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_SELECT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Payment.Cols[COL_CREATED_DATE_USER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PROVIDERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PROVIDERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BUSINESSCENTER_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle ;//= c1Payment.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1Payment.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
 

                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;// = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = c1Payment.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";
                        csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;

                }
          

                C1.Win.C1FlexGrid.CellStyle csEditableStringStyle;// = c1Payment.Styles.Add("cs_EditableStringStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableStringStyle"))
                    {
                        csEditableStringStyle = c1Payment.Styles["cs_EditableStringStyle"];
                    }
                    else
                    {
                        csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                        csEditableStringStyle.DataType = typeof(System.String);
                        csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableStringStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                    csEditableStringStyle.DataType = typeof(System.String);
                    csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableStringStyle.BackColor = Color.White;

                }
         

                C1.Win.C1FlexGrid.CellStyle csEditableDateStyle;// = c1Payment.Styles.Add("cs_EditableDateStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableDateStyle"))
                    {
                        csEditableDateStyle = c1Payment.Styles["cs_EditableDateStyle"];
                    }
                    else
                    {
                        csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                        csEditableDateStyle.DataType = typeof(System.DateTime);
                        csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableDateStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                    csEditableDateStyle.DataType = typeof(System.DateTime);
                    csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableDateStyle.BackColor = Color.White;

                }
         

                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle; // = c1Payment.Styles.Add("cs_ClaimRowStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_ClaimRowStyle"))
                    {
                        csClaimRowStyle = c1Payment.Styles["cs_ClaimRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);

                    }

                }
                catch
                {
                    csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);


                }
            
                C1.Win.C1FlexGrid.CellStyle csPatientRowStyle;// = c1Payment.Styles.Add("cs_PatientRowStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_PatientRowStyle"))
                    {
                        csPatientRowStyle = c1Payment.Styles["cs_PatientRowStyle"];
                    }
                    else
                    {
                        csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                        csPatientRowStyle.DataType = typeof(System.String);
                        csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);

                    }

                }
                catch
                {
                    csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                    csPatientRowStyle.DataType = typeof(System.String);
                    csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);


                }
  

                c1Payment.Cols[COL_TORESERVES].Style = csCurrencyStyle;
                c1Payment.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_USENOW].Style = csCurrencyStyle;

                c1Payment.Cols[COL_USERESERVE].Style = csCurrencyStyle;
                
                #endregion

                c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Payment.AllowEditing = true;

                c1Payment.Cols[COL_CREDITID].AllowEditing = false;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_INSURANCE_COMPANY_ID].AllowEditing = false;//0;
                c1Payment.Cols[COL_INSURANCE_COMPANY_NAME].AllowEditing = false;//100;
                c1Payment.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
                c1Payment.Cols[COL_TORESERVES].AllowEditing = false;//100;
                c1Payment.Cols[COL_NOTE].AllowEditing = false;//100;
                c1Payment.Cols[COL_AVAILABLE].AllowEditing = false;//100;
                c1Payment.Cols[COL_USENOW].AllowEditing = true;//100;
                c1Payment.Cols[COL_USERESERVE].AllowEditing = false;//100;
                c1Payment.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ASSO_PATIENTID].AllowEditing =false;
                c1Payment.Cols[COL_ASSO_PATIENTNAME].AllowEditing = false;
                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].AllowEditing = false;
                c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].AllowEditing = false;
                c1Payment.Cols[COL_ASSO_CLAIMNO].AllowEditing = false;
                c1Payment.Cols[COL_ASSO_SELECT].AllowEditing = true;
                c1Payment.Cols[COL_CREATED_DATE_USER].AllowEditing = false;
                c1Payment.Cols[COL_PROVIDERID].AllowEditing = false;
                c1Payment.Cols[COL_PROVIDERNAME].AllowEditing = false;
                c1Payment.Cols[COL_BUSINESSCENTER_CODE].AllowEditing = false;

                #endregion

                //c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                c1Payment.SelectionMode = SelectionModeEnum.Row;
                //c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
               
                c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.White;
                //c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                c1Payment.Cols[COL_AVAILABLE].AllowSorting = true;
                c1Payment.Cols[COL_TORESERVES].AllowSorting = true;
                c1Payment.Cols[COL_NOTE].AllowSorting = false;
                c1Payment.Cols[COL_PROVIDERNAME].AllowSorting = false;
                c1Payment.Cols[COL_ASSO_PATIENTNAME].AllowSorting = false;
                c1Payment.Cols[COL_ASSO_CLAIMNO].AllowSorting = false;
                c1Payment.Cols[COL_INSURANCE_COMPANY_NAME].AllowSorting = false;
                //c1Payment.Cols[COL_AVAILABLE].AllowSorting = false;
                c1Payment.Cols[COL_USENOW].AllowSorting = false;
                c1Payment.Cols[COL_ORIGINALPAYMENT].AllowSorting = false;
                c1Payment.Cols[COL_ASSO_SELECT].AllowSorting = false;
                c1Payment.Cols[COL_CREATED_DATE_USER].AllowSorting = false;
                c1Payment.Cols[COL_BUSINESSCENTER_CODE].AllowSorting = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
               
                _IsFormLoading = false; c1Payment.Redraw = true; 
            }
        }

        #endregion " Design Grid "

        #region " Grid Events "

        private void c1Reserve_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int _rowIndex = 0;
                _rowIndex = c1Reserve.HitTest(e.X, e.Y).Row;
                if (_rowIndex > 0)
                { 
                    OpenReserveForModify(_rowIndex,c1Reserve); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void c1Reserve_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (c1Reserve.ColSel == COL_INSURANCE_COMPANY_NAME)
                {
                    c1Reserve.Select(c1Reserve.RowSel, COL_USENOW);
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (c1Reserve.ColSel == COL_USENOW)
                {
                    c1Reserve.SetData(c1Reserve.RowSel, c1Reserve.ColSel, 0);
                }
            }
        }

        private void c1Reserve_CellChanged(object sender, RowColEventArgs e)
        {
            if (_IsFormLoading == false)
            {
                try
                {
                    this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);

                    if (e.Row > 0 && e.Col == COL_USENOW)
                    {
                        if (c1Reserve.GetData(e.Row, e.Col) != null && Convert.ToString(c1Reserve.GetData(e.Row, e.Col)).Trim() != ""
                         && c1Reserve.GetData(e.Row, COL_AVAILABLE) != null && Convert.ToString(c1Reserve.GetData(e.Row, COL_AVAILABLE)).Trim() != "")
                        {
                            // Check for negative amount
                            if (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_USENOW)) < 0)
                            {
                                try
                                {

                                    MessageBox.Show("Use amount cannot be negative. Please select a valid amount.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1Reserve.SetData(e.Row, e.Col, 0);
                                    _IsUseNowValidate = true;
                                    c1Reserve.SetCellCheck(e.Row, COL_ASSO_SELECT, CheckEnum.Unchecked);
                                }
                                catch (Exception)// ex)
                                {
                                    //ex.ToString();
                                    //ex = null;
                                }

                            }
                            else if (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_USENOW)) > Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_AVAILABLE)))
                            {
                                try
                                {

                                    MessageBox.Show("Use amount cannot be more than the available amount.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1Reserve.SetData(e.Row, e.Col, 0);
                                    _IsUseNowValidate = true;
                                    c1Reserve.SetCellCheck(e.Row, COL_ASSO_SELECT, CheckEnum.Unchecked);

                                }
                                catch (Exception)// ex)
                                {
                                    //ex.ToString();
                                    //ex = null;
                                }
                            }
                        }
                    }
             
                    if (c1Reserve.GetData(e.Row, COL_USENOW) != null && Convert.ToString(c1Reserve.GetData(e.Row, COL_USENOW)).ToString().Trim() != "")
                    {
                        if (Reserves != null && Reserves.EOBCreditDTL.Count > 0)
                        {
                            for (int index = Reserves.EOBCreditDTL.Count - 1; index >= 0; index--)
                            {
                                if (
                                     Reserves.EOBCreditDTL[index].CreditsRef_ID == Convert.ToInt64(c1Reserve.GetData(e.Row, COL_CREDITID)) &&
                                     Reserves.EOBCreditDTL[index].ReserveID ==  Convert.ToInt64(c1Reserve.GetData(e.Row, COL_RES_EOBPAYID))
                                    )

                                {
                                    Reserves.EOBCreditDTL[index].Amount = Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_USENOW));

                                }
                            }
                        }
                    }

                }

                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    this.c1Reserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
                }
            }

        }

        private void c1SelectReserve_CellChanged(object sender, RowColEventArgs e)
        {

            if (_IsFormLoading == false)
            {
                try
                {
                    this.c1SelectReserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SelectReserve_CellChanged);

                    if (e.Row > 0 && e.Col == COL_USENOW)
                    {
                        if (c1SelectReserve.GetData(e.Row, e.Col) != null && Convert.ToString(c1SelectReserve.GetData(e.Row, e.Col)).Trim() != ""
                         && c1SelectReserve.GetData(e.Row, COL_AVAILABLE) != null && Convert.ToString(c1SelectReserve.GetData(e.Row, COL_AVAILABLE)).Trim() != "")
                        {
                            // Check for negative amount
                            if (Convert.ToDecimal(c1SelectReserve.GetData(e.Row, COL_USENOW)) < 0)
                            {
                                try
                                {

                                    MessageBox.Show("Use amount cannot be negative. Please select a valid amount.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1SelectReserve.SetData(e.Row, e.Col, 0);
                                }
                                catch (Exception)// ex)
                                {
                                    //ex.ToString();
                                    //ex = null;
                                }
                            }
                            else if (Convert.ToDecimal(c1SelectReserve.GetData(e.Row, COL_USENOW)) > Convert.ToDecimal(c1SelectReserve.GetData(e.Row, COL_AVAILABLE)))
                            {
                                try
                                {
                                    MessageBox.Show("Use amount cannot be more than the available amount.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1SelectReserve.SetData(e.Row, e.Col, 0);
                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                    ex = null;
                                }
                            }
                        }
                    }
                    if (_IsUseNowValidate)
                    {
                        if (c1SelectReserve.GetCellCheck(e.Row, COL_ASSO_SELECT) == CheckEnum.Checked)
                        {
                            c1SelectReserve.SetCellCheck(e.Row, COL_ASSO_SELECT, CheckEnum.Unchecked);
                            _IsUseNowValidate = false;
                            c1SelectReserve.Select(e.Row, COL_USENOW);

                        }
                    }
                    else
                    {
                            if (e.Row > 0 && e.Col == COL_ASSO_SELECT)
                            {
                                if (c1SelectReserve.GetData(e.Row, e.Col) != null && Convert.ToString(c1SelectReserve.GetData(e.Row, e.Col)).Trim() != "")
                                {
                                    if (c1SelectReserve.GetCellCheck(e.Row, COL_ASSO_SELECT) == CheckEnum.Checked)
                                    {
                                        AddPaymentReserve(e.Row);
                                        AddSelectedReserve(e.Row);
                                        HideSelectedReserve(e.Row);
                                    }

                                }
                            }
                    }

                }

                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }
                finally
                {
                    this.c1SelectReserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SelectReserve_CellChanged);
                }
            }
        }

        private void c1SelectReserve_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int _rowIndex = 0;
                _rowIndex = c1SelectReserve.HitTest(e.X, e.Y).Row;
                if (_rowIndex > 0)
                { OpenReserveForModify(_rowIndex, c1SelectReserve); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }
      
        #endregion

        #region "Methods"

        void AddPaymentReserve(int _rowIndex)
        {          
            Int64 _selEOBPayId = 0;
            Int64 _selEOBPayDtlId = 0;
            try
            {
                c1SelectReserve.FinishEditing();

                if (c1SelectReserve.GetData(_rowIndex, COL_CREDITID) != null && Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_CREDITID)).ToString().Trim() != "")
                { _selEOBPayId = Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_CREDITID)); }

                if (c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
                { _selEOBPayDtlId = Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTDTLID)); }
                
                ReservesLines = new PaymentCollection.CreditsDTL();
                _closeDate = Convert.ToDateTime(c1SelectReserve.GetData(_rowIndex, COL_CREATED_DATE_USER).ToString().Substring(0, c1SelectReserve.GetData(_rowIndex, COL_CREATED_DATE_USER).ToString().IndexOf("-")));
                ReservesLines.Amount = Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_USENOW));
                ReservesLines.CloseDate = Convert.ToDateTime(_closeDate);
                ReservesLines.CreditsRef_ID = _selEOBPayId;
                ReservesLines.EntryType = PaymentEntryTypeV2.InsuraceReserved;
                ReservesLines.ReserveID = Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_RES_EOBPAYID));
                ReservesLines.InsuranceID = Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_INSURANCE_COMPANY_ID));
                Reserves.EOBCreditDTL.Add(ReservesLines);

                _selEOBPayId = 0;
                _selEOBPayDtlId = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }   
        }

        void AddSelectedReserve( int _rowIndex)
        {
            try
            {
                c1Reserve.Rows.Add();

                #region " Set Data "
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_CREDITID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_CREDITID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_EOBPAYMENTDTLID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTDTLID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_INSURANCE_COMPANY_ID, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_INSURANCE_COMPANY_ID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_INSURANCE_COMPANY_NAME, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_INSURANCE_COMPANY_NAME)));// Insurance Name  
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ORIGINALPAYMENT, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_ORIGINALPAYMENT)));//Check Number,Date,Amount
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_TORESERVES, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_TORESERVES)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_NOTE, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_NOTE)));//Note
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_USERESERVE, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_CREDITID)));//Used amount
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_AVAILABLE, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_AVAILABLE)));//Available amount
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_USENOW, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_AVAILABLE)));//Current amount to use from avaiable amount
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_RES_EOBPAYID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_RES_EOBPAYID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_PATIENTID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_ASSO_PATIENTID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_PATIENTNAME, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_ASSO_PATIENTNAME)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_CLAIMNO, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_ASSO_CLAIMNO)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_SELECT, false);
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_CREATED_DATE_USER, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_CREATED_DATE_USER)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_PROVIDERID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_PROVIDERID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_PROVIDERNAME, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_PROVIDERNAME)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_BUSINESSCENTER_ID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_BUSINESSCENTER_ID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_BUSINESSCENTER_CODE, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_BUSINESSCENTER_CODE)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_EntryType, Convert.ToInt16(c1SelectReserve.GetData(_rowIndex, COL_EntryType)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_CloseDate, Convert.ToDateTime (c1SelectReserve.GetData(_rowIndex, COL_CloseDate)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_PaymentTryID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_PaymentTryID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_PaymentTryDesc, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_PaymentTryDesc)));
                #endregion

                #region " Set Styles "

                c1Reserve.SetCellStyle(c1Reserve.Rows.Count - 1, COL_USENOW, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                #endregion " Set Styles "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        void HideSelectedReserve(int _rowIndex)
        {

            c1SelectReserve.Rows[_rowIndex].Visible = false; 

        }

        /// <summary>
        /// Add data in the collection
        /// 
        /// </summary>
        /// 
        void ValidatePaymentReserve()
        {
          
            try
            {
                //Reserves = new PaymentCollection.Credit();
                for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
                {

                    if (c1Reserve.GetData(i, COL_USENOW) != null && Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).ToString().Trim() != "")
                    {
                        if (Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW)) > 0)
                        {
                            AmountTakenFromReserve += Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW));

                        }
                    }
                }
                //for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
                //{
                //    ReservesLines = new PaymentCollection.CreditsDTL();
                //    if (c1Reserve.GetData(i, COL_USENOW) != null && Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).ToString().Trim() != "")
                //    {
                //        if (Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW)) > 0)
                //        {
                //            _closeDate = Convert.ToDateTime(c1Reserve.GetData(i, COL_CREATED_DATE_USER).ToString().Substring(0, c1Reserve.GetData(i, COL_CREATED_DATE_USER).ToString().IndexOf("-")));
                //            ReservesLines.Amount = Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW));
                //            ReservesLines.CloseDate = _closeDate;
                //            ReservesLines.CreditsRef_ID = Convert.ToInt64(c1Reserve.GetData(i, COL_CREDITID));
                //            ReservesLines.ReserveID = Convert.ToInt64(c1Reserve.GetData(i, COL_RES_EOBPAYID));
                //            ReservesLines.EntryType = PaymentEntryTypeV2.InsuraceReserved;
                //            ReservesLines.InsuranceID = Convert.ToInt64(c1Reserve.GetData(i, COL_INSURANCE_COMPANY_ID));
                //            Reserves.EOBCreditDTL.Add(ReservesLines);
                //            ReservesLines.Dispose();
                //        }
                //    }
                //}

                if (Reserves != null && Reserves.EOBCreditDTL.Count > 0)
                {
                    for (int index = Reserves.EOBCreditDTL.Count - 1; index >= 0; index--)
                    {
                        if (
                             Reserves.EOBCreditDTL[index].Credits_ID == 0 &&
                             (Reserves.EOBCreditDTL[index].DBReserveAmount == 0 && Reserves.EOBCreditDTL[index].Amount == 0)
                            )
                        {
                            Reserves.EOBCreditDTL.RemoveAt(index);

                        }
                    }
                }
               

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        void RemovePaymentReserve()
        {
        }
       
        DataTable GetReserves(C1FlexGrid c1Payment)
        {
            DataTable _dtReserves = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            StringBuilder sb_EOBPaymentID = new StringBuilder();
            StringBuilder sb_InsuranceID = new StringBuilder();
            try
            {
                if (c1Payment.Name == "c1SelectReserve" )
                {
                    if (chkCurrentCheck.Checked)
                    {
                        DataTable dtReserveID = getReserveID(LoadedCheckEOBPaymentID);

                        for ( int i = 0; i <= dtReserveID.Rows.Count -1; i++)
                        {
                             if (i == dtReserveID.Rows.Count - 1)
                            {
                                sb_EOBPaymentID.Append(dtReserveID.Rows[i]["nReserveID"].ToString());
                            }
                            else
                            {
                                sb_EOBPaymentID.Append(dtReserveID.Rows[i]["nReserveID"].ToString() + ",");
                            }
                        }
                        if (sb_EOBPaymentID.Length != 0)
                        {
                            oParameters.Add("@nEObPaymentID", sb_EOBPaymentID.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }
                        if (sb_EOBPaymentID.Length != 0)
                        {
                            oDB.Connect(false);
                            oDB.Retrive("BL_SELECT_PaymentTransaction_UseReserve_Insurance_V2", oParameters, out _dtReserves);
                            oDB.Disconnect();
                        }
                    }
                    else
                    {
                        if (_IsFormLoading == true && InsuranceCompanyID != 0 || PatientID != 0 || txtNoteText.Text != "" || ProviderID != 0 || BusinessCenterID != 0)
                        {
                            if (InsuranceCompanyID != 0)
                            {
                                oParameters.Add("@nInsuranceID", InsuranceCompanyID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
                            }
                            if (MSTTransactionID != 0)
                            {
                                oParameters.Add("@nMSTTransactionID", MSTTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            if (TransactionID != 0)
                            {
                                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            if (PatientID != 0)
                            {
                                oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            if (txtNoteText.Text != "")
                            {
                                oParameters.Add("@NoteText", txtNoteText.Text, ParameterDirection.Input, SqlDbType.VarChar);// NUMERIC(18,0),
                            }
                            if (ProviderID != 0)
                            {
                                oParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            if (BusinessCenterID != 0)
                            {
                                oParameters.Add("@BusinessCenterID", BusinessCenterID, ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            if (InsuranceCompanyID != 0 || PatientID != 0 || txtNoteText.Text != "" || MSTTransactionID != 0 || TransactionID != 0 || ProviderID != 0 || BusinessCenterID !=0 )
                            {
                                oDB.Connect(false);
                                oDB.Retrive("BL_SELECT_PaymentTransaction_UseReserve_Insurance_V2", oParameters, out _dtReserves);
                                oDB.Disconnect();
                            }
                        }
                    }
                   
                }
                else if (c1Payment.Name == "c1Reserve" || chkCurrentCheck.Checked)
                {

                    if (Reserves != null && Reserves.EOBCreditDTL.Count > 0)
                    {

                        for (int i = 0; i < Reserves.EOBCreditDTL.Count; i++)
                        {
                            if (i == Reserves.EOBCreditDTL.Count - 1)
                            {
                                sb_EOBPaymentID.Append(Reserves.EOBCreditDTL[i].ReserveID.ToString());
                            }
                            else
                            {
                                sb_EOBPaymentID.Append(Reserves.EOBCreditDTL[i].ReserveID.ToString() + ",");
                            }
                        }
                    }

                    if (MSTTransactionID != 0)
                    {
                        oParameters.Add("@nMSTTransactionID", MSTTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    }
                    if (TransactionID != 0)
                    {
                        oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    }
                    if (PatientID != 0)
                    {
                        oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    }
                    if (txtNoteText.Text != "")
                    {
                        oParameters.Add("@NoteText", txtNoteText.Text, ParameterDirection.Input, SqlDbType.VarChar);// NUMERIC(18,0),
                    }
                    if (sb_EOBPaymentID.Length != 0)
                    {
                        oParameters.Add("@nEObPaymentID", sb_EOBPaymentID.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    }
                    if (InsuranceCompanyID != 0)
                    {
                        oParameters.Add("@nInsuranceID", InsuranceCompanyID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
                    }
                    if (InsuranceCompanyID != 0 || PatientID != 0 || txtNoteText.Text != "" || MSTTransactionID != 0 || TransactionID != 0 || sb_EOBPaymentID.Length != 0)
                    {
                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_PaymentTransaction_UseReserve_Insurance_V2", oParameters, out _dtReserves);
                        oDB.Disconnect();
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtReserves != null) { _dtReserves.Dispose(); }
            }

            return _dtReserves;
        }

        private void FillSelectReserves()
        {
            _IsFormLoading = true;
            DataView _dv;
            DataTable _dtReserves = GetReserves(c1SelectReserve);
            if (chkPaymentOffset.Checked)
            {
                try
                {
                    _dv = new DataView(_dtReserves);
                    _dv.RowFilter = _dv.Table.Columns[COL_EntryType].ColumnName + "=12";
                    _dtReserves = _dv.ToTable().Copy();
                }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                finally
                {

                }
            }
            decimal _objAmount = 0;
            decimal _db_objAmount = 0;
            decimal _avaible = 0;
            decimal _dbReserves = 0;
            DesignPaymentGrid(c1SelectReserve);
            
     //       Int64 iRow =0;
            try
            {                
                int _rowIndex = 0;
                this.c1SelectReserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SelectReserve_CellChanged);
                if (_dtReserves.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtReserves.Rows)
                    {
                        
                            _rowIndex = c1SelectReserve.Rows.Add().Index;
                            _objAmount = 0;
                            _db_objAmount = 0;
                            _avaible = 0;
                            _dbReserves = 0;
                        
                            #region " Set Data "
                            c1SelectReserve.SetData(_rowIndex, COL_CREDITID, Convert.ToInt64(row["nEOBPaymentID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(row["nReserveID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_ID, Convert.ToString(row["InsuranceCompanyID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_NAME, Convert.ToString(row["InsuarnceCompanyName"]));// Insurance Name
                            string _originalPayment = "";
                            _originalPayment = ((PaymentModeV2)Convert.ToInt32(row["nPayMode"])).ToString() + "# " + Convert.ToString(row["CheckNumber"]) + " " + Convert.ToString(row["nCheckDate"]) + " $ " + Convert.ToDecimal(row["nCheckAmount"]);
                            c1SelectReserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount
                            c1SelectReserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(row["nAmount"]));                           
                            c1SelectReserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(row["sNoteDescription"]));//Note                        

                            if (Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves <= Convert.ToDecimal(row["nAmount"]))
                            { _avaible = Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves; }
                            else
                            { _avaible = Convert.ToDecimal(row["AvailableReserve"]); }


                            if (_db_objAmount <= 0) { _db_objAmount = _objAmount; }
                            c1SelectReserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(row["UsedReserve"]));//Used amount
                            c1SelectReserve.SetData(_rowIndex, COL_AVAILABLE, _avaible);//Available amount
                            c1SelectReserve.SetData(_rowIndex, COL_USENOW, _db_objAmount);//Current amount to use from avaiable amount
                            c1SelectReserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nReserveID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(row["AssociationPatientID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_PATIENTNAME, Convert.ToString(row["AssociationPatient"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_CLAIMNO, Convert.ToString(row["AssociationClaim"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(row["AssociationMSTTransactionID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(row["AssociationnTransactionID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_SELECT, false);
                            c1SelectReserve.SetData(_rowIndex, COL_CREATED_DATE_USER, Convert.ToString(row["CreatedDateUser"]));
                            c1SelectReserve.SetData(_rowIndex, COL_PROVIDERID, Convert.ToString(row["AssociationProviderID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_PROVIDERNAME, Convert.ToString(row["AssociationProvider"]));
                            c1SelectReserve.SetData(_rowIndex, COL_BUSINESSCENTER_CODE, Convert.ToString(row["BusinessCenterCode"]));
                            c1SelectReserve.SetData(_rowIndex, COL_BUSINESSCENTER_ID, Convert.ToString(row["BusinessCenterID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_EntryType, Convert.ToString(row["EntryType"]));

                            c1SelectReserve.SetData(_rowIndex, COL_CloseDate, Convert.ToString(row["CloseDate"]));
                            c1SelectReserve.SetData(_rowIndex, COL_PaymentTryID, Convert.ToInt64(row["PaymentTryID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_PaymentTryDesc, Convert.ToString(row["PaymentTryDesc"]));
                            #endregion

                            #region " Set Styles "

                            c1SelectReserve.SetCellStyle(_rowIndex, COL_USENOW, c1SelectReserve.Styles["cs_EditableCurrencyStyle"]);

                            #endregion " Set Styles "

                            if (Reserves != null && Reserves.EOBCreditDTL.Count > 0)
                            {
                                for (int index = 0; index < Reserves.EOBCreditDTL.Count; index++)
                                {
                                    if (Reserves.EOBCreditDTL[index].ReserveID == Convert.ToInt64(row["nReserveID"]) )
                                    {
                                        HideSelectedReserve(_rowIndex);
                                    }
                                }
                            }                       
                    }
               }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                this.c1SelectReserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SelectReserve_CellChanged);
                _IsFormLoading = false; 
            } 
        }

        private void FillReserves()
        {
            _IsFormLoading = true;
            DesignPaymentGrid(c1Reserve);
            DataTable _dtReserves = new DataTable();
            if (Reserves != null && Reserves.EOBCreditDTL.Count > 0)
            {
                _dtReserves = GetReserves(c1Reserve);
            }
            decimal _objAmount = 0;
            decimal _db_objAmount = 0;
            decimal _avaible = 0;
            decimal _dbReserves = 0;

            try
            {
                int _rowIndex = 0;
                this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);

                if (_dtReserves.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtReserves.Rows)
                    {
                        _rowIndex = c1Reserve.Rows.Add().Index;
                        _objAmount = 0;
                        _db_objAmount = 0;
                        _avaible = 0;
                        _dbReserves = 0;
                        bool _isSameReserve = false;


                        if (Reserves != null && Reserves.EOBCreditDTL.Count > 0)
                        {
                            for (int index = 0; index < Reserves.EOBCreditDTL.Count; index++)
                            {
                                if (
                                    Reserves.EOBCreditDTL[index].CreditsRef_ID == Convert.ToInt64(row["nEOBPaymentID"])&&
                                    Reserves.EOBCreditDTL[index].ReserveID == Convert.ToInt64(row["nReserveID"])
                                    )
                                {
                                    if (Reserves.EOBCreditDTL[index].CreditsRef_ID > 0 )
                                    {
                                        _objAmount += Reserves.EOBCreditDTL[index].Amount;
                                    }
                                    else
                                    {
                                        //This is done if the user select the reserve amount & want to locate against the same check from which amount has already used previosly
                                        //in this case to add the previous used amount to current used amount so use now will show total.
                                        for (int i = 0; i < Reserves.EOBCreditDTL.Count; i++)
                                        {
                                            if (Reserves.EOBCreditDTL[index].Credits_ID == Reserves.EOBCreditDTL[i].Credits_ID )
                                            {
                                                _isSameReserve = true;
                                                break;
                                            }
                                        }
                                        if (_isSameReserve)
                                        {
                                            _objAmount += Reserves.EOBCreditDTL[index].Amount;
                                        }
                                        else
                                        {
                                            _db_objAmount += Reserves.EOBCreditDTL[index].Amount;
                                        }
                                    }

                                    _dbReserves += Reserves.EOBCreditDTL[index].DBReserveAmount;
                                }
                            }
                        }
                        if (_db_objAmount <= 0) { _db_objAmount = _objAmount; }

                        if (_IsReserveOpenForModify)
                        {   
                            #region " Set Data "

                            c1Reserve.SetData(_rowIndex, COL_CREDITID, Convert.ToInt64(row["nEOBPaymentID"]));
                            c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(row["nReserveID"]));
                            c1Reserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_ID, Convert.ToString(row["InsuranceCompanyID"]));
                            c1Reserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_NAME, Convert.ToString(row["InsuarnceCompanyName"]));// Insurance Name

                            string _originalPayment = "";
                            _originalPayment = ((PaymentMode)Convert.ToInt32(row["nPayMode"])).ToString() + "# " + Convert.ToString(row["CheckNumber"]) + " " + String.Format("{0:MM/dd/yyyy}", row["nCheckDate"]) + " $ " + Convert.ToDecimal(row["nCheckAmount"]);
                            c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                            c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(row["nAmount"]));
                            c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(row["sNoteDescription"]));//Note

                            if (Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves <= Convert.ToDecimal(row["nAmount"]))
                            { _avaible = Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves; }
                            else
                            { _avaible = Convert.ToDecimal(row["AvailableReserve"]); }

                            c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(row["UsedReserve"]));//Used amount
                            c1Reserve.SetData(_rowIndex, COL_AVAILABLE, _avaible);//Available amount
                            c1Reserve.SetData(_rowIndex, COL_USENOW, _db_objAmount);//Current amount to use from avaiable amount
                            c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nReserveID"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(row["AssociationPatientID"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTNAME, Convert.ToString(row["AssociationPatient"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_CLAIMNO, Convert.ToString(row["AssociationClaim"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(row["AssociationMSTTransactionID"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(row["AssociationnTransactionID"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_SELECT, false);
                            c1Reserve.SetData(_rowIndex, COL_CREATED_DATE_USER, Convert.ToString(row["CreatedDateUser"]));
                            c1Reserve.SetData(_rowIndex, COL_PROVIDERID, Convert.ToString(row["AssociationProviderID"]));
                            c1Reserve.SetData(_rowIndex, COL_PROVIDERNAME, Convert.ToString(row["AssociationProvider"]));
                            c1Reserve.SetData(_rowIndex, COL_BUSINESSCENTER_CODE, Convert.ToString(row["BusinessCenterCode"]));
                            c1Reserve.SetData(_rowIndex, COL_BUSINESSCENTER_ID, Convert.ToString(row["BusinessCenterID"]));
                             #endregion

                            #region " Set Styles "

                            c1Reserve.SetCellStyle(_rowIndex, COL_USENOW, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                            #endregion " Set Styles "

                        }
                        else
                        {
                                #region " Set Data "
                                c1Reserve.SetData(_rowIndex, COL_CREDITID, Convert.ToInt64(row["nEOBPaymentID"]));
                                c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(row["nReserveID"]));
                                c1Reserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_ID, Convert.ToString(row["InsuranceCompanyID"]));
                                c1Reserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_NAME, Convert.ToString(row["InsuarnceCompanyName"]));// Insurance Name

                                string _originalPayment = "";
                                _originalPayment = ((PaymentModeV2)Convert.ToInt32(row["nPayMode"])).ToString() + "# " + Convert.ToString(row["CheckNumber"]) + " " + string.Format("{0:MM/dd/yyyy}", row["nCheckDate"]) + " $ " + Convert.ToDecimal(row["nCheckAmount"]);
                                c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                                c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(row["nAmount"]));
                                c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(row["sNoteDescription"]));//Note
  
                                if (Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves <= Convert.ToDecimal(row["nAmount"]))
                                { _avaible = Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves; }
                                else
                                { _avaible = Convert.ToDecimal(row["AvailableReserve"]); }
                                c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(row["UsedReserve"]));//Used amount
                                c1Reserve.SetData(_rowIndex, COL_AVAILABLE, _avaible);//Available amount
                                c1Reserve.SetData(_rowIndex, COL_USENOW, _db_objAmount);//Current amount to use from avaiable amount
                                c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nReserveID"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(row["AssociationPatientID"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTNAME, Convert.ToString(row["AssociationPatient"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_CLAIMNO, Convert.ToString(row["AssociationClaim"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(row["AssociationMSTTransactionID"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(row["AssociationnTransactionID"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_SELECT, false);
                                c1Reserve.SetData(_rowIndex, COL_CREATED_DATE_USER, Convert.ToString(row["CreatedDateUser"]));
                                c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nReserveID"]));
                                c1Reserve.SetData(_rowIndex, COL_PROVIDERID, Convert.ToString(row["AssociationProviderID"]));
                                c1Reserve.SetData(_rowIndex, COL_PROVIDERNAME, Convert.ToString(row["AssociationProvider"]));
                                c1Reserve.SetData(_rowIndex, COL_BUSINESSCENTER_CODE, Convert.ToString(row["BusinessCenterCode"]));
                                c1Reserve.SetData(_rowIndex, COL_BUSINESSCENTER_ID, Convert.ToString(row["BusinessCenterID"]));
                            #endregion

                                #region " Set Styles "

                                c1Reserve.SetCellStyle(_rowIndex, COL_USENOW, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                                #endregion " Set Styles "
                   
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                this.c1Reserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
           
            } 
            
                    
        }

        private void getPatientClaimNos(Int64 nPatientID)        
        {
           
            DataTable _dtClaimNo = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            try
            {
                string _strSql = "";
                _strSql = "SELECT CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionMasterID )+ '-' + CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionID) AS ID, "
                         + " dbo.GetSubClaimNumber(BL_Transaction_Claim_MST.nClaimNo,BL_Transaction_Claim_MST.nSubClaimNo ,BL_Transaction_Claim_MST.sMainClaimNo,5) as Claim  "
                         + " FROM BL_Transaction_Claim_MST WITH (NOLOCK) WHERE (LEFT(nSubClaimNo,1)<> '-')  AND nPatientID = " + nPatientID
                         + "  ORDER BY dtCreateDate DESC "; 

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
                    
                }
                else
                {
                    cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                      
                try
                {
                    if (nInsuranceCompanyID != 0)
                    {

                        string _strSql = "";
                        _strSql = "SELECT ISNULL(sDescription,'') AS sInsName FROM Contacts_InsuranceCompany_MST WITH (NOLOCK)  " +
                                  " WHERE nID = " + nInsuranceCompanyID + "";

                        oDB.Connect(false);
                        oDB.Retrive_Query(_strSql, out dtInsCompany);
                        oDB.Disconnect();                       
                    }
                    return dtInsCompany;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                    return null;
                }
        
        }   

        private bool getValidClaimDetails()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling( gloPMGlobal.DatabaseConnectionString, "");  
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);

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
                    this.MSTTransactionID = 0;
                    this.TransactionID = 0;
                    this.PatientID = 0;
                    this.PatientName = ""; ;
                    ClaimNo = "";


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
                            if (oDB != null) { oDB.Dispose(); }

                            this.MSTTransactionID = Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionMasterID"]);
                            this.TransactionID = Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionID"]);
                            ClaimNo = cmbClaimNo.Text;
                            cmbClaimNo.Tag = Convert.ToString(MSTTransactionID) + '-' + Convert.ToString(TransactionID);

                            txtPatient.Text = "";

                            this.PatientID = Convert.ToInt64(dtTransactionID.Rows[0]["nPatientID"]);
                            this.PatientName = Convert.ToString(dtTransactionID.Rows[0]["Patient"]);
                            txtPatient.Text = PatientName;
                            txtPatient.Tag = Convert.ToInt64(PatientID);
                            
                        }
                    }
                    else
                    {
                        this.PatientID = Convert.ToInt64(txtPatient.Tag);
                        this.PatientName = txtPatient.Text;

                    }
                }
                return true;
            }
            catch (Exception ex )
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
                return false;

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }

        }

        private DataTable getReserveID(Int64 nCreditID)
        { 
        
         gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling( gloPMGlobal.DatabaseConnectionString, "");  
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);

            try
            {
                DataTable dtReserveID = new DataTable();

                string _strSql = "";
                _strSql = "select nReserveID from Reserves where nCredits_RefID = " + nCreditID + "";
                
                oDB.Connect(false);
                oDB.Retrive_Query(_strSql, out dtReserveID);
                oDB.Disconnect();
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                return dtReserveID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        private void OpenReserveForModify(int RowIndex, C1FlexGrid c1Payment)
        {
           
                
                Int64 _nEOBPaymentID = 0;
                Int64 _nEOBPaymentDetailID = 0;                
                gloAccountsV2.PaymentCollection.Reserves _ReserveDetails = new gloAccountsV2.PaymentCollection.Reserves();
                gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(AppSettings.ConnectionStringPM, string.Empty);

                string _ReserveNote = string.Empty;
                string _InsuranceCompany = string.Empty;
                _IsReserveOpenForModify = true;
                try
                {
                    if (c1Payment.GetData(RowIndex, COL_CREDITID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_CREDITID)).ToString().Trim() != "")
                    { _nEOBPaymentID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_CREDITID)); }

                    if (c1Payment.GetData(RowIndex, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
                    { _nEOBPaymentDetailID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_EOBPAYMENTDTLID)); }

                    if (c1Payment.GetData(RowIndex, COL_INSURANCE_COMPANY_ID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_INSURANCE_COMPANY_ID)).Trim() != "")
                    { _ReserveDetails.InsCompanyID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_INSURANCE_COMPANY_ID)); }

                    if (c1Payment.GetData(RowIndex, COL_NOTE) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_NOTE)).Trim() != "")
                    { _ReserveDetails.ReserveNote = Convert.ToString(c1Payment.GetData(RowIndex, COL_NOTE)); }

                    if (c1Payment.GetData(RowIndex, COL_INSURANCE_COMPANY_NAME) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_INSURANCE_COMPANY_NAME)).Trim() != "")
                    { _ReserveDetails.InsCompanyName = Convert.ToString(c1Payment.GetData(RowIndex, COL_INSURANCE_COMPANY_NAME)); }

                    if (c1Payment.GetData(RowIndex, COL_TORESERVES) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_TORESERVES)).Trim() != "")
                    { _ReserveDetails.ReserveAmount = Convert.ToDecimal(c1Payment.GetData(RowIndex, COL_TORESERVES)); }

                    if (c1Payment.GetData(RowIndex, COL_ASSO_MSTTRANSACTIONID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_ASSO_MSTTRANSACTIONID)).Trim() != "")
                    { _ReserveDetails.MSTTransactionID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_ASSO_MSTTRANSACTIONID)); }

                    if (c1Payment.GetData(RowIndex, COL_ASSO_TRACKTRANSACTIONID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_ASSO_TRACKTRANSACTIONID)).Trim() != "")
                    { _ReserveDetails.TransactionID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_ASSO_TRACKTRANSACTIONID)); }

                    if (c1Payment.GetData(RowIndex, COL_ASSO_PATIENTID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_ASSO_PATIENTID)).Trim() != "")
                    { _ReserveDetails.PatientID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_ASSO_PATIENTID)); }

                    if (c1Payment.GetData(RowIndex, COL_ASSO_CLAIMNO) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_ASSO_CLAIMNO)).Trim() != "")
                    { _ReserveDetails.ClaimNo = Convert.ToString(c1Payment.GetData(RowIndex, COL_ASSO_CLAIMNO)); }

                    if (c1Payment.GetData(RowIndex, COL_ASSO_PATIENTNAME) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_ASSO_PATIENTNAME)).Trim() != "")
                    { _ReserveDetails.PatientName = Convert.ToString(c1Payment.GetData(RowIndex, COL_ASSO_PATIENTNAME)); }
                    
                    if (c1Payment.GetData(RowIndex, COL_PROVIDERID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_PROVIDERID)).Trim() != "")
                    { _ReserveDetails.ProviderID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_PROVIDERID)); }

                    if (c1Payment.GetData(RowIndex, COL_PROVIDERNAME) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_PROVIDERNAME)).Trim() != "")
                    { _ReserveDetails.ProviderName = Convert.ToString(c1Payment.GetData(RowIndex, COL_PROVIDERNAME)); }

                    if (c1Payment.GetData(RowIndex, COL_BUSINESSCENTER_ID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_BUSINESSCENTER_ID)).Trim() != "")
                    { _ReserveDetails.BusinessCenterID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_BUSINESSCENTER_ID)); }

                    //if (c1Payment.GetData(RowIndex, COL_BUSINESSCENTER_CODE) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_BUSINESSCENTER_CODE)).Trim() != "")
                    //{ _ReserveDetails.BusinessCenterID = Convert.ToString(c1Payment.GetData(RowIndex, COL_BUSINESSCENTER_CODE)); }

                    if (c1Payment.GetData(RowIndex, COL_EntryType) != null && (Convert.ToInt16(c1Payment.GetData(RowIndex, COL_EntryType))) == 12)
                    {
                        if (c1Payment.GetData(RowIndex, COL_CloseDate) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_CloseDate)).Trim() != "")
                        { _ReserveDetails.CloseDateOffset = Convert.ToDateTime(c1Payment.GetData(RowIndex, COL_CloseDate)); }

                        if (c1Payment.GetData(RowIndex, COL_PaymentTryID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_PaymentTryID)).Trim() != "")
                        { _ReserveDetails.PaymentTryID  = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_PaymentTryID)); }

                        if (c1Payment.GetData(RowIndex, COL_PaymentTryDesc) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_PaymentTryDesc)).Trim() != "")
                        { _ReserveDetails.PaymentTryDesc  = Convert.ToString(c1Payment.GetData(RowIndex, COL_PaymentTryDesc)); }
                        
                        frmInsuranceOffset ofrmInsuranceOffset = new frmInsuranceOffset(gloPMGlobal.DatabaseConnectionString, _nEOBPaymentID, _nEOBPaymentDetailID);
                        ofrmInsuranceOffset.IsProviderMandatory = IsProviderEnable;
                        ofrmInsuranceOffset.IsDayClosed = ogloBilling.IsDayClosed(gloAccountsV2.gloBillingCommonV2.GetPaymentCloseDate(_nEOBPaymentID));
                        ofrmInsuranceOffset._isOpenForView = true;
                        if (_ReserveDetails != null)
                        {
                            ofrmInsuranceOffset.ReserveDetails = _ReserveDetails;
                        }

                        ofrmInsuranceOffset.ShowInTaskbar = false;
                        ofrmInsuranceOffset.ShowDialog(this);

                        if (ofrmInsuranceOffset.DialogResult == DialogResult.OK)
                        {
                            if (c1Payment.Name == "c1SelectReserve")
                            {
                                FillSelectReserves();
                            }
                            else if (c1Payment.Name == "c1Reserve")
                            {
                                FillReserves();
                            }

                        }
                        if (ofrmInsuranceOffset != null)
                        {

                            ofrmInsuranceOffset.Dispose();
                        }
                    }
                    else
                    {
                        frmInsuranceReserveRemainingV2 ofrmInsuranceReserveRemaining = new frmInsuranceReserveRemainingV2(gloPMGlobal.DatabaseConnectionString, _nEOBPaymentID, _nEOBPaymentDetailID);
                        ofrmInsuranceReserveRemaining.IsProviderMandatory = IsProviderEnable;
                        ofrmInsuranceReserveRemaining.IsDayClosed = ogloBilling.IsDayClosed(gloAccountsV2.gloBillingCommonV2.GetPaymentCloseDate(_nEOBPaymentID));
                        ofrmInsuranceReserveRemaining._isOpenForView = true;
                        if (_ReserveDetails != null)
                        {
                            ofrmInsuranceReserveRemaining.ReserveDetails = _ReserveDetails;
                        }

                        ofrmInsuranceReserveRemaining.ShowInTaskbar = false;
                        ofrmInsuranceReserveRemaining.ShowDialog(this);

                        if (ofrmInsuranceReserveRemaining.DialogResult == DialogResult.OK)
                        {
                            if (c1Payment.Name == "c1SelectReserve")
                            {
                                FillSelectReserves();
                            }
                            else if (c1Payment.Name == "c1Reserve")
                            {
                                FillReserves();
                            }

                        }
                        if (ofrmInsuranceReserveRemaining != null)
                        {

                            ofrmInsuranceReserveRemaining.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                    _IsReserveOpenForModify = false;
                }
                finally
                { 
                    if (_ReserveDetails !=null)
                    {
                        _ReserveDetails.Dispose();
                    }
                    _IsReserveOpenForModify = false;
                    if (ogloBilling != null)
                    {
                        ogloBilling.Dispose();
                        ogloBilling = null;
                    }
                }
        }

        private void c1SelectReserve_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1Reserve_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1SelectReserve_MouseLeave(object sender, EventArgs e)
        {
            C1SuperTooltip1.Hide();
        }

        private void c1Reserve_MouseLeave(object sender, EventArgs e)
        {
            C1SuperTooltip1.Hide();
        }

        private void chkPatient_CheckedChanged(object sender, EventArgs e)
        {
            if (LoadedClaimPatientName != null && LoadedClaimPatientName != string.Empty)
            {
                if (chkPatient.Checked)
                {

                    txtPatient.Text = LoadedClaimPatientName;
                    txtPatient.Tag = LoadedClaimPatientID;
                    PatientName = LoadedClaimPatientName;
                    PatientID = LoadedClaimPatientID;
                }
                else
                {
                    txtPatient.Text = "";
                    txtPatient.Tag = 0;
                    PatientID = 0;
                    PatientName = "";
                }
            }
        }

        private void chkCurrentCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCurrentCheck.Checked)
            {

                txtInsCompany.Text = "";
                txtInsCompany.Tag = 0;
                txtPatient.Text = "";
                txtPatient.Tag = 0;
                cmbClaimNo.Text = "";
                cmbClaimNo.Tag = 0;
                cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
               
                cmbClaimNo.DataSource = null;
                cmbClaimNo.Items.Clear();
                txtNoteText.Text = "";
                chkPatient.CheckState = CheckState.Unchecked;
                
                if (LoadedCheckEOBPaymentID != 0)
                {
                    FillSelectReserves();
                }
            }
            else
            {
             //   c1SelectReserve.Clear();
                c1SelectReserve.DataSource = null;
                c1SelectReserve.Rows.Count = 1;
            }

        }

        private void cmbClaimNo_Leave(object sender, EventArgs e)
        {
            getValidClaimDetails();
        }
        #endregion

        private void cmbBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBusinessCenter.Items.Count > 0)
                { BusinessCenterID = Convert.ToInt64(cmbBusinessCenter.SelectedValue);}
                else
                { BusinessCenterID = 0; }
                combo = cmbBusinessCenter;
                if (Convert.ToString(cmbBusinessCenter.SelectedValue) != "" && Convert.ToString(cmbBusinessCenter.SelectedValue) != "0")
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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
                        this.toolTip1.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillBusinessCenter()
        {
            DataTable _dtBusinessCenter = new DataTable();
            _dtBusinessCenter = gloGlobal.gloPMMasters.GetBusinessCenter();

            if (_dtBusinessCenter != null && _dtBusinessCenter.Rows.Count > 0)
            {
                DataRow dr = _dtBusinessCenter.NewRow();
                dr["BusinessCenter"] = "";
                dr["nBusinessCenterID"] = 0;

                _dtBusinessCenter.Rows.InsertAt(dr, 0);

                cmbBusinessCenter.ValueMember = "nBusinessCenterID";
                cmbBusinessCenter.DisplayMember = "BusinessCenter";
                cmbBusinessCenter.DataSource = _dtBusinessCenter.Copy();

                Int64 _DefaultBusinessCenter = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
                if (_DefaultBusinessCenter > 0)
                { cmbBusinessCenter.SelectedValue = _DefaultBusinessCenter; }
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
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
        private void tsb_PaymetOffset_Click(object sender, EventArgs e)
        {
            frmInsuranceOffset ofrmInsuranceReserveRemaining = new frmInsuranceOffset(AppSettings.ConnectionStringPM);
            try
            {
                DataTable dtBusinessCenter = null;
                // If Claim opened in Correction mode then default the claims account business center
                dtBusinessCenter = gloCharges.GetPatientAccountBusinessCenter(ClaimDetails.PAccountID);
                ReserveDetails.ClaimsAccountID = ClaimDetails.PAccountID;
                if (dtBusinessCenter != null && dtBusinessCenter.Rows.Count > 0)
                {
                    ReserveDetails.BusinessCenterID = Convert.ToInt64(dtBusinessCenter.Rows[0]["nBusinessCenterID"]);
                }
                ofrmInsuranceReserveRemaining.ReserveDetails = this.ReserveDetails;
                ofrmInsuranceReserveRemaining.ShowInTaskbar = false;
                ofrmInsuranceReserveRemaining.StartPosition = FormStartPosition.CenterScreen;
                ofrmInsuranceReserveRemaining.ShowDialog(this);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ofrmInsuranceReserveRemaining != null) { ofrmInsuranceReserveRemaining.Dispose(); }
            }
        }

        private void chkPaymentOffset_CheckedChanged(object sender, EventArgs e)
        {
            FillSelectReserves(); 
        }
       
    }
}