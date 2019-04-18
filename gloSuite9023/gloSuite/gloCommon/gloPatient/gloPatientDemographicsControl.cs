using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using gloContacts;
using gloAddress;
using gloPatient.Classes;
using gloPictureBox;
using System.Drawing.Imaging;


namespace gloPatient
{
    public partial class gloPatientDemographicsControl : UserControl
    {
        #region "Declarations"
        private bool gblnYesNoLab = false; //''YES/No Labs
        //private bool glbIsPediatric = false;//Pediatric Settings (Now it not required for the pedetric validation)
        //Added for Bug #77539: 00000115 : Patient Setup
        private bool _ispicPAPhotomodified = false;
        //**
        private ComboBox combo;
        public bool _IsAuditLogGetData = false;
        public bool _IsAuditLogModified = false;
        //**
        private string _databaseconnectionstring = "";
        private bool _isRefferalsModified = false;
        private bool _isCareTeamModified = false;
        private bool _isPrimaryCarePhysicianModified = false;
        private bool _isPharmacyModified = false;
        private bool _isInsurenceModified = false;
        private bool _isGaurdianModified = false;
        private bool _isOccupationModified = false;
        private bool _isGaurentorModified = false;
        private bool _isPatientOtherModified = false;
        //Patient Portal
        public Boolean gblnPatientPortalSendActivationEmail = false;
        public Boolean gblnPatientPortalActivationEmailAlreadySent = false;
        public Boolean gblnPatientPortalEnabled = false;
        frmPatientPortal ofrmPatientPortal = null;
        //Patient Portal
        //API
        public Boolean gblnPatientAPISendActivationEmail = false;
        frmAPIAccessAccount ofrmAPIAccessAccount = null;

        Hashtable hashRefferals;

        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
     //   private bool _isPatientOtherGuarantorModified = false;
     //   private bool _isPatientRepresentativeModified = false;

        private string _MessageBoxCaption = "gloPM";
        private bool _IsInternetFax = false;
        private Int64 _PatientId;
        private gloListControl.gloListControl oListControl;
        private Int64 _Contactid = 0;
        private string _sSPIID = string.Empty;
        String searchstr = ""; //Search String Control

        private bool _isAutogenerate = false;  //Set for the AutoGenerate PaientCode '' if Autogenerate is true then dont validate the patient code.

    //    gloPatientGuarantorControl ogloPatientGuarantorControl = null;
        gloPAGuarantorControl ogloPAGuarantorControl = null;
        gloPatientGuardianControl ogloPatientGuardianControl = null;
        gloPatientInsuranceControl ogloPatientInsuranceControl = null;
        gloPatientOccupationControl ogloPatientOccupationControl = null;
        gloPatientOtherInfoControl ogloPatientOtherInfoCntrl = null;

        // Declare Objects of the Types Demographics,  Guarantor , Guardian, Occupation & Insurance
        // To Access OutSide of this Control
        PatientDemographics oPatientDemo = null;
        PatientGuardian _PatientGuardian = null;
        PatientOccupation _PatientOccupation = null;
        PatientInsuranceOther _PatientInsurance = null;
        PatientWorkersComps _PatientWorkersComp = null;
        //Other Details
        PatientDemographicOtherInfo _PatientDemographicOtherInfo = null;

        //gloAddressControl oAddresscontrol = null;

        gloAddressControl oAddresscontrol = null;
        //
     //   private bool _SaveFormDetails = false;
        private PatientDetails oPatientPharmacies = null;
        private PatientDetails oPatientReferrals = null;
        private PatientDetails oPatientCareTeam = null;
        private PatientDetails oPrimaryCarePhysicians = null;

        private PatientOtherContacts oPatientGuarantors = null;

        private ModifyPatientDetailType _ModificationDetail = ModifyPatientDetailType.None;
     //   gloWebcam.gloWebcam myCam;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public static bool _Ismodify = false;
        public bool _IsSaveAsCopy = false;

        //Added by SaiKrishna:2011-06-27(yyyy-mm-dd) for Patient Account Feature  
        public bool _IsPatientAccountFeature = false;
        //Enable Patient ‘Other Guarantors’ flag in gloPM Admin
        public bool _IsAllowMultipleGuarantors = false;
        public bool IsCopyAccountFeature = false;
        public bool IsCmbSameAsGuardianLoadFlag = true;
        private Account oAccount = null;
        PatientAccount oPatientAccount = null;
        private PatientAccounts oPatientAccounts = null;
        frmAddPatientAccount oFrmAddPatientAccount = null;
        frmCopyPatientAccount oFrmCopyPatientAccount = null;
        gloAccount objgloAccount = null;
        //Added By Mahesh Satlapalli (Apollo) On 2011-06-27(yyyy-mm-dd) - Get the Updated Guarantor Data
        frmEditPatientAccount oFrmEditPatientAccount = null;
        private Int64 _UserID = 0;
        public Int64 _nGuarantorId;
        //Added by SaiKrishna:2011-06-27(yyyy-mm-dd).For account,when saveascopy patient.
        public Int64 _Id;
        PatientOtherContacts oPatientOtherGuarantors = null;
        PatientRepresentatives oPatientRepresentatives = null;
        PatientPortalAccount oPatientPortalAccount = null;
        //API
        PatientRepresentatives oAPIRepresentatives = null;
        PatientPortalAccount oAPIAccount = null;
  
        //It is used for when account feature off
        public Int64 nPAccountId;
        public Int64 nGuarantorId;
        gloPatientGuarantorControl ogloPatientOtherGuarantorControl = null;
        gloPatientRepresentativeControl ogloPatientRepresentativeControl = null;
        public bool _IsPatientDataModified = false;
        public bool _IsPatientCodeModified = false;
        
        private bool isActivatedWebCam = false;// GLO2010-0007047 [BJMC]: Webcam image too small
        // public bool _IsFormClosed = false;

       

        private string _Country = "";
        public bool _IsRequireBusinessCenterOnPAccounts = false;

      //  bool isLanguageValidated = false;
        public DataTable dtPathosp_data = null;
        public DataTable dtReturn; 
        #endregion

        #region "Property Procedures"

        //Property added to check whether any insurance related updates are done or not (ref #GLO2010-0007999)
        public bool IsInsuranceModified
        {
            get { return _isInsurenceModified; }
            set { _isInsurenceModified = value; }
        }

        //Start :: ''YES/No Labs
        public bool GBlnYesNoLab
        {
            get { return gblnYesNoLab; }
            set { gblnYesNoLab = value; }
        }

        //End :: ''YES/No Labs
        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
        //public string Description
        //{
        //    get { return _Description; }
        //}

        public PatientDemographics PatientDemographicsDetails
        {
            get { return oPatientDemo; }
            set { oPatientDemo = value; }
        }

        public PatientGuardian PatientGuardianDetails
        {
            get { return _PatientGuardian; }
            set { _PatientGuardian = value; }
        }

        public PatientOccupation PatientOccupationDetails
        {
            get { return _PatientOccupation; }
            set { _PatientOccupation = value; }
        }

        public PatientInsuranceOther PatientInsuranceDetails
        {
            get { return _PatientInsurance; }
            set { _PatientInsurance = value; }
        }

        //create the collection and defined property for deleted insurances by pankaj on 20110113
        List<Int64> _deletedInsurances = new List<Int64>();
        public List<Int64> DeletedInsurances
        {
            get { return _deletedInsurances; }
            set { _deletedInsurances = value; }
        }

        //Other Details
        public PatientDemographicOtherInfo PatientDemographicOtherInfo
        {
            get { return _PatientDemographicOtherInfo; }
            set { _PatientDemographicOtherInfo = value; }
        }

        public PatientDetails PatientPharmacies
        {
            get
            { return oPatientPharmacies; }
            set
            { oPatientPharmacies = value; }
        }

        public PatientDetails PatientReferrals
        {
            get
            { return oPatientReferrals; }
            set
            { oPatientReferrals = value; }
        }
        public PatientDetails PatientCareTeam
        {
            get
            { return oPatientCareTeam; }
            set
            { oPatientCareTeam = value; }
        }
        public PatientDetails PrimaryCarePhysicians
        {
            get
            { return oPrimaryCarePhysicians; }
            set
            { oPrimaryCarePhysicians = value; }
        }

        public PatientWorkersComps PatientWorkersComps
        {
            get
            { return _PatientWorkersComp; }
            set
            { _PatientWorkersComp = value; }
        }

        public PatientOtherContacts PatientGuarantors
        {
            get
            { return oPatientGuarantors; }
            set
            { oPatientGuarantors = value; }
        }

        public PatientRepresentatives PatientRepresentatives
        {
            get
            { return oPatientRepresentatives; }
            set
            { oPatientRepresentatives = value; }
        }

        public PatientPortalAccount PatientPortalAccount
        {
            get
            { return oPatientPortalAccount; }
            set
            { oPatientPortalAccount = value; }
        }
        //API
        public PatientRepresentatives APIRepresentatives
        {
            get
            { return oAPIRepresentatives; }
            set
            { oAPIRepresentatives = value; }
        }

        public PatientPortalAccount APIAccount
        {
            get
            { return oAPIAccount; }
            set
            { oAPIAccount = value; }
        }
        //API
        public ModifyPatientDetailType ModificationDetail
        {
            get
            { return _ModificationDetail; }
            set
            { _ModificationDetail = value; }
        }
        //Property procedures for Modify
        //public string Description
        //{
        //    get
        //    {
        //        return _Description;
        //    }
        //}

        #region "Get/Set Control Value"
        public Int64 PatientId
        {
            get { return _PatientId; }
            set { _PatientId = value; }
        }
        public String GetFirstName
        {
            get { return txtPAFname.Text; }
            set { txtPAFname.Text = value; }
        }

        public String GetMIName
        {
            get { return txtPAMName.Text; }
            set { txtPAMName.Text = value; }
        }

        public String GetLastName
        {
            get { return txtPALName.Text; }
            set { txtPALName.Text = value; }
        }
        public String GetDOB
        {
            get { return mtxtPADOB.Text; }
            set { mtxtPADOB.Text = value; }
        }
        public String GetGender
        {
            get { return cmbGender.Text; }
            set { cmbGender.Text = value; }
        }
        public String GetAddress1
        {
            get { return oAddresscontrol.txtAddress1.Text; }
            set { oAddresscontrol.txtAddress1.Text = value; }
        }
        public String GetAddress2
        {
            get { return oAddresscontrol.txtAddress2.Text; }
            set { oAddresscontrol.txtAddress2.Text = value; }
        }
        public String GetCity
        {
            get { return oAddresscontrol.txtCity.Text; }
            set { oAddresscontrol.txtCity.Text = value; }
        }
        public String GetState
        {
            get { return oAddresscontrol.cmbState.Text; }
            set { oAddresscontrol.cmbState.Text = value; }
        }
        public String GetCounty
        {
            get { return oAddresscontrol.txtCounty.Text; }
            set { oAddresscontrol.txtCounty.Text = value; }
        }
        public String GetCountry
        {
            get { return oAddresscontrol.cmbCountry.Text; }
            set { oAddresscontrol.cmbCountry.Text = value; }
        }
        public String GetZip
        {
            get
            {
                return oAddresscontrol.txtZip.Text;

            }
            set
            {
                oAddresscontrol.txtZip.Text = value;

            }
        }

        public Boolean flgOCR
        {
            get { return oAddresscontrol.isOCRdata; }
            set { oAddresscontrol.isOCRdata = value; }
        }
        public Image GetPatPhoto
        {
            get { return picPAPhoto.Image; }
            set
            {   
                picPAPhoto.Image = value;
                if (picPAPhoto.Image != null)
                {
                    //myPictureBox.gloWebCameraClipingsGet();
                    //myPictureBox.Image = value;
                    //myPictureBox.gloWebCameraClipingsGet();
                    picPAPhoto.AspectRatio(value, false);
                    myPictureBox.closeCam();
                    myNewTrackBar.SendToBack();
                    //picPAPhoto.gloWebCameraClipingsGet( myPictureBox);
                    //myPictureBox.gloWebCameraClipingsSet();
                    isActivatedWebCam = false;
               //     picWebCamPatient.Enabled = false;
                    picPAPhoto.Visible = true;
                    myNewTrackBar.Visible = true;
                    TrackbarPlus.Visible = true;
                    TrackbarMinus.Visible = true;
                    rbBrowsePhoto.Checked = true;
                    btn_PAClearPhoto.Enabled = true;
                    btn_PAClearPhoto.Text = "Clear Photo"; 

                    semaPhoreTosuppressValueChange = true;
                    myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
                    semaPhoreTosuppressValueChange = false;
                    
                    myPictureBox.SendToBack();
                    picPAPhoto.Refresh();
                }
                else
                {
                    myNewTrackBar.Visible = false;
                    TrackbarPlus.Visible = false;
                    TrackbarMinus.Visible = false;
                    btn_PAClearPhoto.Enabled = false;
                    btn_PAClearPhoto.Text = "Clear Photo"; 
                    semaPhoreTosuppressValueChange = true;
                    myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
                    semaPhoreTosuppressValueChange = false;
                    picPAPhoto.Refresh();
                }
            }
        }

        public Image GetcardFrontImage { get; set; }
        public Image GetcardBackImage { get; set; }

        #endregion

        //Added by SaiKrishna:2011-06-27(yyyy-mm-dd)
        public Account Account
        {
            get { return oAccount; }
            set { oAccount = value; }
        }

        public PatientAccount PatientAccount
        {
            get { return oPatientAccount; }
            set { oPatientAccount = value; }
        }

        public PatientAccounts PatientAccounts
        {
            get { return oPatientAccounts; }
            set { oPatientAccounts = value; }
        }

        public PatientOtherContacts PatientOtherGuarantors
        {
            get { return oPatientOtherGuarantors; }
            set { oPatientOtherGuarantors = value; }
        }

        #endregion

        #region "Constructors"

        public gloPatientDemographicsControl(Int64 PatientId, string databaseConnectionString, bool IsSaveAsCopy)
        {
            InitializeComponent();
            //Added by mitesh
            //if (cmbPAReferrals.Enabled)
            //{
            //    cmbPAReferrals.DrawMode = DrawMode.OwnerDrawFixed;
            //}
            //else
            //{
            //    cmbPAReferrals.DrawMode = DrawMode.Normal;
            //}

            //--------
            picPAPhoto.OnZoomChanged += new gloPictureBox.gloPictureBox.ZoomChanged(TrackBarZoomChange);
            myPictureBox.OnZoomChanged += new gloPictureBox.gloPictureBox.ZoomChanged(TrackBarZoomChange);

            cmbRelationship.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRelationship.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            
            cmbPARace.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPARace.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbPALang.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPALang.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbPAEthn.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPAEthn.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);


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


            //Sandip Darade  20090428
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
            //_Description = Description;
            _databaseconnectionstring = databaseConnectionString;
            _IsSaveAsCopy = IsSaveAsCopy;

            _PatientId = PatientId;

            oPatientDemo = new PatientDemographics();
            oListControl = new gloListControl.gloListControl();
            //Code Review Changes: Twice new opatient demo - see what needs to be commented
            //oPatientDemo = new PatientDemographics();
            _PatientGuardian = new PatientGuardian(_databaseconnectionstring);
            _PatientOccupation = new PatientOccupation();
            _PatientInsurance = new PatientInsuranceOther();

            //Other Details 
            _PatientDemographicOtherInfo = new PatientDemographicOtherInfo();
            _PatientWorkersComp = new PatientWorkersComps();
            //

            oPatientPharmacies = new PatientDetails();
            oPatientReferrals = new PatientDetails();
            oPatientCareTeam = new PatientDetails();
            oPrimaryCarePhysicians = new PatientDetails();

            oPatientGuarantors = new PatientOtherContacts();
            oPatientDemo.PatientID = _PatientId;

            //Sandip Darade  20091006
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
            //Added by SaiKrishna:2011-06-27(yyyy-mm-dd)
            oAccount = new Account();
            oPatientAccount = new PatientAccount();
            oPatientAccounts = new PatientAccounts();
            objgloAccount = new gloAccount(_databaseconnectionstring);
            oPatientOtherGuarantors = new PatientOtherContacts();
            oPatientRepresentatives = new PatientRepresentatives();
            oAPIRepresentatives = new PatientRepresentatives();
            fillCountry();

            #region " Retrieve Country from AppSettings "

            if (appSettings["Country"] != null)
            {
                if (appSettings["Country"] != "")
                {
                    _Country = Convert.ToString(appSettings["Country"]);

                }
                else
                {
                    _Country = "US";
                }
            }
            else
            { _Country = "US"; }

            #endregion

            
        }

        #endregion

        bool _IsReferralClick = false;
        bool _bValidatePortalInvitationEmail = true;
        bool _bValidateAPIInvitationEmail = true;
        #region  CONTROLS Events and Delegates

        public delegate void onDemographicControlEnter(object sender, EventArgs e);
        public event onDemographicControlEnter onDemographicControl_Enter;

        public delegate void onDemographicControlLeave(object sender, EventArgs e);
        public event onDemographicControlLeave onDemographicControl_Leave;
        public void closeWebcam(bool bCaptureAndClose)
        {
            if (myPictureBox.iRunning)
            {
                if (bCaptureAndClose)
                {

                    //Disposing object
                    if (picPAPhoto.Image != null)
                    {

                        try
                        {
                            picPAPhoto.Image.Dispose();
                            picPAPhoto.Image = null;
                        }
                        catch
                        {
                        }

                    }
                    picPAPhoto.Image = myPictureBox.Image;
                    myPictureBox.closeCam();
                    myNewTrackBar.SendToBack();
                    picPAPhoto.gloWebCameraClipingsGet(myPictureBox);
                    myPictureBox.gloWebCameraClipingsSet();
                    //   picWebCamPatient.Enabled = false;
                    picPAPhoto.Visible = true;
                    myNewTrackBar.Visible = true;
                    TrackbarPlus.Visible = true;
                    TrackbarMinus.Visible = true;

                    isActivatedWebCam = false;
                    rbBrowsePhoto.Checked = true;
                    btn_PAClearPhoto.Enabled = true;
                    btn_PAClearPhoto.Text = "Clear Photo";
                    semaPhoreTosuppressValueChange = true;
                    myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
                    semaPhoreTosuppressValueChange = false;
                    //if (myPictureBox != null)
                    //{
                    //    myPictureBox.Dispose();
                    //    myPictureBox = null;
                    //}
                    myPictureBox.SendToBack();
                    picPAPhoto.Refresh();
                    //btn_PACapturePhoto_Click(null, null);
                }
                else
                {
                    myPictureBox.closeCam();
                }
            }
        }

        #region "GLO2010-0007047 [BJMC]: Webcam image too small"
        // GLO2010-0007047 [BJMC]: Webcam image too small
        public void myNewTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (semaPhoreTosuppressValueChange) return;

            if (myNewTrackBar == null) return;
            
                if (isActivatedWebCam)
                {
                    if (myPictureBox == null)
                    {
                        return;
                    }
                    myPictureBox.ZoomValueForTrackBar=myNewTrackBar.Value;
                }
                else
                {
                    picPAPhoto.ZoomValueForTrackBar=myNewTrackBar.Value;
                }
                //Added for Bug #77539: 00000115 : Patient Setup
                _ispicPAPhotomodified = true;
          
        }
        private void TrackbarPlus_Click(object sender, EventArgs e)
        {
            if (myNewTrackBar != null)
            {
                if (myNewTrackBar.Value < 44)
                {
                    myNewTrackBar.Value++;
                }
            }
            //Added for Bug #77539: 00000115 : Patient Setup
            _ispicPAPhotomodified = true;
        }
        private void TrackbarMinus_Click(object sender, EventArgs e)
        {
            if (myNewTrackBar != null)
            {
                if (myNewTrackBar.Value > -44)
                {
                    myNewTrackBar.Value--;
                }
            }
            //Added for Bug #77539: 00000115 : Patient Setup
            _ispicPAPhotomodified = true;
        }
        #endregion "GLO2010-0007047 [BJMC]: Webcam image too small"


        #region Insurence
        //Calling insurance control.
        private void btnInsurInfo_Click(object sender, EventArgs e)
        {
            try
            {
                //Int64 ii=;
                if (ogloPatientInsuranceControl != null)
                {
                    if (this.Controls.Contains(ogloPatientInsuranceControl))
                    {
                        this.Controls.Remove(ogloPatientInsuranceControl);
                        
 
                    }
                    try
                    {
                        ogloPatientInsuranceControl.onInsuranceSave_Clicked -= new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                        ogloPatientInsuranceControl.onInsuranceClose_Clicked -= new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);
 
                    }
                    catch { }

                    ogloPatientInsuranceControl.Dispose();
                    ogloPatientInsuranceControl = null;
                }
                ogloPatientInsuranceControl = new gloPatientInsuranceControl(_databaseconnectionstring, Convert.ToInt64(txtPAProvider.Tag), _PatientId);
                ogloPatientInsuranceControl.InsuranceOtherDetails = this.PatientInsuranceDetails;
                
                ogloPatientInsuranceControl.onInsuranceSave_Clicked += new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                ogloPatientInsuranceControl.onInsuranceClose_Clicked += new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);
                //ogloPatientInsuranceControl.PatientName = txtPAFname.Text.Trim() + " " + txtPALName.Text.Trim();
                ogloPatientInsuranceControl.PatientFName = txtPAFname.Text.Trim();
                ogloPatientInsuranceControl.PatientMName = txtPAMName.Text.Trim();
                ogloPatientInsuranceControl.PatientLName = txtPALName.Text.Trim();
                //Code added on 20081030

                ogloPatientInsuranceControl.IsSaveAsCopy = _IsSaveAsCopy;

                //ogloPatientInsuranceControl.PatientAddressLine1 = txtPAAddress1.Text.Trim();
                //ogloPatientInsuranceControl.PatientAddressLine2 = txtPAAddress2.Text.Trim();
                //ogloPatientInsuranceControl.PatientState = cmbPAState.Text.Trim();
                //ogloPatientInsuranceControl.PatientCity  = txtPACity.Text.Trim();
                //ogloPatientInsuranceControl.PatientCounty = txtPACounty.Text.Trim();
                //ogloPatientInsuranceControl.PatientCountry = cmbPACountry.Text.Trim();
                //ogloPatientInsuranceControl.PatientZip = txtPAZip.Text.Trim();

                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 
                ogloPatientInsuranceControl.PatientAddressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                ogloPatientInsuranceControl.PatientAddressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                ogloPatientInsuranceControl.PatientCity = oAddresscontrol.txtCity.Text.Trim();
                ogloPatientInsuranceControl.PatientCounty = oAddresscontrol.txtCounty.Text.Trim();
                ogloPatientInsuranceControl.PatientZip = oAddresscontrol.txtZip.Text.Trim();
                ogloPatientInsuranceControl.PatientState = oAddresscontrol.cmbState.Text.Trim();
                ogloPatientInsuranceControl.PatientCountry = oAddresscontrol.cmbCountry.Text.Trim();

                if (txtPAProvider.Tag != null && Convert.ToInt64(txtPAProvider.Tag) > 0)
                { ogloPatientInsuranceControl.ProviderID = Convert.ToInt64(txtPAProvider.Tag); }
                ogloPatientInsuranceControl.PatientSSN = txtmPASSN.Text.Trim();

                //

                //if(dtpPADOB.Checked == true)
                //    ogloPatientInsuranceControl.PatientDOB= dtpPADOB.Value.ToShortDateString();

                if (mtxtPADOB.MaskCompleted == true)
                {
                    mtxtPADOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                    ogloPatientInsuranceControl.PatientDOB = mtxtPADOB.Text;
                }


                ogloPatientInsuranceControl.PatientPhone = mtxtPAPhone.Text.Trim();

                //code comented by dipak 20100507 to replace gender radio buttons to combo list
                //if (rbGender1.Checked)
                //{
                //    ogloPatientInsuranceControl.PatientGender = "Male";
                //}
                //if (rbGender2.Checked)
                //{
                //    ogloPatientInsuranceControl.PatientGender = "Female";
                //}
                //if (rbGender3.Checked)
                //{
                //    ogloPatientInsuranceControl.PatientGender = "Other";
                //}
                //Line Added by dipak 
                ogloPatientInsuranceControl.PatientGender = cmbGender.Text.Trim();
                //end code added by dipak


                //oPatientDemo.o 
                ogloPatientInsuranceControl.Employer = cmbPAOccupation.Text;

                this.Controls.Add(ogloPatientInsuranceControl);
                ogloPatientInsuranceControl.Dock = DockStyle.Fill;
                ogloPatientInsuranceControl.BringToFront();
                onDemographicControl_Leave(sender, e);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
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
                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 20)
                            this.toolTip1.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 100, e.Bounds.Bottom + 25);
                        else
                            this.toolTip1.Hide(combo);
                    }
                    else
                    {
                        this.toolTip1.Hide(combo);
                    }
                }
                else
                {
                    toolTip1.Hide(combo);
                }
                e.DrawFocusRectangle();
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {//code review changes 
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g != null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            return width;
        }
        //Save Insurence
        private void ogloPatientInsuranceControl_onInsuranceSave_Clicked(object sender, EventArgs e)
        {
            //Set the propery _isInsurenceModified if any Save is clicked from Insurance Setup screen
            _isInsurenceModified = ogloPatientInsuranceControl.IsInsuranceModified;

            _PatientInsurance = ogloPatientInsuranceControl.InsuranceOtherDetails;

            //For Resolving BUG ID : 8277 i.e 
            // If we delete insurance and add new insurance and print the Patient detail then 
            // Insurance which is deleted and newly added also gets Printed.
            if (_deletedInsurances != null)
            {
                if (_deletedInsurances.Count == 0)
                {
                    _deletedInsurances = ogloPatientInsuranceControl.DeletedInsurances;
                }
                else
                {
                    for (int i = 0; i < ogloPatientInsuranceControl.DeletedInsurances.Count; i++)
                    {
                        _deletedInsurances.Add(ogloPatientInsuranceControl.DeletedInsurances[i]);
                    }
                }
            }
            //cmbGenInfoInsurance.Items.Clear();
            //for (int i = 0; i < _PatientInsurance.InsurancesDetails.Count; i++)
            //{
            //    cmbGenInfoInsurance.Items.Add(_PatientInsurance.InsurancesDetails[i].InsuranceName);
            //    cmbGenInfoInsurance.SelectedIndex = 0;
            //}
            //_isInsurenceModified = true;
            //this.Controls.Remove(ogloPatientInsuranceControl);
           
            cmbGenInfoInsurance.DataSource = null;
            cmbGenInfoInsurance.Items.Clear();
            DataTable dtInsurances = new DataTable();
            dtInsurances.Columns.Add("InsuranceID");
            dtInsurances.Columns.Add("InsuranceName");

            for (int i = 0; i < _PatientInsurance.InsurancesDetails.Count; i++)
            {
                //Do not Show inactive Insurances in Insurance Combobox
                if (_PatientInsurance.InsurancesDetails[i].InsuranceFlag != Convert.ToInt64(Insurance.InsuranceTypeFlag.None))
                {
                    DataRow dr = dtInsurances.NewRow();
                    dr["InsuranceID"] = _PatientInsurance.InsurancesDetails[i].InsuranceID.ToString();
                    dr["InsuranceName"] = _PatientInsurance.InsurancesDetails[i].InsuranceName;
                    dtInsurances.Rows.Add(dr);
                }
            }
            dtInsurances.AcceptChanges();
            cmbGenInfoInsurance.DataSource = dtInsurances;
            cmbGenInfoInsurance.DisplayMember = "InsuranceName";
            cmbGenInfoInsurance.ValueMember = "InsuranceID";
            if (dtInsurances.Rows.Count > 0)
            {
                cmbGenInfoInsurance.SelectedIndex = 0;
            }

            _isInsurenceModified = true;
            this.Controls.Remove(ogloPatientInsuranceControl);
            
            //Disposed object
            try
            {
                if (ogloPatientInsuranceControl != null)
                {

                    ogloPatientInsuranceControl.onInsuranceSave_Clicked -= new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                    ogloPatientInsuranceControl.onInsuranceClose_Clicked -= new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);

                    //Resolved Bug No. 92028::Modify patient>>Application does not remove insurance plan
                    //ogloPatientInsuranceControl.Dispose();
                    //ogloPatientInsuranceControl = null;
                }
            }
            catch
            {
            }
          //  if (ogloPatientInsuranceControl != null) { ogloPatientInsuranceControl.Dispose(); }

            onDemographicControl_Enter(sender, e);

            // added by sandip dhakane 20100715 setting focus on gaurdiancombo when savin info.
            cmbGenInfoInsurance.Focus();

        }

        //Close Insurence control
        private void ogloPatientInsuranceControl_onInsuranceClose_Clicked(object sender, EventArgs e)
        {
            if (ogloPatientInsuranceControl.pnlAddresControl.Controls.Count > 0)
            {
                Control[] myControl = ogloPatientInsuranceControl.pnlAddresControl.Controls.Find("InsuranceAddressControl", true);
                if (myControl.Length > 0)
                {
                    ((gloAddress.gloAddressControl)myControl[0]).ControlClosing = true;
                    ogloPatientInsuranceControl.pnlAddresControl.Controls.Remove(myControl[0]);
                }
            }
            this.Controls.Remove(ogloPatientInsuranceControl);
            //Disposed object
            try
            {
                if (ogloPatientInsuranceControl != null)
                {

                    ogloPatientInsuranceControl.onInsuranceSave_Clicked -= new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                    ogloPatientInsuranceControl.onInsuranceClose_Clicked -= new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);


                    ogloPatientInsuranceControl.Dispose();
                    ogloPatientInsuranceControl = null;
                }
            }
            catch
            {
            }
            // if (ogloPatientInsuranceControl != null) { ogloPatientInsuranceControl.Dispose(); }
            onDemographicControl_Enter(sender, e);

            // added by sandip dhakane 20100715 setting focus on gaurdiancombo when savin info.
            cmbGenInfoInsurance.Focus();


        }

        //Clear Insurence
        private void btnClrInsurance_Click(object sender, EventArgs e)
        
        {
            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
            try
            {
                bool bUsedForBilling = false;

                for (int i = PatientInsuranceDetails.InsurancesDetails.Count - 1; i >= 0; i--)
                {
                    if (PatientInsuranceDetails.InsurancesDetails[i].InsuranceFlag != Convert.ToInt64(Insurance.InsuranceTypeFlag.None))
                    {

                        if (ogloPatient.IsInsuranceUsed(PatientInsuranceDetails.InsurancesDetails[i].InsuranceID) == false)
                        {

                            if (PatientInsuranceDetails.InsurancesDetails[i].InsuranceID > 0)
                            {
                                DeletedInsurances.Add(PatientInsuranceDetails.InsurancesDetails[i].InsuranceID);
                            }
                            PatientInsuranceDetails.InsurancesDetails.RemoveAt(i);

                            //Set the propery _isInsurenceModified if 'Remove Insurances' is clicked.
                            _isInsurenceModified = true;

                        }
                        else
                        {
                            if (_IsSaveAsCopy == false)
                            {
                                bUsedForBilling = true;
                            }
                            else
                            {
                                PatientInsuranceDetails.InsurancesDetails.RemoveAt(i);
                            }
                            //MessageBox.Show("Can not remove this insurance. Insurance is used for billing.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //return;
                        }
                    }
                    else
                    {
                        continue;
                    }

                }


                DataTable dtInsurance = new DataTable();
                dtInsurance.Columns.Add("InsuranceID");
                dtInsurance.Columns.Add("InsuranceName");
                dtInsurance.AcceptChanges();
                for (int i = 0; i < PatientInsuranceDetails.InsurancesDetails.Count; i++)
                {
                    if (PatientInsuranceDetails.InsurancesDetails[i].InsuranceFlag != Convert.ToInt64(Insurance.InsuranceTypeFlag.None))
                    {
                        DataRow dr = dtInsurance.NewRow();
                        dr["InsuranceID"] = PatientInsuranceDetails.InsurancesDetails[i].InsuranceID;
                        dr["InsuranceName"] = PatientInsuranceDetails.InsurancesDetails[i].InsuranceName;
                        dtInsurance.Rows.Add(dr);
                        dtInsurance.AcceptChanges();
                    }
                }


                cmbGenInfoInsurance.DataSource = dtInsurance;
                cmbGenInfoInsurance.DisplayMember = "InsuranceName";
                cmbGenInfoInsurance.ValueMember = "InsuranceID";

                if (cmbGenInfoInsurance.Items.Count > 0)
                {
                    cmbGenInfoInsurance.SelectedIndex = 0;
                }

                if (bUsedForBilling == true)
                {
                    MessageBox.Show("Can not remove this insurance. Insurance is used for billing.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (ogloPatient != null) { ogloPatient.Dispose(); }
            }
        }


        #endregion
        private void removeOListControl()
        {
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_CareTeamSelectedClick);
                    }
                    catch
                    { 
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ExistingAccountSelectClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_GaurantorSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_OtherGaurantorSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_PharmaSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ProviderSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_PhysiciansSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_refferalsSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_RaceSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch
                    {
                    }
                }
                catch
                {

                }
                oListControl.Dispose();
                oListControl = null;
            }
        }
        #region Gaurantor
        //Open patient list to select Gaurantor  
        private void btnGuarantorSelect_Click(object sender, EventArgs e)
        {
            try
            {

                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);
                oListControl.ControlHeader = "Guarantor";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_GaurantorSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                //
                //Sandip Darade  20090907 
                //To allow the user to add multiple guarantors at one time 
                //for (int i = 0; i < cmbGuarantor.Items.Count; i++)
                //{
                //    cmbGuarantor.SelectedIndex = i;
                //    oListControl.SelectedItems.Add(Convert.ToInt64(cmbGuarantor.SelectedValue), cmbGuarantor.Text);
                //    //cmbPAReferrals.SelectedIndex = -1;  
                //}
                //
                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        //select existing patient as guarantor
        private void oListControl_GaurantorSelectedClick(object sender, EventArgs e)
        {
            try
            {
                Int64 _TempPatientID = 0;
         

                if (oListControl.SelectedItems.Count > 1)
                {
                    MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (oListControl.SelectedItems.Count > 0)
                {
                    //New mode
                    if (PatientId == 0)
                    {
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            oPatientGuarantors.RemoveAt(0);
                            txtAccGuarantor.Text = "";
                        }
                    }

                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        _TempPatientID = Convert.ToInt64(oListControl.SelectedItems[i].ID);
                        bool _ShouldAdd = true;
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (int _Count = 0; _Count < oPatientGuarantors.Count; _Count++)
                            {
                                if (Convert.ToInt64(oListControl.SelectedItems[i].ID) == oPatientGuarantors[_Count].GuarantorAsPatientID)
                                {
                                    _ShouldAdd = false;
                                    break;
                                }
                            }
                        }
                        if (_ShouldAdd == true)
                        {
                            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                            Patient oPatientTemp = ogloPatient.GetPatientDemo(_TempPatientID);

                            if (oPatientTemp != null)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();

                                oGuarantor.GuarantorAsPatientID = _TempPatientID;
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = oPatientTemp.DemographicsDetail.PatientFirstName;
                                oGuarantor.MiddleName = oPatientTemp.DemographicsDetail.PatientMiddleName;
                                oGuarantor.LastName = oPatientTemp.DemographicsDetail.PatientLastName;
                                oGuarantor.DOB = oPatientTemp.DemographicsDetail.PatientDOB;
                                oGuarantor.SSN = oPatientTemp.DemographicsDetail.PatientSSN;
                                oGuarantor.Gender = oPatientTemp.DemographicsDetail.PatientGender;
                                oGuarantor.Relation = "";
                                oGuarantor.AddressLine1 = oPatientTemp.DemographicsDetail.PatientAddress1;
                                oGuarantor.AddressLine2 = oPatientTemp.DemographicsDetail.PatientAddress2;
                                oGuarantor.City = oPatientTemp.DemographicsDetail.PatientCity;
                                oGuarantor.State = oPatientTemp.DemographicsDetail.PatientState;
                                oGuarantor.Zip = oPatientTemp.DemographicsDetail.PatientZip;
                                oGuarantor.Country = oPatientTemp.DemographicsDetail.PatientCountry;
                                oGuarantor.County = oPatientTemp.DemographicsDetail.PatientCounty;
                                oGuarantor.Phone = oPatientTemp.DemographicsDetail.PatientPhone;
                                oGuarantor.Mobile = oPatientTemp.DemographicsDetail.PatientMobile;
                                oGuarantor.Email = oPatientTemp.DemographicsDetail.PatientEmail;
                                oGuarantor.Fax = oPatientTemp.DemographicsDetail.PatientFax;
                                oGuarantor.OtherConatctType = PatientOtherContactType.Patient;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(false);
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;

                                if (oPatientGuarantors.Count == 0)
                                {
                                    this.oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        oPatientTemp.Dispose();
                                        oPatientTemp = null;

                                        ogloPatient.Dispose();
                                        ogloPatient = null;
                                        return;
                                    }
                                }
                                oPatientTemp.Dispose();
                                oPatientTemp = null;
                            }
                            ogloPatient.Dispose();
                            ogloPatient = null;
                        }

                    }


                }
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;

                        setCmbSameAsGuardianIndex();
                    }
                }

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                _isGaurentorModified = true;
                onDemographicControl_Enter(sender, e);
            }
        }

        //Close Guarantor Control
        private void ogloPatientGuarantorControl_CloseButton_Click(object sender, EventArgs e)
        {
            //this.Controls.Remove(ogloPatientGuarantorControl);
            //this.Controls.Remove(ogloPAGuarantorControl);
            RemoveGuarantorControl();
            onDemographicControl_Enter(sender, e);
            pnlDemographicInfo.Visible = true;
            txtAccGuarantor.Focus();
        }
        private void RemoveGuarantorControl()
        {
            try
            {
                if (ogloPAGuarantorControl.pnlAddresssControl.Controls.Count > 0)
                {
                    Control[] myControl = ogloPAGuarantorControl.pnlAddresssControl.Controls.Find("GuarantorAddressControl", true);
                    if (myControl.Length > 0)
                    {
                        ((gloAddress.gloAddressControl)myControl[0]).ControlClosing = true;
                        ogloPAGuarantorControl.pnlAddresssControl.Controls.Remove(myControl[0]);
                    }
                }
                this.Controls.Remove(ogloPAGuarantorControl);
                try
                {
                    ogloPAGuarantorControl.SaveButton_Click -= new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                    ogloPAGuarantorControl.CloseButton_Click -= new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                }
                catch
                {
                }
   
            }
            catch //(Exception ex)
            {

            }
        }
        //Create New Guarantor
        private void btnNewGuarantor_Click(object sender, EventArgs e)
        {
            try
            {

                //ogloPatientGuarantorControl = new gloPatientGuarantorControl(_databaseconnectionstring);
                ////Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                //ogloPatientGuarantorControl.PatientGuarantors = oPatientGuarantors;
                //ogloPatientGuarantorControl.FromAccountGuarantor = true;
                //ogloPatientGuarantorControl.PatientId = PatientId;
                //ogloPatientGuarantorControl.SaveButton_Click += new gloPatientGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                //ogloPatientGuarantorControl.CloseButton_Click += new gloPatientGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                //this.Controls.Add(ogloPatientGuarantorControl);
                //ogloPatientGuarantorControl.Dock = DockStyle.Fill;
                ////ogloPatientGuarantorControl.BringToFront();
                //onDemographicControl_Leave(sender, e);
                //pnlDemographicInfo.Visible = false;
                try
                {
                    if (_PatientId == 0)
                    {
                        GetSameAsPatientGuarantor();
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                RemoveGuarantorControl();
                if (ogloPAGuarantorControl != null)
                {
                    ogloPAGuarantorControl.Dispose();
                    ogloPAGuarantorControl = null;
                }
                ogloPAGuarantorControl = new gloPAGuarantorControl(_databaseconnectionstring);
                ogloPAGuarantorControl.PatientGuarantors = oPatientGuarantors;
                ogloPAGuarantorControl.FromAccountGuarantor = true;
                ogloPAGuarantorControl.PatientId = PatientId;
                ogloPAGuarantorControl.SaveButton_Click += new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                ogloPAGuarantorControl.CloseButton_Click += new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                this.Controls.Add(ogloPAGuarantorControl);
                ogloPAGuarantorControl.Dock = DockStyle.Fill;
                onDemographicControl_Leave(sender, e);
                pnlDemographicInfo.Visible = false;



            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        //Save New Guarantor
        private void ogloPatientGuarantorControl_SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                //oPatientGuarantors = ogloPatientGuarantorControl.PatientGuarantors;
                ////Added by Sai Krishna for PAF 2011-05-09(yyyy-mm-dd)
                //if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                //{
                //    for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                //    {
                //        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                //        setCmbSameAsGuardianIndex();
                //    }
                //}
                //else
                //{
                //    txtAccGuarantor.Text = "";
                //    IsCmbSameAsGuardianLoadFlag = false;
                //    cmbSameAsGuardian.SelectedIndex = -1;
                //    IsCmbSameAsGuardianLoadFlag = true;
                //}
                //_isGaurentorModified = true;
                //this.Controls.Remove(ogloPatientGuarantorControl);

                oPatientGuarantors = ogloPAGuarantorControl.PatientGuarantors;
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;

                        //..Start - Code added on 2011 5 Nov by Sagar Ghodke
                        //..Code added to set the "Same As Patient" drop to blank if patient LastName/FirstName/SSN is modified 
                        //..assuming that the guarantor is not patient.
                        if (
                            (oPatientGuarantors[gindex].FirstName.Trim() != txtPAFname.Text.Trim() || oPatientGuarantors[gindex].LastName.Trim() != txtPALName.Text.Trim())
                            ||
                            (oPatientGuarantors[gindex].SSN.Trim() != txtmPASSN.Text.Trim())
                            )
                        {
                            //&& oPatientGuarantors[gindex].OtherConatctType == PatientOtherContactType.SameAsPatient
                            if (_PatientId == 0)
                            {
                                oPatientGuarantors[gindex].OtherConatctType = PatientOtherContactType.Guarantor;
                            }
                        }
                        //..End - Code added on 2011 5 Nov by Sagar Ghodke


                        setCmbSameAsGuardianIndex();
                    }
                }
                else
                {
                    txtAccGuarantor.Text = "";
                    IsCmbSameAsGuardianLoadFlag = false;
                    cmbSameAsGuardian.SelectedIndex = -1;
                    IsCmbSameAsGuardianLoadFlag = true;
                }
                _isGaurentorModified = true;
                //this.Controls.Remove(ogloPAGuarantorControl);
                ////Disposed object
                //if (ogloPatientGuardianControl != null) { ogloPatientGuardianControl.Dispose(); }
                RemoveGuarantorControl();
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                pnlDemographicInfo.Visible = true;
                onDemographicControl_Enter(sender, e);
                txtAccGuarantor.Focus();
            }
        }
        #endregion

        #region Guardian

        //Call Guardian Control
        private void btnGuardianSelect_Click(object sender, EventArgs e)
        {
            try
            {
                removeogloPatientGaurdianControl();
                ogloPatientGuardianControl = new gloPatientGuardianControl(_databaseconnectionstring);
                ogloPatientGuardianControl.GuardianDetail = this.PatientGuardianDetails;
                ogloPatientGuardianControl.SaveButton_Click += new gloPatientGuardianControl.SaveButtonClick(ogloPatientGuardianControl_SaveButton_Click);
                ogloPatientGuardianControl.CloseButton_Click += new gloPatientGuardianControl.CloseButtonClick(ogloPatientGuardianControl_CloseButton_Click);

                //ogloPatientGuardianControl.Address.PatientAddress1 = txtPAAddress1.Text.Trim();
                //ogloPatientGuardianControl.Address.PatientAddress2 = txtPAAddress2.Text.Trim();
                //ogloPatientGuardianControl.Address.PatientCity = txtPACity.Text.Trim();
                //ogloPatientGuardianControl.Address.PatientCounty = txtPACounty.Text.Trim();
                //ogloPatientGuardianControl.Address.PatientZip = txtPAZip.Text.Trim();
                //ogloPatientGuardianControl.Address.PatientState = cmbPAState.Text.Trim();
                //ogloPatientGuardianControl.Address.PatientCountry = cmbPACountry.Text.Trim();   

                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 
                ogloPatientGuardianControl.Address.PatientAddress1 = oAddresscontrol.txtAddress1.Text.Trim();
                ogloPatientGuardianControl.Address.PatientAddress2 = oAddresscontrol.txtAddress2.Text.Trim();
                ogloPatientGuardianControl.Address.PatientCity = oAddresscontrol.txtCity.Text.Trim();
                ogloPatientGuardianControl.Address.PatientCounty = oAddresscontrol.txtCounty.Text.Trim();
                ogloPatientGuardianControl.Address.PatientZip = oAddresscontrol.txtZip.Text.Trim();
                ogloPatientGuardianControl.Address.PatientState = oAddresscontrol.cmbState.Text.Trim();
                ogloPatientGuardianControl.Address.PatientCountry = oAddresscontrol.cmbCountry.Text.Trim();

                this.Controls.Add(ogloPatientGuardianControl);
                ogloPatientGuardianControl.Dock = DockStyle.Fill;
                ogloPatientGuardianControl.BringToFront();
                onDemographicControl_Leave(sender, e);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        //Save Guardian 
        private void ogloPatientGuardianControl_SaveButton_Click(object sender, EventArgs e)
        {
            _PatientGuardian = ogloPatientGuardianControl.GuardianDetail;
            cmbGaurdian.Items.Clear();

            //Added by SaiKrishna:2011-06-27(yyyy-mm-dd) 
            //clear all items in sameasguardian combobox
            cmbSameAsGuardian.Items.Clear();
            cmbSameAsGuardian.Items.Add("Patient");
            if (_PatientGuardian.PatientMotherFirstName != "" || _PatientGuardian.PatientMotherLastName != "")
            {
                cmbGaurdian.Items.Add(_PatientGuardian.PatientMotherFirstName + " " + _PatientGuardian.PatientMotherLastName);
                cmbGaurdian.SelectedIndex = 0;
                //Added by SaiKrishna:2011-06-27(yyyy-mm-dd) 
                //when patient having mother information then add in sameasguardian combobox
                cmbSameAsGuardian.Items.Add("Mother");
            }
            if (_PatientGuardian.PatientFatherFirstName != "" || _PatientGuardian.PatientFatherLastName != "")
            {
                cmbGaurdian.Items.Add(_PatientGuardian.PatientFatherFirstName + " " + _PatientGuardian.PatientFatherLastName);
                cmbGaurdian.SelectedIndex = 0;
                //Added by SaiKrishna:2011-06-27(yyyy-mm-dd)
                //when patient having father information then add in sameasguardian combobox
                cmbSameAsGuardian.Items.Add("Father");
            }
            if (_PatientGuardian.PatientGuardianFirstName != "" || _PatientGuardian.PatientGuardianLastName != "")
            {
                cmbGaurdian.Items.Add(_PatientGuardian.PatientGuardianFirstName + " " + _PatientGuardian.PatientGuardianLastName);
                cmbGaurdian.SelectedIndex = 0;
                //Added by SaiKrishna:2011-06-27(yyyy-mm-dd)
                //when patient having otherguardian information then add in sameasguardian combobox
                cmbSameAsGuardian.Items.Add("Other Guardian");
            }

            _isGaurdianModified = true;
            removeogloPatientGaurdianControl();
            onDemographicControl_Enter(sender, e);

            // added by sandip dhakane 20100715 setting focus on gaurdiancombo when savin info.
            cmbGaurdian.Focus();
            //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
            IsCmbSameAsGuardianLoadFlag = false;
            setCmbSameAsGuardianIndex();
            IsCmbSameAsGuardianLoadFlag = true;

        }

        private void removeogloPatientGaurdianControl()
        {
            if (ogloPatientGuardianControl != null)
            {
                if (this.Controls.Contains(ogloPatientGuardianControl))
                {
                    this.Controls.Remove(ogloPatientGuardianControl);
                }
                //Disposed object
                try
                {
                    ogloPatientGuardianControl.SaveButton_Click -= new gloPatientGuardianControl.SaveButtonClick(ogloPatientGuardianControl_SaveButton_Click);
                    ogloPatientGuardianControl.CloseButton_Click -= new gloPatientGuardianControl.CloseButtonClick(ogloPatientGuardianControl_CloseButton_Click);
                }
                catch
                {
                }
                ogloPatientGuardianControl.Dispose();
            }
        }

        //Close Guardian control
        private void ogloPatientGuardianControl_CloseButton_Click(object sender, EventArgs e)
        {
            removeogloPatientGaurdianControl();
            //this.Controls.Remove(ogloPatientGuardianControl);
            ////Disposed object
            //ogloPatientGuardianControl.SaveButton_Click -= new gloPatientGuardianControl.SaveButtonClick(ogloPatientGuardianControl_SaveButton_Click);
            //ogloPatientGuardianControl.CloseButton_Click -= new gloPatientGuardianControl.CloseButtonClick(ogloPatientGuardianControl_CloseButton_Click);
            //if (ogloPatientGuardianControl != null) { ogloPatientGuardianControl.Dispose(); }
            onDemographicControl_Enter(sender, e);

            // added by sandip dhakane 20100715 setting focus on gaurdiancombo when savin info.
            cmbGaurdian.Focus();

        }

        //Clear Guardian
        private void btnGuardianDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //guardian
                PatientGuardianDetails.PatientGuardianAddress1 = "";
                PatientGuardianDetails.PatientGuardianAddress2 = "";
                PatientGuardianDetails.PatientGuardianCity = "";
                PatientGuardianDetails.PatientGuardianCountry = "";
                PatientGuardianDetails.PatientGuardianEmail = "";
                PatientGuardianDetails.PatientGuardianFAX = "";
                PatientGuardianDetails.PatientGuardianFirstName = "";
                PatientGuardianDetails.PatientGuardianLastName = "";
                PatientGuardianDetails.PatientGuardianMiddleName = "";
                PatientGuardianDetails.PatientGuardianMobile = "";
                PatientGuardianDetails.PatientGuardianPhone = "";
                PatientGuardianDetails.PatientGuardianState = "";
                PatientGuardianDetails.PatientGuardianZip = "";

                //mother
                _PatientGuardian.PatientMotherFirstName = "";
                _PatientGuardian.PatientMotherAddress1 = "";
                _PatientGuardian.PatientMotherAddress2 = "";
                _PatientGuardian.PatientMotherCity = "";
                _PatientGuardian.PatientMotherCountry = "";
                _PatientGuardian.PatientMotherCounty = "";
                _PatientGuardian.PatientMotherEmail = "";
                _PatientGuardian.PatientMotherFAX = "";
                _PatientGuardian.PatientMotherFirstName = "";
                _PatientGuardian.PatientMotherLastName = "";
                _PatientGuardian.PatientMotherMiddleName = "";
                _PatientGuardian.PatientMotherMaidenFirstName = "";
                _PatientGuardian.PatientMotherMaidenMiddleName = "";
                _PatientGuardian.PatientMotherMaidenLastName = "";
                _PatientGuardian.PatientMotherMobile = "";
                _PatientGuardian.PatientMotherPhone = "";
                _PatientGuardian.PatientMotherState = "";
                _PatientGuardian.PatientMotherZip = "";

                //father
                _PatientGuardian.PatientFatherFirstName = "";
                _PatientGuardian.PatientFatherAddress1 = "";
                _PatientGuardian.PatientFatherAddress2 = "";
                _PatientGuardian.PatientFatherCity = "";
                _PatientGuardian.PatientFatherCountry = "";
                _PatientGuardian.PatientFatherCounty = "";
                _PatientGuardian.PatientFatherEmail = "";
                _PatientGuardian.PatientFatherFAX = "";
                _PatientGuardian.PatientFatherFirstName = "";
                _PatientGuardian.PatientFatherLastName = "";
                _PatientGuardian.PatientFatherMiddleName = "";
                _PatientGuardian.PatientFatherMobile = "";
                _PatientGuardian.PatientFatherPhone = "";
                _PatientGuardian.PatientFatherState = "";
                _PatientGuardian.PatientFatherZip = "";




                if (cmbGaurdian.Items.Count > 0)
                {
                    cmbGaurdian.Items.Clear();
                    _isGaurdianModified = true;
                }

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }
        #endregion

        #region Occupation

        //Call Occupation control
        private void btnOccupationSelect_Click(object sender, EventArgs e)
        {
            try
            {
                removeogloPatientOccupationControl();
                ogloPatientOccupationControl = new gloPatientOccupationControl(_databaseconnectionstring);
                ogloPatientOccupationControl.OcupationDetails = this.PatientOccupationDetails;
                ogloPatientOccupationControl.onOccupationSave_Clicked += new gloPatientOccupationControl.onOccupationSaveClicked(ogloPatientOccupationControl_onOccupationSave_Clicked);
                ogloPatientOccupationControl.onOccupationClose_Clicked += new gloPatientOccupationControl.onOccupationCloseClicked(ogloPatientOccupationControl_onOccupationClose_Clicked);
                this.Controls.Add(ogloPatientOccupationControl);
                ogloPatientOccupationControl.Dock = DockStyle.Fill;
                ogloPatientOccupationControl.BringToFront();
                onDemographicControl_Leave(sender, e);

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }


        //Save Occupation
        private void ogloPatientOccupationControl_onOccupationSave_Clicked(object sender, EventArgs e)
        {
            _PatientOccupation = ogloPatientOccupationControl.OcupationDetails;
            cmbPAOccupation.Items.Clear();
            if (Convert.ToString(_PatientOccupation.EmployerName) != "")
            {
                cmbPAOccupation.Items.Add(Convert.ToString(_PatientOccupation.EmployerName));
            }
            else if (Convert.ToString(_PatientOccupation.Occupation.ToString()) != "")
            {
                cmbPAOccupation.Items.Add(_PatientOccupation.Occupation.ToString());
            }

            if (Convert.ToString(_PatientOccupation.EmployerName) != "" && Convert.ToString(_PatientOccupation.Occupation.ToString()) != "")
            {
                cmbPAOccupation.Items.Clear();
                cmbPAOccupation.Items.Add(Convert.ToString(_PatientOccupation.EmployerName + "-" + _PatientOccupation.Occupation.ToString()));
            }
            if (cmbPAOccupation.Items.Count > 0)
            {
                cmbPAOccupation.SelectedIndex = 0;
            }
            //20100629 Shubhangi //20100629 Shubhangi THIS IS FOR THE SCENARIO WHEN WE CHANGE OCCUPATION & TRY TO SAVE THAT UODATED OCCUPATION
            for (int _Counter = 0; _Counter < PatientInsuranceDetails.InsurancesDetails.Count; _Counter++)
            {
                if (Convert.ToString(PatientInsuranceDetails.InsurancesDetails[_Counter].RelationshipName.ToUpper().Trim()) == "SELF")
                {
                    if (Convert.ToString(cmbPAOccupation.Text) != "")
                    {
                        PatientInsuranceDetails.InsurancesDetails[_Counter].Employer = cmbPAOccupation.Text;
                    }
                }
            }
            _isOccupationModified = true;
            removeogloPatientOccupationControl();

            onDemographicControl_Enter(sender, e);

            // added by sandip dhakane 20100715 setting focus on combobox of Occupation when saving occupation info.
            cmbPAOccupation.Focus();
        }

        private void removeogloPatientOccupationControl()
        {
            if (ogloPatientOccupationControl != null)
            {
                if (this.Controls.Contains(ogloPatientOccupationControl))
                {
                    this.Controls.Remove(ogloPatientOccupationControl);
                }
                try
                {
                    ogloPatientOccupationControl.onOccupationSave_Clicked -= new gloPatientOccupationControl.onOccupationSaveClicked(ogloPatientOccupationControl_onOccupationSave_Clicked);
                    ogloPatientOccupationControl.onOccupationClose_Clicked -= new gloPatientOccupationControl.onOccupationCloseClicked(ogloPatientOccupationControl_onOccupationClose_Clicked);

                }
                catch
                {
                }
                //Disposed object
                ogloPatientOccupationControl.Dispose();
            }
        }

        //Close Occupation Control
        private void ogloPatientOccupationControl_onOccupationClose_Clicked(object sender, EventArgs e)
        {
            //this.Controls.Remove(ogloPatientOccupationControl);
            ////Disposed object
            //if (ogloPatientOccupationControl != null) { ogloPatientOccupationControl.Dispose(); }
            removeogloPatientOccupationControl();
            onDemographicControl_Enter(sender, e);

            // added by sandip dhakane 20100715 setting focus on combobox of Occupation when saving occupation info.
            cmbPAOccupation.Focus();

        }

        //Cleare Occupation
        private void btnOccupationDelete_Click(object sender, EventArgs e)
        {
            try
            {
                PatientOccupationDetails.Occupation = "";
                PatientOccupationDetails.EmployerName = "";
                //20100629 Shubhangi THIS IS FOR THE SCENARIO WHEN WE CHANGE OCCUPATION & TRY TO SAVE THAT UODATED OCCUPATION
                for (int i = 0; i < PatientInsuranceDetails.InsurancesDetails.Count; i++)
                {
                    if (Convert.ToString(PatientInsuranceDetails.InsurancesDetails[i].RelationshipName.ToUpper().Trim()) == "SELF")
                    {
                        if (Convert.ToString(cmbPAOccupation.Text) != "")
                        {
                            PatientInsuranceDetails.InsurancesDetails[i].Employer = "";
                        }
                    }

                }

                PatientOccupationDetails.PatientEmploymentStatus = "";
                PatientOccupationDetails.PatientPlaceofEmployment = "";
                PatientOccupationDetails.PatientWorkAddress1 = "";
                PatientOccupationDetails.PatientWorkAddress2 = "";
                PatientOccupationDetails.PatientWorkCity = "";
                PatientOccupationDetails.PatientWorkCountry = "";
                PatientOccupationDetails.PatientWorkEmail = "";
                PatientOccupationDetails.PatientWorkFax = "";
                PatientOccupationDetails.PatientWorkMobile = "";
                PatientOccupationDetails.PatientWorkPhone = "";
                PatientOccupationDetails.PatientWorkState = "";
                PatientOccupationDetails.PatientWorkZip = "";
                PatientOccupationDetails.PatientWorkCounty = "";




                if (cmbPAOccupation.Items.Count > 0)
                {
                    cmbPAOccupation.Items.Clear();
                    _isOccupationModified = true;
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }
        #endregion

        #region Photo
        // Browse Photo
        //private void btn_PAPhotoBrowse_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rbBrowsePhoto.Checked == true)
        //        {
        //            //picPAPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
        //            dlgPhotoBrowser.Title = " Browse photo ";
        //            dlgPhotoBrowser.Filter = "Images Files(*.bmp,*.tif,*.jpg,*.jpeg,*.gif)|*.bmp;*.tif;*.jpg;*.jpeg;*.gif";
        //            dlgPhotoBrowser.FileName = "";
        //            dlgPhotoBrowser.CheckFileExists = true;
        //            dlgPhotoBrowser.Multiselect = false;
        //            dlgPhotoBrowser.ShowHelp = false;
        //            dlgPhotoBrowser.ShowReadOnly = false;

        //            if (dlgPhotoBrowser.ShowDialog() == DialogResult.OK)
        //            {
        //                picPAPhoto.Visible = true;
        //                //picPAPhoto.Image = Image.FromFile(dlgPhotoBrowser.FileName);

        //                Image img = Image.FromFile(dlgPhotoBrowser.FileName);

        //                //int nHight;
        //                //int nWidth;

        //                //img = picPAPhoto.Image;
        //                //nHight = img.Height;
        //                //nWidth = img.Width;
        //                //if (nWidth > 140)
        //                //    nWidth = 140;
        //                //if (nHight > 150)
        //                //    nHight = 150;


        //                picPAPhoto.BackgroundImageLayout = ImageLayout.Stretch;
        //                //picPAPhoto.SizeMode = PictureBoxSizeMode.CenterImage;
        //                int picOutputwidth = picPAPhoto.Width;
        //                int picOutputheight = picPAPhoto.Height;
        //                int OutputWidth = img.Width;
        //                int OutputHeight = img.Height;

        //                double myPicWidth = picOutputwidth;
        //                double myPicHeight = picOutputheight;

        //                double myScaleX = myPicWidth / (double)OutputWidth;
        //                double myScaleY = myPicHeight / (double)OutputHeight;
        //                double myStartX = 0;
        //                double myStartY = 0;

        //                if (myScaleX < myScaleY)
        //                {
        //                    myPicWidth = (double)OutputWidth * myScaleY;
        //                    myStartX = ((double)picOutputwidth - myPicWidth) / 2;
        //                }
        //                else
        //                {
        //                    myPicHeight = (double)OutputHeight * myScaleX;
        //                    myStartY = ((double)picOutputheight - myPicHeight) / 2;
        //                }

        //                Image myimg = new Bitmap(img, new Size((int)myPicWidth,(int) myPicHeight));

        //                picPAPhoto.Image = myimg ;
        //              //picPAPhoto.SetCenter();
        //                picPAPhoto.Visible = true;


        //                //gloPictureBox.gloPictureBox gloPic = new gloPictureBox.gloPictureBox();
        //                //pnlgloPic.Controls.Add(gloPic);
        //                //pnlPAPhoto.Controls.Add(gloPic);
        //                //gloPic.Location = picPAPhoto.Location;
        //                //gloPic.Size = pnlgloPic.Size;

        //                //gloPic.Image = img;
        //                //gloPic.Visible = true;
        //                //pnlgloPic.Enabled = true;
        //                //pnlgloPic.Visible = true;
        //                //pnlgloPic.BringToFront(); 
        //                //gloPic.BringToFront();
        //                //gloPic.Show();


        //                // picPAPhoto.SizeMode = PictureBoxSizeMode.CenterImage;
        //            }
        //        }
        //        else // Capture through webcam
        //        {
        //            picPAPhoto.Visible = false;
        //            picWebCamPatient.Visible = true;
        //            myCam = new gloWebcam();
        //            IntPtr picHndl = picWebCamPatient.Handle;
        //            //myCam.initCam(picWebCamPatient.Handle.ToInt32());
        //            myCam.initCam(picWebCamPatient.Handle.ToInt32(), picWebCamPatient.Height, picWebCamPatient.Width);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        ex = null;
        //    }
        //}
        // GLO2010-0007047 [BJMC]: Webcam image too small
        private void btn_PAPhotoBrowse_Click(object sender, EventArgs e)
        {            
            try
            {

                if (rbBrowsePhoto.Checked == true)
                {
                    //picPAPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    dlgPhotoBrowser.Title = " Browse photo ";
                    dlgPhotoBrowser.Filter = "Images Files(*.bmp,*.tif,*.jpg,*.jpeg,*.gif)|*.bmp;*.tif;*.jpg;*.jpeg;*.gif";
                    dlgPhotoBrowser.FileName = "";
                    dlgPhotoBrowser.CheckFileExists = true;
                    dlgPhotoBrowser.Multiselect = false;
                    dlgPhotoBrowser.ShowHelp = false;
                    dlgPhotoBrowser.ShowReadOnly = false;
                    btn_PAClearPhoto.Enabled = true;
                    btn_PAClearPhoto.Text = "Clear Photo"; 
                    if (dlgPhotoBrowser.ShowDialog(this) == DialogResult.OK)
                    {
                        picPAPhoto.Visible = true;
                        try
                        {
                            Image myImage = Image.FromFile(dlgPhotoBrowser.FileName);
                            Image cloneImage = (Image) myImage.Clone();
                            //Problem No: 00000361
                            picPAPhoto.AspectRatio( cloneImage, false);
                            myImage.Dispose();
                            myImage = null;
                            cloneImage.Dispose();
                            cloneImage = null;
                        }
                        catch
                        {
                        }
                        //Dispose myImage object
                        //myImage.Dispose();
                        //10679/Start/ Patient Photo>> Patient photo zoom in - zoom out  is not working when we go for new patient , start web cam,  cancel it  & then go for  browse photo
                        //myNewTrackBar.Visible = true;
                        //TrackbarPlus.Visible = true;
                        //TrackbarMinus.Visible = true;
                        //picPAPhoto.GetZoomFromTrackbar(ref myNewTrackBar);
                        //picPAPhoto.BringToFront();
                        if (picPAPhoto.Image != null)
                        {
                            myNewTrackBar.SendToBack();
                            //GLO2011-0015477 : Patient pictures zoomed in since update...Start
                           // picPAPhoto.gloWebCameraClipingsGet( picPAPhoto.Zoom , picPAPhoto.Rotation, picPAPhoto.PLocation, new Point(0, 0));
                            //picPAPhoto.gloWebCameraClipingsGet((int)((double)myPictureBox.Zoom), myPictureBox.PLocation, myPictureBox.ELocation);
                            //myPictureBox.gloWebCameraClipingsSet();
                            isActivatedWebCam = false;
                       //     picWebCamPatient.Enabled = false;
                            picPAPhoto.Visible = true;
                            myNewTrackBar.Visible = true;
                            TrackbarPlus.Visible = true;
                            TrackbarMinus.Visible = true;
                            btn_PAClearPhoto.Enabled = true;
                            btn_PAClearPhoto.Text = "Clear Photo"; 
                            semaPhoreTosuppressValueChange=true;
                            myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
                            semaPhoreTosuppressValueChange = false;
                          
                           
                        }
                        //10679/End/ Patient Photo>> Patient photo zoom in - zoom out  is not working when we go for new patient , start web cam,  cancel it  & then go for  browse photo
                    }
                    myPictureBox.SendToBack();
                    picPAPhoto.Refresh();
                }
                else // Capture through webcam
                {

                    btn_PAClearPhoto.Enabled = true;
                    btn_PAClearPhoto.Text = "Select Camera"; 

                    if (myPictureBox == null)
                    {
                        myPictureBox = new gloPictureBox.gloCameraBox();
                        this.myPictureBox.AutoScroll = true;
                        this.myPictureBox.AutoScrollMinSize = new System.Drawing.Size(1, 1);
                        this.myPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        this.myPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
                        this.myPictureBox.Location = new System.Drawing.Point(125, 8);
                        this.myPictureBox.Name = "myPictureBox";
                        this.myPictureBox.Size = new System.Drawing.Size(123, 137);
                        this.myPictureBox.TabIndex = 0;
                        this.myPictureBox.TabStop = false;
                        this.myPictureBox.Zoom = 10;
                        this.pnlPAPhoto.Controls.Add(myPictureBox);
                        this.pnlPAPhoto.Size = new System.Drawing.Size(270, 152);
                        this.pnlPAPhoto.TabIndex = 15;
                    }




                    picPAPhoto.Visible = false;
               //     picWebCamPatient.Visible = true;
                    myPictureBox.BringToFront();
                 //   IntPtr picHndl = picWebCamPatient.Handle;
                    myPictureBox.gloWebCameraClipingsGet();
                    isActivatedWebCam = myPictureBox.initCam(); //Start/OldCode
                    if (isActivatedWebCam)
                    {
                        myNewTrackBar.Visible = true;
                        TrackbarPlus.Visible = true;
                        TrackbarMinus.Visible = true;
                        
                        semaPhoreTosuppressValueChange = true;
                        myNewTrackBar.Value = myPictureBox.ZoomValueForTrackBar;
                        semaPhoreTosuppressValueChange = false;
                    }

                    myPictureBox.BringToFront();
                    myPictureBox.Refresh();
                   // btn_PAClearPhoto.Enabled = false;
                    //if (myPictureBox != null)
                    //{
                    //    myPictureBox.Dispose();
                    //    myPictureBox = null;
                    //}

                }
                //Added for Bug #77539: 00000115 : Patient Setup
                _ispicPAPhotomodified = true;
            }
                
            catch //(Exception ex)
            {
            }            
        }
        private void AspectRatio(Image img)
        {
            picPAPhoto.BackgroundImageLayout = ImageLayout.Stretch;
            //picPAPhoto.SizeMode = PictureBoxSizeMode.CenterImage;
            int picOutputwidth = picPAPhoto.Width;
            int picOutputheight = picPAPhoto.Height;
            int OutputWidth = img.Width;
            int OutputHeight = img.Height;

            double myPicWidth = picOutputwidth;
            double myPicHeight = picOutputheight;

            double myScaleX = myPicWidth / (double)OutputWidth;
            double myScaleY = myPicHeight / (double)OutputHeight;
            double myStartX = 0;
            double myStartY = 0;

            if (myScaleX < myScaleY)
            {
                myPicWidth = (double)OutputWidth * myScaleY;
                myStartX = ((double)picOutputwidth - myPicWidth) / 2;
            }
            else
            {
                myPicHeight = (double)OutputHeight * myScaleX;
                myStartY = ((double)picOutputheight - myPicHeight) / 2;
            }
            Image myimg = new Bitmap(img, new Size((int)myPicWidth, (int)myPicHeight));
            //Disposing object
            if (picPAPhoto.Image != null)
            {

                try
                {
                    picPAPhoto.Image.Dispose();
                    picPAPhoto.Image = null;
                }
                catch
                {
                }

            }
            picPAPhoto.Image = myimg;
        }

        // GLO2010-0007047 [BJMC]: Webcam image too small
        private void btn_PAClearPhoto_Click(object sender, EventArgs e)
        {
            //Bug #75032: gloEMR-Webcam-Application is shifting the focus from webcam to browse when we capture picture
            if (btn_PAClearPhoto.Text == "Clear Photo")
            {
                try
                {
                    myNewTrackBar.Visible = false;
                    TrackbarPlus.Visible = false;
                    TrackbarMinus.Visible = false;
                    //Disposing object
                    if (picPAPhoto.Image != null)
                    {

                        try
                        {
                            picPAPhoto.Image.Dispose();
                            picPAPhoto.Image = null;
                        }
                        catch
                        {
                        }

                    }
                    picPAPhoto.Refresh();
                }
                catch (Exception) // ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
            }
            else
            {
                if (myPictureBox != null)
                {
                    frmConfigureWebCam myConfig = new frmConfigureWebCam();
                    myPictureBox.OtherOperations = true;
                    myConfig.ShowDialog(this);
                    myPictureBox.OtherOperations = false;
                    if (myConfig.okPressed)
                    {

                        myPictureBox.sCameraName = myConfig.sCameraName;
                        myPictureBox.iFPS = myConfig.iFPS;
                        myPictureBox.gloWebCameraClipingsGet();
                        isActivatedWebCam = myPictureBox.initCam(); //Start/OldCode
                        if (isActivatedWebCam)
                        {
                            myNewTrackBar.Visible = true;
                            TrackbarPlus.Visible = true;
                            TrackbarMinus.Visible = true;

                            semaPhoreTosuppressValueChange = true;
                            myNewTrackBar.Value = myPictureBox.ZoomValueForTrackBar;
                            semaPhoreTosuppressValueChange = false;
                        }


                    }
                    else
                    {

                    }
                    myConfig.Dispose();
                    myPictureBox.Refresh();
                }
            }
            //Added for Bug #77539: 00000115 : Patient Setup
            _ispicPAPhotomodified = true;
        }

        // GLO2010-0007047 [BJMC]: Webcam image too small
        private void btn_PACapturePhoto_Click(object sender, EventArgs e)
        {
            Image myImage = null;
            try
            {
                if (myPictureBox.iRunning)
                {
                    //Disposing object
                    if (picPAPhoto.Image != null)
                    {

                        try
                        {
                            picPAPhoto.Image.Dispose();
                            picPAPhoto.Image = null;
                        }
                        catch
                        {
                        }
                       
                    }
                    picPAPhoto.Image = myPictureBox.Image;
                    myPictureBox.closeCam();
                    myNewTrackBar.SendToBack();
                    picPAPhoto.gloWebCameraClipingsGet(myPictureBox);
                    myPictureBox.gloWebCameraClipingsSet();
                 //   picWebCamPatient.Enabled = false;
                    picPAPhoto.Visible = true;
                    myNewTrackBar.Visible = true;
                    TrackbarPlus.Visible = true;
                    TrackbarMinus.Visible = true;

                    isActivatedWebCam = false;
                    //Bug #75032: gloEMR-Webcam-Application is shifting the focus from webcam to browse when we capture picture
                    //rbBrowsePhoto.Checked = true;
                    //End
                    btn_PAClearPhoto.Enabled = true;
                    btn_PAClearPhoto.Text = "Clear Photo"; 
                    semaPhoreTosuppressValueChange = true;
                    myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
                    semaPhoreTosuppressValueChange = false;
                    //if (myPictureBox != null)
                    //{
                    //    myPictureBox.Dispose();
                    //    myPictureBox = null;
                    //}
                    myPictureBox.SendToBack();
                    picPAPhoto.Refresh();
                }
                //Problem No: 00000361
                //Else part is added, work same as browse.
                else
                {
                    if (rbBrowsePhoto.Checked == true)
                    {
                        picPAPhoto.Visible = true;
                        myImage = copyClipBoard();
                        if (myImage != null)
                        {
                            picPAPhoto.AspectRatio(myImage, true);
                            if (picPAPhoto.Image != null)
                            {
                                myNewTrackBar.SendToBack();
                              //  picPAPhoto.gloWebCameraClipingsGet( picPAPhoto.Zoom , picPAPhoto.Rotation, picPAPhoto.PLocation, new Point(0, 0));
                                isActivatedWebCam = false;
                             //   picWebCamPatient.Enabled = false;
                                picPAPhoto.Visible = true;
                                myNewTrackBar.Visible = true;
                                TrackbarPlus.Visible = true;
                                TrackbarMinus.Visible = true;
                                btn_PAClearPhoto.Enabled = true;
                                btn_PAClearPhoto.Text = "Clear Photo"; 
                                semaPhoreTosuppressValueChange = true;
                                myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
                                semaPhoreTosuppressValueChange = false;
                               
                              
                            }
                        }
                        myPictureBox.SendToBack();
                        picPAPhoto.Refresh();
                    }
                }
                //Added for Bug #77539: 00000115 : Patient Setup
                _ispicPAPhotomodified = true;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (myImage != null)
                {
                    myImage.Dispose();
                    myImage = null;
                }
            }
        }

        // GLO2010-0007047 [BJMC]: Webcam image too small
        private void rbBrowsePhoto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBrowsePhoto.Checked == true)
            {
                rbBrowsePhoto.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                if (picPAPhoto.Image != null)
                {

                    myPictureBox.closeCam();
                    myPictureBox.Image = null;
                    myNewTrackBar.SendToBack();
                    //SLR: Commented, since on switching only between browse and web should not change the zoom values..

               //     picPAPhoto.gloWebCameraClipingsGet( myPictureBox.Zoom , myPictureBox.Rotation, myPictureBox.PLocation, myPictureBox.ELocation);
               //     myPictureBox.gloWebCameraClipingsSet();

                    isActivatedWebCam = false;
                //    picWebCamPatient.Enabled = false;
                    picPAPhoto.Visible = true;
                    myNewTrackBar.Visible = true;
                    TrackbarPlus.Visible = true;
                    TrackbarMinus.Visible = true;
                    rbBrowsePhoto.Checked = true;
                    btn_PAClearPhoto.Enabled = true;
                    btn_PAClearPhoto.Text = "Clear Photo"; 
                    semaPhoreTosuppressValueChange = true;
                    myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
                    semaPhoreTosuppressValueChange = false;
                   
                    myPictureBox.SendToBack();
                    picPAPhoto.Refresh();
                }
                else
                {
                    myNewTrackBar.Visible = false;
                    TrackbarPlus.Visible = false;
                    TrackbarMinus.Visible = false;
                    btn_PAClearPhoto.Enabled = false;
                    btn_PAClearPhoto.Text = "Clear Photo"; 
                    semaPhoreTosuppressValueChange = true;
                    myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
                    semaPhoreTosuppressValueChange = false;
                    picPAPhoto.Refresh();
                }
            }
            else
            {
                rbBrowsePhoto.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                //if (picPAPhoto.Image != null)

            }


            //if (rbBrowsePhoto.Checked == true)
            //{
            //    btn_PAPhotoBrowse.Enabled = true;
            //    btn_PACapturePhoto.Enabled = false;
            //}
            //else
            //{
            //    btn_PAPhotoBrowse.Enabled = false;
            //    btn_PACapturePhoto.Enabled = true;
            //}


            if (rbBrowsePhoto.Checked == true)
            {
                //Bug ID: 43309 
                //Description: gloEMR : New/Modify Patient : Text for Browse button is not consistence as it shows "Browse Photo" and then shows Browse
                //change made in text of btn_PAPhotoBrowse to Browse Photo instead of Browse as 
                //btn_PAPhotoBrowse.Text = "Browse";
                btn_PAPhotoBrowse.Text = "Browse Photo";
                //Problem No: 00000361
                //changes made to implement webcam functionality in TS
                //btn_PACapturePhoto.Enabled = false;
                btn_PACapturePhoto.Text = "From Clipboard";
                this.toolTip1.SetToolTip(btn_PACapturePhoto, "Capture from Clipboard");
                picPAPhoto.Visible = true;
                //when click on start webcam then clear Photo button became disable also when we change to browse so make it enable when browse is selected
                btn_PAClearPhoto.Enabled = true;
                btn_PAClearPhoto.Text = "Clear Photo";
           //     picWebCamPatient.Visible = false;
                //Capture photo button is shown in message box when switching between browse and webcam so made change ifimage not present then picture box send to back.
                if (picPAPhoto.Image == null)
                {
                    myPictureBox.SendToBack();
                }
                picPAPhoto.Refresh();
            }
            else
            {
                btn_PAPhotoBrowse.Text = "Start Webcam";
                //btn_PACapturePhoto.Enabled = true;
                //Problem No: 00000361
                //changes made to implement webcam functionality in TS
                btn_PACapturePhoto.Text = "Capture Photo";
                this.toolTip1.SetToolTip(btn_PACapturePhoto, "Capture Photo");
                picPAPhoto.Visible = false;
                btn_PAClearPhoto.Enabled = true;
                btn_PAClearPhoto.Text = "Select Camera";
                myPictureBox.BringToFront();
                myPictureBox.Refresh();
               // picWebCamPatient.Visible = true;
            }
            //Added for Bug #77539: 00000115 : Patient Setup
            _ispicPAPhotomodified = true;
        }

        #endregion

        #region Pharmacy
        //Browse Pharmacy
        private void btn_PAPharmaBr_Click(object sender, EventArgs e)
        {
            try
            {
                // onPharmaBrowse_Clicked(sender, e);               
                /// 
                this.Cursor = Cursors.WaitCursor;
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                //SHUBHANGI 20101222
                //oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Pharmacy, false, this.Width);
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Pharmacy, true, this.Width);
                //oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Pharmacy";

                //_CurrentControlType = gloListControl.gloListControlType.Pharmacy;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_PharmaSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                oListControl.Dock = DockStyle.Fill;
                this.Controls.Add(oListControl);


                DataTable oFillTable = null;
                oFillTable = (DataTable)cmbPAPharma.DataSource;

                //MaheshB
                if (oFillTable != null && oFillTable.Rows.Count > 0)
                {
                    for (int i = 0; i <= oFillTable.Rows.Count - 1; i++)
                    {
                        Int64 _TagValue = 0;
                        if (Convert.ToString(cmbPAPharma.Tag) == "")
                        {
                            _TagValue = 0;
                        }
                        else
                        {
                            _TagValue = Convert.ToInt64(cmbPAPharma.Tag);
                        }
                        oListControl.SelectedItems.Add(Convert.ToInt64(oFillTable.Rows[i][0].ToString()), oFillTable.Rows[i][1].ToString(), _TagValue);
                        // oListControl.SelectedItems.Add(Convert.ToInt64(oFillTable.Rows[i][0].ToString()), oFillTable.Rows[i][1].ToString(),oFillTable.Rows);
                    }
                }

                if (oFillTable != null)
                {
                    oFillTable = null;
                }

                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        //Select Pharmacy
        private void oListControl_PharmaSelectedClick(object sender, EventArgs e)
        {
            try
            {
                //    //SHUBHANGI 20101222
                //    //txtPAPharma.Text = "";
                //    //txtPAPharma.Tag = null;
                //    cmbPAPharma.Text = "";
                //    cmbPAPharma.Tag = null;

                //    if (oListControl.SelectedItems.Count > 0)
                //    {
                //        for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                //        {
                //            //SHUBHANGI
                //            //// nContactID, sName, sContact
                //            //txtPAPharma.Tag = oListControl.SelectedItems[i].ID; //nContactID
                //            ////oListControl.SelectedItems[_Counter].Code ; //nName
                //            //txtPAPharma.Text = oListControl.SelectedItems[i].Description; //nContact
                //            cmbPAPharma.Tag = oListControl.SelectedItems[i].ID;
                //            cmbPAPharma.Text = oListControl.SelectedItems[i].Description;
                //        }
                //    }
               // cmbPAPharma.Items.Clear();
                cmbPAPharma.DataSource = null;
                cmbPAPharma.Items.Clear();
                DataTable dtPharma = new DataTable();
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");
                DataColumn dcDefault = new DataColumn("Default");
                dtPharma.Columns.Add(dcId);
                dtPharma.Columns.Add(dcDescription);
                Boolean blnDefault = false;
                //MaheshB
                dtPharma.Columns.Add(dcDefault);
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (int i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtPharma.NewRow();

                        drTemp["ID"] = oListControl.SelectedItems[i].ID;
                        drTemp["Description"] = oListControl.SelectedItems[i].Description;


                        //MaheshB
                        if ( Convert.ToInt64(oListControl.SelectedItems[i].Default) > 0 && blnDefault == false)
                        {
                            //drTemp["Default"] = oListControl.SelectedItems[i].Default;
                            cmbPAPharma.Tag = Convert.ToInt64(oListControl.SelectedItems[i].ID);
                            blnDefault = true;
                        }
                        else
                        {
                            if (blnDefault == false)
                                cmbPAPharma.Tag = Convert.ToInt64(oListControl.SelectedItems[0].ID);
                        }
                        dtPharma.Rows.Add(drTemp);
                    }
                }
                cmbPAPharma.DataSource = dtPharma;
                cmbPAPharma.ValueMember = dtPharma.Columns["ID"].ColumnName;
                cmbPAPharma.DisplayMember = dtPharma.Columns["Description"].ColumnName;
                if (Convert.ToInt64(cmbPAPharma.Tag) == 0)
                    cmbPAPharma.SelectedValue = dtPharma.Rows[0]["ID"].ToString();
                else
                    cmbPAPharma.SelectedValue = cmbPAPharma.Tag;
                cmbPAPharma.DrawMode = DrawMode.Normal;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                onDemographicControl_Enter(sender, e);
            }
            _isPharmacyModified = true;

            // added by sandip dhakane 20100715 for setting focus on pharmacy textbox after selecting pharmacy
            //SHUBHANGI
            //txtPAPharma.Focus();
            cmbPAPharma.Focus();
        }

        //Cleare Pharmacy
        private void btn_PAPharmaDel_Click(object sender, EventArgs e)
        {
            try
            {
                //SHUBHANGI  
                //txtPAPharma.Text = "";
                //txtPAPharma.Tag = null;
               
                cmbPAPharma.DataSource = null;
                cmbPAPharma.Items.Clear();
                cmbPAPharma.Tag = null;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            _isPharmacyModified = true;
        }
        #endregion

        #region Provider
        //Browse provider
        private void btnProviderSelect_Click(object sender, EventArgs e)
        {

            //onProviderSelect_Clicked(sender, e);
            try
            {
                // onPharmaBrowse_Clicked(sender, e);               
                /// 
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, false, this.Width);
                //oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Providers";

                //_CurrentControlType = gloListControl.gloListControlType.Pharmacy;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ProviderSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                this.Controls.Add(oListControl);

                //

                //
                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }

        //Select Provider
        private void oListControl_ProviderSelectedClick(object sender, EventArgs e)
        {
            try
            {
                txtPAProvider.Text = "";
                txtPAProvider.Tag = null;
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        txtPAProvider.Tag = oListControl.SelectedItems[i].ID;
                        txtPAProvider.Text = oListControl.SelectedItems[i].Description;
                    }
                }

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                onDemographicControl_Enter(sender, e);
            }
            // added by sandip dhakane 20100715 for setting focus on Provider textbox after selecting Provider
            txtPAProvider.Focus();
        }

        //Clear Provider 
        private void btnProviderDelete_Click(object sender, EventArgs e)
        {
            try
            {
                txtPAProvider.Text = "";
                txtPAProvider.Tag = null;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }
        #endregion

        #region Referrals
        //Browse Referals
        private void btn_PAReferralsBr_Click(object sender, EventArgs e)
        {
            //onProviderSelect_Clicked(sender, e);
            try
            {
                // onPharmaBrowse_Clicked(sender, e);               
                /// 
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, true, this.Width);
                //oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Referrals";

                //_CurrentControlType = gloListControl.gloListControlType.Pharmacy;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_refferalsSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                oListControl.Dock = DockStyle.Fill;
                this.Controls.Add(oListControl);

                hashRefferals = new Hashtable();

                //

                if (cmbPAReferrals.Items.Count > 0)
                {
                    DataTable dtReferals = new DataTable();
                    dtReferals = (DataTable)(cmbPAReferrals.DataSource);
                    for (int i = 0; i < dtReferals.Rows.Count; i++)
                    {
                        cmbPAReferrals.SelectedIndex = i;
                        oListControl.SelectedItems.Add(Convert.ToInt64(dtReferals.Rows[i]["ID"].ToString()), dtReferals.Rows[i]["Description"].ToString(), Convert.ToDateTime(dtReferals.Rows[i]["MuDate"].ToString()), Convert.ToBoolean(dtReferals.Rows[i]["MuCheckBox"].ToString()));
                        hashRefferals.Add(dtReferals.Rows[i]["ID"].ToString(),Convert.ToString(Convert.ToDateTime(dtReferals.Rows[i]["MuDate"].ToString()))+","+dtReferals.Rows[i]["MuCheckBox"].ToString());
                    }
                }

                //for (int i = 0; i < cmbPAReferrals.Items.Count; i++)
                //{
                //    cmbPAReferrals.SelectedIndex = i;
                //    oListControl.SelectedItems.Add(Convert.ToInt64(cmbPAReferrals.SelectedValue), cmbPAReferrals.Text);
                //    //cmbPAReferrals.SelectedIndex = -1;  
                //}

                if (cmbPAReferrals.Items.Count > 0)
                    cmbPAReferrals.SelectedIndex = 0;

                //Added By Mitesh
                //if (cmbPAReferrals.Enabled)
                //{
                //    cmbPAReferrals.DrawMode = DrawMode.OwnerDrawFixed;
                //}
                //else
                //{
                //    cmbPAReferrals.DrawMode = DrawMode.Normal;
                //}

                //-------



                //
                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }

        //Select Refferal
        private void oListControl_refferalsSelectedClick(object sender, EventArgs e)
        {
            try
            {
               // cmbPAReferrals.Items.Clear();
                cmbPAReferrals.DataSource = null;
                cmbPAReferrals.Items.Clear();
                DataTable dtReff = new DataTable();
                DataColumn dcId = new DataColumn("ID", typeof(long));
                DataColumn dcDescription = new DataColumn("Description", typeof(string));
                DataColumn dcMuDate = new DataColumn("MuDate", typeof(DateTime));
                DataColumn dcMuCheckBox = new DataColumn("MuCheckBox", typeof(bool));
             
                dtReff.Columns.Add(dcId);
                dtReff.Columns.Add(dcDescription);
                dtReff.Columns.Add(dcMuDate);
                dtReff.Columns.Add(dcMuCheckBox);

                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtReff.NewRow();
                        drTemp["ID"] = oListControl.SelectedItems[i].ID;
                        drTemp["Description"] = oListControl.SelectedItems[i].Description;

                        if (hashRefferals.ContainsKey(oListControl.SelectedItems[i].ID.ToString()))
                        {
                            string value = Convert.ToString(hashRefferals[Convert.ToString(oListControl.SelectedItems[i].ID)]);
                            string[] arrStr = value.Split(',');
                            if (arrStr.Length > 0)
                            {
                                drTemp["MuDate"] = Convert.ToDateTime(arrStr[0]);
                                drTemp["MuCheckBox"] = Convert.ToBoolean(arrStr[1]);
                            }
                        }
                        else
                        {
                            drTemp["MuDate"] = System.DateTime.Now;
                            drTemp["MuCheckBox"] = true;
                        }
                        dtReff.Rows.Add(drTemp);
                    }
                }
                cmbPAReferrals.DataSource = dtReff;
                cmbPAReferrals.ValueMember = dtReff.Columns["ID"].ColumnName;
                cmbPAReferrals.DisplayMember = dtReff.Columns["Description"].ColumnName;
                cmbPAReferrals.DrawMode = DrawMode.Normal;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                onDemographicControl_Enter(sender, e);
            }
            _isRefferalsModified = true;

            // added by sandip dhakane 20100715
            cmbPAReferrals.Focus();
        }

        //Clear Refferal 
        private void btn_PAReferralsDel_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dtReff = (DataTable)cmbPAReferrals.DataSource;
                //if (cmbPAReferrals.Items.Count > 0 && cmbPAReferrals.SelectedIndex != -1)
                //{
                //    dtReff.Rows.RemoveAt(cmbPAReferrals.SelectedIndex);
                //    dtReff.AcceptChanges();
                //}
                //cmbPAReferrals.DataSource = dtReff;
                //cmbPAReferrals.Refresh();
                //cmbPAReferrals.SelectedIndex = -1;

                //code comment start by nilesh on date 20101220 for case GLO2010-0005424
                //DataTable dtReff = (DataTable)cmbPAReferrals.DataSource;
                ////for (int i = cmbPAReferrals.Items.Count -1; i >= 0; i--)
                ////{
                ////    dtReff.Rows.RemoveAt(i);
                ////    dtReff.AcceptChanges();
                ////}
                //dtReff.Rows.Clear();
                //cmbPAReferrals.DataSource = dtReff;
                //cmbPAReferrals.Refresh();
                //cmbPAReferrals.SelectedIndex = -1;
                //cmbPAReferrals.DrawMode = DrawMode.OwnerDrawFixed;
                //code comment end by nilesh on date 20101220 for case GLO2010-0005424

                //code start by nilesh on date 20101220 for case GLO2010-0005424
                DataTable dtReff = null;
                dtReff = (DataTable)cmbPAReferrals.DataSource;

                if (cmbPAReferrals.Items.Count > 0 && cmbPAReferrals.SelectedIndex != -1)
                {
                    dtReff.Rows.RemoveAt(cmbPAReferrals.SelectedIndex);
                    dtReff.AcceptChanges();
                }
                cmbPAReferrals.DataSource = dtReff;
                cmbPAReferrals.ValueMember = dtReff.Columns[0].ColumnName;
                cmbPAReferrals.DisplayMember = dtReff.Columns[1].ColumnName;
                cmbPAReferrals.SelectedIndex = 0;
                cmbPAReferrals.DrawMode = DrawMode.Normal;
                //code end by nilesh on date 20101220 for case GLO2010-0005424

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            _isRefferalsModified = true;
        }
        #endregion

        #region Primary care Physician
        //Browse Primary care Physician
        private void btn_PrimaryCareBr_Click(object sender, EventArgs e)
        {
            //onProviderSelect_Clicked(sender, e);
            try
            {
                // onPharmaBrowse_Clicked(sender, e);               
                /// 
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Physicians, false, this.Width);
                //oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Physicians";

                //_CurrentControlType = gloListControl.gloListControlType.Pharmacy;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_PhysiciansSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                oListControl.Dock = DockStyle.Fill;
                this.Controls.Add(oListControl);

                //

                //
                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        //select Primary care Physician
        private void oListControl_PhysiciansSelectedClick(object sender, EventArgs e)
        {
            try
            {
                txtPAPrimaryCarePhy.Text = "";
                txtPAPrimaryCarePhy.Tag = null;
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {

                        txtPAPrimaryCarePhy.Tag = oListControl.SelectedItems[i].ID;
                        txtPAPrimaryCarePhy.Text = oListControl.SelectedItems[i].Description;

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
                _isPrimaryCarePhysicianModified = true;
                onDemographicControl_Enter(sender, e);
            }

            // added by sandip dhakane 20100715 for setting focus on Physician textbox after selecting Physician

            txtPAPrimaryCarePhy.Focus();
        }

        //Clear primary care Ptysician
        private void btn_PrimaryCareDel_Click(object sender, EventArgs e)
        {
            try
            {
                txtPAPrimaryCarePhy.Text = "";
                txtPAPrimaryCarePhy.Tag = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            _isPrimaryCarePhysicianModified = true;
        }
        #endregion

        #region OtherDetails

        private void btnOtherDetails_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDemographicInfo.Visible = false;
                removeogloPatientOtherInfoCntrl();
                ogloPatientOtherInfoCntrl = new gloPatientOtherInfoControl(_databaseconnectionstring);
                ogloPatientOtherInfoCntrl.PatientDemographicOtherInfo = this.PatientDemographicOtherInfo;
                ogloPatientOtherInfoCntrl.onOtherDetails_SaveClicked += new gloPatientOtherInfoControl.onOtherDetailsSaveClicked(ogloPatientOtherInfoCntrl_onOtherDetails_SaveClicked);
                ogloPatientOtherInfoCntrl.onOtherDetailsClose_Clicked += new gloPatientOtherInfoControl.onOtherDetailsCloseClicked(ogloPatientOtherInfoCntrl_onOtherDetailsClose_Clicked);
                this.Parent.Parent.Height = ogloPatientOtherInfoCntrl.Height;
                ogloPatientOtherInfoCntrl.Dock = DockStyle.Fill;
                this.Controls.Add(ogloPatientOtherInfoCntrl);
                //ogloPatientOtherInfoCntrl.BringToFront();
                onDemographicControl_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ogloPatientOtherInfoCntrl_onOtherDetailsClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                removeogloPatientOtherInfoCntrl();
                //this.Controls.Remove(ogloPatientOtherInfoCntrl);
                //Disposed object
                ogloPatientOtherInfoCntrl.Visible = false;
                //if (ogloPatientOtherInfoCntrl != null) { ogloPatientOtherInfoCntrl.Dispose(); }
                onDemographicControl_Enter(sender, e);
                pnlDemographicInfo.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR: " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void ogloPatientOtherInfoCntrl_onOtherDetails_SaveClicked(object sender, EventArgs e)
        {
            try
            {
                _PatientDemographicOtherInfo = ogloPatientOtherInfoCntrl.PatientDemographicOtherInfo;
                _isPatientOtherModified = true;
                removeogloPatientOtherInfoCntrl();
                pnlDemographicInfo.Visible = true;
                onDemographicControl_Enter(sender, e);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void removeogloPatientOtherInfoCntrl()
        {
            if (ogloPatientOtherInfoCntrl != null)
            {
                if (this.Controls.Contains(ogloPatientOtherInfoCntrl))
                {
                    this.Controls.Remove(ogloPatientOtherInfoCntrl);
                }

                try
                {
                    ogloPatientOtherInfoCntrl.onOtherDetails_SaveClicked += new gloPatientOtherInfoControl.onOtherDetailsSaveClicked(ogloPatientOtherInfoCntrl_onOtherDetails_SaveClicked);
                    ogloPatientOtherInfoCntrl.onOtherDetailsClose_Clicked += new gloPatientOtherInfoControl.onOtherDetailsCloseClicked(ogloPatientOtherInfoCntrl_onOtherDetailsClose_Clicked);
                }
                catch
                {
                }

                //Disposed object
                ogloPatientOtherInfoCntrl.Dispose(); 

            }
        }


        #endregion OtherDetails

        //Close list control
        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            //if (oListControl != null)
            //{
            //    for (int i = this.Controls.Count - 1; i >= 0; i--)
            //    {
            //        if (this.Controls[i].Name == oListControl.Name)
            //        {
            //            this.Controls.Remove(this.Controls[i]);
            //            break;
            //        }
            //    }
            //}
            removeOListControl();
            onDemographicControl_Enter(sender, e);
        }

        #endregion
        //Open form for Adding physician from List control
        private void oListControl_AddFormHandlerClick(object sender, EventArgs e)
        {
            if (oListControl.ControlHeader == "Referrals")
            {
                frmSetupPhysician ofrmAddContact = new frmSetupPhysician(_databaseconnectionstring);
                ofrmAddContact.CallFrom = "Physician";
                ofrmAddContact.ShowDialog(this);

                if (ofrmAddContact.DialogResult == DialogResult.OK)
                {
                    oListControl.FillListAsCriteria(ofrmAddContact.ContactID);

                }
                ofrmAddContact.Dispose();

            }
            else if (oListControl.ControlHeader == "Care Team")
            {
                frmSetupPhysician ofrmAddContact = new frmSetupPhysician(_databaseconnectionstring);
                ofrmAddContact.CallFrom = "Physician";
                ofrmAddContact.ShowDialog(this);

                if (ofrmAddContact.DialogResult == DialogResult.OK)
                {
                    oListControl.FillListAsCriteria(ofrmAddContact.ContactID);

                }
                ofrmAddContact.Dispose();

            }
            else if (oListControl.ControlHeader == "Physicians")
            {
                frmSetupPhysician ofrmAddContact = new frmSetupPhysician(_databaseconnectionstring);
                ofrmAddContact.CallFrom = "Physician";
                ofrmAddContact.ShowDialog(this);
                if (ofrmAddContact.DialogResult == DialogResult.OK)
                {
                    oListControl.FillListAsCriteria(ofrmAddContact.ContactID);
                }
                ofrmAddContact.Dispose();
            }
            else if (oListControl.ControlHeader == "Pharmacy")
            {
                frmSetupPharmacy ofrmAddContact = new frmSetupPharmacy(_databaseconnectionstring);
                ofrmAddContact.ShowDialog(this);

                if (ofrmAddContact.DialogResult == DialogResult.OK)
                {
                    oListControl.FillListAsCriteria(ofrmAddContact.ContactID);
                }
                ofrmAddContact.Dispose();
            }

        }
        //Added by Mayuri:20100810-To open form for modify
        private void oListControl_ModifyFormHandlerClick(object sender, EventArgs e)
        {

            if (oListControl.ControlHeader == "Referrals")
            {
                if (oListControl.dgListView.CurrentRow != null)
                {
                    _Contactid = Convert.ToInt64(oListControl.dgListView["nContactID", oListControl.dgListView.CurrentRow.Index].Value);
                    _sSPIID = Convert.ToString(oListControl.dgListView["sSPI", oListControl.dgListView.CurrentRow.Index].Value);
                }
                if (oListControl.dgListView.Rows.Count != 0)
                {
                    frmSetupPhysician ofrmModifyContact = new frmSetupPhysician(_Contactid, _databaseconnectionstring);
                    if (_sSPIID == "")
                    { ofrmModifyContact.CallFrom = "Physician"; }
                    else
                    { ofrmModifyContact.CallFrom = "Direct Physician"; }

                        ofrmModifyContact.ShowDialog(this);

                    if (ofrmModifyContact.DialogResult == DialogResult.OK)
                    {
                        _Ismodify = true;
                        oListControl.FillListAsCriteria(ofrmModifyContact.ContactID);

                        // oListControl.FillListAsCriteria1(ofrmModifyContact.ContactID, true);

                    }
                    ofrmModifyContact.Dispose();
                }

            }
            else
                if (oListControl.ControlHeader == "Care Team")
                {
                    if (oListControl.dgListView.CurrentRow != null)
                    {
                        _Contactid = Convert.ToInt64(oListControl.dgListView["nContactID", oListControl.dgListView.CurrentRow.Index].Value);
                        _sSPIID = Convert.ToString(oListControl.dgListView["sSPI", oListControl.dgListView.CurrentRow.Index].Value);
                    }
                    if (oListControl.dgListView.Rows.Count != 0)
                    {
                        frmSetupPhysician ofrmModifyContact = new frmSetupPhysician(_Contactid, _databaseconnectionstring);
                        if (_sSPIID == "")
                        { ofrmModifyContact.CallFrom = "Physician"; }
                        else
                        { ofrmModifyContact.CallFrom = "Direct Physician"; }
                        ofrmModifyContact.ShowDialog(this);

                        if (ofrmModifyContact.DialogResult == DialogResult.OK)
                        {
                            _Ismodify = true;
                            oListControl.FillListAsCriteria(ofrmModifyContact.ContactID);

                            // oListControl.FillListAsCriteria1(ofrmModifyContact.ContactID, true);

                        }
                        ofrmModifyContact.Dispose();
                    }

                }
            else if (oListControl.ControlHeader == "Physicians")
            {

                if (oListControl.dgListView.CurrentRow != null)
                {
                    _Contactid = Convert.ToInt64(oListControl.dgListView["nContactID", oListControl.dgListView.CurrentRow.Index].Value);
                    _sSPIID = Convert.ToString(oListControl.dgListView["sSPI", oListControl.dgListView.CurrentRow.Index].Value);
                }
                if (oListControl.dgListView.Rows.Count != 0)
                {
                    frmSetupPhysician ofrmModifyContact = new frmSetupPhysician(_Contactid, _databaseconnectionstring);
                    if (_sSPIID == "")
                    { ofrmModifyContact.CallFrom = "Physician"; }
                    else
                    { ofrmModifyContact.CallFrom = "Direct Physician"; }
                    ofrmModifyContact.ShowDialog(this);
                    if (ofrmModifyContact.DialogResult == DialogResult.OK)
                    {
                        oListControl.FillListAsCriteria(ofrmModifyContact.ContactID);
                    }
                    ofrmModifyContact.Dispose();
                }
            }
            else if (oListControl.ControlHeader == "Pharmacy")
            {
                if (oListControl.dgListView.CurrentRow != null)
                {
                    _Contactid = Convert.ToInt64(oListControl.dgListView["nContactID", oListControl.dgListView.CurrentRow.Index].Value);
                }
                if (oListControl.dgListView.Rows.Count != 0)
                {
                    frmSetupPharmacy ofrmModifyContact = new frmSetupPharmacy(_Contactid, _databaseconnectionstring);
                    ofrmModifyContact.ShowDialog(this);

                    if (ofrmModifyContact.DialogResult == DialogResult.OK)
                    {
                        oListControl.FillListAsCriteria(ofrmModifyContact.ContactID);
                    }
                    ofrmModifyContact.Dispose();
                }
            }




        }
        //End code Added by Mayuri:20100810

        //Problem No: 00000361
        //New function added to implement webcam functionality in TS
        public Bitmap copyClipBoard()
        {
            Bitmap _result = null;
            Bitmap webCamImg = default(Bitmap);
            try
            {
                IDataObject data = Clipboard.GetDataObject();
                if (data != null)
                {
                    if (data.GetDataPresent(DataFormats.Bitmap))
                    {
                        webCamImg = (data.GetData(DataFormats.Bitmap, true) as Bitmap);
                        _result = (Bitmap)webCamImg.Clone();
                    }
                }
                //if (webCamImg == null)
                //{
                //    // take the complete screen since there is a problem with Clipboard
                //    //webCamImg = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                //    // Create a graphics object from the bitmap.
                //    Graphics srcMem = Graphics.FromImage(webCamImg);

                //    // Take the screenshot from the upper left corner to the right bottom corner.
                //    srcMem.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                //    _result = (Bitmap)webCamImg.Clone();
                //    srcMem.Dispose();

                //}
                //else
                //{
                //    _result = (Bitmap)webCamImg.Clone();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Disposing object
                if (webCamImg != null)
                {
                    webCamImg.Dispose();
                    webCamImg = null;
                }

            }
            finally
            {
                if (webCamImg != null)
                {
                    webCamImg.Dispose();
                }
            }

            return _result;
        }

        #region Methods to Fill Combo Boxes

        private void fillHandDomain()
        {
            //Code commented by Mayuri:20090923
            //To fill comPAHandDom instead of Hardcoding values into combo box
            //cmbPAHandDom.Items.Add("Right Hand Dominant");
            //cmbPAHandDom.Items.Add("Left Hand Dominant");
            //Change Done by MAYURI on 20090921
            //Changed HandDominance 'others' by 'Ambidextrous' 
            //cmbPAHandDom.Items.Add("Ambidextrous");
            //End Code by MAYURI on 20090921
            //Code Added by Mayuri:20090923
            //To fill comPAHandDom 
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dthanddominant = null;
                string _sqlQuery = "SELECT nCategoryID, sDescription FROM Category_MST WHERE UPPER(sCategoryType) ='HAND DOMINANCE' AND bIsBlocked = '" + false + "' AND nClinicID = " + _ClinicID + " order by sDescription ";
                oDB.Retrive_Query(_sqlQuery, out dthanddominant);
                oDB.Disconnect();

                if (dthanddominant != null)
                {
                    DataRow dr = dthanddominant.NewRow();
                    dr["nCategoryID"] = 0;
                    dr["sDescription"] = "";
                    dthanddominant.Rows.InsertAt(dr, 0);
                    dthanddominant.AcceptChanges();

                    cmbPAHandDom.DataSource = dthanddominant;
                    cmbPAHandDom.DisplayMember = "sDescription";
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
                if (oDB != null) { oDB.Dispose(); }
            }


        }

        private void fillRace()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtRace = null;
                string _sqlQuery = "";

                if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                {
                    _sqlQuery = "SELECT -2 as nCategoryID, 'Declined to specify' as sDescription UNION SELECT -3 as nCategoryID, 'Unknown' as sDescription";
                }
                else
                {
                    _sqlQuery = "SELECT nCategoryID, sDescription FROM Category_MST WHERE UPPER(sCategoryType) ='RACE' AND bIsBlocked = '" + false + "' AND nClinicID = " + _ClinicID + " order by sDescription ";
                }
                oDB.Retrive_Query(_sqlQuery, out dtRace);
                oDB.Disconnect();

                if (dtRace != null)
                {
                    DataRow dr = dtRace.NewRow();
                    dr["nCategoryID"] = 0;
                    dr["sDescription"] = "";
                    dtRace.Rows.InsertAt(dr, 0);
                    dtRace.AcceptChanges();

                    if (!gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    {
                        DataRow dr1 = dtRace.NewRow();
                        dr1["nCategoryID"] = 1;
                        dr1["sDescription"] = "Declined to specify";
                        dtRace.Rows.InsertAt(dr1, 1);
                        dtRace.AcceptChanges();
                    }

                    cmbPARace.DataSource = dtRace;
                    cmbPARace.DisplayMember = "sDescription";
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
                if (oDB != null) { oDB.Dispose(); }
            }

            //cmbPARace.Items.Add("American");
            //cmbPARace.Items.Add("Black American");
        }

        private void fillMaritalStatus()
        {
            cmbPAMarital.Items.Add("UnMarried");
            cmbPAMarital.Items.Add("Married");
            cmbPAMarital.Items.Add("Single");
            cmbPAMarital.Items.Add("Widowed");
            cmbPAMarital.Items.Add("Divorced");
            cmbPAMarital.Refresh();
            cmbPAMarital.SelectedIndex = -1;
        }

        private void fillLocations()
        {
            try
            {
                gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();

                DataTable dt = oLocation.GetList();
                oLocation.Dispose();
                oLocation = null;
                if (dt != null)
                {
                    cmbPALocation.DataSource = dt;
                    cmbPALocation.DisplayMember = dt.Columns[1].ColumnName;
                    cmbPALocation.ValueMember = dt.Columns[1].ColumnName;

                    //Load default location 
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dt.Rows[i]["bIsDefault"]) == true)
                        {
                            cmbPALocation.SelectedIndex = i;
                            break;
                        }
                    }

                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }

        private void fillStates()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtStates = null;
                string _sqlQuery = "SELECT distinct ST FROM CSZ_MST order by ST";
                oDB.Retrive_Query(_sqlQuery, out dtStates);
                oDB.Disconnect();

                if (dtStates != null)
                {
                    DataRow dr = dtStates.NewRow();
                    dr["ST"] = "";
                    dtStates.Rows.InsertAt(dr, 0);
                    dtStates.AcceptChanges();

                    cmbPAState.DataSource = dtStates;
                    cmbPAState.DisplayMember = "ST";
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
                if (oDB != null) { oDB.Dispose(); }
            }

        }
        private void AutoGenratePatientCode_Off()
        {
            SqlConnection objCon = new SqlConnection();
            objCon.ConnectionString = _databaseconnectionstring;
            SqlCommand objCmd = new SqlCommand();
            //Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "gsp_UpdateSettings";
            SqlParameter objParaSettingsName = new SqlParameter();
            SqlParameter objParaSettingsValue = new SqlParameter();

            SqlParameter objParaSettingsClinicID = new SqlParameter();
            SqlParameter objParaSettingsUserID = new SqlParameter();
            SqlParameter objParaSettingsUserClinicFlag = new SqlParameter();

            try
            {
                objCmd.Connection = objCon;

                objCon.Open();
                objCmd.Parameters.Clear();
                objParaSettingsName.ParameterName = "@SettingsName";
                objParaSettingsName.Value = "Auto-Generate Patient Code";
                objParaSettingsName.Direction = ParameterDirection.Input;
                objParaSettingsName.SqlDbType = SqlDbType.VarChar;

                objCmd.Parameters.Add(objParaSettingsName);

                objParaSettingsValue.ParameterName = "@SettingsValue";
                objParaSettingsValue.Value = "0";
                objParaSettingsValue.Direction = ParameterDirection.Input;
                objParaSettingsValue.SqlDbType = SqlDbType.VarChar;

                objCmd.Parameters.Add(objParaSettingsValue);
                objParaSettingsClinicID.ParameterName = "@nClinicID";
                objParaSettingsClinicID.Value = 1;
                objParaSettingsClinicID.Direction = ParameterDirection.Input;
                objParaSettingsClinicID.SqlDbType = SqlDbType.BigInt;

                objCmd.Parameters.Add(objParaSettingsClinicID);


                objParaSettingsUserID.ParameterName = "@nUserID";
                objParaSettingsUserID.Value = 0;
                objParaSettingsUserID.Direction = ParameterDirection.Input;
                objParaSettingsUserID.SqlDbType = SqlDbType.BigInt;

                objCmd.Parameters.Add(objParaSettingsUserID);

                {
                    objParaSettingsUserClinicFlag.ParameterName = "@nUserClinicFlag";
                    objParaSettingsUserClinicFlag.Value = 1;
                    objParaSettingsUserClinicFlag.Direction = ParameterDirection.Input;
                    objParaSettingsUserClinicFlag.SqlDbType = SqlDbType.Int;
                }
                objCmd.Parameters.Add(objParaSettingsUserClinicFlag);
                //' End Add ClinicID, UserID,UserClinicFlag
                objCmd.ExecuteNonQuery();

                object oResult;
                gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                ogloSettings.GetSetting("UseSitePrefix", out oResult);
                ogloSettings.Dispose();
                ogloSettings = null;

                Int32 _UseSitePrefix = 0;
                if (oResult != null && oResult.ToString() != "")
                {
                    _UseSitePrefix = Convert.ToInt32(oResult);
                }
                if (_UseSitePrefix != 0)
                {
                    if (appSettings["PatientPrefix"] != null)
                    {
                        if (appSettings["PatientPrefix"] != "")
                        {
                            txtPACode.Text = Convert.ToString(appSettings["PatientPrefix"]);
                            SendKeys.Send("-");
                        }
                    }
                    if (txtPACode.Text.Trim() == "")
                    {


                        DataTable dtPrefix = null;
                        gloPatient oPatient = new gloPatient(_databaseconnectionstring);
                        dtPrefix = oPatient.GetPrefix();
                        if (dtPrefix != null)
                        {
                            if (dtPrefix.Rows.Count > 0)
                            {
                                txtPACode.Text = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                SendKeys.Send("-");

                            }
                        }
                        if (dtPrefix != null) { dtPrefix.Dispose(); }
                        if (oPatient != null) { oPatient.Dispose(); }
                    }

                }
                else
                {
                    txtPACode.Mask = "AAAAAAAAAAAAA";
                    txtPatientPrefix.Text = "";
                    txtPACode.Text = "";

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if ((objCon != null))
                {
                    objCon.Dispose();
                    objCon = null;
                }

                if ((objParaSettingsName != null))
                {
                    objParaSettingsName = null;
                }

                if ((objParaSettingsValue  != null))
                {
                    objParaSettingsValue = null;
                }

                if ((objParaSettingsClinicID != null))
                {
                    objParaSettingsClinicID = null;
                }

                if ((objParaSettingsUserID != null))
                {
                    objParaSettingsUserID = null;
                }

                if ((objParaSettingsUserClinicFlag != null))
                {
                    objParaSettingsUserClinicFlag = null;
                }

                if ((objCmd != null))
                {
                    objCmd.Parameters.Clear();
                    objCmd.Dispose();
                    objCmd = null;
                }
            }
        }

        private void GeneratePatientCode()
        {


            //try
            //{

            //    txtPACode.Text = ogloPatient.GeneratePatientCode(); 
            //}

            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            object oResult;
            object oResultAllowEditablePatientCode;
            oDB.Connect(false);

            try
            {
                //Solving Case GLO2010-0006553
                //if (_MessageBoxCaption.ToUpper().Trim() == "GLOPM")
                //{

                //    //MaheshB 20100113
                //    txtPACode.ReadOnly = true;
                //    txtPACode.Focus();
                //    txtPACode.Text = ogloPatient.GeneratePatientCode();
                //    _isAutogenerate = true;    
                //    if (ogloPatient._result.Length > 13)
                //    {
                //        if (MessageBox.Show("You have entered the maximum possible Patient Code. No more Patient Codes can be auto generated.  In order to register additional patients, auto generation of patient codes must be turned off in the gloEMR Admin. Do you want to continue with manual entry of patient code?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //        {
                //            txtPACode.ReadOnly = false;
                //            AutoGenratePatientCode_Off();
                //            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is now made turn off as no more Patient Codes can be auto generated, continue with manual entry.", gloAuditTrail.ActivityOutCome.Success);

                //        }
                //        else
                //        {
                //            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is still turn on even no more Patient Codes can be auto generated, now patient code will be differenet that what it is appearing on new patient registration screen.", gloAuditTrail.ActivityOutCome.Success);
                //        }
                //    }
                //}
                //else  //end
                {
                    // Int64 nProviderID = 0;
                    //code Added by Mayuri:20091229-To fix issue ID:#4837-Patient code is automatically generated even on unchecking ‘Autogenerated Patient code’ checkbox in ADMIN.
                    ogloSettings.GetSetting("Auto-Generate Patient Code", out oResult);
                    //
                    ogloSettings.GetSetting("Allow-Editable Patient Code", out oResultAllowEditablePatientCode);
                    Int32 _AutoGenerate = 0;
                    Int32 _AllowEditableCode = 0;
                    if (oResult != null && oResult.ToString() != "" && oResultAllowEditablePatientCode != null && oResultAllowEditablePatientCode.ToString() != "")
                    {
                        _AutoGenerate = Convert.ToInt32(oResult);
                        _AllowEditableCode = Convert.ToInt32(oResultAllowEditablePatientCode);
                        if (_AutoGenerate != 0 && _AllowEditableCode == 0) //only autogenerate true
                        {
                            txtPACode.ReadOnly = true;
                        }
                        //else if (_AutoGenerate != 0 && _AllowEditableCode != 0) //both true
                        //{
                        //    //txtPACode.ReadOnly = false;
                        //    txtPACode.ReadOnly = true;
                        //}
                        else  //autogenerate false
                        {
                            txtPACode.ReadOnly = false;
                        }
                        //
                        //Int32 _AutoGenerate = 0;
                        //if (oResult != null && oResult.ToString() != "")
                        //{
                        //    _AutoGenerate = Convert.ToInt32(oResult);
                        if (_AutoGenerate != 0)
                        {
                            //txtPACode.ReadOnly = true;
                            //To select ssn textbox if pateitn code textbox is non-editable
                            if (_PatientId == 0)
                            {
                                // else set to next control i.e. SSN Textbox
                                if (_AllowEditableCode == 0)
                                {
                                    txtmPASSN.Select();
                                }
                                else
                                {
                                    txtPACode.Focus();
                                }
                            }
                            else
                            {
                                txtPACode.Focus();
                            }
                            // nProviderID = Convert.ToInt64(value);
                            txtPACode.Text = ogloPatient.GeneratePatientCode();
                            _isAutogenerate = true;
                            if (ogloPatient._result.Length > 13)
                            {

                                if (MessageBox.Show("You have entered the maximum possible Patient Code. No more Patient Codes can be auto generated.  In order to register additional patients, auto generation of patient codes must be turned off in the gloEMR Admin. Do you want to continue with manual entry of patient code?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    txtPACode.ReadOnly = false;
                                    AutoGenratePatientCode_Off();
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is now made turn off as no more Patient Codes can be auto generated, continue with manual entry.", gloAuditTrail.ActivityOutCome.Success);
                                }
                                else
                                {
                                    txtPACode.Mask = "AAAAAAAAAAAAAA";
                                    txtPACode.Text = ogloPatient._result;
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is still turn on even no more Patient Codes can be auto generated, now patient code will be differenet that what it is appearing on new patient registration screen.", gloAuditTrail.ActivityOutCome.Success);
                                }
                            }
                        }
                    }
                    //object oResult;
                    //gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                    ogloSettings.GetSetting("UseSitePrefix", out oResult);
                    Int32 _UseSitePrefix = 0;
                    if (oResult != null && oResult.ToString() != "")
                    {
                        _UseSitePrefix = Convert.ToInt32(oResult);
                    }
                    if (_UseSitePrefix != 0)
                    {
                        if (_AutoGenerate == 0)
                        {
                            if (appSettings["PatientPrefix"] != null)
                            {
                                if (appSettings["PatientPrefix"] != "")
                                {
                                    txtPACode.Text = Convert.ToString(appSettings["PatientPrefix"]);
                                    SendKeys.Send("-");
                                }
                            }
                            if (txtPACode.Text.Trim() == "")
                            {


                                DataTable dtPrefix = null;
                                gloPatient oPatient = new gloPatient(_databaseconnectionstring);
                                dtPrefix = oPatient.GetPrefix();
                                if (dtPrefix != null)
                                {
                                    if (dtPrefix.Rows.Count > 0)
                                    {
                                        txtPACode.Text = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                        SendKeys.Send("-");

                                    }
                                }
                                if (dtPrefix != null) { dtPrefix.Dispose(); }
                                if (oPatient != null) { oPatient.Dispose(); }
                            }
                        }
                    }

                    //End code Added by Mayuri:20091229
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null)
                { oDB.Disconnect(); oDB.Dispose(); }
                if (ogloPatient != null) { ogloPatient.Dispose(); }
                if (ogloSettings != null) { ogloSettings.Dispose(); }
            }

        }

        private void SetDefaultProvider()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtResult = null;
            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                Int64 nProviderID = 0;
                ogloSettings.GetSetting("PatientDefaultProvider", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    nProviderID = Convert.ToInt64(value);
                }
                value = null;

                if (nProviderID <= 0)
                {
                    //_strSQL = " SELECT nProviderID AS ProviderID, " +
                    //            " (ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') + space(1) + ISNULL(sLastName,'')) AS ProviderName " +
                    //            " From Provider_MST WHERE Provider_MST.nClinicID = " + _ClinicID + " ";

                    _strSQL = " SELECT nProviderID AS ProviderID, " +
                               " (ISNULL(sFirstName,'') + space(1) + CASE ISNULL(sMiddleName,'') WHEN  '' THEN '' When sMiddleName then sMiddleName + SPACE(1) END + ISNULL(sLastName,'')) AS ProviderName " +
                               " From Provider_MST WHERE  bIsblocked <> 1 And Provider_MST.nClinicID = " + _ClinicID + " ";

                    //CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then " 
                    //              + "Provider_MST.sMiddleName + SPACE(1) END
                }
                else
                {
                    //_strSQL = " SELECT nProviderID AS ProviderID, " +
                    //            " (ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') + space(1) + ISNULL(sLastName,'')) AS ProviderName " +
                    //            " From Provider_MST WHERE Provider_MST.nProviderID = " + nProviderID + " AND Provider_MST.nClinicID = " + _ClinicID + " ";
                    _strSQL = " SELECT nProviderID AS ProviderID, " +
                              " (ISNULL(sFirstName,'') + space(1) + CASE ISNULL(sMiddleName,'') WHEN  '' THEN '' When sMiddleName then sMiddleName + SPACE(1) END  + ISNULL(sLastName,'')) AS ProviderName " +
                              " From Provider_MST WHERE bIsblocked <> 1 And Provider_MST.nProviderID = " + nProviderID + " AND Provider_MST.nClinicID = " + _ClinicID + " ";
                }

                oDB.Retrive_Query(_strSQL, out dtResult);
                //
                if (nProviderID == 0)
                {
                    DataRow dr = dtResult.NewRow();
                    dr["ProviderID"] = "0";
                    dr["ProviderName"] = "";
                    dtResult.Rows.InsertAt(dr, 0);
                    dtResult.AcceptChanges();
                }
                //
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    txtPAProvider.Text = Convert.ToString(dtResult.Rows[0]["ProviderName"]);
                    txtPAProvider.Tag = Convert.ToString(dtResult.Rows[0]["ProviderID"]);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                { oDB.Disconnect(); oDB.Dispose(); }
                if (dtResult != null) { dtResult.Dispose(); }
                if (ogloSettings != null) { ogloSettings.Dispose(); }
     
            }
        }

        //Sandip Darade 20090622
        //to read the the default patient gender setting for gloEMR50
        private void Set_DefaultPatientGender()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = null;
            try
            {

                ogloSettings.GetSetting("DefaultPatientGender", out value);
                if (value != null && Convert.ToString(value) != "")
                {

                    //Code commented by dipak 20100507 to change gender radio buttons to combo list
                    //if (Convert.ToString(value) == "Male")
                    //{
                    //    rbGender1.Checked = true;
                    //}

                    //if (Convert.ToString(value) == "Female")
                    //{
                    //    rbGender2.Checked = true;
                    //}

                    //if (Convert.ToString(value) == "Other")
                    //{
                    //    rbGender3.Checked = true;
                    //}
                    //line added by dipak to change gender radio buttons to combo list
                    cmbGender.Text = value.ToString().Trim();
                    //end code by dipak 20100507

                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
    
                if (ogloSettings != null) { ogloSettings.Dispose(); }
     
            }
        }

        private void fillRelationships()
        {

            // To Fill The Relationships
            RelationShip oRelationShip = new RelationShip(_databaseconnectionstring);
            DataTable dtRelation = null;
            dtRelation = oRelationShip.GetList();
            if (dtRelation != null)
            {
                DataRow dr = dtRelation.NewRow();
                dr["sRelationshipCode"] = "";
                dr["sRelationshipDesc"] = "";
                dtRelation.Rows.InsertAt(dr, 0);
                dtRelation.AcceptChanges();

                cmbRelationship.DataSource = dtRelation;
                cmbRelationship.ValueMember = dtRelation.Columns["sRelationshipCode"].ColumnName;
                cmbRelationship.DisplayMember = dtRelation.Columns["sRelationshipDesc"].ColumnName;

                if (dtRelation.Rows.Count > 0)
                {
                    cmbRelationship.SelectedIndex = 0;
                }

            }
            oRelationShip.Dispose();
            oRelationShip = null;
            //


        }

        private void FillBusinessCenter()
        {
            DataTable dtBusinessCenter = null;
            try
            {
                dtBusinessCenter = gloGlobal.gloPMMasters.GetBusinessCenterByState(true);
                if (dtBusinessCenter != null && dtBusinessCenter.Rows.Count > 0)
                {
                    //DataRow dr = dtBusinessCenter.NewRow();
                    //dr["nBusinessCenterID"] = "";
                    //dr["BusinessCenter"] = "";
                    //dtBusinessCenter.Rows.InsertAt(dr, 0);

                    cmbBusinessCenter.BeginUpdate();
                    cmbBusinessCenter.DataSource = dtBusinessCenter.Copy();
                    cmbBusinessCenter.DisplayMember = dtBusinessCenter.Columns["sBusinessCenterCode"].ColumnName;
                    cmbBusinessCenter.ValueMember = dtBusinessCenter.Columns["nBusinessCenterID"].ColumnName;
                    cmbBusinessCenter.EndUpdate();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (dtBusinessCenter != null) { dtBusinessCenter.Dispose(); }
            }
        }
        #endregion

        #region Methods to GetData, SetData, Validations
        //get demographic data
        /// <summary>
        /// Function check whether combos contain text is for add new category
        /// </summary>
        public void CheckforaddCategory()
        {
            KeyEventArgs e = new KeyEventArgs(Keys.Tab);
            cmbPAEthn_KeyDown(null, e);
            cmbPALang_KeyDown(null, e);
            cmbPARace_KeyDown(null, e);
            cmbCommPref_KeyDown(null, e);
        }

        #region "Start :: Pedetric Settings"
        public TimeSpan GetAgeInHrs(DateTime _DateOfBirth, String _BirthTime)
        {
            TimeSpan AgeDiff = new TimeSpan();
            String sDateTime = "";
            DateTime Bdate;
            try
            {
                Bdate = _DateOfBirth.Date;
                sDateTime = Bdate.ToShortDateString() + " " + _BirthTime;
                AgeDiff = DateTime.Now.Subtract(Convert.ToDateTime(sDateTime));


            }
            catch
            {

            }
            return AgeDiff;
        }

        //No Reqired to check the Pedetric settings
        //public void GetPediatricSetting()
        //{
        //    SqlConnection conn = new SqlConnection(_databaseconnectionstring);
        //    SqlCommand cmd = default(SqlCommand);
        //    string _strSQL = "";
        //    try
        //    {
        //        _strSQL = "select ISNULL(sSettingsValue,'') AS sSettingsValue from settings where sSettingsName='PEDIATRICS'";
        //        if ((conn != null))
        //        {
        //            if (conn.State == ConnectionState.Closed)
        //            {
        //                conn.Open();
        //            }
        //            cmd = new SqlCommand(_strSQL, conn);
        //            if ((cmd != null))
        //            {
        //                object _myValue = cmd.ExecuteScalar() ;
        //                glbIsPediatric = Convert.ToBoolean(_myValue);
        //            }
        //            if (conn.State == ConnectionState.Open)
        //            {
        //                conn.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        if ((cmd != null))
        //        {
        //            cmd.Dispose();
        //            cmd = null;
        //        }
        //        if ((conn != null))
        //        {
        //            conn.Dispose();
        //            conn = null;
        //        }
        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }

        //        if ((cmd != null))
        //        {
        //            cmd.Dispose();
        //            cmd = null;
        //        }
        //        if ((conn != null))
        //        {
        //            conn.Dispose();
        //            conn = null;
        //        }
        //    }

        //}
        #endregion "End :: Pedetric Settings"


        public bool GetData()
        {
            try
            {
                _bValidatePortalInvitationEmail = false;
                _bValidateAPIInvitationEmail = false;
                //if (IsModified())
                //{
                //    if (DialogResult.No == MessageBox.Show(" Do you want to save the changes ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                //        return false;
                //}                

                if (ValidateData())
                {
                    //if (oPatientDemo.PatientCode.Length > 13)
                    //{
                    //          if (MessageBox.Show("You have entered the maximum possible Patient Code. No more Patient Codes can be auto generated.  In order to register additional patients, auto generation of patient codes must be turned off in the gloEMR Admin. Do you want to continue?.", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //    {
                    //        txtPACode.Focus();
                    //        return false;
                    //    }
                    //}
                    //Patient Code,Name
                    if (_PatientId != 0)
                    {
                        _IsPatientCodeModified = IsPatientCodeModified();
                        oPatientDemo.PatientCode = txtPACode.Text.Trim();
                    }
                    else
                    {
                        //oPatientDemo.PatientCode = "0";
                    }

                    // Case# 0004279 Chaneged for gloEMR
                    // To Autogerate Patient Code in gloEMR if setting is turned ON the generate Patient Code else use the code which has been entered by user.
                    if (_MessageBoxCaption == "gloPM")
                    {
                        //oPatientDemo.PatientCode = "0";
                        oPatientDemo.PatientCode = txtPACode.Text.Trim();
                    }
                    else
                    {
                        oPatientDemo.PatientCode = txtPACode.Text.Trim();
                    }

                    //Added by SaiKrishna
                    _IsPatientDataModified = IsPatientDataModified();


                    oPatientDemo.PatientFirstName = txtPAFname.Text.Trim();
                    oPatientDemo.PatientMiddleName = txtPAMName.Text.Trim();
                    oPatientDemo.PatientLastName = txtPALName.Text.Trim();

                    oPatientDemo.PatientSuffix = txtPatSuffix.Text.Trim();

                    oPatientDemo.Signature = null;
                    //SSN
                    //if (txtmPASSN.MaskCompleted)
                    //    oPatientDemo.PatientSSN = txtmPASSN.Text;
                    //else
                    //    oPatientDemo.PatientSSN = "";

                    //dtpBirthTime Settings
                    oPatientDemo.PatientDOB = Convert.ToDateTime(mtxtPADOB.Text);
                    if (dtpBirth != null)
                    {
                        if (Convert.ToString(dtpBirth.Text).Trim() != "")
                        {
                            if (dtpBirth.Checked == true)
                            {
                                //oPatientDemo.BirthTime = dtpBirth.Text.Trim() + " " + Convert.ToString(cmbTimeformat.Text);
                                oPatientDemo.BirthTime = dtpBirth.Text.Trim();// +" " + Convert.ToString(cmbTimeformat.Text);

                            }
                            else
                            {
                                oPatientDemo.BirthTime = " ";
                            }
                        }
                    }

                    if (txtmPASSN.IsValidated == true)
                        oPatientDemo.PatientSSN = txtmPASSN.Text;
                    else
                        oPatientDemo.PatientSSN = "";

                    ////Sandip Darade 20100216 case 2029k
                    //if (txtmPASSN.Text.Length != 0 && txtmPASSN.MaskFull == false)
                    //{
                    //    oPatientDemo.PatientSSN = "";
                    //}
                    //else
                    //{
                    //    oPatientDemo.PatientSSN = txtmPASSN.Text;

                    //}
                    //DOB
                    //oPatientDemo.PatientDOB = dtpPADOB.Value;
                    mtxtPADOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                    oPatientDemo.PatientDOB = Convert.ToDateTime(mtxtPADOB.Text);

                    //MaritalStatus
                    if (cmbPAMarital.SelectedIndex != -1)
                    {
                        oPatientDemo.PatientMaritalStatus = cmbPAMarital.SelectedItem.ToString();
                    }

                    //Race
                    if (cmbPARace.SelectedIndex != -1)
                    {
                        if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                        {
                            if (cmbPARace.Items.Count > 0)
                            {
                                string strRace = "";

                                if (cmbPARace.Text == "" || cmbPARace.Text == "Declined to specify" || cmbPARace.Text == "Unknown")
                                {
                                    strRace = cmbPARace.Text;
                                }
                                else
                                {
                                    cmbPARace.SelectedIndex = 0;
                                    for (int race = 0; race <= cmbPARace.Items.Count - 1; race++)
                                    {
                                        if (race == 0)
                                        {
                                            strRace = cmbPARace.Text.Trim();
                                        }
                                        else
                                        {
                                            cmbPARace.SelectedIndex = race;
                                            strRace = strRace + "|" + cmbPARace.Text.Trim();
                                        }
                                    }
                                }
                                oPatientDemo.PatientRace = strRace;
                            }
                        }
                        else
                        {
                            oPatientDemo.PatientRace = cmbPARace.Text.Trim();
                        }
                    }

                    //Communcication Preference 
                    if (cmbCommPref.SelectedIndex != -1 || cmbCommPref.Text.Trim() == "")
                    {
                        oPatientDemo.PatientCommunicationPrefence = cmbCommPref.Text.Trim();
                    }

                    //if (cmbPAEthn.SelectedIndex != -1)
                    //{
                    //    oPatientDemo.PatientEthnicities = cmbPAEthn.Text.Trim();
                    //}

                    if (cmbPARace.SelectedIndex != -1)
                    {
                        if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                        {
                            if (cmbPAEthn.Items.Count > 0)
                            {
                                string strEthn = "";

                                if (cmbPAEthn.Text == "" || cmbPAEthn.Text == "Declined to specify" || cmbPAEthn.Text == "Unknown")
                                {
                                    strEthn = cmbPAEthn.Text;
                                }
                                else
                                {
                                    cmbPAEthn.SelectedIndex = 0;
                                    for (int ethn = 0; ethn <= cmbPAEthn.Items.Count - 1; ethn++)
                                    {
                                        if (ethn == 0)
                                        {
                                            strEthn = cmbPAEthn.Text.Trim();
                                        }
                                        else
                                        {
                                            cmbPAEthn.SelectedIndex = ethn;
                                            strEthn = strEthn + "|" + cmbPAEthn.Text.Trim();
                                        }
                                    }
                                }
                                oPatientDemo.PatientEthnicities = strEthn;
                            }
                        }
                        else
                        {
                            oPatientDemo.PatientEthnicities = cmbPAEthn.Text.Trim();
                        }
                    }

                    //if (cmbPALang.SelectedIndex != -1)
                    //{
                    oPatientDemo.PatientLanguage = cmbPALang.Text.Trim();
                    //}
                    oPatientDemo.PatientPrefix = txtPatientPrefix.Text;
                    //Hand Domain
                    if (cmbPAHandDom.SelectedIndex != -1)
                    {
                        oPatientDemo.PatientHandDominance = cmbPAHandDom.Text.Trim();
                        //oPatientDemo.PatientHandDominance = cmbPAHandDom.SelectedItem.ToString();
                    }

                    //gender
                    //code commented by dipak 20100507 to change gender radio buttons to combo list
                    //if (rbGender1.Checked)
                    //{
                    //    oPatientDemo.PatientGender = "Male";
                    //}
                    //if (rbGender2.Checked)
                    //{
                    //    oPatientDemo.PatientGender = "Female";
                    //}
                    //if (rbGender3.Checked)
                    //{
                    //    oPatientDemo.PatientGender = "Other";
                    //}
                    oPatientDemo.PatientGender = cmbGender.Text;
                    //end code added by dipak 20100507

                    //Pharmacy
                    //COMMENTED BY SHUBHANGI 20101222
                    //oPatientDemo.PatientPharmacyID = Convert.ToInt64(txtPAPharma.Tag);
                    //oPatientDemo.PharmacyName = txtPAPharma.Text.Trim();
                    oPatientDemo.Pharmacy.Clear();
                    if (cmbPAPharma.SelectedIndex != -1)
                    {
                        DataTable _dtPharma = (DataTable)cmbPAPharma.DataSource;
                        if (_dtPharma != null && _dtPharma.Rows.Count > 0)
                        {
                            for (int iRow = 0; iRow < _dtPharma.Rows.Count; iRow++)
                            {
                                // USED COLUMN NUMBER, CZ COLUMN NAMES ARE DIFFERENT WHILE BINDING DATASOURCE //
                                oPatientDemo.Pharmacy.Add(Convert.ToInt64(_dtPharma.Rows[iRow][0].ToString()), _dtPharma.Rows[iRow][1].ToString());
                            }
                        }
                    }

                    //Provider
                    oPatientDemo.PatientProviderID = Convert.ToInt64(txtPAProvider.Tag);
                    oPatientDemo.ProvideName = txtPAProvider.Text;

                    //Location
                    if (cmbPALocation.SelectedIndex > -1)
                    {
                        oPatientDemo.PatientLocation = cmbPALocation.SelectedValue.ToString();
                    }

                    //Primary Care Physician
                    oPatientDemo.PrimaryCarePhysicianName = txtPAPrimaryCarePhy.Text.Trim();
                    oPatientDemo.PatientPCPId = Convert.ToInt64(txtPAPrimaryCarePhy.Tag);

                    //Address Details
                    //oPatientDemo.PatientAddress1 = txtPAAddress1.Text.Trim();
                    //oPatientDemo.PatientAddress2 = txtPAAddress2.Text.Trim();
                    //oPatientDemo.PatientCity = txtPACity.Text.Trim();
                    //oPatientDemo.PatientState = cmbPAState.Text.ToString();
                    //oPatientDemo.PatientZip = txtPAZip.Text.Trim();
                    //oPatientDemo.PatientCounty = txtPACounty.Text.Trim();
                    //oPatientDemo.PatientCountry = cmbPACountry.Text.Trim();
                    //Sandip Darade 20091008 gloAddress control added for adding address information 
                    oPatientDemo.PatientAddress1 = oAddresscontrol.txtAddress1.Text.Trim();
                    oPatientDemo.PatientAddress2 = oAddresscontrol.txtAddress2.Text.Trim();
                    oPatientDemo.PatientCity = oAddresscontrol.txtCity.Text.Trim();
                    oPatientDemo.PatientState = oAddresscontrol.cmbState.Text.ToString();
                    // if (oAddresscontrol.checkZip(oAddresscontrol.txtZip.Text.Trim()) == true) //Dhruv---Allowing the user to add data----
                    //{
                    oPatientDemo.PatientZip = oAddresscontrol.txtZip.Text.Trim();              //no need to compare against the database the value exists or not
                    //}                                                                         //End------------------------Dhruv
                    oPatientDemo.PatientCounty = oAddresscontrol.txtCounty.Text.Trim();
                    oPatientDemo.PatientCountry = oAddresscontrol.cmbCountry.Text.Trim();
                    //7022Items: Home Billing
                    //Added to save area code for patient
                    oPatientDemo.AreaCode = oAddresscontrol.txtAreaCode.Text.Trim();

                    //Contact
                    oPatientDemo.PatientMobile = mtxtPAMobile.Text.Trim();
                    oPatientDemo.PatientPhone = mtxtPAPhone.Text.Trim();
                    oPatientDemo.PatientFax = txtPAFax.Text.Trim();


                    //Sandip Darade 20100216
                    //case  GLO2008-0002029
                    //phone no,mobile no ,fax no will be saved with  mask e.g .(111)222-3333
                    //if (mtxtPAMobile.Text.Length != 0 && mtxtPAMobile.MaskFull == false)
                    //{
                    //    oPatientDemo.PatientMobile = "";
                    //}
                    //else
                    //{
                    //    oPatientDemo.PatientMobile = mtxtPAMobile.Text;

                    //}
                    //if (mtxtPAPhone.Text.Length != 0 && mtxtPAPhone.MaskFull == false)
                    //{
                    //    oPatientDemo.PatientPhone = "";
                    //}
                    //else
                    //{
                    //    oPatientDemo.PatientPhone = mtxtPAPhone.Text;

                    //}

                    //if (_IsInternetFax == true)
                    //{
                    //    if (txtPAFax.Text.Length != 0 && txtPAFax.MaskFull == false)
                    //    {
                    //        oPatientDemo.PatientFax = "";
                    //    }
                    //    else
                    //    {
                    //        oPatientDemo.PatientFax = txtPAFax.Text.Trim();

                    //    }
                    //}
                    //else
                    //{
                    //    oPatientDemo.PatientFax = txtPAFax.Text.Trim();
                    //}

                    oPatientDemo.PatientEmail = txtPAEmail.Text.Trim();
                    //Patient Portal
                    if (cbSendPatientPortalActivationEmail.Visible)
                    {
                        if (cbSendPatientPortalActivationEmail.Checked)
                        {
                            gblnPatientPortalSendActivationEmail = true;
                        }
                        else
                        {
                            gblnPatientPortalSendActivationEmail = false;
                        }
                    }
                    else
                    {
                        gblnPatientPortalSendActivationEmail = false;
                        gblnPatientPortalActivationEmailAlreadySent = false;
                    }
                    //Patient Portal
                    //API
                    if (cbSendAPIInvitation.Visible)
                    {
                        if (cbSendAPIInvitation.Checked)
                        {
                            gblnPatientAPISendActivationEmail = true;
                        }
                        else
                        {
                            gblnPatientAPISendActivationEmail = false;
                        }
                    }
                    else
                    {
                        gblnPatientAPISendActivationEmail = false;
                        //ASK gblnPatientPortalActivationEmailAlreadySent = false;
                    }
                    //Emergency Contact
                    oPatientDemo.EmergencyContact = txtEmergencyContact.Text.Trim();
                    oPatientDemo.EmergencyPhone = mtxtEmergencyPhone.Text.Trim();
                    oPatientDemo.EmergencyMobile = mtxtEmergencyMobile.Text.Trim();

                    ////Sandip Darade 20100216 case 2029
                    //if (mtxtEmergencyPhone.Text.Length != 0 && mtxtEmergencyPhone.MaskFull == false)
                    //{
                    //    oPatientDemo.EmergencyPhone = "";
                    //}
                    //else
                    //{
                    //    oPatientDemo.EmergencyPhone = mtxtEmergencyPhone.Text;

                    //}
                    //if (mtxtEmergencyMobile.Text.Length != 0 && mtxtEmergencyMobile.MaskFull == false)
                    //{
                    //    oPatientDemo.EmergencyMobile = "";
                    //}
                    //else
                    //{
                    //    oPatientDemo.EmergencyMobile = mtxtEmergencyMobile.Text;

                    //}
                    //Added by Anil 20090713
                    if (cmbRelationship.SelectedIndex != -1)
                    {
                        oPatientDemo.EmergencyRelationshipCode = cmbRelationship.SelectedValue.ToString();
                        oPatientDemo.EmergencyRelationshipDesc = cmbRelationship.Text.ToString();
                    }

                    //Start/ Commented Code
                    ////Photo
                    //oPatientDemo.PatientPhoto = picPAPhoto.copyFrame(); 
                    //End/ Commented Code

                    //Start/New Code
                   //SLR: Following code note needed since myPictureboxControl will set this
                    //oPatientDemo.PatientPhoto = picPAPhoto.copyFrame(true);
                    oPatientDemo.MyPictureBoxControl = picPAPhoto.byteImage;
                    //End/New Code


                    //Misc
                    oPatientDemo.Directive = chkdirective.Checked;
                    oPatientDemo.ExemptFromReport = chkExempt.Checked;
                    //Added by Mayuri:20091021
                    // oPatientDemo.ExcludeFromStatement = chkExcludefromStatement.Checked;
                    //End code Added by Mayuri:20091021

                    //Gurantor
                    // oPatientDemo.IsGuarantor = ckhGaurantorSameAsPatient.Checked;


                    //YES/No Labs
                    oPatientDemo.IsYesNoLab = chkYesNoLab.Checked;

                    //oPatientDemo.PatientGuarantorID = Convert.ToInt64(txtPAGuarantor.Tag);
                    //oPatientDemo.PatientGuarantor = txtPAGuarantor.Text.Trim();

                    //Refferals
                    oPatientDemo.Referrals.Clear();
                    DataTable _dt = (DataTable)cmbPAReferrals.DataSource;
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        for (int iRow = 0; iRow < _dt.Rows.Count; iRow++)
                        {
                            // USED COLUMN NUMBER, CZ COLUMN NAMES ARE DIFFERENT WHILE BINDING DATASOURCE //
                            //oPatientDemo.Referrals.Add(Convert.ToInt64(_dt.Rows[iRow][0].ToString()), _dt.Rows[iRow][1].ToString());
                            oPatientDemo.Referrals.Add(Convert.ToInt64(_dt.Rows[iRow]["ID"].ToString()), _dt.Rows[iRow]["Description"].ToString(), Convert.ToDateTime(_dt.Rows[iRow]["MuDate"].ToString()), Convert.ToBoolean(_dt.Rows[iRow]["MuCheckBox"].ToString()));
                        }
                    }
                   





                    // COMMENT BY SUDHIR 20100327 // TO RESOLVE FLICKERRING ISSUE WHILE SAVING PATIENT //
                    //for (int i = 0; i < cmbPAReferrals.Items.Count; i++)
                    //{
                    //cmbPAReferrals.SelectedIndex = i;
                    //oPatientDemo.Referrals.Add(Convert.ToInt64(cmbPAReferrals.SelectedValue), cmbPAReferrals.Text);
                    //}


                    if (_isPharmacyModified == true)
                    {
                        #region " Get Pharmacy Details "
                        //shubhangi
                        //if (Convert.ToString(txtPAPharma.Tag) != "")
                        //if (Convert.ToString(cmbPAPharma.Tag) != "")
                        //{
                        //shubhangi
                        //DataTable dt = GetPatientDetails(Convert.ToInt64(txtPAPharma.Tag), PatientContactType.Pharmacy);
                        oPatientPharmacies.Clear();

                        //code start by nilesh on date 20101220 for case GLO2010-0005424
                        if (cmbPAPharma.SelectedIndex != -1)
                        {
                            DataTable dtPharma = null;
                            dtPharma = (DataTable)cmbPAPharma.DataSource;

                            //for (int i = 0; i < cmbPAReferrals.Items.Count; i++)
                            foreach (DataRow drRow in dtPharma.Rows)
                            {
                                DataTable dt = GetPatientDetails(Convert.ToInt64(drRow[0].ToString()), PatientContactType.Pharmacy);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //sName,sContact,sAddressLine1,sAddressLine2,sCity,sState,sZIP,sPhone,sFax,sEmail,sURL,sMobile,
                                    //sPager,sNotes,sFirstName,sMiddleName,sLastName,sGender,sTaxonomy,sTaxonomyDesc,sTaxID,sUPIN,
                                    //sNPI,sHospitalAffiliation,sExternalCode,sDegree,

                                    //oPatientPharmacies.Clear();

                                    PatientDetail _Pharmacy = new PatientDetail();

                                    _Pharmacy.PatientID = _PatientId;

                                    //_Pharmacy.ContactId = Convert.ToInt64(txtPAPharma.Tag);
                                    _Pharmacy.ContactId = Convert.ToInt64(drRow[0].ToString());
                                    _Pharmacy.Name = Convert.ToString(dt.Rows[0]["sName"]);
                                    _Pharmacy.Contact = Convert.ToString(dt.Rows[0]["sContact"]);
                                    _Pharmacy.AddressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                                    _Pharmacy.AddressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                                    _Pharmacy.City = Convert.ToString(dt.Rows[0]["sCity"]);
                                    _Pharmacy.State = Convert.ToString(dt.Rows[0]["sState"]);
                                    _Pharmacy.ZIP = Convert.ToString(dt.Rows[0]["sZIP"]);
                                    _Pharmacy.Phone = Convert.ToString(dt.Rows[0]["sPhone"]);
                                    _Pharmacy.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                                    _Pharmacy.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                                    _Pharmacy.URL = Convert.ToString(dt.Rows[0]["sURL"]);
                                    _Pharmacy.Mobile = Convert.ToString(dt.Rows[0]["sMobile"]);
                                    _Pharmacy.Pager = Convert.ToString(dt.Rows[0]["sPager"]);
                                    _Pharmacy.Notes = Convert.ToString(dt.Rows[0]["sNotes"]);
                                    _Pharmacy.FirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                                    _Pharmacy.MiddleName = Convert.ToString(dt.Rows[0]["sMiddleName"]);
                                    _Pharmacy.LastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                                    _Pharmacy.Gender = Convert.ToString(dt.Rows[0]["sGender"]);
                                    _Pharmacy.Taxonomy = Convert.ToString(dt.Rows[0]["sTaxonomy"]);
                                    _Pharmacy.TaxonomyDesc = Convert.ToString(dt.Rows[0]["sTaxonomyDesc"]);
                                    _Pharmacy.TaxID = Convert.ToString(dt.Rows[0]["sTaxID"]);
                                    _Pharmacy.UPIN = Convert.ToString(dt.Rows[0]["sUPIN"]);
                                    _Pharmacy.NPI = Convert.ToString(dt.Rows[0]["sNPI"]);
                                    _Pharmacy.HospitalAffiliation = Convert.ToString(dt.Rows[0]["sHospitalAffiliation"]);
                                    _Pharmacy.ExternalCode = Convert.ToString(dt.Rows[0]["sExternalCode"]);
                                    _Pharmacy.Degree = Convert.ToString(dt.Rows[0]["sDegree"]);

                                    _Pharmacy.NCPDPID = Convert.ToString(dt.Rows[0]["sNCPDPID"]);

                                    if (dt.Rows[0]["ActiveStartTime"] != DBNull.Value)
                                        _Pharmacy.ActiveStartTime = Convert.ToDateTime(dt.Rows[0]["ActiveStartTime"]);
                                    if (dt.Rows[0]["ActiveEndTime"] != DBNull.Value)
                                        _Pharmacy.ActiveEndTime = Convert.ToDateTime(dt.Rows[0]["ActiveEndTime"]);

                                    _Pharmacy.ServiceLevel = Convert.ToString(dt.Rows[0]["sServiceLevel"]);
                                    _Pharmacy.PharmacyStatus = Convert.ToString(dt.Rows[0]["sPharmacyStatus"]);
                                    //_Pharmacy.ContactStatus = Convert.ToInt64(dt.Rows[0]["nContactStatus"]);

                                    _Pharmacy.ContactFlag = PatientContactType.Pharmacy;
                                    _Pharmacy.ClinicID = 0;

                                    if (_Pharmacy.ContactId == Convert.ToInt64(cmbPAPharma.Tag))
                                    {
                                        _Pharmacy.ContactStatus = 1;
                                    }

                                    oPatientPharmacies.Add(_Pharmacy);
                                }
                                if (dt != null)
                                {
                                    dt.Dispose();
                                    dt = null;
                                }
                            }
                        }
                        //else
                        //{
                        //    oPatientPharmacies.Clear();
                        //}
                        #endregion
                    }

                    if (_isPrimaryCarePhysicianModified == true)
                    {
                        #region "Get Primary care Physician Details"

                        if (Convert.ToString(txtPAPrimaryCarePhy.Tag) != "")
                        {
                            DataTable dt = GetPatientDetails(Convert.ToInt64(txtPAPrimaryCarePhy.Tag), PatientContactType.PrimaryCarePhysician);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                //sName,sContact,sAddressLine1,sAddressLine2,sCity,sState,sZIP,sPhone,sFax,sEmail,sURL,sMobile,
                                //sPager,sNotes,sFirstName,sMiddleName,sLastName,sGender,sTaxonomy,sTaxonomyDesc,sTaxID,sUPIN,
                                //sNPI,sHospitalAffiliation,sExternalCode,sDegree,

                                oPrimaryCarePhysicians.Clear();

                                PatientDetail _Physician = new PatientDetail();

                                _Physician.PatientID = _PatientId;
                                _Physician.ContactId = Convert.ToInt64(txtPAPrimaryCarePhy.Tag);
                                _Physician.Name = Convert.ToString(dt.Rows[0]["sName"]);
                                _Physician.Contact = Convert.ToString(dt.Rows[0]["sContact"]);
                                _Physician.AddressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                                _Physician.AddressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                                _Physician.City = Convert.ToString(dt.Rows[0]["sCity"]);
                                _Physician.State = Convert.ToString(dt.Rows[0]["sState"]);
                                _Physician.ZIP = Convert.ToString(dt.Rows[0]["sZIP"]);
                                _Physician.Phone = Convert.ToString(dt.Rows[0]["sPhone"]);
                                _Physician.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                                _Physician.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                                _Physician.URL = Convert.ToString(dt.Rows[0]["sURL"]);
                                _Physician.Mobile = Convert.ToString(dt.Rows[0]["sMobile"]);
                                _Physician.Pager = Convert.ToString(dt.Rows[0]["sPager"]);
                                _Physician.Notes = Convert.ToString(dt.Rows[0]["sNotes"]);
                                _Physician.FirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                                _Physician.MiddleName = Convert.ToString(dt.Rows[0]["sMiddleName"]);
                                _Physician.LastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                                _Physician.Gender = Convert.ToString(dt.Rows[0]["sGender"]);
                                _Physician.Taxonomy = Convert.ToString(dt.Rows[0]["sTaxonomy"]);
                                _Physician.TaxonomyDesc = Convert.ToString(dt.Rows[0]["sTaxonomyDesc"]);
                                _Physician.TaxID = Convert.ToString(dt.Rows[0]["sTaxID"]);
                                _Physician.UPIN = Convert.ToString(dt.Rows[0]["sUPIN"]);
                                _Physician.NPI = Convert.ToString(dt.Rows[0]["sNPI"]);
                                _Physician.HospitalAffiliation = Convert.ToString(dt.Rows[0]["sHospitalAffiliation"]);
                                _Physician.ExternalCode = Convert.ToString(dt.Rows[0]["sExternalCode"]);
                                _Physician.Degree = Convert.ToString(dt.Rows[0]["sDegree"]);
                                _Physician.ContactFlag = PatientContactType.PrimaryCarePhysician;
                                _Physician.Prefix = Convert.ToString(dt.Rows[0]["sPrefix"]);
                                // Not Applicable for physician
                                _Physician.NCPDPID = "";
                                _Physician.ActiveStartTime = null;
                                _Physician.ActiveEndTime = null;
                                _Physician.ServiceLevel = "";
                                _Physician.PharmacyStatus = "";
                                //------------------------------

                                _Physician.ClinicID = 0;

                                oPrimaryCarePhysicians.Add(_Physician);
                            }
                            if (dt != null)
                            {
                                dt.Dispose();
                                dt = null;
                            }
                        }
                        else
                        {
                            oPrimaryCarePhysicians.Clear();
                        }

                        #endregion
                    }

                    if (_isRefferalsModified == true)
                    {
                        #region "Referrals"

                        oPatientReferrals.Clear();

                        //code start by nilesh on date 20101220 for case GLO2010-0005424
                        DataTable dtReff = null;
                        dtReff = (DataTable)cmbPAReferrals.DataSource;

                        //for (int i = 0; i < cmbPAReferrals.Items.Count; i++)
                        foreach (DataRow drRow in dtReff.Rows)
                        {
                            //cmbPAReferrals.SelectedIndex = i;
                            //DataTable dt = GetPatientDetails(Convert.ToInt64(cmbPAReferrals.SelectedValue), PatientContactType.Referral);
                            DataTable dt = GetPatientDetails(Convert.ToInt64(drRow[0].ToString()), PatientContactType.Referral);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                PatientDetail _Referral = new PatientDetail();

                                _Referral.PatientID = _PatientId;
                                //_Referral.ContactId = Convert.ToInt64(cmbPAReferrals.SelectedValue);
                                _Referral.ContactId = Convert.ToInt64(drRow[0].ToString());
                                //code end by nilesh on date 20101220 for case GLO2010-0005424
                                _Referral.Name = Convert.ToString(dt.Rows[0]["sName"]);
                                _Referral.Contact = Convert.ToString(dt.Rows[0]["sContact"]);
                                _Referral.AddressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                                _Referral.AddressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                                _Referral.City = Convert.ToString(dt.Rows[0]["sCity"]);
                                _Referral.State = Convert.ToString(dt.Rows[0]["sState"]);
                                _Referral.ZIP = Convert.ToString(dt.Rows[0]["sZIP"]);
                                _Referral.Phone = Convert.ToString(dt.Rows[0]["sPhone"]);
                                _Referral.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                                _Referral.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                                _Referral.URL = Convert.ToString(dt.Rows[0]["sURL"]);
                                _Referral.Mobile = Convert.ToString(dt.Rows[0]["sMobile"]);
                                _Referral.Pager = Convert.ToString(dt.Rows[0]["sPager"]);
                                _Referral.Notes = Convert.ToString(dt.Rows[0]["sNotes"]);
                                _Referral.FirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                                _Referral.MiddleName = Convert.ToString(dt.Rows[0]["sMiddleName"]);
                                _Referral.LastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                                _Referral.Gender = Convert.ToString(dt.Rows[0]["sGender"]);
                                _Referral.Taxonomy = Convert.ToString(dt.Rows[0]["sTaxonomy"]);
                                _Referral.TaxonomyDesc = Convert.ToString(dt.Rows[0]["sTaxonomyDesc"]);
                                _Referral.TaxID = Convert.ToString(dt.Rows[0]["sTaxID"]);
                                _Referral.UPIN = Convert.ToString(dt.Rows[0]["sUPIN"]);
                                _Referral.NPI = Convert.ToString(dt.Rows[0]["sNPI"]);
                                _Referral.HospitalAffiliation = Convert.ToString(dt.Rows[0]["sHospitalAffiliation"]);
                                _Referral.ExternalCode = Convert.ToString(dt.Rows[0]["sExternalCode"]);
                                _Referral.Degree = Convert.ToString(dt.Rows[0]["sDegree"]);
                                _Referral.Prefix = Convert.ToString(dt.Rows[0]["sPrefix"]);
                                // Not Applicable for Referral
                                _Referral.NCPDPID = "";
                                _Referral.ActiveStartTime = null;
                                _Referral.ActiveEndTime = null;
                                _Referral.ServiceLevel = "";
                                _Referral.PharmacyStatus = "";
                                //------------------------------

                                _Referral.ContactFlag = PatientContactType.Referral;
                                _Referral.ClinicID = 0;

                                _Referral.MUTransactionDate = Convert.ToDateTime(drRow["MuDate"].ToString());
                                _Referral.MUCheckBox = Convert.ToBoolean(drRow["MuCheckBox"].ToString());

                                oPatientReferrals.Add(_Referral);
                            }
                            if (dt != null)
                            {
                                dt.Dispose();
                                dt = null;
                            }
                        }
                        #endregion
                    }


                    if (_isCareTeamModified == true)
                    {
                        #region "Care Team"

                        oPatientCareTeam.Clear();

                        //code start by nilesh on date 20101220 for case GLO2010-0005424
                        DataTable dtReff = null;
                        dtReff = (DataTable)cmbPACareTeam.DataSource;

                        //for (int i = 0; i < cmbPAReferrals.Items.Count; i++)
                        foreach (DataRow drRow in dtReff.Rows)
                        {
                            //cmbPAReferrals.SelectedIndex = i;
                            //DataTable dt = GetPatientDetails(Convert.ToInt64(cmbPAReferrals.SelectedValue), PatientContactType.Referral);
                            DataTable dt = GetPatientDetails(Convert.ToInt64(drRow[0].ToString()), PatientContactType.CareTeam);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                PatientDetail _CareTeam = new PatientDetail();

                                _CareTeam.PatientID = _PatientId;
                                //_Referral.ContactId = Convert.ToInt64(cmbPAReferrals.SelectedValue);
                                _CareTeam.ContactId = Convert.ToInt64(drRow[0].ToString());
                                //code end by nilesh on date 20101220 for case GLO2010-0005424
                                _CareTeam.Name = Convert.ToString(dt.Rows[0]["sName"]);
                                _CareTeam.Contact = Convert.ToString(dt.Rows[0]["sContact"]);
                                _CareTeam.AddressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                                _CareTeam.AddressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                                _CareTeam.City = Convert.ToString(dt.Rows[0]["sCity"]);
                                _CareTeam.State = Convert.ToString(dt.Rows[0]["sState"]);
                                _CareTeam.ZIP = Convert.ToString(dt.Rows[0]["sZIP"]);
                                _CareTeam.Phone = Convert.ToString(dt.Rows[0]["sPhone"]);
                                _CareTeam.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                                _CareTeam.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                                _CareTeam.URL = Convert.ToString(dt.Rows[0]["sURL"]);
                                _CareTeam.Mobile = Convert.ToString(dt.Rows[0]["sMobile"]);
                                _CareTeam.Pager = Convert.ToString(dt.Rows[0]["sPager"]);
                                _CareTeam.Notes = Convert.ToString(dt.Rows[0]["sNotes"]);
                                _CareTeam.FirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                                _CareTeam.MiddleName = Convert.ToString(dt.Rows[0]["sMiddleName"]);
                                _CareTeam.LastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                                _CareTeam.Gender = Convert.ToString(dt.Rows[0]["sGender"]);
                                _CareTeam.Taxonomy = Convert.ToString(dt.Rows[0]["sTaxonomy"]);
                                _CareTeam.TaxonomyDesc = Convert.ToString(dt.Rows[0]["sTaxonomyDesc"]);
                                _CareTeam.TaxID = Convert.ToString(dt.Rows[0]["sTaxID"]);
                                _CareTeam.UPIN = Convert.ToString(dt.Rows[0]["sUPIN"]);
                                _CareTeam.NPI = Convert.ToString(dt.Rows[0]["sNPI"]);
                                _CareTeam.HospitalAffiliation = Convert.ToString(dt.Rows[0]["sHospitalAffiliation"]);
                                _CareTeam.ExternalCode = Convert.ToString(dt.Rows[0]["sExternalCode"]);
                                _CareTeam.Degree = Convert.ToString(dt.Rows[0]["sDegree"]);
                                _CareTeam.Prefix = Convert.ToString(dt.Rows[0]["sPrefix"]);
                                // Not Applicable for Referral
                                _CareTeam.NCPDPID = "";
                                _CareTeam.ActiveStartTime = null;
                                _CareTeam.ActiveEndTime = null;
                                _CareTeam.ServiceLevel = "";
                                _CareTeam.PharmacyStatus = "";
                                //------------------------------

                                _CareTeam.ContactFlag = PatientContactType.Referral;
                                _CareTeam.ClinicID = 0;

                                oPatientCareTeam.Add(_CareTeam);
                            }
                            if (dt != null)
                            {
                                dt.Dispose();
                                dt = null;
                            }
                        }
                        #endregion
                    }





















                    oPatientDemo.PatientID = _PatientId;

                    oPatientDemo.SetToCollection = chkSetToCollection.Checked;
                    oPatientDemo.InsuranceNotes = txtPAInsuranceNotes.Text.Trim();

                    //-------------------------------
                    // SignatureOnFile this field was on Other Details but moved on demographic control 
                    // But Object is not changes so Property is available in Other Info object  

                    _PatientDemographicOtherInfo.SOF = chkSignatureOnFile.Checked;

                    //-------------------------------

                    _IsAuditLogGetData = true;

                    //Added by SaiKrishna:2011-06-27(yyyy-mm-dd) for Patient Account Feature
                    if (_IsPatientAccountFeature == true)
                    {

                        if (rbAccountNew.Checked == true && rbAccountNew.Visible == true)
                        {
                            if (oAccount == null) { oAccount = new global::gloPatient.Account(); }
                            oAccount.PAccountID = 0;
                            oAccount.AccountNo = txtAccountNo.Text;
                            oAccount.AccountDesc = txtAccountDescription.Text;

                            if (cmbBusinessCenter.SelectedIndex != -1)
                            {
                                oAccount.nBusinessCenterID = Convert.ToInt64(cmbBusinessCenter.SelectedValue);

                            }

                            oAccount.SentToCollection = chkSetToCollection.Checked;
                            oAccount.ExcludeStatement = chkExcludefromStatement.Checked;
                            oAccount.AccountClosedDate = DateTime.MinValue;
                            oAccount.RecordDate = DateTime.Now;
                            oAccount.GuarantorID = 0;
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {

                                    //Refill Address to guarantor when guarantor is sameas patient.
                                    if (oPatientGuarantors[gIndex].OtherConatctType.GetHashCode() == PatientOtherContactType.SameAsPatient.GetHashCode())
                                    {
                                        if (mtxtPADOB.MaskCompleted == true)
                                            oPatientGuarantors[gIndex].DOB = Convert.ToDateTime(mtxtPADOB.Text);
                                        if (txtmPASSN.IsValidated == true)
                                            oPatientGuarantors[gIndex].SSN = txtmPASSN.Text.Trim();
                                        else
                                            oPatientGuarantors[gIndex].SSN = "";

                                        oPatientGuarantors[gIndex].Gender = cmbGender.Text.Trim();
                                        oPatientGuarantors[gIndex].AddressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                                        oPatientGuarantors[gIndex].AddressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                                        oPatientGuarantors[gIndex].City = oAddresscontrol.txtCity.Text.Trim();
                                        oPatientGuarantors[gIndex].County = oAddresscontrol.txtCounty.Text.Trim();
                                        oPatientGuarantors[gIndex].Zip = oAddresscontrol.txtZip.Text.Trim();
                                        oPatientGuarantors[gIndex].State = oAddresscontrol.cmbState.Text.Trim();
                                        oPatientGuarantors[gIndex].Country = oAddresscontrol.cmbCountry.Text;
                                        oPatientGuarantors[gIndex].County = oAddresscontrol.txtCounty.Text.Trim();
                                        //Sanjog - Added on 2011 Aug 16 to assign phone value for guarantor
                                        oPatientGuarantors[gIndex].Phone = mtxtPAPhone.Text;
                                        //Sanjog - Added on 2011 Aug 16 to assign phone value for guarantor
                                    }

                                    //assign account guarantor.
                                    if (oPatientGuarantors[gIndex].IsAccountGuarantor == true)
                                    {
                                        //validation for selected guarantor address
                                        //if (oPatientGuarantors[gIndex].AddressLine1.ToString().Trim() == "")
                                        //{
                                        //    MessageBox.Show("Enter address for selected guarantor", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return false;
                                        //}
                                        //if (oPatientGuarantors[gIndex].City.ToString().Trim() == "")
                                        //{
                                        //    MessageBox.Show("Enter city for selected guarantor", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return false;
                                        //}

                                        //if (oPatientGuarantors[gIndex].State.ToString().Trim() == "")
                                        //{
                                        //    MessageBox.Show("Enter state for selected guarantor", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return false;
                                        //}

                                        //if (oPatientGuarantors[gIndex].Zip.ToString().Trim() == "")
                                        //{
                                        //    MessageBox.Show("Enter zip for selected guarantor", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return false;
                                        //}
                                        oAccount.FirstName = oPatientGuarantors[gIndex].FirstName.ToString().Trim();
                                        oAccount.LastName = oPatientGuarantors[gIndex].LastName.ToString().Trim();
                                        oAccount.MiddleName = oPatientGuarantors[gIndex].MiddleName.ToString().Trim();
                                        oAccount.AddressLine1 = oPatientGuarantors[gIndex].AddressLine1.ToString().Trim();
                                        oAccount.AddressLine2 = oPatientGuarantors[gIndex].AddressLine2.ToString().Trim();
                                        oAccount.Active = true;
                                        oAccount.City = oPatientGuarantors[gIndex].City.ToString().Trim();
                                        oAccount.Zip = oPatientGuarantors[gIndex].Zip.ToString().Trim();
                                        oAccount.State = oPatientGuarantors[gIndex].State.ToString().Trim();
                                        oAccount.Country = oPatientGuarantors[gIndex].Country.ToString().Trim();
                                        oAccount.County = oPatientGuarantors[gIndex].County.ToString().Trim();
                                        oAccount.ClinicID = _ClinicID;
                                        oAccount.MachineName = System.Environment.MachineName;
                                        oAccount.GuarantorCode = "";
                                        oAccount.AreaCode = "";
                                        oAccount.UserID = _UserID;
                                        oAccount.EntityType = oPatientGuarantors[gIndex].GurantorType.GetHashCode();
                                        oAccount.SiteID = 1;
                                        //Indicates that new account
                                        oAccount.IsExistingAccount = false;
                                        break;

                                    }
                                }
                            }

                            oPatientAccount.AccountPatientID = 0;
                            oPatientAccount.PatientID = _PatientId;
                            oPatientAccount.AccountNo = txtAccountNo.Text.Trim();
                            oPatientAccount.PatientCode = oPatientDemo.PatientCode;
                            oPatientAccount.AccountClosedDate = DateTime.MinValue;
                            oPatientAccount.ClinicID = _ClinicID;
                            oPatientAccount.SiteID = 1;
                            oPatientAccount.UserID = _UserID;
                            oPatientAccount.MachineName = System.Environment.MachineName;
                            oPatientAccount.RecordDate = DateTime.Now;
                            oPatientAccount.Active = true;
                            oPatientAccount.OwnAccount = true;
                        }
                        else if (rbAccountExisting.Checked == true && rbAccountExisting.Visible == true)
                        {
                            DataTable dt = objgloAccount.GetAccountDetailsById(Convert.ToInt64(txtAccountNo.Tag.ToString()));

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (oAccount == null) { oAccount = new global::gloPatient.Account(); }
                                oAccount.PAccountID = Convert.ToInt64(dt.Rows[0]["nPAccountID"].ToString());
                                oAccount.IsExistingAccount = true;
                            }
                            oPatientAccount.AccountPatientID = 0;
                            oPatientAccount.PatientID = _PatientId;
                            oPatientAccount.AccountNo = txtAccountNo.Text.Trim();
                            oPatientAccount.PatientCode = oPatientDemo.PatientCode;
                            oPatientAccount.AccountClosedDate = DateTime.MinValue;
                            oPatientAccount.ClinicID = _ClinicID;
                            oPatientAccount.SiteID = 1;
                            oPatientAccount.UserID = _UserID;
                            oPatientAccount.MachineName = System.Environment.MachineName;
                            oPatientAccount.RecordDate = DateTime.Now;
                            oPatientAccount.Active = true;
                            oPatientAccount.OwnAccount = false;
                            if (dt != null)
                            {
                                dt.Dispose();
                                dt = null;
                            }
                        }
                        else
                        {
                            oAccount = null;
                        }

                    }
                    else
                    {
                        if (_PatientId == 0)
                        {
                            GetAccountDataForAccountFeatureDisabled();
                        }
                        else
                        {
                            //get Account details
                            DataTable dt = objgloAccount.GetAccountDetailsById(nPAccountId);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                oAccount.PAccountID = Convert.ToInt64(dt.Rows[0]["nPAccountID"].ToString());
                                oAccount.AccountNo = dt.Rows[0]["sAccountNo"].ToString();
                                oAccount.AccountDesc = dt.Rows[0]["sAccountDesc"].ToString();

                                if (cmbBusinessCenter.SelectedIndex != -1)
                                {
                                    oAccount.nBusinessCenterID = Convert.ToInt64(cmbBusinessCenter.SelectedValue);

                                }

                                oAccount.GuarantorCode = dt.Rows[0]["sGuarantorCode"].ToString();
                                oAccount.GuarantorID = Convert.ToInt64(dt.Rows[0]["nGuarantorID"].ToString());

                                if (dt.Rows[0]["dtAccountClosedDate"] != null && dt.Rows[0]["dtAccountClosedDate"].ToString() != "")
                                {
                                    oAccount.AccountClosedDate = Convert.ToDateTime(dt.Rows[0]["dtAccountClosedDate"].ToString());
                                }
                                else
                                {
                                    oAccount.AccountClosedDate = DateTime.MinValue;
                                }

                                if (dt.Rows[0]["bIsSentToCollection"] != DBNull.Value && Convert.ToString(dt.Rows[0]["bIsSentToCollection"]).Trim() != "")
                                { oAccount.SentToCollection = Convert.ToBoolean(dt.Rows[0]["bIsSentToCollection"].ToString()); }
                                else
                                { oAccount.SentToCollection = false; }

                                if (dt.Rows[0]["bIsExcludeStatement"] != DBNull.Value && Convert.ToString(dt.Rows[0]["bIsExcludeStatement"]).Trim() != "")
                                { oAccount.ExcludeStatement = Convert.ToBoolean(dt.Rows[0]["bIsExcludeStatement"]); }
                                else
                                { oAccount.ExcludeStatement = false; }

                                oAccount.RecordDate = Convert.ToDateTime(dt.Rows[0]["dtRecordDate"].ToString());
                                oAccount.IsAccountFeatureEnabled = true;
                                oAccount.FirstName = dt.Rows[0]["sFirstName"].ToString();
                                oAccount.LastName = dt.Rows[0]["sLastName"].ToString();
                                oAccount.MiddleName = dt.Rows[0]["sMiddleName"].ToString();
                                oAccount.AddressLine1 = dt.Rows[0]["sAddressLine1"].ToString();
                                oAccount.AddressLine2 = dt.Rows[0]["sAddressLine2"].ToString();
                                oAccount.Active = Convert.ToBoolean(dt.Rows[0]["bIsActive"].ToString());
                                oAccount.City = dt.Rows[0]["sCity"].ToString();
                                oAccount.Zip = dt.Rows[0]["sZip"].ToString();
                                oAccount.State = dt.Rows[0]["sState"].ToString();
                                oAccount.Country = dt.Rows[0]["sCountry"].ToString();
                                oAccount.County = dt.Rows[0]["sCounty"].ToString();
                                oAccount.ClinicID = Convert.ToInt64(dt.Rows[0]["nClinicID"].ToString());
                                oAccount.MachineName = dt.Rows[0]["sMachineName"].ToString();
                                oAccount.AreaCode = dt.Rows[0]["sAreaCode"].ToString();
                                oAccount.UserID = Convert.ToInt64(dt.Rows[0]["nUserID"].ToString());
                                oAccount.EntityType = Convert.ToInt64(dt.Rows[0]["nEntityType"].ToString());
                                oAccount.SiteID = Convert.ToInt64(dt.Rows[0]["nSiteID"].ToString());

                                //Check Account data modified or not
                                bool _Result = true;
                                if (_nGuarantorId == oPatientGuarantors[0].PatientContactID
                                     && chkExcludefromStatement.Checked == oAccount.ExcludeStatement
                                     && chkSetToCollection.Checked == oAccount.SentToCollection
                                    )
                                {
                                    _Result = false;
                                }
                                oAccount.IsDataModified = _Result;
                                oAccount.ExcludeStatement = chkExcludefromStatement.Checked;
                                oAccount.SentToCollection = chkSetToCollection.Checked;

                                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                                {
                                    //assign changed guarantor details to account
                                    if (_nGuarantorId != oPatientGuarantors[0].PatientContactID)
                                    {
                                        for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                        {
                                            if (oPatientGuarantors[gIndex].IsAccountGuarantor == true)
                                            {
                                                oAccount.FirstName = oPatientGuarantors[gIndex].FirstName.ToString().Trim();
                                                oAccount.LastName = oPatientGuarantors[gIndex].LastName.ToString().Trim();
                                                oAccount.MiddleName = oPatientGuarantors[gIndex].MiddleName.ToString().Trim();
                                                oAccount.AddressLine1 = oPatientGuarantors[gIndex].AddressLine1.ToString().Trim();
                                                oAccount.AddressLine2 = oPatientGuarantors[gIndex].AddressLine2.ToString().Trim();
                                                oAccount.Active = true;
                                                oAccount.City = oPatientGuarantors[gIndex].City.ToString().Trim();
                                                oAccount.Zip = oPatientGuarantors[gIndex].Zip.ToString().Trim();
                                                oAccount.State = oPatientGuarantors[gIndex].State.ToString().Trim();
                                                oAccount.Country = oPatientGuarantors[gIndex].Country.ToString().Trim();
                                                oAccount.County = oPatientGuarantors[gIndex].County.ToString().Trim();
                                            }
                                        }
                                    }
                                }
                            }
                            if (dt != null)
                            {
                                dt.Dispose();
                                dt = null;
                            }
                        }

                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                _bValidatePortalInvitationEmail = true;
                _bValidateAPIInvitationEmail = true;
            }
        }

        //set demographic data
        public bool SetData()
        {
            try
            {
                // Patient code , name
                if (_PatientId == 0)
                {
                    GeneratePatientCode();
                }
                txtPACode.Text = oPatientDemo.PatientCode;
                txtPAFname.Text = oPatientDemo.PatientFirstName;
                txtPAMName.Text = oPatientDemo.PatientMiddleName;
                txtPALName.Text = oPatientDemo.PatientLastName;
                txtPatSuffix.Text = oPatientDemo.PatientSuffix;
                txtPAInsuranceNotes.Text = oPatientDemo.InsuranceNotes;
                // SSN
                if (oPatientDemo.PatientSSN != "")
                    txtmPASSN.Text = oPatientDemo.PatientSSN.ToString();

                //DOB
                //dtpPADOB.Value = oPatientDemo.PatientDOB;
                //dtpPADOB.Checked = true;  

                mtxtPADOB.Text = oPatientDemo.PatientDOB.ToString("MM/dd/yyyy");

                //dtpBirthTime Settings
                string sBirthTime = oPatientDemo.BirthTime.ToString();
                if ((sBirthTime != " ") && (sBirthTime != ""))
                {
                    dtpBirth.Text = sBirthTime;
                    dtpBirth.Checked = true;
                }
                else
                {
                    dtpBirth.Text = "00:00:00";
                    dtpBirth.Checked = false;
                }

                //Gender
                //code added by dipak to change patient gender radiobuttons to combo list
                cmbGender.Text = oPatientDemo.PatientGender;
                //code commneted by dipak 
                //if (oPatientDemo.PatientGender == "Male")
                //    rbGender1.Checked = true;
                //else if (oPatientDemo.PatientGender.ToString() == "Female")
                //    rbGender2.Checked = true;
                //else 
                //    rbGender3.Checked = true;
                //end code changes by dipak

                //MaritalStatus ;
                if (oPatientDemo.PatientMaritalStatus != "")
                    cmbPAMarital.SelectedItem = oPatientDemo.PatientMaritalStatus;

                //Address
                //txtPAAddress1.Text = oPatientDemo.PatientAddress1.ToString();
                //txtPAAddress2.Text = oPatientDemo.PatientAddress2.ToString();
                //txtPAZip.Text = oPatientDemo.PatientZip.Trim();
                //txtPACity.Text = oPatientDemo.PatientCity.Trim();
                //txtPACounty.Text = oPatientDemo.PatientCounty.Trim();
                //cmbPAState.Text = oPatientDemo.PatientState.ToString();
                //cmbPACountry.Text = oPatientDemo.PatientCountry.Trim();

                //Sandip Darade 20091008
                //Above code for address replaced by code below as we now use addrescontrol 
                oAddresscontrol.txtAddress1.Text = oPatientDemo.PatientAddress1.ToString();
                oAddresscontrol.txtAddress2.Text = oPatientDemo.PatientAddress2.ToString();
                oAddresscontrol.isFormLoading = true;
                oAddresscontrol.txtZip.Text = oPatientDemo.PatientZip;
                oAddresscontrol.isFormLoading = false;
                oAddresscontrol.txtCity.Text = oPatientDemo.PatientCity;
                //oAddresscontrol.cmbCountry.Text = oPatientDemo.PatientCountry;

                //oAddresscontrol.cmbCountry.SelectedText = oPatientDemo.PatientCountry;
                try
                { oAddresscontrol.cmbCountry.SelectedIndex = oAddresscontrol.cmbCountry.FindString(oPatientDemo.PatientCountry, -1); }
                catch
                { oAddresscontrol.cmbCountry.SelectedIndex = 0; }


                oAddresscontrol.cmbState.Text = oPatientDemo.PatientState.ToString();
                oAddresscontrol.txtCounty.Text = oPatientDemo.PatientCounty;
                oPatientDemo.PatientCountry = oAddresscontrol.cmbCountry.Text;

                //7022Items: Home Billing
                //set Area Code from database to txtAreaCode.
                oAddresscontrol.txtAreaCode.Text = oPatientDemo.AreaCode.ToString();

                //Contact
                mtxtPAPhone.Text = oPatientDemo.PatientPhone.Replace(") ", ")").Replace("+1", "+");
              //  mtxtPAPhone.Text = "tel:+1(234)567-8900";
                mtxtPAMobile.Text = oPatientDemo.PatientMobile.Trim();
                txtPAEmail.Text = oPatientDemo.PatientEmail.Trim();
                txtPAFax.Text = oPatientDemo.PatientFax.Trim();

                //Emergency Contact
                txtEmergencyContact.Text = oPatientDemo.EmergencyContact.Trim();
                mtxtEmergencyPhone.Text = oPatientDemo.EmergencyPhone.Trim();
                mtxtEmergencyMobile.Text = oPatientDemo.EmergencyMobile.Trim();
                //Added by Anil on 20090713
                if (oPatientDemo.EmergencyRelationshipCode.Trim() != "")
                {
                    cmbRelationship.SelectedValue = oPatientDemo.EmergencyRelationshipCode.Trim();

                }

                //oPatientDemo.PatientLocation = cmbPALocation.Text;

                //Dhruv 20100104 
                //it is used to check wheather the Location is defined or not.
                //location 
                if (oPatientDemo.PatientLocation.Trim() != "")
                {
                    cmbPALocation.Text = oPatientDemo.PatientLocation.Trim();
                }
                else
                {
                    oPatientDemo.PatientLocation = cmbPALocation.Text;
                }

                //Gurantor
                //txtPAGuarantor.Text = oPatientDemo.PatientGuarantor.Trim();
                //txtPAGuarantor.Tag = Convert.ToInt64(oPatientDemo.PatientGuarantorID);

                //patientAccounts
                #region "patientAccounts"

                if (oPatientAccounts != null && oPatientAccounts.Count > 0)
                {
                    DataTable dtPatientAccounts = new DataTable();
                    DataColumn dtAccountId = new DataColumn("AccountId");
                    DataColumn dtAccountNo = new DataColumn("AccountNo");
                    dtPatientAccounts.Columns.Add(dtAccountId);
                    dtPatientAccounts.Columns.Add(dtAccountNo);
                    for (int i = 0; i < oPatientAccounts.Count; i++)
                    {
                        DataRow drTemp = dtPatientAccounts.NewRow();
                        drTemp["AccountId"] = oPatientAccounts[i].PAccountID;

                        if (oPatientAccounts[i].OwnAccount == false)
                            drTemp["AccountNo"] = "[" + oPatientAccounts[i].AccountNo + "]";
                        else
                            drTemp["AccountNo"] = oPatientAccounts[i].AccountNo;

                        dtPatientAccounts.Rows.Add(drTemp);
                    }

                    cmbAccounts.Items.Clear();
                    cmbAccounts.DisplayMember = dtPatientAccounts.Columns["AccountNo"].ColumnName;
                    cmbAccounts.ValueMember = dtPatientAccounts.Columns["AccountId"].ColumnName;
                    cmbAccounts.DataSource = dtPatientAccounts;

                }
                #endregion


                ////Pharmacy 
                //txtPAPharma.Tag = Convert.ToInt64(oPatientDemo.PatientPharmacyID);
                //txtPAPharma.Text = oPatientDemo.PharmacyName.ToString();

                ////Primary Care Physician  
                //txtPAPrimaryCarePhy.Text = oPatientDemo.PrimaryCarePhysicianName.ToString();
                //txtPAPrimaryCarePhy.Tag = Convert.ToInt64(oPatientDemo.PatientPCPId);

                ////Referral
                //DataTable dt = new DataTable();
                //DataColumn dc1 = new DataColumn("ReferralID");
                //DataColumn dc2 = new DataColumn("ReferralName");
                //dt.Columns.Add(dc1);
                //dt.Columns.Add(dc2);
                //for (int i = 0; i < oPatientDemo.Referrals.Count; i++)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["ReferralID"] = oPatientDemo.Referrals[i].ReferralID;
                //    dr["ReferralName"] = oPatientDemo.Referrals[i].Name;
                //    dt.Rows.Add(dr);
                //}
                //cmbPAReferrals.DataSource = dt;
                //cmbPAReferrals.ValueMember = dt.Columns[0].ColumnName;
                //cmbPAReferrals.DisplayMember = dt.Columns[1].ColumnName;


                //Pharmacy 
                if (oPatientPharmacies != null && oPatientPharmacies.Count > 0)
                {
                    //shubhangi
                    //txtPAPharma.Tag = oPatientPharmacies[0].ContactId;
                    //txtPAPharma.Text = oPatientPharmacies[0].Name;  
                    DataTable dt = new DataTable();
                    DataColumn dc1 = new DataColumn("ContactID");
                    DataColumn dc2 = new DataColumn("Pharmacy Name");
                    dt.Columns.Add(dc1);
                    dt.Columns.Add(dc2);
                    for (int i = 0; i < oPatientPharmacies.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ContactID"] = oPatientPharmacies[i].ContactId;
                        dr["Pharmacy Name"] = oPatientPharmacies[i].Name;
                        dt.Rows.Add(dr);
                    }
                    cmbPAPharma.DataSource = dt;
                    cmbPAPharma.ValueMember = dt.Columns[0].ColumnName;
                    cmbPAPharma.DisplayMember = dt.Columns[1].ColumnName;
                    for (int i = 0; i < oPatientPharmacies.Count; i++)
                    {
                        if (oPatientPharmacies[i].ContactStatus != 0)
                            cmbPAPharma.SelectedValue = oPatientPharmacies[i].ContactId;
                    }
                }
                if (cmbPAPharma.SelectedIndex != -1)
                {
                    cmbPAPharma.Tag = Convert.ToInt64(cmbPAPharma.SelectedValue);
                }

                //Primary Care Physician  

                if (oPrimaryCarePhysicians != null && oPrimaryCarePhysicians.Count > 0)
                {
                    //txtPAPrimaryCarePhy.Text = oPrimaryCarePhysicians[0].FirstName + " " + oPrimaryCarePhysicians[0].MiddleName + " " + oPrimaryCarePhysicians[0].LastName;
                    //ADDED TO DISPLAY PREFIX & SUFFIX IF THEY PRESENT 20100610
                    if (oPrimaryCarePhysicians[0].Prefix != "" && oPrimaryCarePhysicians[0].Degree != "")
                    {
                        txtPAPrimaryCarePhy.Text = oPrimaryCarePhysicians[0].Prefix + " " + oPrimaryCarePhysicians[0].FirstName + " " + oPrimaryCarePhysicians[0].MiddleName + " " + oPrimaryCarePhysicians[0].LastName + " " + oPrimaryCarePhysicians[0].Degree;
                    }
                    else if (oPrimaryCarePhysicians[0].Prefix == "" && oPrimaryCarePhysicians[0].Degree != "")
                    {
                        txtPAPrimaryCarePhy.Text = oPrimaryCarePhysicians[0].FirstName + " " + oPrimaryCarePhysicians[0].MiddleName + " " + oPrimaryCarePhysicians[0].LastName + " " + oPrimaryCarePhysicians[0].Degree;
                    }
                    else if (oPrimaryCarePhysicians[0].Prefix != "" && oPrimaryCarePhysicians[0].Degree == "")
                    {
                        txtPAPrimaryCarePhy.Text = oPrimaryCarePhysicians[0].Prefix + " " + oPrimaryCarePhysicians[0].FirstName + " " + oPrimaryCarePhysicians[0].MiddleName + " " + oPrimaryCarePhysicians[0].LastName;
                    }
                    else
                    {
                        txtPAPrimaryCarePhy.Text = oPrimaryCarePhysicians[0].FirstName + " " + oPrimaryCarePhysicians[0].MiddleName + " " + oPrimaryCarePhysicians[0].LastName;
                    }
                    txtPAPrimaryCarePhy.Tag = oPrimaryCarePhysicians[0].ContactId;

                }
                //Referral
                if (oPatientReferrals != null)
                {
                DataTable dt = new DataTable();
                  
                DataColumn dcId = new DataColumn("ID", typeof(long));
                DataColumn dcDescription = new DataColumn("Description", typeof(string));
                DataColumn dcMuDate = new DataColumn("MuDate", typeof(DateTime));
                DataColumn dcMuCheckBox = new DataColumn("MuCheckBox", typeof(bool));
             
                dt.Columns.Add(dcId);
                dt.Columns.Add(dcDescription);
                dt.Columns.Add(dcMuDate);
                dt.Columns.Add(dcMuCheckBox);          



                    for (int i = 0; i < oPatientReferrals.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID"] = oPatientReferrals[i].ContactId;
                        // dr["ReferralName"] = oPatientReferrals[i].Prefix + " " + oPatientReferrals[i].FirstName + " " + oPatientReferrals[i].MiddleName + " " + oPatientReferrals[i].LastName + " " + oPatientReferrals[i].Degree;
                        //ADDED TO DISPLAY PREFIX & SUFFIX IF THEY PRESENT 20100610
                        if (oPatientReferrals[i].Prefix != "" && oPatientReferrals[i].Degree != "")
                        {
                            dr["Description"] = oPatientReferrals[i].Prefix + " " + oPatientReferrals[i].FirstName + " " + oPatientReferrals[i].MiddleName + " " + oPatientReferrals[i].LastName + " " + oPatientReferrals[i].Degree;
                        }
                        else if (oPatientReferrals[i].Prefix == "" && oPatientReferrals[i].Degree != "")
                        {
                            dr["Description"] = oPatientReferrals[i].FirstName + " " + oPatientReferrals[i].MiddleName + " " + oPatientReferrals[i].LastName + " " + oPatientReferrals[i].Degree;
                        }
                        else if (oPatientReferrals[i].Prefix != "" && oPatientReferrals[i].Degree == "")
                        {
                            dr["Description"] = oPatientReferrals[i].Prefix + " " + oPatientReferrals[i].FirstName + " " + oPatientReferrals[i].MiddleName + " " + oPatientReferrals[i].LastName;
                        }
                        else
                        {
                            dr["Description"] = oPatientReferrals[i].FirstName + " " + oPatientReferrals[i].MiddleName + " " + oPatientReferrals[i].LastName;
                        }

                        if (oPatientReferrals[i].MUTransactionDate != null)
                        {
                            dr["MuDate"] = oPatientReferrals[i].MUTransactionDate;
                        }
                        else
                        {
                            dr["MuDate"] = System.DateTime.Now;
                        }


                        dr["MuCheckBox"] = oPatientReferrals[i].MUCheckBox;

                        //Added on 20100804-To fix Issue:#3328-Application showing error message 'system.data.constrainexception'
                        // if (dt.Rows.Contains(oPatientReferrals[i].ContactId) == false)
                        //{

                        dt.Rows.Add(dr);
                        //}

                    }

                    cmbPAReferrals.DataSource = dt;
                    cmbPAReferrals.ValueMember = dt.Columns[0].ColumnName;
                    cmbPAReferrals.DisplayMember = dt.Columns[1].ColumnName;
                }
                //Care Team
                if (oPatientCareTeam != null)
                {
                    DataTable dt = new DataTable();
                    DataColumn dc1 = new DataColumn("ContactID");


                    DataColumn dc2 = new DataColumn("ReferralName");
                    dt.Columns.Add(dc1);
                    dt.Columns.Add(dc2);
                    //DataColumn[] pk5 = new DataColumn[] { dt.Columns[0] };
                    //dt.PrimaryKey = pk5;

                    for (int i = 0; i < oPatientCareTeam.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ContactID"] = oPatientCareTeam[i].ContactId;
                        // dr["ReferralName"] = oPatientCareTeam[i].Prefix + " " + oPatientCareTeam[i].FirstName + " " + oPatientCareTeam[i].MiddleName + " " + oPatientCareTeam[i].LastName + " " + oPatientCareTeam[i].Degree;
                        //ADDED TO DISPLAY PREFIX & SUFFIX IF THEY PRESENT 20100610
                        if (oPatientCareTeam[i].Prefix != "" && oPatientCareTeam[i].Degree != "")
                        {
                            dr["ReferralName"] = oPatientCareTeam[i].Prefix + " " + oPatientCareTeam[i].FirstName + " " + oPatientCareTeam[i].MiddleName + " " + oPatientCareTeam[i].LastName + " " + oPatientCareTeam[i].Degree;
                        }
                        else if (oPatientCareTeam[i].Prefix == "" && oPatientCareTeam[i].Degree != "")
                        {
                            dr["ReferralName"] = oPatientCareTeam[i].FirstName + " " + oPatientCareTeam[i].MiddleName + " " + oPatientCareTeam[i].LastName + " " + oPatientCareTeam[i].Degree;
                        }
                        else if (oPatientCareTeam[i].Prefix != "" && oPatientCareTeam[i].Degree == "")
                        {
                            dr["ReferralName"] = oPatientCareTeam[i].Prefix + " " + oPatientCareTeam[i].FirstName + " " + oPatientCareTeam[i].MiddleName + " " + oPatientCareTeam[i].LastName;
                        }
                        else
                        {
                            dr["ReferralName"] = oPatientCareTeam[i].FirstName + " " + oPatientCareTeam[i].MiddleName + " " + oPatientCareTeam[i].LastName;
                        }
                        //Added on 20100804-To fix Issue:#3328-Application showing error message 'system.data.constrainexception'
                        // if (dt.Rows.Contains(oPatientCareTeam[i].ContactId) == false)
                        //{
                        dt.Rows.Add(dr);
                        //}

                    }

                    cmbPACareTeam.DataSource = dt;
                    cmbPACareTeam.ValueMember = dt.Columns[0].ColumnName;
                    cmbPACareTeam.DisplayMember = dt.Columns[1].ColumnName;
                }

                //Race
                if (oPatientDemo.PatientRace.Trim() != "")
                {

                    if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    {
                        if (oPatientDemo.PatientRace.Trim() == "" || oPatientDemo.PatientRace.Trim() == "Declined to specify")
                        {
                            cmbPARace.Text = oPatientDemo.PatientRace.Trim();
                        }
                        else
                        {
                            DataTable dtRace = new DataTable();
                            dtRace = GetRaceLoad(oPatientDemo.PatientRace);
                            cmbPARace.DataSource = dtRace;
                            cmbPARace.ValueMember = "nCategoryID";
                            cmbPARace.DisplayMember = "sDescription";
                        }
                    }
                    else
                    {
                        cmbPARace.Text = oPatientDemo.PatientRace.Trim();
                    }
                }

                //Communcication Preference 
                if (oPatientDemo.PatientCommunicationPrefence.Trim() != "")
                    cmbCommPref.Text = oPatientDemo.PatientCommunicationPrefence.Trim();

                chkYesNoLab.Checked = oPatientDemo.IsYesNoLab; //'YES/No Labs

                if (oPatientDemo.PatientEthnicities.Trim() != "")
                {
                    if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    {
                        if (oPatientDemo.PatientEthnicities.Trim() == "" || oPatientDemo.PatientEthnicities.Trim() == "Declined to specify")
                        {
                            cmbPAEthn.Text = oPatientDemo.PatientEthnicities.Trim();
                        }
                        else
                        {
                            DataTable dtEthinicity = new DataTable();
                            dtEthinicity = GetEthnicityLoad(oPatientDemo.PatientEthnicities);
                            cmbPAEthn.DataSource = dtEthinicity;
                            cmbPAEthn.ValueMember = "id";
                            cmbPAEthn.DisplayMember = "name";
                        }
                    }
                    else
                    {
                        cmbPAEthn.Text = oPatientDemo.PatientEthnicities.Trim();
                    }
                }

                //if (oPatientDemo.PatientEthnicities.Trim() != "")
                //    cmbPAEthn.Text = oPatientDemo.PatientEthnicities.Trim();
                //if (oPatientDemo.PatientLanguage.Trim() != "")
                    cmbPALang.Text = oPatientDemo.PatientLanguage.Trim();

                txtPatientPrefix.Text = oPatientDemo.PatientPrefix.Trim();
                //Hand Domain

                if (oPatientDemo.PatientHandDominance.Trim() != "")
                    cmbPAHandDom.DropDownStyle = ComboBoxStyle.Simple;
                //cmbPAHandDom.Text = oPatient.DemographicsDetail.PatientHandDominance;
                cmbPAHandDom.Text = oPatientDemo.PatientHandDominance.Trim();
                // cmbPAHandDom.
                cmbPAHandDom.DropDownStyle = ComboBoxStyle.DropDownList;

                //Start/Commented Code
                ////Photo
                //    if (oPatientDemo.PatientPhoto != null)
                //    {
                //        picPAPhoto.BackgroundImageLayout = ImageLayout.Stretch;
                //        //picPAPhoto.SizeMode = PictureBoxSizeMode.CenterImage;
                //        int picOutputwidth = picPAPhoto.Width;
                //        int picOutputheight = picPAPhoto.Height;
                //        int OutputWidth = oPatientDemo.PatientPhoto.Width;
                //        int OutputHeight = oPatientDemo.PatientPhoto.Height;
                //        double myPicWidth = picOutputwidth;
                //        double myPicHeight = picOutputheight;
                //        double myScaleX = myPicWidth / (double)OutputWidth;
                //        double myScaleY = myPicHeight / (double)OutputHeight;
                //        double myStartX = 0;
                //        double myStartY = 0;
                //        if (myScaleX < myScaleY)
                //        {
                //            myPicWidth = (double)OutputWidth * myScaleY;
                //            myStartX = ((double)picOutputwidth - myPicWidth) / 2;
                //        }
                //        else
                //        {
                //            myPicHeight = (double)OutputHeight * myScaleX;
                //            myStartY = ((double)picOutputheight - myPicHeight) / 2;
                //        }
                //        Image img = new Bitmap(oPatientDemo.PatientPhoto, new Size((int)myPicWidth, (int)myPicHeight));
                //        picPAPhoto.Image = img;
                //        //picPAPhoto.SetCenter();
                //        picPAPhoto.Visible = true;
                //    }
                ////picPAPhoto.Image = oPatientDemo.PatientPhoto;
                ////picPAPhoto.SizeMode = PictureBoxSizeMode.StretchImage;

                //End/ Commented Code
                //Start/ new Code
                if (oPatientDemo.PatientPhoto != null)
                {

                    picPAPhoto.byteImage = oPatientDemo.MyPictureBoxControl;
                    semaPhoreTosuppressValueChange = true;
                    myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
                    semaPhoreTosuppressValueChange = false;
                    myNewTrackBar.Visible = true;
                    TrackbarPlus.Visible = true;
                    TrackbarMinus.Visible = true;
                }
                else
                {
                    oPatientDemo.MyPictureBoxControl = null;
                    myNewTrackBar.Visible = false;
                    TrackbarPlus.Visible = false;
                    TrackbarMinus.Visible = false;
                }
                //End/ new Code

                //Misc
                chkExempt.Checked = oPatientDemo.ExemptFromReport;
                chkdirective.Checked = oPatientDemo.Directive;
                //Code Added by Mayuri

                // code commented by SaiKrishna because excludefromstatement and SentToCollection related to account not patient
                // chkExcludefromStatement.Checked = oPatientDemo.ExcludeFromStatement;
                // chkSetToCollection.Checked = oPatientDemo.SetToCollection;



                //Occupation
                if (Convert.ToString(_PatientOccupation.EmployerName) != "")
                {
                    cmbPAOccupation.Items.Add(Convert.ToString(PatientOccupationDetails.EmployerName));
                }
                else if (Convert.ToString(_PatientOccupation.Occupation.ToString()) != "")
                {
                    cmbPAOccupation.Items.Add(PatientOccupationDetails.Occupation.ToString());
                }

                if (Convert.ToString(_PatientOccupation.EmployerName) != "" && Convert.ToString(_PatientOccupation.Occupation.ToString()) != "")
                {
                    cmbPAOccupation.Items.Clear();
                    cmbPAOccupation.Items.Add(Convert.ToString(PatientOccupationDetails.EmployerName + "-" + PatientOccupationDetails.Occupation.ToString()));
                }
                if (cmbPAOccupation.Items.Count > 0)
                {
                    cmbPAOccupation.SelectedIndex = 0;
                }

                // Mother
                if ((PatientGuardianDetails.PatientMotherFirstName.ToString() + " " + PatientGuardianDetails.PatientMotherLastName.ToString()).Trim() != "")
                {
                    cmbGaurdian.Items.Add(PatientGuardianDetails.PatientMotherFirstName.ToString() + " " + PatientGuardianDetails.PatientMotherLastName.ToString());
                    //added by SaiKrishna:2011-06-27(yyyy-mm-dd)
                    cmbSameAsGuardian.Items.Add("Mother");
                }
                // Father
                if ((PatientGuardianDetails.PatientFatherFirstName.ToString() + " " + PatientGuardianDetails.PatientFatherLastName.ToString()).Trim() != "")
                {
                    cmbGaurdian.Items.Add(PatientGuardianDetails.PatientFatherFirstName.ToString() + " " + PatientGuardianDetails.PatientFatherLastName.ToString());
                    //added by SaiKrishna:2011-06-27(yyyy-mm-dd)
                    cmbSameAsGuardian.Items.Add("Father");
                }
                //Gaurdian
                if ((PatientGuardianDetails.PatientGuardianFirstName.ToString() + " " + PatientGuardianDetails.PatientGuardianLastName.ToString()).Trim() != "")
                {
                    cmbGaurdian.Items.Add(PatientGuardianDetails.PatientGuardianFirstName.ToString() + " " + PatientGuardianDetails.PatientGuardianLastName.ToString());
                    //added by SaiKrishna:2011-06-27(yyyy-mm-dd)
                    cmbSameAsGuardian.Items.Add("Other Guardian");
                }

                if (cmbGaurdian.Items.Count > 0)
                { cmbGaurdian.SelectedIndex = 0; }

                //Patient AccountGuarantor.
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    //code added by SaiKrishna
                    for (int i = 0; i < oPatientGuarantors.Count; i++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[i].FirstName + " " + ((oPatientGuarantors[i].MiddleName != "") ? oPatientGuarantors[i].MiddleName + " " : "") + oPatientGuarantors[i].LastName;
                        setCmbSameAsGuardianIndex();
                        if (_IsPatientAccountFeature == false)
                        {
                            nPAccountId = oPatientGuarantors[i].PAccountID;
                            nGuarantorId = oPatientGuarantors[i].PatientContactID;
                            DataTable dtAccount = objgloAccount.GetAccountDetailsById(nPAccountId);
                            if (dtAccount != null && dtAccount.Rows.Count > 0)
                            {
                                if (dtAccount.Rows[0]["bIsExcludeStatement"] != DBNull.Value && Convert.ToString(dtAccount.Rows[0]["bIsExcludeStatement"]).Trim() != "")
                                { chkExcludefromStatement.Checked = Convert.ToBoolean(dtAccount.Rows[0]["bIsExcludeStatement"]); }
                                else
                                { chkExcludefromStatement.Checked = false; }

                                if (dtAccount.Rows[0]["bIsSentToCollection"] != DBNull.Value && Convert.ToString(dtAccount.Rows[0]["bIsSentToCollection"]).Trim() != "")
                                { chkSetToCollection.Checked = Convert.ToBoolean(dtAccount.Rows[0]["bIsSentToCollection"]); }
                                else
                                { chkSetToCollection.Checked = false; }
                            }
                        }
                    }

                }

                //PatientotherGuarantors
                if (oPatientOtherGuarantors != null && oPatientOtherGuarantors.Count > 0)
                {
                    DataTable dtGuarantor = new DataTable();
                    DataColumn dcId = new DataColumn("Id");
                    DataColumn dcDescription = new DataColumn("Description");
                    dtGuarantor.Columns.Add(dcId);
                    dtGuarantor.Columns.Add(dcDescription);

                    for (int i = 0; i < oPatientOtherGuarantors.Count; i++)
                    {
                        DataRow drTemp = dtGuarantor.NewRow();
                        drTemp["Id"] = oPatientOtherGuarantors[i].GuarantorAsPatientID;
                        drTemp["Description"] = oPatientOtherGuarantors[i].FirstName + " " + ((oPatientOtherGuarantors[i].MiddleName != "") ? oPatientOtherGuarantors[i].MiddleName + " " : "") + oPatientOtherGuarantors[i].LastName;//Guarantor name 
                        dtGuarantor.Rows.Add(drTemp);
                    }
                  //  cmbOtherGuarantor.Items.Clear();
                    cmbOtherGuarantor.DataSource = null;
                    cmbOtherGuarantor.Items.Clear();
                    cmbOtherGuarantor.DataSource = dtGuarantor;

                    cmbOtherGuarantor.ValueMember = dtGuarantor.Columns["Id"].ColumnName;
                    cmbOtherGuarantor.DisplayMember = dtGuarantor.Columns["Description"].ColumnName;

                }
                if (cmbOtherGuarantor.Items.Count != 0)
                {
                    cmbOtherGuarantor.SelectedIndex = 0;
                }

                //if (oPatientRepresentatives != null && oPatientRepresentatives.Count > 0)
                //{
                //    DataTable dtPatientRepresentative = new DataTable();
                //    DataColumn dcId = new DataColumn("Id");
                //    DataColumn dcDescription = new DataColumn("Description");
                //    dtPatientRepresentative.Columns.Add(dcId);
                //    dtPatientRepresentative.Columns.Add(dcDescription);

                //    for (int i = 0; i < oPatientRepresentatives.Count; i++)
                //    {
                //        DataRow drTemp = dtPatientRepresentative.NewRow();
                //        drTemp["Id"] = oPatientRepresentatives[i].PRId;
                //        drTemp["Description"] = oPatientRepresentatives[i].FirstName + " " + oPatientRepresentatives[i].LastName;
                //        dtPatientRepresentative.Rows.Add(drTemp);
                //    }
                //    cmbPatientRepresentative.DataSource = null;
                //    cmbPatientRepresentative.Items.Clear();
                //    cmbPatientRepresentative.DataSource = dtPatientRepresentative;

                //    cmbPatientRepresentative.ValueMember = dtPatientRepresentative.Columns["Id"].ColumnName;
                //    cmbPatientRepresentative.DisplayMember = dtPatientRepresentative.Columns["Description"].ColumnName;

                //}
                //if (cmbPatientRepresentative.Items.Count != 0)
                //{
                //    cmbPatientRepresentative.SelectedIndex = 0;
                //}

                //for (int i = 0; i < PatientInsuranceDetails.InsurancesDetails.Count; i++)
                //{
                //    cmbGenInfoInsurance.Items.Add(PatientInsuranceDetails.InsurancesDetails[i].InsuranceName);
                //    cmbGenInfoInsurance.SelectedIndex = 0;
                //}

                //Fill Insurances 
              
                cmbGenInfoInsurance.DataSource = null;
                cmbGenInfoInsurance.Items.Clear();
                DataTable dtInsurances = new DataTable();
                dtInsurances.Columns.Add("InsuranceID");
                dtInsurances.Columns.Add("InsuranceName");
                for (int i = 0; i < PatientInsuranceDetails.InsurancesDetails.Count; i++)
                {
                    //Do not Show inactive Insurances in Insurance Combobox
                    if (_PatientInsurance.InsurancesDetails[i].InsuranceFlag != Convert.ToInt64(Insurance.InsuranceTypeFlag.None))
                    {
                        DataRow dr = dtInsurances.NewRow();
                        dr["InsuranceID"] = _PatientInsurance.InsurancesDetails[i].InsuranceID.ToString();
                        dr["InsuranceName"] = _PatientInsurance.InsurancesDetails[i].InsuranceName;
                        dtInsurances.Rows.Add(dr);
                    }
                }
                dtInsurances.AcceptChanges();
                cmbGenInfoInsurance.DataSource = dtInsurances;
                cmbGenInfoInsurance.DisplayMember = "InsuranceName";
                cmbGenInfoInsurance.ValueMember = "InsuranceID";
                if (dtInsurances.Rows.Count > 0)
                {
                    cmbGenInfoInsurance.SelectedIndex = 0;
                }


                //Providers
                txtPAProvider.Tag = Convert.ToInt64(oPatientDemo.PatientProviderID);

                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtPro = new DataTable();
                oDb.Connect(false);
                //Modified by Mayuri:20100519-Case No:#0004942
                String strQuery = "SELECT nProviderID, (sFirstName+SPACE(1)+ CASE sMiddleName WHEN  '' THEN '' When sMiddleName then   sMiddleName + SPACE(1)  END +sLastName) as ProviderName FROM Provider_MST WHERE nProviderID = " + oPatientDemo.PatientProviderID + " ";
                //String strQuery = "SELECT nProviderID, (sFirstName+' '+sMiddleName+' '+sLastName) as ProviderName FROM Provider_MST WHERE nProviderID = " + oPatientDemo.PatientProviderID + " ";
                oDb.Retrive_Query(strQuery, out dtPro);
                if (dtPro != null && dtPro.Rows.Count > 0)
                {
                    txtPAProvider.Text = dtPro.Rows[0]["ProviderName"].ToString();
                }
                if (dtPro != null)
                {
                    dtPro.Dispose();
                }
                oDb.Disconnect();
                oDb.Dispose();

                //-------------------------------
                // SignatureOnFile this field was on Other Details but moved on demographic control 
                // But Object is not changes so Property is available in Other Info object  

                chkSignatureOnFile.Checked = _PatientDemographicOtherInfo.SOF;

                //-------------------------------

                return true;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                //ex.ToString();
                //ex = null;
                MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //validate demographic data
        private bool ValidateData()
        {
            if (oAddresscontrol.txtZip.Focused == true)
            {
                oAddresscontrol.txtZip_LostFocus(null, null);
            }

            mtxtPADOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (mtxtPADOB.MaskCompleted == true)
            {
                try
                {
                    mtxtPADOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                    //Code review changes Replace IsValidDate() by gloDateMaster.gloDate.IsValidDateV2()
                    if (gloDateMaster.gloDate.IsValidDateV2(mtxtPADOB.Text))
                    {
                        //if (Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date)
                        //if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                        if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                        {
                            MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //mtxtPADOB.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                    return false;
                }
            }


            //Start :: Validate BirthTime 
            if (mtxtPADOB.MaskCompleted == true)
            {
                if (dtpBirth.Checked == true)
                {
                    try
                    {
                        DateTime _myBirthDateTime = Convert.ToDateTime(mtxtPADOB.Text);
                        string _myBirthTime = dtpBirth.Text.Trim();
                        TimeSpan _tmptimespan = GetAgeInHrs(_myBirthDateTime, _myBirthTime);
                        //GetPediatricSetting();
                        //if (glbIsPediatric == true)
                        //{
                        if (_tmptimespan.TotalHours < 0)
                        {
                            MessageBox.Show("Enter valid time of birth", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpBirth.Focus();
                            return false;
                        }
                        //}

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Enter a valid time of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        ex = null;
                        return false;
                    }
                }
            }

            //End :: Validate BirthTime 

            if ((txtPACode.Text.Length < 4) && (txtPatientPrefix.Text.Trim() != ""))
            {
                MessageBox.Show("Enter the patient code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPACode.Focus();
                return false;
            }
            else if (txtPACode.Text.Trim() == "")
            {
                MessageBox.Show("Enter the patient code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPACode.Focus();
                return false;
            }
            if (_IsSaveAsCopy == true)
            {
                _PatientId = 0;
                GetSameAsPatientGuarantor();
            }
            if (_PatientId == 0)
            {
                object oResult;
                gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                ogloSettings.GetSetting("UseSitePrefix", out oResult);
                ogloSettings.Dispose();
                ogloSettings = null;
                Int32 _UseSitePrefix = 0;
                if (oResult != null && oResult.ToString() != "")
                {
                    _UseSitePrefix = Convert.ToInt32(oResult);
                }
                if (_UseSitePrefix != 0)
                {
                    if (_IsSaveAsCopy && txtPACode.Mask.ToString().Contains("-") == false)
                    {
                        if (txtPACode.Text.Trim().ToString() == "9999999999999")
                        {
                            if (MessageBox.Show("You have entered the maximum possible Patient Code. No more Patient Codes can be auto generated.  In order to register additional patients, auto generation of patient codes must be turned off in the gloEMR Admin. Do you want to continue?.", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                txtPACode.Focus();
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is now made turn off as no more Patient Codes can be auto generated, continue with manual entry.", gloAuditTrail.ActivityOutCome.Success);
                                return false;
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is still turn on even no more Patient Codes can be auto generated, now patient code will be differenet that what it is appearing on new patient registration screen.", gloAuditTrail.ActivityOutCome.Success);
                            }

                        }

                    }
                    else
                    {
                        if (txtPACode.Text.Substring(3).Trim().ToString() == "9999999999")
                        {
                            if (MessageBox.Show("You have entered the maximum possible Patient Code. No more Patient Codes can be auto generated.  In order to register additional patients, auto generation of patient codes must be turned off in the gloEMR Admin. Do you want to continue?.", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                txtPACode.Focus();
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is now made turn off as no more Patient Codes can be auto generated, continue with manual entry.", gloAuditTrail.ActivityOutCome.Success);
                                return false;
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is still turn on even no more Patient Codes can be auto generated, now patient code will be differenet that what it is appearing on new patient registration screen.", gloAuditTrail.ActivityOutCome.Success);
                            }
                        }
                    }
                }
                else
                {
                    if (txtPACode.Text.Trim().ToString() == "9999999999999")
                    {
                        if (MessageBox.Show("You have entered the maximum possible Patient Code. No more Patient Codes can be auto generated.  In order to register additional patients, auto generation of patient codes must be turned off in the gloEMR Admin. Do you want to continue?.", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            txtPACode.Focus();
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is now made turn off as no more Patient Codes can be auto generated, continue with manual entry.", gloAuditTrail.ActivityOutCome.Success);
                            return false;
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is still turn on even no more Patient Codes can be auto generated, now patient code will be differenet that what it is appearing on new patient registration screen.", gloAuditTrail.ActivityOutCome.Success);
                        }

                    }

                }


            }


            gloPatient oPatientTrans = new gloPatient(_databaseconnectionstring);
            if (_PatientId == 0)
            {
                //if (oPatientTrans.ChkExistPatientID(txtPACode.Text.Trim()) == true)
                //{
                //    MessageBox.Show("Patient code is assigned to another patient.  Please select a unique patient code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtPACode.Focus();
                //    return false;
                //}
                //Added by Mayuri:20101125-to validate on new patient
                //scenario: 1. Copy patient settings ON-User copies patient having current sequential code
                //2. User changes settings As Auto geretae ON
                //3. then on save n close it should validate for ssame sequential code as we set focus on ssn and not on patient code field
                if (oPatientTrans.ChkExistPatientIDUpdate(txtPACode.Text.Trim(), _PatientId) == true)
                {
                    if (MessageBox.Show("Duplicate patient code, do you want to generate new patient code?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);


                        txtPACode.Text = ogloPatient.GeneratePatientCode();
                        if (ogloPatient._UseSitePrefix != 0)
                        {
                            if (txtPACode.Text.Length <= 10)
                            { txtPACode.Mask = "AAA-AAAAAAAAAA"; }
                            else
                            { txtPACode.Mask = "AAA-AAAAAAAAAAA"; }
                            txtPatientPrefix.Text = txtPACode.Text.Substring(0, 3);
                        }
                        else
                        {
                            txtPACode.Mask = "AAAAAAAAAAAAA";
                            txtPatientPrefix.Text = "";

                        }
                        ogloPatient.Dispose();
                        ogloPatient = null;

                    }
                    //Added by Mayuri:#20101206-To fix issue:#6846-message showing thrice
                    //MessageBox.Show("Patient code is assigned to another patient. Select a unique patient code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (txtPACode.ReadOnly == true)
                    {
                        txtmPASSN.Select();
                        txtmPASSN.Focus();
                    }
                    else
                    {
                        txtPACode.Focus();
                    }
                    oPatientTrans.Dispose();
                    oPatientTrans = null;
                    return false;
                }
            }
            else
            {
                if (oPatientTrans.ChkExistPatientIDUpdate(txtPACode.Text.Trim(), _PatientId) == true)
                {
                    if (MessageBox.Show("Duplicate patient code, do you want to generate new patient code?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);


                        txtPACode.Text = ogloPatient.GeneratePatientCode();
                        if (ogloPatient._UseSitePrefix != 0)
                        {
                            txtPACode.Mask = "AAA-AAAAAAAAAA";
                            txtPatientPrefix.Text = txtPACode.Text.Substring(0, 3);
                        }
                        else
                        {
                            txtPACode.Mask = "AAAAAAAAAAAAA";
                            txtPatientPrefix.Text = "";

                        }
                        ogloPatient.Dispose();
                        ogloPatient = null;
                    }

                    //MessageBox.Show("Patient code is assigned to another patient. Select a unique patient code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPACode.Focus();
                    oPatientTrans.Dispose();
                    oPatientTrans = null;
                    return false;
                }
            }
            oPatientTrans.Dispose();
            oPatientTrans = null;
            //Check if SSN is completed
            //txtmPASSN.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (txtmPASSN.Text.Length > 0 && txtmPASSN.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter the valid SSN number.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtmPASSN.Focus();
            //    return false;
            //}
            if (txtmPASSN.IsValidated == false)
            {
                txtmPASSN.Focus();
                return false;
            }
            //******first name
            if (txtPAFname.Text.Trim() == "")
            {
                MessageBox.Show("Enter a first name for the patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPAFname.Focus();
                return false;
            }

            //******Last name
            if (txtPALName.Text.Trim() == "")
            {
                MessageBox.Show("Enter a last name for the patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPALName.Focus();
                return false;
            }
            //date of birth      
            mtxtPADOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mtxtPADOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a date of birth for the patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPADOB.Focus();
                return false;
            }
            //Validating for leap year..
            mtxtPADOB.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            //Code review changes Replace IsValidDate() by gloDateMaster.gloDate.IsValidDateV2() 
            if (!gloDateMaster.gloDate.IsValidDateV2(mtxtPADOB.Text))
            {
                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPADOB.Focus();
                return false;
            }
            //if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
            if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
            {
                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPADOB.Focus();
                return false;
            }
            //if (dtpPADOB.Checked == false)
            //{
            //    MessageBox.Show("Please enter the Patient Date of Birth ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dtpPADOB.Focus();
            //    return false;
            //}

            //Provider Required
            if (txtPAProvider.Text == "")
            {
                MessageBox.Show("Select a provider for the patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPAProvider.Focus();
                return false;
            }

            //Gender Required
            //if (rbGender1.Checked == false && rbGender2.Checked == false && rbGender3.Checked == false)
            //{
            //    MessageBox.Show("Select patient gender.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //rbGender1.Focus();
            //    return false;
            //}
            if (cmbGender.SelectedIndex <= 0)
            {
                MessageBox.Show("Select patient gender.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //rbGender1.Focus();
                return false;
            }

            //Code added by SaiKrishna date 2011-06-27(yyyy-mm-dd) for address validation.
            //if (oAddresscontrol.txtAddress1.Text.Trim() == "")
            //{
            //    MessageBox.Show("Enter address for patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    oAddresscontrol.txtAddress1.Focus();
            //    return false;
            //}
            //if (oAddresscontrol.txtCity.Text.Trim() == "")
            //{
            //    MessageBox.Show("Enter city for patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    oAddresscontrol.txtCity.Focus();
            //    return false;
            //}
            //if (oAddresscontrol.cmbState.Text.Trim() == "")
            //{
            //    MessageBox.Show("Enter state for patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    oAddresscontrol.cmbState.Focus();
            //    return false;
            //}
            //if (oAddresscontrol.txtZip.Text.Trim() == "")
            //{
            //    MessageBox.Show("Enter zip for patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    oAddresscontrol.txtZip.Focus();
            //    return false;
            //}

            //Incomplete Phone Numbers
            ////mtxtPAPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            ////if (mtxtPAPhone.Text.Length > 0 && mtxtPAPhone.MaskCompleted == false)
            ////{
            ////    MessageBox.Show("Please enter a 10 digit number for phone.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    mtxtPAPhone.Focus();
            ////    return false;
            ////}
            //Incomplete 
            if (mtxtPAPhone.IsValidated == false)
            {
                //mtxtPAPhone.Focus();
                return false;
            }
            if (txtPAFax.IsValidated == false)
            {
                return false;
            }
            //mtxtPAMobile.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mtxtPAMobile.Text.Length > 0 && mtxtPAMobile.MaskCompleted == false)
            //{

            //    MessageBox.Show("Please enter a 10 digit number for mobile.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mtxtPAMobile.Focus();
            //    return false;

            //}
            if (mtxtPAMobile.IsValidated == false)
            {
                //mtxtPAPhone.Focus();
                return false;
            }



            //Incomplete Emergency Phone Numbers
            ////mtxtEmergencyPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            ////if (mtxtEmergencyPhone.Text.Length > 0 && mtxtEmergencyPhone.MaskCompleted == false)
            ////{
            ////    MessageBox.Show("Please enter a 10 digit number for emergency contact phone.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    mtxtEmergencyPhone.Focus();
            ////    return false;
            ////}
            if (mtxtEmergencyPhone.IsValidated == false)
            {
                //mtxtEmergencyPhone.Focus();
                return false;
            }
            //Incomplete Emergency mobile 
            ////mtxtEmergencyMobile.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            ////if (mtxtEmergencyMobile.Text.Length > 0 && mtxtEmergencyMobile.MaskCompleted == false)
            ////{

            ////    MessageBox.Show("Please enter a 10 digit number for emergency contact mobile.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    mtxtEmergencyMobile.Focus();
            ////    return false;

            ////}
            if (mtxtEmergencyMobile.IsValidated == false)
            {
                //mtxtEmergencyMobile.Focus();
                return false;
            }
            //Invalid Email address
            if (CheckEmailAddress(txtPAEmail.Text) == false)
            {
                MessageBox.Show("Enter a valid email ID.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPAEmail.Focus();
                return false;
            }

            //Patient Portal
            if (cbSendPatientPortalActivationEmail.Visible)
            {
                if (cbSendPatientPortalActivationEmail.Checked)
                {
                    if ((txtPAEmail.Text.Trim() == "") && (oAddresscontrol.txtZip.Text.Trim() == ""))
                    {
                        MessageBox.Show("You must enter a valid Email address and a Zip Code to send Patient Portal Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPAEmail.Focus();
                        return false;
                    }
                    else if (txtPAEmail.Text.Trim() == "")
                    {
                        MessageBox.Show("You must enter a valid Email address to send Patient Portal Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPAEmail.Focus();
                        return false;
                    }
                    else if (oAddresscontrol.txtZip.Text.Trim() == "")
                    {
                        MessageBox.Show("You must enter a Zip Code to send Patient Portal Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        oAddresscontrol.txtZip.Focus();
                        return false;
                    }
                }
            }
            //Patient Portal
            //API

            if (cbSendAPIInvitation.Visible)
            {
                if (cbSendAPIInvitation.Checked)
                {

                    if (txtPAEmail.Text.Trim() == "")
                    {
                        MessageBox.Show("You must enter a valid Email address to send Patient API Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPAEmail.Focus();
                        return false;
                    }
                    ////else if (oAddresscontrol.txtZip.Text.Trim() == "")
                    ////{
                    ////    MessageBox.Show("You must enter a Zip Code to send Patient Portal Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////    oAddresscontrol.txtZip.Focus();
                    ////    return false;
                    ////}
                }
            }

            //API
            //Alert For SSN no if already exists...
            if (txtmPASSN.Text.Trim() != "")
            {
                gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                Boolean _IsExist = ogloPatient.IsPatientSSNExists(txtmPASSN.Text.Trim(), _PatientId);

                // check for (if exits and ssn should not Blank)
                if (_IsExist == true)
                {
                    DialogResult oDialogResult = DialogResult.None;
                    oDialogResult = MessageBox.Show("SSN already exists for another patient. Do you want to save patient with same SSN ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (oDialogResult != DialogResult.Yes)
                    {
                        //Added code to fix:6209-Validation message for duplicate patient was coming twice afte click on NO of above message
                        if (ValidateDescription(txtPACode.Text, _PatientId) == true)
                        {
                            //txtmPASSN.Focus();
                            txtPACode.Focus();
                            txtPACode_Validating(null, null);
                        }
                        else
                        {
                            txtmPASSN.Focus();
                        }
                        ogloPatient.Dispose();
                        ogloPatient = null;
                        return false;
                    }
                }
                ogloPatient.Dispose();
                ogloPatient = null;
            }



            //for new patient, Give Alert when patient name and DOB already exists ...


            gloPatient ogloDPatient = new gloPatient(_databaseconnectionstring);



            Boolean _IsExists = ogloDPatient.IsPatientExists(txtPAFname.Text.Trim(), txtPALName.Text.Trim(), Convert.ToDateTime(mtxtPADOB.Text.Trim()), _PatientId);


            if (_IsExists == true)
            {
                string sMessage = "";
                if (_PatientId == 0)
                    sMessage = "Patient with same name and date of birth already exists. Do you want to register as a new patient?";
                else if (_IsSaveAsCopy == true)
                    sMessage = "Patient with same name and date of birth already exists. Do you want to register as a new patient?";
                else
                    sMessage = "Patient with same name and date of birth already exists. Do you want to modify patient with same name?";

                DialogResult oDialogResult = DialogResult.None;

                oDialogResult = MessageBox.Show(sMessage, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (oDialogResult != DialogResult.Yes)
                {
                    txtPAFname.Focus();
                    ogloDPatient.Dispose();
                    ogloDPatient = null;
                    return false;
                }

            }
            ogloDPatient.Dispose();
            ogloDPatient = null;
            // Check Ethnicity,Language,Race


            //gloGlobal.clsMU objMU = new gloGlobal.clsMU();   
            
            ////06-Oct-14 Aniket: Implement the Race, Ethnicity check only if there are MU dashboards created
            //if (objMU.GetMUReportCount() > 0)
            //{

            //StringBuilder sELR = new StringBuilder("");
            //StringBuilder sFocus = new StringBuilder("");
            //string sELRCondition = "";
            //string ErrMessage = "";

            //if (cmbPAEthn.SelectedIndex <= 0)
            //{
            //    sELR.Append("1");
            //    sFocus.Append("1");
            //}
            //else
            //{
            //    sELR.Append("0");
            //    sFocus.Append("0");
            //}

            //if (cmbPALang.SelectedIndex <= 0)
            //{
            //    sELR.Append("1");
            //    sFocus.Append("2");
            //}
            //else
            //{
            //    sELR.Append("0");
            //    sFocus.Append("0");
            //}

            //if (cmbPARace.Text =="")
            //{
            //    sELR.Append("1");
            //    sFocus.Append("3");
            //}
            //else
            //{
            //    sELR.Append("0");
            //    sFocus.Append("0");
            //}

            //sELRCondition = sELR.ToString();

            //if (sELRCondition != "000")
            //{
            //    ErrMessage = GetErrorMessage(sELRCondition);
            //    if (MessageBox.Show("Following demographics are not recorded for this patient" + System.Environment.NewLine + System.Environment.NewLine + ErrMessage + System.Environment.NewLine + System.Environment.NewLine + "These are required for the MU measure 'Record Demographics'. Do you want to continue saving the record ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            //    {
            //        if (sFocus.ToString().Contains("1"))
            //        {
            //            cmbPAEthn.Focus();
            //        }
            //        if (sFocus.ToString().Contains("2"))
            //        {
            //            cmbPALang.Focus();
            //        }
            //        if (sFocus.ToString().Contains("3"))
            //        {
            //            cmbPARace.Focus();
            //        }
            //        objMU = null;
            //        return false;
            //    }
                
            //}
            

            //}
            
            //objMU = null;

            // // Check Ethnicity,Language,Race

            //Patient is less than 18 years old. give message Guarantor is required.
            int nDays = ((365 * 18) + 4);
            DateTime dtEighteenYear = DateTime.Now.Subtract(new TimeSpan(nDays, 0, 0, 0));
            mtxtPADOB.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            if (Convert.ToDateTime(mtxtPADOB.Text).Date > dtEighteenYear.Date)
            {

                if (_IsPatientAccountFeature == false)
                {
                    if (cmbSameAsGuardian.Text == "Patient")
                    {
                        if (MessageBox.Show(" Patient is less than 18 years old. Do you want to select a different guarantor ?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            btnGuarantorExistingPatientBrowse.Focus();
                            return false;
                        }
                    }
                }
                else
                {
                    if (_PatientId == 0)
                    {
                        if (rbAccountNew.Checked == true && rbAccountNew.Visible == true)
                        {
                            if (cmbSameAsGuardian.Text == "Patient")
                            {
                                if (MessageBox.Show(" Patient is less than 18 years old. Do you want to select a different guarantor ?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    btnGuarantorExistingPatientBrowse.Focus();
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {

                        gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                        object _showMessage = 0;

                        try
                        {

                            oParameters.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@bShowAddGuarantorMessage", _showMessage, ParameterDirection.InputOutput, SqlDbType.Bit);

                            oDBLayer.Connect(false);
                            oDBLayer.Execute("gsp_ShowAddGuarantorMessage", oParameters, out _showMessage);
                            oDBLayer.Disconnect();

                            if (_showMessage != null && _showMessage != DBNull.Value && Convert.ToString(_showMessage).Trim() != ""
                                && Convert.ToBoolean(_showMessage) == true)
                            {
                                if (MessageBox.Show(" Patient is less than 18 years old. Do you want to select a different guarantor ?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    btnGuarantorExistingPatientBrowse.Focus();
                                    return false;
                                }
                            }
                        }
                        catch //(Exception ex)
                        {
                        }
                        finally
                        {
                            _showMessage = null;
                            if (oParameters != null) { oParameters.Dispose(); }
                            if (oDBLayer != null) { oDBLayer.Dispose(); }
                        }

                    }
                }

                //// Added by SaiKrishna:2011-06-27(yyyy-mm-dd).If it is SaveAsCopyPatient no need to add guarantor,because he can add either existing or new account. 
                //if (oPatientGuarantors.Count < 1 && _IsSaveAsCopy == false)
                //{
                //    if (MessageBox.Show(" Patient is less than 18 years old. Do you want to select a different guarantor ?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //    {
                //        btnGuarantorExistingPatientBrowse.Focus();
                //        return false;
                //    }
                //}
                //else if (oPatientGuarantors.Count > 0 && oPatientGuarantors[0].Relation == "Self")
                //{
                //    if (MessageBox.Show(" Patient is less than 18 years old. Do you want to select a different guarantor ?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //    {
                //        btnGuarantorExistingPatientBrowse.Focus();
                //        return false;
                //    }
                //}
            }
            //patient Account Feature
            //Added by SaiKrishna:2011-06-27(yyyy-mm-dd) 
            if (txtAccountNo.Text.Trim() == "" && txtAccountNo.Visible == true && _IsPatientAccountFeature == true)
            {
                MessageBox.Show(((rbAccountNew.Checked == true) ? "Enter" : "Select") + " Acct.#.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show("Enter Account #.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return false;
            }
            //if (txtAccountDescription.Text == "" && txtAccountDescription.Visible == true && txtAccountNo.Visible == true && _IsPatientAccountFeature == true)
            //{
            //    MessageBox.Show("Enter Acct. Desc.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtAccountDescription.Focus();
            //    return false;
            //}

            if (txtAccGuarantor.Text == "" && txtAccGuarantor.Visible == true)
            {
                MessageBox.Show("Select guarantor for Account.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (CheckAccountNoExistsForGuarantor(txtAccountNo.Text.Trim()) == true && rbAccountNew.Checked == true && rbAccountNew.Visible == true && _IsPatientAccountFeature == true)
            {
                MessageBox.Show("Acct.#/Patient Code already exists.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return false;
            }
            if (_IsRequireBusinessCenterOnPAccounts && _IsPatientAccountFeature)
            {
                if (Convert.ToInt64(cmbBusinessCenter.SelectedValue) == 0 && lblBusinessCenter.Text == "")
                {
                    MessageBox.Show("Select Business Center for Account.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            
            //7022Items: Home Billing
            //Bug #47729: gloEMR/PM- Copy patient- Application is displaying unnecessary message for area code if we registered patient through scan ID 
            if (oAddresscontrol.txtAreaCode.Visible == true)
            {
                //7022Items: Home Billing
                //Added validation if area code enter and it's length >0 and <4 then this message is shown on save&cls.
                if (oAddresscontrol.txtAreaCode.TextLength > 0 && oAddresscontrol.txtAreaCode.TextLength < 4)
                {
                    //Bug #47660: Exception While Copy Patient
                    //check for only if No nutton is click and move to end as message shows multiple time.
                    if (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        oAddresscontrol.txtAreaCode.Select();
                        oAddresscontrol.txtAreaCode.Focus();
                        return false;
                    }
                }
            }
            #region "Check special Character"
            var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z\b]");
            if (regex.IsMatch(txtPAFname.Text.Trim()) || regex.IsMatch(txtPAMName.Text.Trim()) || regex.IsMatch(txtPALName.Text.Trim()))
            {
                if ((MessageBox.Show("Patient name contains special/numeric character(s) which may cause billing rejection.\n Continue Save?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) == DialogResult.No)
                {
                    if (regex.IsMatch(txtPAFname.Text.Trim())) { txtPAFname.Select(); return false; }
                    if (regex.IsMatch(txtPAMName.Text.Trim())) { txtPAMName.Select(); return false; }
                    if (regex.IsMatch(txtPALName.Text.Trim())) { txtPALName.Select(); return false; }
                }
            }

            #endregion "Check special Character"



            //Bug No.49416::Language - Application does not display message box if user add new language.::20130418

            //03-Jun-13 Aniket: Do not allow user to add languages on the Patient Screen
            //if (!isLanguageValidated)
            //{
            //    if (!ValidateLanguage())
            //    {
            //        cmbPALang.Focus();
            //        return false;
            //    }
            //}
            return true;
        }


        private string GetErrorMessage(string sELR)
        {
            string EMessage = "";
            switch (sELR)
            {
                case "111":
                    EMessage = "Ethnicity"+System.Environment.NewLine+"Language"+System.Environment.NewLine+"Race";
                    break;
                case "110":
                    EMessage = "Ethnicity"+System.Environment.NewLine+"Language";
                    break;
                case "101":
                    EMessage = "Ethnicity"+System.Environment.NewLine+"Race";
                    break;
                case "100":
                    EMessage = "Ethnicity";
                    break;
                case "011":
                    EMessage = "Language"+System.Environment.NewLine+"Race";
                    break;
                case "010":
                    EMessage = "Language";
                    break;
                case "001":
                    EMessage = "Race";
                    break;     
            }

            return EMessage;

        }


        //Function for validating Leap Year..
        private bool IsValidDate(string DOB)
        {
            Int32 year = 0; Int32 month = 0; Int32 day = 0;
            //vishal 
            if (DOB.Trim().Length <= 4)   // for blank date,length=4 ,including '/' character...  
            {
                return true;
            }

            //*****
            string[] _Date = DOB.Split('/');
            if (_Date.Length == 3)
            {
                for (int i = 0; i < _Date.Length; i++)
                {
                    if (_Date[i].Trim() != "")
                    {
                        if (i == 0)
                        {
                            month = Convert.ToInt32(_Date[i]);
                        }
                        if (i == 1)
                        {
                            day = Convert.ToInt32(_Date[i]);
                        }
                        if (i == 2)
                        {

                            if (_Date[i].Trim().Replace("_", "").Length == 4)
                                year = Convert.ToInt32(_Date[i]);
                            else
                                return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }

                if (month > 12)
                {
                    return false;
                }

                if (day == 29)
                {
                    if (month == 2)
                    {
                        if (year % 4 == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    else
                    {
                        return true;
                    }
                }
                else if (day > 31)
                {
                    return false;
                }
                else if (day == 0)
                {
                    return false;
                }
                else if (day == 31)
                {
                    if (month == 2 || month == 4 || month == 6 || month == 9 || month == 11)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }


        private void mtxtPADOB_Validating(object sender, CancelEventArgs e)
        {
            mtxtPADOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mtxtPADOB.Text.Length > 0 && mtxtPADOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (mtxtPADOB.MaskCompleted == true)
                {
                    try
                    {
                        mtxtPADOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        //Code review changes Replace IsValidDate() by gloDateMaster.gloDate.IsValidDateV2()
                        if (gloDateMaster.gloDate.IsValidDateV2(mtxtPADOB.Text))
                        {
                            //if (Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date)
                            //if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mtxtPADOB.Focus();
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                        MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
        }


        //check for modifications
        public bool IsModified()
        {
            
            if (_PatientId != 0)
            {
                //mtxtPAPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                //mtxtPAMobile.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                //mtxtEmergencyPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                //mtxtEmergencyMobile.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mtxtPADOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                //*****

                //for Validating leap year...(else it'll give error in comparision while converting to date)
                //Code review changes Replace IsValidDate() by gloDateMaster.gloDate.IsValidDateV2()
                if (!gloDateMaster.gloDate.IsValidDateV2(mtxtPADOB.Text))
                {
                    return false;
                }



                if (mtxtPADOB.Text.Replace(" /", "").Trim() != "")
                {

                    #region Temp Checking for changed values
                    string strDOB = "";
                    if (oPatientDemo.PatientDOB.Date.Day < 10)
                    {
                        strDOB = oPatientDemo.PatientDOB.Date.ToShortDateString().Insert(2, "0");
                    }
                    else
                    {
                        strDOB = oPatientDemo.PatientDOB.Date.ToShortDateString();
                    }
                    if (oPatientDemo.PatientDOB.Date.Month < 10)
                    {
                        strDOB = strDOB.Insert(0, "0");
                    }
                    string Genderchanged = String.Empty;
                    //if (rbGender1.Checked)
                    //{
                    //    Genderchanged = "Male";
                    //}
                    //else if (rbGender2.Checked)
                    //{
                    //    Genderchanged = "Female";
                    //}
                    //else if (rbGender3.Checked)
                    //{
                    //    Genderchanged = "Other";
                    //}
                    Genderchanged = cmbGender.Text.Trim();
                    #endregion
                    
                    //Dhruv 20100104 
                    //To check on save and close wheather it has been modified or not
                    if (txtPACode.Text.Trim() == oPatientDemo.PatientCode &&
                           txtPAFname.Text.Trim() == oPatientDemo.PatientFirstName &&
                           txtPAMName.Text.Trim() == oPatientDemo.PatientMiddleName &&
                           txtPALName.Text.Trim() == oPatientDemo.PatientLastName &&
                           //Commented and Added for Bug #77539: 00000115 : Patient Setup
                           //picPAPhoto.Image == oPatientDemo.PatientPhoto &&
                            _ispicPAPhotomodified == false &&
                            picPAPhoto.IsPAPhotomodified ==false &&
                           ((txtmPASSN.Text.Trim() == "" && oPatientDemo.PatientSSN == "") || (txtmPASSN.Text.Trim() == oPatientDemo.PatientSSN.ToString())) &&
                            //dtpPADOB.Value.Date == oPatientDemo.PatientDOB.Date  &&
                            mtxtPADOB.Text == strDOB &&
                           cmbPAMarital.Text == oPatientDemo.PatientMaritalStatus &&
                            //dhruv 20091229 
                            //new control added gloAddress
                            //txtPAAddress1.Text == oPatientDemo.PatientAddress1 &&
                        //txtPAAddress2.Text == oPatientDemo.PatientAddress2 &&
                        //txtPACity.Text == oPatientDemo.PatientCity &&
                        //cmbPAState.Text == oPatientDemo.PatientState &&
                        //txtPAZip.Text == oPatientDemo.PatientZip &&
                        //txtPACounty.Text == oPatientDemo.PatientCounty &&
                        //New data validation not done.
                            oAddresscontrol.txtAddress1.Text == oPatientDemo.PatientAddress1.ToString() &&
                            oAddresscontrol.txtAddress2.Text == oPatientDemo.PatientAddress2.ToString() &&
                            oAddresscontrol.txtZip.Text == oPatientDemo.PatientZip &&
                            oAddresscontrol.txtCity.Text == oPatientDemo.PatientCity &&
                            oAddresscontrol.cmbCountry.Text == oPatientDemo.PatientCountry &&
                            oAddresscontrol.cmbState.Text == oPatientDemo.PatientState.ToString() &&
                            oAddresscontrol.txtCounty.Text == oPatientDemo.PatientCounty &&

                           mtxtPAPhone.Text == oPatientDemo.PatientPhone &&
                           mtxtPAMobile.Text == oPatientDemo.PatientMobile &&
                           txtPAEmail.Text == oPatientDemo.PatientEmail &&
                           txtPAFax.Text == oPatientDemo.PatientFax &&
                           txtEmergencyContact.Text == oPatientDemo.EmergencyContact &&
                           mtxtEmergencyPhone.Text == oPatientDemo.EmergencyPhone &&
                           mtxtEmergencyMobile.Text == oPatientDemo.EmergencyMobile &&
                        //Convert.ToInt64(txtPAPharma.Tag) == oPatientDemo.PatientPharmacyID &&
                        //Convert.ToInt64(txtPAPrimaryCarePhy.Tag) == oPatientDemo.PatientPCPId &&
                           Convert.ToInt64(txtPAProvider.Tag) == oPatientDemo.PatientProviderID &&
                           _isPrimaryCarePhysicianModified == false && _isPharmacyModified == false &&
                           _isRefferalsModified == false && _isOccupationModified == false &&
                            _isCareTeamModified == false && 
                           _isInsurenceModified == false && _isGaurentorModified == false &&
                           _isGaurdianModified == false && _isPatientOtherModified == false &&

                        //cmbPARace.Text == oPatientDemo.PatientRace &&
                         GetSelectedRace() == oPatientDemo.PatientRace &&
                        Genderchanged.ToString() == oPatientDemo.PatientGender &&
                        //cmbPACountry.Text == oPatientDemo.PatientCountry &&
                        cmbPAHandDom.Text == oPatientDemo.PatientHandDominance &&
                        cmbRelationship.Text == oPatientDemo.EmergencyRelationshipDesc &&
                         cmbPALocation.Text == oPatientDemo.PatientLocation.Trim() &&
                        cmbPALang.Text == oPatientDemo.PatientLanguage )
                    {
                        return false;  // not modified
                    }


                    else
                    {
                        _IsAuditLogModified = true;
                        return true;   // modified

                    }
                }


            }
            _IsAuditLogModified = true;
            return true; //Date is not entered 
        }

        private String GetSelectedRace()
        {
            string strRace = "";

            try
            {

                if (cmbPARace.SelectedIndex != -1)
                {
                    if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    {
                        if (cmbPARace.Items.Count > 0)
                        {
                            if (cmbPARace.Text == "" || cmbPARace.Text == "Declined to specify" || cmbPARace.Text == "Unknown")
                            {
                                strRace = cmbPARace.Text;
                            }
                            else
                            {
                                cmbPARace.SelectedIndex = 0;
                                for (int race = 0; race <= cmbPARace.Items.Count - 1; race++)
                                {
                                    if (race == 0)
                                    {
                                        strRace = cmbPARace.Text.Trim();
                                    }
                                    else
                                    {
                                        cmbPARace.SelectedIndex = race;
                                        strRace = strRace + "|" + cmbPARace.Text.Trim();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        strRace = cmbPARace.Text.Trim();
                    }
                }

                return strRace;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                strRace = null;
            }
        }

        private DataTable GetPatientDetails(long contactID, PatientContactType _PatientContactType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "SELECT Contacts_MST.sName,ISNULL(Contacts_MST.sContact,'') AS sContact,  ISNULL(Contacts_MST.sAddressLine1,'') AS sAddressLine1, ISNULL(Contacts_MST.sAddressLine2,'') AS sAddressLine2, "
                + " ISNULL(Contacts_MST.sCity,'') AS sCity, ISNULL(Contacts_MST.sState,'') AS sState, ISNULL(Contacts_MST.sZIP,'') AS sZIP, ISNULL(Contacts_MST.sPhone,'') AS sPhone, "
                + " ISNULL(Contacts_MST.sFax,'') AS sFax, ISNULL(Contacts_MST.sEmail,'') AS sEmail, ISNULL(Contacts_MST.sURL, '') AS sURL, ISNULL(Contacts_MST.sMobile,'') AS sMobile, ISNULL(Contacts_MST.sPager,'') AS sPager, "
                + " ISNULL(Contacts_MST.sNotes,'') AS sNotes, ISNULL(Contacts_MST.sFirstName,'') AS sFirstName, ISNULL(Contacts_MST.sMiddleName,'') AS sMiddleName, ISNULL(Contacts_MST.sLastName,'') AS sLastName, "
                + " ISNULL(Contacts_MST.sGender,'') AS sGender, ISNULL(Contacts_Physician_DTL.sTaxonomy,'') AS sTaxonomy, ISNULL(Contacts_Physician_DTL.sTaxonomyDesc,'') AS sTaxonomyDesc, "
                + " ISNULL(Contacts_Physician_DTL.sTaxID,'') AS sTaxID, ISNULL(Contacts_Physician_DTL.sUPIN,'') AS sUPIN, ISNULL(Contacts_Physician_DTL.sNPI,'') AS sNPI, "
                + " ISNULL(Contacts_Physician_DTL.sHospitalAffiliation,'') AS sHospitalAffiliation, ISNULL(Contacts_Physician_DTL.sExternalCode,'') AS sExternalCode, ISNULL(Contacts_Physician_DTL.sDegree,'') AS sDegree,"
                + " ISNULL(Contacts_MST.sNCPDPID,'') AS sNCPDPID,ISNULL(Contacts_MST.sPharmacyStatus,'') AS sPharmacyStatus,ISNULL(Contacts_MST.sServiceLevel,'') AS sServiceLevel,Contacts_MST.ActiveStartTime,Contacts_MST.ActiveEndTime"//Line added by Sandip Darade 20100513 case 4426
                + " ,ISNULL(Contacts_Physician_DTL.sPrefix,'') AS sPrefix"
                + " FROM Contacts_MST LEFT OUTER JOIN Contacts_Physician_DTL ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID "
                + " WHERE (Contacts_MST.nContactID = " + contactID + ") ";

                switch (_PatientContactType)
                {
                    case PatientContactType.Pharmacy:
                        {
                            SqlQuery += " AND (Contacts_MST.sContactType = 'Pharmacy')";
                        }
                        break;
                    case PatientContactType.PrimaryCarePhysician:
                    case PatientContactType.Referral:
                        {
                            SqlQuery += " AND (Contacts_MST.sContactType = 'Physician')";
                        }
                        break;
                    default:
                        break;
                }



                oDB.Retrive_Query(SqlQuery, out  dt);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dt;
        }

        #endregion

        #region "Control Load & Other Events"
        
        //Control Load
        private void gloPatientDemographicsControl_Load(object sender, EventArgs e)
        {
            //Sandip Darade 20091006
            //If fax is not internet fax do no masking  for fax information
            if (_IsInternetFax == false)
            {
                txtPAFax.MaskType = gloMaskControl.gloMaskType.Other;
            }

            if (oAddresscontrol != null)
            {
                if (pnlAddresssControl.Controls.Contains(oAddresscontrol))
                {
                    pnlAddresssControl.Controls.Remove(oAddresscontrol);

                }
                try
                {
                    oAddresscontrol.txtZip.TextChanged -= new EventHandler(txtZip_TextChanged1);
                }
                catch
                {
                }
                oAddresscontrol.Dispose();
                oAddresscontrol = null;
            }
            oAddresscontrol = new gloAddressControl(_databaseconnectionstring);
            //Bug #70411: Enter zip code and Email ID save and close : Send Patient portal Invitation check box is net checked automatically and No Invitation mail is going
            //added code to checked check box to send patient portal invitation mail.
            oAddresscontrol.txtZip.TextChanged += new EventHandler(txtZip_TextChanged1);

            //7022Items: Home Billing
            //Added to check setting from Settings table for USEAREACODEFORPATIENT if value =1(true) then show atra code textbox on patient registration form else not.
            #region "Setting an Area Code for Patient Address "

            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);

            object oUseAreaCode = null;
            oSettings.GetSetting("USEAREACODEFORPATIENT", out oUseAreaCode);

            //7022Items: Home Billing
            //check for country only for US to disply area code.
            oAddresscontrol.UseAreaCodeForPatient = Convert.ToBoolean(Convert.ToInt16(oUseAreaCode));
            oAddresscontrol.SetAreaCode();
            oSettings.Dispose();
            oSettings = null;

            #endregion

            oAddresscontrol.Name = "DemographicsAddressControl";
            pnlAddresssControl.Controls.Add(oAddresscontrol);
            try
            {
                fillMaritalStatus();
                fillRace();
                fillCommunicationPrefence(); //Communication Preferece
                fillHandDomain();
                fillLocations();
                fillStates();
                fillRelationships();
                fillLang();
                fillEthnicities();
                FillGender();
                isFormLoading = true;
                //dtpPADOB.MaxDate = DateTime.Now;   
                //Code Added by Mayuri:20090923

                //Added by SaiKrishna:2010-12-31(yyyy-mm-dd)
                cmbSameAsGuardian.Items.Add("Patient");
                IsCmbSameAsGuardianLoadFlag = false;
                cmbSameAsGuardian.SelectedIndex = 0;
                IsCmbSameAsGuardianLoadFlag = true;
                if (_IsPatientAccountFeature == false)
                {
                    lblAccountNo.Visible = false;
                    txtAccountNo.Visible = false;
                    lblAccountDescription.Visible = false;
                    txtAccountDescription.Visible = false;
                    rbAccountNew.Visible = false;
                    rbAccountExisting.Visible = false;
                    lblAccount.Visible = false;
                    lblAccMandatory.Visible = false;
                    lblGuarMandatory.Visible = false;
                    pnlGuarantorDetails.Top = pnlGuarantorDetails.Top - 50;
                    if (_PatientId == 0)
                    {
                        pnlPatientOtherGuarantorInfo.Top = pnlPatientOtherGuarantorInfo.Top - 28;
                    }
                }


                if (_PatientId == 0)
                {
                    //txtPACode.ReadOnly = true;
                    //txtPACode.Focus();
                    //Select US as default country
                    //if (cmbPACountry.Items.Count > 0)
                    //{
                    //    cmbPACountry.SelectedIndex = 0;
                    //}

                    cmbPACountry.SelectedValue = _Country;
                    if (_Country == "Canada")
                    {
                        lblState.Text = "Province :";
                        Point pt = new Point(168, 57);
                        lblState.Location = pt;

                        txtPACounty.Visible = false;
                        lblCountry.Visible = false;
                        txtPAZip.MaxLength = 6;


                    }
                    pnlAddDetails.BorderStyle = BorderStyle.None;
                    label1.Visible = false;


                    GeneratePatientCode();
                    SetDefaultProvider();
                    Set_DefaultPatientGender();
                    //Date time 
                    dtpBirth.Text = "00:00:00";
                    dtpBirth.Checked = false;

                    //Added by SaiKrishna:2010-12-31(yyyy-mm-dd)
                    if (_IsPatientAccountFeature == true)
                    {
                        rbAccountNew.Visible = true;
                        rbAccountExisting.Visible = true;
                    }
                    btnGuarantorClear.Visible = false;
                    lblBusinessCenter.Visible = false;
                }
                else
                {
                    txtPACode.ReadOnly = false;
                    //if (_IsSaveAsCopy == true)
                    //{
                    //    txtPACode.ReadOnly = false;
                    //}
                    //else
                    //{
                    //    txtPACode.ReadOnly = true;
                    //}

                    if (_IsSaveAsCopy == true)
                    {
                        IsInsuranceModified = true;   //condition checked by Mayuri:20110120-to fix issue:#7767
                        //Copy Patient >> if the patient having insurance & if we copy of that patient >> its not showing the insurance details for copy patient
                        txtPACode.Focus();
                    }
                    else
                    {
                        IsInsuranceModified = false;
                        txtmPASSN.Focus();
                        txtmPASSN.Select();
                    }

                    //Added by SaiKrishna:2011-06-27(yyyy-mm-dd)
                    if (_IsPatientAccountFeature == true)
                    {
                        rbAccountNew.Visible = false;
                        rbAccountExisting.Visible = false;

                        IsCopyAccountFeature = objgloAccount.GetCopyAccountFeatureSetting();
                        if (IsCopyAccountFeature == true)
                            btnCopyAccount.Visible = true;

                        btnAddAccount.Visible = true;
                        btnEditAccount.Visible = true;
                        lblGuarantorDetails.Visible = true;
                        lblGuarantorDetails.Text = "";
                        txtAccountDescription.Text = "";
                        txtAccountDescription.ReadOnly = true;
                        txtAccountDescription.Enabled = false;
                        txtAccountNo.Text = "";
                        lblBusinessCenter.Text = "";

                        cmbAccounts.Visible = true;

                        txtAccountNo.Visible = false;
                        btnGuarantorExistingPatientBrowse.Visible = false;
                        btnNewGuarantor.Visible = false;
                        btnGuarantorClear.Visible = false;
                        chkExcludefromStatement.Visible = false;
                        chkSetToCollection.Visible = false;
                        lblSameAsGuardian.Visible = false;
                        cmbSameAsGuardian.Visible = false;

                        lblGuarantorDetails.Height = 53;
                        lblGuarantorDetails.Width = 275;

                        txtAccGuarantor.Visible = false;
                    }
                    lblBusinessCenter.Visible = true;
                    cmbBusinessCenter.Visible = false;


                    pnlPatientOtherGuarantorInfo.Top = pnlPatientOtherGuarantorInfo.Top - 28;
                    //txtPACode.Focus();
                    SetData();
                    //Added by SaiKrishna:2011-06-27(yyyy-mm-dd)for SaveAsCopyPatient regarding patient account feature
                    if (_IsSaveAsCopy == true)
                    {
                        this._Id = _PatientId;
                        this.GetAccountDataForSaveAsCopyPatient();
                    }


                    switch (_ModificationDetail)
                    {
                        case ModifyPatientDetailType.None:
                            break;
                        case ModifyPatientDetailType.Insurance:
                            btnInsurInfo_Click(null, null);
                            break;
                        case ModifyPatientDetailType.Guarantor:
                            break;
                        case ModifyPatientDetailType.Guardian:
                            break;
                        case ModifyPatientDetailType.Occupation:
                            break;
                        case ModifyPatientDetailType.OtherInfo:
                            btnOtherDetails_Click(null, null);
                            break;
                        case ModifyPatientDetailType.Referral:
                            _IsReferralClick = true;
                            //btn_PAReferralsBr_Click(null, null);
                            break;
                        default:
                            break;
                    }
                }

                //Check whether Business Center Reuired on Accounts or no
                if (_IsRequireBusinessCenterOnPAccounts && _IsPatientAccountFeature)
                {
                    pnlBusinessCenter.Visible = true;
                    if (_PatientId == 0)
                    {
                        FillBusinessCenter();
                        Int64 _DefaultBusinessCenter = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
                        if (_DefaultBusinessCenter != 0)
                        {
                            cmbBusinessCenter.SelectedValue = _DefaultBusinessCenter;
                        }
                        if (cmbBusinessCenter.Items.Count > 0)
                        {
                            if (cmbBusinessCenter.SelectedIndex == -1)
                            {
                                cmbBusinessCenter.SelectedIndex = 0;
                            }
                        }
                    }

                }
                else
                {
                    pnlBusinessCenter.Visible = false;
                }


                //Check allow PatientOtherguarantors admin switch
                _IsAllowMultipleGuarantors = objgloAccount.GetAllowMultipleGuarantorsFeatureSetting();
                if (_IsAllowMultipleGuarantors == true)
                {
                    //if (oPatientOtherGuarantors != null && oPatientOtherGuarantors.Count == 0)
                    //{
                    //    pnlPatientOtherGuarantorInfo.Visible = false;
                    //}
                    //else
                    //{
                    //    pnlPatientOtherGuarantorInfo.Visible = true;
                    //}
                    pnlPatientOtherGuarantorInfo.Visible = true;
                }
                else
                {
                    if (oPatientOtherGuarantors != null && oPatientOtherGuarantors.Count == 0)
                    {
                        pnlPatientOtherGuarantorInfo.Visible = false;
                    }
                    else
                    {
                        pnlPatientOtherGuarantorInfo.Visible = true;
                    }
                }
                //MaheshB
                if (_PatientId == 0)
                {
                    chkSignatureOnFile.Checked = true;
                }
                else
                {
                }
                chkYesNoLab_CheckedChanged(null, null);
                timer1.Interval = 1000;
                timer1.Start();


                if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures )
                {
                    btn_Race.Visible = true;
                    btn_RaceDel.Visible = true;
                    txtPAMName.MaxLength = 50;
                    btn_Ethnicity.Visible = true;
                    btn_EthnicityDel.Visible = true;
                }
                
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception  ex)
            {
                //ex.ToString();
                //ex = null;
                MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isFormLoading = false;
            }
        }
        bool semaPhoreTosuppressValueChange = false;
        void TrackBarZoomChange(object sender, gloPictureEventArgs e)
        {  
            semaPhoreTosuppressValueChange = true;
            if (isActivatedWebCam)
            {
               
                myNewTrackBar.Value = myPictureBox.ZoomValueForTrackBar;
              
            }
            else
            {
                myNewTrackBar.Value = picPAPhoto.ZoomValueForTrackBar;
            }
              semaPhoreTosuppressValueChange = false;
              //Added for Bug #77539: 00000115 : Patient Setup
              _ispicPAPhotomodified = true;
        }
        //Patient Portal
        //Bug #70411: Enter zip code and Email ID save and close : Send Patient portal Invitation check box is net checked automatically and No Invitation mail is going
        //added code to checked check box to send patient portal invitation mail.
        void txtZip_TextChanged1(object sender, EventArgs e)
        {
            ValidatePortalSendActivationEmail();
            ValidateAPISendActivationEmail();
        }
        //Patient Portal

        //event to change buttons color on MouseOver 
        private void btn_PAPhotoBrowse_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
           
        }

        //event to change buttons color on MouseLeave 
        private void btn_PAPhotoBrowse_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongButton;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
           
        }

        private void txtPAZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
                //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
                //{
                //    e.Handled = true;
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        //To fill the City,State,County according to zip Code
        private void txtPAZip_Leave(object sender, EventArgs e)
        {
            if (txtPAZip.Text.Trim() != "" && Regex.IsMatch(txtPAZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = null;
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = " + txtPAZip.Text.Trim() + "";
                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbPAState.Text = Convert.ToString(dt.Rows[0]["ST"]);

                        if (txtPACity.Text.Trim() == "")
                            txtPACity.Text = Convert.ToString(dt.Rows[0]["City"]);

                        txtPACounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                        cmbPACountry.Text = "US";
                    }
                    else
                    {
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                    oDb.Disconnect();
                    oDb.Dispose();
                }
            }
        }


        private PatientOtherContact.GuarantorTypeFlag GetNextTypeFlag(bool CallFromSameAsPatient)
        {
            PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.None;

            bool isPrimaryPresent = false;
            bool isSecondaryPresent = false;
            bool isTertioryPresent = false;

            if  ((oPatientGuarantors!=null)   &&  (oPatientGuarantors.Count != 0)) //null condition checked for copy patient issue
            {
                for (int i = 0; i < oPatientGuarantors.Count; i++)
                {
                    if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode())
                    { isPrimaryPresent = true; }
                    else if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode())
                    { isSecondaryPresent = true; }
                    else if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode())
                    { isTertioryPresent = true; }

                    //if (oPatientGuarantors[i].nGuarantorTypeFlag > _GuarantorTypeFlag.GetHashCode())
                    //{ _GuarantorTypeFlag = (PatientOtherContact.GuarantorTypeFlag)(oPatientGuarantors[i].nGuarantorTypeFlag); }
                }

                if (!isPrimaryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary; }
                else if (!isSecondaryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Secondary; }
                else if (!isTertioryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary; }
                else
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary; }
            }
            else
            {
                _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary;
            }

            //if (oPatientGuarantors.Count != 0)
            //{
            //    for (int i = 0; i < oPatientGuarantors.Count; i++)
            //    {
            //        //if (oPatientGuarantors[i].nGuarantorTypeFlag > _GuarantorTypeFlag.GetHashCode())
            //        //{ _GuarantorTypeFlag = (PatientOtherContact.GuarantorTypeFlag)(oPatientGuarantors[i].nGuarantorTypeFlag); }

            //        if (CallFromSameAsPatient)
            //        {
            //            if (oPatientGuarantors[i].FirstName.Trim().ToUpper() == Convert.ToString(txtPAFname.Text).ToUpper()
            //                    && oPatientGuarantors[i].MiddleName.Trim().ToUpper() == Convert.ToString(txtPAMName.Text).ToUpper()
            //                    && oPatientGuarantors[i].LastName.Trim().ToUpper() == Convert.ToString(txtPALName.Text).ToUpper())
            //            {
            //                _GuarantorTypeFlag = (PatientOtherContact.GuarantorTypeFlag)(oPatientGuarantors[i].nGuarantorTypeFlag);
            //                break; 
            //            }
            //            else
            //            {
            //                if (oPatientGuarantors[i].nGuarantorTypeFlag > _GuarantorTypeFlag.GetHashCode())
            //                { _GuarantorTypeFlag = (PatientOtherContact.GuarantorTypeFlag)(oPatientGuarantors[i].nGuarantorTypeFlag); }
            //            }
            //        }
            //        else
            //        {
            //            if (oPatientGuarantors[i].nGuarantorTypeFlag > _GuarantorTypeFlag.GetHashCode())
            //            { _GuarantorTypeFlag = (PatientOtherContact.GuarantorTypeFlag)(oPatientGuarantors[i].nGuarantorTypeFlag); }
            //        }
            //    }

            //    if (_GuarantorTypeFlag.GetHashCode() < 4)
            //    { _GuarantorTypeFlag = (PatientOtherContact.GuarantorTypeFlag)(_GuarantorTypeFlag.GetHashCode() + 1); }
            //    else
            //    { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Inactive; }
            //}
            //else
            //{ _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary; }


            return _GuarantorTypeFlag;
        }

        #endregion


        private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        #region "Email Address Validation"

        public bool CheckEmailAddress(string input)
        {
            bool response = false;

            gloGlobal.RegexUtilities util = new gloGlobal.RegexUtilities();

            try
            {
                //31-May-16 Aniket: Resolving Bug #95905 ( Modified): gloEMR: Email address in patient demographics accepts multiple "@"
                if (util.IsValidEmail(input) || input.Trim() == "")
                {
                    response = true;
                }
                else
                {
                    response = false;
                }
                return response;
            }
            catch
            {
                return response;
            }

            finally
            {
                if (util != null)
                {
                    util = null;
                }
            }
        }

        private void txtPAEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtPAEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        #endregion

        

        private void rbWebCam_CheckedChanged(object sender, EventArgs e)
        {

            if (rbWebCam.Checked == true)
            {
                rbWebCam.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbWebCam.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }
            //Added for Bug #77539: 00000115 : Patient Setup
            _ispicPAPhotomodified = true;
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongButton;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
           
        }

        
        private void gloPatientDemographicsControl_Paint(object sender, PaintEventArgs e)
        {
            if (_IsReferralClick == true)
            {
                _IsReferralClick = false;
                btn_PAReferralsBr_Click(null, null);
            }
        }
       

        //Communication Preference
        private void fillCommunicationPrefence()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                if (oDB != null)
                {
                    if (oDB.Connect(false))
                    {
                        ////string itm="";
                        try
                        {
                            DataTable dtCommunicationPrefence = null;
                            // Logic changed as per discussion with Pramod on 20/01/2010 - to fetch LANGUAGES from Category only - by Ujwala Atre                
                            //string _sqlQuery = "SELECT id, name FROM languages order by name";
                            string _sqlQuery = "SELECT  nCategoryid id,sDescription name FROM category_mst WHERE UPPER(sCategoryType) ='Communication Preference' AND IsNull(bIsBlocked,0) = 0 AND nClinicID = " + _ClinicID + " order by sDescription ";
                            // Logic changed as per discussion with Pramod on 20/01/2010 - to fetch LANGUAGES from Category only - by Ujwala Atre                
                            oDB.Retrive_Query(_sqlQuery, out dtCommunicationPrefence);
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }

                            if (dtCommunicationPrefence != null)
                            {
                                DataRow dr = dtCommunicationPrefence.NewRow(); //Adding the new row int the Datatable
                                dr["id"] = 0;
                                dr["name"] = "";
                                dtCommunicationPrefence.Rows.InsertAt(dr, 0); //Need to be inserted in the speicfied location
                                dtCommunicationPrefence.AcceptChanges();  //After adding the row in the datatable accept the changes

                                cmbCommPref.DataSource = dtCommunicationPrefence;  //Binding the Datasource to the 

                                cmbCommPref.DisplayMember = "name";
                                //cmbCommPref.SelectedIndex  = cmbPALang.FindString("English") ;
                            }
                        }
                        catch (gloDatabaseLayer.DBException ex)
                        {
                            ex.ERROR_Log(ex.ToString());
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                        }
                        finally
                        {
                            ////cmbCommPref.Text = itm;
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                        }
                    }//odbConnectFalse
                }
            }
            catch
            {
            }
            finally
            {
               
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        private void fillCountry()
        {

            try
            {

                DataTable dtCountry = null;
                dtCountry = gloGlobal.gloPMMasters.GetCounrty();

                if (dtCountry != null)
                {
                    DataRow dr = dtCountry.NewRow();
                    dr["sCode"] = "";
                    dr["sSubCode"] = "";
                    dr["sName"] = "";
                    dr["sStateLabel"] = "State";
                    dtCountry.Rows.InsertAt(dr, 0);
                    dtCountry.AcceptChanges();

                    cmbPACountry.DataSource = dtCountry;
                    cmbPACountry.DisplayMember = "sName";
                    cmbPACountry.ValueMember = "sCode";

                    cmbPACountry.BeginUpdate();
                    cmbPACountry.SelectedIndex = -1;
                    cmbPACountry.EndUpdate();
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

            }

        }

        ////////////
        //Sandip Darade 20090827
        //Zip control implemented 
        #region "Zip control implemented  "
        bool isFormLoading = false;

        private gloZipcontrol oZipcontrol;
      //  private bool isSearchControlOpen = false;
        //enumZipTextType _ZipTextType;
        private string _TempZipText;
        private bool _isZipItemSelected = false;
        private bool _isTextBoxLoading = false;
        private ToolTip oToolTip = new ToolTip();
        #region " ZIP Text Events "

        private void txtZip_GotFocus(object sender, System.EventArgs e)
        {
            try
            {
                //if (_ZipTextType != enumZipTextType.PatientZip) {
                _TempZipText = txtPAZip.Text.Trim();

                //}
            }
            catch
            {
            }
        }

        private void txtZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down | e.KeyCode == Keys.Up)
                {
                    //' HITS UP / DOWN ''
                    if (pnlInternalControl.Visible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        oZipcontrol.C1GridList.Focus();
                        oZipcontrol.C1GridList.Select(oZipcontrol.C1GridList.RowSel, 0);
                    }
                }
            }
            catch
            {
            }
        }

        private void txtZip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                //_ZipTextType = enumZipTextType.PatientZip;
                if (e.KeyChar == Convert.ToChar(13))
                {
                    //' HITS ENTER BUTTON ''
                    if (pnlInternalControl.Visible)
                    {

                        oZipcontrol_ItemSelected(null, null);
                    }
                }
                else if (e.KeyChar == Convert.ToChar(27))
                {
                    //' HITS ESCAPE ''

                    if (txtPACity.Text == "" && txtPACounty.Text == "" && txtPAZip.Text == "")
                    //if ( txtPAZip.Text == "")
                    {
                        _TempZipText = txtPAZip.Text;

                    }
                    txtPACity.Focus();
                }
                //Sandip Darade 200090912
                //we are allowing only alphanumeric charactors for according referring the information from the link below  
                // http://www.postalcodedownload.com/
                //The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
                //an alphabetic character and "N" represents a numeric character. 

                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void txtZip_LostFocus(object sender, System.EventArgs e)
        {
            if (oZipcontrol != null)
            {
                if (_isZipItemSelected == false & oZipcontrol.C1GridList.Focused == false & oZipcontrol.Focused == false)
                {
                    _isTextBoxLoading = true;
                    txtPAZip.Text = _TempZipText;
                    if (txtPACity.Text == "" && txtPACounty.Text == "" && txtPAZip.Text == "")
                    {
                        _TempZipText = txtPAZip.Text;

                    }
                    pnlInternalControl.Visible = false;
                    _isTextBoxLoading = false;
                }
            }
        }

        private void txtZip_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                //_ZipTextType = enumZipTextType.PatientZip;
                pnlInternalControl.BringToFront();

                if (isFormLoading == false & _isTextBoxLoading == false)
                {
                    if (pnlInternalControl.Visible == false)
                    {
                        pnlInternalControl.Visible = true;
                        OpenInternalControl(gloGridListControlType.ZIP, "Zip", false, 0, 0, "");
                        oZipcontrol.FillControl(Convert.ToString(txtPAZip.Text.Trim()));
                    }
                    else
                    {
                        oZipcontrol.FillControl(Convert.ToString(txtPAZip.Text.Trim()));
                    }
                }
            }
            catch
            {
            }
            finally
            {
                //_TempZipText = txtPAZip.Text;
            }
        }



        private void oZipcontrol_ItemSelected(object sender, EventArgs e)
        {


            try
            {
                if (oZipcontrol.C1GridList.Row < 0)
                {
                    return;
                }
                string _Zip = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 0).ToString();
                string _City = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 1).ToString();
                string _ID = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2).ToString();
                string _County = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 3).ToString();
                string _State = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 4).ToString();
                string _AreaCode = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 5).ToString();

                _isTextBoxLoading = true;
                //switch (_ZipTextType) {
                //    case enumZipTextType.PatientZip:
                txtPAZip.Text = _Zip;
                txtPAZip.Tag = _ID;
                txtPACity.Text = _City;
                txtPACity.Tag = _AreaCode;
                txtPACounty.Text = _County;
                cmbPAState.Text = _State;

                //break;
                //case enumZipTextType.WorkZip:
                //    txtwZip.Text = _Zip;
                //    txtwZip.Tag = _ID;
                //    txtwCity.Text = _City;
                //    txtwCity.Tag = _AreaCode;
                //    cmbwState.Text = _State;
                //    cmbwState.Tag = _County;

                //    break;

                //case enumZipTextType.MotherZip:

                //    txtMother_Zip.Text = _Zip;
                //    txtMother_Zip.Tag = _ID;
                //    txtMother_City.Text = _City;
                //    txtMother_City.Tag = _AreaCode;
                //    txtMother_County.Text = _County;
                //    cmbMother_State.Text = _State;
                //    break;
                //case enumZipTextType.GuardianZip:

                //    txtGuardian_Zip.Text = _Zip;
                //    txtGuardian_Zip.Tag = _ID;
                //    txtGuardian_City.Text = _City;
                //    txtGuardian_City.Tag = _AreaCode;
                //    txtGuardian_County.Text = _County;
                //    cmbGuardian_State.Text = _State;

                //    break;
                //case enumZipTextType.FatherZip:

                //    txtFather_Zip.Text = _Zip;
                //    txtFather_Zip.Tag = _ID;
                //    txtFather_City.Text = _City;
                //    txtFather_City.Tag = _AreaCode;
                //    txtFather_County.Text = _County;
                //    cmbFather_State.Text = _State;

                //    break;
                //case enumZipTextType.InsuranceZip:
                //    txtInsZip.Text = _Zip;
                //    txtInsZip.Tag = _ID;
                //    txtInsCity.Text = _City;
                //    txtInsCity.Tag = _AreaCode;
                //    txtInsCounty.Text = _County;
                //    cmbInsState.Text = _State;
                //break;
                //}
                _isTextBoxLoading = false;
                _isZipItemSelected = true;
                if (pnlInternalControl.Visible == true)
                {
                    pnlInternalControl.Visible = false;
                    txtPACity.Focus();
                }
                //else if (pnlWInternalControl.Visible == true) {
                //    pnlWInternalControl.Visible = false;
                //    txtwCity.Focus();
                //}
                //else if (pnlIIntrernalControl.Visible == true) {
                //    pnlIIntrernalControl.Visible = false;
                //    txtInsCity.Focus();
                //}
                //else if (pnlMInternalControl.Visible == true) {
                //    pnlMInternalControl.Visible = false;
                //    txtMother_City.Focus();
                //}
                //else if (pnlFInternalControl.Visible == true) {
                //    pnlFInternalControl.Visible = false;
                //    txtFather_City.Focus();
                //}
                //else if (pnlGInternalControl.Visible == true) {
                //    pnlGInternalControl.Visible = false;
                //    txtGuardian_City.Focus();
                //}
                //_ZipTextType = enumZipTextType.None;
             //   isSearchControlOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void oZipcontrol_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        private void oZipcontrol_AddBtnClick(object sender, System.EventArgs e)
        {

            try
            {
                //switch (_ZipTextType) {
                //    case enumZipTextType.PatientZip:
                if (!string.IsNullOrEmpty(txtPACity.Text))
                {
                    AddCity(txtPACity.Text.Trim(), cmbPAState.SelectedText.Trim(), txtPAZip.Text.Trim(), "0", txtPACounty.Text.Trim());
                    pnlInternalControl.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPACity.Focus();
                }


                //    break;
                //case enumZipTextType.WorkZip:
                //    if (!string.IsNullOrEmpty(txtwCity.Text)) {
                //        AddCity(txtwCity.Text.Trim, cmbwState.SelectedText.Trim, txtwZip.Text.Trim(), 0, "");
                //        pnlWInternalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtwCity.Focus();
                //    }

                //    break;
                //case enumZipTextType.MotherZip:
                //    if (!string.IsNullOrEmpty(txtMother_City.Text)) {
                //        AddCity(txtMother_City.Text.Trim(), cmbMother_State.SelectedText.Trim, txtMother_Zip.Text.Trim(), 0, txtMother_County.Text.Trim());
                //        pnlMInternalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtCity.Focus();
                //    }

                //    break;
                //case enumZipTextType.GuardianZip:
                //    if (!string.IsNullOrEmpty(txtGuardian_City.Text)) {
                //        AddCity(txtGuardian_City.Text.Trim(), cmbGuardian_State.SelectedText.Trim(), txtGuardian_Zip.Text.Trim(), 0, txtMother_County.Text.Trim());
                //        pnlGInternalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtCity.Focus();
                //    }

                //    break;
                //case enumZipTextType.FatherZip:
                //    if (!string.IsNullOrEmpty(txtFather_City.Text)) {
                //        AddCity(txtFather_City.Text.Trim(), cmbFather_State.SelectedText.Trim, txtFather_Zip.Text.Trim, 0, txtFather_County.Text.Trim);
                //        pnlFInternalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtCity.Focus();
                //    }

                //    break;
                //case enumZipTextType.InsuranceZip:
                //    if (!string.IsNullOrEmpty(txtInsCity.Text)) {
                //        AddCity(txtInsCity.Text.Trim, cmbInsState.SelectedText.Trim, txtInsZip.Text.Trim, 0, txtInsCounty.Text.Trim);
                //        pnlIIntrernalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtCity.Focus();
                //    }

                //    break;
                //}
         //       isSearchControlOpen = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void oZipcontrol_CloseBtnClick1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.pnlInternalControl.Visible == true)
                {
                    this.pnlInternalControl.Visible = false;
                }
                //else if (this.pnlWInternalControl.Visible == true) {
                //    this.pnlWInternalControl.Visible = false;
                //}
                //else if (this.pnlIIntrernalControl.Visible == true) {
                //    this.pnlIIntrernalControl.Visible = false;
                //}
                //else if (this.pnlMInternalControl.Visible == true) {
                //    this.pnlMInternalControl.Visible = false;
                //}
                //else if (this.pnlFInternalControl.Visible == true) {
                //    this.pnlFInternalControl.Visible = false;
                //}
                //else if (this.pnlGInternalControl.Visible == true) {
                //    this.pnlGInternalControl.Visible = false;
                //}
      //          isSearchControlOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //private void  oZipcontrol_LostFocus(object sender, System.EventArgs e)
        //{
        //    switch (_ZipTextType) {
        //        case enumZipTextType.PatientZip:
        //            if (txtZip.Focused == false) txtZip_LostFocus(null, null); 

        //            break;
        //        case enumZipTextType.WorkZip:
        //            if (txtwZip.Focused == false) txtwZip_LostFocus(null, null); 

        //            break;
        //        case enumZipTextType.MotherZip:
        //            if (txtMother_Zip.Focused == false) txtMother_Zip_LostFocus(null, null); 

        //            break;
        //        case enumZipTextType.FatherZip:
        //            if (txtFather_Zip.Focused == false) txtFather_Zip_LostFocus(null, null); 

        //            break;
        //        case enumZipTextType.GuardianZip:
        //            if (txtGuardian_Zip.Focused == false) txtGuardian_Zip_LostFocus(null, null); 

        //            break;
        //        case enumZipTextType.InsuranceZip:
        //            if (txtInsZip.Focused == false) txtInsZip_LostFocus(null, null); 
        //            break;
        //    }
        //}

        private void oZipcontrol_ModifyBtnClick(object sender, System.EventArgs e)
        {

            try
            {
                Int64 nZipID = default(Int64);
                nZipID = Convert.ToInt64(oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2));
                //Update the city against this zipcode in the db
                //switch (_ZipTextType) {
                //    case enumZipTextType.PatientZip:
                if (!string.IsNullOrEmpty(txtPACity.Text))
                {
                    UpdateCity(txtPACity.Text.Trim(), txtPAZip.Text.Trim(), nZipID);
                    pnlInternalControl.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPACity.Focus();
                }

                //    break;
                //case enumZipTextType.WorkZip:
                //    if (!string.IsNullOrEmpty(txtwCity.Text)) {
                //        UpdateCity(txtwCity.Text.Trim, txtwZip.Text.Trim, nZipID);
                //        pnlWInternalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtwCity.Focus();
                //    }


                //    break;
                //case enumZipTextType.MotherZip:
                //    if (!string.IsNullOrEmpty(txtMother_City.Text)) {
                //        UpdateCity(txtMother_City.Text.Trim, txtMother_Zip.Text.Trim, nZipID);
                //        pnlMInternalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtMother_City.Focus();
                //    }


                //    break;
                //case enumZipTextType.GuardianZip:
                //    if (!string.IsNullOrEmpty(txtGuardian_City.Text)) {
                //        UpdateCity(txtGuardian_City.Text.Trim, txtGuardian_Zip.Text.Trim, nZipID);
                //        pnlGInternalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtGuardian_City.Focus();
                //    }


                //    break;
                //case enumZipTextType.FatherZip:
                //    if (!string.IsNullOrEmpty(txtFather_City.Text)) {
                //        UpdateCity(txtFather_City.Text.Trim, txtFather_Zip.Text.Trim, nZipID);
                //        pnlFInternalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtFather_City.Focus();
                //    }

                //    break;
                //case enumZipTextType.InsuranceZip:
                //    if (!string.IsNullOrEmpty(txtInsCity.Text)) {
                //        UpdateCity(txtInsCity.Text.Trim, txtInsZip.Text.Trim, nZipID);
                //        pnlIIntrernalControl.Visible = false;
                //    }
                //    else {
                //        MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtInsCity.Focus();
                //    }

                //    break;
                //}



        //       isSearchControlOpen = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            _isZipItemSelected = false;
            try
            {

                if (oZipcontrol != null)
                {
                    CloseInternalControl();
                }
                oZipcontrol = new gloZipcontrol(ControlType, false, 0, 0, 0, _databaseconnectionstring);
                oZipcontrol.ItemSelectedclick += oZipcontrol_ItemSelected;
                oZipcontrol.InternalGridKeyDownclick += oZipcontrol_InternalGridKeyDown;
                //AddHandler oZipcontrol.CloseBtnClick, AddressOf oZipcontrol_CloseBtnClick
                oZipcontrol.ControlHeader = ControlHeader;
                oZipcontrol.ShowHeader = false;

                //switch (_ZipTextType)
                //{
                //    case enumZipTextType.PatientZip:
                oZipcontrol.Dock = DockStyle.Fill;
                pnlInternalControl.BringToFront();
                pnlInternalControl.Visible = true;
                pnlInternalControl.Controls.Add(oZipcontrol);

                //    break;
                //case enumZipTextType.WorkZip:
                //    oZipcontrol.Dock = DockStyle.Fill;
                //    pnlWInternalControl.BringToFront();
                //    pnlWInternalControl.Visible = true;
                //    pnlWInternalControl.Controls.Add(oZipcontrol);

                //    break;
                //case enumZipTextType.InsuranceZip:
                //    oZipcontrol.Dock = DockStyle.Fill;
                //    pnlIIntrernalControl.BringToFront();
                //    pnlIIntrernalControl.Visible = true;
                //    pnlIIntrernalControl.Controls.Add(oZipcontrol);

                //    break;
                //case enumZipTextType.MotherZip:
                //    oZipcontrol.Dock = DockStyle.Fill;
                //    pnlMInternalControl.BringToFront();
                //    pnlMInternalControl.Visible = true;
                //    pnlMInternalControl.Controls.Add(oZipcontrol);

                //    break;
                //case enumZipTextType.FatherZip:
                //    oZipcontrol.Dock = DockStyle.Fill;
                //    pnlFInternalControl.BringToFront();
                //    pnlFInternalControl.Visible = true;
                //    pnlFInternalControl.Controls.Add(oZipcontrol);

                //    break;
                //case enumZipTextType.GuardianZip:
                //    oZipcontrol.Dock = DockStyle.Fill;
                //    pnlGInternalControl.BringToFront();
                //    pnlGInternalControl.Visible = true;
                //    pnlGInternalControl.Controls.Add(oZipcontrol);
                //    break;
                //}
                //pnlInternalControl.Controls.Add(oZipcontrol)



                //pnlInternalControl.Controls.Add(oZipcontrol)

                //oZipcontrol.Dock = DockStyle.Fill
                //pnlInternalControl.BringToFront()
                //pnlInternalControl.Visible = True

                if (!string.IsNullOrEmpty(SearchText))
                {
                    oZipcontrol.Search(SearchText, SearchColumn.Code);
                }
                oZipcontrol.Show();
                _result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {

            }

       //     isSearchControlOpen = true;
            return _result;
        }

        private bool CloseInternalControl()
        {
            if (oZipcontrol != null)
            {

                _isTextBoxLoading = true;
                //switch (_ZipTextType)
                //{
                //    case enumZipTextType.PatientZip:
                //SLR: Changed on 4/2/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0;  i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                try
                {
                    oZipcontrol.ItemSelectedclick -= oZipcontrol_ItemSelected;
                    oZipcontrol.InternalGridKeyDownclick -= oZipcontrol_InternalGridKeyDown;
                }
                catch
                {
                }

                //    break;

                //case enumZipTextType.WorkZip:
                //    for (int i = 0; i <= pnlWInternalControl.Controls.Count - 1; i++)
                //    {
                //        pnlWInternalControl.Controls.RemoveAt(i);
                //    }


                //    break;
                //case enumZipTextType.MotherZip:
                //    for (int i = 0; i <= pnlMInternalControl.Controls.Count - 1; i++)
                //    {
                //        pnlMInternalControl.Controls.RemoveAt(i);
                //    }


                //    break;
                //case enumZipTextType.FatherZip:
                //    for (int i = 0; i <= pnlFInternalControl.Controls.Count - 1; i++)
                //    {
                //        pnlFInternalControl.Controls.RemoveAt(i);
                //    }


                //    break;
                //case enumZipTextType.GuardianZip:
                //    for (int i = 0; i <= pnlGInternalControl.Controls.Count - 1; i++)
                //    {
                //        pnlGInternalControl.Controls.RemoveAt(i);
                //    }


                //    break;
                //case enumZipTextType.InsuranceZip:
                //    for (int i = 0; i <= pnlIIntrernalControl.Controls.Count - 1; i++)
                //    {
                //        pnlIIntrernalControl.Controls.RemoveAt(i);
                //    }


                //    break;
                //}

                if (oZipcontrol != null)
                {
                    oZipcontrol.Dispose();
                    oZipcontrol = null;
                }


                _isTextBoxLoading = false;

            }
            return _isTextBoxLoading;
        }

        private void UpdateCity(string sCity, string sZip, Int64 ID)
        {

            try
            {
                UpdateCity1(sCity, sZip, ID);

                if (!string.IsNullOrEmpty(txtPACity.Text.Trim()))
                {
                    if (txtPAZip.Text.Trim() == sZip)
                    {
                        txtPACity.Text = sCity;
                    }
                }

                //if (!string.IsNullOrEmpty(txtwCity.Text.Trim))
                //{
                //    if (txtwZip.Text.Trim == sZip)
                //    {
                //        txtwCity.Text = sCity;
                //    }
                //}

                //if (!string.IsNullOrEmpty(txtMother_City.Text.Trim))
                //{
                //    if (txtMother_Zip.Text == sZip)
                //    {
                //        txtMother_City.Text = sCity;
                //    }
                //}

                //if (!string.IsNullOrEmpty(txtFather_City.Text.Trim))
                //{
                //    if (txtFather_Zip.Text.Trim == sZip)
                //    {
                //        txtFather_City.Text = sCity;
                //    }
                //}

                //if (!string.IsNullOrEmpty(txtGuardian_City.Text.Trim))
                //{
                //    if (txtGuardian_Zip.Text.Trim == sZip)
                //    {
                //        txtGuardian_City.Text = sCity;
                //    }
                //}


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCityAgainstZip(string sCity, string sZip)
        {
            SqlConnection conn = new SqlConnection(_databaseconnectionstring);
            SqlCommand oCmd = default(SqlCommand);
            string _strSQL = "";


            try
            {

                _strSQL = "update csz_mst set city = '" + sCity.Replace("'", "''") + "' where zip = '" + sZip + "'";

                oCmd = new System.Data.SqlClient.SqlCommand();

                oCmd.Connection = conn;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = _strSQL;

                conn.Open();
                oCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                      

                       
                    }
                    conn.Dispose();
                    conn = null;
                }
                if ((oCmd != null))
                {
                    oCmd.Parameters.Clear();
                    oCmd.Dispose();
                    oCmd = null;
                }
            }
        }
        public void UpdateCity1(string sCity, string sZip, Int64 ID)
        {
            SqlConnection conn = new SqlConnection(_databaseconnectionstring);
            SqlCommand oCmd = default(SqlCommand);
            string _strSQL = "";

            try
            {

                _strSQL = "update csz_mst set city = '" + sCity.Replace("'", "''") + "' where zip = '" + sZip + "' AND nID = " + ID + "";

                oCmd = new SqlCommand();

                oCmd.Connection = conn;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = _strSQL;

                conn.Open();
                oCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();



                    }
                    conn.Dispose();
                    conn = null;
                }
                if ((oCmd != null))
                {
                    oCmd.Parameters.Clear();
                    oCmd.Dispose();
                    oCmd = null;
                }
            }
        }
        ////To add new city against provided zip code
        private void AddCity(string sCity, string sState, string sZip, string sAreaCode, string sCounty)
        {
            try
            {
                AddCity1(sCity, sState, sZip, Convert.ToInt64(sAreaCode), sCounty);

                if (!string.IsNullOrEmpty(txtPACity.Text.Trim()))
                {
                    if (txtPAZip.Text.Trim() == sZip)
                    {
                        txtPACity.Text = sCity;
                    }
                }

                //if (!string.IsNullOrEmpty(txtwCity.Text.Trim))
                //{
                //    if (txtwZip.Text.Trim == sZip)
                //    {
                //        txtwCity.Text = sCity;
                //    }
                //}

                //if (!string.IsNullOrEmpty(txtMother_City.Text.Trim))
                //{
                //    if (txtMother_Zip.Text == sZip)
                //    {
                //        txtMother_City.Text = sCity;
                //    }
                //}

                //if (!string.IsNullOrEmpty(txtFather_City.Text.Trim))
                //{
                //    if (txtFather_Zip.Text.Trim == sZip)
                //    {
                //        txtFather_City.Text = sCity;
                //    }
                //}

                //if (!string.IsNullOrEmpty(txtGuardian_City.Text.Trim))
                //{
                //    if (txtGuardian_Zip.Text.Trim == sZip)
                //    {
                //        txtGuardian_City.Text = sCity;
                //    }
                //}


                //objPatientReg = null;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public void AddCity1(string sCity, string sState, string sZip, Int64 sAreaCode, string sCounty)
        {

            SqlConnection conn = new SqlConnection(_databaseconnectionstring);
            SqlCommand oCmd = default(SqlCommand);
            string _strSQL = "";
            object _result = null;

            try
            {
                _strSQL = "SELECT MAX(ISNULL(nID,0)) +1 From csz_mst";

                oCmd = new SqlCommand();

                oCmd.Connection = conn;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = _strSQL;
                conn.Open();
                _result = oCmd.ExecuteScalar();


                _strSQL = "";
                _strSQL = "Insert into csz_mst (City,ST,Zip,Areacode,county,nID) values ('" + sCity.Replace("'", "''") + "','" + sState.Replace("'", "''") + "','" + sZip.Replace("'", "''") + "'," + sAreaCode + ",'" + sCounty.Replace("'", "''") + "'," + Convert.ToInt64(_result) + ")";
                // where zip = '" & sZip & "'"
                oCmd.Parameters.Clear();
                oCmd.Dispose();
                oCmd = null;

                oCmd = new SqlCommand();
                oCmd.Connection = conn;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = _strSQL;

                oCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();



                    }
                    conn.Dispose();
                    conn = null;
                }
                if ((oCmd != null))
                {
                    oCmd.Parameters.Clear();
                    oCmd.Dispose();
                    oCmd = null;
                }
            }
        }
        #endregion

        private void cmbPAHandDom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        /// <summary>
        /// Function fill Ethnicities combobox
        /// </summary>
        private void fillEthnicities()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtEthn = null;
            string _sqlQuery = string.Empty;
            try
            {
                // Logic changed as per discussion with Pramod on 20/01/2010 - to fetch Ethnicities from Category only - by Ujwala Atre                
                //string _sqlQuery = "SELECT id, name FROM Ethnicities order by name";
                if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                {
                    _sqlQuery = "SELECT -2 as id, 'Declined to specify' as name UNION SELECT -3 as id, 'Unknown' as name";

                }
                else
                {
                    _sqlQuery = "sELECT  nCategoryid id,sCode,sDescription name FROM category_mst WHERE UPPER(sCategoryType) ='ETHNICITY' AND bIsBlocked = '" + false + "' AND nClinicID = " + _ClinicID + " order by sDescription ";
                }
                // Logic changed as per discussion with Pramod on 20/01/2010 - to fetch Ethnicities from Category only - by Ujwala Atre                
                oDB.Retrive_Query(_sqlQuery, out dtEthn);
                oDB.Disconnect();

                if (dtEthn != null)
                {
                    DataRow dr = dtEthn.NewRow();

                    dr["id"] = 0;
                    dr["name"] = "";
                    dtEthn.Rows.InsertAt(dr, 0);
                    dtEthn.AcceptChanges();
                    if (!gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    {
                        DataRow dr1 = dtEthn.NewRow();
                        //if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                        //{
                            dr1["id"] = 1;
                            dr1["name"] = "Declined to specify";
                            dtEthn.Rows.InsertAt(dr1, 1);
                            dtEthn.AcceptChanges();
                        //}

                    }
                    cmbPAEthn.DataSource = dtEthn;
                    cmbPAEthn.DisplayMember = "name";
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
                if (oDB != null) { oDB.Dispose(); }
                // if (dtEthn != null) { dtEthn.Dispose (); }

            }


        }

        private void FillGender()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtBirthSex = null;
            try
            {
                // Logic changed as per discussion with Pramod on 20/01/2010 - to fetch Ethnicities from Category only - by Ujwala Atre                
                //string _sqlQuery = "SELECT id, name FROM Ethnicities order by name";
                string _sqlQuery = "SELECT  nCategoryid id,sCode,sDescription name FROM category_mst WHERE UPPER(sCategoryType) ='Gender' AND bIsBlocked = '" + false + "' AND nClinicID = " + _ClinicID + " order by sDescription ";
                // Logic changed as per discussion with Pramod on 20/01/2010 - to fetch Ethnicities from Category only - by Ujwala Atre                
                oDB.Retrive_Query(_sqlQuery, out dtBirthSex);
                oDB.Disconnect();

                if (dtBirthSex != null)
                {
                    DataRow dr = dtBirthSex.NewRow();

                    dr["id"] = 0;
                    dr["name"] = "";
                    dtBirthSex.Rows.InsertAt(dr, 0);
                    dtBirthSex.AcceptChanges();

                    //DataRow dr1 = dtEthn.NewRow();
                    //if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    //{
                    //    dr1["id"] = 1;
                    //    dr1["name"] = "Declined to specify";
                    //    dtEthn.Rows.InsertAt(dr1, 1);
                    //    dtEthn.AcceptChanges();
                    //}
                    cmbGender.BeginUpdate();                   
                    cmbGender.DataSource = dtBirthSex;
                    cmbGender.DisplayMember = "name";
                    cmbGender.EndUpdate();
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
                if (oDB != null) { oDB.Dispose(); }
                // if (dtEthn != null) { dtEthn.Dispose (); }

            }


        }
        /// <summary>
        /// Function Fill language combo box
        /// </summary>
        private void fillLang()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            ////string itm="";
            DataTable dtLang = null;
            try
            {
                // Logic changed as per discussion with Pramod on 20/01/2010 - to fetch LANGUAGES from Category only - by Ujwala Atre                
                //string _sqlQuery = "SELECT id, name FROM languages order by name";

                string _sqlQuery = "SELECT nCategoryid id, sDescription name FROM category_mst WHERE UPPER(sCategoryType) = 'LANGUAGE' AND bIsBlocked = '" + false + "' and bFavorites = 1 AND nClinicID = " + _ClinicID ;

                if (_PatientId != 0)
                {
                    _sqlQuery = _sqlQuery + " UNION SELECT nCategoryid id, sDescription name FROM category_mst WHERE UPPER(sCategoryType) = 'LANGUAGE' AND nClinicID = " + _ClinicID + " AND sDescription = (select sLang FROM Patient where nPatientid = " + _PatientId + " )";
                }
                _sqlQuery = _sqlQuery + " ORDER BY sDescription ";

                oDB.Retrive_Query(_sqlQuery, out dtLang);
                oDB.Disconnect();

                if (dtLang != null)
                {
                    DataRow dr = dtLang.NewRow();
                    dr["id"] = 0;
                    dr["name"] = "";
                    dtLang.Rows.InsertAt(dr, 0);
                    dtLang.AcceptChanges();

                    DataRow dr1 = dtLang.NewRow();
                    if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    {
                        dr1["id"] = 1;
                        dr1["name"] = "Declined to specify";
                        dtLang.Rows.InsertAt(dr1, 1);
                        dtLang.AcceptChanges();
                    }

                    DataRow dr2 = dtLang.NewRow();
                    if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    {
                        dr2["id"] = dtLang.Rows.Count + 3;
                        dr2["name"] = "More...";
                        dtLang.Rows.InsertAt(dr2, dtLang.Rows.Count + 3);
                        dtLang.AcceptChanges();
                    }

                    cmbPALang.DataSource = dtLang;
                    cmbPALang.DisplayMember = "name";
                    cmbPALang.SelectedIndex = cmbPALang.FindString("English");
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
                ////cmbPALang.Text = itm;
                if (oDB != null) { oDB.Dispose(); }
                //if (dtLang != null) { dtLang.Dispose(); }
            }
        }


        private void fillLang_All()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            ////string itm="";
            DataTable dtLang = null;
            try
            {
                // Logic changed as per discussion with Pramod on 20/01/2010 - to fetch LANGUAGES from Category only - by Ujwala Atre                
                
                //01-Nov-13: Aniket: Fixing issue where not all languages were not seen
                string _sqlQuery = "SELECT  nCategoryid id,sDescription name FROM category_mst WHERE UPPER(sCategoryType) ='LANGUAGE' AND IsNull(bIsBlocked,0) = 0  AND nClinicID = " + _ClinicID + " order by sDescription "; //and (bFavorites=0 or bFavorites is null)
                // Logic changed as per discussion with Pramod on 20/01/2010 - to fetch LANGUAGES from Category only - by Ujwala Atre                
                oDB.Retrive_Query(_sqlQuery, out dtLang);
                oDB.Disconnect();

                if (dtLang != null)
                {
                    DataRow dr = dtLang.NewRow();
                    dr["id"] = 0;
                    dr["name"] = "";
                    dtLang.Rows.InsertAt(dr, 0);
                    dtLang.AcceptChanges();

                    DataRow dr1 = dtLang.NewRow();
                    if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    {
                        dr1["id"] = 1;
                        dr1["name"] = "Declined to specify";
                        dtLang.Rows.InsertAt(dr1, 1);
                        dtLang.AcceptChanges();
                    }

                    DataRow dr2 = dtLang.NewRow();
                    if (gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures)
                    {
                        dr2["id"] = dtLang.Rows.Count + 3;
                        dr2["name"] = "Show Favorites...";
                        dtLang.Rows.InsertAt(dr2, dtLang.Rows.Count + 3);
                        dtLang.AcceptChanges();
                    }

                    cmbPALang.DataSource = dtLang;
                    cmbPALang.DisplayMember = "name";
                    cmbPALang.SelectedIndex = cmbPALang.FindString("English");
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
                ////cmbPALang.Text = itm;
                if (oDB != null) { oDB.Dispose(); }
                //if (dtLang != null) { dtLang.Dispose(); }
            }
        }

        private void cmbPARace_KeyDown(object sender, KeyEventArgs e)
        {
            //if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbPARace.SelectedIndex == -1 && cmbPARace.Text.Trim() != "")
            //{
            //    //if (cmbPARace.FindStringExact(cmbPARace.Text) != -1)
            //    //{
            //    //    return;
            //    //}

            //    if (DialogResult.Yes == MessageBox.Show(" Do you want to Add new Race Category? \r\n  Yes - Add Category \r\n  No  - Select From the List", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //    {
            //        addCategory(cmbPARace.Text, "Race");
            //        MessageBox.Show("Race Category Added Successfully ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        //MessageBox.Show("Please select Race from the list ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cmbPARace.Focus();
            //    }
            //}
        }
        private void cmbPARace_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //// by Ujwala as on 18022010 - To trap TAB key
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbPARace.SelectedIndex == -1 && cmbPARace.Text.Trim() != "")
            {
                e.IsInputKey = true;
            }
            else
            {
                e.IsInputKey = false;
            }
            //// by Ujwala as on 18022010 - To trap TAB key
        }
        /// <summary>
        /// Function add category to category master according to parameter passed
        /// </summary>
        /// <param name="CategoryDescription"></param>
        /// <param name="CategType"></param>
        private void addCategory(String CategoryDescription, String CategType)
        {

            System.Data.SqlClient.SqlConnection conn;
            conn = new SqlConnection(_databaseconnectionstring);
            System.Data.SqlClient.SqlCommand oCmd = null;
            SqlParameter objParam = new SqlParameter();
            try
            {

                oCmd = new SqlCommand("InsertCategory", conn);
                oCmd.CommandType = CommandType.StoredProcedure;
                objParam = oCmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 50);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = CategoryDescription.Trim();

                objParam = oCmd.Parameters.Add("@sCategoryType", SqlDbType.VarChar, 50);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = CategType;

                objParam = oCmd.Parameters.Add("@nClinicID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = _ClinicID;

                objParam = oCmd.Parameters.Add("@bIsBlocked", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = false;

                conn.Open();
                oCmd.ExecuteNonQuery();
                //////fill combo again after adding new category
                switch (CategType.ToLower())
                {
                    case "race":
                        {
                            searchstr = cmbPARace.Text;
                            fillRace();
                            //cmbPARace.Text = CategoryDescription.Trim();
                            cmbPARace.Text = searchstr;
                            break;
                        }
                    case "language":
                        {
                            searchstr = cmbPALang.Text;
                            fillLang();
                            //cmbPALang.Text = CategoryDescription.Trim();
                            cmbPALang.Text = searchstr;
                            break;
                        }

                    case "CommunicationPrefence":
                        {
                            searchstr = cmbCommPref.Text;
                            fillCommunicationPrefence();
                            cmbPALang.Text = searchstr;
                            break;
                        }
                    case "ethnicity":
                        {
                            searchstr = cmbPAEthn.Text;
                            fillEthnicities();
                            //cmbPAEthn.Text = CategoryDescription.Trim();  //no need to take the value 
                            cmbPAEthn.Text = searchstr;
                            break;
                        }
                        

                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "Category Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "Category Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (oCmd != null) { oCmd.Parameters.Clear(); oCmd.Dispose(); oCmd = null; }
                if (objParam != null) { objParam = null; }

            }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            searchstr = "";
        }
        private void cmbPALang_KeyDown(object sender, KeyEventArgs e)
        {
            //Bug No.49416::Language - Application does not display message box if user add new language.::20130418
           // isLanguageValidated = false;
            //if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbPALang.SelectedIndex == -1 && cmbPALang.Text.Trim() != "")
            //{

            //    //if (cmbPALang.FindStringExact(cmbPALang.Text)!=-1 ) 
            //    //{
            //    //    return;
            //    //}
            //    if (DialogResult.Yes == MessageBox.Show(" Do you want to Add new Language Category?  \r\n  Yes - Add Category \r\n  No  - Select From the List", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //    {
            //        addCategory(cmbPALang.Text, "Language");
            //        MessageBox.Show("Language Category Added Successfully ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        //  MessageBox.Show("Please select Language from the list ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cmbPALang.Focus();
            //    }
            //}
        }

        private void cmbPALang_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //// by Ujwala as on 18022010 - To trap TAB key
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbPALang.SelectedIndex == -1 && cmbPALang.Text.Trim() != "")
            {
                e.IsInputKey = true;
            }
            else
            {
                e.IsInputKey = false;
            }
            //// by Ujwala as on 18022010 - To trap TAB key

            //Trap tab key on language combobox
            if (e.KeyCode == Keys.Tab && cmbPALang.Focused)
            {
                rbBrowsePhoto.Focus();
            }
        }


        private void cmbPAEthn_KeyDown(object sender, KeyEventArgs e)
        {
            //if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbPAEthn.SelectedIndex == -1 && cmbPAEthn.Text.Trim() != "")
            //{
            //    //if (cmbPAEthn.FindStringExact(cmbPAEthn.Text) != -1)
            //    //{
            //    //    return;
            //    //}
            //    if (DialogResult.Yes == MessageBox.Show(" Do you want to Add new Ethnicity Category?  \r\n  Yes - Add Category \r\n  No  - Select From the List", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //    {
            //        addCategory(cmbPAEthn.Text, "Ethnicity");
            //        MessageBox.Show("Ethnicity Category Added Successfully ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        // MessageBox.Show("Please select Ethnicity from the list ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cmbPAEthn.Focus();
            //    }
            //}
        }


        private void cmbPAEthn_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //// by Ujwala as on 18022010 - To trap TAB key
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbPAEthn.SelectedIndex == -1 && cmbPAEthn.Text.Trim() != "")
            {
                e.IsInputKey = true;
            }
            else
            {
                e.IsInputKey = false;
            }
            //// by Ujwala as on 18022010 - To trap TAB key
        }
        private void txtPACode_KeyDown(object sender, KeyEventArgs e)
        {

            if ((txtPACode.Mask == "AAA-AAAAAAAAAA"))
            {
                if (txtPACode.SelectionStart <= 4)
                {
                    e.Handled = true;
                    txtPACode.SelectionStart = 4;
                }

                if (((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete)) & (txtPACode.SelectionStart < 4))
                {
                    e.Handled = true;
                    txtPACode.SelectionStart = 4;
                }
            }


        }
        //added code to check the space between prefix and code
        private void txtPACode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //txtPACode.SelectionStart = txtPACode.Text.Trim().Length+1;

                //if (char.IsWhiteSpace(e.KeyChar) == true)
                if (e.KeyChar == Convert.ToChar(32))
                {
                    //Dont Allow space 
                    //if (!string.IsNullOrEmpty(txtPACode.Text))
                    {
                        e.Handled = true;
                        SendKeys.Send("-");
                    }
                }
                txtPACode.Text = txtPACode.Text.Replace(" ", "");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void txtPatientPrefix_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPACode_KeyUp(object sender, KeyEventArgs e)
        {
            txtPACode.Text = txtPACode.Text.Replace(" ", "");
        }


        #region "Communication Preference"
        private void cmbCommPref_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbCommPref.SelectedIndex == -1 && cmbCommPref.Text.Trim() != "")
            {
                e.IsInputKey = true;
            }
            else
            {
                e.IsInputKey = false;
            }
        }

        private void cmbCommPref_KeyDown(object sender, KeyEventArgs e)
        {
            //if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbCommPref.SelectedIndex == -1 && cmbCommPref.Text.Trim() != "")
            //{
            //    if (DialogResult.Yes == MessageBox.Show(" Do you want to Add new Language Category?  \r\n  Yes - Add Category \r\n  No  - Select From the List", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //    {
            //        addCategory(cmbCommPref.Text, "Communication Preference");
            //        MessageBox.Show("Communication Preference Category Added Successfully ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        //  MessageBox.Show("Please select Language from the list ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cmbCommPref.Focus();
            //    }
            //}
        }
        #endregion "Communication Preference"


        #region "Validating the PatientCode - dhruv 20100720"
        private void txtPACode_Validating(object sender, CancelEventArgs e)
        {
            //While disposing current control on frmsetuppatientclose control was coming in current loop so to avoid this added below condition
            //if (_IsFormClosed == true)
            //{
            //    return;
            //}
            //  if (_isAutogenerate == false)
            //{
            //    string strtxtPACode = txtPACode.Text.Trim();
            //    if (strtxtPACode != string.Empty)
            //    {
            //        if (_IsSaveAsCopy == true)
            //        {
            //            _PatientId = 0;
            //        }
            //        if (ValidateDescription(strtxtPACode, _PatientId) == true)
            //        {
            //            MessageBox.Show("Duplicate Patientcode", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtPACode.Focus();

            //        }
            //    }
            //}
            // Added condition to check duplicate Patient code
            if (_isAutogenerate == false || txtPACode.ReadOnly == false)
            {
                string strtxtPACode = txtPACode.Text.Trim();
                if (strtxtPACode != string.Empty)
                {
                    if (_IsSaveAsCopy == true)
                    {
                        _PatientId = 0;
                    }
                    if (ValidateDescription(strtxtPACode, _PatientId) == true)
                    {

                        if (MessageBox.Show("Duplicate patient code, do you want to generate new patient code?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);


                            txtPACode.Text = ogloPatient.GeneratePatientCode();
                            if (ogloPatient._UseSitePrefix != 0)
                            {
                                if (txtPACode.Text.Length <= 10)
                                { txtPACode.Mask = "AAA-AAAAAAAAAA"; }
                                else
                                { txtPACode.Mask = "AAA-AAAAAAAAAAA"; }
                                txtPatientPrefix.Text = txtPACode.Text.Substring(0, 3);
                            }
                            else
                            {
                                txtPACode.Mask = "AAAAAAAAAAAAA";
                                txtPatientPrefix.Text = "";

                            }
                            txtPACode.Focus();
                            ogloPatient.Dispose();
                            ogloPatient = null;
                        }
                        else
                        {
                            //txtPACode.Focus();
                        }

                        //txtPACode.Focus();

                        //_IsValidated = false;


                    }
                    //if (ValidateDescription_Excluding_sufix(strtxtPACode, _PatientId) == true)
                    //{
                    //    MessageBox.Show("Same patient code is exist with given sufix", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtPACode.Focus();

                    //}
                }
            }
        }
        private bool ValidateDescription(string PatientCode, Int64 PatientID)
        {
            bool _Result = false;
            SqlConnection objConn = null;
            SqlCommand objCmd = null;
            SqlParameter objParam = null;
            try
            {
                objConn = new SqlConnection(_databaseconnectionstring);
                if (objConn != null)
                {
                    objCmd = new SqlCommand("gsp_checkPatient", objConn);
                    if (objCmd != null)
                    {
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objParam = new SqlParameter();
                        if (objParam != null)
                        {
                            objParam = objCmd.Parameters.Add("@sPatientCode", SqlDbType.VarChar);
                            objParam.Direction = ParameterDirection.Input;
                            objParam.Value = PatientCode;
                            //---------------------------
                            objParam = objCmd.Parameters.Add("@nPatientId", SqlDbType.BigInt);
                            objParam.Direction = ParameterDirection.Input;
                            objParam.Value = PatientID;
                            if (objConn.State == ConnectionState.Closed)
                            {
                                objConn.Open();
                            }
                            if (objConn.State == ConnectionState.Open)
                            {
                                object Count = objCmd.ExecuteScalar();
                                if ((int)Count > 0)
                                {
                                    _Result = true;
                                }
                                else
                                {
                                    _Result = false;

                                }
                                objConn.Close();
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                string _errorMessage = "Connection not established succefully" + Ex.ToString();

                if (objConn != null)
                {
                    if (objConn.State == ConnectionState.Open)
                    {
                        objConn.Close();
                    }
                    objConn.Dispose();
                    objConn = null;
                }
                if (objCmd != null)
                {
                    objCmd.Parameters.Clear();
                    objCmd.Dispose();
                    objCmd = null;
                }
                if (objParam != null)
                {
                    objParam = null;
                }

                _Result = false;
            }
            finally
            {
                if (objConn != null)
                {
                    if (objConn.State == ConnectionState.Open)
                    {
                        objConn.Close();
                    }
                    objConn.Dispose();
                    objConn = null;
                }
                if (objCmd != null)
                {
                    objCmd.Parameters.Clear();
                    objCmd.Dispose();
                    objCmd = null;
                }
                if (objParam != null)
                {
                    objParam = null;
                }
            }
            return _Result;
        }
        #endregion

        private void dtpBirth_Validating(object sender, CancelEventArgs e)
        {
            //Start :: Validate BirthTime 
            if (mtxtPADOB.MaskCompleted == true)
            {
                if (dtpBirth.Checked == true)
                {
                    try
                    {
                        DateTime _myBirthDateTime = Convert.ToDateTime(mtxtPADOB.Text);
                        string _myBirthTime = dtpBirth.Text.Trim();
                        TimeSpan _tmptimespan = GetAgeInHrs(_myBirthDateTime, _myBirthTime);
                        //GetPediatricSetting();
                        //if (glbIsPediatric == true)
                        //{
                        if (_tmptimespan.TotalHours < 0)
                        {
                            MessageBox.Show("Enter valid time of birth", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpBirth.Focus();

                        }
                        //}

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Enter a valid time of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        ex = null;

                    }
                }
            }

            //End :: Validate BirthTime 
        }

        //Start :: Yes/No Lab
        private void chkYesNoLab_CheckedChanged(object sender, EventArgs e)
        {


            if (GBlnYesNoLab == true)
            {
                chkYesNoLab.Visible = true;
                if (oPatientDemo.IsYesNoLab == true)
                {

                    chkYesNoLab.Text = "Yes Lab";
                    //chkYesNoLab.Font = new Font("Tahoma", 9, FontStyle.Bold);

                }
                else if (chkYesNoLab.Checked == true)
                {

                    chkYesNoLab.Text = "Yes Lab";
                    //chkYesNoLab.Font = new Font("Tahoma", 9, FontStyle.Bold);

                }
                else
                {
                    chkYesNoLab.Text = "Yes Lab";
                    //chkYesNoLab.Font = new Font("Tahoma", 9, FontStyle.Regular);
                }
            }
            else
            {
                chkYesNoLab.Visible = false;
            }


        }

        private void txtPACode_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPACode.Mask.ToString().Contains("-") == true)
            {
                txtPACode.SelectionStart = txtPACode.Text.Length + 1;
            }
            else
            {
                txtPACode.SelectionStart = txtPACode.Text.Length;
            }
        }

        private void cmbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRelationship.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbRelationship.Items[cmbRelationship.SelectedIndex])["sRelationshipDesc"]), cmbRelationship) >= cmbRelationship.DropDownWidth - 20)
                    {
                        toolTip1.SetToolTip(cmbRelationship, Convert.ToString(((DataRowView)cmbRelationship.Items[cmbRelationship.SelectedIndex])["sRelationshipDesc"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbRelationship, "");
                        this.toolTip1.Hide(cmbRelationship);
                    }
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbRelationship, "");
                    this.toolTip1.Hide(cmbRelationship);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void cmbPARace_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPARace.SelectedItem != null)
                {

                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPARace.Items[cmbPARace.SelectedIndex])["sDescription"]), cmbPARace) >= cmbPARace.DropDownWidth - 20)
                    {
                        toolTip1.SetToolTip(cmbPARace, Convert.ToString(((DataRowView)cmbPARace.Items[cmbPARace.SelectedIndex])["sDescription"]));
                    }
                    else
                    {
                        toolTip1.SetToolTip(cmbPARace, "");
                        this.toolTip1.Hide(cmbPARace);
                    }
                }
                else
                {
                    this.toolTip1.Hide(cmbPARace);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void cmbPAEthn_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool setWidth = false;
            try
            {
                if (cmbPAEthn.SelectedItem != null)
                {
                    if (cmbPAEthn.SelectedIndex >= 0)
                    {
                        DataRowView drv = cmbPAEthn.Items[cmbPAEthn.SelectedIndex] as DataRowView;
                        if (drv != null)
                        {
                            if (drv.Row.Table.Columns.Contains("name"))
                            {
                                String thisString = Convert.ToString(drv["name"]);
                                if (string.IsNullOrEmpty(thisString) == false)
                                {
                                    if (getWidthofListItems(thisString, cmbPAEthn) >= cmbPAEthn.DropDownWidth - 20)
                                    {
                                        toolTip1.SetToolTip(cmbPAEthn, thisString);
                                        setWidth = true;
                                    }
                                }

                            }
                        }
                    }

                }
                if (setWidth == false)
                {
                    toolTip1.SetToolTip(cmbPAEthn, "");
                    this.toolTip1.Hide(cmbPAEthn);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }


        //Added by SaiKrishna:2011-06-27(yyyy-mm-dd) for Patient Account Feature
        #region "Patient Account Feature Related Events"

        /// <summary>
        /// Event to create new account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAccountNew_CheckedChanged(object sender, EventArgs e)
        {
            txtAccountNo.Text = txtPACode.Text.Trim();
            txtAccountDescription.Text = "";
            txtAccountNo.ReadOnly = false;
            txtAccountDescription.ReadOnly = false;
            txtAccountDescription.Enabled = true;

            txtAccountNo.Visible = true;
            btnGuarantorExistingPatientBrowse.Visible = true;
            btnNewGuarantor.Visible = true;
            //btnGuarantorClear.Visible = true;
            btnGuarantorClear.Visible = false;
            chkExcludefromStatement.Visible = true;
            chkSetToCollection.Visible = true;
            lblSameAsGuardian.Visible = true;
            cmbSameAsGuardian.Visible = true;

            lblGuarantorDetails.Height = 21;
            lblGuarantorDetails.Width = 203;

            txtAccGuarantor.Visible = true;

            btnExistingAccountSelect.Visible = false;
            btnExistingAccountDelete.Visible = false;
            lblGuarantorDetails.Visible = false;
            cmbAccounts.Visible = false;

            chkExcludefromStatement.Checked = false;
            chkSetToCollection.Checked = false;
            if (_IsSaveAsCopy == true)
            {
                GetSameAsPatientGuarantor();
                if (_IsRequireBusinessCenterOnPAccounts && _IsPatientAccountFeature)
                {
                    FillBusinessCenter();
                    Int64 _DefaultBusinessCenter = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
                    if (_DefaultBusinessCenter != 0)
                    {
                        cmbBusinessCenter.SelectedValue = _DefaultBusinessCenter;
                    }
                    if (cmbBusinessCenter.Items.Count > 0)
                    {
                        if (cmbBusinessCenter.SelectedIndex == -1)
                        {
                            cmbBusinessCenter.SelectedIndex = 0;
                        }
                    }
                }
            }

            cmbBusinessCenter.Visible = true;
            lblBusinessCenter.Visible = false;
        }

        /// <summary>
        /// Event to select existing account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAccountExisting_CheckedChanged(object sender, EventArgs e)
        {

            txtAccountNo.Text = "";
            txtAccountDescription.Text = "";
            lblGuarantorDetails.Text = "";
            lblBusinessCenter.Text = "";

            txtAccountNo.ReadOnly = true;
            txtAccountDescription.ReadOnly = true;
            txtAccountDescription.Enabled = false;

            btnExistingAccountSelect.Visible = true;
            btnExistingAccountDelete.Visible = true;
            lblGuarantorDetails.Visible = true;

            txtAccGuarantor.Visible = false;
            btnGuarantorExistingPatientBrowse.Visible = false;
            btnNewGuarantor.Visible = false;
            btnGuarantorClear.Visible = false;
            chkExcludefromStatement.Visible = false;
            chkSetToCollection.Visible = false;
            lblSameAsGuardian.Visible = false;
            cmbSameAsGuardian.Visible = false;

            lblGuarantorDetails.Height = 53;
            lblGuarantorDetails.Width = 275;

            cmbBusinessCenter.Visible = false;
            lblBusinessCenter.Visible = true;


            //select the existing sameas patient account when saveascopy patient.
            if (_IsSaveAsCopy == true)
            {
                DataTable dt = GetSameAsPatientAccountForSaveAsCopyPatient(_Id);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtAccountNo.Tag = dt.Rows[0]["nPAccountID"].ToString();
                    txtAccountNo.Text = dt.Rows[0]["sAccountNo"].ToString();
                    txtAccountDescription.Text = dt.Rows[0]["sAccountDesc"].ToString();
                    //fill guarantor details
                    string guarantordetails = dt.Rows[0]["sFirstName"].ToString().Trim() + ' ' + dt.Rows[0]["sMiddleName"].ToString() + ' ' + dt.Rows[0]["sLastName"].ToString() + Environment.NewLine;

                    if (dt.Rows[0]["sAddressLine1"].ToString() != "")
                        guarantordetails = guarantordetails + dt.Rows[0]["sAddressLine1"].ToString() + ',';

                    if (dt.Rows[0]["sAddressLine2"].ToString() != "")
                        guarantordetails = guarantordetails + dt.Rows[0]["sAddressLine2"].ToString() + ',' + Environment.NewLine;

                    if (dt.Rows[0]["sCity"].ToString() != "")
                        guarantordetails = guarantordetails + dt.Rows[0]["sCity"].ToString() + ' ' + dt.Rows[0]["sState"].ToString() + ' ' + dt.Rows[0]["sZip"].ToString();

                    lblGuarantorDetails.Text = guarantordetails;

                    if (Convert.ToString(dt.Rows[0]["sBusinessCenterCode"]) != "")
                    {
                        lblBusinessCenter.Text = Convert.ToString(dt.Rows[0]["sBusinessCenterCode"]);
                    }
                    else
                    {
                        lblBusinessCenter.Text = "";

                    }

                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }

            }
        }

        /// <summary>
        ///  Event to create new account when Patient modify mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            try
            {
                //Get count of OwnAccounts.Based on this generate AccountNo sequence.
                string AccountNumber = string.Empty;
                int ownAccountSequenceCount = 0;
                //if (oPatientAccounts != null && oPatientAccounts.Count > 0)
                //{
                //    for (int i = 0; i < oPatientAccounts.Count; i++)
                //    {
                //        if (oPatientAccounts[i].OwnAccount.ToString().ToLower() == "true")
                //        {
                //            ownAccountCount++;
                //        }
                //    }
                //}
                AccountNumber = objgloAccount.GetAccountSequenceNumber(PatientId, _ClinicID);
                if (AccountNumber != "")
                {
                    if (AccountNumber.ToString().Contains("_") == true)
                    {
                        try
                        {
                            ownAccountSequenceCount = int.Parse((AccountNumber.ToString().Trim().Split('_')[1]).Substring(0, 1));
                        }
                        catch (Exception)
                        {
                            ownAccountSequenceCount = 1;
                        }

                    }
                    ownAccountSequenceCount = ownAccountSequenceCount + 1;
                }
                oFrmAddPatientAccount = new frmAddPatientAccount(PatientId, _databaseconnectionstring);
                oFrmAddPatientAccount.PatientDemographicDetails = PatientDemographicsDetails;
                oFrmAddPatientAccount.PatientGuarantors = new PatientOtherContacts();
                oFrmAddPatientAccount.PatientGuardianDetails = PatientGuardianDetails;
                oFrmAddPatientAccount.PatientAccounts = PatientAccounts;
                oFrmAddPatientAccount.OwnAccountCount = ownAccountSequenceCount;
                oFrmAddPatientAccount.SaveButton_Click += new frmAddPatientAccount.SaveButtonClick(oFrmAddPatientAccount_SaveButton_Click);
                oFrmAddPatientAccount.ShowDialog(this);
                oFrmAddPatientAccount.SaveButton_Click -= new frmAddPatientAccount.SaveButtonClick(oFrmAddPatientAccount_SaveButton_Click);
                oFrmAddPatientAccount.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to Edit Account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAccounts.Items.Count > 0)
                {
                    bool ownAccount = true;
                    if (oPatientAccounts != null && oPatientAccounts.Count > 0)
                    {
                        for (int i = 0; i < oPatientAccounts.Count; i++)
                        {
                            if (oPatientAccounts[i].PAccountID == Convert.ToInt64(cmbAccounts.SelectedValue))
                            {
                                ownAccount = oPatientAccounts[i].OwnAccount;
                                break;
                            }
                        }
                    }
                    oFrmEditPatientAccount = new frmEditPatientAccount(_databaseconnectionstring, _PatientId, _nGuarantorId, Convert.ToInt64(cmbAccounts.SelectedValue));
                    oFrmEditPatientAccount.PatientGuarantors = new PatientOtherContacts();
                    oFrmEditPatientAccount.PatientGuardianDetails = PatientGuardianDetails;
                    oFrmEditPatientAccount.PatientDemographicDetails = PatientDemographicsDetails;
                    //Added by Mayuri : 20151006-2.	Update Guarantor Address if Guarantor is “Same as Patient” when patient address is updated.
                    oFrmEditPatientAccount.PatientDemographicDetails.PatientAddress1 = oAddresscontrol.txtAddress1.Text.Trim();
                    oFrmEditPatientAccount.PatientDemographicDetails.PatientAddress2 = oAddresscontrol.txtAddress2.Text.Trim();
                    oFrmEditPatientAccount.PatientDemographicDetails.PatientCity = oAddresscontrol.txtCity.Text.Trim();
                    oFrmEditPatientAccount.PatientDemographicDetails.PatientCounty = oAddresscontrol.txtCounty.Text.Trim();
                    oFrmEditPatientAccount.PatientDemographicDetails.PatientZip = oAddresscontrol.txtZip.Text.Trim();
                    oFrmEditPatientAccount.PatientDemographicDetails.PatientState = oAddresscontrol.cmbState.Text.Trim();
                    oFrmEditPatientAccount.PatientDemographicDetails.PatientCountry = oAddresscontrol.cmbCountry.Text.Trim();

                    oFrmEditPatientAccount._ownAccount = ownAccount;
                    oFrmEditPatientAccount.SaveButton_Click += new frmEditPatientAccount.SaveButtonClick(oFrmEditPatientAccount_SaveButton_Click);
                   // oFrmEditPatientAccount.CloseButton_Click += new frmEditPatientAccount.CloseButtonClick(oFrmEditPatientAccount_CloseButton_Click);
                    oFrmEditPatientAccount.ShowDialog(this);
                    oFrmEditPatientAccount.SaveButton_Click -= new frmEditPatientAccount.SaveButtonClick(oFrmEditPatientAccount_SaveButton_Click);
                 //   oFrmEditPatientAccount.CloseButton_Click -= new frmEditPatientAccount.CloseButtonClick(oFrmEditPatientAccount_CloseButton_Click);
                    if (oFrmEditPatientAccount != null) { oFrmEditPatientAccount.Dispose(); oFrmEditPatientAccount = null; }
                }
                else
                {
                    MessageBox.Show("No accounts to modify.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        /// <summary>
        /// Event to Assign PatientAccounts of frmEditPatientAccount to gloPatientDemographic control PatientAccounts property.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oFrmEditPatientAccount_SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Assign PatientAccounts of frmEditPatientAccount to gloPatientDemographic control PatientAccounts  
                PatientAccounts = oFrmEditPatientAccount.PatientAccounts;
                if (oPatientAccounts != null && oPatientAccounts.Count > 0)
                {
                    DataTable dtPatientAccounts = new DataTable();
                    DataColumn dtAccountId = new DataColumn("AccountId");
                    DataColumn dtAccountNo = new DataColumn("AccountNo");
                    dtPatientAccounts.Columns.Add(dtAccountId);
                    dtPatientAccounts.Columns.Add(dtAccountNo);
                    for (int i = 0; i < oPatientAccounts.Count; i++)
                    {
                        DataRow drTemp = dtPatientAccounts.NewRow();
                        drTemp["AccountId"] = oPatientAccounts[i].PAccountID;

                        if (oPatientAccounts[i].OwnAccount == false)
                            drTemp["AccountNo"] = "[" + oPatientAccounts[i].AccountNo + "]";
                        else
                            drTemp["AccountNo"] = oPatientAccounts[i].AccountNo;

                        dtPatientAccounts.Rows.Add(drTemp);
                    }
                    //cmbAccounts.Items.Clear();
                    cmbAccounts.DataSource = null;
                    cmbAccounts.Items.Clear();
                    cmbAccounts.DisplayMember = dtPatientAccounts.Columns["AccountNo"].ColumnName;
                    cmbAccounts.ValueMember = dtPatientAccounts.Columns["AccountId"].ColumnName;
                    cmbAccounts.DataSource = dtPatientAccounts;
                    cmbAccounts.SelectedValue = oFrmEditPatientAccount.nAccountId;

                }
                GetPatientOtherGuarantors();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        /// <summary>
        /// Event to Close FrmEditPatientAccount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oFrmEditPatientAccount_CloseButton_Click(object sender, EventArgs e)
        {
            oFrmEditPatientAccount.Close();
        }

        /// <summary>
        /// Event to Assign PatientAccounts of frmAddPatientAccount to gloPatientDemographic control PatientAccounts property. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oFrmAddPatientAccount_SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Assign PatientAccounts of frmAddPatientAccount to gloPatientDemographic control PatientAccounts  
                PatientAccounts = oFrmAddPatientAccount.PatientAccounts;
                if (oPatientAccounts != null && oPatientAccounts.Count > 0)
                {
                    DataTable dtPatientAccounts = new DataTable();
                    DataColumn dtAccountId = new DataColumn("AccountId");
                    DataColumn dtAccountNo = new DataColumn("AccountNo");
                    dtPatientAccounts.Columns.Add(dtAccountId);
                    dtPatientAccounts.Columns.Add(dtAccountNo);
                    for (int i = 0; i < oPatientAccounts.Count; i++)
                    {
                        DataRow drTemp = dtPatientAccounts.NewRow();
                        drTemp["AccountId"] = oPatientAccounts[i].PAccountID;

                        if (oPatientAccounts[i].OwnAccount == false)
                            drTemp["AccountNo"] = "[" + oPatientAccounts[i].AccountNo + "]";
                        else
                            drTemp["AccountNo"] = oPatientAccounts[i].AccountNo;

                        dtPatientAccounts.Rows.Add(drTemp);
                    }
                   // cmbAccounts.Items.Clear();
                    cmbAccounts.DataSource = null;
                    cmbAccounts.Items.Clear();
                    cmbAccounts.DisplayMember = dtPatientAccounts.Columns["AccountNo"].ColumnName;
                    cmbAccounts.ValueMember = dtPatientAccounts.Columns["AccountId"].ColumnName;
                    cmbAccounts.DataSource = dtPatientAccounts;

                }
                GetPatientOtherGuarantors();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to list Existing accounts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExistingAccountSelect_Click(object sender, EventArgs e)
        {
            try
            {

                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.GuarantorsAccounts, false, this.Width);
                oListControl.ControlHeader = "Guarantors Accounts";
                oListControl.PatientID = _PatientId;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ExistingAccountSelectClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                oListControl.OpenControl();

                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        /// <summary>
        /// Event to clear Existing account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExistingAccountDelete_Click(object sender, EventArgs e)
        {
            try
            {
                txtAccountNo.Text = "";
                txtAccountNo.Tag = null;
                txtAccountDescription.Text = "";
                lblGuarantorDetails.Text = "";
                lblBusinessCenter.Text = "";
                
                oAccount = new Account();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to select Existing account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oListControl_ExistingAccountSelectClick(object sender, EventArgs e)
        {
            try
            {
                txtAccountNo.Text = "";
                txtAccountDescription.Text = "";
                lblGuarantorDetails.Text = "";
                lblBusinessCenter.Text = "";

                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        //AccountId
                        txtAccountNo.Tag = oListControl.SelectedItems[i].ID;
                        txtAccountNo.Text = oListControl.SelectedItems[i].Code;
                        txtAccountDescription.Text = oListControl.SelectedItems[i].Description;

                        DataTable dt = objgloAccount.GetAccountDetailsById(Convert.ToInt64(txtAccountNo.Tag.ToString()));

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //fill guarantor details
                            string guarantordetails = dt.Rows[0]["sFirstName"].ToString().Trim() + ' ' + dt.Rows[0]["sMiddleName"].ToString() + ' ' + dt.Rows[0]["sLastName"].ToString() + Environment.NewLine;

                            if (dt.Rows[0]["sAddressLine1"].ToString() != "")
                                guarantordetails = guarantordetails + dt.Rows[0]["sAddressLine1"].ToString() + ',';

                            if (dt.Rows[0]["sAddressLine2"].ToString() != "")
                                guarantordetails = guarantordetails + dt.Rows[0]["sAddressLine2"].ToString() + ',' + Environment.NewLine;
                            else { guarantordetails = guarantordetails + Environment.NewLine; }
                            if (dt.Rows[0]["sCity"].ToString() != "")
                                guarantordetails = guarantordetails + dt.Rows[0]["sCity"].ToString() + ' ' + dt.Rows[0]["sState"].ToString() + ' ' + dt.Rows[0]["sZip"].ToString();

                            lblGuarantorDetails.Text = guarantordetails;

                            if (Convert.ToString(dt.Rows[0]["sBusinessCenterCode"]) != "")
                            {
                                //lblBusinessCenter.Visible = true;
                                lblBusinessCenter.Text = Convert.ToString(dt.Rows[0]["sBusinessCenterCode"]);
                            }
                            else
                            {
                                lblBusinessCenter.Text = "";
                                //if (_IsRequireBusinessCenterOnPAccounts)
                                //{
                                //    cmbBusinessCenter.Visible = true;
                                //    lblBusinessCenter.Visible = false;
                                //}
                            }

                        }
                        if (dt != null)
                        {
                            dt.Dispose();
                            dt = null;
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
                onDemographicControl_Enter(sender, e);
            }

        }

        /// <summary>
        /// Event to Account Selection Changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (cmbAccounts.SelectedIndex >= 0)
                {
                    DataTable dtAccount = objgloAccount.GetAccountDetailsById(Convert.ToInt64(cmbAccounts.SelectedValue.ToString()));
                    if (dtAccount != null && dtAccount.Rows.Count > 0)
                    {
                        txtAccountDescription.Text = dtAccount.Rows[0]["sAccountDesc"].ToString();
                        string guarantordetails = dtAccount.Rows[0]["sFirstName"].ToString().Trim() + ' ' + dtAccount.Rows[0]["sMiddleName"].ToString() + ' ' + dtAccount.Rows[0]["sLastName"].ToString() + Environment.NewLine;

                        if (dtAccount.Rows[0]["sAddressLine1"].ToString() != "")
                            guarantordetails = guarantordetails + dtAccount.Rows[0]["sAddressLine1"].ToString() + ',';

                        if (dtAccount.Rows[0]["sAddressLine2"].ToString() != "")
                            guarantordetails = guarantordetails + dtAccount.Rows[0]["sAddressLine2"].ToString() + ',' + Environment.NewLine;
                        else { guarantordetails = guarantordetails + Environment.NewLine; }

                        if (dtAccount.Rows[0]["sCity"].ToString() != "")
                            guarantordetails = guarantordetails + dtAccount.Rows[0]["sCity"].ToString() + ' ' + dtAccount.Rows[0]["sState"].ToString() + ' ' + dtAccount.Rows[0]["sZip"].ToString();

                        lblGuarantorDetails.Text = guarantordetails;
                        _nGuarantorId = Convert.ToInt64(dtAccount.Rows[0]["nGuarantorId"].ToString());

                        if (Convert.ToString(dtAccount.Rows[0]["sBusinessCenterCode"]) != "")
                        {
                            lblBusinessCenter.Text = Convert.ToString(dtAccount.Rows[0]["sBusinessCenterCode"]);
                        }
                        else
                        {
                            lblBusinessCenter.Text = "";
                        }
                    }
                    if (dtAccount != null)
                    {
                        dtAccount.Dispose();
                        dtAccount = null;
                    }


                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to PatientCode text changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPACode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (_IsPatientAccountFeature == true && rbAccountNew.Checked == true)
                {
                    txtAccountNo.Text = txtPACode.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to CmbSameAsGuardian selected index changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSameAsGuardian_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsCmbSameAsGuardianLoadFlag == true)
                {
                    //New mode
                    if (PatientId == 0)
                    {
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            oPatientGuarantors.RemoveAt(0);
                            txtAccGuarantor.Text = "";
                        }
                    }
                    if (cmbSameAsGuardian.Text == "Patient")
                    {
                        if (txtPAFname.Text.Trim() != "" && txtPALName.Text.Trim() != "")
                        {
                            bool _shouldAdd = true;

                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.SameAsPatient)
                                    {
                                        _shouldAdd = false;
                                        break;

                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {

                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = _PatientId;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = txtPAFname.Text.Trim();
                                oGuarantor.MiddleName = txtPAMName.Text.Trim();
                                oGuarantor.LastName = txtPALName.Text.Trim();
                                if (mtxtPADOB.MaskCompleted == true)
                                { oGuarantor.DOB = Convert.ToDateTime(mtxtPADOB.Text); }

                                if (txtmPASSN.IsValidated == true)
                                    oGuarantor.SSN = txtmPASSN.Text.Trim();
                                else
                                    oGuarantor.SSN = "";

                                oGuarantor.Gender = cmbGender.Text.Trim();

                                //if (oAddresscontrol.txtAddress1.Text.Trim() == "")
                                //{
                                //    MessageBox.Show("Enter address for selected guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    cmbSameAsGuardian.SelectedIndex = 0;
                                //    oAddresscontrol.txtAddress1.Focus();
                                //    return;
                                //}
                                //if (oAddresscontrol.txtCity.Text.Trim() == "")
                                //{
                                //    MessageBox.Show("Enter city for selected guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    cmbSameAsGuardian.SelectedIndex = 0;
                                //    oAddresscontrol.txtCity.Focus();
                                //    return;
                                //}
                                //if (oAddresscontrol.cmbState.Text.Trim() == "")
                                //{
                                //    MessageBox.Show("Enter state for selected guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    cmbSameAsGuardian.SelectedIndex = 0;
                                //    oAddresscontrol.cmbState.Focus();
                                //    return;
                                //}
                                //if (oAddresscontrol.txtZip.Text.Trim() == "")
                                //{
                                //    MessageBox.Show("Enter zip for selected guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    cmbSameAsGuardian.SelectedIndex = 0;
                                //    oAddresscontrol.txtZip.Focus();
                                //    return;
                                //}

                                oGuarantor.AddressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                                oGuarantor.AddressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                                oGuarantor.City = oAddresscontrol.txtCity.Text.Trim();
                                oGuarantor.County = oAddresscontrol.txtCounty.Text.Trim();
                                oGuarantor.Zip = oAddresscontrol.txtZip.Text.Trim();
                                oGuarantor.State = oAddresscontrol.cmbState.Text.Trim();
                                oGuarantor.Country = oAddresscontrol.cmbCountry.Text;
                                oGuarantor.County = oAddresscontrol.txtCounty.Text.Trim();

                                oGuarantor.Relation = "Self";
                                oGuarantor.Phone = mtxtPAPhone.Text.Trim();

                                if (mtxtPAMobile.IsValidated == true) { oGuarantor.Mobile = mtxtPAMobile.Text; }
                                oGuarantor.Email = txtPAEmail.Text.Trim();
                                oGuarantor.Fax = txtPAFax.Text.Trim();
                                oGuarantor.OtherConatctType = PatientOtherContactType.SameAsPatient;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;

                                if (oPatientGuarantors.Count == 0)
                                {
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                        {
                                            txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                            setCmbSameAsGuardianIndex();
                                        }
                                        return;
                                    }
                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;

                                    setCmbSameAsGuardianIndex();
                                }
                            }

                        }
                    }
                    if (cmbSameAsGuardian.Text == "Mother")
                    {

                        if (_PatientGuardian.PatientMotherFirstName.Trim() != "" && _PatientGuardian.PatientMotherLastName.Trim() != "")
                        {
                            bool _shouldAdd = true;
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.Mother)
                                    {
                                        _shouldAdd = false;
                                        break;

                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = _PatientGuardian.PatientMotherFirstName.Trim();
                                oGuarantor.MiddleName = _PatientGuardian.PatientMotherMiddleName.Trim();
                                oGuarantor.LastName = _PatientGuardian.PatientMotherLastName.Trim();
                                oGuarantor.Relation = "Mother";
                                oGuarantor.Gender = "Female";
                                //Check Address validation for mother.

                                //if (_PatientGuardian.PatientMotherAddress1.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide address for Guardian(Mother).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}

                                //if (_PatientGuardian.PatientMotherCity.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide city for Guardian(Mother).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (_PatientGuardian.PatientMotherState.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide state for Guardian(Mother).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (_PatientGuardian.PatientMotherZip.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide zip for Guardian(Mother).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}

                                oGuarantor.AddressLine1 = _PatientGuardian.PatientMotherAddress1.Trim().ToString();
                                oGuarantor.AddressLine2 = _PatientGuardian.PatientMotherAddress2.Trim().ToString();
                                oGuarantor.City = _PatientGuardian.PatientMotherCity.Trim().ToString();
                                oGuarantor.County = _PatientGuardian.PatientMotherCounty.Trim().ToString();
                                oGuarantor.Zip = _PatientGuardian.PatientMotherZip.Trim().ToString();
                                oGuarantor.State = _PatientGuardian.PatientMotherState.Trim().ToString();
                                oGuarantor.Country = _PatientGuardian.PatientMotherCountry.Trim().ToString();
                                oGuarantor.Phone = _PatientGuardian.PatientMotherPhone.Trim().ToString();
                                oGuarantor.Mobile = _PatientGuardian.PatientMotherMobile.Trim().ToString();
                                oGuarantor.Email = _PatientGuardian.PatientMotherEmail.Trim().ToString();
                                oGuarantor.Fax = _PatientGuardian.PatientMotherFAX.Trim().ToString();
                                oGuarantor.OtherConatctType = PatientOtherContactType.Mother;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;
                                if (oPatientGuarantors.Count == 0)
                                {
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }
                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
                            }


                        }
                    }
                    if (cmbSameAsGuardian.Text == "Father")
                    {
                        if (_PatientGuardian.PatientFatherFirstName.Trim() != "" && _PatientGuardian.PatientFatherLastName.Trim() != "")
                        {
                            bool _shouldAdd = true;

                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.Father)
                                    {
                                        _shouldAdd = false;
                                        break;

                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = _PatientGuardian.PatientFatherFirstName;
                                oGuarantor.MiddleName = _PatientGuardian.PatientFatherMiddleName.Trim();
                                oGuarantor.LastName = _PatientGuardian.PatientFatherLastName.Trim();
                                oGuarantor.Relation = "Father";
                                oGuarantor.Gender = "Male";

                                //Check Address validation for father.

                                //if (_PatientGuardian.PatientFatherAddress1.Trim().ToString() == "")
                                //{
                                //    MessageBox.Show("Provide address for Guardian(Father).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}

                                //if (_PatientGuardian.PatientFatherCity.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide city for Guardian(Father).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (_PatientGuardian.PatientFatherState.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide state for Guardian(Father).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (_PatientGuardian.PatientFatherZip.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide zip for Guardian(Father).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}

                                oGuarantor.AddressLine1 = _PatientGuardian.PatientFatherAddress1.Trim().ToString();
                                oGuarantor.AddressLine2 = _PatientGuardian.PatientFatherAddress2.Trim().ToString();
                                oGuarantor.City = _PatientGuardian.PatientFatherCity.Trim().ToString();
                                oGuarantor.County = _PatientGuardian.PatientFatherCounty.Trim().ToString();
                                oGuarantor.Zip = _PatientGuardian.PatientFatherZip.Trim().ToString();
                                oGuarantor.State = _PatientGuardian.PatientFatherState.Trim().ToString();
                                oGuarantor.Country = _PatientGuardian.PatientFatherCountry.Trim().ToString();


                                oGuarantor.Phone = _PatientGuardian.PatientFatherPhone.Trim().ToString();
                                oGuarantor.Mobile = _PatientGuardian.PatientFatherMobile.Trim().ToString();

                                oGuarantor.Email = _PatientGuardian.PatientFatherEmail.Trim().ToString();
                                oGuarantor.Fax = _PatientGuardian.PatientFatherFAX.Trim().ToString();
                                oGuarantor.OtherConatctType = PatientOtherContactType.Father;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;

                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;

                                if (oPatientGuarantors.Count == 0)
                                {
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }
                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
                            }

                        }

                    }

                    if (cmbSameAsGuardian.Text == "Other Guardian")
                    {

                        if (_PatientGuardian.PatientGuardianFirstName.Trim() != "" && _PatientGuardian.PatientGuardianLastName.Trim() != "")
                        {
                            bool _shouldAdd = true;

                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.OtherGuardian)
                                    {
                                        _shouldAdd = false;
                                        break;
                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = _PatientGuardian.PatientGuardianFirstName.Trim();
                                oGuarantor.MiddleName = _PatientGuardian.PatientGuardianMiddleName.Trim();
                                oGuarantor.LastName = _PatientGuardian.PatientGuardianLastName.Trim();
                                oGuarantor.Relation = _PatientGuardian.PatientGuardianRelationDS.Trim().ToString();

                                //Check Address validation for otherGuardian

                                //if (_PatientGuardian.PatientGuardianAddress1.Trim().ToString() == "")
                                //{
                                //    MessageBox.Show("Provide address for Guardian(Other).", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (_PatientGuardian.PatientGuardianCity.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide city for Guardian(Other)", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (_PatientGuardian.PatientGuardianState.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide state for Guardian(Other)", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (_PatientGuardian.PatientGuardianZip.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide zip for Guardian(Other)", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}

                                oGuarantor.AddressLine1 = _PatientGuardian.PatientGuardianAddress1.Trim().ToString();
                                oGuarantor.AddressLine2 = _PatientGuardian.PatientGuardianAddress2.Trim().ToString();
                                oGuarantor.City = _PatientGuardian.PatientGuardianCity.Trim().ToString();
                                oGuarantor.County = _PatientGuardian.PatientGuardianCounty.Trim().ToString();
                                oGuarantor.Zip = _PatientGuardian.PatientGuardianZip.Trim().ToString();
                                oGuarantor.State = _PatientGuardian.PatientGuardianState.Trim().ToString();
                                oGuarantor.Country = _PatientGuardian.PatientGuardianCountry.Trim().ToString();


                                oGuarantor.Phone = _PatientGuardian.PatientGuardianPhone.Trim().ToString();
                                oGuarantor.Mobile = _PatientGuardian.PatientGuardianMobile.Trim().ToString();

                                oGuarantor.Email = _PatientGuardian.PatientGuardianEmail.Trim().ToString();
                                oGuarantor.Fax = _PatientGuardian.PatientGuardianFAX.Trim().ToString();
                                oGuarantor.OtherConatctType = PatientOtherContactType.OtherGuardian;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;

                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;
                                if (oPatientGuarantors.Count == 0)
                                {
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }
                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
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

        /// <summary>
        /// Event to create copy account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyAccount_Click(object sender, EventArgs e)
        {
            try
            {
                //Code Added for SaiKrishna.
                if (cmbAccounts.Items.Count > 0)
                {
                    bool ownAccount = true;
                    if (oPatientAccounts != null && oPatientAccounts.Count > 0)
                    {
                        for (int i = 0; i < oPatientAccounts.Count; i++)
                        {
                            if (oPatientAccounts[i].PAccountID == Convert.ToInt64(cmbAccounts.SelectedValue))
                            {
                                ownAccount = oPatientAccounts[i].OwnAccount;
                                break;
                            }
                        }
                    }
                    //Allow copy account whent it is own account.
                    if (ownAccount == true)
                    {
                        oFrmCopyPatientAccount = new frmCopyPatientAccount(_databaseconnectionstring, _PatientId, _nGuarantorId, Convert.ToInt64(cmbAccounts.SelectedValue));
                        oFrmCopyPatientAccount.PatientGuarantors = new PatientOtherContacts();
                        oFrmCopyPatientAccount.PatientGuardianDetails = PatientGuardianDetails;
                        oFrmCopyPatientAccount.PatientDemographicDetails = PatientDemographicsDetails;
                        //Added by Mayuri : 20151006-2.	Update Guarantor Address if Guarantor is “Same as Patient” when patient address is updated.
                        oFrmCopyPatientAccount.PatientDemographicDetails.PatientAddress1 = oAddresscontrol.txtAddress1.Text.Trim();
                        oFrmCopyPatientAccount.PatientDemographicDetails.PatientAddress2 = oAddresscontrol.txtAddress2.Text.Trim();
                        oFrmCopyPatientAccount.PatientDemographicDetails.PatientCity = oAddresscontrol.txtCity.Text.Trim();
                        oFrmCopyPatientAccount.PatientDemographicDetails.PatientCounty = oAddresscontrol.txtCounty.Text.Trim();
                        oFrmCopyPatientAccount.PatientDemographicDetails.PatientZip = oAddresscontrol.txtZip.Text.Trim();
                        oFrmCopyPatientAccount.PatientDemographicDetails.PatientState = oAddresscontrol.cmbState.Text.Trim();
                        oFrmCopyPatientAccount.PatientDemographicDetails.PatientCountry = oAddresscontrol.cmbCountry.Text.Trim();

                        oFrmCopyPatientAccount.SaveButton_Click += new frmCopyPatientAccount.SaveButtonClick(oFrmCopyPatientAccount_SaveButton_Click);
                       // oFrmCopyPatientAccount.CloseButton_Click += new frmCopyPatientAccount.CloseButtonClick(oFrmCopyPatientAccount_CloseButton_Click);
                        oFrmCopyPatientAccount.ShowDialog(this);
                        oFrmCopyPatientAccount.SaveButton_Click -= new frmCopyPatientAccount.SaveButtonClick(oFrmCopyPatientAccount_SaveButton_Click);
                      //  oFrmCopyPatientAccount.CloseButton_Click -= new frmCopyPatientAccount.CloseButtonClick(oFrmCopyPatientAccount_CloseButton_Click);
                        if (oFrmCopyPatientAccount != null) { oFrmCopyPatientAccount.Dispose(); oFrmCopyPatientAccount = null; }
                    }
                    else
                    {
                        MessageBox.Show("This is an existing account.\nCopy Account feature is not allowed.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No accounts to copy.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to Assign PatientAccounts of frmCopyPatientAccount to gloPatientDemographic control PatientAccounts property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oFrmCopyPatientAccount_SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Assign PatientAccounts of frmCopyPatientAccount to gloPatientDemographic control PatientAccounts  
                PatientAccounts = oFrmCopyPatientAccount.PatientAccounts;
                if (oPatientAccounts != null && oPatientAccounts.Count > 0)
                {
                    DataTable dtPatientAccounts = new DataTable();
                    DataColumn dtAccountId = new DataColumn("AccountId");
                    DataColumn dtAccountNo = new DataColumn("AccountNo");
                    dtPatientAccounts.Columns.Add(dtAccountId);
                    dtPatientAccounts.Columns.Add(dtAccountNo);
                    for (int i = 0; i < oPatientAccounts.Count; i++)
                    {
                        DataRow drTemp = dtPatientAccounts.NewRow();
                        drTemp["AccountId"] = oPatientAccounts[i].PAccountID;

                        if (oPatientAccounts[i].OwnAccount == false)
                            drTemp["AccountNo"] = "[" + oPatientAccounts[i].AccountNo + "]";
                        else
                            drTemp["AccountNo"] = oPatientAccounts[i].AccountNo;

                        dtPatientAccounts.Rows.Add(drTemp);
                    }
                    //cmbAccounts.Items.Clear();
                    cmbAccounts.DataSource = null;
                    cmbAccounts.Items.Clear();
                    cmbAccounts.DisplayMember = dtPatientAccounts.Columns["AccountNo"].ColumnName;
                    cmbAccounts.ValueMember = dtPatientAccounts.Columns["AccountId"].ColumnName;
                    cmbAccounts.DataSource = dtPatientAccounts;
                    cmbAccounts.SelectedValue = oFrmCopyPatientAccount.nAccountId;

                }
                GetPatientOtherGuarantors();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        /// <summary>
        /// Event to close FrmCopyPatientAccount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oFrmCopyPatientAccount_CloseButton_Click(object sender, EventArgs e)
        {
            oFrmCopyPatientAccount.Close();
        }

        /// <summary>
        /// Event to Assign SameAsPatientGuarantor when patient LastName leave the focus. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPALName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (_PatientId == 0)
                {
                    GetSameAsPatientGuarantor();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to Assign SameAsPatientGuarantor when patient FirstName leave the focus. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPAFname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (_PatientId == 0)
                {
                    GetSameAsPatientGuarantor();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Method to clear to Account Guarantor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuarantorClear_Click(object sender, EventArgs e)
        {
            PatientOtherContact oSelectedGuarantor = null;
            try
            {
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Are you sure you want to remove selected guarantor? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        oSelectedGuarantor = oPatientGuarantors[0];
                        if (oSelectedGuarantor.PatientContactID != 0)
                        {
                            bool IsTransExist = false;
                            IsTransExist = objgloAccount.CheckTransactionsExistForAccountGuarantor(oSelectedGuarantor.PAccountID, oSelectedGuarantor.PatientContactID);

                            if (IsTransExist == true)
                            {
                                DialogResult result = MessageBox.Show("Selected guarantor is associated with transactions. Do you want to continue to remove. ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result == DialogResult.Yes)
                                {
                                    oPatientGuarantors.Clear();
                                }
                            }
                            else
                            {
                                oPatientGuarantors.Clear();
                            }
                        }
                        else
                        {
                            oPatientGuarantors.Clear();
                        }
                    }
                    if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                    {
                        for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                        {
                            txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                            setCmbSameAsGuardianIndex();
                        }
                    }
                    else
                    {
                        txtAccGuarantor.Text = "";
                        IsCmbSameAsGuardianLoadFlag = false;
                        cmbSameAsGuardian.SelectedIndex = -1;
                        IsCmbSameAsGuardianLoadFlag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oSelectedGuarantor = null;
            }
        }

        #endregion

        //Added by SaiKrishna:2011-06-27(yyyy-mm-dd) for Patient Account Feature
        #region "PatientAccountFeature Related Methods"

        /// <summary>
        /// Thsis method is used to check Patient Name and Address are changed or not.
        /// If changed we can update guarantor information when guarantor is like patient. 
        /// </summary>
        /// <returns></returns>
        private bool IsPatientDataModified()
        {
            bool _result = false;
            try
            {
                if (_PatientId != 0)
                {
                    if (txtPAFname.Text.Trim() == oPatientDemo.PatientFirstName &&
                        txtPAMName.Text.Trim() == oPatientDemo.PatientMiddleName &&
                        txtPALName.Text.Trim() == oPatientDemo.PatientLastName &&
                        oAddresscontrol.txtAddress1.Text == oPatientDemo.PatientAddress1.ToString() &&
                        oAddresscontrol.txtAddress2.Text == oPatientDemo.PatientAddress2.ToString() &&
                        oAddresscontrol.txtZip.Text == oPatientDemo.PatientZip &&
                        oAddresscontrol.txtCity.Text == oPatientDemo.PatientCity &&
                        oAddresscontrol.cmbCountry.Text == oPatientDemo.PatientCountry &&
                        oAddresscontrol.cmbState.Text == oPatientDemo.PatientState.ToString() &&
                        oAddresscontrol.txtCounty.Text == oPatientDemo.PatientCounty)
                    {
                        _result = false;
                    }
                    else
                    {
                        _result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;

        }
        /// <summary>
        ///  Thsis method is used to check Patient Code changed or not.
        ///  If changed we can update PatientCode in PA_Accounts_Patients
        /// </summary>
        /// <returns></returns>
        private bool IsPatientCodeModified()
        {
            bool _result = false;
            try
            {
                if (_PatientId != 0)
                {
                    if (txtPACode.Text.ToString().Trim() == oPatientDemo.PatientCode)
                    {
                        _result = false;
                    }
                    else
                    {
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }

        /// <summary>
        /// Method to Check  accountno exists or not.
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        private bool CheckAccountNoExistsForGuarantor(string accountNo)
        {

            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                string _strSqlQuery = "Select COUNT(*) from " +
                                            " (Select sAccountNo as sCode from PA_Accounts " +
                                            " Union " +
                                            " Select sPatientCode as sCode From Patient) as Main " +
                                            " Where sCode = ltrim(rtrim('" + accountNo.Trim().Replace("'", "''") + "'))";

                result = oDB.ExecuteScalar_Query(_strSqlQuery);

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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            if (Convert.ToInt64(result) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Method to get AccountData when accountfeature is disabled.
        /// When feature is off then then assign Primary guarantor to account. 
        /// 
        /// </summary>
        private void GetAccountDataForAccountFeatureDisabled()
        {
            //new mode
            if (_PatientId == 0)
            {
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    //for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                    for (int gIndex = oPatientGuarantors.Count-1; gIndex >= 0; gIndex--)
                    {

                        if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.SameAsPatient)
                        {
                            //SLR: Problem to be changed on 4/2/2014
                            //SLR: Problem to be addressed on 8/1/2014

                            //SG: Problem addressed, loop is made in reverse manner. 
                            //    Issue is addressed on 8/8/2014 [due to v8030 branch created on 8/6/2014]

                            oPatientGuarantors.RemoveAt(gIndex);
                            Boolean _shouldAdd = true;
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int Index = 0; Index < oPatientGuarantors.Count; Index++)
                                {
                                    if (oPatientGuarantors[Index].OtherConatctType == PatientOtherContactType.SameAsPatient)
                                    {
                                        _shouldAdd = false;
                                        break;
                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = _PatientId;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = txtPAFname.Text.Trim();
                                oGuarantor.MiddleName = txtPAMName.Text.Trim();
                                oGuarantor.LastName = txtPALName.Text.Trim();
                                if (mtxtPADOB.MaskCompleted == true)
                                { oGuarantor.DOB = Convert.ToDateTime(mtxtPADOB.Text); }

                                if (txtmPASSN.IsValidated == true)
                                    oGuarantor.SSN = txtmPASSN.Text.Trim();
                                else
                                    oGuarantor.SSN = "";

                                oGuarantor.Gender = cmbGender.Text.Trim();
                                oGuarantor.Relation = "";

                                oGuarantor.AddressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                                oGuarantor.AddressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                                oGuarantor.City = oAddresscontrol.txtCity.Text.Trim();
                                oGuarantor.County = oAddresscontrol.txtCounty.Text.Trim();
                                oGuarantor.Zip = oAddresscontrol.txtZip.Text.Trim();
                                oGuarantor.State = oAddresscontrol.cmbState.Text.Trim();
                                oGuarantor.Country = oAddresscontrol.cmbCountry.Text;

                                oGuarantor.Relation = "Self";
                                oGuarantor.Phone = mtxtPAPhone.Text.Trim();

                                if (mtxtPAMobile.IsValidated == true) { oGuarantor.Mobile = mtxtPAMobile.Text; }
                                oGuarantor.Email = txtPAEmail.Text.Trim();
                                oGuarantor.Fax = txtPAFax.Text.Trim();
                                oGuarantor.OtherConatctType = PatientOtherContactType.SameAsPatient;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;
                                oPatientGuarantors.Add(oGuarantor);
                            }

                        }
                    }
                }
            }

            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
            {
                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                {
                    //assign guarantor as account guarantor.
                    if (oPatientGuarantors[gIndex].IsAccountGuarantor == true)
                    {
                        //fill account  object
                        oAccount = new Account();
                        oPatientAccount = new PatientAccount();
                        oAccount.PAccountID = 0;
                        //Indicates that New Account
                        oAccount.IsExistingAccount = false;
                        oAccount.GuarantorID = 0;
                        oAccount.AccountNo = oPatientDemo.PatientCode;
                        oAccount.AccountDesc = "";
                        oAccount.SentToCollection = chkSetToCollection.Checked;
                        oAccount.ExcludeStatement = chkExcludefromStatement.Checked;
                        oAccount.AccountClosedDate = DateTime.MinValue;
                        oAccount.RecordDate = DateTime.Now;
                        oAccount.IsAccountFeatureEnabled = false;
                        oAccount.FirstName = oPatientGuarantors[gIndex].FirstName.ToString().Trim();
                        oAccount.LastName = oPatientGuarantors[gIndex].LastName.ToString().Trim();
                        oAccount.MiddleName = oPatientGuarantors[gIndex].MiddleName.ToString().Trim();
                        oAccount.AddressLine1 = oPatientGuarantors[gIndex].AddressLine1.ToString().Trim();
                        oAccount.AddressLine2 = oPatientGuarantors[gIndex].AddressLine2.ToString().Trim();
                        oAccount.Active = true;
                        oAccount.City = oPatientGuarantors[gIndex].City.ToString().Trim();
                        oAccount.Zip = oPatientGuarantors[gIndex].Zip.ToString().Trim();
                        oAccount.State = oPatientGuarantors[gIndex].State.ToString().Trim();
                        oAccount.Country = oPatientGuarantors[gIndex].Country.ToString().Trim();
                        oAccount.County = oPatientGuarantors[gIndex].County.ToString().Trim();
                        oAccount.ClinicID = _ClinicID;
                        oAccount.MachineName = System.Environment.MachineName;
                        oAccount.GuarantorCode = "";
                        oAccount.AreaCode = "";
                        oAccount.UserID = _UserID;
                        oAccount.EntityType = oPatientGuarantors[gIndex].GurantorType.GetHashCode();
                        oAccount.SiteID = 1;
                        break;

                    }
                }
            }
            //fill the patientaccount object
            oPatientAccount.AccountPatientID = 0;
            oPatientAccount.PatientID = _PatientId;
            oPatientAccount.AccountNo = oPatientDemo.PatientCode;
            oPatientAccount.PatientCode = oPatientDemo.PatientCode;
            oPatientAccount.AccountClosedDate = DateTime.MinValue;
            oPatientAccount.ClinicID = _ClinicID;
            oPatientAccount.SiteID = 1;
            oPatientAccount.UserID = _UserID;
            oPatientAccount.MachineName = System.Environment.MachineName;
            oPatientAccount.RecordDate = DateTime.Now;
            oPatientAccount.Active = true;
            oPatientAccount.OwnAccount = true;
        }

        /// <summary>
        /// Method to get account with SameAsPatient guarantor.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        private DataTable GetSameAsPatientAccountForSaveAsCopyPatient(Int64 patientId)
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                odbparam.Add("@nPatienID", patientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_GetSameAsPatientAccount", odbparam, out  dt);

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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (odbparam != null)
                {
                    odbparam.Dispose();
                    odbparam = null;
                }
            }
            return dt;

        }

        /// <summary>
        /// Method to get AccountData for SaveAs CopyPatient.When Account Feature Enabled then for SaveAs CopyPatient, defaulted 
        /// the existing SameAsPatient guarantor account of copied patient.When Account Feature is disabled then one account will be created with SameAsPatient guarantor
        /// </summary>
        public void GetAccountDataForSaveAsCopyPatient()
        {
            try
            {
                if (_IsPatientAccountFeature == true)
                {
                    txtAccountNo.Text = "";
                    txtAccountDescription.Text = "";
                    lblBusinessCenter.Text = "";
                    lblGuarantorDetails.Visible = true;
                    btnExistingAccountSelect.Visible = true;
                    btnExistingAccountDelete.Visible = true;
                    rbAccountNew.Visible = true;
                    rbAccountExisting.Checked = true;
                    rbAccountExisting.Visible = true;

                    btnCopyAccount.Visible = false;
                    btnAddAccount.Visible = false;
                    btnEditAccount.Visible = false;
                    cmbAccounts.Visible = false;
                    btnGuarantorExistingPatientBrowse.Visible = false;
                    btnNewGuarantor.Visible = false;
                    btnGuarantorClear.Visible = false;
                    chkExcludefromStatement.Visible = false;
                    chkSetToCollection.Visible = false;
                    lblSameAsGuardian.Visible = false;
                    cmbSameAsGuardian.Visible = false;

                    lblGuarantorDetails.Height = 53;
                    lblGuarantorDetails.Width = 275;


                    txtAccGuarantor.Visible = false;

                    oPatientGuarantors = new PatientOtherContacts();

                    //Copy all Patient other gurantors when copy patient .
                    if (PatientOtherGuarantors != null && PatientOtherGuarantors.Count > 0)
                    {
                        for (int i = 0; i < PatientOtherGuarantors.Count; i++)
                        {
                            PatientOtherGuarantors[i].PatientContactID = 0;
                        }
                    }

                    DataTable dt = GetSameAsPatientAccountForSaveAsCopyPatient(_PatientId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtAccountNo.Tag = dt.Rows[0]["nPAccountID"].ToString();
                        txtAccountNo.Text = dt.Rows[0]["sAccountNo"].ToString();
                        txtAccountDescription.Text = dt.Rows[0]["sAccountDesc"].ToString();
                        //fill guarantor details
                        string guarantordetails = dt.Rows[0]["sFirstName"].ToString().Trim() + ' ' + dt.Rows[0]["sMiddleName"].ToString() + ' ' + dt.Rows[0]["sLastName"].ToString() + Environment.NewLine;

                        if (dt.Rows[0]["sAddressLine1"].ToString() != "")
                            guarantordetails = guarantordetails + dt.Rows[0]["sAddressLine1"].ToString() + ',';

                        if (dt.Rows[0]["sAddressLine2"].ToString() != "")
                            guarantordetails = guarantordetails + dt.Rows[0]["sAddressLine2"].ToString() + ',' + Environment.NewLine;
                        else { guarantordetails = guarantordetails + Environment.NewLine; }

                        if (dt.Rows[0]["sCity"].ToString() != "")
                            guarantordetails = guarantordetails + dt.Rows[0]["sCity"].ToString() + ' ' + dt.Rows[0]["sState"].ToString() + ' ' + dt.Rows[0]["sZip"].ToString();

                        lblGuarantorDetails.Text = guarantordetails;

                        if (Convert.ToString(dt.Rows[0]["sBusinessCenterCode"]) != "")
                        {
                            lblBusinessCenter.Text = Convert.ToString(dt.Rows[0]["sBusinessCenterCode"]);
                        }
                        else
                        {
                            lblBusinessCenter.Text = "";
                        }

                    }
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                    pnlPatientOtherGuarantorInfo.Top = pnlPatientOtherGuarantorInfo.Top + 28;

                }
                else if (_IsPatientAccountFeature == false)
                {

                    oPatientGuarantors = new PatientOtherContacts();

                    //Copy all Patient other gurantors when copy patient .
                    if (PatientOtherGuarantors != null && PatientOtherGuarantors.Count > 0)
                    {
                        for (int i = 0; i < PatientOtherGuarantors.Count; i++)
                        {
                            PatientOtherGuarantors[i].PatientContactID = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }


        }

        /// <summary>
        /// Method to create SameAsPatientGuarantor.
        /// </summary>
        public void GetSameAsPatientGuarantor()
        {
            try
            {
                // new mode
                if (_PatientId == 0)
                {

                    if (cmbSameAsGuardian.Text == "" && _IsSaveAsCopy == true)
                    {
                        if (oPatientGuarantors == null || oPatientGuarantors.Count == 0)
                        {
                            cmbSameAsGuardian.Text = "Patient";
                        }
                    }

                    #region " Patient "

                    if (cmbSameAsGuardian.Text == "Patient")
                    {
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            //for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                            for (int gIndex = oPatientGuarantors.Count - 1; gIndex >= 0; gIndex--)
                            {
                                //SLR: Problem in the logic to be changed on 4/2/2014
                                //SLR: Problem in the logic to be changed on 8/1/2014

                                //SG: Problem addressed, loop is made in reverse manner. 
                                //Issue is addressed on 8/8/2014 [due to v8030 branch created on 8/6/2014]
	
                                if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.SameAsPatient)
                                {
                                    oPatientGuarantors.RemoveAt(gIndex);
                                }
                            }
                        }

                        if (txtPAFname.Text.Trim() != "" && txtPALName.Text.Trim() != "")
                        {
                            bool _shouldAdd = true;

                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.SameAsPatient)
                                    {
                                        _shouldAdd = false;
                                        break;
                                    }
                                }
                            }
                            if ((oPatientGuarantors != null) && (_shouldAdd == true))    //null condition checked for copy patient issue
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);

                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = _PatientId;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = txtPAFname.Text.Trim();
                                oGuarantor.MiddleName = txtPAMName.Text.Trim();  
                                oGuarantor.LastName = txtPALName.Text.Trim();
                                if (mtxtPADOB.MaskCompleted == true)
                                { oGuarantor.DOB = Convert.ToDateTime(mtxtPADOB.Text); }

                                if (txtmPASSN.IsValidated == true)
                                    oGuarantor.SSN = txtmPASSN.Text.Trim();
                                else
                                    oGuarantor.SSN = "";

                                oGuarantor.Gender = cmbGender.Text.Trim();

                                oGuarantor.AddressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                                oGuarantor.AddressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                                oGuarantor.City = oAddresscontrol.txtCity.Text.Trim();
                                oGuarantor.County = oAddresscontrol.txtCounty.Text.Trim();
                                oGuarantor.Zip = oAddresscontrol.txtZip.Text.Trim();
                                oGuarantor.State = oAddresscontrol.cmbState.Text.Trim();
                                oGuarantor.Country = oAddresscontrol.cmbCountry.Text;
                                oGuarantor.County = oAddresscontrol.txtCounty.Text.Trim();

                                oGuarantor.Relation = "Self";
                                oGuarantor.Phone = mtxtPAPhone.Text.Trim();

                                if (mtxtPAMobile.IsValidated == true) { oGuarantor.Mobile = mtxtPAMobile.Text; }
                                oGuarantor.Email = txtPAEmail.Text.Trim();
                                oGuarantor.Fax = txtPAFax.Text.Trim();
                                oGuarantor.OtherConatctType = PatientOtherContactType.SameAsPatient;

                                oGuarantor.GurantorType = GuarantorType.Personal;
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;
                                oPatientGuarantors.Add(oGuarantor);
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                }
                            }
                        }
                    }

                    #endregion

                    #region " Mother "

                    if (cmbSameAsGuardian.Text == "Mother")
                    {
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            //for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                            for (int gIndex = oPatientGuarantors.Count- 1; gIndex >= 0; gIndex--)
                            {
                                //SLR: To be changed on 4/2/2014
                                //SLR: Problem in the logic to be changed on 8/1/2014

                                //SG: Problem addressed, loop is made in reverse manner. 
                                //Issue is addressed on 8/8/2014 [due to v8030 branch created on 8/6/2014]

                                if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.Mother)
                                {
                                    oPatientGuarantors.RemoveAt(gIndex);
                                }
                            }
                        }

                        if (_PatientGuardian.PatientMotherFirstName.Trim() != "" && _PatientGuardian.PatientMotherLastName.Trim() != "")
                        {
                            bool _shouldAdd = true;
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.Mother)
                                    {
                                        _shouldAdd = false;
                                        break;

                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = _PatientGuardian.PatientMotherFirstName.Trim();
                                oGuarantor.MiddleName = _PatientGuardian.PatientMotherMiddleName.Trim();
                                oGuarantor.LastName = _PatientGuardian.PatientMotherLastName.Trim();
                                oGuarantor.Relation = "Mother";
                                oGuarantor.Gender = "Female";

                                oGuarantor.AddressLine1 = _PatientGuardian.PatientMotherAddress1.Trim().ToString();
                                oGuarantor.AddressLine2 = _PatientGuardian.PatientMotherAddress2.Trim().ToString();
                                oGuarantor.City = _PatientGuardian.PatientMotherCity.Trim().ToString();
                                oGuarantor.County = _PatientGuardian.PatientMotherCounty.Trim().ToString();
                                oGuarantor.Zip = _PatientGuardian.PatientMotherZip.Trim().ToString();
                                oGuarantor.State = _PatientGuardian.PatientMotherState.Trim().ToString();
                                oGuarantor.Country = _PatientGuardian.PatientMotherCountry.Trim().ToString();
                                oGuarantor.Phone = _PatientGuardian.PatientMotherPhone.Trim().ToString();
                                oGuarantor.Mobile = _PatientGuardian.PatientMotherMobile.Trim().ToString();
                                oGuarantor.Email = _PatientGuardian.PatientMotherEmail.Trim().ToString();
                                oGuarantor.Fax = _PatientGuardian.PatientMotherFAX.Trim().ToString();
                                oGuarantor.OtherConatctType = PatientOtherContactType.Mother;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;
                                if (oPatientGuarantors.Count == 0)
                                {
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }
                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
                            }


                        }
                    }

                    #endregion

                    #region " Father "

                    if (cmbSameAsGuardian.Text == "Father")
                    {
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            //for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                            for (int gIndex = oPatientGuarantors.Count - 1; gIndex >=0; gIndex--)
                            {
                                //SLR: To be changed on 4/2/2014
                                //SLR: Problem in the logic to be changed on 8/1/2014

                                //SG: Problem addressed, loop is made in reverse manner. 
                                //Issue is addressed on 8/8/2014 [due to v8030 branch created on 8/6/2014]

                                if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.Father)
                                {
                                    oPatientGuarantors.RemoveAt(gIndex);
                                }
                            }
                        }

                        if (_PatientGuardian.PatientFatherFirstName.Trim() != "" && _PatientGuardian.PatientFatherLastName.Trim() != "")
                        {
                            bool _shouldAdd = true;

                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.Father)
                                    {
                                        _shouldAdd = false;
                                        break;

                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = _PatientGuardian.PatientFatherFirstName;
                                oGuarantor.MiddleName = _PatientGuardian.PatientFatherMiddleName.Trim();
                                oGuarantor.LastName = _PatientGuardian.PatientFatherLastName.Trim();
                                oGuarantor.Relation = "Father";
                                oGuarantor.Gender = "Male";

                                oGuarantor.AddressLine1 = _PatientGuardian.PatientFatherAddress1.Trim().ToString();
                                oGuarantor.AddressLine2 = _PatientGuardian.PatientFatherAddress2.Trim().ToString();
                                oGuarantor.City = _PatientGuardian.PatientFatherCity.Trim().ToString();
                                oGuarantor.County = _PatientGuardian.PatientFatherCounty.Trim().ToString();
                                oGuarantor.Zip = _PatientGuardian.PatientFatherZip.Trim().ToString();
                                oGuarantor.State = _PatientGuardian.PatientFatherState.Trim().ToString();
                                oGuarantor.Country = _PatientGuardian.PatientFatherCountry.Trim().ToString();


                                oGuarantor.Phone = _PatientGuardian.PatientFatherPhone.Trim().ToString();
                                oGuarantor.Mobile = _PatientGuardian.PatientFatherMobile.Trim().ToString();

                                oGuarantor.Email = _PatientGuardian.PatientFatherEmail.Trim().ToString();
                                oGuarantor.Fax = _PatientGuardian.PatientFatherFAX.Trim().ToString();
                                oGuarantor.OtherConatctType = PatientOtherContactType.Father;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;

                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;

                                if (oPatientGuarantors.Count == 0)
                                {
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }
                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
                            }

                        }
                    }

                    #endregion

                    #region "Other Guardian"

                    if (cmbSameAsGuardian.Text == "Other Guardian")
                    {
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            //for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                            for (int gIndex = oPatientGuarantors.Count-1; gIndex >= 0; gIndex--)
                            {
                                //SLR: To be Changed on 4/2/2014
                                //SLR: Problem in the logic to be changed on 8/1/2014

                                //SG: Problem addressed, loop is made in reverse manner. 
                                //Issue is addressed on 8/8/2014 [due to v8030 branch created on 8/6/2014]

                                if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.OtherGuardian)
                                {
                                    oPatientGuarantors.RemoveAt(gIndex);
                                }
                            }
                        }

                        if (_PatientGuardian.PatientGuardianFirstName.Trim() != "" && _PatientGuardian.PatientGuardianLastName.Trim() != "")
                        {
                            bool _shouldAdd = true;

                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.OtherGuardian)
                                    {
                                        _shouldAdd = false;
                                        break;
                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = _PatientGuardian.PatientGuardianFirstName.Trim();
                                oGuarantor.MiddleName = _PatientGuardian.PatientGuardianMiddleName.Trim();
                                oGuarantor.LastName = _PatientGuardian.PatientGuardianLastName.Trim();
                                oGuarantor.Relation = _PatientGuardian.PatientGuardianRelationDS.Trim().ToString();

                                oGuarantor.AddressLine1 = _PatientGuardian.PatientGuardianAddress1.Trim().ToString();
                                oGuarantor.AddressLine2 = _PatientGuardian.PatientGuardianAddress2.Trim().ToString();
                                oGuarantor.City = _PatientGuardian.PatientGuardianCity.Trim().ToString();
                                oGuarantor.County = _PatientGuardian.PatientGuardianCounty.Trim().ToString();
                                oGuarantor.Zip = _PatientGuardian.PatientGuardianZip.Trim().ToString();
                                oGuarantor.State = _PatientGuardian.PatientGuardianState.Trim().ToString();
                                oGuarantor.Country = _PatientGuardian.PatientGuardianCountry.Trim().ToString();


                                oGuarantor.Phone = _PatientGuardian.PatientGuardianPhone.Trim().ToString();
                                oGuarantor.Mobile = _PatientGuardian.PatientGuardianMobile.Trim().ToString();

                                oGuarantor.Email = _PatientGuardian.PatientGuardianEmail.Trim().ToString();
                                oGuarantor.Fax = _PatientGuardian.PatientGuardianFAX.Trim().ToString();
                                oGuarantor.OtherConatctType = PatientOtherContactType.OtherGuardian;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;

                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;
                                if (oPatientGuarantors.Count == 0)
                                {
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }
                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
                            }
                        }
                    }

                    #endregion

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

        }

        /// <summary>
        /// Method to set CmbSameAsGuardian Selected index
        /// </summary>
        private void setCmbSameAsGuardianIndex()
        {
            try
            {
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;

                        IsCmbSameAsGuardianLoadFlag = false;
                        if (oPatientGuarantors[gindex].OtherConatctType.GetHashCode() == PatientOtherContactType.SameAsPatient.GetHashCode())
                            cmbSameAsGuardian.SelectedIndex = cmbSameAsGuardian.Items.IndexOf("Patient");
                        else if (oPatientGuarantors[gindex].OtherConatctType.GetHashCode() == PatientOtherContactType.Mother.GetHashCode())
                            cmbSameAsGuardian.SelectedIndex = cmbSameAsGuardian.Items.IndexOf("Mother");
                        else if (oPatientGuarantors[gindex].OtherConatctType.GetHashCode() == PatientOtherContactType.Father.GetHashCode())
                            cmbSameAsGuardian.SelectedIndex = cmbSameAsGuardian.Items.IndexOf("Father");
                        else if (oPatientGuarantors[gindex].OtherConatctType.GetHashCode() == PatientOtherContactType.OtherGuardian.GetHashCode())
                            cmbSameAsGuardian.SelectedIndex = cmbSameAsGuardian.Items.IndexOf("Other Guardian");
                        else
                            cmbSameAsGuardian.SelectedIndex = -1;
                        IsCmbSameAsGuardianLoadFlag = true;
                    }
                }
                else
                {
                    IsCmbSameAsGuardianLoadFlag = false;
                    cmbSameAsGuardian.SelectedIndex = -1;
                    IsCmbSameAsGuardianLoadFlag = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region "Patient other Guarantors related code"

        private void btnOtherGuarantorExistingPatientBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, true, this.Width);
                oListControl.ControlHeader = "Guarantor";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_OtherGaurantorSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                //To allow the user to add multiple guarantors at one time 

                DataTable dtMultipleGarantor = (DataTable)cmbOtherGuarantor.DataSource;
                if (dtMultipleGarantor != null && dtMultipleGarantor.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMultipleGarantor.Rows.Count; i++)
                    {
                        if (Convert.ToInt64(dtMultipleGarantor.Rows[i]["Id"]) > 0)
                        {
                            oListControl.SelectedItems.Add(Convert.ToInt64(dtMultipleGarantor.Rows[i]["Id"]), Convert.ToString(dtMultipleGarantor.Rows[i]["Description"]));
                        }
                    }
                }

                //for (int i = 0; i < cmbOtherGuarantor.Items.Count; i++)
                //{
                //    cmbOtherGuarantor.SelectedIndex = i;
                //    //Existing patient as guarantor(based on patienid)
                //    if (Convert.ToInt64(cmbOtherGuarantor.SelectedValue.ToString()) > 0)
                //        oListControl.SelectedItems.Add(Convert.ToInt64(cmbOtherGuarantor.SelectedValue.ToString()), cmbOtherGuarantor.Text);
                //}


                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void oListControl_OtherGaurantorSelectedClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dtGuarantor = new DataTable();
                DataColumn dcId = new DataColumn("Id");
                DataColumn dcDescription = new DataColumn("Description");
                dtGuarantor.Columns.Add(dcId);
                dtGuarantor.Columns.Add(dcDescription);

                Int64 _TempPatientID = 0;
               

                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        _TempPatientID = Convert.ToInt64(oListControl.SelectedItems[i].ID);

                        bool _ShouldAdd = true;

                        for (int _Count = 0; _Count < oPatientOtherGuarantors.Count; _Count++)
                        {
                            if (Convert.ToInt64(oListControl.SelectedItems[i].ID) == oPatientOtherGuarantors[_Count].GuarantorAsPatientID)
                            {
                                _ShouldAdd = false;
                                break;
                            }
                        }
                        if (_ShouldAdd == true)
                        {
                            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                            Patient oPatientTemp = ogloPatient.GetPatientDemo(_TempPatientID);

                            if (oPatientTemp != null)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();

                                oGuarantor.GuarantorAsPatientID = _TempPatientID;
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = oPatientTemp.DemographicsDetail.PatientFirstName;
                                oGuarantor.MiddleName = oPatientTemp.DemographicsDetail.PatientMiddleName;
                                oGuarantor.LastName = oPatientTemp.DemographicsDetail.PatientLastName;
                                oGuarantor.DOB = oPatientTemp.DemographicsDetail.PatientDOB;
                                oGuarantor.SSN = oPatientTemp.DemographicsDetail.PatientSSN;
                                oGuarantor.Gender = oPatientTemp.DemographicsDetail.PatientGender;
                                oGuarantor.Relation = "";
                                oGuarantor.AddressLine1 = oPatientTemp.DemographicsDetail.PatientAddress1;
                                oGuarantor.AddressLine2 = oPatientTemp.DemographicsDetail.PatientAddress2;
                                oGuarantor.City = oPatientTemp.DemographicsDetail.PatientCity;
                                oGuarantor.State = oPatientTemp.DemographicsDetail.PatientState;
                                oGuarantor.Zip = oPatientTemp.DemographicsDetail.PatientZip;
                                oGuarantor.Country = oPatientTemp.DemographicsDetail.PatientCountry;
                                oGuarantor.County = oPatientTemp.DemographicsDetail.PatientCounty;
                                oGuarantor.Phone = oPatientTemp.DemographicsDetail.PatientPhone;
                                oGuarantor.Mobile = oPatientTemp.DemographicsDetail.PatientMobile;
                                oGuarantor.Email = oPatientTemp.DemographicsDetail.PatientEmail;
                                oGuarantor.Fax = oPatientTemp.DemographicsDetail.PatientFax;
                                oGuarantor.OtherConatctType = PatientOtherContactType.Patient;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlagForOtherGuarantor();
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = false;
                                oPatientOtherGuarantors.Add(oGuarantor);
                                oPatientTemp.Dispose();
                                oPatientTemp = null;
                                oGuarantor.Dispose();
                                oGuarantor = null;
                            }
                            ogloPatient.Dispose();
                            ogloPatient = null;
                        }

                    }
                }

                for (int i = 0; i < oPatientOtherGuarantors.Count; i++)
                {
                    DataRow drTemp = dtGuarantor.NewRow();
                    drTemp["Id"] = oPatientOtherGuarantors[i].GuarantorAsPatientID;
                    drTemp["Description"] = oPatientOtherGuarantors[i].FirstName + " " + oPatientOtherGuarantors[i].MiddleName + " " + oPatientOtherGuarantors[i].LastName;
                    dtGuarantor.Rows.Add(drTemp);
                }
             //   cmbOtherGuarantor.Items.Clear();
                cmbOtherGuarantor.DataSource = null;
                cmbOtherGuarantor.Items.Clear();
                cmbOtherGuarantor.DataSource = dtGuarantor;
                cmbOtherGuarantor.ValueMember = dtGuarantor.Columns["Id"].ColumnName;
                cmbOtherGuarantor.DisplayMember = dtGuarantor.Columns["Description"].ColumnName;
           //     _isPatientOtherGuarantorModified = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                onDemographicControl_Enter(sender, e);
            }
            cmbOtherGuarantor.Focus();
        }

        private void btnotherNewGuarantor_Click(object sender, EventArgs e)
        {
            try
            {
                RemovePatientOtherGuarantorControl();
                ogloPatientOtherGuarantorControl = new gloPatientGuarantorControl(_databaseconnectionstring);
                ogloPatientOtherGuarantorControl.PatientGuarantors = oPatientOtherGuarantors;
                ogloPatientOtherGuarantorControl.FromAccountGuarantor = false;
                ogloPatientOtherGuarantorControl.SaveButton_Click += new gloPatientGuarantorControl.SaveButtonClick(ogloPatientOtherGuarantorControl_SaveButton_Click);
                ogloPatientOtherGuarantorControl.CloseButton_Click += new gloPatientGuarantorControl.CloseButtonClick(ogloPatientOtherGuarantorControl_CloseButton_Click);
                this.Controls.Add(ogloPatientOtherGuarantorControl);
                ogloPatientOtherGuarantorControl.Dock = DockStyle.Fill;
                ogloPatientOtherGuarantorControl.BringToFront();
                onDemographicControl_Leave(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ogloPatientOtherGuarantorControl_SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                oPatientOtherGuarantors = ogloPatientOtherGuarantorControl.PatientGuarantors;
                if (oPatientOtherGuarantors != null && oPatientOtherGuarantors.Count > 0)
                {

                    DataTable dtGuarantor = new DataTable();
                    DataColumn dcId = new DataColumn("Id");
                    DataColumn dcDescription = new DataColumn("Description");
                    dtGuarantor.Columns.Add(dcId);
                    dtGuarantor.Columns.Add(dcDescription);

                    for (int i = 0; i < oPatientOtherGuarantors.Count; i++)
                    {
                        DataRow drTemp = dtGuarantor.NewRow();
                        drTemp["Id"] = oPatientOtherGuarantors[i].GuarantorAsPatientID;
                        drTemp["Description"] = oPatientOtherGuarantors[i].FirstName + " " + oPatientOtherGuarantors[i].MiddleName + " " + oPatientOtherGuarantors[i].LastName;
                        dtGuarantor.Rows.Add(drTemp);
                    }
                    cmbOtherGuarantor.DataSource = null;
                    cmbOtherGuarantor.Items.Clear();
                    cmbOtherGuarantor.ValueMember = dtGuarantor.Columns["Id"].ColumnName;
                    cmbOtherGuarantor.DisplayMember = dtGuarantor.Columns["Description"].ColumnName;
                    cmbOtherGuarantor.DataSource = dtGuarantor;
                }
                else
                {
                    cmbOtherGuarantor.DataSource = null;
                    cmbOtherGuarantor.Items.Clear();
                }
                //this.Controls.Remove(ogloPatientOtherGuarantorControl);
                //if (ogloPatientOtherGuarantorControl != null) { ogloPatientOtherGuarantorControl.Dispose(); }
                RemovePatientOtherGuarantorControl();
         //       _isPatientOtherGuarantorModified = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                onDemographicControl_Enter(sender, e);
            }
        }
        private void RemovePatientOtherGuarantorControl()
        {
            try
            {
                if (ogloPatientOtherGuarantorControl != null)
                {
                    if (ogloPatientOtherGuarantorControl.pnlAddresssControl.Controls.Count > 0)
                    {
                        Control[] myControl = ogloPatientOtherGuarantorControl.pnlAddresssControl.Controls.Find("PatientOtherGuarantorAddressControl", true);
                        if (myControl.Length > 0)
                        {
                            ((gloAddress.gloAddressControl)myControl[0]).ControlClosing = true;
                            ogloPatientOtherGuarantorControl.pnlAddresssControl.Controls.Remove(myControl[0]);
                        }
                    }
                    this.Controls.Remove(ogloPatientOtherGuarantorControl);
                    try
                    {
                        ogloPatientOtherGuarantorControl.SaveButton_Click -= new gloPatientGuarantorControl.SaveButtonClick(ogloPatientOtherGuarantorControl_SaveButton_Click);
                        ogloPatientOtherGuarantorControl.CloseButton_Click -= new gloPatientGuarantorControl.CloseButtonClick(ogloPatientOtherGuarantorControl_CloseButton_Click);
                    }
                    catch
                    {
                    }

                    ogloPatientOtherGuarantorControl.Dispose();
                }
            }
            catch //(Exception ex)
            {

            }
        }

        private void ogloPatientOtherGuarantorControl_CloseButton_Click(object sender, EventArgs e)
        {
            RemovePatientOtherGuarantorControl();
            onDemographicControl_Enter(sender, e);
        }

        private PatientOtherContact.GuarantorTypeFlag GetNextTypeFlagForOtherGuarantor()
        {
            PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.None;

            bool isPrimaryPresent = false;
            bool isSecondaryPresent = false;
            bool isTertioryPresent = false;

            if (oPatientOtherGuarantors.Count != 0)
            {
                for (int i = 0; i < oPatientOtherGuarantors.Count; i++)
                {
                    if (oPatientOtherGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode())
                    { isPrimaryPresent = true; }
                    else if (oPatientOtherGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode())
                    { isSecondaryPresent = true; }
                    else if (oPatientOtherGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode())
                    { isTertioryPresent = true; }
                }

                if (!isPrimaryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary; }
                else if (!isSecondaryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Secondary; }
                else if (!isTertioryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary; }
                else
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary; }
            }
            else
            {
                _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary;
            }
            return _GuarantorTypeFlag;
        }

        private void GetPatientOtherGuarantors()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                DataTable dtAccGuarantors;
                string _strSqlQuery = "SELECT nPatientID, nPatientContactID, nLineNumber,ISNULL(nPatientContactType,0) AS nPatientContactType, "
                            + " ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,"
                            + " nDOB,ISNULL(sSSN,'') AS sSSN,ISNULL(sGender,'') AS sGender,ISNULL(sRelation,'') As sRelation,ISNULL(sAddressLine1,'') As sAddressLine1,"
                            + " ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS sZIP,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                            + " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFax,'') AS sFax,ISNULL(sEmail,'') AS sEmail,"
                            + " ISNULL(nVisitID,0) AS nVisitID,ISNULL(nAppointmentID,0) As nAppointmentID,ISNULL(bIsActive,'false') As  bIsActive,ISNULL(nGuarantorAsPatientID,0) AS nGuarantorAsPatientID,ISNULL(nPatientContactTypeFlag,4) AS nPatientContactTypeFlag,nClinicID,nPAccountID,ISNULL(nGuarantorType,1) as nGuarantorType,bIsAccountGuarantor"
                            + " FROM Patient_OtherContacts WHERE nPatientID = " + PatientId + " AND ISNULL(nClinicID,1) = " + _ClinicID + " AND nPAccountID != 0 ORDER BY nPatientContactTypeFlag";

                oDB.Retrive_Query(_strSqlQuery, out dtAccGuarantors);
                PatientGuarantors = new PatientOtherContacts();
                if (dtAccGuarantors != null && dtAccGuarantors.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAccGuarantors.Rows.Count; i++)
                    {
                        PatientOtherContact oGuarantor = new PatientOtherContact();
                        oGuarantor.PatientID = PatientId;
                        oGuarantor.PatientContactID = Convert.ToInt64(dtAccGuarantors.Rows[i]["nPatientContactID"]);
                        oGuarantor.FirstName = Convert.ToString(dtAccGuarantors.Rows[i]["sFirstName"]);
                        oGuarantor.MiddleName = Convert.ToString(dtAccGuarantors.Rows[i]["sMiddleName"]);
                        oGuarantor.LastName = Convert.ToString(dtAccGuarantors.Rows[i]["sLastName"]);
                        if (dtAccGuarantors.Rows[i]["nDOB"] != null && dtAccGuarantors.Rows[i]["nDOB"].ToString() != "")
                        {
                            oGuarantor.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAccGuarantors.Rows[i]["nDOB"]));
                        }
                        oGuarantor.SSN = Convert.ToString(dtAccGuarantors.Rows[i]["sSSN"]);
                        oGuarantor.Relation = Convert.ToString(dtAccGuarantors.Rows[i]["sRelation"]);
                        oGuarantor.Gender = Convert.ToString(dtAccGuarantors.Rows[i]["sGender"]);
                        oGuarantor.AddressLine1 = Convert.ToString(dtAccGuarantors.Rows[i]["sAddressLine1"]);
                        oGuarantor.AddressLine2 = Convert.ToString(dtAccGuarantors.Rows[i]["sAddressLine2"]);
                        oGuarantor.City = Convert.ToString(dtAccGuarantors.Rows[i]["sCity"]);
                        oGuarantor.State = Convert.ToString(dtAccGuarantors.Rows[i]["sState"]);

                        oGuarantor.County = Convert.ToString(dtAccGuarantors.Rows[i]["sCounty"]);
                        oGuarantor.Country = Convert.ToString(dtAccGuarantors.Rows[i]["sCountry"]);

                        oGuarantor.Zip = Convert.ToString(dtAccGuarantors.Rows[i]["sZIP"]);
                        oGuarantor.Phone = Convert.ToString(dtAccGuarantors.Rows[i]["sPhone"]);
                        oGuarantor.Mobile = Convert.ToString(dtAccGuarantors.Rows[i]["sMobile"]);
                        oGuarantor.Email = Convert.ToString(dtAccGuarantors.Rows[i]["sEmail"]);
                        oGuarantor.Fax = Convert.ToString(dtAccGuarantors.Rows[i]["sFax"]);
                        oGuarantor.IsActive = Convert.ToBoolean(dtAccGuarantors.Rows[i]["bIsActive"]);
                        oGuarantor.VisitID = Convert.ToInt64(dtAccGuarantors.Rows[i]["nVisitID"]);
                        oGuarantor.AppointmentID = Convert.ToInt64(dtAccGuarantors.Rows[i]["nAppointmentID"]);
                        oGuarantor.GuarantorAsPatientID = Convert.ToInt64(dtAccGuarantors.Rows[i]["nGuarantorAsPatientID"]);
                        oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtAccGuarantors.Rows[i]["nPatientContactTypeFlag"]);
                        oGuarantor.OtherConatctType = (PatientOtherContactType)Convert.ToInt32(dtAccGuarantors.Rows[i]["nPatientContactType"]);

                        oGuarantor.PAccountID = Convert.ToInt64(dtAccGuarantors.Rows[i]["nPAccountID"]);
                        oGuarantor.GurantorType = (GuarantorType)Convert.ToInt32(dtAccGuarantors.Rows[i]["nGuarantorType"]);
                        oGuarantor.IsAccountGuarantor = Convert.ToBoolean(dtAccGuarantors.Rows[i]["bIsAccountGuarantor"]);
                        PatientGuarantors.Add(oGuarantor);
                    }
                }
                if (dtAccGuarantors != null)
                {
                    dtAccGuarantors.Dispose();
                    dtAccGuarantors = null;
                }
                //select account guarnator as patient otherguarantor then delete that guarnator in patient otherguarantors
                if (PatientOtherGuarantors != null && PatientOtherGuarantors.Count > 0)
                {
                    //for (int i = 0; i < PatientOtherGuarantors.Count; i++)
                    //{
                    for (int i = PatientOtherGuarantors.Count - 1; i >= 0; i--)
                    {
                        if (PatientOtherGuarantors[i].PatientContactID != 0)
                        {
                            for (int j = 0; j < PatientGuarantors.Count; j++)
                            {
                                if (PatientOtherGuarantors.Count > 0)
                                {
                                    //SLR: To be changed on 4/2/2014

                                    //SG: 8/8/2014 No change required here, the RemoveAt is done on "PatientOtherGuarantors" which already
                                    //     run in reverse loop above the next for loop for "PatientGuarantors" is just used to 
                                    //     compare object value (no matter if runnig forward or reverse

                                    if (PatientOtherGuarantors[i].PatientContactID == PatientGuarantors[j].PatientContactID)
                                    {
                                        PatientOtherGuarantors.RemoveAt(i);
                                    }
                                }
                            }

                        }
                    }
                }
                if (oPatientOtherGuarantors != null && oPatientOtherGuarantors.Count > 0)
                {
                    DataTable dtGuarantor = new DataTable();
                    DataColumn dcId = new DataColumn("Id");
                    DataColumn dcDescription = new DataColumn("Description");
                    dtGuarantor.Columns.Add(dcId);
                    dtGuarantor.Columns.Add(dcDescription);

                    for (int i = 0; i < oPatientOtherGuarantors.Count; i++)
                    {
                        DataRow drTemp = dtGuarantor.NewRow();
                        drTemp["Id"] = oPatientOtherGuarantors[i].GuarantorAsPatientID;
                        drTemp["Description"] = oPatientOtherGuarantors[i].FirstName + " " + ((oPatientOtherGuarantors[i].MiddleName != "") ? oPatientOtherGuarantors[i].MiddleName + " " : "") + oPatientOtherGuarantors[i].LastName;//Guarantor name 
                        dtGuarantor.Rows.Add(drTemp);
                    }
                  //  cmbOtherGuarantor.Items.Clear();
                    cmbOtherGuarantor.DataSource = null;
                    cmbOtherGuarantor.Items.Clear();
                    cmbOtherGuarantor.DataSource = dtGuarantor;

                    cmbOtherGuarantor.ValueMember = dtGuarantor.Columns["Id"].ColumnName;
                    cmbOtherGuarantor.DisplayMember = dtGuarantor.Columns["Description"].ColumnName;

                }
                else
                {
                   // cmbOtherGuarantor.Items.Clear();
                    cmbOtherGuarantor.DataSource = null;
                    cmbOtherGuarantor.Items.Clear();
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        #endregion

        public void DisposeAllControls()
        {
            try
            {
                //05-Jan-15 Aniket: Resolving Bug #79101: gloEMR - Copy Patient - Application is prompting for Duplicate Patient Code even if user has changed patient code.
                this.txtPACode.Validating -= new System.ComponentModel.CancelEventHandler(this.txtPACode_Validating);

                if (ogloPatientGuardianControl != null) { ogloPatientGuardianControl.DisposeAllControls(); ogloPatientGuardianControl.Dispose(); ogloPatientGuardianControl = null; }

                if (ogloPatientInsuranceControl != null)
                {

                    ogloPatientInsuranceControl.DisposeAllControls();
                    ogloPatientInsuranceControl.Dispose();
                    ogloPatientInsuranceControl = null;
                }
                if (ogloPatientOccupationControl != null) { ogloPatientOccupationControl.DisposeAllControls(); ogloPatientOccupationControl.Dispose(); ogloPatientOccupationControl = null; }
                if (ogloPatientOtherGuarantorControl != null) { ogloPatientOtherGuarantorControl.DisposeAllControls(); ogloPatientOtherGuarantorControl.Dispose(); ogloPatientOtherGuarantorControl = null; }
                if (ogloPatientOtherInfoCntrl != null) { ogloPatientOtherInfoCntrl.DisposeAllControls(); ogloPatientOtherInfoCntrl.Dispose(); ogloPatientOtherInfoCntrl = null; }
                if (ogloPAGuarantorControl != null) { ogloPAGuarantorControl.DisposeAllControls(); ogloPAGuarantorControl.Dispose(); ogloPAGuarantorControl = null; }


                if (ogloPAGuarantorControl != null) { ogloPAGuarantorControl.Dispose(); ogloPAGuarantorControl = null; }
                if (ogloPatientGuardianControl != null) { ogloPatientGuardianControl.Dispose(); ogloPatientGuardianControl = null; }
                if (ogloPatientInsuranceControl != null) { ogloPatientInsuranceControl.Dispose(); ogloPatientInsuranceControl = null; }
                if (ogloPatientOccupationControl != null) { ogloPatientOccupationControl.Dispose(); ogloPatientOccupationControl = null; }
                if (ogloPatientOtherGuarantorControl != null) { ogloPatientOtherGuarantorControl.Dispose(); ogloPatientOtherGuarantorControl = null; }
                if (ogloPatientOtherInfoCntrl != null) { ogloPatientOtherInfoCntrl.Dispose(); ogloPatientOtherInfoCntrl = null; }

                if (oListControl != null) { removeOListControl(); }

                if (_PatientInsurance != null) { _PatientInsurance.Dispose(); _PatientInsurance = null; }
                if (_PatientGuardian != null) { _PatientGuardian.Dispose(); _PatientGuardian = null; }
                if (_PatientOccupation != null) { _PatientOccupation.Dispose(); _PatientOccupation = null; }
                if (_PatientWorkersComp != null) { _PatientWorkersComp.Clear(); _PatientWorkersComp.Dispose(); _PatientWorkersComp = null; }
                if (_PatientDemographicOtherInfo != null) { _PatientDemographicOtherInfo.Dispose(); _PatientDemographicOtherInfo = null; }

                if (oPatientDemo != null) { oPatientDemo.Dispose(); oPatientDemo = null; }


                if (PatientAccounts != null) { PatientAccounts.Clear(); PatientAccounts.Dispose(); PatientAccounts = null; }

                if (objgloAccount != null) { objgloAccount.Dispose(); objgloAccount = null; }
                if (oAddresscontrol != null)
                {
                    if (pnlAddresssControl.Controls.Count > 0)
                    {
                        Control[] myControl = pnlAddresssControl.Controls.Find("DemographicsAddressControl", true);
                        if (myControl.Length > 0)
                        {
                            ((gloAddress.gloAddressControl)myControl[0]).ControlClosing = true;
                            pnlAddresssControl.Controls.Remove(myControl[0]);
                        }
                    }
                    try
                    {
                        oAddresscontrol.txtZip.TextChanged -= new EventHandler(txtZip_TextChanged1);
                    }
                    catch
                    {
                    }
                    oAddresscontrol.Dispose();
                    oAddresscontrol = null;
                }
                if (_PatientGuardian != null)
                {
                    _PatientGuardian.Dispose();
                    _PatientGuardian = null;
                }
                if (_PatientOccupation != null)
                {
                    _PatientOccupation.Dispose();
                    _PatientOccupation = null;
                }
                if (_PatientInsurance != null)
                {
                    _PatientInsurance.Dispose();
                    _PatientInsurance = null;
                }

                //Other Details 
                if (_PatientDemographicOtherInfo != null)
                {

                    _PatientDemographicOtherInfo.Dispose();
                    _PatientDemographicOtherInfo = null;
                }
                if (_PatientWorkersComp != null)
                {
                    _PatientWorkersComp.Clear();
                    _PatientWorkersComp.Dispose();
                    _PatientWorkersComp = null;
                }
                //
                if (oPatientPharmacies != null)
                {
                    oPatientPharmacies.Clear();
                    oPatientPharmacies.Dispose();
                    oPatientPharmacies = null;
                }
                if (oPatientReferrals != null)
                {
                    oPatientReferrals.Clear();
                    oPatientReferrals.Dispose();
                    oPatientReferrals = null;
                }
                if (oPatientCareTeam != null)
                {
                    oPatientCareTeam.Clear();
                    oPatientCareTeam.Dispose();
                    oPatientCareTeam = null;
                }
                if (oPrimaryCarePhysicians != null)
                {
                    oPrimaryCarePhysicians.Clear();
                    oPrimaryCarePhysicians.Dispose();
                    oPrimaryCarePhysicians = null;
                }
                if (oPatientGuarantors != null)
                {
                    oPatientGuarantors.Clear();
                    oPatientGuarantors.Dispose();
                    oPatientGuarantors = null;

                }
                if (oAccount != null)
                {
                    oAccount.Dispose();
                    oAccount = null;
                }
                if (oPatientAccount != null)
                {
                    oPatientAccount.Dispose();
                    oPatientAccount = null;
                }
                if (oPatientAccounts != null)
                {
                    oPatientAccounts.Clear();
                    oPatientAccounts.Dispose();
                    oPatientAccounts = null;
                }
                if (objgloAccount != null)
                {
                    objgloAccount.Dispose();
                    objgloAccount = null;
                }
                if (oPatientOtherGuarantors != null)
                {
                    oPatientOtherGuarantors.Clear();
                    oPatientOtherGuarantors.Dispose();
                    oPatientOtherGuarantors = null;
                }
                if (oPatientRepresentatives != null)
                {
                    oPatientRepresentatives.Clear();
                    oPatientRepresentatives.Dispose();
                    oPatientRepresentatives = null;
                }
                if (oAPIRepresentatives != null)
                {
                    oAPIRepresentatives.Clear();
                    oAPIRepresentatives.Dispose();
                    oAPIRepresentatives = null;
                }

                if (oPatientPharmacies != null) { oPatientPharmacies.Dispose(); oPatientPharmacies = null; }
                if (oPatientReferrals != null) { oPatientReferrals.Dispose(); oPatientReferrals = null; }
                if (oPatientCareTeam != null) { oPatientCareTeam.Dispose(); oPatientCareTeam = null; }
                if (oPatientOtherGuarantors != null) { oPatientOtherGuarantors.Dispose(); oPatientOtherGuarantors = null; }
                if (oPatientGuarantors != null) { oPatientGuarantors.Dispose(); oPatientGuarantors = null; }
                if (oPatientAccounts != null) { oPatientAccounts.Dispose(); oPatientAccounts = null; }
                if (oPrimaryCarePhysicians != null) { oPrimaryCarePhysicians.Dispose(); oPrimaryCarePhysicians = null; }

                if (oPatientAccount != null) { oPatientAccount.Dispose(); oPatientAccount = null; }

                if (oAccount != null) { oAccount.Dispose(); oAccount = null; }

                if (ogloPatientRepresentativeControl != null) { ogloPatientRepresentativeControl.DisposeAllControls(); }




            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void btn_Race_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Race, true, this.Width);

                oListControl.ControlHeader = "Race";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_RaceSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                oListControl.Dock = DockStyle.Fill;
                this.Controls.Add(oListControl);

                for (int i = 0; i < cmbPARace.Items.Count; i++)
                {
                    cmbPARace.SelectedIndex = i;
                    if (cmbPARace.Text == "" || cmbPARace.Text == "Declined to specify" || cmbPARace.Text == "Unknown")
                    { }
                    else
                    {
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbPARace.SelectedValue), cmbPARace.Text);
                    }
                }
                if (cmbPARace.Items.Count > 0)
                    cmbPARace.SelectedIndex = 0;

                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void btn_RaceDel_Click(object sender, EventArgs e)
        {
            fillRace();
        }

        private void oListControl_RaceSelectedClick(object sender, EventArgs e)
        {

            string CategoryID;
            CategoryID = "";

            try
            {
                if (oListControl.SelectedItems.Count > 0)
                {
                   // cmbPARace.Items.Clear();
                    cmbPARace.DataSource = null;
                    cmbPARace.Items.Clear();
                    DataTable dtRace = new DataTable();
                    DataColumn dcId = new DataColumn("nCategoryID");
                    DataColumn dcDescription = new DataColumn("sDescription");
                    dtRace.Columns.Add(dcId);
                    dtRace.Columns.Add(dcDescription);

                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtRace.NewRow();
                        drTemp["nCategoryID"] = oListControl.SelectedItems[i].ID;
                        drTemp["sDescription"] = oListControl.SelectedItems[i].Description;
                        dtRace.Rows.Add(drTemp);

                        if (CategoryID == "")
                        {
                            CategoryID = Convert.ToString(oListControl.SelectedItems[i].ID);
                        }
                        else
                        {
                            CategoryID = CategoryID + "," + Convert.ToString(oListControl.SelectedItems[i].ID);
                        }
                    }

                    cmbPARace.DataSource = dtRace;
                    cmbPARace.ValueMember = dtRace.Columns["nCategoryID"].ColumnName;
                    cmbPARace.DisplayMember = dtRace.Columns["sDescription"].ColumnName;
                    //cmbPARace.DrawMode = DrawMode.Normal;
                }
                else
                {
                    fillRace();
                }

                checkPatientRaceFavorites(CategoryID);

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                onDemographicControl_Enter(sender, e);
            }
            cmbPARace.Focus();
        }

        private void checkPatientRaceFavorites(string CategoryID)
        {
            DataTable dtIM = null;
            string srace;
            srace = "";

            string raceID;
            raceID = "";

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParam = default(gloDatabaseLayer.DBParameters);

            try
            {
                oDB.Connect(false);
                oParam = new gloDatabaseLayer.DBParameters();

                oParam.Add("@nCategoryID", CategoryID, ParameterDirection.Input, SqlDbType.Text);
                oDB.Retrive("checkPatientRaceFavorites", oParam, out dtIM);
                oDB.Disconnect();

                if (dtIM != null)
                {
                    if (dtIM.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIM.Rows.Count ; i++)
                        {
                            if (srace == "")
                            {
                                srace = "'" + Convert.ToString(dtIM.Rows[i]["sDescription"].ToString()) + "'";
                                raceID = Convert.ToString(dtIM.Rows[i]["nCategoryID"].ToString());
                            }
                            else
                            {
                                srace = srace + "," + System.Environment.NewLine + "'"  + Convert.ToString(dtIM.Rows[i]["sDescription"].ToString()) + "'" ;
                                raceID = raceID + "," + Convert.ToString(dtIM.Rows[i]["nCategoryID"].ToString());
                            }
                        }

                        if (srace != "")
                        {
                            DialogResult drresult;

                            if (dtIM.Rows.Count == 1)
                            { drresult = MessageBox.Show(srace.ToString() + System.Environment.NewLine + "is not added in favorite list, do you want to add?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            else
                            { drresult = MessageBox.Show(srace.ToString() + System.Environment.NewLine + "are not added in favorite list, do you want to add?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            if (drresult.ToString() == "Yes")
                            {
                                if (raceID != "")
                                {
                                    UpdatePatientRaceFavorites(raceID);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                srace = null;
                raceID= null;

                if (oDB != null)
                { oDB.Dispose();
                  oDB = null;
                }

                if (oParam != null)
                { oParam.Dispose();
                  oParam = null;
                }

                if (dtIM != null)
                { dtIM.Dispose();
                  dtIM = null;
                }
            }
        }

        private void UpdatePatientRaceFavorites(string sRace)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParam = default(gloDatabaseLayer.DBParameters);
            try
            {
                oDB.Connect(false);
                oParam = new gloDatabaseLayer.DBParameters();

                oParam.Add("@nCategoryID", sRace, ParameterDirection.Input, SqlDbType.Text);

                oDB.Execute("UpdatePatientRaceFavorites", oParam);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oParam != null)
                {
                    oParam.Dispose();
                    oParam = null;
                }
            }
        }
        
        private DataTable GetRaceLoad(string sRace)
        {
            DataTable dtIM = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParam = default(gloDatabaseLayer.DBParameters);

            try
            {
                oDB.Connect(false);
                oParam = new gloDatabaseLayer.DBParameters();

                oParam.Add("@sRace", sRace, ParameterDirection.Input, SqlDbType.Text);

                oDB.Retrive("GetRaceLoad", oParam, out dtIM);
                oDB.Disconnect();
                return dtIM;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose();
                  oDB = null;
                }

                if (oParam != null)
                { oParam.Dispose();
                  oParam = null;
                }

                //if (dtIM != null)
                //{ dtIM.Dispose();
                //  dtIM = null;
                //}
            }
        }

        private DataTable GetEthnicityLoad(string sEthnicity)
        {
            DataTable dtEthnicity = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParam = default(gloDatabaseLayer.DBParameters);

            try
            {
                oDB.Connect(false);
                oParam = new gloDatabaseLayer.DBParameters();

                oParam.Add("@sEthnicity", sEthnicity, ParameterDirection.Input, SqlDbType.Text);

                oDB.Retrive("GetEthnicityLoad", oParam, out dtEthnicity);
                oDB.Disconnect();
                return dtEthnicity;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oParam != null)
                {
                    oParam.Dispose();
                    oParam = null;
                }

                //if (dtIM != null)
                //{ dtIM.Dispose();
                //  dtIM = null;
                //}
            }
        }

        private Boolean ValidateLanguage()
        {
                string SelectedLang = cmbPALang.Text.Trim().ToString();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                DataTable dtLang = null;
                try
                {
                    if (cmbPALang.Text != "Declined to specify" && cmbPALang.Text != "")
                    {
                        string _sqlQuery = "sELECT  nCategoryid id,sDescription name,bFavorites as Favorite FROM category_mst WHERE UPPER(sCategoryType) ='LANGUAGE' AND sDescription = '" + SelectedLang + "'    AND nClinicID = " + _ClinicID + " order by sDescription ";//AND bIsBlocked = '" + false + "'
                        oDB.Retrive_Query(_sqlQuery, out dtLang);
                        oDB.Disconnect();
                        if (dtLang != null && dtLang.Rows.Count > 0)
                        {
                            string fav = dtLang.Rows[0]["Favorite"].ToString();
                            long cId = Convert.ToInt64(dtLang.Rows[0]["id"]);
                            bool isFavorite = false;
                            if (fav != "")
                                isFavorite = Convert.ToBoolean(dtLang.Rows[0]["Favorite"].ToString());

                            if (!isFavorite)
                            {
                                DialogResult drresult = MessageBox.Show("Selected language is not added in favorite language list, do you want to add?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (drresult.ToString() == "Yes")
                                {
                                    //Update selected Language to Category Master
                                    oDB.Connect(false);
                                    _sqlQuery = "update Category_MST set  bFavorites='" + true + "',bisBlocked ='" + false + "' WHERE nCategoryid =" + cId + " ";//UPPER(sCategoryType) ='LANGUAGE' AND sDescription like '%" + SelectedLang + "%'    AND nClinicID = " + _ClinicID + " order by sDescription ";//AND bIsBlocked = '" + false + "'
                                    oDB.Execute_Query(_sqlQuery);
                                    oDB.Disconnect();
                                    fillLang();
                                }
                                cmbPALang.Text = SelectedLang;
                                return true;
                            }
                            else
                                return true;
                        }
                        else
                        {
                            DialogResult drresult = MessageBox.Show("Entered preferred language is not available in the standard language list, do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (drresult.ToString() == "Yes")
                            {
                                ////Save Language to Category Master
                                //oDB.Connect(false);
                                //_sqlQuery = " declare @ID  numeric(18,0)  " +
                                //           "EXEC gSP_GetUniqueID @ID = @ID OUTPUT " +
                                //           "insert into Category_MST(nCategoryid,sDescription,sCategoryType,bFavorites,bisBlocked,nClinicId)" +
                                //           "Values ( @ID,'" + SelectedLang + "','Language','" + true + "','" + false + "','" + _ClinicID + "')";
                                //oDB.Execute_Query(_sqlQuery);
                                //oDB.Disconnect();
                                //fillLang();
                                cmbPALang.Text = SelectedLang;
                                return true;
                            }
                            else
                            {
                                cmbPALang.Text = "";
                                cmbPALang.Focus();
                                return false;
                            }
                        }
                    }
                    else
                        return true;
                }
                catch //(Exception ex)
                {
                    cmbPALang.Text = "";
                    cmbPALang.Focus();
                    return false;
                }
                finally
                {
                    oDB.Dispose();
                    oDB = null;
                }
            
        }

        private void cmbPALang_Validating(object sender, CancelEventArgs e)
        {
            //03-Jun-13 Aniket: Do not allow user to add languages on the Patient Screen
            //if (!ValidateLanguage())
            //{
            //    cmbPALang.Focus();
            //    e.Cancel = true;
            //}
            //else
            //{
            //    rbBrowsePhoto.Focus();
            //}
       //     isLanguageValidated = true;
        }

        private void cmbPALang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cmbPALang.Text;
            if (selectedValue == "More...")
            {
                fillLang_All();
                cmbPALang.Text = "";
            }
            else if (selectedValue == "Show Favorites...")
            {
                fillLang();
                cmbPALang.Text = "";
            }

            if (cmbPALang.SelectedItem != null)
            {

                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPALang.Items[cmbPALang.SelectedIndex])["name"]), cmbPALang) >= cmbPALang.DropDownWidth - 20)
                {
                    toolTip1.SetToolTip(cmbPALang, Convert.ToString(((DataRowView)cmbPALang.Items[cmbPALang.SelectedIndex])["name"]));

                }
                else
                {
                    toolTip1.SetToolTip(cmbPALang, "");
                    this.toolTip1.Hide(cmbPALang);
                }
            }
            else
            {
                this.toolTip1.Hide(cmbPALang);
            }

        }

        private void cmbPALang_TextChanged(object sender, EventArgs e)
        {
                if (getWidthofListItems(cmbPALang.Text, cmbPALang) >= cmbPALang.DropDownWidth - 20)
                {
                    toolTip1.SetToolTip(cmbPALang, cmbPALang.Text);
                }
                else
                {
                    toolTip1.SetToolTip(cmbPALang, "");
                    this.toolTip1.Hide(cmbPALang);
                }
           // Bug No.49416::Language - Application does not display message box if user add new language.::20130418
        //     isLanguageValidated = false;
        }

        private void btn_PACareTeamBr_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, true, this.Width);
                oListControl.ControlHeader = "Care Team";

             
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_CareTeamSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                oListControl.Dock = DockStyle.Fill;
                this.Controls.Add(oListControl);

                //
                for (int i = 0; i < cmbPACareTeam.Items.Count; i++)
                {
                    cmbPACareTeam.SelectedIndex = i;
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbPACareTeam.SelectedValue), cmbPACareTeam.Text);
                 
                }
                if (cmbPACareTeam.Items.Count > 0)
                    cmbPACareTeam.SelectedIndex = 0;
               
                oListControl.OpenControl();

               
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void btn_PACareTeamDel_Click(object sender, EventArgs e)
        {
            try
            {
              
                DataTable dtReff = null;
                dtReff = (DataTable)cmbPACareTeam.DataSource;

                if (cmbPACareTeam.Items.Count > 0 && cmbPACareTeam.SelectedIndex != -1)
                {
                    dtReff.Rows.RemoveAt(cmbPACareTeam.SelectedIndex);
                    dtReff.AcceptChanges();
                }
                cmbPACareTeam.DataSource = dtReff;
                cmbPACareTeam.ValueMember = dtReff.Columns[0].ColumnName;
                cmbPACareTeam.DisplayMember = dtReff.Columns[1].ColumnName;
                cmbPACareTeam.SelectedIndex = 0;
                cmbPACareTeam.DrawMode = DrawMode.Normal;
                

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            _isCareTeamModified = true;
        }


        //Select Care Team
        private void oListControl_CareTeamSelectedClick(object sender, EventArgs e)
        {
            try
            {
              //  cmbPACareTeam.Items.Clear();
                cmbPACareTeam.DataSource = null;
                cmbPACareTeam.Items.Clear();
                DataTable dtReff = new DataTable();
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");
                dtReff.Columns.Add(dcId);
                dtReff.Columns.Add(dcDescription);
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtReff.NewRow();
                        drTemp["ID"] = oListControl.SelectedItems[i].ID;
                        drTemp["Description"] = oListControl.SelectedItems[i].Description;
                        dtReff.Rows.Add(drTemp);
                    }
                }
                cmbPACareTeam.DataSource = dtReff;
                cmbPACareTeam.ValueMember = dtReff.Columns["ID"].ColumnName;
                cmbPACareTeam.DisplayMember = dtReff.Columns["Description"].ColumnName;
                cmbPACareTeam.DrawMode = DrawMode.Normal;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                onDemographicControl_Enter(sender, e);
            }
            _isCareTeamModified = true;           
            cmbPACareTeam.Focus();
        }

        //private void btnPatientRepresentativeBrowse_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (oListControl != null)
        //        {
        //            for (int i = this.Controls.Count - 1; i >= 0; i--)
        //            {
        //                if (this.Controls[i].Name == oListControl.Name)
        //                {
        //                    this.Controls.Remove(this.Controls[i]);
        //                    break;
        //                }
        //            }
        //        }

        //        oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PatientRepresentative, true, true, this.Width);
        //        oListControl.ControlHeader = "Patient Representative";

        //        oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_PatientRepresentativeSelectedClick);
        //        oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

        //        this.Controls.Add(oListControl);

        //        //To allow the user to add multiple guarantors at one time 

        //        DataTable dtPatientRepresentative = (DataTable)cmbPatientRepresentative.DataSource;
        //        if (dtPatientRepresentative != null && dtPatientRepresentative.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dtPatientRepresentative.Rows.Count; i++)
        //            {
        //                if (Convert.ToInt64(dtPatientRepresentative.Rows[i]["Id"]) > 0)
        //                {
        //                    oListControl.SelectedItems.Add(Convert.ToInt64(dtPatientRepresentative.Rows[i]["Id"]), Convert.ToString(dtPatientRepresentative.Rows[i]["Description"]));
        //                }
        //            }
        //        }


        //        oListControl.OpenControl();

        //        //oListControl is disposed in OpenControl() Method if Zero records found
        //        if (oListControl.IsDisposed == false)
        //        {
        //            oListControl.Dock = DockStyle.Fill;
        //            oListControl.BringToFront();
        //            onDemographicControl_Leave(sender, e);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }

        //}
        //private void oListControl_PatientRepresentativeSelectedClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dtPatientRepresentative = new DataTable();
        //        DataColumn dcId = new DataColumn("Id");
        //        DataColumn dcDescription = new DataColumn("Description");
        //        dtPatientRepresentative.Columns.Add(dcId);
        //        dtPatientRepresentative.Columns.Add(dcDescription);

        //        Int64 _TempPRID = 0;
        //        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);

        //        if (oListControl.SelectedItems.Count > 0)
        //        {
        //            for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
        //            {
        //                _TempPRID = Convert.ToInt64(oListControl.SelectedItems[i].ID);

        //                bool _ShouldAdd = true;

        //                for (int _Count = 0; _Count < oPatientRepresentatives.Count; _Count++)
        //                {
        //                    if (Convert.ToInt64(oListControl.SelectedItems[i].ID) == oPatientRepresentatives[_Count].PRId)
        //                    {
        //                        _ShouldAdd = false;
        //                        break;
        //                    }
        //                }
        //                if (_ShouldAdd == true)
        //                {
        //                    PatientRepresentative oPatientRepresentative = ogloPatient.GetPatientRepresentativesById(_TempPRID);

        //                    if (oPatientRepresentative != null)
        //                    {
        //                        oPatientRepresentatives.Add(oPatientRepresentative);
        //                    }
        //                }

        //            }
        //        }
        //        for (int _Count = 0; _Count < oPatientRepresentatives.Count; _Count++)
        //        {
        //            //SLR: Logic to be changed on 2/4/2014
        //            bool _found = false;
        //            for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
        //            {
        //                if (Convert.ToInt64(oListControl.SelectedItems[i].ID) == oPatientRepresentatives[_Count].PRId)
        //                {
        //                    _found = true;
        //                    break;
        //                }
        //            }
        //            if (_found == false)
        //            {
        //                oPatientRepresentatives.RemoveAt(_Count);
        //                _Count -= 1;
        //            }
                   
        //        }
              

        //        for (int i = 0; i < oPatientRepresentatives.Count; i++)
        //        {
        //            DataRow drTemp = dtPatientRepresentative.NewRow();
        //            drTemp["Id"] = oPatientRepresentatives[i].PRId;
        //            drTemp["Description"] = oPatientRepresentatives[i].FirstName + " " + oPatientRepresentatives[i].LastName;
        //            dtPatientRepresentative.Rows.Add(drTemp);
        //        }
        //        cmbPatientRepresentative.DataSource = null;
        //        cmbPatientRepresentative.Items.Clear();
        //        cmbPatientRepresentative.DataSource = dtPatientRepresentative;
        //        cmbPatientRepresentative.ValueMember = dtPatientRepresentative.Columns["Id"].ColumnName;
        //        cmbPatientRepresentative.DisplayMember = dtPatientRepresentative.Columns["Description"].ColumnName;
        //        _isPatientRepresentativeModified = true;



        //        //cmbPatientRepresentative.DataSource = null;
        //        //cmbPatientRepresentative.Items.Clear();
        //        //DataTable dtPatientRepresentative = new DataTable();
        //        //DataColumn dcId = new DataColumn("Id");
        //        //DataColumn dcDescription = new DataColumn("Description");
        //        //dtPatientRepresentative.Columns.Add(dcId);
        //        //dtPatientRepresentative.Columns.Add(dcDescription);

        //        //if (oListControl.SelectedItems.Count > 0)
        //        //{
        //        //    for (int i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
        //        //    {
        //        //        DataRow drTemp = dtPatientRepresentative.NewRow();

        //        //        drTemp["ID"] = oListControl.SelectedItems[i].ID;
        //        //        drTemp["Description"] = oListControl.SelectedItems[i].Description;
        //        //        cmbPatientRepresentative.Tag = Convert.ToInt64(oListControl.SelectedItems[i].ID);
        //        //        dtPatientRepresentative.Rows.Add(drTemp);

        //        //        for (int k = 0 ;k<=oPatientRepresentatives.Count-1;k++)
        //        //        {

        //        //            if (oPatientRepresentatives[k].PRId == oListControl.SelectedItems[i].ID)
        //        //            {
        //        //                oPatientRepresentatives.RemoveAt(k);
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //        //cmbPatientRepresentative.DataSource = dtPatientRepresentative;
        //        //cmbPatientRepresentative.ValueMember = dtPatientRepresentative.Columns["ID"].ColumnName;
        //        //cmbPatientRepresentative.DisplayMember = dtPatientRepresentative.Columns["Description"].ColumnName;
        //        //if (Convert.ToInt64(cmbPatientRepresentative.Tag) == 0)
        //        //    cmbPatientRepresentative.SelectedValue = dtPatientRepresentative.Rows[0]["ID"].ToString();
        //        //else
        //        //    cmbPatientRepresentative.SelectedValue = cmbPatientRepresentative.Tag;
        //        //cmbPatientRepresentative.DrawMode = DrawMode.Normal;

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        onDemographicControl_Enter(sender, e);
        //    }
        //    //cmbPatientRepresentative.Focus();
        //}
        //private void btnPatientRepresentativeNew_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ogloPatientRepresentativeControl = new gloPatientRepresentativeControl(_databaseconnectionstring);
        //        ogloPatientRepresentativeControl.PatientRepresentatives = oPatientRepresentatives;
        //        ogloPatientRepresentativeControl.SaveButton_Click += new gloPatientRepresentativeControl.SaveButtonClick(ogloPatientRepresentativeControl_SaveButton_Click);
        //        ogloPatientRepresentativeControl.CloseButton_Click += new gloPatientRepresentativeControl.CloseButtonClick(ogloPatientRepresentativeControl_CloseButton_Click);
        //        this.Controls.Add(ogloPatientRepresentativeControl);
        //        ogloPatientRepresentativeControl.Dock = DockStyle.Fill;
        //        ogloPatientRepresentativeControl.BringToFront();
        //        onDemographicControl_Leave(sender, e);
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}
       

        //private void ogloPatientRepresentativeControl_SaveButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        oPatientRepresentatives = ogloPatientRepresentativeControl.PatientRepresentatives;
        //        if (oPatientRepresentatives != null && oPatientRepresentatives.Count > 0)
        //        {

        //            DataTable dtRepresentative = new DataTable();
        //            DataColumn dcId = new DataColumn("Id");
        //            DataColumn dcDescription = new DataColumn("Description");
        //            dtRepresentative.Columns.Add(dcId);
        //            dtRepresentative.Columns.Add(dcDescription);

        //            for (int i = 0; i < oPatientRepresentatives.Count; i++)
        //            {
        //                DataRow drTemp = dtRepresentative.NewRow();
        //                drTemp["Id"] = oPatientRepresentatives[i].PRId;
        //                drTemp["Description"] = oPatientRepresentatives[i].FirstName + " " + oPatientRepresentatives[i].LastName;
        //                dtRepresentative.Rows.Add(drTemp);
        //            }
        //            cmbPatientRepresentative.DataSource = null;
        //            cmbPatientRepresentative.Items.Clear();
        //            cmbPatientRepresentative.ValueMember = dtRepresentative.Columns["Id"].ColumnName;
        //            cmbPatientRepresentative.DisplayMember = dtRepresentative.Columns["Description"].ColumnName;
        //            cmbPatientRepresentative.DataSource = dtRepresentative;
        //        }
        //        else
        //        {
        //            cmbPatientRepresentative.DataSource = null;
        //            cmbPatientRepresentative.Items.Clear();
        //        }
        //        RemovePatientRepresentative();
        //        _isPatientRepresentativeModified = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        onDemographicControl_Enter(sender, e);
        //    }
        //}

        //private void RemovePatientRepresentative()
        //{
        //    try
        //    {
        //        this.Controls.Remove(ogloPatientRepresentativeControl);
        //        if (ogloPatientRepresentativeControl != null) { ogloPatientRepresentativeControl.Dispose(); }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void ogloPatientRepresentativeControl_CloseButton_Click(object sender, EventArgs e)
        //{
        //    RemovePatientRepresentative();
        //    onDemographicControl_Enter(sender, e);
        //}

        private void btnMUTransaction_Click(object sender, EventArgs e)
        {
           
           if (cmbPAReferrals.Items.Count > 0 && cmbPAReferrals.SelectedIndex != -1)
                {
                    DataTable dtReferals = null;
                    //DataTable dtReturn; 
                    dtReferals = (DataTable)(cmbPAReferrals.DataSource);

                    if (dtReferals.Rows.Count > 0)
                    {
                        frmMUReferals frmMuRef = new frmMUReferals();
                        frmMuRef.cmbRef = dtReferals;
                        frmMuRef.Focus();
                        frmMuRef.ShowDialog(this);
                        dtReturn = frmMuRef.cmbRef;

                      //  cmbPAReferrals.Items.Clear();
                        cmbPAReferrals.DataSource = null;
                        cmbPAReferrals.Items.Clear();

                        cmbPAReferrals.DataSource = dtReturn;
                        cmbPAReferrals.ValueMember = dtReturn.Columns["ID"].ColumnName;
                        cmbPAReferrals.DisplayMember = dtReturn.Columns["Description"].ColumnName;
                        cmbPAReferrals.DrawMode = DrawMode.Normal;

                        if (_PatientId != 0)
                        {
                            _isRefferalsModified = true;
                        }
                        frmMuRef.Dispose();
                        frmMuRef = null;
                    }
                }

           
            
        }

        private void txtPAMName_MouseHover(object sender, EventArgs e)
        {
            try
            {
                toolTip1.SetToolTip(txtPAMName, txtPAMName.Text);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Patient Portal
        public void SetPatientPortalEmailFacility(Boolean _IsSaveAsCopy)
        {
            //string PatientPortalINTUITFEATURE = "";
            //string PatientPortalEnabled = "";
            //DataTable dt = GetSetting("INTUIT FEATURE ENABLE SETTING");
            //if (dt.Rows.Count > 0)
            //{
            //    PatientPortalINTUITFEATURE = dt.Rows[0]["sSettingsValue"].ToString();
            //}
            //dt = GetSetting("PatientPortalEnabled");
            //if (dt.Rows.Count > 0)
            //{
            //    PatientPortalEnabled = dt.Rows[0]["sSettingsValue"].ToString();
            //}
            //if (PatientPortalINTUITFEATURE.ToLower() == "true" && PatientPortalEnabled.ToLower() == "true")
            //    gblnPatientPortalEnabled = true;
            //else
            //    gblnPatientPortalEnabled = false;
            lblPatientPortalAccountStatus.Text = "";
            if (gblnPatientPortalEnabled)
            {
                if (_IsSaveAsCopy || (_PatientId == 0))
                {
                    //btnPatientPortalAccountStatus.Visible = true;
                    //lblPatientPortalAccountStatus.Visible = true;
                    //lblPortalAccountStatus.Visible = true;
                    pnlPortalAccount.Visible = false;
                    pnlPortalInvitaitonEmail.Visible = true;
                    if (_IsSaveAsCopy)
                    {
                        ValidatePortalSendActivationEmail();
                       
                    }
                    //pnlAPIAccount.Visible = false;
                    //pnlAPIActivationEmail.Visible = true;
                    GetPatientAPIStatus();
                    ValidateAPISendActivationEmail();

                }
                else
                {
                    //btnPatientPortalAccountStatus.Visible = true;
                    //lblPatientPortalAccountStatus.Visible = true;
                    //lblPortalAccountStatus.Visible = true;
                    pnlPortalAccount.Visible = true;
                    pnlPortalInvitaitonEmail.Visible = false;
                    GetPatientPortalStatus();
                    //pnlAPIAccount.Visible = true;
                    //pnlAPIActivationEmail.Visible = false;
                    GetPatientAPIStatus();
                }

            }
            else
            {
                //btnPatientPortalAccountStatus.Visible = false;
                //lblPatientPortalAccountStatus.Visible = false;
                //lblPortalAccountStatus.Visible = false;
                if (_IsSaveAsCopy || (_PatientId == 0))
                {
                    GetPatientAPIStatus();
                    pnlAPIAccount.Visible = false;
                    pnlAPIActivationEmail.Visible = true;
                }
                else
                {
                    GetPatientAPIStatus();
                    pnlAPIAccount.Visible = true;
                    pnlAPIActivationEmail.Visible = false;
                }
                pnlPortalAccount.Visible = false;
                pnlPortalInvitaitonEmail.Visible = false;
            }

            if (_IsSaveAsCopy || (_PatientId == 0))
            {
                pnlAPIAccount.Visible = false;
                pnlAPIActivationEmail.Visible = true;
            }
            else
            {
                pnlAPIAccount.Visible = true;
                pnlAPIActivationEmail.Visible = false;
            }

        }

        private void ValidatePortalSendActivationEmail()
        {
            if (_bValidatePortalInvitationEmail==false)
            {
                return;
            }
            cbSendPatientPortalActivationEmail.Checked = false;
            //added IF for checked check box "send Patient Portal Invitation" if mininum required information in present for sending mail.
            if (txtPAFname.Text.Trim() != "" && txtPALName.Text.Trim() != "" && mtxtPADOB.MaskCompleted == true)
            {
                if (txtPAEmail.Text.Trim() != "" && CheckEmailAddress(txtPAEmail.Text.Trim()))
                {
                   if (oAddresscontrol != null)
                    if (oAddresscontrol.txtZip.Text.Trim() != "")
                    {
                        cbSendPatientPortalActivationEmail.Checked = true;
                    }
                }
            }
        }
        //API
        private void ValidateAPISendActivationEmail()
        {
            if (_bValidateAPIInvitationEmail == false)
            {
                return;
            }
            cbSendAPIInvitation.Checked = false;
            //added IF for checked check box "send Patient Portal Invitation" if mininum required information in present for sending mail.
            if (txtPAFname.Text.Trim() != "" && txtPALName.Text.Trim() != "" && mtxtPADOB.MaskCompleted == true)
            {
                if (txtPAEmail.Text.Trim() != "" && CheckEmailAddress(txtPAEmail.Text.Trim()))
                {
                    //if (oAddresscontrol != null)
                    //    if (oAddresscontrol.txtZip.Text.Trim() != "")
                    //    {
                            cbSendAPIInvitation.Checked = true;
                        //}
                }
            }
        }
        //API
        private void btnAPIActivation_Click(object sender, EventArgs e)
        {

            if (oAPIAccountStatus != APIAccountStatus.PatientActivated && oAPIAccountStatus != APIAccountStatus.PatientBlocked)
            {
                if (((txtPAEmail.Text.Trim() == "") || (CheckEmailAddress(txtPAEmail.Text) == false)))
                {
                    if (MessageBox.Show("If you want to activate patient for API, you must enter a valid Email address in Patient Demographics Screen.", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        txtPAEmail.Focus();
                        return;
                    }
                }

            }

            ofrmAPIAccessAccount = new frmAPIAccessAccount(APIRepresentatives, _PatientId, DatabaseConnectionString, _IsSaveAsCopy, gblnPatientPortalEnabled, txtPAEmail.Text.Trim(), _ClinicID, oAddresscontrol.txtZip.Text.Trim());
            ofrmAPIAccessAccount.PatientPortalAccount = APIAccount;
            ofrmAPIAccessAccount.ShowDialog(this);
            APIRepresentatives = ofrmAPIAccessAccount.PatientRepresentatives;
            APIAccount = ofrmAPIAccessAccount.PatientPortalAccount;
            ofrmAPIAccessAccount.Dispose();
            GetPatientAPIStatus();
        }

        //API
        //API

        private void btnPatientPortalAccountStatus_Click(object sender, EventArgs e)
        {
            //Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal"
            //Set rigth for showing Patient Portal account details.
            if (gblnPatientPortalEnabled)
            {
                gloUserRights.ClsgloUserRights objUserRights = new gloUserRights.ClsgloUserRights(DatabaseConnectionString);
                objUserRights.CheckForUserRights(_UserID);
                bool objrights = objUserRights.PatientPortal;
                objUserRights.Dispose();
                objUserRights = null;
                if (objrights == false)
                {
                    MessageBox.Show("This user does not have the rights to view patient portal account details. Please contact your system administrator for the same.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }    
            }
            if (oPatientPortalAccountStatus != PatientPortalAccountStatus.PatientActivated && oPatientPortalAccountStatus != PatientPortalAccountStatus.PatientBlocked)
            { 
                  if (((txtPAEmail.Text.Trim() == "") || (CheckEmailAddress(txtPAEmail.Text) == false)) && (oAddresscontrol.txtZip.Text.Trim() == ""))
                {
                    if (MessageBox.Show("If you want to Send Invitation to Patient, you must enter a valid Email address and a Zip Code in Patient Demographics Screen.", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        txtPAEmail.Focus();
                        return;
                    }
                }
                else if ((txtPAEmail.Text.Trim() == "") || (CheckEmailAddress(txtPAEmail.Text) == false))
                {
                    if (MessageBox.Show("If you want to Send Invitation to Patient, you must enter a valid Email address in Patient Demographics Screen.", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        txtPAEmail.Focus();
                        return;
                    }
                }
                else if (oAddresscontrol.txtZip.Text.Trim() == "")
                {
                    if (MessageBox.Show("If you want to Activate/Send Invitation to Patient, you must enter a Zip Code in Patient Demographics Screen.", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            ofrmPatientPortal = new frmPatientPortal(PatientRepresentatives, _PatientId, DatabaseConnectionString, _IsSaveAsCopy, gblnPatientPortalEnabled, txtPAEmail.Text.Trim(), _ClinicID, oAddresscontrol.txtZip.Text.Trim(), txtPALName.Text.Trim(), txtPAFname.Text.Trim(), mtxtPADOB.Text.Trim());
            ofrmPatientPortal.PatientPortalAccount = PatientPortalAccount;
            ofrmPatientPortal.ShowDialog(this);
            PatientRepresentatives = ofrmPatientPortal.PatientRepresentatives;
            PatientPortalAccount = ofrmPatientPortal.PatientPortalAccount;
            ofrmPatientPortal.Dispose();
            GetPatientPortalStatus();
        }

        private enum PatientPortalAccountStatus
        {
            PatientNotInvited = 0,
            PatientInvited = 1,
            PatientActivated = 2,
            PatientBlocked = 3
        }
        PatientPortalAccountStatus oPatientPortalAccountStatus = new PatientPortalAccountStatus();

        private void GetPatientPortalStatus()
        {
            lblPatientPortalAccountStatus.Text = "";
            if (_PatientId != 0)
            {
                DataTable dtPatientPortalStatus = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters odbParams = default(gloDatabaseLayer.DBParameters);
                try
                {
                    oDB.Connect(false);
                    odbParams = new gloDatabaseLayer.DBParameters();
                    odbParams.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("gsp_PatientPortalStatus", odbParams, out dtPatientPortalStatus);
                    oDB.Disconnect();

                    if (dtPatientPortalStatus != null)
                    {
                        if (dtPatientPortalStatus.Rows.Count > 0)
                        {
                            if (dtPatientPortalStatus.Rows[0]["PatientPortalStatus"] != null)
                            {
                                lblPatientPortalAccountStatus.Text = dtPatientPortalStatus.Rows[0]["PatientPortalStatus"].ToString();

                                if (lblPatientPortalAccountStatus.Text.Trim().ToLower() == "Not Invited".ToLower())
                                {
                                    oPatientPortalAccountStatus = PatientPortalAccountStatus.PatientNotInvited;
                                }
                                else if (lblPatientPortalAccountStatus.Text.Trim().ToLower() == "Invited".ToLower())
                                {
                                    oPatientPortalAccountStatus = PatientPortalAccountStatus.PatientInvited;
                                }
                                else if (lblPatientPortalAccountStatus.Text.Trim().ToLower() == "Activated".ToLower())
                                {
                                    oPatientPortalAccountStatus = PatientPortalAccountStatus.PatientActivated;
                                }
                                else if (lblPatientPortalAccountStatus.Text.Trim().ToLower() == "Blocked".ToLower())
                                {
                                    oPatientPortalAccountStatus = PatientPortalAccountStatus.PatientBlocked;
                                }
                            }
                        }
                        dtPatientPortalStatus.Dispose();
                        dtPatientPortalStatus = null;
                    }

                }
                finally
                {
                    oDB.Dispose();
                    oDB = null;
                    if (odbParams != null)
                    {
                        odbParams.Dispose();
                        odbParams = null;
                    }
                } 
            }
         
        }
        //API
        private enum APIAccountStatus
        {
            PatientNotActivated = 0,

            PatientActivated = 1,
            PatientBlocked = 3
        }
        APIAccountStatus oAPIAccountStatus = new APIAccountStatus();

        private void GetPatientAPIStatus()
        {
            lblAPIActivationStatus.Text = "";

            if (_PatientId != 0)
            {
                //SLR: Changes on 7/30/2014
                DataTable dtPatientPortalStatus = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters odbParams = default(gloDatabaseLayer.DBParameters);
                try
                {
                    oDB.Connect(false);
                    odbParams = new gloDatabaseLayer.DBParameters();
                    odbParams.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("gsp_APIStatus", odbParams, out dtPatientPortalStatus);
                    oDB.Disconnect();
                    //SLR: Changes on 7/30/2014
                    oDB.Dispose();
                    oDB = null;
                    if (odbParams != null)
                    {
                        odbParams.Dispose();
                        odbParams = null;
                    }
                    if (dtPatientPortalStatus != null)
                    {
                        if (dtPatientPortalStatus.Rows.Count > 0)
                        {
                            if (dtPatientPortalStatus.Rows[0]["PatientPortalStatus"] != null)
                            {
                                lblAPIActivationStatus.Text = dtPatientPortalStatus.Rows[0]["PatientPortalStatus"].ToString();
                            }

                        }
                        //SLR: Changes on 7/30/2014
                        dtPatientPortalStatus.Dispose();
                        dtPatientPortalStatus = null;
                    }
                }
                finally
                {
                }

                if (lblPortalAccountStatus.Text.Trim().ToLower() == "Not Activated".ToLower())
                {
                    oAPIAccountStatus = APIAccountStatus.PatientNotActivated;

                    // SetPatientInvitation(_IsSaveAsCopy);

                }

                else if (lblPortalAccountStatus.Text.Trim().ToLower() == "Activated".ToLower())
                {
                    oAPIAccountStatus = APIAccountStatus.PatientActivated;

                }
                else if (lblPortalAccountStatus.Text.Trim().ToLower() == "Blocked".ToLower())
                {
                    oAPIAccountStatus = APIAccountStatus.PatientBlocked;

                }
            }




        }
        //API
        //Patient Portal
        //Bug #70411: Enter zip code and Email ID save and close : Send Patient portal Invitation check box is net checked automatically and No Invitation mail is going
        //added code to checked check box to send patient portal invitation mail.
        private void txtPAEmail_TextChanged(object sender, EventArgs e)
        {
            ValidatePortalSendActivationEmail();
            ValidateAPISendActivationEmail();
        }

        //added to checked check box "send Patient Portal Invitation" if mininum required information in present for sending mail.
        private void txtPAFname_TextChanged(object sender, EventArgs e)
        {
            ValidatePortalSendActivationEmail();
            ValidateAPISendActivationEmail();
        }
        
        //added to checked check box "send Patient Portal Invitation" if mininum required information in present for sending mail.
        private void txtPALName_TextChanged(object sender, EventArgs e)
        {
            ValidatePortalSendActivationEmail();
            ValidateAPISendActivationEmail();
        }

        //added to checked check box "send Patient Portal Invitation" if mininum required information in present for sending mail.
        private void mtxtPADOB_TextChanged(object sender, EventArgs e)
        {
            ValidatePortalSendActivationEmail();
            ValidateAPISendActivationEmail();
        }

        private void btn_EthnicityDel_Click(object sender, EventArgs e)
        {
            fillEthnicities();
        }

        private void btn_Ethnicity_Click(object sender, EventArgs e)
        {
            try
            {
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Ethnicity, true, this.Width);

                oListControl.ControlHeader = "Ethnicity";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_EthnicitySelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                oListControl.Dock = DockStyle.Fill;
                this.Controls.Add(oListControl);

                for (int i = 0; i < cmbPAEthn.Items.Count; i++)
                {
                    cmbPAEthn.SelectedIndex = i;
                    if (cmbPAEthn.Text == "" || cmbPAEthn.Text == "Declined to specify" || cmbPAEthn.Text == "Unknown")
                    { }
                    else
                    {
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbPAEthn.SelectedValue), cmbPAEthn.Text);
                    }
                }
                if (cmbPAEthn.Items.Count > 0)
                    cmbPAEthn.SelectedIndex = 0;

                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                    onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void oListControl_EthnicitySelectedClick(object sender, EventArgs e)
        {

            string CategoryID;
            CategoryID = "";

            try
            {
                if (oListControl.SelectedItems.Count > 0)
                {
                    // cmbPARace.Items.Clear();
                    cmbPAEthn.DataSource = null;
                    cmbPAEthn.Items.Clear();
                    DataTable dtEthn = new DataTable();
                    DataColumn dcId = new DataColumn("nCategoryID");
                    DataColumn dcDescription = new DataColumn("sDescription");
                    dtEthn.Columns.Add(dcId);
                    dtEthn.Columns.Add(dcDescription);

                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtEthn.NewRow();
                        drTemp["nCategoryID"] = oListControl.SelectedItems[i].ID;
                        drTemp["sDescription"] = oListControl.SelectedItems[i].Description;
                        dtEthn.Rows.Add(drTemp);

                        if (CategoryID == "")
                        {
                            CategoryID = Convert.ToString(oListControl.SelectedItems[i].ID);
                        }
                        else
                        {
                            CategoryID = CategoryID + "," + Convert.ToString(oListControl.SelectedItems[i].ID);
                        }
                    }

                    cmbPAEthn.DataSource = dtEthn;
                    cmbPAEthn.ValueMember = dtEthn.Columns["nCategoryID"].ColumnName;
                    cmbPAEthn.DisplayMember = dtEthn.Columns["sDescription"].ColumnName;
                    //cmbPARace.DrawMode = DrawMode.Normal;
                }
                else
                {
                    fillEthnicities();
                }

                checkPatientRaceFavorites(CategoryID);

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                onDemographicControl_Enter(sender, e);
            }
            cmbPARace.Focus();
        }

        private void btnOrientation_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDemographicInfo.Visible = false;
                removeogloPatientOtherInfoCntrl();
                ogloPatientOtherInfoCntrl = new gloPatientOtherInfoControl(_databaseconnectionstring,"GenderIdentity");
                ogloPatientOtherInfoCntrl.PatientDemographicOtherInfo = this.PatientDemographicOtherInfo;
                ogloPatientOtherInfoCntrl.onOtherDetails_SaveClicked += new gloPatientOtherInfoControl.onOtherDetailsSaveClicked(ogloPatientOtherInfoCntrl_onOtherDetails_SaveClicked);
                ogloPatientOtherInfoCntrl.onOtherDetailsClose_Clicked += new gloPatientOtherInfoControl.onOtherDetailsCloseClicked(ogloPatientOtherInfoCntrl_onOtherDetailsClose_Clicked);
                this.Parent.Parent.Height = ogloPatientOtherInfoCntrl.Height;
                ogloPatientOtherInfoCntrl.Dock = DockStyle.Fill;
                this.Controls.Add(ogloPatientOtherInfoCntrl);
                //ogloPatientOtherInfoCntrl.BringToFront();
                onDemographicControl_Leave(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        frmMUHosp objfrmmuhosp = null;
        private void btnbrhosp_Click(object sender, EventArgs e)
        {
            objfrmmuhosp = null;
          
            objfrmmuhosp = new frmMUHosp(_databaseconnectionstring, _PatientId);
            objfrmmuhosp._SaveData = false;
            if (dtPathosp_data != null)
            {
                objfrmmuhosp.dtHosp  = dtPathosp_data;
            }
                objfrmmuhosp.ShowDialog(this);
            if (objfrmmuhosp._SaveData == true)
            {
                if (objfrmmuhosp.dtpathosp != null)
                    dtPathosp_data = objfrmmuhosp.dtpathosp;
               
            }
            
        }


      

       
        
        
        //Patient Portal

        //private DataTable GetSetting(string SettingName)
        //{
        //    SqlConnection objCon = new SqlConnection();
        //    SqlCommand objCmd = new SqlCommand();
        //    DataTable dtTable = new DataTable();
        //    try
        //    {
        //        objCon.ConnectionString = _databaseconnectionstring;
        //        objCmd.CommandType = CommandType.Text;
        //        objCmd.CommandText = "SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" + SettingName + "'  AND nClinicID = " + _ClinicID + "";
        //        objCmd.Connection = objCon;
        //        objCmd.Connection = objCon;
        //        objCon.Open();
        //        SqlDataAdapter objDA = new SqlDataAdapter(objCmd);
        //        objDA.Fill(dtTable);
        //        objCon.Close();

        //        objDA.Dispose();
        //        objDA = null;

        //        return dtTable;

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        if ((objCon != null))
        //        {
        //            objCon.Dispose();
        //            objCon = null;
        //        }

        //        if ((objCmd != null))
        //        {
        //            objCmd.Dispose();
        //            objCmd = null;
        //        }

        //        if ((dtTable == null))
        //        {
        //            dtTable = null;
        //        }
        //    }

        //}
    }



}
