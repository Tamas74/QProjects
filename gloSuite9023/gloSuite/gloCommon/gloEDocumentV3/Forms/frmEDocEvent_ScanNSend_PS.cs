using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using PegasusImaging.WinForms.TwainPro5;

using System.IO;
using gloEDocumentV3.Enumeration;
using gloEDocumentV3.SDKInteraction;
using gloSettings;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using Saraff.Twain;
using gloRemoteScanGeneral;

namespace gloEDocumentV3.Forms
{
    public partial class frmEDocEvent_ScanNSend_PS : Form
    {

        #region " Constructors "

        public frmEDocEvent_ScanNSend_PS(bool OpenedFromExam)
        {
            InitializeComponent();

            PagasusLicencing();
            bOpenedFromExam = OpenedFromExam;
        }

        private void PagasusLicencing()
        {
            try
            {
                if (!gloGlobal.gloEliminatePegasus.bEliminatePegasus && twainPro1 != null)
                {
                    twainPro1.Licensing.UnlockRuntime(1808984205, 249325542, 1216513884, 14413);
                    //imagXpress1.Licensing.UnlockRuntime(1908208815, 373700144, 1341181380, 19197);
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisposingTwain()
        {
            try
            {
                if (twainDevice != null)
                {
                    twainDevice.Dispose();
                    twainDevice = null;
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public frmEDocEvent_ScanNSend_PS()
        {
            InitializeComponent();
            PagasusLicencing();
        }

        #endregion " Constructors "

        #region " Private and Public Variables "

        public string _ErrorMessage = "";

        private bool bOpenedFromExam = false;
        public bool oDialogResultIsOK = false;
        public Int64 oDialogDocumentID = 0;
        public Int64 oDialogContainerID = 0;

        public Int64 oPatientID = 0;
        public Int64 oScanInCategoryID = 0;
        public string oScanInCategory = "";
        public string oScanInSubCategory = "";
        public string oScanInYear = "";
        public string oScanInMonth = "";
        public Int64 oClinicID = 0;
        public enum_DocumentEventType oEventType = enum_DocumentEventType.None;
        public enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None;

        

        public ArrayList oDialogScanImages = new ArrayList();

        private PegasusImaging.WinForms.TwainPro5.TwainDevice twainDevice;

        //private Twain32 SaraffTwainDevice;
        public static frmLocalTwainScanLauncher objLocalTwainScanLauncher = null;

        private System.Int32 _nImageCount;
        private string _ScanImagesTempFolderPath = "";

        //Sandip Darade   20090926
        // Add card size setting 
        private string sSetting_CardWidth = "CardWidth";
        private string sSetting_CardLength = "CardLength";
        bool isClosedClick = false;


        private int COL_NAME = 0;
        private int COL_PATH = 1;
        private bool _IsFormLoading = false;

        private bool _IsScannerConnected = true;

        SaveOptions _SaveOption;
        bool _ScanningInProgress = false;

        enum_ZoomState _ZoomState = enum_ZoomState.BestFit;

        //Added By Shweta 20091130
        //To maintain the count of loaded image
        public Int32 nCountImageNo = 0;
        private string invalidCharString = ":" + "*" + "?" + "<" + ">" + "\\" + "//";
        bool _IsScanCard = false;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd); //new handle added 25-feb-2013
      
        #endregion

        #region " Form Load Event "

        private void frmEDocEvent_ScanNSend_PS_Load(object sender, EventArgs e)
        {
            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
            {
                pnlDocumentName.Visible = false;
            }

            gloUserControlLibrary.gloC1FlexStyle.Style(c1Documents, true);

            gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();

            try
            {
                gloPatient.gloPatient.GetWindowTitle(this, oPatientID, gloEDocV3Admin.gDatabaseConnectionString , gloEDocV3Admin.gMessageBoxCaption);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

            btn_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Rewind;
            btn_Left.BackgroundImageLayout = ImageLayout.Center;

            btnZoomOut.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Minus1;
            btnZoomOut.BackgroundImageLayout = ImageLayout.Center;

            btnZoomIn.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Pluse;
            btnZoomIn.BackgroundImageLayout = ImageLayout.Center;

            try
            {
                //imageXView.Image = null;
                imageControl1.CurrImage = null;
                //imageControl1.SetImageSource(null);
                _IsFormLoading = true;
                tlb_Ok.Visible = true;
                tlb_Cancel.Visible = true;
                tlb_Remove.Visible = true;
                tlb_RemoveAll.Visible = true;
                tlb_LoadImages.Visible = true;
                tlb_RotateForward.Visible = true;
                tlb_RotateBack.Visible = true;
                tlb_Settings.Visible = true;

                tlb_SaveSetting.Visible = false;
                tlb_CloseSetting.Visible = false;

                pnlSetting.Visible = false;
                pnlScanDocument.Visible = true;
                splitter1.Visible = true;

                pnlSmallStrip.Visible = false;
                pnlDocumentNameAcquiredImages.Visible = true;

                InitPagasusTwainDevice();
                
                #region "Scanner Setting Default Data"
                //Scan Mode

                cmbScanMode.Items.Clear();

                cmbScanMode.Items.Add("Black & White");

                cmbScanMode.Items.Add("Color");

                cmbScanMode.SelectedIndex = 0;

                //Scan Side
                cmbScanSide.Items.Clear();
                cmbScanSide.Items.Add("Front Side");
                cmbScanSide.Items.Add("Duplex");
                cmbScanSide.SelectedIndex = 0;

                //Scan DPI
                cmbResolution.Items.Clear();
                cmbResolution.Items.Add(Common.Scanner.ScanDPI.DPI_100);
                cmbResolution.Items.Add(Common.Scanner.ScanDPI.DPI_150);
                cmbResolution.Items.Add(Common.Scanner.ScanDPI.DPI_200);
                cmbResolution.Items.Add(Common.Scanner.ScanDPI.DPI_240);
                cmbResolution.Items.Add(Common.Scanner.ScanDPI.DPI_300);
                cmbResolution.SelectedIndex = 2;
                cmbResolution.Enabled = false;

                //Scan Contrast & Brightness
                cmbContrast.Items.Clear();
                cmbBrightness.Items.Clear();

                cmbContrast.Items.Add(Common.Scanner.ScanContrastBrightness.CB_24);
                cmbContrast.Items.Add(Common.Scanner.ScanContrastBrightness.CB_48);
                cmbContrast.Items.Add(Common.Scanner.ScanContrastBrightness.CB_72);
                cmbContrast.Items.Add(Common.Scanner.ScanContrastBrightness.CB_96);
                cmbContrast.Items.Add(Common.Scanner.ScanContrastBrightness.CB_128);

                cmbBrightness.Items.Add(Common.Scanner.ScanContrastBrightness.CB_24);
                cmbBrightness.Items.Add(Common.Scanner.ScanContrastBrightness.CB_48);
                cmbBrightness.Items.Add(Common.Scanner.ScanContrastBrightness.CB_72);
                cmbBrightness.Items.Add(Common.Scanner.ScanContrastBrightness.CB_96);
                cmbBrightness.Items.Add(Common.Scanner.ScanContrastBrightness.CB_128);

                cmbContrast.SelectedIndex = 3;
                cmbBrightness.SelectedIndex = 3;

                cmbContrast.Enabled = false;
                cmbBrightness.Enabled = false;

                chkShowScannerDialog.Checked = false;


                #region "Dhruv 20100621 ->Select Scanner"


                AddScanner();
                #endregion


                #endregion

                if (gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                {

                }
                else
                {

                }
                _nImageCount = 0;

                //if (myWatcher != null)
                //{
                //    myWatcher.OnClipboardContentChanged += new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
                //}

                //Commented -> Dhruv
                
                
                chkSplitFile.Checked = true;
                txtDocumentName.Text = eDocManager.eDocValidator.GetNewDocumentName(oPatientID, oScanInCategory, oClinicID, _OpenExternalSource);

                _ScanImagesTempFolderPath = gloEDocV3Admin.gTemporaryProcessPath + "\\TempScan\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff");// +System.Guid.NewGuid().ToString();DateTime.Now.ToString("MMddyyyyHHmmssffff") + System.Guid.NewGuid().ToString();
                if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
                {
                    System.IO.Directory.CreateDirectory(_ScanImagesTempFolderPath);
                    if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
                    {
                        _ErrorMessage = "Unable to create directory " + _ScanImagesTempFolderPath;
                        AuditLogErrorMessage(_ErrorMessage);
                    }
                }
                //MessageBox.Show(_ScanImagesTempFolderPath);
                DesignGrid();
                tlb_Remove.Enabled = false;
                tlb_RemoveAll.Enabled = false;
                tlb_RotateBack.Enabled = false;
                tlb_RotateForward.Enabled = false;
                _IsFormLoading = false;
                pnlProgressBar.Visible = false;
                //to set focus on document name after loading form
                //Incident #64334
                this.ActiveControl = txtDocumentName;

                if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                {
                    gloGlobal.gloProgressAndClipboard.GetClipboardData();
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion " Form Load "


        #region "Dhruv 20100621 -> AddScanner Called over the form load"
        private void AddScanner()
        {
            cmbScanner.Items.Clear();
            DataSourceCollection oScanners = null;//new DataSourceCollection(twainDevice);
            try
            {
                if (twainDevice != null)
                {
                    twainDevice.OpenDataSourceManager();
                    oScanners = new DataSourceCollection(twainDevice);
                    if (oScanners != null)
                    {
                        for (int i = 0; i <= oScanners.Count - 1; i++)
                        {
                            cmbScanner.Items.Add(oScanners[i].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                if (oScanners != null)
                {
                    oScanners.Dispose();
                    oScanners = null;
                }
            }
        }
        #endregion "Dhruv 20100621 -> AddScanner Called over the form load"


        #region "Dhruv 20100621 -> Select Scanning Part "
        
        private void SelectScanner()
        {

            DataSourceCollection oScanners = null;
            _IsScannerConnected = false;  //Flag
            try
            {


                if (twainDevice != null)
                {
                    twainDevice.OpenDataSourceManager();
                    if (cmbScanner.SelectedItem != null)
                    {
                        oScanners = new DataSourceCollection(twainDevice);
                        if (oScanners != null)
                        {
                            string myString = cmbScanner.SelectedItem.ToString().ToUpper();
                            for (int i = 0; i <= oScanners.Count - 1; i++)
                            {
                                if (myString == oScanners[i].ToString().ToUpper())
                                {
                                    oScanners.Current = i;
                                    _IsScannerConnected = true;
                                    break;
                                }
                            }
                        }
                    }

                }
            }


            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                if (oScanners != null)
                {
                    oScanners.Dispose();
                    oScanners = null;
                }
            }
        }
        #endregion "Dhruv 20100621 -> Select Scanning Part"


        #region "Dhruv 20100621 -> Application Settings"
       
        private void ApplySettings(bool IsCardScan)
        {
            try
            {
                
                #region "Variable Decleration"
                PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat myCap = null;
                //RegistryKey regKey = null;
                string _Resolution = "";
                string _ScanSide = "";
                string _ScanMode = "";
                string _ScanBrightness = "";
                string _ScanContrast = "";
                #endregion




                if (twainDevice.IsCapabilitySupported(Capability.IcapUnits) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                    myCap.Value = (float)PegasusImaging.WinForms.TwainPro5.CapabilityConstants.TwunInches;
                    twainDevice.SetCapability(myCap);

                   
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                    {
                        return;
                    }
                    cmbScanner.SelectedIndex = -1;
                    SelectMyComboBoxSetting(ref cmbScanner, gloRegistrySetting.gstrDMSScan, ""); //Select the data from combobox

                    object oSetting_DMSResolution = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSResol);
                    if (oSetting_DMSResolution != null)
                    {
                        _Resolution = oSetting_DMSResolution.ToString();
                        if (_Resolution.Trim() != "")
                        {
                            for (int i = 0; i < cmbResolution.Items.Count; i++)
                            {
                                if (cmbResolution.Items[i].ToString() == _Resolution)
                                {
                                    cmbResolution.SelectedIndex = i;
                                    try
                                    {

                                        if (twainDevice.IsCapabilitySupported(Capability.IcapXResolution) == true)
                                        {
                                            myCap = new CapabilityContainerOneValueFloat(Capability.IcapXResolution);
                                            myCap.Value = (float)(Convert.ToInt32(cmbResolution.SelectedItem.ToString()));
                                            twainDevice.SetCapability(myCap);

                                        }

                                        if (twainDevice.IsCapabilitySupported(Capability.IcapYResolution) == true)
                                        {
                                            myCap = new CapabilityContainerOneValueFloat(Capability.IcapYResolution);
                                            myCap.Value = (float)(Convert.ToInt32(cmbResolution.SelectedItem.ToString()));
                                            twainDevice.SetCapability(myCap);
                                        }


                                    }

                                    catch (Exception ex)
                                    {
                                        _ErrorMessage = ex.ToString();
                                        AuditLogErrorMessage(_ErrorMessage);
                                    }
                                    break;
                                }
                            }
                        }

                    }

                    if (_IsScanCard == true)
                    {
                        _ScanSide = "Duplex";
                        for (int i = 0; i < cmbScanSide.Items.Count; i++)
                        {
                            if (cmbScanSide.Items[i].ToString() == _ScanSide)
                            {
                                cmbScanSide.SelectedIndex = i;

                                if (twainDevice.IsCapabilitySupported(Capability.CapDuplexEnabled) == true)
                                {
                                    myCap = new CapabilityContainerOneValueFloat(Capability.CapDuplexEnabled);
                                    myCap.Value = 1;
                                    twainDevice.SetCapability(myCap);
                                }
                                break;
                            }
                        }
                        _IsScanCard = false;

                    }
                    else
                    {
                        object oSetting_DMSScanSide = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanSide);
                        if (oSetting_DMSScanSide != null)
                        {
                            _ScanSide = oSetting_DMSScanSide.ToString();
                            if (_ScanSide.Trim() != "")
                            {
                                for (int i = 0; i < cmbScanSide.Items.Count; i++)
                                {
                                    if (cmbScanSide.Items[i].ToString() == _ScanSide)
                                    {
                                        cmbScanSide.SelectedIndex = i;

                                        if (twainDevice.IsCapabilitySupported(Capability.CapDuplexEnabled) == true)
                                        {
                                            myCap = new CapabilityContainerOneValueFloat(Capability.CapDuplexEnabled);
                                            if (_ScanSide == "Front Side")
                                            {
                                                myCap.Value = 0;

                                            }
                                            if (_ScanSide == "Duplex")
                                            {
                                                myCap.Value = 1;


                                            }

                                            try
                                            {
                                                twainDevice.SetCapability(myCap);
                                            }
                                            catch (Exception ex)
                                            {
                                                _ErrorMessage = ex.ToString();
                                                AuditLogErrorMessage(_ErrorMessage);
                                                // MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            }

                                        }
                                        break;
                                    }
                                }
                            }

                        }
                    }

                    object oSetting_DMSScanMode = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanMode);
                    if (oSetting_DMSScanMode != null)
                    {
                        _ScanMode = oSetting_DMSScanMode.ToString();

                    }
                    object oSetting_DMSScanBrightness = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSBright);
                    if (oSetting_DMSScanBrightness != null)
                    {
                        _ScanBrightness = oSetting_DMSScanBrightness.ToString();
                    }
                    object oSetting_DMSScanContrast = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSContrast);
                    if (oSetting_DMSScanContrast != null)
                    {
                        _ScanContrast = oSetting_DMSScanContrast.ToString();
                    }
                    if (IsCardScan == false)
                    {
                        if (_ScanMode.Trim() != "")
                        {
                            for (int i = 0; i < cmbScanMode.Items.Count; i++)
                            {
                                if (cmbScanMode.Items[i].ToString() == _ScanMode)
                                {
                                    cmbScanMode.SelectedIndex = i;

                                    if (twainDevice.IsCapabilitySupported(Capability.IcapPixelType) == true)
                                    {
                                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapPixelType);
                                        if (_ScanMode == "Black & White")
                                        {
                                            myCap.Value = Single.Parse("0"); //Black & White
                                        }
                                        else if (_ScanMode == "GrayScale")
                                        {
                                            myCap.Value = Single.Parse("1"); // GrayScale
                                        }
                                        else if (_ScanMode == "Color")
                                        {
                                            myCap.Value = Single.Parse("2");
                                        }
                                        try
                                        {
                                            twainDevice.SetCapability(myCap);
                                        }
                                        catch (Exception ex)
                                        {
                                            _ErrorMessage = ex.ToString();
                                            AuditLogErrorMessage(_ErrorMessage);

                                        }

                                    }

                                    //} 
                                    break;
                                }
                            }


                        }


                        if (_ScanBrightness.Trim() != "")
                        {
                            for (int i = 0; i < cmbBrightness.Items.Count; i++)
                            {
                                if (cmbBrightness.Items[i].ToString() == _ScanBrightness)
                                {
                                    cmbBrightness.SelectedIndex = i;

                                    if (twainDevice.IsCapabilitySupported(Capability.IcapBrightness) == true)
                                    {
                                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapBrightness);
                                        if (_ScanMode == "Black & White")
                                        {
                                            myCap.Value = (float)(96.0);
                                        }
                                        else if (_ScanMode == "GrayScale")
                                        {
                                            myCap.Value = (float)(96.0);
                                        }
                                        else if (_ScanMode == "Color")
                                        {
                                            myCap.Value = (float)(124.00);
                                        }
                                        //twainDevice.SetCapability(myCap);
                                        try
                                        {
                                            twainDevice.SetCapability(myCap);
                                        }
                                        catch (Exception ex)
                                        {
                                            _ErrorMessage = ex.ToString();
                                            AuditLogErrorMessage(_ErrorMessage);


                                        }
                                        //break;
                                    }
                                    break;
                                }
                            }


                        }



                        if (_ScanContrast.Trim() != "")
                        {
                            for (int i = 0; i < cmbContrast.Items.Count; i++)
                            {
                                if (cmbContrast.Items[i].ToString() == _ScanContrast)
                                {
                                    cmbContrast.SelectedIndex = i;

                                    if (twainDevice.IsCapabilitySupported(Capability.IcapContrast) == true)
                                    {
                                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapContrast);
                                        if (_ScanMode == "Black & White")
                                        {
                                            myCap.Value = (float)(96.0);
                                        }
                                        else if (_ScanMode == "GrayScale")
                                        {
                                            myCap.Value = (float)(96.0);
                                        }
                                        else if (_ScanMode == "Color")
                                        {
                                            myCap.Value = (float)(124.0);
                                        }

                                        try
                                        {
                                            twainDevice.SetCapability(myCap);
                                        }
                                        catch (Exception ex)
                                        {
                                            _ErrorMessage = ex.ToString();
                                            AuditLogErrorMessage(_ErrorMessage);
                                        }

                                    }
                                    break;
                                }
                            }


                        }

                        //object ooSetting_DMSScanMode = regKey.GetValue(sSetting_DMSScanMode);
                        if (oSetting_DMSScanMode != null)
                        {
                            if (oSetting_DMSScanMode.ToString() != "")
                            {
                                if (_ScanMode.Trim() != "")
                                {

                                    if (twainDevice.IsCapabilitySupported(Capability.IcapBitDepth) == true)
                                    {
                                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapBitDepth);
                                        if (_ScanMode == "Black & White")
                                        {
                                            myCap.Value = (float)1.0;
                                        }
                                        else if (_ScanMode == "GrayScale")
                                        {
                                            myCap.Value = (float)8.0;
                                        }
                                        else if (_ScanMode == "Color")
                                        {
                                            myCap.Value = (float)24.0;
                                        }
                                        try
                                        {
                                            twainDevice.SetCapability(myCap);
                                        }
                                        catch (Exception ex)
                                        {
                                            _ErrorMessage = ex.ToString();
                                            AuditLogErrorMessage(_ErrorMessage);
                                            //myCap.Value = (float)8.0;
                                            //twainDevice.SetCapability(myCap); 
                                            try
                                            {
                                                myCap.Value = (float)8.0;
                                                twainDevice.SetCapability(myCap);
                                            }
                                            catch (Exception xx)
                                            {
                                                _ErrorMessage = xx.ToString();
                                                AuditLogErrorMessage(_ErrorMessage);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    //    bool _isCapBitWith = false;
                        //--
                        if (twainDevice.IsCapabilitySupported(Capability.IcapPixelType) == true)
                        {
                            myCap = new CapabilityContainerOneValueFloat(Capability.IcapPixelType);
                            if (_ScanMode == "Black & White")
                            {
                                myCap.Value = Single.Parse("0"); //Black & White
                            }
                            else if (_ScanMode == "GrayScale")
                            {
                                myCap.Value = Single.Parse("1"); // GrayScale
                            }
                            else if (_ScanMode == "Color")
                            {
                                myCap.Value = Single.Parse("2");
                            }
                            try
                            {
                                twainDevice.SetCapability(myCap);
                            }
                            catch (Exception ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);

                            }

                        }

                      
                        if (twainDevice.IsCapabilitySupported(Capability.IcapBitDepth) == true)
                        {
                            myCap = new CapabilityContainerOneValueFloat(Capability.IcapBitDepth);
                            if (_ScanMode == "Black & White")
                            {
                                myCap.Value = (float)1.0;
                            }
                            else if (_ScanMode == "GrayScale")
                            {
                                myCap.Value = (float)8.0;
                            }
                            else if (_ScanMode == "Color")
                            {
                                myCap.Value = (float)24.0;
                            }
                            try
                            {
                                twainDevice.SetCapability(myCap);
                            }
                            catch (Exception ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);
                               
                                try
                                {
                                    myCap.Value = (float)8.0;
                                    twainDevice.SetCapability(myCap);
                                }
                                catch (Exception xx)
                                {
                                    _ErrorMessage = xx.ToString();
                                    AuditLogErrorMessage(_ErrorMessage);
                                }
                            }

                        }

                    }

                   
                    if (twainDevice.IsCapabilitySupported(Capability.IcapBrightness) == true)
                    {
                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapBrightness);
                        if (_ScanMode == "Black & White")
                        {
                            myCap.Value = (float)(-32.0);
                        }
                        else if (_ScanMode == "GrayScale")
                        {
                            myCap.Value = (float)(96.0);
                        }
                        else if (_ScanMode == "Color")
                        {
                            myCap.Value = (float)(-32.00);
                        }
                        try
                        {
                            twainDevice.SetCapability(myCap);
                        }
                        catch (Exception ex)
                        {
                            _ErrorMessage = ex.ToString();
                            AuditLogErrorMessage(_ErrorMessage);
                        }

                        CapabilityContainer cap = twainDevice.GetCapability(Capability.IcapBrightness);
                        //break;
                    }
                   
                    if (twainDevice.IsCapabilitySupported(Capability.IcapContrast) == true)
                    {
                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapContrast);
                        if (_ScanMode == "Black & White")
                        {
                            myCap.Value = (float)(-32.0);
                        }
                        else if (_ScanMode == "GrayScale")
                        {
                            myCap.Value = (float)(96.0);
                        }
                        else if (_ScanMode == "Color")
                        {
                            //myCap.Value = (float)(124.0);
                            myCap.Value = (float)(-32.0);
                        }

                        try
                        {
                            twainDevice.SetCapability(myCap);
                        }
                        catch (Exception ex)
                        {
                            _ErrorMessage = ex.ToString();
                            AuditLogErrorMessage(_ErrorMessage);
                        }
                        CapabilityContainer cap = twainDevice.GetCapability(Capability.IcapContrast);
                    }


                    chkShowScannerDialog.Checked = false;
                    object oSetting_DMSShowScanner = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSShowScann);
                    if (oSetting_DMSShowScanner != null)
                    {
                        bool _ScanInterface = Convert.ToBoolean(oSetting_DMSShowScanner);
                        if (_ScanInterface == true)
                        {
                            chkShowScannerDialog.Checked = true;
                        }
                        else
                        {
                            chkShowScannerDialog.Checked = false;
                        }
                        twainDevice.ShowUserInterface = chkShowScannerDialog.Checked;

                    }
                    //}else
                }
                else
                {
                    MessageBox.Show("Scanner settings not loaded, you can use default settings.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
              
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                {
                    gloRegistrySetting.CloseRegistryKey();
                }
            }

            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion "Dhruv 20100621 -> Application Settings"


        #region "Dhruv 20100621-> Save the ScannerSetting"


        private void SaveScannerSettings()
        {

            try
            {
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                {
                    return;
                }

                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScan, cmbScanner.Text);
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSResol, cmbResolution.Text);
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSBright, cmbBrightness.Text);
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSContrast, cmbContrast.Text);
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanMode, cmbScanMode.Text);
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanSide, cmbScanSide.Text);
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSShowScann, chkShowScannerDialog.Checked);
                //Sandip Darade   20090926
                // Add card size setting 
                gloRegistrySetting.SetRegistryValue(sSetting_CardWidth, txtCardWidth.Text.Trim());
                gloRegistrySetting.SetRegistryValue(sSetting_CardLength, txtCardLength.Text.Trim());

                //Dhruv -> Closed
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                {
                    gloRegistrySetting.CloseRegistryKey();
                }


            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //declared -> Dhruv
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                {
                    gloRegistrySetting.CloseRegistryKey();
                }
            }
        }

        #endregion "Dhruv 20100621-> Save the ScannerSetting"

        C1.Win.C1FlexGrid.CellStyle ostyle_Document;
        bool bIsNewDocument = false;
        int icnt = 0;

        void twainDevice_Scanned(object sender, ScannedEventArgs e)
        {
           // AuditLogErrorMessage("In twainDevice_Scanned : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            try
            {
                {
                    icnt += 1;
                    Console.WriteLine("twainDevice_Scanned : " + icnt);

                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                    {
                        if (c1Documents.Styles.Contains("style_Document"))
                        {
                            ostyle_Document = c1Documents.Styles["style_Document"];
                        }
                        else
                        {
                            ostyle_Document = c1Documents.Styles.Add("style_Document");
                            ostyle_Document.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            ostyle_Document.ForeColor = System.Drawing.Color.DarkBlue;
                            ostyle_Document.BackColor = System.Drawing.Color.FromArgb(232, 237, 243);
                            
                        }

                        int BarCodeCnt = 0;
                        ArrayList barcodes = new ArrayList();
                        Int32 iScans = 100;
                        BarcodeImaging.UseBarcodeZones = false;
                        BarcodeImaging.FullScanBarcodeTypes = BarcodeImaging.BarcodeType.Code39;
                       // AuditLogErrorMessage("Sending to chk barcode : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                        BarcodeImaging.FullScanPage(ref barcodes, e.ScannedImage.ToBitmap(), iScans);
                      //  AuditLogErrorMessage("After checking barcode : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                        BarCodeCnt = barcodes.Count;

                        if (BarCodeCnt > 0)
                        {
                            Console.WriteLine("BarCode found on page :  " + icnt + "   with count : " + BarCodeCnt + "   value : " + barcodes[0].ToString());
                        }

                        if (BarCodeCnt > 0 && barcodes[0].ToString().ToUpper() == "RCM")//&& barcodes[0].ToString().ToUpper() == "DOCUMENT SEPERATOR"
                        {
                            bIsNewDocument = true;
                            return;
                        }
                        else
                        {
                            if (c1Documents.Rows.Count <= 0 || bIsNewDocument)
                            {
                                c1Documents.Rows.Add();
                                c1Documents.SetData(c1Documents.Rows.Count - 1, COL_NAME, eDocManager.eDocValidator.GetNewDocumentName(oPatientID, oScanInCategory, oClinicID, _OpenExternalSource));
                                c1Documents.SetData(c1Documents.Rows.Count - 1, COL_PATH, "");
                                c1Documents.SetCellStyle(c1Documents.Rows.Count - 1, COL_NAME, ostyle_Document);

                                bIsNewDocument = false;
                            }

                        }
                    }

                    _nImageCount = _nImageCount + 1;
                    int _rowCount = c1Documents.Rows.Count + 1;

                    //To increment the count each time when image gets scanned
                    nCountImageNo++;
                    //Call the function to count unique loaded or scanned image no
                    int _Count = imageCount();
                    //End code add

                    string _ImageFilePath = "";
                    //GLO2011-0012086- (Placed the or condition for pixeltype as palette)When attempting to scan a card with card location left and top value of 22.001 nothing happens. At a value of 0 for top and left the scan occurs but displays a message 'Error writing file' after scan finishes. 
                    if (e.ScannedImage.ScannedImageData.PixelType == PixelType.TwptGray || e.ScannedImage.ScannedImageData.PixelType == PixelType.TwptRgb || e.ScannedImage.ScannedImageData.PixelType == PixelType.TwptPalette)
                    {

                        //Added By Shweta 20090926
                        _ImageFilePath = _ScanImagesTempFolderPath + "\\Image - " + _Count.ToString("000#") + ".jpg";
                        //End code add

                        _SaveOption.Format = ScannedImageFormat.Jpeg;
                        _SaveOption.Jpeg.Chrominance = 60;// 30;
                        _SaveOption.Jpeg.Luminance = 48;// 24;
                    }
                    else
                    {

                        //Added By Shweta 20090926
                        _ImageFilePath = _ScanImagesTempFolderPath + "\\Image - " + _Count.ToString("000#") + ".TIF";
                        //End code add

                        _SaveOption.Format = ScannedImageFormat.Tiff;
                        _SaveOption.Tiff.Compression = TiffCompression.Rle;
                    }

                    e.ScannedImage.SaveFile(_ImageFilePath, _SaveOption);
                   // AuditLogErrorMessage("Before Adding image to grid : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                    AddImageFileIntoGrid("Image - " + _Count.ToString("000#"), _ImageFilePath);
                    //End code add
                   // AuditLogErrorMessage("After Adding image to grid : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                    Zoom();//For Intiallially Zoom to the Best Fit
                    Application.DoEvents();

                }
            }
            catch (Exception ex)
            {
                
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DesignGrid()
        {
            c1Documents.Rows.Count = 0;
            c1Documents.Rows.Fixed = 0;
            c1Documents.Cols.Count = 2;
            c1Documents.Cols.Fixed = 0;

            c1Documents.Cols[COL_NAME].Visible = true;
            c1Documents.Cols[COL_PATH].Visible = false;

            c1Documents.Cols[COL_NAME].AllowEditing = false;
            c1Documents.Cols[COL_PATH].AllowEditing = false;

            c1Documents.Cols[COL_NAME].Width = c1Documents.Width - 20;
            c1Documents.Cols[COL_PATH].Width = 0;
        }

        private void AddImageFileIntoGridForRemoteScan(string FileName, string FilePath)
        {
            _IsFormLoading = true;

            if (IsValidDPIFile(FilePath) == true)
            {
                c1Documents.Rows.Add();
                c1Documents.SetData(c1Documents.Rows.Count - 1, COL_NAME, FileName);
                c1Documents.SetData(c1Documents.Rows.Count - 1, COL_PATH, FilePath);

                //if (c1Documents.Rows.Count > 0)
                //{
                //    if (_ScanningInProgress == false)
                //    {
                //        tlb_Remove.Enabled = true;
                //        tlb_RemoveAll.Enabled = true;
                //        tlb_RotateBack.Enabled = true;
                //        tlb_RotateForward.Enabled = true;
                //    }
                //}
            }
            else
            {
                MessageBox.Show("Only " + gloEDocV3Admin.gImageMaxDPI.ToString() + " and less DPI image is allowed.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (File.Exists(FilePath) == true)
                {
                    File.Delete(FilePath);
                }

            }
            _IsFormLoading = false;
            //ZoomFitButtons();
        }

        private void AddImageFileIntoGrid(string FileName, string FilePath)
        {
            _IsFormLoading = true;

            if (IsValidDPIFile(FilePath) == true)
            {
                c1Documents.Rows.Add();
                c1Documents.SetData(c1Documents.Rows.Count - 1, COL_NAME, FileName);
                c1Documents.SetData(c1Documents.Rows.Count - 1, COL_PATH, FilePath);

                if (c1Documents.Rows.Count > 0)
                {
                    if (_ScanningInProgress == false)
                    {
                        tlb_Remove.Enabled = true;
                        tlb_RemoveAll.Enabled = true;
                        tlb_RotateBack.Enabled = true;
                        tlb_RotateForward.Enabled = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Only " + gloEDocV3Admin.gImageMaxDPI.ToString() + " and less DPI image is allowed.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (File.Exists(FilePath) == true)
                {
                    File.Delete(FilePath);
                }

            }
            _IsFormLoading = false;

            //if (_OpenExternalSource != enum_OpenExternalSource.RCM)
            //{
            //    imageControl1.SetImageWithPath(FilePath);
            //}
            imageControl1._CurrZoomIndex = 9;

            //if (gloGlobal.gloEliminatePegasus.bEliminatePegasus)
            //{ imageControl1.SetImageWithPath(FilePath); }
            //else
            //{ ZoomFitButtons(); }
        }



        #region "Dhruv 20100621 -> C1Document-AfterRowColumnChanged"
       
        private void c1Documents_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if (_IsFormLoading == false)
                {
                    if (c1Documents != null)
                    {
                        if (c1Documents.Rows.Count > 0)
                        {
                            if (c1Documents.RowSel >= 0)
                            {
                                string _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                if (_filePath != "")
                                {
                                    if (File.Exists(_filePath) == true)
                                    {
                                        //if (imageControl1._CurrZoomIndex >= 0)
                                        //{
                                        //    cmbZoom.SelectedIndex = imageControl1._CurrZoomIndex;
                                        //}
                                        //LoadImage(_filePath);
                                        //Zoom();
                                        LoadImageWithoutFlickering(_filePath);

                                    }
                                }
                                else
                                {
                                    UnloadImage();
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
        }
        #endregion "Dhruv 20100621 -> C1Document-AfterRowColumnChanged"


        private void LoadImageWithoutFlickering(string _filePath)
        {
            int oldSelectedIndex = cmbZoom.SelectedIndex;

            imageControl1.UpdateScreenOfControl(false);

            if (oldSelectedIndex >= 0)
            {
                LoadImage(_filePath, oldSelectedIndex);
            }
            else
            {
                LoadImage(_filePath);
                if (imageControl1._CurrZoomIndex >= 0)
                {
                    cmbZoom.SelectedIndex = imageControl1._CurrZoomIndex;
                }
            }
            // LoadImage(_filePath);
            Zoom();
            imageControl1.UpdateScreenOfControl(true);
        }


        private void c1Documents_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    C1.Win.C1FlexGrid.HitTestInfo oHit =  c1Documents.HitTest(e.X, e.Y);
                    if (oHit.Row > -1)
                    {
                        c1Documents.Select(oHit.Row, 0);
                    }

                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }

        }



        #region "Dhruv 20100621 -> IsValidDPIFile"
        private bool FreeBigImageResources(ref BitmapImage big)
        {

            if (big != null)
            {


                try
                {
                    big.StreamSource.Dispose();

                }
                catch
                {
                }

                big.UriSource = null;

                try
                {
                    big.StreamSource.Dispose();

                }
                catch
                {
                }


                try
                {
                    big.BeginInit();
                    big.UriSource = null;
                    big.EndInit();
                }
                catch
                {
                }
                try
                {
                    big.StreamSource.Dispose();

                }
                catch
                {
                }

                //08-May-13 Aniket: Resolving Memory Leaks
                //big = New BitmapImage()
                //big.UriSource = Nothing

                big = null;
                return true;

            }
            else
            {
                return false;
            }

        }

        private bool IsValidDPIFile(string ImageFilePath)
        {
            if (!gloGlobal.clsMISC.WaitForFileToBeReady(ImageFilePath, 100, 200))
            { return false; }// bFileReady = true;

            if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan || gloGlobal.gloEliminatePegasus.bEliminatePegasus)
            { return true; }

            //Start/New Code for Validation
            BitmapImage big = new BitmapImage();
            double orgHScale = 0;
            double orgVScale = 0;

            try
            {
                big.BeginInit();

                big.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                big.CacheOption = BitmapCacheOption.OnLoad;

                big.UriSource = new Uri(ImageFilePath, UriKind.RelativeOrAbsolute);
                big.EndInit();


                orgHScale = big.DpiX;
                orgVScale = big.DpiY;
            }
            catch
            {
            }

            FreeBigImageResources(ref big);

            //End/New Code for Validation

            bool _result = false;
            System.Drawing.Bitmap bmp = null;
            try
            {
                //--
                if (ImageFilePath != "")
                {
                    if (System.IO.File.Exists(ImageFilePath) == true)
                    {
                        if (bmp != null)
                        {
                            bmp.Dispose();
                            bmp = null;
                        }
                        try
                        {
                            bmp = new System.Drawing.Bitmap(ImageFilePath);
                        }
                        catch (Exception ex)
                        {
                            if (bmp != null)
                            {
                                bmp.Dispose();
                                bmp = null;
                            }
                            _ErrorMessage = ex.ToString();
                            AuditLogErrorMessage(_ErrorMessage);
                            _result = true;
                            return _result;
                        }
                        if (bmp != null)
                        {
                            try
                            {

                                float _resHor = 0;
                                float _resVer = 0;

                                _resHor = bmp.HorizontalResolution;
                                _resVer = bmp.VerticalResolution;

                                if (_resHor <= gloEDocV3Admin.gImageMaxDPI && _resVer <= gloEDocV3Admin.gImageMaxDPI)
                                {
                                    _result = true;
                                }
                                else
                                {
                                    if (bmp != null)
                                    {
                                        bmp.Dispose();
                                        bmp = null;
                                    }

                                    System.IO.FileInfo oFileInfo = new FileInfo(ImageFilePath);
                                    oFileInfo.IsReadOnly = false;
                                    oFileInfo.Delete();

                                    _result = false;
                                }

                            }


                            catch (Exception ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);
                                _result = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                    bmp = null;
                }
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                _result = false;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                    bmp = null;
                    System.IO.FileInfo oFileInfo = null;
                    if (ImageFilePath != "")
                    {
                        oFileInfo = new FileInfo(ImageFilePath);

                        if (oFileInfo != null)
                        {
                            oFileInfo.IsReadOnly = false;
                        }
                    }
                }
            }
            return _result;
        }
        #endregion "Dhruv 20100621 -> IsValidDPIFile"



        #region " Toolstrip Button Events "

        private void ts_SmallStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            btn_Right_Click(null, null);
        }



        #region "Dhruv 20100621 -> ScanClick"
        
        private void tlb_Scan_Click(object sender, EventArgs e)
        {
            PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat myCap = null;
            bool _isScannerSession = false;
            try
            {
                _ScanningInProgress = true;
                tlb_Ok.Enabled = false;
                tlb_Cancel.Enabled = false;
                tlb_Settings.Enabled = false;
                tls_btnScanCard.Enabled = false;
                tlb_LoadImages.Enabled = false;
                LoadScannerSettings();

                if (cmbScanner.SelectedIndex < 0 || cmbScanMode.SelectedIndex < 0)
                {
                    SettingClick();
                    return;

                }

                SelectScanner();
                twainDevice.OpenSession();
                _isScannerSession = true;
                #region " Set Full Page Size "

                if (twainDevice.IsCapabilitySupported(Capability.IcapSupportedSizes) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(PegasusImaging.WinForms.TwainPro5.Capability.IcapSupportedSizes);
                    myCap.Value = (float)3.0;
                    twainDevice.SetCapability(myCap);
                }
                if (twainDevice.IsCapabilitySupported(Capability.IcapUnits) == true)
                {

                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                    myCap.Value = (float)0; //Inches
                    twainDevice.SetCapability(myCap);
                }
                twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0f, 8.5f, 11f);//Card Size

                #endregion

                ApplySettings(false);


                if (_IsScannerConnected == true)
                {
                    twainDevice.StartSession();
                    #region " Load File "

                    if (c1Documents != null)
                    {
                        if (c1Documents.RowSel >= 0)
                        {
                            if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
                            {
                                string _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                if (File.Exists(_filePath) == true)
                                {
                                    //LoadImage(_filePath);
                                    LoadImageWithoutFlickering(_filePath);
                                }
                            }
                        }

                    }

                    #endregion

                    if (twainDevice != null)
                    {
                        twainDevice.CloseSession();
                        
                    }
                    _isScannerSession = false;


                }
                else
                {
                    MessageBox.Show("Scanner is not connected.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (PegasusImaging.WinForms.TwainPro5.TwainProException ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);

            }
            catch (System.Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _ScanningInProgress = false;
                if (c1Documents.Rows.Count > 0)
                {
                    tlb_Remove.Enabled = true;
                    tlb_RemoveAll.Enabled = true;
                    tlb_RotateBack.Enabled = true;
                    tlb_RotateForward.Enabled = true;
                }
                tls_btnScanCard.Enabled = true;
                tlb_Ok.Enabled = true;
                tlb_LoadImages.Enabled = true;
                tlb_Cancel.Enabled = true;
                tlb_Settings.Enabled = true;
                if (_isScannerSession == true)
                {
                    if (twainDevice != null)
                    {
                        twainDevice.CloseSession();
                      
                    }
                }
            }
        }

        #endregion

        #region "Dhruv 20100621 -> ScanHalfClick"
       
        private void tls_btnScanHalf_Click(object sender, EventArgs e)
        {
            PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat myCap = null;// new CapabilityContainerOneValueFloat(Capability.IcapUnits);
            bool _isScannerSession = false;

            try
            {
                _ScanningInProgress = true;
                tlb_Scan.Enabled = false;
                tlb_Ok.Enabled = false;
                tlb_Cancel.Enabled = false;
                tlb_Settings.Enabled = false;
                tls_btnScanCard.Enabled = false;

                tlb_LoadImages.Enabled = false;
                LoadScannerSettings();
                if (cmbScanner.SelectedIndex < 0 || cmbScanMode.SelectedIndex < 0)
                {
                    SettingClick();
                    return;
                }


                SelectScanner();
                twainDevice.OpenSession();
                _isScannerSession = true;

                #region " Set Half Page Size "

                if (twainDevice.IsCapabilitySupported(Capability.IcapSupportedSizes) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapSupportedSizes);
                    myCap.Value = (float)4.0;
                    twainDevice.SetCapability(myCap);
                }
                if (twainDevice.IsCapabilitySupported(Capability.IcapUnits) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                    myCap.Value = (float)0; //Inches
                    twainDevice.SetCapability(myCap);
                }
                twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0f, 8.25f, 5.7f);//Half Size
                #endregion

                ApplySettings(false);

                if (_IsScannerConnected == true)
                {
                    twainDevice.StartSession();
                    #region " Load File "

                    if (c1Documents != null)
                    {
                        if (c1Documents.RowSel >= 0)
                        {
                            if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
                            {
                                string _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                if (File.Exists(_filePath) == true)
                                {
                                    //LoadImage(_filePath);
                                    LoadImageWithoutFlickering(_filePath);
                                }
                            }
                        }
                    }

                    #endregion
                    if (twainDevice != null)
                    {
                        twainDevice.CloseSession();
                       
                    }
                    _isScannerSession = false;

                }
                else
                {

                    _ErrorMessage = "Scanner is not connected.";
                    AuditLogErrorMessage(_ErrorMessage);
                    MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (PegasusImaging.WinForms.TwainPro5.TwainProException ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _ScanningInProgress = false;
                if (c1Documents.Rows.Count > 0)
                {
                    tlb_Remove.Enabled = true;
                    tlb_RemoveAll.Enabled = true;
                    tlb_RotateBack.Enabled = true;
                    tlb_RotateForward.Enabled = true;
                }

                tls_btnScanCard.Enabled = true;
                tlb_Scan.Enabled = true;
                tlb_Ok.Enabled = true;
                tlb_LoadImages.Enabled = true;
                tlb_Cancel.Enabled = true;
                tlb_Settings.Enabled = true;
                if (_isScannerSession == true)
                {
                    twainDevice.CloseSession();
                 
                }
            }
        }
        #endregion


        #region "Dhruv -> New form Setting"
       
        private void tlb_Settings_Click(object sender, EventArgs e)
        {
            SettingClick();
        }
        private void SettingClick()
        {
            frm_EDocEvent_ScannerSettings ofrm = null;
            try
            {
                //if (gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                //{
                //    ofrm = new frm_EDocEvent_ScannerSettings();
                //}
                //else
                //{
                    ofrm = new frm_EDocEvent_ScannerSettings(twainDevice,twainPro1);
                //}
                
                if (ofrm != null)
                {
                    ofrm.ShowDialog(this);

                    if (!gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                    {
                        InitPagasusTwainDevice(true);
                    }
                }

            }
            catch (Exception ex)
            {
                if (ofrm != null)
                {
                    ofrm.Dispose();
                    ofrm = null;
                }
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (ofrm != null)
                {
                    ofrm.Dispose();
                    ofrm = null;
                }
            }
        }

        private void InitPagasusTwainDevice(bool bInitTwain=false)
        {
            try
            {
                //if (!gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                //{
                PagasusLicencing();

                if (bInitTwain)
                {
                    twainDevice.Scanned -= new ScannedEventHandler(twainDevice_Scanned);
                    DisposingTwain();
                    _SaveOption = null;
                }
                if (twainDevice == null)
                {

                    twainDevice = new TwainDevice(twainPro1);
                    twainDevice.Scanned += new ScannedEventHandler(twainDevice_Scanned);

                    twainDevice.EnableExtendedCapabilities = true;
                    twainDevice.EnableExtendedCapabilities = true;
                }
                if (_SaveOption == null)
                {
                    _SaveOption = new SaveOptions();
                }
                //}
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion "Dhruv -> New form Setting"


        #endregion

        private void tlb_SaveSetting_Click(object sender, EventArgs e)
        {
            if (txtCardWidth.Text.Trim() != "")
            {
                if (!isClosedClick)
                {
                    float _CardWidth;
                    if (txtCardWidth.Text.Trim() != "")
                    {
                        try
                        {
                            _CardWidth = (float)Convert.ToDouble(txtCardWidth.Text.Trim());
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Card Width is invalid", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCardWidth.Focus();
                            return;
                        }
                        if (_CardWidth < (float)2.1)
                        {
                            MessageBox.Show("Card Width must be greater than 2.0 Inches", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCardWidth.Focus();
                            return;
                        }
                    }
                }
               
            }
            else
            {
                MessageBox.Show("Please enter card width.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCardWidth.Focus();
                return;
            }

            if (txtCardLength.Text.Trim() != "")
            {
                //code added by dipak to fix 4437 :-Default card size to 4"x4" Comment no :12 It given error when we click on card size
                if (!isClosedClick)
                {
                    float _CardLength;
                    if (txtCardLength.Text.Trim() != "")
                    {
                        try
                        {

                            _CardLength = (float)Convert.ToDouble(txtCardLength.Text.Trim());
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Card Length is invalid", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCardLength.Focus();
                            return;
                        }
                        if (_CardLength < (float)2.2)
                        {
                            MessageBox.Show("Card Length must be greater than 2.1 Inches", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCardLength.Focus();
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter card Length.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCardLength.Focus();
                return;
            }
            tlb_Ok.Visible = true;
            tlb_Cancel.Visible = true;
            // tlb_Scan.Visible = true;
            tlb_Remove.Visible = true;
            tlb_RemoveAll.Visible = true;
            tlb_LoadImages.Visible = true;
            tlb_RotateForward.Visible = true;
            tlb_RotateBack.Visible = true;
            tlb_Settings.Visible = true;
            tls_btnScanCard.Visible = true;
            //tls_btnScanHalf.Visible = true;

            tlb_SaveSetting.Visible = false;
            tlb_CloseSetting.Visible = false;

            //Sandip Darade 20100217
            //Case  0002329
            tlb_BWFront.Visible = true;
            tlb_BWDuplex.Visible = true;
            tlb_ColorFront.Visible = true;
            tlb_ColorDuplex.Visible = true;
            tlb_GrayFront.Visible = true;
            tlb_GrayDuplex.Visible = true;

            pnlSetting.Visible = false;
            pnlScanDocument.Visible = true;
            SaveScannerSettings();
            LoadScannerSettings();

            this.Width = 1128;
            this.Height = 826;
            Point pt = new Point();
            pt.X = this.Location.X - 300;
            pt.Y = this.Location.Y - 200;
            this.Location = pt;
        }

        private void tlb_CloseSetting_Click(object sender, EventArgs e)
        {
            isClosedClick = true;
            tlb_Ok.Visible = true;
            tlb_Cancel.Visible = true;
            //tlb_Scan.Visible = true;
            tlb_Remove.Visible = true;
            tlb_RemoveAll.Visible = true;
            tlb_LoadImages.Visible = true;
            tlb_RotateForward.Visible = true;
            tlb_RotateBack.Visible = true;
            tlb_Settings.Visible = true;
            tls_btnScanCard.Visible = true;
            //  tls_btnScanHalf.Visible = true;

            tlb_SaveSetting.Visible = false;
            tlb_CloseSetting.Visible = false;



            //Sandip Darade 20100217
            //Case  0002329
            tlb_BWFront.Visible = true;
            tlb_BWDuplex.Visible = true;
            tlb_ColorFront.Visible = true;
            tlb_ColorDuplex.Visible = true;
            tlb_GrayFront.Visible = true;
            tlb_GrayDuplex.Visible = true;
            pnlSetting.Visible = false;
            pnlScanDocument.Visible = true;
            //Sandip Darade   20090926
            // Add card size setting 
            //line isClosedClick = true commented and moved to top of the function  by dipak 20091118
            //isClosedClick = true ;
            LoadScannerSettings();
            //code added by dipak to solve problem of : click setting->close->and again click on setting-> :=after following sequence validation on leave event no perform beacause  isClosedClick = true 
            //for that make it false again.
            isClosedClick = false;

            this.Width = 1128;
            this.Height = 826;
            Point pt = new Point();
            pt.X = this.Location.X - 300;
            pt.Y = this.Location.Y - 200;
            this.Location = pt;
        }

        private void btn_Left_Click(object sender, EventArgs e)
        {
            pnlSmallStrip.Visible = true;
            splitter1.Visible = false;
            pnlDocumentNameAcquiredImages.Visible = false;
            btn_Right.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Forward;
            btn_Right.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Right_Click(object sender, EventArgs e)
        {
            pnlSmallStrip.Visible = false;
            splitter1.Visible = true;
            pnlDocumentNameAcquiredImages.Visible = true;
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               
            }
            
        }


        #region "Dhruv 20100621 -> Clicked on the SaveNClose -> Ok"

        bool _isSaveClose = false;


        private void ImportDocs(ArrayList oSourceDocuments, eDocManager.eDocManager oDocManager, string strDocName)
        {

            #region "Validation"
            if (oSourceDocuments.Count <= 0)
            {
                _ErrorMessage = "Please scan documents to save.";
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(_ErrorMessage, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(strDocName))
            {
                _ErrorMessage = "Please enter document name";
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(_ErrorMessage, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (int i = 0; i < invalidCharString.Length; i++)
            {

                bool _result = strDocName.Contains(invalidCharString.Substring(i, 1));
                if (_result == true)
                {
                    _ErrorMessage = "Document name is not in valid format. " + strDocName;
                    AuditLogErrorMessage(_ErrorMessage);
                    MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            #endregion
            this.Cursor = Cursors.WaitCursor;
            pbDocument.Minimum = 0;
            pbDocument.Maximum = 100 * oSourceDocuments.Count;
            if (bOpenedFromExam == false)
            {
                Application.DoEvents();



                #region "Send to Database"
                if (chkSplitFile.Checked)
                {
                    Application.DoEvents();
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Scan  for PDF - START");
                    oDialogResultIsOK = oDocManager.ImportImages(oPatientID, oSourceDocuments, strDocName.Trim(), oScanInCategoryID, oScanInCategory, oScanInSubCategory, oScanInYear, oScanInMonth, oClinicID, out oDialogContainerID, out oDialogDocumentID, chkUseCompression.Checked, pbDocument, _OpenExternalSource);
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Scan for PDF - FINISHED");
                    Application.DoEvents();
                }
                else
                {
                    Application.DoEvents();
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Scan for PDF - START");
                    oDialogResultIsOK = oDocManager.ImportImages(oPatientID, oSourceDocuments, strDocName.Trim(), oScanInCategoryID, oScanInCategory, oScanInSubCategory, oScanInYear, oScanInMonth, oClinicID, out oDialogContainerID, out oDialogDocumentID, chkUseCompression.Checked, pbDocument, _OpenExternalSource);
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Scan for PDF - FINISHED");
                    Application.DoEvents();
                }

                if (oDialogDocumentID > 0)
                {
                    oDialogResultIsOK = true;
                }

                if (oDialogResultIsOK == true)
                {
                    //    this.Close();
                }
                else
                {
                    MessageBox.Show("Error while sending document.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
                //pbDocument.Value = 100;
                pbDocument.Value = pbDocument.Maximum;
                this.Cursor = Cursors.Default;
            }

            else
            {
                Application.DoEvents();
                oDialogResultIsOK = true;// oDocManager.SendToEDoc(oPatientID, oScanInCategory, oScanInYear, oScanInMonth, strDocName.Text, false, null, oSourceDocuments, oClinicID, out oDialogContainerID, out oDialogDocumentID);
                oDialogScanImages = oSourceDocuments;
                Application.DoEvents();


                if (oSourceDocuments.Count > 0)
                {
                    oDialogResultIsOK = true;
                }

                if (oDialogResultIsOK == true)
                {
                    this.Close();
                }
                else
                {
                    _ErrorMessage = "Error while sending document.";
                    AuditLogErrorMessage(_ErrorMessage);
                    MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //    oDocManager.DocumentProgressEvent -= new gloEDocumentV3.eDocManager.eDocManager.DocumentProgress(oDocManager_DocumentProgressEvent);

            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Add, "RCM document(s) scanned.", oPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            else
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Add, "Document(s) scanned.", oPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
        }


        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
            {
                if (c1Documents.Rows.Count<=0)
                {
                    _ErrorMessage = "Please scan documents to save.";
                    AuditLogErrorMessage(_ErrorMessage);
                    MessageBox.Show(_ErrorMessage, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
                _isSaveClose = true;
                if (oDocManager != null)
                {
                    //  oDocManager.DocumentProgressEvent += new gloEDocumentV3.eDocManager.eDocManager.DocumentProgress(oDocManager_DocumentProgressEvent);
                    pnlProgressBar.Visible = true;

                    ArrayList oSourceDocuments = new ArrayList();
                    ArrayList oSubDocuments = new ArrayList();
                    ArrayList oSendDocuments = new ArrayList();


                    #region "Try Catch region"
                    try
                    {
                        #region "Button Show/Hide"
                        tlb_Ok.Enabled = false;
                        tlb_Cancel.Enabled = false;
                        tlb_Remove.Enabled = false;
                        tlb_RemoveAll.Enabled = false;
                        tlb_Settings.Enabled = false;
                        tlb_RotateBack.Enabled = false;
                        tlb_RotateForward.Enabled = false;
                        #endregion

                        Application.DoEvents();

                        if (string.IsNullOrEmpty(oScanInSubCategory))
                        { oScanInSubCategory = DateTime.Now.ToString("MM dd yyyy"); }

                        pbDocument.Minimum = 0;
                        pbDocument.Maximum = 100;
                        pbDocument.Value = 0;
                        Application.DoEvents();
                        #region "Document Collection"
                        System.IO.DirectoryInfo oDirectoryInfo = new System.IO.DirectoryInfo(_ScanImagesTempFolderPath);
                        //System.IO.FileInfo[] oFiles = null;

                        if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
                        {
                            System.IO.Directory.CreateDirectory(_ScanImagesTempFolderPath);

                            if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
                            {
                                _ErrorMessage = "Directory does not exists " + _ScanImagesTempFolderPath;
                                AuditLogErrorMessage(_ErrorMessage);
                                return;
                            }
                        }

                        int currentCnt = 0;
                        string currFilePath = null;
                        int currDocCnt = 1;
                        string sDocName = null;

                        for (int j = currentCnt; j < c1Documents.Rows.Count; j++)
                        {
                            currFilePath = c1Documents.GetData(j, COL_PATH).ToString().Trim();

                            if (currFilePath != "")
                            {
                                FileInfo oFileInfo = new FileInfo(currFilePath);
                                string strFileExtenstion = oFileInfo.Extension.ToUpper();

                                if (strFileExtenstion == ".TIF" || strFileExtenstion == ".BMP" || strFileExtenstion == ".JPG" || strFileExtenstion == ".PNG" || strFileExtenstion == ".JPEG")
                                {
                                    // System.IO.File.SetAttributes(oFile.FullName, System.IO.FileAttributes.Normal);
                                    oSourceDocuments.Add(currFilePath);
                                }
                            }
                            else
                            {
                                if (oSourceDocuments.Count > 0)
                                {
                                    if (string.IsNullOrEmpty(sDocName))
                                    {
                                        if (!string.IsNullOrEmpty(txtDocumentName.Text))
                                        {
                                            sDocName = txtDocumentName.Text;
                                        }
                                        else
                                        {
                                            sDocName = eDocManager.eDocValidator.GetNewDocumentName(oPatientID, oScanInCategory, oClinicID, _OpenExternalSource);
                                        }
                                    }

                                    ImportDocs(oSourceDocuments, oDocManager, sDocName);
                                    oSourceDocuments.Clear();
                                    currDocCnt++;
                                }

                                sDocName = c1Documents.GetData(j, COL_NAME).ToString().Trim();
                            }
                        }

                        if (oSourceDocuments.Count > 0)
                        {
                            if (string.IsNullOrEmpty(sDocName))
                            {
                                if (!string.IsNullOrEmpty(txtDocumentName.Text))
                                {
                                    sDocName = txtDocumentName.Text;
                                }
                                else
                                {
                                    sDocName = eDocManager.eDocValidator.GetNewDocumentName(oPatientID, oScanInCategory, oClinicID, _OpenExternalSource);
                                }
                            }

                            ImportDocs(oSourceDocuments, oDocManager, sDocName);
                            oSourceDocuments.Clear();
                            sDocName = null;
                        }

                        #endregion


                    }


                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                        MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (oDocManager != null)
                        {
                            oDocManager.Dispose();
                            oDocManager = null;
                        }
                        pnlProgressBar.Visible = false;
                        tlb_Ok.Enabled = true;
                        tlb_Cancel.Enabled = true;
                        tlb_Settings.Enabled = true;
                        tls_btnScanCard.Enabled = true;
                        if (c1Documents.Rows.Count > 0)
                        {
                            tlb_Remove.Enabled = true;
                            tlb_RemoveAll.Enabled = true;
                            tlb_RotateBack.Enabled = true;
                            tlb_RotateForward.Enabled = true;
                        }
                    }
                    #endregion

                }
                this.Close();
            }
            else
            {
                eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
                _isSaveClose = true;
                if (oDocManager != null)
                {
                    //  oDocManager.DocumentProgressEvent += new gloEDocumentV3.eDocManager.eDocManager.DocumentProgress(oDocManager_DocumentProgressEvent);
                    pnlProgressBar.Visible = true;

                    ArrayList oSourceDocuments = new ArrayList();
                    ArrayList oSubDocuments = new ArrayList();
                    ArrayList oSendDocuments = new ArrayList();


                    #region "Try Catch region"
                    try
                    {
                        #region "Button Show/Hide"
                        tlb_Ok.Enabled = false;
                        tlb_Cancel.Enabled = false;
                        tlb_Remove.Enabled = false;
                        tlb_RemoveAll.Enabled = false;
                        tlb_Settings.Enabled = false;
                        tlb_RotateBack.Enabled = false;
                        tlb_RotateForward.Enabled = false;
                        #endregion

                        Application.DoEvents();
                        pbDocument.Minimum = 0;
                        pbDocument.Maximum = 100;
                        pbDocument.Value = 0;
                        Application.DoEvents();
                        #region "Document Collection"
                        System.IO.DirectoryInfo oDirectoryInfo = new System.IO.DirectoryInfo(_ScanImagesTempFolderPath);
                        System.IO.FileInfo[] oFiles = null;

                        if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
                        {
                            System.IO.Directory.CreateDirectory(_ScanImagesTempFolderPath);

                            if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
                            {
                                _ErrorMessage = "Directory does not exists " + _ScanImagesTempFolderPath;
                                AuditLogErrorMessage(_ErrorMessage);
                                return;
                            }
                        }

                        oFiles = oDirectoryInfo.GetFiles();
                        if (oFiles != null)
                        {
                            foreach (System.IO.FileInfo oFile in oFiles)
                            {
                                string strFileExtenstion = oFile.Extension.ToUpper();
                                if (strFileExtenstion == ".TIF" || strFileExtenstion == ".TIFF" || strFileExtenstion == ".BMP" || strFileExtenstion == ".JPG" || strFileExtenstion == ".PNG" || strFileExtenstion == ".JPEG")
                                {
                                    System.IO.File.SetAttributes(oFile.FullName, System.IO.FileAttributes.Normal);
                                    oSourceDocuments.Add(oFile.FullName);
                                }
                            }
                        }


                        #endregion

                        #region "Validation"
                        if (oSourceDocuments.Count <= 0)
                        {
                            _ErrorMessage = "Please scan documents to save.";
                            AuditLogErrorMessage(_ErrorMessage);
                            MessageBox.Show(_ErrorMessage, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (txtDocumentName.Text.Trim() == "")
                        {
                            _ErrorMessage = "Please enter document name";
                            AuditLogErrorMessage(_ErrorMessage);
                            MessageBox.Show(_ErrorMessage, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        for (int i = 0; i < invalidCharString.Length; i++)
                        {

                            bool _result = txtDocumentName.Text.Contains(invalidCharString.Substring(i, 1));
                            if (_result == true)
                            {
                                _ErrorMessage = "Document name is not in valid format. " + txtDocumentName.Text;
                                AuditLogErrorMessage(_ErrorMessage);
                                MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        #endregion
                        this.Cursor = Cursors.WaitCursor;
                        pbDocument.Minimum = 0;
                        pbDocument.Maximum = 100 * oSourceDocuments.Count;
                        if (bOpenedFromExam == false)
                        {
                            Application.DoEvents();

                            if (string.IsNullOrEmpty(oScanInSubCategory))
                            { oScanInSubCategory = DateTime.Now.ToString("MM dd yyyy"); }

                            #region "Send to Database"
                            if (chkSplitFile.Checked)
                            {
                                Application.DoEvents();
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Scan  for PDF - START");
                                oDialogResultIsOK = oDocManager.ImportImages(oPatientID, oSourceDocuments, txtDocumentName.Text.Trim(), oScanInCategoryID, oScanInCategory, oScanInSubCategory, oScanInYear, oScanInMonth, oClinicID, out oDialogContainerID, out oDialogDocumentID, chkUseCompression.Checked, pbDocument, _OpenExternalSource,true);
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Scan for PDF - FINISHED");
                                Application.DoEvents();
                            }
                            else
                            {
                                Application.DoEvents();
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Scan for PDF - START");
                                oDialogResultIsOK = oDocManager.ImportImages(oPatientID, oSourceDocuments, txtDocumentName.Text.Trim(), oScanInCategoryID, oScanInCategory, oScanInSubCategory, oScanInYear, oScanInMonth, oClinicID, out oDialogContainerID, out oDialogDocumentID, chkUseCompression.Checked, pbDocument, _OpenExternalSource,true);
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Scan for PDF - FINISHED");
                                Application.DoEvents();
                            }

                            if (oDialogDocumentID > 0)
                            {
                                oDialogResultIsOK = true;
                            }

                            if (oDialogResultIsOK == true)
                            {
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Error while sending document.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            #endregion
                            //pbDocument.Value = 100;
                            pbDocument.Value = pbDocument.Maximum;
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            Application.DoEvents();
                            oDialogResultIsOK = true;// oDocManager.SendToEDoc(oPatientID, oScanInCategory, oScanInYear, oScanInMonth, txtDocumentName.Text, false, null, oSourceDocuments, oClinicID, out oDialogContainerID, out oDialogDocumentID);
                            oDialogScanImages = oSourceDocuments;
                            Application.DoEvents();


                            if (oSourceDocuments.Count > 0)
                            {
                                oDialogResultIsOK = true;
                            }

                            if (oDialogResultIsOK == true)
                            {
                                this.Close();
                            }
                            else
                            {
                                _ErrorMessage = "Error while sending document.";
                                AuditLogErrorMessage(_ErrorMessage);
                                MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        //    oDocManager.DocumentProgressEvent -= new gloEDocumentV3.eDocManager.eDocManager.DocumentProgress(oDocManager_DocumentProgressEvent);

                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Add, "RCM document(s) scanned.", oPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Add, "Document(s) scanned.", oPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                        MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (oDocManager != null)
                        {
                            oDocManager.Dispose();
                            oDocManager = null;
                        }
                        pnlProgressBar.Visible = false;
                        tlb_Ok.Enabled = true;
                        tlb_Cancel.Enabled = true;
                        tlb_Settings.Enabled = true;
                        tls_btnScanCard.Enabled = true;
                        if (c1Documents.Rows.Count > 0)
                        {
                            tlb_Remove.Enabled = true;
                            tlb_RemoveAll.Enabled = true;
                            tlb_RotateBack.Enabled = true;
                            tlb_RotateForward.Enabled = true;
                        }
                    }
                    #endregion

                }     
            }

                     
        }
        #endregion  "Dhruv 20100621 -> Clicked on the SaveNClose -> Ok"


        #region "Dhruv 20100621 -> RemoveClick"
        
        private void tlb_Remove_Click(object sender, EventArgs e)
        {
            RemoveClick();
            //cmbZoom.SelectedIndex = 11;
            Zoom();
        }
      
        private void RemoveClick()
        {
            try
            {
                if (c1Documents != null)
                {
                    if (c1Documents.Rows.Count > 0)
                    {
                        if (c1Documents.RowSel >= 0)
                        {
                            if (!string.IsNullOrEmpty(c1Documents.GetData(c1Documents.RowSel, COL_PATH).ToString()))
                            {
                                if (MessageBox.Show("Are you sure you want to remove file?", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    UnloadImage();
                                    try
                                    {
                                        string sfilePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                        if (sfilePath != "")
                                        {
                                            if (File.Exists(sfilePath) == true)
                                            {
                                                try
                                                {
                                                    imageControl1.CloseCurrentImage();
                                                    System.IO.File.Delete(sfilePath);
                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }

                                        int nC1DocumentSeletedRow = c1Documents.RowSel;
                                        c1Documents.RemoveItem(nC1DocumentSeletedRow);

                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                        {
                                            string _NextfilePath = string.Empty;
                                            string _PrevfilePath = string.Empty;


                                            _PrevfilePath = Convert.ToString(c1Documents.GetData(nC1DocumentSeletedRow - 1, COL_PATH));
                                            
                                            if (nC1DocumentSeletedRow < c1Documents.Rows.Count)
                                            {
                                                _NextfilePath = Convert.ToString(c1Documents.GetData(nC1DocumentSeletedRow, COL_PATH));
                                            }
                                            if (string.IsNullOrEmpty(_PrevfilePath) && string.IsNullOrEmpty(_NextfilePath))
                                            {
                                                nC1DocumentSeletedRow = nC1DocumentSeletedRow - 1;
                                                c1Documents.RemoveItem(nC1DocumentSeletedRow);
                                                
                                            }

                                        }

                                        if (nC1DocumentSeletedRow < c1Documents.Rows.Count)
                                        {
                                            c1Documents.RowSel = nC1DocumentSeletedRow;
                                        }
                                        else
                                        {
                                            c1Documents.RowSel = c1Documents.Rows.Count - 1;
                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        _ErrorMessage = ex.ToString();
                                        AuditLogErrorMessage(_ErrorMessage);
                                        MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    if (c1Documents.RowSel < 0)
                                    {
                                       // UnloadImage();
                                        UnloadDisplayImage();
                                    }
                                    else
                                    {
                                        if (c1Documents.Rows.Count > 0)
                                        {
                                            if (c1Documents.RowSel >= 0)
                                            {
                                                string _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                                if (_filePath != "")
                                                {
                                                    if (File.Exists(_filePath) == true)
                                                    {
                                                        //LoadImage(_filePath);
                                                        LoadImageWithoutFlickering(_filePath);
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                                if (c1Documents.Rows.Count == 0)
                                {
                                    tlb_Remove.Enabled = false;
                                    tlb_RemoveAll.Enabled = false;
                                    tlb_RotateBack.Enabled = false;
                                    tlb_RotateForward.Enabled = false;
                                  //  UnloadImage();
                                    UnloadDisplayImage();
                                    imageControl1.CurrImage = null;
                                    nCountImageNo = 0; //Set the count again after remove all the loaded or scanned image
                                    imgCnt = 0;

                                }
                            }
                            else
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {

                                    ArrayList arrDelPtrs = new ArrayList();

                                    if (MessageBox.Show("Are you sure you want to remove document?", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        
                                        int CurSelected = c1Documents.RowSel;
                                        int TotCnt = c1Documents.Rows.Count;
                                        bool bFlg = false;

                                        for (int i = CurSelected +1; i <= TotCnt - 1; i++)
                                        {
                                            try
                                            {
                                                string sfilePath = Convert.ToString(c1Documents.GetData(i, COL_PATH));

                                                if (sfilePath != "" && bFlg != true)
                                                { arrDelPtrs.Add(sfilePath); }
                                                else
                                                { bFlg = true; break; }

                                            }
                                            catch (Exception ex)
                                            {
                                                _ErrorMessage = ex.ToString();
                                                AuditLogErrorMessage(_ErrorMessage);
                                                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }

                                        c1Documents.RemoveItem(CurSelected);

                                        for (int i = 0; i <= arrDelPtrs.Count - 1; i++)
                                        {
                                            string sfilePath = Convert.ToString(arrDelPtrs[i]);
                                            if (sfilePath != "")
                                            {
                                                if (File.Exists(sfilePath) == true)
                                                {
                                                    imageControl1.CloseCurrentImage();
                                                    System.IO.File.Delete(sfilePath);

                                                    CurSelected=c1Documents.FindRow(sfilePath, 0, COL_PATH,false);
                                                    c1Documents.RemoveItem(CurSelected);
                                                }

                                            }

                                        }

                                    }

                                    if (c1Documents.Rows.Count == 0)
                                    {
                                        tlb_Remove.Enabled = false;
                                        tlb_RemoveAll.Enabled = false;
                                        tlb_RotateBack.Enabled = false;
                                        tlb_RotateForward.Enabled = false;
                                        //  UnloadImage();
                                        UnloadDisplayImage();
                                        imageControl1.CurrImage = null;
                                        nCountImageNo = 0; //Set the count again after remove all the loaded or scanned image
                                        imgCnt = 0;

                                    }

                                    


                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion "Dhruv 20100621 -> RemoveClick"


        #region "Dhruv 20100621 -> RemoveClickAll"
       
        private void tlb_RemoveAll_Click(object sender, EventArgs e)
        {
            RemoveAll();
        }
        private void RemoveAll()
        {
            try
            {
                if (c1Documents != null)
                {
                    if (c1Documents.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Are you sure you want to remove all files?", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {


                            //UnloadImage();
                            //imageXView.AutoImageDispose = true;
                            UnloadImage();
                           // imageControl1.
                            for (int i = c1Documents.Rows.Count - 1; i >= 0; i--)
                            {
                                try
                                {

                                    string sfilePath = Convert.ToString(c1Documents.GetData(i, COL_PATH));
                                    if (sfilePath != "")
                                    {
                                        if (File.Exists(sfilePath) == true)
                                        {
                                            imageControl1.CloseCurrentImage();
                                            System.IO.File.Delete(sfilePath);

                                        }
                                    }
                                    c1Documents.RemoveItem(i);
                                }
                                catch (Exception ex)
                                {
                                    _ErrorMessage = ex.ToString();
                                    AuditLogErrorMessage(_ErrorMessage);
                                    MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        if (c1Documents.Rows.Count == 0)
                        {
                            tlb_Remove.Enabled = false;
                            tlb_RemoveAll.Enabled = false;
                            tlb_RotateBack.Enabled = false;
                            tlb_RotateForward.Enabled = false;
                            //UnloadImage();
                            UnloadDisplayImage();
                            imageControl1.CurrImage = null;
                            nCountImageNo = 0;  //Set the count again after remove all the loaded or scanned image
                            //End
                            imgCnt = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion "Dhruv 20100621 -> RemoveClickAll"




        #region "Dhruv 20100621 -> LoadImageClick"
        
        //private void tlb_LoadImages_Click_Working(object sender, EventArgs e)
        //{
        //    int _rowCount;
        //    OpenFileDialog oDialog = new OpenFileDialog();
        //    if (oDialog != null)
        //    {
        //        oDialog.Multiselect = true;
        //        oDialog.Title = "Import Images";
        //        oDialog.Filter = "JPEG Image (*.JPEG,*.JPG)|*.JPEG;*.JPG|TIFF Image (*.TIFF,*.TIF)|*.TIFF;*.TIF|BITMAP Image (*.BMP)|*.BMP|PNG Image (*.PNG)|*.PNG";
        //        tlb_LoadImages.Enabled = false;
        //        try
        //        {
        //            if (oDialog.ShowDialog(this) == DialogResult.OK)
        //            {
        //                eDocTIFFManager oTiff = new eDocTIFFManager();
        //                if (oTiff != null)
        //                {
        //                    foreach (string CurFile in oDialog.FileNames)
        //                    {
        //                        System.IO.FileInfo oFile = new System.IO.FileInfo(CurFile);
        //                        if (oFile != null)
        //                        {

        //                            string sFileExtension = oFile.Extension.ToUpper();
        //                            if (IsValidExtenstionFile(sFileExtension, false) == true)
        //                            {
        //                                if (sFileExtension == ".TIF" || sFileExtension == ".TIFF")
        //                                {
        //                                    nCountImageNo = nCountImageNo + 1;

        //                                    ArrayList TiffPageList = null;//new ArrayList();

        //                                    _rowCount = c1Documents.Rows.Count + 1;
        //                                    TiffPageList = oTiff.SplitTiffImage(_ScanImagesTempFolderPath, System.Drawing.Imaging.EncoderValue.CompressionLZW, oFile.Name.ToUpper(), nCountImageNo);
        //                                    if (TiffPageList != null)
        //                                    {
        //                                        for (int i = 0; i < TiffPageList.Count; i++)
        //                                        {

        //                                            int _Count = imageCount();
        //                                            string _strfileName = TiffPageList[i].ToString();
        //                                            AddImageFileIntoGrid("ImportImage - " + _Count.ToString("000#"), _strfileName.Replace(".tif", "ImportImage - " + _Count.ToString("000#")));
        //                                            c1Documents.Refresh();
        //                                        }
        //                                    }

        //                                }
        //                                else
        //                                {
        //                                    if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
        //                                    {
        //                                        System.IO.Directory.CreateDirectory(_ScanImagesTempFolderPath);
        //                                        if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
        //                                        {
        //                                            _ErrorMessage = "Unable to create Directory" + _ScanImagesTempFolderPath;
        //                                        }

        //                                    }

        //                                    nCountImageNo = nCountImageNo + 1;
        //                                    int _Count = imageCount();

        //                                    oFile.CopyTo(_ScanImagesTempFolderPath + "\\Image - " + _Count.ToString("000#") + ".TIF", true);

        //                                    string _ImageFilePath = _ScanImagesTempFolderPath + "\\Image - " + _Count.ToString("000#") + ".TIF";
        //                                    AddImageFileIntoGrid("ImportImage - " + _Count.ToString("000#"), _ImageFilePath);
        //                                    c1Documents.Refresh();
        //                                }
        //                            }
        //                            oFile = null;
        //                            if (c1Documents.Rows.Count > 0)
        //                            {
        //                                if (c1Documents.RowSel >= 0)
        //                                {

        //                                    string _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
        //                                    if (_filePath != "")
        //                                    {
        //                                        if (File.Exists(_filePath) == true)
        //                                        {
        //                                            LoadImage(_filePath);
        //                                        }
        //                                    }

        //                                }
        //                            }
        //                        }

        //                    }
        //                }
        //                if (oTiff != null)
        //                {
        //                    oTiff.Dispose();
        //                    oTiff = null;
        //                }
        //                Zoom();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _ErrorMessage = ex.ToString();
        //            AuditLogErrorMessage(_ErrorMessage);
        //            MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }

        //        finally
        //        {
        //            tlb_LoadImages.Enabled = true;
        //            if (oDialog != null)
        //            {
        //                oDialog.Dispose();
        //                oDialog = null;
        //            }


        //        }
        //    }
        //}
         
         int _rowCount = 0;

         private void tlb_LoadImages_Click(object sender, EventArgs e)
         {

             OpenFileDialog oDialog = new OpenFileDialog();

             oDialog.Multiselect = true;
             oDialog.Title = "Import Images";
             oDialog.Filter = "JPEG Image (*.JPEG,*.JPG)|*.JPEG;*.JPG|TIFF Image (*.TIFF,*.TIF)|*.TIFF;*.TIF|BITMAP Image (*.BMP)|*.BMP|PNG Image (*.PNG)|*.PNG";
             tlb_LoadImages.Enabled = false;
             try
             {
                 if (oDialog.ShowDialog(this) == DialogResult.OK)
                 {
                     eDocTIFFManager oTiff = new eDocTIFFManager();
                     if (oTiff != null)
                     {
                         foreach (string CurFile in oDialog.FileNames)
                         {
                             System.IO.FileInfo oFile = new System.IO.FileInfo(CurFile);

                             string sFileExtension = oFile.Extension.ToUpper();
                             if (IsValidExtenstionFile(oFile.Extension, false) == true)
                             {
                                 if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                 {
                                     if (c1Documents.Rows.Count <= 0)
                                     {
                                         if (c1Documents.Styles.Contains("style_Document"))
                                         {
                                             ostyle_Document = c1Documents.Styles["style_Document"];
                                         }
                                         else
                                         {
                                             ostyle_Document = c1Documents.Styles.Add("style_Document");
                                             ostyle_Document.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;
                                             ostyle_Document.ForeColor = System.Drawing.Color.DarkBlue;
                                             ostyle_Document.BackColor = System.Drawing.Color.FromArgb(232, 237, 243);
                                         }

                                         c1Documents.Rows.Add();
                                         c1Documents.SetData(c1Documents.Rows.Count - 1, COL_NAME, eDocManager.eDocValidator.GetNewDocumentName(oPatientID, oScanInCategory, oClinicID, _OpenExternalSource));
                                         c1Documents.SetData(c1Documents.Rows.Count - 1, COL_PATH, "");
                                         c1Documents.SetCellStyle(c1Documents.Rows.Count - 1, COL_NAME, ostyle_Document);
                                     }
                                 }

                                 if (sFileExtension == ".TIF" || sFileExtension == ".TIFF")
                                 {
                                     ArrayList TiffPageList = null;//new ArrayList();
                                     TiffPageList = oTiff.SplitTiffImage(_ScanImagesTempFolderPath, System.Drawing.Imaging.EncoderValue.CompressionLZW, CurFile);
                                     for (int i = 0; i < TiffPageList.Count; i++)
                                     {
                                         int _rowCount = c1Documents.Rows.Count + 1;
                                         string _strfileName = TiffPageList[i].ToString();
                                         AddImageFileIntoGrid("ImportImage - " + _rowCount.ToString("000#"), _strfileName);
                                         c1Documents.Refresh();
                                     }
                                     TiffPageList.Clear();
                                 }
                                 else
                                 {
                                     if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
                                     {
                                         System.IO.Directory.CreateDirectory(_ScanImagesTempFolderPath);
                                         if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == false)
                                         {
                                             _ErrorMessage = "Unable to create Directory" + _ScanImagesTempFolderPath;
                                         }


                                     }
                                     //_rowCount = c1Documents.Rows.Count + 1;
                                     nCountImageNo = nCountImageNo + 1;
                                     _rowCount = imageCount();
                                     string fname = "ImportImage - " + gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") + _rowCount.ToString("000#") + oFile.Extension;
                                     string _ImageFilePath = Path.Combine(_ScanImagesTempFolderPath, fname);
                                     // fname = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                     oFile.CopyTo(_ImageFilePath, true);

                                    
                                     AddImageFileIntoGrid("ImportImage - " + _rowCount.ToString("000#"), _ImageFilePath);
                                     c1Documents.Refresh();
                                 }
                             }
                             oFile = null;
                             //if (!gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                             //{
                             //    if (c1Documents.Rows.Count > 0)
                             //    {
                             //        //if (c1Documents.RowSel != null)
                             //        {
                             //            //LoadImageFile(c1Documents.RowSel);
                             //            if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
                             //            {
                             //                string _filePath = "";
                             //                _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                             //                if (File.Exists(_filePath) == true)
                             //                {
                             //                    LoadImage(_filePath);
                             //                }
                             //            }
                             //        }
                             //    }
                             //}
                         }
                         // Sudhir 20090112 -- for initially zoom at BestFit.
                         bool zoomNotDone = true;
                         try
                         {
                             if (c1Documents.Rows.Count > 0)
                             {
                                 if (c1Documents.RowSel >= 0)
                                 {

                                     string _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                     if (_filePath != "")
                                     {
                                         if (File.Exists(_filePath) == true)
                                         {
                                             //LoadImage(_filePath);
                                             LoadImageWithoutFlickering(_filePath);
                                             zoomNotDone = false;
                                         }
                                     }

                                 }
                             }
                         }
                         catch
                         {
                         }
                         if (zoomNotDone)
                         {
                             Zoom();
                         }
                         if (oTiff != null)
                         {
                             oTiff.Dispose();
                             oTiff = null;
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 #region " Make Log Entry "

                 _ErrorMessage = ex.ToString();
                 //Code added on 7rd October 2008 By - Sagar Ghodke
                 //Make Log entry in DMSExceptionLog file for any exceptions found
                 if (_ErrorMessage.Trim() != "")
                 {
                     string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                     gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                     _MessageString = "";
                 }

                 //End Code add
                 #endregion " Make Log Entry "


                 MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             finally
             {
                 tlb_LoadImages.Enabled = true;
                 if (oDialog != null)
                 {
                     oDialog.Dispose();
                     oDialog = null;
                 }
             }
         }
        #endregion "Dhruv 20100621 -> LoadImageClick"


        #region "Dhruv 20100621 -> ScanCard click"

        private void tls_btnScanCard_Click_olfNew(object sender, EventArgs e)
        {

            PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat myCap = null;//new CapabilityContainerOneValueFloat(Capability.IcapUnits);
            //PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueString strCap = null;//new CapabilityContainerOneValueString(Capability.IcapUnits);
     //       PegasusImaging.WinForms.TwainPro5.CapabilityContainerEnum CapEnum = null;
            bool _isSessionStarted = false;


            try
            {

                _ScanningInProgress = true;
                // tlb_Scan.Enabled = false;
                tlb_Ok.Enabled = false;
                tlb_Cancel.Enabled = false;
                tlb_Settings.Enabled = false;

                tls_btnScanCard.Enabled = false;
                //tls_btnScanHalf.Enabled = false;

                tlb_BWFront.Enabled = false;
                tlb_BWDuplex.Enabled = false;
                tlb_ColorFront.Enabled = false;
                tlb_ColorDuplex.Enabled = false;
                tlb_GrayFront.Enabled = false;
                tlb_GrayDuplex.Enabled = false;


                tlb_LoadImages.Enabled = false;
                _IsScanCard = true;
                LoadScannerSettings();


                if (cmbScanner.SelectedIndex < 0 || cmbScanMode.SelectedIndex < 0)
                {

                    SettingClick();
                    return;

                }

                SelectScanner();

                twainDevice.OpenSession();

                _isSessionStarted = true;


                #region " Set Card Size "
                float _CardWidth = 4.0F;
                float _CardLength = 4.0F;
                try
                {
                    _CardWidth = (float)Convert.ToDouble(txtCardWidth.Text.Trim());
                    _CardLength = (float)Convert.ToDouble(txtCardLength.Text.Trim());
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                    MessageBox.Show("Invalid card size settings, please enter from settings. ", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }


                if (twainDevice.IsCapabilitySupported(Capability.IcapSupportedSizes) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapSupportedSizes);
                    if (_CardWidth >= 3 && _CardLength >= 2.2 && _CardWidth < 4)
                    {
                        myCap.Value = (float)13;
                        twainDevice.SetCapability(myCap);
                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                        myCap.Value = (float)0; //Inches
                        twainDevice.SetCapability(myCap);
                        if (_CardWidth <= 3.7 && _CardWidth > 3.0)
                        {
                            //twainDevice.ImageLayout = new System.Drawing.RectangleF(0.0f, 0f, 2.166f, 3.583f);//Card Size
                            twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0f, 3.8f, _CardLength);//Card Size
                        }
                        else
                        {
                            twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0f, _CardWidth, _CardLength);//Card Size
                        }
                    }
                    else if (_CardWidth < 3 && _CardLength < 4)
                    {
                        myCap.Value = (float)53;
                        twainDevice.SetCapability(myCap);
                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                        myCap.Value = (float)0; //Inches
                        twainDevice.SetCapability(myCap);
                        twainDevice.ImageLayout = new System.Drawing.RectangleF(0.0f, 0f, 2.166f, 3.583f);//Card Size
                        //twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0f, _CardWidth, _CardLength);//Card Size
                    }
                    else if (_CardWidth >= 4 && _CardLength >= 4)
                    {
                        myCap.Value = (float)13;
                        twainDevice.SetCapability(myCap);
                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                        myCap.Value = (float)0; //Inches
                        twainDevice.SetCapability(myCap);
                        //twainDevice.ImageLayout = new System.Drawing.RectangleF(0.0f, 0f, 2.166f, 3.583f);//Card Size
                        twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0f, _CardWidth, _CardLength);//Card Size
                    }
                }


                #endregion " Set Card Size "


                ApplySettings(true);


                if (_IsScannerConnected == true)
                {

                    twainDevice.StartSession();

                    #region " Load File "

                    if (c1Documents != null)
                    {
                       // if (c1Documents.RowSel != null)
                        {
                            if (c1Documents.RowSel >= 0)
                            {
                                if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
                                {
                                    string _filePath = "";
                                    _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                    if (File.Exists(_filePath) == true)
                                    {
                                        //LoadImage(_filePath);
                                        LoadImageWithoutFlickering(_filePath);
                                    }
                                }
                            }
                        }
                    }

                    #endregion
                    if (twainDevice != null)
                    {
                        twainDevice.CloseSession();
                        
                    }
                    _isSessionStarted = false;

                }
                else
                {
                    _ErrorMessage = "Scanner is not connected.";
                    AuditLogErrorMessage(_ErrorMessage);
                    MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (PegasusImaging.WinForms.TwainPro5.TwainProException ex)
            {
                if (_isSessionStarted == true)
                {
                    if (twainDevice != null)
                    {
                        twainDevice.CloseSession();
                       
                    }
                }
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);

                //MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (_isSessionStarted == true)
                {
                    if (twainDevice != null)
                    {
                        twainDevice.CloseSession();
                       
                    }
                }
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _ScanningInProgress = false;
                if (c1Documents.Rows.Count > 0)
                {
                    tlb_Remove.Enabled = true;
                    tlb_RemoveAll.Enabled = true;
                    tlb_RotateBack.Enabled = true;
                    tlb_RotateForward.Enabled = true;
                }
                tlb_Scan.Enabled = true;
                tls_btnScanCard.Enabled = true;
                
                tlb_Ok.Enabled = true;
                tlb_LoadImages.Enabled = true;
                tlb_Cancel.Enabled = true;
                tlb_Settings.Enabled = true;

                tlb_BWFront.Enabled = true;
                tlb_BWDuplex.Enabled = true;
                tlb_ColorFront.Enabled = true;
                tlb_ColorDuplex.Enabled = true;
                tlb_GrayFront.Enabled = true;
                tlb_GrayDuplex.Enabled = true;
                if (_isSessionStarted == true)
                {
                    twainDevice.CloseSession();
                    
                }
            }
        }
        #endregion

        #region "setting Shortcuts"
        //Sandip Darade  20100217
        //Case  GLO2009-0002329
        //providing setting short cuts
        private void tlb_BWFront_Click(object sender, EventArgs e)
        {
            //SaveScannerSettings("Black & White", "Front Side");
            StartScanning("Black & White", "1", "Front Side");
        }

        private void tlb_BWDuplex_Click(object sender, EventArgs e)
        {
            //SaveScannerSettings("Black & White", "Duplex");
            StartScanning("Black & White", "1", "Duplex");
        }

        private void tlb_ColorFront_Click(object sender, EventArgs e)
        {

            //SaveScannerSettings("Color", "Front Side");
            StartScanning("Color", "8", "Front Side");
        }

        private void tlb_ColorDuplex_Click(object sender, EventArgs e)
        {
            //SaveScannerSettings("Color", "Duplex");
            StartScanning("Color", "8", "Duplex");

        }

        private void tlb_GrayFront_Click(object sender, EventArgs e)
        {
            StartScanning("GRAYSCALE", "8", "Front Side");
        }

        private void tlb_GrayDuplex_Click(object sender, EventArgs e)
        {
            StartScanning("GRAYSCALE", "8", "Duplex");
        }

        #region "Dhruv -> SaveScanner Settings"

        private void SaveScannerSettings(string _ScanMode, string _ScanSide)
        {
            #region "variable Decleration"
            PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat myCap = null;
            //RegistryKey regKey = null;
            bool _isAnswer = false;
            bool _isOpenSeeion = false;
            #endregion


            try
            {
                _ScanningInProgress = true;
                // tlb_Scan.Enabled = false;
                tlb_Ok.Enabled = false;
                tlb_Cancel.Enabled = false;
                tlb_Settings.Enabled = false;

                tls_btnScanCard.Enabled = false;
                //tls_btnScanHalf.Enabled = false;

                tlb_BWFront.Enabled = false;
                tlb_BWDuplex.Enabled = false;
                tlb_ColorFront.Enabled = false;
                tlb_ColorDuplex.Enabled = false;
                tlb_GrayFront.Enabled = false;
                tlb_GrayDuplex.Enabled = false;


                tlb_LoadImages.Enabled = false;
                LoadScannerSettings();
                /////

                #region "Check for the selected scanner"
                if (cmbScanner.SelectedIndex < 0 || cmbScanMode.SelectedIndex < 0)
                {
                    SettingClick();
                    return;
                }
                #endregion

                SelectScanner();

                twainDevice.OpenSession();
                _isOpenSeeion = true;
                #region " Set Full Page Size "

                if (twainDevice.IsCapabilitySupported(Capability.IcapSupportedSizes) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(PegasusImaging.WinForms.TwainPro5.Capability.IcapSupportedSizes);
                    myCap.Value = (float)3.0;
                    twainDevice.SetCapability(myCap);
                }
                if (twainDevice.IsCapabilitySupported(Capability.IcapUnits) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                    myCap.Value = (float)0; //Inches
                    twainDevice.SetCapability(myCap);
                }

                twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0f, 8.5f, 11f);//Card Size

                #endregion



                try
                {
                    string _Resolution = "";
                    string _ScanBrightness = "";
                    string _ScanContrast = "";

                    if (twainDevice.IsCapabilitySupported(Capability.IcapUnits) == true)
                    {
                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                        myCap.Value = (float)PegasusImaging.WinForms.TwainPro5.CapabilityConstants.TwunInches;
                        twainDevice.SetCapability(myCap);

                        if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                        {
                            return;
                        }


                        SelectMyComboBoxSetting(ref cmbScanner, gloRegistrySetting.gstrDMSScan, "");

                        object oSetting_DMSResolution = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSResol);
                        if (oSetting_DMSResolution != null)
                        {
                            if (oSetting_DMSResolution.ToString() != "")
                            {
                                _Resolution = oSetting_DMSResolution.ToString();
                                if (_Resolution.Trim() != "")
                                {
                                    for (int i = 0; i < cmbResolution.Items.Count; i++)
                                    {
                                        if (cmbResolution.Items[i].ToString() == _Resolution)
                                        {
                                            cmbResolution.SelectedIndex = i;
                                            myCap = new CapabilityContainerOneValueFloat(Capability.IcapXResolution);
                                            if (twainDevice.IsCapabilitySupported(Capability.IcapXResolution) == true)
                                            {
                                                myCap.Value = (float)(Convert.ToInt32(cmbResolution.SelectedItem.ToString()));
                                                twainDevice.SetCapability(myCap);
                                            }
                                            myCap = new CapabilityContainerOneValueFloat(Capability.IcapYResolution);
                                            if (twainDevice.IsCapabilitySupported(Capability.IcapYResolution) == true)
                                            {
                                                myCap.Value = (float)(Convert.ToInt32(cmbResolution.SelectedItem.ToString()));
                                                twainDevice.SetCapability(myCap);
                                            }
                                            break;
                                        }
                                        
                                    }
                                }
                            }
                        }



                        for (int i = 0; i < cmbScanSide.Items.Count; i++)
                        {
                            if (cmbScanSide.Items[i].ToString() == _ScanSide)
                            {
                                cmbScanSide.SelectedIndex = i;
                                myCap = new CapabilityContainerOneValueFloat(Capability.CapDuplexEnabled);
                                if (twainDevice.IsCapabilitySupported(Capability.CapDuplexEnabled) == true)
                                {
                                    if (_ScanSide == "Front Side")
                                    {
                                        myCap.Value = 0;
                                        //twainDevice.SetCapability(myCap);
                                        //break;
                                    }
                                    if (_ScanSide == "Duplex")
                                    {
                                        myCap.Value = 1;
                                        //twainDevice.SetCapability(myCap);
                                        //break;
                                    }

                                    try
                                    {
                                        twainDevice.SetCapability(myCap);
                                    }
                                    catch (Exception ex)
                                    {
                                        _ErrorMessage = ex.ToString();
                                        AuditLogErrorMessage(_ErrorMessage);
                                        //MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }
                                }
                                break;
                            }

                        }




                        for (int i = 0; i < cmbScanMode.Items.Count; i++)
                        {
                            if (cmbScanMode.Items[i].ToString() == _ScanMode)
                            {
                                cmbScanMode.SelectedIndex = i;

                                //if (twainDevice.IsCapabilitySupported(Capability.IcapBitDepth) == true)
                                // {
                                if (twainDevice.IsCapabilitySupported(Capability.IcapPixelType) == true)
                                {
                                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapPixelType);
                                    if (_ScanMode == "Black & White")
                                    {
                                        myCap.Value = Single.Parse("0"); //Black & White
                                    }
                                    else if (_ScanMode == "GrayScale")
                                    {
                                        myCap.Value = Single.Parse("1"); // GrayScale
                                    }
                                    else if (_ScanMode == "Color")
                                    {
                                        myCap.Value = Single.Parse("2");
                                    }
                                    try
                                    {
                                        twainDevice.SetCapability(myCap);
                                    }
                                    catch (Exception ex)
                                    {
                                        _ErrorMessage = ex.ToString();
                                        AuditLogErrorMessage(_ErrorMessage);
                                        //MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }
                                    //break;
                                    //}

                                }
                                break;
                            }

                        }


                        object oSetting_DMSScanBrightness = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSBright);
                        if (oSetting_DMSScanBrightness != null)
                        {

                            _ScanBrightness = oSetting_DMSScanBrightness.ToString();
                            if (_ScanBrightness.Trim() != "")
                            {
                                for (int i = 0; i < cmbBrightness.Items.Count; i++)
                                {
                                    if (cmbBrightness.Items[i].ToString() == _ScanBrightness)
                                    {
                                        cmbBrightness.SelectedIndex = i;

                                        if (twainDevice.IsCapabilitySupported(Capability.IcapBrightness) == true)
                                        {
                                            myCap = new CapabilityContainerOneValueFloat(Capability.IcapBrightness);
                                            if (_ScanMode == "Black & White")
                                            {
                                                myCap.Value = (float)(-32.0);
                                            }
                                            else if (_ScanMode == "GrayScale")
                                            {
                                                myCap.Value = (float)(96.0);
                                            }
                                            else if (_ScanMode == "Color")
                                            {
                                                //myCap.Value = (float)(124.00);
                                                myCap.Value = (float)(-32.00);
                                            }
                                            try
                                            {
                                                twainDevice.SetCapability(myCap);
                                            }
                                            catch (Exception ex)
                                            {
                                                _ErrorMessage = ex.ToString();
                                                AuditLogErrorMessage(_ErrorMessage);
                                                //MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            //twainDevice.SetCapability(myCap);
                                            //break;
                                        }
                                        break;
                                    }


                                }

                            }

                        }


                        object oSetting_DMSScanContrast = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSContrast);
                        if (oSetting_DMSScanContrast != null)
                        {
                            _ScanContrast = oSetting_DMSScanContrast.ToString();
                            if (_ScanContrast.Trim() != "")
                            {
                                for (int i = 0; i < cmbContrast.Items.Count; i++)
                                {
                                    if (cmbContrast.Items[i].ToString() == _ScanContrast)
                                    {
                                        cmbContrast.SelectedIndex = i;
                                        myCap = new CapabilityContainerOneValueFloat(Capability.IcapContrast);
                                        if (twainDevice.IsCapabilitySupported(Capability.IcapContrast) == true)
                                        {
                                            if (_ScanMode == "Black & White")
                                            {
                                                myCap.Value = (float)(-32.0);
                                            }
                                            else if (_ScanMode == "GrayScale")
                                            {
                                                myCap.Value = (float)(96.0);
                                            }
                                            else if (_ScanMode == "Color")
                                            {
                                                //myCap.Value = (float)(124.0);
                                                myCap.Value = (float)(-32.0);
                                            }
                                            try
                                            {
                                                twainDevice.SetCapability(myCap);
                                            }
                                            catch (Exception ex)
                                            {
                                                _ErrorMessage = ex.ToString();
                                                AuditLogErrorMessage(_ErrorMessage);
                                                // MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            }
                                            //twainDevice.SetCapability(myCap);
                                            //break;
                                        }
                                        break;
                                    }

                                }
                            }

                        }


                        object oSetting_DMSScanMode = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanMode);
                        if (oSetting_DMSScanMode != null)
                        {
                            if (oSetting_DMSScanMode.ToString() != "")
                            {
                                if (_ScanMode.Trim() != "")
                                {
                                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapBitDepth);
                                    if (twainDevice.IsCapabilitySupported(Capability.IcapBitDepth) == true)
                                    {
                                        if (_ScanMode == "Black & White")
                                        {
                                            myCap.Value = (float)1.0;
                                        }
                                        else if (_ScanMode == "GrayScale")
                                        {
                                            myCap.Value = (float)8.0;
                                        }
                                        else if (_ScanMode == "Color")
                                        {
                                            myCap.Value = (float)24.0;
                                        }
                                        try
                                        {
                                            twainDevice.SetCapability(myCap);
                                        }
                                        catch (Exception ex)
                                        {
                                            ex.ToString();
                                            _isAnswer = true;
                                            _ErrorMessage = ex.ToString();
                                            AuditLogErrorMessage(_ErrorMessage);
                                        }
                                        if (_isAnswer == true)
                                        {
                                            try
                                            {
                                                myCap.Value = (float)8.0;
                                                twainDevice.SetCapability(myCap);
                                                _isAnswer = false;
                                            }
                                            catch (Exception ex)
                                            {

                                                _ErrorMessage = ex.ToString();
                                                AuditLogErrorMessage(_ErrorMessage);
                                                //MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            }
                                        }

                                    }
                                }
                            }
                        }
                        //}//else


                        chkShowScannerDialog.Checked = false;
                        object oSetting_DMSShowScanner = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSShowScann);
                        if (oSetting_DMSShowScanner != null)
                        {

                            bool _ScanInterface = Convert.ToBoolean(oSetting_DMSShowScanner.ToString());
                            if (_ScanInterface == true)
                            {
                                chkShowScannerDialog.Checked = true;
                            }
                            else
                            {
                                chkShowScannerDialog.Checked = false;
                            }
                            twainDevice.ShowUserInterface = chkShowScannerDialog.Checked;
                        }

                    }

                    else
                    {
                        MessageBox.Show("Scanner settings not loaded, you can use default settings.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                    {
                        gloRegistrySetting.CloseRegistryKey();
                    }

                }

                catch (Exception ex)
                {
                    #region " Make Log Entry "

                    _ErrorMessage = ex.ToString();
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                    MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (_IsScannerConnected == true)
                {
                    twainDevice.StartSession();

                    #region " Load File "

                    if (c1Documents != null)
                    {

                        if (c1Documents.RowSel >= 0)
                        {
                            if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
                            {
                                string _filePath = "";
                                _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                if (File.Exists(_filePath) == true)
                                {
                                    //LoadImage(_filePath);
                                    LoadImageWithoutFlickering(_filePath);
                                }
                            }
                        }

                    }

                    #endregion

                    if (twainDevice != null)
                    {
                        twainDevice.CloseSession();
                       
                    }
                    _isOpenSeeion = false;
                }
                else
                {
                    MessageBox.Show("Scanner is not connected.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (PegasusImaging.WinForms.TwainPro5.TwainProException ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                //MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _ScanningInProgress = false;
                if (c1Documents.Rows.Count > 0)
                {
                    tlb_Remove.Enabled = true;
                    tlb_RemoveAll.Enabled = true;
                    tlb_RotateBack.Enabled = true;
                    tlb_RotateForward.Enabled = true;
                }
                //tlb_Scan.Enabled = true;
                tls_btnScanCard.Enabled = true;
                //tls_btnScanHalf.Enabled = true;
                tlb_Ok.Enabled = true;
                tlb_LoadImages.Enabled = true;
                tlb_Cancel.Enabled = true;
                tlb_Settings.Enabled = true;

                tlb_BWFront.Enabled = true;
                tlb_BWDuplex.Enabled = true;
                tlb_ColorFront.Enabled = true;
                tlb_ColorDuplex.Enabled = true;
                tlb_GrayFront.Enabled = true;
                tlb_GrayDuplex.Enabled = true;
                if (_isOpenSeeion == true)
                {
                    if (twainDevice != null)
                    {
                        twainDevice.CloseSession();
                        
                    }
                }
            }

        }



        #endregion
        //End--Commented
        # endregion "setting Shortcuts"


        #region "Dhruv 20100621 -> Form Closing Event "
        
        private Control _lastControl;
        private void TrapLostFocusOnChildControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.LostFocus += new EventHandler(AllLostFocus);

                Control.ControlCollection childControls = control.Controls;
                if (childControls != null)
                    TrapLostFocusOnChildControls(childControls);
            }
        }
        public void AllLostFocus(object sender, EventArgs e)
        {
            _lastControl = (Control)sender;
        }
       
        private void frmEDocEvent_ScanNSend_PS_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (objfrmScanProgress != null)
                {

                    if (MessageBox.Show("Scanning is in progress still do you want to close scanning window?", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + imgCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".cancel")))
                        {
                            myFile.Close();
                            // interact with myFile here, it will be disposed automatically
                        }

                    }
                    else
                    {
                        e.Cancel = false;
                        objfrmScanProgress.BringToFront();
                        return;
                    }

                }

                if (_isSaveClose == true)
                {
                    _isSaveClose = false;
                    saveClose();

                }
                else
                {
                    if (c1Documents.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Are you sure to close scanning window, without saving document?", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            
                            if (_bStartSession == false)
                            {
                                saveClose();
                            }
                            else
                            {
                                MessageBox.Show("Please close the scanner error message which might be in a minimized state.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK , MessageBoxIcon.Question);
                                e.Cancel = true;
                            }
                            e.Cancel = false;

                        }
                        else
                        {
                            if (_bStartSession == false)
                            {
                                e.Cancel = true;
                            }
                            else
                            {
                                MessageBox.Show("Please close the scanner error message which might be in a minimized state.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question);
                                e.Cancel = true;
                            }
                        }
                    }
                    else
                    {
                        if (_bStartSession == false)
                        {
                            e.Cancel = false;
                        }
                        else
                        {
                            MessageBox.Show("Please close the scanner error message which might be in a minimized state.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question);
                            e.Cancel = false;
                        }
                    }

                }
                if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan && e.Cancel == false)
                {
                    gloGlobal.gloProgressAndClipboard.SetClipboardData();
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion " Form Closing Event "

        #region " Private Methods "

        private void saveClose()
        {
            if (bOpenedFromExam == false)
            {
                if (_ScanImagesTempFolderPath != "")
                {
                    if (System.IO.Directory.Exists(_ScanImagesTempFolderPath) == true)
                    {
                        //if (imageControl1.BackgroundImage != null)
                        //{
                        //    imageControl1.BackgroundImage.Dispose();
                        //    imageControl1.BackgroundImage = null;
                        //}
                        if (imageControl1 != null)
                        {
                            imageControl1.Dispose();
                            imageControl1 = null;
                        }
                        //if (oImgViewer.CurrentImage != null)
                        //{
                        //    oImgViewer.CurrentImage.Dispose();
                        //}
                        //if (oImgViewer != null)
                        //{
                        //    oImgViewer.Dispose();
                        //    oImgViewer = null;

                        //}
                        //if (imageXView.Image != null)
                        //{
                        //    imageXView.Image.Dispose();
                        //    imageXView.Image = null;
                        //}
                        //if (imageXView != null)
                        //{
                        //    imageXView.Dispose();
                        //    imageXView = null;
                        //}
                        //if (imagXpress1 != null)
                        //{
                        //    imagXpress1.Dispose();
                        //    imagXpress1 = null;
                        //}
                        if (!gloGlobal.clsFileExtensions.IsSystemOrRootDir(_ScanImagesTempFolderPath))
                        {
                            try
                            {
                                System.IO.DirectoryInfo oDirectoryInfo = new System.IO.DirectoryInfo(_ScanImagesTempFolderPath);
                                if (oDirectoryInfo != null)
                                {

                                    oDirectoryInfo.Delete(true);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            tlb_Cancel.Enabled = true;


            //Dhruv
            if (twainDevice != null)
            {
                twainDevice.Scanned -= new ScannedEventHandler(twainDevice_Scanned);
                twainDevice.Dispose();
                twainDevice = null;
            }
        }


        public Int32 GetPageCount(string FileFullPath)
        {
            Int32 _result = 0;

            pdftron.PDF.PDFDoc oDocument;

            try
            {
                oDocument = new pdftron.PDF.PDFDoc(FileFullPath);
                try
                {
                    oDocument.InitSecurityHandler();
                }
                catch (Exception)
                {

                    //Intetionally left Blank
                }
                
                _result = oDocument.GetPageCount();

                oDocument.Close();
                oDocument.Dispose();
                oDocument = null;
            }
            catch (Exception ex)
            {
                ex.ToString();
                _result = 0;

            }
            finally
            {
            }

            if (_ErrorMessage.Trim() != "")
            {
                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "GetPageCount" + Environment.NewLine + "ERROR : " + _ErrorMessage;
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                _MessageString = "";
            }

            return _result;
        }

        private bool GetXWidthYHeight(float HorRes, float VerRes, float pxHeight, float pxWidth, out int XWidth, out int YHeight)
        {
            bool _result = false;
            float _XWidth = 0;
            float _YHeight = 0;
            try
            {
                float _ResDactor = 0;
                bool _XMajor = false;
              //  string _Orientation = "S"; // P-Potrait, L-Landscape, S-Same
                float _XInch = 0;
                float _YInch = 0;

                //Factor + Major X or Y
                if (HorRes > VerRes) { _ResDactor = HorRes / VerRes; _XMajor = true; }
                else if (HorRes < VerRes) { _ResDactor = VerRes / HorRes; _XMajor = false; }
                else if (HorRes == VerRes) { _ResDactor = 1; }

                //Height & Width in Inches
                _XInch = pxHeight / HorRes;
                _YInch = pxWidth / VerRes;

                ////Page Orientation
                //if (_XInch > _YInch) { _Orientation = "L"; }
                //else if (_XInch < _YInch) { _Orientation = "P"; }

                //Calculate Return Height and Width in Pixcel
                if (_XMajor == true)
                {
                    _XWidth = pxWidth;
                    _YHeight = pxHeight * _ResDactor;
                }
                else
                {
                    _XWidth = pxWidth * _ResDactor;
                    _YHeight = pxHeight;
                }

                _result = true;
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


            }
            finally
            {
            }

            XWidth = Convert.ToInt32(_XWidth);
            YHeight = Convert.ToInt32(_YHeight);
            return _result;
        }

        void oDocManager_DocumentProgressEvent(int Percentage, string Message)
        {
            Application.DoEvents();
            if (Percentage <= pbDocument.Maximum) { pbDocument.Value = Percentage; }
        }



        #region "Dhruv 20100621 -> IsValidExtenstionFile"
       
        private bool IsValidExtenstionFile(string Extenstion, bool IsPDFDocument)
        {
            bool _result = false;
            string strFileExtension = Extenstion.ToUpper();
            if (IsPDFDocument == true)
            {
                if (strFileExtension == ".PDF")
                {
                    _result = true;
                }
            }
            else
            {
                if (strFileExtension == ".JPEG" || strFileExtension == ".JPG" || strFileExtension == ".TIFF" || strFileExtension == ".TIF" || strFileExtension == ".BMP" || strFileExtension == ".PNG")
                {
                    _result = true;
                }
            }
            return _result;
        }
        #endregion "Dhruv 20100621 -> IsValidExtenstionFile"

        #endregion " Private Methods "

        #region " Pegasus Image Manipulation "


        #region "Dhruv 20100621-> LoadImage + UnloadImage "
       // gloScanImaging.ImageControl ogloImageControl = null;
        private void LoadImage(string _strFilePath, int _CurrIndex = 9)
        {

            try
            {

               // UnloadImage();
                try
                {
                   // imageControl1.SetImageSource(new gloScanImaging.ImageFile(_strFilePath));
                    imageControl1.SetImageWithPath(_strFilePath, _CurrIndex: _CurrIndex);

                   // oImgViewer.SetImageSource(new gloScanImaging.ImageFile(_strFilePath));
                   
                    //ogloImageControl.SetImageSource(new gloScanImaging.ImageFile(_strFilePath));
                    //imageControl1.BackgroundImage.HorizontalResolution
                   // imageXView.Image = PegasusImaging.WinForms.ImagXpress9.ImageX.FromFile(imagXpress1, _strFilePath, 1);
                   // imageXView.Image.ImageXData.Resolution.Units = GraphicsUnit.Inch;
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                    MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                //if (oImgViewer.CurrentImage == null)
                //{
                //    _ErrorMessage = " oImgViewer is null ";
                //    AuditLogErrorMessage(_ErrorMessage);
                //    MessageBox.Show("ERROR : " + _ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}
                //if (imageXView.Image == null)
                //{
                //    _ErrorMessage = " ImageXview is null ";
                //    AuditLogErrorMessage(_ErrorMessage);
                //    MessageBox.Show("ERROR : " + _ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);

            }

        }

        private void UnloadDisplayImage()
        {
            try
            {
                imageControl1.UnloadDisplayImage();
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);

            }
        }

        private void UnloadImage()
        {
           UnloadDisplayImage();
        }

        #endregion "Dhruv 20100621-> LoadImage + UnloadImage "
        private void Rotate(double angle)
        {
            //processor of ImagXpress for image processing operations
            //PegasusImaging.WinForms.ImagXpress9.Processor processor1 = new PegasusImaging.WinForms.ImagXpress9.Processor(imagXpress1);
            //try
            //{
            //    if (imageXView.Image != null)
            //    {
            //        processor1.Image = imageXView.Image;
            //        processor1.Rotate(angle);
            //        imageXView.Image.Save(Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)));

            //    }

            //}
            //catch (Exception ex)
            //{
            //    #region " Make Log Entry "

            //    _ErrorMessage = ex.ToString();

            //    if (_ErrorMessage.Trim() != "")
            //    {
            //        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //        _MessageString = "";
            //    }

            //    #endregion " Make Log Entry "

            //    MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //finally
            //{
            //    if (processor1 != null)
            //    {
            //        processor1.Image = null;
            //        processor1.Dispose();
            //    }

            //}
        }

        private void Rotate(bool Clockwise)
        {
            try
            {

                if (imageControl1.CurrImage != null)
                {
                    
                    //imageControl1.SetImageWithPath(Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)));
                    imageControl1.RotateImage(Clockwise,imageControl1._CurrZoomIndex);
                    //oImg.CurrentDisplayedImage.Save(Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)));
                    
                }
                
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();

                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }


        }

        #region "Dhruv 20100621-> Zoom"
      //  public gloScanImaging.ImageViewer oImgViewer = new gloScanImaging.ImageViewer();
        private void Zoom()
        {
            if (cmbZoom.SelectedIndex != -1)
            {
                //double _ZoomFactor = 1.0;
                try
                {
                    //if (imageXView.Image != null)
                    //{

                    //    //Dhruv
                    //    string scmbvalue = cmbZoomPercentage.Text.Replace("%", "");
                    //    float fPercentage = (float.Parse(scmbvalue) / 100);
                    //    _ZoomFactor = fPercentage;

                    //    imageXView.ZoomFactor = (double)_ZoomFactor;
                    //}
                    if (imageControl1.CurrImage != null)
                    {
                        imageControl1.ZoomValueChanged(cmbZoom);
                        imageControl1._CurrZoomIndex = cmbZoom.SelectedIndex;
                        //if (cmbZoomPercentage.Text.IndexOf('%') != -1)
                        //{ }
                        //string scmbvalue = cmbZoomPercentage.Text.Replace("%", "");
                        //float fPercentage = (float.Parse(scmbvalue) / 100);
                        //_ZoomFactor = fPercentage;
                        ////oImgViewer.ZoomImage((double)_ZoomFactor);
                        //imageControl1.DisplayImageWithZoomVal((double)_ZoomFactor);
                    }

                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                }
            }
            else
            {
                // Sudhir 20090112
                
                if (_ZoomState == enum_ZoomState.BestFit)
                {
                    ZoomFitButton_Click(null, null); 
                    //ZoomFitButtons();
                    //imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITHEIGHT);

                }
                if (_ZoomState == enum_ZoomState.FitToHeight)
                {
                    ZoomHeightButton_Click(null, null); 
                    //ZoomHeightButtons();
                }
                if (_ZoomState == enum_ZoomState.FitToWidth)
                {
                    ZoomWidthButton_Click(null, null); 
                    //ZoomWidthButtons();

                }
            }
        }
        #endregion "Dhruv 20100621-> Zoom"

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (imageControl1.CurrImage != null)
            {
                if (cmbZoom.SelectedIndex < (cmbZoom.Items.Count - 1))
                {
                    if (cmbZoom.SelectedIndex != 8)
                    {
                        imageControl1.UpdateScreenOfControl(false);
                        if (cmbZoom.SelectedIndex == 9 || cmbZoom.SelectedIndex == 10 || cmbZoom.SelectedIndex == 11 || cmbZoom.SelectedIndex == 12)
                        {
                            cmbZoom.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbZoom.SelectedIndex = cmbZoom.SelectedIndex + 1;
                        }
                        imageControl1.UpdateScreenOfControl(true);
                    }
                }
            }
        }






        private void btnZoomOut_Click(object sender, EventArgs e)
        {




            if (imageControl1.CurrImage != null)
            {
                if (cmbZoom.SelectedIndex > 0)
                {
                    if (cmbZoom.SelectedIndex != 9 && cmbZoom.SelectedIndex != 10 && cmbZoom.SelectedIndex != 11 && cmbZoom.SelectedIndex != 12)
                    {
                        imageControl1.UpdateScreenOfControl(false);

                        cmbZoom.SelectedIndex = cmbZoom.SelectedIndex - 1;

                        imageControl1.UpdateScreenOfControl(true);
                    }
                }
            }







        }




        //#region "Dhruv 20100621-> ZoomIn"

        //private void btnZoomIn_Click(object sender, EventArgs e)
        //{
        //    ZoomIn();
        //}
        //private void ZoomIn()
        //{
        //    //if (imageXView.Image != null)
        //    //{
        //    //    if (cmbZoomPercentage.SelectedIndex < (cmbZoomPercentage.Items.Count - 1))
        //    //    {
        //    //        cmbZoomPercentage.SelectedIndex = cmbZoomPercentage.SelectedIndex + 1;
        //    //    }
        //    //}
        //     //if (imageControl1.CurrImage != null)
        //     //{
        //     //    if (cmbZoom.SelectedIndex < (cmbZoom.Items.Count - 1))
        //     //    {
        //     //        cmbZoom.SelectedIndex = cmbZoom.SelectedIndex + 1;
        //     //    }
        //     //}

        //}
        //#endregion "Dhruv 20100621-> ZoomIn"



        //#region "Dhruv 20100621 -> ZoomOut"

        //private void btnZoomOut_Click(object sender, EventArgs e)
        //{
        //    ZoomOut();
        //}
        //private void ZoomOut()
        //{
        //    if (imageControl1.CurrImage != null)
        //    {
        //        if (cmbZoom.SelectedIndex > 0)
        //        {
        //            cmbZoom.SelectedIndex = cmbZoom.SelectedIndex - 1;
        //        }
        //    }
        //    //if (imageXView.Image != null)
        //    //{
        //    //    if (cmbZoomPercentage.SelectedIndex > 0)
        //    //    {
        //    //        cmbZoomPercentage.SelectedIndex = cmbZoomPercentage.SelectedIndex - 1;
        //    //    }
        //    //}
        //}

        //#endregion "Dhruv 20100621 -> ZoomOut"


        //private void cmbZoomPercentage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbZoomPercentage.SelectedIndex != -1)
        //    {
        //        Zoom();
        //    }
        //}




        private void ZoomHeightButton_Click(object sender, EventArgs e)
        {
            //ZoomToolUpdate(ZoomToFitType.FitHeight);
            imageControl1.UpdateScreenOfControl(false);
            imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITHEIGHT);
            cmbZoom.SelectedIndex = 11;
            imageControl1.UpdateScreenOfControl(true);
            //_ZoomState = enum_ZoomState.FitToHeight;




        }




        private void ZoomFitButton_Click(object sender, EventArgs e)
        {
            //ZoomToolUpdate(ZoomToFitType.FitBest);
            imageControl1.UpdateScreenOfControl(false);
            imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITPAGE);
            cmbZoom.SelectedIndex = 9;
            imageControl1.UpdateScreenOfControl(true);
            //_ZoomState = enum_ZoomState.BestFit;











        }




        private void ZoomWidthButton_Click(object sender, EventArgs e)
        {
            //ZoomToolUpdate(ZoomToFitType.FitWidth);
            imageControl1.UpdateScreenOfControl(false);
            imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITWIDTH);
            cmbZoom.SelectedIndex = 10;
            imageControl1.UpdateScreenOfControl(true);
            //_ZoomState = enum_ZoomState.FitToWidth;
        }
        //#region "Dhruv 20100621-> ZoomHeightButton"

        //private void ZoomHeightButton_Click(object sender, EventArgs e)
        //{
        //    //ZoomHeightButtons();
        //    cmbZoom.SelectedIndex = -1;
        //    imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITHEIGHT);
        //}
        //private void ZoomHeightButtons()
        //{
        //    cmbZoom.SelectedIndex = -1;
        //    //ZoomToolUpdate(PegasusImaging.WinForms.ImagXpress9.ZoomToFitType.FitHeight);
        //    ZoomToolUpdate(gloScanImaging.ZoomMode.FITHEIGHT);
        //    _ZoomState = enum_ZoomState.FitToHeight;
        //}
        //#endregion "Dhruv 20100621-> ZoomHeightButton"

        //#region "Dhruv 20100621-> ZoomFitButton"

        //private void ZoomFitButton_Click(object sender, EventArgs e)
        //{
        //    //ZoomFitButtons();
        //    cmbZoom.SelectedIndex = -1;
        //    imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITPAGE);
        //}
        //private void ZoomFitButtons()
        //{
        //    cmbZoom.SelectedIndex = -1;
        //    //if (gloGlobal.gloEliminatePegasus.bEliminatePegasus)
        //    //{
        //        ZoomToolUpdate(gloScanImaging.ZoomMode.FITPAGE);
        //    //}
        //    //else
        //    //{
        //    //    ZoomToolUpdate(PegasusImaging.WinForms.ImagXpress9.ZoomToFitType.FitBest);
        //    //}

        //    _ZoomState = enum_ZoomState.BestFit;
        //}
        //#endregion

        //#region "Dhruv 20100621-> ZoomWidthButton"

        //private void ZoomWidthButton_Click(object sender, EventArgs e)
        //{
        //    //ZoomWidthButtons();
        //    cmbZoom.SelectedIndex = -1;
        //    imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITWIDTH);
        //}
        //private void ZoomWidthButtons()
        //{
        //    cmbZoom.SelectedIndex = -1;
        //    //ZoomToolUpdate(PegasusImaging.WinForms.ImagXpress9.ZoomToFitType.FitWidth);
        //    ZoomToolUpdate(gloScanImaging.ZoomMode.FITWIDTH);
        //    _ZoomState = enum_ZoomState.FitToWidth;
        //}
        //#endregion "Dhruv 20100621-> ZoomWidthButton"

        //private void ZoomToolUpdate(PegasusImaging.WinForms.ImagXpress9.ZoomToFitType zoom)
        //{
        //    //try
        //    //{
        //    //    //imageXView object's AutoResize is set to BestFit at startup, we must
        //    //    //set to a different enumeration that allows ZoomToFit() to have an effect
        //    //    imageXView.AutoResize = PegasusImaging.WinForms.ImagXpress9.AutoResizeType.CropImage;

        //    //    //Zoom to BestFit
        //    //    imageXView.ZoomToFit(zoom);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    _ErrorMessage = ex.ToString();
        //    //    AuditLogErrorMessage(_ErrorMessage);

        //    //    MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //}
        //}

        //private void ZoomToolUpdate(gloScanImaging.ZoomMode zoom)
        //{
        //    try
        //    {
        //        //imageXView object's AutoResize is set to BestFit at startup, we must
        //        ////set to a different enumeration that allows ZoomToFit() to have an effect
        //        //if (gloGlobal.gloEliminatePegasus.bEliminatePegasus)
        //        //{
        //            imageControl1.ZoomImage(zoom);
        //        //}
        //        //else
        //        //{
        //        //    imageXView.AutoResize = PegasusImaging.WinForms.ImagXpress9.AutoResizeType.CropImage;
        //        //    //Zoom to BestFit
        //        //    imageXView.ZoomToFit(zoom);
        //        //}

        //       // oImgViewer.ZoomImage(zoom);
        //    }
        //    catch (Exception ex)
        //    {
        //        _ErrorMessage = ex.ToString();
        //        AuditLogErrorMessage(_ErrorMessage);

        //        MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void tlb_RotateForward_Click(object sender, EventArgs e)
        {
            //Rotate(270.0);
            Rotate(true);
            Zoom();
            //cmbZoom.SelectedIndex = 11;
            // LoadRotatedImage();
        }

        private void tlb_RotateBack_Click(object sender, EventArgs e)
        {
            //Rotate(90.0);
            Rotate(false);
            Zoom();
            //cmbZoom.SelectedIndex = 11;
            //   LoadRotatedImage();
        }


        #endregion

        #region " Designer Events "

        #endregion " Designer Events "

        //private void imageXView_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        imageXView.ContextMenu = null;
        //        imageXView.ContextMenuStrip = null;
        //    }
        //}

        //private void imageXView_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        imageXView.ContextMenu = null;
        //        imageXView.ContextMenuStrip = null;
        //    }
        //}

        //private void imageXView_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        imageXView.ContextMenu = null;
        //        imageXView.ContextMenuStrip = null;
        //    }
        //}

        private void txtDocumentName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sFileName = txtDocumentName.Text.Trim();
                string sValidFileName = "";
               
                sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace("(", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");
                if (sFileName != sValidFileName)
                {
                    txtDocumentName.Text = sValidFileName;
                    txtDocumentName.Select(txtDocumentName.Text.Length, 1);
                }
            }
            catch (Exception ex)
            {
                string _ex = ex.Message;
            }
        }

        private void btn_Right_MouseHover(object sender, EventArgs e)
        {
            btn_Right.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Forward;
            btn_Right.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Right_MouseLeave(object sender, EventArgs e)
        {
            btn_Right.BackgroundImage = global::gloEDocumentV3.Properties.Resources.ForwardHover;
            btn_Right.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomOut_MouseHover(object sender, EventArgs e)
        {
            btnZoomOut.BackgroundImage = global::gloEDocumentV3.Properties.Resources.MinusHover;
            btnZoomOut.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomOut_MouseLeave(object sender, EventArgs e)
        {
            btnZoomOut.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Minus1;
            btnZoomOut.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomIn_MouseHover(object sender, EventArgs e)
        {
            btnZoomIn.BackgroundImage = global::gloEDocumentV3.Properties.Resources.pluseHover;
            btnZoomIn.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomIn_MouseLeave(object sender, EventArgs e)
        {
            btnZoomIn.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Pluse;
            btnZoomIn.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Left_MouseHover(object sender, EventArgs e)
        {
            btn_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.RewindHover;
            btn_Left.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Left_MouseLeave(object sender, EventArgs e)
        {
            btn_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Rewind;
            btn_Left.BackgroundImageLayout = ImageLayout.Center;
        }

        private void txtCardWidth_Leave(object sender, EventArgs e)
        {
            if (!isClosedClick)
            {
                float _CardWidth;
                if (txtCardWidth.Text.Trim() != "")
                {
                    //try catch block added by dipak to fix 4437 :-Default card size to 4"x4" Comment no :12 It given error when we click on card size
                    try
                    {
                        _CardWidth = (float)Convert.ToDouble(txtCardWidth.Text.Trim());
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Card Width is invalid", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCardWidth.Focus();
                        return;
                    }
                    if (_CardWidth < (float)2.1)
                    {
                        MessageBox.Show("Card Width must be greater than 2.0 Inches", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCardWidth.Focus();
                    }
                }
            }
        }

        private void txtCardLength_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!isClosedClick)
                {
                    float _CardLength;
                    if (txtCardLength.Text.Trim() != "")
                    {
                        //try catch block added by dipak to fix 4437 :-Default card size to 4"x4" Comment no :12 It given error when we click on card size
                        try
                        {

                            _CardLength = (float)Convert.ToDouble(txtCardLength.Text.Trim());
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Card Length is invalid", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCardLength.Focus();
                            return;
                        }

                        if (_CardLength < (float)2.2)
                        {
                            MessageBox.Show("Card Length must be greater than 2.1 Inches", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCardLength.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCardWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
            else
            {
                if (txtCardWidth.SelectionStart > txtCardWidth.Text.IndexOf("."))
                {
                    if (txtCardWidth.Text.Contains(".") == true)
                    {
                        if (txtCardWidth.Text.Substring(txtCardWidth.Text.IndexOf(".") + 1, txtCardWidth.Text.Length - (txtCardWidth.Text.IndexOf(".") + 1)).Length == 3)
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            if (e.KeyChar == Convert.ToChar(46) && txtCardWidth.Text.Contains("."))
            {
                e.Handled = true;
            }
            //added by dipak 20091027
            //If Backspace pressed
            if (e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
        }

        private void txtCardLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
            else
            {
                if (txtCardLength.SelectionStart > txtCardLength.Text.IndexOf("."))
                {
                    if (txtCardLength.Text.Contains(".") == true)
                    {
                        if (txtCardLength.Text.Substring(txtCardLength.Text.IndexOf(".") + 1, txtCardLength.Text.Length - (txtCardLength.Text.IndexOf(".") + 1)).Length == 3)
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            if (e.KeyChar == Convert.ToChar(46) && txtCardLength.Text.Contains("."))
            {
                e.Handled = true;
            }
            //added by dipak 20091027
            //If Backspace pressed
            if (e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
        }


        #region "Dhruv 20100621-> ImageCount"
       
        private int imageCount()
        {

            bool isUnique = false;
            int currentCounter = 0;
            while (isUnique == false)
            {
                isUnique = true;
                for (int j = currentCounter; j < c1Documents.Rows.Count; j++)
                {
                    if ("ImportImage - " + nCountImageNo.ToString("000#") == c1Documents.GetData(j, COL_NAME).ToString())
                    {
                        isUnique = false;
                        nCountImageNo = nCountImageNo + 1;
                        currentCounter = j + 1;
                        break;
                    }
                }
            }
            return nCountImageNo; //Return Image count
        }
        #endregion "Dhruv 20100621-> ImageCount"

        #region "Dhruv 20100621-> Audit Log"
        private void AuditLogErrorMessage(string _ErrorMessage)
        {
            string _MessageString = "";
            if (_ErrorMessage.Trim() != "")
            {
                _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                _MessageString = "";
            }


        }
        #endregion

        #region "Dhruv 20100621 -> LoadScanner + SelectMyCombobox Setting"

        private void LoadScannerSettings()
        {
            #region "Varable Decleration"
            //RegistryKey regKey = null;
            #endregion
            try
            {

                //regKey = Registry.LocalMachine.OpenSubKey("Software\\gloEMR", true);
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                {
                    return;
                }

                //Scanner List
                //SelectMyComboBoxSetting(ref cmbScanner, sSetting_DMSScanner, regKey);
                SelectMyComboBoxSetting(ref cmbScanner, gloRegistrySetting.gstrDMSScan, "");



                //Resolution List
                //SelectMyComboBoxSetting(ref cmbResolution, sSetting_DMSResolution, regKey);
                SelectMyComboBoxSetting(ref cmbResolution, gloRegistrySetting.gstrDMSResol, "");

                //Side setting : Duplex ? setting
                //SelectMyComboBoxSetting(ref cmbScanSide, (_IsScanCard == true ? "Duplex" :sSetting_DMSScanSide), regKey);
                SelectMyComboBoxSetting(ref cmbScanSide, (_IsScanCard == true ? "Duplex" : gloRegistrySetting.gstrDMSScanSide), "");

                //Scanner mode
                //SelectMyComboBoxSetting(ref cmbScanMode, sSetting_DMSScanMode, regKey);
                SelectMyComboBoxSetting(ref cmbScanMode, gloRegistrySetting.gstrDMSScanMode, "");

                //Brightness settings
                //SelectMyComboBoxSetting(ref cmbBrightness, sSetting_DMSScanBrightness, regKey);
                SelectMyComboBoxSetting(ref cmbBrightness, gloRegistrySetting.gstrDMSBright, "");


                //Constrast setting
                //SelectMyComboBoxSetting(ref cmbContrast, sSetting_DMSScanContrast, regKey);
                SelectMyComboBoxSetting(ref cmbContrast, gloRegistrySetting.gstrDMSContrast, "");


                chkShowScannerDialog.Checked = false;
                object oSetting_DMSShowScanner = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSShowScann);
                if (oSetting_DMSShowScanner != null)
                {
                    if (oSetting_DMSShowScanner.ToString() != "")
                    {
                        bool _ScanInterface = Convert.ToBoolean(oSetting_DMSShowScanner.ToString());
                        if (_ScanInterface == true)
                        {
                            chkShowScannerDialog.Checked = true;
                        }
                        else
                        {
                            chkShowScannerDialog.Checked = false;
                        }
                        twainDevice.ShowUserInterface = chkShowScannerDialog.Checked;
                    }
                }


                object oSetting_CardWidth = gloRegistrySetting.GetRegistryValue(sSetting_CardWidth);
                if (oSetting_CardWidth != null)
                {
                    if (oSetting_CardWidth.ToString().Trim() != "")
                    {
                        string _ScanCardWidth = oSetting_CardWidth.ToString();
                        if (_ScanCardWidth.Trim() != "")
                        {
                            txtCardWidth.Text = _ScanCardWidth.Trim();
                        }
                        //else is added by dipak 20091007 to set default card width to 4.0
                        else
                        {
                            txtCardWidth.Text = "4.0";
                            gloRegistrySetting.SetRegistryValue(sSetting_CardWidth, txtCardWidth.Text.Trim());
                        }
                    }
                    else
                    {
                        txtCardWidth.Text = "4.0";
                        gloRegistrySetting.SetRegistryValue(sSetting_CardWidth, txtCardWidth.Text.Trim());
                    }
                }
                else
                {
                    txtCardWidth.Text = "4.0";
                    gloRegistrySetting.SetRegistryValue(sSetting_CardWidth, txtCardWidth.Text.Trim());
                }
                //Sandip Darade   20090926
                // Add card size setting :card length
                object oSetting_CardLength = gloRegistrySetting.GetRegistryValue(sSetting_CardLength);
                if (oSetting_CardLength != null)
                {
                    string _ScanCardLength = oSetting_CardLength.ToString();
                    if (_ScanCardLength.Trim() != "")
                    {
                        txtCardLength.Text = _ScanCardLength.Trim();
                    }
                    else
                    {
                        txtCardLength.Text = "4.0";
                        gloRegistrySetting.SetRegistryValue(sSetting_CardLength, txtCardLength.Text.Trim());
                    }

                }
                else
                {
                    txtCardLength.Text = "4.0";
                    gloRegistrySetting.SetRegistryValue(sSetting_CardLength, txtCardLength.Text.Trim());
                }
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                {
                    gloRegistrySetting.CloseRegistryKey();
                }
                //}
            }

            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                {
                    gloRegistrySetting.CloseRegistryKey();
                }
            }
        }
        
        private int SelectMyComboBoxSetting(ref System.Windows.Forms.ComboBox cmbBox, string regString, string none)
        {

            cmbBox.SelectedIndex = -1;
            object oSetting = gloRegistrySetting.GetRegistryValue(regString);
            if (oSetting != null)
            {

                string curString = oSetting.ToString().Trim();
                if (curString != "")
                {
                    for (int i = 0; i < cmbBox.Items.Count; i++)
                    {
                        if (cmbBox.Items[i].ToString() == curString)
                        {
                            cmbBox.SelectedIndex = i;
                            break;
                        }
                    }
                }

            }
            return cmbBox.SelectedIndex;
        }
        private int SelectMyComboBoxSetting(ref System.Windows.Forms.ComboBox cmbBox, string regString)
        {

            cmbBox.SelectedIndex = -1;



            string curString = regString;
            if (curString != "")
            {
                for (int i = 0; i < cmbBox.Items.Count; i++)
                {
                    if (cmbBox.Items[i].ToString() == curString)
                    {
                        cmbBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            return cmbBox.SelectedIndex;
        }
        #endregion



        #region "Scanning new functionality->StartScanning"
    

        bool _bStartSession = false;
        int nCurrScanCnt;
        private void StartScanning(String ScanMode, String ScanBitDepth, String ScanDuplex)
        {
            icnt = 0;
            //AuditLogErrorMessage("In StartScanning : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            bool openedSession = false;
            //bool _isSessionStarted = false;
            try
            {
                if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan || gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                {
                    string ScanType = ScanMode + " , " + ScanDuplex;
                    nCurrScanCnt = 0;
                    CallScanningForRemoteScan(ScanMode, ScanDuplex, ScanType, ScanBitDepth);
                }
                else
                {
                    _ScanningInProgress = true;
                    // tlb_Scan.Enabled = false;
                    tlb_Ok.Enabled = false;
                    tlb_Cancel.Enabled = false;
                    tlb_Settings.Enabled = false;

                    tls_btnScanCard.Enabled = false;
                    //tls_btnScanHalf.Enabled = false;

                    tlb_BWFront.Enabled = false;
                    tlb_BWDuplex.Enabled = false;
                    tlb_ColorFront.Enabled = false;
                    tlb_ColorDuplex.Enabled = false;
                    tlb_GrayFront.Enabled = false;
                    tlb_GrayDuplex.Enabled = false;

                    tlb_LoadImages.Enabled = false;
                    _IsScanCard = true;

                    System.Windows.Forms.ComboBox cmbBox = null;
                    ScannerSettings objScannerSettings = new ScannerSettings();
                    if (twainDevice == null)
                    {
                        //AuditLogErrorMessage("(twainDevice == null)");
                    }
                    else
                    { //AuditLogErrorMessage("(twainDevice != null)"); 
                    }

                    //AuditLogErrorMessage("Before InitPagasusTwainDevice");
                    InitPagasusTwainDevice();
                    //AuditLogErrorMessage("After InitPagasusTwainDevice");
                    _IsScannerConnected = objScannerSettings.GetAndSetScanners(ref twainDevice, ref cmbBox, gloRegistrySetting.gstrDMSScan);//Setting  up the Scanner
                   // AuditLogErrorMessage("_IsScannerConnected : " + _IsScannerConnected);
                    if (_IsScannerConnected == true)
                    {
                        try
                        {
                            twainDevice.OpenSession();
                            openedSession = true;
                        }
                        catch (Exception ex)
                        {
                            _ErrorMessage = ex.ToString();
                            AuditLogErrorMessage(_ErrorMessage);
                            openedSession = false;
                        }
                        if (openedSession == true)
                        {

                            SetScannersSettings(objScannerSettings, ScanMode, ScanBitDepth, ScanDuplex);
                            GetRemoteScannerSetting();

                            #region " Set Full Page Size "

                            #endregion

                            _bStartSession = true;
                            try
                            {
                                //AuditLogErrorMessage("Before StartSession");
                                twainDevice.StartSession();
                                //AuditLogErrorMessage("After StartSession");
                            }
                            catch (PegasusImaging.WinForms.TwainPro5.TwainException ex)
                            {
                                AuditLogErrorMessage("ConditionCode : " + ex.ConditionCode + " || ConditionDescription : " + ex.ConditionDescription);
                            }
                            
                            //Problem : 00000341 : Scanning
                            //Activate Scan window
                            //  SetActiveWindow();

                            _bStartSession = false;
                            #region " Load File "
                            bool zoomNotDone = true;

                            if (c1Documents != null)
                            {
                                //   if (c1Documents.RowSel != null)
                                {
                                    if (c1Documents.RowSel >= 0)
                                    {
                                        if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
                                        {
                                            string _filePath = "";
                                            _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                            if (File.Exists(_filePath) == true)
                                            {
                                                //LoadImage(_filePath);
                                                LoadImageWithoutFlickering(_filePath);
                                                zoomNotDone = false;
                                            }
                                        }
                                    }
                                }
                            }
                            if (zoomNotDone)
                            {
                                Zoom();
                            }
                            #endregion
                            if (twainDevice != null)
                            {
                                twainDevice.CloseSession();

                            }

                        }
                        else
                        {

                            if (twainDevice != null)
                            {
                                twainDevice.CloseSession();

                            }
                            _ErrorMessage = "Scanner is not connected.";
                            AuditLogErrorMessage(_ErrorMessage);
                            MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {

                        if (twainDevice != null)
                        {
                            twainDevice.CloseSession();

                        }
                        _ErrorMessage = "Scanner is not connected.";
                        AuditLogErrorMessage(_ErrorMessage);
                        MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


            }
            catch (PegasusImaging.WinForms.TwainPro5.TwainProException ex)
            {
                _bStartSession = false;
                if (twainDevice != null)
                {
                    if (openedSession == true)
                    {
                        twainDevice.CloseSession();

                    }
                }

                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);

            }
            catch (Exception ex)
            {

                if (twainDevice != null)
                {
                    if (openedSession == true)
                    {
                        twainDevice.CloseSession();

                    }
                }
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _ScanningInProgress = false;
                if (!gloGlobal.gloRemoteScanSettings.EnableRemoteScan && !gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                {
                    if (c1Documents.Rows.Count > 0)
                    {
                        tlb_Remove.Enabled = true;
                        tlb_RemoveAll.Enabled = true;
                        tlb_RotateBack.Enabled = true;
                        tlb_RotateForward.Enabled = true;
                    }
                    tlb_Scan.Enabled = true;
                    tls_btnScanCard.Enabled = true;

                    tlb_Ok.Enabled = true;
                    tlb_LoadImages.Enabled = true;
                    tlb_Cancel.Enabled = true;
                    tlb_Settings.Enabled = true;

                    tlb_BWFront.Enabled = true;
                    tlb_BWDuplex.Enabled = true;
                    tlb_ColorFront.Enabled = true;
                    tlb_ColorDuplex.Enabled = true;
                    tlb_GrayFront.Enabled = true;
                    tlb_GrayDuplex.Enabled = true;

                    if (twainDevice != null)
                    {
                        if (openedSession == true)
                        {
                            twainDevice.CloseSession();

                        }
                    }
                }
            }
        }

        bool _ImgFocus = false;

        private void EnableTaskBarButtons(bool bIsEnable)
        {
            try
            {
                tlb_Ok.Enabled = bIsEnable;
                tlb_Cancel.Enabled = bIsEnable;
                tlb_Settings.Enabled = bIsEnable;
                tls_btnScanCard.Enabled = bIsEnable;
                //tls_btnScanHalf.Enabled = false;
                tlb_BWFront.Enabled = bIsEnable;
                tlb_BWDuplex.Enabled = bIsEnable;
                tlb_ColorFront.Enabled = bIsEnable;
                tlb_ColorDuplex.Enabled = bIsEnable;
                tlb_GrayFront.Enabled = bIsEnable;
                tlb_GrayDuplex.Enabled = bIsEnable;
                tlb_LoadImages.Enabled = bIsEnable;
                tlb_Remove.Enabled = bIsEnable;
                tlb_RemoveAll.Enabled = bIsEnable;
                tlb_RotateBack.Enabled = bIsEnable;
                tlb_RotateForward.Enabled = bIsEnable;
            }
            catch (Exception)
            {

            }
        }
        public enum ScanningStatus
        {
            Scanning = 0,
            Regetting = 1,
            Waiting=2
        };
        public void RemoteScanning_ImageLoad(string ImgPath, bool bBarcode, gloGlobal.gloProgressAndClipboard objgloProgressAndClipboard, ScanningStatus eStatus)
        {
            if (eStatus == ScanningStatus.Scanning)
            {
                try
                {
                    icnt += 1;
                    //if (!bBarcode)
                    //{
                    //    File.Copy(ImgPath, @"D:\RemoteScan\" + icnt.ToString());
                    //}
                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                    {
                        //if (c1Documents != null)
                        //{
                        if (c1Documents.Styles.Contains("style_Document"))
                        {
                            ostyle_Document = c1Documents.Styles["style_Document"];
                        }
                        else
                        {
                            ostyle_Document = c1Documents.Styles.Add("style_Document");
                            ostyle_Document.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            ostyle_Document.ForeColor = System.Drawing.Color.DarkBlue;
                            ostyle_Document.BackColor = System.Drawing.Color.FromArgb(232, 237, 243);
                        }
                        //}
                        if (bBarcode)//&& barcodes[0].ToString().ToUpper() == "DOCUMENT SEPERATOR"
                        {
                            bIsNewDocument = true;
                            return;
                        }
                        else
                        {
                            if (c1Documents.Rows.Count <= 0 || bIsNewDocument)
                            {
                                c1Documents.Rows.Add();
                                c1Documents.SetData(c1Documents.Rows.Count - 1, COL_NAME, eDocManager.eDocValidator.GetNewDocumentName(oPatientID, oScanInCategory, oClinicID, _OpenExternalSource));
                                c1Documents.SetData(c1Documents.Rows.Count - 1, COL_PATH, "");
                                c1Documents.SetCellStyle(c1Documents.Rows.Count - 1, COL_NAME, ostyle_Document);

                                bIsNewDocument = false;
                            }

                        }
                    }

                    _nImageCount = _nImageCount + 1;
                    int _rowCount = c1Documents.Rows.Count + 1;

                    //To increment the count each time when image gets scanned
                    nCountImageNo++;
                    AddImageFileIntoGridForRemoteScan(Path.GetFileNameWithoutExtension(ImgPath), ImgPath);
                    //End code add
                    //Zoom();//For Intiallially Zoom to the Best Fit
                    if (c1Documents != null)
                    {
                        if (c1Documents.RowSel >= 0)
                        {
                            if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
                            {

                                string _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                if (File.Exists(_filePath) == true)
                                {
                                    if (!_ImgFocus || c1Documents.Rows.Count == 1)
                                    {
                                        //LoadImage(_filePath);
                                        LoadImageWithoutFlickering(_filePath);

                                        _ImgFocus = true;
                                    }
                                }
                                else
                                {
                                    imageControl1._CurrZoomIndex = 9;
                                }
                            }
                            else
                            {
                                imageControl1._CurrZoomIndex = 9;
                            }
                        }
                    }
                    UpdateScanProgressBar(objgloProgressAndClipboard, eStatus);
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                    MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {

                }
            }
            else
            {
                UpdateScanProgressBar(objgloProgressAndClipboard, eStatus);
            }
        }

        private void SetScannersSettings(ScannerSettings objMyScannerSetting, String ScanMode, String ScanDepth, String ScanDuplex)
        {
            #region "Scanner Setting Default Data"



            if (twainDevice != null)
            {
                ComboCollection myCollection;
               
                System.Windows.Forms.ComboBox cmbBox = null;
                myCollection = objMyScannerSetting.GetScannerSettings(ScanMode, ref  twainDevice, AdvancedCapability.IcapPixelType, ref  cmbBox);
                objMyScannerSetting.SetScannerSettings(ref twainDevice, AdvancedCapability.IcapPixelType, myCollection);
               
                myCollection = objMyScannerSetting.GetScannerSettings(ScanDepth, ref  twainDevice, AdvancedCapability.IcapBitDepth, ref  cmbBox);
                objMyScannerSetting.SetScannerSettings(ref twainDevice, AdvancedCapability.IcapBitDepth, myCollection);
              

                //Resolution
                PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat myUnitsCaps = new CapabilityContainerOneValueFloat(AdvancedCapability.IcapUnits);
                if (myUnitsCaps != null)
                {
                    if (twainDevice.IsCapabilitySupported(AdvancedCapability.IcapUnits) == true)
                    {
                        myUnitsCaps.Value = (float)PegasusImaging.WinForms.TwainPro5.CapabilityConstants.TwunInches;
                        twainDevice.SetCapability(myUnitsCaps);
                    }
                }
                myCollection = objMyScannerSetting.GetScannerSettings(ref  twainDevice, AdvancedCapability.IcapXResolution, ref  cmbBox, gloRegistrySetting.gstrDMSResol);
                objMyScannerSetting.SetScannerSettings(ref twainDevice, AdvancedCapability.IcapXResolution, myCollection);
                objMyScannerSetting.SetScannerSettings(ref twainDevice, AdvancedCapability.IcapYResolution, myCollection);

                

                //Brightness
                if (twainDevice.IsCapabilitySupported(Capability.IcapBrightness) == true)
                {
                    myCollection = objMyScannerSetting.GetScannerSettings(ref  twainDevice, AdvancedCapability.IcapBrightness, ref  cmbBox, gloRegistrySetting.gstrDMSBright);
                    //myCollection = SetScannerSetting(Capability.IcapBrightness, gloRegistrySetting.gstrDMSBright);

                }
                else
                {
                    cmbBrightness.Items.Add(Common.Scanner.ScanContrastBrightness.CB_96);
                    myCollection = new ComboCollection("96", -32);

                }
                objMyScannerSetting.SetScannerSettings(ref twainDevice, AdvancedCapability.IcapBrightness, myCollection);

                //ApplyToScanner(Capability.IcapBrightness, myCollection);
                //Contrast
                if (twainDevice.IsCapabilitySupported(Capability.IcapContrast) == true)
                {
                    myCollection = objMyScannerSetting.GetScannerSettings(ref  twainDevice, AdvancedCapability.IcapContrast, ref  cmbBox, gloRegistrySetting.gstrDMSContrast);
                    //myCollection = SetScannerSetting(Capability.IcapContrast, gloRegistrySetting.gstrDMSContrast);
                }
                else
                {
                    myCollection = new ComboCollection("96", -32);
                }
                objMyScannerSetting.SetScannerSettings(ref twainDevice, AdvancedCapability.IcapContrast, myCollection);

                

                if (ScanDuplex.Trim() == "Duplex")
                {
                    myCollection = new ComboCollection("Duplex", 1);
                }
                else
                {
                    myCollection = new ComboCollection("Front Side", 0);
                }
                objMyScannerSetting.SetScannerSettings(ref twainDevice, AdvancedCapability.CapDuplexEnabled, myCollection);


                //SupportedSize
                myCollection = objMyScannerSetting.GetScannerSettings(ref  twainDevice, AdvancedCapability.IcapSupportedSizes, ref  cmbBox, gloRegistrySetting.gstrDMSupporedSize);
                objMyScannerSetting.SetScannerSettings(ref twainDevice, AdvancedCapability.IcapSupportedSizes, myCollection);



                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                {
                    return;
                }

                object oSetting_DMSShowScannerDiaglogbox = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSShowScann);
                gloRegistrySetting.CloseRegistryKey();
                twainDevice.ShowUserInterface = Convert.ToBoolean(oSetting_DMSShowScannerDiaglogbox);
                //twainDevice.ImageLayout = new System.Drawing.RectangleF(0.0f,0.0f,8.0f,11.0f);
               // int a = twainDevice.MaximumImages;
 
            }

            #endregion

        }
        
        private void tls_btnScanCard_Click(object sender, EventArgs e)
        {
            try
            {
                if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan || gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                {
                    nCurrScanCnt = 0;
                    CallScanningForRemoteScan(null,null,"Scan Card",null);
                }
                else 
                {
                    _ScanningInProgress = true;
                    // tlb_Scan.Enabled = false;
                    tlb_Ok.Enabled = false;
                    tlb_Cancel.Enabled = false;
                    tlb_Settings.Enabled = false;

                    tls_btnScanCard.Enabled = false;
                    //tls_btnScanHalf.Enabled = false;

                    tlb_BWFront.Enabled = false;
                    tlb_BWDuplex.Enabled = false;
                    tlb_ColorFront.Enabled = false;
                    tlb_ColorDuplex.Enabled = false;
                    tlb_GrayFront.Enabled = false;
                    tlb_GrayDuplex.Enabled = false;

                    tlb_LoadImages.Enabled = false;
                    _IsScanCard = true;

                    ScannerSettings objScannerSettings = new ScannerSettings();
                    bool openedSession = false;
                    if (objScannerSettings == null)
                    {
                        return;
                    }

                    System.Windows.Forms.ComboBox cmbBox = null;
                    _IsScannerConnected = objScannerSettings.GetAndSetScanners(ref twainDevice, ref cmbBox, gloRegistrySetting.gstrDMSScan); //Setting  up the Scanner//AddScanner(gloRegistrySetting.gstrDMSScan);
                    //twainDevice.CloseSession();
                    try
                    {
                        if (_IsScannerConnected == true)
                        {
                            twainDevice.OpenSession();
                            openedSession = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                        openedSession = false;

                    }
                    if (openedSession == true)
                    {
                        if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                        {
                            return;
                        }
                        //Start/GLO2010-0007876 :: -  Dms ScanCard releated issue.
                        object myRegDMSScanMode = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanMode);
                        object myRegDMSScanBitDepth = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanDepth);
                        object myRegDMSScanDuplex = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanSide);
                        //EndGLO2010-0007876 :: -  Dms ScanCard releated issue.
                        gloRegistrySetting.CloseRegistryKey();
                        String ScanMode = "Black & White";
                        String ScanBitDepth = "1";
                        String ScanDuplex = "Front Side";
                        if (myRegDMSScanMode != null)
                        {
                            ScanMode = myRegDMSScanMode.ToString();

                        }
                        if (myRegDMSScanBitDepth != null)
                        {
                            ScanBitDepth = myRegDMSScanBitDepth.ToString();

                        }
                        if (myRegDMSScanDuplex != null)
                        {
                            ScanDuplex = myRegDMSScanDuplex.ToString();

                        }
                        SetScannersSettings(objScannerSettings, ScanMode, ScanBitDepth, ScanDuplex);

                        float _CardWidth = 4.0F;
                        float _CardLength = 4.0F;
                        if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                        {
                            return;
                        }

                        object myRegDMSCardWidth = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardWidth);
                        object myRegDMSCardLength = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardLength);
                        object myRegDMSCardX = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardLeftX);
                        object myRegDMSCardY = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardTopY);
                        gloRegistrySetting.CloseRegistryKey();

                        if (myRegDMSCardWidth.ToString().Trim() == String.Empty || myRegDMSCardWidth.ToString().Trim() == "")
                        {
                            myRegDMSCardWidth = _CardWidth;

                        }
                        if (myRegDMSCardLength.ToString().Trim() == String.Empty || myRegDMSCardLength.ToString().Trim() == "")
                        {
                            myRegDMSCardLength = _CardLength;

                        }
                        if (myRegDMSCardX.ToString().Trim() == String.Empty || myRegDMSCardX.ToString().Trim() == "")
                        {
                            myRegDMSCardX = 0;

                        }
                        if (myRegDMSCardY.ToString().Trim() == String.Empty || myRegDMSCardY.ToString().Trim() == "")
                        {
                            myRegDMSCardY = 0;

                        }

                        _CardWidth = (float)(Convert.ToDouble(myRegDMSCardWidth));
                        _CardLength = (float)(Convert.ToDouble(myRegDMSCardLength));
                        float X = (float)(Convert.ToDouble(myRegDMSCardX));
                        float Y = (float)(Convert.ToDouble(myRegDMSCardY));


                        // bool _isCardValidated = false;
                        if (objScannerSettings != null)
                        {

                            objScannerSettings.GetAndSetScannerSettingsSize(ref twainDevice, ref _CardWidth, ref _CardLength);

                        }

                        twainDevice.ImageLayout = new System.Drawing.RectangleF(X, Y, _CardWidth, _CardLength);



                        _bStartSession = true;
                        twainDevice.StartSession();
                        _bStartSession = false;
                        #region " Load File "
                        bool zoomNotDone = true;

                        if (c1Documents != null)
                        {
                            //  if (c1Documents.RowSel != null)
                            {
                                if (c1Documents.RowSel >= 0)
                                {
                                    if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
                                    {
                                        string _filePath = "";
                                        _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                                        if (File.Exists(_filePath) == true)
                                        {
                                            //LoadImage(_filePath);
                                            LoadImageWithoutFlickering(_filePath);
                                            zoomNotDone = false;

                                        }
                                    }
                                }
                            }
                        }
                        if (zoomNotDone)
                        {
                            Zoom();
                        }
                        #endregion
                        if (twainDevice != null)
                        {
                            twainDevice.CloseSession();

                        }

                    }
                    else
                    {
                        if (twainDevice != null)
                        {

                            twainDevice.CloseSession();

                        }
                        _ErrorMessage = "Scanner is not connected.";
                        AuditLogErrorMessage(_ErrorMessage);
                        MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

            }
            catch (PegasusImaging.WinForms.TwainPro5.TwainProException ex)
            {
                _bStartSession = false;
                if (twainDevice != null)
                {

                    twainDevice.CloseSession();
                    
                }
                _ErrorMessage = "Scanner is not connected.";
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);

            }
            catch (Exception ex)
            {
                if (twainDevice != null)
                {

                    twainDevice.CloseSession();
                    
                }
                _ErrorMessage = "Scanner is not connected.";
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _ScanningInProgress = false;
                if (!gloGlobal.gloRemoteScanSettings.EnableRemoteScan && !gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                {
                    if (c1Documents.Rows.Count > 0)
                    {
                        tlb_Remove.Enabled = true;
                        tlb_RemoveAll.Enabled = true;
                        tlb_RotateBack.Enabled = true;
                        tlb_RotateForward.Enabled = true;
                    }
                    tlb_Scan.Enabled = true;
                    tls_btnScanCard.Enabled = true;
                    tlb_Ok.Enabled = true;
                    tlb_LoadImages.Enabled = true;
                    tlb_Cancel.Enabled = true;
                    tlb_Settings.Enabled = true;

                    tlb_BWFront.Enabled = true;
                    tlb_BWDuplex.Enabled = true;
                    tlb_ColorFront.Enabled = true;
                    tlb_ColorDuplex.Enabled = true;
                    tlb_GrayFront.Enabled = true;
                    tlb_GrayDuplex.Enabled = true;
                    if (twainDevice != null)
                    {

                        twainDevice.CloseSession();

                    }
                }
            }
        }

        public int imgCnt = 0;
        Int16 attemptsToGetClipboard = 0;
        private static gloGlobal.gloClipboardWatcher myWatcher = null;
		//private static bool bInsideScan = false;

        private void UpdateScanProgressBar(gloGlobal.gloProgressAndClipboard objgloProgressAndClipboard, ScanningStatus sStatus)
        {
            if (objfrmScanProgress != null)
            {
                if (objgloProgressAndClipboard != null)
                {
                    int totpagecnt = 0;
                    int statuscode = 0;
                    if (objgloProgressAndClipboard.objgloDataProgressBar.totalcnt > 0)
                    {
                        int totalcnt = objgloProgressAndClipboard.objgloDataProgressBar.totalcnt & gloGlobal.gloDataProgressBar.PAGESMASK;
                        if (totalcnt > 0)
                        {
                            totpagecnt = totalcnt - 1;
                        }
                        
                        statuscode = objgloProgressAndClipboard.objgloDataProgressBar.totalcnt & gloGlobal.gloDataProgressBar.STAUSMASK;
                    }
                    //if (bAgain)
                    //{
                    //objfrmScanProgress.lblCurrentProgress.Text = sStatus.ToString() + " " + objgloProgressAndClipboard.objgloDataProgressBar.sequenceNo.ToString() + " of " + totpagecnt.ToString();
                    if (statuscode > 0 && sStatus == ScanningStatus.Scanning)
                    {
                        objfrmScanProgress.lblCurrentProgress.Text = "Downloading " + nCurrScanCnt.ToString() + " of " + totpagecnt.ToString();
                    }
                    else
                    {
                        objfrmScanProgress.lblCurrentProgress.Text = sStatus.ToString() + " " + nCurrScanCnt.ToString() + " of " + totpagecnt.ToString();
                    }
                    //}
                    //else
                    //{
                    //    objfrmScanProgress.lblCurrentProgress.Text = "Scanning " + objgloProgressAndClipboard.objgloDataProgressBar.sequenceNo.ToString() + " of " + totpagecnt.ToString();
                    //}
                    objfrmScanProgress.prbarScanning.Maximum = totpagecnt;
                    if (nCurrScanCnt > totpagecnt)
                    {
                        nCurrScanCnt = totpagecnt;
                    }
                    objfrmScanProgress.prbarScanning.Value = nCurrScanCnt;//objgloProgressAndClipboard.objgloDataProgressBar.sequenceNo;
                }
                else
                {
                    objfrmScanProgress.lblCurrentProgress.Text = sStatus.ToString();
                }
                objfrmScanProgress.Refresh();
                objfrmScanProgress.BringToFront();
                Application.DoEvents();
            }
        }

        private static string nextFileName = "";
        private static gloGlobal.clsDatalog logdata = new gloGlobal.clsDatalog("gloTSPrint");
        void myWatcher_OnClipboardContentChanged(object sender, EventArgs e)
        {
            //AuditLogErrorMessage("myWatcher_OnClipboardContentChanged: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            if (ScanTimer != null)
            {
                ScanTimer.Enabled = false;
                ScanTimer.Stop();
            }
            string sExtForScan = string.Empty;

            string sImgTempPath = Path.Combine(_ScanImagesTempFolderPath, "Image - " + (imgCnt + 1).ToString("000#"));
            int nCurrProcId = gloGlobal.gloProgressAndClipboard.getProcessIdInt();
            logdata.Log("myWatcher_OnClipboardContentChanged executed for sImgTempPath : " + sImgTempPath);
            //AuditLogErrorMessage("BEFORE GetImageFromClipboard" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            gloGlobal.gloProgressAndClipboard objgloProgressAndClipboard = gloGlobal.gloProgressAndClipboard.GetImageFromClipboard(3, sImgTempPath, ref sExtForScan, nCurrProcId);
            if (objgloProgressAndClipboard != null)
            {
                logdata.Log("objgloProgressAndClipboard object created for sImgTempPath : " + sImgTempPath + " sExtForScan : " + sExtForScan);
                //AuditLogErrorMessage("TOTAL COUNTS :  " + objgloProgressAndClipboard.objgloDataProgressBar.sequenceNo + " , " + objgloProgressAndClipboard.objgloDataProgressBar.totalcnt);
                //AuditLogErrorMessage("AFTER GetImageFromClipboard -- >" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                //AuditLogErrorMessage("sExtForScan: " + sExtForScan);
                if (objgloProgressAndClipboard.objgloDataClipboardCopy != null)
                {
                    if (sExtForScan == ".bar")
                    {
                        nCurrScanCnt++;
                        //using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + nCurrScanCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".next")))
                        //{
                        //    myFile.Close();
                        //    // interact with myFile here, it will be disposed automatically
                        //}

                        //if (File.Exists(sImgTempPath + sExtForScan))
                        //{
                        RemoteScanning_ImageLoad(null, true, objgloProgressAndClipboard, ScanningStatus.Scanning);//sImgTempPath + sExtForScan
                        bScannerStatus = true;
                        
                        GetScannedFile(".next");
                        //}
                    }
                    else
                    {
                        if (sExtForScan == ".end")
                        {
                            ReleaseScanTimer();
                        }
                        else if (sExtForScan == ".err")
                        {
                            ReleaseScanTimer(bAddFailLog: true);
                        }
                        else
                        {
                            if (sExtForScan == ".wait")
                            {
                                RemoteScanning_ImageLoad(null, false, objgloProgressAndClipboard, ScanningStatus.Waiting);
                                GetScannedFile(".next");
                            }
                            else
                            {
                                imgCnt++;
                                nCurrScanCnt++;
                                //AuditLogErrorMessage("Before Creating .Next file " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                                //using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + nCurrScanCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".next")))
                                //{
                                //    myFile.Close();
                                //    // interact with myFile here, it will be disposed automatically
                                //}

                                if (File.Exists(sImgTempPath + sExtForScan))
                                {

                                    RemoteScanning_ImageLoad(sImgTempPath + sExtForScan, false, objgloProgressAndClipboard, ScanningStatus.Scanning);
                                    bScannerStatus = true;
                                }
                                else
                                { imgCnt--; }
                                //UpdateScanProgressBar(objgloProgressAndClipboard, false);
                                int totcount = objgloProgressAndClipboard.objgloDataProgressBar.totalcnt & gloGlobal.gloDataProgressBar.PAGESMASK;
                                int scanstatus = objgloProgressAndClipboard.objgloDataProgressBar.totalcnt & gloGlobal.gloDataProgressBar.STAUSMASK;
                                if ((scanstatus > 0) && ((totcount - 1) <= nCurrScanCnt) && (totcount > 0))
                                {
                                    if (scanstatus == gloGlobal.gloDataProgressBar.SCANEND)
                                    {
                                        ReleaseScanTimer();
                                    }
                                    else
                                    {
                                        ReleaseScanTimer(bAddFailLog: true);
                                    }
                                    GetScannedFile(".over");

                                }
                                else
                                {
                                    GetScannedFile(".next");
                                }
                            }
                        }
                    }
                }
                else
                {
                    RemoteScanning_ImageLoad(null, false, objgloProgressAndClipboard, ScanningStatus.Waiting);
                    //UpdateScanProgressBar with some other process to finalize scanning waiting;
                }
                attemptsToGetClipboard = 0;
            }
            else
            {
                //using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + nCurrScanCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".again")))
                //{
                //    myFile.Close();
                //    // interact with myFile here, it will be disposed automatically
                //}
                //UpdateScanProgressBar(objgloProgressAndClipboard,true);
                RemoteScanning_ImageLoad(null, false, objgloProgressAndClipboard, ScanningStatus.Regetting);

                GetScannedFile(".again");
            }
            if (ScanTimer != null)
            {
                ScanTimer.Enabled = true;
                ScanTimer.Start();
            }

        }

        private void GetScannedFile(string sExt)
        {
            String sFileName = Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + nCurrScanCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + sExt);
            nextFileName = Path.GetFileNameWithoutExtension(sFileName);
            using (var myFile = File.Create(sFileName))
            {
                myFile.Close();
                // interact with myFile here, it will be disposed automatically
            }
        }

        private static frmScanProgress objfrmScanProgress = null;
        //public string PerformRemoteScan(string aScannedFilePath, string sExt)//ref CardScanSettingsScanCardSettings cardScanSettings
        //{
        //    string sResult = string.Empty;
        //    //attemptsToGetClipboard = 0;
        //    ////String ScanCardRequestFile = gloGlobal.gloTSPrint.TempPath + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xml";
        //    ////gloRemoteScanMetaDataWriter.CreateXMLFile(cardScanSettings, ScanCardRequestFile);
        //    ////String referenceFileName = Path.GetFileName(ScanCardRequestFile);

        //    ////AuditLogErrorMessage("PerformRemoteScan myWatcher: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
        //    //if(myWatcher==null)
        //    //{
        //    //   // AuditLogErrorMessage("myWatcher==null" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
        //    //    myWatcher = gloGlobal.gloDataClipboardCopy.getClipboardWatcher();
        //    //    //myWatcher.Start();
        //    //}
        //    //if (myWatcher != null)
        //    //{ 
        //    //    myWatcher.OnClipboardContentChanged += new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
        //    //}

            
        //    objfrmScanProgress = new frmScanProgress();
        //    objfrmScanProgress.BringToFront();
        //    objfrmScanProgress.lblCurrentProgress.Text = "Started Remote Scanning...";
        //    objfrmScanProgress.prbarScanning.Maximum = 1;
        //    objfrmScanProgress.prbarScanning.Value = 0;
        //    objfrmScanProgress.Show();
           
        //   // AuditLogErrorMessage("OUT of PerformRemoteSCan : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
        //    return sResult;
        //}
        private static Timer ScanTimer=null;

        private void ScanTimerFired(object sender, EventArgs e)
        {
            //AuditLogErrorMessage("ScanTimerFired");
            //AuditLogErrorMessage("attempts : " + attempts);
            ScanTimer.Enabled = false;

            if (attemptsToGetClipboard < 50)
            {
                attemptsToGetClipboard++;
                if ((attemptsToGetClipboard % 10 == 0) || (nextFileName != "" && File.Exists(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, nextFileName + ".pushed"))))
                {
                    GetScannedFile(".again");
                    //using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + nCurrScanCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".again")))
                    //{
                    //    myFile.Close();
                    //    // interact with myFile here, it will be disposed automatically
                    //}
                }
                ScanTimer.Enabled = true;

            }
            else
            {
                if (attemptsToGetClipboard != 50)
                {
                    ScanTimer.Enabled = true;
                }
                else
                {
                    attemptsToGetClipboard = 0;
                    ReleaseScanTimer(bAddFailLog: true);
                }
            }
            if (objfrmScanProgress != null)
            {
                objfrmScanProgress.BringToFront();
            }
        }
        private static bool bScannerStatus = false;
        private void StartScanTimer()
        {
            bScannerStatus = false;
            if (ScanTimer == null)
            {
                ScanTimer = new Timer();
                ScanTimer.Interval = 2000;//5000;
                ScanTimer.Tick += ScanTimerFired;
            }
            if (ScanTimer != null)
            {
                ScanTimer.Enabled = true;
            }
            attemptsToGetClipboard = 0;
            if (myWatcher == null)
            {
                myWatcher = gloGlobal.gloProgressAndClipboard.getClipboardWatcher();
            }
            if (myWatcher != null)
            {
                myWatcher.OnClipboardContentChanged += new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
                bReleaseClipboardEvents = false;
            }
            if (objfrmScanProgress == null)
            {
                objfrmScanProgress = new frmScanProgress();
                objfrmScanProgress.BringToFront();
                objfrmScanProgress.lblCurrentProgress.Text = "Started Local Scanning...";
                objfrmScanProgress.prbarScanning.Maximum = 1;
                objfrmScanProgress.prbarScanning.Value = 0;
            }
            if (objfrmScanProgress != null)
            {
                objfrmScanProgress.Show();
            }
            EnableTaskBarButtons(false);
            Application.DoEvents();
        }
        private bool bReleaseClipboardEvents = false;
        private void ReleaseScanTimer(bool bAddFailLog = false)
        {
            if (objfrmScanProgress != null)
            {
                objfrmScanProgress.lblCurrentProgress.Text = "Closing...";
                objfrmScanProgress.Refresh();
                objfrmScanProgress.BringToFront();
                Application.DoEvents();
            }
            if (!bScannerStatus)
            {
                if (bAddFailLog)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Save, "Failure", gloAuditTrail.ActivityOutCome.Failure);
                }
            }
            else
            {
                bScannerStatus = false;
            }
            if (ScanTimer != null)
            {
                ScanTimer.Enabled = false;
                ScanTimer.Stop();

                ScanTimer.Tick -= ScanTimerFired;
                ScanTimer.Dispose();
                ScanTimer = null;
            }
            if (myWatcher != null && !bReleaseClipboardEvents)
            {
                try
                {
                    myWatcher.OnClipboardContentChanged -= new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
                    bReleaseClipboardEvents = true;
                }
                catch
                {
                }
            }

            if (objfrmScanProgress != null)
            {
                objfrmScanProgress.Dispose();
                objfrmScanProgress = null;
            }

            EnableTaskBarButtons(true);
            Application.DoEvents();
        }
        string sScanImageFormat;
        private void CallScanningForRemoteScan(string ScanMode, string ScanSide, string ScanType, string ScanDepth)
        {
            if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
            {
                if (!gloGlobal.gloTSPrint.isLocalMachineUpdated())
                {
                    return;
                }
                if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                {
                    return;
                }
            }
            try
            {
                Clipboard.Clear();
            }
            catch
            { }
            gloEDocumentV3.Common.RemoteScanCommon oRemoteScanCommon = new Common.RemoteScanCommon();
            //AuditLogErrorMessage("CallScanningForRemoteScan");
            try
            {
                string sRetVal = "";
                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                {
                    sRetVal = oRemoteScanCommon.SetRemoteScannerCurrentSettings(ScanMode, ScanSide, ScanType, "RCM", ScanDepth);
                }
                else
                {
                    //if (ScanType != "Scan Card")
                    //{
                    //    ScanType = "DMS";
                    //}
                    //AuditLogErrorMessage("Before SetRemoteScannerCurrentSettings : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                    sRetVal = oRemoteScanCommon.SetRemoteScannerCurrentSettings(ScanMode, ScanSide, ScanType, "DMS", ScanDepth);
                    //AuditLogErrorMessage("After SetRemoteScannerCurrentSettings : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                }

                //AuditLogErrorMessage("sRetVal --> " + sRetVal);
                if (!string.IsNullOrEmpty(sRetVal))
                { 
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Save, sRetVal, gloAuditTrail.ActivityOutCome.Failure); 
                }
                else
                {
                    if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                    {
                        StartScanTimer();
                        // AuditLogErrorMessage("Before CreateCurrentSettingsForRemoteScan : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));

                        string sScannedFilePath = string.Empty;
                        try
                        {
                            sScannedFilePath = gloRemoteScanGeneral.RemoteScanSettings.CreateCurrentSettingsForRemoteScan();
                            if (string.IsNullOrEmpty(sScannedFilePath))
                            {
                                gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                                if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                                {
                                    ReleaseScanTimer();
                                    return;
                                }
                                else
                                {
                                    ReleaseScanTimer();
                                    MessageBox.Show("Unable to start a scan request . Please check whether gloLDSSniffer Service is running and local scan setting is enabled in service", "Local scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                nextFileName = Path.GetFileNameWithoutExtension(sScannedFilePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            AuditLogErrorMessage(ex.ToString());
                            gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                            if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                            {
                                ReleaseScanTimer();
                                return;
                            }
                        }
                    }
                    else if (gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                    {
                        //AuditLogErrorMessage("bEliminatePegasus true");
                        //if (!bLocalScanningDone)
                        //{
                        if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                        {
                            return;
                        }
                        object oScanImageFormat = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrScanImageFormat);
                        sScanImageFormat = Convert.ToString(oScanImageFormat).Trim();
                        //AuditLogErrorMessage("oScanImageFormat : " + sScanImageFormat);
                        objLocalTwainScanLauncher = new frmLocalTwainScanLauncher(gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting);
                        //AuditLogErrorMessage("After objLocalTwainScanLauncher");
                        if (ScanType == "Scan Card")
                        {
                            objLocalTwainScanLauncher.bFromCardScan = true;
                            objLocalTwainScanLauncher.bFromScanDocCardScan = true;
                        }
                        else
                        {
                            objLocalTwainScanLauncher.bFromCardScan = false;
                            objLocalTwainScanLauncher.bFromScanDocCardScan = false;
                        }
                        EnableTaskBarButtons(false);
                        //AuditLogErrorMessage("After EnableTaskBarButtons");
                        Application.DoEvents();
                        if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScannerName.StartsWith("WIA"))
                        {
                            //objLocalTwainScanLauncher.WIAScanning();
                            objLocalTwainScanLauncher.IsWIAScanning = true;
                            objLocalTwainScanLauncher.OnForEachWIAImageScanned += new frmLocalTwainScanLauncher.ForEachWIAImageScanned(objLocalTwainScanLauncher_OnForEachWIAImageScanned);
                            objLocalTwainScanLauncher.OnWIAImageScanningDone += new frmLocalTwainScanLauncher.WIAImageScanningDone(objLocalTwainScanLauncher_OnWIAImageScanningDone);    
                        }
                        else
                        {
                            objLocalTwainScanLauncher.IsWIAScanning = false;
                            objLocalTwainScanLauncher.OnForEachImageScanned += new frmLocalTwainScanLauncher.ForEachImageScanned(objLocalTwainScanLauncher_OnForEachImageScanned);
                            objLocalTwainScanLauncher.OnImageScanningDone += new frmLocalTwainScanLauncher.ImageScanningDone(objLocalTwainScanLauncher_OnImageScanningDone);
                            //bLocalScanningDone = false;
                            //AuditLogErrorMessage("Before EnableTaskBarButtons");
                        }
                        objLocalTwainScanLauncher.ShowDialog();
                        //while (bLocalScanningDone == false)
                        //{
                        //    System.Threading.Thread.Sleep(100);
                        //}
                        if (bSuccess)
                        {
                        }
                        else
                        {
                            MessageBox.Show(sScanStatus);
                        }

                        EnableTaskBarButtons(true);
                        Application.DoEvents();

                        //    bLocalScanningDone = false;
                        //}

                    }
                    //AuditLogErrorMessage("After CreateCurrentSettingsForRemoteScan : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                    //AuditLogErrorMessage("After XML File creation");

                    //string sExt = ".png";
                    //if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting != null)
                    //{
                    //    //if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanModeName == "BW")
                    //    //{
                    //    //    sExt = ".png";
                    //    //}
                    //    //else 
                    //    {
                    //        if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanModeName != "BW")
                    //        {
                    //            sExt = ".jpeg";
                    //        }
                    //    }
                    //}

                    //string sRet = PerformRemoteScan(sScannedFilePath, sExt);

                    //if (!string.IsNullOrEmpty(sRet))
                    //{ gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Save, sRet, gloAuditTrail.ActivityOutCome.Failure); }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 

            }
            finally
            {
                oRemoteScanCommon = null;
            }
        }

        void objLocalTwainScanLauncher_OnWIAImageScanningDone(object sender, gloWIAScanningDoneEventArgs e)
        {
            bSuccess = e.bSuccess;
            sScanStatus = e.sScanStatus;
        }

        void objLocalTwainScanLauncher_OnForEachWIAImageScanned(object sender, gloWIAScanControllerEventArgs e)
        {
            string sBarcode = e.sBarcode;
            Image iScannedImage = e.ScannedImg;
            string sExt = e.sExt;

            try
            {
                DisplayImgInGrid(sBarcode, sExt, iScannedImage);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }

        }

       // volatile bool bLocalScanningDone = false;
        bool bSuccess = false;
        string sScanStatus = null;

        void objLocalTwainScanLauncher_OnImageScanningDone(object sender, gloTwainScanningDoneEventArgs e)
        {
            //bLocalScanningDone = true;
            bSuccess = e.bSuccess;
            sScanStatus = e.sScanStatus;
        }

        public void DisplayImgInGrid(string sBarcode, string sExt, Image iScannedImage)
        {
            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
            {
                if (c1Documents.Styles.Contains("style_Document"))
                {
                    ostyle_Document = c1Documents.Styles["style_Document"];
                }
                else
                {
                    ostyle_Document = c1Documents.Styles.Add("style_Document");
                    ostyle_Document.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    ostyle_Document.ForeColor = System.Drawing.Color.DarkBlue;
                    ostyle_Document.BackColor = System.Drawing.Color.FromArgb(232, 237, 243);

                }
                if (!string.IsNullOrEmpty(sBarcode) && sBarcode.ToUpper() == "RCM")
                {
                    bIsNewDocument = true;
                    return;
                }
                else
                {
                    if (c1Documents.Rows.Count <= 0 || bIsNewDocument)
                    {
                        c1Documents.Rows.Add();
                        c1Documents.SetData(c1Documents.Rows.Count - 1, COL_NAME, eDocManager.eDocValidator.GetNewDocumentName(oPatientID, oScanInCategory, oClinicID, _OpenExternalSource));
                        c1Documents.SetData(c1Documents.Rows.Count - 1, COL_PATH, "");
                        c1Documents.SetCellStyle(c1Documents.Rows.Count - 1, COL_NAME, ostyle_Document);

                        bIsNewDocument = false;
                    }
                }
            }


            _nImageCount = _nImageCount + 1;
            int _rowCount = c1Documents.Rows.Count + 1;

            //To increment the count each time when image gets scanned
            nCountImageNo++;
            //Call the function to count unique loaded or scanned image no
            int _Count = imageCount();
            //End code add

            string _ImageFilePath = "";
            System.Drawing.Imaging.ImageFormat ImgFrm = null;
            //AuditLogErrorMessage("sScanImageFormat : " + sScanImageFormat);
            if (sScanImageFormat == "Default")
            {
                //AuditLogErrorMessage("(e.sExt : " + e.sExt);
                //GLO2011-0012086- (Placed the or condition for pixeltype as palette)When attempting to scan a card with card location left and top value of 22.001 nothing happens. At a value of 0 for top and left the scan occurs but displays a message 'Error writing file' after scan finishes. 
                if (sExt == ".png")
                {

                    //Added By Shweta 20090926
                    _ImageFilePath = _ScanImagesTempFolderPath + "\\Image - " + _Count.ToString("000#") + ".png";
                    //End code add
                    ImgFrm = System.Drawing.Imaging.ImageFormat.Png;
                    //_SaveOption.Format = ScannedImageFormat.;
                    //_SaveOption.Jpeg.Chrominance = 60;// 30;
                    //_SaveOption.Jpeg.Luminance = 48;// 24;
                }
                else if (sExt == ".jpeg")
                {

                    //Added By Shweta 20090926
                    _ImageFilePath = _ScanImagesTempFolderPath + "\\Image - " + _Count.ToString("000#") + ".jpeg";
                    //End code add
                    ImgFrm = System.Drawing.Imaging.ImageFormat.Jpeg;
                    //_SaveOption.Format = ScannedImageFormat.Tiff;
                    //_SaveOption.Tiff.Compression = TiffCompression.Rle;
                }
            }
            else
            {
                switch (sScanImageFormat)
                {

                    case "Bmp":
                        ImgFrm = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                    case "Jpeg":
                        ImgFrm = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case "Png":
                        ImgFrm = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                    case "Tiff":
                        ImgFrm = System.Drawing.Imaging.ImageFormat.Tiff;
                        break;
                    case "Tif":
                        ImgFrm = System.Drawing.Imaging.ImageFormat.Tiff;
                        break;
                    default:
                        ImgFrm = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                }

                _ImageFilePath = _ScanImagesTempFolderPath + "\\Image - " + _Count.ToString("000#") + "." + sScanImageFormat;
            }
            //AuditLogErrorMessage("_ImageFilePath : " + _ImageFilePath);
            iScannedImage.Save(_ImageFilePath, ImgFrm);
            //AuditLogErrorMessage("AFTER SAVE " );
            // AuditLogErrorMessage("Before Adding image to grid : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            AddImageFileIntoGrid("Image - " + _Count.ToString("000#"), _ImageFilePath);
            //End code add
            // AuditLogErrorMessage("After Adding image to grid : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            Zoom();//For Intiallially Zoom to the Best Fit

            if (c1Documents != null)
            {
                if (c1Documents.RowSel >= 0)
                {
                    if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
                    {
                        string _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
                        if (File.Exists(_filePath) == true)
                        {
                            if (!_ImgFocus || c1Documents.Rows.Count == 1)
                            {
                                //LoadImage(_filePath);
                                LoadImageWithoutFlickering(_filePath);
                                _ImgFocus = true;
                            }
                        }
                    }
                }
            }

            Application.DoEvents();
        }

        public void objLocalTwainScanLauncher_OnForEachImageScanned(object sender, gloTwainScanControllerEventArgs e)
        {
            string sBarcode = e.sBarcode;
            Image iScannedImage =  e.ScannedImg;
            string sExt = e.sExt;
            //AuditLogErrorMessage("In objLocalTwainScanLauncher_OnForEachImageScanned");
            try
            {
                DisplayImgInGrid(sBarcode, sExt, iScannedImage);   
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 

            }

        }
   

        #endregion "Scanning new functionality"

        private void GetRemoteScannerSetting()
        {      
            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
            {
                return;
            }
            object DmsScannerName = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScan);
            if (DmsScannerName.ToString().Trim() == "RemoteScan(TM)")
            {
                twainDevice.ImageLayout = new System.Drawing.RectangleF(0.0F,0.0F, 8.5F, 11.0F);
            }
            gloRegistrySetting.CloseRegistryKey();
        }

        private void imageControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                imageControl1.ContextMenu = null;
                imageControl1.ContextMenuStrip = null;
                //imageXView.ContextMenu = null;
                //imageXView.ContextMenuStrip = null;
            }
        }

        private void imageControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                imageControl1.ContextMenu = null;
                imageControl1.ContextMenuStrip = null;
            }
        }

        private void imageControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                imageControl1.ContextMenu = null;
                imageControl1.ContextMenuStrip = null;
            }
        }
       
        private void btnPan_Click(object sender, EventArgs e)
        {
            //imageControl1.PanButtonClicked(sender);
        }

        private void btnRegionZoom_Click(object sender, EventArgs e)
        {
            //imageControl1.PanButtonClicked(sender);
        }

        private void btnZoomingOut_Click(object sender, EventArgs e)
        {
          //  imageControl1.PanButtonClicked(sender);
            if (imageControl1.CurrImage != null)
            {
                if (imageControl1._CurrZoomIndex > 0)
                {
                    try
                    {
                        cmbZoom.SelectedIndex = imageControl1._CurrZoomIndex - 1;
                        //    imageControl1._CurrZoomIndex=imageControl1._CurrZoomIndex - 1;
                    }
                    catch //(Exception ex)
                    {

                    }
                }
            }
        }

        private void btnZoomingIn_Click(object sender, EventArgs e)
        {
            //imageControl1.PanButtonClicked(sender);
            if (imageControl1.CurrImage != null)
            {
                if ((cmbZoom.Items.Count - 1) > imageControl1._CurrZoomIndex)
                {
                    try
                    {
                        cmbZoom.SelectedIndex = imageControl1._CurrZoomIndex + 1;
                        // imageControl1._CurrZoomIndex = imageControl1._CurrZoomIndex + 1;
                    }
                    catch //(Exception ex)
                    {

                    }
                }
            }
        }

        private void cmbZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZoom.SelectedIndex >= 0)
            {
                if (imageControl1.CurrImage != null)
                {
                    imageControl1.ZoomValueChanged((ToolStripComboBox)sender);
                    imageControl1._CurrZoomIndex = cmbZoom.SelectedIndex;
                }
            }
        }

        private void cmbZoom_TextUpdate(object sender, EventArgs e)
        {
            imageControl1.ZoomTextUpdate((ToolStripComboBox)sender);
        }

        void imageControl1_SizeChanged(object sender, System.EventArgs e)
        {
            Zoom();
            //if (cmbZoom.SelectedIndex != -1)
            //{
            //    imageControl1.ZoomValueChanged((ToolStripComboBox)cmbZoom);
            //    imageControl1._CurrZoomIndex = cmbZoom.SelectedIndex;
            //    // Zoom();
            //    //  _ZoomState = enum_ZoomState.ZoomPercent;
            //}
        }
    }

}
