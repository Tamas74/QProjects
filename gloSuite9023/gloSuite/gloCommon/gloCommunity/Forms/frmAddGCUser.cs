using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using System.Net;
using System.Xml;
using Microsoft.Web.Administration;
using System.Security.Cryptography.X509Certificates;
namespace gloCommunity.Forms
{
    public partial class frmAddGCUser : Form
    {
        string _encryptionKey = "12345678";
    //    string _strEncryptPWD = "";
        public frmAddGCUser()
        {
            InitializeComponent();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            UserManagement.UserManagementService objUserManagementService = null;
            clsgloCommunityUsers objclsgloCommunityUsers = null;
            clsEncryption oclsEncryption = null;
            try
            {
                #region Validations

                //Install certificate as per staging/production environment.
                if (InstallCertificate() == false)
                {
                    MessageBox.Show("Error in installing certificate", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserNm.Focus();
                    return;
                }
                //end

                if (string.IsNullOrEmpty(clsGeneral.gstrClinicName.Trim()))
                {
                    MessageBox.Show("Please enter Clinic details from admin", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserNm.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtUserNm.Text.Trim()))
                {
                    MessageBox.Show("Please enter User Name", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserNm.Focus();
                    return;
                }
                if (CheckSpecialCharacters(txtUserNm.Text) == false)
                {
                    MessageBox.Show("Please enter Valid User Name", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserNm.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("Please enter Password", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return;
                }
                if (CheckPassword(txtPassword.Text.Trim()) == false)
                {
                    txtPassword.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtConfirmPwd.Text.Trim()))
                {
                    MessageBox.Show("Please enter Confirm Password", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtConfirmPwd.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Please enter E-mail", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmail.Focus();
                    return;
                }
                if (CheckEmail(txtEmail.Text) == false)
                {
                    MessageBox.Show("Please enter valid E-mail address", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmail.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtQuestion.Text.Trim()))
                {
                    MessageBox.Show("Please enter Security Question", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQuestion.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAnswer.Text.Trim()))
                {
                    MessageBox.Show("Please enter Security Answer", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAnswer.Focus();
                    return;
                }
                if (txtPassword.Text.Trim() != txtConfirmPwd.Text.Trim())
                {
                    MessageBox.Show("Password & Confirmation Password must match", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtConfirmPwd.Focus();
                    return;
                }
                #endregion

                //Add User
                objUserManagementService = new UserManagement.UserManagementService();
                objUserManagementService.Url = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstr_Layouts + "/UserManagementService.asmx";

                oclsEncryption = new clsEncryption();
                string _strEncryptPWD = oclsEncryption.EncryptToBase64String(txtPassword.Text.Trim(), _encryptionKey);

                objclsgloCommunityUsers = new clsgloCommunityUsers();
                DataTable dtClinic = objclsgloCommunityUsers.getClinicData();
                if (dtClinic != null && dtClinic.Rows.Count > 0)
                {
                    string strStatus = objUserManagementService.CreateUser(txtUserNm.Text, txtPassword.Text, txtEmail.Text, txtQuestion.Text, txtAnswer.Text, true, dtClinic.Rows[0]["sClinicName"].ToString(), dtClinic.Rows[0]["sExternalcode"].ToString());

                    switch (strStatus)
                    {
                        case "DuplicateUserName":
                            MessageBox.Show("Username already exists. Please enter a different user name.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        case "DuplicateEmail":
                            MessageBox.Show("A username for that e-mail address already exists. Please enter a different e-mail address.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        case "InvalidPassword":
                            MessageBox.Show("The password provided is invalid. Please enter a valid password value.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        case "InvalidEmail":
                            MessageBox.Show("The E-mail address provided is invalid. Please check the value and try again.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        case "InvalidAnswer":
                            MessageBox.Show("The password retrieval answer provided is invalid. Please check the value and try again.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        case "InvalidQuestion":
                            MessageBox.Show("The password retrieval question provided is invalid. Please check the value and try again.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        case "InvalidUserName":
                            MessageBox.Show("The user name provided is invalid. Please check the value and try again.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        case "ProviderError":
                            MessageBox.Show("The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        case "UserRejected":
                            MessageBox.Show("The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        case "Success":
                            {
                                objclsgloCommunityUsers.InsertGCUSer(clsGeneral.gnLoginID, txtUserNm.Text, _strEncryptPWD, clsGeneral.gblnIscommunityStaging);

                                gloCommunity.Classes.clsgloCommunityUsers clsGCUSer = new gloCommunity.Classes.clsgloCommunityUsers();
                                DataTable dtUSer = clsGCUSer.getGCUser(clsGeneral.gnLoginID, clsGeneral.gblnIscommunityStaging);
                                if ((dtUSer != null) & dtUSer.Rows.Count > 0)
                                {
                                    //'get gloCommunity username & password as per login id

                                    _strEncryptPWD = oclsEncryption.DecryptFromBase64String(dtUSer.Rows[0]["gc_sPassword"].ToString(), _encryptionKey);
                                    gloCommunity.Classes.clsGeneral.gstrGCUserName = dtUSer.Rows[0]["gc_sUserName"].ToString();
                                    gloCommunity.Classes.clsGeneral.gstrGCPassword = _strEncryptPWD;
                                }
                                if (IsSiteExist() == false)//if site not exist then create
                                {

                                    if (CreateSite() == true)
                                        MessageBox.Show("User has been created successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("User has been created successfully.\n Please Note: Failed to create site please contact to system administrator.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("User has been created successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                this.Close();
                                break;
                            }
                    }
                }
                else
                    MessageBox.Show("Please enter Clinic details from admin", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {
            }
            finally
            {
                if (objUserManagementService != null)
                {
                    objUserManagementService.Dispose();
                    objUserManagementService = null;
                }
                if (objclsgloCommunityUsers != null)
                    objclsgloCommunityUsers = null;
                if (oclsEncryption != null)
                    oclsEncryption = null;
                this.Cursor = Cursors.Default;
            }

        }

        private bool CheckEmail(string EmailAddress)
        {
            string strPattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (System.Text.RegularExpressions.Regex.IsMatch(EmailAddress, strPattern))

            { return true; }
            return false;
        }

        private bool CheckSpecialCharacters(string _text)
        {
            string strPattern = "^[a-zA-Z0-9_.-]+$";
            if (System.Text.RegularExpressions.Regex.IsMatch(_text, strPattern))
            {
                return true;
            }
            return false;
        }

        private bool CheckPassword(string _text)
        {
            string strPattern = string.Empty;
            try
            {
                strPattern = "^.*(?=.{7,}).*$";
                if (System.Text.RegularExpressions.Regex.IsMatch(_text, strPattern))
                {
                    strPattern = "^.*(?=.*[!@#$%^&+=*]).*$";
                    if (System.Text.RegularExpressions.Regex.IsMatch(_text, strPattern))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Password should contain at least 1 special character", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Password should contain atleast 7 characters,which include 1 special character", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
               // return false;
            }
            catch //(Exception ex)
            {
                return false;
            }
            finally
            {
                strPattern = string.Empty;
            }
        }

        private void txtConfirmPwd_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Please enter Password", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }

        }

        #region Create Site
        private bool CreateSite()
        {
            bool _result = false;
            MeetingService.Meetings WMService = null;
            try
            {
                WMService = new MeetingService.Meetings();
                WMService.Url = clsGeneral.gstrSharepointSrvNm;//ConfigurationManager.AppSettings["SharePointSiteAddress"];
                WMService.CookieContainer = new CookieContainer();

                if (clsGeneral.oFormCookie == null)
                    WMService.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                else
                    WMService.CookieContainer.Add(clsGeneral.oFormCookie);

                MeetingService.TimeZoneInf tz = new MeetingService.TimeZoneInf();

                if ((WMService.Url.EndsWith("/")))
                {
                    WMService.Url = WMService.Url.Remove(WMService.Url.Length - 1, 1);
                }

                WMService.Url = WMService.Url + clsGeneral.gstrSharepointSiteNm + "/_vti_bin/meetings.asmx";

                System.Xml.XmlNode CreateWorkspaceResponse = default(System.Xml.XmlNode);

                if ((!string.IsNullOrEmpty(WMService.Url)))
                {
                    XmlNode temp = WMService.GetMeetingWorkspaces(false);
                    // "STS#0" for Team site & 1033 for language(Eng-Us)
                    CreateWorkspaceResponse = WMService.CreateWorkspace(clsGeneral.gstrClinicName.Trim(), "STS#0", System.UInt32.Parse("1033"), tz);

                    gloLists.Lists objList = new gloLists.Lists();

                    objList.CookieContainer = new CookieContainer();
                    if (clsGeneral.oFormCookie == null)
                        objList.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                    else
                        objList.CookieContainer.Add(clsGeneral.oFormCookie);


                    objList.Url = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName.Trim() + "/_vti_bin/lists.asmx";

                    objList.AddList("DataConnections", null, 130);//130 for DataConnections Library
                    string xml = "<Batch OnError='Continue'>";
                    xml += "<List EnableModeration='False'/>";
                    xml += "</Batch>";
                    XmlNode listNode = objList.GetList("DataConnections");
                    XmlNode version = listNode.Attributes["Version"];
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNode propertiesNode = doc.SelectSingleNode("//List");
                    XmlNode resultNode = objList.UpdateList("DataConnections", propertiesNode, null, null, null, version.Value);

                    objList.AddList("Repository", null, 101);//101 for Document Library
                    _result = true;
                }
            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
            finally
            {
                if (WMService != null)
                {
                    WMService.Dispose();
                }
            }
            return _result;
        }

        private bool IsSiteExist()
        {
            bool _result = true;
            try
            {
                gloLists.Lists olst = new gloLists.Lists();

                olst.CookieContainer = new CookieContainer();
                if (clsGeneral.oFormCookie == null)
                    olst.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                else
                    olst.CookieContainer.Add(clsGeneral.oFormCookie);

                olst.Url = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName.Trim() + "/_vti_bin/lists.asmx";
                XmlNode node = olst.GetListCollection();

            }
            catch //(Exception ex)
            {
                _result = false;
            }
            return _result;
        }

        private bool InstallCertificate()
        {
            bool _IsCertInstalled = false;

            try
            {
                ServerManager mgr = null;
                string server = Environment.MachineName; // or remote machine name
                if (String.IsNullOrEmpty(server))
                {
                    mgr = new ServerManager();
                }
                else
                {
                    mgr = ServerManager.OpenRemote(server);
                }

                //For some reason this created a new store called 'Personal' - we'll have to figure that out
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadWrite);

                //Looks like we can create this from a byte array as well
                string _strgloCommunityCert = "StagingCert.cer";
                if (clsGeneral.gblnIscommunityStaging == false)
                    _strgloCommunityCert = "ProductionCert.cer";

                X509Certificate2 certificate = new X509Certificate2(System.Windows.Forms.Application.StartupPath + "\\gloCommunityCertificates\\" + _strgloCommunityCert);

                store.Add(certificate);
                //object thumb = "bf 08 fc e1 13 9e 96 0e b0 43 8d 07 92 97 5c 5a c0 59 e6 19";
                //X509Certificate2Collection test = store.Certificates.Find(X509FindType.FindByThumbprint, thumb, false);
                store.Close();
                _IsCertInstalled = true;
            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
            return _IsCertInstalled;
        }
        #endregion
    }
}
