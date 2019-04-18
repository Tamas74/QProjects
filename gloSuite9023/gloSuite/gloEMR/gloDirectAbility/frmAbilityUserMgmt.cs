using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace gloDirectAbility
{
    public partial class frmAbilityUserMgmt : Form
    {
        #region " Variable Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;       
        private Int64 _nLoginID = 0;       
        private string _messageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
       // private bool _DialgoResult = false;

        #endregion 
        public bool DialgoResult { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    
        #region " Constructor "

        public frmAbilityUserMgmt(string Databaseconnectionstring, Int64 nLoginID)
        {
            InitializeComponent();

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            _databaseconnectionstring = Databaseconnectionstring;
            _nLoginID = nLoginID;

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

        #endregion " Constructor "

        private void frmAbilityUserMgmt_Load(object sender, EventArgs e)
        {
            try
            {
                if (EmailAddress == "")
                {
                    lblMessage.Text = "Enter the gloStream Direct Inbox credentials below.";
                }
                else
                {
                    lblMessage.Text = "The gloStream Direct Inbox credentials given are incorrect. Enter the valid credentials below.";
                    txtEmailAddress.Text = EmailAddress;  
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        private void tsb_SavenClose_Click(object sender, EventArgs e)
        {            
        }

        private bool SaveCredentials()
        {
            gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
            clsAbility oAbility = new clsAbility(_databaseconnectionstring);
            try
            {
                if (txtEmailAddress.Text == "")
                {
                    MessageBox.Show("Please enter Direct address. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmailAddress.Focus();
                    return false;
                }

                if (CheckEmailAddress(txtEmailAddress.Text) == false)
                {
                    MessageBox.Show("Please enter valid Direct address. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmailAddress.Focus();
                    return false;
                }

                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please enter password. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return false;
                }

                if (oAbility.getAuthentication(txtEmailAddress.Text, txtPassword.Text)==false)
                {
                    MessageBox.Show("Wrong email/login and/or password. Authentication failed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return false;
                }
               
                string sEmail = txtEmailAddress.Text;
                string sPassword = string.Empty;
                string _encryptionKey = "12345678";

                EmailAddress = txtEmailAddress.Text;
                Password = txtPassword.Text;

                sPassword = oClsEncryption.EncryptToBase64String(txtPassword.Text, _encryptionKey);
                oAbility.UpdateAbilityEmail(_nLoginID, sEmail, sPassword);

                this.DialgoResult = true;
               // this.Close();
                return true;
            }
            catch (Exception ex)
            {              
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oAbility != null) { oAbility.Dispose(); }
                if (oClsEncryption != null) { oClsEncryption.Dispose(); }
            }
        }

        private bool CheckEmailAddress(string input)
        {

            bool result = false;

            if ((Regex.IsMatch(input, "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*") | object.ReferenceEquals(input.Trim(), "")))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialgoResult = false;
            this.Close();
        }

        private void frmAbilityUserMgmt_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (chkSaveCredential.Checked == true)
            //{
            //    if (SaveCredentials() == false)
            //    {
            //        e.Cancel = true;
            //    }
            //    else
            //    {
            //        DialgoResult = true; 
            //        e.Cancel = false;
            //    }

            //}
            //else
            //{
            //    if (txtEmailAddress.Text.Trim() != "")
            //    {
            //        if (CheckEmailAddress(txtEmailAddress.Text) == false)
            //        {
            //            MessageBox.Show("Please enter valid e-mail address. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtEmailAddress.Focus();
            //            e.Cancel = true;
            //            return;
            //        }
            //        if (txtPassword.Text == "")
            //        {
            //            MessageBox.Show("Please enter password. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtPassword.Focus();
            //            e.Cancel = true;
            //            return;
            //        }
            //        EmailAddress = txtEmailAddress.Text;
            //        Password = txtPassword.Text;
            //        DialgoResult = true;
            //        e.Cancel = false;
            //    }
            //    else
            //    {
            //        DialgoResult = false;
            //        e.Cancel = false;
            //    }
            //}

        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            clsAbility oAbility = new clsAbility(_databaseconnectionstring);
            try
            {
                if (chkSaveCredential.Checked == true)
                {
                    if (SaveCredentials() == false)
                    {
                        return;
                    }
                    else
                    {
                        DialgoResult = true;
                        this.Close();
                    }

                }
                else
                {
                    if (CheckEmailAddress(txtEmailAddress.Text) == false)
                    {
                        MessageBox.Show("Please enter valid Direct address. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEmailAddress.Focus();
                        return;
                    }
                    if (txtPassword.Text == "")
                    {
                        MessageBox.Show("Please enter password. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Focus();
                        return;
                    }
                    if (oAbility.getAuthentication(txtEmailAddress.Text, txtPassword.Text) == false)
                    {
                        MessageBox.Show("Wrong email/login and/or password. Authentication failed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Focus();
                        return ;
                    }

                    EmailAddress = txtEmailAddress.Text;
                    Password = txtPassword.Text;
                    DialgoResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAbility != null) { oAbility.Dispose(); }
            }
        }

    }
}