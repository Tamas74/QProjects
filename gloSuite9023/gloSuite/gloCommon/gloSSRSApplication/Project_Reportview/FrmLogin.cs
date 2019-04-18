using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Project_Reportview;
using mdlGeneral;
//using SSRSApplication.Classes;
using gloDatabaseLayer;
using gloAuditTrail;
using gloSecurity;
using gloSettings;    

namespace SSRSApplication
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }


        #region 'Declarations'

        //#region "App.config Settings Retrive"
        ////Retrieve info from app.Config file
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //

        private string _MessageBoxCaption = string.Empty;
        private string _databaseconnectionstring = "";
       // private string _emrdatabaseconnectionstring = "";
        //private bool _blnIsAcessDenied = false;
        private static int _cntLoginTry = 0;
        //private int _nUserID;
        //private string _provider;
        private string sLoginName;
        private string sPassword;
        private const string _encryptionKey = "12345678";
        //private bool _AddPatientToEMR = false;
        //private string _ProductVersion = " ";


        #endregion 'Declarations'

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                MessageBox.Show("User Name must be entered", "gloReport Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Password must be entered", "gloReport Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                return;
            }

            sLoginName = txtUserName.Text;
            sPassword = txtPassword.Text;

           

            ClsEncryption oEncrypt = new ClsEncryption();
            string e_sPassword = oEncrypt.EncryptToBase64String(sPassword, _encryptionKey);

            gloValidateUser oVU = new gloValidateUser(_databaseconnectionstring);

            if (oEncrypt != null) { oEncrypt.Dispose(); oEncrypt = null; }

            #region " User Authentication & Credentials check"
            ////Check login whether entered user exists.
            //if (!oVU.chkLogin(sLoginName, e_sPassword))
            //{
            //    //added for issue no:  1532
            //    #region  " Check Is Blocked"
            //    if (oVU.chkISBlocked(sLoginName))
            //    {
            //        MessageBox.Show("User is Blocked.Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //        //added on 21/04/2010 case:0004256                   
            //        this.Close();
            //        return;
            //    }
            //    #endregion

            //    #region  " Check Access Deneid"
            //    //Checking for Access is given to user or not.
            //    if (oVU.chkIsAccessDenied(sLoginName))
            //    {
            //        MessageBox.Show("Access denied. Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //        //added on 21/04/2010 case:0004256                   
            //        this.Close();
            //        return;

            //    }
            //    #endregion  " Check Access Deneid"
            //    //--------
            //    // 1. check Reset password have any entry into database //23/04/2010: 



            //    MessageBox.Show("Invalid user name or password.  Please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Failure);

            //    //txtUserName.Text = "";
            //    //txtPassword.Text = "";
            //    txtPassword.Clear();
            //    txtUserName.Focus();
            //    //Checking the No Of Login try only 3 allowed.


            #endregion " User Authentication & Credentials check"

            #region  " Check Is Blocked"
            if (oVU.chkISBlocked(sLoginName))
            {
                MessageBox.Show("User is Blocked.Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //added on 21/04/2010 case:0004256                   
                this.Close();
                return;
            }
            #endregion

            //#region  " Check Access Deneid"
            ////Checking for Access is given to user or not.
            //if (oVU.chkIsAccessDenied(sLoginName))
            //{
            //    MessageBox.Show("Access denied. Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    //added on 21/04/2010 case:0004256                   
            //    this.Close();
            //    return;

            //}
            //#endregion  " Check Access Deneid"


            #region " User Authentication & Credentials check"
            //Check login whether entered user exists.
            if (!oVU.chkLogin(sLoginName, e_sPassword))
            {
                //added for issue no:  1532
                #region  " Check Is Blocked"
                if (oVU.chkISBlocked(sLoginName))
                {
                    MessageBox.Show("User is Blocked.Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //added on 21/04/2010 case:0004256                   
                    this.Close();
                    return;
                }
                #endregion

                #region  " Check Access Deneid"
                //Checking for Access is given to user or not.
                if (oVU.chkIsAccessDenied(sLoginName))
                {
                    MessageBox.Show("Access denied. Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //added on 21/04/2010 case:0004256                   
                    this.Close();
                    return;

                }
                #endregion  " Check Access Deneid"
                //--------
                // 1. check Reset password have any entry into database //23/04/2010: 
                if (oVU.IsPasswordResetted(sLoginName))
                {
                    //Your Password has been reset by the administrator. Please contact the administrator to get the new password.  
                    MessageBox.Show("Your Password has been reset by the administrator. Please contact the administrator to get the new password.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    ////gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Failure);
                    //frmChangePassword ofrmpsw = new frmChangePassword(sLoginName, _databaseconnectionstring);
                    //ofrmpsw.ShowDialog();

                    txtPassword.Clear();
                    txtUserName.Clear();
                    txtUserName.Focus();

                    //-------added on 26/04/2010
                    if (_cntLoginTry < Program.LockOutAttempts - 1) //Added Program.LockOutAttempts - 1 by Debasish Das on 26th Mar 2010
                    {
                        _cntLoginTry = _cntLoginTry + 1;
                    }
                    else
                    {
                        //added on 21/04/2010 case:0004256
                        // set Accesed denied to user becouse all no of lockout attempt failed 
                        //check for Admin - skip admin user for denied 

                        if (!oVU.IsUserAdministrator(sLoginName))
                        {
                            oVU.SetAccessDenied(sLoginName);
                            MessageBox.Show("Access denied. Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            this.Close();
                        }
                        txtPassword.Clear();
                        txtUserName.Focus();

                    }
                    return;
                    //-------
                }

              
               
                MessageBox.Show("Invalid user name or password.  Please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, "Log in ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure);

                //txtUserName.Text = "";
                //txtPassword.Text = "";
                txtPassword.Clear();
                txtUserName.Focus();
                //Checking the No Of Login try only 3 allowed.

                if (_cntLoginTry < Program.LockOutAttempts - 1) //Added Program.LockOutAttempts - 1 by Debasish Das on 26th Mar 2010
                {
                    _cntLoginTry = _cntLoginTry + 1;
                }
                else
                {
                    //added on 21/04/2010 case:0004256
                    // set Accesed denied to user becouse all no of lockout attempt failed 
                    //check for Admin - skip admin user for denied 

                    if (!oVU.IsUserAdministrator(sLoginName))
                    {
                        oVU.SetAccessDenied(sLoginName);
                        MessageBox.Show("Access denied. Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        this.Close();
                    }
                    txtPassword.Clear();
                    txtUserName.Focus();

                }
                return;

            }
            e_sPassword = null;
            #endregion " User Authentication & Credentials check"

            #region  " Set Values for logged in User "

            //Creating object of User who is trying to login 
            //Now we can remove this code since we are setting user in app.config
            gloUser oGetUser = new gloUser(_databaseconnectionstring);
            DataTable dttempUser;
            dttempUser = oGetUser.GetUser(oVU.nUserID);
            
            User oUser = new User();
            try
            {

                oUser.UserID = Convert.ToInt64(dttempUser.Rows[0]["nUserID"]);
                oUser.UserName = dttempUser.Rows[0]["sLoginName"].ToString();
                oUser.Password = dttempUser.Rows[0]["sPassword"].ToString();
                oUser.IsAdministrator = Convert.ToBoolean(dttempUser.Rows[0]["nAdministrator"]);
                if (dttempUser.Rows[0]["nProviderID"] != null)
                    oUser.ProviderID = Convert.ToInt64(dttempUser.Rows[0]["nProviderID"]);

                oUser.IsAuditTrail = Convert.ToBoolean(dttempUser.Rows[0]["IsAuditTrail"]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (oGetUser != null) { oGetUser.Dispose(); oGetUser = null; }
                if (dttempUser != null) { dttempUser.Dispose(); dttempUser = null; }

            }
            #endregion  " Set Values for logged in User "

            #region "Show Main Menu"
            appSettings.Set("DataBaseConnectionString", _databaseconnectionstring);

            if (oVU != null) { oVU.Dispose(); oVU = null; }

            MdiMain ofrmMainMenu = new MdiMain();
            this.Hide();

            ofrmMainMenu.Show();
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, "Log in ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            #endregion

        }       

                   
                
       
        

        //Check Login

        


        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Environment.Exit(0);
            }
            catch (Exception objErr)
            {
                MessageBox.Show(objErr.ToString(), "gloReport Designer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
            }
        }

        private void txtPassword_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == Convert.ToChar(13)))
                {
                    btnLogin_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void txtUserName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == Convert.ToChar(13)))
                {
                    btnLogin_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

            if (_chkSettings())
            {              
                  
                txtUserName.Focus();
              
            }
           

           
        }


        #region "Check setting"
        private bool _chkSettings()
        {
            bool Result = false;
            try
            {
                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
              //  DataSet ds = new DataSet();
                bool blnValidSettings = false;
                //if (oSettings.Read() == false)

                if (oSettings.GetDatabaseSettings_Registry() == true) 
                {
                    oSettings.GetXMLSettings();
                    blnValidSettings = true;
                }
                else
                {
                    blnValidSettings = false;
                }

                //Check Report Settings
                if (oSettings.GetDatabaseSettings_Registry_Reports() == true) 
                    {
                       // oSettings.GetXMLSettings();
                        blnValidSettings = true;
                    }
                else
                    {
                        blnValidSettings = false;
                    }

                // If database settings are not valid then ask user to enter new settings 
                if (blnValidSettings == false)
                {
                    DialogResult oDlgResult = DialogResult.None;
                    oDlgResult = MessageBox.Show("Unable to connect to the database. Please check the database settings.  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (oDlgResult == DialogResult.Yes)
                    {
                        if (oSettings.ShowDatabaseSettings(this) == false)
                        {
                            // if Settings is been Canceled then close the Application
                            object sender = new object();
                            EventArgs erg = new EventArgs();
                            btnCancel_Click(sender, erg);
                            sender = null;
                            erg = null;
                            oSettings.Dispose();
                            oSettings = null;
                            return false;
                        }
                        else
                        {
                            oSettings.GetXMLSettings();
                            oSettings.Dispose();
                            oSettings = null;
                        }
                    }
                    else if (oDlgResult == DialogResult.No)
                    {
                        oSettings.Dispose();
                        oSettings = null;
                        Application.ExitThread();
                    }
                    if (oSettings != null)
                    {
                        oSettings.Dispose();
                        oSettings = null;
                    }
                }

                Program.gSQLServerName = oSettings.SQLServerName;
                Program.gDatabase = oSettings.DatabaseName;
                Program.gWindowsAuthentication = oSettings.WindowsAuthentication;
                Program.gLoginUser = oSettings.LoginUser;
                Program.gLoginPassword = oSettings.LoginPassword;
               


                _databaseconnectionstring = Program.GetConnectionString(Program.gWindowsAuthentication, Program.gSQLServerName, Program.gDatabase, Program.gLoginUser, Program.gLoginPassword);

               

                appSettings["SQLServerName"] = oSettings.SQLServerName;
                appSettings["DatabaseName"] = oSettings.DatabaseName;
                appSettings["SQLLoginName"] = oSettings.LoginUser;
                appSettings["SQLPassword"] = oSettings.LoginPassword;
                appSettings["WindowAuthentication"] = Convert.ToString(oSettings.WindowsAuthentication);
                appSettings["DataBaseConnectionString"] = _databaseconnectionstring;
                        

                //Comment on 11-30-2012 For HL7 outound setting from Database.
                //appSettings["GenerateHL7Message"] = oSettings.GenerateHL7Message.ToString();
                ////Added by Abhijeet on 20110926
                //appSettings["SendPatientDetails"] = oSettings.SendPatientDetails.ToString();
                //appSettings["SendAppointmentDetails"] = oSettings.SendAppointmentDetails.ToString();                
                //appSettings["GenerateOutboundMessage"] = oSettings.GenerateOutboundMessage.ToString();
                //End of code by Abhijeet on 20110926
                
                _MessageBoxCaption = "gloPM";
                appSettings["MessageBOXCaption"] = _MessageBoxCaption;
                
                Result = true;
            }
            catch
            {
                Result = false;
            }
            return Result;
        }
        #endregion

        //protected override void Finalize()
        //{
           
        //    this.Finalize(); 
        //}

        //private void Finalize()
        //{
        //    throw new NotImplementedException();
        //}

        private void Timer1_Tick(System.Object sender, System.EventArgs e)
        {
            Timer1.Enabled = false;

            mdlGeneral.mdlGeneral.gstrgloReportDesignerStartupPath = System.Windows.Forms.Application.StartupPath;
            string aModuleName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;

            string aProcName = System.IO.Path.GetFileNameWithoutExtension(aModuleName);

            if (Process.GetProcessesByName(aProcName).Length > 1)
            {
                MessageBox.Show("Another instance of this application is already running.", "gloReport Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                System.Environment.Exit(0);
                return;
            }


            //string strDate = strings.Format(File.GetLastWriteTime(mdlGeneral.mdlGeneral.gstrgloReportDesignerStartupPath + "\\" + aModuleName), "dd MMM, yyyy") + Constants.vbCrLf + string.Format(File.GetLastWriteTime(mdlGeneral.mdlGeneral.gstrgloReportDesignerStartupPath + "\\" + aModuleName), "hh:mm:ss tt");
            //lblLastModifiedDate.Text = "Software Last Modified " + strDate;
            lblLastModifiedDate.Text = "Software Last Modified  " + File.GetLastWriteTime(Application.StartupPath + "\\" + System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName).ToString("dd MMM, yyyy");
           
           
        }

      

        //public frmLogin()
        //{
        //    Load += frmLogin_Load;
        //}

    }
}
