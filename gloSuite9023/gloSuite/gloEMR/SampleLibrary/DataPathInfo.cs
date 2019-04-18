using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

namespace SampleLibrary
{
    public partial class DataPathInfo : Form
    {
        private string _messageBoxCaption = "gloEMR Client ";

        public string gloMessageBoxCaption
        {
            get { return _messageBoxCaption; }
            set { _messageBoxCaption = value; }
        }
        public DataPathInfo()
        {
            InitializeComponent();
            if (CheckRegistryExists())
            {
                object obServerPath = GetRegistryValue("ServerPath");
                if (obServerPath != null && obServerPath.ToString() != "")
                {
                    txtPrerequisitesPath.Text = obServerPath.ToString();
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
        public string gloVoiceInfo
        {
            get { return rbtVoice.Checked.ToString(); }
        }
        public string gloNonVoiceInfo
        {
            get { return rbtNonVoice.Checked.ToString(); }
        }
        public string gloDataPathInfo
        {
            get { return txtPrerequisitesPath.Text; }
        }
        public bool ValidateFeature()
        {
            bool _sucess = true;
            if (rbtVoice.Checked == false && rbtNonVoice.Checked == false)
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
                        _success = false;
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
        public bool ValidatePaths()
        {
            bool _success = true;
            if (txtPrerequisitesPath.Text != null && txtPrerequisitesPath.Text.ToString() != "")
            {
                if (CheckDir(txtPrerequisitesPath.Text.ToString()))
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

       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidatePaths()&& ValidateFeature() && ValidategloVoice())
            {
                this.Close();

            }
            else
            {
                this.Focus();
                return;
            }
            
        }
    }
}