using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloSettings;
using gloSecurity;
using gloSSRSApplication.SSRS;



namespace gloSSRSApplication
{
    public class gloSSRS
    {

        #region "Create Datasource"

        //30-Jan-15 Aniket: Resolving Bug #77784: gloEMR > Reports >Add on Reports> Try to generate report showing message 'The item /foaOl_gloSuiteReports/Appointment Template List cannot be found. (rsltemNotFound) '
        public static bool CheckReportExists(string ReportName, string _databaseconnectionstring)
        {

            string sReportProtocol = string.Empty;
            string sReportserver = string.Empty;
            string sReportfolder = string.Empty;
            string sCustomizedReportfolder = string.Empty;
            string sVirtualDir = string.Empty;

            string sServername = string.Empty;
            string sDatabase = string.Empty;
            string sUserName = string.Empty;
            string sPassword = string.Empty;

            GeneralSettings oSetting = new GeneralSettings(_databaseconnectionstring);
            ReportingService2005 rs = null;
            CatalogItem[] items = null;
            SearchCondition condition = null;
            SearchCondition[] conditions = null;
            try
            {

            object oValue = null;

            oSetting.GetSetting("ReportProtocol", out oValue);
            if (oValue != null)
            {
                sReportProtocol = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("ReportServer", out oValue);
            if (oValue != null)
            {
                sReportserver = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("CustomizedReportFolder", out oValue);
            if (oValue != null)
            {
                sCustomizedReportfolder = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("ReportFolder", out oValue);
            if (oValue != null)
            {
                sReportfolder = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("ReportVirtualDirectory", out oValue);
            if (oValue != null)
            {
                sVirtualDir = oValue.ToString();
                oValue = null;
            }

            // Create a Web service proxy object and set credentials
            rs = new ReportingService2005();
            rs.Url = sReportProtocol + "://" + sReportserver + "/" + sVirtualDir + "/ReportService2005.asmx";
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

            items = null;
            condition = new SearchCondition();
            condition.Condition = ConditionEnum.Contains;
            condition.ConditionSpecified = true;
            condition.Name = "Name";
            condition.Value = ReportName;

            conditions = new SearchCondition[1];
            conditions[0] = condition;

            // Return a list of catalog items in the report server database
            //CatalogItem[] items = rs.ListChildren("/8040", true);

            items = rs.FindItems("/" + sReportfolder, BooleanOperatorEnum.Or, conditions);

            if (items != null)
            {
                foreach (CatalogItem ci in items)
                {
                    if (ci.Type == ItemTypeEnum.Report && ci.Name == ReportName)
                    {
                        return true;
                    }
                }
            }

          

            return false;
        }
             catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return true;
            }

             finally
            {
                sReportserver = null;
                sReportfolder = null;
                sVirtualDir = null;
                sServername = null;
                sDatabase = null;
                sUserName = null;
                sPassword = null;
                if (rs != null) { rs.Dispose(); rs = null; }
                items = null;
                condition = null;
                conditions = null;
                oSetting.Dispose();
                oSetting = null;
            }
        }

        public static void Create_Datasource(string sDSName, string sReportType, string _databaseconnectionstring, string _gstrSQLServerName, string _gstrDatabaseName, bool _gWindowsAuthentication, string _gstrSQLUser, string _gstrSQLPassword, bool overwrite)
        {
            string sReportProtocol = string.Empty;
            string sReportserver = string.Empty;
            string sReportfolder = string.Empty;
            string sCustomizedReportfolder = string.Empty;
            string sVirtualDir = string.Empty;

            string sServername = string.Empty;
            string sDatabase = string.Empty;
            string sUserName = string.Empty;
            string sPassword = string.Empty;

            bool isWinAuth;
            ReportingService2005 rs = new ReportingService2005();
            DataSourceDefinition Def = new DataSourceDefinition();
            ClsEncryption oEncrypt = new ClsEncryption();

            GeneralSettings oSetting = new GeneralSettings(_databaseconnectionstring);

            try
            {
                object oValue = null;

                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {   sReportProtocol = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {   sReportserver = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("CustomizedReportFolder", out oValue);
                if (oValue != null)
                {   sCustomizedReportfolder = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {   sReportfolder = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {   sVirtualDir = oValue.ToString();
                    oValue = null;
                }

                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                rs.Url = sReportProtocol + "://" + sReportserver + "/" + sVirtualDir + "/ReportService2005.asmx";


                sServername = _gstrSQLServerName;
                sDatabase = _gstrDatabaseName;
                sUserName = _gstrSQLUser;
                sPassword = _gstrSQLPassword;
                if (_gWindowsAuthentication == true) { isWinAuth = false; } else { isWinAuth = true; }

                if (isWinAuth == true)
                {
                    Def.CredentialRetrieval = CredentialRetrievalEnum.Integrated;
                    Def.Extension = "SQL";
                    Def.ConnectString = "Data Source=" + sServername + ";Initial Catalog=" + sDatabase; //+ "; Integrated Security = SSPI";
                    Def.Enabled = true;
                }
                else
                {
                    Def.CredentialRetrieval = CredentialRetrievalEnum.Store;
               //     DataSourceReference reference = new DataSourceReference();
                    Def.ConnectString = "Data Source=" + sServername + ";Initial Catalog=" + sDatabase;
                    Def.UserName = sUserName;
                    Def.Password = sPassword;
                    Def.Extension = "SQL";
                    Def.WindowsCredentials = false;
                }

                try
                {

                    if (sReportType == "gloCustomizedSSRS")  
                    {
                        sReportfolder = sCustomizedReportfolder;
                    }


                    CatalogItem[] items = null;
                    SearchCondition condition = new SearchCondition();
                    condition.Condition = ConditionEnum.Contains;
                    condition.ConditionSpecified = true;
                    condition.Name = "Name";
                    condition.Value = sReportfolder;

                    SearchCondition[] conditions = new SearchCondition[1];
                    conditions[0] = condition;

                    bool IsFolderExist = false ;
                    

                    items = rs.FindItems("/", BooleanOperatorEnum.Or, conditions);

                    if (items != null)
                    {
                        string sUpperFolder = sReportfolder.ToUpper();
                        foreach (CatalogItem ci in items)
                        {
                            if (ci.Type == ItemTypeEnum.Folder && ci.Name.ToUpper() == sUpperFolder)
                            {
                                IsFolderExist = true;
                                break;
                            }
                        }
                    }
                    condition = null;
                    conditions = null;
                    if (IsFolderExist == false)
                    {
                        rs.CreateFolder(sReportfolder, "/", null);
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                rs.CreateDataSource(sDSName, "/" + sReportfolder, true, Def, null);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                sReportserver = null;
                sReportfolder = null;
                sVirtualDir = null;
                sServername = null;
                sDatabase = null;
                sUserName = null;
                sPassword = null;
                Def = null;

                rs.Dispose();
                rs = null;

                oSetting.Dispose();
                oSetting = null;

                oEncrypt.Dispose();
                oEncrypt = null;
            }
        }

        public static void Create_Datasource(string sDSName, string sReportType, string _databaseconnectionstring, string _gstrSQLServerName, string _gstrDatabaseName, bool _gWindowsAuthentication, string _gstrSQLUser, string _gstrSQLPassword, bool overwrite, string _sReportProtocol, string _sReportserver, string _sCustomizedReportfolder, string _sReportfolder, string _sVirtualDir)
        {
            string sReportProtocol = string.Empty;
            string sReportserver = string.Empty;
            string sReportfolder = string.Empty;
            string sCustomizedReportfolder = string.Empty;
            string sVirtualDir = string.Empty;

            string sServername = string.Empty;
            string sDatabase = string.Empty;
            string sUserName = string.Empty;
            string sPassword = string.Empty;

            bool isWinAuth;
            ReportingService2005 rs = new ReportingService2005();
            DataSourceDefinition Def = new DataSourceDefinition();
            ClsEncryption oEncrypt = new ClsEncryption();


            try
            {
                sReportProtocol = _sReportProtocol;
                sReportserver = _sReportserver;
                sCustomizedReportfolder = _sCustomizedReportfolder;
                sReportfolder = _sReportfolder;
                sVirtualDir = _sVirtualDir;

                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                rs.Url = sReportProtocol + "://" + sReportserver + "/" + sVirtualDir + "/ReportService2005.asmx";


                sServername = _gstrSQLServerName;
                sDatabase = _gstrDatabaseName;
                sUserName = _gstrSQLUser;
                sPassword = _gstrSQLPassword;
                if (_gWindowsAuthentication == true) { isWinAuth = false; } else { isWinAuth = true; }

                if (isWinAuth == true)
                {
                    Def.CredentialRetrieval = CredentialRetrievalEnum.Integrated;
                    Def.Extension = "SQL";
                    Def.ConnectString = "Data Source=" + sServername + ";Initial Catalog=" + sDatabase; //+ "; Integrated Security = SSPI";
                    Def.Enabled = true;
                }
                else
                {
                    Def.CredentialRetrieval = CredentialRetrievalEnum.Store;
                    //     DataSourceReference reference = new DataSourceReference();
                    Def.ConnectString = "Data Source=" + sServername + ";Initial Catalog=" + sDatabase;
                    Def.UserName = sUserName;
                    Def.Password = sPassword;
                    Def.Extension = "SQL";
                    Def.WindowsCredentials = false;
                }

                try
                {

                    if (sReportType == "gloCustomizedSSRS")
                    {
                        sReportfolder = sCustomizedReportfolder;
                    }


                    CatalogItem[] items = null;
                    SearchCondition condition = new SearchCondition();
                    condition.Condition = ConditionEnum.Contains;
                    condition.ConditionSpecified = true;
                    condition.Name = "Name";
                    condition.Value = sReportfolder;

                    SearchCondition[] conditions = new SearchCondition[1];
                    conditions[0] = condition;

                    bool IsFolderExist = false;


                    items = rs.FindItems("/", BooleanOperatorEnum.Or, conditions);

                    if (items != null)
                    {
                        string sUpperFolder = sReportfolder.ToUpper();
                        foreach (CatalogItem ci in items)
                        {
                            if (ci.Type == ItemTypeEnum.Folder && ci.Name.ToUpper() == sUpperFolder)
                            {
                                IsFolderExist = true;
                                break;
                            }
                        }
                    }
                    condition = null;
                    conditions = null;
                    if (IsFolderExist == false)
                    {
                        rs.CreateFolder(sReportfolder, "/", null);
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                rs.CreateDataSource(sDSName, "/" + sReportfolder, true, Def, null);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                sReportserver = null;
                sReportfolder = null;
                sVirtualDir = null;
                sServername = null;
                sDatabase = null;
                sUserName = null;
                sPassword = null;
                Def = null;

                rs.Dispose();
                rs = null;

                oEncrypt.Dispose();
                oEncrypt = null;
            }
        }
        #endregion
    }
}
