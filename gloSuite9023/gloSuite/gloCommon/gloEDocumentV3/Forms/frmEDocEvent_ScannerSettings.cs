using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Win32;
using PegasusImaging.WinForms.TwainPro5;
using gloSettings;
using System.IO;
using System.Linq;

namespace gloEDocumentV3.Forms
{

    /// <summary>
    /// Dhruv -> Created a new form for scanner setting
    /// </summary>
    public partial class frm_EDocEvent_ScannerSettings : Form
    {

        #region "Private Variable"

        private RectangleF myScanLayout;
        //bool _IsScanCard = false;
        public string _ErrorMessage = "";
        ScannerSettings objScannerSettings = null;
        static bool inCmbBoxSelection = false;
        static bool bChkPegasusValues = false;
        //static bool inRemoteCmbBoxSelection = false;
        private PegasusImaging.WinForms.TwainPro5.TwainDevice twainDevice;
        private PegasusImaging.WinForms.TwainPro5.TwainPro TwainPr;
        //For Remote Scan
        private DataTable dtScanner = null;
        private DataTable dtScanMode = null;
        private DataTable dtScanDepth = null;
        private DataTable dtResolution = null;
        private DataTable dtBrightness = null;
        private DataTable dtContrast = null;
        private DataTable dtScanSide = null;
        private DataTable dtSupportedSizes = null;
        //
        #endregion
        gloEDocumentV3.Common.RemoteScanCommon oRemoteScanCommon = new Common.RemoteScanCommon();

        #region "Constructor Section"
        public frm_EDocEvent_ScannerSettings()
        {
            InitializeComponent();
            objScannerSettings = new ScannerSettings();//
            myScanLayout = new RectangleF(1f, 1f, 8.5f, 14f);
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public frm_EDocEvent_ScannerSettings(PegasusImaging.WinForms.TwainPro5.TwainDevice twainDevice1)
        {
            InitializeComponent();
            
            twainDevice = twainDevice1;

            objScannerSettings = new ScannerSettings();//
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            myScanLayout = new RectangleF(1f, 1f, 8.5f, 14f);


        }

        public frm_EDocEvent_ScannerSettings(PegasusImaging.WinForms.TwainPro5.TwainDevice twainDevice1, PegasusImaging.WinForms.TwainPro5.TwainPro TwainPro1)
        {
            InitializeComponent();
            twainDevice = twainDevice1;
            TwainPr = TwainPro1;
           // TwainPr.Licensing.UnlockRuntime(1808984205, 249325542, 1216513884, 14413);

            objScannerSettings = new ScannerSettings();//
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            myScanLayout = new RectangleF(1f, 1f, 8.5f, 14f);


        }
        #endregion "Constructor Section"

        #region "Component Event Function"
        private void frm_EDocEvent_ScannerSettings_Load(object sender, EventArgs e)
        {

            if (inCmbBoxSelection == true)
            {
                return;
            }
            inCmbBoxSelection = true;

            try
            {
                gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
            }
            catch (Exception ex)
            {
                AuditLogErrorMessage("Check Network Dir/File Exists : " + ex.ToString());
            }

            //FillImageFormatCMB();

            if ((gloGlobal.gloTSPrint.TerminalServer() != "RDP") || (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking()))
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
            {//RDP
                chkEnableRemoteScanner.Enabled = true;
                chkEnableRemoteScanner.Checked = gloGlobal.gloRemoteScanSettings.EnableRemoteScan;
                
                if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                {
                    chkEliminatePegasus.Enabled = false;
                    chkEliminatePegasus.Checked = false;
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

            //cmbImageFormat.Enabled = false;

            if (chkEliminatePegasus.Checked)
            {
                btnRefreshTwainScanners.Visible = true;
                //cmbRemoteImageFormat.Enabled = true;
            }
            else if (chkEnableRemoteScanner.Checked)
            {
                btnRefreshTwainScanners.Visible = false;
                //cmbRemoteImageFormat.Enabled = false;
            }
            else
            {
                btnRefreshTwainScanners.Visible = false;
            }

            if (chkEliminatePegasus.Checked && !chkEnableRemoteScanner.Checked)
            {
                cmbRemoteImageFormat.Enabled = true;
            }
            else if (!chkEliminatePegasus.Checked || chkEnableRemoteScanner.Checked)
            {
                cmbRemoteImageFormat.Enabled = false;
            }

            btnRefreshScanners.Visible = chkEnableRemoteScanner.Checked;
            pnlRemoteScan.Enabled = chkEnableRemoteScanner.Checked;
            pnlSetting.Enabled = !chkEnableRemoteScanner.Checked;
            pnlRemoteScan.Visible = chkEnableRemoteScanner.Checked;
            pnlSetting.Visible = !chkEnableRemoteScanner.Checked;

            if (chkEnableRemoteScanner.Checked || chkEliminatePegasus.Checked)
            {
                FillFeederCombo();
                DisposingTwain();
                CallRemoteScanSettingsLoad(true);
            }
            else
            {
                InitPagasusTwainDevice();
                objScannerSettings.GetAndSetScanners(ref twainDevice, ref cmbScanner, gloRegistrySetting.gstrDMSScan);
                objScannerSettings.ObtainScannerSettings(ref  twainDevice, ref  cmbScanner, ref  cmbScanner, ref  cmbScanMode, ref  cmbBitDepth, ref  cmbResolution, ref  cmbBrightness, ref  cmbContrast, ref  cmbScanSide, ref  chkShowScannerDialog, ref  cmbSupportedSize, ref  txtCardWidth, ref  txtCardLength, ref txtStartX, ref txtStartY, ref myScanLayout);
            }
            FillImageFormatCMB();

            inCmbBoxSelection = false;
        }

        private void FillImageFormatCMB()
        {
            cmbRemoteImageFormat.Items.Insert(0, "Default");
            cmbRemoteImageFormat.Items.Insert(1, "Bmp");
            //cmbRemoteImageFormat.Items.Insert(2, "Emf");
            //cmbRemoteImageFormat.Items.Insert(3, "Exif");
            //cmbRemoteImageFormat.Items.Insert(4, "Gif");
            //cmbRemoteImageFormat.Items.Insert(5, "Icon");
            cmbRemoteImageFormat.Items.Insert(2, "Jpeg");
            //cmbRemoteImageFormat.Items.Insert(7, "MemoryBmp");
            cmbRemoteImageFormat.Items.Insert(3, "Png");
            cmbRemoteImageFormat.Items.Insert(4, "Tiff");
            //cmbRemoteImageFormat.Items.Insert(10, "Wmf");

            cmbRemoteImageFormat.SelectedIndex = 0;

            if (chkEliminatePegasus.Checked)
            {
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                {
                    return;
                }
                object oScanImageFormat = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrScanImageFormat);

                string sScanImageFormat = Convert.ToString(oScanImageFormat).Trim();

                for (int i = 0; i < cmbRemoteImageFormat.Items.Count; i++)
                {
                    if (Convert.ToString(cmbRemoteImageFormat.Items[i]) == sScanImageFormat)
                    {
                        cmbRemoteImageFormat.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private bool CallRemoteScanSettingsLoad(bool bFormLoad=false)
        {
            if (chkEnableRemoteScanner.Checked)
            {
                if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                {
                    return false;
                }
            }
            pnlRemoteScan.Enabled = true;
            pnlSetting.Enabled = false;

            pnlRemoteScan.Visible = true;
            pnlSetting.Visible = false;

            if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings == null)
            {
                try
                {
                    gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject(chkEliminatePegasus.Checked);
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg))
                    {
                        MessageBox.Show(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg, gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg = null;
                    }
                    else
                    {
                        MessageBox.Show(ex.ToString(), gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    EmptyAllDropDown();
                    return false;
                }
            }
            if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings != null)
            {
                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner == null)
                {
                    MessageBox.Show("Local scanners not found.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                string sCurrRemoteShowScann = GetRegistryValue(gloRegistrySetting.gstrRemoteShowScann);
                if (!string.IsNullOrEmpty(sCurrRemoteShowScann))
                {
                    chkRemoteShowScannerDialog.Checked = Convert.ToBoolean(sCurrRemoteShowScann);
                }
                else
                {
                    chkRemoteShowScannerDialog.Checked = true;
                }
                string sCurrRemoteScanner = GetRegistryValue(gloRegistrySetting.gstrRemoteScanner);
                Int32 indexi = 0;
                Int32 iRowCnt = 0;
                dtScanner = new DataTable();
                dtScanner.Columns.Add("ScannerId", typeof(string));
                dtScanner.Columns.Add("ScannerName", typeof(string));
                for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner.Length; i++)
                {
                    if (sCurrRemoteScanner == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[i].Name)
                    { indexi = iRowCnt; }//Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[i].ScannerID);

                    dtScanner.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[i].ScannerID, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[i].Name);
                    iRowCnt++;
                }
                cmbRemoteScanner.DataSource = null;
                cmbRemoteScanner.ValueMember = "ScannerId";
                cmbRemoteScanner.DisplayMember = "ScannerName";
                cmbRemoteScanner.DataSource = dtScanner;
                cmbRemoteScanner.SelectedIndex = indexi;

                inCmbBoxSelection = false;
                cmbRemoteScanner_SelectedIndexChanged(null, null);
                return true;
            }
            else
            {
                if (!bFormLoad)
                {
                    if (string.IsNullOrEmpty(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg))
                    {
                        MessageBox.Show("Local scanner settings not found.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg, gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg = null;
                EmptyAllDropDown();

                return false;
            }
        }

        private void MakeDataSourceNULL(ref ComboBox cmbRemote)
        {
            DataTable dtTemp = null;
            try
            {
                if (cmbRemote.DataSource != null)
                {
                    try
                    {
                        dtTemp = (DataTable)cmbRemote.DataSource;
                    }
                    catch { }

                    if (dtTemp != null)
                    {
                        dtTemp.Dispose();
                        dtTemp = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            try
            {
                cmbRemote.Items.Clear();
            }
            catch { }

            cmbRemote.DataSource = null;
        }

        private void EmptyAllDropDown()
        {
            try
            {
                MakeDataSourceNULL(ref cmbRemoteScanner);
                MakeDataSourceNULL(ref cmbRemoteScanMode);
                MakeDataSourceNULL(ref cmbRemoteBitDepth);
                MakeDataSourceNULL(ref cmbRemoteResolution);
                MakeDataSourceNULL(ref cmbRemoteBrightness);
                MakeDataSourceNULL(ref cmbRemoteContrast);
                MakeDataSourceNULL(ref cmbRemoteScanSide);
                MakeDataSourceNULL(ref cmbRemoteSupportedSize);
                //MakeDataSourceNULL(ref cmbRemoteImageFormat);
                cmbRemoteImageFormat.Items.Clear();
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
        }

        private void tls_MaintainDoc_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":
                    {

                        if (CheckForScanCardSize() == true)
                        {

                            SaveScannerSettings();
                            CloseForm();

                        }
                    }

                    break;


                case "Close":
                    {
                        CloseForm();
                        break;
                    }

            }
        }

        #endregion "Component Event Function"

        #region "Private Functions"

        private string GetRegistryValue(string strRegistryKey)
        {
            string RegValue = null;

            try
            {
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                {
                    _ErrorMessage = "Unable to open registry. " + gloRegistrySetting.gstrSoftEMR;
                    AuditLogErrorMessage(_ErrorMessage);
                    return RegValue;
                }

                object oSetting = gloRegistrySetting.GetRegistryValue(strRegistryKey);

                if (oSetting != null)
                {
                    RegValue = oSetting.ToString();
                }
                gloRegistrySetting.CloseRegistryKey();

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                RegValue = null;
            }

            return RegValue;
        }

        private bool CheckForScanCardSize()
        {
            try
            {
                if (chkEnableRemoteScanner.Checked || chkEliminatePegasus.Checked)
                { return true; }

                float myCardWidth = (float)(Convert.ToDouble(txtCardWidth.Text.Trim()));
                float myCardLength = (float)(Convert.ToDouble(txtCardLength.Text.Trim()));

                bool retValue = objScannerSettings.CardValidator(myScanLayout, ref  myCardWidth, ref  myCardLength, false);

                return retValue;

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                return true;
            }

        }
        private void SaveScannerSettings()
        {


            try
            {


                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                {
                    return;
                }
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrEnableRemoteScan, chkEnableRemoteScanner.Checked);
                gloGlobal.gloRemoteScanSettings.EnableRemoteScan = chkEnableRemoteScanner.Checked;

                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrZipScannerSettings, chkZipScannerSettings.Checked);
                gloGlobal.gloRemoteScanSettings.bZipScanSettings = chkZipScannerSettings.Checked;

                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrEliminatePegasus, chkEliminatePegasus.Checked);
                gloGlobal.gloEliminatePegasus.bEliminatePegasus = chkEliminatePegasus.Checked;

                if (chkEliminatePegasus.Checked)
                {
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrScanImageFormat, cmbRemoteImageFormat.Text);
                }

                if (chkEnableRemoteScanner.Checked || chkEliminatePegasus.Checked) //(gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                {
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanner, cmbRemoteScanner.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanResol, cmbRemoteResolution.Text);
                    if (bChkForBrightness)
                    {
                        gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanBright, (Convert.ToInt64(cmbRemoteBrightness.Text) - BrightnessScale));
                    }
                    else
                    {
                        gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanBright, cmbRemoteBrightness.Text);
                    }
                    if (bChkForContrast)
                    {
                        gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanContrast, (Convert.ToInt64(cmbRemoteContrast.Text) - ContrastScale));
                    }
                    else
                    {
                        gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanContrast, cmbRemoteContrast.Text);
                    }
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanMode, cmbRemoteScanMode.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanSide, cmbRemoteScanSide.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanFeeder, cmbRemoteScanSideFeeder.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteShowScann, chkRemoteShowScannerDialog.Checked);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteSupporedSize, cmbRemoteSupportedSize.Text);//Supported Size
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanDepth, cmbRemoteBitDepth.Text);//0007876::/Card scan from within scan documents not working properly/[ScanDepth Was not setted]

                    // Add card size setting 
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteCardWidth, txtRemoteCardWidth.Text.Trim());
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteCardLength, txtRemoteCardLength.Text.Trim());
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteCardLeftX, txtRemoteStartX.Text.Trim());//txtstartX Position
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteCardTopY, txtRemoteStartY.Text.Trim());//txtStartYPosistion

                    if (oRemoteScanCommon == null) { oRemoteScanCommon = new gloEDocumentV3.Common.RemoteScanCommon(); }
                    string sRetVal = oRemoteScanCommon.SetRemoteScannerCurrentSettings(null, null, null);
                    if (!string.IsNullOrEmpty(sRetVal))
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Save, sRetVal, gloAuditTrail.ActivityOutCome.Failure);
                    }
                }
                else
                {
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScan, cmbScanner.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSResol, cmbResolution.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSBright, cmbBrightness.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSContrast, cmbContrast.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanMode, cmbScanMode.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanSide, cmbScanSide.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSShowScann, chkShowScannerDialog.Checked);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSupporedSize, cmbSupportedSize.Text);//Supported Size
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanDepth, cmbBitDepth.Text);//0007876::/Card scan from within scan documents not working properly/[ScanDepth Was not setted]

                    //Sandip Darade   20090926
                    // Add card size setting 
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardWidth, txtCardWidth.Text.Trim());
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardLength, txtCardLength.Text.Trim());
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardLeftX, txtStartX.Text.Trim());//txtstartX Position
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardTopY, txtStartY.Text.Trim());//txtStartYPosistion

                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrPegasusBright, cmbBrightness.Text);
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrPegasusContrast, cmbContrast.Text);

                    gloGlobal.gloEliminatePegasus.sPegasusBright = cmbBrightness.Text;
                    gloGlobal.gloEliminatePegasus.sPegasusContrast = cmbContrast.Text;
                }

                //Dhruv -> Closed
                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                {
                    gloRegistrySetting.CloseRegistryKey();
                }

                try
                {
                    gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                }
                catch (Exception ex)
                {
                    AuditLogErrorMessage("Check Network Dir/File Exists : " + ex.ToString());
                }
                bChkPegasusValues = false;

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
        private void CloseForm()
        {
            bChkPegasusValues = false;
            this.Close();
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
        #endregion "Private Functions"


        #region "Selected IndexChange"
        private void cmbScanner_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbScanner.SelectedIndex == -1)
            {
                return;
            }
            if (inCmbBoxSelection == true)
            {
                return;
            }
            inCmbBoxSelection = true;

            if (chkEnableRemoteScanner.Checked || chkEliminatePegasus.Checked)
            {
                DisposingTwain();
            }
            else
            {
                ComboCollection mycombocollections = (ComboCollection)cmbScanner.Items[cmbScanner.SelectedIndex];

                InitPagasusTwainDevice();
                objScannerSettings.GetAndSetScanners(mycombocollections.MyStrings, ref  twainDevice, ref  cmbScanner);
                objScannerSettings.ObtainScannerSettings(ref  twainDevice, ref  cmbScanner, ref  cmbScanner, ref  cmbScanMode, ref  cmbBitDepth, ref  cmbResolution, ref  cmbBrightness, ref  cmbContrast, ref  cmbScanSide, ref  chkShowScannerDialog, ref cmbSupportedSize, ref  txtCardWidth, ref  txtCardLength, ref txtStartX, ref txtStartY, ref myScanLayout);

                if (bChkPegasusValues)
                {
                    setPegasusBrightNContrast();
                }
            }

            inCmbBoxSelection = false;

            return;
        }
        private void cmbScanMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbScanMode.SelectedIndex == -1)
            {
                return;
            }
            if (inCmbBoxSelection == true)
            {
                return;
            }
            inCmbBoxSelection = true;
            if (chkEnableRemoteScanner.Checked || chkEliminatePegasus.Checked)
            {
                DisposingTwain();

            }
            else
            {
                InitPagasusTwainDevice();
                objScannerSettings.ObtainScannerSettings(ref  twainDevice, ref  cmbScanMode, ref  cmbScanner, ref  cmbScanMode, ref  cmbBitDepth, ref  cmbResolution, ref  cmbBrightness, ref  cmbContrast, ref  cmbScanSide, ref  chkShowScannerDialog, ref cmbSupportedSize, ref  txtCardWidth, ref  txtCardLength, ref txtStartX, ref txtStartY, ref myScanLayout);
            }
            inCmbBoxSelection = false;
        }
        private void cmbBitDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBitDepth.SelectedIndex == -1)
            {
                return;
            }
            if (inCmbBoxSelection == true)
            {
                return;
            }
            inCmbBoxSelection = true;
            if (chkEnableRemoteScanner.Checked || chkEliminatePegasus.Checked)
            { DisposingTwain(); }
            else
            {
                InitPagasusTwainDevice();
                objScannerSettings.ObtainScannerSettings(ref  twainDevice, ref  cmbBitDepth, ref  cmbScanner, ref  cmbScanMode, ref  cmbBitDepth, ref  cmbResolution, ref  cmbBrightness, ref  cmbContrast, ref  cmbScanSide, ref  chkShowScannerDialog, ref cmbSupportedSize, ref  txtCardWidth, ref  txtCardLength, ref txtStartX, ref txtStartY, ref myScanLayout);
            }
            inCmbBoxSelection = false;
        }
        private void cmbSupportedSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbSupportedSize.SelectedIndex == -1)
            //{
            //    return;
            //}
            //if (inCmbBoxSelection == true)
            //{
            //    return;
            //}
            //inCmbBoxSelection = true;
            //if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
            //{

            //}
            //else
            //{
            //}
            //inCmbBoxSelection = false;
        }
        #endregion "Selected IndexChange"

        private void txtCardWidth_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar > 47) && (e.KeyChar < 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCardLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 47) && (e.KeyChar < 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtStartX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 47) && (e.KeyChar < 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtStartY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 47) && (e.KeyChar < 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void frm_EDocEvent_ScannerSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposingTwain();

            if (dtScanner != null) { dtScanner.Dispose(); dtScanner = null; }
            if (dtScanMode != null) { dtScanMode.Dispose(); dtScanMode = null; }
            if (dtScanDepth != null) { dtScanDepth.Dispose(); dtScanDepth = null; }
            if (dtResolution != null) { dtResolution.Dispose(); dtResolution = null; }
            if (dtBrightness != null) { dtBrightness.Dispose(); dtBrightness = null; }
            if (dtContrast != null) { dtContrast.Dispose(); dtContrast = null; }
            if (dtScanSide != null) { dtScanSide.Dispose(); dtScanSide = null; }
            if (dtSupportedSizes != null) { dtSupportedSizes.Dispose(); dtSupportedSizes = null; }
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

            SwitchSettingsPanels();
            
            if (chkEnableRemoteScanner.Checked)
            {
                cmbRemoteImageFormat.Enabled = false;
            }
            else
            { //Unchecked

                //cmbRemoteImageFormat.Enabled = true;
                //cmbRemoteImageFormat.BringToFront();
                if (chkEliminatePegasus.Checked)
                {
                    cmbRemoteImageFormat.Enabled = true;
                    cmbRemoteImageFormat.BringToFront();
                }
                else
                {
                    cmbRemoteImageFormat.Enabled = false;
                }
            }
            btnRefreshScanners.Visible = chkEnableRemoteScanner.Checked;

           
            
            

            if (chkEnableRemoteScanner.Checked)
            {
                //pnlSetting.Enabled = false;
                //pnlSetting.Visible = false;
                //pnlRemoteScan.Enabled = true;
                //pnlRemoteScan.Visible = true;
                gloGlobal.gloRemoteScanSettings.AssignReEvaluate();

                gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();

                DisposingTwain();
                gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings = null;
                CallRemoteScanSettingsLoad();
               
            }
            else
            {
                //pnlSetting.Enabled = true;
                //pnlSetting.Visible = true;
                //pnlRemoteScan.Enabled = false;
                //pnlRemoteScan.Visible = false;
                InitPagasusTwainDevice();
                objScannerSettings.GetAndSetScanners(ref twainDevice, ref cmbScanner, gloRegistrySetting.gstrDMSScan);
                objScannerSettings.ObtainScannerSettings(ref  twainDevice, ref  cmbScanner, ref  cmbScanner, ref  cmbScanMode, ref  cmbBitDepth, ref  cmbResolution, ref  cmbBrightness, ref  cmbContrast, ref  cmbScanSide, ref  chkShowScannerDialog, ref  cmbSupportedSize, ref  txtCardWidth, ref  txtCardLength, ref txtStartX, ref txtStartY, ref myScanLayout);
            }
        }

        private void SwitchSettingsPanels()
        {
            if (!chkEnableRemoteScanner.Checked && !chkEliminatePegasus.Checked)
            {
                pnlSetting.Enabled = true;
                pnlSetting.Visible = true;
                pnlRemoteScan.Enabled = false;
                pnlRemoteScan.Visible = false;
            }
            else if (chkEnableRemoteScanner.Checked || chkEliminatePegasus.Checked)
            {
                pnlSetting.Enabled = false;
                pnlSetting.Visible = false;
                pnlRemoteScan.Enabled = true;
                pnlRemoteScan.Visible = true;
            }
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = gloEDocumentV3.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;

        }
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = gloEDocumentV3.Properties.Resources.Img_LongButton;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;

        }
        bool bChkForBrightness = false;
        bool bChkForContrast = false;
        Int64 BrightnessScale = 0;
        Int64 ContrastScale = 0;


        private void cmbRemoteScanner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRemoteScanner.SelectedIndex == -1)
            {
                return;
            }
            if (inCmbBoxSelection == true)
            {
                return;
            }
            inCmbBoxSelection = true;

            if (oRemoteScanCommon == null) { oRemoteScanCommon = new gloEDocumentV3.Common.RemoteScanCommon(); }
            Int64 currScanner = Convert.ToInt64(cmbRemoteScanner.SelectedValue);
            Int32 indexi = 0;
            Int32 iRowCnt = 0;
            try
            {
                string sCurrRemoteScanMode = GetRegistryValue(gloRegistrySetting.gstrRemoteScanMode);
                //ScanMode
                cmbRemoteScanMode.DataSource = null;
                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode != null)
                {
                    dtScanMode = new DataTable();
                    dtScanMode.Columns.Add("ScanModeId", typeof(string));
                    dtScanMode.Columns.Add("ScanModeName", typeof(string));
                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode.Length; i++)
                    {
                        if (oRemoteScanCommon.GetXMLTagNameForMode(sCurrRemoteScanMode) == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[i].Name)
                        { indexi = iRowCnt; }//Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[i].ScanModeID);
                        dtScanMode.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[i].ScanModeID,
                            oRemoteScanCommon.GetXMLTagNameForMode(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[i].Name, true));
                        iRowCnt++;
                    }

                    cmbRemoteScanMode.ValueMember = "ScanModeId";
                    cmbRemoteScanMode.DisplayMember = "ScanModeName";
                    cmbRemoteScanMode.DataSource = dtScanMode;
                    cmbRemoteScanMode.SelectedIndex = indexi;
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }

            //ScanMode -End
            inCmbBoxSelection = false;
            //cmbScanMode_SelectedIndexChanged(null, null);
            cmbRemoteScanMode_SelectedIndexChanged(null, null);

            //Scan Resolution
            try
            {
                string sCurrRemoteScanResol = GetRegistryValue(gloRegistrySetting.gstrRemoteScanResol);
                indexi = -1;
                iRowCnt = 0;
                cmbRemoteResolution.DataSource = null;


                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Resolution != null)
                {
                    Int32 iStep;

                    if (cmbRemoteScanner.Text.StartsWith("WIA"))
                    {
                        if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Resolution.Length > 25)
                        { iStep = 25; }
                        else
                        { iStep = 1; }
                    }
                    else
                    {
                        if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Resolution.Length > 50)
                        { iStep = 50; }
                        else
                        { iStep = 1; }
                    }

                    dtResolution = new DataTable();
                    dtResolution.Columns.Add("ResolutionId", typeof(string));
                    dtResolution.Columns.Add("ResolutionName", typeof(string));
                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Resolution.Length; i = i + iStep)
                    {
                        if (sCurrRemoteScanResol == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Resolution[i].Name)
                        { indexi = iRowCnt; }// Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Resolution[i].ResolutionID);

                        dtResolution.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Resolution[i].ResolutionID,
                            gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Resolution[i].Name);
                        iRowCnt++;
                    }

                    cmbRemoteResolution.ValueMember = "ResolutionId";
                    cmbRemoteResolution.DisplayMember = "ResolutionName";
                    cmbRemoteResolution.DataSource = dtResolution;
                    if (indexi == -1)
                    {
                        if (dtResolution != null && dtResolution.Rows.Count > 0)
                        {
                            indexi = (dtResolution.Rows.Count) / 2;
                            cmbRemoteResolution.SelectedIndex = indexi;
                        }
                    }
                    else
                    {
                        cmbRemoteResolution.SelectedIndex = indexi;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            //Scan Resolution -End

            //Scan Brightness
            try
            {
                string sCurrRemoteScanBright = GetRegistryValue(gloRegistrySetting.gstrRemoteScanBright);
                indexi = -1;
                iRowCnt = 0;
                cmbRemoteBrightness.DataSource = null;
                Int64 iBrightnessName = 0;
                bChkForBrightness = false;
                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Brightness != null)
                {
                    dtBrightness = new DataTable();
                    dtBrightness.Columns.Add("BrightnessId", typeof(string));
                    dtBrightness.Columns.Add("BrightnessName", typeof(string));

                    //Int64.TryParse(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Brightness[0].Name, out BrightnessScale);
                    //if (BrightnessScale < 0) { BrightnessScale = -(BrightnessScale) + 1; bChkForBrightness = true; }

                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Brightness.Length; i++)
                    {
                        if (sCurrRemoteScanBright == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Brightness[i].Name)
                        { indexi = iRowCnt; }// Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Brightness[i].BrightnessID); 

                        if (bChkForBrightness)
                        {
                            iBrightnessName = Convert.ToInt64(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Brightness[i].Name) + BrightnessScale;
                            dtBrightness.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Brightness[i].BrightnessID,
                                                Convert.ToString(iBrightnessName));
                            iBrightnessName = 0;
                        }
                        else
                        {
                            dtBrightness.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Brightness[i].BrightnessID,
                               gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Brightness[i].Name);
                        }
                        iRowCnt++;
                    }
                    // bChkForBrightness = false;

                    cmbRemoteBrightness.ValueMember = "BrightnessId";
                    cmbRemoteBrightness.DisplayMember = "BrightnessName";
                    cmbRemoteBrightness.DataSource = dtBrightness;
                    if (indexi == -1)
                    {
                        if (dtBrightness != null && dtBrightness.Rows.Count > 0)
                        {
                            indexi = (dtBrightness.Rows.Count) / 2;
                            cmbRemoteBrightness.SelectedIndex = indexi;
                        }
                    }
                    else
                    {
                        cmbRemoteBrightness.SelectedIndex = indexi;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            //Scan Brightness -End

            //Scan Contrast
            try
            {
                string sCurrRemoteScanContrast = GetRegistryValue(gloRegistrySetting.gstrRemoteScanContrast);
                indexi = -1;
                iRowCnt = 0;
                cmbRemoteContrast.DataSource = null;
                Int64 iContrastName = 0;
                bChkForContrast = false;
                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Contrast != null)
                {


                    dtContrast = new DataTable();
                    dtContrast.Columns.Add("ContrastId", typeof(string));
                    dtContrast.Columns.Add("ContrastName", typeof(string));

                    //Int64.TryParse(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Contrast[0].Name, out ContrastScale);
                    //if (ContrastScale < 0) { ContrastScale = -(ContrastScale) + 1; bChkForContrast = true; }

                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Contrast.Length; i++)
                    {
                        if (sCurrRemoteScanContrast == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Contrast[i].Name)
                        { indexi = iRowCnt; }// Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Contrast[i].ContrastID); 

                        if (bChkForContrast)
                        {
                            iContrastName = Convert.ToInt64(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Contrast[i].Name) + ContrastScale;
                            dtContrast.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Contrast[i].ContrastID,
                                                Convert.ToString(iContrastName));
                            iContrastName = 0;
                        }
                        else
                        {
                            dtContrast.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Contrast[i].ContrastID,
                                gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].Contrast[i].Name);
                        }
                        iRowCnt++;
                    }

                    cmbRemoteContrast.ValueMember = "ContrastId";
                    cmbRemoteContrast.DisplayMember = "ContrastName";
                    cmbRemoteContrast.DataSource = dtContrast;
                    if (indexi == -1)
                    {
                        if (dtContrast != null && dtContrast.Rows.Count > 0)
                        {
                            indexi = (dtContrast.Rows.Count) / 2;
                            cmbRemoteContrast.SelectedIndex = indexi;
                        }
                    }
                    else
                    {
                        cmbRemoteContrast.SelectedIndex = indexi;
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            //Scan Contrast -End

            // Scan Side
            try
            {
                string sCurrRemoteScanSide = GetRegistryValue(gloRegistrySetting.gstrRemoteScanSide);
                indexi = 0;
                iRowCnt = 0;
                cmbRemoteScanSide.DataSource = null;
                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanSide != null)
                {
                    dtScanSide = new DataTable();
                    dtScanSide.Columns.Add("ScanSideID", typeof(string));
                    dtScanSide.Columns.Add("ScanSideName", typeof(string));
                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanSide.Length; i++)
                    {
                        if (sCurrRemoteScanSide == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanSide[i].Name)
                        { indexi = iRowCnt; }// Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanSide[i].ScanSideID); 

                        dtScanSide.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanSide[i].ScanSideID,
                            gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanSide[i].Name);
                        iRowCnt++;
                    }

                    cmbRemoteScanSide.ValueMember = "ScanSideID";
                    cmbRemoteScanSide.DisplayMember = "ScanSideName";
                    cmbRemoteScanSide.DataSource = dtScanSide;

                    cmbRemoteScanSide.SelectedIndex = indexi;
                    cmbRemoteScanSide_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }

            try
            {
                if (cmbRemoteScanSideFeeder.Items.Count < 1)
                {
                    FillFeederCombo();
                }
                string sCurrRemoteScanSideFeeder = GetRegistryValue(gloRegistrySetting.gstrDMSScanFeeder);
                if (!string.IsNullOrEmpty(sCurrRemoteScanSideFeeder))
                {
                    int currIndex = cmbRemoteScanSideFeeder.FindStringExact(sCurrRemoteScanSideFeeder);
                    if (currIndex < 0)
                    {
                        currIndex = 0;
                    }
                    cmbRemoteScanSideFeeder.SelectedIndex = currIndex;
                }
                else
                {
                    if (cmbRemoteScanSide.Text.ToLower() == "duplex")
                    {
                        cmbRemoteScanSideFeeder.Text = "Feeder";
                    }
                    else
                    {
                        cmbRemoteScanSideFeeder.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            // Scan Side - End

            try
            {
            //Scan SupportedSizes


                txtRemoteCardLength.Text = GetRegistryValue(gloRegistrySetting.gstrRemoteCardLength);
                txtRemoteCardWidth.Text = GetRegistryValue(gloRegistrySetting.gstrRemoteCardWidth);
                txtRemoteStartX.Text = GetRegistryValue(gloRegistrySetting.gstrRemoteCardLeftX);
                txtRemoteStartY.Text = GetRegistryValue(gloRegistrySetting.gstrRemoteCardTopY);

                string sCurrRemoteSupporedSize = GetRegistryValue(gloRegistrySetting.gstrRemoteSupporedSize);
                indexi = 0;
                iRowCnt = 0;
                cmbRemoteSupportedSize.DataSource = null;
                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize != null)
                {
                    dtSupportedSizes = new DataTable();
                    dtSupportedSizes.Columns.Add("SupportedSizeID", typeof(string));
                    dtSupportedSizes.Columns.Add("SupportedSizeName", typeof(string));
                    dtSupportedSizes.Columns.Add("Length", typeof(string));
                    dtSupportedSizes.Columns.Add("Left", typeof(string));
                    dtSupportedSizes.Columns.Add("Top", typeof(string));
                    dtSupportedSizes.Columns.Add("Width", typeof(string));

                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize.Length; i++)
                    {
                        if (sCurrRemoteSupporedSize == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize[i].Name)
                        { indexi = iRowCnt; }// Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize[i].SupportedSizeID); 

                        dtSupportedSizes.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize[i].SupportedSizeID,
                                                    gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize[i].Name,
                                                    gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize[i].Length,
                                                    gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize[i].Left,
                                                    gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize[i].Top,
                                                    gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].SupportedSize[i].Width
                                                 );
                        iRowCnt++;
                    }

                    cmbRemoteSupportedSize.ValueMember = "SupportedSizeID";
                    cmbRemoteSupportedSize.DisplayMember = "SupportedSizeName";
                    cmbRemoteSupportedSize.DataSource = dtSupportedSizes;

                    cmbRemoteSupportedSize.SelectedIndex = indexi;
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            //Scan SupportedSizes -End

            inCmbBoxSelection = false;
        }

        private void FillFeederCombo()
        {
            cmbRemoteScanSideFeeder.Items.Clear();
            cmbRemoteScanSideFeeder.Items.Add("Default");
            cmbRemoteScanSideFeeder.Items.Add("Flatbed");
            cmbRemoteScanSideFeeder.Items.Add("Feeder");
            cmbRemoteScanSideFeeder.SelectedIndex = 0;
        }

        private void cmbRemoteScanMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRemoteScanMode.SelectedIndex == -1)
            {
                return;
            }
            if (inCmbBoxSelection == true)
            {
                return;
            }
            inCmbBoxSelection = true;
            string sCurrRemoteScanDepth = GetRegistryValue(gloRegistrySetting.gstrRemoteScanDepth);
            Int32 indexi = 0;
            Int32 iRowCnt = 0;
            Int64 currScanner = Convert.ToInt64(cmbRemoteScanner.SelectedValue);
            Int64 currScanMode = Convert.ToInt64(cmbRemoteScanMode.SelectedValue);
            cmbRemoteBitDepth.DataSource = null;
            if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[currScanMode].ScanDepth != null)
            {


                dtScanDepth = new DataTable();
                dtScanDepth.Columns.Add("ScanDepthId", typeof(string));
                dtScanDepth.Columns.Add("ScanDepthName", typeof(string));
                for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[currScanMode].ScanDepth.Length; i++)
                {
                    if (sCurrRemoteScanDepth == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[currScanMode].ScanDepth[i].Name)
                    { indexi = iRowCnt; }// Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[currScanMode].ScanDepth[i].ScanDepthId); 
                    dtScanDepth.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[currScanMode].ScanDepth[i].ScanDepthId, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[currScanner].ScanMode[currScanMode].ScanDepth[i].Name);
                    iRowCnt++;
                }

                cmbRemoteBitDepth.ValueMember = "ScanDepthId";
                cmbRemoteBitDepth.DisplayMember = "ScanDepthName";
                cmbRemoteBitDepth.DataSource = dtScanDepth;

                cmbRemoteBitDepth.SelectedIndex = indexi;
            }
            inCmbBoxSelection = false;
        }

        private void cmbRemoteSupportedSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRemoteSupportedSize.SelectedIndex == -1)
            {
                return;
            }
            if (inCmbBoxSelection == true)
            {
                return;
            }
            //inCmbBoxSelection = true;

            //Int64 currSupportedSize = Convert.ToInt64(cmbRemoteSupportedSize.SelectedValue);

            //if (dtSupportedSizes != null)
            //{
                //DataRow[] results = dtSupportedSizes.Select("SupportedSizeID='" + Convert.ToString(currSupportedSize) + "'");
                //if (results.Length != 0)
                //{
                //    txtRemoteCardWidth.Text = Convert.ToString(results[0]["Width"]);
                //    txtRemoteCardLength.Text = Convert.ToString(results[0]["Length"]);
                //    txtRemoteStartX.Text = Convert.ToString(results[0]["Left"]);
                //    txtRemoteStartY.Text = Convert.ToString(results[0]["Top"]);
                //}
                //else
                //{

                //}
            //}
            inCmbBoxSelection = false;
        }



        private void btnRefreshScanners_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnRefreshScanners.Enabled = false;
                Application.DoEvents();
                if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                {
                    return;
                }
                if (gloRemoteScanGeneral.RemoteScanSettings.RefreshScanners() == true)
                {
                    string sRetVal = null;
                    //Current Settings
                    sRetVal = oRemoteScanCommon.SetRemoteScannerCurrentSettings(null, null, null);
                    if (!string.IsNullOrEmpty(sRetVal)) { gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, sRetVal, gloAuditTrail.ActivityOutCome.Failure); }

                    gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject();

                    if (CallRemoteScanSettingsLoad())
                    {
                        //Update clients machine name
                        gloAuditTrail.MachineDetails.MachineInfo myRemoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails(true);
                        gloGlobal.gloTSPrint.sClientLocalMachineName = myRemoteMachine.MachineName;
                        MessageBox.Show("Scanners Refreshed", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Unable to refresh Scanners. One/multiple of the following might be the possible reason(s)." + Environment.NewLine + "1. Unable to retrieve scanner list." + Environment.NewLine + "2. Unable to write scanner list to configuration file.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Unable to update scanner list, Please try after some time.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

            finally
            {
                this.Cursor = Cursors.Default;
                btnRefreshScanners.Enabled = true;
                Application.DoEvents();
            }
        }

        private void txtRemoteCardWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 47) && (e.KeyChar < 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtRemoteCardLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 47) && (e.KeyChar < 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtRemoteStartX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 47) && (e.KeyChar < 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtRemoteStartY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 47) && (e.KeyChar < 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void txtRemoteStartX_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRemoteStartY_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void cmbRemoteBitDepth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void txtRemoteCardWidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRemoteCardLength_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void cmbRemoteContrast_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void cmbRemoteBrightness_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void cmbRemoteScanSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRemoteScanSide.Text.ToLower() == "duplex")
                {
                    if (cmbRemoteScanSideFeeder.Items.Count < 1)
                    {
                        FillFeederCombo();
                    }
                    cmbRemoteScanSideFeeder.Text = "Feeder";
                }
            }
            catch
            { }
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void cmbRemoteResolution_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void chkRemoteShowScannerDialog_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkEliminatePegasus_CheckedChanged(object sender, EventArgs e)
        {
            SwitchSettingsPanels();
            
            
            if ((gloGlobal.gloTSPrint.TerminalServer() == "RDP"))
            {

            }
            else
            {

            }

            if (chkEliminatePegasus.Checked)
            {
                btnRefreshTwainScanners.Visible = true;
                               
                cmbRemoteImageFormat.Enabled = true;
                cmbRemoteImageFormat.BringToFront();
                //if (chkEnableRemoteScanner.Checked) { cmbImageFormat.Enabled = false; }
                //else { cmbImageFormat.Enabled = true; }
                DisposingTwain();
                gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings = null;
                CallRemoteScanSettingsLoad();
            }
            else
            {
                cmbRemoteImageFormat.Enabled = false;
                btnRefreshTwainScanners.Visible = false;
                InitPagasusTwainDevice();
                objScannerSettings.GetAndSetScanners(ref twainDevice, ref cmbScanner, gloRegistrySetting.gstrDMSScan);
                objScannerSettings.ObtainScannerSettings(ref  twainDevice, ref  cmbScanner, ref  cmbScanner, ref  cmbScanMode, ref  cmbBitDepth, ref  cmbResolution, ref  cmbBrightness, ref  cmbContrast, ref  cmbScanSide, ref  chkShowScannerDialog, ref  cmbSupportedSize, ref  txtCardWidth, ref  txtCardLength, ref txtStartX, ref txtStartY, ref myScanLayout);

                setPegasusBrightNContrast();
                bChkPegasusValues = true;
            }

        }

        private void setPegasusBrightNContrast()
        {
            //Set previous pegasus brightness
            try
            {
                gloGlobal.gloEliminatePegasus.setComboIndex(gloGlobal.gloEliminatePegasus.sPegasusBright, ref cmbBrightness);
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                // MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Set previous pegasus brightness
            try
            {
                gloGlobal.gloEliminatePegasus.setComboIndex(gloGlobal.gloEliminatePegasus.sPegasusContrast, ref cmbContrast);
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                // MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitPagasusTwainDevice()
        {
            try
            {
               // AuditLogErrorMessage("In InitPagasusTwainDevice");
                if (twainDevice == null)
                {
                 //   AuditLogErrorMessage("twainDevice == null");
                    TwainPr.Licensing.UnlockRuntime(1808984205, 249325542, 1216513884, 14413);
                    twainDevice = new TwainDevice(TwainPr);
                }
                else
                {
                //    AuditLogErrorMessage("twainDevice != null");
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    //string sRetVal = null;
                    //Current Settings
                    //sRetVal = oRemoteScanCommon.SetRemoteScannerCurrentSettings(null, null, null);
                    //gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject(chkEliminatePegasus.Checked);
                    //if (!string.IsNullOrEmpty(sRetVal))
                    //{
                    //    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, sRetVal, gloAuditTrail.ActivityOutCome.Failure);
                    //}
                    gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings = null;
                    if (CallRemoteScanSettingsLoad())
                    {
                        MessageBox.Show("Scanners Refreshed", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg))
                    {
                        MessageBox.Show("Unable to update scanner list, Please try after some time.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg, gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg = null;
                        EmptyAllDropDown();
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


    //Get/Set --> Class
    public class ComboCollection : IDisposable
    {
        #region "Constructor"
        public ComboCollection(String IntMyString, float IntMyValue)
        {
            MyString = IntMyString;
            MyValue = IntMyValue;
        }
        #endregion "Constructor"

        #region "Distructor"
        ~ComboCollection()
        {
            Dispose(false);
        }
        #endregion "Distructor"

        #region "Private Variable"

        String MyString = "";
        float MyValue = 0;
        private bool disposed = false;
        #endregion "Private Variable"

        //Public Property
        public String MyStrings
        {
            get { return MyString; }
            set { MyString = value; }
        }

        public float MyValues
        {
            get { return MyValue; }
            set { MyValue = value; }
        }

        //Disposing
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                }
            }
            disposed = true;
        }
    }

    public class ScannerSettings : IDisposable
    {
        #region "Private varibale"
        bool disposed = false;
        static bool semaphore = false;
        static bool ScannerSemaphore = false;
        #endregion "Private varibale"

        #region "Public Function"

        #region "Return type ->bool(GetAndSetScanner)"
        public bool GetAndSetScanners(ref TwainDevice twainDevice, ref System.Windows.Forms.ComboBox cmbBox, String strRegistryKey)
        {
            String RegValue = "";

            //setting the registrykey
            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
            {
                return false;
            }

            object oSetting = gloRegistrySetting.GetRegistryValue(strRegistryKey);
            if (oSetting != null)
            {
                RegValue = oSetting.ToString().Trim();
            }


            gloRegistrySetting.CloseRegistryKey();

            return GetAndSetScanners(RegValue, ref  twainDevice, ref  cmbBox);


        }
        static bool _myCmbBoxSemaphore = false;
        public bool GetAndSetScanners(String RegValue, ref TwainDevice twainDevice, ref System.Windows.Forms.ComboBox cmbBox)
        {

            if (ScannerSemaphore == true)
            {
                return false;
            }
            ScannerSemaphore = true;

            int regIndex = -1;
            int curIndex = -1;
            DataSourceCollection oScanners = null;//new DataSourceCollection(twainDevice);
            ArrayList ComboCollections = null;
            if (cmbBox != null)
            {
                ComboCollections = new ArrayList();
            }
            try
            {
                if (twainDevice != null)
                {
                    twainDevice.OpenDataSourceManager();
                    oScanners = new DataSourceCollection(twainDevice);
                    if (oScanners != null)
                    {

                        //if (ComboCollections != null)
                        //{

                        String strResult = "";
                        for (int i = 0; i <= oScanners.Count - 1; i++)
                        {
                            strResult = oScanners[i].ToString();

                            ComboCollection myCollection = new ComboCollection(strResult, 0);
                            if (myCollection != null)
                            {
                                if (ComboCollections != null)
                                {
                                    ComboCollections.Add(myCollection);
                                }

                                if (RegValue == strResult)
                                {
                                    if (ComboCollections == null)
                                    {
                                        oScanners.Current = i;
                                        ScannerSemaphore = false;
                                        return true;
                                    }
                                    regIndex = i;
                                    // oScanners.Current = i;
                                }
                                try
                                {
                                    if (oScanners.Current == i)
                                    {
                                        curIndex = i;
                                    }
                                }
                                catch
                                {
                                    //please do not catch these exception
                                    //It is used for the bypass their exception
                                }
                            }
                        }
                        if ((regIndex == -1) && (RegValue != ""))
                        {
                            using (ComboCollection myAnotherCollection = new ComboCollection(RegValue, 0))
                            {
                                if (myAnotherCollection != null)
                                {
                                    if (ComboCollections != null)
                                    {
                                        ComboCollections.Add(myAnotherCollection);
                                    }
                                    else
                                    {
                                        if (curIndex != -1)
                                        {//extra condition added for Incident #00001678: tool->settings and Scan Card form DMS Screen
                                            oScanners.Current = oScanners.Count;
                                        }
                                        ScannerSemaphore = false;
                                        return false;
                                    }
                                    regIndex = oScanners.Count;
                                }
                                else
                                {
                                    String _ErrorMessage = "Finally. " + regIndex;
                                    AuditLogErrorMessage(_ErrorMessage);

                                }
                            }
                        }

                        if ((regIndex != curIndex) && (regIndex != -1) && (curIndex != -1))
                        {//extra condition added for Incident #00001678: tool->settings 
                            oScanners.Current = regIndex;

                            if (ComboCollections == null)
                            {
                                ScannerSemaphore = false;
                                return true;
                            }
                        }

                    }

                    //}

                }

                if ((regIndex == -1) && (RegValue != ""))
                {
                    using (ComboCollection myAnotherCollection = new ComboCollection(RegValue, 0))
                    {
                        if (myAnotherCollection != null)
                        {
                            if (ComboCollections != null)
                            {
                                ComboCollections.Add(myAnotherCollection);
                                regIndex = 0;
                            }
                            else
                            {

                                ScannerSemaphore = false;
                                return false;
                            }
                        }
                        else
                        {
                            String _ErrorMessage = "Finally. " + regIndex;
                            AuditLogErrorMessage(_ErrorMessage);

                        }
                    }
                }
                if (cmbBox != null)
                {
                    if (_myCmbBoxSemaphore == true)
                    {
                        return false;
                    }
                    _myCmbBoxSemaphore = true;

                    cmbBox.DataSource = null;
                    cmbBox.Items.Clear();
                    cmbBox.DisplayMember = "MyStrings";
                    cmbBox.ValueMember = "MyValues";
                    cmbBox.DataSource = ComboCollections;
                    if (regIndex == -1)
                        regIndex = curIndex;
                    if ((regIndex == -1) && (ComboCollections.Count == 1))
                    {
                        regIndex = 0;
                    }
                    cmbBox.SelectedIndex = regIndex;
                    cmbBox.Enabled = (ComboCollections.Count > 1);
                    _myCmbBoxSemaphore = false;
                }

            }
            catch (Exception ex)
            {
                if (oScanners != null)
                {
                    oScanners.Dispose();
                    oScanners = null;
                }
                String _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ScannerSemaphore = false;
                return false;
            }
            finally
            {

                if (oScanners != null)
                {
                    oScanners.Dispose();
                    oScanners = null;
                }
            }
            ScannerSemaphore = false;
            return false;
        }
        #endregion "Return type ->bool(GetAndSetScanner)"

        #region "Return Type ->Void(SetScannerSettings)"
        public void SetScannerSettings(ref TwainDevice twainDevice, AdvancedCapability MyCAP, ref  System.Windows.Forms.ComboBox cmbBox)
        {
            if (cmbBox.SelectedIndex != -1)
            {
                ComboCollection myCollection = (ComboCollection)cmbBox.Items[cmbBox.SelectedIndex];
                SetScannerSettings(ref  twainDevice, MyCAP, myCollection);
            }
        }
        public void SetScannerSettings(ref TwainDevice twainDevice, AdvancedCapability MyCAP, ComboCollection myCollection)
        {
            //Applying the Setting to the Scanner
            try
            {
                if (twainDevice != null)
                {
                    if (twainDevice.IsCapabilitySupported(MyCAP) == true)
                    {
                        CapabilityContainerOneValueFloat myCap = new CapabilityContainerOneValueFloat(MyCAP);
                        if (myCap != null)
                        {

                            if (myCollection != null)
                            {

                                myCap.Value = myCollection.MyValues;
                                twainDevice.SetCapability(myCap);
                            }
                        }
                    }

                }
            }
            catch (FormatException ex)
            {
                String _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }

            catch (System.AccessViolationException ex)
            {
                String _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            catch (Exception ex)
            {
                String _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
        }
        #endregion "Return Type ->Void(SetScannerSettings)"

        #region "Return type - ComboCollection(GetScannerSettings)"
        public ComboCollection GetScannerSettings(ref TwainDevice twainDevice, AdvancedCapability capability, ref   System.Windows.Forms.ComboBox cmbBox, String strRegistryKey)
        {
            String RegValue = "";
            String _ErrorMessage = "";
            ComboCollection nullComoboCollection = null;

            ComboCollection emptyComoboCollection = new ComboCollection("", 0);
            //setting the registrykey
            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
            {
                _ErrorMessage = "Unable to open registry. " + gloRegistrySetting.gstrSoftEMR;
                AuditLogErrorMessage(_ErrorMessage);
                if (cmbBox == null)
                {

                    return emptyComoboCollection;
                }
                else
                {

                    return nullComoboCollection;
                }
            }

            object oSetting = gloRegistrySetting.GetRegistryValue(strRegistryKey);
            if (oSetting != null)
            {
                RegValue = oSetting.ToString();
            }
            gloRegistrySetting.CloseRegistryKey();
            return GetScannerSettings(RegValue, ref  twainDevice, capability, ref cmbBox);

        }
        public ComboCollection GetScannerSettings(String RegValue, ref TwainDevice twainDevice, AdvancedCapability capability, ref   System.Windows.Forms.ComboBox cmbBox)
        {
            int regIndex = -1;
            int curIndex = -1;
            int defIndex = -1;
            ArrayList ComboCollections = GetScannerSettings(RegValue, ref   twainDevice, capability, cmbBox != null, ref regIndex, ref curIndex, ref defIndex);
            if (ComboCollections != null)
            {
                if (ComboCollections.Count > 0)
                {
                    if (cmbBox != null)
                    {

                        cmbBox.DisplayMember = "MyStrings";
                        cmbBox.ValueMember = "MyValues";
                        cmbBox.DataSource = ComboCollections;
                        if (regIndex == -1)
                            regIndex = curIndex;
                        if (regIndex == -1)
                            regIndex = defIndex;
                        if ((regIndex == -1) && (ComboCollections.Count == 1))
                        {
                            regIndex = 0;
                        }
                        cmbBox.SelectedIndex = regIndex;
                        cmbBox.Enabled = (ComboCollections.Count > 1);
                        return null;
                    }
                    else
                    {
                        return (ComboCollection)ComboCollections[0];
                    }
                }
            }


            return null;
        }
        public ArrayList GetScannerSettings(String RegValue, ref TwainDevice twainDevice, AdvancedCapability capability, bool ReturnAll, ref int regIndex, ref int curIndex, ref int defIndex)
        {

            String _ErrorMessage = "";
            //setting the registrykey

            ArrayList ComboCollections = null;
            //if (ReturnAll  == true)
            //{
            ComboCollections = new ArrayList();
            if (ComboCollections == null)
            {
                _ErrorMessage = "Unable to allocate memory in this SetScannerSetting routine. ";
                AuditLogErrorMessage(_ErrorMessage);
                return null;
            }

            //}
            regIndex = -1;
            curIndex = -1;
            defIndex = -1;
            float regFloat = 0;
            try
            {
                regFloat = (float)Convert.ToDecimal(RegValue);
            }
            catch
            {
                //do not use catch 
            }

            float CheckValue = 0;
            if (capability == AdvancedCapability.IcapPixelType)
            {
                CheckValue = 2;
            }

            if (twainDevice != null)
            {
                if (twainDevice.IsCapabilitySupported(capability) == true)
                {


                    PegasusImaging.WinForms.TwainPro5.CapabilityContainer capcontainer = twainDevice.GetCapability(capability);



                    try
                    {

                        if (capcontainer != null)
                        {
                            switch (capcontainer.GetType().ToString())
                            {
                                // Type ONEVALUE only returns a single value
                                case "PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat":
                                    {
                                        PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat myCap = (CapabilityContainerOneValueFloat)capcontainer;
                                        if (myCap != null)
                                        {
                                            String valString = myCap.Value.ToString();
                                            using (ComboCollection myCollection = new ComboCollection(valString, myCap.Value))
                                            {

                                                if (myCollection != null)
                                                {
                                                    if (ReturnAll == true)
                                                    {
                                                        ComboCollections.Add(myCollection);
                                                    }
                                                    regIndex = 0;
                                                    defIndex = 0;
                                                    curIndex = 0;

                                                    if ((RegValue != valString) && (RegValue != ""))
                                                    {

                                                        using (ComboCollection myAnotherCollection = new ComboCollection(RegValue, myCap.Value))
                                                        {
                                                            if (myAnotherCollection != null)
                                                            {
                                                                ComboCollections.Add(myAnotherCollection);
                                                                if (ReturnAll == false)
                                                                {
                                                                    return ComboCollections;
                                                                }

                                                                regIndex = 1;

                                                            }
                                                            else
                                                            {
                                                                _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat. " + valString + myCap.Value.ToString();
                                                                AuditLogErrorMessage(_ErrorMessage);

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {

                                                        if (ReturnAll == false)
                                                        {
                                                            ComboCollections.Add(myCollection);
                                                            return ComboCollections;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat. " + valString + myCap.Value.ToString();
                                                    AuditLogErrorMessage(_ErrorMessage);

                                                }
                                            }
                                        }

                                        break;
                                    }
                                case "PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueString":
                                    {
                                        PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueString myCap = (CapabilityContainerOneValueString)capcontainer;
                                        if (myCap != null)
                                        {
                                            using (ComboCollection myCollection = new ComboCollection(myCap.Value, 0))
                                            {
                                                if (myCollection != null)
                                                {
                                                    if (ReturnAll == true)
                                                    {
                                                        ComboCollections.Add(myCollection);
                                                    }

                                                    regIndex = 0;
                                                    defIndex = 0;
                                                    curIndex = 0;
                                                    if ((RegValue != myCap.Value) && (RegValue != ""))
                                                    {
                                                        using (ComboCollection myAnotherCollection = new ComboCollection(RegValue, regFloat))
                                                        {
                                                            if (myAnotherCollection != null)
                                                            {
                                                                ComboCollections.Add(myAnotherCollection);//
                                                                if (ReturnAll == false)
                                                                {
                                                                    return ComboCollections;
                                                                }

                                                                regIndex = 1;
                                                            }
                                                            else
                                                            {
                                                                _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueString. " + myCap.Value;
                                                                AuditLogErrorMessage(_ErrorMessage);

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (ReturnAll == false)
                                                        {
                                                            ComboCollections.Add(myCollection);
                                                            return ComboCollections;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueString. " + myCap.Value;
                                                    AuditLogErrorMessage(_ErrorMessage);

                                                }
                                            }
                                        }

                                        break;
                                    }

                                case "PegasusImaging.WinForms.TwainPro5.CapabilityContainerEnum":
                                    {
                                        PegasusImaging.WinForms.TwainPro5.CapabilityContainerEnum myCap = (CapabilityContainerEnum)capcontainer;
                                        int nIndex = 0;
                                        if (myCap != null)
                                        {
                                            float Current = ((CapabilityContainerOneValueFloat)myCap.CurrentValue).Value;
                                            float Default = ((CapabilityContainerOneValueFloat)myCap.DefaultValue).Value;


                                            regIndex = -1;

                                            for (; nIndex < myCap.Values.Count; nIndex++)
                                            {
                                                float fResult = ((CapabilityContainerOneValueFloat)myCap.Values[nIndex]).Value;// (float)myCap.Values[1];
                                                Capability cap = (Capability)capability;
                                                string strResult = twainDevice.GetCapabilityConstantDescription(cap, nIndex);

                                                if (strResult == null)
                                                {
                                                    strResult = "";
                                                }
                                                if (strResult == "")
                                                {
                                                    strResult = fResult.ToString();
                                                }

                                                if ((CheckValue == fResult) && (capability == AdvancedCapability.IcapPixelType))
                                                {
                                                    strResult = "Color";
                                                }

                                                ComboCollection myCollection = new ComboCollection(strResult, fResult);
                                                if (myCollection != null)
                                                {
                                                    if (ReturnAll == true)
                                                    {
                                                        ComboCollections.Add(myCollection);

                                                    }

                                                    if (RegValue == strResult)
                                                    {
                                                        if (ReturnAll == false)
                                                        {
                                                            ComboCollections.Add(myCollection);

                                                            return ComboCollections;
                                                        }
                                                        regIndex = nIndex;
                                                    }
                                                    if (fResult == Current)
                                                    {

                                                        curIndex = nIndex;
                                                    }
                                                    if (fResult == Default)
                                                    {

                                                        defIndex = nIndex;
                                                    }
                                                    myCollection.Dispose();
                                                    myCollection = null;
                                                }
                                                else
                                                {
                                                    _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerEnum. " + strResult + "  " + fResult;
                                                    AuditLogErrorMessage(_ErrorMessage);

                                                }


                                            }
                                            nIndex = myCap.Values.Count - 1;

                                            if ((regIndex == -1) && (RegValue != ""))
                                            {
                                                using (ComboCollection myAnotherCollection = new ComboCollection(RegValue, regFloat))
                                                {
                                                    if (myAnotherCollection != null)
                                                    {
                                                        nIndex++;
                                                        ComboCollections.Add(myAnotherCollection);
                                                        if (ReturnAll == false)
                                                        {
                                                            return ComboCollections;
                                                        }


                                                        regIndex = nIndex - 1;
                                                    }
                                                    else
                                                    {
                                                        _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerEnum. " + regIndex;
                                                        AuditLogErrorMessage(_ErrorMessage);

                                                    }
                                                }
                                            }

                                            if (curIndex == -1)
                                            {
                                                using (ComboCollection myAnotherCollection = new ComboCollection("Current", Current))
                                                {
                                                    if (myAnotherCollection != null)
                                                    {
                                                        nIndex++;
                                                        if (ReturnAll == true)
                                                        {
                                                            ComboCollections.Add(myAnotherCollection);//
                                                        }

                                                        if (regIndex == -1)
                                                        {
                                                            if (ReturnAll == false)
                                                            {
                                                                ComboCollections.Add(myAnotherCollection);
                                                                return ComboCollections;
                                                            }
                                                            regIndex = nIndex - 1;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerEnum. " + curIndex;
                                                        AuditLogErrorMessage(_ErrorMessage);

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (regIndex == -1)
                                                {
                                                    if (ReturnAll == false)
                                                    {
                                                        using (ComboCollection myAnotherCollection = new ComboCollection("Current", Current))
                                                        {
                                                            if (myAnotherCollection != null)
                                                            {
                                                                nIndex++;
                                                                ComboCollections.Add(myAnotherCollection);
                                                                return ComboCollections;
                                                            }
                                                            regIndex = nIndex - 1;
                                                        }
                                                    }
                                                }
                                            }
                                            if (defIndex == -1)
                                            {
                                                using (ComboCollection myAnotherCollection = new ComboCollection("Default", Default))
                                                {
                                                    if (myAnotherCollection != null)
                                                    {
                                                        nIndex++;
                                                        if (ReturnAll == true)
                                                        {
                                                            ComboCollections.Add(myAnotherCollection);//
                                                        }
                                                        if (regIndex == -1)
                                                        {
                                                            if (ReturnAll == false)
                                                            {
                                                                ComboCollections.Add(myAnotherCollection);
                                                                return ComboCollections;
                                                            }
                                                            regIndex = nIndex - 1;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerEnum. " + defIndex;
                                                        AuditLogErrorMessage(_ErrorMessage);

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (regIndex == -1)
                                                {
                                                    if (ReturnAll == false)
                                                    {
                                                        using (ComboCollection myAnotherCollection = new ComboCollection("Default", Default))
                                                        {
                                                            if (myAnotherCollection != null)
                                                            {
                                                                nIndex++;
                                                                ComboCollections.Add(myAnotherCollection);
                                                                return ComboCollections;
                                                            }
                                                            regIndex = nIndex - 1;
                                                        }
                                                    }
                                                }
                                            }


                                        }

                                        break;
                                    }

                                // Type ARRAY returns a list of values, but no current or default values
                                // This is a less common type that many sources don't use
                                case "PegasusImaging.WinForms.TwainPro5.CapabilityContainerArray":
                                    {
                                        PegasusImaging.WinForms.TwainPro5.CapabilityContainerArray myCap = (CapabilityContainerArray)capcontainer;
                                        if (myCap != null)
                                        {
                                            regIndex = -1;

                                            for (int nIndex = 0; nIndex < myCap.Values.Count; nIndex++)
                                            {
                                                String strResult = "";
                                                float fResult = ((CapabilityContainerOneValueFloat)myCap.Values[nIndex]).Value;
                                                strResult = fResult.ToString();

                                                if ((CheckValue == fResult) && (capability == AdvancedCapability.IcapPixelType))
                                                {
                                                    strResult = "Color";
                                                }
                                                ComboCollection myCollection = new ComboCollection(strResult, fResult);
                                                if (myCollection != null)
                                                {
                                                    if (ReturnAll == true)
                                                    {
                                                        ComboCollections.Add(myCollection);//
                                                    }

                                                    if (RegValue == strResult)
                                                    {
                                                        if (ReturnAll == false)
                                                        {
                                                            ComboCollections.Add(myCollection);
                                                            return ComboCollections;

                                                        }
                                                        regIndex = nIndex;
                                                    }
                                                    if (myCollection != null) /////
                                                    {
                                                        myCollection.Dispose();
                                                        myCollection = null;
                                                    }
                                                }
                                                else
                                                {
                                                    _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerArray. " + strResult + " " + fResult;
                                                    AuditLogErrorMessage(_ErrorMessage);

                                                }

                                            }
                                            if ((regIndex == -1) && (RegValue != ""))
                                            {
                                                using (ComboCollection myAnotherCollection = new ComboCollection(RegValue, regFloat))
                                                {
                                                    if (myAnotherCollection != null)
                                                    {
                                                        ComboCollections.Add(myAnotherCollection);
                                                        if (ReturnAll == false)
                                                        {
                                                            return ComboCollections; //
                                                        }
                                                        regIndex = myCap.Values.Count;
                                                    }
                                                    else
                                                    {
                                                        _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerArray. " + defIndex;
                                                        AuditLogErrorMessage(_ErrorMessage);

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (regIndex == -1)
                                                {
                                                    if (ReturnAll == false)
                                                    {
                                                        if (myCap.Values.Count > 0)
                                                        {
                                                            float Current = ((CapabilityContainerOneValueFloat)myCap.Values[0]).Value;
                                                            using (ComboCollection myAnotherCollection = new ComboCollection("Current", Current))
                                                            {
                                                                if (myAnotherCollection != null)
                                                                {

                                                                    ComboCollections.Add(myAnotherCollection);
                                                                    return ComboCollections;
                                                                }
                                                                regIndex = 0;
                                                            }
                                                        }

                                                    }
                                                }
                                            }

                                        }

                                        break;
                                    }

                                // Returns a range of values as well as current and default values
                                case "PegasusImaging.WinForms.TwainPro5.CapabilityContainerRange":
                                    {
                                        PegasusImaging.WinForms.TwainPro5.CapabilityContainerRange myCap = (CapabilityContainerRange)capcontainer;
                                        if (myCap != null)
                                        {
                                            int myIndex = 0;
                                            regIndex = -1;
                                            String strResult = "";
                                            float nIndex;

                                            float increment = myCap.Step;
                                            float Default = myCap.Default;

                                            if (capability == AdvancedCapability.IcapXResolution)
                                            {
                                                increment = 50;
                                                nIndex = ((float)((int)((float)(myCap.Minimum / increment)))) * increment;
                                                if (nIndex < myCap.Minimum) nIndex += increment;
                                            }
                                            else
                                            {
                                                nIndex = myCap.Minimum;
                                            }
                                            for (; nIndex <= myCap.Maximum; nIndex += increment)
                                            {
                                                myIndex++;
                                                if (capability == AdvancedCapability.IcapXResolution)
                                                {
                                                    strResult = nIndex.ToString();
                                                }
                                                else
                                                {
                                                    strResult = myIndex.ToString();
                                                }
                                                //cmbBox.Items.Add(strResult);

                                                if ((CheckValue == nIndex) && (capability == AdvancedCapability.IcapPixelType))
                                                {
                                                    strResult = "Color";
                                                }
                                                ComboCollection myCollection = new ComboCollection(strResult, nIndex);
                                                if (myCollection != null)
                                                {
                                                    if (ReturnAll == true)
                                                    {
                                                        ComboCollections.Add(myCollection);
                                                    }

                                                    if (RegValue == strResult)
                                                    {
                                                        if (ReturnAll == false)
                                                        {
                                                            ComboCollections.Add(myCollection);
                                                            return ComboCollections;
                                                        }
                                                        regIndex = myIndex - 1;

                                                    }
                                                    if (Default == nIndex)
                                                    {
                                                        defIndex = myIndex - 1;
                                                    }
                                                    if (myCollection != null) ///
                                                    {
                                                        myCollection.Dispose();
                                                        myCollection = null;
                                                    }
                                                }
                                                else
                                                {
                                                    _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerRange. " + strResult + " " + nIndex;
                                                    AuditLogErrorMessage(_ErrorMessage);

                                                }
                                            }
                                            if ((regIndex == -1) && (RegValue != ""))
                                            {
                                                using (ComboCollection myAnotherCollection = new ComboCollection(RegValue, regFloat))
                                                {
                                                    if (myAnotherCollection != null)
                                                    {
                                                        myIndex++;
                                                        ComboCollections.Add(myAnotherCollection);//
                                                        if (ReturnAll == false)
                                                        {
                                                            return ComboCollections;
                                                        }

                                                        regIndex = myIndex - 1;
                                                    }
                                                    else
                                                    {
                                                        _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerRange. " + regIndex;
                                                        AuditLogErrorMessage(_ErrorMessage);

                                                    }
                                                }
                                            }

                                            if (defIndex == -1)
                                            {
                                                using (ComboCollection myAnotherCollection = new ComboCollection("Default", Default))
                                                {
                                                    if (myAnotherCollection != null)
                                                    {
                                                        myIndex++;
                                                        if (ReturnAll == true)
                                                        {
                                                            ComboCollections.Add(myAnotherCollection);//
                                                        }

                                                        if (regIndex == -1)
                                                        {
                                                            if (ReturnAll == false)
                                                            {
                                                                ComboCollections.Add(myAnotherCollection);
                                                                return ComboCollections;
                                                            }
                                                            regIndex = myIndex - 1;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        _ErrorMessage = "PegasusImaging.WinForms.TwainPro5.CapabilityContainerRange. " + defIndex;
                                                        AuditLogErrorMessage(_ErrorMessage);

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (regIndex == -1)
                                                {
                                                    if (ReturnAll == false)
                                                    {
                                                        float Current = nIndex;
                                                        using (ComboCollection myAnotherCollection = new ComboCollection("Current", Current))
                                                        {
                                                            if (myAnotherCollection != null)
                                                            {
                                                                myIndex++;
                                                                ComboCollections.Add(myAnotherCollection);
                                                                return ComboCollections;
                                                            }
                                                            regIndex = myIndex - 1;
                                                        }
                                                    }
                                                }
                                            }



                                        }

                                        break;
                                    }
                            }


                        }

                    }
                    catch (System.InvalidCastException ex)
                    {
                        _ErrorMessage = ex.Message;
                        AuditLogErrorMessage(_ErrorMessage);
                    }
                    catch (System.StackOverflowException ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                    }

                }
            }
            if ((regIndex == -1) && (RegValue != ""))
            {
                using (ComboCollection myAnotherCollection = new ComboCollection(RegValue, regFloat))
                {
                    if (myAnotherCollection != null)
                    {
                        ComboCollections.Add(myAnotherCollection);
                        if (ReturnAll == false)
                        {
                            return ComboCollections;
                        }

                        regIndex = 0;
                    }
                    else
                    {
                        _ErrorMessage = "Finally. " + regIndex;
                        AuditLogErrorMessage(_ErrorMessage);

                    }
                }
            }


            return ComboCollections;


        }
        #endregion "Return type - ComboCollection(GetScannerSettings)"

        #region "GetAndSetScannerSettingsSize"
        public void GetAndSetScannerSettingsSize(ref TwainDevice twainDevice, ref float CardWidth, ref float CardLength)
        {

            String _ErrorMessage = "";



            ArrayList ComboCollections = new ArrayList();


            if (ComboCollections == null)
            {
                _ErrorMessage = "Unable to allocate memory in this SetScannerSetting routine. ";
                AuditLogErrorMessage(_ErrorMessage);
                return;
            }




            int regIndex = -1;
            int curIndex = -1;
            int defIndex = -1;

            #region "Card Min and Max value"


            RectangleF rectF = new RectangleF(1f, 1f, 8.5f, 14f);
            CardValidator(rectF, ref CardWidth, ref CardLength, true);

            #endregion "Card Min and Max value"



            ArrayList myCollections = GetScannerSettings("", ref twainDevice, AdvancedCapability.IcapSupportedSizes, true, ref regIndex, ref curIndex, ref defIndex);


            if (twainDevice.IsCapabilitySupported(Capability.IcapSupportedSizes) == true)
            {

                float myMininumValue = float.MaxValue;
                ComboCollection myClsObjSupportedSize = null;
                int myIndexs = -1;



                for (int i = 0; i <= myCollections.Count - 1; i++)
                {
                    myClsObjSupportedSize = (ComboCollection)myCollections[i];
                    CapabilityContainerOneValueFloat myCap = new CapabilityContainerOneValueFloat(Capability.IcapSupportedSizes);
                    if (myCap != null)
                    {
                        //myCap.Value = myClsObjSupportedSize.MyIndexs;
                        myCap.Value = myClsObjSupportedSize.MyValues;
                        twainDevice.SetCapability(myCap);
                    }
                    RectangleF rectFlt = twainDevice.ImageLayout;


                    float myWidth = rectFlt.Width;


                    float myHeight = rectFlt.Height;

                    float myWidthAnswer = myWidth * 25;
                    float myHeightAnswer = myHeight * 25;
                    if ((myHeight >= CardLength) && (myWidth >= CardWidth))
                    {
                        float myDistance = (myHeight - CardLength) * (myHeight - CardLength) + (myWidth - CardWidth) * (myWidth - CardWidth);
                        if (myDistance < myMininumValue)
                        {
                            myMininumValue = myDistance;
                            myIndexs = i;
                        }
                    }

                }
                if (myIndexs != -1)
                {
                    ComboCollection myCombo = new ComboCollection("", ((ComboCollection)myCollections[myIndexs]).MyValues);
                    SetScannerSettings(ref twainDevice, AdvancedCapability.IcapSupportedSizes, myCombo);
                }
                else
                {
                    ComboCollection myCombo = new ComboCollection("", 0);
                    SetScannerSettings(ref twainDevice, AdvancedCapability.IcapSupportedSizes, myCombo);
                }



            }//Checking for the Capabilitysize supported
        }
        #endregion "GetAndSetScannerSettingsSize"

        #region "Validate CardScanner"
        public bool CardValidator(RectangleF rectF, ref float CardWidth, ref float CardLength, bool SetOrValidate)
        {
            float minWidthvalue = 0;
            float maxWidthvalue = 0;
            float minHeightvalue = 0;
            float maxHeigthvalue = 0;
            float fCardWidth = Convert.ToInt32(Math.Floor(CardWidth * 100 / 25)) * 0.25f;
            float fCardLength = Convert.ToInt32(Math.Floor(CardLength * 100 / 25)) * 0.25f;

            if ((rectF.X > fCardWidth) || (rectF.Y > fCardLength) || (rectF.Width < fCardWidth) || (rectF.Height < fCardLength))
            {
                if (SetOrValidate == false)
                {
                    MessageBox.Show("Invalid CardWidth and CardLength", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    if ((rectF.X > CardWidth))
                    {
                        CardWidth = minWidthvalue;
                    }
                    if ((rectF.Y > CardLength))
                    {
                        CardLength = minHeightvalue;
                    }
                    if ((rectF.Width < CardWidth))
                    {
                        CardWidth = maxWidthvalue;
                    }
                    if ((rectF.Height < CardLength))
                    {
                        CardLength = maxHeigthvalue;
                    }
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        public RectangleF GetMinMaxHeightWidth(ref TwainDevice twainDevice)
        {


            RectangleF rectF = new RectangleF(1f, 1f, 8.5f, 14.0f);
            if (rectF == null)
            {
                return rectF;
            }
            try
            {
                #region "Card Min and Max value"
                string RegistryValue = "";
                ComboCollection myCardCollection = null;

                //width

                System.Windows.Forms.ComboBox cmbbox = null;
                //Min 
                myCardCollection = GetScannerSettings(RegistryValue, ref twainDevice, AdvancedCapability.IcapMinimumWidth, ref cmbbox);

                if (myCardCollection != null)
                {
                    rectF.X = myCardCollection.MyValues;

                }


                //Max
                myCardCollection = GetScannerSettings(RegistryValue, ref twainDevice, AdvancedCapability.IcapPhysicalWidth, ref cmbbox);
                if (myCardCollection != null)
                {
                    rectF.Width = myCardCollection.MyValues;

                }



                //height

                myCardCollection = GetScannerSettings(RegistryValue, ref twainDevice, AdvancedCapability.IcapMinimumHeight, ref cmbbox);

                //MinHeight
                if (myCardCollection != null)
                {
                    rectF.Y = myCardCollection.MyValues;

                }


                //MaxHeight
                myCardCollection = GetScannerSettings(RegistryValue, ref twainDevice, AdvancedCapability.IcapPhysicalHeight, ref cmbbox);
                if (myCardCollection != null)
                {
                    rectF.Height = myCardCollection.MyValues;

                }

            }
            catch (Exception ex)
            {

                string _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                //return false;
            }
            return rectF;
                #endregion "Card Min and Max value"
        }
        #endregion "Validate CardScanner"

        public void ObtainScannerSettings(ref TwainDevice twainDevice, ref System.Windows.Forms.ComboBox cmbbox, ref System.Windows.Forms.ComboBox cmbScanner, ref System.Windows.Forms.ComboBox cmbScanMode, ref System.Windows.Forms.ComboBox cmbBitDepth, ref System.Windows.Forms.ComboBox cmbResolution, ref System.Windows.Forms.ComboBox cmbBrightness, ref System.Windows.Forms.ComboBox cmbContrast, ref System.Windows.Forms.ComboBox cmbScanSide, ref System.Windows.Forms.CheckBox chkShowScannerDialog, ref System.Windows.Forms.ComboBox cmbSupportedSize, ref System.Windows.Forms.TextBox txtCardWidth, ref System.Windows.Forms.TextBox txtCardLength, ref System.Windows.Forms.TextBox txtStartX, ref System.Windows.Forms.TextBox txtStartY, ref RectangleF myScanLayout)
        {

            try
            {
                #region "Scanner Setting Default Data"

                if (semaphore == true)
                {
                    return;
                }
                semaphore = true;

                if (twainDevice != null)
                {
                    try
                    {
                        twainDevice.OpenSession();
                    }
                    catch
                    {
                    }
                }
                //scan mode
                if ((cmbbox == cmbScanner))
                {
                    GetScannerSettings(ref twainDevice, AdvancedCapability.IcapPixelType, ref cmbScanMode, gloRegistrySetting.gstrDMSScanMode);//
                }
                SetScannerSettings(ref twainDevice, AdvancedCapability.IcapPixelType, ref cmbScanMode);

                //bitDepth
                if ((cmbbox == cmbScanner) || (cmbbox == cmbScanMode))
                {
                    GetScannerSettings(ref twainDevice, AdvancedCapability.IcapBitDepth, ref cmbBitDepth, gloRegistrySetting.gstrDMSScanDepth);
                }
                SetScannerSettings(ref twainDevice, AdvancedCapability.IcapBitDepth, ref cmbBitDepth);


                //unitcap
                PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat myUnitsCaps = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                if (myUnitsCaps != null)
                {
                    if (twainDevice.IsCapabilitySupported(AdvancedCapability.IcapUnits) == true)
                    {
                        myUnitsCaps.Value = (float)PegasusImaging.WinForms.TwainPro5.CapabilityConstants.TwunInches;
                        twainDevice.SetCapability(myUnitsCaps);
                    }
                }
                //Resolution
                if ((cmbbox == cmbScanner) || (cmbbox == cmbScanMode) || (cmbbox == cmbBitDepth))
                {

                    GetScannerSettings(ref twainDevice, AdvancedCapability.IcapXResolution, ref cmbResolution, gloRegistrySetting.gstrDMSResol);
                }

                SetScannerSettings(ref twainDevice, AdvancedCapability.IcapXResolution, ref cmbResolution);
                SetScannerSettings(ref twainDevice, AdvancedCapability.IcapYResolution, ref cmbResolution);

                if ((cmbbox == cmbScanner) || (cmbbox == cmbScanMode) || (cmbbox == cmbBitDepth) || (cmbbox == cmbResolution))
                {
                    //Brightness
                    if (twainDevice.IsCapabilitySupported(AdvancedCapability.IcapBrightness) == true)
                    {
                        GetScannerSettings(ref twainDevice, AdvancedCapability.IcapBrightness, ref cmbBrightness, gloRegistrySetting.gstrDMSBright);
                    }
                    else
                    {
                        ComboCollection myCollection = new ComboCollection("96", -32);
                        ArrayList ComboCollections = new ArrayList();
                        ComboCollections.Add(myCollection);

                        cmbBrightness.DataSource = ComboCollections;
                        cmbBrightness.DisplayMember = "MyStrings";
                        cmbBrightness.ValueMember = "MyValues";
                        cmbBrightness.SelectedIndex = 0;

                    }
                }
                SetScannerSettings(ref twainDevice, AdvancedCapability.IcapBrightness, ref cmbBrightness);


                if ((cmbbox == cmbScanner) || (cmbbox == cmbScanMode) || (cmbbox == cmbBitDepth) || (cmbbox == cmbResolution))
                {
                    //Contrast
                    if (twainDevice.IsCapabilitySupported(AdvancedCapability.IcapContrast) == true)
                    {
                        GetScannerSettings(ref twainDevice, AdvancedCapability.IcapContrast, ref cmbContrast, gloRegistrySetting.gstrDMSContrast);
                    }
                    else
                    {

                        ComboCollection myCollection = new ComboCollection("96", -32);
                        ArrayList ComboCollections = new ArrayList();
                        ComboCollections.Add(myCollection);

                        cmbContrast.DataSource = ComboCollections;
                        cmbContrast.DisplayMember = "MyStrings";
                        cmbContrast.ValueMember = "MyValues";
                        cmbContrast.SelectedIndex = 0;
                    }
                }
                SetScannerSettings(ref twainDevice, AdvancedCapability.IcapContrast, ref cmbContrast);


                //Scan side
                if (twainDevice.IsCapabilitySupported(AdvancedCapability.CapDuplexEnabled) == true)
                {
                    //                    SetScannerSetting(Capability.CapDuplexEnabled, cmbScanSide, gloRegistrySetting.gstrDMSScanSide);
                    ComboCollection myCollection = new ComboCollection("Front Side", 0);
                    ArrayList ComboCollections = new ArrayList();
                    ComboCollections.Add(myCollection);
                    myCollection = new ComboCollection("Duplex", 1);
                    ComboCollections.Add(myCollection);
                    cmbScanSide.DataSource = ComboCollections;
                    cmbScanSide.DisplayMember = "MyStrings";
                    cmbScanSide.ValueMember = "MyValues";
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                    {
                        return;
                    }
                    else
                    {
                        object oSetting_DMSDuplexFront = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanSide);
                        gloRegistrySetting.CloseRegistryKey();
                        string DMSDuplexFont = oSetting_DMSDuplexFront.ToString();
                        if (DMSDuplexFont.Trim() == "Duplex")
                        {
                            cmbScanSide.SelectedIndex = 1;
                        }
                        else
                        {
                            cmbScanSide.SelectedIndex = 0;
                        }
                    }   //cmbContrast.SelectedIndex = (int) GetScannerSettingOneValue(Capability.CapDuplexEnabled); 
                }
                else
                {

                    ComboCollection myCollection = new ComboCollection("Front Side", 0);
                    ArrayList ComboCollections = new ArrayList();
                    ComboCollections.Add(myCollection);
                    cmbScanSide.DataSource = ComboCollections;
                    cmbScanSide.DisplayMember = "MyStrings";
                    cmbScanSide.ValueMember = "MyValues";
                    cmbScanSide.SelectedIndex = 0;

                }

                SetScannerSettings(ref twainDevice, AdvancedCapability.CapDuplexEnabled, ref cmbScanSide);


                //Supported Size
                GetScannerSettings(ref twainDevice, AdvancedCapability.IcapSupportedSizes, ref cmbSupportedSize, gloRegistrySetting.gstrDMSupporedSize);
                SetScannerSettings(ref twainDevice, AdvancedCapability.IcapSupportedSizes, ref cmbSupportedSize);



                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                {
                    return;
                }
                object oSetting_DMSShowScanner = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSShowScann);

                if (oSetting_DMSShowScanner != null)
                {
                    if (Convert.ToString(oSetting_DMSShowScanner) != "")
                    {

                        bool _ScanInterface = Convert.ToBoolean(oSetting_DMSShowScanner);
                        if (_ScanInterface == true)
                        {
                            twainDevice.ShowUserInterface = true;
                            chkShowScannerDialog.Checked = true;
                        }
                        else
                        {
                            twainDevice.ShowUserInterface = false;
                            chkShowScannerDialog.Checked = false;
                        }
                    }
                }



                myScanLayout = GetMinMaxHeightWidth(ref twainDevice);





                //checking the Card Length and width
                //Width
                System.Drawing.RectangleF twainImageLayout = new RectangleF();
                try
                {
                    if (twainDevice.ImageLayout != null)
                    {
                        twainImageLayout = twainDevice.ImageLayout;
                    }
                }
                catch
                {
                }
                txtCardWidth.Text = twainImageLayout.Width.ToString();
                object oSetting_DMSCardWidth = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardWidth);
                if (oSetting_DMSCardWidth != null)
                {
                    txtCardWidth.Text = oSetting_DMSCardWidth.ToString().Trim();
                }
                if (txtCardWidth.Text == "")
                {
                    txtCardWidth.Text = "4";
                }
                float CardWidth = (float)Convert.ToDouble(txtCardWidth.Text.Trim());//Setting Used against the cardValidator


                //Length
                txtCardLength.Text = twainImageLayout.Height.ToString();
                object oSetting_DMSCardLength = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardLength);
                if (oSetting_DMSCardLength != null)
                {
                    txtCardLength.Text = oSetting_DMSCardLength.ToString();
                }
                if (txtCardLength.Text == "")
                {
                    txtCardLength.Text = "4";
                }
                float CardHeight = (float)Convert.ToDouble(txtCardLength.Text.Trim());//Setting Used against the cardValidator

                //Start X
                txtStartX.Text = twainImageLayout.Location.X.ToString();
                object oSetting_DMSCardX = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardLeftX);
                if (oSetting_DMSCardX != null)
                {
                    txtStartX.Text = oSetting_DMSCardX.ToString().Trim();
                }
                if (txtStartX.Text == "")
                {
                    txtStartX.Text = "0";
                }

                //Start Y
                txtStartY.Text = twainImageLayout.Location.Y.ToString();
                object oSetting_DMSCardY = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardTopY);
                if (oSetting_DMSCardY != null)
                {
                    txtStartY.Text = oSetting_DMSCardY.ToString();
                }
                if (txtStartY.Text == "")
                {
                    txtStartY.Text = "0";
                }
                gloRegistrySetting.CloseRegistryKey();


                #endregion
            }
            catch (Exception ex)
            {
                String _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            if (twainDevice != null)
            {
                twainDevice.CloseSession();
            }
            semaphore = false;
            return;
        }

        public void AuditLogErrorMessage(string _ErrorMessage)
        {
            string _MessageString = "";
            if (_ErrorMessage.Trim() != "")
            {
                _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                _MessageString = "";
            }


        }
        #endregion "Public Function"

        #region "Disposing Object"
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }
            disposed = true;
        }
        #endregion "Disposing Object"





    }




}
