using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;


namespace gloRemoteScanGeneral
{
    public enum CardScanStatus
    {
        SentToScan = 0,
        Success = 1,
        Information = 2,
        Error = 3
    }

    public class gloRemoteScanMetaDataWriter
    {
        public static ScannerCurrentSettingsScannerSettings ScannerSetting = null;
        public static gloGlobal.clsDatalog logdata = new gloGlobal.clsDatalog("gloTSPrint");

        public static void UpdateOrCreateScanConfigFiles(ScannersInstalledScanners aScannerList, bool bUseClipboard = false, int nProcessId = 0)
        {
            try
            {
                //XML Setings File
                string ScanConfigFolderPath = System.IO.Path.Combine(gloGlobal.gloTSPrint.mappedLocalPath, gloGlobal.gloRemoteScanSettings.ScanFolderName, gloGlobal.gloRemoteScanSettings.ScanSettingsFolderName); // your code goes here

                //if (Directory.Exists(ScanConfigFolderPath) == false)
                //{
                //    Directory.CreateDirectory(ScanConfigFolderPath);
                //}

                gloGlobal.gloRemoteScanSettings.CreateDir(ScanConfigFolderPath);
                //     bool exists = gloAuditTrail.gloAuditTrail.CreateDirectoryIfNotExists(ScanConfigFolderPath);

                string sScanSettingsFile = System.Guid.NewGuid().ToString() + ".xml";
                string ScanConfigFilePath = System.IO.Path.Combine(ScanConfigFolderPath, sScanSettingsFile);


                CreateXMLFile(aScannerList, ScanConfigFilePath, bUseClipboard, nProcessId);

                //ScanMasterConfig File

                //ScanMasterConfig objMasterList = new ScanMasterConfig();
                //objMasterList.Items = new ScanMasterConfigMasterConfig[1];

                ScanMasterConfigMasterConfig aScanMasterList = new ScanMasterConfigMasterConfig();
                aScanMasterList.ScanSettingsFile = sScanSettingsFile;

                String ScanMasterConfigFilePath = Path.Combine(ScanConfigFolderPath, gloGlobal.gloRemoteScanSettings.ScanMasterConfig);
                String tempFile = Path.Combine(ScanConfigFolderPath, "Temp_" + System.Guid.NewGuid().ToString() + ".xml");
                //gloQueueSchema.gloSerialization.SetMasterConfigDocument(tempFile, mainConfig);
                CreateXMLFile(aScanMasterList, tempFile);

                CopyAndDeleteFile(tempFile, ScanMasterConfigFilePath);

                //if (File.Exists(tempFile))
                //{
                //    File.Copy(tempFile, ScanMasterConfigFilePath, true);
                //    File.Delete(tempFile);
                //}
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
            finally
            {

            }

        }

        public static void CopyAndDeleteFile(string SourceFileName, string DestFileName, bool bOnlydelete = false)
        {
            try
            {
                //logdata.Log("In CopyAndDeleteFile");
                if (File.Exists(SourceFileName))
                {
                    //logdata.Log("Before Copy --> DEST --> " + DestFileName);
                    if (!bOnlydelete)
                    {
                        File.Copy(SourceFileName, DestFileName, true);
                    }
                    //logdata.Log("After Copy");
                    try
                    {
                        //logdata.Log("Before Delete");
                        File.Delete(SourceFileName);
                        //logdata.Log("After Delete");
                    }
                    catch (Exception ex)
                    {
                        logdata.ExceptionLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
            finally
            {
            }
        }

        public static void CreateXMLFile(ScanMasterConfigMasterConfig aScanMasterList, string path)
        {
            string sFilePath = path;// +"\\" + System.Guid.NewGuid().ToString() + ".xml";
            try
            {
                if (File.Exists(sFilePath) == false)
                {

                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();

                    Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
                    settings.Encoding = utf8EncodingWithOutBOM;
                    settings.Indent = true;
                    System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(ScanMasterConfigMasterConfig));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(ms, settings))//sFilePath
                        {
                            s.Serialize(w, aScanMasterList);
                        }
                        Byte[] barrMS = ms.ToArray();
                        File.WriteAllBytes(sFilePath, barrMS);
                        barrMS = null;
                    }
                    //using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(sFilePath, settings))
                    //{
                    //    s.Serialize(w, aScanMasterList);
                    //}

                }
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
            finally
            {
            }
        }

        public static void CreateXMLFile(ScannersInstalledScanners aScannerList, string path, bool bUseClipboard = false, int nProcessId = 0)
        {
            string sFilePath = path;// +"\\" + System.Guid.NewGuid().ToString() + ".xml";
            try
            {
                //if (File.Exists(sFilePath) == false)
                {

                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();

                    Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
                    settings.Encoding = utf8EncodingWithOutBOM;
                    settings.Indent = true;
                    System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(ScannersInstalledScanners));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(ms, settings))//sFilePath
                        {
                            s.Serialize(w, aScannerList);
                        }
                        Byte[] barrMS = ms.ToArray();
                        File.WriteAllBytes(sFilePath, barrMS);
                        if (bUseClipboard)
                        {
                            if (gloGlobal.gloProgressAndClipboard.SetImageToClipboard(null, ".xml", false, "", false, 0, nProcessId, 1, barrMS))
                            {
                                //CloseRemoteScanLauncherForWIA();
                            }
                        }
                        barrMS = null;
                    }
                    //using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(sFilePath, settings))
                    //{
                    //    s.Serialize(w, aScannerList);
                    //}

                }
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
            finally
            {
            }
        }

        public static bool CreateXMLFile(ScannerCurrentSettingsScannerSettings aScannerSettings, string path)
        {
            string sFilePath = path;// +"\\" + System.Guid.NewGuid().ToString() + ".xml";
            try
            {

                //if (File.Exists(sFilePath) == false)
                {
                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();

                    Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
                    settings.Encoding = utf8EncodingWithOutBOM;
                    settings.Indent = true;
                    System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(ScannerCurrentSettingsScannerSettings));

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(ms, settings))//sFilePath
                        {
                            s.Serialize(w, aScannerSettings);
                        }
                        Byte[] barrMS = ms.ToArray();
                        File.WriteAllBytes(sFilePath, barrMS);
                        barrMS = null;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
                return false;
            }
            finally
            {
            }

        }

        public static bool CreateXMLFile(CardScanSettingsScanCardSettings cardScanSettings, string path)
        {
            //string sFilePath = path;// +"\\" + System.Guid.NewGuid().ToString() + ".xml";
            //try
            //{

            //    if (File.Exists(sFilePath) == false)
            //    {
            //        System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();

            //        Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
            //        settings.Encoding = utf8EncodingWithOutBOM;
            //        settings.Indent = true;
            //        System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(CardScanSettingsScanCardSettings));

            //        using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(sFilePath, settings))
            //        {
            //            s.Serialize(w, cardScanSettings);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logdata.ExceptionLog(ex);
            //}
            //finally
            //{
            //}

            string sFilePath = path;// +"\\" + System.Guid.NewGuid().ToString() + ".xml";
            try
            {

                //if (File.Exists(sFilePath) == false)
                {
                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();

                    Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
                    settings.Encoding = utf8EncodingWithOutBOM;
                    settings.Indent = true;
                    System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(CardScanSettingsScanCardSettings));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(ms, settings))//sFilePath
                        {
                            s.Serialize(w, cardScanSettings);
                        }
                        Byte[] barrMS = ms.ToArray();
                        File.WriteAllBytes(sFilePath, barrMS);
                        barrMS = null;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
                return false;
            }
            finally
            {
            }

        }

        public static void UpdateScanCardSettingFileData(CardScanSettingsScanCardSettings cardConfig, String CardConfigFilePath)
        {
            try
            {
                String tempFile = Path.Combine(Path.GetDirectoryName(CardConfigFilePath), gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xml");
                CreateXMLFile(cardConfig, tempFile);
                if (File.Exists(tempFile))
                {
                    File.Copy(tempFile, CardConfigFilePath, true);
                    File.Delete(tempFile);
                }
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
        }

        public static CardScanSettingsScanCardSettings GetScanCardSettings(string settingsFile)
        {
            CardScanSettingsScanCardSettings ScancardSetting = null;
            try
            {
                if (File.Exists(settingsFile))
                {
                    try
                    {
                        ScancardSetting = Deserialize<CardScanSettingsScanCardSettings>(@"" + settingsFile + "");
                    }
                    catch (Exception ex)
                    {
                        logdata.ExceptionLog(ex);
                        ScancardSetting = null;
                    }
                }

                //logdata.Log("IN GetScanCardSettings");
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
            return ScancardSetting;
        }

        public static ScannerCurrentSettingsScannerSettings GetScannerCurrentSettings(string ScannerSettingsPath)
        {
            try
            {
                ScannerSetting = null;

                if (File.Exists(ScannerSettingsPath))
                {
                    try
                    {
                        ScannerSetting = Deserialize<ScannerCurrentSettingsScannerSettings>(@"" + ScannerSettingsPath + "");
                    }
                    catch (Exception ex)
                    {
                        logdata.ExceptionLog(ex);
                        ScannerSetting = null;
                    }
                }

                // logdata.Log("IN GetScannerCurrentSettings");
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
            return ScannerSetting;
        }

        public static ScanMasterConfigMasterConfig GetMasterConfigSettings(bool bEliminatePegasus = false)
        {
            
            
            
            string ScannerMasterFilePath = string.Empty;
            if (bEliminatePegasus)
            {
                gloGlobal.clsDatalog logdata = new gloGlobal.clsDatalog();
                ScannerMasterFilePath = Path.Combine(gloGlobal.clsDatalog.ApplicationPathForTwainScan, gloGlobal.gloRemoteScanSettings.ScanMasterConfig);
            }
            else
            {
                ScannerMasterFilePath = Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloRemoteScanSettings.ScanFolderName, gloGlobal.gloRemoteScanSettings.ScanSettingsFolderName, gloGlobal.gloRemoteScanSettings.ScanMasterConfig);
            }

            ScanMasterConfigMasterConfig oScanMasterConfig = null;

            try
            {

                if (!File.Exists(ScannerMasterFilePath))
                {
                    try
                    {
                        if (bEliminatePegasus)
                        { 
                            if (!gloRemoteScanGeneral.TwainScanFunctionality.CreateTwainScanSettingsFile())
                            {
                                logdata.Log("Twain config file could not be created.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logdata.ExceptionLog(ex);
                        oScanMasterConfig = null;
                    }
                }
                try
                {
                    
                    if (File.Exists(ScannerMasterFilePath))  //file exist condition checked 
                    {
                        oScanMasterConfig = Deserialize<ScanMasterConfigMasterConfig>(@"" + ScannerMasterFilePath + "");
                    }
                }
                catch (Exception ex)
                {
                    logdata.ExceptionLog(ex);
                    oScanMasterConfig = null;
                }

                // logdata.Log("IN GetMasterConfigSettings");
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
            return oScanMasterConfig;
        }

        public static void DeleteScanConfigFiles()
        {
            List<String> keepFile = null;
            try
            {
                gloRemoteScanGeneral.ScanMasterConfigMasterConfig oScanMasterConfig = gloRemoteScanGeneral.gloRemoteScanMetaDataWriter.GetMasterConfigSettings(true);

                if (oScanMasterConfig != null)
                {
                    keepFile = new List<string>();

                    keepFile.Add(oScanMasterConfig.ScanSettingsFile);

                    keepFile.Add(gloGlobal.gloRemoteScanSettings.ScanMasterConfig);
                    //keepFile.Add(gloGlobal.gloRemoteScanSettings.ScanNoSet);

                    string ScannerSettingsFolder = Path.Combine(gloGlobal.clsDatalog.ApplicationPathForTwainScan);

                    gloGlobal.gloRemoteScanSettings.DeleteOldTwainScanConfigFiles(ScannerSettingsFolder, keepFile);

                }

            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
            finally
            {
                if (keepFile != null)  //object clear
                {
                    keepFile.Clear();
                    keepFile = null;
                }
            }
        }

        public static ScannersInstalledScanners GetRemoteScanSettings(bool bEliminatePegasus = false)
        {

            string ScannerSettingsFilePath = string.Empty;
            ScannersInstalledScanners oScannerSettings = null;
            bool settingsLoaded = false;

            if (bEliminatePegasus == false && gloGlobal.gloRemoteScanSettings.bZipScanSettings)
            {
                try
                {
                    if (gloGlobal.gloRemoteScanSettings.CurrLocalScanSettingBytes != null)
                    {
                        oScannerSettings = Deserialize<ScannersInstalledScanners>(gloGlobal.gloRemoteScanSettings.CurrLocalScanSettingBytes);
                        if (oScannerSettings != null)
                        {
                            settingsLoaded = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logdata.ExceptionLog(ex);
                    oScannerSettings = null;
                }
            }

            if (!settingsLoaded)
            {
                ScanMasterConfigMasterConfig oScanMasterConfig = GetMasterConfigSettings(bEliminatePegasus);
                if (bEliminatePegasus)
                {
                    gloGlobal.clsDatalog logdata = new gloGlobal.clsDatalog();
                    ScannerSettingsFilePath = Path.Combine(gloGlobal.clsDatalog.ApplicationPathForTwainScan, oScanMasterConfig.ScanSettingsFile);
                }
                else
                {
                    ScannerSettingsFilePath = Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloRemoteScanSettings.ScanFolderName, gloGlobal.gloRemoteScanSettings.ScanSettingsFolderName, oScanMasterConfig.ScanSettingsFile);

                }

                try
                {

                    if (File.Exists(ScannerSettingsFilePath))
                    {
                        try
                        {
                            oScannerSettings = Deserialize<ScannersInstalledScanners>(@"" + ScannerSettingsFilePath + "");
                        }
                        catch (Exception ex)
                        {
                            logdata.ExceptionLog(ex);
                            oScannerSettings = null;
                        }
                    }

                    // logdata.Log("IN GetMasterConfigSettings");
                }
                catch (Exception ex)
                {
                    logdata.ExceptionLog(ex);
                }
            }
            return oScannerSettings;
        }

        private static T Deserialize<T>(string pathName)
        {
            XmlSerializer serializer = null;
            byte[] fileArray = null;
            try
            {
                fileArray = File.ReadAllBytes(pathName);
                using (MemoryStream ms = new MemoryStream(fileArray))
                {
                    using (TextReader reader = new StreamReader(ms))
                    {
                        serializer = new XmlSerializer(typeof(T));
                        return (T)serializer.Deserialize(reader);
                    }
                }

                //using (TextReader reader = new StreamReader(pathName))
                //{
                //    serializer = new XmlSerializer(typeof(T));
                //    return (T)serializer.Deserialize(reader);
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                serializer = null;
                fileArray = null;
            }

        }

        private static T Deserialize<T>(byte[] objectBytes)
        {
            XmlSerializer serializer = null;
            try
            {
                using (MemoryStream ms = new MemoryStream(objectBytes))
                {
                    using (TextReader reader = new StreamReader(ms))
                    {
                        serializer = new XmlSerializer(typeof(T));
                        return (T)serializer.Deserialize(reader);
                    }
                }

                //using (TextReader reader = new StreamReader(pathName))
                //{
                //    serializer = new XmlSerializer(typeof(T));
                //    return (T)serializer.Deserialize(reader);
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                serializer = null;
            }

        }

    }
}
