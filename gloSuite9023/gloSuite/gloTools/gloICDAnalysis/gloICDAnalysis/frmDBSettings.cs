using System;
using System.Windows.Forms;
using gloICDAnalysis.ClassLib;


namespace gloICDAnalysis
{
    internal partial class frmDBSettings : Form
    {
        #region "Variables"
        
        //public string _PMSsqlservername = "";
        //public string _PMSdatabasename = "";
        //public bool _PMSwindowsauthentication = false;
        //public string _PMSSQLusername = "";
        //public string _PMSSQLpassword = "";

        //public string _EMRSsqlservername = "";
        //public string _EMRSdatabasename = "";
        //public bool _EMRSwindowsauthentication = false;
        //public string _EMRSSQLusername = "";
        //public string _EMRSSQLpassword = "";

        //private string _RPTsqlservername = "";
        //private string _RPTdatabasename = "";
        //private bool _RPTwindowsauthentication = false;
        //private string _RPTSQLusername = "";
        //private string _RPTSQLpassword = "";
        //private Boolean _blnShowReportDesigner = false;

        private string _MessageBoxCaption = "ICD Utility";
        //private string _databaseconnectionstring = "";
        //private bool _ShowReportingServices = false; 

        private const string _encryptionKey = "12345678";
        private const string _SqlEncryptionKey = "20gloStreamInc08";
        

        #endregion

        #region "Setting Property"

        //public string PMSSQLServerName
        //{
        //    get { return _PMSsqlservername; }
        //    set { _PMSsqlservername = value; }
        //}

        //public string PMSSQLDatabaseName
        //{
        //    get { return _PMSdatabasename; }
        //    set { _PMSdatabasename = value; }
        //}

        //public bool PMSWindowsAuthentication
        //{
        //    get { return _PMSwindowsauthentication; }
        //    set { _PMSwindowsauthentication = value; }
        //}

        //public string PMSSQLusername
        //{
        //    get { return _PMSSQLusername; }
        //    set { _PMSSQLusername = value; }
        //}

        //public string PMSSQLpassword
        //{
        //    get { return _PMSSQLpassword; }
        //    set { _PMSSQLpassword = value; }
        //}

        //public string RPTSQLServerName
        //{
        //    get { return _RPTsqlservername; }
        //    set { _RPTsqlservername = value; }
        //}

        //public string RPTSQLDatabaseName
        //{
        //    get { return _RPTdatabasename; }
        //    set { _RPTdatabasename = value; }
        //}

        //public bool RPTWindowsAuthentication
        //{
        //    get { return _RPTwindowsauthentication; }
        //    set { _RPTwindowsauthentication = value; }
        //}

        //public string RPTSQLusername
        //{
        //    get { return _RPTSQLusername; }
        //    set { _RPTSQLusername = value; }
        //}

        //public string RPTSQLpassword
        //{
        //    get { return _RPTSQLpassword; }
        //    set { _RPTSQLpassword = value; }
        //}

        //public Boolean ShowReportDesigner
        //{
        //    get { return _blnShowReportDesigner; }
        //    set { _blnShowReportDesigner = value; }
        //}

        //public string DBConnectionString
        //{
        //    get { return _databaseconnectionstring; }
        //    set { _databaseconnectionstring = value; }
        //}

        public DBSetting.ApplicationType ApplicationType { get; set; }

        #endregion


        //public DBSetting SelectedDB { get; set; }

        public frmDBSettings(DBSetting.ApplicationType type)
        {
            ApplicationType = type;

            InitializeComponent();
        }

        private void frmDatabaseSettings_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "Database Settings - " + ApplicationType.ToString();

                txt_PMSDB_ServerName.Select();
                cmb_PMSDB_Authentication.Items.Clear();
                cmb_PMSDB_Authentication.Items.Add("Windows");
                cmb_PMSDB_Authentication.Items.Add("SQL");

                if (ApplicationType == DBSetting.ApplicationType.gloEMR)
                {
                    txt_PMSDB_ServerName.Text = clsDBSettings.CurrentDBInfo.SqlServerName;
                    txt_PMSDB_DatabaseName.Text = clsDBSettings.CurrentDBInfo.DatabaseName;

                    if (clsDBSettings.CurrentDBInfo.IsWindowsAuthentication)
                    { cmb_PMSDB_Authentication.SelectedIndex = 0; }
                    else
                    { cmb_PMSDB_Authentication.SelectedIndex = 1; }

                    txt_PMSDB_UserName.Text = clsDBSettings.CurrentDBInfo.SqlUserName;
                    txt_PMSDB_Password.Text = clsDBSettings.CurrentDBInfo.SqlPassword;
                }
                else
                {
                    txt_PMSDB_ServerName.Text = clsDBSettings.CurrentDBInfo.SqlServerName;
                    txt_PMSDB_DatabaseName.Text = clsDBSettings.CurrentDBInfo.DatabaseName;

                    if (clsDBSettings.CurrentDBInfo.IsWindowsAuthentication == true)
                    { cmb_PMSDB_Authentication.SelectedIndex = 0; }
                    else
                    { cmb_PMSDB_Authentication.SelectedIndex = 1; }

                    txt_PMSDB_UserName.Text = clsDBSettings.CurrentDBInfo.SqlUserName;
                    txt_PMSDB_Password.Text = clsDBSettings.CurrentDBInfo.SqlPassword;
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), false);
            }
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
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                clsDBSettings.CurrentDBInfo = new DBSetting(ApplicationType);

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

                clsDBSettings.CurrentDBInfo.IsDBChanged = false;

                if (txt_PMSDB_ServerName.Text != clsDBSettings.CurrentDBInfo.SqlServerName)
                {
                    clsDBSettings.CurrentDBInfo.IsDBChanged = true;
                }

                clsDBSettings.CurrentDBInfo.SqlServerName = txt_PMSDB_ServerName.Text;
                
                if (txt_PMSDB_DatabaseName.Text != clsDBSettings.CurrentDBInfo.DatabaseName)
                {
                    clsDBSettings.CurrentDBInfo.IsDBChanged = true;
                }
                clsDBSettings.CurrentDBInfo.DatabaseName = txt_PMSDB_DatabaseName.Text;

                clsDBSettings.CurrentDBInfo.Application = ApplicationType;

                if (cmb_PMSDB_Authentication.SelectedIndex == 0 || cmb_PMSDB_Authentication.SelectedIndex == 2)
                {
                    clsDBSettings.CurrentDBInfo.IsWindowsAuthentication = true;
                }
                else
                {
                    clsDBSettings.CurrentDBInfo.IsWindowsAuthentication = false;
                }

                clsDBSettings.CurrentDBInfo.SqlUserName = txt_PMSDB_UserName.Text;
                clsDBSettings.CurrentDBInfo.SqlPassword = txt_PMSDB_Password.Text;


                if (!clsDBSettings.ValidateDatabaseSettings())
                {
                    MessageBox.Show("Connection can not established with given parameter, please verify it", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                #region "Write database settings to registry"

                try
                {
                    if (gloRegistrySetting.OpenRemoteBaseKey())
                    {
                        if (ApplicationType ==  DBSetting.ApplicationType.gloEMR)
                        {
                            if (gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrSoftEMR, ""))
                            {
                                gloRegistrySetting.SetRegistryValue("SQLSERVER", clsDBSettings.CurrentDBInfo.SqlServerName);
                                gloRegistrySetting.SetRegistryValue("Database", clsDBSettings.CurrentDBInfo.DatabaseName);

                                if (clsDBSettings.CurrentDBInfo.IsWindowsAuthentication)
                                {
                                    gloRegistrySetting.SetRegistryValue("IsSQLAuthentication", Convert.ToString(false));
                                }
                                else
                                {
                                    gloRegistrySetting.SetRegistryValue("IsSQLAuthentication", Convert.ToString(true));
                                }
                                
                                gloRegistrySetting.SetRegistryValue("SQLUserEMR", clsDBSettings.CurrentDBInfo.SqlUserName);
                                
                                ClsEncryption oEncrypt = new ClsEncryption();
                                string e_sPassword = oEncrypt.EncryptToBase64String(clsDBSettings.CurrentDBInfo.SqlPassword, _encryptionKey);

                                gloRegistrySetting.SetRegistryValue("SQLPasswordEMR", e_sPassword);
                                gloRegistrySetting.CloseRegistryKey();
                            }
                        }
                        else
                        {
                            if (gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrSoftPM, ""))
                            {
                                gloRegistrySetting.SetRegistryValue("SQLSERVER", clsDBSettings.CurrentDBInfo.SqlServerName);
                                gloRegistrySetting.SetRegistryValue("Database", clsDBSettings.CurrentDBInfo.DatabaseName);
                                gloRegistrySetting.SetRegistryValue("ISWINAUTHENTICATION", Convert.ToString(clsDBSettings.CurrentDBInfo.IsWindowsAuthentication));
                                gloRegistrySetting.SetRegistryValue("SQLUSERNAME", clsDBSettings.CurrentDBInfo.SqlUserName);

                                ClsEncryption oEncrypt = new ClsEncryption();
                                string e_sPassword = oEncrypt.EncryptToBase64String(clsDBSettings.CurrentDBInfo.SqlPassword, _SqlEncryptionKey);

                                gloRegistrySetting.SetRegistryValue("SQLPASSWORD", e_sPassword);
                                gloRegistrySetting.CloseRegistryKey();
                            }
                        }
                        gloRegistrySetting.CloseRegistryKey();
                    }
                }
                catch (Exception ex)
                {
                    clsICDAnalysis.UpdateLog(ex.ToString(), false);
                }

                #endregion

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }
    }
}