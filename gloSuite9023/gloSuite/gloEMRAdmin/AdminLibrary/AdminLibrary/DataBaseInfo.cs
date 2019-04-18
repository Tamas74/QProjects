using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;

namespace AdminLibrary
{
    public partial class DataBaseInfo : Form
    {
        private string _messageBoxCaption = "gloEMR Admin";

        public string gloMessageBoxCaption
        {
            get { return _messageBoxCaption; }
            set { _messageBoxCaption = value; }
        }

        public DataBaseInfo()
        {
            InitializeComponent();
            if (CheckRegistryExists())
            {
                object obglodataPath = GetRegistryValue("gloDatapath");
                if (obglodataPath != null && obglodataPath.ToString() != "")
                {
                    txtgloDataPath.Text = obglodataPath.ToString();
                }
                object objServerName = GetRegistryValue("SQLServer");
                if (objServerName != null && objServerName.ToString() != "")
                {
                    txtSqlServer.Text = objServerName.ToString();
                }
                object objDatabase = GetRegistryValue("Database");
                if (objDatabase != null && objDatabase.ToString() != "")
                {
                    txtDataBaseName.Text = objDatabase.ToString();

                }
                object objServerPath = GetRegistryValue("Serverpath");
                if (objServerPath != null && objServerPath.ToString() != "")
                {
                    txtPrerequisitePath.Text = objServerPath.ToString();


                }
            }
            else
            {

            }
           
        }
        public object GetRegistryValue(string _value)
        {


            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\gloEMR", true);

            object value;
            value = oKey.GetValue(_value);
            return value;
        }
        public bool CheckRegistryExists()
        {
            //1.Check Whether gloEMR Registry Key Exists or not
            //2.If it exists  Dont show the Screen
            //3.If it does not exist show the Custom Screen
            bool _success = true;
            RegistryKey oKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\gloEMR", true);
            if (oKey != null && oKey.ToString() != "")
            {
                _success = true;
            }
            else
            {
                _success = false;
            }
            return _success;

        }


        //The properties to get input data
        public string ServerName
        {
            get { return txtSqlServer.Text; }
        }

        public string Database
        {
            get { return txtDataBaseName.Text; }
        }
        public string UserName
        {
            get { return txtUserName.Text; }
        }
        public string Password
        {
            get { return txtPassword.Text; }
        }
        public string ExistingDataBase
        {
            get { return rbtExistingDatabase.Checked.ToString(); }
        }
        public string NewDataBase
        {
            get { return rbtNewDataBase.Checked.ToString(); }
        }
        public string Windows
        {
            get { return rbtWindows.Checked.ToString(); }
        }
        public string Sql
        {
            get { return rbtSQL.Checked.ToString(); }
        }
        public string IsReplication
        {
            get { return chkIsReplication.Checked.ToString(); }
        }
        public string PrerequisitePath
        {
            get { return txtPrerequisitePath.Text; }
        }

        public string gloDataPath
        {
            get { return txtgloDataPath.Text.ToString(); }
        }
        private string _gloDatabaseVersion = "";
        public string gloDatabaseVersion
        {
            get { return _gloDatabaseVersion; }
            set { gloDatabaseVersion = value; }
        }
        //The OK and Cancel button click event handlers.
        public bool IsValidSqlLogin(string userName, string serverName, string password)
        {
            bool _result = false;
            string strConnection = "";
            if (userName != null && userName.ToString() != "" && password != null && password.ToString() != "")
            {
                strConnection = "Data Source='" + serverName + "';User id='" + userName + "';password='" + password + "' ";
            }
            else
            {
                strConnection = "Data Source='" + serverName + "'; Integrated Security=True;";
            }
            SqlConnection Mycon = new SqlConnection(strConnection);
            SqlCommand Mycmd = new SqlCommand();
            try
            {
                Mycon.Open();
                _result = true;
            }
            catch (Exception ex)
            {

                _result = false;
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
        private bool ValidateControls()
        {
            bool _success = true;
            if (rbtWindows.Checked == true)
            {
                if (txtSqlServer.Text != null && txtSqlServer.Text.ToString() != "" && txtDataBaseName.Text != null && txtDataBaseName.Text.ToString() != "")
                {
                    if (IsValidSqlLogin(txtUserName.Text, txtSqlServer.Text, txtPassword.Text))
                    {

                        _success = true;

                    }
                    else
                    {
                        MessageBox.Show("Unable to connect to database. Check credentials.", _messageBoxCaption);
                        this.Focus();
                        // return;
                        _success = false;
                    }
                }
                else
                {
                    MessageBox.Show("Enter Sql Login Credentials", _messageBoxCaption);
                    this.Focus();
                    _success = false;
                }
            }
            else
            {
                if (txtSqlServer.Text != null && txtSqlServer.Text.ToString() != "" && txtDataBaseName.Text != null && txtDataBaseName.Text.ToString() != "" && txtUserName.Text != null && txtUserName.Text.ToString() != "" && txtPassword.Text != null && txtPassword.Text.ToString() != "")
                {
                    if (IsValidSqlLogin(txtUserName.Text, txtSqlServer.Text, txtPassword.Text))
                    {
                        _success = true;
                    }
                    else
                    {
                        MessageBox.Show("Unable to connect to database Check credentials.", _messageBoxCaption);
                        this.Focus();
                        // return;
                        _success = false;
                    }
                }
                else
                {
                    MessageBox.Show("Enter  Sql Login Credentials", _messageBoxCaption);
                    this.Focus();
                    _success = false;
                }
            }
            return _success;
        }



        public bool ValidatePaths()
        {
            bool _success = true;
            if (txtgloDataPath.Text != null && txtgloDataPath.Text.ToString() != "")
            {
                string _append = txtgloDataPath.Text.ToString();

                while (_append.EndsWith("\\"))
                { _append = _append.TrimEnd('\\'); }
                if (CheckDir(_append))
                {
                    _success = true;
                }
                else
                {
                    MessageBox.Show("Enter Valid  gloData Path ", _messageBoxCaption);
                    this.Focus();
                    //return;
                    _success = false;
                }

            }
            else
            {
                MessageBox.Show("Enter gloDataPath", _messageBoxCaption);
                this.Focus();
                //return;
                _success = false;
            }
            return _success;
        }



        private void FillDBVersions()
        {
            try
            {
                //if (rbtExistingDatabase.Checked)
                //{
                //    this.ddlDBVersion.Visible = true;
                //}
                //else
                //{
                //    this.ddlDBVersion.Visible = false;
                //}
                ddlDBVersion.Items.Clear();

                // ddlDBVersion.Items.Add("272");
                ddlDBVersion.Items.Add("4.2.7.3");
                ddlDBVersion.Items.Add("4.2.8.1");
                //ddlDBVersion.Items.Add("28");
                //ddlDBVersion.Items.Add("281");
                //ddlDBVersion.Items.Add("RTM1");
                //ddlDBVersion.Items.Add("HF1");
                //ddlDBVersion.Items.Add("HF2");
                ddlDBVersion.Items.Add("5.0.0.3");

            }
            catch (Exception ex)
            {


            }
        }
        public bool CheckDir(string strPath)
        {
            bool _success = false;
            if (strPath.StartsWith("\\"))
            {
                //string[] arInfo;
                //char[] textdelimiter = { '\\' };

                //arInfo = strPath.Split(textdelimiter);
                // string test = arInfo[arInfo.Length - 1].ToString();
                //string strsource ="\\\\" +arInfo[arInfo.Length - 3].ToString() +"\\"+ arInfo[arInfo.Length - 2].ToString();
                string[] folders = Directory.GetDirectories(strPath);

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
                else
                {
                    _success = false;
                }
            }
            return _success;
        }
        public bool ValidatePrerequisitePath()
        {
            bool _success = true;
            if (txtPrerequisitePath.Text != null && txtPrerequisitePath.Text.ToString() != "")
            {
                string _append = txtPrerequisitePath.Text.ToString();

                while (_append.EndsWith("\\"))
                { _append = _append.TrimEnd('\\'); }

                if (CheckDir(_append))
                {
                    _success = true;
                }
                else
                {
                    MessageBox.Show("Enter Valid  Prerequisites Path ", _messageBoxCaption);
                    this.Focus();
                    //return;
                    _success = false;
                }

            }
            else
            {
                MessageBox.Show("Enter Prerequisites Path", _messageBoxCaption);
                this.Focus();
                //return;
                _success = false;
            }
            return _success;
        }


        public bool ValidateExistingDataBase(string serverName, string DataBaseName, string userName, string password)
        {
            string strConnection = "";
            if (userName != null && userName.ToString() != "" && password != null && password.ToString() != "")
            {
                strConnection = "Data Source='" + serverName + "';User id='" + userName + "';password='" + password + "' ";
            }
            else
            {
                strConnection = "Data Source='" + serverName + "'; Integrated Security=True;";
            }
           
            bool _success = true;
            Int32 _count = -1;
            SqlConnection Mycon = new SqlConnection(strConnection);
            string strQuery = "select name from sys.databases where Name=N'" + DataBaseName.ToString() + "' ";
            DataTable dt = new DataTable();

            SqlCommand Mycmd = new SqlCommand(strQuery, Mycon);
            if (rbtExistingDatabase.Checked == true)
            {
                try
                {
                    Mycon.Open();
                    SqlDataAdapter da = new SqlDataAdapter(Mycmd);
                    da.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        _success = true;
                    }
                    else
                        _success = false;
                }
                catch (Exception ex)
                {
                    _success = false;
                }
                finally
                {
                    if (Mycon != null && Mycon.State == ConnectionState.Open)
                    {
                        Mycon.Close();
                        dt.Dispose();
                    }
                }
            }
            else
            {
                _success = true;
            }


            return _success;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rbtExistingDatabase.Checked && _gloDatabaseVersion.Length == 0&& chkIsReplication.Checked ==false )
            {
                MessageBox.Show("Select database version.", _messageBoxCaption);
                ddlDBVersion.Focus();
                return;
            }
            if (ValidatePrerequisitePath() && ValidatePaths() && ValidateControls() && ValidateExistingDataBase(txtSqlServer.Text.ToString(), txtDataBaseName.Text.ToString(),txtUserName.Text.ToString(),txtPassword.Text.ToString()))
            {
                this.Close();
            }
            else
            {
                this.Focus();
                return;
            }

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void rbtWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtWindows.Checked == true)
            {
                lblUserName.Enabled = false;
                lblPassword.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtUserName.Text = "";
                txtPassword.Text = "";
            }
        }
        private void rbtSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSQL.Checked == true)
            {

                lblUserName.Enabled = true;
                lblPassword.Enabled = true;
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void rbtExistingDatabase_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtExistingDatabase.Checked)
            {
               
                this.chkIsReplication.Visible = true;
                this.chkIsReplication.Checked = true;
            }
            else
            {
                this.ddlDBVersion.Visible = false;
                this.lblDBVersion.Visible = false;
                this.chkIsReplication.Visible = false;
            }
        }

        private void ddlDBVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDBVersion.SelectedText != null)
            {
                _gloDatabaseVersion = ddlDBVersion.Text.ToString();
            }
        }

        private void chkIsReplication_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsReplication.Checked == true)
            {
                lblDBVersion.Visible = false;
                ddlDBVersion.Visible = false;
            }
            else
            {
                this.lblDBVersion.Visible = true;
               this.ddlDBVersion.Visible = true;
                FillDBVersions();
          

            }
        }

        private void txtgloDataPath_Leave(object sender, EventArgs e)
        {
            if (txtgloDataPath.Text != null && txtgloDataPath.Text.ToString() != "" && txtPrerequisitePath.Text.Trim() == "")
            {
                string _append = txtgloDataPath.Text.ToString();
                while (_append.EndsWith("\\"))
                { _append = _append.TrimEnd('\\'); }
                txtPrerequisitePath.Text = _append + "\\Prerequisites";
            }
            else
            {

            }
        }

        private void rbtNewDataBase_CheckedChanged(object sender, EventArgs e)
        {
            this.ddlDBVersion.Visible = false;
                this.lblDBVersion.Visible = false;
                this.chkIsReplication.Visible = false;
        }

        private void DataBaseInfo_Load(object sender, EventArgs e)
        {

        }











    }
}