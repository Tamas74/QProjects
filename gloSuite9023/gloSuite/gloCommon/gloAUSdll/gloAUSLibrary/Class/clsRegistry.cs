using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace gloAUSLibrary
{
    public static class clsRegistry
    {
        public static string str64EMRKey = "Software\\Wow6432Node\\gloEMR";
        public static string str32EMRKey = "Software\\gloEMR";

        public static string str32PMKey = "Software\\gloPM";
        public static string str64PMKey = "Software\\Wow6432Node\\gloPM";

        public static string gStrSqlServer = "SQLSERVER";
        public static string gStrDatabase = "DATABASE";
        public static string gStrUserName = "SQLUSER";
        public static string gStrPassword = "SQLPASSWORD";
        public static string gStrServiceInterval = "CLIENTUPDATESERVICEINTERVAL";
        public static string gStrClientUpdateAvailable = "IsClientUpdateAvailable";
        public static string gStrClientInstallationLog = "ClientServiceInstallationLog";
        public static string gStrIsLoggingEnable = "IsClientAUSLoggingEnable";
        private static RegistryKey regkey = null;
        private static RegistryKey regPMkey = null;
        private static RegistryKey Objlocalregkey = null;
        private static RegistryKey ObjlocalPMregkey = null;

        public static string GetRegistryValue(string strValue, string strKey)
        {
            string value = string.Empty;
            //RegistryKey oKey = Registry.LocalMachine.OpenSubKey(strKey, true);
            RegistryKey oKey = Registry.CurrentUser.OpenSubKey(strKey, true);
            try
            {
                if (oKey != null)
                {
                    if ((oKey.GetValue(strValue) == null) == false)
                    {
                        value = oKey.GetValue(strValue).ToString().Trim();
                    }
                    else
                    {
                        value = null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                clsGeneral.UpdateLog("Exception in GetRegistryValue " + ex.Message.ToString() + "");
            }
            finally
            {
                if (oKey != null)
                {
                    try
                    {
                        oKey.Close();
                        oKey.Dispose();
                    }
                    catch
                    {
                    }
                    oKey = null;
                }

            }
            return value;
        }

        public static int _type = CheckMachineStatus();

        public static void OpenSubKey(string SubKey, string SubKeyVersion, Boolean flag)
        {
            try
            {
                regkey = Registry.CurrentUser.OpenSubKey(SubKey, flag);
                if (regkey == null)
                {
                    CopyValueLocalMachineToCurrentUserRegistry(SubKey, SubKeyVersion);
                }
                else
                {
                    clsGeneral.UpdateLog("Migrating  setting from Local Machine to Current User.");
                    MigrateClientUpdateRegistryValueLocalMachineToCurrentUser(SubKey, SubKeyVersion);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in OpenSubKey with 2 parameter" + ex.Message.ToString() + "");
            }

        } //OpenSubKey

        //public static bool OpenSubKey(string SubKey, Boolean flag, string SubLocalKeyVer = null)
        //{
        //    bool retStatus = false;
        //    try
        //    {

        //        regkey = Registry.CurrentUser.OpenSubKey(SubKey, flag);
        //        if (regkey == null)
        //        {
        //            RegistryKey lregkey = Registry.LocalMachine.OpenSubKey(SubKey, flag);
        //            if (lregkey != null)
        //            {
        //                regkey = Registry.CurrentUser.CreateSubKey(SubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
        //                if (regkey != null)
        //                {
        //                    string[] keyNames = lregkey.GetValueNames();
        //                    foreach (string keyName in keyNames)
        //                    {
        //                        regkey.SetValue(keyName, lregkey.GetValue(keyName));
        //                    }
        //                }
        //                lregkey.Close();
        //            }
        //        }
        //        else
        //        {
        //            Objlocalregkey = Registry.LocalMachine.OpenSubKey(SubKey, flag);
        //        }

        //        if (regkey != null)
        //            retStatus = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = false;
        //        clsGeneral.UpdateLog("Exception in OpenSubKey with 3 paramter " + ex.Message.ToString() + "");
        //    }
        //    return retStatus;
        //} //OpenSubKey

        public static bool OpenSubKey(string SubKey, Boolean flag, string SubLocalKeyVer = null)
        {
            bool retStatus = false;
            try
            {
                regkey = Registry.CurrentUser.OpenSubKey(SubKey, flag);

                if (SubLocalKeyVer != null)
                    regPMkey = Registry.CurrentUser.OpenSubKey(SubLocalKeyVer, flag);

                Objlocalregkey = Registry.LocalMachine.OpenSubKey(SubKey, flag);

                if (SubLocalKeyVer != null)
                    ObjlocalPMregkey = Registry.LocalMachine.OpenSubKey(SubLocalKeyVer, flag);

                if (regkey != null || Objlocalregkey != null)
                    retStatus = true;
                else if (regPMkey != null || ObjlocalPMregkey != null)
                    retStatus = true;

            }
            catch (Exception ex)
            {
                retStatus = false;
                clsGeneral.UpdateLog("Exception in OpenSubKey with 3 paramter " + ex.Message.ToString() + "");
            }
            return retStatus;
        } //OpenSubKey

        public static void CopyValueLocalMachineToCurrentUserRegistry(string SubKey, string SubKeyVersion)
        {
            RegistryKey lregkey = null;
            try
            {
                lregkey = Registry.LocalMachine.OpenSubKey(SubKeyVersion, false);
                if (lregkey != null)
                {
                    #region "Old code used for referring current user registry"
                    //regkey = Registry.CurrentUser.CreateSubKey(SubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                    //if (regkey != null)
                    //{
                    //    string[] keyNames = lregkey.GetValueNames();
                    //    foreach (string keyName in keyNames)
                    //    {
                    //        regkey.SetValue(keyName, lregkey.GetValue(keyName));
                    //    }
                    //}
                    #endregion "Old code used for referring current user registry"

                    string[] keyNames = lregkey.GetValueNames();
                    for (int i = 0; i < keyNames.Length; i++)
                    {
                        if (lregkey.GetValue(keyNames[i]) != null)
                        {
                            switch (keyNames[i].ToUpper())
                            {
                                case "SQLSERVER":
                                    clsGeneral.gstrSQLServerName = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Server Nam:" + clsGeneral.gstrSQLServerName);
                                    break;
                                case "DATABASE":
                                    clsGeneral.gstrDatabaseName = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Database:" + clsGeneral.gstrDatabaseName);
                                    break;
                                case "SQLUSER":
                                    clsGeneral.gstrUserId = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("User id:" + clsGeneral.gstrUserId);
                                    break;
                                case "SQLPASSWORD":
                                    clsGeneral.gstrPassword = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Password:" + clsGeneral.gstrPassword);
                                    break;
                                case "CLIENTUPDATESERVICEINTERVAL":
                                    clsGeneral.time_INTERVAL = Convert.ToInt16(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Interval :" + clsGeneral.time_INTERVAL);
                                    break;
                                //case "ISCLIENTUPDATEAVAILABLE":
                                //    clsGeneral.gIsUpdateAvailable = Convert.ToBoolean(keyNames[i]);
                                //    clsGeneral.UpdateLog("Update Flag :" + clsGeneral.gIsUpdateAvailable);
                                //    break;
                                case "CLIENTSERVICEINTALLATIONLOG":
                                    clsGeneral.strServiceLogPath = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    // clsGeneral.UpdateLog("Install Log :" + clsGeneral.strServiceLogPath);
                                    break;
                                case "ISCLIENTAUSLOGGINGENABLE":
                                    clsGeneral.isLoggingEnable = Convert.ToBoolean(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Logging Enable :" + clsGeneral.isLoggingEnable);
                                    break;
                            }
                        }
                    }
                    if (lregkey != null)
                    {
                        try
                        {
                            lregkey.Close();
                            lregkey.Dispose();
                        }
                        catch
                        {
                        }
                        lregkey = null;
                    }
                   
                }
                else
                {
                    regkey = Registry.CurrentUser.CreateSubKey(SubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception while copying value from Local to Current user :" + ex.Message.ToString() + "");
            }
            finally
            {
                if (lregkey != null)
                {
                    try
                    {
                        lregkey.Close();
                        lregkey.Dispose();
                    }
                    catch
                    {
                    }
                    lregkey = null;
                }
            }
        } //CopyValueLocalMachineToCurrentUserRegistry

        public static void MigrateClientUpdateRegistryValueLocalMachineToCurrentUser(string SubKey, string SubKeyVersion)
        {
            RegistryKey lregkey = null;

            #region "Variable used when refered for current user registry"
            //object objServerName = null;
            //object objDatabasename = null;
            //object objSQLUsername = null;
            //object objSQLPassword = null;
            //object objClientUpdateServiceInterval = null;
            //object objClientInstallationLog = null;
            //object objIsClientLoggingEnable = null;
            #endregion "Variable used when refered for current user registry"

            try
            {
                lregkey = Registry.LocalMachine.OpenSubKey(SubKeyVersion, false);
                if (lregkey != null)
                {
                    #region  "old code referring current User registry"
                    //objServerName = clsRegistry.GetRegistryValue(clsRegistry.gStrSqlServer);
                    //objDatabasename = clsRegistry.GetRegistryValue(clsRegistry.gStrDatabase);
                    //objSQLUsername = clsRegistry.GetRegistryValue(clsRegistry.gStrUserName);
                    //objSQLPassword = clsRegistry.GetRegistryValue(clsRegistry.gStrPassword);

                    ////clsGeneral.UpdateLog("Password : " + Convert.ToString(objSQLPassword));

                    //objClientUpdateServiceInterval = clsRegistry.GetRegistryValue(clsRegistry.gStrServiceInterval);
                    //objClientInstallationLog = clsRegistry.GetRegistryValue(clsRegistry.gStrClientInstallationLog);
                    //objIsClientLoggingEnable = clsRegistry.GetRegistryValue(clsRegistry.gStrIsLoggingEnable);
                    //if (objServerName == null)
                    //{
                    //    regkey.SetValue(clsRegistry.gStrSqlServer, lregkey.GetValue(clsRegistry.gStrSqlServer));
                    //}

                    //if (objDatabasename == null)
                    //{
                    //    regkey.SetValue(clsRegistry.gStrDatabase, lregkey.GetValue(clsRegistry.gStrDatabase));
                    //}

                    //if (objSQLUsername == null)
                    //{
                    //    regkey.SetValue(clsRegistry.gStrUserName, lregkey.GetValue(clsRegistry.gStrUserName));
                    //}
                    ////else
                    ////    clsGeneral.UpdateLog("Username already exist");

                    //if (objSQLPassword == null)
                    //{
                    //    //clsGeneral.UpdateLog("Local Machine Password:" + lregkey.GetValue(clsRegistry.gStrPassword));
                    //    regkey.SetValue(clsRegistry.gStrPassword, lregkey.GetValue(clsRegistry.gStrPassword));
                    //}
                    ////else
                    ////    clsGeneral.UpdateLog("Pass already exist");

                    //if (objClientUpdateServiceInterval == null)
                    //{
                    //    regkey.SetValue(clsRegistry.gStrServiceInterval, lregkey.GetValue(clsRegistry.gStrServiceInterval));
                    //}
                    //if (objClientInstallationLog == null)
                    //{
                    //    regkey.SetValue(clsRegistry.gStrClientInstallationLog, lregkey.GetValue(clsRegistry.gStrClientInstallationLog));
                    //}
                    //if (objIsClientLoggingEnable == null)
                    //{
                    //    regkey.SetValue(clsRegistry.gStrIsLoggingEnable, lregkey.GetValue(clsRegistry.gStrIsLoggingEnable));
                    //}
                    #endregion "old code referring current User registry"

                    string[] keyNames = lregkey.GetValueNames();
                    for (int i = 0; i < keyNames.Length; i++)
                    {
                        if (lregkey.GetValue(keyNames[i]) != null)
                        {
                            switch (keyNames[i].ToUpper())
                            {
                                case "SQLSERVER":
                                    clsGeneral.gstrSQLServerName = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Server Nam:" + clsGeneral.gstrSQLServerName);
                                    break;
                                case "DATABASE":
                                    clsGeneral.gstrDatabaseName = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Database:" + clsGeneral.gstrDatabaseName);
                                    break;
                                case "SQLUSER":
                                    clsGeneral.gstrUserId = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("User id:" + clsGeneral.gstrUserId);
                                    break;
                                case "SQLPASSWORD":
                                    clsGeneral.gstrPassword = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Password:" + clsGeneral.gstrPassword);
                                    break;
                                case "CLIENTUPDATESERVICEINTERVAL":
                                    clsGeneral.time_INTERVAL = Convert.ToInt16(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Interval :" + clsGeneral.time_INTERVAL);
                                    break;
                                //case "ISCLIENTUPDATEAVAILABLE":
                                //    clsGeneral.gIsUpdateAvailable = Convert.ToBoolean(keyNames[i]);
                                //    clsGeneral.UpdateLog("Update Flag :" + clsGeneral.gIsUpdateAvailable);
                                //    break;
                                case "CLIENTSERVICEINTALLATIONLOG":
                                    clsGeneral.strServiceLogPath = Convert.ToString(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Install Log :" + clsGeneral.strServiceLogPath);
                                    break;
                                case "ISCLIENTAUSLOGGINGENABLE":
                                    clsGeneral.isLoggingEnable = Convert.ToBoolean(lregkey.GetValue(keyNames[i]));
                                    //clsGeneral.UpdateLog("Logging Enable :" + clsGeneral.isLoggingEnable);
                                    break;
                            }
                        }
                    }

                    if (lregkey != null)
                    {
                        try
                        {
                            lregkey.Close();
                            lregkey.Dispose();
                        }
                        catch
                        {
                        }
                        lregkey = null;
                    }
                }
                else
                {
                    regkey = Registry.CurrentUser.CreateSubKey(SubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception while migrating value from Local to Current user :" + ex.Message.ToString() + "");
            }
            finally
            {
                //objServerName = null;
                //objDatabasename = null;
                //objSQLUsername = null;
                //objSQLPassword = null;
                //objClientUpdateServiceInterval = null;
                //objClientInstallationLog = null;
                if (lregkey != null)
                {
                    try
                    {
                        lregkey.Close();
                        lregkey.Dispose();
                    }
                    catch
                    {
                    }
                    lregkey = null;
                }
            }
        } //CopyClientUpdateValueLocalMachineToCurrentUserRegistry

        public static object GetRegistryValue(string KeyName)
        {
            string strRetValue = null;
            try
            {
                object strValue = regkey.GetValue(KeyName);
                if (strValue != null)
                    strRetValue = Convert.ToString(strValue);
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error in function 'GetRegistryValue'. Error Message :" + ex.Message.ToString());
            }
            return strRetValue;
        } //GetRegistryValue

        public static void SetRegistryValue(string Name, object Reg)
        {
            try
            {
                if (regkey != null)
                    regkey.SetValue(Name, Reg);
                
                if (regPMkey != null)
                    regPMkey.SetValue(Name, Reg);

                if (Objlocalregkey != null)
                    Objlocalregkey.SetValue(Name, Reg);
                
                if (ObjlocalPMregkey != null)
                    ObjlocalPMregkey.SetValue(Name, Reg);


            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while setting registry '" + Name + "' and value :'" + Reg + "'. Error Message :" + ex.Message.ToString());
            }
        }

        public static void CloseRegistryKey()
        {
            try
            {
                if (regkey != null)
                {
                    regkey.Close();
                    regkey.Dispose();
                    regkey = null;
                }
                if (regPMkey != null)
                {
                    regPMkey.Close();
                    regPMkey.Dispose();
                    regPMkey = null;
                }
                if (Objlocalregkey != null)
                {
                    Objlocalregkey.Close();
                    Objlocalregkey.Dispose();
                    Objlocalregkey = null;

                }

                if (ObjlocalPMregkey != null)
                {
                    ObjlocalPMregkey.Close();
                    ObjlocalPMregkey.Dispose();
                    ObjlocalPMregkey = null;

                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error While Closing registry key. Error Message : " + ex.Message.ToString());
            }

        } //CloseRegistryKey

        //Not in Use
        public static bool CheckRegistryExists(string strRegKey)
        {
            bool _success = false;
            RegistryKey oKey = null;
            //oKey = Registry.LocalMachine.OpenSubKey(strRegKey, true);
            oKey = Registry.CurrentUser.OpenSubKey(strRegKey, true);
            try
            {
                if (oKey != null && oKey.ToString() != "")
                {
                    _success = true;
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Exception in CheckRegistryExists " + ex.Message.ToString() + "");
            }
            finally
            {
                if (oKey != null)
                {
                    try
                    {
                        oKey.Close();
                        oKey.Dispose();
                    }
                    catch
                    {
                    }
                    oKey = null;
                }

            }
            return _success;
        }

        //Not in Use
        public static bool WriteValue(RegistryKey ParentKey, string SubKey, string ValueName, object Value)
        {
            bool opened = false;
            try
            {
                if (ParentKey != null)
                {
                    //Open the given subkey 
                    RegistryKey Key = ParentKey.OpenSubKey(SubKey, true);

                    if (Key == null)
                    {
                        //when subkey doesn't exist create it 
                        Key = ParentKey.CreateSubKey(SubKey);
                        Key = ParentKey.OpenSubKey(SubKey, true);
                    }
                    if (Key != null)
                    {
                        opened = true;
                        Key.SetValue(ValueName, Value);
                        if (Key != null)
                        {
                            try
                            {
                                Key.Close();
                                Key.Dispose();
                            }
                            catch
                            {
                            }
                            Key = null;
                        }
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                //MessageBox.Show(e.InnerException.ToString());
            }
            finally
            {
                if (opened)
                {
                    if (ParentKey != null)
                        ParentKey.Close();
                }
            }
        }

        //Not in Use
        public static bool CreateSubKey(RegistryKey ParentKey, string SubKey)
        {
            bool opened = false;
            try
            {
                if (ParentKey != null)
                {
                    RegistryKey Key = ParentKey.OpenSubKey(SubKey, true);

                    if (Key == null)
                    {
                        //when subkey doesn't exist create it 
                        Key = ParentKey.CreateSubKey(SubKey);
                    }
                    else
                    {
                        opened = true;
                    }
                    if (Key != null)
                    {
                        try
                        {
                            Key.Close();
                            Key.Dispose();
                        }
                        catch
                        {
                        }
                        Key = null;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                //MessageBox.Show(e.InnerException.ToString());
            }
            finally
            {
                if (opened)
                {
                    if (ParentKey != null)
                        ParentKey.Close();
                }
            }
        }

        //Not in Use
        public static void CreateSubKey(string SubKeyName)
        {
            regkey = regkey.CreateSubKey(SubKeyName);

        } //CreateSubKey

        //Not in Use
        public static bool CreateSubKey(string SubKeyName, string none)
        {
            try
            {
                regkey = regkey.CreateSubKey(SubKeyName);
                return true;
            }
            catch
            {
                return false;
            }
        } //CreateSubKey

        //Not in use
        public static bool IsRegistryKeyExists(string SubKey)
        {
            RegistryKey checkRegkey = Registry.CurrentUser.OpenSubKey(SubKey);

            if (checkRegkey == null)
            {
                return false;
                //if ((Registry.LocalMachine.OpenSubKey(SubKey) == null))
                //{
                //    return false;
                //}
                //else
                //    return true;

            }
            else
            {
                try
                {
                    checkRegkey.Close();
                    checkRegkey.Dispose();
                }
                catch
                {
                }
                return true;
            }
        } //IsRegistryKeyExists

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
        }

        public static bool CheckRegistryExists()
        {
            //1.Check Whether gloEMR Registry Key Exists or not
            //2.If it exists  Dont show the Screen
            //3.If it does not exist show the Custom Screen
            bool _success = false;
            RegistryKey oKey = null;
            RegistryKey okey64 = null;
            int _type = CheckMachineStatus();
            if (_type == 0)
            {
                oKey = Registry.LocalMachine.OpenSubKey(str32EMRKey, true);
            }
            else
            {
                okey64 = Registry.LocalMachine.OpenSubKey(str64EMRKey, true);
                if (okey64 != null && okey64.ToString() != "")
                {
                    okey64.Close();
                    okey64.Dispose();
                    okey64 = null;
                    oKey = Registry.LocalMachine.OpenSubKey(str64EMRKey, true);
                }
                else
                {
                    oKey = Registry.LocalMachine.OpenSubKey(str32EMRKey, true);
                }

            }
            try
            {
                if (oKey != null && oKey.ToString() != "")
                {
                    _success = true;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (oKey != null)
                {
                    oKey.Close();
                    oKey.Dispose();
                }
                if (okey64 != null)
                {
                    okey64.Close();
                    okey64.Dispose();
                }
            }
            return _success;

        }

        public static object GetgloPMRegistryValue(string _value)
        {
            object value = null;
            RegistryKey oKey = Registry.LocalMachine.OpenSubKey(str32PMKey, true);
            try
            {
                if (oKey != null && oKey.ToString() != "")
                {
                    value = oKey.GetValue(_value);
                }

            }
            catch (System.NullReferenceException)
            {
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }
            finally
            {
                if (oKey != null)
                {
                    oKey.Close();
                    oKey.Dispose();
                }
            }
            return value;
        }

        public static bool CheckgloPMregistryExists()
        {
            bool _success = false;
            RegistryKey oKey = null;

            if (_type == 0)
            {
                oKey = Registry.LocalMachine.OpenSubKey(str32PMKey, true);
            }
            else
            {
                oKey = Registry.LocalMachine.OpenSubKey(str64PMKey, true);
            }
            try
            {
                if (oKey != null && oKey.ToString() != "")
                {
                    _success = true;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (oKey != null)
                {
                    oKey.Close();
                    oKey.Dispose();
                }
            }
            return _success;
        }
    }
}
