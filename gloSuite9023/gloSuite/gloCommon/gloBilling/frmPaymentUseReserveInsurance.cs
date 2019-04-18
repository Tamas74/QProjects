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

namespace gloBilling
{
    public partial class frmPaymentUseReserveInsurance: Form
    {

        #region " Private Variables "

        private Int64 _InsuranceCompanyID = 0;
        private decimal _AmountTakenFromReserve = 0;
        private Int64 _ClinicID = 0;
        Int64 _UserId = 0;
        string _UserName = "";
        private DateTime _closeDate = DateTime.Now;
        private string _closeDayTray = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = "";
        
        // Unused variables need to delete        
        private bool _IsFormLoading = false;
      //  bool _IsGenerateReserve = false;
        public decimal SelectedUseReserveAmount = 0;
    //    public gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
        public EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsurancePaymentMasterLines = null;
        


        Int64 _EOBPaymentDetailID = 0;
        Int64 _EOBPaymentID = 0;

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        public bool _IsUseNowValidate =false;
        public bool _IsReserveOpenForModify = false;
        #endregion " Private Variables "

        #region  " Grid Constants "

        //nEOBPaymentID, nEOBID, nEOBDtlID, nEOBPaymentDetailID, 
        //nBillingTransactionID, nBillingTransactionDetailID, nBillingTransactionLineNo, 
        //nPatientID, nDOSFrom, nDOSTo, nAmount, nPayMode, 
        //nRefEOBPaymentID, nRefEOBPaymentDetailID, 
        //nAccountID, nAccountType, nMSTAccountID, nMSTAccountType 

        const int COL_EOBPAYMENTID = 0;
        const int COL_EOBID = 1;
        const int COL_EOBDTLID = 2;
        const int COL_EOBPAYMENTDTLID = 3;
        const int COL_BLTRANSACTIONID = 4;
        const int COL_BLTRANDTLID = 5;
        const int COL_BLTRANLINEID = 6;
        const int COL_DOSFROM = 7;
        const int COL_DOSTO = 8;

        const int COL_INSURANCE_COMPANY_ID = 9;
        const int COL_SOURCE = 10; // Insurance Name will come

        const int COL_ORIGINALPAYMENT = 11;//Check Number,Date,Amount

        const int COL_ASSO_PATIENTID = 12;
        const int COL_ASSO_PATIENTNAME = 13;
        const int COL_ASSO_CLAIMNO = 14;
        const int COL_ASSO_TRACKTRANSACTIONID = 15;
        const int COL_ASSO_MSTTRANSACTIONID = 16;

        const int COL_TORESERVES = 17;//Amount for reserve
        const int COL_TYPE = 18;//Copay,Advance,Other

        const int COL_CREATED_DATE_USER = 19;

        const int COL_NOTE =20;//Note

        const int COL_DB_AMOUNT = 21;
        const int COL_OBJ_AMOUNT = 22;
        const int COL_AVAILABLE = 23;//Available amount
        const int COL_USERESERVE = 24;//Used Reserve
        const int COL_USENOW = 25;//Current amount to use from avaiable amount

        const int COL_PAYMODE = 26;
        const int COL_REFEOBPAYID = 27;
        const int COL_REFEOBPAYDTLID = 28;
        const int COL_ACCOUNTID = 29;
        const int COL_ACCOUNTTYPE =30;
        const int COL_MSTACCOUNTID = 31;
        const int COL_MSTACCOUNTTYPE = 32;
        const int COL_RES_EOBPAYID = 33;
        const int COL_RES_EOBPAYDTLID = 34;
        const int COL_ASSO_SELECT = 35;           

        const int COL_COUNT = 36;

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
        public Int64 ClinicID
        { 
            get { return _ClinicID; } 
            set { _ClinicID = value; } 
        }
        public Int64  UserID
        { 
            get { return _UserId; } 
            set { _UserId = value; } 
        }
        public string UserName
        { 
            get { return _UserName; } 
            set { _UserName = value; } 
        }
        public DateTime CloseDate
        {
            get { return _closeDate; }
            set { _closeDate = value; }
        }
        public string CloseDayTray
        {
            get { return _closeDayTray; }
            set { _closeDayTray = value; }
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

        gloGeneralItem.gloItems _oSeletedReserveItems = new gloGeneralItem.gloItems();

        public gloGeneralItem.gloItems SeletedReserveItems
        {
            get { return _oSeletedReserveItems; }
            set { _oSeletedReserveItems = value; }
        }

        Int64 _MainInsuranceCompanyID = 0;
        public Int64 MainInsuranceCompanyID
        {
            get { return _MainInsuranceCompanyID; }
            set { _MainInsuranceCompanyID = value; }
        }

        #endregion " Property Procedures "

        #region " Constructors "

        public frmPaymentUseReserveInsurance(string DatabaseConnectionString,Int64 InsuranceCompanyID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _InsuranceCompanyID =InsuranceCompanyID;
            MainInsuranceCompanyID = InsuranceCompanyID;

            #region " Retrive Clinic ID "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion " Retrive Clinic ID "

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserId = 0;
            }

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

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        #endregion " Constructors "

        #region " Form Events "

        private void frmPaymentUseReserve_Load(object sender, EventArgs e)
        {
            try
            {
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);

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
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    
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
                           // c1SelectReserve.Clear();
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
            if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
            {
                EOBInsurancePaymentMasterLines.Dispose();
            }

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
            //gloGeneralItem.gloItems _oSeletedReserveItems = new gloGeneralItem.gloItems();
            //gloGeneralItem.gloItem ogloItem = null;
            //decimal _selectedAmount = 0;
            //Int64 _selEOBPayId = 0;
            //Int64 _selEOBPayDtlId = 0;
            //Int64 _selRefEOBPayId = 0;
            //Int64 _selRefEOBPayDtlId = 0;
            //Int32 _selEOBPayPayMode = 0;

            //try
            //{
            //    if (c1Reserve != null && c1Reserve.Rows.Count > 1)
            //    {
            //        c1Reserve.FinishEditing();

            //        for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
            //        {
            //            if (c1Reserve.GetData(i, COL_USENOW) != null && Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).ToString().Trim() != "")
            //            {
            //                if (Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW)) > 0)
            //                {
            //                    _selectedAmount += Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW));

            //                    if (c1Reserve.GetData(i, COL_EOBPAYMENTID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTID)).ToString().Trim() != "")
            //                    { _selEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTID)); }
            //                    if (c1Reserve.GetData(i, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
            //                    { _selEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)); }

            //                    if (c1Reserve.GetData(i, COL_REFEOBPAYID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYID)).ToString().Trim() != "")
            //                    { _selRefEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYID)); }

            //                    if (c1Reserve.GetData(i, COL_REFEOBPAYDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)).ToString().Trim() != "")
            //                    { _selRefEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)); }

            //                    if (c1Reserve.GetData(i, COL_PAYMODE) != null && Convert.ToString(c1Reserve.GetData(i, COL_PAYMODE)).ToString().Trim() != "")
            //                    {
            //                        _selEOBPayPayMode = ((EOBPaymentMode)Convert.ToInt32(c1Reserve.GetData(i, COL_PAYMODE))).GetHashCode();
            //                    }

            //                    ogloItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).Trim());
            //                    ogloItem.SubItems.Add(_selRefEOBPayId, _selEOBPayPayMode.ToString(), _selRefEOBPayDtlId.ToString());
            //                    _oSeletedReserveItems.Add(ogloItem);
            //                    ogloItem.Dispose();

            //                    _selEOBPayId = 0;
            //                    _selEOBPayDtlId = 0;
            //                    _selEOBPayId = 0;
            //                    _selEOBPayDtlId = 0;
            //                    _selEOBPayPayMode = 0;
            //                }
            //            }
            //        }

            //        if (_selectedAmount > 0)
            //        {

            //            // commented temp
            //            //frmBillingEOBPatientRefund ofrmBillingEOBPatientRefund = new frmBillingEOBPatientRefund(_databaseconnectionstring, _patientId);
            //            //ofrmBillingEOBPatientRefund.oSeletedReserveItems = _oSeletedReserveItems;
            //            //ofrmBillingEOBPatientRefund.RefundAmt = _selectedAmount;
            //            //ofrmBillingEOBPatientRefund.CloseDayTray = _closeDayTray;
            //            //ofrmBillingEOBPatientRefund.CloseDate = this.CloseDate;
            //            //ofrmBillingEOBPatientRefund.ShowDialog();
            //            FillSelectReserves();
            //            if (c1Reserve.Rows.Count > 1) { c1Reserve.Select(1, COL_USENOW, true); }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please select the amount to refund.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            if (c1Reserve != null && c1Reserve.Rows.Count > 1) { c1Reserve.Select(1, COL_USENOW, true); }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
        }

        #endregion

        #region " Design Grid "

        private void DesignPaymentGrid(C1FlexGrid c1Payment)
        {
            try
            {
               
                _IsFormLoading = true;
                c1Payment.Redraw = false;
                c1Payment.AllowSorting = AllowSortingEnum.None;

                c1Payment.Clear();
                c1Payment.Cols.Count = COL_COUNT;
                c1Payment.Rows.Count = 1;
                c1Payment.Rows.Fixed = 1;
                c1Payment.Cols.Fixed = 0;

                #region " Set Headers "

                c1Payment.SetData(0, COL_EOBPAYMENTID,"EOBPaymentID");
                c1Payment.SetData(0, COL_EOBID,"EOBID");
                c1Payment.SetData(0, COL_EOBDTLID,"EOBDetailID");
                c1Payment.SetData(0, COL_EOBPAYMENTDTLID, "EOBPaymentDetailID");
                c1Payment.SetData(0, COL_BLTRANSACTIONID,"BillingTransactioID");
                c1Payment.SetData(0, COL_BLTRANDTLID, "BillingTransactioDetailID");
                c1Payment.SetData(0, COL_BLTRANLINEID, "BillingTransactioLineID");
                c1Payment.SetData(0, COL_DOSFROM, "DOSFrom");
                c1Payment.SetData(0, COL_DOSTO,"DOSTo");
                c1Payment.SetData(0, COL_INSURANCE_COMPANY_ID,"InsuranceCompanyID"); // AccountID will come
                c1Payment.SetData(0, COL_SOURCE,"Insurance Company");// Insurance Name

                c1Payment.SetData(0, COL_ORIGINALPAYMENT,"Original Payment");//Check Number,Date,Amount
                c1Payment.SetData(0, COL_TORESERVES,"To Reserves");//Amount for reserve
                c1Payment.SetData(0, COL_TYPE,"Type");//Copay,Advance,Other
                c1Payment.SetData(0, COL_NOTE,"Note");//Note

                c1Payment.SetData(0, COL_DB_AMOUNT,"DBAmount");
                c1Payment.SetData(0, COL_OBJ_AMOUNT, "OBJAmount");
                c1Payment.SetData(0, COL_AVAILABLE,"Available");//Available amount
                c1Payment.SetData(0, COL_USERESERVE, "Used");//Used Reserve
                c1Payment.SetData(0, COL_USENOW,"Use Now");//Current amount to use from avaiable amount

                c1Payment.SetData(0, COL_PAYMODE,"Payment Mode");
                c1Payment.SetData(0, COL_REFEOBPAYID,"Ref.EOBID");
                c1Payment.SetData(0, COL_REFEOBPAYDTLID,"Ref.EOBDetailID");
                c1Payment.SetData(0, COL_ACCOUNTID,"AccountID");
                c1Payment.SetData(0, COL_ACCOUNTTYPE,"Account Type");
                c1Payment.SetData(0, COL_MSTACCOUNTID,"Mst.AccountID");
                c1Payment.SetData(0, COL_MSTACCOUNTTYPE, "Mst.AccountType");
                c1Payment.SetData(0, COL_RES_EOBPAYID,"ReserveRefPayID");
                c1Payment.SetData(0, COL_RES_EOBPAYDTLID,"ReserveRefPayDtlID");

                c1Payment.SetData(0, COL_ASSO_PATIENTID, "PatientID");
                c1Payment.SetData(0, COL_ASSO_PATIENTNAME, "Patient");
                c1Payment.SetData(0, COL_ASSO_MSTTRANSACTIONID, "nTransactionID");
                c1Payment.SetData(0, COL_ASSO_TRACKTRANSACTIONID, "nTrackTrnID");
                c1Payment.SetData(0, COL_ASSO_CLAIMNO, "Claim #");
                c1Payment.SetData(0, COL_ASSO_SELECT, "Select");
                c1Payment.SetData(0, COL_CREATED_DATE_USER, "Created");


                #endregion

                #region " Show/Hide "

                c1Payment.Cols[COL_SOURCE].Visible = true;
                c1Payment.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1Payment.Cols[COL_TORESERVES].Visible = true;
                c1Payment.Cols[COL_TYPE].Visible = false;
                c1Payment.Cols[COL_NOTE].Visible = true;
                c1Payment.Cols[COL_AVAILABLE].Visible = true;
                c1Payment.Cols[COL_USENOW].Visible = true;

                c1Payment.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANSACTIONID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANLINEID].Visible = false;// 0;
                c1Payment.Cols[COL_DOSFROM].Visible = false;// 50;
                c1Payment.Cols[COL_DOSTO].Visible = false;// 0;
                c1Payment.Cols[COL_INSURANCE_COMPANY_ID].Visible = false;// 0;
                c1Payment.Cols[COL_SOURCE].Visible = true;// 100;
                c1Payment.Cols[COL_ORIGINALPAYMENT].Visible = true;// 100;
                c1Payment.Cols[COL_TORESERVES].Visible = true;// 100;
                c1Payment.Cols[COL_TYPE].Visible = false;// 100;
                c1Payment.Cols[COL_NOTE].Visible = true;// 100;

                c1Payment.Cols[COL_DB_AMOUNT].Visible = false;
                c1Payment.Cols[COL_OBJ_AMOUNT].Visible = false;
                c1Payment.Cols[COL_AVAILABLE].Visible = true;// 100;
                c1Payment.Cols[COL_USENOW].Visible = true;// 100;

                c1Payment.Cols[COL_PAYMODE].Visible = false;// 100;
                c1Payment.Cols[COL_REFEOBPAYID].Visible = false;// 0;
                c1Payment.Cols[COL_REFEOBPAYDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_ACCOUNTID].Visible = false;// 0;
                c1Payment.Cols[COL_ACCOUNTTYPE].Visible = false;// 0;
                c1Payment.Cols[COL_MSTACCOUNTID].Visible = false;// 0;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].Visible = false;// 0;
                c1Payment.Cols[COL_USERESERVE].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYID].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].Visible = false;


                c1Payment.Cols[COL_ASSO_PATIENTID].Visible = false;
                c1Payment.Cols[COL_ASSO_PATIENTNAME].Visible = true;
                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].Visible = false;
                c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].Visible = false;
                c1Payment.Cols[COL_ASSO_CLAIMNO].Visible = true;
                

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


                #endregion

                #region " Width "
                bool _designWidth = false;
                //try
                //{
                //    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
                //    if (c1Payment.Name == c1SinglePayment.Name)
                //    {
                //        _designWidth = oSetting.LoadGridColumnWidth(c1Payment, gloSettings.ModuleOfGridColumn.PaymentSinglePaymentGrid, _userId);
                //    }
                //    else if (c1Payment.Name == c1MultiplePayment.Name)
                //    {
                //        _designWidth = oSetting.LoadGridColumnWidth(c1Payment, gloSettings.ModuleOfGridColumn.PaymentMultiplePaymentGrid, _userId);
                //    }
                //    oSetting.Dispose();
                //}
                //catch (Exception ex)
                //{

                //}

                if (_designWidth == false)
                {

                    c1Payment.Cols[COL_EOBPAYMENTID].Width = 0;
                    c1Payment.Cols[COL_EOBID].Width = 0;
                    c1Payment.Cols[COL_EOBDTLID].Width = 0;
                    c1Payment.Cols[COL_EOBPAYMENTDTLID].Width = 0;
                    c1Payment.Cols[COL_BLTRANSACTIONID].Width = 0;
                    c1Payment.Cols[COL_BLTRANDTLID].Width = 0;
                    c1Payment.Cols[COL_BLTRANLINEID].Width = 0;
                    c1Payment.Cols[COL_DOSFROM].Width = 50;
                    c1Payment.Cols[COL_DOSTO].Width = 0;
                    c1Payment.Cols[COL_INSURANCE_COMPANY_ID].Width = 0;
                    c1Payment.Cols[COL_SOURCE].Width = 150;
                    c1Payment.Cols[COL_ORIGINALPAYMENT].Width = 250;
                    c1Payment.Cols[COL_TORESERVES].Width = 80;
                    c1Payment.Cols[COL_TYPE].Width = 0;
                    if (c1Payment.Name == "c1SelectReserve")
                    {
                        c1Payment.Cols[COL_NOTE].Width = 290;
                    }
                    else
                    {
                        c1Payment.Cols[COL_NOTE].Width = 202;
                    }

                    c1Payment.Cols[COL_DB_AMOUNT].Width = 75;
                    c1Payment.Cols[COL_OBJ_AMOUNT].Width = 75;
                    c1Payment.Cols[COL_AVAILABLE].Width = 75;
                    c1Payment.Cols[COL_USENOW].Width = 75;

                    c1Payment.Cols[COL_PAYMODE].Width = 100;
                    c1Payment.Cols[COL_REFEOBPAYID].Width = 0;
                    c1Payment.Cols[COL_REFEOBPAYDTLID].Width = 0;
                    c1Payment.Cols[COL_ACCOUNTID].Width = 0;
                    c1Payment.Cols[COL_ACCOUNTTYPE].Width = 0;
                    c1Payment.Cols[COL_MSTACCOUNTID].Width = 0;
                    c1Payment.Cols[COL_MSTACCOUNTTYPE].Width = 0;
                    c1Payment.Cols[COL_USERESERVE].Width = 0;
                    c1Payment.Cols[COL_RES_EOBPAYID].Width = 0;
                    c1Payment.Cols[COL_RES_EOBPAYDTLID].Width = 0;

                    c1Payment.Cols[COL_ASSO_PATIENTID].Width = 0;
                    c1Payment.Cols[COL_ASSO_PATIENTNAME].Width =150;
                    c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].Width =0;
                    c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].Width = 0;
                    c1Payment.Cols[COL_ASSO_CLAIMNO].Width = 75;
                    c1Payment.Cols[COL_ASSO_SELECT].Width = 50;
                    c1Payment.Cols[COL_CREATED_DATE_USER].Width = 108;
                    
                }

                #endregion

                #region " Data Type "

                c1Payment.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANLINEID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_DOSFROM].DataType = typeof(System.String);
                c1Payment.Cols[COL_DOSTO].DataType = typeof(System.String);
                c1Payment.Cols[COL_INSURANCE_COMPANY_ID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_SOURCE].DataType = typeof(System.String);
                c1Payment.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
                c1Payment.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_TYPE].DataType = typeof(System.String);
                c1Payment.Cols[COL_NOTE].DataType = typeof(System.String);

                c1Payment.Cols[COL_DB_AMOUNT].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_OBJ_AMOUNT].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_USENOW].DataType = typeof(System.Decimal);

                c1Payment.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_REFEOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_REFEOBPAYDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ACCOUNTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_MSTACCOUNTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_MSTACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_RES_EOBPAYDTLID].DataType = typeof(System.Int64);

                c1Payment.Cols[COL_ASSO_PATIENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ASSO_PATIENTNAME].DataType = typeof(System.String);
                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ASSO_CLAIMNO].DataType = typeof(System.String);
                c1Payment.Cols[COL_ASSO_SELECT].DataType = typeof(System.Boolean);
                c1Payment.Cols[COL_CREATED_DATE_USER].DataType = typeof(System.String);

                #endregion

                #region " Alignment "

                c1Payment.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_BLTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BLTRANDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BLTRANLINEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_DOSFROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_DOSTO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_INSURANCE_COMPANY_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1Payment.Cols[COL_DB_AMOUNT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_OBJ_AMOUNT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_USENOW].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1Payment.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAYMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_REFEOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_REFEOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_MSTACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;


                c1Payment.Cols[COL_ASSO_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ASSO_SELECT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Payment.Cols[COL_CREATED_DATE_USER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1Payment.Styles.Add("cs_CurrencyStyle");
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
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;  //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableStringStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                    csEditableStringStyle.DataType = typeof(System.String);
                    csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableDateStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                    csEditableDateStyle.DataType = typeof(System.DateTime);
                    csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableDateStyle.BackColor = Color.White;

                }
  
  

                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;// = c1Payment.Styles.Add("cs_ClaimRowStyle");
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
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);

                    }

                }
                catch
                {
                    csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                    csPatientRowStyle.DataType = typeof(System.String);
                    csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);

                }
  
  
                c1Payment.Cols[COL_TORESERVES].Style = csCurrencyStyle;

                c1Payment.Cols[COL_DB_AMOUNT].Style = csCurrencyStyle;
                c1Payment.Cols[COL_OBJ_AMOUNT].Style = csCurrencyStyle;
                c1Payment.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_USENOW].Style = csCurrencyStyle;

                c1Payment.Cols[COL_USERESERVE].Style = csCurrencyStyle;
                
                #endregion

                c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Payment.AllowEditing = true;

                c1Payment.Cols[COL_EOBPAYMENTID].AllowEditing = false;
                c1Payment.Cols[COL_EOBID].AllowEditing = false;//0;
                c1Payment.Cols[COL_EOBDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANSACTIONID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANLINEID].AllowEditing = false;//0;
                c1Payment.Cols[COL_DOSFROM].AllowEditing = false;//50;
                c1Payment.Cols[COL_DOSTO].AllowEditing = false;//0;
                c1Payment.Cols[COL_INSURANCE_COMPANY_ID].AllowEditing = false;//0;
                c1Payment.Cols[COL_SOURCE].AllowEditing = false;//100;
                c1Payment.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
                c1Payment.Cols[COL_TORESERVES].AllowEditing = false;//100;
                c1Payment.Cols[COL_TYPE].AllowEditing = false;//100;
                c1Payment.Cols[COL_NOTE].AllowEditing = false;//100;

                c1Payment.Cols[COL_DB_AMOUNT].AllowEditing = false;
                c1Payment.Cols[COL_OBJ_AMOUNT].AllowEditing = false;
                c1Payment.Cols[COL_AVAILABLE].AllowEditing = false;//100;
                c1Payment.Cols[COL_USENOW].AllowEditing = true;//100;

                c1Payment.Cols[COL_USERESERVE].AllowEditing = false;//100;
                c1Payment.Cols[COL_PAYMODE].AllowEditing = false;//100;
                c1Payment.Cols[COL_REFEOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_REFEOBPAYDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ACCOUNTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ACCOUNTTYPE].AllowEditing = false;//0;
                c1Payment.Cols[COL_MSTACCOUNTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].AllowEditing = false;//0;
                c1Payment.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].AllowEditing = false;//0;


                c1Payment.Cols[COL_ASSO_PATIENTID].AllowEditing =false;
                c1Payment.Cols[COL_ASSO_PATIENTNAME].AllowEditing = false;
                c1Payment.Cols[COL_ASSO_MSTTRANSACTIONID].AllowEditing = false;
                c1Payment.Cols[COL_ASSO_TRACKTRANSACTIONID].AllowEditing = false;
                c1Payment.Cols[COL_ASSO_CLAIMNO].AllowEditing = false;
                c1Payment.Cols[COL_ASSO_SELECT].AllowEditing = true;
                c1Payment.Cols[COL_CREATED_DATE_USER].AllowEditing = false;
                #endregion

                c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

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
                if (c1Reserve.ColSel == COL_SOURCE)
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

                                    MessageBox.Show("Use amount cannot be negative. Please select a valid amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                                    MessageBox.Show("Use amount cannot be more than the available amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        //if (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_USENOW)) > 0)
                        //{
                          
                            if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                            {
                                for (int index = EOBInsurancePaymentMasterLines.Count - 1; index >= 0; index--)
                                {
                                    if (EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true &&
                                        EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == Convert.ToInt64(c1Reserve.GetData(e.Row, COL_EOBPAYMENTDTLID))
                                        && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == Convert.ToInt64(c1Reserve.GetData(e.Row, COL_EOBPAYMENTID))
                                      )
                                    {
                                        EOBInsurancePaymentMasterLines[index].Amount = Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_USENOW));
                                       
                                    }
                                }
                           // }
                        }
                    }



                    //if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                    //{
                    //    for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                    //    {
                    //        if (EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == _selEOBPayId
                    //            && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == _selEOBPayDtlId

                    //            && EOBInsurancePaymentMasterLines[index].PaySign == EOBPaymentSign.Payment_Credit
                    //            && EOBInsurancePaymentMasterLines[index].PaymentType == EOBPaymentType.InsuraceReserverd)
                    //        {
                    //            EOBInsurancePaymentMasterLines[index].Amount = Convert.ToDecimal(c1PaymentReserve.GetData(i, COL_USENOW));
                    //            _isExistReserve = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (_IsUseNowValidate)
                    //{
                    //    if (c1Reserve.GetCellCheck(e.Row, COL_ASSO_SELECT) == CheckEnum.Checked)
                    //    {
                    //        c1Reserve.SetCellCheck(e.Row, COL_ASSO_SELECT, CheckEnum.Unchecked);
                    //        _IsUseNowValidate = false;
                    //        c1Reserve.Select(e.Row, COL_USENOW);

                    //    }
                    //}
                    //else
                    //{
                    //    if ((Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_USENOW)) >= 0) && (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_USENOW)) <= Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_AVAILABLE))))
                    //    {
                    //        if (e.Row > 0 && e.Col == COL_ASSO_SELECT)
                    //        {
                    //            if (c1Reserve.GetData(e.Row, e.Col) != null && Convert.ToString(c1Reserve.GetData(e.Row, e.Col)).Trim() != "")
                    //            {
                    //                if (c1Reserve.GetCellCheck(e.Row, COL_ASSO_SELECT) == CheckEnum.Checked)
                    //                {
                    //                    AddPaymentReserve(e.Row);
                    //                    AddSelectedReserve(e.Row);
                    //                    HideSelectedReserve(e.Row);
                    //                }

                    //            }
                    //        }
                    //    }
                    //}
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

                                    MessageBox.Show("Use amount cannot be negative. Please select a valid amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                    MessageBox.Show("Use amount cannot be more than the available amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1SelectReserve.SetData(e.Row, e.Col, 0);
                                }
                                catch (Exception)// ex)
                                {
                                    //ex.ToString();
                                    //ex = null;
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
                        //if ((Convert.ToDecimal(c1SelectReserve.GetData(e.Row, COL_USENOW)) >= 0) && (Convert.ToDecimal(c1SelectReserve.GetData(e.Row, COL_USENOW)) <= Convert.ToDecimal(c1SelectReserve.GetData(e.Row, COL_AVAILABLE))))
                        //{
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
                       // }
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

        /// <summary>
        /// Add data in the collection
        /// for further use
        /// </summary>
        /// 
        void AddPaymentReserve(int _rowIndex)
        {
           // AmountTakenFromReserve = 0;
            SeletedReserveItems.Clear();

            Int64 _selEOBPayId = 0;
            Int64 _selEOBPayDtlId = 0;
            Int64 _selRefEOBPayId = 0;
            Int64 _selRefEOBPayDtlId = 0;
            Int32 _selEOBPayPayMode = 0;
            bool _isExistReserve = false;
            EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine oEOBInsPaymentResAsCreditDetail = null;

            try
            {
                c1SelectReserve.FinishEditing();
                _isExistReserve = false;
                

                if (c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTID) != null && Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTID)).ToString().Trim() != "")
                { _selEOBPayId = Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTID)); }

                if (c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
                { _selEOBPayDtlId = Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTDTLID)); }

                if (c1SelectReserve.GetData(_rowIndex, COL_REFEOBPAYID) != null && Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_REFEOBPAYID)).ToString().Trim() != "")
                { _selRefEOBPayId = Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_REFEOBPAYID)); }

                if (c1SelectReserve.GetData(_rowIndex, COL_REFEOBPAYDTLID) != null && Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_REFEOBPAYDTLID)).ToString().Trim() != "")
                { _selRefEOBPayDtlId = Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_REFEOBPAYDTLID)); }

                if (c1SelectReserve.GetData(_rowIndex, COL_PAYMODE) != null && Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_PAYMODE)).ToString().Trim() != "")
                {
                    _selEOBPayPayMode = ((EOBPaymentMode)Convert.ToInt32(c1SelectReserve.GetData(_rowIndex, COL_PAYMODE))).GetHashCode();
                }
                if (_isExistReserve == false)
                {
                    #region "Set Object"
                    oEOBInsPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();
                    oEOBInsPaymentResAsCreditDetail.EOBPaymentID = 0;
                    oEOBInsPaymentResAsCreditDetail.EOBID = 0;
                    oEOBInsPaymentResAsCreditDetail.EOBDtlID = 0;
                    oEOBInsPaymentResAsCreditDetail.EOBPaymentDetailID = 0;

                    //will be assigning current check refrences.
                    oEOBInsPaymentResAsCreditDetail.RefEOBPaymentID = _selRefEOBPayId;
                    oEOBInsPaymentResAsCreditDetail.RefEOBPaymentDetailID = _selRefEOBPayDtlId;
                    oEOBInsPaymentResAsCreditDetail.ReserveEOBPaymentID = _selEOBPayId;
                    oEOBInsPaymentResAsCreditDetail.ReserveEOBPaymentDetailID = _selEOBPayDtlId;

                    oEOBInsPaymentResAsCreditDetail.OldRefEOBPaymentID = _selRefEOBPayId;
                    oEOBInsPaymentResAsCreditDetail.OldRefEOBPaymentDetailID = _selRefEOBPayDtlId;
                    oEOBInsPaymentResAsCreditDetail.OldReserveEOBPaymentID = _selEOBPayId;
                    oEOBInsPaymentResAsCreditDetail.OldReserveEOBPaymentDetailID = _selEOBPayDtlId;

                    oEOBInsPaymentResAsCreditDetail.BillingTransactionID = 0;
                    oEOBInsPaymentResAsCreditDetail.BillingTransactionDetailID = 0;
                    oEOBInsPaymentResAsCreditDetail.BillingTransactionLineNo = 0;

                    oEOBInsPaymentResAsCreditDetail.TrackBillingTransactionID = 0;
                    oEOBInsPaymentResAsCreditDetail.TrackBillingTransactionDetailID = 0;
                    oEOBInsPaymentResAsCreditDetail.TrackBillingTransactionLineNo = 0;

                    oEOBInsPaymentResAsCreditDetail.DOSFrom = 0;
                    oEOBInsPaymentResAsCreditDetail.DOSTo = 0;
                    oEOBInsPaymentResAsCreditDetail.CPTCode = "";
                    oEOBInsPaymentResAsCreditDetail.CPTDescription = "";
                    oEOBInsPaymentResAsCreditDetail.Amount = Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_USENOW));

                    //Pending - idenify is patient reserve or insurace reserve
                    // EOBPaymentType _ResIsPatOrIns = EOBPaymentType.InsuraceReserverd;
                    oEOBInsPaymentResAsCreditDetail.PaymentType = EOBPaymentType.InsuraceReserverd;
                    oEOBInsPaymentResAsCreditDetail.PaymentSubType = EOBPaymentSubType.Reserved;
                    oEOBInsPaymentResAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                    oEOBInsPaymentResAsCreditDetail.PayMode = (EOBPaymentMode)_selEOBPayPayMode;

                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID

                    //Pending - idenify is patient reserve or insurace reserve
                    Int64 _ResIDIsPatOrIns = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();
                    Int64 _MstResIDIsPatOrIns = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();

                    oEOBInsPaymentResAsCreditDetail.AccountID = _ResIDIsPatOrIns;
                    oEOBInsPaymentResAsCreditDetail.AccountType = EOBPaymentAccountType.Reserved;
                    oEOBInsPaymentResAsCreditDetail.MSTAccountID = _MstResIDIsPatOrIns;
                    oEOBInsPaymentResAsCreditDetail.MSTAccountType = EOBPaymentAccountType.Reserved;

                    oEOBInsPaymentResAsCreditDetail.PatientID = 0;
                    oEOBInsPaymentResAsCreditDetail.PaymentTrayID = 0;
                    oEOBInsPaymentResAsCreditDetail.PaymentTrayCode = "";
                    oEOBInsPaymentResAsCreditDetail.PaymentTrayDescription = "";
                    oEOBInsPaymentResAsCreditDetail.UserID = _UserId;
                    oEOBInsPaymentResAsCreditDetail.UserName = _UserName;
                    oEOBInsPaymentResAsCreditDetail.ClinicID = _ClinicID;

                    oEOBInsPaymentResAsCreditDetail.FinanceLieNo = 0;
                    oEOBInsPaymentResAsCreditDetail.MainCreditLineID = 0;
                    oEOBInsPaymentResAsCreditDetail.IsMainCreditLine = false;
                    oEOBInsPaymentResAsCreditDetail.IsReserveCreditLine = true;
                    oEOBInsPaymentResAsCreditDetail.IsCorrectionCreditLine = false;
                    oEOBInsPaymentResAsCreditDetail.RefFinanceLieNo = 0;
                    oEOBInsPaymentResAsCreditDetail.UseRefFinanceLieNo = false;
                    oEOBInsPaymentResAsCreditDetail.InsuranceCompanyID = Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_INSURANCE_COMPANY_ID));

                    EOBInsurancePaymentMasterLines.Add(oEOBInsPaymentResAsCreditDetail);

                    #endregion
                }

                _selEOBPayId = 0;
                _selEOBPayDtlId = 0;
                _selEOBPayId = 0;
                _selEOBPayDtlId = 0;
                _selEOBPayPayMode = 0;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }           
        
        }

        /// <summary>
        /// Add Selected Reserve grid to the Payment Reserve grid after selecting row      
        /// </summary>
        /// 
        void AddSelectedReserve( int _rowIndex)
        {
            try
            {
                c1Reserve.Rows.Add();

                #region " Set Data "
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_EOBPAYMENTID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_EOBID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_EOBID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_EOBDTLID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_EOBDTLID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_EOBPAYMENTDTLID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTDTLID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_BLTRANSACTIONID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_BLTRANSACTIONID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_BLTRANDTLID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_BLTRANDTLID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_BLTRANLINEID, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_BLTRANLINEID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_DOSFROM, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_DOSFROM)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_DOSTO, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_DOSTO)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_INSURANCE_COMPANY_ID, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_INSURANCE_COMPANY_ID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_SOURCE, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_SOURCE)));// Insurance Name  

                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ORIGINALPAYMENT, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_ORIGINALPAYMENT)));//Check Number,Date,Amount
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_TORESERVES, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_TORESERVES)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_NOTE, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_NOTE)));//Note
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_DB_AMOUNT, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_DB_AMOUNT)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_OBJ_AMOUNT, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_OBJ_AMOUNT)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_USERESERVE, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_EOBPAYMENTID)));//Used amount
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_AVAILABLE, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_AVAILABLE)));//Available amount
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_USENOW, Convert.ToDecimal(c1SelectReserve.GetData(_rowIndex, COL_AVAILABLE)));//Current amount to use from avaiable amount

                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_PAYMODE, ((EOBPaymentMode)Convert.ToInt32(c1SelectReserve.GetData(_rowIndex, COL_PAYMODE))));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_REFEOBPAYID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_REFEOBPAYID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_REFEOBPAYDTLID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_REFEOBPAYDTLID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ACCOUNTID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_ACCOUNTID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ACCOUNTTYPE, Convert.ToInt32(c1SelectReserve.GetData(_rowIndex, COL_ACCOUNTTYPE)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_MSTACCOUNTID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_MSTACCOUNTID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_MSTACCOUNTTYPE, Convert.ToInt32(c1SelectReserve.GetData(_rowIndex, COL_MSTACCOUNTTYPE)));

                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_RES_EOBPAYID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_RES_EOBPAYID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_RES_EOBPAYDTLID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_RES_EOBPAYDTLID)));

                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_PATIENTID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_ASSO_PATIENTID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_PATIENTNAME, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_ASSO_PATIENTNAME)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_CLAIMNO, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_ASSO_CLAIMNO)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(c1SelectReserve.GetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID)));
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_ASSO_SELECT, false);
                c1Reserve.SetData(c1Reserve.Rows.Count - 1, COL_CREATED_DATE_USER, Convert.ToString(c1SelectReserve.GetData(_rowIndex, COL_CREATED_DATE_USER)));


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
                if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                {
                    for (int index = EOBInsurancePaymentMasterLines.Count - 1; index >= 0; index--)
                    {
                        if (EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true &&
                            EOBInsurancePaymentMasterLines[index].EOBPaymentDetailID == 0
                            && EOBInsurancePaymentMasterLines[index].EOBPaymentID == 0
                            && EOBInsurancePaymentMasterLines[index].Amount == 0)
                        {
                            EOBInsurancePaymentMasterLines.RemoveAt(index);
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

     ///<summary>
     /// Remove item from collection after hit 'Cancel' button
     ///</summary>
        void RemovePaymentReserve()
        {
            if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
            {
                for (int index = EOBInsurancePaymentMasterLines.Count - 1; index >= 0; index--)
                {
                    if (EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true &&
                        EOBInsurancePaymentMasterLines[index].EOBPaymentDetailID == 0
                        && EOBInsurancePaymentMasterLines[index].EOBPaymentID == 0
                       )
                    {
                        EOBInsurancePaymentMasterLines.RemoveAt(index);
                    }
                }
            }        
        }

       
        /// <summary>
        /// Added by Pankaj on 13022010 
        /// Changed by Shweta on 03222011
        /// Get the Insurance Reserve List with available balance
        /// </summary>
        /// <returns></returns>        
        DataTable GetReserves(C1FlexGrid c1Payment)
        {
            DataTable _dtReserves = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            StringBuilder sb_EOBPaymentID = new StringBuilder();
            StringBuilder sb_InsuranceID = new StringBuilder();
            try
            {
                if (c1Payment.Name == "c1SelectReserve" )
                {
                    if (chkCurrentCheck.Checked)
                    {

                        sb_EOBPaymentID.Append(LoadedCheckEOBPaymentID.ToString());
                       

                        if (sb_EOBPaymentID.Length != 0)
                        {
                            oParameters.Add("@nEObPaymentID", sb_EOBPaymentID.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        }
                        oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),

                        oDB.Connect(false);
                        oDB.Retrive("BL_SELECT_PaymentTransaction_UseReserve_Insurance", oParameters, out _dtReserves);
                        oDB.Disconnect();
                    }
                    else
                    {
                        if (_IsFormLoading == true && InsuranceCompanyID != 0 || PatientID != 0 || txtNoteText.Text != "")
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
                            oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),

                            oDB.Connect(false);
                            oDB.Retrive("BL_SELECT_PaymentTransaction_UseReserve_Insurance", oParameters, out _dtReserves);
                            oDB.Disconnect();
                        }
                    }
                   
                }
                else if (c1Payment.Name == "c1Reserve" || chkCurrentCheck.Checked)
                {

                    if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                    {

                        for (int i = 0; i < EOBInsurancePaymentMasterLines.Count; i++)
                        {
                            if (i == EOBInsurancePaymentMasterLines.Count - 1)
                            {
                                sb_EOBPaymentID.Append(EOBInsurancePaymentMasterLines[i].ReserveEOBPaymentID.ToString());
                            }
                            else
                            {
                                sb_EOBPaymentID.Append(EOBInsurancePaymentMasterLines[i].ReserveEOBPaymentID.ToString() + ",");
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
                    oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),

                    oDB.Connect(false);
                    oDB.Retrive("BL_SELECT_PaymentTransaction_UseReserve_Insurance", oParameters, out _dtReserves);
                    oDB.Disconnect();
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


        /// <summary>
        /// Fill the  Reserve Data in to the Select Reserve Grid 
        /// </summary>
        ///
        private void FillSelectReserves()
        {
            _IsFormLoading = true;
            DataTable _dtReserves = GetReserves(c1SelectReserve);            
            decimal _objAmount = 0;
            decimal _db_objAmount = 0;
            decimal _avaible = 0;
            decimal _dbReserves = 0;
            DesignPaymentGrid(c1SelectReserve);
            
         //   Int64 iRow =0;
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
                            c1SelectReserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(row["nEOBPaymentID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_EOBID, Convert.ToInt64(row["nEOBID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_EOBDTLID, Convert.ToInt64(row["nEOBDtlID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(row["nEOBPaymentDetailID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_BLTRANSACTIONID, Convert.ToInt64(row["nBillingTransactionID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_BLTRANDTLID, Convert.ToInt64(row["nBillingTransactionDetailID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_BLTRANLINEID, Convert.ToString(row["nBillingTransactionLineNo"]));
                            c1SelectReserve.SetData(_rowIndex, COL_DOSFROM, Convert.ToInt64(row["nDOSFrom"]));
                            c1SelectReserve.SetData(_rowIndex, COL_DOSTO, Convert.ToString(row["nDOSTo"]));
                            c1SelectReserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_ID, Convert.ToString(row["nAccountID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(row["InsuarnceCompanyName"]));// Insurance Name

                            string _originalPayment = "";
                            _originalPayment = ((EOBPaymentMode)Convert.ToInt32(row["nPayMode"])).ToString() + "# " + Convert.ToString(row["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nCheckDate"])).ToString("MM/dd/yyyy") + " $ " + Convert.ToDecimal(row["nCheckAmount"]);
                            c1SelectReserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                            c1SelectReserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(row["nAmount"]));
                            //c1SelectReserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(row["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                            c1SelectReserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(row["sNoteDescription"]));//Note
                           
                            if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                            {
                                for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                                {
                                    if (
                                        EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == Convert.ToInt64(row["nEOBPaymentID"])
                                        && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == Convert.ToInt64(row["nEOBPaymentDetailID"])
                                        && EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true)
                                    {
                                        if (EOBInsurancePaymentMasterLines[index].EOBPaymentID > 0 && EOBInsurancePaymentMasterLines[index].EOBPaymentDetailID > 0)
                                        {
                                            _objAmount += EOBInsurancePaymentMasterLines[index].Amount;
                                        }
                                        else
                                        { _db_objAmount += EOBInsurancePaymentMasterLines[index].Amount; }

                                        _dbReserves += EOBInsurancePaymentMasterLines[index].DBReserveAmount;
                                    }
                                }
                            }

                            if (Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves <= Convert.ToDecimal(row["nAmount"]))
                            { _avaible = Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves; }
                            else
                            { _avaible = Convert.ToDecimal(row["AvailableReserve"]); }


                            if (_db_objAmount <= 0) { _db_objAmount = _objAmount; }

                            c1SelectReserve.SetData(_rowIndex, COL_DB_AMOUNT, Convert.ToDecimal(row["AvailableReserve"]));                           
                            c1SelectReserve.SetData(_rowIndex, COL_OBJ_AMOUNT, _dbReserves);
                            c1SelectReserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(row["UsedReserve"]));//Used amount
                            c1SelectReserve.SetData(_rowIndex, COL_AVAILABLE, _avaible);//Available amount
                            c1SelectReserve.SetData(_rowIndex, COL_USENOW, _db_objAmount);//Current amount to use from avaiable amount

                            c1SelectReserve.SetData(_rowIndex, COL_PAYMODE, ((EOBPaymentMode)Convert.ToInt32(row["nPayMode"])));
                            c1SelectReserve.SetData(_rowIndex, COL_REFEOBPAYID, Convert.ToInt64(row["nRefEOBPaymentID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_REFEOBPAYDTLID, Convert.ToInt64(row["nRefEOBPaymentDetailID"]));                            
                            c1SelectReserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(row["nAccountID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ACCOUNTTYPE, Convert.ToInt32(row["nAccountType"]));
                            c1SelectReserve.SetData(_rowIndex, COL_MSTACCOUNTID, Convert.ToInt64(row["nMSTAccountID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_MSTACCOUNTTYPE, Convert.ToInt32(row["nMSTAccountType"]));
                            c1SelectReserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nResEOBPaymentID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(row["nResEOBPaymentDetailID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(row["AssociationPatientID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_PATIENTNAME, Convert.ToString(row["AssociationPatient"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_CLAIMNO, Convert.ToString(row["AssociationClaim"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(row["AssociationMSTTransactionID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(row["AssociationnTransactionID"]));
                            c1SelectReserve.SetData(_rowIndex, COL_ASSO_SELECT, false);
                            c1SelectReserve.SetData(_rowIndex, COL_CREATED_DATE_USER, Convert.ToString(row["CreatedDateUser"]));

                            #endregion

                            #region " Set Styles "

                            c1SelectReserve.SetCellStyle(_rowIndex, COL_USENOW, c1SelectReserve.Styles["cs_EditableCurrencyStyle"]);

                            #endregion " Set Styles "

                            if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                            {
                                for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                                {
                                    if (EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == Convert.ToInt64(row["nEOBPaymentID"])
                                     && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == Convert.ToInt64(row["nEOBPaymentDetailID"]))
                                    {
                                        HideSelectedReserve(_rowIndex);
                                    }
                                }
                            }                       
                    }
               }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                this.c1SelectReserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SelectReserve_CellChanged);
                _IsFormLoading = false; 
            } 
        }

        /// <summary>
        /// Fill the used Reserve Data in to the Payment Reserve Grid 
        /// </summary>
        ///
        private void FillReserves()
        {
            _IsFormLoading = true;
            DesignPaymentGrid(c1Reserve);
            DataTable _dtReserves = new DataTable();
            if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
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
                     

                        if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                        {
                            for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                            {
                                if (
                                    EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == Convert.ToInt64(row["nEOBPaymentID"])
                                    && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == Convert.ToInt64(row["nEOBPaymentDetailID"])
                                    && EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true)
                                {
                                    if (EOBInsurancePaymentMasterLines[index].EOBPaymentID > 0 && EOBInsurancePaymentMasterLines[index].EOBPaymentDetailID > 0)
                                    {
                                        _objAmount += EOBInsurancePaymentMasterLines[index].Amount;
                                    }
                                    else
                                    { 
                                        //This is done if the user select the reserve amount & want to locate against the same check from which amount has already used previosly
                                        //in this case to add the previous used amount to current used amount so use now will show total.
                                        for (int i = 0; i < EOBInsurancePaymentMasterLines.Count; i++)
                                        {
                                            if (EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == EOBInsurancePaymentMasterLines[i].ReserveEOBPaymentID
                                                && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == EOBInsurancePaymentMasterLines[i].ReserveEOBPaymentDetailID)
                                            {
                                                _isSameReserve = true;
                                                break;
                                            }                                        
                                        }
                                        if (_isSameReserve)
                                        {
                                            _objAmount += EOBInsurancePaymentMasterLines[index].Amount;
                                        }
                                        else
                                        {
                                            _db_objAmount += EOBInsurancePaymentMasterLines[index].Amount;
                                        }
                                    }

                                    _dbReserves += EOBInsurancePaymentMasterLines[index].DBReserveAmount;
                                }
                            }
                        }
                        if (_db_objAmount <= 0) { _db_objAmount = _objAmount; }

                        if (_IsReserveOpenForModify)
                        {   
                            #region " Set Data "

                            c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(row["nEOBPaymentID"]));
                            c1Reserve.SetData(_rowIndex, COL_EOBID, Convert.ToInt64(row["nEOBID"]));
                            c1Reserve.SetData(_rowIndex, COL_EOBDTLID, Convert.ToInt64(row["nEOBDtlID"]));
                            c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(row["nEOBPaymentDetailID"]));
                            c1Reserve.SetData(_rowIndex, COL_BLTRANSACTIONID, Convert.ToInt64(row["nBillingTransactionID"]));
                            c1Reserve.SetData(_rowIndex, COL_BLTRANDTLID, Convert.ToInt64(row["nBillingTransactionDetailID"]));
                            c1Reserve.SetData(_rowIndex, COL_BLTRANLINEID, Convert.ToString(row["nBillingTransactionLineNo"]));
                            c1Reserve.SetData(_rowIndex, COL_DOSFROM, Convert.ToInt64(row["nDOSFrom"]));
                            c1Reserve.SetData(_rowIndex, COL_DOSTO, Convert.ToString(row["nDOSTo"]));
                            c1Reserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_ID, Convert.ToString(row["nAccountID"]));
                            c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(row["InsuarnceCompanyName"]));// Insurance Name

                            string _originalPayment = "";
                            _originalPayment = ((EOBPaymentMode)Convert.ToInt32(row["nPayMode"])).ToString() + "# " + Convert.ToString(row["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nCheckDate"])).ToString("MM/dd/yyyy") + " $ " + Convert.ToDecimal(row["nCheckAmount"]);
                            c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                            c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(row["nAmount"]));
                            //c1PaymentReserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(row["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                            c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(row["sNoteDescription"]));//Note

                            //if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                            //{
                            //    for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                            //    {
                            //        if (
                            //            EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == Convert.ToInt64(row["nEOBPaymentID"])
                            //            && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == Convert.ToInt64(row["nEOBPaymentDetailID"])
                            //            && EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true)
                            //        {
                            //            if (EOBInsurancePaymentMasterLines[index].EOBPaymentID > 0 && EOBInsurancePaymentMasterLines[index].EOBPaymentDetailID > 0)
                            //            {
                            //                _objAmount += EOBInsurancePaymentMasterLines[index].Amount;
                            //            }
                            //            else
                            //            { _db_objAmount += EOBInsurancePaymentMasterLines[index].Amount; }

                            //            _dbReserves += EOBInsurancePaymentMasterLines[index].DBReserveAmount;
                            //        }
                            //    }
                            //}

                            if (Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves <= Convert.ToDecimal(row["nAmount"]))
                            { _avaible = Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves; }
                            else
                            { _avaible = Convert.ToDecimal(row["AvailableReserve"]); }

                            //_avaible = Convert.ToDecimal(row["AvailableReserve"]) + _objAmount; 


                            // if (_db_objAmount <= 0) { _db_objAmount = _objAmount; }

                            //c1PaymentReserve.SetData(_rowIndex, COL_DB_AMOUNT, Convert.ToDecimal(row["AvailableReserve"]));

                            c1Reserve.SetData(_rowIndex, COL_DB_AMOUNT, Convert.ToDecimal(row["AvailableReserve"]));

                            //c1PaymentReserve.SetData(_rowIndex, COL_OBJ_AMOUNT, _db_objAmount);
                            c1Reserve.SetData(_rowIndex, COL_OBJ_AMOUNT, _dbReserves);

                            c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(row["UsedReserve"]));//Used amount
                            c1Reserve.SetData(_rowIndex, COL_AVAILABLE, _avaible);//Available amount
                            c1Reserve.SetData(_rowIndex, COL_USENOW, _db_objAmount);//Current amount to use from avaiable amount

                            c1Reserve.SetData(_rowIndex, COL_PAYMODE, ((EOBPaymentMode)Convert.ToInt32(row["nPayMode"])));
                            c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID, Convert.ToInt64(row["nRefEOBPaymentID"]));
                            c1Reserve.SetData(_rowIndex, COL_REFEOBPAYDTLID, Convert.ToInt64(row["nRefEOBPaymentDetailID"]));
                            //c1PaymentReserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nEOBPaymentID"]));
                            //c1PaymentReserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(row["nEOBPaymentDetailID"]));
                            c1Reserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(row["nAccountID"]));
                            c1Reserve.SetData(_rowIndex, COL_ACCOUNTTYPE, Convert.ToInt32(row["nAccountType"]));
                            c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTID, Convert.ToInt64(row["nMSTAccountID"]));
                            c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTTYPE, Convert.ToInt32(row["nMSTAccountType"]));

                            c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nResEOBPaymentID"]));
                            c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(row["nResEOBPaymentDetailID"]));

                            c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(row["AssociationPatientID"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTNAME, Convert.ToString(row["AssociationPatient"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_CLAIMNO, Convert.ToString(row["AssociationClaim"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(row["AssociationMSTTransactionID"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(row["AssociationnTransactionID"]));
                            c1Reserve.SetData(_rowIndex, COL_ASSO_SELECT, false);
                            c1Reserve.SetData(_rowIndex, COL_CREATED_DATE_USER, Convert.ToString(row["CreatedDateUser"]));

                             #endregion

                            #region " Set Styles "

                            c1Reserve.SetCellStyle(_rowIndex, COL_USENOW, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                            #endregion " Set Styles "

                        }
                        else
                        {
                            //if (_db_objAmount != 0 || _dbReserves != 0)
                            //{
                                #region " Set Data "
                                c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(row["nEOBPaymentID"]));
                                c1Reserve.SetData(_rowIndex, COL_EOBID, Convert.ToInt64(row["nEOBID"]));
                                c1Reserve.SetData(_rowIndex, COL_EOBDTLID, Convert.ToInt64(row["nEOBDtlID"]));
                                c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(row["nEOBPaymentDetailID"]));
                                c1Reserve.SetData(_rowIndex, COL_BLTRANSACTIONID, Convert.ToInt64(row["nBillingTransactionID"]));
                                c1Reserve.SetData(_rowIndex, COL_BLTRANDTLID, Convert.ToInt64(row["nBillingTransactionDetailID"]));
                                c1Reserve.SetData(_rowIndex, COL_BLTRANLINEID, Convert.ToString(row["nBillingTransactionLineNo"]));
                                c1Reserve.SetData(_rowIndex, COL_DOSFROM, Convert.ToInt64(row["nDOSFrom"]));
                                c1Reserve.SetData(_rowIndex, COL_DOSTO, Convert.ToString(row["nDOSTo"]));
                                c1Reserve.SetData(_rowIndex, COL_INSURANCE_COMPANY_ID, Convert.ToString(row["nAccountID"]));
                                c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(row["InsuarnceCompanyName"]));// Insurance Name

                                string _originalPayment = "";
                                _originalPayment = ((EOBPaymentMode)Convert.ToInt32(row["nPayMode"])).ToString() + "# " + Convert.ToString(row["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nCheckDate"])).ToString("MM/dd/yyyy") + " $ " + Convert.ToDecimal(row["nCheckAmount"]);
                                c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                                c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(row["nAmount"]));
                                //c1PaymentReserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(row["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                                c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(row["sNoteDescription"]));//Note

                                //if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                                //{
                                //    for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                                //    {
                                //        if (
                                //            EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == Convert.ToInt64(row["nEOBPaymentID"])
                                //            && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == Convert.ToInt64(row["nEOBPaymentDetailID"])
                                //            && EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true)
                                //        {
                                //            if (EOBInsurancePaymentMasterLines[index].EOBPaymentID > 0 && EOBInsurancePaymentMasterLines[index].EOBPaymentDetailID > 0)
                                //            {
                                //                _objAmount += EOBInsurancePaymentMasterLines[index].Amount;
                                //            }
                                //            else
                                //            { _db_objAmount += EOBInsurancePaymentMasterLines[index].Amount; }

                                //            _dbReserves += EOBInsurancePaymentMasterLines[index].DBReserveAmount;
                                //        }
                                //    }
                                //}

                                if (Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves <= Convert.ToDecimal(row["nAmount"]))
                                { _avaible = Convert.ToDecimal(row["AvailableReserve"]) + _dbReserves; }
                                else
                                { _avaible = Convert.ToDecimal(row["AvailableReserve"]); }

                                //_avaible = Convert.ToDecimal(row["AvailableReserve"]) + _objAmount; 


                                // if (_db_objAmount <= 0) { _db_objAmount = _objAmount; }

                                //c1PaymentReserve.SetData(_rowIndex, COL_DB_AMOUNT, Convert.ToDecimal(row["AvailableReserve"]));

                                c1Reserve.SetData(_rowIndex, COL_DB_AMOUNT, Convert.ToDecimal(row["AvailableReserve"]));

                                //c1PaymentReserve.SetData(_rowIndex, COL_OBJ_AMOUNT, _db_objAmount);
                                c1Reserve.SetData(_rowIndex, COL_OBJ_AMOUNT, _dbReserves);

                                c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(row["UsedReserve"]));//Used amount
                                c1Reserve.SetData(_rowIndex, COL_AVAILABLE, _avaible);//Available amount
                                c1Reserve.SetData(_rowIndex, COL_USENOW, _db_objAmount);//Current amount to use from avaiable amount

                                c1Reserve.SetData(_rowIndex, COL_PAYMODE, ((EOBPaymentMode)Convert.ToInt32(row["nPayMode"])));
                                c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID, Convert.ToInt64(row["nRefEOBPaymentID"]));
                                c1Reserve.SetData(_rowIndex, COL_REFEOBPAYDTLID, Convert.ToInt64(row["nRefEOBPaymentDetailID"]));
                                //c1PaymentReserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nEOBPaymentID"]));
                                //c1PaymentReserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(row["nEOBPaymentDetailID"]));
                                c1Reserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(row["nAccountID"]));
                                c1Reserve.SetData(_rowIndex, COL_ACCOUNTTYPE, Convert.ToInt32(row["nAccountType"]));
                                c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTID, Convert.ToInt64(row["nMSTAccountID"]));
                                c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTTYPE, Convert.ToInt32(row["nMSTAccountType"]));

                                c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(row["nResEOBPaymentID"]));
                                c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(row["nResEOBPaymentDetailID"]));

                                c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(row["AssociationPatientID"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTNAME, Convert.ToString(row["AssociationPatient"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_CLAIMNO, Convert.ToString(row["AssociationClaim"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(row["AssociationMSTTransactionID"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(row["AssociationnTransactionID"]));
                                c1Reserve.SetData(_rowIndex, COL_ASSO_SELECT, false);
                                c1Reserve.SetData(_rowIndex, COL_CREATED_DATE_USER, Convert.ToString(row["CreatedDateUser"]));

                            #endregion

                                #region " Set Styles "

                                c1Reserve.SetCellStyle(_rowIndex, COL_USENOW, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                                #endregion " Set Styles "

                            //}
                            //else
                            //{
                            //    c1Reserve.Rows.Remove(_rowIndex);
                            //}                       
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
       
        /// <summary>
        ///Get selected patient claim#
        /// </summary>
        ///
        private void getPatientClaimNos(Int64 nPatientID)        
        {
           
            DataTable _dtClaimNo = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
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
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
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
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");  
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
                                _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                                    + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                                    + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                                    + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                                    + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + "";
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

        private void OpenReserveForModify(int RowIndex, C1FlexGrid c1Payment)
        {
           
                
                Int64 _nEOBPaymentID = 0;
                Int64 _nEOBPaymentDetailID = 0;                
                EOBPayment.Common.InsuranceReserveRemainingDetails _ReserveDetails = new global::gloBilling.EOBPayment.Common.InsuranceReserveRemainingDetails();


                string _ReserveNote = string.Empty;
                string _InsuranceCompany = string.Empty;
                _IsReserveOpenForModify = true;
                try
                {
                    if (c1Payment.GetData(RowIndex, COL_EOBPAYMENTID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_EOBPAYMENTID)).ToString().Trim() != "")
                    { _nEOBPaymentID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_EOBPAYMENTID)); }

                    if (c1Payment.GetData(RowIndex, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
                    { _nEOBPaymentDetailID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_EOBPAYMENTDTLID)); }

                    if (c1Payment.GetData(RowIndex, COL_INSURANCE_COMPANY_ID) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_INSURANCE_COMPANY_ID)).Trim() != "")
                    { _ReserveDetails.InsuranceCompanyID = Convert.ToInt64(c1Payment.GetData(RowIndex, COL_INSURANCE_COMPANY_ID)); }

                    if (c1Payment.GetData(RowIndex, COL_NOTE) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_NOTE)).Trim() != "")
                    { _ReserveDetails.ReserveNote = Convert.ToString(c1Payment.GetData(RowIndex, COL_NOTE)); }

                    if (c1Payment.GetData(RowIndex, COL_SOURCE) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_SOURCE)).Trim() != "")
                    { _ReserveDetails.InsuranceCompany = Convert.ToString(c1Payment.GetData(RowIndex, COL_SOURCE)); }

                    if (c1Payment.GetData(RowIndex, COL_TORESERVES) != null && Convert.ToString(c1Payment.GetData(RowIndex, COL_TORESERVES)).Trim() != "")
                    { _ReserveDetails.AmountToReserve = Convert.ToDecimal(c1Payment.GetData(RowIndex, COL_TORESERVES)); }

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

                    frmInsuranceReserveRemaining ofrmInsuranceReserveRemaining = new frmInsuranceReserveRemaining(_databaseconnectionstring, _nEOBPaymentID, _nEOBPaymentDetailID);
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
                    if(ofrmInsuranceReserveRemaining !=null)
                    {
                    
                      ofrmInsuranceReserveRemaining.Dispose();
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
                //c1SelectReserve.Clear();
                c1SelectReserve.DataSource = null;
                c1SelectReserve.Rows.Count = 1;
            
            }

        }

        private void cmbClaimNo_Leave(object sender, EventArgs e)
        {
            getValidClaimDetails();
        }

        


        //void SaveNClose()
        //{
        //    AmountTakenFromReserve = 0;
        //    SeletedReserveItems.Clear();

        //    Int64 _selEOBPayId = 0;
        //    Int64 _selEOBPayDtlId = 0;
        //    Int64 _selRefEOBPayId = 0;
        //    Int64 _selRefEOBPayDtlId = 0;
        //    Int32 _selEOBPayPayMode = 0;
        //    bool _isExistReserve = false;
        //    EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine oEOBInsPaymentResAsCreditDetail = null;

        //    c1Reserve.FinishEditing();
        //    for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
        //    {
        //        _isExistReserve = false;
        //        if (c1Reserve.GetData(i, COL_USENOW) != null && Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).ToString().Trim() != "")
        //        {
        //            //if (Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW)) > 0)
        //            {
        //                AmountTakenFromReserve += Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW));

        //                if (c1Reserve.GetData(i, COL_EOBPAYMENTID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTID)).ToString().Trim() != "")
        //                { _selEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTID)); }

        //                if (c1Reserve.GetData(i, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
        //                { _selEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)); }

        //                if (c1Reserve.GetData(i, COL_REFEOBPAYID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYID)).ToString().Trim() != "")
        //                { _selRefEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYID)); }

        //                if (c1Reserve.GetData(i, COL_REFEOBPAYDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)).ToString().Trim() != "")
        //                { _selRefEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)); }

        //                if (c1Reserve.GetData(i, COL_PAYMODE) != null && Convert.ToString(c1Reserve.GetData(i, COL_PAYMODE)).ToString().Trim() != "")
        //                {
        //                    _selEOBPayPayMode = ((EOBPaymentMode)Convert.ToInt32(c1Reserve.GetData(i, COL_PAYMODE))).GetHashCode();
        //                }

        //                if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
        //                {
        //                    for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
        //                    {
        //                        if (EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentID == _selEOBPayId
        //                            && EOBInsurancePaymentMasterLines[index].ReserveEOBPaymentDetailID == _selEOBPayDtlId

        //                            && EOBInsurancePaymentMasterLines[index].PaySign == EOBPaymentSign.Payment_Credit
        //                            && EOBInsurancePaymentMasterLines[index].PaymentType == EOBPaymentType.InsuraceReserverd)
        //                        {
        //                            EOBInsurancePaymentMasterLines[index].Amount = Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW));
        //                            _isExistReserve = true;
        //                            break;
        //                        }
        //                    }
        //                }

        //                if (_isExistReserve == false && Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW)) > 0)
        //                {
        //                    #region "Set Object"
        //                    oEOBInsPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();
        //                    oEOBInsPaymentResAsCreditDetail.EOBPaymentID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.EOBID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.EOBDtlID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.EOBPaymentDetailID = 0;

        //                    //will be assigning current check refrences.
        //                    oEOBInsPaymentResAsCreditDetail.RefEOBPaymentID = _selRefEOBPayId;
        //                    oEOBInsPaymentResAsCreditDetail.RefEOBPaymentDetailID = _selRefEOBPayDtlId;
        //                    oEOBInsPaymentResAsCreditDetail.ReserveEOBPaymentID = _selEOBPayId;
        //                    oEOBInsPaymentResAsCreditDetail.ReserveEOBPaymentDetailID = _selEOBPayDtlId;

        //                    oEOBInsPaymentResAsCreditDetail.OldRefEOBPaymentID = _selRefEOBPayId;
        //                    oEOBInsPaymentResAsCreditDetail.OldRefEOBPaymentDetailID = _selRefEOBPayDtlId;
        //                    oEOBInsPaymentResAsCreditDetail.OldReserveEOBPaymentID = _selEOBPayId;
        //                    oEOBInsPaymentResAsCreditDetail.OldReserveEOBPaymentDetailID = _selEOBPayDtlId;

        //                    oEOBInsPaymentResAsCreditDetail.BillingTransactionID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.BillingTransactionDetailID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.BillingTransactionLineNo = 0;

        //                    oEOBInsPaymentResAsCreditDetail.TrackBillingTransactionID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.TrackBillingTransactionDetailID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.TrackBillingTransactionLineNo = 0;

        //                    oEOBInsPaymentResAsCreditDetail.DOSFrom = 0;
        //                    oEOBInsPaymentResAsCreditDetail.DOSTo = 0;
        //                    oEOBInsPaymentResAsCreditDetail.CPTCode = "";
        //                    oEOBInsPaymentResAsCreditDetail.CPTDescription = "";
        //                    oEOBInsPaymentResAsCreditDetail.Amount = Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW));

        //                    //Pending - idenify is patient reserve or insurace reserve
        //                    // EOBPaymentType _ResIsPatOrIns = EOBPaymentType.InsuraceReserverd;
        //                    oEOBInsPaymentResAsCreditDetail.PaymentType = EOBPaymentType.InsuraceReserverd;
        //                    oEOBInsPaymentResAsCreditDetail.PaymentSubType = EOBPaymentSubType.Reserved;
        //                    oEOBInsPaymentResAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
        //                    oEOBInsPaymentResAsCreditDetail.PayMode = (EOBPaymentMode)_selEOBPayPayMode;

        //                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID

        //                    //Pending - idenify is patient reserve or insurace reserve
        //                    Int64 _ResIDIsPatOrIns = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();
        //                    Int64 _MstResIDIsPatOrIns = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();

        //                    oEOBInsPaymentResAsCreditDetail.AccountID = _ResIDIsPatOrIns;
        //                    oEOBInsPaymentResAsCreditDetail.AccountType = EOBPaymentAccountType.Reserved;
        //                    oEOBInsPaymentResAsCreditDetail.MSTAccountID = _MstResIDIsPatOrIns;
        //                    oEOBInsPaymentResAsCreditDetail.MSTAccountType = EOBPaymentAccountType.Reserved;

        //                    oEOBInsPaymentResAsCreditDetail.PatientID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.PaymentTrayID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.PaymentTrayCode = "";
        //                    oEOBInsPaymentResAsCreditDetail.PaymentTrayDescription = "";
        //                    oEOBInsPaymentResAsCreditDetail.UserID = _UserId;
        //                    oEOBInsPaymentResAsCreditDetail.UserName = _UserName;
        //                    oEOBInsPaymentResAsCreditDetail.ClinicID = _ClinicID;

        //                    oEOBInsPaymentResAsCreditDetail.FinanceLieNo = 0;
        //                    oEOBInsPaymentResAsCreditDetail.MainCreditLineID = 0;
        //                    oEOBInsPaymentResAsCreditDetail.IsMainCreditLine = false;
        //                    oEOBInsPaymentResAsCreditDetail.IsReserveCreditLine = true;
        //                    oEOBInsPaymentResAsCreditDetail.IsCorrectionCreditLine = false;
        //                    oEOBInsPaymentResAsCreditDetail.RefFinanceLieNo = 0;
        //                    oEOBInsPaymentResAsCreditDetail.UseRefFinanceLieNo = false;

        //                    EOBInsurancePaymentMasterLines.Add(oEOBInsPaymentResAsCreditDetail);

        //                    #endregion
        //                }

        //                //gloGeneralItem.gloItem SeletedReserveItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).Trim());
        //                //SeletedReserveItem.SubItems.Add(_selRefEOBPayId, _selEOBPayPayMode.ToString(), _selRefEOBPayDtlId.ToString());
        //                //SeletedReserveItems.Add(SeletedReserveItem);
        //                //SeletedReserveItems.Dispose();

        //                _selEOBPayId = 0;
        //                _selEOBPayDtlId = 0;
        //                _selEOBPayId = 0;
        //                _selEOBPayDtlId = 0;
        //                _selEOBPayPayMode = 0;
        //            }
        //        }
        //    }

        //    if (AmountTakenFromReserve >= 0)
        //    { this.DialogResult = DialogResult.OK; }
        //    else
        //    { this.DialogResult = DialogResult.Cancel; }


        //    if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
        //    {
        //        for (int index = EOBInsurancePaymentMasterLines.Count - 1; index >= 0; index--)
        //        {
        //            if (EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true &&
        //                EOBInsurancePaymentMasterLines[index].EOBPaymentDetailID == 0
        //                && EOBInsurancePaymentMasterLines[index].EOBPaymentID == 0
        //                && EOBInsurancePaymentMasterLines[index].Amount == 0)
        //            {
        //                EOBInsurancePaymentMasterLines.RemoveAt(index);
        //            }
        //        }
        //    }

        //    this.Close();
        //}

             
        #endregion
    }
}