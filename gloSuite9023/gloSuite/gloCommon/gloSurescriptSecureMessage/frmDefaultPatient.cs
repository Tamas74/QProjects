using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Text.RegularExpressions;

namespace gloSurescriptSecureMessage
{
    public partial class frmDefaultPatient : Form
    {

        private string _defaultName = "";
        public string DefaultName
        {
            get { return _defaultName; }
            set { _defaultName = value; }
        }

        public frmDefaultPatient()
        {
            InitializeComponent();
            txtName.Focus();
        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            bool _isValid = true;
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" +
             @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" +
             @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            if (txtName.Text.Trim() != "")
            {
                string[] strArray = null;
                strArray = txtName.Text.Trim().Split(';');

                if (strArray.Length > 0)
                {
                    for (int i = 0; i <= strArray.Length - 1; i++)
                    {
                        if (strArray[i].ToString() != "")
                        {
                            System.Text.RegularExpressions.Match match =
                            Regex.Match(strArray[i].ToString(), pattern, RegexOptions.IgnoreCase);
                            if (match.Success)
                            {
                                //  DefaultName = txtName.Text.Trim();
                                //  this.Close();
                            }
                            else
                            {
                               // _isValid = false;
                               // System.Windows.MessageBox.Show(strArray[i].ToString() + " is not a valid email address. Please enter valid email address", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                               // txtName.Focus();
                            }


                            if (strArray[i].Length > 254)
                            {
                                _isValid = false;
                                System.Windows.MessageBox.Show("Please enter valid email address '"+ strArray[i].ToString() + "'", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                                txtName.Focus();
                            }
                        }
                        else
                        {
                            _isValid = false;
                            System.Windows.MessageBox.Show("Please enter valid email address '"+ strArray[i].ToString() + "'", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                            txtName.Focus();
                        }

                    }

                    if (_isValid == true)
                    {
                        DefaultName = txtName.Text.Trim();
                        this.Close();
                    }
                }          
              
            }
            else
            {
                System.Windows.MessageBox.Show("Please enter email address", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                txtName.Focus();
            }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            DefaultName = "";
            this.Close();
        }
    }
}
