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
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmPaymentReserveRemaningV2 : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        bool IsProviderEnable = false;
        bool IsDefaultPatientProviderEnable = false;
        public decimal ReserveAmount = 0;
        public string ReserveNote = "";
        public NoteSubTypeV2 ReserveSubType = NoteSubTypeV2.None;
        public bool ReserveNoteOnPrint = false;

        private Int64 _eobPaymentId = 0;
        private Int64 _eobPaymentDetailId = 0;
        private bool _isOpenModify = false;
        private Int64 _patientId = 0;
        private Int64 _PAccountID = 0;
        private Int64 _nProviderID = 0;
        string _sProviderName = "";
        
        private Int64 _nCollectionAgencyContactID = 0;
        string _sCollectionAgencyrName = "";

        private bool _isProviderMandatory = false;
        private bool _isDayClosed = false;
        private bool _isInsuranceReserve = false;

        gloGridListControl ogloGridListControl = null;
        private Boolean bCPTAssigned = true;
        private Boolean bICDAssigned = true;
        public gloGeneralItem.gloItems oCPTList = new gloGeneralItem.gloItems();

        public gloGeneralItem.gloItems oICD9List = new gloGeneralItem.gloItems();

        public bool _IsValidate = true;


        #region "Reserve DOS Varaiables"

        public DateTime _dtReserveForDOS = DateTime.MinValue;
        public bool _IsReservedForDOSValidated = true;

        #endregion

        #endregion " Declarations "

        #region " Property Procedures "

        public gloGeneralItem.gloItems CPTList
        {
            get { return oCPTList; }
            set
            {
                if (bCPTAssigned)
                {
                    if (oCPTList != null)
                    {
                        oCPTList.Clear();
                        oCPTList.Dispose();
                        oCPTList = null;
                    }
                    bCPTAssigned = false;
                }
                oCPTList = value;
            }
        }

        public gloGeneralItem.gloItems ICD9List
        {
            get { return oICD9List; }
            set
            {
                if (bICDAssigned)
                {
                    if (oICD9List != null)
                    {
                        oICD9List.Clear();
                        oICD9List.Dispose();
                        oICD9List = null;
                    }
                    bICDAssigned = false;
                }
                oICD9List = value;
            }
        }

        public bool IsInsuranceReserve
        {
            get { return _isInsuranceReserve; }
            set { _isInsuranceReserve = value; }
        }
        /// <summary>
        /// Reserve Amount which sent from the Payment Form 
        /// </summary>
        decimal _ReserveAllowed = 0;
        public decimal ReserveAllowed
        {
            get { return _ReserveAllowed; }
            set { _ReserveAllowed = value; }
        }
        //Code added by subashish 14/05/2011 for passing the PAccount id 
        public Int64 PAccountID
        {
            get { return _PAccountID; }
            set { _PAccountID = value; }
        }

        public Int64 ProviderID
        {
            get { return _nProviderID; }
            set { _nProviderID = value; }
        }

        public string ProviderName
        {
            get { return _sProviderName; }
            set { _sProviderName = value; }
        }

        public Int64 CollectionAgencyContactID
        {
            get { return _nCollectionAgencyContactID; }
            set { _nCollectionAgencyContactID = value; }
        }

        public string CollectionAgencyName
        {
            get { return _sCollectionAgencyrName; }
            set { _sCollectionAgencyrName = value; }
        }

        //End
        decimal _ReserveAmountFromUR = 0;
        public decimal ReserveAmountFromUR
        {
            get { return _ReserveAmountFromUR; }
            set { _ReserveAmountFromUR = value; }
        }

        public bool IsProviderMandatory
        {
            get { return _isProviderMandatory; }
            set { _isProviderMandatory = value; }
        }

        public bool IsDayClosed
        {
            get { return _isDayClosed; }
            set { _isDayClosed = value; }
        }

        public DateTime dtReserveForDOS
        {
            get { return _dtReserveForDOS; }
            set { _dtReserveForDOS = value; }
        }

       

        #endregion

        #region " Column Constant "

        const int COL_CPT_CODE = 0;
        const int COL_CPT_DESC = 1;
        const int COL_CPT_COUNT = 2;

        const int COL_DX_CODE = 0;
        const int COL_DX_DESC = 1;
        const int COL_DX_COUNT = 2;

        #endregion

        #region " Constructor "

        public frmPaymentReserveRemaningV2(string DatabaseConnectionString, Int64 PatientId)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            InitializeComponent();
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            _patientId = PatientId;
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
        }

        public frmPaymentReserveRemaningV2(string DatabaseConnectionString, Int64 PatientId, Int64 EOBPaymentId, Int64 EOBPaymentDetailId, bool IsOpenModify)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _eobPaymentId = EOBPaymentId;
            _eobPaymentDetailId = EOBPaymentDetailId;
            _isOpenModify = IsOpenModify;
            _patientId = PatientId;

            InitializeComponent();

            #region " Retrive ClinicID "

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
        }

        #endregion " Constructor "

        private void frmPaymentReserveRemaning_Load(object sender, EventArgs e)
        {   // line added for selection of default icd 9 Sameer 02172014
         
            ShowHideDiagnosis();
            DesignCPTGrid();
            DesignDxGrid();
            txtAmount.Tag = null;
            IsProviderEnable = gloAccountsV2.gloBillingCommonV2.IsPatientReserve_ProviderEnable();
            //if (IsDayClosed) { btnSearchProvider.Enabled = false; btnClearProvider.Enabled = false; }
            IsDefaultPatientProviderEnable = gloAccountsV2.gloBillingCommonV2.IsDefaultPatient_ProviderEnable();          
            //if (IsProviderEnable)
            //{
            //    pnlEnableProvider.Visible = true;
            //}
            //else
            //{
            //    pnlEnableProvider.Visible = false;
            //}
            if (_isOpenModify == false)
            {
                rbRecType_Copay.Visible = true;
                rbRecType_Advance.Visible = true;
                rbRecType_Other.Visible = true;

                txtAmount.Text = ReserveAmount.ToString("#0.00");
                txtAmount.Tag = ReserveAmount;
                txtProvider.Tag = ProviderID;
                txtProvider.Text = ProviderName;
                

                if (IsDefaultPatientProviderEnable && ProviderID == 0)
                {

                    DataTable _dtDefaultPatientProvider = new DataTable();
                    _dtDefaultPatientProvider = gloAccountsV2.gloBillingCommonV2.GetDefaultPatientProvider(_patientId);
                    if (_dtDefaultPatientProvider != null && _dtDefaultPatientProvider.Rows.Count > 0)
                    {
                        txtProvider.Text = Convert.ToString(_dtDefaultPatientProvider.Rows[0]["ProviderName"]);
                        txtProvider.Tag = Convert.ToInt64(_dtDefaultPatientProvider.Rows[0]["nProviderID"]);
                    }

                }
                // Reserve Remaining Amount will go as Allowed
                // So user will not be allowed to enter more than this amount

                //...........................................
                //_ReserveAllowed = ReserveAmount;
                //...........................................

                //txtAmount.ReadOnly = true;
                //txtAmount.BackColor = Color.White;

                if (ReserveSubType == NoteSubTypeV2.Copay)
                {
                    rbRecType_Copay.Checked = true;
                }
                else if (ReserveSubType == NoteSubTypeV2.Advance)
                {
                    rbRecType_Advance.Checked = true;
                }
                else if (ReserveSubType == NoteSubTypeV2.Other)
                {
                    rbRecType_Other.Checked = true;
                }
                txtNotes.Text = ReserveNote.ToString();
                chkIncludeNotes.Checked = ReserveNoteOnPrint;
                if (gloGlobal.gloPMGlobal.CurrentICDRevision == gloGlobal.gloICD.CodeRevision.ICD10)
                {
                    rbICD10.Checked = true;
                    rbICD9.Checked = false;
                }
                else if (gloGlobal.gloPMGlobal.CurrentICDRevision == gloGlobal.gloICD.CodeRevision.ICD9)
                {
                    rbICD10.Checked = false;
                    rbICD9.Checked = true;
                }
                if (dtReserveForDOS != DateTime.MinValue)
                {
                    mskReserveForDOS.Text = dtReserveForDOS.ToString("MM/dd/yyyy");
                }
                collectionAgencyAssociationVisibility();
            }
            else
            { OpenModify(); }

            if (rbRecType_Advance.Checked)
            {
                string _code = "";
                string _desc = "";
                if (oCPTList != null && oCPTList.Count > 0)
                {
                    for (int i = 0; i < oCPTList.Count; i++)
                    {
                        _code = oCPTList[i].Code.ToString();
                        _desc = oCPTList[i].Description.ToString();
                        if (_code.Trim() != "")
                        {
                            AddCPTToList(_code, _desc);
                        }
                    }
                }
                if (oICD9List != null && oICD9List.Count > 0)
                {
                    for (int i = 0; i < oICD9List.Count; i++)
                    {
                        _code = oICD9List[i].Code.ToString();
                        _desc = oICD9List[i].Description.ToString();
                        if (_code.Trim() != "")
                        {
                            AddDxToList(_code, _desc);
                        }
                    }
                }
            }
            if (txtProvider.Tag != null && Convert.ToInt64(txtProvider.Tag) > 0)
            {
                if (IsProviderMandatory) { btnClearProvider.Enabled = false; }
            }

            //if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
            //{
            //    gloContacts.clsCollectionAgency oCollection = null;
            //    DataTable dtCollectionAgency = null;
            //    this.Width = 539;
            //    this.Height = 302;
            //    pnlCollectionAgency.Visible = true;
            //    dtCollectionAgency = oCollection.GetCollectionAgency(0, false);
            //}
            //else
            //{

            //    this.Width = 539;
            //    this.Height = 275;
            //    pnlMain.Height = 193;
            //    pnlCollectionAgency.Visible = false;
            //}
            

        }

        private void tlsbtnSAVEnCLOSE_Click(object sender, EventArgs e)
        {
            try
            {
                //ICDValidation();
                //if (!_IsValidate)
                //{
                //    return;
                //}
                pnltlsStrip.Select();
                //if (IsReserveDOSAvailable || (IsReserveDOSAvailable == false && Convert.ToString(mskReserveForDOS.Text.Trim()) == "/  /"))
                //{
                if (Convert.ToString(mskReserveForDOS.Text.Trim()) == null || Convert.ToString(mskReserveForDOS.Text.Trim()) == "/  /")
                {
                    if (DialogResult.No == (MessageBox.Show("Reserve 'For DOS' is not entered.Do you want Continue ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)))
                    {
                        mskReserveForDOS.Focus();
                        return;
                    }
                }


                if (SaveValidation() == true && _IsReservedForDOSValidated == true)
                {
                    if (_isInsuranceReserve == false)
                    {
                        #region " Patient Reserve "

                        if (_isOpenModify == false)
                        {
                            if (txtAmount.Text.Trim() != "" && Convert.ToDecimal(txtAmount.Text.ToString()) >= 0)
                            {
                                //if (txtNotes.Text.Trim().Length > 0)
                                {
                                    if (IsProviderEnable)
                                    {
                                        if (Convert.ToString(txtProvider.Text.ToString()) == "" && btnSearchProvider.Enabled == true)
                                        {
                                            MessageBox.Show("Please select provider.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            btnSearchProvider.Focus();
                                            return;
                                        }
                                    }
                                    if (txtAmount.Text.Trim() != "")
                                    {
                                        ReserveAmount = Convert.ToDecimal(txtAmount.Text.ToString());
                                    }
                                    if (rbRecType_Copay.Checked == true)
                                    {
                                        ReserveSubType = NoteSubTypeV2.Copay;
                                    }
                                    else if (rbRecType_Advance.Checked == true)
                                    {
                                        ReserveSubType = NoteSubTypeV2.Advance;
                                    }
                                    else
                                    {
                                        ReserveSubType = NoteSubTypeV2.Other;
                                    }
                                    ReserveNote = txtNotes.Text.Trim().Replace("'", "''");
                                    ReserveNoteOnPrint = chkIncludeNotes.Checked;
                                    oCPTList.Clear();
                                    oICD9List.Clear();
                                    if (rbRecType_Advance.Checked)
                                    {
                                        string _code = "";
                                        string _desc = "";
                                        if (c1CPTList != null && c1CPTList.Rows.Count > 1)
                                        {
                                            for (int i = 1; i < c1CPTList.Rows.Count; i++)
                                            {
                                                _code = Convert.ToString(c1CPTList.GetData(i, COL_CPT_CODE));
                                                _desc = Convert.ToString(c1CPTList.GetData(i, COL_CPT_DESC));

                                                oCPTList.Add(0, _code, _desc);
                                            }
                                        }
                                        if (c1Dx != null && c1Dx.Rows.Count > 1)
                                        {
                                            for (int i = 1; i < c1Dx.Rows.Count; i++)
                                            {
                                                _code = Convert.ToString(c1Dx.GetData(i, COL_DX_CODE));
                                                _desc = Convert.ToString(c1Dx.GetData(i, COL_DX_DESC));

                                                oICD9List.Add(0, _code, _desc);
                                            }
                                        }
                                    }
                                    ProviderID = Convert.ToInt64(txtProvider.Tag);
                                    ProviderName = txtProvider.Text;
                                    CollectionAgencyContactID = Convert.ToInt64(cmbCollectionAgency.SelectedValue);
                                    CollectionAgencyName = Convert.ToString(cmbCollectionAgency.Text);

                                    if (Convert.ToString(mskReserveForDOS.Text.Trim()) != null && Convert.ToString(mskReserveForDOS.Text.Trim()) != "/  /")
                                    {
                                        mskReserveForDOS.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                        dtReserveForDOS = Convert.ToDateTime(Convert.ToString(mskReserveForDOS.Text));
                                    }

                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                                //else
                                //{
                                //    MessageBox.Show("Please enter the notes.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    txtNotes.Select(); txtNotes.Focus();
                                //    return;
                                //}
                            }
                            else
                            {
                                MessageBox.Show("Reserve amount cannot be negative or empty.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAmount.Select(); txtAmount.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (UpdateReserve()) { this.DialogResult = DialogResult.OK; }
                            this.Close();
                        }

                        #endregion " Patient Reserve "
                    }
                    else
                    {
                        #region " Patient Reserve "

                        if (_isOpenModify == false)
                        {
                            if (txtAmount.Text.Trim() != "" && Convert.ToDecimal(txtAmount.Text.ToString()) >= 0)
                            {
                                //if (txtNotes.Text.Trim().Length > 0)
                                {
                                    if (txtAmount.Text.Trim() != "")
                                    {
                                        ReserveAmount = Convert.ToDecimal(txtAmount.Text.ToString());
                                    }

                                    ReserveSubType = NoteSubTypeV2.MasterPayment;
                                    ReserveNote = txtNotes.Text.Trim().Replace("'", "''");
                                    ReserveNoteOnPrint = chkIncludeNotes.Checked;
                                    oCPTList.Clear();
                                    oICD9List.Clear();
                                    if (rbRecType_Advance.Checked)
                                    {
                                        string _code = "";
                                        string _desc = "";
                                        if (c1CPTList != null && c1CPTList.Rows.Count > 1)
                                        {
                                            for (int i = 1; i < c1CPTList.Rows.Count; i++)
                                            {
                                                _code = Convert.ToString(c1CPTList.GetData(i, COL_CPT_CODE));
                                                _desc = Convert.ToString(c1CPTList.GetData(i, COL_CPT_DESC));

                                                oCPTList.Add(0, _code, _desc);
                                            }
                                        }
                                        if (c1Dx != null && c1Dx.Rows.Count > 1)
                                        {
                                            for (int i = 1; i < c1Dx.Rows.Count; i++)
                                            {
                                                _code = Convert.ToString(c1Dx.GetData(i, COL_DX_CODE));
                                                _desc = Convert.ToString(c1Dx.GetData(i, COL_DX_DESC));

                                                oICD9List.Add(0, _code, _desc);
                                            }
                                        }
                                    }

                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Reserve amount cannot be negative or empty.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAmount.Select(); txtAmount.Focus();
                                return;
                            }
                        }
                        else
                        {
                            //...Need to write code for update Insurance reserve
                            //if (UpdateReserve()) { this.DialogResult = DialogResult.OK; }
                            this.Close();
                        }

                        #endregion " Patient Reserve "
                    }
                }





                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        private bool SaveValidation()
        {
            bool _retValue = true;
            decimal _CheckAmount = 0;

            try
            {
                if (_isOpenModify == true) { return true; }

                _CheckAmount = Convert.ToDecimal(txtAmount.Text);

                if (_CheckAmount < 0) // Reserve Amount should not be zero.
                {
                    MessageBox.Show("Reserve amount is required.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _retValue = false;

                    txtAmount.Text = _CheckAmount.ToString("#0.00");
                }
                else if (_CheckAmount > _ReserveAllowed)   // Reserve amount should not be greater than reserve allowed.
                {
                    MessageBox.Show("Cannot reserve more funds than remaining funds", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _retValue = false;

                    txtAmount.Text = _ReserveAllowed.ToString("#0.00");
                    //txtAmount.Text = "0.00";
                }
            }
            catch // Reserve Amount should not be invalid / blank
            {
                MessageBox.Show("Please enter a valid amount.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _retValue = false;
            }
            return _retValue;
        }

        private void tlsbtnCLOSE_Click(object sender, EventArgs e)
        {
            oCPTList.Clear();
            oICD9List.Clear();
            this.Close();
        }

        private void frmPaymentReserveRemaning_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK) { this.DialogResult = DialogResult.Cancel; }
        }

        private void OpenModify()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtReserves = new DataTable();
            string _IcdCode = string.Empty;
            try
            {
                if (_eobPaymentId > 0 && _patientId > 0)
                {
                    oParameters.Add("@nCreditID", _eobPaymentId, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                    oParameters.Add("@nReserveID", _eobPaymentDetailId, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),

                    oDB.Connect(false);
                    oDB.Retrive("BL_View_PatientReserveDetails_V2", oParameters, out _dtReserves);
                    oDB.Disconnect();

                    if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dtReserves.Rows.Count; i++)
                        {
                            if (_dtReserves.Rows[i]["nPaymentNoteSubType"] != null && Convert.ToString(_dtReserves.Rows[i]["nPaymentNoteSubType"]).Trim() != ""
                                && Convert.ToInt16(_dtReserves.Rows[i]["nPaymentNoteSubType"]) != 0)
                            {
                                //txtAmount.Text = Convert.ToString(_dtReserves.Rows[i]["AvailableReserve"]);
                                txtAmount.Text = Convert.ToString(_ReserveAmountFromUR);
                                if (Convert.ToString(_dtReserves.Rows[i]["sNoteCode"]) != "CPT" && Convert.ToString(_dtReserves.Rows[i]["sNoteCode"]) != "ICD9")
                                {
                                    txtNotes.Text = Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]);
                                }
                                if (((NoteSubTypeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])) == NoteSubTypeV2.Copay)
                                { rbRecType_Copay.Checked = true; }
                                else if (((NoteSubTypeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])) == NoteSubTypeV2.Advance)
                                { rbRecType_Advance.Checked = true; }
                                else if (((NoteSubTypeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])) == NoteSubTypeV2.Other)
                                { rbRecType_Other.Checked = true; }
                                chkIncludeNotes.Checked = false;

                                txtAmount.Enabled = false;
                                //txtNotes.Enabled = false;
                            }
                            if (Convert.ToString(_dtReserves.Rows[i]["sNoteCode"]).Trim() != "")
                            {
                                if (Convert.ToString(_dtReserves.Rows[i]["sNoteCode"]).Trim().ToUpper() == "CPT" && (NoteSubTypeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"]) == NoteSubTypeV2.Advance)
                                {
                                    string _cptCodes = "";
                                    _cptCodes = Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]).Trim();

                                    if (_cptCodes != "")
                                    {
                                        string[] _arrcptCodes = _cptCodes.Split('|');
                                        if (_arrcptCodes.Length > 0)
                                        {
                                            for (int arrIndex = 0; arrIndex < _arrcptCodes.Length; arrIndex++)
                                            {
                                                if (_arrcptCodes[arrIndex].Trim() != "")
                                                {
                                                    AddCPTToList(_arrcptCodes[arrIndex].Split('~')[0], _arrcptCodes[arrIndex].Split('~')[1]);
                                                    oCPTList.Add(0, _arrcptCodes[arrIndex].Split('~')[0], _arrcptCodes[arrIndex].Split('~')[1]);
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (Convert.ToString(_dtReserves.Rows[i]["sNoteCode"]).Trim().ToUpper() == "ICD9" && (NoteSubTypeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"]) == NoteSubTypeV2.Advance)
                                {
                                    string _icd9Codes = "";
                                    _icd9Codes = Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]).Trim();


                                    if (_icd9Codes != "")
                                    {
                                        string[] _arricd9Codes = _icd9Codes.Split('|');

                                        if (_arricd9Codes.Length > 0)
                                        {
                                            _IcdCode = _arricd9Codes[0].Split('~')[0];
                                            for (int arrIndex = 0; arrIndex < _arricd9Codes.Length; arrIndex++)
                                            {
                                                if (_arricd9Codes[arrIndex].Trim() != "")
                                                {
                                                    AddDxToList(_arricd9Codes[arrIndex].Split('~')[0], _arricd9Codes[arrIndex].Split('~')[1]);
                                                    oICD9List.Add(0, _arricd9Codes[arrIndex].Split('~')[0], _arricd9Codes[arrIndex].Split('~')[1]);
                                                }
                                            }
                                        }


                                        gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, _databaseconnectionstring);
                                        if (ogloBilling.CheckDxStatus(_IcdCode, gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode()) == 1)
                                        {
                                            rbICD9.Checked = true;
                                        }
                                        else
                                        {
                                            rbICD10.Checked = true;
                                        }
                                        if (ogloBilling != null)
                                        {
                                            ogloBilling.Dispose();
                                            ogloBilling = null;
                                        }
                                    }
                                }
                            }
                            if (_dtReserves.Rows[i]["AssociationProviderID"] != null && Convert.ToString(_dtReserves.Rows[i]["AssociationProviderID"]).Trim() != ""
                                && Convert.ToInt64(_dtReserves.Rows[i]["AssociationProviderID"]) != 0)
                            {
                                txtProvider.Tag = Convert.ToInt64(_dtReserves.Rows[i]["AssociationProviderID"]);
                                txtProvider.Text = Convert.ToString(_dtReserves.Rows[i]["AssociationProvider"]);
                            }

                            collectionAgencyAssociationVisibility();
                            cmbCollectionAgency.SelectedValue = Convert.ToInt64(_dtReserves.Rows[i]["AssociationCollectionAgencyContactID"]);

                            if (Convert.ToString(_dtReserves.Rows[i]["dtReserveForDOS"]) != null && Convert.ToString(_dtReserves.Rows[i]["dtReserveForDOS"]) != "")
                            {
                                _dtReserveForDOS = Convert.ToDateTime(_dtReserves.Rows[i]["dtReserveForDOS"]);
                                mskReserveForDOS.Text = dtReserveForDOS.ToString("MM/dd/yyyy");                  
                            }
                       

                            //if (IsProviderEnable)
                            //{
                            //    txtProvider.Enabled = false;
                            //    btnSearchProvider.Enabled = false;
                            //    btnClearProvider.Enabled = false;
                            //}
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_dtReserves != null) { _dtReserves.Dispose(); }
            }
        }

        private bool UpdateReserve()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            bool _retValue = false;

            try
            {

                if (rbRecType_Copay.Checked == true)
                { ReserveSubType = NoteSubTypeV2.Copay; }
                else if (rbRecType_Advance.Checked == true)
                { ReserveSubType = NoteSubTypeV2.Advance; }
                else
                { ReserveSubType = NoteSubTypeV2.Other; }

                string _code = "";
                string _desc = "";
                if (oCPTList != null) { oCPTList.Clear(); }
                if (oICD9List != null) { oICD9List.Clear(); }

                if (c1CPTList != null && c1CPTList.Rows.Count > 1)
                {
                    for (int i = 1; i < c1CPTList.Rows.Count; i++)
                    {
                        _code = Convert.ToString(c1CPTList.GetData(i, COL_CPT_CODE));
                        _desc = Convert.ToString(c1CPTList.GetData(i, COL_CPT_DESC));

                        oCPTList.Add(0, _code, _desc);
                    }
                }
                if (c1Dx != null && c1Dx.Rows.Count > 1)
                {
                    for (int i = 1; i < c1Dx.Rows.Count; i++)
                    {
                        _code = Convert.ToString(c1Dx.GetData(i, COL_DX_CODE));
                        _desc = Convert.ToString(c1Dx.GetData(i, COL_DX_DESC));

                        oICD9List.Add(0, _code, _desc);
                    }
                }

                string _strCPTList = "";
                string _strICD9List = "";

                _strCPTList = GetListString(oCPTList);
                _strICD9List = GetListString(oICD9List);


                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object _result = null;
                oParameters.Clear();
                oDB.Connect(false);

                oParameters.Add("@ReserveSubType", ReserveSubType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@NoteDescription", txtNotes.Text.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@EOBPaymentId", _eobPaymentId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@EOBPaymentDetailID", _eobPaymentDetailId, ParameterDirection.Input, SqlDbType.BigInt);
                //if (Convert.ToString(mskReserveForDOS.Text.Trim()) != null && Convert.ToString(mskReserveForDOS.Text.Trim()) != "/  /")
                if (dtReserveForDOS != DateTime.MinValue) 
                {
                    oParameters.Add("@dtReserveForDos", dtReserveForDOS, ParameterDirection.Input, SqlDbType.Date);
                }
                oDB.Execute("BL_Update_PatientReserveDetails_V2", oParameters, out _result);
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                    oDB.Disconnect();
                }
                int _val = Convert.ToInt16(_result);

                //_sqlQuery = " UPDATE BL_EOB_Notes SET nPaymentNoteSubType = " + ReserveSubType.GetHashCode() + " , sNoteDescription = '" + txtNotes.Text.Trim().Replace("'", "''") + "' " +
                //" WHERE nEOBPaymentID = " + _eobPaymentId + " AND nPaymentNoteSubType <> 0 AND nEOBPaymentDetailID = " + _eobPaymentDetailId;
                //oDB.Connect(false);
                //int _val = oDB.Execute_Query(_sqlQuery);
                //oDB.Disconnect();

                if (ReserveSubType == NoteSubTypeV2.Advance)
                {
                    oDB.Connect(false);

                    //gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                    //object _result = null;

                    #region " CPT "

                    _sqlQuery = "Select nID from BL_EOB_Notes " +
                    " WHERE nEOBPaymentID = " + _eobPaymentId + "AND nEOBPaymentDetailID = " + _eobPaymentDetailId +
                    " AND sNoteCode IS NOT NULL AND UPPER(sNoteCode) = 'CPT' ";

                    _result = oDB.ExecuteScalar_Query(_sqlQuery);

                    if (_result != null && _result.ToString() != "" && Convert.ToInt64(_result) > 0)
                    {
                        Int64 nID = 0;
                        nID = Convert.ToInt64(_result.ToString());
                        _sqlQuery = " UPDATE BL_EOB_Notes WITH (READPAST) SET sNoteDescription = '" + _strCPTList.Replace("'", "''") + "' " +
                        " WHERE nEOBPaymentID = " + _eobPaymentId +
                        " AND sNoteCode IS NOT NULL AND UPPER(sNoteCode) = 'CPT' AND nID = " + nID;
                        oDB.Execute_Query(_sqlQuery);
                    }
                    else
                    {
                        oParameters = new gloDatabaseLayer.DBParameters();                        
                        oParameters.Clear();

                        oParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                        oParameters.Add("@nClaimNo", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBPaymentID", _eobPaymentId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBPaymentDetailID", _eobPaymentDetailId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nBillingTransactionID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                        oParameters.Add("@nBillingTransactionDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@sNoteCode", "CPT", ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                        oParameters.Add("@sNoteDescription", _strCPTList.Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                        oParameters.Add("@dNoteAmount", 0, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                        oParameters.Add("@nPaymentNoteType", NoteTypeV2.Payment_PatientReserved.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                        oParameters.Add("@nPaymentNoteSubType", NoteSubTypeV2.Advance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                        oParameters.Add("@nBillingNoteType", BillingNoteTypeV2.CPT.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                        oParameters.Add("@nIncludeNoteOnPrint", false, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                        oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                        oDB.Execute("BL_INUP_EOBNotes", oParameters, out _result);

                    }

                    #endregion " CPT "

                    _result = null;

                    #region " ICD9 "

                    _sqlQuery = "Select nID from BL_EOB_Notes " +
                    " WHERE nEOBPaymentID = " + _eobPaymentId + "AND nEOBPaymentDetailID = " + _eobPaymentDetailId +
                    " AND sNoteCode IS NOT NULL AND UPPER(sNoteCode) = 'ICD9'";

                    _result = oDB.ExecuteScalar_Query(_sqlQuery);

                    if (_result != null && _result.ToString() != "" && Convert.ToInt64(_result) > 0)
                    {
                        Int64 nID = 0;
                        nID = Convert.ToInt64(_result.ToString());
                        _sqlQuery = " UPDATE BL_EOB_Notes SET sNoteDescription = '" + _strICD9List.Replace("'", "''") + "' " +
                        " WHERE nEOBPaymentID = " + _eobPaymentId +
                        " AND sNoteCode IS NOT NULL AND UPPER(sNoteCode) = 'ICD9' AND nID = " + nID;
                        oDB.Execute_Query(_sqlQuery);
                    }
                    else
                    {
                        oParameters = new gloDatabaseLayer.DBParameters();
                        oParameters.Clear();

                        oParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                        oParameters.Add("@nClaimNo", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBPaymentID", _eobPaymentId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nEOBPaymentDetailID", _eobPaymentDetailId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@nBillingTransactionID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                        oParameters.Add("@nBillingTransactionDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@sNoteCode", "ICD9", ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                        oParameters.Add("@sNoteDescription", _strICD9List.Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                        oParameters.Add("@dNoteAmount", 0, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                        oParameters.Add("@nPaymentNoteType", NoteTypeV2.Payment_PatientReserved.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                        oParameters.Add("@nPaymentNoteSubType", NoteSubTypeV2.Advance.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                        oParameters.Add("@nBillingNoteType", BillingNoteTypeV2.ICD9.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	numeric(18, 0),
                        oParameters.Add("@nIncludeNoteOnPrint", false, ParameterDirection.Input, SqlDbType.Bit);//	numeric(18, 0),
                        oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                        oDB.Execute("BL_INUP_EOBNotes", oParameters, out _result);

                    }

                    #endregion " ICD9 "
                    _result = null;
                    oDB.Disconnect();
                    if (oParameters != null)
                    {
                        oParameters.Dispose();
                        oParameters = null;
                    }

                }

                if (txtProvider.Tag != null)
                {
                    //gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                    //object _result = null;
                    _result = null;
                    oParameters = new gloDatabaseLayer.DBParameters();
                    oParameters.Clear();
                    oDB.Connect(false);

                    oParameters.Add("@nCreditID", _eobPaymentId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nReserveID", _eobPaymentDetailId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nProviderID", Convert.ToInt64(txtProvider.Tag.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("BL_INUP_ReserveProviderAssociation", oParameters, out _result);
                    if (oParameters != null)
                    {
                        oParameters.Dispose();
                        oParameters = null;
                    }
                    //oDB.Connect(false);
                    //_sqlQuery = " UPDATE BL_Reserve_Association SET nProviderID = " + Convert.ToInt64(txtProvider.Tag.ToString()) +
                    //   " WHERE nEOBPaymentID = " + _eobPaymentId + " AND nEOBPaymentDetailID = " + _eobPaymentDetailId;
                    //oDB.Execute_Query(_sqlQuery);
                    //oDB.Disconnect();
                }
                if (cmbCollectionAgency.SelectedValue != null)
                {
                    //gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                    //object _result = null;
                    _result = null;
                    oParameters = new gloDatabaseLayer.DBParameters();
                    oParameters.Clear();
                    oDB.Connect(false);

                    oParameters.Add("@nCreditID", _eobPaymentId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nReserveID", _eobPaymentDetailId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nCollectionAgencyContactID", Convert.ToInt64(cmbCollectionAgency.SelectedValue.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("BL_INUP_ReserveCollectionAgencyAssociation", oParameters, out _result);
                    if (oParameters != null)
                    {
                        oParameters.Dispose();
                        oParameters = null;
                    }
                    _result = null;
                }
                if (_val > 0) { _retValue = true; }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _retValue;
        }


        #region " Get Replication ID "

        private Int64 GetPrefixTransactionID()
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            try
            {
                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            finally
            {

            }
            return _Result;
        }

        #endregion " Get Replication ID "




        private string GetListString(gloGeneralItem.gloItems oList)
        {
            string _strList = "";

            try
            {
                if (oList != null && oList.Count > 0)
                {
                    for (int i = 0; i <= oList.Count - 1; i++)
                    {
                        _strList += oList[i].Code.ToString().Trim() + "~" + oList[i].Description.ToString().Trim() + "|";
                    }
                    _strList = _strList.TrimEnd('|');
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            { }

            return _strList;
        }

        void ShowHideDiagnosis()
        {
            CloseInternalControl();         
            pnlDX.Visible = rbRecType_Advance.Checked;
            pnlCPT.Visible = rbRecType_Advance.Checked;          
            if (rbRecType_Advance.Checked)
            {
                this.Height = 663;
                //DesignCPTGrid();
                //DesignDxGrid();
            }
            else
            { this.Height = 280; }
            if (pnlCollectionAgency.Visible)
            {
                this.Width = 539;
                this.Height = 302;
                pnlMain.Height = 220;
                if (rbRecType_Advance.Checked)
                {
                    this.Height = 663;
                    //DesignCPTGrid();
                    //DesignDxGrid();
                }
            }
            else
            {
                this.Width = 539;
                this.Height = 275;
                pnlMain.Height = 193;
                pnlCollectionAgency.Visible = false;
                if (rbRecType_Advance.Checked)
                {
                    this.Height = 663;
                    //DesignCPTGrid();
                    //DesignDxGrid();
                }
            }
        }
        private void collectionAgencyAssociationVisibility()
        {
            gloContacts.clsCollectionAgency oCollection = new gloContacts.clsCollectionAgency(_databaseconnectionstring) ;
            DataTable dtCollectionAgency = null;
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_databaseconnectionstring);
            try
            {
                dtCollectionAgency = oCollection.GetCollectionAgencyforPayment();
                if (oSecurity.isBadDebtPatient(_patientId,true) && gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled && dtCollectionAgency != null && dtCollectionAgency.Rows.Count > 0)
                {
                    DataRow dr = dtCollectionAgency.NewRow();
                    try
                    {

                        dtCollectionAgency.Rows.InsertAt(dr, 0);
                        dtCollectionAgency.Rows[0][0] = 0;
                        dtCollectionAgency.Rows[0][1] = "";                       
                        dtCollectionAgency.AcceptChanges();
                        cmbCollectionAgency.DataSource = null;
                        cmbCollectionAgency.DataSource = dtCollectionAgency.DefaultView;
                        cmbCollectionAgency.DisplayMember = dtCollectionAgency.Columns["sName"].ColumnName;
                        cmbCollectionAgency.ValueMember = dtCollectionAgency.Columns["nContactID"].ColumnName;
                        cmbCollectionAgency.SelectedValue = 0;

                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (dr != null)
                        {
                            dr = null;
                        }

                        if (oCollection != null)
                        {
                            oCollection.Dispose();
                            oCollection = null;
                        }
                        if (oSecurity != null)
                        {
                            oSecurity.Dispose();
                            oSecurity = null;
                        }
                    }
                    this.Width = 539;
                    this.Height = 302;
                    pnlMain.Height = 220;
                    pnlCollectionAgency.Visible = true;
                }
                else
                {

                    this.Width = 539;
                    this.Height = 275;
                    pnlMain.Height = 193;
                    pnlCollectionAgency.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }      
        private void rbRecType_Advance_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideDiagnosis();
        }

        private void rbRecType_Copay_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideDiagnosis();
        }

        private void rbRecType_Other_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideDiagnosis();
        }

        private void c1FlexGrid_MouseMove(object sender, MouseEventArgs e)
        {
            C1FlexGrid _c1Grid = (C1FlexGrid)sender;

            if (_c1Grid.HitTest(e.X, e.Y).Column == COL_DX_DESC)
            {
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location, true);
            }
            else
            {
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
            }
        }

        private void txtCheckAmount_Leave(object sender, EventArgs e)
        {
            //try
            //{

            //    decimal _CheckAmount = 0;
            //    decimal _actualReserveAmt = 0;


            //    if (txtAmount.Text.Trim() != "")
            //    {
            //        _CheckAmount = Convert.ToDecimal(txtAmount.Text);

            //        if (txtAmount.Tag != null && Convert.ToString(txtAmount.Tag).Trim() != "")
            //        { _actualReserveAmt = Convert.ToDecimal(txtAmount.Tag); }

            //        if (_CheckAmount > _actualReserveAmt)
            //        {
            //            MessageBox.Show("Please enter Reserve Amount less than or equal to " + _actualReserveAmt.ToString("#.00"), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            _CheckAmount = _actualReserveAmt;
            //        }

            //    }
            //    txtAmount.Text = _CheckAmount.ToString("#0.00");


            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void txtCheckAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                {
                    e.Handled = true;
                }
                else
                {
                    // e.Handled = true;
                    //int _CurPos = txtCheckAmount.SelectionStart;
                    //int _DecPos = txtCheckAmount.Text.IndexOf(".");
                    //int _TotLen = txtCheckAmount.Text.Length;
                    //int _DecLen = txtCheckAmount.Text.Substring(_DecPos + 1).Length; 
                }
            }
            else
                if (txtAmount.Text.Contains(".") && e.KeyChar == Convert.ToChar(46))
                {

                    e.Handled = true;
                }

            if (e.KeyChar == Convert.ToChar(13))
            {

                //txtCheckAmount.Select();
                //txtCheckAmount.Focus();
            }

        }



        #region "CPT and Diagnosis Selection"

        private void DesignCPTGrid()
        {
            //c1Dx.Clear(ClearFlags.All);

            try
            {

                c1CPTList.Clear(C1.Win.C1FlexGrid.ClearFlags.All);

                c1CPTList.Cols.Count = COL_CPT_COUNT;
                c1CPTList.Rows.Count = 1;

                c1CPTList.SetData(0, COL_CPT_CODE, "Code");
                c1CPTList.SetData(0, COL_CPT_DESC, "Description");

                c1CPTList.Cols[COL_CPT_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                int nWidht = 0;
                nWidht = c1Dx.Width;

                c1CPTList.Cols[COL_CPT_CODE].AllowEditing = false;
                c1CPTList.Cols[COL_CPT_DESC].AllowEditing = false;

                c1CPTList.Cols[COL_CPT_CODE].Width = Convert.ToInt32(nWidht * 0.2);
                c1CPTList.Cols[COL_CPT_DESC].Width = Convert.ToInt32(nWidht * 0.8);


                c1CPTList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
            }
        }

        public void AddCPTToList(string CPTCode, string CPTDesc)
        {
            bool _IsExistItem = false;
            string _cptcode = "";
            string _cptdesc = "";

            try
            {
                if (CPTCode.Trim() != "")
                {
                    if (c1CPTList != null && c1CPTList.Rows.Count > 1)
                    {
                        for (int i = 1; i < c1CPTList.Rows.Count; i++)
                        {
                            _cptcode = "";
                            _cptdesc = "";

                            _cptcode = Convert.ToString(c1CPTList.GetData(i, COL_CPT_CODE));
                            _cptdesc = Convert.ToString(c1CPTList.GetData(i, COL_CPT_DESC));
                            if (_cptcode.ToUpper().Trim() == CPTCode.ToUpper().Trim()) // && _dxdesc.ToUpper() == DxDesc.ToUpper())
                            { _IsExistItem = true; break; }
                        }
                    }
                    if (_IsExistItem == false && CPTCode.Trim() != "")
                    {
                        c1CPTList.Rows.Add();
                        int rowIndex = c1CPTList.Rows.Count - 1;
                        c1CPTList.SetData(rowIndex, COL_CPT_CODE, CPTCode);
                        c1CPTList.SetData(rowIndex, COL_CPT_DESC, CPTDesc);

                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void DesignDxGrid()
        {
            //c1Dx.Clear(ClearFlags.All);

            try
            {

                c1Dx.Clear(C1.Win.C1FlexGrid.ClearFlags.All);

                c1Dx.Cols.Count = COL_DX_COUNT;
                c1Dx.Rows.Count = 1;

                c1Dx.SetData(0, COL_DX_CODE, "Code");
                c1Dx.SetData(0, COL_DX_DESC, "Description");

                c1Dx.Cols[COL_DX_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                int nWidht = 0;
                nWidht = c1Dx.Width;

                c1Dx.Cols[COL_DX_CODE].AllowEditing = false;
                c1Dx.Cols[COL_DX_DESC].AllowEditing = false;

                c1Dx.Cols[COL_DX_CODE].Width = Convert.ToInt32(nWidht * 0.2);
                c1Dx.Cols[COL_DX_DESC].Width = Convert.ToInt32(nWidht * 0.8);


                c1Dx.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
            }
        }

        public void AddDxToList(string DxCode, string DxDesc)
        {
            bool _IsExistItem = false;
            string _dxcode = "";
            string _dxdesc = "";

            try
            {
                if (DxCode.Trim() != "")
                {
                    if (c1Dx != null && c1Dx.Rows.Count > 1)
                    {
                        for (int i = 1; i < c1Dx.Rows.Count; i++)
                        {
                            _dxcode = "";
                            _dxdesc = "";

                            _dxcode = Convert.ToString(c1Dx.GetData(i, COL_DX_CODE));
                            _dxdesc = Convert.ToString(c1Dx.GetData(i, COL_DX_DESC));
                            if (_dxcode.ToUpper().Trim() == DxCode.ToUpper().Trim()) // && _dxdesc.ToUpper() == DxDesc.ToUpper())
                            { _IsExistItem = true; break; }
                        }
                    }
                    if (_IsExistItem == false && DxCode.Trim() != "")
                    {
                        c1Dx.Rows.Add();
                        int rowIndex = c1Dx.Rows.Count - 1;
                        c1Dx.SetData(rowIndex, COL_DX_CODE, DxCode);
                        c1Dx.SetData(rowIndex, COL_DX_DESC, DxDesc);

                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1Diagnosis_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                OpenInternalControl(gloGridListControlType.ICD9, "Diagnosis", false, 0, 0, "");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                throw;
            }
        }

        private void c1Diagnosis_ChangeEdit(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {

                    string _strSearchString = c1Diagnosis.Editor.Text;

                    //******************************************************************
                    //Commented By Debasish as Business Logic is shifted to SP
                    //*******************************************************************
                    //////if (_strSearchString.Length == 3)
                    //////{
                    //////    _strSearchString = _strSearchString + ".";
                    //////}
                    //////else if (_strSearchString.Length > 3)
                    //////{
                    //////    if (_strSearchString.Substring(3, 1).ToString() != ".")
                    //////    {
                    //////        string _PeriodSearch = _strSearchString.Substring(0, 3) + "." + _strSearchString.Substring(3, _strSearchString.Length - 3);
                    //////        _strSearchString = _PeriodSearch;
                    //////    }

                    //////}
                    //******************************************************************

                    ogloGridListControl.FillControl(_strSearchString);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1Diagnosis_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {

                                    bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                                    if (_IsItemSelected)
                                    {
                                    }
                                }
                            }
                        }
                        break;
                    case Keys.Delete:
                        {

                        }
                        break;
                    case Keys.Down:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    ogloGridListControl.Focus();
                                }
                            }
                        }
                        break;
                    case Keys.Escape:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    CloseInternalControl();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void btnDeleteDx_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1Dx.Rows.Count > 1)
                {
                    c1Dx.Rows.Remove(c1Dx.Row);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
            }
        }

        private void btnSelectDx_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlInternalControl.Visible)
                {
                    if (ogloGridListControl != null)
                    {

                        bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                        if (_IsItemSelected)
                        {
                        }
                    }
                }
                else
                {
                    OpenInternalControl(gloGridListControlType.ICD9, "Diagnosis", false, 0, 0, "");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                throw;
            }
        }


        private void c1CPT_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                OpenInternalControl(gloGridListControlType.CPT, "CPT", false, 0, 0, "");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                throw;
            }
        }

        private void c1CPT_ChangeEdit(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    string _strSearchString = c1CPT.Editor.Text;

                    //if (_strSearchString.Length == 3)
                    //{
                    //    _strSearchString = _strSearchString + ".";
                    //}
                    //else if (_strSearchString.Length > 3)
                    //{
                    //    if (_strSearchString.Substring(3, 1).ToString() != ".")
                    //    {
                    //        string _PeriodSearch = _strSearchString.Substring(0, 3) + "." + _strSearchString.Substring(3, _strSearchString.Length - 3);
                    //        _strSearchString = _PeriodSearch;
                    //    }

                    //}
                    ogloGridListControl.FillControl(_strSearchString);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1CPT_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {

                                    bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                                    if (_IsItemSelected)
                                    {
                                    }
                                }
                            }
                        }
                        break;
                    case Keys.Delete:
                        {

                        }
                        break;
                    case Keys.Down:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    ogloGridListControl.Focus();
                                }
                            }
                        }
                        break;
                    case Keys.Escape:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    CloseInternalControl();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void btnDeleteCPT_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1CPTList.Rows.Count > 1)
                {
                    c1CPTList.Rows.Remove(c1CPTList.Row);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
            }
        }

        private void btnSelectCPT_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlInternalControl.Visible)
                {
                    if (ogloGridListControl != null)
                    {

                        bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                        if (_IsItemSelected)
                        {
                        }
                    }
                }
                else
                {
                    OpenInternalControl(gloGridListControlType.CPT, "CPT", false, 0, 0, "");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                throw;
            }
        }



        #endregion

        #region "Grid Control Methods & events"

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {
                //_dxCodeForDistinct = "";
                //_dxDescriptionForDistinct = "";

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                if (rbICD10.Checked)
                {
                    ogloGridListControl.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode();
                }
                else
                {
                    ogloGridListControl.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
                }
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();

                //int _x = c1Modifier.Cols[ColIndex].Left;
                //int _y = c1Modifier.Rows[RowIndex].Bottom;
                //int _width = pnlInternalControl.Width;
                //int _height = pnlInternalControl.Height;
                //int _parentleft = this.Parent.Bounds.Left;
                //int _parentwidth = this.Parent.Bounds.Width;
                //int _diffFactor = _parentwidth - _x;

                //if (_diffFactor < _width)
                //{
                //    _x = this.Parent.Bounds.Width + (_diffFactor);
                //    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                //}
                //else
                //{
                //    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                //}

                if (ogloGridListControl.ControlType == gloGridListControlType.ICD9)
                {
                    pnlInternalControl.Controls.Add(ogloGridListControl);
                    pnlInternalControl.SetBounds(c1Diagnosis.Location.X, c1Diagnosis.Location.Y + c1Diagnosis.Height + pnlDX.Location.Y, 0, 0, BoundsSpecified.Location);
                    pnlInternalControl.Visible = true;
                    pnlInternalControl.BringToFront();
                }
                if (ogloGridListControl.ControlType == gloGridListControlType.CPT)
                {
                    pnlInternalControl.Controls.Add(ogloGridListControl);
                    pnlInternalControl.SetBounds(c1CPT.Location.X, c1CPT.Location.Y + c1CPT.Height + pnlCPT.Location.Y, 0, 0, BoundsSpecified.Location);
                    pnlInternalControl.Visible = true;
                    pnlInternalControl.BringToFront();
                }



                ogloGridListControl.Focus();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                //RePositionInternalControl();
            }
            return _result;
        }

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
        }

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl.SelectedItems != null)
                {
                    if (ogloGridListControl.SelectedItems.Count > 0)
                    {
                        switch (ogloGridListControl.ControlType)
                        {

                            case gloGridListControlType.ICD9:
                                {

                                    //Added By Pramod Nair For Avoiding Duplicate Insertion Of CPT
                                    Boolean isCPTExists = false;
                                    for (int j = 0; j <= c1Dx.Rows.Count - 1; j++)
                                    {
                                        if (c1Dx.GetData(j, COL_DX_CODE).ToString() == ogloGridListControl.SelectedItems[0].Code.ToString())
                                        {
                                            isCPTExists = true;
                                            break;
                                        }

                                    }
                                    //End 

                                    if (!isCPTExists)
                                    {
                                        AddDxToList(ogloGridListControl.SelectedItems[0].Code, ogloGridListControl.SelectedItems[0].Description);
                                    }

                                }
                                break;
                            case gloGridListControlType.CPT:
                                {

                                    //Added By Pramod Nair For Avoiding Duplicate Insertion Of CPT
                                    Boolean isCPTExists = false;
                                    for (int j = 0; j <= c1CPTList.Rows.Count - 1; j++)
                                    {
                                        if (c1CPTList.GetData(j, COL_CPT_CODE).ToString() == ogloGridListControl.SelectedItems[0].Code.ToString())
                                        {
                                            isCPTExists = true;
                                            break;
                                        }

                                    }
                                    //End 

                                    if (!isCPTExists)
                                    {
                                        AddCPTToList(ogloGridListControl.SelectedItems[0].Code, ogloGridListControl.SelectedItems[0].Description);
                                    }

                                }
                                break;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                switch (ogloGridListControl.ControlType)
                {
                    case gloGridListControlType.ICD9:
                        {
                            c1Diagnosis.Clear(C1.Win.C1FlexGrid.ClearFlags.Content, 0, 0);
                            c1Diagnosis.Focus();
                        }
                        break;
                    case gloGridListControlType.CPT:
                        {
                            c1CPT.Clear(C1.Win.C1FlexGrid.ClearFlags.Content, 0, 0);
                            c1CPT.Focus();
                        }
                        break;
                }
                CloseInternalControl();
            }
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 4/2/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null) { ogloGridListControl.Dispose(); ogloGridListControl = null; }
                //c1Diagnosis.Focus();
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();
                c1CPT.Clear(ClearFlags.Content);
                c1Diagnosis.Clear(ClearFlags.Content);
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            { }
            return _result;
        }



        #endregion

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {

                int _Counter = 0;
                switch (_CurrentControlType)
                {

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
                        pnlMain.Visible = true; 
                        break;
                }
                //if (IsProviderEnable)
                //{
                //    pnlEnableProvider.Visible = true;
                //}
                //else
                //{
                //    pnlEnableProvider.Visible = false;
                //}
                this.Height = 280;
                this.Width = 539;
                rbRecType_Advance_CheckedChanged(null, null);
                if (rbRecType_Advance.Checked)
                {
                    pnlDX.Visible = true;
                    pnlCPT.Visible = true;
                    pnlMain.Visible = true;
                }
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
            }
            //if (IsProviderEnable)
            //{
            //    pnlEnableProvider.Visible = true;
            //}
            //else
            //{
            //    pnlEnableProvider.Visible = false;
            //}
            this.Height = 280;
            this.Width = 539;
            rbRecType_Advance_CheckedChanged(null, null);
            if (rbRecType_Advance.Checked)
            {
                pnlDX.Visible = true;
                pnlCPT.Visible = true;
            }
            pnlMain.Visible = true;
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_databaseconnectionstring);
            if (oSecurity.isBadDebtPatient(_patientId, true) && gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
            {
                this.Width = 539;
                this.Height = 302;
                pnlMain.Height = 220;
                pnlCollectionAgency.Visible = true;
                if (rbRecType_Advance.Checked)
                {
                    this.Height = 663;
                }
            }
            else
            {
                this.Width = 539;
                this.Height = 275;
                pnlMain.Height = 193;
                pnlCollectionAgency.Visible = false;
                if (rbRecType_Advance.Checked)
                {
                    this.Height = 663;
                }
            }
            oSecurity.Dispose();
            oSecurity = null;

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
                this.Height = 663;
                this.Width = 675;
                pnlDX.Visible = false;
                pnlCPT.Visible = false;
                pnlMain.Visible = false;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
            txtProvider.Text = "";
            txtProvider.Tag = 0;
        }
        // code added for selection  icd and icd validation Sameer 02172014
        private void rbICD9_CheckedChanged(object sender, EventArgs e)
        {
            if (rbICD9.Checked == true)
            {
                rbICD9.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbICD9.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
            CloseInternalControl();
        }

        private void rbICD10_CheckedChanged(object sender, EventArgs e)
        {
            if (rbICD10.Checked == true)
            {
                rbICD10.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbICD10.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
            CloseInternalControl();
        }

        private void ICDValidation()
        {

            string _dxcode = "";
            string _dxdesc = "";
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, _databaseconnectionstring);
            if (c1Dx != null && c1Dx.Rows.Count > 1)
            {

                int nICDRevision = (int)gloGlobal.gloICD.CodeRevision.ICD9;
                if (rbICD10.Checked == true)
                    nICDRevision = (int)gloGlobal.gloICD.CodeRevision.ICD10;
                else if (rbICD9.Checked == true)
                    nICDRevision = (int)gloGlobal.gloICD.CodeRevision.ICD9;

                for (int i = 1; i < c1Dx.Rows.Count; i++)
                {
                    _dxcode = "";
                    _dxdesc = "";

                    _dxcode = Convert.ToString(c1Dx.GetData(i, COL_DX_CODE));
                    _dxdesc = Convert.ToString(c1Dx.GetData(i, COL_DX_DESC));


                    if (_dxcode != "")
                    {
                        if (ogloBilling.CheckDxStatus(_dxcode.Trim(), nICDRevision) == 0)
                        {
                            if (rbICD10.Checked)
                            {
                                MessageBox.Show("First remove the ICD9 codes before changing to ICD10", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("First remove the ICD10 codes before changing to ICD9", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            _IsValidate = false;
                            break;
                        }
                        else
                        {
                            _IsValidate = true;

                        }

                    }


                }

            }
            if (ogloBilling != null)
            {
                ogloBilling.Dispose();
                ogloBilling = null;
            }
        }

        private void txtNotes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(126))
            {
                e.Handled = true;
            }


        }

        private void mskReserveForDOS_MouseClick(object sender, MouseEventArgs e)
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

            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                //IsReserveDOSAvailable = true;

                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _IsReservedForDOSValidated = false;
                            e.Cancel = true;

                            //IsReserveDOSAvailable = false;
                            //isclosecheck = false;
                        }
                        else
                        {
                            _IsReservedForDOSValidated = true;
                            mskReserveForDOS.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                            if (mskReserveForDOS.Text.ToString().Trim() != "")
                            {
                                mskReserveForDOS.TextMaskFormat = MaskFormat.IncludeLiterals;
                                dtReserveForDOS = Convert.ToDateTime(mskReserveForDOS.Text);
                            }
                            else
                            {
                                dtReserveForDOS = DateTime.MinValue;
                            }
                        }
                    }
                    else
                    { dtReserveForDOS = DateTime.MinValue; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _IsReservedForDOSValidated = false;
                e.Cancel = true;
                //_IsAllDatesValid = false;
                //isclosecheck = false;
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                //if (Convert.ToString(mskReserveForDOS.Text.Trim()) != null && Convert.ToString(mskReserveForDOS.Text.Trim()) != "/  /" && IsReserveDOSAvailable == true)
                //{
                //    mskReserveForDOS.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                //    dtReserveForDOS = Convert.ToDateTime(Convert.ToString(mskReserveForDOS.Text));
                //}
                //else if(Convert.ToString(mskReserveForDOS.Text.Trim()) == null || Convert.ToString(mskReserveForDOS.Text.Trim()) == "/  /")
                //{
                //    dtReserveForDOS = DateTime.MinValue;
                //}
              
            }

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(e.ToString(), false);
                e = null;
                Success = false; // If this line is reached, an exception was thrown
            }
            return Success;
        }
       
    }
}
