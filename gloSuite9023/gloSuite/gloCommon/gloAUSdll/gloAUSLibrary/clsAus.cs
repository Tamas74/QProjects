using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using IWshRuntimeLibrary;


namespace gloAUSLibrary
{
    public class clsAus
    {
        #region Global Variables
        public string strEmrInstallationpath = string.Empty;
        public string _messageBoxCaption = "gloUpdate Manager";
        public string strPMInstallationpath = string.Empty;
        public static string strConnectionstring = string.Empty;
        public string strExeversion = string.Empty;
        public string strMachineName = Environment.MachineName.ToString();
        public string strApppath = Application.StartupPath.ToString();
        public string LogFilePathLocation = string.Empty;
        public string strServerPath = string.Empty;
        public static string strTypeofProduct = string.Empty;
        public static string strUpdateLocation = string.Empty;

        public static string strAdminProductType = string.Empty;
        public static string strAdminupdateMSI = string.Empty;
        public static string strAdminupdatePath = string.Empty;
        #endregion Global Variables

        #region Database Operations
        public DataTable getUpdateDetails(string strConnection, string strDBName)
        {
            DataTable dt = new DataTable();

            try
            {
                //string strQuery = "SELECT   ClientSettings_MST.blnIsUpdated, ClientSettings_MST.nUpdateId, " +
                //                 "ClientSettings_MST.sProductCode, ClinicUpdates_Settings.sUpdateLocation, " +
                //                 "ClinicUpdates_Settings.sProductName, ClinicUpdates_Settings.sProductType " +
                //                 "FROM         ClientSettings_MST INNER JOIN " +
                //                 "ClinicUpdates_Settings ON ClientSettings_MST.nUpdateId = ClinicUpdates_Settings.nUpdateID " +
                //                 "AND " +
                //                 "ClientSettings_MST.sProductCode = ClinicUpdates_Settings.sProductCode " +
                //                 "where ClientSettings_MST.sMachineName='" + strMachineName + "' ";
                string strQuery = "Select  " +
                                  "       [ClientSettings_MST].[blnIsUpdated], " +
                                  "       [AusUpdateDetails].[sUpdategloSuiteClientLocation], " +
                                  "       [AusUpdateDetails].[sUpdateProductCode]  " +
                                  "       from [" + strDBName + "].[dbo].[ClientSettings_MST] inner join [gloServices].[dbo].[AusUpdateDetails]  " +
                                  "  on [" + strDBName + "].[dbo].[ClientSettings_MST].[nUpdateID] = [gloServices].[dbo].[AusUpdateDetails].[nUpdateAusId] " +
                                  "  where [" + strDBName + "].[dbo].[ClientSettings_MST].[sMachineName] ='" + strMachineName + "'  " +
                                  "  and [" + strDBName + "].[dbo].[ClientSettings_MST].[blnIsUpdated] = 'False' ";

                gloAusLog("Query to get the Update Details " + strQuery + "");

                dt = GetDataTable(strQuery, strConnection);
                if (dt != null && dt.Rows.Count == 1)
                {
                    gloAusLog("Update exists");
                }
                else
                {
                    gloAusLog("Unable to get the Update details");
                }

            }
            catch (Exception ex)
            {
                gloAusLog("Exception in getUpdateDetails() " + ex.Message.ToString() + "");

            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    // dt = null;
                }
            }
            return dt;
        }
        public bool IsValidSqlLogin(string Connectionstring)
        {
            bool _result = false;

            SqlConnection Mycon = new SqlConnection(Connectionstring);
            try
            {
                if (Mycon != null)
                {
                    Mycon.Open();
                    _result = true;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (Mycon != null && Mycon.State == ConnectionState.Open)
                {
                    Mycon.Close();
                }
            }
            return _result;
        }
        public bool ExecuteQuery(string strConnection, string strQuery)
        {
            bool _success = false;
            SqlConnection Mycon = new SqlConnection(strConnection);
            SqlCommand Mycmd = new SqlCommand(strQuery, Mycon);
            try
            {
                if (Mycon != null && Mycmd != null)
                {
                    Mycon.Open();//open the connection
                    Mycmd.CommandTimeout = 0;
                    if (Mycmd.ExecuteNonQuery() > 0)
                    {
                        _success = true;
                    }

                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Unable to connect to database.To get the Database version");
                //this.Focus();
                //_success = true;
                // MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (Mycmd != null)
                {
                    Mycmd.Parameters.Clear();
                    Mycmd.Dispose();
                    Mycmd = null;
                }
                if (Mycon != null && Mycon.State == ConnectionState.Open)
                {
                    Mycon.Close();

                }
            }
            return _success;
        }
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
                if (Mycmd != null)
                {
                    Mycmd.Parameters.Clear();
                    Mycmd.Dispose();
                    Mycmd = null;
                }
                if (Mycon != null && Mycon.State == ConnectionState.Open)
                {
                    Mycon.Close();
                }
            }
        }
        public DataTable GetDataTable(string strQuery, string strConnection)
        {

            SqlConnection Mycon = new SqlConnection(strConnection);
            DataTable dt = new DataTable();
            SqlCommand Mycmd = new SqlCommand(strQuery, Mycon);
            try
            {
                Mycon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Mycmd);
                da.Fill(dt);
                if (Mycon != null && da != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                    }
                }
            }
            catch (System.ArgumentException)
            {

            }
            catch (Exception)
            {
            }
            finally
            {
                if (Mycmd != null)
                {
                    Mycmd.Parameters.Clear();
                    Mycmd.Dispose();
                    Mycmd = null;
                }

                if (Mycon != null && Mycon.State == ConnectionState.Open)
                {
                    Mycon.Close();
                    if (dt != null)
                    {
                        dt.Dispose();
                    }
                }
            }
            return dt;
        }
        #endregion Database Operations

        #region CommonFunctions

        public bool CheckDir(string strPath)
        {
            bool _success = false;
            try
            {
                //_success = new DirectoryInfo(strPath).FullName == strPath;
                if (strPath.StartsWith("\\"))
                {
                    string[] folders = Directory.GetDirectories(strPath);
                    foreach (string folder in folders)//looping through folders
                    {
                        string name = Path.GetFileName(folder);
                        string dest = Path.Combine(strPath, name);
                        if (dest.Length > 0)
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
                    if (System.IO.Directory.Exists(strPath))
                    {
                        _success = true;
                    }
                    else
                    {
                        _success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAusLog("The UNC path should be of the form \\\\server\\share.");
                gloAusLog(" Exception in CheckDir() " + ex.Message.ToString() + "");
                _success = false;

            }
            return _success;
        }
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
            catch (System.AccessViolationException)
            {

            }
            catch (System.ArgumentException)
            {

            }
            catch (System.Exception)
            {

            }
            return success;
        }
        public static void gloAusLog(String strLogMessage)
        {
            StreamWriter objFile;
            string strgloServerLogPath = string.Empty;
            try
            {
                strgloServerLogPath = Application.StartupPath + "\\AusLog";
                if (CreateFolder(strgloServerLogPath))
                {
                }
                objFile = new StreamWriter(strgloServerLogPath + "\\AusLog.log", true);
                objFile.WriteLine(System.DateTime.Now.ToString() + System.DateTime.Now.Millisecond.ToString() + ": " + strLogMessage);
                objFile.Close();

            }
            catch (Exception)
            {

            }
            finally
            {
                objFile = null;
            }

        }
        public bool ExecuteInstallerExe(string strSetupPath, string strSwitch)
        {
            bool _success = false;
            string strsetup = string.Empty;
            string strWinpath = Environment.GetEnvironmentVariable("WINDIR").ToString();//gets the WIndir path           
            if (strSetupPath == "")
            {
                strsetup = ("" + strWinpath + "\\System32\\msiexec.exe");
            }
            else
            {
                strsetup = strSetupPath;
            }
            System.Diagnostics.Process oProcess = new Process();
            try
            {
                oProcess.StartInfo.FileName = strsetup;
                oProcess.StartInfo.Arguments = strSwitch;
                oProcess.StartInfo.UseShellExecute = false;
                oProcess.StartInfo.CreateNoWindow = true;
                oProcess.StartInfo.RedirectStandardOutput = false;
                oProcess.Start();
                oProcess.WaitForExit();
                if (oProcess.ExitCode == 0 || oProcess.ExitCode == 3010)
                {
                    _success = true;
                    gloAusLog("Installed successfully " + strSwitch + "");
                    //MessageBox.Show("Installed gloEMR Client successfully", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                gloAusLog("ExitCode " + oProcess.ExitCode + "");
            }
            catch (System.ArgumentNullException)
            {
            }
            catch (Exception ex)
            {

                gloAusLog("Exception : While Executing Exe " + strSwitch + " " + ex.Message.ToString() + "");
            }
            finally
            {
                if (oProcess != null)
                {
                    oProcess.Close();
                    oProcess.Dispose();
                }
            }
            return _success;
        }
        public void CopyFolder(string sourceFolder, string destFolder)
        {
            try
            {
                //pgInfo.SetmyStatusText("Copying data to the gloData Folder");
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);

                string[] files = Directory.GetFiles(sourceFolder);
                Application.DoEvents();
                foreach (string file in files)//looping through files
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destFolder, name);
                    System.IO.File.Copy(file, dest, true);
                    Application.DoEvents();

                }
                string[] folders = Directory.GetDirectories(sourceFolder);
                Application.DoEvents();
                foreach (string folder in folders)//looping through folders
                {

                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destFolder, name);
                    //pgInfo.SetmyStatusText("Copying "+sourceFolder+"");
                    Application.DoEvents();
                    //clsInstallationLogs.gloServerLog("Copying " + sourceFolder + " to " + destFolder + " ");
                    CopyFolder(folder, dest);//recursive function
                    Application.DoEvents();
                }
            }
            catch (System.AccessViolationException)
            {
            }
            catch (System.ApplicationException)
            {
            }
            catch (System.ArgumentException)
            {
            }
            catch (Exception)
            {
            }
            Application.DoEvents();
        }

        #endregion CommonFunctions

        #region ClientUpdates

        //public void CheckforUpdate(string Connectionstring, string _type, string ServerPath,string strDbName)
        //{

        //    string LogFilePathLocation = strApppath;
        //    SqlConnection conn = new SqlConnection(Connectionstring);
        //    string strValue = string.Empty;
        //    string strUpdateId = string.Empty;
        //    string strUpdatelocation = string.Empty;
        //    string strProductCode = string.Empty;
        //    gloAusLog("Check for Update process started");
        //    gloAusLog("Parameters are");
        //    if (Connectionstring != null)
        //    {
        //        //gloAusLog("Connection string " + Connectionstring + "");
        //    }
        //    if (_type != null)
        //    {
        //        gloAusLog("type of instalaltion " + _type + "");
        //    }
        //    if (ServerPath != null)
        //    {
        //        gloAusLog("Server Path " + ServerPath + "");
        //    }
        //    bool blnUpdateFlag = false;
        //    try
        //    {
        //        if (IsValidSqlLogin(Connectionstring))
        //        {

        //            conn.Open();
        //            strConnectionstring = Connectionstring;
        //            gloAusLog("Conencted to database sucessfully");
        //            string _strSQL = "select ssettingsvalue from settings where ssettingsname='Db Version'";
        //            //string _strSQL = "select ssettingsvalue from settings where ssettingsname='Application Version'";
        //            gloAusLog("Query to get the Database version" + _strSQL + "");
        //            SqlCommand Mycmd = new SqlCommand(_strSQL, conn);
        //            //conn.Open();
        //            if (Mycmd.ExecuteScalar() != DBNull.Value)
        //            {
        //                strValue = Mycmd.ExecuteScalar().ToString();
        //                gloAusLog("Database version " + strValue + "");
        //            }
        //            DataTable dt = new DataTable();
        //            dt = getUpdateDetails(Connectionstring, strDbName);
        //            if (dt != null && dt.Rows.Count == 1)
        //            {
        //                // strUpdateId
        //                blnUpdateFlag = Convert.ToBoolean(dt.Rows[0]["blnisUpdated"].ToString());
        //                strUpdatelocation = dt.Rows[0]["sUpdategloSuiteClientLocation"].ToString();
        //                strProductCode = dt.Rows[0]["sUpdateProductCode"].ToString();
        //            }
        //            else
        //            {
        //                gloAusLog("Unable to get the update details datatable");
        //            }

        //            if (!String.IsNullOrEmpty(strProductCode))
        //            {
        //                if (strProductCode.ToLower() == "9" || strProductCode.ToLower() == "12" || strProductCode.ToLower() == "15")
        //                {
        //                    //either suite update or glosuite client update...
        //                    _type = "/suite";
        //                    gloAusLog("Suite update");

        //                }
        //                else
        //                {
        //                    gloAusLog("not a suite update");

        //                }
        //            }
        //            else
        //            {
        //                gloAusLog("unable to find the product details");

        //            }
        //            if (_type.ToLower() == "/emr")
        //            {
        //                if (clsRegistry.CheckRegistryExists())
        //                {
        //                    gloAusLog("gloEMR registy exists");
        //                    object objEMRInstPath = clsRegistry.GetRegistryValue("EMRInstallationPath");
        //                    if (objEMRInstPath != null && objEMRInstPath.ToString() != "")
        //                    {
        //                        strEmrInstallationpath = objEMRInstPath.ToString();
        //                        gloAusLog("EMR Installaion path " + strEmrInstallationpath + "");
        //                    }
        //                    object objServerPath = clsRegistry.GetRegistryValue("ServerPath");
        //                    if (objServerPath != null && objServerPath.ToString() != "")
        //                    {
        //                        strServerPath = objServerPath.ToString();
        //                        gloAusLog("Server path " + strServerPath + "");
        //                    }

        //                    string Pathttoexe = strEmrInstallationpath + "\\gloEMR.exe";
        //                    gloAusLog("path to exe " + Pathttoexe + "");
        //                    if (System.IO.File.Exists(Pathttoexe))
        //                    {
        //                        gloAusLog("path to exe exists " + Pathttoexe + "");

        //                        FileVersionInfo myFI = FileVersionInfo.GetVersionInfo(Pathttoexe);
        //                        strExeversion = myFI.ProductVersion.ToString();
        //                        gloAusLog("File version of gloEMR exe is  " + strExeversion + "");

        //                    }
        //                    else
        //                    {
        //                        gloAusLog("Unable to find the File version of gloEMR exe");
        //                    }
        //                }
        //                else
        //                {
        //                    gloAusLog("Unable to find gloEMR Registry key");
        //                }
        //            }
        //            else if (_type.ToLower() == "/pm")
        //            {
        //                if (clsRegistry.CheckgloPMregistryExists())
        //                {
        //                    gloAusLog("gloPM registy exists");
        //                    object objPMInstPath = clsRegistry.GetgloPMRegistryValue("PMInstallationPath");
        //                    if (objPMInstPath != null && objPMInstPath.ToString() != "")
        //                    {
        //                        strPMInstallationpath = objPMInstPath.ToString();
        //                        gloAusLog("PM Installaion path " + strPMInstallationpath + "");
        //                    }
        //                    object objServerPath = clsRegistry.GetRegistryValue("ServerPath");
        //                    if (objServerPath != null && objServerPath.ToString() != "")
        //                    {
        //                        strServerPath = objServerPath.ToString();
        //                        gloAusLog("Server path " + strServerPath + "");
        //                    }
        //                    else
        //                    {
        //                        objServerPath = clsRegistry.GetgloPMRegistryValue("ServerPath");
        //                        strServerPath = objServerPath.ToString();
        //                        gloAusLog("Server path " + strServerPath + "");
        //                    }

        //                    string Pathttoexe = strPMInstallationpath + "\\gloPM.exe";
        //                    gloAusLog("path to exe " + Pathttoexe + "");
        //                    if (System.IO.File.Exists(Pathttoexe))
        //                    {
        //                        gloAusLog("path to exe exists " + Pathttoexe + "");

        //                        FileVersionInfo myFI = FileVersionInfo.GetVersionInfo(Pathttoexe);
        //                        strExeversion = myFI.ProductVersion.ToString();
        //                        gloAusLog("File version of gloPM exe is  " + strExeversion + "");
        //                    }
        //                    else
        //                    {
        //                        gloAusLog("Unable to find the File version of gloPM exe");
        //                    }
        //                }
        //                else
        //                {
        //                    gloAusLog("Unable to find gloPM Registry key");
        //                }
        //            }
        //            if (_type.ToLower() == "/suite")
        //            {
        //                if (clsRegistry.CheckRegistryExists())
        //                {
        //                    gloAusLog("gloEMR registy exists");
        //                    object objEMRInstPath = clsRegistry.GetRegistryValue("EMRInstallationPath");
        //                    if (objEMRInstPath != null && objEMRInstPath.ToString() != "")
        //                    {
        //                        strEmrInstallationpath = objEMRInstPath.ToString();
        //                        gloAusLog("EMR Installaion path " + strEmrInstallationpath + "");
        //                    }
        //                    object objServerPath = clsRegistry.GetRegistryValue("ServerPath");
        //                    if (objServerPath != null && objServerPath.ToString() != "")
        //                    {
        //                        strServerPath = objServerPath.ToString();
        //                        gloAusLog("Server path " + strServerPath + "");
        //                    }

        //                    string Pathttoexe = strEmrInstallationpath + "\\gloEMR.exe";
        //                    gloAusLog("path to exe " + Pathttoexe + "");
        //                    if (System.IO.File.Exists(Pathttoexe))
        //                    {
        //                        gloAusLog("path to exe exists " + Pathttoexe + "");

        //                        FileVersionInfo myFI = FileVersionInfo.GetVersionInfo(Pathttoexe);
        //                        strExeversion = myFI.ProductVersion.ToString();
        //                        gloAusLog("File version of gloEMR exe is  " + strExeversion + "");

        //                    }
        //                    else
        //                    {
        //                        gloAusLog("Unable to find the File version of gloEMR exe");
        //                    }
        //                }
        //                else
        //                {
        //                    gloAusLog("Unable to find gloEMR Registry key");
        //                }

        //                if (!String.IsNullOrEmpty(strExeversion))
        //                {
        //                }
        //                else
        //                {
        //                    if (clsRegistry.CheckgloPMregistryExists())
        //                    {
        //                        gloAusLog("gloPM registy exists");
        //                        object objPMInstPath = clsRegistry.GetgloPMRegistryValue("PMInstallationPath");
        //                        if (objPMInstPath != null && objPMInstPath.ToString() != "")
        //                        {
        //                            strPMInstallationpath = objPMInstPath.ToString();
        //                            gloAusLog("PM Installaion path " + strPMInstallationpath + "");
        //                        }
        //                        object objServerPath = clsRegistry.GetRegistryValue("ServerPath");
        //                        if (objServerPath != null && objServerPath.ToString() != "")
        //                        {
        //                            strServerPath = objServerPath.ToString();
        //                            gloAusLog("Server path " + strServerPath + "");
        //                        }
        //                        else
        //                        {
        //                            objServerPath = clsRegistry.GetgloPMRegistryValue("ServerPath");
        //                            strServerPath = objServerPath.ToString();
        //                            gloAusLog("Server path " + strServerPath + "");
        //                        }

        //                        string Pathttoexe = strPMInstallationpath + "\\gloPM.exe";
        //                        gloAusLog("path to exe " + Pathttoexe + "");
        //                        if (System.IO.File.Exists(Pathttoexe))
        //                        {
        //                            gloAusLog("path to exe exists " + Pathttoexe + "");

        //                            FileVersionInfo myFI = FileVersionInfo.GetVersionInfo(Pathttoexe);
        //                            strExeversion = myFI.ProductVersion.ToString();
        //                            gloAusLog("File version of gloPM exe is  " + strExeversion + "");
        //                        }
        //                        else
        //                        {
        //                            gloAusLog("Unable to find the File version of gloPM exe");
        //                        }
        //                    }
        //                }
        //                if (!String.IsNullOrEmpty(strUpdatelocation))
        //                {

        //                    if (Convert.ToInt16(strExeversion.ToLower().Replace(".", "")) < Convert.ToInt16(strValue.ToLower().Replace(".", "")))
        //                    {
        //                        gloAusLog("Updates are available");
        //                        if (strProductCode == "9" || strProductCode == "15")
        //                        {
        //                            MessageBox.Show("Updates are available for your machine.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                            //logic comes for calling glosuiteclient exe
        //                            InstallClientUpdate(_type, strUpdatelocation, strServerPath);
        //                        }
        //                        else
        //                        {
        //                            gloAusLog("product code is not correct");
        //                        }

        //                    }
        //                    else
        //                    {
        //                        gloAusLog("Application version " + strExeversion + "");
        //                    }
        //                    //}
        //                    //else
        //                    //{
        //                    //    gloAusLog("Not a valid Update location " + strUpdatelocation + "");
        //                    //}
        //                }

        //            }
        //            else
        //            {
        //                if (!String.IsNullOrEmpty(strValue) && !String.IsNullOrEmpty(strExeversion) && !String.IsNullOrEmpty(strUpdatelocation) && !String.IsNullOrEmpty(strServerPath))
        //                {

        //                    if (Convert.ToInt16(strExeversion.ToLower().Replace(".", "")) <= Convert.ToInt16(strValue.ToLower().Replace(".", "")) && !blnUpdateFlag)
        //                    {
        //                        if (_type.ToLower() == "/emr" && strProductCode == "10")
        //                        {
        //                            gloAusLog("Updated are available");
        //                            MessageBox.Show("Updates are available for your machine.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                            //logic comes for calling glosuiteclient exe
        //                            InstallClientUpdate(_type, strUpdatelocation, strServerPath);
        //                        }
        //                        else if (_type.ToLower() == "/pm" && strProductCode == "11")
        //                        {
        //                            gloAusLog("Updated are available");
        //                            MessageBox.Show("Updates are available for your machine.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                            //logic comes for calling glosuiteclient exe
        //                            InstallClientUpdate(_type, strUpdatelocation, strServerPath);
        //                        }
        //                        else
        //                        {
        //                            gloAusLog("product code is not correct");
        //                        }


        //                        //if (strProductCode == "9" || strProductCode == "10" || strProductCode == "11" || strProductCode == "15" || strProductCode == "16")
        //                        //{
        //                        //    gloAusLog("Updated are available");
        //                        //    MessageBox.Show("Updates are available for your machine.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                        //    //logic comes for calling glosuiteclient exe
        //                        //    InstallClientUpdate(_type, strUpdatelocation, strServerPath);
        //                        //}
        //                        //else
        //                        //{
        //                        //    gloAusLog("product code is not correct");
        //                        //}
        //                    }
        //                    else
        //                    {
        //                        gloAusLog("Application version " + strExeversion + "");
        //                    }

        //                }
        //                else
        //                {
        //                    gloAusLog("Unable to find the Database version " + strValue + "");
        //                    gloAusLog("Unable to find the Application version " + strExeversion + "");
        //                    gloAusLog("Unable to find the UpdateLocation " + strUpdatelocation + "");
        //                    gloAusLog("Unable to find the Serverpath " + strServerPath + "");
        //                }
        //            }


        //            //if (!String.IsNullOrEmpty(strValue) && !String.IsNullOrEmpty(strExeversion) && !String.IsNullOrEmpty(strUpdatelocation))
        //            //{
        //            //    if (strValue.ToLower() != strExeversion.ToLower() || !blnUpdateFlag)
        //            //    {
        //            //        gloAusLog("Updates are available");
        //            //        MessageBox.Show("Updates are available for your machine.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            //        //logic comes for calling glosuiteclient exe
        //            //        InstallClientUpdate(_type, strUpdatelocation, strServerPath);
        //            //    }
        //            //    else
        //            //    {
        //            //        gloAusLog("Application version " + strExeversion + "");
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    gloAusLog("Unable to find the Database version " + strValue + "");
        //            //    gloAusLog("Unable to find the Application version " + strExeversion + "");
        //            //    gloAusLog("Unable to find the UpdateLocation " + strUpdatelocation + "");
        //            //    gloAusLog("Unable to find the Serverpath " + strServerPath + "");
        //            //}
        //        }
        //        else
        //        {
        //            gloAusLog("Unable to connect to connect to database");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAusLog("Exception CheckforUpdate " + ex.Message.ToString() + "");
        //    }
        //    finally
        //    {
        //        if (conn != null && conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }


        //    }
        //}
        //Aniket: Return value for Single Sign ON
        public bool CheckforUpdate(string Connectionstring, string _type, string ServerPath, string strDbName)
        {
            string LogFilePathLocation = strApppath;
            string strValue = string.Empty;
            string strUpdateId = string.Empty;
            string strUpdatelocation = string.Empty;
            string strProductCode = string.Empty;
            DataTable dtAvailableUpdate = null;
            clsInstallUpdates objInstallUpdate = null;
            frmProgress oProgress = null;
            bool blnIsUpdatePresent = false; 
            try
            {
                //conn = new SqlConnection(Connectionstring);
                objInstallUpdate = new clsInstallUpdates(Connectionstring);

                gloAusLog("Check for Update process started");
                gloAusLog("Parameters are");
                if (Connectionstring != null)
                {
                    //gloAusLog("Connection string " + Connectionstring + "");
                }
                if (_type != null)
                {
                    gloAusLog("type of installation " + _type + "");
                }
                if (ServerPath != null)
                {
                    gloAusLog("Server Path " + ServerPath + "");
                }

                if (IsValidSqlLogin(Connectionstring))
                {
                    if (objInstallUpdate.CheckUpdatesAvailable())
                    {
                        blnIsUpdatePresent = true;
                        dtAvailableUpdate = objInstallUpdate.GetAvailableUpdateList();

                        using (oProgress = new frmProgress(Connectionstring, _type.TrimStart('/')))
                        {
                            oProgress.ShowDialog(oProgress.Parent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAusLog("Exception CheckforUpdate " + ex.Message.ToString() + "");
            }
            finally
            {
                if (dtAvailableUpdate != null)
                {
                    dtAvailableUpdate.Dispose();
                    dtAvailableUpdate = null;
                }

                if (objInstallUpdate != null)
                {
                    objInstallUpdate.Dispose();
                    objInstallUpdate = null;

                }
            }
            return blnIsUpdatePresent;
        } //CheckforUpdate

        public void UpdateFlag()
        {
            string strQuery = string.Empty;
            try
            {
                if (!String.IsNullOrEmpty(strConnectionstring))
                {
                    string strQueryVersion = "select ssettingsvalue from settings where ssettingsname='Application Version'";
                    string strVersion = GetScalar(strQueryVersion, strConnectionstring);
                    if (!String.IsNullOrEmpty(strVersion))
                    {
                        strQuery = "update ClientSettings_MST set blnIsUpdated='True',sCurrentProductVersion='" + strVersion + "',sLatestProductVersion='" + strVersion + "' where sMachineName='" + strMachineName + "'";
                    }
                    else
                    {
                        strQuery = "update ClientSettings_MST set blnIsUpdated='True' where sMachineName='" + strMachineName + "'";
                    }

                    if (ExecuteQuery(strConnectionstring, strQuery))
                    {
                        gloAusLog("Updated blnIsupdated to True successfully");
                    }
                    else
                    {
                        gloAusLog("Failed to update blnIsupdated to True");
                    }
                }
                else
                {
                    gloAusLog("connectionstring not found");
                }
            }
            catch (Exception ex)
            {
                gloAusLog("Exception in UpdateFlag " + ex.Message.ToString() + "");
            }


        }

        //public void InstallClientUpdate(string _type, string Updatelocation, string ServerPath)
        //{
        //    strTypeofProduct = _type;
        //    strUpdateLocation = Updatelocation;
        //    frmProgress oProgress = new frmProgress();
        //    oProgress.ShowDialog(oProgress.Parent);
        //    oProgress.Dispose();
        //    oProgress = null;
        //}

        #endregion ClientUpdates

        #region AdminUpdates
        /// <summary>
        /// Logging for Adminupdates
        /// </summary>
        /// <param name="strLogMessage"></param>
        public static void gloAusAdminLog(String strLogMessage)
        {
            StreamWriter objFile = null;
            string strgloServerLogPath = string.Empty;
            try
            {
                strgloServerLogPath = Application.StartupPath + "\\AusAdminLog";
                if (CreateFolder(strgloServerLogPath))
                {
                }
                objFile = new StreamWriter(strgloServerLogPath + "\\AusAdminLog.log", true);
                objFile.WriteLine(System.DateTime.Now.ToString() + System.DateTime.Now.Millisecond.ToString() + ": " + strLogMessage);
                objFile.Close();

            }
            catch (Exception)
            {

            }
            finally
            {
                if (objFile != null)
                {
                    objFile.Dispose();
                    objFile = null;
                }
            }

        }

        public void CheckForAdminUpdate(string strConnection, string _UpdateType)
        {
            string strMSIPathQuery = string.Empty;
            string strExeversion = string.Empty;
            string strPathtoExe = string.Empty;
            string strDbVersion = string.Empty;
            string strPathtoMSI = string.Empty;
            string strInstallationPath = string.Empty;
            try
            {
                if (!String.IsNullOrEmpty(strConnection) && !String.IsNullOrEmpty(_UpdateType))
                {
                    //check whether connection is valid or not...
                    if (IsValidSqlLogin(strConnection))
                    {
                        gloAusAdminLog("Connection established successfully to database");
                        #region Finding Database and Appliaction Version
                        string strCheckVersion = "select ssettingsvalue from settings where ssettingsname='DB Version'";
                        strDbVersion = GetScalar(strCheckVersion, strConnection);
                        if (!String.IsNullOrEmpty(strDbVersion))
                        {
                            gloAusAdminLog("Database version : " + strDbVersion + "");

                        }
                        else
                        {
                            gloAusAdminLog("Unable to find the database version setting from the settings table");
                            //check for the application version setting....
                            strCheckVersion = "select ssettingsvalue from settings where ssettingsname='Application Version'";
                            strDbVersion = GetScalar(strCheckVersion, strConnection);
                        }
                        #endregion Finding Database and Appliaction Version
                        #region Get the installation path for respective admin update type
                        switch (_UpdateType.ToLower())
                        {
                            case "emradmin":
                                strMSIPathQuery = "select UPPER(ssettingsvalue) from settings where UPPER(ssettingsname)='AUSEMRADMINPATH'";
                                if (clsRegistry.CheckRegistryExists())
                                {
                                    object objEMRInstPath = clsRegistry.GetRegistryValue("EMRAdminPath", clsRegistry.str32EMRKey);
                                    if (objEMRInstPath != null && objEMRInstPath.ToString() != "")
                                    {
                                        strInstallationPath = objEMRInstPath.ToString();
                                        gloAusAdminLog("EMRAdmin Installation path " + strEmrInstallationpath + "");
                                    }
                                    else
                                    {
                                        int _type = clsRegistry.CheckMachineStatus();
                                        if (_type == 1)
                                        {
                                            objEMRInstPath = clsRegistry.GetRegistryValue("EMRAdminPath", clsRegistry.str64EMRKey);
                                            if (objEMRInstPath != null && objEMRInstPath.ToString() != "")
                                            {
                                                strInstallationPath = objEMRInstPath.ToString();
                                                gloAusAdminLog("EMRAdmin Installtaion path " + strEmrInstallationpath + "");
                                            }
                                        }
                                    }
                                    if (!String.IsNullOrEmpty(strInstallationPath))
                                    {
                                        if (Directory.Exists(strInstallationPath))
                                        {
                                            strPathtoExe = strInstallationPath + "\\gloEMRAdmin.exe";
                                        }
                                    }
                                }
                                else
                                {
                                    gloAusAdminLog("Unable to find the gloEMR registry key");
                                }
                                break;
                            case "pmadmin":
                                strMSIPathQuery = "select UPPER(ssettingsvalue) from settings where UPPER(ssettingsname)='AUSPMADMINPATH'";
                                if (clsRegistry.CheckgloPMregistryExists())
                                {
                                    object objPMInstPath = clsRegistry.GetRegistryValue("PMAdminPath", clsRegistry.str32PMKey);
                                    if (objPMInstPath != null && objPMInstPath.ToString() != "")
                                    {
                                        strInstallationPath = objPMInstPath.ToString();
                                        gloAusAdminLog("PMAdmin Installation path " + strEmrInstallationpath + "");
                                    }
                                    else
                                    {
                                        int _type = clsRegistry.CheckMachineStatus();
                                        if (_type == 1)
                                        {
                                            objPMInstPath = clsRegistry.GetRegistryValue("PMAdminPath", clsRegistry.str64PMKey);
                                            if (objPMInstPath != null && objPMInstPath.ToString() != "")
                                            {
                                                strInstallationPath = objPMInstPath.ToString();
                                                gloAusAdminLog("PMAdmin Installtaion path " + strInstallationPath + "");
                                            }
                                        }
                                    }
                                    if (!String.IsNullOrEmpty(strInstallationPath))
                                    {
                                        if (Directory.Exists(strInstallationPath))
                                        {
                                            strPathtoExe = strInstallationPath + "\\gloPMAdmin.exe";
                                        }
                                    }
                                }
                                else
                                {
                                    gloAusAdminLog("Unable to find the gloPM registry key");
                                }
                                break;
                            case "cm":
                                strMSIPathQuery = "select UPPER(ssettingsvalue) from settings where UPPER(ssettingsname)='AUSCMPATH'";
                                if (clsRegistry.CheckgloPMregistryExists())
                                {
                                    object objPMInstPath = clsRegistry.GetRegistryValue("PMInstallationPath", clsRegistry.str32PMKey);
                                    if (objPMInstPath != null && objPMInstPath.ToString() != "")
                                    {
                                        strInstallationPath = objPMInstPath.ToString();
                                        gloAusAdminLog("ClaimManager Installation path " + strInstallationPath + "");
                                    }
                                    else
                                    {
                                        int _type = clsRegistry.CheckMachineStatus();
                                        if (_type == 1)
                                        {
                                            objPMInstPath = clsRegistry.GetRegistryValue("PMInstallationPath", clsRegistry.str64PMKey);
                                            if (objPMInstPath != null && objPMInstPath.ToString() != "")
                                            {
                                                strInstallationPath = objPMInstPath.ToString();
                                                gloAusAdminLog("ClaimManager Installtaion path " + strInstallationPath + "");
                                            }
                                        }
                                    }
                                    if (!String.IsNullOrEmpty(strInstallationPath))
                                    {
                                        strInstallationPath = strInstallationPath + "\\Claim Manager";
                                        if (Directory.Exists(strInstallationPath))
                                        {
                                            strPathtoExe = strInstallationPath + "\\gloPMClaimService.exe";
                                        }
                                    }
                                }
                                else
                                {
                                    gloAusAdminLog("Unable to find the gloPM registry key");
                                }
                                break;
                            default:
                                break;
                        }
                        #endregion Get the installation path for respective admin update type
                        #region Get the Installed Exe version
                        if (!String.IsNullOrEmpty(strPathtoExe))
                        {
                            if (System.IO.File.Exists(strPathtoExe))
                            {
                                FileVersionInfo myFI = FileVersionInfo.GetVersionInfo(strPathtoExe);
                                strExeversion = myFI.ProductVersion.ToString();
                                if (!String.IsNullOrEmpty(strExeversion))
                                {
                                    gloAusAdminLog("Installed " + _UpdateType + " exe version is   " + strExeversion + "");
                                }
                            }
                            else
                            {
                                gloAusAdminLog("Unable to find the " + _UpdateType + " exe");
                            }

                        }
                        #endregion Get the Installed Exe version
                        #region CheckForUpdate and Install Update
                        if (!String.IsNullOrEmpty(strDbVersion) && !String.IsNullOrEmpty(strExeversion))
                        {
                            if (Convert.ToInt16(strExeversion.ToLower().Replace(".", "")) < Convert.ToInt16(strDbVersion.ToLower().Replace(".", "")))
                            {
                                if (!String.IsNullOrEmpty(strMSIPathQuery))
                                {
                                    strPathtoMSI = GetScalar(strMSIPathQuery, strConnection);
                                    if (!String.IsNullOrEmpty(strPathtoMSI) && !String.IsNullOrEmpty(strInstallationPath))
                                    {
                                        MessageBox.Show("Updates are available for your machine.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        InstallAdminUpdates(strPathtoMSI, strInstallationPath, _UpdateType);
                                    }
                                    else
                                    {
                                        gloAusAdminLog("Unable to find the value of msi path");
                                    }
                                }
                                else
                                {
                                    gloAusAdminLog("Unable to find the Query to retreive the msi path from the settings table");
                                }
                            }
                        }
                        else
                        {
                            gloAusAdminLog("Unable to find the Db version and application version");
                        }



                        #endregion CheckForUpdate and Install Update
                    }
                    else
                    {
                        gloAusAdminLog("Unable to connect to database");
                    }

                }
            }
            catch (Exception ex)
            {
                gloAusAdminLog("Exception in CheckForAdminUpdate " + ex.Message.ToString() + "");
            }
            finally
            {

            }
        }

        public void XcopyData(string Source, string Destination)
        {
            try
            {
                string strCommand = " \"" + Source + "\" \"" + Destination + "\" /E /Y";
                ExecuteInstallerExe("xcopy", strCommand);
            }
            catch (Exception ex)
            {
                gloAusAdminLog("Exception in XcopyData " + ex.Message.ToString() + "");
            }
        }

        public bool InstallgloPMAdmin(string strFullPath, string strInstallationPath)
        {
            bool success = false;
            string strCommand = string.Empty;
            string IconTargetPath = string.Empty;
            if (!String.IsNullOrEmpty(strFullPath))
            {
                if (System.IO.File.Exists(strFullPath))
                {
                    strCommand = "/i \"" + strFullPath + "\" /qn TARGETDIR=\"" + strInstallationPath + "\" ALLUSERS=1 REINSTALLMODE=amus /norestart DISABLEADVTSHORTCUTS=1 /l* \"" + strInstallationPath + "\\gloPMAdmin.txt" + "\" ";
                    IconTargetPath = strInstallationPath;
                    try
                    {
                        if (ExecuteInstallerExe("", strCommand))
                        {
                            gloAusAdminLog("gloPM Admin Installed successfully");
                            success = true;
                            ////CreateDesktoIcons("gloPM Admin.lnk", IconTargetPath + "\\gloPMAdmin.exe", IconTargetPath + "\\gloPMAdminXP.ico", 0, IconTargetPath);
                            ////CreateDesktoIcons("gloPM Admin.lnk", IconTargetPath + "\\gloPMAdmin.exe", IconTargetPath + "\\gloPMAdminXP.ico", 1, IconTargetPath);

                            CreateDesktoIcons("QPM Admin.lnk", IconTargetPath + "\\gloPMAdmin.exe", IconTargetPath + "\\gloPMAdminXP.ico", 0, IconTargetPath);
                            CreateDesktoIcons("QPM Admin.lnk", IconTargetPath + "\\gloPMAdmin.exe", IconTargetPath + "\\gloPMAdminXP.ico", 1, IconTargetPath);
                            //
                            Deleteshortcut("gloPM Admin.lnk", 0);
                            Deleteshortcut("gloPM Admin.lnk", 1);
                            //
                        }
                        else
                        {
                            gloAusAdminLog("gloPM Admin failed to install");
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAusAdminLog("Exception : while installing gloPM Admin " + ex.Message.ToString() + "");
                    }
                }
                else
                {
                    gloAusAdminLog("gloPMAdminInstall msi does not exist in the location " + strFullPath + "");
                }
            }
            return success;
        }

        public bool InstallgloEMRAdmin(string strPathtoMSI, string strInstallationPath)
        {
            string strCommand = string.Empty;
            bool success = false;
            if (!String.IsNullOrEmpty(strInstallationPath))
            {
                if (System.IO.File.Exists(strPathtoMSI))
                {
                    strCommand = "/i  \"" + strPathtoMSI + "\" /qn TARGETDIR=\"" + strInstallationPath + "\" ALLUSERS=1 REINSTALLMODE=amus DISABLEADVTSHORTCUTS=1 /norestart /l* \"" + strInstallationPath + "\\gloEMRAdmin.txt" + "\" ";
                    try
                    {
                        if (ExecuteInstallerExe("", strCommand))
                        {
                            gloAusLog("gloEMR Admin Installed successfully");
                            success = true;
                            //CreateDesktoIcons("gloEMR Admin.lnk", strInstallationPath + "\\gloEMRAdmin.exe", strInstallationPath + "\\gloEMRAdmin.ico", 0, strInstallationPath);
                            //CreateDesktoIcons("gloEMR Admin.lnk", strInstallationPath + "\\gloEMRAdmin.exe", strInstallationPath + "\\gloEMRAdmin.ico", 1, strInstallationPath);
                            CreateDesktoIcons("QEMR Admin.lnk", strInstallationPath + "\\gloEMRAdmin.exe", strInstallationPath + "\\gloEMRAdmin.ico", 0, strInstallationPath);
                            CreateDesktoIcons("QEMR Admin.lnk", strInstallationPath + "\\gloEMRAdmin.exe", strInstallationPath + "\\gloEMRAdmin.ico", 1, strInstallationPath);
                            //
                            Deleteshortcut("gloEMR Admin.lnk", 0);
                            Deleteshortcut("gloEMR Admin.lnk", 1);
                            //
                        }
                        else
                        {
                            gloAusLog("gloEMR Admin failed to install");
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAusLog("Exception : while installing gloEMR Admin " + ex.Message.ToString() + "");
                    }
                }
                else
                {
                    gloAusLog("gloEMRAdminInstall msi does not exist in the location " + strPathtoMSI + "");

                }
            }
            Application.DoEvents();
            return success;
        }

        public static void CreateDesktoIcons(string IconName, string IconTargetPath, string IconLocation, int type, string startinpath)
        {
            try
            {
                WshShellClass WshShell;
                // Create a new instance of WshShellClass
                WshShell = new WshShellClass();
                // Create the shortcut
                IWshRuntimeLibrary.IWshShortcut MyShortcut;
                string strAllUsersPath = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
                //strAllUsersPath = strAllUsersPath + "\\Desktop\\gloPM.lnk";
                if (System.IO.File.Exists(IconLocation))
                {
                    if (type == 0)
                    {
                        strAllUsersPath = strAllUsersPath + "\\Desktop\\" + IconName + "";
                    }
                    else
                    {
                        strAllUsersPath = strAllUsersPath + "\\Start Menu\\Programs\\" + IconName + "";
                    }
                    // Choose the path for the shortcut
                    //MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(@"C:\MyShortcut.lnk");
                    MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(strAllUsersPath);
                    // Where the shortcut should point to
                    MyShortcut.TargetPath = IconTargetPath;
                    // Description for the shortcut
                    MyShortcut.WorkingDirectory = startinpath;
                    MyShortcut.Description = "Launch ";
                    // Location for the shortcut's icon
                    //MyShortcut.IconLocation = Application.StartupPath + @"\gloPMXP.ico";
                    MyShortcut.IconLocation = IconLocation;
                    // Create the shortcut at the given path
                    MyShortcut.Save();
                }
            }
            catch (System.ArgumentException)
            {
            }
            catch (System.ApplicationException)
            {
            }
            catch (Exception)
            {
            }
        }

        public static void Deleteshortcut(string IconName, int _type)
        {
            //find the machine status
            try
            {
                OperatingSystem os = Environment.OSVersion;
                int osVer;
                osVer = os.Version.Major;
                string IconPathLoaction = string.Empty;
                if (osVer < 6) //windows xp
                {
                    if (_type == 0)//desktop
                    {
                        IconPathLoaction = clsIcons.GetAllUsersDesktopFolderPath();
                    }
                    else //all programs
                    {
                        IconPathLoaction = clsIcons.GetallprogramsMenuPath();
                    }
                }
                else //windows 7 vista or any server os
                {
                    if (_type == 0)//desktop
                    {
                        IconPathLoaction = clsIcons.GetSharedDesktop();

                    }
                    else  //all programs
                    {
                        IconPathLoaction = clsIcons.GetSharedAllProgramsPath();
                    }
                }
                //MessageBox.Show(IconPathLoaction);
                if (!String.IsNullOrEmpty(IconPathLoaction))
                {
                    IconPathLoaction = IconPathLoaction + "\\" + IconName + "";
                }
                if (System.IO.File.Exists(IconPathLoaction))
                {
                    System.IO.File.Delete(IconPathLoaction);
                }

            }
            catch (System.IO.FileNotFoundException)
            {
            }
            catch (System.Security.SecurityException)
            {
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        public bool InstallClaimManager(string strFullPath, string strInstallationPath)
        {
            bool success = false;
            string strCommand = string.Empty;
            if (!String.IsNullOrEmpty(strFullPath))
            {
                if (System.IO.File.Exists(strFullPath))
                {
                    //Change for enabling openFileLocation Property for Shortcut icon for claim Mananger.
                    strCommand = "/i  \"" + strFullPath + "\" /qn ALLUSERS=1 REINSTALLMODE=amus DISABLEADVTSHORTCUTS=1 /norestart /l* \"" + strInstallationPath + "\\ClaimManager.txt" + "\" ";
                    try
                    {
                        if (ExecuteInstallerExe("", strCommand))
                        {
                            gloAusAdminLog("ClaimManager Installed successfully");
                            success = true;
                            CreateDesktoIcons("Claim Manager.lnk", strInstallationPath + "\\gloPMClaimService.exe", strInstallationPath + "\\Claim Status.ico", 0, strInstallationPath);
                            CreateDesktoIcons("Claim Manager.lnk", strInstallationPath + "\\gloPMClaimService.exe", strInstallationPath + "\\Claim Status.ico", 1, strInstallationPath);
                        }
                        else
                        {

                            gloAusAdminLog("ClaimManager failed to install");
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAusAdminLog("Exception : while installing ClaimManager " + ex.Message.ToString() + "");

                    }
                }
                else
                {
                    gloAusAdminLog("CMSetup msi does not exist in the location " + strFullPath + "");
                }
            }
            return success;
        }

        public void InstallAdminUpdates(string strPathtoMSI, string strInstallationPath, string _type)
        {
            try
            {
                //copy the file to local machine and execute the msi...
                strAdminProductType = _type;
                strAdminupdateMSI = strPathtoMSI;
                strAdminupdatePath = strInstallationPath;
                frmAdminProgress oProgress = new frmAdminProgress();
                oProgress.ShowDialog(oProgress.Parent);
                oProgress.Dispose();
                oProgress = null;
                #region commented code
                //string strPathtotemp = Path.GetTempPath();
                //string strFullPathtoMSI = string.Empty;
                //if (!String.IsNullOrEmpty(strPathtotemp))
                //{
                //    XcopyData(strPathtoMSI, strPathtotemp);
                //}
                //else
                //{
                //    gloAusAdminLog("Unable to find the Temp path");
                //}
                //if (_type.ToLower() == "emradmin")
                //{
                //    strFullPathtoMSI = strPathtotemp + "\\gloEMRAdmininstall.msi";
                //    if (System.IO.File.Exists(strFullPathtoMSI))
                //    {
                //        InstallgloEMRAdmin(strPathtoMSI, strInstallationPath);
                //    }
                //    else
                //    {
                //        gloAusAdminLog("Unable to find the path to EmrAdmin msi");
                //    }
                //}
                //else if (_type.ToLower() == "pmadmin")
                //{
                //    strFullPathtoMSI = strPathtotemp + "\\gloPMAdminInstall.msi";
                //    if (System.IO.File.Exists(strFullPathtoMSI))
                //    {
                //        InstallgloPMAdmin(strPathtoMSI, strInstallationPath);
                //    }
                //    else
                //    {
                //        gloAusAdminLog("Unable to find the path to PMAdminmsi");
                //    }
                //}
                //else if (_type.ToLower() == "cm")
                //{
                //    strFullPathtoMSI = strPathtotemp + "\\CMSetup.msi";
                //    if (System.IO.File.Exists(strFullPathtoMSI))
                //    {
                //        InstallClaimManager(strPathtoMSI, strInstallationPath);
                //    }
                //    else
                //    {
                //        gloAusAdminLog("Unable to find the path to CM msi");
                //    }
                //}    
                #endregion commented code
            }
            catch (Exception ex)
            {
                gloAusAdminLog("Exception in InstallAdminUpdates " + ex.Message.ToString() + "");

            }
        }

        #endregion AdminUpdates
    }
}
