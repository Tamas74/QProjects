using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using gloSecurity;
using gloGlobal;

namespace gloPM
{
    public partial class frmLockScreen : gloAUSLibrary.MasterForm
    {
       
        public frmLockScreen()
        {
            InitializeComponent();

            _UserID = gloPMGlobal.UserID;
            _UserName = gloPMGlobal.UserName;
        }

        #region 'Declarations'

        ////#region "App.config Settings Retrive"
        //////Retrieve info from app.Config file
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        ////

        private static int _cntLoginTry = 0;
        private string sLoginName;
        private string sPassword;
        private const string _encryptionKey = "12345678";

        #endregion 'Declarations'

        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";
        private bool _blnClose = false;
       
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
      
       //Int32 x;
        [DllImport("User32.dll", EntryPoint = "disable")]
        private static extern Int32 disable(ref Int32 a, ref Boolean b, ref Boolean c, ref Int32 d);

        [DllImport("user32.dll")]
        private static extern bool LockWindowUpdate(IntPtr hWnd);

        private void frmLockScreen_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
            //Int32 a, d;
            //Boolean b, c;
            //a = 97;
            //b = true;
            //c = false;
            //d = 0;
            //x = disable(ref a,ref b,ref c,ref d);

         //   LockWindowUpdate(this.Handle);

            try
            {
                LockWindowUpdate(this.Handle);
                this.SuspendLayout();
               // gloUIControlLibrary.WPFUserControl.gloPMLockScreen obj = new gloUIControlLibrary.WPFUserControl.gloPMLockScreen();
              // elementHost1.Child = obj;
                this.ResumeLayout();
                // NoofAttempts = 0
            }
            catch (Exception objErr)
            {
                MessageBox.Show(objErr.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        //Problem 00000363
        //Added code to resolve problem of flickring.
        protected override CreateParams CreateParams
        {
            get
            {
                // Activate double buffering at the form level.  All child controls will be double buffered as well.
                CreateParams cp = base.CreateParams;
                if (gloSettings.gloRegistrySetting.IsServerOS)
                {
                    cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                }
                return cp;
            }
        } 

        private void frmLockScreen_Shown(object sender, EventArgs e)
        {
            LockWindowUpdate(IntPtr.Zero);
            Point pt = new Point();
            pt.Y= Convert.ToInt32((this.Height / 2) - (pnlLogin.Height / 2));
            pt.X = Convert.ToInt32((this.Width/ 2) - (pnlLogin.Width/ 2));

          
            pnlLogin.Location = pt;

        }

       
        private void frmLockScreen_SizeChanged(object sender, EventArgs e)
        {
            Point pt = new Point();
            pt.Y = Convert.ToInt32((this.Height / 2) - (pnlLogin.Height / 2));
            pt.X = Convert.ToInt32((this.Width / 2) - (pnlLogin.Width / 2));

            pnlLogin.Location = pt;
        }
        /// <summary>
        /// Checking Login Validatation's.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
      
        private void btnOK_Click(object sender, EventArgs e)
        {
            ClsEncryption oEncrypt = new ClsEncryption();
            gloValidateUser oVU = new gloValidateUser(gloPMGlobal.DatabaseConnectionString);
            gloUser oGetUser = new gloUser(gloPMGlobal.DatabaseConnectionString);
            User oUser = new User();           
            try
            {
                if (txtUserName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter your username.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUserName.Focus();
                    return;
                }

                if (txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter your password.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPassword.Focus();
                    return;
                }
                sLoginName = txtUserName.Text;
                sPassword = txtPassword.Text;


                
                string e_sPassword = oEncrypt.EncryptToBase64String(sPassword, _encryptionKey);
                Program.LockOutAttempts = getLockOutAttempts(); //added on 07/05/2010

                #region " User Authentication & Credentials check"
                //Check login whether entered user exists.
                if (!oVU.chkLogin(sLoginName, e_sPassword))
                {
                    // added for issue no:  1532
                    #region  " Check Is Blocked"
                    if (oVU.chkISBlocked(sLoginName))
                    {
                        MessageBox.Show("User is Blocked.Please contact the administrator. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ////added on 21/04/2010 case:0004256                   
                        _blnClose = true;
                        //this.Close();
                        //Application.Exit();
                        System.Environment.Exit(0);
                        //return;
                    }
                    #endregion

                    #region  " Check Access Deneid"
                    //Checking for Access is given to user or not.
                    if (oVU.chkIsAccessDenied(sLoginName))
                    {
                        MessageBox.Show("Access denied. Please contact your administrator. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ////added on 21/04/2010 case:0004256                   
                        _blnClose = true;
                        //this.Close();
                        //Application.Exit();
                        System.Environment.Exit(0);
                        //return;
                    }
                    #endregion  " Check Access Deneid"

                    MessageBox.Show("Invalid user name or password.  Please try again.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.UpdateRemoteLoginDetails(sLoginName, false , "", gloAuditTrail.SoftwareComponent.gloPM.ToString(), _ClinicID);
                    
                    //txtUserName.Text = "";
                    //txtPassword.Text = "";
                    txtUserName.Focus();
                    txtPassword.Clear();

                    //Checking the No Of Login try only 3 allowed.
                    //if (_cntLoginTry < 2)//Commented By Debasish Das on 26th Mar 2010
                    if (_cntLoginTry < Program.LockOutAttempts - 1) //Added Program.LockOutAttempts - 1 by Debasish Das on 26th Mar 2010
                    {
                        _cntLoginTry = _cntLoginTry + 1;
                    }
                    else
                    {

                        //added on 21/04/2010 case:0004256
                        // set Accesed denied to user becouse all no of lockout attempt failed                                                   
                        if (!oVU.IsUserAdministrator(sLoginName)) //check for Admin - skip admin user for denied
                        {
                            oVU.SetAccessDenied(sLoginName);
                            MessageBox.Show("Access denied. Please contact your administrator.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            _blnClose = true;
                            //this.Close();
                            //Application.Exit();
                            System.Environment.Exit(0);

                        }
                        txtPassword.Clear();
                        txtUserName.Focus();

                        ////Code Chages made by Sagar Ghodke on 20081210
                        ////
                        ////this.Close();
                        //gloSecurity.gloUser ogloUser = new gloUser(_databaseconnectionstring);
                        //ogloUser.BlockUser(_UserID, true);       
                        //Application.ExitThread(); 
                        ////End Code Chages 20081210

                    }
                    return;

                }
                #endregion " User Authentication & Credentials check"

                #region  " Check Is Blocked"
                if (oVU.chkISBlocked(sLoginName))
                {
                    MessageBox.Show("User is Blocked.Please contact the administrator. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ////added on 21/04/2010 case:0004256                   
                    _blnClose = true;
                    //this.Close();
                    //Application.Exit();
                    System.Environment.Exit(0);
                    //return;
                }
                #endregion

                #region  " Check Access Deneid"
                //Checking for Access is given to user or not.
                if (oVU.chkIsAccessDenied(sLoginName))
                {
                    MessageBox.Show("Access denied. Please contact your administrator.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ////added on 21/04/2010 case:0004256                   
                    _blnClose = true;
                    //this.Close();
                    //Application.Exit();
                    System.Environment.Exit(0);
                    //return;
                }
                #endregion  " Check Access Deneid"

                #region  " Set Values for logged in User "


                //Creating object of User who is trying to login 
                //Now we can remove this code since we are setting user in app.config
                
                DataTable dttempUser;
                dttempUser = oGetUser.GetUser(oVU.nUserID);

                try
                {

                    oUser.UserID = Convert.ToInt64(dttempUser.Rows[0]["nUserID"]);
                    oUser.UserName = dttempUser.Rows[0]["sLoginName"].ToString();
                    oUser.Password = dttempUser.Rows[0]["sPassword"].ToString();
                    oUser.IsAdministrator = Convert.ToBoolean(dttempUser.Rows[0]["nAdministrator"]);
                    oUser.ProviderID = Convert.ToInt64(dttempUser.Rows[0]["nProviderID"]);
                    oUser.IsAuditTrail = Convert.ToBoolean(dttempUser.Rows[0]["IsAuditTrail"]);
                    //Add login users UserID in appsettings
                  //  if (oUser.UserID != null)
                    {
                        appSettings["UserID"] = Convert.ToString(oUser.UserID);

                    }
                    //Added By Pramod Nair For User Rights
                    if (oUser.UserName != null)
                    {
                        appSettings["UserName"] = Convert.ToString(oUser.UserName);

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if(dttempUser != null)
                    {
                        dttempUser.Dispose();  
                    }
                }

                #endregion  " Set Values for logged in User "

                //Added by manoj jadhav on 20150203 V8040 For Split Screen changes reading View document settings on Reloginng
                #region "SpiltSCreen Settings" 
                gloSettings.GeneralSettings ogloSettings=null;
                try
                {
                    object objSettingValue = null;

                    ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);

                    ogloSettings.GetSetting("bViewDocumentsOnCharges", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.ViewDocumentsOnCharges = Convert.ToBoolean(objSettingValue);
                    }
                    objSettingValue = null;
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex = null;
                }
                finally
                {
                    if (ogloSettings != null)
                    {
                        ogloSettings.Dispose();
                        ogloSettings = null;
                    }                   
                }
                #endregion

                #region "charge edit validate all claim Insurancess Setting"

                try
                {
                    object objSettingValue = null;

                    if (ogloSettings == null)
                    { ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString); }

                    ogloSettings.GetSetting("bchargeeditvalidateallclaimInsurancess", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.ChargeEditValidateAllClaimInsurances = Convert.ToBoolean(objSettingValue);
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (ogloSettings != null)
                    {
                        ogloSettings.Dispose();
                        ogloSettings = null;
                    }
                }

                #endregion

                #region "ExternalCollectionfeature Setting"

                try
                {
                    object objSettingValue = null;

                    if (ogloSettings == null)
                    { ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString); }

                    ogloSettings.GetSetting("ExternalCollectionfeature", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled = Convert.ToBoolean(objSettingValue);
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (ogloSettings != null)
                    {
                        ogloSettings.Dispose();
                        ogloSettings = null;
                    }
                }

                #endregion

                #region "Reason Code Setup Setting"
                try
                {
                    object objSettingValue = null;
                    if (ogloSettings == null)
                    { ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString); }
                    ogloSettings.GetSetting("InsurancePaymentResoneCodeSetup", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.ReasonCodeSetup = Convert.ToInt16(objSettingValue);
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region "Patient Appointments Linking to Charges Settings"
                try
                {
                    object objSettingValue = null;

                    if (ogloSettings == null)
                    { ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString); }

                    ogloSettings.GetSetting("EnableAppointmentLinkingToCharges", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        Boolean bValue;

                        if (Boolean.TryParse(Convert.ToString(objSettingValue), out bValue))
                        { gloGlobal.gloPMGlobal.EnableAppointmentLinkingToCharges = bValue; }

                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (ogloSettings != null)
                    {
                        ogloSettings.Dispose();
                        ogloSettings = null;
                    }
                }
                #endregion

                #region "sClaimPrefix Setting"

                try
                {
                    object objSettingValue = null;

                    if (ogloSettings == null)
                    { ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString); }

                    ogloSettings.GetSetting("sClaimPrefix", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.sClaimPrefix = Convert.ToString(objSettingValue);
                    }
                    else
                    {
                        gloGlobal.gloPMGlobal.sClaimPrefix = "";
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (ogloSettings != null)
                    {
                        ogloSettings.Dispose();
                        ogloSettings = null;
                    }
                }
                
                try
                {
                    object objSettingValue = null;

                    if (ogloSettings == null)
                    { ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString); }

                    ogloSettings.GetSetting("bUsePrefixforBatch", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.IsUseBatchClaimPrefix = Convert.ToBoolean(objSettingValue);
                    }
                    else
                    {
                        gloGlobal.gloPMGlobal.IsUseBatchClaimPrefix = false;
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (ogloSettings != null)
                    {
                        ogloSettings.Dispose();
                        ogloSettings = null;
                    }
                }

                try
                {
                    object objSettingValue = null;

                    if (ogloSettings == null)
                    { ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString); }

                    ogloSettings.GetSetting("bUsePrefixforClaims", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.IsUseClaimPrefix = Convert.ToBoolean(objSettingValue);
                    }
                    else
                    {
                        gloGlobal.gloPMGlobal.IsUseClaimPrefix = false;
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (ogloSettings != null)
                    {
                        ogloSettings.Dispose();
                        ogloSettings = null;
                    }
                }
                #endregion

                #region " Login "

                //Check if login user is Provider then set the ProviderID.
                try
                {

                    //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
                    appSettings.Set("UserName", oUser.UserName);
                    appSettings.Set("UserID", Convert.ToString(oUser.UserID));
                    appSettings.Set("ProviderID", Convert.ToString(oUser.ProviderID));
                    appSettings.Set("DataBaseConnectionString", gloPMGlobal.DatabaseConnectionString);

                    #region "License Check"
                    string smessage = "";
                    smessage = base.ValidateLogin(oUser.ProviderID, gloPMGlobal.DatabaseConnectionString);
                    if (!string.IsNullOrEmpty((smessage)))
                    {
                        if (MessageBox.Show(smessage, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            System.Environment.Exit(0);
                        }
                    }
                    smessage = "";
                    #endregion "License Check"

                    gloPMGlobal.UserID = oUser.UserID;
                    gloPMGlobal.UserName = oUser.UserName;
                    gloPMGlobal.LoginProviderID = oUser.ProviderID;
                    gloPMGlobal.IsAccountsOn = GetPatientAccountFeatureSetting();
                    gloAuditTrail.gloAuditTrail.UpdateRemoteLoginDetails(sLoginName, true, "", gloAuditTrail.SoftwareComponent.gloPM.ToString(), _ClinicID);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Success);
                    _blnClose = true;
                    this.Close();
                }
                catch (Exception) // ex)
                {
                    //ex.ToString();
                    //ex = null;
                    throw;
                }
                finally
                {
                    oVU.updateLoginStatus(sLoginName, true, System.Windows.Forms.SystemInformation.ComputerName);
                    //gloAuditTrail.gloAuditTrail.UpdateRemoteLoginDetails(sLoginName, true, "", gloAuditTrail.SoftwareComponent.gloPM.ToString(), _ClinicID);
                }

                #endregion " Login "

                #region "getICDReviosn"
                try
                {
                    gloGlobal.gloPMGlobal.CurrentICDRevision=gloGlobal.clsICD.GetICDCodeType();
                }
                catch (Exception Ex)
                {
                    gloGlobal.gloPMGlobal.CurrentICDRevision = gloICD.CodeRevision.ICD10;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
                }
                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oUser.Dispose();  
                oGetUser.Dispose(); 
                oVU.Dispose();
                oEncrypt.Dispose(); 

            }
     
        }

        private void frmLockScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_blnClose == false)
                e.Cancel = true ;
        }

        #region "Private Methods"

        public Int32 getLockOutAttempts()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dtDataTable = new DataTable();
            string sSQL;
            Int32 iReturn = 1;
            try
            {
                oDB.Connect(false);
                sSQL = "SELECT sSettingsValue FROM SETTINGS WITH (NOLOCK)  WHERE sSettingsName = 'No. Of. Attempts'";
                oDB.Retrive_Query(sSQL, out dtDataTable);

                if (dtDataTable != null && dtDataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtDataTable.Rows[0]["sSettingsValue"]) != 0 )
                    {
                        iReturn = Convert.ToInt32(dtDataTable.Rows[0]["sSettingsValue"]);
                    }
                    else
                    {
                        iReturn = 1;
                    }
                }
                oDB.Disconnect();
                oDB.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return iReturn;
        }

        private bool GetPatientAccountFeatureSetting()
        {

            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);
                string _strSqlQuery = "select ISNULL(sSettingValue,'') AS sSettingsValue from Settings_Replication WITH (NOLOCK) where sSettingName='Patient Account Feature'";
                result = oDB.ExecuteScalar_Query(_strSqlQuery);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            if (result.ToString().Trim().Length == 0)
                result = 0;
            return Convert.ToBoolean(result);
        }


        #endregion "Private Methods"


    }
}