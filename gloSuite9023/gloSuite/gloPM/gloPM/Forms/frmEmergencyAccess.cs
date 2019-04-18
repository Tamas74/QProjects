using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloGlobal;

namespace gloPM.Forms
{
    public partial class frmEmergencyAccess : Form
    {

        #region " Declarations "
        private const string _encryptionKey = "12345678";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string AccessingForm = string.Empty;
        #endregion

        #region " Constructor "
        public frmEmergencyAccess()
        {
            InitializeComponent();
        }


        public frmEmergencyAccess(String CallingForm)
        {
            InitializeComponent();
            AccessingForm = CallingForm;
        }
        #endregion

        #region " Form Events "
        private void frmEmergencyAccess_Load(object sender, EventArgs e)
        {
            txtPassword.Clear();

            if (AccessingForm == string.Empty)
            {
                this.Text = "Allow Emergency Access of Patient Chart";
            }
            else
            {
                this.Text = "Authenticate User";
            }
        }
        #endregion

        #region " Toolstrip Button Clicks "
        private void btnOK_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloSecurity.ClsEncryption  oEncryption = null;

            try
            {
                if (txtPassword.Text.ToString() == "")
                {
                    MessageBox.Show("Enter Emergency Access Password.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return;
                }

                oEncryption = new gloSecurity.ClsEncryption();
                string _Password = oEncryption.EncryptToBase64String(txtPassword.Text.ToString(), _encryptionKey);
                oDB = new gloDatabaseLayer.DBLayer(Program.GetConnectionString());
                oDB.Connect(false);
                if (AccessingForm == string.Empty)
                {
                    Object oCount = oDB.ExecuteScalar_Query("SELECT COUNT(NUSERID) FROM USER_MST WHERE NUSERID=" + gloSettings.AppSettings.UserID.ToString() + " AND sAccessPassword = '" + _Password.Replace("'", "''") + "'");
                    oDB.Disconnect();
                    if (oCount != null && oCount.ToString() != "")
                        if (Convert.ToInt32(oCount.ToString()) > 0)
                        {
                            appSettings["BreakTheGlass"] = "true";
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.BreakTheGlass, gloAuditTrail.ActivityType.EmergencyAccess, "Emergency Access Login Successful.", 0, 0, gloSettings.AppSettings.UserID, gloAuditTrail.ActivityOutCome.Success);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            appSettings["BreakTheGlass"] = "false";
                            MessageBox.Show("Incorrect Password.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.BreakTheGlass, gloAuditTrail.ActivityType.EmergencyAccess, "Incorrect Password for Emergency Access.", 0, 0, gloSettings.AppSettings.UserID, gloAuditTrail.ActivityOutCome.Failure);
                            txtPassword.Focus();
                            return;
                        }
                }
                else
                {
                    if (AccessingForm == "TRIARQ Admin Reports")
                    {
                        Object oPassword = oDB.ExecuteScalar_Query("SELECT sSettingsValue FROM settings WHERE sSettingsName='TRIARQADMINREPORTPASSWORD'");
                        if (Convert.ToString(oPassword)!="")
                        {
                            if (txtPassword.Text.Trim() == oEncryption.DecryptFromBase64String(Convert.ToString(oPassword), _encryptionKey).Trim())
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.BreakTheGlass, gloAuditTrail.ActivityType.EmergencyAccess, "Emergency Access Login Successful.", 0, 0, gloSettings.AppSettings.UserID, gloAuditTrail.ActivityOutCome.Success);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Password.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.BreakTheGlass, gloAuditTrail.ActivityType.EmergencyAccess, "Incorrect Password for Emergency Access.", 0, 0, gloSettings.AppSettings.UserID, gloAuditTrail.ActivityOutCome.Failure);
                                txtPassword.Focus();
                                return;
                            }

                        }
                        oDB.Disconnect();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oEncryption != null) { oEncryption.Dispose(); oEncryption = null; }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 

        #region " Private Methods "

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(sender, e);
        }

        #endregion 

    }
}
