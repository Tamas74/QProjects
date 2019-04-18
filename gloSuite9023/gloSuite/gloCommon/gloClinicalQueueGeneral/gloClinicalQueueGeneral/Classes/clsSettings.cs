using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Printing;
using System.IO;
using Microsoft.Win32;
using gloGlobal;

namespace gloClinicalQueueGeneral.Classes
{
    public class clsSettings
    {
        private static string m_sRegistryPath = @"Software\\gloData";
        private static CommonApplicationData cad = new CommonApplicationData("TriarqHealth", "gloTSPrint", true);
        private static clsConfiguration Config = new clsConfiguration();
        public static bool isFromServiceTray = true;

        public static clsConfiguration GetConfigurationFile(clsConfiguration cConfig = null, bool reLoad = false)
        {
            if (cConfig != null)
            {
                Config = cConfig;
            }
            if (Config.Config == null)
            {
                reLoad = true;
            }
            if (reLoad)
            {
                //Read through settings file
                if (clsSettingsConfiguration.isSettingsFileExists())
                {
                    Int64 ConfigInterval = 2000;
                    if (Int64.TryParse(clsSettingsConfiguration.ReadAppData("ConfigInterval", "2000"), out ConfigInterval))
                    {
                        Config.ConfigInterval = ConfigInterval;
                    }
                    else
                    {
                        Config.ConfigInterval = 2000; 
                    }
                    Boolean ServiceBy = false;
                    if (Boolean.TryParse(clsSettingsConfiguration.ReadAppData("ServiceBy", "false"), out ServiceBy))
                    {
                        Config.ServiceBy = ServiceBy;
                    }
                    else
                    {
                        Config.ServiceBy = false;
                    }
                    Config.ServiceType = clsSettingsConfiguration.ReadAppData("ServiceType", "defaultprinter").ToLower();
                    if (string.IsNullOrEmpty(Config.ServiceType))
                    {
                        Config.ServiceType = "defaultprinter";
                    }
                    Config.ConfigPath = clsSettingsConfiguration.ReadAppData("ConfigFilePath", cad.ConfigFolderPath);
                    if (string.IsNullOrEmpty(Config.ConfigPath))
                    {
                        Config.ConfigPath = cad.ConfigFolderPath;
                    }
                    Boolean bAddLogForDebug = false;
                    if (Boolean.TryParse(clsSettingsConfiguration.ReadAppData("AddLogForDebug", "false"), out bAddLogForDebug))
                    {
                        Config.bAddLogForDebug = bAddLogForDebug;
                    }
                    else
                    {
                        Config.bAddLogForDebug = false;
                    }
                }
                else //Read through registry values 
                {
                    RegistryKey key = null;
                    try
                    {
                        try
                        {
                            key = Registry.LocalMachine.OpenSubKey(m_sRegistryPath, RegistryKeyPermissionCheck.ReadSubTree);
                        }
                        catch 
                        {}
                        if (key != null)
                        {
                            string interval = Convert.ToString(key.GetValue("ConfigInterval"));
                            try
                            {
                                Config.ConfigInterval = Convert.ToInt64(interval);
                            }
                            catch
                            {
                                Config.ConfigInterval = 2000;
                            }
                            interval = Convert.ToString(key.GetValue("ServiceBy"));
                            Config.ServiceBy = (interval.ToLower() == "true");
                            Config.ServiceType = Convert.ToString(key.GetValue("ServiceType"));
                            if (string.IsNullOrEmpty(Config.ServiceType))
                            {
                                key.SetValue("ServiceType", "defaultprinter");
                                Config.ServiceType = Convert.ToString(key.GetValue("ServiceType"));
                            }
                            else
                            {
                                Config.ServiceType = Config.ServiceType.ToLower();
                            }
                            Config.ConfigPath = Convert.ToString(key.GetValue("ConfigFilePath"));
                        }
                        else
                        {
                            Config.ConfigInterval = 2000;
                            Config.ServiceBy = false;
                            Config.ServiceType = "defaultprinter";
                            Config.ConfigPath = cad.ConfigFolderPath;
                        }
                        //save to settings config
                        clsSettingsConfiguration.SaveSettingsConfig(Config);
                    }
                    catch (Exception ex)
                    {
                        if (!gloGlobal.clsMISC.IsService)
                        {
                            MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //adding exception to log files
                            //  clsgeneral.ExceptionLog("In function clsSettings-clsConfiguration GetConfigurationFile: ex - "+ex.ToString());
                            // clsgeneral.ExceptionLog("In function clsSettings-clsConfiguration GetConfigurationFile: InnerException - " + ex.InnerException.ToString());
                        }
                    }
                    finally
                    {
                        if (key != null)
                        {
                            key.Close();
                            key.Dispose();
                            key = null;
                        }
                    }
                }

               
                try
                {
                    return GetConfigFile(reLoad);

                }
                catch (Exception ex)
                {
                    if (!gloGlobal.clsMISC.IsService)
                    {
                        MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //adding exception to log files
                        //  clsgeneral.ExceptionLog("In function clsSettings-clsConfiguration GetConfigurationFile: ex - "+ex.ToString());
                        // clsgeneral.ExceptionLog("In function clsSettings-clsConfiguration GetConfigurationFile: InnerException - " + ex.InnerException.ToString());
                    }
                    return null;
                }
            }
            else return Config;
        }

        //private static void SetDefaultConfig(RegistryKey key)
        //{
        //    try
        //    {
        //        //cad = new CommonApplicationData("TriarqHealth", "gloTSPrint", true);
        //        String configPath = cad.ConfigFolderPath;
        //        key.SetValue("ConfigFilePath", configPath);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        public static clsConfiguration GetConfigFile(bool reLoad)
        {

            if (String.IsNullOrEmpty(Config.ConfigPath))
            {
                return Config;
            }
            else
            {
                ExeConfigurationFileMap objfile = new ExeConfigurationFileMap();
                objfile.ExeConfigFilename = Config.ConfigPath + "\\Service.config";
                Config.Config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(objfile, ConfigurationUserLevel.None);
                if (reLoad)
                {
                    string LowerServiceType = Config.ServiceType.ToLower();

                    Config.DeviceInterfaceSettings = GetDeviceInterfaceSettings();

                    Config.SignaturePadSettings = GetSignaturePadSettings();

                    Config.RemoteScanSettings = GetRemoteScanSettings();

                    if (LowerServiceType == "download")
                    {
                        Config.DownLoadSettings = GetDownloadSettings();
                    }
                    if (LowerServiceType == "upload")
                    {
                        Config.UpLoadSettings = GetUploadSettings();
                    }
                    if (LowerServiceType == "defaultprinter")
                    {
                        Config.TSPrinterSettings = GetTSPrinterSettings();
                    }


                }
                return Config;
            }
        }

        private static clsUploadSettings GetUploadSettings()
        {
            clsUploadSettings UploadSettings = new clsUploadSettings();
            if (isFromServiceTray)
            {
                UploadSettings.ftpfolderpath = ReadAppData("ftpfolderpath", "");
                UploadSettings.sourcepath = ReadAppData("sourcepath", "");
                UploadSettings.ftp = ReadAppData("ftp", "Interface.gloclouds.com");
                UploadSettings.user = ReadAppData("user", "");
                UploadSettings.pwd = ReadAppData("pwd", "");
                //string FtpDeleteFileLog = ReadAppData("FTPDeleteFileLog", "7");
                //int.TryParse(FtpDeleteFileLog, out UploadSettings.FTPDeleteFileLog);
                //if (UploadSettings.FTPDeleteFileLog <= 0)
                //{
                //    UploadSettings.FTPDeleteFileLog = 7;
                //}
                string DeleteFileLog = ReadAppData("DeleteFileLog", "7");
                int.TryParse(DeleteFileLog, out UploadSettings.DeleteFileLog);
                if (UploadSettings.DeleteFileLog <= 0)
                {
                    UploadSettings.DeleteFileLog = 7;
                }
                UploadSettings.timerintervalInMin = ReadAppData("timerintervalInMin", "15");
            }

            return UploadSettings;
        }
        private static void SetUploadSettings(clsUploadSettings UploadSettings)
        {
            if (isFromServiceTray)
            {
                WriteAppData("ftpfolderpath", UploadSettings.ftpfolderpath);
                WriteAppData("sourcepath", UploadSettings.sourcepath);
                WriteAppData("ftp", UploadSettings.ftp);
                WriteAppData("user", UploadSettings.user);
                WriteAppData("pwd", UploadSettings.pwd);
                WriteAppData("timerintervalInMin", UploadSettings.timerintervalInMin);
                //WriteAppData("FTPDeleteFileLog", UploadSettings.FTPDeleteFileLog.ToString());
                WriteAppData("DeleteFileLog", UploadSettings.DeleteFileLog.ToString());
            }

        }

        private static clsDeviceInterfaceSettings GetDeviceInterfaceSettings()
        {
            clsDeviceInterfaceSettings DeviceInterfaceSettings = new clsDeviceInterfaceSettings();
            DeviceInterfaceSettings.EnableLocalWelchAllynECGDevice = Convert.ToBoolean(ReadAppData("EnableLocalWelchAllynECGDevice", "False"));

            return DeviceInterfaceSettings;
        }

        private static clsSignaturePadSettings GetSignaturePadSettings()
        {
            clsSignaturePadSettings SignaturePadSettings = new clsSignaturePadSettings();
            SignaturePadSettings.EnableLocalSignaturePad = Convert.ToBoolean(ReadAppData("EnableLocalSignaturePad","True"));
            SignaturePadSettings.IsNewSignaturePad = Convert.ToBoolean(ReadAppData("IsNewSignaturePad", "False"));

            return SignaturePadSettings;
        }

        private static clsRemoteScanSettings GetRemoteScanSettings()
        {
            clsRemoteScanSettings RemoteScanSettings = new clsRemoteScanSettings();
            RemoteScanSettings.EnableRemoteScan = Convert.ToBoolean(ReadAppData("EnableRemoteScan", "False"));
            RemoteScanSettings.EnableWebP = Convert.ToBoolean(ReadAppData("EnableWebP", "False"));
            //RemoteScanSettings.EnableBWCompression = Convert.ToBoolean(ReadAppData("EnableCompression", "False"));
            //gloGlobal.gloRemoteScanSettings.EnableBWCompression = RemoteScanSettings.EnableBWCompression;
            RemoteScanSettings.QualityOfBW = Convert.ToInt32(ReadAppData("QualityOfBW", "70"));
            gloGlobal.gloRemoteScanSettings.QualityOfBW = RemoteScanSettings.QualityOfBW;
            RemoteScanSettings.QualityOfColor = Convert.ToInt32(ReadAppData("QualityOfColor", "100"));
            gloGlobal.gloRemoteScanSettings.QualityOfColor = RemoteScanSettings.QualityOfColor;
            RemoteScanSettings.TwainVersion = ReadAppData("TwainVersion", "Default");
            gloGlobal.gloRemoteScanSettings.twainVersion = RemoteScanSettings.TwainVersion;
            RemoteScanSettings.TwainDuplex = ReadAppData("TwainDuplex", "Default");
            gloGlobal.gloRemoteScanSettings.twainDuplex = RemoteScanSettings.TwainDuplex;

            gloGlobal.gloRemoteScanSettings.skipScannerList = ReadAppData("SkipScanners", "rdm").Split(',').Select(s => s.Trim().ToLower()).ToArray();
            if (RemoteScanSettings.EnableRemoteScan)
            {
                gloGlobal.gloRemoteScanSettings.bSuppressLastImageWait = Convert.ToBoolean(ReadAppData("bSuppressLastImageWait", "True"));
            }
            else
            {
                gloGlobal.gloRemoteScanSettings.bSuppressLastImageWait = Convert.ToBoolean(ReadAppData("bSuppressLastImageWait", "False"));
            }
            
            return RemoteScanSettings;
        }
        private static clsDownloadSettings GetDownloadSettings()
        {
            clsDownloadSettings DownloadSettings = new clsDownloadSettings();
            gloGlobal.gloTSPrint.isDeleteOrMove = true;
            DownloadSettings.claimdocprinter = ReadAppData("claimdocprinter", "Select Printer");
            DownloadSettings.ClaimPrinterData = ReadAppData("ClaimPrinterData", "");
            DownloadSettings.CMSclaimdocprinter = ReadAppData("CMSclaimdocprinter", "Select Printer");
            DownloadSettings.CMSClaimPrinterData = ReadAppData("CMSClaimPrinterData", "");
            DownloadSettings.UBclaimdocprinter = ReadAppData("UBclaimdocprinter", "Select Printer");
            DownloadSettings.UBClaimPrinterData = ReadAppData("UBClaimPrinterData", "");
            DownloadSettings.generaldocprinter = ReadAppData("generaldocprinter", "Select Printer");
            DownloadSettings.DocumentPrinterData = ReadAppData("DocumentPrinterData", "");

            DownloadSettings.IsCMSClaimAutoPrint = ReadAppData("IsCMSClaimAutoPrint", "true").ToLower() == "true";
            DownloadSettings.IsDocumentAutoPrint = ReadAppData("IsDocumentAutoPrint", "true").ToLower() == "true";
            DownloadSettings.IsUBClaimAutoPrint = ReadAppData("IsUBClaimAutoPrint", "true").ToLower() == "true";
            DownloadSettings.timerintervalInMin = ReadAppData("timerintervalInMin", "15");
            DownloadSettings.downloadpath = ReadAppData("downloadpath", "");

            if (isFromServiceTray)
            {
                DownloadSettings.ftpfolderpath = ReadAppData("ftpfolderpath", "");
                DownloadSettings.sourcepath = ReadAppData("sourcepath", "");
                DownloadSettings.ftp = ReadAppData("ftp", "Interface.gloclouds.com");
                DownloadSettings.user = ReadAppData("user", "");
                DownloadSettings.pwd = ReadAppData("pwd", "");
                string FtpDeleteFileLog = ReadAppData("FTPDeleteFileLog", "7");
                int.TryParse(FtpDeleteFileLog, out DownloadSettings.FTPDeleteFileLog);
                if (DownloadSettings.FTPDeleteFileLog <= 0)
                {
                    DownloadSettings.FTPDeleteFileLog = 7;
                }
                string DeleteFileLog = ReadAppData("DeleteFileLog", "7");
                int.TryParse(DeleteFileLog, out DownloadSettings.DeleteFileLog);
                if (DownloadSettings.DeleteFileLog <= 0)
                {
                    DownloadSettings.DeleteFileLog = 7;
                }
            }
            //else
            {
                string PrintedQueuePeriod = ReadAppData("PrintedQueuePeriod", "7");
                int.TryParse(PrintedQueuePeriod, out DownloadSettings.PrintedQueuePeriod);
            }
            return DownloadSettings;
        }
        private static void SetDownloadSettings(clsDownloadSettings DownloadSettings)
        {
            WriteAppData("claimdocprinter", DownloadSettings.claimdocprinter);
            WriteAppData("ClaimPrinterData", DownloadSettings.ClaimPrinterData);
            WriteAppData("CMSclaimdocprinter", DownloadSettings.CMSclaimdocprinter);
            WriteAppData("CMSClaimPrinterData", DownloadSettings.CMSClaimPrinterData);
            WriteAppData("UBclaimdocprinter", DownloadSettings.UBclaimdocprinter);
            WriteAppData("UBClaimPrinterData", DownloadSettings.UBClaimPrinterData);
            WriteAppData("generaldocprinter", DownloadSettings.generaldocprinter);
            WriteAppData("DocumentPrinterData", DownloadSettings.DocumentPrinterData);

            WriteAppData("IsCMSClaimAutoPrint", DownloadSettings.IsCMSClaimAutoPrint.ToString());
            WriteAppData("IsDocumentAutoPrint", DownloadSettings.IsDocumentAutoPrint.ToString());
            WriteAppData("IsUBClaimAutoPrint", DownloadSettings.IsUBClaimAutoPrint.ToString());
            WriteAppData("timerintervalInMin", DownloadSettings.timerintervalInMin);
            WriteAppData("FTPDeleteFileLog", DownloadSettings.FTPDeleteFileLog.ToString());
            WriteAppData("DeleteFileLog", DownloadSettings.DeleteFileLog.ToString());
            WriteAppData("downloadpath", DownloadSettings.downloadpath);
            if (isFromServiceTray)
            {
                WriteAppData("ftpfolderpath", DownloadSettings.ftpfolderpath);
                WriteAppData("sourcepath", DownloadSettings.sourcepath);
                WriteAppData("ftp", DownloadSettings.ftp);
                WriteAppData("user", DownloadSettings.user);
                WriteAppData("pwd", DownloadSettings.pwd);
            }
            //else
            {
                WriteAppData("PrintedQueuePeriod", DownloadSettings.PrintedQueuePeriod.ToString());
            }

        }
        public enum ModulePrintType
        {
            Documentprinter,
            Claimprinter,
            CMS1500Claimprinter,
            UB04Claimprinter,
            DefaultPrinter
        }
        private static clsTSPrinterSettings GetTSPrinterSettings()
        {
            clsTSPrinterSettings TSPrinterSettings = new clsTSPrinterSettings();

            gloGlobal.gloTSPrint.isDeleteOrMove = false;

            TSPrinterSettings.MapPath = ReadAppData("MapPath", "");
            if (String.IsNullOrEmpty(TSPrinterSettings.MapPath))
            {
                TSPrinterSettings.MapPath = cad.ApplicationFolderPath;
            }
            gloGlobal.gloTSPrint.mappedLocalPath = TSPrinterSettings.MapPath;

            String ClaimPrinterType = "", DocPrinterType = "", DefaultPrinterType = "";
            String PrinterConfigFolder = Path.Combine(gloGlobal.gloTSPrint.mappedLocalPath, gloGlobal.gloTSPrint.PrinterConfigDirectory);
            String MasterConfigFilePath = Path.Combine(PrinterConfigFolder, gloGlobal.gloTSPrint.MasterConfig);
            try
            {
                gloClinicalQueueGeneral.MasterConfigFileMasterConfig mainConfig = gloClinicalQueueGeneral.gloQueueMetadatawriter.GetMasterConfigFileData(MasterConfigFilePath);
                if (mainConfig != null)
                {
                    Dictionary<String, String> dictModuleConfig = gloClinicalQueueGeneral.gloQueueMetadatawriter.GenerateDictionaryForModuleConfig(mainConfig);
                    if (dictModuleConfig.Count > 0)
                    {
                        try
                        {
                            dictModuleConfig.TryGetValue(ModulePrintType.Claimprinter.ToString() + "_PrinterName", out TSPrinterSettings.claimdocprinter);
                            dictModuleConfig.TryGetValue(ModulePrintType.CMS1500Claimprinter.ToString() + "_PrinterName", out TSPrinterSettings.CMSclaimdocprinter);
                            dictModuleConfig.TryGetValue(ModulePrintType.UB04Claimprinter.ToString() + "_PrinterName", out TSPrinterSettings.UBclaimdocprinter);
                            dictModuleConfig.TryGetValue(ModulePrintType.Documentprinter.ToString() + "_PrinterName", out TSPrinterSettings.generaldocprinter);
                            dictModuleConfig.TryGetValue(ModulePrintType.DefaultPrinter.ToString() + "_PrinterName", out TSPrinterSettings.DefaultPrinter);

                            dictModuleConfig.TryGetValue(ModulePrintType.CMS1500Claimprinter.ToString() + "_PrinterType", out ClaimPrinterType);
                            dictModuleConfig.TryGetValue(ModulePrintType.Documentprinter.ToString() + "_PrinterType", out DocPrinterType);
                            dictModuleConfig.TryGetValue(ModulePrintType.DefaultPrinter.ToString() + "_PrinterType", out DefaultPrinterType);
                        }
                        catch
                        {
                        }

                    }
                    if (dictModuleConfig != null)
                    {
                        dictModuleConfig.Clear();
                        dictModuleConfig = null;
                    }
                }
            }
            catch 
            {
            }
            
            
            //string printerDirectory = Path.Combine(TSPrinterSettings.MapPath, gloTSPrint.PrinterConfigDirectory, gloTSPrint.MasterConfig);
            //TSPrinterSettings.claimdocprinter = ReadAppData("claimdocprinter", "");
            if (String.IsNullOrEmpty(TSPrinterSettings.claimdocprinter) || TSPrinterSettings.claimdocprinter == "Select Printer")
            {
                TSPrinterSettings.claimdocprinter = gloTSPrint.GetPrinter(ModulePrintType.Claimprinter.ToString());
            }
            TSPrinterSettings.ClaimPrinterData = ReadAppData("ClaimPrinterData", "");

            //TSPrinterSettings.CMSclaimdocprinter = ReadAppData("CMSclaimdocprinter", "");
            if (String.IsNullOrEmpty(TSPrinterSettings.CMSclaimdocprinter) || TSPrinterSettings.CMSclaimdocprinter == "Select Printer")
            {
                TSPrinterSettings.CMSclaimdocprinter = gloTSPrint.GetPrinter(ModulePrintType.CMS1500Claimprinter.ToString());
            }
            TSPrinterSettings.CMSClaimPrinterData = ReadAppData("CMSClaimPrinterData", "");

            //TSPrinterSettings.UBclaimdocprinter = ReadAppData("UBclaimdocprinter", "");
            if (String.IsNullOrEmpty(TSPrinterSettings.UBclaimdocprinter) || TSPrinterSettings.UBclaimdocprinter == "Select Printer")
            {
                TSPrinterSettings.UBclaimdocprinter = gloTSPrint.GetPrinter(ModulePrintType.UB04Claimprinter.ToString());
            }
            TSPrinterSettings.UBClaimPrinterData = ReadAppData("UBClaimPrinterData", "");

            //TSPrinterSettings.generaldocprinter = ReadAppData("generaldocprinter", "");
            if (String.IsNullOrEmpty(TSPrinterSettings.generaldocprinter) || TSPrinterSettings.generaldocprinter == "Select Printer")
            {
                TSPrinterSettings.generaldocprinter = gloTSPrint.GetPrinter(ModulePrintType.Documentprinter.ToString());
            }
            TSPrinterSettings.DocumentPrinterData = ReadAppData("DocumentPrinterData", "");

            //TSPrinterSettings.DefaultPrinter = ReadAppData("DefaultPrinter", "");
            if (String.IsNullOrEmpty(TSPrinterSettings.DefaultPrinter) || TSPrinterSettings.DefaultPrinter == "Select Printer")
            {
                TSPrinterSettings.DefaultPrinter = gloTSPrint.GetPrinter(ModulePrintType.DefaultPrinter.ToString());
            }
            TSPrinterSettings.DefaultPrinterData = ReadAppData("DefaultPrinterData", "");
            TSPrinterSettings.RDPExeName = ReadAppData("RDPExeName", "mstsc");
            TSPrinterSettings.timerintervalInMin = ReadAppData("timerintervalInMin", "1");
            //string FtpDeleteFileLog = ReadAppData("FTPDeleteFileLog", "7");
            //int.TryParse(FtpDeleteFileLog, out TSPrinterSettings.FTPDeleteFileLog);
            //if (TSPrinterSettings.FTPDeleteFileLog <= 0)
            //{
            //    TSPrinterSettings.FTPDeleteFileLog = 7;
            //}
            string DeleteFileLog = ReadAppData("DeleteFileLog", "7");
            int.TryParse(DeleteFileLog, out TSPrinterSettings.DeleteFileLog);
            if (TSPrinterSettings.DeleteFileLog <= 0)
            {
                TSPrinterSettings.DeleteFileLog = 7;
            }
            //  if (!isFromServiceTray)
            {
                string PrintedQueuePeriod = ReadAppData("PrintedQueuePeriod", "7");
                int.TryParse(PrintedQueuePeriod, out TSPrinterSettings.PrintedQueuePeriod);
                if (TSPrinterSettings.PrintedQueuePeriod <= 0)
                {
                    TSPrinterSettings.PrintedQueuePeriod = 7;
                }
            }

            //TSPrinterSettings.IsClaimDefaultPrint = ReadAppData("IsClaimDefaultPrint", "true").ToLower() == "true";
            //TSPrinterSettings.IsDocumentDefaultPrint = ReadAppData("IsDocumentDefaultPrint", "true").ToLower() == "true";
            //TSPrinterSettings.IsMSDefaultPrint = ReadAppData("IsMSDefaultPrint", "true").ToLower() == "true";
            
            TSPrinterSettings.IsClaimDefaultPrint = (String.IsNullOrWhiteSpace(ClaimPrinterType) || (ClaimPrinterType == "default"));
            TSPrinterSettings.IsDocumentDefaultPrint = (String.IsNullOrWhiteSpace(DocPrinterType) || (DocPrinterType == "default"));
            TSPrinterSettings.IsMSDefaultPrint = (String.IsNullOrWhiteSpace(DefaultPrinterType) || (DefaultPrinterType == "default"));

            //String defaultInstalledPrinterXML = ReadAppData("InstalledPrinters", "");
            //TSPrinterSettings.InstalledPrinterXML = String.IsNullOrWhiteSpace(defaultInstalledPrinterXML) ? System.Guid.NewGuid().ToString("N")+".xml" : defaultInstalledPrinterXML;
            return TSPrinterSettings;
        }

        private static void SetDeviceInterfaceSettings(clsDeviceInterfaceSettings DeviceInterfaceSettings)
        {
            WriteAppData("EnableLocalWelchAllynECGDevice", DeviceInterfaceSettings.EnableLocalWelchAllynECGDevice.ToString());
        }

        private static void SetSignaturePadSettings(clsSignaturePadSettings SignaturePadSettings)
        {
            WriteAppData("EnableLocalSignaturePad", SignaturePadSettings.EnableLocalSignaturePad.ToString());
            WriteAppData("IsNewSignaturePad", SignaturePadSettings.IsNewSignaturePad.ToString());
        }

        private static void SetRemoteScanSettings(clsRemoteScanSettings RemoteScanSettings)
        {
            WriteAppData("EnableRemoteScan", RemoteScanSettings.EnableRemoteScan.ToString());
            WriteAppData("EnableWebP", RemoteScanSettings.EnableWebP.ToString());
            //WriteAppData("EnableCompression", RemoteScanSettings.EnableBWCompression.ToString());
            WriteAppData("QualityOfBW", RemoteScanSettings.QualityOfBW.ToString());
            WriteAppData("QualityOfColor", RemoteScanSettings.QualityOfColor.ToString());
            WriteAppData("TwainVersion", RemoteScanSettings.TwainVersion);
            WriteAppData("TwainDuplex", RemoteScanSettings.TwainDuplex);
            WriteAppData("SkipScanners", String.Join(",", gloGlobal.gloRemoteScanSettings.skipScannerList));
            WriteAppData("bSuppressLastImageWait", gloGlobal.gloRemoteScanSettings.bSuppressLastImageWait.ToString());
        }

        private static void SetTSPrinterSettings(clsTSPrinterSettings TSPrinterSettings)
        {

            WriteAppData("claimdocprinter", TSPrinterSettings.claimdocprinter);
            WriteAppData("ClaimPrinterData", TSPrinterSettings.ClaimPrinterData);
            WriteAppData("CMSclaimdocprinter", TSPrinterSettings.CMSclaimdocprinter);
            WriteAppData("CMSClaimPrinterData", TSPrinterSettings.CMSClaimPrinterData);
            WriteAppData("UBclaimdocprinter", TSPrinterSettings.UBclaimdocprinter);
            WriteAppData("UBClaimPrinterData", TSPrinterSettings.UBClaimPrinterData);
            WriteAppData("generaldocprinter", TSPrinterSettings.generaldocprinter);
            WriteAppData("DocumentPrinterData", TSPrinterSettings.DocumentPrinterData);
            WriteAppData("DefaultPrinter", TSPrinterSettings.DefaultPrinter);
            WriteAppData("DefaultPrinterData", TSPrinterSettings.DefaultPrinterData);
            WriteAppData("RDPExeName", TSPrinterSettings.RDPExeName);
            //WriteAppData("FTPDeleteFileLog", TSPrinterSettings.FTPDeleteFileLog.ToString());
            WriteAppData("DeleteFileLog", TSPrinterSettings.DeleteFileLog.ToString());
            WriteAppData("MapPath", TSPrinterSettings.MapPath);
            WriteAppData("timerintervalInMin", TSPrinterSettings.timerintervalInMin);
            WriteAppData("IsClaimDefaultPrint", TSPrinterSettings.IsClaimDefaultPrint.ToString());
            WriteAppData("IsDocumentDefaultPrint", TSPrinterSettings.IsDocumentDefaultPrint.ToString());
            WriteAppData("IsMSDefaultPrint", TSPrinterSettings.IsMSDefaultPrint.ToString());
            //  if( !isFromServiceTray)
            {
                WriteAppData("PrintedQueuePeriod", TSPrinterSettings.PrintedQueuePeriod.ToString());
            }
            //WriteAppData("InstalledPrinters", TSPrinterSettings.InstalledPrinterXML);
        }

        public static void SetConfigurationFile(clsConfiguration cConfig = null)
        {
            if (cConfig != null)
            {
                Config = cConfig;
            }
            if (Config == null)
            {
                GetConfigurationFile();
            }
            //SaveConfigFile();
            clsSettingsConfiguration.SaveSettingsConfig(Config);
            GetConfigFile(false);
            GetConfigurationFile();
            if (Config.Config == null)
            {
                return;
            }
            string LowerServiceType = Config.ServiceType.ToLower();
            if (LowerServiceType == "download")
            {
                SetDownloadSettings(Config.DownLoadSettings);
            }
            if (LowerServiceType == "upload")
            {
                SetUploadSettings(Config.UpLoadSettings);
            }
            if (LowerServiceType == "defaultprinter")
            {
                SetTSPrinterSettings(Config.TSPrinterSettings);
            }

            SetDeviceInterfaceSettings(Config.DeviceInterfaceSettings);

            SetSignaturePadSettings(Config.SignaturePadSettings);

            SetRemoteScanSettings(Config.RemoteScanSettings);

            Config.Config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

        }

        //private static void SaveConfigFile()
        //{
        //    RegistryKey key = null;
        //    try
        //    {
        //        key = Registry.LocalMachine.OpenSubKey(m_sRegistryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
        //        if (key == null)
        //        {
        //            key = Registry.LocalMachine.CreateSubKey(m_sRegistryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
        //        }
        //        key.SetValue("ConfigFilePath", Config.ConfigPath);
        //        key.SetValue("ServiceType", Config.ServiceType);
        //        key.SetValue("ConfigInterval", Config.ConfigInterval.ToString());
        //        key.SetValue("ServiceBy", Config.ServiceBy.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!gloGlobal.clsMISC.IsService)
        //        {
        //            MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        return;
        //    }
        //    finally
        //    {
        //        if (key != null)
        //        {
        //            key.Close();
        //            key.Dispose();
        //            key = null;
        //        }
        //    }
        //    return;
        //}
        private static string ReadAppData(string var, string defaultvalue)
        {
            try
            {
                GetConfigurationFile();
                if (Config.Config == null)
                {
                    return defaultvalue;
                }
                if (Config.Config.AppSettings.Settings[var] == null)
                {
                    Config.Config.AppSettings.Settings.Add(var, defaultvalue);
                    return defaultvalue;

                }
                else
                {
                    return Config.Config.AppSettings.Settings[var].Value;
                }
            }
            catch (Exception ex)
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return defaultvalue;
            }

        }

        private static void WriteAppData(string var, string defaultvalue)
        {
            try
            {
                GetConfigurationFile();
                if (Config.Config == null)
                {
                    return;
                }
                if (Config.Config.AppSettings.Settings[var] == null)
                {
                    Config.Config.AppSettings.Settings.Add(var, defaultvalue);
                }
                else
                {
                    Config.Config.AppSettings.Settings[var].Value = defaultvalue;
                }
            }
            catch (Exception ex)
            {
                if (!gloGlobal.clsMISC.IsService)
                {

                    MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
    public class clsUploadSettings
    {
        public string ftpfolderpath = "";
        public string sourcepath = "";

        public string ftp = "";
        public string user = "";
        public string pwd = "";

        //public int FTPDeleteFileLog = 7;
        public int DeleteFileLog = 7;
        public string timerintervalInMin = "15";
    }

    public class clsDeviceInterfaceSettings
    {
        public bool EnableLocalWelchAllynECGDevice = false;
    }

    public class clsSignaturePadSettings
    {
        public bool EnableLocalSignaturePad = true;
        public bool IsNewSignaturePad = false;
    }

    public class clsRemoteScanSettings
    {
        public bool EnableRemoteScan = false;
        public bool EnableWebP = true;
        //public bool EnableBWCompression = true;
        public int QualityOfBW = 70;
        public int QualityOfColor = 100;
        public string TwainVersion = "Default";
        public string TwainDuplex = "Default";
    }

    public class clsDownloadSettings
    {
        public bool IsUpload = false;
        public bool IsDocumentAutoPrint = true;
        public bool IsCMSClaimAutoPrint = true;
        public bool IsUBClaimAutoPrint = true;

        public string claimdocprinter = "Select Printer";
        public string ClaimPrinterData = "";
        public string CMSclaimdocprinter = "Select Printer";
        public string CMSClaimPrinterData = "";
        public string UBclaimdocprinter = "Select Printer";
        public string UBClaimPrinterData = "";
        public string generaldocprinter = "Select Printer";
        public string DocumentPrinterData = "";

        public string downloadpath = "";
        public string ftpfolderpath = "";
        public string sourcepath = "";

        public string ftp = "";
        public string user = "";
        public string pwd = "";

        public int FTPDeleteFileLog = 7;
        public int DeleteFileLog = 7;
        public string timerintervalInMin = "15";
        public int PrintedQueuePeriod = 7;
    }
    public class clsTSPrinterSettings
    {
        public string MapPath = "";

        public string claimdocprinter = "Select Printer";
        public string ClaimPrinterData = "";
        public string CMSclaimdocprinter = "Select Printer";
        public string CMSClaimPrinterData = "";
        public string UBclaimdocprinter = "Select Printer";
        public string UBClaimPrinterData = "";
        public string generaldocprinter = "Select Printer";
        public string DocumentPrinterData = "";
        public string DefaultPrinter = "Select Printer";
        public string DefaultPrinterData = "";
        public string RDPExeName = "mstsc";

        public bool IsDocumentDefaultPrint = true;
        public bool IsClaimDefaultPrint = true;
        public bool IsMSDefaultPrint = true;

        //public int FTPDeleteFileLog = 7;
        public int DeleteFileLog = 7;
        public int PrintedQueuePeriod = 7;
        public string timerintervalInMin = "1";

        //public string InstalledPrinterXML = "";
    }
    public class clsConfiguration
    {
        public Configuration Config = null;
        public string ServiceType = null;
        public string ConfigPath = null;
        public long ConfigInterval = 2000;
        public bool ServiceBy = false;
        public clsUploadSettings UpLoadSettings = new clsUploadSettings();
        public clsDownloadSettings DownLoadSettings = new clsDownloadSettings();
        public clsTSPrinterSettings TSPrinterSettings = new clsTSPrinterSettings();
        public clsSignaturePadSettings SignaturePadSettings = new clsSignaturePadSettings();
        public clsRemoteScanSettings RemoteScanSettings = new clsRemoteScanSettings();
        public bool bAddLogForDebug = false;
        public clsDeviceInterfaceSettings DeviceInterfaceSettings = new clsDeviceInterfaceSettings(); 
    }

    public static class clsSettingsConfiguration
    {
        private static string companyFolder = "TriarqHealthLog";
        private static string ApplicationFolder = "gloSettings";
        private static CommonApplicationData cad = new CommonApplicationData(companyFolder, ApplicationFolder, allUsers: true, createConfig:false, createLog: false);
        private static System.Configuration.Configuration config = null;

        private static void getConfig()
        {
            if (config == null)
            {
                ExeConfigurationFileMap objfile = new ExeConfigurationFileMap();
                objfile.ExeConfigFilename = Path.Combine(cad.ApplicationFolderPath, "settings.config");
                config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(objfile, ConfigurationUserLevel.None);
                objfile = null;
            }
        }

        public static bool isSettingsFileExists()
        {
            bool bisFileExist = false;
            try
            {
                if (File.Exists(Path.Combine(cad.ApplicationFolderPath, "settings.config")))
                {
                    bisFileExist = true;
                }
                else
                {
                    config = null;
                }
            }
            catch
            {
                config = null;
            }
            return bisFileExist;
        }

        public static string ReadAppData(string var, string defaultvalue)
        {
            try
            {
                getConfig();
                if (config == null)
                {
                    return defaultvalue;
                }
                if (config.AppSettings.Settings[var] == null)
                {
                    config.AppSettings.Settings.Add(var, defaultvalue);
                    return defaultvalue;
                }
                else
                {
                    return config.AppSettings.Settings[var].Value;
                }
            }
            catch (Exception ex)
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return defaultvalue;
            }

        }

        private static void WriteAppData(string var, string defaultvalue)
        {
            try
            {
                getConfig();
                if (config == null)
                {
                    return;
                }
                if (config.AppSettings.Settings[var] == null)
                {
                    config.AppSettings.Settings.Add(var, defaultvalue);
                }
                else
                {
                    config.AppSettings.Settings[var].Value = defaultvalue;
                }
            }
            catch (Exception ex)
            {
                if (!gloGlobal.clsMISC.IsService)
                {

                    MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void SaveSettingsConfig(clsConfiguration clsConfig)
        {
            try
            {
                config = null;
                WriteAppData("ConfigFilePath", clsConfig.ConfigPath);
                WriteAppData("ServiceType", clsConfig.ServiceType);
                WriteAppData("ConfigInterval", clsConfig.ConfigInterval.ToString());
                WriteAppData("ServiceBy", clsConfig.ServiceBy.ToString());
                WriteAppData("AddLogForDebug", clsConfig.bAddLogForDebug.ToString());

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    MessageBox.Show(ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
