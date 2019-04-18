using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;

namespace SampleLibrary
{
    public partial class DataBaseInfo : Form
    {
        private string _messageBoxCaption = "gloEMR Client";

        public string gloMessageBoxCaption
        {
            get { return _messageBoxCaption; }
            set { _messageBoxCaption = value; }
        }

        public DataBaseInfo()
        {
            InitializeComponent();
            //string strTest="\\";
            //strTest += "\\glotest\\glodata\\setup\\prerequisites";
            //txtgloDataPath.Text = strTest;       
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
        public string  gloVoice
        {
            get{return rbtVoice.Checked.ToString();}
        }
        public string   gloNonVoice
        {
            get { return rbtNonVoice.Checked.ToString(); }
        }
        public string  Windows
        {
            get { return rbtWindows.Checked.ToString(); }
        }
        public string  Sql
        {
            get { return rbtSQL.Checked.ToString(); }
        }
        
        public string gloDataPath
        {
            get { return txtgloDataPath.Text; }
        }
        public string ServerPath
        {
            get { return txtServer.Text.ToString(); }
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
            SqlCommand Mycmd=new SqlCommand();
            try
            {
                 Mycon.Open();
                _result =true;
            }
            catch (Exception ex)
            {
  
                _result=  false;
            }
            finally
            {
                if(Mycon != null && Mycon.State == ConnectionState.Open)
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
                if (txtSqlServer.Text != null && txtSqlServer.Text.ToString() != "" && txtDataBaseName.Text != null && txtDataBaseName.Text.ToString() !="")
                {
                    if (IsValidSqlLogin(txtUserName.Text, txtSqlServer.Text, txtPassword.Text))
                    {
                        if (rbtVoice.Checked == true)
                        {
                            if (ValidategloVoice())
                            {
                                _success = true;
                            }
                            else
                            {
                                _success = false;
                            }
                        }
                        else
                        {

                            _success = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to connect to database  Check credentials.",_messageBoxCaption);
                        this.Focus();
                        //return;
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

                        if (rbtVoice.Checked == true)
                        {
                            if (ValidategloVoice())
                            {
                                _success = true;
                            }
                            else
                            {
                                _success = false;
                            }
                        }
                        else
                        {

                            _success = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to connect to database Check credentials.", _messageBoxCaption);
                        this.Focus();
                        
                        _success = false;
                    }
                }
                else
                {
                    MessageBox.Show("Enter  Sql Login Credentials",_messageBoxCaption);
                    this.Focus();
                    _success = false;
                }
            }
            return _success;
        }
        public bool ValidategloVoice()
        {
            bool _success = true;
            if (rbtVoice.Checked == true)
            {
                if (ValidateProcess())
                {
                    _success = true;
                }
                else
                {
                    this.Focus();
                    _success = false;
                   // return;
                }
            }
            return _success;
        }
        public bool ValidateProcess()
        {
            bool _Success = true;
            Process[] pname = Process.GetProcessesByName("Excel");
            if (pname.Length == 0)
            {
                if (_Success == true)
                {
                    _Success = true;
                }
                else
                {
                    _Success = false;
                }
            }
            else
            {
                MessageBox.Show("Close All  Microsoft Excel Documents ");
                this.Focus();
                _Success = false;
            
            }


            Process[] pnameWinword = Process.GetProcessesByName("Winword");
            if (pnameWinword.Length == 0)
            {
                if (_Success == true)
                {
                    _Success = true;
                }
                else
                {
                    _Success = false;
                }
            }
            else
            {
                MessageBox.Show("Close All Microsoft Word Documents");
                this.Focus();
                _Success = false;
               
            }
            Process[] pnameOutlook = Process.GetProcessesByName("Outlook");
            if (pnameOutlook.Length == 0)
            {
                if (_Success == true)
                {
                    _Success = true;
                }
                else
                {
                    _Success = false;
                }
            }
            else
            {
                MessageBox.Show("Close Microsoft Outlook");
                this.Focus();
                _Success = false;
             
            }
            return _Success;
        }

        public bool ValidateFeature()
        {
            bool _sucess = true;
            if (rbtVoice.Checked == false && rbtNonVoice.Checked==false)
            {
                MessageBox.Show("Select Feature", _messageBoxCaption);
                this.Focus();
               // return;
                _sucess = false;
            }
            else 
            {
                _sucess = true;
            }
            return _sucess;
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
                    MessageBox.Show("Enter Valid Prerequisites Path ", _messageBoxCaption);
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
            //string strConnection = "Data Source='" + serverName + "'; Integrated Security=True;";
            bool _success = true;
            Int32 _count = -1;
            SqlConnection Mycon = new SqlConnection(strConnection);
            string strQuery = "select name from sys.databases where Name=N'" + DataBaseName.ToString() + "' ";
            DataTable dt = new DataTable();

            SqlCommand Mycmd = new SqlCommand(strQuery, Mycon);

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
                {
                    _success = false;
                    MessageBox.Show("Unable to connect to database");
                    this.Focus();
                    //return;
                }

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




            return _success;
        }
      
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateServerPath() && ValidatePaths() && ValidateFeature() && ValidateControls()&& ValidateExistingDataBase(txtSqlServer.Text.ToString(),txtDataBaseName.Text.ToString(),txtUserName.Text.ToString(),txtPassword.Text.ToString()))
            {
                this.Close();

            }
            else
            {
                this.Focus();
                return;
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
                    else
                    {
                        _success =false;
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
        public bool ValidateServerPath()
        {
            bool _success = true;
            if (txtServer.Text != null && txtServer.Text.ToString() != "")
            {
                string _append = txtServer.Text.ToString();

                while (_append.EndsWith("\\"))
                { _append = _append.TrimEnd('\\'); }
                if (CheckDir(_append))
                {
                    _success = true;
                }
                else
                {
                    MessageBox.Show("Enter Valid gloData Path ", _messageBoxCaption);
                    this.Focus();
                    //return;
                    _success = false;
                }

            }
            else
            {
                MessageBox.Show("Enter gloData Path", _messageBoxCaption);
                this.Focus();
                //return;
                _success = false;
            }
            return _success;
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

        

        private void rbtVoice_CheckedChanged(object sender, EventArgs e)
        {
           
        }
        private void rbtNonVoice_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtServer_Leave(object sender, EventArgs e)
        {
            if (txtServer.Text != null && txtServer.Text.ToString() != "" && txtgloDataPath.Text.Trim() == "")
            {
                string _append = txtServer.Text.ToString();
                while (_append.EndsWith("\\"))
                { _append = _append.TrimEnd('\\'); }
                txtgloDataPath.Text = _append + "\\Prerequisites";
            }
        }
      
        
        
    }
}