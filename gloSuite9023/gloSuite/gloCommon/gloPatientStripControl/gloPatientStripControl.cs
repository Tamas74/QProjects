 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gloPatient;
using gloSecurity;
using C1.Win.C1FlexGrid;
using gloDateMaster;


namespace gloPatientStripControl
{
    /// <summary>
    /// Anil 20070114
    /// Patient User Control to show Patient Details
    /// </summary>
    /// 
    //Enumerating the fields shown in the Patient User Control
    public enum PatientInfo
    { 
        Gender = 0,
        HandDominance = 1,
        Occupation = 2,
        HomePhone = 3,
        CellPhone = 4,
        ReferralPhysican = 5,
        PharmacyName = 6,
        PharmacyPhone = 7,
        PharmacyFax = 8,
        PrimaryInsurance = 9,
        SecondaryInsurance = 10,
        SSN = 11,
        ProviderName=12,
        DateofBirth=13,
        PatientCode=14,
        Gaurantor=15
        
    }

    //If Chaged also make chage at gloPatient,gloBilling
    public enum InsuranceTypeFlag
    {
        None = 0,
        Primary = 1,
        Secondary = 2,
        Tertiary = 3
    }
    //If Chaged also make chage at gloPatient,gloBilling
               
    //Enumerating the Forms where the Patient Strip is to be shown.
    public enum FormName
    { 
        None=0,
        Schedule=1,
        Billing=2,
        Temp=3,
        Appointment=4,
        NewCharges=5,
        CMS1500=6,
        UB04=7
    }

    public enum InsuracePaymentType
    {
        None = 0, Patient = 1, Insurace = 2, Charges = 3
    }
   

    public partial class gloPatientStripControl : UserControl
    {

        public class AgeDetail
        {
            public string Age = "";  
            public Int16 Years;
            public Int16 Months;
            public Int16 Days;
        }

        #region Constructors
        //private string _StrPatientAlert = "";
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings; 

        public gloPatientStripControl()
        {
            InitializeComponent();

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            _Age = new AgeDetail();

            //Added By Pramod Nair For Messagebox Caption 
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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion
        }

        public gloPatientStripControl(string connectionstring)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            _Age = new AgeDetail();

            //Added By Pramod Nair For Messagebox Caption 
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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion
        }

        public gloPatientStripControl(string connectionstring, Boolean AllowPatientSearch)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;
            _AllowPatientSearch =AllowPatientSearch ;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            _Age = new AgeDetail();

            //Added By Pramod Nair For Messagebox Caption 
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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion
        }

        public gloPatientStripControl(string connectionstring, Boolean AllowPatientSearch, Boolean AllowClaimNoSearch)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;
            _AllowPatientSearch = AllowPatientSearch;
            _AllowClaimNoSearch = AllowClaimNoSearch;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            _Age = new AgeDetail();

            //Added By Pramod Nair For Messagebox Caption 
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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion
        }

        public gloPatientStripControl(string connectionstring, Boolean AllowPatientSearch, Boolean AllowClaimNoSearch, Boolean IsHCFA1500)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;
            _AllowPatientSearch = AllowPatientSearch;
            _AllowClaimNoSearch = AllowClaimNoSearch;
            _IsHCFA1500 = IsHCFA1500;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            _Age = new AgeDetail();

            //Added By Pramod Nair For Messagebox Caption 
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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion

        }

        public gloPatientStripControl(string connectionstring, Boolean AllowPatientSearch, Boolean AllowClaimNoSearch, Boolean IsHCFA1500,bool DefaultToClaimNoSearch)
        {
            InitializeComponent();
            _DatabaseConnectionString = connectionstring;
            _AllowPatientSearch = AllowPatientSearch;
            _AllowClaimNoSearch = AllowClaimNoSearch;
            _IsHCFA1500 = IsHCFA1500;
            _DefaultClaimNoSearch = DefaultToClaimNoSearch;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            _Age = new AgeDetail();

            //Added By Pramod Nair For Messagebox Caption 
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

            #region "UserId And UserName"

            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }

            #endregion

            chk_ClaimNoSearch.Checked = DefaultToClaimNoSearch;
        }

        #endregion

        #region Private Variables

        DataView dvPatient = new DataView ();
        
        DataView dvNext = new DataView();

        //Size
        Int32 StripHeight = 110 ; //95;
        Int32 MainPanelHeight = 56;//65;


        //private string _MessageBoxCaption = "gloPM";
        private string _MessageBoxCaption = String.Empty;

        private string _DatabaseConnectionString = "";
        private Boolean _AllowPatientSearch = true;
        private Boolean _AllowClaimNoSearch = false;
        private Boolean _IsHCFA1500 = false;
        private Boolean _DefaultClaimNoSearch = false; 
        //Patient Fields
        private Int64 _PatientID = 0;
        private Int64 _TransactionID = 0;
        private Int64 _TransactionMasterID = 0;
        private string _PatientCode = "";
        private string _PatientName = "";
        private string _ProviderName = "";
        private string _PatientHomePhone = "";
        private string _Gender = "";
        private string _Guarantor = "";
        private string _PatientCellPhone = "";
        private string _PatientsMaritalStatus = "";

        private string _PatientAge = "";
        private Int64 _ProviderID = 0;

        private Int64 _ClaimNumber = 0;
        private Int64 _SubClaimNumber = 0;
        private string _ClaimSubClaimNo = "";

        private AgeDetail _Age;

        private DateTime _DateOfBirth;
        private DateTime _TransactionDate = DateTime.Now;

        
        //private string _PrimaryInsurance = "";
        //private string _SecondaryInsurance = "";
        //private string _HandDominance = "";
        //private string _ReferralPhysician = "";
        //private string _SSN = "";
        //private string _PatientOccupation = "";
        //private string _PharmacyName = "";
        //private string _PharmacyPhone = "";
        //private string _PharmacyFax = "";


        //Private boolean variables to hide or show panels
        private bool _HideAllPanels = false;

        //private bool _HidePatientCode = true;
        //private bool _HidePatientName = true;
       // private bool _HideProviderName = true;
        private bool _HidePatientHomePhone = true;
        private bool _HideGender = true;
        
        private bool _HideGuarantor = true;

        private bool _ShowTotalBalance = false;
        private bool _ShowInsuraces = false;
        private bool _ShowAlerts = false;
        private bool _ShowNotesAlerts = false;

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

        //Added By Pramod Nair For UserRights
        private Int64 _UserID = 0;
        private string _UserName = "";
       

        private bool _viewSearchOptionCheckBox = true;
        private InsuracePaymentType _InsuracePaymentType = InsuracePaymentType.None;

        //MaheshB For Appointment

        private FormName _FormName = FormName.None;//Check  Comment To Defination.
        
        private int _selectedPartyNo = -1;

        private Boolean _IsSearchOnPatientCode = false;

        private bool _HasSecondaryInsOnClaim = false;
        private bool _bBtn_ModityPatient = true;
      
        ToolTip btnToolTip;
        ToolTip patintNameToolTip;
        #endregion

        #region Properties

        bool _AllowEditingParty = true;
        public bool AllowEditingParty
        {
            get { return _AllowEditingParty; }
            set { _AllowEditingParty = value; }
        }
        
        public Int64  PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        public Int64 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        public Int64 TransactionMasterID
        {
            get { return _TransactionMasterID; }
            set { _TransactionMasterID = value; }
        }

        public  string PatientCode
        {
            get { return _PatientCode; }
            set { _PatientCode = value; }
        }

        public bool DTPEnabled
        {
            get { return dtpDate.Enabled; }
            set { dtpDate.Enabled = value; }
        }

        public DateTime DTPValue
        {
            get { return dtpDate.Value; }
            set { dtpDate.Value = value; }
        }

        public bool btnUpEnable
        {
            get { return btnUP.Enabled; }
            set { btnUP.Enabled = value; }
        }

        public bool btnModityPatientEnable
        {
            get { return _bBtn_ModityPatient; }
            set { _bBtn_ModityPatient = value; }
        }

        public bool btnDownEnable
        {
            get { return btnDown.Enabled; }
            set { btnDown.Enabled = value; }
        }

        public string Provider
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }

        public DateTimePicker DTP
        {
            get { return dtpDate; }
            set 
            {
                try
                {
                    if (dtpDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDate);
                        }
                        catch
                        {
                        }
                        dtpDate.Dispose();
                        dtpDate = null;
                    }
                }
                catch
                {
                }

                dtpDate = value; 
            }
        }

        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }

        public Int64 ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }

        public AgeDetail PatientAge
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public DateTime PatientDateOfBirth
        {
            get { return _DateOfBirth; }       
            set { _DateOfBirth = value; }
        }

        public DateTime TransactionDate
        {
            get
            {
                _TransactionDate = dtpDate.Value;
                return _DateOfBirth;
            }
            set
            {
                value = DateTime.Now;
                try
                {
                    if (value.ToString() == "#12:00:00 AM#")
                        value = DateTime.Now;
                    else if (value.ToString() =="1/1/001 12:00:00 AM")
                        value = DateTime.Now;
                }
                catch (Exception) // ex)
                {
                    //ex.ToString();
                    //ex = null;
                    value = DateTime.Now;
                }
                _TransactionDate = value;
                dtpDate.Value = _TransactionDate;
            }
         }

        public string PatientPhone
        {
            get { return _PatientHomePhone; }
            set { _PatientHomePhone = value; }
        }


        public string PatientCellPhone
        {
            get { return _PatientCellPhone; }
            set { _PatientCellPhone = value; }
        }

        public string PatientGender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }


        public string PatientsMaritalStatus
        {
            get { return _PatientsMaritalStatus; }
            set { _PatientsMaritalStatus = value; }
        }
       

        public string Guarantor
        {
            get { return _Guarantor; }
            set { _Guarantor = value; }        
        }
        public bool HideAllPanels
        {
            get { return _HideAllPanels; }
            set
            {
                _HideAllPanels = value;
                if (_HideAllPanels == false)
                {
                    pnlLeft.Visible = true;
                    this.Height = StripHeight;  // 96
                    pnlLeft.Height = MainPanelHeight; //64
                }
                else
                {
                    pnlLeft.Visible = true;
                }
             }
          }


        public bool HidePatientPhone
        {
            get { return _HidePatientHomePhone; }
            set
            {
                _HidePatientHomePhone = value;
                if (_HidePatientHomePhone == false)
                {
                    pnlPatientPhone.Visible = true;
                }
                else
                {
                    pnlPatientPhone.Visible = false;
                }
            }
        }

        public bool HideGender
        {
            get { return _HideGender; }
            set
            {
                _HideGender = value;
                if (_HideGender == false)
                {
                    pnlGender.Visible = true;
                }
                else
                {
                    pnlGender.Visible = false;
                }
            }
        }

        public bool HideGuarantor
        {
            get { return _HideGuarantor; }
            set
            {
                _HideGuarantor = value;
                if (_HideGuarantor == false)
                {
                    pnlGuarantor.Visible = true;
                }
                else
                {
                    pnlGuarantor.Visible = false;
                }
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

        public bool ShowAlerts
        {
            get { return pnlAlerts.Visible; }
            set
            {
                _ShowAlerts = value;
                pnlAlerts.Visible = value;
            }
        }
        public bool ShowNotesAlerts
        { 
            get{return pnlNotes_Alerts.Visible;}
            set
            {
                _ShowNotesAlerts =value;
                pnlNotes_Alerts.Visible=value;
            }
        }
       

        public Int64 ClaimNumber
        {
            get { return _ClaimNumber; }
            set { _ClaimNumber = value; }
        }

        public Int64 SubClaimNumber
        {
            get { return _SubClaimNumber; }
            set { _SubClaimNumber = value; }
        }

        public string ClaimSubClaimNo
        {
            get { return _ClaimSubClaimNo; }
            set { _ClaimSubClaimNo = value; }
        }

        public bool SearchOnClaimNumber
        {
            get { return chk_ClaimNoSearch.Checked; }
        }

        public bool ShowSearch
        {
            get { return txtPatientSearch.Visible; }
            set 
            { 
                txtPatientSearch.Visible = value;
                chk_ClaimNoSearch.Visible = value;  
            }
        }


        //To minimize and maximize the patient strip control  
        public Boolean isOpenedFromCharges { get; set; }


        public FormName formname
        {

            get { return _FormName; }
            set
            {
                _FormName = value;
            }
        }

        public void LoadClaim(Int64 ClaimNo)
        {
            _ClaimNumber = ClaimNo;
            txtPatientSearch.Text = _ClaimNumber.ToString();

            InstringSearch(txtPatientSearch.Text.Trim());
            KeyPressEventArgs e = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));
            Object sender = txtPatientSearch.Text.Trim();
            if (c1PatientDetails.Visible == false)
                OnPatientSearchKeyPress(sender,e);
            e = null;
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

        public void SetInsuracePaymentType(InsuracePaymentType mInsuracePaymentType)
        {
            _InsuracePaymentType = mInsuracePaymentType;
        }

        public void SetClaimNoSearch(bool ClaimNoCheckValue)
        {
            chk_ClaimNoSearch.Checked = ClaimNoCheckValue;
        }

        public bool ShowAddInsurancePlan
        {
            get { return btn_AddInsurancePlan.Visible; }
            set { btn_AddInsurancePlan.Visible = value; }
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

        public int SelectedPartyNo
        {
            get { return _selectedPartyNo; }
            set { _selectedPartyNo = value; }
        }

        public Boolean SearchOnPatientCode
        {
            get { return _IsSearchOnPatientCode; }
            set { _IsSearchOnPatientCode = value; }
        }

        public bool HasSecondaryInsOnClaim
        {
            get { return _HasSecondaryInsOnClaim; }
            set { _HasSecondaryInsOnClaim = value; }
        }



        #endregion

        #region DELEGATES & EVENTS

        public delegate void ControlSizeChanged(object sender, EventArgs e);
        public event ControlSizeChanged ControlSize_Changed;

        public delegate void DateValidated(object sender, EventArgs e);
        //public event DateValidated Date_Validated;

        public delegate void DateValueChanged1(object sender, EventArgs e);
        //public event DateValueChanged1 DateValue_Changed;

        public delegate void DateValidating(object sender, CancelEventArgs e);
       // public event EventHandler Date_Validating;

        public delegate void DateDropDown(object sender, EventArgs e);
        //public event DateDropDown Date_DropDown;

        public delegate void DateCloseUp(object sender, EventArgs e);
        //public event DateCloseUp Date_CloseUp;

        public delegate void txtPatientSearchTextChanged(object sender, EventArgs e);
    //    public event txtPatientSearchTextChanged txtPatientSearchTextChanged1;

        public delegate void PatientSearchKeyPressHandler(object sender, KeyPressEventArgs e);
        public event PatientSearchKeyPressHandler OnPatientSearchKeyPress;

        //public delegate void AfterPatientModified(object sender, EventArgs e);
        //public event AfterPatientModified After_PatientModified;

        public delegate void Patient_Modified(object sender, EventArgs e);
        public event Patient_Modified PatientModified;

        public delegate void Insurance_Selected(Int64 InsuranceID, string InsuranceName, Int32 InsuraceSelfMode, Int64 ContactID);
        public event Insurance_Selected InsuranceSelected;
        
        #endregion
        
        private void gloPatientStripControl_Load(object sender, EventArgs e)
        {
            try
            {
                //pnlLeft.Visible = true;
                //btnUP.Visible = true;
                btnUP.BackgroundImage = global::gloPatientStripControl.Properties.Resources.UP;
                btnUP.BackgroundImageLayout = ImageLayout.Center;

                btn_ModityPatient.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Patient;
                btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;
               
                txtPatientSearch.Visible = _AllowPatientSearch;
                chk_ClaimNoSearch.Visible = _AllowClaimNoSearch;

                if (_IsHCFA1500 == true)
                {
                    chk_ClaimNoSearch.Visible = false;
                    chk_ClaimNoSearch.Checked = _IsHCFA1500;
                    chk_ClaimNoSearch.Enabled = false;
                    lblSearchonClaimNo.Visible = true;
                }
                else
                {
                    if (_DefaultClaimNoSearch == true)
                    {
                        chk_ClaimNoSearch.Visible = true;
                        chk_ClaimNoSearch.Checked = _DefaultClaimNoSearch;
                        chk_ClaimNoSearch.Enabled = true;
                        lblSearchonClaimNo.Visible = false;
                    }
                }

                // Commented by Mahesh 20081011 coz Giving error
                //   ControlSize_Changed(sender,e);

                //*******************************************************
                //Commented By Debasish Das on 13th March 2010
                //Reason: method GetPatients() takes much time to load 
                //*******************************************************
                //DataTable dt = new DataTable();
                //dt = GetPatients();
                //if (dt != null)
                //{
                //    dvPatient = dt.DefaultView;
                //}
                //*************** Edds Here *********************************
                lblTodaysDate.Text = dtpDate.Value.Date.ToString("ddd, MMM dd yyyy");
               
                DesignPatientGrid();
                c1PatientDetails.Visible = false;

                if (_ShowInsuraces == true && _ShowTotalBalance == false)
                {
                    StripHeight = StripHeight + 35;
                }

                if (_ShowTotalBalance == true && _ShowInsuraces == false)
                {
                    StripHeight = StripHeight + 45;
                }

                if (_ShowTotalBalance == true && _ShowInsuraces == true)
                {
                    StripHeight = StripHeight + 45;
                }

                if (_ShowAlerts == true)
                {
                    StripHeight = StripHeight + pnlAlerts.Height;
                }

                if (_ShowInsuraces == false && _ShowTotalBalance == false && _ShowAlerts == false && _ShowNotesAlerts == false)
                {
                    StripHeight = StripHeight - 10;
                }

                this.Height = StripHeight;

                //This Condition is added in 6010 to disable modify patient button forcefully from Modifycharge form.
                if (btnModityPatientEnable)
                {
                    //Assign User Rights 20090720
                    AssignUserRights();
                }
                else
                {
                    btn_ModityPatient.Enabled = btnModityPatientEnable;
                }
                //***Ends Here***********************
               
                if (Convert.ToString(_FormName.GetHashCode()) == "4")
                {
                    //this.Width = 120;
                    //this.Height = 900;
                    //pnl_Main.Width = 0;
                    //pnlLeft.Dock = DockStyle.None;
                    //pnlLeft.Width = 7;
                }
               // gloC1FlexStyle.Style(c1Insurance, false);
                
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

        private void gloPatientStripControl_Paint(object sender, PaintEventArgs e)
        {
           
            //if (Convert.ToString(_FormName.GetHashCode()) == "4")
            //{
            //    pnlLeft.Width = 100;
            //    pnlRight.Width = 100;
            //}
            //else
            //{
                if (pnl_Main.Width > 0)
                {
                    pnlLeft.Width = pnl_Main.Width / 2;
                    pnlRight.Width = pnl_Main.Width - pnlLeft.Width;
                }
            //}
        }

        #region "Methods"
      
        public bool FillDetails(Int64 PatientID, FormName CallingFormName, Int64 nProviderid, bool blnflag)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_DatabaseConnectionString);
            bool _ShowPatient = true;
            try
            {

                if (pnl_Main.Width > 0)
                {
                    pnlLeft.Width = pnl_Main.Width / 2;
                    pnlRight.Width = pnl_Main.Width - pnlLeft.Width - 2;
                }

                _PatientID = PatientID;

                //------Check For Patient Lock Chart Status--------------------------

               
                if (CallingFormName != FormName.NewCharges 
                    && CallingFormName!=FormName.Appointment
                    && CallingFormName != FormName.CMS1500
                    && CallingFormName != FormName.UB04)
                {
                    //---This Method will Show Message box if Patient is Locked
                    if (oSecurity.isPatientLock(_PatientID, true) == true)
                    {
                        _ShowPatient = false;
                    }
                }
                //------Check For Patient Lock Chart Status--------------------------



                string _strQuery = "";

                //-- Do not display patient having status as LOCK CHART
                if (_ShowPatient == true)
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                    oDB.Connect(false);
                    DataTable dt = null;

                    _strQuery = "SELECT Patient.sPatientCode AS PatientCode, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') "
                      + " AS PatientName, Patient.dtDOB AS DOB, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) "
                      + " + ISNULL(Provider_MST.sLastName, '') AS PrName,  ISNULL(Patient.sPhone, '') AS PatPhone, ISNULL(Patient.sOccupation, '') "
                      + " AS PatientOccupation, ISNULL(Patient.sMobile, '') AS PatientCellPhone, ISNULL(Patient.nSSN, '') AS SSN, ISNULL(Patient.sGender, '') AS Gender, "
                      + " ISNULL(Patient.sMaritalStatus, '') AS sMaritalStatus, ISNULL(Patient.sHandDominance, '') AS HandDominance,ISNULL(Patient.sGuarantor,'') AS Guarantor, "
                      + " isnull(Provider_MST.sFirstName,'') + space(1) + isnull(Provider_MST.sMiddleName,'') + space(1) + isnull(Provider_MST.sLastName,'') AS ProviderName, isnull(Provider_MST.nProviderID,0) AS ProviderID "
                      + " FROM Patient WITH (NOLOCK) LEFT OUTER JOIN Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID "
                      + " WHERE Patient.nPatientID = " + _PatientID + " AND Patient.nClinicID = " + _ClinicID + "";

                    //dt = new DataTable();
                    oDB.Retrive_Query(_strQuery, out dt);

                    _strQuery = "";
                    _strQuery = "SELECT ISNULL(Patient_OtherContacts.sFirstName,'') + SPACE(1) + ISNULL(Patient_OtherContacts.sMiddleName,'') + SPACE(1)+ ISNULL(Patient_OtherContacts.sLastName,'') AS Guarantor "
                              + " FROM Patient WITH (NOLOCK) LEFT JOIN Patient_OtherContacts WITH (NOLOCK) ON Patient.nPatientID = Patient_OtherContacts.nPatientID "
                              + " WHERE Patient.nPatientID = " + _PatientID + " AND Patient.nClinicID =" + _ClinicID + " AND (Patient_OtherContacts.nPatientContactTypeFlag = 1 OR Patient_OtherContacts.nPatientContactTypeFlag  IS NULL )  ";
                    //DataTable dtGuarantor;
                    object _ResultGuarantor;
                    //dtGuarantor = new DataTable();

                    _ResultGuarantor = oDB.ExecuteScalar_Query(_strQuery);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        _PatientCode = dt.Rows[0]["PatientCode"].ToString();
                        _PatientName = dt.Rows[0]["PatientName"].ToString();
                        //_SSN = dt.Rows[0]["SSN"].ToString();
                        _DateOfBirth = Convert.ToDateTime(dt.Rows[0]["DOB"]);


                        _ProviderName = dt.Rows[0]["ProviderName"].ToString();
                        _ProviderID = Convert.ToInt64(dt.Rows[0]["ProviderID"].ToString());

                        //Code to get the Age from Date of Birth
                        // Int64 nMonths = 9;
                        string strAge = "";
                        strAge = FormatAge(_DateOfBirth);
                        if (_Age == null)
                        {
                            _Age = new AgeDetail();
                        }
                        _Age.Age = strAge;

                        _PatientAge = _Age.Age;
                        _PatientCellPhone = dt.Rows[0]["PatientCellPhone"].ToString();
                        _PatientHomePhone = dt.Rows[0]["PatPhone"].ToString();
                        _Gender = dt.Rows[0]["Gender"].ToString();
                        _Guarantor = Convert.ToString(_ResultGuarantor);

                        _PatientsMaritalStatus = dt.Rows[0]["sMaritalStatus"].ToString();

                        //Show Patient Details on control
                        lblAge.Text = _PatientAge;
                        //gloPM5076 Date Format issue.
                        lblDOB.Text = _DateOfBirth.ToString("MM/dd/yyyy");
                        lblGender.Text = _Gender;
                        lblPatientCode.Text = _PatientCode;
                        lblPatientName.Text = _PatientName;
                        lblPatNameNCode.Text = _PatientCode + " - " + _PatientName;
                        lblProvider.Text = _ProviderName;
                        lblPhone.Text = _PatientHomePhone;
                        lblGuarantor.Text = _Guarantor;

                        //Added By Debasish Das Bug ID: 6397**********************************
                        lblTodaysDate.Text = dtpDate.Value.Date.ToString("ddd, MMM dd yyyy");
                        //Ends Here***********************************************************

                    }
                    // Hide patient gender panel 1
                    pnlGender.Visible = false;
                    // Hide patient home phone panel 2
                    pnlPatientPhone.Visible = false;
                    // Hide patient occupation panel 3

                    if (HideGuarantor == true)
                    {
                        pnlGuarantor.Visible = false;
                    }
                    else
                    {
                        pnlGuarantor.Visible = true;
                    }


                    if (ShowInsurances == true) { FillPatientInsurances(_PatientID); }
                    FillPatientBalance(_PatientID);
                    FillAlerts_Notes();
                    FillClaimOnHold();
                    FillStatementNote();

                    if (CallingFormName == FormName.Billing)
                    {
                        txtPatientSearch.Text = "";
                    }
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                }
                else
                {
                    // Hide patient gender panel 1
                    pnlGender.Visible = false;
                    // Hide patient home phone panel 2
                    pnlPatientPhone.Visible = false;
                    // Hide patient occupation panel 3

                    if (HideGuarantor == true)
                    {
                        pnlGuarantor.Visible = false;
                    }
                    else
                    {
                        pnlGuarantor.Visible = true;
                    }

                    ClearControl(true);
                    c1Insurance.Clear();

                    _PatientID = 0;
                }

                if (btnDown.Visible == false)
                {
                    HidePatientDetailOnStrip(); 
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
                oSecurity.Dispose();  
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return _ShowPatient;
        }
           
        private void DesignPatientGrid()
        {
            try
            {
                //c1PatientDetails.Hide();
                
                c1PatientDetails.Rows.Count = 1;
                c1PatientDetails.Rows.Fixed = 1;
                c1PatientDetails.Cols.Count = 10;
                c1PatientDetails.Cols.Fixed = 0;

                c1PatientDetails.SetData(0, COL_PAT_ID, "Id");
                c1PatientDetails.SetData(0, COL_PAT_Code, "Code");
                c1PatientDetails.SetData(0, COL_PAT_FirstName, "First Name");
                c1PatientDetails.SetData(0, COL_PAT_MI, "MI");
                c1PatientDetails.SetData(0, COL_PAT_LastName, "Last Name");
                c1PatientDetails.SetData(0, COL_PAT_SSN, "SSN");
                c1PatientDetails.SetData(0, COL_PAT_Provider, "Provider");
                c1PatientDetails.SetData(0, COL_PAT_DOB, "DOB");
                c1PatientDetails.SetData(0, COL_PAT_Phone, "Phone");
                c1PatientDetails.SetData(0, COL_PAT_Mobile, "Mobile");


                c1PatientDetails.Cols[COL_PAT_ID].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_SSN].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_Provider].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_DOB].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_Phone].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_Mobile].Visible = false;
                //c1PatientDetails.Cols[COL_PAT_MI].Visible = false;

                int _width = (this.Width - 20) / 10;

                c1PatientDetails.Cols[COL_PAT_Code].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_FirstName].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_MI].Width = Convert.ToInt32(_width * 0.5);
                c1PatientDetails.Cols[COL_PAT_LastName].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_SSN].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_Provider].Width = _width * 2;
                c1PatientDetails.Cols[COL_PAT_DOB].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_Phone].Width = _width * 1;
                c1PatientDetails.Cols[COL_PAT_Mobile].Width = _width * 1;


                c1PatientDetails.Cols[COL_PAT_Code].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_FirstName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_MI].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_LastName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_SSN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_Provider].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_DOB].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_Phone].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PAT_Mobile].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                //This code is commented by Ojeswini(02172010)
                //c1PatientDetails.VisualStyle = VisualStyle.Office2007Blue;
                //c1PatientDetails.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                //c1PatientDetails.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                //c1PatientDetails.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);


                gloC1FlexStyle.Style(c1PatientDetails, false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //c1PatientDetails.Show();
            }
        }

        private void DesignInsuranceGrid()
        {
            //c1Insurance.Clear(ClearFlags.All);

            try
            {

                c1Insurance.Hide();

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

                bool _designWidth = false;
                try
                {
                    //gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_DatabaseConnectionString);
                    //_designWidth = oSetting.LoadGridColumnWidth(c1Insurance, gloSettings.ModuleOfGridColumn.Billing, _UserID);
                    //oSetting.Dispose();
                }
                catch (Exception) // ex)
                {
                    //ex.ToString();
                    //ex = null;
                }


                if (_designWidth == false)
                {
                    c1Insurance.Cols[COL_SELECT].Width = 35;
                    c1Insurance.Cols[COL_PARTY].Width = 40;
                    c1Insurance.Cols[COL_INSURANCENAME].Width = Convert.ToInt32(nWidth - 165);
                    c1Insurance.Cols[COL_INSURANCEID].Width = 0;
                    c1Insurance.Cols[COL_INSURANCETYPE].Width = 0;
                    c1Insurance.Cols[COL_INSSELFMODE].Width = 0;
                    c1Insurance.Cols[COL_INSURANCEWORKERCOMP].Width = 0;
                    c1Insurance.Cols[COL_INSURANCEAUTOCLAIM].Width = 0;
                    c1Insurance.Cols[COL_COPAY].Width = 70;

                    //if (_InsuracePaymentType == InsuracePaymentType.Patient)
                    //{
                    //    c1Insurance.Cols[COL_SELECT].Width = 0;
                    //    c1Insurance.Cols[COL_INSURANCENAME].Width = Convert.ToInt32(nWidth - 165) + 35;
                    //    c1Insurance.Cols[COL_SELECT].Visible = false;
                    //}

                }

                #region " Allow editing "


                #endregion " Allow editing "

                //    c1Insurance.VisualStyle = VisualStyle.Office2007Blue;
                //  c1Insurance.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31,73,125);
                // c1Insurance.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);

                gloC1FlexStyle.Style(c1Insurance, false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                c1Insurance.Show();
            }

        }

        public string FormatAge(DateTime BirthDate)
        {
            DateTime _BDate = BirthDate;
            // Compute the difference between BirthDate 'CODE FROM gloPM
            //year and end year. 
            bool IsBirthDateLeap = false;
            int years = DateTime.Now.Year - BirthDate.Year;
            int months = 0;
            int days = 0;
            //Test if BirthDay for LeapYear.
            if (BirthDate.Day == 29 & BirthDate.Month == 2)
            {
                IsBirthDateLeap = true;
            }
            // Check if the last year was a full year. 
            if (DateTime.Now < BirthDate.AddYears(years) && years != 0)
            {
                years -= 1;
            }
            BirthDate = BirthDate.AddYears(years);
            // Now we know BirthDate <= end and the diff between them 
            // is < 1 year. 
            if (BirthDate.Year == DateTime.Now.Year)
            {
                months = DateTime.Now.Month - BirthDate.Month;
            }
            else
            {
                months = (12 - BirthDate.Month) + DateTime.Now.Month;
            }
            // Check if the last month was a full month. 
            if (DateTime.Now < BirthDate.AddMonths(months) && months != 0)
            {
                months -= 1;
            }
            BirthDate = BirthDate.AddMonths(months);
            // Now we know that BirthDate < end and is within 1 month 
            // of each other. 
            days = (DateTime.Now - BirthDate).Days;

            //To Adjust Age if BirthDate is 29th Feb in leap year
            if (IsBirthDateLeap == true)
            {
                //'Sequence of following IF code is too important.. DON'T MODIFY
                days -= 1;
                if (DateTime.Now.Day == 29 & DateTime.Now.Month == 2)
                {
                    days += 1;
                }
                else if (DateTime.Now.Year % 4 == 0)
                {
                    days += 1;
                }
                if (days < 0 & DateTime.Now.Year % 4 != 0)
                {
                    days = 30;
                    months = months - 1;
                    if (months < 0)
                    {
                        months = 11;
                        years = years - 1;
                    }
                }
                if (months == 12)
                {
                    days = 30;
                    months = 11;
                }
            }

            //Return years & " years " & months & " months " & days & " days"
            //Following code to display age in Numeric and Text
            //Dim age As New AgeDetail
            //age.Age = years & " Years " & months & " Months " & days & " Days"
            //' Cases

            //'20081119   ''Following Code to Store ExactAge in String
            string _AgeStr = "";
            //if (gblShowAgeInDays == true & gblAgeLimit >= DateDiff(DateInterval.Day, (System.DateTime)_BDate, System.DateTime.Now.Date))
            //{
            if (years == 0)
            {
                if (months == 0)
                {


                    if (days <= 1)
                    {
                        _AgeStr = days + " Day";
                    }
                    else
                    {
                        _AgeStr = days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Months " + days + " Days";
                    }
                }
            }
            else if (years == 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Month ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Months ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Days";
                    }
                }
            }
            else if (years > 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Days";
                    }
                }
            }
            //}
            //else
            //{
            //    //ShowAgeInDay is False OR AgeLimit less than Settings.
            //    if (years == 0)
            //    {
            //        //Added by pravin on 11/25/2008
            //        //                If months = 0 And months = 1 Then
            //        if (months == 1)
            //        {
            //            _AgeStr = months + " Month";
            //        }
            //        else if (months > 1)
            //        {
            //            _AgeStr = months + " Months";
            //        }
            //    }
            //    else if (years == 1)
            //    {
            //        if (months == 0)
            //        {
            //            _AgeStr = years + " Year ";
            //        }
            //        else if (months == 1)
            //        {
            //            _AgeStr = years + " Year " + months + " Month ";
            //        }
            //        else if (months > 1)
            //        {
            //            _AgeStr = years + " Year " + months + " Months ";
            //        }
            //    }
            //    else if (years > 1)
            //    {
            //        if (months == 0)
            //        {
            //            _AgeStr = years + " Years ";
            //        }
            //        else if (months == 1)
            //        {
            //            _AgeStr = years + " Years " + months + " Month ";
            //        }
            //        else if (months > 1)
            //        {
            //            _AgeStr = years + " Years " + months + " Months ";
            //        }
            //    }
            //    //Added by pravin if age in days  11/25/2008
            //    if (years == 0 & months == 0)
            //    {
            //        if (days <= 1)
            //        {
            //            _AgeStr = days + " Day";
            //        }
            //        else
            //        {

            //            _AgeStr = days + " Days";
            //        }

            //    }
            //}
            return _AgeStr;
        }

        public void SelectSearchBox()
        {
            txtPatientSearch.Select(); txtPatientSearch.Focus(); txtPatientSearch.SelectAll();
        }

        public void ClearControl()
        {
            try
            {
                //c1PatientDetails.Visible = false;
                _PatientID = 0;
                _PatientCode = "";
                _PatientName = "";
                _ProviderName = "";
                _PatientHomePhone = "";
                _Gender = "";
                _PatientCellPhone = "";
                _PatientAge = "";
                _ProviderID = 0;
                _Age = new AgeDetail();
                _TransactionDate = DateTime.Now;
                _Guarantor = "";
                _PatientsMaritalStatus = "";


                //Clear Patient Details on control
                lblAge.Text = "";
                lblDOB.Text = "";
                lblGender.Text = "";
                lblPatientCode.Text = "";
                lblPatientName.Text = "";
                lblProvider.Text = "";
                lblPhone.Text = "";
                lblGuarantor.Text = "";
                lblPatNameNCode.Text = "";
                lblTodaysDate.Text = ""; 


                lblTotalCharges.Text = "$ 0.00";
                lblInsurancePending.Text = "$ 0.00";
                lblPatientPending.Text = "$ 0.00";
                lblTotalBalance.Text = "$ 0.00";
                lblPendingCopay.Text = "$ 0.00";
                lblPendingAdvance.Text = "$ 0.00";
                lblPendingOtherReserved.Text = "$ 0.00";


                DesignInsuranceGrid();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void ClearControl(bool SelectSearch)
        {
            try
            {
                //c1PatientDetails.Visible = false;
                _PatientID = 0;
                _PatientCode = "";
                _PatientName = "";
                _ProviderName = "";
                _PatientHomePhone = "";
                _Gender = "";
                _PatientCellPhone = "";
                _PatientAge = "";
                _ProviderID = 0;
                _Age = new AgeDetail();
                _TransactionDate = DateTime.Now;
                _Guarantor = "";


                //Clear Patient Details on control
                lblAge.Text = "";
                lblDOB.Text = "";
                lblGender.Text = "";
                lblPatientCode.Text = "";
                lblPatientName.Text = "";
                lblProvider.Text = "";
                lblPhone.Text = "";
                lblGuarantor.Text = "";
                lblPatNameNCode.Text = "";
                lblTodaysDate.Text = ""; 

                lblTotalCharges.Text = "$ 0.00";
                lblInsurancePending.Text = "$ 0.00";
                lblPatientPending.Text = "$ 0.00";
                lblTotalBalance.Text = "$ 0.00";
                lblPendingCopay.Text = "$ 0.00";
                lblPendingAdvance.Text = "$ 0.00";
                lblPendingOtherReserved.Text = "$ 0.00";

                DesignInsuranceGrid();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                txtPatientSearch.Text = "";
                txtPatientSearch.Select();
                txtPatientSearch.Focus();
            }
        }
   
        private void FillPatientBalance(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            string _sqlQuery = "";
            object _Result = null;
            DataTable _dt = null;

            decimal _TotCharges = 0;
           // decimal _TotInsPaid = 0;
            //decimal _TotPatPaid = 0;
            decimal _totalInsResponsiblity = 0;
            decimal _totalPatResponsiblity = 0;
            decimal _totalBalAmt = 0;
            decimal _totalResAmt = 0;

            oDB.Connect(false);

            try
            {
                if (PatientID <= 0)
                { return; }


                lblTotalCharges.Text = "$ " + Convert.ToDecimal(_TotCharges).ToString("#0.00");

                lblInsurancePending.Text = "$ 0.00";
                lblPatientPending.Text = "$ 0.00";
                lblTotalBalance.Text = "$ 0.00";

                
                //Total Charges
                 _sqlQuery = "SELECT  ISNULL(SUM(BL_Transaction_Lines.dTotal),0) AS TotalCharges " +
                 " FROM  BL_Transaction_MST WITH (NOLOCK) INNER JOIN BL_Transaction_Lines WITH (NOLOCK) ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID " +
                 " WHERE  BL_Transaction_MST.nPatientID = " + PatientID + " AND BL_Transaction_MST.nClinicID = " + _ClinicID + " AND ISNULL(BL_Transaction_MST.bIsVoid,0) = 0 ";
                //_Result = new object();
                _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_Result != null && Convert.ToString(_Result) != "")  
                {_TotCharges = Convert.ToDecimal(_Result);}
                _Result = null;


                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                //_dt = new DataTable();


                oDB.Retrive("BL_GET_PATIENT_ACCOUNT_BALANCE_REVISED", oParameters, out _dt);
                
                if(_dt != null && _dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0]["InsuranceDue"] != DBNull.Value && Convert.ToString(_dt.Rows[0]["InsuranceDue"]).Trim() != "")
                    { _totalInsResponsiblity = Convert.ToDecimal(_dt.Rows[0]["InsuranceDue"]); }
                    if (_dt.Rows[0]["PatientDue"] != DBNull.Value && Convert.ToString(_dt.Rows[0]["PatientDue"]).Trim() != "")
                    { _totalPatResponsiblity = Convert.ToDecimal(_dt.Rows[0]["PatientDue"]); }
                    
                    //if (_dt.Rows[0]["TotalBalance"] != DBNull.Value && Convert.ToString(_dt.Rows[0]["TotalBalance"]).Trim() != "")
                    //{ _totalBalAmt = Convert.ToDecimal(_dt.Rows[0]["TotalBalance"]); }
                    
                    
                    // Code Commented by Pankaj as the formula has changed
                    // New formula is Bal = Pat due + Ins due
                    //_totalBalAmt = _TotCharges;

                    if (_dt.Rows[0]["AvailableReserve"] != DBNull.Value && Convert.ToString(_dt.Rows[0]["AvailableReserve"]).Trim() != "")
                    { _totalResAmt = Convert.ToDecimal(_dt.Rows[0]["AvailableReserve"]); }
                }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
                lblInsurancePending.Text = "$ " + _totalInsResponsiblity.ToString("#0.00");

                //if (_totalPatResponsiblity > 0)
                //{
                    _totalPatResponsiblity = _totalPatResponsiblity - _totalResAmt;
                    lblPatientPending.Text = "$ " + _totalPatResponsiblity.ToString("#0.00");
                //}

                _totalBalAmt = _totalInsResponsiblity + _totalPatResponsiblity;
                lblTotalBalance.Text = "$ " + _totalBalAmt.ToString("#0.00");

                #region " .... Patient Responsibility ..... "

                ////..
                //oParameters = new gloDatabaseLayer.DBParameters();
                //oParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                //DataTable _dt = new DataTable();
                //oDB.Retrive("BL_GET_PATIENTDUE", oParameters, out _dt);


                //decimal _PatientDueamount = 0;
                //if (_dt != null && _dt.Rows.Count > 0)
                //{
                //    _PatientDueamount = Convert.ToDecimal(_dt.Rows[0]["PatientDue"]);
                //    lblPatientPending.Text = "$ " + _PatientDueamount.ToString("#0.00");
                //}

                #endregion " .... Patient Responsibility ..... "

                #region " ... Insurance Responsibility  for patient ... "

                //oParameters = new gloDatabaseLayer.DBParameters();
                //oParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                //_dt = new DataTable();
                //oDB.Retrive("BL_GET_INSURANCEDUE", oParameters, out _dt);

                //decimal _InsDueamount = 0;
                //if (_dt != null && _dt.Rows.Count > 0)
                //{
                //    _InsDueamount = Convert.ToDecimal(_dt.Rows[0]["Amount"]);
                //    lblInsurancePending.Text = "$ " + _InsDueamount.ToString("#0.00");
                //}

                #endregion " ... Insurance Responsibility  for patient ... "

                //lblTotalBalance.Text = "$ " + Convert.ToString(_InsDueamount + _PatientDueamount); 

                //_TotPatPaid = 0;
                
                #region "Pending Copay, Advance and Reserve"
                lblPendingCopay.Text = "$ 0.00";
                lblPendingAdvance.Text = "$ 0.00";
                lblPendingOtherReserved.Text = "$ 0.00";


                //....Code commented by Sagar Ghodke on 20100505
                //....Replaced sql query with stored procedure

                //_sqlQuery = "select nPaymentNoteSubType,sum(AvailableReserve) as AvailableReserve from " +
                //" ( " +
                //    " SELECT  " +
                //    " ISNULL(BL_EOB_Notes.nPaymentNoteSubType,'') AS nPaymentNoteSubType, " +
                //    " ISNULL( " +
                //    " ( " +
                //    " (BL_EOBPayment_DTL.nAmount) -    " +
                //    " ISNULL( " +
                //    " ( " +
                //    " SELECT " +
                //    " ( " +
                //    " ( " +
                //    " SELECT SUM(ISNULL(nAmount,0))  " +
                //    " FROM BL_EOBPayment_DTL AS BL_EOBPayment_DTL_UseRes    " +
                //    " WHERE  " +
                //    " BL_EOBPayment_DTL_UseRes.nResEOBPaymentID = BL_EOBPayment_DTL.nEOBPaymentID  " +
                //    " AND   BL_EOBPayment_DTL_UseRes.nResEOBPaymentDetailID = BL_EOBPayment_DTL.nEOBPaymentDetailID " +
                //    " AND   (BL_EOBPayment_DTL_UseRes.nPaymentType = 6 AND BL_EOBPayment_DTL_UseRes.nPaymentSubType = 8 AND nPaySign = 2) " +
                //    " AND   (BL_EOBPayment_DTL_UseRes.nPaymentType = 6 AND BL_EOBPayment_DTL_UseRes.nPaymentSubType = 8 AND BL_EOBPayment_DTL_UseRes.nPaySign = 2 AND ISNULL(BL_EOBPayment_DTL_UseRes.nPayMode,0) <> 0) " +
                //    " ) " +
                //    " ) " +
                //    " ) " +
                //    " ,0)),0) AS AvailableReserve " +
                //    " FROM         BL_EOBPayment_DTL INNER JOIN " +
                //    " BL_EOBPayment_MST ON BL_EOBPayment_DTL.nEOBPaymentID = BL_EOBPayment_MST.nEOBPaymentID INNER JOIN " +
                //    " Patient ON BL_EOBPayment_DTL.nPatientID = Patient.nPatientID INNER JOIN " +
                //    " BL_EOB_Notes ON BL_EOBPayment_DTL.nEOBPaymentID = BL_EOB_Notes.nEOBPaymentID AND  " +
                //    " BL_EOBPayment_DTL.nEOBPaymentDetailID = BL_EOB_Notes.nEOBPaymentDetailID " +
                //    " WHERE " +
                //    " BL_EOBPayment_DTL.npaymenttype = 2 AND BL_EOBPayment_DTL.npaymentsubtype = 9 AND BL_EOBPayment_DTL.npaysign = 2 " +
                //    " AND BL_EOBPayment_DTL.npatientid = "+_PatientID+" AND BL_EOBPayment_DTL.nClinicID = "+_ClinicID+" " +
                //" ) " +     
                //" as Final	GROUP BY nPaymentNoteSubType ";

                //....End code comment by Sagar Ghodke on 20100505

                oParameters.Clear();
                oParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                DataTable oCARData = null;
                //oDB.Retrive_Query(_sqlQuery, out oCARData);
                oDB.Retrive("BL_GET_PATIENT_RESERVE_BALANCE",oParameters, out oCARData);
                if (oCARData != null && oCARData.Rows.Count > 0)
                {
                    for (int i = 0; i <= oCARData.Rows.Count - 1; i++)
                    {
                        if (oCARData.Rows[i]["nPaymentNoteSubType"] != null && oCARData.Rows[i]["nPaymentNoteSubType"].ToString().Trim() != "")
                        {
                            decimal _Pending = 0;
                            if (oCARData.Rows[i]["AvailableReserve"] != null && oCARData.Rows[i]["AvailableReserve"].ToString().Trim() != "")
                            {
                                _Pending = Convert.ToDecimal(Convert.ToString(oCARData.Rows[i]["AvailableReserve"]));
                            }
                            //public enum EOBPaymentSubType
                            //{
                            //    None = 0, Insurace = 1, Copay = 2, Advance = 3, Coinsurace = 4, Dedcutiable = 5, WriteOff = 6, WithHold = 7, Patient = 8, Reserved = 9, Other = 10, TakeBack = 11, Adjuestment = 12
                            //}
                            //PLEASE REFER THIS ENUM FROM BILLING FOR ANY MODIFICATION, 
                            //CURRENTLY WE ARE USING HARD CODED VALUE TO AVOID BILLING REF IN PATIENT STRIP
                            if (_Pending > 0)
                            {
                                if (Convert.ToInt32(oCARData.Rows[i]["nPaymentNoteSubType"]) == 2)
                                {
                                    lblPendingCopay.Text = "$ " + _Pending.ToString("#0.00");
                                }
                                else if (Convert.ToInt32(oCARData.Rows[i]["nPaymentNoteSubType"]) == 3)
                                {
                                    lblPendingAdvance.Text = "$ " + _Pending.ToString("#0.00");
                                }
                                else if (Convert.ToInt32(oCARData.Rows[i]["nPaymentNoteSubType"]) == 10)
                                {
                                    lblPendingOtherReserved.Text = "$ " + _Pending.ToString("#0.00");
                                }
                            }
                        }
                    }
                }
                if (oCARData != null)
                {
                    oCARData.Dispose();
                    oCARData = null;
                }

                
            #endregion

            }
            catch (gloDatabaseLayer.DBException oDBEx)
            {
                oDBEx.ERROR_Log(oDBEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public void UpdatePatientBalanceAndAlerts()
        {
            FillPatientBalance(_PatientID);
        }

        private void FillAlerts_Notes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            DataTable dt = null;
            DataTable dtNotes = null;
            string _strSQL = "";
            lblAlerts.Text = "";
            txtNotes.Text = "";
            if (btnToolTip == null)
            {
                btnToolTip = new ToolTip();
            }
            btnToolTip.SetToolTip(lblAlerts, "");


            try
            {
                oDB.Connect(false);
                _strSQL = " SELECT nAlertID, sAlertName, nAlertType, bAlertStatus, sAlertColor, nPatientID, nClinicID " +
                          " FROM PatientAlerts WITH (NOLOCK) " +
                          " WHERE (nPatientID = " + _PatientID + ") AND (nClinicID = " + _ClinicID + ") " +
                          " ORDER BY nAlertID desc";
                oDB.Retrive_Query(_strSQL, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                {
                    //if (dt.Rows.Count > 1)
                    //{
                    //    lblAlerts.Text = dt.Rows[0]["sAlertName"].ToString() + " . . . ";
                    //}
                    //else
                    //{
                    if (dt.Rows[0]["sAlertName"].ToString().Length > 35)
                        lblAlerts.Text = dt.Rows[0]["sAlertName"].ToString().Substring(0, 28) + " . . . ";
                    else
                        lblAlerts.Text = dt.Rows[0]["sAlertName"].ToString();
                //    btnToolTip = new ToolTip();
                    if (btnToolTip == null)
                    {
                        btnToolTip = new ToolTip();
                    }
                    btnToolTip.SetToolTip(lblAlerts, dt.Rows[0]["sAlertName"].ToString());
                    if (dt.Rows.Count > 1)
                        lblAlertsCap.Text = "Alerts (" + dt.Rows.Count + ") :";
                    else
                        lblAlertsCap.Text = "Alerts :";
                    //_StrPatientAlert=dt.Rows[0]["sAlertName"].ToString();
                }
                else
                {
                  //  btnToolTip = new ToolTip();
                    if (btnToolTip == null)
                    {
                        btnToolTip = new ToolTip();
                    }
                    btnToolTip.SetToolTip(lblAlerts, "");
                    lblAlertsCap.Text = "Alerts :";
                    //_StrPatientAlert="";
                }
                //}

                oDB.Connect(false);
                _strSQL = "SELECT sInsuranceNotes AS Note FROM Patient WITH (NOLOCK) WHERE (nPatientID = " + _PatientID + ") AND (nClinicID = " + _ClinicID + ")";

                oDB.Retrive_Query(_strSQL, out dtNotes);
                oDB.Disconnect();

                if (dtNotes != null && dtNotes.Rows.Count > 0)
                {
                    txtNotes.Text = dtNotes.Rows[0]["Note"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (dtNotes != null)
                {
                    dtNotes.Dispose();
                    dtNotes = null;
                }
            }
        }

        private void FillClaimOnHold()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
         //   string _strSQL = "";
            lblClaimOnHold.Text = "";
            //txtNotes.Text = "";
            DataTable dtClaimOnHold = null;
            oParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
            oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

            oDB.Connect(false);
            oDB.Retrive("Patient_Financial_View_Header_ClaimOnHold", oParameters, out dtClaimOnHold);
            oDB.Disconnect();
            if (dtClaimOnHold != null)
            {
                if (dtClaimOnHold.Rows.Count > 0)
                {
                    lblClaimOnHold.Text = "Some claims are on hold !";//ClaimOnHold.Rows[0]["ClaimNo"].ToString();
                    txtClaimOnHold.Text = dtClaimOnHold.Rows[0]["ClaimNo"].ToString();
                    if (pnlClaimOnHold.Visible == true)
                        btnClaimOnHold.Visible = false;
                    else
                        btnClaimOnHold.Visible = true;

                }
                else
                {
                    btnClaimOnHold.Visible = false;
                    lblClaimOnHold.Text = "";
                    txtClaimOnHold.Text = "";
                    pnlClaimOnHold.Visible = false;

                }

            }
            else
            {
                btnClaimOnHold.Visible = false;
                lblClaimOnHold.Text = "";
                txtClaimOnHold.Text = "";
                pnlClaimOnHold.Visible = false;
            }
            if (oDB != null) { oDB.Dispose(); }
            if (dtClaimOnHold != null)
            {
                dtClaimOnHold.Dispose();
                dtClaimOnHold = null;
            }
            
        }

        private void FillStatementNote()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            DataTable dt = null;
            //DataTable dtNotes = null;
            string _sqlQuery = "";
            string _sFromDate = "";
            string _sToDate = "";
            lblStatementNote.Text = "";
            //btnToolTip = new ToolTip();
            //btnToolTip.SetToolTip(lblStatementNote, "");
            try
            {
                oDB.Connect(false);
                //  _sqlQuery = "SELECT TOP(1)nFromDate, nToDate,sStatementNote FROM Patient_Statement_Notes WHERE nPatientID = " + _PatientID + " Order BY nToDate desc ";
                _sqlQuery = "SELECT TOP(1)nFromDate, nToDate,sStatementNote FROM Patient_Statement_Notes WITH (NOLOCK) WHERE nPatientID = " + _PatientID + ""
                            + " AND dbo.EDI837_DateAsNumber(CONVERT(VARCHAR(10),dbo.gloGetDate(),101)) <= nToDate "
                            + " AND dbo.EDI837_DateAsNumber(CONVERT(VARCHAR(10),dbo.gloGetDate(),101)) >= nFromDate "
                            + "ORDER BY nToDate DESC ";
                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0) //&& dt.Rows[0]["nFromDate"] )
                {

                    _sFromDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[0]["nFromDate"])).ToString("MM/dd/yy");
                    _sToDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[0]["nToDate"])).ToString("MM/dd/yy");
                    lblStatementNote.Text = dt.Rows[0]["sStatementNote"].ToString();
                    lblStatementNoteCap.Text = "Statement Note";
                    lblStatementDate.Text = "[" + _sFromDate + " - " + _sToDate + "] :";
                    //btnToolTip = new ToolTip();
                    toolTip1.SetToolTip(lblStatementNote, SplitToolTip(dt.Rows[0]["sStatementNote"].ToString()));

                }
                else
                {
                    //btnToolTip = new ToolTip();
                    toolTip1.SetToolTip(lblStatementNote, "");
                    lblStatementNoteCap.Text = "Statement Note :";
                    lblStatementDate.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                
            }


        }

        public Int16 GetBillingType(Int64 TransactionId, Int64 MstTransactionId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                object BillingType;
                oParameters.Add("@nTransactionId", TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionMstId", MstTransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                BillingType = oDB.ExecuteScalar("BL_Get_BillingType", oParameters);
                oDB.Disconnect();
                if (BillingType.ToString().Trim() == "")
                    return 0;
                else
                    return Convert.ToInt16(BillingType);
            }
            catch
            {
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
               
            }
        }


        #region "UserRights"

        //Added By Pramod Nair For UserRights 20090720
        private void AssignUserRights()
        {
            try
            {
                if (_UserName.Trim() != "")
                {
                    gloUserRights.ClsgloUserRights oClsgloUserRights = null;
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(_DatabaseConnectionString);
                    oClsgloUserRights.CheckForUserRights(_UserName);
                    btn_ModityPatient.Enabled = oClsgloUserRights.ModifyPatient;
                    oClsgloUserRights.Dispose();
                    oClsgloUserRights = null;
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        #endregion

        #endregion
      
        #region "Patient Search"

        private void InstringSearch(string SearchText)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatients = null;
            String strSQL = "";
            try
            {
                // solving sales force case - GLO2010-0007900

                pnlMiddle.Visible = true;
                pnlAlerts.Visible = _ShowAlerts;
                c1PatientDetails.Visible = false;
                // End

                //pnlLeft.Visible = true;
                if (chk_ClaimNoSearch.Checked == true)
                { 

                    #region "Claim No Search"

                    object oPatientID = null;

                    Int64 nTransactionID = 0;
                    Int64 nTransactionMasterID = 0;

                    _ClaimNumber = 0;
                    _SubClaimNumber = 0;
                    _ClaimSubClaimNo = "";

                    if (String.IsNullOrEmpty(SearchText) == false)
                    {
                        string[] strClaimSubClaim = null;
                        strClaimSubClaim = SearchText.Trim().Split('-');

                        ClaimSubClaimNo = SearchText.Trim();

                        if (strClaimSubClaim != null && strClaimSubClaim.Length > 1)
                        {
                            if (strClaimSubClaim[0].Trim() != "") { ClaimNumber = Convert.ToInt64(strClaimSubClaim[0]); }

                            if (Convert.ToString(strClaimSubClaim[1]).Trim() != "" && Convert.ToInt64(strClaimSubClaim[1]) > 0)
                            { SubClaimNumber = Convert.ToInt64(strClaimSubClaim[1]); }
                            else
                            {
                                MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClaimNumber = 0;
                                ClaimSubClaimNo = string.Empty;
                                SubClaimNumber = 0;
                                return;
                            }
                        }
                        else if (strClaimSubClaim != null && strClaimSubClaim.Length == 1)
                        {
                            if (strClaimSubClaim[0].Trim() != "") { ClaimNumber = Convert.ToInt64(strClaimSubClaim[0]); }
                        }
                                                
                        //strSQL = "SELECT nPatientID FROM BL_Transaction_MST WHERE nClaimNo = " + SearchText;
                        strSQL = "SELECT nPatientID FROM BL_Transaction_MST WITH (NOLOCK) WHERE nClaimNo = " + ClaimNumber + " and nClinicID = " + _ClinicID + " ";

                        oDB.Connect(false);
                        oPatientID = oDB.ExecuteScalar_Query(strSQL);

                        if (oPatientID != null && oPatientID.ToString().Trim() != "")
                        {
                            string strSubClaimNo = "";
                            if (SubClaimNumber == 0)
                            { strSubClaimNo = ""; }
                            else { strSubClaimNo = SubClaimNumber.ToString(); }
                            DataTable _dt = null;
                          

                            oParameters.Clear();
                            oParameters.Add("@nClaimno", ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),  
                            oParameters.Add("@sSubClaimno", strSubClaimNo, ParameterDirection.Input, SqlDbType.VarChar, 50);// Varchar(50) 
                                                        
                            oDB.Retrive("BL_Select_SplitClaims",oParameters, out _dt);
                            if (_dt != null && _dt.Rows.Count > 0)
                            {
                                nTransactionID = Convert.ToInt64(_dt.Rows[0]["nTransactionID"]);
                                nTransactionMasterID = Convert.ToInt64(_dt.Rows[0]["nTransactionMasterID"]);
                            }
                            if (_dt != null)
                            {
                                _dt.Dispose();
                                _dt = null;
                            }
                        }

                        oDB.Disconnect();
                    }
                    if (nTransactionID.ToString().Trim() != "")
                    {
                        _TransactionID = Convert.ToInt64(nTransactionID);
                    }
                    if (nTransactionMasterID.ToString().Trim() != "")
                    {
                        _TransactionMasterID = Convert.ToInt64(nTransactionMasterID);
                    }
                    if (oPatientID != null && oPatientID.ToString().Trim() != "")
                    {
                        _PatientID = Convert.ToInt64(oPatientID);
                        //_ClaimNumber = Convert.ToInt64(SearchText);
                        //_ClaimNumber = Convert.ToInt64(SearchText);
                        FillDetails(_PatientID,_FormName, 1, false);
                    }
                    else
                    {
                        _PatientID = 0;
                        _ClaimNumber = 0;
                        ClearControl();
                    }

                    #endregion
                    
                    // If Search on Claim No then dont go further to search on Patient Info
                    return;
                }

                // Seach On Patient Information
                if (dvPatient == null)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                //byte nPatientIDColNo = 0;
             //   byte nPatientCodeColumnNo = 1;
            //    byte nPatientFirstNameColumnNo = 2;
             //   byte nPatientMiddleNameColumnNo = 3;
             //   byte nPatientLastNameColumnNo = 4;

                string str = "";
                //int rowid;
                string[] strSearchArray;
                str = SearchText;
                //20100106 Mahesh Nawal Set the logic for special Character for rowfilter

                str = SearchText;
                str = str.Trim().Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%"); ;

                if (str.Length > 1)
                {
                    string str1 = str.Substring(1).Replace("%", "");
                    str = str.Substring(0, 1) + str1;
                }

                if (str.Trim() != "")
                {
                    //dvPatient = new DataView();
                    strSearchArray = str.Split(',');
                    string strSearch = "";
                    if (strSearchArray.Length == 1)
                    {
                        strSearch = strSearchArray[0];

                       
                        
                        //string _sqlQuery = "";
                        //int retValue = 0;
                        
                        oDB.Connect(false);

                        if (SearchOnPatientCode == true)
                        
                        {
                            //*******************************************************
                            //Commented and added By Debasish Das on 13th March 2010
                            //*******************************************************

                            ////dvPatient.RowFilter = dvPatient.Table.Columns[nPatientCodeColumnNo].ColumnName + " = '" + strSearch + "'";
                         
                            strSQL = "SELECT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                            + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                            + " Convert(varchar,Patient.dtDOB,101) AS DOB, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName "
                            + " FROM Patient WITH (NOLOCK) INNER JOIN Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID "
                            + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') = '" + strSearch + "%' ";

                            oDB.Retrive_Query(strSQL, out dtPatients);
                            if (dtPatients != null)
                            {
                                dvPatient = dtPatients.DefaultView;
                                dtPatients.Dispose();
                            }
                            else
                            {
                                dvPatient = null;
                                return;
                            }
                            
                            //***************************** Ends Here ****************
                        }

                        if (SearchOnPatientCode == false || dvPatient.Count == 0)
                        {
                            //*******************************************************
                            //Commented By Debasish Das on 13th March 2010
                            //*******************************************************

                            //dvPatient.RowFilter = dvPatient.Table.Columns[nPatientCodeColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                      dvPatient.Table.Columns[nPatientFirstNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                      dvPatient.Table.Columns[nPatientMiddleNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                            //                      dvPatient.Table.Columns[nPatientLastNameColumnNo].ColumnName + " Like '" + strSearch + "%'";

                            strSQL = "SELECT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                            + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                            + " Convert(varchar,Patient.dtDOB,101) AS DOB, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName "
                            + " FROM Patient WITH (NOLOCK) INNER JOIN Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID "
                            + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') LIKE '" + strSearch + "%' OR " +
                            " ISNULL(Patient.sFirstName, '') LIKE '" + strSearch + "%' OR " +
                            " ISNULL(Patient.sMiddleName, '') LIKE '" + strSearch + "%' OR " +
                            " ISNULL(Patient.sLastName, '') LIKE '" + strSearch + "%' ";

                            oDB.Retrive_Query(strSQL, out dtPatients);
                            if (dtPatients != null)
                            {
                                dvPatient = dtPatients.DefaultView;

                                dtPatients.Dispose();
                            }
                            else
                            {
                                dvPatient = null;
                                return;
                            }

                            //********************* Ends Here ***********************
                        }


                        if (dvPatient.Count > 0)
                        {
                            //  txtPatientName.Text = dvPatient[0][nPatientFirstNameColumnNo].ToString() + " " + dvPatient[0][nPatientLastNameColumnNo].ToString();
                            if (dvPatient.Count == 1)
                            {
                                pnlMiddle.Visible = true;
                                pnlAlerts.Visible = _ShowAlerts;
                                c1PatientDetails.Visible = false;

                                _PatientID = Convert.ToInt64(dvPatient[0]["nPatientID"]);
                                FillDetails(_PatientID, _FormName, 1, false);
                            }
                            else
                            {
                                #region "Show Multiple Patients"
                                DesignPatientGrid();
                                for (int i = 0; i <= dvPatient.Count - 1; i++)
                                {
                                    c1PatientDetails.Rows.Add();
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_ID, dvPatient[i]["nPatientID"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Code, dvPatient[i]["PatientCode"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_FirstName, dvPatient[i]["FirstName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_MI, dvPatient[i]["MiddleName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_LastName, dvPatient[i]["LastName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_SSN, dvPatient[i]["SSN"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Provider, dvPatient[i]["sProviderName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_DOB, dvPatient[i]["DOB"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Phone, dvPatient[i]["Phone"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Mobile, dvPatient[i]["Mobile"].ToString());

                                }
                                pnlMiddle.Visible = false;
                                pnlAlerts.Visible = false;
                                c1PatientDetails.Visible = true;

                                c1PatientDetails.BringToFront();
                                c1PatientDetails.Select(1, 0);
                                c1PatientDetails.Focus();

                                if (btnDown.Visible == true)
                                {
                                    btnDown_Click(null, null);
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            pnlMiddle.Visible = true;
                            pnlAlerts.Visible = _ShowAlerts;
                            c1PatientDetails.Visible = false;

                            _PatientID = 0;
                            ClearControl();

                        }
                    }
                    else
                    {
                        for (int i = 0; i < strSearchArray.Length; i++)
                        {
                            strSearch = strSearchArray[i];
                            #region "Comma seprated patient Search Query "
                            oDB.Connect(false);

                            if (SearchOnPatientCode == true)
                            {
                                
                                strSQL = "SELECT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                                + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                                + " Convert(varchar,Patient.dtDOB,101) AS DOB, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName "
                                + " FROM Patient WITH (NOLOCK) INNER JOIN Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID "
                                + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') = '" + strSearch + "%' ";

                                oDB.Retrive_Query(strSQL, out dtPatients);
                                if (dtPatients != null)
                                {
                                    dvPatient = dtPatients.DefaultView;
                                    dtPatients.Dispose();
                                }
                                else
                                {
                                    dvPatient = null;
                                    return;
                                }

                             
                            }

                            if (SearchOnPatientCode == false || dvPatient.Count == 0)
                            {
                                
                                strSQL = "SELECT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                                + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                                + " Convert(varchar,Patient.dtDOB,101) AS DOB, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName "
                                + " FROM Patient WITH (NOLOCK) INNER JOIN Provider_MST WITH (NOLOCK) ON Patient.nProviderID = Provider_MST.nProviderID "
                                + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') LIKE '" + strSearch + "%' OR " +
                                " ISNULL(Patient.sFirstName, '') LIKE '" + strSearch + "%' OR " +
                                " ISNULL(Patient.sMiddleName, '') LIKE '" + strSearch + "%' OR " +
                                " ISNULL(Patient.sLastName, '') LIKE '" + strSearch + "%' ";

                                oDB.Retrive_Query(strSQL, out dtPatients);
                                if (dtPatients != null)
                                {
                                    dvPatient = dtPatients.DefaultView;

                                    dtPatients.Dispose();
                                }
                                else
                                {
                                    dvPatient = null;
                                    return;
                                }

                               
                            }



                            #endregion

                            DataTable dtTemp = null;
                            if (strSearch.Trim() != "")
                            {
                                if (i == 0)
                                {
                                    dtTemp = dvPatient.ToTable();
                                    dvNext = dtTemp.DefaultView;
                                }
                                else
                                {
                                    //dtTemp = dvNext.ToTable();
                                    //dvNext = dtTemp.DefaultView;

                                    dtTemp = dvPatient.ToTable();
                                    dvNext = dtTemp.DefaultView;
                                    
                                }

                                //dvNext.RowFilter = dvNext.Table.Columns[nPatientCodeColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                //            dvNext.Table.Columns[nPatientFirstNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                //            dvNext.Table.Columns[nPatientMiddleNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                //            dvNext.Table.Columns[nPatientLastNameColumnNo].ColumnName + " Like '" + strSearch + "%' ";
                            }
                            if (dtTemp != null)
                            {
                                dtTemp.Dispose();
                                dtTemp = null;
                            }
                        }

                        if (dvNext.Count > 0)
                        {
                            // txtPatientName.Text = dvNext[0][nPatientFirstNameColumnNo].ToString() + " " + dvPatient[0][nPatientLastNameColumnNo].ToString();
                            if (dvNext.Count == 1)
                            {
                                pnlMiddle.Visible = true;
                                pnlAlerts.Visible = _ShowAlerts;
                                c1PatientDetails.Visible = false;

                                _PatientID = Convert.ToInt64(dvNext[0]["nPatientID"]);
                                FillDetails(_PatientID, _FormName, 1, false);
                            }
                            else
                            {
                                #region "Show Multiple Patients"
                                DesignPatientGrid();
                                for (int i = 0; i <= dvNext.Count - 1; i++)
                                {
                                    c1PatientDetails.Rows.Add();
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_ID, dvNext[i]["nPatientID"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Code, dvNext[i]["PatientCode"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_FirstName, dvNext[i]["FirstName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_MI, dvNext[i]["MiddleName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_LastName, dvNext[i]["LastName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_SSN, dvNext[i]["SSN"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Provider, dvNext[i]["sProviderName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_DOB, dvNext[i]["DOB"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Phone, dvNext[i]["Phone"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Mobile, dvNext[i]["Mobile"].ToString());
                                }

                                pnlMiddle.Visible = false;
                                pnlAlerts.Visible = false;
                                c1PatientDetails.Visible = true;

                                c1PatientDetails.BringToFront();
                                c1PatientDetails.Select(1, 0);
                                c1PatientDetails.Focus();

                                if (btnDown.Visible == true)
                                {
                                    btnDown_Click(null, null);
                                }

                                #endregion

                            }
                        }
                        else
                        {
                            _PatientID = 0;
                            ClearControl();
                        }
                    }

                }
                else
                {
                    _PatientID = 0;
                    ClearControl();
                }


            }
            catch (System.OverflowException e)
            {
                MessageBox.Show("Claim number is invalid, Please enter a valid claim number.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(e.Message, false);
                return;
            }
            catch (Exception objErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), true);
                return;
                // MessageBox.Show(objErr.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtPatients != null) { dtPatients.Dispose(); }
                if (dvPatient != null) { dvPatient.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
             //   if (dtTemp != null) { dtTemp.Dispose(); }
                if (dvNext != null) { dvNext.Dispose(); }
            }
        }


        private void txtPatientSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chk_ClaimNoSearch.Checked == true)
            {
                //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
                //{
                //    e.Handled = true;
                //}
                //if (e.KeyChar == 46)
                //{
                //    e.Handled = true;
                //}
                //for split cliam
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
                #region
                //bool _IsValidChar = true;

                //if (chk_ClaimNoSearch.Checked == true && txtPatientSearch.Text.Contains(".") == true && txtPatientSearch.Text.Contains("?") == true && txtPatientSearch.Text.Contains("/") == true
                //     && txtPatientSearch.Text.Contains("/") == true && txtPatientSearch.Text.Contains("^") == true
                //     && txtPatientSearch.Text.Contains("!") == true && txtPatientSearch.Text.Contains("&") == true
                //     && txtPatientSearch.Text.Contains("@") == true && txtPatientSearch.Text.Contains("*") == true
                //     && txtPatientSearch.Text.Contains("#") == true && txtPatientSearch.Text.Contains("(") == true
                //     && txtPatientSearch.Text.Contains("$") == true && txtPatientSearch.Text.Contains(")") == true
                //      && txtPatientSearch.Text.Contains("_") == true
                //     && txtPatientSearch.Text.Contains("+") == true && txtPatientSearch.Text.Contains("=") == true
                //     && txtPatientSearch.Text.Contains("<") == true && txtPatientSearch.Text.Contains(">") == true
                //     && txtPatientSearch.Text.Contains("?") == true && txtPatientSearch.Text.Contains("/") == true
                //     && txtPatientSearch.Text.Contains("~") == true && txtPatientSearch.Text.Contains("`") == true
                //     && txtPatientSearch.Text.Contains("%") == true && txtPatientSearch.Text.Contains(",") == true
                //     && txtPatientSearch.Text.Contains(":") == true && txtPatientSearch.Text.Contains(";") == true
                //     && txtPatientSearch.Text.Contains("{") == true && txtPatientSearch.Text.Contains("}") == true
                //     && txtPatientSearch.Text.Contains("[") == true && txtPatientSearch.Text.Contains("]") == true
                //     && txtPatientSearch.Text.Contains("/") == true
                //     && txtPatientSearch.Text.Contains("+") == true && txtPatientSearch.Text.Contains("*") == true
                //    && txtPatientSearch.Text.Contains(".") == true)
                //{
                //    _IsValidChar=false;
                //}

                #endregion

                InstringSearch(txtPatientSearch.Text.Trim());
                FillPatientInsurances(_PatientID);
                FillPatientBalance(_PatientID);
                FillAlerts_Notes();
                FillStatementNote();

                if (c1PatientDetails.Visible == false)
                    OnPatientSearchKeyPress(sender, e);
            }
        }

        public void SearchPatient(String SearchText)
        {
            txtPatientSearch.Text = SearchText;
            SetClaimNoSearch(false);

            object sender = (object)txtPatientSearch;
            KeyPressEventArgs e = new KeyPressEventArgs((char)13);

            txtPatientSearch_KeyPress(sender, e);
            e = null;

        }
        #endregion

        #region "Other Events"
       
        private void gloPatientStripControl_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (ControlSize_Changed != null)
                {
                    ControlSize_Changed(sender, e);
                }
            }
            catch (Exception)
            {
            }
        }
     
        private void btn_ModityPatient_Click(object sender, EventArgs e)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_DatabaseConnectionString);

            try
            {
                if (oSecurity.isPatientLock(_PatientID, true) == false && _PatientID > 0)
                {
                    gloPatient.frmSetupPatient ofrmSetupPatient = new gloPatient.frmSetupPatient(_PatientID, _DatabaseConnectionString);
                    ofrmSetupPatient.ShowDialog(this);
                    ofrmSetupPatient.Dispose();
                    FillDetails(_PatientID, _FormName, 1, false);
                    if (PatientModified != null)
                        PatientModified(null, null);
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

        private void btn_AddInsurancePlan_Click(object sender, EventArgs e)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_DatabaseConnectionString);

            try
            {
                if (oSecurity.isPatientLock(_PatientID, true) == false && _PatientID > 0)
                {
                    frmSetupInsurancePlan ofrmSetupInsurancePlan = new frmSetupInsurancePlan(_PatientID, _DatabaseConnectionString);
                    ofrmSetupInsurancePlan.ShowDialog(this);
                    ofrmSetupInsurancePlan.Dispose();
                    ofrmSetupInsurancePlan = null;
                    FillDetails(_PatientID, _FormName, 1, false);
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

        private void btnSearchPatientClaim_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmPatientClaims ofrmPatientClaims = new frmPatientClaims())
                {
                    ofrmPatientClaims.PatientId = _PatientID;
                    ofrmPatientClaims.SelectedClaim = _ClaimSubClaimNo;
                    if (ofrmPatientClaims.ShowDialog(this) == DialogResult.Yes)
                    {
                        _PatientID = ofrmPatientClaims.PatientId;

                        if (ofrmPatientClaims.ClaimNo > 0)
                        { txtPatientSearch.Text = ofrmPatientClaims.SelectedClaim; }
                        else
                        { txtPatientSearch.Text = ""; }
                    }
                    else
                    {
                        return;
                    }
                }

                #region " Load the Selected Claim "

                InstringSearch(txtPatientSearch.Text.Trim());

                FillPatientInsurances(_PatientID);
                FillPatientBalance(_PatientID);

                if (c1PatientDetails.Visible == false)
                {
                    object _sender = (object)txtPatientSearch;
                    KeyPressEventArgs _e = new KeyPressEventArgs((char)13);

                    OnPatientSearchKeyPress(_sender, _e);
                    _e = null;
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #region " Old Code commented by Pankaj Bedse "

            //try
            //{
            //    frmPatientClaims ofrmPatientClaims = new frmPatientClaims();
            //    ofrmPatientClaims.ShowDialog();
            //    //InstringSearch(ofrmPatientClaims.ClaimNo.ToString());
            //    InstringSearch(ofrmPatientClaims.SelectedClaim.ToString());
            //    _PatientID = ofrmPatientClaims.PatientId;
            //    FillPatientInsurances(_PatientID);
            //    FillPatientBalance(_PatientID);
            //    if (c1PatientDetails.Visible == false)
            //    {
            //        //Object snd = ofrmPatientClaims.ClaimNo.ToString();
            //        Object snd = ofrmPatientClaims.SelectedClaim.ToString();
            //        KeyPressEventArgs evt = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));
            //        OnPatientSearchKeyPress(snd, evt);
            //        if (ofrmPatientClaims.ClaimNo > 0)
            //        { 
            //            //txtPatientSearch.Text = ofrmPatientClaims.ClaimNo.ToString(); 
            //            txtPatientSearch.Text = ofrmPatientClaims.SelectedClaim;
            //        }
            //        else
            //        { txtPatientSearch.Text = ""; }
            //    }
            //    ofrmPatientClaims.Dispose();

            //    #region " Load the Selected Claim "

            //    object _sender = (object)txtPatientSearch;
            //    KeyPressEventArgs _e = new KeyPressEventArgs((char)13);

            //    InstringSearch(txtPatientSearch.Text.Trim());
            //    FillPatientInsurances(_PatientID);
            //    FillPatientBalance(_PatientID);

            //    if (c1PatientDetails.Visible == false)
            //        OnPatientSearchKeyPress(_sender, _e);

            //    #endregion

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //finally
            //{ 

            //}

            #endregion
        }
    
        private void txtPatientSearch_MouseDown(object sender, MouseEventArgs e)
        {
            //ContextMenu c = new ContextMenu();
            txtPatientSearch.ContextMenu = null;
            txtPatientSearch.ContextMenuStrip = null;
        }

        private void btn_Alerts_Click(object sender, EventArgs e)
        {
            try
            {
                frmPatientAlerts ofrmPatientAlerts = new frmPatientAlerts(_DatabaseConnectionString, _PatientID);
                ofrmPatientAlerts.ShowDialog(this);
                ofrmPatientAlerts.Dispose();
                FillAlerts_Notes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chk_ClaimNoSearch_CheckedChanged(object sender, EventArgs e)
        {
            txtPatientSearch.Text = "";
        }

        private void btnClaimOnHold_Click(object sender, EventArgs e)
        {
            pnlClaimOnHold.Visible = true;
            btnClaimOnHold.Visible = false;
            //FillClaimOnHold();
        }

        private void btnClaimOnHoldClose_Click(object sender, EventArgs e)
        {
            pnlClaimOnHold.Visible = false;
            btnClaimOnHold.Visible = true;
        }

        private void c1PatientDetails_DoubleClick(object sender, EventArgs e)
        {
            if (c1PatientDetails.Rows.Count > 0)
            {
                if (c1PatientDetails.RowSel > 0)
                {
                    if (c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID) != null && c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID).ToString() != "")
                    {
                        _PatientID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID).ToString());
                        if (FillDetails(_PatientID, _FormName, 1, false) == true)
                        {
                            FillPatientInsurances(_PatientID);
                            FillPatientBalance(_PatientID);
                            FillAlerts_Notes();
                            FillStatementNote();

                            pnlMiddle.Visible = true;
                            pnlAlerts.Visible = _ShowAlerts;
                            c1PatientDetails.Visible = false;

                            OnPatientSearchKeyPress(null, null);
                        }
                    }
                }
            }
        }

        private void c1PatientDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                c1PatientDetails_DoubleClick(null, null);
            }
        }

        private void c1Insurance_CellChanged(object sender, RowColEventArgs e)
        {
            string _fillInsuranceName = "";
            Int64 _fillInsuranceID = 0;
            Int32 _fillInsSelfMode = 0;
            Int64 _fillInsContactID = 0;

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
                    _fillInsContactID = Convert.ToInt64(c1Insurance.GetData(_CurRowIndex, COL_CONTACTID));
                    _selectedPartyNo = Convert.ToInt16(c1Insurance.GetData(_CurRowIndex, COL_PARTY));

                    if (_fillInsSelfMode == 1 && _fillInsuranceName.ToUpper() == "PATIENT")
                    {
                        _fillInsuranceName = "Self";
                    }

                    if (InsuranceSelected != null) { InsuranceSelected(_fillInsuranceID, _fillInsuranceName, _fillInsSelfMode, _fillInsContactID); }
                }
                else
                {
                    _fillInsuranceID = 0;
                    _fillInsuranceName = "";
                    _fillInsSelfMode = 0;
                    _fillInsContactID = 0;
                    _selectedPartyNo = -1;

                    if (InsuranceSelected != null) { InsuranceSelected(_fillInsuranceID, _fillInsuranceName, _fillInsSelfMode, _fillInsContactID); }
                }
            }


        }

        private void c1Insurance_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1PatientDetails_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        #endregion
      
        #region "Insurance Methods"

        private void FillPatientInsurances(Int64 PatientID)
        {
            if (ShowInsurances == false)
            {
                return;
            }

            DataTable dtPatientInsurances = null;
            bool _IsPrimaryPresent = false;
            bool _HasInsurance = true;

            this.c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
            try
            {

                DesignInsuranceGrid();
                c1Insurance.Rows.Count = 1;

                if (_InsuracePaymentType == InsuracePaymentType.Insurace)
                {
                    FillClaimInsurances(ClaimNumber, PatientID);
                    return;
                }
                else if (_InsuracePaymentType == InsuracePaymentType.Patient)
                {
                    FillClaimInsurances(ClaimNumber, PatientID);
                    return;
                }
                else if (_InsuracePaymentType == InsuracePaymentType.Charges)
                {
                    #region "If InsuracePaymentType is set to none means load all insurance as well as self "

                    gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_DatabaseConnectionString);
                    dtPatientInsurances = ogloPatient.getPatientInsurances(_PatientID);
                    ogloPatient.Dispose();

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
                                c1Insurance.SetData(rowIndex, COL_INSURANCENAME, Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceName"])); //
                                c1Insurance.SetData(rowIndex, COL_INSURANCEID, Convert.ToString(dtPatientInsurances.Rows[i]["nInsuranceID"])); //
                                c1Insurance.SetData(rowIndex, COL_INSSELFMODE, "2"); // None = 0,Self = 1,Insurance = 2
                                c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, Convert.ToString(dtPatientInsurances.Rows[i]["sInsuranceFlag"]));
                                if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                                {
                                    c1Insurance.SetData(rowIndex, COL_SELECT, true);
                                    _IsPrimaryPresent = true;
                                }
                                c1Insurance.SetData(rowIndex, COL_INSURANCECOPAYAMT, Convert.ToDecimal(dtPatientInsurances.Rows[i]["CoPay"]));

                                if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bWorkersComp"]) == true)
                                    c1Insurance.SetData(rowIndex, COL_INSURANCEWORKERCOMP, true);

                                if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bAutoClaim"]) == true)
                                    c1Insurance.SetData(rowIndex, COL_INSURANCEAUTOCLAIM, "Auto Claim");

                                c1Insurance.SetData(rowIndex, COL_CONTACTID, Convert.ToString(dtPatientInsurances.Rows[i]["nContactID"])); //

                                c1Insurance.SetData(rowIndex, COL_PARTY, "");
                                c1Insurance.SetData(rowIndex, COL_COPAY, Convert.ToDecimal(Convert.ToString(dtPatientInsurances.Rows[i]["CoPay"])));

                                if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                                {
                                    c1Insurance.SetData(rowIndex, COL_PARTY, "1");
                                }
                                else if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Secondary.GetHashCode())
                                {
                                    c1Insurance.SetData(rowIndex, COL_PARTY, "2");
                                }
                                else if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Tertiary.GetHashCode())
                                {
                                    c1Insurance.SetData(rowIndex, COL_PARTY, "3");
                                }


                            }

                            //if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bPrimaryFlag"]))
                            //{
                            //    c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, "Primary"); //
                            //    c1Insurance.SetData(rowIndex, COL_SELECT, true);
                            //    _IsPrimaryPresent = true;
                            //}

                        }

                    }
                    else
                    {
                        _HasInsurance = false;
                    }


                    c1Insurance.Rows.Add();
                    c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_SELECT, false);//Select-CheckBox
                    c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCENAME, "Self"); //
                    c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSURANCEID, 0); //
                    c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSSELFMODE, "1"); // None = 0,Self = 1,Insurance = 2
                    c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_PARTY, "P");
                    c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_COPAY, "0.00");
                    c1Insurance.Rows[c1Insurance.Rows.Count - 1].AllowEditing = false;

                    c1Insurance.Cols[COL_PARTY].AllowEditing = true;
                    #endregion
                }

                //Check if Insurance/Insurances Present for Patient & has primary insurance is
                //not set then select the 1st Insurance in grid
                if (_HasInsurance == true && _IsPrimaryPresent == false)
                {
                    c1Insurance.SetCellCheck(1, COL_SELECT, CheckEnum.Checked);
                }
                else if (_HasInsurance == false)
                {
                    if (c1Insurance.Rows.Count > 1)
                    { c1Insurance.SetCellCheck((c1Insurance.Rows.Count - 1), COL_SELECT, CheckEnum.Checked); }
                }
                //

                if (_InsuracePaymentType == InsuracePaymentType.Patient)
                {
                    c1Insurance.Cols[COL_SELECT].AllowEditing = false;
                }
                else
                {
                    c1Insurance.Cols[COL_SELECT].AllowEditing = AllowEditingParty; //true;
                }
                c1Insurance.Cols[COL_INSURANCETYPE].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCENAME].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCEID].AllowEditing = false;
                c1Insurance.Cols[COL_INSSELFMODE].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCECOPAYAMT].AllowEditing = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                this.c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                if (dtPatientInsurances != null) { dtPatientInsurances.Dispose(); }
            }
        }

        public void FillClaimInsurances(Int64 ClaimNo, Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtPatientInsurances = null;
            //bool _IsPrimaryPresent = false;
            //bool _HasInsurance = true;
            int _ClaimInsCounter = 0;

            try
            {
                DesignInsuranceGrid();
                c1Insurance.Rows.Count = 1;

                if (ClaimNumber > 0 && PatientId > 0)
                {
                    oDB.Connect(false);
                    oParameters.Clear();
                    oParameters.Add("@nPatientID", PatientId, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    oParameters.Add("@nClaimNo", ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                    oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0)
                    oDB.Retrive("BL_SELECT_CLAIM_INSURANCES", oParameters, out dtPatientInsurances);
                    oDB.Disconnect();


                    //if (_InsuracePaymentType == InsuracePaymentType.Insurace)
                    {
                        #region "If InsuracePaymentType is set to INSURANCE means load only insurance not self "

                        if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                        {
                            int _responsiblityType = 0;

                            //nInsuranceID,InsuranceName ,sSubscriberID,sSubscriberName,sSubscriberPolicy#,sGroup,
                            //sPhone ,bPrimaryFlag,dtDOB,dtEffectiveDate,dtExpiryDate,sSubscriberID
                            for (int i = 0; i < dtPatientInsurances.Rows.Count; i++)
                            {
                                _responsiblityType = Convert.ToInt32(dtPatientInsurances.Rows[i]["nResponsibilityType"]);

                                //Ref. PayerMode enum in billing for Responsiblity Type as  1 = Patient , 2 = Insurance
                                if (_responsiblityType == 2)
                                {
                                    _ClaimInsCounter = _ClaimInsCounter + 1;

                                    c1Insurance.Rows.Add();
                                    int rowIndex = c1Insurance.Rows.Count - 1;
                                    c1Insurance.SetData(rowIndex, COL_SELECT, false);//Select-CheckBox
                                    c1Insurance.SetData(rowIndex, COL_INSURANCENAME, Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceName"])); //
                                    c1Insurance.SetData(rowIndex, COL_INSURANCEID, Convert.ToString(dtPatientInsurances.Rows[i]["nInsuranceID"])); //
                                    c1Insurance.SetData(rowIndex, COL_INSURANCETYPE, Convert.ToString(dtPatientInsurances.Rows[i]["sInsuranceFlag"]));

                                    //...With existing logic we use to select the patient primary insurance on the payment form
                                    //...for now we will select the current party for the claim on insurance claim

                                    //if (Convert.ToInt32(dtPatientInsurances.Rows[i]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                                    //{ c1Insurance.SetData(rowIndex, COL_SELECT, true); _IsPrimaryPresent = true; }

                                    //

                                    c1Insurance.SetData(rowIndex, COL_INSURANCECOPAYAMT, Convert.ToDecimal(dtPatientInsurances.Rows[i]["CoPay"]));
                                    if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bWorkersComp"]) == true)
                                    { c1Insurance.SetData(rowIndex, COL_INSURANCEWORKERCOMP, true); }
                                    if (Convert.ToBoolean(dtPatientInsurances.Rows[i]["bAutoClaim"]) == true)
                                    { c1Insurance.SetData(rowIndex, COL_INSURANCEAUTOCLAIM, "Auto Claim"); }
                                    c1Insurance.SetData(rowIndex, COL_CONTACTID, Convert.ToString(dtPatientInsurances.Rows[i]["nContactID"])); //
                                    c1Insurance.SetData(rowIndex, COL_COPAY, Convert.ToDecimal(Convert.ToString(dtPatientInsurances.Rows[i]["CoPay"])));
                                    c1Insurance.SetData(rowIndex, COL_PARTY, Convert.ToInt64(dtPatientInsurances.Rows[i]["nResponsibilityNo"]));
                                    c1Insurance.SetData(rowIndex, COL_INSSELFMODE, Convert.ToInt32(dtPatientInsurances.Rows[i]["nResponsibilityType"])); // None = 0,Self = 1,Insurance = 2

                                    if (_ClaimInsCounter == 2) { _HasSecondaryInsOnClaim = true; }
                                }
                                else if (_responsiblityType == 1) //Patient or Self
                                {
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
                                    c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_INSSELFMODE, Convert.ToInt32(dtPatientInsurances.Rows[i]["nResponsibilityType"])); // None = 0,Self = 1,Insurance = 2
                                    c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_PARTY, Convert.ToInt64(dtPatientInsurances.Rows[i]["nResponsibilityNo"]));
                                    c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_COPAY, "");
                                    c1Insurance.Rows[c1Insurance.Rows.Count - 1].AllowEditing = false;
                                }
                            }
                        }
                        else
                        {
                            // _HasInsurance = false;

                            //..If no insurances are present for the claim load the Self entry
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
                            c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_PARTY, "1");
                            c1Insurance.SetData(c1Insurance.Rows.Count - 1, COL_COPAY, "");
                            c1Insurance.Rows[c1Insurance.Rows.Count - 1].AllowEditing = false;

                        }

                        #endregion
                    }

                    #region " .... Select the current party for the claim .... "

                    //string _sqlQuery = "";
                    DataTable _dtParty = null;

                    //_sqlQuery =
                    //" SELECT DISTINCT ISNULL(nNextActionPartyNumber,0) AS CurrentPartyNo, " +
                    //" ISNULL(nNextActionPatientInsID,0) AS InsuranceId, " +
                    //" ISNULL(nNextActionContactID,0) AS ContactId " +
                    //" FROM BL_EOB_NextAction " +
                    //" WHERE nID = (SELECT MAX(nID) FROM  BL_EOB_NextAction where nClaimNo = " + ClaimNumber + " "+
                    //" AND nClinicID = " + _ClinicID + " AND ISNULL(bIsVoid,0) <> 1) ";

                    string _subClmNo = string.Empty;

                    if (!SubClaimNumber.Equals(0))
                    { _subClmNo = SubClaimNumber.ToString(); }

                    oParameters.Clear();
                    oParameters.Add("@nClaimno", ClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sSubClaimno", _subClmNo, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDB.Connect(false);
                    oDB.Retrive("BL_Select_SplitClaims", oParameters, out _dtParty);
                    oDB.Disconnect();

                    //Commented on 20100323
                    ////if (_subClmNo != string.Empty)
                    ////{
                    ////    _sqlQuery = " SELECT ISNULL(nResponsibilityNo,0) as CurrentPartyNo," +
                    ////           " ISNULL(nContactID,0) as ContactId, " +
                    ////           " ISNULL(nInsuranceID,0) as InsuranceId " +
                    ////           " FROM dbo.BL_Transaction_Claim_MST " +
                    ////           " WHERE nClaimNo = " + ClaimNumber + " AND nSubClaimNo = '" + _subClmNo + "'" +
                    ////           " AND nClinicID = " + _ClinicID + " AND ISNULL(bIsVoid,0) <> 1";
                    ////}
                    ////else
                    ////{
                    ////    _sqlQuery = " SELECT ISNULL(nResponsibilityNo,0) as CurrentPartyNo," +
                    ////           " ISNULL(nContactID,0) as ContactId, " +
                    ////           " ISNULL(nInsuranceID,0) as InsuranceId " +
                    ////           " FROM dbo.BL_Transaction_Claim_MST " +
                    ////           " WHERE nClaimNo = " + ClaimNumber + " " +
                    ////           " AND ISNULL(nClaimStatus,0) = 1" + //...1 - Open 2- Close
                    ////           " AND nClinicID = " + _ClinicID + " AND ISNULL(bIsVoid,0) <> 1";
                    ////}
                    //END - Commented on 20100323





                    //if (SubClaimNumber > 0)
                    //{
                    //    _subClmNo = SubClaimNumber.ToString();

                    //    _sqlQuery =
                    //    " SELECT DISTINCT ISNULL(nNextActionPartyNumber,0) AS CurrentPartyNo, " +
                    //    " ISNULL(nNextActionPatientInsID,0) AS InsuranceId, " +
                    //    " ISNULL(nNextActionContactID,0) AS ContactId " +
                    //    " FROM BL_EOB_NextAction " +
                    //    " WHERE nID = (SELECT MAX(nID) FROM  BL_EOB_NextAction where nClaimNo = " + ClaimNumber + " AND sSubClaimNo = '" + _subClmNo.Trim() + "' " +
                    //    " AND nClinicID = " + _ClinicID + " AND ISNULL(bIsVoid,0) <> 1) ";
                    //}
                    //else
                    //{
                    //    _sqlQuery =
                    //    " SELECT DISTINCT ISNULL(nNextActionPartyNumber,0) AS CurrentPartyNo, " +
                    //    " ISNULL(nNextActionPatientInsID,0) AS InsuranceId, " +
                    //    " ISNULL(nNextActionContactID,0) AS ContactId " +
                    //    " FROM BL_EOB_NextAction " +
                    //    " WHERE nID = (SELECT MAX(nID) FROM  BL_EOB_NextAction where nClaimNo = " + ClaimNumber + " " +
                    //    " AND nClinicID = " + _ClinicID + " AND ISNULL(bIsVoid,0) <> 1) ";
                    //}


                    //Commented on 20100323
                    ////oDB.Connect(false);
                    ////oDB.Retrive_Query(_sqlQuery, out _dtParty);
                    ////oDB.Disconnect();
                    //END - Commented on 20100323

                    if (_dtParty != null && _dtParty.Rows.Count > 0)
                    {
                        if (_dtParty.Rows[0]["CurrentPartyNo"] != null && Convert.ToString(_dtParty.Rows[0]["CurrentPartyNo"]).Trim() != "" && Convert.ToInt64(_dtParty.Rows[0]["CurrentPartyNo"]) > 0)
                        {
                            if (c1Insurance != null && c1Insurance.Rows.Count > 1)
                            {
                                for (int rIndex = 1; rIndex < c1Insurance.Rows.Count; rIndex++)
                                {
                                    if (
                                         Convert.ToInt64(_dtParty.Rows[0]["InsuranceId"]) == Convert.ToInt64(c1Insurance.GetData(rIndex, COL_INSURANCEID))
                                         && Convert.ToInt64(_dtParty.Rows[0]["InsuranceId"]) != 0
                                       )
                                    {
                                        try
                                        {
                                            this.c1Insurance.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged);
                                            c1Insurance.SetData(rIndex, COL_SELECT, true);
                                            _selectedPartyNo = Convert.ToInt16(c1Insurance.GetData(rIndex, COL_PARTY));
                                            break;
                                        }
                                        catch (Exception)// ex)
                                        {
                                            //ex.ToString();
                                            //ex = null;
                                        }
                                        finally
                                        { this.c1Insurance.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_CellChanged); }
                                    }
                                }
                            }
                        }
                    }
                    if (_dtParty != null)
                    {
                        _dtParty.Dispose();
                        _dtParty = null;
                    }

                    #endregion " .... Select the current party for the claim .... "

                    if (_InsuracePaymentType == InsuracePaymentType.Patient)
                    { c1Insurance.Cols[COL_SELECT].AllowEditing = false; }
                    else
                    {
                        c1Insurance.Cols[COL_SELECT].AllowEditing = AllowEditingParty; //true; 
                    }
                }

                c1Insurance.Cols[COL_INSURANCETYPE].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCENAME].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCEID].AllowEditing = false;
                c1Insurance.Cols[COL_INSSELFMODE].AllowEditing = false;
                c1Insurance.Cols[COL_INSURANCECOPAYAMT].AllowEditing = false;

                c1Insurance.Cols[COL_INSURANCENAME].TextAlign = TextAlignEnum.LeftCenter;

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (dtPatientInsurances != null) { dtPatientInsurances.Dispose(); }
            }

        }

        public void SetInsurance(Int64 InsuranceID)
        {

            if (InsuranceID >= 0)
            {
                //select the corresponding insurance in the Patient Insurance Grid Above
                if (c1Insurance != null && c1Insurance.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCEID)) != "")
                        {
                            if (Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID)) == InsuranceID)
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
        }

        public void SetInsurance(Int64 InsuranceID, bool InsuranceFormView)
        {
            Int32 _responsiblityType = 0;

            if (InsuranceID > 0)
            {
                //select the corresponding insurance in the Patient Insurance Grid Above
                if (c1Insurance != null && c1Insurance.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        if (c1Insurance.GetData(i, COL_INSSELFMODE) != null && Convert.ToString(c1Insurance.GetData(i, COL_INSSELFMODE)).Trim() != "")
                        { _responsiblityType = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE)); }

                        //Ref. PayerMode enum in billing for Responsiblity Type as  1 = Patient , 2 = Insurance
                        if (_responsiblityType == 2)
                        {
                            if (Convert.ToString(c1Insurance.GetData(i, COL_INSURANCEID)) != "")
                            {
                                if (Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID)) == InsuranceID)
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
            //else
            //{
            //    //Uncheck all 
            //    if (c1Insurance != null && c1Insurance.Rows.Count > 0)
            //    {
            //        for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
            //        {
            //            c1Insurance.SetCellCheck(i, COL_SELECT, CheckEnum.Unchecked);
            //        }
            //    }

            //}
        }

        public string GetInsuranceName(int Party)
        {
            string _InsuranceCompanyName = string.Empty;

            if (c1Insurance.Rows.Count > 0)
            {
                for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                {
                    int _party = Convert.ToInt32(c1Insurance.GetData(i, COL_PARTY));
                    if (Party.Equals(_party))
                    {
                        _InsuranceCompanyName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                        break;
                    }
                }
            }
            return _InsuranceCompanyName;
        }

        public void GetSelectedInsurance(out Int64 InsuranceID, out string InsuranceName, out Int32 InsuraceSelfMode, out Int64 ContactID)
        {
            string _fillInsuranceName = "";
            Int64 _fillInsuranceID = 0;
            Int32 _fillInsSelfMode = 0;
            Int64 _fillInsContactID = 0;

            if (c1Insurance.Rows.Count > 0)
            {
                for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                {
                    if (c1Insurance.GetCellCheck(i, COL_SELECT) == CheckEnum.Checked)
                    {
                        _fillInsuranceID = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                        _fillInsuranceName = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                        _fillInsSelfMode = Convert.ToInt32(c1Insurance.GetData(i, COL_INSSELFMODE));
                        _fillInsContactID = Convert.ToInt64(c1Insurance.GetData(i, COL_CONTACTID));
                        if (_fillInsSelfMode == 1 && _fillInsuranceName.ToUpper() == "PATIENT")
                        {
                            _fillInsuranceName = "Self";
                        }
                        break;
                    }
                }
            }

            InsuranceID = _fillInsuranceID;
            InsuranceName = _fillInsuranceName;
            InsuraceSelfMode = _fillInsSelfMode;
            ContactID = _fillInsContactID;

        }

        public Int64 GetInsurance(out DataTable Insurance)
        {
            Int64 _selected = 0;
            DataTable _dtInsurance = new DataTable("Insurance");
            try
            {
                _dtInsurance.Columns.Add("InsuranceID", typeof(System.Int64));
                _dtInsurance.Columns.Add("ContactID", typeof(System.Int64));
                _dtInsurance.Columns.Add("InsuranceTypeFlag", typeof(System.Int64));
                _dtInsurance.Columns.Add("InsuranceType", typeof(System.Int64));
                _dtInsurance.Columns.Add("InsuranceName", typeof(System.String));
                if (c1Insurance.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        //int _InsType = 0;
                        if (Convert.ToString(c1Insurance.GetData(i, COL_PARTY)) == "1")
                        {
                            _selected = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                        }
                        DataRow _dr = _dtInsurance.NewRow();
                        _dr["InsuranceID"] = Convert.ToInt64(c1Insurance.GetData(i, COL_INSURANCEID));
                        _dr["ContactID"] = Convert.ToInt64(c1Insurance.GetData(i, COL_CONTACTID));

                        //string _InsuranceTypeFlag =Convert.ToString(c1Insurance.GetData(i, COL_INSURANCETYPE));

                        //if (_InsuranceTypeFlag.ToString() == InsuranceTypeFlag.Primary.ToString())
                        //{ _dr["InsuranceTypeFlag"] = InsuranceTypeFlag.Primary.GetHashCode(); }
                        //else if (_InsuranceTypeFlag.ToString() == InsuranceTypeFlag.Secondary.ToString())
                        //{ _dr["InsuranceTypeFlag"] = InsuranceTypeFlag.Secondary.GetHashCode(); }
                        //else if (_InsuranceTypeFlag.ToString() == InsuranceTypeFlag.Tertiary.ToString())
                        //{ _dr["InsuranceTypeFlag"] = InsuranceTypeFlag.Tertiary.GetHashCode(); }
                        //else
                        //{ _dr["InsuranceTypeFlag"] = InsuranceTypeFlag.None.GetHashCode(); }

                        switch (Convert.ToString(c1Insurance.GetData(i, COL_PARTY)).Trim())
                        {
                            case "1":
                                _dr["InsuranceTypeFlag"] = InsuranceTypeFlag.Primary.GetHashCode();
                                break;
                            case "2":
                                _dr["InsuranceTypeFlag"] = InsuranceTypeFlag.Secondary.GetHashCode();
                                break;
                            case "3":
                                _dr["InsuranceTypeFlag"] = InsuranceTypeFlag.Tertiary.GetHashCode();
                                break;
                            case "P":
                                _dr["InsuranceTypeFlag"] = InsuranceTypeFlag.None.GetHashCode();
                                break;
                        }

                        _dr["InsuranceType"] = Convert.ToInt64(c1Insurance.GetData(i, COL_INSSELFMODE));
                        _dr["InsuranceName"] = Convert.ToString(c1Insurance.GetData(i, COL_INSURANCENAME));
                        _dtInsurance.Rows.Add(_dr);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                Insurance = _dtInsurance;
            }
            return _selected;
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

        public DataTable GetCurrentClaimParty()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable _dtParty = null;

            try
            {
                #region " .... Select the current party for the claim .... "

                _sqlQuery =
               " SELECT DISTINCT ISNULL(nNextActionPartyNumber,0) AS CurrentPartyNo, " +
               " ISNULL(nNextActionPatientInsID,0) AS InsuranceId, " +
               " ISNULL(nNextActionContactID,0) AS ContactId " +
               " FROM BL_EOB_NextAction  WITH (NOLOCK) " +
               " WHERE nID = (SELECT MAX(nID) FROM  BL_EOB_NextAction  WITH (NOLOCK) where nClaimNo = " + ClaimNumber + " " +
               " AND nClinicID = " + _ClinicID + " AND ISNULL(bIsVoid,0) <> 1) ";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtParty);
                oDB.Disconnect();

                #endregion " .... Select the current party for the claim .... "
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _dtParty;
        }

        #endregion
       
        #region "Show/Hide Details"

        /// <summary>
        /// Use this method from Parent form to Enable Up/Down functionality
        /// </summary>
        public void ShowUpDown()
        {
            btnUP.Visible = true;
            btnDown.Visible = false;
        }

        private void ShowPatientDetailOnStrip()
        {
            pnlPatientOnStrip.Show();
            if (patintNameToolTip == null)
            {
                patintNameToolTip = new ToolTip();
            }
            patintNameToolTip.SetToolTip(lblPatNameNCode, lblPatNameNCode.Text);
        }

        private void HidePatientDetailOnStrip()
        {
            pnlPatientOnStrip.Hide();
        }

        private void btnUP_Click(object sender, EventArgs e)
        {

            pnlAlerts.Visible = false;
            pnlMiddle.Visible = false;

            this.Height = pnlTop.Height;

            btnUP.Visible = false;
            btnDown.Visible = true;
            ControlSize_Changed(sender, e);
            ShowPatientDetailOnStrip();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            pnlAlerts.Visible = _ShowAlerts;
            pnlMiddle.Visible = true;
            this.Height = StripHeight;
            btnUP.Visible = true;
            btnDown.Visible = false;
            HidePatientDetailOnStrip();

            ControlSize_Changed(sender, e);
            Application.DoEvents();
        }

        #endregion

        #region "Designer Events"

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

        private void btn_ModityPatient_MouseHover(object sender, EventArgs e)
        {
            //btn_ModityPatient.BackgroundImage = global::gloPatientStripControl.Properties.Resources.PatientHover;
            //btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;

            //toolTip1  = new ToolTip();
            //toolTip1.SetToolTip(btn_ModityPatient,"Modify Patient");

        }

        private void btn_ModityPatient_MouseLeave(object sender, EventArgs e)
        {
            btn_ModityPatient.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Patient;
            btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUP_MouseHover(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloPatientStripControl.Properties.Resources.UPHover;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUP_MouseLeave(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloPatientStripControl.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloPatientStripControl.Properties.Resources.DownHover;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void pnlDate_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearchPatientClaim_MouseHover(object sender, EventArgs e)
        {
            btnSearchPatientClaim.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Img_Yellow;
            btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
            //btnSearchPatientClaim.ForeColor = Color.FromArgb(31, 73, 125);
        }

        private void btnSearchPatientClaim_MouseLeave(object sender, EventArgs e)
        {
            btnSearchPatientClaim.BackgroundImage = null;
            btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
            //btnSearchPatientClaim.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Img_Button;
            //btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
            // btnSearchPatientClaim.ForeColor = Color.White;
        }

        #endregion
      
    }
}
