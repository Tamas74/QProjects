using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

using System.Windows.Forms;
using Microsoft.Win32;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Reflection;
using System.Security;
using System.Configuration;
using System.Security.Principal;
using gloGlobal;


namespace gloAUSLibrary
{
    public enum enumDownloadType
    {
        Full,
        Daily,
        Instant,
    }
    public enum enumFileType
    {
        XMLFile,
        ZipFile,

    }

    // The Class is mainly Static methods used through out service for different purpose like logging and most general methods.
    public static class clsGeneral
    {
        public static string gstrWebService = "Production";
        public static String gstrDatabaseName = null;
        public static String gstrSQLServerName = null;
        public static String gstrUserId = null;
        public static String gstrPassword = null;

        public static String gstrDirDownload = "";
        public static String constEncryptDecryptKey = "20gloStreamInc08";

        public static Int32 time_INTERVAL = 1;
        public static Boolean blnOpen = false;

        public static Int32 gnDayInterval = 4;
        public static Int32 gnTimeInterval = 1;

        public static string gstrUpdatesPath;

        public static RegistryKey regKey;
        //private static clsEncryption objEncryption;

        public static int nTotalCount = 0;
        public static bool blnIsAuthenticated = false;
        public static List<string> myInt = new List<string>();
        public static bool IsUpdate = false;

        public static string strFtpHostName = string.Empty;
        public static string strFtpUserId = string.Empty;
        public static string strFtpPwd = string.Empty;
        public static string strFtpFolderPath = string.Empty;
        public static string strAdminFolderName = string.Empty;
        public static string strClientFolderName = string.Empty;
        public static string strPathtogloSuiteAdminexe = string.Empty;

        public static string gstrClientMachineName = string.Empty;
        public static bool gIsUpdateAvailable = false;
        public static bool gIsUpdateInstalling = false;

        // Variable for log file
        public static Int32 gblLogMaxSize = 2;
        public static bool gblLogArchive = false;
        public static string gblLogArchivePath = Application.StartupPath;

        public static string strServiceLogPath = Application.StartupPath;

        public static string strDomainName = string.Empty;
        public static string strDomainUsername = string.Empty;
        public static string strDomainPassword = string.Empty;
        public static bool isLoggingEnable = true;

        //Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table instead of Hardcoding
        public static string strgloServiceDatabaseName = string.Empty;
        public static string strgloServicesServerName = string.Empty;
        public static string strgloServicesUserID = string.Empty;
        public static string strgloServicesPassWord = string.Empty;
        public static bool strgloServicesIsSQLAUTHEN ;
        //Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table instead of Hardcoding


        public static String GetClinicCode()
        {
            clsGeneral.UpdateLog("Function for Getting Clinic code");
            string strClinicCode = String.Empty;
            DataTable dtClinicCode = new DataTable();
            DBLayer oDBLayer = new DBLayer(GetConnectionString());
            try
            {
                oDBLayer.Connect(false);
                oDBLayer.Retrive_Query("select sExternalCode from Clinic_MST", out dtClinicCode);

                if (dtClinicCode.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtClinicCode.Rows.Count - 1; i++)
                    {
                        strClinicCode = Convert.ToString(dtClinicCode.Rows[i]["sExternalCode"]);
                    }
                }

                oDBLayer.Disconnect();
                oDBLayer.Dispose();

            }
            catch (Exception Ex)
            {
                clsGeneral.UpdateLog("Error :" + Ex.ToString());
                strClinicCode = "";
                if (oDBLayer != null)
                {
                    oDBLayer.Disconnect();
                    oDBLayer.Dispose();
                }
            }
            finally
            {

                if (oDBLayer != null)
                {
                    oDBLayer.Disconnect();
                    oDBLayer.Dispose();
                }
            }
            return strClinicCode;
        } //GetClinicCode

        public static String GetConnectionString(string strServerName, string strDatabase, string strUserName, string strPassword)
        {
            if (!String.IsNullOrEmpty(strServerName) && !String.IsNullOrEmpty(strDatabase) && !String.IsNullOrEmpty(strUserName) && !String.IsNullOrEmpty(strPassword))
            {
                String strConnectionString;
                // strConnectionString = "SERVER=" & gstrSQLServerName & ";DATABASE=" & gstrDatabaseName & ";Integrated Security=SSPI"
                strConnectionString = "SERVER=" + strServerName + ";DATABASE=" + strDatabase + ";USER id=" + strUserName + ";Password=" + strPassword;
                //  strConnectionString = "SERVER=192.168.123.99;DATABASE=gloEMRUpdates;USER id=sa;password=sasakar";
                // UpdateLog(strConnectionString);
                return strConnectionString;
            }
            else
            {
                return "";
            }
        } //GetConnectionString

        public static String GetConnectionString()
        {
            string strConnectionString = string.Empty;
            try
            {
                //gstrUserId = "sa";
                //gstrPassword = "satesting18";
                //if ((gstrSQLServerName != null) && (gstrDatabaseName != null) && (gstrSQLServerName != "") && (gstrDatabaseName != ""))
                if (!string.IsNullOrEmpty(gstrSQLServerName) && !string.IsNullOrEmpty(gstrDatabaseName))
                {
                    if (string.IsNullOrEmpty(gstrUserId) || string.IsNullOrEmpty(gstrPassword))
                        strConnectionString = "Data Source=" + gstrSQLServerName + ";Initial Catalog=" + gstrDatabaseName + ";Integrated Security=True";
                    else
                        strConnectionString = "Data Source=" + gstrSQLServerName + ";Initial Catalog=" + gstrDatabaseName + ";USER id=" + gstrUserId + ";Password=" + gstrPassword + ";Connection Timeout = 10";

                    //  strConnectionString = "SERVER=192.168.123.99;DATABASE=gloEMRUpdates;USER id=sa;password=sasakar";
                    // UpdateLog(strConnectionString);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                strConnectionString = null;
                UpdateLog("Error while fetching connection string :" + ex.Message.ToString());
            }
            finally
            {

            }
            return strConnectionString;
        } //GetConnectionString

        public static SecureString MakeSecureString(string text)
        {
            SecureString secure = new SecureString();
            foreach (char c in text)
            {
                secure.AppendChar(c);
            }

            return secure;
        } //MakeSecureString

        public static bool RunExe(string strPath, string strCommand)
        {
            bool Success = false;
            string strsetup = string.Empty;
            string strWinpath = null;
            System.Diagnostics.Process oProcess = null;
            bool hasAdministrativeRight = false;
            WindowsPrincipal pricipal = null;
            try
            {
                strWinpath = Environment.GetEnvironmentVariable("WINDIR").ToString();//gets the WIndir path
                oProcess = new Process();
                pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

                UpdateLog("User: " + pricipal.Identity.Name);

                hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

                if (strPath == "")
                {
                    strsetup = ("" + strWinpath + "\\System32\\msiexec.exe");
                }
                else
                {
                    strsetup = strPath;
                }
                if (oProcess != null)
                {
                    oProcess.StartInfo.FileName = strsetup;
                    oProcess.StartInfo.Arguments = strCommand;
                    oProcess.StartInfo.UseShellExecute = false;
                    oProcess.StartInfo.CreateNoWindow = true;

                    try
                    {
                       
                        //Added strgloServiceDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table
                        ////strDomainName = Getsettingsvalue("GLODOMAINNAME", gstrUserId, gstrPassword, gstrSQLServerName, "gloServices");
                        ////strDomainUsername = Getsettingsvalue("GLODOMAINUSERNAME", gstrUserId, gstrPassword, gstrSQLServerName, "gloServices");
                        ////strDomainPassword = Getsettingsvalue("GLODOMAINPASSWORD", gstrUserId, gstrPassword, gstrSQLServerName, "gloServices");

                        strDomainName = Getsettingsvalue("GLODOMAINNAME", strgloServicesUserID, strgloServicesPassWord, strgloServicesServerName, strgloServiceDatabaseName);
                        strDomainUsername = Getsettingsvalue("GLODOMAINUSERNAME", strgloServicesUserID, strgloServicesPassWord, strgloServicesServerName, strgloServiceDatabaseName);
                        strDomainPassword = Getsettingsvalue("GLODOMAINPASSWORD", strgloServicesUserID, strgloServicesPassWord, strgloServicesServerName, strgloServiceDatabaseName);
                        //Added strgloServiceDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table

                        //if (ConfigurationManager.AppSettings["IsLogging"].ToString() == "1")
                        //{
                        //    UpdateLog("Domain name :" + strDomainName);
                        //    UpdateLog("Domain Username :" + strDomainUsername);
                        //    UpdateLog("Domain Password :" + strDomainPassword);
                        //}
                    }
                    catch (Exception ex)
                    {
                        UpdateLog("Error while fetching domain admin user details : " + ex.Message.ToString());
                        strDomainName = null;
                        strDomainUsername = null;
                        strDomainPassword = null;
                    }

                    if (strsetup.Contains("gloSuiteClient.exe") == false)
                    {
                        if (!string.IsNullOrEmpty(strDomainName) && !string.IsNullOrEmpty(strDomainUsername) && !string.IsNullOrEmpty(strDomainPassword) && (strsetup.Contains("xcopy") || strsetup.Contains("msiexec")))
                        {
                            clsEncryption objEnc = new clsEncryption();
                            strDomainPassword = objEnc.DecryptFromBase64String(strDomainPassword, "12345678");

                            oProcess.StartInfo.Domain = strDomainName;
                            oProcess.StartInfo.UserName = strDomainUsername;
                            oProcess.StartInfo.Password = MakeSecureString(strDomainPassword);

                        }
                    }
                    //oProcess.StartInfo.RedirectStandardOutput = true;
                    oProcess.Start();

                    do
                    {
                        oProcess.WaitForExit(1000);
                        oProcess.Refresh();
                        Application.DoEvents();
                    }
                    while (!oProcess.HasExited);
                    Application.DoEvents();
                    if (oProcess.ExitCode == 0 || oProcess.ExitCode == 3010)
                    {
                        Success = true;
                    }
                    clsGeneral.UpdateLog(strCommand);
                    clsGeneral.UpdateLog(oProcess.ExitCode.ToString());
                }

            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in RunExe " + ex.Message.ToString() + "");
            }
            finally
            {
                if (oProcess != null)
                {
                    oProcess.Close();
                    oProcess.Dispose();
                }
            }
            return Success;
        } //RunExe

        private static string CreateDirectory(string path, bool IsDeleteRepace)
        {
            try
            {
                if (IsDeleteRepace)
                {
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Unable to Delete and create Directory" + ex.ToString());
                path = "";
            }
            return path;
        } //CreateDirectory

        public static string GetFileName(enumFileType eFType)
        {
            string strAppPath = Application.StartupPath + "\\Temp\\";
           // string _NewDocumentName = "";
            string _Extension;
            try
            {

                CreateDirectory(strAppPath, false);
                //switch (eFType)
                //{
                //    case enumFileType.XMLFile:
                //        _Extension = ".xml";
                //        break;
                //    case enumFileType.TxtFile:
                //        _Extension = ".txt";
                //        break;
                //    default:
                //        _Extension = ".zip";
                //        break;
                //}

                if (eFType == enumFileType.XMLFile)
                {
                    _Extension = ".xml";
                }
                else
                {
                    _Extension = ".zip";
                }

                //DateTime _dtCurrentDateTime = System.DateTime.Now;

                //int i = 0;
                //_NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + _Extension;
                //while (File.Exists(strAppPath + "\\" + _NewDocumentName) == true)
                //{
                //    i = i + 1;
                //    _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + "-" + i + _Extension;
                //}
                //return strAppPath + _NewDocumentName;
                return gloGlobal.clsFileExtensions.NewDocumentName(strAppPath, _Extension, "MMddyyyyHHmmssffff");

            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error Creating a File Name. " + ex.Message.ToString() + "");
                return "";
            }
        } //GetFileName

        public static Byte[] ConvertFiletoBinary(String strFileName)
        {
            //const int CHUNK_SIZE = 1024 * 8; //8K write buffer.

            if (File.Exists(strFileName))
            {
                FileStream oFile;
                BinaryReader oReader;

                //'Please uncomment the following line of code to read the file, even the file is in use by same or another process 
                // oFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous);

                //'To read the file only when it is not in use by any process 
                using (oFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
                {

                    using (oReader = new BinaryReader(oFile))
                    {

                        Byte[] bytesRead = new Byte[oReader.BaseStream.Length];
                        oReader.Read(bytesRead, 0, bytesRead.Length);
                        oFile.Close();
                        oReader.Close();
                        return bytesRead;

                    }

                }
            }
            else
            {
                return null;
            }
        } //ConvertFiletoBinary

        public static String ConvertBinarytoFile(byte[] cntFromDB, String strFileName)
        {
            if (cntFromDB != null)
            {
                //MemoryStream stream = new MemoryStream(cntFromDB);
                FileStream oFile = new FileStream(strFileName, System.IO.FileMode.Create);
                //stream.WriteTo(oFile);
                oFile.Write(cntFromDB, 0, cntFromDB.Length);
                oFile.Close();
                oFile.Dispose();
                oFile = null;
                return strFileName;

            }
            else
            {
                UpdateLog("Unable to convert to Physical file");
                return "";

            }

        } //ConvertBinarytoFile

        public static bool CreateFolder(string strPath)
        {
            bool success = false;
            try
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                    success = true;
                }
                else
                {
                    success = true;
                }
            }
            catch (System.Exception ex)
            {
                clsGeneral.UpdateLog("Error while creating folder name :" + strPath + ".Error Message :" + ex.Message.ToString());
            }
            return success;
        } //CreateFolder

        public static void UpdateLog(String strLogMessage)
        {
            StreamWriter objFile = null;
            try
            {
                if (isLoggingEnable)
                {
                    strServiceLogPath = Path.Combine(Application.StartupPath, "InstallationLogs");
                    if (!Directory.Exists(strServiceLogPath))
                    {
                        Directory.CreateDirectory(strServiceLogPath);
                    }
                    objFile = new StreamWriter(Path.Combine(strServiceLogPath, "gloClientAUSUpdates.log"), true);
                    objFile.WriteLine(System.DateTime.Now.ToString() + System.DateTime.Now.Millisecond.ToString() + ": " + strLogMessage);
                    objFile.Close();
                }
            }
            catch (Exception)
            {
                if (isLoggingEnable)
                {
                    strServiceLogPath = Path.Combine(Path.GetTempPath(), "InstallationLogs");
                    if (!Directory.Exists(strServiceLogPath))
                    {
                        Directory.CreateDirectory(strServiceLogPath);
                    }

                    objFile = new StreamWriter(Path.Combine(strServiceLogPath, "gloClientAUSUpdates.log"), true);
                    objFile.WriteLine(System.DateTime.Now.ToString() + System.DateTime.Now.Millisecond.ToString() + ": " + strLogMessage);
                    objFile.Close();
                }
            }
            finally
            {
                objFile = null;
            }

        } //UpdateLog

        public static bool GetRegistrySettings(string strRegKey)
        {
            bool success = true;
            DBLayer objDblayer = null;
            string strConnectionString = null;
            clsEncryption objEncryption = null;
            string strPassword = null;

            try
            {
                objEncryption = new clsEncryption();

                //object objServerName = clsRegistry.GetRegistryValue(clsRegistry.gStrSqlServer);
                //if (objServerName != null && objServerName.ToString() != "")
                //{
                //    gstrSQLServerName = objServerName.ToString();
                //}
                //else
                //{
                //    success = false;
                //}

                //object objDatabasename = clsRegistry.GetRegistryValue(clsRegistry.gStrDatabase);
                //if (objDatabasename != null && objDatabasename.ToString() != "")
                //{
                //    gstrDatabaseName = objDatabasename.ToString();
                //}
                //else
                //{
                //    success = false;
                //}

                //object objUserName = clsRegistry.GetRegistryValue(clsRegistry.gStrUserName);
                //if (objUserName != null && objUserName.ToString() != "")
                //{
                //    gstrUserId = objUserName.ToString();
                //}
                //else
                //{
                //    success = false;
                //}

                //object objPassword = clsRegistry.GetRegistryValue(clsRegistry.gStrPassword);
                //if (objPassword != null && objPassword.ToString() != "")
                //{
                strPassword = gstrPassword;
                //    objEncryption = new clsEncryption();
                gstrPassword = objEncryption.DecryptFromBase64String(strPassword, clsGeneral.constEncryptDecryptKey);

                //    if (objEncryption != null)
                //    {
                //        objEncryption = null;
                //    }
                //}
                //else
                //{
                //    success = false;
                //}

                //object objTimeInterval = clsRegistry.GetRegistryValue(clsRegistry.gStrServiceInterval);
                //if (objTimeInterval != null && objTimeInterval.ToString() != "")
                //{
                //    time_INTERVAL = Convert.ToInt32(objTimeInterval);
                //}

                //object objLoggingEnable = clsRegistry.GetRegistryValue(clsRegistry.gStrIsLoggingEnable);
                //if (objLoggingEnable != null && objLoggingEnable.ToString() != "")
                //{
                //    isLoggingEnable = Convert.ToBoolean(objLoggingEnable);
                //}

                strConnectionString = clsGeneral.GetConnectionString();

                if (!string.IsNullOrEmpty(strConnectionString))
                {
                    //clsGeneral.UpdateLog("Connection string :" + strConnectionString);
                    objDblayer = new DBLayer(strConnectionString);

                    if (!objDblayer.CheckConnection())
                    {
                        success = false;
                        clsGeneral.UpdateLog("Error while connection to server : '" + gstrSQLServerName + "' and database :'" + gstrDatabaseName + "'. ");
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                clsGeneral.UpdateLog("Exception in CheckRegistrySettings " + ex.Message.ToString() + "");
            }
            finally
            {
                if (objDblayer != null)
                {
                    objDblayer.Dispose();
                }
                //if (oKey != null)
                //{
                //    oKey.Close();
                //}
            }
            return success;
        } //GetRegistrySettings

        //public static bool GetRegistrySettings(string strRegKey)
        //{
        //    bool success = true;
        //    RegistryKey oKey = Registry.LocalMachine;
        //    DBLayer objDblayer = null;
        //    string strConnectionString = null;
        //    clsEncryption objEncryption = null;
        //    string strPassword = null;
        //    try
        //    {
        //        //check wether 32 bit or 64 bit...
        //        //if it is 32 bit read the regisrty values from the node software\gloemr
        //        //if it is 64 bit read the regisrty values from the node software\wow6432node\gloemr
        //        //SQLServer //Database //SQLUSERNAME       
        //        objEncryption = new clsEncryption();

        //        object objServerName = clsRegistry.GetRegistryValue("SQLServer", strRegKey);
        //        if (objServerName != null && objServerName.ToString() != "")
        //        {
        //            gstrSQLServerName = objServerName.ToString();
        //        }
        //        else
        //        {
        //            success = false;
        //        }

        //        object objDatabasename = clsRegistry.GetRegistryValue("Database", strRegKey);
        //        if (objDatabasename != null && objDatabasename.ToString() != "")
        //        {
        //            gstrDatabaseName = objDatabasename.ToString();
        //        }
        //        else
        //        {
        //            success = false;
        //        }

        //        object objUserName = clsRegistry.GetRegistryValue("SQLUser", strRegKey);
        //        if (objUserName != null && objUserName.ToString() != "")
        //        {
        //            gstrUserId = objUserName.ToString();
        //        }
        //        else
        //        {
        //            success = false;
        //        }

        //        object objPassword = clsRegistry.GetRegistryValue("SQLPassword", strRegKey);
        //        if (objPassword != null && objPassword.ToString() != "")
        //        {
        //            strPassword = objPassword.ToString();
        //            objEncryption = new clsEncryption();
        //            gstrPassword = objEncryption.DecryptFromBase64String(strPassword, clsGeneral.constEncryptDecryptKey);

        //        }
        //        else
        //        {
        //            success = false;
        //        }

        //        object objTimeInterval = clsRegistry.GetRegistryValue("ClientUpdateServiceInterval", strRegKey);
        //        if (objTimeInterval != null && objTimeInterval.ToString() != "")
        //        {
        //            time_INTERVAL = Convert.ToInt32(objTimeInterval);
        //        }

        //        strConnectionString = clsGeneral.GetConnectionString();

        //        if (!string.IsNullOrEmpty(strConnectionString))
        //        {
        //            //clsGeneral.UpdateLog("Connection string :" + strConnectionString);
        //            objDblayer = new DBLayer(strConnectionString);

        //            if (!objDblayer.CheckConnection())
        //            {
        //                success = false;
        //                clsGeneral.UpdateLog("Error while connection to server : '" + gstrSQLServerName + "' and database :'" + gstrDatabaseName + "'. ");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        success = false;
        //        clsGeneral.UpdateLog("Exception in CheckRegistrySettings " + ex.Message.ToString() + "");
        //    }
        //    finally
        //    {
        //        if (objDblayer != null)
        //        {
        //            objDblayer.Dispose();
        //        }
        //        if (oKey != null)
        //        {
        //            oKey.Close();
        //        }
        //    }
        //    return success;
        //}

        public static bool IsSettings()
        {
            bool success = false;
            //bool bIsEmrRegistryPresent = false;
            //bool bIsPMRegistryPresent = false;
            try
            {
                int _type = clsGeneral.CheckMachineStatus();
                gstrClientMachineName = Environment.MachineName;

                if (!String.IsNullOrEmpty(_type.ToString()))
                {
                    if (_type == 0)
                    {
                        clsRegistry.OpenSubKey(clsRegistry.str32EMRKey, clsRegistry.str32EMRKey, true);

                        if (GetRegistrySettings(clsRegistry.str32EMRKey))
                        {
                            success = true;
                        }

                        if (success == false)
                        {
                            clsRegistry.OpenSubKey(clsRegistry.str32PMKey, clsRegistry.str32PMKey, true);

                            if (GetRegistrySettings(clsRegistry.str32PMKey))
                            {
                                success = true;
                            }
                        }
                    }
                    else
                    {
                        clsRegistry.OpenSubKey(clsRegistry.str32EMRKey, clsRegistry.str64EMRKey, true);

                        if (GetRegistrySettings(clsRegistry.str32EMRKey))
                        {
                            success = true;
                        }

                        if (success == false)
                        {
                            clsRegistry.OpenSubKey(clsRegistry.str32PMKey, clsRegistry.str64EMRKey, true);

                            if (GetRegistrySettings(clsRegistry.str32PMKey))
                            {
                                success = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in IsSettings " + ex.Message.ToString() + "");
            }
            finally
            {
                clsRegistry.CloseRegistryKey();
            }
            return success;
        } //IsSettings

        //public static bool IsSettings()
        //{
        //    bool success = false;
        //    try
        //    {
        //        int _type = clsGeneral.CheckMachineStatus();
        //        gstrClientMachineName = Environment.MachineName;
        //        if (!String.IsNullOrEmpty(_type.ToString()))
        //        {
        //            if (_type == 0)
        //            {
        //                if (clsRegistry.CheckRegistryExists(clsRegistry.str32EMRKey) || clsRegistry.CheckRegistryExists(clsRegistry.str32PMKey))
        //                {
        //                    if (GetRegistrySettings(clsRegistry.str32EMRKey) || GetRegistrySettings(clsRegistry.str32PMKey))
        //                    {
        //                        success = true;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (clsRegistry.CheckRegistryExists(clsRegistry.str64EMRKey) || clsRegistry.CheckRegistryExists(clsRegistry.str64PMKey))
        //                {
        //                    if (GetRegistrySettings(clsRegistry.str64EMRKey) || GetRegistrySettings(clsRegistry.str64PMKey))
        //                    {
        //                        success = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGeneral.UpdateLog("Exception in IsSettings " + ex.Message.ToString() + "");
        //    }
        //    finally
        //    {

        //    }
        //    return success;
        //}

        public static void GetdatabaseList()
        {
            try
            {
                string strQuery = "select UPPER(ssettingsvalue) from settings where UPPER(ssettingsname)='AUSDBSETTINGS'";
                string strDBnames = clsGeneral.GetScalar(strQuery, clsGeneral.gstrSQLServerName, clsGeneral.gstrDatabaseName, clsGeneral.gstrUserId, clsGeneral.gstrPassword);
                if (!String.IsNullOrEmpty(strDBnames))
                {
                    if (clsGeneral.myInt.Count > 0)
                    {
                        clsGeneral.myInt.Clear();
                    }
                    string[] strChkDBNames = new string[40];
                    char[] textdelimiter = { '|' };
                    strChkDBNames = strDBnames.Split(textdelimiter);
                    for (int i = 0; i < strChkDBNames.Length; i++)
                    {
                        if (strChkDBNames[i].ToString() != null)
                        {
                            clsGeneral.myInt.Add(strChkDBNames[i].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in GetdatabaseList " + ex.Message.ToString() + "");
            }
        } //GetdatabaseList

        public static void DeleteXMLFile(string strFileAbsoluteName)
        {
            try
            {
                File.Delete(strFileAbsoluteName);
                //To check if file is deleted UpdateLog("Deleted Response File successfully named : " + strFileAbsoluteName);
            }
            catch (Exception ex)
            {
                UpdateLog("Unable to Delete Response File named : " + strFileAbsoluteName + "  " + ex.Message);
            }
        } //DeleteXMLFile

        public static bool CheckDir(string strPath, string _messageBoxCaption)
        {
            bool _success = false;
            try
            {
                //_success = new DirectoryInfo(strPath).FullName == strPath;
                if (strPath.StartsWith("\\"))
                {
                    string[] folders = Directory.GetDirectories(strPath);
                    if (folders.Length == 0)
                    {
                        //if folder is empty then try to create folder and delete that folder..
                        if (clsGeneral.CreateFolder(strPath + "\\Test"))
                        {
                            //if created successfully then delete the folder and return success
                            Directory.Delete(strPath + "\\Test");
                            _success = true;
                        }
                    }
                    foreach (string folder in folders)//looping through folders
                    {
                        string name = Path.GetFileName(folder);
                        string dest = Path.Combine(strPath, name);
                        if (dest.Length > 0)
                        {
                            _success = true;
                        }
                    }
                }
                else
                {
                    if (System.IO.Directory.Exists(strPath))
                    {
                        _success = true;
                    }
                }
            }
            catch (Exception)
            {
                clsGeneral.UpdateLog("The UNC path should be of the form \\\\server\\share.");
            }
            return _success;
        } //CheckDir

        public static string appendMachinename(string serverpath)
        {
            string strNewlocation = string.Empty;
            try
            {
                string strMachinename = Environment.MachineName.ToString();
                string[] strSepeartor = new string[40];

                char[] textdelimiter = { '\\' };
                strSepeartor = serverpath.Split(textdelimiter);
                if (strSepeartor.Length > 0)
                {
                    //clsGeneral.UpdateLog("Product new location length " + strSepeartor.Length + "");
                    string strPath = strSepeartor[0].ToString();
                    strPath = strPath.Replace(strPath, "\\" + strMachinename);
                    for (int i = 0; i < strSepeartor.Length; i++)
                    {
                        if (i == 0)
                        {
                            strNewlocation = strSepeartor[i].ToString();
                            strNewlocation = strNewlocation.Replace(strNewlocation, "\\\\" + strMachinename);
                        }
                        else
                        {
                            strNewlocation += "\\" + strSepeartor[i].ToString();
                        }
                    }
                }

                else
                {
                    clsGeneral.UpdateLog("unable to append the machine name");
                }
                clsGeneral.UpdateLog("After appending " + strNewlocation + "");
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in appendMachinename() " + ex.Message.ToString() + "");
            }
            //clsGeneral.UpdateLog("strNewLocation " + strNewlocation + "");
            return strNewlocation;


        } //appendMachinename

        public static bool IsSetingsValueExists(string settingsname, string strSqlServer, string strDatabase, string strUserName, string strPassword)
        {
            bool success = false;
            try
            {
                string strCheckValue = string.Empty;
                string strQuery = "select UPPER(ssettingsname) from settings where UPPER(ssettingsname)='" + settingsname + "'";
                strCheckValue = GetScalar(strQuery, strSqlServer, strDatabase, strUserName, strPassword);
                if (!String.IsNullOrEmpty(strCheckValue))
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in IsSetingsValueExists " + ex.Message.ToString() + "");
            }
            finally
            {
            }
            return success;
        } //IsSetingsValueExists

        public static string GetText(string strPathToTextfile)
        {
            StreamReader objStreamReader = null;
            string line = string.Empty;
            string strOutput = string.Empty;
            // bool _success = false;
            try
            {
                string FILENAME = strPathToTextfile;
                //int count = 0;
                string[] arInfo = null;

                //Get a StreamReader class that can be used to read the file
                objStreamReader = System.IO.File.OpenText(FILENAME);
                line = objStreamReader.ReadToEnd();
                char[] textdelimiter = { '\n' };
                arInfo = line.Split(textdelimiter);
                for (int i = 2; i < arInfo.Length - 5; i++)
                {
                    strOutput += arInfo[i];
                }
                //while ((line = objStreamReader.ReadLine()) != null)
                //{
                //    if (line != "")
                //    {
                //        char[] textdelimiter = { ',' };
                //        arInfo = line.Split(textdelimiter);
                //    }
                //}
            }
            catch (Exception)
            {
            }
            finally
            {
                objStreamReader.Close();
            }
            return strOutput;
        } //GetText

        public static bool ReadLogFile(string strFilepath)
        {
            StreamReader objStreamReader = null;
            bool _success = false;
            try
            {
                string FILENAME = strFilepath;
                int count = 0;
                string[] arInfo;
                string line;
                //Get a StreamReader class that can be used to read the file
                objStreamReader = File.OpenText(FILENAME);
                while ((line = objStreamReader.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        // contents = line.Replace(("").PadRight(tabSize, ' '), "\t");
                        // contents = line;
                        // define which character is seperating fields
                        char[] textdelimiter = { ',' };
                        arInfo = line.Split(textdelimiter);
                        if (arInfo.Length > 4)
                        {
                            _success = false;
                            count = 1;
                            break;

                        }
                        else
                        {
                            if (count == 0)
                            {
                                _success = true;
                            }
                            else
                            {
                                _success = false;
                            }
                        }
                    }
                    else
                    {
                        _success = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                objStreamReader.Close();
            }
            Application.DoEvents();
            return _success;
        } //ReadLogFile

        public static int CheckMachineStatus()
        {
            int _type = 0;
            string strProcArchi = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            bool Proc64running32 = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            //MessageBox.Show(strProcArchi);
            if ((strProcArchi.IndexOf("64") > 0) || (!Proc64running32))
            {

                _type = 1; //64 bit machine
                //  MessageBox.Show("64bit machine");
            }
            else
            {
                _type = 0; //32 bit machine
                //MessageBox.Show("32bit machine");
            }
            return _type;
        } //CheckMachineStatus

        public static void WriteToTextFile(string pathtofile, string text)
        {
            TextWriter tw = null;
            try
            {
                tw = new StreamWriter(pathtofile, true);
                // write a line of text (present date/time) to the file  
                tw.WriteLine(DateTime.Now);
                // write the rest of the text lines  
                tw.Write(text);
                // close the stream   
                tw.Close();

            }
            catch (Exception)
            {
            }
            finally
            {
                if (tw != null)
                {
                    tw.Dispose();
                }

            }
        } //WriteToTextFile

        public static void InsertUpdateInstalled(string _value, string settingsname, string strServerName, string strDatabasename, string strUserName, string strPassword)
        {
            try
            {
                string strQuery = string.Empty;
                if (IsSetingsValueExists(settingsname, strServerName, strDatabasename, strUserName, strPassword)) //true update  the Demo application 
                {
                    strQuery = "update settings set ssettingsvalue='" + _value + "',nuserid='0' where UPPER(ssettingsname)='" + settingsname + "'";
                    if (clsGeneral.ExecuteQuery(strQuery, strServerName, strDatabasename, strUserName, strPassword))
                    {
                    }
                }
                else
                {
                    //Select nClinicID from Clinic_MST
                    strQuery = "Select nClinicID from Clinic_MST";
                    string strClinicId = GetScalar(strQuery, strServerName, strDatabasename, strUserName, strPassword);
                    if (!String.IsNullOrEmpty(strClinicId))
                    {
                    }
                    else
                    {
                        strClinicId = "1";
                    }
                    strQuery = "Insert into settings  " +
                             "(nSettingsID, sSettingsName, sSettingsValue, nClinicID, nUserID, nUserClinicFlag) " +
                             "select isnull(max(nSettingsID),0)+1,'" + settingsname + "','" + _value + "','" + strClinicId + "',0,null from settings ";
                    if (clsGeneral.ExecuteQuery(strQuery, strServerName, strDatabasename, strUserName, strPassword))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in InsertUpdateInstalled " + ex.Message.ToString() + "");
            }
        } //InsertUpdateInstalled

        public static bool ExecuteQuery(string strQuery, string strServerName, string strDatabasename, string strUserName, string strPassword)
        {

            bool _success = false;
            string strConnection = GetConnectionString(strServerName, strDatabasename, strUserName, strPassword);
            if (!String.IsNullOrEmpty(strConnection))
            {
                SqlConnection Mycon = new SqlConnection(strConnection);
                SqlCommand Mycmd = new SqlCommand(strQuery, Mycon);
                try
                {
                    if (Mycon != null && Mycmd != null)
                    {
                        Mycon.Open();//open the connection
                        Mycmd.CommandTimeout = 0;
                        if (Mycmd.ExecuteNonQuery() >= 0)
                        {
                            _success = true;
                        }

                    }
                }
                catch (Exception ex)
                {
                    clsGeneral.UpdateLog("Exception in clsGeneral.ExecuteQuery " + ex.Message.ToString() + "");
                }
                finally
                {
                    if (Mycon != null && Mycon.State == ConnectionState.Open)
                    {
                        Mycon.Close();
                        Mycon.Dispose();

                    }

                    if (Mycmd != null)
                    {
                        Mycmd.Parameters.Clear();
                        Mycmd.Dispose();
                        Mycmd = null;
                    }
                }
            }
            return _success;
        } //ExecuteQuery

        public static string GetScalar(string strQuery, string strServerName, string strDatabaseName, string strUsername, string strPassword)
        {
            SqlConnection Mycon = null;
            SqlCommand Mycmd = null;
            try
            {
                string strValue = string.Empty;
                string strConnection = GetConnectionString(strServerName, strDatabaseName, strUsername, strPassword);
                Mycon = new SqlConnection(strConnection);
                Mycmd = new SqlCommand(strQuery, Mycon);
                Mycon.Open();
                if (Mycmd.ExecuteScalar() != DBNull.Value)
                {
                    strValue = Mycmd.ExecuteScalar().ToString();
                    return strValue;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (Mycon != null && Mycon.State == ConnectionState.Open)
                {
                    Mycon.Close();
                }
                if (Mycmd != null)
                {
                    Mycmd.Parameters.Clear();
                    Mycmd.Dispose();
                    Mycmd = null;
                }
            }
        } //GetScalar

        public static string GetScalar(string strQuery, string strConnection)
        {
            SqlConnection Mycon = null;
            SqlCommand Mycmd = null;
            try
            {
                string strValue = string.Empty;
                Mycon = new SqlConnection(strConnection);
                Mycmd = new SqlCommand(strQuery, Mycon);
                Mycon.Open();
                if (Mycmd.ExecuteScalar() != DBNull.Value)
                {
                    strValue = Mycmd.ExecuteScalar().ToString();
                    return strValue;
                }
                else
                    return null;


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (Mycon != null && Mycon.State == ConnectionState.Open)
                {
                    Mycon.Close();
                }
                if (Mycmd != null)
                {
                    Mycmd.Parameters.Clear();
                    Mycmd.Dispose();
                    Mycmd = null;
                }
            }
        } //GetScalar

        public static string TrimPathVariables(string strPath)
        {
            try
            {
                while (strPath.EndsWith("\\"))
                { strPath = strPath.TrimEnd('\\'); }
            }
            catch (Exception ex)
            {
                UpdateLog("Error while in function TrimPathVariables :" + ex.Message.ToString());
            }
            return strPath;
        } //TrimPathVariables

        public static string GetAssemblyVersion()
        {
            // FileVersionInfo.Comments = AssemblyDescription

            // FileVersionInfo.CompanyName = AssemblyCompany

            // FileVersionInfo.FileDescription = AssemblyTitle

            // FileVersionInfo.FileVersion = AssemblyFileVersion

            // FileVersionInfo.LegalCopyright = AssemblyCopyright

            // FileVersionInfo.LegalTrademarks = AssemblyTrademark

            // FileVersionInfo.ProductName = AssemblyProduct

            // FileVersionInfo.ProductVersion = AssemblyInformationalVersion

            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            return fvi.ProductVersion.ToString();//AssemblyInformationalVersion
            //return fvi.FileVersion.ToString();
        } //GetAssemblyVersion

        public static bool RunScript(string strPath, string strServerName, string strDatabase, string strOutputpath, bool IsSQlAuthentication, string UserName, string Password)
        {
            bool _success = false;
            string strCommand = string.Empty;
            try
            {
                string strpath = "sqlcmd";
                if (IsSQlAuthentication) //sql authentication
                {
                    if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password)) //check for credentials
                    {
                        strCommand = "-U " + UserName + " -P " + Password + " -W -S " + strServerName + " -d " + strDatabase + " -i\"" + strPath + "\"  -o \"" + strOutputpath + "\"";
                    }
                }
                else //windows authentication execute scripts with windows authentication
                {
                    strCommand = "-E -W -S " + strServerName + " -d " + strDatabase + " -i\"" + strPath + "\"  -o \"" + strOutputpath + "\"";
                }
                if (RunExe(strpath, strCommand))
                {
                    _success = true;
                    clsGeneral.UpdateLog("Executed Script successfully on Server : " + strServerName + " Database : " + strDatabase + "");
                    Application.DoEvents();
                }
                else
                {

                    clsGeneral.UpdateLog("Execution of script failed on Server : " + strServerName + " Database : " + strDatabase + "");

                }

            }
            catch (Exception ex)
            {

                clsGeneral.UpdateLog("Exception while Executing script  failed on Server : " + strServerName + " Database : " + strDatabase + "");
                clsGeneral.UpdateLog("Exception in RunScript " + ex.Message.ToString() + "");
            }
            finally
            {

            }
            Application.DoEvents();
            return _success;
        } //RunScript

        public static DataTable GetDatabases(string serverName, string userName, string password)
        {

            string strConnection = string.Empty;
            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
            {
                strConnection = "Data Source='" + serverName + "';User id='" + userName + "';password='" + password + "' ";
            }
            else
            {
                strConnection = "Data Source='" + serverName + "'; Integrated Security=True";
            }
            SqlConnection Mycon = new SqlConnection(strConnection);

            Mycon.Open();
            DataTable tblDatabases = Mycon.GetSchema("Databases");
            Mycon.Close();

            return tblDatabases;
        } //GetDatabases

        public static bool CheckExistingDatabase(string serverName, string DataBaseName, string userName, string password)
        {

            string strConnection = string.Empty;
            bool _success = false;
            SqlConnection Mycon = null;
            string strQuery = null;
            DataTable dt = null;
            SqlDataAdapter da = null;
            try
            {
                clsGeneral.UpdateLog("Checking Whether database " + DataBaseName + " is exists ");
                dt = new DataTable();
                strQuery = "select name from sys.databases where Name=N'" + DataBaseName.ToString() + "' ";

                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    strConnection = "Data Source='" + serverName + "';User id='" + userName + "';password='" + password + "' ";
                }
                else
                {
                    strConnection = "Data Source='" + serverName + "'; Integrated Security=True;";
                }

                using (Mycon = new SqlConnection(strConnection))
                {
                    Mycon.Open();
                    // Mycmd = new SqlCommand(strQuery, Mycon);
                    using (da = new SqlDataAdapter(strQuery, Mycon))
                    {
                        da.Fill(dt);
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            _success = true;
                            // clsGeneral.UpdateLog("Database "+strConnection+" Exists");
                        }
                    }
                    Mycon.Close();
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception CheckExistingDatabase " + ex.Message.ToString() + "");
            }
            finally
            {
                if (da != null)
                {
                    da.Dispose();
                    da = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (Mycon != null)
                {
                    if (Mycon.State == ConnectionState.Open)
                        Mycon.Close();

                    Mycon.Dispose();
                    Mycon = null;
                }
            }
            return _success;
        } //CheckExistingDatabase

        public static DataTable GetDataTable(string strQuery, string strServerName, string Databasename, string strUserName, string strPassword)
        {
            string strConnectionstring = null;
            SqlConnection Mycon = null;
            DataTable dt = null;
            SqlDataAdapter da = null;
            try
            {
                strConnectionstring = GetConnectionString(strServerName, Databasename, strUserName, strPassword);
                dt = new DataTable();
                using (Mycon = new SqlConnection(strConnectionstring))
                {
                    Mycon.Open();
                    using (da = new SqlDataAdapter(strQuery, Mycon))
                    {
                        da.Fill(dt);
                    }
                    Mycon.Close();
                }
            }
            catch //(Exception ex)
            {

            }
            finally
            {
                if (da != null)
                {
                    da.Dispose();
                    da = null;
                }
                if (Mycon != null)
                {
                    if (Mycon.State == ConnectionState.Open)
                        Mycon.Close();

                    Mycon.Dispose();
                    Mycon = null;
                }
            }
            return dt;
        } //GetDataTable

        public static DataTable GetDataTable(string strQuery, string strConnection)
        {
            SqlConnection Mycon = null;
            DataTable dt = null;
            SqlDataAdapter da = null;
            try
            {
                dt = new DataTable();
                using (Mycon = new SqlConnection(strConnection))
                {
                    Mycon.Open();
                    using (da = new SqlDataAdapter(strQuery, Mycon))
                    {
                        da.Fill(dt);
                    }
                    Mycon.Close();
                }
            }
            catch (Exception)
            {
            }
            finally
            {

            }
            return dt;
        } //GetDataTable

        //public static void SetClientUpdateFlagValue(string strValue)
        //{
        //    try
        //    {
        //        if (clsRegistry.OpenSubKey(clsRegistry.str32EMRKey, true, ""))
        //        {
        //            clsRegistry.SetRegistryValue(clsRegistry.gStrClientUpdateAvailable, strValue);
        //            clsGeneral.UpdateLog("Flag set client machine update notification.");
        //        }
        //        else if (clsRegistry.OpenSubKey(clsRegistry.str32PMKey, true, ""))
        //        {
        //            clsRegistry.SetRegistryValue(clsRegistry.gStrClientUpdateAvailable, strValue);
        //            clsGeneral.UpdateLog("Flag set client machine update notification.");
        //        }
        //        //if (clsGeneral.CheckMachineStatus() == 0)
        //        //{
        //        //    if (clsRegistry.CheckRegistryExists(clsRegistry.str32EMRKey))
        //        //    {
        //        //        clsRegistry.WriteValue(oKey, clsRegistry.str32EMRKey, strKeyName, strKeyValue);
        //        //        //clsGeneral.UpdateLog("Flag set client machine update notification.");
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    if (clsRegistry.CheckRegistryExists(clsRegistry.str64EMRKey))
        //        //    {
        //        //        clsRegistry.WriteValue(oKey, clsRegistry.str64EMRKey, strKeyName, strKeyValue);
        //        //        //clsGeneral.UpdateLog("Flag set client machine update notification.");
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGeneral.UpdateLog("Error while creating the registry key for enabling client update: " + ex.ToString());
        //        //   clsGeneral.blnIsBallonTipClosed = false;
        //    }
        //    finally
        //    {
        //        //if (oKey != null)
        //        //{
        //        //    oKey.Close();
        //        //    oKey = null;
        //        //}
        //    }
        //}

        //public static string GetClientUpdateFlagValue(string strKeyName)
        //{
        //    //RegistryKey oKey = Registry.LocalMachine;
        //    object strValue = null;
        //    string retValue = "FALSE";
        //    try
        //    {
        //        if (clsRegistry.OpenSubKey(clsRegistry.str32EMRKey, true, ""))
        //        {
        //            strValue = clsRegistry.GetRegistryValue(clsRegistry.gStrClientUpdateAvailable);
        //            clsGeneral.UpdateLog("client machine update Flag :" + strValue);
        //        }
        //        else if (clsRegistry.OpenSubKey(clsRegistry.str32PMKey, true, ""))
        //        {
        //            strValue = clsRegistry.GetRegistryValue(clsRegistry.gStrClientUpdateAvailable);
        //            clsGeneral.UpdateLog("client machine update Falg :" + strValue);
        //        }
        //        //if (clsGeneral.CheckMachineStatus() == 0)
        //        //{
        //        //    if (clsRegistry.CheckRegistryExists(clsRegistry.str32EMRKey))
        //        //    {
        //        //        strValue = clsRegistry.GetRegistryValue(strKeyName, clsRegistry.str32EMRKey);
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    if (clsRegistry.CheckRegistryExists(clsRegistry.str64EMRKey))
        //        //    {
        //        //        strValue = clsRegistry.GetRegistryValue(strKeyName, clsRegistry.str64EMRKey);
        //        //    }
        //        //}
        //        if (strValue != null)
        //            retValue = Convert.ToString(strValue).ToUpper();
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGeneral.UpdateLog("Error while retrive registry value for update ballon:" + ex.ToString());
        //    }
        //    finally
        //    {
        //        //if (oKey != null)
        //        //{
        //        //    oKey.Close();
        //        //    oKey = null;
        //        //}
        //    }
        //    return retValue;
        //}

        public static void GetLogSettings()
        {
            //Region for archive log file
            string FilePath = null;
            string temp = null;
            System.IO.FileInfo oFileInfo = null;
            try
            {
                temp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                if (string.IsNullOrEmpty(clsGeneral.gblLogArchivePath) == true)
                    FilePath = System.Windows.Forms.Application.StartupPath + "\\gloClientAUSUpdates.log";
                else
                    FilePath = clsGeneral.gblLogArchivePath + "\\gloClientAUSUpdates.log";

                oFileInfo = new System.IO.FileInfo(FilePath);

                if (oFileInfo.Exists)
                {
                    if (isLoggingEnable)
                    {
                        if (!string.IsNullOrEmpty(clsGeneral.gblLogMaxSize.ToString()))
                        {
                            if (oFileInfo.Length > 1048576 * (Int32)clsGeneral.gblLogMaxSize)
                            {
                                if (clsGeneral.gblLogArchive == true)
                                {
                                    //archive log file on Path provided in setting if it's exceeds limit set in Log setting
                                    if (!string.IsNullOrEmpty(clsGeneral.gblLogArchivePath))
                                    {
                                        FilePath = clsGeneral.gblLogArchivePath + "\\" + temp + ".Log";
                                        oFileInfo.MoveTo(FilePath);
                                    }
                                    else
                                    {
                                        FilePath = System.Windows.Forms.Application.StartupPath + "\\" + temp + ".Log";
                                        oFileInfo.MoveTo(FilePath);
                                    }
                                }
                                else
                                {
                                    //Delete log file if it's exceeds limit set in Log setting
                                    oFileInfo.Delete();
                                }
                            }
                        }
                    }
                    oFileInfo = null;
                }
            }
            catch (Exception ex)
            {
                UpdateLog("Error while Log maintenance ****" + ex.ToString());
            }
            finally
            {
                if ((oFileInfo != null))
                {
                    oFileInfo = null;
                }
            }
        } //GetLogSettings

        public static string Getsettingsvalue(string settingsname, string strUserName, string strPassword, string strSqlServer, string strDatabase)
        {
            string strDemoValue = null;
            DataTable dt = new DataTable();
            string strQuery = null;
            try
            {
                //For Resolving Batch Eligibilty Problem
                strQuery = "select ssettingsvalue from settings where UPPER(ssettingsname)='" + settingsname.ToUpper() + "'";

                dt = GetDataTable(strQuery, strSqlServer, strDatabase, strUserName, strPassword);
                if (dt != null && dt.Rows.Count == 1)
                {
                    strDemoValue = dt.Rows[0]["ssettingsvalue"].ToString();
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while fetching setting from setting name '" + settingsname + "' .Error Message :" + ex.Message.ToString());
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }

            }
            return strDemoValue;
        } //Getsettingsvalue


        public static DataTable GetServicesDBCredentials(string gloSuiteconnectionstring)
        {
            DBLayer oDBLayer = new DBLayer(gloSuiteconnectionstring);
            DataSet dsData = new DataSet();

            try
            {
                oDBLayer.Connect(false);
                string strSQL = " select sSettingsName,sSettingsValue from Settings where sSettingsName in ('SERVICESDATABASENAME','SERVICESSERVERNAME','SERVICESUSERID','SERVICESPASSWORD','SERVICESAUTHEN') order by sSettingsName";
                oDBLayer.Retrive_Query(strSQL, out dsData);
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 2)
                    {
                        return dsData.Tables[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Database : " + gstrDatabaseName + Environment.NewLine + ex.ToString(),"", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGeneral.UpdateLog("Error in Database : " + gloSuiteconnectionstring + Environment.NewLine + ex.ToString());
                return null;
            }
            finally
            {
                if ((dsData != null))
                {
                    dsData.Dispose();
                    dsData = null;
                }

                if ((oDBLayer != null))
                {
                    oDBLayer.Disconnect();
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

            }
        }

  
        //public static bool GetServiceSetting(string strRegKey)
        //{
        //    bool success = true;
        //    DBLayer objDblayer = null;
        //    string strConnectionString = null;
        //    clsEncryption objEncryption = null;
        //    string strPassword = null;
        //    try
        //    {
        //        //check wether 32 bit or 64 bit...
        //        //if it is 32 bit read the regisrty values from the node software\gloemr
        //        //if it is 64 bit read the regisrty values from the node software\wow6432node\gloemr
        //        //SQLServer //Database //SQLUSERNAME
        //        objEncryption = new clsEncryption();
        //        object objServerName = clsRegistry.GetRegistryValue("SQLServer", strRegKey);
        //        if (objServerName != null && objServerName.ToString() != "")
        //        {
        //            gstrSQLServerName = objServerName.ToString();
        //        }
        //        else
        //        {
        //            success = false;
        //        }
        //        object objDatabasename = clsRegistry.GetRegistryValue("Database", strRegKey);
        //        if (objDatabasename != null && objDatabasename.ToString() != "")
        //        {
        //            gstrDatabaseName = objDatabasename.ToString();
        //        }
        //        else
        //        {
        //            success = false;
        //        }
        //        object objUserName = clsRegistry.GetRegistryValue("SQLUser", strRegKey);
        //        if (objUserName != null && objUserName.ToString() != "")
        //        {
        //            gstrUserId = objUserName.ToString();
        //        }
        //        else
        //        {
        //            success = false;
        //        }

        //        object objPassword = clsRegistry.GetRegistryValue("SQLPassword", strRegKey);
        //        if (objPassword != null && objPassword.ToString() != "")
        //        {
        //            strPassword = objPassword.ToString();
        //            objEncryption = new clsEncryption();
        //            gstrPassword = objEncryption.DecryptFromBase64String(strPassword, clsGeneral.constEncryptDecryptKey);
        //        }
        //        else
        //        {
        //            success = false;
        //        }

        //        object objTimeInterval = clsRegistry.GetRegistryValue("ClientUpdateServiceInterval", strRegKey);
        //        if (objTimeInterval != null && objTimeInterval.ToString() != "")
        //        {
        //            time_INTERVAL = Convert.ToInt32(objTimeInterval);
        //        }

        //        strConnectionString = clsGeneral.GetConnectionString();

        //        if (!string.IsNullOrEmpty(strConnectionString))
        //        {
        //            //clsGeneral.UpdateLog("Connection string :" + strConnectionString);
        //            objDblayer = new DBLayer(strConnectionString);

        //            if (!objDblayer.CheckConnection())
        //            {
        //                success = false;
        //                clsGeneral.UpdateLog("Error while connection to server : '" + gstrSQLServerName + "' and database :'" + gstrDatabaseName + "'. ");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        success = false;
        //        clsGeneral.UpdateLog("Exception in GetServiceSetting Function: " + ex.Message.ToString() + "");
        //    }
        //    finally
        //    {
        //        if (objDblayer != null)
        //        {
        //            objDblayer.Dispose();
        //        }
        //    }
        //    return success;
        //} //GetServiceSetting

        //public static bool SetClientUpdateFlag(string strValue)
        //{
        //    bool retValue = false;

        //    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    AppSettingsSection configSection = config.AppSettings;

        //    try
        //    {

        //        if (configSection != null)
        //        {
        //            if (configSection.IsReadOnly() == false && configSection.SectionInformation.IsLocked == false)
        //            {
        //                config.AppSettings.Settings["IsClientUpdateAvailable"].Value = strValue;
        //                ConfigurationManager.RefreshSection("appSettings");
        //                config.Save(ConfigurationSaveMode.Modified);
        //                retValue = true;
        //            }
        //        }

        //        //config.AppSettings.Settings["IsClientUpdateAvailable"].Value = strValue;
        //        //ConfigurationManager.RefreshSection("appSettings");
        //        //config.Save(ConfigurationSaveMode.Modified);

        //    }
        //    catch (Exception ex)
        //    {
        //        retValue = false;
        //        clsGeneral.UpdateLog("Error while setting client update flag. Error Message :" + ex.Message.ToString());
        //    }
        //    return retValue;
        //} //SetClientUpdateFlag

        //public static string GetClientUpdateFlag()
        //{
        //    string retValue = null;

        //    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    AppSettingsSection configSection = config.AppSettings;
        //    try
        //    {
        //        if (configSection != null)
        //        {
        //            if (configSection.IsReadOnly() == false && configSection.SectionInformation.IsLocked == false)
        //            {
        //                ConfigurationManager.RefreshSection("appSettings");
        //                retValue = config.AppSettings.Settings["IsClientUpdateAvailable"].Value;
        //            }
        //        }

        //        //config.AppSettings.Settings["IsClientUpdateAvailable"].Value = strValue;
        //        //ConfigurationManager.RefreshSection("appSettings");
        //        //config.Save(ConfigurationSaveMode.Modified);

        //    }
        //    catch (Exception ex)
        //    {
        //        retValue = null;
        //        clsGeneral.UpdateLog("Error while setting client update flag. Error Message :" + ex.Message.ToString());
        //    }
        //    return retValue;
        //} //SetClientUpdateFlag
    } //clsGeneral
} //gloClientAusWinService
