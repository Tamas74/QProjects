using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAuditTrail;
using gloBilling.Collections;
using gloBilling.Common;
using gloGlobal;
using gloSettings;

namespace gloBilling
{
    public partial class frmBillingModifyCharges : Form
    {
        #region " Enumeration "

        public enum ShowHideType
        {
            None = 0,
            ModifyLoad = 1,

        }

        #endregion " Enumeration "

        #region "Variable Declarations"

        public string _DatabaseConnectionString = "";
        public string _emrdatabaseConnectionString = "";
        private string _messageBoxCaption = "";
        private Int64 _UserID = 0;
        private string _UserName = "";
        private Int64 _ClinicID = 0;
        private Int64 _TransPatientID = 0;
        private Int64 _TransactionID = 0;
        private Int64 nPAccountID = 0;
        private Int64 nGuarantorID = 0;
        private Int64 nAccountPatientID = 0;
        private bool _IsPatientAccountFeature = false;
        private string _PatientProviderName = "";
    //    private Int64 _nextClaimNo = 0;
        private String _InsuranceName = "";
        private String _sDelayReasonCode = string.Empty;
        private String _sServiceAuthExceptionCode = string.Empty;
        private Int64 _selectedChargeTrayId = 0;
        private string _selectedChargeTrayDescription = "";
     //   private bool _patientHasAutoClaim = false;
        private bool _patientHasPriorAuthorization = false;
        private ClaimValidationService _claimValidationService = ClaimValidationService.None;
        private FacilityType _defaultFeeTypeCharges = FacilityType.None;
        private Int64 _EMRFeeScheduleID = 0;
        private DataTable _dtUB04Settings = null;
        private StringBuilder _sbExcludeTransDtlID;
        private StringBuilder _sbExcludeTransMStDtlID;
        private StringBuilder _sbExcludeNoteID;
        private string _NextActionCode = "T";
        DataTable _dtNextActionID = null;
        private Int64 _InsuranceID = 0;
        private Int16 _TempParty = 1;
        private DataTable _dtParty = null;
        PayerMode _InsuranceSelfMode = PayerMode.None;
        private string _NextActionDescription = "Transacted";
        InsuranceTypeFlag _InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.None;
        bool _IsPartyChanged = false;
        const int ZERO_VALUE = 0;
        private string _TypeOfBill = "";
        private bool bIsFollowUpEnabled = false;
        private String _sMedicaidResubmissionCode = string.Empty;
        private bool _IsWorkercimp = false;
        private String _sPWKReportTypeCode = string.Empty;
        private String _sPWKReportTransmissionCode = string.Empty;
        private String _sPWKAttachmentControlNumber = string.Empty;
        private String _sMammogramCertNumber = String.Empty;
        private String _sIDENo = String.Empty;
        private string responsiblitytransferdate = string.Empty;
        private bool isReponsibilityTransfer = false;
        #endregion

        #region " Form Public and Private Methods "

        public Int64 SelectedChargeTrayID
        {
            get { return _selectedChargeTrayId; }
            set { _selectedChargeTrayId = value; }
        }

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

        private bool PatientHasPriorAuthorization
        {
            get { return _patientHasPriorAuthorization; }
            set { _patientHasPriorAuthorization = value; }
        }

        private Boolean bIsRefProvAsSupervisor { get; set; }

        private Boolean bIsSupProvSavedForExClaims { get; set; }

        public FacilityType DefaultFeeTypeCharges
        {
            get { return _defaultFeeTypeCharges; }
            set { _defaultFeeTypeCharges = value; }
        }

        public Boolean IsUBEnabled
        { get; set; }

        public string TypeOfBill
        {
            get { return _TypeOfBill; }
            set { _TypeOfBill = value; }
        }
        public string AdmitType
        {
            get;
            set;
        }

        public string DischargeStatus
        {
            get;
            set;
        }
        public bool bShowInitialTreatmentDate { get; set; }

        private void FillFormLoadData(Int64 TransactionId)
        {
            DataSet _dsChargesData = null;
            try
            {
                if (_TransPatientID > 0 && _UserID > 0)
                {
                    //LoadPatientStrip(_TransPatientID, 0, true);

                    _dsChargesData = gloCharges.GetChargesFormData(_TransPatientID);

                    if (_dsChargesData != null && _dsChargesData.Tables.Count > 0)
                    {
                        DesignDxGrid();
                        DesignInsuranceGrid();
                        if (CL_FollowUpCode.IsFollowUpFeatureON())
                        {
                            setActionTemplateVisibility();
                        }
                        FillStatesData();
                        FillFacilitiesData();
                        //FillProviderData();
                        FillReferralProvidersData(_dsChargesData.Tables[0], true);
                        FillPatientInsurances(_dsChargesData.Tables[3]);
                        FillClaimWorkerCompData(_dsChargesData.Tables[1]);
                        FillStandardFeeSchedules(_dsChargesData.Tables[7]);

                        SetPatientProvider();
                        //SetProviderSettings(_dsChargesData.Tables[2]);
                        SetCurrentResPartyStatus();
                        SetPriorAuthorization(_dsChargesData.Tables[5]);
                        SetSupervisorOptionSetting(_dsChargesData.Tables[12]);
                        SetUBAdminSetting(_dsChargesData.Tables[13]);
                        SetUB04AdminSetting(_dsChargesData.Tables[10]);
                        SetInitialTreatmentSetting(_dsChargesData.Tables[14]);
                        SetPatientCases(_dsChargesData.Tables[11]);
                        CheckForPatientCases();
                        SetAnesthesiaSetting(_dsChargesData.Tables[15]);
                        FillClaimReportingCategory(TransactionId);
                        if (_dsChargesData.Tables[16] != null && _dsChargesData.Tables[16].Rows.Count > 0)
                        {
                            Boolean bClaimReportingCategorySetting = false;
                            Boolean.TryParse(_dsChargesData.Tables[16].Rows[0]["sSettingsValue"].ToString(), out bClaimReportingCategorySetting);
                            if (bClaimReportingCategorySetting == true || Convert.ToInt16(_dsChargesData.Tables[16].Rows[0]["sSettingsValue"]) == 1)
                            {
                                cmbClaimCategory.Visible = true;
                                lblClaimCategory.Visible = true;
                            }
                            else
                            {
                                cmbClaimCategory.Visible = false;
                                lblClaimCategory.Visible = false;
                            }
                        }

                        SetDefaultQualifierForProvider(_dsChargesData.Tables[18]);

                        // line added on 12182013 Sameer for setting default date qualifier 
                        SetDefaultDateQualifier(_dsChargesData.Tables[19]);
                        FillReportingProviderTypes();

                        FillOtherClaimDateQualifiers();
                        FillClaimDateQualifiers();
                    }
                    
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Either PatientID or UserID found zero while loading charges form.", false);
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
            }
            finally
            {
                _dsChargesData.Dispose();
                _dsChargesData = null;
            }


        }

        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            oPatientControl.FillDetails(PatientId, _TransAccountID, gloStripControl.FormName.ModifyCharges);
            _TransPatientID = oPatientControl.PatientID;
            this.nPAccountID = oPatientControl.PAccountID;
            this.nGuarantorID = oPatientControl.GuarantorID;
            this.nAccountPatientID = oPatientControl.AccountPatientID;
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

        private void FillPatientInsurances(DataTable dtPatientInsurances)
        {
            c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
            bool _IsPrimaryPresent = false;
            bool _HasInsurance = true;
            int _CntPrimary = 0;
            _IsWorkercimp = false; 

            try
            {

                c1Insurance.Rows.Count = 1;
                this.c1Insurance.Rows.RemoveRange(1, (this.c1Insurance.Rows.Count - 1));
                CountResponsibilityNo = 0;


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
                            c1Insurance.SetData(rowIndex, COL_SELECT, false);
                            c1Insurance.SetData(rowIndex, COL_INSURANCENAME, Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceName"])); //
                            c1Insurance.SetData(rowIndex, COL_INSURANCEID, Convert.ToString(dtPatientInsurances.Rows[i]["nInsuranceID"])); //
                            c1Insurance.SetData(rowIndex, COL_INSSELFMODE, PayerMode.Insurance.GetHashCode()); //
                            c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, Convert.ToString(dtPatientInsurances.Rows[i]["sInsuranceFlag"]));
                            c1Insurance.SetData(rowIndex, COL_INSURANCERESPONSIBILITY, "");
                            c1Insurance.SetData(rowIndex, COL_INSURANCEPARTY, Convert.ToString("X"));
                            CountResponsibilityNo = Convert.ToInt64(dtPatientInsurances.Rows[i]["nInsuranceFlag"].ToString());
                            String _sInsDate = "";
                            if (Convert.ToString(dtPatientInsurances.Rows[i]["dtStartDate"]).Trim().Length > 0)
                                _sInsDate = Convert.ToString(dtPatientInsurances.Rows[i]["dtStartDate"]);
                            if (Convert.ToString(dtPatientInsurances.Rows[i]["dtStartDate"]).Trim().Length > 0 || Convert.ToString(dtPatientInsurances.Rows[i]["dtEndDate"]).Trim().Length > 0)
                                _sInsDate = _sInsDate + "-";
                            if (Convert.ToString(dtPatientInsurances.Rows[i]["dtEndDate"]).Trim().Length > 0)
                                _sInsDate = _sInsDate + Convert.ToString(dtPatientInsurances.Rows[i]["dtEndDate"]);
                            c1Insurance.SetData(rowIndex, COL_INSSTARTENDDATE, _sInsDate);
                            bool bWorkerComp = false;
                            try
                            {
                                bWorkerComp = Convert.ToBoolean(dtPatientInsurances.Rows[i]["bWorkersComp"]);
                            }
                            catch
                            {
                            }
                            if (bWorkerComp == true)
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

                            if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                            {
                                c1Insurance.SetData(rowIndex, COL_SELECT, true);
                                _CntPrimary = _CntPrimary + 1;
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
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCERESPONSIBILITY, (CountResponsibilityNo + 1).ToString());
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEPARTY, Convert.ToString("X"));
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCENAME, "Self"); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEID, "0"); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSSELFMODE, PayerMode.Self.GetHashCode()); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCECONTACTID, 0);
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEPLANONHOLD, 0);
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

                c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);

            }
            finally
            {
                if (IsBatchModify == false)
                {
                    SetInsuranceResponsibility(_CntPrimary);
                }
                ChangeInsuranceGridColor();

            }
            c1Insurance.Select(1, 1);
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
                cmbFacility.SelectedIndex = -1;
            }
        }

        private void FillProviderData()
        {
            try
            {

                this.cmbBillingProvider.SelectedIndexChanged -= new System.EventHandler(this.cmbBillingProvider_SelectedIndexChanged);
                
                DataTable dtProvidersTemp = null;
                dtProvidersTemp = gloCharges.GetCachedAllProviders();

                DataTable dtProviders = null;
                if (dtProvidersTemp != null && dtProvidersTemp.Rows.Count > 0)
                {
                    dtProviders = dtProvidersTemp.Clone();
                    DataRow[] _drProviders = null;
                    _drProviders = dtProvidersTemp.Select("bIsblocked <> 1 OR nProviderID IN(" + _InitialTransaction.ProviderID + ") ");
                    foreach (DataRow dr in _drProviders)
                    {
                        dtProviders.ImportRow(dr);
                    }
                }

                if (dtProviders != null)
                {
                    if (dtProviders.Rows.Count > 0)
                    {
                        dtProviders.DefaultView.Sort = "sProviderName";
                        cmbBillingProvider.BeginUpdate();
                        cmbBillingProvider.DataSource = dtProviders.DefaultView.ToTable().Copy();
                        cmbBillingProvider.ValueMember = dtProviders.Columns["nProviderID"].ColumnName;
                        cmbBillingProvider.DisplayMember = dtProviders.Columns["sProviderName"].ColumnName;
                        cmbBillingProvider.EndUpdate();
                        cmbBillingProvider.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            {
                this.cmbBillingProvider.SelectedIndexChanged += new System.EventHandler(this.cmbBillingProvider_SelectedIndexChanged);
            }
        }

        private void FillReferralProvidersData(DataTable dtReferralProviders, bool fillBlankItem)
        {

            if (dtReferralProviders != null)
            {

                string _selectedRefProvName = "";
                if (cmbReferralProvider.SelectedValue != null)
                {
                    _selectedRefProvName = Convert.ToString(cmbReferralProvider.Text);
                }

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
                    if (_InitialReferalProviderId > 0)
                    {
                        cmbReferralProvider.SelectedValue = _InitialReferalProviderId;

                    }
                    else
                    {
                        cmbReferralProvider.SelectedValue = 0;

                    }
                }
            }
        }
        private void FillClaimReportingCategory(Int64 nTransactionID)
        {
            DataTable dtClaimReportingCategory=null;
            dtClaimReportingCategory = gloBilling.GetAllClaimReportingCategory(nTransactionID);
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
    
        private void FillClaimWorkerCompData(DataTable dtWorkerCompClaim)
        {
            if (dtWorkerCompClaim != null)
            {
                cmbClaimNo.BeginUpdate();
                cmbClaimNo.DataSource = dtWorkerCompClaim;
                cmbClaimNo.ValueMember = dtWorkerCompClaim.Columns["sClaimno"].ColumnName;
                cmbClaimNo.DisplayMember = dtWorkerCompClaim.Columns["sClaimno"].ColumnName;
                cmbClaimNo.EndUpdate();
                cmbClaimNo.SelectedIndex = -1;
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

        private void SetPriorAuthorization(DataTable dtPriorAuthorization)
        {
            if (dtPriorAuthorization != null && dtPriorAuthorization.Rows.Count > 0)
            {
                PatientHasPriorAuthorization = Convert.ToBoolean(dtPriorAuthorization.Rows[0]["PriorAuthorization"]);
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

                //Will set Onset as default as per our existing implementation
                cmbBox14DateQualifier.SelectedValue = "431";
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
        private void CheckForPatientPriorAuthorization()
        {
            try
            {
                if (this.PatientHasPriorAuthorization)
                {
                    if ((Convert.ToString(txtPriorAuthorizationNo.Tag) == "") || (txtPriorAuthorizationNo.Tag == null) || (Convert.ToInt64(txtPriorAuthorizationNo.Tag) == 0))
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
                    if (txtPriorAuthorizationNo.Tag == null || Convert.ToString(txtPriorAuthorizationNo.Tag) == "" || Convert.ToInt64(txtPriorAuthorizationNo.Tag) == 0)
                    {
                        txtPriorAuthorizationNo.Text = "";
                        txtPriorAuthorizationNo.Tag = "";
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
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

        private void SyncronizeDxGridWithServiceline()
        {
            List<string> lstDx = new List<string>();
            ArrayList _arrDx = UC_gloBillingTransactionLines.GetUniqueDx();
            try
            {

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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                lstDx = null;
                _arrDx = null;
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
                            if (Convert.ToInt64(dr["sValue"]) > 0)
                            {
                                cmbBillingProvider.SelectedValue = Convert.ToInt64(dr["sValue"]);
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
                            break;
                        case "Facility":
                            if (Convert.ToString(dr["sValue"]).Trim() != "")
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

        private void SetCurrentResPartyStatus()
        {
            DataTable dtPartyStatus = null;
            DataRow[] resultRows = null;
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, "");
            try
            {

                for (int icount = 1; icount <= c1Insurance.Rows.Count - 1; icount++)
                {
                    if (c1Insurance.GetCellCheck(icount, COL_SELECT_CURRENT_RESPONSIBLE_PARTY) == CheckEnum.Checked)
                    {
                        nCurrentResponsibleParty = icount;
                        break;
                    }
                }
                dtPartyStatus = ogloBilling.GetAllPatientPartyStatus(_MasterTransactionID, _TransactionID);
                resultRows = dtPartyStatus.Select("nContactID = " + Convert.ToInt64(c1Insurance.GetData(Convert.ToInt16(nCurrentResponsibleParty), COL_INSURANCECONTACTID)));
                if (resultRows.Length > 0)
                {
                    foreach (DataRow dr in resultRows)
                    {
                        if (c1Insurance.GetData(Convert.ToInt16(nCurrentResponsibleParty), COL_INSURANCENAME).ToString().ToLower() != "self")
                        {
                            c1Insurance.SetData(Convert.ToInt16(nCurrentResponsibleParty), COL_INSVIEW_PARTYSTATUS, Convert.ToString(dr["PartyStatus"]));
                        }
                        else
                        {
                            tls_ViewClaim.Visible = false;
                        }
                        c1Insurance.SetData(Convert.ToInt16(nCurrentResponsibleParty), COL_INS_INTERNAL_PARTYSTATUS, Convert.ToString(dr["PartyStatus"]));
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            {
                if (dtPartyStatus != null) { dtPartyStatus.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        private void SetFacility(Boolean IsFacilityChange = false)
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
                            UC_gloBillingTransactionLines.SetFacilityPOS(IsFacilityChange); 
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        private DataTable GetCachedChargeTrays()
        {
            DataTable _dtChargeTrays = null;

            try
            {
                _dtChargeTrays = gloGlobal.gloPMMasters.GetChargesTrays();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

            return _dtChargeTrays;
        }

        public void GetClaimDetails()
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _emrdatabaseConnectionString);
            Transaction _Transaction = null;
            Int64 _TransMasterID = 0;
            Int64 _nClaimNo = 0;
            DateTime _ModifyDate = DateTime.Now;
            try
            {


                UC_gloBillingTransactionLines = new gloBillingTransaction();
                UC_gloBillingTransactionLines.AutoSort = false;
                UC_gloBillingTransactionLines.InitialNofRows = 1;
                UC_gloBillingTransactionLines.IsOpenForModify = true;
                pnlTransactionLines.Controls.Add(UC_gloBillingTransactionLines);
                UC_gloBillingTransactionLines.Visible = true;
                UC_gloBillingTransactionLines.Dock = DockStyle.Fill;
                UC_gloBillingTransactionLines.PatientID = this.PatientID;
                UC_gloBillingTransactionLines.DefaultTOSID = _DefaultTOSID;
                UC_gloBillingTransactionLines.DefaultPOSID = _DefaultPOSID;
                UC_gloBillingTransactionLines.IsFormLoading = _IsFormLoading;
                //UC_gloBillingTransactionLines.PatientProviderID = _PatientPoviderID;
                
                //UC_gloBillingTransactionLines.onCPTCharges_Load += new gloBillingTransaction.onCPTChargesLoaded(UC_gloBillingTransactionLines_onCPTCharges_Load);


                #region " Retrieve the Charges Details in a Transaction Object "

                //_Transaction = ogloBilling.GetChargesClaimDetails(_TransactionID, _ClinicID);
                _Transaction = ogloBilling.GetChargesClaimDetails(_TransactionID, _ClinicID);

                //To Preserve the object
                if (_InitialTransaction != null)
                {
                    _InitialTransaction.Dispose();
                    _InitialTransaction = null;
                }
                _InitialTransaction = _Transaction;

                FillProviderData();

                _TransAccountID = _Transaction.PAccountID;
                this.PatientID = _Transaction.PatientID;

                LoadPatientStrip(_TransPatientID, 0, true);
                nInitialStripAccountID = oPatientControl.CmbSelectedAccountID;
                nInitialStripPatientID = oPatientControl.CmbSelectedPatientID;
                nInitialStripGuarantorName = oPatientControl.Guarantor;

                UC_gloBillingTransactionLines.PatientProviderID = oPatientControl.PatientProviderID;

       
                DataTable dtClaimReportCategory = null;
                dtClaimReportCategory = gloBilling.LoadClaimReprtingCategory(_InitialTransaction.TransactionMasterID);
                if (dtClaimReportCategory != null && dtClaimReportCategory.Rows.Count > 0)
                {
                    try
                    {
                        _Transaction.nClaimCategoryID = Convert.ToInt64(dtClaimReportCategory.Rows[0]["nClaimReportingCategoryID"]);
                    }
                    catch
                    {
                    }
                    cmbClaimCategory.SelectedValue = _Transaction.nClaimCategoryID; // Convert.ToInt64(dtClaimReportCategory.Rows[0]["nClaimReportingCategoryID"]);
                }
                
               
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

                    _dtEOBPayments = gloBilling.GetEOBPaymentsV2(_MasterTransactionID);
                    _dtEOBNextAction = gloBilling.GetEOBNextAction(_MasterTransactionID);

                    txtClaimNo.Text = _Transaction.ClaimNumber;

                    sClaimNo = gloCharges.FormattedClaimNumberGeneration(Convert.ToString(_Transaction.ClaimNo));
                    txtClaimNo.Tag = _Transaction.SubClaimNo;
                    try
                    {
                        cmbFacility.SelectedValue = Convert.ToString(_Transaction.FacilityCode);
                    }
                    catch
                    {
                    }
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
                        SetFacility();
                    }
                    try
                    {
                        cmbBillingProvider.SelectedValue = _Transaction.ProviderID;
                    }
                    catch
                    {
                    }
                    mskClaimDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.TransactionDate).ToString("MM/dd/yyyy");

                    if (_Transaction.ClaimBox15Date != null)
                    { mskBox15Date.Text = Convert.ToDateTime(_Transaction.ClaimBox15Date).ToString("MM/dd/yyyy"); }
                    else { mskBox15Date.Text = ""; }
                    if (_Transaction.ClaimBox15QualifierCode != null && Convert.ToString(_Transaction.ClaimBox15QualifierCode).Trim() != "" && _Transaction.ClaimBox15QualifierCode != "0")
                    { cmbBox15DateQualifier.SelectedValue = _Transaction.ClaimBox15QualifierCode; }
                    else
                    {
                        try
                        {
                            cmbBox15DateQualifier.SelectedValue = Convert.ToString((DefaultDateQualifierCode != null) ? DefaultDateQualifierCode : "");
                        }
                        catch
                        {
                        }
                    }

                    if (_Transaction.ClaimBox14QualifierCode != null && Convert.ToString(_Transaction.ClaimBox14QualifierCode).Trim() != "")
                    { cmbBox14DateQualifier.SelectedValue = _Transaction.ClaimBox14QualifierCode; }
                    else
                    { cmbBox14DateQualifier.SelectedValue = "431"; }


                    if (_Transaction.ProviderQualifierCode != null && Convert.ToString(_Transaction.ProviderQualifierCode).Trim() != "" && _Transaction.ProviderQualifierCode != "0")
                    { cmbProviderType.SelectedValue = _Transaction.ProviderQualifierCode; }
                    else
                    {
                        try
                        {
                            cmbProviderType.SelectedValue = DefaultProviderQualifierCode;
                        }
                        catch
                        {
                        }
                         
                    }
                    

                    txtPriorAuthorizationNo.Tag = Convert.ToInt64(_Transaction.PriorAuthorizationID);
                    txtPriorAuthorizationNo.Text = Convert.ToString(_Transaction.PriorAuthorizationNo);
                    //CheckForPatientPriorAuthorization();


                    if (_Transaction.OnsiteDate > 0)
                    {
                        mskOnsiteDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.OnsiteDate).ToString("MM/dd/yyyy");
                        mskOnsiteDate.BringToFront();
                    }
                    else
                    {
                        mskOnsiteDate.Text = "";
                    }


                    #region " Worker Comp,Auto Claim "

                    if (_Transaction.WorkersComp == true)
                    {
                        _WorkerCompType = "WorkersComp";

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
                        _WorkerCompType = "AutoClaim";
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
                        _WorkerCompType = "OtherAccident";
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

                    //if (_Transaction.UnableToWorkFromDate > 0)
                    //{
                    //    mskUnableFromDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.UnableToWorkFromDate).ToString("MM/dd/yyyy");
                    //}
                    //else
                    //{
                    //    mskUnableFromDate.Text = "";
                    //}

                    if (_Transaction.UnableToWorkFromDate > 0)
                    { _UnableToWorkFromDate_MoreClaimData = _Transaction.UnableToWorkFromDate; }
                    else
                    { mskUnableFromDate.Text = ""; _UnableToWorkFromDate_MoreClaimData = 0; }

                    //if (_Transaction.UnableToWorkTillDate > 0)
                    //{
                    //    mskUnableTillDate.Text = gloDateMaster.gloDate.DateAsDate(_Transaction.UnableToWorkTillDate).ToString("MM/dd/yyyy");
                    //}
                    //else
                    //{
                    //    mskUnableTillDate.Text = "";
                    //}

                    if (_Transaction.UnableToWorkTillDate > 0)
                    { _UnableToWorkTillDate_MoreClaimData = _Transaction.UnableToWorkTillDate; }
                    else
                    { mskUnableTillDate.Text = ""; _UnableToWorkTillDate_MoreClaimData = 0; }

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

                    #endregion


                    chkOutSideLab.Checked = _Transaction.OutSideLab;
                    

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
                    txtClaimCLIAno.Text = Convert.ToString(_Transaction.CLIANumber);


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

                    _selectedChargeTrayId = Convert.ToInt64(_Transaction.CloseDayTrayID);
                    _selectedChargeTrayDescription = Convert.ToString(_Transaction.CloseDayTrayName);
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
                   
                    UC_gloBillingTransactionLines._nContactID = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                    if (_Transaction.Lines != null)
                    {
                        if (_Transaction.Lines.Count > 0)
                        {
                            UC_gloBillingTransactionLines.SetLineTransaction(_Transaction.Lines);
                        }
                    }



                    SetDxList(_Transaction.TransactionMasterID, _Transaction.VisitID, _Transaction.ClaimNo, _Transaction.PatientID, _Transaction.ClinicID);

                    if (UC_gloBillingTransactionLines != null && _IsFormLoading == false)
                    {
                        UC_gloBillingTransactionLines.SetFNFCharges();

                    }

                    //To Remove the Previous Flag
                    for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                    }

                    DataTable _dt = gloCharges.GetCurrentPartyNumber(_TransactionID, _MasterTransactionID);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {

                        Int16 _party = -1;
                        _party = Convert.ToInt16(_dt.Rows[0]["PartyNo"]);
                        InsParty = Convert.ToInt16(_dt.Rows[0]["PartyNo"]);
                        nCurrentResponsibleParty = Convert.ToInt16(_dt.Rows[0]["PartyNo"]);
                        System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                        c1Insurance.SetCellImage(_party, COL_INSURANCERESPONSIBILITY, imgFlag);
                        c1Insurance.SetCellCheck(_party, COL_SELECT_CURRENT_RESPONSIBLE_PARTY, CheckEnum.Checked);

                        Int64 nContactID = 0;
                        Int64.TryParse(Convert.ToString(c1Insurance.GetData(_party, COL_INSURANCECONTACTID)), out nContactID);

                        Int64 nInsuranceID = 0;
                        Int64.TryParse(Convert.ToString(c1Insurance.GetData(_party, COL_INSURANCEID)), out nInsuranceID);

                        //if (gloAccountsV2.gloInsurancePaymentV2.IsRebilled(_MasterTransactionID, _TransactionID, nContactID, nInsuranceID))
                        //{
                        _sClaimRefNo = _Transaction.sClaimRefNo;
                        _IsbCliamReplacement = _Transaction.IsReplacementClaim;
                        //}

                        if (nContactID > 0)
                        {
                            tls_FollowUpActionDate.Visible = true;
                        }
                        else
                        {
                            tls_FollowUpActionDate.Visible = false;
                        }


                    }

                    #endregion " Insurance Plan "

                    #region "Claim Hold"

                    if (_Transaction.Hold != null)
                    {
                        _oClaimHold = _Transaction.Hold;
                    }
                    else
                    {
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
                        _oBox19Note = new ClaimBox19Note();
                    }

                    if (_Transaction.ClaimBox10dNote != null)
                    {
                        _sBox10dNote = _Transaction.ClaimBox10dNote;
                    }
                    else
                    {
                        _sBox10dNote = string.Empty;
                    }

                    _IllnessDate = _Transaction.IllnessDate;
                    _LastSeenDate = _Transaction.LastSeenDate;
                    _UnableToWorkFromDate_MoreClaimData = _Transaction.UnableToWorkFromDate;
                    _UnableToWorkTillDate_MoreClaimData = _Transaction.UnableToWorkTillDate;
                    bIsRefProvAsSupervisor = _Transaction.bIsRefprovAsSupervisor;
                    bIsSupProvSavedForExClaims = _Transaction.bIsRefprovAsSupervisor;

                    _sDelayReasonCode = _Transaction.DelayReasonCodeID;
                    _sServiceAuthExceptionCode = _Transaction.ServiceAuthExceCode;
                    _sMedicaidResubmissionCode = _Transaction.MedicaidResubmissioncode;
                    _sPWKReportTypeCode = _Transaction.PWKReportTypeCode;
                    _sPWKReportTransmissionCode = _Transaction.PWKReportTransmissionCode;
                    _sPWKAttachmentControlNumber = _Transaction.PWKAttachmentControlNumber;
                    _sMammogramCertNumber = _Transaction.MammogramCertNumber;
                    _sIDENo = _Transaction.IDENo;
                    //  _sMedicaidResubmissionCode=_Transactio

                    #endregion

                    #region "Check For Payment Notes "

                    CheckForPaymentNotes(_Transaction.Lines);

                    #endregion

                    #region " ICD Revision"
                    if (_Transaction.nICDRevision == 9)
                        rbICD9.Checked = true;
                    else if (_Transaction.nICDRevision == 10)
                        rbICD10.Checked = true;
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

                #region " Billing Transaction Control Event Attaching "

                UC_gloBillingTransactionLines.onGrid_CellChanged += new gloBillingTransaction.onGridCellChanged(UC_gloBillingTransactionLines_onGrid_CellChanged);
                UC_gloBillingTransactionLines.onGrid_SelChanged += new gloBillingTransaction.onGridSelChanged(UC_gloBillingTransactionLines_onGrid_SelChanged);
                UC_gloBillingTransactionLines.onInsCPTDxMod_Changed += new gloBillingTransaction.onInsCPTDxModChanged(UC_gloBillingTransactionLines_onInsCPTDxMod_Changed);
                UC_gloBillingTransactionLines.show_LineNotes += new gloBillingTransaction.showLineNote(UC_gloBillingTransactionLines_show_LineNotes);
                //UC_gloBillingTransactionLines.onCPTCharges_Load += new gloBillingTransaction.onCPTChargesLoaded(UC_gloBillingTransactionLines_onCPTCharges_Load);
                UC_gloBillingTransactionLines.date_Changed += new gloBillingTransaction.dateChanged(UC_gloBillingTransactionLines_date_Changed);
                UC_gloBillingTransactionLines.CLIA_Enter += new gloBillingTransaction.CLIAEnter(UC_gloBillingTransactionLines_CLIA_Enter);
                #endregion

                IsEditable(_Transaction);

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

                _dtNextActionID = GetIDForNextAction();
                CheckForPatientPriorAuthorization();
                gloCharges.GetMasterTransactionID(_TransactionID, out _TransMasterID, out _nClaimNo, out _ModifyDate);


                GetReplacementClaimIndication();


            }
            catch (Exception ex)
            {
                Boolean _isReleased = gloCharges.ReleaseLockStatus(_MasterTransactionID);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
        }

        public void LoadPatientModifiedData()
        {
            Int64 nSelectedRefProvider = 0;
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _emrdatabaseConnectionString);
            Transaction _Transaction = new Transaction();
            Int64 _ContactId = 0;
            try
            {
                this.c1Insurance.EnterCell -= new System.EventHandler(this.c1Insurance_EnterCell);
                c1Insurance_BeforeEdit(null, null);
                this.c1Insurance.BeforeEdit -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_BeforeEdit);


                if (oPatientControl.PatientID > 0)
                {
                    nSelectedRefProvider = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                    _TransPatientID = oPatientControl.PatientID;
                    SetPatientChangeData(false);
                    SelectPrimaryInsurance();
                    SetInsuranceParty();
                    SetCurrentResPartyStatus();
                    ChangeInsuranceGridColor();
                    cmbFacility.SelectedValue = UC_gloBillingTransactionLines.FacilityID;

                    //To Remove the Previous Flag
                    for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                    }

                    DataTable _dt = gloCharges.GetCurrentPartyNumber(_TransactionID, _MasterTransactionID);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {

                        Int16 _party = -1;
                        _party = Convert.ToInt16(_dt.Rows[0]["PartyNo"]);
                        InsParty = Convert.ToInt16(_dt.Rows[0]["PartyNo"]);
                        nCurrentResponsibleParty = Convert.ToInt16(_dt.Rows[0]["PartyNo"]);
                        System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                        c1Insurance.SetCellImage(_party, COL_INSURANCERESPONSIBILITY, imgFlag);
                        c1Insurance.SetCellCheck(_party, COL_SELECT_CURRENT_RESPONSIBLE_PARTY, CheckEnum.Checked);
                    }


                    if (UC_gloBillingTransactionLines != null)
                    {
                        UC_gloBillingTransactionLines.PatientID = this.PatientID;
                        UC_gloBillingTransactionLines.DefaultPOSID = _DefaultPOSID;
                        UC_gloBillingTransactionLines.DefaultTOSID = _DefaultTOSID;
                        UC_gloBillingTransactionLines.PatientProviderID = _PatientPoviderID;

                    }


                    chk_SameasBillingProvider_CheckedChanged(null, null);

                    if (!_InitialTransaction.IsVoid) // --- If Voided then dont Relaod the Insurance Grid and tool bars buse in void no changes are allowed
                    {
                        IsEditable(_InitialTransaction);
                    }

                    //**GetHoldMessage();
                    SetHoldnMoreClaimDataMesseges();
                    SetLastGlobalPeriods();
                    CheckForEPSDTEnabled();      

                    #region " Expanded Claim Settings "

                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        if (c1Insurance.GetCellCheck(i, COL_SELECT_CURRENT_RESPONSIBLE_PARTY) == CheckEnum.Checked)
                        {
                            _ContactId = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCECONTACTID));
                            UC_gloBillingTransactionLines._nContactID = _ContactId;
                            if (c1Insurance.GetData(i, COL_INSURANCEID) != null && Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID))>0)
                                UC_gloBillingTransactionLines.setModifyAllowedAmount(Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID)));
                            break;
                        }
                    }

                    ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                    _Transaction.NoOfServiceLine = _NoOfMaxServiceLines;
                    _Transaction.NoOfDiagnosis = _NoOfMaxDiagnosis;
                    UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                    UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;

                    #endregion

                    #region " UB settings "


                    if (IsUBEnabled == true && (gloCharges.GetBillingType(_TransactionID, _MasterTransactionID) == Convert.ToInt16(BillingType.Institutional)))
                    {
                        tls_AddUBData.Visible = true;
                    }
                    else
                    {
                        tls_AddUBData.Visible = false;
                    }

                    #endregion

                    bool bExists = false;
                    if (cmbReferralProvider.DataSource != null)
                    {
                        foreach (DataRowView drv in cmbReferralProvider.Items)
                        {
                            if (Convert.ToInt64(drv.Row.ItemArray[0]) == nSelectedRefProvider) { bExists = true; break; }
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

                    if (IsClaimVoided)
                    {
                        SetVoidViewMode();
                    }

                    GetReplacementClaimIndication();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            {
                this.c1Insurance.EnterCell += new System.EventHandler(this.c1Insurance_EnterCell);
                if (_Transaction != null) { _Transaction.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                this.c1Insurance.EnterCell += new System.EventHandler(this.c1Insurance_EnterCell);
                this.c1Insurance.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_BeforeEdit);

            }


        }

        private void SetPatientChangeData(bool LoadPatientBanner)
        {
            DataSet _dsChargesData = null;
            try
            {
                _TransPatientID = oPatientControl.CmbSelectedPatientID;
                this.nPAccountID = oPatientControl.PAccountID;
                this.nGuarantorID = oPatientControl.GuarantorID;
                this.nAccountPatientID = oPatientControl.AccountPatientID;

                _dsChargesData = gloCharges.GetPatientModifiedData(_TransPatientID);

                DesignInsuranceGrid();// Instead of redesigning overwrite the data
                if (_dsChargesData != null && _dsChargesData.Tables.Count > 0)
                {
                    FillPatientInsurances(_dsChargesData.Tables[2]);
                    //AppendInsurance(_dsChargesData.Tables[2]); // Will Reduce the retrieval of data in to the transaction object and setting the resp again
                    FillReferralProvidersData(_dsChargesData.Tables[0], true);
                    FillClaimWorkerCompData(_dsChargesData.Tables[1]);
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
            }
            finally
            {
                _dsChargesData.Dispose();
                _dsChargesData = null;
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

        private void AppendInsurance(DataTable dtPatientInsurances)
        {

            try
            {
                if (dtPatientInsurances.Rows.Count > 0)
                {
                    c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                    bool _IsFound = false;

                    for (int i = 0; i < dtPatientInsurances.Rows.Count; i++)
                    {
                        #region " Update the Insurance Type,workercomp,copay etc For previously added ins in the grid"

                        _IsFound = false;
                        for (int j = 1; j < c1Insurance.Rows.Count; j++)
                        {
                            if (Convert.ToInt64(c1Insurance.GetData(j, COL_INSURANCEID)) == Convert.ToInt64(dtPatientInsurances.Rows[i]["nInsuranceID"]))
                            {
                                c1Insurance.SetData(j, COL_INSURANCECONTACTID, Convert.ToString(dtPatientInsurances.Rows[i]["nContactID"]));
                                c1Insurance.SetData(j, COL_INSSELFMODE, PayerMode.Insurance.GetHashCode());
                                c1Insurance.SetData(j, COL_INSURANCETYPE, Convert.ToString(dtPatientInsurances.Rows[i]["sInsuranceFlag"]));
                                c1Insurance.SetData(j, COL_INSURANCECOPAYAMT, Convert.ToDecimal(dtPatientInsurances.Rows[i]["CoPay"]));
                                c1Insurance.SetData(j, COL_INSURANCEPLANONHOLD, Convert.ToBoolean(dtPatientInsurances.Rows[i]["IsPlanOnHold"]));

                                if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bWorkersComp"]) == true)
                                    c1Insurance.SetData(j, COL_INSURANCEWORKERCOMP, true);

                                if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bAutoClaim"]) == true)
                                    c1Insurance.SetData(j, COL_INSURANCEAUTOCLAIM, "Auto Claim");

                                _IsFound = true;
                                break;
                            }
                        }

                        #endregion

                        #region "If Insurance Not Found in Grid then Add From the Datatable"

                        if (_IsFound == false)
                        {
                            c1Insurance.Rows.Add();
                            int rowIndex = c1Insurance.Rows.Count - 1;

                            c1Insurance.SetData(rowIndex, COL_SELECT, false);
                            c1Insurance.SetData(rowIndex, COL_INSURANCENAME, Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceName"]));
                            c1Insurance.SetData(rowIndex, COL_INSURANCEID, Convert.ToString(dtPatientInsurances.Rows[i]["nInsuranceID"]));
                            c1Insurance.SetData(rowIndex, COL_INSURANCECONTACTID, Convert.ToString(dtPatientInsurances.Rows[i]["nContactID"]));
                            c1Insurance.SetData(rowIndex, COL_INSSELFMODE, PayerMode.Insurance.GetHashCode());
                            c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, Convert.ToString(dtPatientInsurances.Rows[i]["sInsuranceFlag"]));
                            c1Insurance.SetData(rowIndex, COL_INSURANCERESPONSIBILITY, "");
                            c1Insurance.SetData(rowIndex, COL_INSURANCEPARTY, Convert.ToString("X"));
                            c1Insurance.SetData(rowIndex, COL_INSURANCECOPAYAMT, Convert.ToDecimal(dtPatientInsurances.Rows[i]["CoPay"]));

                            if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bWorkersComp"]) == true)
                                c1Insurance.SetData(rowIndex, COL_INSURANCEWORKERCOMP, true);

                            if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bAutoClaim"]) == true)
                                c1Insurance.SetData(rowIndex, COL_INSURANCEAUTOCLAIM, "Auto Claim");

                            #region "Set Backcolor For Resp. Column"

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

                                }

                            }
                            catch
                            {
                                cStyle = c1Insurance.Styles.Add("BubbleValues");

                            }
                            cStyle.BackColor = Color.White;

                            C1.Win.C1FlexGrid.CellRange rgBubbleValues = c1Insurance.GetCellRange(rowIndex, COL_INSURANCERESPONSIBILITY);
                            rgBubbleValues.Style = cStyle;

                            #endregion
                        }

                        #endregion
                    }

                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
            }
        }

        #region " TVP Save Methods "

        private bool SaveClaimData(bool IsSplit)
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _emrdatabaseConnectionString);
            ClsBL_Hold oClsBL_Hold = new ClsBL_Hold(_DatabaseConnectionString, _emrdatabaseConnectionString);
            Transaction MasterTransaction = new Transaction();
            TransactionInsurance Insurance = new TransactionInsurance();
            TransactionLines oLineTransactions = null;
            clsgloResend oclsgloResend = new clsgloResend();
            SplitClaimDetails oSplitClaimDetails;

            Int64 _returnID = 0;
            Int64 _ContactID = 0;

            string _InsTransferCloseDate = "";

            bool _result = false;
            bool isFinishEditing;

            dsChargesTVP odsChargesTVP = new dsChargesTVP();

            try
            {
                mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskClaimDate.Text != "")
                {
                    mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    UC_gloBillingTransactionLines.ColseDate = mskClaimDate.Text;

                }
                else
                {
                    UC_gloBillingTransactionLines.ColseDate = "";
                    mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                }

                if (_dtEOBPayments != null && _dtEOBPayments.Rows.Count > 0)
                {
                    DateTime dr = Convert.ToDateTime(_dtEOBPayments.Compute("MIN(dtCloseDate)", String.Empty));

                    if (dr != null)
                    {
                        UC_gloBillingTransactionLines.MaxPaymentDate = dr.ToShortDateString();
                    }
                    else
                    {
                        UC_gloBillingTransactionLines.MaxPaymentDate = "";
                    }
                }



                #region " Save Transaction "

                if (!(UC_gloBillingTransactionLines == null))
                {
                    isFinishEditing = UC_gloBillingTransactionLines.FinishEditing();

                    if (ValidateMasterTransaction())
                    {
                        UC_gloBillingTransactionLines.IsPlanOrAdminEPSDTEnabled = EPSDTEnabled;
                        if (UC_gloBillingTransactionLines.ValidateTransaction(_InitialTransaction))
                        {
                            if (IsDxSelected())
                            {
                                if (UC_gloBillingTransactionLines.ValidateDxList(GetSelectedDx(), IsSplit))
                                {

                                    #region " Check for Insurance Responsibility Transfer "

                                    if (IsPartyChanged())
                                    {

                                        _IsPartyChanged = true;

                                    }
                                    else
                                    {
                                        _IsPartyChanged = false;
                                    }

                                    #endregion

                                    if (_IsPartyChanged && _IsResnd)
                                    {
                                        _IsResnd = false;
                                        MasterTransaction.IsResend = true;
                                    }
                                    else
                                    {
                                        MasterTransaction.IsResend = false;
                                    }




                                    if (UC_gloBillingTransactionLines.GetLinesCount() > 0)
                                    {

                                        SetMasterClaimDetails_TVP(odsChargesTVP);
                                        SetServiceLineDetails_TVP(odsChargesTVP);
                                        SetDXClaimDetails_TVP(odsChargesTVP);
                                        SetBox19Notes_TVP(odsChargesTVP);

                                        _result = SaveClaim(odsChargesTVP);

                                        #region " Update Master claim Lines Data on Split Claims "

                                        //gloDatabaseLayer.DBLayer oDBsplit = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                                        //gloDatabaseLayer.DBParameters oDBParameters1 = new gloDatabaseLayer.DBParameters();
                                        //oDBsplit.Connect(false);
                                        //for (int i = 0; i < MasterTransaction.Lines.Count; i++)
                                        //{
                                        //    oDBParameters1.Clear();
                                        //    oDBParameters1.Add("@nTransactionID", MasterTransaction.Lines[i].TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                                        //    oDBParameters1.Add("@nTransactionDetailID", MasterTransaction.Lines[i].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                                        //    oDBsplit.Execute("BL_UP_SPLITCLIAMS", oDBParameters1);
                                        //}
                                        //oDBsplit.Dispose();

                                        #endregion


                                        MasterTransaction.TransactionID = _MasterTransactionID;

                                        //Modified by Pramod Nair For Split Claims -- 
                                        string _NextAction = "";
                                        DataTable dtNextAction = gloCharges.GetNextAction(_MasterTransactionID);
                                        if (dtNextAction != null && dtNextAction.Rows.Count > 0)
                                        {
                                            _NextAction = Convert.ToString(dtNextAction.Rows[0]["sNextActionCode"]);
                                        }

                                        _NextAction = _NextActionCode;

                                        //update the transaction Lines only if a Add Line Feature is available
                                        if (_NextAction == "T")
                                        {
                                            for (int i = 0; i < MasterTransaction.Lines.Count; i++)
                                            {
                                                MasterTransaction.Lines[i].TransactionId = MasterTransaction.Lines[i].TransactionMasterID;
                                                MasterTransaction.Lines[i].TransactionDetailID = MasterTransaction.Lines[i].TransactionMasterDetailID;

                                            }

                                            //To insert the Details in the TransactionMaster
                                            //_returnID = ogloBilling.AddChargesModifySave(MasterTransaction, _ClinicID);

                                            //Added by Subashish_b on 22/Feb /2011 (integration made on date-10/May/2011) for  update patient change informaton in db.

                                            //if (_IsPatientAccountFeature)
                                            //{
                                            //    if (_isPatientChanged && _isAccountChanged)
                                            //    {
                                            //        UpdateOldPatientPayement_To_NewPatient(MasterTransaction.TransactionMasterID, nInitialStripPatientID, oPatientControl.CmbSelectedPatientID);
                                            //    }
                                            //    if (_isPatientChanged)
                                            //    {
                                            //        UpdateOldPatientPayement_To_NewPatient(MasterTransaction.TransactionMasterID, nInitialStripPatientID, oPatientControl.CmbSelectedPatientID);
                                            //    }
                                            //    if (_isAccountChanged)
                                            //    {
                                            //        UpdateOldPatientPayement_To_NewPatient(MasterTransaction.TransactionMasterID, nInitialStripPatientID, oPatientControl.CmbSelectedPatientID);
                                            //    }

                                            //}



                                            //ogloBilling.UpdateNewLine(_MasterTransactionID, _TransactionID);

                                            ////Update UB Claim Details
                                            //ogloBilling.UBClaimSave(MasterTransaction, _ClinicID);


                                            _TransactionID = _returnID;

                                            #region " Delete the Previous TransactionLines if any Lines are Removed While Modifying"

                                            //gloDatabaseLayer.DBLayer oDBLines = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                                            //String _sqlQueryLines = "";
                                            //sb_ExcludeTransDtlID = new StringBuilder();
                                            //try
                                            //{
                                            //    oDBLines.Connect(false);

                                            //    for (int i = 0; i < MasterTransaction.Lines.Count; i++)
                                            //    {
                                            //        if (i == MasterTransaction.Lines.Count - 1)
                                            //        {
                                            //            sb_ExcludeTransDtlID.Append(MasterTransaction.Lines[i].TransactionDetailID.ToString());
                                            //        }
                                            //        else
                                            //        {
                                            //            sb_ExcludeTransDtlID.Append(MasterTransaction.Lines[i].TransactionDetailID.ToString() + ",");
                                            //        }


                                            //    }
                                            //    //_sqlQueryLines = " delete from BL_Transaction_Claim_Lines where  nTransactionID=" + MasterTransaction.TransactionMasterID + " and nTransactionDetailID NOT IN ( " + sb_ExcludeTransDtlID + ")";
                                            //    _sqlQueryLines = " delete from BL_Transaction_Claim_Lines WITH(READPAST) where  nTransactionMasterID=" + MasterTransaction.TransactionMasterID + " and nTransactionMasterDetailID NOT IN ( " + sb_ExcludeTransDtlID + ")";
                                            //    oDBLines.Execute_Query(_sqlQueryLines);
                                            //    oDBLines.Disconnect();
                                            //}
                                            //catch
                                            //{

                                            //}
                                            //finally
                                            //{

                                            //    if (oDBLines != null)
                                            //    {
                                            //        oDBLines.Dispose();
                                            //    }
                                            //}

                                            #endregion

                                            String strClaimRemitRefNo = "";
                                            strClaimRemitRefNo = gloCharges.CheckRefNumber(MasterTransaction.TransactionMasterID, MasterTransaction.ContactID, MasterTransaction.InsuranceID, MasterTransaction.ClinicID);

                                            # region Claim Remittance Ref. No.

                                            //gloDatabaseLayer.DBLayer oDBClaimRef = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                                            //gloDatabaseLayer.DBParameters oDBClaimRefParameters = new gloDatabaseLayer.DBParameters();
                                            //oDBClaimRef.Connect(false);

                                            //if (_sClaimRefNo.Trim() != string.Empty)
                                            //{
                                            //    if (MasterTransaction.ResponsibilityType == PayerMode.Insurance)
                                            //    {
                                            //        oDBClaimRefParameters.Add("@nTransactionID", MasterTransaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2), MasterTransaction.TransactionMasterID
                                            //        oDBClaimRefParameters.Add("@nContactID", MasterTransaction.ContactID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                            //        oDBClaimRefParameters.Add("@nInsuranceID", MasterTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                            //        oDBClaimRefParameters.Add("@nResponsibilityNo", MasterTransaction.ResponsibilityNo, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                            //        oDBClaimRefParameters.Add("@sClaimRemittanceRefNo", _sClaimRefNo, ParameterDirection.Input, SqlDbType.VarChar, 50);// numeric(18,2),
                                            //        oDBClaimRefParameters.Add("@nClinicID", MasterTransaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),


                                            //        int _retVal = 0;
                                            //        _retVal = oDBClaimRef.Execute("BL_INUP_ClaimRemittanceRefNo", oDBClaimRefParameters);
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    if (strClaimRemitRefNo != "")
                                            //    {
                                            //        if (MasterTransaction.ResponsibilityType == PayerMode.Insurance)
                                            //        {
                                            //            oDBClaimRefParameters.Add("@nTransactionID", MasterTransaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2), MasterTransaction.TransactionMasterID
                                            //            oDBClaimRefParameters.Add("@nContactID", MasterTransaction.ContactID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                            //            oDBClaimRefParameters.Add("@nInsuranceID", MasterTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                            //            oDBClaimRefParameters.Add("@nResponsibilityNo", MasterTransaction.ResponsibilityNo, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                            //            oDBClaimRefParameters.Add("@sClaimRemittanceRefNo", _sClaimRefNo, ParameterDirection.Input, SqlDbType.VarChar, 50);// numeric(18,2),
                                            //            oDBClaimRefParameters.Add("@nClinicID", MasterTransaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),


                                            //            int _retVal = 0;
                                            //            _retVal = oDBClaimRef.Execute("BL_INUP_ClaimRemittanceRefNo", oDBClaimRefParameters);
                                            //        }
                                            //    }
                                            //}
                                            //oDBClaimRef.Disconnect();
                                            //oDBClaimRef.Dispose();
                                            # endregion


                                        }
                                        else
                                        {
                                            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                                            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                                            try
                                            {
                                                #region " Insurance Plan "
                                                //oDB.Connect(false);
                                                ////Delete the existing Transaction Insurance Plans if save modify
                                                //oDB.Execute_Query("DELETE FROM BL_Transaction_InsPlan WHERE nTransactionID = " + _MasterTransactionID);

                                                ////Add Transaction Insurance Plans 
                                                //if (MasterTransaction.InsurancePlans != null)
                                                //{
                                                //    for (int i = 0; i < MasterTransaction.InsurancePlans.Count; i++)
                                                //    {
                                                //        oDBParameters.Clear();
                                                //        oDBParameters.Add("@nTransactionID", _MasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                                //        oDBParameters.Add("@nPatientID", MasterTransaction.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                                //        oDBParameters.Add("@nClaimNo", MasterTransaction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
                                                //        oDBParameters.Add("@nInsuranceID", MasterTransaction.InsurancePlans[i].InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                                                //        oDBParameters.Add("@nContactID", MasterTransaction.InsurancePlans[i].ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                                                //        oDBParameters.Add("@nResponsibilityNo", MasterTransaction.InsurancePlans[i].ResponsibilityNo, ParameterDirection.Input, SqlDbType.Int);
                                                //        oDBParameters.Add("@nResponsibilityType", MasterTransaction.InsurancePlans[i].ResponsibilityType, ParameterDirection.Input, SqlDbType.Int);
                                                //        oDBParameters.Add("@nClinicID", MasterTransaction.InsurancePlans[i].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                                //        oDB.Execute("BL_INUP_Transaction_InsPlan", oDBParameters);

                                                //    }
                                                //}

                                                //oDB.Dispose();
                                                #endregion

                                                SaveDxList(_MasterTransactionID, MasterTransaction.VisitID, MasterTransaction.ClaimNo, MasterTransaction.PatientID);

                                                # region "Box19 Notes"

                                                ////Insert Box19 Notes in transaction line notes Added by Abhisekh 0n 18 aug 2010
                                                //gloDatabaseLayer.DBLayer oDBBox19Notes = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                                                //gloDatabaseLayer.DBParameters oDBParametersBox19 = new gloDatabaseLayer.DBParameters();
                                                //oDBBox19Notes.Connect(false);
                                                //if (_oBox19Notes != null)
                                                //{
                                                //    if (_oBox19Notes.Count > 0)
                                                //    {
                                                //        for (int j = 0; j < _oBox19Notes.Count; j++)
                                                //        {

                                                //            oDBParametersBox19.Clear();
                                                //            oDBParametersBox19.Add("@nTransactionID", MasterTransaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                                                //            oDBParametersBox19.Add("@nLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                                //            oDBParametersBox19.Add("@nTransactionDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                                //            oDBParametersBox19.Add("@nNoteType", _oBox19Notes[j].NoteType, ParameterDirection.Input, SqlDbType.Int);
                                                //            oDBParametersBox19.Add("@nNoteId", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                                //            oDBParametersBox19.Add("@nNoteDateTime", _oBox19Notes[j].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                                                //            oDBParametersBox19.Add("@nUserID", _oBox19Notes[j].UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                                //            oDBParametersBox19.Add("@sNoteDescription", _oBox19Notes[j].Box19NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                                //            oDBParametersBox19.Add("@nClinicID", _oBox19Notes[j].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                                //            oDBParametersBox19.Add("@MachineID", oDBBox19Notes.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                                                //            oDBParametersBox19.Add("@nBillingNoteType", _oBox19Notes[j].BillingNoteType, ParameterDirection.Input, SqlDbType.Int);
                                                //            oDBParametersBox19.Add("@nCloseDate", null, ParameterDirection.Input, SqlDbType.Int);
                                                //            oDBParametersBox19.Add("@nStatementNoteDate", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                                                //            oDBBox19Notes.Execute("BL_INUP_Transaction_Lines_Notes", oDBParametersBox19);

                                                //            oDBBox19Notes.Disconnect();
                                                //            oDBBox19Notes.Dispose();
                                                //        }
                                                //    }
                                                //    //End Lines Notes
                                                //    if (_oBox19Notes != null)
                                                //        _oBox19Notes.Clear();
                                                //    if (_oBox19Note != null)
                                                //        _oBox19Note = null;

                                                //}
                                                //oDBBox19Notes.Dispose();
                                                # endregion

                                                String strClaimRemitRefNo = "";
                                                strClaimRemitRefNo = gloCharges.CheckRefNumber(MasterTransaction.TransactionMasterID, MasterTransaction.ContactID, MasterTransaction.InsuranceID, MasterTransaction.ClinicID);

                                                # region Claim Remittance Ref. No.

                                                gloDatabaseLayer.DBLayer oDBClaimRef = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                                                gloDatabaseLayer.DBParameters oDBClaimRefParameters = new gloDatabaseLayer.DBParameters();
                                                oDBClaimRef.Connect(false);
                                                if (_sClaimRefNo.Trim() != string.Empty)
                                                {
                                                    if (MasterTransaction.ResponsibilityType == PayerMode.Insurance)
                                                    {
                                                        oDBClaimRefParameters.Add("@nTransactionID", MasterTransaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2), MasterTransaction.TransactionMasterID
                                                        oDBClaimRefParameters.Add("@nContactID", MasterTransaction.ContactID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                                        oDBClaimRefParameters.Add("@nInsuranceID", MasterTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                                        oDBClaimRefParameters.Add("@nResponsibilityNo", MasterTransaction.ResponsibilityNo, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                                        oDBClaimRefParameters.Add("@sClaimRemittanceRefNo", _sClaimRefNo, ParameterDirection.Input, SqlDbType.VarChar, 50);// numeric(18,2),
                                                        oDBClaimRefParameters.Add("@nClinicID", MasterTransaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
                                                        int _retVal = 0;
                                                        _retVal = oDBClaimRef.Execute("BL_INUP_ClaimRemittanceRefNo", oDBClaimRefParameters);
                                                    }
                                                }
                                                else
                                                {
                                                    if (strClaimRemitRefNo != "")
                                                    {
                                                        if (MasterTransaction.ResponsibilityType == PayerMode.Insurance)
                                                        {
                                                            oDBClaimRefParameters.Add("@nTransactionID", MasterTransaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2), MasterTransaction.TransactionMasterID
                                                            oDBClaimRefParameters.Add("@nContactID", MasterTransaction.ContactID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                                            oDBClaimRefParameters.Add("@nInsuranceID", MasterTransaction.InsuranceID, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                                            oDBClaimRefParameters.Add("@nResponsibilityNo", MasterTransaction.ResponsibilityNo, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                                            oDBClaimRefParameters.Add("@sClaimRemittanceRefNo", _sClaimRefNo, ParameterDirection.Input, SqlDbType.VarChar, 50);// numeric(18,2),
                                                            oDBClaimRefParameters.Add("@nClinicID", MasterTransaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
                                                            int _retVal = 0;
                                                            _retVal = oDBClaimRef.Execute("BL_INUP_ClaimRemittanceRefNo", oDBClaimRefParameters);
                                                        }
                                                    }
                                                }
                                                oDBClaimRef.Disconnect();
                                                oDBClaimRef.Dispose();
                                                # endregion
                                                //Added by Subashish_b on 15/Feb /2011 (integration made on date-10/May/2011) for  saving current Transaction in the history table.

                                                MasterTransaction.Transaction_Status = 0;



                                                oDB.Dispose();
                                            }
                                            catch (Exception ex)
                                            {

                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                                            }
                                            finally
                                            {
                                                oDB.Dispose();
                                                oDBParameters.Dispose();
                                            }
                                        }


                                        _ClaimNumberForSaveAndCopay = MasterTransaction.ClaimNo;

                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter the service line to save.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    //if (_returnID > 0)
                                    //{
                                    //    SaveDxList(_returnID, MasterTransaction.VisitID, MasterTransaction.ClaimNo, MasterTransaction.PatientID);
                                    //}


                                    _returnID = MasterTransaction.TransactionID;


                                    #region "Add Next Party Action"

                                    //if (!ogloBilling.IsClaimSplitted(_InitialTransaction))
                                    //{
                                    //    if (MasterTransaction != null && MasterTransaction.Lines != null)
                                    //    {
                                    //        for (int i = 0; i < MasterTransaction.Lines.Count; i++)
                                    //        {

                                    //            MasterTransaction.Lines[i].TransactionId = _InitialTransaction.Lines[i].TransactionId;
                                    //            MasterTransaction.Lines[i].TransactionDetailID = _InitialTransaction.Lines[i].TransactionDetailID;


                                    //            if (_InitialTransaction != null)
                                    //            {
                                    //                if (_InitialTransaction.Transaction_Status != TransactionStatus.InsurancePaid && _IsPartyChanged == false)
                                    //                {
                                    //                    //Updates only the Responsibility No,ContactID and InsuranceID
                                    //                    if (MasterTransaction.Lines[i].InsuranceSelfMode == PayerMode.Self)
                                    //                    {
                                    //                        ogloBilling.UpdateNextParty(MasterTransaction.ClaimNo, MasterTransaction.SubClaimNo, MasterTransaction.TransactionMasterID, MasterTransaction.Lines[i].TransactionMasterDetailID, TransactionID, MasterTransaction.Lines[i].TransactionDetailID, MasterTransaction.PatientID, PayerMode.Self.ToString().ToUpper(), _TempParty, PayerMode.Self, 0, MasterTransaction.Lines[i].Total, MasterTransaction.ClinicID, _NextActionCode, _NextActionDescription, gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text));
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        ogloBilling.UpdateNextParty(MasterTransaction.ClaimNo, MasterTransaction.SubClaimNo, MasterTransaction.TransactionMasterID, MasterTransaction.Lines[i].TransactionMasterDetailID, TransactionID, MasterTransaction.Lines[i].TransactionDetailID, MasterTransaction.Lines[i].InsuranceID, MasterTransaction.Lines[i].InsuranceName.ToUpper(), _TempParty, PayerMode.Insurance, _ContactID, MasterTransaction.Lines[i].Charges, MasterTransaction.Lines[i].ClinicID, _NextActionCode, _NextActionDescription, gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text));

                                    //                    }

                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //}

                                    #endregion "Add Next Party Action"

                                    #region "Send to Queue"
                                    if (_returnID > 0 && _IsOpenForModify == false)
                                    {
                                        if (_IsOpenForResend != true)
                                        {
                                            int _SelfCount = 0;
                                            for (int i = 0; i <= MasterTransaction.Lines.Count - 1; i++)
                                            {
                                                if (MasterTransaction.Lines[i].InsuranceSelfMode == PayerMode.Self)
                                                {
                                                    _SelfCount = _SelfCount + 1;
                                                }
                                            }

                                            Int64 _statusid = 0;
                                            Int64 _statusdate = 0;
                                            Int64 _statustime = 0;


                                            Int64 _BatchDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                                            Int64 _Prefix = Convert.ToInt64(DateTime.Now.ToString("MMddyyyyhhmmss"));

                                            //For self no need of clearinghouse, but as per table structure we will send it blank
                                            //Int64 _ClearingHouseId = 0;
                                            //string _ClearingHouseCode = "";
                                            // string _ClearingHouseName = "";

                                            if (MasterTransaction.Lines.Count != _SelfCount)
                                            {
                                                //_ClaimNo = Convert.ToInt64(txtClaimNo.Text);
                                                _ClaimNo = Convert.ToInt64(sClaimNo);

                                                _statusdate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                                                _statustime = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToShortTimeString());

                                                _statusid = ogloBilling.UpdateTransactionStatus(_TransPatientID, 0, 0, "", 0, BatchType.Queue.GetHashCode(), 0, _returnID, _ClaimNo, 0, 0, TransactionStatus.Queue, _statusdate, _statustime, "", this.ClinicID, 0, gloPatient.TypeOfBilling.None);
                                                ogloBilling.UpdateCurrentStatus(_returnID, TransactionStatus.Queue, _statusid);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region "Self Pay goes to accpeted - Vinayak - 20090722"
                                    //IF All service line are self paid then send to accepted
                                    //if its new then send to new batch and accepted
                                    //if modify then
                                    //if charges then send to new batch and accepted
                                    //if queue then send to new batch and accepted/
                                    //if batch then send to accpeted against existing batch

                                    if (_returnID > 0)
                                    {
                                        if (_IsOpenForResend != true)
                                        {
                                            int _SelfCount = 0;
                                            for (int i = 0; i <= MasterTransaction.Lines.Count - 1; i++)
                                            {
                                                if (MasterTransaction.Lines[i].InsuranceSelfMode == PayerMode.Self)
                                                {
                                                    _SelfCount = _SelfCount + 1;
                                                }
                                            }


                                            Int64 _statusid = 0;
                                            Int64 _statusdate = 0;
                                            Int64 _statustime = 0;

                                            Int64 _BatchDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                                            Int64 _Prefix = Convert.ToInt64(DateTime.Now.ToString("MMddyyyyhhmmss"));


                                            if (MasterTransaction.Lines.Count == _SelfCount)
                                            {
                                                //_ClaimNo = Convert.ToInt64(txtClaimNo.Text);
                                                _ClaimNo = Convert.ToInt64(sClaimNo);

                                                #region "Send to Queue"
                                                _statusdate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                                                _statustime = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToShortTimeString());

                                                _statusid = ogloBilling.UpdateTransactionStatus(_TransPatientID, 0, 0, "", 0, BatchType.Queue.GetHashCode(), 0, _returnID, _ClaimNo, 0, 0, TransactionStatus.Queue, _statusdate, _statustime, "", this.ClinicID, 0, gloPatient.TypeOfBilling.None);
                                                ogloBilling.UpdateCurrentStatus(_returnID, TransactionStatus.Queue, _statusid);

                                                #endregion
                                                _ClaimNo = 0;
                                            }


                                        }
                                    }
                                    #endregion


                                    #region "Resend logic - send to reque with saving to history - Sagar - 20090729"
                                    if (_returnID > 0)
                                    {
                                        if (_IsOpenForResend == true)
                                        {
                                            Int64 _statusid = 0;
                                            Int64 _statusdate = 0;
                                            Int64 _statustime = 0;

                                            string _BatchName = "";
                                            Int64 _BatchId = 0;
                                            Int64 _BatchNo = 0;
                                            Int64 _BatchDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                                            Int64 _Prefix = Convert.ToInt64(DateTime.Now.ToString("MMddyyyyhhmmss"));

                                            //For self no need of clearinghouse, but as per table structure we will send it blank
                                            // Int64 _ClearingHouseId = 0;
                                            // string _ClearingHouseCode = "";
                                            // string _ClearingHouseName = "";

                                            //_ClaimNo = Convert.ToInt64(txtClaimNo.Text);
                                            _ClaimNo = Convert.ToInt64(sClaimNo);
                                            #region "Send to Re-Queue"
                                            _statusdate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                                            _statustime = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToShortTimeString());

                                            _statusid = ogloBilling.UpdateTransactionStatus(_TransPatientID, 0, 0, "", 0, BatchType.Queue.GetHashCode(), 0, _returnID, _ClaimNo, 0, 0, TransactionStatus.ReQueue, _statusdate, _statustime, "", this.ClinicID, 0, gloPatient.TypeOfBilling.None);
                                            ogloBilling.UpdateCurrentStatus(_returnID, TransactionStatus.ReQueue, _statusid);

                                            #endregion


                                            #region "Send to Accepted"
                                            //In resend if user select all to self then again send to accepted

                                            int _SelfCount = 0;
                                            for (int i = 0; i <= MasterTransaction.Lines.Count - 1; i++)
                                            {
                                                if (MasterTransaction.Lines[i].InsuranceSelfMode == PayerMode.Self)
                                                {
                                                    _SelfCount = _SelfCount + 1;
                                                }
                                            }

                                            if (_SelfCount == MasterTransaction.Lines.Count)
                                            {
                                                _statusid = ogloBilling.UpdateTransactionStatus(_TransPatientID, 0, _BatchId, _BatchName, _BatchDate, BatchType.Batch.GetHashCode(), _BatchNo, _returnID, _ClaimNo, 0, 0, TransactionStatus.Accepted, _statusdate, _statustime, "", this.ClinicID, 0, gloPatient.TypeOfBilling.None);
                                                ogloBilling.UpdateCurrentStatus(_returnID, TransactionStatus.Accepted, _statusid);
                                            }
                                            #endregion

                                            _ClaimNo = 0;

                                        }
                                    }
                                    #endregion


                                    if (_returnID > 0)
                                    {
                                        if (_IsOpenForModify == false)
                                        {
                                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Add, "Charges added", MasterTransaction.PatientID, _returnID, MasterTransaction.ProviderID, ActivityOutCome.Success);
                                        }
                                        else
                                        {
                                            ogloBilling.Dispose();
                                            if (_IsSaveCopayStart == false) { this.Close(); }
                                        }



                                        IsEMRTransactionSaved = true;
                                        _result = true;
                                    }
                                    else
                                    {
                                        if (_IsOpenForModify == false)
                                        {
                                            //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Add, "Charges added", MasterTransaction.PatientID, _returnID, MasterTransaction.ProviderID, ActivityOutCome.Failure);
                                        }
                                        else
                                        {
                                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Modify, "Charges Modified", MasterTransaction.PatientID, _returnID, MasterTransaction.ProviderID, ActivityOutCome.Failure);
                                        }
                                    }

                                    #region " Claim Split Code after Responsiblity Transfer "

                                    //Added By Debasish Das on 16th July 2010 (5060)
                                    if (_IsPartyChanged == true)
                                    {
                                        Int64 nNewTransID = 0;
                                       // Transaction oTransaction = new Transaction();
                                        _InitialTransaction.Hold = _oClaimHold;
                                        MasterTransaction.TransactionID = _InitialTransaction.TransactionID;
                                        MasterTransaction.TransactionMasterID = _InitialTransaction.TransactionMasterID;
                                        MasterTransaction.ClinicID = _ClinicID;
                                        oSplitClaimDetails = new SplitClaimDetails();
                                        oSplitClaimDetails.GenerateNewClaimOnRespTransfer(this._DatabaseConnectionString, this._emrdatabaseConnectionString, MasterTransaction, _InitialTransaction, _TempParty, _InsTransferCloseDate, _NextActionCode, _NextActionDescription, _ContactID, out nNewTransID);

                                    }
                                    //** 

                                    #endregion

                                }
                                else
                                {
                                    c1Dx.Focus();
                                    if (c1Dx.Rows.Count >= 2)
                                    {
                                        c1Dx.Select(1, COL_DX_SELECT);
                                    }
                                    _result = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please select diagnosis.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1Dx.Focus();
                                if (c1Dx.Rows.Count >= 2)
                                {
                                    c1Dx.Select(1, COL_DX_SELECT);
                                }
                                _result = false;
                            }
                        }

                      //  if (UC_gloBillingTransactionLines.AutoSort != null)
                        {

                            #region "Update IsTransaction Lines Sorted --- allow it to sort only once"

                            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                            string strQuery = "";
                            try
                            {
                                oDB.Connect(false);
                                strQuery = "Update BL_Transaction_Claim_MST  WITH(READPAST) SET bSorted = " + UC_gloBillingTransactionLines.AutoSort.GetHashCode() + " WHERE nTransactionMasterID = " + _MasterTransactionID + "";
                                oDB.Execute_Query(strQuery);
                                oDB.Disconnect();
                            }
                            catch (Exception Ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, true);
                            }
                            finally
                            {
                                oDB.Dispose();
                            }

                            #endregion

                        }
                    }

                }




                #endregion

                //MaheshB 20100424
                #region "RESEND & Hold CLAIM"

                clsgloResend objResend = null;
                try
                {
                    if (_IsResnd == true && _oClaimHold.IsHold == true)
                    {

                        #region "Remove Claim From Batch according to Transaction Status"

                        switch (_InitialTransaction.Transaction_Status)
                        {
                            case TransactionStatus.Batch:
                                oClsBL_Hold.RemoveClaimsForHold(_InitialTransaction.TransactionID, _InitialTransaction.Transaction_Status);
                                break;
                            case TransactionStatus.SendToClaimManager:
                                oclsgloResend.ResendClaim(_InitialTransaction.TransactionID, _InitialTransaction, _oClaimHold.IsHold, _sClaimRefNo);
                                break;
                            case TransactionStatus.SendToClearingHouse:
                                oclsgloResend.ResendClaim(_InitialTransaction.TransactionID, _InitialTransaction, _oClaimHold.IsHold, _sClaimRefNo);
                                break;
                            default:

                                break;

                        }

                        #endregion

                    }
                    else
                    {
                        if (_oClaimHold.IsHold == true)
                        {
                            switch (_InitialTransaction.Transaction_Status)
                            {
                                case TransactionStatus.Batch:
                                    oClsBL_Hold.RemoveClaimsForHold(_InitialTransaction.TransactionID, _InitialTransaction.Transaction_Status);
                                    break;
                                case TransactionStatus.SendToClaimManager:
                                    oClsBL_Hold.HoldUnholdClaim(_oClaimHold, _InitialTransaction.TransactionMasterID, _InitialTransaction.TransactionID);
                                    break;
                                case TransactionStatus.SendToClearingHouse:
                                    oClsBL_Hold.HoldUnholdClaim(_oClaimHold, _InitialTransaction.TransactionMasterID, _InitialTransaction.TransactionID);
                                    break;
                                default:
                                    break;

                            }
                        }
                        else if (_IsResnd == true)
                        {
                            objResend = new clsgloResend();
                            objResend.ResendClaim(_TransactionID, _InitialTransaction, _oClaimHold.IsHold, _sClaimRefNo);
                        }

                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                }
                finally
                {
                    if (objResend != null) { objResend.Dispose(); }

                }

                #endregion

                #region " Remove Claim From Batch if Responsible Plan is on Hold "
                if (!_oClaimHold.IsHold)
                {
                    oClsBL_Hold.RemovePlanHoldClaimsFromBatch(Convert.ToInt64(c1Insurance.GetData(Convert.ToInt16(nCurrentResponsibleParty), COL_INSURANCECONTACTID)), _InitialTransaction.TransactionID);
                }
                #endregion

                if (_result == true)
                    if (MasterTransaction.SubClaimNo == "")
                        _TransactionID = ogloBilling.GetBillingTransactionTrackingID(MasterTransaction.ClaimNo);
                    else
                        _TransactionID = ogloBilling.GetBillingTransactionTrackingID(MasterTransaction.ClaimNo, Convert.ToInt64(MasterTransaction.SubClaimNo));


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                // gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Add, "Exception while Add Charges : " + ex.Message + " ", MasterTransaction.PatientID, 0, MasterTransaction.ProviderID, ActivityOutCome.Failure);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (oClsBL_Hold != null) { oClsBL_Hold.Dispose(); }
                if (MasterTransaction != null) { MasterTransaction.Dispose(); }
                if (Insurance != null) { Insurance.Dispose(); }

                if (oLineTransactions != null) { oLineTransactions.Dispose(); }

            }
            return _result;
        }

        public Boolean SaveClaim(dsChargesTVP dsChargesTVP)
        {
          //  Int64 TransactionMasterID = 0;
            string sTransResult = "";
          //  object _oTransactionMstID = null;
            string _ValidDxList = "''";
            object _oNewClaimNo = null;
            Boolean _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                if (dsChargesTVP.BL_Transaction_Claim_MST != null && dsChargesTVP.BL_Transaction_Claim_MST.Rows.Count > 0)
                {

                    //Get all the Dx from the arraylist to be deleted
                    if (_arrDxCodes != null && _arrDxCodes.Count > 0)
                    {
                        for (int i = 0; i <= _arrDxCodes.Count - 1; i++)
                        {
                            _ValidDxList += ",'" + _arrDxCodes[i].ToString() + "'";
                        }
                    }

                    oDB.Connect(false);

                    oParameters.Add("@tvpClaimMST", dsChargesTVP.Tables["BL_Transaction_Claim_MST"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpClaim_Lines", dsChargesTVP.Tables["BL_Transaction_Claim_Lines"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpClaimDx", dsChargesTVP.Tables["BL_Transaction_Diagnosis"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpClaim_Lines_Notes", dsChargesTVP.Tables["BL_Transaction_Lines_Notes"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpClaim_InsurancePlans", dsChargesTVP.Tables["BL_Transaction_InsPlan"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpNextAction", dsChargesTVP.Tables["BL_EOB_NextAction"], ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@sExcludeTransMstDetailID", _sbExcludeTransMStDtlID.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 1000);
                    oParameters.Add("@sExcludeTransDetailID", _sbExcludeTransDtlID.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 1000);
                    oParameters.Add("@sExcludeNoteID", _sbExcludeNoteID.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 1000);
                    oParameters.Add("@sTranResult", sTransResult, ParameterDirection.Output, SqlDbType.VarChar, 1000);

                    oDB.Execute("ModifyCharges_TVP", oParameters, out _oNewClaimNo);
                    oDB.Disconnect();


                    if (_oNewClaimNo != null)
                    {
                        sTransResult = Convert.ToString(_oNewClaimNo);
                    }
                    else
                    {
                        sTransResult = "";
                    }

                    if (sTransResult == "Updated")
                    {

                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(sTransResult, true);
                    }
                    _result = true;
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _result;

        }

        private void SetMasterClaimDetails_TVP(dsChargesTVP dsChargesTVP)
        {

            //bool _IsPartyChanged = false;
            string _InsTransferCloseDate = "";
            Int64 _MasterTransactionAppointmentId = 0;
            Int64 _MasterTransactionVisitId = 0;
            //Transaction MasterTransaction = new Transaction();


            try
            {


                //***********************************************
                //Fields not implemented yet, so hard coded
                _MasterTransactionAppointmentId = 999;
                _MasterTransactionVisitId = 987;
                //***********************************************

                this.PatientID = oPatientControl.CmbSelectedPatientID;
                this.nAccountPatientID = oPatientControl.AccountPatientID;
                this.nPAccountID = oPatientControl.PAccountID;
                this.nGuarantorID = oPatientControl.GuarantorID;


                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows.Add();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClaimNo"] = Convert.ToInt64(sClaimNo);
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nSubClaimNo"] = txtClaimNo.Tag.ToString();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionDate"] = gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text.ToString());
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sCaseNoPrefix"] = "Claim";
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAppointmentID"] = _MasterTransactionAppointmentId;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sFacilityCode"] = Convert.ToString(cmbFacility.SelectedValue);
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sFacilityDescription"] = Convert.ToString(cmbFacility.Text);
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sMaritalStatus"] = oPatientControl.PatientsMaritalStatus; ;
                //MasterTransaction.PrefixID = _MasterTransactionPrefixId;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionProviderID"] = Convert.ToInt64(cmbBillingProvider.SelectedValue);
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nPatientID"] = _TransPatientID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nPAccountID"] = this.nPAccountID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nGuarantorID"] = this.nGuarantorID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAccountPatientID"] = this.nAccountPatientID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUserID"] = this.UserID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUserName"] = this.UserName;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClinicId"] = _ClinicID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionType"] = TransactionType.Billed;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nVisitID"] = _MasterTransactionVisitId;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nPatientID"] = this.PatientID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nPAccountID"] = this.nPAccountID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nGuarantorID"] = this.nGuarantorID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAccountPatientID"] = this.nAccountPatientID;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nChargesDayTrayID"] = _selectedChargeTrayId;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sChargesTrayCode"] = "";
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sChargesTrayDescription"] = _selectedChargeTrayDescription;

                if (_IsOpenForModify == true)
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionMasterID"] = _MasterTransactionID;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionID"] = _TransactionID;
                }
                else
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionID"] = _TransactionID;
                }

                if (cmbState != null && cmbState.Items.Count > 0 & cmbState.SelectedIndex != -1)
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sState"] = cmbState.Text;
                }

                #region "worker Comp "

                if (CmbAccidentType.Text.Trim() == "Work")
                {

                    mskInjuryDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if (IsValidDate(mskInjuryDate.Text))
                    { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nInjuryDate"] = gloDateMaster.gloDate.DateAsNumber(mskInjuryDate.Text.ToString()); }
                    else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nInjuryDate"] = 0; }

                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sWorkersCompNo"] = cmbClaimNo.Text;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bWorkersComp"] = true;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsWorkersCompOnCMS1500"] = chkPrintWorkersComp.Checked;


                }
                else if (CmbAccidentType.Text.Trim() == "Auto")
                {

                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sWorkersCompNo"] = cmbClaimNo.Text;
                    mskAccidentDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                    if (IsValidDate(mskAccidentDate.Text))
                    { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAccidentDate"] = gloDateMaster.gloDate.DateAsNumber(mskAccidentDate.Text.ToString()); }
                    else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAccidentDate"] = 0; }


                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bAutoClaim"] = true;

                }
                else if (CmbAccidentType.Text.Trim() == "Other")
                {
                    mskOtherDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                    if (IsValidDate(mskOtherDate.Text))
                    { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOtherAccidentDate"] = gloDateMaster.gloDate.DateAsNumber(mskOtherDate.Text.ToString()); }
                    else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOtherAccidentDate"] = 0; }

                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bOtherAccident"] = true;

                }
                else
                {
                    mskOnsiteDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                    if (IsValidDate(mskOnsiteDate.Text))
                    { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOnsiteDate"] = gloDateMaster.gloDate.DateAsNumber(mskOnsiteDate.Text.ToString()); }
                    else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOnsiteDate"] = 0; }

                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bWorkersComp"] = false;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bAutoClaim"] = false;
                }


                //if (IsValidDate(mskUnableFromDate.Text))
                //{ dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkFromDate"] = gloDateMaster.gloDate.DateAsNumber(mskUnableFromDate.Text.ToString()); }
                //else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkFromDate"] = 0; }

                if (_UnableToWorkFromDate_MoreClaimData > 0)
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkFromDate"] = _UnableToWorkFromDate_MoreClaimData; }
                else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkFromDate"] = 0; }


                //if (IsValidDate(mskUnableTillDate.Text))
                //{ dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkTillDate"] = gloDateMaster.gloDate.DateAsNumber(mskUnableTillDate.Text.ToString()); }
                //else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkTillDate"] = 0; }

                if (_UnableToWorkTillDate_MoreClaimData > 0)
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkTillDate"] = _UnableToWorkTillDate_MoreClaimData; }
                else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nUnableToWorkTillDate"] = 0; }


                #endregion

                #region "Other Dates"

                if (IsValidDate(mskHospitaliztionFrom.Text))
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHopitalizationDateFrom"] = gloDateMaster.gloDate.DateAsNumber(mskHospitaliztionFrom.Text.ToString()); }
                else
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHopitalizationDateFrom"] = 0; }

                if (IsValidDate(mskHospitaliztionTo.Text))
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHopitalizationDateTo"] = gloDateMaster.gloDate.DateAsNumber(mskHospitaliztionTo.Text.ToString()); }
                else
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHopitalizationDateTo"] = 0; }

                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bOutSideLab"] = chkOutSideLab.Checked;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dOutSideLabCharges"] = Convert.ToDecimal(txtOutSideLabCharges.Text.Trim());

                #endregion

                #region " Insurance "

                //MasterTransaction.Insurances = new TransactionInsurances();
                //Insurance.ClinicID = _ClinicID;
                //Insurance.TransactionID = MasterTransaction.TransactionID;  //oLineTransactions[0].TransactionId;
                //Insurance.TransactionDetailID = 0;
                //Insurance.TransactionLineNo = 0;
                //MasterTransaction.Insurances.Add(Insurance);

                #endregion

                #region "Prior Authorization"

                if (txtPriorAuthorizationNo.Tag != null)
                {
                    Int64 _PriorAuthorizationNo = 0;
                    if (Int64.TryParse(txtPriorAuthorizationNo.Tag.ToString(), out _PriorAuthorizationNo))
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nAuthorizationID"] = Convert.ToInt64(txtPriorAuthorizationNo.Tag);

                        //if (!MasterTransaction.PriorAuthorizationID.Equals(0))
                        //{
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sAuthorizationNumber"] = txtPriorAuthorizationNo.Text.Trim();
                        //}
                    }
                }

                #endregion


                #region "Billing and Referral Provider"

                if (cmbReferralProvider.Text != "")
                {
                    if (cmbReferralProvider.Items.Count > 0 && cmbReferralProvider.SelectedIndex != -1)
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nReferralProviderID"] = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nReferralID"] = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                    }
                }
                if (chk_SameasBillingProvider.Checked)
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bSameAsBillingProvider"] = true; }
                else { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bSameAsBillingProvider"] = false; }

                #endregion

                //MasterTransaction.SendCounter = 0;
                // MasterTransaction.SendToRejection = 0;

                #region "Claim Status"

                if (_IsOpenForModify == true || _IsBatchModify == true)
                {
                    if (txtClaimLastStatus.Text.Trim() != "")
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nStatus"] = ((TransactionStatus)Convert.ToInt32(txtClaimLastStatus.Text.Trim()));
                    }

                    if (txtSendCounter.Text.Trim() != "")
                    {
                        //MasterTransaction.SendCounter = Convert.ToInt32(txtSendCounter.Text); 
                    }

                    if (txtSendToRejection.Text.Trim() != "")
                    {
                        //MasterTransaction.SendToRejection = Convert.ToInt32(txtSendToRejection.Text); 
                    }
                }
                else
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nStatus"] = TransactionStatus.Queue;
                }

                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nLastStatusId"] = Convert.ToInt64(txtLastStatusId.Text.Trim());
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nSendToInsFlag"] = this._IsResendToInsType;


                #endregion

                #region " Insurance Plan "

                if (oPatientControl != null)
                {
                    int _rowno = 0;

                    //DataTable _dtInsurance;
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
                                        _InsurancePlan.TransactionId = _MasterTransactionID;//_TransactionID;
                                        _InsurancePlan.PatientId = _TransPatientID;
                                        _InsurancePlan.ClaimNo = _ClaimNo;
                                        _InsurancePlan.InsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                                        _InsurancePlan.ContactID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCECONTACTID));



                                        _InsurancePlan.ResponsibilityNo = _Party;
                                        _InsurancePlan.ResponsibilityType = Convert.ToInt16(c1Insurance.GetData(i, COL_INSSELFMODE));

                                        if (_TempParty == _Party)
                                        {
                                            _TempParty = _Party;
                                            _InsuranceID = _InsurancePlan.InsuranceID;
                                            _InsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                                            _InsuranceSelfMode = (PayerMode)_InsurancePlan.ResponsibilityType;
                                            _ContactID = _InsurancePlan.ContactID;

                                            if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE)) == InsuranceTypeFlag.Primary.ToString())
                                            { _InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Primary; }
                                            else if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE)) == InsuranceTypeFlag.Secondary.ToString())
                                            { _InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Secondary; }
                                            else if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE)) == InsuranceTypeFlag.Tertiary.ToString())
                                            { _InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Tertiary; }
                                            else
                                            { _InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.None; }

                                        }

                                        _InsurancePlan.ClinicID = _ClinicID;

                                        //MasterTransaction.InsurancePlans.Add(_InsurancePlan);

                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows.Add();
                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nTransactionID"] = 0;
                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nPatientID"] = _TransPatientID;
                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nClaimNo"] = 0;
                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nInsuranceID"] = _InsurancePlan.InsuranceID;
                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nContactID"] = _InsurancePlan.ContactID;
                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nResponsibilityNo"] = _InsurancePlan.ResponsibilityNo;
                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nResponsibilityType"] = _InsurancePlan.ResponsibilityType;
                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["nClinicId"] = _ClinicID;
                                        dsChargesTVP.Tables["BL_Transaction_InsPlan"].Rows[_rowno]["sClaimRemittanceRefNo"] = "";

                                        _rowno = _rowno + 1;
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }

                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nInsuranceID"] = _InsuranceID;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nContactID"] = _ContactID;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nResponsibilityNo"] = 1;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nResponsibilityType"] = _InsuranceSelfMode;

                }

                #endregion

                #region "Responsibility Transfer"

                if (_IsPartyChanged == true)
                {
                    frmInsTransCloseDate ofrmInsTransCloseDate = new frmInsTransCloseDate(_DatabaseConnectionString, _InitialTransaction.TransactionID, _InitialTransaction.TransactionMasterID, mskClaimDate.Text);
                    ofrmInsTransCloseDate.ShowDialog(this);

                    if (ofrmInsTransCloseDate.oDialogResult)
                    {
                        _InsTransferCloseDate = ofrmInsTransCloseDate.InsTransferCloseDate;
                        responsiblitytransferdate = _InsTransferCloseDate;
                        ofrmInsTransCloseDate.Dispose();
                    }
                    else
                    {
                        ofrmInsTransCloseDate.Dispose();
                        responsiblitytransferdate = string.Empty;
                        _IsPartyChanged = false;
                    }
                }

                #endregion

                #region " Fee-Schedule "

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

                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFeeScheduleType"] = UC_gloBillingTransactionLines.Fee_ScheduleType;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFeeScheduleID"] = UC_gloBillingTransactionLines.Fee_ScheduleID;
                }


                if (chkFacilityFeeSchedule.Checked == true)
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFacilityType"] = FacilityType.Facility.GetHashCode(); }
                else if (chkNonFacilityCharges.Checked == true)
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFacilityType"] = FacilityType.NonFacility.GetHashCode(); }
                else
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nFacilityType"] = FacilityType.None.GetHashCode(); }



                #endregion " Fee-Schedule "

                #region " More Claim Data "

                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsReplacementClaim"] = _IsbCliamReplacement;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClaimType"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nIllnessDate"] = _IllnessDate;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sClaimRemittanceRefNo"] = _sClaimRefNo.Trim();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sDelayReasonCodeID"] = _sDelayReasonCode.Trim();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sServiceAuthExceCode"] = _sServiceAuthExceptionCode.Trim();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sPWKReportTypeCode"] = _sPWKReportTypeCode.Trim();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sPWKReportTransmissionCode"] = _sPWKReportTransmissionCode.Trim();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sPWKAttachmentControlNumber"] = _sPWKAttachmentControlNumber.Trim();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sMammogramCertNumber"] = _sMammogramCertNumber.Trim();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sIDENo"] = _sIDENo.Trim();
                #endregion

                #region " Hold Claim "

                if (_oClaimHold.IsHold)
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsHold"] = _oClaimHold.IsHold;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sHoldReason"] = _oClaimHold.HoldReason;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtHoldDate"] = _oClaimHold.HoldDateTime;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHoldUserID"] = _oClaimHold.HoldUserID;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtHoldModifyDateTime"] = _oClaimHold.HoldModDateTime;

                }
                else
                {
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsHold"] = DBNull.Value;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sHoldReason"] = DBNull.Value;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtHoldDate"] = DBNull.Value;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nHoldUserID"] = DBNull.Value;
                    dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtHoldModifyDateTime"] = DBNull.Value;

                }

                #endregion

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
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bSorted"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nParentTransactionID"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nParentClaimNo"] = "";
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsOpened"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sMachineID"] = DBNull.Value;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bCheckedIn"] = true;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sMainClaimNo"] = "";
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["bIsRebilled"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nEOBPaymentID"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nEOBID"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nCreditID"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOldEOBID"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nOldEobpaymentID"] = 0;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nCaseID"] = Convert.ToInt64(txtCases.Tag);

                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sProviderQualifier"] = cmbProviderType.SelectedValue;
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sBox15DateQualifier"] = cmbBox15DateQualifier.SelectedValue;
                if (mskBox15Date.MaskCompleted)
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtBox15Date"] = mskBox15Date.Text; }

                else
                { dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["dtBox15Date"] = DBNull.Value; }


                #region"UB04 Settings"

                if (_dtUB04Settings != null && _dtUB04Settings.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtUB04Settings.Rows.Count; i++)
                    {
                        if (Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsName"]).Trim() == "UB04_TypeOfBill")
                        {
                            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04TypeOfBill"] = Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsValue"]).Trim();
                        }
                        else if (Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsName"]).Trim() == "UB04_AdmisionType")
                        {
                            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04AdmisionType"] = Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsValue"]).Trim(); ;
                        }
                        else if (Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsName"]).Trim() == "UB04_AdmisionSource")
                        {
                            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04AdmisionSource"] = Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsValue"]).Trim(); ;
                        }
                        else if (Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsName"]).Trim() == "UB04_DischargeStatus")
                        {
                            dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["sUB04DischargeStatus"] = Convert.ToString(_dtUB04Settings.Rows[i]["sSettingsValue"]).Trim(); ;
                        }

                    }
                }

                #endregion

                dsChargesTVP.Tables["BL_Transaction_InsPlan"].AcceptChanges();
                dsChargesTVP.Tables["BL_Transaction_Claim_MST"].AcceptChanges();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            
        }

        private void SetServiceLineDetails_TVP(dsChargesTVP dsChargesTVP)
        {
            TransactionLines oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();
            int _NoteIndex = 0;
            DataTable _dtLinesUniqueIds = null;
            StringBuilder _sCPTCode = new StringBuilder();
            DataTable _dtRevenueCode = null;
            _sbExcludeTransDtlID = new StringBuilder();
            _sbExcludeTransMStDtlID = new StringBuilder();
            _sbExcludeNoteID = new StringBuilder();

            try
            {

                if (oLineTransactions != null && oLineTransactions.Count > 0)
                {

                    #region "Get PartyNumber"

                    _dtParty = gloCharges.GetCurrentPartyNumber(_TransactionID, _MasterTransactionID);

                    if (_dtParty != null && _dtParty.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(_dtParty.Rows[0]["PartyNo"]) > 0)
                        {
                            _TempParty = Convert.ToInt16(_dtParty.Rows[0]["PartyNo"]);
                            _NextActionCode = Convert.ToString(_dtParty.Rows[0]["sNextActionCode"]);
                        }
                        //Code Added By Debasish
                        for (int iCount = 1; iCount <= c1Insurance.Rows.Count - 1; iCount++)
                        {
                            if (c1Insurance.GetCellCheck(iCount, COL_SELECT_CURRENT_RESPONSIBLE_PARTY) == CheckEnum.Checked)
                            {
                                _TempParty = Convert.ToInt16(iCount);
                            }
                        }
                        //**
                    }
                    if (_dtParty != null)
                    {
                        _dtParty.Dispose();
                        _dtParty = null;
                    }
                    #endregion

                    //..Get Unique Ids
                    _dtLinesUniqueIds = GetUniqueIDsForLines(oLineTransactions.Count);


                    #region "Revenue Code For CPT's"
                    //Get the Revenue Code For Eaach CPT
                    for (int i = 0; i <= oLineTransactions.Count - 1; i++)
                    {

                        if (i == oLineTransactions.Count - 1)
                        {
                            _sCPTCode.Append(oLineTransactions[i].CPTCode.ToString());
                            _sbExcludeTransDtlID.Append(oLineTransactions[i].TransactionDetailID.ToString());
                            _sbExcludeTransMStDtlID.Append(oLineTransactions[i].TransactionMasterDetailID.ToString());
                        }
                        else
                        {
                            _sCPTCode.Append(oLineTransactions[i].CPTCode.ToString() + ",");
                            _sbExcludeTransDtlID.Append(oLineTransactions[i].TransactionDetailID.ToString() + ",");
                            _sbExcludeTransMStDtlID.Append(oLineTransactions[i].TransactionMasterDetailID.ToString() + ",");

                        }


                        oLineTransactions[i].InsuranceID = _InsuranceID;
                        oLineTransactions[i].InsuranceName = _InsuranceName;
                        oLineTransactions[i].InsurancePrimarySecondaryTertiary = _InsurancePrimarySecondaryTertiary.ToString();
                        oLineTransactions[i].InsuranceSelfMode = _InsuranceSelfMode;


                    }

                    _dtRevenueCode = GetRevenueCodeForCPT(_sCPTCode);


                    #endregion

                    for (int i = 0; i <= oLineTransactions.Count - 1; i++)
                    {
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows.Add();

                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionMasterID"];
                        if (oLineTransactions[i].TransactionMasterDetailID == 0)
                        {
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterDetailID"] = Convert.ToInt64(_dtLinesUniqueIds.Rows[i]["ID"]);
                        }
                        else
                        {
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterDetailID"] = oLineTransactions[i].TransactionMasterDetailID;
                        }

                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionID"];
                        if (oLineTransactions[i].TransactionDetailID == 0)
                        {
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionDetailID"] = Convert.ToInt64(_dtLinesUniqueIds.Rows[i]["ID2"]);
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sRowStateType"] = "Insert";
                        }
                        else
                        {
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionDetailID"] = oLineTransactions[i].TransactionDetailID;
                            dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sRowStateType"] = "Update";
                        }

                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionLineNo"] = oLineTransactions[i].TransactionLineId;
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nFromDate"] = gloDateMaster.gloDate.DateAsNumber(oLineTransactions[i].DateServiceFrom.ToShortDateString());
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nToDate"] = gloDateMaster.gloDate.DateAsNumber(oLineTransactions[i].DateServiceTill.ToShortDateString());
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
                        dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["sRevenueCode"] = Convert.ToString(_dtRevenueCode.Select("sCPTCode ='" + oLineTransactions[i].CPTCode + "'")[0]["sRevenueCode"]);

                        #region " Notes "

                        if (oLineTransactions[i].LineNotes.Count > 0)
                        {
                            for (int j = 0; j < oLineTransactions[i].LineNotes.Count; j++)
                            {

                                if (i == oLineTransactions.Count - 1)
                                {
                                    _sbExcludeNoteID.Append(oLineTransactions[i].LineNotes[j].NoteID.ToString());
                                }
                                else
                                {
                                    _sbExcludeNoteID.Append(oLineTransactions[i].LineNotes[j].NoteID.ToString() + ",");
                                }


                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows.Add();
                                if (oLineTransactions[i].LineNotes[j].TransactionID != 0)
                                {
                                    dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nTransactionID"] = oLineTransactions[i].LineNotes[j].TransactionID;
                                }
                                else
                                {
                                    dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nTransactionID"] = 0;
                                }
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nLineNo"] = oLineTransactions[i].TransactionLineId;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nTransactionDetailID"] = dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterDetailID"];
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nNoteType"] = oLineTransactions[i].LineNotes[j].NoteType;
                                if (oLineTransactions[i].LineNotes[j].NoteID != 0)
                                {
                                    dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nNoteId"] = oLineTransactions[i].LineNotes[j].NoteID;
                                }
                                else
                                {
                                    dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nNoteId"] = 0;

                                }
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nNoteDateTime"] = DBNull.Value;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["sNoteDescription"] = oLineTransactions[i].LineNotes[j].NoteDescription;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nUserID"] = oLineTransactions[i].LineNotes[j].UserID;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nClinicID"] = oLineTransactions[i].LineNotes[j].ClinicID;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nCloseDate"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nTransactionDate"];//Needs To be implemented
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nBillingNoteType"] = oLineTransactions[i].LineNotes[j].BillingNoteType;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["nStatementNoteDate"] = oLineTransactions[i].LineNotes[j].StatementNoteDate;
                                dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].Rows[_NoteIndex]["dtCreatedDateTime"] = oLineTransactions[i].LineNotes[j].dtCreatedDatetime;
                                _NoteIndex = _NoteIndex + 1;
                            }

                        }

                        #endregion

                        #region " Next Action "

                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows.Add();

                        DataRow[] dr;
                        dr = _dtNextActionID.Select("nBillingTransactionDetailID = " + dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterDetailID"] + " and nTrackMstTrnDetailID = " + dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionDetailID"] + "");
                        if (dr.Length > 0)
                        {

                            dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nID"] = Convert.ToInt64(dr[0]["nID"]);
                        }

                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nClaimNo"] = oLineTransactions[i].ClaimNumber;//dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nContactID"]
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nEOBPaymentID"] = 0;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nEOBID"] = 0;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nEOBPaymentDetailID"] = 0;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nBillingTransactionID"] = _MasterTransactionID;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nBillingTransactionDetailID"] = dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionMasterDetailID"];
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nLineNo"] = i + 1;

                        if (oLineTransactions[i].InsuranceSelfMode == PayerMode.Insurance)
                        {
                            dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPatientInsID"] = oLineTransactions[i].InsuranceID;
                        }
                        else
                        {
                            dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPatientInsID"] = 0;
                        }

                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPatientInsName"] = oLineTransactions[i].InsuranceName;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionPartyNumber"] = _TempParty;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextActionContactID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nContactID"];
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sNextActionCode"] = _NextActionCode;

                        if (_NextActionCode == "B")
                        {
                            dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sNextActionDescription"] = "BILL";
                        }
                        else if (_NextActionCode == "R")
                        {
                            dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sNextActionDescription"] = "REBILL";
                        }
                        else if (_NextActionCode == "P")
                        {
                            dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sNextActionDescription"] = "PENDING, NO BILL";
                        }
                        else if (_NextActionCode == "N")
                        {
                            dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sNextActionDescription"] = "NONE";
                        }
                        else
                        {
                            dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sNextActionDescription"] = "Transacted";
                        }


                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["dNextActionAmount"] = oLineTransactions[i].Total;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nClinicID"] = oLineTransactions[i].ClinicID;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["bIsVoid"] = false;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nNextPartyType"] = oLineTransactions[i].InsuranceSelfMode.GetHashCode();
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text);
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nUserID"] = this.UserID;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["dtDate"] = DBNull.Value;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sUserName"] = this.UserName;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["sSubClaimNo"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nSubClaimNo"];
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nTrackMstTrnID"] = _TransactionID;
                        dsChargesTVP.Tables["BL_EOB_NextAction"].Rows[i]["nTrackMstTrnDetailID"] = dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].Rows[i]["nTransactionDetailID"];

                        #endregion

                    }
                    if (_dtLinesUniqueIds != null)
                    {
                        _dtLinesUniqueIds.Clear();
                        _dtLinesUniqueIds.Dispose();
                        _dtLinesUniqueIds = null;
                    }
                    if (_dtRevenueCode != null)
                    {
                        _dtRevenueCode.Clear();
                        _dtRevenueCode.Dispose();
                        _dtRevenueCode = null;
                    }
                    dsChargesTVP.Tables["BL_Transaction_Lines_Notes"].AcceptChanges();
                    dsChargesTVP.Tables["BL_EOB_NextAction"].AcceptChanges();
                    dsChargesTVP.Tables["BL_Transaction_Claim_Lines"].AcceptChanges();
                }
                if (oLineTransactions != null)
                {
                    oLineTransactions.Clear();
                    oLineTransactions.Dispose();
                    oLineTransactions = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if(oLineTransactions != null)
                {
                    oLineTransactions.Dispose();
                    oLineTransactions = null;
                }
                if (_dtLinesUniqueIds != null)
                {
                    _dtLinesUniqueIds.Dispose();
                    _dtLinesUniqueIds = null;
                }
                _sCPTCode = null;
                if (_dtRevenueCode != null)
                {
                    _dtRevenueCode.Dispose();
                    _dtRevenueCode = null;
                }
            }
        }

        public void SetDXClaimDetails_TVP(dsChargesTVP dsChargesTVP)
        {

            string _dxCode = "";
            string _dxDesc = "";
            bool _isPrimary = false;
            bool _isSelected = false;

            try
            {

                //**Delete the existing entry for the transaction
                //_sqlQuery = "delete from BL_Transaction_Diagnosis where nTransactionID = " + TransactionId + "";
                int rowNo = 0;
                for (int i = 1; i < c1Dx.Rows.Count; i++)
                {

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

                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nTransactionID"] = _MasterTransactionID;
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nVisitID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nVisitID"];
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nClaimNo"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nClaimNo"];
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nPatientID"] = dsChargesTVP.Tables["BL_Transaction_Claim_MST"].Rows[0]["nPatientID"];
                    dsChargesTVP.Tables["BL_Transaction_Diagnosis"].Rows[rowNo]["nSerialNo"] = 0; //Has to generate automatically - Primary Key
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                //..Do not dispose _oBox19Notes objects
            }

        }

        private DataTable GetUniqueIDsForLines(int claimLinesCount)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtLineIds = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@IDCount", claimLinesCount, ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetUniqueIDs", oParameters, out _dtLineIds);
                oDB.Disconnect();

                if (_dtLineIds != null && _dtLineIds.Rows.Count > 0)
                {
                    _dtLineIds.Columns.Add("ChargeLineNo");
                    _dtLineIds.AcceptChanges();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtLineIds;

        }

        private DataTable GetRevenueCodeForCPT(StringBuilder sCPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtRevenueCodes = null;


            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sCPTCode", sCPTCode.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("BL_GetRevenueCodesForCPT", oParameters, out _dtRevenueCodes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtRevenueCodes;

        }

        private DataTable GetIDForNextAction()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtNextActionID = null;


            try
            {
                string _strQuery = "";
                _strQuery = " Select nID,nBillingTransactionDetailID,nTrackMstTrnDetailID from BL_EOB_NextAction WITH (NOLOCK) " +
                            " WHERE nBillingTransactionID = " + _MasterTransactionID + " and nTrackMstTrnID= " + _TransactionID + "  " +
                            " and nEOBPaymentID=0 and nEOBID=0 and nEOBPaymentDetailID=0  ";

                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out _dtNextActionID);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtNextActionID;

        }

        #endregion " TVP Save Methods "

        private void ShowHide(ShowHideType showhide)
        {

            try
            {
                switch (showhide)
                {
                    case ShowHideType.None:
                        break;
                    case ShowHideType.ModifyLoad:
                        {
                            #region " Modify Charges Load form control setup "

                            #region "1. Check calling form if form is ClaimChargeHistory show ViewHistory button "

                            switch (CallingContainer)
                            {
                                case "frmClaimChargeHistoryV2":
                                    { tsbViewHistory.Enabled = false; }
                                    break;
                                case "frmInsurancePaymentV2":
                                    { tls_btnInsurancePayment.Enabled = false; }
                                    break;
                                default:
                                    { tsbViewHistory.Enabled = true; }
                                    break;
                            }

                            #endregion "1. Check calling form if form is ClaimChargeHistory show ViewHistory button "

                            #region "2. If External calling hide SaveNClose button "

                            if (_IsOpenForExternal == true)
                            { tls_btnSaveNClose.Visible = true; }

                            #endregion "2. If External calling hide SaveNClose button "

                            #region "3. SaveToHistory Set controls "

                            if (_IsSaveToHistoryForModify == true)
                            {
                                tls_btnSaveNClose.Visible = false;
                                tls_btnCancel.Visible = true;
                            }

                            #endregion "3. SaveToHistory Set controls "

                            #region "4. Set Claim Validation controls "

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

                            #endregion "4. Set Claim Validation controls "

                            #region "5. Checking Followup Feature is On or Off "

                            GeneralSettings oSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                            string sType = oSettings.GetInstallationType(0, 1);
                            object oValue = null;
                            bool SettingsValue = false;
                            oSettings.GetSetting("FOLLOWUP_FEATURE", 0, gloPMGlobal.ClinicID, out oValue);
                            if (Convert.ToString(oValue).ToLower().Trim() == "True".ToLower() || Convert.ToString(oValue).ToLower().Trim() == "False".ToLower())
                            {
                                SettingsValue = Convert.ToBoolean(oValue);
                            }
                            else if (Convert.ToString(oValue).Trim() == "1" || Convert.ToString(oValue).Trim() == "0")
                            {
                                SettingsValue = Convert.ToBoolean(Convert.ToString(oValue).Trim() == "1" ? "TRUE" : "FALSE");
                            }
                            if (!SettingsValue)
                            {
                                tls_FollowUpActionDate.Visible = false;
                                tsbClaimGenerateTemp.Visible = false;
                            }
                            else
                            {
                                bIsFollowUpEnabled = SettingsValue;
                            }

                            oSettings.Dispose();

                            #endregion

                            lblHoldMessage.Text = "";

                            #endregion " Modify Charges Load form control setup "
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }

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
                if (oUB != null && tls_AddUBData.Visible == true && ((_TypeOfBill != oUB.sTypeofbill && oUB.sTypeofbill != null && Convert.ToString(oUB.sTypeofbill) != "" && oUB.TypeofbillDeleted == false) || (DischargeStatus != oUB.sDischargeStatus && oUB.sDischargeStatus != null && Convert.ToString(oUB.sDischargeStatus) != "" && oUB.DischargeStatusDeleted == false) || (AdmitType != oUB.sAdmissionType && oUB.sAdmissionType != null && Convert.ToString(oUB.sAdmissionType) != "" && oUB.AdmitTypeDeleted == false) || (_nOccurrenceDate != _nMinDate && _nOccurrenceDate != 0 && _nMinDate != 0) || oUB.HasOtherData == true))
                {
                    _IsUB = true;
                }

                //Reason of Condition
                //When Transaction Status is SendToClaimManager & SendToClearingHouse 
                //insurance Hold indication should not Display.
                //**
                if (_InitialTransaction.Transaction_Status != TransactionStatus.SendToClaimManager && _InitialTransaction.Transaction_Status != TransactionStatus.InsurancePaid && _InitialTransaction.Transaction_Status != TransactionStatus.SendToClearingHouse)
                {
                    for (int icount = 1; icount <= c1Insurance.Rows.Count - 1; icount++)
                    {
                        if (c1Insurance.GetCellCheck(icount, COL_SELECT_CURRENT_RESPONSIBLE_PARTY) == CheckEnum.Checked)
                        {
                            nCurrentResponsibleParty = icount;
                            break;
                        }
                    }

                    if (c1Insurance.Rows.Count > 1)
                    {
                        _nInsuranceID = Convert.ToInt64(c1Insurance.GetData(nCurrentResponsibleParty, COL_INSURANCECONTACTID));
                        _sPartyNo = Convert.ToString(c1Insurance.GetData(nCurrentResponsibleParty, COL_INSURANCERESPONSIBILITY));
                        try
                        {
                            _bInsHoldStatus = Convert.ToBoolean(c1Insurance.GetData(nCurrentResponsibleParty, COL_INSURANCEPLANONHOLD));
                        }
                        catch
                        {
                        }
                    }

                    if (_bInsHoldStatus)
                    {
                        _sInsHoldmsg = "Insurance Plan [" + Convert.ToString(c1Insurance.GetData(nCurrentResponsibleParty, COL_INSURANCENAME)).Trim().Replace("&", "&&") + "] ";
                    }
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
                        if (_IsbCliamReplacement || (bIsRefProvAsSupervisor && bEnableSupervisorOption) || _sClaimRefNo != "" || (_oBox19Notes != null && _oBox19Notes.Count > 0 && _oBox19Notes[0].Box19NoteDescription.Trim() != "") || (_sBox10dNote.Trim() != "") || _IllnessDate > 0 || _LastSeenDate > 0 || _sDelayReasonCode.Trim() != "" || _sServiceAuthExceptionCode.Trim() != "" || _sMedicaidResubmissionCode.Trim() != "" || _UnableToWorkFromDate_MoreClaimData > 0 || _UnableToWorkTillDate_MoreClaimData > 0 || _sPWKReportTypeCode.Trim() != "" || _sPWKReportTransmissionCode.Trim() != "" || _sPWKAttachmentControlNumber.Trim() != "" || _sMammogramCertNumber.Trim() != "" || _sIDENo.Trim() != "")
                        {

                            lblHoldMessage.Text = msg + "; " + "More Claim Data is present";
                        }
                        else
                        {
                            lblHoldMessage.Text = msg;
                        }
                    }
                    else
                    {
                        if (_IsbCliamReplacement || (bIsRefProvAsSupervisor && bEnableSupervisorOption) || _sClaimRefNo != "" || (_oBox19Notes != null && _oBox19Notes.Count > 0 && _oBox19Notes[0].Box19NoteDescription.Trim() != "") || (_sBox10dNote.Trim() != "") || _IllnessDate > 0 || _LastSeenDate > 0 || _sDelayReasonCode.Trim() != "" || _sServiceAuthExceptionCode.Trim() != "" || _sMedicaidResubmissionCode.Trim() != "" || _UnableToWorkFromDate_MoreClaimData > 0 || _UnableToWorkTillDate_MoreClaimData > 0 || _sPWKReportTypeCode.Trim() != "" || _sPWKReportTransmissionCode.Trim() != "" || _sPWKAttachmentControlNumber.Trim() != "" || _sMammogramCertNumber.Trim() != "" || _sIDENo.Trim() != "")
                        {

                            lblHoldMessage.Text = "More Claim Data is present";
                        }
                        else
                        {
                            lblHoldMessage.Text = "";
                        }
                    }
                }
                if (_InitialTransaction.IsClaimSplitted)
                {
                    if (lblHoldMessage.Text == "")
                    {
                        lblHoldMessage.Text = "Claim " + _InitialTransaction.ClaimNumber + " is Split Claim";
                    }
                    else
                    {
                        if (!lblHoldMessage.Text.Contains("Split"))
                        {
                            lblHoldMessage.Text = "Claim " + _InitialTransaction.ClaimNumber + " is Split Claim" + "; " + lblHoldMessage.Text;
                        }
                    }
                }

                if (lblHoldMessage.Text.Trim() == "")
                {
                    if (msg != null && msg != "")
                    {
                        if (_IsbCliamReplacement || _sClaimRefNo != "" || (_oBox19Notes != null && _oBox19Notes.Count > 0 && _oBox19Notes[0].Box19NoteDescription.Trim() != "") || (_sBox10dNote.Trim() != "") || _IllnessDate > 0 || _LastSeenDate > 0 || _sDelayReasonCode.Trim() != "" || _sServiceAuthExceptionCode.Trim() != "" || _sMedicaidResubmissionCode.Trim() != "" || _UnableToWorkFromDate_MoreClaimData > 0 || _UnableToWorkTillDate_MoreClaimData > 0 || _sPWKReportTypeCode.Trim() != "" || _sPWKReportTransmissionCode.Trim() != "" || _sPWKAttachmentControlNumber.Trim() != "" || _sMammogramCertNumber.Trim() != "" || _sIDENo.Trim() != "")
                        {
                            if (oUB != null && _IsUB == true)
                            {
                                lblHoldMessage.Text = "More Claim Data and UB Data are present";
                            }
                            else
                            {
                                lblHoldMessage.Text = msg + "; " + "More Claim Data is present";
                            }
                        }
                        else
                        {
                            if (oUB != null && _IsUB == true)
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
                        if (_IsbCliamReplacement || _sClaimRefNo != "" || (_oBox19Notes != null && _oBox19Notes.Count > 0 && _oBox19Notes[0].Box19NoteDescription.Trim() != "") || (_sBox10dNote.Trim() != "") || _IllnessDate > 0 || _LastSeenDate > 0 || _sDelayReasonCode.Trim() != "" || _sServiceAuthExceptionCode.Trim() != "" || _sMedicaidResubmissionCode.Trim() != "" || _UnableToWorkFromDate_MoreClaimData > 0 || _UnableToWorkTillDate_MoreClaimData > 0 || _sPWKReportTypeCode.Trim() != "" || _sPWKReportTransmissionCode.Trim() != "" || _sPWKAttachmentControlNumber.Trim() != "" || _sMammogramCertNumber.Trim() != "" || _sIDENo.Trim() != "")
                        {
                            if (oUB != null && _IsUB == true)
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
                            if (oUB != null && _IsUB == true)
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
                else if (oUB != null && _IsUB == true)
                {
                    lblHoldMessage.Text = lblHoldMessage.Text + "; " + "UB Data is present";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            
        }
        private void setActionTemplateVisibility()
        {
            if (c1Insurance.Rows.Count > 1)
            {
                for (int icount = 1; icount <= c1Insurance.Rows.Count - 1; icount++)
                {
                    if (c1Insurance.GetCellCheck(icount, COL_SELECT_CURRENT_RESPONSIBLE_PARTY) == CheckEnum.Checked)
                    {
                        nCurrentResponsibleParty = icount;
                        break;
                    }
                }
                Int64 _nRespParty = 0;
                _nRespParty = Convert.ToInt64(c1Insurance.GetData(nCurrentResponsibleParty, COL_INSURANCECONTACTID));
                if (_nRespParty == 0)
                {
                    tls_FollowUpActionDate.Visible = false;
                    tsbClaimGenerateTemp.Visible = false;
                }
                else
                {
                    tls_FollowUpActionDate.Visible = true;
                    tsbClaimGenerateTemp.Visible = true;
                }
            }
           
        }
        private void SetLastGlobalPeriods()
        {
            DataSet dsClaimFollowUp = null;
            DataTable dtClaimNote = null;
            DataTable dtClaimFollowUp = null;
            DataTable _dtLatestGlobalPeriod = gloCharges.GetLastPatientGlobalPeriod(_TransPatientID, false);
            try
            {
                if (_dtLatestGlobalPeriod != null && _dtLatestGlobalPeriod.Rows.Count > 0)
                {
                    string sMessage = Convert.ToString(_dtLatestGlobalPeriod.Rows[0]["sDuration"]) + "   ";
                    lblGlobalPeriodAlert.Tag = _dtLatestGlobalPeriod.Rows[0]["nGlobalPeriodID"];
                    lblGlobalPeriodAlert.Text = (sMessage != string.Empty ? "Last Global Period : " + sMessage : "");
                }
                else
                {
                    lblGlobalPeriodAlert.Tag = 0;
                    lblGlobalPeriodAlert.Text = string.Empty;
                }
                if (CL_FollowUpCode.IsFollowUpFeatureON())
                {
                    //Hide Action & Template Buttons if responsibility transferred on self START 
                    setActionTemplateVisibility();
                    //Hide Action & Template Buttons if responsibility transferred on self END
                    dsClaimFollowUp = CL_FollowUpCode.GetClaimFollowUp(_MasterTransactionID, _TransactionID);
                    if (dsClaimFollowUp != null && dsClaimFollowUp.Tables.Count > 0)
                    {
                        dtClaimNote = dsClaimFollowUp.Tables[0];
                        dtClaimFollowUp = dsClaimFollowUp.Tables[1];
                    }
                    if (dtClaimNote != null && dtClaimNote.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtClaimNote.Rows[0]["Note"]) != "")
                        {
                            pnlClaimNote.Visible = true;
                            lblClaimNote.Text = dtClaimNote.Rows[0]["Note"].ToString();
                            ToolTip1.SetToolTip(lblClaimNote, SplitToolTip(dtClaimNote.Rows[0]["Note"].ToString()));
                            pnlClaimNote.BringToFront();
                        }
                        else
                        {
                            pnlClaimNote.Visible = false;
                            lblClaimNote.Text = "";
                            ToolTip1.SetToolTip(lblClaimNote, "");
                        }
                    }
                    else
                    {
                        pnlClaimNote.Visible = false;
                        lblClaimNote.Text = "";
                        ToolTip1.SetToolTip(lblClaimNote, "");
                    }

                    if (dtClaimFollowUp != null && dtClaimFollowUp.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtClaimFollowUp.Rows[0]["sFollowupDescription"]) != "")
                        {
                            pnlClaimFollowUp.Visible = true;
                            lblClaimFollowup.Text = dtClaimFollowUp.Rows[0]["sFollowupDescription"].ToString();
                            ToolTip1.SetToolTip(lblClaimFollowup, SplitToolTip(dtClaimFollowUp.Rows[0]["sFollowupDescription"].ToString()));
                            if (Convert.ToDateTime(dtClaimFollowUp.Rows[0]["dtClaimFollowUpDate"].ToString()) <= DateTime.Now)
                            {
                                lblClaimFollowup.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblClaimFollowup.Font, FontStyle.Bold);
                                lblClaimFollowup.ForeColor = System.Drawing.Color.Maroon;
                            }
                            else
                            {
                                lblClaimFollowup.ForeColor = System.Drawing.Color.Black;
                            }
                        }
                        else
                        {
                            pnlClaimFollowUp.Visible = false;
                            lblClaimFollowup.Text = "";
                            ToolTip1.SetToolTip(lblClaimFollowup, "");
                        }
                    }
                    else
                    {
                        pnlClaimFollowUp.Visible = false;
                        lblClaimFollowup.Text = "";
                        ToolTip1.SetToolTip(lblClaimFollowup, "");
                    }
                }
                else
                {
                    tls_FollowUpActionDate.Visible = false;
                    tsbClaimGenerateTemp.Visible = false;
                }

                if (lblGlobalPeriodAlert.Text != string.Empty || lblClaimNote.Text != string.Empty || lblClaimFollowup.Text != string.Empty)
                {
                    if (lblGlobalPeriodAlert.Text != string.Empty) { btnModifyGlobalPeriod.Visible = true; } else { btnModifyGlobalPeriod.Visible = false; }
                    pnlAlerts.Visible = true;
                }
                else
                {
                    pnlAlerts.Visible = false;
                }

                if (nReplacementByTransMasterID != 0 || nReplacingTransMasterID != 0)
                {
                    pnlAlerts.Visible = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (dsClaimFollowUp != null)
                {
                    dsClaimFollowUp.Dispose();
                    dsClaimFollowUp = null;
                }

                if (dtClaimNote != null)
                {
                    dtClaimNote.Dispose();
                    dtClaimNote = null;
                }

                if (dtClaimFollowUp != null)
                {
                    dtClaimFollowUp = null;
                    dtClaimFollowUp = null;
                }

                if (_dtLatestGlobalPeriod != null)
                {
                    _dtLatestGlobalPeriod.Dispose();
                    _dtLatestGlobalPeriod = null;
                }
            }
        }

        private string SplitToolTip(string strOrig)
        {
            try
            {
                string[] strArray;
                string CR = "\r\n";
                string strBuilder = "";
                string strReturn = "";
                strArray = strOrig.Split(' ');
                foreach (string strOneWord in strArray)
                {
                    strBuilder = (strBuilder + (strOneWord + ' '));
                    if (strBuilder.Length > 70)
                    {
                        strReturn = (strReturn + (strBuilder + CR));
                        strBuilder = "";
                    }
                }
                if (strBuilder.Length < 8)
                {
                    strReturn = strReturn.Substring(0, (strReturn.Length - 2));
                }
                return (strReturn + strBuilder);
            }
            catch //(Exception e)
            {
                return strOrig;
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
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true); }
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
                        ReloadPatientRefferals(PatientID);
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

                    if (_dt.Rows[0]["nFacilityID"] != DBNull.Value && Convert.ToInt64(_dt.Rows[0]["nFacilityID"]) != 0)
                    {
                        if (cmbFacility.Enabled)
                        {
                            cmbFacility.SelectedIndex = -1;
                            cmbFacility.SelectedValue = _dt.Rows[0]["nFacilityID"];
                        }

                    }
                    // Commented on 02212014
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

                                //UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);

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

                                //UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);

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
                        mskUnableFromDate.Text = ""; _UnableToWorkFromDate_MoreClaimData = 0;
                    }

                    if (_dt.Rows[0]["dtunbaleWorkto"] != DBNull.Value)
                    {
                        //mskUnableTillDate.Text = Convert.ToDateTime(_dt.Rows[0]["dtunbaleWorkto"]).ToString("MM/dd/yyyy");
                        _UnableToWorkTillDate_MoreClaimData = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(_dt.Rows[0]["dtunbaleWorkto"]).ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        mskUnableTillDate.Text = ""; _UnableToWorkTillDate_MoreClaimData = 0;
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
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true); }
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
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true); }
        }

        private void SetCaseInsurance(DataTable _dt)
        {
            bool bInsPlanExist = true;
            int iCount = 0;
            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    do
                    {
                        int iRow = c1Insurance.FindRow(Convert.ToString(_dt.Rows[iCount]["nInsuranceId"]), 1, COL_INSURANCEID, false);
                        if (iRow < 0)
                        {
                            bInsPlanExist = false;
                            break;
                        }

                        iCount++;
                    } while (iCount <= _dt.Rows.Count - 1);
                    //}

                    if (bInsPlanExist)
                    {
                        for (iCount = 1; iCount <= c1Insurance.Rows.Count - 1; iCount++)
                        {
                            // c1Insurance.SetData(iCount, COL_INSURANCEPARTY, "");
                            c1Insurance.SetData(iCount, COL_INSURANCERESPONSIBILITY, "");
                        }

                        for (iCount = 0; iCount <= _dt.Rows.Count - 1; iCount++)
                        {
                            int iRow = c1Insurance.FindRow(Convert.ToString(_dt.Rows[iCount]["nInsuranceId"]), 1, COL_INSURANCEID, false);

                            if (iRow > 0)
                            {
                                c1Insurance.SetData(iRow, COL_INSURANCEPARTY, Convert.ToString(_dt.Rows[iCount]["nResponsibilityNumber"]));
                                c1Insurance.SetData(iRow, COL_INSURANCERESPONSIBILITY, Convert.ToString(_dt.Rows[iCount]["nResponsibilityNumber"]));
                            }
                        }

                        ReorderInsurance();

                        //To Remove the Previous Flag
                        for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                            c1Insurance.SetCellCheck(i, COL_SELECT_CURRENT_RESPONSIBLE_PARTY, CheckEnum.Unchecked);
                        }

                        _dt = gloCharges.GetCurrentPartyNumber(_TransactionID, _MasterTransactionID);
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            Int16 _party = 1;
                            System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                            c1Insurance.SetCellImage(_party, COL_INSURANCERESPONSIBILITY, imgFlag);
                            c1Insurance.SetCellCheck(_party, COL_SELECT_CURRENT_RESPONSIBLE_PARTY, CheckEnum.Checked);
                        }
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
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true); }
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
        }

        private void CheckForPaymentNotes(TransactionLines _oTransactionLines)
        {
            try
            {
             //   Boolean _IsPaymentNotesModified = false;
               

                if (_oTransactionLines != null)
                {

                    if (_oTransactionLines.Count > 0)
                    {
                        Int64 _LineNo = 0;
                        DataTable dtPayNotes = null;
                        dtPayNotes = gloCharges.GetPaymentNotes(_MasterTransactionID);
                        GeneralNotes oPaymentNotes = null;
                        for (int i = 0; i < _oTransactionLines.Count; i++)
                        {
                            //if (i == _oTransactionLines.Count - 1)
                            //{
                           
                            //}
                            //else
                            //{
                            //    _sTransactionMstDtlID.Append(_oTransactionLines[i].TransactionMasterDetailID.ToString() + ",");
                            //}
                            DataRow[] dr = null;
                            dr = dtPayNotes.Select(" nTransactionDetailID = " + _oTransactionLines[i].TransactionMasterDetailID + "");
                            _LineNo++;


                            if (dr != null)
                            {
                                if (dr.Length > 0)
                                {
                                    oPaymentNotes = new GeneralNotes();
                                    GeneralNote oPaymentNote = new GeneralNote();

                                    for (int j = 0; j < dr.Length; j++)
                                    {
                                        oPaymentNote = new global::gloBilling.Common.GeneralNote();
                                        oPaymentNote.TransactionID = Convert.ToInt64(dr[j]["nTransactionID"]);
                                        oPaymentNote.TransactionLineId = Convert.ToInt64(dr[j]["nLineNo"]);
                                        oPaymentNote.TransactionDetailID = Convert.ToInt64(dr[j]["nTransactionDetailID"]);
                                        oPaymentNote.NoteType = NoteType.GeneralNote;
                                        oPaymentNote.NoteID = Convert.ToInt64(dr[j]["nNoteId"]);
                                        oPaymentNote.UserID = Convert.ToInt64(dr[j]["nUserID"]);
                                        object ob = Convert.ToDateTime(dr[j]["nNoteDateTime"]).Second;

                                        //Commented on 03/14/2012 For the Account Log issue - The nNoteDateTime was getting updated every time instead of the nCloseDate Field 
                                        //if (Convert.ToString(dtPayNotes.Rows[j]["sNoteCode"]) != "1")
                                        //{
                                        //    _IsPaymentNotesModified = false;
                                        //    oPaymentNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dtPayNotes.Rows[j]["nCloseDate"]).ToString());
                                        //    oPaymentNote.StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dtPayNotes.Rows[j]["nCloseDate"]).ToString());
                                        //}
                                        //else
                                        //{
                                        //    _IsPaymentNotesModified = true;
                                        //    oPaymentNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dtPayNotes.Rows[j]["nNoteDateTime"]).ToString());
                                        //    oPaymentNote.StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dtPayNotes.Rows[j]["nNoteDateTime"]).ToString());
                                        //}


                                        oPaymentNote.NoteDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dr[j]["nCloseDate"]).ToString());
                                        oPaymentNote.StatementNoteDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(dr[j]["nCloseDate"]).ToString());

                                        oPaymentNote.NoteDescription = Convert.ToString(dr[j]["sNoteDescription"]);
                                        oPaymentNote.ClinicID = _ClinicID;
                                        if (Convert.ToString(dr[j]["nNoteType"].ToString()) == "5")
                                        {
                                            oPaymentNote.BillingNoteType = EOBPaymentSubType.StatementNote;
                                        }
                                        else if (Convert.ToString(dr[j]["nNoteType"].ToString()) == "6")
                                        {
                                            oPaymentNote.BillingNoteType = EOBPaymentSubType.InternalNote;
                                        }
                                        else
                                        {
                                            oPaymentNote.BillingNoteType = (EOBPaymentSubType)(dr[j]["nNoteType"]);
                                        }

                                        oPaymentNote.NoteRowID = j;

                                        if (oPaymentNote != null)
                                        {
                                            oPaymentNotes.Add(oPaymentNote);
                                        }

                                        if (oPaymentNotes != null)
                                        {
                                            UC_gloBillingTransactionLines.SetNotes(Convert.ToInt16(_LineNo), oPaymentNote);
                                            UC_gloBillingTransactionLines.SetEPSDTNotesNDCCodeFlag(Convert.ToInt16(_LineNo));
                                        }
                                    }
                                }

                            }
                            dr = null;
                            

                        }

                        //}//For
                    }

                }
            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }


        }

        public bool PerformCaseAdd()
        {
            bool _Return = false;
            DialogResult oResult = System.Windows.Forms.DialogResult.Yes;
            if (_TransPatientID > 0)
            {
                if (gloCharges.CheckPatientCases(_TransPatientID) == true)
                {
                    frmShowCases ofrmShowCases = new frmShowCases(_TransPatientID);
                    try
                    {
                        ofrmShowCases.ShowDialog(this);

                        if (ofrmShowCases.CurrentCase != 0)
                        {
                            //this.PatientHasCases = true;

                            oResult = MessageBox.Show("Case \"" + ofrmShowCases.CurrentnCaseName + "\" has been selected."
                                        + Environment.NewLine
                                        + "Replace Claim entries with Case information?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                            if (oResult == System.Windows.Forms.DialogResult.Yes)
                            {
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

                                    //Setting Case data into charge master form
                                    SetCaseDetailsintoCharge(ofrmShowCases.CurrentCaseData);

                                    //Setting Case diagonosis into charge lines
                                    SetCaseDiagonosisintoChargeLines(ofrmShowCases.CurrentDiagnosis);

                                    //Setting Case Insurance
                                    if (_dtEOBPayments != null && _dtEOBPayments.Rows.Count <= 0)
                                    {
                                        SetCaseInsurance(ofrmShowCases.CurrentCaseInsurences);
                                    }
                                }
                            }
                            else if (oResult == System.Windows.Forms.DialogResult.No)
                            {
                                txtCases.Tag = ofrmShowCases.CurrentCase;
                                txtCases.Text = ofrmShowCases.CurrentnCaseName;
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

                            gloCharges.getSingleValidCase(_dtCloseDate, PatientID, out dtCases, out dtCaseDiag, out dtCasesIns);
                            if (dtCases != null && dtCases.Rows.Count > 0)
                            {
                                this.PatientHasCases = true;
                            }
                            else
                            {
                                this.PatientHasCases = false;
                            }
                        }

                        CheckForPatientCases();
                        _Return = true;
                    }
                    catch (Exception ex)
                    {
                        _Return = false;
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                    }
                    finally
                    {
                        UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);
                        ofrmShowCases.Dispose();
                    }
                }
                else
                {
                    frmSetupCases ofrmSetupCases = new frmSetupCases(_TransPatientID);
                    try
                    {
                        ofrmSetupCases.ShowDialog(this);
                        if (ofrmSetupCases.nCaseID != 0)
                        {
                            //this.PatientHasCases = true;

                            oResult = MessageBox.Show("Case \"" + ofrmSetupCases.sCaseName.ToString().Trim() + "\" has been selected."
                                        + Environment.NewLine
                                        + "Replace Claim entries with Case information?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                            if (oResult == System.Windows.Forms.DialogResult.Yes)
                            {

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

                                    //Setting Case data into charge master form
                                    SetCaseDetailsintoCharge(ofrmSetupCases.CaseData);

                                    //Setting Case diagonosis into charge lines
                                    SetCaseDiagonosisintoChargeLines(ofrmSetupCases.Diagnosis);

                                    //Setting Case Insurance
                                    if (_dtEOBPayments != null && _dtEOBPayments.Rows.Count <= 0)
                                    {
                                        SetCaseInsurance(ofrmSetupCases.CurrentCaseInsurences);
                                    }
                                }

                            }
                            else if (oResult == System.Windows.Forms.DialogResult.No)
                            {
                                txtCases.Tag = ofrmSetupCases.nCaseID;
                                txtCases.Text = ofrmSetupCases.sCaseName.ToString().Trim();
                            }
                            CheckForPatientCases();
                        }
                        _Return = true;
                    }
                    catch (Exception ex)
                    {
                        _Return = false;
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
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
                if (txtCases.Tag != null && txtCases.Tag.ToString() != "")
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Remove, "Cases removed from the Transaction", _TransPatientID, Convert.ToInt64(txtCases.Tag), 0, ActivityOutCome.Success);
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

                gloCharges.getSingleValidCase(_dtCloseDate, PatientID, out dtCases, out dtCaseDiag, out dtCasesIns);
                if (dtCases != null && dtCases.Rows.Count > 0)
                {
                    this.PatientHasCases = true;
                }
                else
                {
                    this.PatientHasCases = false;
                }

                CheckForPatientCases();
                _Return = true;
            }
            catch (Exception ex)
            {
                _Return = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            return _Return;

        }

        private void GetReplacementClaimIndication()
        {
            DataSet _dtReplacementClaimDtls = gloCharges.RetreiveReplacementClaim(_MasterTransactionID);
            try
            {
                if (_dtReplacementClaimDtls != null && _dtReplacementClaimDtls.Tables.Count > 0)
                {
                    pnlReplacedByClaim.Visible = false;
                    pnlReplacesClaim.Visible = false;
                    lblReplacedByClaim.Text = "";
                    lblReplacesClaim.Text = "";
                    lblReplacesClaim.Tag = "";
                    lblReplacedByClaim.Tag = "";


                    if (_dtReplacementClaimDtls.Tables[0] != null && _dtReplacementClaimDtls.Tables[0].Rows.Count > 0)
                    {

                        if (Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacedByTransMasterID"]) > 0 && Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacingTransMasterID"]) > 0)
                        {
                            if ((_dtReplacementClaimDtls.Tables[1] != null && _dtReplacementClaimDtls.Tables[1].Rows.Count > 0))
                            {
                                pnlAlerts.Visible = true;
                                lblReplacedByClaim.Text = "Replaced By Claim : " + Convert.ToString(_dtReplacementClaimDtls.Tables[1].Rows[0]["nReplacedByClaimNo"]);
                                pnlReplacedByClaim.Visible = true;
                                lblReplacedByClaim.Tag = Convert.ToString(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacedByTransMasterID"]);
                                nReplacementByTransMasterID = Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacedByTransMasterID"]);
                            }

                            if (_dtReplacementClaimDtls.Tables[2] != null && _dtReplacementClaimDtls.Tables[2].Rows.Count > 0)
                            {
                                pnlAlerts.Visible = true;
                                lblReplacesClaim.Text = "Replaces Claim : " + Convert.ToString(_dtReplacementClaimDtls.Tables[2].Rows[0]["nReplacingClaimNo"]);
                                pnlReplacesClaim.Visible = true;
                                lblReplacesClaim.Tag = Convert.ToString(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacingTransMasterID"]);
                                nReplacingTransMasterID = Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacingTransMasterID"]);
                            }

                        }
                        else if (Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacedByTransMasterID"]) > 0)
                        {
                            if (_dtReplacementClaimDtls.Tables[0] != null && _dtReplacementClaimDtls.Tables[1].Rows.Count > 0)
                            {
                                pnlAlerts.Visible = true;
                                lblReplacedByClaim.Text = "Replaced By Claim : " + Convert.ToString(_dtReplacementClaimDtls.Tables[1].Rows[0]["nReplacedByClaimNo"]);
                                pnlReplacedByClaim.Visible = true;
                                lblReplacedByClaim.Tag = Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacedByTransMasterID"]);
                                nReplacementByTransMasterID = Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacedByTransMasterID"]);
                            }

                        }
                        else if (Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacingTransMasterID"]) > 0)
                        {
                            if (_dtReplacementClaimDtls.Tables[0] != null && _dtReplacementClaimDtls.Tables[2].Rows.Count > 0)
                            {
                                pnlAlerts.Visible = true;
                                lblReplacesClaim.Text = "Replaces Claim :  " + Convert.ToString(_dtReplacementClaimDtls.Tables[2].Rows[0]["nReplacingClaimNo"]);
                                pnlReplacesClaim.Visible = true;
                                lblReplacesClaim.Tag = Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacingTransMasterID"]);
                                nReplacingTransMasterID = Convert.ToInt64(_dtReplacementClaimDtls.Tables[0].Rows[0]["nReplacingTransMasterID"]);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (_dtReplacementClaimDtls != null)
                {
                    _dtReplacementClaimDtls.Dispose();
                    _dtReplacementClaimDtls = null;
                }
            }
        }

        #region "Patient Appointments Linking"
        void UC_gloBillingTransactionLines_date_Changed(object sender, RowColEventArgs e)
        {            
            try
            {
                using (DataTable dtAppointments = this.GetPatientAppointments())
                {
                    if (dtAppointments != null && dtAppointments.Rows.Count > 0)
                    { this.CheckMorePatientAppointments(dtAppointments); }

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
                }                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void CheckMorePatientAppointments(DataTable dtAppointments)
        {
            List<PatientAppointment> lstPatientAppointments = null;

            try
            {
                if (dtAppointments != null && dtAppointments.Rows.Count > 0)
                {
                    lstPatientAppointments = dtAppointments.AsEnumerable().Select(p => new PatientAppointment() { AppointmentID = Convert.ToInt64(p["ID"]) }).ToList();

                    if (lstPatientAppointments.Except(this.ListOfPatientAppointments).Any())
                    { this.MorePatientAppointmentsAvailable = true; }
                    else
                    { this.MorePatientAppointmentsAvailable = false; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (lstPatientAppointments != null)
                {
                    lstPatientAppointments.Clear();
                    lstPatientAppointments = null;
                }
            }
        }

        private void FillPatientAppointmentsOnLoad()
        {
            DataTable dtPatientAppointments = null;
            DataTable dtApp = null;

            try
            {
                if (this.lstAppointmentIDs == null) { this.lstAppointmentIDs = new List<long>(); }
                if (this.ListOfPatientAppointments == null) { this.ListOfPatientAppointments = new List<PatientAppointment>(); }

                dtPatientAppointments = gloCharges.GetLinkedPatientAppointments(this.PatientID, this._MasterTransactionID);
                dtApp = this.GetPatientAppointments();

                if (dtPatientAppointments.Rows.Count > 0)
                {
                    this.PatientHasAppointments = true;
                    this.SetPatientAppointmentsLinked();

                    foreach (DataRow elementRow in dtPatientAppointments.Rows)
                    {
                        long nAppointmentID = Convert.ToInt64(elementRow["nAppointmentID"]);
                        this.lstAppointmentIDs.Add(nAppointmentID);
                    }                    
                }
                else
                {                    
                    this.CheckForPriorAppointments(dtApp);                    
                }

                this.LoadAppointmentsInList(dtApp);               
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
                if (dtApp != null)
                {
                    dtApp.Dispose();
                    dtApp = null;
                }
            }
        }

        private void LoadAppointmentsInList(DataTable Appointments)
        {
            try
            {
                this.ListOfPatientAppointments.Clear();
                
                foreach (Int64 element in this.lstAppointmentIDs)
                { this.ListOfPatientAppointments.Add(new PatientAppointment() { AppointmentID = element }); }

                foreach (DataRow drow in Appointments.AsEnumerable().Where(p => !this.ListOfPatientAppointments.Any(q => q.AppointmentID == Convert.ToInt64(p["ID"]))))
                { this.ListOfPatientAppointments.Add(new PatientAppointment() { AppointmentID = Convert.ToInt64(drow["ID"]) }); }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion        
    }

        #endregion
    
}
