using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web;
using gloCommon;
using System.Runtime.InteropServices;
using gloRemoteScanGeneral;
using gloSettings;

namespace gloCardScanning
{
    public partial class frmSetupScannerSettings : Form
    {

        #region " Variable Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ClinicID = 0;
        private string _machineID = "";
        private static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public NetScanW.SLibEx mSLib;
        public NetScanWex.SLibEx mSLibEx;
        private string sLoadSetting = "";

        #endregion " Variable Declarations "

        #region  " Grid Constants "

        const int COL_COUNT = 7;

        const int COL_NID = 0;
        const int COL_SCANNERNAME = 1;
        const int COL_XCOORDINATE = 2;
        const int COL_YCOORDINATE = 3;
        const int COL_WIDTH = 4;
        const int COL_HEIGHT = 5;
        const int COL_ISDEFAULT = 6;

        #endregion

        #region " Log Method "

        static public void UpdateLog(string strLogMessage)
        {
            //System.IO.StreamWriter objFile = new System.IO.StreamWriter(Application.StartupPath + "\\gloPMLog.log", true);
            System.IO.StreamWriter objFile = new System.IO.StreamWriter(appSettings["StartupPath"].ToString() + "\\gloPMLog.log", true);
            objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "   " + strLogMessage);
            objFile.Close();
            if (objFile != null)
            {
                objFile.Dispose();
                objFile = null;
            }

            //MessageBox.Show("Error while accessing Database. Please click on Help to view log.", "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0, Application.StartupPath + "\\gloPMS_ERRORLog.log");
        }

        #endregion " Log Method "

        #region " Property Procedures "

        private string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        private Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        private string MachineID
        {
            get { return _machineID; }
            set { _machineID = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmSetupScannerSettings(string dataBaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = dataBaseConnectionString;
            _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            _machineID = System.Windows.Forms.SystemInformation.ComputerName;

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

        }

        #endregion " Constructor "

        #region " Form Load "

        private void frmSetupScannerSettings_Load(object sender, EventArgs e)
        {
            try
            {
                cmbColorScheme.Items.Clear();
                cmbColorScheme.Items.Add("True Color");
                cmbColorScheme.Items.Add("Gray Color");
                cmbColorScheme.Items.Add("Black & White");

                //cmbScannerType.Items.Clear();
                //cmbScannerType.Items.Add("CSSN_NONE");
                //cmbScannerType.Items.Add("CSSN_600");
                //cmbScannerType.Items.Add("CSSN_800");
                //cmbScannerType.Items.Add("CSSN_800N");
                //cmbScannerType.Items.Add("CSSN_1000");
                //cmbScannerType.Items.Add("CSSN_2000");
                //cmbScannerType.Items.Add("CSSN_2000N");
                //cmbScannerType.Items.Add("CSSN_800E");
                //cmbScannerType.Items.Add("CSSN_800EN");
                //cmbScannerType.Items.Add("CSSN_3000");
                //cmbScannerType.Items.Add("CSSN_4000");
                //cmbScannerType.Items.Add("CSSN_800G");
                //cmbScannerType.Items.Add("CSSN_5000");
                //cmbScannerType.Items.Add("CSSN_IDR");
                //cmbScannerType.Items.Add("CSSN_800DX");
                //cmbScannerType.Items.Add("CSSN_800DXN");
                //cmbScannerType.Items.Add("CSSN_FDA");
                //cmbScannerType.Items.Add("CSSN_TWN");


                cmbColorScheme.SelectedIndex = 0;
                rbSR600.Checked = true;
                rbLicense600.Checked = true;

                try
                {
                    gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Check Network Dir/File Exists : " + ex.ToString(), false);
                }

                if ((gloGlobal.gloTSPrint.TerminalServer() != "RDP") || (! gloGlobal.gloRemoteScanSettings.isScanServiceWorking()) )
                {
                    chkEnableRemoteScanner.Enabled = false;
                    chkEnableRemoteScanner.Checked = false;

                    chkEliminatePegasus.Checked = gloGlobal.gloEliminatePegasus.bEliminatePegasus;
                    chkEliminatePegasus.Enabled = true;
                    chkEliminatePegasus.Visible = true;

                    chkZipScannerSettings.Enabled = false;
                    chkZipScannerSettings.Checked = false;
                }
                else
                {
                    chkEnableRemoteScanner.Enabled = true;
                    chkEnableRemoteScanner.Checked = gloGlobal.gloRemoteScanSettings.EnableRemoteScan;

                    if (chkEnableRemoteScanner.Checked)
                    {
                        chkEliminatePegasus.Checked = false;
                        chkEliminatePegasus.Enabled = false;
                        chkEliminatePegasus.Visible = false;

                        chkZipScannerSettings.Enabled = true;
                        chkZipScannerSettings.Checked = gloGlobal.gloRemoteScanSettings.bZipScanSettings;
                    }
                    else 
                    {
                        chkEliminatePegasus.Checked = gloGlobal.gloEliminatePegasus.bEliminatePegasus;
                        chkEliminatePegasus.Enabled = true;

                        chkZipScannerSettings.Enabled = false;
                        chkZipScannerSettings.Checked = false;
                    }
                }

                btnRefreshTwainScanners.Visible = chkEliminatePegasus.Checked;
                btnRefreshRemoteScanner.Visible = chkEnableRemoteScanner.Checked;
                if (chkEliminatePegasus.Checked || chkEnableRemoteScanner.Checked)
                {
                    LoadRemoteScanners();
                }
                else
                {
                    //added on 04/05/2010: discusion with vinayak sir add scaner selection setting 
                    btnRefreshTwainScanners.Visible = false;
                    btnRefreshTwainScanners.Visible = false;
                    LoadScanerCombo();
                    //-----------------------------------------------------------------------
                }
                if (chkEliminatePegasus.Checked)
                {
                    sLoadSetting = "No Pegasus";
                }
                else
                {
                    sLoadSetting = "Pegasus";
                }

                //added on 10/05/10
                //rdbtnScanShell.Checked = false;
                //rdbtnOther.Checked = true ;
                //pnlScanShellProperty.Visible = false;
                this.Height = 342;
                twainPro1.Licensing.UnlockRuntime(1808984205, 249325542, 1216513884, 14413);
                LoadSettings();

                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        #endregion " Form Load "

        #region " Tool Strip Click Event "

        private void tls_SetupResource_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            SaveSettings();
                            this.Close();
                        }
                        break;

                    case "Cancel":
                        {
                            this.Close();
                        }
                        break;
                    case "Calibrate":
                        {
                            CalibrateScanner();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //Added by Shweta 20091107
                //To show proper message to the user 
                //Against Bugzilla Id:4306
                MessageBox.Show("Component required for card scanning is not available.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //End code adding 20091107
            }
        }


        #endregion " Tool Strip Click Event "

        #region  " Methods "

        //private void SaveSettings()
        //{
        //    int _Resolution = 0;
        //    int _ColorScheme = 0;
        //    int _ScanDuplex = 0;

        //    gloCardScanning ogloCardScanning = new gloCardScanning(_databaseconnectionstring);
        //    gloSettings.DatabaseSetting.DataBaseSetting   oSettings = new gloSettings.DatabaseSetting.DataBaseSetting(Application.StartupPath  );

        //    try
        //    {
        //        if (rdSR300.Checked == false && rbSR600.Checked == false)
        //        {
        //            MessageBox.Show("Please select the Scanner Resolution", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            rdSR300.Focus();
        //        }

        //        //Resolution
        //        if (rdSR300.Checked == true)
        //        {
        //            _Resolution = gloCSlibconst.RES_300;
        //        }
        //        else
        //        {
        //            _Resolution = gloCSlibconst.RES_600;
        //        }
        //        //

        //        //Color Scheme

        //        switch (cmbColorScheme.Text)
        //        {
        //            case "Gray Color":
        //                { _ColorScheme = gloCSlibconst.GRAY_COLOR; }
        //                break;
        //            case "True Color":
        //                { _ColorScheme = gloCSlibconst.TRUECOLOR; }
        //                break;
        //            case "Black & White":
        //                { _ColorScheme = gloCSlibconst.BW; }
        //                break;
        //        }

        //        //

        //        //Duplex Scan
        //        if (chkboxDuplex.Checked == true)
        //        {
        //            _ScanDuplex = 1;
        //        }
        //        else
        //        {
        //            _ScanDuplex = 0;
        //        }

        //        //
        //        oSettings.WriteSettings_XML ("CardScannerSettings", "Resolution", _Resolution.ToString());
        //        oSettings.WriteSettings_XML("CardScannerSettings", "ColorScheme", _ColorScheme.ToString());
        //        oSettings.WriteSettings_XML("CardScannerSettings", "ScanDuplex", _ScanDuplex.ToString());
        //        //

        //        //string CompId = System.Windows.Forms.SystemInformation.ComputerName.ToString();

        //        //ogloCardScanning.SaveScannerSettings(_Resolution, _ColorScheme, _ScanDuplex, CompId);



        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    { 

        //    }
        //}

        private void SaveSettings()
        {
            int _ResolutionLicense = 0;
            int _ResolutionInsurance = 0;
            int _ColorScheme = 0;
            int _ScanDuplex = 0;
            string _SelectedScanner = ""; // added on 04/05/2010
            string _ScannerType = "";
            int _IsScanPatientPhoto = 0;
            int _CenterImage = 0;
            int _UniformCardPrinting = 0;
            gloCardScanning ogloCardScanning = new gloCardScanning(_databaseconnectionstring);
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();

            try
            {
                if (rdSR300.Checked == false && rbSR600.Checked == false)
                {
                    MessageBox.Show("Please select the scanner resolution for the insurance card.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rdSR300.Focus();
                }

                if (rbLicense300.Checked == false && rbLicense600.Checked == false)
                {
                    MessageBox.Show("Please select the scanner resolution for the drivers license card.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rdSR300.Focus();
                }

                //Resolution Insurace Card
                if (rdSR300.Checked == true)
                {
                    _ResolutionInsurance = gloCSlibconst.RES_300;
                }
                else
                {
                    _ResolutionInsurance = gloCSlibconst.RES_600;
                }
                //

                //Resolution Insurace Card
                if (rbLicense300.Checked == true)
                {
                    _ResolutionLicense = gloCSlibconst.RES_300;
                }
                else
                {
                    _ResolutionLicense = gloCSlibconst.RES_600;
                }
                //

                //Color Scheme

                switch (cmbColorScheme.Text)
                {
                    case "Gray Color":
                        { _ColorScheme = gloCSlibconst.GRAY_COLOR; }
                        break;
                    case "True Color":
                        { _ColorScheme = gloCSlibconst.TRUECOLOR; }
                        break;
                    case "Black & White":
                        { _ColorScheme = gloCSlibconst.BW; }
                        break;
                }

                //

                //Duplex Scan
                if (chkboxDuplex.Checked == true)
                {
                    _ScanDuplex = 1;
                }
                else
                {
                    _ScanDuplex = 0;
                }

                //

                //-------select Scanner name from combo
                if (cmbPScanners.Items.Count > 0)
                {

                    if (cmbPScanners.Text.Contains("FUJITSU"))
                    {
                        _SelectedScanner = cmbPScanners.Text.Trim();  // "FUJITSU";
                    }
                    else if (cmbPScanners.Text.Contains("SCANSHELL"))
                    {
                        _SelectedScanner = cmbPScanners.Text.Trim();     // "SCANSHELL";
                    }

                }

                ////Added on 10/05/10
                //if (rdbtnOther.Checked == true) // check & set scanner type setting
                //{
                //    _ScannerType = "Other"; 
                //}
                //else if ( rdbtnScanShell.Checked == true )   
                //{
                //    _ScannerType ="SCANSHELL";

                //    if (chkBoxScanPhoto.Checked == true) //Check & set Scan Patient Photo Settings
                //    {
                //        _IsScanPatientPhoto = 1;
                //    }
                //    else 
                //    {
                //        _IsScanPatientPhoto = 0;
                //    }
                //}

                if (chkBoxScanPhoto.Checked == true) //Check & set Scan Patient Photo Settings
                {
                    _IsScanPatientPhoto = 1;
                }
                else
                {
                    _IsScanPatientPhoto = 0;
                }

                //Added for center Image
                if (chkCenterImage.Checked == true)
                {
                    _CenterImage = 1;
                }
                else
                {
                    _CenterImage=0;
                }

                //Uniform Card Printing
                if (chkUniformCardPrinting.Checked == true)
                {
                    _UniformCardPrinting = 1;
                }
                else
                {
                    _UniformCardPrinting = 0;
                }

                DataTable dtFinalData = null;
                c1ScannerProps.FinishEditing();
                if (c1ScannerProps.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1ScannerProps.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(c1ScannerProps.GetData(i, COL_XCOORDINATE)).Trim() != string.Empty)
                        {
                            oSettings.WriteSettings_XML("ScannerProperties_" + Convert.ToString(c1ScannerProps.GetData(i, COL_SCANNERNAME)).Trim(), "X", Convert.ToString(c1ScannerProps.GetData(i, COL_XCOORDINATE)).Trim());
                            oSettings.WriteSettings_XML("ScannerProperties_" + Convert.ToString(c1ScannerProps.GetData(i, COL_SCANNERNAME)).Trim(), "Y", Convert.ToString(c1ScannerProps.GetData(i, COL_YCOORDINATE)).Trim());
                            oSettings.WriteSettings_XML("ScannerProperties_" + Convert.ToString(c1ScannerProps.GetData(i, COL_SCANNERNAME)).Trim(), "Width", Convert.ToString(c1ScannerProps.GetData(i, COL_WIDTH)).Trim());
                            oSettings.WriteSettings_XML("ScannerProperties_" + Convert.ToString(c1ScannerProps.GetData(i, COL_SCANNERNAME)).Trim(), "Height", Convert.ToString(c1ScannerProps.GetData(i, COL_HEIGHT)).Trim());
                            oSettings.WriteSettings_XML("ScannerProperties_" + Convert.ToString(c1ScannerProps.GetData(i, COL_SCANNERNAME)).Trim(), "ScannerName", Convert.ToString(c1ScannerProps.GetData(i, COL_SCANNERNAME)).Trim());
                            oSettings.WriteSettings_XML("ScannerProperties_" + Convert.ToString(c1ScannerProps.GetData(i, COL_SCANNERNAME)).Trim(), "IsDefault", Convert.ToString(c1ScannerProps.GetData(i, COL_ISDEFAULT)).Trim());
                        }
                    }
                }

                if (dtFinalData != null && dtFinalData.Rows.Count > 0)
                {
                    //oSettings.WriteSettings_XML("CardScannerSettings", "CenterImage", dtFinalData[][].ToString()); //scan photo 
                }
                //-------

                //oSettings.WriteSettings_XML("CardScannerSettings", "Resolution", _Resolution.ToString());
                oSettings.WriteSettings_XML("CardScannerSettings", "ResolutionInsurance", _ResolutionInsurance.ToString());
                oSettings.WriteSettings_XML("CardScannerSettings", "ResolutionLicense", _ResolutionLicense.ToString());
                oSettings.WriteSettings_XML("CardScannerSettings", "ColorScheme", _ColorScheme.ToString());
                oSettings.WriteSettings_XML("CardScannerSettings", "ScanDuplex", _ScanDuplex.ToString());
                //oSettings.WriteSettings_XML("CardScannerSettings", "DefaultScanner", cmbScannerType.Text.Trim());
                oSettings.WriteSettings_XML("CardScannerSettings", "SelectedScanner", cmbPScanners.Text.Trim()); // added on 05/05/2010
                oSettings.WriteSettings_XML("CardScannerSettings", "ScannerType", _ScannerType );                       //scanner type
                oSettings.WriteSettings_XML("CardScannerSettings", "ScanPatientPhoto", _IsScanPatientPhoto.ToString()); //scan photo 
                oSettings.WriteSettings_XML("CardScannerSettings", "CenterImage", _CenterImage.ToString()); //scan photo 
                oSettings.WriteSettings_XML("CardScannerSettings", "UniformCardPrinting", _UniformCardPrinting.ToString()); //Uniform Card Printing
                //

                //string CompId = System.Windows.Forms.SystemInformation.ComputerName.ToString();

                //ogloCardScanning.SaveScannerSettings(_Resolution, _ColorScheme, _ScanDuplex, CompId);

                gloGlobal.gloRemoteScanSettings.EnableRemoteScan = chkEnableRemoteScanner.Checked;
                gloGlobal.gloRemoteScanSettings.bZipScanSettings = chkZipScannerSettings.Checked;
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                {
                    return;
                }
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrEnableRemoteScan, chkEnableRemoteScanner.Checked);
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrZipScannerSettings, chkZipScannerSettings.Checked);

                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrEliminatePegasus, chkEliminatePegasus.Checked);
                gloGlobal.gloEliminatePegasus.bEliminatePegasus = chkEliminatePegasus.Checked;

                String sCurrSetting = "";
                if (chkEliminatePegasus.Checked)
                {
                    sCurrSetting = "No Pegasus";
                }
                else
                {
                    sCurrSetting = "Pegasus";
                }
                if (sCurrSetting != sLoadSetting)
                {
                    if (sCurrSetting == "Pegasus")
                    {
                        if (!String.IsNullOrEmpty(gloGlobal.gloEliminatePegasus.sPegasusBright))
                        {
                            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSBright, gloGlobal.gloEliminatePegasus.sPegasusBright);
                        }
                        if (!String.IsNullOrEmpty(gloGlobal.gloEliminatePegasus.sPegasusContrast))
                        {
                            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSContrast, gloGlobal.gloEliminatePegasus.sPegasusContrast);
                        }
                    }
                    else
                    {
                    }
                }

                gloRegistrySetting.CloseRegistryKey();
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
                if (ogloCardScanning != null)
                {
                    ogloCardScanning.Dispose();
                    ogloCardScanning = null;
                }
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
                _SelectedScanner = null;
                _ScannerType = null;
            }
        }

        private string getScannersSettingsFromXML(string Name,string Tag)
        {
            string result="";
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                if (oSettings.ReadSettings_XML(Name,Tag) != "")
                {
                    result = oSettings.ReadSettings_XML(Name, Tag); 
                }
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
            }
            return result;
        }

        private void LoadSettings()
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                //Resolution Insurance Card
                if (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionInsurance") != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionInsurance"))
                    {
                        case "300":
                            rdSR300.Checked = true;
                            rbSR600.Checked = false;
                            break;
                        case "600":
                            rbSR600.Checked = true;
                            rdSR300.Checked = false;
                            break;
                    }

                }
                //

                //Resolution Drivers License Card
                if (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionLicense") != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionLicense"))
                    {
                        case "300":
                            rbLicense300.Checked = true;
                            rbLicense600.Checked = false;
                            break;
                        case "600":
                            rbLicense600.Checked = true;
                            rbLicense300.Checked = false;
                            break;
                    }

                }
                //

                //ColorScheme
                if (oSettings.ReadSettings_XML("CardScannerSettings", "ColorScheme") != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ColorScheme"))
                    {
                        case "4":
                            cmbColorScheme.Text = "True Color";
                            break;
                        case "1":
                            cmbColorScheme.Text = "Gray Color";
                            break;
                        case "2":
                            cmbColorScheme.Text = "Black & White";
                            break;
                    }
                }
                //

                //ScanDuplex
                if (oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex") != "")
                {
                    string _scanduplex = oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex");
                    if (_scanduplex == "1")
                    {
                        chkboxDuplex.Checked = true;
                    }
                    else
                    {
                        chkboxDuplex.Checked = false;
                    }
                }
                //

                ////Default Scanner
                //if (oSettings.ReadSettings_XML("CardScannerSettings", "DefaultScanner") != "")
                //{
                //    cmbScannerType.Text = oSettings.ReadSettings_XML("CardScannerSettings", "DefaultScanner");
                //}
                ////

                //Load selected combo scanner
                if (oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner") != "")
                {
                    cmbPScanners.Text = oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner");
                }

                ////Load scanner Type 
                //if (oSettings.ReadSettings_XML("CardScannerSettings", "ScannerType") != "")
                //{
                    
                //    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ScannerType"))
                //    {
                //        case "Other":
                //            rdbtnOther.Checked = true;
                //            rdbtnScanShell.Checked = false ;
                //            break;
                //        case "SCANSHELL":
                //            rdbtnScanShell.Checked = true;
                //            rdbtnOther.Checked = false; 
                //            break;
                //    }
                //}

                //Load scan Patient photo
                if (oSettings.ReadSettings_XML("CardScannerSettings", "ScanPatientPhoto") != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ScanPatientPhoto"))
                    {
                        case "1":
                            chkBoxScanPhoto.Checked = true;
                            break;
                        case "0":
                            chkBoxScanPhoto.Checked = false;
                            break;
                    }
                }

                if (oSettings.ReadSettings_XML("CardScannerSettings", "CenterImage") != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "CenterImage"))
                    {
                        case "1":
                            chkCenterImage.Checked = true;
                            break;
                        case "0":
                            chkCenterImage.Checked = false;
                            break;
                    }
                }

                //Load patient card printing setting
                if (oSettings.ReadSettings_XML("CardScannerSettings", "UniformCardPrinting") != "")
                {
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "UniformCardPrinting"))
                    {
                        case "1":
                            chkUniformCardPrinting.Checked = true;
                            break;
                        case "0":
                            chkUniformCardPrinting.Checked = false;
                            break;
                    }
                }
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
            }
        }

        //added on 04/05/2010
        private void LoadScanerCombo()
        {

            PegasusImaging.WinForms.TwainPro5.DataSourceCollection oScanners=null;
            PegasusImaging.WinForms.TwainPro5.TwainDevice twainDevice = new PegasusImaging.WinForms.TwainPro5.TwainDevice(twainPro1);
            //int _curScnrIndx = -1;
            int i = 0;
            try
            {
                cmbPScanners.Items.Clear();

                twainDevice.OpenDataSourceManager();
                oScanners = new PegasusImaging.WinForms.TwainPro5.DataSourceCollection(twainDevice);
                cmbPScanners.Items.Add("None");
                for (i = 0; i <= oScanners.Count - 1; i++)
                {
                    cmbPScanners.Items.Add(oScanners[i].ToString());
                }
                cmbPScanners.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oScanners != null)
                {
                    oScanners.Dispose();
                    oScanners = null;
                }
                if (twainDevice != null)
                {
                    twainDevice.Dispose();
                    twainDevice = null;
                }
            }
        }

        private void LoadRemoteScanners()
        {
            try
            {
                cmbPScanners.Items.Clear();
                cmbPScanners.Items.Add("None");
                for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner.Length; i++)
                {
                    cmbPScanners.Items.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[i].Name);
                }
                cmbPScanners.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void CalibrateScanner()
        {
            if (chkEnableRemoteScanner.Checked || chkEliminatePegasus.Checked)
            {
                try
                {
                    if (!chkEliminatePegasus.Checked)
                    {
                        if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                        {
                            return;
                        }
                    }
                    if (MessageBox.Show("Please insert calibration card and click OK.  ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        CardScanSettingsScanCardSettings cardScanSettings = new CardScanSettingsScanCardSettings();
                        cardScanSettings.CardType = "Calibrate";
                        cardScanSettings.Status = (int)CardScanStatus.SentToScan;
                        cardScanSettings.Description = "";
                        if (gloScannerGeneral.PerformRemoteScan(ref cardScanSettings))
                        {
                            MessageBox.Show("Calibration  completed.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                this.Cursor = Cursors.Default;
            }
            else
            {
                try
                {
                    try
                    {
                        mSLib = new NetScanW.SLibEx();
                        mSLibEx = new NetScanWex.SLibEx();
                    }
                    catch (COMException ce)
                    {
                        if ((uint)ce.ErrorCode == 0x80040154)
                        {
                            MessageBox.Show("Scanning device is not installed on the machine. Install the scanning device or contact your system administrator.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    mSLibEx.DefaultScanner = gloCSlibconst.CSSN_3000;
                    int ret = mSLib.InitLibrary(ClsgloScanConstants.LICENSE_VALUE);
                    if (ret != ClsgloScanConstants.LICENSE_VALID)
                    {
                        UpdateLog(" Slib-LICENSE is Invalid LICENSE_VALID = " + ret.ToString());
                        switch (ret)
                        {
                            case ClsgloScanConstants.LICENSE_EXPIRED:
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Error: License expired! - Library not loaded (SLib).  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //ExitApplication();
                                break;
                            case gloCSlibconst.SLIB_LIBRARY_ALREADY_INITIALIZED:
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Error: Library is already loaded.  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            case gloCSlibconst.SLIB_ERR_SCANNER_NOT_FOUND:
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Scanner is not working.  Please check cables and confirm that power is on.  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            case gloCSlibconst.SLIB_ERR_DRIVER_NOT_FOUND:
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Error: The scanner's driver was not found. Please re-install the driver and re-start the application.  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                        }
                    }
                    else
                    {
                        //Calibrate the scanner
                        if (MessageBox.Show("Please insert calibration card and click OK.  ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            mSLib.CalibrateScanner();
                            this.Cursor = Cursors.Default;

                            if (mSLib.IsNeedCalibration == 0)
                            {
                                MessageBox.Show("Calibration  completed.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show("Calibration failed.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    this.Cursor = Cursors.Default;
                }
                finally
                {
                    if (mSLibEx != null)
                        mSLibEx.UnInit();
                    mSLib = null;

                }
            }
        }

        #endregion " Methods "

        private void rdSR300_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSR300.Checked == true)
            {
                rdSR300.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rdSR300.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbSR600_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSR600.Checked == true)
            {
                rbSR600.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbSR600.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbLicense300_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLicense300.Checked == true)
            {
                rbLicense300.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbLicense300.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbLicense600_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLicense600.Checked == true)
            {
                rbLicense600.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbLicense600.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        //private void cmbScannerType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbScannerType.Text.Trim() != "")
        //    {
        //        switch (cmbScannerType.Text.Trim())
        //        {
        //            case "CSSN_3000":
        //            case "CSSN_5000":
        //                chkboxDuplex.Enabled = true;
        //                break;
        //            default:
        //                chkboxDuplex.Checked = false;
        //                chkboxDuplex.Enabled = false;
        //                break;
        //        }
        //    }

        //}

        private void cmbPScanners_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable _dtScannerInfo = null;
            try
            {
                if (cmbPScanners.Items.Count > 0)
                {
                    if (cmbPScanners.SelectedIndex.ToString().Trim() != "")
                    {
                        cmbPScanners.Text = cmbPScanners.SelectedItem.ToString();
 
                    }
                    if (cmbPScanners.Text != "" && cmbPScanners.Text == "RemoteScan(TM)")
                    {
                        DesignScannerPropsGrid();
                        this.Height = 506;
                        pnlScannerSettings.Visible = true;
                       
                        gloCardScanning ogloCardScanning = new gloCardScanning(_databaseconnectionstring);
                        _dtScannerInfo = ogloCardScanning.GetScannerProperties();
                        if (_dtScannerInfo != null && _dtScannerInfo.Rows.Count > 0)
                        {
                            foreach (DataRow row in _dtScannerInfo.Rows)
                            {
                                c1ScannerProps.Rows.Add();
                                int i = c1ScannerProps.Rows.Count - 1;
                                c1ScannerProps.SetData(i, COL_NID, row["nID"]);

                                string result = "";
                                result = getScannersSettingsFromXML("SCANNERPROPERTIES_" + row["sScannerName"], "SCANNERNAME");
                                if (result == "")
                                     c1ScannerProps.SetData(i, COL_SCANNERNAME, row["sScannerName"]);
                                else
                                    c1ScannerProps.SetData(i, COL_SCANNERNAME, result);

                                result = "";
                                result = getScannersSettingsFromXML("SCANNERPROPERTIES_" + row["sScannerName"], "X");
                                if (result == "")
                                    c1ScannerProps.SetData(i, COL_XCOORDINATE, row["nXcoOrdinate"]);
                                else
                                    c1ScannerProps.SetData(i, COL_XCOORDINATE, result);

                                result = "";
                                result = getScannersSettingsFromXML("SCANNERPROPERTIES_" + row["sScannerName"], "Y");
                                if (result == "")
                                    c1ScannerProps.SetData(i, COL_YCOORDINATE, row["nYcoOrdinate"]);
                                else
                                    c1ScannerProps.SetData(i, COL_YCOORDINATE, result);

                                result = "";
                                result = getScannersSettingsFromXML("SCANNERPROPERTIES_" + row["sScannerName"], "WIDTH");
                                if (result == "")
                                    c1ScannerProps.SetData(i, COL_WIDTH, row["nWidth"]);
                                else
                                    c1ScannerProps.SetData(i, COL_WIDTH, result);

                                result = "";
                                result = getScannersSettingsFromXML("SCANNERPROPERTIES_" + row["sScannerName"], "HEIGHT");
                                if (result == "")
                                    c1ScannerProps.SetData(i, COL_HEIGHT, row["nHeight"]);
                                else
                                    c1ScannerProps.SetData(i, COL_HEIGHT, result);

                                result = "";
                                result = getScannersSettingsFromXML("SCANNERPROPERTIES_" + row["sScannerName"], "ISDEFAULT");
                                if (result == "")
                                    c1ScannerProps.SetData(i, COL_ISDEFAULT, false);
                                else
                                    c1ScannerProps.SetData(i, COL_ISDEFAULT, result);
                                
                                //c1ScannerProps.SetData(i, COL_NID, row["nID"]);
                                //c1ScannerProps.SetData(i, COL_SCANNERNAME, row["sScannerName"]);
                                //c1ScannerProps.SetData(i, COL_XCOORDINATE, row["nXcoOrdinate"]);
                                //c1ScannerProps.SetData(i, COL_YCOORDINATE, row["nYcoOrdinate"]);
                                //c1ScannerProps.SetData(i, COL_WIDTH, row["nWidth"]);
                                //c1ScannerProps.SetData(i, COL_HEIGHT, row["nHeight"]);
                                //c1ScannerProps.SetData(i, COL_ISDEFAULT, row["bIsDefault"]);
                            }
                        }
                    }
                    else
                    {
                        this.Height = 342;  
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void DesignScannerPropsGrid()
        {
            try
            {
                #region " Grid Settings "

                c1ScannerProps.Redraw = false;
                c1ScannerProps.Clear();

                c1ScannerProps.Cols.Count = COL_COUNT;
                c1ScannerProps.Rows.Count = 1;
                c1ScannerProps.Rows.Fixed = 1;
                c1ScannerProps.Cols.Fixed = 0;

                c1ScannerProps.AllowEditing = true;
                c1ScannerProps.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1ScannerProps.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #endregion

                #region " Set Headers "

                c1ScannerProps.SetData(0, COL_SCANNERNAME, "Scanner Type");
                c1ScannerProps.SetData(0, COL_XCOORDINATE, "X");
                c1ScannerProps.SetData(0, COL_YCOORDINATE, "Y");
                c1ScannerProps.SetData(0, COL_WIDTH, "Width");
                c1ScannerProps.SetData(0, COL_HEIGHT, "Height");
                c1ScannerProps.SetData(0, COL_ISDEFAULT, "Connected");

                #endregion

                #region " Show/Hide "
                c1ScannerProps.Cols[COL_NID].Visible = false;
                #endregion

                #region " Width "

                c1ScannerProps.Cols[COL_SCANNERNAME].Width = 150;
                c1ScannerProps.Cols[COL_XCOORDINATE].Width = 80;
                c1ScannerProps.Cols[COL_YCOORDINATE].Width = 80;
                c1ScannerProps.Cols[COL_WIDTH].Width = 80;
                c1ScannerProps.Cols[COL_HEIGHT].Width = 80;
                c1ScannerProps.Cols[COL_ISDEFAULT].Width = 70;

                #endregion

                #region " Data Type "

                c1ScannerProps.Cols[COL_SCANNERNAME].DataType = typeof(System.String);
                c1ScannerProps.Cols[COL_XCOORDINATE].DataType = typeof(System.Decimal);
                c1ScannerProps.Cols[COL_YCOORDINATE].DataType = typeof(System.Decimal);
                c1ScannerProps.Cols[COL_WIDTH].DataType = typeof(System.Decimal);

                c1ScannerProps.Cols[COL_HEIGHT].DataType = typeof(System.Decimal);
                c1ScannerProps.Cols[COL_ISDEFAULT].DataType = typeof(System.Boolean);

                #endregion

                #region " Alignment "

                //c1ScannerProps.Cols[COL_COMPANY].TextAlign = TextAlignEnum.LeftCenter;
                //c1ScannerProps.Cols[COL_CHECK_NO].TextAlign = TextAlignEnum.LeftCenter;

                #endregion
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { c1ScannerProps.Redraw = true; }
        }

        private void btnDefaultSet_Click(object sender, EventArgs e)
        {
            DataTable _dtScannerInfo = null;
            try
            {
                if (cmbPScanners.Items.Count > 0)
                {
                    if (cmbPScanners.Text != "" && cmbPScanners.Text == "RemoteScan(TM)")
                    {
                        DesignScannerPropsGrid();
                        this.Height = 506;
                        pnlScannerSettings.Visible = true;

                        gloCardScanning ogloCardScanning = new gloCardScanning(_databaseconnectionstring);
                        _dtScannerInfo = ogloCardScanning.GetScannerProperties();
                        if (_dtScannerInfo != null && _dtScannerInfo.Rows.Count > 0)
                        {
                            foreach (DataRow row in _dtScannerInfo.Rows)
                            {
                                c1ScannerProps.Rows.Add();
                                int i = c1ScannerProps.Rows.Count - 1;
                                c1ScannerProps.SetData(i, COL_NID, row["nID"]);
                                c1ScannerProps.SetData(i, COL_SCANNERNAME, row["sScannerName"]);
                                c1ScannerProps.SetData(i, COL_XCOORDINATE, row["nXcoOrdinate"]);
                                c1ScannerProps.SetData(i, COL_YCOORDINATE, row["nYcoOrdinate"]);
                                c1ScannerProps.SetData(i, COL_WIDTH, row["nWidth"]);
                                c1ScannerProps.SetData(i, COL_HEIGHT, row["nHeight"]);
                                c1ScannerProps.SetData(i, COL_ISDEFAULT, false);
                            }
                        }
                    }
                    else
                    {
                        this.Height = 342;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (_dtScannerInfo != null) { _dtScannerInfo.Dispose(); _dtScannerInfo = null; }
            }
        }

        private void c1ScannerProps_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                c1ScannerProps.Redraw = false;
                if (e.Row > 0 && e.Col == COL_ISDEFAULT)
                {
                    int i = 0;
                    if (c1ScannerProps.GetCellCheck(e.Row, e.Col) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        for (i = 1; i <= c1ScannerProps.Rows.Count - 1; i++)
                        {
                            if (e.Row != i)
                            {
                                c1ScannerProps.SetCellCheck(i, COL_ISDEFAULT, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            }
                        }
                    }
                }

                c1ScannerProps.Redraw = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }

        private void c1ScannerProps_SetupEditor(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == COL_XCOORDINATE || e.Col == COL_YCOORDINATE || e.Col == COL_WIDTH || e.Col == COL_HEIGHT)
            {
                ((TextBox)c1ScannerProps.Editor).MaxLength = 8;
            }
        }

        private void c1ScannerProps_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            c1ScannerProps.Editor = (TextBox)c1ScannerProps.Editor;
        }

        private void btnDefaultSet_MouseHover(object sender, EventArgs e)
        {
            btnDefaultSet.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_LongYellow;
            btnDefaultSet.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnDefaultSet_MouseLeave(object sender, EventArgs e)
        {
            btnDefaultSet.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_LongButton;
            btnDefaultSet.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void chkEnableRemoteScanner_CheckedChanged(object sender, EventArgs e)
        {
            if ((gloGlobal.gloTSPrint.TerminalServer() == "RDP") && (gloGlobal.gloRemoteScanSettings.isScanServiceWorking()))
            {
                if (!chkEnableRemoteScanner.Checked)
                {
                    chkEliminatePegasus.Enabled = true;
                    chkEliminatePegasus.Checked = gloGlobal.gloEliminatePegasus.bEliminatePegasus;
                    chkEliminatePegasus.Visible = true;

                    chkZipScannerSettings.Checked = false;
                    chkZipScannerSettings.Enabled = false;
                }
                else
                {
                    chkEliminatePegasus.Enabled = false;
                    chkEliminatePegasus.Checked = false;
                    chkEliminatePegasus.Visible = false;

                    chkZipScannerSettings.Enabled = true;
                    chkZipScannerSettings.Checked = gloGlobal.gloRemoteScanSettings.bZipScanSettings;
                }
            }


            btnRefreshRemoteScanner.Visible = chkEnableRemoteScanner.Checked;
            if (chkEnableRemoteScanner.Checked)
            {
                gloGlobal.gloRemoteScanSettings.AssignReEvaluate();

                gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();

                gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject(chkEliminatePegasus.Checked);
                LoadRemoteScanners();
            }
            else
            {
                LoadScanerCombo();
            }
            LoadSettings();
        }
        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloCardScanning.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;

        }
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloCardScanning.Properties.Resources.Img_LongButton;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btnRefreshRemoteScanner_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnRefreshRemoteScanner.Enabled = false;
                Application.DoEvents();
                if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                {
                    return;
                }
                if (gloRemoteScanGeneral.RemoteScanSettings.RefreshScanners() == true)
                {
                    //string sRetVal = null;
                    ////Current Settings
                    //sRetVal = oRemoteScanCommon.SetRemoteScannerCurrentSettings(null, null, null);
                    //if (!string.IsNullOrEmpty(sRetVal)) { gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, sRetVal, gloAuditTrail.ActivityOutCome.Failure); }
                    gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject(chkEliminatePegasus.Checked);
                    LoadRemoteScanners();
                    LoadSettings();

                    //Update clients machine name
                    gloAuditTrail.MachineDetails.MachineInfo myRemoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails(true);
                    gloGlobal.gloTSPrint.sClientLocalMachineName = myRemoteMachine.MachineName;
                    MessageBox.Show("Scanners Refreshed", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Unable to update scanner list, Please try after some time.", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

            finally
            {
                this.Cursor = Cursors.Default;
                btnRefreshRemoteScanner.Enabled = true;
                Application.DoEvents();
            }
        }

        private void chkEliminatePegasus_CheckedChanged(object sender, EventArgs e)
        {
            if ((gloGlobal.gloTSPrint.TerminalServer() == "RDP"))
            {

            }
            else
            {

            }

            if (chkEliminatePegasus.Checked)
            {
                btnRefreshTwainScanners.Visible = true;
                gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject(chkEliminatePegasus.Checked);
                LoadRemoteScanners();
            }
            else
            {
                btnRefreshTwainScanners.Visible = false;
                LoadScanerCombo();
            }
            LoadSettings();
        }

        private void btnRefreshTwainScanners_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnRefreshTwainScanners.Enabled = false;
                Application.DoEvents();
                //if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                //{
                //    return;
                //}
                if (gloRemoteScanGeneral.TwainScanFunctionality.CreateTwainScanSettingsFile())
                {
                    try
                    {
                        gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject(chkEliminatePegasus.Checked);
                        LoadRemoteScanners();
                        LoadSettings();
                        MessageBox.Show("Scanners Refreshed", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to update scanner list, Please try after some time.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                   
                }
                else
                {
                    if (string.IsNullOrEmpty(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg))
                    {
                        MessageBox.Show("Scanners not refreshed", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg, gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

            finally
            {
                this.Cursor = Cursors.Default;
                btnRefreshTwainScanners.Enabled = true;
                Application.DoEvents();
            }
        }

        private void chkZipMetadata_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
