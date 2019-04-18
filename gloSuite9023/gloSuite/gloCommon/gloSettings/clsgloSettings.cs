using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Reflection;
namespace gloSettings
{ 
    namespace DatabaseSetting
    {
        public class DataBaseSetting : IDisposable
        {
            #region " Private Variables "
            //private string _MessageBoxCaption = "gloPM";
            private string _sqlservername = "";
            private string _databasename = "";
            private bool _windowsauthentication = false;
            private string _sqlusername = "";
            private string _sqlpassword = "";

            //Report Database

            private string _rptsqlservername = "";
            private string _rptInstancename = "";
            private string _rptFoldername = "";
            private string _rptdatabasename = "";
            private bool _rptwindowsauthentication = false;
            private string _rptsqlusername = "";
            private string _rptsqlpassword = "";

            private string _emrsqlservername = "";
            private string _emrdatabasename = "";
            private bool _emrwindowsauthentication = false;
            private string _emrsqlusername = "";
            private string _emrsqlpassword = "";

            private string _databaseconnectionstring = "";
            private string _emrdatabaseconnectionstring = "";

            private string _ReportingSQLusername = "";
            private string _ReportingSQLpassword = "";
            private Boolean _blnShowReportDesigner = false;


            //private bool _GenerateHL7Message = false;  // For HL7 Message OutBound Queue
            //private bool _GenerateOutboundMessage = false; // Main parent setting 'Generate Outbound Message'
            //private bool _SendPatientDetails = false; //'Send Patient Detail' setting checkbox
            //private bool _SendAppointmentDetails = false; //'Send Appointment Detail' setting checkbox            



            private string _lastPatientID = "";

            // For [GENERALSETTINGS] Profile 
            private string _LockScreenTime = "10";
            //

            private string _title_sqlservername = "SQLSERVER";
            private string _title_databasename = "DATABASE";
            private string _title_windowsauthentication = "ISWINDOWSAUTHENTICATION";
            private string _title_sqlusername = "SQLUSERNAME";
            private string _title_sqlpassword = "SQLPASSWORD";
            private string _title_lastPatientID = "PATIENTID";
            private string _title_ShowReportDesigner = "SHOWREPORTDESIGNER";
            private string _title_LockScreenTime = "LOCKSCREENTIME";

            // --------
            private string _title_profileSettingsMain = "SETTINGS";
            private string _title_profileDBSettings = "DBSETTINGS";
            private string _title_profilePatient = "PATIENTSETTINGS";
            private string _title_profileReportDesignerSetting = "REPORTDESIGNERSETTINGS";
            private string _title_profileGeneralSettings = "GENERALSETTINGS";
            // private string _title_profileEMRDesignerSetting = "EMRSETTINGS";

            //private string _title_seprator = "=";

            private string _FilePath = "";

            private const string _encryptionKey = "12345678";

            //SQL Server Password Encyption key.
            private const string _SqlEncryptionKey = "20gloStreamInc08";

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
          
            #endregion "Private Variable "

            #region "Constructor & Distructor"

            public DataBaseSetting()
            {
                string filepath = "";
                filepath=FolderSettings.AppPrintSpoolerFolderPath;
                _FilePath = FolderSettings.AppFolderPath + "gloPMSSetting.XML";
            }
            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                // Take yourself off the Finalization queue 
                // to prevent finalization code for this object
                // from executing a second time.
                GC.SuppressFinalize(this);
            }

            // Dispose(bool disposing) executes in two distinct scenarios.
            // If disposing equals true, the method has been called directly
            // or indirectly by a user's code. Managed and unmanaged resources
            // can be disposed.
            // If disposing equals false, the method has been called by the 
            // runtime from inside the finalizer and you should not reference 
            // other objects. Only unmanaged resources can be disposed.
            protected virtual void Dispose(bool disposing)
            {
                // Check to see if Dispose has already been called.
                if (!this.disposed)
                {
                    // If disposing equals true, dispose all managed 
                    // and unmanaged resources.
                    if (disposing)
                    {
                        // Dispose managed resources.
                        ////Components.Dispose();

                    }
                    // Release unmanaged resources. If disposing is false, 
                    // only the following code is executed.
                    ////CloseHandle(handle);
                    ////handle = IntPtr.Zero;
                    // Note that this is not thread safe.
                    // Another thread could start disposing the object
                    // after the managed resources are disposed,
                    // but before the disposed flag is set to true.
                    // If thread safety is necessary, it must be
                    // implemented by the client.

                }
                disposed = true;
            }

            // Use C# destructor syntax for finalization code.
            // This destructor will run only if the Dispose method 
            // does not get called.
            // It gives your base class the opportunity to finalize.
            // Do not provide destructors in types derived from this class.
            ~DataBaseSetting()
            {
                // Do not re-create Dispose clean-up code here.
                // Calling Dispose(false) is optimal in terms of
                // readability and maintainability.
                Dispose(false);
            }

            #endregion

            #region "Setting Properties"

            //Code Added by Sagar Ghodke - 04 Feb 2008
            public string LastPatient
            {
                get { return _lastPatientID; }
                set { _lastPatientID = value; }

            }
            //

            public string SQLServerName
            {
                get { return _sqlservername; }
                set { _sqlservername = value; }
            }

            public string DatabaseName
            {
                get { return _databasename; }
                set { _databasename = value; }
            }

            public bool WindowsAuthentication
            {
                get { return _windowsauthentication; }
                set { _windowsauthentication = value; }
            }

            public string LoginUser
            {
                get { return _sqlusername; }
                set { _sqlusername = value; }
            }

            public string LoginPassword
            {
                get { return _sqlpassword; }
                set { _sqlpassword = value; }
            }

            //
            public string EMRSQLServerName
            {
                get { return _emrsqlservername; }
                set { _emrsqlservername = value; }
            }

            public string EMRDatabaseName
            {
                get { return _emrdatabasename; }
                set { _emrdatabasename = value; }
            }

            public bool EMRWindowsAuthentication
            {
                get { return _emrwindowsauthentication; }
                set { _emrwindowsauthentication = value; }
            }

            public string EMRLoginUser
            {
                get { return _emrsqlusername; }
                set { _emrsqlusername = value; }
            }

            public string EMRLoginPassword
            {
                get { return _emrsqlpassword; }
                set { _emrsqlpassword = value; }
            }
            //

            public string ReportingSQLUserName
            {
                get { return _ReportingSQLusername; }
                set { _ReportingSQLusername = value; }
            }

            public string ReportingSQLPassword
            {
                get { return _ReportingSQLpassword; }
                set { _ReportingSQLpassword = value; }
            }

            public Boolean ShowReportDesigner
            {
                get { return _blnShowReportDesigner; }
                set { _blnShowReportDesigner = value; }
            }

            public string DBConnectionString
            {
                get { return _databaseconnectionstring; }
                set { _databaseconnectionstring = value; }
            }

            public string EMRDBConnectionString
            {
                get { return _emrdatabaseconnectionstring; }
                set { _emrdatabaseconnectionstring = value; }
            }

            //public bool GenerateHL7Message
            //{
            //    get { return _GenerateHL7Message; }
            //    set { _GenerateHL7Message = value; }
            //}

            ////Added by Abhijeet on 20110926
            //public bool SendPatientDetails
            //{
            //    get { return _SendPatientDetails;}
            //    set { _SendPatientDetails = value; }
            //}
            //public bool SendAppointmentDetails
            //{
            //    get { return _SendAppointmentDetails; }
            //    set { _SendAppointmentDetails = value; }
            //}

            //public bool GenerateOutboundMessage
            //{
            //    get {return _GenerateOutboundMessage;}
            //    set {_GenerateOutboundMessage=value ;}
            //}
            //End of changes by Abhijeet on 20110926

            public string LockScreenTime
            {
                get { return _LockScreenTime; }
                set { _LockScreenTime = value; }
            }


            public string ProfileDBSettings
            {
                get { return _title_profileDBSettings; }
                set { _title_profileDBSettings = value; }
            }
            public string ProfileReportDesigner
            {
                get { return _title_profileReportDesignerSetting; }
                set { _title_profileReportDesignerSetting = value; }
            }
            public string ProfilePatientSettings
            {
                get { return _title_profilePatient; }
                set { _title_profilePatient = value; }
            }
            public string ProfileGeneralSettings
            {
                get { return _title_profileGeneralSettings; }
                set { _title_profileGeneralSettings = value; }
            }

            //public string FilePath
            //{
            //    get { _FilePath; }
            //}

            #endregion


            #region "Report Database connection validation"

            private bool ValidateReportDatabaseSettings()
            {
                bool result;
                try
                {
                    // Validate DB Connection for PM
                    if (ReportDB_ConnectionOpen() == false)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    result = false;
                }
                return result;
            }

            private bool ReportDB_ConnectionOpen()
            {
                bool _result;
                string _connstring = "";
                try
                {
                    if (_windowsauthentication == false)
                    {   //SQL authentication
                        _connstring = "Server=" + _rptInstancename + ";Database=" + _rptdatabasename + ";Uid=" + _rptsqlusername + ";Pwd=" + _rptsqlpassword + ";";
                    }
                    else
                    {   //windows authentication
                        _connstring = "Server=" + _rptInstancename + ";Database=" + _rptdatabasename + ";Trusted_Connection=yes;";
                    }

                    System.Data.SqlClient.SqlConnection _connection = new System.Data.SqlClient.SqlConnection();
                    _connection.ConnectionString = _connstring;

                    _connection.Open();

                    // If for given Credential Set the Connection String to Private Variable 
                    _databaseconnectionstring = _connstring;
                    //
                    _connection.Close();

                    _result = true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    _result = false;
                }
                finally
                { _connstring = null; }
                return _result;
            }
            #endregion


            #region "Public Methods"

            public bool ShowDatabaseSettings(Form oParent)
            {
                frmDatabaseSettings oSetting = new frmDatabaseSettings();
                try
                {
                    GetDatabaseSettings_Registry();
                    GetXMLSettings();

                    //frmDatabaseSettings oSetting = new frmDatabaseSettings();
                    oSetting.PMSSQLServerName = _sqlservername;
                    oSetting.PMSSQLDatabaseName = _databasename;
                    oSetting.PMSWindowsAuthentication = _windowsauthentication;
                    oSetting.PMSSQLusername = _sqlusername;
                    oSetting.PMSSQLpassword = _sqlpassword;
                    oSetting.ShowReportDesigner = _blnShowReportDesigner;

                    // if the Connection String is Valid then Retrive the Settings 
                    if (_databaseconnectionstring != "" && ValidateDatabaseSettings())
                    {
                        //  Retrive the ReportDesigner's SQlServer Username & Password (UserName,Password)
                        object oValue = new object();
                        GetSetting("UserName", out oValue);
                        if (oValue != null)
                        {
                            _ReportingSQLusername = oValue.ToString();
                            oValue = null;
                        }

                        GetSetting("Password", out oValue);
                        if (oValue != null)
                        {
                            _ReportingSQLpassword = oValue.ToString();
                            oValue = null;
                        }
                    }
                    oSetting.RPTSQLusername = _ReportingSQLusername;
                    oSetting.RPTSQLpassword = _ReportingSQLpassword;

                    if (oSetting.ShowDialog(oParent) == DialogResult.OK)
                    {
                        _sqlservername = oSetting.PMSSQLServerName;
                        _databasename = oSetting.PMSSQLDatabaseName;
                        _windowsauthentication = oSetting.PMSWindowsAuthentication;
                        _sqlusername = oSetting.PMSSQLusername;
                        _sqlpassword = oSetting.PMSSQLpassword;
                        _blnShowReportDesigner = oSetting.ShowReportDesigner;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    if (oSetting != null)
                    {
                        oSetting.Dispose();
                        oSetting = null;
                    }
                }
            }

            public bool ShowGeneralSettings(Form oParent)
            {
                bool result = false;
                try
                {
                    GetXMLSettings();

                    frmSettings frm = new frmSettings(_databaseconnectionstring);
                    frm.ShowInTaskbar = false;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.LockScreenTime = _LockScreenTime;
                    if (frm.ShowDialog(oParent) == DialogResult.OK)
                    {
                        _LockScreenTime = frm.LockScreenTime.ToString();
                        result = true;
                    }
                    frm.Dispose();
                    frm = null;
                }
                catch (Exception ex)
                {
                    result = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                return result;
            }


            #region "Check Setting For Report Designer"

            public bool GetDatabaseSettings_Registry_Reports()
            {
                //Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
                gloRegistrySetting.OpenRemoteBaseKey();
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM);

                Boolean _Result = false;
                try
                {

                    if (gloRegistrySetting.IsRegistryKeyExists(gloRegistrySetting.gstrSoftPM) != false)
                    {
                        // Read Setting From Registry
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_SERVER")).Trim() != "")
                        {
                            _rptsqlservername = Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_SERVER"));
                        }
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_InstanceName")).Trim() != "")
                        {
                            _rptInstancename = Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_InstanceName"));
                        }

                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_Database")) != "")
                        {
                            _rptdatabasename = Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_Database"));
                        }

                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("ReportFolder")) != "")
                        {
                            _rptFoldername = Convert.ToString(gloRegistrySetting.GetRegistryValue("ReportFolder"));
                        }

                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_ISWINAUTHENTICATION")) != "")
                        {
                            _rptwindowsauthentication = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("RPT_ISWINAUTHENTICATION"));
                        }
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_SQLUSERNAME")) != "")
                        {
                            _rptsqlusername = Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_SQLUSERNAME"));
                        }
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_SQLPASSWORD")) != "")
                        {
                            gloSecurity.ClsEncryption oDecrypt = new gloSecurity.ClsEncryption();
                            string d_sPassword = oDecrypt.DecryptFromBase64String(Convert.ToString(gloRegistrySetting.GetRegistryValue("RPT_SQLPASSWORD")), _SqlEncryptionKey);
                            
                            if (oDecrypt != null) { oDecrypt.Dispose(); oDecrypt = null; }
                            
                            _rptsqlpassword = d_sPassword;
                        }

                        // Validate Database Connection
                        if (_databasename.Trim() != "" && ValidateReportDatabaseSettings())
                        {
                            _Result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    _Result = false;
                }
                finally
                {
                    gloRegistrySetting.CloseRegistryKey();
                }
                return _Result;
            }

            #endregion

            public bool GetDatabaseSettings_Registry()
            {
                //gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();

                //RegistryKey oRegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, System.Windows.Forms.SystemInformation.ComputerName);
                //oRegistryKey = oRegistryKey.OpenSubKey("Software\\gloPM");
                //Boolean _Result = false;
                //try
                //{

                //    if (oRegistryKey != null)
                //    {
                //        // Read Setting From Registry
                //        if (Convert.ToString(oRegistryKey.GetValue("SQLSERVER")) != "")
                //        {
                //            _sqlservername = Convert.ToString(oRegistryKey.GetValue("SQLSERVER"));
                //        }
                //        if (Convert.ToString(oRegistryKey.GetValue("Database")) != "")
                //        {
                //            _databasename = Convert.ToString(oRegistryKey.GetValue("Database"));
                //        }
                //        if (Convert.ToString(oRegistryKey.GetValue("ISWINAUTHENTICATION")) != "")
                //        {
                //            _windowsauthentication = Convert.ToBoolean(oRegistryKey.GetValue("ISWINAUTHENTICATION"));
                //        }
                //        if (Convert.ToString(oRegistryKey.GetValue("SQLUSERNAME")) != "")
                //        {
                //            _sqlusername = Convert.ToString(oRegistryKey.GetValue("SQLUSERNAME"));
                //        }
                //        if (Convert.ToString(oRegistryKey.GetValue("SQLPASSWORD")) != "")
                //        {
                //            gloSecurity.ClsEncryption oDecrypt = new gloSecurity.ClsEncryption();
                //            string d_sPassword = oDecrypt.DecryptFromBase64String(Convert.ToString(oRegistryKey.GetValue("SQLPASSWORD")), _SqlEncryptionKey);

                //            _sqlpassword = d_sPassword;
                //        }

                //        //Read HL7 Settings
                //        Boolean isHL7MessageSettings = false; 
                //        if (Convert.ToString(oRegistryKey.GetValue("GenerateHL7Message")) != "")
                //        {
                //            isHL7MessageSettings = true; 
                //            _GenerateHL7Message = Convert.ToBoolean(oRegistryKey.GetValue("GenerateHL7Message"));
                //        }
                //        else
                //        {
                //            isHL7MessageSettings = false;
                //        }

                //        oRegistryKey.Close();

                //        //if HL7 setting not present in registry write it to registry
                //        if (isHL7MessageSettings == false)
                //        {
                //            oRegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, System.Windows.Forms.SystemInformation.ComputerName);
                //            oRegistryKey = oRegistryKey.CreateSubKey("Software\\gloPM");
                //            _GenerateHL7Message = false;
                //            oRegistryKey.SetValue("GenerateHL7Message", false);

                //            oRegistryKey.Close();  
                //        }
                //        //----------


                //        // Validate Database Connection
                //        if (_databasename.Trim() != "" && ValidateDatabaseSettings())
                //        {
                //            _Result = true;
                //        }
                //    }


                //}





                //****** code added by sandip dhakane 20100707
                //Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
                gloRegistrySetting.OpenRemoteBaseKey();
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM);


                Boolean _Result = false;
                try
                {
                    if (gloRegistrySetting.IsRegistryKeyExists(gloRegistrySetting.gstrSoftPM) != false)
                    {
                        // Read Setting From Registry
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLSERVER")) != "")
                        {
                            _sqlservername = Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLSERVER"));
                        }
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("Database")) != "")
                        {
                            _databasename = Convert.ToString(gloRegistrySetting.GetRegistryValue("Database"));
                        }
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("ISWINAUTHENTICATION")) != "")
                        {
                            _windowsauthentication = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("ISWINAUTHENTICATION"));
                        }
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLUSERNAME")) != "")
                        {
                            _sqlusername = Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLUSERNAME"));
                        }
                        if (Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLPASSWORD")) != "")
                        {
                            gloSecurity.ClsEncryption oDecrypt = new gloSecurity.ClsEncryption();
                            string d_sPassword = oDecrypt.DecryptFromBase64String(Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLPASSWORD")), _SqlEncryptionKey);
                            if (oDecrypt != null) { oDecrypt.Dispose(); oDecrypt = null; }
                            _sqlpassword = d_sPassword;
                        }

                        //Code modified by Abhijeet on 20110926. commented cod eto use old combined setting of HL7 outbound
                        // set variables for nes setting. if new settings are not available in registry then value for
                        // thsese setting in appsetting setting file set with value of old combined setting 'GenerateHL7Message'

                        //Read HL7 Settings

                        //Boolean isHL7MessageSettings = false;
                        //if (Convert.ToString(gloRegistrySetting.GetRegistryValue("GenerateHL7Message")) != "")
                        //{
                        //    isHL7MessageSettings = true;
                        //    _GenerateHL7Message = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("GenerateHL7Message"));
                        //}
                        //else
                        //{
                        //    isHL7MessageSettings = false;
                        //}
                        ////if HL7 setting not present in registry write it to registry
                        //if (isHL7MessageSettings == false)
                        //{
                        //    gloRegistrySetting.OpenRemoteBaseKey();
                        //    gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrSoftPM);
                        //    _GenerateHL7Message = false;
                        //    gloRegistrySetting.SetRegistryValue("GenerateHL7Message", false);

                        //    gloRegistrySetting.CloseRegistryKey();
                        //}
                        //----------

                        //if (gloRegistrySetting.GetRegistryValue("GenerateHL7Message") != null)
                        //{
                        //    if (Convert.ToString(gloRegistrySetting.GetRegistryValue("GenerateHL7Message")) != "")
                        //    {
                        //        _GenerateHL7Message = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("GenerateHL7Message"));
                        //    }
                        //    else
                        //    {
                        //        _GenerateHL7Message = false;
                        //    }
                        //}
                        //else
                        //{
                        //    _GenerateHL7Message = false;
                        //}

                        //if (gloRegistrySetting.GetRegistryValue("GenerateOutboundMessage") != null)
                        //{
                        //    if (Convert.ToString(gloRegistrySetting.GetRegistryValue("GenerateOutboundMessage")) != "")
                        //    {
                        //        _GenerateOutboundMessage = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("GenerateOutboundMessage"));
                        //    }
                        //    else
                        //    {
                        //        _GenerateOutboundMessage = false;
                        //    }
                        //}
                        //else
                        //{
                        //    //One time setting as splited one setting to multiple flags as per EMR for more contrl on type of OutBound in //6050
                        //    _GenerateOutboundMessage = _GenerateHL7Message;
                        //}


                        //if (gloRegistrySetting.GetRegistryValue("SendPatientDetails") != null)
                        //{
                        //    if (Convert.ToString(gloRegistrySetting.GetRegistryValue("SendPatientDetails")) != "")
                        //    {
                        //        _SendPatientDetails = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("SendPatientDetails"));
                        //    }
                        //    else
                        //    {
                        //        _SendPatientDetails = false;
                        //    }
                        //}
                        //else
                        //{
                        //    //One time setting as splited one setting to multiple flags as per EMR for more contrl on type of OutBound in //6050
                        //    _SendPatientDetails = _GenerateHL7Message;
                        //}

                        //HL7 outbound setting retrive from Database.
                        //if (gloRegistrySetting.GetRegistryValue("SendAppointmentDetails")!=null)
                        //{
                        //    if (Convert.ToString(gloRegistrySetting.GetRegistryValue("SendAppointmentDetails")) != "")
                        //    {
                        //        _SendAppointmentDetails = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("SendAppointmentDetails"));
                        //    }
                        //    else
                        //    {
                        //        _SendAppointmentDetails = false;
                        //    }
                        //}
                        //else
                        //{
                        //    //One time setting as splited one setting to multiple flags as per EMR for more contrl on type of OutBound in //6050
                        //    _SendAppointmentDetails = _GenerateHL7Message;
                        //}

                        // Validate Database Connection
                        if (_databasename.Trim() != "" && ValidateDatabaseSettings())
                        {
                            _Result = true;
                        }
                    }

                }

                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    _Result = false;
                }
                finally
                {
                    gloRegistrySetting.CloseRegistryKey();
                }
                return _Result;
            }

            public bool GetXMLSettings()
            {
                bool result = false;
                DataSet ds = new DataSet();
                try
                {
                    if (ReadSettings_XML(out ds) == true)
                    {

                        // Set the Report Designer
                        try
                        {
                            _blnShowReportDesigner = Convert.ToBoolean(ds.Tables[_title_profileReportDesignerSetting].Rows[0][_title_ShowReportDesigner]);
                        }
                        catch { }

                        // Set the Lock Screen Timing
                        try
                        {
                            _LockScreenTime = Convert.ToString(ds.Tables[_title_profileGeneralSettings].Rows[0][_title_LockScreenTime]);
                        }
                        catch { }

                        try
                        {
                            _lastPatientID = ds.Tables[_title_profilePatient].Rows[0][_title_lastPatientID].ToString();
                        }
                        catch { }

                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    result = false;
                }
                finally
                {
                    if (ds != null) { ds.Dispose(); ds = null; }
                }
                return result;
            }




            #endregion

            #region "Setting File using XML "
            /// <summary>
            /// TO Read the Settings from The XML File
            /// </summary>
            /// <returns> Boolean if Successfully read </returns>
            public bool ReadSettings_XML(out DataSet ds)
            {
                if (File.Exists(_FilePath) == true)
                {// File Exists
                    ds = new DataSet();
                    try
                    {
                        ds.ReadXml(_FilePath, XmlReadMode.InferSchema);
                    }
                    catch
                    {
                        //GLO2010-0009043
                        //All users are now getting the attached error when in use or even just in waiting. Users were only getting this before every time when attempting to 'Save & Close' screen to scree

                    }

                    return true;
                }
                else
                {// File is Not Exists then Do Not 
                    ds = null;
                    return false;
                }

                //XmlDocument XMLDoc= new XmlDocument() ;
                //XmlNodeList NodeList ;
                //XmlNode Node;

                //XMLDoc.Load(_FilePath);
                //XMLDoc.SelectNodes("/Settings");



                //ds.Tables();



            }

            /// <summary>
            /// To Get the Value of Setting From the XML File
            /// </summary>
            /// <param name="profileName"> Name of the Profile</param>
            /// <param name="fieldName"> BName of the Setting</param>
            /// <returns> returns value of the Setting </returns>
            public string ReadSettings_XML(string profileName, string fieldName)
            {
                DataSet ds = new DataSet();
                string value = "";

                if (File.Exists(_FilePath) == true)
                {
                    try
                    {
                        ds.ReadXml(_FilePath, XmlReadMode.InferSchema);
                    }
                    catch
                    {
                        //GLO2010-0009043
                        //All users are now getting the attached error when in use or even just in waiting. Users were only getting this before every time when attempting to 'Save & Close' screen to scree

                    }
                }
                else
                {
                    return value;
                }


                if (ds != null)
                {
                    if (ds.Tables.Contains(profileName) == true)
                    {
                        if (ds.Tables[profileName].Columns.Contains(fieldName) == true)
                        {
                            if (ds.Tables[profileName].Rows.Count > 0)
                            {
                                value = ds.Tables[profileName].Rows[0][fieldName].ToString();
                            }
                        }
                    }
                }
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                return value;
            }

            public bool WriteSettings_XML()
            {
                XmlTextWriter oXmlTextWriter = null;
                gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                try
                {
                    oXmlTextWriter = new XmlTextWriter(_FilePath, null);

                    oXmlTextWriter.WriteStartDocument();

                    oXmlTextWriter.WriteStartElement(_title_profileSettingsMain);

                    //-------
                    // Add Element "DB Settings"
                    oXmlTextWriter.WriteStartElement(_title_profileDBSettings);
                    // Add Element String to DB Settings
                    oXmlTextWriter.WriteElementString(_title_sqlservername, _sqlservername);
                    oXmlTextWriter.WriteElementString(_title_databasename, _databasename);
                    oXmlTextWriter.WriteElementString(_title_windowsauthentication, _windowsauthentication.ToString());
                    oXmlTextWriter.WriteElementString(_title_sqlusername, oClsEncryption.EncryptToBase64String(_sqlusername, _SqlEncryptionKey));
                    oXmlTextWriter.WriteElementString(_title_sqlpassword, oClsEncryption.EncryptToBase64String(_sqlpassword, _SqlEncryptionKey));
                    //
                    oXmlTextWriter.WriteEndElement();
                    // End Of Element "DB Settings"
                    //-------

                    //-------
                    // Add Element "Report Designer Settings"
                    oXmlTextWriter.WriteStartElement(_title_profileReportDesignerSetting);

                    // Add Element String to Report Designer Settings
                    oXmlTextWriter.WriteElementString(_title_ShowReportDesigner, _blnShowReportDesigner.ToString());

                    // End Of Element "Report Designer Settings"
                    oXmlTextWriter.WriteEndElement();
                    //-------

                    //-------
                    // Add Element "Patient Settings"
                    oXmlTextWriter.WriteStartElement(_title_profilePatient);

                    // Add Element String to Patient Setttings
                    oXmlTextWriter.WriteElementString(_title_lastPatientID, _lastPatientID);

                    // End Of Element "Patient Settings"
                    oXmlTextWriter.WriteEndElement();
                    //-------

                    //-------
                    // Add Element "General Settings"
                    oXmlTextWriter.WriteStartElement(_title_profileGeneralSettings);

                    // Add Lock Screen Time Element String to General Setttings 
                    oXmlTextWriter.WriteElementString(_title_LockScreenTime, _LockScreenTime);

                    // End Of Element "General Settings"
                    oXmlTextWriter.WriteEndElement();
                    //-------

                    oXmlTextWriter.WriteEndElement(); // End Of Element "Settings"

                    oXmlTextWriter.WriteEndDocument();
                    // oXmlTextWriter.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    if (oXmlTextWriter != null)
                    {
                        oXmlTextWriter.Close();
                        oXmlTextWriter = null;
                    }

                    if (oClsEncryption != null)
                    {
                        oClsEncryption.Dispose();
                        oClsEncryption = null;
                    }
                }
            }

            /// <summary>
            /// To Add /Modify the Setting value if File is already Exist
            /// </summary>
            /// <param name="profileName">
            /// Name of the Profile eg.  
            /// </param>
            /// <param name="fieldName">
            /// Name of the Field
            /// </param>
            /// <param name="value">
            /// Value of the Field
            /// </param>
            /// <returns>
            /// returns true if Successful else false
            /// </returns>
            public bool WriteSettings_XML(string profileName, string fieldName, string value)
            {
                // XmlTextWriter oXmlTextWriter = null;
                DataSet ds = new DataSet();

                try
                {
                    // First read the Settings from XML File 
                    // check for the Profile Existance
                    // check for the Field Avilable or Not if not then Add 
                    // add / Modify the Value of respective field


                    //DataSet ds = new DataSet();

                    // Check if File is Exits
                    if (File.Exists(_FilePath) == false)
                    {
                        // IF File IS Not Exists the Create it
                        //File.Create(_FilePath);

                        // Write a Element for Settings in the XML File which is Newly Created
                        XmlTextWriter oXmlTextWriter = null;

                        oXmlTextWriter = new XmlTextWriter(_FilePath, null);

                        oXmlTextWriter.WriteStartDocument();

                        // Add SETTINGS
                        oXmlTextWriter.WriteStartElement(_title_profileSettingsMain);
                        oXmlTextWriter.WriteEndElement();

                        if (oXmlTextWriter != null)
                            oXmlTextWriter.Close();
                    }

                    // Read the Setting from XML File & get it into the DataSet
                    try
                    {
                        ds.ReadXml(_FilePath, XmlReadMode.InferSchema);
                    }
                    catch
                    {
                        //GLO2010-0009043
                        //All users are now getting the attached error when in use or even just in waiting. Users were only getting this before every time when attempting to 'Save & Close' screen to scree

                    }

                    //  oXmlTextWriter = new XmlTextWriter(_FilePath, null);



                    // Check for profile existance
                    if (ds.Tables.Contains(profileName) == true)
                    { // Profile Exists

                        // Check for the Field
                        if (ds.Tables[profileName].Columns.Contains(fieldName) == true)
                        {// field exists
                            ds.Tables[profileName].Rows[0][fieldName] = value;
                        }
                        else
                        {// field does not exists 

                            // Add Lock Screen Time Element String to General Setttings 
                            //oXmlTextWriter.WriteElementString(fieldName.ToUpper(), value);
                            ds.Tables[profileName.ToUpper()].Columns.Add(fieldName.ToUpper());
                            if (ds.Tables[profileName.ToUpper()].Rows.Count > 0)
                            {
                                ds.Tables[profileName.ToUpper()].Rows[0][fieldName.ToUpper()] = value;
                            }
                            else
                            {
                                DataRow r;
                                r = ds.Tables[profileName.ToUpper()].NewRow();
                                r[fieldName.ToUpper()] = value;
                                ds.Tables[profileName.ToUpper()].Rows.Add(r);
                            }
                        }
                    }
                    else
                    { // If the Profile  is Not Exists
                        //-------
                        // Add Element "General Settings"

                        //oXmlTextWriter.WriteStartElement(profileName.ToUpper());
                        ds.Tables.Add(profileName.ToUpper());

                        // Add Lock Screen Time Element String to General Setttings 
                        //oXmlTextWriter.WriteElementString(fieldName.ToUpper(), value);
                        ds.Tables[profileName.ToUpper()].Columns.Add(fieldName.ToUpper());

                        if (ds.Tables[profileName.ToUpper()].Rows.Count > 0)
                        {
                            ds.Tables[profileName.ToUpper()].Rows[0][fieldName.ToUpper()] = value;
                        }
                        else
                        {
                            DataRow r;
                            r = ds.Tables[profileName.ToUpper()].NewRow();
                            r[fieldName.ToUpper()] = value;
                            ds.Tables[profileName.ToUpper()].Rows.Add(r);
                        }


                        // End Of Element "General Settings"
                        //oXmlTextWriter.WriteEndElement();
                        //-------
                    }

                    //  oXmlTextWriter.Close();
                    try
                    {
                        ds.WriteXml(_FilePath, XmlWriteMode.IgnoreSchema);
                    }
                    catch
                    {
                        //GLO2010-0009043
                        //All users are now getting the attached error when in use or even just in waiting. Users were only getting this before every time when attempting to 'Save & Close' screen to scree
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    //GLO2010-0009043
                    //All users are now getting the attached error when in use or even just in waiting. Users were only getting this before every time when attempting to 'Save & Close' screen to scree
                    //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    if (ds != null)
                    {
                        ds.Dispose();
                        ds = null;
                    }
                    //if (oXmlTextWriter != null)
                    //    oXmlTextWriter.Close();
                }
            }
            #endregion

            #region "Private methods"

            private bool ValidateDatabaseSettings()
            {
                bool result;
                try
                {
                    // Validate DB Connection for PM
                    if (ConnectionOpen() == false)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    result = false;
                }
                return result;
            }

            private bool ConnectionOpen()
            {
                bool _result;
                string _connstring = "";
                System.Data.SqlClient.SqlConnection _connection = new System.Data.SqlClient.SqlConnection();
                try
                {
                    if (_windowsauthentication == false)
                    {   //SQL authentication
                        _connstring = "Server=" + _sqlservername + ";Database=" + _databasename + ";Uid=" + _sqlusername + ";Pwd=" + _sqlpassword + ";";
                    }
                    else
                    {   //windows authentication
                        _connstring = "Server=" + _sqlservername + ";Database=" + _databasename + ";Trusted_Connection=yes;";
                    }

                    //System.Data.SqlClient.SqlConnection _connection = new System.Data.SqlClient.SqlConnection();
                    _connection.ConnectionString = _connstring;

                    _connection.Open();

                    // If for given Credential Set the Connection String to Private Variable 
                    _databaseconnectionstring = _connstring;
                    //
                    _connection.Close();

                    _result = true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    _result = false;
                }
                finally
                {
                    if (_connection != null)
                    {

                        _connection.Dispose();
                        _connection = null;
                    }
                    _connstring = null;
                }
                return _result;
            }

            /// <summary>
            /// To Get the Value of the Setting from The Database
            /// </summary>
            /// <param name="SettingName"> Name of Setting</param>
            /// <param name="Value"> Gets the Value of Respective Setting if Exits else set to 'null'</param>
            private void GetSetting(string SettingName, out object Value)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    oDB.Connect(false);
                    oDBParameters.Add("@sSettingsName", SettingName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                    Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" + SettingName + "'");
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    Value = null;
                    DBErr.ERROR_Log(DBErr.Message);
                }
                catch (Exception ex)
                {
                    Value = null;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();

                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDBParameters != null)
                    {
                        oDBParameters.Dispose();
                    }
                }
            }

            #endregion
        }
    }
    public enum enumDefaultClinicalChart
    {
        Patient_Information = 1,
        Current_Medications = 2,
        Lab_Flowsheet = 3,
        Labs = 4,
        Patient_Exams = 5,
        Scanned_Documents = 6,
        Orders = 7,
        Referral_Letters = 8,
        Messages = 9,
        Nurse_Notes = 10,
        Triage = 11,
        PT_Protocol = 12,
        Patient_Consent = 13,
        Disclosure_Management = 14,
        Patient_Letters = 15,
        Form_Gallery = 16,
        Claims = 17,
        Patient_Forms = 19,
        Patient_Education = 21
    }
    public enum SettingFlag
    {
        None = 0,
        Clinic = 1,
        User = 2,
    }
    public enum TypeOfBilling   //Added only For Admin Settings
    {
        None = 0,
        Paper = 2,
        Electronic = 1,
        UB04Electronic = 3,
        UB04Paper = 4,
        CMS1500New = 5
    }

    public enum BatchBillingMethod   //For Batch billing method
    {
        None = 0,
        CMS1500 = 2,
        ElectronicProfessionalANSI4010 = 1,
        ElectronicInstitutionalANSI4010 = 3,
        UB04 = 4,
        ElectronicProfessionalANSI5010 = 5,
        Multiple = 6,
        ElectronicInstitutionalANSI5010 = 7,
        CMS1500New = 8,
    }

    public enum AlternateIDSettingLevel
    {
        Clinic = 10,
        ClinicProvider = 20,
        InsurancePlan = 30,
        InsurancePlanProvider = 40
    }
    //gloPM6010
    public enum ExpandedClaimSettingLevel
    {
        None = 0,
        Clinic = 10,
        InsuranceCompany = 20,
        InsurancePlan = 30

    }

    public enum AlternateIDSource
    {
        Billing_Provider = 1,
        Billing_Provider_Company = 2,
        Claim_Facility = 3,
        Clinic = 4
    }

    public enum ModuleOfGridColumn
    {
        Dashboard = 1,
        Billing = 2,
        PatientStrip = 3,
        PatientList = 4,
        Appointment = 5,
        Schedule = 6,
        DashBoardPatientInsurance = 7,
        DashBoardPatientAppointments = 8,
        DashBoardPatientReferrals = 9,
        DashBoardPatientProcedures = 10,
        DashBoardPatientBilling = 11,
        DashBoardPatientBalance = 12,
        DashBoardPatientList = 13,
        PaymentSinglePaymentGrid = 14,
        PaymentMultiplePaymentGrid = 15,
        PaymentTotalSinglePaymentGrid = 16,
        PaymentTotalMultiplePaymentGrid = 17,
        DashBoardPMPriorAuthorization = 18,
        DashBoardPatientCases = 20,
        //Addded by madan for labResultGrid.
        LabResultGrid = 19,
        //End madan.
        DashBoardPatientTasks = 21,
        ViewBenefit = 22,
        Reconciliation=23,
        SecureReferal=24,
        DashBoardPatientNYWorkersCompForms = 25
    }
    public enum PaperBillingSettingLevel
    {
        None = 0,
        Clinic = 10,
        InsuranceCompany = 20,
        InsurancePlan = 30
    }

    public enum Box29Names
    {
        AllPriorPaymentsandAdjustments = 1,
        AllPriorPayments, NoAdjustments = 2,
        AllPriorPaymentsandAdjustmentsMinusMedicare = 3,
        AllPriorPaymentsandAdjustmentsMinusPatientPayments = 4,
        OnlyPriorPatientPayments = 5

    }
    public enum Box30Names
    {
        Balance = 1,
        Box28MinusBox29 = 2,
        OriginalChargeAmount = 3,
        Blank = 4,
        Zero = 5
    }
    public enum Box23Setting
    {
        Box23 = 1,
        Box19 = 2
    }
    public enum PaperBillingBoxtype
    {
        Box29 = 29,
        Box30 = 30,
        Box23 = 23
    }

    public enum ClearingHouseType
    {
        None = 0,
        GatewayEDI = 1,
        RealMed = 2,
        Other = 3,
    }
    public enum ANSIVersions
    {
        ANSI_4010 = 1,
        ANSI_5010 = 2
    }

    public enum PaperFormVersion
    {
         [Description("CMS1500 08/05")]
         CMS1500 = 1,
         [Description("CMS1500 02/12")]
        CMS1500New = 2,
       
    }

    
    public enum GPPromptOutput
    {
        Single_StoptoReviewCharges,
        Single_StoptoReviewGlobalPeriodDetails,
        Single_ContinueSaveofNewCharges,
        Multiple_StoptoReviewCharges,
        Multiple_StoptoReviewGlobalPeriodDetails,
        Multiple_ContinueSaveofNewCharges
    }

    public enum ExternalChargesType
    {
        gloEMRTreatment = 1,
        HL7InboundCharges = 2,
        gloEMRnHL7InboundCharges = 3
    }
    public enum AccidentType
    {
        None = 0, Work = 1, Auto = 2, Other = 3
    }

    public class GeneralSettings
    {
        private string _databaseConnectionString = "";
        private string _messageBoxCaption = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #region "Constructor & Distructor"

        public GeneralSettings(string DatabaseConnectionString)
        {
            _databaseConnectionString = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"].ToString() != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            {
                _ClinicID = 1;
            }

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~GeneralSettings()
        {
            Dispose(false);
        }

        #endregion

        //public void AddSetting(string SettingName, string Value)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {

        //        oDB.Connect(false);
        //        oDBParameters.Add("@sSettingsName", SettingName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //        oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

        //        oDB.Execute("gsp_InUpSettings", oDBParameters);
        //    }
        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //        DBErr.ERROR_Log(DBErr.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDBParameters.Dispose();
        //        oDB.Dispose();
        //    }
        //}

        //public bool AddSetting(string Name, string Value)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {
        //        oDB.Connect(false);

        //        oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //        oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //        oDB.Execute("gsp_Settings", oDBParameters);
        //        return true;

        //    }
        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDBParameters.Dispose();
        //        oDB.Dispose();
        //    }
        //}

        public bool AddSetting(string Name, string Value, Int64 ClinicID, Int64 UserID, SettingFlag UserClinicFlag)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDB.Execute("gsp_InUpSettings", oDBParameters);
                return true;

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
                return false;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
        }

        public bool DeleteSetting(string SettingName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDB.Execute_Query("DELETE FROM Settings WHERE sSettingsName = '" + SettingName + "' AND nClinicID = " + _ClinicID + "");
                return true;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.Message);
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
        }

        



        public DataTable GetEnumItemsDescriptionPaperFormVersion(Enum _enm, bool _bForComboBoxFilling)
        { 
            System.Type _oEnmType = _enm.GetType();
            TypeCode tp = _enm.GetTypeCode();
            DataTable _dt = new DataTable();
           
            Array arrValues = System.Enum.GetValues(_oEnmType);
                        
            Int32 iVal = default(Int32);
            try
            {
                _dt.Columns.Add("nID", typeof(System.Int16));
                _dt.Columns.Add("sDescription", typeof(System.String));

                foreach (int ival_loopVariable in arrValues)
                {
                    iVal = ival_loopVariable;
                    string sVal = System.Enum.GetName(_oEnmType, iVal);

                    string sDescription = ((PaperFormVersion)Enum.Parse(typeof(PaperFormVersion), sVal)).GetDescription();
                                                       
                    DataRow _dr = _dt.NewRow();
                    _dr["nID"] = iVal;
                    _dr["sDescription"] = sDescription;
                    _dt.Rows.Add(_dr);
                    sVal = null;
                    sDescription = null;
                }

                if (_bForComboBoxFilling)
                {
                    DataRow _drBlank = _dt.NewRow();
                    _drBlank["nID"] = 0;
                    _drBlank["sDescription"] = "";
                    _dt.Rows.InsertAt(_drBlank, 0);

                }
                _dt.AcceptChanges();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                _oEnmType = null;
                arrValues = null;
            }


            return _dt;
        }




        public DataTable GetEnumItems(Enum _enm, bool _bForComboBoxFilling)
        {
            System.Type _oEnmType = _enm.GetType();
            DataTable _dt = new DataTable();
            Array arrValues = System.Enum.GetValues(_oEnmType);
            Int32 iVal = default(Int32);
            try
            {
                _dt.Columns.Add("nID", typeof(System.Int16));
                _dt.Columns.Add("sDescription", typeof(System.String));

                foreach (int ival_loopVariable in arrValues)
                {
                    iVal = ival_loopVariable;
                    string sVal = System.Enum.GetName(_oEnmType, iVal);

               
                    DataRow _dr = _dt.NewRow();
                    _dr["nID"] = iVal;
                    _dr["sDescription"] = sVal.Replace("_", " ");
                    _dt.Rows.Add(_dr);
                    sVal = null;
                }

                if (_bForComboBoxFilling)
                {
                    DataRow _drBlank = _dt.NewRow();
                    _drBlank["nID"] = 0;
                    _drBlank["sDescription"] = "";
                    _dt.Rows.InsertAt(_drBlank, 0);

                }
                _dt.AcceptChanges();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                _oEnmType = null;
                arrValues = null;
            }


            return _dt;
        }

        public static bool ValidateEmailAddress(string strEmailIdInput)
        {

            string strRegex = null;

            strRegex = "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(strEmailIdInput, strRegex) == false)
            {
                strRegex = null;
                return false;
            }
            else
            {
                strRegex = null;
                return true;
            }
        }
        public static bool ValidateURLAddress(string strUrlInput)
        {

            string strRegex = null;
            // strRegex = "^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))([-a-zA-Z0-9@:%_\+.~#?&//=])+.([-a-zA-Z0-9@:%_\+.~#?&//=])+$"
            strRegex = "^((((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[a-zA-Z0-9_\\-]+(?:\\.[a-zA-Z0-9_\\-]+)*\\.[a-zA-Z]{2,4}(?:\\/[a-zA-Z0-9_]+)*(?:\\/[a-zA-Z0-9_]+\\.[a-zA-Z]{2,4}(?:\\?[a-zA-Z0-9_]+\\=[a-zA-Z0-9_]+)?)?(?:\\&[a-zA-Z0-9_]+\\=[a-zA-Z0-9_]+)*)$";
            if (Regex.IsMatch(strUrlInput, strRegex) == false)
            {
                strRegex = null;
                return false;
            }
            else
            {
                strRegex = null;
                return true;
            }
        }
        /// <summary>
        /// To Get the Value of the Setting from The Database
        /// </summary>
        /// <param name="SettingName">Name of Setting</param>
        /// <returns>Datatable containing Value(s) of Respective Setting if Exits else set to 'null'</returns>
        /// 
        public string GetInstallationType(Int64 _UserID, Int64 _clinicID)
        {
            object oValueEMR = new object();
            object oValuePM = new object();
            string sReturn = string.Empty;
            string sValue = string.Empty;
            ArrayList alinstallationTypes = new ArrayList();
            try
            {
                GetSetting("EMRInstalled", _UserID, _clinicID, out oValueEMR);
                GetSetting("PMInstalled", _UserID, _clinicID, out oValuePM);

                if (oValueEMR != null && Convert.ToString(oValueEMR) != "")
                {
                    if (Convert.ToString(oValueEMR).ToLower().Trim() == "True".ToLower() || Convert.ToString(oValueEMR).ToLower().Trim() == "False".ToLower())
                    {
                        if (Convert.ToBoolean(oValueEMR)) { alinstallationTypes.Add("gloEMR"); }
                    }
                    else if (Convert.ToString(oValueEMR).Trim() == "1" || Convert.ToString(oValueEMR).Trim() == "0")
                    {
                        sValue = (Convert.ToString(oValueEMR).Trim() == "1" ? "TRUE" : "FALSE");
                        if (Convert.ToBoolean(sValue)) { alinstallationTypes.Add("gloEMR"); }
                        sValue = string.Empty;
                    }
                }

                if (oValuePM != null && Convert.ToString(oValuePM) != "")
                {
                    if (Convert.ToString(oValuePM).ToLower().Trim() == "True".ToLower() || Convert.ToString(oValuePM).ToLower().Trim() == "False".ToLower())
                    {
                        if (Convert.ToBoolean(oValuePM)) { alinstallationTypes.Add("gloPM"); }
                    }
                    else if (Convert.ToString(oValuePM).Trim() == "1" || Convert.ToString(oValuePM).Trim() == "0")
                    {
                        sValue = (Convert.ToString(oValuePM).Trim() == "1" ? "TRUE" : "FALSE");
                        if (Convert.ToBoolean(sValue)) { alinstallationTypes.Add("gloPM"); }
                        sValue = string.Empty;
                    }
                }

                if (alinstallationTypes.Count == 2)
                {
                    sReturn = "Both";
                }
                else if (alinstallationTypes.Count == 1)
                {
                    sReturn = Convert.ToString(alinstallationTypes[0]);
                }
                else if (alinstallationTypes.Count <= 0)
                {
                    sReturn = "None";
                }

            }
            catch (Exception ex)
            {
                sReturn = string.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oValueEMR = null;
                oValuePM = null;
                alinstallationTypes = null;
            }

            return sReturn;
        }

        public DataTable GetSetting(string SettingName)
        {
            DataTable Value=null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                //Value = new DataTable();
                oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue, ISNULL(nUserID,0) AS nUserID FROM Settings WITH (NOLOCK) WHERE sSettingsName = '" + SettingName + "'  AND nClinicID = " + _ClinicID + "", out Value);

                return Value;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }

            return Value;
        }

        public DataTable getIDQualifiersAssociation(Boolean bSkipSystemRecords, Boolean bskipProvider)
        {
            DataTable dtIDQualifiers=null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDBParameters.Add("@nClinincID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
            oDBParameters.Add("@bSkipSystemRecords", bSkipSystemRecords, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
            oDBParameters.Add("@bskipProviderCompany", bskipProvider, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
            try
            {
                oDB.Connect(false);
                //dtIDQualifiers = new DataTable();
                oDB.Retrive("BL_SELECT_IDQUALFIER_ASSOCIATION", oDBParameters, out dtIDQualifiers);
                //if (bSkipSystemRecords)
                //{
                //    oDB.Retrive("SELECT ISNULL(nQualifierID,0) AS nQualifierID, ISNULL(sCode,'')  AS sCode,ISNULL(sAdditionalDescription,'')  AS sAdditionalDescription, '' as value FROM BL_IDQualifier_Association WHERE isnull(bIsSystem,0) <> 1   AND nClinicID = " + _ClinicID + " order by sAdditionalDescription", out dtIDQualifiers);
                //}
                //else
                //{
                //    oDB.Retrive("SELECT ISNULL(nQualifierID,0) AS nQualifierID, ISNULL(sCode,'')  AS sCode,ISNULL(sAdditionalDescription,'')  AS sAdditionalDescription, '' as value FROM BL_IDQualifier_Association WHERE  nClinicID = " + _ClinicID + " order by sAdditionalDescription", out dtIDQualifiers);
                //}

                DataRow dr = dtIDQualifiers.NewRow();
                dr["nQualifierID"] = 0;
                dr["sCode"] = "";
                dr["sAdditionalDescription"] = "";
                dr["value"] = "";
                dtIDQualifiers.Rows.InsertAt(dr, 0);
                dtIDQualifiers.AcceptChanges();

                return dtIDQualifiers;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                dtIDQualifiers = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                dtIDQualifiers = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }

            return dtIDQualifiers;
        }

        public DataTable getSources(bool _bisAddOtherProviderCompany)
        {
            DataTable dtInsuranceType = null;
            try
            {
                //dtInsuranceType.Columns.Add("nID", typeof(System.Int16));
                //dtInsuranceType.Columns.Add("sDescription", typeof(System.String));

                //DataRow dr = dtInsuranceType.NewRow();
                //dr["nID"] = 0;
                //dr["sDescription"] = "";
                //dtInsuranceType.Rows.Add(dr);

                //dr = dtInsuranceType.NewRow();
                //dr["nID"] = 1;
                //dr["sDescription"] = "Billing Provider";
                //dtInsuranceType.Rows.Add(dr);

                //dr = dtInsuranceType.NewRow();
                //dr["nID"] = 2;
                //dr["sDescription"] = "Billing Provider Company";
                //dtInsuranceType.Rows.Add(dr);

                //dr = dtInsuranceType.NewRow();
                //dr["nID"] = 3;
                //dr["sDescription"] = "Claim Facility";
                //dtInsuranceType.Rows.Add(dr);

                //dr = dtInsuranceType.NewRow();
                //dr["nID"] = 4;
                //dr["sDescription"] = "Clinic";
                //dtInsuranceType.Rows.Add(dr);

                //AlternateIDSource AlterEnum;
                //ArrayList _arrList = new ArrayList();
                //_arrList.Add(AlterEnum);

                dtInsuranceType = GetEnumItems(AlternateIDSource.Billing_Provider, true);
                dtInsuranceType.Columns.Add("nSortSources", typeof(int));

                int nIndex = 1;
                foreach (DataRow dr in dtInsuranceType.Rows)
                {
                    if (dr["sDescription"].ToString() == "Billing Provider Company")
                    {
                        dr["nSortSources"] = 5;
                    }
                    else
                    {
                        dr["nSortSources"] = nIndex;
                        nIndex++;
                    }

                }
                if (_bisAddOtherProviderCompany)
                {
                    Object _oProviderCount = null;
                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    oSettings.GetSetting("NoOfProviderCompany", out _oProviderCount);
                    oSettings.Dispose();
                    oSettings = null;
                    int nDefaultSourcesCount = dtInsuranceType.Rows.Count - 1;
                    Int16 nProviderCount = 1;
                    if (_oProviderCount != null)
                    {
                        nProviderCount = Convert.ToInt16(_oProviderCount);
                    }
                    _oProviderCount = null;

                    if (nProviderCount > 1)
                    {
                        dtInsuranceType.DefaultView.Sort = "nSortSources";
                        dtInsuranceType = dtInsuranceType.DefaultView.ToTable();

                        for (int Count = 2; Count <= nProviderCount; Count++)
                        {
                            DataRow _drBlank = dtInsuranceType.NewRow();
                            _drBlank["nID"] = Count + nDefaultSourcesCount;
                            _drBlank["sDescription"] = "Billing Provider Company-" + Count;
                            _drBlank["nSortSources"] = Count + nDefaultSourcesCount;

                            dtInsuranceType.Rows.InsertAt(_drBlank, dtInsuranceType.Rows.Count);
                            dtInsuranceType.AcceptChanges();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return dtInsuranceType;
        }

        public void SaveBillingQualifierIDSettings(string sSettingName, Int64 nQualifierID, string sValue, bool bIsOtherID, string sOtherID, bool bIsSwapIDs, AlternateIDSettingLevel enmLevel, Int64 nContactID, Int64 nProviderID, Int64 nClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Int64 nOtherId = 0;

            try
            {
                if (sOtherID.Trim() != "")
                { nOtherId = Convert.ToInt64(sOtherID.Trim()); }

                oDB.Connect(false);

                oDBParameters.Add("@nID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sSettingName", sSettingName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sValue", sValue, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIsOtherID", bIsOtherID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@nOtherID", nOtherId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@bIsSwapIDs", bIsSwapIDs, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@nLevel", (int)enmLevel, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", nContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nProviderID", nProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", nClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nQualifierID", nQualifierID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sOtherIDDesc", "", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDB.Execute("BL_INUP_AlternateID_Settings", oDBParameters);

                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }

        }


        public void SaveBillingQualifierIDSettings(string sSettingName, Int64 nQualifierID, string sValue, bool bIsOtherID, string sOtherID, bool bIsSwapIDs, AlternateIDSettingLevel enmLevel, Int64 nContactID, Int64 nProviderID, Int64 nClinicID, String OtherIdDesc)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Int64 nOtherId = 0;

            try
            {
                if (sOtherID.Trim() != "")
                { nOtherId = Convert.ToInt64(sOtherID.Trim()); }

                oDB.Connect(false);

                oDBParameters.Add("@nID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sSettingName", sSettingName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sValue", sValue, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIsOtherID", bIsOtherID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@nOtherID", nOtherId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@bIsSwapIDs", bIsSwapIDs, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@nLevel", (int)enmLevel, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", nContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nProviderID", nProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", nClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nQualifierID", nQualifierID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sOtherIDDesc", OtherIdDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDB.Execute("BL_INUP_AlternateID_Settings", oDBParameters);

                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }

        }

        public DataTable getIDQualifiers(int mode, Int64 ID, Boolean bSkipSystemRecords)
        {
            DataTable dtIDQualifiers=null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                //dtIDQualifiers = new DataTable();

                oDBParameters.Add("@Mode", mode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nID", ID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oDBParameters.Add("@bSkipSystemRecords", bSkipSystemRecords, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);


                oDB.Retrive("BL_SELECT_IDQUALFIFIERWITHVALUE", oDBParameters, out dtIDQualifiers);

                return dtIDQualifiers;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                dtIDQualifiers = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                dtIDQualifiers = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }

            return dtIDQualifiers;
        }

        public DataSet GetEMRClientSettings( Int64 UserID, Int64 ModuleID)
        {
            gloDatabaseLayer.DBLayer odb = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters odbParas = new gloDatabaseLayer.DBParameters();
            DataSet dsData = new DataSet();

            try
            {
                odb.Connect(false);

                odbParas.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.Decimal);
                odbParas.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.Decimal);
                odbParas.Add("@ModuleId", ModuleID, ParameterDirection.Input, SqlDbType.Decimal);

                odb.Retrive("gsp_GetAllClientSettings", odbParas, out dsData);
               
            }
                
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (odbParas != null)
                {
                    odbParas.Dispose();
                    odbParas = null;
                }
                if (odb != null)
                {
                    odb.Disconnect();
                    odb.Dispose();
                    odb = null;
                }
            }

            return dsData;
        }

        public void ScanClientInterfaceForPM(string strProductName, string strMachineName)
        {

            gloDatabaseLayer.DBLayer odb = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters odbParas = new gloDatabaseLayer.DBParameters();
            DataSet dsData = new DataSet();
            try
            {
                odb.Connect(false);

                odbParas.Add("@sMachineName", strMachineName, ParameterDirection.Input, SqlDbType.NVarChar);
                odbParas.Add("@sProductName", strProductName, ParameterDirection.Input, SqlDbType.NVarChar);

                odb.Retrive("gsp_ViewClientInterface", odbParas, out dsData);
                int nCount = 0;
                for (nCount = 0; nCount <= dsData.Tables[0].Rows.Count - 1; nCount++)
                {
                    appSettings["SendPatientDetails"] = Convert.ToInt16(dsData.Tables[0].Rows[nCount]["bHl7_SendPatientDetails"]).ToString();
                    appSettings["SendAppointmentDetails"] = Convert.ToInt16(dsData.Tables[0].Rows[nCount]["bHL7_SendAppointmentDetails"]).ToString();
                    appSettings["HL7SENDOUTBOUNDGLOPM"] = Convert.ToInt16(dsData.Tables[0].Rows[nCount]["HL7SENDOUTBOUNDGLOPM"]).ToString();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (odbParas != null)
                {
                    odbParas.Dispose();
                    odbParas = null;
                }
                if (odb != null)
                {
                    odb.Disconnect();
                    odb.Dispose();
                    odb = null;
                }
            }
        }


        //Added For Product Version.
        public DataTable GetSetting(string SettingName, int ClinicID)
        {
            DataTable dtSettings=null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                //dtSettings = new DataTable();

                if (oDB.CheckConnection() == true)
                {
                    oDB.Connect(false);
                    oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" + SettingName + "'", out dtSettings);
                    oDB.Disconnect();
                }
                return dtSettings;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                dtSettings = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                dtSettings = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDBParameters.Dispose();
                oDB.Dispose();
            }

            return dtSettings;
        }

        /// <summary>
        /// To Get the Value of the Setting from The Database
        /// </summary>
        /// <param name="SettingName"> Name of Setting</param>
        /// <param name="Value"> Gets the Value of Respective Setting if Exits else set to 'null'</param>
        public void GetSetting(string SettingName, out object Value)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
   //         gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) = '" + SettingName.Trim().ToUpper() + "' AND nClinicID = " + _ClinicID + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();
                //oDBParameters.Dispose();
                //oDBParameters = null;

                oDB.Dispose();
                oDB = null;
            }
        }

        /// <summary>
        /// To Get the Value of the Clinic_MST from The Database
        /// </summary>
        /// <param name="SettingName"> Name of Setting</param>
        /// <param name="Value"> Gets the Value of Respective Setting if Exits else set to 'null'</param>
        public void GetClinicMasterSetting(string SettingName, out object Value)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            //         gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                Value = oDB.ExecuteScalar_Query("SELECT TOP 1 ISNULL(" + SettingName + ",'') as Resultvalue from Clinic_MST");
                //Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) = '" + SettingName.Trim().ToUpper() + "' AND nClinicID = " + _ClinicID + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oDB.Disconnect();
                //oDBParameters.Dispose();
                //oDBParameters = null;

                oDB.Dispose();
                oDB = null;
            }
        }

        public DataTable GetSSRSReportSettings()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("select sSettingsName, sSettingsValue from settings where sSettingsName IN ('ReportProtocol', 'ReportServer', 'ReportFolder', 'ReportVirtualDirectory')", out dt);
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                return dt;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                dt.Dispose();
                dt = null;
            }
        }



        public DataTable GetClinicTime()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("select sSettingsValue as StartTime, (select top 1 sSettingsValue from settings where upper(sSettingsName) = 'CLINICENDTIME') as endtime from settings where upper(sSettingsName) = 'CLINICSTARTTIME' AND nClinicID = " + _ClinicID, out dt);
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                return dt;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                dt.Dispose();
                dt = null;
            }
        }


        public void GetSetting(string SettingName, Int64 UserID, Int64 ClinicID, out object Value)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE sSettingsName = '" + SettingName + "' AND nUserID = " + UserID + " AND nClinicID = " + ClinicID + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool IsSecurityUser(Int64 UserID, Int64 ClinicID)
        {
            bool Value = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                Value = (bool)oDB.ExecuteScalar_Query("SELECT ISNULL(bIsSecurityUser,'False') AS bIsSecurityUser FROM User_MST WITH (NOLOCK) WHERE  nUserID = " + UserID + " AND nClinicID = " + ClinicID + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = false;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

                oDB.Disconnect();
                oDB.Dispose();
            }
            return Value;
        }

        public bool IsAdminUser(Int64 UserID, Int64 ClinicID)
        {
            bool Value = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                Value = (bool)oDB.ExecuteScalar_Query("SELECT ISNULL(nAdministrator,'False') AS nAdministrator FROM User_MST WITH (NOLOCK) WHERE  nUserID = " + UserID + " AND nClinicID = " + ClinicID + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = false;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

                oDB.Disconnect();
                oDB.Dispose();
            }
            return Value;
        }

        public void SaveGridColumnWidth(C1.Win.C1FlexGrid.C1FlexGrid oControlGrid, ModuleOfGridColumn ModuleName, Int64 UserID)
        {
            string _SettingValue = "";
            string _SettingName = "";
            try
            {
                _SettingName = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Name.ToString().ToUpper();
                for (int i = 0; i <= oControlGrid.Cols.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        _SettingValue = oControlGrid.Cols[i].Width.ToString();
                    }
                    else
                    {
                        _SettingValue = _SettingValue + "," + oControlGrid.Cols[i].Width.ToString();
                    }
                }
                //DeleteSetting(_SettingName);
                bool _result = AddSetting(_SettingName, _SettingValue, _ClinicID, UserID, SettingFlag.User);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                _SettingValue = null;
                _SettingName = null;
            }
        }

        public bool LoadGridColumnWidth(C1.Win.C1FlexGrid.C1FlexGrid oControlGrid, ModuleOfGridColumn ModuleName, Int64 UserID)
        {
            bool _result = false;

            string _SettingValue = "";
            object _SettingValueObj = null;
            string[] _SettingValueList = null;
            try
            {

                string _SettingName = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Name.ToString().ToUpper();
                _SettingValue = "";
                GetSetting(_SettingName, UserID, _ClinicID, out _SettingValueObj);

                if (_SettingValueObj != null && _SettingValueObj.ToString().Trim() != "")
                {
                    _SettingValue = _SettingValueObj.ToString();
                }
                if (_SettingValue.Trim().Length > 0)
                {
                    _SettingValueList = _SettingValue.Split(',');
                }

                if (_SettingValueList != null)
                {
                    if (_SettingValueList.Length > 0)
                    {
                        for (int i = 0; i <= _SettingValueList.Length - 1; i++)
                        {
                            int _ColWidth = 0;
                            _ColWidth = Convert.ToInt32(_SettingValueList[i].ToString());
                            oControlGrid.Cols[i].Width = _ColWidth;
                        }
                        _result = true;
                    }
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                _SettingValue = null;
                _SettingValueObj = null;
                _SettingValueList = null;
            }
            return _result;
        }

        public void SaveGridColumnDisplayIndex(DataGridView oControlGrid, ModuleOfGridColumn ModuleName, Int64 UserID)
        {
            string _SettingValue = null;
            string _SettingName = null;
            string[] _Values = null;

            try
            {
                _SettingValue = "";
                _SettingName = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Tag.ToString().ToUpper() + "_DISPLAYINDEX";

                _Values = new string[oControlGrid.Columns.Count];


                for (int i = 0; i <= oControlGrid.Columns.Count - 1; i++)
                {
                    _Values[oControlGrid.Columns[i].DisplayIndex] = i.ToString();

                    //if (i == 0)
                    //{
                    //    _SettingValue = oControlGrid.Columns[i].DisplayIndex.ToString();  
                    //}
                    //else
                    //{
                    //    _SettingValue = _SettingValue + "," + oControlGrid.Columns[i].DisplayIndex.ToString();
                    //}
                }

                for (int i = 0; i < _Values.Length; i++)
                {
                    if (i == 0)
                    {
                        _SettingValue = _Values[i];
                    }
                    else
                    {
                        _SettingValue = _SettingValue + "," + _Values[i];
                    }
                }

                //DeleteSetting(_SettingName);
                bool _result = AddSetting(_SettingName, _SettingValue, _ClinicID, UserID, SettingFlag.User);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                _SettingValue = null;
                _SettingName = null;
                 _Values = null;
            }
        }

        public bool LoadGridColumnDisplayIndex(DataGridView oControlGrid, ModuleOfGridColumn ModuleName, Int64 UserID)
        {
            bool _result = false;

            string _SettingValue = "";
            object _SettingValueObj = null;
            string[] _SettingValueList = null;
            string _SettingName = null;

            try
            {

                _SettingName = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Tag.ToString().ToUpper() + "_DISPLAYINDEX";
                _SettingValue = "";
                GetSetting(_SettingName, UserID, _ClinicID, out _SettingValueObj);

                if (_SettingValueObj != null && _SettingValueObj.ToString().Trim() != "")
                {
                    _SettingValue = _SettingValueObj.ToString();
                }
                if (_SettingValue.Trim().Length > 0)
                {
                    _SettingValueList = _SettingValue.Split(',');
                }

                if (_SettingValueList != null)
                {
                    if (_SettingValueList.Length > 0)
                    {
                        _result = true;
                        for (int i = 0; i <= _SettingValueList.Length - 1; i++)
                        {
                            int _ColIndex = 0;
                            _ColIndex = Convert.ToInt32(_SettingValueList[i].ToString());
                            if (_ColIndex < oControlGrid.ColumnCount)
                            {
                                oControlGrid.Columns[_ColIndex].DisplayIndex = i;
                            }
                        }
                    }
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                _SettingValue = null;
                _SettingValueObj = null;
                _SettingValueList = null;
                _SettingName = null;
            }
            return _result;
        }

        public void SaveGridColumnWidth(DataGridView oControlGrid, ModuleOfGridColumn ModuleName, Int64 UserID)
        {
            string _SettingValue = null ;
            string _SettingName = null;
            try
            {
                _SettingValue = "";
                _SettingName = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Tag.ToString().ToUpper() + "_WIDTH";
                for (int i = 0; i <= oControlGrid.Columns.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        _SettingValue = oControlGrid.Columns[i].Width.ToString();
                    }
                    else
                    {
                        _SettingValue = _SettingValue + "," + oControlGrid.Columns[i].Width.ToString();
                    }
                }
                //DeleteSetting(_SettingName);
                bool _result = AddSetting(_SettingName, _SettingValue, _ClinicID, UserID, SettingFlag.User);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                _SettingValue = null;
                _SettingName = null;
            }
        }


        public bool AddExpandedClaimSettings(Int64 nSettingsID, Int64 nCompanyID, Int64 nContactID, Int16 nSettingsLevel, Int16 nSettingType, Int32 nServiceLines, Int32 nDiagnosis, Int64 ClinicID, Int64 UserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@nSettingsID", nSettingsID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nCompanyID", nCompanyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", nContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nSettingLevel", nSettingsLevel, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nSettingType", nSettingType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nServiceLines", nServiceLines, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nDiagnosis", nDiagnosis, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                // oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

                oDB.Execute("BL_INUP_ExpandedClaimSettings", oDBParameters);


                return true;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                MessageBox.Show(DBErr.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
        }



        public bool LoadGridColumnWidth(DataGridView oControlGrid, ModuleOfGridColumn ModuleName, Int64 UserID)
        {
            bool _result = false;

            string _SettingValue = "";
            object _SettingValueObj = null;
            string[] _SettingValueList = null;
            try
            {

                oControlGrid.AutoSize = false;
                oControlGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                string _SettingName = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Tag.ToString().ToUpper() + "_WIDTH";
                _SettingValue = "";
                GetSetting(_SettingName, UserID, _ClinicID, out _SettingValueObj);

                if (_SettingValueObj != null && _SettingValueObj.ToString().Trim() != "")
                {
                    _SettingValue = _SettingValueObj.ToString();
                }
                if (_SettingValue.Trim().Length > 0)
                {
                    _SettingValueList = _SettingValue.Split(',');
                }

                if (_SettingValueList != null)
                {
                    if (_SettingValueList.Length > 0)
                    {
                        _result = true;
                        for (int i = 0; i <= _SettingValueList.Length - 1; i++)
                        {
                            int _ColWidth = 0;
                            _ColWidth = Convert.ToInt32(_SettingValueList[i].ToString());
                            oControlGrid.Columns[i].Width = _ColWidth;
                        }
                    }
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                _SettingValue = null;
                _SettingValueObj = null;
                _SettingValueList = null;
            }
            return _result;
        }

        public bool IsExpndedClaimSettings()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            Boolean _Result = false;
            //  DataTable dt;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "select isnull(sSettingsvalue,'false') from settings  WITH (NOLOCK) where ssettingsname = 'AllowExpandedElectronicClaims'";
                Object bSettings = oDB.ExecuteScalar_Query(SqlQuery);
                if (Convert.ToBoolean(bSettings) == true)
                {
                    _Result = true;
                }
                bSettings = null;
                SqlQuery = null;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _Result;
        }


        public int getANSIVersion(Int64 ContactID, string type, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            int nANSIVersion = 0;
            try
            {
                oDB.Connect(false);
                string _strSql = "";
                _strSql = "SELECT dbo.GET_ANSI_VERSION (" + ContactID + ",'" + type + "'," + ClinicID + ")";

                //If nANSIVersion is 0 then setting not present,if 1 then ANSI4010 and if 2 then ANSI5010
                nANSIVersion = Convert.ToInt16(oDB.ExecuteScalar_Query(_strSql));
                _strSql = null;
                return nANSIVersion;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.Message);
                return 0;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }


        }




        public bool GetSettingUserSpecific(string sSettingName, Int64 userID, long clinicID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            bool Value = false;

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@SettingName", sSettingName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 200);
                oDBParameters.Add("@UserID", userID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ClinicID", clinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                if (_messageBoxCaption.ToLower() == "gloEmr".ToLower())
                {
                    oDBParameters.Add("@ApplicationType", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                else
                {
                    oDBParameters.Add("@ApplicationType", 1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }

                Value = Convert.ToBoolean(oDB.ExecuteScalar("CheckUserSpecificSettings", oDBParameters));
                return Value;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = false;
                DBErr.ERROR_Log(DBErr.Message);
                return Value;
            }
            catch (Exception ex)
            {
                Value = false;
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return Value;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;
            }

        }

        public bool GetSettingUserSpecificRight(string sRightName, Int64 userID, long clinicID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            bool Value = false;

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@sRightName", sRightName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 200);
                oDBParameters.Add("@UserID", userID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ClinicID", clinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                if (_messageBoxCaption.ToLower() == "gloEmr".ToLower())
                {
                    oDBParameters.Add("@ApplicationType", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                else
                {
                    oDBParameters.Add("@ApplicationType", 1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }

                Value = Convert.ToBoolean(oDB.ExecuteScalar("CheckUserSpecificRights", oDBParameters));
                return Value;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = false;
                DBErr.ERROR_Log(DBErr.Message);
                return Value;
            }
            catch (Exception ex)
            {
                Value = false;
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return Value;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;
            }

        }


        //gloPM6010
        //public enum ExpandedClaimSettings
        //{
        //    None = 0,
        //    Clinic = 10,
        //    InsuranceCompany = 20,
        //    InsurancePlan = 30
        //}

    }

    public static class ExtendedDateTime
    {
        public static bool IsValidDate(this DateTime _dt)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(_dt.ToString("MM/dd/yyyy"), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException e)
            {
                Success = false; // If this line is reached, an exception was thrown
                e.ToString();
                e = null;
            }
            return Success;
        }
    }

    public static class FolderSettings
    {
        

        public static string TempFolderName
        {
            get
            {
                if (gloRegistrySetting.IsServerOS == true)
                {
                    gloAuditTrail.MachineDetails.MachineInfo RemoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails();
                    return "Temp_" + RemoteMachine.MachineName;
                }
                else
                {
                    return "Temp";
                }
            }
        }
        public static string LocalApplicationData
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return path;
            }
        }

        /// <summary>
        /// This will return the path of a Temp File i.e. User specific folder.
        /// Wind 7 : C:\Users\[UserName]\AppData\Local\[ProductName]\Temp
        /// Wind XP : C:\Documents and Settings\[UserName]\ApplicationData\[ProductName]\Temp
        /// </summary>
        public static string AppTempFolderPath
        {
            get
            {
                string productName = System.Windows.Forms.Application.ProductName;
                string path = LocalApplicationData + "\\" + productName + "\\" + TempFolderName + "\\";

                if (!System.IO.Directory.Exists(path))
                { System.IO.Directory.CreateDirectory(path); }

                return path;
            }
        }


        /// <summary>
        /// This will return the path of a Temp File i.e. User specific folder.
        /// Wind 7 : C:\Users\[UserName]\AppData\Local\[ProductName]\Temp
        /// Wind XP : C:\Documents and Settings\[UserName]\ApplicationData\[ProductName]\Temp
        /// </summary>
        public static string AppFolderPath
        {
            get
            {
                string productName = System.Windows.Forms.Application.ProductName;
                string path = LocalApplicationData + "\\" + productName + "\\";

                if (!System.IO.Directory.Exists(path))
                { System.IO.Directory.CreateDirectory(path); }

                return path;
            }
        }
        public static string AppPrintSpoolerFolderPath
        {
            get
            {
                string productName = System.Windows.Forms.Application.ProductName;
                string path = LocalApplicationData + "\\" +  "gloPrintSpooler"+ "\\";

                if (!System.IO.Directory.Exists(path))
                { System.IO.Directory.CreateDirectory(path); }

                return path;
            }
        }

    }


   //Added class for getting EMR admin Settings
   public class gloEMRAdminSettings : IDisposable
   {
       public static Boolean _globlnEnableMultipleRaceFeatures;
       public static Boolean globlnEnableMultipleRaceFeatures
       {
           get { return _globlnEnableMultipleRaceFeatures; }
           set { _globlnEnableMultipleRaceFeatures = value; }
       }
       public static Boolean _globlnEnableCypressTesting;
       public static Boolean globlnEnableCypressTesting
       {
           get { return _globlnEnableCypressTesting; }
           set { _globlnEnableCypressTesting = value; }
       }
       private static bool _IsXDMMessageEnabled = false;

       public static bool XDmMessageEnabled
       {
           get { return _IsXDMMessageEnabled; }
           set
           { _IsXDMMessageEnabled = value; }
       }

        #region "IDisposable Support"

        // IDisposableprotected 
        public Boolean disposedValue;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).  
                }

            } this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    //Added class for getting EMR Settings
    public class gloEMRSettings : IDisposable
    {
        public static Boolean _gblnResetSearchTextBox;
        public static Boolean gblnResetSearchTextBox
        {
            get { return _gblnResetSearchTextBox; }
            set { _gblnResetSearchTextBox = value; }
        }

        #region "IDisposable Support"

        // IDisposableprotected 
        public Boolean disposedValue;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).  
                }

            } this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public static class EnumExtensions
    {

        public static string GetDescription(this Enum value)
        {

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;

        }
    }

    public static class HL7Settings
    {
        public static String gloHL7ServerName { get; set; }
        public static String gloHL7DatabaseName { get; set; }
        public static String gloHL7UserID { get; set; }
        public static String gloHL7Password { get; set; }
        public static Boolean IsSQLAuthenticationOnForGloHL7 { get; set; }

        public static string GetHL7ConnectionString()
        {
            String _gloHL7ConnectionString = string.Empty;

            try
            {
                if (IsSQLAuthenticationOnForGloHL7)
                {
                    if (!string.IsNullOrEmpty(gloHL7ServerName) && !string.IsNullOrEmpty(gloHL7DatabaseName) && !string.IsNullOrEmpty(gloHL7UserID) && !string.IsNullOrEmpty(gloHL7Password))
                    { _gloHL7ConnectionString = "SERVER=" + gloHL7ServerName + ";DATABASE=" + gloHL7DatabaseName + ";User ID=" + gloHL7UserID + ";Password=" + gloHL7Password + ""; }
                
                }
                else
                {
                    if (!string.IsNullOrEmpty(gloHL7ServerName) && !string.IsNullOrEmpty(gloHL7DatabaseName))
                   { _gloHL7ConnectionString = "SERVER=" + gloHL7ServerName + ";DATABASE=" + gloHL7DatabaseName + ";Integrated Security=SSPI"; }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

            return _gloHL7ConnectionString;
        }

        public static Int32 GetEmrDBConnectionIdInHL7DB()
        {
            Int32 _returnDbID = 0;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            
            gloDatabaseLayer.DBLayer oDBLayer = null;            
            string _sSQLStmt = string.Empty;
            
            string _strEmrDBname = string.Empty;
            string _strServerName = string.Empty;
            try
            {
                if (String.IsNullOrEmpty(GetHL7ConnectionString())) { return _returnDbID; }

                 _strEmrDBname = Convert.ToString(appSettings["DatabaseName"]);
                 if (String.IsNullOrEmpty(_strEmrDBname) || _strEmrDBname.Length<=0) { return _returnDbID; }

                 _strServerName = Convert.ToString(appSettings["SQLServerName"]);
                 if (String.IsNullOrEmpty(_strServerName) || _strEmrDBname.Length<=0) { return _returnDbID; }

                if (appSettings != null)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(appSettings["SQLServerName"])) || string.IsNullOrEmpty(Convert.ToString(appSettings["DatabaseName"])))
                    { return _returnDbID; }
                }

                oDBLayer = new gloDatabaseLayer.DBLayer(GetHL7ConnectionString());
                oDBLayer.Connect(false);

                _sSQLStmt = "SELECT ISNULL(nDBConnectionId, 0) FROM DBSettings WHERE ( sServerName = '" + _strServerName.Replace("'","''") + "' ) AND ( sDatabaseName = '" + _strEmrDBname.Replace("'", "''") + "' ) AND ( sServiceName = 'HL7' )";
                _returnDbID = Convert.ToInt32(oDBLayer.ExecuteScalar_Query(_sSQLStmt));

                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                appSettings = null;

                if (oDBLayer != null)
                { oDBLayer.Dispose(); oDBLayer = null; }
                _strEmrDBname = string.Empty;
                _strServerName = string.Empty;
                _sSQLStmt = string.Empty;
            }

            return _returnDbID;
        }

    }
}
