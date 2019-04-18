using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloSecurity;
using gloGlobal;


namespace gloPM
{
    public partial class frmChangePassword : Form
    {
        #region " Declaration "

        private const string _encryptionKey = "12345678";
        private string _sLoginName;
        private string psw = string.Empty;
        private string msg = string.Empty;

        #endregion

        #region " Constructor "

        public frmChangePassword(string sLoginName, string _databaseConString)
        {
            InitializeComponent();

            _sLoginName = sLoginName;
        }


        #endregion

        #region Property Procedure

        public string LoginName
        {
            get { return _sLoginName; }
            set { _sLoginName = value; }

        }

        #endregion

        private void btn_tls_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                //_databaseconnectionstring = Program.GetConnectionString(Program.gWindowsAuthentication, Program.gSQLServerName, Program.gDatabase, Program.gLoginUser, Program.gLoginPassword);

                if (txtChangePassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {                    
                    MessageBox.Show("Confirm Password should be same as the password.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information );

                    txtConfirmPassword.Text = "";
                    txtConfirmPassword.Focus();
                    return ;
                }

                gloValidateUser oVU = new gloValidateUser(gloPMGlobal.DatabaseConnectionString);
                psw = txtChangePassword.Text.Trim();

                if (psw.Trim().Length > 0)
                {
                    //check  user is administrator ? 
                    if (!oVU.IsUserAdministrator(LoginName))
                    {
                        // check Password complexity 
                        if (oVU.ValidatePassword(ref msg,LoginName, psw))
                        {
                            //check recently used password
                            if (!oVU.IsRecentUsedPSW(LoginName, psw)) // not implemented fully
                            {
                                ClsEncryption oEncrypt = new ClsEncryption();
                                string e_sPassword = oEncrypt.EncryptToBase64String(psw, _encryptionKey);

                                // update UserMst with psw & IsResentPassword=0

                                oVU.UpdateUserMST_RecentPwdMST(LoginName, e_sPassword);

                                MessageBox.Show("You will have to log in again", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                               
                                this.Close();
                                //return;
                            }
                            else
                            {
                                MessageBox.Show("You have already used this password recently, so select another password.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtChangePassword.Text = "";
                                txtConfirmPassword.Text = "";
                                txtChangePassword.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (msg.Length > 0 )
                            {
                                MessageBox.Show(msg.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            txtChangePassword.Text = "";
                            txtConfirmPassword.Text = "";
                            txtChangePassword.Focus();
                            return;
                        }
                    }
                    else
                    {
                        ClsEncryption oEncrypt = new ClsEncryption();
                        string e_sPassword = oEncrypt.EncryptToBase64String(psw, _encryptionKey);

                        // update UserMst with psw & IsResentPassword=0
                        oVU.UpdateUserMST_RecentPwdMST(LoginName, e_sPassword); 

                        //issue no:1533
                        MessageBox.Show("You will have to log in again", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    } 

                }
                else 
                {
                    MessageBox.Show("Enter new Password ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtConfirmPassword.Text = "";
                    txtConfirmPassword.Focus();
                    return;

                }              
        
            }
            catch (Exception  ex )
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.ChangePassword , gloAuditTrail.ActivityType.Login , "Change password Log in ", gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally 
            {
            }
        }

        //private void frmChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    e.Cancel;
            
        //}
    }



}