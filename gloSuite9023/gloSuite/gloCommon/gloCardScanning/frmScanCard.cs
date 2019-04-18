using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;



namespace gloCardScanning
{
    internal partial class frmScanCard : Form
    {

        #region " Libraries Declarations "

        //for Image
        public NetScanW.CImageClass mImage;
        public NetScanWex.CImageClass mImageEx;

        //for Data Parse & refining
        public NetScanW.IdDataClass mIdData;
        public NetScanWex.IdDataClass mIdDataEx;

        //for Scanner activity
        public NetScanW.SLibExClass mSLib;
        public NetScanWex.SLibExClass mSLibEx;

        public NetChequeCOM.ChequeClass chkCls;         

        private MEDICSDKCOMLib.Med mMedCom;

        #endregion " Libraries Declarations "

        #region " Declarations existing "

        private CardScanType _CardScanType = CardScanType.None;
        string _databaseconnectionstring = "";
        string _MessageBoxCaption = String.Empty;
      //  private bool _IsCardScanned = false;
        public string _ErrorMessage = "";
        string _RootPath;
        Int64 _PatientID;
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;

        string _CardFrontImagePath = "";
        string _CardFaceImagePath = "";
        string _CardBackImagePath = "";
        string _ChequeFrontImagePath = "";
        string _ChequeBackImagePath = "";
        public bool IsSDKLoaded = false; 

        #endregion " Declarations existing "

        #region " Constructor "

        public frmScanCard(string RootPath, Int64 PatientID, string databaseconnectionstring)
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

            _CardScanType = CardScanType.InsuranceCard;
             
            _RootPath = RootPath;
            _PatientID = PatientID;
            _databaseconnectionstring = databaseconnectionstring;

            if (_PatientID <= 0)
            {
                tsb_Cheque.Enabled = false;

            }
        }

        #endregion " Constructor "

        #region " Form Load Event "

        private void frmCardScanning_Load(object sender, EventArgs e)
        {
            _CardScanType = CardScanType.DrivingLicense;
            pnlCheck.SendToBack();
            pnlLicenseDetails.BringToFront();
            FillProviders();
            txtIns_DOB.Enabled=true;

            txtLicID.Focus();
            txtLicID.Select();

            txtIns_PayerID.Focus();
            txtIns_PayerID.Select();

            txtCheckNo.Focus();
            txtCheckNo.Select();

            
        }

        #endregion

        #region " Load Unload Sdk "

        private bool Load_DLID_Sdk()
        {
            int ret;
            bool retVal = false;
            try
            {
                mSLib = new NetScanW.SLibExClass();
                mSLibEx = new NetScanWex.SLibExClass();
                mSLibEx.DefaultScanner = gloCSlibconst.CSSN_3000;
                ret = mSLib.InitLibrary(ClsgloScanConstants.LICENSE_VALUE);
                if (ret != ClsgloScanConstants.LICENSE_VALID)
                { if (CheckInitialization(ret) == false) { return false; } }

                mIdData = new NetScanW.IdDataClass();
                mIdDataEx = new NetScanWex.IdDataClass();
                ret = mIdData.InitLibrary(ClsgloScanConstants.LICENSE_VALUE);
                if (ret != ClsgloScanConstants.LICENSE_VALID)
                { if (CheckInitialization(ret) == false) { return false; } }

                mImage = new NetScanW.CImageClass();
                mImageEx = new NetScanWex.CImageClass();
                ret = mImage.InitLibrary(ClsgloScanConstants.LICENSE_VALUE);
                if (ret != ClsgloScanConstants.LICENSE_VALID)
                { if (CheckInitialization(ret) == false) { return false; } }

                if (ret == ClsgloScanConstants.LICENSE_VALID || ret == gloCSlibconst.SLIB_LIBRARY_ALREADY_INITIALIZED)
                { retVal = true; }
                else
                { retVal = false; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                retVal = false;
            }
            return retVal;
        }

        private bool Load_Medic_Sdk()
        {
            
            bool retValue = false ;
            try
            {
                mMedCom = new MEDICSDKCOMLib.Med();
                retValue = Convert.ToBoolean(mMedCom.InitSdk(ClsgloScanConstants.LICENSE_VALUE));

                if (retValue == false)
                {
                    MessageBox.Show("Error Initializing Medic SDK", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                retValue = false;
            }
            return retValue;
        }

        private bool Load_Check_Sdk()
        {
            int _InitializeValue = 0;
            bool _retValue = false;

            try
            {
                chkCls = new NetChequeCOM.ChequeClass();
                _InitializeValue = chkCls.InitChequeLibrary(ClsgloScanConstants.LICENSE_VALUE);
                if (_InitializeValue != ClsgloScanConstants.LICENSE_VALID)
                { if (CheckInitialization(_InitializeValue) == false) { return false; } }

                if (_InitializeValue == ClsgloScanConstants.LICENSE_VALID || _InitializeValue == gloCSlibconst.SLIB_LIBRARY_ALREADY_INITIALIZED)
                { _retValue = true; }
                else
                { _retValue = false; }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _retValue;
        }

        private bool CheckInitialization(int Value)
        {
            bool _RetValue = true;
            try
            {
                switch (Value)
                {
                    case ClsgloScanConstants.LICENSE_EXPIRED:
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Error: License Expired! - Library not loaded (Image)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _RetValue = false;
                        break;
                    case gloCSlibconst.SLIB_LIBRARY_ALREADY_INITIALIZED:
                        this.Cursor = Cursors.Default;
                        //MessageBox.Show("Error: Library is already loaded.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RetValue = true;
                        break;
                    case gloCSlibconst.SLIB_ERR_SCANNER_NOT_FOUND:
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("The scanner is not connected to the PC. Connect scanner properly.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RetValue = false;
                        break;
                    case gloCSlibconst.SLIB_ERR_DRIVER_NOT_FOUND:
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Error: The scanner's driver was not found. Re-install the driver and re-start the application.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RetValue = false;
                        break;
                    case ClsgloScanConstants.LICENSE_INVALID:
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Error: License Invalid for SDK (Image)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _RetValue = false;
                        break;
                    case ClsgloScanConstants.LICENSE_DOES_NOT_MATCH_LIBRARY:
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Error: License Invalid for ID Library!  (Image)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _RetValue = false;
                        break;
                    case gloCSlibconst.GENERAL_ERR_PLUG_NOT_FOUND:
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Error: The scanner is not attached or license expired.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RetValue = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                _RetValue = false;
            }
            finally
            {
               // if (_RetValue == false) { this.Close(); }
            }
            return _RetValue;
        }

        private void UnLoadSdk()
        {
            
                try
                {
                    //The Slib object should be the first to uninitilaized
                    if (mSLibEx != null) { int val = mSLibEx.UnInit(); mSLibEx = null; }
                    if (mSLib != null) { mSLib = null; }
                    if (mIdDataEx != null) { mIdDataEx = null; }
                    if (mIdData != null) { mIdData = null; }
                    if (mImageEx != null) { mImageEx = null; }
                    if (mImage != null) { mImage = null; }
                    if (mMedCom != null) { mMedCom = null; }
                    if (chkCls != null) { chkCls = null; }
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

        #endregion " Load Unload Sdk "

        #region " Card Tool Strip Menu Item Click "

        private void tsb_InsuranceCard_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_FrontSide.Image != null || pb_BackSide.Image != null || pbFaceImage.Image != null)
                {
                    DialogResult _DlgClearData = DialogResult.None;
                    _DlgClearData = MessageBox.Show("Do you want to save changes ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_DlgClearData == DialogResult.Yes)
                    { return; }
                }
                ClearData();
                _CardScanType = CardScanType.InsuranceCard;
                pnlCheck.SendToBack();
                pnlInsuranceDetails.BringToFront();
                ScanCard();
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

        private void tsb_DriversLicense_Click(object sender, EventArgs e)
        {
            try
            {
                if (pb_FrontSide.Image != null || pb_BackSide.Image != null || pbFaceImage.Image != null)
                {
                    DialogResult _DlgClearData = DialogResult.None;
                    _DlgClearData = MessageBox.Show("Do you want to save changes ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_DlgClearData == DialogResult.Yes)
                    { return; }
                }
                ClearData();
                pnlCheck.SendToBack();
                pnlLicenseDetails.BringToFront();
                _CardScanType = CardScanType.DrivingLicense;
                //pnlLicenseDetails.BringToFront();
                ScanCard();
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

        private void tsb_ClearData_Click(object sender, EventArgs e)
        {
            try
            {
                if (_CardScanType == CardScanType.InsuranceCard)
                {
                    if (pb_FrontSide.Image != null
                         //.Insurance Card Data
                         || txtIns_PayerID.Text != ""||
                            txtIns_PlanProvider.Text != ""||
                            txtIns_MemberID.Text != ""||
                            txtIns_MemberName.Text != ""||
                            txtIns_PatientFirstName.Text != ""||
                            txtIns_PatientLastName.Text != "" ||
                            txtIns_PatientCode.Text != ""||
                            txtIns_SSN.Text != ""||
                            txtIns_GroupNo.Text != ""||
                            //txtIns_DOB.Text != ""||
                            //txtIns_EffectiveDate.Text != ""||
                            //txtIns_ExpiryDate.Text != ""||
                            txtIns_CopayER.Text != ""||
                            txtIns_CopayOV.Text != ""||
                            txtIns_CopaySP.Text != ""||
                            txtIns_CopayUC.Text != ""||
                            txtIns_Address.Text != ""||
                            txtIns_City.Text != ""||
                            txtIns_State.Text != ""||
                            txtIns_Zip.Text != ""||
                            txtIns_ContractNo.Text != "" ||
                            cmb_Ins_Providers.SelectedIndex > 0||

                            txtAccountNo.Text != ""||
                            txtCheckAmount.Text != "0.00"||
                            txtCheckNo.Text != ""||
                            txtIssuingBank.Text != ""||
                            txtIssuingCompany.Text != ""||
                            txtMICR.Text != ""||
                            txtRoutingNo.Text != ""
                        )
                       {
                            DialogResult DlgRst = DialogResult.None;
                            DlgRst = MessageBox.Show("Are you sure you want to clear data ? ", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (DlgRst == DialogResult.OK)
                            { ClearData(); }
                        }
                }
                if (_CardScanType == CardScanType.DrivingLicense)
                {
                    if (pb_FrontSide.Image != null ||
                        //.DL/ID Data
                        txtLicID.Text != ""||
                        txtLicSSn.Text != ""||
                        txtLicNo.Text != ""||
                        txtLicFirstName.Text != ""||
                        txtLicMiddleName.Text != ""||
                        txtLicLastName.Text != ""||
                        //txtLicDOB.Text != ""||
                        txtLic_PatientCode.Text != ""||
                        //txtLicSex.Text = "";
                        txtLicAddress.Text != ""||
                        txtLicCity.Text != ""||
                        txtLicState.Text != ""||
                        txtLicZip.Text != ""||
                        //txtLicCounty.Text = "";
                        cmb_DLID_Providers.SelectedIndex > 0||
                        txtAccountNo.Text != ""||
                        txtCheckAmount.Text != "0.00"||
                        txtCheckNo.Text != ""||
                        txtIssuingBank.Text != ""||
                        txtIssuingCompany.Text != ""||
                        txtMICR.Text != ""||
                        txtRoutingNo.Text != ""
                       
                        )
                    {
                        DialogResult DlgRst = DialogResult.None;
                        DlgRst = MessageBox.Show("Are you sure you want to clear data ? ", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (DlgRst == DialogResult.OK)
                        { ClearData(); }
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
            if (SaveData() == true)
            {
                this.Close();
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                
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
            finally 
            {
                this.Close();
            }
        }

        private void tsb_Cheque_Click(object sender, EventArgs e)
        {
           
            try
            { pnlCheck.BringToFront();
            _CardScanType = CardScanType.Cheque;
            pnlLicenseDetails.BringToFront();
            ScanCard();
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

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            try
            {
                PrintCards();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        #endregion " Card Tool Strip Menu Item Click "

        #region " Private & Public Methods "

        private void LoadSettings()
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting(Application.StartupPath);
            try
            {

                #region " 1. Get Resolution settings for Insurance "

                if (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionInsurance") != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionInsurance"))
                    {
                        case "300":
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propResolution = 300; }
                            break;
                        case "600":
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propResolution = 600; }
                            break;
                    }

                }
                else //set default
                {
                    if (axms_InsuranceCard != null) { axms_InsuranceCard.propResolution = 600; }
                }

                #endregion 

                #region " 2. Resolution settings for Driver License "

                if (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionLicense") != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionLicense"))
                    {
                        case "300":
                            if (mSLib != null) { mSLib.Resolution = 300; }
                            break;
                        case "600":
                            if (mSLib != null) { mSLib.Resolution = 600; }
                            break;
                    }

                }
                else //set default
                {
                    if (mSLib != null) { mSLib.Resolution = 300; }
                }

                #endregion

                #region " 3.Get ColorScheme Settings for Scan "

                if (oSettings.ReadSettings_XML("CardScannerSettings", "ColorScheme") != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ColorScheme"))
                    {
                        case "4"://true color
                            if (mSLib != null) { mSLib.ScannerColorScheme = 4; }
                            break;
                        case "1"://gray 
                            if (mSLib != null) { mSLib.ScannerColorScheme = 1; }
                            break;
                        case "2"://black & white
                            if (mSLib != null) { mSLib.ScannerColorScheme = 2; }
                            break;
                    }
                }
                else
                {
                    if (mSLib != null) { mSLib.ScannerColorScheme = 4; } //true color
                }

                #endregion

                #region " 4. Get Scan Mode Settings "

                if (oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex") != "")
                {
                    string _scanduplex = oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex");
                    if (_scanduplex == "1")
                    {
                        if (mSLib != null) { mSLibEx.Duplex = 1; }
                        if (axms_InsuranceCard != null) { axms_InsuranceCard.propDuplexScan = true; }
                    }
                    else
                    {
                        if (mSLib != null) { mSLibEx.Duplex = 0; }
                        if (axms_InsuranceCard != null) { axms_InsuranceCard.propDuplexScan = false; }
                    }
                }
                else
                {
                    if (mSLibEx != null) { mSLibEx.Duplex = 1; }
                    if (axms_InsuranceCard != null) { axms_InsuranceCard.propDuplexScan = true; }
                }

                #endregion

                #region " 5. Scan Size Settings "

                if (mSLibEx != null) { mSLib.ScanHeight = 360; }
                if (mSLibEx != null) { mSLib.ScanWidth = 220; }
                if (axms_InsuranceCard != null) { axms_InsuranceCard.propScanHeight = 360; }
                if (axms_InsuranceCard != null) { axms_InsuranceCard.propScanWidth = 220; }

                #endregion

                #region  " 6. Load Default Scanner "

                if (oSettings.ReadSettings_XML("CardScannerSettings", "DefaultScanner").Trim() != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "DefaultScanner").Trim())
                    {
                        case "CSSN_1000":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_1000; }
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_1000; }
                            break;
                        case "CSSN_4000":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_4000; }
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_4000; }
                            break;
                        case "CSSN_600":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_600;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_600; }
                            break;
                        case "CSSN_800":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_800;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_800; }
                            break;
                        case "CSSN_800E":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_800E;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_800E; }
                            break;
                        case "CSSN_800EN":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_800EN;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_800EN; }
                            break;
                        case "CSSN_800G":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_800G;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_800G; }
                            break;
                        case "CSSN_800N":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_800N;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_800N; }
                            break;
                        case "CSSN_800DX":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_800DX;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_800DX; }
                            break;
                        case "CSSN_800DXN":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_800DXN;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_800DXN; }
                            break;
                        case "CSSN_2000":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_2000;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_2000; }
                            break;
                        case "CSSN_2000N":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_2000N;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_2000N; }
                            break;
                        case "CSSN_3000":
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_3000;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_3000; }
                            break;
                        case "CSSN_TWN" :
                            if (mSLibEx != null) { mSLibEx.DefaultScanner = gloCSlibconst.CSSN_TWN;}
                            if (axms_InsuranceCard != null) { axms_InsuranceCard.propScannerType = gloCSlibconst.CSSN_TWN; }
                            break;
                    }
                }

                #endregion  " 6. Load Default Scanner "


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); }
            }
        }

        private void ScanCard()
        {

            int retVal = 0;
            string _TempProcessDirPath = "";
            string _IntFileName = "";
            int status;
            int lastError;

            if (_CardScanType != CardScanType.None)
            {
                if (Directory.Exists(_RootPath) == true)
                {

                    #region " .Check for Paper In Tray & Perform Scan Operation "

                        #region " .Load Settings & Set Image File Path "

                        
                       // LoadSettings();

                        _TempProcessDirPath = _RootPath + "\\" + _PatientID;
                        if (Directory.Exists(_TempProcessDirPath) == false)
                        { Directory.CreateDirectory(_TempProcessDirPath); }

                        _IntFileName = string.Format(DateTime.Now.ToString(), "mmddyyyyhhmmtt").ToString().Replace("/", "").Replace(":", "");

                        _CardFaceImagePath = _TempProcessDirPath + "\\" + _IntFileName + "-Face.jpg";
                        _CardFrontImagePath = _TempProcessDirPath + "\\" + _IntFileName + "-CardFront.jpg";
                        _CardBackImagePath = _TempProcessDirPath + "\\" + _IntFileName + "-CardFront-back.jpg";

                        _ChequeFrontImagePath = _TempProcessDirPath + "\\" + _IntFileName + "_ChqFace.jpg";
                        _ChequeBackImagePath = _TempProcessDirPath + "\\" + _IntFileName + "_ChqBack.jpg";

                        #endregion

                        if (_CardScanType == CardScanType.DrivingLicense)
                        {
                            ClearData();
                            UnLoadSdk();
                            if (Load_DLID_Sdk() == true)
                            {
                                LoadSettings();
                                if (mSLib.PaperInTray > 0)
                                {

                                    #region  " .Check for Calibration & Scanner Validation "

                                    if (Convert.ToBoolean(mSLib.IsNeedCalibration))
                                    {
                                        MessageBox.Show("Calibrate the scanner", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                    status = mSLib.IsScannerValid;
                                    if (status == gloCSlibconst.SLIB_ERR_INVALID_SCANNER || mSLib.LastErrorStatus == gloCSlibconst.SLIB_ERR_SCANNER_NOT_FOUND)
                                    {
                                        MessageBox.Show("The scanner is not found. Verify that the scanner is connected", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                    #endregion

                                    retVal = mSLib.ScanToFileEx(_CardFrontImagePath);

                                    #region " .Check if any Scan error present "

                                    lastError = mSLib.LastErrorStatus;
                                    if (lastError != gloCSlibconst.SLIB_TRUE)
                                    {
                                        switch (lastError)
                                        {
                                            case ClsgloScanConstants.LICENSE_EXPIRED:
                                                MessageBox.Show("ERROR: Licence expired!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case ClsgloScanConstants.LICENSE_INVALID:
                                                MessageBox.Show("ERROR: Licence does not match this type of library", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_SCANNER_GENERAL_FAIL:
                                                MessageBox.Show("ERROR: General failure", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_CANCELED_BY_USER:
                                                MessageBox.Show("ERROR: Scan canceled by the user", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_SCANNER_NOT_FOUND:
                                                MessageBox.Show("ERROR: Scanner not found", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_HARDWARE_ERROR:
                                                MessageBox.Show("ERROR: Hardware failure", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_PAPER_FED_ERROR:
                                                MessageBox.Show("ERROR: Paper feed problem", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_SCANABORT:
                                                MessageBox.Show("ERROR: Scanner aborted", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_NO_PAPER:
                                                MessageBox.Show("ERROR: No paper found", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_PAPER_JAM:
                                                MessageBox.Show("ERROR: Paper Jammed", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_FILE_IO_ERROR:
                                                MessageBox.Show("ERROR: Hardware IO failure", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_PRINTER_PORT_USED:
                                                MessageBox.Show("ERROR: Printer port already used by other utility (for parallel models only)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_OUT_OF_MEMORY:
                                                MessageBox.Show("ERROR: Out of memory", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_INVALID_SCANNER:
                                                MessageBox.Show("ERROR: Scanner type is not supported", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                        }
                                    }

                                    #endregion " .Check if any Scan error present "
                                }
                                else
                                {
                                    MessageBox.Show("Insert the card to be scanned", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                        }
                        else if (_CardScanType == CardScanType.Cheque)
                        {
                            UnLoadSdk();
                            if (Load_Check_Sdk() == true)
                            {
                                status = chkCls.IsScannerValid;
                                if (status == gloCSlibconst.SLIB_ERR_INVALID_SCANNER)
                                {
                                    MessageBox.Show("The scanner is not found. Verify that the scanner is connected", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else if (Convert.ToBoolean(chkCls.IsNeedCalibration))
                                {
                                    MessageBox.Show("Calibrate the scanner", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else if (chkCls.PaperInTray > 0)
                                {
                                    chkCls.ScanHeight = -1;
                                    chkCls.ScanWidth = -1;
                                    retVal = chkCls.ScanToFileEx(_ChequeFrontImagePath);

                                    if (retVal > 0 && _CardScanType != CardScanType.CardImages && _CardScanType != CardScanType.None)
                                    {
                                        //_IsCardScanned = true;
                                        ReadScannedData(_ChequeFrontImagePath, "", "");
                                    }
                                    else if (_CardScanType == CardScanType.CardImages && _PatientID > 0)
                                    {

                                        //_IsCardScanned = true;
                                        ReadScannedData(_ChequeFrontImagePath, "", "");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Enter the check to be scanned", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else if (_CardScanType == CardScanType.InsuranceCard)
                        {
                            ClearData();
                            UnLoadSdk();
                            bool _PaperInTray = false;
                            Load_DLID_Sdk();
                            _PaperInTray = Convert.ToBoolean(mSLib.PaperInTray);
                            UnLoadSdk();

                            if (_PaperInTray)
                            {
                                Load_Medic_Sdk();
                                LoadSettings();

                                #region  " .Check for Calibration & Scanner Validation "

                                if (Convert.ToBoolean(axms_InsuranceCard.propIsScannerValid) == false)
                                {
                                    MessageBox.Show("The scanner is not found. Verify that the scanner is connected", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (Convert.ToBoolean(axms_InsuranceCard.propIsNeedCalibration) == true)
                                {
                                    MessageBox.Show("Calibrate the Scanner", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                #endregion

                                retVal = axms_InsuranceCard.ScanToFileEx(_CardFrontImagePath);
                            }
                            else
                            {
                                MessageBox.Show("Insert the card to be scanned", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        if (retVal > 0 && _CardScanType != CardScanType.CardImages && _CardScanType != CardScanType.None)
                        {
                            //_IsCardScanned = true;
                            ReadScannedData(_CardFrontImagePath, _CardFaceImagePath, _CardBackImagePath);
                        }
                        else if (_CardScanType == CardScanType.CardImages && _PatientID > 0)
                        {
                            bool _IsCardSaved = false;
                            gloCardScanning ogloCardScanning = new gloCardScanning(_databaseconnectionstring);
                            if (pb_FrontSide.Image != null && pb_BackSide.Image != null)
                            {
                                _IsCardSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                this.Close();
                            }
                        }

                    #endregion " .Check for Paper In Tray & Perform Scan Operation "
                }
            }
        }

        private void ReadScannedData(string CardFrontImageName, string FaceImageName, string CardBackImageName)
        {

            int stateId = 0;
            int retVal = 0;
            int img1 = 0;
            //int img2 = 0;
            int angle1 = 0;
            int angle2 = 0;
            //int statId = 0;

            try
            {
                if (_CardScanType == CardScanType.DrivingLicense)
                {
                    retVal = mIdDataEx.DetectProcessAndCompareEX2("", "", -1, ref img1, ref angle1, ref angle2, 1, 0);
                    stateId = retVal;

                    #region " .Retrive Images for Drivers License "

                    //if (retVal > 0)
                    //{
                        #region ".1 Face Image "

                        retVal = 0;
                        retVal = mIdData.GetFaceImage(CardFrontImageName, FaceImageName, stateId);
                    
                        if (File.Exists(FaceImageName) == true)
                        {
                            int nHeight = 0;
                            int nWidth = 0;
                            Image _faceImage = Image.FromFile(FaceImageName);
                            nHeight = _faceImage.Height;
                            nWidth = _faceImage.Width;
                            if (nWidth > 140) { nWidth = 140; }
                            if (nHeight > 150) { nHeight = 150; }
                            _faceImage = new Bitmap(_faceImage, new Size(nWidth, nHeight));
                            pbFaceImage.Image = _faceImage;
                            //if (_faceImage != null) 
                            //{ _faceImage.Dispose(); }
                        }

                        #endregion

                        #region ".2 Card Front Side Image "

                        if (File.Exists(CardFrontImageName) == true)
                        { pb_FrontSide.Image = Image.FromFile(CardFrontImageName); }

                        #endregion

                        #region ".3 Card Back Side Image "

                        if (File.Exists(CardBackImageName) == true)
                        { pb_BackSide.Image = Image.FromFile(CardBackImageName); }

                        #endregion

                        #region ".4 Setting Patients Scanned Information to PatientObject "

                        if (mIdData != null)
                        {
                            mIdData.RefreshData();
                            txtLicID.Text = mIdData.Id;
                            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                            txtLic_PatientCode.Text = ogloPatient.GeneratePatientCode();
                            if (ogloPatient != null) { ogloPatient.Dispose(); }
                            txtLicNo.Text = mIdData.license;
                            txtLicFirstName.Text = mIdData.NameFirst;
                            txtLicMiddleName.Text = mIdData.NameMiddle;
                            txtLicLastName.Text = mIdData.NameLast;
                            //txtLicDOB.Text = mIdData.DateOfBirth;
                            try
                            {
                                DateTime _dtDOB = DateTime.Now;
                                _dtDOB = Convert.ToDateTime(mIdData.DateOfBirth);
                                txtLicDOB.Text = _dtDOB.ToString("MM/dd/yyyy");
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                                txtLicDOB.Text = mIdData.DateOfBirth;
                            }
                            

                            if (mIdData.Sex.Trim() == "M")
                            { rbLicMale.Checked = true; }
                            else if (mIdData.Sex.Trim() == "F")
                            { rbLicFemale.Checked = true; }
                            else if (mIdData.Sex.Trim() == "")
                            { rbLicOthers.Checked = true; }

                            txtLicSSn.Text = mIdData.SocialSecurity;
                            txtLicCity.Text = mIdData.City;
                            txtLicState.Text = mIdData.State;
                            txtLicZip.Text = mIdData.Zip;
                            //txtLicCounty.Text = mIdData.County;
                            txtLicAddress.Text = mIdData.Address;
                        }

                        #endregion "Setting Patients Scanned Information to PatientObject"
                    //}

                    #endregion

                }
                else if (_CardScanType == CardScanType.Cheque)
                {

                    #region " Retrive Cheque Images "

                    if (File.Exists(CardFrontImageName) == true)
                    { pb_ChequeImage.Image = Image.FromFile(CardFrontImageName); }

                    #endregion

                    retVal = chkCls.ProcessCheque("");
                    if (retVal >= 0)
                    {
                        string _value = "";
                        chkCls.GetChequeData(0,out _value); //0 - CHECK_FIELD_AMOUNT
                        if (_value != "") { txtCheckAmount.Text = _value; }
                        chkCls.GetChequeData(1, out  _value); //1 - CHECK_FIELD_COMPANY
                        txtIssuingCompany.Text = _value;
                        chkCls.GetChequeData(2, out  _value); //2 - CHECK_FIELD_BANK 
                        txtIssuingBank.Text = _value;
                        chkCls.GetChequeData(3, out  _value); //3 - CHECK_FIELD_MICR 
                        txtMICR.Text = _value;
                        chkCls.GetChequeData(4, out  _value); //4 - CHECK_FIELD_CHKNUM 
                        txtCheckNo.Text = _value;
                        chkCls.GetChequeData(5, out  _value); //5 - CHECK_FIELD_ROUTING_NUM
                        txtRoutingNo.Text = _value;
                        chkCls.GetChequeData(6, out  _value); //6 - CHECK_FIELD_ACCOUNT_NUM
                        txtAccountNo.Text = _value; 
                        
                    }
                    
                }
                else if (_CardScanType == CardScanType.InsuranceCard)
                {
                    retVal = axms_InsuranceCard.ProcessMedical("", "", 0);

                    #region " .Retrive Images for Insurance "

                    //if (retVal > 0)
                    //{
                    #region ".1 Card Front Side Image "

                    if (File.Exists(CardFrontImageName) == true)
                    { pb_FrontSide.Image = Image.FromFile(CardFrontImageName); }

                    #endregion

                    #region ".2 Card Back Side Image "

                    if (File.Exists(CardBackImageName) == true)
                    { pb_BackSide.Image = Image.FromFile(CardBackImageName); }

                    #endregion
                    //}

                    #endregion

                    #region " .Setting Patients Scaned Information to PatientObject"

                    txtIns_PayerID.Text = axms_InsuranceCard.propPayerID;

                    //auto complete 
                    FillInsurance(axms_InsuranceCard.propPlanProvider);

                    

                    txtIns_PlanProvider.Text = axms_InsuranceCard.propPlanProvider;
                    txtIns_MemberID.Text = axms_InsuranceCard.propMemberID;
                    txtIns_MemberName.Text = axms_InsuranceCard.propMemberName;
                    txtIns_GroupNo.Text = axms_InsuranceCard.propGroupNumber;
                    //txtIns_DOB.Text = axms_InsuranceCard.propDateOfBirth;
                    try
                    {
                        DateTime _dtDOB = DateTime.Now;
                        _dtDOB = Convert.ToDateTime(axms_InsuranceCard.propDateOfBirth);
                        txtIns_DOB.Text = _dtDOB.ToString("MM/dd/yyyy");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        txtIns_DOB.Text = axms_InsuranceCard.propDateOfBirth;
                    }

                    //txtIns_EffectiveDate.Text = axms_InsuranceCard.propEffectiveDate;
                    try
                    {
                        DateTime _dtDOB = DateTime.Now;
                        _dtDOB = Convert.ToDateTime(axms_InsuranceCard.propEffectiveDate);
                        txtIns_EffectiveDate.Text = _dtDOB.ToString("MM/dd/yyyy");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        txtIns_EffectiveDate.Text = axms_InsuranceCard.propEffectiveDate;
                    }

                    //txtIns_ExpiryDate.Text = axms_InsuranceCard.propExpireDate;
                    try
                    {
                        DateTime _dtDOB = DateTime.Now;
                        _dtDOB = Convert.ToDateTime(axms_InsuranceCard.propExpireDate);
                        txtIns_ExpiryDate.Text = _dtDOB.ToString("MM/dd/yyyy");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        txtIns_ExpiryDate.Text = axms_InsuranceCard.propExpireDate;
                    }

                    #region "Copay Logic"
                      decimal _nCopay = 0;
                      if (axms_InsuranceCard.propCopayUC != null && axms_InsuranceCard.propCopayUC.ToString().Trim() != "" && axms_InsuranceCard.propCopayUC.ToString().Trim() != "0")
                      {
                          _nCopay = Convert.ToDecimal(axms_InsuranceCard.propCopayUC.ToString().Replace("$","").Trim());
                      }
                      if (axms_InsuranceCard.propCopaySP != null && axms_InsuranceCard.propCopaySP.ToString().Trim() != "" && axms_InsuranceCard.propCopaySP.ToString().Trim() != "0")
                      {
                          _nCopay = Convert.ToDecimal(axms_InsuranceCard.propCopaySP.ToString().Replace("$", "").Trim());
                      }
                      if (axms_InsuranceCard.propCopayOV != null && axms_InsuranceCard.propCopayOV.ToString().Trim() != "" && axms_InsuranceCard.propCopayOV.ToString().Trim() != "0")
                      {
                          _nCopay = Convert.ToDecimal(axms_InsuranceCard.propCopayOV.ToString().Replace("$", "").Trim());
                      }
                      if (axms_InsuranceCard.propCopayER != null && axms_InsuranceCard.propCopayER.ToString().Trim() != "" && axms_InsuranceCard.propCopayER.ToString().Trim() != "0")
                      {
                          _nCopay = Convert.ToDecimal(axms_InsuranceCard.propCopayER.ToString().Replace("$", "").Trim());
                      }
                      txtCopay.Text = _nCopay.ToString();
                    #endregion
          

                    #endregion "Setting Patients Sacnned Information to PatientObject"

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool SaveData_Old()
        {
            gloPatient.ScanedPatient oScannedPatient = new gloPatient.ScanedPatient();
            gloPatient.ScanedInsurance oScannedInsurance = new gloPatient.ScanedInsurance();
            gloCardScanning ogloCardScanning = new gloCardScanning(_databaseconnectionstring);
            Int64 _PatId = 0;
            bool _IsDataSaved = false;
            Int64 _TempPatientID = 0;
            DataTable dtPatient=new DataTable();
            //bool _IsSave = false;   
            try
            {
                if (ValidateScannedData() == true)
                {

                    #region //DrivingLicense
                    if (_CardScanType == CardScanType.DrivingLicense)
                    {
                        oScannedPatient = GetScanPatientData();
                        //Added By MaheshB
                        dtPatient = ogloCardScanning.IsPatientExists(oScannedPatient.FirstName, oScannedPatient.LastName, Convert.ToDateTime(oScannedPatient.DOB));
                        if (dtPatient != null)
                        {
                            DialogResult dr;
                            dr = MessageBox.Show("Patient Already Exists. Do you want to Modify? \nYes- To Modify.\nNo - Register New Patient.\nCancel - Do nothing.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                            if (dr==DialogResult.Yes)
                            {
                                //_PatientID = 0;
                                //Mofify Method Opens a dialog.
                                if (dtPatient.Rows.Count > 1) //Select from Multiple Patients
                                {
                                    gloCardScanningPatientList.frmPatientList ofrmpatientlist = new gloCardScanningPatientList.frmPatientList(dtPatient);
                                    ofrmpatientlist.ShowDialog();
                                    ofrmpatientlist.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                                    if (ofrmpatientlist.DialogPatientResult == true)
                                    {
                                        _TempPatientID = ofrmpatientlist.DialogPatientID;
                                        //_IsSave = true;
                                        //_RegisterPatient = false;

                                        ogloCardScanning.ModifyPatient(oScannedPatient, _TempPatientID,pbFaceImage.Image);
                                        _PatientID = _TempPatientID;

                                       #region //Save Scan Information
                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                #endregion
                                       
                                    }
                                }
                                else //Only One Patient Exists.Don't show List
                                {
                                    ogloCardScanning.ModifyPatient(oScannedPatient, Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]),pbFaceImage.Image);
                                    _PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                                    //_IsSave = true;
                                     #region //Save Scan Information
                                    _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                    #endregion
                                }
                               
                            }
                            else if (dr == DialogResult.No)
                            {
                                if (oScannedPatient != null)
                                {
                                    _PatId = RegisterScanPatient(oScannedPatient);

                                    if (_PatId > 0)
                                    {
                                        _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                    }
                                }
                            }
                            


                        }
                        //if ((_IsSave == true) || (dtPatient==null)) //Fires Only on Save Button.
                        //{
                        //    if (_PatientID == 0)
                        //    {

                        //        if (oScannedPatient != null)
                        //        {
                        //            _PatId = RegisterScanPatient(oScannedPatient);

                        //            if (_PatId > 0)
                        //            {
                        //                _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                        //            }
                        //        }
                        //    }
                        //    else if (_PatientID > 0)
                        //    {
                        //        _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                        //    }
                        //}
                    } 
                    #endregion //DrivingLicense

                    else if (_CardScanType == CardScanType.Cheque || _CardScanType == CardScanType.CardImages)
                    { 
                        if (_PatientID > 0)
                        {
                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_ChequeImage.Image,null,DateTime.Now, _CardScanType.GetHashCode(), "");
                        }
                    }
                    else if (_CardScanType == CardScanType.InsuranceCard)
                    {
                        oScannedInsurance = GetScanInsuranceData();
                        //_PatientID = ogloCardScanning.IsPatientExists(oScannedInsurance.PatientFirstName, oScannedInsurance.PatientLastName, oScannedInsurance.DOB);
                        //if (_PatientID > 0)
                        //{
                        //    if (DialogResult.No == MessageBox.Show("Patient Already Exists. Do you want to Modify?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        //    {
                        //        _PatientID = 0;                               
                        //    }


                        //}
                        if (oScannedInsurance != null)
                        {
                            if (_PatientID == 0)
                            {
                                _PatId = RegisterScanPatient(oScannedInsurance);
                                _PatientID = _PatId;
                            }

                            if (_PatientID > 0)
                            {
                                Int64 _ContactId = 0;
                                Int64 _PatientInsId = 0;
                                _ContactId = AddInsuranceToContact(oScannedInsurance.PlanProvider, oScannedInsurance.PayerID);
                                _PatientInsId = AddInsuranceToExistingPatient(oScannedInsurance);
                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                            }
                        }
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("Component required for card scanning is not available.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _IsDataSaved = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show("File required for card scanning is not found.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _IsDataSaved = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _IsDataSaved = false;
            }
            finally
            {
                if (oScannedPatient != null) { oScannedPatient.Dispose(); }
                if (oScannedInsurance != null) { oScannedInsurance.Dispose(); }
                if (ogloCardScanning != null) { ogloCardScanning.Dispose(); }
            }
            return _IsDataSaved;
        }

        private bool SaveData_WorkingBefore_Criteria()
        {
            gloPatient.ScanedPatient oScannedPatient = new gloPatient.ScanedPatient();
            gloPatient.ScanedInsurance oScannedInsurance = new gloPatient.ScanedInsurance();
            gloCardScanning ogloCardScanning = new gloCardScanning(_databaseconnectionstring);
            Int64 _PatId = 0;
            bool _IsDataSaved = false;
            Int64 _TempPatientID = 0;
            DataTable dtPatient = new DataTable();
            //bool _IsSave = false;
            try
            {
                if (ValidateScannedData() == true)
                {

                    #region //DrivingLicense
                    if (_CardScanType == CardScanType.DrivingLicense)
                    {
                        oScannedPatient = GetScanPatientData();
                        //Added By MaheshB
                        if (_PatientID == 0)
                        {
                            dtPatient = ogloCardScanning.IsPatientExists(oScannedPatient.FirstName, oScannedPatient.LastName, Convert.ToDateTime(oScannedPatient.DOB));
                            if (dtPatient != null)
                            {
                                DialogResult dr;
                                dr = MessageBox.Show("Patient Already Exists. Do you want to Modify? \nYes- To Modify.\nNo - Register New Patient.\nCancel - Do nothing.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                if (dr == DialogResult.Yes)
                                {
                                    //_PatientID = 0;
                                    //Mofify Method 
                                    if (dtPatient.Rows.Count > 1) //Select from Multiple Patients
                                    {
                                        gloCardScanningPatientList.frmPatientList ofrmpatientlist = new gloCardScanningPatientList.frmPatientList(dtPatient); //Opens a dialog.
                                        ofrmpatientlist.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                                        ofrmpatientlist.ShowDialog();

                                        if (ofrmpatientlist.DialogPatientResult == true)
                                        {
                                            _TempPatientID = ofrmpatientlist.DialogPatientID;
                                            //_IsSave = true;
                                            //_RegisterPatient = false;

                                            ogloCardScanning.ModifyPatient(oScannedPatient, _TempPatientID, pbFaceImage.Image);
                                            _PatientID = _TempPatientID;

                                            #region //Save Scan Information
                                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                            #endregion

                                        }
                                    }
                                    else //Only One Patient Exists.Don't show List
                                    {
                                        if (dtPatient != null)
                                        {
                                            if (dtPatient.Rows.Count > 0)
                                            {
                                                ogloCardScanning.ModifyPatient(oScannedPatient, Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]), pbFaceImage.Image);
                                                _PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                                                //_IsSave = true;
                                                #region //Save Scan Information
                                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                                #endregion
                                            }
                                        }
                                    }

                                }
                                else if (dr == DialogResult.No)//Save as new Patient.
                                {
                                    if (oScannedPatient != null)
                                    {
                                        _PatId = RegisterScanPatient(oScannedPatient);

                                        if (_PatId > 0)
                                        {
                                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                        }
                                    }
                                }



                            }
                            else 
                            {
                                if (oScannedPatient != null)
                                {
                                    _PatId = RegisterScanPatient(oScannedPatient);

                                    if (_PatId > 0)
                                    {
                                        _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                    }
                                }
                            }
                            //if ((_IsSave == true) || (dtPatient==null)) //Fires Only on Save Button.
                            //{
                            //    if (_PatientID == 0)
                            //    {

                            //        if (oScannedPatient != null)
                            //        {
                            //            _PatId = RegisterScanPatient(oScannedPatient);

                            //            if (_PatId > 0)
                            //            {
                            //                _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                            //            }
                            //        }
                            //    }
                            //    else if (_PatientID > 0)
                            //    {
                            //        _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                            //    }
                            //}
                        }
                        else //For Selected Patient..Bottom Scan Option.
                        {
                            DialogResult dr;
                            dr = MessageBox.Show("Do you want to modify patient Address and Photo ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dr == DialogResult.Yes)
                            {
                                ogloCardScanning.ModifyPatient(oScannedPatient, _PatientID,pbFaceImage.Image);
                                #region //Save Scan Information
                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                #endregion
                            }
                        }
                    }
                    #endregion //DrivingLicense

                    #region  //CardImages and Cheque
                    else if (_CardScanType == CardScanType.Cheque || _CardScanType == CardScanType.CardImages) 
                    {
                        if (_PatientID > 0)
                        {
                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_ChequeImage.Image, null, DateTime.Now, _CardScanType.GetHashCode(), "");
                        }
                    }
                    #endregion 

                    #region  //InsuranceCard
                    else if (_CardScanType == CardScanType.InsuranceCard)
                    {
                        oScannedInsurance = GetScanInsuranceData();
                        if (oScannedInsurance != null)
                        {
                            if (_PatientID == 0)
                            {
                                _PatId = RegisterScanPatient(oScannedInsurance);
                                _PatientID = _PatId;
                            }

                            if (_PatientID > 0)
                            {
                                Int64 _ContactId = 0;
                                Int64 _PatientInsId = 0;
                                _ContactId = AddInsuranceToContact(oScannedInsurance.PlanProvider, oScannedInsurance.PayerID);
                                _PatientInsId = AddInsuranceToExistingPatient(oScannedInsurance);
                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                            }
                        }

                        ////Added By MaheshB
                        //dtPatient = ogloCardScanning.IsPatientExists(oScannedInsurance.PatientFirstName, oScannedInsurance.PatientFirstName, Convert.ToDateTime(oScannedInsurance.DOB));
                        //if (dtPatient != null)
                        //{
                        //    DialogResult dr;
                        //    dr = MessageBox.Show("Patient Already Exists. Do you want to Modify? \nYes- To Modify.\nNo - Register New Patient.\nCancel - Do nothing.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        //    if (dr == DialogResult.Yes)
                        //    {
                        //        //_PatientID = 0;
                        //        //Mofify Method Opens a dialog.
                        //        if (dtPatient.Rows.Count > 1) //Select from Multiple Patients
                        //        {
                        //            gloPatientMigration.frmPatientList ofrmpatientlist = new gloPatientMigration.frmPatientList(dtPatient);
                        //            ofrmpatientlist.ShowDialog();
                        //            ofrmpatientlist.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        //            if (ofrmpatientlist.DialogPatientResult == true)
                        //            {
                        //                _TempPatientID = ofrmpatientlist.DialogPatientID;
                        //                //_IsSave = true;
                        //                //_RegisterPatient = false;

                        //                ogloCardScanning.ModifyPatient(oScannedPatient, _TempPatientID);
                        //                _PatientID = _TempPatientID;

                        //                #region //Save Scan Information
                        //                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                        //                #endregion

                        //            }
                        //        }
                        //        else //Only One Patient Exists.Don't show List
                        //        {
                        //            ogloCardScanning.ModifyPatient(oScannedPatient, Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]));
                        //            _PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                        //            //_IsSave = true;
                        //            #region //Save Scan Information
                        //            _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                        //            #endregion
                        //        }

                        //    }
                        //    else if (dr == DialogResult.No)
                        //    {
                        //        if (oScannedPatient != null)
                        //        {
                        //            _PatId = RegisterScanPatient(oScannedPatient);

                        //            if (_PatId > 0)
                        //            {
                        //                _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                        //            }
                        //        }
                        //   }



                        //}
                    }
                    #endregion InsuranceCard
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("Component required for card scanning is not available.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _IsDataSaved = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show("File required for card scanning is not found.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _IsDataSaved = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _IsDataSaved = false;
            }
            finally
            {
                if (oScannedPatient != null) { oScannedPatient.Dispose(); }
                if (oScannedInsurance != null) { oScannedInsurance.Dispose(); }
                if (ogloCardScanning != null) { ogloCardScanning.Dispose(); }
            }
            return _IsDataSaved;
        }

        private bool SaveData_Old1()
        {
            gloPatient.ScanedPatient oScannedPatient = new gloPatient.ScanedPatient();
            gloPatient.ScanedInsurance oScannedInsurance = new gloPatient.ScanedInsurance();
            gloCardScanning ogloCardScanning = new gloCardScanning(_databaseconnectionstring);
            Int64 _PatId = 0;
            bool _IsDataSaved = false;
            Int64 _TempPatientID = 0;
            DataTable dtPatient = new DataTable();
            //bool _IsSave = false;
            try
            {
                if (ValidateScannedData() == true)
                {

                    #region //DrivingLicense
                    if (_CardScanType == CardScanType.DrivingLicense)
                    {
                        oScannedPatient = GetScanPatientData();
                        //Added By MaheshB
                        if (_PatientID == 0)
                        {
                            dtPatient = ogloCardScanning.IsPatientExists(oScannedPatient.FirstName, oScannedPatient.LastName, Convert.ToDateTime(oScannedPatient.DOB));
                            if (dtPatient != null)
                            {
                                DialogResult dr;
                                dr = MessageBox.Show("Patient Already Exists. Do you want to Modify? \nYes- To Modify.\nNo - Register New Patient.\nCancel - Do nothing.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                if (dr == DialogResult.Yes)
                                {
                                    //_PatientID = 0;
                                    //Mofify Method 
                                    if (dtPatient.Rows.Count > 1) //Select from Multiple Patients
                                    {
                                        gloCardScanningPatientList.frmPatientList ofrmpatientlist = new gloCardScanningPatientList.frmPatientList(dtPatient); //Opens a dialog.
                                        ofrmpatientlist.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                                        ofrmpatientlist.ShowDialog();

                                        if (ofrmpatientlist.DialogPatientResult == true)
                                        {
                                            _TempPatientID = ofrmpatientlist.DialogPatientID;
                                            //_IsSave = true;
                                            //_RegisterPatient = false;

                                            ogloCardScanning.ModifyPatient(oScannedPatient, _TempPatientID, pbFaceImage.Image);
                                            _PatientID = _TempPatientID;

                                            #region //Save Scan Information
                                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                            #endregion

                                        }
                                    }
                                    else //Only One Patient Exists.Don't show List
                                    {
                                        if (dtPatient != null)
                                        {
                                            if (dtPatient.Rows.Count > 0)
                                            {
                                                ogloCardScanning.ModifyPatient(oScannedPatient, Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]), pbFaceImage.Image);
                                                _PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                                                //_IsSave = true;
                                                #region //Save Scan Information
                                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                                #endregion
                                            }
                                        }
                                    }

                                }
                                else if (dr == DialogResult.No)//Save as new Patient.
                                {
                                    if (oScannedPatient != null)
                                    {
                                        _PatId = RegisterScanPatient(oScannedPatient);

                                        if (_PatId > 0)
                                        {
                                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                        }
                                    }
                                }



                            }
                            else
                            {
                                if (oScannedPatient != null)
                                {
                                    _PatId = RegisterScanPatient(oScannedPatient);

                                    if (_PatId > 0)
                                    {
                                        _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                    }
                                }
                            }
                            //if ((_IsSave == true) || (dtPatient==null)) //Fires Only on Save Button.
                            //{
                            //    if (_PatientID == 0)
                            //    {

                            //        if (oScannedPatient != null)
                            //        {
                            //            _PatId = RegisterScanPatient(oScannedPatient);

                            //            if (_PatId > 0)
                            //            {
                            //                _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                            //            }
                            //        }
                            //    }
                            //    else if (_PatientID > 0)
                            //    {
                            //        _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                            //    }
                            //}
                        }
                        else //For Selected Patient..Bottom Scan Option.
                        {
                            //DialogResult dr;
                            //dr = MessageBox.Show("Do you want to modify patient Address and Photo ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            frmSetupCriteria ofrmcriteria = new frmSetupCriteria(_PatientID); 
                                ofrmcriteria.StartPosition = FormStartPosition.CenterParent;
                                ofrmcriteria.ShowDialog();
                               
                               
                                //if (dr == DialogResult.Yes)
                                //{
                                if(ofrmcriteria.Isupdate==true)
                                {
               
                                ogloCardScanning.ModifyPatient(oScannedPatient, _PatientID, pbFaceImage.Image);
                                #region //Save Scan Information
                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                #endregion
                            }
                        }
                    }
                    #endregion //DrivingLicense

                    #region  //CardImages and Cheque
                    else if (_CardScanType == CardScanType.Cheque || _CardScanType == CardScanType.CardImages)
                    {
                        if (_PatientID > 0)
                        {
                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_ChequeImage.Image, null, DateTime.Now, _CardScanType.GetHashCode(), "");
                        }
                    }
                    #endregion

                    #region  //InsuranceCard
                    else if (_CardScanType == CardScanType.InsuranceCard)
                    {
                        oScannedInsurance = GetScanInsuranceData();
                        if (oScannedInsurance != null)
                        {
                            if (_PatientID == 0)
                            {
                                _PatId = RegisterScanPatient(oScannedInsurance);
                                _PatientID = _PatId;
                            }

                            if (_PatientID > 0)
                            {
                                Int64 _ContactId = 0;
                                Int64 _PatientInsId = 0;
                                _ContactId = AddInsuranceToContact(oScannedInsurance.PlanProvider, oScannedInsurance.PayerID);
                                _PatientInsId = AddInsuranceToExistingPatient(oScannedInsurance);
                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                            }
                        }

                        ////Added By MaheshB
                        //dtPatient = ogloCardScanning.IsPatientExists(oScannedInsurance.PatientFirstName, oScannedInsurance.PatientFirstName, Convert.ToDateTime(oScannedInsurance.DOB));
                        //if (dtPatient != null)
                        //{
                        //    DialogResult dr;
                        //    dr = MessageBox.Show("Patient Already Exists. Do you want to Modify? \nYes- To Modify.\nNo - Register New Patient.\nCancel - Do nothing.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        //    if (dr == DialogResult.Yes)
                        //    {
                        //        //_PatientID = 0;
                        //        //Mofify Method Opens a dialog.
                        //        if (dtPatient.Rows.Count > 1) //Select from Multiple Patients
                        //        {
                        //            gloPatientMigration.frmPatientList ofrmpatientlist = new gloPatientMigration.frmPatientList(dtPatient);
                        //            ofrmpatientlist.ShowDialog();
                        //            ofrmpatientlist.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        //            if (ofrmpatientlist.DialogPatientResult == true)
                        //            {
                        //                _TempPatientID = ofrmpatientlist.DialogPatientID;
                        //                //_IsSave = true;
                        //                //_RegisterPatient = false;

                        //                ogloCardScanning.ModifyPatient(oScannedPatient, _TempPatientID);
                        //                _PatientID = _TempPatientID;

                        //                #region //Save Scan Information
                        //                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                        //                #endregion

                        //            }
                        //        }
                        //        else //Only One Patient Exists.Don't show List
                        //        {
                        //            ogloCardScanning.ModifyPatient(oScannedPatient, Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]));
                        //            _PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                        //            //_IsSave = true;
                        //            #region //Save Scan Information
                        //            _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                        //            #endregion
                        //        }

                        //    }
                        //    else if (dr == DialogResult.No)
                        //    {
                        //        if (oScannedPatient != null)
                        //        {
                        //            _PatId = RegisterScanPatient(oScannedPatient);

                        //            if (_PatId > 0)
                        //            {
                        //                _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                        //            }
                        //        }
                        //   }



                        //}
                    }
                    #endregion InsuranceCard
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("Component required for card scanning is not available.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _IsDataSaved = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show("File required for card scanning is not found.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _IsDataSaved = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _IsDataSaved = false;
            }
            finally
            {
                if (oScannedPatient != null) { oScannedPatient.Dispose(); }
                if (oScannedInsurance != null) { oScannedInsurance.Dispose(); }
                if (ogloCardScanning != null) { ogloCardScanning.Dispose(); }
            }
            return _IsDataSaved;
        }

        private bool SaveData()
        {
            gloPatient.ScanedPatient oScannedPatient = new gloPatient.ScanedPatient();
            gloPatient.ScanedInsurance oScannedInsurance = new gloPatient.ScanedInsurance();
            gloCardScanning ogloCardScanning = new gloCardScanning(_databaseconnectionstring);
            Int64 _PatId = 0;
            bool _IsDataSaved = false;
            Int64 _TempPatientID = 0;
            DataTable dtPatient = new DataTable();
            //bool _IsSave = false;
            try
            {
                if (ValidateScannedData() == true)
                {

                    #region //DrivingLicense
                    if (_CardScanType == CardScanType.DrivingLicense)
                    {
                        oScannedPatient = GetScanPatientData();
                        //Added By MaheshB
                       
                        if (_PatientID == 0)
                        {
                            dtPatient = ogloCardScanning.IsPatientExists(oScannedPatient.FirstName, oScannedPatient.LastName, Convert.ToDateTime(oScannedPatient.DOB));
                            if (dtPatient != null)
                            {

                                frmSetupCriteria ofrmcriteria = new frmSetupCriteria(_PatientID); 
                                ofrmcriteria.StartPosition = FormStartPosition.CenterParent;
                                ofrmcriteria.ShowDialog();
                               
                           
                                if(ofrmcriteria.Isupdate==true)
                                {
                                    ////_PatientID = 0;
                                    //Mofify Method 
                                    if (dtPatient.Rows.Count > 1) //Select from Multiple Patients
                                    {
                                        gloCardScanningPatientList.frmPatientList ofrmpatientlist = new gloCardScanningPatientList.frmPatientList(dtPatient); //Opens a dialog.
                                        ofrmpatientlist.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                                        ofrmpatientlist.ShowDialog();

                                        if (ofrmpatientlist.DialogPatientResult == true)
                                        {
                                            _TempPatientID = ofrmpatientlist.DialogPatientID;
                                            ////_IsSave = true;
                                            ////_RegisterPatient = false;

                                            //ogloCardScanning.ModifyPatient(oScannedPatient, _TempPatientID, pbFaceImage.Image);
                                            //_PatientID = _TempPatientID;
                                            bool _IsAddress, _IsPhoto, _IsDob, _IsFirstName, _IsMiddleName, _IsLastName, _IsSSN;
                                            _IsAddress=ofrmcriteria.IsAddress;
                                            _IsPhoto=ofrmcriteria.IsPhoto;
                                            _IsDob=ofrmcriteria.IsDOB;
                                            _IsFirstName = ofrmcriteria.IsFirstName;
                                            _IsLastName = ofrmcriteria.IsLastName;
                                            _IsMiddleName = ofrmcriteria.IsMiddleName;
                                            _IsSSN = ofrmcriteria.IsSSN;

                                            ogloCardScanning.ModifyPatientByCriteria(oScannedPatient, _TempPatientID, pbFaceImage.Image, _IsAddress, _IsPhoto, _IsDob, _IsFirstName, _IsMiddleName, _IsLastName, _IsSSN);
                                            //ogloCardScanning.ModifyPatientByCriteria(oScannedPatient, _TempPatientID, pbFaceImage.Image, _IsAddress, _IsPhoto, _IsDob);
                                            #region //Save Scan Information
                                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                            #endregion

                                        }
                                    }
                                    else //Only One Patient Exists.Don't show List
                                    {
                                        if (dtPatient != null)
                                        {
                                            if (dtPatient.Rows.Count > 0)
                                            {
                                                //ogloCardScanning.ModifyPatient(oScannedPatient, Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]), pbFaceImage.Image);
                                                bool _IsAddress, _IsPhoto, _IsDob, _IsFirstName, _IsMiddleName, _IsLastName, _IsSSN;
                                                _IsAddress = ofrmcriteria.IsAddress;
                                                _IsPhoto = ofrmcriteria.IsPhoto;
                                                _IsDob = ofrmcriteria.IsDOB;
                                                _IsFirstName = ofrmcriteria.IsFirstName;
                                                _IsLastName = ofrmcriteria.IsLastName;
                                                _IsMiddleName = ofrmcriteria.IsMiddleName;
                                                _IsSSN = ofrmcriteria.IsSSN;

                                                ogloCardScanning.ModifyPatientByCriteria(oScannedPatient, _TempPatientID, pbFaceImage.Image, _IsAddress, _IsPhoto, _IsDob, _IsFirstName, _IsMiddleName, _IsLastName, _IsSSN);
                                                //ogloCardScanning.ModifyPatientByCriteria(oScannedPatient, _TempPatientID, pbFaceImage.Image, _IsAddress, _IsPhoto, _IsDob);
                                                _PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                                                //_IsSave = true;
                                                #region //Save Scan Information
                                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                                #endregion
                                            }
                                        }
                                    }

                                }
                                else if (ofrmcriteria.IsNew == true)//Save as new Patient.
                                {
                                    if (oScannedPatient != null)
                                    {
                                        _PatId = RegisterScanPatient(oScannedPatient);

                                        if (_PatId > 0)
                                        {
                                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                        }
                                    }
                                }



                            }
                            else
                            {
                                if (oScannedPatient != null)
                                {
                                    _PatId = RegisterScanPatient(oScannedPatient);

                                    if (_PatId > 0)
                                    {
                                        _IsDataSaved = ogloCardScanning.SaveScanData(_PatId, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                    }
                                }
                            }
                        }
                        
                        else //For Selected Patient..Bottom Scan Option.
                        {
                            //DialogResult dr;
                            //dr = MessageBox.Show("Do you want to modify patient Address and Photo ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            frmSetupCriteria ofrmcriteria = new frmSetupCriteria(_PatientID);
                            ofrmcriteria.StartPosition = FormStartPosition.CenterParent;
                            ofrmcriteria.ShowDialog();
                            //if (dr == DialogResulYes)
                              bool _IsAddress,_IsPhoto,_IsDob,_IsFirstName,_IsMiddleName,_IsLastName,_IsSSN;
                                            _IsAddress=ofrmcriteria.IsAddress;
                                            _IsPhoto=ofrmcriteria.IsPhoto;
                                            _IsDob=ofrmcriteria.IsDOB;
                                            _IsFirstName = ofrmcriteria.IsFirstName;
                                            _IsLastName = ofrmcriteria.IsLastName;
                                            _IsMiddleName = ofrmcriteria.IsMiddleName;
                                            _IsSSN = ofrmcriteria.IsSSN;

                                ogloCardScanning.ModifyPatientByCriteria(oScannedPatient, _PatientID, pbFaceImage.Image, _IsAddress, _IsPhoto, _IsDob,_IsFirstName,_IsMiddleName,_IsLastName,_IsSSN);
                                if (_IsAddress == true || _IsPhoto == true || _IsDob == true)
                                {
                                    #region //Save Scan Information
                                    _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                                    #endregion
                                }
                            
                        }
                    }
                    #endregion //DrivingLicense

                    #region  //CardImages and Cheque
                    else if (_CardScanType == CardScanType.Cheque || _CardScanType == CardScanType.CardImages)
                    {
                        if (_PatientID > 0)
                        {
                            _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_ChequeImage.Image, null, DateTime.Now, _CardScanType.GetHashCode(), "");
                        }
                    }
                    #endregion

                    #region  //InsuranceCard
                    else if (_CardScanType == CardScanType.InsuranceCard)
                    {
                        oScannedInsurance = GetScanInsuranceData();
                        if (oScannedInsurance != null)
                        {
                            if (_PatientID == 0)
                            {
                                _PatId = RegisterScanPatient(oScannedInsurance);
                                _PatientID = _PatId;
                            }

                            if (_PatientID > 0)
                            {
                                Int64 _ContactId = 0;
                                Int64 _PatientInsId = 0;
                                _ContactId = AddInsuranceToContact(oScannedInsurance.PlanProvider, oScannedInsurance.PayerID);
                                _PatientInsId = AddInsuranceToExistingPatient(oScannedInsurance);
                                _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "");
                            }
                        }
                    }
                    #endregion InsuranceCard
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("Component required for card scanning is not available.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _IsDataSaved = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show("File required for card scanning is not found.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _IsDataSaved = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _IsDataSaved = false;
            }
            finally
            {
                if (oScannedPatient != null) { oScannedPatient.Dispose(); }
                if (oScannedInsurance != null) { oScannedInsurance.Dispose(); }
                if (ogloCardScanning != null) { ogloCardScanning.Dispose(); }
            }
            return _IsDataSaved;
        } //Wid criteria 

        private void FillInsurance(string InsuranceName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtInsurance = new DataTable();
                string _sqlQuery = "select sname from Contacts_MST where SOUNDEX(replace(sname,' ','')) = soundex(replace('" + InsuranceName.Trim().Replace("'", "''") + "',' ','')) and sContactType = 'Insurance' order by sName";
                oDB.Retrive_Query(_sqlQuery, out dtInsurance);                
                oDB.Disconnect();

                AutoCompleteStringCollection oInsurance = new AutoCompleteStringCollection();

                foreach (DataRow dr in dtInsurance.Rows)
                {
                    oInsurance.Add(Convert.ToString(dr[0]));
                }
                txtIns_PlanProvider.AutoCompleteCustomSource = oInsurance;
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

        private void PrintCards()
        {
            try
            {                
                if (pb_FrontSide.Image == null && pb_BackSide.Image == null && pb_ChequeImage.Image == null)
                {
                    MessageBox.Show("Please scan the card first ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return;
                }
                else
                {
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //set printer settings on printdocument object
                        printDoc.PrinterSettings = printDialog1.PrinterSettings;
                        //print...
                        printDoc.Print();
                    }
                }                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                int y = 3;
                if (_CardScanType == CardScanType.DrivingLicense || _CardScanType == CardScanType.InsuranceCard)
                {
                    if (pb_FrontSide.Image != null)
                    {
                        Image logo = Image.FromFile(_CardFrontImagePath);
                        e.Graphics.DrawImage(logo, new Point(3, y));
                        logo.Dispose();
                        y = y + 250;
                    }
                    if (pb_BackSide.Image != null)
                    {
                        Image logo = Image.FromFile(_CardBackImagePath);
                        e.Graphics.DrawImage(logo, new Point(3, y));
                        logo.Dispose();
                        y = y + 250;
                    }
                }
                else if (_CardScanType == CardScanType.Cheque)
                {
                    if (pb_ChequeImage.Image != null)
                    {
                        Image logo = Image.FromFile(_ChequeFrontImagePath);
                        e.Graphics.DrawImage(logo, new Point(3, y));
                        logo.Dispose();
                        y = y + 250;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private Int64 RegisterScanPatient(gloPatient.ScanedPatient oScannedPatient)
        {
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
            gloPatient.Patient oPatient = new gloPatient.Patient();
            Int64 _TempPatientId = 0;

            try
            {
                if (oScannedPatient != null)
                {
                    oPatient.DemographicsDetail.PatientCode = ogloPatient.GeneratePatientCode();
                    oPatient.DemographicsDetail.PatientSSN = oScannedPatient.SocialSecurity;
                    oPatient.DemographicsDetail.PatientFirstName = oScannedPatient.FirstName;
                    oPatient.DemographicsDetail.PatientMiddleName = oScannedPatient.MiddleName;
                    oPatient.DemographicsDetail.PatientLastName = oScannedPatient.LastName;
                    oPatient.DemographicsDetail.PatientPhoto = pbFaceImage.Image;
                    oPatient.DemographicsDetail.PatientDOB = Convert.ToDateTime(oScannedPatient.DOB);
                    oPatient.DemographicsDetail.PatientProviderID = oScannedPatient.ProviderId;
                    oPatient.DemographicsDetail.PatientGender = oScannedPatient.Sex;
                   
                    
                    oPatient.DemographicsDetail.PatientCity = oScannedPatient.City;
                    oPatient.DemographicsDetail.PatientCounty = oScannedPatient.County;
                    oPatient.DemographicsDetail.PatientZip = oScannedPatient.Zip;
                    oPatient.DemographicsDetail.PatientAddress1 = oScannedPatient.Address1;
                    oPatient.DemographicsDetail.PatientState = oScannedPatient.State;

                    _TempPatientId = ogloPatient.Add(oPatient);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oPatient != null) { oPatient.Dispose(); }
                if (ogloPatient != null) { ogloPatient.Dispose(); } 
            }
            return _TempPatientId; 
        }
      
        private Int64 RegisterScanPatient(gloPatient.ScanedInsurance oScannedInsurance)
        {
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
            gloPatient.Patient oPatient = new gloPatient.Patient();
            Int64 _TempPatientId = 0;

            try
            {
                if (oScannedInsurance != null)
                {
                    oPatient.DemographicsDetail.PatientCode = ogloPatient.GeneratePatientCode();
                    oPatient.DemographicsDetail.PatientSSN = oScannedInsurance.SSN;
                    oPatient.DemographicsDetail.PatientFirstName = oScannedInsurance.PatientFirstName;
                    oPatient.DemographicsDetail.PatientMiddleName = oScannedInsurance.PatientMiddleName;
                    oPatient.DemographicsDetail.PatientLastName = oScannedInsurance.PatientLastName;
                    //oPatient.DemographicsDetail.PatientPhoto = pbFaceImage.Image;
                    oPatient.DemographicsDetail.PatientDOB = Convert.ToDateTime(oScannedInsurance.DOB);
                    oPatient.DemographicsDetail.PatientProviderID = oScannedInsurance.ProviderId;
                    oPatient.DemographicsDetail.PatientGender = oScannedInsurance.Sex;
                    oPatient.DemographicsDetail.PatientCity = oScannedInsurance.City;
                    oPatient.DemographicsDetail.PatientCounty = oScannedInsurance.County;
                    oPatient.DemographicsDetail.PatientZip = oScannedInsurance.Zip;
                    oPatient.DemographicsDetail.PatientAddress1 = oScannedInsurance.Address;
                    oPatient.DemographicsDetail.PatientState = oScannedInsurance.State;

                    _TempPatientId = ogloPatient.Add(oPatient);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oPatient != null) { oPatient.Dispose(); }
                if (ogloPatient != null) { ogloPatient.Dispose(); }
            }
            return _TempPatientId;
        }

        private Int64 AddInsuranceToContact(string InsuranceName,string PayerId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string  _strQuery = "";
            oDB.Connect(false);
            object oContactID = null;
            Int64 _nContactID = 0;
            try
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
                     + " VALUES(" + Convert.ToInt64(oContactID) + ",'" + InsuranceName.Replace("'", "''").Trim()  + "','','','','','','','','','','',"
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

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _nContactID;
        }

        private Int64 AddInsuranceToExistingPatient(gloPatient.ScanedInsurance oScannedInsurance)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters odbParams;
            string _sqlQuery = "";
            Int64 _ContactID = 0;
            Object retValue = null;
            Int64 _InsuranceId = 0;
            try
            {
                oDB.Connect(false);
                _ContactID = AddInsuranceToContact(oScannedInsurance.PlanProvider, oScannedInsurance.PayerID);
                if (_ContactID > 0)
                {
                    _sqlQuery = " SELECT COUNT(*) FROM PatientInsurance_DTL " +
                    " WHERE nContactID = " + _ContactID + "  AND nPatientID = " + _PatientID + " " +
                    " AND sInsuranceName = '" + oScannedInsurance.PlanProvider.Replace("'", "''").Trim() + "'";
                    retValue = oDB.ExecuteScalar_Query(_sqlQuery);

                    
                    gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);
                    gloContacts.Insurance oInsurance = oContact.SelectInsurance(_ContactID);
                    

                    if (retValue != null && Convert.ToInt64(retValue) == 0)
                    {
                        _sqlQuery = "SELECT ISNULL(MAX(nInsuranceID),0)+1 as nInsuranceID  FROM PatientInsurance_DTL";
                        retValue = oDB.ExecuteScalar_Query(_sqlQuery);
                        if (retValue != null && Convert.ToInt64(retValue) > 0)
                        {
                            _InsuranceId = Convert.ToInt64(retValue);
                        }
                        _sqlQuery = " INSERT INTO PatientInsurance_DTL " +
                        " (nPatientID, nInsuranceID, sSubscriberName, sSubscriberPolicy#, sSubscriberID," + 
                        "sGroup, sEmployer, sPhone, dtDOB, sPayerID, " + 
                        "CopayER, CopayOV, CopaySP, CopayUC,  " +
                        " dtEffectiveDate, dtExpiryDate, sSubFName, sSubMName, sSubLName, nRelationShipID,  " +
                        " sRelationShip, nDeductableamount, nCoveragePercent, nCoPay, bAssignmentofBenifit,  " +
                        " dtStartDate, dtEndDate, nInsuranceFlag, " +
                        " sSubscriberGender, sSubscriberAddr1, sSubscriberAddr2,sSubscriberCity, sSubscriberState, sSubscriberZip, " +
                        " nContactID, sInsuranceName, sAddressLine1, sAddressLine2, sCity, sState, sZIP, sInsurancePhone, " +
                        " sFax, sEmail, sURL, sInsuranceTypeCode, sInsuranceTypeDesc, bAccessAssignment,  " +
                        " bStatementToPatient, bMedigap, bReferringIDInBox19, bNameOfacilityinBox33,  " +
                        " bDoNotPrintFacility, b1stPointer, bBox31Blank, bShowPayment, nTypeOBilling,  " +
                        " nClearingHouse, bIsClaims, bIsRemittanceAdvice, bIsRealTimeEligibility, bIsElectronicCOB,  " +
                        " bIsRealTimeClaimStatus, bIsEnrollmentRequired, sPayerPhone, sWebsite, sServicingState,  " +
                        " sComments, sPayerPhoneExtn) " +
                        " VALUES (" + _PatientID + "," + _InsuranceId + ",'" + oScannedInsurance.MemberName.Replace("'", "''").Trim() + "','','', " +
                        " '" + oScannedInsurance.GroupNumber.Replace("'", "''").Trim() + "','','','" + oScannedInsurance.DOB + "','" + oScannedInsurance.PayerID.Replace("'", "''").Trim() + "', " +
                        " '" + oScannedInsurance.CopayER + "','" + oScannedInsurance.CopayOV + "','" + oScannedInsurance.CopaySP + "','" + oScannedInsurance.CopayUC + "'," +
                        " '" + oScannedInsurance.EffectiveDate + "','" + oScannedInsurance.ExpiryDate + "', " +
                        " '" + oScannedInsurance.PatientFirstName.Replace("'", "''").Trim() + "','" + oScannedInsurance.PatientMiddleName.Replace("'", "''").Trim() + "','" + oScannedInsurance.PatientLastName.Replace("'", "''").Trim() + "',0," + 
                        "'',0,0," + oScannedInsurance.Copay + ",'" + false + "'," + 
                        "Null,Null,0," +
                        "'" + oScannedInsurance.Sex + "','" + oScannedInsurance.Address.Replace("'", "''").Trim() + "','','" + oScannedInsurance.City.Replace("'", "''").Trim() + "','" + oScannedInsurance.State.Replace("'", "''").Trim() + "','" + oScannedInsurance.Zip.Replace("'", "''").Trim() + "'," +
                        " " + _ContactID + ",'" + oScannedInsurance.PlanProvider.Replace("'", "''").Trim() + "', " +
                        " '" + oInsurance.Companyaddress.AddrressLine1 + "','" + oInsurance.Companyaddress.AddrressLine2 + "','" + oInsurance.Companyaddress.City + "','" + oInsurance.Companyaddress.State + "','" + oInsurance.Companyaddress.ZIP + "','" + oInsurance.Companyaddress.Phone + "','" + oInsurance.Companyaddress.Fax + "','" + oInsurance.Companyaddress.Email + "','" + oInsurance.Companyaddress.URL + "','" + oInsurance.InsuranceTypeCode + "','" + oInsurance.InsuranceTypeDesc + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "', " +
                        " '" + false + "','" + false + "','" + false + "',0,0,'" + false + "','" + false + "','" + false + "','" + false + "', " +
                        " '" + false + "','" + false + "','','','','','')";

                        oDB.Execute_Query(_sqlQuery);
                        
                    }
                    else if (retValue != null && Convert.ToInt64(retValue) > 0)
                    {
                        _sqlQuery = " SELECT nInsuranceID FROM PatientInsurance_DTL " +
                        " WHERE nContactID = " + _ContactID + "  AND nPatientID = " + _PatientID + " " +
                        " AND sInsuranceName = '" + oScannedInsurance.PlanProvider.Replace("'", "''").Trim() + "'";
                        retValue = oDB.ExecuteScalar_Query(_sqlQuery);
                        if (retValue != null && Convert.ToInt64(retValue) > 0)
                        {
                            _InsuranceId = Convert.ToInt64(retValue);
                            _sqlQuery = "UPDATE [PatientInsurance_DTL] set " +
                                          "[sSubscriberName] = '" + oScannedInsurance.MemberName.Replace("'", "''").Trim() + "'," +
                                          "[sGroup] = '" + oScannedInsurance.GroupNumber.Replace("'", "''").Trim() + "'," +
                                          "[sPayerID] = '" + oScannedInsurance.PayerID.Replace("'", "''").Trim() + "'," +
                                          "[CopayER] = '" + oScannedInsurance.CopayER + "'," +
                                          "[CopayOV] = '" + oScannedInsurance.CopayOV + "'," +
                                          "[CopaySP] = '" + oScannedInsurance.CopaySP + "'," +
                                          "[CopayUC] = '" + oScannedInsurance.CopayUC + "'," +
                                          "[dtEffectiveDate] = '" + oScannedInsurance.EffectiveDate + "'," +
                                          "[dtExpiryDate] = '" + oScannedInsurance.ExpiryDate + "'," +
                                          "[nCoPay] = " + oScannedInsurance.Copay + "," +
                                          "[nContactID] = " + _ContactID + "," +                                          
                                          "sSubFName ='" + oScannedInsurance.PatientFirstName.Replace("'", "''").Trim() + "'," +
                                          "sSubMName ='" + oScannedInsurance.PatientMiddleName.Replace("'", "''").Trim() + "'," +
                                          "sSubLName ='" + oScannedInsurance.PatientLastName + "'," +
                                          "sSubscriberGender ='" + oScannedInsurance.Sex + "'," +
                                          "sSubscriberAddr1 ='" + oScannedInsurance.Address.Replace("'", "''").Trim() + "'," +
                                          "sSubscriberState ='" + oScannedInsurance.State.Replace("'", "''").Trim() + "'," +
                                          "sSubscriberCity ='" + oScannedInsurance.City.Replace("'", "''").Trim() + "'," +
                                          "sSubscriberZip ='" + oScannedInsurance.Zip.Replace("'", "''").Trim() + "'," +                                         
                                          "sAddressLine1='" + oInsurance.Companyaddress.AddrressLine1.Replace("'", "''").Trim() + "'," +                                          
                                          "sAddressLine2'" + oInsurance.Companyaddress.AddrressLine2.Replace("'", "''").Trim() + "'," + 
                                          "sCity'" + oInsurance.Companyaddress.City.Replace("'", "''").Trim() + "'," + 
                                          "sState'" + oInsurance.Companyaddress.State.Replace("'", "''").Trim() + "'," + 
                                          "sZIP'" + oInsurance.Companyaddress.ZIP.Replace("'", "''").Trim() + "'," + 
                                          "sInsurancePhone'" + oInsurance.Companyaddress.Phone.Replace("'", "''").Trim() + "'," + 
                                          "sFax'" + oInsurance.Companyaddress.Fax.Replace("'", "''").Trim() + "'," + 
                                          "sEmail'" + oInsurance.Companyaddress.Email.Replace("'", "''").Trim() + "'," + 
                                          "sURL'" + oInsurance.Companyaddress.URL.Replace("'", "''").Trim() + "'," + 
                                          "sInsuranceTypeCode'" + oInsurance.InsuranceTypeCode.Replace("'", "''").Trim() + "'," +
                                          "sInsuranceTypeDesc'" + oInsurance.InsuranceTypeDesc.Replace("'", "''").Trim() + "'," +
                                          "[sInsuranceName] = '" + oScannedInsurance.PlanProvider.Replace("'", "''").Trim() + "' " +
                                          " WHERE nPatientID = " + _PatientID + " " +
                                          " AND nInsuranceID = " + _InsuranceId + " ";
                            retValue = oDB.Execute_Query(_sqlQuery);
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
                if (oDB != null) { oDB.Dispose(); }
            }
            return _InsuranceId;
        }

        private gloPatient.ScanedInsurance GetScanInsuranceData()
        {
            gloPatient.ScanedInsurance oScannedInsData = null;

            try
            {
                if (axms_InsuranceCard != null)
                {
                    oScannedInsData = new gloPatient.ScanedInsurance();
                    oScannedInsData.PlanProvider = txtIns_PlanProvider.Text.Trim().Replace("'", "''");
                    oScannedInsData.MemberName = txtIns_MemberName.Text.Trim().Replace("'", "''");
                    oScannedInsData.MemberID = txtIns_MemberID.Text.Trim();
                    oScannedInsData.GroupNumber = txtIns_GroupNo.Text.Trim();
                    oScannedInsData.PayerID = txtIns_PayerID.Text.Trim();
                    oScannedInsData.EffectiveDate = txtIns_EffectiveDate.Text.Trim();
                    txtIns_DOB.TextMaskFormat = MaskFormat.IncludePromptAndLiterals; 
                    oScannedInsData.DOB = txtIns_DOB.Text.Trim();
                    oScannedInsData.ExpiryDate = txtIns_ExpiryDate.Text.Trim();
                    oScannedInsData.CopayOV = txtIns_CopayOV.Text.Trim();
                    oScannedInsData.CopayUC = txtIns_CopayUC.Text.Trim();
                    oScannedInsData.CopaySP = txtIns_CopaySP.Text.Trim();
                    oScannedInsData.CopayER = txtIns_CopayER.Text.Trim();
                    if (txtCopay.Text != null && txtCopay.Text.ToString().Trim() != "" && txtCopay.Text.ToString().Trim() != "0")
                    {
                        oScannedInsData.Copay = Convert.ToDecimal(txtCopay.Text.ToString().Trim());
                    }
                    //oScannedInsurance.TelList = axms_InsuranceCard 
                    //oScannedInsurance.WebList = axms_InsuranceCard
                    oScannedInsData.SSN = txtIns_SSN.Text.Trim();
                    oScannedInsData.PatientCode = txtIns_PatientCode.Text.Trim();
                    oScannedInsData.ProviderId = Convert.ToInt64(cmb_Ins_Providers.SelectedValue);
                    oScannedInsData.ProviderName = Convert.ToString(cmb_Ins_Providers.SelectedText);
                    oScannedInsData.PatientFirstName = txtIns_PatientFirstName.Text.Trim().Replace("'", "''");
                    oScannedInsData.PatientMiddleName = "";
                    oScannedInsData.PatientLastName = txtIns_PatientLastName.Text.Trim().Replace("'", "''");
                    
                    //oScannedInsData.Sex = txtIns_Sex.Text.Trim();
                    if (rbInsMale.Checked == true)
                    { oScannedInsData.Sex = "Male"; }
                    else if (rbInsFemale.Checked == true)
                    { oScannedInsData.Sex = "Female"; }
                    else if (rbInsOther.Checked == true)
                    { oScannedInsData.Sex = "Other"; }

                    oScannedInsData.Address = txtIns_Address.Text.Trim().Replace("'", "''");
                    oScannedInsData.City = txtIns_City.Text.Trim().Replace("'", "''");
                    oScannedInsData.State = txtIns_State.Text.Trim().Replace("'", "''");
                }
                else
                {
                    MessageBox.Show("Please scan the card first ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return oScannedInsData;
        }

        private gloPatient.ScanedPatient GetScanPatientData()
        {
            gloPatient.ScanedPatient oScannedPatientData = null;

            try
            {
                if (mIdData != null)
                {
                    oScannedPatientData = new gloPatient.ScanedPatient();

                    oScannedPatientData.License = txtLicNo.Text.Trim(); //oScannedPatientData.License = mIdData.license;
                    oScannedPatientData.ID = txtLicID.Text.Trim(); //oScannedPatientData.ID = mIdData.Id.Trim();
                    oScannedPatientData.PatientCode = txtLic_PatientCode.Text.Trim();
                    oScannedPatientData.SocialSecurity = txtLicSSn.Text.Trim(); // oScannedPatientData.SocialSecurity = mIdData.SocialSecurity;
                    oScannedPatientData.FirstName = txtLicFirstName.Text.Trim().Replace("'","''");// oScannedPatientData.FirstName = mIdData.NameFirst;
                    oScannedPatientData.MiddleName = txtLicMiddleName.Text.Trim().Replace("'", "''");
                    oScannedPatientData.LastName = txtLicLastName.Text.Trim().Replace("'", "''");// oScannedPatientData.MiddleName = mIdData.NameMiddle;
                    
                    //oScannedPatientData.Sex = txtLicSex.Text.Trim(); //oScannedPatientData.Sex = mIdData.Sex;
                    if (rbLicMale.Checked == true)
                    { oScannedPatientData.Sex = "Male"; }
                    else if (rbLicFemale.Checked == true)
                    { oScannedPatientData.Sex = "Female"; }
                    else if (rbLicOthers.Checked == true)
                    { oScannedPatientData.Sex = "Other"; }

                    txtLicDOB.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    oScannedPatientData.DOB = txtLicDOB.Text.Trim(); // oScannedPatientData.DOB = mIdData.DateOfBirth;
                    oScannedPatientData.Address1 = txtLicAddress.Text.Trim().Replace("'", "''"); // oScannedPatientData.Address1 = mIdData.Address;
                    oScannedPatientData.City = txtLicCity.Text.Trim().Replace("'", "''"); // oScannedPatientData.City = mIdData.City;
                    oScannedPatientData.Zip = txtLicZip.Text.Trim(); // oScannedPatientData.Zip = mIdData.Zip;
                    oScannedPatientData.State = txtLicState.Text.Trim().Replace("'", "''"); // oScannedPatientData.State = mIdData.State;
                    //oScannedPatientData.County = txtLicCounty.Text.Trim(); // oScannedPatientData.County = mIdData.County;
                    oScannedPatientData.ProviderId = Convert.ToInt64(cmb_DLID_Providers.SelectedValue);
                    oScannedPatientData.ProviderName = Convert.ToString(cmb_DLID_Providers.SelectedText);
                    //string str = Convert.ToString(cmb_Ins_Providers.SelectedText);
                    oScannedPatientData.Name = mIdData.Name.Replace("'", "''");
                    oScannedPatientData.sClass = mIdData.Class;
                    oScannedPatientData.Type = mIdData.Type;
                    oScannedPatientData.NameSuffix = mIdData.NameSuffix;
                    oScannedPatientData.Eyes = mIdData.Eyes;
                    oScannedPatientData.Hair = mIdData.Hair;
                    oScannedPatientData.IssueDate = mIdData.IssueDate;
                    oScannedPatientData.Height = mIdData.Height;
                    oScannedPatientData.ExpirationDate = mIdData.ExpirationDate;
                    oScannedPatientData.Weight = mIdData.Weight;
                    oScannedPatientData.SigNum = mIdData.SigNum;
                    oScannedPatientData.Audit = mIdData.Audit;
                    oScannedPatientData.Restriction = mIdData.Restriction;
                    oScannedPatientData.Endorsments = mIdData.Endorsements;
                    oScannedPatientData.CSC = mIdData.CSC;
                    oScannedPatientData.Fee = mIdData.Fee;
                    oScannedPatientData.Address2 = mIdData.Address2.Replace("'", "''");
                    oScannedPatientData.Address3 = mIdData.Address3.Replace("'", "''");
                    oScannedPatientData.Address4 = mIdData.Address4.Replace("'", "''");
                    oScannedPatientData.Address5 = mIdData.Address5.Replace("'", "''");
                    oScannedPatientData.Text1 = mIdData.Text1;
                    oScannedPatientData.Text2 = mIdData.Text2;
                    oScannedPatientData.Text3 = mIdData.Text3;
                    oScannedPatientData.Duplicate = mIdData.Dup_Test;
                }
                else
                {
                    MessageBox.Show("Please scan the card first ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return oScannedPatientData; 
        }

        private void FillProviders()
        {
            gloAppointmentBook.Books.Resource oProvider = null;
            DataTable dt = null;
            try
            {
                oProvider = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                dt = oProvider.GetProviders();

                DataRow r;
                r = dt.NewRow();
                r["nProviderID"] = 0;
                r["ProviderName"] = "";
                dt.Rows.InsertAt(r, 0);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                            //..Fill Provider combo for Drivers License
                            cmb_DLID_Providers.DataSource = dt.Copy();
                            cmb_DLID_Providers.ValueMember = dt.Columns["nProviderID"].ColumnName;
                            cmb_DLID_Providers.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                            cmb_DLID_Providers.Refresh();
                            if (dt.Rows.Count > 1)
                                cmb_DLID_Providers.SelectedIndex = 0;

                            //..Fill Provider combo for Insurance Scan 
                            cmb_Ins_Providers.DataSource = dt.Copy();
                            cmb_Ins_Providers.ValueMember = dt.Columns["nProviderID"].ColumnName;
                            cmb_Ins_Providers.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                            cmb_Ins_Providers.Refresh();
                            if (dt.Rows.Count > 1)
                                cmb_Ins_Providers.SelectedIndex = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dt != null) { dt = null; }
                if (oProvider != null) { oProvider.Dispose(); }
            }

        }

        private void ClearData()
        {
            

            pb_FrontSide.Image = null;
            pb_BackSide.Image = null;
            pbFaceImage.Image = null;
            //.DL/ID Data
            txtLicID.Text = "";
            txtLicSSn.Text = "";
            txtLicNo.Text = "";
            txtLicFirstName.Text = "";
            txtLicMiddleName.Text = "";
            txtLicLastName.Text = "";
            txtLicDOB.Text = "";
            txtLic_PatientCode.Text = "";
            //txtLicSex.Text = "";
            txtLicAddress.Text = "";
            txtLicCity.Text = "";
            txtLicState.Text = "";
            txtLicZip.Text = "";
            //txtLicCounty.Text = "";
            cmb_DLID_Providers.SelectedIndex = -1;
            //.Insurance Card Data
            txtIns_PayerID.Text = "";
            txtIns_PlanProvider.Text = "";
            txtIns_MemberID.Text = "";
            txtIns_MemberName.Text = "";
            txtIns_PatientFirstName.Text = "";
            
            txtIns_PatientLastName.Text = "";
            txtIns_PatientCode.Text = "";
            txtIns_SSN.Text = "";
            txtIns_GroupNo.Text = "";
            txtIns_DOB.Text = "";
            txtIns_EffectiveDate.Text = "";
            txtIns_ExpiryDate.Text = "";
            txtIns_CopayER.Text = "";
            txtIns_CopayOV.Text = "";
            txtIns_CopaySP.Text = "";
            txtIns_CopayUC.Text = "";
            txtIns_Address.Text = "";
            txtIns_City.Text = "";
            txtIns_State.Text = "";
            txtIns_Zip.Text = "";
            txtIns_ContractNo.Text = "";
            cmb_Ins_Providers.SelectedIndex = -1;

            txtAccountNo.Text = "";
            txtCheckAmount.Text = "0.00";
            txtCheckNo.Text = "";
            txtIssuingBank.Text = "";
            txtIssuingCompany.Text = "";
            txtMICR.Text = "";
            txtRoutingNo.Text = "";
            pb_ChequeImage.Image = null;

        
        }

        private bool ValidateScannedData()
        {
            bool _IsValid = true;
            try
            {
                if (_CardScanType == CardScanType.DrivingLicense)
                {
                    txtLicDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                    if (pb_FrontSide.Image == null || pb_BackSide.Image == null || mIdData == null || axms_InsuranceCard == null)
                    {
                        MessageBox.Show("Please scan the card first ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tsb_DriversLicense.Select();
                        _IsValid = false; 
                    }
                    //else if (txtLicSSn.Text.Trim() == "")
                    //{
                    //    MessageBox.Show("Enter the SSN", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtLicSSn.Focus();
                    //    _IsValid = false;
                    //}
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
                    //else if (txtLicDOB.Text.Trim() == "")
                    //{
                    //    MessageBox.Show("Please enter the Patient date of birth", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtLicDOB.Focus();
                    //    _IsValid = false;
                    //}
                    //else if (txtLicDOB.Text.Trim() != "")
                    //{
                    //    try
                    //    {
                    //        DateTime dtDOB = Convert.ToDateTime(txtLicDOB.Text.Trim());
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show("Please enter valid Patient date of birth", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        txtLicDOB.Focus();
                    //        _IsValid = false;
                    //    }
                    //}
                    else if (cmb_DLID_Providers.SelectedIndex <= 0)
                    {
                        MessageBox.Show("Select the provider", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmb_DLID_Providers.Focus();
                        _IsValid = false;
                    }
                    txtLicDOB.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    
                }
                else if (_CardScanType == CardScanType.Cheque)
                {
                    if (pb_ChequeImage.Image == null)
                    {
                        MessageBox.Show("Scan the check", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tsb_Cheque.Select();
                        _IsValid = false;
                    }
                }
                else if (_CardScanType == CardScanType.InsuranceCard)
                {
                    txtIns_DOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                    if (pb_FrontSide.Image == null || pb_BackSide.Image == null)
                    {
                        MessageBox.Show("Please scan the card first ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tsb_InsuranceCard.Select();
                        _IsValid = false;
                    }
                    else if (txtIns_PayerID.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter the payer id", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIns_PayerID.Focus();
                        _IsValid = false;
                    }
                    else if (txtIns_MemberName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter the member name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIns_MemberName.Focus();
                        _IsValid = false;
                    }
                    //else if (txtIns_SSN.Text.Trim() == "")
                    //{
                    //    MessageBox.Show("Please enter the SSN", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtIns_SSN.Focus();
                    //    _IsValid = false;
                    //}
                    //date of birth      
                    else if (txtIns_DOB.MaskCompleted == false)
                    {
                        MessageBox.Show("Enter a date of birth for the patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIns_DOB.Focus();
                        _IsValid = false;
                    }
                    else if (txtIns_PatientFirstName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter the patient first name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIns_PatientFirstName.Focus();
                        _IsValid = false;
                    }
                    else if (txtIns_PatientLastName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter the patient last name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIns_PatientLastName.Focus();
                        _IsValid = false;
                    }
                    ////else if (txtIns_DOB.Text.Trim() == "")
                    ////{
                    ////    MessageBox.Show("Please enter the Patient date of birth", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////    txtIns_DOB.Focus();
                    ////    _IsValid = false;
                    ////}
                    //else if (txtIns_DOB.Text.Trim() != "")
                    //{
                    //    try
                    //    {
                    //        DateTime dtDOB = Convert.ToDateTime(txtIns_DOB.Text.Trim());
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show("Please enter valid Patient date of birth", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        txtIns_DOB.Focus();
                    //        _IsValid = false;
                    //    }
                    //}
                    else if (cmb_Ins_Providers.SelectedIndex <= 0)
                    {
                        MessageBox.Show("Select the provider", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmb_Ins_Providers.Focus();
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

        #region " Form Closing Event "

        private void frmScanCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string _TempPath = _RootPath + "\\" + _PatientID;
                if (Directory.Exists(_TempPath) == true)
                {
                    pb_FrontSide.Image = null;
                    pb_BackSide.Image = null;
                    pbFaceImage.Image = null;
                    Directory.Delete(_TempPath, true);
                }
                UnLoadSdk();
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                
               
            }
        }

        #endregion " Form Closing Event "

        private void txtPAZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void txtPAZip_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Trim() != "")
            {
                DataTable dt = new System.Data.DataTable();
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string _ZipCode = "";
                    
                    if (_CardScanType == CardScanType.DrivingLicense)
                    {_ZipCode = txtLicZip.Text.Trim(); }
                    else if (_CardScanType == CardScanType.InsuranceCard)
                    { _ZipCode = txtIns_Zip.Text.Trim(); }

                    if (_ZipCode != "")
                    {
                        string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = " + _ZipCode + "";
                        oDb.Retrive_Query(qry, out dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (_CardScanType == CardScanType.DrivingLicense)
                            {
                                txtLicState.Text = Convert.ToString(dt.Rows[0]["ST"]);
                                txtLicCity.Text = Convert.ToString(dt.Rows[0]["City"]);
                                txtLicCounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                            }
                            else if (_CardScanType == CardScanType.InsuranceCard)
                            {
                                txtIns_State.Text = Convert.ToString(dt.Rows[0]["ST"]);
                                txtIns_City.Text = Convert.ToString(dt.Rows[0]["City"]);
                                txtInsCounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                            }
                        }
                        else
                        {
                            if (_CardScanType == CardScanType.DrivingLicense)
                            {
                                txtLicState.Text = "";
                                txtLicCity.Text = "";
                                txtLicCounty.Text = "";
                            }
                            else if (_CardScanType == CardScanType.InsuranceCard)
                            {
                                txtIns_State.Text = "";
                                txtIns_City.Text = "";
                                txtInsCounty.Text = "";
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
                    MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dt.Dispose();
                    oDb.Disconnect();
                    oDb.Dispose();
                }
            }
        }

        private void rbLicMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLicMale.Checked == true)
            {
                rbLicMale.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbLicMale.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbLicFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLicFemale.Checked == true)
            {
                rbLicFemale.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbLicFemale.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbLicOthers_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLicOthers.Checked == true)
            {
                rbLicOthers.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbLicOthers.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbInsMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInsMale.Checked == true)
            {
                rbInsMale.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbInsMale.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbInsFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInsFemale.Checked == true)
            {
                rbInsFemale.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbInsFemale.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbInsOther_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInsOther.Checked == true)
            {
                rbInsOther.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbInsOther.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void txtCheckAmount_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
            else
            {
                if (txtCheckAmount.SelectionStart > txtCheckAmount.Text.IndexOf("."))
                {
                    if (txtCheckAmount.Text.Contains(".") == true)
                    {
                        if (txtCheckAmount.Text.Substring(txtCheckAmount.Text.IndexOf(".") + 1, txtCheckAmount.Text.Length - (txtCheckAmount.Text.IndexOf(".") + 1)).Length == 2)
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            if (e.KeyChar == Convert.ToChar(46) && txtCheckAmount.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        

       

        private void txtIns_DOB_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private bool IsValidDate(string DOB)
        {
            Int32 year = 0; Int32 month = 0; Int32 day = 0;
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


        private void txtIns_DOB_Validating(object sender, CancelEventArgs e)
        {
            txtIns_DOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (txtIns_DOB.Text.Length > 0 && txtIns_DOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (txtIns_DOB.MaskCompleted == true)
                {
                    try
                    {
                        txtIns_DOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(txtIns_DOB.Text))
                        {
                            if (Convert.ToDateTime(txtIns_DOB.Text).Date >= DateTime.Now.Date)
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

        private void txtIns_SSN_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIns_DOB_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void txtIns_DOB_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLicDOB_Validating(object sender, CancelEventArgs e)
        {
            txtLicDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (txtLicDOB.Text.Length > 0 && txtLicDOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (txtLicDOB.MaskCompleted == true)
                {
                    try
                    {
                        txtLicDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(txtLicDOB.Text))
                        {
                            if (Convert.ToDateTime(txtLicDOB.Text).Date >= DateTime.Now.Date)
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

        private void txtIns_EffectiveDate_Validating(object sender, CancelEventArgs e)
        {
            txtIns_EffectiveDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (txtIns_EffectiveDate.Text.Length > 0 && txtIns_EffectiveDate.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (txtIns_EffectiveDate.MaskCompleted == true)
                {
                    try
                    {
                        txtIns_EffectiveDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(txtIns_EffectiveDate.Text))
                        {
                            if (Convert.ToDateTime(txtIns_EffectiveDate.Text).Date >= DateTime.Now.Date)
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

        private void txtIns_ExpiryDate_Validating(object sender, CancelEventArgs e)
        {
            txtIns_ExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (txtIns_ExpiryDate.Text.Length > 0 && txtIns_ExpiryDate.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (txtIns_ExpiryDate.MaskCompleted == true)
                {
                    try
                    {
                        txtIns_ExpiryDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(txtLicDOB.Text))
                        {
                            if (Convert.ToDateTime(txtIns_ExpiryDate.Text).Date >= DateTime.Now.Date)
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

       
        
        
       }
}