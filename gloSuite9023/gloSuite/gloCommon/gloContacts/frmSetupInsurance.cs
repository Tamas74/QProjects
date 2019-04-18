using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Text.RegularExpressions;
using System.Collections;
using gloSettings;
using gloCommon;

namespace gloContacts
{
    public partial class frmSetupInsurance : Form
    {
        #region "Private Variables"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
      //  private gloListControl.gloListControl oListControl;
      //  private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private Int64 _ContactID = 0;
        private Int64 _UserID = 0;
        private string _InsuranceName = "";
        private Int64 _ClinicID = 0;
       // private bool _IsAddClicked = false;
        private bool _IsWorking = false;
        private DataTable dtInsuranceCompany;
        private bool isCmbInsuranceCompanyLoading = true;
        private Int64 _DefaultCompanyId = 0;
        //SHUBHANGI 20100222    
        private bool _IsModified = false;
        private bool _IsSaveClicked = false;
        private String _LastCompanyChanged = "";
        //public AlternatePayerIDs _objAlternatePayerIDs = new AlternatePayerIDs();
        public BindingList<AlternatePayerID> _objAlternatePayerIDs = new BindingList<AlternatePayerID>();
        public ArrayList _arrAlternatePayerID = new ArrayList();
        private bool _IsenableUB04 = false;
        private Int64 _nInsuranceID = 0;
        private Int64 _nPatientID = 0;
        private bool _IsCopy = false;
        private bool _bIsGroupMandatory = false;
        

        public Int64 nInsuranceID
        {
            get { return _nInsuranceID; }
            set { _nInsuranceID = value; }
        }
        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }
        public Int64 ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }

        public string InsuranceName
        {
            get { return _InsuranceName; }
            set { _InsuranceName = value; }
        }
        public bool bIsGroupMandatory
        {
            get { return _bIsGroupMandatory; }
            set { _bIsGroupMandatory = value; }
        }

        //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private bool _IsInternetFax = false;
        gloAddress.gloAddressControl oAddresscontrol = null;
        public int width;
        Hashtable htMidLevelSettings = new Hashtable();
        Hashtable htSourceSettings = new Hashtable();

        ComboBox combo;
        bool _IsDeleteclicked = false;//Sandip Darade 20100201

        private gloPMContacts.PlanHold _oPlanHold = new gloPMContacts.PlanHold();
        private gloPMContacts.PlanCorrectedReplacement _oCorrectedReplacement = new gloPMContacts.PlanCorrectedReplacement();

        public gloGridListControl ogloGridListControl = null;

        public string sSourceComboString = "";

        bool nonNumberEntered = false;
        bool bBillingtype = false;

        Int64 _OriginalClearingHouse = 0;

        //private bool _IsIncludeTaxonomyforPaper = false;
        //private bool _IsIncludeTaxonomyforElec = false;
        //private string _sQualifier = "";

        #endregion "Private Variables"

        #region "Contructor"

        public frmSetupInsurance(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            cmbBox29.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBox29.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBox30.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBox30.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbClIAPostn.DrawMode = DrawMode.OwnerDrawFixed;
            cmbClIAPostn.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsuranceType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsuranceType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            //DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCptCrosswalk.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCptCrosswalk.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBillingProviderSource.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBillingProviderSource.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBillingProviderSourceOtherIDType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBillingProviderSourceOtherIDType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbClearingHouse.DrawMode = DrawMode.OwnerDrawFixed;
            cmbClearingHouse.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbdonotprintfacility.DrawMode = DrawMode.OwnerDrawFixed;
            cmbdonotprintfacility.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbElectronicRendering.DrawMode = DrawMode.OwnerDrawFixed;
            cmbElectronicRendering.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbFeeSchedules.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFeeSchedules.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsuranceCompany.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsuranceCompany.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbMidLevelSpeProvider.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMidLevelSpeProvider.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbPaperRendering.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPaperRendering.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbReportingCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReportingCategory.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbServiceFacilityOtherIDType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbServiceFacilityOtherIDType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbServiceFacilitySource.DrawMode = DrawMode.OwnerDrawFixed;
            cmbServiceFacilitySource.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbTypeOFBilling.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTypeOFBilling.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbReferringProviderOtherIDType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReferringProviderOtherIDType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsEligibilityPrimProvType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsEligibilityPrimProvType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsEligibilitySecProvType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsEligibilitySecProvType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbUBBlngprvdraltID.DrawMode = DrawMode.OwnerDrawFixed;
            cmbUBBlngprvdraltID.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbFedTaxNoBox5.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFedTaxNoBox5.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbOprtingPrvderBox77.DrawMode = DrawMode.OwnerDrawFixed;
            cmbOprtingPrvderBox77.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBox77Rendering.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBox77Rendering.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbExtendedZipCode.DrawMode = DrawMode.OwnerDrawFixed;
            cmbExtendedZipCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81AValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81AValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81BValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81BValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81CValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81CValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81DValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81DValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            //Sandip Darade  20090428
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

            //Sandip Darade  20091021
            #region " Retrieve Internet fax setting from AppSettings "

            if (appSettings["Internet Fax"] != null)
            {
                if (appSettings["Internet Fax"] != "")
                {
                    _IsInternetFax = Convert.ToBoolean(appSettings["Internet Fax"]);
                }
                else
                {
                    _IsInternetFax = false;
                }
            }
            else
            { _IsInternetFax = false; }

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



        public frmSetupInsurance(Int64 ContactId, string DatabaseConnectionString)
        {
            InitializeComponent();

            cmbInsuranceType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsuranceType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            // DropDownStyle = ComboBoxStyle.DropDownList;

            cmbCptCrosswalk.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCptCrosswalk.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBillingProviderSource.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBillingProviderSource.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBillingProviderSourceOtherIDType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBillingProviderSourceOtherIDType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbClearingHouse.DrawMode = DrawMode.OwnerDrawFixed;
            cmbClearingHouse.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbdonotprintfacility.DrawMode = DrawMode.OwnerDrawFixed;
            cmbdonotprintfacility.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbElectronicRendering.DrawMode = DrawMode.OwnerDrawFixed;
            cmbElectronicRendering.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbFeeSchedules.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFeeSchedules.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsuranceCompany.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsuranceCompany.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbMidLevelSpeProvider.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMidLevelSpeProvider.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbPaperRendering.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPaperRendering.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbReportingCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReportingCategory.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbServiceFacilityOtherIDType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbServiceFacilityOtherIDType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbServiceFacilitySource.DrawMode = DrawMode.OwnerDrawFixed;
            cmbServiceFacilitySource.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbTypeOFBilling.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTypeOFBilling.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbReferringProviderOtherIDType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReferringProviderOtherIDType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBox29.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBox29.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBox30.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBox30.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsEligibilityPrimProvType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsEligibilityPrimProvType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsEligibilitySecProvType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsEligibilitySecProvType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            
            cmbUBBlngprvdraltID.DrawMode = DrawMode.OwnerDrawFixed;
            cmbUBBlngprvdraltID.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbFedTaxNoBox5.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFedTaxNoBox5.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbOprtingPrvderBox77.DrawMode = DrawMode.OwnerDrawFixed;
            cmbOprtingPrvderBox77.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBox77Rendering.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBox77Rendering.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbExtendedZipCode.DrawMode = DrawMode.OwnerDrawFixed;
            cmbExtendedZipCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81AValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81AValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81BValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81BValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81CValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81CValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81DValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81DValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            _databaseconnectionstring = DatabaseConnectionString;

            _ContactID = ContactId;

            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            //Sandip Darade  20090428
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

            //Sandip Darade  20091021
            #region " Retrieve Internet fax setting from AppSettings "

            if (appSettings["Internet Fax"] != null)
            {
                if (appSettings["Internet Fax"] != "")
                {
                    _IsInternetFax = Convert.ToBoolean(appSettings["Internet Fax"]);
                }
                else
                {
                    _IsInternetFax = false;
                }
            }
            else
            { _IsInternetFax = false; }

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


        public frmSetupInsurance(Int64 ContactId, string DatabaseConnectionString, bool copyContact)
        {
            InitializeComponent();

            cmbInsuranceType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsuranceType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            // DropDownStyle = ComboBoxStyle.DropDownList;

            cmbCptCrosswalk.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCptCrosswalk.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBillingProviderSource.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBillingProviderSource.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBillingProviderSourceOtherIDType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBillingProviderSourceOtherIDType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbClearingHouse.DrawMode = DrawMode.OwnerDrawFixed;
            cmbClearingHouse.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbdonotprintfacility.DrawMode = DrawMode.OwnerDrawFixed;
            cmbdonotprintfacility.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbElectronicRendering.DrawMode = DrawMode.OwnerDrawFixed;
            cmbElectronicRendering.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbFeeSchedules.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFeeSchedules.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsuranceCompany.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsuranceCompany.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbMidLevelSpeProvider.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMidLevelSpeProvider.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbPaperRendering.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPaperRendering.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbReportingCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReportingCategory.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbServiceFacilityOtherIDType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbServiceFacilityOtherIDType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbServiceFacilitySource.DrawMode = DrawMode.OwnerDrawFixed;
            cmbServiceFacilitySource.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbTypeOFBilling.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTypeOFBilling.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbReferringProviderOtherIDType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReferringProviderOtherIDType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBox29.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBox29.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBox30.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBox30.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsEligibilityPrimProvType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsEligibilityPrimProvType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbInsEligibilitySecProvType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbInsEligibilitySecProvType.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbUBBlngprvdraltID.DrawMode = DrawMode.OwnerDrawFixed;
            cmbUBBlngprvdraltID.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbFedTaxNoBox5.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFedTaxNoBox5.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbOprtingPrvderBox77.DrawMode = DrawMode.OwnerDrawFixed;
            cmbOprtingPrvderBox77.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbBox77Rendering.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBox77Rendering.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbExtendedZipCode.DrawMode = DrawMode.OwnerDrawFixed;
            cmbExtendedZipCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81AValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81AValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81BValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81BValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81CValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81CValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmb81DValue.DrawMode = DrawMode.OwnerDrawFixed;
            cmb81DValue.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            _databaseconnectionstring = DatabaseConnectionString;

            _ContactID = ContactId;
            _IsCopy = copyContact;

            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            //Sandip Darade  20090428
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

            //Sandip Darade  20091021
            #region " Retrieve Internet fax setting from AppSettings "

            if (appSettings["Internet Fax"] != null)
            {
                if (appSettings["Internet Fax"] != "")
                {
                    _IsInternetFax = Convert.ToBoolean(appSettings["Internet Fax"]);
                }
                else
                {
                    _IsInternetFax = false;
                }
            }
            else
            { _IsInternetFax = false; }

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

        #region "Column Declaration"
        private const int COL_CPT_CODE = 0;
        private const int COL_CPT_DESC = 1;
        private const int COL_CPT_CHARGES = 2;
        private const int COL_MOD1_CODE = 3;
        private const int COL_MOD1_CHARGES = 4;
        private const int COL_MOD2_CODE = 5;
        private const int COL_MOD2_CHARGES = 6;
        private const int COL_MOD3_CODE = 7;
        private const int COL_MOD3_CHARGES = 8;
        private const int COL_MOD4_CODE = 9;
        private const int COL_MOD4_CHARGES = 10;
        private const int COL_SpecialityDesc = 11;
        private const int COL_COUNT = 12;
        #endregion

        #region " Enums "

        public enum TabPages
        {
            InsuranSetup = 0,
            Eligibility = 1,
            MidLevel = 2,
            Billing = 3,
            BillingTaxonomy = 4,
            ANSI5010Transition = 5,
            AlternatePayerIDTab = 6,
            UB = 7
        }

        public enum MidLevelGridColumn
        {
            ProviderID,
            ProviderName,
            ProviderTypeID,
            ProviderType,
            SettingsName,
            SettingsID
        }

        public enum BillingTaxonomy
        {
            ProviderID,
            ProviderName,
            DefaultTaxonomyDesc,
            DefaultTaxonomy,
            PlanOverrideDesc,
            PlanOverride
        }

        public enum BillingTaxonomyQualifierItems
        {
            ZZ,
            PXC
        }

        public enum BillingGridColumn
        {
            ProviderID,
            ProviderName,
            ServiceFacilitySource,
            BillingProviderSource
        }

        #endregion

        #region "Form Load "

        private void frmSetupInsurance_Load(object sender, EventArgs e)
        {
            lblHoldMessage.Text = "";
            this.Height = 432;
            gloPMContacts.Insurance oInsurance = new gloPMContacts.Insurance();
            pnlBillingSourceOther.Enabled = false;
            pnlServiceFacilityOther.Enabled = false;

            if (tbInsuranceSetup.TabPages.IndexOfKey("tbp_Collection") > 0)
            {
                tbInsuranceSetup.TabPages.RemoveAt(tbInsuranceSetup.TabPages.IndexOfKey("tbp_Collection"));
            }
            //Sandip Darade 20091021
            //If fax is not internet fax do no masking  for fax information
            if (_IsInternetFax == false)
            {
                txtFax.MaskType = gloMaskControl.gloMaskType.Other;
            }
            oAddresscontrol = new gloAddress.gloAddressControl(_databaseconnectionstring, true);
            oAddresscontrol.Dock = DockStyle.Fill;
            pnlAddresssControl.Controls.Add(oAddresscontrol);
            FillEDIAltPayerID();
            FillBillingTaxonomyQualifierCombo();
            FillBox11dSettings();
            //txtname.Select();
            FillInsuranceType();
            //Shubhangi 20100211
            Fill_CPTMapping();
            FillInsuranceCompany();
            FillReportingCategory();
            FillFUAction();
            //End
            fillPaperClaimEPSDTCodeBox();
            fillPaperClaimFamilyPlanningCodeBox();
            Fill_CmbClearinghouse();
            fillDefaultbillingMethod();
            FillFeeSchedules();
            FillPaperBilling();
            fillUBo4BillingProviderOtherID();
            LoadAlternatePayerID();
            FillAlternatePayerID();
            FillBillUnitsAs();
            //gloPM5060 MaheshB
            //Sandip Darade 20091211
            //cmbdonotprintfacility.Items.Add("");
            //cmbdonotprintfacility.Items.Add("Yes");
            //cmbdonotprintfacility.Items.Add("No");
            fillIncludePOS();
            FillUB04ExtendedSettings();
            // DesignGrid_C1View();
            if (_ContactID != 0)
            {
                //_IsIncludeTaxonomyforPaper = false; ;
                //_IsIncludeTaxonomyforElec = false;
                //_sQualifier ="";

                oInsurance = LoadInsurance();
                LoadInsuranceComapanytDetails(_ContactID);

                if (_DefaultCompanyId != 0)
                {
                    // lblInsuranceCompany_Mandatory.Visible = true;
                }
                else
                {
                    //lblInsuranceCompany_Mandatory.Visible =false ;
                }
                if (!_IsCopy)
                {   
                    LoadHoldInfo();
                    GetHoldMessage();
                }
                LoadCorrectedReplacementInfo();
                

                //Get Expanded Claim Settings
                GetExpandedClaimSettings();
                if (Convert.ToString(cmbTypeOFBilling.SelectedValue) != "")
                {
                    bBillingtype = true;
                }
                //

                
            }
            else
            {
                bBillingtype = true;
                cmbFedTaxNoBox5.Text = "SSN";
                cmbOprtingPrvderBox77.Text = "Attending";
                cmbCMSDateFormat.Text = "YYYY";
                chkIncludePrimaryDxInBox69.Checked = true;
            }

            //FillInsuranceComapanyDefaultDetails();
            ts_btnSave.Visible = true;
            ts_btnClose.Visible = true;
            ts_btnFeeSchedule.Visible = false;
            ts_btnSave_FeeSchedule.Visible = false;
            ts_btnClose_FeeSchedule.Visible = false;
            ts_btnAddLine.Visible = false;
            ts_btnRemoveLine.Visible = false;
            ts_btnRemoveAll.Visible = false;
            //txt_Search.Visible = false;
            //lblSearch.Visible = false;
            ts_btnImportFeeSchedule.Visible = false;
            ts_btnAdd_FeeSchedule.Visible = false;
            //pnlFeeSchedule.SendToBack();
            pnl_Base.BringToFront();
            pnlTopToolStrip.SendToBack();

            // CHECK WHETHER PLAN IS IN TRANSACTION OR NOT //
            // IF IN TRANSACTION THEN DESABLE ITS FIELDS //
            //gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);
            //if (oContact.IsInsurancePlanUsed(_ContactID) == true)
            //{
            //    if (cmbInsuranceCompany.Text.Trim() != "")
            //        cmbInsuranceCompany.Enabled = false;
            //}

            //oContact.Dispose();
            //oContact = null;
            _IsModified = false;
            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            try
            {
                _IsenableUB04 = oglocontact.IsenableUB04(_ClinicID);
                if (!_IsenableUB04)
                {
                    chkIsInstitutionalBilling.Visible = false;
                }
                oAddresscontrol.AddressModified = false;
                cmbInsuranceCompany.Focus();
                if (oglocontact.GetEnableWorkercompSetting())
                {
                    chkWorkersComp.Visible = true;
                }
                else
                {
                    chkWorkersComp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { oglocontact.Dispose(); }

           

            GeneralSettings oSettings = null;
            object oEPSDTSetting = null;
            string sType = null;
            object oValue = null;
            try
            {
                oSettings = new GeneralSettings(_databaseconnectionstring);
                sType = oSettings.GetInstallationType(0, 1);
                oSettings.GetSetting("Enable Insurance Plan 5010", 0, _ClinicID, out oValue);
                oSettings.GetSetting("EnableEPSDTFamilyPlanning", 0, _ClinicID, out oEPSDTSetting);
                //Fill 5010 Transition Data

                //Fill Eligibility tab data
                FillInsEligibilityProviderType();
                FillInsuranceEligibilityProvider(oInsurance);

                //Fill Fill 5010 Transition tab data
                Fill5010TransitionData(oInsurance);
                switch (sType)
                {
                    case "gloEMR":

                        //tbInsuranceSetup.TabPages.RemoveAt((int)TabPages.MidLevel);
                        //tbInsuranceSetup.TabPages.RemoveAt((int)TabPages.Billing);

                        tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_MidLevel"]);
                        tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_BillingSettings"]);
                        tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_BillingTaxon"]);
                        tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_Eligibility"]);
                        tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_5010Transition"]);
                        tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_AlternatePayerID"]);
                        tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_Institutional"]);
                        tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_EPSDT"]);
                        tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tpAnesthesia"]);

                        tls_Hold.Visible = false;
                        tls_Release.Visible = false;
                        lblHoldMessage.Visible = false;
                        pnlHoldMessage.Visible = false;

                        break;
                    case "gloPM":


                        if (!string.IsNullOrEmpty(Convert.ToString(oValue)) && (Convert.ToString(oValue).ToUpper() == "TRUE" || Convert.ToString(oValue).ToUpper() == "FALSE"))
                        {
                            if (!Convert.ToBoolean(oValue))
                            {
                                tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_5010Transition"]);
                            }
                        }
                        else
                        {
                            tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_5010Transition"]);
                        }

                        if (_IsenableUB04 == false)
                        {
                            tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_Institutional"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(oEPSDTSetting)) && (Convert.ToString(oEPSDTSetting).ToUpper() == "TRUE" || Convert.ToString(oEPSDTSetting).ToUpper() == "FALSE"))
                        {
                            if (!Convert.ToBoolean(oEPSDTSetting))
                            {
                                tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_EPSDT"]);
                            }
                        }
                        if (!IsAnesthesiaBillingEnabled())
                        { tbInsuranceSetup.TabPages.Remove(tpAnesthesia); }
                        //Fill Alternate ID Settings Data
                        FillBillingTabFields();
                        FillAlternateIDSettingsData();

                        //Fill Taxonomy Settings Data.
                        //FillBillingTaxonomyQualifierCombo();
                        DesignBillingTaxonomyGrid();
                        FillBillingTaxonomyGrid();
                        pnlInternalControl.Height = pnlInternalControl.Height - 5;
                        if (_ContactID > 0)
                        {
                            FillBillingTaxonomyData(_ContactID);
                        }
                        else
                        {
                            chkIncludeTaxRenPaper.Checked = true;
                            chkIncludeTaxBilPaper.Checked = true;
                            chkIncludeTaxRenElec.Checked = true;
                            chkIncludeTaxBillElec.Checked = true;
                        }
                        //**

                        //Filling MidLevel Combo for All Provider(5070)
                        FillMidLevelSettingsAllProviderCombo();
                        //Design Mid Level Grid 
                        DesignMidLevelGrid();
                        //Fill Mid Level Grid 
                        FillMidLevelGrid();
                        //Fill Mid Level Data 
                        FillMidLevelData(_ContactID);
                        tbInsuranceSetup.TabPages[(int)TabPages.InsuranSetup].Focus();
                        tbInsuranceSetup.TabPages[(int)TabPages.InsuranSetup].Select();

                        //_oPlanHold = new gloPMContacts.PlanHold();
                        //_oCorrectedReplacement = new gloPMContacts.PlanCorrectedReplacement();
                        if (_oPlanHold.IsHold)
                        {
                            tls_Hold.Visible = false;
                            tls_Release.Visible = true;
                        }
                        else
                        {
                            tls_Hold.Visible = true;
                            tls_Release.Visible = false;
                        }

                        lblHoldMessage.Visible = true;
                        pnlHoldMessage.Visible = true;
                        //chkIncludeTax4Paper.Checked = _IsIncludeTaxonomyforPaper;
                        //chkIncludeTax4Elec.Checked = _IsIncludeTaxonomyforElec;
                        //cmbQualifier.Text = _sQualifier;
                        break;
                    case "Both":
                    case "None":

                        if (!string.IsNullOrEmpty(Convert.ToString(oValue)) && (Convert.ToString(oValue).ToUpper() == "TRUE" || Convert.ToString(oValue).ToUpper() == "FALSE"))
                        {
                            if (!Convert.ToBoolean(oValue))
                            {
                                tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_5010Transition"]);
                            }
                        }
                        else
                        {
                            tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_5010Transition"]);
                        }

                        if (_IsenableUB04 == false)
                        {
                            tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_Institutional"]);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(oEPSDTSetting)) && (Convert.ToString(oEPSDTSetting).ToUpper() == "TRUE" || Convert.ToString(oEPSDTSetting).ToUpper() == "FALSE"))
                        {
                            if (!Convert.ToBoolean(oEPSDTSetting))
                            {
                                tbInsuranceSetup.TabPages.Remove(tbInsuranceSetup.TabPages["tbp_EPSDT"]);
                            }
                        }

                        if (!IsAnesthesiaBillingEnabled())
                        { tbInsuranceSetup.TabPages.Remove(tpAnesthesia); }
                        //Fill Alternate ID Settings Data
                        FillBillingTabFields();
                        if (_ContactID > 0)
                        {
                            FillAlternateIDSettingsData();
                        }

                        //Filling MidLevel Combo for All Provider(5070)
                        FillMidLevelSettingsAllProviderCombo();

                        //Fill Taxonomy Settings Data.
                        //FillBillingTaxonomyQualifierCombo();
                        DesignBillingTaxonomyGrid();
                        FillBillingTaxonomyGrid();
                        pnlInternalControl.Height = pnlInternalControl.Height - 5;
                        if (_ContactID > 0)
                        {
                            FillBillingTaxonomyData(_ContactID);
                        }
                        else
                        {
                            chkIncludeTaxRenPaper.Checked = true;
                            chkIncludeTaxBilPaper.Checked = true;
                            chkIncludeTaxRenElec.Checked = true;
                            chkIncludeTaxBillElec.Checked = true;
                        }
                        //**


                        //Design Mid Level Grid 
                        DesignMidLevelGrid();
                        //Fill Mid Level Grid 
                        FillMidLevelGrid();
                        //Fill Mid Level Data 
                        FillMidLevelData(_ContactID);
                        tbInsuranceSetup.TabPages[(int)TabPages.InsuranSetup].Focus();
                        tbInsuranceSetup.TabPages[(int)TabPages.InsuranSetup].Select();

                        //_oPlanHold = new gloPMContacts.PlanHold();
                        //_oCorrectedReplacement = new gloPMContacts.PlanCorrectedReplacement();

                        if (_oPlanHold.IsHold)
                        {
                            tls_Hold.Visible = false;
                            tls_Release.Visible = true;
                        }
                        else
                        {
                            tls_Hold.Visible = true;
                            tls_Release.Visible = false;
                        }

                        lblHoldMessage.Visible = true;
                        pnlHoldMessage.Visible = true;
                        //chkIncludeTax4Paper.Checked = _IsIncludeTaxonomyforPaper;
                        //chkIncludeTax4Elec.Checked = _IsIncludeTaxonomyforElec;
                        //cmbQualifier.Text = _sQualifier;
                        break;
                    default:

                        break;
                }

                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                // This method actually sets the order all the way down the control hierarchy.
                tom.SetTabOrder(scheme);
                //scheme = null;
                tom = null;

            }
            catch
            {

            }
            finally
            {
                
                oInsurance.Dispose();
                oSettings.Dispose();
                oEPSDTSetting = null;
                sType = null;
                oValue = null;
            }

            if (_IsCopy)
            {
                txtname.Text =txtname.Text + " -Copy";              
                _ContactID = 0;
            }
        }

        #endregion

        #region "Methods & form control events for insurance info "
        private void LoadHoldInfo()
        {
            gloPMContacts.gloContacts oglocontact = null;
            try
            {
                oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                _oPlanHold = oglocontact.SetHoldInfo(_ContactID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
            }
        }

        private void LoadCorrectedReplacementInfo()
        {
            gloPMContacts.gloContacts oglocontact = null;
            try
            {
                oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                _oCorrectedReplacement = oglocontact.SetCorrectedReplacementInfo(_ContactID);
                if (_oCorrectedReplacement.bIsCorrectedReplacement)
                    chkCorrectRplmnt.Checked = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
            }
        }

        private void GetHoldMessage()
        {

            try
            {
                lblHoldMessage.Text = "";


                if (_oPlanHold.IsHold)
                {
                    //lblHoldMessage.Text = "Plan On Billing Hold";
                    lblHoldMessage.Text = "Plan on Hold";

                    tls_Hold.Visible = false;
                    tls_Release.Visible = true;
                    pnlHoldMessage.Visible = true;
                }
                else
                {
                    lblHoldMessage.Text = "";

                    tls_Hold.Visible = true;
                    tls_Release.Visible = false;
                    pnlHoldMessage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        private gloPMContacts.Insurance LoadInsurance()
        {
            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            gloPMContacts.Insurance oInsurance = new gloPMContacts.Insurance(); 
            try
            {
                
                oInsurance = oglocontact.SelectInsurance(_ContactID);

                _InsuranceName = oInsurance.Name;
                txtname.Text = oInsurance.Name;
                txtcontact.Text = oInsurance.ContactName;
                cmbInsuranceType.SelectedValue = oInsurance.InsuranceTypeCode;

                //txtAddressLine1.Text = oInsurance.CompanyAddress.AddrressLine1;
                //txtAddressLine2.Text = oInsurance.CompanyAddress.AddrressLine2;
                //txtCity.Text = oInsurance.CompanyAddress.City;
                //txtState.Text = oInsurance.CompanyAddress.State;
                //txtZip.Text = oInsurance.CompanyAddress.ZIP;

                //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 

                oAddresscontrol.txtAddress1.Text = oInsurance.CompanyAddress.AddrressLine1;
                oAddresscontrol.txtAddress2.Text = oInsurance.CompanyAddress.AddrressLine2;
                oAddresscontrol.txtCity.Text = oInsurance.CompanyAddress.City;
                oAddresscontrol.cmbState.Text = oInsurance.CompanyAddress.State;
                oAddresscontrol._isTextBoxLoading = true;
                oAddresscontrol.txtZip.Text = oInsurance.CompanyAddress.ZIP;
                oAddresscontrol._isTextBoxLoading = false;

                mtxtPhone.Text = oInsurance.CompanyAddress.Phone;
                txtFax.Text = oInsurance.CompanyAddress.Fax;
                txtEmail.Text = oInsurance.CompanyAddress.Email;
                txtURL.Text = oInsurance.CompanyAddress.URL;

                txtPayerID.Text = oInsurance.sPayerID;
                chkBox31Blank.Checked = oInsurance.bBox31Blank;
                chkOnlyPrintFirstPointer.Checked = oInsurance.b1stPointer;

                chkDoNotPrintFacility.Checked = oInsurance.bDoNotPrintFacility;
                chkMedigap.Checked = oInsurance.bMedigap;
                chkNameOfFacilityinBox33.Checked = oInsurance.bNameOfacilityinBox33;
                chkRefferingID.Checked = oInsurance.bReferringIDInBox19;
                chkShowPayment.Checked = oInsurance.bShowPayment;
                if (oInsurance.bStatementToPatient == true)
                {
                    rbStatementToPatientYes.Checked = true;
                }
                else
                {
                    rbStatementToPatientNo.Checked = true;
                }
                if (oInsurance.bAccessAssignment == true)
                {
                    chkAcceptAssignment.Checked = true;
                }
                else
                {
                    chkAcceptAssignment.Checked = false;
                }
                if (oglocontact.GetEnableWorkercompSetting())
                {
                    chkWorkersComp.Visible = true;
                }
                else
                {
                    chkWorkersComp.Visible = false;
                }

                if (oInsurance.bIsWorkerComp == true)
                {
                    chkWorkersComp.Checked = true;
                }
                else
                {
                    chkWorkersComp.Checked = false;
                }
                if (oInsurance.nTypeOBilling == gloPMContacts.TypeOfBilling.Paper)
                {
                    cmbTypeOFBilling.Text = "Paper";

                }
                if (oInsurance.nTypeOBilling == gloPMContacts.TypeOfBilling.Electronic)
                {
                    cmbTypeOFBilling.Text = "Electronic";
                }
                if (oInsurance.nTypeOBilling == gloPMContacts.TypeOfBilling.None)
                {
                    //cmbTypeOFBilling.SelectedIndex = -1;
                    cmbTypeOFBilling.SelectedIndex = 0;
                }
                if (oInsurance.bIsGroupMandatory == true)
                {
                    chkGroupMandatory.Checked = true;
                }
                else
                {
                    chkGroupMandatory.Checked = false;
                }
                _bIsGroupMandatory = chkGroupMandatory.Checked;
                cmbClearingHouse.SelectedValue = oInsurance.nClearingHouse;
                cmbClearingHouse.Refresh();

                _OriginalClearingHouse = oInsurance.nClearingHouse;


                txtOfficeID.Text = oInsurance.OfficeID;

                txtWebsite.Text = oInsurance.sWebsite;
                txtServicingState.Text = oInsurance.sServicingState;
                txt_PayerPhExt.Text = oInsurance.sPayerPhoneExtn;
                mtxt_PayerPhone.Text = oInsurance.sPayerPhone;
                txtAdditionalInfo.Text = oInsurance.sComments;
                chkClaims.Checked = oInsurance.bIsClaims;
                chkElectronicCOB.Checked = oInsurance.bIsElectronicCOB;
                chkEnrollmentRequired.Checked = oInsurance.bIsEnrollmentRequired;
                chkRealTimeClaimStatus.Checked = oInsurance.bIsRealTimeClaimStatus;
                chkRealTimeEligibility.Checked = oInsurance.bIsRealTimeEligibility;
                chkRemittanceAdvice.Checked = oInsurance.bIsRemittanceAdvice;
                chkNotesInBox19.Checked = oInsurance.bNotesInBox19;

                chkIncludeOTAFAmount.Checked = oInsurance.bIsOTAFAmount; // added on 27/04/2010

                cmbBox32.Text = oInsurance.Box32;
                cmbBox32A.Text = oInsurance.Box32A;
                cmbBox32B.Text = oInsurance.Box32B;

                cmbBox33.Text = oInsurance.Box33;
                cmbBox33A.Text = oInsurance.Box33A;
                cmbBox33B.Text = oInsurance.Box33B;
                //Sandip Darade 20091211
                cmbdonotprintfacility.Text = oInsurance.sDoNotPrintFacility;


                FillInsuranceFeeSchedule(_ContactID);

                cmbCptCrosswalk.SelectedValue = oInsurance.CPTCrosswalkID;
                chkPARequired.Checked = oInsurance.PARequired;
                chkIsInstitutionalBilling.Checked = oInsurance.IsInstitutionalBilling;
                //if (oInsurance.FedTaxNoBox5 == null || oInsurance.FedTaxNoBox5 =="")
                //{
                //    cmbFedTaxNoBox5.Text = "SSN";
                //}
                //else
                //{   
                //    cmbFedTaxNoBox5.Text = oInsurance.FedTaxNoBox5;
                //}
                if (oInsurance.operationgProviderBox77 == null || oInsurance.operationgProviderBox77 == "")
                {
                    cmbOprtingPrvderBox77.Text = "";
                }
                else
                {
                    cmbOprtingPrvderBox77.Text = oInsurance.operationgProviderBox77;
                }

                if (oInsurance.Box77RenderingProvider == null || oInsurance.Box77RenderingProvider == "")
                {
                    cmbBox77Rendering.Text = "";
                }
                else
                {
                    cmbBox77Rendering.Text = oInsurance.Box77RenderingProvider;
                }
                //cmbUBBlngprvdraltID.SelectedItem = oInsurance.UB51BillingProvderOtherID.ToString();
                //cmbUBBlngprvdraltID.SelectedValue = oInsurance.UB51BillingProvderOtherID;


                //By Debasish on 11/19/2010
                txtInsEligibilityProvderID.Text = oInsurance.InsuranceEligibilityProviderID;
                //chkPARequired.Checked = oInsurance.IsDefaultPriorAuthorizationRequired;


                txtBillClaimOfficeNo.Text = oInsurance.sBillClaimOfficeNo;
                if (Convert.ToString(oInsurance.sEDIAltPayerIDType) != "")
                    cmbRefSecIdentification.SelectedValue = oInsurance.sEDIAltPayerIDType;

                txtBox19DefaultNote.Text = oInsurance.sBox19DefaultNote;

                //_IsIncludeTaxonomyforPaper = oInsurance.IsIncludeTaxonomyforPaper;
                //_IsIncludeTaxonomyforElec = oInsurance.IsIncludeTaxonomyforElectronic;
                //_sQualifier = oInsurance.sQualifier;
                //chkIncludeTax4Paper.Checked = oInsurance.IsIncludeTaxonomyforPaper;
                //chkIncludeTax4Elec.Checked = oInsurance.IsIncludeTaxonomyforElectronic;

                this.chkIncludeTax4Paper.CheckedChanged -= new System.EventHandler(this.chkIncludeTax4Paper_CheckedChanged);
                this.chkIncludeTaxRenPaper.CheckedChanged -= new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);
                this.chkIncludeTaxBilPaper.CheckedChanged -= new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);

                this.chkIncludeTax4Elec.CheckedChanged -= new System.EventHandler(this.chkIncludeTax4Elec_CheckedChanged);
                this.chkIncludeTaxRenElec.CheckedChanged -= new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);
                this.chkIncludeTaxBillElec.CheckedChanged -= new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);

                chkIncludeTaxRenPaper.Checked = oInsurance.bPaperRenderingTaxonomy;
                chkIncludeTaxBilPaper.Checked = oInsurance.bPaperBillingTaxonomy;
                chkIncludeTaxRenElec.Checked = oInsurance.bElectronicRenderingTaxonomy;
                chkIncludeTaxBillElec.Checked = oInsurance.bElectronicBillingTaxonomy;

                if (chkIncludeTaxRenPaper.Checked || chkIncludeTaxBilPaper.Checked)
                {
                    chkIncludeTax4Paper.Checked = true;
                    chkIncludeTaxRenPaper.Enabled = true;
                    chkIncludeTaxBilPaper.Enabled = true;
                }
                else
                {
                    chkIncludeTax4Paper.Checked = false;
                }
                if (chkIncludeTax4Paper.Checked == false)
                {
                    chkIncludeTaxRenPaper.Enabled = false;
                    chkIncludeTaxBilPaper.Enabled = false;
                }

                if (chkIncludeTaxRenElec.Checked || chkIncludeTaxBillElec.Checked)
                {
                    chkIncludeTax4Elec.Checked = true;
                    chkIncludeTaxRenElec.Enabled = true;
                    chkIncludeTaxBillElec.Enabled = true;
                }
                else
                {
                    chkIncludeTax4Elec.Checked = false;
                }
                if (chkIncludeTax4Elec.Checked == false)
                {
                    chkIncludeTaxRenElec.Enabled = false;
                    chkIncludeTaxBillElec.Enabled = false;
                }

                cmbQualifier.Text = oInsurance.sQualifier;
                //chkIncludeRendering.Checked = oInsurance.IncludeRenderingProvider;
                chkIDInBox31.Checked = oInsurance.bIDInBox31;
                chkIncludePlanName.Checked = oInsurance.bIncludePlanName;
                chkPaperDisplayMailingAddress.Checked = oInsurance.bPaperDisplayMailingAddress;
                chkSwap1a9a1aMCare.Checked = oInsurance.bSwap1a9a1aMCare;
                chkIncludeRendering_Attending.Checked = oInsurance.bIncludeRendering_Attending;
                chkDefaultDOS.Checked = oInsurance.bDefaultOccuranceDOS;
                chkIncludeUB04DischargeHour.Checked = oInsurance.bIncludeUB04DischargeHour;
                chkIncludeUB04AdmissionHour.Checked = oInsurance.bIncludeUB04AdmissionHour;
                ckhSentUB04RevenuecodeTotal.Checked = oInsurance.bIncludeUB04RevenueCodeTotal;

                if (oInsurance.IncludePriorPatientPayment==null || oInsurance.IncludePriorPatientPayment==DBNull.Value)
                {
                     rbNone.Checked = true;
                }
                else if (Convert.ToBoolean(oInsurance.IncludePriorPatientPayment) ==true)
                {
                    rbSend.Checked=true;
                }
                else
                {
                    rbDoNotSend.Checked=true;
                }
               
                TxtEligiblitycntct.Text = oInsurance.EligibiltiContact;
                mskeligibiltyPhone.Text = oInsurance.EligibilityPhone;
                TxtEligibiltyWebste.Text = oInsurance.Eligibilitywebsite;
                txtEligibiltyNote.Text = oInsurance.EligibilityNote;

                chkBillEpsdtFamPlan.Checked = oInsurance.bIsBillEPSDTorFamilyPlanning;
                chkIncludeSV.Checked = oInsurance.bIsEDIIncludeSV;
                chkIncludeCRC.Checked = oInsurance.bIsEDIIncludeCRC;
                chkIncludeRefCode.Checked = oInsurance.bIsPaperIncludeReferralCode;
                txtEPSDTCode.Text = oInsurance.sPaperClaimEPSDTCode;
                if (oInsurance.sPaperClaimEPSDTCodeBox.Trim() != string.Empty)
                {
                    cmbEPSDTCodeBox.SelectedValue = oInsurance.sPaperClaimEPSDTCodeBox;
                }
                txtFamPlanCode.Text = oInsurance.sPaperClaimFamilyPlanningCode;
                if (oInsurance.sPaperClaimFamilyPlanningCodeBox.Trim() != string.Empty)
                {
                    cmbFamPlanCodeBox.SelectedValue = oInsurance.sPaperClaimFamilyPlanningCodeBox;
                }
                chkSuppressRenderPaperEdi.Checked = oInsurance.bIsSupressRenderEPSDTClaimOnPaperEDI;
                ChkEMGAsX.Checked = oInsurance.bEMGAsX;
                chkShowClaim.Checked = oInsurance.bShowClaim;
                if (oInsurance.sBillUnitsAs.Trim() != string.Empty)
                {
                    cmbBillUnitsAs.SelectedValue = oInsurance.sBillUnitsAs;
                }
                if (oInsurance.nMinutesPerUnits != 0)
                {
                    txtBaseUnits.Text = Convert.ToString(oInsurance.nMinutesPerUnits);
                }
                chkClaimFreq.Checked = oInsurance.bIsClaimFrequencyOne;
                if (oInsurance.bIncludeMedicareClaimRef == true)
                {
                    chkMedClaimRef.Checked = true;
                }
                else
                {
                    chkMedClaimRef.Checked = false;
                }

                cmbBox11bSettings.SelectedValue = oInsurance.nBox11bSettingID;
                //if block added for include EDI Alt. Payer ID on secondary claims 06May2014 Sameer
                if (oInsurance.bIncludeEdiAltPayerID == true)
                {
                    chkEdiAltPayerID.Checked = true;
                }
                else
                {
                    chkEdiAltPayerID.Checked = false;
                }
                chkIncludeRefnSupervising.Checked = oInsurance.bIncludeReferring_supervising;
                chkIncludeRefnOrdering.Checked = oInsurance.bIncludeReferring_ordering;
                chkIncludePatientSSN.Checked = oInsurance.bIncludePatientSSN;
                chkIncludeMod_in_SVD.Checked = oInsurance.bIncludeMod_in_SVD;  // Code added to add modifiers in svd segment 06-23-2017
                chkIncludePrimaryDxInBox69.Checked = oInsurance.bIncludePrimaryDxInBox69;
                if (oInsurance.sCMSDateFormat.Trim() != string.Empty)
                {
                    cmbCMSDateFormat.Text = oInsurance.sCMSDateFormat;
                }
                else
                {
                    cmbCMSDateFormat.SelectedIndex = 0;
                }
                chkReportClinicName.Checked = oInsurance.bReportClinicName;
                if (Convert.ToBoolean(oInsurance.bIncludeSecondaryPayerAddress) == true)
                {
                    chkSecondaryPayerAddress.Checked = true;
                }
                else
                {
                    chkSecondaryPayerAddress.Checked = false;
                }

                return oInsurance;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                this.chkIncludeTax4Paper.CheckedChanged += new System.EventHandler(this.chkIncludeTax4Paper_CheckedChanged);
                this.chkIncludeTaxRenPaper.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);
                this.chkIncludeTaxBilPaper.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);

                this.chkIncludeTax4Elec.CheckedChanged += new System.EventHandler(this.chkIncludeTax4Elec_CheckedChanged);
                this.chkIncludeTaxRenElec.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);
                this.chkIncludeTaxBillElec.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);

                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
                if (oInsurance != null) { oInsurance.Dispose(); oInsurance = null; }
            }
        }


        public void FillInsuranceEligibilityProvider(gloPMContacts.Insurance oInsurance)
        {

            if (oInsurance.InsuranceEligibilityProviderID != null && oInsurance.InsuranceEligibilityProviderID != "")
            {
                txtInsEligibilityPrimProvID.Text = oInsurance.InsuranceEligibilityProviderID;
            }
            if (oInsurance.InsuranceEligibilityProvSecID != null && oInsurance.InsuranceEligibilityProvSecID != "")
            {
                txtInsEligibilitySecProvID.Text = oInsurance.InsuranceEligibilityProvSecID;
            }
            if (oInsurance.InsuranceEligibilityProvType != null && oInsurance.InsuranceEligibilityProvType != "")
            {
                cmbInsEligibilityPrimProvType.SelectedValue = oInsurance.InsuranceEligibilityProvType;
            }
            if (oInsurance.InsuranceEligibilityProvSecType != null && oInsurance.InsuranceEligibilityProvSecType != "")
            {
                cmbInsEligibilitySecProvType.SelectedValue = oInsurance.InsuranceEligibilityProvSecType;
            }
            if (oInsurance.EligibiltiContact != null && oInsurance.EligibiltiContact != "")
            {
                TxtEligiblitycntct.Text = oInsurance.EligibiltiContact;
            }
            if (oInsurance.EligibilityPhone != null && oInsurance.EligibilityPhone != "")
            {
                mskeligibiltyPhone.Text = oInsurance.EligibilityPhone;
            }
            if (oInsurance.Eligibilitywebsite != null && oInsurance.Eligibilitywebsite != "")
            {
                TxtEligibiltyWebste.Text = oInsurance.Eligibilitywebsite;
            }
            if (oInsurance.EligibilityNote != null && oInsurance.EligibilityNote != "")
            {
                txtEligibiltyNote.Text = oInsurance.EligibilityNote;
            }
        }

        public void Fill5010TransitionData(gloPMContacts.Insurance oInsurance)
        {
            chkIncludeRendering.Checked = oInsurance.IncludeRenderingProvider;
            chkIncludeOrdering.Checked = oInsurance.IncludeOrderingProvider;
            chkIncludeServiceFacility.Checked = oInsurance.IncludeServiceFacility;
            chkIncludeSubscriberAddress.Checked = oInsurance.IncludeSubscriberAddress;
        }

        private void FillInsuranceType()
        {
            gloContact ogloContacts = new gloContact(_databaseconnectionstring);
            DataTable dtInsuranceType = ogloContacts.GetInsuranceTypes();
            try
            {

                if (dtInsuranceType != null && dtInsuranceType.Rows.Count > 0)
                {
                    DataRow dr = dtInsuranceType.NewRow();
                    dr["sInsuranceTypeCode"] = "";
                    dr["sInsuranceTypeDesc"] = "";
                    dtInsuranceType.Rows.InsertAt(dr, 0);
                    dtInsuranceType.AcceptChanges();

                    cmbInsuranceType.DataSource = dtInsuranceType;
                    cmbInsuranceType.DisplayMember = "sInsuranceTypeDesc";
                    cmbInsuranceType.ValueMember = "sInsuranceTypeCode";
                    cmbInsuranceType.Refresh();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
            }
        }
        //Shubhangi
        private void FillInsuranceCompany()
        {
            gloContact ogloContacts = new gloContact(_databaseconnectionstring);
            try
            {
                dtInsuranceCompany = ogloContacts.GetInsuranceCompanyDetails();
                if (dtInsuranceCompany != null && dtInsuranceCompany.Rows.Count > 0)
                {
                    isCmbInsuranceCompanyLoading = true;
                    DataRow dr = dtInsuranceCompany.NewRow();
                    dr["nID"] = 0;
                    dr["sDescription"] = "";
                    dtInsuranceCompany.Rows.InsertAt(dr, 0);
                    dtInsuranceCompany.AcceptChanges();

                    cmbInsuranceCompany.DataSource = dtInsuranceCompany;
                    cmbInsuranceCompany.DisplayMember = "sDescription";
                    cmbInsuranceCompany.ValueMember = "nID";
                    cmbInsuranceCompany.Refresh();
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                isCmbInsuranceCompanyLoading = false;
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
            }
        }
        private void FillReportingCategory()
        {
            gloContact ogloContacts = new gloContact(_databaseconnectionstring);
            try
            {
                DataTable dtReportingCategory = ogloContacts.GetReportingCategory();
                if (dtReportingCategory != null && dtReportingCategory.Rows.Count > 0)
                {
                    DataRow dr = dtReportingCategory.NewRow();
                    dr["nID"] = 0;
                    dr["sDescription"] = "";
                    dtReportingCategory.Rows.InsertAt(dr, 0);
                    dtReportingCategory.AcceptChanges();

                    cmbReportingCategory.DataSource = dtReportingCategory;
                    cmbReportingCategory.DisplayMember = "sDescription";
                    cmbReportingCategory.ValueMember = "nID";
                    cmbReportingCategory.Refresh();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
            }


        }

     
      

        private void FillBox11dSettings()
        {
            
            using (DataTable dtBox11dSettings = new DataTable())
            {
                dtBox11dSettings.Columns.Add("SettingID", System.Type.GetType("System.Int32"));
                dtBox11dSettings.Columns.Add("Setting",System.Type.GetType("System.String"));
                
                dtBox11dSettings.Rows.Add();
                dtBox11dSettings.Rows[0]["SettingID"] = 1;
                dtBox11dSettings.Rows[0]["Setting"] = "";
                
                dtBox11dSettings.Rows.Add();
                dtBox11dSettings.Rows[1]["SettingID"] = 2;
                dtBox11dSettings.Rows[1]["Setting"] = "Employer Name";
                
                dtBox11dSettings.Rows.Add(); 
                dtBox11dSettings.Rows[2]["SettingID"] = 3;
                dtBox11dSettings.Rows[2]["Setting"] = "WC# or Auto# from Claim";
                
                dtBox11dSettings.Rows.Add();
                dtBox11dSettings.Rows[3]["SettingID"] = 4;
                dtBox11dSettings.Rows[3]["Setting"] = "Insurance ID";
               
                dtBox11dSettings.Rows.Add();
                dtBox11dSettings.Rows[4]["SettingID"] = 5;
                dtBox11dSettings.Rows[4]["Setting"] = "Group ID";
              

                dtBox11dSettings.AcceptChanges();
                cmbBox11bSettings.DataSource = dtBox11dSettings.Copy();
                cmbBox11bSettings.ValueMember = "SettingID";
                cmbBox11bSettings.DisplayMember = "Setting";
                cmbBox11bSettings.Refresh();
           
            }
          
        }

        private void FillFUAction()
        {
            gloContact ogloContacts = new gloContact(_databaseconnectionstring);
            try
            {
                DataTable dtFUAction = ogloContacts.GetFUAction();

                if (dtFUAction != null && dtFUAction.Rows.Count > 0)
                {
                    DataRow dr = dtFUAction.NewRow();
                    dr["sFollowUpActionCode"] = "";
                    dr["sFollowUpDesc"] = "";
                    dtFUAction.Rows.InsertAt(dr, 0);
                    dtFUAction.AcceptChanges();
                    DataTable dtFUActionRebill = dtFUAction.Copy();
                    cmbInsClmStartFUAction.DataSource = dtFUAction;
                    cmbInsClmStartFUAction.DisplayMember = "sFollowUpDesc";
                    cmbInsClmStartFUAction.ValueMember = "sFollowUpActionCode";
                    cmbInsClmStartFUAction.Refresh();

                    cmbInsClmRebillFUAction.DataSource = dtFUActionRebill;
                    cmbInsClmRebillFUAction.DisplayMember = "sFollowUpDesc";
                    cmbInsClmRebillFUAction.ValueMember = "sFollowUpActionCode";
                    cmbInsClmRebillFUAction.Refresh();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
            }


        }

        private void FillBillUnitsAs()
        {
            using (DataTable dtBillUnitsAs = new DataTable())
            {
                dtBillUnitsAs.Columns.Add("nID");
                dtBillUnitsAs.Columns.Add("sDesc");
                dtBillUnitsAs.Rows.Add();
                dtBillUnitsAs.Rows[0]["nID"] = "Units";
                dtBillUnitsAs.Rows[0]["sDesc"] = "Units";
                dtBillUnitsAs.Rows.Add();
                dtBillUnitsAs.Rows[1]["nID"] = "Minutes";
                dtBillUnitsAs.Rows[1]["sDesc"] = "Minutes";
                dtBillUnitsAs.AcceptChanges();
                cmbBillUnitsAs.DataSource = dtBillUnitsAs.Copy();
                cmbBillUnitsAs.ValueMember = "nID";
                cmbBillUnitsAs.DisplayMember = "sDesc";
                cmbBillUnitsAs.Refresh();
            }
        }

        private void FillEDIAltPayerID()
        {
            using (DataTable dtEDIAltPayerID = new DataTable())
            {
                dtEDIAltPayerID.Columns.Add("sType");
                dtEDIAltPayerID.Columns.Add("sDesc");
                dtEDIAltPayerID.Rows.Add();
                dtEDIAltPayerID.Rows[0]["sType"] = "FY";
                dtEDIAltPayerID.Rows[0]["sDesc"] = "Claim Off. # - FY";
                dtEDIAltPayerID.Rows.Add();
                dtEDIAltPayerID.Rows[1]["sType"] = "2U";
                dtEDIAltPayerID.Rows[1]["sDesc"] = "Payer ID # - 2U";
                dtEDIAltPayerID.Rows.Add();
                dtEDIAltPayerID.Rows[2]["sType"] = "EI";
                dtEDIAltPayerID.Rows[2]["sDesc"] = "Empl. ID # - EI";
                dtEDIAltPayerID.Rows.Add();
                dtEDIAltPayerID.Rows[3]["sType"] = "NF";
                dtEDIAltPayerID.Rows[3]["sDesc"] = "NAIC Code - NF";
                dtEDIAltPayerID.AcceptChanges();
                cmbRefSecIdentification.DataSource = dtEDIAltPayerID.Copy();
                cmbRefSecIdentification.ValueMember = "sType";
                cmbRefSecIdentification.DisplayMember = "sDesc";
                cmbRefSecIdentification.Refresh();
            }
        }

        //End

        private void FillInsEligibilityProviderType()
        {
            DataTable dtInsEligibilityProvPrimType = new DataTable();
            DataTable dtInsEligibilityProvSecType = new DataTable();
            try
            {

                //Fill cmbInsEligibilityPrimProvType
                dtInsEligibilityProvPrimType.Columns.Add("sInsEligibilityProvPrimTypeQualifier");
                dtInsEligibilityProvPrimType.Columns.Add("sInsEligibilityProvPrimType");

                dtInsEligibilityProvPrimType.Rows.Add();
                dtInsEligibilityProvPrimType.Rows[0]["sInsEligibilityProvPrimTypeQualifier"] = "XX";
                dtInsEligibilityProvPrimType.Rows[0]["sInsEligibilityProvPrimType"] = "XX - NPI(Centers for Medicare and Medicaid Services National Provider Identifier)";

                dtInsEligibilityProvPrimType.Rows.Add();
                dtInsEligibilityProvPrimType.Rows[1]["sInsEligibilityProvPrimTypeQualifier"] = "FI";
                dtInsEligibilityProvPrimType.Rows[1]["sInsEligibilityProvPrimType"] = "FI - Federal Taxpayer’s Identification Number";


                dtInsEligibilityProvPrimType.AcceptChanges();

                cmbInsEligibilityPrimProvType.DataSource = dtInsEligibilityProvPrimType;
                cmbInsEligibilityPrimProvType.DisplayMember = "sInsEligibilityProvPrimType";
                cmbInsEligibilityPrimProvType.ValueMember = "sInsEligibilityProvPrimTypeQualifier";
                cmbInsEligibilityPrimProvType.Refresh();


                //Fill cmbInsEligibilitySecProvType
                dtInsEligibilityProvSecType.Columns.Add("sInsEligibilityProvSecTypeQualifier");
                dtInsEligibilityProvSecType.Columns.Add("sInsEligibilityProvSecType");

                dtInsEligibilityProvSecType.Rows.Add();
                dtInsEligibilityProvSecType.Rows[0]["sInsEligibilityProvSecTypeQualifier"] = "";
                dtInsEligibilityProvSecType.Rows[0]["sInsEligibilityProvSecType"] = "";

                dtInsEligibilityProvSecType.Rows.Add();
                dtInsEligibilityProvSecType.Rows[1]["sInsEligibilityProvSecTypeQualifier"] = "TJ";
                dtInsEligibilityProvSecType.Rows[1]["sInsEligibilityProvSecType"] = "TJ - Federal Taxpayer’s Identification Number";

                dtInsEligibilityProvSecType.Rows.Add();
                dtInsEligibilityProvSecType.Rows[2]["sInsEligibilityProvSecTypeQualifier"] = "HPI";
                dtInsEligibilityProvSecType.Rows[2]["sInsEligibilityProvSecType"] = "HPI - NPI Centers for Medicare and Medicaid Services National Provider Identifier";


                dtInsEligibilityProvSecType.Rows.Add();
                dtInsEligibilityProvSecType.Rows[3]["sInsEligibilityProvSecTypeQualifier"] = "Q4";
                dtInsEligibilityProvSecType.Rows[3]["sInsEligibilityProvSecType"] = "Q4 - Prior Identifier Number";


                dtInsEligibilityProvSecType.AcceptChanges();

                cmbInsEligibilitySecProvType.DataSource = dtInsEligibilityProvSecType;
                cmbInsEligibilitySecProvType.DisplayMember = "sInsEligibilityProvSecType";
                cmbInsEligibilitySecProvType.ValueMember = "sInsEligibilityProvSecTypeQualifier";
                cmbInsEligibilitySecProvType.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }



        }

        private void txtZip_TextChanged(object sender, EventArgs e)
        {
            if (txtZip.Text.Trim() != "")
            {
                DataTable dt = null;
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = " + txtZip.Text.Trim() + "";

                    txtState.Text = "";
                    txtCity.Text = "";

                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtState.Text = Convert.ToString(dt.Rows[0]["ST"]);
                        txtCity.Text = Convert.ToString(dt.Rows[0]["City"]);
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    dt.Dispose();
                    oDb.Disconnect();
                    oDb.Dispose();
                }
            }
            else
            {
                txtState.Text = "";
                txtCity.Text = "";
            }

        }

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            //code to allow nos only 
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {

                e.Handled = true;
            }
        }

        //private void cmbInsuranceType_MouseMove(object sender, MouseEventArgs e)
        //{
        //    //try
        //    //{
        //    //    System.Drawing.Point p = new Point(e.X, e.Y);
        //    //    object obj = (cmbInsuranceType.GetChildAtPoint(p));
        //    //    int index = cmbInsuranceType.Items.IndexOf(obj);

        //    //    cmbInsuranceType.  
        //    //    if (index != ListBox.NoMatches)
        //    //    {
        //    //        if (LastIndex != index)
        //    //        {
        //    //            string s = cmbInsuranceType.Items[index].ToString();
        //    //            ttInsuranceType.Show(s, cmbInsuranceType);
        //    //            //IntPtr hdc = GetDC(this.Handle);
        //    //            //SIZE size;
        //    //            //size.cx = 0;
        //    //            //size.cy = 0;
        //    //            //GetTextExtentPoint32(hdc, s, s.Length, ref size);
        //    //            //ReleaseDC(this.Handle, hdc);

        //    //            //if (this.Width < size.cx)
        //    //            //    tp.SetToolTip(this, s);

        //    //            //LastIndex = index;
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception)
        //    //{

        //    //    //throw;
        //    //}

        //    //For Implementing Tool tip on combo Box -- Added By Pramod Nair on 20100121 
        //    combo = (ComboBox)sender;
        //    if (cmbInsuranceType.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsuranceType.Items[cmbInsuranceType.SelectedIndex])["sInsuranceTypeDesc"]), cmbInsuranceType) >= cmbInsuranceType.DropDownWidth - 18)
        //            this.toolTip1.SetToolTip(cmbInsuranceType,Convert.ToString(((DataRowView)cmbInsuranceType.Items[cmbInsuranceType.SelectedIndex])["sInsuranceTypeDesc"]));//, cmbInsuranceType, 0, cmbInsuranceType.Bottom - 40);
        //        else
        //            this.toolTip1.Hide(combo);

        //    }
        //}

        #endregion

        #region "Tool Strip Buttons"

        private bool SaveInsurancePlan()
        {
            bool _ReturnResult = true;
            gloPMContacts.gloContacts oglocontact = null;
            try
            {
                //Added By MaheshB

                oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                bool _result;
                long ii = _ContactID;
                string _Insurance = txtname.Text;
                //Shubhangi 20091112
                //First require to do validation then check is Exist
                if (ValidateData() == true)
                {
                    Int64 _CompanyID = Convert.ToInt64(cmbInsuranceCompany.SelectedValue);
                    _result = oglocontact.IsExistsInsurance(_Insurance, _ContactID, _CompanyID);
                    if (_result == true)
                    {
                        //Commented by Mayuri:20091105
                        //Change of messagebox for gloEMR50RTM1-Case
                        //if (DialogResult.No == (MessageBox.Show("Insurance name already exists.  Do you want register anyway?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)))
                        if (DialogResult.No == (MessageBox.Show("Contact name already exists. Do you want to register it anyway?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
                        {
                            return false;
                        }

                    }



                    if (SaveData() == true)
                    {

                        this.Close();
                    }

                }
                else
                {
                    _ReturnResult = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _ReturnResult = false;
            }
            finally
            {
                //if()
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
            }
            return _ReturnResult;
        }
        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            if (SaveInsurancePlan() == true)
                this.Close();

        }
        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region "Save Modify Methods"

        private bool IsExistsInsurance(string PlanName)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "";

                _sqlQuery = "select Count(sName) from Contacts_mst where  sContactType = 'Insurance' and sName = '" + PlanName + "'";

                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;

        }

        private bool SaveData()
        {
            gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloPMContacts.Insurance oInsurance = new gloPMContacts.Insurance();
            gloPMContacts.gloContacts ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);
            bool _Result = false;
            bool _IsModify=false;
            try
            {
                _IsSaveClicked = true;
                oInsurance = AddInsuranceContacts();
                //New Contact
                if (_ContactID == 0)
                {
                    _IsModify=false;
                    //if (_IsCopy)
                    //{
                    //    if (IsExistsInsurance(txtname.Text))
                    //    {
                    //        DialogResult dResult = MessageBox.Show("Contact name already exists. Do you want to register it anyway?  ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    //        if (dResult == DialogResult.No)
                    //        {
                    //            return false;
                    //        }
                    //    }
                    //}
                    _bIsGroupMandatory = oInsurance.bIsGroupMandatory;
                    _ContactID = ogloContacts.Add(oInsurance);
                    if (_ContactID == 0)
                    {
                        _Result = false;
                        if (_IsCopy == true)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Copy, "Copy insurance", 0, _ContactID, 0, ActivityOutCome.Failure);
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Add, "Add insurance", 0, _ContactID, 0, ActivityOutCome.Failure);
                        }
                    }
                    else
                    {
                        //abhisekh 18 aug 2010
                        if (chkCorrectRplmnt.Checked == true)
                            SaveCorrectedReplacement(_ContactID);

                        //SaveFeeSchedule_Old(_ContactID);
                        SaveFeeSchedule(_ContactID);

                        _Result = true;
                        if (_IsCopy == true)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Copy, "Insurance copied", 0, _ContactID, 0, ActivityOutCome.Success);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Copy, "Insurance copied payerID=" + oInsurance.sPayerID + "", 0, _ContactID, 0, ActivityOutCome.Success);
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Add, "Insurance added", 0, _ContactID, 0, ActivityOutCome.Success);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Add, "Insurance added payerID=" + oInsurance.sPayerID + "", 0, _ContactID, 0, ActivityOutCome.Success);
                        }
                    }
                }
                else
                {
                    oInsurance.ContactID = _ContactID;
                    _IsModify=true;
                    _bIsGroupMandatory = oInsurance.bIsGroupMandatory;
                    if (ogloContacts.Add(oInsurance) == 0)
                    {
                        _Result = false;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Modify, "Modify insurance ", 0, _ContactID, 0, ActivityOutCome.Failure);
                    }
                    else
                    {


                        SaveCorrectedReplacement(_ContactID);
                        //Save Fee Schedule for the Contact(Insurance)
                        //SaveFeeSchedule_Old(_ContactID);
                        SaveFeeSchedule(_ContactID);

                        if (ModifyPatientInsurance(oInsurance) != true)
                        {
                            _Result = false;
                        }
                        else
                        {
                            _Result = true;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Modify, "Insurance modified", 0, _ContactID, 0, ActivityOutCome.Success);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Modify, "Insurance modified payerID=" + oInsurance.sPayerID + "", 0, _ContactID, 0, ActivityOutCome.Success);
                        }
                    }

                }
                if (_Result == true)
                {
                    string _strsql = "";
                    //DataTable dt = new System.Data.DataTable();
                    Int64 _CompanyID = Convert.ToInt64(cmbInsuranceCompany.SelectedValue);
                    Int64 _ReportingCategoryID = Convert.ToInt64(cmbReportingCategory.SelectedValue);

                    oDb.Connect(false);
                    _strsql = "Delete from Contact_InsurancePlan_Association where nContactId = " + _ContactID + " and nClinicId = " + _ClinicID;
                    oDb.Execute_Query(_strsql);
                    if (_CompanyID > 0)
                    {
                        _strsql = "Insert into Contact_InsurancePlan_Association (nCompanyId,nContactId,nClinicId) Values(" + _CompanyID + "," + _ContactID + "," + _ClinicID + "" + ")";
                        oDb.Execute_Query(_strsql);
                    }
                    _strsql = "Delete from Contact_InsurancePlanReportingCat_Association where nContactId = " + _ContactID + " and nClinicId = " + _ClinicID;
                    oDb.Execute_Query(_strsql);
                    if (_ReportingCategoryID > 0)
                    {
                        _strsql = "Insert into Contact_InsurancePlanReportingCat_Association (nReportingCategoryID,nContactId,nClinicId) Values(" + _ReportingCategoryID + "," + _ContactID + "," + _ClinicID + "" + ")";
                        oDb.Execute_Query(_strsql);
                    }

                }

                GeneralSettings oSettings = null;
                oSettings = new GeneralSettings(_databaseconnectionstring);
                string sType = oSettings.GetInstallationType(0, 1);

                //if (_Result == true && _messageBoxCaption == "gloPM")
                if (_Result == true && (sType == "gloPM" || sType == "Both" || sType == "None"))
                {
                    if (_oPlanHold != null)
                    {
                        _oPlanHold.ContactID = _ContactID;
                        _oPlanHold.InsCompanyID = Convert.ToInt64(cmbInsuranceCompany.SelectedValue);
                        ogloContacts.AddorModifyHoldInfo(_oPlanHold);

                        //Show MessgaeBox.
                        //MaheshB 20100427
                        if (_oPlanHold.HoldModified == true && _oPlanHold.IsHold == true)
                        {
                            ogloContacts.RemoveClaimsForHold(_ContactID);
                        }
                    }

                    #region "Mid Level Settings "

                    string _strsql = "";

                    oDb.Connect(false);
                    _strsql = "Delete from BL_MidLevel_Settings where nContactId = " + _ContactID + " and nClinicId = " + _ClinicID;
                    oDb.Execute_Query(_strsql);

                    if (cmbMidLevelSpeProvider.Text != string.Empty && cmbMidLevelSpeProvider.SelectedIndex > 0)
                    {
                        ogloContacts.AddMidLevelSettings(0, Convert.ToInt64(cmbMidLevelSpeProvider.SelectedValue), _ContactID, 0, gloPMContacts.gloContacts.MidLevelSettingsType.SpecificPlan, _UserID, _ClinicID, DateTime.Now, DateTime.Now);
                    }

                    for (int iCount = 1; iCount <= c1MidlevelSettings.Rows.Count - 1; iCount++)
                    {
                        if (Convert.ToString(c1MidlevelSettings.GetData(iCount, (int)MidLevelGridColumn.SettingsName)) != " " && Convert.ToString(c1MidlevelSettings.GetData(iCount, (int)MidLevelGridColumn.SettingsName)) != string.Empty)
                        {
                            Int64 i = Convert.ToInt64(htMidLevelSettings[Convert.ToString(c1MidlevelSettings.GetData(iCount, (int)MidLevelGridColumn.SettingsName))]);

                            ogloContacts.AddMidLevelSettings(Convert.ToInt64(c1MidlevelSettings.GetData(iCount, (int)MidLevelGridColumn.ProviderID)), Convert.ToInt64(htMidLevelSettings[Convert.ToString(c1MidlevelSettings.GetData(iCount, (int)MidLevelGridColumn.SettingsName))]), _ContactID, 0, gloPMContacts.gloContacts.MidLevelSettingsType.SpecificPlanSpecificProvider, _UserID, _ClinicID, DateTime.Now, DateTime.Now);
                        }
                    }

                    _strsql = null;
                    #endregion

                    #region "Billing Taxonomy"

                    //_strsql = "";

                    //oDb.Connect(false);
                    //_strsql = "Delete from BL_TaxonomyCodes where nContactId = " + _ContactID + " and nClinicId = " + _ClinicID;
                    //oDb.Execute_Query(_strsql);

                    //if (cmbMidLevelSpeProvider.Text != string.Empty && cmbMidLevelSpeProvider.SelectedIndex > 0)
                    //{
                    //    ogloContacts.AddMidLevelSettings(0, Convert.ToInt64(cmbMidLevelSpeProvider.SelectedValue), _ContactID, 0, gloPMContacts.gloContacts.MidLevelSettingsType.SpecificPlan, _UserID, _ClinicID, DateTime.Now, DateTime.Now);
                    //}

                    for (int iCount = 1; iCount <= c1BillingTaxonomy.Rows.Count - 1; iCount++)
                    {
                        //if (Convert.ToString(c1BillingTaxonomy.GetData(iCount, (int)BillingTaxonomy.PlanOverride)) != " " && Convert.ToString(c1BillingTaxonomy.GetData(iCount, (int)BillingTaxonomy.PlanOverride)) != string.Empty)
                        //{
                        ogloContacts.AddBillingTaxonomy(Convert.ToInt64(c1BillingTaxonomy.GetData(iCount, (int)BillingTaxonomy.ProviderID)), Convert.ToString(c1BillingTaxonomy.GetData(iCount, (int)BillingTaxonomy.PlanOverride)), _ContactID, _UserID, _ClinicID, DateTime.Now, DateTime.Now);
                        //}
                    }

                    #endregion


                    #region "Alternate Payer ID"

                    SaveAlternatePayerId();

                    #endregion

                    //*************************************************
                    //Saving Alternate ID Settings
                    SaveAlternateIDSettings();
                    //Ends Here****************************************
                    //Save Expanded Claim Settings.
                    SaveExpandedClaimSettings();
                    SavePaperBillingSetting();
                    AddUB04ExtendSettings();
                    //
                }
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
                //Name|Plan Type|PayerID|Addr1|Addr2|City|State|Zip
                string sInsurancePlanDetails = string.Format("Insurance plan details :{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", Convert.ToString(oInsurance.Name), Convert.ToString(oInsurance.InsuranceTypeDesc), Convert.ToString(oInsurance.sPayerID), Convert.ToString(oInsurance.CompanyAddress.AddrressLine1), Convert.ToString(oInsurance.CompanyAddress.AddrressLine2), Convert.ToString(oInsurance.CompanyAddress.City), Convert.ToString(oInsurance.CompanyAddress.State), Convert.ToString(oInsurance.CompanyAddress.ZIP));
                MakeAuditLog(oInsurance.ContactID, sInsurancePlanDetails, _IsModify, _Result);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDb != null) { oDb.Disconnect(); oDb.Dispose(); }
                if (oInsurance != null) { oInsurance.Dispose(); }
                if (ogloContacts != null) { ogloContacts.Dispose(); }

            }
            return _Result;
        }

        private void MakeAuditLog(Int64 nInsContactID, string sInsuranceDetails, bool bIsMidify, bool bResult)
        {
            try
            {
                if (nInsContactID != 0)
                {
                    if (bIsMidify)
                    {
                        //acitity=modify
                        if (bResult)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.Modify, sInsuranceDetails, 0, nInsContactID, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.Modify, sInsuranceDetails, 0, nInsContactID, 0, gloAuditTrail.ActivityOutCome.Failure);
                        }
                    }
                    else
                    {
                        //acitity=add
                        if (bResult)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.Add, sInsuranceDetails, 0, nInsContactID, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.Add, sInsuranceDetails, 0, nInsContactID, 0, gloAuditTrail.ActivityOutCome.Failure);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.None, "Exception while recording log for Add insurance", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SaveAlternatePayerId()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            int Count = 0;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                if (_arrAlternatePayerID.Count > 0)
                {
                    for (int i = 0; i < _arrAlternatePayerID.Count; i++)
                    {
                        string ActionPerformed = "DELETED BY USERID " + Convert.ToString(_UserID);
                        oParameters.Clear();
                        oParameters.Add("@ID", _arrAlternatePayerID[i], ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@ContactID", this.ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sMachineName", System.Environment.MachineName.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 100);
                        oParameters.Add("@sActionPerformed", ActionPerformed, ParameterDirection.Input, SqlDbType.VarChar, 100);
                        oParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                        Count = oDB.Execute("ERA_IN_AlternatePayerID_HST", oParameters, out _oResult);

                        deleteAlternatePayerID(Convert.ToInt64(_arrAlternatePayerID[i]));
                    }
                }
                Count = 0;
                for (int j = 0; j < _objAlternatePayerIDs.Count; j++)
                {
                    AlternatePayerID _objAlternatePayerID = _objAlternatePayerIDs[j];
                    if (_IsCopy)
                    {
                        _objAlternatePayerID.ID = 0;
                    }
                    oParameters.Clear();
                    oParameters.Add("@ID", _objAlternatePayerID.ID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@ContactID", this.ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sName", _objAlternatePayerID.Name, ParameterDirection.Input, SqlDbType.VarChar, 100);
                    oParameters.Add("@sPayerID", _objAlternatePayerID.AlternatePayerId, ParameterDirection.Input, SqlDbType.VarChar, 100);
                    oParameters.Add("@sDescription", _objAlternatePayerID.Desc, ParameterDirection.Input, SqlDbType.VarChar, 100);
                    oParameters.Add("@sMachineName", System.Environment.MachineName.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 100);
                    oParameters.Add("@UserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    Count = oDB.Execute("ERA_IN_AlternatePayerID", oParameters, out _oResult);
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oParameters != null)
                    oParameters.Dispose();
                _oResult = null;
            }
        }

        private bool ModifyPatientInsurance(gloPMContacts.Insurance oInsurance)
        {
            gloPMContacts.gloContacts ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);
            bool _result = false;
            try
            {
                bool IsPatientInsurance = false;

                IsPatientInsurance = ogloContacts.IsPatientInsurance(oInsurance.ContactID);

                if (IsPatientInsurance == true)
                {
                    DialogResult _DialogResult = DialogResult.None;
                    _DialogResult = DialogResult.Yes;
                    // _DialogResult = MessageBox.Show("This Insurance is associated with patient.  Are you sure you want to modify?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (_DialogResult == DialogResult.Yes)
                    {
                        ogloContacts.ModifyPatientInsurance(oInsurance);
                        _result = true;
                    }
                    else if (_DialogResult == DialogResult.No)
                    {
                        _result = true;
                    }
                    else if (_DialogResult == DialogResult.Cancel)
                    {
                        _result = false;
                    }
                }
                else
                {
                    _result = true;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloContacts != null)
                {
                    ogloContacts.Dispose(); ogloContacts = null;
                }
            }
            return _result;
        }

        private gloPMContacts.Insurance AddInsuranceContacts()
        {
            gloPMContacts.Insurance oInsurance = new gloPMContacts.Insurance();

            _InsuranceName = txtname.Text;
            oInsurance.Name = txtname.Text;
            oInsurance.ContactName = txtcontact.Text;
            if (cmbInsuranceType.SelectedIndex != -1)
            {
                oInsurance.InsuranceTypeDesc = Convert.ToString(cmbInsuranceType.Text);
                oInsurance.InsuranceTypeCode = Convert.ToString(cmbInsuranceType.SelectedValue);
            }
            else
            {
                oInsurance.InsuranceTypeDesc = "";
                oInsurance.InsuranceTypeCode = "";
            }

            //oInsurance.CompanyAddress.AddrressLine1 = txtAddressLine1.Text.Trim();
            //oInsurance.CompanyAddress.AddrressLine2 = txtAddressLine2.Text.Trim();
            //oInsurance.CompanyAddress.City = txtCity.Text.Trim();
            //oInsurance.CompanyAddress.State = txtState.Text.Trim();
            //oInsurance.CompanyAddress.ZIP = txtZip.Text.Trim();

            //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 

            oInsurance.CompanyAddress.AddrressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
            oInsurance.CompanyAddress.AddrressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
            oInsurance.CompanyAddress.City = oAddresscontrol.txtCity.Text.Trim();
            oInsurance.CompanyAddress.State = oAddresscontrol.cmbState.Text.Trim();
            oInsurance.CompanyAddress.ZIP = oAddresscontrol.txtZip.Text.Trim();

            oInsurance.CompanyAddress.Phone = mtxtPhone.Text.Trim();  //Sandip Darade 20100216
            oInsurance.CompanyAddress.Fax = txtFax.Text.Trim();  //Sandip Darade 20100216
            oInsurance.CompanyAddress.Email = txtEmail.Text.Trim();
            oInsurance.CompanyAddress.URL = txtURL.Text.Trim();

            oInsurance.sPayerID = txtPayerID.Text.Trim();
            oInsurance.bBox31Blank = chkBox31Blank.Checked;
            oInsurance.b1stPointer = chkOnlyPrintFirstPointer.Checked;
            if (chkAcceptAssignment.Checked == true)
            {
                oInsurance.bAccessAssignment = true;
            }
            else
            {
                oInsurance.bAccessAssignment = false;
            }
            if (chkWorkersComp.Checked == true)
            {
                oInsurance.bIsWorkerComp = true;
            }
            else
            {
                oInsurance.bIsWorkerComp = false;
            }

            if (chkMedClaimRef.Checked == true)
            {
                oInsurance.bIncludeMedicareClaimRef = true;
            }
            else
            {
                oInsurance.bIncludeMedicareClaimRef = false;
            }

            oInsurance.bDoNotPrintFacility = chkDoNotPrintFacility.Checked;
            oInsurance.bMedigap = chkMedigap.Checked;
            oInsurance.bNameOfacilityinBox33 = chkNameOfFacilityinBox33.Checked;
            oInsurance.bReferringIDInBox19 = chkRefferingID.Checked;
            oInsurance.bShowPayment = chkShowPayment.Checked;
            if (rbStatementToPatientYes.Checked == true)
            {
                oInsurance.bStatementToPatient = true;
            }
            if (rbStatementToPatientNo.Checked == true)
            {
                oInsurance.bStatementToPatient = false;
            }

            if (cmbClearingHouse.SelectedIndex != -1)
            {
                oInsurance.nClearingHouse = Convert.ToInt64(cmbClearingHouse.SelectedValue);
            }

            if (cmbTypeOFBilling.Text == "Paper")
            {
                oInsurance.nTypeOBilling = gloPMContacts.TypeOfBilling.Paper;
            }
            else if (cmbTypeOFBilling.Text == "Electronic")
            {
                oInsurance.nTypeOBilling = gloPMContacts.TypeOfBilling.Electronic;
            }
            else
            {
                oInsurance.nTypeOBilling = gloPMContacts.TypeOfBilling.None;
            }
            if (chkGroupMandatory.Checked == true)
            {
                oInsurance.bIsGroupMandatory = true;
            }
            else
            {
                oInsurance.bIsGroupMandatory = false;
            }

            oInsurance.sWebsite = txtWebsite.Text.Trim();
            oInsurance.sServicingState = txtServicingState.Text.Trim();
            oInsurance.sPayerPhoneExtn = txt_PayerPhExt.Text.Trim();
            oInsurance.sPayerPhone = mtxt_PayerPhone.Text.Trim();// //Sandip Darade 20100216
            oInsurance.sComments = txtAdditionalInfo.Text.Trim();
            oInsurance.bIsClaims = chkClaims.Checked;
            oInsurance.bIsElectronicCOB = chkElectronicCOB.Checked;
            oInsurance.bIsEnrollmentRequired = chkEnrollmentRequired.Checked;
            oInsurance.bIsRealTimeClaimStatus = chkRealTimeClaimStatus.Checked;
            oInsurance.bIsRealTimeEligibility = chkRealTimeEligibility.Checked;
            oInsurance.bIsRemittanceAdvice = chkRemittanceAdvice.Checked;

            oInsurance.bIsOTAFAmount = chkIncludeOTAFAmount.Checked; //added on 27/04/2010

            oInsurance.ContactType = Convert.ToString(gloPMContacts.ContactType.Insurance);
            //Added by Anil 2009004
            oInsurance.bNotesInBox19 = chkNotesInBox19.Checked;
            oInsurance.OfficeID = txtOfficeID.Text.Trim();

            oInsurance.Box32 = cmbBox32.Text.Trim();
            oInsurance.Box32A = cmbBox32A.Text.Trim();
            oInsurance.Box32B = cmbBox32B.Text.Trim();

            oInsurance.Box33 = cmbBox33.Text.Trim();
            oInsurance.Box33A = cmbBox33A.Text.Trim();
            oInsurance.Box33B = cmbBox33B.Text.Trim();

            //Sandip Darade 20091211
            oInsurance.sDoNotPrintFacility = cmbdonotprintfacility.Text.Trim();

            if (cmbCptCrosswalk.SelectedValue != null)
            {
                oInsurance.CPTCrosswalkID = Convert.ToInt64(cmbCptCrosswalk.SelectedValue);
            }
            oInsurance.PARequired = chkPARequired.Checked;
            oInsurance.IsInstitutionalBilling = chkIsInstitutionalBilling.Checked;
            //oInsurance.FedTaxNoBox5 = cmbFedTaxNoBox5.Text;
            //oInsurance.FedTaxNoBoxCompanyQualifierID = Convert.ToInt16(cmbFedTaxNoBox5.SelectedValue);
            String SelectedFedTaxBox = cmbFedTaxNoBox5.Text;
            if(SelectedFedTaxBox.Contains("-"))
            {
                String strComapanyPrefix="Billing Provider Company ";
                String selectedCompanyIndex = SelectedFedTaxBox.Substring(strComapanyPrefix.Length, SelectedFedTaxBox.IndexOf("-") - strComapanyPrefix.Length).Trim();
                int _nCompanyIndex=0;
                if(int.TryParse(selectedCompanyIndex, out _nCompanyIndex))
                {
                       // oInsurance.FedTaxNoBoxCompanyIndex=_nCompanyIndex;
                }
            }
            oInsurance.operationgProviderBox77 = cmbOprtingPrvderBox77.Text;
            oInsurance.Box77RenderingProvider = cmbBox77Rendering.Text;
           // oInsurance.UB51BillingProvderOtherID = Convert.ToInt64(cmbUBBlngprvdraltID.SelectedValue);

            oInsurance.IsCorrectedReplacement = chkCorrectRplmnt.Checked;

            //Debasish 11192010
            //oInsurance.IsDefaultPriorAuthorizationRequired = chkPARequired.Checked;
            oInsurance.InsuranceEligibilityProviderID = FormatString(txtInsEligibilityPrimProvID.Text);

            oInsurance.InsuranceEligibilityProvSecID = FormatString(txtInsEligibilitySecProvID.Text);
            oInsurance.InsuranceEligibilityProvType = Convert.ToString(cmbInsEligibilityPrimProvType.SelectedValue);
            oInsurance.InsuranceEligibilityProvSecType = Convert.ToString(cmbInsEligibilitySecProvType.SelectedValue);

            //oInsurance.IsIncludeTaxonomyforPaper = chkIncludeTax4Paper.Checked;

            //oInsurance.IsIncludeTaxonomyforElectronic = chkIncludeTax4Elec.Checked;

            oInsurance.bPaperBillingTaxonomy = chkIncludeTaxBilPaper.Checked;

            oInsurance.bPaperRenderingTaxonomy = chkIncludeTaxRenPaper.Checked;

            oInsurance.bElectronicBillingTaxonomy = chkIncludeTaxBillElec.Checked;

            oInsurance.bElectronicRenderingTaxonomy = chkIncludeTaxRenElec.Checked;

            oInsurance.sQualifier = cmbQualifier.Text.Trim();

            oInsurance.sBillClaimOfficeNo = txtBillClaimOfficeNo.Text.Trim();
            oInsurance.sEDIAltPayerIDType = Convert.ToString(cmbRefSecIdentification.SelectedValue);
            oInsurance.sBox19DefaultNote = txtBox19DefaultNote.Text.Trim();
            oInsurance.IncludeRenderingProvider = chkIncludeRendering.Checked;
            oInsurance.IncludeOrderingProvider = chkIncludeOrdering.Checked;
            oInsurance.IncludeServiceFacility = chkIncludeServiceFacility.Checked;
            oInsurance.IncludeSubscriberAddress = chkIncludeSubscriberAddress.Checked;

            if (rbNone.Checked)
            {
                oInsurance.IncludePriorPatientPayment = null;
            }
            else if (rbSend.Checked)
            {
                oInsurance.IncludePriorPatientPayment = true;
            }
            else
            {
                oInsurance.IncludePriorPatientPayment = false;
            }

            //Added in 6031
            oInsurance.bIDInBox31 = chkIDInBox31.Checked;
            oInsurance.bIncludePlanName = chkIncludePlanName.Checked;
            oInsurance.bPaperDisplayMailingAddress = chkPaperDisplayMailingAddress.Checked;
            oInsurance.bSwap1a9a1aMCare = chkSwap1a9a1aMCare.Checked;
            //**

            oInsurance.bIncludeRendering_Attending = chkIncludeRendering_Attending.Checked;
            oInsurance.bDefaultOccuranceDOS = chkDefaultDOS.Checked;
            oInsurance.EligibiltiContact = TxtEligiblitycntct.Text.Trim();
            oInsurance.EligibilityPhone = mskeligibiltyPhone.Text.Trim();
            oInsurance.Eligibilitywebsite = TxtEligibiltyWebste.Text.Trim();
            oInsurance.EligibilityNote = txtEligibiltyNote.Text.Trim();

            oInsurance.bIsBillEPSDTorFamilyPlanning = chkBillEpsdtFamPlan.Checked;
            oInsurance.bIsEDIIncludeSV = chkIncludeSV.Checked;
            oInsurance.bIsEDIIncludeCRC = chkIncludeCRC.Checked;
            oInsurance.bIsPaperIncludeReferralCode = chkIncludeRefCode.Checked;
            oInsurance.sPaperClaimEPSDTCode = txtEPSDTCode.Text;
            oInsurance.sPaperClaimEPSDTCodeBox = Convert.ToString(cmbEPSDTCodeBox.SelectedValue);
            oInsurance.sPaperClaimFamilyPlanningCode = txtFamPlanCode.Text;
            oInsurance.sPaperClaimFamilyPlanningCodeBox = Convert.ToString(cmbFamPlanCodeBox.SelectedValue);
            oInsurance.bIsSupressRenderEPSDTClaimOnPaperEDI = chkSuppressRenderPaperEdi.Checked;

            oInsurance.bEMGAsX = ChkEMGAsX.Checked;
            oInsurance.bShowClaim = chkShowClaim.Checked;
            if (cmbBillUnitsAs.SelectedValue != null)
            {
                oInsurance.sBillUnitsAs = Convert.ToString(cmbBillUnitsAs.SelectedValue);
            }

            if (txtBaseUnits.Text != "")
            {
                oInsurance.nMinutesPerUnits = Convert.ToInt32(txtBaseUnits.Text.ToString());
            }
            oInsurance.bIsClaimFrequencyOne = chkClaimFreq.Checked;
            oInsurance.bIncludeReferring_supervising = chkIncludeRefnSupervising.Checked;
            oInsurance.bIncludeReferring_ordering = chkIncludeRefnOrdering.Checked;
            oInsurance.bIncludePatientSSN = chkIncludePatientSSN.Checked;
            oInsurance.bIncludeMod_in_SVD = chkIncludeMod_in_SVD.Checked;
            oInsurance.bIncludePrimaryDxInBox69 = chkIncludePrimaryDxInBox69.Checked;
            if (cmbCMSDateFormat.Text != null)
            {
                oInsurance.sCMSDateFormat = Convert.ToString(cmbCMSDateFormat.Text);
            }

            oInsurance.bIncludeUB04AdmissionHour = chkIncludeUB04AdmissionHour.Checked;
            oInsurance.bIncludeUB04DischargeHour = chkIncludeUB04DischargeHour.Checked;
            oInsurance.bIncludeUB04RevenueCodeTotal = ckhSentUB04RevenuecodeTotal.Checked;
            oInsurance.bIncludeSecondaryPayerAddress = chkSecondaryPayerAddress.Checked;
            //// Start 
            ////Sandip Darade 20100216
            ////case  GLO2008-0002029
            ////phone no,mobile no ,fax no will be saved with  mask e.g .(111)222-3333

            //if (mtxtMobile.Text.Length != 0 && mtxtMobile.MaskFull == false)
            //{
            //    oInsurance.Mobile = "";
            //}
            //else
            //{
            //    oInsurance.Mobile = mtxtMobile.Text;

            //}
            //if (mtxtPhone.Text.Length != 0 && mtxtPhone.MaskFull == false)
            //{
            //    oInsurance.CompanyAddress.Phone = "";
            //}
            //else
            //{
            //    oInsurance.CompanyAddress.Phone = mtxtPhone.Text;

            //}

            //if (mtxt_PayerPhone.Text.Length != 0 && mtxt_PayerPhone.MaskFull == false)
            //{
            //    oInsurance.sPayerPhone = "";
            //}
            //else
            //{
            //    oInsurance.sPayerPhone = mtxt_PayerPhone.Text;

            //}

            //if (_IsInternetFax == true)
            //{
            //    if (txtFax.Text.Length != 0 && txtFax.MaskFull == false)
            //    {
            //        oInsurance.CompanyAddress.Fax = "";
            //    }
            //    else
            //    {
            //        oInsurance.CompanyAddress.Fax = txtFax.Text.Trim();

            //    }
            //}
            //else
            //{
            //    oInsurance.CompanyAddress.Fax = txtFax.Text.Trim();
            //}
            ////Sandip Darade 20100216
            //// end 

            oInsurance.nBox11bSettingID = Convert.ToInt32(cmbBox11bSettings.SelectedValue);
            //if block added for include EDI Alt. Payer ID on secondary claims 06May2014 Sameer
            if (chkEdiAltPayerID.Checked == true)
            {
                oInsurance.bIncludeEdiAltPayerID = true;
            }
            else
            {
                oInsurance.bIncludeEdiAltPayerID = false;
            }
            oInsurance.bReportClinicName = chkReportClinicName.Checked;
            
            return oInsurance;
        }


        #region "Email Address Validation"

        public bool CheckEmailAddress(string input)
        {
            bool response = false;
            if (Regex.IsMatch(input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") || input.Trim() == "")
            {
                response = true;
            }
            else
            {
                response = false;
            }
            return response;
        }

        public bool CheckURL(string input)
        {
            bool response = false;

            System.Globalization.CompareInfo cmpUrl = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;

            //if (cmpUrl.IsPrefix(input, "http://") == false)
            //{ input = "http://" + input; }

            Regex RgxUrl = new Regex("^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\\+.~#?&//=]+$");

            if (RgxUrl.IsMatch(input))
            { response = true; }
            else
            { response = false; }

            return response;
        }


        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        #endregion

        private bool ValidateData()
        {
            if (txtname.Text.Trim() == "")
            {
                MessageBox.Show("Enter insurance plan name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbInsuranceSetup.SelectedIndex = (int)TabPages.InsuranSetup;
                txtname.Focus();
                return false;
            }
            //20100313
            if (cmbInsuranceType.Text == "")
            {
                MessageBox.Show("Enter plan type.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbInsuranceSetup.SelectedIndex = (int)TabPages.InsuranSetup;
                cmbInsuranceType.Focus();
                return false;
            }

            //Expanded Claim Settings validation                        
            if (bBillingtype == true)
            {
                if (cmbTypeOFBilling.Text == "")
                {
                    // MessageBox.Show("You have not selected any default billing method." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (DialogResult.No == (MessageBox.Show("You have not selected any default billing method." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)))
                    {
                        tbInsuranceSetup.SelectedIndex = (int)TabPages.Billing;
                        cmbTypeOFBilling.Focus();
                        return false;
                    }

                }
            }

            if (ValidateExpClaimData() == false)
            {
                return false;
            }


            //if (Convert.ToInt64(cmbInsuranceCompany.SelectedValue) <= 0)
            //    // &&  _DefaultCompanyId )
            //{
            //    if (_ContactID  == 0)
            //    {
            //        MessageBox.Show("Select company.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cmbInsuranceCompany.Focus();
            //        return false;
            //    }
            //    else
            //    {
            //        if (_DefaultCompanyId != 0)
            //        {
            //            MessageBox.Show("Select company.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            cmbInsuranceCompany.Focus();
            //            return false;
            //        }

            //    }

            //}
            //Sandip Darade 20090424
            ///Validations  removed as gloMask control being used
            //////validate the phone no. and mobile no.
            ////if (mtxtPhone.Text.Trim().Length > 0 & mtxtPhone.Text.Trim().Length < 10)
            ////{
            ////    MessageBox.Show("Please enter a 10 digit number for phone.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    mtxtPhone.Focus();
            ////    return false;
            ////}
            //code start added by kanchan on 20130613 to avoid multiple messages
            if (mtxtPhone.IsValidated == false)
            {
                tbInsuranceSetup.SelectedIndex = (int)TabPages.InsuranSetup;
                mtxtPhone.Focus();
                return false;
            }
            
            if (txtFax.IsValidated == false)
            {
                tbInsuranceSetup.SelectedIndex = (int)TabPages.InsuranSetup;
                txtFax.Focus();
                return false;
            }
            if (mskeligibiltyPhone.IsValidated == false)
            {
                tbInsuranceSetup.SelectedIndex = (int)TabPages.Eligibility;
                mskeligibiltyPhone.Focus();
                return false;
            }
            //code end added by kanchan on 20130613 to avoid multiple messages

            //////validate the phone no. and mobile no.
            ////if (mtxtMobile.Text.Trim().Length > 0 & mtxtMobile.Text.Trim().Length < 10)
            ////{
            ////    MessageBox.Show("Please enter a 10 digit number for mobile.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    mtxtMobile.Focus();
            ////    return false;
            ////}

            if (CheckEmailAddress(txtEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }


            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                if (CheckURL(txtURL.Text) == false)
                {
                    MessageBox.Show("Please enter a valid URL ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtURL.Focus();
                    return false;
                }

            }
            if (!string.IsNullOrEmpty(TxtEligibiltyWebste.Text))
            {
                if (CheckURL(TxtEligibiltyWebste.Text) == false)
                {
                    MessageBox.Show("Please enter a valid website ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtEligibiltyWebste.Focus();
                    return false;
                }
            }

            //Sandip Darade 20090424
            ///Validations  removed as gloMask control being used
            //Validate Payer's phone 
            //if (mtxt_PayerPhone.Text.Trim().Length > 0 & mtxt_PayerPhone.Text.Trim().Length < 10)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for payer phone.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mtxt_PayerPhone.Focus();
            //    return false;
            //}
            if (mtxt_PayerPhone.IsValidated == false)
            {
                return false;
            }

            if (chkServiceFacOtherID.Checked && cmbServiceFacilityOtherIDType.SelectedIndex <= 0)
            {
                MessageBox.Show("Select \"Other ID Type\"  for service facility source. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbInsuranceSetup.SelectedIndex = (int)TabPages.Billing;
                cmbServiceFacilityOtherIDType.Focus();
                return false;
            }

            if (chkBillingProviderOtherID.Checked && cmbBillingProviderSourceOtherIDType.SelectedIndex <= 0)
            {
                MessageBox.Show("Select \"Other ID Type\"  for billing provider source.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbInsuranceSetup.SelectedIndex = (int)TabPages.Billing;
                cmbBillingProviderSourceOtherIDType.Focus();
                return false;
            }

            if (cmbClearingHouse.SelectedValue != null && cmbClearingHouse.SelectedIndex != -1)
            {
                if (IsInsuranceUsedForBilling(_ContactID) == true && Convert.ToInt64(cmbClearingHouse.SelectedValue) != _OriginalClearingHouse && _ContactID != 0)
                {
                    if (MessageBox.Show("Claims exist for this insurance plan. Changing the clearinghouse can cause batch sending errors and/or Re-bills to go through different clearinghouses. Continue?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            if(cmbOprtingPrvderBox77.Text != "")
            {
                if (cmbBox77Rendering.Text != "" && (cmbBox77Rendering.Text == cmbOprtingPrvderBox77.Text || cmbBox77Rendering.Text == "Both Operating and Attending" || cmbOprtingPrvderBox77.Text == "Both Operating and Attending"))
                {
                    MessageBox.Show("UB/Institutional Box 76,77 conflict for billing and rendering provider. Please correct to save insurance plan.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbInsuranceSetup.SelectedIndex = (int)TabPages.UB;
                    cmbOprtingPrvderBox77.Focus();
                    return false;
                }
            }

            //if ((cmbOprtingPrvderBox77.SelectedIndex * cmbBox77Rendering.SelectedIndex) != 0 && (cmbOprtingPrvderBox77.SelectedIndex * cmbBox77Rendering.SelectedIndex) != 2)
            //{
            //    MessageBox.Show("UB/Institutional Box 76,77 conflict for billing and rendering provider. Please correct to save insurance plan.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    tbInsuranceSetup.SelectedIndex = (int)TabPages.UB;
            //    cmbOprtingPrvderBox77.Focus();
            //    return false;
            //}

            return true;
        }

        private bool IsInsuranceUsedForBilling(Int64 _ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string _strSQL = String.Empty;
            bool _result = false;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                _strSQL = " SELECT  ISNULL(Count(BL_Transaction_Claim_MST.nTransactionID),0) as nTransactionCount FROM   BL_Transaction_Claim_MST with (nolock) " +
                          " where BL_Transaction_Claim_MST.nContactID = " + _ContactID + " and ISNULL(BL_Transaction_Claim_MST.bIsVoid,0) = 0 ";
                _result = Convert.ToBoolean(oDB.ExecuteScalar_Query(_strSQL));
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _messageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _strSQL = null;
            }
            return _result;
        }

        private void Fill_CmbClearinghouse()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtClearing = null;
            string _sqlQuery = "";
            try
            {


                oDB.Connect(false);

                _sqlQuery = "SELECT ISNULL(nClearingHouseID,0) AS nClearingHouseID,ISNULL(sClearingHouseCode,'') AS  sClearingHouseName "
                            + " FROM BL_ClearingHouse_MST  WHERE nClinicID = " + _ClinicID + " ORDER By isnull(bIsDefault,0) desc";

                oDB.Retrive_Query(_sqlQuery, out dtClearing);


                if (dtClearing != null && dtClearing.Rows.Count > 0)
                {
                    DataRow dr = dtClearing.NewRow();
                    dr["nClearingHouseID"] = 0;
                    dr["sClearingHouseName"] = "";
                    dtClearing.Rows.InsertAt(dr, 0);
                    dtClearing.AcceptChanges();

                    cmbClearingHouse.DataSource = dtClearing;
                    cmbClearingHouse.DisplayMember = "sClearingHouseName";
                    cmbClearingHouse.ValueMember = "nClearingHouseID";
                    cmbClearingHouse.Refresh();
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
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }


        }

        #endregion

        //int LastIndex = -1;

        #region " Fee Schedule "

        #region "Design Grid"

        private void DesignGrid()
        {
            //c1CPTs.Clear(C1.Win.C1FlexGrid.ClearFlags.All);

            //c1CPTs.Cols.Count = COL_COUNT;
            //c1CPTs.Rows.Fixed = 1;
            //c1CPTs.Rows.Count = 2;

            //c1CPTs.Cols[COL_CPT_CODE].DataType = typeof(System.String);
            //c1CPTs.Cols[COL_CPT_DESC].DataType = typeof(System.String);
            //c1CPTs.Cols[COL_CPT_CHARGES].DataType = typeof(System.Decimal);
            //c1CPTs.Cols[COL_MOD1_CODE].DataType = typeof(System.String);
            //c1CPTs.Cols[COL_MOD1_CHARGES].DataType = typeof(System.Decimal);
            //c1CPTs.Cols[COL_MOD2_CODE].DataType = typeof(System.String);
            //c1CPTs.Cols[COL_MOD2_CHARGES].DataType = typeof(System.Decimal);
            //c1CPTs.Cols[COL_MOD3_CODE].DataType = typeof(System.String);
            //c1CPTs.Cols[COL_MOD3_CHARGES].DataType = typeof(System.Decimal);
            //c1CPTs.Cols[COL_MOD4_CODE].DataType = typeof(System.String);
            //c1CPTs.Cols[COL_MOD4_CHARGES].DataType = typeof(System.Decimal);
            //c1CPTs.Cols[COL_SpecialityDesc].DataType = typeof(System.String);

            //c1CPTs.SetData(0, COL_CPT_CODE, "CPT Code");
            //c1CPTs.SetData(0, COL_CPT_DESC, "Description");
            //c1CPTs.SetData(0, COL_CPT_CHARGES, "Fee");
            //c1CPTs.SetData(0, COL_MOD1_CODE, "M1");
            //c1CPTs.SetData(0, COL_MOD1_CHARGES, "Fee");
            //c1CPTs.SetData(0, COL_MOD2_CODE, "M2");
            //c1CPTs.SetData(0, COL_MOD2_CHARGES, "Fee");
            //c1CPTs.SetData(0, COL_MOD3_CODE, "M3");
            //c1CPTs.SetData(0, COL_MOD3_CHARGES, "Fee");
            //c1CPTs.SetData(0, COL_MOD4_CODE, "M4");
            //c1CPTs.SetData(0, COL_MOD4_CHARGES, "Fee");
            //c1CPTs.SetData(0, COL_SpecialityDesc, "Speciality");

            //c1CPTs.Cols[COL_CPT_CODE].Width = 100;
            //c1CPTs.Cols[COL_CPT_DESC].Width = 200;
            //c1CPTs.Cols[COL_CPT_CHARGES].Width = 100;
            //c1CPTs.Cols[COL_MOD1_CODE].Width = 100;
            //c1CPTs.Cols[COL_MOD1_CHARGES].Width = 100;
            //c1CPTs.Cols[COL_MOD2_CODE].Width = 100;
            //c1CPTs.Cols[COL_MOD2_CHARGES].Width = 100;
            //c1CPTs.Cols[COL_MOD3_CODE].Width = 100;
            //c1CPTs.Cols[COL_MOD3_CHARGES].Width = 100;
            //c1CPTs.Cols[COL_MOD4_CODE].Width = 100;
            //c1CPTs.Cols[COL_MOD4_CHARGES].Width = 100;
            //c1CPTs.Cols[COL_SpecialityDesc].Visible = false;
            //c1CPTs.Cols[COL_CPT_CODE].AllowEditing = true;
            //c1CPTs.Cols[COL_CPT_DESC].AllowEditing = false;

            //C1.Win.C1FlexGrid.CellStyle csCurrency = c1CPTs.Styles.Add("cs_Currency");
            //csCurrency.DataType = typeof(System.Decimal);
            //csCurrency.Format = "c";
            //csCurrency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            //c1CPTs.Cols[COL_CPT_CHARGES].Style = csCurrency;
            //c1CPTs.Cols[COL_MOD1_CHARGES].Style = csCurrency;
            //c1CPTs.Cols[COL_MOD2_CHARGES].Style = csCurrency;
            //c1CPTs.Cols[COL_MOD3_CHARGES].Style = csCurrency;
            //c1CPTs.Cols[COL_MOD4_CHARGES].Style = csCurrency;

            //if (c1CPTs != null && c1CPTs.Rows.Count > 1)
            //{
            //    c1CPTs.SetData(c1CPTs.Rows.Count - 1, COL_CPT_CHARGES, 0.00);
            //    c1CPTs.SetData(c1CPTs.Rows.Count - 1, COL_MOD1_CHARGES, 0.00);
            //    c1CPTs.SetData(c1CPTs.Rows.Count - 1, COL_MOD2_CHARGES, 0.00);
            //    c1CPTs.SetData(c1CPTs.Rows.Count - 1, COL_MOD3_CHARGES, 0.00);
            //    c1CPTs.SetData(c1CPTs.Rows.Count - 1, COL_MOD4_CHARGES, 0.00);
            //}


        }

        private void DesignGrid_C1View()
        {
            //c1_View.Clear(C1.Win.C1FlexGrid.ClearFlags.All);

            //c1_View.Cols.Count = COL_COUNT;
            //c1_View.Rows.Fixed = 1;
            //c1_View.Rows.Count = 2;

            //c1_View.Cols[COL_CPT_CODE].DataType = typeof(System.String);
            //c1_View.Cols[COL_CPT_DESC].DataType = typeof(System.String);
            //c1_View.Cols[COL_CPT_CHARGES].DataType = typeof(System.Decimal);
            //c1_View.Cols[COL_MOD1_CODE].DataType = typeof(System.String);
            //c1_View.Cols[COL_MOD1_CHARGES].DataType = typeof(System.Decimal);
            //c1_View.Cols[COL_MOD2_CODE].DataType = typeof(System.String);
            //c1_View.Cols[COL_MOD2_CHARGES].DataType = typeof(System.Decimal);
            //c1_View.Cols[COL_MOD3_CODE].DataType = typeof(System.String);
            //c1_View.Cols[COL_MOD3_CHARGES].DataType = typeof(System.Decimal);
            //c1_View.Cols[COL_MOD4_CODE].DataType = typeof(System.String);
            //c1_View.Cols[COL_MOD4_CHARGES].DataType = typeof(System.Decimal);
            //c1_View.Cols[COL_SpecialityDesc].DataType = typeof(System.String);

            //c1_View.SetData(0, COL_CPT_CODE, "CPT Code");
            //c1_View.SetData(0, COL_CPT_DESC, "Description");
            //c1_View.SetData(0, COL_CPT_CHARGES, "Fee");
            //c1_View.SetData(0, COL_MOD1_CODE, "M1");
            //c1_View.SetData(0, COL_MOD1_CHARGES, "Fee");
            //c1_View.SetData(0, COL_MOD2_CODE, "M2");
            //c1_View.SetData(0, COL_MOD2_CHARGES, "Fee");
            //c1_View.SetData(0, COL_MOD3_CODE, "M3");
            //c1_View.SetData(0, COL_MOD3_CHARGES, "Fee");
            //c1_View.SetData(0, COL_MOD4_CODE, "M4");
            //c1_View.SetData(0, COL_MOD4_CHARGES, "Fee");
            //c1_View.SetData(0, COL_SpecialityDesc, "Speciality");

            //c1_View.Cols[COL_CPT_CODE].Width = 100;
            //c1_View.Cols[COL_CPT_DESC].Width = 200;
            //c1_View.Cols[COL_CPT_CHARGES].Width = 100;
            //c1_View.Cols[COL_MOD1_CODE].Width = 100;
            //c1_View.Cols[COL_MOD1_CHARGES].Width = 100;
            //c1_View.Cols[COL_MOD2_CODE].Width = 100;
            //c1_View.Cols[COL_MOD2_CHARGES].Width = 100;
            //c1_View.Cols[COL_MOD3_CODE].Width = 100;
            //c1_View.Cols[COL_MOD3_CHARGES].Width = 100;
            //c1_View.Cols[COL_MOD4_CODE].Width = 100;
            //c1_View.Cols[COL_MOD4_CHARGES].Width = 100;
            //c1_View.Cols[COL_SpecialityDesc].Visible = false;
            //c1_View.Cols[COL_CPT_CODE].AllowEditing = true;
            //c1_View.Cols[COL_CPT_DESC].AllowEditing = false;
            //c1_View.AllowEditing = false;

            //C1.Win.C1FlexGrid.CellStyle csCurrency = c1_View.Styles.Add("cs_Currency");
            //csCurrency.DataType = typeof(System.Decimal);
            //csCurrency.Format = "c";
            //csCurrency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            //c1_View.Cols[COL_CPT_CHARGES].Style = csCurrency;
            //c1_View.Cols[COL_MOD1_CHARGES].Style = csCurrency;
            //c1_View.Cols[COL_MOD2_CHARGES].Style = csCurrency;
            //c1_View.Cols[COL_MOD3_CHARGES].Style = csCurrency;
            //c1_View.Cols[COL_MOD4_CHARGES].Style = csCurrency;

            //if (c1_View != null && c1_View.Rows.Count > 1)
            //{
            //    c1_View.SetData(c1_View.Rows.Count - 1, COL_CPT_CHARGES, 0.00);
            //    c1_View.SetData(c1_View.Rows.Count - 1, COL_MOD1_CHARGES, 0.00);
            //    c1_View.SetData(c1_View.Rows.Count - 1, COL_MOD2_CHARGES, 0.00);
            //    c1_View.SetData(c1_View.Rows.Count - 1, COL_MOD3_CHARGES, 0.00);
            //    c1_View.SetData(c1_View.Rows.Count - 1, COL_MOD4_CHARGES, 0.00);
            //}


        }
        #endregion

        #region " Tool Strip Button Click "

        private void ts_btnFeeSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                //_IsAddClicked = false;
                ts_btnSave.Visible = false;
                ts_btnClose.Visible = false;
                ts_btnFeeSchedule.Visible = false;
                //ts_btnSave_FeeSchedule.Visible = true;
                ts_btnClose_FeeSchedule.Visible = true;
                //ts_btnAddLine.Visible = true;

                //ts_btnImportFeeSchedule.Visible = true;


                ts_btnAddLine.Visible = false;
                //ts_btnRemoveLine.Visible = false;
                ts_btnSave_FeeSchedule.Visible = false;
                ts_btnImportFeeSchedule.Visible = false;
                ts_btnAdd_FeeSchedule.Visible = true;

                //pnlAdd.SendToBack();
                //pnl_View.BringToFront();


                //pnlFeeSchedule.BringToFront();
                pnl_Base.SendToBack();
                pnlTopToolStrip.SendToBack();

                //cmb_StdFeeSchedule.Visible = false;
                //lbl_StdFeeSchedule.Visible = false;
                //FillFacilities();
                //GetSchedules();
                DesignGrid_C1View();
                //GetSchedules_new();
                //Set_ViewFeeschedulecontrols();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void ts_btnAdd_FeeSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsWorking == false)
                {
                    DesignGrid();
                }
                //FillStandardFeeScheduleType();

                //pnlAdd.BringToFront();
                //pnl_View.SendToBack();
                ts_btnAddLine.Visible = true;
                ts_btnSave_FeeSchedule.Visible = true;
                ts_btnImportFeeSchedule.Visible = true;
                ts_btnAdd_FeeSchedule.Visible = false;
                //cmb_StdFeeSchedule.Visible = true;
                //lbl_StdFeeSchedule.Visible = true;
                //_IsAddClicked = true;
                //Set_ViewFeeschedulecontrols();

                //if (c1CPTs.Rows.Count > 1)
                //{
                //    ts_btnRemoveLine.Visible = true; //
                //}


            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ts_btnSave_FeeSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFeeSchedule() == true)
                {
                    ts_btnSave.Visible = true;
                    ts_btnClose.Visible = true;
                    ts_btnFeeSchedule.Visible = true;
                    ts_btnSave_FeeSchedule.Visible = false;
                    ts_btnClose_FeeSchedule.Visible = false;
                    ts_btnAddLine.Visible = false;
                    ts_btnRemoveLine.Visible = false;
                    ts_btnRemoveAll.Visible = false;
                    //txt_Search.Visible = false;
                    //lblSearch.Visible = false;
                    ts_btnImportFeeSchedule.Visible = false;
                    //pnlFeeSchedule.SendToBack();
                    pnl_Base.BringToFront();
                    pnlTopToolStrip.SendToBack();

                    //if (c1CPTs.Rows.Count > 1)
                    //{
                    //    c1CPTs.SetData(c1CPTs.RowSel, COL_SpecialityDesc, cmbFacility.SelectedValue);
                    //}
                    _IsWorking = true;

                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void tls_btnAddLine_Click(object sender, EventArgs e)
        {
            //AddLine();
        }

        private void tls_btnRemoveLine_Click(object sender, EventArgs e)
        {
            ////    if (_IsAddClicked == true)
            ////    {
            ////        if (c1CPTs != null && c1CPTs.Rows.Count > 1)
            ////        {
            ////            c1CPTs.Rows.Remove(c1CPTs.RowSel);
            ////            if (c1CPTs.Rows.Count == 1)
            ////            {
            ////                ts_btnRemoveLine.Visible = false;
            ////            }

            ////            //    DesignGrid();
            ////        }
            ////        else
            ////        {
            ////            ts_btnRemoveLine.Visible = false;
            ////        }

            ////    }
            ////    if (_IsAddClicked == false)
            ////    {
            ////        if (c1_View != null && c1_View.Rows.Count > 1)
            ////        {
            ////            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ////            try
            ////            {
            ////                oDB.Connect(false);
            ////                string _sqlQuery = "DELETE FROM BL_ClinicFeeSchedule WHERE   HCPCSMOD = '" + Convert.ToString(c1_View.GetData(c1_View.RowSel, COL_CPT_CODE)) + "' AND SPEC = '" + Convert.ToString(c1_View.GetData(c1_View.RowSel, COL_SpecialityDesc)) + "' AND InsuranceId = " + _ContactID + " ";
            ////                int retVal = oDB.Execute_Query(_sqlQuery);

            ////                if (retVal > 0)
            ////                    c1_View.Rows.Remove(c1_View.RowSel);

            ////                oDB.Disconnect();
            ////            }
            ////            catch (Exception ex)
            ////            {
            ////                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            ////            }
            ////            finally
            ////            {
            ////                if (oDB != null) { oDB.Dispose(); }
            ////            }
            ////        }
            ////    }

        }

        private void ts_btnRemoveAll_Click(object sender, EventArgs e)
        {
            //    if (_IsAddClicked == false)
            //    {
            //        if (c1_View != null && c1_View.Rows.Count > 1)
            //        {
            //            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //            try
            //            {
            //                oDB.Connect(false);
            //                string _sqlQuery = "DELETE FROM BL_ClinicFeeSchedule WHERE InsuranceId = " + _ContactID + " ";
            //                int retVal = oDB.Execute_Query(_sqlQuery);

            //                if (retVal > 0)
            //                {
            //                    ts_btnFeeSchedule_Click(null, null);
            //                }
            //                oDB.Disconnect();

            //            }
            //            catch (Exception ex)
            //            {
            //                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //            }
            //            finally
            //            {
            //                if (oDB != null) { oDB.Dispose(); }
            //            }
            //        }
            //    }
        }

        private void ts_btnClose_FeeSchedule_Click(object sender, EventArgs e)
        {
            //    try
            //    {
            //        if (_IsAddClicked == false)
            //        {
            //            ts_btnSave.Visible = true;
            //            ts_btnClose.Visible = true;
            //            ts_btnFeeSchedule.Visible = true;
            //            ts_btnSave_FeeSchedule.Visible = false;
            //            ts_btnClose_FeeSchedule.Visible = false;
            //            ts_btnAddLine.Visible = false;
            //            ts_btnRemoveLine.Visible = false;
            //            ts_btnRemoveAll.Visible = false;
            //            txt_Search.Visible = false;
            //            lblSearch.Visible = false;
            //            ts_btnImportFeeSchedule.Visible = false;
            //            pnlFeeSchedule.SendToBack();
            //            pnl_Base.BringToFront();
            //            pnlTopToolStrip.SendToBack();
            //            ts_btnAdd_FeeSchedule.Visible = false;
            //            cmb_StdFeeSchedule.Visible = false;
            //            lbl_StdFeeSchedule.Visible = false;
            //        }
            //        if (_IsAddClicked == true)
            //        {
            //            _IsAddClicked = false;

            //            pnlAdd.SendToBack();
            //            pnl_View.BringToFront();
            //            ts_btnAddLine.Visible = false;
            //            //ts_btnRemoveLine.Visible = false;
            //            Set_ViewFeeschedulecontrols();
            //            ts_btnSave_FeeSchedule.Visible = false;
            //            ts_btnImportFeeSchedule.Visible = false;
            //            ts_btnAdd_FeeSchedule.Visible = true;
            //            cmb_StdFeeSchedule.Visible = false;
            //            lbl_StdFeeSchedule.Visible = false;

            //            //GetSchedules_new();
            //            _IsWorking = true;

            //        }



            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //   }






        }

        private void ts_btnImportFeeSchedule_Click(object sender, EventArgs e)
        {
            //    if (c1CPTs.Rows.Count > 1 && c1CPTs.GetData(1, COL_CPT_CODE) != null)
            //    {
            //        if (MessageBox.Show("Existing records  will be overwritten by this import.  Do you want to continue ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //        {
            //            ImportSchedules();
            //            // cmb_StdFeeSchedule.SelectedIndex = -1;
            //        }

            //    }
            //    else
            //    {
            //        ImportSchedules();
            //    }

        }

        #endregion

        #region " List Control Methods & Events "

        //private void LoadList(int rowIndex, int colIndex, gloListControl.gloListControlType oType)
        //{
        //    int _x = 0;
        //    try
        //    {
        //        if (oListControl != null)
        //        {
        //            for (int i = pnlListControl.Controls.Count - 1; i >= 0; i--)
        //            {
        //                if (pnlListControl.Controls[i].Name == oListControl.Name)
        //                {
        //                    pnlListControl.Controls.Remove(pnlListControl.Controls[i]);
        //                    break;
        //                }
        //            }
        //        }

        //        c1CPTs.Controls.Add(pnlListControl);

        //        if (oType == gloListControl.gloListControlType.CPT)
        //        {
        //            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.CPT, false, this.Width);
        //            oListControl.ControlHeader = "CPT";
        //            _CurrentControlType = gloListControl.gloListControlType.CPT;
        //            _x = c1CPTs.Cols[colIndex].Left + 5;
        //        }
        //        else if (oType == gloListControl.gloListControlType.Modifier)
        //        {
        //            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Modifier, false, this.Width);
        //            oListControl.ControlHeader = "Modifiers";
        //            _CurrentControlType = gloListControl.gloListControlType.Modifier;
        //            _x = c1CPTs.Cols[COL_CPT_DESC].Left + 70;
        //        }
        //        oListControl.ClinicID = _ClinicID;
        //        oListControl.HideToolStrip = true;
        //        oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
        //        oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
        //        pnlListControl.Controls.Add(oListControl);
        //        pnlListControl.Parent = this;

        //        int _y = c1CPTs.Rows[rowIndex].Bottom + pnlTopToolStrip.Height + pnlSpeciality.Height + c1CPTs.Top;
        //        pnlListControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
        //        pnlListControl.Visible = true;
        //        pnlListControl.BringToFront();

        //        oListControl.OpenControl();
        //        oListControl.Dock = DockStyle.Fill;
        //        oListControl.BringToFront();

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    { }
        //}

        private void UnLoadList()
        {
           // if (oListControl != null)
            {
                ////for (int i = pnlListControl.Controls.Count - 1; i >= 0; i--)
                ////{
                ////    if (pnlListControl.Controls[i].Name == oListControl.Name)
                ////    {
                ////        pnlListControl.Controls.Remove(pnlListControl.Controls[i]);
                ////        break;
                ////    }
                ////}
            }
            //pnlListControl.Visible = false;
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            UnLoadList();
        }

        //void oListControl_ItemSelectedClick(object sender, EventArgs e)
        //{
        //    int _Counter = 0;

        //    try
        //    {
        //        switch (_CurrentControlType)
        //        {
        //            case gloListControl.gloListControlType.Other:
        //                break;
        //            case gloListControl.gloListControlType.CPT:
        //                {
        //                    if (oListControl.SelectedItems.Count > 0)
        //                    {
        //                        for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
        //                        {
        //                            c1CPTs.SetData(c1CPTs.RowSel, COL_CPT_CODE, oListControl.SelectedItems[_Counter].Code.ToString());
        //                            c1CPTs.SetData(c1CPTs.RowSel, COL_CPT_DESC, oListControl.SelectedItems[_Counter].Description.ToString());
        //                            c1CPTs.Focus();
        //                            c1CPTs.Select();
        //                            c1CPTs.Select(c1CPTs.RowSel, COL_CPT_CHARGES, true);

        //                        }
        //                    }
        //                }
        //                break;
        //            case gloListControl.gloListControlType.Modifier:
        //                {
        //                    if (oListControl.SelectedItems.Count > 0)
        //                    {
        //                        for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
        //                        {
        //                            c1CPTs.SetData(c1CPTs.RowSel, c1CPTs.ColSel, oListControl.SelectedItems[_Counter].Code.ToString());
        //                            c1CPTs.Focus();
        //                            c1CPTs.Select();
        //                            c1CPTs.Select(c1CPTs.RowSel, c1CPTs.ColSel + 1, true);
        //                            //c1CPTs.SetData(c1CPTs.RowSel, COL_MOD_DESC, oListControl.SelectedItems[_Counter].Description.ToString());
        //                        }
        //                    }
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        UnLoadList();
        //    }
        //}

        #endregion

        #region " c1CPTs Grid Events "

        //private void c1CPTs_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        //{
        //    try
        //    {

        //        if (e.Col == COL_CPT_CODE)
        //        {
        //            e.Cancel = true;
        //            LoadList(e.Row, e.Col, gloListControl.gloListControlType.CPT);
        //        }
        //        else if (e.Col == COL_MOD1_CODE || e.Col == COL_MOD2_CODE || e.Col == COL_MOD3_CODE || e.Col == COL_MOD4_CODE)
        //        {
        //            e.Cancel = true;
        //            LoadList(e.Row, e.Col, gloListControl.gloListControlType.Modifier);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //}

        //private void c1CPTs_KeyUp(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && (c1CPTs.ColSel == COL_MOD4_CHARGES))// &&
        //        //Convert.ToString(c1CPTs.GetData(c1CPTs.RowSel, COL_MOD4_CHARGES)) != "" && 
        //        //Convert.ToDecimal(c1CPTs.GetData(c1CPTs.RowSel, COL_CHARGES)) > 0)
        //        {
        //            AddLine();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        ex = null;
        //        throw;
        //    }
        //}

        private void c1CPTs_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            UnLoadList();
        }

        //private void c1CPTs_Click(object sender, EventArgs e)
        //{
        //    if (c1CPTs.Row == -1)
        //    {
        //        return;
        //    }
        //    if (c1CPTs.Rows.Count > 1)
        //    {
        //        //if (c1CPTs.GetData(c1CPTs.RowSel, COL_SpecialityDesc) != null)
        //        //    cmbFacility.SelectedValue = c1CPTs.GetData(c1CPTs.RowSel, COL_SpecialityDesc);
        //    }
        //}

        #endregion

        #region " Public & Private Methods "

        #region "Mid Level Settings Methods"

        private void FillMidLevelSettingsAllProviderCombo()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMidLevelSettings = null;
            try
            {

                dtMidLevelSettings = GetSettingValues();

                if (dtMidLevelSettings != null && dtMidLevelSettings.Rows.Count > 0)
                {
                    DataRow dr = dtMidLevelSettings.NewRow();
                    dr["nSettingsID"] = 0;
                    dr["sSettingsName"] = "";
                    dtMidLevelSettings.Rows.InsertAt(dr, 0);
                    dtMidLevelSettings.AcceptChanges();

                    cmbMidLevelSpeProvider.DataSource = dtMidLevelSettings;
                    cmbMidLevelSpeProvider.DisplayMember = "sSettingsName";
                    cmbMidLevelSpeProvider.ValueMember = "nSettingsID";
                    cmbMidLevelSpeProvider.Refresh();

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
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }


        }

        private void FillBillingTaxonomyQualifierCombo()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            GeneralSettings oSettings = new GeneralSettings(_databaseconnectionstring);
            DataTable dtQualifier = null;
            try
            {

                dtQualifier = oSettings.GetEnumItems(BillingTaxonomyQualifierItems.ZZ, false);

                if (dtQualifier != null && dtQualifier.Rows.Count > 0)
                {
                    cmbQualifier.DataSource = dtQualifier;
                    cmbQualifier.ValueMember = "nID";
                    cmbQualifier.DisplayMember = "sDescription";
                    cmbQualifier.Refresh();
                    cmbQualifier.SelectedIndex = -1;
                    cmbQualifier.SelectedIndex = 0;
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
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oSettings.Dispose();
                oSettings = null;
            }

        }

        private DataTable GetSettingValues()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMidLevelSettings = null;
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(nSettingsID,0) as nSettingsID,ISNULL(sMidLevelBillingType,'') as sSettingsName from BL_MidLevelSettings_MST";
                oDB.Retrive_Query(_sqlQuery, out dtMidLevelSettings);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }
            return dtMidLevelSettings;

        }

        private void FillMidLevelData(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMidLevelSpecificPlan = null;
            DataTable dtProviderSettings = null;
            //string sComboString = string.Empty;
            string _sqlQuery = string.Empty;
            int iRow = 0;
            try
            {

                oDB.Connect(false);

                _sqlQuery = " SELECT BL_MidLevel_Settings.nProviderID,BL_MidLevelSettings_MST.sMidLevelBillingType from  BL_MidLevel_Settings,BL_MidLevelSettings_MST " +
                                " WHERE" +
                                " BL_MidLevel_Settings.nSettingsID = BL_MidLevelSettings_MST.nSettingsID" +
                                " AND nContactID = " + nContactID + "AND nBillingSettingType = " + (int)gloPMContacts.gloContacts.MidLevelSettingsType.SpecificPlanSpecificProvider;


                oDB.Retrive_Query(_sqlQuery, out dtProviderSettings);

                if (dtProviderSettings != null && dtProviderSettings.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtProviderSettings.Rows)
                    {
                        iRow = c1MidlevelSettings.FindRow(Convert.ToString(dr["nProviderID"]), 0, (int)MidLevelGridColumn.ProviderID, true);
                        //c1MidlevelSettings.FindRow(Convert.ToString(dr["nProviderID"]),
                        if (iRow > 0)
                        {
                            c1MidlevelSettings.SetData(iRow, (int)MidLevelGridColumn.SettingsName, Convert.ToString(dr["sMidLevelBillingType"]));
                        }

                    }

                }

                _sqlQuery = " SELECT BL_MidLevel_Settings.nProviderID,BL_MidLevel_Settings.nSettingsID,BL_MidLevelSettings_MST.sMidLevelBillingType from  BL_MidLevel_Settings,BL_MidLevelSettings_MST " +
                               " WHERE" +
                               " BL_MidLevel_Settings.nSettingsID = BL_MidLevelSettings_MST.nSettingsID" +
                               " AND nContactID = " + nContactID + "AND nBillingSettingType = " + (int)gloPMContacts.gloContacts.MidLevelSettingsType.SpecificPlan;


                oDB.Retrive_Query(_sqlQuery, out dtMidLevelSpecificPlan);
                if (dtMidLevelSpecificPlan.Rows.Count > 0)
                {
                    cmbMidLevelSpeProvider.SelectedValue = dtMidLevelSpecificPlan.Rows[0]["nSettingsID"];
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
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (dtMidLevelSpecificPlan != null) { dtMidLevelSpecificPlan.Dispose(); dtMidLevelSpecificPlan = null; }
                if (dtProviderSettings != null) { dtProviderSettings.Dispose(); dtProviderSettings = null; }
                _sqlQuery = null;
            }


        }

        private void FillBillingTaxonomyData(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtBillingTaxonomy = null;
            //string sComboString = string.Empty;
            string _sqlQuery = string.Empty;
            int iRow = 0;
            try
            {

                oDB.Connect(false);

                _sqlQuery = "  SELECT nProviderID,sProviderTaxonomy,sTaxonomyDesc FROM BL_TaxonomyCodes,Specialty_MST WHERE BL_TaxonomyCodes.sProviderTaxonomy = Specialty_MST.sTaxonomyCode AND nContactID =  " + nContactID;


                oDB.Retrive_Query(_sqlQuery, out dtBillingTaxonomy);

                if (dtBillingTaxonomy != null && dtBillingTaxonomy.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtBillingTaxonomy.Rows)
                    {
                        iRow = c1BillingTaxonomy.FindRow(Convert.ToString(dr["nProviderID"]), 0, (int)BillingTaxonomy.ProviderID, true);
                        //c1MidlevelSettings.FindRow(Convert.ToString(dr["nProviderID"]),
                        if (iRow > 0)
                        {
                            c1BillingTaxonomy.SetData(iRow, (int)BillingTaxonomy.PlanOverride, Convert.ToString(dr["sProviderTaxonomy"]));
                            c1BillingTaxonomy.SetData(iRow, (int)BillingTaxonomy.PlanOverrideDesc, Convert.ToString(dr["sTaxonomyDesc"]));
                        }

                    }

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
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (dtBillingTaxonomy != null) { dtBillingTaxonomy.Dispose(); dtBillingTaxonomy = null; }
                _sqlQuery = null;
            }


        }

        private void FillMidLevelGrid()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMidLevelSettings = null;
            DataTable dtProvider = null;
            string sComboString = string.Empty;
            string _sqlQuery = string.Empty;

            try
            {
                htMidLevelSettings = new Hashtable();
                dtMidLevelSettings = GetSettingValues();
                if (dtMidLevelSettings != null)
                {
                    for (int iCount = 0; iCount <= dtMidLevelSettings.Rows.Count - 1; iCount++)
                    {
                        htMidLevelSettings.Add(Convert.ToString(dtMidLevelSettings.Rows[iCount]["sSettingsName"]), Convert.ToString(dtMidLevelSettings.Rows[iCount]["nSettingsID"]));

                        if (sComboString != string.Empty)
                        {
                            sComboString += "||" + Convert.ToString(dtMidLevelSettings.Rows[iCount]["sSettingsName"]);
                        }
                        else
                        {
                            sComboString = " " + "||" + Convert.ToString(dtMidLevelSettings.Rows[iCount]["sSettingsName"]);
                        }
                    }
                }

                oDB.Connect(false);
                _sqlQuery = "SELECT PM.nProviderID, PM.sFirstName + ' '+ PM.sMiddleName + ' '+ PM.sLastName AS sName,PM.nProviderType, PTM.sProviderType FROM Provider_MST PM,ProviderType_MST PTM WHERE  PM.nProviderType = PTM.nProviderTypeID ORDER BY sName";
                oDB.Retrive_Query(_sqlQuery, out dtProvider);

                if (dtProvider != null && dtProvider.Rows.Count > 0)
                {
                    c1MidlevelSettings.Rows.Count = 1;
                    foreach (DataRow dr in dtProvider.Rows)
                    {
                        c1MidlevelSettings.Rows.Add();
                        c1MidlevelSettings.SetData(c1MidlevelSettings.Rows.Count - 1, (int)MidLevelGridColumn.ProviderID, Convert.ToString(dr["nProviderID"]));
                        c1MidlevelSettings.SetData(c1MidlevelSettings.Rows.Count - 1, (int)MidLevelGridColumn.ProviderName, Convert.ToString(dr["sName"]));
                        c1MidlevelSettings.SetData(c1MidlevelSettings.Rows.Count - 1, (int)MidLevelGridColumn.ProviderTypeID, Convert.ToString(dr["nProviderType"]));
                        c1MidlevelSettings.SetData(c1MidlevelSettings.Rows.Count - 1, (int)MidLevelGridColumn.ProviderType, Convert.ToString(dr["sProviderType"]));

                    }

                    c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsName].ComboList = sComboString;
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
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (dtMidLevelSettings != null) { dtMidLevelSettings.Dispose(); dtMidLevelSettings = null; }
                if (dtProvider != null) { dtProvider.Dispose(); dtProvider = null; }
                sComboString = null;
                _sqlQuery = null;
            }


        }


        private void DesignMidLevelGrid()
        {

            try
            {


                c1MidlevelSettings.Cols.Count = 6;
                c1MidlevelSettings.Rows.Count = 1;
                c1MidlevelSettings.Rows.Fixed = 1;

                c1MidlevelSettings.Rows.Add();
                c1MidlevelSettings.SetData(0, (int)MidLevelGridColumn.ProviderID, "ProviderID");
                c1MidlevelSettings.SetData(0, (int)MidLevelGridColumn.ProviderName, "Provider");
                c1MidlevelSettings.SetData(0, (int)MidLevelGridColumn.ProviderTypeID, "TypeID");
                c1MidlevelSettings.SetData(0, (int)MidLevelGridColumn.ProviderType, "Type");
                c1MidlevelSettings.SetData(0, (int)MidLevelGridColumn.SettingsID, "SettingsID");
                c1MidlevelSettings.SetData(0, (int)MidLevelGridColumn.SettingsName, "Report Rendering");


                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderID].AllowEditing = false;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderName].AllowEditing = false;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderTypeID].AllowEditing = false;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderType].AllowEditing = false;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsID].AllowEditing = false;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsName].AllowEditing = true;

                //c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderID].AllowSorting = false;
                //c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderName].AllowSorting = false;
                //c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderTypeID].AllowSorting = false;
                //c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderType].AllowSorting = false;
                //c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsID].AllowSorting = false;
                //c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsName].AllowSorting = false;

                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderTypeID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderID].Width = 100;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderName].Width = 300;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderTypeID].Width = 100;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderType].Width = 100;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsID].Width = 100;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsName].Width = 150;

                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderID].Visible = false;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.SettingsID].Visible = false;
                c1MidlevelSettings.Cols[(int)MidLevelGridColumn.ProviderTypeID].Visible = false;

                c1MidlevelSettings.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

                c1MidlevelSettings.ExtendLastCol = true;
                c1MidlevelSettings.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                c1MidlevelSettings.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private void DesignBillingTaxonomyGrid()
        {

            try
            {


                c1BillingTaxonomy.Cols.Count = 6;
                c1BillingTaxonomy.Rows.Count = 1;
                c1BillingTaxonomy.Rows.Fixed = 1;

                c1BillingTaxonomy.Rows.Add();
                c1BillingTaxonomy.SetData(0, (int)BillingTaxonomy.ProviderID, "ProviderID");
                c1BillingTaxonomy.SetData(0, (int)BillingTaxonomy.ProviderName, "Provider");
                c1BillingTaxonomy.SetData(0, (int)BillingTaxonomy.DefaultTaxonomyDesc, "Default Taxonomy Desc");
                c1BillingTaxonomy.SetData(0, (int)BillingTaxonomy.DefaultTaxonomy, "Default Taxonomy");
                c1BillingTaxonomy.SetData(0, (int)BillingTaxonomy.PlanOverrideDesc, "Plan Override Desc");
                c1BillingTaxonomy.SetData(0, (int)BillingTaxonomy.PlanOverride, "Plan Override");


                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.ProviderID].AllowEditing = false;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.ProviderName].AllowEditing = false;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.DefaultTaxonomyDesc].AllowEditing = false;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.DefaultTaxonomy].AllowEditing = false;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.PlanOverrideDesc].AllowEditing = false;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.PlanOverride].AllowEditing = true;

                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.ProviderID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.ProviderName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.DefaultTaxonomyDesc].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.DefaultTaxonomy].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.PlanOverrideDesc].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.PlanOverride].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.ProviderID].Width = 100;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.ProviderName].Width = 200;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.DefaultTaxonomyDesc].Width = 125;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.DefaultTaxonomy].Width = 125;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.PlanOverrideDesc].Width = 100;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.PlanOverride].Width = 378;

                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.ProviderID].Visible = false;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.DefaultTaxonomyDesc].Visible = false;
                c1BillingTaxonomy.Cols[(int)BillingTaxonomy.PlanOverrideDesc].Visible = false;

                c1BillingTaxonomy.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

                c1BillingTaxonomy.ExtendLastCol = true;
                c1BillingTaxonomy.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                c1BillingTaxonomy.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }



        #endregion "Mid Level Settings Methods"

        //private void FillStandardFeeScheduleType()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    DataTable dtStdFeeSchedule = new DataTable();
        //    string strQuery = "";
        //    cmb_StdFeeSchedule.DataSource = null;

        //    try
        //    {
        //        oDB.Connect(false);


        //        strQuery = "SELECT  DISTINCT  nStdFeeScheduleId ,sStdFeeScheduleType FROM BL_StandardFeeSchedule ";


        //        oDB.Retrive_Query(strQuery, out dtStdFeeSchedule);

        //        if (dtStdFeeSchedule != null && dtStdFeeSchedule.Rows.Count > 0)
        //        {
        //            DataRow dr = dtStdFeeSchedule.NewRow();
        //            dr[0] = "0";
        //            dtStdFeeSchedule.Rows.InsertAt(dr, 0);
        //            dtStdFeeSchedule.AcceptChanges();
        //            cmb_StdFeeSchedule.DataSource = dtStdFeeSchedule;
        //            cmb_StdFeeSchedule.DisplayMember = dtStdFeeSchedule.Columns["sStdFeeScheduleType"].ColumnName;
        //            cmb_StdFeeSchedule.ValueMember = dtStdFeeSchedule.Columns["nStdFeeScheduleId"].ColumnName;
        //        }

        //        oDB.Disconnect();

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
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

        //private void SaveFeeSchedule_Old(Int64 InsuranceId)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string _sqlQuery = "";
        //    string _CptCode = "";
        //    decimal _CptCharges = 0;
        //    string _SpecialityCode = "";
        //    //bool _result = false;
        //    try
        //    {
        //        oDB.Connect(false);
        //        //_CptCode = c1CPTs.GetData()
        //        // _sqlQuery = "DELETE FROM BL_ClinicFeeSchedule WHERE InsuranceId = " + InsuranceId + "  AND SPEC = '" + Convert.ToString(cmbFacility.SelectedValue) + "' ";

        //        //int retVal = oDB.Execute_Query(_sqlQuery);
        //        //_sqlQuery = "";
        //        for (int i = 1; i <= c1CPTs.Rows.Count - 1; i++)
        //        {
        //            //string _CptCode = "";
        //            //decimal _CptCharges = 0;
        //            //string _SpecialityCode = "";

        //            if (c1CPTs.GetData(i, COL_CPT_CODE) != null && Convert.ToString(c1CPTs.GetData(i, COL_CPT_CODE)) != "")
        //            {

        //                int retVal = 0;

        //                //individual Speciality for every C1 row
        //                if (c1CPTs.GetData(i, COL_SpecialityDesc) != null && Convert.ToString(c1CPTs.GetData(i, COL_SpecialityDesc)) != "")
        //                {
        //                    _SpecialityCode = Convert.ToString(c1CPTs.GetData(i, COL_SpecialityDesc));
        //                }

        //                //_sqlQuery = "DELETE FROM BL_ClinicFeeSchedule WHERE InsuranceId = " + InsuranceId + "  AND SPEC = '" + Convert.ToString(_SpecialityCode) + "' ";

        //                //int retVal = oDB.Execute_Query(_sqlQuery);
        //                //_sqlQuery = "";

        //                _CptCode = Convert.ToString(c1CPTs.GetData(i, COL_CPT_CODE));
        //                if (c1CPTs.GetData(i, COL_CPT_CHARGES) != null && Convert.ToString(c1CPTs.GetData(i, COL_CPT_CHARGES)) != "")
        //                {
        //                    _CptCharges = Convert.ToDecimal(c1CPTs.GetData(i, COL_CPT_CHARGES).ToString());
        //                }

        //                if (_CptCharges > 0)
        //                {
        //                    _sqlQuery = "DELETE FROM BL_ClinicFeeSchedule WHERE InsuranceId = " + InsuranceId + " AND  HCPCSMOD = '" + _CptCode + "'  AND SPEC = '" + Convert.ToString(_SpecialityCode) + "' ";
        //                    retVal = oDB.Execute_Query(_sqlQuery);
        //                    _sqlQuery = "";


        //                    _sqlQuery = "DELETE FROM BL_ClinicFeeSchedule " +
        //                    " WHERE HCPCSMOD = '" + _CptCode + "' AND SPEC = '" + _SpecialityCode + "' AND (MOD IS NULL OR MOD = '') AND InsuranceId = " + InsuranceId + " ";

        //                    retVal = oDB.Execute_Query(_sqlQuery);
        //                    _sqlQuery = "";

        //                    _sqlQuery = " INSERT INTO BL_ClinicFeeSchedule " +
        //                    " (HCPCSMOD, SPEC, HCPCS, ALCHG,InsuranceId) " +
        //                    " VALUES " +
        //                    " ('" + _CptCode + "','" + _SpecialityCode + "','" + _CptCode + "'," + _CptCharges + ",'" + InsuranceId + "')";

        //                    retVal = oDB.Execute_Query(_sqlQuery);
        //                    //if (retVal > 0)
        //                    //{
        //                    //    _result = true;
        //                    //}
        //                }

        //                for (int j = COL_MOD1_CODE; j <= COL_MOD4_CODE; j += 2)
        //                {
        //                    string _ModCode = "";
        //                    decimal _ModCharges = 0;
        //                    if (c1CPTs.GetData(i, j) != null && Convert.ToString(c1CPTs.GetData(i, j)) != "")
        //                    {
        //                        _ModCode = Convert.ToString(c1CPTs.GetData(i, j));

        //                        if (c1CPTs.GetData(i, j + 1) != null && Convert.ToString(c1CPTs.GetData(i, j + 1)) != "")
        //                        {
        //                            _ModCharges = Convert.ToDecimal(Convert.ToString(c1CPTs.GetData(i, j + 1)));
        //                        }
        //                    }
        //                    if (_ModCharges > 0)
        //                    {

        //                        _sqlQuery = "DELETE FROM BL_ClinicFeeSchedule " +
        //                        " WHERE HCPCSMOD = '" + _CptCode + "' AND SPEC = '" + _SpecialityCode + "' " +
        //                        " AND HCPCS = '" + _CptCode + "' AND MOD = '" + _ModCode + "' AND InsuranceId = " + InsuranceId + " ";
        //                        retVal = oDB.Execute_Query(_sqlQuery);
        //                        _sqlQuery = "";

        //                        _sqlQuery = " INSERT INTO BL_ClinicFeeSchedule " +
        //                        " (HCPCSMOD, SPEC, HCPCS, MOD, ALCHG, InsuranceId) " +
        //                        " VALUES " +
        //                        " ('" + _CptCode + "','" + _SpecialityCode + "','" + _CptCode + "','" + _ModCode + "'," + _ModCharges + ",'" + InsuranceId + "')";

        //                        retVal = oDB.Execute_Query(_sqlQuery);
        //                    }
        //                }
        //                //if (retVal > 0)
        //                //{
        //                //    _result = true;
        //                //}
        //            }


        //        }
        //        oDB.Disconnect();
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //    //return _result;
        //}

        //private void AddLine()
        //{
        //    try
        //    {
        //        if (c1CPTs.Rows.Count > 1)
        //        {
        //            c1CPTs.SetData(c1CPTs.RowSel, COL_SpecialityDesc, cmbFacility.SelectedValue);
        //            ts_btnRemoveLine.Visible = true;

        //        }
        //        if (c1CPTs != null)
        //        {
        //            c1CPTs.Rows.Add();
        //            int rowIndex = c1CPTs.Rows.Count - 1;
        //            SetCurrencyCellValue(rowIndex);
        //            c1CPTs.Select(rowIndex, COL_CPT_CODE, true);
        //            //To add Speciality to the Schedule Line  

        //            if (c1CPTs.Rows.Count == 2)
        //            {
        //                ts_btnRemoveLine.Visible = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        //private void SetCurrencyCellValue(int rowIndex)
        //{
        //    try
        //    {
        //        if (c1CPTs != null && c1CPTs.Rows.Count > 0)
        //        {
        //            if (rowIndex > 0 && rowIndex < c1CPTs.Rows.Count)
        //            {
        //                c1CPTs.SetData(rowIndex, COL_CPT_CHARGES, 0.00);
        //                c1CPTs.SetData(rowIndex, COL_MOD1_CHARGES, 0.00);
        //                c1CPTs.SetData(rowIndex, COL_MOD2_CHARGES, 0.00);
        //                c1CPTs.SetData(rowIndex, COL_MOD3_CHARGES, 0.00);
        //                c1CPTs.SetData(rowIndex, COL_MOD4_CHARGES, 0.00);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        private bool ValidateFeeSchedule()
        {
            bool _ReturnValue = true;
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { }
            return _ReturnValue;
        }

        ////public void FillFacilities()
        ////{

        ////    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        ////    DataTable dtFacility = new DataTable();
        ////    string strQuery = "";
        ////    cmbFacility.DataSource = null;

        ////    try
        ////    {
        ////        oDB.Connect(false);
        ////        //Select the fields for given FaciltyID

        ////        if (_ClinicID == 0)
        ////        {
        ////            strQuery = "SELECT sCode,(sCode + ' - ' + sDescription) AS SpecCodeName FROM Specialty_MST where " +
        ////                " sCode IS NOT NULL AND sDescription IS NOT NULL AND bIsBlocked = 0 ";
        ////        }
        ////        else
        ////        {
        ////            strQuery = "SELECT sCode,(sCode + ' - ' + sDescription) AS SpecCodeName FROM Specialty_MST where " +
        ////                " sCode IS NOT NULL AND sDescription IS NOT NULL AND bIsBlocked = 0 AND nClinicID = " + _ClinicID + "";
        ////        }

        ////        oDB.Retrive_Query(strQuery, out dtFacility);

        ////        if (dtFacility != null && dtFacility.Rows.Count > 0)
        ////        {
        ////            //DataRow dr = dtFacility.NewRow();
        ////            //dr[0] = "0";
        ////            //dtFacility.Rows.InsertAt(dr, 0);
        ////            //dtFacility.AcceptChanges(); 

        ////            cmbFacility.DataSource = dtFacility;
        ////            cmbFacility.DisplayMember = dtFacility.Columns["SpecCodeName"].ColumnName;
        ////            cmbFacility.ValueMember = dtFacility.Columns["sCode"].ColumnName;
        ////        }
        ////        cmbFacility.SelectedValue = 11;
        ////        //if (c1CPTs.Rows.Count > 1)
        ////        //{
        ////        //    if (_ContactID != 0)
        ////        //        if (c1CPTs.GetData(1, COL_SpecialityDesc) != null)
        ////        //            cmbFacility.SelectedValue = c1CPTs.GetData(1, COL_SpecialityDesc);
        ////        //}
        ////        oDB.Disconnect();
        ////    }
        ////    catch (gloDatabaseLayer.DBException dbEx)
        ////    {
        ////        dbEx.ERROR_Log(dbEx.ToString());
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        ////    }
        ////    finally
        ////    {
        ////        if (oDB != null) { oDB.Dispose(); }
        ////    }

        ////}

        //private void GetSchedules()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string _sqlRetrieveQuery = "";
        //    DataTable dtTemp = new DataTable();
        //    DataTable dtDistinctCPT = new DataTable();

        //    try
        //    {
        //        oDB.Connect(false);

        //        _sqlRetrieveQuery = "SELECT  DISTINCT ISNULL(HCPCSMOD,'') AS HCPCSMOD  FROM BL_ClinicFeeSchedule Where  InsuranceId = " + _ContactID + " AND SPEC ='" + cmbFacility.SelectedValue + "'";
        //        oDB.Retrive_Query(_sqlRetrieveQuery, out dtDistinctCPT);

        //        c1CPTs.Rows.Count = 1;
        //        if (dtDistinctCPT != null && dtDistinctCPT.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < (dtDistinctCPT.Rows.Count); i++)
        //            {
        //                c1CPTs.Rows.Add();
        //                Int32 Index = c1CPTs.Rows.Count - 1;
        //                dtTemp = null;
        //                _sqlRetrieveQuery = "";
        //                _sqlRetrieveQuery = " SELECT  ISNULL(BL_ClinicFeeSchedule.SPEC,'') AS SPEC,ISNULL(BL_ClinicFeeSchedule.HCPCS,'') AS HCPCS, " +
        //                "ISNULL(BL_ClinicFeeSchedule.MOD,'') AS MOD,ISNULL(BL_ClinicFeeSchedule.FACILITY,'') AS FACILITY, " +
        //                " ISNULL(BL_ClinicFeeSchedule.ALCHG,0) AS ALCHG,  ISNULL(BL_ClinicFeeSchedule.SERVICES,0) AS SERVICES," +
        //                "ISNULL(CPT_MST.sDescription,'') AS CPTDesc FROM BL_ClinicFeeSchedule LEFT OUTER JOIN  CPT_MST ON BL_ClinicFeeSchedule.HCPCSMOD =CPT_MST.sCPTCode  " +
        //                " WHERE BL_ClinicFeeSchedule.InsuranceId = " + _ContactID + "  AND BL_ClinicFeeSchedule.HCPCSMOD ='" + Convert.ToString(dtDistinctCPT.Rows[i]["HCPCSMOD"]) + "' AND BL_ClinicFeeSchedule.SPEC ='" + cmbFacility.SelectedValue + "'";

        //                oDB.Retrive_Query(_sqlRetrieveQuery, out dtTemp);

        //                if (dtTemp != null && dtTemp.Rows.Count > 0)
        //                {
        //                    for (int j = 0; j < dtTemp.Rows.Count; j++)
        //                    {

        //                        if (j == 0)
        //                        {
        //                            c1CPTs.SetData(Index, COL_CPT_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                            c1CPTs.SetData(Index, COL_CPT_CODE, Convert.ToString(dtTemp.Rows[j]["HCPCS"]));
        //                            c1CPTs.SetData(Index, COL_CPT_DESC, Convert.ToString(dtTemp.Rows[j]["CPTDesc"]));
        //                            c1CPTs.SetData(Index, COL_SpecialityDesc, Convert.ToString(dtTemp.Rows[j]["SPEC"]));

        //                        }
        //                        if (j == 1)
        //                        {
        //                            c1CPTs.SetData(Index, COL_MOD1_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1CPTs.SetData(Index, COL_MOD1_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }
        //                        if (j == 2)
        //                        {
        //                            c1CPTs.SetData(Index, COL_MOD2_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1CPTs.SetData(Index, COL_MOD2_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }
        //                        if (j == 3)
        //                        {
        //                            c1CPTs.SetData(Index, COL_MOD3_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1CPTs.SetData(Index, COL_MOD3_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }
        //                        if (j == 4)
        //                        {
        //                            c1CPTs.SetData(Index, COL_MOD4_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1CPTs.SetData(Index, COL_MOD4_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }

        //                    }
        //                }
        //            }

        //        }
        //        oDB.Disconnect();
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
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

        //private void GetSchedules_new()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string _sqlRetrieveQuery = "";
        //    DataTable dtTemp = new DataTable();
        //    DataTable dtDistinctCPT = new DataTable();

        //    try
        //    {
        //        oDB.Connect(false);

        //        _sqlRetrieveQuery = "SELECT  DISTINCT ISNULL(HCPCSMOD,'') AS HCPCSMOD  FROM BL_ClinicFeeSchedule Where  InsuranceId = " + _ContactID + " AND SPEC ='" + cmbFacility.SelectedValue + "'";
        //        oDB.Retrive_Query(_sqlRetrieveQuery, out dtDistinctCPT);

        //        c1_View.Rows.Count = 1;
        //        if (dtDistinctCPT != null && dtDistinctCPT.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < (dtDistinctCPT.Rows.Count); i++)
        //            {
        //                c1_View.Rows.Add();
        //                Int32 Index = c1_View.Rows.Count - 1;
        //                dtTemp = null;
        //                _sqlRetrieveQuery = "";
        //                _sqlRetrieveQuery = " SELECT  ISNULL(BL_ClinicFeeSchedule.SPEC,'') AS SPEC,ISNULL(BL_ClinicFeeSchedule.HCPCS,'') AS HCPCS, " +
        //                "ISNULL(BL_ClinicFeeSchedule.MOD,'') AS MOD,ISNULL(BL_ClinicFeeSchedule.FACILITY,'') AS FACILITY, " +
        //                " ISNULL(BL_ClinicFeeSchedule.ALCHG,0) AS ALCHG,  ISNULL(BL_ClinicFeeSchedule.SERVICES,0) AS SERVICES," +
        //                "ISNULL(CPT_MST.sDescription,'') AS CPTDesc FROM BL_ClinicFeeSchedule LEFT OUTER JOIN  CPT_MST ON BL_ClinicFeeSchedule.HCPCSMOD =CPT_MST.sCPTCode  " +
        //                " WHERE BL_ClinicFeeSchedule.InsuranceId = " + _ContactID + "  AND BL_ClinicFeeSchedule.HCPCSMOD ='" + Convert.ToString(dtDistinctCPT.Rows[i]["HCPCSMOD"]) + "' AND BL_ClinicFeeSchedule.SPEC ='" + cmbFacility.SelectedValue + "'";

        //                oDB.Retrive_Query(_sqlRetrieveQuery, out dtTemp);

        //                if (dtTemp != null && dtTemp.Rows.Count > 0)
        //                {
        //                    for (int j = 0; j < dtTemp.Rows.Count; j++)
        //                    {

        //                        if (j == 0)
        //                        {
        //                            c1_View.SetData(Index, COL_CPT_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                            c1_View.SetData(Index, COL_CPT_CODE, Convert.ToString(dtTemp.Rows[j]["HCPCS"]));
        //                            c1_View.SetData(Index, COL_CPT_DESC, Convert.ToString(dtTemp.Rows[j]["CPTDesc"]));
        //                            c1_View.SetData(Index, COL_SpecialityDesc, Convert.ToString(dtTemp.Rows[j]["SPEC"]));

        //                        }
        //                        if (j == 1)
        //                        {
        //                            c1_View.SetData(Index, COL_MOD1_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1_View.SetData(Index, COL_MOD1_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }
        //                        if (j == 2)
        //                        {
        //                            c1_View.SetData(Index, COL_MOD2_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1_View.SetData(Index, COL_MOD2_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }
        //                        if (j == 3)
        //                        {
        //                            c1_View.SetData(Index, COL_MOD3_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1_View.SetData(Index, COL_MOD3_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }
        //                        if (j == 4)
        //                        {
        //                            c1_View.SetData(Index, COL_MOD4_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1_View.SetData(Index, COL_MOD4_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }

        //                    }
        //                }
        //            }

        //        }

        //        Set_ViewFeeschedulecontrols();
        //        oDB.Disconnect();
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
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

        //private void ImportSchedules()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string _sqlRetrieveQuery = "";
        //    Int64 _StdFeeScheeduleID = 0;
        //    DataTable dtTemp = new DataTable();
        //    DataTable dtDistinctCPT = new DataTable();
        //    if (cmb_StdFeeSchedule.SelectedIndex != -1)
        //    {
        //        _StdFeeScheeduleID = Convert.ToInt64(cmb_StdFeeSchedule.SelectedValue);

        //    }
        //    try
        //    {
        //        oDB.Connect(false);
        //        //_sqlRetrieveQuery = " SELECT  DISTINCT  TOP 150 ISNULL(HCPCSMOD,'')AS HCPCSMOD, ISNULL(SPEC,'') AS SPEC " +
        //        //                    "  FROM BL_StandardFeeSchedule  WHERE nStdFeeScheduleId = " + _StdFeeScheeduleID + " AND SPEC= '" + cmbFacility.SelectedValue.ToString() + "'";

        //        _sqlRetrieveQuery = " SELECT  DISTINCT   ISNULL(HCPCSMOD,'')AS HCPCSMOD, ISNULL(SPEC,'') AS SPEC " +
        //                            "  FROM BL_StandardFeeSchedule  WHERE nStdFeeScheduleId = " + _StdFeeScheeduleID + " AND SPEC= '" + cmbFacility.SelectedValue.ToString() + "'";
        //        oDB.Retrive_Query(_sqlRetrieveQuery, out dtDistinctCPT);

        //        c1CPTs.Rows.Count = 1;
        //        if (dtDistinctCPT != null && dtDistinctCPT.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < (dtDistinctCPT.Rows.Count); i++)
        //            {
        //                c1CPTs.Rows.Add();
        //                Int32 Index = c1CPTs.Rows.Count - 1;
        //                dtTemp = null;
        //                _sqlRetrieveQuery = "";

        //                _sqlRetrieveQuery = " SELECT  ISNULL(BL_StandardFeeSchedule.SPEC,'') AS SPEC,ISNULL(BL_StandardFeeSchedule.HCPCS,'') AS HCPCS, " +
        //                        " ISNULL(BL_StandardFeeSchedule.MOD,'') AS MOD,ISNULL(BL_StandardFeeSchedule.FACILITY,'') AS FACILITY, ISNULL(BL_StandardFeeSchedule.ALCHG,0) AS ALCHG, " +
        //                        " ISNULL(BL_StandardFeeSchedule.SERVICES,0) AS SERVICES,ISNULL(BL_StandardFeeSchedule.sStdFeeScheduleType,'') AS  sStdFeeScheduleType, " +
        //                        " ISNULL(CPT_MST.sDescription,'') AS CPTDesc   FROM BL_StandardFeeSchedule LEFT OUTER JOIN  CPT_MST ON BL_StandardFeeSchedule.HCPCSMOD =CPT_MST.sCPTCode " +
        //                        " WHERE BL_StandardFeeSchedule.nStdFeeScheduleId = " + _StdFeeScheeduleID + " AND BL_StandardFeeSchedule.HCPCSMOD ='" + Convert.ToString(dtDistinctCPT.Rows[i]["HCPCSMOD"]) + "' AND  BL_StandardFeeSchedule.SPEC ='" + Convert.ToString(dtDistinctCPT.Rows[i]["SPEC"]) + "' ";
        //                oDB.Retrive_Query(_sqlRetrieveQuery, out dtTemp);

        //                if (dtTemp != null && dtTemp.Rows.Count > 0)
        //                {
        //                    for (int j = 0; j < dtTemp.Rows.Count; j++)
        //                    {

        //                        if (j == 0)
        //                        {
        //                            c1CPTs.SetData(Index, COL_CPT_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                            c1CPTs.SetData(Index, COL_CPT_CODE, Convert.ToString(dtTemp.Rows[j]["HCPCS"]));
        //                            c1CPTs.SetData(Index, COL_CPT_DESC, Convert.ToString(dtTemp.Rows[j]["CPTDesc"]));
        //                            c1CPTs.SetData(Index, COL_SpecialityDesc, Convert.ToString(dtTemp.Rows[j]["SPEC"]));

        //                        }
        //                        if (j == 1)
        //                        {
        //                            c1CPTs.SetData(Index, COL_MOD1_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1CPTs.SetData(Index, COL_MOD1_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }
        //                        if (j == 2)
        //                        {
        //                            c1CPTs.SetData(Index, COL_MOD2_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1CPTs.SetData(Index, COL_MOD2_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }
        //                        if (j == 3)
        //                        {
        //                            c1CPTs.SetData(Index, COL_MOD3_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1CPTs.SetData(Index, COL_MOD3_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }
        //                        if (j == 4)
        //                        {
        //                            c1CPTs.SetData(Index, COL_MOD4_CODE, Convert.ToString(dtTemp.Rows[j]["MOD"]));
        //                            c1CPTs.SetData(Index, COL_MOD4_CHARGES, Convert.ToString(dtTemp.Rows[j]["ALCHG"]));
        //                        }

        //                    }
        //                }
        //            }

        //        }
        //        if (c1CPTs.Rows.Count > 1)
        //        {
        //            if (_ContactID != 0)
        //            {
        //                if (c1CPTs.GetData(1, COL_SpecialityDesc) != null)

        //                    cmbFacility.SelectedValue = c1CPTs.GetData(1, COL_SpecialityDesc);
        //            }
        //            ts_btnRemoveLine.Visible = true;
        //        }
        //        oDB.Disconnect();
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
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

        //private void cmbFacility_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (c1CPTs.Row == -1)
        //        {
        //            return;
        //        }
        //        //if (c1CPTs.Rows.Count > 1)
        //        //    c1CPTs.SetData(c1CPTs.RowSel, COL_SpecialityDesc, cmbFacility.SelectedValue);

        //        if (_IsAddClicked == true)
        //        {
        //            if (c1CPTs.Rows.Count > 1 && c1CPTs.GetData(1, COL_CPT_CODE) != null && cmb_StdFeeSchedule.Text != "")
        //            {
        //                if (MessageBox.Show("Existing records  will be overwritten ,Do you still want to continue ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //                {
        //                    ImportSchedules();
        //                    if (c1CPTs.Rows.Count == 1)
        //                        ts_btnRemoveLine.Visible = false;
        //                }
        //                else
        //                {
        //                    cmbFacility.SelectedValue = Convert.ToInt64(c1CPTs.GetData(c1CPTs.RowSel, COL_SpecialityDesc));
        //                }

        //            }
        //            else
        //            {
        //                //cmbFacility.SelectedValue = Convert.ToInt64(c1CPTs.GetData(c1CPTs.RowSel, COL_CPT_DESC));  
        //                if (c1CPTs.Rows.Count > 1)
        //                {
        //                    c1CPTs.SetData(c1CPTs.RowSel, COL_SpecialityDesc, cmbFacility.SelectedValue);
        //                    ts_btnRemoveLine.Visible = true;

        //                }
        //                else
        //                {

        //                    //if grid is empty  //200080121
        //                    if (cmb_StdFeeSchedule.Text != "")
        //                    {
        //                        ImportSchedules();
        //                    }
        //                    if (c1CPTs.Rows.Count == 1)
        //                        ts_btnRemoveLine.Visible = false;

        //                }

        //            }
        //        }
        //        if (_IsAddClicked == false)
        //        {
        //            GetSchedules_new();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }

        //}

        //private void Set_ViewFeeschedulecontrols()
        //{
        //    if (c1_View.Rows.Count > 1 && _IsAddClicked == false)
        //    {
        //        ts_btnRemoveLine.Visible = true;
        //    }
        //    else
        //    {
        //        ts_btnRemoveLine.Visible = false;
        //    }
        //    if (c1_View.Rows.Count > 2 && _IsAddClicked == false)
        //    {
        //        ts_btnRemoveAll.Visible = true;
        //        txt_Search.Visible = true;
        //        lblSearch.Visible = true;
        //    }
        //    else
        //    {
        //        ts_btnRemoveAll.Visible = false;
        //        txt_Search.Visible = false;
        //        lblSearch.Visible = false;
        //    }
        //}

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {

                //string strSearch = txt_Search.Text.Trim();
                //strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "");
                //int Rowindex = 0;
                //Rowindex = c1_View.FindRow(strSearch, 0, COL_CPT_CODE, false, false, false);
                //c1_View.Row = Rowindex;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion


        #endregion " Fee Schedule "

        #region "Fee Schedule New"

        private void FillFeeSchedules()
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = null;
            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT ISNULL(nFeeScheduleID,0) AS nFeeScheduleID,ISNULL(sFeeScheduleName,'') AS sFeeScheduleName "
                + " FROM BL_FeeSchedule_MST WHERE nClinicID = " + _ClinicID + " ORDER BY  sFeeScheduleName";

                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["nFeeScheduleID"] = 0;
                    dr["sFeeScheduleName"] = "";
                    dt.Rows.InsertAt(dr, 0);
                    dt.AcceptChanges();

                    cmbFeeSchedules.DataSource = dt.Copy();
                    cmbFeeSchedules.ValueMember = "nFeeScheduleID";
                    cmbFeeSchedules.DisplayMember = "sFeeScheduleName";

                    //cmbFeeSchedules.SelectedIndex = -1;
                    cmbFeeSchedules.Refresh();
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
                _sqlQuery = null;
            }
        }
        private void fillDefaultbillingMethod()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nID");
            dt.Columns.Add("sDesc");
            dt.Rows.Add();
            dt.Rows[0]["nID"] = "";
            dt.Rows[0]["sDesc"] = "";
            dt.Rows.Add();
            dt.Rows[1]["nID"] = "Electronic";
            dt.Rows[1]["sDesc"] = "Electronic";
            dt.Rows.Add();
            dt.Rows[2]["nID"] = "Paper";
            dt.Rows[2]["sDesc"] = "Paper";

            dt.AcceptChanges();
            cmbTypeOFBilling.DataSource = dt;
            cmbTypeOFBilling.ValueMember = "nID";
            cmbTypeOFBilling.DisplayMember = "sDesc";
            cmbTypeOFBilling.Refresh();
        }

        private void fillIncludePOS()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nID");
            dt.Columns.Add("sDesc");
            dt.Rows.Add();
            dt.Rows[0]["nID"] = "";
            dt.Rows[0]["sDesc"] = "";
            dt.Rows.Add();
            dt.Rows[1]["nID"] = "Yes";
            dt.Rows[1]["sDesc"] = "Yes";
            dt.Rows.Add();
            dt.Rows[2]["nID"] = "No";
            dt.Rows[2]["sDesc"] = "No";

            dt.AcceptChanges();
            cmbdonotprintfacility.DataSource = dt;
            cmbdonotprintfacility.ValueMember = "nID";
            cmbdonotprintfacility.DisplayMember = "sDesc";
            cmbdonotprintfacility.Refresh();
        }

        private void fillPaperClaimEPSDTCodeBox()
        {
            using (DataTable dtEPSDTCodeBox = new DataTable())
            {
                dtEPSDTCodeBox.Columns.Add("nID");
                dtEPSDTCodeBox.Columns.Add("sDesc");
                dtEPSDTCodeBox.Rows.Add();
                dtEPSDTCodeBox.Rows[0]["nID"] = "24h shaded";
                dtEPSDTCodeBox.Rows[0]["sDesc"] = "24h shaded";
                dtEPSDTCodeBox.Rows.Add();
                dtEPSDTCodeBox.Rows[1]["nID"] = "24c unshaded";
                dtEPSDTCodeBox.Rows[1]["sDesc"] = "24c unshaded";
                dtEPSDTCodeBox.Rows.Add();
                dtEPSDTCodeBox.Rows[2]["nID"] = "24h unshaded";
                dtEPSDTCodeBox.Rows[2]["sDesc"] = "24h unshaded";
                dtEPSDTCodeBox.AcceptChanges();
                cmbEPSDTCodeBox.DataSource = dtEPSDTCodeBox.Copy();
                cmbEPSDTCodeBox.ValueMember = "nID";
                cmbEPSDTCodeBox.DisplayMember = "sDesc";
                cmbEPSDTCodeBox.Refresh();
            }
        }

        private void fillPaperClaimFamilyPlanningCodeBox()
        {
            using (DataTable dtFPCodeBox = new DataTable())
            {
                dtFPCodeBox.Columns.Add("nID");
                dtFPCodeBox.Columns.Add("sDesc");
                dtFPCodeBox.Rows.Add();
                dtFPCodeBox.Rows[0]["nID"] = "24h unshaded";
                dtFPCodeBox.Rows[0]["sDesc"] = "24h unshaded";
                dtFPCodeBox.AcceptChanges();
                cmbFamPlanCodeBox.DataSource = dtFPCodeBox.Copy();
                cmbFamPlanCodeBox.ValueMember = "nID";
                cmbFamPlanCodeBox.DisplayMember = "sDesc";
                cmbFamPlanCodeBox.Refresh();
            }
        }


        private void FillPaperBilling()
        {
            cmbClIAPostn.Items.Clear();
            cmbClIAPostn.Items.Add("Box 23");
            cmbClIAPostn.Items.Add("Box 19");
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                string _sqlQuery = "SELECT ISNULL(nID,0) AS nID ,ISNULL(sBox29,'') AS sBox29 ,ISNULL(sBox30,'') AS sBox30 from BL_PaperBillingdefaultSetting";
                oDB.Retrive_Query(_sqlQuery, out dt);

                if (dt != null)
                {
                    cmbBox29.DataSource = dt.Copy();
                    cmbBox29.ValueMember = "nID";
                    cmbBox29.DisplayMember = "sBox29";
                    cmbBox29.Refresh();
                    cmbBox30.DataSource = dt.Copy();
                    cmbBox30.ValueMember = "nID";
                    cmbBox30.DisplayMember = "sBox30";
                    cmbBox30.Refresh();
                }


                _sqlQuery = " SELECT ISNULL(nSettingValue,0) AS nSettingValue"
                + " FROM BL_PaperBillingSetting WHERE nSettingLevel=30 And nSettingType=29 And nClinicID = " + _ClinicID + " AND nContactID =" + ContactID + " order by nSettingType";

                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                    cmbBox29.SelectedValue = Convert.ToInt16(dt.Rows[0]["nSettingValue"].ToString());

                _sqlQuery = " SELECT ISNULL(nSettingValue,0) AS nSettingValue"
                + " FROM BL_PaperBillingSetting WHERE nSettingLevel=30 And nSettingType=30 And nClinicID = " + _ClinicID + " AND nContactID =" + ContactID + " order by nSettingType";

                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                    cmbBox30.SelectedValue = Convert.ToInt16(dt.Rows[0]["nSettingValue"].ToString());

                _sqlQuery = " SELECT ISNULL(nSettingValue,0) AS nSettingValue"
               + " FROM BL_PaperBillingSetting WHERE nSettingLevel=30 And nSettingType=23 And nClinicID = " + _ClinicID + " AND nContactID =" + ContactID + " order by nSettingType";

                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["nSettingValue"].ToString() == "2")
                        cmbClIAPostn.SelectedItem = "Box 19";
                    else
                        cmbClIAPostn.SelectedItem = "Box 23";
                }
                else
                    cmbClIAPostn.SelectedItem = "Box 23";

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }

        }

        private void fillUBo4BillingProviderOtherID()
        {
            GeneralSettings oSettings = null;
            DataTable dtQualifiersAssociation = null;
            DataTable dtSources = null;

            try
            {
                oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                dtQualifiersAssociation = oSettings.getIDQualifiersAssociation(false,false);

                if (dtQualifiersAssociation != null && dtQualifiersAssociation.Rows.Count > 0)
                {
                    cmbUBBlngprvdraltID.DataSource = dtQualifiersAssociation.Copy();
                    cmbUBBlngprvdraltID.DisplayMember = "sAdditionalDescription";
                    cmbUBBlngprvdraltID.ValueMember = "nQualifierID";
                    cmbUBBlngprvdraltID.Update();
                    cmbUBBlngprvdraltID.Refresh();

                }
                fillUBo4FedTaxId();
            }
                 catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); oSettings = null; }
                if (dtQualifiersAssociation != null) { dtQualifiersAssociation.Dispose(); dtQualifiersAssociation = null; }
                if (dtSources != null) { dtSources.Dispose(); dtSources = null; }
            }
        }

        private void fillUBo4FedTaxId()
        {
            DataTable dtIDQualifiers = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
           
            try
            {
                oDB.Connect(false);
                //dtIDQualifiers = new DataTable();
                oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDB.Retrive("BL_Get_FedTaxID_UB04", oDBParameters, out dtIDQualifiers);

                cmbFedTaxNoBox5.DataSource = dtIDQualifiers.Copy();
                cmbFedTaxNoBox5.DisplayMember = "sAdditionalDescription";
                cmbFedTaxNoBox5.ValueMember = "nQualifierID";
                cmbFedTaxNoBox5.Update();
                cmbFedTaxNoBox5.Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                if (dtIDQualifiers != null)
                {
                    dtIDQualifiers.Dispose();
                    dtIDQualifiers = null;
                }

            }
        }
        private void LoadAlternatePayerID()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dt = null;
            string _sqlQuery = null;
            AlternatePayerID _objAlternatePayerID = null;
            try
            {
                //DataView dv = null;

                oDB.Connect(false);
                _sqlQuery = "Select ISNULL(nAlternateID,0)AS ID, ISNULL(sName,'') AS Name , ISNULL(sAlternatePayerId,'') AS PayerID, ISNULL(sDescription,'') AS Description from ERA_AlternatePayerID WHERE  nContactID=" + _ContactID;
                oDB.Retrive_Query(_sqlQuery, out _dt);
                //dv = _dt.DefaultView;
                if (_dt != null)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        _objAlternatePayerID = new AlternatePayerID();
                        _objAlternatePayerID.ID = Convert.ToInt64(_dt.Rows[i][0]);
                        _objAlternatePayerID.Name = _dt.Rows[i][1].ToString();
                        _objAlternatePayerID.AlternatePayerId = _dt.Rows[i][2].ToString();
                        _objAlternatePayerID.Desc = _dt.Rows[i][3].ToString();
                        _objAlternatePayerIDs.Add(_objAlternatePayerID);
                        _objAlternatePayerID = null;
                    }
                }
                DataGridViewTextBoxColumn IDColumn = new DataGridViewTextBoxColumn();
                IDColumn.DataPropertyName = "ID";
                IDColumn.HeaderText = "ID";

                DataGridViewTextBoxColumn NameColumn = new DataGridViewTextBoxColumn();
                NameColumn.DataPropertyName = "Name";
                NameColumn.HeaderText = "Name";

                DataGridViewTextBoxColumn PayerColumn = new DataGridViewTextBoxColumn();
                PayerColumn.DataPropertyName = "AlternatePayerId";
                PayerColumn.HeaderText = "Payer Id";

                DataGridViewTextBoxColumn DescriptionColumn = new DataGridViewTextBoxColumn();
                DescriptionColumn.DataPropertyName = "Desc";
                DescriptionColumn.HeaderText = "Description";


                dgMasters.Columns.Add(IDColumn);
                dgMasters.Columns.Add(NameColumn);
                dgMasters.Columns.Add(PayerColumn);
                dgMasters.Columns.Add(DescriptionColumn);

                IDColumn = null;
                NameColumn = null;
                PayerColumn = null;
                DescriptionColumn = null;

                //Make columns visible true or false
                dgMasters.Columns[0].Visible = false;
                dgMasters.Columns[1].Visible = true;
                dgMasters.Columns[2].Visible = true;
                dgMasters.Columns[3].Visible = true;

                //Set the width for columns of a grid(dgMasters)
                int nWidth = dgMasters.Width;
                dgMasters.Columns[0].Width = 0;
                dgMasters.Columns[1].Width = (int)(nWidth * 0.20 - 10);
                dgMasters.Columns[2].Width = (int)(nWidth * 0.15 - 10);
                dgMasters.Columns[3].Width = (int)(nWidth * 0.65);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                _objAlternatePayerID = null;
            }

        }
        private void FillAlternatePayerID()
        {


            try
            {

                // dgMasters.DataSource = null; 
                dgMasters.DataSource = _objAlternatePayerIDs;
                dgMasters.Refresh();


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }

        }

        private void deleteAlternatePayerID(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                string _sqlQuery = "Delete  from ERA_AlternatePayerID WHERE  nAlternateID=" + ID;
                oDB.Connect(false);
                oDB.Execute_Query(_sqlQuery);
                _sqlQuery = null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }
        private void SaveFeeSchedule(long ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                if (cmbFeeSchedules.SelectedIndex == -1)
                    return;

                oDB.Connect(false);
                // solving salesforce GLO2010-0007633
                // string _sqlQuery = " DELETE FROM BL_InsuranceFeeSchedule_Allocation WHERE nFeeScheduleID = " + Convert.ToInt64(cmbFeeSchedules.SelectedValue) + " AND nClinicID = " + _ClinicID;

                string _sqlQuery = " DELETE FROM BL_InsuranceFeeSchedule_Allocation WHERE nClinicID = " + _ClinicID + " AND nInsuranceID = " + _ContactID;
                oDB.Execute_Query(_sqlQuery);

                if (cmbFeeSchedules.SelectedValue != null && Convert.ToInt64(cmbFeeSchedules.SelectedValue) > 0)
                {
                    _sqlQuery = "INSERT INTO BL_InsuranceFeeSchedule_Allocation (nFeeScheduleID, nInsuranceID, nClinicID) "
                    + " VALUES (" + Convert.ToInt64(cmbFeeSchedules.SelectedValue) + ", " + ContactID + ", " + _ClinicID + ")";

                    oDB.Execute_Query(_sqlQuery);
                }

                _sqlQuery = null;
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Dispose();
            }
        }
        private void SavePaperBillingSetting()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            string _sqlQuery = null;
            oDB.Connect(false);
            try
            {
                _sqlQuery = " DELETE FROM BL_PaperBillingSetting WHERE nSettingLevel=30 And nClinicID = " + _ClinicID + " AND nContactID= " + _ContactID;
                oDB.Execute_Query(_sqlQuery);
                if (cmbBox29.SelectedIndex == -1 || cmbBox29.SelectedIndex == 0)
                {

                }
                else
                {
                    oDBParameters.Add("@nCompanyID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nContactID", ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nSettingLevel", PaperBillingSettingLevel.InsurancePlan.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@nSettingType", PaperBillingBoxtype.Box29.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@nSettingValue", Convert.ToInt16(cmbBox29.SelectedValue), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nUserID", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    oDB.Execute("BL_INUP_PaperBillingSettings", oDBParameters);
                }
                if (cmbBox30.SelectedIndex == -1 || cmbBox30.SelectedIndex == 0)
                {
                }
                else
                {
                    oDBParameters.Clear();
                    oDBParameters.Add("@nCompanyID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nContactID", ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nSettingLevel", PaperBillingSettingLevel.InsurancePlan.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@nSettingType", PaperBillingBoxtype.Box30.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@nSettingValue", Convert.ToInt16(cmbBox30.SelectedValue), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nUserID", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    oDB.Execute("BL_INUP_PaperBillingSettings", oDBParameters);

                }
                oDBParameters.Clear();
                oDBParameters.Add("@nCompanyID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nSettingLevel", PaperBillingSettingLevel.InsurancePlan, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nSettingType", PaperBillingBoxtype.Box23.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                int box19 = Convert.ToInt16(cmbClIAPostn.SelectedIndex == 1 ? 2 : 1);
                oDBParameters.Add("@nSettingValue", box19, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nUserID", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDB.Execute("BL_INUP_PaperBillingSettings", oDBParameters);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Dispose();
                _sqlQuery = null;
            }

        }

        private void SaveCorrectedReplacement(long ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {

                oDB.Connect(false);

                try
                {

                    oDBParameters.Add("@nContactID", ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nInsCompanyID", Convert.ToInt64(cmbInsuranceCompany.SelectedValue), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@bIsCorrectRplmnt", (chkCorrectRplmnt.Checked == true ? 1 : 0), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@nCreatedUserID", _oCorrectedReplacement.nCreatedUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nModifiedUserID", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    oDB.Execute("BL_INUP_CorrectedReplacement_Plan", oDBParameters);


                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Dispose();
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }

        }


        private void FillInsuranceFeeSchedule(long ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            try
            {

                oDB.Connect(false);

                string _sqlQuery = "SELECT ISNULL(BL_FeeSchedule_MST.nFeeScheduleID,0) AS  nFeeScheduleID, ISNULL(BL_FeeSchedule_MST.sFeeScheduleName,'') AS sFeeScheduleName"
                + " FROM BL_FeeSchedule_MST INNER JOIN BL_InsuranceFeeSchedule_Allocation ON BL_FeeSchedule_MST.nFeeScheduleID = BL_InsuranceFeeSchedule_Allocation.nFeeScheduleID"
                + " WHERE BL_InsuranceFeeSchedule_Allocation.nInsuranceID = " + ContactID + " AND BL_InsuranceFeeSchedule_Allocation.nClinicID = " + _ClinicID;

                oDB.Retrive_Query(_sqlQuery, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmbFeeSchedules.Text = Convert.ToString(dt.Rows[0]["sFeeScheduleName"]);
                }
                _sqlQuery = null;
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Dispose();
                if (dt != null) { dt.Dispose(); dt = null; }
            }
        }

        //private void FillPaperBillingSetting()
        //{

        //}

        #region "CPT Mapping"

        private void Fill_CPTMapping()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtClearing = null;
            string _sqlQuery = "";
            try
            {


                oDB.Connect(false);

                _sqlQuery = "SELECT ISNULL(nCPTMappingID,0) as nCPTMappingID,ISNULL(sCPTMappingName,'') as sCPTMappingName from CPT_Mapping_MST order by sCPTMappingName";

                oDB.Retrive_Query(_sqlQuery, out dtClearing);


                if (dtClearing != null && dtClearing.Rows.Count > 0)
                {
                    DataRow dr = dtClearing.NewRow();
                    dr["nCPTMappingID"] = 0;
                    dr["sCPTMappingName"] = "";
                    dtClearing.Rows.InsertAt(dr, 0);
                    dtClearing.AcceptChanges();

                    cmbCptCrosswalk.DataSource = dtClearing;
                    cmbCptCrosswalk.DisplayMember = "sCPTMappingName";
                    cmbCptCrosswalk.ValueMember = "nCPTMappingID";
                    cmbCptCrosswalk.Refresh();
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
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }


        }

        #endregion "CPT Mapping"

        #endregion

        #region "Anesthesia Billing"

        private bool IsAnesthesiaBillingEnabled()
        {
            GeneralSettings oSettings = null;
            bool IsAnesthesiaBillingEnabled = false;
            object oAnesthesiaBillingSetting = null;
            try
            {
                oSettings = new GeneralSettings(_databaseconnectionstring);
                oSettings.GetSetting("EnableAnesthesiaBilling", 0, _ClinicID, out oAnesthesiaBillingSetting);
                if (!string.IsNullOrEmpty(Convert.ToString(oAnesthesiaBillingSetting)) && (Convert.ToString(oAnesthesiaBillingSetting).ToUpper() == "TRUE" || Convert.ToString(oAnesthesiaBillingSetting).ToUpper() == "FALSE"))
                {
                    if (Convert.ToBoolean(oAnesthesiaBillingSetting))
                    {
                        IsAnesthesiaBillingEnabled = true;
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); oSettings = null; }
                oAnesthesiaBillingSetting = null;
            }
            return IsAnesthesiaBillingEnabled;
        }

        #endregion "Anesthesia Billing"

        # region ToolTip

        //Event for showing the ToolTip on DropList - Added By Pramod Nair on 20100121
        //void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        //{

        //    combo = (ComboBox)sender;
        //    if (combo.Items.Count > 0 && e.Index >= 0)
        //    {

        //        e.DrawBackground();
        //        using (SolidBrush br = new SolidBrush(e.ForeColor))
        //        {
        //            e.Graphics.DrawString(combo.GetItemText(combo.Items[e.Index]).ToString(), e.Font, br, e.Bounds);
        //        }

        //        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
        //        {
        //            if (combo.DroppedDown)
        //            {
        //                if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 18 && _LastBoundIndex != e.Index)
        //                    this.toolTip1.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 170, e.Bounds.Bottom + 30);
        //                _LastBoundIndex = e.Index;


        //            }
        //            else
        //            {
        //                this.toolTip1.Hide(combo);
        //            }
        //        }
        //        else
        //        {
        //            this.toolTip1.SetToolTip(combo, "");
        //        }
        //        e.DrawFocusRectangle();
        //    }
        //}
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
       // Int32 _LastBoundIndex = -1;


        //Function For Calculating the Lenghth of the Items in the combo box
        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
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

        # endregion

        private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
        {

            //((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (((MaskedTextBox)sender).Text.Trim() == "")
            //{
            //    ((MaskedTextBox)sender).SelectionStart = 0;
            //    ((MaskedTextBox)sender).SelectionLength = 0;
            //}

        }

        private void gBoxComContact_Enter(object sender, EventArgs e)
        {

        }



        //Sandip Darade 20100130  BuG ID 2435  Case No GLO2008-0002029
        private void txt_PayerPhExt_TextChanged(object sender, EventArgs e)
        {
            _IsModified = true;
            if (_IsDeleteclicked == false)
            {
                foreach (char c in txt_PayerPhExt.Text)
                {
                    if (Regex.IsMatch(c.ToString(), "[0-9]") == false)
                    {
                        txt_PayerPhExt.Text = txt_PayerPhExt.Text.Replace(c.ToString(), "");

                    }

                }
                txt_PayerPhExt.SelectionStart = txt_PayerPhExt.Text.Trim().Length;
            }


        }

        private void txt_PayerPhExt_KeyPress(object sender, KeyPressEventArgs e)
        {
            _IsDeleteclicked = false;

            // ((MaskedTextBox)sender).SelectionStart = mtxt_PayerPhExt.Text.Trim().Length;
            if ((e.KeyChar == Convert.ToChar(8)) || (e.KeyChar == Convert.ToChar(32)))
            {
                //  _IsDeleteclicked = true;
            }

        }

        private void txt_PayerPhExt_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_PayerPhExt.Text == "")
                txt_PayerPhExt.SelectionStart = txt_PayerPhExt.Text.Trim().Length;
        }

        private void txt_PayerPhExt_Enter(object sender, EventArgs e)
        {
            _IsDeleteclicked = false;
        }

        private void txt_PayerPhExt_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void txt_PayerPhExt_CursorChanged(object sender, EventArgs e)
        {

        }

        private void txt_PayerPhExt_Click(object sender, EventArgs e)
        {

        }

        private void cmbInsuranceCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isCmbInsuranceCompanyLoading == false && _ContactID == 0)
            {
                FillInsuranceComapanyDefaultDetails();
                if ((txtname.Text.Trim() == "") || (txtname.Text.Trim() == _LastCompanyChanged.Trim()))
                {
                    txtname.Text = cmbInsuranceCompany.Text;
                    _LastCompanyChanged = txtname.Text;
                }
            }
            combo = (ComboBox)sender;
            if (cmbInsuranceCompany.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany) >= cmbInsuranceCompany.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbInsuranceCompany, Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]));
                //                                      (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany, 0, cmbInsuranceCompany.Bottom - 98);
                else
                    this.toolTip1.Hide(combo);

            }
        }
        public void LoadInsuranceComapanytDetails(Int64 InsurancePlanId)
        {
            string _strsql = "";
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDb.Connect(false);
                _strsql = "SELECT nCompanyID From Contact_InsurancePlan_Association WHERE nContactId=" + _ContactID + " AND nClinicId=" + _ClinicID;
                oDb.Retrive_Query(_strsql, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["nCompanyID"] != null)
                    {
                        _DefaultCompanyId = Convert.ToInt64(dt.Rows[0]["nCompanyID"]);
                        cmbInsuranceCompany.SelectedValue = _DefaultCompanyId;

                    }
                }


                _strsql = "SELECT nReportingCategoryID From Contact_InsurancePlanReportingCat_Association WHERE nContactId=" + _ContactID + " AND nClinicId=" + _ClinicID;
                oDb.Retrive_Query(_strsql, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["nReportingCategoryID"] != null)
                    {
                        cmbReportingCategory.SelectedValue = Convert.ToInt64(dt.Rows[0]["nReportingCategoryID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _strsql = null;
                if (dt != null) { dt.Dispose(); dt = null; }
                if (oDb != null) { oDb.Disconnect(); oDb.Dispose(); }
            }

        }
        public DataTable FillInsuranceComapanyDefaultDetails()
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strsql = null;
            try
            {
                Int64 _nInsuranceType = 0;
                Int64 _nDefaultReportingCategoryId = 0;
                Int64 _nDefaultFeeSchedule = 0;
                oDb.Connect(false);
                Int64 _CompanyContactID = Convert.ToInt64(cmbInsuranceCompany.SelectedValue);

                _strsql = "SELECT nID,ISNULL( sCode,'') as sCode ,ISNULL(sDescription,'')as sDescription,ISNULL(nInsuranceType,0) as nInsuranceType,ISNULL(nReportCategoryID,0) as nReportCategoryID,ISNULL(nFeeScheduleID,0) as nFeeScheduleID ,ISNULL(sAddressLine1,'')as sAddressLine1,ISNULL(sAddressLine2,'')as sAddressLine2,ISNULL(sCity,'')as sCity,ISNULL(sState,'') as sState,ISNULL(sZip,'')as sZip,ISNULL(sPayerID,'')as sPayerID,nClinicID,ISNULL(nCPTMappingID,0) AS nCPTMappingID,ISNULL(nTypeOBilling,0) AS nTypeOBilling,ISNULL(bIsInstitutionalBilling,0) AS bIsInstitutionalBilling from Contacts_InsuranceCompany_MST where nId= '" + Convert.ToInt64(cmbInsuranceCompany.SelectedValue) + "'";
                oDb.Retrive_Query(_strsql, out dt);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    oAddresscontrol.isFormLoading = true;
                    oAddresscontrol.txtAddress1.Text = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                    oAddresscontrol.txtAddress2.Text = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                    oAddresscontrol.txtCity.Text = Convert.ToString(dt.Rows[0]["sCity"]);
                    oAddresscontrol.txtZip.Text = Convert.ToString(dt.Rows[0]["sZip"]);
                    oAddresscontrol.cmbState.Text = Convert.ToString(dt.Rows[0]["sState"]);
                    oAddresscontrol.isFormLoading = false;
                    txtPayerID.Text = Convert.ToString(dt.Rows[0]["sPayerID"]);
                    _nInsuranceType = Convert.ToInt64(dt.Rows[0]["nInsuranceType"]);
                    _nDefaultReportingCategoryId = Convert.ToInt64(dt.Rows[0]["nReportCategoryID"]);
                    _nDefaultFeeSchedule = Convert.ToInt64(dt.Rows[0]["nFeeScheduleID"]);
                    cmbCptCrosswalk.SelectedValue = Convert.ToInt64(dt.Rows[0]["nCPTMappingID"]);
                    chkIsInstitutionalBilling.Checked = Convert.ToBoolean(dt.Rows[0]["bIsInstitutionalBilling"]);

                    if (Convert.ToInt16(dt.Rows[0]["nTypeOBilling"]) == Convert.ToInt16(gloPMContacts.TypeOfBilling.Paper))
                    {
                        cmbTypeOFBilling.Text = "Paper";

                    }
                    if (Convert.ToInt16(dt.Rows[0]["nTypeOBilling"]) == Convert.ToInt16(gloPMContacts.TypeOfBilling.Electronic))
                    {
                        cmbTypeOFBilling.Text = "Electronic";
                    }
                    if (Convert.ToInt16(dt.Rows[0]["nTypeOBilling"]) == Convert.ToInt16(gloPMContacts.TypeOfBilling.None))
                    {
                        cmbTypeOFBilling.SelectedIndex = -1;
                    }

                }
                else { cmbCptCrosswalk.SelectedValue = 0; }
                _strsql = "SELECT ISNULL(sInsuranceTypeDesc,'') as sInsuranceTypeDesc FROM InsuranceType_MST ";
                if (_nInsuranceType > 0)
                {
                    _strsql = _strsql + " WHERE nInsuranceTypeID=" + _nInsuranceType;
                }
                else
                {
                    _strsql = _strsql + " WHERE nInsuranceTypeID=0";
                }
                oDb.Retrive_Query(_strsql, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmbInsuranceType.Text = Convert.ToString(dt.Rows[0]["sInsuranceTypeDesc"]);
                    }
                }

                _strsql = "SELECT ISNULL(sDescription,'')as sDescription FROM Contacts_InsuranceReportingCategory_MST ";
                if (_nDefaultReportingCategoryId > 0)
                {
                    _strsql = _strsql + " WHERE nID=" + _nDefaultReportingCategoryId;
                }
                else
                {
                    _strsql = _strsql + " WHERE nID=0";
                }
                oDb.Retrive_Query(_strsql, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmbReportingCategory.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                    }
                }
                _strsql = "SELECT ISNULL(sFeeScheduleName,'') AS sFeeScheduleName FROM BL_FeeSchedule_MST ";
                if (_nDefaultFeeSchedule > 0)
                {
                    _strsql = _strsql + " WHERE nFeeScheduleID=" + _nDefaultFeeSchedule;
                }
                else
                {
                    _strsql = _strsql + " WHERE nFeeScheduleID=0";
                }
                oDb.Retrive_Query(_strsql, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmbFeeSchedules.Text = Convert.ToString(dt.Rows[0]["sFeeScheduleName"]);
                    }
                }







                oDb.Disconnect();
            }

            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            finally
            {
                _strsql = null;
            }
            return dt;

            //throw new Exception("The method or operation is not implemented.");
        }

        private void rbAcceptAssignmentYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAcceptAssignmentYes.Checked == true)
            {
                rbAcceptAssignmentYes.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbAcceptAssignmentYes.Font = gloGlobal.clsgloFont.gFont;
            }

        }

        private void rbStatementToPatientYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStatementToPatientYes.Checked == true)
            {
                rbStatementToPatientYes.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbStatementToPatientYes.Font = gloGlobal.clsgloFont.gFont;
            }
        }

        private void rbAcceptAssignmentNo_CheckedChanged(object sender, EventArgs e)
        {
            _IsModified = true;
            if (rbAcceptAssignmentNo.Checked == true)
            {
                rbAcceptAssignmentNo.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbAcceptAssignmentNo.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbStatementToPatientNo_CheckedChanged(object sender, EventArgs e)
        {
            _IsModified = true;
            if (rbStatementToPatientNo.Checked == true)
            {
                rbStatementToPatientNo.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbStatementToPatientNo.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void frmSetupInsurance_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((oAddresscontrol.AddressModified == true || _IsModified == true) && _IsSaveClicked == false)
            {
                DialogResult _Result;
                _Result = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (_Result == DialogResult.Yes)
                {
                    if (SaveInsurancePlan() == false)
                    {
                        e.Cancel = true;
                        _IsSaveClicked = false;
                    }
                }
                else if (_Result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }
            if (htMidLevelSettings != null)
            {
                htMidLevelSettings.Clear();
                htMidLevelSettings = null;
            }
            if (htSourceSettings != null)
            {
                htSourceSettings.Clear();
                htSourceSettings = null;
            }
        }


        private void AllControlValueChanged_Event(object sender, System.EventArgs e)
        {
            _IsModified = true;
        }

        private void tls_Hold_Click(object sender, EventArgs e)
        {
            gloPMContacts.gloContacts ogloContact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            try
            {
                if (!_oPlanHold.IsHold)
                {
                    if (ogloContact.ClaimCountUnderBatch(_ContactID) > 0)
                    {
                        if (MessageBox.Show("Claims are included in Insurance Billing batches. They will be put on hold and removed from their batches." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                frmPlanHold ofrmPlanHold = new frmPlanHold(_databaseconnectionstring, Convert.ToString(txtname.Text), _ContactID, _DefaultCompanyId, _oPlanHold);
                ofrmPlanHold.ShowDialog(this);

                if (ofrmPlanHold.oDialogResult)
                {
                    _oPlanHold = ofrmPlanHold._oPlanHold;
                }
                GetHoldMessage();
                ofrmPlanHold.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloContact != null) { ogloContact.Dispose(); ogloContact = null; }
            }
        }

        //private void cmbCptCrosswalk_MouseMove(object sender, MouseEventArgs e)
        //{
        //    combo = (ComboBox)sender;
        //    if (cmbCptCrosswalk.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbCptCrosswalk.Items[cmbCptCrosswalk.SelectedIndex])["sCPTMappingName"]), cmbCptCrosswalk) >= cmbCptCrosswalk.DropDownWidth - 18)
        //            this.toolTip1.SetToolTip(cmbCptCrosswalk,Convert.ToString(((DataRowView)cmbCptCrosswalk.Items[cmbCptCrosswalk.SelectedIndex])["sCPTMappingName"]));//, cmbCptCrosswalk, 0, cmbCptCrosswalk.Bottom - 98);
        //        else
        //            this.toolTip1.Hide(combo);

        //    }
        //}

        private void cmbCptCrosswalk_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _IsModified = true;
        }

        private void chkCorrectRplmnt_Leave(object sender, EventArgs e)
        {

            // TopToolStrip.Focus();
        }

        private void txtURL_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                if (CheckURL(txtURL.Text) == false)
                {
                    MessageBox.Show("Please enter a valid URL ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
            }
        }

        private void tls_Release_Click(object sender, EventArgs e)
        {
            tls_Hold_Click(sender, e);
        }

        private void chkPARequired_Leave(object sender, EventArgs e)
        {
            TopToolStrip.Focus();
            tls_Hold.Select();
        }
        //private void cmbFeeSchedules_MouseMove(object sender, MouseEventArgs e)
        //{

        //    combo = (ComboBox)sender;
        //    if (cmbFeeSchedules.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbFeeSchedules.Items[cmbFeeSchedules.SelectedIndex])["sFeeScheduleName"]), cmbFeeSchedules) >= cmbFeeSchedules.DropDownWidth - 18)
        //            this.toolTip1.SetToolTip(cmbFeeSchedules,Convert.ToString(((DataRowView)cmbFeeSchedules.Items[cmbFeeSchedules.SelectedIndex])["sFeeScheduleName"]));//, cmbFeeSchedules, 0, cmbFeeSchedules.Bottom - 98);
        //        else
        //            this.toolTip1.Hide(combo);

        //    }

        //}


        private void cmbdonotprintfacility_MouseMove(object sender, MouseEventArgs e)
        {

        }

        //private void cmbClearingHouse_MouseMove(object sender, MouseEventArgs e)
        //{
        //    combo = (ComboBox)sender;
        //    if (cmbClearingHouse.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbClearingHouse.Items[cmbClearingHouse.SelectedIndex])["sClearingHouseName"]), cmbClearingHouse) >= cmbClearingHouse.DropDownWidth - 18)
        //            this.toolTip1.SetToolTip(cmbClearingHouse, Convert.ToString(((DataRowView)cmbClearingHouse.Items[cmbClearingHouse.SelectedIndex])["sClearingHouseName"]));//, cmbClearingHouse, 0, cmbClearingHouse.Bottom - 98);
        //        else
        //            this.toolTip1.Hide(combo);

        //    }
        //}

        private void cmbMidLevelSpeProvider_MouseMove(object sender, MouseEventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbMidLevelSpeProvider.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbMidLevelSpeProvider.Items[cmbMidLevelSpeProvider.SelectedIndex])["sSettingsName"]), cmbMidLevelSpeProvider) >= cmbMidLevelSpeProvider.DropDownWidth - 18)
                    this.toolTip1.Show(Convert.ToString(((DataRowView)cmbMidLevelSpeProvider.Items[cmbMidLevelSpeProvider.SelectedIndex])["sSettingsName"]), cmbMidLevelSpeProvider, 0, cmbMidLevelSpeProvider.Bottom - 98);
                else
                    this.toolTip1.Hide(combo);

            }
        }





        //private void cmbInsuranceCompany_MouseMove(object sender, MouseEventArgs e)
        //{
        //    combo = (ComboBox)sender;
        //    if (cmbInsuranceCompany.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany) >= cmbInsuranceCompany.DropDownWidth - 18)
        //            this.toolTip1.SetToolTip(cmbInsuranceCompany, Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]));
        //        //                                      (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany, 0, cmbInsuranceCompany.Bottom - 98);
        //        else
        //            this.toolTip1.Hide(combo);

        //    }
        //}

        private void cmbInsuranceCompany_MouseEnter(object sender, EventArgs e)
        {
            if (cmbInsuranceCompany.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany) >= cmbInsuranceCompany.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbInsuranceCompany, Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]));
                //                                      (Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany, 0, cmbInsuranceCompany.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbInsuranceCompany, "");

            }
        }

        private void cmbReportingCategory_MouseEnter(object sender, EventArgs e)
        {
            if (cmbReportingCategory.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbReportingCategory.Items[cmbReportingCategory.SelectedIndex])["sDescription"]), cmbReportingCategory) >= cmbReportingCategory.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbReportingCategory, Convert.ToString(((DataRowView)cmbReportingCategory.Items[cmbReportingCategory.SelectedIndex])["sDescription"]));//, cmbReportingCategory, 0, cmbReportingCategory.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbReportingCategory, "");

            }
        }

        private void cmbInsuranceType_MouseEnter(object sender, EventArgs e)
        {
            if (cmbInsuranceType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsuranceType.Items[cmbInsuranceType.SelectedIndex])["sInsuranceTypeDesc"]), cmbInsuranceType) >= cmbInsuranceType.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbInsuranceType, Convert.ToString(((DataRowView)cmbInsuranceType.Items[cmbInsuranceType.SelectedIndex])["sInsuranceTypeDesc"]));//, cmbInsuranceType, 0, cmbInsuranceType.Bottom - 40);
                else
                    this.toolTip1.SetToolTip(cmbInsuranceType, "");

            }
        }

        private void cmbFeeSchedules_MouseEnter(object sender, EventArgs e)
        {
            if (cmbFeeSchedules.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbFeeSchedules.Items[cmbFeeSchedules.SelectedIndex])["sFeeScheduleName"]), cmbFeeSchedules) >= cmbFeeSchedules.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbFeeSchedules, Convert.ToString(((DataRowView)cmbFeeSchedules.Items[cmbFeeSchedules.SelectedIndex])["sFeeScheduleName"]));//, cmbFeeSchedules, 0, cmbFeeSchedules.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbFeeSchedules, "");

            }
        }

        private void cmbCptCrosswalk_MouseEnter(object sender, EventArgs e)
        {
            if (cmbCptCrosswalk.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbCptCrosswalk.Items[cmbCptCrosswalk.SelectedIndex])["sCPTMappingName"]), cmbCptCrosswalk) >= cmbCptCrosswalk.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbCptCrosswalk, Convert.ToString(((DataRowView)cmbCptCrosswalk.Items[cmbCptCrosswalk.SelectedIndex])["sCPTMappingName"]));//, cmbCptCrosswalk, 0, cmbCptCrosswalk.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbCptCrosswalk, "");

            }
        }

        private void cmbClearingHouse_MouseEnter(object sender, EventArgs e)
        {
            if (cmbClearingHouse.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbClearingHouse.Items[cmbClearingHouse.SelectedIndex])["sClearingHouseName"]), cmbClearingHouse) >= cmbClearingHouse.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbClearingHouse, Convert.ToString(((DataRowView)cmbClearingHouse.Items[cmbClearingHouse.SelectedIndex])["sClearingHouseName"]));//, cmbClearingHouse, 0, cmbClearingHouse.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbClearingHouse, "");
            }
        }

        private void cmbReportingCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportingCategory.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbReportingCategory.Items[cmbReportingCategory.SelectedIndex])["sDescription"]), cmbReportingCategory) >= cmbReportingCategory.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbReportingCategory, Convert.ToString(((DataRowView)cmbReportingCategory.Items[cmbReportingCategory.SelectedIndex])["sDescription"]));//, cmbReportingCategory, 0, cmbReportingCategory.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbReportingCategory, "");

            }
        }





        private void cmbInsuranceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInsuranceType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsuranceType.Items[cmbInsuranceType.SelectedIndex])["sInsuranceTypeDesc"]), cmbInsuranceType) >= cmbInsuranceType.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbInsuranceType, Convert.ToString(((DataRowView)cmbInsuranceType.Items[cmbInsuranceType.SelectedIndex])["sInsuranceTypeDesc"]));//, cmbInsuranceType, 0, cmbInsuranceType.Bottom - 40);
                else
                    this.toolTip1.SetToolTip(cmbInsuranceType, "");

            }
        }

        private void cmbFeeSchedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFeeSchedules.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbFeeSchedules.Items[cmbFeeSchedules.SelectedIndex])["sFeeScheduleName"]), cmbFeeSchedules) >= cmbFeeSchedules.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbFeeSchedules, Convert.ToString(((DataRowView)cmbFeeSchedules.Items[cmbFeeSchedules.SelectedIndex])["sFeeScheduleName"]));//, cmbFeeSchedules, 0, cmbFeeSchedules.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbFeeSchedules, "");

            }
        }

        private void cmbCptCrosswalk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCptCrosswalk.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbCptCrosswalk.Items[cmbCptCrosswalk.SelectedIndex])["sCPTMappingName"]), cmbCptCrosswalk) >= cmbCptCrosswalk.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbCptCrosswalk, Convert.ToString(((DataRowView)cmbCptCrosswalk.Items[cmbCptCrosswalk.SelectedIndex])["sCPTMappingName"]));//, cmbCptCrosswalk, 0, cmbCptCrosswalk.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbCptCrosswalk, "");

            }
        }

        private void cmbClearingHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClearingHouse.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbClearingHouse.Items[cmbClearingHouse.SelectedIndex])["sClearingHouseName"]), cmbClearingHouse) >= cmbClearingHouse.DropDownWidth - 18)
                    this.toolTip1.SetToolTip(cmbClearingHouse, Convert.ToString(((DataRowView)cmbClearingHouse.Items[cmbClearingHouse.SelectedIndex])["sClearingHouseName"]));//, cmbClearingHouse, 0, cmbClearingHouse.Bottom - 98);
                else
                    this.toolTip1.SetToolTip(cmbClearingHouse, "");
            }
        }




        private void SaveExpandedClaimSettings()
        {


            try
            {
                string _strSQL = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                if (!oglocontact.IsenableUB04(_ClinicID))
                {
                    chkIsInstitutionalBilling.Checked = false;
                }
                oglocontact.Dispose();
                //


                oDB.Connect(false);

                _strSQL = "delete from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode() + " and nContactID = " + _ContactID;
                oDB.Execute_Query(_strSQL);

                oDB.Disconnect();
                oDB.Dispose();

                gloSettings.GeneralSettings ogloSettings = new GeneralSettings(_databaseconnectionstring);
                Int32 dCharges = 0;
                Int32 dDiagnoses = 0;
                //Int16 dCharges = (Int16)numup_dn_ChargesPerClaim.Value;
                //Int16 dDiagnoses = (Int16)numup_dn_DiagnosisPerClaim.Value;
                if (TxtChargesperClaim.Text == "")
                {
                    dCharges = 0;
                }
                else
                {
                    dCharges = Convert.ToInt32(TxtChargesperClaim.Text);
                }
                if (TxtDiagnosisperClaim.Text == "")
                {
                    dDiagnoses = 0;
                }
                else
                {
                    dDiagnoses = Convert.ToInt32(TxtDiagnosisperClaim.Text);
                }
                if ((cmbTypeOFBilling.Text == "") && (chkIsInstitutionalBilling.Checked == false))
                {
                    ogloSettings.AddExpandedClaimSettings(0, 0, _ContactID, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.Paper.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                    ogloSettings.AddExpandedClaimSettings(0, 0, _ContactID, Convert.ToInt16(gloSettings.AlternateIDSettingLevel.InsurancePlan.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.Electronic.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "") && (chkIsInstitutionalBilling.Checked == true))
                {
                    ogloSettings.AddExpandedClaimSettings(0, 0, _ContactID, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.UB04Paper.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                    ogloSettings.AddExpandedClaimSettings(0, 0, _ContactID, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.UB04Electronic.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == true))
                {
                    ogloSettings.AddExpandedClaimSettings(0, 0, _ContactID, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.UB04Electronic.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == true))
                {
                    ogloSettings.AddExpandedClaimSettings(0, 0, _ContactID, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.UB04Paper.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == false))
                {
                    ogloSettings.AddExpandedClaimSettings(0, 0, _ContactID, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.Electronic.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == false))
                {
                    ogloSettings.AddExpandedClaimSettings(0, 0, _ContactID, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.Paper.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                ogloSettings.Dispose();
                ogloSettings = null;

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }

        }

        private void GetExpandedClaimSettings()
        {
            string _strSQL = string.Empty;
            try
            {

                gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                if (!oglocontact.IsenableUB04(_ClinicID))
                {
                    chkIsInstitutionalBilling.Checked = false;
                }
                oglocontact.Dispose();
                //

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtClaim = null;
                oDB.Connect(false);

                _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode() + " and nContactID = " + _ContactID; //+ " and nSettingType = " + gloSettings.TypeOfBilling.Paper.GetHashCode();

                //if ((cmbTypeOFBilling.Text == "") && (chkIsInstitutionalBilling.Checked == false))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode() + " and nContactID = " + _ContactID + " and nSettingType = " + gloSettings.TypeOfBilling.Paper.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "") && (chkIsInstitutionalBilling.Checked == true))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode() + " and nContactID = " + _ContactID + " and nSettingType = " + gloSettings.TypeOfBilling.UB04Paper.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == true))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode() + " and nContactID = " + _ContactID + " and nSettingType = " + gloSettings.TypeOfBilling.UB04Electronic.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == true))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode() + " and nContactID = " + _ContactID + " and nSettingType = " + gloSettings.TypeOfBilling.UB04Paper.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == false))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode() + " and nContactID = " + _ContactID + " and nSettingType = " + gloSettings.TypeOfBilling.Electronic.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == false))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsurancePlan.GetHashCode() + " and nContactID = " + _ContactID + " and nSettingType = " + gloSettings.TypeOfBilling.Paper.GetHashCode();
                //}

                oDB.Retrive_Query(_strSQL, out dtClaim);

                if (dtClaim != null && dtClaim.Rows.Count > 0)
                {

                    if (dtClaim.Rows[0]["nServiceLines"].ToString() == "0") { TxtChargesperClaim.Text = ""; } else { TxtChargesperClaim.Text = dtClaim.Rows[0]["nServiceLines"].ToString(); }
                    if (dtClaim.Rows[0]["nDiagnosis"].ToString() == "0") { TxtDiagnosisperClaim.Text = ""; } else { TxtDiagnosisperClaim.Text = dtClaim.Rows[0]["nDiagnosis"].ToString(); }

                }
                else
                {
                    TxtChargesperClaim.Text = "";
                    TxtDiagnosisperClaim.Text = "";
                }
                oDB.Disconnect();
                oDB.Dispose();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
            finally
            {
                _strSQL = null;
            }
        }

        private bool ValidateExpClaimData()
        {
            Int32 dCharges = 0;
            Int32 dDiagnoses = 0;
            bool bClaimVersion4010_5010 = false;
            string sMessage = string.Empty;
            
            gloSettings.GeneralSettings ogloSettings = new GeneralSettings(_databaseconnectionstring);
            //Read Batch Claim Setting
            if (ogloSettings.getANSIVersion(_ContactID, "CLAIM", _ClinicID) == 2)
            {
                bClaimVersion4010_5010 = true;
            }
            if (ogloSettings != null) { ogloSettings.Dispose(); }

            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            if (!oglocontact.IsenableUB04(_ClinicID))   //|| chkIsInstitutionalBilling.Visible == false
            {
                chkIsInstitutionalBilling.Checked = false;
            }
            oglocontact.Dispose();
            //



            if (TxtChargesperClaim.Text == "")
            {
                dCharges = 0;
            }
            else
            {
                dCharges = Convert.ToInt32(TxtChargesperClaim.Text);
            }
            if (TxtDiagnosisperClaim.Text == "")
            {
                dDiagnoses = 0;
            }
            else
            {
                dDiagnoses = Convert.ToInt32(TxtDiagnosisperClaim.Text);
            }
            if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == true))
            {
                //Institutional Edi
                if (dCharges > 999)
                {
                    //if (bClaimVersion4010_5010 == false)
                    //{
                    sMessage = "System limits Institutional Electronic Claims (837I 4010) to 999 service lines. ";
                    //}
                    //else
                    //{
                    //    sMessage = "System limits Institutional Electronic Claims (837I 5010) to 999 service lines. ";
                    //}
                    if (DialogResult.Cancel == (MessageBox.Show(sMessage, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtChargesperClaim.Focus();
                        return false;
                    }
                }

                if (dDiagnoses > 18)
                {
                    //if (bClaimVersion4010_5010 == false)
                    //{
                    sMessage = "System limits Institutional Electronic Claims (837I 4010) to 18 diagnoses. ";
                    //}
                    //else
                    //{
                    //    sMessage = "System limits Institutional Electronic Claims (837I 5010) to 18 diagnoses. ";
                    //}

                    if (DialogResult.Cancel == (MessageBox.Show(sMessage, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtDiagnosisperClaim.Focus();
                        return false;
                    }
                }
            }
            // else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == true))
            //else if (((cmbTypeOFBilling.Text == "Paper") || (cmbTypeOFBilling.Text == "")) && (chkIsInstitutionalBilling.Checked == true)) //If default billing method is blank then take paper validations
            //{
            //    if (dCharges > 22)
            //    {
            //        if (DialogResult.Cancel == (MessageBox.Show("System limits CMS1450 to 22 service lines. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
            //        {
            //            TxtChargesperClaim.Focus();
            //            return false;
            //        }
            //    }

            //    if (dDiagnoses > 18)
            //    {
            //        if (DialogResult.Cancel == (MessageBox.Show("System limits CMS1450 to 18 diagnoses. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
            //        {
            //            TxtDiagnosisperClaim.Focus();
            //            return false;
            //        }
            //    }
            //}
            else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == false))
            {
                //if (dDiagnoses > 8)
                //{
                //    if (bClaimVersion4010_5010 == false)
                //    {
                //        sMessage = "Electronic Claims (837P 4010) may only display up to 8 diagnoses.  ";
                //    }
                //    else
                //    {
                //        sMessage = "Electronic Claims (837P 5010) may only display up to 8 diagnoses.  ";
                //    }

                //    if (DialogResult.Cancel == (MessageBox.Show(sMessage, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                //    {
                //        TxtDiagnosisperClaim.Focus();
                //        return false;
                //    }
                //}

                if (bClaimVersion4010_5010 == false)
                {
                    if (dDiagnoses > 8)
                    {
                        sMessage = "Electronic Claims (837P 4010) may only display up to 8 diagnoses.  ";
                        if (DialogResult.Cancel == (MessageBox.Show(sMessage, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                        {
                            TxtDiagnosisperClaim.Focus();
                            return false;
                        }
                    }

                }
                else
                {
                    if (dDiagnoses > 12)
                    {
                        sMessage = "Electronic Claims (837P 5010) may only display up to 12 diagnoses.  ";
                        if (DialogResult.Cancel == (MessageBox.Show(sMessage, _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                        {
                            TxtDiagnosisperClaim.Focus();
                            return false;
                        }
                    }
                }


            }
            // else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == false))
                  
            else if (((cmbTypeOFBilling.Text == "Paper") || (cmbTypeOFBilling.Text == "")) && (chkIsInstitutionalBilling.Checked == false)) //If default billing method is blank then take paper validations
            {
                Int32 _nPaperVersion = CheckPaperVersion(ContactID);
                if (dCharges > 6)
                {
                    if (_nPaperVersion == PaperFormVersion.CMS1500.GetHashCode())
                    {
                        if (DialogResult.Cancel == (MessageBox.Show("CMS1500 08/05 may only display up to 6 service lines. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                        {
                            TxtChargesperClaim.Focus();
                            return false;
                        }
                    }

                    else if (_nPaperVersion == PaperFormVersion.CMS1500New.GetHashCode())
                    {
                        if (DialogResult.Cancel == (MessageBox.Show("CMS1500 02/12 may only display up to 6 service lines. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                        {
                            TxtChargesperClaim.Focus();
                            return false;
                        }
                    }
                }

                               
              
                if (_nPaperVersion == PaperFormVersion.CMS1500.GetHashCode())
	            {
		 
                if (dDiagnoses > 4)
                {
                    if (DialogResult.Cancel == (MessageBox.Show("CMS1500 08/05 may only display up to 4 diagnoses. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtDiagnosisperClaim.Focus();
                        return false;
                    }
                } 
               }
                else if (_nPaperVersion == PaperFormVersion.CMS1500New.GetHashCode())
                {
                    if (dDiagnoses > 12)
                    {
                        if (DialogResult.Cancel == (MessageBox.Show("CMS1500 02/12 may only display up to 12 diagnoses. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                        {
                            TxtDiagnosisperClaim.Focus();
                            return false;
                        }
                    } 
                }

            }
            sMessage = null;
            return true;
        }


        private void TxtChargesperClaim_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;


            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {

                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {

                    if (e.KeyCode != Keys.Back | e.KeyCode == Keys.Decimal)
                    {
                        nonNumberEntered = true;
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }

        }

        private void TxtChargesperClaim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }

        private void TxtChargesperClaim_Validating(object sender, CancelEventArgs e)
        {
            //if (TxtChargesperClaim.Text != "")
            //{
            //    if ((Convert.ToInt16(TxtChargesperClaim.Text) < 6 || Convert.ToInt16(TxtChargesperClaim.Text) > 30) && Convert.ToInt16(TxtChargesperClaim.Text) != 0)
            //    {
            //        //TxtChargesperClaim.Text = Convert.ToString(6);
            //        MessageBox.Show("Max charges per claim should not be less than 6 and greater than 30.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        e.Cancel = true;
            //    }
            //}
        }

        private void TxtDiagnosisperClaim_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;


            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {

                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {

                    if (e.KeyCode != Keys.Back | e.KeyCode == Keys.Decimal)
                    {
                        nonNumberEntered = true;
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }

        }

        private void TxtDiagnosisperClaim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }

        private void TxtDiagnosisperClaim_Validating(object sender, CancelEventArgs e)
        {
            //if (TxtDiagnosisperClaim.Text != "")
            //{
            //    if ((Convert.ToInt16(TxtDiagnosisperClaim.Text) < 4 || Convert.ToInt16(TxtDiagnosisperClaim.Text) > 8) && Convert.ToInt16(TxtDiagnosisperClaim.Text) != 0)
            //    {
            //       // TxtDiagnosisperClaim.Text = Convert.ToString(4);
            //        MessageBox.Show("Max diagnosis per claim should not be less than 4 and greater than 8.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        e.Cancel = true;
            //    }
            //}
        }

        private void cmbBox29_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                combo = (ComboBox)sender;

                if (cmbBox29.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBox29.Items[cmbBox29.SelectedIndex])["sBOX29"]), cmbBox29) >= cmbBox29.DropDownWidth - 60)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        string temp = Convert.ToString(((DataRowView)cmbBox29.Items[cmbBox29.SelectedIndex])["sBOX29"]);
                        this.toolTip1.SetToolTip(cmbBox29, Convert.ToString(((DataRowView)cmbBox29.Items[cmbBox29.SelectedIndex])["sBOX29"]));
                    }
                    else
                    {
                        this.toolTip1.Hide(cmbBox29);
                    }
                }
                else
                {
                    this.toolTip1.Hide(cmbBox29);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void cmbInsEligibilityPrimProvType_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                if (cmbInsEligibilityPrimProvType.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsEligibilityPrimProvType.Items[cmbInsEligibilityPrimProvType.SelectedIndex])["sInsEligibilityProvPrimType"]), cmbInsEligibilityPrimProvType) >= cmbInsEligibilityPrimProvType.DropDownWidth - 12)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        string temp = Convert.ToString(((DataRowView)cmbInsEligibilityPrimProvType.Items[cmbInsEligibilityPrimProvType.SelectedIndex])["sInsEligibilityProvPrimType"]);
                        this.toolTip1.SetToolTip(cmbInsEligibilityPrimProvType, Convert.ToString(((DataRowView)cmbInsEligibilityPrimProvType.Items[cmbInsEligibilityPrimProvType.SelectedIndex])["sInsEligibilityProvPrimType"]));
                    }
                    else
                    {
                        this.toolTip1.Hide(cmbInsEligibilityPrimProvType);
                    }
                }
                else
                {
                    this.toolTip1.Hide(cmbInsEligibilityPrimProvType);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }


        }

        private void cmbInsEligibilitySecProvType_MouseEnter(object sender, EventArgs e)
        {
            try
            {


                if (cmbInsEligibilitySecProvType.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsEligibilitySecProvType.Items[cmbInsEligibilitySecProvType.SelectedIndex])["sInsEligibilityProvSecType"]), cmbInsEligibilitySecProvType) >= cmbInsEligibilitySecProvType.DropDownWidth - 12)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        string temp = Convert.ToString(((DataRowView)cmbInsEligibilitySecProvType.Items[cmbInsEligibilitySecProvType.SelectedIndex])["sInsEligibilityProvSecType"]);
                        this.toolTip1.SetToolTip(cmbInsEligibilitySecProvType, Convert.ToString(((DataRowView)cmbInsEligibilitySecProvType.Items[cmbInsEligibilitySecProvType.SelectedIndex])["sInsEligibilityProvSecType"]));
                    }
                    else
                    {
                        this.toolTip1.Hide(cmbInsEligibilitySecProvType);
                    }
                }
                else
                {
                    this.toolTip1.Hide(cmbInsEligibilitySecProvType);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }

        }

        private string FormatString(string _Value)
        {
            string _result = "";
            _result = Convert.ToString(_Value).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "");
            return _result;
        }

        private void dgMasters_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Int64 ID = 0;
            if (e.RowIndex > -1)
            {
                ID = Convert.ToInt64(dgMasters.Rows[e.RowIndex].Cells[0].Value);
                frmAlternatePayerID ofrm = new frmAlternatePayerID(_databaseconnectionstring, _ContactID, ID, true, e.RowIndex);
                ofrm.IsModified = _IsModified;
                ofrm.ObjAlternatePayerIDs = _objAlternatePayerIDs;
                ofrm.ShowDialog(this);
                _objAlternatePayerIDs = ofrm.ObjAlternatePayerIDs;
                _IsModified = ofrm.IsModified;
                FillAlternatePayerID();
                if (ofrm != null)
                    ofrm.Dispose();
            }
        }

        private void ts_btnNewAlternateID_Click(object sender, EventArgs e)
        {
            frmAlternatePayerID ofrm = new frmAlternatePayerID(_databaseconnectionstring, _ContactID, 0);
            ofrm.ObjAlternatePayerIDs = _objAlternatePayerIDs;
            ofrm.IsModified = _IsModified;
            ofrm.ShowDialog(this);
            _objAlternatePayerIDs = ofrm.ObjAlternatePayerIDs;
            _IsModified = ofrm.IsModified;
            if (ofrm != null)
                ofrm.Dispose();
            FillAlternatePayerID();
        }

        private void ts_btnEditAlternateID_Click(object sender, EventArgs e)
        {
            Int64 ID = 0;
            if (dgMasters.Rows.Count != 0 && dgMasters.SelectedRows.Count > 0)
            {
                ID = Convert.ToInt64(dgMasters.SelectedRows[0].Cells[0].Value.ToString());
                frmAlternatePayerID ofrm = new frmAlternatePayerID(_databaseconnectionstring, _ContactID, ID, true, dgMasters.SelectedRows[0].Index);
                ofrm.ObjAlternatePayerIDs = _objAlternatePayerIDs;
                ofrm.IsModified = _IsModified;
                ofrm.ShowDialog(this);
                _objAlternatePayerIDs = ofrm.ObjAlternatePayerIDs;
                _IsModified = ofrm.IsModified;
                if (ofrm != null)
                    ofrm.Dispose();
            }

            FillAlternatePayerID();
        }

        private void ts_btnDeleteAlternateID_Click(object sender, EventArgs e)
        {
            Int64 ID = 0;
            if (dgMasters.Rows.Count != 0 && dgMasters.SelectedRows.Count > 0)
            {
                int i = 0;
                ID = Convert.ToInt64(dgMasters.SelectedRows[0].Cells[0].Value.ToString());
                //  _arrAlternateID 

                if (MessageBox.Show("Are you sure you want to delete this record?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    _objAlternatePayerIDs.RemoveAt(dgMasters.SelectedRows[0].Index);
                    _arrAlternatePayerID.Add(ID);
                    _IsModified = true;
                    for (i = 0; i < dgMasters.Rows.Count; i++)
                        if (dgMasters.Rows[i].Selected == true)
                            break;

                }
                if (dgMasters.Rows.Count > 0 && dgMasters.Rows.Count == i)
                {
                    dgMasters.Rows[0].Selected = true;
                }
            }
            FillAlternatePayerID();
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            if (txtname.Text.Trim() != "")
            {
                this.Text = "Setup Insurance Plan" + " [" + txtname.Text.Trim() + "]";
            }
            else
            {
                this.Text = "Setup Insurance Plan";
            }
        }

        private void chkIncludeTaxRenPaper_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncludeTaxBilPaper.Checked == true || chkIncludeTaxRenPaper.Checked == true)
            {
                chkIncludeTax4Paper.Checked = true;
            }
            else
            {
                try
                {
                    this.chkIncludeTax4Paper.CheckedChanged -= new System.EventHandler(this.chkIncludeTax4Paper_CheckedChanged);
                    chkIncludeTax4Paper.Checked = false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    this.chkIncludeTax4Paper.CheckedChanged += new System.EventHandler(this.chkIncludeTax4Paper_CheckedChanged);
                }
            }

            if (chkIncludeTax4Paper.Checked == false)
            {
                chkIncludeTaxBilPaper.Enabled = false;
                chkIncludeTaxRenPaper.Enabled = false;
            }

        }

        private void chkIncludeTax4Paper_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkIncludeTaxRenPaper.CheckedChanged -= new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);
                this.chkIncludeTaxBilPaper.CheckedChanged -= new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);

                if (chkIncludeTax4Paper.Checked == true)
                {
                    chkIncludeTaxBilPaper.Checked = true;
                    chkIncludeTaxRenPaper.Checked = true;
                    chkIncludeTaxBilPaper.Enabled = true;
                    chkIncludeTaxRenPaper.Enabled = true;
                }
                else
                {
                    chkIncludeTaxBilPaper.Checked = false;
                    chkIncludeTaxRenPaper.Checked = false;
                    chkIncludeTaxBilPaper.Enabled = false;
                    chkIncludeTaxRenPaper.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                this.chkIncludeTaxRenPaper.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);
                this.chkIncludeTaxBilPaper.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);
            }
        }

        private void chkIncludeTax4Elec_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.chkIncludeTaxRenElec.CheckedChanged -= new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);
                this.chkIncludeTaxBillElec.CheckedChanged -= new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);

                if (chkIncludeTax4Elec.Checked == true)
                {
                    chkIncludeTaxBillElec.Checked = true;
                    chkIncludeTaxRenElec.Checked = true;
                    chkIncludeTaxBillElec.Enabled = true;
                    chkIncludeTaxRenElec.Enabled = true;
                }
                else
                {
                    chkIncludeTaxBillElec.Checked = false;
                    chkIncludeTaxRenElec.Checked = false;
                    chkIncludeTaxBillElec.Enabled = false;
                    chkIncludeTaxRenElec.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                this.chkIncludeTaxRenElec.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);
                this.chkIncludeTaxBillElec.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);
            }
        }

        private void chkIncludeTaxRenElec_CheckedChanged(object sender, EventArgs e)
        {

            if (chkIncludeTaxBillElec.Checked == true || chkIncludeTaxRenElec.Checked == true)
            {
                chkIncludeTax4Elec.Checked = true;
            }
            else
            {
                try
                {
                    this.chkIncludeTax4Elec.CheckedChanged -= new System.EventHandler(this.chkIncludeTax4Elec_CheckedChanged);
                    chkIncludeTax4Elec.Checked = false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    this.chkIncludeTax4Elec.CheckedChanged += new System.EventHandler(this.chkIncludeTax4Elec_CheckedChanged);
                }
            }
            if (chkIncludeTax4Elec.Checked == false)
            {
                chkIncludeTaxBillElec.Enabled = false;
                chkIncludeTaxRenElec.Enabled = false;
            }
        }

        private void TxtEligibiltyWebste_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtEligibiltyWebste.Text))
            {
                if (CheckURL(TxtEligibiltyWebste.Text) == false)
                {
                    MessageBox.Show("Please enter a valid website ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
            }
        }

        private void chkBillEpsdtFamPlan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBillEpsdtFamPlan.Checked == true)
            {
                chkIncludeSV.Checked = true;
                chkIncludeCRC.Checked = true;
                chkIncludeRefCode.Checked = true;
            }
            else
            {
                chkIncludeSV.Checked = false;
                chkIncludeCRC.Checked = false;
                chkIncludeRefCode.Checked = false;
            }
        }

        private void txtBaseUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                e.Handled = true;
            }
        }


        private void chkWorkersComp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWorkersComp.Checked == false)
            {
                if (IsWorkCompOrCompayOn(_nInsuranceID, _nPatientID))
                {
                    MessageBox.Show("Warning:" + Environment.NewLine + "Patient Insurances associated to Insurance Plan has Worker Comp information filled out.Turning the setting " + "OFF" + " will not show " + Environment.NewLine + "the Worker Comp details and will keep reporting the worker comp information on claims.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        private bool IsWorkCompOrCompayOn(Int64 nInsuranceID, Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _Query = "";
            DataTable _dt = null;
            try
            {
                oDb.Connect(false);
                _Query = "SELECT ISNULL( bIsCompnay,0) AS bIsCompnay ,ISNULL(bworkerscomp,0) AS bworkerscomp FROM dbo.PatientInsurance_DTL WHERE nInsuranceID=" + nInsuranceID + " and nPatientID=" + nPatientID;
                oDb.Retrive_Query(_Query, out _dt);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(_dt.Rows[0]["bIsCompnay"]) || Convert.ToBoolean(_dt.Rows[0]["bworkerscomp"]))
                    {
                        return true;
                    }
                    else
                        return false;
                }


            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                Ex = null;
            }
            finally
            {
                _Query = null;
                if (_dt != null) { _dt.Dispose(); _dt = null; }
                if (oDb != null) { oDb.Disconnect(); oDb.Dispose(); oDb = null; }
            }
            return false;
        }

        private void cmbUBBlngprvdraltID_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                combo = (ComboBox)sender;

                if (cmbUBBlngprvdraltID.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbUBBlngprvdraltID.Items[cmbUBBlngprvdraltID.SelectedIndex])["sAdditionalDescription"]), cmbUBBlngprvdraltID) >= cmbUBBlngprvdraltID.DropDownWidth - 60)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        string temp = Convert.ToString(((DataRowView)cmbUBBlngprvdraltID.Items[cmbUBBlngprvdraltID.SelectedIndex])["sAdditionalDescription"]);
                        this.toolTip1.SetToolTip(cmbUBBlngprvdraltID, Convert.ToString(((DataRowView)cmbUBBlngprvdraltID.Items[cmbUBBlngprvdraltID.SelectedIndex])["sAdditionalDescription"]));
                    }
                    else
                    {
                        this.toolTip1.Hide(cmbUBBlngprvdraltID);
                    }
                }
                else
                {
                    this.toolTip1.Hide(cmbUBBlngprvdraltID);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }



        public Int32 CheckPaperVersion(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();
            Int32 nPaperVersion = 0;

            try
            {
                oDBPara.Add("@nContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                nPaperVersion = Convert.ToInt32(oDB.ExecuteScalar("gsp_CheckPaperVersion", oDBPara));
            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oDBPara != null) { oDBPara.Dispose(); }
            }
            return nPaperVersion;

        }

        private void rbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNone.Checked == true)
            {
                rbNone.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbNone.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbSend_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSend.Checked == true)
            {
                rbSend.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbSend.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbDoNotSend_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDoNotSend.Checked == true)
            {
                rbDoNotSend.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbDoNotSend.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void chkIncludePrimaryDxInBox69_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt81AQual_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        private void txt81BQual_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        private void txt81CQual_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        private void txt81DQual_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

      
       
    }
}
