using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAuditTrail;
using gloBilling.Common;
using gloPMGeneral;
using gloSettings;
using System.Collections.Generic;
using gloUIControlLibrary;
using ChargeRules;
using System.Linq;
using System.ServiceModel;


namespace gloBilling
{
    public partial class frmBillingTransaction : gloAUSLibrary.MasterForm 
    {

        [DllImport("user32.dll")]
        static extern bool LockWindowUpdate(IntPtr hWnd);
        #region " Variable Declarations
                
        gloBillingTransaction UC_gloBillingTransactionLines;
        gloListControl.gloListControl oListControl;
        private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private static frmBillingTransaction frm;
        private static frmBillingTransaction frm_Copy;
     //   private gloAddress.gloZipcontrol oZipcontrol;
        ClaimHold _oClaimHold = null;
        ClaimBox19Note _oBox19Note = null;
        ClaimBox19Notes _oBox19Notes = new ClaimBox19Notes();

        private string _sClaimBox10dNote = null;
                
        System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
        private ToolTip oToolTip = new ToolTip();
        private ComboBox combo;
        
       // private string _TempZipText;
        bool _IsICD9Driven = false;

        private Boolean _bDxFlag = false;
        private Boolean _bSmartTreatmentLoading = false;
        private Boolean _bEMRTreatmentLoading = false;
        private Boolean _bOnlineClaimPostingLoading = false;
        gloSettings.ExternalChargesType _nEMRTreatmentType = ExternalChargesType.gloEMRTreatment;
        
        bool _IsCancelEMRTreatment = false;

        private Int64 TransactionClaimID = 0;
        public Int64 _TransactionID = 0;
        private Int64 _PatientPoviderID = 0;
        private Int64 _EMRExamID = 0;
        private Int64 _EMRVisitID = 0;
        private Int64 _EMRPatientId = 0;
        private Int64 _EMRProviderId = 0;
        
        private Int64 _RenderingProviderID = 0;
        private Int64 _DefaultTOSID = 0;
        private Int64 _DefaultPOSID = 0;
        private Int64 _FeeScheduleID = 0;
        private Int64 _DefaultFeeScheduleID = 0;
        
        private Int32 _NoOfMaxServiceLines = 30;
        private Int32 _NoOfMaxDiagnosis;

        private bool _IsSaveNextTreatmentClick = false;
        //Returns True if all the Dates are Valid
        
        private bool _IsValidDate = true;
        

        private bool _OpenViewMode = false;
        private bool _IsOpenForExternal = false;
       
        
        
        private bool _IsEMRTreatmentStart = false;
        private bool _IsEMRTreatmentStop = false;
        private bool _IsFormLoading = false;

        
       
        private bool _isDxListLoading = false;
        private bool blnDisposed;
        private bool _ShowSelectTray = false;
        
        public bool isFormLoading = false;
     //   private bool _isZipItemSelected = false;
        public bool _isTextBoxLoading = false;
        public bool _IsModifyed = false;
        private String _sClaimRefNo = "";

        private Boolean _IsbCliamReplacement = false;
        public Int64 nReplacementByTransMasterID = 0;
        public Int64 nReplacingTransMasterID = 0;
        public string sReplaingClaimNo = "";
        public ReplacementClaimCreationType ReplacementClaimCreationType = ReplacementClaimCreationType.None;
        public Int64 _MasterTransactionID = 0;
        public string sClaimNo = "";
     //   private string _WorkerCompType = "";
        private string _WorkerCompNo = "";
        private string _AutoClaimNo = "";
        private string _OtherAccidentNo = "";
        private string _WorkerInjuryDate = "";
        private string _AccidentDate = "";
        private string _OtherDate = "";
        public Boolean bIsCopiedClaim = false;
        public Int64 _InitialReferalProviderId = 0;
        private bool _IsOpenForResend = false;
        private bool IsAppointmentPresent=false;
        private InsuranceTypeFlag _IsResendToInsType = InsuranceTypeFlag.None;
  //      private Int32 InsParty = 0;
  //      private int nCurrentResponsibleParty = 0;
        private string _sBox10dNote = string.Empty;
        ArrayList _arrDxCodes = null;
        ArrayList _arrValidDxCodes = null;

        private Int64 _EMRFeeScheduleID = 0;
        private Boolean _bDXSwitch = false;
        //private bool _IsFormLoading = false;

        ArrayList _SelectedSmartTreatmentIDs = null;

        private DateTime _ExternalDateTime = DateTime.Now;
        

        private Int64 _IllnessDate;
        private Int64 _LastSeenDate;

        private Int64 _UnableToWorkFromDate_MoreClaimData;
        private Int64 _UnableToWorkTillDate_MoreClaimData;
        

        private Int64 _nResponsibleParty = 0;
        private string _sResponsibleNo = "";
        private string _sLastServiceLineDOS = "";

        private Boolean _bIsRefprovAsSupervisor = false;

        private string _TypeOfBill = "";
        private Int64 _SelectedTrayID = 0;
        private string _SelectedTrayName = string.Empty;

        ArrayList ArraylistSelectedCPTS = null;

        TransactionLines oEMRTransLinesSplit = null;
        TransactionLine oEMRTransLineSplit = null;

        StringBuilder sbEMRTreatmentLineNos = new StringBuilder();

        private Boolean bIsFullyPosted = false;

        DataTable _dtNoPostCharges = new DataTable();

        DataTable _dtLoadedCPTS = new DataTable();

        C1.Win.C1FlexGrid.CellStyle csNoPost = null;
        C1.Win.C1FlexGrid.CellStyle csPosted =null;
        C1.Win.C1FlexGrid.CellStyle csNoPostNormal = null;


        private Boolean _bIsSplitEMRTreatmentLoaded = false;

        private Boolean bIsFacilityChangeFromModifyPatient = false;

        public Boolean bIsChargeSaved = false;
        List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> triggeredRuleInfo = null;
        List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> triggeredRuleInfoGlobal = null;
        private List<long> lstAppointmentIDs = null;

        public Tuple<Int64, string, string> SyncPatientId
        {
            get { return new Tuple<Int64, string, string>(_PatientID, oPatientControl.PatientCode, oPatientControl.PatientName); }
        }

        private string _PWKReportTypeCode = string.Empty;
        private string _PWKReportTransmissionCode = string.Empty;
        private string _PWKAttachmentControlNumber = string.Empty;
        private string _MammogramCertNumber = string.Empty;
        private string _IDENo = string.Empty;

        private Int64 _PortalClaimID = 0;
        private Int64 _OnlinePatientID = 0;
        private Int64 _OnlineProviderID = 0;
        private Int64 _OnlineFacilityID = 0;
        private String _OnlineChargetype = "";
        private String _OnlineHospFromDate = "";
        private String _OnlineHospToDate = "";
        private string _OnlineClaimNote = "";

        public bool IsEMRTreatmentBind = false;
        public bool IsOnlineChargeBind = false;
        public bool IsMissingChargeLoad = false;        

        #endregion

        #region " Property Procedures "

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public Int64 PatientPoviderID
        {
            get { return _PatientPoviderID; }
            set { _PatientPoviderID = value; }
        }
        public string PatientProviderName
        {
            get { return _PatientProviderName; }
            set { _PatientProviderName = value; }
        }

        public bool OpenChargesForExternal
        {
            get { return _IsOpenForExternal; }
            set { _IsOpenForExternal = value; }
        }
        public DateTime OpenChargesExternalDateTime
        {
            get { return _ExternalDateTime; }
            set { _ExternalDateTime = value; }
        }
       

        public Int64 EMRPatientID
        {
            get { return _EMRPatientId; }
            set { _EMRPatientId = value; }
        }
        public Int64 EMRProviderID
        {
            get { return _EMRProviderId; }
            set { _EMRProviderId = value; }
        }

        public bool OpenViewMode
        {
            get { return _OpenViewMode; }
            set { _OpenViewMode = value; }
        }

        public Int64 RenderingProviderID
        {
            get { return _RenderingProviderID; }
            set { _RenderingProviderID = value; }
        }

        public EPSDTFamilyPlanningClaim ClaimEPSDT { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle |= 0x02000000;
                //cp.ExStyle |= 0x00000020;//WS_EX_TRANSPARENT
                return cp;
            }
        }

        public bool bEnableSupervisorOption { get; set; }

        public bool bShowInitialTreatmentDate { get; set; }

        public string DefaultProviderQualifierCode { get; set; }

        public string DefaultDateQualifierCode { get; set; }

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
        public bool EPSDTEnabled { get; set; }

        public bool bIsAnesthesiaEnabled { get; set; }


        private Boolean bisEMRTreatmentSplitEnabled = false;

        private Boolean bIsRefProvAsSupervisor { get; set; }

        private Int64 SelectedChargeTrayID
        {
            get { return _SelectedTrayID; }
            set
            {
                _SelectedTrayID = value;
                lblCloseDayTray.Tag = _SelectedTrayID;
            }
        }

        private string SelectedChargeTray
        {
            get { return _SelectedTrayName; }
            set
            {
                _SelectedTrayName = value;
                lblCloseDayTray.Text = _SelectedTrayName;
            }
        }

        public Boolean bIsOCPPortalEnable { get; set; }
        public Boolean bIsDublicteCPTsWarningEnable { get; set; }
        private List<PatientAppointment> ListOfPatientAppointments = new List<PatientAppointment>();
        private Boolean MorePatientAppointmentsAvailable = false;
        #endregion

        #region " Constructor "

        public frmBillingTransaction(Int64 PatientID)
        {
            InitializeComponent();
            ReadApplicationSettings();

            _PatientID = PatientID;
            _EMRExamID = 0;
            _EMRVisitID = 0;

            if (oUB != null)
            {
                oUB.sTypeofbill = "";
                oUB.sAdmissionType = "";
                oUB.sAdmitDate = "";
                oUB.sAdmitHour = "";
                oUB.sDischargeHour = "";
                oUB.sDischargeStatus = "";

            }

            _IsPatientAccountFeature = gloGlobal.gloPMGlobal.IsAccountsOn;
            //End

            //cmbProviderType.Dock = DockStyle.Left;
            //lblReferralProvider.Controls.Add(cmbProviderType);
            //lblReferralProvider.Height = cmbProviderType.Height;
            //lblReferralProvider.BorderStyle = BorderStyle.FixedSingle;

            
        }

        #endregion

        #region " Form Load "
        private    Font boldFont =gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
        private    Font regularFont =gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);

        //added varribales uiPanSplitScreen, clsSplit_PatientCharges by manoj jadhav on 20150202 to show Spilit screen V8040
        Janus.Windows.UI.Dock.UIPanelGroup uiPanSplitScreen =null;
        gloPMGeneral.clsSplitScreenPM clsSplit_PatientCharges = null;
        
        private void frmBillingTransaction_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            LockWindowUpdate(this.Handle);
            this.SuspendLayout();
            
           
          //  UC_gloBillingTransactionLines.IcdCodeType=  (int)gloGlobal.gloICD.CodeRevision.ICD9;
            mskOnsiteDate.BringToFront();
            PerformFormLoad();
            mskClaimDate.Focus();
            mskClaimDate.Select();

            //Start of initialising and rendering  Spilit screen added by manoj jadhav on 20150202 V8040
            if (gloGlobal.gloPMGlobal.ViewDocumentsOnCharges)
            {
                lblViewDocuments.Visible = true;
                lblF7.Visible = true;               
                this.mnuBilling_ShowSplitScreen.Click += new System.EventHandler(this.mnuBilling_ShowSplitScreen_Click);
                clsSplit_PatientCharges = new gloPMGeneral.clsSplitScreenPM();
                clsSplit_PatientCharges._DatabaseConnectionString = _DatabaseConnectionString;
                clsSplit_PatientCharges.clsDMS = new gloEDocumentV3.eDocManager.eDocGetList();
                uiPanSplitScreen = clsSplit_PatientCharges.LoadSplitControl(this, _PatientID, 0, "Charges", gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID);
                uiPanSplitScreen.Visible = true;
                uiPanSplitScreen.BringToFront();
            }           
            //END of initialising and rendering  Spilit screen added by manoj jadhav on 20150202 V8040

            this.ResumeLayout();
            
            cmnu_DxNoPost.Visible = false;

 			if (!bIsCopiedClaim)
            {
                //// Problem# 243 - When Accident type is not selected Onsite Date is not saved.
                CmbAccidentType.SelectedIndex = 0;
                //// End
            }

            if (!bIsCopiedClaim)
            {
                switch (GetICDCodeType(UC_gloBillingTransactionLines._nContactID, UC_gloBillingTransactionLines.getfirstservicelineDos()))
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
            if (rbICD9.Checked)
            { tlsICD10CoddingRules.Visible = false; }
            else
            { tlsICD10CoddingRules.Visible = true; }
            

            oPatientControl.IsCalledFromChargesOrModifyCharges = true;
            this.Cursor = Cursors.Default;
            if (bIsCopiedClaim && ReplacementClaimCreationType == ReplacementClaimCreationType.Replacement)
                tls_btnInsurancePayment.Visible = true;
            else
                tls_btnInsurancePayment.Visible = false;

            // License Check
            List<object> _ToolStrip = new List<object>();
            _ToolStrip.Add(this.tls_btnOK);
            _ToolStrip.Add(this.tls_btnSaveNClose);          
            base.FormControls = null;
            //if (gloGlobal.gloPMGlobal.LoginProviderID == 0) { base.strProviderID = _PatientProviderId.ToString(); }                
            base.FormControls = _ToolStrip.ToArray();
            base.SetChildFormControls();
            _ToolStrip = null;


            if (IsMissingChargeLoad == true)
            {
                GetReportMissingCharges();
            }
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_DatabaseConnectionString);
            object value = null;
            ogloSettings.GetSetting("OCPPORTALENABLE", out value);

            if (value != null && Convert.ToString(value).Trim() != "")
            {
                //bIsOCPPortalEnable = Convert.ToBoolean(value);
                if (value.ToString() == "0")
                    bIsOCPPortalEnable = false;
                else
                    bIsOCPPortalEnable = true;
                tls_btnOnlineCharge.Visible = bIsOCPPortalEnable;

            }
            else
            {
                tls_btnOnlineCharge.Visible = bIsOCPPortalEnable;
            }

            DefaultSelfPayFeeSchedule();

            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_DatabaseConnectionString);
            object oValue = null;
            oSettings.GetSetting("EnableDublicateCPTsWarning", out oValue);

            if (oValue != null && Convert.ToString(oValue).Trim() != "")
            {
                if (oValue.ToString().ToUpper() == "FALSE")
                    bIsDublicteCPTsWarningEnable = false;
                else
                    bIsDublicteCPTsWarningEnable = true;
            }
        }
       
        public string sSelectedAppointmentDOS = String.Empty;
        public string sSelectedAppointmentLocation = String.Empty;
        public string sSelectedAppointmentProvider = String.Empty;
        public Int64 nSelectedAppointmnetLocationID = 0;
        public Int64 sSelectedAppointmentProviderID = 0;
        public Int64 nSelectedAppointmentFacilityID = 0;
        public Int64 nSelectedAppointmentTypeID = 0 ;
        public Int64 nSelectedAppointmentID = 0;
        public bool DefaultApptDos = false;
        public bool DefaultApptFacility = false;
        public bool DefaultApptRenderringProvider = false;

        public void GetPatientMissingCharges(gloPMGeneral.gloAppointmentsChargesLinking.frmPatientAppointments frmPatientAppointment)
        {

            try
            {
                //SetPatientAppointmentsLinked();

                DefaultApptDos = frmPatientAppointment.bIsDefaultDOS;
                DefaultApptFacility = frmPatientAppointment.bIsDefaultFacility;
                DefaultApptRenderringProvider = frmPatientAppointment.bIsDefaultRenderingProvider;


                sSelectedAppointmentDOS = frmPatientAppointment.DefaultDos;
                sSelectedAppointmentLocation = frmPatientAppointment.Location;
                sSelectedAppointmentProvider = frmPatientAppointment.Provider;
                sSelectedAppointmentProviderID = frmPatientAppointment.ProviderID;
                nSelectedAppointmnetLocationID = frmPatientAppointment.LocationID;
                nSelectedAppointmentFacilityID = frmPatientAppointment.FacilityID;


                if (sSelectedAppointmentDOS != "" || sSelectedAppointmentLocation != "" || sSelectedAppointmentProvider != "")
                {
                    IsAppointmentPresent = true;
                }

                if (IsAppointmentPresent == true)
                {
                    if (IsValidDate(sSelectedAppointmentDOS) == true && DefaultApptDos == true)
                    {
                        #region " Set Close Date to addedd Line "

                        //..Start: Code changes done by Sagar Ghodke on 3 Jan 2013
                        //..Code change done against Incident#8139
                        //..previous condition, << if (UC_gloBillingTransactionLines != null) >>
                        //..New charges setup the DOS on billing control depending upon the close date, since new charges screen has one 
                        //..empty charge line by default it sets the close date as DOS.
                        //..The copy/replace claim functionality set's the existing claim data on control where the DOS is set right, but this line
                        //..of code changes it to the close date again.
                        //..New condition includes the bIsCopiedClaim variable if it is true then do not attempt to set the DOS.
                        if (UC_gloBillingTransactionLines != null && bIsCopiedClaim == false) //..End : Code changes
                        {
                            if (IsValidDate(sSelectedAppointmentDOS) == true)
                            {
                                //Bug #53622: 7040 - gloPM - Missing Charges - DOS changes to system Date on Charges screen invoked through Missing charges
                                //Description: DOS changes to close date if present or to system date if open from missing charges so added condition
                                if (_IsOpenForExternal != true)
                                {
                                    UC_gloBillingTransactionLines.SetServiceLineDateForMissingCharge(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(sSelectedAppointmentDOS));
                                }

                            }
                        }
                        //..End
                        #endregion " Set Close Date to addedd Line "
                    }

                    #region Fill Provider

                    if (sSelectedAppointmentProviderID != 0 && DefaultApptRenderringProvider == true)
                    {
                        UC_gloBillingTransactionLines.SetServiceLineProvider(UC_gloBillingTransactionLines.CurrentTransactionLine, sSelectedAppointmentProviderID, sSelectedAppointmentProvider);
                    }

                    #endregion Fill Provider

                    #region fill facility

                    if (cmbFacility != null && cmbFacility.Items.Count > 0 && nSelectedAppointmentFacilityID != 0 && DefaultApptFacility == true)
                    {
                        if (cmbFacility.SelectedIndex == -1)
                        {
                            //string abc = Convert.ToInt64((System.Data.DataRowView)(cmbFacility.Items[0])).Row.ItemArray[0].ToString());
                            if (bIsFacilityChangeFromModifyPatient && UC_gloBillingTransactionLines._facilityID == Convert.ToInt64(((System.Data.DataRowView)(cmbFacility.Items[0])).Row.ItemArray[0]))
                            {
                                bModifyPOS = true;
                            }
                            cmbFacility.SelectedIndex = 0;
                            bModifyPOS = false;
                        }

                        if (UC_gloBillingTransactionLines != null && cmbFacility.SelectedIndex != -1)
                        {
                            UC_gloBillingTransactionLines.FacilityID = nSelectedAppointmentFacilityID;
                            cmbFacility.SelectedValue = nSelectedAppointmentFacilityID;
                            SetFacilitySettingsData();
                        }
                    }
                    #endregion


                }
                if ( Convert.ToInt64(frmPatientAppointment._AppointmentID) > 0)
                {
                    lstAppointmentIDs.Clear();
                    CheckForPriorAppointments(Convert.ToInt64(frmPatientAppointment._AppointmentID));
                }
                //  FillPatientAppointmentsOnLoad();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
               
            }
        }

        public void GetReportMissingCharges()
        {
            DataSet dtSettings = null;
            DataTable dt = null;
            try
            {
                dtSettings = gloCharges.GetMissingChargeAppointments(_PatientID, 1, nSelectedAppointmentTypeID);
                if (dtSettings.Tables.Count > 1)
                {
                    string nSelectedId = "ID=" + nSelectedAppointmentID;
                    DataRow[] foundRows;
                    foundRows = dtSettings.Tables[0].Select(nSelectedId);
                    if (foundRows.Length != 0)
                    {
                        DefaultApptDos = Convert.ToBoolean(foundRows[0]["bIsDefaultDOS"]);
                        DefaultApptFacility = Convert.ToBoolean(foundRows[0]["bIsDefaultFacility"]);
                        DefaultApptRenderringProvider = Convert.ToBoolean(foundRows[0]["bIsDefaultRenderingProvider"]);
                    }
                }

                if (IsValidDate(sSelectedAppointmentDOS) == true && DefaultApptDos == true)
                {
                    #region " Set Close Date to addedd Line "

                    //..Start: Code changes done by Sagar Ghodke on 3 Jan 2013
                    //..Code change done against Incident#8139
                    //..previous condition, << if (UC_gloBillingTransactionLines != null) >>
                    //..New charges setup the DOS on billing control depending upon the close date, since new charges screen has one 
                    //..empty charge line by default it sets the close date as DOS.
                    //..The copy/replace claim functionality set's the existing claim data on control where the DOS is set right, but this line
                    //..of code changes it to the close date again.
                    //..New condition includes the bIsCopiedClaim variable if it is true then do not attempt to set the DOS.
                    if (UC_gloBillingTransactionLines != null && bIsCopiedClaim == false) //..End : Code changes
                    {
                        if (IsValidDate(sSelectedAppointmentDOS) == true)
                        {
                            //Bug #53622: 7040 - gloPM - Missing Charges - DOS changes to system Date on Charges screen invoked through Missing charges
                            //Description: DOS changes to close date if present or to system date if open from missing charges so added condition
                            if (_IsOpenForExternal != true)
                            {
                                UC_gloBillingTransactionLines.SetServiceLineDateForMissingCharge(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(sSelectedAppointmentDOS));
                            }

                        }
                    }
                    //..End
                    #endregion " Set Close Date to addedd Line "
                }
                #region Fill Provider

                if (sSelectedAppointmentProviderID != 0 && DefaultApptRenderringProvider == true)
                {
                    UC_gloBillingTransactionLines.SetServiceLineProvider(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToInt64(sSelectedAppointmentProviderID), sSelectedAppointmentProvider);
                }

                #endregion Fill Provider
                //#region fill facility

                //if (cmbFacility != null && cmbFacility.Items.Count > 0 && nSelectedAppointmentFacilityID != 0 && DefaultApptFacility == true)
                //{
                //    if (cmbFacility.SelectedIndex == -1)
                //    {
                //        //string abc = Convert.ToInt64((System.Data.DataRowView)(cmbFacility.Items[0])).Row.ItemArray[0].ToString());
                //        if (bIsFacilityChangeFromModifyPatient && UC_gloBillingTransactionLines._facilityID == Convert.ToInt64(((System.Data.DataRowView)(cmbFacility.Items[0])).Row.ItemArray[0]))
                //        {
                //            bModifyPOS = true;
                //        }
                //        cmbFacility.SelectedIndex = 0;
                //        bModifyPOS = false;
                //    }

                //    if (UC_gloBillingTransactionLines != null && cmbFacility.SelectedIndex != -1)
                //    {
                //        UC_gloBillingTransactionLines.FacilityID = nSelectedAppointmentFacilityID;
                //        cmbFacility.SelectedValue = nSelectedAppointmentFacilityID;
                //        SetFacilitySettingsData();
                //    }
                //}
                //#endregion
                if (nSelectedAppointmentID != 0 && nSelectedAppointmentID > 0)
                {
                    CheckForPriorAppointments(Convert.ToInt64(nSelectedAppointmentID));
                }
                //FillPatientAppointmentsOnLoad();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (dtSettings != null)
                {
                    dtSettings.Dispose();
                    dtSettings = null;
                }
            }
        }
       


        private void frmBillingTransaction_Shown(object sender, EventArgs e)
        {
            DataTable dtCases = null;
            DataTable dtCaseDiag = null;
            DataTable dtCasesIns = null;
            DialogResult oResult = DialogResult.Yes;

            cmbFacility.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFacility.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);


            cmbReferralProvider.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReferralProvider.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbClaimCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbClaimCategory.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            Cls_GlobalSettings.ControlMover(this.lblExamDxCPT, this.pnlExamCPTDX);
            Cls_GlobalSettings.ControlMover(this.imgCPTDX, this.pnlExamCPTDX);
            Cls_GlobalSettings.ControlMover(this.pnlExamDxCPTTop, this.pnlExamCPTDX);

            Cls_GlobalSettings.ControlMover(this.lblOCPDX, this.pnlOCPCPTDX);
            Cls_GlobalSettings.ControlMover(this.imgOCPDX, this.pnlOCPCPTDX);
            Cls_GlobalSettings.ControlMover(this.pnlOCPDxTop, this.pnlOCPCPTDX);

            tls_btnMoreChargeData.Enabled = false;
            tls_Anesthesia.Enabled = false;

            if (bIsCopiedClaim)
            {
                if ((UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE) != null) && (UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE).ToString().Trim() != ""))
                {
                    tls_btnMoreChargeData.Enabled = true;
                    tls_Anesthesia.Enabled = true;
                }
            }
            
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);

            btnShowInsurance.BackgroundImage = global::gloBilling.Properties.Resources.UP;
            btnShowInsurance.BackgroundImageLayout = ImageLayout.Center;

            LockWindowUpdate(IntPtr.Zero);
            if (_TransactionID <= 0)
            {
                //IsDefaultFeeScheduleExpired();

                if (_ShowSelectTray == true)
                {
                    SelectChargeTray();
                }

                #region "Plot Case Data if there is only single valid case against current patient"

                mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskClaimDate.Text != "")
                {
                    mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                }

                gloCharges.getSingleValidCase((mskClaimDate.Text != string.Empty ? Convert.ToDateTime(mskClaimDate.Text) : DateTime.Now), _PatientID, out dtCases, out dtCaseDiag, out dtCasesIns);
                if (dtCases.Rows.Count == 1)
                {
                    if (oResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        _dtCaseData = dtCases.Copy();
                        txtCases.Tag = dtCases.Rows[0]["nCaseID"];
                        txtCases.Text = Convert.ToString(dtCases.Rows[0]["sCaseName"]);

                        this.PatientHasCases = true;
                        CheckForPatientCases();

                        //Setting Case data into charge master form
                        SetCaseDetailsintoCharge(dtCases);
                        //Setting Case diagonosis into charge lines                     
                        SetCaseDiagonosisintoChargeLines(dtCaseDiag);
                        //Setting Case Insurance
                        //_IsFormLoading = true;
                        SetCaseInsurance(dtCasesIns);
                        //_IsFormLoading = false;

                        
                    }
                }
                else
                {
                    //BudID - 35835 : Charges >> Application does no display Init. treatment date box on form load
                    /// Previously it was ( CmbAccidentType.SelectedIndex == -1 ) now we have check whether it was less that or equal to 0.
                    if (bShowInitialTreatmentDate && CmbAccidentType.SelectedIndex <=0)
                    {
                        lblInitDate.Visible = false;
                        mskInitTreatment.Visible = false;
                    }
                    else
                    {
                        lblInitDate.Visible = false;
                        mskInitTreatment.Visible = false;
                    }
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
                mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals; 

                #endregion

               
            }
          
        }

        private void SelectChargeTray()
        {
            frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                //...Show pop-up to select the Tray
                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingTraySelection.IsChargeTray = true;
                if (lblCloseDayTray.Text != "")
                {
                    ofrmBillingTraySelection.IsChargeTrayLoaded = true;
                    ofrmBillingTraySelection.LoadedChargeTrayID = Convert.ToInt64(lblCloseDayTray.Tag);
                    ofrmBillingTraySelection.LoadedChargeTray = Convert.ToString(lblCloseDayTray.Text);
                }
                ofrmBillingTraySelection.ShowDialog(this);

                //toolTip1.SetToolTip(lblCloseDayTray, null);

                // If Charge tray Modified or selected from Charge tray dialog then reflect the changes
                if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        this.SelectedChargeTray = ofrmBillingTraySelection.SelectedTrayName;
                        this.SelectedChargeTrayID = ofrmBillingTraySelection.SelectedTrayID;

                        //toolTip1.SetToolTip(lblCloseDayTray, ofrmBillingTraySelection.SelectedTrayName);
                    }
                    else
                    {
                        this.SelectedChargeTray = string.Empty;
                        this.SelectedChargeTrayID = 0;
                    }
                }
                // If Charge Tray dialog closed and Charge tray made inactivated then reflect the changes
                else if (ofrmBillingTraySelection.IsOperationPerformed)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        this.SelectedChargeTray = ofrmBillingTraySelection.SelectedTrayName;
                        this.SelectedChargeTrayID = ofrmBillingTraySelection.SelectedTrayID;

                        //toolTip1.SetToolTip(lblCloseDayTray, ofrmBillingTraySelection.SelectedTrayName);
                    }
                    else
                    {
                        this.SelectedChargeTray = string.Empty;
                        this.SelectedChargeTrayID = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ofrmBillingTraySelection.Dispose();
            }
        }

        private void PerformFormLoad()
        {
            try
            {
                _IsFormLoading = true;

                SetupData();

               // if (UC_gloBillingTransactionLines.Fee_ScheduleID > 0)
                //{
                _DefaultFeeScheduleID = gloCharges.GetProviderFeeScheduleID(_PatientPoviderID);
                if (_DefaultFeeScheduleID==0)
                {
                    _DefaultFeeScheduleID = gloCharges.GetClinicFeeScheduleID();
                }
                
                if (_DefaultFeeScheduleID > 0)
                {
                    cmbFeeSchedule.SelectedValue = _DefaultFeeScheduleID;
                }
                //}

                //Set default POS to c1_transaction on load
                if (UC_gloBillingTransactionLines.FacilityPOS > 0)
                {
                    if (UC_gloBillingTransactionLines != null)
                    {
                        UC_gloBillingTransactionLines.SetFacilityPOS();
                    }
                }

                if (cmbBillingProvider.SelectedIndex >= 0)
                {
                    if (UC_gloBillingTransactionLines != null)
                    {
                        UC_gloBillingTransactionLines.BillingProviderID = Convert.ToInt64(cmbBillingProvider.SelectedValue);
                    }
                }

                if (IsValidDate(mskClaimDate.Text) == true)
                {
                    #region " Set Close Date to addedd Line "

                    //..Start: Code changes done by Sagar Ghodke on 3 Jan 2013
                    //..Code change done against Incident#8139
                    //..previous condition, << if (UC_gloBillingTransactionLines != null) >>
                    //..New charges setup the DOS on billing control depending upon the close date, since new charges screen has one 
                    //..empty charge line by default it sets the close date as DOS.
                    //..The copy/replace claim functionality set's the existing claim data on control where the DOS is set right, but this line
                    //..of code changes it to the close date again.
                    //..New condition includes the bIsCopiedClaim variable if it is true then do not attempt to set the DOS.
                    if (UC_gloBillingTransactionLines != null && bIsCopiedClaim == false) //..End : Code changes
                    {
                        if (IsValidDate(mskClaimDate.Text) == true)
                        {
                            //Bug #53622: 7040 - gloPM - Missing Charges - DOS changes to system Date on Charges screen invoked through Missing charges
                            //Description: DOS changes to close date if present or to system date if open from missing charges so added condition
                            if (_IsOpenForExternal != true)
                            {
                                UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(mskClaimDate.Text));
                            }
                            
                        }
                    }
                    //..End
                    #endregion " Set Close Date to addedd Line "
                }

                lblSearch.Text = "Exam Name : ";
                lblSearch.Tag = "Exam";

                //GetHoldMessage();
                this.FillPatientAppointmentsOnLoad();                
                SetHoldnMoreClaimDataMesseges();
                SetLastGlobalPeriods();
                //pnlExamCPTDX.SendToBack();

                if (cmbReferralProvider.Items.Count == 2)
                {
                    cmbReferralProvider.SelectedIndex = 1;
                }
                //Code Added for self option availability on lines as per current Resposibility -Sameer 11-15-2013
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
                //Code Added for self option availability
                gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_DatabaseConnectionString);
                object value = null;
                ogloSettings.GetSetting("EMRTREATMENT_SPLITS", out value);


                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    bisEMRTreatmentSplitEnabled = Convert.ToBoolean(value);
                }

                if (ogloSettings != null) { ogloSettings.Dispose(); }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                _IsFormLoading = false;
            }

        }

        private void SetupData()
        {
            try
            {
                pnlReplacmentClaimDtls.Visible = false;
                tlb_AddEMRTreatment.Visible = true;
                SetFormLoadData();
                txtOutSideLabCharges.Text = "0.00";
                _sResponsibleNo = Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "");
                _nResponsibleParty = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                LoadBillingControl();               
                //RoopaliB 19 July 2012
                // New setting added to insurance plan setup to set where occurance date should be default to DOS or not.
                // Same setting we are going to retrive here and will assigned to UB04 object
                // This is done here so that if user didn't explicitly opens UB04 data window 
                // it should respect setting accosiated to insurance plan
                UB04Default();
                /////////////////////////////////////////////////////////
                if (bIsCopiedClaim)
                {
                    CreateCopyofClaim();
                    if (ReplacementClaimCreationType == global::gloBilling.ReplacementClaimCreationType.Replacement)
                    {
                        pnlReplacmentClaimDtls.Visible = true;
                        lblReplacementClaim.Text = "Replaces Claim : ";
                        lblReplacementClaimNo.Text = sReplaingClaimNo;
                    }
                    tlb_AddEMRTreatment.Visible = false;
                }

               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }
        
        #endregion " Form Load "

        #region " Delegated Events "
        void UC_gloBillingTransactionLines_CLIA_Enter()
        {
            #region "CLIANo. of FirstCharge in CLIANo. TextBox"
                txtClaimCLIAno.Text = UC_gloBillingTransactionLines.FirstCLIANo;            
            #endregion
        }

        void UC_gloBillingTransactionLines_onGrid_SelChanged(object sender, RangeEventArgs e)
        {
           
            try
            {
                if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                {

                    if ((Convert.ToString(UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE)).Trim() != "") && (UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE) != null))
                    {
                        tls_btnMoreChargeData.Enabled = true;
                        tls_Anesthesia.Enabled = true;
                    }
                    else
                    {
                        //if ((Convert.ToString(UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_NDCCODE)).Trim() != "") && (UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_NDCCODE) != null))
                        //{
                        //    TransactionLines oLineTransacts = null;
                        //    oLineTransacts = UC_gloBillingTransactionLines.GetLineTransactions();
                        //    oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode = "";
                        //    oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCUnit = null;
                        //    oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCUnitCode = null;
                        //    oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCUnitDescription = null;
                        //    UC_gloBillingTransactionLines.SetNDCFields(oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1]);
                        //} 
                        tls_btnMoreChargeData.Enabled = false;
                        tls_Anesthesia.Enabled = false;
                       
                    }
                }
                //select the patient insurance as per the insurance in the transaction line
              
                this.c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                this.c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);

                if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.GetLinesCount() > 0)
                {
                    #region " Set Line Insurance to Insurance Grid "

                    if (UC_gloBillingTransactionLines.HasInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine) == true)
                    {
                        int rowIndex = UC_gloBillingTransactionLines.CurrentTransactionLine;
                        Int64 _TempInsuranceid = UC_gloBillingTransactionLines.GetRowInsuranceId(rowIndex);
                        if (_TempInsuranceid >= 0)
                        {
                            //select the corresponding insurance in the Patient Insurance Grid Above
                            if (c1Insurance != null && c1Insurance.Rows.Count > 0)
                            {
                                for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                                {
                                    if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCEID)) != "")
                                    {
                                        if (Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID)) == _TempInsuranceid)
                                        {
                                            c1Insurance.SetCellCheck(i, COL_SELECT, CheckEnum.Checked);
                                        }
                                        else
                                        {
                                            c1Insurance.SetCellCheck(i, COL_SELECT, CheckEnum.Unchecked);
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        //Uncheck all 
                        if (c1Insurance != null && c1Insurance.Rows.Count > 0)
                        {
                            for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                            {
                                c1Insurance.SetCellCheck(i, COL_SELECT, CheckEnum.Unchecked);
                            }
                        }

                    }

                    #endregion " Set Line Insurance to Insurance Grid "

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
                    if (UC_gloBillingTransactionLines.sMammogramCertNo != "")
                    {
                        this._MammogramCertNumber = UC_gloBillingTransactionLines.sMammogramCertNo;
                    }
                    
                    #endregion " Set Line Primary Dx "
                }
                //UC_gloBillingTransactionLines.SetFNFCharges();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                this.c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
            }
            finally
            {
                this.c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                if (!_bDXSwitch)
                {
                    this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                }
            }

        }

        //****************************************************************************************************
        //Function Added By Debasish
        //Reason: Set Primary DX In Diagonosis Grid
        //****************************************************************************************************
        private void SetPrimaryDXInDiagonosisGrid()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
               
            }
        }
        //********************************Ends Here***********************************************************

        #endregion " Delegated Events "

        #region " Tool Strip Item Click Event "

        private void tls_Top_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        

            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            
                            tls_Top.Select();  
        
                        }
                        break;
                    case "SaveandNextTreatment": //new button for save and go to EMR List
                        {
                            
                        }
                        break;
                    case "AddLine":
                        {
                            
                        }
                        break;
                    case "InsertLine":
                        {
                            if (!(UC_gloBillingTransactionLines == null))
                            {
                                UC_gloBillingTransactionLines.InsertTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.AddTransactionLine, "Transaction Line inserted in to Billing Control,Line Number : " + UC_gloBillingTransactionLines.CurrentTransactionLine + " ", this.PatientID, _TransactionID, _PatientPoviderID, ActivityOutCome.Success);
                                UC_gloBillingTransactionLines.SortControl();
                            }
                        }
                        break;
                    case "RemoveLine":
                        {
                            
                        }
                        break;

                    case "AddNotes":
                        {
                            
                        }
                        break;
                    case "MoreChargeData":
                        {
                           
                        }
                        break;

                    case "MoreClaimData":
                        {
                            
                        }
                         
                        break;
  
                    case "Patient":
                        {
                            LoadPatientList(false);
                        }
                        break;
                    case "SmartTreatment":
                        {
                            
                        }
                        break;

                    case "EMRTreatment":
                        
                        break;
                    case "VoidEMRTreatment":
                        

                        break;
                    case "CloseEMRTreatment":
                        {
                            

                        }
                        break;
                    case "CancelEMRTreatment":
                        {
                            
                        }
                        break;
                    case "LoadExam":
                        {
                            
                        }
                        break;
                    case "ExamDXCPT":
                        {

                            

                        }
                        break;
                    case "Cancel":
                            
                        
                        break;
                    case "SaveNClose":

                        tls_Top.Select();
                        
                        break;
                    case "Payment":
                        {
                            

                        }
                        break;

                    case "Hold":
                        {
                            
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //this code is return for if error occure before visiable false wait screen then it will back charge screen as it is
                lblPleaseWait.Visible = false;
                lblPleaseWait.Enabled = false;

                _bDxFlag = false;
                _bSmartTreatmentLoading = false;
               
                if (this.Enabled == false) { this.Enabled = true; }
                if (pnlTransactionMaster.Enabled == false) { pnlTransactionMaster.Enabled = true; }
                if (pnlTransactionLines.Enabled == false) { pnlTransactionLines.Enabled = true; }
                if (pnlToolStrip.Enabled == false) { pnlToolStrip.Enabled = true; }

            }
        }

        #endregion " Tool Strip Item Click Event "

        #region "Fill data"
        
        void UC_gloBillingTransactionLines_onGrid_CellChanged(object sender, RowColEventArgs e)
        {
            if (UC_gloBillingTransactionLines._bISCptBlank)
            {
                tls_btnMoreChargeData.Enabled = false;
                tls_Anesthesia.Enabled = false;
                UC_gloBillingTransactionLines._bISCptBlank = false;
                return;
            }
            UC_gloBillingTransactionLines.PerformValidation = true;
           
        }

        private void FillPatientInsurances(Int64 PatientID)
        {
            c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);

            DataTable dtPatientInsurances = null;
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_DatabaseConnectionString);
            bool _IsPrimaryPresent = false;
            bool _HasInsurance = true;

            int _CntPrimary = 0;


            try
            {


                dtPatientInsurances = ogloPatient.getPatientInsurances(_PatientID);
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

                            c1Insurance.SetData(rowIndex, COL_INSURANCERESPONSIBILITY, Convert.ToString(""));
                            c1Insurance.SetData(rowIndex, COL_INSURANCEPARTY, Convert.ToString("X"));

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
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCERESPONSIBILITY, Convert.ToString(""));
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEPARTY, Convert.ToString("X"));
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCENAME, "Self"); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEID, 0); //                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSSELFMODE, PayerMode.Self.GetHashCode()); //
                c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCECONTACTID, 0);
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

                //if (c1Insurance.GetData(1, COL_INSURANCEPARTY).ToString() != "\0")
                //{
                //if (_CntPrimary == 1)
                //{
                    //c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, null);
                    System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                    c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, imgFlag);
                //}
                c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                SetInsuranceResponsibility(1);
                ChangeInsuranceGridColor();
                ogloBilling.Dispose();
                ogloPatient.Dispose();
                if (dtPatientInsurances != null)
                {
                    dtPatientInsurances.Dispose();
                    dtPatientInsurances = null;
                }
            }
            c1Insurance.Select(1, 1);
        }

        private void SelectPrimaryInsurance()
        {
            bool _IsSelected = false;
            try
            {
                if (c1Insurance != null && c1Insurance.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE)) == "Primary")
                        {
                            c1Insurance.SetCellCheck(i, COL_SELECT, CheckEnum.Checked);
                            _IsSelected = true;
                        }
                    }

                    //If no Insurance is present the select the Self mode
                    if (_IsSelected == false) { c1Insurance.SetCellCheck(c1Insurance.Rows.Count - 1, COL_SELECT, CheckEnum.Checked); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
            }
        }

        #endregion

        #region "Patient List Control Methods & Events"
        private void removeOListControl()
        {
            if (oListControl != null)
            {
                if (pnlListContainer.Controls.Contains(oListControl))
                {
                    try
                    {
                        pnlListContainer.Controls.Remove(oListControl);
 
                    }
                    catch
                    {

                    }
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch
                {
                }
                try
                {
                     
                    oListControl.Dispose();
                    oListControl = null;
                }
                catch
                {

                }

            }
        }
        private void LoadPatientList(bool IsEMRPatientList)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = pnlListContainer.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (pnlListContainer.Controls[i].Name == oListControl.Name)
                //        {
                //            pnlListContainer.Controls.Remove(pnlListContainer.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                if (IsEMRPatientList)
                {
                    oListControl = new gloListControl.gloListControl(_DatabaseConnectionString, gloListControl.gloListControlType.EMRPatient, false, this.Width);
                    oListControl.ControlHeader = " EMR Patient";
                    oListControl.ClinicID = _ClinicID;
                    _CurrentControlType = gloListControl.gloListControlType.EMRPatient;
                }
                else
                {
                    oListControl = new gloListControl.gloListControl(_DatabaseConnectionString, gloListControl.gloListControlType.Patient, false, this.Width);
                    oListControl.ClinicID = _ClinicID;
                    oListControl.ControlHeader = " Patient";
                    _CurrentControlType = gloListControl.gloListControlType.Patient;
                }

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                
                tls_btnOK.Visible = false;
                tls_btnCancel.Visible = false;
                tls_btnRemoveLine.Visible = false;
                tls_btnAddLine.Visible = false;

                pnlTransactionMaster.Visible = false;
                pnlTransactionLines.Visible = false;
                pnlTransactionOther1.Visible = false;
                //pnlTransactionOther2.Visible = false;
                pnlToolStrip.Visible = false;

                pnlListContainer.Visible = true;
                pnlListContainer.BringToFront();

                pnlListContainer.Controls.Add(oListControl);

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }

        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {

                //if (oListControl != null)
                //{
                //    for (int i = pnlListContainer.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (pnlListContainer.Controls[i].Name == oListControl.Name)
                //        {
                //            pnlListContainer.Controls.Remove(pnlListContainer.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                
                tls_btnOK.Visible = true;
                tls_btnCancel.Visible = true;
                tls_btnRemoveLine.Visible = true;
                tls_btnAddLine.Visible = true;

                pnlTransactionMaster.Visible = true;
                pnlTransactionLines.Visible = true;
                pnlTransactionOther1.Visible = false;
                //pnlTransactionOther2.Visible = false;
                pnlToolStrip.Visible = true;

                pnlListContainer.Visible = false;
                pnlListContainer.SendToBack();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                switch (_CurrentControlType)
                {
                    case gloListControl.gloListControlType.Other:
                        break;
                    case gloListControl.gloListControlType.Patient:
                        {
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                //Added by Subashish_b on 03/Feb /2011 (integration made on date-10/May/2011) for  change the control name of new patient banner (gloStripControl.FormName.NewCharges)
                                //oPatientControl.FillDetails(oListControl.SelectedItems[0].ID, gloPatientStripControl.FormName.NewCharges, 1, false);
                                oPatientControl.FillDetails(oListControl.SelectedItems[0].ID, gloStripControl.FormName.NewCharges, 1, false);
                                //End
                                _PatientID = Convert.ToInt64(oListControl.SelectedItems[0].ID);
                                //FillPatientInsurances(_PatientID);
                                LoadPatientModifiedData();
                            }
                        }
                        break;
                    case gloListControl.gloListControlType.Insurance:
                        break;
                    case gloListControl.gloListControlType.PatientInsurence:
                        break;
                    case gloListControl.gloListControlType.EMRPatient:
                        {
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                //_PatientEMRID = Convert.ToInt64(oListControl.SelectedItems[0].ID);
                                //_PatientEMRCode = Convert.ToString(oListControl.SelectedItems[0].Code);
                                //gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
                                //ogloBilling.AssociatePatient(_PatientID, _PatientEMRID, _PatientEMRCode);
                                //ogloBilling.Dispose();
                                //GetEMRTreatment(_PatientEMRID);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                
                tls_btnOK.Visible = true;
                tls_btnCancel.Visible = true;
                tls_btnRemoveLine.Visible = true;
                tls_btnAddLine.Visible = true;
                

                pnlTransactionMaster.Visible = true;
                pnlTransactionLines.Visible = true;
                pnlTransactionOther1.Visible = false;
                //pnlTransactionOther2.Visible = false;
                pnlToolStrip.Visible = true;

                pnlListContainer.Visible = false;
                pnlListContainer.SendToBack();

            }
        }

        #endregion

        #region " Patient Strip Control Events "

        public void oPatientControl_OnPatientSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            SetPatientBannerAccountPatientChangeData();
        }

        private void oPatientControl_ControlSize_Changed(object sender, EventArgs e)
        {

        }

        private void oPatientControl_PatientChanged(object sender, EventArgs e)
        {
            SetPatientBannerAccountPatientChangeData();
        }

        private void oPatientControl_OnAccountChanged(object sender, EventArgs e)
        {
            _PatientID = oPatientControl.CmbSelectedPatientID;
            nPAccountID = oPatientControl.PAccountID;
            nAccountPatientID = oPatientControl.AccountPatientID;
            nGuarantorID = oPatientControl.GuarantorID;
            //SetLastGlobalPeriods();
            SetPatientBannerAccountPatientChangeData();
        }

        private void oPatientControl_OnCmbAccountChanged(object sender, EventArgs e)
        {
            _PatientID = oPatientControl.CmbSelectedPatientID;
            nPAccountID = oPatientControl.PAccountID;
            nAccountPatientID = oPatientControl.AccountPatientID;
            nGuarantorID = oPatientControl.GuarantorID;
            //SetLastGlobalPeriods();
            oPatientControl.FillDetails(_PatientID, nPAccountID, gloStripControl.FormName.NewCharges, true);
            //SetPatientBannerAccountPatientChangeData();
        }

        //private void oPatientControl_OnAccountPatientChanged(object sender, EventArgs e)
        //{
        //}

        void oPatientControl_PatientModified(object sender, EventArgs e)
        {
            bIsFacilityChangeFromModifyPatient = true;
            LoadPatientModifiedData();
            bIsFacilityChangeFromModifyPatient = false;
        }

        #endregion " Patient Strip Control Events "

        #region " Public & Private Methods "

        private void CheckForEPSDTEnabled()
        {
            if (c1Insurance.Rows.Count > 1)
            {
                bool bEnableEPSDT = false;
                Boolean.TryParse(GetSetting("EnableEPSDTFamilyPlanning"), out bEnableEPSDT);

                //bool bEnableEPSDTPlan = gloCharges.GetPromptForEPSDT(Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)));
                bool bEnableEPSDTPlan = gloCharges.GetPromptForEPSDT(oPatientControl.PatientID);
                if (bEnableEPSDTPlan && bEnableEPSDT)
                {
                    EPSDTEnabled = true;
                }
                else
                {
                    EPSDTEnabled = false;
                }

                if (!EPSDTEnabled)
                {
                    tlb_AddEPSDT.Visible = false;
                }
                else
                {
                    tlb_AddEPSDT.Visible = true;
                }
            }
        }

        private void IsAnesthesiaEnabled()
        {
            if (!bIsAnesthesiaEnabled)
            {
                tls_Anesthesia.Visible = false;
            }
            else
            {
                tls_Anesthesia.Visible = true;
            }
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

        public static bool IsValidatePatientAccount(Int64 nPAccountID, Int64 nPatientID)
        {
            string result = "";
            bool blnReturn = true;
            object oReturnValue;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", AppSettings.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sResult", result, ParameterDirection.Output, SqlDbType.VarChar, 255);//	numeric(18, 0)	Unchecked
                oDB.Connect(false);
                oDB.Execute("PA_Validate_AccountPatient", oParameters, out oReturnValue);

                result = oReturnValue.ToString();

                if (result.Trim().Length > 0)
                {
                    // Display Error Message and return 
                    MessageBox.Show(result, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    blnReturn = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                blnReturn = false;
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }

            return blnReturn;
        }

        private bool ValidationMessageExpClaim()
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            Int64 _ContactId = 0;
            int _cntr = 0;
            
            try
            {
                
                if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                {
                    _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                    
                }


                ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);

                
                ArrayList arrDX = UC_gloBillingTransactionLines.GetUniqueDx();
                if (arrDX != null)
                {
                    _cntr = arrDX.Count;
                }

                if (UC_gloBillingTransactionLines.GetLinesCount() - 1 > _NoOfMaxServiceLines && _cntr > _NoOfMaxDiagnosis)
                {
                    MessageBox.Show(" Claim has a max limit of " + _NoOfMaxServiceLines + " service lines and " + _NoOfMaxDiagnosis + " diagnosis. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (UC_gloBillingTransactionLines.GetLinesCount() - 1 > _NoOfMaxServiceLines)
                {
                    MessageBox.Show(" Claim has a max limit of " + _NoOfMaxServiceLines + " service lines. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (_cntr > _NoOfMaxDiagnosis)
                {
                    MessageBox.Show(" Claim has a max limit of " + _NoOfMaxDiagnosis + " diagnosis. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //  _Transaction.NoOfServiceLine = _NoOfMaxServiceLines;
                //  _Transaction.NoOfDiagnosis = _NoOfMaxDiagnosis;
                UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;              
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (ogloBilling != null)
                { ogloBilling.Dispose(); }
            }
        }

        private void SetWC(int type, string Claimno)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            oDB.Connect(false);
            try
            {
                DataTable dt = null;
                string _sqlQuery = "SELECT  top 1  dbo.CONVERT_TO_DATE(dtValidFrom) as Date, ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote FROM Patient_WorkersComp WITH (NOLOCK) WHERE sClaimno = '" + Claimno + "'  AND nPatientId = " + _PatientID + " AND nType = " + type + " ";

                oDB.Retrive_Query(_sqlQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    _isTextBoxLoading = true;
                    txt_WcAc.Text = Convert.ToString(dt.Rows[0]["ClaimNo"]);

                    txt_WcAc.Tag = Convert.ToString(dt.Rows[0]["sNote"]);
                    _isTextBoxLoading = false;
                    if (type == 1)
                    {
                        mskInjuryDate.Text = Convert.ToString(dt.Rows[0]["Date"]);
                    }
                    if (type == 2)
                    {
                        mskAccidentDate.Text = Convert.ToString(dt.Rows[0]["Date"]);
                    }

                }
                else
                {
                    _isTextBoxLoading = true;
                    txt_WcAc.Text = "";
                    Object _objNullTag =  "gloPM#Enteredclaim#isnotpresent#gloPM";
                    txt_WcAc.Tag = _objNullTag.GetHashCode();
                    _isTextBoxLoading = false;
                    mskAccidentDate.Text = "";

                    mskInjuryDate.Text = "";

                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                oDB.Disconnect();

            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        private bool IsDxSelected()
        {
            bool _result = false;
            try
            {
                for (int i = 1; i < c1Dx.Rows.Count; i++)
                {
                    if (c1Dx.GetCellCheck(i, COL_DX_SELECT) == CheckEnum.Checked)
                    { _result = true; break; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            return _result;
        }

        private ArrayList GetSelectedDx()
        {
            ArrayList _dxlist = new ArrayList();
            for (int i = 1; i < c1Dx.Rows.Count; i++)
            {
                if (c1Dx.GetCellCheck(i, COL_DX_SELECT) == CheckEnum.Checked)
                {
                    _dxlist.Add(Convert.ToString(c1Dx.GetData(i, COL_DX_CODE)).Trim());
                }
            }
            return _dxlist;
        }

        public void AddDxToList(string DxCode, string DxDesc, bool IsDeleted)
        {
            bool _IsExistItem = false;
            string _dxcode = "";
            string _dxdesc = "";

            try
            {
                if (IsDeleted == false)
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
                                {
                                    _IsExistItem = true;

                                    if (c1Dx.GetCellCheck(i, COL_DX_SELECT) == CheckEnum.Unchecked)
                                    {
                                        c1Dx.SetData(i, COL_DX_SELECT, true);
                                    }

                                    break;



                                }
                            }
                        }
                        if (_IsExistItem == false && DxCode.Trim() != "")
                        {
                            c1Dx.Rows.Add();
                            int rowIndex = c1Dx.Rows.Count - 1;
                            c1Dx.SetData(rowIndex, COL_DX_CODE, DxCode);
                            c1Dx.SetData(rowIndex, COL_DX_DESC, DxDesc);

                            //if (rowIndex <= 8)
                            if (rowIndex <= _NoOfMaxDiagnosis)
                            { c1Dx.SetData(rowIndex, COL_DX_SELECT, true); }
                            else
                            {
                                int cnt = 0;
                                for (int i = c1Dx.Rows.Count - 1; i > 0; i--)
                                {
                                    if (c1Dx.GetCellCheck(i, COL_DX_SELECT) == CheckEnum.Checked)
                                    {
                                        cnt = cnt + 1;
                                    }
                                }
                                //if (cnt < 8)
                                if (cnt < _NoOfMaxDiagnosis)
                                {
                                    c1Dx.SetData(rowIndex, COL_DX_SELECT, true);
                                }
                            }

                        }

                        //////**Code added on 20090528 by - Sagar Ghodke
                        //////**Code added to select the first Dx as the Line Primary Diagnosis

                        ////if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0
                        ////    && UC_gloBillingTransactionLines.HasPrimaryDx(UC_gloBillingTransactionLines.CurrentTransactionLine) == false)
                        ////{

                        ////    //c1Dx.SetData(c1Dx.Rows.Count - 1, COL_DX_ISPRIMARY, true);
                        ////}

                        //////**End code add 20090528,Sagar Ghodke


                    }
                }
                else
                {
                    for (int i = c1Dx.Rows.Count - 1; i > 0; i--)
                    {
                        _dxcode = "";
                        _dxdesc = "";

                        _dxcode = Convert.ToString(c1Dx.GetData(i, COL_DX_CODE));
                        _dxdesc = Convert.ToString(c1Dx.GetData(i, COL_DX_DESC));
                        if (_dxcode.ToUpper().Trim() == DxCode.ToUpper().Trim())
                        { c1Dx.Rows.Remove(i); break; }
                    }
                }

                //if (_IsOpenForModify == false && _IsOpenForExternal == false && _OpenViewMode == false)
                //{
                //    if (c1Dx != null && c1Dx.Rows.Count > 1)
                //    { c1Dx.SetData(1, COL_DX_ISPRIMARY, true); }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        public void SetInsuranceParty()
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            Transaction _Transaction = null;
            try
            {
                _Transaction = ogloBilling.GetChargesDetails(_TransactionID, _ClinicID);


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
                            if (Convert.ToInt64(c1Insurance.GetData(j, COL_INSURANCEID)) == _Transaction.InsurancePlans[i].InsuranceID)
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
                            c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, "Inactive");
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
                #endregion " Insurance Plan "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

                ogloBilling.Dispose();
            }
        }

        //void UC_gloBillingTransactionLines_onCPTCharges_Load(object sender, FacilityType ChargesType)
        //{
        //    try
        //    {
        //        this.chkFacilityFeeSchedule.CheckedChanged -= new System.EventHandler(this.chkFacilityFeeSchedule_CheckedChanged);
        //        this.chkNonFacilityCharges.CheckedChanged -= new System.EventHandler(this.chkNonFacilityCharges_CheckedChanged);

        //        chkFacilityFeeSchedule.Checked = false;
        //        chkNonFacilityCharges.Checked = false;

        //        if (ChargesType == FacilityType.Facility)
        //        { chkFacilityFeeSchedule.Checked = true; }
        //        else if (ChargesType == FacilityType.NonFacility)
        //        { chkNonFacilityCharges.Checked = true; }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //    finally
        //    {
        //        this.chkFacilityFeeSchedule.CheckedChanged += new System.EventHandler(this.chkFacilityFeeSchedule_CheckedChanged);
        //        this.chkNonFacilityCharges.CheckedChanged += new System.EventHandler(this.chkNonFacilityCharges_CheckedChanged);
        //    }
        //}

        void UC_gloBillingTransactionLines_show_LineNotes(object sender, RowColEventArgs e, GeneralNotes Notes)
        {
            try
            {
                #region " Add Notes "

                GeneralNotes oNotes = null;

                if (e.Row > 0)
                {
                    oNotes = Notes;

                    //frmSetupTransactionNotes ofrmNotes = new frmSetupTransactionNotes(_DatabaseConnectionString, _ClinicID, _TransactionID, Convert.ToInt64(e.Row), oNotes);
                    //frmSetupTransactionNotes ofrmNotes = new frmSetupTransactionNotes(_DatabaseConnectionString, _ClinicID, _TransactionID,UC_gloBillingTransactionLines.TransactionDetailID,UC_gloBillingTransactionLines.TransactionLineNo,oNotes);
                    frmNotes ofrmNotes = new frmNotes(_DatabaseConnectionString, _ClinicID, _TransactionID, UC_gloBillingTransactionLines.TransactionDetailID, UC_gloBillingTransactionLines.TransactionLineNo, oNotes);
                    ofrmNotes.ShowDialog(this);
                    if (ofrmNotes.oDialogResult)
                    {
                        UC_gloBillingTransactionLines.SetNotes(ofrmNotes.oNotes);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Add, "Note added to trasanction line, Line No : " + UC_gloBillingTransactionLines.CurrentTransactionLine.ToString() + " of claim # :"+txtClaimNo.Text+"." ,PatientID , _TransactionID, _PatientPoviderID, ActivityOutCome.Success);
                    }
                    ofrmNotes.Dispose();

                    //..Check if any notes exists for the TransactionLine 
                    //if (UC_gloBillingTransactionLines.HasNote(UC_gloBillingTransactionLines.CurrentTransactionLine) == true)
                    //{ UC_gloBillingTransactionLines.SetEPSDTNotesNDCCodeFlag(UC_gloBillingTransactionLines.CurrentTransactionLine); }
                    //else
                    //{ 
                        UC_gloBillingTransactionLines.SetEPSDTNotesNDCCodeFlag(UC_gloBillingTransactionLines.CurrentTransactionLine);
                    //}

                }
                else
                {
                    //MessageBox.Show("No transaction line is selected.  ", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        void UC_gloBillingTransactionLines_onInsCPTDxMod_Changed(object sender, RowColEventArgs e, TrnCtrlColValChangeEventArg e2)
        {
            
            TransactionLines oLineTransactions = null;
      
            string sNDCCode = "";
            try
            {
                if (e2.oType == TransactionLineColumnType.Diagnosis)
                {
                    if (_bSmartTreatmentLoading || _bEMRTreatmentLoading || _bOnlineClaimPostingLoading)
                    {
                        AddDxToList(e2.code, e2.description, e2.isdeleted);
                    }
                    else
                    {
                        _bDxFlag = UC_gloBillingTransactionLines.bIsDxMsgShown;
                        AddDxToList(e2.code, e2.description, e2.isdeleted);
                        _bDxFlag = true;
                        UC_gloBillingTransactionLines.bIsDxMsgShown = false;

                        #region "To Remove the Dx that are not used in Service Lines "

                        if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                        {
                            oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();
                            StringBuilder sbDx = new StringBuilder();
                            gloCharges.RemoveUnusedDx(oLineTransactions, ref sbDx);
                            if (sbDx.Length > 0)
                            {
                                this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                                for (int k = c1Dx.Rows.Count - 1; k >= 1; k--)
                                {
                                    c1Dx.SetCellCheck(k, COL_DX_SELECT, CheckEnum.Unchecked);
                                }

                                for (int k = c1Dx.Rows.Count - 1; k >= 1; k--)
                                {
                                    if (!sbDx.ToString().Contains(Convert.ToString(c1Dx.GetData(k, COL_DX_CODE))))
                                    {
                                        c1Dx.Rows.Remove(k);
                                    }
                                }
                                for (int i = 1; i <= c1Dx.Rows.Count - 1; i++)
                                {

                                    if (i <= _NoOfMaxDiagnosis)
                                    { c1Dx.SetData(i, COL_DX_SELECT, true); }
                                }

                                this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                                sbDx.Clear();
                            }
                        }

                        #endregion
                    }

                    
                }
                else if (e2.oType == TransactionLineColumnType.CPT)
                {

                    oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();

                    if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                    {
                        if ((Convert.ToString(UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE)).Trim() != "") && (UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE) != null))
                        {
                            oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode = "";
                            oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCUnit = null;
                            oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCUnitCode = null;
                            oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCUnitDescription = null;
                            oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].Prescription = null;
                            oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].PrescriptionDescription = null;
                            UC_gloBillingTransactionLines.SetNDCFields(oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1]);
                            //if (oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode != "" && oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode != null)
                            //{
                            //    frmDrugBilling ofrmDrugBilling = new frmDrugBilling(_DatabaseConnectionString, oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1]);
                            //    ofrmDrugBilling.ShowDialog(this);

                            //    if (ofrmDrugBilling.oDialogResult)
                            //    {
                            //        oLineTransaction = ofrmDrugBilling._oTransLine;
                            //        UC_gloBillingTransactionLines.SetNDCFields(oLineTransaction);
                            //        oLineTransaction.Dispose();
                            //        oLineTransactions.Dispose();
                            //    }
                            //    ofrmDrugBilling._oTransLine.Dispose();
                            //    ofrmDrugBilling.Dispose();
                            //}
                            //else if (IsCPTDrugChecked(e2.code, ref sNDCCode))
                            if (gloCharges.GetDefaultNDCForCPT(e2.code, ref sNDCCode))
                            {
                                oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode = sNDCCode;
                                frmDrugBilling ofrmDrugBilling = new frmDrugBilling(_DatabaseConnectionString, oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1],true);
                                ofrmDrugBilling.ShowDialog(this);

                                if (ofrmDrugBilling.oDialogResult)
                                {
                                    TransactionLine oLineTransaction = null;
                                    oLineTransaction = ofrmDrugBilling._oTransLine;
                                    UC_gloBillingTransactionLines.SetNDCFields(oLineTransaction);
                                  //  oLineTransaction.Dispose();
                                   // oLineTransactions.Dispose();
                                }
                                ofrmDrugBilling._oTransLine.Dispose();
                                ofrmDrugBilling.Dispose();
                            }

                            if (EPSDTEnabled && gloCharges.GetPromptForEPSDT(e2.code))
                            {
                                frmSetupEPSDTFamilyPlanning ofrmEPSDTFamilyPlanning = null;
                                ofrmEPSDTFamilyPlanning = new frmSetupEPSDTFamilyPlanning(oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1], ClaimEPSDT);
                                ofrmEPSDTFamilyPlanning.ShowDialog(this);

                                if (ofrmEPSDTFamilyPlanning.DialogResults)
                                {
                                    TransactionLine oLineTransaction = null;
                                    oLineTransaction = ofrmEPSDTFamilyPlanning.LineObject;
                                    ClaimEPSDT = ofrmEPSDTFamilyPlanning.ClaimEPSDT;

                                    UC_gloBillingTransactionLines.SetEPSDTFields(oLineTransaction);
                                 //   if (oLineTransaction != null) { oLineTransaction.Dispose(); }
                                 //   if (oLineTransactions != null) { oLineTransactions.Dispose(); }
                                }
                                ofrmEPSDTFamilyPlanning.Dispose();
                                ofrmEPSDTFamilyPlanning = null;
                              
                            }

                            if (bIsAnesthesiaEnabled && gloCharges.GetPromptForAnesthesia(e2.code))
                            {
                                frmAnesthesiaBilling ofrmAnesthesiaBilling = new frmAnesthesiaBilling(oLineTransactions[UC_gloBillingTransactionLines.CurrentTransactionLine - 1]);

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
                                    TransactionLine oLineTransaction = null;
                                    oLineTransaction = ofrmAnesthesiaBilling.oTransLine;
                                    UC_gloBillingTransactionLines.SetAnesthesiaFields(oLineTransaction);
                                   // if (oLineTransaction != null) { oLineTransaction.Dispose(); }
                                   // if (oLineTransactions != null) { oLineTransactions.Dispose(); }
                                }
                                ofrmAnesthesiaBilling.oTransLine.Dispose();
                                ofrmAnesthesiaBilling.Dispose();

                            }


                        }



                    }
                    #region "To Remove the Dx that are not used in Service Lines "

                    if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                    {
                        oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();
                        StringBuilder sbDx = new StringBuilder();
                        gloCharges.RemoveUnusedDx(oLineTransactions, ref sbDx);
                        if (sbDx.Length > 0)
                        {
                            this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                            for (int k = c1Dx.Rows.Count - 1; k >= 1; k--)
                            {
                                c1Dx.SetCellCheck(k, COL_DX_SELECT, CheckEnum.Unchecked);
                            }

                            for (int k = c1Dx.Rows.Count - 1; k >= 1; k--)
                            {
                                if (!sbDx.ToString().Contains(Convert.ToString(c1Dx.GetData(k, COL_DX_CODE))))
                                {
                                    c1Dx.Rows.Remove(k);
                                }
                            }
                            for (int i = 1; i <= c1Dx.Rows.Count - 1; i++)
                            {

                                if (i <= _NoOfMaxDiagnosis)
                                { c1Dx.SetData(i, COL_DX_SELECT, true); }
                            }

                            this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                            sbDx.Clear();
                        }
                    }

                    #endregion

                }

                if (oLineTransactions != null)
                {
                    oLineTransactions.Dispose();
                    oLineTransactions = null;
                }

                if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                {
                    TransactionLines oLineTransacts = null;
                    oLineTransacts = UC_gloBillingTransactionLines.GetLineTransactions();
                    if ((UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE) != null) && (UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE).ToString().Trim() != ""))
                    {
                        tls_btnMoreChargeData.Enabled = true;
                        tls_Anesthesia.Enabled = true;
                        if (oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode != "" && oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode != null)
                        {
                            UC_gloBillingTransactionLines.SetEPSDTNotesNDCCodeFlag(UC_gloBillingTransactionLines.CurrentTransactionLine);
                        }
                    }
                    else
                    {
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCCode = "";
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCUnit = null;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCUnitCode = null;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].NDCUnitDescription = null;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].Prescription = null;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].PrescriptionDescription = null;
                        Boolean bIsUnits = false;
                        if (oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].bIsAneshtesia)
                        {
                            bIsUnits = true;
                        }
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].bIsAneshtesia = false;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].AnesthesiaStartTime = null;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].AnesthesiaEndTime = null;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].AnesthesiaID = 0;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].bIsAutoCalculateAnesthesia =true;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].AnesthesiaTotalMinutes = 0;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].AnesthesiaMinPerUnit = 0;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].AnesthesiaTimeUnits = 0;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].AnesthesiaBaseUnits = 0;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].AnesthesiaOtherUnits = 0;
                        oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1].AnesthesiaTotalUnits = 0;
                        if (bIsUnits)
                        {
                            UC_gloBillingTransactionLines.SetAnesthesiaFields(oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1], bIsUnits);
                        }
                        else
                        {
                            UC_gloBillingTransactionLines.SetAnesthesiaFields(oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1], bIsUnits);
                        }

                        UC_gloBillingTransactionLines.SetNDCFields(oLineTransacts[UC_gloBillingTransactionLines.CurrentTransactionLine - 1]);
                        tls_btnMoreChargeData.Enabled = false;
                        tls_Anesthesia.Enabled = false;
                        //UC_gloBillingTransactionLines.SetNDCCodeFlag(UC_gloBillingTransactionLines.CurrentTransactionLine, false);
                        //UC_gloBillingTransactionLines.SetNDCFields(oLineTransaction);
                    }
                    oLineTransacts.Dispose();
                }
                else
                {
                    tls_btnMoreChargeData.Enabled = false;
                    tls_Anesthesia.Enabled = false;
                }

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



            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
             //   if (ofrmEPSDTFamilyPlanning != null) { ofrmEPSDTFamilyPlanning.Dispose(); ofrmEPSDTFamilyPlanning = null; }
             //   if (oLineTransaction != null) { oLineTransaction.Dispose(); oLineTransaction = null; }
                if (oLineTransactions != null) { oLineTransactions.Dispose(); oLineTransactions = null; }
            }
        }

        private bool ValidateMasterTransaction()
        {
            DataTable dtChargesBusCenter = null;
            DataTable dtAccBussinessCenter = null;
            DataTable dtBussinessCenterByRules = null;
            bool _Result = true;
            try
            {
                if (this.PatientHasCases && txtCases.Tag == null)
                { 
                    DialogResult _dlgInsurance = DialogResult.None;
                    DataTable dtCases = null;
                    DataTable dtCaseDiag = null;
                    DataTable dtCasesIns = null;
                    string sMessage = string.Empty;
                    

                    gloCharges.getSingleValidCase((mskClaimDate.Text != string.Empty ? Convert.ToDateTime(mskClaimDate.Text) : DateTime.Now), _PatientID, out dtCases, out dtCaseDiag, out dtCasesIns);
                                       
                    if (dtCases.Rows.Count > 1)
                    {
                        sMessage = "Patient has multiple Cases but none were selected." + Environment.NewLine + "Continue?";
                    }
                    else
                    {
                        sMessage = "No Case selected. Continue?";
                    }
                    _dlgInsurance = MessageBox.Show(sMessage, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (_dlgInsurance == DialogResult.No)
                    {
                        btnAdd_Cases.Focus();
                        _Result = false;
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
                        return _Result;
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
               
                if (cmbFacility.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a facility.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbFacility.Focus();
                    _Result = false;
                }
                else if (cmbBillingProvider.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a billing provider.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbBillingProvider.Focus();
                    _Result = false;

                }
                else if (cmbState.Enabled == true)
                {
                    if (cmbState.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a state.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbState.Focus();
                        _Result = false;
                        return false;

                    }
                }
                else if (chkOutSideLab.Checked == true)
                {
                    if (txtOutSideLabCharges.Text.Trim() == ".")
                    {
                        MessageBox.Show("Please enter the Outside Lab Charges.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOutSideLabCharges.Focus();
                        txtOutSideLabCharges.Select();
                        _Result = false;

                    }

                    if (chkOutSideLab.Checked == true && (txtOutSideLabCharges.Text.Trim() == "" && Convert.ToDecimal(txtOutSideLabCharges.Text.Trim()) <= 0))
                    {
                        MessageBox.Show("Please enter the OutSide Lab Charges.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOutSideLabCharges.Focus();
                        txtOutSideLabCharges.SelectAll();
                        _Result = false;
                    }
                }
                else if (mskClaimDate.MaskCompleted == false)
                {
                    MessageBox.Show("Please enter the close date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskClaimDate.Focus();
                    mskClaimDate.Select();
                    _Result = false;
                }

                gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, "");
                if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskClaimDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskClaimDate.Select(); mskClaimDate.Focus();
                    _Result = false;
                }
                ogloBilling.Dispose();
                int _addDays = 0;
                _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
                if (Convert.ToDateTime(mskClaimDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                {
                    MessageBox.Show("Close date " + mskClaimDate.Text.Trim() + " is too far in the future.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskClaimDate.Focus();
                    mskClaimDate.Select();
                    _Result = false;
                }
                else
                {
                    if (Convert.ToDateTime(mskClaimDate.Text.Trim()).Date > DateTime.Now.Date)
                    {
                        DialogResult _dlgCloseDate = DialogResult.None;
                        _dlgCloseDate = MessageBox.Show("Close date " + mskClaimDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (_dlgCloseDate == DialogResult.Cancel)
                        {
                            mskClaimDate.Focus();
                            mskClaimDate.Select();
                            _Result = false;
                        }
                    }
                }
              

                //if (cmbCloseDayTray == null || cmbCloseDayTray.Items.Count <= 0 || cmbCloseDayTray.SelectedIndex == -1 || cmbCloseDayTray.Text.Trim() == "")
                if(SelectedChargeTrayID == 0)
                {
                    MessageBox.Show("Please select charge tray. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSelectChargeTry.Focus();
                    _Result = false;
                }
                else
                {

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                    oDB.Connect(false);
                    object _retVal = null;
                    String _sqlQuery = "SELECT nChargeTrayID FROM BL_ChargesTray WITH (NOLOCK) where nChargeTrayID = " + SelectedChargeTrayID + " AND ISNULL(bIsActive,0)=1 AND nClinicID = " + _ClinicID + "";
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_retVal == null || _retVal.ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Selected charge tray is inactive. Please select the another charge tray.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    oDB.Disconnect();
                    oDB.Dispose();
                }


                String sMessageReferral = String.Empty;
                DialogResult _dlgReferral = DialogResult.None;

                if (cmbReferralProvider.Items.Count > 1 && chk_SameasBillingProvider.Checked == false && _Result)
                {

                    if (cmbReferralProvider.SelectedIndex == 0 && cmbReferralProvider.Text.Trim() == "")
                    {
                        if (_bIsRefprovAsSupervisor)
                        {
                            sMessageReferral = "Supervising provider information may not be reported since no referring provider is selected on claim. Continue?";
                        }
                        else if (cmbReferralProvider.Items.Count > 2)
                        {
                            sMessageReferral = "Patient has multiple Referring Providers but none were selected." + Environment.NewLine + "Continue?";
                        }
                        else
                        {
                            sMessageReferral = "No Referring provider selected. Continue?";
                        }

                        _dlgReferral = MessageBox.Show(sMessageReferral, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (_dlgReferral == DialogResult.No)
                        {
                            cmbReferralProvider.Focus();
                            _Result = false;
                            return _Result;
                        }


                    }
                }
                else
                {
                    if (_bIsRefprovAsSupervisor && chk_SameasBillingProvider.Checked == false && _Result)
                    {
                        sMessageReferral = "Supervising provider information may not be reported since no referring provider is selected on claim. Continue?";
                        _dlgReferral = MessageBox.Show(sMessageReferral, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (_dlgReferral == DialogResult.No)
                        {
                            cmbReferralProvider.Focus();
                            _Result = false;
                            return _Result;
                        }
                    }

                }

                //Validate For Insurance -- Should not have more than 3 insurances
                if (c1Insurance.Rows.Count > 0)
                {
                    Int64 _PartyCount = 0;

                    for (int i = 1; i < c1Insurance.Rows.Count; i++)
                    {
                        if (c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY) != null && c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY).ToString().Trim().Length > 0)
                        {

                            Int16 _Party = 0;

                            if (Int16.TryParse(c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY).ToString(), out  _Party) == true)
                            {
                                if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE)) == InsuranceTypeFlag.Primary.ToString() || Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE)) == InsuranceTypeFlag.Secondary.ToString() || Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE)) == InsuranceTypeFlag.Tertiary.ToString() || Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE)) == "Inactive")
                                {
                                    if (_Party > 0)
                                    {
                                        _PartyCount++;
                                    }
                                }
                            }
                        }
                    }

                    if (_PartyCount > 3)
                    {


                        DialogResult _dlgInsurance = DialogResult.None;
                        _dlgInsurance = MessageBox.Show("Claim has " + _PartyCount + " Insurances. Claims may only have up to three Insurances. Claims with more Insurances will not bill.Continue? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (_dlgInsurance == DialogResult.No)
                        {
                            _Result = false;
                        }
                    }
                }

                if (_nResponsibleParty != 0)
                {
                    if (!ValidateInsuranceCoverage())
                        return false;
                }

                if (!SavePAValidation())
                {
                    return false;
                }

                #region "Business Center Validation "

                dtChargesBusCenter = gloCharges.GetChargeBusinessCenterSettings();
                if (dtChargesBusCenter != null && dtChargesBusCenter.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtChargesBusCenter.Rows[0]["businesscenter_feature"]) == true && Convert.ToBoolean(dtChargesBusCenter.Rows[0]["BusinessCenter_PatientAccount"]) == true)
                    {
                        dtAccBussinessCenter = gloCharges.GetPatientAccountBusinessCenter(nPAccountID);
                        Int64 nAccBusinessCenterID = 0;
                        string nAccBusinessCenterCode = "";
                        if (dtAccBussinessCenter != null && dtAccBussinessCenter.Rows.Count > 0)
                        {
                            nAccBusinessCenterID = Convert.ToInt64(dtAccBussinessCenter.Rows[0]["nBusinessCenterID"]);
                            nAccBusinessCenterCode = Convert.ToString(dtAccBussinessCenter.Rows[0]["sBusinessCenterCode"]);
                        }
                        if (nAccBusinessCenterID == 0)
                        {
                            if (Convert.ToBoolean(dtChargesBusCenter.Rows[0]["BusinessCenter_PatientAccount"]) == true)
                            {
                                MessageBox.Show("Patient Account has not been assigned to a Business Center.\nPlease assign a Business Center in Patient Demographics before saving new charges.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                        }
                        dtBussinessCenterByRules = gloCharges.GetBusinessCenterByRules(Convert.ToInt64(cmbBillingProvider.SelectedValue), Convert.ToInt64(cmbFacility.SelectedValue));
                        if (dtBussinessCenterByRules != null & dtBussinessCenterByRules.Rows.Count > 0)
                        {
                            if (nAccBusinessCenterID != Convert.ToInt64(dtBussinessCenterByRules.Rows[0]["nBusinessCenterID"]))
                            {
                                if (Convert.ToString(dtChargesBusCenter.Rows[0]["chargeEntryMismatch"]).ToLower() == "warn")
                                {
                                    DialogResult _dlgBusinessCenter = DialogResult.None;
                                    _dlgBusinessCenter = MessageBox.Show("Warning : Charge Business Center is " + dtBussinessCenterByRules.Rows[0]["sBusinessCenterCode"] + " but Account Business Center is " + nAccBusinessCenterCode + ".  \nContinue Save?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                    if (_dlgBusinessCenter == DialogResult.No)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DialogResult _dlgChargeBusinessCenter = DialogResult.None;
                            _dlgChargeBusinessCenter = MessageBox.Show("The system could not verify this claim has been posted to the correct business center.   Administration Business Center rules do not mention this claim’s provider or facility.", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (_dlgChargeBusinessCenter == DialogResult.Cancel)
                            {
                                return false;
                            }
                        }
                    }
                  
                }

                #endregion

                if (ReplacementClaimCreationType == ReplacementClaimCreationType.Replacement)
                {
                    if (gloBilling.CheckForPaymentAgaistClaim(_MasterTransactionID))
                    {

                        MessageBox.Show("New Claim " + txtClaimNo.Text + " has replaced voided claim " + lblReplacementClaimNo.Text + ".\n\nHowever there were financial transactions on the voided claim that have not been applied to the new claim.\n\nPlease review.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                if (_Result)
                {
                    if (bIsDublicteCPTsWarningEnable == true)
                    {
                        TransactionLines oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();

                        if (oLineTransactions != null && oLineTransactions.Count > 0)
                        {
                            List<string> sCPTCodes = new List<string>();
                            for (int i = 0; i < oLineTransactions.Count; i++)
                            {
                                sCPTCodes.Add(Convert.ToString(oLineTransactions[i].CPTCode));
                            }

                            List<string> sDuplicateCPTs = sCPTCodes.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                            if (sDuplicateCPTs.Count > 0)
                            {
                                if ((MessageBox.Show("Procedure: Duplicate procedure code entered \nStop to review?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)) == DialogResult.Yes)
                                {
                                    return false;
                                }
                            }
                            sCPTCodes = null;
                            sDuplicateCPTs = null;

                            for (int i = 0; i < oLineTransactions.Count; i++)
                            {
                                List<string> sDxCodes = new List<string>();
                                sDxCodes.Add(Convert.ToString(oLineTransactions[i].Dx1Code) != "" ? Convert.ToString(oLineTransactions[i].Dx1Code) : "1");
                                sDxCodes.Add(Convert.ToString(oLineTransactions[i].Dx2Code) != "" ? Convert.ToString(oLineTransactions[i].Dx2Code) : "2");
                                sDxCodes.Add(Convert.ToString(oLineTransactions[i].Dx3Code) != "" ? Convert.ToString(oLineTransactions[i].Dx3Code) : "3");
                                sDxCodes.Add(Convert.ToString(oLineTransactions[i].Dx4Code) != "" ? Convert.ToString(oLineTransactions[i].Dx4Code) : "4");
                                string sCurrentCPT = Convert.ToString(oLineTransactions[i].CPTCode);
                                List<string> sDuplicateDxCode = sDxCodes.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                                if (sDuplicateDxCode.Count > 0)
                                {
                                    MessageBox.Show("Diagnosis: Same diagnosis entered twice for the procedure code : \"" + sCurrentCPT + "\".\nStop to Review. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                                sDxCodes = null;
                                sDuplicateDxCode = null;
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Result = false;
            }
            finally
            {
                if (dtAccBussinessCenter != null)
                {
                    dtAccBussinessCenter.Dispose();
                }
                if (dtBussinessCenterByRules != null)
                {
                    dtBussinessCenterByRules.Dispose();
                }
                if (dtChargesBusCenter != null)
                {
                    dtChargesBusCenter.Dispose();
                }
            }
            return _Result;
        }

        private bool SavePAValidation()
        {
            try
            {
                if (txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                {

                    Int64 _paID = Convert.ToInt64(txtPriorAuthorizationNo.Tag);
                    
                    Int64 _visitsAllowed = 0;
                    Int64 _visitsUsed = 0;
                    Int64 _visitsUsedTotal = 0;
                    Int64 _expDate = 0;
                    Int64 _startDate = 0;
                    Int64 _endDate = 0;
                    bool _trackLimit = false;
                    Int64 _VisitsCount = 0;
                    Int64 _tmp = 0;
                    DataRow _drPAInfo = gloPMGeneral.gloPriorAuthorization.clsgloPriorAuthorization.GetPriorAuthorizationInfo(_paID);

                    if (_drPAInfo != null)
                    {
                        if (Convert.ToString(_drPAInfo["nAuthorizationType"]).Trim() != "2")
                        {
                            #region "Grid Unique Dates"

                            TransactionLines oTransactionLines = UC_gloBillingTransactionLines.GetLineTransactions();
                            DataTable _dtDates = null;
                            _dtDates = gloPMGeneral.gloPriorAuthorization.clsgloPriorAuthorization.GetUniqueDates(_paID, UC_gloBillingTransactionLines.GetLastMaxDOS());
                            if (_dtDates != null && _dtDates.Rows.Count > 0 && oTransactionLines != null)
                            {

                                for (int _GridRow = 0; _GridRow < oTransactionLines.Count; _GridRow++)
                                {
                                    _dtDates.DefaultView.RowFilter = "DateUsed=" + gloDateMaster.gloDate.DateAsNumber(oTransactionLines[_GridRow].DateServiceFrom.ToShortDateString());
                                    if (_dtDates.DefaultView.Count.Equals(0))
                                    {
                                        DataRow rw = _dtDates.NewRow();
                                        if (Convert.ToString(oTransactionLines[_GridRow].DateServiceFrom).Trim() != "")
                                        {
                                            rw["DateUsed"] = gloDateMaster.gloDate.DateAsNumber(oTransactionLines[_GridRow].DateServiceFrom.ToShortDateString());
                                            _dtDates.Rows.Add(rw);
                                            _dtDates.AcceptChanges();
                                            _VisitsCount++;
                                        }
                                        rw = null;
                                    }
                                    else
                                    {
                                       
                                    }
                                    _dtDates.DefaultView.RowFilter = "";

                                }

                            }
                            if (_dtDates != null && _dtDates.Rows.Count > 0)
                            { //Entry in Database.
                                _visitsUsedTotal = _dtDates.Rows.Count;
                                //_visitsUsedTotal = _VisitsCount;
                                //_tmp = 1;
                                _tmp = 0;
                            }
                            else if (oTransactionLines != null && (oTransactionLines.Count > 0))
                            { //If no entry in Database.
                                DataTable _dtLines = new DataTable();
                                _dtLines.Columns.Add("DateUsed");
                                for (int _GridRow = 0; _GridRow < oTransactionLines.Count; _GridRow++)
                                {
                                    _dtLines.DefaultView.RowFilter = "DateUsed=" + gloDateMaster.gloDate.DateAsNumber(oTransactionLines[_GridRow].DateServiceFrom.ToShortDateString());
                                    if (_dtLines.DefaultView.Count.Equals(0))
                                    {
                                        DataRow rw = _dtLines.NewRow();
                                        if (Convert.ToString(oTransactionLines[_GridRow].DateServiceFrom).Trim() != "")
                                        {
                                            rw["DateUsed"] = gloDateMaster.gloDate.DateAsNumber(oTransactionLines[_GridRow].DateServiceFrom.ToShortDateString());
                                            _dtLines.Rows.Add(rw);
                                            _dtLines.AcceptChanges();
                                        }
                                        rw = null;
                                    }
                                    _dtLines.DefaultView.RowFilter = "";

                                }
                                if (_dtLines != null && _dtLines.Rows.Count > 0)
                                {
                                    _visitsUsedTotal = _dtLines.Rows.Count;
                                    _VisitsCount = _dtLines.Rows.Count;
                                    _tmp = 0;
                                }
                                _dtLines.Dispose();
                                _dtLines = null;
                            }
                            if (_dtDates != null)
                            {
                                _dtDates.Dispose();
                                _dtDates = null;
                            }
                            if (oTransactionLines != null)
                            {
                                oTransactionLines.Dispose();
                                oTransactionLines = null;
                            }
                            #endregion
                            if (_drPAInfo != null)
                            {
                                _trackLimit = Convert.ToBoolean(_drPAInfo["bIsTrackAuthLimit"]);
                                if (_trackLimit)
                                {
                                    if (c1Insurance.Rows.Count > 1)
                                    {
                                        if (c1Insurance.GetData(1, COL_INSURANCENAME).ToString() == PayerMode.Self.ToString())
                                        {

                                            if (MessageBox.Show("Mismatch – Claim has no insurance but has a prior authorization.\nContinue? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                            {
                                                
                                                return false; 
                                            }
                                         
                                        }
                                    }
                                }
                                if (_trackLimit)
                                {
                                    //If Prior Auth is Limit tracking and Prior Authorization does not have a selected Patient Insurance 
                                    //Claim has a primary insurance then do not allow the save
                                    if (Convert.ToInt64(_drPAInfo["InsuranceID"].ToString()) == 0)
                                    {
                                                  
                                    if (c1Insurance.Rows.Count > 1)
                                    {
                                        
                                            if (c1Insurance.GetData(1, COL_INSURANCENAME).ToString() != PayerMode.Self.ToString())
                                            {
                                                 MessageBox.Show("Prior authorization " + _drPAInfo["sPriorAuthorizationNo"] + " requires a patient’s insurance.\nPlease review the prior authorization before saving charges.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    return false;
                                                }
                                        }
                                    }
                                    
                                
                                    if (c1Insurance.Rows.Count > 1)
                                    {
                                        bool _isAuthInMatching = false;
                                        for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                                        {
                                            if (c1Insurance.GetData(i, COL_INSURANCENAME).ToString() != PayerMode.Self.ToString())
                                            {
                                                if (c1Insurance.GetData(i, COL_INSURANCEID).ToString() == _drPAInfo["InsuranceID"].ToString())
                                                {
                                                    if (Convert.ToInt64(_drPAInfo["InsuranceID"].ToString()) != 0)
                                                    {
                                                        _isAuthInMatching = true;
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        if (_isAuthInMatching == false)
                                        {
                                            MessageBox.Show("Mismatch – Claim’s insurance and prior authorization’s insurance do not match.\nPlease review.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return false;
                                        }
                                    }
                                }
                            }

                            if (_drPAInfo != null)
                            {
                                _visitsAllowed = Convert.ToInt64(_drPAInfo["nVisitsAllowed"]);
                                _trackLimit = Convert.ToBoolean(_drPAInfo["bIsTrackAuthLimit"]);
                                if (_drPAInfo["nExpDate"] == DBNull.Value)
                                    _expDate = 0;
                                else
                                    _expDate = Convert.ToInt64(_drPAInfo["nExpDate"]);
                                //_visitsUsedTotal = Convert.ToInt64(_drPAInfo["nExpDate"]);
                            }
                            _startDate = (UC_gloBillingTransactionLines.GetLastMaxDOS());
                            _endDate = (UC_gloBillingTransactionLines.GetLastMaxDOS());

                            //if (gloPMGeneral.gloPriorAuthorization.clsgloPriorAuthorization.GetVisitsUsed(_paID, _startDate, _endDate) <= 0)
                            //{ _visitsRequired += 1; }
                            _visitsUsed = gloPMGeneral.gloPriorAuthorization.clsgloPriorAuthorization.GetVisitsUsed(_paID);
                            //_visitsRemaining = gloPMGeneral.gloPriorAuthorization.clsgloPriorAuthorization.GetVisitsRemaining(_paID, _endDate, false) - _visitsRequired;
                            //_visitsUsedTotal = _visitsUsed + 1;
                            if (_expDate != 0 && _expDate < _endDate)
                            {
                                if (MessageBox.Show("Prior authorization " + Convert.ToString(txtPriorAuthorizationNo.Text) + " has expired.\nPrior authorization is valid only until " + gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_drPAInfo["nExpDate"])) + ".\nContinue? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                { return false; }
                            }
                            if (_trackLimit)
                            {
                                if (_visitsAllowed != 0)
                                {
                                    if ((_visitsAllowed - _visitsUsedTotal - _tmp) < 0)
                                    {
                                        if (MessageBox.Show("Prior authorization " + Convert.ToString(txtPriorAuthorizationNo.Text) + " has exceeded its # visits allowed.\nContinue? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        { return false; }
                                    }
                                    else
                                    {
                                        string _visit = "visit";
                                        if (_VisitsCount > 1) { _visit = _visit + "s"; }

                                        if (!_visitsAllowed.Equals(0) && _VisitsCount != 0)
                                        {
                                            if (MessageBox.Show("Prior authorization " + Convert.ToString(txtPriorAuthorizationNo.Text) + " will use "
                                                + _VisitsCount + " " + _visit + ".\nContinue? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                            { return false; }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            { }
            return true;
        }

        private bool IsPrimaryDxSelected()
        {
            bool _isPrimaryDxSelected = false;
            try
            {
                if (c1Dx != null && c1Dx.Rows.Count > 0)
                {
                    for (int rowIndex = 1; rowIndex < c1Dx.Rows.Count; rowIndex++)
                    {
                        if (c1Dx.GetCellCheck(rowIndex, COL_DX_ISPRIMARY) == CheckEnum.Checked)
                        { _isPrimaryDxSelected = true; break; }
                    }
                }
            }
            catch (Exception)
            {
            }
            return _isPrimaryDxSelected;
        }

        private bool ValidateInsurance()
        {
            //int _insrowindex = 0;
            bool _insresult = true;
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            try
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

                //_insrowindex = UC_gloBillingTransactionLines.CurrentTransactionLine;
                //Added by Subashish_b on 03/Feb /2011 (integration made on date-10/May/2011) for  validating whether any patient selected or not while saving a charge
                if (_IsPatientAccountFeature)
                {
                    if (oPatientControl.CmbSelectedPatientID == 0)
                    {
                        MessageBox.Show("Select patient for the claim.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // oPatientControl.SelectSearchBox();
                        _insresult = false;
                        return _insresult;
                    }
                }
                //End 
                if (this._PatientID <= 0)
                {
                    MessageBox.Show("Please select Patient for the claim. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Modified by Subashish_b on 03/Feb /2011 (integration made on date-10/May/2011) for  Code commenting  used before in old patient banner
                    // oPatientControl.SelectSearchBox();
                    //End
                    _insresult = false;
                    return _insresult;
                }
                if (oPatientControl != null && oPatientControl.PatientCode.Trim() == "")
                {
                    MessageBox.Show("Please select Patient for the claim. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Modified by Subashish_b on 03/Feb /2011 (integration made on date-10/May/2011) for  Code commenting  used before in old patient banner
                    // oPatientControl.SelectSearchBox();
                    //End
                    _insresult = false;
                    return _insresult;
                }
                //if (UC_gloBillingTransactionLines.ValidateInsurance(out _insrowindex) == false)
                //{
                //    MessageBox.Show("Please select an insurance carrier for the transaction in line. " + _insrowindex.ToString() + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    _insresult = false;
                //    return _insresult;
                //}

                mskClaimDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskClaimDate.Text;
                mskClaimDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (strDate.Length > 0)
                {
                    if (IsValidDate(mskClaimDate.Text) == false)
                    {
                        MessageBox.Show("Please enter a valid date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskClaimDate.Focus();
                        mskClaimDate.Select();
                        _insresult = false;
                        return _insresult;
                    }
                    else
                    {
                      

                        if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskClaimDate.Text)))
                        {
                            MessageBox.Show("Day is already closed. You cannot perform any transaction for this close date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskClaimDate.Focus();
                            mskClaimDate.Select();
                            _insresult = false;
                            return _insresult;
                        }
                        

                    }
                    
                }
                else
                {
                    MessageBox.Show("Please enter close date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskClaimDate.Focus();
                    mskClaimDate.Select();
                    _insresult = false;
                    return _insresult;
                }

                DateTime _OldestServiceDate = UC_gloBillingTransactionLines.GetOldestSeriveDate();

                if (_OldestServiceDate != null && _OldestServiceDate != DateTime.MinValue)
                {
                    if (CmbAccidentType.Text.Trim() != "Work" && CmbAccidentType.Text.Trim() != "Auto")
                    {
                        mskOnsiteDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mskOnsiteDate.Text != "")
                        {
                            mskOnsiteDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            if (IsValidDate(mskOnsiteDate.Text))
                            {
                                if (Convert.ToDateTime(mskOnsiteDate.Text).Date > _OldestServiceDate.Date)
                                {
                                    MessageBox.Show("Claim date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    mskOnsiteDate.Focus();
                                    mskOnsiteDate.Select();
                                    _insresult = false;
                                    return _insresult;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskOnsiteDate.Focus();
                                mskOnsiteDate.Select();
                                _insresult = false;
                                return _insresult;
                            }
                        }
                    }
                    if (CmbAccidentType.Text.Trim() == "Auto" && CmbAccidentType.Text.Trim() != "Work")
                    {
                       
                        mskAccidentDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mskAccidentDate.Text != "")
                        {
                            mskAccidentDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                            if (IsValidDate(mskAccidentDate.Text))
                            {
                                if (Convert.ToDateTime(mskAccidentDate.Text).Date > _OldestServiceDate.Date)
                                {
                                    MessageBox.Show("Accident date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    mskAccidentDate.Focus();
                                    mskAccidentDate.Select();
                                    _insresult = false;
                                    return _insresult;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskAccidentDate.Focus();
                                mskAccidentDate.Select();
                                _insresult = false;
                                return _insresult;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter Accident date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskInjuryDate.Focus();
                            mskInjuryDate.Select();
                           _insresult = false;
                            return _insresult;

                        }
                    }
                    if (CmbAccidentType.Text.Trim() == "Work" && CmbAccidentType.Text.Trim() != "Auto")
                    {
                       

                        mskInjuryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mskInjuryDate.Text != "")
                        {
                            mskInjuryDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            if (IsValidDate(mskInjuryDate.Text))
                            {
                                if (Convert.ToDateTime(mskInjuryDate.Text).Date > _OldestServiceDate.Date)
                                {
                                    MessageBox.Show("Injury date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    mskInjuryDate.Focus();
                                    mskInjuryDate.Select();
                                    _insresult = false;
                                    return _insresult;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskInjuryDate.Focus();
                                mskInjuryDate.Select();
                                _insresult = false;
                                return _insresult;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter Injury date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskInjuryDate.Focus();
                            mskInjuryDate.Select();
                            _insresult = false;
                            return _insresult;

                        }
                    }
                    if (CmbAccidentType.Text.Trim() == "Other")
                    {
                        mskOtherDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mskOtherDate.Text != "")
                        {
                            mskOtherDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            if (IsValidDate(mskOtherDate.Text))
                            {
                                if (Convert.ToDateTime(mskOtherDate.Text).Date > _OldestServiceDate.Date)
                                {
                                    MessageBox.Show("Other Accident date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    mskOtherDate.Focus();
                                    mskOtherDate.Select();
                                    _insresult = false;
                                    return _insresult;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskOtherDate.Focus();
                                mskOtherDate.Select();
                                _insresult = false;
                                return _insresult;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter Other Accident date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskOtherDate.Focus();
                            mskOtherDate.Select();
                            _insresult = false;
                            return _insresult;

                        }
                    }

                    mskHospitaliztionFrom.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskHospitaliztionFrom.Text != "")
                    {
                        mskHospitaliztionFrom.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(mskHospitaliztionFrom.Text))
                        {
                            // Problem# 406 - Even if Hospitalization(from) date is greater then DOS, we are removing the restriction
                            //if (Convert.ToDateTime(mskHospitaliztionFrom.Text).Date > _OldestServiceDate.Date)
                            //{
                            //    MessageBox.Show("Hospitalization(from) date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    mskHospitaliztionFrom.Focus();
                            //    mskHospitaliztionFrom.Select();
                            //    _insresult = false;
                            //    return _insresult;
                            //}
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskHospitaliztionFrom.Focus();
                            mskHospitaliztionFrom.Select();
                            _insresult = false;
                            return _insresult;
                        }
                    }

                  

                    mskHospitaliztionTo.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskHospitaliztionTo.Text != "")
                    {
                        mskHospitaliztionTo.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(mskHospitaliztionTo.Text))
                        {
                            if (Convert.ToDateTime(mskHospitaliztionTo.Text).Date > _OldestServiceDate.Date)
                            {
                                //MessageBox.Show("Hospitalization(To) Date cannot be greater than Service Date", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //dtpHospitaliztionTo.Focus();
                                //dtpHospitaliztionTo.Select();
                                //_insresult = false;
                                //return _insresult;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskHospitaliztionTo.Focus();
                            mskHospitaliztionTo.Select();
                            _insresult = false;
                            return _insresult;
                        }
                    }


                    #region " Unable to Work From/Till Date "
                    //mskUnableFromDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    //if (mskUnableFromDate.Text != "")
                    //{
                    //    mskUnableFromDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    //    if (IsValidDate(mskUnableFromDate.Text))
                    //    {
                    //        if (Convert.ToDateTime(mskUnableFromDate.Text).Date > _OldestServiceDate.Date)
                    //        {
                    //            MessageBox.Show("Unable to work from date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            mskUnableFromDate.Focus();
                    //            mskUnableFromDate.Select();
                    //            _insresult = false;
                    //            return _insresult;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        mskUnableFromDate.Focus();
                    //        mskUnableFromDate.Select();
                    //        _insresult = false;
                    //        return _insresult;
                    //    }
                    //}

                    if (_UnableToWorkFromDate_MoreClaimData > 0 && gloDateMaster.gloDate.DateAsDate(_UnableToWorkFromDate_MoreClaimData) > _OldestServiceDate.Date)
                    {
                        MessageBox.Show("Unable to work from date cannot be greater than service date. Please review More Claim Data.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //mskUnableFromDate.Focus();
                        //mskUnableFromDate.Select();
                        _insresult = false;
                        return _insresult;
                    }


                    mskBox15Date.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskBox15Date.Text != "")
                    {
                        mskBox15Date.TextMaskFormat = MaskFormat.IncludeLiterals;

                        if (Convert.ToString(cmbBox15DateQualifier.SelectedValue) == "")
                        {
                            MessageBox.Show("Please select the Other Claim Date Qualifier.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbBox15DateQualifier.Focus();
                            cmbBox15DateQualifier.Select();
                            _insresult = false;
                            return _insresult;

                        }

                        if (IsValidDate(mskBox15Date.Text))
                        {


                            if (Convert.ToDateTime(mskBox15Date.Text).Date > _OldestServiceDate.Date)
                            {
                                switch (Convert.ToString(cmbBox15DateQualifier.SelectedValue))
                                {
                                    case "454":
                                        MessageBox.Show("Initial Treatment date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case "304":
                                        MessageBox.Show("Latest Visit or Consultation date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case "453":
                                        MessageBox.Show("Acute Manifestation of a Chronic Condition date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case "439":
                                        MessageBox.Show("Accident date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case "455":
                                        MessageBox.Show("Last X-ray date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case "471":
                                        MessageBox.Show("Prescription date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case "090":
                                        MessageBox.Show("Report Start (Assumed Care Date) date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case "091":
                                        MessageBox.Show("Report End (Relinquished Care Date) date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case "438":
                                        MessageBox.Show("Onset Same/Similar Illness Date date cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                     default:
                                        break;

                                }

                              
                                 
                                mskOtherDate.Focus();
                                mskOtherDate.Select();
                                _insresult = false;
                                return _insresult;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskOtherDate.Focus();
                            mskOtherDate.Select();
                            _insresult = false;
                            return _insresult;
                        }
                      
                    }
                    





                    //mskUnableTillDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    //if (mskUnableTillDate.Text != "")
                    //{
                    //    mskUnableTillDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    //    if (IsValidDate(mskUnableTillDate.Text))
                    //    {
                    //        if (Convert.ToDateTime(mskUnableTillDate.Text).Date > _OldestServiceDate.Date)
                    //        {
                    //            //MessageBox.Show("Unable to Work Till Date cannot be greater than Service Date", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            //dtpUnableTillDate.Focus(); 
                    //            //dtpUnableTillDate.Select();
                    //            //_insresult = false;
                    //            //return _insresult;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        mskUnableTillDate.Focus();
                    //        mskUnableTillDate.Select();
                    //        _insresult = false;
                    //        return _insresult;
                    //    }
                    //}
        
                                
                    #endregion " Unable to Work From/Till Date "

                    if (chkOutSideLab.Checked == true)
                    {
                        if (txtOutSideLabCharges.Text.Trim() == ".")
                        {
                            MessageBox.Show("Please enter the Outside Lab Charges.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtOutSideLabCharges.Focus();
                            txtOutSideLabCharges.Select();
                            _insresult = false;
                            return _insresult;
                        }

                        if (txtOutSideLabCharges.Text.Trim() == "" || Convert.ToDecimal(txtOutSideLabCharges.Text.Trim()) <= 0)
                        {
                            MessageBox.Show("Please enter the Outside Lab Charges.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtOutSideLabCharges.Focus();
                            txtOutSideLabCharges.Select();
                            _insresult = false;
                            return _insresult;
                        }
                    }

                }


                //if (IsValidDate(mskUnableFromDate.Text) && IsValidDate(mskUnableTillDate.Text))
                //{
                //    if (Convert.ToDateTime(mskUnableFromDate.Text).Date > Convert.ToDateTime(mskUnableTillDate.Text).Date)
                //    {
                //        MessageBox.Show("Unable to Work From Date cannot be greater than Unable to Work Till Date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        mskUnableFromDate.Focus();
                //        mskUnableFromDate.Select();
                //        _insresult = false;
                //        return _insresult;

                //    }
                //}

                if (_UnableToWorkFromDate_MoreClaimData > 0 && _UnableToWorkTillDate_MoreClaimData > 0)
                {
                    if (gloDateMaster.gloDate.DateAsDate(_UnableToWorkFromDate_MoreClaimData) > gloDateMaster.gloDate.DateAsDate(_UnableToWorkTillDate_MoreClaimData))
                    {
                        MessageBox.Show("Unable to Work From Date cannot be greater than Unable to Work Till Date. Please review More Claim Data", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //mskUnableFromDate.Focus();
                        //mskUnableFromDate.Select();
                        _insresult = false;
                        return _insresult;

                    }
                }


                if (IsValidDate(mskHospitaliztionFrom.Text) && IsValidDate(mskHospitaliztionTo.Text))
                {
                    if (Convert.ToDateTime(mskHospitaliztionFrom.Text).Date > Convert.ToDateTime(mskHospitaliztionTo.Text).Date)
                    {
                        MessageBox.Show("Hospitalization from Date cannot be greater than Hospitalization to Date.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskHospitaliztionFrom.Focus();
                        mskHospitaliztionFrom.Select();
                        _insresult = false;
                        return _insresult;

                    }
                }

                if (!UC_gloBillingTransactionLines.ValidateDx())
                {
                    _insresult = false;
                    return _insresult;
                }
                
                string _dxcode = "";
                string _dxdesc = "";
               
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
                                   // if (rbICD10.Checked)
                                    //{
                                    MessageBox.Show("ICD Type Mismatch", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                  // }
                                  // else
                                  //  {
                                  //  MessageBox.Show("ICD 9 codes cannot be mixed with ICD 10 codes.\nPlease remove ICD 10 codes before saving.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                  // }
                                    _insresult = false;
                                    return _insresult;

                                }

                            }


                        }
                    
                }
                
                //mskInitTreatment.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                //if (mskInitTreatment.Text != "")
                //{
                //    mskInitTreatment.TextMaskFormat = MaskFormat.IncludeLiterals;
                //    if (IsValidDate(mskInitTreatment.Text))
                //    {
                //        if (Convert.ToDateTime(mskInitTreatment.Text).Date > _OldestServiceDate.Date)
                //        {
                //            MessageBox.Show("Initial Date of Treatment cannot be greater than service date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            mskInitTreatment.Focus();
                //            mskInitTreatment.Select();
                //            _insresult = false;
                //            return _insresult;
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        mskInitTreatment.Focus();
                //        mskInitTreatment.Select();
                //        _insresult = false;
                //        return _insresult;
                //    }
                //}

                
                #region " Insurance responsibility validation "

                if (c1Insurance.Rows.Count > 1)
                {

                    Boolean _IsFirstPartySelected = false;
                    Int32 _CntInsurance = 0;
                    Int32 _CntBadDebt = 0;

                    #region " Check for multiple responsibility "

                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        Int16 _Party = 0;
                        if (Int16.TryParse(c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY).ToString(), out _Party) == true)
                        {
                            if (_Party > 0)
                            {

                                if (c1Insurance.GetData(i, COL_INSSELFMODE) != null && c1Insurance.GetData(i, COL_INSSELFMODE).ToString() != "")
                                {
                                    if (PayerMode.Self == (PayerMode)Convert.ToInt16(c1Insurance.GetData(i, COL_INSSELFMODE)))
                                    { _CntInsurance = i; }
                                }
                                if (c1Insurance.GetData(i, COL_INSSELFMODE) != null && c1Insurance.GetData(i, COL_INSSELFMODE).ToString() != "")
                                {
                                    if (PayerMode.BadDebt == (PayerMode)Convert.ToInt16(c1Insurance.GetData(i, COL_INSSELFMODE)))
                                    { _CntBadDebt = i; }
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
                                            MessageBox.Show("Same responsibility is found for multiple Insurances. Please assign unique responsibility.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            c1Insurance.Select(i, COL_INSURANCERESPONSIBILITY);
                                            c1Insurance.Focus();
                                            _insresult = false;
                                            return _insresult;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion " Check for multiple responsibility"

                    #region " Check is first party selected "

                    if (_IsFirstPartySelected == false)
                    {
                        MessageBox.Show("Please specify first responsibility. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Insurance.Select(1, COL_INSURANCERESPONSIBILITY);
                        c1Insurance.Focus();
                        _insresult = false;
                        return _insresult;
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
                                    MessageBox.Show("Responsibility is out of order. Please specify responsibility in sequence. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    c1Insurance.Select(i, COL_INSURANCERESPONSIBILITY);
                                    c1Insurance.Focus();
                                    _insresult = false;
                                    return _insresult;
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
                                    if (_Party > _CntInsurance && _Party != _CntBadDebt)
                                    {
                                        MessageBox.Show("You can not specify responsibility after self. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1Insurance.Select(_CntInsurance + 1, COL_INSURANCERESPONSIBILITY);
                                        c1Insurance.Focus();
                                        _insresult = false;
                                        return _insresult;
                                    }
                                }
                            }
                        }
                        if (_CntBadDebt > 0 && _CntInsurance > _CntBadDebt)
                        {
                            MessageBox.Show("You cannot specify BadDebt responsibility before self. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Insurance.Select(_CntBadDebt + 1, COL_INSURANCERESPONSIBILITY);
                            c1Insurance.Focus();
                            _insresult = false;
                            return _insresult;
                        }
                    }
                    #endregion " Check Self is Last responsibility "

                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        if (PayerMode.Self == (PayerMode)Convert.ToInt16(c1Insurance.GetData(i, COL_INSSELFMODE)))
                        {
                            if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") == "")
                            {
                                MessageBox.Show("Self responsibility cannot be blank.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1Insurance.Select(i, COL_INSURANCERESPONSIBILITY);
                                c1Insurance.Focus();
                                return false;
                            }
                        }

                    }

                }

                #endregion " Insurance responsibility validation "

                if (!ValidateAppointmentsLinkingToCharges())
                {
                    this.btnPatientAppointments_Click(this, new EventArgs());
                    _insresult = false;
                    return _insresult;
                }
                

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                _insresult = false;
            }
            finally
            {
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
                if (ogloBilling != null) { ogloBilling.Dispose(); ogloBilling = null; }
            }
            return _insresult;
        }

        public void SetDataUserWise()
        {
            try
            {

                SetLoginUserChangeData();

                //mskClaimDate.Text = gloBilling.GetUserWiseCloseDay(Convert.ToInt64(appSettings["UserID"]), CloseDayType.Charge);


                #region " Retrive UserID from appSettings "

                //if (appSettings["UserID"] != null)
                //{
                //    if (appSettings["UserID"] != "")
                //    {
                //        _UserID = Convert.ToInt64(appSettings["UserID"]);
                //    }
                //}
                //else
                //{
                //    _UserID = 0;
                //}

                #endregion

                #region " Retrive UserName from appSettings "

                //if (appSettings["UserName"] != null)
                //{
                //    if (appSettings["UserName"] != "")
                //    {
                //        _UserName = Convert.ToString(appSettings["UserName"]);
                //    }
                //}
                //else
                //{
                //    _UserName = "";
                //}

                #endregion

                #region " Retrive EMR Type (gloEMR40 or gloEMR50)"


                //_EMRType = GetSetting("MigrateToEMRType");


                #endregion

                //Need to update the Charges tray user wise

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void ClearFormData()
        {
            try
            {
                _TransactionID = 0;
                txtOutSideLabCharges.Text = "0.00";
                chkOutSideLab.Checked = false;
                txtClaimNo.Text = "";
                //txtCases.Text = string.Empty;
                //txtCases.Tag = null;
                ////mskClaimDate.Text = gloBilling.GetUserWiseCloseDay(_UserID, CloseDayType.Charge);
                //mskHospitaliztionFrom.Text = ""; 
                //mskHospitaliztionTo.Text = ""; 
                //mskUnableFromDate.Text = ""; 
                //mskUnableTillDate.Text = ""; 
                //mskAccidentDate.Text = ""; 
                //mskInjuryDate.Text = "";
                //mskOnsiteDate.Text = "";
                //mskOtherDate.Text = "";

                ////chkAutoClaim.Checked = false;
                ////chkWorkersComp.Checked = false;
                //chkOutSideLab.Checked = false;
                ////chkOther.Checked = false;
                //CmbAccidentType.SelectedIndex = 0;
                //if (cmbState != null) { cmbState.SelectedIndex = -1; }
                //if (cmbClaimNo != null) { cmbClaimNo.SelectedIndex = -1; }

                //if (_oBox19Notes != null) { _oBox19Notes.Clear(); }
                if (_oBox19Note != null) { _oBox19Note = null; }

                _sClaimBox10dNote = string.Empty;

                _sClaimRefNo = "";
                _IsbCliamReplacement = false;
                _bIsRefprovAsSupervisor = false; 
                _IllnessDate = 0;
                _LastSeenDate = 0;
                _UnableToWorkFromDate_MoreClaimData = 0;
                _UnableToWorkTillDate_MoreClaimData = 0;
                _PWKReportTypeCode = "";
                _PWKReportTransmissionCode = "";
                _PWKAttachmentControlNumber = "";
                _MammogramCertNumber = "";
                _InsuranceName = "";
                _sServiceAuthExceptionCode = "";
                _sDelayReasonCode = "";
                _sMedicaidResubmissionCode = "";
                //_EMRExamID = 0;
                //_EMRVisitID = 0; 
                //GetHoldMessage();
                lblHoldMessage.Text = "";
                if (oUB != null)
                {
                    oUB.Dispose();
                    oUB = null; 
                }
                oUB = new UB04Data();

                pnlReplacmentClaimDtls.Visible = false;
                lblReplacementClaim.Text = "";
                lblReplacementClaimNo.Text = "";
                lblReplacementClaim.Tag = "";
                lblReplacementClaimNo.Tag = "";
                nReplacementByTransMasterID = 0;
                nReplacingTransMasterID = 0;
                ReplacementClaimCreationType = ReplacementClaimCreationType.None;
                txtClaimCLIAno.Text = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void SetNonFacilityFee()
        {
            
            tls_btnFNFCharges.Tag = "NonFacility";
            tls_btnFNFCharges.Text = "&Non Facility Charges";
            tls_btnFNFCharges.Image = global ::gloBilling.Properties.Resources.Non_Facility_Charges;
        }

        private void SetFacilityFee()
        {
            
            tls_btnFNFCharges.Tag = "Facility";
            tls_btnFNFCharges.Text = "&Facility Charges";
            tls_btnFNFCharges.Image = global ::gloBilling.Properties.Resources.Facility_Charges;
        }

        private void IsDefaultFeeScheduleExpired()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            string _sqlquery = "";
            object _sqlresult = null;
            Int64 _clinicFeeSettingValue = 0;

            try
            {
               
                _clinicFeeSettingValue = gloCharges.GetDefaultFeeSchedule();

                oDB.Connect(false);
                #region " Check if Fee Schedule is not expired "

                if (_clinicFeeSettingValue > 0)
                {
                    _sqlquery = "select ISNULL(nFeeScheduleID,'') AS nFeeScheduleID from BL_FeeSchedule_Allocation WITH (NOLOCK) where " +
                    " nFeeScheduleID = " + _clinicFeeSettingValue + " " +
                    " and nToDate >= " + gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString()) + " and nClinicID = " + _ClinicID + "";

                    _sqlresult =  oDB.ExecuteScalar_Query(_sqlquery);
                    if (_sqlresult == null || Convert.ToString(_sqlresult).Trim() == "" || Convert.ToInt64(_sqlresult.ToString()) <= 0)
                    {
                        MessageBox.Show("Default fee schedule is expired please extend the time period.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Default fee schedule is not set.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion " Check if Fee Schedule is not expired "

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); }
            }
        }        

        private bool Ok_Click()
        {           
            bool _retValue = false;
            bIsChargeSaved = false;
            try
            {
                //gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                //if (oSecurity.isBadDebtPatient(oPatientControl.PatientID, true))
                //{

                //    DialogResult dr = System.Windows.Forms.MessageBox.Show("Patient is in BAD DEBT status, are you sure you want to create a new charge ?", _messageBoxCaption, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
                //    if (dr.ToString() == "No")
                //    {
                //        return _retValue;
                //    }
                //}
                //if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }


                if (ValidateInsurance())
                {
                    if (!(UC_gloBillingTransactionLines == null))
                    {
                        if (UC_gloBillingTransactionLines.GetLinesCount() > 1)
                        {
                            if (ValidationMessageExpClaim())
                            {
                                if (!IsClaimRulesEnabled() || ValidateRules())
                                {
                                    _retValue = SaveClaimData();
                                    if (_retValue && _EMRExamID != 0 && !_IsSaveNextTreatmentClick)
                                    {
                                        _bEMRTreatmentLoading = true;
                                        _bIsSplitEMRTreatmentLoaded = true;
                                        setTreatmentSplitAfterSave();
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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //_IsSaveNextTreatmentClick = false;
            }
            return _retValue;
        }
        
        private Boolean SaveTreatment()
        {
            bool _retVal = false;
            try
            {

                _retVal = Ok_Click();

                #region " Select the added Line "
                if (_retVal == true)
                {
                    UC_gloBillingTransactionLines.SelectTransactionLine(UC_gloBillingTransactionLines.CurrentTransactionLine);
                    UC_gloBillingTransactionLines_onGrid_SelChanged(null, null);
                    _IllnessDate = 0;
                    _LastSeenDate = 0;
                    _UnableToWorkFromDate_MoreClaimData = 0;
                    _UnableToWorkTillDate_MoreClaimData = 0;
                    _PWKReportTypeCode = "";
                    _PWKReportTransmissionCode = "";
                    _PWKAttachmentControlNumber = "";
                    _MammogramCertNumber = "";
                    SetFormDataAfterClaimSave(_TransactionID, _nextClaimNo);
                }
                #endregion " Select the added Line "
               
                //if (chkFacilityFeeSchedule.Checked == false && chkNonFacilityCharges.Checked == false)
                //{
                //    SetFacilitySettingsData(); // if due to some reason above line dont have any facility or non facility charge then select default once.
                //}

                if (_retVal == true)
                {
                    //if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                    //{
                    //    //Set default POS To the Trasaction Lines
                    //    if (UC_gloBillingTransactionLines.FacilityPOS > 0)
                    //    {
                    //        if (UC_gloBillingTransactionLines != null)
                    //        {
                    //            UC_gloBillingTransactionLines.SetFacilityPOS();
                    //        }
                    //    }
                    //}
                    if (_oClaimHold != null)
                    {
                        _oClaimHold.Dispose();
                        _oClaimHold = null;
                    }
                    _oClaimHold = new ClaimHold();
                    SetHoldnMoreClaimDataMesseges();
                    //CheckForEPSDTEnabled();
                    _IsModifyed = true;

                    if (UC_gloBillingTransactionLines != null)
                    {
                        //if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                        //{
                        //    if (Convert.ToString(UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE)) == "")
                        //    {
                        //        //UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(mskClaimDate.Text));
                        //        if (_sLastServiceLineDOS != "")
                        //        { UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(_sLastServiceLineDOS)); }
                        //    }
                        //}
                        UC_gloBillingTransactionLines.TreatmentType = ExternalChargesType.gloEMRTreatment;
                    }

                    oPatientControl.SetFocusOnPatientSearch = true;
                }
                CheckForPatientCases();
                SetLastGlobalPeriods();
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
            }
            return _retVal;

        }
        
        private String GetSetting(String SettingName)
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_DatabaseConnectionString);
            object value = null;
            string _Result = "";
            try
            {

                ogloSettings.GetSetting(SettingName, out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    _Result = Convert.ToString(value);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Result = "";
            }
            finally
            {
                ogloSettings.Dispose();
                ogloSettings = null;
            }
            return _Result;
        }

        private void SetModifiedInsurance()
        {
            try
            {
                string _fillInsuranceName = "";
                Int64 _fillInsuranceID = 0;
                Int32 _fillInsSelfMode = 0;

                CheckEnum _Selected = CheckEnum.None;
                int _CurRowIndex = c1Insurance.Row;

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
                        if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                        {
                            for (int lineIndex = 1; lineIndex < UC_gloBillingTransactionLines.GetLinesCount(); lineIndex++)
                            {
                                UC_gloBillingTransactionLines.AddInsurance(lineIndex, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                            }
                        }
                    }
                    else
                    {
                        _fillInsuranceID = 0;
                        _fillInsuranceName = "";
                        _fillInsSelfMode = PayerMode.None.GetHashCode();
                        if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                        {
                            for (int lineIndex = 1; lineIndex < UC_gloBillingTransactionLines.GetLinesCount(); lineIndex++)
                            {
                                UC_gloBillingTransactionLines.AddInsurance(lineIndex, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                            }
                            //UC_gloBillingTransactionLines.AddInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion " Public & Private Methods "
                
        #region " Design C1 Grid Methods "

        private void DesignInsuranceGrid()
        {
            //c1Insurance.Clear(ClearFlags.All);
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
                c1Insurance.SetData(0, COL_INSSTARTENDDATE, "Start-End");
                c1Insurance.SetData(0, COL_INSCOMMENT, "Note");
                c1Insurance.SetData(0, COL_INSSELFMODE, "Mode");
                c1Insurance.SetData(0, COL_INSURANCECOPAYAMT, "CopayAmt");

                c1Insurance.SetData(0, COL_INSURANCEWORKERCOMP, "Workers Comp");
                c1Insurance.SetData(0, COL_INSURANCEAUTOCLAIM, "Auto Claim");
                c1Insurance.SetData(0, COL_INSURANCECURRRESP, "Resp.");
                c1Insurance.SetData(0, COL_INSURANCEPLANONHOLD, "IsPlanOnHold");
                c1Insurance.SetData(0, COL_ISINSTITUTIONALBILLING, "IsInstitutional");


                c1Insurance.Cols[COL_INSURANCEWORKERCOMP].DataType = typeof(System.Boolean);
                c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].DataType = typeof(System.Char);
                c1Insurance.Cols[COL_INSURANCEPARTY].DataType = typeof(System.Char);
                c1Insurance.Cols[COL_INSURANCECURRRESP].DataType = typeof(System.Char);
                c1Insurance.Cols[COL_INSURANCEPLANONHOLD].DataType = typeof(System.Boolean);
                c1Insurance.Cols[COL_ISINSTITUTIONALBILLING].DataType = typeof(System.Boolean);

                int nWidth;
                nWidth = pnlPatientInsurence.Width;

                c1Insurance.Cols[COL_SELECT].DataType = System.Type.GetType("System.Boolean");//Select Column
                c1Insurance.Cols[COL_SELECT].AllowEditing = true;
                c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].AllowEditing = true;
                c1Insurance.Cols[COL_INSURANCEWORKERCOMP].AllowEditing = true;
                c1Insurance.Cols[COL_INSURANCEWORKERCOMP].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCECURRRESP].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCEPLANONHOLD].AllowEditing = false;
                c1Insurance.Cols[COL_ISINSTITUTIONALBILLING].AllowEditing = false;
                c1Insurance.Cols[COL_INSSTARTENDDATE].AllowEditing = false;
                c1Insurance.Cols[COL_INSCOMMENT].AllowEditing = false;

                bool _designWidth = false;
                try
                {
                    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_DatabaseConnectionString);
                    _designWidth = oSetting.LoadGridColumnWidth(c1Insurance, gloSettings.ModuleOfGridColumn.Billing, _UserID);
                    oSetting.Dispose();
                }
                catch (Exception)
                {
                }

                if (_designWidth == false)
                {
                    c1Insurance.Cols[COL_SELECT].Width = Convert.ToInt32(nWidth * 0.09);
                    c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].Width = Convert.ToInt32(nWidth * 0.09);
                    c1Insurance.Cols[COL_INSURANCENAME].Width = 260;// Convert.ToInt32(nWidth * 0.30);
                    c1Insurance.Cols[COL_INSURANCEID].Width = 0;
                    c1Insurance.Cols[COL_INSURANCETYPE].Width = Convert.ToInt32(nWidth * 0.12);
                    c1Insurance.Cols[COL_INSSELFMODE].Width = 0;
                    c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Width = 0;
                    c1Insurance.Cols[COL_INSURANCEAUTOCLAIM].Width = Convert.ToInt32(nWidth * 0.14);
                    c1Insurance.Cols[COL_INSURANCEPARTY].Width = 20;
                    c1Insurance.Cols[COL_INSSTARTENDDATE].Width = 115;
                    c1Insurance.Cols[COL_INSCOMMENT].Width = 200;
                    c1Insurance.Cols[COL_INSURANCEPLANONHOLD].Width = 0;
                    c1Insurance.Cols[COL_ISINSTITUTIONALBILLING].Width = 0;

                }
                else
                {
                    //c1Insurance.Cols[COL_SELECT].Width = Convert.ToInt32(nWidth * 0.09);
                    c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].Width = 55;
                    c1Insurance.Cols[COL_INSURANCENAME].Width = 260;
                    c1Insurance.Cols[COL_INSURANCETYPE].Width = 73;
                    c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Width = 0;
                    c1Insurance.Cols[COL_INSURANCEPLANONHOLD].Width = 0;
                    c1Insurance.Cols[COL_ISINSTITUTIONALBILLING].Width = 0;
                    c1Insurance.Cols[COL_INSSTARTENDDATE].Width = 115;
                    c1Insurance.Cols[COL_INSCOMMENT].Width = 200;
                }
                //c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].TextAlign = TextAlignEnum.CenterCenter;
                c1Insurance.Cols[COL_INSURANCERESPONSIBILITY].TextAlign = TextAlignEnum.RightCenter;
                c1Insurance.Cols[COL_INSCOMMENT].TextAlign = TextAlignEnum.LeftCenter;
                c1Insurance.Cols[COL_SELECT].Visible = false;
                c1Insurance.Cols[COL_INSURANCEID].Visible = false;
                c1Insurance.Cols[COL_INSSELFMODE].Visible = false;
                c1Insurance.Cols[COL_INSURANCECOPAYAMT].Visible = false;
                c1Insurance.Cols[COL_INSURANCEAUTOCLAIM].Visible = false;
                c1Insurance.Cols[COL_INSURANCECONTACTID].Visible = false;
                c1Insurance.Cols[COL_INSURANCEPARTY].Visible = false;
                c1Insurance.Cols[COL_INSURANCECURRRESP].Visible = false;
                c1Insurance.Cols[COL_INSURANCEPLANONHOLD].Visible = false;
                c1Insurance.Cols[COL_ISINSTITUTIONALBILLING].Visible = false;

                c1Insurance.Cols[COL_INSURANCENAME].TextAlign = TextAlignEnum.LeftCenter;
                c1Insurance.Cols[COL_INSURANCEWORKERCOMP].TextAlignFixed = TextAlignEnum.CenterCenter;
            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            {
                c1Insurance.EndUpdate();
            }
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

                        }

                    }
                    catch
                    {
                        cStyle = c1Insurance.Styles.Add("BubbleValues");

                    }
                    cStyle.BackColor = Color.White;

                    C1.Win.C1FlexGrid.CellRange rgBubbleValues = c1Insurance.GetCellRange(i, COL_INSURANCERESPONSIBILITY);
                    //cStyle = c1Insurance.Styles.Add("BubbleValues" + i);
                    rgBubbleValues.Style = cStyle;
                }
            }
        }

        private void ReorderInsurance()
        {
            c1Insurance.Sort(SortFlags.Ascending, COL_INSURANCENAME);
            c1Insurance.Sort(SortFlags.Ascending, COL_INSURANCETYPE);
            c1Insurance.Sort(SortFlags.Descending, COL_INSSELFMODE);
            c1Insurance.Sort(SortFlags.Ascending, COL_INSURANCEPARTY);
            // RoopaliB 19 July 2012            
            // After sorting Insurance plan we need to retrive setting for default occurance date as DOS for current primary Ins.plan
            // and will assigned to UB04 object.      
            UB04Default(); 
        }

        private void DesignDxGrid()
        {
            //c1Dx.Clear(ClearFlags.All);

            try
            {
                c1Dx.BeginUpdate();

                _isDxListLoading = true;
                c1Dx.Clear(ClearFlags.All);

                c1Dx.Cols.Count = COL_DX_COUNT;
                c1Dx.Rows.Count = 1;

                c1Dx.SetData(0, COL_SELECT, "Select");
                c1Dx.SetData(0, COL_DX_CODE, "Code");
                c1Dx.SetData(0, COL_DX_DESC, "Description");
                c1Dx.SetData(0, COL_DX_ISPRIMARY, "Primary");

                int nWidht = 0;
                nWidht = pnlPatientInsurence.Width;
                //if (nWidht < 616)
                //    nWidht = 616;
                c1Dx.Cols[COL_DX_SELECT].DataType = typeof(System.Boolean);
                c1Dx.Cols[COL_DX_ISPRIMARY].DataType = typeof(System.Boolean);

                c1Dx.Cols[COL_DX_SELECT].AllowEditing = true;
                c1Dx.Cols[COL_DX_CODE].AllowEditing = false;
                c1Dx.Cols[COL_DX_DESC].AllowEditing = false;
                c1Dx.Cols[COL_DX_ISPRIMARY].AllowEditing = false;

                c1Dx.Cols[COL_DX_CODE].TextAlign = TextAlignEnum.LeftCenter;
                c1Dx.Cols[COL_DX_DESC].TextAlign = TextAlignEnum.LeftCenter;

                c1Dx.Cols[COL_SELECT].Width = Convert.ToInt32(nWidht * 0.09);
                c1Dx.Cols[COL_DX_CODE].Width = Convert.ToInt32(nWidht * 0.11);
                c1Dx.Cols[COL_DX_DESC].Width = Convert.ToInt32(nWidht * 0.65);
                c1Dx.Cols[COL_DX_ISPRIMARY].Width = Convert.ToInt32(nWidht * 0.09);

                c1Dx.Cols[COL_DX_ISPRIMARY].TextAlignFixed = TextAlignEnum.CenterCenter;

                c1Dx.SelectionMode = SelectionModeEnum.Row;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                c1Dx.EndUpdate();
                _isDxListLoading = false;
                
            }


        }

        #endregion " Design C1 Grid Methods "

        #region " C1 Grid Events "

        private void c1Insurance_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                SetHoldnMoreClaimDataMesseges();
                //CheckForEPSDTEnabled();
                
                if (!_IsFormLoading)
                {
                    gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);    
                    //Transaction _Transaction = new Transaction();
                    
                    try
                    {
                        if (_nResponsibleParty != Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)) ||_sResponsibleNo==string.Empty )
                        {
                            #region " Expanded Claim Settings "

                            Int64 _ContactId = 0;
                            string _sContactName = "Admin";

                            if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                            {
                                _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                                _sContactName = Convert.ToString(c1Insurance.GetData(1, COL_INSURANCENAME));
                                ShowHideUB();
                            }

                            
                            ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                            
                            int _cntr = 0;
                            ArrayList arrDX = UC_gloBillingTransactionLines.GetUniqueDx();
                            if (arrDX != null)
                            {
                                _cntr = arrDX.Count;
                            }

                            if (UC_gloBillingTransactionLines.GetLinesCount() - 1 > _NoOfMaxServiceLines && _cntr > _NoOfMaxDiagnosis)
                            {
                                MessageBox.Show(" Claim has a max limit of " + _NoOfMaxServiceLines + " service lines and " + _NoOfMaxDiagnosis + " diagnosis. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (UC_gloBillingTransactionLines.GetLinesCount() - 1 > _NoOfMaxServiceLines)
                            {
                                MessageBox.Show(" Claim has a max limit of " + _NoOfMaxServiceLines + " service lines. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (_cntr > _NoOfMaxDiagnosis)
                            {
                                MessageBox.Show(" Claim has a max limit of " + _NoOfMaxDiagnosis + " diagnosis. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            //_Transaction.NoOfServiceLine = _NoOfMaxServiceLines;
                            //_Transaction.NoOfDiagnosis = _NoOfMaxDiagnosis;
                            UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                            UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;
                            UC_gloBillingTransactionLines._nContactID = _ContactId;
                            UC_gloBillingTransactionLines.setnewAllowedAmount();
                            #endregion

                            _sResponsibleNo = Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "");          
                            _nResponsibleParty = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                            
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        //if (_Transaction != null) { _Transaction.Dispose(); }
                        if (ogloBilling != null) { ogloBilling.Dispose(); }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
                     

        }

        private void c1Insurance_AfterSelChange(object sender, RangeEventArgs e)
        {
            if (_IsFormLoading == false)
            {
                SetHoldnMoreClaimDataMesseges();
                //CheckForEPSDTEnabled();
            }
        }

        private void c1Insurance_CellChanged(object sender, RowColEventArgs e)
        {
            if (_IsFormLoading == false)
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
                        if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                        {
                            UC_gloBillingTransactionLines.AddInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                        }
                    }
                    else
                    {
                        _fillInsuranceID = 0;
                        _fillInsuranceName = "";
                        _fillInsSelfMode = PayerMode.None.GetHashCode();
                        if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                        {
                            UC_gloBillingTransactionLines.AddInsurance(UC_gloBillingTransactionLines.CurrentTransactionLine, _fillInsuranceID, _fillInsuranceName, _fillInsSelfMode);
                        }
                    }



                    if (e.Col == COL_INSURANCERESPONSIBILITY && e.Row > 0)
                    {
                        //Int16 _result = Convert.ToInt16(c1Insurance.GetData(e.Row, e.Col));
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

                        //To Remove the Previous Flag
                        for (int i = 0; i <= c1Insurance.Rows.Count - 1; i++)
                        {
                            c1Insurance.SetCellImage(i, COL_INSURANCERESPONSIBILITY, null);
                        }

                        Char _first = Convert.ToChar(c1Insurance.GetData(1, 1));
                        if (_first.ToString() == "1")
                        {
                            //To mark the current flag
                            System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Normal_Priority;
                            c1Insurance.SetCellImage(1, COL_INSURANCERESPONSIBILITY, imgFlag);

                            DefaultSelfPayFeeSchedule();                            
                        }
                    }
                    //Code Added for self option availability on lines as per current Resposibility -Sameer 11-15-2013
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
                    //Code Added for self option availability
                    if (_bEMRTreatmentLoading == false)
                    {
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

            }
           
        }

        private void c1Insurance_KeyUp(object sender, KeyEventArgs e)
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            // Transaction _Transaction = new Transaction();

            SetHoldnMoreClaimDataMesseges();
            //CheckForEPSDTEnabled();
            if (e.KeyCode == Keys.Delete && c1Insurance.ColSel == COL_INSURANCERESPONSIBILITY && c1Insurance.RowSel > 0)
            {
                c1Insurance.SetData(c1Insurance.RowSel, COL_INSURANCEPARTY, Convert.ToChar("X"));
                c1Insurance.SetData(c1Insurance.RowSel, c1Insurance.ColSel, Convert.ToString(""));
                c1Insurance.SetCellImage(c1Insurance.RowSel, COL_INSURANCERESPONSIBILITY, null);

                _sResponsibleNo = Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "");
                if (_nResponsibleParty != Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID)) || _sResponsibleNo == string.Empty)
                {
                    #region " Expanded Claim Settings "

                    Int64 _ContactId = 0;
                    string _sContactName = "Admin";

                    if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                    {
                        _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                        _sContactName = Convert.ToString(c1Insurance.GetData(1, COL_INSURANCENAME));
                    }

                    //_NoOfMaxServiceLines_old = _NoOfMaxServiceLines;
                    ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                    //if (_NoOfMaxServiceLines_old > _NoOfMaxServiceLines)
                    //{
                    //    MessageBox.Show(" Current insurance plan \"" + Convert.ToString(c1Insurance.GetData(1, COL_INSURANCENAME)) + "\" has max limit of " + _NoOfMaxServiceLines + " service line", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    int _cntr = 0;
                    //for (int i = 1; i < c1Dx.Rows.Count; i++)
                    //{
                    //    if (c1Dx.GetCellCheck(i, COL_DX_SELECT) == CheckEnum.Checked)
                    //    { _cntr++; }
                    //}

                    ArrayList arrDX = UC_gloBillingTransactionLines.GetUniqueDx();
                    if (arrDX != null)
                    {
                        _cntr = arrDX.Count;
                    }

                    if (UC_gloBillingTransactionLines.GetLinesCount() - 1 > _NoOfMaxServiceLines && _cntr > _NoOfMaxDiagnosis)
                    {
                        MessageBox.Show(" Claim has a max limit of " + _NoOfMaxServiceLines + " service lines and " + _NoOfMaxDiagnosis + " diagnosis. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (UC_gloBillingTransactionLines.GetLinesCount() - 1 > _NoOfMaxServiceLines)
                    {
                        MessageBox.Show(" Claim has a max limit of " + _NoOfMaxServiceLines + " service lines. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (_cntr > _NoOfMaxDiagnosis)
                    {
                        MessageBox.Show(" Claim has a max limit of " + _NoOfMaxDiagnosis + " diagnosis. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                  //  _Transaction.NoOfServiceLine = _NoOfMaxServiceLines;
                 //   _Transaction.NoOfDiagnosis = _NoOfMaxDiagnosis;
                    UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                    UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;
                    UC_gloBillingTransactionLines._nContactID = _ContactId;
                    UC_gloBillingTransactionLines.setnewAllowedAmount();

                    #endregion
                    _sResponsibleNo = Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "");
                    _nResponsibleParty = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));

                }
            }
            ogloBilling.Dispose();
            ogloBilling = null;
        }

        private void c1Insurance_BeforeEdit(object sender, RowColEventArgs e)
        {
        
            if (c1Insurance.Rows.Count > 1)
            {

                _sResponsibleNo = Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "");
                _nResponsibleParty = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
            }

        }

        private void c1Insurance_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_DatabaseConnectionString);
                oSetting.SaveGridColumnWidth(c1Insurance, gloSettings.ModuleOfGridColumn.Billing, _UserID);
                oSetting.Dispose();
            }
            catch (Exception)
            {
            }
        }

        private void c1Insurance_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Int64 _currentPatientId = 0;
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            gloUserRights.ClsgloUserRights ObjUserRights = new gloUserRights.ClsgloUserRights(_DatabaseConnectionString);
         //   Transaction _Transaction = new Transaction();
            try
            {
               
                ObjUserRights.CheckForUserRights(_UserName);
                if (ObjUserRights.ModifyPatient == true)
                {
                    this.c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);

                    if (this.PatientID > 0)
                    {
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_DatabaseConnectionString);
                        ogloPatient.ShowPatientRegistration(this.PatientID, gloPatient.ModifyPatientDetailType.Insurance, out _currentPatientId,this);
                        //DesignInsuranceGrid();
                        //FillPatientInsurances(_currentPatientId);
                        //SetInsuranceParty();
                        //SetModifiedInsurance();

                        LoadPatientModifiedData();
                        ogloPatient.Dispose();
                        ogloPatient = null;
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
                }
                ObjUserRights.Dispose();
                ObjUserRights = null;
                #region " Expanded Claim Settings "

                //Int64 _ContactId = 0;

                //if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                //{
                //    _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                //}

                //ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                //_Transaction.NoOfServiceLine = _NoOfMaxServiceLines;
                //_Transaction.NoOfDiagnosis = _NoOfMaxDiagnosis;
                //UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                //UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                this.c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                ogloBilling.Dispose();
                ogloBilling = null;
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

        private void c1PatientEMRExams_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HitTestInfo hitInfo = c1PatientEMRExams.HitTest(e.X, e.Y);
                if (hitInfo.Row > 0)
                {
                    LoadEMRTreatment_New(hitInfo);
                    //bIsSaveCharges = false;

                    C1Dignosis.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
                    C1Dignosis.OwnerDrawCell += c1FlexGrid1_OwnerDrawCell;
                }
                else
                {
                    if (hitInfo.Row == 0)
                    {
                        if (c1PatientEMRExams.Cols[hitInfo.Column].Name.ToString().Trim() != "")
                        {
                            lblSearch.Text = c1PatientEMRExams.Cols[hitInfo.Column].Name.ToString() + " : ";
                            lblSearch.Tag = hitInfo.Column;
                            txtSearch.Focus();
                        }
                    }
                }
            }
        }

        private void c1Dx_CellChanged(object sender, RowColEventArgs e)
        {
            if (_isDxListLoading == true)
            { return; }

            try
            {
                this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                int _cntr = 0;
                if (e.Col == COL_DX_SELECT)
                {
                    for (int i = 1; i < c1Dx.Rows.Count; i++)
                    {
                        if (c1Dx.GetCellCheck(i, COL_DX_SELECT) == CheckEnum.Checked)
                        {
                            _cntr++; 
                        }
                    }
                    //if (_cntr - 1 > 3)
                    //if (_cntr - 1 > 7)
                    if (_cntr - 1 > (_NoOfMaxDiagnosis-1))
                    {
                        c1Dx.SetCellCheck(e.Row, e.Col, CheckEnum.Unchecked);
                        if (_bDxFlag == false)
                        //MessageBox.Show("Cannot select more than 4 Diagnosis.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show("Claim has a max limit of " + _NoOfMaxDiagnosis + " diagnosis.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (e.Col == COL_DX_ISPRIMARY)
                {
                    for (int i = 1; i < c1Dx.Rows.Count; i++)
                    {
                        if (i != e.Row)
                        {
                            if (c1Dx.GetCellCheck(i, COL_DX_ISPRIMARY) == CheckEnum.Checked)
                            { c1Dx.SetCellCheck(i, COL_DX_ISPRIMARY, CheckEnum.Unchecked); }
                        }
                    }
                    string _dxCode = "";
                    string _dxDesc = "";
                    int _CurRowIndex = e.Row;
                    CheckEnum _Selected = CheckEnum.None;
                    bool _retVal = false;

                    if (c1Dx.Rows.Count > 0)
                    {
                        _Selected = c1Dx.GetCellCheck(_CurRowIndex, COL_DX_ISPRIMARY);
                        if (_Selected == CheckEnum.Checked)
                        {
                            _dxCode = Convert.ToString(c1Dx.GetData(_CurRowIndex, COL_DX_CODE)).Trim();
                            _dxDesc = Convert.ToString(c1Dx.GetData(_CurRowIndex, COL_DX_DESC)).Trim();

                            if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.GetLinesCount() > 1)
                            {
                                _retVal = UC_gloBillingTransactionLines.AddLinePrimaryDx(UC_gloBillingTransactionLines.CurrentTransactionLine, _dxCode, _dxDesc);
                            }
                            else
                            {
                                _dxCode = "";
                                _dxDesc = "";
                                _retVal = UC_gloBillingTransactionLines.AddLinePrimaryDx(UC_gloBillingTransactionLines.CurrentTransactionLine, _dxCode, _dxDesc);
                            }
                        }
                        else
                        {
                            UC_gloBillingTransactionLines.RemoveLinePrimaryDx(UC_gloBillingTransactionLines.CurrentTransactionLine);
                        }

                        if (_retVal == false)
                        { c1Dx.SetCellCheck(_CurRowIndex, COL_DX_ISPRIMARY, CheckEnum.Unchecked); }
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
            }
        }
        
        #endregion " C1 Grid Events "

        #region "Menu Events"

        private void mnuBilling_AddLine_Click(object sender, EventArgs e)
        {
            tls_btnAddLine.PerformClick();
        }

        private void mnuBilling_RemoveLine_Click(object sender, EventArgs e)
        {
            tls_btnRemoveLine.PerformClick();
        }

        private void mnuBilling_SmartTreatment_Click(object sender, EventArgs e)
        {
            tlb_btnSmartTreatment.PerformClick();
        }

        private void mnuBilling_AddNote_Click(object sender, EventArgs e)
        {
            tlb_AddNotes.PerformClick();
        }

        private void mnuBilling_AddEMRTreatment_Click(object sender, EventArgs e)
        {
            tlb_AddEMRTreatment.PerformClick();
        }

        private void mnuBilling_Save_Click(object sender, EventArgs e)
        {
            tls_btnOK.PerformClick();
        }

        private void mnuBilling_Refresh_Click(object sender, EventArgs e)
        {
            UC_gloBillingTransactionLines.SortControl();
        }

        #endregion

        #region " Form Button Click Events "

        private void btnAdd_PriorAuthorization_Click(object sender, EventArgs e)
        {
            try
            {
                if (_PatientID > 0)
                {                   

                    if (gloCharges.CheckPriorAuthorization(_PatientID) == true)
                    {
                        if (txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag).Trim() != "" && (mskClaimDate.Text).Replace("/", "").Replace("_", "").Trim() != "")
                        {
                            gloPMGeneral.frmShowPriorAuthorization oPriorAuthorization = new gloPMGeneral.frmShowPriorAuthorization(_DatabaseConnectionString, gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text), Convert.ToInt64(txtPriorAuthorizationNo.Tag), _PatientID);
                            try
                            {
                                oPriorAuthorization.ShowDialog(this);
                                if ( oPriorAuthorization.CurrentPriorAuthorization.ToString().Trim() != "0")
                                {
                                    Int64 nSelectedRefferals = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                                    ReloadPatientRefferals(_PatientID);
                                    chk_SameasBillingProvider.Checked = false;

                                    txtPriorAuthorizationNo.Tag = oPriorAuthorization.CurrentPriorAuthorization;
                                    txtPriorAuthorizationNo.Text = oPriorAuthorization.CurrentPriorAuthorizationNo;
                                    if (oPriorAuthorization.CurrentReferralProviderID != 0)
                                    {
                                        bool bExists = false;
                                        if (cmbReferralProvider.DataSource != null)
                                        {
                                            foreach (DataRowView drv in cmbReferralProvider.Items)
                                            {
                                                if (Convert.ToInt64(drv.Row.ItemArray[0]) == oPriorAuthorization.CurrentReferralProviderID) { bExists = true; break; }
                                            }
                                        }
                                        if (bExists)
                                        {
                                            cmbReferralProvider.SelectedValue = oPriorAuthorization.CurrentReferralProviderID;
                                        }
                                        else
                                        {
                                            cmbReferralProvider.SelectedValue = nSelectedRefferals;
                                        }
                                    }
                                    else
                                    {
                                        cmbReferralProvider.SelectedValue = nSelectedRefferals;
                                    }
                                }
                                this.PatientHasPriorAuthorization = gloCharges.CheckForActivePriorAuth(_PatientID);
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
                        else if ((mskClaimDate.Text).Replace("/", "").Replace("_", "").Trim() != "")
                        {
                            gloPMGeneral.frmShowPriorAuthorization oPriorAuthorization = new gloPMGeneral.frmShowPriorAuthorization(_DatabaseConnectionString, gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text), 0, _PatientID);
                            try
                            {
                                oPriorAuthorization.ShowDialog(this);
                                if ( oPriorAuthorization.CurrentPriorAuthorization.ToString().Trim() != "0")
                                {
                                    Int64 nSelectedRefferals = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                                    ReloadPatientRefferals(_PatientID);
                                    chk_SameasBillingProvider.Checked = false;

                                    txtPriorAuthorizationNo.Tag = oPriorAuthorization.CurrentPriorAuthorization;
                                    txtPriorAuthorizationNo.Text = oPriorAuthorization.CurrentPriorAuthorizationNo;
                                    if (oPriorAuthorization.CurrentReferralProviderID != 0)
                                    {
                                        bool bExists = false;
                                        if (cmbReferralProvider.DataSource != null)
                                        {
                                            foreach (DataRowView drv in cmbReferralProvider.Items)
                                            {
                                                if (Convert.ToInt64(drv.Row.ItemArray[0]) == oPriorAuthorization.CurrentReferralProviderID) { bExists = true; break; }
                                            }
                                        }
                                        if (bExists)
                                        {
                                            cmbReferralProvider.SelectedValue = oPriorAuthorization.CurrentReferralProviderID;
                                        }
                                        else
                                        {
                                            cmbReferralProvider.SelectedValue = nSelectedRefferals;
                                        }
                                       
                                    }
                                    else
                                    {
                                        cmbReferralProvider.SelectedValue = nSelectedRefferals;
                                    }
                                }
                                this.PatientHasPriorAuthorization = gloCharges.CheckForActivePriorAuth(_PatientID);
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
                                    Int64 nSelectedRefferals = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                                    ReloadPatientRefferals(_PatientID);
                                    chk_SameasBillingProvider.Checked = false;

                                    txtPriorAuthorizationNo.Tag = oPriorAuthorization.CurrentPriorAuthorization;
                                    txtPriorAuthorizationNo.Text = oPriorAuthorization.CurrentPriorAuthorizationNo;
                                    if (oPriorAuthorization.CurrentReferralProviderID != 0)
                                    {
                                        bool bExists = false;
                                        if (cmbReferralProvider.DataSource != null)
                                        {
                                            foreach (DataRowView drv in cmbReferralProvider.Items)
                                            {
                                                if (Convert.ToInt64(drv.Row.ItemArray[0]) == oPriorAuthorization.CurrentReferralProviderID) { bExists = true; break; }
                                            }
                                        }
                                        if (bExists)
                                        {
                                            cmbReferralProvider.SelectedValue = oPriorAuthorization.CurrentReferralProviderID;
                                        }
                                        else
                                        {
                                            cmbReferralProvider.SelectedValue = nSelectedRefferals;
                                        }
                                    }
                                    else
                                    {
                                        cmbReferralProvider.SelectedValue = nSelectedRefferals;
                                    }
                                }
                                this.PatientHasPriorAuthorization = gloCharges.CheckForActivePriorAuth(_PatientID);
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
                                Int64 nSelectedRefferals = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                                ReloadPatientRefferals(_PatientID);
                                chk_SameasBillingProvider.Checked = false;

                                txtPriorAuthorizationNo.Tag = priorAuthID;
                                txtPriorAuthorizationNo.Text = objsetupauth._PriorAuthorizationNo.ToString().Trim();

                                if (objsetupauth._ReferralID != 0)
                                {
                                    bool bExists = false;
                                    if (cmbReferralProvider.DataSource != null)
                                    {
                                        foreach (DataRowView drv in cmbReferralProvider.Items)
                                        {
                                            if (Convert.ToInt64(drv.Row.ItemArray[0]) == objsetupauth._ReferralID) { bExists = true; break; }
                                        }
                                    }
                                    if (bExists)
                                    {
                                        cmbReferralProvider.SelectedValue = objsetupauth._ReferralID;
                                    }
                                    else
                                    {
                                        cmbReferralProvider.SelectedValue = nSelectedRefferals;
                                    }
                                }
                                else
                                {
                                    cmbReferralProvider.SelectedValue = nSelectedRefferals;
                                }
                            }

                            this.PatientHasPriorAuthorization = gloCharges.CheckForActivePriorAuth(_PatientID);
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
                            if (!gloCharges.getPriorAuthorizationStatus(Convert.ToInt64(txtPriorAuthorizationNo.Tag)))
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
                    MessageBox.Show("No Patient is selected.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                mskClaimDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
            
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

        private void btnHideInsurance_Click(object sender, EventArgs e)
        {
            pnlPatientInsurence.Visible = true;
            btnShowInsurance.Visible = true;
            btnShowInsurance.BringToFront();
            btnHideInsurance.Visible = false;
        }

        private void btnShowInsurance_Click(object sender, EventArgs e)
        {
            pnlPatientInsurence.Visible = false;
            btnHideInsurance.Visible = true;
            btnHideInsurance.BringToFront();
            btnShowInsurance.Visible = false;
        }

        private void btnHideDiagnosis_Click(object sender, EventArgs e)
        {
            pnllstBoxDiagnosis.Visible = false;
            btnHideDiagnosis.Visible = false;
            btnShowDiagnosis.Visible = true;
        }

        private void btnShowDiagnosis_Click(object sender, EventArgs e)
        {
            pnllstBoxDiagnosis.Visible = true;
            btnHideDiagnosis.Visible = true;
            btnShowDiagnosis.Visible = false;
        }

        private void tlb_DXClose_Click(object sender, EventArgs e)
        {
            pnlExamCPTDX.SendToBack();

        }

        private void tlb_DXSave_Click(object sender, EventArgs e)
        {
           
            C1Dignosis.FinishEditing();
            if (oEMRTransLineSplit != null)
            {
                oEMRTransLineSplit.Dispose();
                oEMRTransLineSplit = null;
            }
            oEMRTransLinesSplit = new TransactionLines();
            if (ArraylistSelectedCPTS != null)
            {
                ArraylistSelectedCPTS.Clear();
            }
            ArraylistSelectedCPTS = new ArrayList();
            try
            {
                if (C1Dignosis != null && C1Dignosis.Rows.Count > 0)
                {

                    DialogResult _dlgDxSave = DialogResult.None;
                    _dlgDxSave = MessageBox.Show("Dx CPT selections will be saved.\n Warning - Charge Entry values will be reset. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (_dlgDxSave == DialogResult.OK)
                    {
                        Int32 _rowcout = 0;
                        
                        _dtNoPostCharges = new DataTable();
                        _dtNoPostCharges.Columns.Add("nLineNo");
                        _dtNoPostCharges.Columns.Add("sCPTCodes");

                        _dtLoadedCPTS = new DataTable();
                        _dtLoadedCPTS.Columns.Add("nLineNo");
                        _dtLoadedCPTS.Columns.Add("sCPTCodes");


                        if (_IsICD9Driven && _nEMRTreatmentType != ExternalChargesType.HL7InboundCharges)
                        {
                            #region "ICD9 Driven "

                            #region "To get the Line No"

                            for (int i = 1; i <= C1Dignosis.Rows.Count - 1; i++)
                            {
                                var node = this.C1Dignosis.Rows[i].Node;

                                if (node.Checked == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    if ((C1Dignosis.GetData(i, DX_Col_CPTCode) != null) && (Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)) != ""))
                                    {
                                        if ((Convert.ToString(C1Dignosis.GetData(i, DX_Col_ClaimStatus)) == ""))
                                        {
                                            _rowcout++;
                                        }
                                    }

                                }
                            }

                            Int32 _LineCount = UC_gloBillingTransactionLines.GetLinesCount();
                            if (_rowcout == _LineCount - 1)
                            {
                                _LineCount = _LineCount - 1;
                            }
                            else if (_rowcout < (_LineCount - 1))
                            {
                                _LineCount = _rowcout;
                            }

                            #endregion

                            #region "To Add and Remove Lines Based on Post and No Post"
                            Boolean bIsExist = false;
                            for (int i = 1; i <= C1Dignosis.Rows.Count - 1; i++)
                            {
                                var node = this.C1Dignosis.Rows[i].Node;

                                if (node.Checked == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    if (C1Dignosis.GetCellCheck(i, DX_Col_Select) == CheckEnum.Unchecked)
                                    {

                                        if ((C1Dignosis.GetData(i, DX_Col_CPTCode) != null) && (Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)) != ""))
                                        {

                                            //To Save only the No Posted CPT's
                                            if (Convert.ToString(C1Dignosis.GetData(i, DX_Col_ClaimStatus)) == "No Post")
                                            {

                                            }
                                            else
                                            {
                                                bIsExist = false;
                                                if (oEMRTransLinesSplit != null && oEMRTransLinesSplit.Count > 0)
                                                {
                                                    for (int cntr = 0; cntr <= oEMRTransLinesSplit.Count - 1; cntr++)
                                                    {
                                                        if (Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)) == oEMRTransLinesSplit[cntr].CPTCode)
                                                        {
                                                            bIsExist = true;
                                                        }
                                                    }
                                                }
                                                if (!bIsExist)
                                                {
                                                    DataRow _dr = _dtLoadedCPTS.NewRow();
                                                    _dr["nLineNo"] = Convert.ToInt32(C1Dignosis.GetData(i, DX_Col_CPTLineNo));
                                                    _dr["sCPTCodes"] = Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode));
                                                    _dtLoadedCPTS.Rows.Add(_dr);
                                                    _dtLoadedCPTS.AcceptChanges();
                                                    if (oEMRTransLineSplit != null)
                                                    {
                                                        oEMRTransLineSplit.Dispose();
                                                        oEMRTransLineSplit = null;
                                                    }
                                                    oEMRTransLineSplit = new TransactionLine();

                                                    oEMRTransLineSplit.CPTCode = Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode));
                                                    oEMRTransLineSplit.CPTDescription = Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTDesc));
                                                    oEMRTransLineSplit.DateServiceFrom = Convert.ToDateTime(EMRExam.GetEMRExamDOS(_EMRExamID, _nEMRTreatmentType));
                                                    oEMRTransLineSplit.Unit = Convert.ToDecimal(C1Dignosis.GetData(i, DX_Col_Units));

                                                    #region " Modifiers"

                                                    if (node.Nodes != null)
                                                    {
                                                        int _icount = 1;

                                                        foreach (Node drv in node.Nodes)
                                                        {
                                                            string sMOD = "";
                                                            string[] MOD = null;
                                                            switch (_icount)
                                                            {
                                                                case 1:
                                                                    sMOD = Convert.ToString(drv.Data);
                                                                    MOD = sMOD.Split('-');

                                                                    if (MOD.Length == 2)
                                                                    {
                                                                        oEMRTransLineSplit.Mod1Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod1Description = Convert.ToString(MOD[1]);

                                                                    }
                                                                    else if (MOD.Length == 1)
                                                                    {
                                                                        oEMRTransLineSplit.Mod1Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod1Description = "";

                                                                    }
                                                                    else if (MOD.Length > 2)
                                                                    {
                                                                        oEMRTransLineSplit.Mod1Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod1Description = Convert.ToString(sMOD.Substring(sMOD.IndexOf("-")));

                                                                    }
                                                                    else
                                                                    {
                                                                        oEMRTransLineSplit.Mod1Code = Convert.ToString("");
                                                                        oEMRTransLineSplit.Mod1Description = Convert.ToString("");
                                                                    }


                                                                    break;
                                                                case 2:


                                                                    sMOD = Convert.ToString(drv.Data);
                                                                    MOD = sMOD.Split('-');

                                                                    if (MOD.Length == 2)
                                                                    {
                                                                        oEMRTransLineSplit.Mod2Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod2Description = Convert.ToString(MOD[1]);

                                                                    }
                                                                    else if (MOD.Length == 1)
                                                                    {
                                                                        oEMRTransLineSplit.Mod2Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod2Description = "";

                                                                    }
                                                                    else if (MOD.Length > 2)
                                                                    {
                                                                        oEMRTransLineSplit.Mod2Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod2Description = Convert.ToString(sMOD.Substring(sMOD.IndexOf("-")));

                                                                    }
                                                                    else
                                                                    {
                                                                        oEMRTransLineSplit.Mod2Code = Convert.ToString("");
                                                                        oEMRTransLineSplit.Mod2Description = Convert.ToString("");
                                                                    }

                                                                    break;
                                                                case 3:
                                                                    sMOD = Convert.ToString(drv.Data);
                                                                    MOD = sMOD.Split('-');

                                                                    if (MOD.Length == 2)
                                                                    {
                                                                        oEMRTransLineSplit.Mod3Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod3Description = Convert.ToString(MOD[1]);

                                                                    }
                                                                    else if (MOD.Length == 1)
                                                                    {
                                                                        oEMRTransLineSplit.Mod3Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod3Description = "";

                                                                    }
                                                                    else if (MOD.Length > 2)
                                                                    {
                                                                        oEMRTransLineSplit.Mod3Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod3Description = Convert.ToString(sMOD.Substring(sMOD.IndexOf("-")));

                                                                    }
                                                                    else
                                                                    {
                                                                        oEMRTransLineSplit.Mod3Code = Convert.ToString("");
                                                                        oEMRTransLineSplit.Mod3Description = Convert.ToString("");
                                                                    }
                                                                    break;
                                                                case 4:
                                                                    sMOD = Convert.ToString(drv.Data);
                                                                    MOD = sMOD.Split('-');

                                                                    if (MOD.Length == 2)
                                                                    {
                                                                        oEMRTransLineSplit.Mod4Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod4Description = Convert.ToString(MOD[1]);

                                                                    }
                                                                    else if (MOD.Length == 1)
                                                                    {
                                                                        oEMRTransLineSplit.Mod4Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod4Description = "";

                                                                    }
                                                                    else if (MOD.Length > 2)
                                                                    {
                                                                        oEMRTransLineSplit.Mod4Code = Convert.ToString(MOD[0]);
                                                                        oEMRTransLineSplit.Mod4Description = Convert.ToString(sMOD.Substring(sMOD.IndexOf("-")));

                                                                    }
                                                                    else
                                                                    {
                                                                        oEMRTransLineSplit.Mod4Code = Convert.ToString("");
                                                                        oEMRTransLineSplit.Mod4Description = Convert.ToString("");
                                                                    }
                                                                    break;

                                                            }
                                                            _icount++;
                                                        }
                                                    }

                                                    #endregion

                                                    #region "To get the ICD9 -For the Selected CPT From Parent"

                                                    string sICD9 = "";
                                                    string[] _sICD9 = null;
                                                    int dxCounter = 1;

                                                    for (int k = 1; k <= C1Dignosis.Rows.Count - 1; k++)
                                                    {
                                                        if ((Convert.ToString(C1Dignosis.GetData(k, DX_Col_CPTCode)).Trim() != "") && (Convert.ToString(C1Dignosis.GetData(k, DX_Col_Units)) != ""))
                                                        {
                                                            if (Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)) == Convert.ToString(C1Dignosis.GetData(k, DX_Col_CPTCode)))
                                                            {
                                                                if (C1Dignosis.Rows[k].Node.Parent.Data != null)
                                                                {
                                                                    sICD9 = Convert.ToString(C1Dignosis.Rows[k].Node.Parent.Data);
                                                                    _sICD9 = sICD9.Split('-');
                                                                }

                                                                switch (dxCounter)
                                                                {
                                                                    case 1:
                                                                        if (_sICD9.Length == 2)
                                                                        {
                                                                            oEMRTransLineSplit.Dx1Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx1Description = Convert.ToString(_sICD9[1]);
                                                                            oEMRTransLineSplit.Dx1Ptr = true;
                                                                            oEMRTransLineSplit.LinePrimaryDxCode = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.LinePrimaryDxDesc = Convert.ToString(_sICD9[1]);

                                                                        }
                                                                        else if (_sICD9.Length == 1)
                                                                        {
                                                                            oEMRTransLineSplit.Dx1Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx1Description = "";
                                                                            oEMRTransLineSplit.Dx1Ptr = true;
                                                                            oEMRTransLineSplit.LinePrimaryDxCode = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.LinePrimaryDxDesc = "";
                                                                        }
                                                                        else if (_sICD9.Length > 2)
                                                                        {
                                                                            oEMRTransLineSplit.Dx1Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx1Description = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));
                                                                            oEMRTransLineSplit.Dx1Ptr = true;
                                                                            oEMRTransLineSplit.LinePrimaryDxCode = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.LinePrimaryDxDesc = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));

                                                                        }
                                                                        else
                                                                        {
                                                                            oEMRTransLineSplit.Dx1Code = "";
                                                                            oEMRTransLineSplit.Dx1Description = "";
                                                                            oEMRTransLineSplit.Dx1Ptr = false;
                                                                            oEMRTransLineSplit.LinePrimaryDxCode = "";
                                                                            oEMRTransLineSplit.LinePrimaryDxDesc = "";
                                                                        }
                                                                        break;
                                                                    case 2:
                                                                        if (_sICD9.Length == 2)
                                                                        {
                                                                            oEMRTransLineSplit.Dx2Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx2Description = Convert.ToString(_sICD9[1]);
                                                                            oEMRTransLineSplit.Dx2Ptr = true;


                                                                        }
                                                                        else if (_sICD9.Length == 1)
                                                                        {
                                                                            oEMRTransLineSplit.Dx2Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx2Description = "";
                                                                            oEMRTransLineSplit.Dx2Ptr = true;

                                                                        }
                                                                        else if (_sICD9.Length > 2)
                                                                        {
                                                                            oEMRTransLineSplit.Dx2Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx2Description = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));
                                                                            oEMRTransLineSplit.Dx2Ptr = true;

                                                                        }
                                                                        else
                                                                        {
                                                                            oEMRTransLineSplit.Dx2Code = "";
                                                                            oEMRTransLineSplit.Dx2Description = "";
                                                                            oEMRTransLineSplit.Dx2Ptr = false;

                                                                        }
                                                                        break;

                                                                    case 3:
                                                                        if (_sICD9.Length == 2)
                                                                        {
                                                                            oEMRTransLineSplit.Dx3Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx3Description = Convert.ToString(_sICD9[1]);
                                                                            oEMRTransLineSplit.Dx3Ptr = true;


                                                                        }
                                                                        else if (_sICD9.Length == 1)
                                                                        {
                                                                            oEMRTransLineSplit.Dx3Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx3Description = "";
                                                                            oEMRTransLineSplit.Dx3Ptr = true;

                                                                        }
                                                                        else if (_sICD9.Length > 2)
                                                                        {
                                                                            oEMRTransLineSplit.Dx3Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx3Description = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));
                                                                            oEMRTransLineSplit.Dx3Ptr = true;

                                                                        }
                                                                        else
                                                                        {
                                                                            oEMRTransLineSplit.Dx3Code = "";
                                                                            oEMRTransLineSplit.Dx3Description = "";
                                                                            oEMRTransLineSplit.Dx3Ptr = false;

                                                                        }
                                                                        break;

                                                                    case 4:
                                                                        if (_sICD9.Length == 2)
                                                                        {
                                                                            oEMRTransLineSplit.Dx4Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx4Description = Convert.ToString(_sICD9[1]);
                                                                            oEMRTransLineSplit.Dx4Ptr = true;


                                                                        }
                                                                        else if (_sICD9.Length == 1)
                                                                        {
                                                                            oEMRTransLineSplit.Dx4Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx4Description = "";
                                                                            oEMRTransLineSplit.Dx4Ptr = true;

                                                                        }
                                                                        else if (_sICD9.Length > 2)
                                                                        {
                                                                            oEMRTransLineSplit.Dx4Code = Convert.ToString(_sICD9[0]);
                                                                            oEMRTransLineSplit.Dx4Description = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));
                                                                            oEMRTransLineSplit.Dx4Ptr = true;

                                                                        }
                                                                        else
                                                                        {
                                                                            oEMRTransLineSplit.Dx4Code = "";
                                                                            oEMRTransLineSplit.Dx4Description = "";
                                                                            oEMRTransLineSplit.Dx4Ptr = false;

                                                                        }
                                                                        break;
                                                                }
                                                                dxCounter++;
                                                            }

                                                        }


                                                    }



                                                    #endregion


                                                    oEMRTransLineSplit.TransactionLineId = _LineCount;
                                                    oEMRTransLineSplit.EMRTreatmentLineNo = Convert.ToInt32(C1Dignosis.GetData(i, DX_Col_CPTLineNo));
                                                    oEMRTransLinesSplit.Add(oEMRTransLineSplit);
                                                    _LineCount = _LineCount + 1;
                                                }
                                            }

                                        }
                                    }
                                    else if (C1Dignosis.GetCellCheck(i, DX_Col_Select) == CheckEnum.Checked)
                                    {
                                        bIsExist = false;
                                        if (oEMRTransLinesSplit != null && oEMRTransLinesSplit.Count > 0)
                                        {
                                            for (int cntr = 0; cntr <= oEMRTransLinesSplit.Count - 1; cntr++)
                                            {
                                                if (Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)) == oEMRTransLinesSplit[cntr].CPTCode)
                                                {
                                                    bIsExist = true;
                                                }
                                            }
                                        }
                                        if (!bIsExist)
                                        {

                                            DataRow _dr = _dtLoadedCPTS.NewRow();
                                            _dr["nLineNo"] = Convert.ToInt32(C1Dignosis.GetData(i, DX_Col_CPTLineNo));
                                            _dr["sCPTCodes"] = Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode));
                                            _dtLoadedCPTS.Rows.Add(_dr);
                                            _dtLoadedCPTS.AcceptChanges();

                                            if (Convert.ToString(C1Dignosis.GetData(i, DX_Col_ClaimStatus)) == "")
                                            {

                                                if (oEMRTransLineSplit != null)
                                                {
                                                    oEMRTransLineSplit.Dispose();
                                                    oEMRTransLineSplit = null;
                                                }
                                                oEMRTransLineSplit = new TransactionLine();

                                                oEMRTransLineSplit.CPTCode = Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode));
                                                oEMRTransLineSplit.CPTDescription = Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTDesc));
                                                oEMRTransLineSplit.DateServiceFrom = Convert.ToDateTime(EMRExam.GetEMRExamDOS(_EMRExamID, _nEMRTreatmentType));
                                                oEMRTransLineSplit.Unit = Convert.ToDecimal(C1Dignosis.GetData(i, DX_Col_Units));

                                                #region " Modifiers"

                                                if (node.Nodes != null)
                                                {
                                                    int _icount = 1;

                                                    foreach (Node drv in node.Nodes)
                                                    {
                                                        string sMOD = "";
                                                        string[] MOD = null;
                                                        switch (_icount)
                                                        {
                                                            case 1:
                                                                sMOD = Convert.ToString(drv.Data);
                                                                MOD = sMOD.Split('-');

                                                                if (MOD.Length == 2)
                                                                {
                                                                    oEMRTransLineSplit.Mod1Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod1Description = Convert.ToString(MOD[1]);

                                                                }
                                                                else if (MOD.Length == 1)
                                                                {
                                                                    oEMRTransLineSplit.Mod1Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod1Description = "";

                                                                }
                                                                else if (MOD.Length > 2)
                                                                {
                                                                    oEMRTransLineSplit.Mod1Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod1Description = Convert.ToString(sMOD.Substring(sMOD.IndexOf("-")));

                                                                }
                                                                else
                                                                {
                                                                    oEMRTransLineSplit.Mod1Code = Convert.ToString("");
                                                                    oEMRTransLineSplit.Mod1Description = Convert.ToString("");
                                                                }


                                                                break;
                                                            case 2:


                                                                sMOD = Convert.ToString(drv.Data);
                                                                MOD = sMOD.Split('-');

                                                                if (MOD.Length == 2)
                                                                {
                                                                    oEMRTransLineSplit.Mod2Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod2Description = Convert.ToString(MOD[1]);

                                                                }
                                                                else if (MOD.Length == 1)
                                                                {
                                                                    oEMRTransLineSplit.Mod2Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod2Description = "";

                                                                }
                                                                else if (MOD.Length > 2)
                                                                {
                                                                    oEMRTransLineSplit.Mod2Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod2Description = Convert.ToString(sMOD.Substring(sMOD.IndexOf("-")));

                                                                }
                                                                else
                                                                {
                                                                    oEMRTransLineSplit.Mod2Code = Convert.ToString("");
                                                                    oEMRTransLineSplit.Mod2Description = Convert.ToString("");
                                                                }

                                                                break;
                                                            case 3:
                                                                sMOD = Convert.ToString(drv.Data);
                                                                MOD = sMOD.Split('-');

                                                                if (MOD.Length == 2)
                                                                {
                                                                    oEMRTransLineSplit.Mod3Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod3Description = Convert.ToString(MOD[1]);

                                                                }
                                                                else if (MOD.Length == 1)
                                                                {
                                                                    oEMRTransLineSplit.Mod3Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod3Description = "";

                                                                }
                                                                else if (MOD.Length > 2)
                                                                {
                                                                    oEMRTransLineSplit.Mod3Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod3Description = Convert.ToString(sMOD.Substring(sMOD.IndexOf("-")));

                                                                }
                                                                else
                                                                {
                                                                    oEMRTransLineSplit.Mod3Code = Convert.ToString("");
                                                                    oEMRTransLineSplit.Mod3Description = Convert.ToString("");
                                                                }
                                                                break;
                                                            case 4:
                                                                sMOD = Convert.ToString(drv.Data);
                                                                MOD = sMOD.Split('-');

                                                                if (MOD.Length == 2)
                                                                {
                                                                    oEMRTransLineSplit.Mod4Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod4Description = Convert.ToString(MOD[1]);

                                                                }
                                                                else if (MOD.Length == 1)
                                                                {
                                                                    oEMRTransLineSplit.Mod4Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod4Description = "";

                                                                }
                                                                else if (MOD.Length > 2)
                                                                {
                                                                    oEMRTransLineSplit.Mod4Code = Convert.ToString(MOD[0]);
                                                                    oEMRTransLineSplit.Mod4Description = Convert.ToString(sMOD.Substring(sMOD.IndexOf("-")));

                                                                }
                                                                else
                                                                {
                                                                    oEMRTransLineSplit.Mod4Code = Convert.ToString("");
                                                                    oEMRTransLineSplit.Mod4Description = Convert.ToString("");
                                                                }
                                                                break;

                                                        }
                                                        _icount++;
                                                    }
                                                }

                                                #endregion

                                                #region "To get the ICD9 -For the Selected CPT From Parent"

                                                string sICD9 = "";
                                                string[] _sICD9 = null;
                                                int dxCounter = 1;

                                                for (int k = 1; k <= C1Dignosis.Rows.Count - 1; k++)
                                                {
                                                    if ((Convert.ToString(C1Dignosis.GetData(k, DX_Col_CPTCode)).Trim() != "") && (Convert.ToString(C1Dignosis.GetData(k, DX_Col_Units)) != ""))
                                                    {
                                                        if (Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)) == Convert.ToString(C1Dignosis.GetData(k, DX_Col_CPTCode)))
                                                        {
                                                            if (C1Dignosis.Rows[k].Node.Parent.Data != null)
                                                            {
                                                                sICD9 = Convert.ToString(C1Dignosis.Rows[k].Node.Parent.Data);
                                                                _sICD9 = sICD9.Split('-');
                                                            }

                                                            switch (dxCounter)
                                                            {
                                                                case 1:
                                                                    if (_sICD9.Length == 2)
                                                                    {
                                                                        oEMRTransLineSplit.Dx1Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx1Description = Convert.ToString(_sICD9[1]);
                                                                        oEMRTransLineSplit.Dx1Ptr = true;
                                                                        oEMRTransLineSplit.LinePrimaryDxCode = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.LinePrimaryDxDesc = Convert.ToString(_sICD9[1]);

                                                                    }
                                                                    else if (_sICD9.Length == 1)
                                                                    {
                                                                        oEMRTransLineSplit.Dx1Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx1Description = "";
                                                                        oEMRTransLineSplit.Dx1Ptr = true;
                                                                        oEMRTransLineSplit.LinePrimaryDxCode = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.LinePrimaryDxDesc = "";
                                                                    }
                                                                    else if (_sICD9.Length > 2)
                                                                    {
                                                                        oEMRTransLineSplit.Dx1Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx1Description = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));
                                                                        oEMRTransLineSplit.Dx1Ptr = true;
                                                                        oEMRTransLineSplit.LinePrimaryDxCode = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.LinePrimaryDxDesc = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));

                                                                    }
                                                                    else
                                                                    {
                                                                        oEMRTransLineSplit.Dx1Code = "";
                                                                        oEMRTransLineSplit.Dx1Description = "";
                                                                        oEMRTransLineSplit.Dx1Ptr = false;
                                                                        oEMRTransLineSplit.LinePrimaryDxCode = "";
                                                                        oEMRTransLineSplit.LinePrimaryDxDesc = "";
                                                                    }
                                                                    break;
                                                                case 2:
                                                                    if (_sICD9.Length == 2)
                                                                    {
                                                                        oEMRTransLineSplit.Dx2Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx2Description = Convert.ToString(_sICD9[1]);
                                                                        oEMRTransLineSplit.Dx2Ptr = true;


                                                                    }
                                                                    else if (_sICD9.Length == 1)
                                                                    {
                                                                        oEMRTransLineSplit.Dx2Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx2Description = "";
                                                                        oEMRTransLineSplit.Dx2Ptr = true;

                                                                    }
                                                                    else if (_sICD9.Length > 2)
                                                                    {
                                                                        oEMRTransLineSplit.Dx2Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx2Description = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));
                                                                        oEMRTransLineSplit.Dx2Ptr = true;

                                                                    }
                                                                    else
                                                                    {
                                                                        oEMRTransLineSplit.Dx2Code = "";
                                                                        oEMRTransLineSplit.Dx2Description = "";
                                                                        oEMRTransLineSplit.Dx2Ptr = false;

                                                                    }
                                                                    break;

                                                                case 3:
                                                                    if (_sICD9.Length == 2)
                                                                    {
                                                                        oEMRTransLineSplit.Dx3Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx3Description = Convert.ToString(_sICD9[1]);
                                                                        oEMRTransLineSplit.Dx3Ptr = true;


                                                                    }
                                                                    else if (_sICD9.Length == 1)
                                                                    {
                                                                        oEMRTransLineSplit.Dx3Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx3Description = "";
                                                                        oEMRTransLineSplit.Dx3Ptr = true;

                                                                    }
                                                                    else if (_sICD9.Length > 2)
                                                                    {
                                                                        oEMRTransLineSplit.Dx3Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx3Description = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));
                                                                        oEMRTransLineSplit.Dx3Ptr = true;

                                                                    }
                                                                    else
                                                                    {
                                                                        oEMRTransLineSplit.Dx3Code = "";
                                                                        oEMRTransLineSplit.Dx3Description = "";
                                                                        oEMRTransLineSplit.Dx3Ptr = false;

                                                                    }
                                                                    break;

                                                                case 4:
                                                                    if (_sICD9.Length == 2)
                                                                    {
                                                                        oEMRTransLineSplit.Dx4Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx4Description = Convert.ToString(_sICD9[1]);
                                                                        oEMRTransLineSplit.Dx4Ptr = true;


                                                                    }
                                                                    else if (_sICD9.Length == 1)
                                                                    {
                                                                        oEMRTransLineSplit.Dx4Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx4Description = "";
                                                                        oEMRTransLineSplit.Dx4Ptr = true;

                                                                    }
                                                                    else if (_sICD9.Length > 2)
                                                                    {
                                                                        oEMRTransLineSplit.Dx4Code = Convert.ToString(_sICD9[0]);
                                                                        oEMRTransLineSplit.Dx4Description = Convert.ToString(sICD9.Substring(sICD9.IndexOf("-")));
                                                                        oEMRTransLineSplit.Dx4Ptr = true;

                                                                    }
                                                                    else
                                                                    {
                                                                        oEMRTransLineSplit.Dx4Code = "";
                                                                        oEMRTransLineSplit.Dx4Description = "";
                                                                        oEMRTransLineSplit.Dx4Ptr = false;

                                                                    }
                                                                    break;
                                                            }
                                                            dxCounter++;
                                                        }

                                                    }


                                                }



                                                #endregion


                                                oEMRTransLineSplit.TransactionLineId = _LineCount;
                                                oEMRTransLineSplit.EMRTreatmentLineNo = Convert.ToInt32(C1Dignosis.GetData(i, DX_Col_CPTLineNo));
                                                oEMRTransLinesSplit.Add(oEMRTransLineSplit);
                                                _LineCount = _LineCount + 1;
                                            }

                                        }
                                    }

                                    if (Convert.ToString(C1Dignosis.GetData(i, DX_Col_ClaimStatus)) == "No Post")
                                    {

                                        DataRow _dr = _dtNoPostCharges.NewRow();
                                        _dr["nLineNo"] = Convert.ToInt32(C1Dignosis.GetData(i, DX_Col_CPTLineNo));
                                        _dr["sCPTCodes"] = Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode));
                                        _dtNoPostCharges.Rows.Add(_dr);
                                        _dtNoPostCharges.AcceptChanges();



                                        UC_gloBillingTransactionLines.DeleteTransactionLine(Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)), 1, _IsICD9Driven);
                                    }

                                }
                                else if (Convert.ToString(C1Dignosis.GetData(i, DX_Col_ClaimStatus)) == "No Post")
                                {

                                    DataRow _dr = _dtNoPostCharges.NewRow();
                                    _dr["nLineNo"] = Convert.ToInt32(C1Dignosis.GetData(i, DX_Col_CPTLineNo));
                                    _dr["sCPTCodes"] = Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode));
                                    _dtNoPostCharges.Rows.Add(_dr);
                                    _dtNoPostCharges.AcceptChanges();


                                    UC_gloBillingTransactionLines.DeleteTransactionLine(Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)), 1, _IsICD9Driven);
                                }
                                else
                                {
                                    if ((C1Dignosis.GetData(i, DX_Col_CPTCode) != null) && (Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)) != ""))
                                    {
                                        if (C1Dignosis.GetCellCheck(i, DX_Col_Select) == CheckEnum.Checked)
                                        {
                                            UC_gloBillingTransactionLines.DeleteTransactionLine(Convert.ToString(C1Dignosis.GetData(i, DX_Col_CPTCode)), 1, _IsICD9Driven);
                                        }
                                    }
                                }


                            }

                            #endregion

                            #endregion "ICD9 Driven"
                        }
                        else
                        {
                            #region " CPT Driven

                            #region "To get the Line No"

                            for (int i = 1; i <= C1Dignosis.Rows.Count - 1; i++)
                            {
                                if ((C1Dignosis.GetCellCheck(i, CPT_COL_SELECT) == CheckEnum.Checked))
                                {
                                    if ((Convert.ToString(C1Dignosis.GetData(i, CPT_COL_ClaimStatus)) == ""))
                                    {
                                        _rowcout++;
                                    }
                                }
                            }

                            Int32 _LineCount = UC_gloBillingTransactionLines.GetLinesCount();
                            if (_rowcout == _LineCount - 1)
                            {
                                _LineCount = _LineCount - 1;
                            }
                            else if (_rowcout < _LineCount - 1)
                            {
                                _LineCount = _rowcout;
                            }

                            #endregion

                            #region "To Add and Remove Lines Based on Post and No Post"

                            for (int i = 1; i <= C1Dignosis.Rows.Count - 1; i++)
                            {
                                if ((C1Dignosis.GetCellCheck(i, CPT_COL_SELECT) == CheckEnum.Checked) && (C1Dignosis.GetCellCheck(i, CPT_ISBILLED) == CheckEnum.Checked))
                                {
                                    if ((Convert.ToString(C1Dignosis.GetData(i, CPT_COL_ClaimStatus)) == ""))
                                    {

                                        DataRow _dr = _dtLoadedCPTS.NewRow();
                                        _dr["nLineNo"] = Convert.ToInt32(C1Dignosis.GetData(i, CPT_COL_LINE_NO));
                                        _dr["sCPTCodes"] = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE));
                                        _dtLoadedCPTS.Rows.Add(_dr);
                                        _dtLoadedCPTS.AcceptChanges();
                                        if (oEMRTransLineSplit != null)
                                        {
                                            oEMRTransLineSplit.Dispose();
                                            oEMRTransLineSplit = null;
                                        }
                                        oEMRTransLineSplit = new TransactionLine();
                                        oEMRTransLineSplit.CPTCode = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE));
                                        oEMRTransLineSplit.DateServiceFrom = Convert.ToDateTime(C1Dignosis.GetData(i, CPT_COL_DOS));
                                        oEMRTransLineSplit.CPTDescription = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_DESC));
                                        oEMRTransLineSplit.Unit = Convert.ToDecimal(C1Dignosis.GetData(i, CPT_COL_UNIT));

                                        #region "Setting DX Pointers"

                                        string[] sDX = null;
                                        string[] sDXDesc = null;
                                        string strDX = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_DX1_CODE));
                                        string strDXDesc = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_DX1_DESC));
                                        if (strDX.Trim() != "")
                                        {
                                            sDX = strDX.Split(',');
                                            sDXDesc = strDXDesc.Split(',');
                                        }

                                        if (sDX != null && sDX.Length > 0)
                                        {
                                            for (int j = 0; j < sDX.Length; j++)
                                            {
                                                strDX = Convert.ToString(sDX[j]).Trim();
                                                strDXDesc = Convert.ToString(sDXDesc[j]).Trim();

                                                if (j == 0)
                                                {
                                                    oEMRTransLineSplit.Dx1Code = strDX;
                                                    oEMRTransLineSplit.Dx1Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx1Ptr = true;
                                                    oEMRTransLineSplit.LinePrimaryDxCode = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_DX1_CODE));
                                                    oEMRTransLineSplit.LinePrimaryDxDesc = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_DX1_CODE));
                                                }
                                                else if (j == 1)
                                                {
                                                    oEMRTransLineSplit.Dx2Code = strDX;
                                                    oEMRTransLineSplit.Dx2Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx2Ptr = true;
                                                }
                                                else if (j == 2)
                                                {
                                                    oEMRTransLineSplit.Dx3Code = strDX;
                                                    oEMRTransLineSplit.Dx3Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx3Ptr = true;
                                                }
                                                else if (j == 3)
                                                {
                                                    oEMRTransLineSplit.Dx4Code = strDX;
                                                    oEMRTransLineSplit.Dx4Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx4Ptr = true;
                                                }
                                                else if (j == 4)
                                                {
                                                    oEMRTransLineSplit.Dx5Code = strDX;
                                                    oEMRTransLineSplit.Dx5Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx5Ptr = true;
                                                }
                                                else if (j == 5)
                                                {
                                                    oEMRTransLineSplit.Dx6Code = strDX;
                                                    oEMRTransLineSplit.Dx6Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx6Ptr = true;
                                                }
                                                else if (j == 6)
                                                {
                                                    oEMRTransLineSplit.Dx7Code = strDX;
                                                    oEMRTransLineSplit.Dx7Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx7Ptr = true;
                                                }
                                                else if (j == 7)
                                                {
                                                    oEMRTransLineSplit.Dx8Code = strDX;
                                                    oEMRTransLineSplit.Dx8Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx8Ptr = true;
                                                }

                                            }
                                        }

                                        #endregion

                                        #region "Setting MOD "

                                        string[] sMOD = null;
                                        string strMOD = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_MOD1_CODE));
                                        if (strMOD.Trim() != "")
                                        {
                                            sMOD = strMOD.Split(',');
                                        }
                                        if (sMOD != null && sMOD.Length > 0)
                                        {
                                            for (int j = 0; j < sMOD.Length; j++)
                                            {
                                                strMOD = Convert.ToString(sMOD[j]).Trim();

                                                if (j == 0)
                                                    oEMRTransLineSplit.Mod1Code = strMOD;
                                                else if (j == 1)
                                                    oEMRTransLineSplit.Mod2Code = strMOD;
                                                else if (j == 2)
                                                    oEMRTransLineSplit.Mod3Code = strMOD;
                                                else if (j == 3)
                                                    oEMRTransLineSplit.Mod4Code = strMOD;

                                            }
                                        }
                                        else
                                        {
                                            oEMRTransLineSplit.Mod1Code = "";
                                            oEMRTransLineSplit.Mod2Code = "";
                                            oEMRTransLineSplit.Mod3Code = "";
                                            oEMRTransLineSplit.Mod4Code = "";
                                        }
                                        #endregion

                                        oEMRTransLineSplit.TransactionLineId = _LineCount;
                                        oEMRTransLineSplit.EMRTreatmentLineNo = Convert.ToInt32(C1Dignosis.GetData(i, CPT_COL_LINE_NO));
                                        oEMRTransLinesSplit.Add(oEMRTransLineSplit);
                                        _LineCount = _LineCount + 1;
                                    }
                                    else if ((C1Dignosis.GetCellCheck(i, CPT_COL_SELECT) == CheckEnum.Unchecked) && (C1Dignosis.GetCellCheck(i, CPT_ISBILLED) == CheckEnum.Unchecked))
                                    {

                                        UC_gloBillingTransactionLines.DeleteTransactionLine(Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE)), Convert.ToInt32(C1Dignosis.GetData(i, CPT_COL_LINE_NO)), false);

                                    }

                                }
                                else if ((C1Dignosis.GetCellCheck(i, CPT_COL_SELECT) == CheckEnum.Unchecked) && (C1Dignosis.GetCellCheck(i, CPT_ISBILLED) == CheckEnum.Unchecked))
                                {

                                    UC_gloBillingTransactionLines.DeleteTransactionLine(Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE)), Convert.ToInt32(C1Dignosis.GetData(i, CPT_COL_LINE_NO)), false);

                                }
                                else if ((C1Dignosis.GetCellCheck(i, CPT_COL_SELECT) == CheckEnum.Checked) && (C1Dignosis.GetCellCheck(i, CPT_ISBILLED) == CheckEnum.Unchecked))
                                {

                                    //if ((C1Dignosis.GetData(i, CPT_COL_CPT_CODE) != null) && (Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE)) != ""))
                                    //{
                                    if ((Convert.ToString(C1Dignosis.GetData(i, CPT_COL_ClaimStatus)) == ""))
                                    {

                                        DataRow _dr = _dtLoadedCPTS.NewRow();
                                        _dr["nLineNo"] = Convert.ToInt32(C1Dignosis.GetData(i, CPT_COL_LINE_NO));
                                        _dr["sCPTCodes"] = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE));
                                        _dtLoadedCPTS.Rows.Add(_dr);
                                        _dtLoadedCPTS.AcceptChanges();

                                        if (oEMRTransLineSplit != null)
                                        {
                                            oEMRTransLineSplit.Dispose();
                                            oEMRTransLineSplit = null;
                                        }
                                        oEMRTransLineSplit = new TransactionLine();
                                        oEMRTransLineSplit.CPTCode = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE));
                                        oEMRTransLineSplit.DateServiceFrom = Convert.ToDateTime(C1Dignosis.GetData(i, CPT_COL_DOS));
                                        oEMRTransLineSplit.CPTDescription = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_DESC));
                                        oEMRTransLineSplit.Unit = Convert.ToDecimal(C1Dignosis.GetData(i, CPT_COL_UNIT));

                                        #region "Setting DX Pointers"

                                        string[] sDX = null;
                                        string[] sDXDesc = null;
                                        string strDX = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_DX1_CODE));
                                        string strDXDesc = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_DX1_DESC));
                                        if (strDX.Trim() != "")
                                        {
                                            sDX = strDX.Split(',');
                                            sDXDesc = strDXDesc.Split(',');
                                        }

                                        if (sDX != null && sDX.Length > 0)
                                        {
                                            for (int j = 0; j < sDX.Length; j++)
                                            {
                                                strDX = Convert.ToString(sDX[j]).Trim();
                                                strDXDesc = Convert.ToString(sDXDesc[j]).Trim();

                                                if (j == 0)
                                                {
                                                    oEMRTransLineSplit.Dx1Code = strDX;
                                                    oEMRTransLineSplit.Dx1Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx1Ptr = true;
                                                    oEMRTransLineSplit.LinePrimaryDxCode = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_DX1_CODE));
                                                    oEMRTransLineSplit.LinePrimaryDxDesc = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_DX1_CODE));
                                                }
                                                else if (j == 1)
                                                {
                                                    oEMRTransLineSplit.Dx2Code = strDX;
                                                    oEMRTransLineSplit.Dx2Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx2Ptr = true;
                                                }
                                                else if (j == 2)
                                                {
                                                    oEMRTransLineSplit.Dx3Code = strDX;
                                                    oEMRTransLineSplit.Dx3Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx3Ptr = true;
                                                }
                                                else if (j == 3)
                                                {
                                                    oEMRTransLineSplit.Dx4Code = strDX;
                                                    oEMRTransLineSplit.Dx4Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx4Ptr = true;
                                                }
                                                else if (j == 4)
                                                {
                                                    oEMRTransLineSplit.Dx5Code = strDX;
                                                    oEMRTransLineSplit.Dx5Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx5Ptr = true;
                                                }
                                                else if (j == 5)
                                                {
                                                    oEMRTransLineSplit.Dx6Code = strDX;
                                                    oEMRTransLineSplit.Dx6Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx6Ptr = true;
                                                }
                                                else if (j == 6)
                                                {
                                                    oEMRTransLineSplit.Dx7Code = strDX;
                                                    oEMRTransLineSplit.Dx7Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx7Ptr = true;
                                                }
                                                else if (j == 7)
                                                {
                                                    oEMRTransLineSplit.Dx8Code = strDX;
                                                    oEMRTransLineSplit.Dx8Description = strDXDesc;
                                                    oEMRTransLineSplit.Dx8Ptr = true;
                                                }

                                            }
                                        }

                                        #endregion

                                        #region "Setting MOD "

                                        string[] sMOD = null;
                                        string strMOD = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_MOD1_CODE));
                                        if (strMOD.Trim() != "")
                                        {
                                            sMOD = strMOD.Split(',');
                                        }
                                        if (sMOD != null && sMOD.Length > 0)
                                        {
                                            for (int j = 0; j < sMOD.Length; j++)
                                            {
                                                strMOD = Convert.ToString(sMOD[j]).Trim();

                                                if (j == 0)
                                                    oEMRTransLineSplit.Mod1Code = strMOD;
                                                else if (j == 1)
                                                    oEMRTransLineSplit.Mod2Code = strMOD;
                                                else if (j == 2)
                                                    oEMRTransLineSplit.Mod3Code = strMOD;
                                                else if (j == 3)
                                                    oEMRTransLineSplit.Mod4Code = strMOD;

                                            }
                                        }
                                        else
                                        {
                                            oEMRTransLineSplit.Mod1Code = "";
                                            oEMRTransLineSplit.Mod2Code = "";
                                            oEMRTransLineSplit.Mod3Code = "";
                                            oEMRTransLineSplit.Mod4Code = "";
                                        }
                                        #endregion

                                        oEMRTransLineSplit.TransactionLineId = _LineCount;
                                        oEMRTransLineSplit.EMRTreatmentLineNo = Convert.ToInt32(C1Dignosis.GetData(i, CPT_COL_LINE_NO));
                                        oEMRTransLinesSplit.Add(oEMRTransLineSplit);
                                        _LineCount = _LineCount + 1;
                                    }

                                    //}

                                }
                                else if ((C1Dignosis.GetCellCheck(i, CPT_COL_SELECT) == CheckEnum.Unchecked) && (C1Dignosis.GetCellCheck(i, CPT_ISBILLED) == CheckEnum.Checked))
                                {

                                    if ((C1Dignosis.GetData(i, CPT_COL_CPT_CODE) != null) && (Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE)) != ""))
                                    {
                                        bIsFullyPosted = false;
                                        UC_gloBillingTransactionLines.DeleteTransactionLine(Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE)), Convert.ToInt32(C1Dignosis.GetData(i, CPT_COL_LINE_NO)), false);

                                    }

                                }

                                if (Convert.ToString(C1Dignosis.GetData(i, CPT_COL_ClaimStatus)) == "No Post")
                                {

                                    DataRow _dr = _dtNoPostCharges.NewRow();
                                    _dr["nLineNo"] = Convert.ToInt32(C1Dignosis.GetData(i, CPT_COL_LINE_NO));
                                    _dr["sCPTCodes"] = Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE));
                                    _dtNoPostCharges.Rows.Add(_dr);
                                    _dtNoPostCharges.AcceptChanges();

                                    UC_gloBillingTransactionLines.DeleteTransactionLine(Convert.ToString(C1Dignosis.GetData(i, CPT_COL_CPT_CODE)), Convert.ToInt32(C1Dignosis.GetData(i, CPT_COL_LINE_NO)), false);
                                }


                            }//For

                            #endregion

                            #endregion
                        }

                        #region "To Assign to the Service Lines Based on Post and No Post"


                        TransactionLines oLinesAlreadyLoaded = null;
                        if (UC_gloBillingTransactionLines != null)
                        {
                            oLinesAlreadyLoaded = UC_gloBillingTransactionLines.GetLineTransactions();
                            UC_gloBillingTransactionLines.PatientID = this.PatientID;
                            UC_gloBillingTransactionLines.PatientProviderID = _EMRProviderId;
                            LoadDefaultBillingSettings();
                            UC_gloBillingTransactionLines.ReinitilizeControl();
                            UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                            SetFacilitySettingsData();
                            UC_gloBillingTransactionLines.TreatmentType = _nEMRTreatmentType;
                            UC_gloBillingTransactionLines.DeleteBlankCPTLines();
                        }

                        Int64 _ContactId = 0;

                        if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                        {
                            _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                        }
                        gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);

                        ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                        ogloBilling.Dispose();
                        ogloBilling = null;
                        UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                        UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;

                        string sMessageOut = gloCharges.ValidateEMRExamOnDXCPTWindow(UC_gloBillingTransactionLines.GetLineTransactions(), oEMRTransLinesSplit, _EMRExamID, _IsICD9Driven, _NoOfMaxDiagnosis, _NoOfMaxServiceLines, _nEMRTreatmentType, _dtLoadedCPTS, oLinesAlreadyLoaded);
                        if (sMessageOut.Trim() != String.Empty)
                        {
                            MessageBox.Show("EMR Treatment requires manual entry.                 " + Environment.NewLine + sMessageOut + Environment.NewLine + Environment.NewLine + "Treatment Details will be displayed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        
                        c1Dx.Rows.Count = 1;
                        
                        if (oLinesAlreadyLoaded != null)
                        {
                            oLinesAlreadyLoaded.Dispose();
                        }

                        _bDxFlag = true;
                        _bEMRTreatmentLoading = true;

                        try
                        {
                            this.c1Dx.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                            SetEMRTreatmentOnDXSave(_EMRExamID);

                        }
                        finally
                        {
                            this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);
                        }
                        #endregion

                        #region "Setting gloBilling Transaction User Control Values"

                        if (UC_gloBillingTransactionLines != null)
                        {


                            //DesignDxGrid();
                            SyncronizeDxGridWithServiceline();

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

                            #region "To Remove the Dx that are not used in Service Lines "

                            if (UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                            {
                                TransactionLines oLineTransactions = UC_gloBillingTransactionLines.GetLineTransactions();
                                StringBuilder sbDx = new StringBuilder();
                                gloCharges.RemoveUnusedDx(oLineTransactions, ref sbDx);
                                if (sbDx.Length > 0)
                                {

                                    for (int k = c1Dx.Rows.Count - 1; k >= 1; k--)
                                    {
                                        c1Dx.SetCellCheck(k, COL_DX_SELECT, CheckEnum.Unchecked);
                                    }

                                    for (int k = c1Dx.Rows.Count - 1; k >= 1; k--)
                                    {
                                        if (!sbDx.ToString().Contains(Convert.ToString(c1Dx.GetData(k, COL_DX_CODE))))
                                        {
                                            c1Dx.Rows.Remove(k);
                                        }
                                    }
                                    for (int i = 1; i <= c1Dx.Rows.Count - 1; i++)
                                    {

                                        if (i <= _NoOfMaxDiagnosis)
                                        { c1Dx.SetData(i, COL_DX_SELECT, true); }
                                    }

                                    sbDx.Clear();
                                }
                                if (oLineTransactions != null)
                                {
                                    oLineTransactions.Dispose();
                                    oLineTransactions = null;
                                }
                            }

                            #endregion

                            this.c1Dx.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Dx_CellChanged);

                            UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                            SetFacilitySettingsData();


                            #region " Set Fee Schedule "

                            UC_gloBillingTransactionLines.FeeScheduleID = 0;
                            UC_gloBillingTransactionLines.Fee_ScheduleID = 0;
                            UC_gloBillingTransactionLines._nContactID = _ContactId;
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
                                }
                                UC_gloBillingTransactionLines.FeeScheduleID = 0;
                                UC_gloBillingTransactionLines.Fee_ScheduleID = 0;
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


                        }
                        #endregion "Setting gloBilling Transaction User Control Values"
                        SelectPrimaryInsurance();
                        pnlExamCPTDX.SendToBack();
                    }
                }
            }
            catch (Exception exDxSave)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exDxSave.ToString(), false);
                pnlExamCPTDX.SendToBack();
            }
            finally
            {
                if (_dtNoPostCharges != null && _dtNoPostCharges.Rows.Count > 0)
                {
                    bIsFullyPosted = false;
                }
                else
                {
                    bIsFullyPosted = false;
                }
                _bDxFlag = false;
                _bEMRTreatmentLoading = false;
            }
           
        }

        private void tlb_NoPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (C1Dignosis != null && C1Dignosis.Rows.Count > 0)
                {
                    //gloOffice.gloC1FlexStyle.Style(C1Dignosis, false);

                    if (_IsICD9Driven && _nEMRTreatmentType != ExternalChargesType.HL7InboundCharges)
                    {
                        #region "ICD9 Driven"

                        if (C1Dignosis.RowSel > 0 && C1Dignosis.Rows.Count > 1)
                        {
                            if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_Units)) != "")
                            {
                                if (Convert.ToString((C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_ClaimStatus))) == "")
                                {
                                    for (int cntr = 1; cntr <= C1Dignosis.Rows.Count - 1; cntr++)
                                    {
                                      
                                            if (Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_Units)).Trim() != "")
                                            {
                                                if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_CPTCode)) == Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_CPTCode)))
                                                {
                                                    C1Dignosis.Rows[cntr].Style = csNoPost;
                                                    C1Dignosis.SetData(cntr, DX_Col_ClaimStatus, "No Post");
                                                }
                                            }
                                        
                                    }
                                }
                                else if (Convert.ToString((C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_ClaimStatus))) == "No Post")
                                {

                                    for (int cntr = 1; cntr <= C1Dignosis.Rows.Count - 1; cntr++)
                                    {

                                        if (Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_Units)).Trim() != "")
                                        {
                                            if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_CPTCode)) == Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_CPTCode)))
                                            {
                                                C1Dignosis.Rows[cntr].Style = csNoPostNormal;
                                                C1Dignosis.SetData(cntr, DX_Col_ClaimStatus, "");
                                            }
                                        }

                                    }
                                    
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region "CPT Driven"

                        if (C1Dignosis.RowSel > 0 && C1Dignosis.Rows.Count > 1)
                        {
                            if (Convert.ToString((C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus))) == "")
                            {

                                C1Dignosis.Rows[C1Dignosis.RowSel].Style = csNoPost;
                                C1Dignosis.SetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus, "No Post");
                            }
                            else if (Convert.ToString((C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus))) == "No Post")
                            {

                                C1Dignosis.Rows[C1Dignosis.RowSel].Style = csNoPostNormal;
                                C1Dignosis.SetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus, "");
                            }
                        }

                        //for (int i = 1; i <= C1Dignosis.Rows.Count - 1; i++)
                        //{

                        //    if ((C1Dignosis.GetCellCheck(i, CPT_COL_SELECT) == CheckEnum.Checked) && ((C1Dignosis.GetCellCheck(i, CPT_ISBILLED)) == CheckEnum.Unchecked))
                        //    {
                        //        //if (Convert.ToString((C1Dignosis.GetData(i, CPT_COL_ClaimStatus))) == "No Post")
                        //        //{
                        //        C1Dignosis.Rows[i].Style = csNoPost;
                        //        C1Dignosis.SetData(i, CPT_COL_ClaimStatus, "No Post");
                        //        //}

                        //    }
                        //    else 
                        //    if (Convert.ToString((C1Dignosis.GetData(i, CPT_COL_ClaimStatus))) == "Posted")
                        //    {

                        //        C1Dignosis.Rows[i].Style = csPosted;
                        //        C1Dignosis.SetData(i, CPT_COL_ClaimStatus, "Posted");
                        //    }
                        //    else if (C1Dignosis.GetCellCheck(i, CPT_COL_SELECT) == CheckEnum.Checked)
                        //    {

                        //        C1Dignosis.Rows[i].Style = csNoPost;
                        //        C1Dignosis.SetData(i, CPT_COL_ClaimStatus, "No Post");


                        //    }
                        //    else
                        //    {
                        //        C1Dignosis.Rows[i].Style = csNoPostNormal;
                        //        C1Dignosis.SetData(i, CPT_COL_ClaimStatus, "");
                        //    }
                        //}

                        #endregion
                    }

                }
            }
            catch (Exception exNoPost)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exNoPost.ToString(), false);
            }

        }

        private void tlb_Undo_Click(object sender, EventArgs e)
        {
            try
            {
                if (C1Dignosis != null && C1Dignosis.Rows.Count > 0)
                {

                    for (int i = 1; i <= C1Dignosis.Rows.Count - 1; i++)
                    {
                        if (_IsICD9Driven && _nEMRTreatmentType != ExternalChargesType.HL7InboundCharges)
                        {
                            if (Convert.ToString((C1Dignosis.GetData(i, DX_Col_ClaimStatus))) == "No Post")
                            {
                                C1Dignosis.Rows[i].Style = csNoPostNormal;
                                C1Dignosis.SetData(i, DX_Col_ClaimStatus, "");
                            }
                        }
                        else
                        {
                            if (Convert.ToString((C1Dignosis.GetData(i, CPT_COL_ClaimStatus))) == "No Post")
                            {
                                C1Dignosis.Rows[i].Style = csNoPostNormal;
                                C1Dignosis.SetData(i, CPT_COL_ClaimStatus, "");
                            }
                        }


                    }
                }
            }
            catch (Exception exUndo)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exUndo.ToString(), false);
            }
        }

        #endregion " Form Button Click Events "

        #region " Designer Events "

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnShowInsurance_MouseLeave(object sender, EventArgs e)
        {
            btnShowInsurance.BackgroundImage = global::gloBilling.Properties.Resources.UP;
            btnShowInsurance.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnShowInsurance_MouseHover(object sender, EventArgs e)
        {
            btnShowInsurance.BackgroundImage = global::gloBilling.Properties.Resources.UPHover;
            btnShowInsurance.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnHideInsurance_MouseHover(object sender, EventArgs e)
        {
            btnHideInsurance.BackgroundImage = global::gloBilling.Properties.Resources.DownHover;
            btnHideInsurance.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnHideInsurance_MouseLeave(object sender, EventArgs e)
        {
            btnHideInsurance.BackgroundImage = global::gloBilling.Properties.Resources.Down;
            btnHideInsurance.BackgroundImageLayout = ImageLayout.Center;
        }

       
        private void btnShowDiagnosis_MouseHover(object sender, EventArgs e)
        {
            btnShowDiagnosis.BackgroundImage = global::gloBilling.Properties.Resources.UPHover;
            btnShowDiagnosis.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnShowDiagnosis_MouseLeave(object sender, EventArgs e)
        {
            btnShowDiagnosis.BackgroundImage = global::gloBilling.Properties.Resources.UP;
            btnShowDiagnosis.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnHideDiagnosis_MouseHover(object sender, EventArgs e)
        {
            btnHideDiagnosis.BackgroundImage = global::gloBilling.Properties.Resources.DownHover;
            btnHideDiagnosis.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnHideDiagnosis_MouseLeave(object sender, EventArgs e)
        {
            btnHideDiagnosis.BackgroundImage = global::gloBilling.Properties.Resources.Down;
            btnHideDiagnosis.BackgroundImageLayout = ImageLayout.Center;
        }

        #endregion

        #region " Form Events"

        private void frmBillingTransaction_FormClosed(object sender, FormClosedEventArgs e)
        {
            //gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();

            try
            {


                if (_IsModifyed)
                {
                    //oSettings.WriteSettings_XML("Charges", "IsClaimModified", Boolean.TrueString);
                    gloGlobal.gloPMGlobal.ChargesIsClaimModified = true;
                }
                else
                {
                    //oSettings.WriteSettings_XML("Charges", "IsClaimModified", Boolean.FalseString);
                    gloGlobal.gloPMGlobal.ChargesIsClaimModified = false;
                }

                //Start of Disposing Spilit screen objects added by manoj jadhav on 20150202 V8040
                if (clsSplit_PatientCharges != null)
                {
                    clsSplit_PatientCharges.SaveControlDisplaySettings(gloGlobal.gloPMGlobal.UserID, "Charges");
                    clsSplit_PatientCharges.Dispose();
                    clsSplit_PatientCharges = null;
                }

                if (uiPanSplitScreen != null)
                {
                    uiPanSplitScreen.Dispose();
                    uiPanSplitScreen = null;
                }
                //End of Disposing Spilit screen objects added by manoj jadhav on 20150202 V8040

                this.Dispose();
                if (_oClaimHold != null)
                {
                    _oClaimHold.Dispose();
                }
                //if (boldFont != null)
                //{
                //    boldFont.Dispose();
                //    boldFont = null;
                //}
                //if (regularFont != null)
                //{
                //    regularFont.Dispose();
                //    regularFont = null;
                //}

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                //if (oSettings != null) { oSettings.Dispose(); }
            }
        }

     
        #endregion " Form Events"

        #region " Form Controls Events "

        private void btnExamCPTDXClose_Click(object sender, EventArgs e)
        {
            pnlExamCPTDX.SendToBack();
        }

        private void cmbFacility_MouseEnter(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbFacility.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbFacility.Items[cmbFacility.SelectedIndex])["sFacilityName"]), cmbFacility) >= cmbFacility.DropDownWidth - 20)
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

        private void cmbClaimCategory_MouseEnter(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbClaimCategory.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbClaimCategory.Items[cmbClaimCategory.SelectedIndex])["sDescription"]), cmbClaimCategory) >= cmbClaimCategory.DropDownWidth - 20)
                {
                    string txt = Convert.ToString(((DataRowView)cmbClaimCategory.Items[cmbClaimCategory.SelectedIndex])["sDescription"]);
                    if (tooltip_Billing.GetToolTip(cmbClaimCategory) != txt)
                    {
                        tooltip_Billing.SetToolTip(cmbClaimCategory, txt);
                    }
                }
                else
                {
                    this.tooltip_Billing.SetToolTip(cmbClaimCategory, "");
                }

            }
        }

        private void CmbAccidentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isTextBoxLoading = true;
            txt_WcAc.Text = "";
            txt_WcAc.Tag = "";
            _isTextBoxLoading = false;
            lblInitDate.Visible = false;
            mskInitTreatment.Visible = false;
            //pnlInitTreatment.Visible = false;

            switch (CmbAccidentType.Text.Trim())
            {
                case "Work":

                    //chkAutoClaim.Checked = false;
                    //chkOther.Checked = false;
                    lblInjuryDate.Visible = true;
                    lblOnsiteDate.Visible = false;
                    lblClaimDateHyphen.Visible = false;
                    lblOtherDate.Visible = false;
                    lblAccidentDate.Visible = false;

                    mskInjuryDate.Visible = true;
                    mskOnsiteDate.Visible = false;
                    cmbBox14DateQualifier.Visible = false;
                    mskOtherDate.Visible = false;
                    mskAccidentDate.Visible = false;

                    mskInjuryDate.Text = "";
                    mskOnsiteDate.Text = "";
                    mskOtherDate.Text = "";
                    mskAccidentDate.Text = "";
                    mskInitTreatment.Text = "";

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
                    for (int i = pnlInternalControl.Controls.Count - 1; i >= 0;  i--)
                    {
                        pnlInternalControl.Controls.RemoveAt(i);
                    }

                    //chkWorkersComp.Checked = false;
                    //chkOther.Checked = false;

                    lblInjuryDate.Visible = false;
                    lblOnsiteDate.Visible = false;
                    lblClaimDateHyphen.Visible = false;
                    lblOtherDate.Visible = false;
                    lblAccidentDate.Visible = true;

                    mskInjuryDate.Visible = false;
                    mskOnsiteDate.Visible = false;
                    cmbBox14DateQualifier.Visible = false;
                    mskOtherDate.Visible = false;
                    mskAccidentDate.Visible = true;

                    mskInjuryDate.Text = "";
                    mskOnsiteDate.Text = "";
                    mskOtherDate.Text = "";
                    mskAccidentDate.Text = "";
                    mskInitTreatment.Text = "";

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

                    //CloseInternalControl();

                    //chkAutoClaim.Checked = false;
                    //chkWorkersComp.Checked = false;

                    lblInjuryDate.Visible = false;
                    lblOnsiteDate.Visible = false;
                    lblClaimDateHyphen.Visible = false;
                    lblOtherDate.Visible = true;
                    lblAccidentDate.Visible = false;
                    mskInjuryDate.Visible = false;
                    mskOnsiteDate.Visible = false;
                    cmbBox14DateQualifier.Visible = false;
                    mskOtherDate.Visible = true;
                    mskAccidentDate.Visible = false;

                    mskInjuryDate.Text = "";
                    mskOnsiteDate.Text = "";
                    mskOtherDate.Text = "";
                    mskAccidentDate.Text = "";
                    mskInitTreatment.Text = "";

                    txt_WcAc.Visible = false;
                    lbl_State.Visible = false;
                    cmbState.Visible = false;
                    cmbState.Enabled = false;
                    cmbClaimNo.Visible = false;
                    lblClaim.Visible = false;

                    break;
                default:

                    //chkWorkersComp.Checked = false;
                    //chkAutoClaim.Checked = false;
                    //chkOther.Checked = false;

                    lblInjuryDate.Visible = false;
                    lblOnsiteDate.Visible = true;
                    lblClaimDateHyphen.Visible = true;
                    lblOtherDate.Visible = false;
                    lblAccidentDate.Visible = false;

                    mskInjuryDate.Visible = false;
                    mskOnsiteDate.Visible = true;
                    mskOnsiteDate.BringToFront();
                    cmbBox14DateQualifier.Visible = true;
                    mskOtherDate.Visible = false;
                    mskAccidentDate.Visible = false;

                    mskInjuryDate.Text = "";
                    mskOnsiteDate.Text = "";
                    mskOtherDate.Text = "";
                    mskAccidentDate.Text = "";

                    txt_WcAc.Visible = false;
                    lbl_State.Visible = false;
                    cmbState.Visible = false;
                    cmbState.Enabled = false;
                    cmbClaimNo.Visible = false;
                    lblClaim.Visible = false;

                    if (bShowInitialTreatmentDate)
                    {
                        lblInitDate.Visible = false;
                        mskInitTreatment.Visible = false;
                    }

                    break;
            }
        }

        private void btnModifyGlobalPeriod_Click(object sender, EventArgs e)
        {
            try
            {
                frmSetupModifyGlobalPeriod ofrmSetupGlobalPeriod = new frmSetupModifyGlobalPeriod(Convert.ToInt64(lblGlobalPeriodAlert.Tag));
                ofrmSetupGlobalPeriod.ShowDialog(this);
                ofrmSetupGlobalPeriod.Dispose();
                SetLastGlobalPeriods();
                CheckForEPSDTEnabled();
                IsAnesthesiaEnabled();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnAdd_Cases_Click(object sender, EventArgs e)
        {
            PerformCaseAdd();
        }

        private void btnRemove_Cases_Click(object sender, EventArgs e)
        {
            PerformRemoveAction();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string strSearch = "";
            string sFilter = "";
            try
            {
                #region " Search "

                if (chkGeneralSearch.Checked == false)
                {
                    if (lblSearch.Tag != null)
                    {
                        DataView dv = (DataView)c1PatientEMRExams.DataSource;
                        if (dv != null)
                        {
                            strSearch = txtSearch.Text.Trim();
                            int colIndex = Convert.ToInt32(lblSearch.Tag);
                            strSearch = strSearch.Replace("'", "");

                            if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            {
                                if (dv.Table.Columns["DOS"].ColumnName==dv.Table.Columns[colIndex].ColumnName)
                                    dv.RowFilter = "CONVERT(" + dv.Table.Columns[colIndex].ColumnName + ",System.String) Like '%" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                                else
                                    dv.RowFilter = dv.Table.Columns[colIndex].ColumnName + " Like '%" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                            }
                            else
                            {
                                if (dv.Table.Columns["DOS"].ColumnName == dv.Table.Columns[colIndex].ColumnName)
                                    dv.RowFilter = "CONVERT(" + dv.Table.Columns[colIndex].ColumnName + ",System.String) Like '" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                                else
                                    dv.RowFilter = dv.Table.Columns[colIndex].ColumnName + " Like '" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                            }
                            c1PatientEMRExams.DataSource = dv;
                        }
                    }
                }
                else
                {
                    // Mahesh nawal 02192011 Resolved Search Problem
                    DataView dv = (DataView)c1PatientEMRExams.DataSource;
                    if (dv != null)
                    {

                        strSearch = txtSearch.Text.Trim();
                        string[] strSearchArray = null;
                        //  strSearch = strSearch.Replace("'", "").Replace(",", "").Replace("%", "").Replace("*", "");
                        strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                        if (strSearch.Length > 1)
                        {
                            string str = strSearch.Substring(1).Replace("%", "");
                            strSearch = strSearch.Substring(0, 1) + str;
                        }
                        if (strSearch.Trim() != "")
                        {
                            strSearchArray = strSearch.Split(',');
                        }

                        if (lblSearch.Tag != null)
                        {

                            // strSearch = strSearch.Replace("%", "").Replace("*", "");

                            if (strSearch.Trim() != "")
                            {


                                if (strSearchArray.Length == 1)
                                {


                                    dv.RowFilter = dv.Table.Columns["ExamName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                        dv.Table.Columns["TemplateName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                        // Bugid- 49716 DOS column  giving error for datetime filtering so it was converted to string 
                                    "CONVERT(" + dv.Table.Columns["DOS"].ColumnName.ToString() + ",System.String)  Like '" + strSearch + "%' OR " +
                                    "CONVERT(" + dv.Table.Columns["SearchFinishDate"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                     "CONVERT(" + dv.Table.Columns["SearchDOB"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["Code"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["MN"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["AccountNote"].ColumnName + " Like '" + strSearch + "%' ";
                                }
                                else
                                {
                                    for (int j = 0; j < strSearchArray.Length; j++)
                                    {
                                        strSearch = strSearchArray[j];
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (j == 0)
                                            {

                                                sFilter = " ( " + dv.Table.Columns["ExamName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    dv.Table.Columns["TemplateName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    // Bugid- 49716 DOS column  giving error for datetime filtering so it was converted to string 
                                                " CONVERT(" + dv.Table.Columns["DOS"].ColumnName.ToString() + ",System.String)  Like '" + strSearch + "%' OR " +
                                                 " CONVERT(" + dv.Table.Columns["SearchFinishDate"].ColumnName.ToString() + ",System.String)  Like '" + strSearch + "%' OR " +
                                                 "CONVERT(" + dv.Table.Columns["SearchDOB"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["Code"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["MN"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["AccountNote"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%' )";
                                            }
                                            else
                                            {

                                                sFilter = sFilter + " AND (" + dv.Table.Columns["ExamName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    dv.Table.Columns["TemplateName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    // Bugid- 49716 DOS column  giving error for datetime filtering so it was converted to string 
                                                " CONVERT(" + dv.Table.Columns["DOS"].ColumnName.ToString() + ",System.String)  Like '" + strSearch + "%' OR " +
                                                " CONVERT(" + dv.Table.Columns["SearchFinishDate"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                                "CONVERT(" + dv.Table.Columns["SearchDOB"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["Code"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["MN"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 dv.Table.Columns["AccountNote"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%' )";
                                            }
                                        }

                                    }
                                    dv.RowFilter = sFilter;
                                }
                            }
                            else
                            {
                                dv.RowFilter = "";
                            }

                            c1PatientEMRExams.DataSource = dv;
                        }
                    }
                }

                #endregion " Search "

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
       
        private void tls_btnFNFCharges_Click(object sender, EventArgs e)
        {
            if (tls_btnFNFCharges.Tag.ToString() == "Facility")
            {
                SetNonFacilityFee();
            }
            else if (tls_btnFNFCharges.Tag.ToString() == "NonFacility")
            {
                SetFacilityFee();
            }
        }

        private void chkFacilityFeeSchedule_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false)
            {
                bool _autosort = false;
                if (chkFacilityFeeSchedule.Checked == true)
                {
                    if (UC_gloBillingTransactionLines.AutoSort == true)
                    {
                        _autosort = true;
                        UC_gloBillingTransactionLines.AutoSort = false;
                    }
                    chkNonFacilityCharges.Checked = false;
                    if (UC_gloBillingTransactionLines != null)
                    {
                        //UC_gloBillingTransactionLines.IsFaclityFee = true;                        
                        UC_gloBillingTransactionLines.DefaultChargesType = FacilityType.Facility;
                        UC_gloBillingTransactionLines.SetFNFLineCharges();
                    }
                    UC_gloBillingTransactionLines.AutoSort = _autosort;
                    if (_autosort == true)
                    {
                        UC_gloBillingTransactionLines.SortControl();
                    }
                }
                else
                {
                    chkNonFacilityCharges.Checked = true;                   
                }
            }
        }

        private void chkNonFacilityCharges_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false)
            {
                bool _autosort = false;
                if (chkNonFacilityCharges.Checked == true)
                {
                    if (UC_gloBillingTransactionLines.AutoSort == true)
                    {
                        _autosort = true;
                        UC_gloBillingTransactionLines.AutoSort = false;
                    }



                    chkFacilityFeeSchedule.Checked = false;
                    if (UC_gloBillingTransactionLines != null)
                    {
                        //UC_gloBillingTransactionLines.IsFaclityFee = false;
                        UC_gloBillingTransactionLines.DefaultChargesType = FacilityType.NonFacility;
                        UC_gloBillingTransactionLines.SetFNFLineCharges();
                    }
                    UC_gloBillingTransactionLines.AutoSort = _autosort;
                    if (_autosort == true)
                    {
                        UC_gloBillingTransactionLines.SortControl();
                    }
                }
                else
                {
                    chkFacilityFeeSchedule.Checked = true;
                }

            }
        }

        private void btnAdd_Referral_Click(object sender, EventArgs e)
        {
            Int64 _currentPatientId = 0;
            try
            {

                gloUserRights.ClsgloUserRights ObjUserRights = new gloUserRights.ClsgloUserRights(_DatabaseConnectionString);
                ObjUserRights.CheckForUserRights(_UserName);
                if (ObjUserRights.ModifyPatient == true)
                {
                    if (this.PatientID > 0)
                    {
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_DatabaseConnectionString);
                        ogloPatient.ShowPatientRegistration(this.PatientID, gloPatient.ModifyPatientDetailType.Referral, out _currentPatientId,this);
                        ReloadPatientRefferals(_currentPatientId);
                        //20100503 - Hot Fix Rendering Provider Issue 
                        chk_SameasBillingProvider_CheckedChanged(null, null);
                        ogloPatient.Dispose();
                        ogloPatient = null;
                    }
                }

                //Added By Debasish Das (5061)
                if (cmbReferralProvider.Items.Count == 2)
                {
                    cmbReferralProvider.SelectedIndex = 1;
                }
                ObjUserRights.Dispose();
                ObjUserRights = null;
                //***
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
            }
        }


        private void chkOutSideLab_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOutSideLab.Checked == true)
            {
                txtOutSideLabCharges.Enabled = true;
                txtOutSideLabCharges.Focus();
                txtOutSideLabCharges.Text = "0.00";
                txtOutSideLabCharges.Select();
            }
            else if (chkOutSideLab.Checked == false)
            {
                txtOutSideLabCharges.Enabled = false;
                txtOutSideLabCharges.Text = "0.00";

            }
        }

        private void dtpClaimDate_ValueChanged(object sender, EventArgs e)
        {
            //SetBatchNumber();
        }

        private void cmbBillingProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false)
            {
                SetBillingProviderDropDownSelectionChangeData();
            }
        }

        private void cmbFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false)
            {
                SetFacilityDropDownSelectionChangeData();

                #region " Tool tip "

                combo = (ComboBox)sender;
                if (cmbFacility.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbFacility.Items[cmbFacility.SelectedIndex])["sFacilityName"]), cmbFacility) >= cmbFacility.DropDownWidth - 20)
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

                #endregion " Tool tip "
            }
        }

        private void cmbClaimCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false)
            {

                #region " Tool tip "

                combo = (ComboBox)sender;
                if (cmbClaimCategory.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbClaimCategory.Items[cmbClaimCategory.SelectedIndex])["sDescription"]), cmbClaimCategory) >= cmbClaimCategory.DropDownWidth - 20)
                    {
                        string txt = Convert.ToString(((DataRowView)cmbClaimCategory.Items[cmbClaimCategory.SelectedIndex])["sDescription"]);
                        if (tooltip_Billing.GetToolTip(cmbClaimCategory) != txt)
                        {
                            tooltip_Billing.SetToolTip(cmbClaimCategory, txt);
                        }
                    }
                    else
                    {
                        this.tooltip_Billing.SetToolTip(cmbClaimCategory, "");
                    }

                }

                #endregion " Tool tip "
            }
        }
      
        private void cmbBillingProvider_MouseHover(object sender, EventArgs e)
        {

        }

        private void txtOutSideLabCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
            else
            {
                if (txtOutSideLabCharges.SelectionStart > txtOutSideLabCharges.Text.IndexOf("."))
                {
                    if (txtOutSideLabCharges.Text.Contains(".") == true)
                    {
                        if (txtOutSideLabCharges.Text.Substring(txtOutSideLabCharges.Text.IndexOf(".") + 1, txtOutSideLabCharges.Text.Length - (txtOutSideLabCharges.Text.IndexOf(".") + 1)).Length == 2)
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            if (e.KeyChar == Convert.ToChar(46) && txtOutSideLabCharges.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void chkFeeSchedule_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFeeSchedule.Checked == true)
            {
                //cmbFeeSchedule.SelectedIndex = -1;
                cmbFeeSchedule.Enabled = true;
                cmbFeeSchedule.Focus();
                if (cmbFeeSchedule.SelectedValue != null)
                {
                    UC_gloBillingTransactionLines.IsFeeSchedule = true;
                    UC_gloBillingTransactionLines.FeeScheduleID = Convert.ToInt64(cmbFeeSchedule.SelectedValue.ToString());
                    UC_gloBillingTransactionLines.SetFNFLineCharges();
                }
            }
            else
            {
                //cmbFeeSchedule.SelectedIndex = -1;
                cmbFeeSchedule.Enabled = false;
                UC_gloBillingTransactionLines.IsFeeSchedule = false;
                UC_gloBillingTransactionLines.FeeScheduleID = 0;

                if (_FeeScheduleID > 0)
                {
                    UC_gloBillingTransactionLines.FeeScheduleID = _FeeScheduleID;
                    cmbFeeSchedule.SelectedValue = _FeeScheduleID;
                }
                else
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
                UC_gloBillingTransactionLines.SetFNFLineCharges();
            }
        }

        private void cmbFeeSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbFeeSchedule.SelectedIndex >= 0 && cmbFeeSchedule.SelectedValue != null)

            if (_IsFormLoading == false)
            {

                if (chkNonFacilityCharges.Checked == true)
                {
                    UC_gloBillingTransactionLines.DefaultChargesType = FacilityType.NonFacility;
                }
                if (chkFacilityFeeSchedule.Checked == true)
                {
                    UC_gloBillingTransactionLines.DefaultChargesType = FacilityType.Facility;
                }


                if (cmbFeeSchedule != null && cmbFeeSchedule.Items != null && cmbFeeSchedule.Items.Count > 0 && cmbFeeSchedule.SelectedIndex >= 0)
                {
                    UC_gloBillingTransactionLines.FeeScheduleID = Convert.ToInt64(cmbFeeSchedule.SelectedValue);
                    //UC_gloBillingTransactionLines.SetFNFCharges();


                    bool _autosort = false;
                    if (UC_gloBillingTransactionLines.AutoSort == true)
                    {
                        _autosort = true;
                        UC_gloBillingTransactionLines.AutoSort = false;
                    }
                    if (UC_gloBillingTransactionLines != null)
                    {
                        UC_gloBillingTransactionLines.SetFNFLineCharges();
                    }
                    UC_gloBillingTransactionLines.AutoSort = _autosort;

                    if (_autosort == true)
                    {
                        UC_gloBillingTransactionLines.SortControl();
                    }


                }
                else
                {
                    UC_gloBillingTransactionLines.FeeScheduleID = 0;
                }

            }
        }

        private void btnSelectChargeTry_Click(object sender, EventArgs e)
        {
            SelectChargeTray();
        }

        private void chk_SameasBillingProvider_CheckedChanged(object sender, EventArgs e)
        {


            SetRefferingAsBillingProvider();
            if (cmbReferralProvider.Items.Count == 2)
            {
                cmbReferralProvider.SelectedIndex = 1;
            }  

          
        }

        private void cmbFacility_MouseMove(object sender, MouseEventArgs e)
        {
          

        }

        #endregion " Form Controls Events "

        #region " From Get Instance Methods "
        public static frmBillingTransaction GetInstance(Int64 PatientID)
        {
            try
            {
                if (frm != null)
                {
                    //frm.Dispose();
                    frm.Show();
                    frm.BringToFront();
                }
                else
                {
                    frm = new frmBillingTransaction(PatientID);
                }
            }
            finally
            {

            }
            return frm;
        }

      
        public static frmBillingTransaction GetInstance_CopyClaim(Int64 PatientID)
        {
            frm_Copy = new frmBillingTransaction(PatientID);
            return frm_Copy;
        }

        #endregion " From Get Instance Methods "

        #region "Form Dispose Methods"
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                // Dispose managed resources. 
                if ((components != null))
                {
                    components.Dispose();
                }
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    try
                    {

                        System.Windows.Forms.ContextMenuStrip[] cntmenuControls = { cmdInsurance, cmnu_DxNoPost };
                        System.Windows.Forms.Control[] cntControls = { cmdInsurance, cmnu_DxNoPost };
                        if (cntmenuControls != null)
                        {
                            if (cntmenuControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntmenuControls);

                            }
                        }
                        if (cntControls != null)
                        {
                            if (cntControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                            }
                        }
                        
                       
                    }
                    catch
                    {
                    }
                    

                  
                    if (oToolTip != null)
                    {
                        oToolTip.Dispose();
                        oToolTip = null;
                    }
                    //frm = Nothing 
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed. 

                // Note that this is not thread safe. 
                // Another thread could start disposing the object 
                // after the managed resources are disposed, 
                // but before the disposed flag is set to true. 
                // If thread safety is necessary, it must be 
                // implemented by the client. 
            }

            if (bIsCopiedClaim)
            {
                frm_Copy = null;
            }
            else
            {
                frm = null;
            }
            this.blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }
        #endregion "Form Dispose Methods"

        #region " Date Validation "

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
            catch (FormatException)
            {
                Success = false; // If this line is reached, an exception was thrown

            }
            return Success;
        }

        private void mskDate_Validating(object sender, CancelEventArgs e)
        {
            try
            { 
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                _IsValidDate = true;
                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Specifies that the Date is InValid
                            _IsValidDate = false;
                            e.Cancel = true;
                        }
                        else if (mskClaimDate.MaskCompleted == true && ((MaskedTextBox)sender).Name == mskClaimDate.Name)
                        {
                            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, "");
                            {
                               
                                if (IsValidDate(mskClaimDate.Text) == true)
                                { 
                                    #region " Set Close Date to added Line "
                                    if (UC_gloBillingTransactionLines != null)
                                    {
                                        if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                                        {
                                            if (Convert.ToString(UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE)) == "")
                                            {
                                                if (_sLastServiceLineDOS.Trim() == string.Empty)
                                                {
                                                    //Bug #53622: 7040 - gloPM - Missing Charges - DOS changes to system Date on Charges screen invoked through Missing charges
                                                    //Description: DOS changes to close date if present or to system date if open from missing charges so added condition
                                                    if (_IsOpenForExternal != true)
                                                    {
                                                        if (IsAppointmentPresent == true && DefaultApptDos == true)
                                                        {
                                                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(sSelectedAppointmentDOS));
                                                        }
                                                        else
                                                        {
                                                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(mskClaimDate.Text));
                                                        }
                                                    }
                                                    
                                                }
                                                else
                                                {
                                                    //Bug #53622: 7040 - gloPM - Missing Charges - DOS changes to system Date on Charges screen invoked through Missing charges
                                                    //Description: DOS changes to close date if present or to system date if open from missing charges so added condition
                                                    if (_IsOpenForExternal != true)
                                                    {
                                                        if (IsAppointmentPresent == true && DefaultApptDos == true)
                                                        {
                                                            UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(sSelectedAppointmentDOS));
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

                                    CheckForPatientCases();
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
                            ogloBilling.Dispose();
                        }

                    }
                    else if (((MaskedTextBox)sender).Name == mskClaimDate.Name)
                    {
                        #region " Set Close Date to addedd Line "
                        if (UC_gloBillingTransactionLines != null)
                        {
                            if (UC_gloBillingTransactionLines.GetLinesCount() == 2)
                            {
                                if (Convert.ToString(UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, COL_CPT_CODE)) == "")
                                {
                                    if (_sLastServiceLineDOS.Trim() == string.Empty)
                                    {
                                        //Bug #53622: 7040 - gloPM - Missing Charges - DOS changes to system Date on Charges screen invoked through Missing charges
                                        //Description: DOS changes to close date if present or to system date if open from missing charges so added condition
                                        if (_IsOpenForExternal != true)
                                        {
                                            if (IsAppointmentPresent == true && DefaultApptDos == true)
                                            {
                                                UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(sSelectedAppointmentDOS));
                                            }
                                            else
                                            {
                                                UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, DateTime.Today);
                                            }
                                        }
                                        
                                    }
                                    else
                                    {
                                        //Bug #53622: 7040 - gloPM - Missing Charges - DOS changes to system Date on Charges screen invoked through Missing charges
                                        //Description: DOS changes to close date if present or to system date if open from missing charges so added condition
                                        if (_IsOpenForExternal != true)
                                        {
                                            if (IsAppointmentPresent == true && DefaultApptDos == true)
                                            {
                                                UC_gloBillingTransactionLines.SetServiceLineDate(UC_gloBillingTransactionLines.CurrentTransactionLine, Convert.ToDateTime(sSelectedAppointmentDOS));
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
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Specifies that the Date is InValid
                _IsValidDate = false;
                e.Cancel = true;
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


        #endregion " Date Validation "

        #region " WC  Text Events "

        private void txt_WcAc_MouseHover(object sender, EventArgs e)
        {
            //System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            if (!this.Focused)
            {
                if (Convert.ToString(txt_WcAc.Tag).Trim() != "-1848750000") // To validate if date is entered and Wc is entered and hit save&cls
                {
                    ToolTip1.SetToolTip(this.txt_WcAc, Convert.ToString(txt_WcAc.Tag));
                }
            }
        }
       
        #endregion

        # region ToolTip
        //Event for showing the ToolTip on DropList 
        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {

            if (sender is ComboBox)
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
                        //this.tooltip_Billing.SetToolTip(combo,"");
                    }
                    e.DrawFocusRectangle();
                }
            }            
        }

        //Function For Calculating the Lenghth of the Items in the combo box
        private int getWidthofListItems(string _text, ComboBox combo)
        {
            int width = 0;
            if (frm != null)
            {

                Graphics g = frm.CreateGraphics();
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            return width;
        }

        #endregion

        #region " EMR Exam CPT DX Fill "

        private void SetDXGrid()
        {
            try
            {
                C1Dignosis.Rows.Count = 1;
                gloC1FlexStyle.Style(C1Dignosis);

                float _TotalWidth = C1Dignosis.Width - 5;
                C1Dignosis.Cols.Fixed = 0;
                C1Dignosis.Rows.Fixed = 1;
                C1Dignosis.Cols.Count = DX_Col_Count;
                C1Dignosis.Width = 500;
                C1Dignosis.AllowResizing = AllowResizingEnum.None;
                C1Dignosis.AllowDragging = AllowDraggingEnum.None;


                //for Select
                C1Dignosis.Cols[DX_Col_Select].Width = 0;
                C1Dignosis.SetData(0, DX_Col_Select, "");
                C1Dignosis.Cols[DX_Col_Select].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;
                C1Dignosis.Cols[DX_Col_Select].DataType = typeof(System.Boolean);
                C1Dignosis.Cols[DX_Col_Select].Visible = false;
                C1Dignosis.Cols[DX_Col_Select].AllowEditing = false;

                //for ICD9
                C1Dignosis.Cols[DX_Col_ICD9Code_Description].Width = 540;
                C1Dignosis.SetData(0, DX_Col_ICD9Code_Description, "ICD9");
                C1Dignosis.Cols[DX_Col_ICD9Code_Description].AllowEditing = true;
                C1Dignosis.Cols[DX_Col_ICD9Code_Description].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;



                C1Dignosis.Cols[DX_Col_ICD9Code].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_ICD9Code, "ICD9CODE");
                C1Dignosis.Cols[DX_Col_ICD9Code].AllowEditing = true;
                C1Dignosis.Cols[DX_Col_ICD9Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                C1Dignosis.Cols[DX_Col_ICD9Desc].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_ICD9Desc, "ICD9Description");
                C1Dignosis.Cols[DX_Col_ICD9Desc].AllowEditing = true;
                C1Dignosis.Cols[DX_Col_ICD9Desc].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                C1Dignosis.Cols[DX_Col_CPTCode].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_CPTCode, "CPTCODE");
                C1Dignosis.Cols[DX_Col_CPTCode].AllowEditing = true;
                C1Dignosis.Cols[DX_Col_CPTCode].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                C1Dignosis.Cols[DX_Col_CPTDesc].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_CPTDesc, "CPTDescription");
                C1Dignosis.Cols[DX_Col_CPTDesc].AllowEditing = true;
                C1Dignosis.Cols[DX_Col_CPTDesc].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                C1Dignosis.Cols[DX_Col_ModCode].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_ModCode, "MODCODE");
                C1Dignosis.Cols[DX_Col_ModCode].AllowEditing = true;
                C1Dignosis.Cols[DX_Col_ModCode].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                C1Dignosis.Cols[DX_Col_ModDesc].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_ModDesc, "MODDescription");
                C1Dignosis.Cols[DX_Col_ModDesc].AllowEditing = true;
                C1Dignosis.Cols[DX_Col_ModDesc].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                C1Dignosis.Cols[DX_Col_Units].Width = 40;
                C1Dignosis.SetData(0, DX_Col_Units, "Units");
                C1Dignosis.Cols[DX_Col_Units].DataType = typeof(System.Decimal);
                C1Dignosis.Cols[DX_Col_Units].AllowEditing = false;
                C1Dignosis.Cols[DX_Col_Units].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                C1Dignosis.Cols[DX_Col_Units].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //C1Dignosis.Cols[DX_Col_Units].Format = "#############0.####";

                C1Dignosis.Cols[DX_Col_ICD9Count].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_ICD9Count, "ICD9 Count");
                C1Dignosis.Cols[DX_Col_ICD9Count].DataType = typeof(System.Int64);
                C1Dignosis.Cols[DX_Col_ICD9Count].AllowEditing = false;
                C1Dignosis.Cols[DX_Col_ICD9Count].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                C1Dignosis.Cols[DX_Col_CPTCount].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_CPTCount, "CPT Count");
                C1Dignosis.Cols[DX_Col_CPTCount].DataType = typeof(System.Int64);
                C1Dignosis.Cols[DX_Col_CPTCount].AllowEditing = false;
                C1Dignosis.Cols[DX_Col_CPTCount].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                C1Dignosis.Cols[DX_Col_ModCount].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_ModCount, "Mod Count");
                C1Dignosis.Cols[DX_Col_ModCount].DataType = typeof(System.Int64);
                C1Dignosis.Cols[DX_Col_ModCount].AllowEditing = false;
                C1Dignosis.Cols[DX_Col_ModCount].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;


                C1Dignosis.Cols[DX_Col_CPTLineNo].Width = Convert.ToInt32(_TotalWidth * 0);
                C1Dignosis.SetData(0, DX_Col_CPTLineNo, "Line No");
                C1Dignosis.Cols[DX_Col_CPTLineNo].DataType = typeof(System.Int64);
                C1Dignosis.Cols[DX_Col_CPTLineNo].AllowEditing = false;
                C1Dignosis.Cols[DX_Col_CPTLineNo].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;


                C1Dignosis.Cols[DX_Col_ClaimStatus].Width =5;
                C1Dignosis.SetData(0, DX_Col_ClaimStatus, "Status");
                C1Dignosis.Cols[DX_Col_ClaimStatus].AllowEditing = false;
                C1Dignosis.Cols[DX_Col_ClaimStatus].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Dignosis.Cols[DX_Col_ClaimStatus].Visible = true;
                C1Dignosis.Cols[DX_Col_Units].Visible = true;
                C1Dignosis.ExtendLastCol = true;
                C1Dignosis.AllowEditing = true;

                C1Dignosis.ShowCellLabels = true;


                #region "Cell Styles For No Post "

               // csNoPost = C1Dignosis.Styles.Add("csNoPost");
                try
                {
                    if (C1Dignosis.Styles.Contains("csNoPost"))
                    {
                        csNoPost = C1Dignosis.Styles["csNoPost"];
                    }
                    else
                    {
                        csNoPost = C1Dignosis.Styles.Add("csNoPost");
                        csNoPost.BackColor = Color.LightGray;
                        csNoPost.ForeColor = Color.Maroon;
                        csNoPost.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, (byte)0);

                    }

                }
                catch
                {
                    csNoPost = C1Dignosis.Styles.Add("csNoPost");
                    csNoPost.BackColor = Color.LightGray;
                    csNoPost.ForeColor = Color.Maroon;
                    csNoPost.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, (byte)0);

                }

             //   csPosted = C1Dignosis.Styles.Add("csPosted");
                try
                {
                    if (C1Dignosis.Styles.Contains("csPosted"))
                    {
                        csPosted = C1Dignosis.Styles["csPosted"];
                    }
                    else
                    {
                        csPosted = C1Dignosis.Styles.Add("csPosted");
                        csPosted.BackColor = Color.FromArgb(231, 231, 231); //Color.FromArgb(252, 243, 220);
                        csPosted.ForeColor = System.Drawing.Color.Black;
                        csPosted.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);

                    }

                }
                catch
                {
                    csPosted = C1Dignosis.Styles.Add("csPosted");
                    csPosted.BackColor = Color.FromArgb(231, 231, 231); //Color.FromArgb(252, 243, 220);
                    csPosted.ForeColor = System.Drawing.Color.Black;
                    csPosted.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);

                }
 

            //    csNoPostNormal = C1Dignosis.Styles.Add("cs_PostNormal");
                try
                {
                    if (C1Dignosis.Styles.Contains("cs_PostNormal"))
                    {
                        csNoPostNormal = C1Dignosis.Styles["cs_PostNormal"];
                    }
                    else
                    {
                        csNoPostNormal = C1Dignosis.Styles.Add("cs_PostNormal");
                        // Normal Style
                        csNoPostNormal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
                        csNoPostNormal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
                        csNoPostNormal.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
                        csNoPostNormal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

                        // Alternative Style
                        csNoPostNormal.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
                        csNoPostNormal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
                        csNoPostNormal.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                        csNoPostNormal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

                    }

                }
                catch
                {
                    csNoPostNormal = C1Dignosis.Styles.Add("cs_PostNormal");
                    // Normal Style
                    csNoPostNormal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
                    csNoPostNormal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
                    csNoPostNormal.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
                    csNoPostNormal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

                    // Alternative Style
                    csNoPostNormal.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
                    csNoPostNormal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
                    csNoPostNormal.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                    csNoPostNormal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

                }
 

           

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetCPTGrid()
        {
            try
            {
                gloC1FlexStyle.Style(C1Dignosis);

                C1Dignosis.Clear();
                C1Dignosis.Width = 525;
                C1Dignosis.Rows.Count = 1;
                C1Dignosis.Rows.Fixed = 1;
                C1Dignosis.Cols.Count = CPT_COL_COUNT;
                C1Dignosis.Cols.Fixed = 1;

                C1Dignosis.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
                C1Dignosis.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
                C1Dignosis.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

                C1Dignosis.Cols[CPT_COL_DX1_CODE].AllowResizing = true;

                C1Dignosis.Cols[CPT_COL_DOS].DataType = Type.GetType("System.DateTime");
                C1Dignosis.Cols[CPT_COL_UNIT].DataType = Type.GetType("System.Decimal");
                C1Dignosis.Cols[CPT_COL_SELECT].DataType = Type.GetType("System.Boolean");
                //C1Dignosis.Cols[CPT_COL_UNIT].Format = "#############0.####";

                //' WIDTH ''
                C1Dignosis.Cols[CPT_COL_LINE_NO].Width = 0;
                C1Dignosis.Cols[CPT_COL_LINE_NO_Display].Width = 30;
                C1Dignosis.Cols[CPT_COL_DOS].Width = 80;
                C1Dignosis.Cols[CPT_COL_CPT_ID].Width = 0;
                C1Dignosis.Cols[CPT_COL_CPT_CODE].Width = 55;
                C1Dignosis.Cols[CPT_COL_CPT_DESC].Width = 60;
                C1Dignosis.Cols[CPT_COL_DX1_ID].Width = 0;
                C1Dignosis.Cols[CPT_COL_DX1_CODE].Width = 250;
                C1Dignosis.Cols[CPT_COL_DX1_DESC].Width = 0;
                C1Dignosis.Cols[CPT_COL_MOD1_ID].Width = 0;
                C1Dignosis.Cols[CPT_COL_MOD1_CODE].Width = 90;
                C1Dignosis.Cols[CPT_COL_MOD1_DESC].Width = 0;
                C1Dignosis.Cols[CPT_COL_UNIT].Width = 40;
                C1Dignosis.Cols[CPT_ISBILLED].Width = 0;
                C1Dignosis.Cols[CPT_COL_ClaimStatus].Width = 8;

                C1Dignosis.SetData(0, CPT_COL_SELECT, "");
                C1Dignosis.SetData(0, CPT_COL_LINE_NO, "");
                C1Dignosis.SetData(0, CPT_COL_LINE_NO_Display, "No.");
                C1Dignosis.SetData(0, CPT_COL_DOS, "DOS");
                C1Dignosis.SetData(0, CPT_COL_CPT_ID, "CPT ID");
                C1Dignosis.SetData(0, CPT_COL_CPT_CODE, "CPT");
                C1Dignosis.SetData(0, CPT_COL_CPT_DESC, "CPTDesc");
                C1Dignosis.SetData(0, CPT_COL_DX1_ID, "DxID");
                C1Dignosis.SetData(0, CPT_COL_DX1_CODE, "Diagnosis");
                C1Dignosis.SetData(0, CPT_COL_DX1_DESC, "DxDesc");
                C1Dignosis.SetData(0, CPT_COL_MOD1_ID, "ModID");
                C1Dignosis.SetData(0, CPT_COL_MOD1_CODE, "Modifiers");
                C1Dignosis.SetData(0, CPT_COL_MOD1_DESC, "ModDesc");
                C1Dignosis.SetData(0, CPT_COL_UNIT, "Units");
                C1Dignosis.SetData(0, CPT_COL_ClaimStatus, "Status");


                //' HIDE ALL ID COLS ''
                C1Dignosis.Cols[CPT_COL_LINE_NO].Visible = false;
                C1Dignosis.Cols[CPT_COL_LINE_NO_Display].Visible = true;
                C1Dignosis.Cols[CPT_COL_CPT_ID].Visible = false;
                C1Dignosis.Cols[CPT_COL_DX1_ID].Visible = false;
                C1Dignosis.Cols[CPT_COL_MOD1_ID].Visible = false;

                //' HIDE ALL DESCRIPTIONS ''
                C1Dignosis.Cols[CPT_COL_CPT_DESC].Visible = false;
                C1Dignosis.Cols[CPT_COL_DX1_DESC].Visible = false;
                C1Dignosis.Cols[CPT_COL_MOD1_DESC].Visible = false;
                C1Dignosis.Cols[CPT_ISBILLED].Visible = false;

                //' ALIGHNMENT ''
                for (int iCol = 1; iCol <= CPT_COL_COUNT - 1; iCol++)
                {
                    C1Dignosis.Cols[iCol].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                }

                C1Dignosis.Cols[CPT_COL_UNIT].TextAlign = TextAlignEnum.RightCenter;
                C1Dignosis.Cols[CPT_COL_UNIT].TextAlignFixed = TextAlignEnum.LeftCenter;

                C1Dignosis.ExtendLastCol = true;
                C1Dignosis.Cols[CPT_COL_SELECT].AllowEditing = true;
                C1Dignosis.Cols[CPT_COL_ClaimStatus].Visible = true;
                C1Dignosis.Cols[CPT_COL_LINE_NO].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_LINE_NO_Display].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_DOS].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_CPT_ID].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_CPT_CODE].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_CPT_DESC].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_DX1_ID].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_DX1_CODE].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_DX1_DESC].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_MOD1_ID].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_MOD1_CODE].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_MOD1_DESC].AllowEditing = false;
                C1Dignosis.Cols[CPT_ISBILLED].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_UNIT].AllowEditing = false;
                C1Dignosis.Cols[CPT_COL_ClaimStatus].AllowEditing = false;

                C1Dignosis.ShowCellLabels = true;

          //      csNoPost = C1Dignosis.Styles.Add("csNoPost");
                try
                {
                    if (C1Dignosis.Styles.Contains("csNoPost"))
                    {
                        csNoPost = C1Dignosis.Styles["csNoPost"];
                    }
                    else
                    {
                        csNoPost = C1Dignosis.Styles.Add("csNoPost");
                        csNoPost.BackColor = Color.LightGray;
                        csNoPost.ForeColor = Color.Maroon;
                        csNoPost.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, (byte)0);

                    }

                }
                catch
                {
                    csNoPost = C1Dignosis.Styles.Add("csNoPost");
                    csNoPost.BackColor = Color.LightGray;
                    csNoPost.ForeColor = Color.Maroon;
                    csNoPost.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, (byte)0);

                }
 

            //    csPosted = C1Dignosis.Styles.Add("csPosted");
                try
                {
                    if (C1Dignosis.Styles.Contains("csPosted"))
                    {
                        csPosted = C1Dignosis.Styles["csPosted"];
                    }
                    else
                    {
                        csPosted = C1Dignosis.Styles.Add("csPosted");
                        csPosted.BackColor = Color.FromArgb(231, 231, 231); //Color.FromArgb(252, 243, 220);
                        csPosted.ForeColor = System.Drawing.Color.Black;
                        csPosted.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);

                    }

                }
                catch
                {
                    csPosted = C1Dignosis.Styles.Add("csPosted");
                    csPosted.BackColor = Color.FromArgb(231, 231, 231); //Color.FromArgb(252, 243, 220);
                    csPosted.ForeColor = System.Drawing.Color.Black;
                    csPosted.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);

                }


        //        csNoPostNormal = C1Dignosis.Styles.Add("cs_PostNormal");
                try
                {
                    if (C1Dignosis.Styles.Contains("cs_PostNormal"))
                    {
                        csNoPostNormal = C1Dignosis.Styles["cs_PostNormal"];
                    }
                    else
                    {
                        csNoPostNormal = C1Dignosis.Styles.Add("cs_PostNormal");
                        // Normal Style
                        csNoPostNormal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
                        csNoPostNormal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
                        csNoPostNormal.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
                        csNoPostNormal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

                        // Alternet Style
                        csNoPostNormal.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
                        csNoPostNormal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
                        csNoPostNormal.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                        csNoPostNormal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

                    }

                }
                catch
                {
                    csNoPostNormal = C1Dignosis.Styles.Add("cs_PostNormal");
                    // Normal Style
                    csNoPostNormal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
                    csNoPostNormal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
                    csNoPostNormal.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
                    csNoPostNormal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

                    // Alternet Style
                    csNoPostNormal.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
                    csNoPostNormal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
                    csNoPostNormal.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                    csNoPostNormal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

                }
 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCPTGrid()
        {
            DataTable dtExamCPT = EMRExam.GetCPTs(_EMRExamID, _EMRVisitID, _nEMRTreatmentType);

            string DOS = "";
            string CPTCode = "";
            string CPTDesc = "";
            string Diagnosis = "";
            string DiagnosisDesc = "";
            string Modifiers = "";
            decimal Units = 0;
            int LineNo = 0;
            int rowIndexCPT = 0;
            SetCPTGrid();

            StringBuilder sbEMRSplitCPTs = new StringBuilder();
            ArrayList sCPTSCodes = new ArrayList();

            DataRow[] _drLoadedCPTS = null;
            DataRow[] _drPostedCPTS = null;
            tlb_NoPost.Enabled = true;
            tlb_DXSave.Enabled = true;
           

            try
            {

                #region "To Skip the Service Lines Which are Loaded in the Services Lines User Control "

                if (oEMRTransLinesSplit != null && oEMRTransLinesSplit.Count > 0)
                {
                    for (int i = 0; i < oEMRTransLinesSplit.Count; i++)
                    {
                        // if No Post in database and user unposted them
                        if (_dtNoPostCharges != null && _dtNoPostCharges.Rows.Count > 0)
                        {
                           //SLR: Changed on 2/4/2014
                            for (int k = _dtNoPostCharges.Rows.Count - 1; k >= 0;  k--)
                            {
                                if (Convert.ToString(_dtNoPostCharges.Rows[k]["sCPTCodes"]) == Convert.ToString(oEMRTransLinesSplit[i].CPTCode) && Convert.ToString(_dtNoPostCharges.Rows[k]["nLineNo"]) == Convert.ToString(oEMRTransLinesSplit[i].EMRTreatmentLineNo))
                                {
                                    _dtNoPostCharges.Rows.RemoveAt(k);
                                }
                            }
                        }
                    }
                }

                #endregion

                if (EMRExam.IsFullyPosted(_nEMRTreatmentType, _EMRExamID))
                {
                    IsFullyPosted = true;
                }
                else
                {
                    IsFullyPosted = false;
                }
                if (dtExamCPT != null)
                {
                    foreach (DataRow rowCPT in dtExamCPT.Rows)
                    {

                        CPTCode = Convert.ToString(rowCPT["sCPTcode"]);
                        CPTDesc = Convert.ToString(rowCPT["sCPTDescription"]);
                        Units = gloCharges.FormatNumber(Convert.ToDecimal(rowCPT["nUnit"]));
                        if (rowCPT["dtDOS"] != DBNull.Value)
                        {
                            DOS = Convert.ToDateTime(rowCPT["dtDOS"]).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DOS = System.DateTime.Now.ToString("MM/dd/yyyy");
                        }
                        LineNo = Convert.ToInt32(rowCPT["nLineNo"]);

                        DataTable _dtDx = EMRExam.GetDiagnosisDataTable(_EMRExamID, _EMRVisitID, CPTCode, LineNo, _nEMRTreatmentType);
                        if (_dtDx != null && _dtDx.Rows.Count > 0)
                        {
                            Diagnosis = Convert.ToString(_dtDx.Rows[0][0]);
                            DiagnosisDesc = Convert.ToString(_dtDx.Rows[0][1]);
                        }
                        if (_dtDx != null)
                        {
                            _dtDx.Dispose();
                            _dtDx = null;
                        }
                        //Diagnosis = EMRExam.GetDiagnosisString(_EMRExamID, _EMRVisitID, CPTCode, LineNo, _nEMRTreatmentType);
                        Modifiers = EMRExam.GetModifiersString(_EMRExamID, _EMRVisitID, CPTCode, LineNo, _nEMRTreatmentType);

                        C1Dignosis.Rows.Add();
                        rowIndexCPT = C1Dignosis.Rows.Count - 1;

                        #region "To Mark - No Post,Posted "


                        _drPostedCPTS = dtBilledCPTS.Select("nLineNo= '" + LineNo + "'");

                        _drLoadedCPTS = _dtLoadedCPTS.Select(" nLineNo= '" + LineNo + "'");


                        //To Mark No Post CPTS
                        if (_dtNoPostCharges != null && _dtNoPostCharges.Rows.Count > 0)
                        {
                            DataRow[] _drNoPost = null;
                            _drNoPost = _dtNoPostCharges.Select("sCPTCodes = ('" + CPTCode + "') AND nLineNo= '" + LineNo + "' ");
                            if (_drNoPost != null && _drNoPost.Length > 0)
                            {

                                C1Dignosis.Rows[rowIndexCPT].Style = csNoPost;
                                C1Dignosis.SetData(rowIndexCPT, CPT_COL_ClaimStatus, "No Post");
                                C1Dignosis.SetData(rowIndexCPT, CPT_COL_SELECT, CheckEnum.Checked);
                                C1Dignosis.SetCellCheck(rowIndexCPT, CPT_ISBILLED, CheckEnum.Unchecked);

                            }
                            else if (_drPostedCPTS != null && _drPostedCPTS.Length > 0)
                            {

                                C1Dignosis.Rows[rowIndexCPT].Style = csPosted;
                                C1Dignosis.SetData(rowIndexCPT, CPT_COL_ClaimStatus, "Posted");

                                C1Dignosis.SetCellCheck(rowIndexCPT, CPT_COL_SELECT, CheckEnum.Checked);
                                C1Dignosis.SetCellCheck(rowIndexCPT, CPT_ISBILLED, CheckEnum.Checked);
                                C1Dignosis.Rows[rowIndexCPT].AllowEditing = false;

                            }
                            else if (_drLoadedCPTS != null && _drLoadedCPTS.Length > 0)
                            {
                                C1Dignosis.SetData(rowIndexCPT, CPT_COL_ClaimStatus, "");
                                C1Dignosis.SetCellCheck(rowIndexCPT, CPT_COL_SELECT, CheckEnum.Checked);
                                C1Dignosis.SetCellCheck(rowIndexCPT, CPT_ISBILLED, CheckEnum.Checked);
                            }
                            else
                            {
                                C1Dignosis.SetData(rowIndexCPT, CPT_COL_ClaimStatus, "");
                                C1Dignosis.SetCellCheck(rowIndexCPT, CPT_COL_SELECT, CheckEnum.Unchecked);
                                C1Dignosis.SetCellCheck(rowIndexCPT, CPT_ISBILLED, CheckEnum.Unchecked);
                            }

                        }
                        else if (_drPostedCPTS != null && _drPostedCPTS.Length > 0)
                        {

                            C1Dignosis.Rows[rowIndexCPT].Style = csPosted;
                            C1Dignosis.SetData(rowIndexCPT, CPT_COL_ClaimStatus, "Posted");

                            C1Dignosis.SetCellCheck(rowIndexCPT, CPT_COL_SELECT, CheckEnum.Checked);
                            C1Dignosis.SetCellCheck(rowIndexCPT, CPT_ISBILLED, CheckEnum.Checked);
                            C1Dignosis.Rows[rowIndexCPT].AllowEditing = false;


                        }
                        else if (_drLoadedCPTS != null && _drLoadedCPTS.Length > 0)
                        {
                            C1Dignosis.SetData(rowIndexCPT, CPT_COL_ClaimStatus, "");
                            C1Dignosis.SetCellCheck(rowIndexCPT, CPT_COL_SELECT, CheckEnum.Checked);
                            C1Dignosis.SetCellCheck(rowIndexCPT, CPT_ISBILLED, CheckEnum.Checked);
                        }
                        else
                        {
                            C1Dignosis.SetData(rowIndexCPT, CPT_COL_ClaimStatus, "");
                            C1Dignosis.SetCellCheck(rowIndexCPT, CPT_COL_SELECT, CheckEnum.Unchecked);
                            C1Dignosis.SetCellCheck(rowIndexCPT, CPT_ISBILLED, CheckEnum.Unchecked);
                        }

                        #endregion

                        //C1Dignosis.SetData(rowIndexCPT, CPT_COL_LINE_NO, rowIndexCPT);
                        C1Dignosis.SetData(rowIndexCPT, CPT_COL_LINE_NO, LineNo);
                        C1Dignosis.SetData(rowIndexCPT, CPT_COL_LINE_NO_Display, rowIndexCPT);
                        C1Dignosis.SetData(rowIndexCPT, CPT_COL_DOS, DOS);
                        C1Dignosis.SetData(rowIndexCPT, CPT_COL_CPT_CODE, CPTCode);
                        C1Dignosis.SetData(rowIndexCPT, CPT_COL_DX1_CODE, Diagnosis);
                        C1Dignosis.SetData(rowIndexCPT, CPT_COL_DX1_DESC, DiagnosisDesc);
                        C1Dignosis.SetData(rowIndexCPT, CPT_COL_MOD1_CODE, Modifiers);
                        C1Dignosis.SetData(rowIndexCPT, CPT_COL_UNIT, Units);
                    }
                }
            }
            catch (Exception exCPT)
            {
               gloAuditTrail.gloAuditTrail.ExceptionLog(exCPT.ToString(), false);
            }
            finally
            {
                if (C1Dignosis != null && C1Dignosis.Rows.Count > 1)
                {
                    C1Dignosis_RowColChange(null, null);
                }
                if (IsFullyPosted)
                {
                    tlb_DXSave.Enabled = false;
                }
                else
                {
                    tlb_DXSave.Enabled = true;
                }
                if (dtExamCPT != null)
                {
                    dtExamCPT.Dispose();
                    dtExamCPT = null;
                }
            }

        }

        private void LoadDXGrid()
        {
            

            string ICD9Code = "";
            string ICD9Desc = "";
            int LineNo = 0;

            int rowIndexICD = 0;

            SetDXGrid();

            tlb_NoPost.Enabled = true;
            tlb_DXSave.Enabled = true;

            try
            {

                if (EMRExam.IsFullyPosted(_nEMRTreatmentType, _EMRExamID))
                {
                    IsFullyPosted = true;
                }
                else
                {
                    IsFullyPosted = false;
                }



                C1Dignosis.Tree.Column = DX_Col_ICD9Code_Description;
                C1Dignosis.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;
                C1Dignosis.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                C1Dignosis.Tree.Indent = 15;
                C1Dignosis.ShowCellLabels = true;
                DataTable dtExamICD = null;
                dtExamICD = EMRExam.GetDiagnosis(_EMRExamID, _EMRVisitID, _nEMRTreatmentType);

                C1Dignosis.SetData(0, DX_Col_ICD9Code_Description, "ICD - Exam DOS [" + EMRExam.GetEMRExamDOS(_EMRExamID, _nEMRTreatmentType) + "]");
                if (dtExamICD != null)
                {
                    foreach (DataRow rowICD in dtExamICD.Rows)
                    {
                        ICD9Code = Convert.ToString(rowICD["sICD9Code"]);
                        ICD9Desc = Convert.ToString(rowICD["sICD9Description"]);
                        LineNo = Convert.ToInt32(rowICD["nLineNo"]);

                        if (!string.IsNullOrEmpty(ICD9Code))
                        {
                            C1Dignosis.Rows.Add();
                            rowIndexICD = C1Dignosis.Rows.Count - 1;

                            C1Dignosis.SetCellCheck(rowIndexICD, DX_Col_Select, CheckEnum.None);

                            C1Dignosis.Rows[rowIndexICD].AllowEditing = false;
                            C1Dignosis.Rows[rowIndexICD].ImageAndText = true;
                            C1Dignosis.Rows[rowIndexICD].Height = 24;
                            C1Dignosis.Rows[rowIndexICD].IsNode = true;
                            C1Dignosis.Rows[rowIndexICD].Node.Level = 0;
                            C1Dignosis.Rows[rowIndexICD].Node.Data = ICD9Code + "-" + ICD9Desc;
                            if (gloCharges.GetICDRevision(ICD9Code.Trim()) == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                            {
                                C1Dignosis.Rows[rowIndexICD].Node.Image = global::gloBilling.Properties.Resources.ICD10GalleryGreen;
                            }
                            else
                            {
                                C1Dignosis.Rows[rowIndexICD].Node.Image = global::gloBilling.Properties.Resources.ICD_09;
                            }
                            C1Dignosis.SetData(rowIndexICD, DX_Col_ICD9Code, ICD9Code);
                            C1Dignosis.SetData(rowIndexICD, DX_Col_ICD9Desc, ICD9Desc);
                            C1Dignosis.Cols[DX_Col_Select].AllowEditing = true;
                            // Set CPT Nodes
                            LoadCPTNodes(ICD9Code, ICD9Desc, LineNo);

                        }
                        else
                        {
                            LoadInboundCPTNodes(LineNo);
                        }
                    }

                    dtExamICD.Dispose();
                    dtExamICD = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (C1Dignosis!=null && C1Dignosis.Rows.Count>1 )
                {
                    C1Dignosis_RowColChange(null,null);
                }
                if (IsFullyPosted)
                {
                    tlb_DXSave.Enabled = false;
                }
                else
                {
                    tlb_DXSave.Enabled = true;
                }
            }
            //SetDXGrid();
            C1Dignosis.AllowEditing = true;
        }

        private void LoadInboundDXGrid()
        {
            

            string ICD9Code = "";
            string ICD9Desc = "";
            int LineNo = 0;

            int rowIndexICD = 0;

            SetDXGrid();

            try
            {

                C1Dignosis.Tree.Column = DX_Col_ICD9Code_Description;
                C1Dignosis.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;
                C1Dignosis.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                C1Dignosis.Tree.Indent = 15;
                C1Dignosis.ShowCellLabels = true;

                C1Dignosis.Cols[DX_Col_ICD9Code].DataType = typeof(System.String);
                C1Dignosis.Cols[DX_Col_ICD9Code_Description].DataType = typeof(System.String);
                C1Dignosis.Cols[DX_Col_ICD9Desc].DataType = typeof(System.String);

                C1Dignosis.Cols[DX_Col_CPTCode].DataType = typeof(System.String);
                C1Dignosis.Cols[DX_Col_CPTDesc].DataType = typeof(System.String);

                C1Dignosis.Cols[DX_Col_ModCode].DataType = typeof(System.String);
                C1Dignosis.Cols[DX_Col_ModDesc].DataType = typeof(System.String);

                DataTable dtExamICD = null;
                dtExamICD = EMRExam.GetDiagnosis(_EMRExamID, _EMRVisitID, _nEMRTreatmentType);
                C1Dignosis.SetData(0, DX_Col_ICD9Code_Description, "ICD9 - Exam DOS [" + EMRExam.GetEMRExamDOS(_EMRExamID, _nEMRTreatmentType) + "]");
                if (dtExamICD != null)
                {
                    foreach (DataRow rowICD in dtExamICD.Rows)
                    {
                        ICD9Code = Convert.ToString(rowICD["sICD9Code"]);
                        ICD9Desc = Convert.ToString(rowICD["sICD9Description"]);
                        LineNo = Convert.ToInt32(rowICD["nLineNo"]);

                        if (!string.IsNullOrEmpty(ICD9Code))
                        {
                            C1Dignosis.Rows.Add();
                            rowIndexICD = C1Dignosis.Rows.Count - 1;

                            C1Dignosis.Rows[rowIndexICD].AllowEditing = true;
                            C1Dignosis.Rows[rowIndexICD].ImageAndText = true;
                            C1Dignosis.Rows[rowIndexICD].Height = 24;
                            C1Dignosis.Rows[rowIndexICD].IsNode = true;
                            C1Dignosis.Rows[rowIndexICD].Node.Level = 0;
                            C1Dignosis.Rows[rowIndexICD].Node.Data = Convert.ToString(ICD9Code + "-" + ICD9Desc);
                            C1Dignosis.Rows[rowIndexICD].Node.Image = global::gloBilling.Properties.Resources.ICD_09;
                            C1Dignosis.SetData(rowIndexICD, DX_Col_ICD9Code, ICD9Code);
                            C1Dignosis.SetData(rowIndexICD, DX_Col_ICD9Desc, ICD9Desc);
                            LoadCPTNodes(ICD9Code, ICD9Desc, LineNo);

                        }
                        else
                        {
                            LoadInboundCPTNodes(LineNo);
                        }
                    }

                    dtExamICD.Dispose();
                    dtExamICD = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCPTNodes(string ICD9Code, string ICD9Desc, int LineNo)
        {
            DataTable dtExamCPT = null;

            string CPTCode = "";
            string CPTDesc = "";
            decimal Units = 0;
            int rowIndexCPT = 0;

            ArrayList CPTCodes = new ArrayList();
            dtExamCPT = EMRExam.GetCPTs(_EMRExamID, _EMRVisitID, ICD9Code, LineNo,_nEMRTreatmentType);
            DataRow[] _drLoadedCPTS = null;
            DataRow[] _drPostedCPTS = null;
            //StringBuilder sbEMRSplitCPTs = new StringBuilder();
            //DataTable _dtLoadedCPTS = new DataTable();
            //_dtLoadedCPTS.Columns.Add("nLineNo");
            //_dtLoadedCPTS.Columns.Add("sCPTCodes");

            try
            {

                #region " To Load From The Object"

                if (oEMRTransLinesSplit != null && oEMRTransLinesSplit.Count > 0)
                {
                    #region "To Skip the Service Lines Which are Loaded in the Services Lines User Control "

                    //for (int i = 0; i < oEMRTransLinesSplit.Count; i++)
                    //{
                    //    foreach (DataRow rowCPT in dtExamCPT.Rows)
                    //    {
                    //        if (oEMRTransLinesSplit[i].CPTCode.ToString() == Convert.ToString(rowCPT["sCPTcode"]))
                    //        {

                    //            if (i == oEMRTransLinesSplit.Count - 1)
                    //            {
                    //                sbEMRSplitCPTs.Append(oEMRTransLinesSplit[i].CPTCode.ToString());
                    //            }
                    //            else
                    //            {
                    //                sbEMRSplitCPTs.Append(oEMRTransLinesSplit[i].CPTCode.ToString() + "','");
                    //            }

                    //            DataRow _dr = _dtLoadedCPTS.NewRow();
                    //            _dr["nLineNo"] = Convert.ToInt32(oEMRTransLinesSplit[i].EMRTreatmentLineNo);
                    //            _dr["sCPTCodes"] = Convert.ToString(oEMRTransLinesSplit[i].CPTCode);
                    //            _dtLoadedCPTS.Rows.Add(_dr);
                    //            _dtLoadedCPTS.AcceptChanges();

                    //            break;
                    //        }
                    //    }
                    //}

                    #endregion
                }

                #endregion
                if (dtExamCPT != null)
                {
                    foreach (DataRow rowCPT in dtExamCPT.Rows)
                    {
                        CPTCode = Convert.ToString(rowCPT["sCPTcode"]);
                        CPTDesc = Convert.ToString(rowCPT["sCPTDescription"]);
                        Units = gloCharges.FormatNumber(Convert.ToDecimal(rowCPT["nUnit"]));

                        if (!CPTCodes.Contains(CPTCode))
                        {
                            if (!string.IsNullOrEmpty(CPTCode))
                            {
                                C1Dignosis.Rows.Add();
                                rowIndexCPT = C1Dignosis.Rows.Count - 1;

                                C1Dignosis.Rows[rowIndexCPT].AllowEditing = true;
                                C1Dignosis.Rows[rowIndexCPT].ImageAndText = true;
                                C1Dignosis.Rows[rowIndexCPT].Height = 24;
                                C1Dignosis.Rows[rowIndexCPT].IsNode = true;

                                #region " To Determine the Posted CPTS And No Post CPTS"

                                _drPostedCPTS = dtBilledCPTS.Select("sCPTCodes  IN ('" + CPTCode + "') ");

                                _drLoadedCPTS = _dtLoadedCPTS.Select("sCPTCodes IN ('" + CPTCode + "')  ");

                                //To Mark No Post CPTS
                                if (_dtNoPostCharges != null && _dtNoPostCharges.Rows.Count > 0)
                                {
                                    DataRow[] _drNoPost = null;
                                    _drNoPost = _dtNoPostCharges.Select("sCPTCodes = ('" + CPTCode + "') ");
                                    if (_drNoPost.Length > 0)
                                    {

                                        C1Dignosis.Rows[rowIndexCPT].Style = csNoPost;
                                        C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "No Post");
                                        C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                        C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Unchecked);//Determines Whether already billed 

                                    }
                                    else if (_drPostedCPTS != null && _drPostedCPTS.Length > 0)
                                    {

                                        C1Dignosis.Rows[rowIndexCPT].Style = csPosted;
                                        C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "Posted");

                                        C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                        C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Checked);
                                        C1Dignosis.Rows[rowIndexCPT].AllowEditing = false;

                                    }
                                    else if (_drLoadedCPTS != null && _drLoadedCPTS.Length > 0)
                                    {
                                        C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "");
                                        C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                        C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "");
                                        C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
                                        C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Unchecked);
                                    }

                                }
                                else if (_drPostedCPTS != null && _drPostedCPTS.Length > 0)
                                {

                                    C1Dignosis.Rows[rowIndexCPT].Style = csPosted;
                                    C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "Posted");

                                    C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                    C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Checked);
                                    C1Dignosis.Rows[rowIndexCPT].AllowEditing = false;

                                }
                                else if (_drLoadedCPTS != null && _drLoadedCPTS.Length > 0)
                                {
                                    C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "");
                                    C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                    C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Checked);
                                }
                                else
                                {
                                    C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "");
                                    C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
                                    C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Unchecked);
                                }


                                #endregion

                                C1Dignosis.Rows[rowIndexCPT].Node.Level = 1;
                                C1Dignosis.Rows[rowIndexCPT].Node.Data = CPTCode + "-" + CPTDesc;
                                C1Dignosis.Rows[rowIndexCPT].Node.Image = global::gloBilling.Properties.Resources.CPT;

                                C1Dignosis.SetData(rowIndexCPT, DX_Col_CPTLineNo, Convert.ToInt32(rowCPT["nLineNo"]));
                                C1Dignosis.SetData(rowIndexCPT, DX_Col_ICD9Code, ICD9Code);
                                C1Dignosis.SetData(rowIndexCPT, DX_Col_ICD9Desc, ICD9Desc);
                                C1Dignosis.SetData(rowIndexCPT, DX_Col_CPTCode, CPTCode);
                                C1Dignosis.SetData(rowIndexCPT, DX_Col_CPTDesc, CPTDesc);
                                C1Dignosis.SetData(rowIndexCPT, DX_Col_Units, Units);

                                C1Dignosis.Cols[DX_Col_Select].Visible = false;
                                C1Dignosis.Cols[DX_Col_ClaimStatus].Visible = true;

                                C1Dignosis.OwnerDrawCell -= c1FlexGrid1_OwnerDrawCell;
                                LoadModifierNodes(ICD9Code, ICD9Desc, CPTCode, CPTDesc, Units, LineNo);
                                C1Dignosis.OwnerDrawCell += c1FlexGrid1_OwnerDrawCell;

                            }
                            CPTCodes.Add(CPTCode);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            if (dtExamCPT != null)
            {
                dtExamCPT.Dispose();
                dtExamCPT = null;
            }
            C1Dignosis.AllowEditing = true;
           
        }

        private void LoadInboundCPTNodes(int LineNo)
        {
            DataTable dtExamCPT = null;

            string CPTCode = "";
            string CPTDesc = "";
            decimal Units = 0;
            int rowIndexCPT = 0;

            ArrayList CPTCodes = new ArrayList();
            dtExamCPT = EMRExam.GetCPTs(_EMRExamID, _EMRVisitID, "", LineNo, _nEMRTreatmentType);
            DataRow[] _drLoadedCPTS = null;
            DataRow[] _drPostedCPTS = null;
            StringBuilder sbEMRSplitCPTs = new StringBuilder();
            DataTable _dtLoadedCPTS = new DataTable();

            _dtLoadedCPTS.Columns.Add("nLineNo");
            _dtLoadedCPTS.Columns.Add("sCPTCodes");

            try
            {

                #region " To Load From The Object"

                if (oEMRTransLinesSplit != null && oEMRTransLinesSplit.Count > 0)
                {
                    #region "To Skip the Service Lines Which are Loaded in the Services Lines User Control "

                    //for (int i = 0; i < oEMRTransLinesSplit.Count; i++)
                    //{
                    //    foreach (DataRow rowCPT in dtExamCPT.Rows)
                    //    {
                    //        if (oEMRTransLinesSplit[i].CPTCode.ToString() == Convert.ToString(rowCPT["sCPTcode"]))
                    //        {

                    //            if (i == oEMRTransLinesSplit.Count - 1)
                    //            {
                    //                sbEMRSplitCPTs.Append(oEMRTransLinesSplit[i].CPTCode.ToString());
                    //            }
                    //            else
                    //            {
                    //                sbEMRSplitCPTs.Append(oEMRTransLinesSplit[i].CPTCode.ToString() + "','");
                    //            }

                    //            DataRow _dr = _dtLoadedCPTS.NewRow();
                    //            _dr["nLineNo"] = Convert.ToInt32(oEMRTransLinesSplit[i].EMRTreatmentLineNo);
                    //            _dr["sCPTCodes"] = Convert.ToString(oEMRTransLinesSplit[i].CPTCode);
                    //            _dtLoadedCPTS.Rows.Add(_dr);
                    //            _dtLoadedCPTS.AcceptChanges();

                    //            break;
                    //        }
                    //    }
                    //}

                    #endregion
                }

                #endregion
                if (dtExamCPT != null)
                {
                    foreach (DataRow rowCPT in dtExamCPT.Rows)
                    {
                        CPTCode = Convert.ToString(rowCPT["sCPTcode"]);
                        CPTDesc = Convert.ToString(rowCPT["sCPTDescription"]);
                        Units = gloCharges.FormatNumber(Convert.ToDecimal(rowCPT["nUnit"]));

                        if (!CPTCodes.Contains(CPTCode))
                        {
                            if (!string.IsNullOrEmpty(CPTCode))
                            {
                                C1Dignosis.Rows.Add();
                                rowIndexCPT = C1Dignosis.Rows.Count - 1;

                                C1Dignosis.Rows[rowIndexCPT].AllowEditing = true;
                                C1Dignosis.Rows[rowIndexCPT].ImageAndText = true;
                                C1Dignosis.Rows[rowIndexCPT].Height = 24;
                                C1Dignosis.Rows[rowIndexCPT].IsNode = true;

                                #region " To Determine the Posted CPTS And No Post CPTS"

                                _drPostedCPTS = dtBilledCPTS.Select("sCPTCodes  IN ('" + CPTCode + "') AND nLineNo= '" + LineNo + "' ");

                                _drLoadedCPTS = _dtLoadedCPTS.Select("sCPTCodes IN ('" + CPTCode + "') AND nLineNo= '" + LineNo + "' ");

                                //To Mark No Post CPTS
                                if (_dtNoPostCharges != null && _dtNoPostCharges.Rows.Count > 0)
                                {
                                    DataRow[] _drNoPost = null;
                                    _drNoPost = _dtNoPostCharges.Select("sCPTCodes = ('" + CPTCode + "') AND nLineNo= '" + LineNo + "' ");
                                    if (_drNoPost.Length > 0)
                                    {

                                        C1Dignosis.Rows[rowIndexCPT].Style = csNoPost;
                                        C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "No Post");
                                        C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                        C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Unchecked);//Determines Whether already billed 

                                    }
                                    else if (_drPostedCPTS != null && _drPostedCPTS.Length > 0)
                                    {

                                        C1Dignosis.Rows[rowIndexCPT].Style = csPosted;
                                        C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "Posted");

                                        C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                        C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Checked);
                                        C1Dignosis.Rows[rowIndexCPT].AllowEditing = false;

                                    }
                                    else if (_drLoadedCPTS != null && _drLoadedCPTS.Length > 0)
                                    {
                                        C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "");
                                        C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                        C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Checked);
                                    }
                                    else
                                    {
                                        C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "");
                                        C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
                                        C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Unchecked);
                                    }

                                }
                                else if (_drPostedCPTS != null && _drPostedCPTS.Length > 0)
                                {

                                    C1Dignosis.Rows[rowIndexCPT].Style = csPosted;
                                    C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "Posted");

                                    C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                    C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Checked);
                                    C1Dignosis.Rows[rowIndexCPT].AllowEditing = false;

                                }
                                else if (_drLoadedCPTS != null && _drLoadedCPTS.Length > 0)
                                {
                                    C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "");
                                    C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                    C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Checked);
                                }
                                else
                                {
                                    C1Dignosis.SetData(rowIndexCPT, DX_Col_ClaimStatus, "");
                                    C1Dignosis.Rows[rowIndexCPT].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
                                    C1Dignosis.SetCellCheck(rowIndexCPT, DX_Col_Select, CheckEnum.Unchecked);
                                }


                                #endregion

                                C1Dignosis.Rows[rowIndexCPT].Node.Level = 1;
                                C1Dignosis.Rows[rowIndexCPT].Node.Data = CPTCode + "-" + CPTDesc;
                                C1Dignosis.Rows[rowIndexCPT].Node.Image = global::gloBilling.Properties.Resources.CPT;

                                C1Dignosis.SetData(rowIndexCPT, DX_Col_CPTLineNo, Convert.ToInt32(rowCPT["nLineNo"]));
                                //C1Dignosis.SetData(rowIndexCPT, DX_Col_ICD9Code, ICD9Code);
                                //C1Dignosis.SetData(rowIndexCPT, DX_Col_ICD9Desc, ICD9Desc);
                                C1Dignosis.SetData(rowIndexCPT, DX_Col_CPTCode, CPTCode);
                                C1Dignosis.SetData(rowIndexCPT, DX_Col_CPTDesc, CPTDesc);
                                C1Dignosis.SetData(rowIndexCPT, DX_Col_Units, Units);

                                C1Dignosis.Cols[DX_Col_Select].Visible = false;
                                C1Dignosis.Cols[DX_Col_ClaimStatus].Visible = true;

                                C1Dignosis.OwnerDrawCell -= c1FlexGrid1_OwnerDrawCell;
                                LoadModifierNodes("", "", CPTCode, CPTDesc, Units, LineNo);
                                C1Dignosis.OwnerDrawCell += c1FlexGrid1_OwnerDrawCell;

                            }
                            CPTCodes.Add(CPTCode);
                        }

                    }

                    dtExamCPT.Dispose();
                    dtExamCPT = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void LoadModifierNodes(string ICD9Code, string ICD9Desc, string CPTCode, string CPTDesc, decimal Units, int LineNo)
        {
            DataTable dtExamMOD = null;

            string ModCode = "";
            string ModDesc = "";

            int rowIndexMOD = 0;

            try
            {
                dtExamMOD = EMRExam.GetModifiers(_EMRExamID, _EMRVisitID, ICD9Code, CPTCode, LineNo, _nEMRTreatmentType);
                if (dtExamMOD != null)
                {
                    foreach (DataRow rowMOD in dtExamMOD.Rows)
                    {
                        ModCode = Convert.ToString(rowMOD["sModCode"]);
                        ModDesc = Convert.ToString(rowMOD["sModDescription"]);

                        if (!string.IsNullOrEmpty(ModCode))
                        {
                            C1Dignosis.Rows.Add();
                            rowIndexMOD = C1Dignosis.Rows.Count - 1;

                            C1Dignosis.Rows[rowIndexMOD].AllowEditing = true;
                            C1Dignosis.Rows[rowIndexMOD].ImageAndText = true;
                            C1Dignosis.Rows[rowIndexMOD].Height = 24;
                            C1Dignosis.Rows[rowIndexMOD].IsNode = true;
                            C1Dignosis.Rows[rowIndexMOD].Node.Level = 2;
                            C1Dignosis.Rows[rowIndexMOD].Node.Data = ModCode + "-" + ModDesc;
                            C1Dignosis.Rows[rowIndexMOD].Node.Image = global::gloBilling.Properties.Resources.Modify_PNG;

                            C1Dignosis.SetData(rowIndexMOD, DX_Col_ICD9Code, ICD9Code);
                            C1Dignosis.SetData(rowIndexMOD, DX_Col_ICD9Desc, ICD9Desc);

                            C1Dignosis.SetData(rowIndexMOD, DX_Col_CPTCode, CPTCode);
                            C1Dignosis.SetData(rowIndexMOD, DX_Col_CPTDesc, CPTDesc);
                            //C1Dignosis.SetData(rowIndexMOD, DX_Col_Units, Units);

                            C1Dignosis.SetData(rowIndexMOD, DX_Col_ModCode, ModCode);
                            C1Dignosis.SetData(rowIndexMOD, DX_Col_ModDesc, ModDesc);
                            C1Dignosis.Rows[rowIndexMOD].AllowEditing = false;
                        }
                    }
                }
            }
            catch (Exception exMod)
            {
                
              gloAuditTrail.gloAuditTrail.ExceptionLog(exMod.ToString(), false);
            }
            if (dtExamMOD != null)
            {
                dtExamMOD.Dispose();
                dtExamMOD = null;
            }
        }

        void c1FlexGrid1_OwnerDrawCell(object sender, C1.Win.C1FlexGrid.OwnerDrawCellEventArgs e)
        {
            try
            {
                // custom drawing for nodes
                var row = C1Dignosis.Rows[e.Row];
                if (row.IsNode)
                {

                    if ((row.Node.Level != 0))
                    {
                        if (C1Dignosis.Cols[e.Col].Index == 1 && e.Text != "")
                        {

                            if (Convert.ToString(C1Dignosis.GetData(e.Row, DX_Col_Units)) != "")
                            {
                                var text = e.Text;
                                e.Text = string.Empty;
                                e.DrawCell();

                                // get the node
                                var node = row.Node;

                                // get the image and the node indentation
                                var img = global::gloBilling.Properties.Resources.CPT;
                                var indent = C1Dignosis.Tree.Indent * (node.Level + 1);
                                var rc = e.Bounds;
                                rc.X += indent;
                                rc.Width -= indent;

                                // draw the image
                                var rcImage = rc;
                                rcImage.X += 16; // room for checkbox
                                rcImage.Width = rc.Height;
                                rcImage.Inflate(-4, -4);
                                e.Graphics.DrawImage(img, rcImage);

                                // draw the text
                                var rcText = rc;
                                rcText.X += 16 + rc.Height; // room for checkbox and for image
                                using (SolidBrush br = new SolidBrush(e.Style.ForeColor))
                                {
                                    e.Graphics.DrawString(text, e.Style.Font, br, rcText, e.Style.StringFormat);
                                }
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

        private void C1Dignosis_AfterEdit(object sender, RowColEventArgs e)
        {

        }

        private void C1Dignosis_CellChecked(object sender, RowColEventArgs e)
        {
            try
            {
                if (C1Dignosis != null && C1Dignosis.Rows.Count > 0)
                {
                    if (C1Dignosis.Rows[e.Row].Node.Checked == CheckEnum.Unchecked)
                    {
                        C1Dignosis.Rows[e.Row].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
                        //C1Dignosis.SetCellCheck(e.Row, DX_Col_Select, CheckEnum.Unchecked);
                    }
                    else
                    {
                        C1Dignosis.Rows[e.Row].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                        //C1Dignosis.SetCellCheck(e.Row, DX_Col_Select, CheckEnum.Unchecked);
                    }
                    if (_IsICD9Driven)
                    {
                        for (int cntr = 1; cntr <= C1Dignosis.Rows.Count - 1; cntr++)
                        {
                            if (cntr != e.Row)
                            {
                                if (Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_Units)).Trim() != "")
                                {
                                    if (Convert.ToString(C1Dignosis.GetData(e.Row, DX_Col_CPTCode)) == Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_CPTCode)))
                                    {
                                        if (C1Dignosis.Rows[e.Row].Node.Checked == CheckEnum.Unchecked)
                                        {
                                            C1Dignosis.Rows[cntr].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
                                        }
                                        else
                                        {
                                            C1Dignosis.Rows[cntr].Node.Checked = C1.Win.C1FlexGrid.CheckEnum.Checked;
                                        }
                                    }
                                }
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

        private void C1Dignosis_RowColChange(object sender, EventArgs e)
        {
            try
            {
                //if (C1Dignosis.Rows.Count > 1)
                //{
                //    if (C1Dignosis.RowSel > 0)
                //    {
                //        if (_IsICD9Driven && _nEMRTreatmentType != ExternalChargesType.HL7InboundCharges)
                //        {
                //            //if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_CPTCode)).Trim() == "" || Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_ClaimStatus)).Trim().ToUpper() == "POSTED")
                //            //{
                //            //    tlb_NoPost.Enabled = false;
                //            //}
                //            //else
                //            //{
                //            //    tlb_NoPost.Enabled = true;
                //            //}
                //        }
                //        else
                //        {
                //            //if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_CPT_CODE)).Trim() == "" || Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus)).Trim().ToUpper() == "POSTED")
                //            //{
                //            //    tlb_NoPost.Enabled = false;
                //            //}
                //            //else
                //            //{
                //            //    tlb_NoPost.Enabled = true;
                //            //}
 
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void C1Dignosis_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                string sValue = "";
                string sStatus = "";

                //if (C1Dignosis.HitTest(e.X, e.Y).Row > 0 && C1Dignosis.RowSel == C1Dignosis.HitTest(e.X, e.Y).Row)
                if (C1Dignosis.HitTest(e.X, e.Y).Row > 0)
                {
                    C1Dignosis.Select(C1Dignosis.HitTest(e.X, e.Y).Row, DX_Col_CPTCode);

                    if (_IsICD9Driven && _nEMRTreatmentType != ExternalChargesType.HL7InboundCharges)
                    {
                        if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_Units)).Trim() != "" || Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_ClaimStatus)).Trim().ToUpper() != "POSTED")
                        {
                            sValue = Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_Units)).Trim();
                            sStatus = Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_ClaimStatus)).Trim();
                        }

                    }
                    else
                    {
                        if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_CPT_CODE)).Trim() != "" || Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus)).Trim().ToUpper() != "POSTED")
                        {
                            sValue = Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_CPT_CODE)).Trim();
                            sStatus = Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus)).Trim();
                        }

                    }


                }
               
                for (int i = cmnu_DxNoPost.Items.Count - 1; i >= 0; i--)
                {
                    cmnu_DxNoPost.Items.RemoveAt(i);

                }

                if (!IsFullyPosted)
                {
                    if (e.Button == MouseButtons.Right && sValue != "")
                    {
                        cmnu_DxNoPost.Visible = true;
                        C1Dignosis.ContextMenuStrip = cmnu_DxNoPost;

                        if (sStatus == "No Post")
                        {
                            cmnu_DxNoPost.Items.Add(mnuItem_UndoNoPost);
                        }
                        else if (sStatus != "Posted")
                        {
                            cmnu_DxNoPost.Items.Add(mnuItem_NoPost);
                        }
                        else
                        {
                            C1Dignosis.ContextMenuStrip = null;
                            cmnu_DxNoPost.Visible = false;
                        }

                    }
                    else
                    {
                        C1Dignosis.ContextMenuStrip = null;
                        cmnu_DxNoPost.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuItem_NoPost_Click(object sender, EventArgs e)
        {
            string sValue = "";
            c1Insurance.ContextMenuStrip = null;

            if (_IsICD9Driven && _nEMRTreatmentType != ExternalChargesType.HL7InboundCharges)
            {
                if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_CPTCode)).Trim() != "" || Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_ClaimStatus)).Trim().ToUpper() != "POSTED")
                {
                    sValue = Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_CPTCode)).Trim();
                }

            }
            else
            {
                if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_CPT_CODE)).Trim() != "" || Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus)).Trim().ToUpper() != "POSTED")
                {
                    sValue = Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_CPT_CODE)).Trim();
                }

            }

            if (C1Dignosis != null && C1Dignosis.Rows.Count > 0 && sValue != "")
            {

                if (_IsICD9Driven && _nEMRTreatmentType != ExternalChargesType.HL7InboundCharges)
                {
                    #region "ICD9 Driven"

                    if (C1Dignosis.RowSel > 0 && C1Dignosis.Rows.Count > 1)
                    {
                        if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_Units)) != "")
                        {
                            if (Convert.ToString((C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_ClaimStatus))) == "")
                            {
                                for (int cntr = 1; cntr <= C1Dignosis.Rows.Count - 1; cntr++)
                                {

                                    if (Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_Units)).Trim() != "")
                                    {
                                        if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_CPTCode)) == Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_CPTCode)))
                                        {
                                            C1Dignosis.Rows[cntr].Style = csNoPost;
                                            C1Dignosis.SetData(cntr, DX_Col_ClaimStatus, "No Post");
                                        }
                                    }

                                }
                            }
                            else if (Convert.ToString((C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_ClaimStatus))) == "No Post")
                            {

                                for (int cntr = 1; cntr <= C1Dignosis.Rows.Count - 1; cntr++)
                                {

                                    if (Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_Units)).Trim() != "")
                                    {
                                        if (Convert.ToString(C1Dignosis.GetData(C1Dignosis.RowSel, DX_Col_CPTCode)) == Convert.ToString(C1Dignosis.GetData(cntr, DX_Col_CPTCode)))
                                        {
                                            C1Dignosis.Rows[cntr].Style = csNoPostNormal;
                                            C1Dignosis.SetData(cntr, DX_Col_ClaimStatus, "");
                                        }
                                    }

                                }

                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region "CPT Driven"

                    if (C1Dignosis.RowSel > 0 && C1Dignosis.Rows.Count > 1)
                    {
                        if (Convert.ToString((C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus))) == "")
                        {

                            C1Dignosis.Rows[C1Dignosis.RowSel].Style = csNoPost;
                            C1Dignosis.SetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus, "No Post");
                        }
                        else if (Convert.ToString((C1Dignosis.GetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus))) == "No Post")
                        {

                            C1Dignosis.Rows[C1Dignosis.RowSel].Style = csNoPostNormal;
                            C1Dignosis.SetData(C1Dignosis.RowSel, CPT_COL_ClaimStatus, "");
                        }
                    }



                    #endregion
                }

            }

        }

        private void LoadDXCPTWindow()
        {
            gloBilling ogloBilling;
            ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            //tlb_NoPost.Visible = false;
            try
            {
                if (dtBilledCPTS != null)
                {
                    dtBilledCPTS.Dispose();
                    dtBilledCPTS = null;
                }
                dtBilledCPTS = ogloBilling.GetPostedCPTs(_EMRExamID, _nEMRTreatmentType);
                if (oEMRTransLineSplit != null)
                {
                    oEMRTransLineSplit.Dispose();
                    oEMRTransLineSplit = null;
                }
                oEMRTransLinesSplit = UC_gloBillingTransactionLines.GetLineTransactions();

                if (!bIsFullyPosted)
                {
                }
                else
                {
                    if (_dtNoPostCharges != null)
                    {
                        _dtNoPostCharges.Dispose();
                        _dtNoPostCharges = null;
                    }
                    _dtNoPostCharges = ogloBilling.Get_NOPOST_CPTs(_EMRExamID, _nEMRTreatmentType);
                    bIsFullyPosted = false;

                }

                pnlExamCPTDX.SendToBack();

                if (_nEMRTreatmentType == gloSettings.ExternalChargesType.gloEMRTreatment)
                {
                    if (_IsICD9Driven)
                    { LoadDXGrid(); }
                    else
                    { LoadCPTGrid(); }
                }
                else if (_nEMRTreatmentType == gloSettings.ExternalChargesType.HL7InboundCharges)
                {
                    LoadCPTGrid();
                }
                if (C1Dignosis != null && C1Dignosis.Rows.Count > 0)
                {
                    //tlb_NoPost.Visible = true;
                }

                pnlExamCPTDX.Location = new System.Drawing.Point(320, 130);
                pnlExamCPTDX.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        private void setTreatmentSplitAfterSave()
        {
            string sMessage = string.Empty;
            gloBilling ogloBilling = null;
            try
            {

                if (EMRExam.IsFullyPosted(_nEMRTreatmentType, _EMRExamID))
                {
                    IsFullyPosted = true;
                }
                else
                {
                    IsFullyPosted = false;
                }


                ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);

                if (UC_gloBillingTransactionLines != null)
                {
                    UC_gloBillingTransactionLines.PatientID = this.PatientID;
                    UC_gloBillingTransactionLines.PatientProviderID = _EMRProviderId;
                    UC_gloBillingTransactionLines.ReinitilizeControl();
                    UC_gloBillingTransactionLines.FacilityID = Convert.ToInt64(cmbFacility.SelectedValue);
                    SetFacilitySettingsData();
                    UC_gloBillingTransactionLines.TreatmentType = _nEMRTreatmentType;
                }

                pnlEMRExams.SendToBack();
                panel6.Visible = true;

                SelectPrimaryInsurance();

                Int64 _ContactId = 0;

                if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                {
                    _ContactId = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
                }

                ogloBilling.GetExpandedClaimSetting(_ContactId, _ClinicID, out _NoOfMaxServiceLines, out _NoOfMaxDiagnosis);
                UC_gloBillingTransactionLines._NoOfDiagnosis = _NoOfMaxDiagnosis;
                UC_gloBillingTransactionLines._NoOfServiceLines = _NoOfMaxServiceLines;



                #region " Check EMR Treatment Present or Not "

                if (!IsFullyPosted)
                {
                    sMessage = gloCharges.ValidateEMRExam(_EMRExamID, _IsICD9Driven, _NoOfMaxDiagnosis, _NoOfMaxServiceLines, _nEMRTreatmentType);

                    if (sMessage.Trim() != String.Empty)
                    {
                        if (MessageBox.Show("EMR Treatment requires manual entry.                 " + Environment.NewLine + sMessage + Environment.NewLine + Environment.NewLine + "Treatment Details will be displayed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                        }
                    }
                    else
                    {

                    }
                }
                SetEMRTreatment(_EMRExamID);
                if (!IsFullyPosted)
                {
                    LoadDXCPTWindow();
                }

                #endregion

                if (UC_gloBillingTransactionLines != null)
                {
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
                        }
                        UC_gloBillingTransactionLines.FeeScheduleID = 0;
                        UC_gloBillingTransactionLines.Fee_ScheduleID = 0;
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (IsFullyPosted)
                {
                    _EMRExamID = 0;
                }
            }
        }

        #endregion 
      
        #region OCP DX CPT
        public void LoadOCPDXCPTWindow()
        {
            gloBilling ogloBilling;
            ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
            DataTable dtOCPDX = null;
            DataView _dv = null;

            try
            {
                C1OCPDignosis.Visible = false;
                if (C1Dignosis != null)
                {
                    C1Dignosis.DataSource = null;
                }

                dtOCPDX = ogloBilling.GetOCPDX(_PortalClaimID);
                if (dtOCPDX != null)
                {
                    if (dtOCPDX.Rows.Count > 0)
                    {
                        pnlOCPCPTDX.Visible = true;
                        C1OCPDignosis.AutoResize = false;
                        C1OCPDignosis.Redraw = false;

                        C1OCPDignosis.Redraw = true;
                        C1OCPDignosis.AutoResize = true;

                        _dv = dtOCPDX.Copy().DefaultView;
                        dtOCPDX.Dispose();
                        dtOCPDX = null;
                        C1OCPDignosis.DataSource = _dv;

                        C1OCPDignosis.Cols["nrowID"].Visible = false;
                        C1OCPDignosis.Cols["sContent"].Visible = false;
                        C1OCPDignosis.Cols["CPTCOde"].Visible = true;
                        C1OCPDignosis.Cols["sKey"].Visible = false;
                        C1OCPDignosis.Cols["sValue"].Visible = true;
                        C1OCPDignosis.Cols["sDesc"].Visible = true;

                        C1OCPDignosis.Cols["CPTCOde"].Caption = "CPT Code";
                        C1OCPDignosis.Cols["sValue"].Caption = "Dx Code";
                        C1OCPDignosis.Cols["sDesc"].Caption = "Description";

                        int nWidth = 0;
                        nWidth = c1OnlineCharge.Width;
                        C1OCPDignosis.Cols["nrowID"].Width = Convert.ToInt32(nWidth * 0.10);
                        C1OCPDignosis.Cols["sContent"].Width = Convert.ToInt32(nWidth * 0.10);
                        C1OCPDignosis.Cols["CPTCOde"].Width = Convert.ToInt32(nWidth * 0.10);
                        C1OCPDignosis.Cols["sKey"].Width = Convert.ToInt32(nWidth * 0.10);
                        C1OCPDignosis.Cols["sValue"].Width = Convert.ToInt32(nWidth * 0.10);
                        C1OCPDignosis.Cols["sDesc"].Width = Convert.ToInt32(nWidth * 0.10);

                        C1OCPDignosis.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                        C1OCPDignosis.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                        C1OCPDignosis.AutoResize = true;
                        C1OCPDignosis.ExtendLastCol = true;
                    }
                }


                pnlOCPCPTDX.Location = new System.Drawing.Point(320, 130);

                pnlOCPCPTDX.BringToFront();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                }
                C1OCPDignosis.Visible = true;
                if (C1OCPDignosis != null && C1OCPDignosis.ScrollBars == ScrollBars.None)
                {
                    C1OCPDignosis.ScrollBars = ScrollBars.Vertical;
                }
            }
        }





        #endregion

        #region "Eligibility Insurance  Covrage"
        private Boolean ValidateInsuranceCoverage()
        {
            DataTable _dtInsuranceCoverageDates = null;
            _sResponsibleNo = Convert.ToString(c1Insurance.GetData(1, COL_INSURANCERESPONSIBILITY)).Replace("\0", "");
            _nResponsibleParty = Convert.ToInt64(c1Insurance.GetData(1, COL_INSURANCECONTACTID));
            Int64 nCurrentInsuranceID = Convert.ToInt64(c1Insurance.GetData(Convert.ToInt16(_sResponsibleNo), COL_INSURANCEID));
            string ncurrentInsuranceParty = Convert.ToString(c1Insurance.GetData(Convert.ToInt16(_sResponsibleNo), COL_INSURANCENAME));
            gloCharges.GetInsuranceCovrageDates(_PatientID, _nResponsibleParty, nCurrentInsuranceID, out _dtInsuranceCoverageDates);
            try
            {
                if (_dtInsuranceCoverageDates.Rows.Count > 0)
                {
                    return UC_gloBillingTransactionLines.ValidateInsuranceCovrageDates(_dtInsuranceCoverageDates, ncurrentInsuranceParty);
                }
                return true;
            }
            catch
            {
                return true;
            }
            finally
            {
                if (_dtInsuranceCoverageDates != null)
                {
                    _dtInsuranceCoverageDates.Dispose();
                    _dtInsuranceCoverageDates = null;
                }
            }
        }
        private void cmdViewBenefits_Click(object sender, EventArgs e)
        {
            this.c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
            try
            {

                c1Insurance.ContextMenuStrip = null;
                Int64 nCurrentInsuranceID = Convert.ToInt64(c1Insurance.GetData(c1Insurance.RowSel, COL_INSURANCEID));
                frmViewBenefit ofrm = new frmViewBenefit(_PatientID, nCurrentInsuranceID, _DatabaseConnectionString);
                ofrm.StartPosition = FormStartPosition.CenterParent;
                ofrm.ShowDialog(this);
                LoadPatientModifiedData();
                ofrm.Dispose();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            finally
            {
                this.c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
            }
          
          
        }
        private void c1Insurance_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {                
                Int64 nCurrentInsuranceID = Convert.ToInt64(c1Insurance.GetData(c1Insurance.RowSel, COL_INSURANCEID));
                if (e.Button == MouseButtons.Right && nCurrentInsuranceID > 0)
                {
                    c1Insurance.ContextMenuStrip = cmdInsurance;
                    cmdInsurance.Items.Add(mnuViewBenefits);
                }
                else
                    c1Insurance.ContextMenuStrip = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion         

        private void btnModifyCharges_Click(object sender, EventArgs e)
        {
            if (nReplacingTransMasterID != 0)
            {
                Int64 nTransactionID = gloCharges.GetParentClaimTransactionID(nReplacingTransMasterID);
                frmBillingModifyCharges ofrmBillingModifyCharges = new frmBillingModifyCharges(this.PatientID, nTransactionID, false, _DatabaseConnectionString, _DatabaseConnectionString);
                ofrmBillingModifyCharges.WindowState = FormWindowState.Maximized;
                ofrmBillingModifyCharges.IsClaimVoided = false;
                ofrmBillingModifyCharges.ShowDialog(this);
                ofrmBillingModifyCharges.Dispose();
            }
        }

        private void rbICD9_CheckedChanged(object sender, EventArgs e)
        {
            if (rbICD9.Checked == true)
            {
                rbICD9.Font = boldFont;
                tlsICD10CoddingRules.Visible = false;
            }
            else
            {

                rbICD9.Font = regularFont;
                tlsICD10CoddingRules.Visible = true ;
            }
           
            UC_gloBillingTransactionLines.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
        }

        private void rbICD10_CheckedChanged(object sender, EventArgs e)
        {
            if (rbICD10.Checked == true)
            {
                rbICD10.Font = boldFont;
                tlsICD10CoddingRules.Visible = true;
            }
            else
            {
                tlsICD10CoddingRules.Visible = false ;
                rbICD10.Font = regularFont;
            }
            UC_gloBillingTransactionLines.IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode();
        }

        private void frmBillingTransaction_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason != CloseReason.MdiFormClosing)
                {
                    c1Insurance.FinishEditing();
                    if (oPatientControl != null) { oPatientControl.Dispose(); oPatientControl = null; }
                    if (UC_gloBillingTransactionLines != null) { UC_gloBillingTransactionLines.Dispose(); UC_gloBillingTransactionLines = null; }
                    if (oListControl != null) { oListControl.Dispose(); oListControl = null; }
                    if (frm != null) { frm.Dispose(); frm = null; }
                    if (frm_Copy != null) { frm_Copy.Dispose(); frm_Copy = null; }
                    if (_oClaimHold != null) { _oClaimHold.Dispose(); _oClaimHold = null; }
                    if (_oBox19Note != null) { _oBox19Note.Dispose(); _oBox19Note = null; }
                    if (_oBox19Notes != null) { _oBox19Notes.Clear(); _oBox19Notes.Dispose(); _oBox19Notes = null; }
                    if (ToolTip1 != null) { ToolTip1.Dispose(); ToolTip1 = null; }
                    if (oToolTip != null) { oToolTip.Dispose(); oToolTip = null; }
                    if (combo != null) { combo.Dispose(); combo = null; }
                    if (ArraylistSelectedCPTS != null) { ArraylistSelectedCPTS.Clear(); ArraylistSelectedCPTS = null; }
                    if (oEMRTransLineSplit != null) { oEMRTransLineSplit.Dispose(); oEMRTransLineSplit = null; }
                    if (oEMRTransLinesSplit != null) { oEMRTransLinesSplit.Clear(); oEMRTransLinesSplit.Dispose(); oEMRTransLinesSplit = null; }
                    if (sbEMRTreatmentLineNos != null) { sbEMRTreatmentLineNos.Clear(); }
                    if (_dtNoPostCharges != null) { _dtNoPostCharges.Clear(); _dtNoPostCharges.Dispose(); _dtNoPostCharges = null; }
                    if (_dtLoadedCPTS != null) { _dtLoadedCPTS.Clear(); _dtLoadedCPTS.Dispose(); _dtLoadedCPTS = null; }
                    if (csNoPost != null) { csNoPost = null; }
                    if (csPosted != null) { csPosted = null; }
                    if (csNoPostNormal != null) { csNoPostNormal = null; }
                    if (this.lstAppointmentIDs != null)
                    {
                        this.lstAppointmentIDs.Clear();
                        this.lstAppointmentIDs = null;
                    }

                    if (this.ListOfPatientAppointments != null)
                    {
                        this.ListOfPatientAppointments.Clear();
                        this.ListOfPatientAppointments = null;
                    }
                }
            }
            catch //Blank catch
            {

            }
        }
              
        //added new event by manoj jadhav on 20150202 to show or hide Spilit screen on F7 key V8040
        private void mnuBilling_ShowSplitScreen_Click(object sender, EventArgs e)
        {
            try
            {
                //uiPanSplitScreen.AutoHide = true;
                if (gloGlobal.gloPMGlobal.ViewDocumentsOnCharges)
                    uiPanSplitScreen.AutoHideActive =!uiPanSplitScreen.AutoHideActive;
            }
            catch (Exception)
            {}
        }

        #region ICD 10
        private void tlsICD10CoddingRules_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbICD10.Checked)
                {
                    if (UC_gloBillingTransactionLines != null && UC_gloBillingTransactionLines.CurrentTransactionLine > 0)
                    {                        
                        if (UC_gloBillingTransactionLines.CurrentColumn == UC_gloBillingTransactionLines.Col_DxCode1 ||
                            UC_gloBillingTransactionLines.CurrentColumn == UC_gloBillingTransactionLines.Col_DxCode2 ||
                            UC_gloBillingTransactionLines.CurrentColumn == UC_gloBillingTransactionLines.Col_DxCode3 ||
                            UC_gloBillingTransactionLines.CurrentColumn == UC_gloBillingTransactionLines.Col_DxCode4)
                        {
                            object DxCode, DxDescripton = null;
                            DxCode = UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, UC_gloBillingTransactionLines.CurrentColumn);

                            switch (UC_gloBillingTransactionLines.CurrentColumn)
                            {
                                case 14:
                                    DxDescripton = UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, UC_gloBillingTransactionLines.Col_DxDescription1);
                                    break;
                                case 17:
                                    DxDescripton = UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, UC_gloBillingTransactionLines.Col_DxDescription2);
                                    break;
                                case 20:
                                    DxDescripton = UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, UC_gloBillingTransactionLines.Col_DxDescription3);
                                    break;
                                case 23:
                                    DxDescripton = UC_gloBillingTransactionLines.GetItem(UC_gloBillingTransactionLines.CurrentTransactionLine, UC_gloBillingTransactionLines.Col_DxDescription4);
                                    break;

                            }
                            string ICDcode = "";
                            string ICDDescripton = "";
                            if (DxCode != null)
                            {
                                ICDcode = Convert.ToString(DxCode);
                            }
                            if (DxDescripton != null)
                            {
                                ICDDescripton = Convert.ToString(DxDescripton);

                            }
                            if (ICDcode != "" && ICDDescripton != "")
                            {
                                if (gloCharges.GetICDRevision(ICDcode.Trim()) == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                                {
                                    gloUIControlLibrary.WPFForms.frmShowCodingRules objCodingRules = new gloUIControlLibrary.WPFForms.frmShowCodingRules(ICDcode, ICDDescripton, _DatabaseConnectionString);

                                    System.Windows.Interop.WindowInteropHelper interopHelper = new System.Windows.Interop.WindowInteropHelper(objCodingRules);
                                    interopHelper.Owner = this.Handle;

                                    objCodingRules.LoadNotes();

                                    if (objCodingRules.NoData)
                                    {
                                        MessageBox.Show("No notes available for code " + ICDcode, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    { objCodingRules.ShowDialog(); }


                                    if (objCodingRules != null)
                                    { objCodingRules = null; }

                                    if (interopHelper != null)
                                    {
                                        interopHelper = null;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please select ICD10 code or category to view coding rules.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please select ICD10 code or category to view coding rules.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
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
        #endregion

        #region Claim Rules
        private Int64 GetInsuranceID()
        {
            Int64 nInsuranceID = 0;

            try
            {
                if (c1Insurance.Rows.Count == 2)
                {
                    nInsuranceID = Convert.ToInt64(c1Insurance.GetData(1, 2));
                }
                else
                {
                    for (int i = 0; i <= c1Insurance.Rows.Count; i++)
                    {
                        if (Convert.ToString(c1Insurance.GetData(i, 1)) == "1")
                        {
                            nInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, 2));
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return nInsuranceID;
        }

        private string GetInsuranceIDs()
        {
            string sReturned = string.Empty;
            StringBuilder sb = new StringBuilder();

            try
            {                
                for (Int32 counter = 1; counter <= c1Insurance.Rows.Count - 1; counter++)
                {
                    if (Convert.ToString(c1Insurance.GetData(counter, COL_INSURANCERESPONSIBILITY)).Replace("\0", "") != "")
                    {
                        sb.Append(Convert.ToString(c1Insurance.GetData(counter, COL_INSURANCEID)));
                        if (counter <= c1Insurance.Rows.Count - 2)
                        { sb.Append(","); }
                    }                    
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return sb.ToString();
            }
        }




        private List<ChargeRules.Claim> GenerateClaimRuleObject()
        {
            List<ChargeRules.Claim> claims = new List<ChargeRules.Claim>();

            string sInsuranceCompanyName = string.Empty;
            string sReferringProviderNPI = string.Empty;
            string sOrderingProviderNPI = string.Empty;
            string sSupervisingProviderNPI = string.Empty;
            string sBillingProviderNPI = string.Empty;
            string sPatientRelationshipToSubscriber = string.Empty;
            string sInsurancePlanName = string.Empty;
            string sPlanType = string.Empty;
            string sReportingCategory = string.Empty;
            string sInsCompanyReportingCategory = string.Empty;
            string sPayerID = string.Empty;
            string sInsuranceIDs = string.Empty;
            List<Insurance> lstInsurance = null;
            string primaryInsuranceID = string.Empty;
            string sPriorAuthorization = string.Empty;

            DataSet ds = null;            
            try
            {
                lstInsurance = new List<Insurance>();
                primaryInsuranceID = Convert.ToString(GetInsuranceID());
                gloFacility ogloFacility= new gloFacility(_DatabaseConnectionString);
                Facility oFacility = ogloFacility.GetFacility(Convert.ToInt64(cmbFacility.SelectedValue));
                ogloFacility.Dispose();
                ogloFacility = null;



                // If _nResponsibleParty is 0 then Patient Insurance is self.                
                // Skipping Charge Rules object filling in this case.
                if (UC_gloBillingTransactionLines != null && _nResponsibleParty != 0)
                {
                    TransactionLines tranLines = UC_gloBillingTransactionLines.GetLineTransactions();                    

                    if (tranLines != null && tranLines.Count > 0)
                    {
                        sInsuranceIDs = this.GetInsuranceIDs();

                        ds = gloCharges.ClaimRulesGetRequiredInformation(sInsuranceIDs,
                           Convert.ToInt64(cmbBillingProvider.SelectedValue),
                           Convert.ToInt64(cmbReferralProvider.SelectedValue),                           
                           _PatientID);

                        DataRow dRow = ds.Tables[0].AsEnumerable()
                                        .FirstOrDefault(p => Convert.ToString(p["nInsuranceID"]) == primaryInsuranceID);
                        
                        if (dRow != null)
                        {
                            sInsuranceCompanyName = Convert.ToString(dRow["InsuranceCompanyName"]);
                            sPlanType = Convert.ToString(dRow["InsurancePlanTypeDesc"]);
                            sReportingCategory = Convert.ToString(dRow["InsurancePlanReportingCategory"]);
                            sPayerID = Convert.ToString(dRow["InsurancePlanPayerID"]);
                            
                            sPatientRelationshipToSubscriber = Convert.ToString(dRow["PatientSubscriberRelationShip"]);
                            sInsurancePlanName = Convert.ToString(dRow["InsurancePlanName"]);
                            sInsCompanyReportingCategory = Convert.ToString(dRow["InsuranceCompanyReportingCategory"]);

                        }
                        
                        if (txtPriorAuthorizationNo.Text == "<available>")
                            sPriorAuthorization = "";
                        else
                            sPriorAuthorization = txtPriorAuthorizationNo.Text;
                        
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            if (Convert.ToString(cmbProviderType.SelectedValue) == "DN")
                            { sReferringProviderNPI = Convert.ToString(ds.Tables[1].Rows[0]["sNPI"]); }

                            else if (Convert.ToString(cmbProviderType.SelectedValue) == "DK")
                            { sOrderingProviderNPI = Convert.ToString(ds.Tables[1].Rows[0]["sNPI"]); }

                            else
                            { sSupervisingProviderNPI = Convert.ToString(ds.Tables[1].Rows[0]["sNPI"]); }
                        }       

                        if (ds.Tables[2].Rows.Count > 0) { sBillingProviderNPI = Convert.ToString(ds.Tables[2].Rows[0]["sNPI"]); }
                                                                  
                        foreach (DataRow elementRow in ds.Tables[0].Rows)
                        {
                            ChargeRules.Insurance insurance = new Insurance();

                            insurance.InsurancePayerID = Convert.ToString(elementRow["InsurancePlanPayerID"]);
                            insurance.InsurancePlanName = Convert.ToString(elementRow["InsurancePlanName"]);                          
                            insurance.InsurancePlanType = Convert.ToString(elementRow["InsurancePlanTypeDesc"]);
                            insurance.InsuranceReportingCategory = Convert.ToString(elementRow["InsurancePlanReportingCategory"]);
                            insurance.InsuranceCompanyName = Convert.ToString(elementRow["InsuranceCompanyName"]); ;
                            insurance.InsuranceCompanyReportingCategory = Convert.ToString(elementRow["InsuranceCompanyReportingCategory"]);

                            insurance.ContactID = Convert.ToInt64(elementRow["nContactID"]);
                            insurance.InsuranceID = Convert.ToInt64(elementRow["nInsuranceID"]);

                            lstInsurance.Add(insurance);
                            insurance = null;
                        }

                        Claim_Facility oClaimFacility = new Claim_Facility();
                        oClaimFacility.ClaimFacility = oFacility.FacilityName;
                        oClaimFacility.FacilityState = oFacility.State;
                        oClaimFacility.FacilityTaxonomy = oFacility.TaxonomyCode;
                        oClaimFacility.AUSID = gloGlobal.gloPMGlobal.AusID;

                        oFacility.Dispose();
                        oFacility = null;

                        for (Int32 i = 0; i <= tranLines.Count - 1; i++)
                        {
                            TransactionLine tran = tranLines[i];

                            if (!tran.bIsSelfClaim)
                            {
                                ChargeRules.Claim claim = new ChargeRules.Claim();

                                claim.InsuranceList.AddRange(lstInsurance);

                                for (Int32 c = 0; c <= tranLines.Count - 1; c++)
                                { 
                                    claim.CPTCodes.Add(new CPT_Code() { CPTCode = tranLines[c].CPTCode });

                                    if (!claim.ClaimDiagnosis.Any(t => t.ClaimDiagnosis == Convert.ToString(tranLines[c].Dx1Code).Trim()))
                                    {
                                        claim.ClaimDiagnosis.Add(new Claim_Diagnosis()
                                        {
                                            ClaimDiagnosis = Convert.ToString(tranLines[c].Dx1Code).Trim()
                                        });
                                    }

                                    if (!claim.ClaimDiagnosis.Any(t => t.ClaimDiagnosis == Convert.ToString(tranLines[c].Dx2Code).Trim()))
                                    {
                                        claim.ClaimDiagnosis.Add(new Claim_Diagnosis()
                                        {
                                            ClaimDiagnosis = Convert.ToString(tranLines[c].Dx2Code).Trim()
                                        });
                                    }

                                    if (!claim.ClaimDiagnosis.Any(t => t.ClaimDiagnosis == Convert.ToString(tranLines[c].Dx3Code).Trim()))
                                    {
                                        claim.ClaimDiagnosis.Add(new Claim_Diagnosis()
                                        {
                                            ClaimDiagnosis = Convert.ToString(tranLines[c].Dx3Code).Trim()
                                        });
                                    }

                                    if (!claim.ClaimDiagnosis.Any(t => t.ClaimDiagnosis == Convert.ToString(tranLines[c].Dx4Code).Trim()))
                                    {
                                        claim.ClaimDiagnosis.Add(new Claim_Diagnosis()
                                        {
                                            ClaimDiagnosis = Convert.ToString(tranLines[c].Dx4Code).Trim()
                                        });
                                    }


                                    if (!claim.ClaimModfiers.Any(t => t.ClaimModifier == Convert.ToString(tranLines[c].Mod1Code).Trim()))
                                    {
                                        claim.ClaimModfiers.Add(new Claim_Modfier()
                                        {
                                            ClaimModifier = Convert.ToString(tranLines[c].Mod1Code).Trim()
                                        });

                                    }

                                    if (!claim.ClaimModfiers.Any(t => t.ClaimModifier == Convert.ToString(tranLines[c].Mod2Code).Trim()))
                                    {
                                        claim.ClaimModfiers.Add(new Claim_Modfier()
                                        {
                                            ClaimModifier = Convert.ToString(tranLines[c].Mod2Code).Trim()
                                        });

                                    }

                                    if (!claim.ClaimModfiers.Any(t => t.ClaimModifier == Convert.ToString(tranLines[c].Mod3Code).Trim()))
                                    {
                                        claim.ClaimModfiers.Add(new Claim_Modfier()
                                        {
                                            ClaimModifier = Convert.ToString(tranLines[c].Mod3Code).Trim()
                                        });

                                    }

                                    // bool a = claim.oPropertyValueWithExistsOperator.Any(t => t.PropertyCode == "");

                                    //PropertyValueWithExistsOperator otest = new PropertyValueWithExistsOperator();
                                    //otest.PropertyCode="";
                                    //IEnumerable<PropertyValueWithExistsOperator> oPpertyValueWithExistsOperator = from PropertyValue in claim.oPropertyValueWithExistsOperator.AsEnumerable() where PropertyValue.PropertyCode == Convert.ToString("") select PropertyValue;

                                    //var test = claim.oPropertyValueWithExistsOperator.Select(t => t.PropertyCode == otest.PropertyCode);

                                    if (!claim.ClaimModfiers.Any(t => t.ClaimModifier == Convert.ToString(tranLines[c].Mod4Code).Trim()))
                                    {
                                        claim.ClaimModfiers.Add(new Claim_Modfier()
                                        {
                                            ClaimModifier = Convert.ToString(tranLines[c].Mod4Code).Trim()
                                        });

                                    }

                                    if (!claim.RenderringProviderNames.Any(t => t.RenderingProviderName == Convert.ToString(tranLines[c].RefferingProvider).Trim()))
                                    {
                                        claim.RenderringProviderNames.Add(new RenderringProvider_Name()
                                        {
                                            RenderingProviderName = Convert.ToString(tranLines[c].RefferingProvider).Trim()
                                        });

                                    }

                                    if (!claim.RenderringProviderNPIs.Any(t => t.RenderingProviderNPI == Convert.ToString(ClsRuleEngine.GetBillingProviderNPI(Convert.ToInt64(tranLines[c].RefferingProviderId))).Trim()))
                                    {
                                        claim.RenderringProviderNPIs.Add(new RenderringProvider_NPI()
                                        {
                                            RenderingProviderNPI = Convert.ToString((ClsRuleEngine.GetBillingProviderNPI(Convert.ToInt64(tranLines[c].RefferingProviderId)))).Trim()
                                        });

                                    }  
                                }

                                claim.POS = tran.POSCode;
                                claim.InsuranceReportingCategory = sReportingCategory;
                                claim.InsuranceCompanyReportingCategory = sInsCompanyReportingCategory;
                                claim.InsurancePlanType = sPlanType;
                                claim.InsurancePayerID = sPayerID;

                                claim.ClaimNumber = txtClaimNo.Text;
                                claim.PatientId = this.PatientID;

                                claim.BillingProviderName = cmbBillingProvider.Text.Trim();
                                claim.BillingProviderNPI = sBillingProviderNPI;

                                claim.InsuranceCompanyName = sInsuranceCompanyName.Trim();
                                claim.InsurancePlanName = sInsurancePlanName;
                                claim.PatientRelationshipToSubscriber = sPatientRelationshipToSubscriber;

                                if (Convert.ToString(cmbProviderType.SelectedValue) == "DN")
                                {
                                    claim.ReferringProviderName = cmbReferralProvider.Text.Trim();
                                    claim.ReferringProviderID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                                    claim.ReferringProviderNPI = sReferringProviderNPI.Trim();
                                }
                                else if (Convert.ToString(cmbProviderType.SelectedValue) == "DK")
                                {
                                    claim.OrderingProviderName = cmbReferralProvider.Text.Trim();
                                    claim.OrderingProviderID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                                    claim.OrderingProviderNPI = sOrderingProviderNPI.Trim();
                                }
                                else
                                {
                                    claim.SupervisingProviderName = cmbReferralProvider.Text.Trim();
                                    claim.SupervisingProviderID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                                    claim.SupervisingProviderNPI = sSupervisingProviderNPI.Trim();
                                }

                                if (IsValidDate(mskOnsiteDate.Text))
                                { claim.ClaimDate = Convert.ToDateTime(mskOnsiteDate.Text.ToString()); }
                                claim.ClaimDateQualifier = Convert.ToString(cmbBox14DateQualifier.SelectedValue);

                                if (IsValidDate(mskBox15Date.Text))
                                { claim.OtherClaimDate = Convert.ToDateTime(mskBox15Date.Text.ToString()); }

                                claim.OtherClaimDateQualifier = Convert.ToString(cmbBox15DateQualifier.SelectedValue);
                                claim.CPTCode = tran.CPTCode.Trim();

                                claim.Dx1Code = tran.Dx1Code.Trim();
                                claim.Dx2Code = tran.Dx2Code.Trim();
                                claim.Dx3Code = tran.Dx3Code.Trim();
                                claim.Dx4Code = tran.Dx4Code.Trim();

                                claim.Mod1Code = tran.Mod1Code.Trim();
                                claim.Mod2Code = tran.Mod2Code.Trim();
                                claim.Mod3Code = tran.Mod3Code.Trim();
                                claim.Mod4Code = tran.Mod4Code.Trim();

                                claim.Unit = tran.Unit;
                                claim.FacilityName = Convert.ToString(cmbFacility.Text).Trim();

                                claim.ChargeFromDOS = tran.DateServiceFrom;
                                claim.ChargeToDOS = tran.DateServiceTill;

                                if (IsValidDate(mskHospitaliztionFrom.Text))
                                { claim.HospitalizationFromDOS = Convert.ToDateTime(mskHospitaliztionFrom.Text.ToString()); }

                                if (IsValidDate(mskHospitaliztionTo.Text))
                                { claim.HospitalizationToDOS = Convert.ToDateTime(mskHospitaliztionTo.Text.ToString()); }

                                gloStripControl.DateOfBirthIntegers dateOfBirth = new gloStripControl.DateOfBirthIntegers();
                                dateOfBirth = oPatientControl.GetAge(oPatientControl.PatientDateOfBirth);

                                claim.AgeYears = dateOfBirth.DOBYears;
                                claim.AgeMonths = dateOfBirth.DOBMonths;
                                claim.AgeDays = dateOfBirth.DOBDays;

                                if (oPatientControl.PatientGender == "Male") { claim.PatientGender = enumGender.Male.ToString(); }
                                else if (oPatientControl.PatientGender == "Female") { claim.PatientGender = enumGender.Female.ToString(); }
                                else { claim.PatientGender = enumGender.Other.ToString(); }

                                claim.PriorAuthorization = sPriorAuthorization;
                                claim.RenderingProviderName = Convert.ToString(tran.RefferingProvider).Trim();
                                claim.RenderingProviderNPI = ClsRuleEngine.GetBillingProviderNPI(Convert.ToInt64(tran.RefferingProviderId));

                                claim.ClaimFacility = oClaimFacility;
                                claim.TransactionMasterID = 0;
                                claim.TransactionID = 0;

                                claims.Add(claim);

                                claim = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }

                if (lstInsurance != null)
                {
                    lstInsurance.Clear();
                    lstInsurance = null;
                }
            }

            return claims;
        }

        //private Boolean ValidateRules()
        //{
        //    List<ChargeRules.Claim> ClaimsList = null;           
        //    ChargeRules.BusinessOperation businessOperation = null;
        //    Boolean dlgResultReviewScren = true;
        //    try
        //    {
        //        ClaimsList = this.GenerateClaimRuleObject();
        //        businessOperation = new BusinessOperation();
        //        triggeredRuleInfo = businessOperation.EvaluateRules(ClaimsList, false);
        //        if (triggeredRuleInfo != null && triggeredRuleInfo.Count > 0)
        //        {
        //            gloUIControlLibrary.WPFForms.frmTriggeredRules frmTriggeredRules = new gloUIControlLibrary.WPFForms.frmTriggeredRules(triggeredRuleInfo);
        //            System.Windows.Interop.WindowInteropHelper _interophelper = new System.Windows.Interop.WindowInteropHelper(frmTriggeredRules);
        //            _interophelper.Owner = this.Handle;
        //            frmTriggeredRules.ShowDialog();
        //            dlgResultReviewScren = Convert.ToBoolean(frmTriggeredRules.DialogResult);
        //        }
        //        return dlgResultReviewScren;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
        //        return false;
        //    }
        //}
        private Boolean ValidateRules()
        {
            List<ChargeRules.Claim> ClaimsList = null;
            ChargeRules.BusinessOperation businessOperation = null;
            Boolean dlgResultReviewScren = true;
            List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> lstTriggeredRules = new List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo>();
            triggeredRuleInfoGlobal = new List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo>();
            try
            {
                ClaimsList = this.GenerateClaimRuleObject();
                if (gloGlobal.gloPMGlobal.IsCommunicationServiceEnable && Uri.IsWellFormedUriString(gloGlobal.gloPMGlobal.sCommunicationServiceURL, UriKind.Absolute) == true)
                {
                    #region "Global Rules"
                    try
                    {
                        var JSONClaimList = Newtonsoft.Json.JsonConvert.SerializeObject(ClaimsList);

                        WSHttpBinding httpsws = new WSHttpBinding("WSHttpBinding_IQCommunicatorService");
                        httpsws.Security.Mode = SecurityMode.Transport;
                        string strEPAddress = gloGlobal.gloPMGlobal.sCommunicationServiceURL;
                        EndpointAddress ep = new EndpointAddress(strEPAddress);
                        ChargeRules.QCommService.QCommunicatorServiceClient client = new ChargeRules.QCommService.QCommunicatorServiceClient(httpsws, ep);

                        string sRuleResult = client.PopulateRules(JSONClaimList);
                        triggeredRuleInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo>>(sRuleResult);
                        if (triggeredRuleInfo != null && triggeredRuleInfo.Count > 0)
                        {
                            foreach (var item in triggeredRuleInfo)
                            {
                                lstTriggeredRules.Add(item);
                                triggeredRuleInfoGlobal.Add(item);
                            }
                            if (triggeredRuleInfo != null && triggeredRuleInfo.Count > 0)
                            {
                                DataTable dtRule = new DataTable();

                                dtRule.Columns.Add("nRuleID");
                                dtRule.Columns.Add("sRuleName");
                                dtRule.Columns.Add("sRuleDescription");
                                dtRule.Columns.Add("nRuleType");
                                dtRule.Columns.Add("sErrorMessage");
                                dtRule.Columns.Add("sRuleCategory");

                                foreach (gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo oRule in triggeredRuleInfo)
                                {

                                    dtRule.Rows.Add();
                                    dtRule.Rows[dtRule.Rows.Count - 1]["nRuleID"] = oRule.RuleId;
                                    dtRule.Rows[dtRule.Rows.Count - 1]["sRuleName"] = oRule.RuleName;
                                    dtRule.Rows[dtRule.Rows.Count - 1]["sRuleDescription"] = oRule.RuleMessage;
                                    dtRule.Rows[dtRule.Rows.Count - 1]["nRuleType"] = oRule.RuleTypeInfo.GetHashCode();
                                    dtRule.Rows[dtRule.Rows.Count - 1]["sErrorMessage"] = oRule.RuleMessage;
                                    dtRule.Rows[dtRule.Rows.Count - 1]["sRuleCategory"] = oRule.RuleCategory;
                                }
                                gloCharges.SaveGlobalTriggeredRules(dtRule);
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleEvaluation, gloAuditTrail.ActivityType.SaveWithErrors, "Claim saved with errors", 0, TransactionClaimID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                            }
                        }
                        triggeredRuleInfo = null;
                    }
                    catch (Exception ex)
                    {
                        triggeredRuleInfo = null;
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    }
                    #endregion
                }

                businessOperation = new BusinessOperation();
                triggeredRuleInfo = businessOperation.EvaluateRules(ClaimsList, false);
                if (triggeredRuleInfo != null && triggeredRuleInfo.Count > 0)
                {
                    foreach (var item in triggeredRuleInfo)
                    {
                        lstTriggeredRules.Add(item);
                        triggeredRuleInfoGlobal.Add(item);
                    }
                }

                //if (triggeredRuleInfo != null && triggeredRuleInfo.Count > 0)
                //{
                //    gloUIControlLibrary.WPFForms.frmTriggeredRules frmTriggeredRules = new gloUIControlLibrary.WPFForms.frmTriggeredRules(triggeredRuleInfo);
                //    System.Windows.Interop.WindowInteropHelper _interophelper = new System.Windows.Interop.WindowInteropHelper(frmTriggeredRules);
                //    _interophelper.Owner = this.Handle;
                //    frmTriggeredRules.ShowDialog();
                //    dlgResultReviewScren = Convert.ToBoolean(frmTriggeredRules.DialogResult);
                //}
                if (lstTriggeredRules != null && lstTriggeredRules.Count > 0)
                {
                    gloUIControlLibrary.WPFForms.frmTriggeredRules frmTriggeredRules = new gloUIControlLibrary.WPFForms.frmTriggeredRules(lstTriggeredRules);
                    System.Windows.Interop.WindowInteropHelper _interophelper = new System.Windows.Interop.WindowInteropHelper(frmTriggeredRules);
                    _interophelper.Owner = this.Handle;
                    frmTriggeredRules.ShowDialog();
                    dlgResultReviewScren = Convert.ToBoolean(frmTriggeredRules.DialogResult);
                }
                return dlgResultReviewScren;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return false;
            }
        }

        private Boolean IsClaimRulesEnabled()
        {

            DataTable dt = null;
            Boolean result = false;
            try
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                oDB.Connect(false);
                string _sqlRetrieveQuery = String.Empty;
                _sqlRetrieveQuery = "SELECT sSettingsValue As ClaimRuleEnabled FROM dbo.settings WHERE sSettingsName='bEnableclaimRule'";
                if (_sqlRetrieveQuery != "")
                {
                    oDB.Retrive_Query(_sqlRetrieveQuery, out dt);
                }
                oDB.Dispose();
                oDB = null;

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        result = Convert.ToBoolean(dt.Rows[0]["ClaimRuleEnabled"]);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            return result;
        }
        #endregion

        #region Patient Appointments linking with Charges

        private bool ValidateAppointmentsLinkingToCharges()
        {
            bool bReturned = false;
            try
            {
                if (this.lstAppointmentIDs.Any())
                {                    
                    if (UC_gloBillingTransactionLines != null)
                    {
                        if (this.MorePatientAppointmentsAvailable && gloGlobal.gloPMGlobal.EnableAppointmentLinkingToCharges)
                        {
                            if (MessageBox.Show("More patient appointments are available for the DOS that are selected. Do you want to link them?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            { bReturned = false; }
                            else { bReturned = true; }
                        }
                        else { bReturned = true; }

                        if (bReturned == true)
                        {
                            using (DataTable dtTVP = new DataTable())
                            {
                                List<Int32> lstDates = new List<Int32>();

                                long nMinimumDOS = UC_gloBillingTransactionLines.GetMinDOS();
                                long nMaximumFromDOS = UC_gloBillingTransactionLines.GetLastMaxDOS();

                                long nMaximumDOS = UC_gloBillingTransactionLines.GetMaxToDOS();

                                if (nMaximumDOS == 0 || nMaximumDOS < nMaximumFromDOS) { nMaximumDOS = nMaximumFromDOS; }

                                dtTVP.Columns.Add("nAppointmentIDs", System.Type.GetType("System.Int64"));

                                foreach (Int64 i in this.lstAppointmentIDs)
                                {
                                    DataRow dRow = dtTVP.NewRow();
                                    dRow["nAppointmentIDs"] = i;
                                    dtTVP.Rows.Add(dRow);
                                }

                                using (DataTable dtReturned = gloCharges.GetTVPAppointments(dtTVP))
                                {
                                    lstDates = dtReturned
                                      .AsEnumerable()
                                      .Select(p => gloDateMaster.gloDate.DateAsNumber(Convert.ToString(p["AppointmentDate"])))
                                      .ToList();
                                }
                                if (gloGlobal.gloPMGlobal.EnableAppointmentLinkingToCharges == true)
                                {
                                    //Case 1: Associated date is less than Service line FromDOS
                                    if ((lstDates.Any(p => p < nMinimumDOS)))
                                    { bReturned = false; }
                                    else
                                    { bReturned = true; }

                                    if (bReturned == true && nMaximumDOS > 0)
                                    {
                                        //Case 2: Associated date is more than Service line ToDOS
                                        if (lstDates.Any(p => p > nMaximumDOS))
                                        { bReturned = false; }
                                        else
                                        { bReturned = true; }
                                    }

                                    if (bReturned == true && nMaximumDOS == 0)
                                    {
                                        //Case 3: Associated date is more than Service line FromDOS
                                        if (lstDates.Any(p => p > nMaximumFromDOS))
                                        { bReturned = false; }
                                        else
                                        { bReturned = true; }
                                    }

                                    if (bReturned == false)
                                    {
                                        if (MessageBox.Show("Charge DOS are changed. Do you want to re-associate selected patient appointments?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                        { bReturned = false; }
                                        else { bReturned = true; }
                                    }
                                }
                            }
                        }                                                                        
                    }                                     
                }
                else if (this.PatientHasAppointments && !this.lstAppointmentIDs.Any() && gloGlobal.gloPMGlobal.EnableAppointmentLinkingToCharges)
                {
                    if (MessageBox.Show("Patient has appointments that can be linked to this Charge. Do you want to link them?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    { bReturned = false; }
                    else { bReturned = true; }
                }
                else { bReturned = true; }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return bReturned;
        }
        
        private void btnPatientAppointments_Click(object sender, EventArgs e)
        {
           
            gloPMGeneral.gloAppointmentsChargesLinking.frmPatientAppointments frmPatientAppointments = null;

            DataTable dtAppointments = null;

            try
            {
                if (UC_gloBillingTransactionLines != null)
                {                                        
                    dtAppointments = this.GetPatientAppointments();
                    
                    if (dtAppointments != null && dtAppointments.Rows.Count > 0)
                    {
                        long nMinimumDOS = UC_gloBillingTransactionLines.GetMinDOS();
                        long nMaximumFromDOS = UC_gloBillingTransactionLines.GetLastMaxDOS();

                        long nMaximumDOS = UC_gloBillingTransactionLines.GetMaxToDOS();

                        if (nMaximumDOS == 0 || nMaximumDOS < nMaximumFromDOS)
                        {
                            nMaximumDOS = nMaximumFromDOS;
                        }                       

                        frmPatientAppointments = new gloPMGeneral.gloAppointmentsChargesLinking.frmPatientAppointments(_DatabaseConnectionString, _PatientID, nMinimumDOS, nMaximumDOS);
                        frmPatientAppointments.AppointmentsData = dtAppointments;
                        this.ListOfPatientAppointments.Clear();

                        if (this.lstAppointmentIDs.Any())
                        { foreach (Int64 element in this.lstAppointmentIDs)  { frmPatientAppointments.Appointments.Add(element); }  }

                        this.LoadAppointmentsInList(dtAppointments);
                        this.MorePatientAppointmentsAvailable = false;

                        frmPatientAppointments.ShowDialog(this);

                        if (frmPatientAppointments.AppointmentsSet)
                        {
                            this.lstAppointmentIDs.Clear();                            
                            if (frmPatientAppointments.AppointmentIDs.Any())
                            {
                                foreach (Int64 element in frmPatientAppointments.AppointmentIDs)
                                { if (!this.lstAppointmentIDs.Contains(element)) { this.lstAppointmentIDs.Add(element); } }
                            }
                            else { this.lstAppointmentIDs.Clear(); }
                        }
                        this.CheckForPriorAppointments();
                        
                    }
                    else
                    { 
                        this.lstAppointmentIDs.Clear();
                        this.CheckForPriorAppointments();
                        MessageBox.Show("No appointments were found for this patient.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                if (frmPatientAppointments != null)
                {
                    frmPatientAppointments.Dispose();
                    frmPatientAppointments = null;
                }

                if (dtAppointments != null)
                {
                    dtAppointments.Dispose();
                    dtAppointments = null;
                }
            }
        }

        private DataTable GetPatientAppointments()
        {
            DataTable dtReturned = null;

            try
            {
                if (UC_gloBillingTransactionLines != null)
                {
                    DataTable dtTVP = new DataTable();
                    dtTVP.Columns.Add("nAppointmentIDs", System.Type.GetType("System.Int64"));

                    foreach (Int64 i in this.lstAppointmentIDs)
                    {
                        DataRow dRow = dtTVP.NewRow();
                        dRow["nAppointmentIDs"] = i;
                        dtTVP.Rows.Add(dRow);
                    }

                    long nMinimumDOS = UC_gloBillingTransactionLines.GetMinDOS();
                    long nMaximumFromDOS = UC_gloBillingTransactionLines.GetLastMaxDOS();

                    long nMaximumDOS = UC_gloBillingTransactionLines.GetMaxToDOS();

                    if (nMaximumDOS == 0 || nMaximumDOS < nMaximumFromDOS) { nMaximumDOS = nMaximumFromDOS; }
                    
                    dtReturned = gloCharges.GetAppointments(this.PatientID, nMinimumDOS, nMaximumDOS, dtTVP);

                    if (dtTVP != null)
                    {
                        dtTVP.Dispose();
                        dtTVP = null;
                    }
                }                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dtReturned;            
        }

        private void btnClearPatientAppointments_Click(object sender, EventArgs e)
        {
            DataTable dtAppointments = null;

            try
            {
                this.lstAppointmentIDs.Clear();
                this.ListOfPatientAppointments.Clear();
                this.MorePatientAppointmentsAvailable = false;

                dtAppointments = this.GetPatientAppointments();

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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dtAppointments != null)
                {
                    dtAppointments.Dispose();
                    dtAppointments = null;
                }
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

        void UC_gloBillingTransactionLines_date_Changed(object sender, RowColEventArgs e)
        {
            //List<PatientAppointment> lstPatientAppointments = null;

            try
            {                
                using (DataTable dtAppointments = this.GetPatientAppointments())
                {
                    if (dtAppointments != null && dtAppointments.Rows.Count > 0)
                    {
                        //lstPatientAppointments = dtAppointments.AsEnumerable().Select(p => new PatientAppointment() { AppointmentID = Convert.ToInt64(p["ID"]) }).ToList();

                        //if (lstPatientAppointments.Except(this.ListOfPatientAppointments).Any())
                        //{
                        //    //MessageBox.Show("More Patient Appointments available.");
                        //    this.MorePatientAppointmentsAvailable = true;
                        //}
                        //else
                        //{ 
                        //    //MessageBox.Show("Patient Appointments matched.");
                        //    this.MorePatientAppointmentsAvailable = false;
                        //}

                        this.CheckMorePatientAppointments(dtAppointments);
                        if (!this.lstAppointmentIDs.Any())
                        {
                            long nMinimumDOS = UC_gloBillingTransactionLines.GetMinDOS();
                            long nMaximumFromDOS = UC_gloBillingTransactionLines.GetLastMaxDOS();
                            long nMaximumDOS = UC_gloBillingTransactionLines.GetMaxToDOS();

                            if (nMaximumDOS == 0 || nMaximumDOS < nMaximumFromDOS) { nMaximumDOS = nMaximumFromDOS; }

                            if (nMinimumDOS == nMaximumDOS && dtAppointments.Rows.Count == 1)
                            {
                                this.CheckForPriorAppointments(dtAppointments);
                                this.MorePatientAppointmentsAvailable = false;
                            }
                            else
                            {
                                this.SetPatientAppointmentsAvailable();
                                this.PatientHasAppointments = true;
                            }
                        }
                    }
                    else
                    {
                        this.ResetPatientAppointments();
                        this.PatientHasAppointments = false;
                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    if (lstPatientAppointments != null)
            //    {
            //        lstPatientAppointments.Clear();
            //        lstPatientAppointments = null;
            //    }
            //}
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


        private void tlb_ReportEMRTreatment_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptEMRTreatmentException", "EMR Treatment Exception", true, null);
        }

        private void ShowSSRSReport(string ReportName, string ReportTitle, bool blnIsgloStreamReport, Image img)
        {
            Cursor.Current = Cursors.WaitCursor;
            SSRSApplication.frmSSRSViewer frmSSRS = new SSRSApplication.frmSSRSViewer();
            frmSSRS.Conn = _DatabaseConnectionString;
            frmSSRS.reportName = ReportName;
            frmSSRS.reportTitle = ReportTitle;
            frmSSRS.formIcon = img;
            frmSSRS.IsgloStreamReport = blnIsgloStreamReport;
            Cursor.Current = Cursors.Default;
            frmSSRS.ShowDialog();
        }

        private void tls_btnOnlineCharge_Click(object sender, EventArgs e)
        {
            if (_IsPatientAccountFeature)
            {
                if (!IsValidatePatientAccount(this.nPAccountID, this.PatientID))
                {
                    return;
                }
            }
                tls_Top.Select();
                this.Cursor = Cursors.WaitCursor;
                BindOnlineCharges();

                ShowHideControls(ShowHideType.OnlineCharge);
                this.Cursor = Cursors.Default;

            
        }

        private void c1OnlineCharge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HitTestInfo hitInfo = c1OnlineCharge.HitTest(e.X, e.Y);
                if (hitInfo.Row > 0)
                {
                    LoadOnlineCharge(hitInfo);

                    C1Dignosis.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
                    C1Dignosis.OwnerDrawCell += c1FlexGrid1_OwnerDrawCell;
                }
                else
                {
                    if (hitInfo.Row == 0)
                    {
                        if (c1OnlineCharge.Cols[hitInfo.Column].Name.ToString().Trim() != "")
                        {
                            lblOCPSearch.Text = c1OnlineCharge.Cols[hitInfo.Column].Name.ToString() + ":";
                            lblOCPSearch.Tag = hitInfo.Column;
                            txtSearch.Focus();
                        }
                    }
                }
            }
        }
        private void tlb_CancelOnlineCharge_Click(object sender, EventArgs e)
        {
            IsOnlineChargeBind = true;
            this.Cursor = Cursors.WaitCursor;
            tls_Top.Select();

            _PortalClaimID = 0;
            _OnlinePatientID = 0;
            _OnlineProviderID = 0;
            if (_IsValidDate)
            {
                if (_dtNoPostCharges != null && _dtNoPostCharges.Rows.Count > 0)
                {
                    _dtNoPostCharges.Rows.Clear();
                }
                BindOnlineCharges();
                ShowHideControls(ShowHideType.CancelOnlineCharge);
                SetFacilitySettingsData();
                DesignDxGrid();
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
        private void txtOCPSearch_TextChanged(object sender, EventArgs e)
        {

            string strSearch = "";
            string sFilter = "";
            try
            {
                #region " Search "

                if (chkOCPGeneralSearch.Checked == false)
                {
                    if (lblOCPSearch.Tag != null)
                    {
                        DataView dv = (DataView)c1OnlineCharge.DataSource;
                        if (dv != null)
                        {
                            strSearch = txtOCPSearch.Text.Trim();
                            int colIndex = Convert.ToInt32(lblOCPSearch.Tag);
                            strSearch = strSearch.Replace("'", "");

                            if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            {
                                if (dv.Table.Columns["PortalClaimNo"].ColumnName == dv.Table.Columns[colIndex].ColumnName)
                                    dv.RowFilter = "CONVERT(" + dv.Table.Columns[colIndex].ColumnName + ",System.String) Like '%" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                                else
                                    dv.RowFilter = dv.Table.Columns[colIndex].ColumnName + " Like '%" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                            }
                            else
                            {
                                if (dv.Table.Columns["PortalClaimNo"].ColumnName == dv.Table.Columns[colIndex].ColumnName)
                                    dv.RowFilter = "CONVERT(" + dv.Table.Columns[colIndex].ColumnName + ",System.String) Like '" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                                else
                                    dv.RowFilter = dv.Table.Columns[colIndex].ColumnName + " Like '" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                            }
                            DesignOCPChargeGrid(dv);
                        }
                    }
                }
                else
                {

                    DataView dv = (DataView)c1OnlineCharge.DataSource;
                    if (dv != null)
                    {

                        strSearch = txtOCPSearch.Text.Trim();
                        string[] strSearchArray = null;
                        //  strSearch = strSearch.Replace("'", "").Replace(",", "").Replace("%", "").Replace("*", "");
                        strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                        if (strSearch.Length > 1)
                        {
                            string str = strSearch.Substring(1).Replace("%", "");
                            strSearch = strSearch.Substring(0, 1) + str;
                        }
                        if (strSearch.Trim() != "")
                        {
                            strSearchArray = strSearch.Split(',');
                        }

                        if (lblOCPSearch.Tag != null)
                        {

                            // strSearch = strSearch.Replace("%", "").Replace("*", "");

                            if (strSearch.Trim() != "")
                            {


                                if (strSearchArray.Length == 1)
                                {
                                    dv.RowFilter = "CONVERT(" + dv.Table.Columns["PostingDate"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                   "CONVERT(" + dv.Table.Columns["PortalClaimNo"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                   dv.Table.Columns["PatientCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["PatientName"].ColumnName + " Like '" + strSearch + "%'  ";

                                }
                                else
                                {
                                    for (int j = 0; j < strSearchArray.Length; j++)
                                    {
                                        strSearch = strSearchArray[j];
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (j == 0)
                                            {

                                                sFilter = "CONVERT(" + dv.Table.Columns["PostingDate"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                   "CONVERT(" + dv.Table.Columns["PortalClaimNo"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                   dv.Table.Columns["PatientCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["PatientName"].ColumnName + " Like '" + strSearch + "%'  ";

                                            }
                                            else
                                            {

                                                sFilter = sFilter + " AND CONVERT(" + dv.Table.Columns["PostingDate"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                   "CONVERT(" + dv.Table.Columns["PortalClaimNo"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                   dv.Table.Columns["PatientCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["PatientName"].ColumnName + " Like '" + strSearch + "%'  ";


                                            }
                                        }

                                    }
                                    dv.RowFilter = sFilter;
                                }
                            }
                            else
                            {
                                dv.RowFilter = "";
                            }

                            DesignOCPChargeGrid(dv);
                        }
                    }
                }

                #endregion " Search "

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void btnOCPDxClose_Click(object sender, EventArgs e)
        {
            pnlOCPCPTDX.SendToBack();
        }

        private void tlb_OCPDXClose_Click(object sender, EventArgs e)
        {
            pnlOCPCPTDX.SendToBack();
        }

        private void c1OnlineCharge_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1FlexGrid)sender), e.Location);
        }

        private void DefaultSelfPayFeeSchedule()
        {
            try
            {
                #region "Default Fee Schedule to Pull the charge for Self Resonsipble Claim"

                if (Convert.ToString(c1Insurance.GetData(1, COL_INSURANCENAME)) == "Self")
                {
                    ClsFeeSchedule oClsFeeSchedule = new ClsFeeSchedule(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    _FeeScheduleID = oClsFeeSchedule.GetDefaultSelfPayFeeSchedule();
                    if (cmbFeeSchedule != null && _FeeScheduleID != 0)
                    {
                        cmbFeeSchedule.SelectedValue = _FeeScheduleID;
                        chkFeeSchedule.Checked = true;
                    }
                }

                #endregion
            }
            catch (Exception ex) 
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }
    }    
}
