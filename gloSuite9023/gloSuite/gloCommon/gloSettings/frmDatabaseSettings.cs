using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;
using gloSecurity;

namespace gloSettings
{

    internal partial class frmDatabaseSettings : Form
    {
        private string _PMSsqlservername = "";
        private string _PMSdatabasename = "";
        private bool _PMSwindowsauthentication = false;
        private string _PMSSQLusername = "";
        private string _PMSSQLpassword = "";

        private string _RPTsqlservername = "";
        private string _RPTdatabasename = "";
        private bool _RPTwindowsauthentication = false;
        private string _RPTSQLusername = "";
        private string _RPTSQLpassword = "";
        private Boolean _blnShowReportDesigner = false;

        private const string _encryptionKey = "12345678";

        //SQL Server Password Encyption key.
        private const string _SqlEncryptionKey = "20gloStreamInc08";
       
        //private bool _savesettings = false;

        //private string _MessageBoxCaption = "gloPM";
        private string _MessageBoxCaption = String.Empty;

        private string _databaseconnectionstring = "";
        private bool _ShowReportingServices = false;

        public frmDatabaseSettings()
        {
            InitializeComponent();
        }

        public frmDatabaseSettings(bool ShowReportingServices)
        {
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

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

            #endregion

            InitializeComponent();
            _ShowReportingServices = ShowReportingServices;
        }

        #region "Setting Property"
        public string PMSSQLServerName
        {
            get { return _PMSsqlservername; }
            set { _PMSsqlservername = value; }
        }

        public string PMSSQLDatabaseName
        {
            get { return _PMSdatabasename; }
            set { _PMSdatabasename = value; }
        }

        public bool PMSWindowsAuthentication
        {
            get { return _PMSwindowsauthentication ; }
            set { _PMSwindowsauthentication = value; }
        }

        public string PMSSQLusername
        {
            get { return _PMSSQLusername; }
            set { _PMSSQLusername = value; }
        }

        public string PMSSQLpassword
        {
            get { return _PMSSQLpassword ; }
            set { _PMSSQLpassword = value; }
        }

        //public bool SaveSettings
        //{
        //    get { return _savesettings; }
        //    set { _savesettings = value; }
        //}

        public string RPTSQLServerName
        {
            get { return _RPTsqlservername; }
            set { _RPTsqlservername = value; }
        }

        public string RPTSQLDatabaseName
        {
            get { return _RPTdatabasename; }
            set { _RPTdatabasename = value; }
        }

        public bool RPTWindowsAuthentication
        {
            get { return _RPTwindowsauthentication; }
            set { _RPTwindowsauthentication = value; }
        }

         public string RPTSQLusername
        {
            get { return _RPTSQLusername; }
            set { _RPTSQLusername = value; }
        }

        public string RPTSQLpassword
        {
            get { return _RPTSQLpassword; }
            set { _RPTSQLpassword = value; }
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

      
        #endregion

        private void frmDatabaseSettings_Load(object sender, EventArgs e)
        {
            try
            {
            txt_PMSDB_ServerName.Select();
            cmb_PMSDB_Authentication.Items.Clear();
            cmb_PMSDB_Authentication.Items.Add("Windows");
            cmb_PMSDB_Authentication.Items.Add("SQL");

            cmb_RPTDB_Authentication.Items.Clear();
            cmb_RPTDB_Authentication.Items.Add("SQL");

            txt_PMSDB_ServerName.Text = _PMSsqlservername;
            txt_PMSDB_DatabaseName.Text = _PMSdatabasename;
            if (_PMSwindowsauthentication  == true)
            {
                cmb_PMSDB_Authentication.SelectedIndex = 0;
            }
            else
            {
                cmb_PMSDB_Authentication.SelectedIndex = 1;
            }
            txt_PMSDB_UserName.Text = _PMSSQLusername;
            txt_PMSDB_Password.Text = _PMSSQLpassword ;

            txt_RPTDB_ServerName.Text = _PMSsqlservername ;
            txt_RPTDB_DataBaseName.Text = _PMSdatabasename;
            cmb_RPTDB_Authentication.SelectedIndex = 0;  
            txt_RPTDB_UserName.Text = _RPTSQLusername;
            txt_RPTDB_Password.Text = _RPTSQLpassword;


            if (_ShowReportingServices == false)
            {
                tb_Settings.TabPages.Remove(tbpg_ReportingDBSettings);
            }
        }
        catch (Exception ex)
        {
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);           
        }

            //_savesettings = false;
        }

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_PMSDB_Authentication.SelectedIndex == 0)
            {
                txt_PMSDB_UserName.Text = "";
                txt_PMSDB_Password.Text = "";
                txt_PMSDB_UserName.Enabled = false;
                txt_PMSDB_Password.Enabled = false;
            }
            else
            {
                txt_PMSDB_UserName.Enabled = true;
                txt_PMSDB_Password.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //_savesettings = false;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // Check Whether Connection is established
                if (txt_PMSDB_ServerName.Text == "")
                {
                    MessageBox.Show("Please enter SQL Server Name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txt_PMSDB_DatabaseName.Text == "")
                {
                    MessageBox.Show("Please enter Database Name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cmb_PMSDB_Authentication.SelectedIndex != 0)
                {
                    if (txt_PMSDB_UserName.Text == "")
                    {
                        MessageBox.Show("Please enter User Name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (ConnectionOpen() == false)
                {
                    MessageBox.Show("Connection can not established with given parameter, please verify it", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _PMSsqlservername = txt_PMSDB_ServerName.Text;
                _PMSdatabasename = txt_PMSDB_DatabaseName.Text;
                if (cmb_PMSDB_Authentication.SelectedIndex == 0 || cmb_PMSDB_Authentication.SelectedIndex == 2)
                {
                    _PMSwindowsauthentication = true;
                }
                else
                {
                    _PMSwindowsauthentication = false;
                }
                _PMSSQLusername = txt_PMSDB_UserName.Text;
                _PMSSQLpassword  = txt_PMSDB_Password.Text;


                
                //_savesettings = true;

                // Check for the SQL Authentication for Reporting Services
                //if (txtReportingSQLUserName.Text != "")
                //{

                bool blnShowReportDesigner = false;
                if (_ShowReportingServices == true)
                {
                    if (CheckSQLAuthenticationForgloReporting(out blnShowReportDesigner) == true)
                    {
                        txt_RPTDB_UserName.Focus();
                        return;

                    }
                }
                //}
                //

                _blnShowReportDesigner = blnShowReportDesigner;


                #region "Write database settings to registry"

                //try
                //{
                //    RegistryKey oRegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, System.Windows.Forms.SystemInformation.ComputerName);
                //    oRegistryKey = oRegistryKey.CreateSubKey("Software\\gloPM");
                //    if (oRegistryKey != null)
                //    {
                //        oRegistryKey.SetValue("SQLSERVER", _PMSsqlservername);
                //        oRegistryKey.SetValue("Database", _PMSdatabasename);
                //        oRegistryKey.SetValue("ISWINAUTHENTICATION", Convert.ToString(_PMSwindowsauthentication));
                //        oRegistryKey.SetValue("SQLUSERNAME", _PMSSQLusername);

                //        ClsEncryption oEncrypt = new ClsEncryption();
                //        string e_sPassword = oEncrypt.EncryptToBase64String(_PMSSQLpassword, _SqlEncryptionKey);

                //        oRegistryKey.SetValue("SQLPASSWORD", e_sPassword);
                //        oRegistryKey.Close();
                //    }
                //}


                try
                {
                    if (gloRegistrySetting.OpenRemoteBaseKey())
                    {
                        // if(  gloRegistrySetting.IsRegistryKeyExists(gloRegistrySetting.gstrSoftPM)==false)
                        if (gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrSoftPM, ""))
                        {



                            gloRegistrySetting.SetRegistryValue("SQLSERVER", _PMSsqlservername);
                            gloRegistrySetting.SetRegistryValue("Database", _PMSdatabasename);
                            gloRegistrySetting.SetRegistryValue("ISWINAUTHENTICATION", Convert.ToString(_PMSwindowsauthentication));
                            gloRegistrySetting.SetRegistryValue("SQLUSERNAME", _PMSSQLusername);

                            ClsEncryption oEncrypt = new ClsEncryption();
                            string e_sPassword = oEncrypt.EncryptToBase64String(_PMSSQLpassword, _SqlEncryptionKey);

                            gloRegistrySetting.SetRegistryValue("SQLPASSWORD", e_sPassword);
                            gloRegistrySetting.CloseRegistryKey();
                        }

                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                }

                #endregion

                //// To Add The Exchange Server Domain & URL
                //DatabaseSetting.DataBaseSetting oSetting = new gloSettings.DatabaseSetting.DataBaseSetting(Application.StartupPath);
                //oSetting.DBConnectionString = _databaseconnectionstring;
                ////Exchange Domain
                //oSetting.SaveSetting("ExchangeDomain", txtExchangeDomain.Text.Trim(),_databaseconnectionstring );
                ////if (Result == false)
                ////    return Result;

                ////Exchange URL
                //oSetting.SaveSetting("ExchangeURL", txtExchangeURL.Text.Trim(),_databaseconnectionstring );
                ////if (Result == false)
                ////    return Result;
                ////
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool CheckSQLAuthenticationForgloReporting(out bool blnShowReportDesigner)
        {
            string conString = "";
            SqlConnection con = new SqlConnection();
            bool result = true ;
            blnShowReportDesigner = false;
            // Temporary Take the SQl ServerName & DB Name of PMSDB 
            conString = "Server=" + txt_PMSDB_ServerName.Text.Trim() + ";Database=" + txt_PMSDB_DatabaseName.Text.Trim() + ";Uid=" + txt_RPTDB_UserName.Text.Trim() + ";Pwd=" + txt_RPTDB_Password.Text.Trim() + ";";
            try
            {
                System.Data.SqlClient.SqlConnection _connection = new System.Data.SqlClient.SqlConnection();
                con.ConnectionString = conString;
                con.Open();
                con.Close();
                gloSettings.DatabaseSetting.DataBaseSetting oSetting = new gloSettings.DatabaseSetting.DataBaseSetting();
                gloSettings.GeneralSettings oGeneral = new gloSettings.GeneralSettings(conString);
                oSetting.ReportingSQLUserName = txt_RPTDB_UserName.Text.Trim();
                oSetting.ReportingSQLPassword = txt_RPTDB_Password.Text.Trim();
                //if (oSetting.SaveSettings(conString) == true)
                //{
                //    _blnShowReportDesigner = true;
                //}
                if (oGeneral.AddSetting("UserName", txt_RPTDB_UserName.Text.Trim(), 0, 0, SettingFlag.None) == false)
                {
                    result = false;
                }

                if (oGeneral.AddSetting("Password", txt_RPTDB_Password.Text.Trim(), 0, 0, SettingFlag.None) == false)
                {
                    result = false;
                }

                if (result == true)
                {
                    blnShowReportDesigner = true;
                }
                result = false;
            }
            catch (Exception) // ex)
            {
                if (MessageBox.Show("Connection can not established with given parameter for gloReporting.\n If the Settings are not done then you can not use Report Designer. Do you want to try again?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    // If User Don't wants to reenter the UserID & Password 
                    blnShowReportDesigner = false;
                    result = false;
                }
                else
                {
                    // If User wants to reenter the UserID & Password 
                    blnShowReportDesigner = false;
                    result = true;
                }

                //ex.ToString();
                //ex = null;
            }
            finally
            { conString = null; }
            return result;
        }

        private bool ConnectionOpen()
        {
            bool _result;
            string _connstring = "";
            try
            {
                if (cmb_PMSDB_Authentication.SelectedIndex == 1)//SQL authentication
                {
                    _connstring = "Server=" + txt_PMSDB_ServerName.Text + ";Database=" + txt_PMSDB_DatabaseName.Text + ";Uid=" + txt_PMSDB_UserName.Text + ";Pwd=" + txt_PMSDB_Password.Text + ";";
                }
                else//windows authentication
                {
                    _connstring = "Server=" + txt_PMSDB_ServerName.Text + ";Database=" + txt_PMSDB_DatabaseName.Text + ";Trusted_Connection=yes;";
                }

                System.Data.SqlClient.SqlConnection _connection = new System.Data.SqlClient.SqlConnection();
                _connection.ConnectionString = _connstring;

                _connection.Open();
                _connection.Close();
                _databaseconnectionstring = _connstring;
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

        private void btnOK_MouseHover(object sender, EventArgs e)
        {
            btnOK.BackgroundImage = global::gloSettings.Properties.Resources.ImgButtonHover;
            btnOK.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnOK_MouseLeave(object sender, EventArgs e)
        {
            btnOK.BackgroundImage = global::gloSettings.Properties.Resources.ImgButton;
            btnOK.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnCancel_MouseHover(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = global::gloSettings.Properties.Resources.ImgButtonHover;
            btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = global::gloSettings.Properties.Resources.ImgButton;
            btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnHelp_MouseHover(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = global::gloSettings.Properties.Resources.ImgButtonHover;
            btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnHelp_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = global::gloSettings.Properties.Resources.ImgButton;
            btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void chk_RPT_SameAsPMSDBSettings_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_RPT_SameAsPMSDBSettings.Checked == true)
                {
                    txt_RPTDB_ServerName.Text = txt_PMSDB_ServerName.Text.Trim();
                    txt_RPTDB_DataBaseName.Text = txt_PMSDB_DatabaseName.Text.Trim();
                    cmb_RPTDB_Authentication.SelectedIndex = 0;
                    txt_RPTDB_UserName.Text = txt_PMSDB_UserName.Text.Trim();
                    txt_RPTDB_Password.Text = txt_PMSDB_Password.Text.Trim();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


    }
}