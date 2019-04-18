using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using gloAddress;
using System.Text.RegularExpressions;

namespace gloCardScanning
{
    public partial class frmScanCard_New : Form
    {

        #region " Variable Declarations "

        private CardScanType _CardScanType = CardScanType.None;
        string _databaseconnectionstring = "";
        string _MessageBoxCaption = String.Empty;

        public string _ErrorMessage = "";
        Int64 _PatientID;
        string _sMode;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 1;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private bool isSaveAndClose = false;       
        public bool oDialogResult = false;
        private bool oValidate = false;
        string[] sMembername;
        private string _Country = "";
        private ComboBox combo;
     //   private string _TempProcessDirPath = "";
        private gloListControl.gloListControl oListControl;       
        private static bool _isDoubleClicked = false;
        private float _ColorScheme = 0;
        gloDrivingLicenseCardScanWithOCR ogloDrivingLicenseCardScanWithOCR = null;
        gloInsuranceCardScanWithOCR ogloInsuranceCardScanWithOCR = null;
        #endregion " Declarations existing "

        #region "Property Procedures"

        #region "ID Card Properties"

        public String PatientCode { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public Int64 DOB { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String Zip { get; set; }
        public String State { get; set; }
        public String County { get; set; }
        public String CountryShort { get; set; }
        public String Sex { get; set; }
        public Image PhotoImage { get; set; }
        public Image cardFrontImage { get; set; }
        public Image cardBackImage { get; set; }

        #endregion

        #region "Insurance Properties"

        //Insuance Property
        public String MemberName { get; set; }
        public String PlanProvider { get; set; }
        public String ContactID { get; set; }
        public String MemberID { get; set; }
        public String GroupNo { get; set; }
        public String Relation { get; set; }
        public String FName { get; set; }
        public String MName { get; set; }
        public String LName { get; set; }
        public bool IsInsModify { get; set; }
        #endregion


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion "Property Procedures"

        #region " Constructor "

        public frmScanCard_New(Int64 PatientID, string databaseconnectionstring, string sMode)
        {
            InitializeComponent();

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

            #region "Retrieve UserId"
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
            #endregion ""

            _PatientID = PatientID;

            _databaseconnectionstring = databaseconnectionstring;
            _sMode = sMode;

            if (_PatientID <= 0)
            {
                tsb_Cheque.Enabled = false;

            }
        }

        #endregion " Constructor "

        #region " Form Load Event "

        private void frmCardScanning_Load(object sender, EventArgs e)
        {
            try
            {

                if (_sMode == "New")
                {
                    this.Icon = global::gloCardScanning.Properties.Resources.Patient_scan_card;
                    _CardScanType = CardScanType.NewDrivingLicense;
                    txtNewCode.Text = PatientCode;
                    //Fill state DropDown.(Insurance)
                    //cmbNew_Country.DropDownStyle = ComboBoxStyle.DropDownList;
                    //cmbNew_State.DropDownStyle = ComboBoxStyle.DropDown;

                    FillStates(cmbNew_State);
                    fillCountry(cmbNew_Country);
                    if (cmbNew_Country.Items.Count > 0)
                    {
                        cmbNew_Country.SelectedIndex = 0;
                    }

                    tsb_InsuranceCard.Visible = false;
                    PnlNewPatScan.Visible = true;
                    pnlLicenseDetails.Visible = false;
                    pnlLicenseDetails.Visible = false;
                    pnlInsuranceDetails.Visible = false;

                    this.Width = 819;
                    this.Height = 617;

                    txtNew_FirstName.Focus();
                    txtNew_FirstName.Select();
                }
                else if (_sMode == "Modify")
                {

                    this.Icon = global::gloCardScanning.Properties.Resources.Patient_scan_card;
                    _CardScanType = CardScanType.DrivingLicense;
                    //Fill state DropDown.(Driving Licence)

                    FillStates(cmbLicState);//To fill the previous scanned State combo box
                    FillStates(cmbNewState);//To fill the new scanned State combo box
                    fillCountry(cmbLicCountry);
                    //cmbNew_Country.SelectedIndexChanged -= cmbNew_Country_SelectedIndexChanged;
                    fillCountry(cmbNewCountry);

                    if (cmbNewCountry.Items.Count > 0)
                    {
                        cmbNewCountry.SelectedIndex = 0;
                    }

                    tsb_InsuranceCard.Visible = false;
                    pnlLicenseDetails.Visible = true;
                    PnlNewPatScan.Visible = false;
                    pnlInsuranceDetails.Visible = false;

                    txtLic_PatientCode.Text = PatientCode;
                    txt_NewPatientcode.Text = PatientCode;
                    txtLicLastName.Text = LastName;
                    txtLicFirstName.Text = FirstName;
                    txtLicMiddleName.Text = MiddleName;
                    txtLicDOB.Text = gloDateMaster.gloDate.DateAsDateString(DOB);
                    txtLicAddress.Text = Address1;
                    txtLicAddress2.Text = Address2;
                    txtLicCity.Text = City;
                    txtLic_County.Text = County;
                    txtLicZip.Text = Zip;
                    cmbLicGender.Text = Sex;
                    cmbLicState.Text = State;
                    cmbLicCountry.Text = CountryShort;
                    //cmbNew_Country.SelectedIndexChanged += cmbNew_Country_SelectedIndexChanged;
                    //SLR30:
                    if (pbFaceImage.Image != null)
                    {
                        pbFaceImage.Image.Dispose();
                        pbFaceImage.Image = null;
                    }
                    if (PhotoImage != null)
                    {
                        pbFaceImage.Image = (Image)(PhotoImage.Clone());
                    }
                    //SLR30:
                    else
                    {
                        pbFaceImage.Image = null;
                    }
                    chk_Patientname.Checked = true;
                    chk_DOB.Checked = true;
                    chk_Address.Checked = true;
                    chk_Sex.Checked = true;
                    chk_Photo.Checked = true;


                    this.Width = 819;
                    this.Height = 734;

                    //To set the tab index from the photo selection check box
                    chk_Photo.Focus();
                    chk_Photo.Select();

                }
                else if (_sMode == "Insurance")
                {
                    this.Icon = global::gloCardScanning.Properties.Resources.Insurance_Card_scan;
                    _CardScanType = CardScanType.InsuranceCard;
                    this.Text = "Scan Insurance Card";


                    tsb_DriversLicense.Visible = false;
                    tsb_InsuranceCard.Visible = true;
                    pnlLicenseDetails.Visible = false;
                    PnlNewPatScan.Visible = false;
                    pnlInsuranceDetails.Visible = true;


                    pnlMain.Width = 813;
                    pnlMain.Height = 203;
                    this.Width = 819;
                    this.Height = 542;

                    //Bug #44826: 00000378 : Claims process
                    // If in modify state Fill insurance Name as shown in insurance screen
                    // and disable combo and brows button
                    if (IsInsModify)
                    {
                        FillInsuranceName(PlanProvider);
                        cmbIns_PlanProvider.Enabled = false;
                        btnInsBrowse.Enabled = false;
                    }                  
                    cmbIns_PlanProvider.Focus();
                    cmbIns_PlanProvider.Select();

                }
                try
                {
                    gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Check Network Dir/File Exists : " + ex.ToString(),false);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }

        }

        #endregion

        #region " Card Tool Strip Menu Item Click "

        private void tsb_InsuranceCard_Click(object sender, EventArgs e)
        {
            try
            {
                ts_Commands.Enabled = false; //Disable ToolStrip at the time of Scanning.

                if (_isDoubleClicked == false)
                {
                    _isDoubleClicked = true;

                    pnlMain.Width = 813;
                    pnlMain.Height = 203;

                    this.Width = 819;
                    this.Height = 542;

                    cmbIns_PlanProvider.Focus();
                    cmbIns_PlanProvider.Select();
                    if (pb_FrontSide.Image != null || pb_BackSide.Image != null || pbFaceImage.Image != null)
                    {
                        DialogResult _DlgClearData = DialogResult.None;
                        _DlgClearData = MessageBox.Show("Do you want to clear data ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_DlgClearData == DialogResult.Yes)
                        {
                            //Clearing Existing data.
                            ogloInsuranceCardScanWithOCR = null; 
                            ClearData();
                        }
                        else
                        {
                            _isDoubleClicked = false;
                            return;
                        }

                    }
                    //SLR : Change here:
                    else
                    {
                        ogloInsuranceCardScanWithOCR = null;
                        ClearData();
                    }
                    this.Cursor = Cursors.WaitCursor;
                    _CardScanType = CardScanType.InsuranceCard;
                    ogloInsuranceCardScanWithOCR = new gloInsuranceCardScanWithOCR();
                    ogloInsuranceCardScanWithOCR.ScanInsuranceCardWithOCR();
                    _ColorScheme = ogloInsuranceCardScanWithOCR.ColorScheme;
                    if (ogloInsuranceCardScanWithOCR != null)
                    {
                        ReadScannedData(ogloInsuranceCardScanWithOCR.CardFrontImagePath, ogloInsuranceCardScanWithOCR.CardFaceImagePath, ogloInsuranceCardScanWithOCR.CardBackImagePath);
                    }                    
                    cmbIns_PlanProvider.Focus();
                    _isDoubleClicked = false;
                }

            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                _isDoubleClicked = false;
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (IOException ex)
            {
                _isDoubleClicked = false;
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {
                _isDoubleClicked = false;
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                ts_Commands.Enabled = true;
                this.Cursor = Cursors.Default;
            }

        }

        private void tsb_DriversLicense_Click(object sender, EventArgs e)
        {
            try
            {
                ts_Commands.Enabled = false; //Disable ToolStrip at the time of Scanning.
                if (pb_FrontSide.Image != null || pb_BackSide.Image != null || PicNew.Image != null)
                {
                    DialogResult _DlgClearData = DialogResult.None;
                    _DlgClearData = MessageBox.Show("Do you want to clear data ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_DlgClearData == DialogResult.Yes)
                    {
                        ogloDrivingLicenseCardScanWithOCR = null; 
                        ClearData();

                    }
                    else
                    {
                        return;
                    }
                }               
                else
                {
                    ogloDrivingLicenseCardScanWithOCR = null; 
                    ClearData();
                }

                if (_sMode == "New")
                {
                    if (_isDoubleClicked == false)
                    {
                        _isDoubleClicked = true;
                        this.Width = 819;
                        this.Height = 617;
                        this.Cursor = Cursors.WaitCursor;
                        _CardScanType = CardScanType.NewDrivingLicense;
                         ogloDrivingLicenseCardScanWithOCR = new gloDrivingLicenseCardScanWithOCR();
                        ogloDrivingLicenseCardScanWithOCR.ScanDrivingLicenceWithOCR();
                        _ColorScheme = ogloDrivingLicenseCardScanWithOCR.ColorScheme;
                        if (ogloDrivingLicenseCardScanWithOCR != null)
                        {
                            ReadScannedData(ogloDrivingLicenseCardScanWithOCR.CardFrontImagePath, ogloDrivingLicenseCardScanWithOCR.CardFaceImagePath, ogloDrivingLicenseCardScanWithOCR.CardBackImagePath);
                        }
                          _isDoubleClicked = false;
                       
                        ts_Commands.Enabled = true; //Enable toolStrip
                    }
                }
                else if (_sMode == "Modify")
                {
                    if (_isDoubleClicked == false)
                    {
                        _isDoubleClicked = true;
                        this.Width = 819;
                        this.Height = 734;
                        this.Cursor = Cursors.WaitCursor;
                        _CardScanType = CardScanType.DrivingLicense;
                         ogloDrivingLicenseCardScanWithOCR = new gloDrivingLicenseCardScanWithOCR();
                         ogloDrivingLicenseCardScanWithOCR.ScanDrivingLicenceWithOCR();                        
                         _ColorScheme = ogloDrivingLicenseCardScanWithOCR.ColorScheme;
                         if (ogloDrivingLicenseCardScanWithOCR != null)
                         {
                             ReadScannedData(ogloDrivingLicenseCardScanWithOCR.CardFrontImagePath, ogloDrivingLicenseCardScanWithOCR.CardFaceImagePath, ogloDrivingLicenseCardScanWithOCR.CardBackImagePath);
                         }                                               
                        _isDoubleClicked = false;
                    }
                }

            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                _isDoubleClicked = false;
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (IOException ex)
            {
                _isDoubleClicked = false;
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {
                _isDoubleClicked = false;
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                ts_Commands.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void tsb_ClearData_Click(object sender, EventArgs e)
        {
            try
            {
                if (_CardScanType == CardScanType.InsuranceCard)
                {
                    if (pb_FrontSide.Image != null
                         || txtIns_GroupNo.Text != ""

                        )
                    {
                        DialogResult DlgRst = DialogResult.None;
                        DlgRst = MessageBox.Show("Are you sure you want to clear data ? ", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (DlgRst == DialogResult.OK)
                        { ClearData(); }
                    }
                    //SLR: Change here:
                    else
                    {
                        ClearData();
                    }
                }

                if (_CardScanType == CardScanType.DrivingLicense)
                {
                    if (pb_FrontSide.Image != null ||
                            txt_NewFname.Text != "" ||
                            txt_NewMIname.Text != "" ||
                            txt_NewLastname.Text != "" ||
                            txt_NewAddress1.Text != "" ||
                            txt_NewAddress2.Text != "" ||
                            txt_NewCity.Text != "" ||
                            cmbNewState.Text != "" ||
                            txt_NewZip.Text != "" ||
                            txtNew_County.Text != ""
                        )
                    {
                        DialogResult DlgRst = DialogResult.None;
                        DlgRst = MessageBox.Show("Are you sure you want to clear data ? ", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (DlgRst == DialogResult.OK)
                        { ClearData(); }
                    }
                    //SLR: Change Here
                    else
                    {
                        ClearData();
                    }
                }
                if (_CardScanType == CardScanType.NewDrivingLicense)
                {
                    if (pb_FrontSide.Image != null ||
                        txtNew_FirstName.Text != "" ||
                        txtNew_LastName.Text != "" ||
                        txtNew_MIName.Text != "" ||
                        txtNew_Address1.Text != "" ||
                        txtNew_Address2.Text != "" ||
                        txtNew_City.Text != "" ||
                        txtNew_Zip.Text != "" ||
                        cmbNew_State.Text != "" ||
                        txtNew_County.Text != "" ||
                        cmbNew_Country.Text != "" ||
                        cmbNew_Gender.Text != "" ||
                        Pic_NewPatient.Image != null
                        )
                    {
                        DialogResult DlgRst = DialogResult.None;
                        DlgRst = MessageBox.Show("Are you sure you want to clear data ? ", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (DlgRst == DialogResult.OK)
                        { ClearData(); }
                    }
                    //SLR: Changehere
                    else
                    {
                        ClearData();
                    }
                }

            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (IOException ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (_isDoubleClicked == false)
            {
                _isDoubleClicked = true;

                isSaveAndClose = true;
                //Save Data in Objects & validate the Fields.
                if (SaveData() == true)
                {
                    oDialogResult = true;
                    this.Close();

                }
                else
                {
                    oDialogResult = false;
                }
                _isDoubleClicked = false;
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        #endregion " Card Tool Strip Menu Item Click "

        #region " Private & Public Methods "       
        // Calibrate the scanner,checking Scanner Settings For insurance card & Driving Licence. 
        private void ReadScannedData(string CardFrontImageName, string FaceImageName, string CardBackImageName)
        {

            try
            {
                 this.Cursor = Cursors.WaitCursor;
                 
                 
                if (_CardScanType == CardScanType.DrivingLicense)
                {

                  #region "1.Get Face Image"
                    if (File.Exists(FaceImageName) == true && ogloDrivingLicenseCardScanWithOCR.bIsScanDrivingLicenceWithOCR)
                    {
                        int nHeight = 0;
                        int nWidth = 0;                       
                        Image _faceImage = null;
                        if (_ColorScheme == 1)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _faceImage = (Image)oCardScan.ConvertToGrayScale(FaceImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else if (_ColorScheme == 0)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _faceImage = (Image)oCardScan.ConvertToBlacknWhite(FaceImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else
                        {
                            _faceImage = (Image)gloCardScanning.ImageFromFile(FaceImageName).Clone();
                        }
                        if (_faceImage != null)
                        {
                            nHeight = _faceImage.Height;
                            nWidth = _faceImage.Width;
                            if ((nHeight > 0) && (nWidth > 0))
                            {
                                double xWidth = (double)121 / (double)nWidth;
                                double yHeight = (double)132 / (double)nHeight;
                                double zScale = 1.0;
                                if (zScale > xWidth) zScale = xWidth;
                                if (zScale > yHeight) zScale = yHeight;
                                if (zScale < 1.0)
                                {
                                    nWidth = (int)((double)nWidth * zScale);
                                    nHeight = (int)((double)nHeight * zScale);
                                }

                            }                           
                            if (PicNew.Image != null)
                            {
                                PicNew.Image.Dispose();
                                PicNew.Image = null;
                            }
                            PicNew.Image = new Bitmap(_faceImage, new Size(nWidth, nHeight));                           
                            if (_faceImage != null)
                            {
                                _faceImage.Dispose();
                                _faceImage = null;
                            }
                        }
                    }

                #endregion "Get Face Image"

                  #region ".2 Card Front Side Image "

                    if (File.Exists(CardFrontImageName) == true && ogloDrivingLicenseCardScanWithOCR.bIsScanDrivingLicenceImage)
                    {
                        Image _FrntImage = null;
                        if (_ColorScheme == 1)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _FrntImage = (Image)oCardScan.ConvertToGrayScale(CardFrontImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else if (_ColorScheme == 0)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _FrntImage = (Image)oCardScan.ConvertToBlacknWhite(CardFrontImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else
                        {
                            _FrntImage = gloCardScanning.ImageFromFile(CardFrontImageName);
                        }
                        if (_FrntImage != null)
                        {
                            //SLR30:
                            if (pb_FrontSide.Image != null)
                            {
                                pb_FrontSide.Image.Dispose();
                                pb_FrontSide.Image = null;
                            }

                            // pb_FrontSide.Image = new Bitmap(_FrntImage);
                            int nHeight = _FrntImage.Height;
                            int nWidth = _FrntImage.Width;

                            if ((nHeight > 0) && (nWidth > 0))
                            {
                                double xWidth = (double)360 / (double)nWidth;
                                double yHeight = (double)220 / (double)nHeight;
                                double zScale = 1.0;
                                if (zScale > xWidth) zScale = xWidth;
                                if (zScale > yHeight) zScale = yHeight;
                                if (zScale < 1.0)
                                {
                                    nWidth = (int)((double)nWidth * zScale);
                                    nHeight = (int)((double)nHeight * zScale);
                                }

                            }

                            pb_FrontSide.Image = new Bitmap(_FrntImage, new Size(nWidth, nHeight));
                            if (_FrntImage != null)
                            {
                                _FrntImage.Dispose();
                                _FrntImage = null;
                            }
                        }
                    }

                    #endregion

                  #region ".3 Card Back Side Image "

                    if (File.Exists(CardBackImageName) == true && ogloDrivingLicenseCardScanWithOCR.bIsScanDrivingLicenceImage)
                    {
                        Image _BckImage = null;
                        if (_ColorScheme == 1)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _BckImage = (Image)oCardScan.ConvertToGrayScale(CardBackImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else if (_ColorScheme == 0)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _BckImage = (Image)oCardScan.ConvertToBlacknWhite(CardBackImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else
                        {
                            _BckImage = gloCardScanning.ImageFromFile(CardBackImageName);
                        }

                        if (_BckImage != null)
                        {
                          
                            if (pb_BackSide.Image != null)
                            {
                                pb_BackSide.Image.Dispose();
                                pb_BackSide.Image = null;
                            }
                           
                            int nHeight = _BckImage.Height;
                            int nWidth = _BckImage.Width;

                            if ((nHeight > 0) && (nWidth > 0))
                            {
                                double xWidth = (double)360 / (double)nWidth;
                                double yHeight = (double)220 / (double)nHeight;
                                double zScale = 1.0;
                                if (zScale > xWidth) zScale = xWidth;
                                if (zScale > yHeight) zScale = yHeight;
                                if (zScale < 1.0)
                                {
                                    nWidth = (int)((double)nWidth * zScale);
                                    nHeight = (int)((double)nHeight * zScale);
                                }
                            }
                            pb_BackSide.Image = new Bitmap(_BckImage, new Size(nWidth, nHeight));
                            if (_BckImage != null)
                            {
                                _BckImage.Dispose();
                                _BckImage = null;
                            }
                        }
                    }

                    #endregion

                  #region ".4 Setting Patients Scanned Information to PatientObject "

                if (ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData != null && ogloDrivingLicenseCardScanWithOCR.bIsScanDrivingLicenceWithOCR)
                {
                    //SLR30:
                    this.txt_NewZip.TextChanged -= txtZip_TextChanged;
                    txt_NewFname.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.NameFirst;
                    txt_NewMIname.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.NameMiddle;
                    txt_NewLastname.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.NameLast;

                    //Validate Date.
                    if (gloDateMaster.gloDate.IsValidDate(ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.DateOfBirth) != false)
                    {
                        DateTime _dtDOB = Convert.ToDateTime(ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.DateOfBirth);
                        txt_NewDOB.Text = _dtDOB.ToString("MM/dd/yyyy");
                    }
                    if (ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Sex.Trim() == "M")
                    { cmbNewGender.Text = "Male"; }
                    else if (ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Sex.Trim() == "F")
                    { cmbNewGender.Text = "Female"; }
                    else if (ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Sex.Trim() == "")
                    { cmbNewGender.Text = "Other"; }                  
                    else
                    {
                        cmbNewGender.Text = "";
                    }
                    txt_NewCity.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.City;
                    cmbNewState.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.State;                   
                    //Incident #00001777 
                    //If Zip code length is greater than 5 digits then take only first 5 digits as a zip code.
                    if (!string.IsNullOrEmpty(ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Zip) && ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Zip.Length > 5)
                    {
                        txt_NewZip.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Zip.Substring(0, 5);
                    }
                    else
                    {
                        txt_NewZip.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Zip;
                    }

                    txt_NewAddress1.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Address;
                    cmbNewCountry.Text = "US";

                    this.txt_NewZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
                }
                  #endregion

                }

                else if (_CardScanType == CardScanType.InsuranceCard)
                {
                    #region ".1 Card Front Side Image "

                    if (File.Exists(CardFrontImageName) == true && ogloInsuranceCardScanWithOCR.bIsInsuranceCardScanImage)
                    //{ pb_FrontSide.Image = Image.FromFile(CardFrontImageName); }
                    {
                        // Image _FrntImage = gloCardScanning.ImageFromFile(CardFrontImageName);
                        Image _FrntImage = null;
                        if (_ColorScheme == 1)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _FrntImage = (Image)oCardScan.ConvertToGrayScale(CardFrontImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else if (_ColorScheme == 0)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _FrntImage = (Image)oCardScan.ConvertToBlacknWhite(CardFrontImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else
                        {
                            _FrntImage = gloCardScanning.ImageFromFile(CardFrontImageName);
                        }

                        if (_FrntImage != null)
                        {
                            //SLR30:
                            if (pb_FrontSide.Image != null)
                            {
                                pb_FrontSide.Image.Dispose();
                                pb_FrontSide.Image = null;
                            }

                            int nHeight = _FrntImage.Height;
                            int nWidth = _FrntImage.Width;

                            if ((nHeight > 0) && (nWidth > 0))
                            {
                                double xWidth = (double)360 / (double)nWidth;
                                double yHeight = (double)220 / (double)nHeight;
                                double zScale = 1.0;
                                if (zScale > xWidth) zScale = xWidth;
                                if (zScale > yHeight) zScale = yHeight;
                                if (zScale < 1.0)
                                {
                                    nWidth = (int)((double)nWidth * zScale);
                                    nHeight = (int)((double)nHeight * zScale);
                                }


                            }


                            pb_FrontSide.Image = new Bitmap(_FrntImage, new Size(nWidth, nHeight));

                            if (_FrntImage != null)
                            {
                                _FrntImage.Dispose();
                                _FrntImage = null;
                            }
                        }
                    }

                    #endregion

                    #region ".2 Card Back Side Image "

                    if (File.Exists(CardBackImageName) == true && ogloInsuranceCardScanWithOCR.bIsInsuranceCardScanImage)
                    //{ pb_BackSide.Image = Image.FromFile(CardBackImageName); }
                    {
                        // Image _BckImage = gloCardScanning.ImageFromFile(CardBackImageName);

                        Image _BckImage = null;
                        if (_ColorScheme == 1)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _BckImage = (Image)oCardScan.ConvertToGrayScale(CardBackImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else if (_ColorScheme == 0)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _BckImage = (Image)oCardScan.ConvertToBlacknWhite(CardBackImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else
                        {
                            _BckImage = gloCardScanning.ImageFromFile(CardBackImageName);
                        }

                        if (_BckImage != null)
                        {
                            //SLR30:
                            if (pb_BackSide.Image != null)
                            {
                                pb_BackSide.Image.Dispose();
                                pb_BackSide.Image = null;
                            }

                            int nHeight = _BckImage.Height;
                            int nWidth = _BckImage.Width;

                            if ((nHeight > 0) && (nWidth > 0))
                            {
                                double xWidth = (double)360 / (double)nWidth;
                                double yHeight = (double)220 / (double)nHeight;
                                double zScale = 1.0;
                                if (zScale > xWidth) zScale = xWidth;
                                if (zScale > yHeight) zScale = yHeight;
                                if (zScale < 1.0)
                                {
                                    nWidth = (int)((double)nWidth * zScale);
                                    nHeight = (int)((double)nHeight * zScale);
                                }


                            }

                            pb_BackSide.Image = new Bitmap(_BckImage, new Size(nWidth, nHeight));

                            if (_BckImage != null)
                            {
                                _BckImage.Dispose();
                                _BckImage = null;
                            }
                        }
                    }

                    #endregion

                    
                    if (ogloInsuranceCardScanWithOCR.gloInsuranceCardData != null && ogloInsuranceCardScanWithOCR.bIsInsuranceCardScanWithOCR)
                    {
                        #region " .Retrive Images for Insurance "
                       
                        #endregion

                        #region " .Setting Patients Scaned Information to PatientObject"

                        // txtIns_PayerID.Text = NmedSdkCom.propPayerID;
                        if (ogloInsuranceCardScanWithOCR.gloInsuranceCardData != null)
                        {
                            //auto complete 
                            FillInsurance(ogloInsuranceCardScanWithOCR.gloInsuranceCardData.PlanProvider);
                        }

                        if (ogloInsuranceCardScanWithOCR.gloInsuranceCardData != null)
                        {
                            //Bug #44826: 00000378 : Claims process
                            // If in modify state no need to update Name and Contact ID
                            if (!IsInsModify)
                            {
                                //Bug #45640: EMR - Modify Patient - Modify Insurance -After Scan the Scan Card It give exception.
                                cmbIns_PlanProvider.Items.Clear();
                                FillInsuranceName(ogloInsuranceCardScanWithOCR.gloInsuranceCardData.PlanProvider);
                            }
                            txtIns_MemberID.Text = ogloInsuranceCardScanWithOCR.gloInsuranceCardData.InsMemberID.Trim();
                            txtIns_MemberName.Text = ogloInsuranceCardScanWithOCR.gloInsuranceCardData.InsMemberName;
                            txtIns_GroupNo.Text = ogloInsuranceCardScanWithOCR.gloInsuranceCardData.GroupNumber;
                            cmbIns_PlanProvider.Focus();
                           
                        }                       

                        #endregion "Setting Patients Sacnned Information to PatientObject"
                    }
                }
                else if (_CardScanType == CardScanType.NewDrivingLicense) //for new Pat. reg.
                {
                   
                    #region " .Retrive Images for Drivers License "

                  
                    #region ".1 Face Image "

                    if (File.Exists(FaceImageName) == true && ogloDrivingLicenseCardScanWithOCR.bIsScanDrivingLicenceWithOCR)
                    {
                        int nHeight = 0;
                        int nWidth = 0;

                        // Image _faceImage = gloCardScanning.ImageFromFile(FaceImageName);
                        Image _faceImage = null;
                        if (_ColorScheme == 1)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _faceImage = (Image)oCardScan.ConvertToGrayScale(FaceImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else
                        {
                            _faceImage = (Image)gloCardScanning.ImageFromFile(FaceImageName).Clone();
                        }

                        if (_faceImage != null)
                        {
                            nHeight = _faceImage.Height;
                            nWidth = _faceImage.Width;
                            //if (nWidth > 140) { nWidth = 140; }
                            //if (nHeight > 150) { nHeight = 150; }
                            //SLR: Wrong Wrong Wrong

                            //     if (nWidth > 121) { nWidth = 121; }
                            //   if (nHeight > 132) { nHeight = 132; }
                            if ((nHeight > 0) && (nWidth > 0))
                            {
                                double xWidth = (double)121 / (double)nWidth;
                                double yHeight = (double)132 / (double)nHeight;
                                double zScale = 1.0;
                                if (zScale > xWidth) zScale = xWidth;
                                if (zScale > yHeight) zScale = yHeight;
                                if (zScale < 1.0)
                                {
                                    nWidth = (int)((double)nWidth * zScale);
                                    nHeight = (int)((double)nHeight * zScale);
                                }

                                // if (nWidth > 121) { nWidth = 121; }
                                //if (nHeight > 132) { nHeight = 132; }
                            }

                            //SLR30:
                            if (Pic_NewPatient.Image != null)
                            {
                                Pic_NewPatient.Image.Dispose();
                                Pic_NewPatient.Image = null;
                            }
                            Pic_NewPatient.Image = new Bitmap(_faceImage, new Size(nWidth, nHeight));

                            if (_faceImage != null)
                            {
                                _faceImage.Dispose();
                                //SLR30:
                                _faceImage = null;
                            }
                        }
                    }

                    #endregion

                    #region ".2 Card Front Side Image "

                    if (File.Exists(CardFrontImageName) == true && ogloDrivingLicenseCardScanWithOCR.bIsScanDrivingLicenceImage)
                    //{ pb_FrontSide.Image = Image.FromFile(CardFrontImageName); }
                    {
                        // Image _FrntImage = gloCardScanning.ImageFromFile(CardFrontImageName);
                        Image _FrntImage = null;
                        if (_ColorScheme == 1)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _FrntImage = (Image)oCardScan.ConvertToGrayScale(CardFrontImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else
                        {
                            _FrntImage = gloCardScanning.ImageFromFile(CardFrontImageName);
                        }

                        if (_FrntImage != null)
                        {

                            if (pb_FrontSide.Image != null)
                            {
                                pb_FrontSide.Image.Dispose();
                                pb_FrontSide.Image = null;
                            }

                            int nHeight = _FrntImage.Height;
                            int nWidth = _FrntImage.Width;

                            if ((nHeight > 0) && (nWidth > 0))
                            {
                                double xWidth = (double)360 / (double)nWidth;
                                double yHeight = (double)220 / (double)nHeight;
                                double zScale = 1.0;
                                if (zScale > xWidth) zScale = xWidth;
                                if (zScale > yHeight) zScale = yHeight;
                                if (zScale < 1.0)
                                {
                                    nWidth = (int)((double)nWidth * zScale);
                                    nHeight = (int)((double)nHeight * zScale);
                                }


                            }

                            pb_FrontSide.Image = new Bitmap(_FrntImage, new Size(nWidth, nHeight));

                            if (_FrntImage != null)
                            {
                                _FrntImage.Dispose();
                                //SLR30:
                                _FrntImage = null;
                            }
                        }
                    }

                    #endregion

                    #region ".3 Card Back Side Image "

                    if (File.Exists(CardBackImageName) == true && ogloDrivingLicenseCardScanWithOCR.bIsScanDrivingLicenceImage)
                    // { pb_BackSide.Image = Image.FromFile(CardBackImageName); }
                    {
                        // Image _BckImage = gloCardScanning.ImageFromFile(CardBackImageName);
                        Image _BckImage = null;
                        if (_ColorScheme == 1)
                        {
                            gloCardScanning oCardScan = new gloCardScanning(_databaseconnectionstring);
                            _BckImage = (Image)oCardScan.ConvertToGrayScale(CardBackImageName).Clone();
                            if (oCardScan != null) { oCardScan.Dispose(); oCardScan = null; }
                        }
                        else
                        {
                            _BckImage = gloCardScanning.ImageFromFile(CardBackImageName);
                        }

                        if (_BckImage != null)
                        {


                            if (pb_BackSide.Image != null)
                            {
                                pb_BackSide.Image.Dispose();
                                pb_BackSide.Image = null;
                            }
                            //  pb_BackSide.Image  = new Bitmap(_BckImage);

                            int nHeight = _BckImage.Height;
                            int nWidth = _BckImage.Width;

                            if ((nHeight > 0) && (nWidth > 0))
                            {
                                double xWidth = (double)360 / (double)nWidth;
                                double yHeight = (double)220 / (double)nHeight;
                                double zScale = 1.0;
                                if (zScale > xWidth) zScale = xWidth;
                                if (zScale > yHeight) zScale = yHeight;
                                if (zScale < 1.0)
                                {
                                    nWidth = (int)((double)nWidth * zScale);
                                    nHeight = (int)((double)nHeight * zScale);
                                }


                            }

                            pb_BackSide.Image = new Bitmap(_BckImage, new Size(nWidth, nHeight));

                            if (_BckImage != null)
                            {
                                _BckImage.Dispose();
                                //SLR30:
                                _BckImage = null;
                            }
                        }
                    }

                    #endregion

                    #region ".4 Setting Patients Scanned Information to PatientObject "

                    if (ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData != null && ogloDrivingLicenseCardScanWithOCR.bIsScanDrivingLicenceWithOCR)
                    {
                        //SLR30:
                        this.txtNew_Zip.TextChanged -= this.txtNewZip_TextChanged;
                        txtNew_FirstName.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.NameFirst;
                        txtNew_MIName.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.NameMiddle;
                        txtNew_LastName.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.NameLast;
                        //Validate Date.
                        if (gloDateMaster.gloDate.IsValidDate(ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.DateOfBirth) != false)
                        {
                            //SLR change here
                            DateTime _dtDOB = Convert.ToDateTime(ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.DateOfBirth);
                            txtNew_DOB.Text = _dtDOB.ToString("MM/dd/yyyy");
                        }

                        if (ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Sex.Trim() == "M")
                        { cmbNew_Gender.Text = "Male"; }
                        else if (ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Sex.Trim() == "F")
                        { cmbNew_Gender.Text = "Female"; }
                        else if (ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Sex.Trim() == "")
                        { cmbNew_Gender.Text = "Other"; }
                        else
                        {
                            cmbNew_Gender.Text = "";
                        }

                        txtNew_City.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.City;
                        cmbNew_State.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.State;

                        //Incident #00001777 
                        //If Zip code length is greater than 5 digits then take only first 5 digits as a zip code.
                        if (!string.IsNullOrEmpty(ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Zip) && ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Zip.Length > 5)
                        {
                            txtNew_Zip.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Zip.Substring(0, 5);
                        }
                        else
                        {
                            txtNew_Zip.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Zip;
                        }

                        txtNew_Address1.Text = ogloDrivingLicenseCardScanWithOCR.gloDrivingLicenceData.Address;
                        cmbNew_Country.Text = "US";
                        this.txtNew_Zip.TextChanged += new System.EventHandler(this.txtNewZip_TextChanged);
                    }

                    #endregion "Setting Patients Scanned Information to PatientObject"


                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private bool SaveData() //Save Card Data in Object;
        {
            bool _IsDataSaved = false;
            try
            {

                if (_sMode == "New")
                {

                    FirstName = txtNew_FirstName.Text;
                    LastName = txtNew_LastName.Text;
                    MiddleName = txtNew_MIName.Text;


                    txtNew_DOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (txtNew_DOB.Text != "")
                    {
                        txtNew_DOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        //Validate Date(DOB).
                        if ((IsValidDate(txtNew_DOB.Text)) != false)
                        {

                            //SLR30: Store convert.ToDateTIme..... to a temp variable and then check for temp variable for all comparision: Do this for wherever comparing,,,

                            // if (Convert.ToDateTime(txtNew_DOB.Text).Date >= DateTime.Now.Date)
                            DateTime dtNewDOB = Convert.ToDateTime(txtNew_DOB.Text);

                            // if (Convert.ToDateTime(txtNew_DOB.Text) == DateTime.MinValue || Convert.ToDateTime(txtNew_DOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(txtNew_DOB.Text) < Convert.ToDateTime("01/01/1900"))
                            if (dtNewDOB == DateTime.MinValue || dtNewDOB.Date > DateTime.Now.Date || dtNewDOB < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                oValidate = true;
                                txtNew_DOB.Focus();
                                //SLR30: Is the following correct and Check for all such returns?
                                return _IsDataSaved = false;
                            }
                            else
                            {
                                DOB = gloDateMaster.gloDate.DateAsNumber(txtNew_DOB.Text);

                            }

                        }
                        else
                        {
                            MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            oValidate = true;
                            txtNew_DOB.Focus();
                            //SLR: Is the following correct? and check for all such returns:
                            return _IsDataSaved = false;
                        }
                    }
                    Address1 = txtNew_Address1.Text;
                    Address2 = txtNew_Address2.Text;
                    City = txtNew_City.Text;
                    Zip = txtNew_Zip.Text;
                    State = cmbNew_State.Text;
                    County = txtNew_County.Text;
                    CountryShort = cmbNew_Country.Text;
                    Sex = cmbNew_Gender.Text;
                    //SLR30:
                    if (PhotoImage != null)
                    {
                        PhotoImage.Dispose();
                        PhotoImage = null;
                    }
                    if (Pic_NewPatient.Image != null)
                    {
                        PhotoImage = (Image)(Pic_NewPatient.Image.Clone());
                    }
                    //SLR30:
                    else
                    {
                        PhotoImage = null;
                    }
                    //SLR30:
                    if (cardFrontImage != null)
                    {
                        cardFrontImage.Dispose();
                        cardFrontImage = null;
                    }
                    if (pb_FrontSide.Image != null)
                    {
                        cardFrontImage = (Image)(pb_FrontSide.Image.Clone());
                    }
                    //SLR30:
                    else
                    {
                        cardFrontImage = null;
                    }
                    //SLR30:
                    if (cardBackImage != null)
                    {
                        cardBackImage.Dispose();
                        cardBackImage = null;
                    }
                    if (pb_BackSide.Image != null)
                    {
                        cardBackImage = (Image)(pb_BackSide.Image.Clone());
                    }
                    //SLR30:
                    else
                    {
                        cardBackImage = null;
                    }
                    _IsDataSaved = true;
                }
                else if (_sMode == "Modify")
                {
                    if (chk_Photo.Checked == true)
                    {
                        //SLR30:
                        if (PhotoImage != null)
                        {
                            PhotoImage.Dispose();
                            PhotoImage = null;
                        }
                        if (PicNew.Image != null)
                        {
                            PhotoImage = (Image)(PicNew.Image.Clone());
                        }
                        //SLR30:
                        else
                        {
                            PhotoImage = null;
                        }
                    }
                    else
                    { //SLR30:
                        if (PhotoImage != null)
                        {
                            PhotoImage.Dispose();
                            PhotoImage = null;
                        }
                        if (pbFaceImage.Image != null)
                        {
                            PhotoImage = (Image)(pbFaceImage.Image.Clone());
                        }
                        //SLR30:
                        {
                            PhotoImage = null;
                        }
                    }


                    if (chk_Patientname.Checked == true)
                    {
                        if (txt_NewFname.Text == "")
                        {
                            MessageBox.Show("Enter a first name for the patient. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            oValidate = true;
                            txt_NewFname.Focus();
                            return _IsDataSaved = false;
                        }
                        else if (txt_NewLastname.Text == "")
                        {
                            MessageBox.Show("Enter a last name for the patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            oValidate = true;
                            txt_NewLastname.Focus();
                            return _IsDataSaved = false;
                        }
                        FirstName = txt_NewFname.Text;
                        LastName = txt_NewLastname.Text;
                        MiddleName = txt_NewMIname.Text;
                    }
                    else
                    {
                        FirstName = txtLicFirstName.Text;
                        LastName = txtLicLastName.Text;
                        MiddleName = txtLicMiddleName.Text;
                    }
                    if (chk_DOB.Checked == true)
                    {
                        txt_NewDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (txt_NewDOB.Text == "")
                        {
                            MessageBox.Show("Enter date of birth for patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            oValidate = true;
                            txt_NewDOB.Focus();
                            return _IsDataSaved = false;
                        }

                        txt_NewDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        //Validate Date(DOB).
                        if ((IsValidDate(txt_NewDOB.Text)) != false)
                        {
                            DateTime dtDOBdate = Convert.ToDateTime(txt_NewDOB.Text);

                            // if (Convert.ToDateTime(txt_NewDOB.Text) == DateTime.MinValue || Convert.ToDateTime(txt_NewDOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(txt_NewDOB.Text) < Convert.ToDateTime("01/01/1900"))
                            if (dtDOBdate == DateTime.MinValue || dtDOBdate.Date > DateTime.Now.Date || dtDOBdate < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                oValidate = true;
                                txt_NewDOB.Focus();
                                return _IsDataSaved = false;
                            }
                            else
                            {
                                DOB = gloDateMaster.gloDate.DateAsNumber(txt_NewDOB.Text);

                            }

                        }

                        else
                        {
                            MessageBox.Show("Enter a valid date of birth. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            oValidate = true;
                            txt_NewDOB.Focus();
                            return _IsDataSaved = false;
                        }

                    }
                    else
                    {
                        DOB = gloDateMaster.gloDate.DateAsNumber(txtLicDOB.Text);
                    }
                    if (chk_Address.Checked == true)
                    {

                        Address1 = txt_NewAddress1.Text;
                        Address2 = txt_NewAddress2.Text;
                        City = txt_NewCity.Text;
                        Zip = txt_NewZip.Text;
                        State = cmbNewState.Text;
                        County = txt_NewCounty.Text;
                        CountryShort = cmbNewCountry.Text;

                    }
                    else
                    {

                        Address1 = txtLicAddress.Text;
                        Address2 = txtLicAddress2.Text;
                        City = txtLicCity.Text;
                        Zip = txtLicZip.Text;
                        State = cmbLicState.Text;
                        County = txtLic_County.Text;
                        CountryShort = cmbLicCountry.Text;
                    }
                    if (chk_Sex.Checked == true)
                    {
                        if (cmbNewGender.Text == "")
                        {
                            MessageBox.Show("Select patient gender.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            oValidate = true;
                            cmbNewGender.Focus();
                            return _IsDataSaved = false;
                        }
                        Sex = cmbNewGender.Text;
                    }
                    else
                    {
                        Sex = cmbLicGender.Text;
                    }
                    //cardFrontImage = (Image)(pb_FrontSide.Image.Clone());
                    //cardBackImage = (Image)(pb_BackSide.Image.Clone()); ;
                    //SLR30:
                    if (cardFrontImage != null)
                    {
                        cardFrontImage.Dispose();
                        cardFrontImage = null;
                    }
                    if (pb_FrontSide.Image != null)
                    {
                        cardFrontImage = (Image)(pb_FrontSide.Image.Clone());
                    }
                    //SLR30:
                    else
                    {
                        cardFrontImage = null;
                    }
                    //SLR30:
                    if (cardBackImage != null)
                    {
                        cardBackImage.Dispose();
                        cardBackImage = null;
                    }
                    if (pb_BackSide.Image != null)
                    {
                        cardBackImage = (Image)(pb_BackSide.Image.Clone());
                    }
                    //SLR30:
                    else
                    {
                        cardBackImage = null;
                    }
                    _IsDataSaved = true;

                }
                else if (_sMode == "Insurance")
                {
                    sMembername = txtIns_MemberName.Text.Trim().Split(' ');
                    if (sMembername != null)
                    {

                        if (sMembername.Length == 3)
                        {
                            //SLR30: What about Middle name?
                            if (sMembername[0].ToLower().Trim() == FName.ToLower().Trim() && sMembername[2].ToLower().Trim() == LName.ToLower().Trim())
                            {
                                Relation = "Self";
                            }

                            FName = sMembername[0];
                            MName = sMembername[1];
                            LName = sMembername[2];

                        }
                        else if (sMembername.Length == 2)
                        {
                            if (sMembername[0].ToLower().Trim() == FName.ToLower().Trim() && sMembername[1].ToLower().Trim() == LName.ToLower().Trim())
                            {
                                Relation = "Self";
                            }

                            FName = sMembername[0];
                            MName = "";
                            LName = sMembername[1];


                        }
                        else
                        {
                            if (sMembername[0].ToLower().Trim() == FName.ToLower().Trim())
                            {
                                Relation = "Self";

                            }

                            FName = sMembername[0];
                            MName = "";
                            LName = "";

                        }
                    }

                    if (cmbIns_PlanProvider.SelectedValue != null) { ContactID = cmbIns_PlanProvider.SelectedValue.ToString(); }
                    PlanProvider = cmbIns_PlanProvider.Text;
                    MemberID = txtIns_MemberID.Text;
                    GroupNo = txtIns_GroupNo.Text;
                    //cardFrontImage = (Image)(pb_FrontSide.Image.Clone()); ;
                    //cardBackImage = (Image)(pb_BackSide.Image.Clone()); ;
                    //SLR30:
                    if (cardFrontImage != null)
                    {
                        cardFrontImage.Dispose();
                        cardFrontImage = null;
                    }
                    if (pb_FrontSide.Image != null)
                    {
                        cardFrontImage = (Image)(pb_FrontSide.Image.Clone());
                    }
                    //SLR30:
                    else
                    {
                        cardFrontImage = null;
                    }
                    //SLR30:
                    if (cardBackImage != null)
                    {
                        cardBackImage.Dispose();
                        cardBackImage = null;
                    }
                    if (pb_BackSide.Image != null)
                    {
                        cardBackImage = (Image)(pb_BackSide.Image.Clone());
                    }
                    //SLR30:
                    else
                    {
                        cardBackImage = null;
                    }

                    _IsDataSaved = true;
                }

            }
            catch (Exception ex)
            {
                _isDoubleClicked = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
           

            return _IsDataSaved;
        }

        private void FillInsurance(string InsuranceName)
        {
            //Compress
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtInsurance = null;
            AutoCompleteStringCollection oInsurance = new AutoCompleteStringCollection();
            try
            {
                if (oDB != null)
                {
                    if (oDB.Connect(false))
                    {
                        string _sqlQuery = "select sname from Contacts_MST where SOUNDEX(replace(sname,' ','')) = soundex(replace('" + InsuranceName.Trim().Replace("'", "''") + "',' ','')) and sContactType = 'Insurance' AND ISNULL(bIsBlocked,0) = 0 order by sName";
                        oDB.Retrive_Query(_sqlQuery, out dtInsurance);

                        foreach (DataRow dr in dtInsurance.Rows)
                        {
                            oInsurance.Add(Convert.ToString(dr[0]));
                        }
                        cmbIns_PlanProvider.AutoCompleteCustomSource = oInsurance;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (dtInsurance != null) { dtInsurance.Dispose(); dtInsurance = null; }
                //SLR: Check when is OInsurance Allocated and I feel some issue is here..
                //if (oInsurance != null) { oInsurance.Clear(); oInsurance = null; }
            }
        }

        /*
        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //Commented
                //int y = 3;
                //if (_CardScanType == CardScanType.DrivingLicense || _CardScanType == CardScanType.InsuranceCard)
                //{
                //    if (pb_FrontSide.Image != null)
                //    {
                //        Image logo = Image.FromFile(_CardFrontImagePath);
                //        e.Graphics.DrawImage(logo, new Point(3, y));
                //        logo.Dispose();
                //        y = y + 250;
                //    }
                //    if (pb_BackSide.Image != null)
                //    {
                //        Image logo = Image.FromFile(_CardBackImagePath);
                //        e.Graphics.DrawImage(logo, new Point(3, y));
                //        logo.Dispose();
                //        y = y + 250;
                //    }
                //} //-- Commented
                //Compress
                //else if (_CardScanType == CardScanType.Cheque)
                //{
                //    //
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        */

        private Int64 AddInsuranceToContact(string InsuranceName, string PayerId)
        {
            string _strQuery = "";
            object oContactID = null;
            Int64 _nContactID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                if (oDB != null)
                {

                    if (oDB.Connect(false))
                    {
                        _strQuery = "SELECT nContactID FROM Contacts_MST WHERE UPPER(sName) = '" + InsuranceName.Trim().ToUpper() + "' AND sContactType = 'Insurance' ";
                        oContactID = oDB.ExecuteScalar_Query(_strQuery);

                        if (oContactID != null && oContactID.ToString() != "")
                        {
                            return Convert.ToInt64(oContactID);
                        }

                        oContactID = oDB.ExecuteScalar_Query("SELECT (ISNULL(MAX(nContactID),0)+1) AS nContactID FROM Contacts_MST");

                        _strQuery = " INSERT INTO Contacts_MST(nContactID,sName,sContact,sAddressLine1,sAddressLine2, sCity, sState, sZIP, sPhone, sFax, sEmail, sURL, "
                             + " sMobile,sPager,sNotes,sFirstName, sMiddleName, sLastName, sGender, sContactType,nClinicID, bIsBlocked,sExternalCode) "
                             + " VALUES(" + Convert.ToInt64(oContactID) + ",'" + InsuranceName.Replace("'", "''").Trim() + "','','','','','','','','','','',"
                             + " '','','','','','','','Insurance','" + _ClinicID + "',0,'')";
                        oDB.Execute_Query(_strQuery);

                        _strQuery = "";
                        _strQuery = " INSERT INTO Contacts_Insurance_DTL " +
                        " (nContactID, sInsuranceTypeCode, sInsuranceTypeDesc, sPayerId, bAccessAssignment, bStatementToPatient, bMedigap,  " +
                        " bReferringIDInBox19, bNameOfacilityinBox33, bDoNotPrintFacility, b1stPointer, bBox31Blank, bShowPayment, nTypeOBilling,  " +
                        " nClearingHouse, nClinicID, bIsClaims, bIsRemittanceAdvice, bIsRealTimeEligibility, bIsElectronicCOB,   " +
                        " bIsRealTimeClaimStatus,bIsEnrollmentRequired, sPayerPhone, sWebsite, sServicingState, sComments, sPayerPhoneExtn) " +
                        " VALUES  " +
                        " (" + Convert.ToInt64(oContactID) + ", '','','" + PayerId + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "', " +
                        " '" + false + "','" + false + "','" + false + "','" + false + "',0,0," + _ClinicID + ",'" + false + "','" + false + "','" + false + "', " +
                        " '" + false + "','" + false + "','" + false + "','','','','','') ";
                        oDB.Execute_Query(_strQuery);

                        _nContactID = Convert.ToInt64(oContactID);
                    }//oompress
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _strQuery = null;
                oContactID = null;
            }
            return _nContactID;
        }

        private void ClearData()
        {


            //pb_FrontSide.Image = null;
            //pb_BackSide.Image = null;
            if (pb_FrontSide.Image != null)
            {
                pb_FrontSide.Image.Dispose();
                pb_FrontSide.Image = null;
            }
            if (pb_BackSide.Image != null)
            {
                pb_BackSide.Image.Dispose();
                pb_BackSide.Image = null;
            }

            if (_sMode == "New")
            {

                txtNew_FirstName.Text = "";
                txtNew_LastName.Text = "";
                txtNew_MIName.Text = "";
                txtNew_DOB.Text = "";
                txtNew_Address1.Text = "";
                txtNew_Address2.Text = "";
                txtNew_City.Text = "";
                cmbNew_State.Text = "";
                txtNew_County.Text = "";
                cmbNew_Country.Text = null;
                cmbNew_Gender.Text = "";
                //SLR30:
                this.txtNew_Zip.TextChanged -= this.txtNewZip_TextChanged;
                txtNew_Zip.Text = string.Empty;
                this.txtNew_Zip.TextChanged += new System.EventHandler(this.txtNewZip_TextChanged);
                Pic_NewPatient.Image = null;
                txtNew_FirstName.Focus();
            }
            if (cardFrontImage != null)
            {
                cardFrontImage.Dispose();
                cardFrontImage = null;
            }
            if (cardBackImage != null)
            {
                cardBackImage.Dispose();
                cardBackImage = null;
            }

            //cardBackImage = pb_BackSide.Image;

            //.Insurance Card Data
            if (_sMode == "Insurance")
            {

                //Bug #44826: 00000378 : Claims process
                // If in modify state no need to clear Insurance Name.
                if (!IsInsModify)
                {
                   
                    cmbIns_PlanProvider.DataSource = null;
                    cmbIns_PlanProvider.Items.Clear();
                }
                txtIns_MemberID.Text = "";
                txtIns_MemberName.Text = "";
                txtIns_GroupNo.Text = "";
                lbl_ScanMessage.Visible = false;
            }

            if (_sMode == "Modify")
            {
                //Modify Pat. Data

                txt_NewFname.Text = "";
                txt_NewMIname.Text = "";
                txt_NewLastname.Text = "";
                txt_NewDOB.Text = "";
                cmbNewGender.Text = "";
                txt_NewCity.Text = "";
                cmbNewState.Text = "";
                cmbNewCountry.Text = null;
                txt_NewCounty.Text = "";
                txt_NewAddress1.Text = "";
                txt_NewAddress2.Text = "";
                //SLR30:
                this.txt_NewZip.TextChanged -= txtZip_TextChanged;
                txt_NewZip.Text = "";
                this.txt_NewZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
                //PicNew.Image = null;
                if (PicNew.Image != null)
                {
                    PicNew.Image.Dispose();
                    PicNew.Image = null;
                }

                chk_Address.Checked = true;
                chk_DOB.Checked = true;
                chk_Patientname.Checked = true;
                chk_Sex.Checked = true;
                chk_Photo.Checked = true;
                txt_NewFname.Focus();
            }


        }

        private bool ValidateScannedData()
        {
            bool _IsValid = true;
            try
            {
                if (_CardScanType == CardScanType.DrivingLicense)
                {
                    txtLicDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                    if (pb_FrontSide.Image == null || pb_BackSide.Image == null )//|| mIdData == null || NmedSdkCom == null)
                    {
                        MessageBox.Show("Please scan the card first ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tsb_DriversLicense.Select();
                        _IsValid = false;
                    }

                    else if (txtLicFirstName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter the patient first name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtLicFirstName.Focus();
                        _IsValid = false;
                    }
                    else if (txtLicLastName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter the patient last name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtLicLastName.Focus();
                        _IsValid = false;
                    }

                    else if (txtLicDOB.MaskCompleted == false)
                    {
                        MessageBox.Show("Enter a date of birth for the patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtLicDOB.Focus();
                        _IsValid = false;
                    }


                    txtLicDOB.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

                }
                else if (_CardScanType == CardScanType.Cheque)
                {

                }
                else if (_CardScanType == CardScanType.InsuranceCard)
                {


                    if (pb_FrontSide.Image == null || pb_BackSide.Image == null)
                    {
                        MessageBox.Show("Please scan the card first ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tsb_InsuranceCard.Select();
                        _IsValid = false;
                    }

                    else if (txtIns_MemberName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter the member name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIns_MemberName.Focus();
                        _IsValid = false;
                    }



                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _IsValid;
        }
        #endregion

        //SLR: Code Verification End:

        #region "Zip control implemented  "
        bool isFormLoading = false;

        private gloZipcontrol oZipcontrol = null;
   //     private bool isSearchControlOpen = false;
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
                _TempZipText = txt_NewZip.Text.Trim();

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
                // gloZipcontrol oZipcontrol= new gloZipcontrol();

                if (e.KeyCode == Keys.Down | e.KeyCode == Keys.Up)
                {
                    //' HITS UP / DOWN ''
                    if (pnlInternalControl.Visible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        if (oZipcontrol != null)
                        {
                            oZipcontrol.C1GridList.Focus();
                            oZipcontrol.C1GridList.Select(oZipcontrol.C1GridList.RowSel, 0);
                        }
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
                        if (oZipcontrol != null)
                        {
                            oZipcontrol_ItemSelected(null, null);
                        }
                        if (txt_NewZip.Text.Trim() != "")
                        {
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;

                            _isMessageshown = true;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = true;

                        }

                    }
                }
                else if (e.KeyChar == Convert.ToChar(27))
                {
                    //' HITS ESCAPE ''

                    if (txt_NewCity.Text == "" && txt_NewCounty.Text == "" && txt_NewZip.Text == "")
                    //if ( txtPAZip.Text == "")
                    {
                        _TempZipText = txt_NewZip.Text;

                    }
                    txt_NewCity.Focus();
                }


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
                    txt_NewZip.Text = _TempZipText;
                    cmbNewCountry.Text = "US";
                    if (txt_NewCity.Text == "" && txt_NewCounty.Text == "" && txt_NewZip.Text == "")
                    {
                        _TempZipText = txt_NewZip.Text;

                    }
                    else
                    {

                        if (txt_NewZip.Text == "")
                        {
                            pnlInternalControl.Visible = false;
                            _isTextBoxLoading = false;
                            return;
                        }

                        int len;
                        if (_Country == "US")
                        {
                            len = 5;
                        }
                        else if (_Country == "CA")
                        {
                            len = 7;
                        }
                        else
                        {
                            len = 10;
                        }



                        if (_isMessageshown == true)
                        {
                            return;
                        }
                        if (txt_NewZip.Text.Length <= len)
                        {

                            if (checkZip(txt_NewZip.Text) == false)
                            {

                                if (MessageBox.Show("It is not a known zip code. Do you want to continue with this information?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    txt_NewZip.Text = _TempZipText;
                                    pnlInternalControl.Visible = false;
                                    _isTextBoxLoading = false;
                                    _isMessageshown = false;
                                    //return;
                                }
                            }

                        }

                    }
                    pnlInternalControl.Visible = false;
                    _isTextBoxLoading = false;
                }
            }
        }

        private void txtZip_TextChanged(object sender, System.EventArgs e)
        {
            string _strRegex = "";
            string strZipcode = txt_NewZip.Text;
            try
            {
                    //'Allow digits only if country is US 
                    if ((_Country == "US"))
                    {
                        _strRegex = "[0-9]";
                    }
                    else
                    {
                        //'allow alphanumerics if country is Canada 
                        _strRegex = "^([0-9a-zA-Z])";
                    }
                    foreach (char c in strZipcode)
                    {
                        if (Regex.IsMatch(c.ToString(), _strRegex) == false)
                        {


                            strZipcode = strZipcode.Replace(c.ToString(), "");
                        }
                    }
                    txt_NewZip.Text = strZipcode;



                //_ZipTextType = enumZipTextType.PatientZip;
                pnlInternalControl.BringToFront();

                if (isFormLoading == false & _isTextBoxLoading == false)
                {
                    if ((pnlInternalControl.Visible == false) || (oZipcontrol == null))
                    {
                        if (Convert.ToString(cmbNewCountry.SelectedValue).ToUpper() == "US" || Convert.ToString(cmbNewCountry.SelectedValue).ToUpper() == "CA")
                        {

                            pnlInternalControl.Visible = true;
                            OpenInternalControl(gloGridListControlType.ZIP, "Zip", false, 0, 0, "");
                            if (oZipcontrol != null)
                            {
                                oZipcontrol.FillControl(Convert.ToString(txt_NewZip.Text.Trim()));
                            }
                        }
                    }
                    else
                    {
                        // oZipcontrol.FillControl(Convert.ToString(txt_NewZip.Text.Trim()));
                        if (txt_NewZip.Text.Trim().Length == 0)
                        {
                            pnlInternalControl.Visible = false;
                        }
                        else
                        {
                            oZipcontrol.FillControl(Convert.ToString(txt_NewZip.Text.Trim()));
                        }


                    }
                }
            }
            catch
            {
            }
            finally
            {
                //_TempZipText = txtPAZip.Text;
                _strRegex = null;
                strZipcode = null;
            }
        }



        private void oZipcontrol_ItemSelected(object sender, EventArgs e)
        {
            string _Zip = null;
            string _City = null;
            string _ID = null;
            string _County = null;
            string _State = null;
            string _AreaCode = null;
            try
            {
                if (oZipcontrol == null) return;

                if (oZipcontrol.C1GridList.Row < 0)
                {
                    return;
                }
                _Zip = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 0).ToString();
                _City = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 1).ToString();
                _ID = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2).ToString();
                _County = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 3).ToString();
                _State = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 4).ToString();
                _AreaCode = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 5).ToString();

                _isTextBoxLoading = true;
                //switch (_ZipTextType) {
                //    case enumZipTextType.PatientZip:
                txt_NewZip.Text = _Zip;
                txt_NewZip.Tag = _ID;
                txt_NewCity.Text = _City;
                txt_NewCity.Tag = _AreaCode;
                txt_NewCounty.Text = _County;
                cmbNewState.Text = _State;
                cmbNewCountry.Text = "US";

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
                    txt_NewCity.Focus();
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
                //isSearchControlOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _Zip = null;
                _City = null;
                _ID = null;
                _County = null;
                _State = null;
                _AreaCode = null;
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
                if (!string.IsNullOrEmpty(txt_NewCity.Text))
                {
                    AddCity(txt_NewCity.Text.Trim(), cmbNewState.SelectedText.Trim(), txt_NewZip.Text.Trim(), "0", txt_NewCounty.Text.Trim());
                    pnlInternalControl.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_NewCity.Focus();
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
                //isSearchControlOpen = false;
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
                //isSearchControlOpen = false;
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
                if (oZipcontrol != null)
                {
                    nZipID = Convert.ToInt64(oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2));
                }
                //Update the city against this zipcode in the db
                //switch (_ZipTextType) {
                //    case enumZipTextType.PatientZip:
                if (!string.IsNullOrEmpty(txt_NewCity.Text))
                {
                    UpdateCity(txt_NewCity.Text.Trim(), txt_NewZip.Text.Trim(), nZipID);
                    pnlInternalControl.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_NewCity.Focus();
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



                //isSearchControlOpen = false;

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
                if (oZipcontrol != null)
                {
                     
                    oZipcontrol.InternalGridKeyDownclick += oZipcontrol_InternalGridKeyDown;
                    
                    oZipcontrol.ItemSelectedclick += oZipcontrol_ItemSelected;
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
                else
                {
                    _result = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {

            }

            //isSearchControlOpen = true;
            return _result;
        }

        private bool CloseInternalControl()
        {
            if (oZipcontrol != null)
            {

                _isTextBoxLoading = true;
                //SLR30:
                oZipcontrol.ItemSelectedclick -= oZipcontrol_ItemSelected;
                oZipcontrol.InternalGridKeyDownclick -= oZipcontrol_InternalGridKeyDown;

                //switch (_ZipTextType)
                //{
                //    case enumZipTextType.PatientZip:
                //SLR: Changed on 4/4/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
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
                    try
                    {
                        oZipcontrol.InternalGridKeyDownclick -= oZipcontrol_InternalGridKeyDown;
                        
                        oZipcontrol.ItemSelectedclick -= oZipcontrol_ItemSelected;
 
                    }
                    catch { }
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

                if (!string.IsNullOrEmpty(txt_NewCity.Text.Trim()))
                {
                    if (txt_NewZip.Text.Trim() == sZip)
                    {
                        txt_NewCity.Text = sCity;
                    }
                }


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
                if (conn != null)
                {
                    _strSQL = "update csz_mst set city = '" + sCity.Replace("'", "''") + "' where zip = '" + sZip + "'";

                    oCmd = new System.Data.SqlClient.SqlCommand();
                    if (oCmd != null)
                    {
                        oCmd.Connection = conn;
                        oCmd.CommandType = CommandType.Text;
                        oCmd.CommandText = _strSQL;

                        conn.Open();
                        oCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Compress
                if ((oCmd != null))
                {
                    oCmd.Dispose();
                    oCmd = null;
                }
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                    }
                }
                _strSQL = null;
            }
        }
        public void UpdateCity1(string sCity, string sZip, Int64 ID)
        {
            SqlConnection conn = new SqlConnection(_databaseconnectionstring);
            SqlCommand oCmd = default(SqlCommand);
            string _strSQL = "";

            try
            {
                if (conn != null)
                {
                    _strSQL = "update csz_mst set city = '" + sCity.Replace("'", "''") + "' where zip = '" + sZip + "' AND nID = " + ID + "";
                    oCmd = new SqlCommand();
                    if (oCmd != null)
                    {
                        oCmd.Connection = conn;
                        oCmd.CommandType = CommandType.Text;
                        oCmd.CommandText = _strSQL;

                        conn.Open();
                        oCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((oCmd != null))
                {
                    oCmd.Dispose();
                    oCmd = null;
                }
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                        conn = null;

                    }
                }
                _strSQL = null;
            }
        }
        ////To add new city against provided zip code
        private void AddCity(string sCity, string sState, string sZip, string sAreaCode, string sCounty)
        {
            try
            {
                AddCity1(sCity, sState, sZip, Convert.ToInt64(sAreaCode), sCounty);

                if (!string.IsNullOrEmpty(txt_NewCity.Text.Trim()))
                {
                    if (txt_NewZip.Text.Trim() == sZip)
                    {
                        txt_NewCity.Text = sCity;
                    }
                }




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
                if (conn != null)
                {
                    _strSQL = "SELECT MAX(ISNULL(nID,0)) +1 From csz_mst";
                    oCmd = new SqlCommand();
                    if (oCmd != null)
                    {
                        oCmd.Connection = conn;
                        oCmd.CommandType = CommandType.Text;
                        oCmd.CommandText = _strSQL;
                        conn.Open();
                        _result = oCmd.ExecuteScalar();
                        oCmd.Dispose();//Compress
                        oCmd = null;
                    }

                    _strSQL = "";
                    _strSQL = "Insert into csz_mst (City,ST,Zip,Areacode,county,nID) values ('" + sCity.Replace("'", "''") + "','" + sState.Replace("'", "''") + "','" + sZip.Replace("'", "''") + "'," + sAreaCode + ",'" + sCounty.Replace("'", "''") + "'," + Convert.ToInt64(_result) + ")";
                    // where zip = '" & sZip & "'"
                    oCmd = new SqlCommand();
                    if (oCmd != null)
                    {
                        oCmd.Connection = conn;
                        oCmd.CommandType = CommandType.Text;
                        oCmd.CommandText = _strSQL;
                        oCmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((oCmd != null))
                {
                    oCmd.Dispose();
                    oCmd = null;
                }
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                    }
                }
                _strSQL = null;
                _result = null;
            }
        }
        #endregion

        private void cmbPAHandDom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region "Fill State"

        private void FillStates(ComboBox _oComboBox)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                if (oDB != null)
                {
                    if (oDB.Connect(false))
                    {
                        DataTable dtStates = null;
                        string _sqlQuery = "SELECT distinct ST FROM CSZ_MST order by ST";
                        oDB.Retrive_Query(_sqlQuery, out dtStates);
                        oDB.Disconnect();
                        _sqlQuery = null;

                        if (dtStates != null)
                        {
                            DataRow dr = dtStates.NewRow();
                            dr["ST"] = "";
                            dtStates.Rows.InsertAt(dr, 0);
                            dtStates.AcceptChanges();

                            _oComboBox.DataSource = dtStates;
                            _oComboBox.DisplayMember = "ST";

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        #endregion

        #region "Fill Country"
        bool _isCountryComboLoading = false;
        //private void fillCountry(ComboBox _oComboBox)
        //{

        //    try
        //    {
        //        _isCountryComboLoading = true;

        //        DataTable dtCountry = null;

        //        dtCountry = GetCounrty();

        //        if (dtCountry != null)
        //        {
        //            DataRow dr = dtCountry.NewRow();
        //            dr["sCode"] = "";
        //            dr["sSubCode"] = "";
        //            dr["sName"] = "";
        //            dr["sStateLabel"] = "State :";
        //            dtCountry.Rows.InsertAt(dr, 0);
        //            dtCountry.AcceptChanges();

        //            _oComboBox.DataSource = dtCountry.Copy();
        //            _oComboBox.DisplayMember = "sName";
        //            _oComboBox.ValueMember = "sCode";
        //            //_oComboBox.SelectedIndex = -1;
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    {
        //        ex.ERROR_Log(ex.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //    finally
        //    {
        //        _isCountryComboLoading = false;
        //    }

        //}

        private void fillCountry(ComboBox _oComboBox)
        {

            try
            {
                _isCountryComboLoading = true;
                DataTable dtCountry = null;
                dtCountry = GetCounrty();

                if (dtCountry != null)
                {
                    DataRow dr = dtCountry.NewRow();
                    dr["sCode"] = "";
                    dr["sSubCode"] = "";
                    dr["sName"] = "";
                    dr["sStateLabel"] = "State";
                    dtCountry.Rows.InsertAt(dr, 0);
                    dtCountry.AcceptChanges();

                    _oComboBox.DataSource = dtCountry;
                    _oComboBox.DisplayMember = "sName";
                    _oComboBox.ValueMember = "sCode";

                    _oComboBox.BeginUpdate();
                    _oComboBox.SelectedIndex = -1;
                    _oComboBox.EndUpdate();
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
                _isCountryComboLoading = false;
            }

        }

        public DataTable GetCounrty()
        {
            DataTable dt = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {

                strQuery = " SELECT sCode,sSubCode,sName,sStateLabel,ISNULL(bIsSystem,0) AS bIsSystem FROM Contacts_Country_MST WHERE ISNULL(bIsBlocked,0) = 0 ";

                oDB.Connect(false);
                oDB.Retrive_Query(strQuery, out dt);
                oDB.Disconnect();
            }

            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                strQuery = null;
            }

            return dt;

        }

        #endregion "Fill Country"

        #region "Zip control implemented For New patient "

        bool isFormLoading_new = false;

        private gloZipcontrol oZipcontrolNew;
   //     private bool isSearchControlOpen_New = false;

        private string _TempZipTextNew;
        private bool _isZipItemSelected_New = false;
        private bool _isTextBoxLoading_New = false;

        #region " ZIP Text Events "

        private void txtNewZip_GotFocus(object sender, System.EventArgs e)
        {
            try
            {
                //if (_ZipTextType != enumZipTextType.PatientZip) {
                _TempZipTextNew = txtNew_Zip.Text.Trim();

                //}
            }
            catch
            {
            }
        }

        private void txtNewZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down | e.KeyCode == Keys.Up)
                {
                    //' HITS UP / DOWN ''
                    if (pnlInternalControl_New.Visible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        oZipcontrolNew.C1GridList.Focus();
                        oZipcontrolNew.C1GridList.Select(oZipcontrolNew.C1GridList.RowSel, 0);
                    }
                }
            }
            catch
            {
            }
        }
        bool _isMessageshown = false;
        private void txtNewZip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                //_ZipTextType = enumZipTextType.PatientZip;
                if (e.KeyChar == Convert.ToChar(13))
                {
                    //' HITS ENTER BUTTON ''
                    if (pnlInternalControl.Visible)
                    {
                        if (pnlInternalControl_New.Visible)
                        {
                            oZipcontrol_ItemSelected_New(null, null);
                        }

                        if (txtNew_Zip.Text.Trim() != "")
                        {
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;                            
                            _isMessageshown = true;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = false;
                            _isTextBoxLoading = false;
                            _isZipItemSelected = true;
                        }
                    }
                }
                else if (e.KeyChar == Convert.ToChar(27))
                {
                    //' HITS ESCAPE ''

                    if (txtNew_City.Text == "" && txtNew_County.Text == "" && txtNew_Zip.Text == "")
                    //if ( txtPAZip.Text == "")
                    {
                        _TempZipTextNew = txtNew_Zip.Text;

                    }
                    txtNew_City.Focus();
                }


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

        private void txtNewZip_LostFocus(object sender, System.EventArgs e)
        {
            if (oZipcontrolNew != null)
            {
                if (_isZipItemSelected_New == false & oZipcontrolNew.C1GridList.Focused == false & oZipcontrolNew.Focused == false)
                {
                    _isTextBoxLoading_New = true;
                    txtNew_Zip.Text = _TempZipTextNew;
                    cmbNew_Country.Text = "US";
                    if (txtNew_City.Text == "" && txtNew_County.Text == "" && txtNew_Zip.Text == "")
                    {
                        _TempZipTextNew = txtNew_Zip.Text;

                    }
                    else
                    {
                        //_TempZipText = txtZip.Text;
                        if (txtNew_Zip.Text == "")
                        {
                            pnlInternalControl.Visible = false;
                            _isTextBoxLoading = false;
                            return;
                        }
                        // txtZip.Text = ZipLeadingWithZero(txtZip);
                        int len;
                        if (_Country == "US")
                        {
                            len = 5;
                        }
                        else if (_Country == "CA")
                        {
                            len = 7;
                        }
                        else
                        {
                            len = 10;
                        }




                        if (_isMessageshown == true)
                        {
                            return;
                        }
                        if (txtNew_Zip.Text.Length <= len)
                        //if (txtZip.Text.Length <= 5)
                        {
                            //(checkZip(txtZip.Text) == false)
                            if (checkZip(txtNew_Zip.Text) == false)
                            {

                                if (MessageBox.Show("It is not a known zip code. Do you want to continue with this information?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    txtNew_Zip.Text = _TempZipText;
                                    pnlInternalControl.Visible = false;
                                    _isTextBoxLoading = false;
                                    _isMessageshown = false;
                                    //return;
                                }
                            }

                        }

                    }
                    pnlInternalControl_New.Visible = false;
                    _isTextBoxLoading_New = false;

                }

            }
        }

        public bool checkZip(string zip)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //DataTable dtStates = new DataTable();
                string _sqlQuery = "SELECT count(*) FROM  CSZ_MST  WHERE ZIP = '" + zip + "' ";
                Object NoOfRec = oDB.ExecuteScalar_Query(_sqlQuery);
                _sqlQuery = null;
                if (Convert.ToInt64(NoOfRec) > 0)
                {
                    _result = true;
                }
                oDB.Disconnect();
                oDB.Dispose();
                return _result;

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                _result = false;
                return _result;

            }
        }

        private void txtNewZip_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                {
                    string _strRegex = "";
                    //'Allow digits only if country is US 
                    if ((_Country == "US"))
                    {
                        _strRegex = "[0-9]";
                    }
                    else
                    {
                        //'allow alphanumerics if country is Canada 
                        _strRegex = "^([0-9a-zA-Z])";
                    }
                    string strZipcode = txtNew_Zip.Text;
                    foreach (char c in strZipcode)
                    {
                        if (Regex.IsMatch(c.ToString(), _strRegex) == false)
                        {


                            strZipcode = strZipcode.Replace(c.ToString(), "");
                        }
                    }
                    txtNew_Zip.Text = strZipcode;
                    _strRegex = null;
                }



                //_ZipTextType = enumZipTextType.PatientZip;
                pnlInternalControl_New.BringToFront();

                if (isFormLoading_new == false & _isTextBoxLoading_New == false)
                {
                    if (pnlInternalControl_New.Visible == false)
                    {
                        if (Convert.ToString(cmbNew_Country.SelectedValue).ToUpper() == "US" || Convert.ToString(cmbNew_Country.SelectedValue).ToUpper() == "CA")
                        {

                            pnlInternalControl_New.Visible = true;
                            OpenInternalControl_New(gloGridListControlType.ZIP, "Zip", false, 0, 0, "");
                            oZipcontrolNew.FillControl(Convert.ToString(txtNew_Zip.Text.Trim()));
                        }
                    }
                    else
                    {
                        //oZipcontrolNew.FillControl(Convert.ToString(txtNew_Zip.Text.Trim()));
                        if (txtNew_Zip.Text.Trim().Length == 0)
                        {
                            pnlInternalControl.Visible = false;
                        }
                        else
                        {
                            oZipcontrol.FillControl(Convert.ToString(txtNew_Zip.Text.Trim()));
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                //_TempZipTextNew = txtPAZip.Text;
            }
        }



        private void oZipcontrol_ItemSelected_New(object sender, EventArgs e)
        {
            string _Zip = null;
            string _City = null;
            string _ID = null;
            string _County = null;
            string _State = null;
            string _AreaCode = null;

            try
            {
                if (oZipcontrolNew.C1GridList.Row < 0)
                {
                    return;
                }
                _Zip = oZipcontrolNew.C1GridList.GetData(oZipcontrolNew.C1GridList.Row, 0).ToString();
                _City = oZipcontrolNew.C1GridList.GetData(oZipcontrolNew.C1GridList.Row, 1).ToString();
                _ID = oZipcontrolNew.C1GridList.GetData(oZipcontrolNew.C1GridList.Row, 2).ToString();
                _County = oZipcontrolNew.C1GridList.GetData(oZipcontrolNew.C1GridList.Row, 3).ToString();
                _State = oZipcontrolNew.C1GridList.GetData(oZipcontrolNew.C1GridList.Row, 4).ToString();
                _AreaCode = oZipcontrolNew.C1GridList.GetData(oZipcontrolNew.C1GridList.Row, 5).ToString();

                _isTextBoxLoading_New = true;
                //switch (_ZipTextType) {
                //    case enumZipTextType.PatientZip:
                txtNew_Zip.Text = _Zip;
                txtNew_Zip.Tag = _ID;
                txtNew_City.Text = _City;
                txtNew_City.Tag = _AreaCode;
                txtNew_County.Text = _County;
                cmbNew_State.Text = _State;
                cmbNew_Country.Text = "US";

                _isTextBoxLoading_New = false;
                _isZipItemSelected_New = true;
                if (pnlInternalControl_New.Visible == true)
                {
                    pnlInternalControl_New.Visible = false;
                    txtNew_City.Focus();
                }

                //isSearchControlOpen_New = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _Zip = null;
                _City = null;
                _ID = null;
                _County = null;
                _State = null;
                _AreaCode = null;
            }
        }

        private void oZipcontrol_InternalGridKeyDown_New(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl_new();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        private void oZipcontrol_AddBtnClick_New(object sender, System.EventArgs e)
        {

            try
            {
                //switch (_ZipTextType) {
                //    case enumZipTextType.PatientZip:
                if (!string.IsNullOrEmpty(txtNew_City.Text))
                {
                    AddCity_New(txtNew_City.Text.Trim(), cmbNewState.SelectedText.Trim(), txtNew_Zip.Text.Trim(), "0", txtNew_County.Text.Trim());
                    pnlInternalControl_New.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNew_City.Focus();
                }



                //isSearchControlOpen_New = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void oZipcontrol_CloseBtnClick1_New(object sender, System.EventArgs e)
        {
            try
            {
                if (this.pnlInternalControl_New.Visible == true)
                {
                    this.pnlInternalControl_New.Visible = false;
                }

                //isSearchControlOpen_New = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }



        private void oZipcontrol_ModifyBtnClick_New(object sender, System.EventArgs e)
        {

            try
            {
                Int64 nZipID = default(Int64);
                nZipID = Convert.ToInt64(oZipcontrolNew.C1GridList.GetData(oZipcontrolNew.C1GridList.Row, 2));
                //Update the city against this zipcode in the db
                //switch (_ZipTextType) {
                //    case enumZipTextType.PatientZip:
                if (!string.IsNullOrEmpty(txtNew_City.Text))
                {
                    UpdateCity_New(txtNew_City.Text.Trim(), txtNew_Zip.Text.Trim(), nZipID);
                    pnlInternalControl_New.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please enter City", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNew_City.Focus();
                }




                //isSearchControlOpen_New = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool OpenInternalControl_New(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            _isZipItemSelected_New = false;
            try
            {

                if (oZipcontrolNew != null)
                {
                    CloseInternalControl_new();
                }
                oZipcontrolNew = new gloZipcontrol(ControlType, false, 0, 0, 0, _databaseconnectionstring);
                //SLR30:
                oZipcontrolNew.ItemSelectedclick -= oZipcontrol_ItemSelected_New;//oZipcontrol_ItemSelectedNew;

                oZipcontrolNew.InternalGridKeyDownclick -= oZipcontrol_InternalGridKeyDown_New; //oZipcontrol_InternalGridKeyDownNew;
                //SLR: LookforHere
                oZipcontrolNew.ItemSelectedclick += oZipcontrol_ItemSelected_New;
                oZipcontrolNew.InternalGridKeyDownclick += oZipcontrol_InternalGridKeyDown_New;
                //AddHandler oZipcontrolNew.CloseBtnClick, AddressOf oZipcontrol_CloseBtnClick
                oZipcontrolNew.ControlHeader = ControlHeader;
                oZipcontrolNew.ShowHeader = false;

                //switch (_ZipTextType)
                //{
                //    case enumZipTextType.PatientZip:
                oZipcontrolNew.Dock = DockStyle.Fill;
                pnlInternalControl_New.BringToFront();
                pnlInternalControl_New.Visible = true;
                pnlInternalControl_New.Controls.Add(oZipcontrolNew);




                if (!string.IsNullOrEmpty(SearchText))
                {
                    oZipcontrolNew.Search(SearchText, SearchColumn.Code);
                }
                oZipcontrolNew.Show();
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

            //isSearchControlOpen_New = true;
            return _result;
        }

        private bool CloseInternalControl_new()
        {
            if (oZipcontrolNew != null)
            {

                _isTextBoxLoading_New = true;
               
                //switch (_ZipTextType)
                //{
                //    case enumZipTextType.PatientZip:
                //SLR: Changed on 4/4/2014
                for (int i = pnlInternalControl_New.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl_New.Controls.RemoveAt(i);
                }



                if (oZipcontrolNew != null)
                {
                    //SLR30:
                    oZipcontrolNew.ItemSelectedclick -= oZipcontrol_ItemSelected_New;

                    oZipcontrolNew.InternalGridKeyDownclick -= oZipcontrol_InternalGridKeyDown_New;
                    oZipcontrolNew.Dispose();
                    oZipcontrolNew = null;
                }


                _isTextBoxLoading_New = false;

            }
            return _isTextBoxLoading_New;
        }

        private void UpdateCity_New(string sCity, string sZip, Int64 ID)
        {

            try
            {
                UpdateCity1_new(sCity, sZip, ID);

                if (!string.IsNullOrEmpty(txtNew_City.Text.Trim()))
                {
                    if (txtNew_Zip.Text.Trim() == sZip)
                    {
                        txtNew_City.Text = sCity;
                    }
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCityAgainstZip_new(string sCity, string sZip)
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
                        conn.Dispose();
                        conn = null;

                    }
                }
                if ((oCmd != null))
                {
                    oCmd.Dispose();
                    oCmd = null;
                }
                _strSQL = null;
            }
        }
        public void UpdateCity1_new(string sCity, string sZip, Int64 ID)
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
                        conn.Dispose();
                        conn = null;

                    }
                }
                _strSQL = null;
                if ((oCmd != null))
                {
                    oCmd.Dispose();
                    oCmd = null;
                }
            }
        }
        ////To add new city against provided zip code
        private void AddCity_New(string sCity, string sState, string sZip, string sAreaCode, string sCounty)
        {
            try
            {
                AddCity1_new(sCity, sState, sZip, Convert.ToInt64(sAreaCode), sCounty);

                if (!string.IsNullOrEmpty(txtNew_City.Text.Trim()))
                {
                    if (txtNew_Zip.Text.Trim() == sZip)
                    {
                        txtNew_City.Text = sCity;
                    }
                }




            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public void AddCity1_new(string sCity, string sState, string sZip, Int64 sAreaCode, string sCounty)
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
                oCmd.Dispose();//Compress
                oCmd = null;
                _strSQL = "";
                _strSQL = "Insert into csz_mst (City,ST,Zip,Areacode,county,nID) values ('" + sCity.Replace("'", "''") + "','" + sState.Replace("'", "''") + "','" + sZip.Replace("'", "''") + "'," + sAreaCode + ",'" + sCounty.Replace("'", "''") + "'," + Convert.ToInt64(_result) + ")";
                // where zip = '" & sZip & "'"
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
                if ((oCmd != null))
                {
                    oCmd.Dispose();
                    oCmd = null;
                }
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                        conn = null;

                    }
                }
                _strSQL = null;
                _result = null;
            }
        }
        #endregion

        #region "Combo Country Selected index events"

        private void cmbNew_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbNew_Country.Text.ToUpper() != "CANADA" || cmbNew_Country.Text == "")
            //{
            //    txtNew_County.Visible = true;
            //    lblNew_County.Visible = true;
            //    lblState.Text = "State :";

            //}
            //else
            //{
            //    txtNew_County.Visible = false;
            //    lblNew_County.Visible = false;
            //    lblState.Text = "Province :";
            //}
            if (_isCountryComboLoading == false)
            {
                combo = (ComboBox)sender;
                if (cmbNew_Country.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbNew_Country.Items[cmbNew_Country.SelectedIndex])["sName"]), cmbNew_Country) >= cmbNew_Country.DropDownWidth - 20)
                    {
                        string txt = Convert.ToString(((DataRowView)cmbNew_Country.Items[cmbNew_Country.SelectedIndex])["sName"]);
                        if (tooltipZip.GetToolTip(cmbNew_Country) != txt)
                        {
                            tooltipZip.SetToolTip(cmbNew_Country, txt);
                        }
                    }
                    else
                    {
                        this.tooltipZip.SetToolTip(cmbNew_Country, "");
                    }

                }
            }

            DataRowView _drCountrySelectedRow = null;
            string _StateLabel = "";
            bool _bIsSystemRecord = false;

            try
            {
                if (_isCountryComboLoading == false)
                {
                    #region " Code Changes "

                    //lblState.Text = "";


                    if (cmbNew_Country.SelectedItem != null)
                    {
                        _drCountrySelectedRow = ((DataRowView)cmbNew_Country.SelectedItem);
                        _StateLabel = "";
                        _bIsSystemRecord = false;

                        _StateLabel = Convert.ToString(_drCountrySelectedRow["sStateLabel"]);
                        if (_StateLabel.Trim() != "") { _StateLabel = _StateLabel.Trim() + " :"; }
                        else { _StateLabel = "State :"; }
                        lblState.Text = _StateLabel;

                        if (_drCountrySelectedRow["bIsSystem"] != DBNull.Value && Convert.ToString(_drCountrySelectedRow["bIsSystem"]).Trim() != "")
                        {
                            _bIsSystemRecord = Convert.ToBoolean(_drCountrySelectedRow["bIsSystem"]);
                        }
                    }


                    if (_drCountrySelectedRow != null)
                    {
                        if (Convert.ToString(_drCountrySelectedRow["sCode"]).ToUpper() == "US" && _bIsSystemRecord == true)
                        {
                            txtNew_County.Visible = true;
                            lblNew_County.Visible = true;
                            txtNew_Zip.MaxLength = 5;
                            cmbNew_State.DropDownStyle = ComboBoxStyle.DropDownList;
                            _Country = "US";
                        }
                        else if (Convert.ToString(_drCountrySelectedRow["sCode"]).ToUpper() == "CA" && _bIsSystemRecord == true)
                        {
                            txtNew_County.Visible = false;
                            lblNew_County.Visible = false;
                            txtNew_Zip.MaxLength = 7;
                            cmbNew_State.DropDownStyle = ComboBoxStyle.DropDownList;
                            _Country = "CA";
                        }
                        else
                        {
                            txtNew_County.Visible = false;
                            txtNew_County.Text = string.Empty;
                            lblNew_County.Visible = false;
                            txtNew_Zip.MaxLength = 10;
                            cmbNew_State.DropDownStyle = ComboBoxStyle.DropDown;
                            cmbNew_State.MaxLength = 2;
                            _Country = "";
                        }
                    }
                    else
                    {
                        txtNew_County.Visible = false;
                        txtNew_County.Text = string.Empty;
                        lblNew_County.Visible = false;
                        txtNew_Zip.MaxLength = 10;
                        cmbNew_State.DropDownStyle = ComboBoxStyle.DropDown;
                        cmbNew_State.MaxLength = 2;
                        _Country = "";
                    }



                    #endregion " Code Changes "
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _drCountrySelectedRow = null;
                _StateLabel = null;
                ClearZipCodeForUSandCanadaIfLengthExceeded_NewLic();
            }




        }
        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
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
        private void ClearZipCodeForUSandCanadaIfLengthExceeded_NewLic()
        {
            this.txtNew_Zip.TextChanged -= new System.EventHandler(this.txtNewZip_TextChanged);
            if (Convert.ToString(cmbNew_Country.SelectedValue) == "US")
            {
                if (Convert.ToString(txtNew_Zip.Text).Trim().Length > 5)
                {
                    txtNew_Zip.Text = "";
                }
            }
            else if (Convert.ToString(cmbNew_Country.SelectedValue) == "CA")
            {
                if (Convert.ToString(txtNew_Zip.Text).Trim().Length > 7)
                {
                    txtNew_Zip.Text = "";
                }
            }
            this.txtNew_Zip.TextChanged += new System.EventHandler(this.txtNewZip_TextChanged);
        }

        private void cmbNewCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbNewCountry.Text.ToUpper() != "CANADA" || cmbNewCountry.Text == "")
            //{
            //    txt_NewCounty.Visible = true;
            //    lbl_NewCounty.Visible = true;
            //    lbl_NewState.Text = "State :";

            //}
            //else
            //{
            //    txt_NewCounty.Visible = false;
            //    lbl_NewCounty.Visible = false;
            //    lbl_NewState.Text = "Province :";
            //}
            if (_isCountryComboLoading == false)
            {
                combo = (ComboBox)sender;
                if (cmbNewCountry.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbNewCountry.Items[cmbNewCountry.SelectedIndex])["sName"]), cmbNewCountry) >= cmbNewCountry.DropDownWidth - 20)
                    {
                        string txt = Convert.ToString(((DataRowView)cmbNewCountry.Items[cmbNewCountry.SelectedIndex])["sName"]);
                        if (tooltipZip.GetToolTip(cmbNewCountry) != txt)
                        {
                            tooltipZip.SetToolTip(cmbNewCountry, txt);
                        }
                    }
                    else
                    {
                        this.tooltipZip.SetToolTip(cmbNewCountry, "");
                    }

                }
            }

            DataRowView _drCountrySelectedRow = null;
            string _StateLabel = "";
            bool _bIsSystemRecord = false;

            try
            {
                if (_isCountryComboLoading == false)
                {
                    #region " Code Changes "

                    //lbl_NewState.Text = "";


                    if (cmbNewCountry.SelectedItem != null)
                    {
                        _drCountrySelectedRow = ((DataRowView)cmbNewCountry.SelectedItem);
                        _StateLabel = "";
                        _bIsSystemRecord = false;

                        _StateLabel = Convert.ToString(_drCountrySelectedRow["sStateLabel"]);
                        if (_StateLabel.Trim() != "") { _StateLabel = _StateLabel.Trim() + " :"; }
                        else { _StateLabel = "State :"; }
                        lbl_NewState.Text = _StateLabel;

                        if (_drCountrySelectedRow["bIsSystem"] != DBNull.Value && Convert.ToString(_drCountrySelectedRow["bIsSystem"]).Trim() != "")
                        {
                            _bIsSystemRecord = Convert.ToBoolean(_drCountrySelectedRow["bIsSystem"]);
                        }
                    }


                    if (_drCountrySelectedRow != null)
                    {
                        if (Convert.ToString(_drCountrySelectedRow["sCode"]).ToUpper() == "US" && _bIsSystemRecord == true)
                        {
                            txt_NewCounty.Visible = true;
                            lbl_NewCounty.Visible = true;
                            txt_NewZip.MaxLength = 5;
                            cmbNewState.DropDownStyle = ComboBoxStyle.DropDownList;
                            _Country = "US";
                        }
                        else if (Convert.ToString(_drCountrySelectedRow["sCode"]).ToUpper() == "CA" && _bIsSystemRecord == true)
                        {
                            txt_NewCounty.Visible = false;
                            lbl_NewCounty.Visible = false;
                            txt_NewZip.MaxLength = 7;
                            cmbNewState.DropDownStyle = ComboBoxStyle.DropDownList;
                            _Country = "CA";
                        }
                        else
                        {
                            txt_NewCounty.Visible = false;
                            txt_NewCounty.Text = string.Empty;
                            lbl_NewCounty.Visible = false;
                            txt_NewZip.MaxLength = 10;
                            cmbNewState.DropDownStyle = ComboBoxStyle.DropDown;
                            cmbNewState.MaxLength = 2;
                            _Country = "";
                        }
                    }
                    else
                    {
                        txt_NewCounty.Visible = false;
                        txt_NewCounty.Text = string.Empty;
                        lbl_NewCounty.Visible = false;
                        txt_NewZip.MaxLength = 10;
                        cmbNewState.DropDownStyle = ComboBoxStyle.DropDown;
                        cmbNewState.MaxLength = 2;
                        _Country = "";
                    }



                    #endregion " Code Changes "
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _drCountrySelectedRow = null;
                _StateLabel = null;
                ClearZipCodeForUSandCanadaIfLengthExceeded_ModLic();
            }


        }

        private void ClearZipCodeForUSandCanadaIfLengthExceeded_ModLic()
        {
            this.txt_NewZip.TextChanged -= new System.EventHandler(this.txtZip_TextChanged);
            if (Convert.ToString(cmbNewCountry.SelectedValue) == "US")
            {
                if (Convert.ToString(txt_NewZip.Text).Trim().Length > 5)
                {
                    txt_NewZip.Text = "";
                }
            }
            else if (Convert.ToString(cmbNewCountry.SelectedValue) == "CA")
            {
                if (Convert.ToString(txt_NewZip.Text).Trim().Length > 7)
                {
                    txt_NewZip.Text = "";
                }
            }
            this.txt_NewZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
        }


        private void cmbLicCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbLicCountry.Text.ToUpper() != "CANADA" || cmbLicCountry.Text == "")
            //{
            //    txtLic_County.Visible = true;
            //    lblLic_County.Visible = true;
            //    lbl_PrevState.Text = "State :";

            //}
            //else
            //{
            //    txtLic_County.Visible = false;
            //    lblLic_County.Visible = false;
            //    lbl_PrevState.Text = "Province :";

            //}



            DataRowView _drCountrySelectedRow = null;
            string _StateLabel = "";
            bool _bIsSystemRecord = false;

            try
            {
                if (_isCountryComboLoading == false)
                {
                    #region " Code Changes "

                    lbl_PrevState.Text = "";


                    if (cmbLicCountry.SelectedItem != null)
                    {
                        _drCountrySelectedRow = ((DataRowView)cmbLicCountry.SelectedItem);
                        _StateLabel = "";
                        _bIsSystemRecord = false;

                        _StateLabel = Convert.ToString(_drCountrySelectedRow["sStateLabel"]);
                        if (_StateLabel.Trim() != "") { _StateLabel = _StateLabel.Trim() + " :"; }
                        else { _StateLabel = "State :"; }
                        lbl_PrevState.Text = _StateLabel;

                        if (_drCountrySelectedRow["bIsSystem"] != DBNull.Value && Convert.ToString(_drCountrySelectedRow["bIsSystem"]).Trim() != "")
                        {
                            _bIsSystemRecord = Convert.ToBoolean(_drCountrySelectedRow["bIsSystem"]);
                        }
                    }


                    if (_drCountrySelectedRow != null)
                    {
                        if (Convert.ToString(_drCountrySelectedRow["sCode"]).ToUpper() == "US" && _bIsSystemRecord == true)
                        {
                            txtLic_County.Visible = true;
                            lblLic_County.Visible = true;
                            txtLicZip.MaxLength = 5;
                            cmbLicState.DropDownStyle = ComboBoxStyle.DropDownList;
                            _Country = "US";
                        }
                        else if (Convert.ToString(_drCountrySelectedRow["sCode"]).ToUpper() == "CA" && _bIsSystemRecord == true)
                        {
                            txtLic_County.Visible = false;
                            lblLic_County.Visible = false;
                            txtLicZip.MaxLength = 7;
                            cmbLicState.DropDownStyle = ComboBoxStyle.DropDownList;
                            _Country = "CA";
                        }
                        else
                        {
                            txtLic_County.Visible = false;
                            txtLic_County.Text = string.Empty;
                            lblLic_County.Visible = false;
                            txtLicZip.MaxLength = 10;
                            cmbLicState.DropDownStyle = ComboBoxStyle.DropDown;
                            cmbLicState.MaxLength = 2;
                            _Country = "";
                        }
                    }
                    else
                    {
                        txtLic_County.Visible = false;
                        txtLic_County.Text = string.Empty;
                        lblLic_County.Visible = false;
                        txtLicZip.MaxLength = 10;
                        cmbLicState.DropDownStyle = ComboBoxStyle.DropDown;
                        cmbLicState.MaxLength = 2;
                        _Country = "";
                    }


                    #endregion " Code Changes "
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _drCountrySelectedRow = null;
                _StateLabel = null;
                ClearZipCodeForUSandCanadaIfLengthExceeded_Prev();
            }

        }

        private void ClearZipCodeForUSandCanadaIfLengthExceeded_Prev()
        {
            this.txtLicZip.TextChanged -= new System.EventHandler(this.txtZip_TextChanged);
            if (Convert.ToString(cmbLicCountry.SelectedValue) == "US")
            {
                if (Convert.ToString(txt_NewZip.Text).Trim().Length > 5)
                {
                    txtLicZip.Text = "";
                }
            }
            else if (Convert.ToString(cmbLicCountry.SelectedValue) == "CA")
            {
                if (Convert.ToString(txtLicZip.Text).Trim().Length > 7)
                {
                    txtLicZip.Text = "";
                }
            }
            //this.txtLicZip.TextChanged += new System.EventHandler(this.txtp);
        }

        #endregion


        #endregion

        #region "Fill Insurance From scan Data"

        private void FillInsuranceName(string _sName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtIns = null;
            try
            {
                if (oDB != null)
                {
                    if (oDB.Connect(false))
                    {
                        string _sqlQuery = string.Empty;
                        //Bug #44826: 00000378 : Claims process
                        // Condition added to avoid Multiple Insurance Plan if we modify the insurance details.

                      // incident  	00009776    code integrated from 22 to 30 for getting unblock plan contactid 
                        if (!IsInsModify)
                        {
                            _sqlQuery = "select nContactID,sName from Contacts_MST where sName = '" + _sName.Trim() + "' and ltrim(rtrim(sName)) <> ''  AND ISNULL(bIsBlocked,0) = 0  order By sName";
                        }
                        else
                        {
                            _sqlQuery = "select nContactID,sName from Contacts_MST where nContactID = " + Convert.ToInt64(ContactID.Trim()) + " and ltrim(rtrim(sName)) <> ''  AND ISNULL(bIsBlocked,0) = 0  order By sName";
                        }
                     
                        
                        
                        //if (!IsInsModify)
                        //{
                        //    _sqlQuery = "select nContactID,sName from Contacts_MST where sName = '" + _sName.Trim() + "' and ltrim(rtrim(sName)) <> ''  order By sName";
                        //}
                        //else
                        //{
                        //    _sqlQuery = "select nContactID,sName from Contacts_MST where nContactID = " + Convert.ToInt64(ContactID.Trim()) + " and ltrim(rtrim(sName)) <> ''  order By sName";
                        //}



                        oDB.Retrive_Query(_sqlQuery, out dtIns);
                        oDB.Disconnect();

                        if (dtIns != null)
                        {
                            if (dtIns.Rows.Count > 1)
                            {

                                cmbIns_PlanProvider.DataSource = dtIns;
                                cmbIns_PlanProvider.DisplayMember = "sName";
                                cmbIns_PlanProvider.ValueMember = "nContactID";
                                cmbIns_PlanProvider.SelectedIndex = 0;
                                lbl_ScanMessage.Visible = true;
                                lbl_ScanMessage.Text = "Multiple Insurance Plan found.";
                            }
                            else if (dtIns.Rows.Count > 0)
                            {
                                cmbIns_PlanProvider.DataSource = dtIns;
                                cmbIns_PlanProvider.DisplayMember = "sName";
                                cmbIns_PlanProvider.ValueMember = "nContactID";
                                cmbIns_PlanProvider.SelectedIndex = 0;
                                lbl_ScanMessage.Visible = false;
                            }
                            else if (_sName.Trim() != "")
                            {
                                lbl_ScanMessage.Visible = true;
                                lbl_ScanMessage.Text = "No matches for Insurance Plan '" + _sName.Trim() + "'";
                            }
                            else
                            {
                                lbl_ScanMessage.Visible = true;
                                lbl_ScanMessage.Text = "No matches found for Insurance Plan. ";
                            }
                        }
                        _sqlQuery = null;
                    }//compress
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                //if (dtIns != null) { dtIns.Dispose(); dtIns = null; }
            }

        }

        #endregion

        #region "DOB Validation Events"

        private void txt_NewDOB_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txt_NewDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (txt_NewDOB.Text.Length > 0 && txt_NewDOB.MaskCompleted == false)
                {
                    MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                {
                    if (txt_NewDOB.MaskCompleted == true)
                    {
                        try
                        {
                            txt_NewDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                            if (IsValidDate(txt_NewDOB.Text))
                            {
                                if (Convert.ToDateTime(txt_NewDOB.Text).Date >= DateTime.Now.Date)
                                {
                                    MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    e.Cancel = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
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

            }
        }

        private void txtNew_DOB_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtNew_DOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (txtNew_DOB.Text.Length > 0 && txtNew_DOB.MaskCompleted == false)
                {
                    MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                {
                    if (txtNew_DOB.MaskCompleted == true)
                    {
                        try
                        {
                            txtNew_DOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                            if (IsValidDate(txtNew_DOB.Text))
                            {
                                if (Convert.ToDateTime(txtNew_DOB.Text).Date >= DateTime.Now.Date)
                                {
                                    MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    e.Cancel = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
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

            }
        }



        #endregion

        #region "Form Closing"

        private void frmScanCard_New_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (isSaveAndClose == false)
            {
                ////////UnLoadSdk();
                try
                {
                    try
                    {

                        if (_CardScanType == CardScanType.InsuranceCard)
                        {
                            if (pb_FrontSide.Image != null
                                 || cmbIns_PlanProvider.Text != "" ||
                                    txtIns_MemberID.Text != "" ||
                                    txtIns_MemberName.Text != "" ||
                                    txtIns_GroupNo.Text != ""
                                )
                            {
                                DialogResult DlgRst = DialogResult.None;
                                DlgRst = MessageBox.Show("Do you want to save a changes to this record ? ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (DlgRst == DialogResult.Yes)
                                {
                                    //Save Data in Objects & validate the Fields.
                                    if (SaveData() == true)
                                    {

                                        oDialogResult = true;
                                        //Clear Temp Image 
                                       // DeleteTempImages();
                                        e.Cancel = false;
                                    }
                                    else
                                    {
                                        oDialogResult = false;

                                    }
                                }
                                else if (DlgRst == DialogResult.Cancel)
                                {

                                    e.Cancel = true;
                                }
                                else
                                {
                                    oDialogResult = false;
                                    e.Cancel = false;
                                }
                            }
                            else
                            {
                                e.Cancel = false;
                            }


                        }
                        if (_CardScanType == CardScanType.DrivingLicense)
                        {
                            if (pb_FrontSide.Image != null ||

                                txt_NewFname.Text != "" ||
                                txt_NewMIname.Text != "" ||
                                txt_NewLastname.Text != "" ||
                                txt_NewAddress1.Text != "" ||
                                txt_NewAddress2.Text != "" ||
                                txt_NewCity.Text != "" ||
                                cmbNewState.Text != "" ||
                                txt_NewZip.Text != "" ||
                                txtNew_County.Text != ""
                                )
                            {
                                DialogResult DlgRst = DialogResult.None;
                                DlgRst = MessageBox.Show("Do you want to save a changes to this record ?  ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (DlgRst == DialogResult.Yes)
                                {
                                    if (SaveData() == true)
                                    {
                                        oDialogResult = true;
                                        //Clear Temp Image 
                                        //DeleteTempImages();
                                        e.Cancel = false;
                                    }
                                    else
                                    {
                                        if (oValidate != true)
                                        {
                                            oDialogResult = false;
                                        }
                                        else
                                        {
                                            oValidate = false;
                                            e.Cancel = true;
                                        }
                                    }
                                }
                                else if (DlgRst == DialogResult.Cancel)
                                {
                                    //return;
                                    e.Cancel = true;
                                }
                                else
                                {
                                    oDialogResult = false;
                                    e.Cancel = false;
                                }
                            }
                            else
                            {
                                e.Cancel = false;
                            }
                        }
                        if (_CardScanType == CardScanType.NewDrivingLicense)
                        {
                            if (pb_FrontSide.Image != null ||
                                txtNew_FirstName.Text != "" ||
                                txtNew_LastName.Text != "" ||
                                txtNew_MIName.Text != "" ||
                                txtNew_Address1.Text != "" ||
                                txtNew_Address2.Text != "" ||
                                txtNew_City.Text != "" ||
                                txtNew_Zip.Text != "" ||
                                cmbNew_State.Text != "" ||
                                txtNew_County.Text != "" ||
                                cmbNew_Gender.Text != "" ||
                                Pic_NewPatient.Image != null
                                )
                            {
                                DialogResult DlgRst = DialogResult.None;
                                DlgRst = MessageBox.Show("Do you want to save a changes to this record ? ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (DlgRst == DialogResult.Yes)
                                {
                                    //Save Data in Objects & validate the Fields
                                    if (SaveData() == true)
                                    {
                                        oDialogResult = true;
                                        //Clear Temp Image 
                                       // DeleteTempImages();
                                        e.Cancel = false;
                                    }
                                    else
                                    {
                                        if (oValidate != true)
                                        {
                                            oDialogResult = false;
                                        }
                                        else
                                        {
                                            oValidate = false;
                                            e.Cancel = true;
                                        }
                                    }
                                }
                                else if (DlgRst == DialogResult.Cancel)
                                {

                                    e.Cancel = true;
                                }
                                else
                                {
                                    oDialogResult = false;
                                    e.Cancel = false;
                                   
                                }
                            }
                            else
                            {
                                e.Cancel = false;
                            }
                        }

                        if (oZipcontrolNew != null)      //Dispossing Zip
                        {
                            //SLR30:
                            oZipcontrolNew.ItemSelectedclick -= oZipcontrol_ItemSelected_New;
                            oZipcontrolNew.InternalGridKeyDownclick -= oZipcontrol_InternalGridKeyDown_New;
                            txt_NewZip.TextChanged -= txtZip_TextChanged;
                            oZipcontrolNew.Dispose();
                            oZipcontrolNew = null;
                        }

                        if (oZipcontrol != null)
                        {
                            //SLR30:
                            oZipcontrol.ItemSelectedclick -= oZipcontrol_ItemSelected;
                            oZipcontrol.InternalGridKeyDownclick -= oZipcontrol_InternalGridKeyDown;
                            txt_NewZip.TextChanged -= txtZip_TextChanged;
                            oZipcontrol.Dispose();
                            oZipcontrol = null;
                        }
                    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        oDialogResult = false;
                        this.Cursor = Cursors.Default;
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    }
                    catch (IOException ex)
                    {
                        oDialogResult = false;
                        this.Cursor = Cursors.Default;
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    }
                    catch (Exception ex)
                    {
                        oDialogResult = false;
                        this.Cursor = Cursors.Default;
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    }

                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    oDialogResult = false;
                    this.Cursor = Cursors.Default;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                catch (IOException ex)
                {
                    oDialogResult = false;
                    this.Cursor = Cursors.Default;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                catch (Exception ex)
                {
                    oDialogResult = false;
                    this.Cursor = Cursors.Default;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }

            }
        }

        #endregion

        #region "Clear Temp Image"

        private void DeleteTempImages()
        {
            try
            {
                if (ogloDrivingLicenseCardScanWithOCR !=null && Directory.Exists(ogloDrivingLicenseCardScanWithOCR.TempImageDirPath) == true)
                {

                    pb_BackSide.ImageLocation = string.Empty;
                    pb_BackSide.ImageLocation = "";
                    pb_FrontSide.ImageLocation = string.Empty;
                    pb_FrontSide.ImageLocation = "";
                    if (pb_FrontSide.Image != null)
                    {
                        pb_FrontSide.Image.Dispose();
                        pb_FrontSide.Image = null;
                    }

                    if (pb_FrontSide != null)
                    {
                        pb_FrontSide.Dispose();
                        pb_FrontSide = null;
                    }

                    if (pb_BackSide.Image != null)
                    {
                        pb_BackSide.Image.Dispose();
                        pb_BackSide.Image = null;
                    }

                    if (pb_BackSide != null)
                    {
                        pb_BackSide.Dispose();
                        pb_BackSide = null;
                    }
                    
                    //Face Image
                    if (pbFaceImage.Image != null)
                    {
                        pbFaceImage.Image.Dispose();
                        pbFaceImage.Image = null;
                    }

                    if (Pic_NewPatient != null)
                    {
                        Pic_NewPatient.Dispose();
                        Pic_NewPatient = null;
                    }

                    if (PicNew != null)
                    {
                        PicNew.Dispose();
                        PicNew = null;
                    }
                    DirectoryInfo oDirInfo = new DirectoryInfo(ogloDrivingLicenseCardScanWithOCR.TempImageDirPath);
                    foreach (FileInfo oFile in oDirInfo.GetFiles())
                    {
                        try
                        {
                            File.Delete(oFile.FullName);
                        }
                        catch (IOException ioEX)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ioEX.ToString(), false);
                        }

                    }
                    if (oDirInfo != null)
                    {
                        oDirInfo = null;
                    }
                    
                }
                else if (ogloInsuranceCardScanWithOCR!=null && Directory.Exists(ogloInsuranceCardScanWithOCR.TempImageDirPath) == true)
                {

                    pb_BackSide.ImageLocation = string.Empty;
                    pb_BackSide.ImageLocation = "";
                    pb_FrontSide.ImageLocation = string.Empty;
                    pb_FrontSide.ImageLocation = "";
                    if (pb_FrontSide.Image != null)
                    {
                        pb_FrontSide.Image.Dispose();
                        pb_FrontSide.Image = null;
                    }

                    if (pb_FrontSide != null)
                    {
                        pb_FrontSide.Dispose();
                        pb_FrontSide = null;
                    }

                    if (pb_BackSide.Image != null)
                    {
                        pb_BackSide.Image.Dispose();
                        pb_BackSide.Image = null;
                    }

                    if (pb_BackSide != null)
                    {
                        pb_BackSide.Dispose();
                        pb_BackSide = null;
                    }

                    //Face Image
                    if (pbFaceImage.Image != null)
                    {
                        pbFaceImage.Image.Dispose();
                        pbFaceImage.Image = null;
                    }

                    if (Pic_NewPatient != null)
                    {
                        Pic_NewPatient.Dispose();
                        Pic_NewPatient = null;
                    }

                    if (PicNew != null)
                    {
                        PicNew.Dispose();
                        PicNew = null;
                    }
                    DirectoryInfo oDirInfo = new DirectoryInfo(ogloInsuranceCardScanWithOCR.TempImageDirPath);
                    foreach (FileInfo oFile in oDirInfo.GetFiles())
                    {
                        try
                        {
                            File.Delete(oFile.FullName);
                        }
                        catch (IOException ioEX)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ioEX.ToString(), false);
                        }

                    }
                    if (oDirInfo != null)
                    {
                        oDirInfo = null;
                    }

                }
                if (ogloDrivingLicenseCardScanWithOCR != null)
                    ogloDrivingLicenseCardScanWithOCR = null;
                if (ogloInsuranceCardScanWithOCR != null)
                    ogloInsuranceCardScanWithOCR = null; 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region "Insurance Browse Button Events"

        private void btnInsBrowse_Click(object sender, EventArgs e)
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
                //SLR30:
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch { }

                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }

                }
                catch { }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Insurance, false, this.Width);
            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = "Insurance Plans";


            //_CurrentControlType = gloListControl.gloListControlType.Insurance;
            oListControl.strSearchText = cmbIns_PlanProvider.Text.Trim();
            //SLR30:
            oListControl.ItemSelectedClick -= oListControl_ItemSelectedClick;
            oListControl.ItemClosedClick -= oListControl_ItemClosedClick;

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            pnlToolStrip.Visible = false;
            pnlCardLeft.Visible = false;
            this.Controls.Add(oListControl);


            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
            //btnInsBrowse.Focus();
        }

        private void btnInsBrowse_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloCardScanning.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btnInsBrowse_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloCardScanning.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                //cmbIns_PlanProvider.Items.Clear();
                cmbIns_PlanProvider.DataSource = null;
                cmbIns_PlanProvider.Items.Clear();

                //txtInsurance.Tag = null;

                if (oListControl.SelectedItems.Count > 0)
                {
                    DataTable dtSel = new DataTable();
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {

                        if (dtSel != null)
                        {

                            dtSel.Columns.Add("sName", typeof(string));
                            dtSel.Columns.Add("nContactID", typeof(Int64));
                            DataRow dr = dtSel.NewRow();
                            dr["sName"] = oListControl.SelectedItems[i].Description;
                            dr["nContactID"] = oListControl.SelectedItems[i].ID;
                            dtSel.Rows.InsertAt(dr, 0);
                            dtSel.AcceptChanges();

                            cmbIns_PlanProvider.DataSource = dtSel;
                            cmbIns_PlanProvider.DisplayMember = "sName";
                            cmbIns_PlanProvider.ValueMember = "nContactID";
                            cmbIns_PlanProvider.SelectedIndex = 0;

                            lbl_ScanMessage.Visible = false;
                        }

                    }


                    pnlToolStrip.Visible = true;
                    pnlCardLeft.Visible = true;
                }
            }
            catch (Exception ex)
            {
                pnlToolStrip.Visible = true;
                pnlCardLeft.Visible = true;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
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
                //SLR30:
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch { }

                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }

                }
                catch { }
                oListControl.Dispose();
                oListControl = null;
            }
            pnlToolStrip.Visible = true;
            pnlCardLeft.Visible = true;
        }

        #endregion

        #region "Date Validation Function"

        private bool IsValidDate(string DOB)
        {
            Int32 year = 0; Int32 month = 0; Int32 day = 0;
            string[] _Date = DOB.Split('/');
            if (_Date.Length == 3)
            {
                for (int i = 0; i < 3; i++)
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
                            if (_Date[i].Trim().Length == 4)
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

                if (day > 31) return false;

                if (day >= 29)
                {
                    if (month == 2)
                    {
                        if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                        {
                            if (day > 29)
                                return false;
                            else
                                return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    else
                    {

                        //SLR30:Changes
                        if ((day > 30) && (month == 4 || month == 6 || month == 9 || month == 11))
                            return false;
                        else
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

        #endregion

        public void DisposeCardImages()
        {
            if (cardBackImage != null)
            {
                cardBackImage.Dispose();
                cardBackImage = null;
            }
            if (cardFrontImage != null)
            {
                cardFrontImage.Dispose();
                cardFrontImage = null;
            }
            if (PhotoImage != null)
            {
                PhotoImage.Dispose();
                PhotoImage = null;
            }
        }

        private void frmScanCard_New_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteTempImages();           
        }
       
    }

}