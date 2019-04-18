 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;


namespace gloCardPatientStripControl
{
    /// <summary>
    /// Mitesh Patel
    /// Due to Circular refference of gloPatient & gloPatientstrip in glocardScanning
    /// we have added New user control (gloCardPatientStripControl). 
    /// </summary>
    /// 
   
               
    //Enumerating the Forms where the Patient Strip is to be shown.
    public enum FormName
    { 
        None=0,
        Schedule=1,
        Billing=2,
        Temp=3,
        Appointment=4
    }

    public enum InsuracePaymentType
    {
        None = 0, Patient = 1, Insurace = 2, Charges = 3
    }
   

    public partial class gloCardPatientStripControl : UserControl
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

        public gloCardPatientStripControl()
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

        public gloCardPatientStripControl(string connectionstring)
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

        public gloCardPatientStripControl(string connectionstring, Boolean AllowPatientSearch)
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

        public gloCardPatientStripControl(string connectionstring, Boolean AllowPatientSearch, Boolean AllowClaimNoSearch)
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

        public gloCardPatientStripControl(string connectionstring, Boolean AllowPatientSearch, Boolean AllowClaimNoSearch, Boolean IsHCFA1500)
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

        public gloCardPatientStripControl(string connectionstring, Boolean AllowPatientSearch, Boolean AllowClaimNoSearch, Boolean IsHCFA1500,bool DefaultToClaimNoSearch)
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

            //chk_ClaimNoSearch.Checked = DefaultToClaimNoSearch;
        }

        #endregion

        #region Private Variables

       // DataView dvPatient = new DataView ();
       // DataTable dtTemp = new DataTable();
       // DataView dvNext = new DataView();

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
        //Sanjog - Added on 2011 may 20 for show age in days as per setting 
        private bool _ShowAgeInDays = false;
        private int _AgeLimit = 0;
        //Sanjog - Added on 2011 may 20 for show age in days as per setting 

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
        //private bool _ShowNotesAlerts = false;

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
        //gloUserRights.ClsgloUserRights oClsgloUserRights = null;

        private bool _viewSearchOptionCheckBox = true;
        private InsuracePaymentType _InsuracePaymentType = InsuracePaymentType.None;
        private bool _showUpDown = false;


        //MaheshB For Appointment

        private FormName _FormName = FormName.None;//Check  Comment To Defination.
        
        private int _selectedPartyNo = -1;

        private Boolean _IsSearchOnPatientCode = false;

        private bool _HasSecondaryInsOnClaim = false;

        //ToolTip btnToolTip;
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
                catch (Exception)// ex)
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

      

        public bool ShowUpDown
        {
            get { return _showUpDown; }
            set
            {
                _showUpDown = value;
            }
        }

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
           
        }

        public bool ViewSearchOptionCheckBox
        {
            set
            {
                _viewSearchOptionCheckBox = value;
               
            }
        }

        public void SetInsuracePaymentType(InsuracePaymentType mInsuracePaymentType)
        {
            _InsuracePaymentType = mInsuracePaymentType;
        }

        public void SetClaimNoSearch(bool ClaimNoCheckValue)
        {
            
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
 //       public event ControlSizeChanged ControlSize_Changed;

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
     //   public event txtPatientSearchTextChanged txtPatientSearchTextChanged1;

        public delegate void PatientSearchKeyPressHandler(object sender, KeyPressEventArgs e);
     //   public event PatientSearchKeyPressHandler OnPatientSearchKeyPress;

        //public delegate void AfterPatientModified(object sender, EventArgs e);
        //public event AfterPatientModified After_PatientModified;

        public delegate void Patient_Modified(object sender, EventArgs e);
    //    public event Patient_Modified PatientModified;

        public delegate void Insurance_Selected(Int64 InsuranceID, string InsuranceName, Int32 InsuraceSelfMode, Int64 ContactID);
      //  public event Insurance_Selected InsuranceSelected;
        
        #endregion

        public void SelectSearchBox()
        {
           
        }
        
        private void gloPatientStripControl_Load(object sender, EventArgs e)
        {
            try
            {
                            
                //if (_IsHCFA1500 == true)
                //{
                   
                //}
                //else
                //{
                //    if (_DefaultClaimNoSearch == true)
                //    {
                       
                //    }
                //}

               
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
            //        StripHeight = StripHeight;
                }

                this.Height = StripHeight;
                //Assign User Rights 20090720
                AssignUserRights();


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

        public void FillDetails(Int64 PatientID, FormName CallingFormName, Int64 nProviderid, bool blnflag)
        {
            //Start/04-03-2011 : Commperss
            DataTable dt = null;
            DataTable dtGuarantor = null;
            //End/04-03-2011 : Commperss

            try
            {

                if (pnl_Main.Width > 0)
                {
                    pnlLeft.Width = pnl_Main.Width / 2;
                    pnlRight.Width = pnl_Main.Width - pnlLeft.Width - 2;
                }

                _PatientID = PatientID;
               
                //Declare class object to retrieve data
                using (gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString))
                {
                    //oDB.Connect(false);
                    string _strQuery = "";


                    //-------------------------------Provider

                    //oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                    if (oDB.Connect(false))
                    {
                        //dt = new DataTable();//04-03-2011 : Commperss

                        _strQuery = "SELECT Patient.sPatientCode AS PatientCode, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') "
                     + " AS PatientName, Patient.dtDOB AS DOB, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) "
                     + " + ISNULL(Provider_MST.sLastName, '') AS PrName,  ISNULL(Patient.sPhone, '') AS PatPhone, ISNULL(Patient.sOccupation, '') "
                     + " AS PatientOccupation, ISNULL(Patient.sMobile, '') AS PatientCellPhone, ISNULL(Patient.nSSN, '') AS SSN, ISNULL(Patient.sGender, '') AS Gender, "
                     + " ISNULL(Patient.sMaritalStatus, '') AS sMaritalStatus, ISNULL(Patient.sHandDominance, '') AS HandDominance,ISNULL(Patient.sGuarantor,'') AS Guarantor, "
                     + " isnull(Provider_MST.sFirstName,'') + space(1) + isnull(Provider_MST.sMiddleName,'') + space(1) + isnull(Provider_MST.sLastName,'') AS ProviderName, isnull(Provider_MST.nProviderID,0) AS ProviderID "
                     + " FROM Patient LEFT OUTER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                     + " WHERE Patient.nPatientID = " + _PatientID + " AND Patient.nClinicID = " + _ClinicID + "";

                        //dt = new DataTable();
                        oDB.Retrive_Query(_strQuery, out dt);

                        _strQuery = "";
                        _strQuery = "SELECT ISNULL(Patient_OtherContacts.sFirstName,'') + SPACE(1) + ISNULL(Patient_OtherContacts.sMiddleName,'') + SPACE(1)+ ISNULL(Patient_OtherContacts.sLastName,'') AS Guarantor "
                                  + " FROM Patient LEFT JOIN Patient_OtherContacts ON Patient.nPatientID = Patient_OtherContacts.nPatientID "
                                  + " WHERE Patient.nPatientID = " + _PatientID + " AND Patient.nClinicID =" + _ClinicID + " AND (Patient_OtherContacts.nPatientContactTypeFlag = 1 OR Patient_OtherContacts.nPatientContactTypeFlag  IS NULL )  ";
                        //DataTable dtGuarantor;//04-03-2011 : Commperss
                        object _ResultGuarantor;
                        dtGuarantor = new DataTable();

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
                            //lblDOB.Text = _DateOfBirth.ToShortDateString();
                            lblDOB.Text = _DateOfBirth.ToString("MM/dd/yyyy");
                            lblGender.Text = _Gender;
                            lblPatientCode.Text = _PatientCode;
                            lblPatientName.Text = _PatientName;
                            lblProvider.Text = _ProviderName;
                            lblPhone.Text = _PatientHomePhone;
                            lblGuarantor.Text = _Guarantor;

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



                        if (CallingFormName == FormName.Billing)
                        {

                        }
                    }//if //04-03-2011 : Commperss
                    _strQuery = null;
                }//using //04-03-2011 : Commperss
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            //Start/04-03-2011 : Commperss
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (dtGuarantor != null)
                {
                    dtGuarantor.Dispose();
                    dtGuarantor = null;
                }
            }
            //End/04-03-2011 : Commperss
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



               // gloC1FlexStyle.Style(c1PatientDetails, false);
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

        public static string FormatAge_Old(DateTime start, DateTime end)
        {

            // Compute the difference between start

            //year and end year.

            int years = end.Year - start.Year;

            int months = 0;

            int days = 0;

            // Check if the last year was a full year.

            if (end < start.AddYears(years) && years != 0)
            {

                --years;

            }

            start = start.AddYears(years);

            // Now we know start <= end and the diff between them

            // is < 1 year.

            if (start.Year == end.Year)
            {

                months = end.Month - start.Month;

            }

            else
            {

                months = (12 - start.Month) + end.Month;

            }

            // Check if the last month was a full month.

            if (end < start.AddMonths(months) && months != 0)
            {

                --months;

            }

            start = start.AddMonths(months);

            // Now we know that start < end and is within 1 month

            // of each other.

            days = (end - start).Days;

            return years + " years " + months

               + " months " + days + " days";

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

            //Sanjog - Added on 2011 may 20 for show age in days as per setting 
            GetAgeSetting();
            if (_ShowAgeInDays == true && _AgeLimit >= DateTime.Now.Subtract(_DateOfBirth).Days)
            {
            }
            else
            {
                if (months > 0)
                {
                    if (_AgeStr.IndexOf("D") != -1)
                    {
                        _AgeStr = _AgeStr.Remove(_AgeStr.IndexOf("D") - 3);
                    }
                }
            }
            //Sanjog - Added on 2011 may 20 for show age in days as per setting 

            return _AgeStr;
        }
        //Sanjog - Added on 2011 may 20 for show age in days as per setting 
        public void GetAgeSetting()
        {
            string strQry;
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            try
            {
                if (oDB.Connect(false))
                {
                    strQry = "select sSettingsName, ISNULL(sSettingsValue,'') AS sSettingsValue from settings where sSettingsName='SHOW AGE IN DAYS' or sSettingsName='AGE LIMIT'";
                    oDB.Retrive_Query(strQry, out dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0][0]) == "SHOW AGE IN DAYS")
                        {
                            if (Convert.ToInt16(dt.Rows[0][1]) == 0)
                                _ShowAgeInDays = false;
                            else
                                _ShowAgeInDays = true;
                        }
                        else
                        {
                            if (Convert.ToInt16(dt.Rows[1][1]) == 0)
                                _ShowAgeInDays = false;
                            else
                                _ShowAgeInDays = true;
                        }

                        if (Convert.ToString(dt.Rows[0][0]) == "AGE LIMIT")
                        {
                            _AgeLimit = Convert.ToInt16(dt.Rows[0][1]) * 365;
                        }
                        else
                        {
                            _AgeLimit = Convert.ToInt16(dt.Rows[1][1]) * 365;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                strQry = null;
                if (dt != null) { dt.Dispose(); dt = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }
        //Sanjog - Added on 2011 may 20 for show age in days as per setting 
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

               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                
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
                    //oClsgloUserRights = new gloUserRights.ClsgloUserRights(_DatabaseConnectionString);
                    //oClsgloUserRights.CheckForUserRights(_UserName);
                    //btn_ModityPatient.Enabled = oClsgloUserRights.ModifyPatient;
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        #endregion

        #endregion
      
       
      

        #region "Designer Events"

        private void btn_ModityPatient_MouseHover(object sender, EventArgs e)
        {
            //btn_ModityPatient.BackgroundImage = global::gloCardPatientStripControl.Properties.Resources.PatientHover;
            //btn_ModityPatient.BackgroundImageLayout = ImageLayout.Center;

            //toolTip1  = new ToolTip();
            //toolTip1.SetToolTip(btn_ModityPatient,"Modify Patient");

        }

        private void btn_ModityPatient_MouseLeave(object sender, EventArgs e)
        {
            //btn_ModityPatient.BackgroundImage = global::gloCardPatientStripControl.Properties.Resources.Patient;
            
        }

        private void btnUP_MouseHover(object sender, EventArgs e)
        {
            //btnUP.BackgroundImage = global::gloCardPatientStripControl.Properties.Resources.UPHover;
           
        }

        private void btnUP_MouseLeave(object sender, EventArgs e)
        {
            //btnUP.BackgroundImage = global::gloCardPatientStripControl.Properties.Resources.UP;
           
        }

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            //btnDown.BackgroundImage = global::gloCardPatientStripControl.Properties.Resources.DownHover;
           
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            //btnDown.BackgroundImage = global::gloCardPatientStripControl.Properties.Resources.Down;
           
        }

        private void pnlDate_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

       

    

        //private void btnSearchPatientClaim_MouseLeave(object sender, EventArgs e)
        //{
           
        //    //btnSearchPatientClaim.BackgroundImage = global::gloCardPatientStripControl.Properties.Resources.Img_Button;
        //    //btnSearchPatientClaim.BackgroundImageLayout = ImageLayout.Stretch;
        //   // btnSearchPatientClaim.ForeColor = Color.White;
        //}

        //private void c1Insurance_MouseMove(object sender, MouseEventArgs e)
        //{
           
        //}

        //private void c1PatientDetails_MouseMove(object sender, MouseEventArgs e)
        //{
            
        //}

        //private void txtPatientSearch_MouseDown(object sender, MouseEventArgs e)
        //{
        //    ContextMenu c = new ContextMenu();
           
        //}

        //private void btn_Alerts_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
               
                
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void btn_Notes_Click(object sender, EventArgs e)
        //{

        //}

        //private void btn_CliamOnHold_Click(object sender, EventArgs e)
        //{

        //}

        

        //private void btn_Alerts_MouseHover(object sender, EventArgs e)
        //{
        //    //btnToolTip=new ToolTip();
        //    //btnToolTip.SetToolTip(btn_Alerts, "Modify Alerts");
        //}


       

        //private void btnClaimOnHold_Click(object sender, EventArgs e)
        //{
         
        //    //FillClaimOnHold();
        //}

        //private void btnClaimOnHold_MouseHover(object sender, EventArgs e)
        //{
        //    //btnToolTip = new ToolTip();
        //    //btnToolTip.SetToolTip(btnClaimOnHold, "View Claim #");
        //}

        //private void btnClaimOnHoldClose_Click(object sender, EventArgs e)
        //{
           
        //}

        //private void lblAlerts_MouseHover(object sender, EventArgs e)
        //{
        //    //btnToolTip = new ToolTip();
        //    //btnToolTip.SetToolTip(lblAlerts, _StrPatientAlert);
        //}

        //private void label6_Click(object sender, EventArgs e)
        //{

        //}

       
       
    }
}
