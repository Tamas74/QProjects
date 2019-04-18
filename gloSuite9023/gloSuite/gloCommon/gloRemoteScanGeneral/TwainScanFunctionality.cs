using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Saraff.Twain;
using System.IO;
using WIA;
using gloScanWIA;
using System.Runtime.ExceptionServices;
using System.Security;


namespace gloRemoteScanGeneral
{
    public static class TwainScanFunctionality
    {
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public static bool CreateTwainScanSettingsFile()
        {
            bool bRes = true;
            //bool bFailed = false;
            try
            {
                Twain32 objTwain32 = null;
                List<string> arWIA = null;
                try
                {
                    arWIA = gloScanWIA.WiaDevice.GetWIAScannersList();
                }
                catch (Exception)
                {
                    arWIA = new List<string>();
                }

                bool bIsWIAScanner = false;
                ScannersInstalledScannersScanner aScanner = null;
                string sWIAscannername = null;
                ScannersInstalledScanners aScannerList = new ScannersInstalledScanners();
                int ScannerCount = 0;
                aScannerList.Scanner = null;
                int ScannerDevicesCount = arWIA.Count;
                bool bOpentDataSource = false;
                // objRemoteLauncher = RemoteScanLauncher;
                try
                {
                    //Remote Scanning
                    //if (ConfigSettings.SignaturePadSettings.EnableLocalSignaturePad == true) Remote Scan
                    objTwain32 = new Saraff.Twain.Twain32(gloGlobal.gloRemoteScanSettings.twainVersion);
                    objTwain32.AppProductName = "gloScan";
                    //if (RemoteScanLauncher == null)
                    //{
                    //    InitiateRemoteScanLauncher(bServiceByOption);
                    //}

                    //RemoteScanLauncher.TopMost = true;
                    //objTwain32.Parent = RemoteScanLauncher;
                    //  objTwain32.ShowUI = false;


                    //objTwain32.AcquireClosure += new EventHandler<Twain32.AcquireErrorEventArgs>(objTwain32_AcquireClosure);
                    //objTwain32.AcquireError += new EventHandler<Twain32.AcquireErrorEventArgs>(objTwain32_AcquireError);
                    //objTwain32.AcquireException += new EventHandler<Twain32.AcquireErrorEventArgs>(objTwain32_AcquireException);
                    //objTwain32.AcquireFailure += new EventHandler<Twain32.AcquireErrorEventArgs>(objTwain32_AcquireFailure);

                    bool bOpenDSM = false;
                    try
                    {
                        bOpenDSM = objTwain32.OpenDSM();
                    }
                    catch (TwainException ex)
                    {
                        bOpenDSM = false;
                        bRes = false;
                        //bFailed = true;
                        gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg = ex.Message;
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }
                    catch (Exception ex)
                    {
                        bOpenDSM = false;
                        bRes = false;
                        //bFailed = true;
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }

                    if (objTwain32 != null)
                    {
                        if (bOpenDSM)
                        {
                            //ScannersInstalledScanners aScannerList = new ScannersInstalledScanners();
                            bOpentDataSource = true;
                            aScannerList.Default = objTwain32.GetSourceProductName(objTwain32.SourceIndex);
                            try
                            {
                                ScannerDevicesCount = objTwain32.SourcesCount + arWIA.Count;
                            }
                            catch (Exception)
                            {
                                ScannerDevicesCount = arWIA.Count; //SLR: still it has to add wia devices
                                bRes = false;
                                //bFailed = true;
                            }

                            //bool bIsWIAScanner = false;
                            //Item wia_item = null;
                            //ScannerDevice wiascannerdevice = null;
                            //ScannersInstalledScannersScanner aScanner = null;
                            try
                            {
                                if (ScannerDevicesCount > 0)
                                {
                                    aScannerList.Scanner = new ScannersInstalledScannersScanner[ScannerDevicesCount];
                                    // int ScannerCount = 0;

                                    for (int i = 0; i < objTwain32.SourcesCount; i++)//ScannerDevicesCount
                                    {
                                        //Scanners
                                        aScanner = new ScannersInstalledScannersScanner();

                                        aScanner.ScannerID = Convert.ToString(ScannerCount);
                                        aScanner.Name = objTwain32.GetSourceProductName(i);
                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "Processing scanner : " + aScanner.Name, gloAuditTrail.ActivityOutCome.Success);
                                        if (aScanner.Name.StartsWith("WIA"))
                                        {
                                            bIsWIAScanner = true;
                                            sWIAscannername = aScanner.Name.Replace("WIA-", "");

                                        }
                                        else
                                        { bIsWIAScanner = false; }

                                        if (aScannerList.Default == aScanner.Name)
                                        { aScanner.IsDefault = "True"; }
                                        else
                                        { aScanner.IsDefault = "False"; }

                                        //ScanMode

                                        try
                                        {
                                            if (i == 0 || bOpentDataSource)
                                            {
                                                objTwain32.CloseDataSource();
                                                bOpentDataSource = false;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                        }
                                        bool bFoundInSkip = false;
                                        String currScanner = aScanner.Name.Trim().ToLower();
                                        for (int j = 0; j < gloGlobal.gloRemoteScanSettings.skipScannerList.Length; j++)
                                        {
                                            String skipName = gloGlobal.gloRemoteScanSettings.skipScannerList[j].Trim().ToLower();
                                            if (skipName != String.Empty)
                                            {
                                                if (currScanner.StartsWith(skipName))
                                                {
                                                    bFoundInSkip = true;
                                                    break;
                                                }
                                            }

                                        }
                                        if (bFoundInSkip)
                                        {
                                            continue;
                                        }
                                        //objTwain32.CloseDataSource();
                                        objTwain32.SourceIndex = i;
                                        bool notATwainScanner = false;
                                        //objTwain32.OpenDSM();
                                        try
                                        {
                                            if (!bIsWIAScanner)
                                            {
                                                bool bOpenDS = false;
                                                try
                                                {
                                                    bOpenDS = objTwain32.OpenDataSource();
                                                }
                                                catch (Exception ex)
                                                {
                                                    bOpentDataSource = false;
                                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                }

                                                if (bOpenDS)
                                                {
                                                    bOpentDataSource = true;
                                                    try
                                                    {
                                                        aScanner.ScanMode = null;
                                                        var _PixelType = objTwain32.Capabilities.PixelType.Get();

                                                        aScanner.ScanMode = new ScannersInstalledScannersScannerScanMode[_PixelType.Count];

                                                        int ScanModeCount = 0;

                                                        #region "Scan Mode"
                                                        for (int j = 0; j < _PixelType.Count; j++)
                                                        {
                                                            objTwain32.SetCap(TwCap.IPixelType, _PixelType[j]);
                                                            aScanner.Mode = "";// default/selected
                                                            ScannersInstalledScannersScannerScanMode aScanMode = new ScannersInstalledScannersScannerScanMode();
                                                            aScanMode.ScanModeID = Convert.ToString(ScanModeCount);
                                                            aScanMode.Name = Convert.ToString(_PixelType[j]);
                                                            try
                                                            {
                                                                aScanMode.ScanDepth = null;
                                                                var _Depth = objTwain32.Capabilities.BitDepth.Get();
                                                                aScanMode.ScanDepth = new ScannersInstalledScannersScannerScanModeScanDepth[_Depth.Count];

                                                                int ScanDepthCount = 0;

                                                                #region "Depth"
                                                                for (int k = 0; k < _Depth.Count; k++)
                                                                {
                                                                    aScanMode.Depth = "";
                                                                    ScannersInstalledScannersScannerScanModeScanDepth aScanDepth = new ScannersInstalledScannersScannerScanModeScanDepth();
                                                                    aScanDepth.ScanDepthId = Convert.ToString(ScanDepthCount);
                                                                    aScanDepth.Name = Convert.ToString(_Depth[k]);

                                                                    aScanMode.ScanDepth[ScanDepthCount] = aScanDepth;
                                                                    ScanDepthCount++;
                                                                }

                                                                #endregion
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                            }
                                                            aScanner.ScanMode[ScanModeCount] = aScanMode;
                                                            ScanModeCount++;

                                                        }

                                                        #endregion
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                    }


                                                    #region "Scan Resolution"
                                                    try
                                                    {
                                                        aScanner.Resolution = null;
                                                        var _Resolution = objTwain32.Capabilities.XResolution.Get();
                                                        aScanner.Resolution = new ScannersInstalledScannersScannerResolution[_Resolution.Count];
                                                        aScanner.ScanResolution = "";
                                                        int ScanResolutionCount = 0;

                                                        for (int j = 0; j < _Resolution.Count; j++)
                                                        {

                                                            ScannersInstalledScannersScannerResolution aScanResolution = new ScannersInstalledScannersScannerResolution();
                                                            aScanResolution.ResolutionID = Convert.ToString(ScanResolutionCount);
                                                            aScanResolution.Name = Convert.ToString(_Resolution[j]);

                                                            aScanner.Resolution[ScanResolutionCount] = aScanResolution;
                                                            ScanResolutionCount++;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                    }


                                                    #endregion

                                                    #region "Scan Brightness"
                                                    try
                                                    {
                                                        aScanner.Brightness = null;
                                                        var _Brightness = objTwain32.Capabilities.Brightness.Get();
                                                        aScanner.Brightness = new ScannersInstalledScannersScannerBrightness[_Brightness.Count];
                                                        aScanner.ScanBrightness = "";

                                                        int ScanBrightnessCount = 0;
                                                        //Int32 BrightnessScale = 0;
                                                        //Int32 BrightnessDiv = 1;
                                                        Int32 BightnessName = Int32.MinValue;
                                                        Int32 PrevBightnessName = Int32.MinValue;

                                                        //RemoteScanSettings.getScallingForBrighnessAndCotrast(Convert.ToString(_Brightness[0]), out BrightnessScale, out BrightnessDiv);
                                                        Int32 BrightnessMin = 0;
                                                        Int32 BrightnessMax = 0;

                                                        if ((_Brightness.Count) > 0)
                                                        {
                                                            BrightnessMin = Convert.ToInt32(_Brightness[0]);
                                                            BrightnessMax = Convert.ToInt32(_Brightness[(_Brightness.Count) - 1]);

                                                            for (int j = 0; j < _Brightness.Count; j++)
                                                            {
                                                                //Int32.TryParse(Convert.ToString(_Brightness[j]), out BightnessName);
                                                                //BightnessName = (BightnessName / BrightnessDiv) + BrightnessScale;
                                                                Int32 nCurrBrightness = Convert.ToInt32(_Brightness[j]);
                                                                BightnessName = RemoteScanSettings.GetCalculatedCapValue(BrightnessMin, BrightnessMax, nCurrBrightness);

                                                                if ((BightnessName != PrevBightnessName) || j == 0)
                                                                {
                                                                    PrevBightnessName = BightnessName;
                                                                    ScannersInstalledScannersScannerBrightness aScanBrightness = new ScannersInstalledScannersScannerBrightness();
                                                                    aScanBrightness.BrightnessID = Convert.ToString(ScanBrightnessCount);
                                                                    //aScanBrightness.Name = Convert.ToString(_Brightness[j]);
                                                                    aScanBrightness.Name = Convert.ToString(BightnessName);

                                                                    aScanner.Brightness[ScanBrightnessCount] = aScanBrightness;
                                                                    ScanBrightnessCount++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                    }



                                                    #endregion

                                                    #region "Scan Contrast"
                                                    try
                                                    {
                                                        aScanner.Contrast = null;
                                                        var _Contrast = objTwain32.Capabilities.Contrast.Get();
                                                        aScanner.Contrast = new ScannersInstalledScannersScannerContrast[_Contrast.Count];
                                                        aScanner.ScanContrast = "";

                                                        int ScanContrastCount = 0;
                                                        //Int32 CotrastScale = 0;
                                                        //Int32 CotrastDiv = 1;
                                                        Int32 ContrastName = Int32.MinValue;
                                                        Int32 PrevContrastName = Int32.MinValue;

                                                        //RemoteScanSettings.getScallingForBrighnessAndCotrast(Convert.ToString(_Contrast[0]), out CotrastScale, out CotrastDiv);
                                                        Int32 ContrastMin = 0;
                                                        Int32 ContrastMax = 0;
                                                        if ((_Contrast.Count) > 0)
                                                        {
                                                            ContrastMin = Convert.ToInt32(_Contrast[0]);
                                                            ContrastMax = Convert.ToInt32(_Contrast[(_Contrast.Count) - 1]);

                                                            for (int j = 0; j < _Contrast.Count; j++)
                                                            {
                                                                //Int32.TryParse(Convert.ToString(_Contrast[j]), out CotrastName);
                                                                //CotrastName = (CotrastName / CotrastDiv) + CotrastScale;
                                                                Int32 nCurrContrast = Convert.ToInt32(_Contrast[j]);
                                                                ContrastName = RemoteScanSettings.GetCalculatedCapValue(ContrastMin, ContrastMax, nCurrContrast);

                                                                if ((ContrastName != PrevContrastName) || j == 0)
                                                                {
                                                                    PrevContrastName = ContrastName;
                                                                    ScannersInstalledScannersScannerContrast aScanContrast = new ScannersInstalledScannersScannerContrast();
                                                                    aScanContrast.ContrastID = Convert.ToString(ScanContrastCount);
                                                                    //aScanContrast.Name = Convert.ToString(_Contrast[j]);
                                                                    aScanContrast.Name = Convert.ToString(ContrastName);

                                                                    aScanner.Contrast[ScanContrastCount] = aScanContrast;
                                                                    ScanContrastCount++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                    }



                                                    #endregion

                                                    #region "Scan Side"
                                                    try
                                                    {
                                                        aScanner.ScanSide = null;
                                                        //var _ScanSide =objTwain32.Capabilities.Duplex.Get();
                                                        //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "Before IsCapSupported(TwCap.DuplexEnabled).ToString(); For aScanner.Name : " + aScanner.Name, gloAuditTrail.ActivityOutCome.Success);
                                                        string sDuplexCap = objTwain32.IsCapSupported(TwCap.DuplexEnabled).ToString();
                                                        //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "After IsCapSupported(TwCap.DuplexEnabled).ToString();", gloAuditTrail.ActivityOutCome.Success);
                                                        if (sDuplexCap.ToLower().Contains("set"))
                                                        {
                                                            aScanner.ScanSide = new ScannersInstalledScannersScannerScanSide[2];
                                                            ScannersInstalledScannersScannerScanSide aScanSide1 = new ScannersInstalledScannersScannerScanSide();
                                                            aScanSide1.ScanSideID = "0";
                                                            aScanSide1.Name = "Front Side";
                                                            ScannersInstalledScannersScannerScanSide aScanSide2 = new ScannersInstalledScannersScannerScanSide();
                                                            aScanSide2.ScanSideID = "1";
                                                            aScanSide2.Name = "Duplex";

                                                            aScanner.ScanSide[0] = aScanSide1;
                                                            aScanner.ScanSide[1] = aScanSide2;
                                                        }
                                                        else
                                                        {
                                                            if (!bIsWIAScanner)
                                                            {
                                                                aScanner.ScanSide = new ScannersInstalledScannersScannerScanSide[1];
                                                                ScannersInstalledScannersScannerScanSide aScanSide1 = new ScannersInstalledScannersScannerScanSide();
                                                                aScanSide1.ScanSideID = "0";
                                                                aScanSide1.Name = "Front Side";
                                                                aScanner.ScanSide[0] = aScanSide1;
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                    }


                                                    #endregion

                                                    #region "Scan Supported Sizes"
                                                    try
                                                    {
                                                        try
                                                        {
                                                            //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "Before objTwain32.IsCapSupported(TwCap.IUnits).ToString(); For aScanner.Name : " + aScanner.Name, gloAuditTrail.ActivityOutCome.Success);
                                                            string sUnitCap = objTwain32.IsCapSupported(TwCap.IUnits).ToString();
                                                            //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "After objTwain32.IsCapSupported(TwCap.IUnits).ToString();", gloAuditTrail.ActivityOutCome.Success);
                                                            if (sUnitCap.ToLower().Contains("current") && sUnitCap.ToLower().Contains("set")) 
                                                            { 
                                                               // gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "Before objTwain32.Capabilities.Units.GetCurrent();", gloAuditTrail.ActivityOutCome.Success);
                                                                TwUnits _units = objTwain32.Capabilities.Units.GetCurrent();
                                                                if (Convert.ToString(_units) != Convert.ToString(TwUnits.Inches))
                                                                {
                                                                    //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "Before objTwain32.SetCap(TwCap.IUnits, TwUnits.Inches);", gloAuditTrail.ActivityOutCome.Success);
                                                                    objTwain32.SetCap(TwCap.IUnits, TwUnits.Inches);
                                                                }
                                                                // gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "After objTwain32.Capabilities.Units.GetCurrent();", gloAuditTrail.ActivityOutCome.Success);
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                        }

                                                        aScanner.SupportedSize = null;
                                                        //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "Before IsCapSupported(TwCap.SupportedSizes).ToString();", gloAuditTrail.ActivityOutCome.Success);
                                                        string sSizeSupported = objTwain32.IsCapSupported(TwCap.SupportedSizes).ToString();
                                                        //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, "After IsCapSupported(TwCap.SupportedSizes).ToString();", gloAuditTrail.ActivityOutCome.Success);
                                                        if (sSizeSupported.ToLower().Contains("get"))
                                                        {

                                                            var _SupportedSizes = objTwain32.Capabilities.SupportedSizes.Get();
                                                            aScanner.SupportedSize = new ScannersInstalledScannersScannerSupportedSize[_SupportedSizes.Count];
                                                            aScanner.ScanSupportedSize = "";

                                                            int ScanSupportedSizesCount = 0;

                                                            for (int j = 0; j < _SupportedSizes.Count; j++)
                                                            {

                                                                ScannersInstalledScannersScannerSupportedSize aScanSupportedSize = new ScannersInstalledScannersScannerSupportedSize();
                                                                aScanSupportedSize.SupportedSizeID = Convert.ToString(ScanSupportedSizesCount);
                                                                aScanSupportedSize.Name = RemoteScanSettings.supportedSizes[RemoteScanSettings.getValueFromSupportedSizeFromScanner(Convert.ToString(_SupportedSizes[j]))]; //Convert.ToString(_SupportedSizes[j]);
                                                                try
                                                                {
                                                                    objTwain32.SetCap(TwCap.SupportedSizes, _SupportedSizes[j]);
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                                }
                                                                //Card Size

                                                                aScanSupportedSize.Left = Convert.ToString(objTwain32.ImageLayout.Left);
                                                                aScanSupportedSize.Top = Convert.ToString(objTwain32.ImageLayout.Top);
                                                                aScanSupportedSize.Width = Convert.ToString(objTwain32.ImageLayout.Width);
                                                                aScanSupportedSize.Length = Convert.ToString(objTwain32.ImageLayout.Height);

                                                                //Card Size - End
                                                                aScanner.SupportedSize[ScanSupportedSizesCount] = aScanSupportedSize;
                                                                ScanSupportedSizesCount++;
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                                    }

                                                    #endregion


                                                }//if (objTwain32.OpenDataSource())
                                                else
                                                {
                                                    notATwainScanner = true;
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            notATwainScanner = true;
                                            //ScannerDevicesCount = 0;
                                            //bRes = false;
                                            //bFailed = true;
                                        }
                                        finally
                                        {
                                            if (bOpentDataSource)
                                            {
                                                try
                                                {
                                                    //logdata.Log("Before objTwain32.CloseDataSource() for i :" + i.ToString());
                                                    objTwain32.CloseDataSource();
                                                    bOpentDataSource = false;
                                                    //logdata.Log("After objTwain32.CloseDataSource() for i :" + i.ToString());
                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }

                                        //     objTwain32.CloseDataSource();
                                        //     objTwain32.CloseDSM();
                                        if (!notATwainScanner && !bIsWIAScanner)
                                        {
                                            aScannerList.Scanner[ScannerCount] = aScanner;
                                            ScannerCount++;
                                        }

                                    } //FOR



                                }//if (ScannerDevicesCount > 0)

                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                bRes = false;
                                //bFailed = true;
                            }
                        }
                        else
                        {
                            bRes = false;
                            //bFailed = true; 
                        }
                    }//if (objTwain32 != null)

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    bRes = false;
                    //bFailed = true;
                }
                finally
                {
                    if (objTwain32 != null)
                    {
                        try
                        {
                            if (bOpentDataSource)
                            {
                                objTwain32.CloseDataSource();
                                bOpentDataSource = false;
                            }
                        }
                        catch //(Exception ex)
                        {
                            //logdata.Log(ex.ToString());
                        }



                        if (objTwain32 != null)
                        {
                            objTwain32.Dispose();
                            objTwain32 = null;
                        }
                    }

                }

                //if (bIsWIAScanner)
                //{
                if (ScannerDevicesCount > 0)
                {
                    if (aScannerList.Scanner == null)
                    {
                        aScannerList.Scanner = new ScannersInstalledScannersScanner[ScannerDevicesCount];
                    }
                    for (int i = 0; i < arWIA.Count; i++)
                    {
                        string myDeviceNmae = arWIA[i];
                        if (string.IsNullOrEmpty(myDeviceNmae)) { continue; }
                        aScanner = new ScannersInstalledScannersScanner();

                        aScanner.ScannerID = Convert.ToString(ScannerCount);
                        aScanner.Name = myDeviceNmae;

                        Item wia_item = null;
                        ScannerDevice wiascannerdevice = null;
                        //ScanMode

                        //if (bIsWIAScanner) //SLR: For all scanners
                        wia_item = null;
                        try
                        {
                            wiascannerdevice = WiaDevice.GetFirstScannerDevice(myDeviceNmae).AsScannerDevice();
                            if (wiascannerdevice.Device != null)
                            {
                                wia_item = wiascannerdevice.Device.Items[1];
                            }
                            else
                            {
                                wia_item = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        try
                        {
                            gloRemoteScanGeneral.RemoteScanSettings.GetCapMaxMinForWIA(wia_item);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }

                        if (aScannerList.Default == aScanner.Name)
                        { aScanner.IsDefault = "True"; }
                        else
                        { aScanner.IsDefault = "False"; }

                        aScanner.ScanMode = null;
                        aScanner.Resolution = null;
                        aScanner.Brightness = null;
                        aScanner.Contrast = null;
                        aScanner.ScanSide = null;
                        aScanner.SupportedSize = null;

                        if (aScanner.ScanMode == null)
                        {
                            //BW
                            aScanner.ScanMode = new ScannersInstalledScannersScannerScanMode[3];

                            ScannersInstalledScannersScannerScanMode aScanModeBW = new ScannersInstalledScannersScannerScanMode();
                            aScanModeBW.ScanModeID = "0";
                            aScanModeBW.Name = "BW";

                            aScanModeBW.ScanDepth = null;
                            aScanModeBW.ScanDepth = new ScannersInstalledScannersScannerScanModeScanDepth[1];

                            ScannersInstalledScannersScannerScanModeScanDepth aScanDepthBW = new ScannersInstalledScannersScannerScanModeScanDepth();
                            aScanDepthBW.ScanDepthId = "0";
                            aScanDepthBW.Name = "1";

                            aScanModeBW.ScanDepth[0] = aScanDepthBW;
                            aScanner.ScanMode[0] = aScanModeBW;

                            //Gray

                            ScannersInstalledScannersScannerScanMode aScanModeGray = new ScannersInstalledScannersScannerScanMode();
                            aScanModeGray.ScanModeID = "1";
                            aScanModeGray.Name = "Gray";

                            aScanModeGray.ScanDepth = null;
                            aScanModeGray.ScanDepth = new ScannersInstalledScannersScannerScanModeScanDepth[3];

                            ScannersInstalledScannersScannerScanModeScanDepth aScanDepthGray1 = new ScannersInstalledScannersScannerScanModeScanDepth();
                            aScanDepthGray1.ScanDepthId = "0";
                            aScanDepthGray1.Name = "2";
                            aScanModeGray.ScanDepth[0] = aScanDepthGray1;

                            ScannersInstalledScannersScannerScanModeScanDepth aScanDepthGray2 = new ScannersInstalledScannersScannerScanModeScanDepth();
                            aScanDepthGray2.ScanDepthId = "1";
                            aScanDepthGray2.Name = "4";
                            aScanModeGray.ScanDepth[1] = aScanDepthGray2;

                            ScannersInstalledScannersScannerScanModeScanDepth aScanDepthGray3 = new ScannersInstalledScannersScannerScanModeScanDepth();
                            aScanDepthGray3.ScanDepthId = "2";
                            aScanDepthGray3.Name = "8";
                            aScanModeGray.ScanDepth[2] = aScanDepthGray3;


                            aScanner.ScanMode[1] = aScanModeGray;

                            //RGB

                            ScannersInstalledScannersScannerScanMode aScanModeRGB = new ScannersInstalledScannersScannerScanMode();
                            aScanModeRGB.ScanModeID = "2";
                            aScanModeRGB.Name = "RGB";

                            aScanModeRGB.ScanDepth = null;
                            aScanModeRGB.ScanDepth = new ScannersInstalledScannersScannerScanModeScanDepth[2];

                            ScannersInstalledScannersScannerScanModeScanDepth aScanDepthRGB1 = new ScannersInstalledScannersScannerScanModeScanDepth();
                            aScanDepthRGB1.ScanDepthId = "0";
                            aScanDepthRGB1.Name = "8";
                            aScanModeRGB.ScanDepth[0] = aScanDepthRGB1;

                            ScannersInstalledScannersScannerScanModeScanDepth aScanDepthRGB2 = new ScannersInstalledScannersScannerScanModeScanDepth();
                            aScanDepthRGB2.ScanDepthId = "1";
                            aScanDepthRGB2.Name = "24";
                            aScanModeRGB.ScanDepth[1] = aScanDepthRGB2;

                            aScanner.ScanMode[2] = aScanModeRGB;


                        }
                        //}

                        //if (bIsWIAScanner)
                        //{
                        if (aScanner.Resolution == null)
                        {
                            if (!gloRemoteScanGeneral.RemoteScanSettings.bResolution)
                            {
                                gloRemoteScanGeneral.RemoteScanSettings.nResolutionMax = 600;
                                gloRemoteScanGeneral.RemoteScanSettings.nResolutionMin = 75;
                                gloRemoteScanGeneral.RemoteScanSettings.nResolutionStep = 1;
                            }

                            int nArySize = (int)Math.Ceiling((decimal)((gloRemoteScanGeneral.RemoteScanSettings.nResolutionMax - gloRemoteScanGeneral.RemoteScanSettings.nResolutionMin) + 1) / gloRemoteScanGeneral.RemoteScanSettings.nResolutionStep);
                            aScanner.Resolution = new ScannersInstalledScannersScannerResolution[nArySize];
                            int ScanResolutionCount = 0;

                            for (int icnt = gloRemoteScanGeneral.RemoteScanSettings.nResolutionMin; icnt <= gloRemoteScanGeneral.RemoteScanSettings.nResolutionMax; icnt += gloRemoteScanGeneral.RemoteScanSettings.nResolutionStep)
                            {
                                ScannersInstalledScannersScannerResolution aScanResolution = new ScannersInstalledScannersScannerResolution();
                                aScanResolution.ResolutionID = Convert.ToString(ScanResolutionCount);
                                aScanResolution.Name = Convert.ToString(icnt);

                                aScanner.Resolution[ScanResolutionCount] = aScanResolution;
                                ScanResolutionCount++;
                            }
                        }
                        //}

                        //if (bIsWIAScanner)
                        //{
                        if (aScanner.Brightness == null)
                        {
                            if (!gloRemoteScanGeneral.RemoteScanSettings.bBrightness)
                            {
                                gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMax = 127;
                                gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMin = -128;
                                gloRemoteScanGeneral.RemoteScanSettings.nBrightnessStep = 1;
                            }

                            int nArySize = (int)Math.Ceiling((decimal)((gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMax - gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMin) + 1) / gloRemoteScanGeneral.RemoteScanSettings.nBrightnessStep);
                            aScanner.Brightness = new ScannersInstalledScannersScannerBrightness[nArySize];
                            int ScanBrightnessCount = 0;

                            Int32 BightnessName = Int32.MinValue;
                            Int32 PrevBightnessName = Int32.MinValue;
                            // RemoteScanSettings.getScallingForBrighnessAndCotrast(Convert.ToString(_Brightness[0]), out BrightnessScale, out BrightnessDiv);

                            for (int icnt = gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMin; icnt <= gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMax; icnt += gloRemoteScanGeneral.RemoteScanSettings.nBrightnessStep)
                            {
                                Int32 nCurrBrightness = icnt;
                                BightnessName = RemoteScanSettings.GetCalculatedCapValue(gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMin, gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMax, nCurrBrightness);
                                // BightnessName = (BightnessName / BrightnessDiv) + BrightnessScale;
                                if ((BightnessName != PrevBightnessName) || (icnt == gloRemoteScanGeneral.RemoteScanSettings.nBrightnessMin))
                                {
                                    PrevBightnessName = BightnessName;
                                    ScannersInstalledScannersScannerBrightness aScanBrightness = new ScannersInstalledScannersScannerBrightness();
                                    aScanBrightness.BrightnessID = Convert.ToString(ScanBrightnessCount);
                                    aScanBrightness.Name = Convert.ToString(BightnessName);//icnt

                                    aScanner.Brightness[ScanBrightnessCount] = aScanBrightness;
                                    ScanBrightnessCount++;
                                }
                            }
                        }
                        //}

                        //if (bIsWIAScanner)
                        //{
                        if (aScanner.Contrast == null)
                        {
                            if (!gloRemoteScanGeneral.RemoteScanSettings.bContrast)
                            {
                                gloRemoteScanGeneral.RemoteScanSettings.nContrastMax = 127;
                                gloRemoteScanGeneral.RemoteScanSettings.nContrastMin = -128;
                                gloRemoteScanGeneral.RemoteScanSettings.nContrastStep = 1;
                            }

                            int nArySize = (int)Math.Ceiling((decimal)((gloRemoteScanGeneral.RemoteScanSettings.nContrastMax - gloRemoteScanGeneral.RemoteScanSettings.nContrastMin) + 1) / gloRemoteScanGeneral.RemoteScanSettings.nContrastStep);
                            aScanner.Contrast = new ScannersInstalledScannersScannerContrast[nArySize];
                            int ScanContrastCount = 0;

                            Int32 ContrastName = Int32.MinValue;
                            Int32 PrevContrastName = Int32.MinValue;
                            Int32 CurrContrastName = Int32.MinValue;

                            for (int icnt = gloRemoteScanGeneral.RemoteScanSettings.nContrastMin; icnt <= gloRemoteScanGeneral.RemoteScanSettings.nContrastMax; icnt += gloRemoteScanGeneral.RemoteScanSettings.nContrastStep)
                            {
                                CurrContrastName = icnt;
                                ContrastName = RemoteScanSettings.GetCalculatedCapValue(gloRemoteScanGeneral.RemoteScanSettings.nContrastMin, gloRemoteScanGeneral.RemoteScanSettings.nContrastMax, CurrContrastName);
                                if ((ContrastName != PrevContrastName) || (icnt == gloRemoteScanGeneral.RemoteScanSettings.nContrastMin))
                                {
                                    PrevContrastName = ContrastName;
                                    ScannersInstalledScannersScannerContrast aScanContrast = new ScannersInstalledScannersScannerContrast();
                                    aScanContrast.ContrastID = Convert.ToString(ScanContrastCount);
                                    aScanContrast.Name = Convert.ToString(ContrastName);//icnt

                                    aScanner.Contrast[ScanContrastCount] = aScanContrast;
                                    ScanContrastCount++;
                                }
                            }
                        }
                        //}

                        //WIA Scan Side
                        //if (bIsWIAScanner)
                        //{
                        // Device dv = gloScanWIA.WiaDevice.GetFirstScannerDevice();
                        if (aScanner.ScanSide == null)
                        {


                            Item item = wiascannerdevice.Device.Items[1];


                            bool canDuplex = wiascannerdevice.DeviceSettings.DocumentHandlingCapabilities.HasFlag(DocumentHandlingCapabilities.Dup);
                            bool canFlatBed = wiascannerdevice.DeviceSettings.DocumentHandlingCapabilities.HasFlag(DocumentHandlingCapabilities.Flat);

                            if (canDuplex && canFlatBed)
                            {
                                aScanner.ScanSide = new ScannersInstalledScannersScannerScanSide[3];

                                ScannersInstalledScannersScannerScanSide aScanSide1 = new ScannersInstalledScannersScannerScanSide();
                                aScanSide1.ScanSideID = "0";
                                aScanSide1.Name = "Front Side";
                                ScannersInstalledScannersScannerScanSide aScanSide2 = new ScannersInstalledScannersScannerScanSide();
                                aScanSide2.ScanSideID = "1";
                                aScanSide2.Name = "Duplex";
                                ScannersInstalledScannersScannerScanSide aScanSide3 = new ScannersInstalledScannersScannerScanSide();
                                aScanSide3.ScanSideID = "2";
                                aScanSide3.Name = "Flat Bed";

                                aScanner.ScanSide[0] = aScanSide1;
                                aScanner.ScanSide[1] = aScanSide2;
                                aScanner.ScanSide[2] = aScanSide3;

                            }
                            else if (canDuplex)
                            {
                                aScanner.ScanSide = new ScannersInstalledScannersScannerScanSide[2];
                                ScannersInstalledScannersScannerScanSide aScanSide1 = new ScannersInstalledScannersScannerScanSide();
                                aScanSide1.ScanSideID = "0";
                                aScanSide1.Name = "Front Side";
                                ScannersInstalledScannersScannerScanSide aScanSide2 = new ScannersInstalledScannersScannerScanSide();
                                aScanSide2.ScanSideID = "1";
                                aScanSide2.Name = "Duplex";

                                aScanner.ScanSide[0] = aScanSide1;
                                aScanner.ScanSide[1] = aScanSide2;
                            }
                            else if (canFlatBed)
                            {
                                aScanner.ScanSide = new ScannersInstalledScannersScannerScanSide[2];
                                ScannersInstalledScannersScannerScanSide aScanSide1 = new ScannersInstalledScannersScannerScanSide();
                                aScanSide1.ScanSideID = "0";
                                aScanSide1.Name = "Front Side";
                                ScannersInstalledScannersScannerScanSide aScanSide2 = new ScannersInstalledScannersScannerScanSide();
                                aScanSide2.ScanSideID = "1";
                                aScanSide2.Name = "Flat Bed";

                                aScanner.ScanSide[0] = aScanSide1;
                                aScanner.ScanSide[1] = aScanSide2;
                            }
                            else
                            {
                                aScanner.ScanSide = new ScannersInstalledScannersScannerScanSide[1];
                                ScannersInstalledScannersScannerScanSide aScanSide1 = new ScannersInstalledScannersScannerScanSide();
                                aScanSide1.ScanSideID = "0";
                                aScanSide1.Name = "Front Side";
                                aScanner.ScanSide[0] = aScanSide1;
                            }

                        }
                        //}

                        //WIA Supported Size
                        //if (bIsWIAScanner)
                        //{
                        if (aScanner.SupportedSize == null)
                        {
                            aScanner.SupportedSize = new ScannersInstalledScannersScannerSupportedSize[57];
                            aScanner.ScanSupportedSize = "";

                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Legal", 0, 0, 8.5, 14, 0);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Folio", 0, 0, 8.5, 13, 1);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Letter Plus", 0, 0, 8.5, 12.7, 2);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "A4 Plus", 0, 0, 8.3, 13, 3);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Gemnan Std Fanfold", 0, 0, 8.5, 12, 4);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "A4", 0, 0, 8.3, 11.7, 5);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Letter", 0, 0, 8.5, 11, 6);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Quarto", 0, 0, 8.5, 10.8, 7);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "B5 (ISO) Extra", 0, 0, 7.9, 10.9, 8);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Executive", 0, 0, 7.3, 10.5, 9);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC 16K", 0, 0, 7.4, 10.2, 10);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "B5 (JIS)", 0, 0, 7.2, 10.1, 11);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope B5", 0, 0, 6.9, 9.8, 12);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "A5 Extra", 0, 0, 6.9, 9.3, 13);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope C5", 0, 0, 6.4, 9, 14);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope #14", 0, 0, 5, 11.5, 15);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #8", 0, 0, 4.7, 12.2, 16);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #7", 0, 0, 6.3, 9.1, 17);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope #12", 0, 0, 4.8, 11, 18);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "A5 Rotated", 0, 0, 8.3, 5.8, 19);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "A5", 0, 0, 5.8, 8.3, 20);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Statement", 0, 0, 5.5, 8.5, 21);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope #11", 0, 0, 4.5, 10.4, 22);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Japanese Double Postcard", 0, 0, 7.9, 5.8, 23);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Double Japan Postcard Rotated", 0, 0, 5.8, 7.9, 24);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC 32K(Big) Rotated", 0, 0, 8, 5.5, 25);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC 32K(Big)", 0, 0, 5.5, 8, 26);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Japanese Envelope Chou #3", 0, 0, 4.7, 9.3, 27);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #6", 0, 0, 4.7, 9.1, 28);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope C65", 0, 0, 4.5, 9, 29);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope", 0, 0, 4.3, 9.1, 30);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope #10", 0, 0, 4.1, 9.5, 31);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Japanese Envelope You #4", 0, 0, 4.1, 9.3, 32);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope DL", 0, 0, 4.3, 8.7, 33);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC 32K Rotated", 0, 0, 7.2, 5.1, 34);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC 32K", 0, 0, 5.1, 7.2, 35);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "B6", 0, 0, 5, 7.2, 36);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "B6 (JIS) Rotated", 0, 0, 7.2, 5, 37);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "B6 (JIS)", 0, 0, 5, 7.2, 38);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #4 Rotated", 0, 0, 8.2, 4.3, 39);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #4", 0, 0, 4.3, 8.2, 40);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope #9", 0, 0, 3.9, 8.9, 41);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope B6", 0, 0, 6.9, 4.9, 42);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #3", 0, 0, 4.9, 6.9, 43);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope Monarch", 0, 0, 3.9, 7.5, 44);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Envelope C6", 0, 0, 4.5, 6.4, 45);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Japan Envelope Chou #4 Rotated", 0, 0, 8.1, 3.5, 46);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Japanese Envelope Chou #4 ", 0, 0, 3.5, 8.1, 47);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #2 Rotated", 0, 0, 6.9, 4, 48);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #2", 0, 0, 4, 6.9, 49);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #1 Rotated", 0, 0, 6.5, 4, 50);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "PRC Envelope #1", 0, 0, 4, 6.5, 51);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "A6", 0, 0, 4.1, 5.8, 52);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "A6 Rotated", 0, 0, 5.8, 4.1, 53);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "6 3/4 Envelope", 0, 0, 3.6, 6.5, 54);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Japenese Postcard Rotated", 0, 0, 5.8, 3.9, 55);
                            gloRemoteScanGeneral.RemoteScanSettings.SetWIASupportedSize(ref aScanner, "Japenese Postcard", 0, 0, 3.9, 5.8, 56);

                        }

                        //}

                        aScannerList.Scanner[ScannerCount] = aScanner;
                        ScannerCount++;

                    }

                    try
                    {
                        UpdateOrCreateTwainScanConfigFiles(aScannerList);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        bRes = false;
                        //bFailed = true;
                    }

                }

                return bRes;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                bRes = false;
            }
            finally
            {
                //if (key != null)
                //{
                //    key.Close(); key.Dispose(); key = null;
                //}
            }
            return bRes;

        }

        public static void UpdateOrCreateTwainScanConfigFiles(ScannersInstalledScanners aScannerList)
        {
            try
            {
                //gloGlobal.clsDatalog logdata = new gloGlobal.clsDatalog();
                //XML Setings File
                string TwainScanConfigFolderPath = System.IO.Path.Combine(gloGlobal.clsDatalog.ApplicationPathForTwainScan); // your code goes here

                //if (Directory.Exists(ScanConfigFolderPath) == false)
                //{
                //    Directory.CreateDirectory(ScanConfigFolderPath);
                //}

                gloGlobal.gloRemoteScanSettings.CreateDir(TwainScanConfigFolderPath);
                //     bool exists = gloAuditTrail.gloAuditTrail.CreateDirectoryIfNotExists(ScanConfigFolderPath);

                string sScanSettingsFile = System.Guid.NewGuid().ToString() + ".xml";
                string ScanConfigFilePath = System.IO.Path.Combine(TwainScanConfigFolderPath, sScanSettingsFile);


                gloRemoteScanMetaDataWriter.CreateXMLFile(aScannerList, ScanConfigFilePath);

                //ScanMasterConfig File

                //ScanMasterConfig objMasterList = new ScanMasterConfig();
                //objMasterList.Items = new ScanMasterConfigMasterConfig[1];

                ScanMasterConfigMasterConfig aScanMasterList = new ScanMasterConfigMasterConfig();
                aScanMasterList.ScanSettingsFile = sScanSettingsFile;

                String ScanMasterConfigFilePath = Path.Combine(gloGlobal.clsDatalog.ApplicationPathForTwainScan, gloGlobal.gloRemoteScanSettings.ScanMasterConfig);
                String tempFile = Path.Combine(gloGlobal.clsDatalog.ApplicationPathForTwainScan, "Temp_" + System.Guid.NewGuid().ToString() + ".xml");
                //gloQueueSchema.gloSerialization.SetMasterConfigDocument(tempFile, mainConfig);
                gloRemoteScanMetaDataWriter.CreateXMLFile(aScanMasterList, tempFile);

                gloRemoteScanMetaDataWriter.CopyAndDeleteFile(tempFile, ScanMasterConfigFilePath);

                //if (File.Exists(tempFile))
                //{
                //    File.Copy(tempFile, ScanMasterConfigFilePath, true);
                //    File.Delete(tempFile);
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {

            }

        }
    }
}
