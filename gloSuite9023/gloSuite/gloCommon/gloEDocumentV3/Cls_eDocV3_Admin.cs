using System;
using System.Collections.Generic;
using System.Text;
using gloEDocumentV3.Enumeration;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Win32;
using gloSettings;


namespace gloEDocumentV3
{
    public static class     gloEDocV3Admin
    {
        //public static Int64 gPatientID = 0;
        public static string gstrFaxOutputDirectory;

        // CR00000126 : FAX for Terminal Server
        // New setting added for ReceivedFaxFolder useful only for Terminal Server 
        public static string gstrFaxReceivedDirectoryTS;

        public static bool blnIsInternetFaxEnabled;
        public static bool gblnAddFaxCoverpage;
        //sarika 20090206 New Task Implementation as in gloPM
        public static string gUserName = "";
        public static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public static bool blnUseDefaultPrinterDialog = false;

        //--
        public static bool ISDMSDEMO;  //dhruv boolean variable declaration

        // 20090714
        //DMSArchiveDatabase ; DMSArchivePassword ;DMSArchiveSQLServer ;DMSArchiveUserName

        #region "Archive Settings"

        #region "DMS Settings
        private static string gstrArchiveDatabaseName = string.Empty;
        private static string gstrArchiveSQLserverName = string.Empty;
        public static string gstrDMSDatabaseName = string.Empty;
        public static string gstrDMSSqlServerName = string.Empty;
        private static Boolean gblnSQLAuthentication = false;

        private static string gstrArchiveDatabaseConnectionString = string.Empty;
        private static string gstrSQLUser = string.Empty;
        private static string gstrSQLPassword = string.Empty;

        internal static string getArchiveDatabaseConnectionString
        {
            get
            {
                if (getgloDMSSettings())
                {
                    if (gstrArchiveDatabaseConnectionString.Trim().Length > 0)
                    {
                        return gstrArchiveDatabaseConnectionString;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        internal static string gloArchiveDatabaseName
        {
            get
            {
                if (gstrArchiveDatabaseName.Trim().Length > 0)
                {
                    return gstrArchiveDatabaseName;
                }
                else
                {
                    return getArchiveDatabaseName();
                }

            }
        }


        internal static string gloArchiveSQLServerName
        {
            get
            {
                if (gstrArchiveSQLserverName.Trim().Length > 0)
                {
                    return gstrArchiveSQLserverName;
                }
                else
                {
                    return getArchiveSQLServerName();
                }

            }
        }

        private static bool getgloDMSSettings()
        {



            try
            {
                if (appSettings != null)
                {
                    gstrArchiveDatabaseName = Convert.ToString(appSettings["DatabaseName"]).Trim() + "_Archive";
                    gstrArchiveSQLserverName = Convert.ToString(appSettings["SQLServerName"]);
                    gstrSQLPassword = Convert.ToString(appSettings["SQLPassword"]);
                    gstrSQLUser = Convert.ToString(appSettings["SQLLoginName"]);
                    gblnSQLAuthentication = !Convert.ToBoolean(appSettings["WindowAuthentication"]);
                    //if (gblnSQLAuthentication)
                    //    gstrArchiveDatabaseConnectionString = "SERVER=" + gstrArchiveSQLserverName + ";DATABASE=" + gstrArchiveDatabaseName + ";USER id=" + gstrSQLUser + ";Password=" + gstrSQLPassword;
                    //else
                        gstrArchiveDatabaseConnectionString = "SERVER=" + gstrArchiveSQLserverName + ";DATABASE=" + gstrArchiveDatabaseName + ";Integrated Security=SSPI";
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {

                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                return false;
            }

        }

        public static string DecryptFromBase64String(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] key = new byte[] { }; // we are going to pass in the key portion in our method calls
            byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray;//new byte[stringToDecrypt.Length + 1];
            // Note: The DES CryptoService only accepts certain key byte lengths
            // We are going to make things easy by insisting on an 8 byte legal key length

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    // we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    // now decrypt the regular string
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                            return encoding.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string _MessageString = e.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                return e.Message;
            }
        }

        private static String getArchiveDatabaseName()
        {

            string _str = string.Empty;
            try
            {
                if (getgloDMSSettings())
                {
                    _str = gstrArchiveDatabaseName;
                }
                else
                {
                    string _MessageString = "Unable to Read DMS settings.";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                }
            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);

            }
            return _str;
        }
        private static String getArchiveSQLServerName()
        {
            string _str = string.Empty;
            try
            {
                if (getgloDMSSettings())
                {
                    _str = gstrArchiveSQLserverName;
                }
                else
                {
                    string _MessageString = "Unable to Read DMS settings.";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                }
            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            }
            return _str;
        }

        #endregion "DMS Settings

        #region "EMR Settings
        private static string gstrEMRDatabaseName = string.Empty;
        private static string gstrEMRSQLserverName = string.Empty;

        internal static string gloEMRDatabaseName
        {
            get
            {
                if (gstrEMRDatabaseName.Trim().Length > 0)
                {
                    return gstrEMRDatabaseName;
                }
                else
                {
                    return getEMRDatabaseName();
                }

            }
        }
        internal static string gloEMRSQLServerName
        {
            get
            {
                if (gstrEMRSQLserverName.Trim().Length > 0)
                {
                    return gstrEMRSQLserverName;
                }
                else
                {
                    return getEMRSQLServerName();
                }

            }
        }

        private static bool getgloEMRSettings()
        {
            //Microsoft.Win32.RegistryKey regKey = null;


            try
            {

                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) != false)

                // if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\gloEMR") != null)
                {
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true);
                    //regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\gloEMR", true);
                    if (gloRegistrySetting.GetRegistryValue("Database") == null)
                    {
                        return false;
                    }
                    if (gloRegistrySetting.GetRegistryValue("SQLServer") == null)
                    {
                        return false;
                    }

                    gstrEMRDatabaseName = Convert.ToString(gloRegistrySetting.GetRegistryValue("Database"));
                    gstrEMRSQLserverName = Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLServer"));
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string _MessageString = "Unable to read registry values - " + ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);

                return false;
            }
            finally
            {
                gloRegistrySetting.CloseRegistryKey();
            }

        }
        private static String getEMRDatabaseName()
        {

            string _str = string.Empty;
            try
            {
                if (getgloEMRSettings())
                {
                    _str = gstrEMRDatabaseName;
                }
                else
                {
                    string _MessageString = "Unable to Read DMS settings.";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                }
            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);

            }
            return _str;
        }
        private static String getEMRSQLServerName()
        {

            string _str = string.Empty;
            try
            {
                if (getgloDMSSettings())
                {
                    _str = gstrEMRSQLserverName;
                }
                else
                {
                    string _MessageString = "Unable to Read DMS settings.";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                }
            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            }
            return _str;
        }

        #endregion "EMR Settings
        #endregion "Archive Settings"


        //**** 20090714
        //'-------------------sarika internet fax 20080714

        internal static string gMessageBoxCaption = "gloEMR";
        internal static string gDatabaseConnectionString = "";
        //Added by Rahul Patel on 26-10-2010
        //For DMS Connection String
        internal static string gDMSDatabaseConnectionString = "";
        //End of Code Added By Rahul Patel

        internal static string gPDFTronResourcePath = "";
        public static string gTemporaryProcessPath = "";


        //internal static string gPDFTronTemporaryProcessPath = Environment.SystemDirectory + "\\gloTempData";
        internal static string gErrorLogFilePath = "";
        internal static string gPDFTronTemporaryProcessPath = "";
        internal static string gLuraTechTemporaryProcessPath = "";
        internal static string gDocumentOpenTemporaryProcessPath = "";
        //internal static string gDMSV1Path = "";
        internal static Int64 gUserID = 0;
        internal static Int64 gClinicID = 0;


        // GLO2011-0012077 DMS Zoom Issue
        // global variable decalred which will hold the current zoom value during merge/move etc.
        internal static int gStrZoomIndexValue = 0; 

        //internal static string gFontName = "Tahoma";
        //internal static float gFontSize = 8.25F;
         

        internal static int gBufferSize = 1048576;
        internal static Int64 gImageMaxDPI = 300;
        internal static int gFilePageMaxThreshold = 20;
        internal static int gFilePageMinThreshold = 10;

        //--PDF Tron Ini/Terminate
        public static bool gIsPDFTronConnected = false;
        public static bool gIsPDFTronExpired = false;

        // For Spliting The Document
        internal static Int64 gnDMSV3FileSizeMax = 10 * 1024 * 1024;
        internal static Int64 gnDMSV3FileSizeMin = 5 * 1024 * 1024;
        //

        public static bool LoadDocumentsWithCompatibleMode = false;
        public static bool IsRead_LoadDocumentsWithCompatibleMode = false;

        // DMS Migration
        public static bool gDeleteAfterMigration = false;
        public static bool gUseCompressionForMigration = false;
        public static bool gblnAssociatedProvider = false;
        private static Int64 GetImageDPISetting()
        {
            Int64 _result = 300;
            try
            {
                using (Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gDatabaseConnectionString))
                {
                    if (oDBLayer != null)
                    {
                        if (oDBLayer.Connect(false))
                        {


                            object _ret = oDBLayer.ExecuteScalar_Query("SELECT sSettingsValue FROM Settings WHERE UPPER(sSettingsName) = 'DMSIMAGEDPI'");
                            if (_ret != null && _ret.ToString() != "")
                            {
                                _result = Convert.ToInt64(_ret);
                            }
                            if (_ret != null)
                            {
                                _ret = null;
                            }
                            if (oDBLayer != null)
                            {
                                oDBLayer.Disconnect();

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);

            }
            return _result;
        }

        private static bool GetUseCompressionForMigration()
        {
            bool _result = false;

            try
            {
                using (Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gDatabaseConnectionString))
                {
                    if (oDBLayer != null)
                    {
                        if (oDBLayer.Connect(false))
                        {

                            object _ret = oDBLayer.ExecuteScalar_Query("SELECT sSettingsValue FROM Settings WHERE UPPER(sSettingsName) = 'ALLOWCOMPRESSION'");
                            if (_ret != null && _ret.ToString() != "")
                            {
                                if (Convert.ToInt64(_ret.ToString()) > 0)
                                {
                                    _result = true;
                                }
                            }
                            if (_ret != null)
                            {
                                _ret = null;
                            }
                            if (oDBLayer.Connect(false))
                            {
                                oDBLayer.Disconnect();
                            }
                        }
                    }
                    return _result;
                }

            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);

            }
            return _result;
        }


        public static int GetMemoryBuferSetting()
        {
            int _result = 40480;
            try
            {

                //Microsoft.Win32.RegistryKey regKey=null ;
                object buffersize = null;

                if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) != false)
                {
                    //  regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\gloEMR", true);
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true);
                    if (gloRegistrySetting.GetRegistryValue("DMSBufferSize") != null && gloRegistrySetting.GetRegistryValue("DMSBufferSize").ToString() != "")
                    {
                        buffersize = gloRegistrySetting.GetRegistryValue("DMSBufferSize");
                        _result = (int)gloRegistrySetting.GetRegistryValue("DMSBufferSize");
                    }
                }

            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);

            }
            finally
            {
                gloRegistrySetting.CloseRegistryKey();
            }
            return _result;
        }
        //-- Added By Rahul Patel on 26-10-2010
        //-- For DMS Connection String --'
        public static string GetDMSConnectionString(string strSQLServerName, string strDatabase, bool isSQLAuthentication, string sUserName, string sPassword)
        {
            string strConnectionString = null;
            if (isSQLAuthentication == false)
            {
                strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabase + ";Integrated Security=SSPI";
            }
            else
            {
                strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabase + ";User ID=" + sUserName + ";Password=" + sPassword + "";
            }
            //ConnectionString = strConnectionString
            return strConnectionString;
        }
        public static string GetDMSConnectionString(string DatabaseConnectionString)
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(DatabaseConnectionString);

            string sDmsServerName = "";
            string sDmsDatabaseName = "";
            string sDmsUserId = "";
            string sDmsPassword = "";
            bool sDmsIsSqlAuthentication = false;
            object oValue = null;
            
            oSetting.GetSetting("GLODMSSERVERNAME", out oValue);
            if (oValue != null)
            {
                sDmsServerName = oValue.ToString();
                oValue = null;
            }
            oSetting.GetSetting("GLODMSDBNAME", out oValue);
            if (oValue != null)
            {
                sDmsDatabaseName = oValue.ToString();
                oValue = null;
            }
            oSetting.GetSetting("GLODMSUSERID", out oValue);
            if (oValue != null)
            {
                sDmsUserId = oValue.ToString();
                oValue = null;
            }
            oSetting.GetSetting("GLODMSPASSWORD", out oValue);
            if (oValue != null)
            {
                sDmsPassword = oValue.ToString();
                oValue = null;
            }
            oSetting.GetSetting("GLODMSAUTHEN", out oValue);
            if (oValue != null)
            {
                sDmsIsSqlAuthentication = Convert.ToBoolean(oValue);
                oValue = null;
            }
            oSetting.Dispose();
            oSetting = null;
            return GetDMSConnectionString(sDmsServerName, sDmsDatabaseName, sDmsIsSqlAuthentication, sDmsUserId, sDmsPassword);
        }

        public static void Connect(string DatabaseConnectionString, string TemporaryProcessPath, Int64 UserID, Int64 ClinicID, string PDFTronResourcePath)
        {
            Connect(DatabaseConnectionString, GetDMSConnectionString(DatabaseConnectionString), TemporaryProcessPath, UserID, ClinicID, PDFTronResourcePath);
        }

        public static void Connect(string DatabaseConnectionString, string DMSDatabaseConnectionString, string TemporaryProcessPath, Int64 UserID, Int64 ClinicID, string PDFTronResourcePath)
        {
            gDatabaseConnectionString = DatabaseConnectionString;
            gDMSDatabaseConnectionString = DMSDatabaseConnectionString;
            gTemporaryProcessPath = TemporaryProcessPath;
            gUserID = UserID;
            gClinicID = ClinicID;
            gPDFTronResourcePath = PDFTronResourcePath + "\\pdfnet.res";
            gErrorLogFilePath = PDFTronResourcePath + "\\DMSErrorLog.txt";

            if (Directory.Exists(gTemporaryProcessPath) == false)
            {
                Directory.CreateDirectory(gTemporaryProcessPath);
                if (Directory.Exists(gTemporaryProcessPath) == false)
                {
                    string _ErrorMessage = "Unable to create directory. " + gTemporaryProcessPath;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
            }

            gPDFTronTemporaryProcessPath = gTemporaryProcessPath + "\\gloPDFTronTempData";
            if (Directory.Exists(gPDFTronTemporaryProcessPath) == false)
            {
                Directory.CreateDirectory(gPDFTronTemporaryProcessPath);
                if (Directory.Exists(gPDFTronTemporaryProcessPath) == false)
                {
                    string _ErrorMessage = "Unable to create directory. " + gPDFTronTemporaryProcessPath;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                }
            }

            gLuraTechTemporaryProcessPath = gTemporaryProcessPath + "\\gloLuraTechTempData";
            if (Directory.Exists(gLuraTechTemporaryProcessPath) == false)
            {
                Directory.CreateDirectory(gLuraTechTemporaryProcessPath);
                if (Directory.Exists(gLuraTechTemporaryProcessPath) == false)
                {
                    string _ErrorMessage = "Unable to create directory. " + gLuraTechTemporaryProcessPath;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                }
            }

            gDocumentOpenTemporaryProcessPath = gTemporaryProcessPath + "\\gloDocumentOpenData";
            if (Directory.Exists(gDocumentOpenTemporaryProcessPath) == false)
            {
                Directory.CreateDirectory(gDocumentOpenTemporaryProcessPath);
                if (Directory.Exists(gDocumentOpenTemporaryProcessPath) == false)
                {
                    string _ErrorMessage = "Unable to create directory. " + gLuraTechTemporaryProcessPath;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
            }
            ConnectToPDFTron();
            gBufferSize = GetMemoryBuferSetting();
            gImageMaxDPI = GetImageDPISetting();
            gUseCompressionForMigration = GetUseCompressionForMigration();

            if ((gloGlobal.gloPMGlobal.MessageBoxCaption).Length>0)
            {
                gloEDocV3Admin.gMessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            }

        }

        public static void SetUser(Int64 UserID)
        {
            gUserID = UserID;
        }

        //sarika 20090206 New Task Implementation as in gloPM
        public static void SetUserName(string UserName)
        {
            gUserName = UserName;
        }
        //---

        public static void GetDefaultPrinterDialog(bool _blnPrintDialog)
        {
            blnUseDefaultPrinterDialog = _blnPrintDialog;
        }


        public static void SetFileSize(Int64 MaxSize, Int64 MinSize)
        {
            if (MaxSize > 0)
            {
                gnDMSV3FileSizeMax = MaxSize;
            }
            if (MinSize > 0)
            {
                gnDMSV3FileSizeMin = MinSize;
            }
        }


        public static void ConnectToPDFTron()
        {
            if (gIsPDFTronConnected == true)
            {
                DisconnectToPDFTron();
            }

            try
            {
                //changed license key 
                pdftron.PDFNet.Initialize("gloStream, Inc.(glostream.com):OEM:gloEMR::W:AMC(20130603):4DE63118A4FA49B931EDEC194A2640E528387DE495B2C9112BD15C49D07AF0FA");
               
            }
            catch (pdftron.Common.PDFNetException ex)
            {
                gIsPDFTronExpired = true;
                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + ex.Message;
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            }

        }

        public static void DisconnectToPDFTron()
        {
            try
            {
                pdftron.PDFNet.Terminate();
                gIsPDFTronConnected = false;
            }
            catch (pdftron.Common.PDFNetException ex)
            {
                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + ex.Message;
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            }
        }
        public static string ReplaceSpecialCharacters(string searchString)
        {
            try
            {
                searchString = searchString.Replace("#", "[#]") + "";
                searchString = searchString.Replace("$", "[$]") + "";
                searchString = searchString.Replace("%", "[%]") + "";
                searchString = searchString.Replace("^", "[^]") + "";
                searchString = searchString.Replace("&", "[&]") + "";

                searchString = searchString.Replace("~", "[~]") + "";
                searchString = searchString.Replace("!", "[!]") + "";
                searchString = searchString.Replace("*", "[*]") + "";
                searchString = searchString.Replace(";", "[;]") + "";
                searchString = searchString.Replace("/", "[/]") + "";
                searchString = searchString.Replace("?", "[?]") + "";
                searchString = searchString.Replace(">", "[>]") + "";
                searchString = searchString.Replace("<", "[<]") + "";
                searchString = searchString.Replace("\\", "[\\]") + "";
                searchString = searchString.Replace("|", "[|]") + "";
                searchString = searchString.Replace("{", "[{]") + "";
                searchString = searchString.Replace("}", "[}]") + "";
                searchString = searchString.Replace("-", "[-]") + "";
                searchString = searchString.Replace("_", "[_]") + "";
                //dhruv
                searchString = searchString.Replace("`", "[`]") + "";
                searchString = searchString.Replace("'", "[']") + "";



                return searchString;
            }
            catch
            {
                return searchString;
            }
        }
        public static string CoverLetterDocumentName
        {
            get
            {

                //string _Path = gTemporaryProcessPath;// +"\\Temp";
                //string _NewDocumentName = "";
                //string _Extension = ".docx";
                //DateTime _dtCurrentDateTime = System.DateTime.Now;

                //int i = 0;
                //_NewDocumentName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt") + _Extension;
                //while (File.Exists(_Path + "\\" + _NewDocumentName) == true)
                //{
                //    i = i + 1;
                //    _NewDocumentName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt") + "-" + i + _Extension;
                //    if (i == 0)
                //    {
                //        string _ErrorMessage = "Cannot able to create directory. " + _NewDocumentName;
                //        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                //        return string.Empty;
                //    }
                //}
                //return _Path + "\\" + _NewDocumentName;
                return gloGlobal.clsFileExtensions.NewDocumentName(gTemporaryProcessPath, ".docx", "MMddyyyyHHmmssffff");
            }
        }
        //Sanjog
        public static string GetProviderDetails()
        {
            string _result = "";

            try
            {
                using (Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gDatabaseConnectionString))
                {
                    if (oDBLayer != null)
                    {
                        if (oDBLayer.Connect(false))
                        {

                            object _ret = oDBLayer.ExecuteScalar_Query("SELECT nProviderID FROM User_Mst WHERE nUSerID=" + gUserID + "");
                            if (_ret != null && _ret.ToString() != "")
                            {
                                //if (Convert.ToInt64(_ret.ToString()) > 0)
                                {
                                    _result = _ret.ToString();
                                }
                            }
                            if (_ret != null)
                            {
                                _ret = null;
                            }
                            if (oDBLayer.Connect(false))
                            {
                                oDBLayer.Disconnect();
                            }
                        }
                    }
                    return _result;
                }

            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);

            }
            return _result;
        }

        public static bool CheckpatientProviderStatus(long nPatientId, long nProviderID)
        {
            bool _result = false;
            try
            {
                using (Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gDatabaseConnectionString))
                {
                    if (oDBLayer != null)
                    {
                        if (oDBLayer.Connect(false))
                        {

                            object _ret = oDBLayer.ExecuteScalar_Query("select DISTINCT ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from Provider_MSt pr INNER JOIn PAtient ON Patient.nProviderID=pr.nProviderID where Patient.npatientID=" + nPatientId + " and Patient.nProviderID=" + nProviderID + "");
                            if (_ret != null && _ret.ToString() != "")
                            {
                                _result = true;
                            }
                            else
                            {
                                _result = false;
                            }
                            if (oDBLayer.Connect(false))
                            {
                                oDBLayer.Disconnect();
                            }
                        }
                        return _result;
                    }
                    return _result;
                }

            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            }
            return false;
        }


        public static string GetProviderStatus(Int64 npatientid)
        {
            string _result = "";
            try
            {
                using (Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gDatabaseConnectionString))
                {
                    if (oDBLayer != null)
                    {
                        if (oDBLayer.Connect(false))
                        {

                            object _ret = oDBLayer.ExecuteScalar_Query("SELECT nProviderID FROM Patient WHERE nPatientid=" + npatientid + "");
                            if (_ret != null && _ret.ToString() != "")
                            {
                                //if (Convert.ToInt64(_ret.ToString()) > 0)
                                {
                                    _result = _ret.ToString();
                                }
                            }
                            if (_ret != null)
                            {
                                _ret = null;
                            }
                            if (oDBLayer.Connect(false))
                            {
                                oDBLayer.Disconnect();
                            }
                        }
                    }
                    if (_result == "")
                    {
                        _result = "0";
                    }
                    return _result;
                }
            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            }
            return _result;
        }

        public static bool GetSignDelegateStatus()
        {
            try
            {
                using (Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gDatabaseConnectionString))
                {
                    if (oDBLayer != null)
                    {
                        if (oDBLayer.Connect(false))
                        {

                            object _ret = oDBLayer.ExecuteScalar_Query("SELECT sSettingsValue from settings where sSettingsName Like '%USESIGNATUREDELEGATES%'");
                            if (_ret != null && _ret.ToString() != "")
                            {
                                if (Convert.ToInt64(_ret.ToString()) > 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (_ret != null)
                            {
                                _ret = null;
                            }
                            if (oDBLayer.Connect(false))
                            {
                                oDBLayer.Disconnect();
                            }
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            }
            return false;
        }


        public static string GetProviderName(Int64 nProviderID)
        {
            string _result = "";
            try
            {
                using (Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gDatabaseConnectionString))
                {
                    if (oDBLayer != null)
                    {
                        if (oDBLayer.Connect(false))
                        {

                            object _ret = oDBLayer.ExecuteScalar_Query("select DISTINCT ISNULL(sFirstName,'') + ' ' + CASE ISNULL(sMiddleName,'') WHEN  '' THEN '' When ISNULL(sMiddleName,'') then  ISNULL(sMiddleName,'') + ' 'END + ISNULL(sLastName,'') AS sProviderName from Provider_MSt  where nProviderID=" + nProviderID + "");
                            if (_ret != null && _ret.ToString() != "")
                            {
                                //if (Convert.ToInt64(_ret.ToString()) > 0)
                                {
                                    _result = _ret.ToString();
                                }
                            }
                            if (_ret != null)
                            {
                                _ret = null;
                            }
                            if (oDBLayer.Connect(false))
                            {
                                oDBLayer.Disconnect();
                            }
                        }
                    }
                    return _result;
                }
            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            }
            return _result;
        }
        public static string GetPatientProviderName(Int64 nPatientID)
        {
            string _result = "";
            try
            {
                using (Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gDatabaseConnectionString))
                {
                    if (oDBLayer != null)
                    {
                        if (oDBLayer.Connect(false))
                        {

                            //object _ret = oDBLayer.ExecuteScalar_Query("select DISTINCT ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from Provider_MSt pr INNER JOIn PAtient ON Patient.nProviderID=pr.nProviderID where Patient.npatientID=" + nPatientID + "");
                            object _ret = oDBLayer.ExecuteScalar_Query("select DISTINCT CASE ISNULL(pr.sPrefix,'') WHEN '' THEN '' WHEN ISNULL(pr.sPrefix,'') THEN ISNULL(pr.sPrefix,'')+' 'END +ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'')+ CASE ISNULL(pr.sSuffix,'') WHEN '' THen '' WHEN ISNULL(pr.sSuffix,'') THEN +' '+ ISNULL(pr.sSuffix,'') END AS sProviderName from Provider_MSt pr INNER JOIn PAtient ON Patient.nProviderID=pr.nProviderID where Patient.npatientID=" + nPatientID + "");
                            if (_ret != null && _ret.ToString() != "")
                            {
                                //if (Convert.ToInt64(_ret.ToString()) > 0)
                                {
                                    _result = _ret.ToString();
                                }
                            }
                            if (_ret != null)
                            {
                                _ret = null;
                            }
                            if (oDBLayer.Connect(false))
                            {
                                oDBLayer.Disconnect();
                            }
                        }
                    }
                    return _result;
                }
            }
            catch (Exception ex)
            {
                string _MessageString = ex.ToString();
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            }
            return _result;
        }
        //Sanjog

    }
}
