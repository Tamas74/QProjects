using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAuditTrail;
using gloBilling.Collections;
using gloBilling.Common;
using gloSettings;
using gloEDI;
using Ionic.Zip;
using System.IO;


namespace gloBilling
{

    public partial class frmBillingTransaction : gloAUSLibrary.MasterForm 
    {

        #region "Variable Declarations"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public string _DatabaseConnectionString = "";
        private string _messageBoxCaption = "";
        private Int64 _UserID = 0;
        private string _UserName = "";
        private Int64 _ClinicID = 0;
        private Int64 _PatientID = 0;
        private Int64 nPAccountID = 0;
        private Int64 nGuarantorID = 0;
        private Int64 nAccountPatientID = 0;
        private bool _IsPatientAccountFeature = false;
        private string _PatientProviderName = "";
        private Int64 _nextClaimNo = 0;
        private String _InsuranceName = "";
        private String _sDelayReasonCode = string.Empty;
        private String _sServiceAuthExceptionCode = string.Empty;
        private string DefaultBox19Note = "";
       
        private DataTable _dtCaseData = null;
        private DataTable _dtCPTActivationDates = null;
        
        

     //   private Int64 _selectedChargeTrayId = 0;
        private string _selectedChargeTrayDescription = "";
        private bool _patientHasAutoClaim = false;
        private bool _patientHasPriorAuthorization = false;        
        private ClaimValidationService _claimValidationService = ClaimValidationService.None;
        private FacilityType _defaultFeeTypeCharges = FacilityType.None;
        const int ZERO_VALUE = 0;
        UB04Data oUB = new UB04Data();
        private String _sMedicaidResubmissionCode = string.Empty;

        private DataSet _dsLastClaimDetails = null;


        Boolean _bLoadDefaults = false;
        Boolean _bDefaultDOS = false;
        Boolean _bDefaultFacility = false;
        Boolean _bDefaultBillingProvider = false;

       // private Boolean bIsSaveCharges = false;
        private bool _IsWorkercimp = false;

        private Boolean IsFullyPosted = false;
        private Int64 _ResponsibleInsuranceContactID;
        private Int64 _ResponsibleInsuranceID;
        
        public enum ShowHideType
        {
            None = 0,
            EMRTreatment = 1,
            CancelEMRTreatment = 2,
            CloseEMRTreatment = 3,
            LoadEMRTreatment = 4,
            LoadExamTemplate = 5,
            PatientBannerPatientChange = 6,
            WaitStart = 7,
            WaitFinished = 8,
            OnlineCharge = 9,
            CloseOnlineCharge = 10,
            LoadOnlineCharge = 11,
            CancelOnlineCharge = 12

        }

        private static DataTable dtBilledCPTS = null;

        #endregion

        #region Column Contants for C1.Flex.Grid

        // To Define Column Constant for Insurance Grid
        const int COL_SELECT = 0;
        const int COL_INSURANCERESPONSIBILITY = 1;
        const int COL_INSURANCEID = 2;
        const int COL_INSURANCENAME = 3;
        const int COL_INSURANCETYPE = 4;
        const int COL_INSSELFMODE = 5;
        const int COL_INSURANCECOPAYAMT = 6;
        const int COL_INSURANCEWORKERCOMP = 7;
        const int COL_INSURANCEAUTOCLAIM = 8;
        const int COL_INSURANCECONTACTID = 9;
        const int COL_INSURANCEPARTY = 10;
        const int COL_INSURANCECURRRESP = 11;
        const int COL_INSURANCEPLANONHOLD = 12;
        const int COL_ISINSTITUTIONALBILLING = 13;
        const int COL_INSVIEW_COUNT = 16;
        const int COL_INSSTARTENDDATE = 14;
        const int COL_INSCOMMENT = 15;

        //const int COL_INSVIEW_COUNT = 11;


        const int COL_SUBSCRIBERID = 7;
        const int COL_SUBSCRIBERNAME = 8;
        const int COL_SUBSCRIBERGROUPNO = 9;
        const int COL_SUBSCRIBERPOLICYNO = 10;
        const int COL_INSURANCEPAYMENT = 11;
        //const int COL_INSURANCECOPAYAMT = 10;


        const int COL_INSPAYMENT_COUNT = 12;

        const int COL_DX_SELECT = 0;
        const int COL_DX_CODE = 1;
        const int COL_DX_DESC = 2;
        const int COL_DX_ISPRIMARY = 3;
        const int COL_DX_COUNT = 4;
        //const int COL_CPT_CODE = 10;
        //const int COL_CPT_DESC = 11;
        //const int COL_MOD1_CODE = 45;
        //const int COL_MOD2_CODE = 48;
        //const int COL_DATEFROM = 2;
        //const int COL_NDCCODE = 85;

        const int COL_CPT_CODE = 11;
        const int COL_CPT_DESC = 12;
        const int COL_MOD1_CODE = 46;
        const int COL_MOD2_CODE = 49;
        const int COL_DATEFROM = 2;
        const int COL_NDCCODE = 86;
        const int COL_EMRTREATMENTLINENO = 106;

        const int COL_EXAM_SELECT = 0;
        const int COL_EXAM_PATIENTID = 1;
        const int COL_EXAM_EXAMID = 2;
        const int COL_EXAM_VISITID = 3;
        const int COL_EXAM_DOS = 4;
        const int COL_EXAM_NAME = 5;
        const int COL_EXAM_EMRPatientCode = 6;
        const int COL_EXAM_EMRPatientFN = 7;
        const int COL_EXAM_EMRPatientMN = 8;
        const int COL_EXAM_EMRPatientLN = 9;
        const int COL_EXAM_EMRPatientSSN = 10;
        const int COL_EXAM_EMRPatientDOB = 11;
        const int COL_EXAM_PROVIDERID = 12;
        const int COL_EXAM_PROVIDERNAME = 13;
        const int COL_EXAM_PROVIDERFNAME = 14;
        const int COL_EXAM_PROVIDERMNAME = 15;
        const int COL_EXAM_PROVIDERLNAME = 16;

        const int COL_EXAM_COUNT = 17;

        #endregion

        #region " FlexGrid Column variables New"

        private int DX_Col_Select = 0;
        private int DX_Col_ICD9Code_Description = 1;
        private int DX_Col_ICD9Code = 2;
        private int DX_Col_ICD9Desc = 3;
        private int DX_Col_CPTCode = 4;
        private int DX_Col_CPTDesc = 5;
        private int DX_Col_ModCode = 6;
        private int DX_Col_ModDesc = 7;
        private int DX_Col_Units = 8;
        private int DX_Col_ICD9Count = 9;
        private int DX_Col_CPTCount = 10;
        private int DX_Col_ModCount = 11;
        private int DX_Col_CPTLineNo = 12;
        private int DX_Col_ClaimStatus = 13;

        private int DX_Col_Count = 14;

        #endregion

        #region " C1 Constants New"

        private int CPT_COL_SELECT = 0;
        private int CPT_COL_LINE_NO = 1;
        private int CPT_COL_LINE_NO_Display = 2;
        private int CPT_COL_DOS = 3;
        private int CPT_COL_CPT_ID = 4;
        private int CPT_COL_CPT_CODE = 5;
        private int CPT_COL_CPT_DESC = 6;
        private int CPT_COL_DX1_ID = 7;
        private int CPT_COL_DX1_CODE = 8;
        private int CPT_COL_DX1_DESC = 9;
        private int CPT_COL_MOD1_ID = 10;
        private int CPT_COL_MOD1_CODE = 11;
        private int CPT_COL_MOD1_DESC = 12;
        private int CPT_COL_UNIT = 13;
        private int CPT_ISBILLED = 14;
        private int CPT_COL_ClaimStatus = 15;
       
        private int CPT_COL_COUNT = 16;

        #endregion

        #region "Properties"

        public string SelectedChargeTrayDescription
        {
            get
            {

                return _selectedChargeTrayDescription;
            }
            set
            {
                _selectedChargeTrayDescription = value;
                lblCloseDayTray.Text = _selectedChargeTrayDescription;
            }
        }
        private bool PatientHasAutoClaim
        {
            get { return _patientHasAutoClaim; }
            set { _patientHasAutoClaim = value; }
        }        
        
        private bool PatientHasPriorAuthorization
        {
            get { return _patientHasPriorAuthorization; }
            set { _patientHasPriorAuthorization = value; }
        }        

        private bool PatientHasCases { get; set; }
        public ClaimValidationService ClaimValidationServiceType
        {
            get { return _claimValidationService; }
            set { _claimValidationService = value; }
        }
        public FacilityType DefaultFeeTypeCharges
        {
            get { return _defaultFeeTypeCharges; }
            set { _defaultFeeTypeCharges = value; }
        }
        private DataTable dtCaseDiagCache = null;
        public bool bModifyPOS { get; set; }
      
        #endregion

        #region " Form Data Methods "

        private void SetFormLoadData()
        {
            DataSet _dsChargesData = null;
            try
            {

                if (_PatientID > 0 && _UserID > 0)
                {
                    //..Fill the Patient Strip
                    LoadPatientStrip(_PatientID, 0, true);
                    _dsChargesData = gloCharges.GetChargesFormData(_PatientID);

                    if (_dsChargesData != null && _dsChargesData.Tables.Count > 0)
                    {
                        FillStatesData();
                        FillFacilitiesData(); //..Facility data cached 
                        FillChargeTray(_dsChargesData.Tables[4]); //..Charges Tray data cached 
                        FillProviderData(); //..Providers data cached 
                        FillReferralProvidersData(_dsChargesData.Tables[0], true);
                        SetPatientProvider();
                        FillClaimWorkerCompData(_dsChargesData.Tables[1]);
                        SetProviderSettings(_dsChargesData.Tables[2]);
                        SetUBAdminSetting(_dsChargesData.Tables[13]);
                        SetUB04AdminSetting(_dsChargesData.Tables[10]);                       
                        DesignDxGrid();
                        DesignInsuranceGrid();
                        FillPatientInsurances(_dsChargesData.Tables[3]);

                        SetPriorAuthorization(_dsChargesData.Tables[5]);
                        CheckForPatientPriorAuthorization();

                        SetAutoClaim(_dsChargesData.Tables[6]);
                        FillStandardFeeSchedules(_dsChargesData.Tables[7]);
                        SetDefaultFeeChargesSettings(_dsChargesData.Tables[8]);
                        SetUserLastChargeCloseDate(_dsChargesData.Tables[9]);
                         DataTable _dtUB04Settings = _dsChargesData.Tables[10];
                        SetSupervisorOptionSetting(_dsChargesData.Tables[12]);
                        ShowHideUB();

                        SetPatientCases(_dsChargesData.Tables[11]);
                        CheckForPatientCases();

                        SetInitialTreatmentSetting(_dsChargesData.Tables[14]);
                        SetAnesthesiaSetting(_dsChargesData.Tables[15]);
                        if (_dsChargesData.Tables[16] != null && _dsChargesData.Tables[16].Rows.Count > 0)
                        {
                            Boolean bClaimReportingCategorySetting = false;
                            Boolean.TryParse(_dsChargesData.Tables[16].Rows[0]["sSettingsValue"].ToString(), out bClaimReportingCategorySetting);
                            if (bClaimReportingCategorySetting == true || Convert.ToInt16(_dsChargesData.Tables[16].Rows[0]["sSettingsValue"])==1)
                            {
                                cmbClaimCategory.Visible = true;
                                lblClaimCategory.Visible = true;
                                FillClaimReportingCategory(_dsChargesData.Tables[17]);
                            }
                            else
                            {
                                
                                cmbClaimCategory.DataSource = null;
                                cmbClaimCategory.Items.Clear();
                                cmbClaimCategory.Visible = false;
                                lblClaimCategory.Visible = false;
                            }
                        }
                    }
                    CheckForEPSDTEnabled();
                    IsAnesthesiaEnabled();

                    SetDefaultQualifierForProvider(_dsChargesData.Tables[18]);
                    // line added on 12182013 Sameer for setting default date qualifier 
                   
                    FillReportingProviderTypes();

                    
                    SetDefaultDateQualifier(_dsChargesData.Tables[19]);
                    FillOtherClaimDateQualifiers();
                    FillClaimDateQualifiers();
                                                            
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Either PatientID or UserID found zero while loading charges form.", false);
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx.ToString(), false);
            }
            finally
            {
                _dsChargesData.Dispose();
                _dsChargesData = null;
            }


        }
        
        //RoopaliB 19 July 2012
        // New setting added to insurance plan setup to set where occurance date should be default to DOS or not.
        // Same setting we are going to retrive here and will assigned to UB04 object. 
        private void UB04Default()
        {
            try
            {
                string sMinDos = "";
                if (UC_gloBillingTransactionLines.GetLinesCount() > 1)
                {
                    sMinDos = Convert.ToString(gloDateMaster.gloDate.DateAsDate((UC_gloBillingTransactionLines.GetMinDOS())));
                }
                Int64 _nInsuranceID = 0;
                if (c1Insurance.Rows.Count > 1)
                {
                    _nInsuranceID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                }
                if (!IsDefaultOccuranceDOS(_nInsuranceID))
                {
                    sMinDos = "";
                    oUB.MinDOSDeleted = true;
                    oUB.sOccurrenceDate01 = "";
                }
                else
                {
                    oUB.MinDOSDeleted = false;
                    sMinDos = Convert.ToString(gloDateMaster.gloDate.DateAsDate((UC_gloBillingTransactionLines.GetMinDOS())));
                    oUB.sOccurrenceDate01 = sMinDos.ToString();
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            }
        }

        private void SetPatientChangeData(bool LoadPatientBanner)
        {
            DataSet _dsChargesData = null;

            _bLoadDefaults = false;
            _bDefaultDOS = false;
            _bDefaultFacility = false;
            _bDefaultBillingProvider = false;

            try
            {
                if (LoadPatientBanner == false)
                {
                    _PatientID = oPatientControl.CmbSelectedPatientID;
                    this.nPAccountID = oPatientControl.PAccountID;
                    this.nGuarantorID = oPatientControl.GuarantorID;
                    this.nAccountPatientID = oPatientControl.AccountPatientID;
                }

                if (LoadPatientBanner == true)
                { LoadPatientStrip(_PatientID, _PatientPoviderID, true); }

                DesignDxGrid();
                DesignInsuranceGrid();

                _dsChargesData = gloCharges.GetPatientChangeData(_PatientID);

                if (_dsChargesData != null && _dsChargesData.Tables.Count > 0)
                {


                    #region "To Check Whether to Load the Last Saved Claim Details or not "

                    if (_dsChargesData.Tables[7] != null && _dsChargesData.Tables[7].Rows.Count > 0)
                    {

                        DataRow[] _drSettings = null;
                        _drSettings = _dsChargesData.Tables[7].Select("sSettingsName='CHRG_ENTRYDEFAULTS'");
                        if (_drSettings.Length > 0)
                        {
                            if (Convert.ToString(_drSettings[0]["sSettingsValue"]) != "None")
                            {
                                _bLoadDefaults = true;

                                _drSettings = _dsChargesData.Tables[7].Select("sSettingsName='CHRG_DEFAULTS_DOS'");
                                if (_drSettings.Length > 0)
                                {
                                    _bDefaultDOS = Convert.ToBoolean(_drSettings[0]["sSettingsValue"]);
                                }


                                _drSettings = _dsChargesData.Tables[7].Select("sSettingsName='CHRG_DEFAULTS_FACILITY'");
                                if (_drSettings.Length > 0)
                                {
                                    _bDefaultFacility = Convert.ToBoolean(_drSettings[0]["sSettingsValue"]);
                                }



                                _drSettings = _dsChargesData.Tables[7].Select("sSettingsName='CHRG_DEFAULTS_BILLINGPROVIDER'");
                                if (_drSettings.Length > 0)
                                {
                                    _bDefaultBillingProvider = Convert.ToBoolean(_drSettings[0]["sSettingsValue"]);
                                }


                            }
                        }
                    }

                    #endregion

                    FillReferralProvidersData(_dsChargesData.Tables[0], true);
                    SetPatientProvider();
                    FillClaimWorkerCompData(_dsChargesData.Tables[1]);

                    if (_EMRProviderId > 0 && (_EMRProviderId != _PatientPoviderID))
                    {
                        DataTable dtProviderSettings = GetSettingsForExamProvider(_EMRProviderId);
                        if (dtProviderSettings != null && dtProviderSettings.Rows.Count > 0)
                        {
                            SetProviderSettings(dtProviderSettings);
                        }
                        if (dtProviderSettings != null)
                        {
                            dtProviderSettings.Dispose();
                            dtProviderSettings = null;
                        }
                    }
                    else
                    {
                        SetProviderSettings(_dsChargesData.Tables[2]);
                    }

                    FillPatientInsurances(_dsChargesData.Tables[3]);
                    SetPriorAuthorization(_dsChargesData.Tables[4]);
                    CheckForPatientPriorAuthorization();

                    txtCases.Text = "";
                    txtCases.Tag = null;
                    SetPatientCases(_dsChargesData.Tables[6]);
                    CheckForPatientCases();

                    DataTable dtCases = null;
                    DataTable dtCaseDiag = null;
                    DataTable dtCasesIns = null;
                    DialogResult oResult = DialogResult.Yes;

                    mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskClaimDate.Text != "")
                    {
                        mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    }

                    gloCharges.getSingleValidCase((mskClaimDate.Text != string.Empty ? Convert.ToDateTime(mskClaimDate.Text) : DateTime.Now), _PatientID, out dtCases, out dtCaseDiag, out dtCasesIns);
                    if (dtCases != null && dtCases.Rows.Count == 1)
                    {
                        if (oResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (_dtCaseData != null)
                            {
                                _dtCaseData.Dispose();
                                _dtCaseData = null;
                            }
                            _dtCaseData = dtCases.Copy();
                            txtCases.Tag = dtCases.Rows[0]["nCaseID"];
                            txtCases.Text = Convert.ToString(dtCases.Rows[0]["sCaseName"]);
                            txtCases.TextAlign = HorizontalAlignment.Left;
                            txtCases.ForeColor = Color.Black;
                            SetCaseDetailsintoCharge(dtCases);
                            if (dtCaseDiagCache != null)
                            {
                                dtCaseDiagCache.Dispose();
                                dtCaseDiagCache = null;
                            }
                            dtCaseDiagCache = dtCaseDiag.Copy();
                            //SetCaseDiagonosisintoChargeLines(dtCaseDiag);
                            //_IsFormLoading = true;
                            SetCaseInsurance(dtCasesIns);
                            //_IsFormLoading = false;

                            this.PatientHasCases = true;

                        }
                    }
                    else
                    {
                        if (_bEMRTreatmentLoading)
                        {
                            txtCases.Tag = "";
                            txtCases.Text = "";
                            // Problem# - 211 - when load any EMR treatment it does not shows Prior Authorization as <available>
                            //txtPriorAuthorizationNo.Tag = null;
                            //txtPriorAuthorizationNo.Text = string.Empty;
                            // end
                            cmbReferralProvider.SelectedIndex = 0;
                            mskAccidentDate.Text = "";
                            txt_WcAc.Text = "";
                            mskAccidentDate.Text = "";
                            cmbState.Text = "";
                            mskOtherDate.Text = "";
                            mskOnsiteDate.Text = "";
                            mskUnableFromDate.Text = "";
                            mskUnableTillDate.Text = "";
                            _UnableToWorkFromDate_MoreClaimData= 0;
                            _UnableToWorkTillDate_MoreClaimData= 0;
                            mskHospitaliztionFrom.Text = "";
                            mskHospitaliztionTo.Text = "";
                            mskInitTreatment.Text = "";
                            CmbAccidentType.SelectedIndex = (int)AccidentType.None;
                        }

                        if (dtCases != null && dtCases.Rows.Count > 1)
                        {
                            CheckForPatientCases();
                        }

                    }
                    if (_bOnlineClaimPostingLoading == true)
                    {
                        mskHospitaliztionFrom.Text = _OnlineHospFromDate;
                        mskHospitaliztionTo.Text = _OnlineHospToDate;

                    }
                    mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

                    SetAutoClaim(_dsChargesData.Tables[5]);


                    if (cmbReferralProvider.Items.Count == 2)
                    {
                        cmbReferralProvider.SelectedIndex = 1;
                    }


                    if (dtCases != null)
                    {
                        dtCases.Dispose();
                        dtCases = null;
                    }
                    if (dtCaseDiag != null)
                    {
                        dtCaseDiag.Dispose();
                        dtCaseDiag = null;
                    }
                    if (dtCasesIns != null)
                    {
                        dtCasesIns.Dispose();
                        dtCasesIns = null;
                    }
                    

                }



            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            }
            finally
            {
                _dsChargesData.Dispose();
                _dsChargesData = null;
            }

        }

        private DataTable GetSettingsForExamProvider(Int64 _ExamProviderId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            DataTable _dtProviderSettings = null;
            try
            {
                string _strQuery = " SELECT  ProviderSettings.sName, ProviderSettings.sValue, ProviderSettings.nProviderID " +
                                 " FROM ProviderSettings WITH (NOLOCK) " +
                                 " WHERE  ProviderSettings.nProviderID =" + _ExamProviderId + "   AND ProviderSettings.sName IS NOT NULL ";

                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out _dtProviderSettings);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtProviderSettings;

        }

        private void SetLoginUserChangeData()
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            //gloDatabaseLayer.DBParameters oParameters = null;
            DataSet _dsUserData = null;

            try
            {
                _dsUserData = gloCharges.GetLoginUserChangeData();

                //Set the last selected user close date and set
                SetUserLastChargeCloseDate(_dsUserData.Tables[1]);
                //Set the user charge tray
                FillChargeTray(_dsUserData.Tables[0]);

                //..Set the necessary fields on billing control (...pending...)

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (_dsUserData != null) { _dsUserData.Dispose(); }
                //if (oParameters != null) { oParameters.Clear(); oParameters.Dispose(); }
                //if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        private void ReloadPatientRefferals(Int64 nPatientID)
        {
            DataTable _dtPatientRefferals = null;

            try
            {
                //Retrieve Patient Referral Data on Reload
                _dtPatientRefferals = gloCharges.GetReloadPatientRefferalsData(nPatientID);

                FillReferralProvidersData(_dtPatientRefferals, true);

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

        private void SetCaseDetailsintoCharge(DataTable _dt)
        {
            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {

                    if ((_dt.Rows[0]["nAuthorizationID"] != DBNull.Value && Convert.ToInt64(_dt.Rows[0]["nAuthorizationID"]) != 0)
                        || (_dt.Rows[0]["nReferralID"] != DBNull.Value && Convert.ToInt64(_dt.Rows[0]["nReferralID"]) != 0))
                    {
                        if (_dt.Rows[0]["nAuthorizationID"] != DBNull.Value && Convert.ToInt64(_dt.Rows[0]["nAuthorizationID"]) != 0)
                        {
                            txtPriorAuthorizationNo.Tag = Convert.ToInt64(_dt.Rows[0]["nAuthorizationID"]);
                            txtPriorAuthorizationNo.Text = Convert.ToString(_dt.Rows[0]["sAuthorizationNumber"]);

                            txtPriorAuthorizationNo.TextAlign = HorizontalAlignment.Left;
                            txtPriorAuthorizationNo.ForeColor = Color.Black;
                        }

                        Int64 nSelectedReferral = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                        ReloadPatientRefferals(_PatientID);
                        chk_SameasBillingProvider.Checked = false;
                        if (_dt.Rows[0]["nReferralID"] != DBNull.Value && Convert.ToInt64(_dt.Rows[0]["nReferralID"]) != 0)
                        {
                            bool bExists = false;
                            if (cmbReferralProvider.DataSource != null)
                            {
                                foreach (DataRowView drv in cmbReferralProvider.Items)
                                {
                                    if (Convert.ToInt64(drv.Row.ItemArray[0]) == Convert.ToInt64(_dt.Rows[0]["nReferralID"])) { bExists = true; break; }
                                }
                            }
                            if (bExists)
                            {
                                cmbReferralProvider.SelectedValue = _dt.Rows[0]["nReferralID"];
                            }
                            else
                            {
                                cmbReferralProvider.SelectedValue = nSelectedReferral;
                            }
                        }
                        else
                        {
                            cmbReferralProvider.SelectedValue = nSelectedReferral;
                        }

                    }
                    //if (!_bDefaultFacility)
                    //{
                    if (_dt.Rows[0]["nFacilityID"] != DBNull.Value && Convert.ToInt64(_dt.Rows[0]["nFacilityID"]) != 0)
                    {
                        cmbFacility.SelectedValue = _dt.Rows[0]["nFacilityID"];
                    }
                    //}
                    // Lines commented on 02212014
                    //if (_dt.Rows[0]["nICDRevision"] != DBNull.Value && Convert.ToInt16(_dt.Rows[0]["nICDRevision"]) == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                    //{
                    //    rbICD10.Checked = true;
                    //    UC_gloBillingTransactionLines.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode();
                    //}
                    //else
                    //{
                    //    rbICD9.Checked = true;
                    //    UC_gloBillingTransactionLines.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
                    //}

                    if (_dt.Rows[0]["nAccidenttype"] != DBNull.Value)
                    {
                        switch (Convert.ToInt16(_dt.Rows[0]["nAccidenttype"]))
                        {
                            case (int)AccidentType.Work:

                                CmbAccidentType.Text = "Work";
                                CmbAccidentType.SelectedIndex = (int)AccidentType.Work;

                                if (_dt.Rows[0]["dtinjuryDate"] != DBNull.Value)
                                {
                                    mskInjuryDate.Text = Convert.ToDateTime(_dt.Rows[0]["dtinjuryDate"]).ToString("MM/dd/yyyy");
                                }
                                else
                                {
                                    mskInjuryDate.Text = "";
                                }

                                this.txt_WcAc.MouseHover -= new System.EventHandler(this.txt_WcAc_MouseHover);

                                if (_dt.Rows[0]["sWCnumber"] != DBNull.Value)
                                {
                                    txt_WcAc.Text = Convert.ToString(_dt.Rows[0]["sWCnumber"]);
                                }
                                else
                                {
                                    txt_WcAc.Text = "";
                                }

                                UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);

                                this.txt_WcAc.MouseHover += new System.EventHandler(this.txt_WcAc_MouseHover);

                                break;

                            case (int)AccidentType.Auto:

                                CmbAccidentType.Text = "Auto";
                                CmbAccidentType.SelectedIndex = (int)AccidentType.Auto;

                                if (_dt.Rows[0]["dtinjuryDate"] != DBNull.Value)
                                {
                                    mskAccidentDate.Text = Convert.ToDateTime(_dt.Rows[0]["dtinjuryDate"]).ToString("MM/dd/yyyy");
                                }
                                else
                                {
                                    mskAccidentDate.Text = "";
                                }

                                this.txt_WcAc.MouseHover -= new System.EventHandler(this.txt_WcAc_MouseHover);

                                if (_dt.Rows[0]["sWCnumber"] != DBNull.Value)
                                {
                                    txt_WcAc.Text = Convert.ToString(_dt.Rows[0]["sWCnumber"]);
                                }
                                else
                                {
                                    txt_WcAc.Text = "";
                                }

                                UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);

                                this.txt_WcAc.MouseHover += new System.EventHandler(this.txt_WcAc_MouseHover);

                                if (_dt.Rows[0]["sState"] != DBNull.Value)
                                {
                                    cmbState.Text = Convert.ToString(_dt.Rows[0]["sState"]);
                                }
                                else
                                {
                                    cmbState.Text = "";
                                }

                                break;

                            case (int)AccidentType.Other:

                                CmbAccidentType.Text = "Other";
                                CmbAccidentType.SelectedIndex = (int)AccidentType.Other;

                                if (_dt.Rows[0]["dtinjuryDate"] != DBNull.Value)
                                {
                                    mskOtherDate.Text = Convert.ToDateTime(_dt.Rows[0]["dtinjuryDate"]).ToString("MM/dd/yyyy");
                                }
                                else
                                {
                                    mskOtherDate.Text = "";
                                }

                                break;
                            case (int)AccidentType.None:

                                CmbAccidentType.Text = "";
                                CmbAccidentType.SelectedIndex = (int)AccidentType.None;

                                if (_dt.Rows[0]["dtinjuryDate"] != DBNull.Value)
                                {
                                    mskOnsiteDate.Text = Convert.ToDateTime(_dt.Rows[0]["dtinjuryDate"]).ToString("MM/dd/yyyy");
                                }
                                else
                                {
                                    mskOnsiteDate.Text = "";
                                }
                                if (_dt.Rows[0]["sClaimDateQualifier"] != DBNull.Value)
                                {
                                    cmbBox14DateQualifier.SelectedValue = Convert.ToString(_dt.Rows[0]["sClaimDateQualifier"]);
                                }
                                else
                                {
                                    cmbBox14DateQualifier.SelectedValue = "431";
                                }
                                break;
                        }
                    }
                    else
                    {
                        CmbAccidentType.Text = "";
                        CmbAccidentType.SelectedIndex = (int)AccidentType.None;

                        if (_dt.Rows[0]["dtinjuryDate"] != DBNull.Value)
                        {
                            mskOnsiteDate.Text = Convert.ToDateTime(_dt.Rows[0]["dtinjuryDate"]).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            mskOnsiteDate.Text = "";
                        }
                        if (_dt.Rows[0]["sClaimDateQualifier"] != DBNull.Value)
                        {
                            cmbBox14DateQualifier.SelectedValue = Convert.ToString(_dt.Rows[0]["sClaimDateQualifier"]);
                        }
                        else
                        {
                            cmbBox14DateQualifier.SelectedValue = "431";
                        }
                    }
                    if (_dt.Rows[0]["sOtherClaimDateQualifier"] != DBNull.Value && _dt.Rows[0]["dtOtherClaimDate"] != DBNull.Value)
                    {
                        cmbBox15DateQualifier.SelectedValue = Convert.ToString(_dt.Rows[0]["sOtherClaimDateQualifier"]);
                        mskBox15Date.Text = Convert.ToDateTime(_dt.Rows[0]["dtOtherClaimDate"]).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        mskBox15Date.Text = "";
                        GeneralSettings setting = new GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        object DefaultDateQualifierCode = null;
                        setting.GetSetting("DefaultDateQualifier", out DefaultDateQualifierCode);
                        setting.Dispose();
                        if (DefaultDateQualifierCode != null)
                        {
                            if (Convert.ToString(DefaultDateQualifierCode).Trim() != "")
                                cmbBox15DateQualifier.SelectedValue = Convert.ToString(DefaultDateQualifierCode);
                        }

                    }
                    if (_dt.Rows[0]["dtunbaleWorkfrom"] != DBNull.Value)
                    {
                        //mskUnableFromDate.Text = Convert.ToDateTime(_dt.Rows[0]["dtunbaleWorkfrom"]).ToString("MM/dd/yyyy");
                        _UnableToWorkFromDate_MoreClaimData = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(_dt.Rows[0]["dtunbaleWorkfrom"]).ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        mskUnableFromDate.Text = "";
                        _UnableToWorkFromDate_MoreClaimData = 0;
                    }

                    if (_dt.Rows[0]["dtunbaleWorkto"] != DBNull.Value)
                    {
                        //mskUnableTillDate.Text = Convert.ToDateTime(_dt.Rows[0]["dtunbaleWorkto"]).ToString("MM/dd/yyyy");
                        _UnableToWorkTillDate_MoreClaimData = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(_dt.Rows[0]["dtunbaleWorkto"]).ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        mskUnableTillDate.Text = "";
                        _UnableToWorkTillDate_MoreClaimData = 0;
                    }

                    if (_dt.Rows[0]["dtHopitalizationDateFrom"] != DBNull.Value)
                    {
                        mskHospitaliztionFrom.Text = Convert.ToDateTime(_dt.Rows[0]["dtHopitalizationDateFrom"]).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        mskHospitaliztionFrom.Text = "";
                    }

                    if (_dt.Rows[0]["dtHopitalizationDateTo"] != DBNull.Value)
                    {
                        mskHospitaliztionTo.Text = Convert.ToDateTime(_dt.Rows[0]["dtHopitalizationDateTo"]).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        mskHospitaliztionTo.Text = "";
                    }
                }

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void SetCaseDiagonosisintoChargeLines(DataTable _dt)
        {
            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    c1Dx.Rows.Count = 1;
                    UC_gloBillingTransactionLines.SetCaseDiagonosisintoChargeLines(_dt);
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void SetCaseInsurance(DataTable _dt)
        {
            int iCounter = 1;
            bool bInsPlanExist = true;
            int iCount = 0;
            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    do
                    {
                        if (Convert.ToString(_dt.Rows[iCount]["sInsuranceName"]) != "Self")
                        {
                            int iRow = c1Insurance.FindRow(Convert.ToString(_dt.Rows[iCount]["nInsuranceId"]), 1, COL_INSURANCEID, false);
                            if (iRow < 0)
                            {
                                bInsPlanExist = false;
                                break;
                            }
                        }

                        iCount++;
                    } while (iCount <= _dt.Rows.Count - 1);

                    if (bInsPlanExist)
                    {
                        for (int iRowCount = c1Insurance.Rows.Count - 1; iRowCount >= 1; iRowCount--)
                        {
                            c1Insurance.SetData(iRowCount, COL_INSURANCERESPONSIBILITY, "");
                            c1Insurance.SetData(iRowCount, COL_INSURANCERESPONSIBILITY, null);
                        }

                        for (iCount = 0; iCount <= _dt.Rows.Count - 1; iCount++)
                        {
                            int iRow = c1Insurance.FindRow(Convert.ToString(_dt.Rows[iCount]["nInsuranceId"]), 1, COL_INSURANCEID, false);

                            if (iRow > 0)
                            {
                                c1Insurance.SetData(iRow, COL_INSURANCERESPONSIBILITY, Convert.ToString(iCounter));
                                iCounter++;
                            }
                        }

                        ReorderInsurance();
                    }
                    else
                    {
                        MessageBox.Show("Please review Claim Insurances."
                            + Environment.NewLine
                            + "An Insurance Plan on the Case has been inactivated.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void SetPatientBannerAccountPatientChangeData()
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            Transaction _Transaction = new Transaction();
            string _LastSavedDOS = "";
            try
            {

                if (oPatientControl.CmbSelectedPatientID > 0)
                {
                    if (!oPatientControl.isBlankPatientSearch)
                    {
                        ShowHideControls(ShowHideType.PatientBannerPatientChange);

                        SetPatientChangeData(false);

                        if (UC_gloBillingTransactionLines != null)
                        {
                            UC_gloBillingTransactionLines.PatientID = this.PatientID;
                            UC_gloBillingTransactionLines.DefaultPOSID = _DefaultPOSID;
                            UC_gloBillingTransactionLines.DefaultTOSID = _DefaultTOSID;
                            UC_gloBillingTransactionLines.PatientProviderID = _PatientPoviderID;
                            LoadDefaultBillingSettings();
                            _LastSavedDOS = UC_gloBillingTransactionLines.GetLastDOS();
                            UC_gloBillingTransactionLines.ReinitilizeControl();
                        }
                        //20100503 - Hot Fix Rendering Provider Issue 
                        chk_SameasBillingProvider_CheckedChanged(null, null);

                        SelectPrimaryInsurance();

                        if (dtCaseDiagCache != null && dtCaseDiagCache.Rows.Count > 0)
                        {
                            SetCaseDiagonosisintoChargeLines(dtCaseDiagCache);
                          
                        }
                        if (dtCaseDiagCache != null)
                        {
                            dtCaseDiagCache.Dispose();
                            dtCaseDiagCache = null;
                        }
                        #region "Get Selected Insurance"

                        string _fillInsuranceName = "";
                        Int64 _fillInsuranceID = 0;
                        Int32 _fillInsSelfMode = 0;
                        Int32 _PrimaryCount = 0;
                        if (c1Insurance.Rows.Count > 0)
                        {
                            for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                            {
                                if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                                {
                                    _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                    _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                    _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                                    break;
                                }
                            }
                        }
                        if (c1Insurance.Rows.Count > 0)
                        {
                            for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                            {
                                if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                                {
                                    _PrimaryCount = _PrimaryCount + 1;
                                }
                            }
                        }
                        if (UC_gloBillingTransactionLines != null)
                        {
                            if (_fillInsuranceID > 0)
                            {
                                UC_gloBillingTransactionLines.AddInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                            }
                        }

                        #endregion

                        #region "Mark the Current party "

                        //To Remove the Previous Flag
                        for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                        }
                        if (_PrimaryCount == 1)
                        {
                            //c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, null);
                            System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                            c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, imgFlag);
                        }

                        //Added 
                        #region " Set Close Date to addedd Line "


                        if (_bLoadDefaults && _bDefaultDOS)
                        {
                            if (UC_gloBillingTransactionLines != null)
                            {
                                if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                                {
                                    if (_LastSavedDOS.Trim() == string.Empty)
                                    {
                                        if (IsValidDate(mskClaimDate.Text) == true)
                                        {
                                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(mskClaimDate.Text));
                                        }
                                    }
                                    else
                                    {
                                        UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(_LastSavedDOS));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (IsValidDate(mskClaimDate.Text) == true)
                            {
                                if (UC_gloBillingTransactionLines != null)
                                {
                                    if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                                    {
                                        if (Convert.ToString(UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE)) == "")
                                        {
                                            if (_sLastServiceLineDOS.Trim() == string.Empty)
                                            {
                                                UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(mskClaimDate.Text));
                                            }
                                            else
                                            {
                                                UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(_sLastServiceLineDOS));
                                            }

                                        }
                                    }
                                }
                            }
                        }

                        #endregion " Set Close Date to addedd Line "

                        if (UC_gloBillingTransactionLines.FacilityPOS > 0)
                        {
                            if (UC_gloBillingTransactionLines != null)
                            {
                                UC_gloBillingTransactionLines.SetFacilityPOS();
                            }
                        }
                      //  txtClaimNo.Text = gloCharges.FormattedClaimNumberGeneration(Convert.ToString(gloCharges.GenerateClaimNumber()));

                        if (cmbReferralProvider.Items.Count == 2)
                        {
                            cmbReferralProvider.SelectedIndex = 1;
                        }
                        //Added

                        //**GetHoldMessage();
                        SetHoldnMoreClaimDataMesseges();
                        SetLastGlobalPeriods();
                        CheckForEPSDTEnabled();
                        IsAnesthesiaEnabled();
                        #endregion

                        #region " Expanded Claim Settings "

                        Int64 _ContactId = 0;

                        if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                        {
                            _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                            UC_gloBillingTransactionLines._nContactID = _ContactId;                            
                        }

                        ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                        _Transaction.NoOfServiceLine = _NoOfMaxServiceLines;
                        _Transaction.NoOfDiagnosis = _NoOfMaxDiagnosis;
                        UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                        UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;

                        #region " Set Fee Schedule "

                        UC_gloBillingTransactionLines.FeeScheduleID = 0;
                        UC_gloBillingTransactionLines.Fee_ScheduleID = 0;
                        UC_gloBillingTransactionLines.SetFNFCharges();

                        if (UC_gloBillingTransactionLines.Fee_ScheduleID > 0)
                        {
                            _FeeScheduleID = UC_gloBillingTransactionLines.Fee_ScheduleID;
                            cmbFeeSchedule.SelectedValue = _FeeScheduleID;
                        }
                        else
                        {
                            // Solving Problem# -450 - when Fee shchedule is not present in the system then it gives exception
                            // Adding if condition to check whether fee schedule is present or not.
                            if (cmbFeeSchedule.Items.Count > 0)
                            {
                                //Bug #67038: 00000386 : Fee Schedules      
                                //Set Provider default fee schedule if it is not available then set clinic default fee schedule
                                _DefaultFeeScheduleID = gloCharges.GetProviderFeeScheduleID(_PatientPoviderID);
                                if (_DefaultFeeScheduleID == 0)
                                {
                                    _DefaultFeeScheduleID = gloCharges.GetClinicFeeScheduleID();
                                }
                                if (_DefaultFeeScheduleID > 0)
                                    cmbFeeSchedule.SelectedValue = _DefaultFeeScheduleID;
                                else
                                    cmbFeeSchedule.SelectedIndex = 0;
                                _FeeScheduleID = Convert.ToInt64(cmbFeeSchedule.SelectedValue);
                                UC_gloBillingTransactionLines.FeeScheduleID = 0;
                                UC_gloBillingTransactionLines.Fee_ScheduleID = 0;
                            }
                        }

                        chkFeeSchedule.Checked = false;

                        #endregion

                        #endregion

                        ShowHideUB();
                        btnAdd_Cases.Focus();
                        btnAdd_Cases.Select();
                        //IsDefaultFeeScheduleExpired();
                        if (gloGlobal.gloPMGlobal.ViewDocumentsOnCharges)
                            clsSplit_PatientCharges.FillDocuments(this.PatientID, gloGlobal.gloPMGlobal.ClinicID);
                        this.RecheckPatientAppointments();
                    }
                    else
                    {
                        UC_gloBillingTransactionLines.SelectTransactionLine(1);
                        if (gloGlobal.gloPMGlobal.ViewDocumentsOnCharges)
                            clsSplit_PatientCharges.FillDocuments(0, 0);
                    }

                    if (c1Insurance.Rows.Count >= 2 && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                    {

                        switch (GetICDCodeType(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)), UC_gloBillingTransactionLines.getfirstservicelineDos()))
                        {
                            case gloGlobal.gloICD.CodeRevision.ICD10:
                                rbICD10.Checked = true;
                                break;
                            case gloGlobal.gloICD.CodeRevision.ICD9:
                                rbICD9.Checked = true;
                                break;
                            default:
                                rbICD9.Checked = true;
                                break;
                        }


                    }
                    if (oPatientControl.IsPatientExists == true)
                    {
                        gloPMGeneral.gloAppointmentsChargesLinking.frmPatientAppointments frmPatientAppointment = null;
                        frmPatientAppointment = new gloPMGeneral.gloAppointmentsChargesLinking.frmPatientAppointments();
                        frmPatientAppointment._IsOnLoadform = true;
                        DataSet dtPatientAppointment = gloCharges.GetMissingChargeAppointments(oPatientControl.CmbSelectedPatientID, 0,0);
                        frmPatientAppointment.PatientAppointments = dtPatientAppointment;

                        if (dtPatientAppointment.Tables.Count >1)
                        {
                            if (Convert.ToInt32(dtPatientAppointment.Tables[1].Rows[0][0]) == 1)
                            {
                                if (dtPatientAppointment.Tables[0].Rows.Count > 0)
                                {

                                    frmPatientAppointment.ShowDialog(this);

                                    if (frmPatientAppointment.AppointmentID != 0)
                                    {
                                        _IsOpenForExternal = false;
                                        GetPatientMissingCharges(frmPatientAppointment);
                                    }
                                }
                            }
                        }
                        if (dtPatientAppointment != null)
                        {
                            dtPatientAppointment.Dispose();
                            dtPatientAppointment = null;
                        }
                    }

                }

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (_Transaction != null) { _Transaction.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                
            }
        }

        private void LoadPatientModifiedData()
        {
            Int64 nSelectedRefProvider = 0;
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            Transaction _Transaction = new Transaction();

            try
            {

                if (oPatientControl.PatientID > 0)
                {
                    _PatientID = oPatientControl.PatientID;
                    _PatientPoviderID = oPatientControl.PatientProviderID;
                    nSelectedRefProvider = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                    if (_EMRProviderId > 0 && (_EMRProviderId != _PatientPoviderID))
                    {
                        DataTable dtProviderSettings = GetSettingsForExamProvider(_EMRProviderId);
                        if (dtProviderSettings != null && dtProviderSettings.Rows.Count > 0)
                        {
                            SetProviderSettings(dtProviderSettings);
                        }
                        if (dtProviderSettings != null)
                        {
                            dtProviderSettings.Dispose();
                            dtProviderSettings = null;
                        }
                    }
                    else
                    {
                        SetPatientModifiedData(true);
                    }

                    if (UC_gloBillingTransactionLines != null)
                    {
                        UC_gloBillingTransactionLines.PatientID = this.PatientID;
                        UC_gloBillingTransactionLines.DefaultPOSID = _DefaultPOSID;
                        UC_gloBillingTransactionLines.DefaultTOSID = _DefaultTOSID;
                        UC_gloBillingTransactionLines.PatientProviderID = _PatientPoviderID;
                        LoadDefaultBillingSettings();
                        UC_gloBillingTransactionLines.ReinitilizeControlOnModifyPatient();

                    }

                    SelectPrimaryInsurance();
                    SetInsuranceParty();


                    //20100503 - Hot Fix Rendering Provider Issue 

                    chk_SameasBillingProvider_CheckedChanged(null, null);


                    #region "Get Selected Insurance"

                    string _fillInsuranceName = "";
                    Int64 _fillInsuranceID = 0;
                    Int32 _fillInsSelfMode = 0;
                   // Int32 _PrimaryCount = 0;

                    if (c1Insurance.Rows.Count > 0)
                    {
                        for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                            {
                                _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                                break;
                            }
                        }
                    }
                    //if (c1Insurance.Rows.Count > 0)
                    //{
                    //    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    //    {
                    //        if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                    //        {
                    //            _PrimaryCount = _PrimaryCount + 1;
                    //        }
                    //    }
                    //}
                    if (UC_gloBillingTransactionLines != null)
                    {
                        if (_fillInsuranceID > 0)
                        {
                            UC_gloBillingTransactionLines.AddInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                        }
                    }

                    #endregion

                    #region "Mark the Current party "

                    ////To Remove the Previous Flag
                    //for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                    //{
                    //    c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                    //}
                    //if (_PrimaryCount == 1)
                    //{
                    //    System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                    //    c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, imgFlag);
                    //}

                    SetHoldnMoreClaimDataMesseges();
                    SetLastGlobalPeriods();
                    CheckForEPSDTEnabled();
                    IsAnesthesiaEnabled();

                    #endregion

                    #region " Expanded Claim Settings "

                    Int64 _ContactId = 0;

                    if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                    {
                        _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                         UC_gloBillingTransactionLines._nContactID = _ContactId;                       
                         UC_gloBillingTransactionLines.setnewAllowedAmount();
                    }

                    ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                    _Transaction.NoOfServiceLine = _NoOfMaxServiceLines;
                    _Transaction.NoOfDiagnosis = _NoOfMaxDiagnosis;
                    UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                    UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;

                    #endregion

                    ShowHideUB();

                    bool bExists = false;

                    if (cmbReferralProvider.DataSource != null)
                    {
                        foreach (DataRowView drv in cmbReferralProvider.Items)
                        {
                            if (Convert.ToInt64(drv.Row.ItemArray[0]) == nSelectedRefProvider)
                            {
                                bExists = true;
                                break;
                            }
                        }
                    }

                    if (bExists)
                    {
                        cmbReferralProvider.SelectedValue = nSelectedRefProvider;
                    }
                    else
                    {
                        if (cmbReferralProvider.Items.Count == 2)
                        {
                            cmbReferralProvider.SelectedIndex = 1;
                        }
                    }
                  
                    if (c1Insurance.Rows.Count >= 2)
                    {

                        if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCENAME)) == "Self")
                        {
                            //  c1Transaction.Cols[COL_SELFCLAIM].Visible = false;
                            UC_gloBillingTransactionLines.SplitClaimToPatientSetting(false);

                        }
                        else
                        {
                            UC_gloBillingTransactionLines.SplitClaimToPatientSetting(true);
                        }
                    }

                    if (c1Insurance.Rows.Count >= 2 && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                    {

                        switch (GetICDCodeType(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)), UC_gloBillingTransactionLines.getfirstservicelineDos()))
                        {
                            case gloGlobal.gloICD.CodeRevision.ICD10:
                                rbICD10.Checked = true;
                                break;
                            case gloGlobal.gloICD.CodeRevision.ICD9:
                                rbICD9.Checked = true;
                                break;
                            default:
                                rbICD9.Checked = true;
                                break;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (_Transaction != null) { _Transaction.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        private void SetPatientModifiedData(bool LoadPatientBanner)
        {
            DataSet _dsChargesData = null;
            try
            {
                _PatientID = oPatientControl.CmbSelectedPatientID;
                this.nPAccountID = oPatientControl.PAccountID;
                this.nGuarantorID = oPatientControl.GuarantorID;
                this.nAccountPatientID = oPatientControl.AccountPatientID;

                _dsChargesData = gloCharges.GetPatientModifiedData(_PatientID);

                DesignInsuranceGrid();

                if (_dsChargesData != null && _dsChargesData.Tables.Count > 0)
                {
                    FillPatientInsurances(_dsChargesData.Tables[2]);
                    FillReferralProvidersData(_dsChargesData.Tables[0], true);
                    SetPatientProvider();
                    FillClaimWorkerCompData(_dsChargesData.Tables[1]);
                    SetProviderSettings(_dsChargesData.Tables[3]);
                    ShowHideUB();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _dsChargesData.Dispose();
                _dsChargesData = null;
            }

        }

        private void FillStandardFeeSchedules(DataTable dtStandardFeeSchedule)
        {
            if (dtStandardFeeSchedule != null)
            {
                if (dtStandardFeeSchedule.Rows.Count > 0)
                {
                    cmbFeeSchedule.BeginUpdate();
                    cmbFeeSchedule.DataSource = dtStandardFeeSchedule.Copy();
                    cmbFeeSchedule.ValueMember = dtStandardFeeSchedule.Columns["nFeeScheduleID"].ColumnName;
                    cmbFeeSchedule.DisplayMember = dtStandardFeeSchedule.Columns["sFeeScheduleName"].ColumnName;
                    cmbFeeSchedule.SelectedIndex = 0;
                    cmbFeeSchedule.EndUpdate();
                }

            }
        }

        private void SetAutoClaim(DataTable dtAutoClaim)
        {
            if (dtAutoClaim != null && dtAutoClaim.Rows.Count > 0)
            {
                PatientHasAutoClaim = Convert.ToBoolean(dtAutoClaim.Rows[0]["bIsAutoClaim"]);
            }
        }

        private void SetPriorAuthorization(DataTable dtPriorAuthorization)
        {
            if (dtPriorAuthorization != null && dtPriorAuthorization.Rows.Count > 0)
            {
                PatientHasPriorAuthorization = Convert.ToBoolean(dtPriorAuthorization.Rows[0]["PriorAuthorization"]);
            }
        }

        private void SetPatientCases(DataTable dtCases)
        {
            var CaseCount = 0;
            try
            {
                mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (dtCases != null && dtCases.Rows.Count > 0)
                {
                    if (mskClaimDate.Text != string.Empty)
                    {
                        mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        CaseCount = (from _dt in dtCases.AsEnumerable()
                                     where _dt.Field<DateTime?>("dtEnddate") >= Convert.ToDateTime(mskClaimDate.Text)
                                       || _dt.Field<DateTime?>("dtEnddate") == null
                                     select _dt).Count();
                    }
                    else
                    {
                        CaseCount = (from _dt in dtCases.AsEnumerable()
                                     where _dt.Field<DateTime?>("dtEnddate") >= DateTime.Now.Date
                                     || _dt.Field<DateTime?>("dtEnddate") == null
                                     select _dt).Count();
                    }

                    if (CaseCount > 0)
                    {
                        PatientHasCases = true;
                    }
                    else
                    {
                        PatientHasCases = false;
                    }

                }
                else
                {
                    PatientHasCases = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }

        }

        private void FillStatesData()
        {
            DataTable dtStates = null;
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

        private void FillReportingProviderTypes()
        {
            DataTable _dtReportingProviderTypes = null;
            _dtReportingProviderTypes = gloGlobal.gloPMMasters.GetProviderReportingQualifier();

            if (_dtReportingProviderTypes != null && _dtReportingProviderTypes.Rows.Count > 0)
            {
                cmbProviderType.BeginUpdate();
                cmbProviderType.DataSource = _dtReportingProviderTypes;
                cmbProviderType.DisplayMember = _dtReportingProviderTypes.Columns["sDescription"].ColumnName;
                cmbProviderType.ValueMember = _dtReportingProviderTypes.Columns["sQualifier"].ColumnName;
                cmbProviderType.EndUpdate();

                if (DefaultProviderQualifierCode != null && DefaultProviderQualifierCode.Trim() != "")
                { cmbProviderType.SelectedValue = DefaultProviderQualifierCode; }
                else
                { cmbProviderType.SelectedValue = "DN"; } //DN-Referring 
            }


        }

        //FillClaimDateQualifiers
        private void FillOtherClaimDateQualifiers()
        {
            DataTable _dtClaimDatesQualifiers = null;
            _dtClaimDatesQualifiers = gloGlobal.gloPMMasters.GetClaimDatesQualifiers();

            if (_dtClaimDatesQualifiers != null && _dtClaimDatesQualifiers.Rows.Count > 0)
            {
                cmbBox15DateQualifier.BeginUpdate();
                cmbBox15DateQualifier.DataSource = _dtClaimDatesQualifiers;
                cmbBox15DateQualifier.DisplayMember = _dtClaimDatesQualifiers.Columns["sDescription"].ColumnName;
                cmbBox15DateQualifier.ValueMember = _dtClaimDatesQualifiers.Columns["sQualifier"].ColumnName;
                cmbBox15DateQualifier.EndUpdate();

                if (DefaultDateQualifierCode != null && DefaultDateQualifierCode.Trim() != "")
                {
                    cmbBox15DateQualifier.SelectedValue = DefaultDateQualifierCode;   
                }
            }

        }        

        private void FillClaimDateQualifiers()
        {
            DataTable _dtClaimDatesQualifiers = null;
            _dtClaimDatesQualifiers = gloGlobal.gloPMMasters.GetBox14DatesQualifiers();

            if (_dtClaimDatesQualifiers != null && _dtClaimDatesQualifiers.Rows.Count > 0)
            {
                cmbBox14DateQualifier.BeginUpdate();
                cmbBox14DateQualifier.DataSource = _dtClaimDatesQualifiers;
                cmbBox14DateQualifier.DisplayMember = _dtClaimDatesQualifiers.Columns["sDescription"].ColumnName;
                cmbBox14DateQualifier.ValueMember = _dtClaimDatesQualifiers.Columns["sQualifier"].ColumnName;
                cmbBox14DateQualifier.EndUpdate();

                //Set Onset as default as per previous implementation
                cmbBox14DateQualifier.SelectedValue = "431";
            }

        }

        private void FillFacilitiesData()
        {
            DataTable dtFacilities = null;

            dtFacilities = gloCharges.GetCachedFacilities();

            if (dtFacilities != null)
            {
                cmbFacility.BeginUpdate();
                cmbFacility.DataSource = dtFacilities;
                cmbFacility.ValueMember = dtFacilities.Columns["sFacilityCode"].ColumnName;
                cmbFacility.DisplayMember = dtFacilities.Columns["sFacilityName"].ColumnName;
                cmbFacility.EndUpdate();
                //cmbFacility.SelectedIndex = -1;
            }
        }
        private void FillClaimReportingCategory(DataTable dtClaimReportingCategory)
        {
            if (dtClaimReportingCategory != null)
            {
                DataRow dr = dtClaimReportingCategory.NewRow();
                dr["nClaimReportingCategoryID"] = 0;
                dr["sDescription"] = "";
                dtClaimReportingCategory.Rows.InsertAt(dr, 0);
                if (dtClaimReportingCategory.Rows.Count > 0)
                {
                    cmbClaimCategory.BeginUpdate();
                 
                    cmbClaimCategory.DataSource = null;
                    cmbClaimCategory.Items.Clear();
                    cmbClaimCategory.DataSource = dtClaimReportingCategory.Copy();
                    cmbClaimCategory.ValueMember = dtClaimReportingCategory.Columns["nClaimReportingCategoryID"].ColumnName;
                    cmbClaimCategory.DisplayMember = dtClaimReportingCategory.Columns["sDescription"].ColumnName;
                    cmbClaimCategory.EndUpdate();
                }
            }
        }
        private void SetFacilitySettingsData()
        {
            try
            {
                if (cmbFacility.SelectedItem != null)
                {
                    DataRowView _drFacilityView = null;
                    FacilityType oFacilityType = FacilityType.None;

                    _drFacilityView = (DataRowView)cmbFacility.SelectedItem;

                    if (_drFacilityView.Row != null)
                    {
                        //..Get Facility CLIANo if present
                        if (Convert.ToString(_drFacilityView.Row["sCLIANo"]).Trim() != "")
                        {
                            UC_gloBillingTransactionLines.FacilityCLIANo = Convert.ToString(_drFacilityView.Row["sCLIANo"]).Trim();
                        }
                        else
                        {
                            UC_gloBillingTransactionLines.FacilityCLIANo = "";
                        }

                        //..Get Facility POS if present
                        if (Convert.ToString(_drFacilityView.Row["nPOSID"]).Trim() != "")
                        {
                            UC_gloBillingTransactionLines.FacilityPOS = Convert.ToInt64(_drFacilityView.Row["nPOSID"]);
                        }
                        else
                        {
                            UC_gloBillingTransactionLines.FacilityPOS = 0;
                        }

                        //..Get Facility Type
                        if (Convert.ToString(_drFacilityView.Row["nFacilityType"]).Trim() != "")
                        {
                            oFacilityType = ((FacilityType)Convert.ToInt32(_drFacilityView.Row["nFacilityType"]));
                        }

                        if (oFacilityType == FacilityType.Facility)
                        {
                            chkFacilityFeeSchedule.Checked = true;
                            chkNonFacilityCharges.Checked = false;
                        }
                        else if (oFacilityType == FacilityType.NonFacility)
                        {
                            chkNonFacilityCharges.Checked = true;
                            chkFacilityFeeSchedule.Checked = false;
                        }
                        else
                        {
                            //..Read the default settings for fee type.
                            oFacilityType = DefaultFeeTypeCharges;

                            if (oFacilityType == FacilityType.Facility)
                            {
                                chkFacilityFeeSchedule.Checked = true;
                                chkNonFacilityCharges.Checked = false;
                            }
                            else if (oFacilityType == FacilityType.NonFacility)
                            {
                                chkNonFacilityCharges.Checked = true;
                                chkFacilityFeeSchedule.Checked = false;
                            }
                            else
                            {
                                chkNonFacilityCharges.CheckedChanged -= chkNonFacilityCharges_CheckedChanged;
                                chkFacilityFeeSchedule.CheckedChanged -= chkFacilityFeeSchedule_CheckedChanged;
                                chkNonFacilityCharges.Checked = false;
                                chkFacilityFeeSchedule.Checked = false;
                                chkNonFacilityCharges.CheckedChanged += chkNonFacilityCharges_CheckedChanged;
                                chkFacilityFeeSchedule.CheckedChanged += chkFacilityFeeSchedule_CheckedChanged;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void FillChargeTray(DataTable dtLastTray)
        {
            DataTable dtChargesTray = null;
            dtChargesTray = gloCharges.GetCachedChargeTrays();

            //..If user trays are present then take next action else show pop-up
            if (dtChargesTray != null && dtChargesTray.Rows.Count > 0)
            {
                Int64 _userDefaultTrayId = 0;
                string _userDefaultTrayDescription = "";
                Int64 _userLastSelectedTrayId = 0;
                string _userLastSelectedTrayDescription = "";
                DataRow[] dr;

                //..Get the users default tray from the user trays list
                try
                {
                    dr = dtChargesTray.Select("bIsDefault = 1 and nUserID = " + _UserID + "");
                }
                catch //(Exception ex)
                {
                    if (dtChargesTray != null)
                    {
                        dtChargesTray.Dispose();
                        dtChargesTray = null;
                    }
                    throw;
                }

                //..If default tray present take next action else show pop-up
                if (dr.Length > 0)
                {
                    _userDefaultTrayId = Convert.ToInt64(dr[0]["nChargeTrayID"]);
                    _userDefaultTrayDescription = Convert.ToString(dr[0]["sDescription"]);
                }

                dr = null;

                //...Get the last selected tray id and description
                if (dtLastTray != null && dtLastTray.Rows.Count > 0)
                {
                    if (dtLastTray.Rows[0]["LastSelectedTrayId"] != DBNull.Value && Convert.ToString(dtLastTray.Rows[0]["LastSelectedTrayId"]).Trim() != "")
                    { _userLastSelectedTrayId = Convert.ToInt64(dtLastTray.Rows[0]["LastSelectedTrayId"]); }

                    if (_userLastSelectedTrayId > 0)
                    {
                        dr = dtChargesTray.Select("nChargeTrayID = " + _userLastSelectedTrayId + " AND nUserID = " + _UserID + "");

                        if (dr.Length > 0)
                        {
                            _userLastSelectedTrayId = Convert.ToInt64(dr[0]["nChargeTrayID"]);
                            _userLastSelectedTrayDescription = Convert.ToString(dr[0]["sDescription"]);
                        }
                    }
                }

                //..Compare if the user's default tray & last selected tray, if same then set the default else show pop-up
                if (_userDefaultTrayId == _userLastSelectedTrayId)
                {
                    SelectedChargeTrayID = _userDefaultTrayId;
                    SelectedChargeTrayDescription = _userDefaultTrayDescription;
                    SelectedChargeTray = _userDefaultTrayDescription;
                    if (dtChargesTray != null)
                    {
                        dtChargesTray.Dispose();
                        dtChargesTray = null;
                    }
                    return;
                }

            }
            if (dtChargesTray != null)
            {
                dtChargesTray.Dispose();
                dtChargesTray = null;
            }
            _ShowSelectTray = true;

        }

        private void FillProviderData()
        {
            DataTable dtProviders = null;
            dtProviders = gloCharges.GetCachedProviders();

            if (dtProviders != null)
            {
                if (dtProviders.Rows.Count > 0)
                {
                    cmbBillingProvider.BeginUpdate();
                    cmbBillingProvider.DataSource = dtProviders;
                    cmbBillingProvider.ValueMember = dtProviders.Columns["nProviderID"].ColumnName;
                    cmbBillingProvider.DisplayMember = dtProviders.Columns["sProviderName"].ColumnName;
                    cmbBillingProvider.EndUpdate();
                    cmbBillingProvider.SelectedIndex = 0;
                }
            }
        }

        private void FillReferralProvidersData(DataTable dtReferralProviders, bool fillBlankItem)
        {

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
                }
            }
        }

        private void SetPatientProvider()
        {
            if (oPatientControl.PatientProviderID > 0)
            {
                _PatientPoviderID = Convert.ToInt64(oPatientControl.PatientProviderID);
                _PatientProviderName = Convert.ToString(oPatientControl.PatientProviderName);
            }
        }

        private void FillClaimWorkerCompData(DataTable dtWorkerCompClaim)
        {
            if (dtWorkerCompClaim != null)
            {
                cmbClaimNo.BeginUpdate();
                cmbClaimNo.DataSource = dtWorkerCompClaim.Copy();
                cmbClaimNo.ValueMember = dtWorkerCompClaim.Columns["sClaimno"].ColumnName;
                cmbClaimNo.DisplayMember = dtWorkerCompClaim.Columns["sClaimno"].ColumnName;
                cmbClaimNo.EndUpdate();
                cmbClaimNo.SelectedIndex = -1;
            }
        }

        private void SetProviderSettings(DataTable dtProviderSettings)
        {

            if (dtProviderSettings != null)
            {
                foreach (DataRow dr in dtProviderSettings.Rows)
                {
                    switch (Convert.ToString(dr["sName"]).Trim())
                    {
                        case "TypeOfService":
                            _DefaultTOSID = Convert.ToInt64(dr["sValue"]);
                            break;
                        case "PlaceOfService":
                            _DefaultPOSID = Convert.ToInt64(dr["sValue"]);
                            break;
                        case "BillingProvider":

                            if (_dsLastClaimDetails != null && _dsLastClaimDetails.Tables[0].Rows.Count > 0)
                            {
                                if (_bDefaultBillingProvider)
                                {
                                    cmbBillingProvider.SelectedValue = Convert.ToInt64(_dsLastClaimDetails.Tables[0].Rows[0]["nTransactionProviderID"]);
                                }
                                else if (Convert.ToInt64(dr["sValue"]) > 0)
                                {
                                    cmbBillingProvider.SelectedValue = Convert.ToInt64(dr["sValue"]);
                                }
                                else if (_EMRProviderId > 0)
                                {
                                    cmbBillingProvider.SelectedValue = _EMRProviderId;
                                }
                                else
                                {
                                    cmbBillingProvider.SelectedValue = PatientPoviderID;
                                }
                            }
                            else if (Convert.ToInt64(dr["sValue"]) > 0)
                            {
                                cmbBillingProvider.SelectedValue = Convert.ToInt64(dr["sValue"]);
                            }
                            else if (_EMRProviderId > 0)
                            {
                                cmbBillingProvider.SelectedValue = _EMRProviderId;
                            }
                            else
                            {
                                cmbBillingProvider.SelectedValue = PatientPoviderID;
                            }
                            break;
                        case "RenderingProvider":
                            if (Convert.ToInt64(dr["sValue"]) > 0)
                            {
                                _RenderingProviderID = Convert.ToInt64(dr["sValue"]);
                            }
                            else
                            {
                                _RenderingProviderID = 0;
                            }
                            break;
                        case "Facility":
                            if (_dsLastClaimDetails != null && _dsLastClaimDetails.Tables[0].Rows.Count > 0)
                            {
                                if (_bDefaultFacility)
                                {
                                    string sFacilityCode = Convert.ToString(_dsLastClaimDetails.Tables[0].Rows[0]["sFacilityCode"]);
                                    Int64 _nFacilityID = 0;

                                    if (cmbFacility.DataSource != null)
                                    {
                                        DataTable _dtFacility = null;
                                        _dtFacility = ((DataTable)cmbFacility.DataSource).Copy();

                                        if (_dtFacility != null && _dtFacility.Rows.Count > 0)
                                        {
                                            DataRow[] _drFacilityRow = null;
                                            _drFacilityRow = _dtFacility.Select("sFacilityCode = " + sFacilityCode + "");

                                            if (_drFacilityRow.Length > 0)
                                            {
                                                _nFacilityID = Convert.ToInt64(_drFacilityRow[0]["nFacilityID"]);
                                            }
                                            _drFacilityRow = null;
                                        }

                                        if (_dtFacility != null) { _dtFacility.Dispose(); }
                                    }
                                    cmbFacility.SelectedValue = Convert.ToInt64(_nFacilityID);
                                 
                                }
                                else if (Convert.ToString(dr["sValue"]).Trim() != "")
                                {
                                    cmbFacility.SelectedValue = Convert.ToString(dr["sValue"]);
                                }

                            }
                            else if (Convert.ToString(dr["sValue"]).Trim() != "")
                            {
                                cmbFacility.SelectedValue = Convert.ToString(dr["sValue"]);
                            }
                            break;
                        case "Fee Schedule":
                            if (Convert.ToString(dr["sValue"]).Trim() != "")
                            {
                                _EMRFeeScheduleID = Convert.ToInt64(dr["sValue"]);
                            }
                            break;
                    }

                }
            }
        }

        private void FillPatientInsurances(DataTable dtPatientInsurances)
        {
            c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
            bool _IsPrimaryPresent = false;
            bool _HasInsurance = true;
            int _CntPrimary = 0;
            int _CntSec = 0;
            int _CntTertiory = 0;
            _IsWorkercimp = false; 
            try
            {
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
                            String _sInsDate ="";
                            if (Convert.ToString(dtPatientInsurances.Rows[i]["dtStartDate"]).Trim().Length > 0)
                                _sInsDate = Convert.ToString(dtPatientInsurances.Rows[i]["dtStartDate"]);
                            if (Convert.ToString(dtPatientInsurances.Rows[i]["dtStartDate"]).Trim().Length > 0 || Convert.ToString(dtPatientInsurances.Rows[i]["dtEndDate"]).Trim().Length > 0)
                                _sInsDate = _sInsDate +"-";
                            if (Convert.ToString(dtPatientInsurances.Rows[i]["dtEndDate"]).Trim().Length > 0)
                                _sInsDate =_sInsDate+ Convert.ToString(dtPatientInsurances.Rows[i]["dtEndDate"]);
                            c1Insurance.SetData(rowIndex, COL_INSSTARTENDDATE, _sInsDate);
                          
                            if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bWorkersComp"]) == true)
                            {
                                if (Convert.ToString(dtPatientInsurances.Rows[i]["sNote"]) != "")
                                {
                                    c1Insurance.SetData(rowIndex, COL_INSCOMMENT, "WC-" + Convert.ToString(dtPatientInsurances.Rows[i]["sNote"]));
                                }
                                else
                                {
                                    c1Insurance.SetData(rowIndex, COL_INSCOMMENT, "WC" + Convert.ToString(dtPatientInsurances.Rows[i]["sNote"]));
                                }                                
                                _IsWorkercimp = true;
                            }
                            else
                            {
                                if (Convert.ToString(dtPatientInsurances.Rows[i]["sNote"]) != "")
                                {
                                    c1Insurance.Cols[COL_INSCOMMENT].Visible = true;
                                    c1Insurance.SetData(rowIndex, COL_INSCOMMENT, Convert.ToString(dtPatientInsurances.Rows[i]["sNote"]));
                                    _IsWorkercimp = true;
                                }
                                else
                                {
                                    c1Insurance.Cols[COL_INSCOMMENT].Visible = false; 
                                }
                            }
                            if (_IsWorkercimp)
                            {
                                c1Insurance.Cols[COL_INSCOMMENT].Visible = true;
                            }
                            c1Insurance.SetData(rowIndex, COL_INSURANCERESPONSIBILITY, Convert.ToString(""));
                            c1Insurance.SetData(rowIndex, COL_INSURANCEPARTY, Convert.ToString("X"));

                            if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                            {
                                c1Insurance.SetData(rowIndex, COL_SELECT, true);
                                _CntPrimary = _CntPrimary + 1;
                                _IsPrimaryPresent = true;
                            }
                            else if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Secondary.GetHashCode())
                            {
                                c1Insurance.SetData(rowIndex, COL_SELECT, true);
                                _CntSec = _CntSec + 1;
                                _IsPrimaryPresent = true;
                            }
                            else if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Tertiary.GetHashCode())
                            {
                                c1Insurance.SetData(rowIndex, COL_SELECT, true);
                                _CntTertiory = _CntTertiory + 1;
                                _IsPrimaryPresent = true;
                            }

                            c1Insurance.SetData(rowIndex, COL_INSURANCECOPAYAMT, Convert.ToDecimal(dtPatientInsurances.Rows[i]["CoPay"]));

                            if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bWorkersComp"]) == true)
                                c1Insurance.SetData(rowIndex, COL_INSURANCEWORKERCOMP, true);

                            if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bAutoClaim"]) == true)
                                c1Insurance.SetData(rowIndex, COL_INSURANCEAUTOCLAIM, "Auto Claim");

                            c1Insurance.SetData(rowIndex, COL_INSURANCECONTACTID, Convert.ToString(dtPatientInsurances.Rows[i]["nContactID"])); //
                            c1Insurance.SetData(rowIndex, COL_INSURANCEPLANONHOLD, Convert.ToBoolean(dtPatientInsurances.Rows[i]["IsPlanOnHold"]));
                            c1Insurance.SetData(rowIndex, COL_ISINSTITUTIONALBILLING, Convert.ToBoolean(dtPatientInsurances.Rows[i]["IsInstitutionalBilling"]));
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
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCENAME, "Self"); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEID, "0"); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSSELFMODE, PayerMode.Self.GetHashCode()); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCECONTACTID, 0);
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEPLANONHOLD, null);
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_ISINSTITUTIONALBILLING, 0);

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
                c1Insurance.Cols[COL_INSURANCECOPAYAMT].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCECONTACTID].AllowEditing = false;

                //To Remove the Previous Flag
                for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                {
                    c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                }

                if (_CntSec > 1 || _CntTertiory > 1)
                {
                    _CntPrimary = 2;
                }

                if (_HasInsurance == false)
                {
                    _CntPrimary = 1;
                }


                if (_CntPrimary <= 1)
                {
                    System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                    c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, imgFlag);

                    DefaultSelfPayFeeSchedule();                    
                }
                c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (!bIsCopiedClaim)
                {
                    SetInsuranceResponsibility(_CntPrimary);
                }
                ChangeInsuranceGridColor();
            }
            c1Insurance.Select(1, 1);
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

        

        private void CheckForPatientPriorAuthorization()
        {
            try
            {
                //if (clsgloPriorAuthorization.HasPriorAuthorization(PatientID))
                if (this.PatientHasPriorAuthorization)
                {
                    if ((Convert.ToString(txtPriorAuthorizationNo.Tag) == "") || (txtPriorAuthorizationNo.Tag == null))
                    {
                        txtPriorAuthorizationNo.Text = "<available>";
                        txtPriorAuthorizationNo.TextAlign = HorizontalAlignment.Center;
                        txtPriorAuthorizationNo.ForeColor = Color.Maroon;
                        //lblAuthorizationAvailable.Visible = true; 
                    }
                    else
                    {
                        txtPriorAuthorizationNo.TextAlign = HorizontalAlignment.Left;
                        txtPriorAuthorizationNo.ForeColor = Color.Black;
                        //lblAuthorizationAvailable.Visible = false; 
                    }
                }
                else
                {
                    txtPriorAuthorizationNo.Text = "";
                    //*********clear prior authorisation tag if no prior authorization present   18 sept 2010 5060 hot fix Start
                    txtPriorAuthorizationNo.Tag = "";
                    //*********clear prior authorisation tag if no prior authorization present   18 sept 2010 5060 hot fix End 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void CheckForPatientCases()
        {
            try
            {
                if (this.PatientHasCases)
                {
                    if ((Convert.ToString(txtCases.Tag) == "") || (txtCases.Tag == null))
                    {
                        txtCases.Text = "<available>";
                        txtCases.TextAlign = HorizontalAlignment.Center;
                        txtCases.ForeColor = Color.Maroon;
                    }
                    else
                    {
                        txtCases.TextAlign = HorizontalAlignment.Left;
                        txtCases.ForeColor = Color.Black;
                    }
                }
                else
                {
                    if (txtCases.Text.Contains("<available>"))
                    {
                        txtCases.Text = "";
                        txtCases.Tag = null;
                    }

                    txtCases.TextAlign = HorizontalAlignment.Left;
                    txtCases.ForeColor = Color.Black;
                }
                //else
                //{
                //    txtCases.Text = "";                 
                //    txtCases.Tag = null;                
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            oPatientControl.FillDetails(PatientId, gloStripControl.FormName.NewCharges);
            //oPatientControl.IsStripLoadedAfterClaimSave = false;
            _PatientID = oPatientControl.PatientID;
            this.nPAccountID = oPatientControl.PAccountID;
            this.nGuarantorID = oPatientControl.GuarantorID;
            this.nAccountPatientID = oPatientControl.AccountPatientID;
            if (bIsCopiedClaim)
            {
                oPatientControl.DisablePatientChange = true;
            }
        }

        private void LoadPatientStripForCopyClaim(Int64 PatientId, Int64 nAccountID, bool SearchEnable)
        {
            oPatientControl.FillDetails(PatientId, nAccountID, gloStripControl.FormName.ModifyCharges);
            this.PatientID = oPatientControl.PatientID;
            this.nPAccountID = oPatientControl.PAccountID;
            this.nGuarantorID = oPatientControl.GuarantorID;
            this.nAccountPatientID = oPatientControl.AccountPatientID;
            oPatientControl.DisablePatientChange = true;
        }

        private void ReadApplicationSettings()
        {
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

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }

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

            #region " Retrieve ClaimValidation from AppSettings "

            if (appSettings["ClaimValidationSetting"] != null)
            {
                if (appSettings["ClaimValidationSetting"] != "")
                {
                    if (appSettings["ClaimValidationSetting"] == "Alpha2")
                    {
                        _claimValidationService = ClaimValidationService.Alpha2;
                    }
                    else if (appSettings["ClaimValidationSetting"] == "YOST")
                    {
                        _claimValidationService = ClaimValidationService.YOST;
                    }
                    else if (appSettings["ClaimValidationSetting"] == "None")
                    {
                        _claimValidationService = ClaimValidationService.None;
                    }
                }
            }
            else
            { _claimValidationService = ClaimValidationService.None; }

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
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

            switch (ClaimValidationServiceType)
            {
                case ClaimValidationService.YOST:
                    tls_btnValidateProcedure.Text = "Yost";
                    tls_btnValidateProcedure.Visible = true;
                    break;
                case ClaimValidationService.Alpha2:
                    tls_btnValidateProcedure.Text = "Alpha II";
                    tls_btnValidateProcedure.Visible = true;
                    break;
                case ClaimValidationService.None:
                    tls_btnValidateProcedure.Visible = false;
                    break;
                default:
                    break;
            }
        }

        private void SetDefaultFeeChargesSettings(DataTable dtDefaultChargesFeeType)
        {
            _defaultFeeTypeCharges = FacilityType.None;

            if (dtDefaultChargesFeeType != null && dtDefaultChargesFeeType.Rows.Count > 0)
            {
                if (dtDefaultChargesFeeType.Rows[0]["DefaultFeeCharges"] != DBNull.Value && Convert.ToString(dtDefaultChargesFeeType.Rows[0]["DefaultFeeCharges"]).Trim() != "")
                {
                    this._defaultFeeTypeCharges = (FacilityType)Convert.ToInt32(dtDefaultChargesFeeType.Rows[0]["DefaultFeeCharges"]);
                }
            }
        }

        private void SyncronizeDxGridWithServiceline()
        {
            List<string> lstDx = new List<string>();
            ArrayList _arrDx = UC_gloBillingTransactionLines.GetUniqueDx();
            for (int iCount = c1Dx.Rows.Count - 1; iCount >= 1; iCount--)
            {
                if (!_arrDx.Contains(Convert.ToString(c1Dx.GetData(iCount, COL_DX_CODE)).Trim()))
                {
                    lstDx.Add(Convert.ToString(c1Dx.GetData(iCount, COL_DX_CODE)));
                }
            }

            foreach (string curDx in lstDx)
            {
                int iRow = c1Dx.FindRow(curDx, 1, COL_DX_CODE, false);
                if (iRow != -1)
                {
                    c1Dx.Rows.Remove(iRow);
                }
            }
            _arrDx.Clear();
            lstDx.Clear();
        }

        private void SetUserLastChargeCloseDate(DataTable dtCloseDate)
        {
            if (dtCloseDate != null && dtCloseDate.Rows.Count > 0)
            {
                if (dtCloseDate.Rows[0]["nCloseDayDate"] != DBNull.Value && Convert.ToString(dtCloseDate.Rows[0]["nCloseDayDate"]).Trim() != ""
                    && Convert.ToInt64(dtCloseDate.Rows[0]["nCloseDayDate"]) > 0)
                {
                    mskClaimDate.Text = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(dtCloseDate.Rows[0]["nCloseDayDate"]));
                }
            }
        }

        private void SetSupervisorOptionSetting(DataTable dtSupervisor)
        {
            if (dtSupervisor != null && dtSupervisor.Rows.Count > 0)
            {
                if (dtSupervisor.Rows[0]["sSettingsValue"] != DBNull.Value)
                {
                    bEnableSupervisorOption = Convert.ToBoolean(dtSupervisor.Rows[0]["sSettingsValue"]);
                }
            }
        }

        private void SetInitialTreatmentSetting(DataTable dtInitialTreatment)
        {
            if (dtInitialTreatment != null && dtInitialTreatment.Rows.Count > 0)
            {
                if (dtInitialTreatment.Rows[0]["sSettingsValue"] != DBNull.Value)
                {
                    bShowInitialTreatmentDate = Convert.ToBoolean(dtInitialTreatment.Rows[0]["sSettingsValue"]);
                }
            }
        }

        private void SetDefaultQualifierForProvider(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["sSettingsValue"] != DBNull.Value)
                {
                    DefaultProviderQualifierCode = Convert.ToString(dt.Rows[0]["sSettingsValue"]);
                }
            }
        }

        //SetDefaultDateQualifier added on 12182013 Sameer
        private void SetDefaultDateQualifier(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["sSettingsValue"] != DBNull.Value)
                {
                    DefaultDateQualifierCode = Convert.ToString(dt.Rows[0]["sSettingsValue"]);
                }
            }
        }

        private void SetAnesthesiaSetting(DataTable dtAnesthesia)
        {
            bIsAnesthesiaEnabled = false;
            if (dtAnesthesia != null && dtAnesthesia.Rows.Count > 0)
            {
                if (dtAnesthesia.Rows[0]["sSettingsValue"] != DBNull.Value)
                {
                    bIsAnesthesiaEnabled = Convert.ToBoolean(dtAnesthesia.Rows[0]["sSettingsValue"]);
                }
            }
        }

        private void SetUBAdminSetting(DataTable dtUBAdminSetting)
        {
            if (dtUBAdminSetting != null && dtUBAdminSetting.Rows.Count > 0)
            {
                IsUBEnabled = Convert.ToBoolean(dtUBAdminSetting.Rows[0]["IsUBEnabled"]);
                TypeOfBill = Convert.ToString(dtUBAdminSetting.Rows[0]["sTypeOfBill"]);
            }
            else
            {
                IsUBEnabled = false;
            }
        }

        private void SetUB04AdminSetting(DataTable dtUBAdminSetting)
        {
            if (dtUBAdminSetting != null && dtUBAdminSetting.Rows.Count > 0)
            {
                for (int i = 0; i < dtUBAdminSetting.Rows.Count; i++)
                {
                    if (Convert.ToString(dtUBAdminSetting.Rows[i]["sSettingsName"]) == "UB04_AdmisionType")
                    {
                         AdmitType = Convert.ToString(dtUBAdminSetting.Rows[i]["sSettingsValue"]);
                    }
                    if (Convert.ToString(dtUBAdminSetting.Rows[i]["sSettingsName"]) == "UB04_DischargeStatus")
                    {
                         DischargeStatus = Convert.ToString(dtUBAdminSetting.Rows[i]["sSettingsValue"]);
                    }
                }
               

            }
            
        }

        private void ShowHideUB()
        {
            try
            {
                if (c1Insurance.Cols != null && c1Insurance.Cols.Count > 13)
                {
                    bool _IsInstitutionalPlan = Convert.ToBoolean(c1Insurance.GetData(1, COL_ISINSTITUTIONALBILLING));
                    if (IsUBEnabled == true && _IsInstitutionalPlan == true)
                    {
                        tls_btnUB04Data.Visible = true;
                    }
                    else
                    {
                        tls_btnUB04Data.Visible = false;
                    }
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Insurance grid has no institutional column. ", false);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void LoadDefaultBillingSettings()
        {
            try
            {

                //..1 - If Rendering Provider ID is present & set (from default provider settings for selected provider on form) 
                //      then set the Rendering Provider from the set ID (DefaultRenderingProviderID)

                //..2 - If Rendering Provider ID is not set then set the patient provider as rendering provider

                //..3 - If Patient ProviderID not set (will never be a case) check for what is the Billing Provider set in
                //      the drop down list of Billing Provider and set the Rendering Provider as the selected billing provider

                //..Note : The billing provider is set by reading the settings made for the patient provider in gloPMAdmin
                //  


                if (UC_gloBillingTransactionLines != null)
                {
                    string _renderingProviderName = "";

                    if (this.RenderingProviderID > 0)
                    {
                        #region " Set Rendering Provider ID & Name to Billing Control "

                        //..Search for Rendering provider id in the billing provider drop down list 
                        //  and get the name of the provider from there instead of fecting again from database

                        if (cmbBillingProvider.DataSource != null)
                        {
                            DataTable _dtBillingProvider = null;

                            _dtBillingProvider = ((DataTable)cmbBillingProvider.DataSource).Copy();

                            if (_dtBillingProvider != null && _dtBillingProvider.Rows.Count > 0)
                            {
                                DataRow[] _drProviderRow = null;

                                _drProviderRow = _dtBillingProvider.Select("nProviderID = " + this.RenderingProviderID + "");

                                if (_drProviderRow.Length > 0)
                                {
                                    _renderingProviderName = Convert.ToString(_drProviderRow[0]["sProviderName"]);
                                }
                                _drProviderRow = null;
                            }

                            if (_dtBillingProvider != null) { _dtBillingProvider.Dispose(); }
                        }

                        UC_gloBillingTransactionLines.DefaultRenderingProviderID = 0;
                        UC_gloBillingTransactionLines.DefaultRenderringProviderName = string.Empty;
                        foreach (DataRowView row in cmbBillingProvider.Items)
                        {
                            if (Convert.ToInt64(row[0]) == this.RenderingProviderID)
                            {
                                UC_gloBillingTransactionLines.DefaultRenderingProviderID = this.RenderingProviderID;
                                UC_gloBillingTransactionLines.DefaultRenderringProviderName = _renderingProviderName;
                                break;
                            }
                        }

                        //UC_gloBillingTransactionLines.DefaultRenderingProviderID = this.RenderingProviderID;
                        //UC_gloBillingTransactionLines.DefaultRenderringProviderName = _renderingProviderName;
                        UC_gloBillingTransactionLines.BillingProviderID = Convert.ToInt64(cmbBillingProvider.SelectedValue);

                        UC_gloBillingTransactionLines.IsSettingsForProvider = true;

                        #endregion " Set Rendering Provider ID & Name to Billing Control "
                    }
                    else if (this.PatientPoviderID > 0)
                    {
                        #region " Set the Patient Provider as the Rendering Provider to Billing Control "

                        if (_EMRProviderId > 0)
                        {
                            if (cmbBillingProvider.DataSource != null)
                            {
                                DataTable _dtBillingProvider = null;

                                _dtBillingProvider = ((DataTable)cmbBillingProvider.DataSource).Copy();

                                if (_dtBillingProvider != null && _dtBillingProvider.Rows.Count > 0)
                                {
                                    DataRow[] _drProviderRow = null;

                                    _drProviderRow = _dtBillingProvider.Select("nProviderID = " + _EMRProviderId + "");

                                    if (_drProviderRow.Length > 0)
                                    {
                                        _renderingProviderName = Convert.ToString(_drProviderRow[0]["sProviderName"]);
                                    }
                                    _drProviderRow = null;
                                }

                                if (_dtBillingProvider != null) { _dtBillingProvider.Dispose(); }
                            }


                            UC_gloBillingTransactionLines.DefaultRenderingProviderID = 0;
                            UC_gloBillingTransactionLines.DefaultRenderringProviderName = string.Empty;
                            foreach (DataRowView row in cmbBillingProvider.Items)
                            {
                                if (Convert.ToInt64(row[0]) == _EMRProviderId)
                                {
                                    UC_gloBillingTransactionLines.DefaultRenderingProviderID = _EMRProviderId;
                                    UC_gloBillingTransactionLines.DefaultRenderringProviderName = _renderingProviderName;
                                    break;
                                }
                            }

                            //UC_gloBillingTransactionLines.DefaultRenderingProviderID = _EMRProviderId;
                            //UC_gloBillingTransactionLines.DefaultRenderringProviderName = _renderingProviderName;
                            UC_gloBillingTransactionLines.BillingProviderID = Convert.ToInt64(cmbBillingProvider.SelectedValue);
                            UC_gloBillingTransactionLines.IsSettingsForProvider = false;
                        }
                        else
                        {
                            UC_gloBillingTransactionLines.DefaultRenderingProviderID = 0;
                            UC_gloBillingTransactionLines.DefaultRenderringProviderName = string.Empty;
                            foreach (DataRowView row in cmbBillingProvider.Items)
                            {
                                if (Convert.ToInt64(row[0]) == this.PatientPoviderID)
                                {
                                    UC_gloBillingTransactionLines.DefaultRenderingProviderID = this.PatientPoviderID;
                                    UC_gloBillingTransactionLines.DefaultRenderringProviderName = this.PatientProviderName;
                                    break;
                                }
                            }

                            UC_gloBillingTransactionLines.IsSettingsForProvider = false;
                            //UC_gloBillingTransactionLines.DefaultRenderingProviderID = this.PatientPoviderID;
                            //UC_gloBillingTransactionLines.DefaultRenderringProviderName = this.PatientProviderName;
                            UC_gloBillingTransactionLines.BillingProviderID = Convert.ToInt64(cmbBillingProvider.SelectedValue);
                        }

                        #endregion " Set the Patient Provider as the Rendering Provider to Billing Control "
                    }
                    else
                    {
                        #region " Set the selected billing provider as the Rendering Provider to Billing Control "

                        if (cmbBillingProvider != null && cmbBillingProvider.Items.Count > 0)
                        {
                            if (cmbBillingProvider.SelectedIndex != -1)
                            {
                                UC_gloBillingTransactionLines.DefaultRenderingProviderID = Convert.ToInt64(cmbBillingProvider.SelectedValue);
                                UC_gloBillingTransactionLines.DefaultRenderringProviderName = Convert.ToString(cmbBillingProvider.Text);
                                UC_gloBillingTransactionLines.BillingProviderID = Convert.ToInt64(cmbBillingProvider.SelectedValue);
                            }
                        }

                        #endregion " Set the selected billing provider as the Rendering Provider to Billing Control "
                    }
                }

                #region " Set the Facility - Need to check for this code and correct or remove from here"

                if (cmbFacility != null && cmbFacility.Items.Count > 0)
                {
                    if (cmbFacility.SelectedIndex == -1)
                    {
                        //string abc = Convert.ToInt64((System.Data.DataRowView)(cmbFacility.Items[0])).Row.ItemArray[0].ToString());
                        if (bIsFacilityChangeFromModifyPatient && UC_gloBillingTransactionLines._facilityID==Convert.ToInt64(((System.Data.DataRowView)(cmbFacility.Items[0])).Row.ItemArray[0]))
                        {
                            bModifyPOS = true;
                        }
                        cmbFacility.SelectedIndex = 0;
                        bModifyPOS = false;
                    }

                    if (UC_gloBillingTransactionLines != null && cmbFacility.SelectedIndex != -1)
                    {
                        UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                        SetFacilitySettingsData();
                    }
                }

                #endregion " Set the Facility "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ShowHideControls(ShowHideType showhideEventType)
        {
            switch (showhideEventType)
            {
                case ShowHideType.None:
                    break;
                case ShowHideType.EMRTreatment:
                    {
                        #region " EMR Treatement Setup Controls "
                        tls_btnExportToExcel.Visible = true;
                        UC_gloBillingTransactionLines.Enabled = false;
                        chk_SameasBillingProvider.Checked = false;

                        tls_Hold.Visible = false;
                        tls_btnMoreChargeData.Visible = false;
                        tls_Anesthesia.Visible = false;
                        tls_btnMoreClaimData.Visible = false;
                        tls_btnAddLine.Visible = false;
                        tls_btnRemoveLine.Visible = false;
                        tlb_btnSmartTreatment.Visible = false;
                        tlb_AddNotes.Visible = false;
                        tls_btnOK.Visible = false;
                        tlb_LoadExam.Visible = false;
                        tlb_btnExamDXCPT.Visible = false;
                        tls_btnCancel.Visible = false;
                        tls_btnSaveNClose.Visible = false;
                        tls_btnPayment.Visible = false;
                        tlb_AddEMRTreatment.Visible = false;
                        tlb_CloseEMRTreatment.Visible = true;
                        tlb_AddEMRTreatment.Visible = false;
                        tlb_VoidEMRTreatment.Visible = true;
                        tlb_ReportEMRTreatment.Visible = true;
                        tlb_SaveNextTreatment.Visible = false;
                        tlb_CancelEMRTreatment.Visible = false;
                        tls_btnPatAcct.Visible = false;
                        tls_btnUB04Data.Visible = false;
                        tls_btnOnlineCharge.Visible = false;
                        tlb_CloseOnlineCharge.Visible = false;

                        tls_btnValidateProcedure.Visible = false;

                        tlb_AddEPSDT.Visible = false;

                        pnlAlerts.Visible = false;
                        pnlExamCPTDX.SendToBack();
                        pnlEMRExams.Visible = true;
                        pnlEMRExams.BringToFront();
                        oPatientControl.Visible = false;
                        pnlTransactionLines.Visible = false;
                        pnlTransactionOther1.Visible = false;
                        pnlTransactionOther2.Visible = false;
                        panel6.Visible = false;
                        pnlTransactionMaster.Height = this.Height - (pnlToolStrip.Height) - 5;

                        _IsEMRTreatmentStart = true;
                        _IsEMRTreatmentStop = false;
                        tlsICD10CoddingRules.Visible = false;


                        #endregion " EMR Treatement Setup Controls "
                    }
                    break;
                case ShowHideType.CancelEMRTreatment:
                    {
                        #region " Cancel EMRTreatment setup controls "
                        tls_btnExportToExcel.Visible = true;
                        _EMRExamID = 0;
                        _EMRVisitID = 0;

                        _IsCancelEMRTreatment = true;
                        if (_oBox19Notes != null)
                            _oBox19Notes.Clear();
                        if (_oBox19Note != null)
                            _oBox19Note = null;
                        _sClaimRefNo = "";
                        _IsbCliamReplacement = false;
                        _bIsRefprovAsSupervisor = false;

                        UC_gloBillingTransactionLines.Enabled = false;
                        UC_gloBillingTransactionLines.AutoSort = true;
                        txtSearch.Text = "";
                        chk_SameasBillingProvider.Checked = false;

                        tls_Hold.Visible = false;
                        tls_btnAddLine.Visible = false;
                        tls_btnRemoveLine.Visible = false;
                        tlb_btnSmartTreatment.Visible = false;
                        tlb_AddNotes.Visible = false;
                        tls_btnOK.Visible = false;
                        tlb_LoadExam.Visible = false;
                        tlb_btnExamDXCPT.Visible = false;
                        tls_btnCancel.Visible = false;
                        tlb_SaveNextTreatment.Visible = false;
                        tls_btnSaveNClose.Visible = false;
                        tls_btnPayment.Visible = false;
                        tlb_AddEMRTreatment.Visible = false;
                        tlb_CloseEMRTreatment.Visible = true;
                        tlb_VoidEMRTreatment.Visible = true;
                        tlb_ReportEMRTreatment.Visible = true;
                        tlb_CancelEMRTreatment.Visible = false;
                        tls_btnMoreChargeData.Visible = false;
                        tls_Anesthesia.Visible = false;
                        tls_btnMoreClaimData.Visible = false;
                        tls_btnPatAcct.Visible = false;
                        tls_btnUB04Data.Visible = false;
                        tls_btnValidateProcedure.Visible = false;
                        tlsICD10CoddingRules.Visible = false ;
                        tlb_AddEPSDT.Visible = false;
                        pnlAlerts.Visible = false;
                        pnlEMRExams.BringToFront();
                        panel6.Visible = false;
                        pnlExamCPTDX.SendToBack();
                        oPatientControl.Visible = false;
                        pnlTransactionLines.Visible = false;
                        pnlTransactionOther1.Visible = false;
                        pnlTransactionOther2.Visible = false;
                        pnlTransactionMaster.Height = this.Height - (pnlToolStrip.Height) - 5;
                       
                        #endregion " Cancel EMRTreatment setup controls "
                    }
                    break;
                case ShowHideType.CloseEMRTreatment:
                    {
                        #region " Close EMRTreatment setup controls "

                        _IsEMRTreatmentStart = true;
                        _IsEMRTreatmentStop = true;

                        pnlAlerts.Visible = true;
                        panel6.Visible = true;
                        oPatientControl.Visible = true;
                        pnlTransactionLines.Visible = true;
                        pnlTransactionOther1.Visible = false;
                        pnlTransactionOther2.Visible = true;
                        pnlTransactionMaster.Height = 257;
                        pnlEMRExams.SendToBack();
                        pnlEMRExams.Visible = false;

                        UC_gloBillingTransactionLines.Enabled = true;
                        UC_gloBillingTransactionLines.AutoSort = true;

                        tls_Hold.Visible = true;
                        if (_EMRExamID == 0)
                        {
                            tlb_btnExamDXCPT.Visible = false;
                        }
                        else
                        {
                            tlb_btnExamDXCPT.Visible = true;
                        }
                        tls_btnAddLine.Visible = true;
                        tls_btnRemoveLine.Visible = true;
                        tlb_btnSmartTreatment.Visible = true;
                        tlb_AddNotes.Visible = true;
                        tls_btnOK.Visible = true;
                        tls_btnCancel.Visible = true;
                        tls_btnMoreChargeData.Visible = true;
                        tls_Anesthesia.Visible = true;
                        tls_btnMoreClaimData.Visible = true;
                        if (_IsCancelEMRTreatment)
                        {
                            tls_btnMoreChargeData.Enabled = false;
                            tls_Anesthesia.Enabled = false;
                            _IsCancelEMRTreatment = false;
                        }
                        tls_btnSaveNClose.Visible = true;
                        tls_btnPayment.Visible = true;
                        tlb_LoadExam.Visible = true;
                        tlb_CloseEMRTreatment.Visible = false;
                        tlb_VoidEMRTreatment.Visible = false;
                        tlb_ReportEMRTreatment.Visible = false;
                        tlb_AddEMRTreatment.Visible = true;
                        tlb_CancelEMRTreatment.Visible = false;
                        tls_btnPatAcct.Visible = true;
                        tls_btnUB04Data.Visible = true;
                        tlsICD10CoddingRules.Visible = true ;
                        //tls_btnOnlineCharge.Visible = true;
                        tls_btnOnlineCharge.Visible = bIsOCPPortalEnable;
                        if (!EPSDTEnabled)
                        {
                            tlb_AddEPSDT.Visible = false;
                        }
                        else
                        {
                            tlb_AddEPSDT.Visible = true;
                        }

                        if (ClaimValidationServiceType != ClaimValidationService.None)
                        {
                            tls_btnValidateProcedure.Visible = true;
                        }

                        tls_btnExportToExcel.Visible = false;
                        #endregion " Close EMRTreatment setup controls "
                    }
                    break;
                case ShowHideType.LoadEMRTreatment:
                    {
                        #region " Load EMRTreatment setup control "
                        tls_btnExportToExcel.Visible = false;
                        UC_gloBillingTransactionLines.ReinitilizeControl();
                        UC_gloBillingTransactionLines.Enabled = true;
                        cmbFeeSchedule.SelectedValue = _EMRFeeScheduleID;
                        if (_oClaimHold != null)
                        {
                            _oClaimHold.Dispose();
                            _oClaimHold = null;
                        }

                        _oClaimHold = new ClaimHold();
                        if (_oBox19Notes != null)
                            _oBox19Notes.Clear();
                        if (_oBox19Note != null)
                            _oBox19Note = null;
                        _sClaimRefNo = "";
                        _IsbCliamReplacement = false;
                        _bIsRefprovAsSupervisor = false;
                        tls_Hold.Visible = true;
                        tls_btnAddLine.Visible = true;
                        tls_btnRemoveLine.Visible = true;
                        tlb_btnSmartTreatment.Visible = true;
                        tlb_AddNotes.Visible = true;
                        tls_btnOK.Visible = true;
                        tls_btnCancel.Visible = true;
                        tls_btnPayment.Visible = false;
                        tls_btnSaveNClose.Visible = true;
                        tls_btnPayment.Visible = true;
                        tlb_AddEMRTreatment.Visible = false;
                        tlb_btnExamDXCPT.Visible = true;
                        tlb_LoadExam.Visible = true;
                        tlb_SaveNextTreatment.Visible = true;
                        tlb_CloseEMRTreatment.Visible = false;
                        tlb_VoidEMRTreatment.Visible = false;
                        tlb_ReportEMRTreatment.Visible = false;
                        tlb_CancelEMRTreatment.Visible = true;
                        tls_btnMoreChargeData.Visible = true;
                        tls_Anesthesia.Visible = true;
                        tls_Anesthesia.Enabled = false;
                        tls_btnMoreClaimData.Visible = true;
                        tls_btnMoreChargeData.Enabled = false;
                        tls_btnPatAcct.Visible = true;
                        tls_btnUB04Data.Visible = true;
                        tlsICD10CoddingRules.Visible = true ;
                        tlb_btnOCPDx.Visible = false;
                        if (!EPSDTEnabled)
                        {
                            tlb_AddEPSDT.Visible = false;
                        }
                        else
                        {
                            tlb_AddEPSDT.Visible = true;
                        }

                        pnlAlerts.Visible = true;
                        oPatientControl.Visible = true;
                        pnlTransactionLines.Visible = true;
                        pnlTransactionOther1.Visible = false;
                        pnlTransactionOther2.Visible = true;
                        pnlTransactionMaster.Height = 257;// this.Height - pnlToolStrip.Height;

                        if (ClaimValidationServiceType != ClaimValidationService.None)
                        {
                            tls_btnValidateProcedure.Visible = true;
                        }

                        //// Problem# 00000707 - Charges > EMR treatment > select patient A > enter an onset date and complete the charge entry >
                        //// save and next treatment > select patient B > when the charge opens Patient A onset day remains
                        mskBox15Date.Text = "";
                        mskOnsiteDate.Text = "";
                        cmbBox14DateQualifier.SelectedValue = "431";
                        if (DefaultDateQualifierCode != null)
                        {
                            cmbBox15DateQualifier.SelectedValue = DefaultDateQualifierCode;
                        }
                        // end

                        #endregion " Load EMRTreatment setup control "
                    }
                    break;
                case ShowHideType.LoadExamTemplate:
                    break;
                case ShowHideType.PatientBannerPatientChange:
                    {
                        #region " Patient Banner Patient change setup controls "

                        _TransactionID = 0;
                        if (Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                        { btnRemove_PriorAuthorization_Click(null, null); }
                        txtOutSideLabCharges.Text = "0.00";
                       // txtClaimNo.Text = "";
                        mskHospitaliztionFrom.Text = "";
                        mskHospitaliztionTo.Text = "";
                        mskUnableFromDate.Text = "";
                        mskUnableTillDate.Text = "";
                        _UnableToWorkFromDate_MoreClaimData = 0;
                        _UnableToWorkTillDate_MoreClaimData = 0;
                        mskAccidentDate.Text = "";
                        mskInitTreatment.Text = "";
                        //Bug #66395: 00000675.
                        //Added code to clear claim date and onset date when patient search is done form charge screen.
                        mskBox15Date.Text = "";
                        mskOnsiteDate.Text = "";
                        cmbBox14DateQualifier.SelectedValue = "431";
                        cmbBox15DateQualifier.SelectedValue = DefaultDateQualifierCode;
                        //chkAutoClaim.Checked = false;
                        //chkWorkersComp.Checked = false;
                        CmbAccidentType.SelectedIndex = 0;
                        chkOutSideLab.Checked = false;
                        if (cmbReferralProvider != null) { cmbReferralProvider.DataSource = null; cmbReferralProvider.Items.Clear(); cmbReferralProvider.SelectedIndex = -1; }
                        if (cmbClaimNo != null) { cmbClaimNo.SelectedIndex = -1; }
                        if (cmbState != null) { cmbState.SelectedIndex = -1; }

                        pnlExamCPTDX.SendToBack();

                        tlb_SaveNextTreatment.Visible = false;
                        tlb_CancelEMRTreatment.Visible = false;
                        tlb_btnExamDXCPT.Visible = false;
                        //tlb_LoadExam.Visible = false;
                        tlb_AddEMRTreatment.Visible = true;
                        tls_btnMoreChargeData.Enabled = false;

                        _EMRExamID = ZERO_VALUE;
                        _EMRVisitID = ZERO_VALUE;

                        _EMRPatientId = ZERO_VALUE;
                        _EMRProviderId = ZERO_VALUE;
                        _IsICD9Driven = false;

                        if (_oBox19Notes != null)
                            _oBox19Notes.Clear();
                        if (_oBox19Note != null)
                            _oBox19Note = null;
                        _sClaimRefNo = "";
                        _IsbCliamReplacement = false;
                        _bIsRefprovAsSupervisor = false;


                        #endregion " Patient Banner Patient change setup controls "
                    }
                    break;
                case ShowHideType.WaitStart:
                    {
                        #region " Save wait start labels setup controls "

                        lblPleaseWait.Visible = true;
                        lblPleaseWait.Enabled = true;
                        if (this.Enabled == false) { this.Enabled = true; }
                        pnlTransactionMaster.Enabled = false;
                        pnlTransactionLines.Enabled = false;
                        pnlToolStrip.Enabled = false;

                        #endregion " Save wait start labels setup controls "
                    }
                    break;
                case ShowHideType.WaitFinished:
                    {
                        #region " Save wait finished labels setup controls "

                        lblPleaseWait.Visible = false;
                        lblPleaseWait.Enabled = false;
                        if (this.Enabled == false) { this.Enabled = true; }
                        pnlTransactionMaster.Enabled = true;
                        pnlTransactionLines.Enabled = true;
                        pnlToolStrip.Enabled = true;

                        #endregion " Save wait finished labels setup controls "
                    }
                    break;
                case ShowHideType.OnlineCharge:
                    {
                        #region " Online Charge Setup Controls "

                        UC_gloBillingTransactionLines.Enabled = false;
                        chk_SameasBillingProvider.Checked = false;

                        tls_Hold.Visible = false;
                        tls_btnMoreChargeData.Visible = false;
                        tls_Anesthesia.Visible = false;
                        tls_btnMoreClaimData.Visible = false;
                        tls_btnAddLine.Visible = false;
                        tls_btnRemoveLine.Visible = false;
                        tlb_btnSmartTreatment.Visible = false;
                        tlb_AddNotes.Visible = false;
                        tls_btnOK.Visible = false;
                        tlb_LoadExam.Visible = false;
                        tlb_btnExamDXCPT.Visible = false;
                        tls_btnCancel.Visible = false;
                        tls_btnSaveNClose.Visible = false;
                        tls_btnPayment.Visible = false;
                        tlb_AddEMRTreatment.Visible = false;
                        tlb_CloseEMRTreatment.Visible = false;
                        tlb_AddEMRTreatment.Visible = false;
                        tlb_VoidEMRTreatment.Visible = false;
                        tlb_ReportEMRTreatment.Visible = false;
                        tlb_SaveNextTreatment.Visible = false;
                        tlb_CancelEMRTreatment.Visible = false;
                        tls_btnPatAcct.Visible = false;
                        tls_btnUB04Data.Visible = false;
                        tlb_CloseOnlineCharge.Visible = true;
                        tls_btnOnlineCharge.Visible = false;
                        tlb_btnOCPDx.Visible = false;

                        tls_btnValidateProcedure.Visible = false;

                        tlb_AddEPSDT.Visible = false;

                        pnlOCPCPTDX.SendToBack();
                        pnlAlerts.Visible = false;
                        pnlExamCPTDX.SendToBack();
                        pnlOnlineCharge.Visible = true;
                        pnlOnlineCharge.BringToFront();
                        oPatientControl.Visible = false;
                        pnlTransactionLines.Visible = false;
                        pnlTransactionOther1.Visible = false;
                        pnlTransactionOther2.Visible = false;
                        panel6.Visible = false;
                        pnlEMRExams.Visible = false;
                        pnlTransactionMaster.Height = this.Height - (pnlToolStrip.Height) - 5;

                        _IsEMRTreatmentStart = false;
                        _IsEMRTreatmentStop = false;
                        tlsICD10CoddingRules.Visible = false;


                        #endregion "Online Charge Setup Controls "

                    }
                    break;
                case ShowHideType.CloseOnlineCharge:
                    {
                        #region " Close Online Charge setup controls "



                        pnlAlerts.Visible = true;
                        panel6.Visible = true;
                        oPatientControl.Visible = true;
                        pnlTransactionLines.Visible = true;
                        pnlTransactionOther1.Visible = false;
                        pnlTransactionOther2.Visible = true;
                        pnlTransactionMaster.Height = 257;
                        pnlEMRExams.Visible = false;

                        pnlOnlineCharge.SendToBack();
                        pnlOnlineCharge.Visible = false;


                        UC_gloBillingTransactionLines.Enabled = true;
                        UC_gloBillingTransactionLines.AutoSort = true;

                        tls_Hold.Visible = true;
                        if (_EMRExamID == 0)
                        {
                            tlb_btnExamDXCPT.Visible = false;
                        }
                        else
                        {
                            tlb_btnExamDXCPT.Visible = true;
                        }
                        tls_btnAddLine.Visible = true;
                        tls_btnRemoveLine.Visible = true;
                        tlb_btnSmartTreatment.Visible = true;
                        tlb_AddNotes.Visible = true;
                        tls_btnOK.Visible = true;
                        tls_btnCancel.Visible = true;
                        tls_btnMoreChargeData.Visible = true;
                        tls_Anesthesia.Visible = true;
                        tls_btnMoreClaimData.Visible = true;
                        if (_IsCancelEMRTreatment)
                        {
                            tls_btnMoreChargeData.Enabled = false;
                            tls_Anesthesia.Enabled = false;
                            _IsCancelEMRTreatment = false;
                        }
                        tls_btnSaveNClose.Visible = true;
                        tls_btnPayment.Visible = true;
                        tlb_LoadExam.Visible = true;
                        tlb_CloseEMRTreatment.Visible = false;
                        tlb_VoidEMRTreatment.Visible = false;
                        tlb_ReportEMRTreatment.Visible = false;
                        tlb_AddEMRTreatment.Visible = true;
                        tlb_CancelEMRTreatment.Visible = false;
                        tls_btnPatAcct.Visible = true;
                        tls_btnUB04Data.Visible = true;
                        tlsICD10CoddingRules.Visible = true;
                        tlb_CloseOnlineCharge.Visible = false;
                        //tls_btnOnlineCharge.Visible = true;
                        tls_btnOnlineCharge.Visible = bIsOCPPortalEnable;
                        tlb_CancelOnlineCharge.Visible = false;
                        if (!EPSDTEnabled)
                        {
                            tlb_AddEPSDT.Visible = false;
                        }
                        else
                        {
                            tlb_AddEPSDT.Visible = true;
                        }

                        if (ClaimValidationServiceType != ClaimValidationService.None)
                        {
                            tls_btnValidateProcedure.Visible = true;
                        }

                        #endregion " Close Online Charge setup controls "
                    }
                    break;
                case ShowHideType.LoadOnlineCharge:
                    {
                        #region " Load LOnlineCharge setup control "

                        UC_gloBillingTransactionLines.ReinitilizeControl();
                        UC_gloBillingTransactionLines.Enabled = true;
                        cmbFeeSchedule.SelectedValue = _EMRFeeScheduleID;
                        if (_oClaimHold != null)
                        {
                            _oClaimHold.Dispose();
                            _oClaimHold = null;
                        }

                        _oClaimHold = new ClaimHold();
                        if (_oBox19Notes != null)
                            _oBox19Notes.Clear();
                        if (_oBox19Note != null)
                            _oBox19Note = null;
                        _sClaimRefNo = "";
                        _IsbCliamReplacement = false;
                        _bIsRefprovAsSupervisor = false;
                        tls_Hold.Visible = true;
                        tls_btnAddLine.Visible = true;
                        tls_btnRemoveLine.Visible = true;
                        tlb_btnSmartTreatment.Visible = true;
                        tlb_AddNotes.Visible = true;
                        tls_btnOK.Visible = true;
                        tls_btnCancel.Visible = true;
                        tls_btnPayment.Visible = false;
                        tls_btnSaveNClose.Visible = true;
                        tls_btnPayment.Visible = true;
                        tlb_AddEMRTreatment.Visible = false;
                        tlb_btnExamDXCPT.Visible = true;
                        tlb_LoadExam.Visible = true;
                        tlb_SaveNextTreatment.Visible = false;
                        tlb_CloseEMRTreatment.Visible = false;
                        tlb_VoidEMRTreatment.Visible = false;
                        tlb_ReportEMRTreatment.Visible = false;
                        tlb_CancelEMRTreatment.Visible = false;
                        tls_btnMoreChargeData.Visible = true;
                        tls_Anesthesia.Visible = true;
                        tls_Anesthesia.Enabled = false;
                        tls_btnMoreClaimData.Visible = true;
                        tls_btnMoreChargeData.Enabled = false;
                        tls_btnPatAcct.Visible = true;
                        tls_btnUB04Data.Visible = true;
                        tlsICD10CoddingRules.Visible = true;
                        tlb_CloseOnlineCharge.Visible = false;
                        tlb_CancelOnlineCharge.Visible = true;
                        tlb_btnOCPDx.Visible = true;
                        if (!EPSDTEnabled)
                        {
                            tlb_AddEPSDT.Visible = false;
                        }
                        else
                        {
                            tlb_AddEPSDT.Visible = true;
                        }
                        tlb_btnExamDXCPT.Visible = false;
                        pnlAlerts.Visible = true;
                        oPatientControl.Visible = true;
                        pnlTransactionLines.Visible = true;
                        pnlTransactionOther1.Visible = false;
                        pnlTransactionOther2.Visible = true;
                        pnlTransactionMaster.Height = 257;// this.Height - pnlToolStrip.Height;
                        pnlOnlineCharge.Visible = false;
                        pnlOnlineCharge.SendToBack();


                        if (ClaimValidationServiceType != ClaimValidationService.None)
                        {
                            tls_btnValidateProcedure.Visible = true;
                        }

                        //// Problem# 00000707 - Charges > EMR treatment > select patient A > enter an onset date and complete the charge entry >
                        //// save and next treatment > select patient B > when the charge opens Patient A onset day remains
                        mskBox15Date.Text = "";
                        mskOnsiteDate.Text = "";
                        cmbBox14DateQualifier.SelectedValue = "431";
                        if (DefaultDateQualifierCode != null)
                        {
                            cmbBox15DateQualifier.SelectedValue = DefaultDateQualifierCode;
                        }
                        // end

                        #endregion " Load OnlineCharge setup control "
                    }
                    break;
                case ShowHideType.CancelOnlineCharge:
                    {
                        #region " Cancel OnlineCharge setup controls "

                        _EMRExamID = 0;
                        _EMRVisitID = 0;

                        _IsCancelEMRTreatment = true;
                        if (_oBox19Notes != null)
                            _oBox19Notes.Clear();
                        if (_oBox19Note != null)
                            _oBox19Note = null;
                        _sClaimRefNo = "";
                        _IsbCliamReplacement = false;
                        _bIsRefprovAsSupervisor = false;

                        UC_gloBillingTransactionLines.Enabled = false;
                        UC_gloBillingTransactionLines.AutoSort = true;
                        txtSearch.Text = "";
                        chk_SameasBillingProvider.Checked = false;

                        tls_Hold.Visible = false;
                        tls_btnAddLine.Visible = false;
                        tls_btnRemoveLine.Visible = false;
                        tlb_btnSmartTreatment.Visible = false;
                        tlb_AddNotes.Visible = false;
                        tls_btnOK.Visible = false;
                        tlb_LoadExam.Visible = false;
                        tlb_btnExamDXCPT.Visible = false;
                        tls_btnCancel.Visible = false;
                        tlb_SaveNextTreatment.Visible = false;
                        tls_btnSaveNClose.Visible = false;
                        tls_btnPayment.Visible = false;
                        tlb_AddEMRTreatment.Visible = false;
                        tlb_CloseEMRTreatment.Visible = false;
                        tlb_VoidEMRTreatment.Visible = false;
                        tlb_ReportEMRTreatment.Visible = true;
                        tlb_CancelEMRTreatment.Visible = false;
                        tls_btnMoreChargeData.Visible = false;
                        tls_Anesthesia.Visible = false;
                        tls_btnMoreClaimData.Visible = false;
                        tls_btnPatAcct.Visible = false;
                        tls_btnUB04Data.Visible = false;
                        tls_btnValidateProcedure.Visible = false;
                        tlsICD10CoddingRules.Visible = false;
                        tlb_AddEPSDT.Visible = false;
                        tlb_ReportEMRTreatment.Visible = false;

                        tlb_btnOCPDx.Visible = false;

                        pnlOCPCPTDX.SendToBack();
                        pnlAlerts.Visible = false;
                        pnlOnlineCharge.Visible = true;
                        pnlOnlineCharge.BringToFront();
                        panel6.Visible = false;
                        pnlExamCPTDX.SendToBack();
                        oPatientControl.Visible = false;
                        pnlTransactionLines.Visible = false;
                        pnlTransactionOther1.Visible = false;
                        pnlTransactionOther2.Visible = false;
                        pnlTransactionMaster.Height = this.Height - (pnlToolStrip.Height) - 5;
                        tlb_CloseOnlineCharge.Visible = true;
                        tlb_CancelOnlineCharge.Visible = false;

                        #endregion " Cancel OnlineCharge setup controls "
                    }
                    break;
                default:
                    break;
            }
        }

        private void SetFormDataAfterClaimSave(Int64 lastsavedtransactionID, Int64 nextClaimNo)
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            try
            {
                if (lastsavedtransactionID > 0)
                {

                    ClearFormData();

                    //txtClaimNo.Text = "";
                    //mskClaimDate.Text = 

                    //..Refresh the balances on patient banner 
                    if (oPatientControl != null)
                    {
                        oPatientControl.RefreshBalances();
                        //oPatientControl.IsStripLoadedAfterClaimSave = true;

                    }

                    if (_IsEMRTreatmentStart == true && _IsEMRTreatmentStop == false && _IsSaveNextTreatmentClick == true)
                    {
                        tlb_CancelEMRTreatment.PerformClick();
                    }
                    //else
                    //{
                    //    if (PatientPoviderID > 0)
                    //    {
                    //        cmbBillingProvider.SelectedValue = PatientPoviderID;
                    //        DataTable dtProviderSettings = GetSettingsForExamProvider(PatientPoviderID);
                    //        if (dtProviderSettings != null && dtProviderSettings.Rows.Count > 0)
                    //        {
                    //            SetProviderSettings(dtProviderSettings);
                    //        }
                    //    }

                    if (UC_gloBillingTransactionLines != null && !_bIsSplitEMRTreatmentLoaded)
                    {
                        UC_gloBillingTransactionLines.PatientID = this.PatientID;
                        //LoadDefaultBillingSettings();
                        UC_gloBillingTransactionLines.InitialNofRows = 2;
                        UC_gloBillingTransactionLines.ReinitilizeControl();
                        UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                        //SetFacilitySettingsData();
                    }

                    if (UC_gloBillingTransactionLines.FacilityPOS > 0)
                    {
                        if (UC_gloBillingTransactionLines != null)
                        {
                            UC_gloBillingTransactionLines.SetFacilityPOS();

                        }
                    }
                    //}

                    if (!_IsSaveNextTreatmentClick)
                    {
                        tlb_SaveNextTreatment.Visible = false;
                        tlb_CancelEMRTreatment.Visible = false;
                        tlb_AddEMRTreatment.Visible = true;
                        tlb_CloseEMRTreatment.Visible = false;
                        //tlb_LoadExam.Visible = false;
                        tlb_btnExamDXCPT.Visible = false;
                        //tls_btnOnlineCharge.Visible = true;
                        tls_btnOnlineCharge.Visible = bIsOCPPortalEnable;
                        tlb_CancelOnlineCharge.Visible = false;
                        tlb_CloseOnlineCharge.Visible = false;
                        tlb_btnOCPDx.Visible = false;
                    }
                    if (_EMRExamID != 0)
                    {
                        tlb_btnExamDXCPT.Visible = true;
                        tlb_LoadExam.Visible = true;
                    }

                    txtClaimNo.Text = gloCharges.FormattedClaimNumberGeneration(Convert.ToString(nextClaimNo));


                    //UC_gloBillingTransactionLines.InitialNofRows = 2;
                    //UC_gloBillingTransactionLines.ReinitilizeControl();
                    if (!_bIsSplitEMRTreatmentLoaded)
                    {
                        DesignDxGrid();
                    }

                    SelectPrimaryInsurance();

                    if (!_bIsSplitEMRTreatmentLoaded || bIsFullyPosted)
                    {
                        LoadLastSavedData();
                    }


                    #region " Set the Primary Insurance for the Default Transaction Line "

                    if (c1Insurance != null && c1Insurance.Rows.Count > 0)
                    {
                        Int64 _InsuranceId = 0;
                        string _InsuranceName = "";
                        Int32 _InsSelfMode = 0;
                        for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                            {
                                _InsuranceId = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                _InsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                _InsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                                break;
                            }
                        }

                        if (UC_gloBillingTransactionLines != null)
                        {
                            UC_gloBillingTransactionLines.AddInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine, _InsuranceId, _InsuranceName, _InsSelfMode);                            
                        }
                    }

                    #endregion " Set the Primary Insurance for the Default Transaction Line "

                    //#region " Set Close Date for Service Line"

                    //if (IsValidDate(mskClaimDate.Text) == true)
                    //{
                    //    UC_gloBillingTransactionLines.SetServiceLineDate(1, Convert.ToDateTime(mskClaimDate.Text));
                    //}

                    //#endregion

                    ClaimEPSDT = null;
                  
                  
                     
                    if (_bEMRTreatmentLoading == false)
                    {
                        if (c1Insurance.Rows.Count >= 2 && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                        {

                            switch (ogloBilling.GetICDCodeType(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)), UC_gloBillingTransactionLines.getfirstservicelineDos()))
                            {
                                case gloGlobal.gloICD.CodeRevision.ICD10:
                                    rbICD10.Checked = true;
                                    break;
                                case gloGlobal.gloICD.CodeRevision.ICD9:
                                    rbICD9.Checked = true;
                                    break;
                                default:
                                    rbICD9.Checked = true;
                                    break;
                            }


                        }
                    }

                    #region Fill and set patient appointments linking to Charge
                    if (UC_gloBillingTransactionLines != null)
                    { this.FillPatientAppointmentsOnLoad(); }
                    #endregion
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
                _bIsSplitEMRTreatmentLoaded = false;
             
            }
        }

        private void LoadLastSavedData()
        {
            DataTable _dtTempServiceLines = null;
            try
            {
                string _renderingProviderName = "";
                Int64 _nRenderingProviderId = 0;
             
                if (_dsLastClaimDetails != null && _dsLastClaimDetails.Tables.Count > 0)
                {
                    _dtTempServiceLines = _dsLastClaimDetails.Tables[1].Clone();
                    DataRow[] _drServiceLine = null;
                    _drServiceLine = _dsLastClaimDetails.Tables[1].Select("nTransactionLineNo = max(nTransactionLineNo)");
                    foreach (DataRow dr in _drServiceLine)
                    {
                        _dtTempServiceLines.ImportRow(dr);
                    }
                }

                #region "set the Rendering Provider"
                if (_dtTempServiceLines.Rows.Count > 0)
                {
                    _nRenderingProviderId = Convert.ToInt64(_dtTempServiceLines.Rows[0]["nProvider"]);
                    if (cmbBillingProvider.DataSource != null)
                    {

                        DataTable _dtBillingProvider = null;
                        _dtBillingProvider = ((DataTable)cmbBillingProvider.DataSource).Copy();

                        if (_dtBillingProvider != null && _dtBillingProvider.Rows.Count > 0)
                        {
                            DataRow[] _drProviderRow = null;

                            _drProviderRow = _dtBillingProvider.Select("nProviderID = " + _nRenderingProviderId + "");

                            if (_drProviderRow.Length > 0)
                            {
                                _renderingProviderName = Convert.ToString(_drProviderRow[0]["sProviderName"]);
                            }
                            _drProviderRow = null;
                        }

                        if (_dtBillingProvider != null) { _dtBillingProvider.Dispose(); }
                    }

                    UC_gloBillingTransactionLines.DefaultRenderingProviderID = 0;
                    UC_gloBillingTransactionLines.DefaultRenderringProviderName = string.Empty;
                    foreach (DataRowView row in cmbBillingProvider.Items)
                    {
                        if (Convert.ToInt64(row[0]) == _nRenderingProviderId)
                        {
                            UC_gloBillingTransactionLines.DefaultRenderingProviderID = _nRenderingProviderId;
                            UC_gloBillingTransactionLines.DefaultRenderringProviderName = _renderingProviderName;
                            break;
                        }
                    }

                    //UC_gloBillingTransactionLines.DefaultRenderingProviderID = _nRenderingProviderId;
                    //UC_gloBillingTransactionLines.DefaultRenderringProviderName = _renderingProviderName;
                    UC_gloBillingTransactionLines.IsSettingsForProvider = false;
                }
                #endregion

                UC_gloBillingTransactionLines.SetLastLoadedChargesintoServiceLines(_dtTempServiceLines);


                #region "Commented Code - For Cases Defaulting and Ordinary Defaulting "

                //To Load the Last saved Diagnosis in to the service lines if the there are no cases
                //if (Convert.ToString(txtCases.Tag) == string.Empty)
                //{
                //#region "set the Rendering Provider"

                //_nRenderingProviderId = Convert.ToInt64(_dtTempServiceLines.Rows[0]["nProvider"]);
                //if (cmbBillingProvider.DataSource != null)
                //{

                //    DataTable _dtBillingProvider = null;
                //    _dtBillingProvider = ((DataTable)cmbBillingProvider.DataSource).Copy();

                //    if (_dtBillingProvider != null && _dtBillingProvider.Rows.Count > 0)
                //    {
                //        DataRow[] _drProviderRow = null;

                //        _drProviderRow = _dtBillingProvider.Select("nProviderID = " + _nRenderingProviderId + "");

                //        if (_drProviderRow.Length > 0)
                //        {
                //            _renderingProviderName = Convert.ToString(_drProviderRow[0]["sProviderName"]);
                //        }
                //        _drProviderRow = null;
                //    }

                //    if (_dtBillingProvider != null) { _dtBillingProvider.Dispose(); }
                //}

                //UC_gloBillingTransactionLines.DefaultRenderingProviderID = _nRenderingProviderId;
                //UC_gloBillingTransactionLines.DefaultRenderringProviderName = _renderingProviderName;
                //UC_gloBillingTransactionLines.IsSettingsForProvider = false;

                //#endregion

                //UC_gloBillingTransactionLines.SetLastLoadedChargesintoServiceLines(_dtTempServiceLines);
                //}
                //else
                //{
                //    if (_dtCaseDx != null && _dtCaseDx.Rows.Count > 0)
                //    {
                //        //Setting Case DX
                //        SetCaseDiagonosisintoChargeLines(_dtCaseDx);
                //    }
                //    if (_dtCaseInsurances != null && _dtCaseInsurances.Rows.Count > 0)
                //    {
                //        //Setting Case Insurance
                //        SetCaseInsurance(_dtCaseInsurances);
                //    }

                //    if (_dtTempServiceLines != null && _dtTempServiceLines.Rows.Count > 0)
                //    {
                //        DateTime dtDOS = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dtTempServiceLines.Rows[0]["nFromDate"]));
                //        UC_gloBillingTransactionLines.SetServiceLineDate(1, dtDOS);
                //    }
                //    _dtCaseDx = null;
                //    _dtCaseInsurances = null;
                //}

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally 
            {
                if (_dtTempServiceLines != null)
                {
                    _dtTempServiceLines.Dispose();
                    _dtTempServiceLines = null;
                }
            }


        }

        private void SetRefferingAsBillingProvider()
        {
            if (chk_SameasBillingProvider.Checked == true)
            {
                if (cmbBillingProvider.SelectedIndex >= 0)
                {
                    cmbReferralProvider.Tag = cmbReferralProvider.DataSource;
                   
                    cmbReferralProvider.DataSource = null;
                    cmbReferralProvider.Items.Clear();
                    cmbReferralProvider.Items.Add(cmbBillingProvider.Text);

                    if (cmbReferralProvider.Items.Count > 0)
                    { cmbReferralProvider.SelectedIndex = 0; }
                    else
                    { cmbReferralProvider.SelectedIndex = -1; }
                }
            }
            else
            {
                if (cmbReferralProvider.Tag != null)
                {
                    FillReferralProvidersData(((DataTable)cmbReferralProvider.Tag), false);
                }
            }

            ////Added By Debasish Das (5061)
            //if (cmbReferralProvider.Items.Count == 2)
            //{
            //    cmbReferralProvider.SelectedIndex = 1;
            //}
            ////***
        }

        private void SetHoldnMoreClaimDataMesseges()
        {
            string msg = "";
            Int64 _nInsuranceID = 0;
            bool _bInsHoldStatus = false;
            string _sInsHoldmsg = "";
            string _sClaimHoldmsg = "";
            string _sPartyNo = "";
            try
            {

                lblHoldMessage.Text = "";
                if (c1Insurance.Rows.Count > 1)
                {
                    _nInsuranceID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                    _sPartyNo = Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY));
                    _bInsHoldStatus = Convert.ToBoolean(c1Insurance.GetData(1, COL_INSURANCEPLANONHOLD));
                }
                bool _IsUB = false;
                Int64 _nOccurrenceDate = 0;
                Int64 _nMinDate = 0;
                if (oUB != null && oUB.sOccurrenceDate01 != null && Convert.ToString(oUB.sOccurrenceDate01).Trim() != "")
                {
                    _nOccurrenceDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oUB.sOccurrenceDate01));
                }
                if (UC_gloBillingTransactionLines.GetLinesCount() > 1)
                {                  
                   _nMinDate = UC_gloBillingTransactionLines.GetMinDOS();
                }

                if (_oBox19Notes != null)
                {
                    if (_oBox19Notes.Count == 0)
                    {
                        if (_bOnlineClaimPostingLoading == false)
                        {
                            DefaultBox19Note = GetDefaultBox19Note(_nInsuranceID);
                        }
                        else
                        {
                            DefaultBox19Note = _OnlineClaimNote;
                        }
                        if (DefaultBox19Note != "")
                        {
                            _oBox19Note = new ClaimBox19Note();
                            _oBox19Note.TransactionID = _TransactionID; // Convert.ToInt64(dt.Rows[i]["nTransactionID"]);
                            _oBox19Note.NoteType = NoteType.Box19_Note; //NoteType.GeneralNote;
                            _oBox19Note.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString()); //Convert.ToInt64(dt.Rows[i]["nNoteDateTime"]);
                            _oBox19Note.UserID = _UserID; //Convert.ToInt64(dt.Rows[i]["nUserID"]);
                            _oBox19Note.Box19NoteDescription = DefaultBox19Note; //Convert.ToString(dt.Rows[i]["sNoteDescription"]);
                            _oBox19Note.ClinicID = _ClinicID;
                            _oBox19Note.UserName = _UserName;
                            _oBox19Note.NoteRowID = 1;
                            _oBox19Note.BillingNoteType = EOBPaymentSubType.Claim_Box19Note;
                            _oBox19Note.NoteDateTime = DateTime.Now;
                            _oBox19Notes.Add(_oBox19Note);
                            lblHoldMessage.Text = "More Claim Data is present";
                        }
                    }
                }
                    

                //if (oUB != null && oUB.IsModify == true)
                //{
                //    _IsUB = true;
                //}

                if (oUB != null && tls_btnUB04Data.Visible == true && ((_TypeOfBill != oUB.sTypeofbill && oUB.sTypeofbill != null && Convert.ToString(oUB.sTypeofbill) != "" && oUB.TypeofbillDeleted == false) || (DischargeStatus != oUB.sDischargeStatus && oUB.sDischargeStatus != null && Convert.ToString(oUB.sDischargeStatus) != "" && oUB.DischargeStatusDeleted == false) || (AdmitType != oUB.sAdmissionType && oUB.sAdmissionType != null && Convert.ToString(oUB.sAdmissionType) != "" && oUB.AdmitTypeDeleted == false) || (_nOccurrenceDate != _nMinDate && _nOccurrenceDate != 0 && _nMinDate != 0) || oUB.HasOtherData == true))
                {
                    _IsUB = true;
                }
                else if (oUB.IsModify == false)
                {
                    _IsUB = false;
                }
                if (_bInsHoldStatus)
                {
                    _sInsHoldmsg = "Insurance Plan [" + Convert.ToString(c1Insurance.GetData(1, COL_INSURANCENAME)).Trim() + "] ";
                }

                if (_oClaimHold != null && _oClaimHold.IsHold)
                {
                    _sClaimHoldmsg = "Claim ";
                }

                if (_sInsHoldmsg != "" && _sClaimHoldmsg != "")
                {
                    msg = _sClaimHoldmsg + "and " + _sInsHoldmsg + "On Hold";
                }
                else if (_sInsHoldmsg != "")
                {
                    msg = _sInsHoldmsg + "On Hold";
                }
                else if (_sClaimHoldmsg != "")
                {
                    msg = _sClaimHoldmsg + "On Hold";
                }
                if (lblHoldMessage.Text.Trim() == "")
                {
                    if (msg != null && msg != "")
                    {



                        if (_IsbCliamReplacement || (_bIsRefprovAsSupervisor && bEnableSupervisorOption) || _sClaimRefNo != "" || (_oBox19Notes != null && _oBox19Notes.Count > 0 && _oBox19Notes[0].Box19NoteDescription.Trim() != "") || (_sClaimBox10dNote != null && _sClaimBox10dNote.Trim() != "") || _IllnessDate > 0 || _LastSeenDate > 0 || _sDelayReasonCode.Trim() != "" || _sServiceAuthExceptionCode.Trim() != "" || _UnableToWorkFromDate_MoreClaimData > 0 || _UnableToWorkTillDate_MoreClaimData > 0 || _PWKReportTypeCode.Trim() != "" || _PWKReportTransmissionCode.Trim() != "" || _PWKAttachmentControlNumber.Trim() != "" || _MammogramCertNumber.Trim() != "" || _IDENo.Trim() != "")
                        {
                            if (oUB != null && oUB.IsModify == true && _IsUB == true)
                            {
                                lblHoldMessage.Text = msg + "; " + "More Claim Data and UB Data are present";
                            }
                            else
                            {
                                lblHoldMessage.Text = msg + "; " + "More Claim Data is present";
                            }
                        }
                        else
                        {
                            if (oUB != null && oUB.IsModify == true && _IsUB == true)
                            {
                                lblHoldMessage.Text = msg + "; " + "UB Data is present";
                            }
                            else
                            {
                                lblHoldMessage.Text = msg;
                            }
                        }
                    }
                    else
                    {
                        if (_IsbCliamReplacement || (_bIsRefprovAsSupervisor && bEnableSupervisorOption) || _sClaimRefNo != "" || (_oBox19Notes != null && _oBox19Notes.Count > 0 && _oBox19Notes[0].Box19NoteDescription.Trim() != "") || (_sClaimBox10dNote != null && _sClaimBox10dNote.Trim() != "") || _IllnessDate > 0 || _LastSeenDate > 0 || _sDelayReasonCode.Trim() != "" || _sServiceAuthExceptionCode.Trim() != "" || _sMedicaidResubmissionCode.Trim() != "" || _UnableToWorkFromDate_MoreClaimData > 0 || _UnableToWorkTillDate_MoreClaimData > 0 || _PWKReportTypeCode.Trim() != "" || _PWKReportTransmissionCode.Trim() != "" || _PWKAttachmentControlNumber.Trim() != "" || _MammogramCertNumber.Trim() != "" || _IDENo.Trim() != "")
                        {
                            if (oUB != null && oUB.IsModify == true && _IsUB == true)
                            {
                                lblHoldMessage.Text = "More Claim Data and UB Data are present";
                            }
                            else
                            {
                                lblHoldMessage.Text = "More Claim Data is present";
                            }
                        }
                        else
                        {
                            if (oUB != null && oUB.IsModify == true && _IsUB == true)
                            {
                                lblHoldMessage.Text = "UB Data is present";
                            }
                            else
                            {
                                lblHoldMessage.Text = "";
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool IsDefaultOccuranceDOS(Int64 InsuranceID)
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            bool _result = false;
            try
            {
                _result = ogloBilling.GetDefaultOccuranceDateAsDOSSetting(InsuranceID);
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            }
            finally
            {
                if (ogloBilling != null)
                    ogloBilling.Dispose();
            }
            return _result;
        }


        private string GetDefaultBox19Note(Int64 InsuranceID)
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            string _result = "";
            try
            {
                _result = ogloBilling.GetDefaultBox19Note(InsuranceID);
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            }
            finally
            {
                if (ogloBilling != null)
                    ogloBilling.Dispose();
            }
            return _result;
        }

        private void SetLastGlobalPeriods()
        {
            DataTable _dtLatestGlobalPeriod = gloCharges.GetLastPatientGlobalPeriod(_PatientID, false);
            if (_dtLatestGlobalPeriod != null && _dtLatestGlobalPeriod.Rows.Count > 0)
            {
                string sMessage = Convert.ToString(_dtLatestGlobalPeriod.Rows[0]["sDuration"]) + "   ";
                lblGlobalPeriodAlert.Tag = _dtLatestGlobalPeriod.Rows[0]["nGlobalPeriodID"];
                lblGlobalPeriodAlert.Text = (sMessage != string.Empty ? "Last Global Period : " + sMessage : "");
                btnModifyGlobalPeriod.Visible = true; 
                pnlAlerts.Visible = true;
            }
            else
            {
                lblGlobalPeriodAlert.Tag = 0;
                lblGlobalPeriodAlert.Text = string.Empty;
                pnlAlerts.Visible = false;
                btnModifyGlobalPeriod.Visible = false; 
            }
            if (ReplacementClaimCreationType == global::gloBilling.ReplacementClaimCreationType.Replacement)
            {
                if (nReplacementByTransMasterID != 0 || nReplacingTransMasterID != 0)
                {
                    pnlAlerts.Visible = true;
                }
            }
            if (_dtLatestGlobalPeriod != null)
            {
                _dtLatestGlobalPeriod.Dispose();
                _dtLatestGlobalPeriod = null;
            }

            //if (!_IsSaveNextTreatmentClick)
            //{

            //    if (lblGlobalPeriodAlert.Text != string.Empty)
            //    {
            //        pnlAlerts.Visible = true;
            //    }
            //    else
            //    {
            //        pnlAlerts.Visible = false;
            //    }
            //}
        }

        public bool PerformCaseAdd()
        {
            bool _Return = false;
            DialogResult oResult = System.Windows.Forms.DialogResult.Yes;
            if (_PatientID > 0)
            {
                if (gloCharges.CheckPatientCases(_PatientID) == true)
                {
                    frmShowCases ofrmShowCases = new frmShowCases(_PatientID);
                    try
                    {
                        mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        ofrmShowCases.ShowDialog(this);

                        if (ofrmShowCases.CurrentCase != 0)
                        {
                            //this.PatientHasCases = true;
                            if (ofrmShowCases.CurrentCaseData.Rows.Count > 0 && ofrmShowCases.CurrentCaseData.Rows[0]["dtEnddate"] != DBNull.Value)
                            {
                                var results = from _dt in UC_gloBillingTransactionLines.GetCPTList().AsEnumerable()
                                              where _dt.Field<DateTime>("dtStartDate") > Convert.ToDateTime(ofrmShowCases.CurrentCaseData.Rows[0]["dtEnddate"])
                                              select _dt;

                                if (results.AsDataView().Count > 0)
                                {
                                    oResult = MessageBox.Show("Case has an End Date of " + Convert.ToDateTime(ofrmShowCases.CurrentCaseData.Rows[0]["dtEnddate"]).ToString("MM/dd/yyyy")
                                        + Environment.NewLine
                                        + "Continue Save?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                }
                            }

                            if (oResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                txtCases.TextAlign = HorizontalAlignment.Left;
                                txtCases.ForeColor = Color.Black;

                                txtCases.Tag = ofrmShowCases.CurrentCase;
                                txtCases.Text = ofrmShowCases.CurrentnCaseName;
                                if (_dtCaseData != null)
                                {
                                    _dtCaseData.Dispose();
                                    _dtCaseData = null;
                                }
                                _dtCaseData = ofrmShowCases.CurrentCaseData.Copy();
                               DataTable _dtCaseDx  = ofrmShowCases.CurrentDiagnosis;
                               DataTable _dtCaseInsurances = ofrmShowCases.CurrentCaseInsurences;

                                //Setting Case data into charge master form
                                SetCaseDetailsintoCharge(ofrmShowCases.CurrentCaseData);

                                //Setting Case diagonosis into charge lines
                                oResult = System.Windows.Forms.DialogResult.Yes;
                                if (tlb_btnExamDXCPT.Visible)
                                {
                                    if (c1Dx.Rows.Count > 1 && ofrmShowCases.CurrentDiagnosis != null && ofrmShowCases.CurrentDiagnosis.Rows.Count > 0)
                                    {
                                        string sDxList = string.Empty;
                                        for (int iDxCount = 0; iDxCount <= ofrmShowCases.CurrentDiagnosis.Rows.Count - 1; iDxCount++)
                                        {
                                            if (sDxList == string.Empty) { sDxList = (iDxCount + 1) + ") " + Convert.ToString(ofrmShowCases.CurrentDiagnosis.Rows[iDxCount]["sdxCode"]) + " - " + Convert.ToString(ofrmShowCases.CurrentDiagnosis.Rows[iDxCount]["sDxDescription"]); }
                                            else { sDxList += Environment.NewLine + (iDxCount + 1) + ") " + Convert.ToString(ofrmShowCases.CurrentDiagnosis.Rows[iDxCount]["sdxCode"]) + " - " + Convert.ToString(ofrmShowCases.CurrentDiagnosis.Rows[iDxCount]["sDxDescription"]); }
                                        }

                                        oResult = MessageBox.Show("Case has the following Diagnoses:"
                                             + Environment.NewLine
                                             + Environment.NewLine
                                             + sDxList
                                             + Environment.NewLine
                                             + Environment.NewLine
                                             + "Replace the Charge Diagnoses with the Case Diagnoses?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                                    }
                                }

                                if (oResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    SetCaseDiagonosisintoChargeLines(ofrmShowCases.CurrentDiagnosis);
                                }

                                //Setting Case Insurance
                                SetCaseInsurance(ofrmShowCases.CurrentCaseInsurences);

                            }

                        }
                        else
                        {
                            DataTable dtCases = null;
                            DataTable dtCaseDiag = null;
                            DataTable dtCasesIns = null;

                            DateTime _dtCloseDate;
                            if (mskClaimDate.Text != string.Empty)
                            {
                                mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                _dtCloseDate = Convert.ToDateTime(mskClaimDate.Text);
                            }
                            else
                            {
                                _dtCloseDate = DateTime.Now;
                            }

                            gloCharges.getSingleValidCase(_dtCloseDate, _PatientID, out dtCases, out dtCaseDiag, out dtCasesIns);
                            if (dtCases != null && dtCases.Rows.Count > 0)
                            {
                                this.PatientHasCases = true;
                            }
                            else
                            {
                                this.PatientHasCases = false;
                            }
                            if (dtCases != null)
                            {
                                dtCases.Dispose();
                                dtCases = null;
                            }
                            if (dtCaseDiag != null)
                            {
                                dtCaseDiag.Dispose();
                                dtCaseDiag = null;
                            }
                            if (dtCasesIns != null)
                            {
                                dtCasesIns.Dispose();
                                dtCasesIns = null;
                            }
                        }

                        CheckForPatientCases();

                        _Return = true;
                    }
                    catch (Exception ex)
                    {
                        _Return = false;
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);
                        ofrmShowCases.Dispose();
                        mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    }
                }
                else
                {
                    frmSetupCases ofrmSetupCases = new frmSetupCases(_PatientID);
                    try
                    {
                        ofrmSetupCases.ShowDialog(this);
                        if (ofrmSetupCases.nCaseID != 0)
                        {
                            //this.PatientHasCases = true;
                            if (ofrmSetupCases.CaseData.Rows.Count > 0 && ofrmSetupCases.CaseData.Rows[0]["dtEnddate"] != DBNull.Value)
                            {
                                var results = from _dt in UC_gloBillingTransactionLines.GetCPTList().AsEnumerable()
                                              where _dt.Field<DateTime>("dtStartDate") > Convert.ToDateTime(ofrmSetupCases.CaseData.Rows[0]["dtEnddate"])
                                              select _dt;

                                if (results.AsDataView().Count > 0)
                                {
                                    oResult = MessageBox.Show("Case has an End Date of " + Convert.ToDateTime(ofrmSetupCases.CaseData.Rows[0]["dtEnddate"]).ToString("MM/dd/yyyy")
                                        + Environment.NewLine
                                        + "Continue Save?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                }
                            }

                            if (oResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                txtCases.TextAlign = HorizontalAlignment.Left;
                                txtCases.ForeColor = Color.Black;

                                txtCases.Tag = ofrmSetupCases.nCaseID;
                                txtCases.Text = ofrmSetupCases.sCaseName.ToString().Trim();
                                if (_dtCaseData != null)
                                {
                                    _dtCaseData.Dispose();
                                    _dtCaseData = null;
                                }
                                _dtCaseData = ofrmSetupCases.CaseData.Copy();

                                //Setting Case data into charge master form
                                SetCaseDetailsintoCharge(ofrmSetupCases.CaseData);

                                //Setting Case diagonosis into charge lines
                                oResult = System.Windows.Forms.DialogResult.Yes;
                                if (tlb_btnExamDXCPT.Visible)
                                {
                                    if (c1Dx.Rows.Count > 1 && ofrmSetupCases.Diagnosis != null && ofrmSetupCases.Diagnosis.Rows.Count > 0)
                                    {
                                        string sDxList = string.Empty;
                                        for (int iDxCount = 0; iDxCount <= ofrmSetupCases.Diagnosis.Rows.Count - 1; iDxCount++)
                                        {
                                            if (sDxList == string.Empty) { sDxList = (iDxCount + 1) + ") " + Convert.ToString(ofrmSetupCases.Diagnosis.Rows[iDxCount]["sdxCode"]) + " - " + Convert.ToString(ofrmSetupCases.Diagnosis.Rows[iDxCount]["sDxDescription"]); }
                                            else { sDxList += Environment.NewLine + (iDxCount + 1) + ") " + Convert.ToString(ofrmSetupCases.Diagnosis.Rows[iDxCount]["sdxCode"]) + " - " + Convert.ToString(ofrmSetupCases.Diagnosis.Rows[iDxCount]["sDxDescription"]); }
                                        }

                                        oResult = MessageBox.Show("Case has the following Diagnoses:"
                                             + Environment.NewLine
                                             + Environment.NewLine
                                             + sDxList
                                             + Environment.NewLine
                                             + Environment.NewLine
                                             + "Replace the Charge Diagnoses with the Case Diagnoses?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                                    }
                                }

                                if (oResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    SetCaseDiagonosisintoChargeLines(ofrmSetupCases.Diagnosis);
                                }

                                //Setting Case Insurance
                                SetCaseInsurance(ofrmSetupCases.CurrentCaseInsurences);
                            }
                            CheckForPatientCases();
                        }
                        else
                        {
                            if (rbICD10.Checked)
                            {
                             
                                UC_gloBillingTransactionLines.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode();
                            }
                            else
                            {
                                UC_gloBillingTransactionLines.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
                            }
                        }
                        _Return = true;
                    }
                    catch (Exception ex)
                    {
                        _Return = false;
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                    }
                    finally
                    {
                        ofrmSetupCases.Dispose();
                    }

                }

            }
            else
            {
                MessageBox.Show("No Patient is selected.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return _Return;
        }

        public bool PerformRemoveAction()
        {
            bool _Return = false;
            try
            {
                mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (txtCases.Tag != null && txtCases.Tag.ToString() != "")
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Remove, "Cases removed from the Transaction", _PatientID, Convert.ToInt64(txtCases.Tag), 0, ActivityOutCome.Success);
                    txtCases.Text = "";
                    txtCases.Tag = null;
                }

                DataTable dtCases = null;
                DataTable dtCaseDiag = null;
                DataTable dtCasesIns = null;
                DateTime _dtCloseDate;
                if (mskClaimDate.Text != string.Empty)
                {
                    mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    _dtCloseDate = Convert.ToDateTime(mskClaimDate.Text);
                }
                else
                {
                    _dtCloseDate = DateTime.Now;
                }

                gloCharges.getSingleValidCase(_dtCloseDate, _PatientID, out dtCases, out dtCaseDiag, out dtCasesIns);
                if (dtCases != null && dtCases.Rows.Count > 0)
                {
                    this.PatientHasCases = true;
                }
                else
                {
                    this.PatientHasCases = false;
                }
                if (dtCases != null)
                {
                    dtCases.Dispose();
                    dtCases = null;
                }
                if (dtCaseDiag != null)
                {
                    dtCaseDiag.Dispose();
                    dtCaseDiag = null;
                }
                if (dtCasesIns != null)
                {
                    dtCasesIns.Dispose();
                    dtCasesIns = null;
                }
                CheckForPatientCases();
                _Return = true;
            }
            catch (Exception ex)
            {
                _Return = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
            return _Return;

        }

       

        #endregion " Form Data Methods "

        #region " Load Billing Control "

        private void LoadBillingControl()
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);

            try
            {
                if (UC_gloBillingTransactionLines != null)
                {
                    UC_gloBillingTransactionLines.onGrid_CellChanged -= new gloBillingTransaction.onGridCellChanged(UC_gloBillingTransactionLines_onGrid_CellChanged);
                    UC_gloBillingTransactionLines.onGrid_SelChanged -= new gloBillingTransaction.onGridSelChanged(UC_gloBillingTransactionLines_onGrid_SelChanged);
                    UC_gloBillingTransactionLines.onInsCPTDxMod_Changed -= new gloBillingTransaction.onInsCPTDxModChanged(UC_gloBillingTransactionLines_onInsCPTDxMod_Changed);
                    UC_gloBillingTransactionLines.show_LineNotes -= new gloBillingTransaction.showLineNote(UC_gloBillingTransactionLines_show_LineNotes);
                    UC_gloBillingTransactionLines.date_Changed -= new gloBillingTransaction.dateChanged(UC_gloBillingTransactionLines_date_Changed);                    

                    UC_gloBillingTransactionLines.Dispose();
                    UC_gloBillingTransactionLines = null;
                }

                UC_gloBillingTransactionLines = new gloBillingTransaction();
                LoadDefaultBillingSettings();

                UC_gloBillingTransactionLines.DefaultTOSID = _DefaultTOSID;
                UC_gloBillingTransactionLines.DefaultPOSID = _DefaultPOSID;
                if (cmbBillingProvider.Items.Contains(_PatientPoviderID))
                {
                    UC_gloBillingTransactionLines.PatientProviderID = _PatientPoviderID;
                }
                if (bIsCopiedClaim)
                {
                    UC_gloBillingTransactionLines.InitialNofRows = 1;
                    
                }
                else
                {
                    UC_gloBillingTransactionLines.InitialNofRows = 2;
                }
                pnlTransactionLines.Controls.Add(UC_gloBillingTransactionLines);
                UC_gloBillingTransactionLines.Visible = true;
                UC_gloBillingTransactionLines.Dock = DockStyle.Fill;
                pnlTransactionLines.Dock = DockStyle.Fill;
                UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);
                UC_gloBillingTransactionLines.onGrid_CellChanged += new gloBillingTransaction.onGridCellChanged(UC_gloBillingTransactionLines_onGrid_CellChanged);
                UC_gloBillingTransactionLines.onGrid_SelChanged += new gloBillingTransaction.onGridSelChanged(UC_gloBillingTransactionLines_onGrid_SelChanged);
                UC_gloBillingTransactionLines.onInsCPTDxMod_Changed += new gloBillingTransaction.onInsCPTDxModChanged(UC_gloBillingTransactionLines_onInsCPTDxMod_Changed);
                UC_gloBillingTransactionLines.show_LineNotes += new gloBillingTransaction.showLineNote(UC_gloBillingTransactionLines_show_LineNotes);
                UC_gloBillingTransactionLines.date_Changed += new gloBillingTransaction.dateChanged(UC_gloBillingTransactionLines_date_Changed); //date_Changed 
                UC_gloBillingTransactionLines.CLIA_Enter += new gloBillingTransaction.CLIAEnter(UC_gloBillingTransactionLines_CLIA_Enter);

                //UC_gloBillingTransactionLines.onCPTCharges_Load += new gloBillingTransaction.onCPTChargesLoaded(UC_gloBillingTransactionLines_onCPTCharges_Load);






                UC_gloBillingTransactionLines.PatientID = this.PatientID;
                if (c1Insurance.Rows.Count - 1 == 1 && _nResponsibleParty ==0)
                    UC_gloBillingTransactionLines.IsPrimarySelfPay = true;
                if (!bIsCopiedClaim)
                {
                    txtClaimNo.Text = gloCharges.FormattedClaimNumberGeneration(Convert.ToString(gloCharges.GenerateClaimNumber()));
                }

                if (_IsOpenForExternal == true)
                {
                    if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                    {
                        UC_gloBillingTransactionLines.DeleteTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);
                        UC_gloBillingTransactionLines.AddTransactionLine(_ExternalDateTime);
                    }

                    SelectPrimaryInsurance();
                    
                    //BugID: #52636: 00000467 : Charge
                    //Description: When accessing charges through the missing charges report - we are populating the date of service in all start and end dates (onset, hospitalization, off work dates)
                    //Commented all the dates set when accessing charges through missing charge report.

                    //mskAccidentDate.Text = _ExternalDateTime.ToString("MM/dd/yyyy");
                    //mskInjuryDate.Text = _ExternalDateTime.ToString("MM/dd/yyyy");
                    //mskOnsiteDate.Text = _ExternalDateTime.ToString("MM/dd/yyyy");
                    //mskOtherDate.Text = _ExternalDateTime.ToString("MM/dd/yyyy");
                    //mskUnableFromDate.Text = _ExternalDateTime.ToString("MM/dd/yyyy");
                    //mskUnableTillDate.Text = _ExternalDateTime.ToString("MM/dd/yyyy");
                    //mskHospitaliztionFrom.Text = _ExternalDateTime.ToString("MM/dd/yyyy");
                    //mskHospitaliztionTo.Text = _ExternalDateTime.ToString("MM/dd/yyyy");
                }

                #region " Set the Primary Insurance for the Default Transaction Line "

                if (c1Insurance != null && c1Insurance.Rows.Count > 0)
                {
                    Int64 _InsuranceId = 0;
                    Int64 _ContactId = 0;
                    string _InsuranceName = "";
                    Int32 _InsSelfMode = 0;
                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                        {
                            _InsuranceId = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                            _InsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                            _InsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                            _ContactId = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCECONTACTID));
                            break;
                        }
                    }

                    if (UC_gloBillingTransactionLines != null)
                    {
                        UC_gloBillingTransactionLines.AddInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine, _InsuranceId, _InsuranceName, _InsSelfMode);
                    }

                    #region " Expanded Claim Settings "
                    Int64 nContactID = 0;
                    if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                    {
                        nContactID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                        UC_gloBillingTransactionLines._nContactID = nContactID;
                    }
                    ogloBilling.GetExpandedClaimSetting(nContactID, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                    UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                    UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;

                    #endregion

                }

                #endregion " Set the Primary Insurance for the Default Transaction Line "

                #region " Check if Patient has Auto Claim "

                //**chkAutoClaim.Checked = IsAutoClaim(_PatientID);

                //chkAutoClaim.Checked = PatientHasAutoClaim;
                if (PatientHasAutoClaim)
                {
                    CmbAccidentType.SelectedText = "Auto";
                }

                #endregion " Check if Patient has Auto Claim "

                #region " Check if Patient has Other Accident Claim "

                //**chkOther.Checked = IsOtherAccidentClaim(_PatientID);
                //chkOther.Checked = PatientHasAutoClaim;

                if (PatientHasAutoClaim)
                {
                    CmbAccidentType.SelectedText = "Other";
                }
                #endregion " Check if Patient has Other Accident Claim "

                #region " Check if Patient has Workers Comp "

                //Worker comp always remain uncheck while loading. 
                // IsWorkersComp(_PatientID) this method is using a field which is not used now. [Patient].[bWorkersComp]
                //chkWorkersComp.Checked = IsWorkersComp(_PatientID);

                #endregion " Check if Patient has Workers Comp "

                //Saket 20090725
                #region "Set Facility/Non facility charges "

                if (chkFacilityFeeSchedule.Checked == true)
                    UC_gloBillingTransactionLines.DefaultChargesType = FacilityType.Facility;
                else if (chkNonFacilityCharges.Checked == true)
                    UC_gloBillingTransactionLines.DefaultChargesType = FacilityType.NonFacility;
                else
                    UC_gloBillingTransactionLines.DefaultChargesType = FacilityType.None;
                UC_gloBillingTransactionLines.PatientProviderID = _PatientPoviderID;
                UC_gloBillingTransactionLines.SetFNFCharges();

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        #endregion " Load Billing Control "

        #region "Tool strip Button Click Events "

        private void tls_btnPatAcct_Click(object sender, EventArgs e)
        {
            if (oPatientControl != null && oPatientControl.PatientID != 0)
            {
                gloAccountsV2.frmPatientFinancialViewV2 frmFinview = new gloAccountsV2.frmPatientFinancialViewV2(oPatientControl.PatientID);
                frmFinview.StartPosition = FormStartPosition.CenterScreen;
                frmFinview.WindowState = FormWindowState.Maximized;
                frmFinview.ShowInTaskbar = false;
                frmFinview.ShowDialog(this);
                frmFinview.Dispose();

                oPatientControl.FillDetails(oPatientControl.PatientID, gloStripControl.FormName.NewCharges, 1, false);
                oPatientControl_PatientModified(null, null);
                
            }
            SetLastGlobalPeriods();
        }

        private void tls_btnAddLine_Click(object sender, EventArgs e)
        {
            //Added by Subashish_b on 03/Feb /2011 (integration made on date-10/May/2011) for  validating whether selected patient or account is active or not
            if (_IsPatientAccountFeature)
            {
                if (!IsValidatePatientAccount(this.nPAccountID, this.PatientID)) { return; }
            }
            //End
            tls_Top.Select();
            if (_IsValidDate)
            {
                #region " Add Line "
                if (!(UC_gloBillingTransactionLines == null))
                {
                    if (UC_gloBillingTransactionLines.GetLinesCount() <= _NoOfMaxServiceLines)
                    {
                        //UC_gloBillingTransactionLines.SortControl();

                        #region "Get Selected Insurance"

                        string _fillInsuranceName = "";
                        Int64 _fillInsuranceID = 0;
                        Int32 _fillInsSelfMode = 0;

                        if (c1Insurance.Rows.Count > 0)
                        {
                            for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                            {
                                if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                                {
                                    _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                    _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                    _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                                    break;

                                }
                            }
                        }

                        #endregion

                        #region " Add new Line to Billing Control "

                        UC_gloBillingTransactionLines.AddTransactionLine();

                        #endregion " Add new Line to Billing Control "

                        #region " Set the Insurance to added Line "

                        if (UC_gloBillingTransactionLines != null)
                        {
                            if ((_fillInsuranceID > 0 || _fillInsSelfMode == 3))
                            {
                                UC_gloBillingTransactionLines.AddInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                            }
                            else if (_fillInsSelfMode == 1) //if no insurance present set SELF
                            {
                                UC_gloBillingTransactionLines.AddInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);

                            }
                        }

                        #endregion " Set the Insurance to added Line "

                        #region " Set Close Date to addedd Line "
                        if (UC_gloBillingTransactionLines != null)
                        {
                            if (IsValidDate(mskClaimDate.Text) == true && UC_gloBillingTransactionLines.GetLinesCount() == 2)
                            {
                                if (_sLastServiceLineDOS.Trim() == string.Empty)
                                {
                                    UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(mskClaimDate.Text));
                                }
                                else
                                {
                                    UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(_sLastServiceLineDOS));
                                }
                            }
                        }
                        #endregion " Set Close Date to addedd Line "

                        #region " Select the added Line "

                        UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);
                        UC_gloBillingTransactionLines_onGrid_SelChanged(null, null);

                        #endregion " Select the added Line "

                        if (chkFacilityFeeSchedule.Checked == false && chkNonFacilityCharges.Checked == false)
                        {
                            SetFacilitySettingsData(); // if due to some reason above line dont have any facility or non facility charge then select default once.
                        }

                        if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                        {
                            //Set default POS To the Trasaction Lines
                            if (UC_gloBillingTransactionLines.FacilityPOS > 0)
                            {
                                if (UC_gloBillingTransactionLines != null)
                                {
                                    UC_gloBillingTransactionLines.SetFacilityPOS();
                                }
                            }
                        }


                        UC_gloBillingTransactionLines.SortControl();

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.AddTransactionLine, "Transaction Line added to Billing Control,Line Number : " + UC_gloBillingTransactionLines.CurrentTransactionLine + " ", this.PatientID, _TransactionID, _PatientPoviderID, ActivityOutCome.Success);
                    }
                    else
                    {
                        MessageBox.Show(" Claim has a max limit of " + _NoOfMaxServiceLines + " service lines. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);

                    }
                }
                #endregion
            }

            //if (!UC_gloBillingTransactionLines.IsFirstDosChange)
            //{
            //    if (c1Insurance.Rows.Count >= 2)
            //    {
            //        switch (GetICDCodeType(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)), UC_gloBillingTransactionLines.getfirstservicelineDos()))
            //        {
            //            case gloGlobal.gloICD.CodeRevision.ICD10:
            //                rbICD10.Checked = true;
            //                break;
            //            case gloGlobal.gloICD.CodeRevision.ICD9:
            //                rbICD9.Checked = true;
            //                break;
            //            default:
            //                rbICD9.Checked = true;
            //                break;
            //        }


            //    }
            //}
        }

        private void tls_btnRemoveLine_Click(object sender, EventArgs e)
        {
            if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.GetLinesCount() > 1)
            {
                if (MessageBox.Show("Are you sure you want  to remove  selected service line?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (!(UC_gloBillingTransactionLines == null))
                    {
                        UC_gloBillingTransactionLines.DeleteTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.RemoveTransactionLine, "Transaction Line removed from Billing Control,Line Number : " + UC_gloBillingTransactionLines.CurrentTransactionLine + " ", this.PatientID, _TransactionID, _PatientPoviderID, ActivityOutCome.Success);
                        UC_gloBillingTransactionLines.SortControl();

                        if (UC_gloBillingTransactionLines.GetLinesCount() == 1)
                        {
                            DesignDxGrid();
                        }
                    }

                    if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                    {
                        if ((UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE) != null) && (UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE).ToString().Trim() != ""))
                        {
                            tls_btnMoreChargeData.Enabled = true;
                            tls_Anesthesia.Enabled = true;
                        }
                        else
                        {
                            tls_btnMoreChargeData.Enabled = false;
                            tls_Anesthesia.Enabled = false;
                        }

                        using (DataTable dtAppointments = this.GetPatientAppointments())
                        {
                            if (!this.lstAppointmentIDs.Any())
                            {                         
                                if (dtAppointments != null && dtAppointments.Rows.Count > 0)
                                { 
                                    this.SetPatientAppointmentsAvailable();
                                    this.PatientHasAppointments = true;
                                }
                                else
                                { 
                                    this.ResetPatientAppointments();
                                    this.PatientHasAppointments = false;
                                }                            
                            }

                            this.LoadAppointmentsInList(dtAppointments);
                            this.CheckMorePatientAppointments(dtAppointments);
                        }                                 
                    }
                    else
                    {
                        tls_btnMoreChargeData.Enabled = false;
                        tls_Anesthesia.Enabled = false;
                    }
                    UC_gloBillingTransactionLines.getfirstservicelineCLIA();
                }

                SelectPrimaryInsurance();
                SyncronizeDxGridWithServiceline();
            }
        }

        private void tls_btnValidateProcedure_Click(object sender, EventArgs e)
        {
            switch (ClaimValidationServiceType)
            {
                case ClaimValidationService.YOST:
                    ValidateClaim();
                    break;
                case ClaimValidationService.Alpha2:
                    GetAlphaIISettings();
                    if (_AlphaIIValidation == "Alpha2")
                    {
                        if (ValidateConnectionString())
                        {
                            AlphaIIDiagnosisValidation();
                        }
                        else
                        {
                            MessageBox.Show("Connection for Alpha II cannot be establish, please do the setting from gloPM Admin.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    break;
                case ClaimValidationService.None:
                    break;
                default:
                    break;
            }
        }

        private void tlb_btnSmartTreatment_Click(object sender, EventArgs e)
        {
            //Added by Subashish_b on 03/Feb /2011 (integration made on date-10/May/2011) for  validating whether selected patient or account is active or not
            if (_IsPatientAccountFeature)
            {
                if (!IsValidatePatientAccount(this.nPAccountID, this.PatientID)) { return; }
            }
            //End
            #region "Get Selected Insurance"

            string _fillInsuranceName = "";
            Int64 _fillInsuranceID = 0;
            Int32 _fillInsSelfMode = 0;
            DateTime _Closedate;

            if (c1Insurance.Rows.Count > 0)
            {
                for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                {
                    if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                    {
                        _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                        _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                        _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                        break;
                    }
                }
            }

            #endregion
            _bDXSwitch = true;
            this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);


            //Commented on 25-Jan-2011
            //this.Enabled = false; this.Enabled = true;
            this.mskClaimDate.Validating -= new CancelEventHandler(mskDate_Validating);
            this.mskAccidentDate.Validating -= new CancelEventHandler(mskDate_Validating);
            this.mskHospitaliztionFrom.Validating -= new CancelEventHandler(mskDate_Validating);
            this.mskHospitaliztionTo.Validating -= new CancelEventHandler(mskDate_Validating);
            this.mskInjuryDate.Validating -= new CancelEventHandler(mskDate_Validating);
            this.mskOnsiteDate.Validating -= new CancelEventHandler(mskDate_Validating);
            this.mskOtherDate.Validating -= new CancelEventHandler(mskDate_Validating);
            this.mskUnableFromDate.Validating -= new CancelEventHandler(mskDate_Validating);
            this.mskUnableTillDate.Validating -= new CancelEventHandler(mskDate_Validating);
            this.mskInitTreatment.Validating -= new CancelEventHandler(mskDate_Validating);


            #region " Load SmartTreatment "
            bool _SmartTreatmentDialogResult = false;
            frmSelectSmartTreatment ofrmSelectSmartTreatment = new frmSelectSmartTreatment(_DatabaseConnectionString);
            ofrmSelectSmartTreatment.ShowDialog(this);
            _SmartTreatmentDialogResult = ofrmSelectSmartTreatment._DialogResult;
            if (_SmartTreatmentDialogResult == true)
            { _SelectedSmartTreatmentIDs = ofrmSelectSmartTreatment._SelectedTreatments; }
            //ofrmSelectSmartTreatment.Hide();
            ofrmSelectSmartTreatment.Dispose();
            this.Refresh();

            string _errormessage = "";
            if (_SmartTreatmentDialogResult == true)
            {
                //_SelectedSmartTreatmentID = ofrmSelectSmartTreatment._SelectedSmartTreatment;
                //UC_gloBillingTransactionLines.FillTreatmentInGrid(_SelectedSmartTreatmentID);
                //_SelectedSmartTreatmentIDs = ofrmSelectSmartTreatment._SelectedTreatments;
                if (_SelectedSmartTreatmentIDs != null)
                {
                    lblPleaseWait.Visible = true;
                    lblPleaseWait.Enabled = true;


                    //Commented on 25-Jan-2011
                    //this.Enabled = false; this.Enabled = true;

                    pnlTransactionMaster.Enabled = false;
                    pnlTransactionLines.Enabled = false;
                    pnlToolStrip.Enabled = false;
                    lblPleaseWait.BringToFront();
                    this.Refresh();

                    for (int i = 0; i < _SelectedSmartTreatmentIDs.Count; i++)
                    {
                        if (_errormessage.Trim() == "")
                        {
                            //UC_gloBillingTransactionLines.FillTreatmentInGrid(Convert.ToInt64(_SelectedSmartTreatmentIDs[i]));

                            //added on 25/05/2010 Smart Treatment DOS is wrong --> The Last Selected Date is not getting Loaded while loading Smart Treatment
                            string _sClosedate = UC_gloBillingTransactionLines.GetLastDOS();
                            if (_sClosedate != "")
                            {
                                _Closedate = Convert.ToDateTime(_sClosedate);
                            }
                            //added on 28/04/2010 for case: Smart Treatment DOS is wrong --> if close date not given set today's date to service line DOS 
                            else if (IsValidDate(mskClaimDate.Text) == true)
                            {
                                _Closedate = Convert.ToDateTime(mskClaimDate.Text);
                            }
                            else
                            {
                                _Closedate = DateTime.Now.Date;
                            }

                            _bDxFlag = true;
                            _bSmartTreatmentLoading = true;


                            //if (UC_gloBillingTransactionLines.GetLinesCount() > 0)
                            //{
                            //    TransactionLines oTransactionLines = UC_gloBillingTransactionLines.GetLineTransactions();
                            //    if (oTransactionLines != null && oTransactionLines.Count > 0)
                            //    {
                            //        if (string.IsNullOrEmpty(oTransactionLines[0].CPTCode))
                            //        {
                            //            c1Dx.Rows.Count = 1;
                            //        }
                            //    }
                            //}


                            //c1Dx.Rows.Count = 1;
                            UC_gloBillingTransactionLines.FillTreatmentInGrid(Convert.ToInt64(_SelectedSmartTreatmentIDs[i]), _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode, _Closedate, out _errormessage);

                            //Remove unwanted Diagnosis drom Dx Grid
                            SyncronizeDxGridWithServiceline();

                            if (c1Dx.Rows.Count > 0)
                            {
                                for (int dxRowsCount = 1; dxRowsCount < c1Dx.Rows.Count; dxRowsCount++)
                                {
                                    c1Dx.SetData(dxRowsCount, COL_DX_SELECT, true);
                                }
                            }

                            _bDxFlag = false;
                            _bSmartTreatmentLoading = false;
                            //Set default POS To the Trasaction Lines
                            Boolean bIsPosLoaded = false;
                            TransactionLines oTransactionLines = null;
                            oTransactionLines = UC_gloBillingTransactionLines.GetLineTransactions();
                            if (oTransactionLines != null && oTransactionLines.Count > 0)
                            {
                                for (int j = 0; j <= oTransactionLines.Count - 1; j++)
                                {
                                    if (!string.IsNullOrEmpty(oTransactionLines[j].POSCode.Trim()))
                                    {
                                        bIsPosLoaded = true;
                                        break;
                                    }
                                }
                            }
                            if (oTransactionLines != null) { oTransactionLines.Dispose(); oTransactionLines = null; }

                            if (!bIsPosLoaded)
                            {
                                if (UC_gloBillingTransactionLines.FacilityPOS > 0)
                                {
                                    if (UC_gloBillingTransactionLines != null)
                                    {
                                        UC_gloBillingTransactionLines.SetFacilityPOS();
                                    }
                                }
                            }

                            if (oTransactionLines != null) { oTransactionLines.Dispose(); oTransactionLines = null; }
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.LoadSmartTreatment, "Smart Treatement loaded to setup charges", this.PatientID, Convert.ToInt64(_SelectedSmartTreatmentIDs[i]), 0, ActivityOutCome.Success);
                        }
                    }

                    lblPleaseWait.Visible = false;
                    lblPleaseWait.Enabled = false;

                    pnlTransactionMaster.Enabled = true;
                    pnlTransactionLines.Enabled = true;
                    pnlToolStrip.Enabled = true;



                    //to select first service line
                    if (UC_gloBillingTransactionLines != null)
                    {
                        if (UC_gloBillingTransactionLines.GetLinesCount() > 1)
                        {
                            UC_gloBillingTransactionLines.SelectTransactionLine(1);
                        }
                    }
                  
                    if (c1Insurance.Rows.Count >= 2)
                    {

                        switch (GetICDCodeType(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)), UC_gloBillingTransactionLines.getfirstservicelineDos()))
                        {
                            case gloGlobal.gloICD.CodeRevision.ICD10:
                                rbICD10.Checked = true;
                                break;
                            case gloGlobal.gloICD.CodeRevision.ICD9:
                                rbICD9.Checked = true;
                                break;
                            default:
                                rbICD9.Checked = true;
                                break;
                        }


                    }
                }
            }

            //ofrmSelectSmartTreatment.Dispose();
            //UC_gloBillingTransactionLines.FillTreatmentInGrid(UC_gloBillingTransactionLines.CurrentTransactionLine,_SelectedSmartTreatmentID); 

            if (_errormessage.Trim() != "")
            {
                MessageBox.Show(_errormessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion

            this.mskClaimDate.Validating += new CancelEventHandler(mskDate_Validating);
            this.mskAccidentDate.Validating += new CancelEventHandler(mskDate_Validating);
            this.mskHospitaliztionFrom.Validating += new CancelEventHandler(mskDate_Validating);
            this.mskHospitaliztionTo.Validating += new CancelEventHandler(mskDate_Validating);
            this.mskInjuryDate.Validating += new CancelEventHandler(mskDate_Validating);
            this.mskOnsiteDate.Validating += new CancelEventHandler(mskDate_Validating);
            this.mskOtherDate.Validating += new CancelEventHandler(mskDate_Validating);
            this.mskUnableFromDate.Validating += new CancelEventHandler(mskDate_Validating);
            this.mskUnableTillDate.Validating += new CancelEventHandler(mskDate_Validating);
            this.mskInitTreatment.Validating += new CancelEventHandler(mskDate_Validating);

            this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
            _bDXSwitch = false;
        }

        private void tls_Hold_Click(object sender, EventArgs e)
        {
            #region " Hold "
            //ClaimHold oClaimHold = null;

            if (_oClaimHold == null)
            {
                _oClaimHold = new ClaimHold();
            }

            frmBillingHold ofrmBillingHold = new frmBillingHold(_DatabaseConnectionString, Convert.ToString(txtClaimNo.Text), _ClinicID, _UserID, _TransactionID, 0, _oClaimHold, "");
            ofrmBillingHold.ShowDialog(this);

            if (ofrmBillingHold.oDialogResult)
            {
                _oClaimHold = ofrmBillingHold._oClaimHold;
                //**GetHoldMessage();

                SetHoldnMoreClaimDataMesseges();
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Add, "Hold ", ActivityOutCome.Success);
            }
            ofrmBillingHold._oClaimHold.Dispose();
            ofrmBillingHold.Dispose();



            #endregion
        }

        private void tlb_AddNotes_Click(object sender, EventArgs e)
        {
            #region " Add Notes "
            GeneralNotes oNotes = null;

            if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
            {
                //oNotes = UC_gloBillingTransactionLines.GetNotes(UC_gloBillingTransactionLines.CurrentTransactionLine);
                oNotes = UC_gloBillingTransactionLines.GetNotes(UC_gloBillingTransactionLines.TransactionLineNo);


                //frmSetupTransactionNotes ofrmNotes = new frmSetupTransactionNotes(_DatabaseConnectionString, _ClinicID, _TransactionID, Convert.ToInt64(UC_gloBillingTransactionLines.CurrentTransactionLine),oNotes);
                // frmSetupTransactionNotes ofrmNotes = new frmSetupTransactionNotes(_DatabaseConnectionString, _ClinicID, _TransactionID, UC_gloBillingTransactionLines.TransactionDetailID,UC_gloBillingTransactionLines.TransactionLineNo, oNotes);
                frmNotes ofrmNotes = new frmNotes(_DatabaseConnectionString, _ClinicID, _TransactionID, UC_gloBillingTransactionLines.TransactionDetailID, UC_gloBillingTransactionLines.TransactionLineNo, oNotes);
                ofrmNotes.ShowDialog(this);
                if (ofrmNotes.oDialogResult)
                {
                    //UC_gloBillingTransactionLines.SetNotes(UC_gloBillingTransactionLines.CurrentTransactionLine, ofrmNotes.oNote);
                    UC_gloBillingTransactionLines.SetNotes(ofrmNotes.oNotes);                   
                }
                ofrmNotes.Dispose();

                //..Check if any notes exists for the TransactionLine 
                //if (UC_gloBillingTransactionLines.HasNote(UC_gloBillingTransactionLines.CurrentTransactionLine) == true)
                //{ UC_gloBillingTransactionLines.SetEPSDTNotesNDCCodeFlag(UC_gloBillingTransactionLines.CurrentTransactionLine); }
                //else
                //{ 
                UC_gloBillingTransactionLines.SetEPSDTNotesNDCCodeFlag(UC_gloBillingTransactionLines.CurrentTransactionLine);
                //}
                if (oNotes != null)
                {
                    oNotes.Dispose();
                    oNotes = null;
                }
            }
            else
            {
                MessageBox.Show("No transaction line is selected.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #endregion
        }

        private void tlb_AddEMRTreatment_Click(object sender, EventArgs e)
        {
           
            //Added by Subashish_b on 03/Feb /2011 (integration made on date-10/May/2011) for  validating whether selected patient or account is active or not
            if (_IsPatientAccountFeature)
            {
                if (!IsValidatePatientAccount(this.nPAccountID, this.PatientID)) { return; }
            }
            //End
            tls_Top.Select();
            this.Cursor = Cursors.WaitCursor;
            if (_IsValidDate)
            {

                BindEMRExams();

                ShowHideControls(ShowHideType.EMRTreatment);
            }
            this.Cursor = Cursors.Default;
        }

        private void tlb_btnExamDXCPT_Click(object sender, EventArgs e)
        {
            LoadDXCPTWindow();
        }
        private void tlb_btnOCPDx_Click(object sender, EventArgs e)
        {
            LoadOCPDXCPTWindow();
            tls_OCPCPTDX.Visible = true;
            tls_OCPCPTDX.Enabled = true;
        }
       
        private void tlb_CloseOnlineCharge_Click(object sender, EventArgs e)
        {
            IsOnlineChargeBind = false;
            #region " Set Close Date to added Line if the treatment is closed "
            if (IsValidDate(mskClaimDate.Text) == true)
            {
                if (UC_gloBillingTransactionLines != null)
                {
                    if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                    {
                        if (_sLastServiceLineDOS.Trim() == string.Empty)
                        {
                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(mskClaimDate.Text));
                        }
                        else
                        {
                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(_sLastServiceLineDOS));
                        }
                    }
                }
            }

            #endregion " Set Close Date to added Line "

            ShowHideControls(ShowHideType.CloseOnlineCharge);

            SetLastGlobalPeriods();
            ShowHideUB();
            UC_gloBillingTransactionLines.SetFacilityPOS();
        }
       

        private void tlb_CloseEMRTreatment_Click(object sender, EventArgs e)
        {
           
            IsEMRTreatmentBind = false;
            #region " Set Close Date to added Line if the treatment is closed "
            if (IsValidDate(mskClaimDate.Text) == true)
            {
                if (UC_gloBillingTransactionLines != null)
                {
                    if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                    {
                        if (_sLastServiceLineDOS.Trim() == string.Empty)
                        {
                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(mskClaimDate.Text));
                        }
                        else
                        {
                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(_sLastServiceLineDOS));
                        }
                    }
                }
            }

            #endregion " Set Close Date to added Line "

            ShowHideControls(ShowHideType.CloseEMRTreatment);

            SetLastGlobalPeriods();
            ShowHideUB();
            UC_gloBillingTransactionLines.SetFacilityPOS();
        }

        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {
            string _FilePath = "";

            try
            {

                FileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File(.xls)|*.xls";
                saveFileDialog.DefaultExt = ".xls";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                {
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                    return;
                }
                _FilePath = saveFileDialog.FileName;
                saveFileDialog.Dispose();
                saveFileDialog = null;
                c1PatientEMRExams.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);

                MessageBox.Show("File saved successfully.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (IOException)// ioEx)
            {
                MessageBox.Show("File in use. Fail to export report.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                MessageBox.Show("File in use. Fail to export report.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        

        private void tlb_VoidEMRTreatment_Click(object sender, EventArgs e)
        {
            frmVoidEMRTreatment oVoidEMRTreatment = new frmVoidEMRTreatment(_DatabaseConnectionString, _DatabaseConnectionString);
            oVoidEMRTreatment.PatientID = _PatientID;
            oVoidEMRTreatment.ClinicID = _ClinicID;
            oVoidEMRTreatment.UserID = _UserID;
            oVoidEMRTreatment._nEMRTreatmentType = _nEMRTreatmentType;
            oVoidEMRTreatment.ShowDialog(this);
            oVoidEMRTreatment.Dispose();
            //BindPatientExam(PatientID);
            BindEMRExams();
        }

        private void tlb_CancelEMRTreatment_Click(object sender, EventArgs e)
        {
            IsEMRTreatmentBind = true;
            this.Cursor = Cursors.WaitCursor;
            tls_Top.Select();
            _EMRExamID = 0;
            _EMRVisitID = 0;
            _EMRPatientId = 0;
            _EMRProviderId = 0;

            if (_IsValidDate)
            {
                if (_dtNoPostCharges != null && _dtNoPostCharges.Rows.Count > 0)
                {
                    _dtNoPostCharges.Rows.Clear();
                   
                }
                BindEMRExams();
                ShowHideControls(ShowHideType.CancelEMRTreatment);
                //Set default POS to c1_transaction on load
                //20100503 - Hot Fix EMr Treatment Issue - Load the POS From Admin settings and apply to service lines
                SetFacilitySettingsData();
                DesignDxGrid();

                UC_gloBillingTransactionLines.TreatmentType = ExternalChargesType.gloEMRTreatment;

                #region " Set Close Date to added Line If the Treatment is cancelled"
                if (IsValidDate(mskClaimDate.Text) == true)
                {
                    if (UC_gloBillingTransactionLines != null)
                    {
                        if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                        {
                            if (_sLastServiceLineDOS.Trim() == string.Empty)
                            {
                                UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(mskClaimDate.Text));
                            }
                            else
                            {
                                UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(_sLastServiceLineDOS));
                            }
                        }
                    }
                }
                #endregion " Set Close Date to added Line "

                UC_gloBillingTransactionLines.ReinitilizeControl();
                UC_gloBillingTransactionLines.PatientID = this.PatientID;

            }

            this.Cursor = Cursors.Default;
        }

        private void tlb_LoadExam_Click(object sender, EventArgs e)
        {
            if (_EMRExamID!=0)
            {
                gloOffice.frmWd_PatientExam oPatientExam = new gloOffice.frmWd_PatientExam(_DatabaseConnectionString, _EMRExamID);
                oPatientExam.StartPosition = FormStartPosition.CenterParent;
                oPatientExam.WindowState = FormWindowState.Maximized;
                oPatientExam.Show();
            
            }
            else
            {
                string sMinDos = Convert.ToString(gloDateMaster.gloDate.DateAsDate((UC_gloBillingTransactionLines.GetMinDOS())));
                frmBillingPatientviewExam oPatientExam = new frmBillingPatientviewExam(_DatabaseConnectionString, _PatientID,sMinDos,cmbBillingProvider.Text);
                oPatientExam.ShowDialog(this);
                oPatientExam.Dispose();
            }

            
        }

        private void tlb_SaveNextTreatment_Click(object sender, EventArgs e)
        {

            _IsSaveNextTreatmentClick = true;
            tls_Top.Select();
            if (_IsValidDate)
            {
                if (SaveTreatment())
                {
                    if (pnlAlerts.Visible) { pnlAlerts.Visible = false; }
                }
            }

        }

        private void tls_btnMoreChargeData_Click(object sender, EventArgs e)
        {
            string sNDCCode = "";
            TransactionLine oLineTransaction = null;
            TransactionLines oLineTransactions = null;
            #region " Add Charge Fields "
            oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();

            if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
            {
                if ((Convert.ToString(UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE)).Trim() != "") || (UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE) != null))
                {
                    if (oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode != "" && oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode != null)
                    {

                        frmDrugBilling ofrmDrugBilling = new frmDrugBilling(_DatabaseConnectionString, oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1]);
                        ofrmDrugBilling.ShowDialog(this);

                        if (ofrmDrugBilling.oDialogResult)
                        {
                            oLineTransaction = ofrmDrugBilling._oTransLine;
                            UC_gloBillingTransactionLines.SetNDCFields(oLineTransaction);
                            oLineTransaction.Dispose();
                  //          oLineTransactions.Dispose();
                        }
                        ofrmDrugBilling._oTransLine.Dispose();
                        ofrmDrugBilling.Dispose();
                    }
                    else if (gloCharges.GetDefaultNDCForCPT(Convert.ToString(UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE)).Trim(), ref sNDCCode))
                    {
                        oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode = sNDCCode;
                        frmDrugBilling ofrmDrugBilling = new frmDrugBilling(_DatabaseConnectionString, oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1],IsIncDescriptionCheck:true);
                        ofrmDrugBilling.ShowDialog(this);

                        if (ofrmDrugBilling.oDialogResult)
                        {
                            oLineTransaction = ofrmDrugBilling._oTransLine;
                            UC_gloBillingTransactionLines.SetNDCFields(oLineTransaction);
                            oLineTransaction.Dispose();
                    //        oLineTransactions.Dispose();
                        }
                        ofrmDrugBilling._oTransLine.Dispose();
                        ofrmDrugBilling.Dispose();
                    }
                    else if (sNDCCode.Trim() != "")
                    {
                        oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode = sNDCCode;
                        frmDrugBilling ofrmDrugBilling = new frmDrugBilling(_DatabaseConnectionString, oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1], IsIncDescriptionCheck: true);
                        ofrmDrugBilling.ShowDialog(this);

                        if (ofrmDrugBilling.oDialogResult)
                        {
                            oLineTransaction = ofrmDrugBilling._oTransLine;
                            UC_gloBillingTransactionLines.SetNDCFields(oLineTransaction);
                            oLineTransaction.Dispose();
                      //      oLineTransactions.Dispose();
                        }
                        ofrmDrugBilling._oTransLine.Dispose();
                        ofrmDrugBilling.Dispose();
                    }
                    else
                    {
                        frmDrugBilling ofrmDrugBilling = new frmDrugBilling(_DatabaseConnectionString, oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1]);
                        ofrmDrugBilling.ShowDialog(this);

                        if (ofrmDrugBilling.oDialogResult)
                        {
                            oLineTransaction = ofrmDrugBilling._oTransLine;
                            UC_gloBillingTransactionLines.SetNDCFields(oLineTransaction);
                            oLineTransaction.Dispose();
                        //    oLineTransactions.Dispose();
                        }
                        ofrmDrugBilling._oTransLine.Dispose();
                        ofrmDrugBilling.Dispose();
                    }


                }

            }
            else
            {
                MessageBox.Show("No transaction line is selected.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (oLineTransactions != null)
            {
                oLineTransactions.Dispose();
                oLineTransactions = null;
            }
            #endregion
        }

        private void tls_btnMoreClaimData_Click(object sender, EventArgs e)
        {
            #region " Add Claim Box19 Notes "

            //if (_oBox19Notes == null) { _oBox19Notes = new ClaimBox19Notes(); }

            //if (_oBox19Note != null)
            //{
            //    if (_oBox19Notes != null && _oBox19Notes.Count == 0)
            //    {
            //        _oBox19Notes.Clear();
            //        _oBox19Notes.Add(_oBox19Note);
            //    }
            //}
            //Int64 _nInsuranceID = 0;
            //if (c1Insurance.Rows.Count > 1)
            //{
            //    _nInsuranceID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
            //}
            //_sClaimBox10dNote = GetDefaultBox19Note(_nInsuranceID);
            //if (_sClaimBox10dNote != "")
            //{
            //    _oBox19Note.TransactionID = _TransactionID; // Convert.ToInt64(dt.Rows[i]["nTransactionID"]);
            //    _oBox19Note.NoteType = NoteType.Box19_Note; //NoteType.GeneralNote;
            //    _oBox19Note.NoteDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString()); //Convert.ToInt64(dt.Rows[i]["nNoteDateTime"]);
            //    _oBox19Note.UserID = _UserID; //Convert.ToInt64(dt.Rows[i]["nUserID"]);
            //    _oBox19Note.Box19NoteDescription = _sClaimBox10dNote; //Convert.ToString(dt.Rows[i]["sNoteDescription"]);
            //    _oBox19Note.ClinicID = _ClinicID;
            //    _oBox19Note.UserName = _UserName;
            //    _oBox19Note.NoteRowID = 1;
            //    _oBox19Note.BillingNoteType = EOBPaymentSubType.Claim_Box19Note;
            //    _oBox19Note.NoteDateTime = DateTime.Now;
            //    _oBox19Notes.Add(_oBox19Note);
            //}

            frmReplacementClaimIndication ofrmReplacementClaimIndication = new frmReplacementClaimIndication(_DatabaseConnectionString, _ClinicID, _TransactionID, _oBox19Notes, _sClaimRefNo, _IsbCliamReplacement, _IllnessDate);
            ofrmReplacementClaimIndication.ClaimBox10dNote = _sClaimBox10dNote;
            ofrmReplacementClaimIndication.sDelayReasonCode = _sDelayReasonCode;
            ofrmReplacementClaimIndication.sMedicaidRebumissionCode = _sMedicaidResubmissionCode;
            ofrmReplacementClaimIndication.sServiceAuthExceptionCode = _sServiceAuthExceptionCode;
            ofrmReplacementClaimIndication.bEnableSupervisorOption = bEnableSupervisorOption;
            ofrmReplacementClaimIndication.bIsRefProAsSupervisor = _bIsRefprovAsSupervisor;
            ofrmReplacementClaimIndication.bIsUB = tls_btnUB04Data.Visible;
            ofrmReplacementClaimIndication.bIsRebilled = false;

            ofrmReplacementClaimIndication.PAccountID = oPatientControl.PAccountID;
            ofrmReplacementClaimIndication.AccountPatientID = oPatientControl.AccountPatientID;
            ofrmReplacementClaimIndication.PatientID = oPatientControl.CmbSelectedPatientID;

            ofrmReplacementClaimIndication.nReplacementByTransMasterID = nReplacementByTransMasterID;
            ofrmReplacementClaimIndication.nReplacingTransMasterID = nReplacingTransMasterID;
            ofrmReplacementClaimIndication.bShowReplacementClaim = true;
            ofrmReplacementClaimIndication.LastSeenDate = _LastSeenDate;
            ofrmReplacementClaimIndication.MoreClaimData_UnableToWorkFrom = _UnableToWorkFromDate_MoreClaimData;
            ofrmReplacementClaimIndication.MoreClaimData_UnableToWorkTill = _UnableToWorkTillDate_MoreClaimData;
            ofrmReplacementClaimIndication.sPWKReportTypeCode = _PWKReportTypeCode;
            ofrmReplacementClaimIndication.sPWKReportTransmissionCode = _PWKReportTransmissionCode;
            ofrmReplacementClaimIndication.sPWKAttachmentControlNumber = _PWKAttachmentControlNumber;
            ofrmReplacementClaimIndication.sMammogramCertNumber = _MammogramCertNumber;
            ofrmReplacementClaimIndication.sIDENo = _IDENo;
            ofrmReplacementClaimIndication.ShowDialog(this);

            if (ofrmReplacementClaimIndication.oDialogResult)
            {
                _oBox19Note = ofrmReplacementClaimIndication._oBox19Note;
                _sClaimBox10dNote = ofrmReplacementClaimIndication.ClaimBox10dNote;

                if (_oBox19Note != null)
                {

                    if (_oBox19Notes.Count > 0)
                        _oBox19Notes.Clear();
                    _oBox19Notes.Add(_oBox19Note);
                }

                _IsbCliamReplacement = ofrmReplacementClaimIndication.IsbCliamReplacement;
                _bIsRefprovAsSupervisor = ofrmReplacementClaimIndication.bIsRefProAsSupervisor;
                _sClaimRefNo = ofrmReplacementClaimIndication.sClaimRefNo;

                nReplacementByTransMasterID = ofrmReplacementClaimIndication.nReplacementByTransMasterID;
                nReplacingTransMasterID = ofrmReplacementClaimIndication.nReplacingTransMasterID;

                _IllnessDate = ofrmReplacementClaimIndication.IllnessDate;
                _LastSeenDate = ofrmReplacementClaimIndication.LastSeenDate;
                _UnableToWorkFromDate_MoreClaimData = ofrmReplacementClaimIndication.MoreClaimData_UnableToWorkFrom;
                _UnableToWorkTillDate_MoreClaimData = ofrmReplacementClaimIndication.MoreClaimData_UnableToWorkTill;
                _sDelayReasonCode = ofrmReplacementClaimIndication.sDelayReasonCode;
                _sMedicaidResubmissionCode = ofrmReplacementClaimIndication.sMedicaidRebumissionCode;
                _sServiceAuthExceptionCode = ofrmReplacementClaimIndication.sServiceAuthExceptionCode;
                _PWKReportTypeCode = ofrmReplacementClaimIndication.sPWKReportTypeCode;
                _PWKReportTransmissionCode = ofrmReplacementClaimIndication.sPWKReportTransmissionCode;
                _PWKAttachmentControlNumber = ofrmReplacementClaimIndication.sPWKAttachmentControlNumber;
                _MammogramCertNumber = ofrmReplacementClaimIndication.sMammogramCertNumber;
                _IDENo = ofrmReplacementClaimIndication.sIDENo;
                pnlReplacmentClaimDtls.Visible = false;
                lblReplacementClaim.Text = "";
                lblReplacementClaimNo.Text = "";
                lblReplacementClaim.Tag = "";
                lblReplacementClaimNo.Tag = "";

                if (nReplacementByTransMasterID > 0)
                {
                    lblReplacementClaim.Text = "Replaced By Claim : " + ofrmReplacementClaimIndication.sReplacementByClaimNo;
                    pnlReplacmentClaimDtls.Visible = true;
                    lblReplacementClaim.Tag = nReplacementByTransMasterID;
                }
                if (nReplacingTransMasterID > 0)
                {
                    lblReplacementClaim.Text = "Replaces Claim :  " + ofrmReplacementClaimIndication.sReplacingClaimNo;
                    pnlReplacmentClaimDtls.Visible = true;
                    lblReplacementClaim.Tag = nReplacingTransMasterID;

                }
                if (nReplacementByTransMasterID != 0 || nReplacingTransMasterID != 0)
                {
                    pnlAlerts.Visible = true;
                }
                UC_gloBillingTransactionLines.sMammogramCertNo = _MammogramCertNumber;
            }
            else
            {
                if (_oBox19Note != null)
                {
                    if (_oBox19Notes.Count > 0)
                        _oBox19Notes.Clear();
                    _oBox19Notes.Add(_oBox19Note);
                }
            }
            ofrmReplacementClaimIndication._oBox19Note.Dispose();
            ofrmReplacementClaimIndication.Dispose();
            SetHoldnMoreClaimDataMesseges();
            //if (lblHoldMessage.Text.Trim() != string.Empty)
            //{
            //    if (_IsbCliamReplacement || _sClaimRefNo != "" || (_oBox19Notes != null && _oBox19Notes.Count > 0 && _oBox19Notes[0].Box19NoteDescription.Trim() != "") || _IllnessDate > 0 || _sDelayReasonCode.Trim() != "" || _sServiceAuthExceptionCode.Trim() != "")
            //    {
            //        if (!lblHoldMessage.Text.Contains("More Claim Data is present"))
            //            lblHoldMessage.Text = lblHoldMessage.Text + "; " + "More Claim Data is present";
            //    }
            //    else
            //    {
            //        if (lblHoldMessage.Text.Contains(";"))
            //            lblHoldMessage.Text = lblHoldMessage.Text.Trim().Remove(lblHoldMessage.Text.Trim().IndexOf("; More Claim Data is present"));
            //        else
            //        {
            //            if (lblHoldMessage.Text.Contains("More Claim Data is present"))
            //                lblHoldMessage.Text = lblHoldMessage.Text.Trim().Remove(lblHoldMessage.Text.Trim().IndexOf("More Claim Data is present"));
            //        }
            //    }
            //}
            //else
            //{
            //    if (_IsbCliamReplacement || _sClaimRefNo != "" || (_oBox19Notes != null && _oBox19Notes.Count > 0 && _oBox19Notes[0].Box19NoteDescription.Trim() != "") || _IllnessDate > 0 || _sDelayReasonCode.Trim() != "" || _sServiceAuthExceptionCode.Trim() != "")
            //    {
            //        if (!lblHoldMessage.Text.Contains("More Claim Data is present"))
            //            lblHoldMessage.Text = "More Claim Data is present";
            //    }
            //    else
            //    {
            //        lblHoldMessage.Text = "";
            //    }
            //}

            #endregion
        }

        private void tls_btnPayment_Click(object sender, EventArgs e)
        {
            Int64 _ClaimNo = 0;

            string _strQuery = "SELECT ISNULL(MAX(nClaimNo),0) as nClaimNo FROM BL_Transaction_MST WITH (NOLOCK) Where nPatientID=" + _PatientID.ToString();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            oDB.Connect(false);
            DataTable dtClaim = null;
            oDB.Retrive_Query(_strQuery, out dtClaim);
            if (dtClaim != null)
            {
                if (dtClaim.Rows.Count > 0)
                {
                    _ClaimNo = Convert.ToInt64(dtClaim.Rows[0]["nClaimNo"]);
                }
                dtClaim.Dispose();
                dtClaim = null;
            }
            
            oDB.Dispose();

            gloAccountsV2.frmPatientPaymentV2 ofrmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(_PatientID, false, _ClaimNo, 0, 0, 0, 0);
            ofrmPatientPaymentV2.WindowState = FormWindowState.Maximized;
            ofrmPatientPaymentV2.ShowInTaskbar = false;
            ofrmPatientPaymentV2.ShowDialog(this);
            ofrmPatientPaymentV2.Dispose();
        }

        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            _IsSaveNextTreatmentClick = false;
            //gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            //if (oSecurity.isBadDebtPatient(oPatientControl.PatientID, true))
            //{
            //    DialogResult dr = System.Windows.Forms.MessageBox.Show("The status of the patient is Bad Debt.Do you want to continue? ", _messageBoxCaption, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
            //    if (dr == DialogResult.No)
            //    {
            //        return;
            //    }
            //}
            //if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }
            if (_IsValidDate)
            {
                
                    SaveTreatment();
                
            }
            else
            {
                MessageBox.Show("Please enter a valid date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tls_btnSaveNClose_Click(object sender, EventArgs e)
        {

            bIsChargeSaved = false;
            bool bIsChargeRulesEnabled = false;

            //gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            //if (oSecurity.isBadDebtPatient(oPatientControl.PatientID, true))
            //{
            //    DialogResult dr = System.Windows.Forms.MessageBox.Show("Patient is in BAD DEBT status, are you sure you want to create a new charge ?", _messageBoxCaption, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
            //    if (dr == DialogResult.No)
            //    {
            //        return;
            //    }
            //}
            //if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }

            if (_IsValidDate)
            {
                #region " SaveNClode Code"
                if (ValidateInsurance())
                {
                    if (!(UC_gloBillingTransactionLines == null))
                    {
                        if (UC_gloBillingTransactionLines.GetLinesCount() > 1)
                        {

                            if (ValidationMessageExpClaim())
                            {

                                bIsChargeRulesEnabled = IsClaimRulesEnabled();

                                if (bIsChargeRulesEnabled)
                                {
                                    if (ValidateRules())
                                    {
                                        if (SaveClaimData(true) == true)
                                        {
                                            CL_FollowUpCode.SetAutoAccountFollowUp(oPatientControl.PAccountID, oPatientControl.PatientID, Convert.ToDateTime(mskClaimDate.Text));
                                            _IsModifyed = true;
                                            this.Close();
                                            bIsChargeSaved = true;
                                        }
                                    }

                                }
                                else
                                {
                                    if (SaveClaimData(true) == true)
                                    {
                                        CL_FollowUpCode.SetAutoAccountFollowUp(oPatientControl.PAccountID, oPatientControl.PatientID, Convert.ToDateTime(mskClaimDate.Text));
                                        _IsModifyed = true;
                                        this.Close();
                                        bIsChargeSaved = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter at least one service line to save claim.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                #endregion
            }
            else
            {
                MessageBox.Show("Please enter a valid date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            #region "Code For Close The Charge"
            {
                if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.IsTransactionDataPresent() == true)
                {
                    DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        tls_Top.Select();
                        if (_IsValidDate)
                        {
                            //if (ValidateICD10DOS() == true)
                            //{
                                if (Ok_Click() == true)
                                {
                                    if (OpenViewMode == false)
                                    {
                                        gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, "");
                                        ogloBilling.UpdateRecordStatus(_TransactionID, 0, false);
                                        ogloBilling.Dispose();
                                    }
                                    this.Close();
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.View, "View Setup Charges", 0, _TransactionID, 0, ActivityOutCome.Success);
                                }
                           // }
                        }
                    }
                    else if (res == DialogResult.No)
                    {
                        if (OpenViewMode == false)
                        {
                            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, "");
                            ogloBilling.UpdateRecordStatus(_TransactionID, 0, false);
                            ogloBilling.Dispose();
                        }
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.View, "View Setup Charges", 0, _TransactionID, 0, ActivityOutCome.Success);
                        this.mskClaimDate.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.mskAccidentDate.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.mskHospitaliztionFrom.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.mskHospitaliztionTo.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.mskInjuryDate.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.mskOnsiteDate.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.mskOtherDate.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.mskUnableFromDate.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.mskUnableTillDate.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.mskInitTreatment.Validating -= new CancelEventHandler(mskDate_Validating);
                        this.Close();

                    }
                    else if (res == DialogResult.Cancel)
                    {

                        return;
                    }
                }
                else
                {
                    this.mskClaimDate.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.mskAccidentDate.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.mskHospitaliztionFrom.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.mskHospitaliztionTo.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.mskInjuryDate.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.mskOnsiteDate.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.mskOtherDate.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.mskUnableFromDate.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.mskUnableTillDate.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.mskInitTreatment.Validating -= new CancelEventHandler(mskDate_Validating);
                    this.Close();

                }

                _IsModifyed = true;

            }
            #endregion
        }

        private void tls_btnUB04Data_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _nInsuranceID = 0;
                if (c1Insurance.Rows.Count > 1)
                {
                    _nInsuranceID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                }
                string sMinDos = Convert.ToString(gloDateMaster.gloDate.DateAsDate((UC_gloBillingTransactionLines.GetMinDOS())));
                if (UC_gloBillingTransactionLines.GetLinesCount() <= 1 || oUB.MinDOSDeleted == true) sMinDos = null;
                if (oUB != null && (Convert.ToString(oUB.sTypeofbill) == "" || oUB.sTypeofbill == null) && oUB.TypeofbillDeleted == false)
                {
                    oUB.sTypeofbill = TypeOfBill;
                }
                if (oUB != null && (Convert.ToString(oUB.sAdmissionType) == "" || oUB.sAdmissionType == null) && oUB.AdmitTypeDeleted == false)
                {
                    oUB.sAdmissionType = AdmitType;
                }
                if (oUB != null && (Convert.ToString(oUB.sDischargeStatus) == "" || oUB.sDischargeStatus == null) && oUB.DischargeStatusDeleted == false)
                {
                    oUB.sDischargeStatus = DischargeStatus;
                }
                //RoopaliB 19 July 2012
                //Occurance date should be default to DOS or not on UB04 form.                           
                if (!IsDefaultOccuranceDOS(_nInsuranceID))
                {
                    sMinDos = "";
                    oUB.MinDOSDeleted = true;
                    //oUB.sOccurrenceDate01 = "";
                }
                else
                {
                    oUB.MinDOSDeleted = false;
                    sMinDos = Convert.ToString(gloDateMaster.gloDate.DateAsDate((UC_gloBillingTransactionLines.GetMinDOS())));
                }

                frmUB04Data ofrm = new frmUB04Data(_DatabaseConnectionString, _TransactionID, oUB, sMinDos);
                ofrm.ShowDialog(this);
                oUB = ofrm._oUB;
                ofrm.Dispose();
                SetHoldnMoreClaimDataMesseges();
            }
            catch (Exception exUbo4)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exUbo4.ToString(), false);
            }
            finally
            {
            }


        }

        private void tlb_AddEPSDT_Click(object sender, EventArgs e)
        {
            TransactionLines oLineTransactions = null;
            TransactionLine oLineTransaction = null;
            frmSetupEPSDTFamilyPlanning ofrmEPSDTFamilyPlanning = null;

            try
            {
                #region " Add Charge Fields "

                oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();

                if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                {
                    oLineTransaction = oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1];
                }
                else
                {
                    MessageBox.Show("No service line is selected.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                #endregion

                ofrmEPSDTFamilyPlanning = new frmSetupEPSDTFamilyPlanning(oLineTransaction, ClaimEPSDT);
                ofrmEPSDTFamilyPlanning.ShowDialog(this);

                if (ofrmEPSDTFamilyPlanning.DialogResults)
                {
                    oLineTransaction = ofrmEPSDTFamilyPlanning.LineObject;
                    ClaimEPSDT = ofrmEPSDTFamilyPlanning.ClaimEPSDT;

                    UC_gloBillingTransactionLines.SetEPSDTFields(oLineTransaction);
                   // if (oLineTransaction != null) { oLineTransaction.Dispose(); }
                    //if (oLineTransactions != null) { oLineTransactions.Dispose(); }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ofrmEPSDTFamilyPlanning != null) { ofrmEPSDTFamilyPlanning.Dispose(); ofrmEPSDTFamilyPlanning = null; }
                if (oLineTransaction != null) { oLineTransaction.Dispose(); oLineTransaction = null; }
                if (oLineTransactions != null) { oLineTransactions.Dispose(); oLineTransactions = null; }
            }



        }

        private void tls_Anesthesia_Click(object sender, EventArgs e)
        {
            TransactionLines oLineTransactions = null;
            TransactionLine oLineTransaction = null;
            frmAnesthesiaBilling ofrmAnesthesiaBilling = null;

            try
            {
                #region " Add Anesthesia "

                oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();

                if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                {
                    oLineTransaction = oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1];
                    ofrmAnesthesiaBilling = new frmAnesthesiaBilling(oLineTransaction);
                    if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                    {
                        ofrmAnesthesiaBilling.nContactID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));

                    }
                    else
                    {
                        ofrmAnesthesiaBilling.nContactID = 0;
                    }
                    ofrmAnesthesiaBilling.ShowDialog(this);

                    if (ofrmAnesthesiaBilling.oDialogResult)
                    {
                        oLineTransaction = ofrmAnesthesiaBilling.oTransLine;
                        UC_gloBillingTransactionLines.SetAnesthesiaFields(oLineTransaction);
                     //   if (oLineTransaction != null) { oLineTransaction.Dispose(); }
                       // if (oLineTransactions != null) { oLineTransactions.Dispose(); }
                    }
                }
                else
                {
                    MessageBox.Show("No service line is selected.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                #endregion

                

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ofrmAnesthesiaBilling != null) { ofrmAnesthesiaBilling.Dispose(); ofrmAnesthesiaBilling = null; }
                if (oLineTransaction != null) { oLineTransaction.Dispose(); oLineTransaction = null; }
                if (oLineTransactions != null) { oLineTransactions.Dispose(); oLineTransactions = null; }
            }



        }

        private void tls_btnInsurancePayment_Click(object sender, EventArgs e)
        {
            try
            {
                _IsSaveNextTreatmentClick = false;
                if (_IsValidDate)
                {
                    string nClaimNo = txtClaimNo.Text;
                    if (!SaveTreatment())
                    {
                        return;
                    }

                    gloAccountsV2.frmInsurancePaymentV2 oPaymentInsurace = new gloAccountsV2.frmInsurancePaymentV2(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    oPaymentInsurace.StartPosition = FormStartPosition.CenterScreen;
                    oPaymentInsurace.WindowState = FormWindowState.Maximized;
                    oPaymentInsurace.VoidAndReplaceClaimNo = nClaimNo;
                    oPaymentInsurace.IsOpenFromVoidAndReplace = true;

                    oPaymentInsurace.ShowInTaskbar = false;
                    oPaymentInsurace.ShowDialog(this);
                    oPaymentInsurace.Dispose();
                    oPaymentInsurace = null;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter a valid date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        
        #endregion " Tool strip Button Click Events "

        #region " AlphaII Validation "

        private string _AlphaIIServerName = "";
        private string _AlphaIIDatabase = "";
        private string _AlphaIIUserName = "";
        private string _AlphaIIPassword = "";
        private string _AlphaIIAuthentication = "";
        private string _AlphaIIValidation = "";

        private bool ValidateUsingAlphaII(string DxCode, DateTime DateOfService)
        {
            bool _IsAplhaIIValidated = true;
            try
            {
                string ConnectionString = "";

                ConnectionString = GetAlphaIIConnectionString();
                if (ConnectionString != "")
                {
                    //AlphaII.CodeWizard.DataAccess.Common.ConnectionString = ConnectionString;
                    AlphaII.CodeWizard.Configuration.DatabaseConfiguration oDatabaseConfiguration = new AlphaII.CodeWizard.Configuration.DatabaseConfiguration();
                    oDatabaseConfiguration.MsSqlServer = _AlphaIIServerName;
                    oDatabaseConfiguration.MsSqlDatabase = _AlphaIIDatabase;
                    oDatabaseConfiguration.MsSqlUserId = _AlphaIIUserName;
                    oDatabaseConfiguration.MsSqlPassword = _AlphaIIPassword;
                    oDatabaseConfiguration.MsSqlPersistSecurity = false;
                    if (_AlphaIIAuthentication.ToUpper() == "WINDOWS")
                    {
                        oDatabaseConfiguration.MsSqlIntegratedSecurity = true;
                    }
                    else if (_AlphaIIAuthentication.ToUpper() == "SQL")
                    {
                        oDatabaseConfiguration.MsSqlIntegratedSecurity = false;
                    }
                    oDatabaseConfiguration.Save();
                   
                    AlphaII.CodeWizard.Coding oCoding = new AlphaII.CodeWizard.Coding();
                    _IsAplhaIIValidated = oCoding.ValidateDiagnosisCode(DxCode.Trim(), DateOfService);
                    //_IsAplhaIIValidated = GetValidCode(DxCode.Trim(), DateOfService, out  _IsAplhaIIValidated);
                    string strDxDesc = oCoding.GetDiagnosisDescription(DxCode.Trim(), DateOfService, AlphaII.CodeWizard.Objects.DescriptionType.Long);
                    oCoding = null;
                    oDatabaseConfiguration = null;

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return true;
            }
            return _IsAplhaIIValidated;
        }

        private void AlphaIIDiagnosisValidation()
        {
            TransactionLines oTransactionLines = null;
            string _strMessage = "";
            string _strMessageFiller = "is";
            ArrayList arrDxCodes = new ArrayList();
            try
            {
                oTransactionLines = UC_gloBillingTransactionLines.GetLineTransactions();
                if (oTransactionLines != null && oTransactionLines.Count > 0)
                {
                    for (int i = 0; i <= oTransactionLines.Count - 1; i++)
                    {
                        if (oTransactionLines[i].Dx1Code.Trim() != "" || oTransactionLines[i].Dx2Code.Trim() != "" || oTransactionLines[i].Dx3Code.Trim() != "" || oTransactionLines[i].Dx4Code.Trim() != "")
                        {

                            if (oTransactionLines[i].Dx1Code.Trim() != "")
                            {
                                if (ValidateUsingAlphaII(oTransactionLines[i].Dx1Code, oTransactionLines[i].DateServiceFrom) == false)
                                {
                                    if (IsDiagnosisExist(_strMessage, oTransactionLines[i].Dx1Code.Trim()) == false)
                                    {
                                        _strMessage += " " + oTransactionLines[i].Dx1Code.Trim() + ",";
                                    }
                                }
                                arrDxCodes.Add(oTransactionLines[i].Dx1Code);
                            }
                            if (oTransactionLines[i].Dx2Code.Trim() != "")
                            {
                                if (ValidateUsingAlphaII(oTransactionLines[i].Dx2Code, oTransactionLines[i].DateServiceFrom) == false)
                                {
                                    if (IsDiagnosisExist(_strMessage, oTransactionLines[i].Dx2Code.Trim()) == false)
                                    {
                                        _strMessage += " " + oTransactionLines[i].Dx2Code.Trim() + ",";
                                    }
                                }
                                arrDxCodes.Add(oTransactionLines[i].Dx2Code);
                            }
                            if (oTransactionLines[i].Dx3Code.Trim() != "")
                            {
                                if (ValidateUsingAlphaII(oTransactionLines[i].Dx3Code, oTransactionLines[i].DateServiceFrom) == false)
                                {
                                    if (IsDiagnosisExist(_strMessage, oTransactionLines[i].Dx3Code.Trim()) == false)
                                    {
                                        _strMessage += " " + oTransactionLines[i].Dx3Code.Trim() + ",";
                                    }
                                }
                                arrDxCodes.Add(oTransactionLines[i].Dx3Code);
                            }
                            if (oTransactionLines[i].Dx4Code.Trim() != "")
                            {
                                if (ValidateUsingAlphaII(oTransactionLines[i].Dx4Code, oTransactionLines[i].DateServiceFrom) == false)
                                {
                                    if (IsDiagnosisExist(_strMessage, oTransactionLines[i].Dx4Code.Trim()) == false)
                                    {
                                        _strMessage += " " + oTransactionLines[i].Dx4Code.Trim() + ",";
                                    }
                                }
                                arrDxCodes.Add(oTransactionLines[i].Dx4Code);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select at least one diagnosis for line " + (i + 1).ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        CheckValid_ICD_CPTAssociation(oTransactionLines[i].CPTCode, arrDxCodes, oTransactionLines[i].DateServiceFrom);
                    }
                    if (_strMessage.Trim() != "")
                    {
                        if (_strMessage.Trim().Length > 8)
                        {
                            _strMessageFiller = "";
                            _strMessageFiller = "are";
                        }
                        _strMessage = _strMessage.Substring(0, _strMessage.Length - 1);
                        MessageBox.Show("The Diagnosis code(s) " + _strMessage.Trim() + " " + _strMessageFiller + " not valid.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("All diagnosis are valid.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oTransactionLines != null)
                {
                    oTransactionLines.Dispose();
                    oTransactionLines = null;
                }
                _arrDxCodes.Clear();
            }
        }

        public static bool GetValidCode(string diagnosisCode, DateTime serviceDate, out bool notSpecific)
        {
            bool flag = false;
            notSpecific = false;
            if ((diagnosisCode != null) && (diagnosisCode.Length > 0))
            {
                SqlConnection connection = new SqlConnection(AlphaII.CodeWizard.DataAccess.Common.ConnectionString);
                SqlCommand command = new SqlCommand("GetDiagnosisValid", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DiagCode", SqlDbType.VarChar, 9);
                command.Parameters.Add("@DateOfSvc", SqlDbType.SmallDateTime);
                command.Parameters.Add("@ReturnValue", SqlDbType.SmallInt);
                command.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
                try
                {
                    command.Parameters["@DiagCode"].Value = diagnosisCode;
                    command.Parameters["@DateOfSvc"].Value = serviceDate;
                    connection.Open();
                    command.ExecuteNonQuery();
                    int num = Convert.ToInt32(command.Parameters["@ReturnValue"].Value);
                    if (num == 0)
                    {
                        return true;
                    }
                    if (num != 0xbba)
                    {
                        return flag;
                    }
                    if ((diagnosisCode[0] == 'E') && (diagnosisCode.Length >= 4))
                    {
                        notSpecific = true;
                        return flag;
                    }
                    if (diagnosisCode.Length >= 3)
                    {
                        notSpecific = true;
                    }
                }
                finally
                {
                    connection.Close();
                    command.Parameters.Clear();
                    command.Dispose();
                    command = null;
                    connection.Dispose();
                }
            }
            return flag;
        }

        private void GetAlphaIISettings()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_DatabaseConnectionString);
            object value = null;
            try
            {
                ogloSettings.GetSetting("AlphaII SQL Server Name", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIServerName = Convert.ToString(value.ToString());
                    value = null;
                }

                ogloSettings.GetSetting("AlphaII Database Name", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIDatabase = Convert.ToString(value.ToString());
                    value = null;
                }

                ogloSettings.GetSetting("AlphaII Authentication", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIAuthentication = Convert.ToString(value.ToString());
                    value = null;
                }
                ogloSettings.GetSetting("AlphaII User Name", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIUserName = Convert.ToString(value.ToString());
                    value = null;
                }
                ogloSettings.GetSetting("AlphaII Password", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIPassword = Convert.ToString(value.ToString());
                    value = null;
                }
                ogloSettings.GetSetting("ClaimValidationSetting", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIValidation = Convert.ToString(value.ToString());
                    value = null;
                }
                ogloSettings.GetSetting("IsCheckInvalidICD9", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    //_IsCheckInvalidICD9 = Convert.ToBoolean(value);
                    value = null;
                }
                ogloSettings.GetSetting("IsUseScrubber", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    //_IsScrubber = Convert.ToBoolean(value);
                    value = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                value = null;
            }
        }

        private bool ValidateConnectionString()
        {
            Boolean _Result = false;
            SqlConnection _connection = new SqlConnection();
            try
            {
                string _connstring = "";

                if (_AlphaIIAuthentication.ToUpper() == "WINDOWS")
                {
                    _connstring = "Integrated Security=SSPI; Persist Security Info=False; Data Source=" + _AlphaIIServerName + "; Initial Catalog=" + _AlphaIIDatabase + "; Connection Timeout = 0";
                }
                else
                {
                    _connstring = "Persist Security Info=False;Data Source=" + _AlphaIIServerName + ";Initial Catalog=" + _AlphaIIDatabase + ";User ID=" + _AlphaIIUserName + ";Pwd=" + _AlphaIIPassword + ";";
                }


                _connection.ConnectionString = _connstring;
                _connection.Open();
                _connection.Close();

                _Result = true;
            }
            catch (Exception)
            {
                _Result = false;
                if (_Result == false)
                {
                    MessageBox.Show("Connection can not established with given parameter, please verify AlphaII setting.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }



            return _Result;
        }

        private string GetAlphaIIConnectionString()
        {
            string _connstring = "";
            try
            {
                if (_AlphaIIAuthentication.ToUpper() == "WINDOWS")
                {
                    if (_AlphaIIServerName != "" && _AlphaIIDatabase != "")
                    {
                        _connstring = "Integrated Security=SSPI; Persist Security Info=False; Data Source=" + _AlphaIIServerName + "; Initial Catalog=" + _AlphaIIDatabase + "; Connection Timeout = 0";
                        //Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CodeWizard;Data Source=GLOINT

                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    if (_AlphaIIServerName != "" && _AlphaIIDatabase != "" && _AlphaIIUserName != "")//&& _AlphaIIPassword != "")
                    {
                        _connstring = "Persist Security Info=False;Data Source=" + _AlphaIIServerName + ";Initial Catalog=" + _AlphaIIDatabase + ";User ID=" + _AlphaIIUserName + ";Pwd=" + _AlphaIIPassword + ";";
                        // Persist Security Info=False;User ID=sa;Initial Catalog=CodeWizard;Data Source=GLOINT
                    }
                    else
                    {
                        return "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return _connstring;
        }

        private bool IsDiagnosisExist(string strMessage, string DxCode)
        {
            string[] strDxList = null;
            bool _IsExist = false;
            try
            {
                if (strMessage.Trim() != "")
                {
                    strDxList = strMessage.Trim().Split(',');
                    if (strDxList != null && strDxList.Length > 0)
                    {
                        for (int i = 0; i < strDxList.Length; i++)
                        {
                            if (strDxList[i].Trim().ToUpper() == DxCode.Trim().ToUpper())
                            {
                                _IsExist = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _IsExist;
        }

        private void CheckValid_ICD_CPTAssociation(string CPTCode, ArrayList DxCode, DateTime DOS)
        {
                AlphaII.CodeWizard.Coding oCoding = new AlphaII.CodeWizard.Coding();
                AlphaII.CodeWizard.Collections.EditCollection oEditCollection = null;
                // AlphaII.CodeWizard.Objects.Edit oEdit = new AlphaII.CodeWizard.Objects.Edit();
                AlphaII.CodeWizard.Objects.Diagnosis oDiagnosis = null;
                AlphaII.CodeWizard.Collections.DiagnosisCollection oDiagnosisCollection = new AlphaII.CodeWizard.Collections.DiagnosisCollection();
        
            try
            {
                    if (DxCode != null && DxCode.Count > 0 && CPTCode.Trim() != "")
                {
                    for (int _Index = 0; _Index < DxCode.Count; _Index++)
                    {
                        oDiagnosis = new AlphaII.CodeWizard.Objects.Diagnosis(Convert.ToString(DxCode[_Index]).Trim());
                        oDiagnosisCollection.Add(oDiagnosis);
                    }
                    AlphaII.CodeWizard.Objects.Procedure _cpt = new AlphaII.CodeWizard.Objects.Procedure(DOS, CPTCode, null);
                    oEditCollection = oCoding.GetAllEdits();
                    if (oEditCollection != null && oEditCollection.Count > 0)
                    {
                        //oEdit = oCoding.GetCodingEdit();
                        StringBuilder _details = new StringBuilder();
                        for (int i = 0; i < oEditCollection.Count; i++)
                        {
                            _details = new StringBuilder();
                            // Build a details messgae.
                            _details.Append("Description = " + oEditCollection[i].Description + "\r\n");
                        }
                        txtMessages.Text += _details.ToString();
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oCoding != null)
                {
                   // oCoding.Dispose();
                    oCoding = null;
                }
                if (oEditCollection != null)
                {
                    oEditCollection.Clear();
                }
                if (oDiagnosisCollection != null)
                {
                    oDiagnosisCollection.Clear();
                }
                
            }
        }

        #endregion " AlphaII Validation "

        #region " YOST - Claim claim validation "

        private void ValidateClaim()
        {
            TransactionLines oTransactionLines = null;
            ClsTransaction oClaimTransaction = new ClsTransaction();
            ClsServiceInfo oServiceInfo = null;

            try
            {
                oTransactionLines = UC_gloBillingTransactionLines.GetLineTransactions();
                if (oTransactionLines != null && oTransactionLines.Count > 0)
                {
                    for (int i = 0; i <= oTransactionLines.Count - 1; i++)
                    {
                        oServiceInfo = new ClsServiceInfo();
                        oClaimTransaction.ActionCode = "V";
                        oClaimTransaction.CodeSet = "";
                        oClaimTransaction.SearchTerms = "";
                        oClaimTransaction.TransactionID = i.ToString();
                        if (oPatientControl.PatientGender == "Male")
                        {
                            oClaimTransaction.Patient.Gender = "M";
                        }
                        else if (oPatientControl.PatientGender == "Female")
                        {
                            oClaimTransaction.Patient.Gender = "F";
                        }


                        oClaimTransaction.Patient.Day = oPatientControl.PatientDateOfBirth.Day.ToString();
                        oClaimTransaction.Patient.Month = oPatientControl.PatientDateOfBirth.Month.ToString();
                        oClaimTransaction.Patient.Year = oPatientControl.PatientDateOfBirth.Year.ToString();

                        oServiceInfo.FromDay = oTransactionLines[i].DateServiceFrom.Day.ToString();
                        oServiceInfo.FromMonth = oTransactionLines[i].DateServiceFrom.Month.ToString();
                        oServiceInfo.FromYear = oTransactionLines[i].DateServiceFrom.Year.ToString();

                        oServiceInfo.ToDay = oTransactionLines[i].DateServiceTill.Day.ToString();
                        oServiceInfo.ToMonth = oTransactionLines[i].DateServiceTill.Month.ToString();
                        oServiceInfo.ToYear = oTransactionLines[i].DateServiceTill.Year.ToString();

                        oServiceInfo.POS = oTransactionLines[i].POSCode;
                        oServiceInfo.ProcCode = oTransactionLines[i].CPTCode;


                        if (oTransactionLines[i].Dx1Ptr == true)
                        {
                            if (oTransactionLines[i].Dx1Code.Trim() != "")
                            {
                                oServiceInfo.DiagnosisList.Add(oTransactionLines[i].Dx1Code.Trim());
                            }
                            else
                            {
                                MessageBox.Show("Diagnosis has not been entered, cannot validate.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Diagnosis has not been entered, cannot validate.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (oTransactionLines[i].Dx2Ptr == true)
                        {
                            if (oTransactionLines[i].Dx2Code.Trim() != "")
                            {
                                oServiceInfo.DiagnosisList.Add(oTransactionLines[i].Dx2Code.Trim());
                            }
                        }

                        if (oTransactionLines[i].Dx3Ptr == true)
                        {
                            if (oTransactionLines[i].Dx3Code.Trim() != "")
                            {
                                oServiceInfo.DiagnosisList.Add(oTransactionLines[i].Dx3Code.Trim());
                            }
                        }

                        if (oTransactionLines[i].Dx4Ptr == true)
                        {
                            if (oTransactionLines[i].Dx4Code.Trim() != "")
                            {
                                oServiceInfo.DiagnosisList.Add(oTransactionLines[i].Dx4Code.Trim());
                            }
                        }

                        if (oTransactionLines[i].Mod1Code.Trim() != "") { oServiceInfo.ModifierList.Add(oTransactionLines[i].Mod1Code.Trim()); }
                        if (oTransactionLines[i].Mod2Code.Trim() != "") { oServiceInfo.ModifierList.Add(oTransactionLines[i].Mod2Code.Trim()); }


                        oClaimTransaction.Services.Add(oServiceInfo);

                        oServiceInfo = null;
                    }
                    SendClaimtoValidate(oClaimTransaction);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oServiceInfo != null) { oServiceInfo = null; }
                if (oClaimTransaction != null) { oClaimTransaction = null; }
                if (oTransactionLines != null) { oTransactionLines.Dispose(); }
            }
        }

        private void SendClaimtoValidate(ClsTransaction oClaimTransaction)
        {
            ClsClaimSubMittal oClaimSubMittal = default(ClsClaimSubMittal);
            string _FilePath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {

                if (oClaimTransaction != null)
                {
                    oClaimSubMittal = new ClsClaimSubMittal();
                    oClaimSubMittal.ChargesTransaction = oClaimTransaction;
                    // oClaimSubMittal.ApplicationPath = System.Windows.Forms.Application.StartupPath;
                    oClaimSubMittal.ApplicationPath = appSettings["StartupPath"].ToString();
                    oClaimSubMittal.PostXML();
                    System.Text.StringBuilder strSearchResponse = default(System.Text.StringBuilder);
                    strSearchResponse = new System.Text.StringBuilder();

                    System.Text.StringBuilder strValidationResponse = default(System.Text.StringBuilder);
                    strValidationResponse = new System.Text.StringBuilder();

                    if (oClaimSubMittal.oSearchResponseList.Count > 0)
                    {
                        foreach (ClsSearchResponse oSearchResponse in oClaimSubMittal.oSearchResponseList)
                        {
                            strSearchResponse.Append("Code: " + oSearchResponse.Code);
                            strSearchResponse.Append("Description: " + oSearchResponse.Description);
                            strSearchResponse.Append(Environment.NewLine);
                        }
                    }
                    if (oClaimSubMittal.oValidationResponseList.Count > 0)
                    {
                        int icnt = 1;
                        foreach (ClsValidationResponse oValidationResponse in oClaimSubMittal.oValidationResponseList)
                        {
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("Result " + icnt.ToString());
                            //strValidationResponse.Append("Code: " + oValidationResponse.Code);
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("MedicalNecessity: " + FormatString(oValidationResponse.MedicalNecessity));
                            //strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append(Environment.NewLine);
                            //strValidationResponse.Append("Modifiers: " + oValidationResponse.Modifiers);
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("CCI: " + FormatString(oValidationResponse.CCI));
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("Usage: " + FormatString(oValidationResponse.Usage));
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("----------------------------------------------------------------------");
                            strValidationResponse.Append(Environment.NewLine);
                            icnt++;
                        }
                    }

                    //MessageBox.Show(strSearchResponse.ToString() + Environment.NewLine + strValidationResponse.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);

                    if (strValidationResponse.ToString().Trim() != "")
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.ValidateClaim, "Claim Validated", 0, Convert.ToInt64(oClaimTransaction.TransactionID), 0, ActivityOutCome.Success);

                        _FilePath = _FilePath + "ClaimValidation.txt";
                        System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                        oStreamWriter.WriteLine(strValidationResponse.ToString());
                        oStreamWriter.Close();
                        oStreamWriter.Dispose();
                        System.Diagnostics.Process.Start(_FilePath);

                    }

                }
            }
            catch (ClsClaimSubmittalException)
            {
                MessageBox.Show("Connection to server failed. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (oClaimSubMittal != null) { oClaimSubMittal = null; }
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private string FormatString(string strResponse)
        {
            System.Text.RegularExpressions.Regex rxChar = new System.Text.RegularExpressions.Regex("[a-zA-Z]");
            System.Text.RegularExpressions.Regex rxNum = new System.Text.RegularExpressions.Regex("[0-9]");
            for (int i = strResponse.Length - 1; i > 0; i--)
            {
                if (rxChar.Match(strResponse[i].ToString()).Success == true && rxNum.Match(strResponse[i - 1].ToString()).Success == true)
                {
                    strResponse = strResponse.Insert(i, " ");
                }
                if (rxNum.Match(strResponse[i].ToString()).Success == true && rxChar.Match(strResponse[i - 1].ToString()).Success == true)
                {
                    strResponse = strResponse.Insert(i, " ");
                }
            }
            strResponse = strResponse.Replace("CPT ", "CPT");
            strResponse = strResponse.Replace("ICD ", Environment.NewLine + "ICD");
            strResponse = strResponse.Replace("MN ", "MN");
            strResponse = strResponse.Replace("CCI ", "CCI");
            
            return strResponse;
        }

        #endregion " YOST - Claim claim validation "

        #region " Public & Private Methods to Load & Get EMR Treatment "

        private void BindEMRExams()
        {
            IsEMRTreatmentBind = true;
           
            DataTable dtEMRExam = null;
            DataView _dvExam = null;
            try
            {
                c1PatientEMRExams.Visible = false;
                if (c1PatientEMRExams != null)
                {
                 //   c1PatientEMRExams.Clear();
                    c1PatientEMRExams.DataSource = null;
                }

                int nTotalExam = 0;

                dtEMRExam = gloCharges.GetEMRExams(gloCharges.GetEMRTreatmentSourceSetting());

                if (dtEMRExam != null)
                {
                    if (dtEMRExam.Rows.Count <= 0)
                    {
                       // if (_dvExam != null && _dvExam.Table != null) { _dvExam.AddNew(); }
                        c1PatientEMRExams.Rows.Count = 1;
                        c1PatientEMRExams.Rows.Fixed = 1;
                    }

                    _dvExam = dtEMRExam.Copy().DefaultView;
                    dtEMRExam.Dispose();
                    dtEMRExam = null;

                    c1PatientEMRExams.AutoResize = false;
                    c1PatientEMRExams.Redraw = false;
                    c1PatientEMRExams.DataSource = _dvExam;
                    c1PatientEMRExams.Redraw = true;
                    c1PatientEMRExams.AutoResize = true;
                    lblSearch.Text = c1PatientEMRExams.Cols["ExamName"].Name + " : ";
                    lblSearch.Tag = c1PatientEMRExams.Cols["ExamName"].Index;
                    nTotalExam = c1PatientEMRExams.Rows.Count-1;
                    #region " Show Hide Columns "

                    //EMRPatientId,nExamID,nVisitID,DOS,ExamName,EMRPatientCode,EMRPatientFN,EMRPatientMN,
                    //EMRPatientLN,EMRPatientSSN,EMRPatientDOB,nProviderID,ProviderName,ProviderFName,ProviderMName,
                    //ProviderLName
                    c1PatientEMRExams.Cols["bSelect"].Visible = false;
                    c1PatientEMRExams.Cols["EMRPatientId"].Visible = false;
                    c1PatientEMRExams.Cols["nExamID"].Visible = false;
                    c1PatientEMRExams.Cols["nVisitID"].Visible = false;
                    c1PatientEMRExams.Cols["DOS"].Visible = true;
                    c1PatientEMRExams.Cols["FinishDate"].Visible = true;
                    c1PatientEMRExams.Cols["ExamName"].Visible = true;
                    c1PatientEMRExams.Cols["TemplateName"].Visible = true;
                    c1PatientEMRExams.Cols["Code"].Visible = true;
                    c1PatientEMRExams.Cols["FirstName"].Visible = true;
                    c1PatientEMRExams.Cols["MN"].Visible = true;
                    c1PatientEMRExams.Cols["LastName"].Visible = true;
                    //c1PatientEMRExams.Cols["EMRPatientSSN"].Visible = false;
                    c1PatientEMRExams.Cols["DOB"].Visible = true;
                    c1PatientEMRExams.Cols["nProviderID"].Visible = false;
                    c1PatientEMRExams.Cols["ProviderName"].Visible = true;
                    c1PatientEMRExams.Cols["ProviderFName"].Visible = false;
                    c1PatientEMRExams.Cols["ProviderMName"].Visible = false;
                    c1PatientEMRExams.Cols["ProviderLName"].Visible = false;
                    c1PatientEMRExams.Cols["nTreatmentType"].Visible = false;
                    c1PatientEMRExams.Cols["Charge Source"].Visible = false;
                    c1PatientEMRExams.Cols["AccountNote"].Visible = true;

                    if (c1PatientEMRExams.Cols["dtDOS"] != null)
                    {
                        c1PatientEMRExams.Cols["dtDOS"].Visible = false;
                    }

                    if (c1PatientEMRExams.Cols["ndos"] != null)
                    {
                        c1PatientEMRExams.Cols["ndos"].Visible = false;
                    }
                    #endregion

                    #region " Set Columns Width "

                    int nWidth = 0;
                    nWidth = pnlEMRExams.Width;

                    c1PatientEMRExams.Cols["EMRPatientId"].Width = Convert.ToInt32(nWidth * 0.00);
                    c1PatientEMRExams.Cols["nExamID"].Width = Convert.ToInt32(nWidth * 0.00);
                    c1PatientEMRExams.Cols["nVisitID"].Width = Convert.ToInt32(nWidth * 0.00);
                    c1PatientEMRExams.Cols["DOS"].Width = Convert.ToInt32(nWidth * 0.08);
                    c1PatientEMRExams.Cols["FinishDate"].Width = Convert.ToInt32(nWidth * 0.08);
                    c1PatientEMRExams.Cols["ExamName"].Width = Convert.ToInt32(nWidth * 0.25);
                    c1PatientEMRExams.Cols["TemplateName"].Width = Convert.ToInt32(nWidth * 0.18);
                    c1PatientEMRExams.Cols["Code"].Width = Convert.ToInt32(nWidth * 0.05);
                    c1PatientEMRExams.Cols["FirstName"].Width = Convert.ToInt32(nWidth * 0.1);
                    c1PatientEMRExams.Cols["MN"].Width = Convert.ToInt32(nWidth * 0.05);
                    c1PatientEMRExams.Cols["LastName"].Width = Convert.ToInt32(nWidth * 0.1);
                    //c1PatientEMRExams.Cols["EMRPatientSSN"].Width = Convert.ToInt32(nWidth * 0.00);
                    c1PatientEMRExams.Cols["DOB"].Width = Convert.ToInt32(nWidth * 0.06);
                    c1PatientEMRExams.Cols["nProviderID"].Width = Convert.ToInt32(nWidth * 0.00);
                    c1PatientEMRExams.Cols["ProviderName"].Width = Convert.ToInt32(nWidth * 0.09);
                    c1PatientEMRExams.Cols["AccountNote"].Width = Convert.ToInt32(nWidth * 0.14);
                    c1PatientEMRExams.Cols["ProviderFName"].Width = Convert.ToInt32(nWidth * 0.00);
                    c1PatientEMRExams.Cols["ProviderMName"].Width = Convert.ToInt32(nWidth * 0.00);
                    c1PatientEMRExams.Cols["ProviderLName"].Width = Convert.ToInt32(nWidth * 0.00);
                    c1PatientEMRExams.Cols["SearchDOB"].Visible = false;
                    c1PatientEMRExams.Cols["SearchFinishDate"].Visible = false;
                    
                    #endregion " Set Columns Width "

                    c1PatientEMRExams.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                    c1PatientEMRExams.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                    c1PatientEMRExams.AutoResize = false;
                    c1PatientEMRExams.ExtendLastCol = false;

                    //Date Format & Sorting Mahesh Nawal
                    c1PatientEMRExams.Cols["DOS"].DataType = typeof(System.DateTime);
                    c1PatientEMRExams.Cols["DOS"].Format = "MM/dd/yyyy";

                    c1PatientEMRExams.Cols["DOB"].DataType = typeof(System.DateTime);
                    c1PatientEMRExams.Cols["DOB"].Format = "MM/dd/yyyy";

                    c1PatientEMRExams.Cols["FinishDate"].DataType = typeof(System.DateTime);
                    c1PatientEMRExams.Cols["FinishDate"].Format = "MM/dd/yyyy";



                }
                else
                {
                    lblSearch.Text = "ExamName : ";
                    lblSearch.Tag = null;
                    c1PatientEMRExams.Rows.Count = 0;
                    c1PatientEMRExams.Rows.Fixed = 0;
                    nTotalExam = 0;
                }
                lblExamCount.Text = "Total : " + Convert.ToString(nTotalExam);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                c1PatientEMRExams.Visible = true;
                if (c1PatientEMRExams != null && c1PatientEMRExams.ScrollBars == ScrollBars.None) { c1PatientEMRExams.ScrollBars = ScrollBars.Vertical; }

            }
        }
        private void c1PatientEMRExams_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location,false);
        }
        private void SetEMRTreatment(Int64 ExamID)
        {
            gloBilling ogloBilling;
            ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            TransactionLines oLines = null;


            try
            {
                string strUsedCodes = "";
                string strUsedLineNos = "";

                if (dtBilledCPTS != null)
                {
                    dtBilledCPTS.Dispose();
                    dtBilledCPTS = null;
                }
                dtBilledCPTS = ogloBilling.GetPostedCPTs(_EMRExamID,_nEMRTreatmentType);
                if (_dtNoPostCharges != null)
                {
                    _dtNoPostCharges.Dispose();
                    _dtNoPostCharges = null;
                }
                _dtNoPostCharges = ogloBilling.Get_NOPOST_CPTs(_EMRExamID, _nEMRTreatmentType);
               
                if (dtBilledCPTS != null && dtBilledCPTS.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtBilledCPTS.Rows.Count - 1; i++)
                    {
                        if (strUsedCodes == "")
                        {
                            strUsedCodes = Convert.ToString(dtBilledCPTS.Rows[i]["sCPTCodes"]);
                           
                        }
                        else
                        {
                            strUsedCodes = strUsedCodes + "," + Convert.ToString(dtBilledCPTS.Rows[i]["sCPTCodes"]);
                           
                        }
                        if (strUsedLineNos == "")
                        {
                           
                            strUsedLineNos = Convert.ToString(dtBilledCPTS.Rows[i]["nLineNo"]);
                        }
                        else
                        {
                          
                            strUsedLineNos = strUsedLineNos + "," + Convert.ToString(dtBilledCPTS.Rows[i]["nLineNo"]);
                        }
                    }
                }
                if (_dtNoPostCharges != null && _dtNoPostCharges.Rows.Count > 0)
                {
                    for (int i = 0; i <= _dtNoPostCharges.Rows.Count - 1; i++)
                    {
                        if (strUsedCodes == "")
                        {
                            strUsedCodes = Convert.ToString(_dtNoPostCharges.Rows[i]["sCPTCodes"]);
                           
                        }
                        else
                        {
                            strUsedCodes = strUsedCodes + "," + Convert.ToString(_dtNoPostCharges.Rows[i]["sCPTCodes"]);
                          
                        }
                        if (strUsedLineNos == "")
                        {
                           
                            strUsedLineNos = Convert.ToString(_dtNoPostCharges.Rows[i]["nLineNo"]);
                        }
                        else
                        {
                          
                            strUsedLineNos = strUsedLineNos + "," + Convert.ToString(_dtNoPostCharges.Rows[i]["nLineNo"]);
                        }
                    }
                }
                bool ISCPTEXIST = false;
                int nICDRevision;

                if (strUsedLineNos.Trim() != "")
                {

                    oLines = ogloBilling.GetEMRTreatment(_EMRExamID, _EMRVisitID, _NoOfMaxServiceLines, _nEMRTreatmentType, strUsedCodes, strUsedLineNos, true, ref ISCPTEXIST, out nICDRevision);
                   
                }
                else
                {
                    oLines = ogloBilling.GetEMRTreatment(_EMRExamID, _EMRVisitID, _NoOfMaxServiceLines, _nEMRTreatmentType, ref ISCPTEXIST, out nICDRevision);
                }

                if (nICDRevision.ToString() == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode().ToString())
                    rbICD10.Checked = true;
                else if (nICDRevision.ToString() == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode().ToString())
                    rbICD9.Checked = true;
                else
                {
                    if (c1Insurance.Rows.Count >= 2)
                    {
                        switch (GetICDCodeType(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)), UC_gloBillingTransactionLines.getfirstservicelineDos()))
                        {
                            case gloGlobal.gloICD.CodeRevision.ICD10:
                                rbICD10.Checked = true;
                                break;
                            case gloGlobal.gloICD.CodeRevision.ICD9:
                                rbICD9.Checked = true;
                                break;
                            default:
                                rbICD9.Checked = true;
                                break;
                        }


                    }
                } 

                if (ISCPTEXIST)
                {
                    tlb_NoPost.Enabled = false;
                }
                else
                {
                    tlb_NoPost.Enabled = true;
                }
                if (oLines != null && oLines.Count > 0)
                {
                    #region "Get Selected Insurance"

                    string _fillInsuranceName = "";
                    Int64 _fillInsuranceID = 0;
                    Int32 _fillInsSelfMode = 0;
                    string _fillInsType = "";
                    //Int64 _DefaultTOSId = 0;
                    //Int64 _DefaultPOSId = 0;
                    DateTime _fillServiceDate = DateTime.Now;
                    if (c1Insurance.Rows.Count > 0)
                    {
                        for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                            {
                                _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                                _fillInsType = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE));
                                break;
                            }
                        }
                    }

                    #endregion

                    if (_fillInsSelfMode == PayerMode.Insurance.GetHashCode())
                    {
                        for (int i = 0; i <= oLines.Count - 1; i++)
                        {
                            oLines[i].InsuranceID = _fillInsuranceID;
                            oLines[i].InsuranceName = _fillInsuranceName;
                            oLines[i].InsurancePrimarySecondaryTertiary = _fillInsType;
                            oLines[i].InsuranceSelfMode = PayerMode.Insurance;
                        }
                    }
                    if (_dtLoadedCPTS != null)
                    {
                        _dtLoadedCPTS.Dispose();
                        _dtLoadedCPTS = null;
                    }
                    _dtLoadedCPTS = new DataTable();
                    _dtLoadedCPTS.Columns.Add("nLineNo");
                    _dtLoadedCPTS.Columns.Add("sCPTCodes");

                    for (int i = 0; i <= oLines.Count - 1; i++)
                    {
                        DataRow _dr = _dtLoadedCPTS.NewRow();
                        _dr["nLineNo"] = Convert.ToInt32(oLines[i].EMRTreatmentLineNo);
                        _dr["sCPTCodes"] = Convert.ToString(oLines[i].CPTCode);
                        _dtLoadedCPTS.Rows.Add(_dr);
                        _dtLoadedCPTS.AcceptChanges();
                    }

                    //Resetting Diagonosis Grid.
                    c1Dx.Rows.Count = 1;
                    UC_gloBillingTransactionLines.AutoSort = false;
                    UC_gloBillingTransactionLines.SetEMRExamLineTransaction(oLines);

                    for (int lineIndex = 0; lineIndex < oLines.Count; lineIndex++)
                    {
                        if (oLines[lineIndex].DateServiceFrom < _fillServiceDate)
                        {
                            _fillServiceDate = oLines[lineIndex].DateServiceFrom;
                        }
                    }
                }
                //LoadDefaultBillingSettings();
                UC_gloBillingTransactionLines.ShowTotal();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.View, "Open EMR Exam to Setup Charges", 0, 0, 0, ActivityOutCome.Failure);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (oLines != null) { oLines.Dispose(); }
                if (!bisEMRTreatmentSplitEnabled)
                {
                    //tlb_NoPost.Visible = false;
                   
                }
            }

        }


        private void SetEMRTreatmentOnDXSave(Int64 ExamID)
        {
            gloBilling ogloBilling;
            ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            TransactionLines oLines = null;


            try
            {
                string strUsedCodes = "";
                string strUsedLineNos = "";

                string strLoadedCodes = "";
                string strLoadedLineNos = "";

              

                if (dtBilledCPTS != null && dtBilledCPTS.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtBilledCPTS.Rows.Count - 1; i++)
                    {
                        if (strUsedCodes == "")
                        {
                            strUsedCodes = Convert.ToString(dtBilledCPTS.Rows[i]["sCPTCodes"]);
                           
                        }
                        else
                        {
                            strUsedCodes = strUsedCodes + "," + Convert.ToString(dtBilledCPTS.Rows[i]["sCPTCodes"]);
                           
                        }
                        if (strUsedLineNos == "")
                        {
                           
                            strUsedLineNos = Convert.ToString(dtBilledCPTS.Rows[i]["nLineNo"]);
                        }
                        else
                        {
                           
                            strUsedLineNos = strUsedLineNos + "," + Convert.ToString(dtBilledCPTS.Rows[i]["nLineNo"]);
                        }
                    }
                }
                if (_dtNoPostCharges != null && _dtNoPostCharges.Rows.Count > 0)
                {
                    for (int i = 0; i <= _dtNoPostCharges.Rows.Count - 1; i++)
                    {
                        if (strUsedCodes == "")
                        {
                            strUsedCodes = Convert.ToString(_dtNoPostCharges.Rows[i]["sCPTCodes"]);
                            
                        }
                        else
                        {
                            strUsedCodes = strUsedCodes + "," + Convert.ToString(_dtNoPostCharges.Rows[i]["sCPTCodes"]);
                            
                        }

                        if (strUsedLineNos == "")
                        {
                         
                            strUsedLineNos = Convert.ToString(_dtNoPostCharges.Rows[i]["nLineNo"]);
                        }
                        else
                        {
                          
                            strUsedLineNos = strUsedLineNos + "," + Convert.ToString(_dtNoPostCharges.Rows[i]["nLineNo"]);
                        }
                    }
                }


                if (_dtLoadedCPTS != null && _dtLoadedCPTS.Rows.Count > 0)
                {
                    for (int i = 0; i <= _dtLoadedCPTS.Rows.Count - 1; i++)
                    {
                        if (strLoadedCodes == "")
                        {
                            strLoadedCodes = Convert.ToString(_dtLoadedCPTS.Rows[i]["sCPTCodes"]);
                          
                        }
                        else
                        {
                            strLoadedCodes = strLoadedCodes + "," + Convert.ToString(_dtLoadedCPTS.Rows[i]["sCPTCodes"]);
                        
                        }
                        if (strLoadedLineNos == "")
                        {
                         
                            strLoadedLineNos = Convert.ToString(_dtLoadedCPTS.Rows[i]["nLineNo"]);
                        }
                        else
                        {
                           
                            strLoadedLineNos = strLoadedLineNos + "," + Convert.ToString(_dtLoadedCPTS.Rows[i]["nLineNo"]);
                        }
                    }
                }
                else
                {
                    strLoadedCodes = "YYYYYY";
                    strLoadedLineNos = "-1";
                }


                bool ISCPTEXIST = false;
                int nICDRevision = 0;
                if (strLoadedLineNos.Trim() != "")
                {
                    oLines = ogloBilling.GetEMRTreatment(_EMRExamID, _EMRVisitID, _NoOfMaxServiceLines, _nEMRTreatmentType, strUsedCodes, strUsedLineNos, strLoadedCodes, strLoadedLineNos, ref ISCPTEXIST, out nICDRevision);
                }
                else if (strUsedCodes.Trim() != "")
                {

                    oLines = ogloBilling.GetEMRTreatment(_EMRExamID, _EMRVisitID, _NoOfMaxServiceLines, _nEMRTreatmentType, strUsedCodes, strUsedLineNos, false, ref ISCPTEXIST, out nICDRevision);

                }
                else
                {
                    oLines = ogloBilling.GetEMRTreatment(_EMRExamID, _EMRVisitID, _NoOfMaxServiceLines, _nEMRTreatmentType, ref ISCPTEXIST, out nICDRevision);
                }
                if (nICDRevision.ToString() == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode().ToString())
                    rbICD10.Checked = true;
                else  if (nICDRevision.ToString() == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode().ToString())
                    rbICD9.Checked = true;
                else
                {
                    if (c1Insurance.Rows.Count >= 2)
                    {
                        switch (GetICDCodeType(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)), UC_gloBillingTransactionLines.getfirstservicelineDos()))
                        {
                            case gloGlobal.gloICD.CodeRevision.ICD10:
                                rbICD10.Checked = true;
                                break;
                            case gloGlobal.gloICD.CodeRevision.ICD9:
                                rbICD9.Checked = true;
                                break;
                            default:
                                rbICD9.Checked = true;
                                break;
                        }


                    }
                }
                if (ISCPTEXIST)
                {
                    tlb_NoPost.Enabled = false;
                }
                else
                {
                    tlb_NoPost.Enabled = true;
                }
                if (oLines != null && oLines.Count > 0)
                {
                    #region "Get Selected Insurance"

                    string _fillInsuranceName = "";
                    Int64 _fillInsuranceID = 0;
                    Int32 _fillInsSelfMode = 0;
                    string _fillInsType = "";
                 
                    DateTime _fillServiceDate = DateTime.Now;
                    if (c1Insurance.Rows.Count > 0)
                    {
                        for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                            {
                                _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                                _fillInsType = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE));
                                break;
                            }
                        }
                    }

                    #endregion

                    if (_fillInsSelfMode == PayerMode.Insurance.GetHashCode())
                    {
                        for (int i = 0; i <= oLines.Count - 1; i++)
                        {
                            oLines[i].InsuranceID = _fillInsuranceID;
                            oLines[i].InsuranceName = _fillInsuranceName;
                            oLines[i].InsurancePrimarySecondaryTertiary = _fillInsType;
                            oLines[i].InsuranceSelfMode = PayerMode.Insurance;
                        }
                    }
                    if (_dtLoadedCPTS != null)
                    {
                        _dtLoadedCPTS.Dispose();
                        _dtLoadedCPTS = null;
                    }
                    _dtLoadedCPTS = new DataTable();
                    _dtLoadedCPTS.Columns.Add("nLineNo");
                    _dtLoadedCPTS.Columns.Add("sCPTCodes");

                    for (int i = 0; i <= oLines.Count - 1; i++)
                    {
                        DataRow _dr = _dtLoadedCPTS.NewRow();
                        _dr["nLineNo"] = Convert.ToInt32(oLines[i].EMRTreatmentLineNo);
                        _dr["sCPTCodes"] = Convert.ToString(oLines[i].CPTCode);
                        _dtLoadedCPTS.Rows.Add(_dr);
                        _dtLoadedCPTS.AcceptChanges();
                    }

                    //Resetting Diagonosis Grid.
                    c1Dx.Rows.Count = 1;
                    UC_gloBillingTransactionLines.AutoSort = false;

                    this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                    UC_gloBillingTransactionLines.SetEMRExamLineTransaction(oLines);

                    for (int lineIndex = 0; lineIndex < oLines.Count; lineIndex++)
                    {
                        if (oLines[lineIndex].DateServiceFrom < _fillServiceDate)
                        {
                            _fillServiceDate = oLines[lineIndex].DateServiceFrom;
                        }
                    }
                }
                //LoadDefaultBillingSettings();
                UC_gloBillingTransactionLines.ShowTotal();
                this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.View, "Open EMR Exam to Setup Charges", 0, 0, 0, ActivityOutCome.Failure);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (oLines != null) { oLines.Dispose(); }
                if (!bisEMRTreatmentSplitEnabled)
                {
                    tlb_NoPost.Visible = false;
                }
            }

        }

        private void LoadEMRTreatment_New(HitTestInfo hitInfo)
        {
            _bDxFlag = true;
            _bEMRTreatmentLoading = true;
            IsEMRTreatmentBind = false;
            string sMessage = string.Empty;
            gloBilling ogloBilling = null;

            try
            {
                ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);

                #region " Get selected Exam details "

                _EMRExamID = Convert.ToInt64(c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["nExamID"].Index));
                _EMRVisitID = Convert.ToInt64(c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["nVisitID"].Index));
                _EMRPatientId = Convert.ToInt64(c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["EMRPatientId"].Index));
                _EMRProviderId = Convert.ToInt64(c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["nProviderID"].Index));
                _IsICD9Driven = EMRExam.IsExamICDDriven();
                _nEMRTreatmentType = (gloSettings.ExternalChargesType)(c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["nTreatmentType"].Index));

                String _PatientCode = Convert.ToString(c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["code"].Index));
                String _PatientName = Convert.ToString(c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["FirstName"].Index)) + " " + Convert.ToString(c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["LastName"].Index));
                Object _oDOS = c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["DOS"].Index);
                String _Provider = Convert.ToString(c1PatientEMRExams.GetData(hitInfo.Row, c1PatientEMRExams.Cols["ProviderName"].Index));
                String _DOS = "";
                if (_oDOS != null && Convert.ToString(_oDOS).Trim() != String.Empty && Convert.ToString(_oDOS).Trim().Length >= 8)
                { _DOS = String.Format("{0:MM/dd/yyyy}", _oDOS); }
                else
                { _DOS = Convert.ToString(_oDOS); }

                #endregion " Get selected Exam details "

                //1. Check if the selected exam patient is same as the current patient on charges screen
                //   if not then fill the patient details else move next.

                if (_PatientID != _EMRPatientId)
                {
                    this._PatientID = _EMRPatientId;
                    txtPriorAuthorizationNo.Text = "";
                    txtPriorAuthorizationNo.Tag = "";
                    SetPatientChangeData(true);
                    if (gloGlobal.gloPMGlobal.ViewDocumentsOnCharges)
                    { clsSplit_PatientCharges.FillDocuments(this._PatientID, gloGlobal.gloPMGlobal.ClinicID); }
                }
                else
                {
                    //if (_EMRProviderId > 0 && (_EMRProviderId != _PatientPoviderID))
                    if (_EMRProviderId > 0)
                    {
                        DataTable dtProviderSettings = GetSettingsForExamProvider(_EMRProviderId);
                        if (dtProviderSettings != null && dtProviderSettings.Rows.Count > 0)
                        {
                            SetProviderSettings(dtProviderSettings);
                        }
                        if (dtProviderSettings != null)
                        {
                            dtProviderSettings.Dispose();
                            dtProviderSettings = null;
                        }
                    }
                    //else if (_EMRProviderId > 0)
                    //{
                    //    cmbBillingProvider.SelectedValue = _EMRProviderId;
                    //}
                    else
                    {
                        cmbBillingProvider.SelectedValue = PatientPoviderID;
                    }

                    //To Clear the Dx Grid in case of Blank Treatment 
                    c1Dx.Rows.Count = 1;
                }
               

                ShowHideControls(ShowHideType.LoadEMRTreatment);
                //**GetHoldMessage();
                SetHoldnMoreClaimDataMesseges();
                SetLastGlobalPeriods();
                CheckForEPSDTEnabled();
                IsAnesthesiaEnabled();
                ShowHideUB();
                if (UC_gloBillingTransactionLines != null)
                {
                    UC_gloBillingTransactionLines.PatientID = this.PatientID;
                    UC_gloBillingTransactionLines.PatientProviderID = _EMRProviderId;
                    LoadDefaultBillingSettings();
                    UC_gloBillingTransactionLines.ReinitilizeControl();
                    UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                    SetFacilitySettingsData();
                    UC_gloBillingTransactionLines.TreatmentType = _nEMRTreatmentType;
                }

                //if (UC_gloBillingTransactionLines.FacilityPOS > 0)
                //{
                //    if (UC_gloBillingTransactionLines != null)
                //    {

                //        UC_gloBillingTransactionLines.SetFacilityPOS();

                //    }
                //}

                #region " Set Treatment Dos in Case of CPT is Empty -- For Bug ID :0001679 "
                if (IsValidDate(_DOS) == true)
                {
                    if (UC_gloBillingTransactionLines != null)
                    {
                        if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                        {
                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(_DOS));
                        }
                    }
                }
                #endregion " Set Close Date to addedd Line "

                pnlEMRExams.SendToBack();
                panel6.Visible = true;

                SelectPrimaryInsurance();

                Int64 _ContactId = 0;

                if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                {
                    _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                }

                ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                //_Transaction.NoOfServiceLine = _NoOfMaxServiceLines;
                //_Transaction.NoOfDiagnosis = _NoOfMaxDiagnosis;
                UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;
                UC_gloBillingTransactionLines._nContactID = _ContactId;


                #region " Check EMR Treatment Present or Not "

                //sMessage = ValidateEMRExam();
                sMessage = gloCharges.ValidateEMRExam(_EMRExamID, _IsICD9Driven, _NoOfMaxDiagnosis, _NoOfMaxServiceLines, _nEMRTreatmentType);

                if (sMessage.Trim() != String.Empty)
                {
                    if (MessageBox.Show("EMR Treatment requires manual entry.                 " + Environment.NewLine + sMessage + Environment.NewLine + Environment.NewLine + "Treatment Details will be displayed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        SetEMRTreatment(_EMRExamID);

                        oEMRTransLinesSplit = UC_gloBillingTransactionLines.GetLineTransactions();

                        pnlExamCPTDX.SendToBack();
                        pnlExamCPTDX.Location = new System.Drawing.Point(320, 130);
                        if (_nEMRTreatmentType == gloSettings.ExternalChargesType.gloEMRTreatment)
                        {
                            if (_IsICD9Driven)
                            { LoadDXGrid(); }
                            else
                            { LoadCPTGrid(); }
                        }
                        else if (_nEMRTreatmentType == gloSettings.ExternalChargesType.HL7InboundCharges)
                        {
                            //LoadInboundDXGrid();
                            LoadCPTGrid();
                        }
                        pnlExamCPTDX.BringToFront();
                    }
                }
                else
                {
                    SetEMRTreatment(_EMRExamID);
                }
                #endregion

                if (UC_gloBillingTransactionLines != null)
                {
                    //Function Added By Debasish
                    //SetPrimaryDXInDiagonosisGrid();
                    this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);

                    #region " Set Line Primary Dx "

                    if (UC_gloBillingTransactionLines.HasPrimaryDx(UC_gloBillingTransactionLines.CurrentTransactionLine) == true)
                    {
                        int rowIndex = UC_gloBillingTransactionLines.CurrentTransactionLine;
                        string _primaryDxCode = UC_gloBillingTransactionLines.GetRowPrimaryDxCode(rowIndex);

                        if (_primaryDxCode != "")
                        {
                            if (c1Dx != null && c1Dx.Rows.Count > 0)
                            {
                                for (int i = 1; i <= c1Dx.Rows.Count - 1; i++)
                                {
                                    if (Convert.ToString(c1Dx.GetData(i, COL_DX_CODE)) != "")
                                    {
                                        if (Convert.ToString(c1Dx.GetData(i, COL_DX_CODE)).Trim() == _primaryDxCode.Trim())
                                        {
                                            c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Checked);
                                        }
                                        else
                                        {
                                            c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Unchecked);
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        //Uncheck all 
                        if (c1Dx != null && c1Dx.Rows.Count > 0)
                        {
                            for (int i = 1; i <= c1Dx.Rows.Count - 1; i++)
                            {
                                c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Unchecked);
                            }
                        }

                    }

                    #endregion " Set Line Primary Dx "

                    this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);

                    UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                    SetFacilitySettingsData();

                    //if (UC_gloBillingTransactionLines.Fee_ScheduleID != 0)
                    //{
                    //    cmbFeeSchedule.SelectedValue = UC_gloBillingTransactionLines.Fee_ScheduleID;

                    //}
                    //else
                    //{
                    //    cmbFeeSchedule.SelectedIndex = 0; 
                    //}

                    #region " Set Fee Schedule "

                    UC_gloBillingTransactionLines.FeeScheduleID = 0;
                    UC_gloBillingTransactionLines.Fee_ScheduleID = 0;
                    UC_gloBillingTransactionLines.SetFNFCharges();

                    if (UC_gloBillingTransactionLines.Fee_ScheduleID > 0)
                    {
                        _FeeScheduleID = UC_gloBillingTransactionLines.Fee_ScheduleID;
                        cmbFeeSchedule.SelectedValue = _FeeScheduleID;
                    }
                    else
                    {
                        // Solving Problem# -450 - when Fee shchedule is not present in the system then it gives exception
                        // Adding if condition to check whether fee schedule is present or not.
                        if (cmbFeeSchedule.Items.Count > 0)
                        {
                            //Bug #67038: 00000386 : Fee Schedules      
                            //Set Provider default fee schedule if it is not available then set clinic default fee schedule
                            _DefaultFeeScheduleID = gloCharges.GetProviderFeeScheduleID(_PatientPoviderID);
                            if (_DefaultFeeScheduleID == 0)
                            {
                                _DefaultFeeScheduleID = gloCharges.GetClinicFeeScheduleID();
                            }
                            if (_DefaultFeeScheduleID > 0)
                                cmbFeeSchedule.SelectedValue = _DefaultFeeScheduleID;
                            else
                                cmbFeeSchedule.SelectedIndex = 0;
                            _FeeScheduleID = Convert.ToInt64(cmbFeeSchedule.SelectedValue);
                            UC_gloBillingTransactionLines.FeeScheduleID = 0;
                            UC_gloBillingTransactionLines.Fee_ScheduleID = 0;
                        }
                    }


                    chkFeeSchedule.Checked = false;

                    #endregion

                    if (UC_gloBillingTransactionLines.FacilityPOS > 0)
                    {
                        if (UC_gloBillingTransactionLines != null)
                        {
                            UC_gloBillingTransactionLines.SetFacilityPOS();
                        }
                    }

                    this.lstAppointmentIDs.Clear();
                    this.ListOfPatientAppointments.Clear();
                    this.FillPatientAppointmentsOnLoad();
                }


                gloPMGeneral.gloAppointmentsChargesLinking.frmPatientAppointments frmPatientAppointment = null;
                frmPatientAppointment = new gloPMGeneral.gloAppointmentsChargesLinking.frmPatientAppointments();
                frmPatientAppointment._IsOnLoadform = true;
                DataSet dtPatientAppointment = gloCharges.GetMissingChargeAppointments(_EMRPatientId, 0,0);
                frmPatientAppointment.PatientAppointments = dtPatientAppointment;

                if (dtPatientAppointment.Tables.Count > 1)
                {
                    if (Convert.ToInt32(dtPatientAppointment.Tables[1].Rows[0][0]) == 1)
                    {
                        if (dtPatientAppointment.Tables[0].Rows.Count > 0)
                        {

                            frmPatientAppointment.ShowDialog(this);

                            if (frmPatientAppointment.AppointmentID != 0)
                            {
                                GetPatientMissingCharges(frmPatientAppointment);
                            }
                        }
                    }
                }
                if (dtPatientAppointment != null)
                {
                    dtPatientAppointment.Dispose();
                    dtPatientAppointment = null;
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                //tlb_CancelEMRTreatment.Visible = false;
                _bDxFlag = false;
                _bEMRTreatmentLoading = false;
                //IsDefaultFeeScheduleExpired();
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        #region "Not In Use Methods"

        private void LoadPatientExam(Int64 PatientID)
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            DataTable dtEMRExams = null;
            Int64 _emrPatientId = 0;

            try
            {
                if (PatientID > 0)
                {
                    _emrPatientId = ogloBilling.GetEMRPatientID(PatientID);

                    if (_emrPatientId >= 0)
                    {
                        dtEMRExams = ogloBilling.GetEMRExams(0); //_emrPatientId); Pass zero to load all exams

                        if (dtEMRExams != null)
                        {
                            #region " Design Exam Grid "

                            c1PatientEMRExams.Rows.Count = 1;
                            c1PatientEMRExams.Rows.Fixed = 1;
                            c1PatientEMRExams.Cols.Count = COL_EXAM_COUNT;
                            c1PatientEMRExams.Cols.Fixed = 1;

                            int rowIndex = 0;
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_SELECT, "");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PATIENTID, "EMRPatientId");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EXAMID, "ExamId");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_VISITID, "VisitId");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_DOS, "DOS");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_NAME, "Exam Name");

                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientCode, "Code");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientFN, "First Name");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientMN, "MI");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientLN, "Last Name");
                            //c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientSSN, "SSN");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientDOB, "DOB");

                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERID, "ProviderId");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERNAME, "Provider Name");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERFNAME, "ProviderFName");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERMNAME, "ProviderMName");
                            c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERLNAME, "ProviderLName");

                            //c1PatientEMRExams.Cols[COL_EXAM_SELECT].DataType = typeof(System.Boolean);

                            c1PatientEMRExams.Cols[COL_EXAM_SELECT].Visible = true;
                            c1PatientEMRExams.Cols[COL_EXAM_PATIENTID].Visible = false;
                            c1PatientEMRExams.Cols[COL_EXAM_EXAMID].Visible = false;
                            c1PatientEMRExams.Cols[COL_EXAM_VISITID].Visible = false;
                            c1PatientEMRExams.Cols[COL_EXAM_DOS].Visible = true;
                            c1PatientEMRExams.Cols[COL_EXAM_NAME].Visible = true;
                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientCode].Visible = true;
                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientFN].Visible = true;
                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientMN].Visible = true;
                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientLN].Visible = true;
                            //c1PatientEMRExams.Cols[COL_EXAM_EMRPatientSSN].Visible = false;
                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientDOB].Visible = false;
                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERID].Visible = false;
                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERNAME].Visible = true;
                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERFNAME].Visible = false;
                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERMNAME].Visible = false;
                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERLNAME].Visible = false;

                            int nWidth = 0;
                            nWidth = pnlEMRExams.Width;
                            c1PatientEMRExams.Cols[COL_EXAM_SELECT].Width = Convert.ToInt32(nWidth * 0.02);
                            c1PatientEMRExams.Cols[COL_EXAM_PATIENTID].Width = Convert.ToInt32(nWidth * 0.00);
                            c1PatientEMRExams.Cols[COL_EXAM_EXAMID].Width = Convert.ToInt32(nWidth * 0.00);
                            c1PatientEMRExams.Cols[COL_EXAM_VISITID].Width = Convert.ToInt32(nWidth * 0.00);
                            c1PatientEMRExams.Cols[COL_EXAM_DOS].Width = Convert.ToInt32(nWidth * 0.10);
                            c1PatientEMRExams.Cols[COL_EXAM_NAME].Width = Convert.ToInt32(nWidth * 0.3);

                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientCode].Width = Convert.ToInt32(nWidth * 0.15);
                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientFN].Width = Convert.ToInt32(nWidth * 0.1);
                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientMN].Width = Convert.ToInt32(nWidth * 0.05);
                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientLN].Width = Convert.ToInt32(nWidth * 0.1);
                            //c1PatientEMRExams.Cols[COL_EXAM_EMRPatientSSN].Width = Convert.ToInt32(nWidth * 0.0);
                            c1PatientEMRExams.Cols[COL_EXAM_EMRPatientDOB].Width = Convert.ToInt32(nWidth * 0.0);

                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERID].Width = Convert.ToInt32(nWidth * 0.00);
                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERNAME].Width = Convert.ToInt32(nWidth * 0.3);
                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERFNAME].Width = Convert.ToInt32(nWidth * 0.0);
                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERMNAME].Width = Convert.ToInt32(nWidth * 0.0);
                            c1PatientEMRExams.Cols[COL_EXAM_PROVIDERLNAME].Width = Convert.ToInt32(nWidth * 0.0);

                            c1PatientEMRExams.AutoResize = false;
                            c1PatientEMRExams.SelectionMode = SelectionModeEnum.Row;
                            c1PatientEMRExams.AllowEditing = false;

                            #endregion " Design Exam Grid "

                            for (int i = 0; i < dtEMRExams.Rows.Count; i++)
                            {
                                c1PatientEMRExams.Rows.Add();
                                rowIndex = c1PatientEMRExams.Rows.Count - 1;

                                //c1PatientEMRExams.SetCellCheck(rowIndex, COL_EXAM_SELECT,CheckEnum.Unchecked);
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PATIENTID, Convert.ToInt64(dtEMRExams.Rows[i]["EMRPatientId"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EXAMID, Convert.ToInt64(dtEMRExams.Rows[i]["nExamID"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_VISITID, Convert.ToInt64(dtEMRExams.Rows[i]["nVisitID"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_DOS, Convert.ToString(dtEMRExams.Rows[i]["DOS"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_NAME, Convert.ToString(dtEMRExams.Rows[i]["ExamName"]));

                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientCode, Convert.ToString(dtEMRExams.Rows[i]["EMRPatientCode"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientFN, Convert.ToString(dtEMRExams.Rows[i]["EMRPatientFN"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientMN, Convert.ToString(dtEMRExams.Rows[i]["EMRPatientMN"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientLN, Convert.ToString(dtEMRExams.Rows[i]["EMRPatientLN"]));
                                //c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientSSN, Convert.ToString(dtEMRExams.Rows[i]["EMRPatientSSN"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_EMRPatientDOB, Convert.ToString(dtEMRExams.Rows[i]["EMRPatientDOB"]));

                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERID, Convert.ToInt64(dtEMRExams.Rows[i]["nProviderID"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERNAME, Convert.ToString(dtEMRExams.Rows[i]["ProviderName"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERFNAME, Convert.ToString(dtEMRExams.Rows[i]["ProviderFName"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERMNAME, Convert.ToString(dtEMRExams.Rows[i]["ProviderMName"]));
                                c1PatientEMRExams.SetData(rowIndex, COL_EXAM_PROVIDERLNAME, Convert.ToString(dtEMRExams.Rows[i]["ProviderLName"]));
                            }
                            pnlEMRExams.BringToFront();

                            oPatientControl.Enabled = false;
                            UC_gloBillingTransactionLines.Enabled = false;

                            tls_btnAddLine.Visible = false;
                            tls_btnRemoveLine.Visible = false;
                            tlb_btnSmartTreatment.Visible = false;
                            tlb_AddNotes.Visible = false;
                            tls_btnOK.Visible = false;
                            tlb_LoadExam.Visible = false;
                            tls_btnCancel.Visible = false;
                            dtEMRExams.Dispose();
                            dtEMRExams = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Patient is not associated with EMR.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
        }

        private void GetEMRTreatment(Int64 PatientEMRId)
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            DataTable dtEMRTreatment = null;
            string CPTCode = "";
            string CPTDesc = "";
            string Dx1Code = "";
            string Dx1Desc = "";
            string MOD1Code = "";
            string MOD1Desc = "";
            Decimal Unit = 0;
            try
            {
                if (PatientEMRId > 0)
                {
                    dtEMRTreatment = ogloBilling.GetEMRTreatment(PatientEMRId);
                    if (dtEMRTreatment != null)
                    {

                        #region "Get Selected Insurance"

                        string _fillInsuranceName = "";
                        Int64 _fillInsuranceID = 0;
                        Int32 _fillInsSelfMode = 0;

                        if (c1Insurance.Rows.Count > 0)
                        {
                            for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                            {
                                if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                                {
                                    _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                    _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                    _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                                    break;
                                }
                            }
                        }


                        #endregion

                        for (int i = 0; i < dtEMRTreatment.Rows.Count; i++)
                        {
                            CPTCode = Convert.ToString(dtEMRTreatment.Rows[i]["sCPTCode"]);
                            CPTDesc = Convert.ToString(dtEMRTreatment.Rows[i]["sCPTDescription"]);
                            Dx1Code = Convert.ToString(dtEMRTreatment.Rows[i]["sICD9Code"]);
                            Dx1Desc = Convert.ToString(dtEMRTreatment.Rows[i]["sICD9Description"]);
                            MOD1Code = Convert.ToString(dtEMRTreatment.Rows[i]["sModCode"]);
                            MOD1Desc = Convert.ToString(dtEMRTreatment.Rows[i]["sModDescription"]);
                            Unit = Convert.ToDecimal(dtEMRTreatment.Rows[i]["nUnit"]);

                            UC_gloBillingTransactionLines.SetLineTransaction(CPTCode, CPTDesc, Dx1Code, Dx1Desc, MOD1Code, MOD1Desc, Unit, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                        }
                        dtEMRTreatment.Dispose();
                        dtEMRTreatment = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }

        }

        private void SetEMRTreatment(string examPrimaryDxCode, string examPrimaryDxDesc)
        {
            gloBilling ogloBilling;
            ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            int nICDRevision;
            TransactionLines oLines = null;


            try
            {
                bool ISCPTEXIST = false;
                oLines = ogloBilling.GetEMRTreatment(_EMRExamID, _EMRVisitID, _NoOfMaxServiceLines, _nEMRTreatmentType, ref ISCPTEXIST, out nICDRevision);
                if (nICDRevision.ToString() == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode().ToString())
                    rbICD10.Checked = true;
                else if (nICDRevision.ToString() == gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode().ToString())
                    rbICD9.Checked = true;
                else
                {
                    if (c1Insurance.Rows.Count >= 2)
                    {
                        switch (GetICDCodeType(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)), UC_gloBillingTransactionLines.getfirstservicelineDos()))
                        {
                            case gloGlobal.gloICD.CodeRevision.ICD10:
                                rbICD10.Checked = true;
                                break;
                            case gloGlobal.gloICD.CodeRevision.ICD9:
                                rbICD9.Checked = true;
                                break;
                            default:
                                rbICD9.Checked = true;
                                break;
                        }


                    }
                }
                UC_gloBillingTransactionLines._nContactID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)); 
                if (oLines != null && oLines.Count > 0)
                {
                    if (ISCPTEXIST)
                    {
                        tlb_NoPost.Enabled = false;
                    }
                    else{
                        tlb_NoPost.Enabled = true;
                }
                    #region "Get Selected Insurance"

                    string _fillInsuranceName = "";
                    Int64 _fillInsuranceID = 0;
                    Int32 _fillInsSelfMode = 0;
                    string _fillInsType = "";
                    //Int64 _DefaultTOSId = 0;
                    //Int64 _DefaultPOSId = 0;
                    DateTime _fillServiceDate = DateTime.Now;
                    if (c1Insurance.Rows.Count > 0)
                    {
                        for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                            {
                                _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                                _fillInsType = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE));
                                break;
                            }
                        }
                    }

                    #endregion

                    if (_fillInsSelfMode == PayerMode.Insurance.GetHashCode())
                    {
                        for (int i = 0; i <= oLines.Count - 1; i++)
                        {
                            oLines[i].InsuranceID = _fillInsuranceID;
                            oLines[i].InsuranceName = _fillInsuranceName;
                            oLines[i].InsurancePrimarySecondaryTertiary = _fillInsType;
                            oLines[i].InsuranceSelfMode = PayerMode.Insurance;

                            //if (oLines[i].DateServiceFrom < _fillServiceDate)
                            //{
                            //    _fillServiceDate = oLines[i].DateServiceFrom;
                            //}
                        }
                    }

                    //...20090825 Set Line primary diagnosis
                    for (int ln = 0; ln < oLines.Count; ln++)
                    {
                        if (examPrimaryDxCode.Trim() != "" && examPrimaryDxDesc.Trim() != "")
                        {
                            oLines[ln].LinePrimaryDxCode = examPrimaryDxCode;
                            oLines[ln].LinePrimaryDxDesc = examPrimaryDxDesc;
                        }
                    }


                    UC_gloBillingTransactionLines.SetEMRExamLineTransaction(oLines);

                    for (int lineIndex = 0; lineIndex < oLines.Count; lineIndex++)
                    {
                        if (oLines[lineIndex].DateServiceFrom < _fillServiceDate)
                        {
                            _fillServiceDate = oLines[lineIndex].DateServiceFrom;
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.View, "Open EMR Exam to Setup Charges", 0, 0, 0, ActivityOutCome.Failure);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (oLines != null) { oLines.Dispose(); }
                if (!bisEMRTreatmentSplitEnabled)
                {
                    //tlb_NoPost.Visible = false;
                }
            }

        }

        private void CreateCopyofClaim()
        {

            Transaction _Transaction = null;
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);

            try
            {
                #region " Retrieve the Charges Details in a Transaction Object "


                _Transaction = ogloBilling.GetChargesClaimDetailsForCopyClaim(_MasterTransactionID, _TransactionID, _ClinicID);


                FillProviderData();

                // _TransAccountID = _Transaction.PAccountID;
                this.PatientID = _Transaction.PatientID;

                LoadPatientStripForCopyClaim(_Transaction.PatientID, _Transaction.PAccountID, true);

                UC_gloBillingTransactionLines.PatientProviderID = oPatientControl.PatientProviderID;


                #endregion

                #region " Expanded Claim Settings "

                ogloBilling.GetExpandedClaimSetting(_Transaction.ContactID, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);

                _Transaction.NoOfServiceLine = _NoOfMaxServiceLines;
                _Transaction.NoOfDiagnosis = _NoOfMaxDiagnosis;
                UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;
                UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;


                #endregion

                #region " Bind the Data to the Form controls from the Transaction Object"

                if (_Transaction != null)
                {
                    _MasterTransactionID = Convert.ToInt64(_Transaction.TransactionMasterID);

                    sClaimNo = gloCharges.FormattedClaimNumberGeneration(Convert.ToString(gloCharges.GenerateClaimNumber()));
                    txtClaimNo.Text = sClaimNo;
                    txtClaimNo.Tag = _Transaction.ClaimNo;
                    cmbFacility.SelectedValue = Convert.ToString(_Transaction.FacilityCode);

                    ClaimEPSDT = _Transaction.ClaimEPSDT;
                    //Fill Cases
                    if (Convert.ToInt64(_Transaction.CaseID) != 0)
                    {
                        this.PatientHasCases = true;
                        txtCases.Text = _Transaction.CaseName;
                        txtCases.Tag = _Transaction.CaseID;
                    }

                    CheckForPatientCases();
                    //**

                    if (cmbFacility.SelectedIndex != -1)
                    {
                        UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                        SetFacilitySettingsData();
                    }

                    if (_Transaction.nICDRevision == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                    {
                        rbICD10.Checked = true;
                    }
                    else
                    {
                        rbICD9.Checked = true;
                    }
                    cmbBillingProvider.SelectedValue = _Transaction.ProviderID;
                    cmbClaimCategory.SelectedValue = _Transaction.nClaimCategoryID;
                    txtPriorAuthorizationNo.Tag = Convert.ToInt64(_Transaction.PriorAuthorizationID);
                    txtPriorAuthorizationNo.Text = Convert.ToString(_Transaction.PriorAuthorizationNo);
                    if (_Transaction.ClaimBox15Date != null)
                    { mskBox15Date.Text = Convert.ToDateTime( _Transaction.ClaimBox15Date).ToString("MM/dd/yyyy"); }
                    else { mskBox15Date.Text = ""; }
                    if (_Transaction.ClaimBox15QualifierCode != null && Convert.ToString(_Transaction.ClaimBox15QualifierCode).Trim() != "")
                    { cmbBox15DateQualifier.SelectedValue = _Transaction.ClaimBox15QualifierCode; }
                    //else
                    //{ cmbBox15DateQualifier.SelectedValue = ""; }
                    if (_Transaction.ClaimBox14QualifierCode != null && Convert.ToString(_Transaction.ClaimBox14QualifierCode).Trim() != "")
                    { cmbBox14DateQualifier.SelectedValue = _Transaction.ClaimBox14QualifierCode; }


                    if (_Transaction.ProviderQualifierCode != null && Convert.ToString(_Transaction.ProviderQualifierCode).Trim() != "")
                    { cmbProviderType.SelectedValue = _Transaction.ProviderQualifierCode; }
                    
                     
                    


                    if (_Transaction.OnsiteDate > 0)
                    {
                        mskOnsiteDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.OnsiteDate).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        mskOnsiteDate.Text = "";
                    }



                    #region " Worker Comp,Auto Claim "

                    if (_Transaction.WorkersComp == true)
                    {
                        //_WorkerCompType = "WorkersComp";

                        CmbAccidentType.Text = "Work";
                        CmbAccidentType.SelectedIndex = (int)AccidentType.Work;
                        cmbClaimNo.SelectedIndex = cmbClaimNo.FindStringExact(_Transaction.WorkersCompNo);
                        _WorkerCompNo = Convert.ToString(_Transaction.WorkersCompNo);

                        if (_Transaction.InjuryDate > 0)
                        {
                            mskInjuryDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.InjuryDate).ToString("MM/dd/yyyy");
                            _WorkerInjuryDate = gloDateMaster.gloDate.DateAsDate(_Transaction.InjuryDate).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            mskInjuryDate.Text = "";
                            _WorkerInjuryDate = "";
                        }
                    }
                    if (_Transaction.AutoClaim == true)
                    {
                        //_WorkerCompType = "AutoClaim";
                        CmbAccidentType.Text = "Auto";
                        CmbAccidentType.SelectedIndex = (int)AccidentType.Auto;
                        _AutoClaimNo = Convert.ToString(_Transaction.WorkersCompNo);
                        if (_Transaction.AccidentDate > 0)
                        {
                            mskAccidentDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.AccidentDate).ToString("MM/dd/yyyy");
                            _AccidentDate = gloDateMaster.gloDate.DateAsDate(_Transaction.AccidentDate).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            mskAccidentDate.Text = "";
                            _AccidentDate = "";
                        }
                    }
                    if (_Transaction.OtherAccident == true)
                    {
                        //_WorkerCompType = "OtherAccident";
                        CmbAccidentType.Text = "Other";
                        CmbAccidentType.SelectedIndex = (int)AccidentType.Other;
                        _OtherAccidentNo = Convert.ToString(_Transaction.WorkersCompNo);
                        if (_Transaction.OtherAccidentDate > 0)
                        {
                            mskOtherDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.OtherAccidentDate).ToString("MM/dd/yyyy");
                            _OtherDate = gloDateMaster.gloDate.DateAsDate(_Transaction.OtherAccidentDate).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            mskOtherDate.Text = "";
                            _OtherDate = "";

                        }
                    }

                    txt_WcAc.Text = Convert.ToString(_Transaction.WorkersCompNo);


                    #endregion


                    #region "Other Dates "

                    if (_Transaction.UnableToWorkFromDate > 0)
                    {
                        //mskUnableFromDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.UnableToWorkFromDate).ToString("MM/dd/yyyy");
                        _UnableToWorkFromDate_MoreClaimData = _Transaction.UnableToWorkFromDate;
                    }
                    else
                    {
                        mskUnableFromDate.Text = "";
                        _UnableToWorkFromDate_MoreClaimData = 0;
                    }

                    if (_Transaction.UnableToWorkTillDate > 0)
                    {
                        //mskUnableTillDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.UnableToWorkTillDate).ToString("MM/dd/yyyy");
                        _UnableToWorkTillDate_MoreClaimData = _Transaction.UnableToWorkTillDate;
                    }
                    else
                    {
                        mskUnableTillDate.Text = "";
                        _UnableToWorkTillDate_MoreClaimData = 0;
                    }

                    if (_Transaction.HospitalizationDateFrom > 0)
                    {
                        mskHospitaliztionFrom.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.HospitalizationDateFrom).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        mskHospitaliztionFrom.Text = "";
                    }

                    if (_Transaction.HospitalizationDateTo > 0)
                    {
                        mskHospitaliztionTo.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.HospitalizationDateTo).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        mskHospitaliztionTo.Text = "";
                    }


                    if (_Transaction.dtInitTreatmentDate != null)
                    {
                        mskInitTreatment.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(_Transaction.dtInitTreatmentDate));
                    }
                    else
                    {
                        mskInitTreatment.Text = "";
                    }


                    if (bShowInitialTreatmentDate)
                    {
                        if (CmbAccidentType.SelectedIndex == -1)
                        {
                            lblInitDate.Visible = true;
                            mskInitTreatment.Visible = true;
                        }
                        else
                        {
                            lblInitDate.Visible = false;
                            mskInitTreatment.Visible = false;
                        }
                    }

                    txtClaimCLIAno.Text = Convert.ToString(_Transaction.CLIANumber);
                    _MammogramCertNumber = Convert.ToString(_Transaction.MammogramCertNumber);
                    _IDENo = Convert.ToString(_Transaction.IDENo);
                    #endregion

                    switch (CmbAccidentType.Text.Trim())
                    {
                        case "Work":

                          
                            lblInjuryDate.Visible = true;
                            lblOnsiteDate.Visible = false;
                            lblOtherDate.Visible = false;
                            lblAccidentDate.Visible = false;

                            mskInjuryDate.Visible = true;
                            mskOnsiteDate.Visible = false;
                            mskOtherDate.Visible = false;
                            mskAccidentDate.Visible = false;

                            lbl_State.Visible = false;
                            cmbState.Visible = false;
                            cmbState.Enabled = false;
                            txt_WcAc.Visible = true;
                            lblClaim.Visible = true;

                            lblClaim.Text = "Workers Comp # :";
                            txt_WcAc.Focus();

                            break;
                        case "Auto":
                            //SLR: Changed on 2/4/2014
                            for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                            {
                                pnlInternalControl.Controls.RemoveAt(i);
                            }

                         
                            lblInjuryDate.Visible = false;
                            lblOnsiteDate.Visible = false;
                            lblOtherDate.Visible = false;
                            lblAccidentDate.Visible = true;

                            mskInjuryDate.Visible = false;
                            mskOnsiteDate.Visible = false;
                            mskOtherDate.Visible = false;
                            mskAccidentDate.Visible = true;

                          

                            cmbState.Visible = true;
                            lbl_State.Visible = true;
                            cmbState.Enabled = true;
                            cmbState.Focus();

                            txt_WcAc.Visible = true;
                            lblClaim.Visible = true;
                            lblClaim.Text = "Workers Comp # :";
                            cmbClaimNo.Visible = false;
                            lblClaim.Text = "Auto Claim # :";

                            txt_WcAc.Focus();

                            break;
                        case "Other":

                        
                            lblInjuryDate.Visible = false;
                            lblOnsiteDate.Visible = false;
                            lblOtherDate.Visible = true;
                            lblAccidentDate.Visible = false;
                            mskInjuryDate.Visible = false;
                            mskOnsiteDate.Visible = false;
                            mskOtherDate.Visible = true;
                            mskAccidentDate.Visible = false;

                           
                            txt_WcAc.Visible = false;
                            lbl_State.Visible = false;
                            cmbState.Visible = false;
                            cmbState.Enabled = false;
                            cmbClaimNo.Visible = false;
                            lblClaim.Visible = false;

                            break;
                        default:

                        
                            lblInjuryDate.Visible = false;
                            lblOnsiteDate.Visible = true;
                            lblOtherDate.Visible = false;
                            lblAccidentDate.Visible = false;

                            mskInjuryDate.Visible = false;
                            mskOnsiteDate.Visible = true;
                            mskOtherDate.Visible = false;
                            mskAccidentDate.Visible = false;

                          
                            txt_WcAc.Visible = false;
                            lbl_State.Visible = false;
                            cmbState.Visible = false;
                            cmbState.Enabled = false;
                            cmbClaimNo.Visible = false;
                            lblClaim.Visible = false;

                            // if (bShowInitialTreatmentDate) bShowInitialTreatmentDate setting is removed from 8.0.0.0 version
                           // {
                                lblInitDate.Visible = false;
                                mskInitTreatment.Visible = false;
                           // }

                            break;
                    }


                    chkOutSideLab.Checked = _Transaction.OutSideLab;
                    UC_gloBillingTransactionLines._nContactID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                    if (_Transaction.OutSideLabCharges > 0)
                    {
                        txtOutSideLabCharges.Text = _Transaction.OutSideLabCharges.ToString();
                    }

                    if (_Transaction.State != "")
                    {
                        int index = cmbState.FindStringExact(_Transaction.State);
                        if (index >= 0)
                        { cmbState.SelectedIndex = index; }
                    }


                    #region " Referral Provider "
                    if (_Transaction.IsSameAsBillingProvider)
                    {
                        chk_SameasBillingProvider.Checked = true;
                    }
                    else
                    {
                        chk_SameasBillingProvider.Checked = false;
                    }


                    if (_Transaction.ReferalProviderID_New > 0)
                    {
                        cmbReferralProvider.SelectedValue = _Transaction.ReferalProviderID_New;
                        _InitialReferalProviderId = _Transaction.ReferalProviderID_New;
                    }
                    else
                    {
                        cmbReferralProvider.SelectedValue = 0;
                    }

                    if (cmbReferralProvider.SelectedIndex == -1)
                    {
                        AddCurrentSavedReferal(_Transaction.ReferalProviderID_New);
                    }

                    #endregion

                    txtClaimLastStatus.Text = Convert.ToString(_Transaction.Transaction_Status.GetHashCode());
                    txtLastStatusId.Text = Convert.ToString(_Transaction.LastStatusId);
                    txtSendCounter.Text = Convert.ToString(_Transaction.SendCounter);
                    txtSendToRejection.Text = Convert.ToString(_Transaction.SendToRejection);

                    SelectedChargeTrayID = Convert.ToInt64(_Transaction.CloseDayTrayID);
                    SelectedChargeTray = Convert.ToString(_Transaction.CloseDayTrayName);
                    lblCloseDayTray.Text = _selectedChargeTrayDescription;

                    if (_IsOpenForResend == false)
                    {
                        this._IsResendToInsType = _Transaction.SendToInsuranceFlag;
                    }


                    #region " Fee-Schedule "

                    if (_Transaction.FeeScheduleType == FeeScheduleType.UserSelected)
                    {
                        chkFeeSchedule.Checked = true;
                    }

                    if (_Transaction.FeeScheduleID > 0)
                    {
                        cmbFeeSchedule.SelectedValue = _Transaction.FeeScheduleID;
                        UC_gloBillingTransactionLines.FeeScheduleID = _Transaction.FeeScheduleID;
                    }

                    if (_Transaction.FacilityType == FacilityType.Facility)
                    {
                        chkFacilityFeeSchedule.Checked = true;
                        chkNonFacilityCharges.Checked = false;
                        UC_gloBillingTransactionLines.DefaultChargesType = FacilityType.Facility;
                    }
                    else if (_Transaction.FacilityType == FacilityType.NonFacility)
                    {
                        chkFacilityFeeSchedule.Checked = false;
                        chkNonFacilityCharges.Checked = true;
                        UC_gloBillingTransactionLines.DefaultChargesType = FacilityType.NonFacility;
                    }



                    if (UC_gloBillingTransactionLines.Fee_ScheduleID > 0)
                    {
                        _FeeScheduleID = UC_gloBillingTransactionLines.Fee_ScheduleID;
                    }

                    #endregion " Fee-Schedule "


                    if (_Transaction.Lines != null)
                    {
                        if (_Transaction.Lines.Count > 0)
                        {
                            UC_gloBillingTransactionLines.SetLineTransaction(_Transaction.Lines);
                        }
                    }

                    if (UC_gloBillingTransactionLines != null && _IsFormLoading == false)
                    {
                        UC_gloBillingTransactionLines.SetFNFCharges();

                    }

                    SetDxList(_Transaction.TransactionMasterID, _Transaction.VisitID, _Transaction.ClaimNo, _Transaction.PatientID, _Transaction.ClinicID);


                    #region " Insurance Plan "

                    if (_Transaction.InsurancePlans != null)
                    {
                        c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);


                        bool _IsFound = false;
                        for (int i = 0; i < _Transaction.InsurancePlans.Count; i++)
                        {
                            _IsFound = false;
                            for (int j = 1; j < c1Insurance.Rows.Count; j++)
                            {
                                if (Convert.ToInt64(c1Insurance.GetData(j, COL_INSURANCEID)) == _Transaction.InsurancePlans[i].InsuranceID && Convert.ToInt64(c1Insurance.GetData(j, COL_INSURANCECONTACTID)) == _Transaction.InsurancePlans[i].ContactID)
                                {
                                    _IsFound = true;
                                    c1Insurance.SetData(j, COL_INSURANCEPARTY, Convert.ToString(_Transaction.InsurancePlans[i].ResponsibilityNo));
                                    c1Insurance.SetData(j, COL_INSURANCERESPONSIBILITY, Convert.ToString(_Transaction.InsurancePlans[i].ResponsibilityNo));
                                    break;
                                }

                            }

                            #region "Add Insurance Plan if Inactivate"

                            if (_IsFound == false)
                            {
                                c1Insurance.Rows.Add();
                                int rowIndex = c1Insurance.Rows.Count - 1;
                                c1Insurance.SetData(rowIndex, COL_SELECT, false);//Select-CheckBox
                                //Select-CheckBox

                                c1Insurance.SetData(rowIndex, COL_INSURANCEID, _Transaction.InsurancePlans[i].InsuranceID); //=                                
                                c1Insurance.SetData(rowIndex, COL_INSSELFMODE, _Transaction.InsurancePlans[i].ResponsibilityType); //
                                if (_Transaction.InsurancePlans[i].ResponsibilityType == PayerMode.Self.GetHashCode())
                                { c1Insurance.SetData(rowIndex, COL_INSURANCENAME, PayerMode.Self.ToString()); }
                                else
                                { c1Insurance.SetData(rowIndex, COL_INSURANCENAME, _Transaction.InsurancePlans[i].InsuranceName); }
                               
                                if (_Transaction.InsurancePlans[i].ResponsibilityType != PayerMode.BadDebt.GetHashCode())
                                {
                                    c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, "Inactive");
                                }
                                else
                                {
                                    string collectionAgency = "";
                                    collectionAgency = ogloBilling.GetCollectionAgencyname(_Transaction.InsurancePlans[i].ContactID);
                                    c1Insurance.SetData(rowIndex, COL_INSURANCENAME, collectionAgency);
                                }

                                c1Insurance.SetData(rowIndex, COL_INSURANCERESPONSIBILITY, Convert.ToString(_Transaction.InsurancePlans[i].ResponsibilityNo));
                                c1Insurance.SetData(rowIndex, COL_INSURANCEPARTY, Convert.ToString(_Transaction.InsurancePlans[i].ResponsibilityNo));
                                c1Insurance.SetData(rowIndex, COL_INSURANCECOPAYAMT, _Transaction.InsurancePlans[i].CopayAmount);
                                c1Insurance.SetData(rowIndex, COL_INSURANCEWORKERCOMP, _Transaction.InsurancePlans[i].IsWorkerComp);
                                c1Insurance.SetData(rowIndex, COL_INSURANCECONTACTID, _Transaction.InsurancePlans[i].ContactID); //
                            }

                            #endregion "Add Insurance Plan if Inactivate"

                        }
                        c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                        ReorderInsurance();
                    }


                    //To Remove the Previous Flag
                    for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                    }

                    if (c1Insurance.Rows.Count > 0)
                    {
                        System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                        c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, imgFlag);
                        c1Insurance.SetCellCheck(1, COL_INSURANCECURRRESP, CheckEnum.Checked);
                    }

                    if (ReplacementClaimCreationType == global::gloBilling.ReplacementClaimCreationType.Replacement)
                    {
                        _sClaimRefNo = _Transaction.sClaimRefNo;
                        _IsbCliamReplacement = _Transaction.IsReplacementClaim;
                    }
                    else
                    {
                        _sClaimRefNo = "";
                        _IsbCliamReplacement = false;
                    }

                    #endregion " Insurance Plan "

                    #region "Claim Hold"

                    if (_Transaction.Hold != null)
                    {
                        _oClaimHold = _Transaction.Hold;
                    }
                    else
                    {
                        if (_oClaimHold != null)
                        {
                            _oClaimHold.Dispose();
                            _oClaimHold = null;
                        }
                        _oClaimHold = new ClaimHold();
                    }

                    #endregion

                    #region " More Claim Data "

                    if (_Transaction.ClaimBox19Note != null)
                    {
                        _oBox19Note = _Transaction.ClaimBox19Note;
                        _oBox19Notes.Add(_oBox19Note);
                    }
                    else
                    {
                        if (_oBox19Note != null)
                        {
                            _oBox19Note.Dispose();
                            _oBox19Note = null;
                        }
                        _oBox19Note = new ClaimBox19Note();
                    }

                    if (_Transaction.ClaimBox10dNote != null)
                    {
                        _sClaimBox10dNote = _Transaction.ClaimBox10dNote;
                    }
                    else
                    {
                        _sClaimBox10dNote = string.Empty;
                    }

                    _IllnessDate = _Transaction.IllnessDate;
                    _LastSeenDate = _Transaction.LastSeenDate;
                    _UnableToWorkFromDate_MoreClaimData = _Transaction.UnableToWorkFromDate;
                    _UnableToWorkTillDate_MoreClaimData = _Transaction.UnableToWorkTillDate;
                    _sDelayReasonCode = _Transaction.DelayReasonCodeID;
                    _sServiceAuthExceptionCode = _Transaction.ServiceAuthExceCode;
                    bIsRefProvAsSupervisor = _Transaction.bIsRefprovAsSupervisor;
                    _sMedicaidResubmissionCode = _Transaction.MedicaidResubmissioncode;
                    _bIsRefprovAsSupervisor = _Transaction.bIsRefprovAsSupervisor;
                    #endregion

                }

                #endregion

                #region " Set the Renderring Provider "

                //****************************************************************************
                //Set the Patient Provider as the Renderring Provider
                //If Patient Provider not present set the selected Billing Provider from combo
                //as the Renderring Provider
                //****************************************************************************

                if (this.PatientPoviderID > 0)
                {
                    UC_gloBillingTransactionLines.DefaultRenderingProviderID = this.PatientPoviderID;
                    UC_gloBillingTransactionLines.DefaultRenderringProviderName = this.PatientProviderName;
                }
                else
                {
                    if (cmbBillingProvider != null && cmbBillingProvider.Items.Count > 0)
                    {
                        if (cmbBillingProvider.SelectedIndex != -1)
                        {
                            UC_gloBillingTransactionLines.DefaultRenderingProviderID = Convert.ToInt64(cmbBillingProvider.SelectedValue);
                            UC_gloBillingTransactionLines.DefaultRenderringProviderName = Convert.ToString(cmbBillingProvider.Text);
                        }
                    }
                }

                #endregion " Set the Renderring Provider "

                #region " Set Line Primary Dx "

                this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);

                if (UC_gloBillingTransactionLines.HasPrimaryDx(UC_gloBillingTransactionLines.CurrentTransactionLine) == true)
                {
                    int rowIndex = UC_gloBillingTransactionLines.CurrentTransactionLine;
                    string _primaryDxCode = UC_gloBillingTransactionLines.GetRowPrimaryDxCode(rowIndex);

                    if (_primaryDxCode != "")
                    {
                        if (c1Dx != null && c1Dx.Rows.Count > 0)
                        {
                            for (int i = 1; i <= c1Dx.Rows.Count - 1; i++)
                            {
                                if (Convert.ToString(c1Dx.GetData(i, COL_DX_CODE)) != "")
                                {
                                    if (Convert.ToString(c1Dx.GetData(i, COL_DX_CODE)).Trim() == _primaryDxCode.Trim())
                                    {
                                        c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Unchecked);
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    //Uncheck all 
                    if (c1Dx != null && c1Dx.Rows.Count > 0)
                    {
                        for (int i = 1; i <= c1Dx.Rows.Count - 1; i++)
                        {
                            c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Unchecked);
                        }
                    }

                }

                this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);

                #endregion " Set Line Primary Dx "

                ChangeInsuranceGridColor();

                if (IsUBEnabled == true && (gloCharges.GetBillingType(_TransactionID, _MasterTransactionID, bIsCopiedClaim) == Convert.ToInt16(BillingType.Institutional)))
                {
                    tls_btnUB04Data.Visible = true;

                }
                else
                {
                    tls_btnUB04Data.Visible = false;
                }

                gloCharges.FillUB04Data(_MasterTransactionID, _TransactionID, ref oUB);


                if (cmbFacility.Enabled)
                {
                    cmbFacility.DrawMode = DrawMode.OwnerDrawFixed;
                }
                else
                {
                    cmbFacility.DrawMode = DrawMode.Normal;
                }

                if (cmbReferralProvider.Enabled)
                {
                    cmbReferralProvider.DrawMode = DrawMode.OwnerDrawFixed;
                }
                else
                {
                    cmbReferralProvider.DrawMode = DrawMode.Normal;
                }

                if (cmbClaimCategory.Visible && cmbClaimCategory.Enabled)
                {
                    cmbClaimCategory.DrawMode = DrawMode.OwnerDrawFixed;
                }
                else
                {
                    cmbClaimCategory.DrawMode = DrawMode.Normal;
                }

                if (bShowInitialTreatmentDate && CmbAccidentType.SelectedIndex == -1)
                {
                    lblInitDate.Visible = false;
                    mskInitTreatment.Visible = false;
                }
                else
                {
                    lblInitDate.Visible = false;
                    mskInitTreatment.Visible = false;
                }

                CheckForEPSDTEnabled();
                IsAnesthesiaEnabled();

            }
            catch (Exception exCopyClaim)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exCopyClaim.ToString(), true);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (_Transaction != null) { _Transaction.Dispose(); }
            }

        }

        #endregion

        private void SetDxList(Int64 transactionId, Int64 visitId, Int64 claimno, Int64 patientId, Int64 clinicId)
        {
            DataTable dtDxList = null;
            TransactionLines oTransactionLines = null;
            _arrDxCodes = new ArrayList();
            _arrValidDxCodes = new ArrayList();

            try
            {
                _isDxListLoading = true;

                dtDxList = gloCharges.GetTransactionDxList(transactionId, visitId, claimno, patientId, clinicId);

                if (dtDxList != null && dtDxList.Rows.Count > 0)
                {
                    //Get all the Transaction Lines in a Object 
                    oTransactionLines = UC_gloBillingTransactionLines.GetLineTransactions();

                    for (int dxListCounter = 0; dxListCounter < dtDxList.Rows.Count; dxListCounter++)
                    {
                        if (oTransactionLines != null && oTransactionLines.Count > 0)
                        {
                            for (int i = 0; i <= oTransactionLines.Count - 1; i++)
                            {
                                if (oTransactionLines[i].Dx1Code.Trim() != "" || oTransactionLines[i].Dx2Code.Trim() != "" || oTransactionLines[i].Dx3Code.Trim() != "" || oTransactionLines[i].Dx4Code.Trim() != "")
                                {
                                    //Filter the Dx from the TransactionLines According to the selected claim and bind it to the Dx Grid
                                    //If the TranasctionLines has and the Datatable has the same Dx then Bind it to the Dx Grid
                                    if (oTransactionLines[i].Dx1Code.Trim().ToString() == Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]).Trim() || oTransactionLines[i].Dx2Code.Trim().ToString() == Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]).Trim() || oTransactionLines[i].Dx3Code.Trim().ToString() == Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]).Trim() || oTransactionLines[i].Dx4Code.Trim().ToString() == Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]).Trim())
                                    {


                                        if (_arrValidDxCodes.Contains(Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]).Trim()))
                                        {
                                        }
                                        else
                                        {
                                            c1Dx.Rows.Add();
                                            int rowIndex = c1Dx.Rows.Count - 1;
                                            c1Dx.SetData(rowIndex, COL_DX_CODE, Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]));
                                            c1Dx.SetData(rowIndex, COL_DX_DESC, Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Description"]));
                                            if (Convert.ToBoolean(dtDxList.Rows[dxListCounter]["bIsClaimDx"]) == true)
                                            { c1Dx.SetCellCheck(rowIndex, COL_DX_SELECT, CheckEnum.Checked); }
                                            else
                                            { c1Dx.SetCellCheck(rowIndex, COL_DX_SELECT, CheckEnum.Unchecked); }

                                            c1Dx.SetCellCheck(rowIndex, COL_DX_ISPRIMARY, CheckEnum.Unchecked);

                                            _arrValidDxCodes.Add(Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]).Trim());
                                        }

                                    }
                                    else
                                    {
                                        _arrDxCodes.Add(Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]).Trim());
                                    }
                                }
                            }
                        }

                    }
                    for (int dxListCounter = 0; dxListCounter < dtDxList.Rows.Count; dxListCounter++)
                    {
                        if (Convert.ToBoolean(dtDxList.Rows[dxListCounter]["bIsClaimDx"]) == true)
                        {
                            if (_arrValidDxCodes.Contains(Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]).Trim()))
                            {
                                _arrValidDxCodes.Remove(Convert.ToString(dtDxList.Rows[dxListCounter]["sDx1Code"]).Trim());
                            }
                        }

                    }


                }

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                _isDxListLoading = false;
                if ( oTransactionLines != null)
                {
                    oTransactionLines.Dispose();
                    oTransactionLines = null;
                }
                if (dtDxList != null)
                {
                    dtDxList.Dispose();
                    dtDxList = null;
                }
                _arrDxCodes.Clear();
                _arrValidDxCodes.Clear();
            }
        }


        private void AddCurrentSavedReferal(Int64 nRefProviderID)
        {
            try
            {
                DataTable dtReferral = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                oDB.Connect(false);
                string _sqlquery = " SELECT ISNULL(Contacts_MST.sFirstName,'')+' '+ISNULL(Contacts_MST.sMiddleName,'') +' '+ ISNULL(Contacts_MST.sLastName,'')AS sName, " +
                                  " nContactID from Contacts_Mst WITH (NOLOCK) where nContactID=" + nRefProviderID + " ";
                oDB.Retrive_Query(_sqlquery, out dtReferral);
                if (dtReferral != null && dtReferral.Rows.Count > 0)
                {
                    DataTable dtRow = (DataTable)cmbReferralProvider.DataSource;
                    DataRow rw = dtRow.NewRow();
                    rw["nReferralID"] = dtReferral.Rows[0]["nContactID"];
                    rw["sReferralName"] = dtReferral.Rows[0]["sName"];
                    dtRow.Rows.Add(rw);
                    cmbReferralProvider.SelectedValue = nRefProviderID;
                }
                if (dtReferral != null)
                {
                    dtReferral.Dispose();
                    dtReferral = null;
                }
                oDB.Disconnect();
                oDB.Dispose();
            }

            catch //(Exception ex)
            { }

            finally
            {
            }
        }


        #endregion " Public & Private Methods to Load & Get EMR Treatment "

        #region " Form Drop-Down List selection change operation "

        private void SetBillingProviderDropDownSelectionChangeData()
        {
            //.. Consider following things when a billing provider is changed in the drop-down list
            //..1 - Uncheck the Same As Billing Provider checkbox

            chk_SameasBillingProvider.Checked = false;

            //..Following need to be verified so commented for time being
            //..2 - Read the selected billing provider settings (made in admin for Rendering Provider,Facility etc..)
            //..3 - Set the settings to billing control accordingly
            //LoadDefaultBillingSettings();
        }

        private void SetFacilityDropDownSelectionChangeData()
        {
            //.. Consider following things when a facility is changed in the facility drop-down list
            //..1 - Set the newly selected facility id to the billing control
            //..2 - Set the selected facility CLIA number,POS & Facility Type (facility/non-facility) default values 
            //..3 - Once the facility default settings are done, set this settings to the billing control


            if (UC_gloBillingTransactionLines != null)
            {
                if (cmbFacility.SelectedIndex >= 0)
                {
                    UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                    SetFacilitySettingsData();
                    if (!bModifyPOS)
                    {
                        UC_gloBillingTransactionLines.SetFacilityPOS();
                    }
                    UC_gloBillingTransactionLines.SetFacilityCLIA();
                    UC_gloBillingTransactionLines.SetFacilityMammogramNo();
                    this._MammogramCertNumber = UC_gloBillingTransactionLines.sMammogramCertNo;

                  
                }
            }


        }

        #endregion " Form Drop-Down List selection change operation "

        #region " TVP Charge Save Method "

        private bool SaveClaimData(bool bIsSaveClose=false)
        {
            #region "Check License"
            string tempProviderID = "";
            if (gloGlobal.gloPMGlobal.LoginProviderID != 0) { tempProviderID = ""; } else { tempProviderID = Convert.ToString(oPatientControl.PatientProviderID); }
            if (base.SetChildFormModules("SaveClaimData", "Create charges", tempProviderID) == true)
            {
                return false;
            }
            #endregion "" 
            dsChargesTVP odsChargesTVP = new dsChargesTVP();
            Int64 _returnID = 0;
            bool _result = false;
            bool IsModified = false;
            if (_dsLastClaimDetails != null)
            {
                _dsLastClaimDetails.Dispose();
                _dsLastClaimDetails = null;
            }
            _dsLastClaimDetails = new DataSet();
            try
            {
                //..1 - Validation for patient account
                if (!IsValidatePatientAccount(this.nPAccountID, this.PatientID)) { return false; }


                //..2 - Set the close date to billing control (may be we can eliminate this code need to check)
                //mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskClaimDate.Text != "")
                { UC_gloBillingTransactionLines.ColseDate = mskClaimDate.Text; }
                else
                { UC_gloBillingTransactionLines.ColseDate = ""; }
                mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

                UC_gloBillingTransactionLines._nCurrentContactID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));

                //..3 - Check for validations & setup the TVP's for save

                #region " Save Transaction "

                if (UC_gloBillingTransactionLines != null)
                {
                    if (ValidateMasterTransaction())
                    {
                        UC_gloBillingTransactionLines.IsPlanOrAdminEPSDTEnabled = EPSDTEnabled;
                        if (UC_gloBillingTransactionLines.ValidateTransaction())
                        {
                            if (IsDxSelected())
                            {
                                if (UC_gloBillingTransactionLines.ValidateDxList(GetSelectedDx(), false))
                                {
                                    DataTable _dtCPTLists = UC_gloBillingTransactionLines.GetCPTList();

                                    //Method for Checking Existing Active Patient Global Periods in System
                                    if (CheckforExistingPatientGlobalPeriods(_dtCPTLists))
                                    {
                                        //Skip the global Periods For Self claim
                                        if (c1Insurance.GetData(1, COL_INSURANCEID) != null && Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCEID)) > 0)
                                        {
                                            //Method for Checking Charge line CPT's are set as Global Period Triggers to 'Yes' or 'No'
                                            CheckforGlobalPeriodTriggeringCPTs(_dtCPTLists);
                                        }

                                        ShowHideControls(ShowHideType.WaitStart);
                                        SetMasterClaimDetails_TVP(odsChargesTVP);
                                        SetServiceLineDetails_TVP(odsChargesTVP);
                                        SetDXClaimDetails_TVP(odsChargesTVP);
                                        SetBox19Notes_TVP(odsChargesTVP);
                                        SetClaimBox10dNotes_TVP(odsChargesTVP);
                                        if (tls_btnUB04Data.Visible == true && oUB != null)
                                        {
                                            SetUB04Data_TVP(odsChargesTVP);
                                        }
                                        
                                        string sPOSTEDLineNos = "";
                                        string sPOSTEDCPTs = "";
                                        if (_dtLoadedCPTS != null && _dtLoadedCPTS.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in _dtLoadedCPTS.Rows)
                                            {

                                                if (sPOSTEDLineNos == "")
                                                {
                                                    if (!sPOSTEDLineNos.Contains(Convert.ToString(dr["nLineno"])))
                                                    {
                                                        sPOSTEDLineNos = Convert.ToString(dr["nLineno"]);
                                                    }
                                                    if (!sPOSTEDLineNos.Contains(Convert.ToString(dr["sCPTCodes"])))
                                                    {
                                                        sPOSTEDCPTs = Convert.ToString(dr["sCPTCodes"]);
                                                    }
                                                }
                                                else
                                                {
                                                    if (!sPOSTEDLineNos.Contains(Convert.ToString(dr["nLineno"])))
                                                    {
                                                        sPOSTEDLineNos = sPOSTEDLineNos + "," + Convert.ToString(dr["nLineno"]);
                                                    }
                                                    if (!sPOSTEDLineNos.Contains(Convert.ToString(dr["sCPTCodes"])))
                                                    {
                                                        sPOSTEDCPTs = sPOSTEDCPTs + "," + Convert.ToString(dr["sCPTCodes"]);
                                                    }
                                                }

                                            }
                                        }
                                        //..Check for data in TVP's and proceed to database save
                                        if (odsChargesTVP != null && odsChargesTVP.BL_Transaction_Claim_MST.Rows.Count > 0
                                            && odsChargesTVP.BL_Transaction_Claim_Lines.Rows.Count > 0)
                                        {
                                            if (odsChargesTVP.BL_Transaction_Claim_Lines != null && Convert.ToBoolean(GetSetting("EnableEstablishedNonEstablishedPatientWarning"))==true)
                                            {
                                                var sClaimCPTCodes = from claim in odsChargesTVP.BL_Transaction_Claim_Lines
                                                                     select claim.sCPTCode;
                                                string sClaimCPTCode = string.Empty;
                                                foreach (var item in sClaimCPTCodes)
                                                {
                                                    if (sClaimCPTCode == "")
                                                    {
                                                        sClaimCPTCode = item;
                                                    }
                                                    else
                                                    {
                                                        sClaimCPTCode += "," + item;
                                                    }
                                                }
                                                string sPatientStatus = gloCharges.CheckPatientStatus(_PatientID, sClaimCPTCode);
                                                if (!string.IsNullOrEmpty(sPatientStatus))
                                                {
                                                    if (MessageBox.Show(sPatientStatus, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                                    {
                                                        return false;
                                                    }
                                                }
                                               
                                            }
                                            Int64 nTransactionDetailID = 0;
                                            Int64 nTransactionMSTDetailID = 0;
                                            decimal dTotalAvailableCopayReserve = 0;
                                            //List<Tuple<long, long>> myList = new List<Tuple<long, long>>();
                                            //myList.Add(new Tuple<long, long>(11222, 56666));
                                            //myList.Add(new Tuple<long, long>(11223, 56667));
                                            //myList.Add(new Tuple<long, long>(11224, 56668));
                                            DataSet dsAutoDistributeCopayDOSWise = null;

                                            DateTime _dtCloseDate;
                                            if (mskClaimDate.Text != string.Empty)
                                            {
                                                mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                _dtCloseDate = Convert.ToDateTime(mskClaimDate.Text);
                                            }
                                            else
                                            {
                                                _dtCloseDate = DateTime.Now;
                                            }

                                            string Msg = "";
                                            Msg = gloGlobal.gloPMGlobal.getCPTDeativatedCPT(_dtCPTActivationDates);
                                            if (Msg != "")
                                            {
                                                if (MessageBox.Show(Msg, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                                                {
                                                    return false;
                                                }

                                            }
                                            DialogResult dgWarning = DialogResult.No;
                                            bool bIsMammogramCPTPresent = gloCharges.IsMammogramCPTPresentOnClaim(odsChargesTVP.Tables[1]);
                                            if(this._MammogramCertNumber!="")
                                            {
                                                if(bIsMammogramCPTPresent==false)
                                                {
                                                    this._MammogramCertNumber="";
                                                    odsChargesTVP.Tables[0].Rows[0]["sMammogramCertNumber"] = "";
                                                    odsChargesTVP.Tables[0].AcceptChanges();
                                                }
                                            }
                                            else
                                            {
                                                if(bIsMammogramCPTPresent==true)
                                                {
                                                    dgWarning=MessageBox.Show("Missing Mammogram Certification number\nBilling mammogram procedure requires reporting of mammogram certification number on claim. Missing mammogram certification number may cause rejection.\nDo you want to continue? \n\nYes: Save claim without Mammogram Certification Number.\n No: Add Mammogram Certification Number in More Claim Data.", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                                    if(dgWarning==DialogResult.No)
                                                    {
                                                        return false;
                                                    }
                                                }
                                            }
                                            //dTotalAvailableCopayReserve = gloCharges.getPatientTotalCoPayReserve(_PatientID, nPAccountID, _dtCloseDate);                                           
                                            //if (dTotalAvailableCopayReserve > 0)
                                            //{
                                            //    //string sortOrder = "nTransactionLineNo ASC";
                                            //    //DataRow[] _Row = odsChargesTVP.BL_Transaction_Claim_Lines.Select("dTotal>=" + dTotalAvailableCopayReserve + " AND sCPTCode like '99%'", sortOrder);
                                            //    //if (_Row.Length == 0)
                                            //    //{
                                            //    //    _Row = odsChargesTVP.BL_Transaction_Claim_Lines.Select("dTotal>=" + dTotalAvailableCopayReserve + "", sortOrder);
                                            //    //}

                                            //    DataTable _dt = null;
                                                //_dt = gloCharges.getCPTToDistributeCopay(odsChargesTVP, dTotalAvailableCopayReserve);

                                            //    if (_dt != null && _dt.Rows.Count > 0)
                                            //    {
                                            //        nTransactionMSTDetailID = Convert.ToInt64(_dt.Rows[0]["nTransactionMasterDetailID"]);
                                            //        nTransactionDetailID = Convert.ToInt64(_dt.Rows[0]["nTransactionDetailID"]);
                                            //    }
                                            //    else
                                            //    {
                                            //        MessageBox.Show("Copay Reserves are available but could not be applied to this claim.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //    }
                                            //}
                                            #region "code moved to master validation function"
                                            //if (odsChargesTVP.BL_Transaction_Claim_Lines.Rows.Count > 0)
                                            //{
                                            //    List<string> sCPTCodes = new List<string>();
                                            //    for (int i = 0; i < odsChargesTVP.BL_Transaction_Claim_Lines.Rows.Count; i++)
                                            //    {
                                            //        sCPTCodes.Add(Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sCPTCode"]));
                                            //    }

                                            //    List<string> sDuplicateCPTs = sCPTCodes.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                                            //    if (sDuplicateCPTs.Count > 0)
                                            //    {
                                            //        if ((MessageBox.Show("Procedure: Duplicate procedure code entered \n Stop to review?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)) == DialogResult.Yes)
                                            //        {
                                            //            return false;
                                            //        }
                                            //    }
                                            //    sCPTCodes = null;
                                            //    sDuplicateCPTs = null;

                                            //    for (int i = 0; i < odsChargesTVP.BL_Transaction_Claim_Lines.Rows.Count; i++)
                                            //    {
                                            //        List<string> sDxCodes = new List<string>();
                                            //        sDxCodes.Add(Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sDx1Code"]) != "" ? Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sDx1Code"]) : "1");
                                            //        sDxCodes.Add(Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sDx2Code"]) != "" ? Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sDx2Code"]) : "2");
                                            //        sDxCodes.Add(Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sDx3Code"]) != "" ? Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sDx3Code"]) : "3");
                                            //        sDxCodes.Add(Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sDx4Code"]) != "" ? Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sDx4Code"]) : "4");
                                            //        string sCurrentCPT = Convert.ToString(odsChargesTVP.BL_Transaction_Claim_Lines.Rows[i]["sCPTCode"]);
                                            //        List<string> sDuplicateDxCode = sDxCodes.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                                            //        if (sDuplicateDxCode.Count > 0)
                                            //        {
                                            //            MessageBox.Show("Diagnosis: Same diagnosis entered twice for the procedure code : \"" + sCurrentCPT + "\".\n Stop to Review. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            //            return false;
                                            //        }
                                            //        sDxCodes = null;
                                            //        sDuplicateDxCode = null;
                                            //    }
                                            //}
                                            #endregion
                                            dsAutoDistributeCopayDOSWise = gloCharges.AutoDistributeCopayDOSWise(_PatientID, nPAccountID, odsChargesTVP);
                                            if (dsAutoDistributeCopayDOSWise != null)
                                            {
                                                if (dsAutoDistributeCopayDOSWise.Tables.Count == 2)
                                                {
                                                    if (dsAutoDistributeCopayDOSWise.Tables[0].Rows.Count > 0 && dsAutoDistributeCopayDOSWise.Tables[1].Rows.Count <= 0)
                                                    {
                                                        MessageBox.Show("Copay Reserves are available but could not be applied to this claim.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                }
                                            }
                                            DataTable dtAppointmentChargesLink = null;
                                            if (this.lstAppointmentIDs.Any())
                                            {
                                                dtAppointmentChargesLink = new DataTable();
                                                dtAppointmentChargesLink.Columns.Add("nTransactionMasterID");
                                                dtAppointmentChargesLink.Columns.Add("nAppointmentID");

                                                foreach (long nAppointmentID in this.lstAppointmentIDs)
                                                {
                                                    DataRow row = dtAppointmentChargesLink.Rows.Add();

                                                    row["nTransactionMasterID"] = _TransactionID;
                                                    row["nAppointmentID"] = nAppointmentID;

                                                    row = null;
                                                }

                                               // gloCharges.SaveAppointmentsChargesLink(_TransactionID, dtAppointmentChargesLink);

                                                //dtAppointmentChargesLink.Dispose();
                                                //dtAppointmentChargesLink = null;

                                                //this.SetPatientAppointmentsAvailable();

                                                //this.lstAppointmentIDs.Clear();
                                            }

                                            _returnID = gloCharges.SaveClaim(odsChargesTVP, _EMRExamID, _EMRVisitID, out _nextClaimNo, out TransactionClaimID, sbEMRTreatmentLineNos.ToString(), sPOSTEDCPTs, sPOSTEDLineNos, _dtNoPostCharges, bIsSaveClose, dtAppointmentChargesLink);
                                            _TransactionID = _returnID;

                                            if(dgWarning==DialogResult.Yes)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing,gloAuditTrail.ActivityCategory.SetupCharges,gloAuditTrail.ActivityType.Add,"User hit Yes on Mammogram certification warning while creating charge.", PatientID, TransactionClaimID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                            }
                                        
                                            if (triggeredRuleInfoGlobal != null && triggeredRuleInfoGlobal.Count > 0)
                                            {
                                                DataTable dtRule = new DataTable();

                                                dtRule.Columns.Add("nRuleID");
                                                dtRule.Columns.Add("nTransactionMasterID");
                                                dtRule.Columns.Add("nTransactionID");
                                                dtRule.Columns.Add("nInsuranceID");
                                                dtRule.Columns.Add("nContactID");
                                                dtRule.Columns.Add("nUserID");
                                                dtRule.Columns.Add("nRuleType");

                                                foreach (gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo oRule in triggeredRuleInfoGlobal)
                                                {
                                                    
                                                    dtRule.Rows.Add();
                                                    dtRule.Rows[dtRule.Rows.Count-1]["nRuleID"] = oRule.RuleId;
                                                    dtRule.Rows[dtRule.Rows.Count - 1]["nTransactionMasterID"] = _TransactionID;
                                                    dtRule.Rows[dtRule.Rows.Count - 1]["nTransactionID"] = TransactionClaimID;
                                                    dtRule.Rows[dtRule.Rows.Count - 1]["nInsuranceID"] = _ResponsibleInsuranceID;
                                                    dtRule.Rows[dtRule.Rows.Count - 1]["nContactID"] = _ResponsibleInsuranceContactID;
                                                    dtRule.Rows[dtRule.Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                                    dtRule.Rows[dtRule.Rows.Count - 1]["nRuleType"] = oRule.RuleTypeInfo.GetHashCode();
                                                   

                                                }
                                                gloCharges.SaveTriggeredRules(dtRule, false);
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleEvaluation, gloAuditTrail.ActivityType.SaveWithErrors, "Claim saved with errors", 0, TransactionClaimID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                                            }

                                            if (this.lstAppointmentIDs.Any())
                                            {
                                            //    DataTable dtAppointmentChargesLink = new DataTable();
                                            //    dtAppointmentChargesLink.Columns.Add("nTransactionMasterID");
                                            //    dtAppointmentChargesLink.Columns.Add("nAppointmentID");

                                            //    foreach (long nAppointmentID in this.lstAppointmentIDs)
                                            //    {
                                            //        DataRow row = dtAppointmentChargesLink.Rows.Add();

                                            //        row["nTransactionMasterID"] = _TransactionID;
                                            //        row["nAppointmentID"] = nAppointmentID;

                                            //        row = null;
                                            //    }

                                            //    gloCharges.SaveAppointmentsChargesLink(_TransactionID, dtAppointmentChargesLink);
                                                
                                            //    dtAppointmentChargesLink.Dispose();
                                            //    dtAppointmentChargesLink = null;

                                                this.SetPatientAppointmentsAvailable();

                                                this.lstAppointmentIDs.Clear();
                                            }

                                            if (_TransactionID > 0)
                                            {
                                                //Update Case Diagnoses with most recent claim diagnosis if Update flag is checked
                                                if (_dtCaseData != null && _dtCaseData.Rows.Count > 0)
                                                {
                                                    if (rbICD10.Checked)
                                                    {
                                                        gloCharges.UpdateCaseDiagnoses(Convert.ToInt64(_dtCaseData.Rows[0]["nCaseID"]),gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode());
                                                    }
                                                    else
                                                    {
                                                        gloCharges.UpdateCaseDiagnoses(Convert.ToInt64(_dtCaseData.Rows[0]["nCaseID"]),gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode());
                                                    }
                                                }
                                                //**

                                                if (ReplacementClaimCreationType != global::gloBilling.ReplacementClaimCreationType.Copy)
                                                {
                                                    gloCharges.VoidAndReplaceClaim(nReplacementByTransMasterID, nReplacingTransMasterID, _TransactionID, bIsCopiedClaim);
                                                }

                                                #region "Code to Split the Claim if Charge line is more than 6 and Claim is Paper claim"

                                                if (UC_gloBillingTransactionLines.GetLinesCountNew()  > 6)
                                                {
                                                    if (gloCharges.PerformSplitOperation(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID))))
                                                    {
                                                        DialogResult res = MessageBox.Show("Split this paper claim? ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                                        if (res == DialogResult.Yes)
                                                        {
                                                            gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                                                            frmClaimExp objExpclaim = new frmClaimExp(new Transaction(), TransactionClaimID, gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                                            objExpclaim.ShowInTaskbar = false;
                                                            objExpclaim.Autosplit = true;
                                                            objExpclaim.ShowDialog(this);
                                                            ClaimStructure _ClaimStructure;
                                                            _ClaimStructure = objExpclaim._ClaimStructure;
                                                            IsModified = objExpclaim._isModified;

                                                          

                                                            if (_ClaimStructure != null)
                                                            {
                                                                if (_ClaimStructure.CSClaims.Count > 0)
                                                                {
                                                                    Transaction objTransaction = ogloBilling.GetChargesClaimDetails(TransactionClaimID, gloGlobal.gloPMGlobal.ClinicID);
                                                                    _ClaimStructure.Transaction = objTransaction;
                                                                    _ClaimStructure.SplitClaim(false);
                                                                    objTransaction.Dispose();
                                                                }

                                                            }
                                                            objExpclaim.Dispose();
                                                            objExpclaim = null;
                                                            ogloBilling.Dispose();
                                                            ogloBilling = null;
                                                        }
                                                    }
                                                }

                                                #endregion


                                                #region "Account Log"

                                                SAVE_PA_Account_log(odsChargesTVP);

                                                #endregion "Account Log"
                                                if (_PortalClaimID>0)
                                                {
                                                    Int64 nPostedAsTransactionID = 0;
                                                    string sPostedAsTransactionDetailedID = string.Empty;
                                                    nPostedAsTransactionID = _TransactionID;
                                                    bool bIsUpdated = gloCharges.UpdateOCPClaimDetails(_PortalClaimID, nPostedAsTransactionID, 0);
                                                    if (bIsUpdated == true)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupCharges, gloAuditTrail.ActivityType.Add, "Online PortalClaimId: " + _PortalClaimID.ToString() + "Saved Successfully.", _PatientID, TransactionClaimID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                                    }
                                                    else
                                                    {
                                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupCharges, gloAuditTrail.ActivityType.Add, "Online PortalClaimId: " + _PortalClaimID.ToString() + "Saved Unsuccessfully.", _PatientID, TransactionClaimID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, false);
                                                    }
                                                    bool bIsSaveRCMDOC = MoveOCPClaimAttachmentToRCMDOC(_PortalClaimID);
                                                }
                                                
                                                
                                                _result = true;
                                                //bIsSaveCharges = true;
                                                _dtNoPostCharges = null;
                                                bIsFullyPosted = true;
                                            }
                                            Int64 nDos = 0;
                                            if (dsAutoDistributeCopayDOSWise!=null)
                                            {
                                               if(dsAutoDistributeCopayDOSWise.Tables.Count==2)
                                               {
                                                   if (dsAutoDistributeCopayDOSWise.Tables[1].Rows.Count > 0)
                                                  
                                                   {
                                                          for (int i = 0; i < dsAutoDistributeCopayDOSWise.Tables[1].Rows.Count; i++)
                                                           {
                                                               nTransactionMSTDetailID = Convert.ToInt64(dsAutoDistributeCopayDOSWise.Tables[1].Rows[i]["nTransactionMasterDetailID"]);
                                                               nTransactionDetailID = Convert.ToInt64(dsAutoDistributeCopayDOSWise.Tables[1].Rows[i]["nTransactionDetailID"]);
                                                               dTotalAvailableCopayReserve = Convert.ToDecimal(dsAutoDistributeCopayDOSWise.Tables[1].Rows[i]["dAvailiableReserves"]);
                                                               nDos = Convert.ToInt64(dsAutoDistributeCopayDOSWise.Tables[1].Rows[i]["nFromDate"]);
                                                               if (_TransactionID != 0 && nTransactionMSTDetailID != 0 && nTransactionDetailID != 0 && dTotalAvailableCopayReserve != 0)
                                                               {
                                                                   Int64 _nCreaditID = 0;
                                                                   _nCreaditID = gloCharges.AutoDistributeCopay(_TransactionID, nTransactionMSTDetailID, nTransactionDetailID, _PatientID, nPAccountID, nGuarantorID, nAccountPatientID, dTotalAvailableCopayReserve, _dtCloseDate, this.UserID, this.UserName,gloDateMaster.gloDate.DateAsDate(nDos));
                                                               }
                                                           }
                                                   }
                                               }
                                            }
                                        }
                                        else
                                        { MessageBox.Show("Enter charge(s) information to save.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                    }
                                    if (_dtCPTLists != null)
                                    {
                                        _dtCPTLists.Dispose();
                                        _dtCPTLists = null;
                                    }
                                }
                                else
                                {
                                    #region "..If Dx not set,then set focus on the billing control charge line.."

                                    c1Dx.Focus();
                                    if (c1Dx.Rows.Count >= 2)
                                    { c1Dx.Select(1, COL_DX_SELECT); }
                                    _result = false;

                                    #endregion "..If Dx not set,then set focus on the billing control charge line.."
                                }
                            }
                            else
                            {
                                #region "..If Dx not filled give message to user to fill the Dx.."

                                MessageBox.Show("Please select diagnosis.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1Dx.Focus();
                                if (c1Dx.Rows.Count >= 2)
                                { c1Dx.Select(1, COL_DX_SELECT); }
                                _result = false;

                                #endregion "..If Dx not filled give message to user to fill the Dx.."
                            }
                        }
                    }
                }

                #endregion


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (odsChargesTVP != null && odsChargesTVP.Tables[1] != null)
                {
                    if (_dsLastClaimDetails != null)
                    {
                        _dsLastClaimDetails.Dispose();
                        _dsLastClaimDetails = null;
                    }
                    _dsLastClaimDetails = odsChargesTVP.Copy();
                }
                //SetFormDataAfterClaimSave(_TransactionID, _nextClaimNo);
                ShowHideControls(ShowHideType.WaitFinished);
                if (odsChargesTVP != null)
                {
                    odsChargesTVP.Dispose();
                    odsChargesTVP = null;
                }
            }
            return _result;
        }
       
        #region "Move Claim Attached Document to RCM DOC"

        private bool MoveOCPClaimAttachmentToRCMDOC(long _PortalClaimID)
        {
            Boolean bIsDocMoved = false;
            Boolean bIsAttchmentPresent = false;
            try
            {
                DataTable dtAttachment = GetRCMDocDetails(_PortalClaimID, 3);
                if (dtAttachment != null && dtAttachment.Rows.Count > 0)
                {
                    bIsAttchmentPresent = Convert.ToBoolean(dtAttachment.Rows[0]["bIsAttchmentPresent"]);
                }
                if (bIsAttchmentPresent==false)
                    return false;

                gloSettings.GeneralSettings oSettings = new GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                object objOCPDefaultRCMDOC = new object();
                oSettings.GetSetting("OCPDEFAULTRCMCATEGORY", 0, gloGlobal.gloPMGlobal.ClinicID, out objOCPDefaultRCMDOC);
                oSettings.Dispose();
                oSettings = null;

                if (Convert.ToString(objOCPDefaultRCMDOC) == "")
                {
                    MessageBox.Show("Set Default RCM category in gloPM Admin>>User Management for any of user.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return bIsDocMoved;
                }
                Int64 nRCMcategoryID = GetRCMCategoryID(Convert.ToString(objOCPDefaultRCMDOC));
                string sRCMCategoryName = Convert.ToString(objOCPDefaultRCMDOC);
                string sRCMDocName = string.Empty;
                string sRCMDocFullPath = string.Empty;
                Int64 nPostedClaimNo = 0;
                DataTable dtClaimDetails = GetRCMDocDetails(_PortalClaimID, 1);
                if (dtClaimDetails != null && dtClaimDetails.Rows.Count > 0)
                {
                    sRCMDocName = Convert.ToString(dtClaimDetails.Rows[0]["sFileName"]);
                    nPostedClaimNo = Convert.ToInt64(dtClaimDetails.Rows[0]["nPostedClaimNo"]);
                }

                sRCMDocFullPath = ExtractDocument(sRCMDocName, _PortalClaimID);
                if (sRCMDocFullPath != "")
                {
                    Int64 nEDocumentID = 0;
                    bIsDocMoved = MoveDOCToRCM(nRCMcategoryID, sRCMCategoryName, sRCMDocFullPath, nPostedClaimNo, out nEDocumentID);
                    if (bIsDocMoved)
                    {
                        GetRCMDocDetails(_PortalClaimID, 2, nEDocumentID);
                        if (System.IO.Path.GetDirectoryName(sRCMDocFullPath) != "")
                        {
                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(sRCMDocFullPath));
                            dir.Delete(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while insert attached claim document from Online Charge Posting. "+ex.ToString(), true);
            }
            return bIsDocMoved;
        }

        private Boolean MoveDOCToRCM(long nRCMcategoryID, string sRCMCategoryName, string sRCMDocFullPath, long nClaimNo, out long DestDocumentID)
        {
            bool oDialogResultIsOK = false;
            DestDocumentID = 0;
            try
            {
                string sDMSConnectionString = gloGlobal.gloPMGlobal.DMSConnectionString;
                if (nRCMcategoryID > 0 && sDMSConnectionString != "" && sRCMDocFullPath != "")
                {
                    string filename = DateTime.Now.ToString("MM dd yyyy HH mm ss tt");
                    string stryear = DateTime.Now.Year.ToString();
                    string strmonth = DateTime.Now.ToString("MMMM");
                    Int64 oDialogDocumentID = 0;
                    Int64 oDialogContainerID = 0;
                    ProgressBar pbDocument = new ProgressBar();
                    pbDocument.Minimum = 0;
                    pbDocument.Maximum = 100;

                    gloEDocumentV3.eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
                    oDocManager._isgloServices = true;
                    filename = Convert.ToString(nClaimNo) + "_" + System.IO.Path.GetFileNameWithoutExtension(sRCMDocFullPath);

                    string ImportInSubDirectory = "";
                    if (string.IsNullOrEmpty(ImportInSubDirectory))
                    {
                        ImportInSubDirectory = DateTime.Now.ToString("MM dd yyyy");
                    }
                    ArrayList oSourceDocuments = new ArrayList();

                    Int64 nPatientId = 0;
                    Int64 nClinicId = 1;
                    string sFileExt = System.IO.Path.GetExtension(sRCMDocFullPath);
                    bool bImportImage = false, bImportPDF = false;
                    switch (sFileExt.ToUpper())
                    {
                        case ".JPG":
                        case ".PNG":
                        case ".BMP":
                        case ".JPEG": bImportImage = true; break;
                        case ".PDF":
                        case ".TXT":
                        case ".DOCX":
                        case ".DOC": bImportPDF = true; break;
                    }
                    if (bImportImage)
                    {
                        oSourceDocuments.Add(sRCMDocFullPath);
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for Image - START");
                        oDialogResultIsOK = oDocManager.ImportImages(nPatientId, oSourceDocuments, filename, nRCMcategoryID, sRCMCategoryName, ImportInSubDirectory, stryear, strmonth, nClinicId, out oDialogContainerID, out oDialogDocumentID, false, pbDocument, gloEDocumentV3.Enumeration.enum_OpenExternalSource.RCM);
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for Image - FINISHED");
                    }
                    else if (bImportPDF)
                    {
                        string SelectedDocumentname = System.IO.Path.GetFileName(sRCMDocFullPath);
                        string retPDFDocPath = "";
                        string FinalPath = System.IO.Path.GetDirectoryName(sRCMDocFullPath);
                        if (System.IO.Path.GetExtension(SelectedDocumentname).ToUpper() != ".PDF".ToUpper())
                        {
                            string sOtherDocument = System.IO.Path.Combine(FinalPath, SelectedDocumentname);
                            string strOtherDocumentasPDFpath = System.IO.Path.Combine(FinalPath, System.IO.Path.GetFileNameWithoutExtension(SelectedDocumentname) + ".pdf");
                            string sDestPdf1 = "";
                            sDestPdf1 = gloWord.gloWord.ConvertFileToPDF(sOtherDocument, FinalPath);
                            System.IO.FileInfo file = new System.IO.FileInfo(sDestPdf1);
                            file.CopyTo(strOtherDocumentasPDFpath);
                            if (file.Exists)
                            {
                                file.Delete();
                            }

                            retPDFDocPath = strOtherDocumentasPDFpath;
                        }
                        else
                        {
                            retPDFDocPath = System.IO.Path.Combine(FinalPath, SelectedDocumentname);
                        }
                        oSourceDocuments.Add(retPDFDocPath);
                        oDocManager.SetSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString, sDMSConnectionString, gloGlobal.gloPMGlobal.DMSV3TempPath + "DMSLogFile.txt", gloSettings.FolderSettings.AppTempFolderPath + "DMSV2Temp");
                        oDocManager.ConnectToPDFTron();
                        oDialogResultIsOK = oDocManager.ImportSplit(nPatientId, oSourceDocuments, filename, nRCMcategoryID, sRCMCategoryName, ImportInSubDirectory, stryear, strmonth, gloGlobal.gloPMGlobal.ClinicID, out oDialogContainerID, out oDialogDocumentID, false, pbDocument, gloEDocumentV3.Enumeration.enum_OpenExternalSource.RCM);
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.InsertFile, gloAuditTrail.ActivityType.Add, "Insert attached claim document from OCP successfully.", gloAuditTrail.ActivityOutCome.Success);
                    }

                    DestDocumentID = oDialogDocumentID;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while moving document to RCM DOC. " + ex.ToString(), true);
            }
            return oDialogResultIsOK;
        }

        public string ExtractDocument(string SelectedDocumentname, Int64 nPortalClaimID)
        {
            string retPDFDocPath = "";
            string strTempPHIFolder = System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, "OCPDOC");
            try
            {
                DataTable dtDocZipData = GetRCMDocDetails(nPortalClaimID, 0);
                if (dtDocZipData != null && dtDocZipData.Rows.Count > 0)
                {
                    string datacontent = "";
                    byte[] bytesRead = (byte[])dtDocZipData.Rows[0]["sDocument"];
                    datacontent = System.Text.Encoding.ASCII.GetString(bytesRead);
                    if (!System.IO.Directory.Exists(strTempPHIFolder))
                    {
                        System.IO.Directory.CreateDirectory(strTempPHIFolder);
                    }

                    var strZipFilePath = System.IO.Path.Combine(strTempPHIFolder, "OCP_RCMDOC_" + nPortalClaimID.ToString() + "_" + DateTime.Now.ToString("yyyyddMMhhmmss") + ".zip");
                    System.IO.File.WriteAllBytes(strZipFilePath, Convert.FromBase64String(datacontent));
                    string FinalPath = ExtractZipFile(strZipFilePath);
                    string sOtherDoc = System.IO.Path.Combine(FinalPath, SelectedDocumentname);
                    retPDFDocPath = sOtherDoc;

                    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(strZipFilePath));

                    foreach (System.IO.FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }

                    //if (System.IO.Path.GetExtension(SelectedDocumentname).ToUpper() != ".PDF".ToUpper())
                    //{
                    //    string sOtherDocument = System.IO.Path.Combine(FinalPath, SelectedDocumentname);
                    //    string strOtherDocumentasPDFpath = System.IO.Path.Combine(FinalPath, System.IO.Path.GetFileNameWithoutExtension(SelectedDocumentname) + ".pdf");
                    //    string sDestPdf1 = "";
                    //    sDestPdf1 = gloWord.gloWord.ConvertFileToPDF(sOtherDocument, FinalPath);
                    //    System.IO.FileInfo file = new System.IO.FileInfo(sDestPdf1);
                    //    file.CopyTo(strOtherDocumentasPDFpath);
                    //    if (file.Exists)
                    //    {
                    //        file.Delete();
                    //    }

                    //    retPDFDocPath = strOtherDocumentasPDFpath;
                    //}
                    //else
                    //{
                    //    retPDFDocPath = System.IO.Path.Combine(FinalPath, SelectedDocumentname);
                    //}

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while extracting and creating document for moving doc to RCM DOC. " + ex.ToString(), true);
            }

            return retPDFDocPath;
        }

        private Int64 GetRCMCategoryID(string _CategoryName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DMSConnectionString);
            DataTable _dtCategory = null;
            Int64 nRCMCategoryID = 0;
            try
            {
                string _strQuery = string.Format("Select CategoryId as CategoryId From dbo.eDocument_Category_V3_RCM edcvr WITH(NOLOCK) WHERE edcvr.CategoryName ='{0}'", _CategoryName);

                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out _dtCategory);
                oDB.Disconnect();
                if (_dtCategory != null && _dtCategory.Rows.Count > 0)
                {
                    nRCMCategoryID = Convert.ToInt64(_dtCategory.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return nRCMCategoryID;

        }

        /// <summary>
        /// User for RCM Document releted information e.g. Binary Zip Data, File Name and Claim No, Update DocumentID in OCP_PortalFile_MST Table
        /// </summary>
        /// <param name="nPortalClaimID"></param>
        /// <param name="nReturnValueFor">0 = Get Binary RCM Document, 1= Get Claim Details, 2 = To update DocumentID in OCP_PortalFile_MST Table, 3 = Check if claim has attachment.</param>
        /// <param name="nEDocumentID">DocumentID to update</param>
        /// <returns></returns>
        private DataTable GetRCMDocDetails(Int64 nPortalClaimID,Int32 nReturnValueFor,Int64 nEDocumentID=0)
        {
            DataTable _dtRCMDocument = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            object obj = new object();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nPortalClaimID", nPortalClaimID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nReturnValueFor", nReturnValueFor, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nEDocumentID", nEDocumentID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_OCP_GetRCMDOCDetails", oDBParameters, out _dtRCMDocument);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
            return _dtRCMDocument;
        }

        //private DataTable GetRCMClaimDetails(Int64 nPortalClaimID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    DataTable _dtRCMFiles = null;
        //    string nRCMFileName = string.Empty;
        //    try
        //    {
        //        string _strQuery = string.Format("Select ISNULL(opcm.sAttachedFileName,'') AS sFileName, ISNULL(opcm.PostedAsClaimNo,0) AS nPostedClaimNo,ISNULL(opcm.nPatientID,0) From dbo.OCP_PortalClaimMaster opcm	WHERE opcm.nPortalClaimID='{0}'", nPortalClaimID);

        //        oDB.Connect(false);
        //        oDB.Retrive_Query(_strQuery, out _dtRCMFiles);
        //        oDB.Disconnect();

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //    return _dtRCMFiles;

        //}

        //private void UpdateOCPPortalDetails(long PortalClaimID, long EDocumentID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    try
        //    {
        //        string _strQuery = string.Format("UPDATE dbo.OCP_PortalFile_MST SET dbo.OCP_PortalFile_MST.PostedDocumentID = {1} where dbo.OCP_PortalFile_MST.nPortalClaimID = '{0}'", PortalClaimID, EDocumentID);

        //        oDB.Connect(false);
        //        oDB.Execute_Query(_strQuery);
        //        oDB.Disconnect();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //}

        #region "Extract file from zip"
        private string ExtractZipFile(string zipfilename)
        {
            string FinalPath = string.Empty;
            String newpath = String.Empty;
            try
            {
                newpath = System.IO.Path.GetDirectoryName(zipfilename);
                FinalPath = newpath + "\\" + System.IO.Path.GetFileNameWithoutExtension(zipfilename);
                System.IO.Directory.CreateDirectory(FinalPath);
                using (ZipFile zipfile = new ZipFile(zipfilename))
                {
                    List<ZipEntry> entries = GetZippedItems(zipfile, FinalPath);


                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while extracting zip file. "+ex.ToString(), true);
            }
            return FinalPath;
        }
        private static List<ZipEntry> GetZippedItems(ZipFile file, string TargetDirectory)
        {

            List<ZipEntry> entries = new List<ZipEntry>();

            try
            {
                string ExistingZipFile = file.Name.ToString();// @"C:\ZIPFILES\RHDSetup.zip";

                //string dir = System.IO.Path.GetDirectoryName(entry.getName());
                using (ZipFile zip = ZipFile.Read(ExistingZipFile))
                {
                    //System.String TargetDirectory = System.IO.Path.GetDirectoryName(ExistingZipFile);
                    foreach (ZipEntry e in zip)
                    {
                        e.Extract(TargetDirectory, ExtractExistingFileAction.OverwriteSilently);
                        // string sFilename = e.FileName.ToString();  
                        entries.Add(e);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while getting zip file. " + ex.ToString(), true);
            }
            return entries;
        }
        #endregion
        
        #endregion

        private void SAVE_PA_Account_log(dsChargesTVP odsChargesTVP)
        {
            //Create ClsPACCOUNTLOGCollection Obje and call ADD_PA_Account_Log method
            //below is Example 
            //ClsPACCOUNTLOGCollection obj = new ClsPACCOUNTLOGCollection(_DatabaseConnectionString);
            //obj.Add(new ClsPACCOUNTLOG()
            //{

            //    nID = 0,
            //    nAccountPatientID = (from dt in odsChargesTVP.BL_Transaction_Claim_MST
            //                        select Convert.ToInt64(dt.nAccountPatientID)).Last(),
            //    nPAccountID = (from dt in odsChargesTVP.BL_Transaction_Claim_MST
            //                         select Convert.ToInt64(dt.nPAccountID)).Last(),
            //    dtCloseDate=Convert.ToDateTime(mskClaimDate.Text),
            //    sLog = "post claim",
            //    sLogType = "post claim",
            //    sUserName ="Admin",
            //    sActivityBy="EXE",
            //    dtDateTime=DateTime.Now

            //});
            //obj.SAVE_PA_Account_Log(obj);
            //CPTCollection oCPTColl = new CPTCollection();
        }

        private void CheckforGlobalPeriodTriggeringCPTs(DataTable _dtChargeCPTList)
        {
            try
            {
                DataTable _dtGlobalPeriodInfo = gloCharges.CheckForGlobalPeriods(_dtChargeCPTList, Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCEID)), Convert.ToInt64(cmbBillingProvider.SelectedValue), oPatientControl.PatientID);

                clsGlobalPeriods objclsGlobalPeriods = null;



                if (_dtGlobalPeriodInfo != null && _dtGlobalPeriodInfo.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtGlobalPeriodInfo.Rows.Count; i++)
                    {

                        objclsGlobalPeriods = new clsGlobalPeriods();
                        objclsGlobalPeriods.nPatientID = Convert.ToInt64(_dtGlobalPeriodInfo.Rows[i]["nPatientID"]);
                        objclsGlobalPeriods.nInsuranceID = Convert.ToInt64(_dtGlobalPeriodInfo.Rows[i]["nInsuranceID"]);
                        objclsGlobalPeriods.sCPT = Convert.ToString(_dtGlobalPeriodInfo.Rows[i]["sCPT"]);
                        objclsGlobalPeriods.sCPTDescription = Convert.ToString(_dtGlobalPeriodInfo.Rows[i]["sCPTDescription"]);
                        objclsGlobalPeriods.dtStartDate = Convert.ToDateTime(_dtGlobalPeriodInfo.Rows[i]["dtStartDate"]);
                        objclsGlobalPeriods.nDays = Convert.ToInt32(_dtGlobalPeriodInfo.Rows[i]["nPeriodDays"]);
                        objclsGlobalPeriods.nProviderID = Convert.ToInt64(_dtGlobalPeriodInfo.Rows[i]["nProviderID"]);
                        objclsGlobalPeriods.sReminder = Convert.ToString(_dtGlobalPeriodInfo.Rows[i]["sBillingReminder"]);

                        frmSetupGlobalPeriod ofrmSetupGlobalPeriod = new frmSetupGlobalPeriod(objclsGlobalPeriods);
                        ofrmSetupGlobalPeriod.ShowDialog(this);
                        ofrmSetupGlobalPeriod.Dispose();
                        objclsGlobalPeriods.Dispose();
                        objclsGlobalPeriods = null;
                    }
                }
                if (_dtGlobalPeriodInfo != null)
                {
                    _dtGlobalPeriodInfo.Dispose();
                    _dtGlobalPeriodInfo = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool CheckforExistingPatientGlobalPeriods(DataTable _dtChargeCPTList)
        {
            bool _return = false;
            try
            {
                //Skip the global Periods For Self claim
                if (c1Insurance.GetData(1, COL_INSURANCEID) != null && Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCEID)) > 0)
                {
                    DataTable _dtGlobalPeriod = gloCharges.CheckPatientGlobalPeriods(_dtChargeCPTList, Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCEID)), _PatientID);
                    if (_dtGlobalPeriod != null && _dtGlobalPeriod.Rows.Count > 0)
                    {
                        frmPromptforGlobalPeriod ofrmPromptforGlobalPeriod = new frmPromptforGlobalPeriod(_dtGlobalPeriod);
                        ofrmPromptforGlobalPeriod.ShowDialog(this);
                        GPPromptOutput _output = ofrmPromptforGlobalPeriod.PromptOutput;
                        ofrmPromptforGlobalPeriod.Dispose();

                        switch (_output)
                        {
                            case GPPromptOutput.Single_StoptoReviewCharges:
                                _return = false;
                                break;
                            case GPPromptOutput.Single_StoptoReviewGlobalPeriodDetails:

                                frmSetupModifyGlobalPeriod ofrmSetupGlobalPeriod = new frmSetupModifyGlobalPeriod(Convert.ToInt64(_dtGlobalPeriod.Rows[0]["nID"]));
                                ofrmSetupGlobalPeriod.ShowInTaskbar = false;
                                ofrmSetupGlobalPeriod.ShowDialog(this);
                                ofrmSetupGlobalPeriod.Dispose();
                                SetLastGlobalPeriods();
                                _return = false;
                                break;
                            case GPPromptOutput.Single_ContinueSaveofNewCharges:
                                _return = true;
                                break;
                            case GPPromptOutput.Multiple_StoptoReviewCharges:
                                _return = false;
                                break;
                            case GPPromptOutput.Multiple_StoptoReviewGlobalPeriodDetails:

                                gloAccountsV2.frmPatientFinancialViewV2 frm = new gloAccountsV2.frmPatientFinancialViewV2(oPatientControl.PatientID);
                                frm.ShowGlobalperiod = true;
                                frm.StartPosition = FormStartPosition.CenterScreen;
                                frm.WindowState = FormWindowState.Maximized;
                                frm.ShowInTaskbar = false;
                                frm.ShowDialog(this);
                                frm.Dispose();
                                SetLastGlobalPeriods();
                                _return = false;
                                break;

                            case GPPromptOutput.Multiple_ContinueSaveofNewCharges:
                                _return = true;
                                break;
                            default:
                                _return = true;
                                break;
                        }
                    }
                    else
                    {
                        _return = true;
                    }
                    if (_dtGlobalPeriod != null)
                    {
                        _dtGlobalPeriod.Dispose();
                        _dtGlobalPeriod = null;
                    }
                }
                else
                {
                    _return = true;
                }

            }
            catch (Exception ex)
            {
                _return = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _return;
        }

        #region " Save Method Moved to Class "

        //public Int64 SaveClaim(dsChargesTVP dsChargesTVP)
        //{
        //    Int64 TransactionMasterID = 0;
        //    string sTransResult = "";
        //    object _oTransactionMstID = null;
        //    object _oNewClaimNo = null;

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

        //    try
        //    {
        //        if (dsChargesTVP.BL_Transaction_Claim_MST != null && dsChargesTVP.BL_Transaction_Claim_MST.Rows.Count > 0)
        //        {

        //            oParameters.Add("@tvpClaimMST", dsChargesTVP.Tables["BL_Transaction_Claim_MST"], ParameterDirection.Input, SqlDbType.Structured);
        //            oParameters.Add("@tvpClaim_Lines", dsChargesTVP.Tables["BL_Transaction_Claim_Lines"], ParameterDirection.Input, SqlDbType.Structured);
        //            oParameters.Add("@tvpClaimDx", dsChargesTVP.Tables["BL_Transaction_Diagnosis"], ParameterDirection.Input, SqlDbType.Structured);
        //            oParameters.Add("@tvpClaim_Lines_Notes", dsChargesTVP.Tables["BL_Transaction_Lines_Notes"], ParameterDirection.Input, SqlDbType.Structured);
        //            oParameters.Add("@tvpClaim_InsurancePlans", dsChargesTVP.Tables["BL_Transaction_InsPlan"], ParameterDirection.Input, SqlDbType.Structured);
        //            oParameters.Add("@tvpNextAction", dsChargesTVP.Tables["BL_EOB_NextAction"], ParameterDirection.Input, SqlDbType.Structured);

        //            if (_EMRExamID > 0)
        //            {
        //                oParameters.Add("@EMRExamID", _EMRExamID, ParameterDirection.Input, SqlDbType.BigInt);
        //                oParameters.Add("@EMRVisitID", _EMRVisitID, ParameterDirection.Input, SqlDbType.BigInt);
        //            }

        //            oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Output, SqlDbType.BigInt);
        //            oParameters.Add("@sTranResult", sTransResult, ParameterDirection.Output, SqlDbType.VarChar,1000);
        //            oDB.Connect(false);
        //            oDB.Execute("SaveCharges_TVP", oParameters, out  _oTransactionMstID, out _oNewClaimNo);
        //            oDB.Disconnect();

        //            if (_oTransactionMstID != null)
        //                TransactionMasterID = Convert.ToInt64(_oTransactionMstID);
        //            else
        //                TransactionMasterID = 0;

        //            if (_oNewClaimNo != null)
        //            { sTransResult = Convert.ToString(_oNewClaimNo); }
        //            else
        //            { sTransResult = ""; }

        //            if (TransactionMasterID != 0)
        //            {
        //                _nextClaimNo = Convert.ToInt64(sTransResult);
        //            }
        //            else
        //            {
        //                gloAuditTrail.gloAuditTrail.ExceptionLog(sTransResult, true);
        //            }
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (oParameters != null) { oParameters.Dispose(); }
        //    }

        //    return TransactionMasterID;

        //}

        #endregion

        private void SetMasterClaimDetails_TVP(dsChargesTVP dsChargesTVP)
        {


            Int64 _MasterTransactionAppointmentId = 0;
            Int64 _MasterTransactionVisitId = 0;
           // Transaction MasterTransaction = new Transaction();
            //***********************************************
            //Fields not implemented yet, so hard coded
            _MasterTransactionAppointmentId = 999;
            _MasterTransactionVisitId = 987;
            //***********************************************


            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows.Add();

            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionMasterID"] = 0;

            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionID"] = 0;

            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nMasterAppointmentID"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAppointmentID"] = _MasterTransactionAppointmentId;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nVisitID"] = _MasterTransactionVisitId;

            if (CmbAccidentType.Text.Trim() == "Work")
            {

                //if (txt_WcAc.Text.Trim() != "")
                //{
                //    if (Convert.ToString(txt_WcAc.Tag).Trim() != "-1848750000") // To validate if date is entered and Wc is entered and hit save&cls
                //    {
                if (IsValidDate(mskInjuryDate.Text))
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nInjuryDate"] = gloDateMaster.gloDate.DateAsNumber(mskInjuryDate.Text.ToString()); }
                else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nInjuryDate"] = 0; }

                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sWorkersCompNo"] = txt_WcAc.Text;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bWorkersComp"] = true;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsWorkersCompOnCMS1500"] = chkPrintWorkersComp.Checked;
                //    }
                //}

            }
            else if (CmbAccidentType.Text.Trim() == "Auto")
            {
                //if (txt_WcAc.Text.Trim() != "")
                //{
                //    if (Convert.ToString(txt_WcAc.Tag).Trim() != "-1848750000")
                //    {
                if (IsValidDate(mskAccidentDate.Text))
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAccidentDate"] = gloDateMaster.gloDate.DateAsNumber(mskAccidentDate.Text.ToString()); }
                else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAccidentDate"] = 0; }

                if (txt_WcAc.Text.Trim() != "")
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sWorkersCompNo"] = txt_WcAc.Text;
                }
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bAutoClaim"] = true;
                //    }
                //}
            }

            else if (CmbAccidentType.Text.Trim() == "Other")
            {

                if (IsValidDate(mskOtherDate.Text))
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOtherAccidentDate"] = gloDateMaster.gloDate.DateAsNumber(mskOtherDate.Text.ToString()); }
                else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOtherAccidentDate"] = 0; }

                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bOtherAccident"] = true;
            }
            else
            {
                

                if (IsValidDate(mskOnsiteDate.Text) && mskOnsiteDate.MaskCompleted)
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOnsiteDate"] = gloDateMaster.gloDate.DateAsNumber(mskOnsiteDate.Text.ToString());
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sBox14DateQualifier"] = cmbBox14DateQualifier.SelectedValue;
                }
                else
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOnsiteDate"] = 0;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sBox14DateQualifier"] = DBNull.Value;
                }

                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bWorkersComp"] = false;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bAutoClaim"] = false;
            }


            //if (IsValidDate(mskUnableFromDate.Text))
            //{ dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkFromDate"] = gloDateMaster.gloDate.DateAsNumber(mskUnableFromDate.Text.ToString()); }
            //else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkFromDate"] = 0; }


            //if (IsValidDate(mskUnableTillDate.Text))
            //{ dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkTillDate"] = gloDateMaster.gloDate.DateAsNumber(mskUnableTillDate.Text.ToString()); }
            //else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkTillDate"] = 0; }

            
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkFromDate"] = _UnableToWorkFromDate_MoreClaimData; 
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkTillDate"] = _UnableToWorkTillDate_MoreClaimData; 



            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionDate"] = gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text.ToString());
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sCaseNoPrefix"] = "Claim";
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClaimNo"] = Convert.ToInt64(txtClaimNo.Text);
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nSubClaimNo"] = "";
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionProviderID"] = Convert.ToInt64(cmbBillingProvider.SelectedValue);
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sMaritalStatus"] = oPatientControl.PatientsMaritalStatus; ;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sFacilityCode"] = Convert.ToString(cmbFacility.SelectedValue); ;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sFacilityDescription"] = Convert.ToString(cmbFacility.Text);
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionType"] = TransactionType.Billed;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClinicID"] = _ClinicID;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionStatusID"] = TransactionStatus.Queue;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sState"] = cmbState.Text;

            if (IsValidDate(mskHospitaliztionFrom.Text))
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHopitalizationDateFrom"] = gloDateMaster.gloDate.DateAsNumber(mskHospitaliztionFrom.Text.ToString()); }
            else
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHopitalizationDateFrom"] = 0; }


            if (IsValidDate(mskHospitaliztionTo.Text))
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHopitalizationDateTo"] = gloDateMaster.gloDate.DateAsNumber(mskHospitaliztionTo.Text.ToString()); }
            else
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHopitalizationDateTo"] = 0; }

            if (IsValidDate(mskInitTreatment.Text))
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtInitTreatmentDate"] = (mskInitTreatment.Text); }
            else
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtInitTreatmentDate"] = DBNull.Value; }



            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bOutSideLab"] = chkOutSideLab.Checked;
            //if (chkOutSideLab.Checked)
            //{
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dOutSideLabCharges"] = Convert.ToDecimal(txtOutSideLabCharges.Text.Trim());
            //}

            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sCLIANumber"] = Convert.ToString(txtClaimCLIAno.Text);
            //Tobe implemented
            //MasterTransaction.Insurances = new TransactionInsurances();
            //Insurance.ClinicID = _ClinicID;
            //Insurance.TransactionID = MasterTransaction.TransactionID;  //oLineTransactions[0].TransactionId;
            //Insurance.TransactionDetailID = 0;
            //Insurance.TransactionLineNo = 0;
            //MasterTransaction.Insurances.Add(Insurance);



            if (txtPriorAuthorizationNo.Tag != null && txtPriorAuthorizationNo.Tag.ToString().Trim() != "")
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAuthorizationID"] = Convert.ToInt64(txtPriorAuthorizationNo.Tag);
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sAuthorizationNumber"] = txtPriorAuthorizationNo.Text.Trim();
            }


            if (cmbReferralProvider.Items.Count > 0 && cmbReferralProvider.SelectedIndex != -1)
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nReferralProviderID"] = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nReferralID"] = Convert.ToInt64(cmbReferralProvider.SelectedValue);
            }
            if (cmbClaimCategory.Visible==true)
            {
                if (cmbClaimCategory.Items.Count > 0 && cmbClaimCategory.SelectedIndex != -1)
                {
                    if (Convert.ToInt64(cmbClaimCategory.SelectedValue) != 0)
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClaimReprtingCategoryID"] = Convert.ToInt64(cmbClaimCategory.SelectedValue);
                    }
                }
            }
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUserID"] = this.UserID;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUserName"] = this.UserName;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nChargesDayTrayID"] = SelectedChargeTrayID;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sChargesTrayCode"] = "";
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sChargesTrayDescription"] = SelectedChargeTray;
            

            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nStatus"] = TransactionStatus.Queue;


            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nLastStatusId"] = Convert.ToInt64(txtLastStatusId.Text.Trim());
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nSendToInsFlag"] = InsuranceTypeFlag.None;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClaimStatus"] = ClaimStatus.Open;


            if (chkFeeSchedule.Checked == true)
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFeeScheduleType"] = FeeScheduleType.UserSelected;
                if (cmbFeeSchedule.SelectedValue != null)
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFeeScheduleID"] = Convert.ToInt64(cmbFeeSchedule.SelectedValue);
                }

            }
            else
            {
                if (UC_gloBillingTransactionLines.Fee_ScheduleType == FeeScheduleType.CPT && Convert.ToInt64(cmbFeeSchedule.SelectedValue) > 0)
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFeeScheduleType"] = UC_gloBillingTransactionLines.Fee_ScheduleType;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFeeScheduleID"] = Convert.ToInt64(cmbFeeSchedule.SelectedValue);
                }
                else
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFeeScheduleType"] = UC_gloBillingTransactionLines.Fee_ScheduleType;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFeeScheduleID"] = UC_gloBillingTransactionLines.Fee_ScheduleID;
                }
            }


            if (chkFacilityFeeSchedule.Checked == true)
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFacilityType"] = FacilityType.Facility.GetHashCode(); }
            else if (chkNonFacilityCharges.Checked == true)
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFacilityType"] = FacilityType.NonFacility.GetHashCode(); }
            else
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFacilityType"] = FacilityType.None.GetHashCode(); }

            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nIsTrayClose"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtCreateDate"] = DBNull.Value;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtModifyDate"] = DBNull.Value;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsVoid"] = DBNull.Value;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtVoidDate"] = DBNull.Value;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nVoidUserID"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sVoidUserName"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtDayClosedOn"] = DBNull.Value;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nDayClosedUserID"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sDayCloseUserName"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsDayUpdated"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nVoidCloseDate"] = DBNull.Value;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nVoidTrayID"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bSorted"] = UC_gloBillingTransactionLines.AutoSort.GetHashCode();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nParentTransactionID"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nParentClaimNo"] = "";
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsOpened"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sMachineID"] = DBNull.Value;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bCheckedIn"] = true;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sMainClaimNo"] = "";
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsRebilled"] = 0;


            #region " Hold Claim "

            if (_oClaimHold != null && _oClaimHold.IsHold)
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsHold"] = _oClaimHold.IsHold;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sHoldReason"] = _oClaimHold.HoldReason;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtHoldDate"] = _oClaimHold.HoldDateTime;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHoldUserID"] = _oClaimHold.HoldUserID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtHoldModifyDateTime"] = _oClaimHold.HoldModDateTime;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHoldBillingID"] = _oClaimHold.HoldID;

            }
            else
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsHold"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sHoldReason"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtHoldDate"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHoldUserID"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtHoldModifyDateTime"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHoldBillingID"] = DBNull.Value;
            }

            #endregion

            #region " More Claim Data "

            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsReplacementClaim"] = _IsbCliamReplacement;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsRefProvAsSupervisor"] = _bIsRefprovAsSupervisor;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClaimType"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nIllnessDate"] = _IllnessDate;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nLastSeenDate"] = _LastSeenDate;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sClaimRemittanceRefNo"] = _sClaimRefNo.Trim();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sDelayReasonCodeID"] = _sDelayReasonCode.Trim();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sServiceAuthExceCode"] = _sServiceAuthExceptionCode.Trim();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sMedicaidResubmissionCode"] = _sMedicaidResubmissionCode.Trim();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sPWKReportTypeCode"] = _PWKReportTypeCode.Trim();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sPWKReportTransmissionCode"] = _PWKReportTransmissionCode.Trim();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sPWKAttachmentControlNumber"] = _PWKAttachmentControlNumber.Trim();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sMammogramCertNumber"] = _MammogramCertNumber.Trim();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sIDENo"] = _IDENo.Trim();
            
            #endregion

            if (chk_SameasBillingProvider.Checked)
            { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bSameAsBillingProvider"] = true; }
            else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bSameAsBillingProvider"] = false; }
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nEOBPaymentID"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nEOBID"] = 0;

            this.PatientID = oPatientControl.CmbSelectedPatientID;
            this.nAccountPatientID = oPatientControl.AccountPatientID;
            this.nPAccountID = oPatientControl.PAccountID;
            this.nGuarantorID = oPatientControl.GuarantorID;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nPatientID"] = this.PatientID;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nPAccountID"] = this.nPAccountID;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nGuarantorID"] = this.nGuarantorID;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAccountPatientID"] = this.nAccountPatientID;

            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nCreditID"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOldEOBID"] = 0;
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOldEobpaymentID"] = 0;

            if (cmbReferralProvider.Text.Trim() != "")
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sProviderQualifier"] = cmbProviderType.SelectedValue;
            }
            else
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sProviderQualifier"] = DBNull.Value;
            }

           


            if (mskBox15Date.MaskCompleted)
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtBox15Date"] = mskBox15Date.Text;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sBox15DateQualifier"] = cmbBox15DateQualifier.SelectedValue;
            }
            else
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtBox15Date"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sBox15DateQualifier"] = DBNull.Value;
            }
            if (Convert.ToString(txtCases.Tag) != string.Empty)
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nCaseID"] = Convert.ToInt64(txtCases.Tag);
            }
            else
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nCaseID"] = 0;
            }

            #region " Insurance Plan "

            if (oPatientControl != null)
            {

                Int64 _InsuranceID = 0;
                Int64 _ContactID = 0;
                Int16 _TempParty = 0;
                int _rowno = 0;

                PayerMode _InsuranceSelfMode = PayerMode.None;
                if (c1Insurance.Rows.Count > 0)
                {
                    for (int i = 1; i < c1Insurance.Rows.Count; i++)
                    {
                        if (c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY) != null && c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY).ToString().Trim().Length > 0)
                        {

                            Int16 _Party = 0;

                            if (Int16.TryParse(c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY).ToString(), out  _Party) == true)
                            {
                                if (_Party > 0)
                                {


                                    TransactionInsurancePlan _InsurancePlan = new TransactionInsurancePlan();
                                    _InsurancePlan.TransactionId = _TransactionID;
                                    _InsurancePlan.PatientId = _PatientID;
                                    _InsurancePlan.ClaimNo = 0;
                                    _InsurancePlan.InsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                    _InsurancePlan.ContactID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCECONTACTID));



                                    _InsurancePlan.ResponsibilityNo = _Party;
                                    _InsurancePlan.ResponsibilityType = Convert.ToInt16(c1Insurance.GetData(i, COL_INSSELFMODE));

                                    if (_TempParty > _Party || _TempParty == 0)
                                    {
                                        _TempParty = _Party;
                                        _InsuranceID = _InsurancePlan.InsuranceID;
                                        _InsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                        _InsuranceSelfMode = (PayerMode)_InsurancePlan.ResponsibilityType;
                                        _ContactID = _InsurancePlan.ContactID;
                                        _ResponsibleInsuranceContactID=_ContactID;
                                        _ResponsibleInsuranceID = _InsuranceID;
                                    }

                                    _InsurancePlan.ClinicID = _ClinicID;
                                    //MasterTransaction.InsurancePlans.Add(_InsurancePlan);

                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows.Add();
                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nTransactionID"] = 0;
                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nPatientID"] = _PatientID;
                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nClaimNo"] = 0;
                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nInsuranceID"] = _InsurancePlan.InsuranceID;
                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nContactID"] = _InsurancePlan.ContactID;
                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nResponsibilityNo"] = _InsurancePlan.ResponsibilityNo;
                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nResponsibilityType"] = _InsurancePlan.ResponsibilityType;
                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nClinicId"] = _ClinicID;
                                    dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["sClaimRemittanceRefNo"] = "";
                                    
                                    _InsurancePlan.Dispose();
                                    _InsurancePlan = null;
                                    
                                    _rowno = _rowno + 1;
                                }
                            }
                        }
                    }
                }

                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nInsuranceID"] = _InsuranceID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nContactID"] = _ContactID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nResponsibilityNo"] = 1;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nResponsibilityType"] = _InsuranceSelfMode;




                //for (int i = 0; i < MasterTransaction.Lines.Count; i++)
                //{
                //    MasterTransaction.Lines[i].InsuranceID = _InsuranceID;
                //    MasterTransaction.Lines[i].InsuranceName = _InsuranceName;
                //    MasterTransaction.Lines[i].InsurancePrimarySecondaryTertiary = _InsurancePrimarySecondaryTertiary.ToString();
                //    MasterTransaction.Lines[i].InsuranceSelfMode = _InsuranceSelfMode;
                //}

            }




            #endregion

            #region"UB04 Settings"

            //if (_dtUB04Settings != null && _dtUB04Settings.Rows.Count > 0)
            //{
            //    for (int i = 0; i < _dtUB04Settings.Rows.Count; i++)
            //    {
                    //if (Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsName"]).Trim() == "UB04_TypeOfBill")
                    //{
                    //    if (oUB.IsModify == false)
                    //    {
                    //        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04TypeOfBill"] = Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsValue"]).Trim();
                    //    }
                    //    else
                    //    {
                    //        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04TypeOfBill"] = oUB.sTypeofbill;
                    //    }
                    //}
                    //else 
            
                if (oUB.sTypeofbill == null || Convert.ToString(oUB.sTypeofbill) == "")
                {
                    if (oUB.TypeofbillDeleted == false)
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04TypeOfBill"] = TypeOfBill;
                }
                else
                {
                    if (oUB.TypeofbillDeleted == false)
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04TypeOfBill"] = oUB.sTypeofbill;
                }
                if (oUB.sAdmissionType == null || Convert.ToString(oUB.sAdmissionType) == "")
                {
                    if (oUB.AdmitTypeDeleted == false)
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04AdmisionType"] = AdmitType;
                }
                else
                {
                    if (oUB.AdmitTypeDeleted == false)
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04AdmisionType"] = oUB.sAdmissionType;
                }

                if (oUB.sDischargeStatus == null || Convert.ToString(oUB.sDischargeStatus) == "")
                {
                    if (oUB.DischargeStatusDeleted == false)
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04DischargeStatus"] = DischargeStatus;
                }
                else
                {
                    if (oUB.AdmitTypeDeleted == false)
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04DischargeStatus"] = oUB.sDischargeStatus;
                }
                if (oUB.sAdmitDate != "" && oUB.sAdmitDate != null)
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtAdmissionDate"] = Convert.ToDateTime(oUB.sAdmitDate).ToShortDateString();
                }
                

                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sAdmithour"] = oUB.sAdmitHour;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sDischargeHour"] = oUB.sDischargeHour;
            
                    
                     
                    //if (Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsName"]).Trim() == "UB04_AdmisionType")
                    //{
                    //    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04AdmisionType"] = Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsValue"]).Trim(); ;
                    //}
                    //else if (Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsName"]).Trim() == "UB04_AdmisionSource")
                    //{
                    //    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04AdmisionSource"] = Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsValue"]).Trim(); ;
                    //}
                    //else if (Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsName"]).Trim() == "UB04_DischargeStatus")
                    //{
                    //    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04DischargeStatus"] = Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsValue"]).Trim(); ;
                    //}

            //    }
            //}

            #endregion

            if (ClaimEPSDT != null)
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsEPSDTScreening"] = ClaimEPSDT.ClaimIncludeEPSDTScreening;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsEPSDTReferral"] = ClaimEPSDT.PatientGivenEPSDTReferral;
                if (ClaimEPSDT.PatientGivenEPSDTReferral)
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sReferralType"] = ClaimEPSDT.ReferralType;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sReferralCode"] = ClaimEPSDT.ReferralCode;
                }
                else
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sReferralType"] = DBNull.Value;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sReferralCode"] = DBNull.Value;
                }
            }
            else
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsEPSDTScreening"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsEPSDTReferral"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sReferralType"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sReferralCode"] = DBNull.Value;
            }

            if (rbICD10.Checked)
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nICDCodeType"] = 10;
            }
            else
            {
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nICDCodeType"] = 9;
            }

            //// Get last Service line DOS
            //if (MasterTransaction != null && MasterTransaction.Lines != null && MasterTransaction.Lines.Count > 0)
            //{
            //    for (int i = 0; i <= MasterTransaction.Lines.Count - 1; i++)
            //    {
            //        _sLastServiceLineDOS = Convert.ToString(MasterTransaction.Lines[i].DateServiceFrom);
            //    }
            //}

            ////sProviderQualifier
            //if (cmbProviderType.Items.Count > 0 && cmbProviderType.SelectedValue != "")
            //{ dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sProviderQualifier"] = Convert.ToString(cmbProviderType.SelectedValue); }
            //else
            //{ dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sProviderQualifier"] = DBNull.Value; }



            dsChargesTVP.Tables["BL_Transaction_InsPlan"].AcceptChanges();
            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].AcceptChanges();


        }

        private void SetServiceLineDetails_TVP(dsChargesTVP dsChargesTVP)
        {
            TransactionLines oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();
            int _NoteIndex = 0;
            DataTable _dtLinesUniqueIds = null;
            StringBuilder _sCPTCode = new StringBuilder();
            DataTable _dtRevenueCode = null;
            sbEMRTreatmentLineNos = new StringBuilder();
            if (oLineTransactions != null && oLineTransactions.Count > 0)
            {
                //..Get Unique Ids
                _dtLinesUniqueIds = gloCharges.GetUniqueIDsForLines(oLineTransactions.Count);


                #region "Revenue Code For CPT's"

                //Get the Revenue Code For Eaach CPT
                CPTCollection oCPTColl = new CPTCollection();

                for (int i = 0; i <= oLineTransactions.Count - 1; i++)
                {
                    oCPTColl.Add(new CPTDetails()
                    {
                        nSLNo = i + 1,
                        sCPT = oLineTransactions[i].CPTCode.ToString(),
                        dtStartDate = oLineTransactions[i].DateServiceFrom
                    });
                }

                _dtRevenueCode = gloCharges.GetRevenueCodeForCPT(oCPTColl);
                oCPTColl.Clear();
                oCPTColl = null;

                #endregion
                Boolean bIsExist = true;
                _dtCPTActivationDates = new DataTable();

                _dtCPTActivationDates.Columns.Add("sCPTCode");
                _dtCPTActivationDates.Columns.Add("nFromDate");
                _dtCPTActivationDates.Columns.Add("nToDate");

                for (int i = 0; i <= oLineTransactions.Count - 1; i++)
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows.Add();
                    _dtCPTActivationDates.Rows.Add();

                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterID"] = 0;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterDetailID"] = Convert.ToInt64(_dtLinesUniqueIds.Rows[i]["ID"]);
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionID"] = 0;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionDetailID"] = Convert.ToInt64(_dtLinesUniqueIds.Rows[i]["ID2"]);
                    //dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionLineNo"] = oLineTransactions[i].TransactionLineId;

                    //Added To Handle the Line No During the EMR Treatment Split
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionLineNo"] = i+1;

                    if (!_IsICD9Driven)
                    {
                        bIsExist = gloCharges.CheckCptIcd9Exists(_EMRExamID, _nEMRTreatmentType, oLineTransactions[i].CPTCode, oLineTransactions[i].EMRTreatmentLineNo);

                        if (bIsExist)
                        {
                            if (sbEMRTreatmentLineNos != null && sbEMRTreatmentLineNos.Length > 0)
                            {
                                sbEMRTreatmentLineNos.Append("," + oLineTransactions[i].EMRTreatmentLineNo.ToString());
                            }
                            else
                            {
                                sbEMRTreatmentLineNos.Append(oLineTransactions[i].EMRTreatmentLineNo.ToString());
                            }
                        }
                    }
                    else
                    {
                        if (sbEMRTreatmentLineNos != null && sbEMRTreatmentLineNos.Length > 0)
                        {
                            sbEMRTreatmentLineNos.Append("," + oLineTransactions[i].EMRTreatmentLineNo.ToString());
                        }
                        else
                        {
                            sbEMRTreatmentLineNos.Append(oLineTransactions[i].EMRTreatmentLineNo.ToString());
                        }
                    }

                    _dtCPTActivationDates.Rows[i]["sCPTCode"] = oLineTransactions[i].CPTCode;
                    _dtCPTActivationDates.Rows[i]["nFromDate"] = gloDateMaster.gloDate.DateAsNumber(oLineTransactions[i].DateServiceFrom.ToShortDateString());
                   

                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nFromDate"] = gloDateMaster.gloDate.DateAsNumber(oLineTransactions[i].DateServiceFrom.ToShortDateString());
                    if (!oLineTransactions[i].DateServiceTillIsNull)
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nToDate"] = gloDateMaster.gloDate.DateAsNumber(oLineTransactions[i].DateServiceTill.ToShortDateString());
                        _dtCPTActivationDates.Rows[i]["nToDate"] = gloDateMaster.gloDate.DateAsNumber(oLineTransactions[i].DateServiceTill.ToShortDateString()); 
                    }
                    else
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nToDate"] = 0;
                        _dtCPTActivationDates.Rows[i]["nToDate"] = 0;
                    }
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sPOSCode"] = oLineTransactions[i].POSCode;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sPOSDescription"] = oLineTransactions[i].POSDescription;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sTOSCode"] = oLineTransactions[i].TOSCode;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sTOSDescription"] = oLineTransactions[i].TOSDescription;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sCPTCode"] = oLineTransactions[i].CPTCode;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sCPTDescription"] = oLineTransactions[i].CPTDescription;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx1Code"] = oLineTransactions[i].Dx1Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx1Description"] = oLineTransactions[i].Dx1Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx2Code"] = oLineTransactions[i].Dx2Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx2Description"] = oLineTransactions[i].Dx2Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx3Code"] = oLineTransactions[i].Dx3Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx3Description"] = oLineTransactions[i].Dx3Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx4Code"] = oLineTransactions[i].Dx4Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx4Description"] = oLineTransactions[i].Dx4Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx5Code"] = oLineTransactions[i].Dx5Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx5Description"] = oLineTransactions[i].Dx5Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx6Code"] = oLineTransactions[i].Dx6Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx6Description"] = oLineTransactions[i].Dx6Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx7Code"] = oLineTransactions[i].Dx7Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx7Description"] = oLineTransactions[i].Dx7Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx8Code"] = oLineTransactions[i].Dx8Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sDx8Description"] = oLineTransactions[i].Dx8Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nDx1Pointer"] = oLineTransactions[i].Dx1Ptr;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nDx2Pointer"] = oLineTransactions[i].Dx2Ptr;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nDx3Pointer"] = oLineTransactions[i].Dx3Ptr;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nDx4Pointer"] = oLineTransactions[i].Dx4Ptr;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nDx5Pointer"] = oLineTransactions[i].Dx5Ptr;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nDx6Pointer"] = oLineTransactions[i].Dx6Ptr;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nDx7Pointer"] = oLineTransactions[i].Dx7Ptr;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nDx8Pointer"] = oLineTransactions[i].Dx8Ptr;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sMod1Code"] = oLineTransactions[i].Mod1Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sMod1Description"] = oLineTransactions[i].Mod1Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sMod2Code"] = oLineTransactions[i].Mod2Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sMod2Description"] = oLineTransactions[i].Mod2Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sMod3Code"] = oLineTransactions[i].Mod3Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sMod3Description"] = oLineTransactions[i].Mod3Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sMod4Code"] = oLineTransactions[i].Mod4Code;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sMod4Description"] = oLineTransactions[i].Mod4Description;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dCharges"] = oLineTransactions[i].Charges;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dUnit"] = oLineTransactions[i].Unit;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dTotal"] = oLineTransactions[i].Total;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dAllowed"] = oLineTransactions[i].AllowedCharges;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nProvider"] = oLineTransactions[i].RefferingProviderId;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nClinicID"] = oLineTransactions[i].ClinicID;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionLineStatus"] = oLineTransactions[i].LineStatus.GetHashCode();
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nClaimNumber"] = oLineTransactions[i].ClaimNumber;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsLabCPT"] = oLineTransactions[i].IsLabCPT;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sAuthorizationNo"] = oLineTransactions[i].AuthorizationNo;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bSentToClaim"] = oLineTransactions[i].SendToClaim;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sLinePrimaryDxCode"] = oLineTransactions[i].LinePrimaryDxCode;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sLinePrimaryDxDesc"] = oLineTransactions[i].LinePrimaryDxDesc;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nClaimLineStatusID"] = 0;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sClaimLineStatus"] = oLineTransactions[i].LineStatus;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nSendToFlag"] = 0;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nChargesDayTrayID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nChargesDayTrayID"];
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sChargesTrayCode"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sChargesTrayCode"];
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sChargesTrayDescription"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sChargesTrayDescription"];
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nIsTrayClose"] = false;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dPatientResponsibility"] = oLineTransactions[i].PatientResponsibility;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsHold"] = oLineTransactions[i].IsHold;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sHoldReason"] = oLineTransactions[i].HoldReason;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dInsurancePending"] = 0;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dBilliedAmount"] = oLineTransactions[i].BilledAmount;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nVoidCloseDate"] = DBNull.Value;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nVoidTrayID"] = DBNull.Value;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nParentTransactionID"] = oLineTransactions[i].ParentTransactionID;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nParentTransactionDetailID"] = oLineTransactions[i].ParentTransactionDetailID;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsSplitted"] = oLineTransactions[i].IsLineSplitted;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nFeeScheduleType"] = oLineTransactions[i].FeeScheduleType;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nFeeScheduleID"] = oLineTransactions[i].FeeScheduleID;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nFacilityType"] = oLineTransactions[i].FacilityType;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nNDCID"] = oLineTransactions[i].NDCID;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sNDCCodeQualifier"] = oLineTransactions[i].NDCCodeQualifier;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sNDCCode"] = oLineTransactions[i].NDCCode;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sNDCDescription"] = oLineTransactions[i].NDCDescription;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sNDCUnitCode"] = oLineTransactions[i].NDCUnitCode;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sNDCUnitDescription"] = oLineTransactions[i].NDCUnitDescription;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sNDCUnit"] = oLineTransactions[i].NDCUnit;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sNDCUnitPricing"] = oLineTransactions[i].NDCUnitPricing;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bEMG"] = oLineTransactions[i].EMG;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sPrescription"] = oLineTransactions[i].Prescription;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sPrescriptionDescription"] = oLineTransactions[i].PrescriptionDescription;
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsSelfClaim"] = oLineTransactions[i].bIsSelfClaim;
                    var count = (from _dt in _dtRevenueCode.AsEnumerable()
                                 where _dt.Field<string>("sCPTCode") == oLineTransactions[i].CPTCode.Replace("'", "''")
                                 select _dt).Count();

                    if (count > 0)
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sRevenueCode"] = Convert.ToString(_dtRevenueCode.Select("sCPTCode ='" + oLineTransactions[i].CPTCode.Replace("'", "''") + "'")[0]["sRevenueCode"]);
                    }
                    else
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sRevenueCode"] = "";
                    }

                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sRowStateType"] = "";

                    if (oLineTransactions[i] != null)
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsServiceScreening"] = oLineTransactions[i].ServiceIsTheScreening;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsServiceResultofScreening"] = oLineTransactions[i].ServiceIsTheResultOfScreening;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsFamilyPlanningIndicator"] = oLineTransactions[i].ServiceFamilyPlanningIndicator;

                        if (oLineTransactions[i].bIsAneshtesia)
                        {
                            if (Convert.ToDateTime(oLineTransactions[i].AnesthesiaStartTime).Date.ToShortDateString() =="1/1/0001")
                            {
                                dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dtANSStartDateTime"] = DBNull.Value;
                                dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dtANSEndDateTime"] = DBNull.Value;
                            }
                            else
                            {
                                dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dtANSStartDateTime"] = oLineTransactions[i].AnesthesiaStartTime;
                                dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dtANSEndDateTime"] = oLineTransactions[i].AnesthesiaEndTime;
                            }
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nANSTotalMinutes"] = oLineTransactions[i].AnesthesiaTotalMinutes;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSMinPerUnit"] = oLineTransactions[i].AnesthesiaMinPerUnit;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSTimeUnits"] = oLineTransactions[i].AnesthesiaTimeUnits;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSBaseUnits"] = oLineTransactions[i].AnesthesiaBaseUnits;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSOtherUnits"] = oLineTransactions[i].AnesthesiaOtherUnits;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSTotalUnits"] = oLineTransactions[i].AnesthesiaTotalUnits;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsAutoCalculate"] = oLineTransactions[i].bIsAutoCalculateAnesthesia;
                        }
                        else
                        {
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dtANSStartDateTime"] = DBNull.Value;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dtANSEndDateTime"] = DBNull.Value;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nANSTotalMinutes"] = DBNull.Value;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSMinPerUnit"] = DBNull.Value;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSTimeUnits"] = DBNull.Value;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSBaseUnits"] = DBNull.Value;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSOtherUnits"] = DBNull.Value;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSTotalUnits"] = DBNull.Value;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsAutoCalculate"] = DBNull.Value;
                        }
               
                    }
                    else
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsServiceScreening"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsServiceResultofScreening"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsFamilyPlanningIndicator"] = DBNull.Value;


                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dtANSStartDateTime"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dtANSEndDateTime"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nANSTotalMinutes"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSMinPerUnit"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSTimeUnits"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSBaseUnits"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSOtherUnits"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["dANSTotalUnits"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["bIsAutoCalculate"] = DBNull.Value;
               

                    }

                    #region " Notes "

                    if (oLineTransactions[i].LineNotes.Count > 0)
                    {
                        for (int j = 0; j < oLineTransactions[i].LineNotes.Count; j++)
                        {

                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows.Add();

                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nTransactionID"] = 0;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nLineNo"] = oLineTransactions[i].TransactionLineId;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nTransactionDetailID"] = dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterDetailID"];
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nNoteType"] = oLineTransactions[i].LineNotes[j].NoteType;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nNoteId"] = 0;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nNoteDateTime"] = DBNull.Value;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["sNoteDescription"] = oLineTransactions[i].LineNotes[j].NoteDescription;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nUserID"] = oLineTransactions[i].LineNotes[j].UserID;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nClinicID"] = oLineTransactions[i].LineNotes[j].ClinicID;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nCloseDate"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionDate"];//Needs To be implemented
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nBillingNoteType"] = oLineTransactions[i].LineNotes[j].BillingNoteType;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nStatementNoteDate"] = oLineTransactions[i].LineNotes[j].StatementNoteDate;
                            dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["dtCreatedDateTime"] = oLineTransactions[i].LineNotes[j].dtCreatedDatetime;
                            _NoteIndex = _NoteIndex + 1;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Add, "Note added to trasanction line, Line No : " + oLineTransactions[i].TransactionLineId.ToString() + " of Claim # :" + txtClaimNo.Text + ". ", PatientID, _TransactionID, _PatientPoviderID, ActivityOutCome.Success);
                        }

                    }

                    #endregion

                    #region " Next Action "

                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows.Add();

                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nID"] = 0;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nClaimNo"] = oLineTransactions[i].ClaimNumber;//dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nContactID"]
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nEOBPaymentID"] = 0;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nEOBID"] = 0;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nEOBPaymentDetailID"] = 0;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nBillingTransactionID"] = 0;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nBillingTransactionDetailID"] = dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterDetailID"];
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nLineNo"] = i + 1;

                    //dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPatientInsID"] = oLineTransactions[i].InsuranceID;
                    //dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPatientInsName"] = _InsuranceName;
                    //dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPartyNumber"] = 1;
                    //dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionContactID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nContactID"];
                    //dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextPartyType"] = oLineTransactions[i].InsuranceSelfMode.GetHashCode();

                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPatientInsID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nInsuranceID"];
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPatientInsName"] = _InsuranceName;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPartyNumber"] = 1;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionContactID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nContactID"];
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextPartyType"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nResponsibilityType"];

                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sNextActionCode"] = "T";
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sNextActionDescription"] = "Transacted";
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["dNextActionAmount"] = oLineTransactions[i].Total;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nClinicID"] = oLineTransactions[i].ClinicID;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["bIsVoid"] = false;

                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text);
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nUserID"] = this.UserID;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["dtDate"] = DBNull.Value;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sUserName"] = this.UserName;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sSubClaimNo"] = "";
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nTrackMstTrnID"] = 0;
                    dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nTrackMstTrnDetailID"] = dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionDetailID"];

                    #endregion

                }
                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].AcceptChanges();
                dsChargesTVP.Tables["BL_EOB_NextAction"].AcceptChanges();
                dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].AcceptChanges();
            }

            if (oLineTransactions != null)
            {
                oLineTransactions.Dispose();
                oLineTransactions = null;
            }
            if (_dtLinesUniqueIds != null)
            {
                _dtLinesUniqueIds.Dispose();
                _dtLinesUniqueIds = null;
            } 
            if (_dtRevenueCode != null)
            {
                _dtRevenueCode.Dispose();
                _dtRevenueCode = null;
            };
        }

        public void SetDXClaimDetails_TVP(dsChargesTVP dsChargesTVP)
        {

          //  gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            string _dxCode = "";
            string _dxDesc = "";
            bool _isPrimary = false;
            bool _isSelected = false;
            Int64 _serialNo = 0;

            try
            {

                //**Delete the existing entry for the transaction
                //_sqlQuery = "delete from BL_Transaction_Diagnosis where nTransactionID = " + TransactionId + "";

                _serialNo = gloCharges.GetMaxSerialNoForDx();

                int rowNo = 0;
                for (int i = 1; i < c1Dx.Rows.Count; i++)
                {
                    _serialNo = _serialNo + 1;

                    int _rowIndex = i;
                    _dxCode = Convert.ToString(c1Dx.GetData(_rowIndex, COL_DX_CODE));
                    _dxDesc = Convert.ToString(c1Dx.GetData(_rowIndex, COL_DX_DESC));

                    if (c1Dx.GetCellCheck(_rowIndex, COL_DX_SELECT) == CheckEnum.Checked)
                    { _isSelected = true; }
                    else
                    { _isSelected = false; }

                    if (c1Dx.GetCellCheck(_rowIndex, COL_DX_ISPRIMARY) == CheckEnum.Checked)
                    { _isPrimary = true; }
                    else
                    { _isPrimary = false; }

                    rowNo = i - 1;

                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows.Add();

                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nTransactionID"] = 0;
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nVisitID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nVisitID"];
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nClaimNo"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClaimNo"];
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nPatientID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nPatientID"];
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nSerialNo"] = _serialNo; //Has to generate automatically - Primary Key
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["sDx1Code"] = _dxCode.Replace("'", "''");
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["sDx1Description"] = _dxDesc.Replace("'", "''");
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nClinicID"] = this.ClinicID;
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["bIsClaimDx"] = _isSelected;
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["bIsPrimaryDx"] = _isPrimary;
                }

                dsChargesTVP.Tables["BL_Transaction_Diagnosis"].AcceptChanges();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
          //      if (oDB != null) { oDB.Dispose(); }
            }
        }

        public void SetUserWiseCloseDay_TVP(dsChargesTVP dsChargesTVP, string nCloseDayDate, CloseDayType eType)
        {
            try
            {
                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].Rows.Add();

                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].Rows[0]["nCloseDayID"] = 0;
                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].Rows[0]["nCloseDayDate"] = gloDateMaster.gloDate.DateAsNumber(nCloseDayDate);
                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].Rows[0]["nCloseDayType"] = (int)eType.GetHashCode();
                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].Rows[0]["dtCloseDateTime"] = DBNull.Value;
                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].Rows[0]["nUserID"] = this.UserID;
                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].Rows[0]["sUserName"] = _UserName;
                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].Rows[0]["nClinicID"] = this.ClinicID;
                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].Rows[0]["bIsActive"] = true;

                dsChargesTVP.Tables["UserWise_CloseDateAndTray"].AcceptChanges();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

            }
        }

        public void SetBox19Notes_TVP(dsChargesTVP dsChargesTVP)
        {
            try
            {
                if (_oBox19Notes != null)
                {
                    if (_oBox19Notes.Count > 0)
                    {
                        //Get the count from the datatable if notes has been added already and then Insert the Box 19 Notes in the same datatable
                        int _Box19Count = Convert.ToInt16(dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows.Count);

                        for (int j = 0; j < _oBox19Notes.Count; j++)
                        {
                            if (_oBox19Notes[j].Box19NoteDescription.Trim() != null && _oBox19Notes[j].Box19NoteDescription.Trim() != string.Empty)
                            {

                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows.Add();

                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nTransactionID"] = 0;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nLineNo"] = 0;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nTransactionDetailID"] = 0;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nNoteType"] = _oBox19Notes[j].NoteType;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nNoteId"] = 0;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nNoteDateTime"] = _oBox19Notes[j].NoteDate;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["sNoteDescription"] = _oBox19Notes[j].Box19NoteDescription;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nUserID"] = _oBox19Notes[j].UserID;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nClinicID"] = _oBox19Notes[j].ClinicID;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nCloseDate"] = DBNull.Value;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nBillingNoteType"] = _oBox19Notes[j].BillingNoteType;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nStatementNoteDate"] = DBNull.Value;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["dtCreatedDateTime"] = DBNull.Value;
                                _Box19Count = _Box19Count + 1;

                            }
                        }
                    }

                    if (_oBox19Notes != null)
                        _oBox19Notes.Clear();
                    if (_oBox19Note != null)
                        _oBox19Note = null;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //..Do not dispose _oBox19Notes objects
            }

        }

        public void SetClaimBox10dNotes_TVP(dsChargesTVP dsChargesTVP)
        {
            try
            {
                if (_sClaimBox10dNote != null)
                {
                   if (_sClaimBox10dNote != null && _sClaimBox10dNote.Trim() != string.Empty)
                    {
                        int _Box19Count = Convert.ToInt16(dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows.Count);
                       
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows.Add();
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nTransactionID"] = 0;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nLineNo"] = 0;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nTransactionDetailID"] = 0;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nNoteType"] = NoteType.Claim_Box10dNote;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nNoteId"] = 0;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nNoteDateTime"] = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString());
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["sNoteDescription"] = _sClaimBox10dNote;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nCloseDate"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nBillingNoteType"] = Convert.ToInt32(EOBPaymentSubType.Claim_Box10dNote);
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["nStatementNoteDate"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_Box19Count]["dtCreatedDateTime"] = DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }

        }

        public void SetUB04Data_TVP(dsChargesTVP dsChargesTVP)
        {
            try
            {
                dsChargesTVP.Tables["UB04Data"].Rows.Add();

                dsChargesTVP.Tables["UB04Data"].Rows[0]["nTransactionMasterID"] = 0;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nTransactionID"] = 0;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nClaimNo"] = Convert.ToInt64(txtClaimNo.Text);
                if (oUB.sTypeofbill == null || Convert.ToString(oUB.sTypeofbill) == "")
                {
                    if (oUB.TypeofbillDeleted == false)
                        dsChargesTVP.Tables["UB04Data"].Rows[0]["sTypeofbill"] = TypeOfBill;
                }
                else
                {
                    if (oUB.TypeofbillDeleted == false)
                        dsChargesTVP.Tables["UB04Data"].Rows[0]["sTypeofbill"] = oUB.sTypeofbill;
                }
                
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode01"] = oUB.sConditionCode01;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode02"] = oUB.sConditionCode02;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode03"] = oUB.sConditionCode03;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode04"] = oUB.sConditionCode04;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode05"] = oUB.sConditionCode05;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode06"] = oUB.sConditionCode06;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode07"] = oUB.sConditionCode07;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode08"] = oUB.sConditionCode08;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode09"] = oUB.sConditionCode09;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode10"] = oUB.sConditionCode10;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode11"] = oUB.sConditionCode11;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sConditionCode12"] = DBNull.Value;

                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceCode01"] = oUB.sOccurrenceCode01;

                if (oUB.sOccurrenceDate01 == null || Convert.ToString(oUB.sOccurrenceDate01) == "")
                {
                    if (oUB.MinDOSDeleted == false && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                        dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate01"] = gloDateMaster.gloDate.DateAsDate(UC_gloBillingTransactionLines.GetMinDOS());
                }
                else
                {
                    if (oUB.MinDOSDeleted == false)
                        dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate01"] = oUB.sOccurrenceDate01;
                }               

                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceCode02"] = oUB.sOccurrenceCode02;
                if (Convert.ToString(oUB.sOccurrenceDate02) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate02"] = oUB.sOccurrenceDate02;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate02"] = DBNull.Value;
                }

                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceCode03"] = oUB.sOccurrenceCode03;
                if (Convert.ToString(oUB.sOccurrenceDate03) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate03"] = oUB.sOccurrenceDate03;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate03"] = DBNull.Value;
                }

                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceCode04"] = oUB.sOccurrenceCode04;
                if (Convert.ToString(oUB.sOccurrenceDate04) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate04"] = oUB.sOccurrenceDate04;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate04"] = DBNull.Value;
                }


                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceCode05"] = oUB.sOccurrenceCode05;
                if (Convert.ToString(oUB.sOccurrenceDate05) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate05"] = oUB.sOccurrenceDate05;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate05"] = DBNull.Value;
                }




                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceCode06"] = oUB.sOccurrenceCode06;
                if (Convert.ToString(oUB.sOccurrenceDate06) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate06"] = oUB.sOccurrenceDate06;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate06"] = DBNull.Value;
                }




                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceCode07"] = oUB.sOccurrenceCode07;
                if (Convert.ToString(oUB.sOccurrenceDate07) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate07"] = oUB.sOccurrenceDate07;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate07"] = DBNull.Value;
                }




                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceCode08"] = oUB.sOccurrenceCode08;
                if (Convert.ToString(oUB.sOccurrenceDate08) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate08"] = oUB.sOccurrenceDate08;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceDate08"] = DBNull.Value;
                }


                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceSpanCode01"] = oUB.sOccurrenceSpanCode01;
                if (Convert.ToString(oUB.sOccurrenceFromSpanDate01) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceFromSpanDate01"] = oUB.sOccurrenceFromSpanDate01;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceFromSpanDate01"] = DBNull.Value;
                }
                if (Convert.ToString(oUB.sOccurrenceTOSpanDate01) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceToSpanDate01"] = oUB.sOccurrenceTOSpanDate01;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceToSpanDate01"] = DBNull.Value;
                }


                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceSpanCode02"] = oUB.sOccurrenceSpanCode02;
                if (Convert.ToString(oUB.sOccurrenceFromSpanDate02) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceFromSpanDate02"] = oUB.sOccurrenceFromSpanDate02;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceFromSpanDate02"] = DBNull.Value;
                }
                if (Convert.ToString(oUB.sOccurrenceToSpanDate02) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceToSpanDate02"] = oUB.sOccurrenceToSpanDate02;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceToSpanDate02"] = DBNull.Value;
                }



                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceSpanCode03"] = oUB.sOccurrenceSpanCode03;
                if (Convert.ToString(oUB.sOccurrenceFromSpanDate03) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceFromSpanDate03"] = oUB.sOccurrenceFromSpanDate03;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceFromSpanDate03"] = DBNull.Value;
                }
                if (Convert.ToString(oUB.sOccurrenceToSpanDate03) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceToSpanDate03"] = oUB.sOccurrenceToSpanDate03;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceToSpanDate03"] = DBNull.Value;
                }



                dsChargesTVP.Tables["UB04Data"].Rows[0]["sOccurrenceSpanCode04"] = oUB.sOccurrenceSpanCode04;
                if (Convert.ToString(oUB.sOccurrenceFromSpanDate04) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceFromSpanDate04"] = oUB.sOccurrenceFromSpanDate04;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceFromSpanDate04"] = DBNull.Value;
                }
                if (Convert.ToString(oUB.sOccurrenceToSpanDate04) != "")
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceToSpanDate04"] = oUB.sOccurrenceToSpanDate04;
                }
                else
                {
                    dsChargesTVP.Tables["UB04Data"].Rows[0]["dtOccurrenceToSpanDate04"] = DBNull.Value;
                }




                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode01"] = oUB.sValueCode01;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount01"] = oUB.nValueAmount01;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode02"] = oUB.sValueCode02;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount02"] = oUB.nValueAmount02;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode03"] = oUB.sValueCode03;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount03"] = oUB.nValueAmount03;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode04"] = oUB.sValueCode04;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount04"] = oUB.nValueAmount04;//Convert.ToDecimal(c1ValueCode.Cols[3][4]);
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode05"] = oUB.sValueCode05;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount05"] = oUB.nValueAmount05;// Convert.ToDecimal(c1ValueCode.Cols[3][5]);
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode06"] = oUB.sValueCode06;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount06"] = oUB.nValueAmount06;// Convert.ToDecimal(c1ValueCode.Cols[3][6]);
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode07"] = oUB.sValueCode07;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount07"] = oUB.nValueAmount07;//Convert.ToDecimal(c1ValueCode.Cols[3][7]);
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode08"] = oUB.sValueCode08;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount08"] = oUB.nValueAmount08;//Convert.ToDecimal(c1ValueCode.Cols[3][8]);  
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode09"] = oUB.sValueCode09;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount09"] = oUB.nValueAmount09;//Convert.ToDecimal(c1ValueCode.Cols[3][8]);  
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode10"] = oUB.sValueCode10;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount10"] = oUB.nValueAmount10;//Convert.ToDecimal(c1ValueCode.Cols[3][8]);  
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode11"] = oUB.sValueCode11;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount11"] = oUB.nValueAmount11;//Convert.ToDecimal(c1ValueCode.Cols[3][8]);  
                dsChargesTVP.Tables["UB04Data"].Rows[0]["sValueCode12"] = oUB.sValueCode12;
                dsChargesTVP.Tables["UB04Data"].Rows[0]["nValueAmount12"] = oUB.nValueAmount12;//Convert.ToDecimal(c1ValueCode.Cols[3][8]);  

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //..Do not dispose _oBox19Notes objects
            }

        }

        #endregion " TVP Charge Save Method "

        public gloGlobal.gloICD.CodeRevision GetICDCodeType(Int64 nContactID, Int64 nFromDOS)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                oParameters = new gloDatabaseLayer.DBParameters();
                object BillingType;
                oParameters.Add("@nContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nFromDOS", nFromDOS, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                BillingType = oDB.ExecuteScalar("BL_GetICDCodeType", oParameters);
                oDB.Disconnect();
                if (Convert.ToInt16(BillingType) == 10)
                    return gloGlobal.gloICD.CodeRevision.ICD10;
                else if (Convert.ToInt16(BillingType) == 9)
                    return gloGlobal.gloICD.CodeRevision.ICD9;
                return gloGlobal.gloICD.CodeRevision.ICD9;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                ex = null;
                return 0;
             }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                    
                }
            }

        }

        #region "Patient Appointments linking to Charges"
        [DefaultValue(false)]
        private bool PatientHasAppointments { get; set; }

        //[DefaultValue(false)]
        //private bool PatientAppointmentsAreSet { get; set; }

        private void SetPatientAppointmentsLinked()
        {
            txtPatientAppointments.Text = "<Linked>";
            txtPatientAppointments.TextAlign = HorizontalAlignment.Center;
            txtPatientAppointments.ForeColor = Color.Maroon;
        }

        private void SetPatientAppointmentsAvailable()
        {
            txtPatientAppointments.Text = "<Available>";
            txtPatientAppointments.TextAlign = HorizontalAlignment.Center;
            txtPatientAppointments.ForeColor = Color.Maroon;
        }

        private void ResetPatientAppointments()
        { 
            txtPatientAppointments.Text = "";            
        }

        private void CheckForPriorAppointments()
        {
            try
            {
                if (this.lstAppointmentIDs.Any())
                { this.SetPatientAppointmentsLinked(); }
                else 
                {
                    DataTable dtPatientAppointments = this.GetPatientAppointments();                                        

                    if (dtPatientAppointments != null && dtPatientAppointments.Rows.Count > 0)
                    {
                        this.PatientHasAppointments = true;
                        this.SetPatientAppointmentsAvailable(); 
                    } 
                    else 
                    {
                        this.PatientHasAppointments = false;
                        this.ResetPatientAppointments(); 
                    }

                    if (dtPatientAppointments != null)
                    {
                        dtPatientAppointments.Dispose();
                        dtPatientAppointments = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  

        private void CheckForPriorAppointments(DataTable AppointmentsData)
        {
            try
            {
                if (AppointmentsData.Rows.Count > 1)
                {
                    this.PatientHasAppointments = true;
                    this.SetPatientAppointmentsAvailable(); 
                }
                else if (AppointmentsData.Rows.Count == 1)
                {
                    Int64 nAppointmentID = 0;
                    this.PatientHasAppointments = true;
                    if (Int64.TryParse(Convert.ToString(AppointmentsData.Rows[0]["ID"]), out nAppointmentID))
                    {
                        if (gloGlobal.gloPMGlobal.EnableAppointmentLinkingToCharges)
                        {
                            this.SetPatientAppointmentsLinked();
                            this.lstAppointmentIDs.Add(nAppointmentID);
                        }
                        else
                        {
                            this.PatientHasAppointments = true;
                            this.SetPatientAppointmentsAvailable();
                        }                        
                    }
                }
                else 
                {
                    this.PatientHasAppointments = false;
                    this.ResetPatientAppointments(); 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckForPriorAppointments(Int64 nAppointmentsID)
        {
            try
            {
                if (nAppointmentsID > 0)
                {
                       this.PatientHasAppointments = true;
                       if (gloGlobal.gloPMGlobal.EnableAppointmentLinkingToCharges)
                        {
                            this.SetPatientAppointmentsLinked();
                            this.lstAppointmentIDs.Add(nAppointmentsID);
                        }
                        else
                        {
                            this.PatientHasAppointments = true;
                            this.SetPatientAppointmentsAvailable();
                        }
                  
                }
                else
                {
                    this.PatientHasAppointments = false;
                    this.ResetPatientAppointments();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillPatientAppointmentsOnLoad()
        {
            DataTable dtPatientAppointments = null;

            try
            {
                if (this.lstAppointmentIDs == null) { this.lstAppointmentIDs = new List<long>(); }
                if (this.ListOfPatientAppointments == null) { this.ListOfPatientAppointments = new List<PatientAppointment>(); }

                dtPatientAppointments = this.GetPatientAppointments();
                this.CheckForPriorAppointments(dtPatientAppointments);
                this.LoadAppointmentsInList(dtPatientAppointments);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dtPatientAppointments != null)
                {
                    dtPatientAppointments.Dispose();
                    dtPatientAppointments = null;
                }
            }
        }

        private void FillPatientAppointments()
        {
            DataTable dtPatientAppointments = null;

            try
            {
                if (this.lstAppointmentIDs == null) { this.lstAppointmentIDs = new List<long>(); }                

                dtPatientAppointments = this.GetPatientAppointments();
                this.CheckForPriorAppointments(dtPatientAppointments);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                if (dtPatientAppointments != null)
                {
                    dtPatientAppointments.Dispose();
                    dtPatientAppointments = null;
                }
            }
           
        }

        private void RecheckPatientAppointments()
        {
            try
            {
                if (this.lstAppointmentIDs != null)
                {
                    this.lstAppointmentIDs.Clear();
                }
                if (this.ListOfPatientAppointments != null)
                { 
                    this.ListOfPatientAppointments.Clear();
                }
                this.FillPatientAppointmentsOnLoad();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        #region "Online Charges"

        private void LoadOnlineCharge(HitTestInfo hitInfo)
        {
            
            _bDxFlag = true;
            _bOnlineClaimPostingLoading = true;
            IsOnlineChargeBind = false;
            string sMessage = string.Empty;
            //Int64 _PortalClaimID = 0;
            //Int64 _OnlinePatientID = 0;
            //Int64 _OnlineProviderID = 0;
            //String _OnlineChargetype = "";
            gloBilling ogloBillling = null;
            try
            {
                ogloBillling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
                #region "Get selected charge details"
                _PortalClaimID = Convert.ToInt64(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["nPortalClaimID"].Index));
                _OnlineChargetype = Convert.ToString(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["Type"].Index));


                if (c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["nPatientID"].Index) == DBNull.Value)
                {
                    Int64 nNewPatientID;
                    nNewPatientID = ogloBillling.GetOnlineChargePatient(_PortalClaimID);
                    _OnlinePatientID = nNewPatientID;
                    gloPatient.frmSetupPatient frmPatient = new gloPatient.frmSetupPatient(nNewPatientID, _DatabaseConnectionString);
                    frmPatient.ShowDialog(this);
                }
                else
                {
                    _OnlinePatientID = Convert.ToInt64(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["nPatientID"].Index));
                }




                _OnlineProviderID = Convert.ToInt64(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["nProviderID"].Index));
                _OnlineFacilityID = Convert.ToInt64(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["nFacilityID"].Index));
                _OnlineClaimNote = Convert.ToString(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["ClaimNote"].Index));
                _OnlineHospFromDate = Convert.ToString(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["dtHospFromDate"].Index));
                _OnlineHospToDate = Convert.ToString(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["dtHospToDate"].Index));

                //string _PatientCode = Convert.ToString(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["PatientCode"].Index));
                //string _PatientName = Convert.ToString(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["PatientName"].Index));
                string _Provider = Convert.ToString(c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["ProviderName"].Index));
                Object _oDOS = c1OnlineCharge.GetData(hitInfo.Row, c1OnlineCharge.Cols["PostingDate"].Index);
                string _DOS = "";
                if (_DOS != null && Convert.ToString(_oDOS).Trim() != String.Empty && Convert.ToString(_oDOS).Trim().Length >= 8)
                {
                    _DOS = string.Format("{0:MM/dd/yyyy}", _oDOS);
                }
                else
                {
                    _DOS = Convert.ToString(_oDOS);
                }

                #endregion "Get selected charge details"
                
                if (_PatientID != _OnlinePatientID)
                    this._PatientID = _OnlinePatientID;
                txtPriorAuthorizationNo.Text = "";
                txtPriorAuthorizationNo.Tag = "";
                SetPatientChangeData(true);

                if (gloGlobal.gloPMGlobal.ViewDocumentsOnCharges)
                {
                    clsSplit_PatientCharges.FillDocuments(this._PatientID, gloGlobal.gloPMGlobal.ClinicID);
                }
                else
                {
                    if (_OnlineProviderID > 0)
                    {
                        DataTable dtProviderSettings = GetSettingsForExamProvider(_OnlineProviderID);
                        if (dtProviderSettings != null && dtProviderSettings.Rows.Count > 0)
                        {
                            SetProviderSettings(dtProviderSettings);
                        }
                        if (dtProviderSettings != null)
                        {
                            dtProviderSettings.Dispose();
                            dtProviderSettings = null;
                        }
                    }
                    c1Dx.Rows.Count = 1;
                }
                ShowHideControls(ShowHideType.LoadOnlineCharge);
                SetHoldnMoreClaimDataMesseges();
                SetLastGlobalPeriods();
                CheckForEPSDTEnabled();
                IsAnesthesiaEnabled();
                ShowHideUB();
                if (UC_gloBillingTransactionLines != null)
                {
                    cmbFacility.SelectedValue = _OnlineFacilityID;
                    UC_gloBillingTransactionLines.PatientID = this.PatientID;
                    UC_gloBillingTransactionLines.PatientProviderID = _OnlineProviderID;
                    LoadDefaultBillingSettings();
                    UC_gloBillingTransactionLines.ReinitilizeControl();
                    UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                    SetFacilitySettingsData();
                    UC_gloBillingTransactionLines.TreatmentType = _nEMRTreatmentType;
                }

                #region "set Online Dos in case of CPT is Empty"
                if (IsValidDate(_DOS) == true)
                {
                    if (UC_gloBillingTransactionLines != null)
                    {
                        if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                        {
                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(_DOS));
                        }
                    }
                }
                #endregion"Set Close date To added line"
                pnlOnlineCharge.SendToBack();
                panel6.Visible = true;
                SelectPrimaryInsurance();

                Int64 _ContactId = 0;
                if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                {
                    _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                }
                ogloBillling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxServiceLines;
                UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;
                UC_gloBillingTransactionLines._nContactID = _ContactId;

                #region " Check EMR Treatment Present or Not "

                ////sMessage = ValidateEMRExam();
                //sMessage = gloCharges.ValidateEMRExam(_EMRExamID, _IsICD9Driven, _NoOfMaxDiagnosis, _NoOfMaxServiceLines, _nEMRTreatmentType);

                //if (sMessage.Trim() != String.Empty)
                //{
                //    if (MessageBox.Show("EMR Treatment requires manual entry.                 " + Environment.NewLine + sMessage + Environment.NewLine + Environment.NewLine + "Treatment Details will be displayed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                //    {
                //        SetEMRTreatment(_EMRExamID);

                //        oEMRTransLinesSplit = UC_gloBillingTransactionLines.GetLineTransactions();

                //        pnlExamCPTDX.SendToBack();
                //        pnlExamCPTDX.Location = new System.Drawing.Point(320, 130);
                //        if (_nEMRTreatmentType == gloSettings.ExternalChargesType.gloEMRTreatment)
                //        {
                //            if (_IsICD9Driven)
                //            { LoadDXGrid(); }
                //            else
                //            { LoadCPTGrid(); }
                //        }
                //        else if (_nEMRTreatmentType == gloSettings.ExternalChargesType.HL7InboundCharges)
                //        {
                //            //LoadInboundDXGrid();
                //            LoadCPTGrid();
                //        }
                //        pnlExamCPTDX.BringToFront();
                //    }
                //}
                //else
                //{
                //    SetEMRTreatment(_EMRExamID);
                //}
                #endregion

                #region"Check and Load EMR Treatment"
                setOnlineCharge(_PortalClaimID);
                #endregion
                if (UC_gloBillingTransactionLines != null)
                {
                    this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                    #region "Set Line Primary DX"
                    if (UC_gloBillingTransactionLines.HasPrimaryDx(UC_gloBillingTransactionLines.CurrentTransactionLine) == true)
                    {
                        int rowIndex = UC_gloBillingTransactionLines.CurrentTransactionLine;
                        string _primaryDxCode = UC_gloBillingTransactionLines.GetRowPrimaryDxCode(rowIndex);

                        if (_primaryDxCode != "")
                        {
                            if (c1Dx != null && c1Dx.Rows.Count > 0)
                            {
                                for (int i = 1; i <= c1Dx.Rows.Count - 1; i++)
                                {
                                    if (Convert.ToString(c1Dx.GetData(i, COL_DX_CODE)) != "")
                                    {
                                        if (Convert.ToString(c1Dx.GetData(i, COL_DX_CODE)).Trim() == _primaryDxCode.Trim())
                                        {
                                            c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Checked);
                                        }
                                        else
                                        {
                                            c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Unchecked);
                                        }
                                    }
                                }
                            }
                        }
                    }


                    else
                    {
                        if (c1Dx != null && c1Dx.Rows.Count > 0)
                        {
                            for (int i = 1; i <= c1Dx.Rows.Count - 1; i++)
                            {
                                c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Unchecked);
                            }
                        }
                    }
                    #endregion "set Line Primary DX"
                    this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                    UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                    SetFacilitySettingsData();



                    #region "Set Fee Schedule"
                    UC_gloBillingTransactionLines.FeeScheduleID = 0;
                    UC_gloBillingTransactionLines.Fee_ScheduleID = 0;
                    UC_gloBillingTransactionLines.SetFNFCharges();

                    if (UC_gloBillingTransactionLines.Fee_ScheduleID > 0)
                    {
                        _FeeScheduleID = UC_gloBillingTransactionLines.Fee_ScheduleID;
                        cmbFeeSchedule.SelectedValue = _FeeScheduleID;
                    }
                    else
                    {
                        if (cmbFeeSchedule.Items.Count > 0)
                        {
                            _DefaultFeeScheduleID = gloCharges.GetProviderFeeScheduleID(_PatientPoviderID);
                            if (_DefaultFeeScheduleID == 0)
                            {
                                _DefaultFeeScheduleID = gloCharges.GetClinicFeeScheduleID();
                            }
                            if (_DefaultFeeScheduleID > 0)
                                cmbFeeSchedule.SelectedValue = _DefaultFeeScheduleID;
                            else
                                cmbFeeSchedule.SelectedValue = 0;
                            _FeeScheduleID = Convert.ToInt64(cmbFeeSchedule.SelectedValue);
                            UC_gloBillingTransactionLines.FeeScheduleID = 0;
                            UC_gloBillingTransactionLines.Fee_ScheduleID = 0;
                        }
                    }
                    chkFeeSchedule.Checked = false;
                    #endregion
                    if (UC_gloBillingTransactionLines.FacilityPOS > 0)
                    {
                        if (UC_gloBillingTransactionLines != null)
                        {
                            UC_gloBillingTransactionLines.SetFacilityPOS();
                        }
                    }
                    this.lstAppointmentIDs.Clear();
                    this.ListOfPatientAppointments.Clear();
                    this.FillPatientAppointmentsOnLoad();
                }
                
                DataTable dtClaimDetails = GetRCMDocDetails(_PortalClaimID, 3);
                if (dtClaimDetails!=null&& dtClaimDetails.Rows.Count>0)
                {
                    gloSettings.GeneralSettings oSettings = new GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    object objOCPDefaultRCMDOC = new object();
                    oSettings.GetSetting("OCPDEFAULTRCMCATEGORY", 0, gloGlobal.gloPMGlobal.ClinicID, out objOCPDefaultRCMDOC);
                    oSettings.Dispose();
                    oSettings = null;
                    string sRCMCategoryName = Convert.ToString(objOCPDefaultRCMDOC);
                    foreach (DataRow dr in dtClaimDetails.Rows)
                    {
                        if (Convert.ToBoolean(dr["bIsAttchmentPresent"]) == true)
                        {
                            string strMsg = string.Format("Selected claim is submitted with \"{0}\" document.\nThis document will be moved to \"{1}\" RCM DOC category after claim saved.", Convert.ToString(dr["sFileName"]), sRCMCategoryName);
                            MessageBox.Show(strMsg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    } 
                }
            }


            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                _bDxFlag = false;
                _bOnlineClaimPostingLoading = false;
                //if(ogloBilling!=null)
                //{
                //    ogloBilling.Dispose();
                //}
            }
        }

        private void BindOnlineCharges()
        {
            IsOnlineChargeBind = true;
            DataTable dtOnlineCharge = null;
            DataView _dv = null;
            int nTotalOnlineClaim = 0;
            try
            {
                c1OnlineCharge.Visible = false;
                if (c1OnlineCharge != null)
                {
                    c1OnlineCharge.DataSource = null;
                }
                dtOnlineCharge = gloCharges.GetOnlineChargesList();
                if (dtOnlineCharge != null)
                {
                    if (dtOnlineCharge.Rows.Count > 0)
                    {
                        _dv = dtOnlineCharge.Copy().DefaultView;
                        //dtOnlineCharge.Dispose();
                        //dtOnlineCharge = null;
                        nTotalOnlineClaim = DesignOCPChargeGrid(_dv);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                lblOCPCount.Text = "Online Charge Count :" + Convert.ToString(nTotalOnlineClaim);
                c1OnlineCharge.Visible = true;
                if (c1OnlineCharge != null && c1OnlineCharge.ScrollBars == ScrollBars.None) { c1OnlineCharge.ScrollBars = ScrollBars.Vertical; }

            }
        }

        private int DesignOCPChargeGrid(DataView _dv)
        {
            int nTotalOnlineClaim = 0;

            try
            {
                c1OnlineCharge.BeginUpdate();
                c1OnlineCharge.AutoResize = false;
                c1OnlineCharge.Redraw = false;
                c1OnlineCharge.DataSource = _dv;
                c1OnlineCharge.Redraw = true;
                c1OnlineCharge.AutoResize = true;

                //,c1OnlineCharge.Cols["PortalClaimNo"].Index,c1OnlineCharge.Cols["ProviderName"].Index,c1OnlineCharge.Cols["PatientCode"].Index,c1OnlineCharge.Cols["PatientName"].Index,c1OnlineCharge.Cols["PatientDOB"].Index;

                nTotalOnlineClaim = c1OnlineCharge.Rows.Count - 1;

                c1OnlineCharge.Cols["nPortalClaimID"].Visible = false;
                c1OnlineCharge.Cols["nPatientID"].Visible = false;
                c1OnlineCharge.Cols["nProviderID"].Visible = false;
                c1OnlineCharge.Cols["PostingDate"].Visible = true;
                c1OnlineCharge.Cols["PortalClaimNo"].Visible = true;
                c1OnlineCharge.Cols["Title"].Visible = true;
                c1OnlineCharge.Cols["ProviderName"].Visible = true;
                c1OnlineCharge.Cols["Type"].Visible = true;
                c1OnlineCharge.Cols["PatientCode"].Visible = true;
                c1OnlineCharge.Cols["PatientName"].Visible = true;
                c1OnlineCharge.Cols["PatientDOB"].Visible = true;
                c1OnlineCharge.Cols["PatientGender"].Visible = true;
                c1OnlineCharge.Cols["ClaimNote"].Visible = true;
                c1OnlineCharge.Cols["nFacilityID"].Visible = false;
                c1OnlineCharge.Cols["dtHospFromDate"].Visible = false;
                c1OnlineCharge.Cols["dtHospToDate"].Visible = false;
                c1OnlineCharge.Cols["PortalClaimNo"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom;


                c1OnlineCharge.Cols["PostingDate"].Caption = "Posting Date";
                c1OnlineCharge.Cols["PortalClaimNo"].Caption = "Portal Claim #";

                c1OnlineCharge.Cols["ProviderName"].Caption = "Provider Name";

                c1OnlineCharge.Cols["PatientCode"].Caption = "Patient Code";
                c1OnlineCharge.Cols["PatientName"].Caption = "Patient Name";
                c1OnlineCharge.Cols["PatientDOB"].Caption = "Patient DOB";
                c1OnlineCharge.Cols["PatientGender"].Caption = "Gender";
                c1OnlineCharge.Cols["ClaimNote"].Caption = "Claim Note";


                int nWidth = 0;
                nWidth = c1OnlineCharge.Width;
                c1OnlineCharge.Cols["nPortalClaimID"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["nPatientID"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["nProviderID"].Width = Convert.ToInt32(nWidth * 0.13);
                c1OnlineCharge.Cols["PostingDate"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["PortalClaimNo"].Width = Convert.ToInt32(nWidth * 0.08);
                c1OnlineCharge.Cols["Title"].Width = Convert.ToInt32(nWidth * 0.15);
                c1OnlineCharge.Cols["ProviderName"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["Type"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["PatientCode"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["PatientName"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["PatientDOB"].Width = Convert.ToInt32(nWidth * 0.00);
                c1OnlineCharge.Cols["PatientGender"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["ClaimNote"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["nFacilityID"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["dtHospFromDate"].Width = Convert.ToInt32(nWidth * 0.10);
                c1OnlineCharge.Cols["dtHospToDate"].Width = Convert.ToInt32(nWidth * 0.10);

                c1OnlineCharge.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1OnlineCharge.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1OnlineCharge.AutoResize = true;
                c1OnlineCharge.ExtendLastCol = true;
                lblOCPSearch.Text = c1OnlineCharge.Cols["PortalClaimNo"].Caption + " : ";
                lblOCPSearch.Tag = c1OnlineCharge.Cols["PortalClaimNo"].Index;
             
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                lblOCPCount.Text = "Online Charge Count :" + Convert.ToString(nTotalOnlineClaim);
                c1OnlineCharge.EndUpdate();
            }
            return nTotalOnlineClaim;
        }
        private void setOnlineCharge(Int64 PortalClaimID)
        {
            gloBilling ogloBilling;
            ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            TransactionLines oLines = null;


            try
            {
                oLines = ogloBilling.GetOnlineCharge(PortalClaimID);
                rbICD10.Checked = true;


                if (oLines != null && oLines.Count > 0)
                {
                    #region "Get Selected Insurance"

                    string _fillInsuranceName = "";
                    Int64 _fillInsuranceID = 0;
                    Int32 _fillInsSelfMode = 0;
                    string _fillInsType = "";
                    //Int64 _DefaultTOSId = 0;
                    //Int64 _DefaultPOSId = 0;
                    DateTime _fillServiceDate = DateTime.Now;
                    if (c1Insurance.Rows.Count > 0)
                    {
                        for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                            {
                                _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                                _fillInsType = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE));
                                break;
                            }
                        }
                    }

                    #endregion

                    if (_fillInsSelfMode == PayerMode.Insurance.GetHashCode())
                    {
                        for (int i = 0; i <= oLines.Count - 1; i++)
                        {
                            oLines[i].InsuranceID = _fillInsuranceID;
                            oLines[i].InsuranceName = _fillInsuranceName;
                            oLines[i].InsurancePrimarySecondaryTertiary = _fillInsType;
                            oLines[i].InsuranceSelfMode = PayerMode.Insurance;
                        }
                    }
                    if (_dtLoadedCPTS != null)
                    {
                        _dtLoadedCPTS.Dispose();
                        _dtLoadedCPTS = null;
                    }
                    _dtLoadedCPTS = new DataTable();
                    _dtLoadedCPTS.Columns.Add("nLineNo");
                    _dtLoadedCPTS.Columns.Add("sCPTCodes");

                    for (int i = 0; i <= oLines.Count - 1; i++)
                    {
                        DataRow _dr = _dtLoadedCPTS.NewRow();
                        _dr["nLineNo"] = Convert.ToInt32(oLines[i].EMRTreatmentLineNo);
                        _dr["sCPTCodes"] = Convert.ToString(oLines[i].CPTCode);
                        _dtLoadedCPTS.Rows.Add(_dr);
                        _dtLoadedCPTS.AcceptChanges();
                    }

                    //Resetting Diagonosis Grid.
                    c1Dx.Rows.Count = 1;
                    UC_gloBillingTransactionLines.AutoSort = false;
                    UC_gloBillingTransactionLines.SetEMRExamLineTransaction(oLines);

                    for (int lineIndex = 0; lineIndex < oLines.Count; lineIndex++)
                    {
                        if (oLines[lineIndex].DateServiceFrom < _fillServiceDate)
                        {
                            _fillServiceDate = oLines[lineIndex].DateServiceFrom;
                        }
                    }
                }
                //LoadDefaultBillingSettings();
                UC_gloBillingTransactionLines.ShowTotal();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.View, "Open EMR Exam to Setup Charges", 0, 0, 0, ActivityOutCome.Failure);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (oLines != null) { oLines.Dispose(); }
                if (!bisEMRTreatmentSplitEnabled)
                {
                    //tlb_NoPost.Visible = false;

                }
            }

        }
        #endregion




    }
}
