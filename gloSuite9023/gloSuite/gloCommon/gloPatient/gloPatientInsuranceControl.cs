using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using gloContacts;
using gloAddress;
using Edidev.FrameworkEDI;
using gloAppointmentBook.Books;
using System.Text.RegularExpressions;
using gloPatient.Classes;
using gloCardScanning;

namespace gloPatient
{
    public partial class gloPatientInsuranceControl : UserControl
    {

        #region "Declarations"


        //private string _PatientName = "";
        private bool _IsSaveAsCopy = false;

        private string _PatientFName = "";
        private string _PatientMName = "";
        private string _PatientLName = "";
        private string _Employer = "";
        private string _PatientDOB = "";
        private string _PatientPhone = "";
        private string _PatientGender = "";
        private string _PatientAddressLine1 = "";
        private string _PatientAddressLine2 = "";
        private string _PatientCity = "";
        private string _PatientState = "";
        private string _PatientZip = "";
        ComboBox combo;
        private string _PatientCounty = "";
        private string _PatientCountry = "";
        //Int32 _LastBoundIndex = -1;
        private string _PatientSSN = "";
        private string _SubscriberRelationShip = "";
        //Anil on 20090311
        private Int64 _ProviderID = 0;
        private Int64 _PatientID = 0;
        //private Int64 _InsuranceID = 0;
        //private string _SubscriberGender = "";
        //
        private string _MessageBoxCaption = "gloPM";
        private string _databaseconnectionstring;
        private Int64 _ClinicID;
        //for collection of Insurnce objects.
        private Insurances _oInsurancesDetails = null;
        PatientInsuranceOther _oPatienInsuranceOther;
        // private bool _otherInfoFlag = false;
        // Make oListControl Object of Type gloListControl.gloListControl  
        // To Show tyhe InSurance in The Custom Control 
        private gloListControl.gloListControl oListControl;
      //  private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Insurance;
        private bool IsEDIObjectLoaded = false;
        //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _UserName = "";
        //
        gloAddressControl oAddresscontrol = null;
        private bool _IsInsurancesModified = false;
        //shubhangi
        Boolean _isValidated = true;   //VARIABLE TO CHECK WHETHER WE ENTERED THE COPAY AMOUNT CORRECT 
        Boolean _NodeChanged = false;
        Boolean _isModified = false;
    //    Boolean _NodeRemoved = false;
        Boolean _oCardscan = false;
       
        private string _Country = "";
        private bool _IsAddInsurancePlanMode = false;
        private bool _isInsuranceSubscriberMandatory = false;
        private bool _isWorkerCompenable = false;
        private bool _isAddNewInsurance = false; 
        //private bool _isChecked = false;
        //oFocusControl temp storage of focused control before treeview control
        Control oFocusControl = null;
        //Timer to set forcus on  control.
        Timer oTimer;

        private String sWhichGender = ""; //Dhruv 20100621 -> Comming from gloQuickPatientControl
        public int width;
        //Ocr
        // Hashtable HT = new Hashtable();
        private CardScanType _CardScanType = CardScanType.None;
        Image _Fcard = null;
        Image _Bcard = null;

        DataTable dtScannedImages = new DataTable();
     //   private string sCompnayName = "";
        private Int16 nSubPhoneX = 0;
        private Int16 nSubPhoneY = 0;
        private Int16 SubPhoneX = 0;
        private Int16 SubPhoneY = 0;

        private bool bIsCheckAutoEligibilityInsurance = false;
        private bool bIsCallFromSetupPatient = false;
        private bool bIsCapitalizeInsID = false;
        private bool _isCallFromAddInsurance = false;
        private bool _isCallFromAddInsuranceSelf = false;
        private bool _isSubscriberDataChanged = false;
        #endregion "Declarations"

        #region " Public Property "
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        //Property added to check whether any insurance related updates are done or not (ref #GLO2010-0007999)
        public bool IsInsuranceModified
        {
            get { return _IsInsurancesModified; }
            set { _IsInsurancesModified = value; }
        }

        #region"Dhruv 20100622 ->Public property-> Only Called from the frmSetupQuickPatientControl"
        public string WhichGender
        {
            get
            {
                return sWhichGender;
            }
            set
            {
                sWhichGender = value;
            }
        }
        #endregion "Dhruv 20100622 -> Only Called from the frmSetupQuickPatientControl"

        public Insurances InsurancesDetails
        {
            get { return _oInsurancesDetails; }
            set { _oInsurancesDetails = value; }
        }

        public PatientInsuranceOther InsuranceOtherDetails
        {
            get { return _oPatienInsuranceOther; }
            set { _oPatienInsuranceOther = value; }
        }

        //defined new collection for deleted insurances and created property for it by pankaj on 20110113
        public List<Int64> _deletedInsurances = new List<Int64>();
        public List<Int64> DeletedInsurances
        {
            get { return _deletedInsurances; }
            set { _deletedInsurances = value; }
        }

        //public string PatientName
        //{
        //    get { return _PatientName; }
        //    set { _PatientName = value; }
        //}


        public bool IsSaveAsCopy
        {
            get { return _IsSaveAsCopy; }
            set { _IsSaveAsCopy = value; }
        }


        public string PatientFName
        {
            get { return _PatientFName; }
            set { _PatientFName = value; }
        }
        public string PatientMName
        {
            get { return _PatientMName; }
            set { _PatientMName = value; }
        }
        public string PatientLName
        {
            get { return _PatientLName; }
            set { _PatientLName = value; }
        }

        public string PatientDOB
        {
            get { return _PatientDOB; }
            set { _PatientDOB = value; }
        }
        public string PatientPhone
        {
            get { return _PatientPhone; }
            set { _PatientPhone = value; }
        }
        public string Employer
        {
            get { return _Employer; }
            set { _Employer = value; }
        }
        public string PatientGender
        {
            get { return _PatientGender; }
            set { _PatientGender = value; }
        }

        public string PatientAddressLine1
        {
            get { return _PatientAddressLine1; }
            set { _PatientAddressLine1 = value; }
        }
        public string PatientAddressLine2
        {
            get { return _PatientAddressLine2; }
            set { _PatientAddressLine2 = value; }
        }
        public string PatientCity
        {
            get { return _PatientCity; }
            set { _PatientCity = value; }
        }
        public string PatientState
        {
            get { return _PatientState; }
            set { _PatientState = value; }
        }
        public string PatientZip
        {
            get { return _PatientZip; }
            set { _PatientZip = value; }
        }
        public string PatientCounty
        {
            get { return _PatientCounty; }
            set { _PatientCounty = value; }
        }
        public string PatientCountry
        {
            get { return _PatientCountry; }
            set { _PatientCountry = value; }
        }

        //Anil on 20090311
        public Int64 ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }
        public string PatientSSN
        {
            get { return _PatientSSN; }
            set { _PatientSSN = value; }
        }

        public Boolean IsAddInsurancePlanMode
        {
            get { return _IsAddInsurancePlanMode; }
            set { _IsAddInsurancePlanMode = value; }
        }
        #endregion " Public Property "

        #region  "Constructor"

        public gloPatientInsuranceControl(string DatabaseConnectionString)
        {

            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            //for collection of Insurance collection
            _oInsurancesDetails = new Insurances();
            _oPatienInsuranceOther = new PatientInsuranceOther();

            //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

            if (_Country == "CA")
                _Country = "Canada";

            //
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
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

            #endregion

            if (appSettings["UserName"] != null)
            {
                _UserName = appSettings["UserName"].ToString();
            }
              SubPhoneX =Convert.ToInt16 (mskInsurancePhone.Location.X);
              SubPhoneY = Convert.ToInt16(mskInsurancePhone.Location.Y);
              nSubPhoneX = Convert.ToInt16(lblInsuPhone.Location.X);
              nSubPhoneY = Convert.ToInt16(lblInsuPhone.Location.Y);
              bIsCallFromSetupPatient = false;
              
        }
        public gloPatientInsuranceControl(string DatabaseConnectionString, Int64 ProviderID)
        {


            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            //for collection of Insurance collection
            _oInsurancesDetails = new Insurances();
            _oPatienInsuranceOther = new PatientInsuranceOther();
            cmbMedicareTypeCode.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMedicareTypeCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

            if (_Country == "CA")
                _Country = "Canada";

            //
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
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

            #endregion
            _ProviderID = ProviderID;

            if (appSettings["UserName"] != null)
            {
                _UserName = appSettings["UserName"].ToString();
            }
            SubPhoneX = Convert.ToInt16(mskInsurancePhone.Location.X);
            SubPhoneY = Convert.ToInt16(mskInsurancePhone.Location.Y);
            nSubPhoneX = Convert.ToInt16(lblInsuPhone.Location.X);
            nSubPhoneY = Convert.ToInt16(lblInsuPhone.Location.Y);
        }

        public gloPatientInsuranceControl(string DatabaseConnectionString, Int64 ProviderID, Int64 PatientID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            //for collection of Insurance collection
            _oInsurancesDetails = new Insurances();
            _oPatienInsuranceOther = new PatientInsuranceOther();
            cmbMedicareTypeCode.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMedicareTypeCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

            if (_Country == "CA")
                _Country = "Canada";

            //
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
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

            #endregion
            _ProviderID = ProviderID;
            _PatientID = PatientID;
            //_InsuranceID = InsuranceID;

            if (appSettings["UserName"] != null)
            {
                _UserName = appSettings["UserName"].ToString();
            }
            SubPhoneX = Convert.ToInt16(mskInsurancePhone.Location.X);
            SubPhoneY = Convert.ToInt16(mskInsurancePhone.Location.Y);
            nSubPhoneX = Convert.ToInt16(lblInsuPhone.Location.X);
            nSubPhoneY = Convert.ToInt16(lblInsuPhone.Location.Y);
            bIsCallFromSetupPatient = true;
        }

        #endregion "Constructor"

        #region "Delegates"

        public delegate void onInsuranceSaveClicked(object sender, EventArgs e);
        public event onInsuranceSaveClicked onInsuranceSave_Clicked;

        public delegate void onInsuranceCloseClicked(object sender, EventArgs e);
        public event onInsuranceCloseClicked onInsuranceClose_Clicked;


        #endregion "Delegates"

        #region "Form Load Event"

        private void gloPatientInsuranceControl_Load(object sender, EventArgs e)
        {
            //cmbMedicareTypeCode.DrawMode = DrawMode.OwnerDrawFixed;
            //cmbMedicareTypeCode.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            gloSettings.GeneralSettings objSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
            if (objSetting != null)
            {
                DataTable dtSettings = objSetting.GetSetting("InsuranceSubscriberMandatory");
                if (dtSettings != null && dtSettings.Rows.Count > 0)
                {
                    if (dtSettings.Rows[0][0] != null)
                    {
                        if (dtSettings.Rows[0][0].ToString() == "1")
                        {
                            _isInsuranceSubscriberMandatory = true;
                        }
                        else
                        {
                            _isInsuranceSubscriberMandatory = false;
                        }
                    }
                }
                dtSettings = objSetting.GetSetting("EnableWorkersCompBilling");
                if (dtSettings != null && dtSettings.Rows.Count > 0)
                {
                    if (dtSettings.Rows[0][0] != null)
                    {
                        if (dtSettings.Rows[0][0].ToString() == "True")
                        {
                            _isWorkerCompenable = true;
                        }
                        else
                        {
                            _isWorkerCompenable = false;
                        }
                    }
                }
                if (dtSettings != null)
                {
                    dtSettings.Dispose();
                    dtSettings = null;
                }
                if (objSetting != null)
                {
                    objSetting.Dispose();
                    objSetting = null;
                }
            }
            //Scan patient ID btn                    
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            if (oSettings != null)
            {
                string sVal = oSettings.ReadSettings_XML("CardScannerSettings", "ScanPatientPhoto");
                if (sVal == "1")
                {
                    tsb_ScanPatient.Visible = true;
                }
                else
                {
                    tsb_ScanPatient.Visible = false;
                }
            }

            if (oSettings != null)
            {
                oSettings.Dispose();
                oSettings = null;
            }


            //objSetting.Dispose();
            //dtSettings.Dispose();
            //oSettings.Dispose(); 

            //MaheshB 20091114
            //if (_PatientID == 0)
            //{
            chkAssignmentofBenifit.Checked = true;
            //}
            //else
            //{
            //}

            txtInsurance.Select();
            btnInsBrowse.Focus();
            //fillControl();
            fillDefaultTypeCode();
            fillMedicareCode();
            fillStates();
            tvInsurances.ImageList = imageList1;
            tvInsurances.Nodes.Add("Insurance");
            tvInsurances.Nodes[0].ImageIndex = 0;
            tvInsurances.SelectedImageIndex = 0;

            //Sandip Darade 20091021 integrating changes from 50RC4  to PM
            //addres control added 
            oAddresscontrol = new gloAddressControl(_databaseconnectionstring);
            oAddresscontrol.Dock = DockStyle.Fill;
            oAddresscontrol.Name = "InsuranceAddressControl";
            pnlAddresControl.Controls.Add(oAddresscontrol);
            fillControl();
            tvInsurances.ExpandAll();
            SetData();
            bIsCheckAutoEligibilityInsurance = CheckEligibilitySetting();
            if (_PatientID == 0)
            {
                if (!bIsCheckAutoEligibilityInsurance)
                {
                    tsbEligibilityCheck.Visible = false; 
                }
                else
                {
                    tsbEligibilityCheck.Visible = true;
                }

            }
            else
            {
                tsbEligibilityCheck.Visible = true;
            }

            #region "Dhruv 20100622 -> Only Called from the frmSetupQuickPatientControl"
            if (cmbRelationShip.Text.ToLower() == "Self".ToLower())
            {
                //COMMENTED BY SHUBHANGI 20100628 
                // if (WhichGender == "Male")
                //ADDED BY SHUBHANGI 20100628 TO RESOLVE ISSUE ON PATIENT INSURANCE INFORMATION
                if (PatientGender == "Male")
                {
                    rbGender1.Checked = true;
                }
                // else if (WhichGender == "Female")
                //ADDED BY SHUBHANGI 20100628 TO RESOLVE ISSUE ON PATIENT INSURANCE INFORMATION
                else if (PatientGender == "Female")
                {
                    rbGender2.Checked = true;
                }
                //else if (WhichGender == "Other")
                //ADDED BY SHUBHANGI 20100628 TO RESOLVE ISSUE ON PATIENT INSURANCE INFORMATION
                else if (PatientGender == "Other")
                {
                    rbGender3.Checked = true;
                }
                else
                {
                    rbGender3.Checked = true;
                }
            #endregion "Dhruv 20100622 -> Only Called from the frmSetupQuickPatientControl"
            }
            AssignUserRights();

            //if (dtScannedImages != null)
            //{
            dtScannedImages.Columns.Add("ID", typeof(Int64));
            dtScannedImages.Columns.Add("FrontImage", typeof(Image));
            dtScannedImages.Columns.Add("BackImage", typeof(Image));
            dtScannedImages.Columns.Add("bIsActive", typeof(Boolean));
            //}  
         //Bug #79460: gloPM>Modify Patient>It is throwing an exception."System.NullReFerenceException:"
            if ( tvInsurances.SelectedNode == null)
            {
                this.tvInsurances.BeforeSelect -= new System.Windows.Forms.TreeViewCancelEventHandler(this.tvInsurances_BeforeSelect);
                tvInsurances.SelectedNode = tvInsurances.Nodes[0];
                tvInsurances.ExpandAll();
                this.tvInsurances.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvInsurances_BeforeSelect);
            }
            if (bIsGroupMandatory(Convert.ToInt64(txtInsurance.Tag)) == true)
            {
                lblGroupMandatory.Visible = true;
            }
            else
            {
                lblGroupMandatory.Visible = false;
            }
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
            if (oSetting != null)
            {
                DataTable dtSettings = oSetting.GetSetting("bIsCapitalizeInsuranceID");
                if (dtSettings != null && dtSettings.Rows.Count > 0)
                {
                    if (dtSettings.Rows[0][0] != null)
                    {
                        bIsCapitalizeInsID = Convert.ToBoolean(dtSettings.Rows[0][0]);
                    }
                }
                if (dtSettings != null)
                {
                    dtSettings.Dispose();
                    dtSettings = null;
                }
                if (oSetting != null)
                {
                    oSetting.Dispose();
                    oSetting = null;
                }
            }
        }

        #endregion

        #region "Tool Strip Button Events"

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            ts_Commands.Select();
                            _isCallFromAddInsurance = false;
                            mskInsurancePhone.AllowValidate = true;
                            if (mskInsurancePhone.IsValidated == true)
                            {
                                if (txtInsurance.Text.Trim() != "")
                                {
                                    if (_isValidated == true)
                                    {
                                        if (AddInsurance() == false)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    { break; }
                                }
                                //used to validate the customise mask text 
                                // solving issue mantis id - 462
                                //Sanjog -uncomment code on 2011 Octo.31 to fix issue no . 12229
                                else
                                {
                                    if (txtSubscriberID.Text.Trim() != "" || cmbDefaultTypeCode.Text.Trim() != "")
                                    {
                                        MessageBox.Show("Select insurance plan.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtInsurance.Focus();
                                        // return false;
                                        break;
                                    }
                                }
                                //end

                                if (ValidateData() == true)
                                {


                                    GetData();
                                    onInsuranceSave_Clicked(null, null);
                                }

                                //Ocr Image Save 
                                //if (_Fcard != null)
                                //{

                                //}
                                //if (dtScannedImages.Rows.Count > 0)
                                //{
                                //    for (int j = 0; j <= tvInsurances.Nodes[0].Nodes.Count - 1; j++)
                                //    {
                                //        //TreeNode Tn = tvInsurances.Nodes[0];
                                //        Insurance oIns = new Insurance();
                                //        if (oIns != null)
                                //        {
                                //            oIns = (Insurance)tvInsurances.Nodes[0].Nodes[j].Tag;
                                //            for (int i = 0; i <= dtScannedImages.Rows.Count - 1; i++)
                                //            {
                                //                if (oIns.ContactID.ToString() == dtScannedImages.Rows[i]["ID"].ToString())
                                //                {
                                //                    Image FrntImage;
                                //                    if (Convert.ToString(dtScannedImages.Rows[i]["FrontImage"]) != String.Empty)
                                //                    {
                                //                        FrntImage = (Image)dtScannedImages.Rows[i]["FrontImage"];
                                //                    }
                                //                    else
                                //                    {
                                //                        FrntImage = null;
                                //                    }
                                //                    Image BackImage;
                                //                    if (Convert.ToString(dtScannedImages.Rows[i]["BackImage"]) != String.Empty)
                                //                    {
                                //                        BackImage = (Image)dtScannedImages.Rows[i]["BackImage"];
                                //                    }
                                //                    else
                                //                    {
                                //                        BackImage = null;
                                //                    }


                                //                    SaveScanData(_PatientID, FrntImage, BackImage, DateTime.Now, _CardScanType.GetHashCode(), "");

                                //                    //if (BackImage != null)
                                //                    //{
                                //                    //    BackImage.Dispose();
                                //                    //    BackImage = null;
                                //                    //}

                                //                    //if (FrntImage != null)
                                //                    //{
                                //                    //    FrntImage.Dispose();
                                //                    //    FrntImage = null;
                                //                    //}

                                //                    break;
                                //                }

                                //            }
                                //        }
                                //        if (oIns != null)
                                //        {
                                //            oIns.Dispose();
                                //            oIns = null;
                                //        }

                                //    }

                                //    if (dtScannedImages != null)//compress
                                //    {
                                //        dtScannedImages.Clear();
                                //        dtScannedImages.Dispose();
                                //        dtScannedImages = null;
                                //    }

                                //}
                                //---end
                                // Saving OCR Image 
                                SaveScanCards();
                               

                            }
                        }

                        break;
                    case "Cancel":
                        {
                            if (tvInsurances.SelectedNode != null && tvInsurances.SelectedNode.Level != 0)
                            {
                                if (_IsInsurancesModified == false)
                                {


                                    _IsInsurancesModified = IsModified((Insurance)tvInsurances.SelectedNode.Tag);



                                }
                            }

                            if (_IsInsurancesModified == false)
                            {
                                if (mskInsurancePhone.IsValidated == true)
                                {
                                    onInsuranceClose_Clicked(sender, e);
                                }
                            }
                            else
                            {
                                //if (_isValidated == false)
                                //{ break; }
                                //else 
                                //{
                                DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                if (res == DialogResult.Yes)
                                {

                                    if (mskInsurancePhone.IsValidated == true)
                                    {
                                        if (txtInsurance.Text.Trim() != "")
                                        {
                                            ts_Commands.Select();
                                            if (_isValidated == true)
                                            {
                                                if (AddInsurance() == false)
                                                {
                                                    break;
                                                }
                                            }
                                            else
                                            { break; }
                                        }
                                        //used to validate the customise mask text 

                                        // solving issue mantis id - 462
                                        else
                                        {
                                            MessageBox.Show("Select insurance plan.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            txtInsurance.Focus();
                                            // return false;
                                            break;
                                        }
                                        // end 

                                        if (ValidateData() == true)
                                        {


                                            GetData();
                                            onInsuranceSave_Clicked(null, null);
                                        }
                                    }
                                }
                                else if (res == DialogResult.No)
                                {
                                    //set the mask textbox to not to validate
                                    mskInsurancePhone.AllowValidate = false;
                                    onInsuranceClose_Clicked(sender, e);
                                }
                                // }
                            }
                        }
                        break;
                    case "Add":
                        {
                            _isSubscriberDataChanged = false;
                            _isCallFromAddInsurance = true;
                            _isCallFromAddInsuranceSelf = true;
                            _IsInsurancesModified = true;
                           // lblGroupMandatory.Visible = false;
                            if (AddInsurance() == false)
                            {
                                break;
                            }
                            tvInsurances.SelectedNode = tvInsurances.Nodes[0];
                            tvInsurances.ExpandAll();
                            _isCallFromAddInsuranceSelf = false;
                        }
                        break;
                    case "Remove":
                        {
                            _IsInsurancesModified = true;
                            RemoveInsurance();

                        }
                        break;
                    case "Eligibility":
                        {
                            tsbEligibilityCheck.Enabled = false;
                            if (tvInsurances.SelectedNode != null && tvInsurances.SelectedNode.Level != 0)
                            {
                                //CheckEligibility();
                                if (mtxtDOB.Focused == true)
                                {
                                    mskInsurancePhone.Focus();
                                    mtxtDOB.Focus();
                                }
                                else if (mtxtStartDate.Focused == true)
                                {
                                    txtCopay.Focus();
                                    mtxtStartDate.Focus();
                                }
                                else if (mtxtEndDate.Focused == true)
                                {
                                    txtCopay.Focus();
                                    mtxtEndDate.Focus();
                                }

                                if (_isValidated == true)
                                {

                                    EiligibilityCheck();
                                }
                                tsbEligibilityCheck.Enabled = true;
                            }
                            else
                            {

                                MessageBox.Show("No Insurance selected.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "ScanInsurance":
                        {
                            frmScanCard_New oCardScanning = new frmScanCard_New(_PatientID, _databaseconnectionstring, "Insurance");
                            if (oCardScanning != null)
                            {
                                //Bug #44826: 00000378 : Claims process
                                // If insurance already added then open scan insurance screen in modify state
                                if (tvInsurances.SelectedNode != null)
                                {
                                    if (tvInsurances.SelectedNode.Level != 0)
                                    {
                                        Insurance oIns = (Insurance)tvInsurances.SelectedNode.Tag;
                                        oCardScanning.IsInsModify = true;
                                        oCardScanning.PlanProvider = oIns.InsuranceName.ToString();
                                        oCardScanning.ContactID = oIns.ContactID.ToString();
                                       // if (oIns != null) { oIns.Dispose(); oIns = null; }
                                    }
                                }
                                oCardScanning.FName = txtSubFName.Text;
                                oCardScanning.LName = txtSubLName.Text;

                                oCardScanning.ShowDialog(this);
                                _oCardscan = false;
                                if (oCardScanning.oDialogResult)
                                {
                                    txtInsurance.Clear();
                                    txtInsurance.Text = oCardScanning.PlanProvider;
                                    txtInsurance.Tag = oCardScanning.ContactID;
                                    if (cmbRelationShip.Text == "Self")
                                    {
                                        if (txtSubFName.Text.Trim() + txtSubLName.Text.Trim() != "")
                                        {
                                            if (txtSubFName.Text.Trim().ToUpper() + " " + txtSubLName.Text.Trim().ToUpper() != oCardScanning.FName.Trim().ToUpper() + " " + oCardScanning.LName.Trim().ToUpper())
                                            {
                                                MessageBox.Show("Insurance Card Member Name '" + oCardScanning.FName.Trim() + " " + oCardScanning.LName.Trim() + "' does not match the selected Patient’s Name '" + txtSubFName.Text.Trim() + " " + txtSubLName.Text.Trim() + "'.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if ((oCardScanning.FName != "") || (oCardScanning.LName != ""))
                                        {
                                            txtSubFName.Text = string.Empty;
                                            txtSubMName.Text = string.Empty;
                                            txtSubLName.Text = string.Empty;
                                            txtSubFName.Text = oCardScanning.FName;
                                            txtSubMName.Text = oCardScanning.MName;
                                            txtSubLName.Text = oCardScanning.LName;
                                        }
                                        _oCardscan = true;
                                    }
                                    txtSubscriberID.Text = oCardScanning.MemberID;
                                    txtGroup.Text = oCardScanning.GroupNo;

                                    //Bug #44826: 00000378 : Claims process
                                    // Keep Insurance flag as it is in modify state.
                                    if (!oCardScanning.IsInsModify)
                                    {
                                        GetInsuranceFlag();
                                    }

                                    if (oCardScanning.cardFrontImage != null)
                                    {
                                        _Fcard = (Image)oCardScanning.cardFrontImage.Clone();
                                    }
                                    else
                                    {
                                        _Fcard = null;
                                    }
                                    if (oCardScanning.cardBackImage != null)
                                    {
                                        _Bcard = (Image)oCardScanning.cardBackImage.Clone();
                                    }
                                    else
                                    {
                                        _Bcard = null;
                                    }
                                }
                            }
                            if (oCardScanning != null)
                            {
                                oCardScanning.DisposeCardImages();
                                oCardScanning.Dispose();
                                oCardScanning = null;
                            }
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
                _isValidated = true;
                tsbEligibilityCheck.Enabled = true;
            }
        }

        #endregion


        public bool ValidateInsurance(long ContactID)
        {

            // Insurance oIns = new Insurance();

            for (int j = 0; j <= tvInsurances.Nodes[0].Nodes.Count - 1; j++)
            {


                Insurance oIns = (Insurance)tvInsurances.Nodes[0].Nodes[j].Tag;
                if (oIns.ContactID == ContactID)
                {

                    return true;
                }

            }

            return false;

        }




        private void SaveScanCards()
        {
            try
            {
                //09-Nov-15 Aniket: Resolving Bug #91148: gloEMR: Insurance change: Application gives exception on insurance status,type change
                if (dtScannedImages != null)
                { 
                foreach (DataRow row in dtScannedImages.Rows)
                {
                    Image FrntImage;
                    if ((row["FrontImage"]) != null)
                    { FrntImage = (Image)row["FrontImage"]; }
                    else
                    { FrntImage = null; }

                    Image BackImage;
                    if ((row["BackImage"]) != null)
                    { BackImage = (Image)row["BackImage"]; }
                    else
                    { BackImage = null; }

                    SaveScanData(_PatientID, FrntImage, BackImage, DateTime.Now, _CardScanType.GetHashCode(), "");

                    if (BackImage != null)
                    {
                        BackImage.Dispose();
                        BackImage = null;
                    }


                    if (FrntImage != null)
                    {
                        FrntImage.Dispose();
                        FrntImage = null;
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
                if (dtScannedImages != null)
                {
                    dtScannedImages.Clear();
                    dtScannedImages.Dispose();
                    dtScannedImages = null;
                }
            }
        }


        #region "Save Card Data"
        public bool SaveScanData(Int64 PatientID, System.Drawing.Image ScanImage, System.Drawing.Image BackImage, DateTime ScanDate, Int64 CardTypeId, string ScannedData)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            //SLR30: Check for null and then proceed.

            //@nPatientID ,@iCard,@dtScanDateTime ,@nCardTypeID ,@sScannedData
            //MessageBox.Show("Parameter Assigning", "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                if (oDB != null)
                {
                    oDB.Connect(false);
                    if (oParameters != null)
                    {
                        oParameters.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        //Addding Image 
                        if (ScanImage == null)
                        {
                            oParameters.Add("@iCard", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);

                        }
                        else
                        {
                            System.Drawing.Image ilogo = (Image)ScanImage.Clone();
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            if (ms != null)
                            {
                                ilogo.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                try
                                {
                                    ms.Flush();
                                }
                                catch
                                {
                                }
                                Byte[] arrImage = ms.ToArray();
                                oParameters.Add("@iCard", arrImage, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                            }

                            try
                            {
                                ms.Close();
                                //ms.Dispose();

                                if (ms != null)
                                {
                                    ms.Dispose();
                                    ms = null;
                                }
                            }
                            catch
                            {
                            }
                            if (ilogo != null)
                            {
                                ilogo.Dispose();
                                ilogo = null;
                            }
                        }

                        if (BackImage == null)
                        {
                            oParameters.Add("@iCardBack", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                        }
                        else
                        {
                            //Code added on 29th March 2008 by Sagar Ghodke.
                            System.Drawing.Image iBack = (Image)BackImage.Clone();
                            System.IO.MemoryStream msImageBack = new System.IO.MemoryStream();
                            if (msImageBack != null)
                            {
                                iBack.Save(msImageBack, System.Drawing.Imaging.ImageFormat.Jpeg);
                                try
                                {
                                    msImageBack.Flush();
                                }
                                catch
                                {
                                }
                                Byte[] arrImageBack = msImageBack.ToArray();
                                if (oParameters != null)
                                {
                                    oParameters.Add("@iCardBack", arrImageBack, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                                }
                            }
                            try
                            {
                                msImageBack.Close();
                                // msImageBack.Dispose();

                                if (msImageBack != null)
                                {
                                    msImageBack.Dispose();
                                    msImageBack = null;
                                }
                            }
                            catch
                            {
                            }
                            if (iBack != null)
                            {
                                iBack.Dispose();
                                iBack = null;
                            }
                        }


                        oParameters.Add("@dtScanDateTime", ScanDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                        oParameters.Add("@nCardTypeID", CardTypeId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oParameters.Add("@sScannedData", ScannedData, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    }
                    int _res = oDB.Execute("gsp_IN_PatientCards", oParameters);


                    //MessageBox.Show("Database Status" + _res.ToString(), "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
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
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }
        #endregion

        #region "Public Methods GetData & SetData"

        public void GetData()
        {
            mtxtStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            //oInsurance.StartDate = Convert.ToDateTime(mtxtStartDate.Text);
            //oInsurance.StartDate = Convert.ToDateTime(mtxtStartDate.Text.Trim()); 

            try
            {
                //Adding the collection of Insurance object to Insurances class;
                //TreeNode oNode;
                //oNode = null;
                //check if the tree contains nodes or not.
                //i.e - whether its having Insurance objects or not
                // if (tvInsurances.Nodes[0].Nodes.Count > 0)
                //{
                //Adding Insurance objects in the tag of tree view to Insuraces class.
                _oPatienInsuranceOther.InsurancesDetails.Clear();
                for (int i = 0; i < tvInsurances.Nodes[0].Nodes.Count; i++)
                {
                    _oPatienInsuranceOther.InsurancesDetails.Add((Insurance)tvInsurances.Nodes[0].Nodes[i].Tag);

                }
                //_otherInfoFlag = true;



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private bool ValidateData()
        {
            //if (mskInsurancePhone.AllowValidate  == false)
            //{
            //    MessageBox.Show("Phone details are incomplete.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //Incomplete Phone Numbers
            //mskInsuInfoSpousePhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mskInsuInfoSpousePhone.Text.Length > 0 && mskInsuInfoSpousePhone.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for phone.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mskInsuInfoSpousePhone.Focus();
            //    return false;
            //}


            //// Validation for the Checked Workers Comp Claim.
            //if (cbWorkersComp.Checked)
            //{
            //    if (txtWorkersCompClainNo.Text.Trim() == "")
            //    {
            //        MessageBox.Show("Please enter the Workers Comp claim number.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtWorkersCompClainNo.Focus();
            //        return false;
            //    }
            //}

            //// Validation for the Checked Auto Claim.
            //if (cbAutoClaim.Checked)
            //{
            //    if (txtAutoClaimNo.Text.Trim() == "")
            //    {
            //        MessageBox.Show("Please enter Auto claim number.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtAutoClaimNo.Focus();
            //        return false;
            //    }
            //}


            return true;

        }

        public bool SetData()
        {

            try
            {

                Insurance otempInsurance = new Insurance();
                if (otempInsurance != null)
                {
                    //Adding the Insurance Details to tree view
                    for (int i = 0; i < _oPatienInsuranceOther.InsurancesDetails.Count; i++)
                    {
                        TreeNode tempNode = new TreeNode();
                        if (tempNode != null)
                        {
                            otempInsurance = _oPatienInsuranceOther.InsurancesDetails[i];
                            //SHUBHANGI 20100708 SET THE INDEX FOR THE TREE VIEW AT THE TIME OF LOAD 
                            otempInsurance.Index = i;

                            tempNode.Text = otempInsurance.InsuranceName.ToString();

                            tempNode.Tag = otempInsurance;
                            //tempNode.ImageIndex = 1;
                            //tempNode.SelectedImageIndex = 1;

                            if (otempInsurance.PrimaryFlag)
                            {
                                tempNode.ForeColor = Color.IndianRed;
                            }

                            if (otempInsurance.PrimaryFlag)
                            {
                                tempNode.ForeColor = Color.IndianRed;
                            }

                            //Set Insurance Node Fore color Depending on the Flag
                            if (otempInsurance.InsuranceFlag == (Int64)(Insurance.InsuranceTypeFlag.Primary))
                            {
                                tempNode.ForeColor = Color.DarkRed;
                                tempNode.ImageIndex = 2;
                                tempNode.SelectedImageIndex = 2;

                            }
                            if (otempInsurance.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.Secondary)
                            {
                                tempNode.ForeColor = Color.OrangeRed;
                                tempNode.ImageIndex = 3;
                                tempNode.SelectedImageIndex = 3;

                            }
                            if (otempInsurance.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.Tertiary)
                            {
                                tempNode.ForeColor = Color.ForestGreen;
                                tempNode.ImageIndex = 4;
                                tempNode.SelectedImageIndex = 4;

                            }
                            if (otempInsurance.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.None)
                            {
                                //COMMENTED BY SHUBHANGI 20100720
                                // tempNode.ForeColor = Color.Black;
                                //ADDED BY SHUBHANGI 20100720
                                tempNode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));

                                tempNode.ImageIndex = 5;
                                tempNode.SelectedImageIndex = 5;

                            }
                            //**Set Insurance Node Fore color Depending on the Flag

                            tvInsurances.Nodes[0].Nodes.Add(tempNode);
                        }
                        if (tempNode != null)
                        {
                            tempNode = null;
                        }

                    }
                    tvInsurances.ExpandAll();


                    if (_IsAddInsurancePlanMode != true)
                    {
                        //select primary insurance node
                        if (tvInsurances.Nodes[0].Nodes.Count > 0)
                        {
                            tvInsurances.HideSelection = false;
                            tvInsurances.SelectedNode = tvInsurances.Nodes[0].Nodes[0];
                            TreeViewEventArgs eArg = new TreeViewEventArgs(tvInsurances.Nodes[0].Nodes[0]);
                            tvInsurances_AfterSelect(null, eArg);
                        }
                    }


                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }

        }

        public void EnableControls(Boolean isEnabled)
        {
            txtSubFName.Enabled = isEnabled;
            txtSubMName.Enabled = isEnabled;
            txtSubLName.Enabled = isEnabled;
            mtxtDOB.Enabled = isEnabled;
            mskInsurancePhone.Enabled = isEnabled;
            chkAddrSameAsPatient.Enabled = isEnabled;
            oAddresscontrol.txtAddress1.Enabled = isEnabled;
            oAddresscontrol.txtAddress2.Enabled = isEnabled;
            oAddresscontrol.txtCity.Enabled = isEnabled;
            oAddresscontrol.txtCounty.Enabled = isEnabled;
            oAddresscontrol.txtZip.Enabled = isEnabled;
            oAddresscontrol.cmbCountry.Enabled = isEnabled;
            oAddresscontrol.cmbState.Enabled = isEnabled;
            rbGender1.Enabled = isEnabled;
            rbGender2.Enabled = isEnabled;
            rbGender3.Enabled = isEnabled;
            txtEmployer.Enabled = isEnabled;

        }
        public bool clearControls()
        {
            txtInsurance.Clear();
            txtGroup.Clear();
            txtEmployer.Clear();
            txtSubFName.Clear();
            txtSubscriberID.Clear();
            txtSubscriberPolicy.Clear();
            mtxtDOB.Text = "";
            mskInsurancePhone.Clear();
            chkSameAsPatient.Checked = false;
            rbInactive.Checked = true;
            radSetAsPrimary.Checked = false;
            radSetAsSecondary.Checked = false;
            radSetAsTertiary.Checked = false;
            chkworkerscomp.Checked = false;
            chkautoclaim.Checked = false;

            return true;

        }
        private void fillMedicareCode()
        {
            DataTable dtMedicareType = new DataTable();

            if (dtMedicareType != null)
            {
                dtMedicareType.Columns.Add("sInsTypeCodeMedicare");
                dtMedicareType.Columns.Add("sInsTypeDescriptionMedicare");

                DataRow dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = " ";
                dr["sInsTypeDescriptionMedicare"] = " ";
                dtMedicareType.Rows.Add(dr);

                dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = "12";
                dr["sInsTypeDescriptionMedicare"] = "Medicare Secondary Working Aged Beneficiary or Spouse with Employer Group Health Plan";
                dtMedicareType.Rows.Add(dr);

                dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = "13";
                dr["sInsTypeDescriptionMedicare"] = "Medicare Secondary End-Stage Renal Disease Beneficiary in the 12 month coordination period with an employer’s group health plan";
                dtMedicareType.Rows.Add(dr);

                dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = "14";
                dr["sInsTypeDescriptionMedicare"] = "Medicare Secondary, No-fault Insurance including Auto is Primary";
                dtMedicareType.Rows.Add(dr);

                dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = "15";
                dr["sInsTypeDescriptionMedicare"] = "Medicare Secondary Worker’s Compensation";
                dtMedicareType.Rows.Add(dr);

                dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = "16";
                dr["sInsTypeDescriptionMedicare"] = "Medicare Secondary Public Health Service (PHS)or Other Federal Agency";
                dtMedicareType.Rows.Add(dr);

                dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = "41";
                dr["sInsTypeDescriptionMedicare"] = "Medicare Secondary Black Lung";
                dtMedicareType.Rows.Add(dr);

                dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = "42";
                dr["sInsTypeDescriptionMedicare"] = "Medicare Secondary Veteran’s Administration";
                dtMedicareType.Rows.Add(dr);

                dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = "43";
                dr["sInsTypeDescriptionMedicare"] = "Medicare Secondary Disabled Beneficiary Under Age 65 with Large Group Health Plan (LGHP)";
                dtMedicareType.Rows.Add(dr);

                dr = dtMedicareType.NewRow();
                dr["sInsTypeCodeMedicare"] = "47";
                dr["sInsTypeDescriptionMedicare"] = "Medicare Secondary, Other Liability Insurance is Primary";
                dtMedicareType.Rows.Add(dr);

                dtMedicareType.AcceptChanges();

                DataView _dv = dtMedicareType.DefaultView;
                _dv.Sort = "sInsTypeDescriptionMedicare";
                dtMedicareType = _dv.ToTable();

                cmbMedicareTypeCode.DataSource = dtMedicareType;
                cmbMedicareTypeCode.ValueMember = dtMedicareType.Columns["sInsTypeCodeMedicare"].ColumnName;
                cmbMedicareTypeCode.DisplayMember = dtMedicareType.Columns["sInsTypeDescriptionMedicare"].ColumnName;

            }
            //


        }
        private void fillDefaultTypeCode()
        {

            // To Fill The Relationships

            DataTable dtDefaultTypeCode = new DataTable();



            if (dtDefaultTypeCode != null)
            {
                dtDefaultTypeCode.Columns.Add("sCode");
                dtDefaultTypeCode.Columns.Add("sDefaultTypeDesc");

                DataRow dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = " ";
                dr["sDefaultTypeDesc"] = " ";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "AP";
                dr["sDefaultTypeDesc"] = "Auto Insurance Policy";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "C1";
                dr["sDefaultTypeDesc"] = "Commercial";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "CP";
                dr["sDefaultTypeDesc"] = "Medicare Conditionally Primary";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "GP";
                dr["sDefaultTypeDesc"] = "Group Policy";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "HM";
                dr["sDefaultTypeDesc"] = "Health Maintenance Organization (HMO)";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "IP";
                dr["sDefaultTypeDesc"] = "Individual Policy";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "LD";
                dr["sDefaultTypeDesc"] = "Long Term Policy";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "LT";
                dr["sDefaultTypeDesc"] = "Litigation";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "MB";
                dr["sDefaultTypeDesc"] = "Medicare Part B";
                dtDefaultTypeCode.Rows.Add(dr);

                //Added this on 20100330 By Sagar GHodke
                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "MA";
                dr["sDefaultTypeDesc"] = "Medicare Part A";
                dtDefaultTypeCode.Rows.Add(dr);
                //End add 20100330,Sagar Ghodke

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "MC";
                dr["sDefaultTypeDesc"] = "Medicaid";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "MI";
                dr["sDefaultTypeDesc"] = "Medigap Part B";
                dtDefaultTypeCode.Rows.Add(dr);


                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "OT";
                dr["sDefaultTypeDesc"] = "Other";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "PP";
                dr["sDefaultTypeDesc"] = "Personal Payment (Cash - No Insurance)";
                dtDefaultTypeCode.Rows.Add(dr);

                dr = dtDefaultTypeCode.NewRow();
                dr["sCode"] = "SP";
                dr["sDefaultTypeDesc"] = "Supplemental Policy";
                dtDefaultTypeCode.Rows.Add(dr);


                dtDefaultTypeCode.AcceptChanges();
                DataView _dv = dtDefaultTypeCode.DefaultView;
                _dv.Sort = "sDefaultTypeDesc";
                dtDefaultTypeCode = _dv.ToTable();

                cmbDefaultTypeCode.DataSource = dtDefaultTypeCode;
                cmbDefaultTypeCode.ValueMember = dtDefaultTypeCode.Columns["sCode"].ColumnName;
                cmbDefaultTypeCode.DisplayMember = dtDefaultTypeCode.Columns["sDefaultTypeDesc"].ColumnName;

            }

            //???


        }
        private void fillControl()
        {

            // To Fill The Relationships
            RelationShip oRelationShip = new RelationShip(_databaseconnectionstring);
            if (oRelationShip != null)
            {
                DataTable dtRelation = new DataTable();
                dtRelation = oRelationShip.GetList();
                if (dtRelation != null)
                {
                    if (_isInsuranceSubscriberMandatory == false)
                    {
                        DataRow dr = dtRelation.NewRow();
                        dr["nPatientRelID"] = "0";
                        dr["sRelationshipDesc"] = "";
                        dtRelation.Rows.InsertAt(dr, 0);
                        dtRelation.AcceptChanges();
                    }
                    cmbRelationShip.DataSource = dtRelation;
                    cmbRelationShip.ValueMember = dtRelation.Columns["nPatientRelID"].ColumnName;
                    cmbRelationShip.DisplayMember = dtRelation.Columns["sRelationshipDesc"].ColumnName;
                    _SubscriberRelationShip = cmbRelationShip.Text;
                    if (dtRelation.Rows.Count > 0)
                    {
                        cmbRelationShip.SelectedIndex = 0;
                    }

                }
            }
            if (oRelationShip != null)
            {
                oRelationShip.Dispose();
                oRelationShip = null;
            }
            //???


        }

        private void fillStates()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                if (oDB != null)
                {
                    oDB.Connect(false);
                    DataTable dtStates = new DataTable();
                    string _sqlQuery = "SELECT distinct ST FROM CSZ_MST order by ST";
                    oDB.Retrive_Query(_sqlQuery, out dtStates);
                    oDB.Disconnect();

                    if (dtStates != null)
                    {
                        DataRow dr = dtStates.NewRow();
                        dr["ST"] = "";
                        dtStates.Rows.InsertAt(dr, 0);
                        dtStates.AcceptChanges();

                        cmbState.DataSource = dtStates;
                        cmbState.DisplayMember = "ST";
                    }
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
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }
        //SHUBHANGI 
        #region "ValidateMultipleInsurance"
        private Boolean ValidateMultipleInsurance(Int64 selectednodeindex)
        {
            Boolean _result = true;
            DialogResult _dlgRst = DialogResult.None;
            Int32 _nInsuranceFlag = 0;
            mtxtDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            try
            {

                if (radSetAsPrimary.Checked) { _nInsuranceFlag = 1; }
                else if (radSetAsSecondary.Checked) { _nInsuranceFlag = 2; }
                else if (radSetAsTertiary.Checked) { _nInsuranceFlag = 3; }
                else { _nInsuranceFlag = 0; }

                for (int i = 0; i <= tvInsurances.Nodes[0].Nodes.Count - 1; i++)
                {
                    Insurance oIns = (Insurance)tvInsurances.Nodes[0].Nodes[i].Tag;

                    //CODE FOR ADD NEW NODE
                    if (selectednodeindex != i)
                    {
                        if (_isModified == false)
                        {
                            if (oIns.IsNotDOB == false)
                            {
                                //Same Insurance Check
                                //if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberName == txtSubFName.Text && oIns.SubscriberMName == txtSubMName.Text && oIns.SubscriberLName == txtSubLName.Text && oIns.SubscriberID == txtSubscriberID.Text && oIns.DOB.ToString("MM/dd/yyyy") == mtxtDOB.Text)
                                if (cmbRelationShip.Text.Trim().ToLower() != "Self".ToLower() && chkCompany.Checked == true)
                                {
                                    if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberCompanyLName == txtCompanyName.Text.Trim() && oIns.SubscriberID.ToUpper() == txtSubscriberID.Text.ToUpper() && oIns.Group == txtGroup.Text)
                                    {
                                        //Only one plan Active at a time
                                        if (_nInsuranceFlag > 0 && oIns.InsuranceFlag > 0)
                                        {
                                            _dlgRst = MessageBox.Show(this, "Duplicate Insurance already exists. The new insurance will be created and the previous insurance will be inactivated. Continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                                            if (_dlgRst == DialogResult.Yes)
                                            {

                                                _result = true;
                                                oIns.InsuranceFlag = 0;
                                                tvInsurances.Nodes[0].Nodes[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                                            }
                                            else if (_dlgRst == DialogResult.No)
                                            {
                                                _result = false;
                                            }
                                            else if (_dlgRst == DialogResult.Cancel)
                                            {
                                                _result = false;

                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberFName.ToUpper().Trim() == txtSubFName.Text.ToUpper().Trim() && oIns.SubscriberMName.ToUpper().Trim() == txtSubMName.Text.ToUpper().Trim() && oIns.SubscriberLName.ToUpper().Trim() == txtSubLName.Text.ToUpper().Trim() && oIns.SubscriberID.ToUpper() == txtSubscriberID.Text.ToUpper() && oIns.DOB.ToString("MMddyyyy") == mtxtDOB.Text && oIns.Group == txtGroup.Text)
                                    {
                                        //Only one plan Active at a time
                                        if (_nInsuranceFlag > 0 && oIns.InsuranceFlag > 0)
                                        {
                                            _dlgRst = MessageBox.Show(this, "Duplicate Insurance already exists. The new insurance will be created and the previous insurance will be inactivated. Continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                                            if (_dlgRst == DialogResult.Yes)
                                            {

                                                _result = true;
                                                oIns.InsuranceFlag = 0;
                                                tvInsurances.Nodes[0].Nodes[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                                            }
                                            else if (_dlgRst == DialogResult.No)
                                            {
                                                _result = false;
                                            }
                                            else if (_dlgRst == DialogResult.Cancel)
                                            {
                                                _result = false;

                                            }
                                            break;
                                        }
                                    }
                                }
                            }

                            else
                            // else if (_isModified == true )
                            {
                                //if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberName == txtSubFName.Text && oIns.SubscriberMName == txtSubMName.Text && oIns.SubscriberLName == txtSubLName.Text && oIns.SubscriberID == txtSubscriberID.Text)
                                if (cmbRelationShip.Text.Trim().ToLower() != "Self".ToLower() && chkCompany.Checked == true)
                                {
                                    if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberCompanyLName == txtCompanyName.Text.Trim() && oIns.SubscriberID.ToUpper() == txtSubscriberID.Text.ToUpper() && oIns.Group == txtGroup.Text)
                                    {
                                        //Only one plan Active at a time
                                        if (_nInsuranceFlag > 0 && oIns.InsuranceFlag > 0)
                                        {
                                            _dlgRst = MessageBox.Show(this, "Duplicate Insurance already exists. The new insurance will be created and the previous insurance will be inactivated. Continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                                            if (_dlgRst == DialogResult.Yes)
                                            {

                                                _result = true;
                                                oIns.InsuranceFlag = 0;
                                                tvInsurances.Nodes[0].Nodes[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                                            }
                                            else if (_dlgRst == DialogResult.No)
                                            {
                                                _result = false;
                                            }
                                            else if (_dlgRst == DialogResult.Cancel)
                                            {
                                                _result = false;

                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberFName.ToUpper().Trim() == txtSubFName.Text.ToUpper().Trim() && oIns.SubscriberMName.ToUpper().Trim() == txtSubMName.Text.ToUpper().Trim() && oIns.SubscriberLName.ToUpper().Trim() == txtSubLName.Text.ToUpper().Trim() && oIns.SubscriberID.ToUpper() == txtSubscriberID.Text.ToUpper() && oIns.Group == txtGroup.Text)
                                    {
                                        //Only one plan Active at a time
                                        if (_nInsuranceFlag > 0 && oIns.InsuranceFlag > 0)
                                        {
                                            if (MessageBox.Show(this, "Duplicate Insurance already exists. The new insurance will be created and the previous insurance will be inactivated. Continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {

                                                _result = true;
                                                oIns.InsuranceFlag = 0;
                                                tvInsurances.Nodes[0].Nodes[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                                            }
                                            else
                                            {
                                                _result = false;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (oIns.IsNotDOB == false)
                            {
                                //Same Insurance Check
                                if (cmbRelationShip.Text.Trim().ToLower() != "Self".ToLower() && chkCompany.Checked == true)
                                {
                                    if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberCompanyLName == txtCompanyName.Text.Trim() && oIns.SubscriberID.ToUpper() == txtSubscriberID.Text.ToUpper() && oIns.Group == txtGroup.Text)
                                    {
                                        //Only one plan Active at a time
                                        if (_nInsuranceFlag > 0 && oIns.InsuranceFlag > 0)
                                        {
                                            _dlgRst = MessageBox.Show(this, "Duplicate Insurance already exists. The new insurance will be created and the previous insurance will be inactivated. Continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                                            if (_dlgRst == DialogResult.Yes)
                                            {

                                                _result = true;
                                                oIns.InsuranceFlag = 0;
                                                tvInsurances.Nodes[0].Nodes[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                                            }
                                            else if (_dlgRst == DialogResult.No)
                                            {
                                                _result = false;
                                            }
                                            else if (_dlgRst == DialogResult.Cancel)
                                            {
                                                _result = false;

                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberFName.ToUpper().Trim() == txtSubFName.Text.ToUpper().Trim() && oIns.SubscriberMName.ToUpper().Trim() == txtSubMName.Text.ToUpper().Trim() && oIns.SubscriberLName.ToUpper().Trim() == txtSubLName.Text.ToUpper().Trim() && oIns.SubscriberID.ToUpper() == txtSubscriberID.Text.ToUpper() && oIns.DOB.ToString("MMddyyyy") == mtxtDOB.Text.ToString() && oIns.Group == txtGroup.Text)
                                    {
                                        //Only one plan Active at a time
                                        if (_nInsuranceFlag > 0 && oIns.InsuranceFlag > 0)
                                        {
                                            _dlgRst = MessageBox.Show(this, "Duplicate Insurance is already active and will be inactivated. Continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                                            if (_dlgRst == DialogResult.Yes)
                                            {

                                                _result = true;
                                                oIns.InsuranceFlag = 0;
                                                tvInsurances.Nodes[0].Nodes[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                                            }
                                            else if (_dlgRst == DialogResult.No)
                                            {
                                                _result = false;
                                            }
                                            else if (_dlgRst == DialogResult.Cancel)
                                            {
                                                _result = false;

                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (cmbRelationShip.Text.Trim().ToLower() != "Self".ToLower() && chkCompany.Checked == true)
                                {
                                    if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberCompanyLName == txtCompanyName.Text.Trim() && oIns.SubscriberID.ToUpper() == txtSubscriberID.Text.ToUpper() && oIns.Group == txtGroup.Text)
                                    {
                                        //Only one plan Active at a time
                                        if (_nInsuranceFlag > 0 && oIns.InsuranceFlag > 0)
                                        {
                                            _dlgRst = MessageBox.Show(this, "Duplicate Insurance already exists. The new insurance will be created and the previous insurance will be inactivated. Continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                                            if (_dlgRst == DialogResult.Yes)
                                            {

                                                _result = true;
                                                oIns.InsuranceFlag = 0;
                                                tvInsurances.Nodes[0].Nodes[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                                            }
                                            else if (_dlgRst == DialogResult.No)
                                            {
                                                _result = false;
                                            }
                                            else if (_dlgRst == DialogResult.Cancel)
                                            {
                                                _result = false;

                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (oIns.InsuranceName == txtInsurance.Text && oIns.SubscriberFName.ToUpper().Trim() == txtSubFName.Text.ToUpper().Trim() && oIns.SubscriberMName.ToUpper().Trim() == txtSubMName.Text.ToUpper().Trim() && oIns.SubscriberLName.ToUpper().Trim() == txtSubLName.Text.ToUpper().Trim() && oIns.SubscriberID.ToUpper() == txtSubscriberID.Text.ToUpper() && oIns.Group == txtGroup.Text)
                                    {
                                        //Only one plan Active at a time
                                        if (_nInsuranceFlag > 0 && oIns.InsuranceFlag > 0)
                                        {
                                            if (MessageBox.Show(this, "Duplicate Insurance is already active and will be inactivated. Continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {

                                                _result = true;
                                                oIns.InsuranceFlag = 0;
                                                tvInsurances.Nodes[0].Nodes[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                                            }
                                            else
                                            {
                                                _result = false;
                                            }
                                            break;
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
            finally
            {
                mtxtDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
            }
            return _result;

        }
        #endregion "ValidateMultipleInsurance"


        //Here we Insert the Insurance information in the Insurance object  
        // and add each respective Insurance object to the tree node.
        private bool AddInsurance()
        {
            bool _Result = false;
            string sInsDetails = string.Empty;
            string sSubscriberDetails = string.Empty;
            Int64 nInsContactID;
            
            try
            {
                
                if (txtInsurance.Tag == null || Convert.ToInt64(txtInsurance.Tag)==0)
                {
                    return false;
                }

                if (Validate_InsuranceSubscriberMandatory() == false)
                {
                    return false;
                }
                //Insurance Group Mandantory
                if (bIsGroupMandatory(Convert.ToInt64(txtInsurance.Tag)) == true)
                {
                    if (txtGroup.Text.Trim().ToString() == "")
                    {
                        MessageBox.Show(this, "Please enter group value.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGroup.Focus();
                        return false;
                    }
                }


                #region "Validate Amounts"
                //???
                if ((txtCopay.Text.Trim() == "") || (txtCopay.Text.Trim() == "."))
                {
                    if (txtCopay.Text.Trim() == ".")
                    {
                        txtCopay.Text = "0.00";
                    }
                }
                if ((txtDeductableAmount.Text.Trim() == "") || (txtDeductableAmount.Text.Trim() == "."))
                {
                    if (txtDeductableAmount.Text.Trim() == ".")
                    {
                        txtDeductableAmount.Text = "0.00";
                    }
                }
                if ((txtCoveragePercent.Text.Trim() == "") || (txtCoveragePercent.Text.Trim() == "."))
                {
                    if (txtCoveragePercent.Text.Trim() == ".")
                    {
                        txtCoveragePercent.Text = "0.00";
                    }
                }
                else if (Convert.ToDecimal(txtCoveragePercent.Text) > 100)
                {
                    MessageBox.Show(this, "Coverage % should be less than 100.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCoveragePercent.Focus();
                    return false;
                }

                #endregion "Validate Amounts"

                Boolean IsModify = false;
                TreeNode oNode;
                Insurance oInsurance = null;
                oNode = null;

                //check for existing node 
                //SHUBHANGI 20100708 TO SET THE CURRENT INDEX OF SELECTED TREE NODE
                Int32 currentindex = -1;
                Insurance oIns = new Insurance();

                if (tvInsurances.SelectedNode != null)
                {
                    if (tvInsurances.SelectedNode.Text != "Insurance")
                    {
                        oIns = (Insurance)tvInsurances.Nodes[0].Nodes[tvInsurances.SelectedNode.Index].Tag;

                        currentindex = tvInsurances.SelectedNode.Index;
                    }
                }

                if (currentindex == -1)
                {
                    _isModified = false;
                    if (ValidateMultipleInsurance(currentindex) == false)
                    {
                        //_NodeChanged = true;
                        return false;
                    }

                }
                else
                {
                    _isModified = true;
                    if (ValidateMultipleInsurance(currentindex) == false)
                    {
                        //COMMENTED BY SHUBHANGI 20100724
                        // _NodeChanged = true;
                        return false;
                    }

                    IsModify = true;
                    oNode = tvInsurances.Nodes[0].Nodes[currentindex];
                    oInsurance = oIns;

                    if (_IsInsurancesModified == false)
                    {
                        _IsInsurancesModified = IsModified(oIns);
                    }

                }


                if (oNode == null)
                {
                    oNode = new TreeNode();
                    //oNode.ImageIndex = 1;
                    //oNode.SelectedImageIndex = 1;
                    oInsurance = new Insurance();
                }

                if (txtInsurance.Text.Trim() == "")
                {
                    MessageBox.Show(this, "Please select the name of the insurance.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtInsurance.Focus();
                    btnInsBrowse.Focus();
                    return false;
                }

                //...code changes done by Sagar Ghodke on 20100330
                //...Code changes done to consider MA also as medicare
                string _InsType = string.Empty;
                _InsType = GetInsuranceType(Convert.ToInt64(txtInsurance.Tag)).Trim().ToUpper();

                //string[] insurnaceArr = null;
                //insurnaceArr = Convert.ToString(txtInsurance.Tag).Split('|');
                //Int64 insCnttid;
                //insCnttid = Convert.ToInt64(insurnaceArr[0]);
                //_InsType = GetInsuranceType(Convert.ToInt64(insCnttid)).Trim().ToUpper();
                if (_InsType == "MB" || _InsType == "MA")
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txtSubscriberID.Text.Trim(), @"^\d{9}[a-zA-Z]{1}$") == false)
                    {
                        //Commented By Pramod Nair For allowing the user to enter any combination of alphanumeric characters for any kind of insurance
                        //MessageBox.Show(this, "Please enter 9 digit number and 1 alpha character in the end for Insurance ID.\nExample: 123456789A  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtSubscriberID.Focus();
                        //return false;
                    }
                    if (Convert.ToString(cmbRelationShip.Text).ToUpper() != "SELF")
                    {
                        MessageBox.Show(this, "Relationship must be Self for Medicare.   ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbRelationShip.Focus();
                        return false;
                    }
                }


                if (radSetAsPrimary.Checked)
                {
                    #region "Validation to have only one primary insurance"

                    ////Check Primary Insurance
                    //for (int i = 0; i <= tvInsurances.Nodes[0].Nodes.Count - 1; i++)
                    //{
                    //    Insurance oIns = (Insurance)tvInsurances.Nodes[0].Nodes[i].Tag;
                    //    if ( (oIns.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.Primary) && ((Int64)txtInsurance.Tag != oIns.ContactID))
                    //    {
                    //        if ((MessageBox.Show(this, "Primary insurance is already present. \nDo You want to replace with this insurance?", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel))
                    //        {
                    //            return false;
                    //        }
                    //        else
                    //        {
                    //            oIns.InsuranceFlag = (Int64)(Insurance.InsuranceTypeFlag.None);
                    //            tvInsurances.Nodes[0].Nodes[i].ForeColor = Color.Black;
                    //        }
                    //    }
                    //}

                    #endregion

                    oNode.ForeColor = Color.DarkRed;
                    oNode.ImageIndex = 2;
                    oNode.SelectedImageIndex = 2;
                }
                else if (radSetAsSecondary.Checked)
                {
                    #region "Validation to have only one Secondary insurance"

                    //Check Secondary Insurance
                    //for (int i = 0; i <= tvInsurances.Nodes[0].Nodes.Count - 1; i++)
                    //{
                    //    Insurance oIns = (Insurance)tvInsurances.Nodes[0].Nodes[i].Tag;
                    //    if ((oIns.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.Secondary) && ((Int64)txtInsurance.Tag != oIns.ContactID))
                    //    {
                    //        if (MessageBox.Show(this, "Secondary insurance is already present. \nDo you want to replace with this insurance?", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    //        {
                    //            return false;
                    //        }
                    //        else
                    //        {
                    //            oIns.InsuranceFlag = (Int64)(Insurance.InsuranceTypeFlag.None);
                    //            tvInsurances.Nodes[0].Nodes[i].ForeColor = Color.Black;
                    //        }

                    //    }
                    //}

                    #endregion

                    oNode.ForeColor = Color.OrangeRed;

                    oNode.ImageIndex = 3;
                    oNode.SelectedImageIndex = 3;
                }
                else if (radSetAsTertiary.Checked)
                {
                    #region "Validation to have only one Tertiary insurance"

                    //Check Tertiary Insurance
                    //for (int i = 0; i <= tvInsurances.Nodes[0].Nodes.Count - 1; i++)
                    //{
                    //    Insurance oIns = (Insurance)tvInsurances.Nodes[0].Nodes[i].Tag;
                    //    if ((oIns.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.Tertiary) && ((Int64)txtInsurance.Tag != oIns.ContactID))
                    //    {
                    //        if (MessageBox.Show(this, "Tertiary insurance is already present.  \nDo you want to replace with this insurance?", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    //        {
                    //            return false;
                    //        }
                    //        else
                    //        {
                    //            oIns.InsuranceFlag = (Int64)Insurance.InsuranceTypeFlag.None;
                    //            tvInsurances.Nodes[0].Nodes[i].ForeColor = Color.Black;
                    //        }
                    //    }
                    //}

                    #endregion

                    oNode.ForeColor = Color.ForestGreen;

                    oNode.ImageIndex = 4;
                    oNode.SelectedImageIndex = 4;
                }
                else
                {
                    //COMMENTED BY SHUBHANGI 20100720
                    // oNode.ForeColor = Color.Black;
                    //ADDED BY SHUBHANGI 20100720
                    oNode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));

                    oNode.ImageIndex = 5;
                    oNode.SelectedImageIndex = 5;
                }


                oNode.Text = txtInsurance.Text.Trim();
                //ADDED BY SHUBHANGI 

                oInsurance.InsuranceName = txtInsurance.Text.Trim();

                oInsurance.ContactID = Convert.ToInt64(txtInsurance.Tag);



                //if (currentindex != -1)
                //SHUBHANGI 20100708 IF THE NEW NODE IS ADDED THEN SET THE INDEX FOR THAT NEW NODE
                if (IsModify == false)
                {
                    oInsurance.Index = tvInsurances.Nodes[0].Nodes.Count;
                }

                oInsurance.SubscriberName = txtSubFName.Text.Trim();
                oInsurance.SubscriberID = txtSubscriberID.Text.Trim();
                oInsurance.SubscriberPolicy = txtSubscriberPolicy.Text.Trim();
                oInsurance.Group = txtGroup.Text.Trim();
                oInsurance.Employer = txtEmployer.Text.Trim();
                oInsurance.IsSameAsPatient = chkSameAsPatient.Checked;
                oInsurance.IsAddressSameAsPatient = chkAddrSameAsPatient.Checked;

                oInsurance.InsTypeCodeDefault = cmbDefaultTypeCode.SelectedValue.ToString();
                oInsurance.InsTypeDescriptionDefault = cmbDefaultTypeCode.Text;
                oInsurance.InsTypeCodeMedicare = cmbMedicareTypeCode.SelectedValue.ToString();
                oInsurance.InsTypeDescriptionMedicare = cmbMedicareTypeCode.Text;

                oInsurance.Phone = mskInsurancePhone.Text.Trim();
                //Sandip Darade 20100216
                //case  GLO2008-0002029
                //phone no,mobile no ,fax no will be saved with  mask e.g .(111)222-3333
                //if (mskInsurancePhone.Text.Length != 0 && mskInsurancePhone.MaskFull == false)
                //{
                //    oInsurance.Phone = "";
                //}
                //else
                //{
                //    oInsurance.Phone = mskInsurancePhone.Text;

                //}
                oInsurance.PrimaryFlag = radSetAsPrimary.Checked;

                if (rbGender1.Checked)
                {
                    oInsurance.SubscriberGender = "Male";
                }
                if (rbGender2.Checked)
                {
                    oInsurance.SubscriberGender = "Female";
                }
                if (rbGender3.Checked)
                {
                    oInsurance.SubscriberGender = "Other";
                }
                if (!(rbGender1.Checked || rbGender2.Checked || rbGender3.Checked))
                {
                    oInsurance.SubscriberGender = "";
                }


                if (radSetAsPrimary.Checked)
                { oInsurance.InsuranceFlag = Convert.ToInt64(Insurance.InsuranceTypeFlag.Primary); }
                else if (radSetAsSecondary.Checked)
                { oInsurance.InsuranceFlag = Convert.ToInt64(Insurance.InsuranceTypeFlag.Secondary); }
                else if (radSetAsTertiary.Checked)
                { oInsurance.InsuranceFlag = Convert.ToInt64(Insurance.InsuranceTypeFlag.Tertiary); }
                else if (rbInactive.Checked)
                { oInsurance.InsuranceFlag = Convert.ToInt64(Insurance.InsuranceTypeFlag.None); }


                // Added on 20080926...

                oInsurance.AssignmentofBenefit = chkAssignmentofBenifit.Checked;
                if (txtCopay.Text.Trim() == "")
                {
                    oInsurance.CoPay = 0;
                }
                else
                {
                    oInsurance.CoPay = Convert.ToDecimal(txtCopay.Text);
                }

                oInsurance.RelationshipID = Convert.ToInt64(cmbRelationShip.SelectedValue);
                oInsurance.RelationshipName = cmbRelationShip.Text;
                if (txtDeductableAmount.Text.Trim() == "")
                {
                    oInsurance.DeductableAmount = 0;
                }
                else
                {
                    oInsurance.DeductableAmount = Convert.ToDecimal(txtDeductableAmount.Text);
                }
                if (txtEligibilityInsurance.Text.Trim() == "")
                {
                    oInsurance.sEligibiltyInsuranceNotes = "";
                }
                else
                {
                    oInsurance.sEligibiltyInsuranceNotes = Convert.ToString(txtEligibilityInsurance.Text);
                }


                if (txtCoveragePercent.Text.Trim() == "")
                {
                    oInsurance.CoveragePercent = 0;
                }
                else
                {

                    oInsurance.CoveragePercent = Convert.ToDecimal(txtCoveragePercent.Text);

                }

                oInsurance.SubscriberFName = txtSubFName.Text.Trim();
                oInsurance.SubscriberMName = txtSubMName.Text.Trim();
                oInsurance.SubscriberLName = txtSubLName.Text.Trim();
                oInsurance.SubscriberCompanyLName = txtCompanyName.Text.Trim();

                //oInsurance.SubscriberAddr1 = txtAddress1.Text.Trim();
                //oInsurance.SubscriberAddr2 = txtAddress2.Text.Trim();
                //oInsurance.SubscriberState = cmbState.Text.Trim();
                //oInsurance.SubscriberCity = txtCity.Text.Trim();
                //oInsurance.SubscriberZip = txtZip.Text.Trim();
                //oInsurance.SubscriberCounty = txtCounty.Text.Trim();
                //oInsurance.SubscriberCountry = cmbCountry.Text.Trim();    

                //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 

                oInsurance.SubscriberAddr1 = oAddresscontrol.txtAddress1.Text.Trim();
                oInsurance.SubscriberAddr2 = oAddresscontrol.txtAddress2.Text.Trim();
                oInsurance.SubscriberState = oAddresscontrol.cmbState.Text.Trim();
                oInsurance.SubscriberCity = oAddresscontrol.txtCity.Text.Trim();
                oInsurance.SubscriberZip = oAddresscontrol.txtZip.Text.Trim();
                oInsurance.SubscriberCounty = oAddresscontrol.txtCounty.Text.Trim();
                oInsurance.SubscriberCountry = oAddresscontrol.cmbCountry.Text.Trim();


                // Added on 20080926

                // For DOB...

                mtxtDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (!IsValidDate(mtxtDOB.Text))
                {
                    MessageBox.Show("Please enter valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtxtDOB.Focus();
                    return false;
                }
                else
                {
                    if (mtxtDOB.MaskCompleted == true)
                    {
                        oInsurance.IsNotDOB = false;
                        if (Convert.ToDateTime(mtxtDOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtDOB.Text.Trim()) >= DateTime.Now || Convert.ToDateTime(mtxtDOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                        {
                            MessageBox.Show("Please enter valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mtxtDOB.Focus();
                            return false;
                        }
                        oInsurance.DOB = Convert.ToDateTime(mtxtDOB.Text.Trim());
                    }
                    else
                    {
                        oInsurance.DOB = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        oInsurance.IsNotDOB = true;
                    }
                }

                // *** For Start Date ***

                mtxtStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                mtxtEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (!IsValidDate(mtxtEndDate.Text))
                {
                    MessageBox.Show("Please enter valid end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtxtEndDate.Focus();
                    return false;

                }
                if (!IsValidDate(mtxtStartDate.Text))
                {
                    MessageBox.Show("Please enter valid start date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtxtStartDate.Focus();
                    return false;

                }
                else
                {
                    if (mtxtStartDate.MaskCompleted == true)
                    {
                        if (Convert.ToDateTime(mtxtStartDate.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtStartDate.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                        {
                            MessageBox.Show("Please enter valid start date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mtxtStartDate.Focus();
                            return false;
                        }

                        mtxtEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mtxtEndDate.Text.Length > 0)   // masking for start date is already done... 
                        {
                            // Comparision between start date and End date...
                            mtxtEndDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            if (!IsValidDate(mtxtEndDate.Text))
                            {
                                MessageBox.Show("Please enter valid end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mtxtEndDate.Focus();
                                return false;
                            }
                            if (Convert.ToDateTime((mtxtEndDate.Text)) < Convert.ToDateTime((mtxtStartDate.Text)) && Convert.ToDateTime(mtxtEndDate.Text) != Convert.ToDateTime(mtxtStartDate.Text))
                            {
                                MessageBox.Show("Start date should be less than End date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mtxtEndDate.Focus();
                                return false;
                            }
                        }

                        mtxtEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                        mtxtStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                        oInsurance.IsNotStartDate = false;
                        oInsurance.StartDate = Convert.ToDateTime(mtxtStartDate.Text.Trim());
                    }
                    else
                    {
                        oInsurance.StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        oInsurance.IsNotStartDate = true;
                    }
                }

                // *** For End date ***...

                mtxtStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                mtxtEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (!IsValidDate(mtxtEndDate.Text))
                {
                    MessageBox.Show("Please enter valid end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtxtEndDate.Focus();
                    return false;
                }
                else
                {
                    if (mtxtEndDate.MaskCompleted == true)
                    {
                        if (Convert.ToDateTime(mtxtEndDate.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtEndDate.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                        {
                            MessageBox.Show("Please enter valid end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mtxtEndDate.Focus();
                            return false;
                        }

                        // Comparision between start date and End date...

                        mtxtStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mtxtStartDate.Text.Length > 0)  // masking for END date is already done... 
                        {
                            mtxtStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            if (!IsValidDate(mtxtEndDate.Text))
                            {
                                MessageBox.Show("Please enter valid end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mtxtEndDate.Focus();
                                return false;
                            }
                            if (Convert.ToDateTime(mtxtEndDate.Text) < Convert.ToDateTime(mtxtStartDate.Text) && Convert.ToDateTime(mtxtEndDate.Text) != Convert.ToDateTime(mtxtStartDate.Text))
                            {
                                MessageBox.Show("End date should be greater than Start date .  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mtxtEndDate.Focus();
                                return false;
                            }
                        }

                        mtxtEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                        mtxtStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                        oInsurance.IsNotEndDate = false;
                        oInsurance.EndDate = Convert.ToDateTime(mtxtEndDate.Text.Trim());
                    }
                    else
                    {
                        oInsurance.EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        oInsurance.IsNotEndDate = true;
                    }
                }

                //Added By MaheshB               
                oInsurance.Isworkerscomp = Convert.ToBoolean(chkworkerscomp.Checked);
                oInsurance.Isautoclaim = Convert.ToBoolean(chkautoclaim.Checked);
                oInsurance.IsCompnay = Convert.ToBoolean(chkCompany.Checked);

                //Incomplete Phone Numbers
                //mskInsurancePhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                //if (mskInsurancePhone.Text.Length > 0 && mskInsurancePhone.MaskCompleted == false)
                //{
                //    MessageBox.Show("Please enter a 10 digit number for phone.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    mskInsurancePhone.Focus();
                //    return false;
                //}


                //------------------------
                //- Get Insurance Details

                if (IsModify == false)
                {
                    DataTable dt = new DataTable();
                    dt = GetInsuranceDetails(oInsurance.ContactID);
                    //ind = Convert.ToInt64(Arr[0]);
                    //dt = GetInsuranceDetails(ind);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        oInsurance.InsuranceID = 0;
                        oInsurance.InsuranceName = Convert.ToString(dt.Rows[0]["sInsuranceName"]);
                        oInsurance.PayerID = Convert.ToString(dt.Rows[0]["sPayerID"]);
                        oInsurance.AddrressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                        oInsurance.AddrressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                        oInsurance.City = Convert.ToString(dt.Rows[0]["sCity"]);
                        oInsurance.State = Convert.ToString(dt.Rows[0]["sState"]);
                        oInsurance.ZIP = Convert.ToString(dt.Rows[0]["sZIP"]);
                        oInsurance.InsurancePhone = Convert.ToString(dt.Rows[0]["sPhone"]);
                        oInsurance.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                        oInsurance.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                        oInsurance.URL = Convert.ToString(dt.Rows[0]["sURL"]);
                        oInsurance.InsuranceTypeCode = Convert.ToString(dt.Rows[0]["sInsuranceTypeCode"]);
                        oInsurance.InsuranceTypeDesc = Convert.ToString(dt.Rows[0]["sInsuranceTypeDesc"]);
                        oInsurance.bAccessAssignment = Convert.ToBoolean(dt.Rows[0]["bAccessAssignment"]);
                        oInsurance.bStatementToPatient = Convert.ToBoolean(dt.Rows[0]["bStatementToPatient"]);
                        oInsurance.bMedigap = Convert.ToBoolean(dt.Rows[0]["bMedigap"]);
                        oInsurance.bReferringIDInBox19 = Convert.ToBoolean(dt.Rows[0]["bReferringIDInBox19"]);
                        oInsurance.bNameOfacilityinBox33 = Convert.ToBoolean(dt.Rows[0]["bNameOfacilityinBox33"]);
                        oInsurance.bDoNotPrintFacility = Convert.ToBoolean(dt.Rows[0]["bDoNotPrintFacility"]);
                        oInsurance.b1stPointer = Convert.ToBoolean(dt.Rows[0]["b1stPointer"]);
                        oInsurance.bBox31Blank = Convert.ToBoolean(dt.Rows[0]["bBox31Blank"]);
                        oInsurance.bShowPayment = Convert.ToBoolean(dt.Rows[0]["bShowPayment"]);
                        oInsurance.nTypeOBilling = (TypeOfBilling)Convert.ToInt32((dt.Rows[0]["nTypeOBilling"]));
                        oInsurance.nClearingHouse = Convert.ToInt64(dt.Rows[0]["nClearingHouse"]);
                        oInsurance.bIsClaims = Convert.ToBoolean(dt.Rows[0]["bIsClaims"]);
                        oInsurance.bIsRemittanceAdvice = Convert.ToBoolean(dt.Rows[0]["bIsRemittanceAdvice"]);
                        oInsurance.bIsRealTimeEligibility = Convert.ToBoolean(dt.Rows[0]["bIsRealTimeEligibility"]);
                        oInsurance.bIsElectronicCOB = Convert.ToBoolean(dt.Rows[0]["bIsElectronicCOB"]);
                        oInsurance.bIsRealTimeClaimStatus = Convert.ToBoolean(dt.Rows[0]["bIsRealTimeClaimStatus"]);
                        oInsurance.bIsEnrollmentRequired = Convert.ToBoolean(dt.Rows[0]["bIsEnrollmentRequired"]);
                        oInsurance.sPayerPhone = Convert.ToString(dt.Rows[0]["sPayerPhone"]);
                        oInsurance.sWebsite = Convert.ToString(dt.Rows[0]["sWebsite"]);
                        oInsurance.sServicingState = Convert.ToString(dt.Rows[0]["sServicingState"]);
                        oInsurance.sComments = Convert.ToString(dt.Rows[0]["sComments"]);
                        oInsurance.sPayerPhoneExtn = Convert.ToString(dt.Rows[0]["sPayerPhoneExtn"]);
                        oInsurance.bNotesInBox19 = Convert.ToBoolean(dt.Rows[0]["bNotesInBox19"]);
                        oInsurance.OfficeID = Convert.ToString(dt.Rows[0]["sOfficeID"]);

                        //Add Card Image from OCR

                        //HT.Add(txtInsurance.Tag, _Fcard);
                        //HT.Add(txtInsurance.Tag.ToString() + "B", _Bcard);
                        //_Fcard = null;
                        //_Bcard = null;



                        if (_Fcard != null)
                        {
                            int _rowCount = dtScannedImages.Rows.Count;
                            if (_rowCount > 1)
                            {
                                _rowCount = _rowCount + 1;
                            }


                            DataRow dr = dtScannedImages.NewRow();
                            dr["ID"] = oInsurance.ContactID;
                            if (dr["FrontImage"] != null)
                            {
                                dr["FrontImage"] = null;
                            }
                            dr["FrontImage"] = _Fcard.Clone();
                            if (_Bcard != null)
                            {
                                if (dr["BackImage"] != null)
                                {
                                    dr["BackImage"] = null;
                                }
                                dr["BackImage"] = _Bcard.Clone();
                            }

                            if (radSetAsPrimary.Checked)
                            { dr["bIsActive"] = Convert.ToInt64(Insurance.InsuranceTypeFlag.Primary); }
                            else if (radSetAsSecondary.Checked)
                            { dr["bIsActive"] = Convert.ToInt64(Insurance.InsuranceTypeFlag.Secondary); }
                            else if (radSetAsTertiary.Checked)
                            { dr["bIsActive"] = Convert.ToInt64(Insurance.InsuranceTypeFlag.Tertiary); }
                            else if (rbInactive.Checked)
                            { dr["bIsActive"] = Convert.ToInt64(Insurance.InsuranceTypeFlag.None); }
                            dtScannedImages.Rows.InsertAt(dr, _rowCount + 1);
                            dtScannedImages.AcceptChanges();
                            if (_Fcard != null)
                            {
                                _Fcard.Dispose();
                                _Fcard = null;
                            }
                            if (_Bcard != null)
                            {
                                _Bcard.Dispose();
                                _Bcard = null;
                            }
                        }
                    }
                }
                else
                {
                    #region "If not modify but insurance master is modify then this flag is false but modified insurance detail we will get in this block"
                    DataTable dt = new DataTable();
                    dt = GetInsuranceDetails(oInsurance.ContactID);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //oInsurance.InsuranceID = 0;
                        oInsurance.InsuranceName = Convert.ToString(dt.Rows[0]["sInsuranceName"]);
                        oInsurance.PayerID = Convert.ToString(dt.Rows[0]["sPayerID"]);
                        oInsurance.AddrressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                        oInsurance.AddrressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                        oInsurance.City = Convert.ToString(dt.Rows[0]["sCity"]);
                        oInsurance.State = Convert.ToString(dt.Rows[0]["sState"]);
                        oInsurance.ZIP = Convert.ToString(dt.Rows[0]["sZIP"]);
                        oInsurance.InsurancePhone = Convert.ToString(dt.Rows[0]["sPhone"]);
                        oInsurance.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                        oInsurance.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                        oInsurance.URL = Convert.ToString(dt.Rows[0]["sURL"]);
                        oInsurance.InsuranceTypeCode = Convert.ToString(dt.Rows[0]["sInsuranceTypeCode"]);
                        oInsurance.InsuranceTypeDesc = Convert.ToString(dt.Rows[0]["sInsuranceTypeDesc"]);
                        oInsurance.bAccessAssignment = Convert.ToBoolean(dt.Rows[0]["bAccessAssignment"]);
                        oInsurance.bStatementToPatient = Convert.ToBoolean(dt.Rows[0]["bStatementToPatient"]);
                        oInsurance.bMedigap = Convert.ToBoolean(dt.Rows[0]["bMedigap"]);
                        oInsurance.bReferringIDInBox19 = Convert.ToBoolean(dt.Rows[0]["bReferringIDInBox19"]);
                        oInsurance.bNameOfacilityinBox33 = Convert.ToBoolean(dt.Rows[0]["bNameOfacilityinBox33"]);
                        oInsurance.bDoNotPrintFacility = Convert.ToBoolean(dt.Rows[0]["bDoNotPrintFacility"]);
                        oInsurance.b1stPointer = Convert.ToBoolean(dt.Rows[0]["b1stPointer"]);
                        oInsurance.bBox31Blank = Convert.ToBoolean(dt.Rows[0]["bBox31Blank"]);
                        oInsurance.bShowPayment = Convert.ToBoolean(dt.Rows[0]["bShowPayment"]);
                        oInsurance.nTypeOBilling = (TypeOfBilling)Convert.ToInt32((dt.Rows[0]["nTypeOBilling"]));
                        oInsurance.nClearingHouse = Convert.ToInt64(dt.Rows[0]["nClearingHouse"]);
                        oInsurance.bIsClaims = Convert.ToBoolean(dt.Rows[0]["bIsClaims"]);
                        oInsurance.bIsRemittanceAdvice = Convert.ToBoolean(dt.Rows[0]["bIsRemittanceAdvice"]);
                        oInsurance.bIsRealTimeEligibility = Convert.ToBoolean(dt.Rows[0]["bIsRealTimeEligibility"]);
                        oInsurance.bIsElectronicCOB = Convert.ToBoolean(dt.Rows[0]["bIsElectronicCOB"]);
                        oInsurance.bIsRealTimeClaimStatus = Convert.ToBoolean(dt.Rows[0]["bIsRealTimeClaimStatus"]);
                        oInsurance.bIsEnrollmentRequired = Convert.ToBoolean(dt.Rows[0]["bIsEnrollmentRequired"]);
                        oInsurance.sPayerPhone = Convert.ToString(dt.Rows[0]["sPayerPhone"]);
                        oInsurance.sWebsite = Convert.ToString(dt.Rows[0]["sWebsite"]);
                        oInsurance.sServicingState = Convert.ToString(dt.Rows[0]["sServicingState"]);
                        oInsurance.sComments = Convert.ToString(dt.Rows[0]["sComments"]);
                        oInsurance.sPayerPhoneExtn = Convert.ToString(dt.Rows[0]["sPayerPhoneExtn"]);
                        oInsurance.bNotesInBox19 = Convert.ToBoolean(dt.Rows[0]["bNotesInBox19"]);
                        oInsurance.OfficeID = Convert.ToString(dt.Rows[0]["sOfficeID"]);

                    }
                    #endregion

                    if (_Fcard != null)
                    {
                        int _rowCount = dtScannedImages.Rows.Count;
                        if (_rowCount > 1)
                        {
                            _rowCount = _rowCount + 1;
                        }


                        DataRow dr = dtScannedImages.NewRow();
                        dr["ID"] = oInsurance.ContactID;
                        if (dr["FrontImage"] != null)
                        {
                            dr["FrontImage"] = null;
                        }
                        dr["FrontImage"] = _Fcard.Clone();
                        if (_Bcard != null)
                        {
                            if (dr["BackImage"] != null)
                            {
                                dr["BackImage"] = null;
                            }
                            dr["BackImage"] = _Bcard.Clone();
                        }

                        if (radSetAsPrimary.Checked)
                        { dr["bIsActive"] = Convert.ToInt64(Insurance.InsuranceTypeFlag.Primary); }
                        else if (radSetAsSecondary.Checked)
                        { dr["bIsActive"] = Convert.ToInt64(Insurance.InsuranceTypeFlag.Secondary); }
                        else if (radSetAsTertiary.Checked)
                        { dr["bIsActive"] = Convert.ToInt64(Insurance.InsuranceTypeFlag.Tertiary); }
                        else if (rbInactive.Checked)
                        { dr["bIsActive"] = Convert.ToInt64(Insurance.InsuranceTypeFlag.None); }
                        dtScannedImages.Rows.InsertAt(dr, _rowCount + 1);
                        dtScannedImages.AcceptChanges();
                        if (_Fcard != null)
                        {
                            _Fcard.Dispose();
                            _Fcard = null;
                        }
                        if (_Bcard != null)
                        {
                            _Bcard.Dispose();
                            _Bcard = null;
                        }
                    }
                }
                //------------------------

                //oInsurance.DOB= Convert.ToDateTime(mtxtIDOB.Text);
                //Add the Insurance object to the tree view tag.
                oNode.Tag = oInsurance;
                bool bIsClearInsInfo=false;
                bool bIsEligibilitySuccess = false;
                if (IsModify == false)
                {
                    DialogResult diaRes;
                    bIsEligibilitySuccess=EiligibilityCheckOnInsuranceAdd(oInsurance,out diaRes);
                    if (diaRes == DialogResult.No)
                    {
                        tvInsurances.Nodes[0].Nodes.Add(oNode);
                        bIsClearInsInfo = true;
                    }
                    else
                    {
                        if (bIsEligibilitySuccess)
                        {
                            tvInsurances.Nodes[0].Nodes.Add(oNode);
                            bIsClearInsInfo = true;
                        }
                    }
                    
                }


                if (bIsClearInsInfo)
                {
                    ClearInsuranceInfo();
                }
                else if (!bIsEligibilitySuccess)
                {
                    ClearInsuranceInfo();
                }
                _Result = true;
                nInsContactID=Convert.ToInt64(oInsurance.ContactID);
                
                //Name|Gender|DOB|Add1|Add2|City|State|Zip|Country|Phone
                sSubscriberDetails = string.Format("Subscriber Info: {0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}", Convert.ToString(oInsurance.SubscriberFName) + " " + Convert.ToString(oInsurance.SubscriberMName) + " " + Convert.ToString(oInsurance.SubscriberLName), Convert.ToString(oInsurance.SubscriberGender), Convert.ToString(oInsurance.DOB), Convert.ToString(oInsurance.SubscriberAddr1), Convert.ToString(oInsurance.SubscriberAddr2), Convert.ToString(oInsurance.SubscriberCity), Convert.ToString(oInsurance.SubscriberState), Convert.ToString(oInsurance.SubscriberZip), Convert.ToString(oInsurance.SubscriberCountry), Convert.ToString(oInsurance.Phone));
                //Name|Type|Insurance Type Code|Contact ID|Subscriber Details
                sInsDetails = string.Format("Insurance details: {0}|{1}|{2}|{3}|{4}|{5}|~{6}", Convert.ToString(oInsurance.InsuranceName), Convert.ToString(oInsurance.InsuranceTypeDesc), Convert.ToString(oInsurance.InsuranceTypeCode), Convert.ToString(oInsurance.ContactID), Convert.ToString(oInsurance.SubscriberID), Convert.ToString(oInsurance.RelationshipName), sSubscriberDetails);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            MakeAuditLog(nInsContactID, sInsDetails, _isModified, _Result);
            return _Result;
        }
        private bool bIsGroupMandatory(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _bIsGroupMandatory = false;
            object _Result = null;
            try
            {
                oDB.Connect(false);
                string SqlQuery = "Select ISNULL(bIsGroupMandatory,0) as bIsGroupMandatory from Contacts_Insurance_DTL where nContactID=" + nContactID;
                _Result = oDB.ExecuteScalar_Query(SqlQuery);
                if (_Result != null)
                {
                    _bIsGroupMandatory = Convert.ToBoolean(_Result);
                }

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
                    oDB = null;
                }
            }
            return _bIsGroupMandatory;
        }
        
        private void MakeAuditLog(Int64 nInsContactID, string sInsuranceDetails, bool bIsMidify,bool bResult)
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
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.Modify, sInsuranceDetails, _PatientID, nInsContactID, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.Modify, sInsuranceDetails, _PatientID, nInsContactID, 0, gloAuditTrail.ActivityOutCome.Failure);
                        }
                    }
                    else
                    {
                        //acitity=add
                        if (bResult)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.Add, sInsuranceDetails, _PatientID, nInsContactID, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.Add, sInsuranceDetails, _PatientID, nInsContactID, 0, gloAuditTrail.ActivityOutCome.Failure);
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

        private DataTable GetInsuranceDetails(long contactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;

            try
            {
                if (oDB != null)
                {
                    oDB.Connect(false);

                    string SqlQuery = " SELECT  ISNULL(Contacts_MST.sAddressLine1,'') as sAddressLine1, ISNULL(Contacts_MST.sAddressLine2,'') as sAddressLine2, ISNULL(Contacts_MST.sCity,'') as sCity, ISNULL(Contacts_MST.sState,'') as sState," +
                                    " ISNULL(Contacts_MST.sZIP,'') as sZIP,ISNULL(Contacts_MST.sPhone,'')as sPhone,ISNULL(Contacts_MST.sFax,'') as sFax, ISNULL(Contacts_MST.sEmail,'') as sEmail, ISNULL(Contacts_MST.sURL,'') as sURL, ISNULL(Contacts_MST.sMobile,'') as sMobile, ISNULL(Contacts_MST.sPager,'') as sPager," +
                                    " ISNULL(Contacts_MST.sContact,'') as sContact, ISNULL(Contacts_MST.sName,'') as sInsuranceName,ISNULL(Contacts_Insurance_DTL.sInsuranceTypeCode,'') as sInsuranceTypeCode, ISNULL(Contacts_Insurance_DTL.sInsuranceTypeDesc,'') as sInsuranceTypeDesc," +
                                    " ISNULL(Contacts_Insurance_DTL.sPayerId,'') as sPayerId, ISNULL(Contacts_Insurance_DTL.bAccessAssignment,'false') AS bAccessAssignment, ISNULL(Contacts_Insurance_DTL.bStatementToPatient,'false') AS bStatementToPatient, ISNULL(Contacts_Insurance_DTL.bMedigap,'false') AS bMedigap ," +
                                    " ISNULL(Contacts_Insurance_DTL.bReferringIDInBox19,'false') AS bReferringIDInBox19, ISNULL(Contacts_Insurance_DTL.bNameOfacilityinBox33,'false') AS bNameOfacilityinBox33,ISNULL(Contacts_Insurance_DTL.bDoNotPrintFacility,'false') AS bDoNotPrintFacility,ISNULL(Contacts_Insurance_DTL.b1stPointer,'false') AS b1stPointer,ISNULL(Contacts_Insurance_DTL.bBox31Blank,'false') AS bBox31Blank, ISNULL(Contacts_Insurance_DTL.bShowPayment,'false') AS bShowPayment, isnull(Contacts_Insurance_DTL.nTypeOBilling,0) as nTypeOBilling, isnull(Contacts_Insurance_DTL.nClearingHouse,0 ) as nClearingHouse ," +
                                    "ISNULL(Contacts_Insurance_DTL.bIsClaims,'false') AS  bIsClaims,ISNULL(Contacts_Insurance_DTL.bIsRemittanceAdvice,'false') AS bIsRemittanceAdvice ,ISNULL(Contacts_Insurance_DTL.bIsRealTimeEligibility,'false') AS  bIsRealTimeEligibility," +
                                    "ISNULL(Contacts_Insurance_DTL.bIsElectronicCOB,'false') AS  bIsElectronicCOB,ISNULL(Contacts_Insurance_DTL.bIsRealTimeClaimStatus,'false') AS bIsRealTimeClaimStatus ,ISNULL(Contacts_Insurance_DTL.bIsEnrollmentRequired,'false') AS bIsEnrollmentRequired ,  " +
                                    "ISNULL(Contacts_Insurance_DTL.sPayerPhone,'') AS sPayerPhone ,ISNULL(Contacts_Insurance_DTL.sWebsite,'') AS  sWebsite,ISNULL(Contacts_Insurance_DTL.sServicingState,'') AS sServicingState , " +
                                    "ISNULL(Contacts_Insurance_DTL.sComments,'') AS  sComments,ISNULL(Contacts_Insurance_DTL.sPayerPhoneExtn,'') AS  sPayerPhoneExtn, ISNULL(Contacts_Insurance_DTL.bNotesInBox19,'false') AS bNotesInBox19, ISNULL(Contacts_Insurance_DTL.sOfficeID,'') AS sOfficeID " +
                                    " FROM  Contacts_MST left outer  join Contacts_Insurance_DTL ON  Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID WHERE  Contacts_MST.nContactID ='" + contactID + "' AND Contacts_MST.nClinicID=" + _ClinicID + " AND ISNULL(bIsBlocked,0) = 0 ";

                    oDB.Retrive_Query(SqlQuery, out  dt);
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
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
                    oDB = null;
                }
            }
            return dt;
        }

        private void RemoveInsurance()
        {
            gloPatient oPatient = new gloPatient(_databaseconnectionstring);
            try
            {
                if (tvInsurances.SelectedNode != null)
                {
                    if (tvInsurances.SelectedNode.Level != 0)
                    {
                        //Sandip Darade 20091021 prompt a message if user trying to remove an insurance 
                        DialogResult res = MessageBox.Show("Are you sure you want to remove selected insurance? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (res == DialogResult.Yes)
                        {

                            Int64 InsuranceID = 0;
                            Insurance oIns = tvInsurances.SelectedNode.Tag as Insurance;
                            if (oIns != null)
                            {
                                if (_IsSaveAsCopy)
                                {
                                    InsuranceID = 0;
                                }
                                else
                                {
                                    InsuranceID = oIns.InsuranceID;
                                }
                            }

                            //Validate Insurance bafore deleting. 
                            //Do not delete insurance if it is used in billing 
                            if (oPatient.IsInsuranceUsed(InsuranceID) == false)
                            {
                                ClearInsuranceInfo();
                                //string selinsurance= tvInsurances.SelectedNode.Text;
                                //Int64 cnt = 0;
                                //for (int i = 0; i < tvInsurances.Nodes[0].Nodes.Count - 1; i++)
                                //{
                                //    Insurance oInsTemp = (Insurance)tvInsurances.Nodes[0].Nodes[i].Tag;
                                //    if (selinsurance == tvInsurances.Nodes[0].Nodes[i].Text && oInsTemp.InsuranceFlag ==0)
                                //    {

                                //        cnt = cnt + 1;
                                //      //  oInsTemp,in
                                //    }
                                //}
                                //if (cnt > 0)
                                //{

                                DeletedInsurances.Add(InsuranceID); //added the insurance id into collection by pankaj 20110113

                                tvInsurances.SelectedNode.Remove();
                                //}
                                //_NodeRemoved = true;
                                //SHUBHANGI 20100708 SET FOCUS ON THE FIRST RECORD
                                tvInsurances.SelectedNode = tvInsurances.Nodes[0];


                                //if (dtScannedImages.Rows.Count > 0)
                                //{

                                //    foreach (DataRow dr in dtScannedImages.Rows)
                                //    {

                                //        if (dr["ID"] != null)
                                //        {
                                //            if (Convert.ToInt64(dr["ID"]) == oIns.ContactID)
                                //            {

                                //                dr.Delete();
                                //            }
                                //        }


                                //    }
                                //    dtScannedImages.AcceptChanges();


                                //}


                            }
                            else
                            {

                                //MessageBox.Show("Can not remove this insurance. Insurance is used for billing.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show(" Cannot remove this insurance.   This insurance has been used for insurance billing.  You may inactivate this insurance, instead.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

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
                if (oPatient != null) { oPatient.Dispose(); }
            }
        }


        private void ClearInsuranceInfo()
        {
            txtInsurance.Text = "";
            txtSubFName.Text = "";
            txtSubMName.Text = "";
            txtSubLName.Text = "";
            txtCompanyName.Text = "";
            txtInsurance.Tag = null;
            txtSubscriberID.Text = "";
            txtSubscriberPolicy.Text = "";
            txtGroup.Text = "";
            txtEmployer.Text = "";
            txtEmployer.Text = "";
            //txtAddress1.Text = "";
            //txtAddress2.Text = "";
            //cmbState.Text = "";
            //txtCity.Text = "";
            //txtZip.Text ="";
            //txtCounty.Text = "";
            //cmbCountry.Text = "";

            //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 

            oAddresscontrol.txtAddress1.Text = "";
            oAddresscontrol.txtAddress2.Text = "";
            oAddresscontrol.isFormLoading = true;
            oAddresscontrol.txtZip.Text = "";
            oAddresscontrol.isFormLoading = false;
            oAddresscontrol.txtCity.Text = "";
            oAddresscontrol.cmbCountry.Text = "";
            oAddresscontrol.cmbState.Text = "";
            oAddresscontrol.txtCounty.Text = "";

            mskInsurancePhone.Text = "";
            mtxtDOB.Text = "";

            rbGender1.Checked = false;
            rbGender2.Checked = false;
            rbGender3.Checked = false;

            radSetAsPrimary.Checked = false;
            radSetAsSecondary.Checked = false;
            radSetAsTertiary.Checked = false;
            rbInactive.Checked = true;
            chkSameAsPatient.Checked = false;
            //
            mtxtStartDate.Text = "";
            mtxtEndDate.Text = "";
            if (_isInsuranceSubscriberMandatory)
            {
                cmbRelationShip.Text = "Self";
                cmbRelationShip_SelectedIndexChanged(null, null);
            }
            else
            {
                cmbRelationShip.Text = "";
            }
            txtDeductableAmount.Text = "";
            txtCoveragePercent.Text = "";
            txtCopay.Text = "";
            chkAssignmentofBenifit.Checked = true;
            //chkworkerscomp.Checked = false;
            //chkautoclaim.Checked = false;
            cmbDefaultTypeCode.SelectedIndex = 0;
            cmbMedicareTypeCode.SelectedIndex = 0;
            cmbMedicareTypeCode.Enabled = false;
            txtEligibilityInsurance.Text = "";           
            chkCompany.Checked = false;           
           // chkCompany.Visible = true; 
            chkworkerscomp.Checked = false; 
            if (_isWorkerCompenable)
            {
               // chkworkerscomp.Checked = true;
                chkworkerscomp.Visible = true;
            }
            else
            {
               
                chkworkerscomp.Visible = false;
            }
            lblGroupMandatory.Visible = false;
        }

        private bool IsModified(Insurance oInsurance)
        {
            bool _Result = true;
            try
            {

                if (!IsValidDate(mtxtDOB.Text))
                {
                    return false;
                }

                // mtxtDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                //if (!IsValidDate(mtxtDOB.Text))
                //{
                //    return false;
                //}
                //mtxtEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                //if (!IsValidDate(mtxtEndDate.Text))
                //{
                //    return false;
                //}

                //Check if insurance details modified
                //if (txtInsurance.Text == oInsurance.InsuranceName.ToString()
                //    && txtSubFName.Text == oInsurance.SubscriberFName.ToString()
                //    && txtSubMName.Text == oInsurance.SubscriberMName.ToString()
                //   && txtSubLName.Text == oInsurance.SubscriberLName.ToString()
                //    && txtSubscriberID.Text == oInsurance.SubscriberID.ToString()
                //    && txtSubscriberPolicy.Text == oInsurance.SubscriberPolicy.ToString()
                //    && txtGroup.Text == oInsurance.Group.ToString()
                //    && txtEmployer.Text == oInsurance.Employer.ToString()
                //    && txtAddress1.Text == oInsurance.SubscriberAddr1.ToString()
                //    && txtAddress2.Text == oInsurance.SubscriberAddr2.ToString()
                //    && cmbState.Text == oInsurance.SubscriberState.ToString()
                //    && txtCity.Text == oInsurance.SubscriberCity.ToString()
                //    && txtZip.Text == oInsurance.SubscriberZip.ToString()
                //    && mskInsurancePhone.Text == oInsurance.Phone.ToString()
                //    && cmbRelationShip.Text == oInsurance.RelationshipName.ToString()
                //    && txtDeductableAmount.Text == oInsurance.DeductableAmount.ToString()
                //    && txtCoveragePercent.Text == oInsurance.CoveragePercent.ToString()
                //    && txtCopay.Text == oInsurance.CoPay.ToString()
                //    && chkAssignmentofBenifit.Checked == oInsurance.AssignmentofBenefit
                //    && chkworkerscomp.Checked == oInsurance.Isworkerscomp
                //    && chkautoclaim.Checked == oInsurance.Isautoclaim
                //   )

                if (oAddresscontrol.txtAddress1.Text == oInsurance.SubscriberAddr1.ToString()
                    && oAddresscontrol.txtAddress2.Text == oInsurance.SubscriberAddr2.ToString()
                    && oAddresscontrol.txtZip.Text == oInsurance.SubscriberZip.ToString()
                    && oAddresscontrol.txtCity.Text == oInsurance.SubscriberCity.ToString()
                    && oAddresscontrol.cmbCountry.Text == oInsurance.SubscriberCountry.ToString()
                    && oAddresscontrol.cmbState.Text == oInsurance.SubscriberState.ToString()
                    && oAddresscontrol.txtCounty.Text == oInsurance.SubscriberCounty.ToString()
                    && txtInsurance.Text == oInsurance.InsuranceName.ToString()
                    && txtSubFName.Text == oInsurance.SubscriberFName.ToString()
                    && txtSubMName.Text == oInsurance.SubscriberMName.ToString()
                    && txtSubLName.Text == oInsurance.SubscriberLName.ToString()
                    && txtSubscriberID.Text == oInsurance.SubscriberID.ToString()
                    //&& txtSubscriberPolicy.Text == oInsurance.SubscriberPolicy.ToString()
                    && txtGroup.Text == oInsurance.Group.ToString()
                    && txtEmployer.Text == oInsurance.Employer.ToString()
                    && mskInsurancePhone.Text == oInsurance.Phone.ToString()
                    && cmbRelationShip.Text == oInsurance.RelationshipName.ToString()
                    && txtDeductableAmount.Text == oInsurance.DeductableAmount.ToString()
                    && txtCoveragePercent.Text == oInsurance.CoveragePercent.ToString()
                    && txtCopay.Text == oInsurance.CoPay.ToString()
                    && chkAssignmentofBenifit.Checked == oInsurance.AssignmentofBenefit
                    && chkworkerscomp.Checked == oInsurance.Isworkerscomp
                    && chkautoclaim.Checked == oInsurance.Isautoclaim
                    && txtEligibilityInsurance.Text == oInsurance.sEligibiltyInsuranceNotes.ToString()
                     && chkCompany.Checked == oInsurance.IsCompnay
                     && txtCompanyName.Text == oInsurance.SubscriberCompanyLName)
                {

                    //  && (mtxtDOB.Text == oInsurance.DOB.ToString("MM/dd/yyyy") && oInsurance.IsNotDOB == false)  
                    //&& (mtxtStartDate.Text == oInsurance.StartDate.ToString("MM/dd/yyyy") && oInsurance.IsNotStartDate == false)
                    //&& (mtxtEndDate.Text == oInsurance.EndDate.ToString("MM/dd/yyyy") && oInsurance.IsNotEndDate == false)  

                    //Check if subscriber gender modified
                    if ((oInsurance.SubscriberGender == "Male" && rbGender1.Checked == true)
                         || (oInsurance.SubscriberGender == "Female" && rbGender2.Checked == true)
                         || (oInsurance.SubscriberGender == "Other" && rbGender3.Checked == true)
                       )
                    {
                        //Check if InsuranceFlag  modified
                        if ((oInsurance.InsuranceFlag == Insurance.InsuranceTypeFlag.Primary.GetHashCode() && radSetAsPrimary.Checked == true)
                             || (oInsurance.InsuranceFlag == Insurance.InsuranceTypeFlag.Secondary.GetHashCode() && radSetAsSecondary.Checked == true)
                             || (oInsurance.InsuranceFlag == Insurance.InsuranceTypeFlag.Tertiary.GetHashCode() && radSetAsTertiary.Checked == true)
                             || (oInsurance.InsuranceFlag == Insurance.InsuranceTypeFlag.None.GetHashCode() && rbInactive.Checked == true)
                            )
                        {
                            //  Convert.ToDateTime(mtxtPADOB.Text).Date == oPatientDemo.PatientDOB.Date

                            bool _datesModified = false;
                            //Check if dates are modified
                            if (oInsurance.IsNotDOB == false && mtxtDOB.Text != oInsurance.DOB.ToString("MM/dd/yyyy"))
                                _datesModified = true;
                            if (oInsurance.IsNotStartDate == false && mtxtStartDate.Text != oInsurance.StartDate.ToString("MM/dd/yyyy"))
                                _datesModified = true;
                            if (oInsurance.IsNotEndDate == false && mtxtEndDate.Text != oInsurance.EndDate.ToString("MM/dd/yyyy"))
                                _datesModified = true;

                            if (_datesModified == false)
                            {
                                _Result = false;
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
            return _Result;
        }

        public void AssignUserRights()
        {
            gloUserRights.ClsgloUserRights ObjgloUserRights = null;
            try
            {
               
                ObjgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                if (ObjgloUserRights != null)
                {
                    ObjgloUserRights.CheckForUserRights(_UserName);
                    btn_AddInsurance.Visible = ObjgloUserRights.Contacts_Insurance;
                    btnModifyInsurance.Visible = ObjgloUserRights.Contacts_Insurance;
                }
            }
            catch (Exception) // Ex)
            {
                //Ex.ToString();
                //Ex = null;
            }
            finally
            {
                if (ObjgloUserRights != null)
                {
                    ObjgloUserRights.Dispose();
                    ObjgloUserRights = null;
                }
            }

        }

        #endregion "Public Methods GetData & SetData"

        #region "Events"

        //Addding the Selected Insurance Information in the Tree View to respective controls.
        private void tvInsurances_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            tvInsurances.Select();
           // lblGroupMandatory.Visible = false;
            try
            {
                //SHUBHANGI 20100712
                _isCallFromAddInsurance = false;
                if (txtInsurance.Text.Trim() != "")
                {
                    _NodeChanged = false;
                    if (AddInsurance() == false)
                    {
                        //code to Temporary  storage for 
                        if (this.ActiveControl != null)
                            oFocusControl = this.ActiveControl;

                        if (_NodeChanged == false)
                            e.Cancel = true;

                    }
                }
            }
            catch { }
            finally
            {
                //if control get focused before node selected of treeview then activite timer
                if (oFocusControl != null)
                {
                    oTimer = new Timer();
                    oTimer.Interval = 20;
                    oTimer.Enabled = true;
                    oTimer.Tick += new System.EventHandler(this.oTimer_Tick);
                    

                }
            }            
        }
        private void oTimer_Tick(object sender, EventArgs e)
        {
            if (oFocusControl != null)
            {
                oFocusControl.Focus();
                oFocusControl = null;
                if (oTimer != null)
                {
                    oTimer.Tick -= new System.EventHandler(this.oTimer_Tick);
                    oTimer.Dispose();
                    oTimer = null;
                }
            }

        }


        private void tvInsurances_AfterSelect(object sender, TreeViewEventArgs e)
        {
            chkSameAsPatient.Checked = false;
            chkAddrSameAsPatient.Checked = false;
            _isAddNewInsurance = false;           
            
            //Set the insurance info for selected Insurance.
            if (e.Node.Level != 0)
            {
                //Casting the TreeView insurance object.
                Insurance ins = (Insurance)tvInsurances.SelectedNode.Tag;
                txtInsurance.Text = ins.InsuranceName.ToString();
                //SHUBHANGI
                txtInsurance.Tag = Convert.ToInt64(ins.ContactID);
                //txtInsurance.Tag = Convert.ToInt64(tvInsurances.Nodes[0].Index);
                txtSubscriberID.Text = ins.SubscriberID.ToString();
                txtSubscriberPolicy.Text = ins.SubscriberPolicy.ToString();
                txtGroup.Text = ins.Group.ToString();
                txtEmployer.Text = ins.Employer.ToString();
                radSetAsPrimary.Checked = ins.PrimaryFlag;

                if (ins.IsAddressSameAsPatient)
                {
                   
                    chkAddrSameAsPatient.Checked = true;
                }
                else
                {
                   
                    oAddresscontrol.txtAddress1.Text = ins.SubscriberAddr1.ToString();
                    oAddresscontrol.txtAddress2.Text = ins.SubscriberAddr2.ToString();
                    oAddresscontrol.cmbState.Text = ins.SubscriberState.ToString();
                    oAddresscontrol.txtCity.Text = ins.SubscriberCity.ToString();
                    oAddresscontrol._isTextBoxLoading = true;
                    oAddresscontrol.txtZip.Text = ins.SubscriberZip.ToString();
                    oAddresscontrol._isTextBoxLoading = false;
                    oAddresscontrol.txtCounty.Text = ins.SubscriberCounty.Trim();
                    oAddresscontrol.cmbCountry.Text = ins.SubscriberCountry.Trim();

                }
                if (ins.IsSameAsPatient)
                {
                    _isSubscriberDataChanged = false;
                    chkSameAsPatient.Checked = true;
                }
                else
                {
                   
                    txtSubFName.Text = ins.SubscriberFName.ToString();
                    txtSubMName.Text = ins.SubscriberMName.ToString();
                    txtSubLName.Text = ins.SubscriberLName.ToString();
                    txtCompanyName.Text = ins.SubscriberCompanyLName.ToString();
                     if (ins.RelationshipName.ToString().ToLower() == "Self".ToLower())
                    {
                        _isSubscriberDataChanged = true;
                    }
                    else 
                    {
                        _isSubscriberDataChanged = false;
                    }
                    //Code added on 20081030
                    //txtAddress1.Text = ins.SubscriberAddr1.ToString();
                    //txtAddress2.Text = ins.SubscriberAddr2.ToString();
                    //cmbState.Text = ins.SubscriberState.ToString();
                    //txtCity.Text = ins.SubscriberCity.ToString();
                    //txtZip.Text = ins.SubscriberZip.ToString();
                    //txtCounty.Text  = ins.SubscriberCounty.Trim();
                    //cmbCountry.Text = ins.SubscriberCountry.Trim();    

                    //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 
                    //if(IsInsuranceDetailsSameAsPatient(ins.ContactID,ins.PatientID))



                    //
                    rbGender1.Checked = false;
                    rbGender2.Checked = false;
                    rbGender3.Checked = false;

                    if (ins.SubscriberGender == "Male")
                        rbGender1.Checked = true;
                    else if (ins.SubscriberGender == "Female")
                        rbGender2.Checked = true;
                    else if (ins.SubscriberGender == "Other")
                        rbGender3.Checked = true;


                    mskInsurancePhone.Text = ins.Phone.ToString();
                    if (ins.IsNotDOB == false)
                    {
                        mtxtDOB.Text = ins.DOB.ToString("MM/dd/yyyy");
                    }
                    if (ins.RelationshipName.ToString() == "")
                    {
                        if (_isInsuranceSubscriberMandatory)
                        {
                            if ((txtSubFName.Text.Trim() != "") ||
                               (txtSubLName.Text.Trim() != "") ||
                               (txtSubMName.Text.Trim() != "") ||
                               (mtxtDOB.Text.Replace("/", "").Trim() != "") ||
                               ((rbGender1.Checked == true) || (rbGender2.Checked == true) || (rbGender3.Checked == true)) ||
                               (txtSubscriberID.Text.Trim() != "") ||
                               (oAddresscontrol.txtAddress1.Text.Trim() != "") ||
                               (oAddresscontrol.txtAddress2.Text.Trim() != "") ||
                               (oAddresscontrol.txtCity.Text.Trim() != "") ||
                               (oAddresscontrol.txtCounty.Text.Trim() != "") ||
                               (oAddresscontrol.txtZip.Text.Trim() != "") ||
                               (oAddresscontrol.cmbState.Text.Trim() != "") ||
                               (txtGroup.Text.Trim() != "") ||
                               (mskInsurancePhone.Text.Trim() != "") ||
                               (chkAddrSameAsPatient.Checked == true)
                                )
                            {
                                cmbRelationShip.Text = "Unknown";
                            }
                            else
                            {
                                cmbRelationShip.Text = "Self";

                            }
                            cmbRelationShip_SelectedIndexChanged(null, null);
                        }
                    }
                    else
                    {
                        if (cmbRelationShip.FindStringExact(ins.RelationshipName.ToString()) != -1)
                        {
                            cmbRelationShip.Text = ins.RelationshipName.ToString();
                        }
                        else
                        {
                            cmbRelationShip.Text = "Unknown";
                        }
                        cmbRelationShip_SelectedIndexChanged(null, null);
                    }
                }




                if (ins.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.Primary)
                {
                    radSetAsPrimary.Checked = true;
                }
                else if (ins.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.Secondary)
                {
                    radSetAsSecondary.Checked = true;
                }
                else if (ins.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.Tertiary)
                {
                    radSetAsTertiary.Checked = true;
                }
                else if (ins.InsuranceFlag == (Int64)Insurance.InsuranceTypeFlag.None)
                {
                    rbInactive.Checked = true;
                }



                if (ins.IsNotStartDate == false)
                {
                    mtxtStartDate.Text = ins.StartDate.ToString("MM/dd/yyyy");
                }

                if (ins.IsNotEndDate == false)
                {
                    mtxtEndDate.Text = ins.EndDate.ToString("MM/dd/yyyy");
                }



                txtDeductableAmount.Text = ins.DeductableAmount.ToString();
                txtCoveragePercent.Text = ins.CoveragePercent.ToString();
                txtEligibilityInsurance.Text = ins.sEligibiltyInsuranceNotes.ToString();
                txtCoveragePercent.Text = (Convert.ToDecimal(txtCoveragePercent.Text.Trim())).ToString("#0.00");
                if (ins.CoveragePercent == Convert.ToInt32(ins.CoveragePercent))
                {
                    txtCoveragePercent.Text = (Convert.ToDecimal(txtCoveragePercent.Text.Trim())).ToString("#0");
                }


                txtCopay.Text = ins.CoPay.ToString();
                chkAssignmentofBenifit.Checked = ins.AssignmentofBenefit;



                gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                //Added By MaheshB
                if (_isWorkerCompenable&&ogloPatient.IsWorkerComp(Convert.ToInt64(txtInsurance.Tag)))
                {
                    chkworkerscomp.Checked = ins.Isworkerscomp;
                    chkworkerscomp.Visible = true;
                    chkCompany.Checked = ins.IsCompnay;
                    if (cmbRelationShip.Text.ToString() != "Self")
                        chkCompany.Visible = true;
                    else
                        chkCompany.Visible = false; 
                }
                else
                {
                    chkCompany.Visible = false; 
                    chkworkerscomp.Visible = false;
                    chkworkerscomp.Checked = ins.Isworkerscomp;
                    chkCompany.Checked = ins.IsCompnay;
                }
                ogloPatient.Dispose();
                ogloPatient = null;
                chkautoclaim.Checked = ins.Isautoclaim;
                cmbDefaultTypeCode.SelectedValue = ins.InsTypeCodeDefault;
                cmbMedicareTypeCode.SelectedValue = ins.InsTypeCodeMedicare;
              
                //...Code changes done by Sagar Ghodke on 20100330
                //..Code changes done to consider "MA" as Medicare
                string _InsType = string.Empty;
                _InsType = GetInsuranceType(Convert.ToInt64(txtInsurance.Tag)).Trim().ToUpper();

                if (_InsType == "MB" || _InsType == "MA")
                {
                    cmbMedicareTypeCode.Enabled = true;
                }
                else
                {
                    cmbMedicareTypeCode.Enabled = false;
                    cmbMedicareTypeCode.SelectedIndex = 0;
                }
                //cmbDefaultTypeCode.Text = ins.InsTypeDescriptionDefault;
                //cmbMedicareTypeCode.Text = ins.InsTypeDescriptionMedicare;
                if (bIsGroupMandatory(Convert.ToInt64(txtInsurance.Tag)) == true)
                {
                    lblGroupMandatory.Visible = true;
                }
                else
                {
                    lblGroupMandatory.Visible = false;
                }
            }

            else
            {

                ClearInsuranceInfo();
                //chkSameAsPatient.Checked = true ;
                //chkAddrSameAsPatient.Checked = true; 
            }            
        }


        private void btnClsInsuInfo_Click(object sender, EventArgs e)
        {
            onInsuranceClose_Clicked(sender, e);
        }


        private void chkSameAsPatient_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkSameAsPatient.Checked == true )
            {
                if ((_PatientID != 0 && _isSubscriberDataChanged == true))
                {
                    return;
                }
                chkAddrSameAsPatient.Checked = true;
                chkAddrSameAsPatient_CheckedChanged(null, null);

                txtSubFName.Text = _PatientFName;
                txtSubMName.Text = _PatientMName;
                txtSubLName.Text = _PatientLName;
                txtEmployer.Text = _Employer;
                // _PatientDOB = _PatientDOB.Replace("/", "");
                if (_PatientDOB.Replace("/", "") != "")

                //if (_PatientDOB.Trim() != "")
                {
                    // mtxtDOB.Text = Convert.ToDateTime(_PatientDOB).ToString("MM/dd/yyyy");
                    mtxtDOB.Text = _PatientDOB;
                }

                mskInsurancePhone.Text = _PatientPhone;

                if (_PatientGender == "Male")
                    rbGender1.Checked = true;
                else if (_PatientGender.ToString() == "Female")
                    rbGender2.Checked = true;
                else
                    rbGender3.Checked = true;

                cmbRelationShip.SelectedIndex = cmbRelationShip.FindStringExact("Self");
                //EnableControls(false);
            }
            else
            {
                if (_oCardscan != true) //For card Scan Data
                {
                    txtSubFName.Text = "";
                    txtSubMName.Text = "";
                    txtSubLName.Text = "";
                }
                mtxtDOB.Text = "";
                mskInsurancePhone.Text = "";
                chkAddrSameAsPatient.Checked = false;
                chkAddrSameAsPatient_CheckedChanged(null, null);
                txtEmployer.Text = "";
                //cmbRelationShip.SelectedIndex = cmbRelationShip.FindStringExact(" ");
                EnableControls(true);
            }
        }

        private void chkAddrSameAsPatient_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddrSameAsPatient.Checked == true)
            {
                ////Code added on 20081030
                //txtAddress1.Text = PatientAddressLine1;
                //txtAddress2.Text = PatientAddressLine2;
                //cmbState.Text = PatientState;
                //txtCity.Text = PatientCity;
                //txtZip.Text = PatientZip;
                //txtCounty.Text = PatientCounty;
                //cmbCountry.Text = PatientCountry;

                //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 

                oAddresscontrol.txtAddress1.Text = PatientAddressLine1;
                oAddresscontrol.txtAddress2.Text = PatientAddressLine2;
                oAddresscontrol.isFormLoading = true;
                oAddresscontrol.txtZip.Text = PatientZip;
                oAddresscontrol.isFormLoading = false;
                oAddresscontrol.txtCity.Text = PatientCity;
                oAddresscontrol.cmbCountry.Text = PatientCountry;
                oAddresscontrol.cmbState.Text = PatientState;
                oAddresscontrol.txtCounty.Text = PatientCounty;

                oAddresscontrol.txtAddress1.Enabled = false;
                oAddresscontrol.txtAddress2.Enabled = false;
                oAddresscontrol.txtCity.Enabled = false;
                oAddresscontrol.txtCounty.Enabled = false;
                oAddresscontrol.txtZip.Enabled = false;
                oAddresscontrol.cmbCountry.Enabled = false;
                oAddresscontrol.cmbState.Enabled = false;

                // 
            }
            else
            {
                //txtAddress1.Text = "";
                //txtAddress2.Text = "";
                //cmbState.Text = "";
                //txtCity.Text = "";
                //txtZip.Text = "";
                //txtCounty.Text = "";
                //cmbCountry.Text = "";  
                //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 
                oAddresscontrol.txtAddress1.Text = "";
                oAddresscontrol.txtAddress2.Text = "";
                oAddresscontrol.isFormLoading = true;
                oAddresscontrol.txtZip.Text = "";
                oAddresscontrol.isFormLoading = false;
                oAddresscontrol.txtCity.Text = "";
                oAddresscontrol.cmbCountry.Text = _Country;  
                oAddresscontrol.cmbState.Text = "";
                oAddresscontrol.txtCounty.Text = "";

                oAddresscontrol.txtAddress1.Enabled = true;
                oAddresscontrol.txtAddress2.Enabled = true;
                oAddresscontrol.txtCity.Enabled = true;
                oAddresscontrol.txtCounty.Enabled = true;
                oAddresscontrol.txtZip.Enabled = true;
                oAddresscontrol.cmbCountry.Enabled = true;
                oAddresscontrol.cmbState.Enabled = true;
            }





        }


        private void cmbRelationShip_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmbRelationShip.Text.ToLower() == "Self".ToLower())
            {

                if (chkSameAsPatient.Checked == false && _isSubscriberDataChanged == false)
                {
                    chkSameAsPatient.Checked = true;
                }
                //chkSameAsPatient_CheckedChanged(null, null);
                chkCompany.Visible = false; 

            }
            else
            {

                chkSameAsPatient.Checked = false;
                gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                if (txtInsurance.Tag != null && ogloPatient.IsWorkerComp(Convert.ToInt64(txtInsurance.Tag)) && _isWorkerCompenable)
                {
                    chkCompany.Visible = true;
                }
                else
                {
                    chkCompany.Visible = false;
                }
                ogloPatient.Dispose();
                ogloPatient = null;

            }
            if (cmbRelationShip.Text.ToLower() == "Employee".ToLower())
            {
                chkSameAsPatient.Checked = false;
                gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                if (txtInsurance.Tag != null && ogloPatient.IsWorkerComp(Convert.ToInt64(txtInsurance.Tag)))
                {
                    chkCompany.Checked = false;
                    chkCompany.Checked = true;
                }
                ogloPatient.Dispose();
                ogloPatient = null;
            }
            else
                chkCompany.Checked = false;

            //else if (cmbRelationShip.Text == "Mother")
            //{
            //        rbGender1.Checked = false;
            //        rbGender2.Checked = true;
            //        rbGender3.Checked = false;
            //        chkSameAsPatient.Checked = false;
            //        chkSameAsPatient_CheckedChanged(null, null);  
            //}
            //else if (cmbRelationShip.Text == "Father")
            //{
            //    rbGender1.Checked = true;
            //    rbGender2.Checked = false;
            //    rbGender3.Checked = false;
            //    chkSameAsPatient.Checked = false;
            //    chkSameAsPatient_CheckedChanged(null, null);  
            //}
            //else
            //{
            //    chkSameAsPatient.Checked = false;
            //    chkSameAsPatient_CheckedChanged(null, null);  
            //}
        }

        private void txtZip_Leave(object sender, EventArgs e)
        {
            if (txtZip.Text.Trim() != "" && Regex.IsMatch(txtZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = new System.Data.DataTable();
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = " + txtZip.Text.Trim() + "";

                    cmbState.Text = "";
                    //txtCity.Text = "";
                    // txtPACountry.Text = "";

                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbState.Text = Convert.ToString(dt.Rows[0]["ST"]);
                        if (txtCity.Text.Trim() == "")
                            txtCity.Text = dt.Rows[0]["City"].ToString();

                        cmbCountry.Text = "US";
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
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                    if (oDb != null)
                    {
                        oDb.Disconnect();
                        oDb.Dispose();
                    }
                }
            }
            else
            {
                txtCity.Text = "";
                cmbState.Text = "";
            }
        }

        private void txtCoveragePercent_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
            else if (!(e.KeyChar == Convert.ToChar(8))) //if not backspsace
            {
                if (txtCoveragePercent.SelectionStart > txtCoveragePercent.Text.IndexOf("."))
                {
                    if (txtCoveragePercent.Text.Contains(".") == true)
                    {
                        if (txtCoveragePercent.Text.Substring(txtCoveragePercent.Text.IndexOf(".") + 1, txtCoveragePercent.Text.Length - (txtCoveragePercent.Text.IndexOf(".") + 1)).Length == 2 && txtCoveragePercent.SelectedText == "")
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if (txtCoveragePercent.Text.Length >= 3 && txtCoveragePercent.SelectedText == "")
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            if (e.KeyChar == Convert.ToChar(46) && txtCoveragePercent.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtCopay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{
            //    e.Handled = true;
            //}
            if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                {
                    e.Handled = true;
                }
                else
                {
                    if (e.KeyChar != Convert.ToChar(46) && txtCopay.Text.Length >= 3 && txtCopay.SelectedText == "")
                    {
                        if (txtCopay.Text.Contains(".") == false)
                            e.Handled = true;

                    }
                }
            }
            else
                if (txtCopay.Text.Contains(".") && e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                }

            //if (e.KeyChar == Convert.ToChar(13))
            //{
            //    CalculateRemainingAmount();
            //    //txtCheckAmount.Select();
            //    //txtCheckAmount.Focus();
            //}
        }

        private void txtSubscriberID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (System.Text.RegularExpressions.Regex.IsMatch(txtSubscriberID.Text.Trim(),@"^\d{9}[a-zA-Z]{1}$") == false )
                if (e.KeyChar != Convert.ToChar(8))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(Convert.ToString(e.KeyChar), @"[0-9a-zA-Z]") == false)
                        e.Handled = true;
                }
                if (bIsCapitalizeInsID == true)
                {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                }
                
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void txtDeductableAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{
            //    e.Handled = true;
            //}
            if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                {
                    e.Handled = true;
                }
                else
                {
                    if (e.KeyChar != Convert.ToChar(46) && txtDeductableAmount.Text.Length >= 5 && txtDeductableAmount.SelectedText == "")
                    {
                        if (txtDeductableAmount.Text.Contains(".") == false)
                            e.Handled = true;

                    }
                }
            }
            else
                if (txtDeductableAmount.Text.Contains(".") && e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                }
        }


        private void mtxtDOB_Validating(object sender, CancelEventArgs e)
        {
            mtxtDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mtxtDOB.Text.Length > 0 && mtxtDOB.MaskCompleted == false)
            {

                if (!ValidDate(mtxtDOB.Text))
                {
                    MessageBox.Show("Please enter valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isValidated = false;

                    e.Cancel = true;

                }

            }
            else
            {
                if (mtxtDOB.MaskCompleted == true)
                {
                    try
                    {
                        mtxtDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        DateTime dtTemp = Convert.ToDateTime(mtxtDOB.Text);
                        if (Convert.ToDateTime(mtxtDOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtDOB.Text.Trim()) >= DateTime.Now || Convert.ToDateTime(mtxtDOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                        {
                            MessageBox.Show("Please enter valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidated = false;
                            e.Cancel = true;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        MessageBox.Show("Please enter valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isValidated = false;
                        e.Cancel = true;
                        //ex.ToString();
                        //ex = null;
                    }
                }
            }


        }

        //**********************
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
                        if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
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


        public static bool ValidDate(string datevalue)
        {
            bool _result = false;
            try
            {

                DateTime _dt = Convert.ToDateTime(datevalue);
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            return _result;
        }

        ////Validating for leap year..
        //   mtxtPADOB.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //   if (!IsValidDate(mtxtPADOB.Text))
        //   {
        //    MessageBox.Show("Please enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //       mtxtPADOB.Focus();
        //       return false;
        //   }

        private void mtxtStartDate_Validating(object sender, CancelEventArgs e)
        {
            mtxtStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (mtxtStartDate.Text.Length > 0 && mtxtStartDate.MaskCompleted == false)
            {
                if (!ValidDate(mtxtStartDate.Text))
                {
                    _isValidated = false;
                    MessageBox.Show("Please enter valid start date. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    e.Cancel = true;
                }

            }
            else
            {
                if (mtxtStartDate.MaskCompleted == true)
                {
                    try
                    {
                        mtxtStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                        DateTime dtTemp = Convert.ToDateTime(mtxtStartDate.Text);
                        // solving sales force case - 0009923
                        //if (Convert.ToDateTime(mtxtStartDate.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtStartDate.Text.Trim()) >= DateTime.Now || Convert.ToDateTime(mtxtStartDate.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                        if (Convert.ToDateTime(mtxtStartDate.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtStartDate.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                        {
                            _isValidated = false;
                            MessageBox.Show("Please enter valid start date. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            e.Cancel = true;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        _isValidated = false;
                        MessageBox.Show("Please enter valid start date. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                        //ex.ToString();
                        //ex = null;
                    }
                }
            }


        }

        private void mtxtEndDate_Validating(object sender, CancelEventArgs e)
        {
            mtxtEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (mtxtEndDate.Text.Length > 0 && mtxtEndDate.MaskCompleted == false)
            {
                mtxtEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (!ValidDate(mtxtEndDate.Text))
                {
                    MessageBox.Show("Please enter valid end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isValidated = false;
                    e.Cancel = true;
                }
            }
            else
            {
                if (mtxtEndDate.MaskCompleted == true)
                {
                    try
                    {
                        mtxtEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                        DateTime dtTemp = Convert.ToDateTime(mtxtEndDate.Text);
                        if (Convert.ToDateTime(mtxtEndDate.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtEndDate.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                        {
                            MessageBox.Show("Please enter valid end date ..  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidated = false;
                            mtxtEndDate.Focus();
                            e.Cancel = true;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        MessageBox.Show("Please enter valid end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isValidated = false;
                        mtxtEndDate.Focus();
                        e.Cancel = true;
                        //ex.ToString();
                        //ex = null;
                    }
                }
            }

        }


        // private void mtxtEndDate_Validating(object sender, CancelEventArgs e)
        // {
        //   mtxtEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        //   mtxtStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;



        //    if (mtxtEndDate.Text.Length > 0 && mtxtStartDate.Text.Length > 0 )  //&& Convert.ToDateTime(mtxtStartDate).MaskCompleted == false && Convert.ToDateTime(mtxtEndDate.Text.Trim()).MaskCompleted == false)
        //    {
        //        if (mtxtEndDate.Text.Trim() < mtxtStartDate.Text.Trim())

        //        {
        //            MessageBox.Show("Please enter valid end date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            mtxtEndDate.Focus();
        //            e.Cancel = true;
        //        }
        //    }


        //}

        private void btnInsBrowse_Click(object sender, EventArgs e)
        {
            /// 

            if (_IsSaveAsCopy == false)
            {
                if (IsSelecedInsuranceUsed() == true)
                {
                    MessageBox.Show("Cannot change this insurance plan.   This insurance has been used for insurance billing.   You may inactivate this insurance and add a new one.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


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
                try
                {

                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Insurance, false, this.Width);
            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = "Insurance Plans";

            //_CurrentControlType = gloListControl.gloListControlType.Insurance;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

            this.Controls.Add(oListControl);

            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
            //btnInsBrowse.Focus();

        }



        private void btnInsDelete_Click(object sender, EventArgs e)
        {
            //pnlOtherDetails.Visible = true;
            //pnlMyInsurances.Visible = true;

            if (_IsSaveAsCopy == false)
            {
                if (IsSelecedInsuranceUsed() == true)
                {
                    MessageBox.Show("Cannot remove this insurance.   This insurance has been used for insurance billing.  You may inactivate this insurance, instead.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            txtInsurance.Text = "";
            txtInsurance.Tag = null;
            lblGroupMandatory.Visible = false;
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
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
                try
                {

                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }
        }

        private void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                txtInsurance.Text = "";
                txtInsurance.Tag = null;
                
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        // nContactID, sName, sContact
                        //Int64 currentindex = 0;
                        //    currentindex = tvInsurances.Nodes[0].Nodes.Count;
                        txtInsurance.Tag = oListControl.SelectedItems[i].ID;
                        // txtInsurance.Tag = oListControl.SelectedItems[i].ID + "|" + currentindex; //nContactID

                        //oListControl.SelectedItems[_Counter].Code ; //nName

                        txtInsurance.Text = oListControl.SelectedItems[i].Description; //nContact
                        _isAddNewInsurance = true; 

                    }
                    gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                    if (_isWorkerCompenable&&ogloPatient.IsWorkerComp(Convert.ToInt64(txtInsurance.Tag)))
                    {
                        chkworkerscomp.Checked = true;
                        chkworkerscomp.Visible = true;
                        if (cmbRelationShip.Text.ToString() == "Self")
                        {
                            chkCompany.Visible = false;
                            chkCompany.Checked = false; 
                        }
                        else
                        {
                            chkCompany.Visible = true;
                            chkCompany.Checked = true;
                        }

                    }
                    else
                    {
                        chkworkerscomp.Checked = false;
                        chkworkerscomp.Visible = false; 
                    }
                    ogloPatient.Dispose();
                    ogloPatient = null;
                    GetInsuranceFlag();
                    //string[] insurnaceArray = null;
                    //insurnaceArray = Convert.ToString(txtInsurance.Tag).Split('|');
                    //Int64 insContactid;
                    //insContactid = Convert.ToInt64(insurnaceArray[0]);

                    string _InsType = string.Empty;
                    _InsType = GetInsuranceType(Convert.ToInt64(txtInsurance.Tag)).Trim().ToUpper();
                    //  _InsType = GetInsuranceType(insContactid).Trim().ToUpper();
                    //if (GetInsuranceType(Convert.ToInt64(txtInsurance.Tag)).Trim().ToUpper() == "MB")
                    if (_InsType == "MB" || _InsType == "MA")
                    {
                        cmbMedicareTypeCode.Enabled = true;
                    }
                    else
                    {
                        cmbMedicareTypeCode.Enabled = false;
                        cmbMedicareTypeCode.SelectedIndex = 0;
                    }
                    

                }
                if (bIsGroupMandatory(Convert.ToInt64(txtInsurance.Tag)))
                {
                    lblGroupMandatory.Visible = true;
                }
                else
                {
                    lblGroupMandatory.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void GetInsuranceFlag()
        {
            bool IsPrimary = false;
            bool IsSecondary = false;
            bool IsTertiary = false;
            string strInsuranceTypeCode = "";
            try
            {


                Insurance oInsurance = null;
                foreach (TreeNode oNode in tvInsurances.Nodes[0].Nodes)
                {
                    oInsurance = oNode.Tag as Insurance;
                    if (oInsurance != null)
                    {
                        switch ((Insurance.InsuranceTypeFlag)oInsurance.InsuranceFlag)
                        {
                            case Insurance.InsuranceTypeFlag.Primary:
                                IsPrimary = true;
                                break;
                            case Insurance.InsuranceTypeFlag.Secondary:
                                IsSecondary = true;
                                break;
                            case Insurance.InsuranceTypeFlag.Tertiary:
                                IsTertiary = true;
                                break;
                            default:
                                break;
                        }
                    }
                    //SLR: do not dispose reference value
                    //if (oInsurance != null)   //Compress
                    //{
                    //    oInsurance.Dispose();
                    //    oInsurance = null;
                    //}

                }
                if (tvInsurances != null && tvInsurances.SelectedNode != null && tvInsurances.SelectedNode.Text == "Insurance")
                {

                    if (IsPrimary == false)
                        radSetAsPrimary.Checked = true;
                    else if (IsSecondary == false)
                        radSetAsSecondary.Checked = true;
                    else if (IsTertiary == false)
                        radSetAsTertiary.Checked = true;
                    else
                        rbInactive.Checked = true;
                }
                if (txtInsurance.Tag != null)
                {

                    // string[] insurnaceArray = null;
                    // insurnaceArray = Convert.ToString(txtInsurance.Tag).Split('|');
                    //Int64  insContactid ;
                    //insContactid = Convert.ToInt64(insurnaceArray[0]);

                    strInsuranceTypeCode = GetInsuranceType(Convert.ToInt64(txtInsurance.Tag));
                    // strInsuranceTypeCode = GetInsuranceType(insContactid);

                    //...Code changes done by Sagar Ghodke on 20100330
                    //...To consider MA as Medicare
                    //....I am not sure here for what this condition is written to make make as primary need to review this.
                    if (strInsuranceTypeCode.Trim().ToUpper() == "MB" || strInsuranceTypeCode.Trim().ToUpper() == "MA")
                    {
                        radSetAsPrimary.Checked = true;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }



        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion "Events"

        #region "Mouse Hover & Leave Events"
        //event to change buttons color on MouseOver 
        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        //event to change buttons color on MouseLeave 
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void tsb_MouseHover(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
            ((ToolStripButton)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void tsb_MouseLeave(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackgroundImage = null;
        }
        #endregion

        #region "Add Modify Insurance"

        private void btn_AddInsurance_Click(object sender, EventArgs e)
        {
            string _InsType = String.Empty;
           
            try
            {
                if (_IsSaveAsCopy == false)
                {
                    if (IsSelecedInsuranceUsed() == true)
                    {
                        MessageBox.Show("Cannot change this insurance plan.   This insurance has been used for insurance billing.   You may inactivate this insurance and add a new one.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                frmSetupInsurance ofrmSetupInsurance = new frmSetupInsurance(_databaseconnectionstring);
                if (ofrmSetupInsurance != null)
                {
                    ofrmSetupInsurance.ShowDialog(this);

                    txtInsurance.Text = ofrmSetupInsurance.InsuranceName;
                    txtInsurance.Tag = ofrmSetupInsurance.ContactID;

                    //...Code changes done by Sagar Ghodke on 20100330
                    //...Condition checked only for MB so added also for MA
                    //...Below commented line is previous condition
                    //if (GetInsuranceType(Convert.ToInt64(txtInsurance.Tag)).Trim().ToUpper() == "MB")
                    gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                    if (txtInsurance.Text.ToString() != "" && Convert.ToInt64(txtInsurance.Tag)> 0&& _isWorkerCompenable && ogloPatient.IsWorkerComp(Convert.ToInt64(txtInsurance.Tag)))
                    {
                        chkworkerscomp.Checked = true;
                        chkworkerscomp.Visible = true;
                        if (cmbRelationShip.Text.ToString() == "Self")
                        {
                            chkCompany.Visible = false;
                            chkCompany.Checked = false;
                        }
                        else
                        {
                            chkCompany.Visible = true;
                            chkCompany.Checked = true;
                        }
                    }
                    else
                    {
                        chkworkerscomp.Checked = false;
                        chkworkerscomp.Visible = false;
                    }
                    if (ofrmSetupInsurance.bIsGroupMandatory)
                    {
                        lblGroupMandatory.Visible = true;
                    }
                    else
                    {
                        lblGroupMandatory.Visible = false;
                    }
                    ogloPatient.Dispose();
                    ogloPatient = null;
                    _InsType = GetInsuranceType(Convert.ToInt64(txtInsurance.Tag)).Trim().ToUpper();
                    if (_InsType == "MB" || _InsType == "MA")
                    {
                        cmbMedicareTypeCode.Enabled = true;
                    }
                    else
                    {
                        cmbMedicareTypeCode.Enabled = false;
                        cmbMedicareTypeCode.SelectedIndex = 0;

                    }
                }
                //ofrmSetupInsurance.Dispose();

                if (ofrmSetupInsurance != null)
                {
                    ofrmSetupInsurance.Dispose();
                    ofrmSetupInsurance = null;
                }

                GetInsuranceFlag();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnModifyInsurance_Click(object sender, EventArgs e)
        {
           
            string _InsType = string.Empty;
                      
            try
            {
                if (txtInsurance.Tag != null && Convert.ToString(txtInsurance.Tag) != "")
                {
                    Insurance oIns = tvInsurances.SelectedNode.Tag as Insurance; 
                    
                    frmSetupInsurance ofrmSetupInsurance = new frmSetupInsurance(Convert.ToInt64(txtInsurance.Tag), _databaseconnectionstring);
                    if (ofrmSetupInsurance != null)
                    {
                        if (oIns != null)
                        {
                            ofrmSetupInsurance.nInsuranceID = oIns.InsuranceID;
                            ofrmSetupInsurance.PatientID = oIns.PatientID;
                        }
                        ofrmSetupInsurance.ShowDialog(this);
                        txtInsurance.Text = ofrmSetupInsurance.InsuranceName;
                        txtInsurance.Tag = ofrmSetupInsurance.ContactID;
                        if (ofrmSetupInsurance.bIsGroupMandatory)
                        {
                            lblGroupMandatory.Visible = true;
                        }
                        else
                        {
                            lblGroupMandatory.Visible = false;
                        }
                        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                        if (_isWorkerCompenable && ogloPatient.IsWorkerComp(Convert.ToInt64(txtInsurance.Tag)))
                        {
                            chkworkerscomp.Visible = true;
                            if (_isAddNewInsurance)
                                chkworkerscomp.Checked = true; 
                            if (cmbRelationShip.Text.ToString() == "Self")
                                chkCompany.Visible = false;
                            else
                                chkCompany.Visible = true;
                            _isAddNewInsurance = false;
                            //if (cmbRelationShip.Text == "Employee")
                            //{
                            //    chkCompany.Checked = true;
                            //}
                            //else
                            //{

                            //    chkCompany.Checked = false;
                            //}

                        }
                        else
                        {
                            chkworkerscomp.Visible = false;
                            chkCompany.Visible = false; 

                        }
                        ogloPatient.Dispose();
                        ogloPatient = null;
                        //..code changes done by Sagar Ghodke on 20100330
                        _InsType = GetInsuranceType(Convert.ToInt64(txtInsurance.Tag)).Trim().ToUpper();

                        if (_InsType == "MB" || _InsType == "MA")
                        {
                            cmbMedicareTypeCode.Enabled = true;
                        }
                        else
                        {
                            cmbMedicareTypeCode.Enabled = false;
                            cmbMedicareTypeCode.SelectedIndex = 0;
                        }
                    }
                    // ofrmSetupInsurance.Dispose();
                    //GetInsuranceFlag();

                    if (ofrmSetupInsurance != null)
                    {
                        ofrmSetupInsurance.Dispose();
                        ofrmSetupInsurance = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        #endregion

        private string GetInsuranceType(Int64 InsuranceId)
        {
            string strSQL = "";
            string strInsuranceType = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _result = null;
            try
            {
                if (oDB != null)
                {
                    oDB.Connect(false);
                    strSQL = "SELECT sInsuranceTypeCode FROM Contacts_Insurance_DTL WHERE nContactID = " + InsuranceId + " AND (nClinicID = " + _ClinicID + ")";
                    _result = oDB.ExecuteScalar_Query(strSQL);

                    if (_result != null)
                    {
                        strInsuranceType = Convert.ToString(_result);
                    }
                    oDB.Disconnect();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return strInsuranceType;
        }

        //Anil on 20090311
        #region "Check Eligibility"

        #region " Variables for Eligibility "

        string sPath = "";
        string sSEFFile = "";
        string sEdiFile = "";
        ediDocument oEdiDoc = null;
        ediInterchange oInterchange = null;
        ediGroup oGroup = null;
        ediTransactionSet oTransactionset = null;
        ediDataSegment oSegment = null;
        ediSchema oSchema = null;
        ediSchemas oSchemas = null;
        // ediWarnings oWarnings = null;
        // ediWarning oWarning = null;

        //   private string _SubscriberFName = "";
        //private string _SubscriberLName = "";
        //private string _SubscriberMName = "";
        //private string _SubscriberDOB = "";
        //private string _SubscriberCity = "";
        //private string _SubscriberState = "";
        //private string _SubscriberZip = "";
        private string _SubscriberGender = "";
        //private string _PayerName = "";
        //private string _SubscriberPrimaryID = "";
        //private string _SubscriberAdditionalID = "";
        //private string _SubscriberAdditionalIDQualifier = "1L";
        //private string _PayerID = "";
        //private string _SubscriberAddress = "";
        private string _SubscriberCardIssueDate = "";

        private string _ProviderFName = "";
        private string _ProviderLName = "";
        private string _ProviderMName = "";
        private string _ProviderCity = "";
        private string _ProviderSSN = "";
        private string _ProviderNPI = "";
        private string _ProviderState = "";
        private string _ProviderZip = "";
        private string _ProviderAddress = "";
        // private string _InsurancePlanCode = "";
        //private string _ServiceTypeCode = "";
        //   private Int64 _InsuranceId = 0;
        //Insurance Types
        // string strMedicaidCalifornia = "";
        //  string strMailHandlers = "";
        //  string strMutualOfOmaha = "";
        // string strAetna = "";

        #endregion " Variables for Eligibility "

        private void LoadEDIObject()
        {
            try
            {
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSEFFile = "270_X092A1.SEF";
                sEdiFile = "270OUTPUT.x12";
                oEdiDoc = new ediDocument();
                ediDocument.Set(ref oEdiDoc, new ediDocument());
                //oEdiDoc = new ediDocument();
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                oSchemas.EnableStandardReference = false;

                //oEdiDoc.SegmentTerminator = "~\r\n";
                //oEdiDoc.ElementTerminator = "*";
                //oEdiDoc.CompositeTerminator = ":";

                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                //ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema("270_X092A1.SEF", 0));
                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema(sPath + sSEFFile, 0));

                System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
                if (ofile.Exists == false)
                {
                    MessageBox.Show("SEF file is not present in the base directory.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private string ControlNumberGeneration(string HeaderType)
        {
            string strNumber = DateTime.Now.ToString("hhmmss");
            int _length = 0;
            string NumberSize = "";
            _length = strNumber.Trim().Length;
            if (_length == 5)
            {
                NumberSize = "000" + strNumber;
            }
            else if (_length == 6)
            {
                NumberSize = "00" + strNumber;
            }
            else if (_length == 7)
            {
                NumberSize = "0" + strNumber;
            }
            else if (_length == 8)
            {
                NumberSize = strNumber;
            }
            NumberSize = HeaderType + NumberSize;
            return NumberSize;
        }

        private void CheckEligibility()
        {
            DataTable dtClearingHouse = new DataTable();
            //string sEntity = "";
            //  string sInstance = "";
            string _TypeOfData = "T";

            string InterchangeHeader = "";
            string FunctionalGroupHeader = "";
            string TransactionSetHeader = "";

            try
            {
                if (IsEDIObjectLoaded == false)
                {
                    LoadEDIObject();
                    IsEDIObjectLoaded = true;
                }
                DataTable dtInsurance = new DataTable();


                oEdiDoc.New();

                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";

                if (ValidateDataEligibilityData())
                {
                    dtClearingHouse = new DataTable();
                    dtClearingHouse = GetClearingHouseSettings();
                    if (dtClearingHouse == null && dtClearingHouse.Rows.Count < 1)
                    {
                        MessageBox.Show("Clearing House information is not present.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    dtInsurance = GetInsuranceDetails(Convert.ToInt64(txtInsurance.Tag));

                    #region " Interchange Segment "
                    //Create the interchange segment
                    ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());
                    if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 0 || Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 1)
                    {
                        _TypeOfData = "T";
                    }
                    else if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 2)
                    {
                        _TypeOfData = "P";
                    }
                    oSegment.set_DataElementValue(1, 0, "00");
                    oSegment.set_DataElementValue(3, 0, "00");
                    oSegment.set_DataElementValue(5, 0, "12");
                    oSegment.set_DataElementValue(6, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", ""));//"1C26");//txtSenderID.Text.Trim());// "Sender");
                    oSegment.set_DataElementValue(7, 0, "12");
                    oSegment.set_DataElementValue(8, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", ""));//"V2EL");//txtReceiverID.Text.Trim());//"ReceiverID");
                    string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                    oSegment.set_DataElementValue(9, 0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()).ToString().Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                    string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                    oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());//txtEnquiryTime.Text.Trim());//"1548");
                    oSegment.set_DataElementValue(11, 0, "U");
                    oSegment.set_DataElementValue(12, 0, "00401");
                    InterchangeHeader = ControlNumberGeneration("1");
                    oSegment.set_DataElementValue(13, 0, InterchangeHeader);
                    //oSegment.set_DataElementValue(13, 0, "000000020");//txtControlNo.Text.Trim());
                    oSegment.set_DataElementValue(14, 0, "1");
                    oSegment.set_DataElementValue(15, 0, _TypeOfData);
                    oSegment.set_DataElementValue(16, 0, ":");

                    #endregion " Interchange Segment "

                    #region " Functional Group "

                    //Create the functional group segment
                    ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X092A1"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                    oSegment.set_DataElementValue(1, 0, "HS");
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", ""));//"IC26");
                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", ""));//"V2EL");
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());
                    string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                    oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                    FunctionalGroupHeader = ControlNumberGeneration("2");
                    oSegment.set_DataElementValue(6, 0, FunctionalGroupHeader);
                    //oSegment.set_DataElementValue(6, 0, "1");
                    oSegment.set_DataElementValue(7, 0, "X");
                    oSegment.set_DataElementValue(8, 0, "004010X092A1");

                    #endregion " Functional Group "

                    #region "Transaction Set "
                    //HEADER
                    //ST TRANSACTION SET HEADER
                    ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("270"));
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                    //oSegment.set_DataElementValue(2, 0, "00021");
                    TransactionSetHeader = ControlNumberGeneration("3");
                    oSegment.set_DataElementValue(2, 0, TransactionSetHeader);

                    #endregion "Transaction Set "

                    #region " BHT "

                    //Begining Segment 
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                    oSegment.set_DataElementValue(1, 0, "0022");
                    oSegment.set_DataElementValue(2, 0, "13");//Code 13=Request,01=Cancellation,36=Authority to deduct(Reply)
                    oSegment.set_DataElementValue(3, 0, ControlNumberGeneration("12"));//ReferenceID
                    oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"19990501");//Date
                    string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                    oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim());//"1319");


                    #endregion " BHT "

                    #region " Information Source "

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                    oSegment.set_DataElementValue(1, 0, "1");
                    oSegment.set_DataElementValue(3, 0, "20");//20=Information Source
                    oSegment.set_DataElementValue(4, 0, "1");

                    //INFORMATION SOURCE NAME 
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));

                    oSegment.set_DataElementValue(1, 0, "PR");//PR=Payer
                    oSegment.set_DataElementValue(2, 0, "2");//2=Non-Person Entity
                    oSegment.set_DataElementValue(3, 0, txtInsurance.Text.Trim());// _PayerName);//"INFORMATION SOURCE NAME" );//Payer organisation Name
                    oSegment.set_DataElementValue(8, 0, "PI");//PI=Payer Identification
                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtInsurance.Rows[0]["sPayerID"]));//"77710");//PayerID

                    #endregion " Information Source "

                    #region " Receiver Loop "

                    //INFORMATION RECEIVER LEVEL
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                    oSegment.set_DataElementValue(1, 0, "2");
                    oSegment.set_DataElementValue(2, 0, "1");
                    oSegment.set_DataElementValue(3, 0, "21");//21=Information Receiver
                    oSegment.set_DataElementValue(4, 0, "1");//1=Additional Subordinate HL Data segment in this Herarchical structure

                    //INFORMATION RECEIVER NAME (It is the medical service Provider)
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                    oSegment.set_DataElementValue(1, 0, "1P");//1P=Provider
                    oSegment.set_DataElementValue(2, 0, "1");//1=Person
                    oSegment.set_DataElementValue(3, 0, _ProviderLName);//Provider  LastName
                    oSegment.set_DataElementValue(4, 0, _ProviderFName);//Provider FirstName
                    oSegment.set_DataElementValue(5, 0, _ProviderMName);
                    oSegment.set_DataElementValue(8, 0, "XX");//SV=Service Provider Number
                    oSegment.set_DataElementValue(9, 0, _ProviderNPI);//"0202034");//Service Provider No

                    //INFORMATION RECEIVER ADDITIONAL IDENTIFICATION
                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\REF"));
                    //oSegment.set_DataElementValue(1, 0, "SY");
                    //oSegment.set_DataElementValue(2, 0, _ProviderSSN);
                    ////oSegment.set_DataElementValue(3, 0, "");
                    //oSegment.set_DataElementValue(4, 0, "");

                    //INFORMATION RECEIVER ADDRESS
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N3"));
                    oSegment.set_DataElementValue(1, 0, _ProviderAddress);
                    //oSegment.set_DataElementValue(2, 0, "1");

                    //INFORMATION RECEIVER CITY/STATE/ZIP CODE
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\N4"));
                    oSegment.set_DataElementValue(1, 0, _ProviderCity);
                    oSegment.set_DataElementValue(2, 0, _ProviderState);
                    oSegment.set_DataElementValue(3, 0, _ProviderZip);
                    //oSegment.set_DataElementValue(4, 0, "1");

                    ////INFORMATION RECEIVER CONTACT INFORMATION
                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\PER"));
                    //string[] RecContactQualifier = cmbRecContactFunctionCode.Text.Split('-');
                    //oSegment.set_DataElementValue(1, 0, RecContactQualifier[0].Trim());
                    //oSegment.set_DataElementValue(2, 0, txtReceiverContactName.Text.Trim());
                    //string[] RecContactCommQualifier = cmbRecCommNoQualifier.Text.Split('-');
                    //oSegment.set_DataElementValue(3, 0, RecContactCommQualifier[0].Trim());
                    //oSegment.set_DataElementValue(4, 0, txtReceiverCommNo.Text.Trim());

                    ////INFORMATION RECEIVER PROVIDER INFORMATION
                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(2)\\NM1\\PRV"));
                    //string[] RecProviderCode = cmbReceiverProviderCode.Text.Split('-');
                    //oSegment.set_DataElementValue(1, 0, RecProviderCode[0].Trim());
                    //string[] RecProviderIDQualifier = cmbRecProvRefIDQualifier.Text.Split('-');
                    //oSegment.set_DataElementValue(2, 0, RecProviderIDQualifier[0].Trim());
                    //oSegment.set_DataElementValue(3, 0, txtRecProviderSpecialtyCode.Text.Trim());


                    #endregion " Receiver Loop "

                    #region " Subscriber Loop "


                    //SUBSCRIBER LEVEL
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\HL"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                    oSegment.set_DataElementValue(1, 0, "3");
                    oSegment.set_DataElementValue(2, 0, "2");
                    oSegment.set_DataElementValue(3, 0, "22");//22=Subscriber
                    if (_SubscriberRelationShip.Trim().ToUpper() == "SELF")
                    {
                        oSegment.set_DataElementValue(4, 0, "0");//0=No Subordinate HL Data segment in this Herarchical structure
                    }
                    else
                    {
                        oSegment.set_DataElementValue(4, 0, "1");
                    }

                    //SUBSCRIBER TRACE NUMBER
                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\TRN"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                    //oSegment.set_DataElementValue(1, 0, "1");//1=Current Transaction Trace Numbers
                    //oSegment.set_DataElementValue(2, 0, "93175-012547");//Reference ID
                    //oSegment.set_DataElementValue(3, 0, "9967833434");//Originating Company ID


                    //SUBSCRIBER NAME(A person who can be uniquely identified to an information source. Traditionally referred to as a member.)
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\NM1"));	//oSegment = (ediDataSegment) oTransactionset.CreateDataSegment(sN1Loop + "N1");
                    oSegment.set_DataElementValue(1, 0, "IL");//IL=Insured or Subscriber
                    oSegment.set_DataElementValue(2, 0, "1"); //1=Person

                    oSegment.set_DataElementValue(3, 0, txtSubLName.Text.Trim());//"Subscriber Last Name");//
                    oSegment.set_DataElementValue(4, 0, txtSubFName.Text.Trim());//"Subscriber First Name");
                    if (txtSubMName.Text.Trim() != "")
                    {
                        oSegment.set_DataElementValue(5, 0, txtSubMName.Text.Trim());//"Subscriber Middle Name");//
                    }
                    oSegment.set_DataElementValue(8, 0, "MI");//MI=Member Identification number,ZZ=Mutually Defined
                    oSegment.set_DataElementValue(9, 0, txtSubscriberID.Text.Trim());//"11122333301");


                    //SUBSCRIBER ADDITIONAL IDENTIFICATION
                    if (txtGroup.Text.Trim() != "")
                    {
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                        oSegment.set_DataElementValue(1, 0, "6P");
                        oSegment.set_DataElementValue(2, 0, txtGroup.Text.Trim());
                    }
                    else if (Convert.ToString(_PatientSSN) != "" && _SubscriberRelationShip.Trim().ToUpper() == "SELF")
                    {
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\REF"));
                        oSegment.set_DataElementValue(1, 0, "SY");//"SY");//1L=Group or Policy Number
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(_PatientSSN));
                    }


                    ////SUBSCRIBER ADDRESS
                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));	
                    //oSegment.set_DataElementValue(1, 0, txtAddress1.Text.Trim());
                    //if( txtAddress2.Text.Trim()!="")
                    //{
                    //    oSegment.set_DataElementValue(2, 0, txtAddress2.Text.Trim());
                    //}

                    ////SUBSCRIBER CITY,STATE and ZIP
                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));
                    //oSegment.set_DataElementValue(1, 0, txtCity.Text.Trim());//"City");
                    //oSegment.set_DataElementValue(2, 0, cmbState.Text.Trim());//"State");
                    //oSegment.set_DataElementValue(3, 0, txtZip.Text.Trim());//"ZIP");

                    //SUBSCRIBER ADDRESS
                    //Sandip Darade 20091021 gloAddress control implemented  replacing code for address info above with code below 
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N3"));
                    oSegment.set_DataElementValue(1, 0, oAddresscontrol.txtAddress1.Text.Trim());
                    if (txtAddress2.Text.Trim() != "")
                    {
                        oSegment.set_DataElementValue(2, 0, oAddresscontrol.txtAddress2.Text.Trim());
                    }

                    //SUBSCRIBER CITY,STATE and ZIP
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\N4"));
                    oSegment.set_DataElementValue(1, 0, oAddresscontrol.txtCity.Text.Trim());//"City");
                    oSegment.set_DataElementValue(2, 0, oAddresscontrol.cmbState.Text.Trim());//"State");
                    oSegment.set_DataElementValue(3, 0, oAddresscontrol.txtZip.Text.Trim());//"ZIP");



                    //SUBSCRIBER PROVIDER INFORMATION
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\PRV"));
                    oSegment.set_DataElementValue(1, 0, "PE");//PC=Primary Care Physician PE=Performing
                    oSegment.set_DataElementValue(2, 0, "HPI");
                    oSegment.set_DataElementValue(3, 0, _ProviderNPI.Trim());

                    //SUBSCRIBER DEMOGRAPHIC INFORMATION
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DMG"));
                    oSegment.set_DataElementValue(1, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                    //oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(mtxtDOB.Text.Trim())));//"Subscriber Date of Birth"); //Date of Birth
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(mtxtDOB.Text.Replace("*", " "))));//"Subscriber Date of Birth"); //Date of Birth
                    oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().ToUpper());//"Gender"); //Gender

                    ///////

                    if (_SubscriberRelationShip.Trim().ToUpper() == "SELF")
                    {

                        ////SUBSCRIBER RELATIONSHIP
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\INS"));
                        oSegment.set_DataElementValue(1, 0, "Y");//Y-Yes or N-No
                        oSegment.set_DataElementValue(2, 0, "18");//18=Self
                        oSegment.set_DataElementValue(17, 0, "1");

                        if (_SubscriberCardIssueDate.Trim() == "")
                        {
                            //SUBSCRIBER DATE
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                            oSegment.set_DataElementValue(1, 0, "307");
                            oSegment.set_DataElementValue(2, 0, "D8");
                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                        }
                        else
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\DTP"));
                            oSegment.set_DataElementValue(1, 0, "102");//472=Service,102=Issue,307=Eligibility,435=Admission
                            oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                            oSegment.set_DataElementValue(3, 0, _SubscriberCardIssueDate.Trim());
                        }

                    }

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(3)\\NM1\\EQ\\EQ"));
                    oSegment.set_DataElementValue(1, 0, "30");
                    //oSegment.set_DataElementValue(3, 0, "FAM");// "FAM");//FAM= Family
                    //oSegment.set_DataElementValue(4, 0, GetInsuranceType(Convert.ToInt64(txtInsurance.Tag)));//


                    #endregion " Subscriber Loop "

                    //Added By MaheshB
                    #region Dependent Loop
                    if (cmbRelationShip.Text.ToString().ToUpper() != "SELF")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\HL"));
                        oSegment.set_DataElementValue(1, 0, "4");
                        oSegment.set_DataElementValue(2, 0, "3");
                        oSegment.set_DataElementValue(3, 0, "23");
                        oSegment.set_DataElementValue(4, 0, "0");

                        ////DEPENDENT TRACE NUMBER
                        //ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL\\TRN"));
                        //oSegment.set_DataElementValue(1, 0, "1");
                        //oSegment.set_DataElementValue(2, 0, "98175-02157");
                        //oSegment.set_DataElementValue(3, 0, "9877281234");
                        //oSegment.set_DataElementValue(4, 0, "RADIOLOGY");

                        //DEPENDENT NAME
                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\NM1"));
                        oSegment.set_DataElementValue(1, 0, "03");
                        oSegment.set_DataElementValue(2, 0, "1");
                        oSegment.set_DataElementValue(3, 0, _PatientLName.Trim());
                        oSegment.set_DataElementValue(4, 0, _PatientFName.Trim());
                        if (_PatientMName.Trim() != "")
                        {
                            oSegment.set_DataElementValue(5, 0, _PatientMName.Trim());
                        }

                        //DEPENDENT ADDITIONAL IDENTIFICATION
                        //SUBSCRIBER ADDITIONAL IDENTIFICATION
                        if (txtGroup.Text.Trim() != "")
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                            oSegment.set_DataElementValue(1, 0, "6P");
                            oSegment.set_DataElementValue(2, 0, txtGroup.Text.Trim());
                        }
                        else if (Convert.ToString(_PatientSSN) != "")
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\REF"));
                            oSegment.set_DataElementValue(1, 0, "SY");
                            oSegment.set_DataElementValue(2, 0, Convert.ToString(_PatientSSN));
                        }

                        //DEPENDENT ADDRESS
                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N3"));
                        oSegment.set_DataElementValue(1, 0, _PatientAddressLine1.Trim());
                        if (_PatientAddressLine2.Trim() != "")
                        {
                            oSegment.set_DataElementValue(2, 0, _PatientAddressLine2.Trim());
                        }

                        //DEPENDENT CITY/STATE/ZIP CODE
                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\N4"));
                        oSegment.set_DataElementValue(1, 0, _PatientCity.Trim());
                        oSegment.set_DataElementValue(2, 0, _PatientState.Trim());
                        oSegment.set_DataElementValue(3, 0, _PatientZip.Trim());

                        //PROVIDER INFORMATION
                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\PRV"));
                        oSegment.set_DataElementValue(1, 0, "PE");
                        oSegment.set_DataElementValue(2, 0, "HPI");
                        oSegment.set_DataElementValue(3, 0, _ProviderNPI.Trim());

                        //DEPENDENT DEMOGRAPHIC INFORMATION
                        ediDataSegment.Set(ref oSegment, oTransactionset.CreateDataSegment("HL(4)\\NM1\\DMG"));
                        oSegment.set_DataElementValue(1, 0, "D8");
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_PatientDOB.Trim())));
                        oSegment.set_DataElementValue(3, 0, _PatientGender.Trim().ToUpper());

                        //SUBSCRIBER DATE
                        if (mtxtStartDate.MaskCompleted == true)
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                            oSegment.set_DataElementValue(1, 0, "102");
                            oSegment.set_DataElementValue(2, 0, "D8");
                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(mtxtStartDate.Text).ToShortDateString())));
                        }
                        else
                        {
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\DTP"));
                            oSegment.set_DataElementValue(1, 0, "307");//472=Service,102=Issue,307=Eligibility,435=Admission
                            oSegment.set_DataElementValue(2, 0, "D8");//D8=Date Expressed in Format CCYYMMDD
                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//"Service DATE");//Service Date //Statement Date //Admission date/hour // Discharge Hour
                        }

                        //SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL(4)\\NM1\\EQ\\EQ"));
                        oSegment.set_DataElementValue(1, 0, "30"); // "98");//Service Type Code, 98 = Professional Visit - Office
                        oSegment.set_DataElementValue(3, 0, "");// "FAM");//FAM= Family
                        //oSegment.set_DataElementValue(4, 0, GetInsuranceType(Convert.ToInt64(txtInsurance.Tag)));//
                    }

                    #endregion

                    #region  " Save EDI File "

                    //Save to a file
                    oEdiDoc.Save(sPath + sEdiFile);
                    string EdiFile = "";
                    EdiFile = sPath + sEdiFile;
                    string _response = "";
                    //MessageBox.Show("File is sent for Eligibility Inquiry", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClsPostEDI objPostEDI = null;
                    try
                    {
                        objPostEDI = new ClsPostEDI();
                        objPostEDI._FilePath = EdiFile;
                        objPostEDI.PostEDI(Convert.ToString(dtInsurance.Rows[0]["sPayerID"]), _databaseconnectionstring, _PatientID, Convert.ToInt64(txtInsurance.Tag));//Changed By MaheshB On 20092106
                        //objPostEDI.PostEDI("9999", _databaseconnectionstring, 0);
                        _response = objPostEDI.PostEDIResult;

                    }
                    catch (Exception) // ex)
                    {
                       
                        //ex.ToString();
                        //ex = null;
                    }

                    if ((objPostEDI != null))
                    {
                        objPostEDI.Dispose();
                        objPostEDI = null;
                    }
                    #endregion  " Save EDI File "
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }

        }

        private void FillProviders(Int64 _ProviderID)
        {
            DataTable dtProviders = new DataTable();
            Resource oResource = new Resource(_databaseconnectionstring);
            Provider oProvider = null;

            try
            {
                //dtProviders = GetProviderDetail(_ProviderID);
                //if (dtProviders != null && dtProviders.Rows.Count > 0) 
                //{
                //    _ProviderFName = Convert.ToString(dtProviders.Rows[0]["sFirstName"]);
                //    _ProviderMName = Convert.ToString(dtProviders.Rows[0]["sMiddleName"]);
                //    _ProviderLName = Convert.ToString(dtProviders.Rows[0]["sLastName"]);
                //    _ProviderAddress = Convert.ToString(dtProviders.Rows[0]["sBusinessAddressline1"]) + " " + Convert.ToString(dtProviders.Rows[0]["sBusinessAddressline2"]);
                //    _ProviderCity = Convert.ToString(dtProviders.Rows[0]["sBusinessCity"]);
                //    _ProviderState = Convert.ToString(dtProviders.Rows[0]["sBusinessState"]);
                //    _ProviderZip = Convert.ToString(dtProviders.Rows[0]["sBusinessZIP"]);
                //    _ProviderSSN = Convert.ToString(dtProviders.Rows[0]["sSSN"]);
                //    _ProviderNPI = Convert.ToString(dtProviders.Rows[0]["sNPI"]);

                //}
                oProvider = oResource.GetProviderDetail(Convert.ToInt64(this.ProviderID));

                //Get the Address Setting for Billing Provider
                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                Object _objResult = null;
                string strBillingSetting = "";
                oSettings.GetSetting("BillingSetting", Convert.ToInt64(this.ProviderID), _ClinicID, out _objResult);
                if (_objResult != null)
                {
                    // |Company|Practice|Business"
                    strBillingSetting = Convert.ToString(_objResult);
                }
                oSettings.Dispose();
                oSettings = null;
                switch (strBillingSetting)
                {
                    case "Business":
                        {
                            _ProviderAddress = oProvider.BMAddress1 + oProvider.BMAddress2;
                            _ProviderCity = oProvider.BMCity;
                            _ProviderState = oProvider.BMState;
                            _ProviderZip = oProvider.BMZIP;

                        } break;
                    case "Practice":
                        {
                            _ProviderAddress = oProvider.BPracAddress1 + oProvider.BPracAddress2;
                            _ProviderCity = oProvider.BPracCity;
                            _ProviderState = oProvider.BPracState;
                            _ProviderZip = oProvider.BPracZIP;

                        } break;
                    case "Company":
                        {
                            _ProviderAddress = oProvider.CompanyAddress1 + oProvider.CompanyAddress2;
                            _ProviderCity = oProvider.CompanyCity;
                            _ProviderState = oProvider.CompanyState;
                            _ProviderZip = oProvider.CompanyZip;
                        } break;
                    default:

                        _ProviderAddress = oProvider.BMAddress1 + oProvider.BMAddress2;
                        _ProviderCity = oProvider.BMCity;
                        _ProviderState = oProvider.BMState;
                        _ProviderZip = oProvider.BMZIP;

                        break;
                }
                _ProviderFName = oProvider.FirstName;
                _ProviderMName = oProvider.MiddleName;
                _ProviderLName = oProvider.LastName;
                _ProviderSSN = oProvider.SSN;
                _ProviderNPI = oProvider.NPI;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private string GetInsurancePlanCode(string InsuranceName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            Object _result = null;
            try
            {
                strSQL = " SELECT BL_InsurancePlanCodes_MST.sPlanCode FROM Contacts_MST INNER JOIN " +
                         " BL_InsurancePlanCodes_MST ON Contacts_MST.sState = BL_InsurancePlanCodes_MST.sState " +
                         " WHERE (Contacts_MST.sName = '" + InsuranceName + "') AND (Contacts_MST.nClinicID = " + 1 + ") "; //clinic id
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(strSQL);
                oDB.Disconnect();
                return Convert.ToString(_result);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
                return "";
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        private DataTable GetProviderDetail(Int64 ProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dt = null;
            try
            {
                strSQL = "SELECT  nProviderID, sFirstName, sMiddleName, sLastName, sGender, sDEA, sBusinessAddressline1, sBusinessAddressline2, sBusinessCity, " +
                            " sBusinessState, sBusinessZIP, sPracticeAddressline1, sPracticeAddressline2, sPracticeCity, sPracticeState, sPracticeZIP, sBusPhoneNo, sBusFAX, " +
                            " sBusPagerNo, sBusEmail, sBusURL, sPracPhoneNo, sPracFAX, sPracPagerNo, sPracEmail, sPracURL, sMobileNo, imgSignature, nProviderType, " +
                            " sNPI, sUPIN, sSSN, sEmployerID, sMedicalLicenseNo, sPrefix, sExternalCode, sServiceLevel, dtActiveStartTime, dtActiveEndTime, sTaxonomy, sSPIID, nClinicID " +
                            " , sTaxonomyDesc, sCompanyName, sCompanyAddressline1, sCompanyAddressline2, sCompanyCity, sCompanyState, sCompanyZIP," +
                            " sCompanyPhone, sCompanyFax, sCompanyEmail, sCompanyContactName,sCompanyNPI,sCompanyTaxID, sBusinessContactName, sPracContactName, sSuffix, bIsblocked " +
                            " FROM Provider_MST where nProviderID =" + ProviderID + " AND nClinicID = " + _ClinicID + "";

                oDB.Connect(false);
                oDB.Retrive_Query(strSQL, out dt);
                oDB.Disconnect();
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
            return dt;
        }

        private string FormattedTime(string TimeFormat)
        {
            int _length = 0;
            _length = TimeFormat.Length;
            if (_length == 0)
            {
                TimeFormat = "0000";
            }
            if (_length == 1)
            {
                TimeFormat = "000" + TimeFormat;
            }
            else if (_length == 2)
            {
                TimeFormat = "00" + TimeFormat;
            }
            else if (_length == 3)
            {
                TimeFormat = "0" + TimeFormat;
            }            
            return TimeFormat;
        }

        private bool ValidateDataEligibilityData()
        {
            bool _Validated = false;
            string strMissingText = "";
            DataTable dtInsurance = new DataTable();

            string _FilePath = gloSettings.FolderSettings.AppTempFolderPath;  //AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                if (rbGender1.Checked == true)
                {
                    _SubscriberGender = "M";
                }
                else if (rbGender2.Checked == true)
                {
                    _SubscriberGender = "F";
                }
                else if (rbGender3.Checked == true)
                {
                    _SubscriberGender = "U";
                }

                FillProviders(_ProviderID);
                dtInsurance = GetInsuranceDetails(Convert.ToInt64(txtInsurance.Tag));

                if (txtSubFName.Text.Trim() == "") { strMissingText += "Subscriber First Name" + Environment.NewLine + ""; }
                if (txtSubLName.Text.Trim() == "") { strMissingText += "Subscriber Last Name" + Environment.NewLine + ""; }
                //if (_SubscriberMName == "") { strMissingText += "Subscriber Middle Name" + Environment.NewLine + ""; }
                if (mtxtDOB.Text.Trim() == "") { strMissingText += "Subscriber Date of Birth" + Environment.NewLine + ""; }
                //if (_SubscriberCity == "") { strMissingText += "Subscriber City" + Environment.NewLine + ""; }
                //if (_SubscriberState == "") { strMissingText += "Subscriber State" + Environment.NewLine + ""; }
                //if (_SubscriberZip == "") { strMissingText += "Subscriber Zip" + Environment.NewLine + ""; }
                //if (_SubscriberGender == "") { strMissingText += "Subscriber Gender" + Environment.NewLine + ""; }
                if (txtInsurance.Text.Trim() == "") { strMissingText += "Payer/Insurance Name" + Environment.NewLine + ""; }
                if (txtSubscriberID.Text.Trim() == "") { strMissingText += "Insurance ID" + Environment.NewLine + ""; }
                //if (_SubscriberAdditionalID == "") { strMissingText += "Group Number" + Environment.NewLine + ""; }
                //if (_SubscriberAdditionalIDQualifier == "") { strMissingText += " " + Environment.NewLine + ""; }
                if (dtInsurance.Rows.Count > 0)
                {
                    if (Convert.ToString(dtInsurance.Rows[0]["sPayerID"]) == "") { strMissingText += "Payer/Insurance ID" + Environment.NewLine + ""; }
                }
                //if (_SubscriberAddress == "") { strMissingText += "Subscriber Address" + Environment.NewLine + ""; }
                //if (_SubscriberCardIssueDate == "") { strMissingText += "Subscriber Card Issue Date" + Environment.NewLine + ""; }
                //if (_ProviderID == "") { strMissingText += "Provider ID" + Environment.NewLine + ""; }
                if (_ProviderFName == "") { strMissingText += "Provider First Name" + Environment.NewLine + ""; }
                if (_ProviderLName == "") { strMissingText += "Provider Last Name" + Environment.NewLine + ""; }
                //if (_ProviderMName == "") { strMissingText += "Provider Middle Name" + Environment.NewLine + ""; }
                if (_ProviderCity == "") { strMissingText += "Provider City" + Environment.NewLine + ""; }
                //if (_ProviderSSN == "") { strMissingText += "Provider SSN" + Environment.NewLine + ""; }
                if (_ProviderNPI == "") { strMissingText += "Provider NPI" + Environment.NewLine + ""; }
                if (_ProviderState == "") { strMissingText += "Provider State" + Environment.NewLine + ""; }
                if (_ProviderZip == "") { strMissingText += "Provider Zip" + Environment.NewLine + ""; }
                if (_ProviderAddress == "") { strMissingText += "Provider Address" + Environment.NewLine + ""; }
                if (_SubscriberGender == "") { strMissingText += "Subscriber Gender" + Environment.NewLine + ""; }
                if (txtSubFName.Text.Trim() == "") { strMissingText += "Subscriber First Name" + Environment.NewLine + ""; }
                //if (_PatientSSN == "") { strMissingText += "Patient SSN" + Environment.NewLine + ""; }
                //Shubhangi 20091201
                //Add validation for date for removing an exception Bug No:5311
                if (mtxtDOB.Text == "  /  /" || mtxtDOB.Text.Trim() == "") { strMissingText += "Subscriber DOB " + Environment.NewLine + ""; }
                //Shubhangi 20091201
                //cHECK FOR THE SPECIAL CHARACTERS then replace that with blank & check eligibility

                if (txtGroup.Text.Trim().IndexOfAny(new char[] { '*', ':', '!', '@' }) > -1)
                {
                    txtGroup.Select();
                    MessageBox.Show("Invalid values of Group.  ");
                    return false;
                }

                if (txtGroup.Text.Trim() == "") { strMissingText += " Group " + Environment.NewLine + ""; }


                if (cmbRelationShip.Text.ToString().ToUpper() != "SELF")
                {


                    if (_PatientLName == "") { strMissingText += "Patient Last Name" + Environment.NewLine + ""; }
                    if (_PatientDOB == "") { strMissingText += "Patient Date of Birth" + Environment.NewLine + ""; }

                    if (_PatientAddressLine1 + _PatientAddressLine2 == "") { strMissingText += "Patient Address" + Environment.NewLine + ""; }
                    if (_PatientCity == "") { strMissingText += "Patient City" + Environment.NewLine + ""; }
                    if (_PatientState == "") { strMissingText += "Patient State" + Environment.NewLine + ""; }
                    if (_PatientZip == "") { strMissingText += "Patient Zip" + Environment.NewLine + ""; }
                }
                if (strMissingText.Trim() != "")
                {
                    _Validated = false;
                    _FilePath = _FilePath + "270Validation.txt";
                    System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                    oStreamWriter.WriteLine(strMissingText);
                    oStreamWriter.Close();
                    oStreamWriter.Dispose();
                    System.Diagnostics.Process.Start(_FilePath);
                }
                else
                {
                    _Validated = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _Validated;
        }

        public DataTable GetClearingHouseSettings()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            DataTable dt = new DataTable();
            try
            {
                _strSQL = " SELECT nClearingHouseID, sClearingHouseCode, sReceiverID, sReceiverName, sSubmitterID, bIsOneJQulifier, sOneJQulifier, bIsSenderCode, sSenderCode,  " +
                          " bIsVenderIDCode, sVenderIDCode, bIsLoop1000BNM109, sLoop1000BNM109, nTypeOfData, bIsISA, nClinicID " +
                          " FROM BL_ClearingHouse_MST WHERE nClinicID=" + _ClinicID + "";
                oDB.Connect(false);
                oDB.Retrive_Query(_strSQL, out dt);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dt;
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

        private void lblInsuName_Click(object sender, EventArgs e)
        {

        }

        private void radSetAsTertiary_CheckedChanged(object sender, EventArgs e)
        {
            if (radSetAsTertiary.Checked == true)
            {
                radSetAsTertiary.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                radSetAsTertiary.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInactive.Checked == true)
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void radSetAsSecondary_CheckedChanged(object sender, EventArgs e)
        {
            if (radSetAsSecondary.Checked == true)
            {
                radSetAsSecondary.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                radSetAsSecondary.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void radSetAsPrimary_CheckedChanged(object sender, EventArgs e)
        {

            if (radSetAsPrimary.Checked == true)
            {
                radSetAsPrimary.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                radSetAsPrimary.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbGender1_CheckedChanged(object sender, EventArgs e)
        {
            //_isChecked = true; 
            if (rbGender1.Checked == true)
            {
                rbGender1.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbGender1.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void rbGender2_CheckedChanged(object sender, EventArgs e)
        {
            //_isChecked = true;
            if (rbGender2.Checked == true)
            {
                rbGender2.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbGender2.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbGender3_CheckedChanged(object sender, EventArgs e)
        {
            //_isChecked = true;
            if (rbGender3.Checked == true)
            {
                rbGender3.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbGender3.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void tsbEligibilityCheck_Click(object sender, EventArgs e)
        {

        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void gbPAGender_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void txtCoveragePercent_Validating(object sender, CancelEventArgs e)
        {
            if ((txtCoveragePercent.Text.Trim() == "") || (txtCoveragePercent.Text.Trim() == "."))
            {
                if (txtCoveragePercent.Text.Trim() == ".")
                {
                    txtCoveragePercent.Text = "0.00";
                }
            }
            else if (Convert.ToDecimal(txtCoveragePercent.Text) > 100)
            {
                MessageBox.Show(this, "Coverage % should be less than 100.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCoveragePercent.Focus();
                //return false;
            }
            else
            {
                txtCoveragePercent.Text = (Convert.ToDecimal(txtCoveragePercent.Text.Trim())).ToString("#0.00");

                if (Convert.ToDecimal(txtCoveragePercent.Text.Trim()) == (Convert.ToInt32(Convert.ToDecimal(txtCoveragePercent.Text.Trim()))))
                {
                    txtCoveragePercent.Text = (Convert.ToDecimal(txtCoveragePercent.Text.Trim())).ToString("#0");
                }
            }
        }

        private void txtCopay_Validating(object sender, CancelEventArgs e)
        {
            if ((txtCopay.Text.Trim() == "") || (txtCopay.Text.Trim() == "."))
            {
                if (txtCopay.Text.Trim() == ".")
                {
                    txtCopay.Text = "0.00";
                    _isValidated = true;
                }
            }
            else
            {
                if ((Convert.ToDecimal(txtCopay.Text.Trim())).ToString("#0.00").Length > 6 && txtCopay.Text.Contains(".") == false)
                {
                    //txtCopay.Text = (Convert.ToDecimal(txtCopay.Text.Trim())).ToString("#0.00");
                    MessageBox.Show(this, "Enter valid amount of copay.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCopay.Focus();
                    _isValidated = false;
                    return;
                }
                else
                {
                    if (txtCopay.Text.EndsWith(".") == false && txtCopay.Text.Length <= 6)
                    {
                        txtCopay.Text = (Convert.ToDecimal(txtCopay.Text.Trim())).ToString("#0.00");
                        _isValidated = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "Enter valid amount of copay.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCopay.Focus();
                        _isValidated = false;
                        return;
                    }
                }


            }
        }

        private void txtDeductableAmount_Validating(object sender, CancelEventArgs e)
        {
            if ((txtDeductableAmount.Text.Trim() == "") || (txtDeductableAmount.Text.Trim() == "."))
            {
                if (txtDeductableAmount.Text.Trim() == ".")
                {
                    txtDeductableAmount.Text = "0.00";
                }
            }
            else
            {
                // resolving TFS issue id - 1435
                //txtDeductableAmount.Text = (Convert.ToDecimal(txtDeductableAmount.Text.Trim())).ToString("#0.00");

                if ((Convert.ToDecimal(txtDeductableAmount.Text.Trim())).ToString("#0.00").Length > 8 && txtDeductableAmount.Text.Contains(".") == false)
                {

                    MessageBox.Show(this, "Enter valid amount of deductable.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDeductableAmount.Focus();
                    _isValidated = false;
                    return;
                }
                else
                {
                    if (txtDeductableAmount.Text.EndsWith(".") == false && txtDeductableAmount.Text.Length <= 8)
                    {
                        txtDeductableAmount.Text = (Convert.ToDecimal(txtDeductableAmount.Text.Trim())).ToString("#0.00");
                        _isValidated = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "Enter valid amount of deductable.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDeductableAmount.Focus();
                        _isValidated = false;
                        return;
                    }// end
                }

            }
        }

        private string GetFormattedString(string StringToFormat)
        {
            string _returnValue = "";

            try
            {
                if (StringToFormat != null && StringToFormat.ToString() != DBNull.Value.ToString() && StringToFormat.Trim() != "")
                {
                    _returnValue = StringToFormat.Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "").Replace("*", "").Replace("~", "").Replace(":", "");
                }
                else
                {
                    _returnValue = "";
                }
            }
            catch (Exception)
            {
                _returnValue = StringToFormat;
            }

            return _returnValue;
        }

        private void EiligibilityCheck()
        {
            gloPatientEiligibility ogloEiligibility = null;
            EiligiblityData _eligibilityData = null;
            DataTable _dtClearingHouse = null;
            DataTable _dtInsurance = null;
            DataTable _dtProvider = null;
            gloSettings.GeneralSettings oSettings = null;
            Object _objResult = null;
            string _result = "";
            int nANSIVersion = 0;

            try
            {
                this.Parent.Cursor = Cursors.WaitCursor;
                this.Cursor = Cursors.WaitCursor;


                if (txtInsurance.Text.Trim() != "")
                {
                    ogloEiligibility = new gloPatientEiligibility(_databaseconnectionstring);
                    _eligibilityData = new EiligiblityData();

                    oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                    oSettings.GetSetting("INSURANCEELIGIBILITY", out _objResult);
                    _result = Convert.ToString(_objResult);

                    Int64 _ContactID = 0;
                    Int64 _InsuranceID = 0;
                    if (tvInsurances.SelectedNode != null && tvInsurances.SelectedNode.Level > 0)
                    {
                        Insurance ins = (Insurance)tvInsurances.SelectedNode.Tag;
                        _ContactID = Convert.ToInt64(ins.ContactID);
                        _InsuranceID = Convert.ToInt64(ins.InsuranceID);
                      //  if (ins != null) { ins.Dispose(); }
                    }

                    nANSIVersion = oSettings.getANSIVersion(_ContactID, "ELIGIBILITY", _ClinicID);
                    
                    oSettings.Dispose();
                    oSettings = null;

                    if (nANSIVersion != 0)
                    {
                        //..Set Insurance Plan details
                        if (txtInsurance.Tag != null && txtInsurance.Tag.ToString().Trim() != "")
                        { _dtInsurance = ogloEiligibility.GetPatientInsurance(Convert.ToInt64(txtInsurance.Tag)); }

                        //..Set Contact Insurance detail
                        if (_dtInsurance != null && _dtInsurance.Rows.Count > 0)
                        {
                            _eligibilityData.PayerID = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sPayerId"]));
                        }
                        _eligibilityData.PayerName = GetFormattedString(Convert.ToString(txtInsurance.Text.Trim()));

                        _eligibilityData.ContactID = _ContactID;

                        #region " ..Set clearing house details.."

                        _dtClearingHouse = ogloEiligibility.GetClearingHouseSettings(_ContactID, _ClinicID);
                        if (_dtClearingHouse != null && _dtClearingHouse.Rows.Count > 0)
                        {
                            _eligibilityData.ClearingHouseReceiverID = GetFormattedString(Convert.ToString(_dtClearingHouse.Rows[0]["sReceiverID"]));
                            _eligibilityData.ClearingHouseSubmitterID = GetFormattedString(Convert.ToString(_dtClearingHouse.Rows[0]["sSubmitterID"]));
                            _eligibilityData.ClearingHouseTypeOfData = GetFormattedString(Convert.ToString(_dtClearingHouse.Rows[0]["nTypeOfData"]));
                            _eligibilityData.ClearingHouseType = Convert.ToInt32(_dtClearingHouse.Rows[0]["nClearingHouseType"]);
                            _eligibilityData.EligibilityUserName = Convert.ToString(_dtClearingHouse.Rows[0]["sEligibilityUserName"]);
                            _eligibilityData.EligibilityPassword = Convert.ToString(_dtClearingHouse.Rows[0]["sEligibilityPassword"]);
                            _eligibilityData.EligibilityUrl = Convert.ToString(_dtClearingHouse.Rows[0]["sEligibilityUrl"]);
                            _eligibilityData.SubmitterID = Convert.ToString(_dtClearingHouse.Rows[0]["sSubmitterID"]);

                            //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                            _eligibilityData.SenderQualifier = Convert.ToString(_dtClearingHouse.Rows[0]["sSenderIDQualifier"]);
                            _eligibilityData.ReceiverQualifier = Convert.ToString(_dtClearingHouse.Rows[0]["sReceiverIDQualifier"]);
                        }
                        #endregion " ..Set clearing house details.."

                        #region " ..Set Provider Details ""

                        _dtProvider = ogloEiligibility.GetProvider(_ProviderID, _ProviderID);
                        if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                        {
                            _eligibilityData.ProviderFName = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["FirstName"]));
                            _eligibilityData.ProviderMName = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["MiddleName"]));
                            _eligibilityData.ProviderLName = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["LastName"]));
                            _eligibilityData.ProviderSSN = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["SSN"]));
                            //                        _eligibilityData.ProviderNPI = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["NPI"]));


                            _eligibilityData.ProviderNPI = GetFormattedString(GetAlternateProvider(_ProviderID, _ContactID));
                            _eligibilityData.ProviderAddress = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["Address"]));
                            _eligibilityData.ProviderCity = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["City"]));
                            _eligibilityData.ProviderState = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["State"]));
                            _eligibilityData.ProviderZip = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["Zip"]));
                            _eligibilityData.ProviderAreaCode = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["AreaCode"]));
                            _eligibilityData.ProviderTaxId = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["TaxId"]));
                            _eligibilityData.ProviderSettingValue = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["SettingValue"]));
                            if (_dtInsurance != null && _dtInsurance.Rows.Count > 0)
                            {
                                _eligibilityData.InsEligibilityProviderType = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sInsEligibilityProviderType"]));
                                _eligibilityData.InsEligibilityProviderID = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sInsEligibilityProviderID"]));
                                _eligibilityData.InsEligibilityProviSecType = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sInsEligibilityProviSecType"]));
                                _eligibilityData.InsEligibilityProvSecID = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sInsEligibilityProvSecID"]));
                            }

                        }

                        #endregion

                        #region "..Set Subscriber details.."

                        _eligibilityData.Group = GetFormattedString(txtGroup.Text.Trim());
                        _eligibilityData.PatientSubscriberRelationShip = GetFormattedString(cmbRelationShip.Text.Trim());
                        _eligibilityData.SubscriberFName = GetFormattedString(txtSubFName.Text);
                        _eligibilityData.SubscriberLName = GetFormattedString(txtSubLName.Text);
                        _eligibilityData.SubscriberMName = GetFormattedString(txtSubMName.Text);
                        _eligibilityData.SubscriberCompanyname = GetFormattedString(txtCompanyName.Text);
                        _eligibilityData.IsSubscriberCompany = chkCompany.Checked;
                        if (mtxtDOB.MaskCompleted == true)
                        {
                            mtxtDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                            _eligibilityData.SubscriberDOB = Convert.ToDateTime(mtxtDOB.Text).ToString("MM/dd/yyyy");
                        }

                        if (rbGender1.Checked == true)
                        {
                            _eligibilityData.SubscriberGender = "M";
                        }
                        else if (rbGender2.Checked == true)
                        {
                            _eligibilityData.SubscriberGender = "F";
                        }
                        else
                        {
                            _eligibilityData.SubscriberGender = "U";
                        }

                        //if (_eligibilityData.SubscriberGender != "")
                        //{
                        //    if (_eligibilityData.SubscriberGender.ToUpper() == "MALE")
                        //    { _eligibilityData.SubscriberGender = "M"; }
                        //    else if (_eligibilityData.SubscriberGender.ToUpper() == "FEMALE")
                        //    { _eligibilityData.SubscriberGender = "F"; }
                        //    else
                        //    { _eligibilityData.SubscriberGender = "U"; }
                        //}
                        //else
                        //{ _eligibilityData.SubscriberGender = "U"; }


                        _eligibilityData.SubscriberID = GetFormattedString(txtSubscriberID.Text);
                        _eligibilityData.SubscriberCity = GetFormattedString(oAddresscontrol.txtCity.Text);
                        _eligibilityData.SubscriberSSN = "";
                        _eligibilityData.SubscriberState = GetFormattedString(oAddresscontrol.cmbState.Text);
                        _eligibilityData.SubscriberZip = GetFormattedString(oAddresscontrol.txtZip.Text);
                        _eligibilityData.SubscriberAddressLn1 = GetFormattedString(oAddresscontrol.txtAddress1.Text);
                        _eligibilityData.SubscriberAddressLn2 = GetFormattedString(oAddresscontrol.txtAddress2.Text);
                        _eligibilityData.IsSameAsPatient = Convert.ToBoolean(chkSameAsPatient.Checked);

                        if (mtxtStartDate.MaskCompleted == true)
                        {
                            mtxtStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            _eligibilityData.SubscriberInsStartDate = Convert.ToDateTime(mtxtStartDate.Text).ToString("MM/dd/yyyy");
                        }
                        else
                        { _eligibilityData.SubscriberInsStartDate = ""; }

                        if (mtxtEndDate.MaskCompleted == true)
                        {
                            mtxtEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            _eligibilityData.SubscriberInsEndDate = Convert.ToDateTime(mtxtEndDate.Text).ToString("MM/dd/yyyy");
                        }
                        else
                        { _eligibilityData.SubscriberInsEndDate = ""; }

                        #endregion "..Set Subscriber details.."

                        #region "..Set Patient details.."

                        _eligibilityData.PatientCode = ""; //Convert.ToString(_dtPatient.Rows[0]["PatientCode"]);
                        _eligibilityData.PatientFName = GetFormattedString(_PatientFName);
                        _eligibilityData.PatientLName = GetFormattedString(_PatientLName);
                        _eligibilityData.PatientMName = GetFormattedString(_PatientMName);

                        if (_PatientDOB != null && _PatientDOB.ToString() != DBNull.Value.ToString() && _PatientDOB.Trim() != "")
                        { _eligibilityData.PatientDOB = Convert.ToDateTime(_PatientDOB).ToString("MM/dd/yyyy"); }

                        _eligibilityData.PatientGender = GetFormattedString(_PatientGender);

                        if (_eligibilityData.PatientGender != null && _eligibilityData.PatientGender.ToString() != DBNull.Value.ToString() && _eligibilityData.PatientGender != "")
                        {
                            if (_eligibilityData.PatientGender.ToUpper() == "MALE")
                            { _eligibilityData.PatientGender = "M"; }
                            else if (_eligibilityData.PatientGender.ToUpper() == "FEMALE")
                            { _eligibilityData.PatientGender = "F"; }
                            else
                            { _eligibilityData.PatientGender = "U"; }
                        }
                        else
                        { _eligibilityData.PatientGender = "U"; }

                        _eligibilityData.PatientID = GetFormattedString(_PatientID.ToString());
                        _eligibilityData.PatientContactInsID = GetFormattedString(txtInsurance.Tag.ToString());
                        _eligibilityData.PatientCity = GetFormattedString(_PatientCity);
                        _eligibilityData.PatientSSN = GetFormattedString(_PatientSSN);
                        _eligibilityData.PatientState = GetFormattedString(_PatientState);
                        _eligibilityData.PatientZip = GetFormattedString(_PatientZip);
                        _eligibilityData.PatientAddressLn1 = GetFormattedString(_PatientAddressLine1);
                        _eligibilityData.PatientAddressLn2 = GetFormattedString(_PatientAddressLine2);

                        #endregion "..Set Patient details.."
                    }
                    else
                    {
                        MessageBox.Show("Eligibility Requests ANSI Version has not been set. Eligibility may not proceed. Please review in gloPM Admin.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    if ((_result == "" || _result == "BYCODE") && nANSIVersion != 0)
                    {
                        if (nANSIVersion == (int)gloSettings.ANSIVersions.ANSI_4010)
                        {
                            ogloEiligibility.EDIGeneration_270(_eligibilityData);
                        }
                        else if (nANSIVersion == (int)gloSettings.ANSIVersions.ANSI_5010)
                        {
                            ogloEiligibility.EDI5010Generation_270(_eligibilityData, gloSettings.ANSIVersions.ANSI_5010);
                        }

                    }
                    else if (_result == "BYSERVICE" && nANSIVersion != 0)
                    {
                        if (nANSIVersion == 0)
                        {
                            MessageBox.Show("Eligibility Requests ANSI Version has not been set.Eligibility may not proceed.Please review in gloPM Admin.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (nANSIVersion == (Int64)gloSettings.ANSIVersions.ANSI_4010)
                        {
                            ogloEiligibility.EDIGeneration_270(_eligibilityData, _InsuranceID, _ProviderID);
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
                this.Parent.Cursor = Cursors.Default;
                this.Cursor = Cursors.Default;
                if (ogloEiligibility != null) { ogloEiligibility.Dispose(); ogloEiligibility = null; }
                if (_eligibilityData != null) { _eligibilityData.Dispose(); _eligibilityData = null; }
                if (_dtClearingHouse != null) { _dtClearingHouse.Dispose(); _dtClearingHouse = null; }
                if (_dtInsurance != null) { _dtInsurance.Dispose(); _dtInsurance = null; }
                if (_dtProvider != null) { _dtProvider.Dispose(); _dtProvider = null; }
            }
        }

        private Boolean EiligibilityCheckOnInsuranceAdd(Insurance oInsurance,out DialogResult dgResult)
        {
            dgResult = DialogResult.None;
            Boolean bIsEligibilityCheckSuccess = false;
            gloSettings.GeneralSettings oSettings = null;
            
            if (bIsCheckAutoEligibilityInsurance && bIsCallFromSetupPatient)
            {
                string strMessage = string.Format("Automatic Insurance Eligibility will be perform for \"{0}\" insurance.\nDo you want to continue?\n\nYes: Automatic eligibility check take some time\nNo:  Skip automatic eligibility check", oInsurance.InsuranceName);
                DialogResult diaResult= MessageBox.Show(strMessage, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                dgResult = diaResult;
                if (diaResult==DialogResult.No)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.EligibilityCheck, "Insurance eligibility of \"" + oInsurance.InsuranceName + "\" skipped for Patient: " + Convert.ToString(_PatientID), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                    return false;
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.EligibilityCheck, "Insurance eligibility of " + oInsurance.InsuranceName + " started for Patient: " + Convert.ToString(_PatientID), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                gloPatientEiligibility ogloEiligibility = null;
                EiligiblityData _eligibilityData = null;
                DataTable _dtClearingHouse = null;
                DataTable _dtInsurance = null;
                DataTable _dtProvider = null;

                Object _objResult = null;
                string _result = "";
                int nANSIVersion = 0;

                try
                {
                    this.Parent.Cursor = Cursors.WaitCursor;
                    this.Cursor = Cursors.WaitCursor;

                    if (oInsurance.InsuranceName.ToString().Trim() != "")
                    {
                        ogloEiligibility = new gloPatientEiligibility(_databaseconnectionstring);
                        _eligibilityData = new EiligiblityData();

                        oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                        oSettings.GetSetting("INSURANCEELIGIBILITY", out _objResult);
                        _result = Convert.ToString(_objResult);

                        Int64 _ContactID = 0;
                        Int64 _InsuranceID = 0;

                        _ContactID = Convert.ToInt64(oInsurance.ContactID);
                        _InsuranceID = Convert.ToInt64(oInsurance.InsuranceID);

                        nANSIVersion = oSettings.getANSIVersion(_ContactID, "ELIGIBILITY", _ClinicID);

                        oSettings.Dispose();
                        oSettings = null;

                        if (nANSIVersion != 0)
                        {
                            //..Set Insurance Plan details
                            if (oInsurance.ContactID != null && oInsurance.ContactID.ToString().Trim() != "")
                            { _dtInsurance = ogloEiligibility.GetPatientInsurance(Convert.ToInt64(oInsurance.ContactID)); }

                            //..Set Contact Insurance detail
                            if (_dtInsurance != null && _dtInsurance.Rows.Count > 0)
                            {
                                _eligibilityData.PayerID = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sPayerId"]));
                            }
                            _eligibilityData.PayerName = GetFormattedString(Convert.ToString(oInsurance.InsuranceName.Trim()));

                            _eligibilityData.ContactID = _ContactID;

                            #region " ..Set clearing house details.."

                            _dtClearingHouse = ogloEiligibility.GetClearingHouseSettings(_ContactID, _ClinicID);
                            if (_dtClearingHouse != null && _dtClearingHouse.Rows.Count > 0)
                            {
                                _eligibilityData.ClearingHouseReceiverID = GetFormattedString(Convert.ToString(_dtClearingHouse.Rows[0]["sReceiverID"]));
                                _eligibilityData.ClearingHouseSubmitterID = GetFormattedString(Convert.ToString(_dtClearingHouse.Rows[0]["sSubmitterID"]));
                                _eligibilityData.ClearingHouseTypeOfData = GetFormattedString(Convert.ToString(_dtClearingHouse.Rows[0]["nTypeOfData"]));
                                _eligibilityData.ClearingHouseType = Convert.ToInt32(_dtClearingHouse.Rows[0]["nClearingHouseType"]);
                                _eligibilityData.EligibilityUserName = Convert.ToString(_dtClearingHouse.Rows[0]["sEligibilityUserName"]);
                                _eligibilityData.EligibilityPassword = Convert.ToString(_dtClearingHouse.Rows[0]["sEligibilityPassword"]);
                                _eligibilityData.EligibilityUrl = Convert.ToString(_dtClearingHouse.Rows[0]["sEligibilityUrl"]);
                                _eligibilityData.SubmitterID = Convert.ToString(_dtClearingHouse.Rows[0]["sSubmitterID"]);

                                //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                                _eligibilityData.SenderQualifier = Convert.ToString(_dtClearingHouse.Rows[0]["sSenderIDQualifier"]);
                                _eligibilityData.ReceiverQualifier = Convert.ToString(_dtClearingHouse.Rows[0]["sReceiverIDQualifier"]);
                            }
                            #endregion " ..Set clearing house details.."

                            #region " ..Set Provider Details ""

                            _dtProvider = ogloEiligibility.GetProvider(_ProviderID, _ProviderID);
                            if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                            {
                                _eligibilityData.ProviderFName = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["FirstName"]));
                                _eligibilityData.ProviderMName = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["MiddleName"]));
                                _eligibilityData.ProviderLName = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["LastName"]));
                                _eligibilityData.ProviderSSN = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["SSN"]));
                                //                        _eligibilityData.ProviderNPI = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["NPI"]));


                                _eligibilityData.ProviderNPI = GetFormattedString(GetAlternateProvider(_ProviderID, _ContactID));
                                _eligibilityData.ProviderAddress = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["Address"]));
                                _eligibilityData.ProviderCity = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["City"]));
                                _eligibilityData.ProviderState = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["State"]));
                                _eligibilityData.ProviderZip = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["Zip"]));
                                _eligibilityData.ProviderAreaCode = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["AreaCode"]));
                                _eligibilityData.ProviderTaxId = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["TaxId"]));
                                _eligibilityData.ProviderSettingValue = GetFormattedString(Convert.ToString(_dtProvider.Rows[0]["SettingValue"]));
                                if (_dtInsurance != null && _dtInsurance.Rows.Count > 0)
                                {
                                    _eligibilityData.InsEligibilityProviderType = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sInsEligibilityProviderType"]));
                                    _eligibilityData.InsEligibilityProviderID = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sInsEligibilityProviderID"]));
                                    _eligibilityData.InsEligibilityProviSecType = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sInsEligibilityProviSecType"]));
                                    _eligibilityData.InsEligibilityProvSecID = GetFormattedString(Convert.ToString(_dtInsurance.Rows[0]["sInsEligibilityProvSecID"]));
                                }

                            }

                            #endregion

                            #region "..Set Subscriber details.."

                            _eligibilityData.Group = GetFormattedString(oInsurance.Group.Trim());
                            _eligibilityData.PatientSubscriberRelationShip = GetFormattedString(oInsurance.RelationshipName.Trim());
                            _eligibilityData.SubscriberFName = GetFormattedString(oInsurance.SubscriberFName);
                            _eligibilityData.SubscriberLName = GetFormattedString(oInsurance.SubscriberLName);
                            _eligibilityData.SubscriberMName = GetFormattedString(oInsurance.SubscriberMName);
                            _eligibilityData.SubscriberCompanyname = GetFormattedString(oInsurance.SubscriberCompanyLName);
                            _eligibilityData.IsSubscriberCompany = oInsurance.IsCompnay;

                            if (!oInsurance.IsNotDOB)
                            {
                                _eligibilityData.SubscriberDOB = oInsurance.DOB.ToString("MM/dd/yyyy");
                            }
                            else
                            {
                                _eligibilityData.SubscriberDOB = "";
                            }

                            if (oInsurance.SubscriberGender.ToLower().Trim() == "male")
                            {
                                _eligibilityData.SubscriberGender = "M";
                            }
                            else if (oInsurance.SubscriberGender.ToLower().Trim() == "female")
                            {
                                _eligibilityData.SubscriberGender = "F";
                            }
                            else
                            {
                                _eligibilityData.SubscriberGender = "U";
                            }

                            _eligibilityData.SubscriberID = GetFormattedString(oInsurance.SubscriberID);
                            _eligibilityData.SubscriberCity = GetFormattedString(oInsurance.SubscriberCity);
                            _eligibilityData.SubscriberSSN = "";
                            _eligibilityData.SubscriberState = GetFormattedString(oInsurance.SubscriberState);
                            _eligibilityData.SubscriberZip = GetFormattedString(oInsurance.SubscriberZip);
                            _eligibilityData.SubscriberAddressLn1 = GetFormattedString(oInsurance.SubscriberAddr1);
                            _eligibilityData.SubscriberAddressLn2 = GetFormattedString(oInsurance.SubscriberAddr2);
                            _eligibilityData.IsSameAsPatient = oInsurance.IsSameAsPatient;

                            if (!oInsurance.IsNotStartDate)
                            {
                                _eligibilityData.SubscriberInsStartDate = oInsurance.StartDate.ToString("MM/dd/yyyy");
                            }
                            else
                            {
                                _eligibilityData.SubscriberInsStartDate = "";
                            }

                            if (!oInsurance.IsNotEndDate)
                            {
                                _eligibilityData.SubscriberInsEndDate = oInsurance.EndDate.ToString("MM/dd/yyyy");
                            }
                            else
                            {
                                _eligibilityData.SubscriberInsEndDate = "";
                            }

                            #endregion "..Set Subscriber details.."

                            #region "..Set Patient details.."

                            _eligibilityData.PatientCode = ""; //Convert.ToString(_dtPatient.Rows[0]["PatientCode"]);
                            _eligibilityData.PatientFName = GetFormattedString(_PatientFName);
                            _eligibilityData.PatientLName = GetFormattedString(_PatientLName);
                            _eligibilityData.PatientMName = GetFormattedString(_PatientMName);

                            if (_PatientDOB != null && _PatientDOB.ToString() != DBNull.Value.ToString() && _PatientDOB.Trim() != "")
                            { _eligibilityData.PatientDOB = Convert.ToDateTime(_PatientDOB).ToString("MM/dd/yyyy"); }

                            _eligibilityData.PatientGender = GetFormattedString(_PatientGender);

                            if (_eligibilityData.PatientGender != null && _eligibilityData.PatientGender.ToString() != DBNull.Value.ToString() && _eligibilityData.PatientGender != "")
                            {
                                if (_eligibilityData.PatientGender.ToUpper() == "MALE")
                                { _eligibilityData.PatientGender = "M"; }
                                else if (_eligibilityData.PatientGender.ToUpper() == "FEMALE")
                                { _eligibilityData.PatientGender = "F"; }
                                else
                                { _eligibilityData.PatientGender = "U"; }
                            }
                            else
                            { _eligibilityData.PatientGender = "U"; }

                            _eligibilityData.PatientID = GetFormattedString(_PatientID.ToString());
                            _eligibilityData.PatientContactInsID = GetFormattedString(oInsurance.ContactID.ToString());
                            _eligibilityData.PatientCity = GetFormattedString(_PatientCity);
                            _eligibilityData.PatientSSN = GetFormattedString(_PatientSSN);
                            _eligibilityData.PatientState = GetFormattedString(_PatientState);
                            _eligibilityData.PatientZip = GetFormattedString(_PatientZip);
                            _eligibilityData.PatientAddressLn1 = GetFormattedString(_PatientAddressLine1);
                            _eligibilityData.PatientAddressLn2 = GetFormattedString(_PatientAddressLine2);

                            #endregion "..Set Patient details.."
                        }
                        else
                        {
                            MessageBox.Show("Eligibility Requests ANSI Version has not been set. Eligibility may not proceed. Please review in gloPM Admin.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                        if ((_result == "" || _result == "BYCODE") && nANSIVersion != 0)
                        {
                            if (nANSIVersion == (int)gloSettings.ANSIVersions.ANSI_4010)
                            {
                                bIsEligibilityCheckSuccess = ogloEiligibility.EDIGeneration_270(_eligibilityData,true);
                            }
                            else if (nANSIVersion == (int)gloSettings.ANSIVersions.ANSI_5010)
                            {
                                bIsEligibilityCheckSuccess = ogloEiligibility.EDI5010Generation_270(_eligibilityData, gloSettings.ANSIVersions.ANSI_5010,true);
                            }

                        }
                        else if (_result == "BYSERVICE" && nANSIVersion != 0)
                        {
                            if (nANSIVersion == 0)
                            {
                                MessageBox.Show("Eligibility Requests ANSI Version has not been set.Eligibility may not proceed.Please review in gloPM Admin.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (nANSIVersion == (Int64)gloSettings.ANSIVersions.ANSI_4010)
                            {
                                ogloEiligibility.EDIGeneration_270(_eligibilityData, _InsuranceID, _ProviderID);
                            }
                        }
                    }
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.EligibilityCheck, "Insurance eligibility completed for Patient: " + Convert.ToString(_PatientID), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                }
                catch (Exception ex)
                {
                    bIsEligibilityCheckSuccess = false;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.EligibilityCheck, "Exception while Insurance eligibility check." + ex.Message, _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    this.Parent.Cursor = Cursors.Default;
                    this.Cursor = Cursors.Default;
                    if (ogloEiligibility != null) { ogloEiligibility.Dispose(); ogloEiligibility = null; }
                    if (_eligibilityData != null) { _eligibilityData.Dispose(); _eligibilityData = null; }
                    if (_dtClearingHouse != null) { _dtClearingHouse.Dispose(); _dtClearingHouse = null; }
                    if (_dtInsurance != null) { _dtInsurance.Dispose(); _dtInsurance = null; }
                    if (_dtProvider != null) { _dtProvider.Dispose(); _dtProvider = null; }
                }
            }
            else
            {
                dgResult = DialogResult.No;
                bIsEligibilityCheckSuccess = false;
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.EligibilityCheck, "Enable Auto Eligibility Insurance setting: false, Insurance eligibility of \"" + oInsurance.InsuranceName + "\" not performed for Patient: " + Convert.ToString(_PatientID), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            return bIsEligibilityCheckSuccess;
            
        }

        private bool CheckEligibilitySetting()
        {
            gloSettings.GeneralSettings oSettings= null;
            bool bIsEnableAutoEligibilityInsurance= false;
            Object objEnableAutoEligibilityInsurance = null;
            
            try
            {
                oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                oSettings.GetSetting("EnableAutoEligibilityInsurance", out objEnableAutoEligibilityInsurance);

                if (objEnableAutoEligibilityInsurance == null || objEnableAutoEligibilityInsurance == "")
                {
                    bIsEnableAutoEligibilityInsurance = false;
                }
                else
                {
                    bIsEnableAutoEligibilityInsurance = Convert.ToBoolean(objEnableAutoEligibilityInsurance);
                }

                //SLR: Free oSettings, _objSresult
                oSettings.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
                if (objEnableAutoEligibilityInsurance != null)
                {
                    objEnableAutoEligibilityInsurance = null;
                }
            }
            return bIsEnableAutoEligibilityInsurance;
        }

        public string GetAlternateProvider(Int64 nProviderId, Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable _dtInsurance = null;
            string _result = "";
            try
            {
                if (oDB != null)
                {
                    oDB.Connect(false);
                    _sqlquery = "Select Case when ISNULL(sInsEligibilityProviderID,'') ='' then " +
                                "(Select ISNULL(sSettingsValue,'') as sSettingsValue from Settings where sSettingsname ='Eligibility Request Provider ID') " +
                                " else ISNULL(sInsEligibilityProviderID,'') end  " +
                                " as NPI from Contacts_Insurance_DTL where nContactID=" + ContactID + "";
                    oDB.Retrive_Query(_sqlquery, out _dtInsurance);
                    if (_dtInsurance != null && _dtInsurance.Rows.Count > 0)
                    {
                        _result = Convert.ToString(_dtInsurance.Rows[0]["NPI"]);
                    }
                    if (_result.Trim() == "")
                    {
                        _sqlquery = "Select ISNULL(sNPI,'') AS NPI FROM Provider_Mst WHERE nProviderID = " + nProviderId;
                        oDB.Retrive_Query(_sqlquery, out _dtInsurance);
                        if (_dtInsurance != null && _dtInsurance.Rows.Count > 0)
                        {
                            _result = Convert.ToString(_dtInsurance.Rows[0]["NPI"]);
                        }
                    }

                    oDB.Disconnect();
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
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        private bool Validate_InsuranceSubscriberMandatory()
        {
            if (_isInsuranceSubscriberMandatory)
            {
                if (chkworkerscomp.Checked == true && chkCompany.Checked == true && cmbRelationShip.Text.ToString().ToUpper() != "employee".ToUpper())
                {
                    if (MessageBox.Show("Patient relationship to subscriber is normally Employee" + Environment.NewLine + "when the subscriber is a company." + Environment.NewLine + "Continue Save?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        cmbRelationShip.Focus();
                        return false;
                    }
                }
                if (cmbDefaultTypeCode.Text.Trim() == "")
                {
                    cmbDefaultTypeCode.Focus();
                    MessageBox.Show("Select insurance type.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //cmbDefaultTypeCode.Focus();
                    return false;
                }
                if (cmbRelationShip.Text == "Self")
                {
                    if (txtSubscriberID.Text.Trim() == "")
                    {
                        MessageBox.Show(this, "Enter insurance ID.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSubscriberID.Focus();
                        return false;
                    }
                    #region "Check special Character"
                    var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z\b]");
                    if (regex.IsMatch(txtSubFName.Text.Trim()) || regex.IsMatch(txtSubMName.Text.Trim()) || regex.IsMatch(txtSubLName.Text.Trim()))
                    {
                        if ((MessageBox.Show("Subscriber name contains special/numeric character(s) which may cause billing rejection.\n Continue Save?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) == DialogResult.No)
                        {
                            if (regex.IsMatch(txtSubFName.Text.Trim())) { txtSubFName.Select(); return false; }
                            if (regex.IsMatch(txtSubMName.Text.Trim())) { txtSubMName.Select(); return false; }
                            if (regex.IsMatch(txtSubLName.Text.Trim())) { txtSubLName.Select(); return false; }
                            return false;
                        }
                    }
                    if (regex.IsMatch(txtCompanyName.Text.Trim()))
                    {
                        if ((MessageBox.Show("Subscriber name contains special/numeric character(s) which may cause billing rejection.\n Continue Save?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) == DialogResult.No)
                        {
                            if (regex.IsMatch(txtCompanyName.Text.Trim())) { txtCompanyName.Select(); return false; }
                        }
                    }
                    #endregion "Check special Character"

                }
                else
                {
                    if ((cmbRelationShip.Text.Trim() != "") ||
                        (txtSubFName.Text.Trim() != "") ||
                        (txtSubLName.Text.Trim() != "") ||
                        (txtSubMName.Text.Trim() != "") ||
                        (mtxtDOB.Text.Replace("/", "").Trim() != "") ||
                        ((rbGender1.Checked == true) || (rbGender2.Checked == true) || (rbGender3.Checked == true)) ||
                        (txtSubscriberID.Text.Trim() != "") ||
                        (oAddresscontrol.txtAddress1.Text.Trim() != "") ||
                        (oAddresscontrol.txtAddress2.Text.Trim() != "") ||
                        (oAddresscontrol.txtCity.Text.Trim() != "") ||
                        (oAddresscontrol.txtCounty.Text.Trim() != "") ||
                        (oAddresscontrol.txtZip.Text.Trim() != "") ||
                        (oAddresscontrol.cmbState.Text.Trim() != "") ||
                        (txtGroup.Text.Trim() != "") ||
                        (mskInsurancePhone.Text.Trim() != "") ||
                        (chkAddrSameAsPatient.Checked == true) ||
                        (txtCompanyName.Text.Trim() != ""))
                    {
                        if (cmbRelationShip.Text.Trim() == "")
                        {
                            MessageBox.Show(this, "Select subscriber relationship to patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbRelationShip.Focus();
                            return false;
                        }
                        if (txtSubscriberID.Text.Trim() == "")
                        {
                            MessageBox.Show(this, "Enter insurance ID.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSubscriberID.Focus();
                            return false;
                        }
                        if ((txtSubFName.Text.Trim() == "") && chkCompany.Checked == false)
                        {
                            MessageBox.Show(this, "Enter first name for subscriber.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSubFName.Focus();
                            return false;
                        }
                        if ((txtSubLName.Text.Trim() == "") && chkCompany.Checked == false)
                        {
                            MessageBox.Show(this, "Enter last name for subscriber.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSubLName.Focus();
                            return false;
                        }
                        if ((rbGender1.Checked == false) && (rbGender2.Checked == false) && (rbGender3.Checked == false) && chkCompany.Checked == false)
                        {
                            MessageBox.Show(this, "Select gender for subscriber.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        if (mtxtDOB.Text.Replace("/", "").Trim() == "" && chkCompany.Checked == false)
                        {
                            MessageBox.Show(this, "Enter date of birth for subscriber.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mtxtDOB.Focus();
                            return false;
                        }
                        if ((txtCompanyName.Text.Trim() == "") && chkCompany.Checked == true)
                        {
                            MessageBox.Show(this, "Enter name for subscriber.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCompanyName.Focus();
                            return false;
                        }

                    }
                    #region "Check special Character"
                    var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z\b]");
                    if (regex.IsMatch(txtSubFName.Text.Trim()) || regex.IsMatch(txtSubMName.Text.Trim()) || regex.IsMatch(txtSubLName.Text.Trim()))
                    {
                        if ((MessageBox.Show("Subscriber name contains special/numeric character(s) which may cause billing rejection.\n Continue Save?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) == DialogResult.No)
                        {
                            if (regex.IsMatch(txtSubFName.Text.Trim())) { txtSubFName.Select(); return false; }
                            if (regex.IsMatch(txtSubMName.Text.Trim())) { txtSubMName.Select(); return false; }
                            if (regex.IsMatch(txtSubLName.Text.Trim())) { txtSubLName.Select(); return false; }
                            return false;
                        }
                    }
                    if (regex.IsMatch(txtCompanyName.Text.Trim()))
                    {
                        if ((MessageBox.Show("Subscriber name contains special/numeric character(s) which may cause billing rejection.\n Continue Save?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) == DialogResult.No)
                        {
                            if (regex.IsMatch(txtCompanyName.Text.Trim())) { txtCompanyName.Select(); return false; }
                        }
                    }
                    #endregion "Check special Character"
                }
            }
            #region check Subscriber id contains contains combination of letters or  not
            if (_isCallFromAddInsurance == false || tvInsurances.SelectedNode.Text == "Insurance")
            {
                if (Regex.IsMatch(txtSubscriberID.Text, "[a-z]"))
                {
                    if (MessageBox.Show("Insurance ID: Lower case alpha not allowed.\n Stop to review ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        return false;
                    }
                }
            }
            #endregion
          

            return true;
        }

        private void cmbMedicareTypeCode_MouseEnter(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbMedicareTypeCode.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbMedicareTypeCode.Items[cmbMedicareTypeCode.SelectedIndex])["sInsTypeDescriptionMedicare"]), cmbMedicareTypeCode) >= cmbMedicareTypeCode.DropDownWidth - 20)
                {
                    this.toolTipInsurance.SetToolTip(cmbMedicareTypeCode, Convert.ToString(((DataRowView)cmbMedicareTypeCode.Items[cmbMedicareTypeCode.SelectedIndex])["sInsTypeDescriptionMedicare"]));
                }
                else
                {
                    this.toolTipInsurance.SetToolTip(cmbMedicareTypeCode, "");
                }
            }
        }

        private void cmbMedicareTypeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbMedicareTypeCode.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbMedicareTypeCode.Items[cmbMedicareTypeCode.SelectedIndex])["sInsTypeDescriptionMedicare"]), cmbMedicareTypeCode) >= cmbMedicareTypeCode.DropDownWidth - 20)
                {
                    this.toolTipInsurance.SetToolTip(cmbMedicareTypeCode, Convert.ToString(((DataRowView)cmbMedicareTypeCode.Items[cmbMedicareTypeCode.SelectedIndex])["sInsTypeDescriptionMedicare"]));
                }
                else
                {
                    this.toolTipInsurance.SetToolTip(cmbMedicareTypeCode, "");
                }
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {

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

        private void rbGenderAll_Click(object sender, EventArgs e)
        {
            //RadioButton _Rb = (RadioButton)sender;
            //if (_Rb.Checked == true && _isChecked == false)
            //    _Rb.Checked = false;

            //_isChecked = false;
        }

        private void cmbMedicareTypeCode_MouseMove(object sender, MouseEventArgs e)
        {
            //if (cmbMedicareTypeCode.SelectedItem != null)
            //{
            //    if (getWidthofListItems(Convert.ToString(cmbMedicareTypeCode.Text), cmbMedicareTypeCode) >= cmbMedicareTypeCode.DropDownWidth - 20)
            //    {
            //        this.toolTipInsurance.SetToolTip(cmbMedicareTypeCode, Convert.ToString(cmbMedicareTypeCode.Text));
            //    }
            //    else
            //    {
            //        this.toolTipInsurance.SetToolTip(cmbMedicareTypeCode, "");
            //    }
            //}
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
                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 18 && _LastBoundIndex != e.Index)
                            this.toolTipInsurance.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 170, e.Bounds.Bottom + 30);
                        _LastBoundIndex = e.Index;


                    }
                    else
                    {
                        this.toolTipInsurance.Hide(combo);
                    }
                }
                else
                {
                    this.toolTipInsurance.SetToolTip(combo, "");
                }
                e.DrawFocusRectangle();
            }
        }
        Int32 _LastBoundIndex = -1;


        private bool IsSelecedInsuranceUsed()
        {
            Boolean _Result = false;
           
            try
            {
                if (tvInsurances.SelectedNode != null)
                {
                    if (tvInsurances.SelectedNode.Level != 0)
                    {

                        Insurance oIns = tvInsurances.SelectedNode.Tag as Insurance;
                        if (oIns != null)
                        {
                            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                            if (ogloPatient.IsInsuranceUsed(oIns.InsuranceID) == true)
                            {
                                _Result = true;
                            }
                            ogloPatient.Dispose();
                            ogloPatient = null;
                        }
                        //SLR: do not dispose the reference
                        //if (oIns != null) { oIns.Dispose(); oIns = null; }
                    }
                }

            }
            catch (Exception ex)
            {
                _Result = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //if (ogloPatient != null) { ogloPatient.Dispose(); ogloPatient = null; }
            }
            return _Result;

        }

        public void DisposeAllControls()
        {
            try
            {
                if (_oInsurancesDetails != null) { _oInsurancesDetails.Dispose(); }
                if (tvInsurances != null) { tvInsurances.Dispose(); }
                if (_oPatienInsuranceOther != null) { _oPatienInsuranceOther.Dispose(); }

                if (oListControl != null) { oListControl.Dispose(); }
                if (oTimer != null)
                {
                    oTimer.Tick -= new System.EventHandler(this.oTimer_Tick);
                    oTimer.Dispose();
                    oTimer = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void chkCompany_CheckedChanged(object sender, EventArgs e)
        {

            if (chkCompany.Checked == true && cmbRelationShip.Text.Trim().ToLower() != "Self".ToLower())
            {
                txtSubFName.Visible = false;
                txtSubMName.Visible = false;
                txtSubLName.Visible = false;
                txtCompanyName.Visible = true;
                label34.Visible = false;
                label11.Visible = false;
                gbPAGender.Visible = false;
                label14.Visible = false;
                lblDOB.Visible = false;                
                mskInsurancePhone.Location = new Point(gbPAGender.Location.X, gbPAGender.Location.Y);
                lblInsuPhone.Location = new Point(label34.Location.X-50, label34.Location.Y-10);
                mtxtDOB.Visible = false;
            }

            else
            {
                txtSubFName.Visible = true;
                txtSubMName.Visible = true;
                txtSubLName.Visible = true;
                txtCompanyName.Visible = false;
                label34.Visible = true;
                label11.Visible = true;
                gbPAGender.Visible = true;
                label14.Visible = true;
                lblDOB.Visible = true;
                lblInsuPhone.Visible = true;               
                mtxtDOB.Visible = true;
                mskInsurancePhone.Location = new Point(SubPhoneX, SubPhoneY);
                lblInsuPhone.Location = new Point(nSubPhoneX,nSubPhoneY);

            }
        }

        string ClipBoardData = string.Empty;
        private void txtSubscriberID_MouseDown(object sender, MouseEventArgs e)
        {
            bool isMatch = false;
            try
            {
                IDataObject iData = Clipboard.GetDataObject();

                if (iData.GetDataPresent(DataFormats.Text))
                {
                    if (ClipBoardData.ToUpper() != (String)iData.GetData(DataFormats.Text))
                    {
                        ClipBoardData = (String)iData.GetData(DataFormats.Text);
                    }
                    Regex objAlphaPattern = new Regex(@"^[a-zA-Z0-9]*$");
                    isMatch = objAlphaPattern.IsMatch(ClipBoardData);
                    if (isMatch)
                    {
                        if (bIsCapitalizeInsID == true)
                        {
                            Clipboard.SetText((String)iData.GetData(DataFormats.Text).ToString().ToUpper());
                        }
                    }
                    else
                    {
                        Clipboard.Clear();
                    }

                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void txtSubscriberID_Leave(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(ClipBoardData);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            
        }

        private void txtSubLName_Validated(object sender, EventArgs e)
        {
            if (cmbRelationShip.Text.ToLower() == "Self".ToLower())
            {

                if (txtSubFName.Text == "" || txtSubLName.Text == "")
                {
                    return;
                }
                if (txtSubFName.Text != _PatientFName || txtSubMName.Text != _PatientMName || txtSubLName.Text != _PatientLName)
                {
                    this.chkSameAsPatient.CheckedChanged -= new System.EventHandler(this.chkSameAsPatient_CheckedChanged);
                    chkSameAsPatient.Checked = false;
                    _isSubscriberDataChanged = true;
                    this.chkSameAsPatient.CheckedChanged += new System.EventHandler(this.chkSameAsPatient_CheckedChanged);
                }
                else
                {
                    this.chkSameAsPatient.CheckedChanged -= new System.EventHandler(this.chkSameAsPatient_CheckedChanged);
                    chkSameAsPatient.Checked = true;
                    _isSubscriberDataChanged = false;
                    this.chkSameAsPatient.CheckedChanged += new System.EventHandler(this.chkSameAsPatient_CheckedChanged);
                }

            }
        }
    } //end UserControl PatientInsurance



}//end namespace gloPatient
