using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;

namespace gloAuditTrail
{
    public static class gloAuditTrail
    {
        #region "Private variables"

        private static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private static string _databaseconnectionstring = "";
        public static bool gblnEnableApplicationLogs = false;
        public static bool gblnEnableExceptionLogs = false;
        private static Int64 _nAuditTrailID = 0;
        private static DateTime _dtActivityDateTime;
        private static string _sActivityModule = "";
        private static string _sActivityCategory = "";
        private static string _sActivityType = "";
        private static string _sDescription = "";
        private static Int64 _nPatientID = 0;
        private static Int64 _nTransactionID = 0;
        private static Int64 _nProviderID = 0;
        private static Int64 _nUserID = 0;
        private static string _sUserName = "";
        private static string _sMachineName = "";
        private static string _sOutcome = "";
        private static Int64 _nClinicID = 0;
        private static string _MessageBoxCaption = String.Empty;
        //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion


        #region "Property Procedures"

        public static Int64 AuditTrailID
        {
            get { return _nAuditTrailID; }
            set { _nAuditTrailID = value; }
        }

        public static DateTime ActivityDateTime
        {
            get { return _dtActivityDateTime; }
            set { _dtActivityDateTime = value; }
        }

        public static string ActivityModule
        {
            get { return _sActivityModule; }
            set { _sActivityModule = value; }
        }

        public static string ActivityCategory
        {
            get { return _sActivityCategory; }
            set { _sActivityCategory = value; }
        }

        public static string ActivityType
        {
            get { return _sActivityType; }
            set { _sActivityType = value; }
        }

        public static string Description
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }

        public static Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public static Int64 TransactionID
        {
            get { return _nTransactionID; }
            set { _nTransactionID = value; }
        }

        public static Int64 ProviderID
        {
            get { return _nProviderID; }
            set { _nProviderID = value; }
        }

        public static Int64 UserID
        {
            get { return _nUserID; }
            set { _nUserID = value; }
        }

        public static string UserName
        {
            get { return _sUserName; }
            set { _sUserName = value; }
        }

        public static string MachineName
        {
            get { return _sMachineName; }
            set { _sMachineName = value; }
        }

        public static string Outcome
        {
            get { return _sOutcome; }
            set { _sOutcome = value; }
        }

        public static Int64 ClinicID
        {
            get { return _nClinicID; }
            set { _nClinicID = value; }
        }

        //Dhruv for setting of the value of gloEMR/gloEMR Admin
        public static String MessageBoxCaption
        {
            get { return _MessageBoxCaption; }
            set { _MessageBoxCaption = value; }
        }

        public static String DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        #endregion


        static gloAuditTrail()
        {
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }
        }

        public static bool isChecked = false;
        public static bool IsServerOS = false;
       
        private static object GetRegistryValue(string KeyName)
        {
            //Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER

            ////Commented for - From 7020 read registrey from Current user (Windows 8) on 20121217
            //if (!isChecked)
            //{
            //    if (gloDatabaseLayer.clsCommon.GetOSInfo(out isChecked))
            //    { 
            //        IsServerOS = true; 
            //    }
            //}
            //if (IsServerOS)
            //{
            //    regkey = Registry.CurrentUser;
            //}
            //else
            //{
            //    regkey = Registry.LocalMachine;
            //}
            ////End
//SLR: Added on 3/20/2014 - to resolve registry memory
            RegistryKey regkey = Registry.CurrentUser;

            RegistryKey rkey;
            object myObject = null;
            //Following is done to Log Application exception Log which was not done at the time of Terminal Server 
            if (_MessageBoxCaption.Contains("EMR"))
            {
                rkey = regkey.OpenSubKey("Software\\gloEMR");
            }
            else
            {
                rkey = regkey.OpenSubKey("Software\\gloPM");
            }
            try
            {
                if (rkey != null)
                {
                    myObject = rkey.GetValue(KeyName);
                    rkey.Close();
                    rkey.Dispose();
                }
                regkey.Close();
                regkey.Dispose();
            }
            catch
            {
            }
            return myObject;
        }
        public static void Clear()
        {
            _nAuditTrailID = 0;
            _dtActivityDateTime = DateTime.Now;
            _sActivityModule = "";
            _sActivityCategory = "";
            _sActivityType = "";
            _sDescription = "";
            _nPatientID = 0;
            _nTransactionID = 0;
            _nProviderID = 0;
            _nUserID = 0;
            _sUserName = "";
            _sMachineName = "";
            _sOutcome = "";
            _nClinicID = 0;
        }

        private static void GetDefaultParameters()
        {
            try
            {
                // Machine Name
                _sMachineName = System.Windows.Forms.SystemInformation.ComputerName;

                //DatabaseConnection String
                if (appSettings["DataBaseConnectionString"] != null)
                {
                    if (appSettings["DataBaseConnectionString"] != "")
                    { _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]); }
                    else { _databaseconnectionstring = ""; }
                }
                else
                { _databaseconnectionstring = ""; }

                //Clinic ID
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _nClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _nClinicID = 0; }
                }
                else
                { _nClinicID = 0; }

                //User ID
                if (appSettings["UserID"] != null)
                {
                    if (appSettings["UserID"] != "")
                    { _nUserID = Convert.ToInt64(appSettings["UserID"]); }
                    else { _nUserID = 0; }
                }
                else
                { _nUserID = 0; }

                //User Name
                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    { _sUserName = Convert.ToString(appSettings["UserName"]); }
                    else { _sUserName = ""; }
                }
                else
                { _sUserName = ""; }

            }
            catch (Exception ex)
            {
                ExceptionLog(ex.ToString(), false);
            }
        }

        public static void UpdateLog(ActivityModule oActivityModule, ActivityCategory oActivityCategory, ActivityType oActivityType, string Description, ActivityOutCome oActivityOutCome)
        {
            System.IO.StreamWriter objFile = null;
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            string LogPath = string.Empty;
            string _fileName = string.Empty;
            try
            {

                //Boolean flgApplicationErr = false;

                //if (gloAuditTrail.GetRegistryValue("EnableApplicationLogs") != null)
                //{
                //    flgApplicationErr = Convert.ToBoolean(gloAuditTrail.GetRegistryValue("EnableApplicationLogs"));
                //}
                if (gblnEnableApplicationLogs  == true)
                {

                    //string _fileName = "ApplicationLog" + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";
                    _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";

                    LogPath = System.Windows.Forms.Application.StartupPath + "\\Log\\ApplicationLog";
                    if (CreateDirectoryIfNotExists(LogPath))
                    {
                        //if (System.IO.Directory.Exists(LogPath) == false)
                        //{
                        //    System.IO.Directory.CreateDirectory(LogPath);

                        //}
                        // FileStream fs = new FileStream(" ", FileMode.Append, Fi

                        objFile = new System.IO.StreamWriter(LogPath + "\\" + _fileName, true);
                        try
                        {
                            strMessage.Append(Environment.NewLine);
                            //strMessage.Append("-------------------------------------------------------------------------------------------------");
                            //strMessage.Append(Environment.NewLine);
                            strMessage.Append(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                            strMessage.Append("Module: " + oActivityModule.ToString() + ", ");
                            strMessage.Append("Category: " + oActivityCategory.ToString() + ", ");
                            strMessage.Append("Type: " + oActivityType.ToString() + ", ");
                            strMessage.Append("Description: " + Description + ", ");
                            strMessage.Append("OutCome: " + oActivityOutCome.ToString());
                            //strMessage.Append("-------------------------------------------------------------------------------------------------");
                            objFile.WriteLine(strMessage.ToString());
                        }

                        catch (Exception)
                        {

                            //throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

                // throw;
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (objFile != null)
                {
                    objFile.Close();
                    objFile.Dispose();
                    objFile = null;
                }
                if (strMessage != null)
                {
                    strMessage.Remove(0, strMessage.Length);
                    strMessage = null;
                }
                LogPath = null;
                _fileName = null;
            }


            //CreateAuditLog(oActivityModule, oActivityCategory, oActivityType, Description, 0, 0, 0, oActivityOutCome);
        }

        public static void ActivityLog(string TextToLog)
        {
            System.IO.StreamWriter objFile = null;
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            string _fileName = null;
            string LogPath = null;
            try
            {
                _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";

                LogPath = System.Windows.Forms.Application.StartupPath + "\\Log\\PatientMigration";
                if (CreateDirectoryIfNotExists(LogPath))
                {

                    //if (System.IO.Directory.Exists(LogPath) == false)
                    //{
                    //    System.IO.Directory.CreateDirectory(LogPath);
                    //}
                    objFile = new System.IO.StreamWriter(LogPath + "\\" + _fileName, true);
                    strMessage.Append(Environment.NewLine);
                    strMessage.Append(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                    strMessage.Append(TextToLog);
                    objFile.WriteLine(strMessage.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to log the activity");
            }
            finally
            {
                if (objFile != null)
                {
                    objFile.Close();
                    objFile.Dispose();
                    objFile = null;
                }
                if (strMessage != null)
                {
                    strMessage.Remove(0, strMessage.Length);
                    strMessage = null;
                }
                _fileName = null;
                LogPath = null;
            }
        }


        public static void CreateAuditLog(ActivityModule oActivityModule, ActivityCategory oActivityCategory, ActivityType oActivityType, string Description, ActivityOutCome oActivityOutCome)
        {
            CreateAuditLog(oActivityModule, oActivityCategory, oActivityType, Description, 0, 0, 0, oActivityOutCome);
        }

        public static Int64 CreateAuditLog(ActivityModule oActivityModule, ActivityCategory oActivityCategory, ActivityType oActivityType, string Description, ActivityOutCome oActivityOutCome, SoftwareComponent oSoftwareComponent)
        {
            return CreateAuditLog(oActivityModule, oActivityCategory, oActivityType, Description, 0, 0, 0, oActivityOutCome, oSoftwareComponent);
        }

        public static Int64 CreateAuditLog(ActivityModule oActivityModule, ActivityCategory oActivityCategory, ActivityType oActivityType, string Description, Int64 PatientID, Int64 TransactionID, Int64 ProviderID, ActivityOutCome oActivityOutCome)
        {
            long AuditTrailId = 0;
            SoftwareComponent component = SoftwareComponent.None;
            try
            {
                if (Assembly.GetEntryAssembly().GetName().Name.Equals("gloEMR"))
                { component = SoftwareComponent.gloEMR; }
                else if (Assembly.GetEntryAssembly().GetName().Name.Equals("gloEMRAdmin"))
                { component = SoftwareComponent.gloEMRAdmin; }
                else if (Assembly.GetEntryAssembly().GetName().Name.Equals("gloPM"))
                { component = SoftwareComponent.gloPM; }
                else if (Assembly.GetEntryAssembly().GetName().Name.Equals("gloPMAdmin"))
                { component = SoftwareComponent.gloPMAdmin; }
            }
            catch
            {
                component = SoftwareComponent.gloEMR;
            }
            finally
            {
                AuditTrailId = CreateAuditLog(oActivityModule, oActivityCategory, oActivityType, Description, PatientID, TransactionID, ProviderID, oActivityOutCome, component);
            }
            return AuditTrailId;
            //GetDefaultParameters();

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //object oReturnID = null;
            //try
            //{
            //    oDB.Connect(false);

            //    oDBParameters.Add("@nAuditTrailID", _nAuditTrailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
            //    oDBParameters.Add("@dtActivityDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
            //    oDBParameters.Add("@sActivityModule", oActivityModule.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@sActivityCategory", oActivityCategory.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@sActivityType", oActivityType.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@sDescription", Description, ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nUserID", _nUserID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@sUserName", _sUserName, ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@sMachineName", _sMachineName, ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@sOutcome", oActivityOutCome.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
            //    //oDBParameters.Add("@sSoftwareComponent", " ", ParameterDirection.Input, SqlDbType.VarChar);//Dhruv -> It was passed as blank
            //    oDBParameters.Add("@sSoftwareComponent", _MessageBoxCaption, ParameterDirection.Input, SqlDbType.VarChar);//Dhruv -> It passed as messgebox caption value.
            //    oDBParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


            //    oDB.Execute("gsp_INSERT_AuditTrail", oDBParameters, out oReturnID);

            //    oDB.Disconnect();
            //}
            //catch (gloDatabaseLayer.DBException dbEx)
            //{

            //    dbEx.ERROR_Log(dbEx.ToString());
            //}
            //catch (Exception ex)
            //{
            //    //ExceptionLog(ex.ToString(), false); 

            //    ex.ToString();
            //    ex = null; 
            //}
            //finally
            //{
            //    if (oDB != null) { oDB.Dispose(); }
            //    if (oDBParameters != null) { oDBParameters.Dispose(); }

            //    Clear();
            //}
        }

        public static Int64 CreateAuditLogService(ActivityModule oActivityModule, ActivityCategory oActivityCategory, ActivityType oActivityType, string Description, Int64 PatientID, Int64 TransactionID, Int64 ProviderID, ActivityOutCome oActivityOutCome, SoftwareComponent oSoftwareComponent, Boolean bOverrideAuditRights = false)
        {            
            Int64 nAuditTrailID = 0;            
            int _IsAuditEnable = CheckAuditEnabled(_nUserID);
            if (_IsAuditEnable == 1 || bOverrideAuditRights == true)
            {
                int _IsAuditRights = GetAuditRights(_nUserID, oActivityModule.GetHashCode(), oActivityModule.ToString());

                if (_IsAuditRights == 1 || bOverrideAuditRights == true)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                    object oReturnID = null;

                    try
                    {
                        oDB.Connect(false);

                        oDBParameters.Add("@nAuditTrailID", _nAuditTrailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                        oDBParameters.Add("@dtActivityDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@sActivityModule", oActivityModule.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sActivityCategory", oActivityCategory.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sActivityType", oActivityType.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        if (appSettings["BreakTheGlass"] != null)
                        {
                            if (appSettings["BreakTheGlass"].ToLower() == "true".ToLower())
                            {
                                Description = "Emergency Access: " + Description;
                            }
                        }
                        oDBParameters.Add("@sDescription", Description, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);                        
                        oDBParameters.Add("@sOutcome", oActivityOutCome.ToString(), ParameterDirection.Input, SqlDbType.VarChar);                        
                        oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                        oDB.Execute("gsp_INSERT_AuditTrail", oDBParameters, out oReturnID);
                        if (oReturnID != null)
                            nAuditTrailID = Convert.ToInt64(oReturnID);
                        oDB.Disconnect();

                    }
                    catch (gloDatabaseLayer.DBException dbEx)
                    {
                        dbEx.ERROR_Log(dbEx.ToString());
                    }
                    catch (Exception)// ex)
                    {
                        //ExceptionLog(ex.ToString(), false);
                        //ex.ToString();
                        //ex = null;
                    }
                    finally
                    {
                        if (oReturnID != null) oReturnID = null;
                        if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                        if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }

                        Clear();
                    }

                }
            }
            return nAuditTrailID;
        }

        public static Int64 CreateAuditLog(ActivityModule oActivityModule, ActivityCategory oActivityCategory, ActivityType oActivityType, string Description, Int64 PatientID, Int64 TransactionID, Int64 ProviderID, ActivityOutCome oActivityOutCome, SoftwareComponent oSoftwareComponent, Boolean bOverrideAuditRights = false)
        {
            GetDefaultParameters();
            Int64 nAuditTrailID = 0;
            //Added Rahul for cheking user has Audit Rights on 20101019
            int _IsAuditEnable = CheckAuditEnabled(_nUserID);
            if (_IsAuditEnable == 1 || bOverrideAuditRights== true)
            {
                int _IsAuditRights = GetAuditRights(_nUserID, oActivityModule.GetHashCode(), oActivityModule.ToString());
               
                if (_IsAuditRights == 1 || bOverrideAuditRights== true)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                    object oReturnID = null;

                    try
                    {
                        oDB.Connect(false);

                        oDBParameters.Add("@nAuditTrailID", _nAuditTrailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                        oDBParameters.Add("@dtActivityDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@sActivityModule", oActivityModule.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sActivityCategory", oActivityCategory.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sActivityType", oActivityType.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        if (appSettings["BreakTheGlass"] != null)
                        {
                            if (appSettings["BreakTheGlass"].ToLower() == "true".ToLower())
                            {
                                Description = "Emergency Access: " + Description;
                            }
                        }
                        oDBParameters.Add("@sDescription", Description, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                        //Commented Code for Audit LOG Enhancement
                        //oDBParameters.Add("@nUserID", _nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@sUserName", _sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@sMachineName", _sMachineName, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@sOutcome", oActivityOutCome.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@sSoftwareComponent", oSoftwareComponent.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                        oDB.Execute("gsp_INSERT_AuditTrail", oDBParameters, out oReturnID);
                        if (oReturnID != null)
                            nAuditTrailID = Convert.ToInt64(oReturnID);
                        oDB.Disconnect();

                    }
                    catch (gloDatabaseLayer.DBException dbEx)
                    {
                        dbEx.ERROR_Log(dbEx.ToString());
                    }
                    catch (Exception)// ex)
                    {
                        //ExceptionLog(ex.ToString(), false);
                        //ex.ToString();
                        //ex = null;
                    }
                    finally
                    {
                        if (oReturnID != null) oReturnID = null;
                        if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                        if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }

                        Clear();
                    }

                }
            }
            return nAuditTrailID;
        }
        
        public static long UpdateRemoteLoginDetails(string strUserName, bool blnLoginInStatus, string strMachineName = "", string SoftwareComponent = "", Int64 ClinicID = 0, bool _isLogin = true)
        {

        GetDefaultParameters();

       
        MachineDetails.MachineInfo localMachine = MachineDetails.LocalMachineDetails(_isLogin);
        MachineDetails.MachineInfo remotemachine = MachineDetails.RemoteMachineDetails(_isLogin);

        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        
        long intLoginSessionID=0;
        System.Collections.Hashtable oParam = new System.Collections.Hashtable();

        //bool Result = true;
        try
        {
            oDB.Connect(false);
            oDBParameters.Add("@sLoginName", strUserName, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@LocalMachineName", localMachine.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@LocalMachineIP", localMachine.MachineIp, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@LocalUserName", localMachine.UserName, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@RemoteMachineName", remotemachine.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@RemoteMachineIP", remotemachine.MachineIp, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@RemoteUserName", remotemachine.UserName, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@Domain", remotemachine.DomainName, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@ClientProcessID", System.Diagnostics.Process.GetCurrentProcess().Id, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@SoftwareComponent", SoftwareComponent, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@LoginSessionID", 0, ParameterDirection.Output, SqlDbType.BigInt);

            oParam=oDB.Execute("gsp_InsertRemoteUsersDetails", oDBParameters, true);
            
            if (oParam != null)
                intLoginSessionID = Convert.ToInt64(oParam["@LoginSessionID"]);

            oDB.Disconnect();
        }
        catch (gloDatabaseLayer.DBException dbEx)
        {
            dbEx.ERROR_Log(dbEx.ToString());
            return intLoginSessionID;
        }
        catch (Exception)
        {

        }
        finally
        {
            
               if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
               if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
               remotemachine = null;
               localMachine = null;
               Clear();
        }
        return intLoginSessionID;
       }

        public static DataTable ViewUserLog(Int64 UserID, Int64 ClinicID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtUserLog = null;
            try
            {
                oDB.Connect(false);

                string _sqlQuery = "SELECT nAuditTrailID, dtActivityDateTime, ISNULL(sActivityModule,'') AS sActivityModule, ISNULL(sActivityCategory,'') AS sActivityCategory,  "
                + " ISNULL(sActivityType,'') AS sActivityType, ISNULL(sDescription,'') AS sDescription,  ISNULL(nPatientID,0) AS nPatientID,  "
                + " ISNULL(nTransactionID,0) AS nTransactionID, ISNULL(nProviderID,0) AS nProviderID,  ISNULL(nUserID,0) AS nUserID,  "
                + " ISNULL(sUserName,'') AS sUserName, ISNULL(sMachineName,'') AS sMachineName, ISNULL(sOutcome,'') AS sOutcome,  "
                + " ISNULL(nClinicID,0) AS nClinicID "
                + " FROM AuditTrail_MST WHERE  nUserID = " + UserID + " AND nClinicID = " + ClinicID + "";


                oDB.Retrive_Query(_sqlQuery, out dtUserLog);

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception)// ex)
            {
                //ExceptionLog(ex.ToString(), false);  
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return dtUserLog;
        }

        public static DataTable ViewUserLog(String sModule, DateTime dtStartDate, DateTime dtEndDate, Int64 ClinicID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtUserLog = null;
            try
            {
                oDB.Connect(false);


                oDBParameters.Add("@StartDate", dtStartDate.ToString("MM/dd/yyyy"), ParameterDirection.Input, SqlDbType.Date);
                oDBParameters.Add("@EndDate", dtEndDate.ToString("MM/dd/yyyy"), ParameterDirection.Input, SqlDbType.Date);
                oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ActivityModule", sModule, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Retrive("gsp_GetAuditTrail", oDBParameters,out dtUserLog);

                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;
                
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return dtUserLog;
        }
    
        /// <summary>
        /// Use this method for troubleshooting detailed logs for each step during any Print functionality in application. 
        /// Logs will be created on '\Log\PrintLog' directory in application folder.
        /// </summary>
        /// <param name="strException"></param>
        /// <param name="ShowMessageBox"></param>
        public static void PrintLog(string strException, bool ShowMessageBox = false, bool LogInExceptions = true)
        {
            string strLogMessage = string.Empty;
            string _fileName = string.Empty;

            try
            {
                //Boolean flgExceptionErr = true;

                //if (gloAuditTrail.GetRegistryValue("EnableErrorLogs") != null)
                //{
                //    flgExceptionErr = Convert.ToBoolean(gloAuditTrail.GetRegistryValue("EnableErrorLogs"));
                //}

                if (gblnEnableExceptionLogs)
                {
                    String printLogPath = Application.StartupPath + "\\Log\\PrintLog";
                    if (CreateDirectoryIfNotExists(printLogPath))
                    {

                        //if (Directory.Exists(Application.StartupPath + "\\Log\\PrintLog") == false)
                        //{
                        //    Directory.CreateDirectory(Application.StartupPath + "\\Log\\PrintLog");
                        //}

                        strLogMessage = Environment.NewLine + "" +
                                               System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + Environment.NewLine +
                                               strException + Environment.NewLine;

                        // string _fileName = "ExceptionLog " + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";
                        _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
                        //File.AppendAllText(Application.StartupPath + "\\Log\\PrintLog\\" + _fileName, strLogMessage);
                        File.AppendAllText(printLogPath+"\\" + _fileName, strLogMessage);
                    }
                }
            }
            catch
            {
                //Intentionally BLANK do not change anything to call ExceptionLog on this block.                
            }
            finally
            {
                strLogMessage = null;
                _fileName = null;
            }
        }

        public static void ExceptionLog(string strException, bool ShowMessageBox)
        {
            string strLogMessage = string.Empty;
            string _fileName = string.Empty;

            try
            {
                //Boolean flgExceptionErr = true;

                //if (gloAuditTrail.GetRegistryValue("EnableErrorLogs") != null)
                //{
                //    flgExceptionErr = Convert.ToBoolean(gloAuditTrail.GetRegistryValue("EnableErrorLogs"));
                //}

                if (gblnEnableExceptionLogs == true)
                {
                    String exceptionLogPath = Application.StartupPath + "\\Log\\ExceptionLog";
                    if (CreateDirectoryIfNotExists(exceptionLogPath))
                    {
                        //if (Directory.Exists(Application.StartupPath + "\\Log\\ExceptionLog") == false)
                        //{
                        //    Directory.CreateDirectory(Application.StartupPath + "\\Log\\ExceptionLog");
                        //}

                        strLogMessage = Environment.NewLine + "" +
                                               System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + Environment.NewLine +
                                               strException + Environment.NewLine;

                        // string _fileName = "ExceptionLog " + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";
                        _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
                        //File.AppendAllText(Application.StartupPath + "\\Log\\ExceptionLog\\" + _fileName, strLogMessage);
                        File.AppendAllText(exceptionLogPath+"\\" + _fileName, strLogMessage);
                    }
                }

                if (ShowMessageBox == true)
                {
                    // MessageBox.Show(strException, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch// (Exception ex)
            {
                //ExceptionLog(ex.ToString(), false);
                //Aniket Commented because it was going in recursion
            }
            finally
            {
                strLogMessage = null;
                _fileName = null;
            }
        }

        public static void ExceptionLog(Exception exceptionOccured, bool ShowMessageBox)
        {
            string strLogMessage = string.Empty;
            string _fileName = string.Empty;
            try
            {
                // Boolean flgExceptionErr = true;

                //if (gloAuditTrail.GetRegistryValue("EnableErrorLogs") != null)
                //{
                //    flgExceptionErr = Convert.ToBoolean(gloAuditTrail.GetRegistryValue("EnableErrorLogs"));
                //}

                if (gblnEnableExceptionLogs == true)
                {
                    String exceptionLogPath = Application.StartupPath + "\\Log\\ExceptionLog";
                    if (CreateDirectoryIfNotExists(exceptionLogPath))
                    {

                        //if (Directory.Exists(Application.StartupPath + "\\Log\\ExceptionLog") == false)
                        //{
                        //    Directory.CreateDirectory(Application.StartupPath + "\\Log\\ExceptionLog");
                        //}

                        strLogMessage = BuildExceptionLogString(exceptionOccured);

                        // string _fileName = "ExceptionLog " + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";
                        _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
                        //File.AppendAllText(Application.StartupPath + "\\Log\\ExceptionLog\\" + _fileName, strLogMessage);
                        File.AppendAllText(exceptionLogPath + "\\" + _fileName, strLogMessage);
                    }
                }

                if (ShowMessageBox == true)
                {
                    //added to show exception details on new form
                    frmException frex = new frmException(exceptionOccured.ToString(), strLogMessage);
                    frex.ShowDialog();
                    if (frex != null)
                    {
                        frex.Close();
                        frex.Dispose();
                        frex = null;
                    }
                    //   MessageBox.Show(exceptionOccured.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch// (Exception ex)
            {
                //ExceptionLog(ex.ToString(), false);
                //Aniket Commented because it was going in recursion
            }
            finally
            {
                strLogMessage = null;
                _fileName = null;
            }
        }

        /// <summary>
        /// Added for command prompt utility
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="strException"></param>
        /// <param name="ShowMessageBox"></param>
        public static void ExceptionLog(string strMessage, string strException, bool ShowMessageBox)
        {
            string strLogMessage = string.Empty;
            string _fileName = string.Empty;

            try
            {
                //' Update the Status
                LogStatus(false);
                String exceptionLogPath = Application.StartupPath + "\\Log\\ExceptionLog";
                if (CreateDirectoryIfNotExists(exceptionLogPath))
                {

                    //if (Directory.Exists(Application.StartupPath + "\\Log\\ExceptionLog") == false)
                    //{
                    //    Directory.CreateDirectory(Application.StartupPath + "\\Log\\ExceptionLog");
                    //}

                    strLogMessage = ((Environment.NewLine + "" + System.DateTime.Now + ":" + System.DateTime.Now.Millisecond) + Environment.NewLine + strException) + Environment.NewLine;

                    // string _fileName = "ExceptionLog " + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";
                    _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
                    //File.AppendAllText(Application.StartupPath + "\\Log\\ExceptionLog\\" + _fileName, strLogMessage);
                    File.AppendAllText(exceptionLogPath + "\\" + _fileName, strLogMessage);

                    if (ShowMessageBox == true)
                    {
                        if ((MessageBox.Show(strMessage, " Patient Migration", MessageBoxButtons.OK, MessageBoxIcon.Error)) == System.Windows.Forms.DialogResult.OK)
                        {
                            //System.Diagnostics.Process.Start(Application.StartupPath + "\\Log\\ExceptionLog\\" + _fileName);
                            System.Diagnostics.Process.Start(exceptionLogPath + "\\" + _fileName);
                        }

                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Patient Migration - Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                strLogMessage = null;
                _fileName = null;
            }

        }

        public static void ExceptionLog(Exception exceptionOccured, string strException, bool ShowMessageBox)
        {
            try
            {
                //' Update the Status
                LogStatus(false);
                String exceptionLogPath = Application.StartupPath + "\\Log\\ExceptionLog";
                if (CreateDirectoryIfNotExists(exceptionLogPath))
                {

                    //if (Directory.Exists(Application.StartupPath + "\\Log\\ExceptionLog") == false)
                    //{
                    //    Directory.CreateDirectory(Application.StartupPath + "\\Log\\ExceptionLog");
                    //}

                    string strLogMessage = BuildExceptionLogString(exceptionOccured);

                    // string _fileName = "ExceptionLog " + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";
                    string _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
                    //File.AppendAllText(Application.StartupPath + "\\Log\\ExceptionLog\\" + _fileName, strLogMessage);
                    File.AppendAllText(exceptionLogPath + "\\" + _fileName, strLogMessage);

                    if (ShowMessageBox == true)
                    {
                        if ((MessageBox.Show(exceptionOccured.Message, " Patient Migration", MessageBoxButtons.OK, MessageBoxIcon.Error)) == System.Windows.Forms.DialogResult.OK)
                        {
                            //System.Diagnostics.Process.Start(Application.StartupPath + "\\Log\\ExceptionLog\\" + _fileName);
                            System.Diagnostics.Process.Start(exceptionLogPath + "\\" + _fileName);
                        }

                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Patient Migration - Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Added for command prompt utility
        /// </summary>
        /// <param name="Status"></param>
        public static void LogStatus(bool Status)
        {
            try
            {
                String logPath = Application.StartupPath + "\\Log";
                if (CreateDirectoryIfNotExists(logPath))
                {
                    //if (Directory.Exists(Application.StartupPath + "\\Log") == false)
                    //{
                    //    Directory.CreateDirectory(Application.StartupPath + "\\Log");
                    //}

                    string _fileName = logPath+"\\Status.log";

                    if (File.Exists(_fileName))
                    {
                        File.Delete(_fileName);
                    }

                    if (Status == true)
                    {
                        File.AppendAllText(_fileName, "done");
                    }
                    else
                    {
                        File.AppendAllText(_fileName, "failed");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " Patient Migration - Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExceptionLog(ActivityModule oActivityModule, ActivityCategory oActivityCategory, ActivityType oActivityType, string Description, ActivityOutCome oActivityOutCome)
        {
            System.IO.StreamWriter objFile = null;
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            try
            {
                //Boolean flgExceptionErr = false;

                //if (gloAuditTrail.GetRegistryValue("EnableErrorLogs") != null)
                //{
                //    flgExceptionErr = Convert.ToBoolean(gloAuditTrail.GetRegistryValue("EnableErrorLogs"));
                //}

                if (gblnEnableExceptionLogs  == true)
                {

                    string _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";

                    string LogPath = System.Windows.Forms.Application.StartupPath + "\\Log\\ExceptionLog";
                    if (CreateDirectoryIfNotExists(LogPath))
                    {
                        //if (System.IO.Directory.Exists(LogPath) == false)
                        //{
                        //    System.IO.Directory.CreateDirectory(LogPath);

                        //}

                        objFile = new System.IO.StreamWriter(LogPath + "\\" + _fileName, true);
                        try
                        {
                            strMessage.Append(Environment.NewLine);
                            //strMessage.Append("-------------------------------------------------------------------------------------------------");
                            //strMessage.Append(Environment.NewLine);
                            strMessage.Append(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                            strMessage.Append("Module: " + oActivityModule.ToString() + ", ");
                            strMessage.Append("Category: " + oActivityCategory.ToString() + ", ");
                            strMessage.Append("Type: " + oActivityType.ToString() + ", ");
                            strMessage.Append("Description: " + Description + ", ");
                            strMessage.Append("OutCome: " + oActivityOutCome.ToString());
                            //strMessage.Append("-------------------------------------------------------------------------------------------------");
                            objFile.WriteLine(strMessage.ToString());
                        }

                        catch (Exception)
                        {

                            //throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (objFile != null)
                {
                    objFile.Close();
                    objFile.Dispose();
                    objFile = null;
                }
                if (strMessage != null)
                {
                    strMessage.Remove(0, strMessage.Length);
                    strMessage = null;
                }
            }
        }

        public static void ExceptionLog(ActivityModule oActivityModule, ActivityCategory oActivityCategory, ActivityType oActivityType, Exception exceptionOccured, ActivityOutCome oActivityOutCome)
        {
            System.IO.StreamWriter objFile = null;
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            string _fileName = string.Empty;
            string LogPath = string.Empty;

            try
            {
                //Boolean flgExceptionErr = false;

                //if (gloAuditTrail.GetRegistryValue("EnableErrorLogs") != null)
                //{
                //    flgExceptionErr = Convert.ToBoolean(gloAuditTrail.GetRegistryValue("EnableErrorLogs"));
                //}

                if (gblnEnableExceptionLogs  == true)
                {

                    _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";

                    LogPath = System.Windows.Forms.Application.StartupPath + "\\Log\\ExceptionLog";
                    if (CreateDirectoryIfNotExists(LogPath))
                    {
                        //if (System.IO.Directory.Exists(LogPath) == false)
                        //{
                        //    System.IO.Directory.CreateDirectory(LogPath);

                        //}

                        objFile = new System.IO.StreamWriter(LogPath + "\\" + _fileName, true);
                        try
                        {
                            strMessage.Append(Environment.NewLine);
                            //strMessage.Append("-------------------------------------------------------------------------------------------------");
                            //strMessage.Append(Environment.NewLine);
                            strMessage.Append(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                            strMessage.Append("Module: " + oActivityModule.ToString() + ", ");
                            strMessage.Append("Category: " + oActivityCategory.ToString() + ", ");
                            strMessage.Append("Type: " + oActivityType.ToString() + ", ");
                            strMessage.Append("OutCome: " + oActivityOutCome.ToString());
                            strMessage.Append("Description: " + BuildExceptionLogString(exceptionOccured) + ", ");
                            //strMessage.Append("-------------------------------------------------------------------------------------------------");
                            objFile.WriteLine(strMessage.ToString());
                        }

                        catch (Exception)
                        {

                            //throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (objFile != null)
                {
                    objFile.Close();
                    objFile.Dispose();
                    objFile = null;
                }
                if (strMessage != null)
                {
                    strMessage.Remove(0, strMessage.Length);
                    strMessage = null;
                }
                _fileName = null;
                LogPath = null; ;
            }
        }

        private static int GetAuditRights(long nUserId, int nModuleId,string Module)
        {
            SqlConnection objCon = new SqlConnection();
            objCon.ConnectionString = _databaseconnectionstring;
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "gsp_CheckAuditRights";
            objCmd.Parameters.AddWithValue("@nUserId", nUserId);
            objCmd.Parameters.AddWithValue("@nModuleId", nModuleId);
            objCmd.Parameters.AddWithValue("@sModule", Module);
            objCmd.Connection = objCon;
            object _Id;
            try
            {
                objCon.Open();
                _Id = objCmd.ExecuteScalar();
                objCon.Close();

                if (_Id != null && _Id.ToString() != "")
                    return Convert.ToInt16(_Id);
                else
                    return 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCon != null) { objCon.Dispose(); objCon = null; }
                if (objCmd != null) { objCmd.Parameters.Clear();objCmd.Dispose(); objCmd = null; }
            }
        }

        private static int CheckAuditEnabled(long nUserId)
        {
            SqlConnection objCon = new SqlConnection();
            objCon.ConnectionString = _databaseconnectionstring;
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "gsp_CheckAuditEnabled";
            objCmd.Parameters.AddWithValue("@nUserId", nUserId);
            objCmd.Connection = objCon;
            object _Id;
            try
            {
                objCon.Open();
                _Id = objCmd.ExecuteScalar();
                objCon.Close();

                if (_Id != null && _Id.ToString() != "")
                    return Convert.ToInt16(_Id);
                else
                    return 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCon != null) { objCon.Dispose(); objCon = null; }
                if (objCmd != null) { objCmd.Parameters.Clear(); objCmd.Dispose(); objCmd = null; }
                _Id = null;
            }
        }

        public static void SetAppConfig(String DatabaseConnectionString)
        {
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            appSettings["DataBaseConnectionString"] = DatabaseConnectionString;
            appSettings["EMRConnectionString"] = DatabaseConnectionString;
            appSettings["MessageBOXCaption"] = "gloPM";
            appSettings["ClinicID"] = "1";
            appSettings["UserName"] = "TestAdmin";
            appSettings["UserID"] = "1";

        }

        private static string BuildExceptionLogString(Exception thrownexception)
        {
            string _exLogString = Environment.NewLine + "" +
                                System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + Environment.NewLine;
            if (thrownexception == null)
            {
                return "\tBlank Exception"+ Environment.NewLine;
            }
            try
            {
                _exLogString = _exLogString + "Message: " + thrownexception.Message + Environment.NewLine;
            }
            catch
            {
            }
            try
            {
                _exLogString = _exLogString + "Source: " + thrownexception.Source + Environment.NewLine;
            }
            catch
            {
            }
            try
            {
                _exLogString = _exLogString + "StackTrace: " + thrownexception.StackTrace + Environment.NewLine;
            }
            catch
            {
            }
            try
            {
                _exLogString = _exLogString + "TargetSite: " + thrownexception.TargetSite + Environment.NewLine;
            }
            catch
            {
            }
            try
            {
                if (thrownexception.InnerException != null && thrownexception.InnerException.Message.Trim() != "")
                {
                    try
                    {
                        _exLogString = _exLogString + "     InnerException: " + Environment.NewLine +
                            BuildExceptionLogString(thrownexception.InnerException);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
            return _exLogString;
        }
        public static string GetRunningPathFlow(bool bWriteToLog=false,bool bShowMessage=false)
        {
            StackFrame sFrame = new StackFrame(1, true);
	        string ToLog = "LineNumber: " + sFrame.GetFileLineNumber() + " Method: " +sFrame.GetMethod().ToString()+" Code: " +sFrame.GetFileName()+" Type: "+sFrame.GetType().ToString();
            sFrame = null;
            if (bWriteToLog)
            {
                ExceptionLog(ToLog, false);
            }
            if (bShowMessage)
            {
                MessageBox.Show(ToLog,"Reaching");
            }
            return ToLog;
        }
        public static bool CreateDirectoryIfNotExists(string dirName)
        {
            bool exists = false;
            try
            {
                try
                {
                    exists = System.IO.Directory.Exists(dirName);
                    if (exists)
                    {
                        return true;
                    }
                }
                catch
                {
                }
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(dirName);
                }
            }
            catch
            {
            }
            try
            {
                exists = System.IO.Directory.Exists(dirName);
            }
            catch
            {
            }
            return exists;
        }
    }
    public static class MachineDetails
    {
        public class MachineInfo
        {
            public string MachineName = "";
            public string DomainName = "";
            public string UserName = "";
            public string MachineIp = "";
            public string ProgramName = "";
        }
        private static MachineInfo m_localMachine = null;
        private static MachineInfo m_remoteMachine = null;

        public static MachineInfo LocalMachineDetails(bool bReGetDetails = false)
        {
            string LocalIp = string.Empty;
            string Domain = string.Empty;
            NET_API_STATUS status;
            IntPtr bufPtr;
            NETSETUP_JOIN_STATUS GroupType;
            string Host = string.Empty;
            System.Net.IPHostEntry host;
            try
            {
                if ((m_localMachine == null) || (bReGetDetails))
                {
                    Domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                    if (string.IsNullOrEmpty(Domain))
                    {
                        try
                        {
                            Domain = System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain().ToString();
                        }
                        catch
                        {
                        }
                        if (string.IsNullOrEmpty(Domain))
                        {

                            status = default(NET_API_STATUS);
                            bufPtr = default(IntPtr);
                            GroupType = default(NETSETUP_JOIN_STATUS);

                            try
                            {
                                status = NetGetJoinInformation(null, ref bufPtr, ref GroupType);
                                if (status == NET_API_STATUS.NERR_Success)
                                {
                                    Domain = System.Runtime.InteropServices.Marshal.PtrToStringAuto(bufPtr);
                                }
                            }
                            catch
                            {
                            }
                            finally
                            {
                                if (bufPtr != IntPtr.Zero)
                                {
                                    NetApiBufferFree(bufPtr);
                                }
                            }

                        }
                    }
                    Host = System.Net.Dns.GetHostName();
                    if (string.IsNullOrEmpty(Host))
                    {
                        Host = System.Windows.Forms.SystemInformation.ComputerName;
                    }
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {


                        host = System.Net.Dns.GetHostEntry(Host);

                        foreach (System.Net.IPAddress ip in host.AddressList)
                        {

                            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {

                                LocalIp = ip.ToString();

                                break;

                            }
                        }
                    }
                    m_localMachine = new MachineInfo();
                    m_localMachine.MachineName = Host;
                    m_localMachine.MachineIp = LocalIp;
                    m_localMachine.UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    m_localMachine.DomainName = Domain;
                    m_localMachine.ProgramName = Assembly.GetEntryAssembly().GetName().Name;
                    //return m_localMachine;
                }
                else
                {
                    //return m_localMachine;
                }
            }
            catch
            { }
            finally
            {
                LocalIp = null;
                Domain = null;
                //status = null;
                //bufPtr = null;
                //GroupType = null;
                Host = null;
                host = null;
            }
            return m_localMachine;
        }
        private static bool foundRemoteCorrectly=false;
        public static MachineInfo RemoteMachineDetails(bool bReGetDetails = false)
        {


            if( (foundRemoteCorrectly == false) || (m_remoteMachine == null) || (bReGetDetails))
            {
                LocalMachineDetails(bReGetDetails);
                string WhoAmI = GetRemoteConnectionType();
                if (WhoAmI == "RDP")
                {
                    foundRemoteCorrectly = true;
                    m_remoteMachine = new MachineInfo();
                    m_remoteMachine.MachineName = GetWTSInformation(WTS_INFO_CLASS.WTSClientName);
                    if (string.IsNullOrEmpty(m_remoteMachine.MachineName))
                    {
                        foundRemoteCorrectly = false;
                        m_remoteMachine.MachineName = m_localMachine.MachineName;
                    }
                    m_remoteMachine.DomainName = GetWTSInformation(WTS_INFO_CLASS.WTSDomainName);
                    if (string.IsNullOrEmpty(m_remoteMachine.DomainName))
                    {
                        foundRemoteCorrectly = false;
                        m_remoteMachine.DomainName = m_localMachine.DomainName;
                    }
                    m_remoteMachine.MachineIp = GetWTSInformation(WTS_INFO_CLASS.WTSClientAddress);
                    if (string.IsNullOrEmpty(m_remoteMachine.MachineIp))
                    {
                        foundRemoteCorrectly = false;
                        m_remoteMachine.MachineIp = m_localMachine.MachineIp;
                    }
                    m_remoteMachine.UserName =  GetWTSInformation(WTS_INFO_CLASS.WTSUserName);
                    if (string.IsNullOrEmpty(m_remoteMachine.UserName))
                    {
                        foundRemoteCorrectly = false;
                        m_remoteMachine.UserName =  m_localMachine.UserName;
                    }
                    m_remoteMachine.ProgramName = GetWTSInformation(WTS_INFO_CLASS.WTSInitialProgram);
                    if (string.IsNullOrEmpty(m_remoteMachine.ProgramName))
                    {
                        m_remoteMachine.ProgramName =  m_localMachine.ProgramName;
                    }

                    return m_remoteMachine;
                }
                else
                {
                    foundRemoteCorrectly = false;
                    m_remoteMachine = LocalMachineDetails(bReGetDetails);
                    return m_remoteMachine;
                }
            }
            else
            {
                return m_remoteMachine;
            }

        }

        private static string GetWTSInformation(WTS_INFO_CLASS whatInfoNeeded)
        {
            IntPtr buffer = IntPtr.Zero;
            Int32 bytesReturned;

            string strClientName = "";
            try
            {
                WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
                bool sucess = WTSQuerySessionInformation(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, whatInfoNeeded, out buffer, out bytesReturned);
                if (sucess)
                {
                    if (whatInfoNeeded == WTS_INFO_CLASS.WTSClientAddress)
                    {
                        WTS_CLIENT_ADDRESS oClientAddres = new WTS_CLIENT_ADDRESS();
                        try
                        {
                            oClientAddres = (WTS_CLIENT_ADDRESS)System.Runtime.InteropServices.Marshal.PtrToStructure(buffer, oClientAddres.GetType());
                        }
                        catch
                        {
                        }
                        strClientName = oClientAddres.bAddress[2] + "." + oClientAddres.bAddress[3] + "." + oClientAddres.bAddress[4] + "." + oClientAddres.bAddress[5];
                    }
                    else
                    {
                        strClientName = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(buffer);
                    }

                }
            }
            catch
            {
            }

            finally
            {
                if (buffer != IntPtr.Zero)
                {
                    try
                    {
                        WTSFreeMemory(buffer);
                    }
                    catch
                    {
                    }
                    buffer = IntPtr.Zero;
                }
            }
            return strClientName;
        }
        //Structure for Terminal Service Client IP Address
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct WTS_CLIENT_ADDRESS
        {
            public int iAddressFamily;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] bAddress;
        }
        private enum WTS_INFO_CLASS
        {
            WTSInitialProgram = 0,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType

        }
        private enum NET_API_STATUS : uint
        {
            NERR_Success = 0,
            ERROR_NOT_ENOUGH_MEMORY = 8
        }

        private enum NETSETUP_JOIN_STATUS : uint
        {
            NetSetupUnknownStatus = 0,
            NetSetupUnjoined,
            NetSetupWorkgroupName,
            NetSetupDomainName
        }

        [System.Runtime.InteropServices.DllImport("wtsapi32.dll")]
        private static extern bool WTSQuerySessionInformation(System.IntPtr hServer, int sessionId, WTS_INFO_CLASS wtsInfoClass, out System.IntPtr ppBuffer, out Int32 pBytesReturned);
        /// <summary>
        /// The WTSFreeMemory function frees memory allocated by a Terminal
        /// Services function.
        /// </summary>
        /// <param name="memory">Pointer to the memory to free.</param>
        [System.Runtime.InteropServices.DllImport("wtsapi32.dll", ExactSpelling = true, SetLastError = false)]
        private static extern void WTSFreeMemory(IntPtr memory);
        [System.Runtime.InteropServices.DllImport("Netapi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        private static extern NET_API_STATUS NetGetJoinInformation(string lpServer, ref IntPtr lpNameBuffer, ref NETSETUP_JOIN_STATUS bufferType);
        [System.Runtime.InteropServices.DllImport("Netapi32.dll")]
        private static extern NET_API_STATUS NetApiBufferFree(IntPtr Buffer);
        //[System.Runtime.InteropServices.DllImport("wfapi.dll")]
        //private static extern long WFGetActiveProtocol(int SessionId);

        private static IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
        private const int WTS_CURRENT_SESSION = -1;
      
        public static string GetRemoteConnectionType()
        {
            IntPtr buffer = IntPtr.Zero;
            Int32 bytesReturned;
            try
            {
                WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
                bool success = WTSQuerySessionInformation(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, WTS_INFO_CLASS.WTSClientProtocolType, out buffer, out bytesReturned);
                if (success)
                {
                    int ClientProtocolType = System.Runtime.InteropServices.Marshal.ReadInt32(buffer);
                    try
                    {
                        if (buffer != IntPtr.Zero)
                        {
                            try
                            {
                                WTSFreeMemory(buffer);
                            }
                            catch
                            {
                            }
                            buffer = IntPtr.Zero;
                        }

                    }
                    catch
                    {
                    }

                    switch (ClientProtocolType)
                    {

                        case 0:
                            {
                                //long  ResultCode = WFGetActiveProtocol(WTS_CURRENT_SESSION);
                                //switch (ResultCode )
                                //{
                                //    case 0: return "CONSOLE";
                                //    case 1: return "ICA";
                                //    default: return "Others( "+ResultCode.ToString()+" )";
                                //}
                                if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                                {
                                    return "RDP";
                                }
                                else
                                {
                                    return "CONSOLE";
                                }
                            }
                        case 1: if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                            {
                                return "RDP";
                            }
                            else
                            {
                                return "ICA";
                            }
                        case 2: return "RDP";
                        default: if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                            {
                                return "RDP";
                            }
                            else
                            {
                                return "Others( " + ClientProtocolType.ToString() + " )";
                            }
                    }

                }
            }
            catch
            {
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                {
                    try
                    {
                        WTSFreeMemory(buffer);
                    }
                    catch
                    {
                    }
                    buffer = IntPtr.Zero;
                }

            }
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                return "RDP";
            }
            else
            {
                return "CONSOLE";
            }
        }
      
    }
}
