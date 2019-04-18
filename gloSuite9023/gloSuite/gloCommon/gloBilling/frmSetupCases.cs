using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloBilling.gloPriorAuthorization;
using gloSettings;
using C1.Win.C1FlexGrid;
using System.Collections;




namespace gloBilling
{

    public partial class frmSetupCases : Form
    {

        #region "Private Variables"

        private bool blnAbortClose = false;
        private bool _patientHasPriorAuthorization = false;
        private Int64 _PatientID = 0;
        Int64 nCaseId = 0;
        public string _DatabaseConnectionString = "";
        public String _MessageBoxCaption = "";
        string _UserName = "";
        private int _DiagnosisCount = 0;
        private int _DiagnosisFixCount = 4;
        private Int64 _nCaseID = 0;
        private String _sCaseName = "";
        private DataTable _CaseData = new DataTable();
        private DataTable _Diagnosis = new DataTable();
        private DataTable _Insurences = new DataTable();
        public int width;
        private bool _IsValidate = true;
        private ComboBox combo;
        const int COL_NOTE_IMAGE = 5;
        private int iChargesSelRow = 1;
        // To Define Column Constant for Insurance Grid
        const int COL_SELECT = 0;
        const int COL_INSURANCERESPONSIBILITY = 1;
        const int COL_INSURANCEID = 2;
        const int COL_INSURANCENAME = 3;
        const int COL_INSURANCETYPE = 4;
        const int COL_INSSELFMODE = 5;
        //const int COL_INSURANCECOPAYAMT = 6;
        const int COL_INSURANCEWORKERCOMP = 6;
        const int COL_INSURANCEAUTOCLAIM = 7;
        const int COL_INSURANCECONTACTID = 8;
        const int COL_INSURANCEPARTY = 9;
        const int COL_INSURANCECURRRESP = 10;
        const int COL_INSURANCEPLANONHOLD = 11;
        const int COL_INSVIEW_COUNT = 14;
        const int COL_SUBSCRIBERID = 13;
        const int COL_IndID = 12;

        gloGridListControl ogloGridListControl = null;
        const int COL_ID = 0;
        const int COL_CODE = 1;
        const int COL_DESC = 2;
        const int COL_COUNT = 3;
        Boolean _IsCloseClick = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private clsCaseSetup oclsCaseSetup = new clsCaseSetup();

        Form frmOBMedCatlist = null;
        gloListControl.gloListControl oMedicalCategory = null;
        Boolean bIsOBPregCaseCheck = false;

        private Boolean bIsEnableWorkersCompForms = false;
        #endregion

        #region " Constructor "
        public frmSetupCases(Int64 PatientID)
        {
            InitializeComponent();
            _DatabaseConnectionString = AppSettings.ConnectionStringPM;
            _MessageBoxCaption = AppSettings.MessageBoxCaption;
            _PatientID = PatientID;

        }
        public frmSetupCases(Int64 PatientID, Int64 nCaseId)
        {
            InitializeComponent();
            _DatabaseConnectionString = AppSettings.ConnectionStringPM;
            _MessageBoxCaption = AppSettings.MessageBoxCaption;
            _PatientID = PatientID;
            this.nCaseId = nCaseId;

        }

        #endregion " Constructor "

        #region "Properties"
        public bool PatientHasPriorAuthorization
        {
            get { return _patientHasPriorAuthorization; }
            set { _patientHasPriorAuthorization = value; }
        }
        public Int64 nCaseID
        {
            get { return _nCaseID; }
            set { _nCaseID = value; }
        }
        public string sCaseName
        {
            get { return _sCaseName; }
            set { _sCaseName = value; }
        }
        public DataTable CaseData
        {
            get { return _CaseData; }
            set { _CaseData = value; }
        }
        public DataTable Diagnosis
        {
            get { return _Diagnosis; }
            set { _Diagnosis = value; }
        }
        public DataTable CurrentCaseInsurences
        {
            get { return _Insurences; }
            set { _Insurences = value; }
        }
        #endregion

        #region "Destructor"
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (CaseData != null)
            {
                CaseData.Dispose();
                CaseData = null;
            }
            if (Diagnosis != null)
            {
                Diagnosis.Dispose();
                Diagnosis = null;
            }
            if (CurrentCaseInsurences != null)
            {
                CurrentCaseInsurences.Dispose();
                CurrentCaseInsurences = null;
            }
            if (_CaseData != null)
            {
                _CaseData.Dispose();
                _CaseData = null;
            }
            if (_Diagnosis != null)
            {
                _Diagnosis.Dispose();
                _Diagnosis = null;
            }
            if (_Insurences != null)
            {
                _Insurences.Dispose();
                _Insurences = null;
            }

            if (disposing && (components != null))
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion "Destructor

        #region " Form Load "

        private void frmSetupCases_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1Insurance, false);

            DesignGrid();
            try
            {
                bIsEnableWorkersCompForms = GetEnableWorkersCompFormsSetting(_DatabaseConnectionString);

                FillFacilitiesData();
                FillStatesData();
                FillOtherClaimDateQualifiers();
                FillClaimDateQualifiers();
                // SetFormLoadData();
                chkOBPregnancyCase.CheckedChanged -= new EventHandler(chkOBPregnancyCase_CheckedChanged);
                FillCasesData();
                chkOBPregnancyCase.CheckedChanged += new EventHandler(chkOBPregnancyCase_CheckedChanged);
                if (nCaseId != 0)
                {
                    this.Text = "Modify Case";
                }
                else
                {
                    this.Text = "New Case";
                    mskCaseStarDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    this.Height = this.Height - pnlClaimGrid.Height;
                    tsb_Modify.Visible = false;
                    if (gloGlobal.gloPMGlobal.CurrentICDRevision == gloGlobal.gloICD.CodeRevision.ICD10)
                    {
                        rbICD10.Checked = true;
                    }
                    else
                    {
                        rbICD9.Checked = true;
                    }
                }
                ChangeInsuranceGridColor();
                this.PatientHasPriorAuthorization = oclsCaseSetup.CheckPriorAuthorization(_PatientID);
                CheckForPatientPriorAuthorization();
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                // This method actually sets the order all the way down the control hierarchy.
                tom.SetTabOrder(scheme);
                txtCaseNo.Focus();

                txtCaseNo.Select();
                cmbFacility.DrawMode = DrawMode.OwnerDrawFixed;
                cmbFacility.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
                cmbReferralProvider.DrawMode = DrawMode.OwnerDrawFixed;
                cmbReferralProvider.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
                cmbReportingCategory.DrawMode = DrawMode.OwnerDrawFixed;
                cmbReportingCategory.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
                CmbAccidentType.DrawMode = DrawMode.OwnerDrawFixed;
                CmbAccidentType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                Cls_TabIndexSettings tabSettings = null;
                tabSettings = new Cls_TabIndexSettings(this);
                tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);
            }

        }

        #endregion

        #region "Private Method"

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {
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
                pnlInternalControl.Controls.Add(ogloGridListControl);

                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();


                if (ogloGridListControl.ControlType == gloGridListControlType.ICD9)
                    pnlInternalControl.SetBounds(pnlDiagnosis.Location.X, 322, 0, 0, BoundsSpecified.Location);

                pnlInternalControl.Width = c1DiagnosisSelected.Width;

                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
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
                if (ogloGridListControl != null)
                {
                    try
                    {
                        ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown -= new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);

                    }
                    catch { }
                    ogloGridListControl.Dispose();
                    ogloGridListControl = null;
                }
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();
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


                                    if (_DiagnosisCount >= _DiagnosisFixCount)
                                    {

                                        break;
                                    }

                                    Boolean isCPTExists = false;
                                    for (int j = 0; j <= c1DiagnosisSelected.Rows.Count - 1; j++)
                                    {
                                        if (c1DiagnosisSelected.GetData(j, 1).ToString() == ogloGridListControl.SelectedItems[0].Code.ToString())
                                        {
                                            isCPTExists = true;
                                            break;
                                        }

                                    }


                                    if (!isCPTExists && ogloGridListControl.SelectedItems[0].Code.ToString().Trim() != "")
                                    {
                                        C1.Win.C1FlexGrid.Row oNewRow = c1DiagnosisSelected.Rows.Add();
                                        c1DiagnosisSelected.SetData(oNewRow.Index, COL_ID, 0);
                                        c1DiagnosisSelected.SetData(oNewRow.Index, COL_CODE, ogloGridListControl.SelectedItems[0].Code);
                                        c1DiagnosisSelected.SetData(oNewRow.Index, COL_DESC, ogloGridListControl.SelectedItems[0].Description);
                                        _DiagnosisCount++;
                                    }

                                }
                                break;

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                ex = null;
            }
            finally
            {

                c1Diagnosis.Clear(C1.Win.C1FlexGrid.ClearFlags.Content, 0, 0);
                CloseInternalControl();
            }
        }
        private void DesignGrid()
        {
            try
            {

                #region "Diagnosis Grid"
                DesignInsuranceGrid();

                c1DiagnosisSelected.Rows.Count = 1;
                c1DiagnosisSelected.Rows.Fixed = 1;
                c1DiagnosisSelected.Cols.Count = COL_COUNT;
                c1DiagnosisSelected.Cols.Fixed = 0;

                c1DiagnosisSelected.SetData(0, COL_ID, "ID");
                c1DiagnosisSelected.SetData(0, COL_CODE, "Code");
                c1DiagnosisSelected.SetData(0, COL_DESC, "Description");

                c1DiagnosisSelected.Cols[COL_ID].Visible = false;
                c1DiagnosisSelected.Cols[COL_CODE].Visible = true;
                c1DiagnosisSelected.Cols[COL_DESC].Visible = true;
                c1DiagnosisSelected.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1DiagnosisSelected.Cols[COL_DESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                int _width = pnlDiagnosis.Width - 2;

                c1DiagnosisSelected.Cols[COL_ID].Width = 0;
                c1DiagnosisSelected.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.2);
                c1DiagnosisSelected.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.80);

                c1Diagnosis.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                ex = null;
            }
        }
        //private void SetFormLoadData()
        //{
        //    DataSet _dsChargesData = null;
        //    try
        //    {

        //        if (_PatientID > 0 )
        //        {
        //            //..Fill the Patient Strip

        //            _dsChargesData = clsCaseSetup.GetRefferringProviderData(_PatientID);

        //            if (_dsChargesData != null && _dsChargesData.Tables.Count > 0)
        //            {                      
        //                  FillReferralProvidersData(_dsChargesData.Tables[0], true);                         
        //            }


        //        }
        //        else
        //        {
        //            gloAuditTrail.gloAuditTrail.ExceptionLog("Either PatientID or UserID found zero while loading charges form.", false);
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx.ToString(), false);
        //    }
        //    finally
        //    {
        //        _dsChargesData.Dispose();
        //        _dsChargesData = null;
        //    }


        // }
        private void ReloadPatientRefferals(Int64 nPatientID)
        {

            DataSet _dtPatientRefferals = null;

            try
            {
                //Retrieve Patient Referral Data on Reload
                _dtPatientRefferals = clsCaseSetup.GetRefferringProviderData(_PatientID);
                FillReferralProvidersData(_dtPatientRefferals.Tables[0], true);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (_dtPatientRefferals != null) { _dtPatientRefferals.Dispose(); }
            }
        }
        private void FillReferralProvidersData(DataTable dtReferralProviders, bool fillBlankItem)
        {

            string _selectedRefProvName = "";
            if (cmbReferralProvider.SelectedValue != null)
            {
                _selectedRefProvName = Convert.ToString(cmbReferralProvider.Text);
            }
            if (dtReferralProviders != null)
            {
                if (fillBlankItem == true)
                {
                    DataRow dr = dtReferralProviders.NewRow();
                    dr["nReferralID"] = 0;
                    dr["sReferralName"] = "";
                    dtReferralProviders.Rows.InsertAt(dr, 0);
                }


                if (dtReferralProviders.Rows.Count > 0)
                {

                    cmbReferralProvider.BeginUpdate();
                   
                    cmbReferralProvider.DataSource = null;
                    cmbReferralProvider.Items.Clear();
                    cmbReferralProvider.Tag = null;

                    cmbReferralProvider.DataSource = dtReferralProviders.Copy();
                    cmbReferralProvider.ValueMember = dtReferralProviders.Columns["nReferralID"].ColumnName;
                    cmbReferralProvider.DisplayMember = dtReferralProviders.Columns["sReferralName"].ColumnName;
                    cmbReferralProvider.EndUpdate();
                    if (_selectedRefProvName != "")
                    {
                        cmbReferralProvider.Text = _selectedRefProvName;
                    }
                    else if (_selectedRefProvName == "")
                    {
                        cmbReferralProvider.Text = _selectedRefProvName;
                    }
                }
                dtReferralProviders.Dispose();
                dtReferralProviders = null;

            }
        }
        private void CheckForPatientPriorAuthorization()
        {
            try
            {

                if (this.PatientHasPriorAuthorization)
                {
                    if ((Convert.ToString(txtPriorAuthorizationNo.Tag) == "") || (txtPriorAuthorizationNo.Tag == null))
                    {
                        txtPriorAuthorizationNo.Text = "<available>";
                        txtPriorAuthorizationNo.TextAlign = HorizontalAlignment.Center;
                        txtPriorAuthorizationNo.ForeColor = Color.Maroon;

                    }
                    else
                    {
                        txtPriorAuthorizationNo.TextAlign = HorizontalAlignment.Left;
                        txtPriorAuthorizationNo.ForeColor = Color.Black;

                    }
                }
                else
                {
                    txtPriorAuthorizationNo.Text = "";
                    txtPriorAuthorizationNo.Tag = "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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
            catch (FormatException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                Success = false;

            }
            return Success;
        }
        private void FillFacilitiesData()
        {
            DataTable dtFacilities = null;
            try
            {
                dtFacilities = gloCharges.GetCachedFacilities();
                DataRow dr = dtFacilities.NewRow();
                dr["sFacilityCode"] = "";
                dr["sFacilityName"] = "";
                dtFacilities.Rows.InsertAt(dr, 0);
                if (dtFacilities != null)
                {
                    cmbFacility.BeginUpdate();
                    cmbFacility.DataSource = dtFacilities.Copy();
                    cmbFacility.ValueMember = dtFacilities.Columns["sFacilityCode"].ColumnName;
                    cmbFacility.DisplayMember = dtFacilities.Columns["sFacilityName"].ColumnName;
                    cmbFacility.EndUpdate();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return;
            }
            finally
            {
                if (dtFacilities != null)
                {
                    dtFacilities.Dispose();
                    dtFacilities = null;
                }

            }
        }
        private void FillStatesData()
        {
            DataTable dtStates = null;
            try
            {
                dtStates = gloGlobal.gloPMMasters.GetStates();
                if (dtStates != null && dtStates.Rows.Count > 0)
                {
                    cmbState.BeginUpdate();
                    cmbState.DataSource = dtStates;
                    cmbState.DisplayMember = dtStates.Columns["ST"].ColumnName;
                    cmbState.ValueMember = dtStates.Columns["ST"].ColumnName;
                    cmbState.EndUpdate();
                    cmbState.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return;
            }
            finally
            {
                //if (dtStates != null)
                //{
                //    dtStates.Dispose();
                //    dtStates = null;
                //}
            }
        }
        private void FillCasesData()
        {
            #region " Get From DB & Add to Cache "
            DataSet ds = new DataSet();
            DataTable dtReportingCAtegory = new DataTable();
            DataTable dtPatientName = new DataTable();
            DataTable dtPatientInsurances = new DataTable();
            DataTable dtCases = new DataTable();
            DataTable dtCasesDiag = new DataTable();
            DataTable dtCasesIns = new DataTable();
            DataTable dtRefferalProvider = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
           
            bool _IsPrimaryPresent = false;
            bool _HasInsurance = true;

            int _CntPrimary = 0;
            Object PatientName = new Object();
            //    string strQuery = "";
            try
            {

                ds = clsCaseSetup.CasesData(this._PatientID, this.nCaseId);
                if (ds != null)
                {
                    if (ds.Tables[0] != null)
                        dtCases = ds.Tables[0];
                    if (ds.Tables[1] != null)
                    {
                        dtCasesDiag = ds.Tables[1];
                        if (dtCasesDiag.Rows.Count > 0)
                        {
                            if (ogloBilling.CheckDxStatus(dtCasesDiag.Rows[0][1].ToString(), gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode()) != 0)
                            {
                                rbICD9.Checked = true;
                            }
                            else
                            {
                                rbICD10.Checked = true;
                            }
                        }
                        else
                        {
                            if (dtCases.Rows.Count > 0)
                            {
                                if (dtCases.Rows[0]["nICDRevision"] != DBNull.Value && Convert.ToInt16(dtCases.Rows[0]["nICDRevision"]) == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                                {

                                    rbICD10.Checked = true;
                                }
                                else if (dtCases.Rows[0]["nICDRevision"] != DBNull.Value && Convert.ToInt16(dtCases.Rows[0]["nICDRevision"]) == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                                {
                                    rbICD9.Checked = true;
                                }
                            }
                        }
                    }
                    if (ds.Tables[2] != null)
                        dtCasesIns = ds.Tables[2];
                    if (ds.Tables[3] != null)
                        dtReportingCAtegory = ds.Tables[3];
                    if (ds.Tables[4] != null)
                        dtPatientInsurances = ds.Tables[4];
                    if (ds.Tables[5] != null)
                        dtPatientName = ds.Tables[5];
                    if (ds.Tables[6] != null)
                        dtRefferalProvider = ds.Tables[6];
                }

                # region Fill Reporting Category
                //----------------------------Fill Reporting Category--------------------------------------

                DataRow dr = dtReportingCAtegory.NewRow();
                dr["nID"] = 0;
                dr["sDecription"] = "";
                dtReportingCAtegory.Rows.InsertAt(dr, 0);
                oDB.Disconnect();
                if (dtReportingCAtegory != null && dtReportingCAtegory.Rows.Count > 0)
                {
                    if (dtReportingCAtegory != null)
                    {
                        if (dtReportingCAtegory.Rows.Count > 0)
                        {
                            cmbReportingCategory.BeginUpdate();
                            cmbReportingCategory.DataSource = dtReportingCAtegory.Copy();
                            cmbReportingCategory.ValueMember = dtReportingCAtegory.Columns["nID"].ColumnName;
                            cmbReportingCategory.DisplayMember = dtReportingCAtegory.Columns["sDecription"].ColumnName;
                            cmbReportingCategory.EndUpdate();

                        }
                    }
                }
                #endregion
                #region Fill PAtient
                //---------------------------------------------Fill PAtient---------------------------------------------------
                if (dtPatientName != null)
                {

                    lblPatientName1.Text = dtPatientName.Rows[0][0].ToString();
                    this.toolTip1.SetToolTip(this.lblPatientName1, dtPatientName.Rows[0][0].ToString());

                }
                #endregion
                #region Fill Insuences
                c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_DatabaseConnectionString);
                dtPatientInsurances = ogloPatient.getPatientInsurances(_PatientID);
                ogloPatient.Dispose();
                ogloPatient = null;
                c1Insurance.Rows.Count = 1;
                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                {
                    //nInsuranceID,InsuranceName ,sSubscriberID,sSubscriberName,sSubscriberPolicy#,sGroup,
                    //sPhone ,bPrimaryFlag,dtDOB,dtEffectiveDate,dtExpiryDate,sSubscriberID
                    for (int i = 0; i < dtPatientInsurances.Rows.Count; i++)
                    {
                        if (dtPatientInsurances.Rows[i]["sInsuranceFlag"] != null &&
                            dtPatientInsurances.Rows[i]["sInsuranceFlag"] != DBNull.Value &&
                            Convert.ToString(dtPatientInsurances.Rows[i]["sInsuranceFlag"]) != "" &&
                            Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) != InsuranceTypeFlag.None.GetHashCode())
                        {

                            c1Insurance.Rows.Add();
                            int rowIndex = c1Insurance.Rows.Count - 1;
                            c1Insurance.SetData(rowIndex, COL_SELECT, false);//Select-CheckBox
                            //Select-CheckBox
                            c1Insurance.SetData(rowIndex, COL_INSURANCENAME, Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceName"])); //
                            c1Insurance.SetData(rowIndex, COL_INSURANCEID, Convert.ToString(dtPatientInsurances.Rows[i]["nInsuranceID"])); //
                            c1Insurance.SetData(rowIndex, COL_INSSELFMODE, PayerMode.Insurance.GetHashCode()); //
                            c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, Convert.ToString(dtPatientInsurances.Rows[i]["sInsuranceFlag"]));
                            c1Insurance.SetData(rowIndex, COL_SUBSCRIBERID, Convert.ToString(dtPatientInsurances.Rows[i]["sSubscriberID"]));
                            c1Insurance.SetData(rowIndex, COL_INSURANCERESPONSIBILITY, Convert.ToString(""));
                            c1Insurance.SetData(rowIndex, COL_INSURANCEPARTY, Convert.ToString("X"));
                            c1Insurance.SetData(rowIndex, COL_IndID, 0);
                            if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                            {
                                c1Insurance.SetData(rowIndex, COL_SELECT, false);
                                _CntPrimary = _CntPrimary + 1;
                                _IsPrimaryPresent = true;
                            }


                        }
                    }
                }
                else
                {
                    _HasInsurance = false;
                }

                c1Insurance.Rows.Add();
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_SELECT, false);//Select-CheckBox
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCERESPONSIBILITY, Convert.ToString(""));
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEPARTY, Convert.ToString("X"));
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSSELFMODE, PayerMode.Self.GetHashCode()); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCENAME, "Self"); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEID, 0); //                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSSELFMODE, PayerMode.Self.GetHashCode()); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCECONTACTID, 0);
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_IndID, 0);
                //Check if Insurance/Insurances Present for Patient & has primary insurance is
                //not set then select the 1st Insurance in grid
                if (_HasInsurance == true && _IsPrimaryPresent == false)
                {
                    c1Insurance.SetCellCheck(1, COL_SELECT, CheckEnum.Checked);
                }
                else if (_HasInsurance == false)
                {
                    c1Insurance.SetCellCheck((c1Insurance.Rows.Count - 1), COL_SELECT, CheckEnum.Checked);
                }
                //
                c1Insurance.Cols[COL_SELECT].AllowEditing = true;
                c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].AllowEditing = true;
                c1Insurance.Cols[COL_INSURANCETYPE].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCENAME].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCEID].AllowEditing = false;
                c1Insurance.Cols[COL_INSSELFMODE].AllowEditing = false;

                c1Insurance.Cols[COL_INSURANCECONTACTID].AllowEditing = false;

                //To Remove the Previous Flag
                for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                {
                    c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                }

                //if (c1Insurance.GetData(1, COL_INSURANCEPARTY).ToString() != "\0")
                //{
                //if (_CntPrimary == 1)
                //{
                //    //c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, null);
                //    System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                //    c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, imgFlag);
                //}
                c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                #endregion
                #region Fill Referal Provider
                if (dtRefferalProvider != null && dtRefferalProvider.Rows.Count > 0)
                {
                    FillReferralProvidersData(dtRefferalProvider, true);
                }
                #endregion

                if (nCaseId != 0)
                {
                    #region Fill Modify Case Data
                    //----------------------------Fill Case Data----------------------------------------------
                    // gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);

                    //  string strQuery = "";
                    try
                    {
                        if (dtCases != null && dtCases.Rows.Count > 0)
                        {

                            txtCaseNo.Text = dtCases.Rows[0]["sCaseName"].ToString();

                            if (Convert.ToBoolean(dtCases.Rows[0]["bUpdateDiagnose"]) == true)
                                chkUpdateDiagnosisLstChrg.Checked = true;


                            if (Convert.ToBoolean(dtCases.Rows[0]["bUpdateDiagnose"]) == true)
                                chkUpdateDiagnosisLstChrg.Checked = true;

                            if (Convert.ToBoolean(dtCases.Rows[0]["bISOBPregnancyCase"]) == true)
                                chkOBPregnancyCase.Checked = true;

                            if (dtCases.Rows[0]["dtHopitalizationDateFrom"] != DBNull.Value)
                                mskHospitaliztionFrom.Text = Convert.ToDateTime(dtCases.Rows[0]["dtHopitalizationDateFrom"]).ToString("MM/dd/yyyy");
                            if (dtCases.Rows[0]["dtHopitalizationDateTo"] != DBNull.Value)
                                mskHospitaliztionTo.Text = Convert.ToDateTime(dtCases.Rows[0]["dtHopitalizationDateTo"]).ToString("MM/dd/yyyy");
                            if (dtCases.Rows[0]["dtStartdate"] != DBNull.Value)
                                mskCaseStarDate.Text = Convert.ToDateTime(dtCases.Rows[0]["dtStartdate"]).ToString("MM/dd/yyyy");
                            if (dtCases.Rows[0]["dtEnddate"] != DBNull.Value)
                                mskCaseEndDate.Text = Convert.ToDateTime(dtCases.Rows[0]["dtEnddate"]).ToString("MM/dd/yyyy");
                            if (dtCases.Rows[0]["dtunbaleWorkfrom"] != DBNull.Value)
                                mskUnableFromDate.Text = Convert.ToDateTime(dtCases.Rows[0]["dtunbaleWorkfrom"]).ToString("MM/dd/yyyy");
                            if (dtCases.Rows[0]["dtunbaleWorkto"] != DBNull.Value)
                                mskUnableTillDate.Text = Convert.ToDateTime(dtCases.Rows[0]["dtunbaleWorkto"]).ToString("MM/dd/yyyy");
                            if (dtCases.Rows[0]["sNOte"] != DBNull.Value)
                                txtNote.Text = dtCases.Rows[0]["sNOte"].ToString();

                            if (dtCases.Rows[0]["sAuthorizationNumber"] != DBNull.Value)
                                txtPriorAuthorizationNo.Text = dtCases.Rows[0]["sAuthorizationNumber"].ToString();
                            if (dtCases.Rows[0]["nAuthorizationID"] != DBNull.Value)
                                txtPriorAuthorizationNo.Tag = dtCases.Rows[0]["nAuthorizationID"];
                            if (dtCases.Rows[0]["nFacilityID"] != DBNull.Value)
                                cmbFacility.SelectedValue = dtCases.Rows[0]["nFacilityID"];
                            //if (dtCases.Rows[0]["sFacilityDescription"] != DBNull.Value)
                            //    cmbFacility.Text = dtCases.Rows[0]["sFacilityDescription"].ToString();                       
                            if (dtCases.Rows[0]["nAccidentType"] != DBNull.Value)
                                CmbAccidentType.SelectedIndex = Convert.ToInt16(dtCases.Rows[0]["nAccidentType"].ToString());
                            if (dtCases.Rows[0]["dtInjuryDate"] != DBNull.Value)
                                mskAccidentDate.Text = Convert.ToDateTime(dtCases.Rows[0]["dtInjuryDate"]).ToString("MM/dd/yyyy");
                            if (dtCases.Rows[0]["sWCnumber"] != DBNull.Value)
                                txt_WcAc.Text = dtCases.Rows[0]["swcnumber"].ToString();
                            if (dtCases.Rows[0]["sWCBCaseNo"] != DBNull.Value)
                                txtWCBCaseNo.Text = dtCases.Rows[0]["sWCBCaseNo"].ToString();
                            if (dtCases.Rows[0]["nReferralID"] != DBNull.Value)
                                cmbReferralProvider.SelectedValue = dtCases.Rows[0]["nReferralID"];
                            //if (dtCases.Rows[0]["sReferralDescription"] != DBNull.Value)
                            //cmbReferralProvider.Text = dtCases.Rows[0]["sReferralDescription"].ToString();
                            if (dtCases.Rows[0]["sState"] != DBNull.Value)
                                cmbState.Text = dtCases.Rows[0]["sState"].ToString();
                            //if (dtCases.Rows[0]["sInsuranceName"] != DBNull.Value)
                            //    cmbInsuranceCoverage.Text = dtCases.Rows[0]["sInsuranceName"].ToString();
                            if (dtCases.Rows[0]["nReportCategoryID"] != DBNull.Value)
                                cmbReportingCategory.SelectedValue = dtCases.Rows[0]["nReportCategoryID"].ToString();

                            if (dtCases.Rows[0]["sClaimDateQualifier"] != DBNull.Value )
                            {
                                CmbClaimDateQual.SelectedValue = dtCases.Rows[0]["sClaimDateQualifier"].ToString();
                            }
                            if (dtCases.Rows[0]["dtOtherClaimDate"] != DBNull.Value)
                            {
                                mskOtherClaimDate.Text = Convert.ToDateTime(dtCases.Rows[0]["dtOtherClaimDate"]).ToString("MM/dd/yyyy"); 
                            }
                            if (dtCases.Rows[0]["sOtherClaimDateQualifier"] != DBNull.Value)
                            {
                                cmbOtherClaimDateQual.SelectedValue = dtCases.Rows[0]["sOtherClaimDateQualifier"].ToString();
                            }
                            _DiagnosisCount = dtCasesDiag.Rows.Count;
                            for (int k = 1; k <= dtCasesDiag.Rows.Count; k++)
                            {
                                if (dtCasesDiag.Rows[k - 1]["sdxCode"] != null && dtCasesDiag.Rows[k - 1]["sdxCode"].ToString().Trim().Length > 0)
                                {
                                    c1DiagnosisSelected.Rows.Add();
                                    c1DiagnosisSelected.SetData(k, COL_ID, dtCasesDiag.Rows[k - 1]["nId"]);
                                    c1DiagnosisSelected.SetData(k, COL_CODE, dtCasesDiag.Rows[k - 1]["sdxCode"]);
                                    c1DiagnosisSelected.SetData(k, COL_DESC, dtCasesDiag.Rows[k - 1]["sDxDescription"]);
                                }
                            }

                            bool _IsFound = false;

                            c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                            for (int k = 0; k < dtCasesIns.Rows.Count; k++)
                            {
                                for (int i = 1; i < c1Insurance.Rows.Count; i++)
                                {
                                    if (dtCasesIns.Rows[k]["nInsuranceId"].ToString() == c1Insurance.GetData(i, COL_INSURANCEID).ToString())
                                    {
                                        _IsFound = true;
                                        c1Insurance.SetData(i, COL_INSURANCEPARTY, Convert.ToChar((dtCasesIns.Rows[k]["nResponsibilityNumber"].ToString())));
                                        c1Insurance.SetData(i, COL_INSURANCERESPONSIBILITY, Convert.ToChar((dtCasesIns.Rows[k]["nResponsibilityNumber"].ToString())));
                                        c1Insurance.SetData(i, COL_IndID, (dtCasesIns.Rows[k]["nID"].ToString()));
                                        break;
                                    }

                                }
                                #region "Add Insurance Plan if Inactivate"

                                if (_IsFound == false)
                                {
                                    c1Insurance.Rows.Add();
                                    int rowIndex = c1Insurance.Rows.Count - 1;
                                    //Select-CheckBox
                                    //Select-CheckBox
                                    c1Insurance.SetData(rowIndex, COL_SELECT, false);//Select-CheckBox
                                    c1Insurance.SetData(rowIndex, COL_INSURANCEID, dtCasesIns.Rows[k]["nInsuranceId"]); //=                                
                                    //c1Insurance.SetData(rowIndex, COL_INSSELFMODE, _Transaction.InsurancePlans[i].ResponsibilityType); //
                                    c1Insurance.SetData(rowIndex, COL_INSURANCENAME, dtCasesIns.Rows[k]["sInsuranceName"]);
                                    c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, "Inactive");
                                    c1Insurance.SetData(rowIndex, COL_IndID, (dtCasesIns.Rows[k]["nID"]));
                                    c1Insurance.SetData(rowIndex, COL_INSURANCEPARTY, Convert.ToChar((dtCasesIns.Rows[k]["nResponsibilityNumber"].ToString())));
                                    c1Insurance.SetData(rowIndex, COL_INSURANCERESPONSIBILITY, Convert.ToChar((dtCasesIns.Rows[k]["nResponsibilityNumber"].ToString())));

                                }
                                _IsFound = false;
                                #endregion "Add Insurance Plan if Inactivate"
                            }
                            c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                            ReorderInsurance();
                            FillClaim_Charges();
                        }
                    }
                    catch (gloDatabaseLayer.DBException dbex)
                    {
                        dbex.ERROR_Log(dbex.ToString());

                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        dtCases.Dispose();

                    }
                    #endregion
                }
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());

            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtReportingCAtegory != null) { dtReportingCAtegory.Dispose(); dtReportingCAtegory = null; }
                if (dtPatientName != null) { dtPatientName.Dispose(); dtPatientName = null; }
                if (dtPatientInsurances != null) { dtPatientInsurances.Dispose(); dtPatientInsurances = null; }
                if (dtCases != null) { dtCases.Dispose(); dtCases = null; }
                if (dtCasesDiag != null) { dtCasesDiag.Dispose(); dtCasesDiag = null; }
                if (dtCasesIns != null) { dtCasesIns.Dispose(); dtCasesIns = null; }
                if (dtRefferalProvider != null) { dtRefferalProvider.Dispose(); dtRefferalProvider = null; }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
            #endregion
        }
        // code added for icd validation on cases  Sameer 02142014

        private Boolean ICDValidation()
        {

            string _dxcode = "";
            string _dxdesc = "";
            Boolean  IsICD10Validate = true;
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            if (c1DiagnosisSelected != null && c1DiagnosisSelected.Rows.Count > 1)
            {

                int nICDRevision = (int)gloGlobal.gloICD.CodeRevision.ICD9;
                if (rbICD10.Checked == true)
                    nICDRevision = (int)gloGlobal.gloICD.CodeRevision.ICD10;
                else if (rbICD9.Checked == true)
                    nICDRevision = (int)gloGlobal.gloICD.CodeRevision.ICD9;

                for (int i = 1; i < c1DiagnosisSelected.Rows.Count; i++)
                {
                    _dxcode = "";
                    _dxdesc = "";

                    _dxcode = Convert.ToString(c1DiagnosisSelected.GetData(i, COL_CODE));
                    _dxdesc = Convert.ToString(c1DiagnosisSelected.GetData(i, COL_DESC));


                    if (_dxcode != "")
                    {
                        if (ogloBilling.CheckDxStatus(_dxcode.Trim(), nICDRevision) == 0)
                        {
                            //if (rbICD10.Checked)
                            //{
                            MessageBox.Show("ICD Type Mismatch", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //else
                            //{
                            //    MessageBox.Show("ICD 9 codes cannot be mixed with ICD 10 codes.\nPlease remove ICD 10 codes before saving.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            IsICD10Validate = false;
                            break;
                        }
                        else
                        {
                            IsICD10Validate = true;

                        }

                    }


                }

            }
            if (ogloBilling != null)
            {
                ogloBilling.Dispose();
                ogloBilling = null;
            }

            return IsICD10Validate;
        }

        private bool ValidateDate()
        {
            if (IsValidDate(mskUnableFromDate.Text) && IsValidDate(mskUnableTillDate.Text))
            {
                if (Convert.ToDateTime(mskUnableFromDate.Text).Date > Convert.ToDateTime(mskUnableTillDate.Text).Date)
                {
                    MessageBox.Show("Unable to Work From Date cannot be greater than Unable to Work Till Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskUnableFromDate.Focus();
                    mskUnableFromDate.Select();
                    return false;

                }
            }

            if (IsValidDate(mskCaseStarDate.Text) && IsValidDate(mskCaseEndDate.Text))
            {
                if (Convert.ToDateTime(mskCaseStarDate.Text).Date > Convert.ToDateTime(mskCaseEndDate.Text).Date)
                {
                    MessageBox.Show("Case Start Date cannot be greater than Case End Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskUnableFromDate.Focus();
                    mskUnableFromDate.Select();
                    return false;

                }
            }
            if (IsValidDate(mskHospitaliztionFrom.Text) && IsValidDate(mskHospitaliztionTo.Text))
            {
                if (Convert.ToDateTime(mskHospitaliztionFrom.Text).Date > Convert.ToDateTime(mskHospitaliztionTo.Text).Date)
                {
                    MessageBox.Show("Hospitalization from Date cannot be greater than Hospitalization to Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskHospitaliztionFrom.Focus();
                    mskHospitaliztionFrom.Select();
                    return false;
                }
            }

            #region " Insurance responsibility validation "

            if (c1Insurance.Rows.Count > 1)
            {

                Boolean _IsFirstPartySelected = false;
                Int32 _CntInsurance = 0;


                #region " Check for multiple responsibility "
                bool _isOrderset = false;
                for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                {
                    Int16 _Party = 0;
                    if (Int16.TryParse(c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY).ToString(), out _Party) == true)
                    {
                        _isOrderset = true;
                        if (_Party > 0)
                        {

                            if (c1Insurance.GetData(i, COL_INSSELFMODE) != null && c1Insurance.GetData(i, COL_INSSELFMODE).ToString() != "")
                            {
                                if (PayerMode.Self == (PayerMode)Convert.ToInt16(c1Insurance.GetData(i, COL_INSSELFMODE)))
                                { _CntInsurance = i; }
                            }

                            if (_Party == 1)
                            { _IsFirstPartySelected = true; }

                            for (int j = i + 1; j <= c1Insurance.Rows.Count - 1; j++)
                            {
                                Int16 _NextParty = 0;
                                if (Int16.TryParse(c1Insurance.GetData(j, COL_INSURANCERESPONSIBILITY).ToString(), out _NextParty) == true)
                                {
                                    if (_Party == _NextParty)
                                    {
                                        MessageBox.Show("Same responsibility is found for multiple Insurances. Please assign unique responsibility.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1Insurance.Select(i, COL_INSURANCERESPONSIBILITY);
                                        c1Insurance.Focus();
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion " Check for multiple responsibility"

                if (_isOrderset == true)
                {
                    #region " Check is first party selected "

                    if (_IsFirstPartySelected == false)
                    {
                        MessageBox.Show("Please specify first responsibility. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Insurance.Select(1, COL_INSURANCERESPONSIBILITY);
                        c1Insurance.Focus();

                        return false;
                    }

                    #endregion " Check is first party selected "

                    #region " Check is responsibility in order "

                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        Int16 _Party = 0;
                        if (c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY) != null && c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY).ToString().Length > 0)
                        {
                            if (Int16.TryParse(c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY).ToString(), out _Party) == true)
                            {
                                if (_Party != i)
                                {
                                    MessageBox.Show("Responsibility is out of order. Please specify responsibility in sequence. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1Insurance.Select(i, COL_INSURANCERESPONSIBILITY);
                                    c1Insurance.Focus();

                                    return false;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }
                    }

                    #endregion " Check is responsibility in order "

                    #region " Check Self is Last responsibility "

                    if (_CntInsurance > 0)
                    {
                        if (c1Insurance.Rows.Count > _CntInsurance + 1)
                        {
                            Int16 _Party = 0;
                            if (c1Insurance.GetData(_CntInsurance + 1, COL_INSURANCERESPONSIBILITY) != null && c1Insurance.GetData(_CntInsurance + 1, COL_INSURANCERESPONSIBILITY).ToString().Length > 0)
                            {
                                if (Int16.TryParse(c1Insurance.GetData(_CntInsurance + 1, COL_INSURANCERESPONSIBILITY).ToString(), out _Party) == true)
                                {
                                    if (_Party > _CntInsurance)
                                    {
                                        MessageBox.Show("You can not specify responsibility after self. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1Insurance.Select(_CntInsurance + 1, COL_INSURANCERESPONSIBILITY);
                                        c1Insurance.Focus();

                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    #endregion " Check Self is Last responsibility "

                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        if (PayerMode.Self == (PayerMode)Convert.ToInt16(c1Insurance.GetData(i, COL_INSSELFMODE)))
                        {
                            if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") == "")
                            {
                                MessageBox.Show("Self responsibility cannot be blank.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1Insurance.Select(i, COL_INSURANCERESPONSIBILITY);
                                c1Insurance.Focus();
                                return false;
                            }
                        }

                    }
                }

            }

            #endregion " Insurance responsibility validation "

              mskOtherClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
              if (mskOtherClaimDate.Text != "")
              {
                  mskOtherClaimDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                  if (Convert.ToString(cmbOtherClaimDateQual.SelectedValue) == "")
                  {
                      MessageBox.Show("Please select the Other Claim Date Qualifier.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                      cmbOtherClaimDateQual.Focus();
                      cmbOtherClaimDateQual.Select();
                      return false;

                  }
              }
            return true;
        }
        private void FillClaim_Charges()
        {


            DataView _dv = null;
            int cntr = 0;
            DataSet dsClaims_Charges = new DataSet();
            try
            {
                c1ClaimGrid.Cols[c1ClaimGrid.Cols["sPatientName"].Index].Visible = false;

                oclsCaseSetup.GetCasesClaimsNCharges(this.nCaseId, this._PatientID, out dsClaims_Charges);

                if (dsClaims_Charges.Tables["Claims_Charges"].Rows.Count > 0)
                {
                    gloGlobal.gloPMGlobal.SplitClaimColumn(dsClaims_Charges.Tables["Claims_Charges"], dsClaims_Charges.Tables["Claims_Charges"].Columns.IndexOf("SplitClaimNumber"));

                    _dv = dsClaims_Charges.Tables["Claims_Charges"].DefaultView;

                    //_dv.Sort = "SortOrder DESC, nTransactionLineNo";

                    _dv.Sort = "SortClaim Desc,SortSubClaim ASC, nTransactionLineNo";
                    c1ClaimGrid.DataSource = _dv;

                    c1ClaimGrid.Refresh();



                    for (cntr = 0; cntr < (c1ClaimGrid.Rows.Count - 1); cntr++)
                    {
                        if (Convert.ToBoolean(Convert.ToString(c1ClaimGrid.GetData(cntr + 1, c1ClaimGrid.Cols["blnNoteFlag"].Index)) == "" ? 0 : c1ClaimGrid.GetData(cntr + 1, c1ClaimGrid.Cols["blnNoteFlag"].Index)))
                        {
                            System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
                            this.c1ClaimGrid.SetCellImage(cntr + 1, COL_NOTE_IMAGE, imgFlag);
                        }
                        if (c1ClaimGrid.Rows.Count >= iChargesSelRow)
                            c1ClaimGrid.Row = iChargesSelRow;
                        else if (dsClaims_Charges.Tables["Claims_Charges"].Rows.Count > 1)
                            c1ClaimGrid.Row = 1;


                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                this.c1ClaimGrid.Rows[0].Visible = false;

            }
            finally
            {

            }

        }
        private void ReorderInsurance()
        {
            c1Insurance.Sort(SortFlags.Ascending, COL_INSURANCENAME);
            c1Insurance.Sort(SortFlags.Ascending, COL_INSURANCETYPE);
            c1Insurance.Sort(SortFlags.Descending, COL_INSSELFMODE);
            c1Insurance.Sort(SortFlags.Ascending, COL_INSURANCEPARTY);
        }
        private void ChangeInsuranceGridColor()
        {
            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
            {
                for (int i = 1; i < c1Insurance.Rows.Count; i++)
                {
                    C1.Win.C1FlexGrid.CellStyle cStyle;// = c1Insurance.Styles.Add("BubbleValues");
                    try
                    {
                        if (c1Insurance.Styles.Contains("BubbleValues"))
                        {
                            cStyle = c1Insurance.Styles["BubbleValues"];
                        }
                        else
                        {
                            cStyle = c1Insurance.Styles.Add("BubbleValues");
                            cStyle.BackColor = Color.White;

                        }

                    }
                    catch
                    {
                        cStyle = c1Insurance.Styles.Add("BubbleValues");
                        cStyle.BackColor = Color.White;

                    }


                    C1.Win.C1FlexGrid.CellRange rgBubbleValues = c1Insurance.GetCellRange(i, COL_INSURANCERESPONSIBILITY);
                    rgBubbleValues.Style = cStyle;
                }
            }
        }
        private void SetInsuranceResponsibility(int CntPrimary)
        {
            c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);

            for (int i = 1; i < c1Insurance.Rows.Count; i++)
            {
                if (CntPrimary > 1)
                {
                    c1Insurance.SetData(i, COL_INSURANCEPARTY, Convert.ToChar("X"));
                    c1Insurance.SetData(i, COL_INSURANCERESPONSIBILITY, Convert.ToString(""));

                }
                else
                {
                    c1Insurance.SetData(i, COL_INSURANCEPARTY, Convert.ToChar(i.ToString()));
                    c1Insurance.SetData(i, COL_INSURANCERESPONSIBILITY, Convert.ToChar(i.ToString()));

                }
            }
            c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);


        }
        private Boolean ModifyCharge()
        {
            Boolean _IsModified = false;

            try
            {
                gloAccountsV2.gloPatientFinancialViewV2 objPatFinacialView = new gloAccountsV2.gloPatientFinancialViewV2(this._PatientID);
                Int64 ParamTransactionId = 0;
                Int64 PatientID = 0;
                //Int64 _ParamClaimNo = 0;
                gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, "");

                if (c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["nTransactionMSTID"].Index) != null && Convert.ToString(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["nTransactionMSTID"].Index)) != "")
                {
                    DataSet dsPatFinView = new DataSet();
                    if (Convert.ToInt32(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-")) >= 0)
                    {
                        if (Convert.ToInt32(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["bIsVoid"].Index)) == 1)
                        {

                            ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index).ToString().Substring(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), true);
                            PatientID = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["PatientID"].Index));
                            _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, true, true, this);
                        }
                        else
                        {
                            ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index).ToString().Substring(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), false);

                            PatientID = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["PatientID"].Index));
                            if (ParamTransactionId != 0)
                                _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, false, true, this);
                        }

                    }
                    else
                    {

                        if (Convert.ToInt32(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["bIsVoid"].Index)) == 1)
                        {
                            ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index)), "", true);
                            //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", true);
                            PatientID = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["PatientID"].Index));
                            _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, true, true, this);
                        }
                        else
                        {
                            ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["SplitClaimNumber"].Index)), "", false);
                            //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", false);
                            PatientID = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["PatientId"].Index));
                            if (ParamTransactionId != 0)
                                _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, false, true, this);
                        }
                    }

                }
                ogloBilling.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            return _IsModified;
        }
        private void setGridStyle(C1FlexGrid C1Flex, Int32 iRowNumber, Int32 iColNumber, Int32 iColCount)
        {
            CellStyle csSubTotalRow;
            CellStyle csSubCol;
            //  csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
            try
            {
                if (C1Flex.Styles.Contains("SubTotalRow"))
                {
                    csSubTotalRow = C1Flex.Styles["SubTotalRow"];
                }
                else
                {
                    csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                    //csSubTotalRow.DataType = typeof(System.Decimal);
                    csSubTotalRow.Format = "c";
                    csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                    csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                    csSubTotalRow.ForeColor = Color.Blue;
                    csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;

                }

            }
            catch
            {
                csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                //csSubTotalRow.DataType = typeof(System.Decimal);
                csSubTotalRow.Format = "c";
                csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                csSubTotalRow.ForeColor = Color.Blue;
                csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;

            }
            //   csSubCol = C1Flex.Styles.Add("SubCol");
            try
            {
                if (C1Flex.Styles.Contains("SubCol"))
                {
                    csSubCol = C1Flex.Styles["SubCol"];
                }
                else
                {
                    csSubCol = C1Flex.Styles.Add("SubCol");
                    csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                    csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                    csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubCol.TextEffect = TextEffectEnum.Flat;
                    csSubCol.ForeColor = Color.Maroon;

                }

            }
            catch
            {
                csSubCol = C1Flex.Styles.Add("SubCol");
                csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubCol.TextEffect = TextEffectEnum.Flat;
                csSubCol.ForeColor = Color.Maroon;

            }


            CellRange subTotalRange;
            subTotalRange = C1Flex.GetCellRange(iRowNumber, 0, iRowNumber, iColCount);
            subTotalRange.Style = csSubTotalRow;
            CellRange subCol;
            subCol = C1Flex.GetCellRange(iRowNumber, iColNumber, iRowNumber, iColNumber);
            subCol.Style = csSubCol;
        }
        private void DesignInsuranceGrid()
        {

            try
            {
                c1Insurance.BeginUpdate();

                c1Insurance.Cols.Count = COL_INSVIEW_COUNT;
                c1Insurance.Rows.Count = 1;
                c1Insurance.SetData(0, COL_SELECT, "Select");
                c1Insurance.SetData(0, COL_INSURANCERESPONSIBILITY, "Party");
                c1Insurance.SetData(0, COL_INSURANCEID, "InsuranceID");
                c1Insurance.SetData(0, COL_INSURANCENAME, "Insurance");
                c1Insurance.SetData(0, COL_INSURANCETYPE, "Type");
                c1Insurance.SetData(0, COL_INSSELFMODE, "Mode");
                c1Insurance.SetData(0, COL_SUBSCRIBERID, "Insurance ID");

                c1Insurance.Cols[COL_SUBSCRIBERID].Visible = true;

                c1Insurance.SetData(0, COL_INSURANCEWORKERCOMP, "Workers Comp");
                c1Insurance.SetData(0, COL_INSURANCEAUTOCLAIM, "Auto Claim");
                c1Insurance.SetData(0, COL_INSURANCECURRRESP, "Resp.");
                c1Insurance.SetData(0, COL_INSURANCEPLANONHOLD, "IsPlanOnHold");
                c1Insurance.Cols[COL_INSURANCEWORKERCOMP].DataType = typeof(System.Boolean);
                c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].DataType = typeof(System.Char);
                c1Insurance.Cols[COL_INSURANCEPARTY].DataType = typeof(System.Char);
                c1Insurance.Cols[COL_INSURANCECURRRESP].DataType = typeof(System.Char);
                c1Insurance.Cols[COL_INSURANCEPLANONHOLD].DataType = typeof(System.Boolean);
                c1Insurance.Cols[COL_IndID].DataType = typeof(System.Int64);

                int nWidth;
                nWidth = pnlPatientInsurence.Width;

                c1Insurance.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");//Select Column
                c1Insurance.Cols[COL_SELECT].AllowEditing = true;
                c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].AllowEditing = true;

                c1Insurance.Cols[COL_INSURANCEWORKERCOMP].AllowEditing = true;
                c1Insurance.Cols[COL_INSURANCEWORKERCOMP].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCECURRRESP].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCEPLANONHOLD].AllowEditing = false;
                c1Insurance.Cols[COL_SUBSCRIBERID].AllowEditing = false;
                c1Insurance.Cols[COL_IndID].Visible = false;
                c1Insurance.Cols[COL_IndID].Width = 0;
                bool _designWidth = false;
                try
                {
                    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_DatabaseConnectionString);
                    _designWidth = oSetting.LoadGridColumnWidth(c1Insurance, gloSettings.ModuleOfGridColumn.Billing, gloGlobal.gloPMGlobal.UserID);
                    oSetting.Dispose();
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                }

                if (_designWidth == false)
                {
                    c1Insurance.Cols[COL_SELECT].Width = Convert.ToInt32(nWidth * 0.09);
                    c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].Width = Convert.ToInt32(nWidth * 0.09);
                    c1Insurance.Cols[COL_INSURANCENAME].Width = 280;// Convert.ToInt32(nWidth * 0.30);
                    c1Insurance.Cols[COL_INSURANCEID].Width = 0;
                    c1Insurance.Cols[COL_INSURANCETYPE].Width = Convert.ToInt32(nWidth * 0.12);
                    c1Insurance.Cols[COL_INSSELFMODE].Width = 0;
                    c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Width = Convert.ToInt32(nWidth * 0.18);
                    c1Insurance.Cols[COL_INSURANCEAUTOCLAIM].Width = Convert.ToInt32(nWidth * 0.14);
                    c1Insurance.Cols[COL_INSURANCEPARTY].Width = 20;
                    c1Insurance.Cols[COL_INSURANCEPLANONHOLD].Width = 0;
                    c1Insurance.Cols[COL_SUBSCRIBERID].Width = 100;

                }
                else
                {

                    c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].Width = 55;
                    c1Insurance.Cols[COL_INSURANCENAME].Width = 280;
                    c1Insurance.Cols[COL_INSURANCETYPE].Width = 73;
                    c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Width = 98;
                    c1Insurance.Cols[COL_INSURANCEPLANONHOLD].Width = 0;
                    c1Insurance.Cols[COL_SUBSCRIBERID].Width = 100;
                }

                c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].TextAlign = TextAlignEnum.RightCenter;
                c1Insurance.Cols[COL_SELECT].Visible = false;
                c1Insurance.Cols[COL_INSURANCEID].Visible = false;
                c1Insurance.Cols[COL_INSSELFMODE].Visible = false;

                c1Insurance.Cols[COL_INSURANCEAUTOCLAIM].Visible = false;
                c1Insurance.Cols[COL_INSURANCECONTACTID].Visible = false;
                c1Insurance.Cols[COL_INSURANCEPARTY].Visible = false;
                c1Insurance.Cols[COL_INSURANCECURRRESP].Visible = false;
                c1Insurance.Cols[COL_INSURANCEPLANONHOLD].Visible = false;
                c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].Visible = true;
                c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Visible = false;
                c1Insurance.Cols[COL_INSURANCETYPE].Visible = true;
                c1Insurance.Cols[COL_SUBSCRIBERID].TextAlign = TextAlignEnum.LeftCenter;
                c1Insurance.Cols[COL_INSURANCENAME].TextAlign = TextAlignEnum.LeftCenter;
                c1Insurance.Cols[COL_INSURANCEWORKERCOMP].TextAlignFixed = TextAlignEnum.CenterCenter;



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                throw;
            }
            finally
            {
                c1Insurance.EndUpdate();
            }
        }
        private void SetNoteFlag()
        {
            bool isNoteFlag = false;


            foreach (Row myRow in c1ClaimGrid.Rows)
            {
                isNoteFlag = false;

                if (myRow.Index == 0)
                { continue; }

                object noteFlag = c1ClaimGrid.GetData(myRow.Index, c1ClaimGrid.Cols.IndexOf("blnNoteFlag"));

                if (!noteFlag.Equals(System.DBNull.Value))
                {
                    if (Convert.ToBoolean(c1ClaimGrid.GetData(myRow.Index, c1ClaimGrid.Cols.IndexOf("blnNoteFlag"))))
                    { isNoteFlag = true; }
                }

                if (isNoteFlag)
                {
                    // Set the Note Image
                    System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
                    this.c1ClaimGrid.SetCellImage(myRow.Index, COL_NOTE_IMAGE, imgFlag);
                }
                else
                {
                    // Clear the Note Image
                    System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.None;
                    this.c1ClaimGrid.SetCellImage(myRow.Index, COL_NOTE_IMAGE, null);
                }

                noteFlag = null;
            }

            if (c1ClaimGrid.Rows.Count > 1)
            {
                if (c1ClaimGrid.Rows.Count >= iChargesSelRow)
                {
                    c1ClaimGrid.Row = iChargesSelRow;
                }
            }

        }
        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g != null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
                g = null;
            }

            return width;
        }

        #endregion

        # region "Form Events"

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
                    OpenInternalControl(gloGridListControlType.ICD9, "Diagnosis", false, 0, 100, "");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                ex = null;
                throw;
            }
        }
        private void btnDeleteDx_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1DiagnosisSelected.Rows.Count > 1)
                {
                    c1DiagnosisSelected.Rows.Remove(c1DiagnosisSelected.Row);
                    _DiagnosisCount--;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                ex = null;
            }
        }
        private void c1Diagnosis_ChangeEdit(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    string _strSearchString = c1Diagnosis.Editor.Text;
                    ogloGridListControl.FillControl(_strSearchString);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                ex = null;
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
                ex.ToString();
                ex = null;
                throw;
            }
        }
        private void c1DiagnosisSelected_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }
        private void CmbAccidentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mskAccidentDate.Visible = true;
            mskAccidentDate.Text = null;
            txt_WcAc.Text = "";
            txt_WcAc.Tag = "";
            lblWorkComp.Visible = false;
            lblWorkerCompMandatory.Visible = false;
            lblStateMandetory.Visible = false;
            txtWCBCaseNo.Text = "";

            if (CmbAccidentType.SelectedIndex == 1)
            {
                lblInjuryDate.Visible = true;
                lblOnsiteDate.Visible = false;
                lblOtherDate.Visible = false;
                lblAccidentDate.Visible = false;

                lblDateMandatory.Visible = true;
                lblDateMandatory.Location = new Point(lblOnsiteDate.Location.X - 20, lblOnsiteDate.Location.Y);
                lbl_State.Visible = false;
                cmbState.Visible = false;
                cmbState.Enabled = false;
                txt_WcAc.Visible = true;
                lblClaim.Visible = true;
                lblWorkComp.Visible = true;
                lblWorkerCompMandatory.Visible = false;
                lblAutoMan.Visible = false;
                txt_WcAc.Focus();
                CmbClaimDateQual.Visible = false;
                if (bIsEnableWorkersCompForms)
                {
                    lblWCBCaseNo.Visible = true;
                    txtWCBCaseNo.Visible = true;
                }

            }

            else if (CmbAccidentType.SelectedIndex == 2)
            {
                //SLR: Changed on 4/2/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }

                txt_WcAc.Text = "";
                txt_WcAc.Tag = "";

                if (CmbAccidentType.SelectedIndex == 2)
                {


                    lblInjuryDate.Visible = false;
                    lblOnsiteDate.Visible = false;
                    lblOtherDate.Visible = false;
                    lblAccidentDate.Visible = true;
                    lblDateMandatory.Location = new Point(lblAccidentDate.Location.X - 12, lblAccidentDate.Location.Y);
                    lblDateMandatory.Visible = true;
                    cmbState.Visible = true;
                    lbl_State.Visible = true;

                    cmbState.Enabled = true;
                    cmbState.Focus();

                    txt_WcAc.Visible = true;
                    lblClaim.Visible = true;

                    lblClaim.Text = "Auto Claim # :";
                    lblAutoMan.Visible = false;
                    if (nCaseId == 0)
                        lblStateMandetory.Visible = true;
                    else
                        lblStateMandetory.Visible = false;
                    txt_WcAc.Focus();

                    lblWCBCaseNo.Visible = false;
                    txtWCBCaseNo.Visible = false;
                    CmbClaimDateQual.Visible = false;
                }

            }
            else if (CmbAccidentType.SelectedIndex == 3)
            {

                CloseInternalControl();

                txt_WcAc.Text = "";

                if (CmbAccidentType.SelectedIndex == 3)
                {


                    lblInjuryDate.Visible = false;
                    lblOnsiteDate.Visible = false;
                    lblOtherDate.Visible = true;
                    lblAccidentDate.Visible = false;
                    lblDateMandatory.Visible = true;
                    lblDateMandatory.Location = new Point(lblOtherDate.Location.X - 12, lblOtherDate.Location.Y);

                    txt_WcAc.Visible = false;
                    lbl_State.Visible = false;
                    cmbState.Visible = false;
                    cmbState.Enabled = false;

                    lblClaim.Visible = false;

                    lblAutoMan.Visible = false;

                    lblWCBCaseNo.Visible = false;
                    txtWCBCaseNo.Visible = false;
                    CmbClaimDateQual.Visible = false;
                }
            }

            else
            {

                lblDateMandatory.Visible = false;
                lblInjuryDate.Visible = false;
                lblOnsiteDate.Visible = true;
                lblOtherDate.Visible = false;
                lblAccidentDate.Visible = false;
                cmbState.Visible = false;
                cmbState.Enabled = false;
                lbl_State.Visible = false;

                txt_WcAc.Visible = false;
                lblClaim.Visible = false;
                lblAutoMan.Visible = false;

                lblWCBCaseNo.Visible = false;
                txtWCBCaseNo.Visible = false;
                CmbClaimDateQual.Visible = true; 

            }

        }
        private void btnAdd_PriorAuthorization_Click(object sender, EventArgs e)
        {
            if (_PatientID > 0)
            {
                if (oclsCaseSetup.CheckPriorAuthorization(_PatientID) == true)
                {
                    if (txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag).Trim() != "")
                    {
                        gloPMGeneral.frmShowPriorAuthorization oPriorAuthorization = new gloPMGeneral.frmShowPriorAuthorization(_DatabaseConnectionString, gloDateMaster.gloDate.DateAsNumber("09/20/2011"), Convert.ToInt64(txtPriorAuthorizationNo.Tag), _PatientID);
                        try
                        {
                            oPriorAuthorization.ShowDialog(this);
                            if (oPriorAuthorization.CurrentPriorAuthorization.ToString().Trim() != "0")
                            {
                                ReloadPatientRefferals(_PatientID);
                                txtPriorAuthorizationNo.Tag = oPriorAuthorization.CurrentPriorAuthorization;
                                txtPriorAuthorizationNo.Text = oPriorAuthorization.CurrentPriorAuthorizationNo;
                                cmbReferralProvider.SelectedValue = oPriorAuthorization.CurrentReferralProviderID;
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                        }
                        finally
                        {
                            oPriorAuthorization.Dispose();
                        }
                    }

                    else
                    {
                        gloPMGeneral.frmShowPriorAuthorization oPriorAuthorization = new gloPMGeneral.frmShowPriorAuthorization(_DatabaseConnectionString, gloDateMaster.gloDate.DateAsNumber(System.DateTime.Today.ToShortDateString()), 0, _PatientID);
                        try
                        {
                            oPriorAuthorization.ShowDialog(this);
                            if (oPriorAuthorization.CurrentPriorAuthorization.ToString().Trim() != "0")
                            {
                                ReloadPatientRefferals(_PatientID);
                                //chk_SameasBillingProvider.Checked = false;

                                txtPriorAuthorizationNo.Tag = oPriorAuthorization.CurrentPriorAuthorization;
                                txtPriorAuthorizationNo.Text = oPriorAuthorization.CurrentPriorAuthorizationNo;
                                cmbReferralProvider.SelectedValue = oPriorAuthorization.CurrentReferralProviderID;

                            }
                            this.PatientHasPriorAuthorization = oclsCaseSetup.CheckPriorAuthorization(_PatientID);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                        }
                        finally
                        {
                            oPriorAuthorization.Dispose();
                        }

                    }
                }
                else
                {
                    gloPMGeneral.frmSetupAuthorization objsetupauth = new gloPMGeneral.frmSetupAuthorization(_PatientID);
                    try
                    {
                        objsetupauth.ShowDialog(this);
                        Int64 priorAuthID = objsetupauth._PriorAuthorizationID;
                        if (priorAuthID.ToString().Trim() != "" && priorAuthID != 0)
                        {
                            ReloadPatientRefferals(_PatientID);
                            txtPriorAuthorizationNo.Tag = priorAuthID;
                            txtPriorAuthorizationNo.Text = objsetupauth._PriorAuthorizationNo.ToString().Trim();
                            cmbReferralProvider.SelectedValue = objsetupauth._ReferralID;
                        }

                        this.PatientHasPriorAuthorization = oclsCaseSetup.CheckPriorAuthorization(_PatientID);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                    }
                    finally
                    {
                        objsetupauth.Dispose();
                    }
                }

                if (txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag).Trim() != "")
                    if (Convert.ToInt64(txtPriorAuthorizationNo.Tag) > 0)
                    {
                        if (!oclsCaseSetup.getPriorAuthorizationStatus(Convert.ToInt64(txtPriorAuthorizationNo.Tag)))
                        {
                            txtPriorAuthorizationNo.Tag = null;
                            txtPriorAuthorizationNo.Text = "";
                            CheckForPatientPriorAuthorization();
                        }
                    }
                CheckForPatientPriorAuthorization();
            }
            else
            {
                MessageBox.Show("No Patient is selected.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void tlsSaveClose_Click(object sender, EventArgs e)
        {
            tls_SetupResource.Select();                    
            if (!_IsValidate)
                return;
            if (ValidateDate() && ICDValidation())
            {
                DataTable dtCases = new DataTable();
                DataTable dtCasesIns = new DataTable();
                DataTable dtCasesDiag = new DataTable();
                dtCases.Columns.Add("nCaseId");
                dtCases.Columns.Add("nPatientId");
                dtCases.Columns.Add("sCaseName");

                dtCases.Columns.Add("bUpdateDiagnose");
                dtCases.Columns.Add("dtHopitalizationDateTo");
                dtCases.Columns.Add("dtHopitalizationDateFrom");
                dtCases.Columns.Add("nInsuranceId");
                dtCases.Columns.Add("sInsuranceName");
                dtCases.Columns.Add("nAuthorizationID");
                dtCases.Columns.Add("sAuthorizationNumber");
                dtCases.Columns.Add("dtStartdate");
                dtCases.Columns.Add("dtEnddate");
                dtCases.Columns.Add("nFacilityID");
                //  dtCases.Columns.Add("sFacilityDescription");
                dtCases.Columns.Add("nReferralID");
                // dtCases.Columns.Add("sReferralDescription");
                dtCases.Columns.Add("nAccidentType");
                dtCases.Columns.Add("dtInjuryDate");
                dtCases.Columns.Add("sWCnumber");
                dtCases.Columns.Add("dtunbaleWorkfrom");
                dtCases.Columns.Add("dtunbaleWorkto");
                dtCases.Columns.Add("nAutoClaimNo");
                dtCases.Columns.Add("sState");
                dtCases.Columns.Add("nReportCategoryId");
                // dtCases.Columns.Add("sReportCategoryDec");
                dtCases.Columns.Add("sNote");
                dtCases.Columns.Add("dtCreatedDateTime");
                dtCases.Columns.Add("dtModifiedDateTime");
                dtCases.Columns.Add("nCreatedUserID");
                dtCases.Columns.Add("nModifyUserID");
                dtCases.Columns.Add("nICDRevision");
                dtCases.Columns.Add("bISOBPregnancyCase");
                dtCases.Columns.Add("sWCBCaseNo");
                dtCases.Columns.Add("sClaimDateQualifier");
                dtCases.Columns.Add("dtOtherClaimDate");
                dtCases.Columns.Add("sOtherClaimDateQualifier");
                dtCases.Rows.Add();

                dtCasesDiag.Columns.Add("nId");
                dtCasesDiag.Columns.Add("nCaseId");
                dtCasesDiag.Columns.Add("sDxCode");
                dtCasesDiag.Columns.Add("sDxDescription");
                dtCasesDiag.Columns.Add("nIndex");

                dtCasesIns.Columns.Add("nId");
                dtCasesIns.Columns.Add("nCaseId");
                dtCasesIns.Columns.Add("nInsuranceId");
                dtCasesIns.Columns.Add("sInsuranceName");
                dtCasesIns.Columns.Add("nResponsibilityNumber");

                dtCases.Rows[0]["nCaseId"] = nCaseId;
                dtCases.Rows[0]["nPatientId"] = _PatientID;
                
                if (txtCaseNo.Text.Trim() == "" || txtCaseNo.Text == null)
                {
                    MessageBox.Show("Enter the Case Name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCaseNo.Focus();
                    blnAbortClose = true;
                    return;
                }
                
                else if (oclsCaseSetup.IsCaseExistForPatient(nCaseId, txtCaseNo.Text.Trim(), _PatientID))
                {
                    MessageBox.Show("Case name '" + txtCaseNo.Text.Trim() + "' already exist for the Patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCaseNo.Focus();
                    blnAbortClose = true;
                    return;
                }
                
                else
                {
                    dtCases.Rows[0]["sCaseName"] = txtCaseNo.Text.Trim();
                    _sCaseName = txtCaseNo.Text.Trim();
                }

                for (int k = 1; k < c1DiagnosisSelected.Rows.Count && k <= 4; k++)
                {

                    if (c1DiagnosisSelected.GetData(k, COL_CODE) != null && c1DiagnosisSelected.GetData(k, COL_CODE).ToString().Length > 0)
                    {
                        dtCasesDiag.Rows.Add();
                        dtCasesDiag.Rows[k - 1]["nId"] = c1DiagnosisSelected.GetData(k, COL_ID);
                        dtCasesDiag.Rows[k - 1]["nCaseId"] = nCaseId;
                        dtCasesDiag.Rows[k - 1]["sDxCode"] = Convert.ToString(c1DiagnosisSelected.GetData(k, COL_CODE));
                        dtCasesDiag.Rows[k - 1]["sDxDescription"] = Convert.ToString(c1DiagnosisSelected.GetData(k, COL_DESC));
                        dtCasesDiag.Rows[k - 1]["nIndex"] = k;
                        dtCasesDiag.AcceptChanges();
                    }
                }

                dtCases.Rows[0]["bUpdateDiagnose"] = chkUpdateDiagnosisLstChrg.CheckState.GetHashCode();
                if (IsValidDate(mskHospitaliztionTo.Text))
                    dtCases.Rows[0]["dtHopitalizationDateTo"] = Convert.ToDateTime(mskHospitaliztionTo.Text);
                else
                    dtCases.Rows[0]["dtHopitalizationDateTo"] = null;
                if (IsValidDate(mskHospitaliztionFrom.Text))
                    dtCases.Rows[0]["dtHopitalizationDateFrom"] = Convert.ToDateTime(mskHospitaliztionFrom.Text);
                else
                    dtCases.Rows[0]["dtHopitalizationDateFrom"] = null;

                //  dtCases.Rows[0]["nInsuranceId"] = Convert.ToInt64(cmbInsuranceCoverage.SelectedValue);
                //dtCases.Rows[0]["sInsuranceName"] = cmbInsuranceCoverage.Text;
                if (txtPriorAuthorizationNo.Tag != null && txtPriorAuthorizationNo.Tag.ToString().Trim() != "")
                {
                    dtCases.Rows[0]["nAuthorizationID"] = Convert.ToInt64(txtPriorAuthorizationNo.Tag);
                    dtCases.Rows[0]["sAuthorizationNumber"] = txtPriorAuthorizationNo.Text.Trim();
                }
                if (IsValidDate(mskCaseStarDate.Text))
                    dtCases.Rows[0]["dtStartdate"] = Convert.ToDateTime(mskCaseStarDate.Text);
                if (IsValidDate(mskCaseEndDate.Text))
                {
                    if (Convert.ToDateTime(mskCaseEndDate.Text)>DateTime.Now.Date)
                    {
                        if (DialogResult.No == MessageBox.Show(string.Format("You have entered an end date which is in the future.{0}Do you want to continue?",Environment.NewLine), _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        {
                            mskCaseEndDate.Focus();
                            return;
                        } 
                    }
                }
                if (IsValidDate(mskCaseEndDate.Text))
                    dtCases.Rows[0]["dtEnddate"] = Convert.ToDateTime(mskCaseEndDate.Text);
                if (IsValidDate(mskUnableFromDate.Text))
                    dtCases.Rows[0]["dtunbaleWorkfrom"] = Convert.ToDateTime(mskUnableFromDate.Text);
                if (IsValidDate(mskUnableTillDate.Text))
                    dtCases.Rows[0]["dtunbaleWorkto"] = Convert.ToDateTime(mskUnableTillDate.Text);
                if (Convert.ToString(cmbFacility.SelectedValue) != "")
                    dtCases.Rows[0]["nFacilityID"] = cmbFacility.SelectedValue;
                if (Convert.ToString(cmbReferralProvider.SelectedValue) != "")
                    dtCases.Rows[0]["nReferralID"] = cmbReferralProvider.SelectedValue;
                // dtCases.Rows[0]["sReferralDescription"] = cmbReferralProvider.Text.ToString();
                // dtCases.Rows[0]["sFacilityDescription"] = cmbFacility.Text.ToString();
                if (CmbAccidentType.SelectedIndex == -1)
                {
                    dtCases.Rows[0]["nAccidentType"] = AccidentType.None.GetHashCode();
                }
                if (CmbAccidentType.SelectedIndex == 1)
                {
                    dtCases.Rows[0]["nAccidentType"] = AccidentType.Work.GetHashCode();
                    dtCases.Rows[0]["sClaimDateQualifier"] = DBNull.Value;
                }
                if (CmbAccidentType.SelectedIndex == 2)
                {
                    dtCases.Rows[0]["nAccidentType"] = AccidentType.Auto.GetHashCode();
                    dtCases.Rows[0]["sClaimDateQualifier"] = DBNull.Value;
                }
                if (CmbAccidentType.SelectedIndex == 3)
                {
                    dtCases.Rows[0]["nAccidentType"] = AccidentType.Other.GetHashCode();
                    dtCases.Rows[0]["sClaimDateQualifier"] = DBNull.Value;
                }

                if (IsValidDate(mskAccidentDate.Text))
                {
                    dtCases.Rows[0]["dtInjuryDate"] = Convert.ToDateTime(mskAccidentDate.Text);
                    if (CmbAccidentType.SelectedIndex <=0)
                    {
                        dtCases.Rows[0]["sClaimDateQualifier"] = Convert.ToString(CmbClaimDateQual.SelectedValue);
                    }
                }
                else
                {
                    if (CmbAccidentType.SelectedIndex == 1)
                    {
                        MessageBox.Show("Enter the Injury Date .", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        mskAccidentDate.Focus();
                        return;
                    }
                    else if (CmbAccidentType.SelectedIndex == 2)
                    {
                        MessageBox.Show("Enter the Accident Date .", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        mskAccidentDate.Focus();
                        return;
                    }
                    else if (CmbAccidentType.SelectedIndex == 3)
                    {
                        MessageBox.Show("Enter the Accident Date .", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        mskAccidentDate.Focus();
                        return;
                    }
                    else
                    {

                        dtCases.Rows[0]["sClaimDateQualifier"] = DBNull.Value;
                    }

                    //   dtCases.Rows[0]["dtInjuryDate"] = null;
                }
                //if (txt_WcAc.Text.Trim().Length <= 0 && CmbAccidentType.SelectedIndex > 0 && CmbAccidentType.SelectedIndex<3)
                //{
                //    MessageBox.Show("Enter the Claim # .", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    txt_WcAc.Focus();
                //    return;
                //    //   dtCases.Rows[0]["dtInjuryDate"] = null;
                //}
                //else
                if (IsValidDate(mskOtherClaimDate.Text))
                {
                    dtCases.Rows[0]["dtOtherClaimDate"] = Convert.ToDateTime(mskOtherClaimDate.Text);
                    dtCases.Rows[0]["sOtherClaimDateQualifier"] = Convert.ToString(cmbOtherClaimDateQual.SelectedValue);
                }
                else
                {
                    dtCases.Rows[0]["dtOtherClaimDate"] = DBNull.Value;
                    dtCases.Rows[0]["sOtherClaimDateQualifier"] = DBNull.Value;
                }
                dtCases.Rows[0]["sWCnumber"] = txt_WcAc.Text.Trim();

                dtCases.Rows[0]["sWCBCaseNo"] = txtWCBCaseNo.Text.Trim();

                //dtCases.Rows[0]["nAutoClaimNo"] = 0;

                dtCases.Rows[0]["nReportCategoryId"] = cmbReportingCategory.SelectedValue;

                //  dtCases.Rows[0]["sReportCategoryDec"] = cmbReportingCategory.Text;
                if (CmbAccidentType.SelectedIndex == 2 && cmbState.SelectedIndex < 0 && nCaseId == 0)
                {
                    MessageBox.Show("Please select a state.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbState.Focus();
                    return;

                }
                else
                    dtCases.Rows[0]["sState"] = cmbState.Text;

                dtCases.Rows[0]["sNote"] = txtNote.Text.Trim();
                dtCases.Rows[0]["dtCreatedDateTime"] = Convert.ToDateTime(DateTime.Now.Date);
                dtCases.Rows[0]["dtModifiedDateTime"] = Convert.ToDateTime(DateTime.Now.Date);
                dtCases.Rows[0]["nCreatedUserID"] = gloGlobal.gloPMGlobal.UserID;
                dtCases.Rows[0]["nModifyUserID"] = gloGlobal.gloPMGlobal.UserID;
                if (rbICD10.Checked)
                {
                    dtCases.Rows[0]["nICDRevision"] = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode();
                }
                else
                {
                    dtCases.Rows[0]["nICDRevision"] = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
                }
                dtCases.Rows[0]["bISOBPregnancyCase"] = chkOBPregnancyCase.CheckState.GetHashCode();
                dtCases.AcceptChanges();

                CheckOBMedicalCategory();

                for (int i = 1; i < c1Insurance.Rows.Count; i++)
                {

                    if (c1Insurance.GetData(i, COL_INSURANCEPARTY).ToString() != "X")
                    {
                        dtCasesIns.Rows.Add();
                        dtCasesIns.Rows[i - 1]["nId"] = c1Insurance.GetData(i, COL_IndID);
                        dtCasesIns.Rows[i - 1]["nCaseId"] = nCaseId;
                        dtCasesIns.Rows[i - 1]["nInsuranceId"] = c1Insurance.GetData(i, COL_INSURANCEID);
                        dtCasesIns.Rows[i - 1]["sInsuranceName"] = c1Insurance.GetData(i, COL_INSURANCENAME); ;
                        dtCasesIns.Rows[i - 1]["nResponsibilityNumber"] = c1Insurance.GetData(i, COL_INSURANCEPARTY);
                        dtCasesIns.AcceptChanges();
                    }
                }
                oclsCaseSetup.SaveCaes(dtCases, dtCasesDiag, dtCasesIns);
                _nCaseID = oclsCaseSetup.nCaseID;
                if (oclsCaseSetup.CaseData != null)
                    _CaseData = oclsCaseSetup.CaseData;
                if (oclsCaseSetup.Diagnosis != null)
                    _Diagnosis = oclsCaseSetup.Diagnosis;
                if (oclsCaseSetup.CurrentCaseInsurences != null)
                    _Insurences = oclsCaseSetup.CurrentCaseInsurences;
                if (dtCases != null) { dtCases.Dispose(); dtCases = null; }
                if (dtCasesDiag != null) { dtCasesDiag.Dispose(); dtCasesDiag = null; }
                if (dtCasesIns != null) { dtCasesIns.Dispose(); dtCasesIns = null; }

                _IsCloseClick = true;
                this.Close();
            }
        }

        private void CheckOBMedicalCategory()
        {
            Int64 nMedicalCategory = 0;
            bool bShowMedCat = false;
            DataTable _dt = null;
            DataTable _dtMedCat = null;

            try
            {
                GeneralSettings setting = new GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                object OBSpecilityActive = null;
                setting.GetSetting("OB SPECIALITY", out OBSpecilityActive);
                setting.Dispose();

                //Added to delete OB Medical Category of associated to case if end date less than today's date
                if (IsValidDate(mskCaseEndDate.Text))
                {
                    if (Convert.ToDateTime(mskCaseEndDate.Text) <= System.DateTime.Now.Date)
                    {
                        //delete OB medical category associate to case when case is inactive
                        deletePatientOBMedCat();
                        return;
                    }
                }

                if (tsb_Modify.Visible == true)
                {
                    if (bIsOBPregCaseCheck)
                        bShowMedCat = true;
                    else
                        bShowMedCat = false;
                }
                else
                {
                    if (bIsOBPregCaseCheck)
                        bShowMedCat = true;
                    else
                        bShowMedCat = false;
                }
                _dt = getPatientOBMedCat();

                if (bShowMedCat && (_dt != null && _dt.Rows.Count == 0))
                {
                    if (chkOBPregnancyCase.CheckState.GetHashCode() == 1 && OBSpecilityActive.ToString() == "1")
                    {
                        if (CheckOBSpecificMedicalCategory(out nMedicalCategory) && nMedicalCategory > 1)
                        {
                            //Multiple Medical category selected for OB in Admin
                            frmOBMedCatlist = new Form();
                            frmOBMedCatlist.StartPosition = FormStartPosition.CenterScreen;
                            frmOBMedCatlist.Width = 700;
                            frmOBMedCatlist.Text = "OB Medical Category";
                            frmOBMedCatlist.Shown += new EventHandler(frmOBMedCatlist_Shown);

                            oMedicalCategory = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.OBMedicalCategory, true, frmOBMedCatlist.Width);
                            oMedicalCategory.ControlHeader = "OB Medical Category";

                            oMedicalCategory.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oMedicalCategory_ItemSelectedClick);
                            oMedicalCategory.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oMedicalCategory_ItemClosedClick);
                            frmOBMedCatlist.Controls.Add(oMedicalCategory);
                            oMedicalCategory.Dock = DockStyle.Fill;
                            oMedicalCategory.BringToFront();

                            if (_dt != null)
                            {
                                foreach (DataRow dr in _dt.Rows)
                                {
                                    oMedicalCategory.SelectedItems.Add(Convert.ToInt64(dr["nMedicalCategoryID"]), Convert.ToString(dr["sMedicalCategory"]));
                                }
                            }

                            oMedicalCategory.OpenControl();
                            oMedicalCategory.ShowHeaderPanel(false);
                            frmOBMedCatlist.ShowDialog(((frmOBMedCatlist.Parent == null) ? this : frmOBMedCatlist.Parent));


                            if (frmOBMedCatlist != null)
                            {
                                oMedicalCategory.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oMedicalCategory_ItemSelectedClick);
                                oMedicalCategory.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oMedicalCategory_ItemClosedClick);
                                frmOBMedCatlist.Controls.Remove(oMedicalCategory);
                                oMedicalCategory.Dispose();
                                oMedicalCategory = null;
                                frmOBMedCatlist.Close();
                                frmOBMedCatlist.Dispose();
                                frmOBMedCatlist = null;
                            }
                        }
                        else
                        {
                            if (nMedicalCategory == 0)
                            {
                                //No Medical category selected for OB in Admin
                            }
                            else if (nMedicalCategory == 1)
                            {
                                //Single Medical category selected for OB in Admin

                                _dtMedCat = getOBMedicalCategory();
                                SaveMedicalCategory(_dtMedCat);
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
                if (_dtMedCat != null)
                {
                    _dtMedCat.Dispose();
                    _dtMedCat = null;
                }
                if (frmOBMedCatlist != null)
                {
                    oMedicalCategory.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oMedicalCategory_ItemSelectedClick);
                    oMedicalCategory.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oMedicalCategory_ItemClosedClick);
                    frmOBMedCatlist.Controls.Remove(oMedicalCategory);
                    oMedicalCategory.Dispose();
                    oMedicalCategory = null;
                    frmOBMedCatlist.Close();
                    frmOBMedCatlist.Dispose();
                    frmOBMedCatlist = null;
                }
            }
        }

        private void deletePatientOBMedCat()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtOBMedCat = new DataTable();
            try
            {
                oParameters.Clear();
                oParameters.Add("@PatientId", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_DeletePatientOBMedicalCat", oParameters, out dtOBMedCat);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }

        }

        void frmOBMedCatlist_Shown(object sender, EventArgs e)
        {
            try
            {
                gloListControl.gloListControl oglolistcontrol = (gloListControl.gloListControl)frmOBMedCatlist.Controls[0];
                if (oglolistcontrol != null)
                {
                    oglolistcontrol.OpenControl();
                }
            }
            catch (Exception)
            {

            }
        }

        private DataTable getPatientOBMedCat()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtOBMedCat = new DataTable();
            try
            {
                oParameters.Clear();
                oParameters.Add("@PatientId", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetPatientOBMedicalCat", oParameters, out dtOBMedCat);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }

            return dtOBMedCat;
        }

        private DataTable getOBMedicalCategory()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtOBMedCat = new DataTable();
            try
            {
                oParameters.Clear();
                oParameters.Add("@PatientId", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_get_OBMedicalCategory", oParameters, out dtOBMedCat);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
            return dtOBMedCat;
        }

        void oMedicalCategory_ItemClosedClick(object sender, EventArgs e)
        {
            if (frmOBMedCatlist != null)
            {
                frmOBMedCatlist.Close();
                frmOBMedCatlist.Dispose();
                frmOBMedCatlist = null;
            }
        }

        void oMedicalCategory_ItemSelectedClick(object sender, EventArgs e)
        {
            DataTable dtMedCat = new DataTable();
            DataColumn dcPatID = new DataColumn("nPatientID");
            DataColumn dcMedCatID = new DataColumn("nMedicalCategoryId");

            dtMedCat.Columns.Add(dcPatID);
            dtMedCat.Columns.Add(dcMedCatID);

            if (oMedicalCategory.SelectedItems.Count > 0)
            {
                for (int i = 0; i < oMedicalCategory.SelectedItems.Count; i++)
                {
                    DataRow dr = dtMedCat.NewRow();
                    dr["nPatientID"] = Convert.ToInt64(_PatientID);
                    dr["nMedicalCategoryId"] = Convert.ToInt64(oMedicalCategory.SelectedItems[i].ID);
                    dtMedCat.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtMedCat.NewRow();
                dr["nPatientID"] = Convert.ToInt64(_PatientID);
                dr["nMedicalCategoryId"] = 0;
                dtMedCat.Rows.Add(dr);
            }

            SaveMedicalCategory(dtMedCat);
            if (frmOBMedCatlist != null)
            {
                frmOBMedCatlist.Close();
                frmOBMedCatlist.Dispose();
                frmOBMedCatlist = null;
            }
        }

        private void SaveMedicalCategory(DataTable dt = null)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtResult = new DataTable();
            try
            {

                oParameters.Clear();
                oParameters.Add("@tvpMedicalCategory", dt, ParameterDirection.Input, SqlDbType.Structured);
                oParameters.Add("@PatientId", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@IsFromCases", true, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetPatientMedicalCategoryColor", oParameters, out _dtResult);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
        }

        private Boolean CheckOBSpecificMedicalCategory(out Int64 nMedicalCategory)
        {
            Boolean _isMultipleOBMedCategory = false;
            gloDatabaseLayer.DBLayer oDB = null;
            Int64 cntMedCat = 0;
            try
            {
                object count = null;

                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                string sqlstring = "SELECT count(*) FROM  MedicalCategoryOB WITH (NOLOCK)";
                oDB.Connect(false);
                count = oDB.ExecuteScalar_Query(sqlstring);
                oDB.Disconnect();
                if (Convert.ToInt64(count) > 1)
                {
                    _isMultipleOBMedCategory = true;
                }
                cntMedCat = Convert.ToInt64(count);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            nMedicalCategory = cntMedCat;
            return _isMultipleOBMedCategory;
        }
        private void tlsClose_Click(object sender, EventArgs e)
        {

            DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
            {
                tlsSaveClose_Click(sender, e);
            }
            else if (res == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                _IsCloseClick = true;
                this.Close();
            }
        }
        private void mskDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                _IsValidate = true;
                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Specifies that the Date is InValid

                            e.Cancel = true;
                            _IsValidate = false;
                        }

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _IsValidate = false;
            }
        }
        private void mskDate_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }
        private void btnRemove_PriorAuthorization_Click(object sender, EventArgs e)
        {
            if (txtPriorAuthorizationNo.Tag != null && txtPriorAuthorizationNo.Tag.ToString() != "")
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Remove, "Prior Authorization removed from the Transaction", _PatientID, Convert.ToInt64(txtPriorAuthorizationNo.Tag), 0, ActivityOutCome.Success);
                txtPriorAuthorizationNo.Text = "";
                txtPriorAuthorizationNo.Tag = null;
            }

            CheckForPatientPriorAuthorization();
        }
        private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }
        private void btnAdd_Referral_Click(object sender, EventArgs e)
        {
            Int64 _currentPatientId = 0;
            gloUserRights.ClsgloUserRights ObjUserRights = new gloUserRights.ClsgloUserRights(_DatabaseConnectionString);
            try
            {

           
                ObjUserRights.CheckForUserRights(_UserName);
                if (ObjUserRights.ModifyPatient == true)
                {
                    if (this._PatientID > 0)
                    {
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_DatabaseConnectionString);
                        ogloPatient.ShowPatientRegistration(this._PatientID, gloPatient.ModifyPatientDetailType.Referral, out _currentPatientId, this);
                        ReloadPatientRefferals(_currentPatientId);
                        ogloPatient.Dispose();
                        ogloPatient = null;

                    }
                }


                if (cmbReferralProvider.Items.Count == 2)
                {
                    cmbReferralProvider.SelectedIndex = 1;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                try
                {
                    if (ObjUserRights != null)
                    {
                        ObjUserRights.Dispose();
                        ObjUserRights = null;
                    }
                }
                catch
                {
                }
            }
        }
        private void c1Insurance_CellChanged(object sender, RowColEventArgs e)
        {

            {
                string _fillInsuranceName = "";
                Int64 _fillInsuranceID = 0;
                Int32 _fillInsSelfMode = 0;
                CheckEnum _Selected = CheckEnum.None;
                int _CurRowIndex = e.Row;

                if (c1Insurance.Rows.Count > 0)
                {
                    _Selected = c1Insurance.GetCellCheck(_CurRowIndex, COL_SELECT);

                    if (_Selected == CheckEnum.Checked)
                    {
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

                        _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(_CurRowIndex, COL_INSURANCEID));
                        _fillInsuranceName = Convert.ToString(c1Insurance.GetData(_CurRowIndex, COL_INSURANCENAME));
                        _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(_CurRowIndex, COL_INSSELFMODE));

                    }
                    else
                    {
                        _fillInsuranceID = 0;
                        _fillInsuranceName = "";
                        _fillInsSelfMode = PayerMode.None.GetHashCode();

                    }

                    if (e.Col == COL_INSURANCERESPONSIBILITY && e.Row > 0)
                    {

                        Char _result = Convert.ToChar(c1Insurance.GetData(e.Row, e.Col));
                        if (System.Text.RegularExpressions.Regex.IsMatch(_result.ToString(), @"^([1-9]*)$") == false)
                        {
                            c1Insurance.SetData(e.Row, COL_INSURANCEPARTY, Convert.ToChar("X"));
                            c1Insurance.SetData(e.Row, e.Col, Convert.ToString(""));
                        }
                        else
                        {
                            c1Insurance.SetData(e.Row, COL_INSURANCEPARTY, _result);
                        }

                        ReorderInsurance();

                        for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                        }

                    }

                }

            }

        }
        private void cmbFacility_MouseEnter(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbFacility.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbFacility.Items[cmbFacility.SelectedIndex])["sFacilityName"]), cmbFacility) >= cmbFacility.DropDownWidth - 30)
                {
                    string txt = Convert.ToString(((DataRowView)cmbFacility.Items[cmbFacility.SelectedIndex])["sFacilityName"]);
                    if (tooltip_Billing.GetToolTip(cmbFacility) != txt)
                    {
                        tooltip_Billing.SetToolTip(cmbFacility, txt);
                    }
                }
                else
                {
                    this.tooltip_Billing.SetToolTip(cmbFacility, "");
                }

            }
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
                            if (tooltip_Billing.GetToolTip(combo) != txt)
                            {
                                this.tooltip_Billing.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                            }
                        }
                        else
                        {
                            this.tooltip_Billing.SetToolTip(combo, "");
                        }
                    }
                    else
                    {
                        this.tooltip_Billing.Hide(combo);
                    }
                }
                else
                {

                }
                e.DrawFocusRectangle();
            }
        }
        private void cmbReferralProvider_MouseEnter(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbReferralProvider.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbReferralProvider.Items[cmbReferralProvider.SelectedIndex])["sReferralName"]), cmbReferralProvider) >= cmbReferralProvider.DropDownWidth - 30)
                {
                    string txt = Convert.ToString(((DataRowView)cmbReferralProvider.Items[cmbReferralProvider.SelectedIndex])["sReferralName"]);
                    if (tooltip_Billing.GetToolTip(cmbReferralProvider) != txt)
                    {
                        tooltip_Billing.SetToolTip(cmbReferralProvider, txt);
                    }
                }
                else
                {
                    this.tooltip_Billing.SetToolTip(cmbReferralProvider, "");
                }

            }
        }
        private void cmbReportingCategory_MouseEnter(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbReportingCategory.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbReportingCategory.Items[cmbReportingCategory.SelectedIndex])["sDecription"]), cmbReportingCategory) >= cmbReportingCategory.DropDownWidth - 30)
                {
                    string txt = Convert.ToString(((DataRowView)cmbReportingCategory.Items[cmbReportingCategory.SelectedIndex])["sDecription"]);
                    if (tooltip_Billing.GetToolTip(cmbReportingCategory) != txt)
                    {
                        tooltip_Billing.SetToolTip(cmbReportingCategory, txt);
                    }
                }
                else
                {
                    this.tooltip_Billing.SetToolTip(cmbReportingCategory, "");
                }

            }
        }
        private void c1Insurance_KeyUp(object sender, KeyEventArgs e)
        {
            int ResponsibilityNo = 0;
            if (e.KeyCode == Keys.Delete)
            {
                if (c1Insurance.GetData(c1Insurance.RowSel, 1) != null)

                    ResponsibilityNo = Convert.ToInt16(c1Insurance.GetData(c1Insurance.RowSel, 1));
                e.SuppressKeyPress = true;

                switch (c1Insurance.ColSel)
                {

                    case 1:
                        c1Insurance.SetData(c1Insurance.RowSel, c1Insurance.ColSel, "");
                        break;


                }

            }
        }
        private void c1ClaimGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Boolean _IsModified = false;
            try
            {
                HitTestInfo hitInfo = this.c1ClaimGrid.HitTest(e.X, e.Y);
                if (c1ClaimGrid.Rows.Count > 1)
                {
                    if (hitInfo.Row != 0)
                    {
                        _IsModified = ModifyCharge();
                        if (_IsModified)
                        {
                            FillClaim_Charges();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Claim not available.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }
        private void c1ClaimGrid_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }
        private void c1ClaimGrid_AfterSort(object sender, SortColEventArgs e)
        {
            //SetNoteFlag();
            //if (e.Col == c1ClaimGrid.Cols["SplitClaimNumber"].Index)
            //{
            //    c1ClaimGrid.Sort(e.Order, c1ClaimGrid.Cols["SortOrder"].Index);
            //}

            if (c1ClaimGrid.DataSource != null)
            {
                if (e.Col == c1ClaimGrid.Cols["SplitClaimNumber"].Index)
                {
                    c1ClaimGrid.Cols[c1ClaimGrid.Cols.Count - 2].Sort = e.Order;
                    c1ClaimGrid.Cols[c1ClaimGrid.Cols.Count - 1].Sort = SortFlags.Ascending;

                    c1ClaimGrid.Sort(SortFlags.UseColSort, c1ClaimGrid.Cols.Count - 2, c1ClaimGrid.Cols.Count - 1);
                }
            }
            SetNoteFlag();

        }
        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            Boolean _IsModified = false;
            try
            {
                if (c1ClaimGrid.Rows.Count > 1)
                {
                    _IsModified = ModifyCharge();
                    if (_IsModified)
                    {
                        FillClaim_Charges();
                    }
                }
                else
                {
                    MessageBox.Show("Claim not available.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }
        private void frmSetupCases_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_IsCloseClick)
            {
                DialogResult resSaveChanges = MessageBox.Show("Do you want to save changes to this record? ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                if (resSaveChanges == DialogResult.Yes)
                {
                    tlsSaveClose_Click(sender, e);
                }

                else if (resSaveChanges == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                
                else
                {
                    this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupCases_FormClosing);
                    _IsCloseClick = true;
                    this.Close();
                }

                if (blnAbortClose == true)
                {
                    blnAbortClose = false; 
                    e.Cancel = true;
                }
            }
        }
        #endregion

        // code added for icd selection 02142014 Sameer
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

        private void chkOBPregnancyCase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOBPregnancyCase.Checked)
                bIsOBPregCaseCheck = true;
            else if (chkOBPregnancyCase.Checked == false)
                bIsOBPregCaseCheck = false;
        }

        private void btnCloseCase_Click(object sender, EventArgs e)
        {
            if (!IsValidDate(mskCaseEndDate.Text))
            {
                mskCaseEndDate.Text=DateTime.Now.ToString("MM/dd/yyyy");
            }
        }

        private bool GetEnableWorkersCompFormsSetting(string _databaseconnectionstring)
        {
            gloSettings.GeneralSettings _oSettings = null;
            object _obj = null;
            bool bIsEnabled = false;

            try
            {
                _oSettings = new GeneralSettings(_databaseconnectionstring);
                _oSettings.GetSetting("EnableWorkersCompForms", 0, 1, out _obj);

                if (_obj != null && Convert.ToString(_obj).Trim().Length > 0)
                {
                    bIsEnabled = Convert.ToBoolean(_obj);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (_oSettings != null)
                {
                    _oSettings.Dispose();
                    _oSettings = null;
                }

                _obj = null;
            }

            return bIsEnabled;
        }

        private void FillOtherClaimDateQualifiers()
        {
            DataTable _dtClaimDatesQualifiers = null;
           
            _dtClaimDatesQualifiers = gloGlobal.gloPMMasters.GetClaimDatesQualifiers();

            if (_dtClaimDatesQualifiers != null && _dtClaimDatesQualifiers.Rows.Count > 0)
            {
                cmbOtherClaimDateQual.BeginUpdate();
                cmbOtherClaimDateQual.DataSource = _dtClaimDatesQualifiers;
                cmbOtherClaimDateQual.DisplayMember = _dtClaimDatesQualifiers.Columns["sDescription"].ColumnName;
                cmbOtherClaimDateQual.ValueMember = _dtClaimDatesQualifiers.Columns["sQualifier"].ColumnName;
                cmbOtherClaimDateQual.EndUpdate();
                GeneralSettings setting = new GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                object DefaultDateQualifierCode = null;
                setting.GetSetting("DefaultDateQualifier", out DefaultDateQualifierCode);
                setting.Dispose();
                if (DefaultDateQualifierCode != null )
                {
                   if(Convert.ToString(DefaultDateQualifierCode).Trim() != "")
                   cmbOtherClaimDateQual.SelectedValue = Convert.ToString(DefaultDateQualifierCode);
                }
              
            }

        }

        private void FillClaimDateQualifiers()
        {
            DataTable _dtClaimDatesQualifiers = null;
            _dtClaimDatesQualifiers = gloGlobal.gloPMMasters.GetBox14DatesQualifiers();

            if (_dtClaimDatesQualifiers != null && _dtClaimDatesQualifiers.Rows.Count > 0)
            {
                CmbClaimDateQual.BeginUpdate();
                CmbClaimDateQual.DataSource = _dtClaimDatesQualifiers;
                CmbClaimDateQual.DisplayMember = _dtClaimDatesQualifiers.Columns["sDescription"].ColumnName;
                CmbClaimDateQual.ValueMember = _dtClaimDatesQualifiers.Columns["sQualifier"].ColumnName;
                CmbClaimDateQual.EndUpdate();

                //Will set Onset as default as per our existing implementation
                CmbClaimDateQual.SelectedValue = "431";
            }

        }
    }
}
