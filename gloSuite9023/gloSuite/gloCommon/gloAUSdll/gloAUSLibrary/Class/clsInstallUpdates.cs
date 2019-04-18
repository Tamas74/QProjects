using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Configuration;
using Microsoft.Win32;
using System.Threading;
using System.Security.Principal;


namespace gloAUSLibrary
{
    public class clsInstallUpdates : IDisposable
    {
        public string strConnectionString = null;

        #region "Constructor and Distructor"

        public clsInstallUpdates(string strConnString)
        {
            strConnectionString = strConnString;
        }

        ~clsInstallUpdates()
        {
            Dispose(false);
        }

        #endregion "Constructor and Distructor"

        #region "Function"

        public bool InstallFilePatch(string strProductName, string strFileName, string strUpdateLocation, string strProductVersion, string strIsModified = "No")
        {
            bool retStatusFlag = false;

            string strAppInstallationPath = null;
            string strFilePath = null;
            string strAppInstalledName = null;
            Int64 strExistingFileVersion = 0;
            Int64 strNewFileVersion = 0;
            bool blnReplace = true;
            try
            {
                //Fetching installed application display Name from app.config file.
                strAppInstalledName = Convert.ToString(ConfigurationManager.AppSettings[strProductName.ToUpper()]).Trim();

                //Checking Application is installed or not
                if (CheckIsapplicationExistsinARP(strAppInstalledName, strProductVersion))
                {
                    //Fetching application installation file path from ARP
                    strAppInstallationPath = GetInstallationPathBasedOnAppName(strAppInstalledName, strProductVersion);

                    //Fetching gloUpdates file path location that need's to be insatlled
                    strFilePath = GetFilePathFromFileName(strFileName, strUpdateLocation);
                    try
                    {
                        if (Directory.Exists(strAppInstallationPath))
                        {
                            FileInfo objFileinfo = new FileInfo(strFileName);
                            //if (objFileinfo.Extension != ".rpt")

                            if (objFileinfo.Extension.ToLower() != ".rpt" && objFileinfo.Extension.ToLower() != ".xml" && objFileinfo.Extension.ToLower() != ".sef" && objFileinfo.Extension.ToLower() != ".xsl")
                            {
                                if (File.Exists(Path.Combine(strAppInstallationPath, strFileName)))
                                {
                                    FileVersionInfo objFileVerInfo = FileVersionInfo.GetVersionInfo(Path.Combine(strAppInstallationPath, strFileName));
                                    if (!string.IsNullOrEmpty(objFileVerInfo.FileVersion))
                                    {
                                        strExistingFileVersion = Convert.ToInt64(objFileVerInfo.FileVersion.Replace(".", ""));
                                    }

                                    objFileVerInfo = FileVersionInfo.GetVersionInfo(strFilePath);

                                    if (!string.IsNullOrEmpty(objFileVerInfo.FileVersion))
                                    {
                                        strNewFileVersion = Convert.ToInt64(objFileVerInfo.FileVersion.Replace(".", ""));
                                    }

                                    clsGeneral.UpdateLog("Existing File version :" + strExistingFileVersion);
                                    clsGeneral.UpdateLog("New File version :" + strAppInstallationPath);
                                    if (strNewFileVersion > strExistingFileVersion)
                                    {
                                        blnReplace = true;
                                    }
                                    //strNewFileVersion = objFileVerInfo.FileVersion;
                                }

                                clsGeneral.UpdateLog("Update File Path :" + strFilePath);
                                clsGeneral.UpdateLog("Installation Path :" + strAppInstallationPath);
                                if (blnReplace)
                                {
                                    CopyFilesFromSourcetoDestination(strFilePath, strAppInstallationPath, strFileName, true);
                                }
                                else
                                {
                                    clsGeneral.UpdateLog("File already exists with same verison :" + strFileName);
                                }
                            }
                            else if (objFileinfo.Extension.ToLower() == ".xml" || objFileinfo.Extension.ToLower() == ".sef" || objFileinfo.Extension.ToLower() == ".xsl")
                            {
                                if (CopyFilesFromSourcetoDestination(strFilePath, strAppInstallationPath, strFileName, true))
                                {
                                    clsGeneral.UpdateLog("File deployed successfully :" + strFileName);
                                }
                                else
                                {
                                    clsGeneral.UpdateLog("Failed to deploy file :" + strFileName);
                                }
                            }
                            else if (objFileinfo.Extension.ToLower() == ".rpt")
                            {
                                if (File.Exists(Path.Combine(Path.Combine(strAppInstallationPath, "Reports"), strFileName)))
                                {
                                    if (CopyFilesFromSourcetoDestination(strFilePath, Path.Combine(strAppInstallationPath, "Reports\\"), strFileName, true))
                                    {
                                        clsGeneral.UpdateLog("File Replace scuccessfully :" + strFileName);
                                    }
                                    else
                                        clsGeneral.UpdateLog("Error while replacing file :" + strFileName);
                                }
                                else
                                {
                                    clsGeneral.UpdateLog("File path does not exits for type rpt.");
                                }
                            }

                            retStatusFlag = true;
                        }
                        else
                        {
                            retStatusFlag = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        clsGeneral.UpdateLog("Error while installating the update for product '" + strProductName.ToUpper().Trim() + "' : " + ex.Message.ToString());
                        retStatusFlag = false;
                    }
                }
                else
                {
                    //if (strProductName.ToUpper().Trim() == "GLOEMR" || strProductName.ToUpper().Trim() == "GLOPM")
                    //{
                    retStatusFlag = true;
                    clsGeneral.UpdateLog("Update for Product :" + strProductName + " cannot be installed .As product is not Installed or Installation Path does not exist.");
                    //}
                }
            }
            catch (Exception ex)
            {
                retStatusFlag = false;
                clsGeneral.UpdateLog("Error while installing File Updated for Product :" + strProductName + " Error : " + ex.Message.ToString());
            }
            return retStatusFlag;
        } //InstallFilePatch

        public bool InstallMSIPatch(string strProductName, string strFileName, string strUpdateLocation, string strProductVersion, string strIsModified = "No")
        {
            bool retStatusFlag = false;

            string strAppInstallationPath = null;
            string strFilePath = null;
            string strCommand = null;
            string strOutputlogFilePath = null;
            string strAppInstalledName = null;

            try
            {
                //Fetching installed application display Name from app.config file.
                strAppInstalledName = Convert.ToString(ConfigurationManager.AppSettings[strProductName.ToUpper()]).Trim();

                //Checking Application is installed or not
                if (CheckIsapplicationExistsinARP(strAppInstalledName, strProductVersion))
                {
                    //Fetching application installation file path from ARP
                    strAppInstallationPath = GetInstallationPathBasedOnAppName(strAppInstalledName, strProductVersion);

                    if (string.IsNullOrEmpty(strAppInstallationPath))
                    {
                        strAppInstallationPath = clsValidations.GetProgramFilesPath() + "\\" + strAppInstalledName;
                    }

                    if (!Directory.Exists(strAppInstallationPath))
                    {
                        Directory.CreateDirectory(strAppInstallationPath);
                    }

                    //Fetching gloUpdates file path location that need's to be insatlled.
                    strFilePath = GetFilePathFromFileName(strFileName, strUpdateLocation);

                    if (!string.IsNullOrEmpty(strFilePath))
                    {
                        strOutputlogFilePath = strAppInstallationPath + "\\" + Convert.ToString(ConfigurationManager.AppSettings[strProductName.ToUpper() + "INSTALLLOGFILE"]).Trim();
                        strOutputlogFilePath = strOutputlogFilePath.Replace("\\\\", "\\");

                        if (!string.IsNullOrEmpty(strOutputlogFilePath))
                            strCommand = "/i \"" + strFilePath + "\" /qn TARGETDIR=\"" + strAppInstallationPath + "\" ALLUSERS=1 REINSTALLMODE=amus DISABLEADVTSHORTCUTS=1 /l* \"" + strOutputlogFilePath + "_log.txt" + "\" ";
                        else
                            strCommand = "/i \"" + strFilePath + "\" /qn TARGETDIR=\"" + strAppInstallationPath + "\" ALLUSERS=1 REINSTALLMODE=amus DISABLEADVTSHORTCUTS=1";

                        clsGeneral.UpdateLog("Update command for MSI:" + strCommand);

                        if (strCommand != null && strCommand.Trim() != "")
                        {
                            if (clsGeneral.RunExe("", strCommand))
                            {
                                retStatusFlag = true;

                                CreateShortCutIcon(strAppInstalledName.Trim().ToUpper(), strAppInstallationPath);

                                clsGeneral.UpdateLog("Update for Product '" + strProductName + "' installed successfully.");
                            }
                            else
                            {
                                retStatusFlag = false;
                                clsGeneral.UpdateLog("Update for Product '" + strProductName + "' not installed successfully.");
                            }
                        }
                    }
                    else
                    {
                        clsGeneral.UpdateLog("File Path for MSI:" + strFileName + " does not exist.");
                    }
                }
                else
                {
                    //if (strProductName.ToUpper().Trim() == "GLOEMR" || strProductName.ToUpper().Trim() == "GLOPM")
                    //{
                    retStatusFlag = true;
                    clsGeneral.UpdateLog("Update for Product :" + strProductName + " cannot be installed .As product is not Installed or Installation Path does not exist.");
                    //}
                }
            }
            catch (Exception ex)
            {
                retStatusFlag = false;
                clsGeneral.UpdateLog("Error while applying the msi update for product name " + strProductName + " : " + ex.Message.ToString());
            }
            return retStatusFlag;
        } //InstallMSIPatch

        public bool InstallAdminMSIPatch(string strProductName, string strFileName, string strUpdateLocation, string strAppInstallationPath)
        {
            bool retStatusFlag = false;

            string strFilePath = null;
            string strCommand = null;
            string strOutputlogFilePath = null;
            string strAppInstalledName = null;

            try
            {
                //Fetching installed application display Name from app.config file.
                strAppInstalledName = Convert.ToString(ConfigurationManager.AppSettings[strProductName.ToUpper()]).Trim();

                //Fetching gloUpdates file path location that need's to be insatlled.
                strFilePath = GetFilePathFromFileName(strFileName, strUpdateLocation);

                if (!string.IsNullOrEmpty(strFilePath))
                {
                    strOutputlogFilePath = strAppInstallationPath + "\\" + Convert.ToString(ConfigurationManager.AppSettings[strProductName.ToUpper() + "INSTALLLOGFILE"]).Trim();
                    strOutputlogFilePath = strOutputlogFilePath.Replace("\\\\", "\\");

                    if (!string.IsNullOrEmpty(strOutputlogFilePath))
                        strCommand = "/i \"" + strFilePath + "\" /qn TARGETDIR=\"" + strAppInstallationPath + "\" ALLUSERS=1 REINSTALLMODE=amus DISABLEADVTSHORTCUTS=1 /l* \"" + strOutputlogFilePath + "_log.txt \" ";
                    else
                        strCommand = "/i \"" + strFilePath + "\" /qn TARGETDIR=\"" + strAppInstallationPath + "\" ALLUSERS=1 REINSTALLMODE=amus DISABLEADVTSHORTCUTS=1";

                    clsGeneral.UpdateLog("Update command for MSI:" + strCommand);

                    if (strCommand != null && strCommand.Trim() != "")
                    {
                        if (clsGeneral.RunExe("", strCommand))
                        {
                            retStatusFlag = true;

                            CreateShortCutIcon(strAppInstalledName.ToUpper().Trim(), strAppInstallationPath);

                            clsGeneral.UpdateLog("Update for Product '" + strProductName + "' installed successfully.");
                        }
                        else
                        {
                            retStatusFlag = false;
                            clsGeneral.UpdateLog("Update for Product '" + strProductName + "' not installed successfully.");
                        }
                    }
                }
                else
                {
                    clsGeneral.UpdateLog("File Path for MSI:" + strFileName + " does not exist.");
                }
            }
            catch (Exception ex)
            {
                retStatusFlag = false;
                clsGeneral.UpdateLog("Error while applying the msi update for product name " + strProductName + " : " + ex.Message.ToString());
            }
            return retStatusFlag;
        } //InstallAdminMSIPatch

        public string GetInstallationPathBasedOnAppName(string strApplicationName, string strProductVersion)
        {
            string strRetValue = string.Empty;
            string strReg = null;
            RegistryKey regInstallPath = null;

            try
            {
                if (clsValidations.CheckMachineStatus() == 0)
                {
                    strReg = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                }
                else
                {
                    strReg = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                }

                regInstallPath = Registry.LocalMachine.OpenSubKey(strReg, false);
                if (regInstallPath != null)
                {
                    var strInstallationPathDetail = (
                                                     from name in regInstallPath.GetSubKeyNames()
                                                     let app = new InstalledApplicationPath(regInstallPath, name)
                                                     where (
                                                            app.InstallPath != null &&
                                                            app.DisplayName != null && app.DisplayName == strApplicationName &&
                                                            app.DisplayVersion == strProductVersion
                                                            )
                                                     select app
                                                     ).ToList();

                    if (strInstallationPathDetail != null)
                    {
                        if (strInstallationPathDetail.Count() > 0)
                        {
                            strRetValue = strInstallationPathDetail[0].InstallPath.Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strRetValue = null;
                clsGeneral.UpdateLog("Error while fetching installation path for application named '" + strApplicationName + "' :" + ex.Message.ToString());
            }
            finally
            {
                if (regInstallPath != null)
                {
                    try
                    {
                        regInstallPath.Close();

                        regInstallPath.Dispose();
                    }
                    catch
                    {
                    }
                    regInstallPath = null;
                }
            }
            return strRetValue;
        } //GetInstallationPathBasedOnAppName

        public bool CheckIsapplicationExistsinARP(string strApplicationName, string strProductVersion)
        {
            bool success = false;
            #region "New logic Using Linq"
            string strReg = null;
            RegistryKey regInstallPath = null;

            try
            {
                if (clsValidations.CheckMachineStatus() == 0)
                {
                    strReg = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                }
                else
                {
                    strReg = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                }

                regInstallPath = Registry.LocalMachine.OpenSubKey(strReg, false);
                if (regInstallPath != null)
                {
                    var strInstallationPathDetail = (from name in regInstallPath.GetSubKeyNames()
                                                     let app = new InstalledApplicationPath(regInstallPath, name)
                                                     where (
                                                            app.InstallPath != null &&
                                                            app.DisplayName != null && app.DisplayName == strApplicationName &&
                                                            app.DisplayVersion == strProductVersion
                                                            )
                                                     select app
                                                     ).ToList();

                    if (strInstallationPathDetail != null)
                    {
                        if (strInstallationPathDetail.Count() > 0)
                            success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while Checking application exist in ARP :" + ex.Message.ToString());
            }
            finally
            {
                if (regInstallPath != null)
                {
                    try
                    {
                        regInstallPath.Close();

                        regInstallPath.Dispose();
                    }
                    catch
                    {
                    }
                    regInstallPath = null;
                }
            }
            #endregion "New logic Using Linq"
            return success;

        } //CheckIsapplicationExistsinARP

        public List<InstalledApplicationPath> GetApplicationInstallVersion(string strApplicationName)
        {
            List<InstalledApplicationPath> lstInstalledApp = null;
            //bool success = false;
            #region "New logic Using Linq"
            string strReg = null;
            RegistryKey regInstallPath = null;

            try
            {
                if (clsValidations.CheckMachineStatus() == 0)
                {
                    strReg = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                }
                else
                {
                    strReg = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                }

                regInstallPath = Registry.LocalMachine.OpenSubKey(strReg, false);
                if (regInstallPath != null)
                {
                    var strInstallationPathDetail = (from name in regInstallPath.GetSubKeyNames()
                                                     let app = new InstalledApplicationPath(regInstallPath, name)
                                                     where (
                                                            app.InstallPath != null &&
                                                            app.DisplayName != null && app.DisplayName == strApplicationName
                                                            )
                                                     select app
                                                     ).ToList();

                    if (strInstallationPathDetail != null)
                    {
                        if (strInstallationPathDetail.Count() > 0)
                            lstInstalledApp = strInstallationPathDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while Checking application exist in ARP :" + ex.Message.ToString());
            }
            finally
            {
                if (regInstallPath != null)
                {
                    try
                    {
                        regInstallPath.Close();

                        regInstallPath.Dispose();
                    }
                    catch
                    {
                    }
                    regInstallPath = null;
                }
            }
            #endregion "New logic Using Linq"

            return lstInstalledApp;

        } //CheckIsapplicationExistsinARP

        public bool UninstallfrmControlpanel(string strApplicationName, string strProductVersion)
        {
            bool success = false;
            string strReg = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
            RegistryKey regInstallPath = null;
            string uninstall = string.Empty;

            try
            {
                #region "new Logic using Linq"
                if (clsValidations.CheckMachineStatus() == 0)
                {
                    strReg = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                }
                else
                {
                    strReg = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                }

                regInstallPath = Registry.LocalMachine.OpenSubKey(strReg, false);
                if (regInstallPath != null)
                {
                    var strInstallationPathDetail = (from name in regInstallPath.GetSubKeyNames()
                                                     let app = new InstalledApplicationPath(regInstallPath, name)
                                                     where (
                                                            app.UninstallString != null &&
                                                            app.DisplayName != null && app.DisplayName.ToLower() == strApplicationName.ToLower() &&
                                                            app.DisplayVersion == strProductVersion
                                                           )
                                                     select app
                                                     ).ToList();

                    if (strInstallationPathDetail != null)
                    {
                        if (strInstallationPathDetail.Count() > 0)
                        {
                            uninstall = strInstallationPathDetail[0].UninstallString.Trim();
                        }
                    }

                    if (!string.IsNullOrEmpty(uninstall))
                    {
                        uninstall = uninstall.Replace("/I", "/X").Remove(0, 11) + " " + "/qn";
                        if (clsGeneral.RunExe("", uninstall))
                        {
                            clsGeneral.UpdateLog("Unistall application named '" + strApplicationName + "' Successfully.");
                            success = true;
                        }
                    }
                    else //regisrty does not exist
                    {
                        success = true;
                    }
                }
                #endregion "new Logic using Linq"
            }
            catch (Exception ex)
            {
                success = false;
                clsGeneral.UpdateLog("Error while Checking application exist in ARP :" + ex.Message.ToString());
            }
            finally
            {
                if (regInstallPath != null)
                {
                    try
                    {
                        regInstallPath.Close();

                        regInstallPath.Dispose();
                    }
                    catch
                    {
                    }
                    regInstallPath = null;
                }
            }
            return success;
        } //UninstallfrmControlpanel

        public bool CheckInstanceRunning(string strMessage, string strName = null)
        {
            bool Success = false;
            string _messageBoxCaption = "gloUpdates";
            Process[] pname = null;
            Process[] pSpecificName = null;
            try
            {
                //if (!String.IsNullOrEmpty(InstanceName.ToString()))
                //{
                // pname = Process.GetProcessesByName(InstanceName);
                var strAppName = new string[] { "gloEMR", "gloEMRAdmin", "gloPM", "gloPMAdmin", "gloPMClaimService" };

                pname = Process.GetProcesses(Environment.MachineName);

                var exists = pname.Any(p => strAppName.Any(t => p.ProcessName.Contains(t)));

                //if (pname.Length == 0)
                if (!exists)
                {
                    Success = true;
                }
                else
                {
                    if (MessageBox.Show(strMessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        // clsGeneral.UpdateLog(strMessage);
                        pSpecificName = Process.GetProcessesByName(strName);
                        if (pSpecificName.Count() > 0)
                        {
                            clsGeneral.UpdateLog("Killing Process name :" + pSpecificName[0].ProcessName);
                            pSpecificName[0].Kill();
                        }
                        Thread.Sleep(15000);
                        if (CheckInstanceRunning(strMessage))
                        {
                            Success = true;
                        }
                    }
                    Application.DoEvents();
                    //if (MessageBox.Show(strMessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    //{
                    //    if (pname.Length > 0)
                    //    {
                    //        clsGeneral.UpdateLog("Killing Process name :" + pname[0].ProcessName);
                    //        pname[0].Kill();
                    //        Success = true;
                    //    }
                    //}
                }
                //}
            }
            catch (Exception ex)
            {
                Success = false;
                clsGeneral.UpdateLog("Error while Checking instance is running or not :" + ex.Message.ToString());
            }
            finally
            {
                if (pname != null)
                {
                    pname = null;
                }
            }
            return Success;
        } //CheckInstanceRunnig

        public bool CopyFilesFromSourcetoDestination(string SourcePath, string DestinationPath, string FileName, bool overwriteexisting)
        {
            bool retSuccess = false;
            FileInfo objFileInfo = null;
            string strCommand = null;
            //string strSource = null;
            string strDestination = null;
            try
            {
                if (File.Exists(SourcePath))
                {
                    objFileInfo = new FileInfo(SourcePath);
                    if (Directory.Exists(DestinationPath))
                    {
                        //if (SourcePath.Contains(FileName) == false)
                        //{
                        //    strSource = SourcePath + FileName;
                        //}
                        //else
                        //{
                        //    strSource = SourcePath;
                        //}
                        strDestination = DestinationPath + FileName;

                        clsGeneral.UpdateLog("Source Path :" + SourcePath);
                        clsGeneral.UpdateLog("Destination Path :" + strDestination);

                        strCommand = " \"" + SourcePath + "\" \"" + strDestination + "\" /q /y";

                        if (clsGeneral.RunExe("xcopy", strCommand))
                        {
                            clsGeneral.UpdateLog("File patch updated :" + strDestination);
                        }
                        //objFileInfo.CopyTo(DestinationPath + "\\" + FileName, true);
                        retSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retSuccess = false;
                clsGeneral.UpdateLog("Error while copying the file from source to destination :" + ex.Message.ToString());

            }
            finally
            {
                if (objFileInfo != null)
                {
                    objFileInfo = null;
                }
            }
            return retSuccess;
        } //CopyFilesFromSourcetoDestination

        public string GetFilePathFromFileName(string strFileName, string strSearchLocation)
        {
            string strRetValue = string.Empty;
            string[] FilePath = null;
            try
            {
                FilePath = Directory.GetFiles(strSearchLocation, strFileName, SearchOption.AllDirectories);

                if (FilePath.Length >= 1)
                {
                    strRetValue = FilePath[0];
                }
            }
            catch (Exception ex)
            {
                strRetValue = null;
                clsGeneral.UpdateLog("Error while fetching the file path from file name :" + ex.Message.ToString());
            }
            finally
            {
                if (FilePath != null)
                {
                    FilePath = null;
                }

            }
            return strRetValue;
        } //GetFilePathFromFileName

        public DataTable GetAvailableUpdateList()
        {
            DataTable returndtTemp = null;
            DBLayer objDBLayer = null;
            DBParameters objDBParameter = null;
            try
            {
                objDBLayer = new DBLayer(strConnectionString);

                objDBParameter = new DBParameters();
                objDBParameter.Add("@strClientMachineName", clsGeneral.gstrClientMachineName, ParameterDirection.Input, SqlDbType.VarChar);
                objDBParameter.Add("@strProductVersion", Convert.ToInt32(clsGeneral.GetAssemblyVersion().Replace(".", "")), ParameterDirection.Input, SqlDbType.VarChar);


                objDBLayer.Connect(false);
                objDBLayer.Retrive("gsp_GetAvailableUpdateList", objDBParameter, out returndtTemp);

                if (returndtTemp != null)
                {
                    if (returndtTemp.Rows.Count > 0)
                    {
                        return returndtTemp;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while fetching the available update list :" + ex.Message.ToString());
            }
            finally
            {
                if (objDBParameter != null)
                {
                    objDBParameter.Dispose();
                    objDBParameter = null;
                }
                if (objDBLayer != null)
                {
                    objDBLayer.Dispose();
                    objDBLayer = null;
                }
            }

            return returndtTemp;
        } //GetAvailableUpdateList

        public void UpdateClientSettings(Int64 nUpdateId, string strProductCode, string strUpdateStatusMessage, int blnStatus = 0)
        {
            DBLayer oDblayer = null;
            string strQuery = null;
            //string strConnectionstring = null;
            string strMachineName = clsGeneral.gstrClientMachineName;
            try
            {
                //strQuery = "update [ClientSettings_mst] set [blnIsupdated] = " + blnStatus + ", [sUpdateStatus] ='" + strUpdateStatusMessage + "',[sProductcode]='" + strProductCode + "',[nUpdateId]='" + nUpdateId + "' where [sMachineName] = '" + clsGeneral.gstrClientMachineName + "'";
                //strQuery = "update [ClientSettings_mst] set [blnIsupdated] = " + blnStatus + ", [sUpdateStatus] ='" + strUpdateStatusMessage + "',[nUpdateId]='" + nUpdateId + "',dtUpdatedate = dbo.gloGetDate() where [sMachineName] = '" + clsGeneral.gstrClientMachineName + "'";
                strQuery = "update [ClientUpdateDetails] set [blnIsupdated] = " + blnStatus + ",[IsClientUpdateAvailable] = 0 ,[sUpdateStatus] ='" + strUpdateStatusMessage + "',dtInstalldate = dbo.gloGetDate() where [sMachineName] = '" + clsGeneral.gstrClientMachineName + "' and nClientUpdateId =" + nUpdateId;
                //strConnectionstring = clsGeneral.GetConnectionString();

                //if (ConfigurationManager.AppSettings["IsLogging"].ToString() == "1")
                //    clsGeneral.UpdateLog("Query to update client settings " + strQuery + "");

                oDblayer = new DBLayer(strConnectionString);

                oDblayer.Connect(false);

                oDblayer.Execute_Query(strQuery);

                oDblayer.Disconnect();

            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in  UpdateClientSettings() " + ex.Message.ToString() + "");
                oDblayer.Disconnect();
            }
            finally
            {
                if (oDblayer != null)
                {
                    oDblayer.Dispose();
                    oDblayer = null;
                }
            }
        } //UpdateClientSettings

        public bool CheckUpdatesAvailable()
        {
            bool retSuccess = false;
            string strQuery = null;
            DBLayer objDbLayer = null;
            string tempResult = null;
            try
            {
                clsGeneral.gstrClientMachineName = Environment.MachineName;
                objDbLayer = new DBLayer(strConnectionString);

                //strQuery = "SELECT [nUpdateId] from [ClientSettings_MST] where [blnIsUpdated] = 'False' and  [sMachineName] = '" + clsGeneral.gstrClientMachineName + "' and ([sUpdateStatus] is null or Upper([sUpdateStatus]) = 'RETRY') ";
                //clsGeneral.UpdateLog("Connection :" + clsGeneral.GetConnectionString());
                strQuery = "SELECT [nUpdateId] from [ClientUpdateDetails] where [blnIsUpdated] = 0 and  [sMachineName] = '" + clsGeneral.gstrClientMachineName + "' and ([sUpdateStatus] is null or Upper([sUpdateStatus]) = 'RETRY') ";

                //if (ConfigurationManager.AppSettings["IsLogging"].ToString() == "1")
                //    clsGeneral.UpdateLog("strQuery:" + strQuery);

                objDbLayer.Connect(false);

                object tmpUpdateId = objDbLayer.ExecuteScalar_Query(strQuery);


                if (tmpUpdateId != null)
                    tempResult = Convert.ToString(tmpUpdateId);

                if (!string.IsNullOrWhiteSpace(tempResult))
                {
                    clsGeneral.UpdateLog("Update are available for client machine :" + tempResult);
                    retSuccess = true;
                }

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                retSuccess = false;
                clsGeneral.UpdateLog("Error in function 'CheckUpdatesAvailable'. Error:" + ex.Message.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                    objDbLayer = null;
                }
            }
            return retSuccess;
        } //CheckUpdatesAvailable

        public void CreateShortCutIcon(string strProductName, string strInstallationPath)
        {
            try
            {
                clsGeneral.UpdateLog("Product Name : " + strInstallationPath + ". Installation Path :" + strInstallationPath);
                switch (strProductName.ToUpper().Trim())
                {
                    case "GLOEMR":
                        ////clsValidations.CreateDesktoIcons("gloEMR - The Smart EMR.lnk", strInstallationPath + "\\gloEMR.exe", strInstallationPath + "\\gloEMR.ico", 0, strInstallationPath, "Location: gloEMR - The Smart EMR(" + strInstallationPath + ")");
                        ////clsValidations.CreateDesktoIcons("gloEMR - The Smart EMR.lnk", strInstallationPath + "\\gloEMR.exe", strInstallationPath + "\\gloEMR.ico", 1, strInstallationPath, "Location: gloEMR - The Smart EMR(" + strInstallationPath + ")");
                        clsValidations.CreateDesktoIcons("QEMR - The Smart EMR.lnk", strInstallationPath + "\\gloEMR.exe", strInstallationPath + "\\gloEMR.ico", 0, strInstallationPath, "Location: gloEMR - The Smart EMR(" + strInstallationPath + ")");
                        clsValidations.CreateDesktoIcons("QEMR - The Smart EMR.lnk", strInstallationPath + "\\gloEMR.exe", strInstallationPath + "\\gloEMR.ico", 1, strInstallationPath, "Location: gloEMR - The Smart EMR(" + strInstallationPath + ")");                        
                        //
                        clsValidations.Deleteshortcut("gloEMR.lnk", 0);
                        clsValidations.Deleteshortcut("gloEMR.lnk", 1);
                        //
                        break;

                    case "GLOPM":
                        ////clsValidations.CreateDesktoIcons("gloPM.lnk", strInstallationPath + "\\gloPM.exe", strInstallationPath + "\\gloPMXP.ico", 0, strInstallationPath, "Location: gloPM(" + strInstallationPath + ")");
                        ////clsValidations.CreateDesktoIcons("gloPM.lnk", strInstallationPath + "\\gloPM.exe", strInstallationPath + "\\gloPMXP.ico", 1, strInstallationPath, "Location: gloPM(" + strInstallationPath + ")");
                        clsValidations.CreateDesktoIcons("QPM.lnk", strInstallationPath + "\\gloPM.exe", strInstallationPath + "\\gloPMXP.ico", 0, strInstallationPath, "Location: gloPM(" + strInstallationPath + ")");
                        clsValidations.CreateDesktoIcons("QPM.lnk", strInstallationPath + "\\gloPM.exe", strInstallationPath + "\\gloPMXP.ico", 1, strInstallationPath, "Location: gloPM(" + strInstallationPath + ")");
                        //
                        clsValidations.Deleteshortcut("gloPM.lnk", 0);
                        clsValidations.Deleteshortcut("gloPM.lnk", 1);
                        //
                        break;

                    case "GLOEMR ADMIN":
                        ////clsValidations.CreateDesktoIcons("gloEMR Admin.lnk", strInstallationPath + "\\gloEMRAdmin.exe", strInstallationPath + "\\gloEMRAdmin.ico", 0, strInstallationPath, "Location: gloEMRAdmin (" + strInstallationPath + ")");
                        ////clsValidations.CreateDesktoIcons("gloEMR Admin.lnk", strInstallationPath + "\\gloEMRAdmin.exe", strInstallationPath + "\\gloEMRAdmin.ico", 1, strInstallationPath, "Location: gloEMRAdmin (" + strInstallationPath + ")");
                       clsValidations.CreateDesktoIcons("QEMR Admin.lnk", strInstallationPath + "\\gloEMRAdmin.exe", strInstallationPath + "\\gloEMRAdmin.ico", 0, strInstallationPath, "Location: gloEMRAdmin (" + strInstallationPath + ")");
                       clsValidations.CreateDesktoIcons("QEMR Admin.lnk", strInstallationPath + "\\gloEMRAdmin.exe", strInstallationPath + "\\gloEMRAdmin.ico", 1, strInstallationPath, "Location: gloEMRAdmin (" + strInstallationPath + ")");
                       //
                       clsValidations.Deleteshortcut("gloEMR Admin.lnk", 0);
                       clsValidations.Deleteshortcut("gloEMR Admin.lnk", 1);
                       //
                        break;

                    case "GLOPM ADMIN":
                        ////clsValidations.CreateDesktoIcons("gloPM Admin.lnk", strInstallationPath + "\\gloPMAdmin.exe", strInstallationPath + "\\gloPMAdminXP.ico", 0, strInstallationPath, "Location: gloPMAdmin (" + strInstallationPath + ")");
                        ////clsValidations.CreateDesktoIcons("gloPM Admin.lnk", strInstallationPath + "\\gloPMAdmin.exe", strInstallationPath + "\\gloPMAdminXP.ico", 1, strInstallationPath, "Location: gloPMAdmin (" + strInstallationPath + ")");
                        clsValidations.CreateDesktoIcons("QPM Admin.lnk", strInstallationPath + "\\gloPMAdmin.exe", strInstallationPath + "\\gloPMAdminXP.ico", 0, strInstallationPath, "Location: gloPMAdmin (" + strInstallationPath + ")");
                        clsValidations.CreateDesktoIcons("QPM Admin.lnk", strInstallationPath + "\\gloPMAdmin.exe", strInstallationPath + "\\gloPMAdminXP.ico", 1, strInstallationPath, "Location: gloPMAdmin (" + strInstallationPath + ")");
                        //
                        clsValidations.Deleteshortcut("gloPM Admin.lnk", 0);
                        clsValidations.Deleteshortcut("gloPM Admin.lnk", 1);
                        //
                        break;
                    case "CLAIM MANAGER":
                        clsValidations.CreateDesktoIcons("Claim Manager.lnk", strInstallationPath + "\\gloPMClaimService.exe", strInstallationPath + "\\Claim Status.ico", 0, strInstallationPath, "Location: gloPMClaimService (" + strInstallationPath + ")");
                        clsValidations.CreateDesktoIcons("Claim Manager.lnk", strInstallationPath + "\\gloPMClaimService.exe", strInstallationPath + "\\Claim Status.ico", 1, strInstallationPath, "Location: gloPMClaimService (" + strInstallationPath + ")");
                        break;
                }

            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while creating the shortcut icon on desktop for product '" + strProductName + "'.Error :" + ex.Message.ToString());
            }
        } //CreateShortCutIcon

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
                    clsGeneral.UpdateLog("Installed successfully " + strSwitch + "");
                    //MessageBox.Show("Installed gloEMR Client successfully", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                clsGeneral.UpdateLog("ExitCode " + oProcess.ExitCode + "");
            }
            catch (System.ArgumentNullException)
            {
            }
            catch (Exception ex)
            {

                clsGeneral.UpdateLog("Exception : While Executing Exe " + strSwitch + " " + ex.Message.ToString() + "");
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
        } //ExecuteInstallerExe

        public void CopyFolder(string sourceFolder, string destFolder)
        {
            string[] files = null;
            try
            {
                //pgInfo.SetmyStatusText("Copying data to the gloData Folder");
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);

                files = Directory.GetFiles(sourceFolder);
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
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while copying :" + ex.Message.ToString());
            }
            finally
            {
                if (files != null)
                {
                    files = null;
                }
            }
            Application.DoEvents();
        } //CopyFolder

        public void XcopyData(string Source, string Destination)
        {
            string strCommand = string.Empty;
            try
            {
                strCommand = " \"" + Source + "\" \"" + Destination + "\" /E /Y";
                clsGeneral.RunExe("xcopy", strCommand);

            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in XcopyData " + ex.Message.ToString() + "");
            }
        } //XcopyData

        public bool SetClientUpdateFlagInDB(int IsClientUpdate)
        {
            bool retValue = false;
            string strQuery = null;
            DBLayer objDbLayer = null;
            try
            {
                objDbLayer = new DBLayer(strConnectionString);

                //strQuery = "Update ClientSettings_Mst Set IsClientUpdateAvailable = " + IsClientUpdate + " where  [sMachineName] = '" + clsGeneral.gstrClientMachineName + "'";

                strQuery = "Update ClientSettings_Mst Set IsClientUpdateAvailable = " + IsClientUpdate + " where  [sMachineName] = '" + clsGeneral.gstrClientMachineName + "'";

                //if (ConfigurationManager.AppSettings["IsLogging"].ToString() == "1")
                //    clsGeneral.UpdateLog("strQuery: " + strQuery);

                objDbLayer.Connect(false);

                if (objDbLayer.Execute_Query(strQuery) > 0)
                {
                    retValue = true;
                }
                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                retValue = false;
                clsGeneral.UpdateLog("Error while setting client update flag. Error Message :" + ex.Message.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose(); objDbLayer = null;
                }
            }
            return retValue;
        } //SetClientUpdateFlagInDB

        public bool GetClientUpdateFlag()
        {
            bool retValue = false;
            string strQuery = null;
            DBLayer objDbLayer = null;
            object objTemp = null;
            try
            {
                //clsGeneral.UpdateLog("String:" + clsGeneral.GetConnectionString());
                objDbLayer = new DBLayer(strConnectionString);
                strQuery = "Select  IsClientUpdateAvailable from ClientSettings_Mst where [sMachineName] = '" + clsGeneral.gstrClientMachineName + "'";
                //strQuery = "Select  Count(nClientUpdateID) from ClientUpdateDetails where [sMachineName] = '" + clsGeneral.gstrClientMachineName + "' and IsClientUpdateAvailable = 1";
                //if (ConfigurationManager.AppSettings["IsLogging"].ToString() == "1")
                //    clsGeneral.UpdateLog("strQuery :" + strQuery);

                objDbLayer.Connect(false);

                objTemp = objDbLayer.ExecuteScalar_Query(strQuery);

                //if (ConfigurationManager.AppSettings["IsLogging"].ToString() == "1")
                //    clsGeneral.UpdateLog("Client Update Flag :" + objTemp.ToString());

                if (objTemp != null && Convert.ToString(objTemp) != "")
                {
                    retValue = Convert.ToBoolean(objTemp);
                }

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                retValue = false;
                clsGeneral.UpdateLog("Error while checking client update Available. Error Message :" + ex.Message.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose(); objDbLayer = null;
                }
            }
            return retValue;
        } //GetClientUpdateFlag

        //public static string GetInstallationPathForApplication(string strApplicationName)
        //{
        //    string strRetSuccess = string.Empty;
        //    try
        //    {
        //        switch (strApplicationName.ToUpper().Trim())
        //        {
        //            case "GLOEMR":
        //                strRetSuccess = GetRegistryValueBasedOnRegistryKey("EMRInstallationPath", clsRegistry.str32EMRKey, clsRegistry.str64EMRKey);
        //                break;

        //            case "GLOPM":
        //                strRetSuccess = GetRegistryValueBasedOnRegistryKey("PMInstallationPath", clsRegistry.str32PMKey, clsRegistry.str64PMKey);
        //                break;

        //            case "GLOEMRADMIN":
        //                strRetSuccess = GetRegistryValueBasedOnRegistryKey("EMRAdminPath", clsRegistry.str32EMRKey, clsRegistry.str64EMRKey);
        //                break;

        //            case "GLOPMADMIN":
        //                strRetSuccess = GetRegistryValueBasedOnRegistryKey("PMAdminPath", clsRegistry.str32PMKey, clsRegistry.str64PMKey);
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strRetSuccess = null;
        //    }
        //    finally
        //    {

        //    }
        //    return strRetSuccess;
        //}

        //private static string GetRegistryValueBasedOnRegistryKey(string strKeyName, string str32Key, string str64Key)
        //{
        //    string strRetSuccess = null;
        //    object objApplicationInstallPath = null;
        //    try
        //    {

        //        if (clsGeneral.CheckMachineStatus() == 0)
        //        {
        //            if (clsRegistry.CheckRegistryExists(str32Key))
        //            {
        //                objApplicationInstallPath = clsRegistry.GetRegistryValue(strKeyName, str32Key);
        //            }
        //        }
        //        else
        //        {
        //            if (clsRegistry.CheckRegistryExists(str64Key))
        //            {
        //                objApplicationInstallPath = clsRegistry.GetRegistryValue(strKeyName, str64Key);
        //            }
        //        }
        //        if (objApplicationInstallPath != null)
        //        {
        //            strRetSuccess = Convert.ToString(objApplicationInstallPath);
        //        }
        //        else
        //        {
        //            strRetSuccess = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strRetSuccess = null;
        //    }
        //    finally
        //    {

        //    }
        //    return strRetSuccess;
        //}


        //Return Value =0 : Fail , 1: Success , 2 : Retry.

        public void InstallUpdates(DataTable dtUpdateDetails, string strAppName = null)
        {
            // clsInstallUpdates objInstallUpdate = null;
            //float perProgressCnt = 0;
            string sUpdateId = null;
            string sProductCode = null;
            Int64 nClientUpdateID = 0;
            bool IsClientUpdateSuccessfully = true;
            int isUpdateInstalled = 1;
            string strQuery = null;
            int isUpdateInstallationInProgress = 0;
            DBLayer objDbLayer = null;
            string strUserName = null;
            string strMessage = null;
            try
            {
                //objInstallUpdate = new clsInstallUpdates();

                //this.Text = "Updates Installation Progress";
                clsGeneral.UpdateLog("Updates Installation Progress");
                clsGeneral.UpdateLog("Connection String :" + strConnectionString);
                objDbLayer = new DBLayer(strConnectionString);
                Application.DoEvents();

                if (dtUpdateDetails != null)
                {
                    if (dtUpdateDetails.Rows.Count > 0)
                    {
                        //perProgressCnt = 100 / dtUpdateDetails.Rows.Count - 1;

                        foreach (DataRow dr in dtUpdateDetails.Rows)
                        {
                            string strUpdateDownloadLocation = GetUpdateDownloadLocationFromUpdateId(dr["nUpdateId"].ToString());

                            string strUpdateProductName = GetProductTypeBasedOnUpdateId(dr["nUpdateId"].ToString());

                            sUpdateId = dr["nUpdateId"].ToString();

                            sProductCode = Convert.ToString(dr["sProductCode"]);

                            nClientUpdateID = Convert.ToInt64(dr["nClientUpdateID"]);

                            clsGeneral.UpdateLog("nClientUpdateID :" + nClientUpdateID);

                            if (!string.IsNullOrEmpty(strUpdateDownloadLocation.Trim()) && !string.IsNullOrEmpty(strUpdateProductName))
                            {
                                //pgbInstallUpdates.Value = 0;
                                strQuery = "Select IsUpdateInstallProgress from ClientUpdateDetails where nClientUpdateID =" + nClientUpdateID;

                                clsGeneral.UpdateLog("Query:" + strQuery);
                                objDbLayer.Connect(false);

                                isUpdateInstallationInProgress = Convert.ToInt32(objDbLayer.ExecuteScalar_Query(strQuery));

                                objDbLayer.Disconnect();

                                strUserName = WindowsIdentity.GetCurrent().Name;

                                if (isUpdateInstallationInProgress == 0)
                                {

                                    strQuery = "Update ClientUpdateDetails set IsUpdateInstallProgress = 1 ,sUserName = '" + strUserName + "' where nClientUpdateID =" + nClientUpdateID;

                                    clsGeneral.UpdateLog("Query:" + strQuery);

                                    objDbLayer.Connect(false);

                                    objDbLayer.Execute_Query(strQuery);

                                    objDbLayer.Disconnect();

                                    switch (strUpdateProductName.ToUpper())
                                    {
                                        case "GLOSUITE":
                                        case "GLOSUITECLIENT":


                                            if (strUpdateProductName.ToUpper() == "GLOSUITE")
                                                ApplyAdminUpdates(Convert.ToInt64(dr["nUpdateId"]));

                                            isUpdateInstalled = ApplygloSuiteUpdate(strUpdateDownloadLocation, Convert.ToInt64(sUpdateId), strAppName);

                                            if (isUpdateInstalled == 0)
                                            {
                                                UpdateClientSettings(nClientUpdateID, sProductCode, "FAILED", 0);
                                                IsClientUpdateSuccessfully = false;
                                            }
                                            else if (isUpdateInstalled == 1)
                                            {
                                                //clsGeneral.UpdateLog("Successfully Installed the Update :" + dr["nUpdateId"]);
                                                UpdateClientSettings(nClientUpdateID, sProductCode, "INSTALLED", 1);
                                            }
                                            else if (isUpdateInstalled == 2)
                                            {
                                                IsClientUpdateSuccessfully = false;
                                                clsGeneral.UpdateLog("gloSuite Update installation will be retry after closing all gloSuite product for Update ID :" + dr["nUpdateId"]);
                                                UpdateClientSettings(nClientUpdateID, sProductCode, "RETRY", 0);
                                            }
                                            break;
                                        case "GLOAUS":
                                            isUpdateInstalled = ApplyAUSServiceUpdate(strUpdateDownloadLocation, Convert.ToInt64(sUpdateId));
                                            if (isUpdateInstalled == 0)
                                            {
                                                clsGeneral.UpdateLog("Failed to Installed the Update :" + dr["nUpdateId"]);
                                                //break;
                                                UpdateClientSettings(nClientUpdateID, sProductCode, "FAILED", 0);
                                                IsClientUpdateSuccessfully = false;
                                            }
                                            else if (isUpdateInstalled == 1)
                                            {
                                                UpdateClientSettings(nClientUpdateID, sProductCode, "INSTALLED", 1);
                                            }
                                            else if (isUpdateInstalled == 2)
                                            {
                                                IsClientUpdateSuccessfully = false;
                                                clsGeneral.UpdateLog("gloSuite Update installation will be retry after closing all gloSuite product for Update ID :" + dr["nUpdateId"]);
                                                UpdateClientSettings(nClientUpdateID, sProductCode, "RETRY", 0);
                                            }
                                            break;

                                        case "HOTFIX":
                                            isUpdateInstalled = ApplyHotFix(strUpdateDownloadLocation, strAppName);
                                            break;
                                    }
                                }
                                else
                                {
                                    strQuery = "Select sUserName from ClientUpdateDetails where nClientUpdateID =" + nClientUpdateID;

                                    clsGeneral.UpdateLog("Query:" + strQuery);

                                    objDbLayer.Connect(false);

                                    strUserName = Convert.ToString(objDbLayer.ExecuteScalar_Query(strQuery));

                                    objDbLayer.Disconnect();

                                    strMessage = "gloSuite Update installation is in progress by user " + strUserName + ". Please close application and try again ";

                                    MessageBox.Show(strMessage, "gloClientUpdates Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    clsGeneral.UpdateLog(strMessage);
                                }
                            }
                        }

                    }
                }

                if (IsClientUpdateSuccessfully == false)
                {
                    clsGeneral.UpdateLog("Failed to installed Updates.");
                }
                else if (IsClientUpdateSuccessfully == true)
                {
                    clsGeneral.UpdateLog("Updates have been installed successfully.");
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                // isUpdateInstalledSuccessFully = false;
                isUpdateInstalled = 0;
                clsGeneral.UpdateLog("Exception in while Installing the Update " + ex.Message.ToString() + "");
                //this.Close();
            }
            finally
            {
                //if (objInstallUpdate != null)
                //{
                //    objInstallUpdate.Dispose();
                //    objInstallUpdate = null;
                //}

                if (dtUpdateDetails != null)
                {
                    dtUpdateDetails.Dispose();
                    dtUpdateDetails = null;
                }

                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                    objDbLayer = null;
                }
            }
        }

        public int ApplyHotFix(string strUpdateDownloadLocation, string strAppName = null)
        {
            int retSuccess = 0;
            DataTable dtHotFixUpdateDetails = null;
            DataSet dsHotFixUpdateDetails = null;
            string strUpdateListFilePath = string.Empty;
            string strProductName = string.Empty;
            string strProductUpdateType = string.Empty;
            string strProductUpdateRelatedto = string.Empty;
            string strUpdateFileName = string.Empty;
            string strUpdateLocation = string.Empty;
            //string strProductVersion = null;
            //clsInstallUpdates objInstallUpdates = null;
            //string strAlertMessage = null;
            //string strIsModified = null;
            string strArgument = null;
            try
            {
                //objInstallUpdates = new clsInstallUpdates();

                //strAlertMessage = "Important updates are available. Please save work and close out of all gloSuite client and admin application so updates can be applied.";

                strUpdateListFilePath = strUpdateDownloadLocation + "gloSuiteHotfixUpdate.exe";

                clsGeneral.UpdateLog("Update FilePath :" + strUpdateListFilePath);

                strArgument = strAppName;

                //if(clsGeneral.RunExe(strUpdateListFilePath,strArgument))
                //{

                //}

                if (ExecuteInstallerExe(strUpdateListFilePath, strArgument))
                {
                    clsAus.gloAusLog("Update installation process completed.");
                }
                //dsHotFixUpdateDetails = new DataSet();

                //dsHotFixUpdateDetails.ReadXml(strUpdateListFilePath);

                //dtHotFixUpdateDetails = dsHotFixUpdateDetails.Tables["File"];

                //if (dtHotFixUpdateDetails != null)
                //{
                //    if (dtHotFixUpdateDetails.Rows.Count > 0)
                //    {
                //        float perProgressCnt = (100 / dtHotFixUpdateDetails.Rows.Count);

                //        //if (objInstallUpdates.CheckInstanceRunning("gloEMR", strAlertMessage) && objInstallUpdates.CheckInstanceRunning("gloPM", strAlertMessage) && objInstallUpdates.CheckInstanceRunning("gloEMRAdmin", strAlertMessage) && objInstallUpdates.CheckInstanceRunning("gloPMAdmin", strAlertMessage) && objInstallUpdates.CheckInstanceRunning("gloPMClaimService", strAlertMessage) && objInstallUpdates.CheckInstanceRunning("gloSuiteClient", strAlertMessage))
                //        if (CheckInstanceRunning(strAlertMessage, strAppName))
                //        {
                //            if (dsHotFixUpdateDetails.Tables["gloHotFix"].Rows[0]["Version"] != null)
                //            {
                //                strProductVersion = Convert.ToString(dsHotFixUpdateDetails.Tables["gloHotFix"].Rows[0]["Version"]);
                //            }

                //            foreach (DataRow dr in dtHotFixUpdateDetails.Rows)
                //            {
                //                strProductName = Convert.ToString(dr["ProductName"]).ToUpper();
                //                strProductUpdateType = Convert.ToString(dr["ProductUpdateType"]).Trim().ToUpper();
                //                strProductUpdateRelatedto = Convert.ToString(dr["ProductUpdateRelatedTo"]).ToUpper();
                //                strUpdateFileName = Convert.ToString(dr["FileName"]).Trim();
                //                //strUpdateLocation = Convert.ToString(dr["sUpdateLocation"]).Trim();

                //                if (dtHotFixUpdateDetails.Columns.Contains("IsModified"))
                //                {
                //                    strIsModified = Convert.ToString(dr["IsModified"]);
                //                }
                //                else
                //                    strIsModified = "No";

                //                if (strProductUpdateRelatedto == "CLIENT" || strProductUpdateRelatedto == "CLIENTSERVER")
                //                {
                //                    if (strProductUpdateType == "FILE")
                //                    {
                //                        string[] strProductNameArr = strProductName.Split('|');

                //                        if (strProductNameArr.Length > 0)
                //                        {
                //                            for (int i = 0; i < strProductNameArr.Length; i++)
                //                            {
                //                                if (InstallFilePatch(strProductNameArr[i], strUpdateFileName, strUpdateDownloadLocation, strProductVersion, strIsModified))
                //                                    retSuccess = 1;
                //                                else
                //                                {
                //                                    retSuccess = 0;
                //                                    break;
                //                                }
                //                            }
                //                        }
                //                    }
                //                    else if (strProductUpdateType == "MSI")
                //                    {
                //                        if (InstallMSIPatch(strProductName, strUpdateFileName, strUpdateDownloadLocation, strProductVersion, strIsModified))
                //                        {
                //                            retSuccess = 1;
                //                        }
                //                        else
                //                        {
                //                            retSuccess = 0;
                //                            break;
                //                        }
                //                    }
                //                    //pgbInstallUpdates.Value = pgbInstallUpdates.Value + Convert.ToInt32(perProgressCnt);
                //                    Application.DoEvents();
                //                }
                //                else
                //                {
                //                    retSuccess = 1;
                //                    clsGeneral.UpdateLog("Updates are only pushed for server and not applicable to client machine.");
                //                }
                //            }

                //            // pgbInstallUpdates.Value = 100;
                //            Application.DoEvents();

                //        }
                //        else
                //        {
                //            retSuccess = 2;
                //            clsGeneral.UpdateLog("Updates are not installed as gloSuite product is in Running state.");
                //        }
                //    }
                //}
                //else
                //{
                //    retSuccess = 0;
                //}

            }
            catch (Exception ex)
            {
                retSuccess = 0;
                clsGeneral.UpdateLog("Error while applying the Hot Fix update for product '" + strProductName + "' and Updated type : '" + strProductUpdateType + "':" + ex.Message.ToString());
            }
            finally
            {
                if (dtHotFixUpdateDetails != null)
                {
                    dtHotFixUpdateDetails.Dispose();
                    dtHotFixUpdateDetails = null;
                }

                if (dsHotFixUpdateDetails != null)
                {
                    dsHotFixUpdateDetails.Dispose();
                    dsHotFixUpdateDetails = null;
                }
                //if (objInstallUpdates != null)
                //{
                //    objInstallUpdates.Dispose();
                //    objInstallUpdates = null;
                //}

            }
            return retSuccess;
        } //ApplyHotFix

        public int ApplygloSuiteUpdate(string strUpdateLocation, Int64 nUpdateId, string strAppName = null)
        {
            int retSuccess = 0;
            string strInstallerPath = null;
            string strPathToTempFolder = null;
            string strDestination = null;
            string strArgument = null;
            //string strAlertMessage = null;
            List<InstalledApplicationPath> lstInstalledVersion = null;
            string strAppInstalledPath = null;
            bool isEMRExist = false;
            bool isPMExist = false;
            char[] charsToTrim = { '\\' };
            try
            {
                //objInstallUpdate = new clsInstallUpdates();

                //strAlertMessage = "Important updates are available. Please save work and close out of application so updates can be applied.";

                // if (objInstallUpdate.CheckInstanceRunning("gloEMR", strAlertMessage) && objInstallUpdate.CheckInstanceRunning("gloPM", strAlertMessage) && objInstallUpdate.CheckInstanceRunning("gloEMRAdmin", strAlertMessage) && objInstallUpdate.CheckInstanceRunning("gloPMAdmin", strAlertMessage) && objInstallUpdate.CheckInstanceRunning("gloPMClaimService", strAlertMessage))
                //if (CheckInstanceRunning(strAlertMessage, strAppName))
                //{
                clsGeneral.UpdateLog("Applying the gloSuite Update");

                strPathToTempFolder = Path.GetTempPath().TrimEnd(charsToTrim);
                clsGeneral.UpdateLog("Temporary folder path :" + strPathToTempFolder);

                strDestination = Path.Combine(strPathToTempFolder, "gloSuite");
                Application.DoEvents();

                clsGeneral.UpdateLog("Copying gloSuite client installer package to temporary location.");

                try
                {
                    if (Directory.Exists(strDestination))
                        Directory.Delete(strDestination, true);
                }
                catch (Exception ex)
                {
                    clsGeneral.UpdateLog("Error while deleting existing directory. Error Message :" + ex.Message.ToString());
                }

                CopyFolder(strUpdateLocation, strDestination);
                //pgbInstallUpdates.Value = pgbInstallUpdates.Value + 20;
                Application.DoEvents();

                //strInstallerPath = strDestination.TrimEnd('\\') + "\\glosuiteclient.exe";
                strInstallerPath = Path.Combine(strDestination, "glosuiteclient.exe");
                clsGeneral.UpdateLog("gloSuite client installer exe path:" + strInstallerPath);
                Application.DoEvents();

                if (File.Exists(strInstallerPath))
                {
                    // pgbInstallUpdates.Value = pgbInstallUpdates.Value + 20;
                    Application.DoEvents();
                    clsGeneral.UpdateLog("Path to installer exe exists " + strInstallerPath + "");

                    lstInstalledVersion = GetApplicationInstallVersion(ConfigurationManager.AppSettings["gloEMR"].ToString().Trim());

                    if (lstInstalledVersion != null)
                    {
                        if (lstInstalledVersion.Count > 0)
                        {
                            for (int i = 0; i < lstInstalledVersion.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(lstInstalledVersion[i].InstallPath))
                                {
                                    strAppInstalledPath = lstInstalledVersion[i].InstallPath;
                                    break;
                                }
                            }
                            if (!string.IsNullOrEmpty(strAppInstalledPath))
                            {
                                if (File.Exists(Path.Combine(strAppInstalledPath, "gloEMR.exe")))
                                {
                                    isEMRExist = true;
                                }
                                Application.DoEvents();
                            }
                        }
                    }
                    else
                    {
                        clsGeneral.UpdateLog("gloEMR is not installed.");
                    }

                    if (lstInstalledVersion != null)
                    {
                        lstInstalledVersion.Clear();
                    }

                    // pgbInstallUpdates.Value = pgbInstallUpdates.Value + 10;
                    Application.DoEvents();
                    lstInstalledVersion = GetApplicationInstallVersion(ConfigurationManager.AppSettings["gloPM"].ToString().Trim());
                    if (lstInstalledVersion != null)
                    {
                        if (lstInstalledVersion.Count > 0)
                        {
                            for (int i = 0; i < lstInstalledVersion.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(lstInstalledVersion[i].InstallPath))
                                {
                                    strAppInstalledPath = lstInstalledVersion[i].InstallPath;
                                    break;
                                }
                            }
                            if (!string.IsNullOrEmpty(strAppInstalledPath))
                            {
                                if (File.Exists(Path.Combine(strAppInstalledPath, "gloPM.exe")))
                                {
                                    isPMExist = true;
                                }
                                Application.DoEvents();
                            }
                        }
                    }
                    else
                    {
                        clsGeneral.UpdateLog("gloPM is not installed.");
                    }
                    if (lstInstalledVersion != null)
                    {
                        lstInstalledVersion.Clear();
                    }

                    if (isEMRExist && isPMExist)
                    {
                        strArgument = "/silent  /suite " + nUpdateId;
                    }
                    else if (isEMRExist)
                    {
                        strArgument = "/silent  /gloEMR " + nUpdateId;
                    }
                    else
                    {
                        strArgument = "/silent  /gloPM " + nUpdateId;
                    }

                    Application.DoEvents();
                    clsGeneral.UpdateLog("strArgument :" + strArgument);
                    //if (objInstallUpdate.ExecuteInstallerExe(strInstallerPath, strArgument))
                    if (clsGeneral.RunExe(strInstallerPath, strArgument))
                    {
                        clsGeneral.UpdateLog("Installation completed successfully.");
                        retSuccess = 1;
                    }
                    else
                    {
                        clsGeneral.UpdateLog("Installation Failed.");
                        retSuccess = 0;
                    }

                }
                else
                {
                    clsGeneral.UpdateLog("Path to installer exe does not exist " + strInstallerPath + "");
                    retSuccess = 0;
                }

                //pgbInstallUpdates.Value = 100;
                Application.DoEvents();

                //}
                //else
                //{
                //    retSuccess = 2;
                //    clsGeneral.UpdateLog("Updates are not installed as gloSuite product is in Running state.");
                //    Application.DoEvents();
                //}
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while application gloSuiteClient Update :" + ex.Message.ToString());
                retSuccess = 0;
            }
            finally
            {
                //if (objInstallUpdate != null)
                //{
                //    objInstallUpdate.Dispose();
                //    objInstallUpdate = null;
                //}
            }
            return retSuccess;
        } //ApplygloSuiteUpdate

        public int ApplyAUSServiceUpdate(string strUpdateLocation, Int64 nUpdateId)
        {
            int retSuccess = 0;
            string strInstallerPath = null;
            string strPathToTempFolder = null;
            string strDestination = null;
            string strArgument = null;
            //string strAlertMessage = null;
            //List<InstalledApplicationPath> lstInstalledVersion = null;
            //string strAppInstalledPath = null;
            //bool isEMRExist = false;
            //bool isPMExist = false;
            char[] charsToTrim = { '\\' };
            try
            {
                //objInstallUpdate = new clsInstallUpdates();

                //strAlertMessage = "Important updates are available. Please save work and close out of application so updates can be applied.";

                //// if (objInstallUpdate.CheckInstanceRunning("gloEMR", strAlertMessage) && objInstallUpdate.CheckInstanceRunning("gloPM", strAlertMessage) && objInstallUpdate.CheckInstanceRunning("gloEMRAdmin", strAlertMessage) && objInstallUpdate.CheckInstanceRunning("gloPMAdmin", strAlertMessage) && objInstallUpdate.CheckInstanceRunning("gloPMClaimService", strAlertMessage))
                //if (CheckInstanceRunning(strAlertMessage))
                //{
                clsGeneral.UpdateLog("Applying the gloAUS Service Update");

                strPathToTempFolder = Path.GetTempPath().TrimEnd(charsToTrim);
                clsGeneral.UpdateLog("Temporary folder path :" + strPathToTempFolder);

                strDestination = strPathToTempFolder + "\\gloAUSUpdate";
                Application.DoEvents();

                clsGeneral.UpdateLog("Copying gloAUS Service installer to temporary location.");

                try
                {
                    if (Directory.Exists(strDestination))
                        Directory.Delete(strDestination);
                }
                catch (Exception ex)
                {
                    clsGeneral.UpdateLog("Error while deleting existing directory. Error Message :" + ex.Message.ToString());
                }

                CopyFolder(strUpdateLocation, strDestination);
                //pgbInstallUpdates.Value = pgbInstallUpdates.Value + 20;
                Application.DoEvents();

                strInstallerPath = strDestination + "\\gloAUSUpdate.exe";
                clsGeneral.UpdateLog("gloAUS Service installer exe path:" + strInstallerPath);
                Application.DoEvents();

                if (File.Exists(strInstallerPath))
                {
                    // pgbInstallUpdates.Value = pgbInstallUpdates.Value + 20;
                    Application.DoEvents();

                    clsGeneral.UpdateLog("Path to gloAUS Service installer exe exists " + strInstallerPath + "");

                    strArgument = clsGeneral.gstrSQLServerName + " " + clsGeneral.gstrDatabaseName + " " + clsGeneral.gstrUserId + " " + clsGeneral.gstrPassword + " " + nUpdateId + "  Client";

                    Application.DoEvents();
                    //if (ConfigurationManager.AppSettings["IsLogging"].ToString() == null)
                    //{
                    //    clsGeneral.UpdateLog("strArgument :" + strArgument);
                    //}

                    //if (objInstallUpdate.ExecuteInstallerExe(strInstallerPath, strArgument))
                    if (clsGeneral.RunExe(strInstallerPath, strArgument))
                    {
                        clsGeneral.UpdateLog("Installation completed successfully.");
                        retSuccess = 1;
                    }
                    else
                    {
                        clsGeneral.UpdateLog("Installation Failed.");
                        retSuccess = 0;
                    }

                }
                else
                {
                    clsGeneral.UpdateLog("Path to installer exe does not exist " + strInstallerPath + "");
                    retSuccess = 0;
                }

                //pgbInstallUpdates.Value = 100;
                Application.DoEvents();

                //}
                //else
                //{
                //    retSuccess = 2;
                //    clsGeneral.UpdateLog("Updates are not installed as gloSuite product is in Running state.");
                //    Application.DoEvents();
                //}
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while application gloSuiteClient Update :" + ex.Message.ToString());
                retSuccess = 0;
            }
            finally
            {
                //if (objInstallUpdate != null)
                //{
                //    objInstallUpdate.Dispose();
                //    objInstallUpdate = null;
                //}
            }
            return retSuccess;
        } //ApplygloSuiteUpdate

        public void ApplyAdminUpdates(Int64 nUpdateId)
        {
            string strAppInstalledPath = null;
            string strAdminMsiLocation = null;
            //clsInstallUpdates objInstallUpdate = null;
            List<InstalledApplicationPath> lstInstalledVersion = null;
            try
            {
                //objInstallUpdate = new clsInstallUpdates();

                //pgbInstallUpdates.Value = pgbInstallUpdates.Value + 10;
                Application.DoEvents();
                strAdminMsiLocation = GetAdminUpdateLocationBasedOnId(nUpdateId);

                lstInstalledVersion = GetApplicationInstallVersion(ConfigurationManager.AppSettings["GLOEMRADMIN"].ToString().Trim());

                if (lstInstalledVersion != null)
                {
                    if (lstInstalledVersion.Count > 0)
                    {
                        for (int i = 0; i < lstInstalledVersion.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(lstInstalledVersion[i].InstallPath))
                            {
                                strAppInstalledPath = lstInstalledVersion[i].InstallPath;
                                break;
                            }
                        }
                        if (!string.IsNullOrEmpty(strAppInstalledPath))
                        {
                            strAdminMsiLocation = strAdminMsiLocation.ToLower().Replace("glosuiteadmin.exe", "");
                            if (InstallAdminMSIPatch("gloEMRAdmin", "gloEMRAdminInstall.msi", strAdminMsiLocation, strAppInstalledPath))
                                clsGeneral.UpdateLog("gloEMRAdmin installed successfully.");
                            else
                                clsGeneral.UpdateLog("gloEMRAdmin not installed successfully.");

                            //pgbInstallUpdates.Value = pgbInstallUpdates.Value + 10;
                            Application.DoEvents();
                        }
                    }
                }
                else
                {
                    clsGeneral.UpdateLog("gloEMRAdmin is not installed.");
                }
                if (lstInstalledVersion != null)
                {
                    lstInstalledVersion.Clear();
                }
                // pgbInstallUpdates.Value = pgbInstallUpdates.Value + 10;
                Application.DoEvents();
                lstInstalledVersion = GetApplicationInstallVersion(ConfigurationManager.AppSettings["GLOPMADMIN"].ToString().Trim());
                if (lstInstalledVersion != null)
                {
                    if (lstInstalledVersion.Count > 0)
                    {
                        for (int i = 0; i < lstInstalledVersion.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(lstInstalledVersion[i].InstallPath))
                            {
                                strAppInstalledPath = lstInstalledVersion[i].InstallPath;
                                break;
                            }
                        }
                        if (!string.IsNullOrEmpty(strAppInstalledPath))
                        {
                            strAdminMsiLocation = strAdminMsiLocation.Replace("gloSuiteAdmin.exe", "");
                            if (InstallAdminMSIPatch("gloPMAdmin", "gloPMAdminInstall.msi", strAdminMsiLocation, strAppInstalledPath))
                                clsGeneral.UpdateLog("gloPMAdmin installed successfully.");
                            else
                                clsGeneral.UpdateLog("gloPMAdmin not installed successfully.");

                            //pgbInstallUpdates.Value = pgbInstallUpdates.Value + 10;
                            Application.DoEvents();
                        }
                    }
                }
                else
                {
                    clsGeneral.UpdateLog("gloPMAdmin is not installed.");
                }
                if (lstInstalledVersion != null)
                {
                    lstInstalledVersion.Clear();
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while update Admin on Client Machine. Error Message :" + ex.Message.ToString());
            }
        } //ApplyAdminUpdates

        public string GetAdminUpdateLocationBasedOnId(long nUpdateId)
        {
            string strDownloadPath = null;
            string strQuery = null;
            DBLayer objDblayer = null;
            DataTable dtTemp = null;
            try
            {
                strQuery = "select sUpdateMode from [ClinicUpdates_Settings] where [nUpdateID] = " + nUpdateId + "";
                objDblayer = new DBLayer(strConnectionString);
                objDblayer.Connect(false);
                objDblayer.Retrive_Query(strQuery, out dtTemp);

                if (dtTemp != null)
                {
                    if (dtTemp.Rows.Count > 0)
                    {
                        strDownloadPath = Convert.ToString(dtTemp.Rows[0][0]);
                    }
                }
                objDblayer.Disconnect();
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while retiriving the download location for update id :" + nUpdateId + ".Error message:" + ex.Message.ToString());
                objDblayer.Disconnect();
            }
            finally
            {
                if (objDblayer != null)
                {
                    objDblayer.Dispose();
                    objDblayer = null;
                }
                if (dtTemp != null)
                {
                    dtTemp.Dispose();
                    dtTemp = null;
                }
            }
            return strDownloadPath;

        } //GetAdminUpdateLocationBasedOnId

        public string GetUpdateDownloadLocationFromUpdateId(string strUpdateid)
        {
            string strDownloadPath = null;
            string strQuery = null;
            DBLayer objDblayer = null;
            DataTable dtTemp = null;
            try
            {
                strQuery = "select sUpdateLocation from [ClinicUpdates_Settings] where [nUpdateID] = '" + strUpdateid + "'";
                objDblayer = new DBLayer(strConnectionString);
                objDblayer.Connect(false);
                objDblayer.Retrive_Query(strQuery, out dtTemp);

                if (dtTemp != null)
                {
                    if (dtTemp.Rows.Count > 0)
                    {
                        strDownloadPath = Convert.ToString(dtTemp.Rows[0][0]);
                    }
                }
                objDblayer.Disconnect();
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while retiriving the download location for update id :" + strUpdateid + ".Error message:" + ex.Message.ToString());
                objDblayer.Disconnect();
            }
            finally
            {
                if (objDblayer != null)
                {
                    objDblayer.Dispose();
                    objDblayer = null;
                }
            }
            return strDownloadPath;
        } //GetUpdateDownloadLocationFromUpdateId

        public string GetProductTypeBasedOnUpdateId(string strUpdateid)
        {
            string strProductType = null;
            string strQuery = null;
            DBLayer objDblayer = null;
            DataTable dtTemp = null;
            try
            {
                strQuery = "select sProductName from [ClinicUpdates_Settings] where [nUpdateID] = '" + strUpdateid + "'";
                objDblayer = new DBLayer(strConnectionString);
                objDblayer.Connect(false);
                objDblayer.Retrive_Query(strQuery, out dtTemp);

                if (dtTemp != null)
                {
                    if (dtTemp.Rows.Count > 0)
                    {
                        strProductType = Convert.ToString(dtTemp.Rows[0][0]);
                    }
                }
                objDblayer.Disconnect();
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while retiriving the download location for update id :" + strUpdateid + ".Error message:" + ex.Message.ToString());
                objDblayer.Disconnect();
            }
            finally
            {
                if (objDblayer != null)
                {
                    objDblayer.Dispose();
                    objDblayer = null;
                }
            }
            return strProductType;
        } //GetProductTypeBasedOnUpdateId

        #endregion "Function"

        #region "Dispose Method"

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } //Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        } //Dispose

        #endregion "Dispose Method"

    } //clsInstallUpdates

    public class InstalledApplicationPath
    {
        public InstalledApplicationPath(RegistryKey uninstallKey, string keyName)
        {
            RegistryKey key = uninstallKey.OpenSubKey(keyName, false);
            if (key != null)
            {
                try
                {
                    var strdisplayName = key.GetValue("DisplayName");
                    if (strdisplayName != null)
                        DisplayName = strdisplayName.ToString();

                    var strInstallLocation = key.GetValue("InstallLocation");
                    if (strInstallLocation != null)
                        InstallPath = strInstallLocation.ToString();

                    var strUnistallString = key.GetValue("UninstallString");
                    if (strUnistallString != null)
                        UninstallString = strUnistallString.ToString();

                    var strDisplayVersionString = key.GetValue("DisplayVersion");
                    if (strDisplayVersionString != null)
                        DisplayVersion = strDisplayVersionString.ToString();
                }
                finally
                {
                    try
                    {
                        key.Close();
                        key.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
        } //InstalledApplicationPath

        public string DisplayName { get; set; }

        public string InstallPath { get; set; }

        public string UninstallString { get; set; }

        public string DisplayVersion { get; set; }
    } //InstalledApplicationPath
}
