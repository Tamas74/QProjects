using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloSettings;

namespace gloPatientStripControl
{
    public partial class ucPatientStripControl : UserControl
    {
        #region " Variable Declarations "

        //bool _isControlLoaded = false;

        string _Age = string.Empty;
        bool _AllowEditingParty = true;
        
        //DataView dvPatient = new DataView();
        //DataTable dtTemp = new DataTable();
        //DataView dvNext = new DataView();

        //Size
        Int32 StripHeight = 110; //95;
     //   Int32 MainPanelHeight = 56;//65;

        //Patient Fields
        private Int64 _PatientID = 0;
    //    private Int64 _TransactionID = 0;
    //    private Int64 _TransactionMasterID = 0;
        private string _PatientCode = "";
        private string _PatientName = "";
        private string _ProviderName = "";
        //private string _PatientHomePhone = "";
        //private string _Gender = "";
        //private string _PatientCellPhone = "";
        //private string _PrimaryInsurance = "";
        //private string _SecondaryInsurance = "";
        //private string _HandDominance = "";
        //private string _ReferralPhysician = "";
        //private string _SSN = "";
        //private string _PatientOccupation = "";
        //private string _PatientsMaritalStatus = "";
        //private Int64 _ProviderID = 0;

        private Int64 _ClaimNumber = 0;
        private string _SubClaimNumber = string.Empty;
       // private string _ClaimSubClaimNo = "";

        private DateTime _DateOfBirth;
        private DateTime _TransactionDate = DateTime.Now;

        //private string _PharmacyName = "";
        //private string _PharmacyPhone = "";
        //private string _PharmacyFax = "";


        ////Private boolean variables to hide or show panels
        //private bool _HideAllPanels = false;

        //private bool _HidePatientHomePhone = true;
        //private bool _HideGender = true;
        //private bool _HidePatientCellPhone = true;
        //private bool _HidePrimaryInsurance = true;
        //private bool _HideSecondaryInsurance = true;
        //private bool _HideHandDominance = true;
        //private bool _HideReferralPhysician = true;
        //private bool _HideSSN = true;
        //private bool _HidePatientOccupation = true;
        //private bool _HidePharmacyName = true;
        //private bool _HidePharmacyPhone = true;
        //private bool _HidePharmacyFax = true;

        private bool _ShowTotalBalance = false;
        private bool _ShowInsuraces = false;
        private bool _ShowAlerts = false;

        private const int COL_PAT_ID = 0;
        private const int COL_PAT_Code = 1;
        private const int COL_PAT_FirstName = 2;
        private const int COL_PAT_MI = 3;
        private const int COL_PAT_LastName = 4;
        private const int COL_PAT_SSN = 5;
        private const int COL_PAT_Provider = 6;
        private const int COL_PAT_DOB = 7;
        private const int COL_PAT_Phone = 8;
        private const int COL_PAT_Mobile = 9;
        private bool _isTransactionOpen = false;
        private string _recordMachineId = "";
        private Int64 _recordUserId = 0;

        const int COL_SELECT = 0;
        const int COL_PARTY = 1;
        const int COL_INSURANCEID = 2;
        const int COL_INSURANCENAME = 3;
        const int COL_INSURANCETYPE = 4;
        const int COL_INSSELFMODE = 5;
        const int COL_INSURANCECOPAYAMT = 6;
        const int COL_INSURANCEWORKERCOMP = 7;
        const int COL_INSURANCEAUTOCLAIM = 8;
        const int COL_CONTACTID = 9;
        const int COL_COPAY = 10;
        const int COL_INSVIEW_COUNT = 11;


        private bool _viewSearchOptionCheckBox = true;
        //private InsuracePaymentType _InsuracePaymentType = InsuracePaymentType.None;
        //private bool _showUpDown = false;

        //private FormName _FormName = FormName.None;
        //private int _selectedPartyNo = -1;
        //private Boolean _IsSearchOnPatientCode = false;
        private bool _HasSecondaryInsOnClaim = false;

        #endregion

        #region " Property procedures "

        public string PatientCode
        {
            get { return _PatientCode; }
            set
            {
                _PatientCode = value;
                lblPatientCode.Text = _PatientCode;
            }
        }

        public string PatientName
        {
            get { return _PatientName; }
            set 
            { 
                _PatientName = value;
                lblPatientName.Text = _PatientName;
            }
        }

        public string ProviderName
        {
            get { return _ProviderName; }
            set 
            { 
                _ProviderName = value;
                lblProvider.Text = _ProviderName;
            }
        }

        public DateTime PatientDOB
        {
            get { return _DateOfBirth; }
            set 
            { 
                _DateOfBirth = value;
                lblDOB.Text = _DateOfBirth.ToString("MM/dd/yyyy");
            }
        }

        public string PatientAge
        {
            get { return _Age; }
            set
            {
                _Age = value;
                lblAge.Text = _Age;
            }
        }

        public string ClaimDisplayNo
        {
            get
            {
                string _claim = string.Empty;
                if (_ClaimNumber > 0)
                {
                    _claim = PatientStripControl.GetFormattedClaimPaymentNumber(Convert.ToString(_ClaimNumber));
                    if (!String.IsNullOrEmpty(_SubClaimNumber))
                    {
                        _claim = string.Concat(_claim, "-", _SubClaimNumber);
                    }
                }
                return _claim;
            }
        }

        public bool AllowEditingParty
        {
            get { return _AllowEditingParty; }
            set { _AllowEditingParty = value; }
        }

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        public Int64 ClaimNumber
        {
            get { return _ClaimNumber; }
            set { _ClaimNumber = value; }
        }

        public string SubClaimNumber
        {
            get { return _SubClaimNumber; }
            set { _SubClaimNumber = value; }
        }

        public string ClaimText
        {
            get 
            { 
                return Convert.ToString(txtPatientSearch.Text); 
            }
        }

        public string ClaimFieldText
        {
            get { return Convert.ToString(txtPatientSearch.Text); }
            set { txtPatientSearch.Text = value; }
        }

        int _selectedInsuranceParty = 0;
        public int SelectedInsuranceParty
        {
            get { return _selectedInsuranceParty; }
            set { _selectedInsuranceParty = value; }
        }

        decimal _totalBalance = 0;
        public decimal TotalBalance
        {
            get { return _totalBalance; }
            set
            {
                _totalBalance = value;
                lblTotalBalance.Text = "$ " + _totalBalance.ToString("#0.00");
            }
        }

        decimal _insuranceDue = 0;
        public decimal InsuranceDue
        {
            get { return _insuranceDue; }
            set
            {
                _insuranceDue = value;
                lblInsurancePending.Text = "$ " + _insuranceDue.ToString("#0.00");
            }
        }

        decimal _patientDue = 0;
        public decimal PatientDue
        {
            get { return _patientDue; }
            set
            {
                _patientDue = value;
                lblPatientPending.Text = "$ " + _patientDue.ToString("#0.00");
            }
        }

        decimal _totalAvailableReserves = 0;
        public decimal TotalAvailableReserves
        {
            get { return _totalAvailableReserves; }
            set { _totalAvailableReserves = value; }
        }

        decimal _copayReserve = 0;
        public decimal CopayReserve
        {
            get { return _copayReserve; }
            set
            {
                _copayReserve = value;
                lblPendingCopay.Text = "$ " + _copayReserve.ToString("#0.00");
            }
        }

        decimal _advanceReserve = 0;
        public decimal AdvanceReserve
        {
            get { return _advanceReserve; }
            set
            {
                _advanceReserve = value;
                lblPendingAdvance.Text = "$ " + _advanceReserve.ToString("#0.00");
            }
        }

        decimal _otherReserve = 0;
        public decimal OtherReserve
        {
            get { return _otherReserve; }
            set
            {
                _otherReserve = value;
                lblPendingOtherReserved.Text = "$ " + _otherReserve.ToString("#0.00");
            }
        }

        public bool ShowTotalBalance
        {
            get { return pnlTotalBalance.Visible; }
            set
            {
                _ShowTotalBalance = value;
                pnlTotalBalance.Visible = value;
            }
        }

        public bool ShowInsurances
        {
            get { return pnlInsurace.Visible; }
            set
            {
                _ShowInsuraces = value;
                pnlInsurace.Visible = value;
            }
        }

        public bool ViewSearchOptionCheckBox
        {
            set
            {
                _viewSearchOptionCheckBox = value;
                chk_ClaimNoSearch.Visible = _viewSearchOptionCheckBox;
                chk_ClaimNoSearch.Checked = true;
            }
        }

        public bool ShowPatientClaimSearchForm
        {
            get { return btnSearchPatientClaim.Visible; }
            set { btnSearchPatientClaim.Visible = value; }
        }

        public string PatientStripHeaderText
        {
            get { return label6.Text; }
            set { label6.Text = value; }
        }

        public bool HasSecondaryInsOnClaim
        {
            get { return _HasSecondaryInsOnClaim; }
            set { _HasSecondaryInsOnClaim = value; }
        }

        public bool IsSplitClaimSearchActive
        {
            get { return pnlClaimSearch.Visible; }
            set { pnlClaimSearch.Visible = value; }
        }

        #endregion

        #region " Delegates & Events "

        public delegate void ClaimNumberEntered(string ClaimText);
        public event ClaimNumberEntered OnClaimNumberEntered;

        //public delegate void InsuranceSelected(Int64 InsuranceID, Int32 InsuraceSelfMode, Int64 ContactID, string InsurancePlanName);

        public delegate void InsuranceSelected(InsuranceSelectedArgs e);
        public event InsuranceSelected OnInsuranceSelected;

        public delegate void PatientModified();
        public event PatientModified OnPatientModified;

        public delegate void ClearPatientDetails();
        public event ClearPatientDetails OnClearPatientDetails;


        #endregion

        #region " Constructors "

        public ucPatientStripControl()
        {
            InitializeComponent();
        } 

        #endregion

        #region  " Patient Strip Events "

        private void PatientStripControl_Load(object sender, EventArgs e)
        {
            txtPatientSearch.ContextMenu = null;// new ContextMenu();
            SetupControls();
        }

        private void PatientStripControl_Paint(object sender, PaintEventArgs e)
        {
            if (pnl_Main.Width > 0)
            {
                pnlLeft.Width = pnl_Main.Width / 2;
                pnlRight.Width = pnl_Main.Width - pnlLeft.Width;
            }
        }

        #endregion

        #region " Patient strip control events "

        private void btn_ModityPatient_Click(object sender, EventArgs e)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(AppSettings.ConnectionStringPM);
            try
            {
                if (oSecurity.isPatientLock(_PatientID, true) == false && _PatientID > 0)
                {
                    gloPatient.frmSetupPatient ofrmSetupPatient = new gloPatient.frmSetupPatient(_PatientID, AppSettings.ConnectionStringPM);
                    ofrmSetupPatient.ShowDialog(this);
                    ofrmSetupPatient.Dispose();
                    FillDetails();

                    if (OnPatientModified != null)
                    { OnPatientModified(); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oSecurity.Dispose();
                oSecurity = null;
            }
        }

        private void btn_ModityPatient_MouseHover(object sender, EventArgs e)
        {
            btn_ModityPatient.BackgroundImage = global::gloPatientStripControl.Properties.Resources.PatientHover;
            btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;

            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(btn_ModityPatient, "Modify Patient");
        }

        private void btn_ModityPatient_MouseLeave(object sender, EventArgs e)
        {
            btn_ModityPatient.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Patient;
            btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnSearchPatientClaim_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmPatientClaims ofrmPatientClaims = new frmPatientClaims())
                {
                    ofrmPatientClaims.PatientId = _PatientID;
                    ofrmPatientClaims.SelectedClaim = ClaimDisplayNo; //_SubClaimNumber; //_ClaimSubClaimNo;

                    if (ofrmPatientClaims.ShowDialog(this) == DialogResult.Yes)
                    {
                        _PatientID = ofrmPatientClaims.PatientId;

                        if (ofrmPatientClaims.ClaimNo > 0)
                        {
                            txtPatientSearch.Text = ofrmPatientClaims.SelectedClaim;
                            SetClaimNumbers(false);
                        }
                        else
                        { txtPatientSearch.Text = ""; }
                    }
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnSearchPatientClaim_MouseLeave(object sender, EventArgs e)
        {
            btnSearchPatientClaim.BackgroundImage = null;
            btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnSearchPatientClaim_MouseHover(object sender, EventArgs e)
        {
            btnSearchPatientClaim.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Img_Yellow;
            btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void txtPatientSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chk_ClaimNoSearch.Checked == true)
            {
                if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
                {
                    if (e.KeyChar == Convert.ToChar(45) && txtPatientSearch.Text.Contains("-") == true)
                    {
                        e.Handled = true;
                    }
                    else if (e.KeyChar == Convert.ToChar(45) && txtPatientSearch.Text.Contains("-") == false)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            if (e.KeyChar == 46)
            {
                //e.Handled =false;
            }
            if (e.KeyChar == 13)
            {
                if (IsValidClaimNumber())
                {
                    _isTransactionOpen = PatientStripControl.IsRecordOpen(txtPatientSearch.Text.ToString(), out _recordMachineId, out _recordUserId);
                    if (!_isTransactionOpen)
                    {
                        SetClaimNumbers(true);
                    }
                    else
                    {
                        DialogResult _dlgRst = DialogResult.None;
                        _dlgRst = MessageBox.Show("Transaction is already opened for modify on machine " + _recordMachineId +".", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private bool IsValidClaimNumber()
        {
            DataRow _claimDetails = null;
            if (!String.IsNullOrEmpty(txtPatientSearch.Text))
            {
                string[] _claim = txtPatientSearch.Text.Split('-');
                if (_claim.Length.Equals(2))
                {
                    if (Convert.ToString(_claim[1]).Trim() == "")
                    {
                        MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.SelectSearchBox();
                        return false;
                    }
                    else
                    {
                        if (Convert.ToString(_claim[0]).Trim() != string.Empty)
                        {
                            _claimDetails = GetClaimDetails(Convert.ToInt64(_claim[0]), Convert.ToString(_claim[1]).Trim());
                        }
                        if (_claimDetails == null)
                        {
                            MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        #endregion

        #region " C1 Grid Events (Insurance)"

        private void c1Insurance_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1Insurance_AfterEdit(object sender, RowColEventArgs e)
        {
            CheckEnum _Selected = CheckEnum.None;
            int _CurRowIndex = e.Row;

            InsuranceSelectedArgs args = new InsuranceSelectedArgs();
            if (c1Insurance.Rows.Count > 0)
            {
                _Selected = c1Insurance.GetCellCheck(_CurRowIndex, COL_SELECT);
                if (_Selected == CheckEnum.Checked)
                {
                    SelectedInsuranceParty = Convert.ToInt32(c1Insurance.GetData(e.Row, COL_PARTY));
                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        if (i != _CurRowIndex)
                        {
                            if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                            {
                                c1Insurance.SetCellCheck(i, COL_SELECT, CheckEnum.Unchecked);
                            }
                        }
                    }
                    args.InsuranceID = Convert.ToInt64(c1Insurance.GetData(_CurRowIndex, COL_INSURANCEID));
                    args.InsuraceSelfMode = Convert.ToInt32(c1Insurance.GetData(_CurRowIndex, COL_INSSELFMODE));
                    args.SelectedInsurancePlan = Convert.ToString(c1Insurance.GetData(_CurRowIndex, COL_INSURANCENAME));
                    args.ContactID = Convert.ToInt64(c1Insurance.GetData(_CurRowIndex, COL_CONTACTID));
                    args.IsSelectedPlanOnHold = PatientStripControl.IsInsurancePlanOnHold(args.ContactID);
                }
                else if (_Selected == CheckEnum.Unchecked)
                { SelectedInsuranceParty = 0; }

                if (OnInsuranceSelected != null)
                { OnInsuranceSelected(args); }
            }
            args = null;
        }

        #endregion

        #region " C1 Grid Events (Split Claim Search)"

        private void c1SplitClaims_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectClaim();
        }

        private void c1SplitClaims_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SelectClaim();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                IsSplitClaimSearchActive = false;
                SelectSearchBox();
            }
        }

        private void c1SplitClaims_MouseMove(object sender, MouseEventArgs e)
        {
            // gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            //Show Proper tooltip Mahesh Nawal
           gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            
        }

        private void SelectClaim()
        {
            IsSplitClaimSearchActive = false;
            txtPatientSearch.Text = Convert.ToString(c1SplitClaims.GetData(c1SplitClaims.RowSel, "ClaimDisplay"));
            SetClaimNumbers(false);
            //this.SelectSearchBox();
        }

        #endregion

        #region " Methods & Procedures "

        private void SetupControls()
        {
            // Hide patient gender panel 1
            pnlGender.Visible = false;
            // Hide patient home phone panel 2
            pnlPatientPhone.Visible = false;
            // Hide patient occupation panel 3
            pnlOccupation.Visible = false;
            // Hide patient pharmacy name 4
            pnlPharmacyName.Visible = false;
            // Hide Primary Insurance panel 5
            pnlPrInsurance.Visible = false;
            // Hide Secondary Insurance panel 6
            pnlSecInsurance.Visible = false;
            // Hide patient cell phone panel 7
            pnlCellPhone.Visible = false;
            // Hide referral physician panel 8
            pnlReferralPhysician.Visible = false;
            // Hide pharmacy phone panel 9
            pnlPharmacyPhone.Visible = false;
            // Hide Pharmacy Fax panel  10
            pnlPharmacyFax.Visible = false;
            // Hide Patient Hand Dominance panel  11
            pnlHandDominance.Visible = false;
            // Hide Patient SSN panel  12
            pnlSocialSecurity.Visible = false;

            this.ShowInsurances = true;
            this.ShowTotalBalance = true;
            this.ShowPatientClaimSearchForm = true;

            btn_ModityPatient.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Patient;
            btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;

            txtPatientSearch.Visible = true;

            chk_ClaimNoSearch.Visible = true;
            chk_ClaimNoSearch.Checked = false;
            chk_ClaimNoSearch.Enabled = true;
            lblSearchonClaimNo.Visible = false;

            lblTodaysDate.Text = dtpDate.Value.Date.ToString("ddd, MMM dd yyyy");
            c1PatientDetails.Visible = false;

            if (pnl_Main.Width > 0)
            {
                pnlLeft.Width = pnl_Main.Width / 2;
                pnlRight.Width = pnl_Main.Width - pnlLeft.Width - 2;
            }

            DesignInsuranceGrid();

            if (_ShowInsuraces == true && _ShowTotalBalance == false)
            { StripHeight = StripHeight + 35; }

            if (_ShowTotalBalance == true && _ShowInsuraces == false)
            { StripHeight = StripHeight + 45; }

            if (_ShowTotalBalance == true && _ShowInsuraces == true)
            { StripHeight = StripHeight + 45; }

            if (_ShowAlerts == true)
            { StripHeight = StripHeight + pnlAlerts.Height; }

            this.Height = StripHeight;

            gloC1FlexStyle.Style(c1SplitClaims, false);
        }

        public void FillDetails()
        {
            //gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(AppSettings.ConnectionStringPM);
            try
            {
                //if (oSecurity.isPatientLock(_PatientID, true) == true)
                //{
                    Int64 _patientID = PatientStripControl.GetPatientID(ClaimNumber, SubClaimNumber);
                    DataRow _drPatient = PatientStripControl.GetPatientDetails(_patientID);

                    if (_drPatient != null)
                    {
                        PatientID = Convert.ToInt64(_drPatient["nPatientID"]);
                        PatientCode = Convert.ToString(_drPatient["PatientCode"]);
                        PatientName = Convert.ToString(_drPatient["PatientName"]);
                        PatientDOB = Convert.ToDateTime(_drPatient["DOB"]);
                        PatientAge = PatientStripControl.FormatAge(PatientDOB);
                        ProviderName = Convert.ToString(_drPatient["PrName"]);

                        FillInsuranceParties();
                        FillPatientBalances();
                        
                    }
                
                //}
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillPatientBalances()
        {
            try
            {
                Int64 _patientID = PatientStripControl.GetPatientID(ClaimNumber, SubClaimNumber);
                DataRow _drPatientBalance = PatientStripControl.GetPatientBalances(_patientID);

                if (_drPatientBalance != null)
                {
                    TotalAvailableReserves = Convert.ToDecimal(_drPatientBalance["AvailableReserve"]);
                    InsuranceDue = Convert.ToDecimal(_drPatientBalance["InsuranceDue"]);
                    PatientDue = Convert.ToDecimal(_drPatientBalance["PatientDue"]) - TotalAvailableReserves;
                    TotalBalance = InsuranceDue + PatientDue;
                }

                decimal copay = 0;
                decimal advance = 0;
                decimal other = 0;

                DataTable _dtPatientBalance = PatientStripControl.GetPatientResereveBalances(_patientID,out copay,out advance,out other);

                CopayReserve = copay;
                AdvanceReserve = advance;
                OtherReserve = other;
                if (_dtPatientBalance != null)
                {
                    _dtPatientBalance.Dispose();
                    _dtPatientBalance = null;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void ClearDetails()
        {
            try
            {
                PatientID = 0;
                ClaimNumber = 0;
                SubClaimNumber = "";

                TotalBalance = 0;
                InsuranceDue = 0;
                PatientDue = 0;
                PatientName = "";
                CopayReserve = 0;
                AdvanceReserve = 0;
                OtherReserve = 0;

                lblPatientCode.Text = string.Empty;
                lblPatientName.Text = string.Empty;
                lblDOB.Text = string.Empty;
                lblAge.Text = string.Empty;
                lblProvider.Text = string.Empty;

                DesignInsuranceGrid();

                IsSplitClaimSearchActive = false;

                if (OnClearPatientDetails != null)
                { OnClearPatientDetails(); }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillInsuranceParties()
        {
            DataTable _dtInsuranceParties = null;
            try
            {
                DesignInsuranceGrid();
                c1Insurance.Rows.Count = 1;

                _dtInsuranceParties = PatientStripControl.GetInsuranceParties(ClaimNumber, PatientID);
                if (_dtInsuranceParties != null && _dtInsuranceParties.Rows.Count > 0)
                {
                    LoadInsuranceParties(_dtInsuranceParties);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_dtInsuranceParties != null) { _dtInsuranceParties.Dispose(); }
            }
        }

        private void LoadInsuranceParties(DataTable dtInsuranceParties)
        {
            int _ClaimInsCounter = 0;
            if (dtInsuranceParties != null && dtInsuranceParties.Rows.Count > 0)
            {
                int _responsiblityType = 0;
                int _selfPartyNo = dtInsuranceParties.Rows.Count;
                foreach (DataRow row in dtInsuranceParties.Rows)
                {
                    _responsiblityType = Convert.ToInt32(row["nResponsibilityType"]);

                    // ResponsibilityType
                    // 1 : Patient  
                    // 2 : Insurance 
                    if (_responsiblityType.Equals(2))
                    {
                        _ClaimInsCounter = _ClaimInsCounter + 1;

                        c1Insurance.Rows.Add();
                        int rowIndex = c1Insurance.Rows.Count - 1;
                        c1Insurance.SetData(rowIndex, COL_SELECT, false);//Select-CheckBox
                        c1Insurance.SetData(rowIndex, COL_INSURANCENAME, Convert.ToString(row["InsuranceName"])); //
                        c1Insurance.SetData(rowIndex, COL_INSURANCEID, Convert.ToString(row["nInsuranceID"])); //
                        c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, Convert.ToString(row["sInsuranceFlag"]));

                        c1Insurance.SetData(rowIndex, COL_INSURANCECOPAYAMT, Convert.ToDecimal(row["CoPay"]));
                        if (Convert.ToBoolean(row["bWorkersComp"]) == true)
                        { c1Insurance.SetData(rowIndex, COL_INSURANCEWORKERCOMP, true); }
                        if (Convert.ToBoolean(row["bAutoClaim"]) == true)
                        { c1Insurance.SetData(rowIndex, COL_INSURANCEAUTOCLAIM, "Auto Claim"); }
                        c1Insurance.SetData(rowIndex, COL_CONTACTID, Convert.ToString(row["nContactID"])); //
                        c1Insurance.SetData(rowIndex, COL_COPAY, Convert.ToDecimal(Convert.ToString(row["CoPay"])));
                        c1Insurance.SetData(rowIndex, COL_PARTY, Convert.ToInt64(row["nResponsibilityNo"]));
                        c1Insurance.SetData(rowIndex, COL_INSSELFMODE, Convert.ToInt32(row["nResponsibilityType"])); // None = 0,Self = 1,Insurance = 2

                        if (_ClaimInsCounter == 2) { _HasSecondaryInsOnClaim = true; }
                    }
                }

                // Add the Row for SELF 
                c1Insurance.Rows.Add();
                CellStyle csNonSelectCell;// = c1Insurance.Styles.Add("cs_NonSelectCell");
                try
                {
                    if (c1Insurance.Styles.Contains("cs_NonSelectCell"))
                    {
                        csNonSelectCell = c1Insurance.Styles["cs_NonSelectCell"];
                    }
                    else
                    {
                        csNonSelectCell = c1Insurance.Styles.Add("cs_NonSelectCell");

                    }

                }
                catch
                {
                    csNonSelectCell = c1Insurance.Styles.Add("cs_NonSelectCell");


                }
                csNonSelectCell.DataType = typeof(System.String);
                c1Insurance.SetCellStyle(c1Insurance.Rows.Count - 1, COL_SELECT, csNonSelectCell);
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_SELECT, null);//Select-CheckBox

                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCENAME, "Self"); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEID, 0); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSSELFMODE, "1"); // None = 0,Self = 1,Insurance = 2
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_PARTY, _selfPartyNo);
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_COPAY, "");
                c1Insurance.Rows[c1Insurance.Rows.Count - 1].AllowEditing = false;

                c1Insurance.Cols[COL_INSURANCETYPE].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCENAME].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCEID].AllowEditing = false;
                c1Insurance.Cols[COL_INSSELFMODE].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCECOPAYAMT].AllowEditing = false;

                SelectInsuranceParty();
            }
        }

        private void SelectInsuranceParty()
        {
            // Set Selected Insurance Party
            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (int rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (_selectedInsuranceParty != 0)
                    {
                        int _party = Convert.ToInt32(c1Insurance.GetData(rIndex, COL_PARTY));
                        if (_selectedInsuranceParty.Equals(_party))
                        {
                            InsuranceSelectedArgs args = new InsuranceSelectedArgs();
                            args.InsuranceID = Convert.ToInt64(c1Insurance.GetData(rIndex, COL_INSURANCEID));
                            args.InsuraceSelfMode = Convert.ToInt32(c1Insurance.GetData(rIndex, COL_INSSELFMODE));
                            args.SelectedInsurancePlan = Convert.ToString(c1Insurance.GetData(rIndex, COL_INSURANCENAME));
                            args.ContactID = Convert.ToInt64(c1Insurance.GetData(rIndex, COL_CONTACTID));
                            args.IsSelectedPlanOnHold = PatientStripControl.IsInsurancePlanOnHold(args.ContactID);

                            if (args.InsuraceSelfMode != 1)
                            { c1Insurance.SetData(rIndex, COL_SELECT, true); }

                            // Trigger the Insurance Selected event
                            OnInsuranceSelected(args);
                            args = null;
                            break;
                        }
                    }
                }
            }
        }

        private void SetClaimNumbers(bool AllowSearch)
        {
            ClaimNumber = 0;
            SubClaimNumber = string.Empty;
            IsSplitClaimSearchActive = false;

            if (!String.IsNullOrEmpty(txtPatientSearch.Text))
            {
                string[] _claim = txtPatientSearch.Text.Split('-');

                if (_claim.Length.Equals(2))
                {
                    _ClaimNumber = Convert.ToInt64(_claim[0]);
                    _SubClaimNumber = Convert.ToString(_claim[1]);
                    // IF SubClaim Number exist, skip the claim search 
                    AllowSearch = false;
                }
                else if (_claim.Length.Equals(1))
                { _ClaimNumber = Convert.ToInt64(_claim[0]); }

                if (AllowSearch)
                { SearchClaims(); }
            }
                if (!IsSplitClaimSearchActive)
                { OnClaimNumberEntered(txtPatientSearch.Text); }
            //}
        }

        private void SearchClaims()
        {
            DataTable _dtSplittedClaims = null;
            try
            {
                _dtSplittedClaims = PatientStripControl.GetSplittedClaims(_ClaimNumber);
                if (_dtSplittedClaims != null)
                {
                    if (_dtSplittedClaims.Rows.Count > 1)
                    {
                        IsSplitClaimSearchActive = true;
                        c1SplitClaims.DataSource = _dtSplittedClaims;
                        SetupClaimSearchGrid();
                    }
                    else
                    { IsSplitClaimSearchActive = false; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public string GetNextParty()
        {
            string _nextParty = "";

            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (int rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (c1Insurance.GetCellCheck(rIndex, COL_SELECT) == CheckEnum.Checked)
                    {
                        if (Convert.ToInt32(c1Insurance.GetData(rIndex, COL_INSSELFMODE)) == 1)
                        {
                            _nextParty = "0" + "-" + "Self" + "|";
                            break;
                        }
                        else if (rIndex + 1 < c1Insurance.Rows.Count)
                        {
                            //_nextParty = Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_PARTY));
                            if (Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_PARTY)).Trim().Split('-')[0].ToUpper() == "0")
                            { _nextParty = "0" + "-" + "Self" + "|"; }
                            else
                            { _nextParty = Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_PARTY)).Trim().ToUpper() + "-" + Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_INSURANCENAME)).Trim().ToUpper() + "|"; }
                            break;
                        }
                    }
                }
            }
            _nextParty = _nextParty.TrimEnd('|');
            return _nextParty;
        }

        public string GetCurrentParty()
        {
            string _nextParty = "";

            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (int rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (c1Insurance.GetCellCheck(rIndex, COL_SELECT) == CheckEnum.Checked)
                    {
                        if (Convert.ToInt32(c1Insurance.GetData(rIndex, COL_INSSELFMODE)) == 1)
                        {
                            _nextParty = "0" + "-" + "Self" + "|";
                            break;
                        }
                        else if (rIndex < c1Insurance.Rows.Count)
                        {
                            //_nextParty = Convert.ToString(c1Insurance.GetData(rIndex + 1, COL_PARTY));
                            if (Convert.ToString(c1Insurance.GetData(rIndex, COL_PARTY)).Trim().Split('-')[0].ToUpper() == "0")
                            { _nextParty = "0" + "-" + "Self" + "|"; }
                            else
                            { _nextParty = Convert.ToString(c1Insurance.GetData(rIndex, COL_PARTY)).Trim().ToUpper() + "-" + Convert.ToString(c1Insurance.GetData(rIndex, COL_INSURANCENAME)).Trim().ToUpper() + "|"; }
                            break;
                        }
                    }
                }
            }
            _nextParty = _nextParty.TrimEnd('|');
            return _nextParty;
        }

        public int GetSelfPartyNo()
        {
            int _selfCode = 0;

            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (int rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (Convert.ToInt32(c1Insurance.GetData(rIndex, COL_INSSELFMODE)) == 1)
                    {
                        _selfCode = rIndex;
                        break;
                    }
                }
            }
            return _selfCode;
        }

        public int GetSelectedPartyResponsibility()
        {
            int _Party = 0;

            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (int rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                {
                    if (c1Insurance.GetCellCheck(rIndex, COL_SELECT) == CheckEnum.Checked)
                    {
                        _Party = rIndex;
                        break;
                    }
                }
            }
            return _Party;
        }

        public void SelectSearchBox()
        {
            this.Select();
            this.Focus();
            txtPatientSearch.Select(); txtPatientSearch.Focus(); txtPatientSearch.SelectAll();
        }

        public static DataRow GetClaimDetails(Int64 ClaimNumber, string SubClaimNumber)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = string.Empty;
            string _subClaimNumber = string.Empty;

            DataTable _dtClaimDetails = null;
            DataRow _claimDetails = null;

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nClaimno", ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sSubClaimno", SubClaimNumber, ParameterDirection.Input, SqlDbType.VarChar, 50);

                oDB.Retrive("BL_Select_SplitClaims", oParameters, out _dtClaimDetails);
                oDB.Disconnect();

                if (_dtClaimDetails != null && _dtClaimDetails.Rows.Count > 0)
                {
                    _claimDetails = _dtClaimDetails.Rows[0];
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_dtClaimDetails != null) { _dtClaimDetails.Dispose(); }
            }
            return _claimDetails;
        }

        #endregion

        #region " C1 Grid Design Methods "

        private void SetupClaimSearchGrid()
        {
            #region " Hide Columns "

            c1SplitClaims.Cols["ClaimNo"].Visible = false;
            c1SplitClaims.Cols["SubClaimNo"].Visible = false;
            c1SplitClaims.Cols["ClaimNo"].Visible = false;
            c1SplitClaims.Cols["Facility"].Visible = false;
            c1SplitClaims.Cols["FacilityCode"].Visible = false;
            c1SplitClaims.Cols["PatientID"].Visible = false;

            c1SplitClaims.Cols["ProviderFName"].Visible = false;
            c1SplitClaims.Cols["ProviderMName"].Visible = false;
            c1SplitClaims.Cols["ProviderLName"].Visible = false;

            c1SplitClaims.Cols["TransactionMasterID"].Visible = false;
            c1SplitClaims.Cols["TransactionID"].Visible = false;
            c1SplitClaims.Cols["ContactID"].Visible = false;
            c1SplitClaims.Cols["ClinicID"].Visible = false;
            c1SplitClaims.Cols["nResponsibilityNo"].Visible = false;
            c1SplitClaims.Cols["nParentTransactionID"].Visible = false;


            #endregion

            #region " Set Captions "

            c1SplitClaims.Cols["ClaimDisplay"].Caption = "Claim #";
            c1SplitClaims.Cols["PatientCode"].Caption = "Code";

            c1SplitClaims.Cols["PatientFName"].Caption = "Patient FN";
            c1SplitClaims.Cols["PatientMName"].Caption = "Patient MI";
            c1SplitClaims.Cols["PatientLName"].Caption = "Patient LN";

            c1SplitClaims.Cols["ProviderFName"].Caption = "Provider FN";
            c1SplitClaims.Cols["ProviderMName"].Caption = "Provider MI";
            c1SplitClaims.Cols["ProviderLName"].Caption = "Provider LN";

            c1SplitClaims.Cols["InsuranceID"].Caption = "Insurance ID";
            c1SplitClaims.Cols["Insurance"].Caption = "Insurance Plan";
            
            #endregion

            c1SplitClaims.Cols["Date"].Format = "MM/dd/yyyy";
            c1SplitClaims.Focus();
            c1SplitClaims.Select(1, 0);
        }

        private void DesignInsuranceGrid()
        {
            c1Insurance.AllowSorting = AllowSortingEnum.None;
            c1Insurance.Cols.Count = COL_INSVIEW_COUNT;
            c1Insurance.Rows.Count = 1;
            c1Insurance.SetData(0, COL_SELECT, "Sel.");
            c1Insurance.SetData(0, COL_PARTY, "Party");
            c1Insurance.SetData(0, COL_INSURANCEID, "InsuranceID");
            c1Insurance.SetData(0, COL_INSURANCENAME, "Insurance");
            c1Insurance.SetData(0, COL_INSURANCETYPE, "Type");
            c1Insurance.SetData(0, COL_INSSELFMODE, "Mode");
            c1Insurance.SetData(0, COL_INSURANCECOPAYAMT, "CopayAmt");

            c1Insurance.SetData(0, COL_INSURANCEWORKERCOMP, "Workers Comp");
            c1Insurance.SetData(0, COL_INSURANCEAUTOCLAIM, "Auto Claim");
            c1Insurance.SetData(0, COL_CONTACTID, "Contact ID");
            c1Insurance.SetData(0, COL_COPAY, "Copay");

            c1Insurance.Cols[COL_INSURANCEWORKERCOMP].DataType = typeof(System.Boolean);
            c1Insurance.Cols[COL_COPAY].DataType = typeof(System.Decimal);
            c1Insurance.Cols[COL_COPAY].Format = "c";
            c1Insurance.Cols[COL_COPAY].AllowEditing = false;

            c1Insurance.Cols[COL_PARTY].DataType = typeof(System.String);

            int nWidth;
            nWidth = pnlInsurace.Width;

            c1Insurance.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");//Select Column
            c1Insurance.Cols[COL_SELECT].AllowEditing = AllowEditingParty;  //true;
            c1Insurance.Cols[COL_INSURANCEWORKERCOMP].AllowEditing = false;
            c1Insurance.Cols[COL_PARTY].AllowEditing = false;
            

            c1Insurance.Cols[COL_INSURANCEID].Visible = false;
            c1Insurance.Cols[COL_INSSELFMODE].Visible = false;
            c1Insurance.Cols[COL_INSURANCECOPAYAMT].Visible = false;
            c1Insurance.Cols[COL_INSURANCEAUTOCLAIM].Visible = false;
            c1Insurance.Cols[COL_INSURANCETYPE].Visible = false;
            c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Visible = false;
            c1Insurance.Cols[COL_CONTACTID].Visible = false;

            c1Insurance.Cols[COL_SELECT].Width = 35;
            c1Insurance.Cols[COL_PARTY].Width = 40;
            c1Insurance.Cols[COL_INSURANCENAME].Width = Convert.ToInt32(nWidth - 185);
            c1Insurance.Cols[COL_INSURANCEID].Width = 0;
            c1Insurance.Cols[COL_INSURANCETYPE].Width = 0;
            c1Insurance.Cols[COL_INSSELFMODE].Width = 0;
            c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Width = 0;
            c1Insurance.Cols[COL_INSURANCEAUTOCLAIM].Width = 0;

            c1Insurance.Cols[COL_INSURANCENAME].TextAlign = TextAlignEnum.LeftCenter; 

            gloC1FlexStyle.Style(c1Insurance, false);
        }

        #endregion

        private void ucPatientStripControl_Leave(object sender, EventArgs e)
        {
            IsSplitClaimSearchActive = false;
        }
    }
}
