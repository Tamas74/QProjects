using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WIA;
using gloScanWIA;
using System.Windows.Forms;

namespace gloRemoteScanGeneral
{
    public static class RemoteScanSettings
    {
        public static string sErrorMsg { get; set; }
        public static ScanMasterConfigMasterConfig oMasterConfig = null;
        public static string sScanSettingsFileName = null;
        public static ScannersInstalledScanners oScannerSettings = null;
        public static ScannerCurrentSettingsScannerSettings oCurrentSetting = null;
        public static string[] supportedSizes ={"None", // 0,
        "A4", // 1,             ---A4Letter
        "B5 Letter", // 2,
        "US Letter", // 3,
        "US Legal", // 4,
        /* Added 1.5 */
        "A5", // 5,
        "B4", // 6,
        "B6", // 7,
        "Illegal",
        /* Added 1.7 */
        "US Ledger", // 9,
        "US Executive", // 10,
        "A3", // 11,
        "B3", // 12,
        "A6", // 13,
        "C4", // 14,
        "C5", // 15,
        "C6", // 16,
        /* Added 1.8 */
        "4A0", // 17,
        "2A0", // 18,
        "A0", // 19,
        "A1", // 20,
        "A2", // 21,
        //A4= A4Letter,
        "A7", // 22,
        "A8", // 23,
        "A9", // 24,
        "A10", // 25,
        "ISOB0", // 26,
        "ISOB1", // 27,
        "ISOB2", // 28,
        "ISOB3", // B3,
        "ISOB4", // B4,
        "B5", // 29,        ---ISOB5
        //"ISOB6", // B6,
        "ISOB7", // 30,
        "ISOB8", // 31,
        "ISOB9", // 32,
        "ISOB10", // 33,
        "JISB0", // 34,
        "JISB1", // 35,
        "JISB2", // 36,
        "JISB3", // 37,
        "JISB4", // 38,
        //JISB5", // B5Letter,
        "JISB6", // 39,
        "JISB7", // 40,
        "JISB8", // 41,
        "JISB9", // 42,
        "JISB10", // 43,
        "C0", // 44,
        "C1", // 45,
        "C2", // 46,
        "C3", // 47,
        "C7", // 48,
        "C8", // 49,
        "C9", // 50,
        "C10", // 51,
        "US Statement", // 52,
        "BusinessCard" // 53
      };

        public static string[] supportedSizesFromScanner ={"None", // 0,
        "A4Letter", // 1,
        "B5Letter", // 2,
        "USLetter", // 3,
        "USLegal", // 4,
        /* Added 1.5 */
        "A5", // 5,
        "B4", // 6,
        "B6", // 7,
        "Illegal",
        /* Added 1.7 */
        "USLedger", // 9,
        "USExecutive", // 10,
        "A3", // 11,
        "B3", // 12,
        "A6", // 13,
        "C4", // 14,
        "C5", // 15,
        "C6", // 16,
        /* Added 1.8 */
        "_4A0", // 17,
        "_2A0", // 18,
        "A0", // 19,
        "A1", // 20,
        "A2", // 21,
        //A4", // A4Letter,
        "A7", // 22,
        "A8", // 23,
        "A9", // 24,
        "A10", // 25,
        "ISOB0", // 26,
        "ISOB1", // 27,
        "ISOB2", // 28,
        "ISOB3", // B3,
        "ISOB4", // B4,
        "ISOB5", // 29,
        //"ISOB6", // B6,
        "ISOB7", // 30,
        "ISOB8", // 31,
        "ISOB9", // 32,
        "ISOB10", // 33,
        "JISB0", // 34,
        "JISB1", // 35,
        "JISB2", // 36,
        "JISB3", // 37,
        "JISB4", // 38,
        //JISB5", // B5Letter,
        "JISB6", // 39,
        "JISB7", // 40,
        "JISB8", // 41,
        "JISB9", // 42,
        "JISB10", // 43,
        "C0", // 44,
        "C1", // 45,
        "C2", // 46,
        "C3", // 47,
        "C7", // 48,
        "C8", // 49,
        "C9", // 50,
        "C10", // 51,
        "US Statement", // 52,
        "BusinessCard" // 53
       };

        //public static int getValueFromSupportedSizeFromRegistry(String value)
        //{
        //    for (int i = 0; i < supportedSizes.Length; i++)
        //    {
        //        if (value == supportedSizes[i])
        //        {
        //            return i;
        //        }
        //    }
        //    return 0;
        //}

        public static Int32 nBrightnessMin = 0;
        public static Int32 nBrightnessMax = 0;
        public static Int32 nBrightnessStep = 1;
        public static Int32 nContrastMin = 0;
        public static Int32 nContrastMax = 0;
        public static Int32 nContrastStep = 1;
        public static Int32 nResolutionMin = 0;
        public static Int32 nResolutionMax = 0;
        public static Int32 nResolutionStep = 1;
        public static bool bBrightness = false;
        public static bool bContrast = false;
        public static bool bResolution = false;

        public static int getValueFromSupportedSizeFromScanner(String value)
        {
            for (int i = 0; i < supportedSizesFromScanner.Length; i++)
            {
                if (value == supportedSizesFromScanner[i])
                {
                    return i;
                }
            }
            return 0;
        }

        public static Int32 GetCalculatedCapValue(Int32 nMin, Int32 nMax, Int32 nInpNum)
        {
            Int32 nRange = 0;
            Int32 nOutNum = 0;
            try
            {
                nRange = (nMax - nMin);

                nOutNum = (int)Math.Truncate(((double)(nInpNum - nMin) / (double)nRange) * 255.0);

            }
            catch (Exception)
            {
                nOutNum=0;
            }
            return nOutNum;
        }

        public static Int32 GetCalculatedRevCapValue(Int32 nMin, Int32 nMax, Int32 nInpNum)
        {
            Int32 nRange = 0;
            Int32 nOutNum = 0;
            try
            {
                nRange = (nMax - nMin);

                nOutNum = (int)Math.Truncate((((double)nInpNum / 255) * nRange) + nMin);

            }
            catch (Exception)
            {
                nOutNum = 0;
            }
            return nOutNum;
        }

        public static void GetCapMaxMinForWIA(Item wia_item)
        {
            bBrightness = false;
            bContrast = false;
            bResolution = false;

            if (wia_item != null)
            {
                foreach (Property prop in wia_item.Properties) //WIA Properties
                {
                    try
                    {

                        if (bBrightness && bContrast && bResolution)
                        { return; }

                        switch (prop.Name)
                        {
                            case "Brightness":
                                var _Brightness = wia_item.Properties.get_Item("6154");
                                if (_Brightness != null)
                                {
                                    nBrightnessMin = Convert.ToInt32(_Brightness.SubTypeMin);
                                    nBrightnessMax = Convert.ToInt32(_Brightness.SubTypeMax);
                                    nBrightnessStep = Convert.ToInt32(_Brightness.SubTypeStep);
                                    if (nBrightnessStep == 0)
                                    {
                                        nBrightnessStep = 1;
                                    }
                                    bBrightness = true;
                                }
                                break;
                            case "Contrast":
                                var _Contrast = wia_item.Properties.get_Item("6155");
                                if (_Contrast != null)
                                {
                                    nContrastMin = Convert.ToInt32(_Contrast.SubTypeMin);
                                    nContrastMax = Convert.ToInt32(_Contrast.SubTypeMax);
                                    nContrastStep = Convert.ToInt32(_Contrast.SubTypeStep);
                                    if (nContrastStep == 0)
                                    {
                                        nContrastStep = 1;
                                    }
                                    bContrast = true;
                                }
                                break;
                            case "Horizontal Resolution":
                            case "Vertical Resolution":
                                var _Resolution = wia_item.Properties.get_Item("6147");
                                if (_Resolution != null)
                                {
                                    nResolutionMin = wia_item.Properties.get_Item("6147").SubTypeMin;
                                    nResolutionMin = Convert.ToInt32(_Resolution.SubTypeMin);
                                    nResolutionMax = Convert.ToInt32(_Resolution.SubTypeMax);
                                    nResolutionStep = Convert.ToInt32(_Resolution.SubTypeStep);
                                    if (nResolutionStep == 0)
                                    {
                                        nResolutionStep = 1;
                                    }
                                    bResolution = true;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    catch {  }
                }
            }
        }

        public static ScannerDevice SetWIAScannerCap(ScannerDevice ScannerDevice, ScannerCurrentSettingsScannerSettings objScannerSettings)
        {
            Item wia_item = null;
            try
            {
                if (ScannerDevice.Device != null)
                {
                    wia_item = ScannerDevice.Device.Items[1];
                }
                if (wia_item != null)
                {
                    gloRemoteScanGeneral.RemoteScanSettings.GetCapMaxMinForWIA(wia_item);
                }
            }
            catch (Exception) { }

            Int32 nResolution=0;
            //Resolution
            try
            {
                nResolution = Convert.ToInt32(objScannerSettings.ResolutionName);

                ScannerDevice.PictureSettings.HorizontalResolution = nResolution;
                ScannerDevice.PictureSettings.VerticalResolution = nResolution;
            }
            catch (Exception){}

            //Extent
            try
            {
                ScannerDevice.PictureSettings.HorizontalExtent = (int)(objScannerSettings.CardWidth * nResolution);
                ScannerDevice.PictureSettings.VerticalExtent = (int)(objScannerSettings.CardLength * nResolution);
            }
            catch (Exception) { }

            //Position
            try
            {
                ScannerDevice.PictureSettings.HorizontalStartPosition = (int)objScannerSettings.CardLeft;
                ScannerDevice.PictureSettings.VerticalStartPosition = (int)objScannerSettings.CardTop;
            }
            catch (Exception) { }

            //Brightness
            try
            {
                Int32 BrightnessName = 0;
                if (!gloRemoteScanGeneral.RemoteScanSettings.bBrightness)
                {
                    gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMax = 127;
                    gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMin = -128;
                    gloRemoteScanGeneral.RemoteScanSettings.nBrightnessStep = 1;
                }
                int nBrightness;
                int.TryParse(objScannerSettings.BrightnessName, out nBrightness);

                BrightnessName = RemoteScanSettings.GetCalculatedRevCapValue(gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMin, gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMax, nBrightness);
                
                ScannerDevice.PictureSettings.Brightness = BrightnessName;// nBrightness;
            }
            catch (Exception) { }

            
            //Contrast
            try
            {
                Int32 ContrastName = 0;
                if (!gloRemoteScanGeneral.RemoteScanSettings.bContrast)
                {
                    gloRemoteScanGeneral.RemoteScanSettings.nContrastMax = 127;
                    gloRemoteScanGeneral.RemoteScanSettings.nContrastMin = -128;
                    gloRemoteScanGeneral.RemoteScanSettings.nContrastStep = 1;
                }

                int nContrast;
                int.TryParse(objScannerSettings.ContrastName, out nContrast);

                ContrastName = RemoteScanSettings.GetCalculatedRevCapValue(gloRemoteScanGeneral.RemoteScanSettings.nContrastMin, gloRemoteScanGeneral.RemoteScanSettings.nContrastMax, nContrast);

                ScannerDevice.PictureSettings.Contrast = ContrastName;
            }
            catch (Exception) { }

            //Scan Side
            bool bDocHandlingSet = false;
            try
            {
                string[] sScanSideWithFeeder = objScannerSettings.ScanSideName.Split(';');
                string sScanSide = "";
                string sFeeder = "";
                if (sScanSideWithFeeder.Length > 0)
                {
                    sScanSide = sScanSideWithFeeder[0];
                    if (sScanSideWithFeeder.Length > 1)
                    {
                        sFeeder = sScanSideWithFeeder[1];
                    }
                }

                if (sScanSide == "Front Side")
                {
                    if (sFeeder == "flatbed")
                    {
                        ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Flatbed;
                    }
                    else
                    {
                        ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Feeder;
                    }
                }
                else if (sScanSide == "Duplex")
                {
                    try
                    {
                        if (sFeeder == "flatbed")
                        {
                            ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Duplex | DocumentHandlingSelect.Flatbed;
                        }
                        else if (sFeeder == "feeder")
                        {
                            ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Duplex | DocumentHandlingSelect.Feeder;
                        }
                        else
                        {
                            ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Duplex;
                        }
                    }
                    catch
                    {
                        ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Duplex;
                    }
                }
                else if (sScanSide == "Flat Bed")
                {
                    ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Flatbed;
                }
                bDocHandlingSet = true;
            }
            catch (Exception) { bDocHandlingSet = false; }

            try
            {
                if (bDocHandlingSet)
                {
                    ScannerDevice.DeviceSettings.Pages = ((ScannerDevice.DeviceSettings.DocumentHandlingSelect == DocumentHandlingSelect.Duplex) || (ScannerDevice.DeviceSettings.DocumentHandlingSelect == (DocumentHandlingSelect.Duplex | DocumentHandlingSelect.Feeder))) ? 2 : 1;
                }
                else
                {
                    ScannerDevice.DeviceSettings.Pages = 1;
                }
            }
            catch (Exception)
            {

            }

            //Intent
            try
            {
                if (objScannerSettings.ScanModeName == "BW" || objScannerSettings.ScanModeName == "Gray")
                {
                    ScannerDevice.PictureSettings.CurrentIntent = CurrentIntent.ImageTypeGrayscale;
                }
                else if (objScannerSettings.ScanModeName == "RGB")
                {
                    ScannerDevice.PictureSettings.CurrentIntent = CurrentIntent.ImageTypeColor;
                }
                else
                {
                    ScannerDevice.PictureSettings.CurrentIntent = CurrentIntent.ImageTypeText;
                }
            }
            catch (Exception) { }

            return ScannerDevice;
        }

        public static void getScallingForBrighnessAndCotrast(String value, out Int32 scale, out Int32 div)
        {
            div = 1;
            scale = 0;
            Int32.TryParse(value, out scale);
            if (scale == -1000)
            {
                scale = 101;
                div = 10;
            }
            else if (scale == -127)
            {
                scale = 128;
            }
            else if (scale == -128)
            {
                scale = 129;
            }
        }

        public static void SetScannerSettingsObject(bool bEliminatePegasus = false)
        {
            try
            {
                oScannerSettings = gloRemoteScanGeneral.gloRemoteScanMetaDataWriter.GetRemoteScanSettings(bEliminatePegasus);

            }
            catch
            {
                oScannerSettings = null;
            }
        }
       
        public static string CreateCurrentSettingsForRemoteScan()
        {
            //ScannerCurrentSettingsScannerSettings oCurrentSetting = null;
            string sToBeScannedFile = null;
            try
            {
                if (oCurrentSetting != null)
                {
                    //sToBeScannedFile = gloGlobal.clsFileExtensions.GetUniqueID() + ".xml";// System.Guid.NewGuid().ToString() 
                    sToBeScannedFile = gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_0" + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".xml";// System.Guid.NewGuid().ToString() 
                    string sToBeScannedPath = Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloRemoteScanSettings.ScanFolderName, gloGlobal.gloRemoteScanSettings.ToBeScannnedFolderName, sToBeScannedFile);

                    try
                    {
                        int nProcId = gloGlobal.gloProgressAndClipboard.getProcessIdInt();
                        oCurrentSetting.ScannerSettingsID = nProcId;
                    }
                    catch (Exception)
                    {
                        oCurrentSetting.ScannerSettingsID = 0;
                    }

                    if (gloRemoteScanMetaDataWriter.CreateXMLFile(oCurrentSetting, sToBeScannedPath))
                    {
                        return sToBeScannedFile;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                { }

            }
            catch (Exception)
            {

                throw;
            }
            return sToBeScannedFile;
        }

        public static bool RefreshScanners()
        {
            Boolean refreshed = false;
            if (gloGlobal.gloRemoteScanSettings.bZipScanSettings)
            {
                refreshed = CallScanRefreshForRemoteScan();
            }
            else
            {
                try
                {
                    String sScannerConfigDir = Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScanSettingsFolderName);

                    //Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloTSPrint.PrinterConfigDirectory);
                    //Delete existing .ups files
                    System.IO.DirectoryInfo di = new DirectoryInfo(sScannerConfigDir);
                    foreach (FileInfo file in di.GetFiles("*.uss"))
                    {
                        file.Delete();
                    }
                    foreach (FileInfo file in di.GetFiles("*.err"))
                    {
                        file.Delete();
                    }
                    using (var myFile = File.Create(Path.Combine(sScannerConfigDir, System.Guid.NewGuid().ToString() + ".rss")))
                    {
                        try
                        {
                            myFile.Close();
                        }
                        catch
                        {
                        }//SLR: it should be closed, if somebody has to delete
                        // interact with myFile here, it will be disposed automatically
                    }
                    bool exist = Directory.EnumerateFiles(sScannerConfigDir, "*.uss").Any();
                    bool existerr = Directory.EnumerateFiles(sScannerConfigDir, "*.err").Any();
                    Int16 attempts = 0;
                    while (!exist && attempts < 100 && !existerr)
                    {

                        attempts++;
                        System.Threading.Thread.Sleep(1000);
                        Application.DoEvents();
                        exist = Directory.EnumerateFiles(sScannerConfigDir, "*.uss").Any();
                        if (!exist)
                        {
                            existerr = Directory.EnumerateFiles(sScannerConfigDir, "*.err").Any();
                        }
                    }
                    if (exist == true)
                    {
                        refreshed = true;
                        SetScannerSettingsObject();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return refreshed;
        }

        //Code to refresh local scanners using clipboard
        private static Timer ScanRefreshTimer = null;
        //public static int imgCnt = 0;
        static Int16 attemptsToGetClipboard = 0;
        private static gloGlobal.gloClipboardWatcher myWatcher = null;

        //public enum ScanningStatus
        //{
        //    Scanning = 0,
        //    Regetting = 1,
        //    Waiting = 2
        //};

        private static void ScanTimerFired(object sender, EventArgs e)
        {
            //AuditLogErrorMessage("ScanTimerFired");
            //AuditLogErrorMessage("attempts : " + attempts);
            ScanRefreshTimer.Enabled = false;

            if (attemptsToGetClipboard < 50)
            {
                attemptsToGetClipboard++;
                if (attemptsToGetClipboard % 10 == 0)
                {
                    GetScannedFile(".again");
                    //using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + nCurrScanCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".again")))
                    //{
                    //    myFile.Close();
                    //    // interact with myFile here, it will be disposed automatically
                    //}
                }
                ScanRefreshTimer.Enabled = true;

            }
            else
            {
                if (attemptsToGetClipboard != 50)
                {
                    ScanRefreshTimer.Enabled = true;
                }
                else
                {
                    attemptsToGetClipboard = 0;
                    ReleaseScanTimer();
                }
            }
            //if (objfrmScanProgress != null)
            //{
            //    objfrmScanProgress.BringToFront();
            //}
        }
        private static bool bScannerStatus = false;
        private static void StartScanRefreshTimer()
        {
            bScannerStatus = false;
            if (ScanRefreshTimer == null)
            {
                ScanRefreshTimer = new Timer();
                ScanRefreshTimer.Interval = 2000;//5000;
                ScanRefreshTimer.Tick += ScanTimerFired;
            }
            if (ScanRefreshTimer != null)
            {
                ScanRefreshTimer.Enabled = true;
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
            //if (objfrmScanProgress == null)
            //{
            //    objfrmScanProgress = new frmScanProgress();
            //    objfrmScanProgress.BringToFront();
            //    objfrmScanProgress.lblCurrentProgress.Text = "Started Local Scanning...";
            //    objfrmScanProgress.prbarScanning.Maximum = 1;
            //    objfrmScanProgress.prbarScanning.Value = 0;
            //}
            //if (objfrmScanProgress != null)
            //{
            //    objfrmScanProgress.Show();
            //}
            //EnableTaskBarButtons(false);
            //Application.DoEvents();
        }
        private static bool bReleaseClipboardEvents = false;
        private static void ReleaseScanTimer()
        {

            if (!bScannerStatus)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Save, "Failure", gloAuditTrail.ActivityOutCome.Failure);
            }
            else
            {
                bScannerStatus = false;
            }
            if (ScanRefreshTimer != null)
            {
                ScanRefreshTimer.Enabled = false;
                ScanRefreshTimer.Stop();

                ScanRefreshTimer.Tick -= ScanTimerFired;
                ScanRefreshTimer.Dispose();
                ScanRefreshTimer = null;
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

            //if (objfrmScanProgress != null)
            //{
            //    objfrmScanProgress.Dispose();
            //    objfrmScanProgress = null;
            //}

            //EnableTaskBarButtons(true);
            //Application.DoEvents();
        }
        //static string sScanImageFormat;
        private static bool bRefreshStatus = false;
        private static bool CallScanRefreshForRemoteScan()
        {
            bRefreshStatus = false;
            try
            {
                gloGlobal.gloRemoteScanSettings.CurrLocalScanSettingBytes = null;
                StartScanRefreshTimer();
                // AuditLogErrorMessage("Before CreateCurrentSettingsForRemoteScan : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));

                string sScannedFilePath = string.Empty;
                try
                {
                    String sScannerConfigDir = Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScanSettingsFolderName);

                    ////Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloTSPrint.PrinterConfigDirectory);
                    ////Delete existing .ups files
                    //System.IO.DirectoryInfo di = new DirectoryInfo(sScannerConfigDir);
                    //foreach (FileInfo file in di.GetFiles("*.uss"))
                    //{
                    //    file.Delete();
                    //}
                    //foreach (FileInfo file in di.GetFiles("*.err"))
                    //{
                    //    file.Delete();
                    //}
                    using (var myFile = File.Create(Path.Combine(sScannerConfigDir, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + System.Guid.NewGuid().ToString() + ".rsc")))
                    {
                        myFile.Close(); //SLR: it should be closed, if somebody has to delete
                        // interact with myFile here, it will be disposed automatically
                    }
                    while (!bReleaseClipboardEvents)
                    {
                        Application.DoEvents();
                    }

                    //sScannedFilePath = gloRemoteScanGeneral.RemoteScanSettings.CreateCurrentSettingsForRemoteScan();
                    //if (string.IsNullOrEmpty(sScannedFilePath))
                    //{
                    //    gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                    //    if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                    //    {
                    //        ReleaseScanTimer();
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        ReleaseScanTimer();
                    //        MessageBox.Show("Unable to start a scan request . Please check whether gloLDSSniffer Service is running and local scan setting is enabled in service", "Local scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    //AuditLogErrorMessage(ex.ToString());
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                    if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                    {
                        ReleaseScanTimer();
                    }
                }
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
            }
            return bRefreshStatus;
        }

        static int nCurrScanCnt;
        static void myWatcher_OnClipboardContentChanged(object sender, EventArgs e)
        {
            //AuditLogErrorMessage("myWatcher_OnClipboardContentChanged: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            if (ScanRefreshTimer != null)
            {
                ScanRefreshTimer.Enabled = false;
                ScanRefreshTimer.Stop();
            }
            string sExtForScan = string.Empty;

            string sScanConfigTempPath = "ForScanRefresh";
            int nCurrProcId = gloGlobal.gloProgressAndClipboard.getProcessIdInt();
            //AuditLogErrorMessage("BEFORE GetImageFromClipboard" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            gloGlobal.gloProgressAndClipboard objgloProgressAndClipboard = gloGlobal.gloProgressAndClipboard.GetImageFromClipboard(3, sScanConfigTempPath, ref sExtForScan, nCurrProcId);
            if (objgloProgressAndClipboard != null)
            {

                //AuditLogErrorMessage("TOTAL COUNTS :  " + objgloProgressAndClipboard.objgloDataProgressBar.sequenceNo + " , " + objgloProgressAndClipboard.objgloDataProgressBar.totalcnt);
                //AuditLogErrorMessage("AFTER GetImageFromClipboard -- >" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                //AuditLogErrorMessage("sExtForScan: " + sExtForScan);
                if (objgloProgressAndClipboard.objgloDataClipboardCopy != null)
                {
                    if (sExtForScan == ".end" || sExtForScan == ".err")
                    {
                        ReleaseScanTimer();
                    }
                    else
                    {
                        if (sExtForScan == ".wait")
                        {
                            //RemoteScanning_ImageLoad(null, false, objgloProgressAndClipboard, ScanningStatus.Waiting);
                            GetScannedFile(".next");
                        }
                        else
                        {
                            //imgCnt++;
                            nCurrScanCnt++;
                            //AuditLogErrorMessage("Before Creating .Next file " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                            //using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + nCurrScanCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".next")))
                            //{
                            //    myFile.Close();
                            //    // interact with myFile here, it will be disposed automatically
                            //}

                            if (sExtForScan == ".xml")
                            {

                                //RemoteScanning_ImageLoad(sScanConfigTempPath + sExtForScan, false, objgloProgressAndClipboard, ScanningStatus.Scanning);
                                //gloGlobal.clsDatalog.CurrLocalScanSettingBytes = sScanConfigTempPath + sExtForScan;
                                bScannerStatus = true;
                                bRefreshStatus = true;
                            }
                            //else
                            //{ imgCnt--; }
                            //UpdateScanProgressBar(objgloProgressAndClipboard, false);
                            GetScannedFile(".next");
                        }
                    }
                }
                else
                {
                    //RemoteScanning_ImageLoad(null, false, objgloProgressAndClipboard, ScanningStatus.Waiting);
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
                //RemoteScanning_ImageLoad(null, false, objgloProgressAndClipboard, ScanningStatus.Regetting);

                GetScannedFile(".again");
            }
            if (ScanRefreshTimer != null)
            {
                ScanRefreshTimer.Enabled = true;
                ScanRefreshTimer.Start();
            }

        }

        private static void GetScannedFile(string sExt)
        {
            using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + nCurrScanCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + sExt)))
            {
                myFile.Close();
                // interact with myFile here, it will be disposed automatically
            }

        }

        //public static void RemoteScanning_ImageLoad(string ImgPath, bool bBarcode, gloGlobal.gloProgressAndClipboard objgloProgressAndClipboard, ScanningStatus eStatus)
        //{
            //if (eStatus == ScanningStatus.Scanning)
            //{
            //    try
            //    {
            //        icnt += 1;
            //        //if (!bBarcode)
            //        //{
            //        //    File.Copy(ImgPath, @"D:\RemoteScan\" + icnt.ToString());
            //        //}

            //        _nImageCount = _nImageCount + 1;
            //        int _rowCount = c1Documents.Rows.Count + 1;

            //        //To increment the count each time when image gets scanned
            //        nCountImageNo++;
            //        AddImageFileIntoGridForRemoteScan(Path.GetFileNameWithoutExtension(ImgPath), ImgPath);
            //        //End code add
            //        //Zoom();//For Intiallially Zoom to the Best Fit
            //        if (c1Documents != null)
            //        {
            //            if (c1Documents.RowSel >= 0)
            //            {
            //                if (Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)) != "")
            //                {

            //                    string _filePath = Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH));
            //                    if (File.Exists(_filePath) == true)
            //                    {
            //                        if (!_ImgFocus || c1Documents.Rows.Count == 1)
            //                        {
            //                            LoadImage(_filePath);
            //                            Zoom();
            //                            _ImgFocus = true;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        imageControl1._CurrZoomIndex = 11;
            //                    }
            //                }
            //                else
            //                {
            //                    imageControl1._CurrZoomIndex = 11;
            //                }
            //            }
            //        }
            //        UpdateScanProgressBar(objgloProgressAndClipboard, eStatus);
            //        Application.DoEvents();
            //    }
            //    catch (Exception ex)
            //    {
            //        _ErrorMessage = ex.ToString();
            //        AuditLogErrorMessage(_ErrorMessage);
            //        MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {

            //    }
            //}
            //else
            //{
            //    UpdateScanProgressBar(objgloProgressAndClipboard, eStatus);
            //}
        //}

        public static void SetWIASupportedSize(ref ScannersInstalledScannersScanner aScanner, string sSizeName, int nLeft, int nTop, double nWidth, double nLength, int nCnt)
        {
            ScannersInstalledScannersScannerSupportedSize aScanSupportedSize = null;
            try
            {
                aScanSupportedSize = new ScannersInstalledScannersScannerSupportedSize();
                aScanSupportedSize.SupportedSizeID = Convert.ToString(nCnt);
                aScanSupportedSize.Name = sSizeName;

                aScanSupportedSize.Left = Convert.ToString(nLeft);
                aScanSupportedSize.Top = Convert.ToString(nTop);
                aScanSupportedSize.Width = Convert.ToString(nWidth);
                aScanSupportedSize.Length = Convert.ToString(nLength);

                aScanner.SupportedSize[nCnt] = aScanSupportedSize;
            }
            catch //(Exception ex)
            {
                //logdata.ExceptionLog(ex);
            }
            finally
            {
                aScanSupportedSize = null;
            }
        }

    }
}
