using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace gloICDAnalysis.ClassLib
{
    public class clsDBSettings
    {
        private const string gloSuiteEncryptionKey = "12345678";
        private  const string SqlServerEncryptionKey = "20gloStreamInc08";

        public static DBSetting GetDatabaseSettings(DBSetting.ApplicationType type)
        {
            DBSetting dbSetting = new DBSetting(type);
            gloRegistrySetting.OpenRemoteBaseKey();
            
            try
            {
                string key = string.Empty;

                if (type == DBSetting.ApplicationType.gloEMR)
                { key = gloRegistrySetting.gstrSoftEMR; }
                else
                { key = gloRegistrySetting.gstrSoftPM; }

                gloRegistrySetting.OpenSubKey(key);

                if (gloRegistrySetting.IsRegistryKeyExists(key) != false)
                {
                    string regName = string.Empty;

                    if (Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLSERVER")) != "")
                    {
                        dbSetting.SqlServerName = Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLSERVER"));
                    }
                    if (Convert.ToString(gloRegistrySetting.GetRegistryValue("Database")) != "")
                    {
                        dbSetting.DatabaseName = Convert.ToString(gloRegistrySetting.GetRegistryValue("Database"));
                    }
                    //IsSQLAuthentication

                    regName = string.Empty;
                    if (key == gloRegistrySetting.gstrSoftEMR)
                    { regName = "IsSQLAuthentication"; }
                    else { regName = "ISWINAUTHENTICATION"; }

                    if (Convert.ToString(gloRegistrySetting.GetRegistryValue(regName)) != "")
                    {
                        if (key == gloRegistrySetting.gstrSoftEMR)
                        {
                            if (Convert.ToBoolean(gloRegistrySetting.GetRegistryValue(regName)) == true)
                            {
                                dbSetting.IsWindowsAuthentication = false;
                            }
                            else
                            {
                                dbSetting.IsWindowsAuthentication = true;
                            }
                        }
                        else
                        {
                            dbSetting.IsWindowsAuthentication = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue(regName));
                        }

                    }

                    //SQLUSERNAME
                    regName = string.Empty;
                    if (key == gloRegistrySetting.gstrSoftEMR)
                    { regName = "SQLUserEMR"; }
                    else { regName = "SQLUSERNAME"; }
                    if (Convert.ToString(gloRegistrySetting.GetRegistryValue(regName)) != "")
                    {
                        dbSetting.SqlUserName = Convert.ToString(gloRegistrySetting.GetRegistryValue(regName));
                    }
                    regName = string.Empty;
                    if (key == gloRegistrySetting.gstrSoftEMR)
                    { regName = "SQLPasswordEMR"; }
                    else { regName = "SQLPASSWORD"; }

                    if (key == gloRegistrySetting.gstrSoftEMR)
                    {
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue(regName)) != "")
                        {
                            ClsEncryption oDecrypt = new ClsEncryption();
                            string d_sPassword = oDecrypt.DecryptFromBase64String(Convert.ToString(gloRegistrySetting.GetRegistryValue(regName)), gloSuiteEncryptionKey);
                            dbSetting.SqlPassword = d_sPassword;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue(regName)) != "")
                        {
                            ClsEncryption oDecrypt = new ClsEncryption();
                            string d_sPassword = oDecrypt.DecryptFromBase64String(Convert.ToString(gloRegistrySetting.GetRegistryValue(regName)), SqlServerEncryptionKey);
                            dbSetting.SqlPassword = d_sPassword;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
                gloRegistrySetting.CloseRegistryKey();
            }
            return dbSetting;
        }

        public static bool ValidateDatabaseSettings()
        {
            bool result;
            System.Data.SqlClient.SqlConnection _connection = new System.Data.SqlClient.SqlConnection();

            try
            {
                _connection.ConnectionString = CurrentDBInfo.ConnectionString;
                _connection.Open();
                _connection.Close();

                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (_connection != null) { _connection.Dispose(); _connection = null; }
            }
            return result;
        }

        public static string GetConnectionString()
        {
            string _connstring = "";
            try
            {
                if (clsDBSettings.CurrentDBInfo.IsWindowsAuthentication == false)//SQL authentication
                {
                    _connstring = "Server=" + clsDBSettings.CurrentDBInfo.SqlServerName + ";Database=" + clsDBSettings.CurrentDBInfo.DatabaseName + ";Uid=" + clsDBSettings.CurrentDBInfo.SqlUserName + ";Pwd=" + clsDBSettings.CurrentDBInfo.SqlPassword + ";";
                }
                else//windows authentication
                {
                    _connstring = "Server=" + clsDBSettings.CurrentDBInfo.SqlServerName + ";Database=" + clsDBSettings.CurrentDBInfo.DatabaseName + ";Trusted_Connection=yes;";
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            return _connstring;
        }

        public static Int32 GetDBVersion()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            Int32 sVersion = 0;
            try
            {
                con = new SqlConnection(clsDBSettings.CurrentDBInfo.ConnectionString); //}
                cmd = new SqlCommand("Select sSettingsValue  FROM dbo.Settings WHERE sSettingsName = 'DB Version' ", con); //,sSettingsName
                cmd.CommandType = CommandType.Text;
                con.Open();
                var result = cmd.ExecuteScalar().ToString();

                if (result != null)
                {
                    sVersion =  Convert.ToInt32(result.ToString().Replace(".", ""));
                }
            }
            catch //(Exception ex)
            {
                //UpdateLog(ex.ToString(), true);
            }
            finally
            {
                con.Close();

                if (cmd != null) { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }
                if (con != null) { con.Dispose(); con = null; }
            }
            return sVersion;
        }

        public static bool IsRecordExist(string TblName)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            bool _result = true;
            try
            {
                string strSQL=string.Empty;
                if (TblName == "ICD10CM_order")
                {
                     strSQL = "select COUNT(sICD10code) AS cntObject from ICD10OrderFiles ";
                }
                else if (TblName == "ICD10_GEM_File")
                {
                    strSQL = "select COUNT(sICD10Code) AS cntObject from ICD9ToICD10Mapping ";
                }
                con = new SqlConnection(clsDBSettings.CurrentDBInfo.ConnectionString);

                cmd = new SqlCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    if ((Int32)result <= 0)
                    {
                        _result = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Could not find"))
                {
                    _result = false;
                }
            }

            finally
            {
                con.Close();
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (con != null)
                {
                    con.Dispose();
                    con = null;
                }
            }
            return _result;
        }

        //public static bool IsDBUpdatesExists()
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    bool _result = true;
        //    try
        //    {
        //        con = new SqlConnection(clsDBSettings.CurrentDBInfo.ConnectionString);

        //        cmd = new SqlCommand(" SELECT COUNT(object_id) AS cntObject FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICD9ToICD10Mapping]') ", con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        var result = cmd.ExecuteScalar();
        //        if (result != null)
        //        {
        //            if ((Int32)result <= 0)
        //            {
        //                _result = false;
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        if (ex.Message.Contains("Could not find"))
        //        {
        //            _result = false;
        //        }
        //    }

        //    finally
        //    {
        //        con.Close();
        //        if (cmd != null)
        //        {
        //            cmd.Dispose();
        //            cmd = null;
        //        }
        //        if (con != null)
        //        {
        //            con.Dispose();
        //            con = null;
        //        }
        //    }
        //    return _result;
        //}

        public static bool IsDBUpdatesExists()
        {
            SqlConnection con = null;           
            bool _result = true;
            con = new SqlConnection(clsDBSettings.CurrentDBInfo.ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("Check_IsDBScriptExist", con))
                {   
                    con.Open();
                     _result = (bool)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Could not find"))
                {
                    _result = false;
                }
            }

            finally
            {
                con.Close();               
                if (con != null)
                {
                    con.Dispose();
                    con = null;
                }
            }
            return _result;
        }
      
        public static DBSetting CurrentDBInfo {get;set;}

    }



    public  class DBSetting
    {
        public DBSetting(ApplicationType type)
        {
            Application = type;
        }

        public enum ApplicationType
        {
            gloEMR,
            gloPM,
            Utility
        }

        public string SqlServerName { get; set; }
        public string DatabaseName { get; set; }
        public string SqlUserName { get; set; }
        public string SqlPassword { get; set; }
        public bool IsWindowsAuthentication { get; set; }
        public ApplicationType Application { get; set; }
        public bool IsDBChanged { get; set; }

        public string ConnectionString
        {
            get 
            {
                return clsDBSettings.GetConnectionString();
            }
        }
    }
}
