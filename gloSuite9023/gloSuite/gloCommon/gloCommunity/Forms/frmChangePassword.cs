using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;

namespace gloCommunity.Forms
{
    public partial class frmChangePassword : Form
    {
        string _encryptionKey = "12345678";
        public frmChangePassword()
        {
            InitializeComponent();
            //Added for fixed Bug # : 32429 on 20120803
            txtUserNm.Text = clsGeneral.gstrGCUserName;
            //End
        }

        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            UserManagement.UserManagementService objUserManagement = null;
            clsEncryption oclsEncryption = null;
            clsgloCommunityUsers objclsgloCommunityUsers = null;
            try
            {
                if (string.IsNullOrEmpty(txtUserNm.Text.Trim()))
                {
                    MessageBox.Show("Please enter User Name", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserNm.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("Please enter Password", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtNewPwd.Text.Trim()))
                {
                    MessageBox.Show("Please enter New Password", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewPwd.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtConfirmPwd.Text.Trim()))
                {
                    MessageBox.Show("Please enter Confirm Password", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtConfirmPwd.Focus();
                    return;
                }

                if (txtNewPwd.Text.Trim() != txtConfirmPwd.Text.Trim())
                {
                    MessageBox.Show("New Password & Confirmation Password must match", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtConfirmPwd.Focus();
                    return;
                }

                //Change Password
                objUserManagement = new UserManagement.UserManagementService();
                objUserManagement.Url = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstr_Layouts + "/UserManagementService.asmx";


                if (objUserManagement.SPChangePassword(txtUserNm.Text, txtPassword.Text, txtNewPwd.Text) == true)
                {
                    oclsEncryption = new clsEncryption();
                    string _strEncryptPWD = oclsEncryption.EncryptToBase64String(txtNewPwd.Text.Trim(), _encryptionKey);

                    objclsgloCommunityUsers = new clsgloCommunityUsers();
                    objclsgloCommunityUsers.ChangeGCUSerPassword(clsGeneral.gnLoginID, txtUserNm.Text, _strEncryptPWD);

                    MessageBox.Show("Password change successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("User Name or Password is invalid", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (objUserManagement != null)
                {
                    objUserManagement.Dispose();
                    objUserManagement = null;
                }
                if (oclsEncryption != null)
                    oclsEncryption = null;
                if (objclsgloCommunityUsers != null)
                    objclsgloCommunityUsers = null;

                this.Cursor = Cursors.Default;
            }
           
        }
    }
}
